'Option Strict On
Imports SpectrumBL
Imports System.IO
Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Collections
Imports System.Collections.Generic

Public Class ArticleComboPopup

#Region "Constructor"
    Public Sub New(ByVal isEdit As Boolean, Optional ByVal isCalledFromDineIn As Boolean = False)
        ' This call is required by the designer.
        InitializeComponent()
        editMode = isEdit
        ComboArticles = New DataTable()
        InitializeComboArticleTable()
        If editMode = False Then
            SelectedComboArticles = New DataTable()
            InitializeSelectedArticleTable(isCalledFromDineIn)
        End If

        GridSettings()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
#End Region

#Region "Variables"
    Dim clsArticleCombo As New clsArticleCombo
    Dim editMode As Boolean
    Dim objCM As New clsCashMemo
#End Region

#Region "Public Properties"

    Private _ComboQuantity As Integer = 1
    Public Property ComboQuantity As Integer
        Get
            Return _ComboQuantity
        End Get
        Set(ByVal value As Integer)
            _ComboQuantity = value
        End Set
    End Property


    Private _comboDetails As DataTable
    Public Property ComboDetails As DataTable
        Get
            Return _comboDetails
        End Get
        Set(ByVal value As DataTable)
            _comboDetails = value
        End Set
    End Property

    Private _comboArticles As DataTable
    Public Property ComboArticles As DataTable
        Get
            Return _comboArticles
        End Get
        Set(ByVal value As DataTable)
            _comboArticles = value
        End Set
    End Property

    Private _selectedComboArticles As DataTable
    Public Property SelectedComboArticles As DataTable
        Get
            Return _selectedComboArticles
        End Get
        Set(ByVal value As DataTable)
            _selectedComboArticles = value
        End Set
    End Property

    Private _articleGridSource As DataTable
    Public Property ArticleGridSource As DataTable
        Get
            Return _articleGridSource
        End Get
        Set(ByVal value As DataTable)
            _articleGridSource = value
        End Set
    End Property

    Private _comboPartNumber As Integer
    Public Property ComboPartNumber As Integer
        Get
            Return _comboPartNumber
        End Get
        Set(ByVal value As Integer)
            _comboPartNumber = value
        End Set
    End Property

    Private _comboPartQuantity As Integer
    Public Property ComboPartQuantity As Integer
        Get
            Return _comboPartQuantity
        End Get
        Set(ByVal value As Integer)
            _comboPartQuantity = value
        End Set
    End Property

    Private _additionalComboCost As Integer
    Public Property AdditionalComboCost As Integer
        Get
            Return _additionalComboCost
        End Get
        Set(ByVal value As Integer)
            _additionalComboCost = value
        End Set
    End Property


    Private _originalComboItemCost As Double
    Public Property OriginalComboItemCost As Double
        Get
            Return _originalComboItemCost
        End Get
        Set(ByVal value As Double)
            _originalComboItemCost = value
        End Set
    End Property

    Private _originalComboHierarchyCost As Double
    Public Property OriginalComboHierarchyCost As Double
        Get
            Return _originalComboHierarchyCost
        End Get
        Set(ByVal value As Double)
            _originalComboHierarchyCost = value
        End Set
    End Property

    Private ReadOnly Property ButtonSize As System.Drawing.Size
        Get
            Return New Size(80, 80)
        End Get
    End Property

    Dim isUpgraded As Boolean
    Private _IndividualQty As Integer
    Public Property IndividualQty As Integer
        Get
            Return _IndividualQty
        End Get
        Set(ByVal value As Integer)
            _IndividualQty = value
        End Set
    End Property
#End Region

#Region "Class Events"
    Private Sub ArticleComboPopup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            CtrlNumberPad.NumberEntered = AddressOf ChangeQuantity
            If Not ComboDetails Is Nothing AndAlso ComboDetails.Rows.Count > 0 Then
                'Commented BY gaurav for Generic change
                'VerifyIfUpgradable()
                'Commented BY gaurav for Generic change
                'code added by vipul for sabarro log 
                If editMode = False Then
                    ArticleAddEditDeletelog(ComboDetails.Rows(0)("ComboCode").ToString() + " COMBO ADDED " + " Currently On  ComboScreen")
                End If
                txtComboQuantity.Text = ComboQuantity.ToString()
                'IndividualQty = CInt(ComboDetails.Rows(0).Item("IndividualQty").ToString())
                HideUpgradePanel()
                PopulateComboArticles()
                If editMode Then
                    HandleEditMode()
                    ArticleAddEditDeletelog(ComboDetails.Rows(0)("ComboCode").ToString() + " COMBO EDITED  " + "Currently On  ComboScreen")
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtComboQuantity_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtComboQuantity.LostFocus
        Try
            Dim result As Integer
            If Integer.TryParse(txtComboQuantity.Text, result) AndAlso result > 0 Then
                If hdrLbl.Text.Contains(ComboPartQuantity.ToString()) Then
                    hdrLbl.Text = hdrLbl.Text.Replace(ComboPartQuantity.ToString(), (ComboPartQuantity / ComboQuantity).ToString() * result.ToString())
                End If
                ComboPartQuantity = (ComboPartQuantity / ComboQuantity) * result
                ComboQuantity = result
                VerifyAndEnableOkAndNext()
            Else
                txtComboQuantity.Text = ComboQuantity
                ShowMessage(getValueByKey("articlecombopopup.invalidcomboqty"), getValueByKey("CLAE04"))
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub HandleEditMode()
        Try
            Dim comboPartNumberCheck As Integer = SelectedComboArticles.Rows(0)("ComboPartNumber")
            Dim code As String = SelectedComboArticles.Rows(0)("ARTICLECODE")
            For Each row In SelectedComboArticles.Rows
                If row("ComboPartNumber") = ComboPartNumber Then
                    ArticleGridSource.Rows.Add("", row("ARTICLECODE"), row("DISCRIPTION"), row("SELLINGPRICE"), row("Quantity"), row("individualQty"))
                End If
            Next
            HighLightFirstRecord()
            SetTotalItemCount()
            SetTotalPrice()
            txtComboQuantity.Text = itemCount.Text / ComboPartQuantity
            'txtComboQuantity.ReadOnly = True
            txtComboQuantity_LostFocus(Nothing, New EventArgs())
            VerifyAndEnableOkAndNext()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub UpgradeBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles upgradeBtn.Click
        articlePanel.Controls.Clear()
        UpgradeArticleHierarchy()
        SetUpgradeFlag()
    End Sub
    Private Sub ChangeVisibilityOfComboQuantity()
        Try
            'If editMode = False Then
            If ComboPartNumber = 0 Then
                txtComboQuantity.ReadOnly = False
            Else
                txtComboQuantity.ReadOnly = True
            End If
            ' End If           
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub NextBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles nextBtn.Click
        Try
            If Not backBtn.Enabled Then
                backBtn.Enabled = True
            End If
            '---commented and changhed by mahesh for performance Increment 
            Me.articlePanel.SuspendLayout()
            '   articlePanel.Visible = False
            'articlePanel.Controls.Clear()
            'upgradePanel.Controls.Clear()
            Me.articlePanel.AutoScroll = False

            HideUpgradePanel()
            ArticleGridSource.Rows.Clear()
            itemCount.Text = "0"
            amount.Text = "0"
            'Commented BY gaurav for Generic change
            'VerifyIfUpgradable()
            'upgradeBtn.Text = "Upgrade"
            'isUpgraded = False
            'Commented BY gaurav for Generic change
            ChangeVisibilityOfComboQuantity()
            PopulateComboArticles()
            Me.articlePanel.AutoScroll = True
            ' articlePanel.Visible = True
            Me.articlePanel.ResumeLayout()
            'Me.Refresh()
            'Me.ResumeLayout()
            '---- Changes By Mahesh For Increase Performance
            'For Each row In SelectedComboArticles.Rows
            '    If row("ComboPartNumber") = ComboPartNumber Then
            '        ArticleGridSource.Rows.Add("", row("ARTICLECODE"), row("DISCRIPTION"), row("SELLINGPRICE"), row("Quantity"), row("IndividualQty"))
            '    End If
            'Next
            For index = 0 To SelectedComboArticles.Rows.Count - 1
                If SelectedComboArticles.Rows(index)("ComboPartNumber") = ComboPartNumber Then
                    ArticleGridSource.Rows.Add("", SelectedComboArticles.Rows(index)("ARTICLECODE"), SelectedComboArticles.Rows(index)("DISCRIPTION"), SelectedComboArticles.Rows(index)("SELLINGPRICE"), SelectedComboArticles.Rows(index)("Quantity"), SelectedComboArticles.Rows(index)("IndividualQty"))
                End If
            Next
            HighLightFirstRecord()
            SetTotalItemCount()
            SetTotalPrice()
            'DisableUpgradeButton()
            'Commented BY gaurav for Generic change
            'If upgradeBtn.Enabled Then
            '    DisableUpgradeButton()
            'End If
            'Commented BY gaurav for Generic change
            VerifyAndEnableOkAndNext()
            'nextBtn.Enabled = False
            ArticleAddEditDeletelog("CLICK ON NEXT BUTTON")
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub OkBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles okBtn.Click
        CalculateLineDisount()
        CalculateComboAdditionalCost()
        CtrlNumberPad.NumberEntered = Nothing
        ArticleAddEditDeletelog("CLICK ON OK BUTTON")
        Me.Close()
    End Sub

    Private Sub CancelBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs, Optional ByVal IsCancleBtnpress As Boolean = True) Handles cancelBtn.Click
        SelectedComboArticles.Rows.Clear()
        ArticleGridSource.Rows.Clear()
        ComboArticles.Rows.Clear()
        CtrlNumberPad.NumberEntered = Nothing
        If IsCancleBtnpress Then
            ArticleAddEditDeletelog("CLICK ON CANCEL BUTTON")
        End If

        Me.Close()
    End Sub

    Private Sub BackBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backBtn.Click
        Try
            ComboPartNumber = ComboPartNumber - 2
            '---commented and changhed by mahesh for performance Increment 
            '  Me.articlePanel.SuspendLayout()
            'Me.SuspendLayout()
            articlePanel.Visible = False
            'articlePanel.Controls.Clear()
            'upgradePanel.Controls.Clear()

            HideUpgradePanel()
            ArticleGridSource.Rows.Clear()
            'Commented BY gaurav for Generic change
            'VerifyIfUpgradable()
            'Commented BY gaurav for Generic change
            If ComboPartNumber = 0 Then
                backBtn.Enabled = False
            End If
            ChangeVisibilityOfComboQuantity()
            PopulateComboArticles()
            Me.articlePanel.AutoScroll = True
            articlePanel.Visible = True
            'Me.articlePanel.ResumeLayout()
            'Me.Refresh()
            'Me.ResumeLayout()
            For Each row In SelectedComboArticles.Rows
                If row("ComboPartNumber") = ComboPartNumber Then
                    ArticleGridSource.Rows.Add("", row("ARTICLECODE"), row("DISCRIPTION"), row("SELLINGPRICE"), row("Quantity"), row("IndividualQty"))
                End If
            Next
            HighLightFirstRecord()
            SetTotalItemCount()
            SetTotalPrice()
            'Commented BY gaurav for Generic change
            'If upgradeBtn.Text = "Upgrade" AndAlso upgradeBtn.Enabled Then
            '    DisableUpgradeButton()
            'End If
            'Commented BY gaurav for Generic change
            VerifyAndEnableOkAndNext()
            ArticleAddEditDeletelog("CLICK ON BACK BUTTON")
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub HighLightLastRecord()
        If CtrlGrid1.Rows.Count > 0 Then
            CtrlGrid1.Select(CtrlGrid1.Rows.Count - 1, 1, 1, 1, True)
        End If
    End Sub

    Private Sub HighLightFirstRecord()
        If CtrlGrid1.Rows.Count > 1 Then
            CtrlGrid1.Select(1, 1, 1, 1, True)
        End If
    End Sub
    Private Sub ChangeQuantity(ByVal enteredQuantity As String)
        Try
            Dim quantity As Integer
            If Not String.IsNullOrEmpty(enteredQuantity) AndAlso Integer.TryParse(enteredQuantity, quantity) AndAlso CtrlGrid1.Rows.Count > 1 AndAlso CtrlGrid1.Row >= 0 Then
                qtyBeforeChange = CtrlGrid1.Rows(CtrlGrid1.Row)("Qty")
                CtrlGrid1.Rows(CtrlGrid1.Row)("Qty") = quantity
                QuantityChanged(Nothing, New C1.Win.C1FlexGrid.RowColEventArgs(CtrlGrid1.Row, 0))
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Dim qtyBeforeChange As Integer

    Private Sub CtrlGrid1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles CtrlGrid1.KeyDown
        If (e.KeyCode = Keys.Delete AndAlso CtrlGrid1.Rows.Count > 1) Then
            Try
                Dim taxCount = SelectedComboArticles.Rows.Count
                Dim taxIndex As Integer = -1
                For index = 0 To taxCount - 1
                    If (SelectedComboArticles.Rows(index)("ComboPartNumber").Equals(ComboPartNumber.ToString()) AndAlso _
                        SelectedComboArticles.Rows(index)("ArticleCode").Equals(CtrlGrid1.Rows(CtrlGrid1.Row)("ArticleCode")) AndAlso _
                        SelectedComboArticles.Rows(index)("Quantity").Equals(CtrlGrid1.Rows(CtrlGrid1.Row)("Qty"))) Then
                        taxIndex = index
                        Exit For
                    End If
                Next

                If (taxIndex <> -1) Then
                    SelectedComboArticles.Rows.RemoveAt(taxIndex)
                End If

                'Dim dataRow As DataRow = (From row In SelectedComboArticles.Rows Where (row("ComboPartNumber") = ComboPartNumber AndAlso row("ArticleCode") = ArticleGridSource.Rows(CtrlGrid1.Row - 1)("ArticleCode") AndAlso row("Quantity") = ArticleGridSource.Rows(CtrlGrid1.Row - 1)("Qty")) Select row).FirstOrDefault()
                ''Dim dataRow As DataRow = (From row In SelectedComboArticles.Rows Where (row("ComboPartNumber") = ComboPartNumber AndAlso row("DISCRIPTION") = ArticleGridSource.Rows(e.Row - 1)("Description")) Select row).FirstOrDefault()
                'If dataRow("IsUpgraded") = True Then
                '    'UpdateComboCost(dataRow("SELLINGPRICE"), True)
                'End If
                ArticleAddEditDeletelog(ArticleGridSource.Rows(CtrlGrid1.Row - 1)("ArticleCode") + " " + ArticleGridSource.Rows(CtrlGrid1.Row - 1)("Description") + "  " + "DELETED FROM COMBO GRID FROM DELETE BUTTON")
                ArticleGridSource.Rows(CtrlGrid1.Row - 1).Delete()
                'dataRow.Delete()
                SetTotalItemCount()
                SetTotalPrice()
                'Commented BY gaurav for Generic change
                'DisableUpgradeButton()
                'Commented BY gaurav for Generic change
                VerifyAndEnableOkAndNext()
            Catch ex As Exception
                LogException(ex)
            End Try
        End If
    End Sub
    Private Sub CtrlGrid1_StartEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles CtrlGrid1.StartEdit
        Try
            qtyBeforeChange = CtrlGrid1.Rows(e.Row)("Qty")
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub CtrlGrid1_AfterSelChange(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles CtrlGrid1.AfterSelChange
        Try
            Me.CtrlNumberPad.ClearNumber()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub QuantityChanged(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles CtrlGrid1.AfterEdit
        Try
            Dim result As Integer
            If Int32.TryParse(CtrlGrid1.Rows(e.Row)("Qty"), result) = False Or result <= 0 Then
                MessageBox.Show(getValueByKey("CM021"))
                CtrlGrid1.Rows(e.Row)("Qty") = qtyBeforeChange
                Me.CtrlNumberPad.ClearNumber()
                SetTotalItemCount()
                SetTotalPrice()
                qtyBeforeChange = 0
                Exit Sub
            End If

            Dim totalCount As Integer = (From dataRow In ArticleGridSource.Rows Select dataRow).Sum(Function(art) art("Qty")).ToString()
            If Not editMode AndAlso ComboPartQuantity < totalCount AndAlso CtrlGrid1.Rows(e.Row)("Qty") > qtyBeforeChange Then
                MessageBox.Show("Item can not be added", "Warning", MessageBoxButtons.OK)
                CtrlGrid1.Rows(e.Row)("Qty") = qtyBeforeChange
                Me.CtrlNumberPad.ClearNumber()
                SetTotalItemCount()
                SetTotalPrice()

                'ElseIf Not editMode AndAlso totalCount > ComboPartQuantity Then
                '    MessageBox.Show("Item can not be added", "Warning", MessageBoxButtons.OK)
                '    CtrlGrid1.Rows(e.Row)("Qty") = qtyBeforeChange
                '    Me.CtrlNumberPad.ClearNumber()
                '    SetTotalItemCount()
                '    SetTotalPrice()

            ElseIf editMode AndAlso ComboPartQuantity < totalCount AndAlso CtrlGrid1.Rows(e.Row)("Qty") > qtyBeforeChange Then
                MessageBox.Show("Item can not be added", "Warning", MessageBoxButtons.OK)
                CtrlGrid1.Rows(e.Row)("Qty") = qtyBeforeChange
                Me.CtrlNumberPad.ClearNumber()
                SetTotalItemCount()
                SetTotalPrice()

            Else
                'Dim row As DataRow = (From dataRow In SelectedComboArticles.Rows Where dataRow("ARTICLECODE") = CtrlGrid1.Rows(e.Row)("ARTICLECODE") Select dataRow).FirstOrDefault()
                If SelectedComboArticles.Select("ComboPartNumber='" & ComboPartNumber & "' AND Quantity='" & qtyBeforeChange & "' AND ArticleCode='" & CtrlGrid1.Rows(e.Row)("ArticleCode") & "' ").Length > 0 Then
                    SelectedComboArticles.Select("ComboPartNumber='" & ComboPartNumber & "' AND Quantity='" & qtyBeforeChange & "' AND ArticleCode='" & CtrlGrid1.Rows(e.Row)("ArticleCode") & "' ").FirstOrDefault()("Quantity") = CtrlGrid1.Rows(e.Row)("Qty")
                    'by Adil for sbarro 11-jan-2017
                    'commented by khusrao adil for revert purpose on 18-jan-2017
                    'commented by khusrao adil for 27-07-2017 for mod issue
                    SelectedComboArticles.Select("ComboPartNumber='" & ComboPartNumber & "' AND IndividualQty='" & qtyBeforeChange & "' AND ArticleCode='" & CtrlGrid1.Rows(e.Row)("ArticleCode") & "' ").FirstOrDefault()("IndividualQty") = CtrlGrid1.Rows(e.Row)("Qty")
                End If

                'SelectedComboArticles.Rows(e.Row - 1)("Quantity") = CtrlGrid1.Rows(e.Row)("Qty")
                SetTotalItemCount()
                SetTotalPrice()
                'Commented BY gaurav for Generic change
                'If upgradeBtn.Text = "Upgrade" AndAlso upgradeBtn.Enabled Then
                '    DisableUpgradeButton()
                'End If
                'Commented BY gaurav for Generic change
                VerifyAndEnableOkAndNext()
            End If
            qtyBeforeChange = 0
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub MstArticleClicked(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Convert.ToInt32(itemCount.Text) >= ComboPartQuantity Then
                MessageBox.Show("Item can not be added", "Warning", MessageBoxButtons.OK)
            Else
                Dim row As DataRow = (From dataRow In ComboArticles Where dataRow("ARTICLECODE") = DirectCast(sender, Button).Tag Select dataRow).FirstOrDefault()

                'code added by vipul checking kit article mapping 
                If objCM.CheckKitMapping(row("ARTICLECODE").ToString()) = False Then
                    ShowMessage("Article not present in the Kit.", getValueByKey("CLAE04"))
                    Exit Sub
                End If
                Dim ifExist As DataRow = (From dataRow In ArticleGridSource Where dataRow("Description") = row("DISCRIPTION") Select dataRow).FirstOrDefault()
                If ifExist Is Nothing Then
                    'ArticleGridSource.Rows.Add("", row("ARTICLECODE"), row("DISCRIPTION"), row("SELLINGPRICE"), "2")
                    ArticleGridSource.Rows.Add("", row("ARTICLECODE"), row("DISCRIPTION"), row("SELLINGPRICE"), "1", row("IndividualQty"))
                    SelectedComboArticles.Rows.Add(row("ARTICLECODE"), row("EAN"), row("DISCRIPTION"), row("SELLINGPRICE"), ComboDetails.Rows(ComboPartNumber - 1)("Cost"), "1", row("IndividualQty"), ComboPartNumber, 0, False)

                    'code added by vipul for log 
                    Try
                        ArticleAddEditDeletelog("combocode " + ComboDetails.Rows(ComboPartNumber - 1)("ComboCode") + "  article added " + row("ARTICLECODE") + " EAN " + row("EAN") + " " + row("DISCRIPTION")) 'log
                    Catch ex As Exception
                        LogException(ex)
                    End Try

                Else
                    'ifExist("Quantity") = ifExist("Quantity") + 1
                    'ArticleGridSource.Rows.Add("", row("ARTICLECODE"), row("DISCRIPTION"), row("SELLINGPRICE"), "2")
                    'code added by khusrao adil
                    'commented by khusrao adil for revert purpose on 18-jan-2017
                    'ifExist("Qty") = ifExist("Qty") + 1
                    ''ArticleGridSource.Rows(row(5))("Qty") = ifExist("Qty")
                    'For Each dr As DataRow In ArticleGridSource.Rows
                    '    If dr("ARTICLECODE") = row("ARTICLECODE") Then
                    '        dr("Qty") = ifExist("Qty")
                    '        dr("IndividualQty") = ifExist("Qty")
                    '    End If
                    'Next
                    'For Each dr As DataRow In SelectedComboArticles.Rows
                    '    If dr("ARTICLECODE") = row("ARTICLECODE") Then
                    '        dr("Quantity") = ifExist("Qty")
                    '        dr("IndividualQty") = ifExist("Qty")
                    '    End If
                    'Next
                    'comendted by khusrao adil
                    'comment remove by khusrao adil for revert purpose on 18-jan-2017
                    ArticleGridSource.Rows.Add("", row("ARTICLECODE"), row("DISCRIPTION"), row("SELLINGPRICE"), "1", row("IndividualQty"))

                    SelectedComboArticles.Rows.Add(row("ARTICLECODE"), row("EAN"), row("DISCRIPTION"), row("SELLINGPRICE"), ComboDetails.Rows(ComboPartNumber - 1)("Cost"), "1", row("IndividualQty"), ComboPartNumber, 0, False)
                    ArticleAddEditDeletelog("combocode " + ComboDetails.Rows(ComboPartNumber - 1)("ComboCode") + "  article added " + row("ARTICLECODE") + " EAN " + row("EAN") + " " + row("DISCRIPTION")) 'log
                End If
                HighLightLastRecord()
                'If ComboPartNumber = ComboDetails.Rows.Count Then
                '    SelectedComboArticles.Rows.Add(row("ARTICLECODE"), row("DISCRIPTION"), row("SELLINGPRICE"), "1", ComboPartNumber)
                'End If
                SetTotalItemCount()
                SetTotalPrice()
                'Commented BY gaurav for Generic change
                'If upgradeBtn.Text = "Upgrade" AndAlso upgradeBtn.Enabled Then
                '    DisableUpgradeButton()
                'End If
                'Commented BY gaurav for Generic change
                VerifyAndEnableOkAndNext()
                'Commented BY gaurav for Generic change
                'If isUpgraded Then
                '    UpdateComboCost(row("SELLINGPRICE"))
                'End If
                'Commented BY gaurav for Generic change
                'Added BY gaurav for Generic change
                If isUpgraded AndAlso row("ARTICLECODE") <> ComboDetails.Rows(ComboPartNumber - 1)("Item") Then
                    SelectedComboArticles.Rows(SelectedComboArticles.Rows.Count - 1)("IsUpgraded") = True
                    'UpdateComboCost(row("SELLINGPRICE"))
                End If
                'Added BY gaurav for Generic change
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub ArticleGrid_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles CtrlGrid1.CellButtonClick
        Try
            Dim dataRow As DataRow = (From row In SelectedComboArticles.Rows Where (row("ComboPartNumber") = ComboPartNumber AndAlso row("ArticleCode") = ArticleGridSource.Rows(e.Row - 1)("ArticleCode") AndAlso row("Quantity") = ArticleGridSource.Rows(e.Row - 1)("Qty")) Select row).FirstOrDefault()
            'Dim dataRow As DataRow = (From row In SelectedComboArticles.Rows Where (row("ComboPartNumber") = ComboPartNumber AndAlso row("DISCRIPTION") = ArticleGridSource.Rows(e.Row - 1)("Description")) Select row).FirstOrDefault()
            If dataRow("IsUpgraded") = True Then
                'UpdateComboCost(dataRow("SELLINGPRICE"), True)
            End If
            ArticleAddEditDeletelog(ArticleGridSource.Rows(e.Row - 1)("ArticleCode") + " " + ArticleGridSource.Rows(e.Row - 1)("Description") + "  " + "DELETED FROM COMBO GRID")
            ArticleGridSource.Rows(e.Row - 1).Delete()
            dataRow.Delete()
            SetTotalItemCount()
            SetTotalPrice()
            'Commented BY gaurav for Generic change
            'DisableUpgradeButton()
            'Commented BY gaurav for Generic change
            VerifyAndEnableOkAndNext()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
#End Region

#Region "Private Methods"


    'Private Sub AddArticleButtonM(ByVal articleCode As String, ByVal articleName As String, Optional ByVal isUpgraded As Boolean = False, Optional imgName As String = "")
    '    Try
    '        Dim bt As New GetDefaultArticleButton(ButtonSize)
    '        bt.SetArticleCode = articleCode
    '        bt.Tag = articleCode
    '        Dim tipimg As ToolTip = New ToolTip()
    '        tipimg.SetToolTip(bt, articleName)
    '        AddHandler bt.Click, AddressOf MstArticleClicked
    '        ShowArticleImageMOD(articleCode, bt, imgName)

    '        'lb.AutoSize = True
    '        Dim lb As New GetDefaultArticleLabel(ButtonSize)
    '        lb.Text = UCase(articleName)
    '        Dim tip As ToolTip = New ToolTip()
    '        tip.SetToolTip(lb, articleName)

    '        Dim pn As New GetDefaultArticlePanel(ButtonSize)
    '        pn.Controls.Add(bt, 0, 0)
    '        pn.Controls.Add(lb, 0, 1)
    '        pn.AutoSize = False

    '        If isUpgraded = False Then
    '            articlePanel.Controls.Add(pn)
    '        Else
    '            upgradePanel.Controls.Add(pn)
    '        End If
    '    Catch ex As Exception
    '        LogException(ex)
    '    End Try
    'End Sub

    Dim btnFont As New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Dim btnLoc As New System.Drawing.Point(3, 3)
    Dim btnPadding As New Padding(0, 3, 0, 0)
    Dim lblFont As New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Dim lb As Label
    Dim pn As TableLayoutPanel
    Dim bt As CtrlBtn
    Private Sub AddArticleButton(ByVal articleCode As String, ByVal articleName As String, Optional ByVal isUpgraded As Boolean = False, Optional imgName As String = "")
        Try
            bt = New Spectrum.CtrlBtn()
            '  bt.SuspendLayout()
            bt.Font = btnFont
            'Me.CtrlBtnGoods.Image = Global.Spectrum.My.Resources.Resources._new
            bt.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            bt.Location = btnLoc
            bt.Name = "CtrlBtnGoods"
            bt.SetArticleCode = articleCode
            bt.Anchor = AnchorStyles.Top
            bt.Size = ButtonSize
            bt.TabIndex = 1
            bt.Tag = articleCode
            bt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            bt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
            bt.UseVisualStyleBackColor = True
            bt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
            bt.Margin = btnPadding
            Dim tipimg As ToolTip = New ToolTip()
            tipimg.SetToolTip(bt, articleName)
            AddHandler bt.Click, AddressOf MstArticleClicked
            ShowArticleImageMOD(articleCode, bt, imgName)

            lb = New Label()
            '  lb.SuspendLayout()
            lb.TextAlign = ContentAlignment.TopLeft
            lb.MaximumSize = New Size(ButtonSize.Width, 0)
            lb.Size = New Size(ButtonSize.Width, 40)
            lb.Margin = New Padding(3, 0, 0, 0)
            'lb.AutoSize = True
            lb.Text = UCase(articleName)
            Dim tip As ToolTip = New ToolTip()
            tip.SetToolTip(lb, articleName)
            lb.Name = "CtrlBtnGoods"
            lb.Anchor = AnchorStyles.Left
            lb.ForeColor = Color.DarkBlue
            lb.Font = lblFont

            pn = New TableLayoutPanel()
            pn.SuspendLayout()
            ' pn.Visible = False
            pn.Margin = New Padding(0)
            pn.Padding = New Padding(0)
            pn.Size = New System.Drawing.Size(ButtonSize.Width + 8, ButtonSize.Height + 40)
            pn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            pn.RowCount = 2
            pn.ColumnCount = 1
            pn.Controls.Add(bt, 0, 0)
            pn.Controls.Add(lb, 0, 1)
            pn.AutoSize = False
            If isUpgraded = False Then
                articlePanel.Controls.Add(pn)
            Else
                upgradePanel.Controls.Add(pn)
            End If
            'bt.ResumeLayout()
            'lb.ResumeLayout()
            '  pn.Visible = True
            pn.ResumeLayout()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub AddArticleButton(ByVal pn As TableLayoutPanel, ByVal articleCode As String, ByVal articleName As String, Optional ByVal isUpgraded As Boolean = False, Optional imgName As String = "")
        Try
            bt = pn.Controls(0)

            'bt = New Spectrum.CtrlBtn()
            '  bt.SuspendLayout()
            'bt.Font = btnFont
            'Me.CtrlBtnGoods.Image = Global.Spectrum.My.Resources.Resources._new
            'bt.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            'bt.Location = btnLoc
            'bt.Name = "CtrlBtnGoods"
            bt.SetArticleCode = articleCode
            'bt.Anchor = AnchorStyles.Top
            'bt.Size = ButtonSize
            'bt.TabIndex = 1
            bt.Tag = articleCode
            'bt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            'bt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
            'bt.UseVisualStyleBackColor = True
            'bt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
            'bt.Margin = btnPadding
            Dim tipimg As ToolTip = New ToolTip()
            tipimg.SetToolTip(bt, articleName)
            '   AddHandler bt.Click, AddressOf MstArticleClicked
            ShowArticleImageMOD(articleCode, bt, imgName)

            lb = pn.Controls(1)
            'lb = New Label()
            'lb.SuspendLayout()
            'lb.TextAlign = ContentAlignment.TopLeft
            'lb.MaximumSize = New Size(ButtonSize.Width, 0)
            'lb.Size = New Size(ButtonSize.Width, 40)
            'lb.Margin = New Padding(3, 0, 0, 0)
            'lb.AutoSize = True
            lb.Text = UCase(articleName)
            Dim tip As ToolTip = New ToolTip()
            tip.SetToolTip(lb, articleName)
            'lb.Name = "CtrlBtnGoods"
            'lb.Anchor = AnchorStyles.Left
            'lb.ForeColor = Color.DarkBlue
            'lb.Font = lblFont

            'pn = New TableLayoutPanel()
            'pn.SuspendLayout()
            '' pn.Visible = False
            'pn.Margin = New Padding(0)
            'pn.Padding = New Padding(0)
            'pn.Size = New System.Drawing.Size(ButtonSize.Width + 8, ButtonSize.Height + 40)
            'pn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            'pn.RowCount = 2
            'pn.ColumnCount = 1
            'pn.Controls.Add(bt, 0, 0)
            'pn.Controls.Add(lb, 0, 1)
            'pn.AutoSize = False
            'If isUpgraded = False Then
            '    articlePanel.Controls.Add(pn)
            'Else
            '    upgradePanel.Controls.Add(pn)
            'End If
            'bt.ResumeLayout()
            'lb.ResumeLayout()
            '  pn.Visible = True
            '    pn.ResumeLayout()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    'Private Sub ShowArticleImageMOD(ByVal StrArticle As String, ByRef btn As Button)
    '    Try
    '        Dim url As String
    '        Dim objComm As New clsCommon
    '        url = objComm.GetArticleImage(StrArticle, clsAdmin.Articleimagepath)
    '        If url <> String.Empty Then
    '            Dim img As Image
    '            Dim ratio As Double
    '            Dim s As Size
    '            btn.Image = Nothing
    '            If btn.Width > 0 And btn.Height > 0 Then
    '                Try
    '                    img = Image.FromFile(url)
    '                    s.Height = btn.Height
    '                    s.Width = btn.Width
    '                    btn.Image = New Bitmap(img, s)
    '                    img.Dispose()
    '                Catch ex As Exception

    '                End Try
    '            End If
    '        Else
    '            btn.Image = Nothing
    '        End If
    '    Catch ex As Exception
    '        LogException(ex)
    '    End Try
    'End Sub

    Dim objComm As New clsCommon
    Private Sub ShowArticleImageMOD(ByVal StrArticle As String, ByRef btn As Button, Optional imgName As String = "")
        Try
            ''Change By Mahesh For Performance
            'url = objComm.GetArticleImage(StrArticle, clsAdmin.Articleimagepath)
            Dim strImagePath = clsAdmin.Articleimagepath.Replace("\\", "\")
            Dim url As String = strImagePath & "\" & imgName

            If Not String.IsNullOrEmpty(url) Then
                Dim img As Image
                'Dim ratio As Double
                ' Dim s As Size
                btn.Image = Nothing
                If btn.Width > 0 And btn.Height > 0 Then
                    Try
                        img = Image.FromFile(url)
                        's.Height = btn.Height
                        's.Width = btn.Width
                        btn.Image = New Bitmap(img, ButtonSize)
                        img.Dispose()
                    Catch ex As Exception

                    End Try
                End If
            Else
                btn.Image = Nothing
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub GridSettings()
        ArticleGridSource = New DataTable
        ArticleGridSource.Columns.Add("Select")
        ArticleGridSource.Columns.Add("ARTICLECODE")
        ArticleGridSource.Columns.Add("Description")
        ArticleGridSource.Columns.Add("Price")
        ArticleGridSource.Columns.Add("Qty")
        '--- Added for IndividualQty
        ArticleGridSource.Columns.Add("IndividualQty")

        CtrlGrid1.DataSource = ArticleGridSource
        CtrlGrid1.Cols("Select").Width = 22
        CtrlGrid1.Cols("Select").Caption = " "
        CtrlGrid1.Cols("Select").ComboList = "..."
        CtrlGrid1.Cols("ARTICLECODE").Visible = False
        CtrlGrid1.Cols("ARTICLECODE").AllowEditing = False
        CtrlGrid1.Cols("Description").AllowEditing = False
        CtrlGrid1.Cols("Price").AllowEditing = False
        CtrlGrid1.Cols("Qty").AllowEditing = False
        CtrlGrid1.Cols("Description").Width = 200
        CtrlGrid1.Cols("Price").Width = 55
        CtrlGrid1.Cols("Qty").Width = 25
        CtrlGrid1.Cols("IndividualQty").Visible = False
        CtrlGrid1.Cols("IndividualQty").AllowEditing = False
    End Sub

    Private Sub InitializeSelectedArticleTable(Optional ByVal IsCalledFromDineIn As Boolean = False)
        SelectedComboArticles.Columns.Add("ARTICLECODE")
        SelectedComboArticles.Columns.Add("EAN")
        SelectedComboArticles.Columns.Add("DISCRIPTION")
        SelectedComboArticles.Columns.Add("SELLINGPRICE")
        SelectedComboArticles.Columns.Add("NETSELLINGPRICE")
        SelectedComboArticles.Columns.Add("Quantity")
        SelectedComboArticles.Columns.Add("IndividualQty")
        SelectedComboArticles.Columns.Add("ComboPartNumber")
        SelectedComboArticles.Columns.Add("BillLineNo")
        SelectedComboArticles.Columns.Add("IsUpgraded")
        SelectedComboArticles.Columns.Add("Discount")
        If IsCalledFromDineIn Then '3387
            SelectedComboArticles.Columns.Add("TableNo")
        End If
    End Sub

    Private Sub SetTotalItemCount()
        Try
            itemCount.Text = (From dataRow In ArticleGridSource.Rows Select dataRow).Sum(Function(art) art("Qty")).ToString()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub SetTotalPrice()
        Try
            amount.Text = (From dataRow In ArticleGridSource.Rows Select dataRow).Sum(Function(art) art("Price") * art("Qty")).ToString()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub SetHeaderLabel(ByVal qty As String, ByVal name As String)
        'If String.IsNullOrEmpty(qty) Then
        '    hdrLbl.Text = "Select " & name
        'Else
        hdrLbl.Text = "Select " & qty & " " & name
        'End If
    End Sub

    Private Sub VerifyIfUpgradable()
        Try
            If (Not IsDBNull(ComboDetails.Rows(ComboPartNumber)("UpdateItem")) AndAlso
                Not (String.IsNullOrEmpty(ComboDetails.Rows(ComboPartNumber)("UpdateItem")))) Or (Not IsDBNull(ComboDetails.Rows(ComboPartNumber)("UpdateHierarchy")) AndAlso
                Not (String.IsNullOrEmpty(ComboDetails.Rows(ComboPartNumber)("UpdateHierarchy")))) Then
                upgradeBtn.Enabled = True
            Else
                upgradeBtn.Enabled = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub DisableUpgradeButton()
        If upgradeBtn.Text = "Upgrade" AndAlso upgradeBtn.Enabled Then
            If UCase(itemCount.Text) = UCase(ComboPartQuantity.ToString()) Then
                upgradeBtn.Enabled = False
            End If
        ElseIf upgradeBtn.Text = "Upgrade" AndAlso upgradeBtn.Enabled = False Then
            If Convert.ToInt32(itemCount.Text) < Convert.ToInt32(ComboPartQuantity.ToString()) Then
                upgradeBtn.Enabled = True
            End If
        End If
    End Sub
    Dim headerLabel As String
    Private Sub PopulateComboArticles()
        Try
            headerLabel = String.Empty
            ComboArticles.Rows.Clear()
            Dim dataRow As DataRow = ComboDetails.Rows(ComboPartNumber)
            ComboPartQuantity = dataRow("Quantity") * ComboQuantity
            IndividualQty = 1
            If Not IsDBNull(dataRow("IndividualQty")) AndAlso Not (String.IsNullOrEmpty(dataRow("IndividualQty"))) Then
                IndividualQty = dataRow("IndividualQty")
            End If

            headerLabel += "Select " & ComboPartQuantity & " "
            CheckAndPopulateHierarchyArticles(dataRow)
            CheckAndPopulateSingleArticle(dataRow)
            'Commented BY gaurav for Generic change
            'If articlePanel.Controls.Count > 0 Then
            '    SetOriginalComboHierarchyCost(dataRow)
            '    OriginalComboItemCost = 0
            'End If
            'If articlePanel.Controls.Count = 0 Then
            '    CheckAndPopulateSingleArticle(dataRow)
            '    OriginalComboItemCost = ComboArticles.Rows(0)("SELLINGPRICE")
            '    OriginalComboHierarchyCost = 0
            'End If
            'Commented BY gaurav for Generic change UpgradeGroupId

            'Added BY gaurav for Generic change
            If (Not IsDBNull(dataRow("UpdateHierarchy")) AndAlso Not (String.IsNullOrEmpty(dataRow("UpdateHierarchy")))) Or
                (Not IsDBNull(dataRow("UpdateItem")) AndAlso Not (String.IsNullOrEmpty(dataRow("UpdateItem")))) Or
                (Not IsDBNull(dataRow("UpgradeGroupId")) AndAlso Not (String.IsNullOrEmpty(dataRow("UpgradeGroupId")))) Then
                If IsDBNull(dataRow("Hierarchy")) AndAlso IsDBNull(dataRow("GroupIDs")) Then
                    Dim itemArray = DirectCast(dataRow("Item"), String).Split(",")
                    If itemArray.Count = 1 Then
                        isUpgraded = True
                        IncludeUpgradeArticles()
                    End If
                End If
            End If
            'Added BY gaurav for Generic change
            ComboPartNumber = ComboPartNumber + 1
            headerLabel = headerLabel.Trim()
            If (headerLabel.Trim().ToString().IndexOf(",") > -1) Then
                headerLabel = headerLabel.Substring(0, headerLabel.Length - 1)
            End If
            ' headerLabel = headerLabel.Remove(headerLabel.Length - 2, 2)
            hdrLbl.Text = headerLabel
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub DisplayUpgradePanel()
        upgradePanel.Visible = True
        TableLayoutPanel1.SetRowSpan(articlePanel, 1)
        TableLayoutPanel1.SetRowSpan(upgradePanel, 3)
        Label1.Visible = True
        Label2.Visible = True
    End Sub

    Private Sub HideUpgradePanel()
        upgradePanel.Visible = False
        TableLayoutPanel1.SetRowSpan(articlePanel, 5)
        Label1.Visible = False
        Label2.Visible = False
        'TableLayoutPanel1.SetRowSpan(upgradePanel, 3)
    End Sub

    Private Sub IncludeUpgradeArticles()
        Try
            upgradePanel.Controls.Clear()
            Dim dataRow As DataRow = ComboDetails.Rows(ComboPartNumber)
            If Not IsDBNull(dataRow("UpdateHierarchy")) AndAlso Not (String.IsNullOrEmpty(dataRow("UpdateHierarchy"))) Then
                Dim hierarchyArticles As DataTable = clsArticleCombo.GetArticlesOfANode(dataRow("UpdateHierarchy"), clsAdmin.SiteCode)
                Dim isDataPresent As Boolean = False
                For Each drButton In hierarchyArticles.Rows
                    'If Not isDataPresent Then
                    '    ComboArticles.Rows.Add(drButton("ARTICLECODE"), drButton("EAN"), drButton("DISCRIPTION"), drButton("SELLINGPRICE"), drButton("NODENAME"))
                    '    DisplayUpgradePanel()
                    '    AddArticleButton(drButton("ARTICLECODE"), drButton("DISCRIPTION"), True)
                    'Else
                    '    If CheckIfArticleExist(drButton("ARTICLECODE")) = False Then
                    '        ComboArticles.Rows.Add(drButton("ARTICLECODE"), drButton("EAN"), drButton("DISCRIPTION"), drButton("SELLINGPRICE"), drButton("NODENAME"))
                    '        DisplayUpgradePanel()
                    '        AddArticleButton(drButton("ARTICLECODE"), drButton("DISCRIPTION"), True)
                    '    End If
                    'End If
                    If CheckIfArticleExist(drButton("ARTICLECODE")) = False Then
                        ComboArticles.Rows.Add(drButton("ARTICLECODE"), drButton("EAN"), drButton("DISCRIPTION"), drButton("SELLINGPRICE"), drButton("NODENAME"), IndividualQty)
                        AddArticleButton(drButton("ARTICLECODE"), drButton("DISCRIPTION"), True, drButton("ArticleImage"))
                    End If
                Next
                headerLabel += ComboArticles.Rows(ComboArticles.Rows.Count - 1)("NODENAME") & ", "
            End If
            If Not IsDBNull(dataRow("UpdateItem")) AndAlso Not (String.IsNullOrEmpty(dataRow("UpdateItem"))) Then
                Dim objCM As New clsCashMemo
                Dim articles As DataTable = objCM.GetItemDetails(clsAdmin.SiteCode, dataRow("UpdateItem"), False, clsAdmin.LangCode)
                Dim isDataPresent As Boolean = False
                For Each drButton In articles.Rows
                    'If Not isDataPresent Then
                    '    ComboArticles.Rows.Add(drButton("ARTICLECODE"), drButton("EAN"), drButton("DISCRIPTION"), drButton("SELLINGPRICE"), String.Empty)
                    '    DisplayUpgradePanel()
                    '    AddArticleButton(drButton("ARTICLECODE"), drButton("DISCRIPTION"), True)
                    '    headerLabel += drButton("DISCRIPTION") & ", "
                    'Else
                    '    If CheckIfArticleExist(drButton("ARTICLECODE")) = False Then
                    '        ComboArticles.Rows.Add(drButton("ARTICLECODE"), drButton("EAN"), drButton("DISCRIPTION"), drButton("SELLINGPRICE"), String.Empty)
                    '        DisplayUpgradePanel()
                    '        AddArticleButton(drButton("ARTICLECODE"), drButton("DISCRIPTION"), True)
                    '        headerLabel += drButton("DISCRIPTION") & ", "
                    '    End If
                    'End If
                    If CheckIfArticleExist(drButton("ARTICLECODE")) = False Then
                        ComboArticles.Rows.Add(drButton("ARTICLECODE"), drButton("EAN"), drButton("DISCRIPTION"), drButton("SELLINGPRICE"), String.Empty, IndividualQty)
                        'ComboArticles.Rows.Add(drButton("ARTICLECODE"), drButton("EAN"), drButton("DISCRIPTION"), drButton("SELLINGPRICE"), String.Empty, IndividualQty)
                        'DisplayUpgradePanel()
                        AddArticleButton(drButton("ARTICLECODE"), drButton("DISCRIPTION"), True, drButton("ArticleImage"))
                        headerLabel += drButton("DISCRIPTION") & ", "
                    End If
                Next
            End If
            If Not IsDBNull(dataRow("UpgradeGroupId")) AndAlso Not (String.IsNullOrEmpty(dataRow("UpgradeGroupId"))) Then
                Dim groupArticles As DataTable = clsArticleCombo.GetArticlesOfAGroup(dataRow("UpgradeGroupId"), clsAdmin.SiteCode)

                Dim dvArticle As DataView
                If (clsDefaultConfiguration.DisplayArticleAlphabetChar) Then
                    dvArticle = New DataView(groupArticles, String.Empty, "ArticleName Asc", DataViewRowState.CurrentRows)
                Else
                    dvArticle = New DataView(groupArticles, String.Empty, String.Empty, DataViewRowState.CurrentRows)
                End If

                For Each drButton In dvArticle.ToTable().Rows
                    If CheckIfArticleExist(drButton("ARTICLECODE")) = False Then
                        ComboArticles.Rows.Add(drButton("ARTICLECODE"), drButton("EAN"), drButton("DISCRIPTION"), drButton("SELLINGPRICE"), drButton("NODENAME"), IndividualQty)
                        ' ComboArticles.Rows.Add(drButton("ARTICLECODE"), drButton("EAN"), drButton("DISCRIPTION"), drButton("SELLINGPRICE"), drButton("NODENAME"), IndividualQty)
                        AddArticleButton(drButton("ARTICLECODE"), drButton("DISCRIPTION"), True, drButton("ArticleImage"))
                    End If
                Next
                headerLabel += ComboArticles.Rows(ComboArticles.Rows.Count - 1)("NODENAME") & ", "
            End If
            DisplayUpgradePanel()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function CheckIfArticleExist(ByVal articleCode As String) As Boolean
        Try
            Dim comboArticle As DataRow = (From article In ComboArticles.Rows Where article("ARTICLECODE") = articleCode Select article).FirstOrDefault()
            If Not comboArticle Is Nothing Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Sub InitializeComboArticleTable()
        ComboArticles.Columns.Add("ARTICLECODE")
        ComboArticles.Columns.Add("EAN")
        ComboArticles.Columns.Add("DISCRIPTION")
        ComboArticles.Columns.Add("SELLINGPRICE")
        ComboArticles.Columns.Add("NODENAME")
        ComboArticles.Columns.Add("IndividualQty")

    End Sub

    Private Sub CheckAndPopulateHierarchyArticles(ByRef dataRow As DataRow)
        Try
            If Not IsDBNull(dataRow("Hierarchy")) AndAlso Not (String.IsNullOrEmpty(dataRow("Hierarchy"))) Then
                'ComboArticles.Merge(clsArticleCombo.GetArticlesOfANode(dataRow("Hierarchy"), clsAdmin.SiteCode))
                Dim hierarchyArticles As DataTable = clsArticleCombo.GetArticlesOfANode(dataRow("Hierarchy"), clsAdmin.SiteCode)
                '---Commented And Changed By Mahesh For Performance Improvement
                'For Each drButton In hierarchyArticles.Rows
                '    'ComboArticles.Rows.Add(drButton("ARTICLECODE"), drButton("EAN"), drButton("DISCRIPTION"), drButton("SELLINGPRICE"), drButton("NODENAME"))
                '    ComboArticles.Rows.Add(drButton("ARTICLECODE"), drButton("EAN"), drButton("DISCRIPTION"), drButton("SELLINGPRICE"), drButton("NODENAME"), IndividualQty)
                '    AddArticleButton(drButton("ARTICLECODE"), drButton("DISCRIPTION"), , drButton("ArticleImage"))
                'Next
                Dim xNoOfArticles As Int16 = 0
                For index = 0 To hierarchyArticles.Rows.Count - 1
                    xNoOfArticles += 1
                    ComboArticles.Rows.Add(hierarchyArticles.Rows(index)("ARTICLECODE"), hierarchyArticles.Rows(index)("EAN"), hierarchyArticles.Rows(index)("DISCRIPTION"), hierarchyArticles.Rows(index)("SELLINGPRICE"), hierarchyArticles.Rows(index)("NODENAME"), IndividualQty)
                    If xNoOfArticles - 1 < articlePanel.Controls.Count Then
                        Dim pn = DirectCast(articlePanel.Controls(xNoOfArticles - 1), TableLayoutPanel)
                        If pn IsNot Nothing Then
                            AddArticleButton(pn, hierarchyArticles.Rows(index)("ARTICLECODE"), hierarchyArticles.Rows(index)("DISCRIPTION"), , hierarchyArticles.Rows(index)("ArticleImage"))
                            If Not pn.Visible Then pn.Visible = True
                        Else
                            AddArticleButton(hierarchyArticles.Rows(index)("ARTICLECODE"), hierarchyArticles.Rows(index)("DISCRIPTION"), , hierarchyArticles.Rows(index)("ArticleImage"))
                        End If
                    Else
                        AddArticleButton(hierarchyArticles.Rows(index)("ARTICLECODE"), hierarchyArticles.Rows(index)("DISCRIPTION"), , hierarchyArticles.Rows(index)("ArticleImage"))
                    End If
                Next
                ''---- Hide extra controls 
                xNoOfArticles = hierarchyArticles.Rows.Count
                Dim xNoOfControls = articlePanel.Controls.Count

                If xNoOfControls > xNoOfArticles Then
                    For index = xNoOfArticles + 1 To xNoOfControls
                        Dim pn = DirectCast(articlePanel.Controls(index), TableLayoutPanel)
                        pn.Visible = False
                    Next
                End If
                'Commented BY gaurav for Generic change
                'SetHeaderLabel(ComboPartQuantity.ToString(), ComboArticles.Rows(0)("NODENAME"))
                'Commented BY gaurav for Generic change
                headerLabel += ComboArticles.Rows(ComboArticles.Rows.Count - 1)("NODENAME") & ", "
            Else
                ''---- Hide extra controls 
                Dim xNoOfArticles = 0
                Dim xNoOfControls = articlePanel.Controls.Count
                If xNoOfControls > xNoOfArticles Then
                    For index = xNoOfArticles To xNoOfControls - 1 Step 1
                        Dim pn = DirectCast(articlePanel.Controls(index), TableLayoutPanel)
                        pn.Visible = False
                    Next
                End If
            End If

            If Not IsDBNull(dataRow("GroupIDs")) AndAlso Not (String.IsNullOrEmpty(dataRow("GroupIDs"))) Then
                'ComboArticles.Merge(clsArticleCombo.GetArticlesOfAGroup(dataRow("GroupIDs"), clsAdmin.SiteCode))
                Dim groupArticles As DataTable = clsArticleCombo.GetArticlesOfAGroup(dataRow("GroupIDs"), clsAdmin.SiteCode)
                Dim isDataPresent As Boolean = IIf(ComboArticles.Rows.Count > 0, True, False)

                Dim dvArticle As DataView
                If (clsDefaultConfiguration.DisplayArticleAlphabetChar) Then
                    dvArticle = New DataView(groupArticles, String.Empty, "ArticleName Asc", DataViewRowState.CurrentRows)
                Else
                    dvArticle = New DataView(groupArticles, String.Empty, String.Empty, DataViewRowState.CurrentRows)
                End If

                For Each drButton In dvArticle.ToTable().Rows
                    If Not isDataPresent Then
                        'ComboArticles.Rows.Add(drButton("ARTICLECODE"), drButton("EAN"), drButton("DISCRIPTION"), drButton("SELLINGPRICE"), drButton("NODENAME"))
                        ComboArticles.Rows.Add(drButton("ARTICLECODE"), drButton("EAN"), drButton("DISCRIPTION"), drButton("SELLINGPRICE"), drButton("NODENAME"), IndividualQty)
                        AddArticleButton(drButton("ARTICLECODE"), drButton("DISCRIPTION"), , drButton("ArticleImage"))
                    Else
                        If CheckIfArticleExist(drButton("ARTICLECODE")) = False Then
                            'ComboArticles.Rows.Add(drButton("ARTICLECODE"), drButton("EAN"), drButton("DISCRIPTION"), drButton("SELLINGPRICE"), drButton("NODENAME"))
                            ComboArticles.Rows.Add(drButton("ARTICLECODE"), drButton("EAN"), drButton("DISCRIPTION"), drButton("SELLINGPRICE"), drButton("NODENAME"), IndividualQty)
                            AddArticleButton(drButton("ARTICLECODE"), drButton("DISCRIPTION"), , drButton("ArticleImage"))
                        End If
                    End If
                Next
                headerLabel += ComboArticles.Rows(ComboArticles.Rows.Count - 1)("NODENAME") & ", "
                'Commented BY gaurav for Generic change
                'SetHeaderLabel(ComboPartQuantity.ToString(), ComboArticles.Rows(0)("NODENAME"))
                'Commented BY gaurav for Generic change
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub CheckAndPopulateSingleArticle(ByRef dataRow As DataRow)
        Try
            'ComboArticles.Rows.Clear()
            If Not IsDBNull(dataRow("Item")) AndAlso Not (String.IsNullOrEmpty(dataRow("Item"))) Then
                Dim objCM As New clsCashMemo
                Dim itemArray = DirectCast(dataRow("Item"), String).Split(",")

                Dim articleNames As String = String.Empty
                articleNames = String.Format("'{0}'", String.Join("','", itemArray))
                Dim objArticle As New clsIteamSearch()
                Dim dtArticles = objArticle.GetArticleNames(articleNames)

                Dim isDataPresent As Boolean = IIf(ComboArticles.Rows.Count > 0, True, False)
                ' For Each drArticle As DataRow In dtArticles.Rows
                For index = 0 To dtArticles.Rows.Count - 1
                    Dim article As DataTable = objCM.GetItemDetails(clsAdmin.SiteCode, dtArticles.Rows(index)("ArticleCode"), False, clsAdmin.LangCode)
                    'ComboArticles.Merge(objCM.GetItemDetails(clsAdmin.SiteCode, item, False, clsAdmin.LangCode))
                    If article.Rows.Count > 0 Then
                        If Not isDataPresent Then
                            'ComboArticles.Rows.Add(article.Rows(0)("ARTICLECODE"), article.Rows(0)("EAN"), article.Rows(0)("DISCRIPTION"), article.Rows(0)("SELLINGPRICE"), String.Empty)
                            ComboArticles.Rows.Add(article.Rows(0)("ARTICLECODE"), article.Rows(0)("EAN"), article.Rows(0)("DISCRIPTION"), article.Rows(0)("SELLINGPRICE"), String.Empty, IndividualQty)
                            AddArticleButton(article.Rows(0)("ARTICLECODE"), article.Rows(0)("DISCRIPTION"), , article.Rows(0)("ArticleImage"))
                            'headerLabel += article.Rows(0)("DISCRIPTION") & ", "
                            'Added BY gaurav for Generic change
                            OriginalComboItemCost = article.Rows(0)("SELLINGPRICE")
                            'Added BY gaurav for Generic change
                        Else
                            If CheckIfArticleExist(article.Rows(0)("ARTICLECODE")) = False Then
                                'ComboArticles.Rows.Add(article.Rows(0)("ARTICLECODE"), article.Rows(0)("EAN"), article.Rows(0)("DISCRIPTION"), article.Rows(0)("SELLINGPRICE"), String.Empty)
                                ComboArticles.Rows.Add(article.Rows(0)("ARTICLECODE"), article.Rows(0)("EAN"), article.Rows(0)("DISCRIPTION"), article.Rows(0)("SELLINGPRICE"), String.Empty, IndividualQty)
                                AddArticleButton(article.Rows(0)("ARTICLECODE"), article.Rows(0)("DISCRIPTION"), , article.Rows(0)("ArticleImage"))
                                'headerLabel += article.Rows(0)("DISCRIPTION") & ", "
                                'Added BY gaurav for Generic change
                                OriginalComboItemCost = article.Rows(0)("SELLINGPRICE")
                                'Added BY gaurav for Generic change
                            End If
                        End If
                    End If
                Next
                'For Each drButton In ComboArticles.Rows
                '    AddArticleButton(drButton("ARTICLECODE"), drButton("DISCRIPTION"))
                'Next

                'Commented BY gaurav for Generic change
                'If ComboArticles.Rows.Count > 1 Then
                '    SetHeaderLabel(ComboPartQuantity.ToString(), ComboArticles.Rows(0)("DISCRIPTION") & ", " & ComboArticles.Rows(1)("DISCRIPTION"))
                'Else
                '    SetHeaderLabel(ComboPartQuantity.ToString(), ComboArticles.Rows(0)("DISCRIPTION"))
                'End If
                'End COmment

            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub UpgradeArticleHierarchy()
        Try
            Dim dataRow As DataRow = ComboDetails.Rows(ComboPartNumber - 1)
            If Not isUpgraded Then
                If Not IsDBNull(dataRow("UpdateHierarchy")) AndAlso Not (String.IsNullOrEmpty(dataRow("UpdateHierarchy"))) Then
                    ComboArticles = clsArticleCombo.GetArticlesOfANode(dataRow("UpdateHierarchy"), clsAdmin.SiteCode)
                    For Each drButton In ComboArticles.Rows
                        AddArticleButton(drButton("ARTICLECODE"), drButton("DISCRIPTION"), , drButton("ArticleImage"))
                    Next
                    SetHeaderLabel(ComboPartQuantity.ToString(), ComboArticles.Rows(0)("NODENAME"))
                End If

                If Not IsDBNull(dataRow("UpdateItem")) AndAlso Not (String.IsNullOrEmpty(dataRow("UpdateItem"))) Then
                    Dim objCM As New clsCashMemo
                    ComboArticles = objCM.GetItemDetails(clsAdmin.SiteCode, dataRow("UpdateItem"), False, clsAdmin.LangCode)
                    For Each drButton In ComboArticles.Rows
                        AddArticleButton(drButton("ARTICLECODE"), drButton("DISCRIPTION"), , drButton("ArticleImage"))
                    Next
                    SetHeaderLabel(ComboPartQuantity.ToString(), ComboArticles.Rows(0)("DISCRIPTION"))
                End If
            Else
                CheckAndPopulateHierarchyArticles(dataRow)
                If articlePanel.Controls.Count = 0 Then
                    CheckAndPopulateSingleArticle(dataRow)
                End If
            End If
            'ComboPartNumber = ComboPartNumber + 1
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub SetUpgradeFlag()
        If isUpgraded Then
            isUpgraded = False
            upgradeBtn.Text = "Upgrade"
            DisableUpgradeButton()
        Else
            isUpgraded = True
            upgradeBtn.Text = "Original"
        End If
    End Sub

    Private Sub UpdateComboCost(ByVal upgradedArticlePrice As Double, Optional ByVal isDeleted As Boolean = False)
        Try
            Dim difference As Double
            If OriginalComboHierarchyCost > 0 Then
                difference = upgradedArticlePrice - OriginalComboHierarchyCost
                If difference > 0 Then
                    If isDeleted = False Then
                        AdditionalComboCost += difference
                    Else
                        AdditionalComboCost -= difference
                    End If

                End If
            Else
                difference = upgradedArticlePrice - OriginalComboItemCost
                If difference > 0 Then
                    If isDeleted = False Then
                        AdditionalComboCost += difference
                    Else
                        AdditionalComboCost -= difference
                    End If
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub SetOriginalComboHierarchyCost(ByRef dataRow As DataRow)
        OriginalComboHierarchyCost = dataRow("Cost") / (1 - (dataRow("Discount") / 100))
    End Sub

    Private Sub VerifyAndEnableOkAndNext()
        Try
            If Convert.ToInt32(itemCount.Text) = ComboPartQuantity AndAlso ComboPartNumber < ComboDetails.Rows.Count Then
                nextBtn.Enabled = True
            Else
                nextBtn.Enabled = False
            End If
            If Convert.ToInt32(itemCount.Text) = ComboPartQuantity AndAlso ComboPartNumber = ComboDetails.Rows.Count Then
                okBtn.Enabled = True
            Else
                okBtn.Enabled = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub CalculateLineDisount()
        Try
            For i As Integer = 0 To ComboDetails.Rows.Count - 1 Step +1
                Dim totalSellingProce As Double = 0
                Dim addtionalCost As Double = 0
                'Dim totalSellingProce As Double = SelectedComboArticles.Select("ComboPartNumber = '" & (i + 1) & "'").Sum(Function(art) art("SELLINGPRICE")).ToString()
                Dim Row() = SelectedComboArticles.Select("ComboPartNumber = '" & (i + 1) & "'")
                'For Each row As DataRow In SelectedComboArticles.Select("ComboPartNumber = '" & (i + 1) & "'")
                For index = 0 To Row.Length - 1
                    If Row(index)("IsUpgraded") = True Then
                        Dim sellingPrice As Double = clsArticleCombo.GetBaseArticlePrice(clsAdmin.SiteCode, ComboDetails.Rows(Row(index)("ComboPartNumber") - 1)("Item"))
                        If sellingPrice < Row(index)("SELLINGPRICE") Then
                            totalSellingProce += sellingPrice * Row(index)("Quantity")
                        Else
                            totalSellingProce += Row(index)("SELLINGPRICE") * Row(index)("Quantity")
                        End If
                    Else
                        totalSellingProce += Row(index)("SELLINGPRICE") * Row(index)("Quantity")
                    End If
                Next
                Dim stepCost As Double = ComboDetails.Rows(i)("Cost") * ComboQuantity
                Dim totalDiscount As Double = 0
                If totalSellingProce - stepCost > 0 Then
                    totalDiscount = totalSellingProce - stepCost
                End If
                Dim dr() = SelectedComboArticles.Select("ComboPartNumber = '" & (i + 1) & "'")
                '  For Each row As DataRow In SelectedComboArticles.Select("ComboPartNumber = '" & (i + 1) & "'")
                For index = 0 To dr.Length - 1
                    If dr(index)("IsUpgraded") = True Then
                        Dim sellingPrice As Double = clsArticleCombo.GetBaseArticlePrice(clsAdmin.SiteCode, ComboDetails.Rows(dr(index)("ComboPartNumber") - 1)("Item"))
                        If sellingPrice < dr(index)("SELLINGPRICE") Then
                            dr(index)("Discount") = Math.Round((sellingPrice / totalSellingProce) * totalDiscount, 2)
                        Else
                            dr(index)("Discount") = Math.Round((dr(index)("SELLINGPRICE") / totalSellingProce) * totalDiscount, 2)
                        End If
                    Else
                        dr(index)("Discount") = Math.Round((dr(index)("SELLINGPRICE") / totalSellingProce) * totalDiscount, 2)
                    End If
                Next
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub CalculateComboAdditionalCost()
        Try
            For Each Row As DataRow In SelectedComboArticles.Rows
                If Row("IsUpgraded") = True Then
                    Dim sellingPrice As Double = clsArticleCombo.GetBaseArticlePrice(clsAdmin.SiteCode, ComboDetails.Rows(Row("ComboPartNumber") - 1)("Item"))
                    If Row("SellingPrice") - sellingPrice > 0 Then
                        AdditionalComboCost += Row("SellingPrice") - sellingPrice
                    End If
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Public Methods"

#End Region
    Private Sub ArticleAddEditDeletelog(ByVal mes As String)
        Dim ax As New ApplicationException(mes)
        LogException(ax)
    End Sub

    Private Sub ArticleComboPopup_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If (e.Alt = True AndAlso e.KeyCode = Keys.F4) Then
            ArticleAddEditDeletelog("ALT+F4 BUTTON PRESS")
            CancelBtn_Click(Nothing, Nothing, False)
        End If
        If (e.KeyCode = Keys.Escape) Then
            ArticleAddEditDeletelog("ESCAPE BUTTON PRESS")
            CancelBtn_Click(Nothing, Nothing, False)
        End If
    End Sub
End Class


Public Class GetDefaultArticleButton
    Inherits Spectrum.CtrlBtn

    Public Sub New(ButtonSize As System.Drawing.Size)
        Me.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Me.CtrlMenGoods.Image = Global.Spectrum.My.Resources.Resources._new
        Me.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Location = New System.Drawing.Point(3, 3)
        Me.Name = "CtrlMenGoods"
        Me.Anchor = AnchorStyles.Top
        Me.Size = ButtonSize
        Me.TabIndex = 1
        Me.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.UseVisualStyleBackColor = True
        Me.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.Margin = New Padding(0, 3, 0, 0)
    End Sub

End Class

Public Class GetDefaultArticleLabel
    Inherits Label
    Public Sub New(ButtonSize As System.Drawing.Size)
        Me.TextAlign = ContentAlignment.TopLeft
        Me.MaximumSize = New Size(ButtonSize.Width, 0)
        Me.Size = New Size(ButtonSize.Width, 40)
        Me.Margin = New Padding(3, 0, 0, 0)
        Me.Name = "CtrMetnGoods"
        Me.Anchor = AnchorStyles.Left
        Me.ForeColor = Color.DarkBlue
        Me.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    End Sub
End Class

Public Class GetDefaultArticlePanel
    Inherits TableLayoutPanel
    Public Sub New(ButtonSize As System.Drawing.Size)
        Me.Margin = New Padding(0)
        Me.Padding = New Padding(0)
        Me.Size = New System.Drawing.Size(ButtonSize.Width + 8, ButtonSize.Height + 40)
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.RowCount = 2
        Me.ColumnCount = 1

    End Sub
End Class





