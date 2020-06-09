Imports SpectrumBL
Imports System.IO
Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Collections
Public Class frmRecordProduction
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Dim objrp As New clsRecordProduction
    Private Sub frmRecordProduction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            AddHandler txtSearch.KeyDown, AddressOf txtSearch_KeyDown
            Call GridSettings()
            For colno = 1 To DgRecordProductionGrid.Cols.Count - 1
                If DgRecordProductionGrid.Cols(colno).Name.ToUpper() <> "itemdesc".ToUpper() _
                    AndAlso DgRecordProductionGrid.Cols(colno).Name.ToUpper() <> "itemcode".ToUpper() _
                    AndAlso DgRecordProductionGrid.Cols(colno).Name.ToUpper() <> "Qty".ToUpper() _
                    AndAlso DgRecordProductionGrid.Cols(colno).Name.ToUpper() <> "Remarks".ToUpper _
                    AndAlso DgRecordProductionGrid.Cols(colno).Name.ToUpper() <> "Selects".ToUpper() _
                    AndAlso DgRecordProductionGrid.Cols(colno).Name.ToUpper() <> "" Then

                    HideColumns(DgRecordProductionGrid, False, DgRecordProductionGrid.Cols(colno).Name)
                End If
            Next

            Dim dtItemData = objrp.GetItemDetails()
            PopulateComboBox(dtItemData, txtSearch)
            If clsDefaultConfiguration.PrintItemFullName Then
                txtSearch.DisplayMember = "ArticleName"
            Else
                txtSearch.DisplayMember = "ArticleShortName"
            End If
            txtSearch.ValueMember = "ArticleCode"
            pC1ComboSetDisplayMember(txtSearch)

        Catch ex As Exception
            ShowMessage(False, getValueByKey("CLAE05"))
            'Error
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Fetches the Items in Combobox and enter the items in Grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs)
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim strArticle As String = ""
            Dim Ean As String = ""
            Dim SlapperName = txtSlapperName.Text
            If e.KeyCode = Keys.Delete AndAlso DgRecordProductionGrid.Rows.Count > 1 AndAlso txtSearch.Text.Length = 0 Then
                DgRecordProductionGrid.Rows.Remove(DgRecordProductionGrid.Row)
                If (DgRecordProductionGrid.Rows.Count > 1) Then
                    DgRecordProductionGrid.Select(1, 2)
                End If
                sender.Select()
                sender.Focus()
                Exit Sub
            End If

            If (e.KeyCode = Keys.Enter AndAlso sender.Text <> String.Empty) Then
                Dim dt As New DataTable
                dt = objrp.GetAllItems(clsAdmin.SiteCode, sender.Text.Trim, clsAdmin.LangCode)
                If dt.Rows.Count = 0 Then
                    'MsgBox("Please Select Proper Article ")"
                    ShowMessage(getValueByKey("RCP0009"), "RCP0009 - " & getValueByKey("CLAE04"))
                    sender.Text = String.Empty
                    Exit Sub
                End If
                If DgRecordProductionGrid.Rows.Count > 1 Then
                    For index = 1 To DgRecordProductionGrid.Rows.Count - 1

                        If dt.Rows(0)("ArticleCode").ToString() = DgRecordProductionGrid.Rows(index)("itemcode") Then
                            '  Article is Already Added
                            ShowMessage(getValueByKey("CLIST06"), "CLIST06 - " & getValueByKey("CLIST06"))
                            sender.Text = String.Empty
                            Exit Sub
                        End If

                    Next
                End If
                Dim ItemDesc As String = String.Empty
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    'DgRecordProductionGrid.Rows.Add()
                    DgRecordProductionGrid.Rows.Add()
                    DgRecordProductionGrid.Rows(DgRecordProductionGrid.Rows.Count - 1)("Selects") = ""
                    If clsDefaultConfiguration.PrintItemFullName Then
                        DgRecordProductionGrid.Rows(DgRecordProductionGrid.Rows.Count - 1)("itemdesc") = dt.Rows(0)("ArticleName")
                    Else
                        DgRecordProductionGrid.Rows(DgRecordProductionGrid.Rows.Count - 1)("itemdesc") = dt.Rows(0)("ArticleShortName")
                    End If
                    DgRecordProductionGrid.Rows(DgRecordProductionGrid.Rows.Count - 1)("itemcode") = dt.Rows(0)("ArticleCode").ToString()
                    DgRecordProductionGrid.Rows(DgRecordProductionGrid.Rows.Count - 1)("qty") = Val(dt.Rows(0)("Qty"))
                    sender.Text = String.Empty
                    sender.Focus()
                    sender.Select()
                    If (DgRecordProductionGrid.Rows.Count > 1) Then
                        DgRecordProductionGrid.Select(DgRecordProductionGrid.Rows.Count - 1, 2)
                    End If
                End If
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM020"), "CM020 - " & getValueByKey("CLAE05"))
            'Error in searcing Article details
            LogException(ex)

        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub
    ''' <summary>
    ''' Message will be displayed on cancel button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            If MsgBox(getValueByKey("RCP0001"), MsgBoxStyle.OkCancel, "RCP0001 - " & getValueByKey("CLAE04")) = MsgBoxResult.Ok Then
                'All the data will be lost, are you sure you want to continue"
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
                Me.Dispose()
            End If
        Catch ex As Exception
            ShowMessage(False, getValueByKey("CLAE05"))
            'Error
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Code for removing items data from grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DgRecordProductionGrid_CellButtonClick(sender As Object, e As RowColEventArgs) Handles DgRecordProductionGrid.CellButtonClick
        Try

            DgRecordProductionGrid.Rows.Remove(DgRecordProductionGrid.Row)
            If (DgRecordProductionGrid.Rows.Count > 1) Then
                DgRecordProductionGrid.Select(DgRecordProductionGrid.Rows.Count - 1, 2)
            End If
            txtSearch.Focus()
            txtSearch.Select()

        Catch ex As Exception
            ShowMessage(False, getValueByKey("CLAE05"))
            'Error
            LogException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Validations for Quantity if Quantity less than 0
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DgRecordProductionGrid_AfterEdit(sender As Object, e As RowColEventArgs) Handles DgRecordProductionGrid.AfterEdit
        If DgRecordProductionGrid.Row = -1 Then Exit Sub
        Dim currentrow = DgRecordProductionGrid.Row
        Dim currentcell = e.Col

        If DgRecordProductionGrid.Cols(currentcell).Name = "qty" Then
            Try
                Dim currentqty As Double = IIf(DgRecordProductionGrid.Item(currentrow, "qty") Is DBNull.Value, 0, DgRecordProductionGrid.Item(currentrow, "qty"))
                If currentqty < 0 Then
                    ShowMessage(getValueByKey("RCP0002"), "RCP0002 - " & getValueByKey("CLAE04"))
                    ' ShowMessage("Quantity Cannot be less than 0", " " & "Record Production Screen Information")
                    DgRecordProductionGrid.Item(currentrow, "qty") = 0
                    Exit Sub
                End If

                If Val(DgRecordProductionGrid.Item(currentrow, "qty")) > 999999999 Then
                    ShowMessage(getValueByKey("CM059"), "CM059 - " & getValueByKey("CLAE04"))
                    'CM059() " Qty cannot be greater then 999999999
                    DgRecordProductionGrid.Item(currentrow, "qty") = 0
                    e.Cancel = True
                End If
            Catch ex As Exception
                LogException(ex)
                ShowMessage(False, getValueByKey("CLAE05"))
                'Error
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Adding items data to grid 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim strArticle As String = ""
            Dim SlapperName = txtSlapperName.Text
            Dim Ean As String = ""
            Dim dt As New DataTable

            If txtSearch.Text = String.Empty Then
                ShowMessage(getValueByKey("RCP0004"), "RCP0004 - " & getValueByKey("CLAE04"))
                ' ShowMessage("Please select an article", "Item Msg")
                Exit Sub
            End If
            dt = objrp.GetAllItems(clsAdmin.SiteCode, txtSearch.Text.Trim, clsAdmin.LangCode)
            If dt.Rows.Count = 0 Then
                'MsgBox("Please select valid article ")
                ShowMessage(getValueByKey("RCP0009"), "RCP0009 - " & getValueByKey("CLAE04"))
                txtSearch.Text = String.Empty
                Exit Sub
            End If
            If DgRecordProductionGrid.Rows.Count > 1 Then
                For index = 1 To DgRecordProductionGrid.Rows.Count - 1

                    If dt.Rows(0)("ArticleCode").ToString() = DgRecordProductionGrid.Rows(index)("itemcode") Then
                        ShowMessage(getValueByKey("CLIST06"), "CLIST06 - " & getValueByKey("CLIST06"))
                        txtSearch.Text = String.Empty
                        'Article is Already Added
                        Exit Sub
                    End If
                Next
            End If

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                DgRecordProductionGrid.Rows.Add()

                DgRecordProductionGrid.Rows(DgRecordProductionGrid.Rows.Count - 1)("Selects") = ""
                If clsDefaultConfiguration.PrintItemFullName Then
                    DgRecordProductionGrid.Rows(DgRecordProductionGrid.Rows.Count - 1)("itemdesc") = dt.Rows(0)("ArticleName")
                Else
                    DgRecordProductionGrid.Rows(DgRecordProductionGrid.Rows.Count - 1)("itemdesc") = dt.Rows(0)("ArticleShortName")
                End If
                DgRecordProductionGrid.Rows(DgRecordProductionGrid.Rows.Count - 1)("itemcode") = dt.Rows(0)("ArticleCode").ToString()
                DgRecordProductionGrid.Rows(DgRecordProductionGrid.Rows.Count - 1)("qty") = Val(dt.Rows(0)("Qty"))

                txtSearch.Text = String.Empty
                txtSearch.Focus()
                txtSearch.Select()
                If (DgRecordProductionGrid.Rows.Count > 1) Then
                    DgRecordProductionGrid.Select(DgRecordProductionGrid.Rows.Count - 1, 2)
                End If
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM020"), "CM020 - " & getValueByKey("CLAE05"))
            'Error in searcing Article details
            LogException(ex)
        Finally
            txtSearch.Focus()
            txtSearch.Select()
        End Try
    End Sub
    ''' <summary>
    ''' Fetching all Data of grid to datatable 
    ''' Saving all the data to database 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnProduce_Click(sender As Object, e As EventArgs) Handles btnProduce.Click
        Try
            Dim productionid As String = String.Empty
            Dim Remarks As String = txtRemarks.Text
            Dim SlapperName As String = txtSlapperName.Text
            If DgRecordProductionGrid.Rows.Count = 1 Then
                ShowMessage(getValueByKey("RCP0010"), "RCP0010 - " & getValueByKey("CLAE04"))
                txtSearch.Focus()
                'ShowMessage("Please select atleast one item", "Item Msg")
                Exit Sub
            End If
            If SlapperName = String.Empty Then
                ShowMessage(getValueByKey("RCP0008"), "RCP0008 - " & getValueByKey("CLAE04"))
                ' ShowMessage("Slapper Name is Mandatory", "")
                txtSlapperName.Focus()
                ' txtSlapperName.Select()
                Exit Sub
            End If
            Dim dtRecordProduction As New DataTable
            dtRecordProduction = objrp.GetItemSchemaInfoForRecordProduction()
            dtRecordProduction.Rows.Clear()
            For index As Integer = 1 To DgRecordProductionGrid.Rows.Count - 1

                If DgRecordProductionGrid.Item(index, "qty") = 0 Then
                    ShowMessage(getValueByKey("RCP0007"), "RCP0007 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Please Update Quantity", " " & "Record Production Screen Information")
                    Exit Sub
                End If
                Dim newRow As DataRow = dtRecordProduction.NewRow()
                newRow("ItemCode") = DgRecordProductionGrid.Rows(index)("itemcode")
                newRow("Description") = DgRecordProductionGrid.Rows(index)("itemdesc")
                newRow("Quantity") = DgRecordProductionGrid.Rows(index)("qty")
                newRow("Remarks") = DgRecordProductionGrid.Rows(index)("remarks")
                dtRecordProduction.Rows.Add(newRow)
            Next

            objrp.InsertRecordProductionData(productionid, Remarks, SlapperName, clsAdmin.SiteCode, clsAdmin.Financialyear, clsAdmin.UserCode, clsAdmin.TerminalID, dtRecordProduction)
            Me.DialogResult = Windows.Forms.DialogResult.OK
            ShowMessage(getValueByKey("RCP0006"), "RCP0006 - " & getValueByKey("CLAE04"))
            'ShowMessage("Production recorded Sucessfully", "Item Msg")
            Me.Close()

        Catch ex As Exception
            ShowMessage(False, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub txtSlapperName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSlapperName.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub Timer_DateTime_Tick(sender As Object, e As EventArgs) Handles Timer_DateTime.Tick
        lblDateValue.Text = DateTime.Now.ToString("g")
    End Sub
#Region "Function"
    ''' <summary>
    ''' Grid Settings 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GridSettings()
        Try
            DgRecordProductionGrid.Cols("Selects").Caption = " "
            DgRecordProductionGrid.Cols("Selects").Width = 18
            DgRecordProductionGrid.Cols("Selects").ComboList = "..."

            DgRecordProductionGrid.Cols("itemcode").Width = 145
            DgRecordProductionGrid.Cols("itemcode").Caption = getValueByKey("frmRecordProduction.DgRecordProductionGrid.itemcode") 'Article Code
            DgRecordProductionGrid.Cols("itemcode").AllowEditing = False
            DgRecordProductionGrid.Cols("itemcode").TextAlign = TextAlignEnum.LeftCenter

            DgRecordProductionGrid.Cols("itemdesc").Width = 250
            DgRecordProductionGrid.Cols("itemdesc").Caption = getValueByKey("frmRecordProduction.DgRecordProductionGrid.itemdesc") 'Article Description
            DgRecordProductionGrid.Cols("itemdesc").AllowEditing = False
            DgRecordProductionGrid.Cols("itemdesc").TextAlign = TextAlignEnum.LeftCenter

            DgRecordProductionGrid.Cols("qty").Width = 130
            DgRecordProductionGrid.Cols("qty").Caption = getValueByKey("frmRecordProduction.DgRecordProductionGrid.qty") 'Quantity
            DgRecordProductionGrid.Cols("qty").DataType = Type.GetType("System.Decimal")
            DgRecordProductionGrid.Cols("qty").Format = "0.000"
            DgRecordProductionGrid.Cols("qty").TextAlign = TextAlignEnum.RightCenter

            DgRecordProductionGrid.Cols("Remarks").Width = 180
            DgRecordProductionGrid.Cols("Remarks").Caption = getValueByKey("frmRecordProduction.DgRecordProductionGrid.remarks") 'Remark
            DgRecordProductionGrid.Cols("Remarks").AllowEditing = True
            DgRecordProductionGrid.Cols("Remarks").TextAlign = TextAlignEnum.LeftCenter
            DgRecordProductionGrid.AllowResizing = False

        Catch ex As Exception
            ShowMessage(getValueByKey("CM006"), "CM006 - " & getValueByKey("CLAE05"))
            'Article details screen not initialized
            LogException(ex)

        End Try
    End Sub

    Private Function Themechange()
        Me.Size = New Size(847, 410)
        Me.BackColor = Color.FromArgb(134, 134, 134)
        lblSlapperName.ForeColor = Color.Black
        lblSlapperName.BackColor = Color.FromArgb(212, 212, 212)
        lblSlapperName.AutoSize = False
        lblSlapperName.Size = New Size(178, 21)
        lblSlapperName.SendToBack()
        lblSlapperName.TextAlign = ContentAlignment.MiddleLeft
        ' lblSlapperName.Text = lblSlapperName.Text.ToUpper

        lblAddItems.ForeColor = Color.Black
        lblAddItems.BackColor = Color.FromArgb(212, 212, 212)
        lblAddItems.AutoSize = False
        lblAddItems.Size = New Size(178, 21)
        lblAddItems.SendToBack()
        lblAddItems.TextAlign = ContentAlignment.MiddleLeft
        ' lblAddItems.Text = lblAddItems.Text

        lblDate.ForeColor = Color.White

        lblDateValue.ForeColor = Color.White


        DgRecordProductionGrid.VisualStyle = VisualStyle.Custom
        DgRecordProductionGrid.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        DgRecordProductionGrid.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        'dgReprint.Rows.MinSize = 25
        DgRecordProductionGrid.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        DgRecordProductionGrid.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        DgRecordProductionGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        DgRecordProductionGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        'dgReprint.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        DgRecordProductionGrid.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        btnProduce.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnProduce.BackColor = Color.Transparent
        btnProduce.BackColor = Color.FromArgb(0, 107, 163)
        btnProduce.ForeColor = Color.FromArgb(255, 255, 255)
        btnProduce.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnProduce.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnProduce.FlatStyle = FlatStyle.Flat
        btnProduce.FlatAppearance.BorderSize = 0
        btnProduce.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnProduce.Size = New Size(85, 30)

        btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnCancel.BackColor = Color.Transparent
        btnCancel.BackColor = Color.FromArgb(0, 107, 163)
        btnCancel.ForeColor = Color.FromArgb(255, 255, 255)
        btnCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnCancel.Size = New Size(85, 30)

        btnAdd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnAdd.BackColor = Color.Transparent
        btnAdd.BackColor = Color.FromArgb(0, 107, 163)
        btnAdd.ForeColor = Color.FromArgb(255, 255, 255)
        btnAdd.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnAdd.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnAdd.FlatStyle = FlatStyle.Flat
        btnAdd.FlatAppearance.BorderSize = 0
        btnAdd.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnAdd.Size = New Size(85, 30)
       
    End Function
#End Region



End Class