Imports SpectrumBL
Imports SpectrumPrint
Imports SpectrumCommon
Public Class frmPendingCForms
#Region "Global Variables"
    Dim objCforms As New clsCForms
    Public SOList As New List(Of String)
    Public InvDateList As New List(Of DateTime)
    Dim dsCustCForm As New DataSet
#End Region

#Region "Properties"
    Private _IsEdit As Boolean = False
    Public Property IsEdit() As Boolean
        Get
            Return _IsEdit
        End Get
        Set(ByVal value As Boolean)
            _IsEdit = value
        End Set
    End Property

#End Region

#Region "Functions"

    ''' <summary>
    ''' Fetch the Data of SO Invoice List from Database and Bind to Grid 
    ''' </summary>
    ''' <param name="CustomerName"></param>
    ''' <param name="FromDate"></param>
    ''' <param name="ToDate"></param>
    ''' <param name="State"></param>
    ''' <remarks></remarks>
    Private Sub BindCForms(Optional ByVal CustomerName As String = Nothing, Optional ByVal FromDate As DateTime = Nothing, Optional ByVal ToDate As DateTime = Nothing, Optional ByVal State As String = Nothing)
        Try
            Dim dtCForms = objCforms.GetSOListCForms(clsAdmin.SiteCode, IsEdit, CustomerName, FromDate, ToDate, State)
            If dtCForms IsNot Nothing Then
                DgPendingCForm.DataSource = dtCForms.DefaultView
                GridColumnSettings(IsEdit)
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Grid Setting and Column Display depending on Flag
    ''' </summary>
    ''' <param name="IsEdit"></param>
    ''' <remarks></remarks>
    Private Sub GridColumnSettings(ByVal IsEdit As Boolean)
        Try
            DgPendingCForm.AllowEditing = True
            If DgPendingCForm.Cols.Count > 0 Then
                Dim displayColumns As String = "Sel,InvoiceNo,InvoiceDate,CustomerName,Amount,Description"
                If IsEdit = True Then
                    displayColumns = displayColumns & ",CFormNo,CFormDate,Remarks"
                End If
                Dim columnsList = displayColumns.ToUpper().Split(",")

                For colIndex = 0 To DgPendingCForm.Cols.Count - 1 Step 1
                    If columnsList.Contains(DgPendingCForm.Cols(colIndex).Name.ToUpper()) Then
                        DgPendingCForm.Cols(colIndex).Visible = True
                    Else
                        DgPendingCForm.Cols(colIndex).Visible = False
                    End If
                    DgPendingCForm.Cols(colIndex).AllowEditing = False
                Next
                DgPendingCForm.Cols("Sel").DataType = Type.GetType("System.Boolean")
                DgPendingCForm.Cols("Sel").Caption = ""
                DgPendingCForm.Cols("Sel").AllowEditing = True
                DgPendingCForm.Cols("InvoiceNo").Caption = "Invoice no."
                DgPendingCForm.Cols("InvoiceDate").Caption = "Invoice date"
                DgPendingCForm.Cols("CustomerName").Caption = "Customer Name"
                DgPendingCForm.Cols("Amount").Caption = "Amount"
                DgPendingCForm.Cols("Description").Caption = "State"
                If IsEdit = True Then
                    DgPendingCForm.Cols("CFormNo").Caption = "C-form Number"
                    DgPendingCForm.Cols("CFormDate").Caption = "C-form date"
                    DgPendingCForm.Cols("Remarks").Caption = "Remarks"
                    DgPendingCForm.Cols("Sel").Width = 30
                    DgPendingCForm.Cols("InvoiceNo").Width = 130
                    DgPendingCForm.Cols("InvoiceDate").Width = 120
                    DgPendingCForm.Cols("CustomerName").Width = 160
                    DgPendingCForm.Cols("Amount").Width = 100
                    DgPendingCForm.Cols("cformno").Width = 125
                    DgPendingCForm.Cols("cformdate").Width = 125
                    DgPendingCForm.Cols("Description").Width = 160
                    DgPendingCForm.Cols("Remarks").Width = 200
                Else
                    DgPendingCForm.Cols("Sel").Width = 40
                    DgPendingCForm.Cols("InvoiceNo").Width = 150
                    DgPendingCForm.Cols("InvoiceDate").Width = 142
                    DgPendingCForm.Cols("CustomerName").Width = 230
                    DgPendingCForm.Cols("Amount").Width = 140
                    DgPendingCForm.Cols("Description").Width = 230
                End If
                Me.DgPendingCForm.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
                Me.DgPendingCForm.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                For i = 0 To DgPendingCForm.Cols.Count - 1
                    DgPendingCForm.Cols(i).Caption = DgPendingCForm.Cols(i).Caption.ToUpper
                Next
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Preparing Dataset for Making the Status as False of Selected Sales Invoice No.
    ''' </summary>
    ''' <returns>Dataset</returns>
    ''' <remarks></remarks>
    Private Function PrepareDataForSave() As DataSet
        Dim ObjclsCommon As New clsCommon
        Dim ServerDate = ObjclsCommon.GetCurrentDate()

        Dim id As String
        Try
            Dim drcustdtls As DataRow
            Dim finddtls(2) As Object
            For i = 0 To SOList.Count - 1
                Dim CustomerNo As String = objCforms.GetCustomerNo(SOList(i).ToString, InvDateList(i), clsAdmin.SiteCode).ToString
                Dim CustNo() As DataRow = dsCustCForm.Tables(1).Select("InvoiceNo='" & SOList(i) & "' AND InvoiceDate ='" & InvDateList(i) & "' and Status=True")
                If CustNo.Count > 0 Then
                    id = CustNo(0)("CustomerCformId")
                End If
                finddtls(0) = id
                finddtls(1) = SOList(i)
                finddtls(2) = InvDateList(i)
                drcustdtls = dsCustCForm.Tables(1).Rows.Find(finddtls)
                If Not drcustdtls Is Nothing Then
                    drcustdtls("UpdatedAt") = clsAdmin.SiteCode
                    drcustdtls("UpdatedBy") = clsAdmin.UserCode
                    drcustdtls("UpdatedOn") = ServerDate
                    drcustdtls("Status") = False
                End If
                Dim drcust As DataRow
                Dim find(1) As Object
                find(0) = id
                find(1) = CustomerNo
                drcust = dsCustCForm.Tables(0).Rows.Find(find)
                If Not drcust Is Nothing Then
                    drcust("UpdatedAt") = clsAdmin.SiteCode
                    drcust("UpdatedBy") = clsAdmin.UserCode
                    drcust("UpdatedOn") = ServerDate
                    Dim drStatus() As DataRow = dsCustCForm.Tables(1).Select("STATUS=True and CustomerCformid='" & id & "' ")
                    If drStatus.Count = 0 Then
                        drcust("Status") = False
                    End If
                End If
            Next
            Return dsCustCForm
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


#End Region

#Region "Events"

    ''' <summary>
    '''It Will Open New Form where we add or Update CForm Number and CForm Date on when it was received depending on flag
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrlBtnCFormNumber_Click(sender As Object, e As EventArgs) Handles CtrlBtnCFormNumber.Click
        Try
            Using objCformNumber As New frmCFormNumber
                SOList.Clear()
                InvDateList.Clear()
                objCformNumber.IsEdit = IsEdit
                Dim objlist As New List(Of frmCFormNumber)
                If DgPendingCForm.Row > 0 Then
                    objCformNumber.CustomerNo = DgPendingCForm.Rows(DgPendingCForm.Row)("CustomerNo").ToString
                    For i = 1 To DgPendingCForm.Rows.Count - 1
                        If DgPendingCForm.Rows(i)("Sel") = True Then
                            objCformNumber.SalesOrderNumber = DgPendingCForm.Rows(i)("InvoiceNo").ToString()
                            objCformNumber.SOInvDate = DgPendingCForm.Rows(i)("InvoiceDate").ToString()
                            SOList.Add(objCformNumber.SalesOrderNumber)
                            InvDateList.Add(objCformNumber.SOInvDate)
                        End If
                    Next
                    If SOList.Count = 0 Then
                        '   Please Select atleast one Invoice
                        ShowMessage(getValueByKey("CF0005"), "CF0005 - " & getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                    objCformNumber.SOList = SOList
                    objCformNumber.SOInvDateList = InvDateList
                    If IsEdit = True Then
                        Dim dsCustCFormDtls = dsCustCForm
                        Dim Dr() = dsCustCFormDtls.Tables(1).Select("InvoiceNo ='" & SOList(0) & "' and InvoiceDate='" & InvDateList(0) & "'")
                        If Dr.Count > 0 Then
                            Dim DrCformdtls = dsCustCFormDtls.Tables(0).Select("CustomerCformId='" & Dr(0)("CustomerCformId") & "'and Status=True")
                            If DrCformdtls.Count > 0 Then
                                objCformNumber.CformNo = DrCformdtls(0)("CformNo")
                                objCformNumber.CformDate = DrCformdtls(0)("CformDate")
                            End If
                        End If
                    End If
                    Dim dialogResult = objCformNumber.ShowDialog()
                    If (dialogResult = Windows.Forms.DialogResult.Cancel) Then
                        Exit Sub
                    ElseIf (dialogResult = Windows.Forms.DialogResult.OK) Then
                        frmPendingCForms_Load(Nothing, EventArgs.Empty)
                    End If
                End If
            End Using
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load the Data and Populate Customers and States in ComboBox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmPendingCForms_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                ThemeChange()
            End If
            dsCustCForm = objCforms.GetCustCForms()
            Me.Dock = DockStyle.None
            '---Populating All Customers in ComboBox
            Dim dtItemData = objCforms.GetAllCustomers(clsAdmin.SiteCode, IsEdit)
            PopulateCustomerCombo(dtItemData, txtSearchCustomer)
            pC1ComboSetDisplayMember(txtSearchCustomer)
            '---Populating All States in ComboBox
            Dim dtStateData = objCforms.GetAllStates()
            PopulateCustomerCombo(dtStateData, CtrlComboState)
            pC1ComboSetDisplayMember(CtrlComboState)
            If IsEdit Then
                Me.Text = "Edit C-form Details"
                CtrlBtnCFormNumber.Text = "Update"
            Else
                CtrlBtnAddRemark.Visible = False
                CtrlBtnCformDelete.Visible = False
            End If
            Call BindCForms()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    '''  Search The SO List of CForms Based on Conditions
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrlBtnSearch_Click(sender As Object, e As EventArgs) Handles CtrlBtnSearch.Click
        Dim fromDate As Date = IIf(dtpFromDate.Value Is DBNull.Value, Nothing, dtpFromDate.Value)
        Dim toDate As Date = IIf(dtpToDate.Value Is DBNull.Value, Nothing, dtpToDate.Value)
        Call BindCForms(txtSearchCustomer.Text, fromDate, toDate, CtrlComboState.Text)
    End Sub

    ''' <summary>
    ''' Delete CustCform Data from Table (Making Status as 0)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrlBtnCformDelete_Click(sender As Object, e As EventArgs) Handles CtrlBtnCformDelete.Click
        SOList.Clear()
        InvDateList.Clear()
        For i = 1 To DgPendingCForm.Rows.Count - 1
            If DgPendingCForm.Rows(i)("Sel") = True Then
                SOList.Add(DgPendingCForm.Rows(i)("InvoiceNo").ToString())
                InvDateList.Add(DgPendingCForm.Rows(i)("InvoiceDate").ToString())
            End If
        Next
        If SOList.Count = 0 Then
            '   Please Select atleast one Invoice
            ShowMessage(getValueByKey("CF0005"), "CF0005 - " & getValueByKey("CLAE04"))
            Exit Sub
        End If
        'Do you really want to delete the C-form details for the selected Sales order/s?
        If (MsgBox(getValueByKey("CF0004"), MsgBoxStyle.YesNo, "CF0004 - " & getValueByKey("CLAE04")) = MsgBoxResult.No) Then
        Else
            Me.DialogResult = Windows.Forms.DialogResult.Yes
            If SOList.Count > 0 Then
                dsCustCForm = PrepareDataForSave()
                If objCforms.UpdateCFormDetails(dsCustCForm, True) Then
                    'ShowMessage("Cform Number Deleted Successfully", "")
                    frmPendingCForms_Load(Nothing, EventArgs.Empty)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Validations for Checking if Different customers
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DgPendingCForm_AfterEdit(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles DgPendingCForm.AfterEdit
        Dim CurrentCustomer = IIf(DgPendingCForm.Rows(e.Row)("CustomerNo") Is DBNull.Value, Nothing, DgPendingCForm.Rows(e.Row)("CustomerNo"))
        For i = 1 To DgPendingCForm.Rows.Count - 1
            Dim LoopCustomer = IIf(DgPendingCForm.Rows(i)("CustomerNo") Is DBNull.Value, Nothing, DgPendingCForm.Rows(i)("CustomerNo"))
            If DgPendingCForm.Rows(i)("Sel") = True Then
                If LoopCustomer.ToString.Trim <> CurrentCustomer.ToString.Trim AndAlso i <> e.Row Then
                    DgPendingCForm.Rows(e.Row)("Sel") = False
                    '"Multiple Selection Applicable for Same Customers
                    ShowMessage(getValueByKey("CF0006"), "CF0006 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Adding Remark for Particular Invoice
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrlBtnAddRemark_Click(sender As Object, e As EventArgs) Handles CtrlBtnAddRemark.Click
        Try
            Using objCformNumber As New frmCFormNumber
                SOList.Clear()
                InvDateList.Clear()
                objCformNumber.IsRemark = True
                Dim objlist As New List(Of frmCFormNumber)
                If DgPendingCForm.Row > 0 Then
                    For i = 1 To DgPendingCForm.Rows.Count - 1
                        If DgPendingCForm.Rows(i)("Sel") = True Then
                            objCformNumber.SalesOrderNumber = DgPendingCForm.Rows(i)("InvoiceNo").ToString()
                            objCformNumber.SOInvDate = DgPendingCForm.Rows(i)("InvoiceDate").ToString()
                            SOList.Add(objCformNumber.SalesOrderNumber)
                            InvDateList.Add(objCformNumber.SOInvDate)

                        End If
                    Next
                    If SOList.Count = 0 Then
                        ' Please Select atleast one SO
                        ShowMessage(getValueByKey("CF0005"), "CF0005 - " & getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                    objCformNumber.SOList = SOList
                    objCformNumber.SOInvDateList = InvDateList
                    Dim dsCustCFormDtls = dsCustCForm
                    Dim Dr() = dsCustCFormDtls.Tables(1).Select("InvoiceNo ='" & SOList(0) & "' and InvoiceDate='" & InvDateList(0) & "'")
                    If Dr.Count > 0 Then
                        '---Setting Value of Remarks to CformNo Property
                        objCformNumber.CformNo = IIf(Dr(0)("Remarks") Is DBNull.Value, Nothing, Dr(0)("Remarks"))
                    End If

                    Dim dialogResult = objCformNumber.ShowDialog()
                    If (dialogResult = Windows.Forms.DialogResult.Cancel) Then
                        Exit Sub
                    ElseIf (dialogResult = Windows.Forms.DialogResult.OK) Then
                        frmPendingCForms_Load(Nothing, EventArgs.Empty)
                    End If
                End If
            End Using
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        DgPendingCForm.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        DgPendingCForm.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212)
        DgPendingCForm.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        DgPendingCForm.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        DgPendingCForm.Rows.MinSize = 30
        DgPendingCForm.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        DgPendingCForm.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        DgPendingCForm.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        DgPendingCForm.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        DgPendingCForm.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        DgPendingCForm.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        DgPendingCForm.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        DgPendingCForm.Styles.Highlight.ForeColor = Color.Black
        DgPendingCForm.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        DgPendingCForm.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        DgPendingCForm.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        DgPendingCForm.Styles.Highlight.BackColor = Color.FromArgb(177, 227, 253)
        DgPendingCForm.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
        DgPendingCForm.CellButtonImage = Global.Spectrum.My.Resources.Delete
        CtrlBtnAddRemark.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnAddRemark.BackColor = Color.Transparent
        CtrlBtnAddRemark.BackColor = Color.FromArgb(0, 107, 163)
        CtrlBtnAddRemark.ForeColor = Color.FromArgb(255, 255, 255)
        CtrlBtnAddRemark.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlBtnAddRemark.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CtrlBtnAddRemark.TextAlign = ContentAlignment.MiddleCenter
        CtrlBtnAddRemark.FlatStyle = FlatStyle.Flat
        CtrlBtnAddRemark.FlatAppearance.BorderSize = 0
        CtrlBtnAddRemark.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        ' CtrlBtnAddRemark.Size = New Size(85, 30)
        CtrlBtnCformDelete.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnCformDelete.BackColor = Color.Transparent
        CtrlBtnCformDelete.BackColor = Color.FromArgb(0, 107, 163)
        CtrlBtnCformDelete.ForeColor = Color.FromArgb(255, 255, 255)
        '  CtrlBtnCformDelete.Size = New Size(71, 30)
        CtrlBtnCformDelete.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlBtnCformDelete.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CtrlBtnCformDelete.FlatStyle = FlatStyle.Flat
        CtrlBtnCformDelete.FlatAppearance.BorderSize = 0
        CtrlBtnCformDelete.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        CtrlBtnCformDelete.TextAlign = ContentAlignment.MiddleCenter

        CtrlBtnCFormNumber.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnCFormNumber.BackColor = Color.Transparent
        CtrlBtnCFormNumber.BackColor = Color.FromArgb(0, 107, 163)
        CtrlBtnCFormNumber.ForeColor = Color.FromArgb(255, 255, 255)
        'CtrlBtnCFormNumber.Size = New Size(71, 30)
        CtrlBtnCFormNumber.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlBtnCFormNumber.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CtrlBtnCFormNumber.FlatStyle = FlatStyle.Flat
        CtrlBtnCFormNumber.FlatAppearance.BorderSize = 0
        CtrlBtnCFormNumber.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        CtrlBtnCFormNumber.TextAlign = ContentAlignment.MiddleCenter

        CtrlBtnSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnSearch.BackColor = Color.Transparent
        CtrlBtnSearch.BackColor = Color.FromArgb(0, 107, 163)
        CtrlBtnSearch.ForeColor = Color.FromArgb(255, 255, 255)
        ' CtrlBtnSearch.Size = New Size(71, 30)
        CtrlBtnSearch.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlBtnSearch.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CtrlBtnSearch.FlatStyle = FlatStyle.Flat
        CtrlBtnSearch.FlatAppearance.BorderSize = 0
        CtrlBtnSearch.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        lblCustomerName.BackColor = Color.FromArgb(212, 212, 212)
        lblCustomerName.AutoSize = False
        lblCustomerName.Location = New Point(20, 53)
        lblCustomerName.Size = New Size(120, 20)
        lblCustomerName.BorderStyle = BorderStyle.None

        lblFromDate.BackColor = Color.FromArgb(212, 212, 212)
        lblFromDate.AutoSize = False
        lblFromDate.Size = New Size(120, 19)
        lblFromDate.BorderStyle = BorderStyle.None

        lblToDate.BackColor = Color.FromArgb(212, 212, 212)
        lblToDate.AutoSize = False
        lblToDate.Size = New Size(120, 19)
        lblToDate.BorderStyle = BorderStyle.None

        lblState.BackColor = Color.FromArgb(212, 212, 212)
        lblState.Location = New Point(380, 54)
        lblState.AutoSize = False
        lblState.Size = New Size(80, 20)
        lblState.BorderStyle = BorderStyle.None

        CtrlComboState.Size = New Size(202, 18)
        CtrlComboState.Location = New Point(458, 53)
    End Function
#End Region

End Class