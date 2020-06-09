Imports SpectrumBL
Imports SpectrumPrint
Imports System.Web.Extensions
Imports System.IO
Imports System.Net.WebClient
Imports System.Web.Script.Serialization
Imports System
Imports System.Net
Imports System.Text
Imports System.Security.Cryptography
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.Runtime




Public Class FrmOnlinePayment
    Dim isRequestSuccess As String = ""
    Dim jsonResponse As String = ""
#Region "Properties"

    Private _CheckPhonePeError As Boolean = False
    Public Property CheckPhonePeError() As Boolean
        Get
            Return _CheckPhonePeError
        End Get
        Set(ByVal value As Boolean)
            _CheckPhonePeError = value
        End Set
    End Property
    Private _CustName As String
    Public Property CustName() As String
        Get
            Return _CustName
        End Get
        Set(ByVal value As String)
            _CustName = value
        End Set
    End Property

    Private _totalCollectAmt As Decimal 'added by adil 2014
    Public Property TotalCollectAmt() As Decimal
        Get
            Return _totalCollectAmt
        End Get
        Set(ByVal value As Decimal)
            _totalCollectAmt = value
        End Set
    End Property

    Private _totalBillAmt As Decimal 'added by adil 2014
    Public Property TotalBillAmt() As Decimal
        Get
            Return _totalBillAmt
        End Get
        Set(ByVal value As Decimal)
            _totalBillAmt = value
        End Set
    End Property

    Private _Billno As String
    Public Property Billno() As String
        Get
            Return _Billno
        End Get
        Set(ByVal value As String)
            _Billno = value
        End Set
    End Property

    Private _CMBillno As String
    Public Property CMBillno() As String
        Get
            Return _CMBillno
        End Get
        Set(ByVal value As String)
            _CMBillno = value
        End Set
    End Property
    'added by sagar for innvatii

    Dim _DocumentType As String
    Public Property DocumentType() As String
        Get
            Return _DocumentType
        End Get
        Set(ByVal value As String)
            _DocumentType = value
        End Set
    End Property





    Private _IsPaymentSuccess As Boolean
    Public Property IsPaymentSuccess() As Boolean
        Get
            Return _IsPaymentSuccess
        End Get
        Set(ByVal value As Boolean)
            _IsPaymentSuccess = value
        End Set
    End Property
#End Region

#Region "Variables "
    Dim objClsComm As New clsCommon
    Dim objCashMemo As New clsCashMemo
    Dim tran As SqlTransaction
    Dim TransactionID As String
    Dim dtPhonePedtl As New DataTable
    Dim dtcustdtl As New DataTable
    Dim objCustm As New clsCLPCustomer
    Dim CustMobileno As String = ""
#End Region
#Region "PhonePe"

    Private Sub FrmOnlinePayment_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            CtrlBtnCancle_Click(sender, e)
        ElseIf e.KeyCode = Keys.Enter Then
            CtrlBtnRequest_Click(sender, e)
        End If
    End Sub



    'Public Sub txtCollectAmt_keyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
    '    If e.KeyCode = Keys.Enter Then
    '        CtrlBtnRequest_Click(sender, e)
    '    End If
    'End Sub


    Private Sub txtCollectAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCollectAmt.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub frmTenderMode_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Theme()
            txtTotalAmt.Text = TotalBillAmt
            txtTotalAmt.Enabled = False
            txtCollectAmt.Focus()
            CtrlBtnCheck.Enabled = False
            CtrlBtnCancle.Enabled = False
            dtPhonePedtl = objClsComm.GetPhonePePaymentRequestResponseStruct()
            dtPhonePedtl.Clear()
            If Not String.IsNullOrEmpty(CustName) Then
                dtcustdtl = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, CustName, CustomerWisePrice:=clsDefaultConfiguration.customerwisepricemanagement, CustFormat:=clsDefaultConfiguration.DetailedCustomerCreationformat)
                If Not dtcustdtl Is Nothing AndAlso dtcustdtl.Rows.Count > 0 Then
                    CustMobileno = dtcustdtl.Rows(0)("MobileNo").ToString
                End If
            End If
            txtCollectAmt.Text = CustMobileno
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub Theme()

        Me.CtrlBtnCancle.Visible = True
        Me.CtrlBtnCancle.BackColor = Color.FromArgb(0, 107, 163)
        Me.CtrlBtnCancle.ForeColor = Color.FromArgb(255, 255, 255)
        Me.CtrlBtnCancle.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.CtrlBtnCancle.BringToFront()
        Me.CtrlBtnCancle.Image = Nothing
        Me.CtrlBtnCancle.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CtrlBtnCancle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        ' Me.CtrlBtnCancle.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        ' Me.CtrlBtnCancle.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.CtrlBtnCancle.FlatStyle = FlatStyle.Flat
        Me.CtrlBtnCancle.FlatAppearance.BorderSize = 0

        Me.CtrlBtnCheck.Visible = True
        Me.CtrlBtnCheck.BackColor = Color.FromArgb(0, 107, 163)
        Me.CtrlBtnCheck.ForeColor = Color.FromArgb(255, 255, 255)
        Me.CtrlBtnCheck.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.CtrlBtnCheck.BringToFront()
        Me.CtrlBtnCheck.Image = Nothing
        Me.CtrlBtnCheck.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CtrlBtnCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CtrlBtnCheck.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.CtrlBtnCheck.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.CtrlBtnCheck.FlatStyle = FlatStyle.Flat
        Me.CtrlBtnCheck.FlatAppearance.BorderSize = 1

        Me.CtrlBtnRequest.Visible = True
        Me.CtrlBtnRequest.BackColor = Color.FromArgb(0, 107, 163)
        Me.CtrlBtnRequest.ForeColor = Color.FromArgb(255, 255, 255)
        Me.CtrlBtnRequest.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.CtrlBtnRequest.BringToFront()
        Me.CtrlBtnRequest.Image = Nothing
        Me.CtrlBtnRequest.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CtrlBtnRequest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CtrlBtnRequest.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.CtrlBtnRequest.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.CtrlBtnRequest.FlatStyle = FlatStyle.Flat
        Me.CtrlBtnRequest.FlatAppearance.BorderSize = 1
    End Sub

    'Enum sampleinstrumentType
    '    Mobile
    'End Enum
    'Enum TransactionStatusResponse
    '    TRANSACTION_NOT_FOUND
    '    BAD_REQUEST
    '    AUTHORIZATION_FAILED
    '    INTERNAL_SERVER_ERROR
    '    PAYMENT_SUCCESS
    '    PAYMENT_ERROR
    '    PAYMENT_PENDING
    '    PAYMENT_CANCELLED
    '    PAYMENT_DECLINED
    'End Enum

    'Enum ResponsefromCancel
    '    SUCCESS
    '    INTERNAL_SERVER_ERROR
    '    INVALID_TRANSACTION_ID
    '    PAYMENT_ALREADY_COMPLETED
    'End Enum

    Public Function GettransactionIdForPhonePe() As String
        Try
            TransactionID = String.Empty
            Dim objType = "FO_DOC"
            Dim docno As String = objCashMemo.getDocumentNo("PhonePe", clsAdmin.SiteCode, objType)
            TransactionID = GenDocNo("PP" & clsAdmin.SiteCode & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
            If (objClsComm.UpdateDocumentNoForPhonepe()) Then

                Return TransactionID
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Public Function CreatePhonePePaymentRequestUrl()
        Try
            Dim objReq As New PhonePe.PhonePePaymentRequest
            objReq.merchantId = clsDefaultConfiguration.PhonePeMerchantId
            objReq.transactionId = GettransactionIdForPhonePe()
            objReq.merchantOrderId = CMBillno
            objReq.amount = FormatNumber(((txtTotalAmt.Text) * 100), 0).Replace(",", "")
            objReq.instrumentType = "MOBILE"
            objReq.instrumentReference = txtCollectAmt.Text
            objReq.expiresIn = 180
            objReq.message = "collect for bill no " + CMBillno + "transaction id " + objReq.transactionId + " order"
            objReq.email = ""
            objReq.shortName = ""
            objReq.subMerchant = ""
            objReq.storedId = clsAdmin.SiteCode
            objReq.terminalId = clsAdmin.TerminalID

            Dim JSONString = New StringBuilder()
            JSONString.Append("{")
            JSONString.Append("""merchantId""" + ":""" + objReq.merchantId + """,")
            JSONString.Append("""transactionId""" + ":""" + objReq.transactionId + """,")
            JSONString.Append("""merchantOrderId""" + ":""" + objReq.merchantOrderId + """, ")
            JSONString.Append("""amount""" + ": " + Convert.ToString(objReq.amount) + ", ")
            JSONString.Append("""instrumentType""" + ":""" + Convert.ToString(objReq.instrumentType) + """, ")
            JSONString.Append("""instrumentReference""" + ":""" + objReq.instrumentReference + """, ")
            JSONString.Append("""expiresIn""" + ":" + Convert.ToString(objReq.expiresIn) + ", ")
            JSONString.Append("""message""" + ":""" + objReq.message + """, ")
            JSONString.Append("""email""" + ":""" + objReq.email + """, ")
            JSONString.Append("""shortName""" + ":""" + objReq.shortName + """, ")
            JSONString.Append("""subMerchant""" + ":""" + objReq.subMerchant + """, ")
            JSONString.Append("""storedId""" + ":""" + objReq.storedId + """, ")
            JSONString.Append("""terminalId""" + ":""" + objReq.terminalId + """")
            JSONString.Append("}")
            Return JSONString.ToString()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function


    Public Function postData(ByVal JsonString As String, ByVal Authkey As String, ByVal APIUrl As String, ByVal TransactionID As String)
        Try
            isRequestSuccess = "FALSE"
            jsonResponse = ""
            CreateTxtFileForJson("REQUEST", "POST", Authkey, clsDefaultConfiguration.xProviderId, APIUrl, JsonString, TransactionID, isRequestSuccess, jsonResponse)
            If isRequestSuccess.ToString.ToUpper.Equals("TRUE") Then
                Return jsonResponse
            Else
                Dim resp As String = ""
                resp = jsonResponse
                Dim RequestJSSerializer As New JavaScriptSerializer()
                Dim dictforcheck As Dictionary(Of String, Object) = RequestJSSerializer.Deserialize(Of Dictionary(Of String, Object))(resp)
                Dim ResponseCode As String
                ResponseCode = dictforcheck("code")
                If ResponseCode = "SUCCESS" Then
                    ShowMessage("Request Sent sucessfully, Payment is in Progress, Do not press Escape otherwise you will loose Data", getValueByKey("CLAE04"))
                ElseIf ResponseCode = "INTERNAL_SERVER_ERROR" Then
                    ShowMessage("Something went wrong try after some time.", getValueByKey("CLAE04"))
                ElseIf ResponseCode = "INVALID_TRANSACTION_ID" Then
                    ShowMessage("TransactionId in request was duplicate.", getValueByKey("CLAE04"))
                    CtrlBtnRequest.Enabled = True
                ElseIf ResponseCode = "BAD_REQUEST" Then
                    ShowMessage("Some mandatory parameter was missing", getValueByKey("CLAE04"))
                    CtrlBtnRequest.Enabled = True
                    CtrlBtnCheck.Enabled = False
                ElseIf ResponseCode = "PAYMENT_SUCCESS" Then
                    ShowMessage("Payment is successful.", getValueByKey("CLAE04"))
                    ' btnSave_Click(sender, e)
                ElseIf ResponseCode = "AUTHORIZATION_FAILED" Then
                    CtrlBtnRequest.Enabled = True
                    ShowMessage("Authorization Failed. Please check with technical team.", getValueByKey("CLAE04"))
                    Me.Close()
                    CheckPhonePeError = True
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function CheckPhonePeResponce(ByVal TransactionID As String)
        Try
            Dim SHA256forCheck As String = "/v3/transaction/" + clsDefaultConfiguration.PhonePeMerchantId + "/" + TransactionID + "/status" + clsDefaultConfiguration.PhonepeAuthKey
            Dim XVerifyforcheck As String = objClsComm.GenerateSHA256String(SHA256forCheck) + "###" + clsDefaultConfiguration.PhonepeAuthIndex.ToString()
            Dim CheckUrl As String = clsDefaultConfiguration.PhonePeCheckPaymentUrl + clsDefaultConfiguration.PhonePeMerchantId + "/" + TransactionID + "/status"
            objClsComm.UpdatePhonePeCheckRequest(clsAdmin.SiteCode, TransactionID, SHA256forCheck)
            isRequestSuccess = "FALSE"
            jsonResponse = ""
            CreateTxtFileForJson("CHECK", "GET", XVerifyforcheck, clsDefaultConfiguration.xProviderId, CheckUrl, "", TransactionID, isRequestSuccess, jsonResponse)
            If isRequestSuccess.ToString.ToUpper.Equals("TRUE") Then
                Return jsonResponse
            Else
                Dim resp As String = ""
                resp = jsonResponse
                Dim CheckJSResponse As New JavaScriptSerializer()
                Dim dictforcheck As Dictionary(Of String, Object) = CheckJSResponse.Deserialize(Of Dictionary(Of String, Object))(resp)
                Dim ResponseCode As String
                ResponseCode = dictforcheck("code")
                If ResponseCode = "TRANSACTION_NOT_FOUND" Then
                    ShowMessage("Payment not initiated inside PhonePe", getValueByKey("CLAE04"))
                    CtrlBtnRequest.Enabled = True
                    txtCollectAmt.Enabled = True
                    Me.Close()
                    CheckPhonePeError = True
                ElseIf ResponseCode = "BAD_REQUEST" Then
                    ShowMessage("Some mandatory parameter was missing", getValueByKey("CLAE04"))
                ElseIf ResponseCode = "AUTHORIZATION_FAILED" Then
                    ShowMessage("Authorization Failed. Please check with technical team.", getValueByKey("CLAE04"))
                    CheckPhonePeError = True
                    Me.Close()
                ElseIf ResponseCode = "INTERNAL_SERVER_ERROR" Then
                    ShowMessage("Something went wrong try after some time.", getValueByKey("CLAE04"))
                ElseIf ResponseCode = "PAYMENT_SUCCESS" Then
                    IsPaymentSuccess = True
                    objClsComm.UpdatePhonePePaymentStatus(clsAdmin.SiteCode, TransactionID)
                    ShowMessage("Payment is successful", getValueByKey("CLAE04"))
                    Me.Close()
                    CheckPhonePeError = True
                ElseIf ResponseCode = "PAYMENT_ERROR" Then
                    txtCollectAmt.Enabled = True
                    CtrlBtnRequest.Enabled = True
                    ShowMessage("Payment failed", getValueByKey("CLAE04"))
                    CheckPhonePeError = True
                ElseIf ResponseCode = "PAYMENT_FAILED" Then
                    CtrlBtnRequest.Enabled = True
                    ShowMessage("Payment failed", getValueByKey("CLAE04"))
                    Me.Close()
                    CheckPhonePeError = True
                ElseIf ResponseCode = "PAYMENT_PENDING" Then
                    CtrlBtnRequest.Enabled = False
                    ShowMessage("Payment is in progress, please check the status after sometime.", getValueByKey("CLAE04"))
                ElseIf ResponseCode = "PAYMENT_CANCELLED" Then
                    CtrlBtnRequest.Enabled = True
                    txtCollectAmt.Enabled = True
                    ShowMessage("Payment cancelled by merchant", getValueByKey("CLAE04"))
                    Me.Close()
                    CheckPhonePeError = True
                ElseIf ResponseCode = "PAYMENT_DECLINED" Then
                    CtrlBtnRequest.Enabled = True
                    txtCollectAmt.Enabled = True
                    ShowMessage("Payment declined by user", getValueByKey("CLAE04"))
                    Me.Close()
                    CheckPhonePeError = True
                End If
                ' LogException(ex)
                ' Return ex.Message
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function POSTDataForCancel(ByVal TransactionID As String)
        Try
            Dim SHA256forcancel As String = "/v3/charge/" + clsDefaultConfiguration.PhonePeMerchantId + "/" + TransactionID + "/cancel" + clsDefaultConfiguration.PhonepeAuthKey
            Dim Authkey As String = objClsComm.GenerateSHA256String(SHA256forcancel) + "###" + clsDefaultConfiguration.PhonepeAuthIndex.ToString()
            Dim APIUrl As String = clsDefaultConfiguration.PhonePeCancelPaymentUrl + clsDefaultConfiguration.PhonePeMerchantId + "/" + TransactionID + "/cancel"
            objClsComm.UpdatePhonePeCancelRequest(clsAdmin.SiteCode, TransactionID, SHA256forcancel)
            isRequestSuccess = "FALSE"
            jsonResponse = ""
            CreateTxtFileForJson("CANCEL", "POST", Authkey, clsDefaultConfiguration.xProviderId, APIUrl, "", TransactionID, isRequestSuccess, jsonResponse)
            If isRequestSuccess.ToString.ToUpper.Equals("TRUE") Then
                Return jsonResponse
            Else
                Dim resp As String = ""
                resp = jsonResponse
                Dim jss As New JavaScriptSerializer()
                Dim dictforCancel As Dictionary(Of String, Object) = jss.Deserialize(Of Dictionary(Of String, Object))(resp)
                Dim ResponseCancelCode As String
                ResponseCancelCode = dictforCancel("code")
                If ResponseCancelCode = "SUCCESS" Then
                    ShowMessage("Cancel Request sent sucessfully", getValueByKey("CLAE04"))
                    TransactionID = ""
                ElseIf ResponseCancelCode = "INTERNAL_SERVER_ERROR" Then
                    ShowMessage("Something went wrong try after some time. Again call Cancel API for payment status.", getValueByKey("CLAE04"))
                ElseIf ResponseCancelCode = "INVALID_TRANSACTION_ID" Then
                    ShowMessage("TransactionId in request was duplicate.", getValueByKey("CLAE04"))
                ElseIf ResponseCancelCode = "PAYMENT_ALREADY_COMPLETED" Then
                    ShowMessage("Payment has been succesful hence can't cancel the request", getValueByKey("CLAE04"))
                    IsPaymentSuccess = True
                    objClsComm.UpdatePhonePePaymentStatus(clsAdmin.SiteCode, TransactionID)
                    Me.Close()
                    TransactionID = ""
                End If
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function





    'Public Function postData(ByVal JsonString As String, ByVal Authkey As String, ByVal APIUrl As String)
    '    Try
    '        Dim request As HttpWebRequest
    '        Dim response As HttpWebResponse
    '        request = CType(WebRequest.Create(APIUrl), HttpWebRequest)
    '        request.ContentType = "application/json"
    '        request.Headers("ContentType") = "application/json"
    '        request.Headers("X-Verify") = Authkey
    '        request.Headers("X-PROVIDER-ID") = clsDefaultConfiguration.xProviderId
    '        request.ContentLength = JsonString.Length
    '        request.Method = "POST"
    '        request.AllowAutoRedirect = False
    '        Dim requeststream As Stream = request.GetRequestStream()
    '        Dim postBytes As Byte() = System.Text.Encoding.ASCII.GetBytes(JsonString)
    '        requeststream.Write(postBytes, 0, postBytes.Length)
    '        requeststream.Close()
    '        response = CType(request.GetResponse(), HttpWebResponse)
    '        Return New StreamReader(response.GetResponseStream()).ReadToEnd()
    '    Catch ex As WebException
    '        Dim aa = ex.Status
    '        Dim sr As StreamReader
    '        ex.Response.GetResponseStream()
    '        Dim resp = New StreamReader(ex.Response.GetResponseStream()).ReadToEnd()

    '        Dim RequestJSSerializer As New JavaScriptSerializer()
    '        Dim dictforcheck As Dictionary(Of String, Object) = RequestJSSerializer.Deserialize(Of Dictionary(Of String, Object))(resp)
    '        Dim ResponseCode As String
    '        ResponseCode = dictforcheck("code")

    '        If ResponseCode = "SUCCESS" Then
    '            ShowMessage("Request Sent sucessfully, Payment is in Progress, Do not press Escape otherwise you will loose Data", getValueByKey("CLAE04"))

    '        ElseIf ResponseCode = "INTERNAL_SERVER_ERROR" Then
    '            ShowMessage("Something went wrong try after some time.", getValueByKey("CLAE04"))

    '        ElseIf ResponseCode = "INVALID_TRANSACTION_ID" Then
    '            ShowMessage("TransactionId in request was duplicate.", getValueByKey("CLAE04"))
    '            CtrlBtnRequest.Enabled = True

    '        ElseIf ResponseCode = "BAD_REQUEST" Then
    '            ShowMessage("Some mandatory parameter was missing", getValueByKey("CLAE04"))
    '            CtrlBtnRequest.Enabled = True
    '            CtrlBtnCheck.Enabled = False

    '        ElseIf ResponseCode = "PAYMENT_SUCCESS" Then
    '            ShowMessage("Payment is successful.", getValueByKey("CLAE04"))
    '            ' btnSave_Click(sender, e)

    '        ElseIf ResponseCode = "AUTHORIZATION_FAILED" Then
    '            CtrlBtnRequest.Enabled = True
    '            ShowMessage("Authorization Failed. Please check with technical team.", getValueByKey("CLAE04"))
    '            Me.Close()
    '            CheckPhonePeError = True
    '        End If
    '        ' LogException(ex)
    '        Return ex.Message
    '    End Try

    'End Function



    'Public Function CheckPhonePeResponce(ByVal TransactionID As String)
    '    Try
    '        Dim SHA256forCheck As String = "/v3/transaction/" + clsDefaultConfiguration.PhonePeMerchantId + "/" + TransactionID + "/status" + clsDefaultConfiguration.PhonepeAuthKey
    '        Dim XVerifyforcheck As String = objClsComm.GenerateSHA256String(SHA256forCheck) + "###" + clsDefaultConfiguration.PhonepeAuthIndex.ToString()
    '        Dim CheckUrl As String = clsDefaultConfiguration.PhonePeCheckPaymentUrl + clsDefaultConfiguration.PhonePeMerchantId + "/" + TransactionID + "/status"

    '        objClsComm.UpdatePhonePeCheckRequest(clsAdmin.SiteCode, TransactionID, SHA256forCheck)
    '        Dim request As HttpWebRequest
    '        Dim response As HttpWebResponse
    '        request = CType(WebRequest.Create(CheckUrl), HttpWebRequest)
    '        request.ContentType = "application/json"
    '        request.Headers("ContentType") = "application/json"
    '        request.Headers("X-Verify") = XVerifyforcheck
    '        request.Headers("X-PROVIDER-ID") = clsDefaultConfiguration.xProviderId
    '        request.Method = "GET"
    '        request.AllowAutoRedirect = False
    '        response = CType(request.GetResponse(), HttpWebResponse)
    '        Return New StreamReader(response.GetResponseStream()).ReadToEnd()

    '    Catch ex As WebException
    '        Dim aa = ex.Status
    '        Dim sr As StreamReader
    '        ex.Response.GetResponseStream()
    '        Dim resp = New StreamReader(ex.Response.GetResponseStream()).ReadToEnd()
    '        Dim CheckJSResponse As New JavaScriptSerializer()

    '        Dim dictforcheck As Dictionary(Of String, Object) = CheckJSResponse.Deserialize(Of Dictionary(Of String, Object))(resp)
    '        Dim ResponseCode As String
    '        ResponseCode = dictforcheck("code")

    '        If ResponseCode = "TRANSACTION_NOT_FOUND" Then
    '            ShowMessage("Payment not initiated inside PhonePe", getValueByKey("CLAE04"))
    '            CtrlBtnRequest.Enabled = True
    '            txtCollectAmt.Enabled = True
    '            Me.Close()
    '            CheckPhonePeError = True
    '        ElseIf ResponseCode = "BAD_REQUEST" Then
    '            ShowMessage("Some mandatory parameter was missing", getValueByKey("CLAE04"))
    '        ElseIf ResponseCode = "AUTHORIZATION_FAILED" Then
    '            ShowMessage("Authorization Failed. Please check with technical team.", getValueByKey("CLAE04"))
    '            CheckPhonePeError = True
    '            Me.Close()
    '        ElseIf ResponseCode = "INTERNAL_SERVER_ERROR" Then
    '            ShowMessage("Something went wrong try after some time.", getValueByKey("CLAE04"))
    '        ElseIf ResponseCode = "PAYMENT_SUCCESS" Then
    '            IsPaymentSuccess = True
    '            objClsComm.UpdatePhonePePaymentStatus(clsAdmin.SiteCode, TransactionID)
    '            ShowMessage("Payment is successful", getValueByKey("CLAE04"))
    '            Me.Close()
    '            CheckPhonePeError = True
    '        ElseIf ResponseCode = "PAYMENT_ERROR" Then
    '            txtCollectAmt.Enabled = True
    '            CtrlBtnRequest.Enabled = True
    '            ShowMessage("Payment failed", getValueByKey("CLAE04"))
    '            CheckPhonePeError = True
    '        ElseIf ResponseCode = "PAYMENT_FAILED" Then
    '            CtrlBtnRequest.Enabled = True
    '            ShowMessage("Payment failed", getValueByKey("CLAE04"))
    '            Me.Close()
    '            CheckPhonePeError = True
    '        ElseIf ResponseCode = "PAYMENT_PENDING" Then
    '            CtrlBtnRequest.Enabled = False
    '            ShowMessage("Payment is in progress, please check the status after sometime.", getValueByKey("CLAE04"))
    '        ElseIf ResponseCode = "PAYMENT_CANCELLED" Then
    '            CtrlBtnRequest.Enabled = True
    '            txtCollectAmt.Enabled = True
    '            ShowMessage("Payment cancelled by merchant", getValueByKey("CLAE04"))
    '            Me.Close()
    '            CheckPhonePeError = True
    '        ElseIf ResponseCode = "PAYMENT_DECLINED" Then
    '            CtrlBtnRequest.Enabled = True
    '            txtCollectAmt.Enabled = True
    '            ShowMessage("Payment declined by user", getValueByKey("CLAE04"))
    '            Me.Close()
    '            CheckPhonePeError = True
    '        End If
    '        'LogException(ex)
    '        Return ex.Message
    '    End Try

    'End Function



    'Public Function POSTDataForCancel(ByVal TransactionID As String)
    '    Try
    '        Dim SHA256forcancel As String = "/v3/charge/" + clsDefaultConfiguration.PhonePeMerchantId + "/" + TransactionID + "/cancel" + clsDefaultConfiguration.PhonepeAuthKey
    '        Dim Authkey As String = objClsComm.GenerateSHA256String(SHA256forcancel) + "###" + clsDefaultConfiguration.PhonepeAuthIndex.ToString()
    '        Dim APIUrl As String = clsDefaultConfiguration.PhonePeCancelPaymentUrl + clsDefaultConfiguration.PhonePeMerchantId + "/" + TransactionID + "/cancel"

    '        objClsComm.UpdatePhonePeCancelRequest(clsAdmin.SiteCode, TransactionID, SHA256forcancel)
    '        Dim request As HttpWebRequest
    '        Dim response As HttpWebResponse
    '        request = CType(WebRequest.Create(APIUrl), HttpWebRequest)
    '        request.ContentType = "application/json"
    '        request.Headers("ContentType") = "application/json"
    '        request.Headers("X-Verify") = Authkey
    '        request.Headers("X-PROVIDER-ID") = clsDefaultConfiguration.xProviderId
    '        request.Method = "POST"
    '        request.AllowAutoRedirect = False
    '        response = CType(request.GetResponse(), HttpWebResponse)
    '        Return New StreamReader(response.GetResponseStream()).ReadToEnd()
    '    Catch ex As WebException
    '        Dim aa = ex.Status
    '        Dim sr As StreamReader
    '        ex.Response.GetResponseStream()
    '        Dim resp = New StreamReader(ex.Response.GetResponseStream()).ReadToEnd()
    '        Dim jss As New JavaScriptSerializer()
    '        Dim dictforCancel As Dictionary(Of String, Object) = jss.Deserialize(Of Dictionary(Of String, Object))(resp)
    '        Dim ResponseCancelCode As String
    '        ResponseCancelCode = dictforCancel("code")
    '        If ResponseCancelCode = "SUCCESS" Then
    '            ShowMessage("Cancel Request sent sucessfully", getValueByKey("CLAE04"))
    '            TransactionID = ""
    '        ElseIf ResponseCancelCode = "INTERNAL_SERVER_ERROR" Then
    '            ShowMessage("Something went wrong try after some time. Again call Cancel API for payment status.", getValueByKey("CLAE04"))
    '        ElseIf ResponseCancelCode = "INVALID_TRANSACTION_ID" Then
    '            ShowMessage("TransactionId in request was duplicate.", getValueByKey("CLAE04"))
    '        ElseIf ResponseCancelCode = "PAYMENT_ALREADY_COMPLETED" Then
    '            ShowMessage("Payment has been succesful hence can't cancel the request", getValueByKey("CLAE04"))
    '            IsPaymentSuccess = True
    '            objClsComm.UpdatePhonePePaymentStatus(clsAdmin.SiteCode, TransactionID)
    '            Me.Close()
    '            TransactionID = ""
    '        End If
    '        ' LogException(ex)
    '        Return ex.Message
    '    End Try

    'End Function



    Private Sub CtrlBtn5_Click(sender As Object, e As EventArgs) Handles CtrlBtn5.Click
        Try
            If CtrlBtnCancle.Enabled = True Then
                CtrlBtnCancle_Click(sender, e)
            Else
                Me.Close()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub



    Public Function phone(ByVal no As String) As Boolean
        Dim expr As Regex = New Regex("^((\+){0,1}91(\s){0,1}(\-){0,1}(\s){0,1}){0,1}9[0-9](\s){0,1}(\-){0,1}(\s){0,1}[1-9]{1}[0-9]{7}$")

        If expr.IsMatch(no) Then
            Return True
        Else
            Return False
        End If
    End Function


#End Region
#Region "PhonePeRequestResponseCheck"

    Private Sub CtrlBtnCheck_Click(sender As Object, e As EventArgs) Handles CtrlBtnCheck.Click
        Try

            Dim CheckRsponse As String = CheckPhonePeResponce(TransactionID)

            objClsComm.UpdatePhonePeCheckRequestResponse(clsAdmin.SiteCode, TransactionID, CheckRsponse)
            Dim CheckJSResponse As New JavaScriptSerializer()
            Dim dictforcheck As Dictionary(Of String, Object) = CheckJSResponse.Deserialize(Of Dictionary(Of String, Object))(CheckRsponse)
            Dim ResponseCode As String
            ResponseCode = dictforcheck("code")
            If ResponseCode = "TRANSACTION_NOT_FOUND" Then
                ShowMessage("Payment not initiated inside PhonePe", getValueByKey("CLAE04"))
                CtrlBtnRequest.Enabled = True
                txtCollectAmt.Enabled = True
                Me.Close()
            ElseIf ResponseCode = "BAD_REQUEST" Then
                ShowMessage("Some mandatory parameter was missing", getValueByKey("CLAE04"))
            ElseIf ResponseCode = "AUTHORIZATION_FAILED" Then
                ShowMessage("Authorization Failed. Please check with technical team.", getValueByKey("CLAE04"))
                Me.Close()
            ElseIf ResponseCode = "INTERNAL_SERVER_ERROR" Then
                ShowMessage("Something went wrong try after some time.", getValueByKey("CLAE04"))
            ElseIf ResponseCode = "PAYMENT_SUCCESS" Then
                IsPaymentSuccess = True
                objClsComm.UpdatePhonePePaymentStatus(clsAdmin.SiteCode, TransactionID)
                ShowMessage("Payment is successful", getValueByKey("CLAE04"))
                Me.Close()
                'btnSave_Click(sender, e)
            ElseIf ResponseCode = "PAYMENT_ERROR" Then
                txtCollectAmt.Enabled = True
                CtrlBtnRequest.Enabled = True
                ShowMessage("Payment failed", getValueByKey("CLAE04"))
                Me.Close()
            ElseIf ResponseCode = "PAYMENT_FAILED" Then
                CtrlBtnRequest.Enabled = True
                ShowMessage("Payment failed", getValueByKey("CLAE04"))
                Me.Close()
            ElseIf ResponseCode = "PAYMENT_PENDING" Then
                CtrlBtnRequest.Enabled = False
                ShowMessage("Payment is in progress, please check the status after sometime.", getValueByKey("CLAE04"))
            ElseIf ResponseCode = "PAYMENT_CANCELLED" Then
                CtrlBtnRequest.Enabled = True
                txtCollectAmt.Enabled = True
                ShowMessage("Payment cancelled by merchant", getValueByKey("CLAE04"))
                Me.Close()
            ElseIf ResponseCode = "PAYMENT_DECLINED" Then
                CtrlBtnRequest.Enabled = True
                txtCollectAmt.Enabled = True
                ShowMessage("Payment declined by user", getValueByKey("CLAE04"))
                Me.Close()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub CtrlBtnRequest_Click(sender As Object, e As EventArgs) Handles CtrlBtnRequest.Click
        Try


            If txtCollectAmt.Text.Length < 10 Then
                ShowMessage("Enter 10 digit mobile number.", getValueByKey("CLAE04"))
                Exit Sub
            End If

            Dim dtcustmobdtl As New DataTable
            dtcustmobdtl = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, txtCollectAmt.Text, CustomerWisePrice:=clsDefaultConfiguration.customerwisepricemanagement, CustFormat:=clsDefaultConfiguration.DetailedCustomerCreationformat)
            If Not dtcustmobdtl Is Nothing AndAlso dtcustmobdtl.Rows.Count > 0 Then
                CustMobileno = dtcustmobdtl.Rows(0)("MobileNo").ToString
                If CustMobileno <> txtCollectAmt.Text Then
                    Dim objClpCustomer As New frmNSearchCustomer
                    objClpCustomer.CustomerNo = String.Empty
                    objClpCustomer.isHashTagApplicable = True
                    objClpCustomer.AccessCustomerOutside = True
                    objClpCustomer.ShowSO = False
                    objClpCustomer.ShowCLP = True
                    objClpCustomer.IsCustomerfromPhonePe = True
                    objClpCustomer.SearchedValue = txtCollectAmt.Text
                    objClpCustomer.ShowDialog()
                End If
                dtcustmobdtl.Clear()
            End If

            Dim JsonString As String = CreatePhonePePaymentRequestUrl()
            SavePhonePePaymentRequest(JsonString)
            Dim JSONTOBase64Result As String = objClsComm.EncodeTo64(JsonString)
            Dim BodyForRequestAPI As String = JSONTOBase64Result
            Dim InputforSHA256 As String = BodyForRequestAPI + "/v3/charge" + clsDefaultConfiguration.PhonepeAuthKey
            Dim HdrSHA256 As String = objClsComm.GenerateSHA256String(InputforSHA256) + "###" + clsDefaultConfiguration.PhonepeAuthIndex.ToString()
            Dim APIUrl As String = clsDefaultConfiguration.PhonePeRequestPaymentUrl
            BodyForRequestAPI = "{" + """" + "request" + """" + ":" + """" + BodyForRequestAPI + """" + "}"
            Dim PhonePeResponse As String = postData(BodyForRequestAPI, HdrSHA256, clsDefaultConfiguration.PhonePeRequestPaymentUrl, TransactionID)

            objClsComm.UpdatePhonePeRequestResponse(clsAdmin.SiteCode, TransactionID, PhonePeResponse)
            CtrlBtnRequest.Enabled = False
            txtCollectAmt.Enabled = False
            CtrlBtnCheck.Enabled = True
            CtrlBtnCancle.Enabled = True
            Dim RequestJSSerializer As New JavaScriptSerializer()
            Dim dictforcheck As Dictionary(Of String, Object) = RequestJSSerializer.Deserialize(Of Dictionary(Of String, Object))(PhonePeResponse)
            Dim ResponseCode As String
            ResponseCode = dictforcheck("code")

            If ResponseCode = "SUCCESS" Then
                ShowMessage("Request Sent sucessfully, Payment is in Progress, Do not press Escape otherwise you will loose Data", getValueByKey("CLAE04"))

            ElseIf ResponseCode = "INTERNAL_SERVER_ERROR" Then
                ShowMessage("Something went wrong try after some time.", getValueByKey("CLAE04"))

            ElseIf ResponseCode = "INVALID_TRANSACTION_ID" Then
                ShowMessage("TransactionId in request was duplicate.", getValueByKey("CLAE04"))
                CtrlBtnRequest.Enabled = True

            ElseIf ResponseCode = "BAD_REQUEST" Then
                ShowMessage("Some mandatory parameter was missing", getValueByKey("CLAE04"))
                CtrlBtnRequest.Enabled = True
                CtrlBtnCheck.Enabled = False

            ElseIf ResponseCode = "AUTHORIZATION_FAILED" Then
                CtrlBtnRequest.Enabled = True
                ShowMessage("Authorization Failed. Please check with technical team.", getValueByKey("CLAE04"))
                Me.Close()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub CtrlBtnCancle_Click(sender As Object, e As EventArgs) Handles CtrlBtnCancle.Click

        Try
            Dim ResponsefromUser As Int32
            ShowMessage("Are you sure want to close?", "Information", ResponsefromUser, "NO", "YES")

            If ResponsefromUser = 1 Then
                If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
                    If TransactionID Is Nothing Then
                        TransactionID = ""
                    End If
                    If Not String.IsNullOrEmpty(TransactionID) Then
                        Dim CancelResponse As String = POSTDataForCancel(TransactionID)
                        objClsComm.UpdatePhonePeCancelRequestResponse(clsAdmin.SiteCode, TransactionID, CancelResponse)

                        Dim jss As New JavaScriptSerializer()
                        Dim dictforCancel As Dictionary(Of String, Object) = jss.Deserialize(Of Dictionary(Of String, Object))(CancelResponse)
                        Dim ResponseCancelCode As String
                        ResponseCancelCode = dictforCancel("code")
                        If ResponseCancelCode = "SUCCESS" Then
                            ShowMessage("Cancel Request sent sucessfully", getValueByKey("CLAE04"))
                            Me.Close()
                        ElseIf ResponseCancelCode = "INTERNAL_SERVER_ERROR" Then
                            ShowMessage("Something went wrong try after some time.", getValueByKey("CLAE04"))
                        ElseIf ResponseCancelCode = "INVALID_TRANSACTION_ID" Then
                            ShowMessage("TransactionId in request was duplicate.", getValueByKey("CLAE04"))
                        ElseIf ResponseCancelCode = "PAYMENT_ALREADY_COMPLETED" Then
                            ShowMessage("Payment has been succesful hence can't cancel the request", getValueByKey("CLAE04"))
                            IsPaymentSuccess = True
                            objClsComm.UpdatePhonePePaymentStatus(clsAdmin.SiteCode, TransactionID)
                        End If
                    End If
                Else
                    Exit Sub
                End If

            Else
                Exit Sub
            End If
        Catch ex As Exception
            LogException(ex)
        End Try



    End Sub
#End Region

#Region "PhonePeSave"
    Public Sub SavePhonePePaymentRequest(ByVal reqjson As String)
        Try
            Dim drPhonePeDtl As DataRow
            drPhonePeDtl = dtPhonePedtl.NewRow()
            drPhonePeDtl("Sitecode") = clsAdmin.SiteCode
            drPhonePeDtl("TransactionId") = TransactionID
            drPhonePeDtl("BillNo") = CMBillno
            drPhonePeDtl("TerminalId") = clsAdmin.TerminalID
            drPhonePeDtl("MobileNo") = txtCollectAmt.Text
            drPhonePeDtl("CardNo") = CustName
            drPhonePeDtl("TotalAmt") = txtTotalAmt.Text
            drPhonePeDtl("PaymentRequest") = reqjson
            drPhonePeDtl("CREATEDAT") = clsAdmin.SiteCode
            drPhonePeDtl("CREATEDBY") = clsAdmin.UserCode
            drPhonePeDtl("CREATEDON") = DateTime.Now()
            drPhonePeDtl("UPDATEDAT") = clsAdmin.SiteCode
            drPhonePeDtl("UPDATEDBY") = clsAdmin.UserCode
            drPhonePeDtl("UPDATEDON") = objClsComm.GetCurrentDate
            drPhonePeDtl("IsPaymentDone") = False
            drPhonePeDtl("STATUS") = True
            dtPhonePedtl.Rows.Add(drPhonePeDtl)
            objClsComm.SaveOnlineTenderData(dtPhonePedtl, clsAdmin.SiteCode)

        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub

#End Region


    Public Function CreateTxtFileForJson(ByVal requestType As String, ByVal requestMethod As String, ByVal xVerify As String, _
                   ByVal xProviderId As String, ByVal requestUrl As String, _
        ByVal requestBody As String, ByVal transactionId As String, ByRef isRequestSuccess As String, ByRef jsonResponse As String)
        Try
            Dim sTextFile As New System.Text.StringBuilder
            sTextFile.AppendLine("request_type=" + requestType)
            sTextFile.AppendLine("request_method=" + requestMethod)
            sTextFile.AppendLine("request_header_content_type=" + "Application/json")
            sTextFile.AppendLine("request_header_x_verify=" + xVerify)
            sTextFile.AppendLine("request_header_x_provider_id=" + xProviderId)
            sTextFile.AppendLine("request_url=" + requestUrl)
            If Not String.IsNullOrEmpty(requestBody) Then
                sTextFile.AppendLine("request_body=" + requestBody)
            End If
            sTextFile.AppendLine("request_timestamp=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"))
            sTextFile.AppendLine("request_transaction_id=" + transactionId)
            Dim strDirectory As String = clsDefaultConfiguration.DayCloseReportPath + "\PhonePe"

            If (Not System.IO.Directory.Exists(strDirectory)) Then
                System.IO.Directory.CreateDirectory(strDirectory)
            End If
            For Each deleteFile In Directory.GetFiles(strDirectory, "*.*", SearchOption.TopDirectoryOnly)
                File.Delete(deleteFile)
            Next
            Dim path As String = transactionId + "_" + requestType + ".properties"
            Dim fPath = strDirectory + "\" + path
            Dim sw As New IO.StreamWriter(fPath, True)
            sw.WriteLine(sTextFile)
            sw.Close()
            sTextFile.Clear()
            writephonepeLog("File generated calling batch file " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"))
            Dim pid = Shell(Application.StartupPath & "\start_PhonePe.bat", AppWinStyle.Hide)
            'Dim MyProcess As Process = Nothing
            'MyProcess = Process.Start(Application.StartupPath & "\start_PhonePe.bat")
            ReadJSONFile(transactionId, requestType, isRequestSuccess, jsonResponse)
            writephonepeLog("end" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"))
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function ReadJSONFile(ByVal transactionId As String, ByVal requestType As String, ByRef isRequestSuccess As String, ByRef jsonResponse As String) As Boolean
        Try
            Dim strFilePath As String = clsDefaultConfiguration.DayCloseReportPath + "\PhonePe\" + transactionId + "_" + requestType + "_response" + ".json"
            Dim strJsonResponse As String = ""
            For i = 0 To 1000
                If (System.IO.File.Exists(strFilePath)) Then
                    writephonepeLog("reading response from file " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"))
                    Using sr As StreamReader = New StreamReader(strFilePath)
                        strJsonResponse = sr.ReadLine()
                    End Using
                    Dim RequestJSSerializer As New JavaScriptSerializer()
                    Dim dictforcheck As Dictionary(Of String, Object) = RequestJSSerializer.Deserialize(Of Dictionary(Of String, Object))(strJsonResponse)
                    Dim ResponseCode As String = ""
                    ResponseCode = dictforcheck("success")
                    If ResponseCode.ToString.ToUpper.Equals("TRUE") Then
                        jsonResponse = strJsonResponse
                        isRequestSuccess = "TRUE"
                        Return True
                    Else
                        jsonResponse = strJsonResponse
                        isRequestSuccess = "FALSE"
                        Return False
                    End If
                Else
                    writephonepeLog("file not exist " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"))
                    Threading.Thread.Sleep(100)
                End If
            Next i
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Private Sub writephonepeLog(ByVal mes As String)
        Dim ax As New ApplicationException(mes)
        LogException(ax)
    End Sub




End Class