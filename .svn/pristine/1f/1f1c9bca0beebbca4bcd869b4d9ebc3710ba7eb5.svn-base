Imports SpectrumBL
Imports C1.Win.C1FlexGrid
Public Class frmCFormNumber

#Region "Global Variables"
    Dim objCforms As New clsCForms
    Dim dsCustCForm As New DataSet
#End Region

#Region "Properties"

    Private _SOList As List(Of String)
    Public Property SOList() As List(Of String)
        Get
            Return _SOList
        End Get
        Set(ByVal value As List(Of String))
            _SOList = value
        End Set
    End Property

    Private _IsEdit As Boolean = False
    Public Property IsEdit() As Boolean
        Get
            Return _IsEdit
        End Get
        Set(ByVal value As Boolean)
            _IsEdit = value
        End Set
    End Property

    Private _SalesOrderNumber As String
    Public Property SalesOrderNumber() As String
        Get
            Return _SalesOrderNumber
        End Get
        Set(ByVal value As String)
            _SalesOrderNumber = value
        End Set
    End Property


    Private _SOInvDate As DateTime
    Public Property SOInvDate() As DateTime
        Get
            Return _SOInvDate
        End Get
        Set(ByVal value As DateTime)
            _SOInvDate = value
        End Set
    End Property

    Private _SOInvDateList As List(Of DateTime)
    Public Property SOInvDateList() As List(Of DateTime)
        Get
            Return _SOInvDateList
        End Get
        Set(ByVal value As List(Of DateTime))
            _SOInvDateList = value
        End Set
    End Property



    Private _CformNo As String
    Public Property CformNo() As String
        Get
            Return _CformNo
        End Get
        Set(ByVal value As String)
            _CformNo = value
        End Set
    End Property

    Private _CustomerNo As String
    Public Property CustomerNo() As String
        Get
            Return _CustomerNo
        End Get
        Set(ByVal value As String)
            _CustomerNo = value
        End Set
    End Property

    Private _CformDate As DateTime
    Public Property CformDate() As DateTime
        Get
            Return _CformDate
        End Get
        Set(ByVal value As DateTime)
            _CformDate = value
        End Set
    End Property

    Private _IsRemark As Boolean = False
    Public Property IsRemark() As Boolean
        Get
            Return _IsRemark
        End Get
        Set(ByVal value As Boolean)
            _IsRemark = value
        End Set
    End Property
#End Region

#Region "Events"

    ''' <summary>
    ''' Fetching Data of Customer Cform and depending on flag Displaying Content
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCFormNumber_Load(sender As Object, e As EventArgs) Handles Me.Load
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
        End If
        Me.Dock = DockStyle.None
        If IsRemark Then
            txtCformNumber.MaxLength = 2000
        Else
            txtCformNumber.MaxLength = 20
        End If
        txtCformNumber.Text = CformNo
        txtCformNumber.Focus()
        dsCustCForm = objCforms.GetCustCForms()
        If IsRemark = True Then
            lblFromDate.Text = "Enter Remarks"
            lblCformReceivedDate.Visible = False
            dtpReceivedDate.Visible = False
        Else
            If IsEdit Then
                dtpReceivedDate.Value = CformDate
            Else
                dtpReceivedDate.Value = DateTime.Now
            End If
        End If
    End Sub

    ''' <summary>
    ''' Checking on Flag Basis for Updating Remark or Adding Cform Entries.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click

        txtCformNumber.Text = txtCformNumber.Text.Trim()
        If IsRemark Then
            If String.IsNullOrEmpty(txtCformNumber.Text) Then
                ShowMessage(" Remark Can Not Be Blank", getValueByKey("CLAE04"))
                Exit Sub
            End If
        Else
            If dtpReceivedDate.Value > DateTime.Now Then
                ShowMessage("C-Form Received Date Can Not Be Future Date", getValueByKey("CLAE04"))
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtCformNumber.Text) Then
                ShowMessage(" C-Form Number Can Not Be Blank", getValueByKey("CLAE04"))
                Exit Sub
            End If
            Dim objCustm As New clsCLPCustomer
            Dim CurrentState As String = ""
            Dim PreviousState As String = ""
            Dim dtcustinfo = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, CustomerNo)
            If dtcustinfo.Rows.Count > 0 Then
                CurrentState = dtcustinfo.Rows(0)("StateCode")
            End If
            For Each DrFoundCformNo As DataRow In dsCustCForm.Tables("CustomerCForm").Select("CFormNo='" & txtCformNumber.Text & "'")
                Dim PreviousCustomer = DrFoundCformNo("CustomerNo")
                Dim dtcustinfos = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, PreviousCustomer)
                If dtcustinfos.Rows.Count > 0 Then
                    If IsDBNull(dtcustinfos.Rows(0)("StateCode")) = False Then
                        PreviousState = dtcustinfos.Rows(0)("StateCode")
                    Else
                        PreviousState = ""
                    End If
                    'PreviousState =IsDBNull (
                End If
                If String.Compare(PreviousState, CurrentState) = 0 Then
                    ShowMessage(" Same C-Form Number already saved ", getValueByKey("CLAE04"))
                    Exit Sub
                End If

            Next
        End If

        If Not IsRemark Then
            dsCustCForm = AddCustomerCFormDataIntoDataTable(txtCformNumber.Text, IIf(dtpReceivedDate.Value Is DBNull.Value, Nothing, dtpReceivedDate.Value))
        Else
            dsCustCForm = PrepareDataForUpdatingRemark(txtCformNumber.Text)
        End If
        If objCforms.UpdateCFormDetails(dsCustCForm, IIf(IsEdit = True OrElse IsRemark = True, True, False)) Then
            If Not IsEdit Then
                If Not IsRemark Then
                    'The C-form Details have been updated successfully
                    ShowMessage(getValueByKey("CF0002"), "CF0002 - " & getValueByKey("CLAE04"))
                Else
                    ' ShowMessage("Cform Remarks Updated Successfully", "")
                End If
            Else
                'The C-form Details have been updated successfully
                ShowMessage(getValueByKey("CF0002"), "CF0002 - " & getValueByKey("CLAE04"))
            End If
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Validation for Checking if single quote is there should not edit.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtCformNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCformNumber.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

#End Region

#Region "Functions"

    ''' <summary>
    ''' Adding Data into Dataset for Adding and Updating Cform Entries
    ''' </summary>
    ''' <param name="CFormNo"></param>
    ''' <param name="CFormReceivedDate"></param>
    ''' <returns>Dataset</returns>
    ''' <remarks></remarks>
    Private Function AddCustomerCFormDataIntoDataTable(ByVal CFormNo As String, ByVal CFormReceivedDate As DateTime) As DataSet
        Dim ObjclsCommon As New clsCommon
        Dim ServerDate = ObjclsCommon.GetCurrentDate()
        Dim CustomerNo As String = objCforms.GetCustomerNo(SOList(0).ToString, SOInvDateList(0), clsAdmin.SiteCode).ToString
        Try
            If Not IsEdit Then
                Dim objCM As New clsCashMemo
                Dim objType = "FO_DOC"
                Dim docno As String = objCM.getDocumentNo("CustCform", clsAdmin.SiteCode, objType)
                Dim Id = GenDocNo("CF" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                Dim drCustCForm As DataRow = dsCustCForm.Tables(0).NewRow()
                drCustCForm("CustomerCformId") = Id
                drCustCForm("CformNo") = CFormNo
                drCustCForm("CFormDate") = CFormReceivedDate
                drCustCForm("CustomerNo") = CustomerNo
                drCustCForm("SiteCode") = clsAdmin.SiteCode
                drCustCForm("CreatedAt") = clsAdmin.SiteCode
                drCustCForm("CreatedBy") = clsAdmin.UserCode
                drCustCForm("CreatedOn") = ServerDate
                drCustCForm("UpdatedAt") = clsAdmin.SiteCode
                drCustCForm("UpdatedBy") = clsAdmin.UserCode
                drCustCForm("UpdatedOn") = ServerDate
                drCustCForm("Status") = True
                dsCustCForm.Tables(0).Rows.Add(drCustCForm)
                For i = 0 To SOList.Count - 1
                    Dim MaxBilllineno As Integer = 0
                    Dim drCustformdtls As DataRow = dsCustCForm.Tables(1).NewRow()
                    drCustformdtls("CustomerCformId") = Id
                    drCustformdtls("InvoiceNo") = SOList(i)
                    drCustformdtls("CreatedAt") = clsAdmin.SiteCode
                    drCustformdtls("CreatedBy") = clsAdmin.UserCode
                    drCustformdtls("CreatedOn") = ServerDate
                    drCustformdtls("UpdatedAt") = clsAdmin.SiteCode
                    drCustformdtls("UpdatedBy") = clsAdmin.UserCode
                    drCustformdtls("UpdatedOn") = ServerDate
                    drCustformdtls("Status") = True
                    If IsDBNull(dsCustCForm.Tables(1).Compute("Max(CustCformDtlsId)", "")) Then
                        MaxBilllineno = 0
                    Else
                        MaxBilllineno = dsCustCForm.Tables(1).Compute("Max(CustCformDtlsId)", "")
                    End If
                    drCustformdtls("CustCformDtlsId") = MaxBilllineno + 1
                    drCustformdtls("InvoiceDate") = SOInvDateList(i)
                    dsCustCForm.Tables(1).Rows.Add(drCustformdtls)
                Next
            Else
                Dim drcust As DataRow
                For i = 0 To SOList.Count - 1
                    Dim CustomerNoedit As String = objCforms.GetCustomerNo(SOList(i).ToString, SOInvDateList(i), clsAdmin.SiteCode).ToString
                    Dim drTbl1() = dsCustCForm.Tables(1).Select("InvoiceNo='" & SOList(i) & "' AND InvoiceDate='" & SOInvDateList(i) & "'")
                    Dim drCdtls() = dsCustCForm.Tables(0).Select("CustomerNo='" & CustomerNoedit & "' and CustomerCformId='" & drTbl1(0)("CustomerCformId") & "'")
                    Dim find(1) As Object
                    Dim id = ""
                    If drCdtls.Count > 0 Then
                        id = drCdtls(0)("CustomerCformId")
                    End If
                    find(0) = id
                    find(1) = CustomerNoedit
                    drcust = dsCustCForm.Tables(0).Rows.Find(find)
                    If Not drcust Is Nothing Then
                        drcust("CustomerCformId") = id
                        drcust("CformNo") = CFormNo
                        drcust("CFormDate") = CFormReceivedDate
                        drcust("CustomerNo") = CustomerNoedit
                        drcust("UpdatedAt") = clsAdmin.SiteCode
                        drcust("UpdatedBy") = clsAdmin.UserCode
                        drcust("UpdatedOn") = ServerDate
                        drcust("Status") = True
                    End If
                    Dim drcustdtls As DataRow
                    Dim finddtls(2) As Object
                    finddtls(0) = id
                    finddtls(1) = SOList(i)
                    finddtls(2) = SOInvDateList(i)
                    drcustdtls = dsCustCForm.Tables(1).Rows.Find(finddtls)
                    If Not drcustdtls Is Nothing Then
                        drcustdtls("CustomerCformId") = id
                        drcustdtls("InvoiceNo") = SOList(i)
                        drcustdtls("CreatedAt") = clsAdmin.SiteCode
                        drcustdtls("CreatedBy") = clsAdmin.UserCode
                        drcustdtls("CreatedOn") = ServerDate
                        drcustdtls("UpdatedAt") = clsAdmin.SiteCode
                        drcustdtls("UpdatedBy") = clsAdmin.UserCode
                        drcustdtls("UpdatedOn") = ServerDate
                        drcustdtls("Status") = True
                    End If
                Next

            End If
            Return dsCustCForm
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Preparing Dataset for Updating Remark
    ''' </summary>
    ''' <param name="Remark"></param>
    ''' <returns>Dataset</returns>
    ''' <remarks></remarks>
    Private Function PrepareDataForUpdatingRemark(ByVal Remark As String) As DataSet
        Dim ObjclsCommon As New clsCommon
        Dim ServerDate = ObjclsCommon.GetCurrentDate()
        Dim CustomerNo As String = objCforms.GetCustomerNo(SOList(0).ToString, SOInvDateList(0), clsAdmin.SiteCode).ToString
        Dim id As String = ""
        Try
            Dim drcustdtls As DataRow
            Dim finddtls(2) As Object
            For i = 0 To SOList.Count - 1
                Dim CustNo() As DataRow = dsCustCForm.Tables(1).Select("InvoiceNo='" & SOList(i) & "' and InvoiceDate='" & SOInvDateList(i) & "'")
                If CustNo.Count > 0 Then
                    id = CustNo(0)("CustomerCformId")
                End If
                finddtls(0) = id
                finddtls(1) = SOList(i)
                finddtls(2) = SOInvDateList(i)
                drcustdtls = dsCustCForm.Tables(1).Rows.Find(finddtls)
                If Not drcustdtls Is Nothing Then
                    drcustdtls("Remarks") = Remark
                    drcustdtls("UpdatedAt") = clsAdmin.SiteCode
                    drcustdtls("UpdatedBy") = clsAdmin.UserCode
                    drcustdtls("UpdatedOn") = ServerDate
                End If
            Next
            Return dsCustCForm
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        btnOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnOk.BackColor = Color.Transparent
        btnOk.BackColor = Color.FromArgb(0, 107, 163)
        btnOk.ForeColor = Color.FromArgb(255, 255, 255)
        btnOk.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        btnOk.TextAlign = ContentAlignment.MiddleCenter
        btnOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnOk.FlatStyle = FlatStyle.Flat
        btnOk.FlatAppearance.BorderSize = 0
        btnOk.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        ' btnOk.Size = New Size(85, 30)

        lblFromDate.ForeColor = Color.White
        lblFromDate.AutoSize = False
        'lblFromDate.Size = New Size(150, 20)
        lblFromDate.BorderStyle = BorderStyle.None

        lblCformReceivedDate.ForeColor = Color.White
        lblCformReceivedDate.AutoSize = False
        'lblCformReceivedDate.Size = New Size(150, 20)
        lblCformReceivedDate.BorderStyle = BorderStyle.None

    End Function
#End Region

End Class