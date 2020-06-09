Imports System.Xml.Serialization
Imports System.Text
Imports System.Data.SqlClient
Imports SpectrumBL
Imports C1.Win.C1FlexGrid
Public Class FrmDefineCombo
    Private _dtCreditSales As DataTable
    Dim Objcm As New clsCommon
    'Dim Objcom As cls
    Public Property dtCreditSales() As DataTable
        Get
            Return _dtCreditSales
        End Get
        Set(ByVal value As DataTable)
            _dtCreditSales = value
        End Set
    End Property
    Private _PopupType As String
    Public Property PopupType() As String
        Get
            Return _PopupType
        End Get
        Set(ByVal value As String)
            _PopupType = value
        End Set
    End Property
    Dim objArticle As New clsArticleCombo
    Dim dtCustmData As New DataTable
    Dim dtArticle As New DataTable
    Dim objItem As New clsIteamSearch
    Dim dtGuest As New DataTable
    Dim DtUpGradableItem As New DataTable
    Dim StrFilter As String
    Dim dtTree As DataTable
    Dim dt As DataTable
    Dim opendate As Date
    Dim _IsNetSale As Boolean = False
    '  Dim DsCombo As New DataSet
    Dim DtArticleCOmbo As DataTable
    Dim articleCode As String = ""
    Dim UpGradableArticlecode As String = ""
    Dim ComboIncNo As String
    Dim IsAlertRequiredOnClosingForm As Boolean = False
    Dim PlaceHolderString As String = "Please select the Combo"


#Region "Events"
    Private Sub frmProductNotificationPopups_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            '    ComboIncNo = objItem.getDocumentNo("CB", clsAdmin.SiteCode, "BK_DOC")

            Dim condition As String

            Dim dtBind As DataTable = Objcm.GetComboArticle(clsAdmin.SiteCode)
            If Not dtBind Is Nothing AndAlso dtBind.Rows.Count > 0 Then
                Call SetWildSearchTextBox(dtBind, txtFilterArticle, key:="ArticleCode", Value:="Discription", searchData:="ArticleCodeDesc")
            End If


            Dim dtGroupID As DataTable = Objcm.GetGroupDtl(clsAdmin.SiteCode)
            If dtGroupID.Rows.Count > 0 Then
                Call SetWildSearchTextBox(dtGroupID, TxtGroupId, key:="Groupid", Value:="GROUPNAME", searchData:="GroupIdName")
            End If

            dtGroupID.Clear()
            dtGroupID = Objcm.GetGroupDtl(clsAdmin.SiteCode)
            If dtGroupID.Rows.Count > 0 Then
                Call SetWildSearchTextBox(dtGroupID, TxtUpgradableGroup, key:="Groupid", Value:="GROUPNAME", searchData:="GroupIdName")
            End If

            condition = " AND A.ArticleCode <>'" & clsDefaultConfiguration.GVBaseArticle & "' AND A.ArticleCode <>'" & clsDefaultConfiguration.ClpBaseArticle & "' AND A.ArticleCode <>'" & clsAdmin.CVBaseArticle & "' AND  A.ArticalTypeCode in ('Kit','Single')"
            Dim GdtAllArticle = objItem.GetEANData(clsAdmin.SiteCode, "", clsAdmin.LangCode, condition, dtItemScanData)
            '  Dim GrdUpgrdbleItem As New DataTable
            'GrdUpgrdbleItem = GdtAllArticle
            If GdtAllArticle.Rows.Count > 0 Then
                Call SetWildSearchTextBox(GdtAllArticle, TxtInternalComboArticle, key:="ArticleCode", Value:="Discription", searchData:="ArticleCodeDesc")
            End If
            '   GdtAllArticle.Clear()
            '   GdtAllArticle = objItem.GetEANData(clsAdmin.SiteCode, "", clsAdmin.LangCode, condition, dtItemScanData)
            If GdtAllArticle.Rows.Count > 0 Then
                'Dim view As New DataView(dtBind, "", "", DataViewRowState.CurrentRows)
                'TxtupGradableItem.DtSearchData = view.ToTable
                Call SetWildSearchTextBox(GdtAllArticle, TxtupGradableItem, key:="Key", Value:="Value", searchData:="SearchData")
            End If


            dtGuest = objArticle.GetDetailsForGuest()
            dtGuest.Clear()

            DtUpGradableItem = objArticle.GetDetailsForGuest()
            DtUpGradableItem.Clear()

            GrpAddStep.Visible = False
            BlankItemDetailSection()
            DtArticleCOmbo = objItem.GetMstArticleComboStruct()

            'DtArticleCOmbo.TableName = "MstArticleCombo"
            'DsCombo.Tables.Add(DtArticleCOmbo)
            txtFilterArticle.IsCallFromPosTab = True
            TxtInternalComboArticle.IsCallFromPosTab = True
            TxtGroupId.IsCallFromPosTab = True
            TxtupGradableItem.IsCallFromPosTab = True
            TxtUpgradableGroup.IsCallFromPosTab = True

            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            'code added by irfan on 26/04/2018 for maintis issue
            TxtGroupId.IsSetLocation = True
            TxtGroupId.ListBoxXCoordinate = 351
            TxtGroupId.ListBoxYCoordinate = 324
            TxtInternalComboArticle.IsSetLocation = True
            TxtInternalComboArticle.ListBoxXCoordinate = 351
            TxtInternalComboArticle.ListBoxYCoordinate = 300
            txtFilterArticle.IsSetLocation = True
            txtFilterArticle.ListBoxXCoordinate = 333
            txtFilterArticle.ListBoxYCoordinate = 84
            TxtupGradableItem.IsSetLocation = True
            TxtupGradableItem.ListBoxXCoordinate = 350
            TxtupGradableItem.ListBoxYCoordinate = 480
            TxtUpgradableGroup.IsSetLocation = True
            TxtUpgradableGroup.ListBoxXCoordinate = 350
            TxtUpgradableGroup.ListBoxYCoordinate = 502
            TxtLastNodeCode.ReadOnly = True
            TxtParentItemCode.ReadOnly = True
            TxtItemTree.ReadOnly = True
            TxtHierarchy.ReadOnly = True
            TxtUpgradableHierarchy.ReadOnly = True
            Me.ControlBox = False
            BtnSaveCombo.Visible = False
            IsAlertRequiredOnClosingForm = False

            AddHandler TxtQty.Click, AddressOf OnScreenKeyboard
            AddHandler TxtindividualQty.Click, AddressOf OnScreenKeyboard
            AddHandler TxtCost.Click, AddressOf OnScreenKeyboard
            AddHandler txtFilterArticle.Click, AddressOf TxtClick
            AddHandler txtFilterArticle.Leave, AddressOf TxtLeave
            txtFilterArticle.Text = PlaceHolderString
            txtFilterArticle.Focus()
            'Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
            'Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height
            'If screenWidth = 1024 AndAlso screenHeight = 768 Then
            '    Me.Size = New System.Drawing.Size((My.Computer.Screen.WorkingArea.Width - 10), (My.Computer.Screen.WorkingArea.Height - 10))
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Function Themechange() As String
        Me.BackColor = Color.FromArgb(76, 76, 76)

        Me.BackColor = Color.FromArgb(76, 76, 76)
        BtnAddStep.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnAddStep.BackColor = Color.Transparent
        BtnAddStep.BackColor = Color.FromArgb(0, 107, 163)
        BtnAddStep.ForeColor = Color.FromArgb(255, 255, 255)
        '  BtnAddStepck.Size = New Size(71, 30)
        BtnAddStep.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        BtnAddStep.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnAddStep.FlatStyle = FlatStyle.Flat
        BtnAddStep.FlatAppearance.BorderSize = 0
        BtnAddStep.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        BtnSaveStep.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnSaveStep.BackColor = Color.Transparent
        BtnSaveStep.BackColor = Color.FromArgb(0, 107, 163)
        BtnSaveStep.ForeColor = Color.FromArgb(255, 255, 255)
        BtnSaveStep.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        BtnSaveStep.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnSaveStep.FlatStyle = FlatStyle.Flat
        BtnSaveStep.FlatAppearance.BorderSize = 0
        BtnSaveStep.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)


        BtnActivateDeactivate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnActivateDeactivate.BackColor = Color.Transparent
        BtnActivateDeactivate.BackColor = Color.FromArgb(0, 107, 163)
        BtnActivateDeactivate.ForeColor = Color.FromArgb(255, 255, 255)
        BtnActivateDeactivate.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        BtnActivateDeactivate.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnActivateDeactivate.FlatStyle = FlatStyle.Flat
        BtnActivateDeactivate.FlatAppearance.BorderSize = 0
        BtnActivateDeactivate.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        BtnSaveCombo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnSaveCombo.BackColor = Color.Transparent
        BtnSaveCombo.BackColor = Color.FromArgb(0, 107, 163)
        BtnSaveCombo.ForeColor = Color.FromArgb(255, 255, 255)
        BtnSaveCombo.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        BtnSaveCombo.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnSaveCombo.FlatStyle = FlatStyle.Flat
        BtnSaveCombo.FlatAppearance.BorderSize = 0
        BtnSaveCombo.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        BtnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnCancel.BackColor = Color.Transparent
        BtnCancel.BackColor = Color.FromArgb(0, 107, 163)
        BtnCancel.ForeColor = Color.FromArgb(255, 255, 255)
        BtnCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        BtnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnCancel.FlatStyle = FlatStyle.Flat
        BtnCancel.FlatAppearance.BorderSize = 0
        BtnCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)





        btnClose.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnClose.BackColor = Color.Transparent
        btnClose.BackColor = Color.FromArgb(0, 107, 163)
        btnClose.ForeColor = Color.FromArgb(255, 255, 255)
        btnClose.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnClose.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.FlatAppearance.BorderSize = 0
        btnClose.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)



        txtItemCode.ForeColor = Color.White
        TxtItemSearchName.ForeColor = Color.White
        TxtmaterialType.ForeColor = Color.White
        TxtSalePRice.ForeColor = Color.White
        txtItemType.ForeColor = Color.White
        TxtStatus.ForeColor = Color.White
        Label1.ForeColor = Color.White
        lblTotCost.ForeColor = Color.White
        LblTotQty.ForeColor = Color.White

        Label2.ForeColor = Color.White
        Label3.ForeColor = Color.White
        Label4.ForeColor = Color.White
        Label5.ForeColor = Color.White
        Label6.ForeColor = Color.White
        Label7.ForeColor = Color.White

        Label8.ForeColor = Color.White
        Label9.ForeColor = Color.White
        Label10.ForeColor = Color.White
        Label11.ForeColor = Color.White
        Label12.ForeColor = Color.White
        Label13.ForeColor = Color.White

        Label14.ForeColor = Color.White
        Label15.ForeColor = Color.White
        Label16.ForeColor = Color.White
        Label17.ForeColor = Color.White
        Label18.ForeColor = Color.White
        Label19.ForeColor = Color.White
        Label20.ForeColor = Color.White

        GroupBox1.ForeColor = Color.White
        GrpAddStep.ForeColor = Color.White
        GroupBox3.ForeColor = Color.White
        GroupBox4.ForeColor = Color.White
        PanelUpgradableItem.ForeColor = Color.White
        GrpUpgrd.ForeColor = Color.White
    End Function
    Private Sub txtFilterArticle_TextChanged(sender As Object, e As EventArgs) Handles txtFilterArticle.TextChanged
        If Not String.IsNullOrEmpty(txtFilterArticle.Text) AndAlso txtFilterArticle.IsItemSelected Then
            txtFilterArticle.IsItemSelected = False
            Dim eKeyDown = New System.Windows.Forms.KeyEventArgs(Keys.Enter)
            Call txtFilterArticle_Leave(sender, eKeyDown)
        End If
    End Sub
    Private Sub txtFilterArticle_Leave(sender As System.Object, e As System.EventArgs) 'Handles txtFilterArticle.Leave
        Try

            'code added by vipul for issue id 3300
            GrpAddStep.Visible = False

            BlankItemDetailSection()
            BlankStepData()

            Cursor.Current = Cursors.WaitCursor
            txtFilterArticle.Text = txtFilterArticle.Text.ToString().Split(" ")(0)
            DtArticleCOmbo = Objcm.GetComboDefinedDetail(txtFilterArticle.Text)
            If DtArticleCOmbo.Rows.Count > 0 Then
                Dim Dr1 = DtArticleCOmbo.Select("Status =1", "")
                BtnSaveCombo.Visible = False
                BtnCancel.Visible = False
                BtnActivateDeactivate.Visible = True
                If Dr1.Count > 0 Then
                    BtnActivateDeactivate.Text = "De-Activate Combo"
                Else
                    BtnActivateDeactivate.Text = "Activate Combo"
                End If
                BtnAddStep.Visible = False
                '    GrpShowData.Enabled = False
                GrpShowData.Visible = True
                '  GrdShowData.Enabled = False
                GrpAddStep.Visible = False
                BindShowDataGrid()
                BtnActivateDeactivate.Visible = True
                IsAlertRequiredOnClosingForm = False

                GridSizeChangeRequired(True)

            Else
                BtnSaveCombo.Visible = False
                BtnCancel.Visible = True
                BtnActivateDeactivate.Visible = False
                GrpShowData.Visible = False
                BindShowDataGrid()
                GridSizeChangeRequired(False)
            End If
            If txtFilterArticle.Text.Length >= 1 Then
                Call SetSelectedArticle(txtFilterArticle.Text)
                txtFilterArticle.Focus()
            End If
            If String.IsNullOrEmpty(TxtSalePRice.Text) Then
                BtnAddStep.Visible = False
            Else
                If GrdShowData.Visible = False Then
                    If TxtStatus.Text = "In-Active" Then
                        BtnAddStep.Visible = True
                    End If
                End If

            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM020"), "CM020 - " & getValueByKey("CLAE05"))
            LogException(ex)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    'Private Sub CmdShowHierachy_Click(sender As Object, e As EventArgs) Handles CmdShowHierachy.Click
    '    Try
    '        Dim ObjHierPopup As New frmHierarchyPopUp
    '        ObjHierPopup.ShowDialog()
    '        TxtHierarchy.Text = ObjHierPopup.SelectedNodeCode

    '        TxtGroupId.Text = ""
    '        '  txtItemCode.Text = ""
    '        dtGuest.Clear()
    '        grdAdditem.DataSource = dtGuest
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Sub TxtInternalComboArticle_TextChanged(sender As Object, e As EventArgs) Handles TxtInternalComboArticle.TextChanged, TxtInternalComboArticle.TextChanged
        If Not String.IsNullOrEmpty(TxtInternalComboArticle.Text) AndAlso TxtInternalComboArticle.IsItemSelected Then
            TxtInternalComboArticle.IsItemSelected = False
            'SendKeys.Send("{Enter}")
            Dim eKeyDown = New System.Windows.Forms.KeyEventArgs(Keys.Enter)
            Call TxtInternalComboArticle_Leave(sender, eKeyDown)
        End If
    End Sub
    Private Sub TxtInternalComboArticle_Leave(sender As Object, e As EventArgs) 'Handles TxtInternalComboArticle.Leave
        Try
            Cursor.Current = Cursors.WaitCursor
            TxtInternalComboArticle.Text = TxtInternalComboArticle.Text.ToString().Split(" ")(0)
            If TxtInternalComboArticle.Text.Length >= 1 Then
                Dim membershipmaparticle = TxtInternalComboArticle.Text
                Dim objItemSch As New clsIteamSearch
                ' If e.KeyCode = Keys.Enter AndAlso txtFilterArticle.Text <> String.Empty Then
                Call BindAddArticle(TxtInternalComboArticle.Text)
                'End If
                'End If
                TxtInternalComboArticle.Focus()
                TxtHierarchy.Text = ""
                TxtGroupId.Text = ""
            End If
            TxtInternalComboArticle.Text = ""
        Catch ex As Exception
            ShowMessage(getValueByKey("CM020"), "CM020 - " & getValueByKey("CLAE05"))
            LogException(ex)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    Private Sub TxtGroupId_TextChanged(sender As Object, e As EventArgs) Handles TxtGroupId.TextChanged, TxtGroupId.TextChanged
        If Not String.IsNullOrEmpty(TxtGroupId.Text) AndAlso TxtGroupId.IsItemSelected Then
            TxtGroupId.IsItemSelected = False
            'SendKeys.Send("{Enter}")
            Dim eKeyDown = New System.Windows.Forms.KeyEventArgs(Keys.Enter)
            Call TxtGroupId_Leave(sender, eKeyDown)
        End If
    End Sub
    Private Sub TxtGroupId_Leave(sender As Object, e As EventArgs) 'Handles TxtInternalComboArticle.Leave
        Try
            Cursor.Current = Cursors.WaitCursor
            TxtGroupId.Text = TxtGroupId.Text.ToString().Split(" ")(0)
            If TxtGroupId.Text.Length >= 1 Then
                Dim membershipmaparticle = TxtGroupId.Text
                Dim objItemSch As New clsIteamSearch

                TxtGroupId.Focus()
                TxtHierarchy.Text = ""
                dtGuest.Clear()
                grdAdditem.DataSource = dtGuest
                GrpUpgrd.Visible = False
                PanelUpgradableItem.Visible = False
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM020"), "CM020 - " & getValueByKey("CLAE05"))
            LogException(ex)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    Private Sub TxtupGradableItem_TextChanged(sender As Object, e As EventArgs) Handles TxtupGradableItem.TextChanged
        If Not String.IsNullOrEmpty(TxtupGradableItem.Text) AndAlso TxtupGradableItem.IsItemSelected Then
            TxtupGradableItem.IsItemSelected = False
            Dim eKeyDown = New System.Windows.Forms.KeyEventArgs(Keys.Enter)
            Call TxtupGradableItem_Leave(sender, eKeyDown)
        End If

    End Sub
    Private Sub TxtupGradableItem_Leave(sender As Object, e As EventArgs) 'Handles TxtInternalComboArticle.Leave
        Try
            Cursor.Current = Cursors.WaitCursor
            TxtupGradableItem.Text = TxtupGradableItem.Text.ToString().Split(" ")(0)
            If TxtupGradableItem.Text.Length >= 1 Then
                Dim membershipmaparticle = TxtupGradableItem.Text
                Dim objItemSch As New clsIteamSearch
                Call BindUpgradableItem(TxtupGradableItem.Text)
                TxtupGradableItem.Focus()
                TxtupGradableItem.Text = ""
                TxtUpgradableHierarchy.Text = ""
                TxtUpgradableGroup.Text = ""
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM020"), "CM020 - " & getValueByKey("CLAE05"))
            LogException(ex)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    Private Sub grdAdditem_CellButtonClick(sender As Object, e As RowColEventArgs) Handles grdAdditem.CellButtonClick
        Try
            Dim SrNo = grdAdditem.Item(grdAdditem.Row, "SrNo")
            DeleteGuestDetails(SrNo)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function DeleteGuestDetails(srNo As Integer) As Boolean
        Try
            If dtGuest.Rows.Count > 0 Then
                Dim drDtl() = dtGuest.Select("SrNo=" & srNo & "")
                If drDtl.Count > 0 Then
                    For Each row As DataRow In drDtl
                        dtGuest.Rows.Remove(row)
                    Next
                End If
                dtGuest.AcceptChanges()
                Dim count = 1
                For index = 0 To dtGuest.Rows.Count - 1
                    dtGuest.Rows(index)("SrNo") = count
                    count += 1
                Next
                srNo = count
            End If
            If dtGuest.Rows.Count = 0 Then
                GrpUpgrd.Visible = False
                PanelUpgradableItem.Visible = False

                TxtUpgradableGroup.Text = ""
                TxtupGradableItem.Text = ""
                TxtUpgradableHierarchy.Text = ""
                DtUpGradableItem.Clear()
                GrdUpgradableItem.DataSource = DtUpGradableItem

            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Private Function DeleteShowDaeGrid(srNo As Integer) As Boolean
        Try
            'GrdShowData
            If DtArticleCOmbo.Rows.Count > 0 Then
                Dim drDtl() = DtArticleCOmbo.Select("Sequence=" & srNo & "")
                If drDtl.Count > 0 Then
                    For Each row As DataRow In drDtl
                        DtArticleCOmbo.Rows.Remove(row)
                    Next
                End If
                DtArticleCOmbo.AcceptChanges()
                Dim count = 1
                For index = 0 To DtArticleCOmbo.Rows.Count - 1
                    DtArticleCOmbo.Rows(index)("Sequence") = count
                    count += 1
                Next
                srNo = count
            End If
            If DtArticleCOmbo.Rows.Count = 0 Then
                GrpUpgrd.Visible = False
                PanelUpgradableItem.Visible = False
            End If
            '    gridArticleDetailsSetting()

        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Private Function IsEnterefCostPriceValid() As Boolean
        Try
            If String.IsNullOrEmpty(TxtCost.Text) Then
                TxtCost.Text = "0"
            End If
            If String.IsNullOrEmpty(lblTotCost.Text) Then
                TxtCost.Text = "0"
            End If


            Dim EnterCost As Decimal = CDbl(TxtCost.Text)
            Dim TotalCost As Decimal = CDbl(lblTotCost.Text)
            Dim ComboDefinedPrice As Decimal = CDbl(TxtSalePRice.Text)

            If (EnterCost + TotalCost) = ComboDefinedPrice Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Private Sub GrdUpgradableItem_CellButtonClick(sender As Object, e As RowColEventArgs) Handles GrdUpgradableItem.CellButtonClick
        Try

            Dim SrNo = GrdUpgradableItem.Item(GrdUpgradableItem.Row, "SrNo")
            DeleteUpgradableItem(SrNo)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function DeleteUpgradableItem(srNo As Integer) As Boolean
        Try
            If DtUpGradableItem.Rows.Count > 0 Then
                Dim drDtl() = DtUpGradableItem.Select("SrNo=" & srNo & "")
                If drDtl.Count > 0 Then
                    For Each row As DataRow In drDtl
                        DtUpGradableItem.Rows.Remove(row)
                    Next
                End If
                DtUpGradableItem.AcceptChanges()
                Dim count = 1
                For index = 0 To DtUpGradableItem.Rows.Count - 1
                    DtUpGradableItem.Rows(index)("SrNo") = count
                    count += 1
                Next
                srNo = count
            End If
            GridUpgradableItemGrid()

        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
#End Region
#Region "Function"
    Private Sub SetSelectedArticle(ByVal article As String) ''checkout As DateTime,
        Try
            Dim DtArtcleData As DataTable = Objcm.GetComboDtl(clsAdmin.SiteCode, article)
            If Not DtArtcleData Is Nothing AndAlso DtArtcleData.Rows.Count > 0 Then

                TxtLastNodeCode.Text = DtArtcleData.Rows(0)("lastnodecode").ToString.Trim()
                TxtParentItemCode.Text = DtArtcleData.Rows(0)("ParentArt").ToString.Trim()
                TxtItemTree.Text = DtArtcleData.Rows(0)("Treeid").ToString.Trim()

                txtItemCode.Text = DtArtcleData.Rows(0)("ArticleCOde").ToString.Trim()
                TxtItemSearchName.Text = DtArtcleData.Rows(0)("ArticleShortNAme").ToString.Trim()
                txtFilterArticle.Text = DtArtcleData.Rows(0)("ArticleShortNAme").ToString.Trim()
                txtItemType.Text = DtArtcleData.Rows(0)("ArticalTypeCode").ToString.Trim()
                TxtmaterialType.Text = DtArtcleData.Rows(0)("MaterialTypeCode").ToString.Trim()
                TxtSalePRice.Text = IIf(DtArtcleData.Rows(0)("SellingPrice").ToString.Trim() = "", "0", DtArtcleData.Rows(0)("SellingPrice").ToString.Trim())
                TxtStatus.Text = DtArtcleData.Rows(0)("ArticleActive").ToString.Trim()
            Else

                BlankItemDetailSection()
            End If


        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub BindAddArticle(ByVal article As String) ''checkout As DateTime,
        Try
            'grdArticleSearch.Clear()
            ' dtCustmData.Clear()

            dtCustmData = objArticle.GetArticleDetails(article)
            If dtCustmData IsNot Nothing And dtCustmData.Rows.Count > 0 Then
                If dtCustmData IsNot Nothing And dtCustmData.Rows.Count > 0 Then
                    If dtCustmData.Rows.Count > 0 Then
                        For index = 0 To dtGuest.Rows.Count - 1
                            Dim dtRow As Int32 = -1
                            Dim result As DataRow() = dtGuest.Select("ArticleCode='" + article.Trim + "' ")
                            If result.Length > 0 Then
                                ShowMessage("Record Already exist", "Information")
                                '  txtFilterArticle.Clear()
                                Exit Sub
                            End If


                        Next

                    End If
                    Dim rowGuest As DataRow

                    rowGuest = dtGuest.NewRow()
                    rowGuest("Srno") = dtGuest.Rows.Count + 1
                    rowGuest("ArticleCode") = dtCustmData.Rows(0)("ArticleCode")
                    rowGuest("ArticleName") = dtCustmData.Rows(0)("ArticleName")
                    rowGuest("LastNodeCode") = dtCustmData.Rows(0)("NodeName")
                    dtGuest.Rows.Add(rowGuest)


                Else
                    grdAdditem.DataSource = dtCustmData
                    dtArticle = dtCustmData
                    grdAdditem.Refresh()
                End If
                gridArticleDetailsSetting()
                '  txtFilterArticle.Clear()


            ElseIf Not dtCustmData Is Nothing And dtCustmData.Rows.Count = 0 Then
                ShowMessage(getValueByKey("CM016"), "CM016 - " & getValueByKey("CLAE04"))
                '  txtFilterArticle.Text = String.Empty
                txtFilterArticle.Focus()
                Exit Sub
            Else
                grdAdditem.DataSource = dtCustmData
                dtArticle = dtCustmData
                grdAdditem.Refresh()
            End If
            If dtCustmData.Rows.Count > 0 Then
                GrpUpgrd.Visible = True
                PanelUpgradableItem.Visible = True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub BindUpgradableItem(ByVal article As String) ''checkout As DateTime,
        Try
            'grdArticleSearch.Clear()
            ' dtCustmData.Clear()

            dtCustmData = objArticle.GetArticleDetails(article)
            If dtCustmData IsNot Nothing And dtCustmData.Rows.Count > 0 Then
                If dtCustmData IsNot Nothing And dtCustmData.Rows.Count > 0 Then
                    If dtCustmData.Rows.Count > 0 Then
                        For index = 0 To DtUpGradableItem.Rows.Count - 1
                            Dim dtRow As Int32 = -1
                            Dim result As DataRow() = DtUpGradableItem.Select("ArticleCode='" + article.Trim + "' ")
                            If result.Length > 0 Then
                                ShowMessage("Record Already exist", "Information")
                                txtFilterArticle.Clear()
                                Exit Sub
                            End If


                        Next

                    End If
                    Dim rowGuest As DataRow

                    rowGuest = DtUpGradableItem.NewRow()
                    rowGuest("Srno") = DtUpGradableItem.Rows.Count + 1
                    rowGuest("ArticleCode") = dtCustmData.Rows(0)("ArticleCode")
                    rowGuest("ArticleName") = dtCustmData.Rows(0)("ArticleName")
                    rowGuest("LastNodeCode") = dtCustmData.Rows(0)("NodeName")
                    DtUpGradableItem.Rows.Add(rowGuest)


                Else
                    GrdUpgradableItem.DataSource = dtCustmData
                    dtArticle = dtCustmData
                    GrdUpgradableItem.Refresh()
                End If
                GridUpgradableItemGrid()
                '  txtFilterArticle.Clear()


            ElseIf Not dtCustmData Is Nothing And dtCustmData.Rows.Count = 0 Then
                ShowMessage(getValueByKey("CM016"), "CM016 - " & getValueByKey("CLAE04"))
                txtFilterArticle.Text = String.Empty
                txtFilterArticle.Focus()
                Exit Sub
            Else
                GrdUpgradableItem.DataSource = dtCustmData
                dtArticle = dtCustmData
                GrdUpgradableItem.Refresh()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub BlankItemDetailSection()
        Try
            TxtLastNodeCode.Text = ""
            TxtParentItemCode.Text = ""
            TxtItemTree.Text = ""

            txtItemCode.Text = ""
            TxtItemSearchName.Text = ""
            txtItemType.Text = ""
            TxtmaterialType.Text = ""
            TxtSalePRice.Text = ""
            TxtStatus.Text = ""

            TxtUpgradableHierarchy.Text = ""
            TxtHierarchy.Text = ""

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub BlankStepData()
        Try
            TxtHierarchy.Text = ""
            TxtInternalComboArticle.Text = ""
            TxtGroupId.Text = ""
            TxtQty.Value = 0
            TxtindividualQty.Text = ""
            TxtCost.Text = ""
            TxtUpgradableHierarchy.Text = ""
            TxtupGradableItem.Text = ""
            TxtUpgradableGroup.Text = ""

            dtGuest.Clear()
            grdAdditem.DataSource = dtGuest
            DtUpGradableItem.Clear()
            GrdUpgradableItem.DataSource = DtUpGradableItem ' dtArticle

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub gridArticleDetailsSetting()
        Try
            grdAdditem.DataSource = dtGuest ' dtArticle
            'grdArticleSearch.Cols("Del").Caption = ""
            'grdArticleSearch.Cols("Del").Width = 20
            'grdArticleSearch.Cols("Del").ComboList = "..."
            'grdArticleSearch.Cols("Del").Visible = True
            grdAdditem.Cols("SrNo").Visible = True
            grdAdditem.Cols("ArticleCode").Width = 100
            grdAdditem.Cols("ArticleCode").DataType = Type.GetType("System.String")
            grdAdditem.Cols("ArticleCode").AllowEditing = False
            grdAdditem.Cols("ArticleCode").Name = "ArticleCode"
            grdAdditem.Cols("ArticleCode").Caption = "Article Code"
            grdAdditem.Cols("ArticleCode").TextAlign = TextAlignEnum.LeftCenter

            grdAdditem.Cols("ArticleName").Caption = "Article Name"
            grdAdditem.Cols("ArticleName").Width = 300
            grdAdditem.Cols("ArticleName").AllowEditing = False
            grdAdditem.Cols("ArticleName").DataType = Type.GetType("System.String")
            grdAdditem.Cols("ArticleName").Name = "ArticleName"
            grdAdditem.Cols("ArticleName").TextAlign = TextAlignEnum.LeftCenter

            grdAdditem.Cols("LastNodeCode").Width = 150
            grdAdditem.Cols("LastNodeCode").Caption = "Last Node"
            grdAdditem.Cols("LastNodeCode").AllowEditing = False
            grdAdditem.Cols("LastNodeCode").DataType = Type.GetType("System.String")


        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub GridUpgradableItemGrid()
        Try
            GrdUpgradableItem.DataSource = DtUpGradableItem ' dtArticle
            GrdUpgradableItem.Cols("SrNo").Visible = True
            GrdUpgradableItem.Cols("ArticleCode").Width = 100
            GrdUpgradableItem.Cols("ArticleCode").DataType = Type.GetType("System.String")
            GrdUpgradableItem.Cols("ArticleCode").AllowEditing = False
            GrdUpgradableItem.Cols("ArticleCode").Name = "ArticleCode"
            GrdUpgradableItem.Cols("ArticleCode").Caption = "Article Code"
            GrdUpgradableItem.Cols("ArticleCode").TextAlign = TextAlignEnum.LeftCenter

            GrdUpgradableItem.Cols("ArticleName").Caption = "Article Name"
            GrdUpgradableItem.Cols("ArticleName").Width = 300
            GrdUpgradableItem.Cols("ArticleName").AllowEditing = False
            GrdUpgradableItem.Cols("ArticleName").DataType = Type.GetType("System.String")
            GrdUpgradableItem.Cols("ArticleName").Name = "ArticleName"
            GrdUpgradableItem.Cols("ArticleName").TextAlign = TextAlignEnum.LeftCenter

            GrdUpgradableItem.Cols("LastNodeCode").Width = 150
            GrdUpgradableItem.Cols("LastNodeCode").Caption = "Last Node"
            GrdUpgradableItem.Cols("LastNodeCode").AllowEditing = False
            GrdUpgradableItem.Cols("LastNodeCode").DataType = Type.GetType("System.String")


        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub BindShowDataGrid()
        Try
            GrdShowData.DataSource = DtArticleCOmbo
            Dim TotalQty As Decimal
            Dim TotalCost As Decimal
            If DtArticleCOmbo.Rows.Count > 0 Then
                TotalQty = CDbl(DtArticleCOmbo.Compute("Sum(Quantity)", ""))
                TotalCost = CDbl(DtArticleCOmbo.Compute("Sum(Cost)", ""))
                LblTotQty.Text = FormatNumber(TotalQty, 2)
                lblTotCost.Text = FormatNumber(TotalCost, 2)
            Else
                LblTotQty.Text = "0.00"
                lblTotCost.Text = "0.00"
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
#End Region

    Private Sub CtrlBtn1_Click(sender As Object, e As EventArgs)
        Try
            Dim ObjHierPopup As New frmHierarchyPopUp
            ObjHierPopup.ShowDialog()
            TxtUpgradableHierarchy.Text = ObjHierPopup.SelectedNodeCode
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CtrlBtn2_Click(sender As Object, e As EventArgs) Handles BtnAddStep.Click
        If GrpAddStep.Visible = False Then
            Dim iid As Integer
            If grdAdditem.Rows.Count > 0 Then
                For iid = grdAdditem.Rows.Count - 1 To 1 Step -1
                    grdAdditem.Rows.Remove(iid)
                Next
            End If
            If GrdUpgradableItem.Rows.Count > 0 Then
                For iid = GrdUpgradableItem.Rows.Count - 1 To 1 Step -1
                    GrdUpgradableItem.Rows.Remove(iid)
                Next
            End If
            GrpAddStep.Visible = True
        Else
            GrpAddStep.Visible = False
            GrpUpgrd.Visible = False
            PanelUpgradableItem.Visible = False
        End If

    End Sub
    Private Sub CtrlBtn1_Click_1(sender As Object, e As EventArgs)
        Try
            Dim ObjHierPopup As New frmHierarchyPopUp
            ObjHierPopup.ShowDialog()
            TxtUpgradableHierarchy.Text = ObjHierPopup.SelectedNodeCode
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BtnSaveStep_Click(sender As Object, e As EventArgs) Handles BtnSaveStep.Click
        Try
            GrdShowData.Enabled = True
            If String.IsNullOrEmpty(TxtHierarchy.Text) And String.IsNullOrEmpty(TxtGroupId.Text) Then
                If grdAdditem.Rows.Count = 1 Then
                    ShowMessage("Please select atleast 1 mandatory (Hierarchy/Item/Group) criteria.", getValueByKey("CLAE04"))
                    Exit Sub
                End If
            End If
            If TxtQty.Value = 0 Then
                ShowMessage("Please enter the quantity.", getValueByKey("CLAE04"))
                Exit Sub
            End If
            If String.IsNullOrEmpty(TxtindividualQty.Text) Then
                ShowMessage("Please enter the individual quantity.", getValueByKey("CLAE04"))
                Exit Sub
            End If
            If String.IsNullOrEmpty(TxtCost.Text) Then
                ShowMessage("Please enter the cost.", getValueByKey("CLAE04"))
                Exit Sub
            End If

            articleCode = ""
            For Each dtnodetree In dtGuest.Rows
                If articleCode.Trim() = "" Or articleCode.Trim Is Nothing Then
                    articleCode = dtnodetree("ArticleCode").ToString.Trim
                Else
                    articleCode = articleCode & "," & dtnodetree("ArticleCode").ToString.Trim.ToString.Trim
                End If
            Next

            UpGradableArticlecode = ""
            For Each dtnodetree In DtUpGradableItem.Rows
                If UpGradableArticlecode.Trim() = "" Or UpGradableArticlecode.Trim Is Nothing Then
                    UpGradableArticlecode = dtnodetree("ArticleCode").ToString.Trim
                Else
                    UpGradableArticlecode = UpGradableArticlecode & "," & dtnodetree("ArticleCode").ToString.Trim.ToString.Trim
                End If
            Next

            Dim DrArtCombo = DtArticleCOmbo.NewRow()


            '     Dim ComboCode = GenDocNo("CB" & clsAdmin.SiteCode, 15, ComboIncNo)

            If String.IsNullOrEmpty(TxtCost.Text) Then
                TxtCost.Text = "0"
            End If
            If String.IsNullOrEmpty(TxtQty.Text) Then
                TxtQty.Text = "0"
            End If

            DrArtCombo("id") = "0"
            DrArtCombo("ComboCode") = txtItemCode.Text
            If String.IsNullOrEmpty(TxtHierarchy.Text) Then
                DrArtCombo("Hierarchy") = DBNull.Value
            Else
                DrArtCombo("Hierarchy") = TxtHierarchy.Text
            End If

            If String.IsNullOrEmpty(articleCode) Then
                DrArtCombo("Item") = DBNull.Value
            Else
                DrArtCombo("Item") = articleCode
            End If


            DrArtCombo("Cost") = TxtCost.Text
            DrArtCombo("Discount") = 0

            If String.IsNullOrEmpty(TxtUpgradableHierarchy.Text.Trim) Then
                DrArtCombo("UpdateHierarchy") = DBNull.Value 'TxtUpgradableHierarchy.Text
            Else
                DrArtCombo("UpdateHierarchy") = TxtUpgradableHierarchy.Text
            End If

            If String.IsNullOrEmpty(UpGradableArticlecode) Then
                DrArtCombo("UpdateItem") = DBNull.Value 'TxtUpgradableHierarchy.Text
            Else
                DrArtCombo("UpdateItem") = UpGradableArticlecode
            End If

            DrArtCombo("Sequence") = DtArticleCOmbo.Rows.Count + 1
            DrArtCombo("Quantity") = TxtQty.Text
            DrArtCombo("GroupIDs") = TxtGroupId.Text
            DrArtCombo("CREATEDAT") = clsAdmin.SiteCode
            DrArtCombo("CREATEDBY") = clsAdmin.UserCode
            DrArtCombo("CREATEDON") = DateTime.Now()
            DrArtCombo("UPDATEDAT") = clsAdmin.SiteCode
            DrArtCombo("UPDATEDBY") = clsAdmin.UserCode
            DrArtCombo("UPDATEDON") = DateTime.Now()
            DrArtCombo("STATUS") = 1

            If String.IsNullOrEmpty(TxtUpgradableGroup.Text.Trim) Then
                DrArtCombo("UpgradeGroupId") = DBNull.Value 'TxtUpgradableHierarchy.Text
            Else
                DrArtCombo("UpgradeGroupId") = TxtUpgradableGroup.Text
            End If
            DrArtCombo("IndividualQty") = TxtindividualQty.Text
            'DrArtCombo 
            DtArticleCOmbo.Rows.Add(DrArtCombo)
            DtArticleCOmbo.TableName = "MstArticleCombo"

            'If DsCombo.Tables.Count > 0 Then
            'Else
            '    DsCombo.Tables.Add(DtArticleCOmbo)
            'End If

            GrpShowData.Enabled = True
            BindShowDataGrid()

            BlankStepData()
            GrpUpgrd.Visible = False
            PanelUpgradableItem.Visible = False
            '  BlankItemDetailSection()
            If DtArticleCOmbo.Rows.Count > 0 Then
                GrpShowData.Visible = True
                BtnSaveCombo.Visible = True
            End If
            'code is added by irfan on 26/4/2018 for mantis issue.
            GrdShowData.Cols(4).AllowEditing = False
            GrdShowData.Cols(5).AllowEditing = False
            GrdShowData.Cols(6).AllowEditing = False
            GrdShowData.Cols(7).AllowEditing = False
            GrdShowData.Cols(8).AllowEditing = False
            GrdShowData.Cols(9).AllowEditing = False
            GrdShowData.Cols(10).AllowEditing = False
            GrdShowData.Cols(11).AllowEditing = False
            '  ComboIncNo = ComboIncNo + 1
            IsAlertRequiredOnClosingForm = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Try

            If BtnAddStep.Visible = True Then
                If MsgBox("Are you sure you want to Clear.You will loose your unsave data", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    BlankItemDetailSection()
                    BlankStepData()
                    txtFilterArticle.Text = ""
                    txtFilterArticle.Focus()
                    GrpAddStep.Visible = False
                    GrpShowData.Visible = False
                    BtnAddStep.Visible = False
                    BtnSaveCombo.Visible = False
                    IsAlertRequiredOnClosingForm = False
                    TxtLeave()
                End If
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub BtnSaveCombo_Click(sender As Object, e As EventArgs) Handles BtnSaveCombo.Click

        ComboIncNo = objItem.getDocumentNo("CB", clsAdmin.SiteCode, "BK_DOC")


        If DtArticleCOmbo.Rows.Count > 0 Then
        Else
            ShowMessage("Please define atleast 1 step.", getValueByKey("CLAE04"))
            Exit Sub
        End If


        If IsEnterefCostPriceValid() = False Then
            ShowMessage("Total cost price should be equal to combo sales price.", getValueByKey("CLAE04"))
            TxtCost.Focus()
            Exit Sub
        End If



        Dim DsCombo As New DataSet
        Dim DtDefineCOmboForSave As New DataTable
        DtDefineCOmboForSave = Objcm.GetMstArticleComboStruct

        'ashma 28 may 2018 - updating status in MstArticle
        Dim DtArticle As New DataTable
        DtArticle = Objcm.GetArticleDetail(txtItemCode.Text)

        For Each Drrow1 In DtArticleCOmbo.Rows
            '  Dim ComboCode = GenDocNo("CB" & clsAdmin.SiteCode, 15, ComboIncNo)
           
            Dim ComboCode = GenDocNo("CB" & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Length - 3, 3), 15, ComboIncNo)

            Dim drsave = DtDefineCOmboForSave.NewRow()
            drsave("ID") = ComboCode
            drsave("ComboCode") = Drrow1("ComboCode")
            drsave("Hierarchy") = Drrow1("Hierarchy")
            drsave("Item") = Drrow1("Item")
            drsave("Cost") = Drrow1("Cost")
            drsave("Discount") = Drrow1("Discount")
            drsave("UpdateHierarchy") = Drrow1("UpdateHierarchy")
            drsave("UpdateItem") = Drrow1("UpdateItem")
            drsave("Sequence") = Drrow1("Sequence")
            drsave("Quantity") = Drrow1("Quantity")
            drsave("GroupIDs") = Drrow1("GroupIDs")
            drsave("CREATEDAT") = Drrow1("CREATEDAT")
            drsave("CREATEDBY") = Drrow1("CREATEDBY")

            drsave("CREATEDON") = Drrow1("CREATEDON")
            drsave("UPDATEDAT") = Drrow1("UPDATEDAT")
            drsave("UPDATEDBY") = Drrow1("UPDATEDBY")
            drsave("UPDATEDON") = Drrow1("UPDATEDON")
            drsave("STATUS") = Drrow1("STATUS")
            drsave("UpgradeGroupId") = Drrow1("UpgradeGroupId")
            drsave("IndividualQty") = Drrow1("IndividualQty")
            DtDefineCOmboForSave.Rows.Add(drsave)
            ComboIncNo = ComboIncNo + 1 '##
        Next
        'ashma 28 may 2018 - updating status in MstArticle
        DtArticle.Rows(0)("STATUS") = 1
        DtArticle.Rows(0)("Salable") = 1
        DtArticle.Rows(0)("ArticleActive") = 1
        DtArticle.Rows(0)("UPDATEDAT") = clsAdmin.SiteCode
        DtArticle.Rows(0)("UPDATEDBY") = clsAdmin.UserCode
        DtArticle.Rows(0)("UPDATEDON") = Date.Now

        'ashma 28 may 2018 - updating status in MstArticle
        DtArticle.TableName = "MstArticle"
        DtDefineCOmboForSave.TableName = "MstArticleCombo"
        DsCombo.Tables.Add(DtDefineCOmboForSave)
        DsCombo.Tables.Add(DtArticle)


        If objItem.SaveMstArticleCombo(DsCombo, ComboIncNo) Then
            ShowMessage("Combo " & DsCombo.Tables(0).Rows(0)("ComboCode").Trim & " Defined Succesfully", getValueByKey("CLAE04"))
            BtnSaveCombo.Visible = False
            IsAlertRequiredOnClosingForm = False
            TxtLeave()
        Else
            ShowMessage("Error in Define Combo Define", getValueByKey("CLAE04"))
        End If

        BlankItemDetailSection()
        BlankStepData()
        DtArticleCOmbo.Clear()
        DtDefineCOmboForSave.Clear()
        BindShowDataGrid()


        txtFilterArticle.Focus()
        txtFilterArticle.Text = ""
        GrpAddStep.Visible = False
        GrpShowData.Visible = False
        BtnAddStep.Visible = False
    End Sub

    Private Sub BtnActivateDeactivate_Click(sender As Object, e As EventArgs) Handles BtnActivateDeactivate.Click
        Try
            Dim Activate_DeActivate As Boolean = False
            If BtnActivateDeactivate.Text = "Activate Combo" Then
                Activate_DeActivate = True
            Else
                Activate_DeActivate = False
            End If

            If Objcm.ActivateDeActivateComboDtl(txtItemCode.Text, clsAdmin.SiteCode, clsAdmin.UserCode, Activate_DeActivate) Then
                If Activate_DeActivate Then
                    ShowMessage("Combo Activated Successfully", getValueByKey("CLAE04"))
                    BtnActivateDeactivate.Text = "De-Activate Combo"
                Else
                    ShowMessage("Combo De-Activated Successfully", getValueByKey("CLAE04"))
                    BtnActivateDeactivate.Text = "Activate Combo"
                End If
                BlankItemDetailSection()
                BlankStepData()
                DtArticleCOmbo.Clear()
                '    DtDefineCOmboForSave.Clear()
                BindShowDataGrid()
                txtFilterArticle.Focus()
                txtFilterArticle.Text = ""
                GrpAddStep.Visible = False
                GrpShowData.Visible = False
                BtnAddStep.Visible = False
                BtnSaveCombo.Visible = False
                BtnActivateDeactivate.Visible = False
                TxtLeave()
            Else
                ShowMessage("Error while updating combo", getValueByKey("CLAE04"))
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub GrdShowData_CellButtonClick(sender As Object, e As RowColEventArgs) Handles GrdShowData.CellButtonClick
        Try
            Dim SrNo = GrdShowData.Item(GrdShowData.Row, "Sequence")
            DeleteShowDaeGrid(SrNo)
            BindShowDataGrid()
            '  ComboIncNo = ComboIncNo - 1
            If GrdShowData.Rows.Count = 1 Then
                IsAlertRequiredOnClosingForm = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub TxtHierarchy_Click(sender As Object, e As EventArgs) Handles TxtHierarchy.Click
        Try
            Dim ObjHierPopup As New frmHierarchyPopUp
            ObjHierPopup._IsCallFromCombo = True
            ObjHierPopup.ShowDialog()
            TxtHierarchy.Text = ObjHierPopup.SelectedNodeCode

            TxtGroupId.Text = ""
            '  txtItemCode.Text = ""
            dtGuest.Clear()
            grdAdditem.DataSource = dtGuest
            GrpUpgrd.Visible = False
            PanelUpgradableItem.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TxtUpgradableHierarchy_Click(sender As Object, e As EventArgs) Handles TxtUpgradableHierarchy.Click
        Try
            Dim ObjHierPopup As New frmHierarchyPopUp
            ObjHierPopup._IsCallFromCombo = True
            ObjHierPopup.ShowDialog()
            TxtUpgradableHierarchy.Text = ObjHierPopup.SelectedNodeCode

            TxtUpgradableGroup.Text = ""
            DtUpGradableItem.Clear()
            GrdUpgradableItem.DataSource = DtUpGradableItem

        Catch ex As Exception
        End Try
    End Sub
    'Private Sub TxtCost_Leave(sender As Object, e As EventArgs) Handles TxtCost.Leave
    '    If IsEnterefCostPriceValid() = False Then
    '        ShowMessage("Entered cost price is grater than combo sales price.", getValueByKey("CLAE04"))
    '        TxtCost.Focus()
    '        TxtCost.Text = "0"
    '    End If
    'End Sub
    Private Sub TxtUpgradableGroup_TextChanged(sender As Object, e As EventArgs) Handles TxtUpgradableGroup.TextChanged, TxtUpgradableGroup.TextChanged
        If Not String.IsNullOrEmpty(TxtUpgradableGroup.Text) AndAlso TxtUpgradableGroup.IsItemSelected Then
            TxtUpgradableGroup.IsItemSelected = False
            'SendKeys.Send("{Enter}")
            Dim eKeyDown = New System.Windows.Forms.KeyEventArgs(Keys.Enter)
            Call TxtUpgradableGroup_Leave(sender, eKeyDown)
        End If
    End Sub
    Private Sub TxtUpgradableGroup_Leave(sender As Object, e As EventArgs) 'Handles TxtInternalComboArticle.Leave
        Try
            Cursor.Current = Cursors.WaitCursor
            TxtUpgradableGroup.Text = TxtUpgradableGroup.Text.ToString().Split(" ")(0)
            If TxtUpgradableGroup.Text.Length >= 1 Then
                Dim membershipmaparticle = TxtUpgradableGroup.Text
                Dim objItemSch As New clsIteamSearch

                TxtUpgradableGroup.Focus()
                TxtUpgradableHierarchy.Text = ""
                DtUpGradableItem.Clear()
                GrdUpgradableItem.DataSource = DtUpGradableItem
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM020"), "CM020 - " & getValueByKey("CLAE05"))
            LogException(ex)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs)
        If BtnSaveCombo.Visible = True Then
            If MsgBox("Are you sure you want to close.You will loose your unsave data", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If

    End Sub

    Private Sub FrmDefineCombo_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = Keys.Escape) Then
            'code added by vipul for issue id 3300
            If BtnSaveCombo.Visible = True Then
                If MsgBox("Are you sure you want to close.You will loose your unsave data", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Me.Close()
                End If
            Else
                Me.Close()
            End If
        End If
    End Sub

    Private Sub TxtindividualQty_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TxtindividualQty.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TxtCost_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TxtCost.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso Not (e.KeyChar) = "." Then
            e.Handled = True
        End If
    End Sub
    Private Sub GridSizeChangeRequired(ByVal IsGridSizeLargeRequired As Boolean)
        If IsGridSizeLargeRequired Then
            GrpShowData.Location = New Point(7, 300)
            GrpShowData.Size = New Size(1240, 340)
            GrdShowData.Size = New Size(1228, 290)
            lblTotCost.Location = New Point(1158, 310)
            LblTotQty.Location = New Point(1073, 310)
            Label1.Location = New Point(981, 310)

        Else
            GrpShowData.Location = New Point(7, 517)
            GrpShowData.Size = New Size(1240, 131)
            GrdShowData.Size = New Size(1228, 96)
            lblTotCost.Location = New Point(1158, 110)
            LblTotQty.Location = New Point(1073, 111)
            Label1.Location = New Point(981, 110)
        End If

    End Sub


    Private Sub btnClose_Click_1(sender As Object, e As EventArgs) Handles btnClose.Click
        If IsAlertRequiredOnClosingForm = True Then
            If MsgBox("Are you sure you want to Close.You will loose your unsave data", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Me.Close()
            End If

        Else
            Me.Close()
        End If
    End Sub
    Private Sub OnScreenKeyboard()
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub
    Private Sub TxtLeave()
        If txtFilterArticle.Text = "" And TxtLastNodeCode.Text = "" Then
            txtFilterArticle.Text = "Please select the Combo"
        End If

    End Sub
    Private Sub TxtClick()
        If txtFilterArticle.Text <> "" And TxtLastNodeCode.Text = "" Then
            txtFilterArticle.Text = ""

        End If
    End Sub
End Class