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
Public Class frmTenderMode

#Region "Properties"
    'added by khusrao adil on 25-07-2018
    Private _allowInnovitiPayment As Boolean = False
    Public IsloadEventCalled As Boolean = False
    Public Property AllowInnovitiPayment() As Boolean
        Get
            Return _allowInnovitiPayment
        End Get
        Set(ByVal value As Boolean)
            _allowInnovitiPayment = value
        End Set
    End Property
    'added by khusrao adil on 25-07-2018 for innviti with card functionality point _ natural client
    Private _dtInnoviti As DataTable
    Public Property dtInnoviti() As DataTable
        Get
            Return _dtInnoviti
        End Get
        Set(ByVal value As DataTable)
            _dtInnoviti = value
        End Set
    End Property
    Public _IsCashierPromoSelected As Boolean
    Dim _Actiontype As String
    Public ReadOnly Property Action() As String
        Get
            Return _Actiontype
        End Get
    End Property
    Private _isGiftVouchheReq As Boolean = False
    Public Property IsGiftVouchheReq() As Boolean
        Get
            Return _isGiftVouchheReq
        End Get
        Set(ByVal value As Boolean)
            _isGiftVouchheReq = value
        End Set
    End Property
    Private _IsCancelAcceptPayment As Boolean = False
    Public Property IsCancelAcceptPayment() As Boolean
        Get
            Return _IsCancelAcceptPayment
        End Get
        Set(ByVal value As Boolean)
            _IsCancelAcceptPayment = value
        End Set
    End Property

    Private _TenderMode As String = False
    Public Property TenderMode() As String
        Get
            Return _TenderMode
        End Get
        Set(ByVal value As String)
            _TenderMode = value
        End Set
    End Property

    Private _CreditCardNumber As String
    Public Property CreditCardNumber() As String
        Get
            Return _CreditCardNumber
        End Get
        Set(ByVal value As String)
            _CreditCardNumber = value
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

    Private _CardExpiryDate As DateTime?
    Public Property CardExpiryDate() As DateTime?
        Get
            Return _CardExpiryDate
        End Get
        Set(ByVal value As DateTime?)
            _CardExpiryDate = value
        End Set
    End Property

    Private _BankAccNumber As String
    Public Property BankAccNumber() As String
        Get
            Return _BankAccNumber
        End Get
        Set(ByVal value As String)
            _BankAccNumber = value
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
    'added on 12 may - ashma - for Innoviti
    Private _Billno As String
    Public Property Billno() As String
        Get
            Return _Billno
        End Get
        Set(ByVal value As String)
            _Billno = value
        End Set
    End Property
    'added on 12 may - ashma - for Innoviti
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
    Private _resonseInnoviti As Dictionary(Of String, String)
    Public Property resonseInnoviti() As Dictionary(Of String, String)
        Get
            Return _resonseInnoviti
        End Get
        Set(ByVal value As Dictionary(Of String, String))
            _resonseInnoviti = value
        End Set
    End Property
    Dim _DocumentType As String
    Public Property DocumentType() As String
        Get
            Return _DocumentType
        End Get
        Set(ByVal value As String)
            _DocumentType = value
        End Set
    End Property
    Private _strGiftReceiptMessage As String
    Public Property GiftReceiptMessage() As String
        Get
            Return _strGiftReceiptMessage
        End Get
        Set(ByVal value As String)
            _strGiftReceiptMessage = value
        End Set
    End Property
    Protected dsRecieptType As New DataSet()
    Public ReadOnly Property ReciptTotalAmount() As DataSet
        Get
            Return dsRecieptType
        End Get
    End Property

    Private _CardTenderName As String  '' added by nikhil
    Public Property CardTenderName() As String
        Get
            Return _CardTenderName
        End Get
        Set(ByVal value As String)
            _CardTenderName = value
        End Set
    End Property

    Private _CardTenderCode As String
    Public Property CardTenderCode() As String
        Get
            Return _CardTenderCode
        End Get
        Set(ByVal value As String)
            _CardTenderCode = value
        End Set
    End Property
    Public _dtCheque As New DataTable()
    Public Property dtCheque() As DataTable
        Get
            Return _dtCheque
        End Get
        Set(ByVal value As DataTable)
            _dtCheque = value
        End Set
    End Property

    Private _TenderTypeCode As String = ""
    Public Property TenderTypeCode() As String
        Get
            Return _TenderTypeCode
        End Get
        Set(ByVal value As String)
            _TenderTypeCode = value
        End Set
    End Property


    'Private _CustEmailID As String
    'Public Property CustEmailID() As String
    '    Get
    '        Return _CustEmailID
    '    End Get
    '    Set(ByVal value As String)
    '        _CustEmailID = value
    '    End Set
    'End Property

    'Private _CustMobileno As String
    'Public Property CustMobileno() As String
    '    Get
    '        Return _CustMobileno
    '    End Get
    '    Set(ByVal value As String)
    '        _CustMobileno = value
    '    End Set
    'End Property




#End Region

#Region "Variables "
    Dim objClsComm As New clsCommon
    Dim objCashMemo As New clsCashMemo
    Dim tran As SqlTransaction
    Dim TransactionID As String 'PhonePe
    Dim IsNewRow As Boolean = False   'PhonePe
    Dim JsonString As String
    Dim PhonePeRespons As String
    Dim CheckRsponse As String
    Dim CancelRsponse As String

    Dim dtPhonePedtl As New DataTable
    Dim CustMobileno As String = ""
    Dim dtcustdtl As New DataTable
    Dim drPhonePeDtl As DataRow
    Dim objCustm As New clsCLPCustomer
    Dim cancelRequestUrl As String = ""
    Dim cancelRequestResponse As String = ""
    Dim ObjonlinePayment As New FrmOnlinePayment
    ' Dim ResponseCodeFormRequestAPI As String
#End Region

    Private Sub frmTenderMode_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try


            If Not String.IsNullOrEmpty(CustName) Then
                dtcustdtl = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, CustName, CustomerWisePrice:=clsDefaultConfiguration.customerwisepricemanagement, CustFormat:=clsDefaultConfiguration.DetailedCustomerCreationformat)
                If Not dtcustdtl Is Nothing AndAlso dtcustdtl.Rows.Count > 0 Then
                    CustMobileno = dtcustdtl.Rows(0)("MobileNo").ToString
                End If
            End If

            ' txtCalCollectAmount.ReadOnly = True
            Theme()
            'added by khusrao adil on 25-07-2018 for innviti with card functionality point _ natural client
            AllowInnovitiPayment = False
            btnCash_Click(sender, e)
            Dim clsCommon As New SpectrumBL.clsCommon()
            Dim bankDetails = clsCommon.GetBankDetails(clsAdmin.SiteCode)
            dtInnoviti = clsCommon.GetInnovitiStruc()
            dtInnoviti.Clear()
            If bankDetails IsNot Nothing AndAlso bankDetails.Rows.Count > 0 Then
                cmbBankName.DataSource = bankDetails
                cmbBankName.DisplayMember = "BankName"
                cmbBankName.ValueMember = "BankAccNo"
                cmbBankName.Splits(0).DisplayColumns(0).Visible = False
            End If
            ' AddHandler txtCollectAmt.KeyPress, AddressOf txtCollectAmt_KeyPress
            Dim objBirthListGlobal As New SpectrumBL.clsBirthListGobal
            Dim strErrorMsg As String = ""
            Dim dtPayment As DataTable = objBirthListGlobal.RetrieveQuery("select * from dbo.MstTender where tendertype='CreditCard'and Positive_Negative='+' and status=1 and sitecode='" & clsAdmin.SiteCode & "' ", strErrorMsg)

            If (dtPayment IsNot Nothing AndAlso dtPayment.Rows.Count > 0) Then
                CardTenderName = dtPayment.Rows(0)("TenderHeadName").ToString()
                CardTenderCode = dtPayment.Rows(0)("TenderHeadCode").ToString()
            End If
            TenderModeAvailable()
            _DocumentType = ""
            If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
                txtCollectAmt.DataType = Type.GetType("System.Int32")
                'AddHandler txtCollectAmt.KeyDown, AddressOf txtCollectAmt_keyDown
            End If
            btnFoodPandaCash.Text = "FP Cash"
            btnFoodPandaOnline.Text = "FP Online"
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then '@@
                Me.btnSave.Text = "Print (F10) "
                Me.btnGift.Text = "Gift Print (F11)"
                Me.btnCancle.Text = "Cancel"
                Me.CtrlBtn5.Visible = False
                '  If clsDefaultConfiguration.EnablePhonePeIntegration Then
                Me.CtrlBtnRequest.Visible = False
                Me.CtrlBtnCheck.Visible = False
                Me.CtrlBtnCancle.Visible = False
                'End If
            End If

            Dim dtDynamicTender = clsCommon.GetDynamicTenderTypes(clsAdmin.SiteCode)
            displaydtDynamicTender(dtDynamicTender)
            IsloadEventCalled = True
            dtPhonePedtl = objClsComm.GetPhonePePaymentRequestResponseStruct()
            dtPhonePedtl.Clear()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If TenderMode.ToString.ToUpper.Equals("CHEQUE") Then
            If CustName = "" Then
                ShowMessage(getValueByKey("Crs015"), "CM030 - " & getValueByKey("CLAE05"))
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtCardNo.Text.ToString) Then
                ShowMessage("Please enter cheque Number", "Information")
                Exit Sub
            End If

            If String.IsNullOrEmpty(dtpExpiryDateCheque.Value.ToString) Then
                ShowMessage("Please select the date", "Information")
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtmicrno.Text.ToString) Then
                ShowMessage("Please enter MICR Number", "Information")
                Exit Sub
            End If
            CreateDatatableforCheque(dtCheque)
        End If
        If TenderMode = "Card" Then
            '----------------------------------------------------------------------------------------------
            Try
                'added on 12 may - ashma - for Innoviti (If condition)
                'modified bt khusrao adil on 25-07-2018 for innviti with card functionality point _ natural client
                If clsDefaultConfiguration.PayFromInnoviti = True And clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) AndAlso AllowInnovitiPayment = True Then
                    ' DocumentType = ""
                    If DocumentType.ToUpper = "SO" Then

                    Else
                        Billno = modCommonFunc.GetCashBillno(clsAdmin.TerminalID, clsAdmin.DayOpenDate, clsAdmin.Financialyear)
                    End If

                    Dim responseFromInnov As New Dictionary(Of String, String)
                    responseFromInnov = modCommanFunction.PaymentToCallEDC(Billno, txtCalCollectAmount.Text.Trim)
                    If responseFromInnov Is Nothing Then
                        Dim ex As New Exception(modCommanFunction.InnovitiResponseError.ToString())
                        LogException(ex)
                        ShowMessage("Error in Card payment", "Information")
                        Exit Sub
                    Else
                        resonseInnoviti = New Dictionary(Of String, String)
                        resonseInnoviti = responseFromInnov
                        CreditCardNumber = resonseInnoviti("CardNumber")
                        ' ''-------------------------
                        'added by khusrao adil on 25-07-2018 for innviti with card functionality point _ natural client
                        Dim InnvoitiLineNo = 1
                        Dim dr As DataRow = dtInnoviti.NewRow
                        dr("LineNo") = InnvoitiLineNo
                        dr("TenderType") = TenderMode
                        dr("BillNo") = Billno
                        dr("AmountTendered") = txtCalCollectAmount.Text.Trim
                        dr("CardNo") = resonseInnoviti("CardNumber")
                        dr("RetrievalReferenceNumber") = resonseInnoviti("RetrievalReferenceNumber")
                        dr("InnvoitiApplicable") = True
                        dtInnoviti.Rows.Add(dr)
                        ' ''-------------------------
                    End If
                Else
                    CreditCardNumber = txtCardNo.Text
                    BankAccNumber = cmbBankName.SelectedValue
                    TotalCollectAmt = txtCollectAmt.Text.Trim()
                    TotalBillAmt = txtTotalAmt.Text.Trim()
                End If
            Catch ex As Exception
                LogException(ex)
            End Try
            '--------------------------------------------------------------------
            If Not ValidateEntries(TenderMode) Then
                Exit Sub
            End If
            If Not IsDBNull(dtpExpiryDate.Value) Then
                CardExpiryDate = dtpExpiryDate.Value
            End If
            CreditCardNumber = txtCardNo.Text
            BankAccNumber = cmbBankName.SelectedValue
        ElseIf TenderMode <> "Card" AndAlso TenderMode <> "Credit" Then
            If clsDefaultConfiguration.EnablePhonePeIntegration = True AndAlso TenderMode.ToString.ToUpper().Equals("PHONEPE") Then
                txtCollectAmt.Text = txtTotalAmt.Text
            End If
            If Not (CheckInteger(txtCollectAmt.Text)) Then
                ShowMessage(getValueByKey("ACP002"), "ACP002 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            TotalCollectAmt = txtCollectAmt.Text.Trim()
            TotalBillAmt = txtTotalAmt.Text.Trim()
            If Not ValidateEntries("Cash") Then
                Exit Sub
            End If
            If IsAmountReturnTocustomer() = True Then

            End If

        End If
        If TenderMode = "Credit" Then
            If CustName = "" And clsDefaultConfiguration.IsCustomerMandatoryForCreditSale = True Then
                ShowMessage(getValueByKey("Crs015"), "CM030 - " & getValueByKey("CLAE05"))
                Exit Sub
            End If
        End If
        IsCancelAcceptPayment = True
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub CtrlBtn5_Click(sender As Object, e As EventArgs) Handles CtrlBtn5.Click
        Me.Close()
    End Sub

    Private Sub btnCash_Click(sender As Object, e As EventArgs) Handles btnCash.Click
        If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
            If IsloadEventCalled = True AndAlso CancelRequestOnTenderChange() = False Then
                If ObjonlinePayment.IsPaymentSuccess Then
                    btnSave_Click(sender, e)
                End If
                Exit Sub
            Else
                PhonePeButtonVisibility()
            End If
        End If
        tblCard.Visible = False
        tblCash.Visible = True
        lblPaymentModeVal.Text = "Cash"
        TenderMode = "Cash"
        txtTotalAmt.Enabled = False
        txtCollectAmt.Enabled = True
        txtCollectAmt.Text = txtTotalAmt.Text.Trim()
        'added by khusrao adil on 22-11-2017
        AllowInnovitiPayment = False

        Me.btnCardInnoviti.BackColor = Color.WhiteSmoke
        Me.btnCardInnoviti.ForeColor = Color.Black
        ChangeBtnColor()
        Me.btnCash.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnCash.ForeColor = Color.FromArgb(255, 255, 255)

        Me.btnCard.BackColor = Color.WhiteSmoke
        Me.btnCard.ForeColor = Color.Black

        Me.btnFoodPandaCash.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaCash.ForeColor = Color.Black

        Me.btnFoodPandaOnline.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaOnline.ForeColor = Color.Black

        Me.btnOther.BackColor = Color.WhiteSmoke
        Me.btnOther.ForeColor = Color.Black

        Me.btnCheque.BackColor = Color.WhiteSmoke
        Me.btnCheque.ForeColor = Color.Black

        Me.btnPaytm.BackColor = Color.WhiteSmoke
        Me.btnPaytm.ForeColor = Color.Black

        Me.btnMobiKwik.BackColor = Color.WhiteSmoke
        Me.btnMobiKwik.ForeColor = Color.Black

        Me.btnJioMoney.BackColor = Color.WhiteSmoke
        Me.btnJioMoney.ForeColor = Color.Black

        Me.btnCreditSale.BackColor = Color.WhiteSmoke
        Me.btnCreditSale.ForeColor = Color.Black

        'code added on 31-07-2017 by vipul
        Me.btnSodexo.BackColor = Color.WhiteSmoke
        Me.btnSodexo.ForeColor = Color.Black

        Me.btnTr.BackColor = Color.WhiteSmoke
        Me.btnTr.ForeColor = Color.Black


        'code added by vipul on 15-05-2018
        Me.btnPayso.BackColor = Color.WhiteSmoke
        Me.btnPayso.ForeColor = Color.Black

        Me.btnTicketRestaurant.BackColor = Color.WhiteSmoke
        Me.btnTicketRestaurant.ForeColor = Color.Black

        Me.btnSodexoCards.BackColor = Color.WhiteSmoke
        Me.btnSodexoCards.ForeColor = Color.Black

        Me.btnSodexoCpn.BackColor = Color.WhiteSmoke
        Me.btnSodexoCpn.ForeColor = Color.Black
        Me.btnPhonePe.BackColor = Color.WhiteSmoke
        Me.btnPhonePe.ForeColor = Color.Black
    End Sub
    Private Sub btnCreditSale_Click(sender As Object, e As EventArgs) Handles btnCreditSale.Click

        If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
            If CancelRequestOnTenderChange() = False Then
                If ObjonlinePayment.IsPaymentSuccess Then
                    btnSave_Click(sender, e)
                End If
                Exit Sub
            Else
                PhonePeButtonVisibility()
            End If
        End If
        txtRemark.Focus()
        tblCard.Visible = False
        tblCash.Visible = True
        btnCreditSale.Visible = True
        '  lblPaymentModeVal.Text = "Cash"
        'added by khusrao adil on 25-07-2018 for innviti with card functionality point _ natural client
        AllowInnovitiPayment = False
        Me.btnCardInnoviti.BackColor = Color.WhiteSmoke
        Me.btnCardInnoviti.ForeColor = Color.Black
        TenderMode = "Credit"
        txtCollectAmt.Text = txtTotalAmt.Text.Trim()
        txtCollectAmt.Enabled = True
        lblPaymentModeVal.Text = "Credit"
        ChangeBtnColor()
        Me.btnCreditSale.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnCreditSale.ForeColor = Color.FromArgb(255, 255, 255)

        Me.btnCard.BackColor = Color.WhiteSmoke
        Me.btnCard.ForeColor = Color.Black

        Me.btnFoodPandaCash.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaCash.ForeColor = Color.Black

        Me.btnFoodPandaOnline.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaOnline.ForeColor = Color.Black

        Me.btnOther.BackColor = Color.WhiteSmoke
        Me.btnOther.ForeColor = Color.Black

        Me.btnCash.BackColor = Color.WhiteSmoke
        Me.btnCash.ForeColor = Color.Black

        Me.btnCheque.BackColor = Color.WhiteSmoke
        Me.btnCheque.ForeColor = Color.Black

        Me.btnPaytm.BackColor = Color.WhiteSmoke
        Me.btnPaytm.ForeColor = Color.Black

        Me.btnMobiKwik.BackColor = Color.WhiteSmoke
        Me.btnMobiKwik.ForeColor = Color.Black

        Me.btnJioMoney.BackColor = Color.WhiteSmoke
        Me.btnJioMoney.ForeColor = Color.Black

        'Me.tblCash.BackColor = Color.WhiteSmoke
        'Me.tblCash.ForeColor = Color.Black
        'code added on 31-07-2017 by vipul
        Me.btnSodexo.BackColor = Color.WhiteSmoke
        Me.btnSodexo.ForeColor = Color.Black

        Me.btnTr.BackColor = Color.WhiteSmoke
        Me.btnTr.ForeColor = Color.Black


        'code added by vipul on 15-05-2018
        Me.btnPayso.BackColor = Color.WhiteSmoke
        Me.btnPayso.ForeColor = Color.Black

        Me.btnTicketRestaurant.BackColor = Color.WhiteSmoke
        Me.btnTicketRestaurant.ForeColor = Color.Black

        Me.btnSodexoCards.BackColor = Color.WhiteSmoke
        Me.btnSodexoCards.ForeColor = Color.Black

        Me.btnSodexoCpn.BackColor = Color.WhiteSmoke
        Me.btnSodexoCpn.ForeColor = Color.Black
        Me.btnPhonePe.BackColor = Color.WhiteSmoke
        Me.btnPhonePe.ForeColor = Color.Black
    End Sub

    Private Sub btnCard_Click(sender As Object, e As EventArgs) Handles btnCard.Click

        If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
            If CancelRequestOnTenderChange() = False Then
                If ObjonlinePayment.IsPaymentSuccess Then
                    btnSave_Click(sender, e)
                End If
                Exit Sub
            Else
                PhonePeButtonVisibility()
            End If
        End If
        AllowInnovitiPayment = False
        lblmicrno.Visible = False
        txtmicrno.Visible = False
        cmbBankName.Text = ""
        txtCardNo.Value = ""
        dtpExpiryDateCheque.Visible = False
        dtpExpiryDate.Visible = True
        tblCard.Visible = True
        tblCash.Visible = False
        lblPaymentModeVal.Text = "Card"
        lblCardNo.Text = "*Card No."
        TenderMode = "Card"
        txtCalCollectAmount.Enabled = False

        dtpExpiryDateCheque.Value = ""
        dtpExpiryDate.Value = ""
        txtCollectAmt.Text = txtTotalAmt.Text.Trim()
        'commented by khusrao adil on 25-07-2018 for innviti with card functionality point _ natural client
        'If clsDefaultConfiguration.PayFromInnoviti = True And clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) Then
        '    lblBankName.Visible = False
        '    lblCardNo.Visible = False
        '    lblExpiryDate.Visible = False
        '    cmbBankName.Visible = False
        '    txtCardNo.Visible = False
        '    dtpExpiryDate.Visible = False

        'End If
        'added by khusrao adil on 25-07-2018 for innviti with card functionality point _ natural client
        lblBankName.Visible = True
        lblCardNo.Visible = True
        lblExpiryDate.Visible = True
        cmbBankName.Visible = True
        txtCardNo.Visible = True
        dtpExpiryDate.Visible = True
        Me.btnCardInnoviti.BackColor = Color.WhiteSmoke
        Me.btnCardInnoviti.ForeColor = Color.Black
        ChangeBtnColor()
        Me.btnCard.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnCard.ForeColor = Color.FromArgb(255, 255, 255)

        Me.btnFoodPandaCash.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaCash.ForeColor = Color.Black

        Me.btnFoodPandaOnline.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaOnline.ForeColor = Color.Black

        Me.btnCash.BackColor = Color.WhiteSmoke
        Me.btnCash.ForeColor = Color.Black

        Me.btnOther.BackColor = Color.WhiteSmoke
        Me.btnOther.ForeColor = Color.Black

        Me.btnCheque.BackColor = Color.WhiteSmoke
        Me.btnCheque.ForeColor = Color.Black

        Me.btnPaytm.BackColor = Color.WhiteSmoke
        Me.btnPaytm.ForeColor = Color.Black

        Me.btnMobiKwik.BackColor = Color.WhiteSmoke
        Me.btnMobiKwik.ForeColor = Color.Black

        Me.btnJioMoney.BackColor = Color.WhiteSmoke
        Me.btnJioMoney.ForeColor = Color.Black

        Me.btnCreditSale.BackColor = Color.WhiteSmoke
        Me.btnCreditSale.ForeColor = Color.Black
        'code added on 31-07-2017 by vipul
        Me.btnSodexo.BackColor = Color.WhiteSmoke
        Me.btnSodexo.ForeColor = Color.Black

        Me.btnTr.BackColor = Color.WhiteSmoke
        Me.btnTr.ForeColor = Color.Black


        'code added by vipul on 15-05-2018
        Me.btnPayso.BackColor = Color.WhiteSmoke
        Me.btnPayso.ForeColor = Color.Black

        Me.btnTicketRestaurant.BackColor = Color.WhiteSmoke
        Me.btnTicketRestaurant.ForeColor = Color.Black


        Me.btnSodexoCards.BackColor = Color.WhiteSmoke
        Me.btnSodexoCards.ForeColor = Color.Black

        Me.btnSodexoCpn.BackColor = Color.WhiteSmoke
        Me.btnSodexoCpn.ForeColor = Color.Black
        Me.btnPhonePe.BackColor = Color.WhiteSmoke
        Me.btnPhonePe.ForeColor = Color.Black
    End Sub

    Private Sub btnCheque_Click(sender As Object, e As EventArgs) Handles btnCheque.Click

        If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
            If CancelRequestOnTenderChange() = False Then
                If ObjonlinePayment.IsPaymentSuccess Then
                    btnSave_Click(sender, e)
                End If
                Exit Sub
            Else
                PhonePeButtonVisibility()
            End If
        End If

        lblmicrno.Visible = True
        txtmicrno.Visible = True
        cmbBankName.Text = ""


        txtCardNo.Value = ""
        txtmicrno.Value = ""

        dtpExpiryDateCheque.Value = ""
        dtpExpiryDate.Value = ""


        dtpExpiryDateCheque.Visible = True
        dtpExpiryDate.Visible = False

        lblCardNo.Text = "*ChequeNo"

        'added by khusrao adil on 25-07-2018 for innviti with card functionality point _ natural client
        AllowInnovitiPayment = False
        Me.btnCardInnoviti.BackColor = Color.WhiteSmoke
        Me.btnCardInnoviti.ForeColor = Color.Black


        tblCard.Visible = True
        tblCash.Visible = True
        lblPaymentModeVal.Text = "Cheque"
        TenderMode = "Cheque"

        txtCollectAmt.Text = txtTotalAmt.Text.Trim()
        txtCollectAmt.Enabled = False
        ChangeBtnColor()
        Me.btnCheque.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnCheque.ForeColor = Color.FromArgb(255, 255, 255)

        Me.btnCard.BackColor = Color.WhiteSmoke
        Me.btnCard.ForeColor = Color.Black

        Me.btnFoodPandaCash.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaCash.ForeColor = Color.Black

        Me.btnFoodPandaOnline.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaOnline.ForeColor = Color.Black

        Me.btnOther.BackColor = Color.WhiteSmoke
        Me.btnOther.ForeColor = Color.Black

        Me.btnCash.BackColor = Color.WhiteSmoke
        Me.btnCash.ForeColor = Color.Black

        Me.btnPaytm.BackColor = Color.WhiteSmoke
        Me.btnPaytm.ForeColor = Color.Black

        Me.btnMobiKwik.BackColor = Color.WhiteSmoke
        Me.btnMobiKwik.ForeColor = Color.Black

        Me.btnJioMoney.BackColor = Color.WhiteSmoke
        Me.btnJioMoney.ForeColor = Color.Black

        Me.btnCreditSale.BackColor = Color.WhiteSmoke
        Me.btnCreditSale.ForeColor = Color.Black
        ' cmdPayments_Click(cmdPayments, New System.EventArgs)

        'code added on 31-07-2017 by vipul
        Me.btnSodexo.BackColor = Color.WhiteSmoke
        Me.btnSodexo.ForeColor = Color.Black

        Me.btnTr.BackColor = Color.WhiteSmoke
        Me.btnTr.ForeColor = Color.Black


        'code added by vipul on 15-5-2018
        Me.btnPayso.BackColor = Color.WhiteSmoke
        Me.btnPayso.ForeColor = Color.Black

        Me.btnTicketRestaurant.BackColor = Color.WhiteSmoke
        Me.btnTicketRestaurant.ForeColor = Color.Black

        Me.btnSodexoCards.BackColor = Color.WhiteSmoke
        Me.btnSodexoCards.ForeColor = Color.Black

        Me.btnSodexoCpn.BackColor = Color.WhiteSmoke
        Me.btnSodexoCpn.ForeColor = Color.Black
        Me.btnPhonePe.BackColor = Color.WhiteSmoke
        Me.btnPhonePe.ForeColor = Color.Black
    End Sub

    Private Sub btnPaytm_Click(sender As Object, e As EventArgs) Handles btnPaytm.Click
        If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
            If CancelRequestOnTenderChange() = False Then
                If ObjonlinePayment.IsPaymentSuccess Then
                    btnSave_Click(sender, e)
                End If
                Exit Sub
            Else
                PhonePeButtonVisibility()
            End If
        End If
        txtRemark.Focus()
        tblCard.Visible = False
        tblCash.Visible = True
        lblPaymentModeVal.Text = "Paytm"
        TenderMode = "Paytm"


        'added by khusrao adil on 25-07-2018 for innviti with card functionality point _ natural client
        AllowInnovitiPayment = False
        Me.btnCardInnoviti.BackColor = Color.WhiteSmoke
        Me.btnCardInnoviti.ForeColor = Color.Black

        txtCollectAmt.Text = txtTotalAmt.Text.Trim()
        txtCollectAmt.Enabled = True
        ChangeBtnColor()
        Me.btnPaytm.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnPaytm.ForeColor = Color.FromArgb(255, 255, 255)

        Me.btnCard.BackColor = Color.WhiteSmoke
        Me.btnCard.ForeColor = Color.Black

        Me.btnFoodPandaCash.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaCash.ForeColor = Color.Black

        Me.btnFoodPandaOnline.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaOnline.ForeColor = Color.Black

        Me.btnOther.BackColor = Color.WhiteSmoke
        Me.btnOther.ForeColor = Color.Black

        Me.btnCash.BackColor = Color.WhiteSmoke
        Me.btnCash.ForeColor = Color.Black

        Me.btnCheque.BackColor = Color.WhiteSmoke
        Me.btnCheque.ForeColor = Color.Black

        Me.btnMobiKwik.BackColor = Color.WhiteSmoke
        Me.btnMobiKwik.ForeColor = Color.Black

        Me.btnJioMoney.BackColor = Color.WhiteSmoke
        Me.btnJioMoney.ForeColor = Color.Black

        Me.btnCreditSale.BackColor = Color.WhiteSmoke
        Me.btnCreditSale.ForeColor = Color.Black


        'code added on 31-07-2017 by vipul
        Me.btnSodexo.BackColor = Color.WhiteSmoke
        Me.btnSodexo.ForeColor = Color.Black

        Me.btnTr.BackColor = Color.WhiteSmoke
        Me.btnTr.ForeColor = Color.Black


        'code added by vipul on 15-05-2018
        Me.btnPayso.BackColor = Color.WhiteSmoke
        Me.btnPayso.ForeColor = Color.Black


        Me.btnTicketRestaurant.BackColor = Color.WhiteSmoke
        Me.btnTicketRestaurant.ForeColor = Color.Black

        Me.btnSodexoCards.BackColor = Color.WhiteSmoke
        Me.btnSodexoCards.ForeColor = Color.Black

        Me.btnSodexoCpn.BackColor = Color.WhiteSmoke
        Me.btnSodexoCpn.ForeColor = Color.Black
        Me.btnPhonePe.BackColor = Color.WhiteSmoke
        Me.btnPhonePe.ForeColor = Color.Black
    End Sub

    Private Sub btnMobiKwik_Click(sender As Object, e As EventArgs) Handles btnMobiKwik.Click

        If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
            If CancelRequestOnTenderChange() = False Then
                If ObjonlinePayment.IsPaymentSuccess Then
                    btnSave_Click(sender, e)
                End If
                Exit Sub
            Else
                PhonePeButtonVisibility()
            End If
        End If
        txtRemark.Focus()
        tblCard.Visible = False
        tblCash.Visible = True
        lblPaymentModeVal.Text = "MobiKwik"
        TenderMode = "MobiKwik"
        txtCollectAmt.Enabled = True
        txtCollectAmt.Text = txtTotalAmt.Text.Trim()

        'added by khusrao adil on 25-07-2018 for innviti with card functionality point _ natural client
        AllowInnovitiPayment = False
        Me.btnCardInnoviti.BackColor = Color.WhiteSmoke
        Me.btnCardInnoviti.ForeColor = Color.Black
        ChangeBtnColor()
        Me.btnMobiKwik.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnMobiKwik.ForeColor = Color.FromArgb(255, 255, 255)

        Me.btnCard.BackColor = Color.WhiteSmoke
        Me.btnCard.ForeColor = Color.Black

        Me.btnFoodPandaCash.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaCash.ForeColor = Color.Black

        Me.btnFoodPandaOnline.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaOnline.ForeColor = Color.Black

        Me.btnOther.BackColor = Color.WhiteSmoke
        Me.btnOther.ForeColor = Color.Black

        Me.btnCash.BackColor = Color.WhiteSmoke
        Me.btnCash.ForeColor = Color.Black

        Me.btnCheque.BackColor = Color.WhiteSmoke
        Me.btnCheque.ForeColor = Color.Black

        Me.btnPaytm.BackColor = Color.WhiteSmoke
        Me.btnPaytm.ForeColor = Color.Black

        Me.btnJioMoney.BackColor = Color.WhiteSmoke
        Me.btnJioMoney.ForeColor = Color.Black

        Me.btnCreditSale.BackColor = Color.WhiteSmoke
        Me.btnCreditSale.ForeColor = Color.Black


        'code added on 31-07-2017 by vipul
        Me.btnSodexo.BackColor = Color.WhiteSmoke
        Me.btnSodexo.ForeColor = Color.Black

        Me.btnTr.BackColor = Color.WhiteSmoke
        Me.btnTr.ForeColor = Color.Black


        'code added by vipul on 15-05-2018
        Me.btnPayso.BackColor = Color.WhiteSmoke
        Me.btnPayso.ForeColor = Color.Black


        Me.btnTicketRestaurant.BackColor = Color.WhiteSmoke
        Me.btnTicketRestaurant.ForeColor = Color.Black


        Me.btnSodexoCards.BackColor = Color.WhiteSmoke
        Me.btnSodexoCards.ForeColor = Color.Black

        Me.btnSodexoCpn.BackColor = Color.WhiteSmoke
        Me.btnSodexoCpn.ForeColor = Color.Black
        Me.btnPhonePe.BackColor = Color.WhiteSmoke
        Me.btnPhonePe.ForeColor = Color.Black
    End Sub

    Private Sub btnJioMoney_Click(sender As Object, e As EventArgs) Handles btnJioMoney.Click

        If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
            If CancelRequestOnTenderChange() = False Then
                If ObjonlinePayment.IsPaymentSuccess Then
                    btnSave_Click(sender, e)
                End If
                Exit Sub
            Else
                PhonePeButtonVisibility()
            End If
        End If
        txtRemark.Focus()
        tblCard.Visible = False
        tblCash.Visible = True
        lblPaymentModeVal.Text = "Jio Money"
        TenderMode = "JioMoney"
        txtCollectAmt.Enabled = True
        txtCollectAmt.Text = txtTotalAmt.Text.Trim()

        'added by khusrao adil on 25-07-2018 for innviti with card functionality point _ natural client
        AllowInnovitiPayment = False
        Me.btnCardInnoviti.BackColor = Color.WhiteSmoke
        Me.btnCardInnoviti.ForeColor = Color.Black
        ChangeBtnColor()
        Me.btnJioMoney.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnJioMoney.ForeColor = Color.FromArgb(255, 255, 255)

        Me.btnCard.BackColor = Color.WhiteSmoke
        Me.btnCard.ForeColor = Color.Black

        Me.btnFoodPandaCash.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaCash.ForeColor = Color.Black

        Me.btnFoodPandaOnline.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaOnline.ForeColor = Color.Black

        Me.btnOther.BackColor = Color.WhiteSmoke
        Me.btnOther.ForeColor = Color.Black

        Me.btnCash.BackColor = Color.WhiteSmoke
        Me.btnCash.ForeColor = Color.Black

        Me.btnCheque.BackColor = Color.WhiteSmoke
        Me.btnCheque.ForeColor = Color.Black

        Me.btnPaytm.BackColor = Color.WhiteSmoke
        Me.btnPaytm.ForeColor = Color.Black

        Me.btnMobiKwik.BackColor = Color.WhiteSmoke
        Me.btnMobiKwik.ForeColor = Color.Black

        Me.btnCreditSale.BackColor = Color.WhiteSmoke
        Me.btnCreditSale.ForeColor = Color.Black


        'code added on 31-07-2017 by vipul
        Me.btnSodexo.BackColor = Color.WhiteSmoke
        Me.btnSodexo.ForeColor = Color.Black

        Me.btnTr.BackColor = Color.WhiteSmoke
        Me.btnTr.ForeColor = Color.Black


        'code added by vipul on 15-05-2018
        Me.btnPayso.BackColor = Color.WhiteSmoke
        Me.btnPayso.ForeColor = Color.Black

        Me.btnTicketRestaurant.BackColor = Color.WhiteSmoke
        Me.btnTicketRestaurant.ForeColor = Color.Black


        Me.btnSodexoCards.BackColor = Color.WhiteSmoke
        Me.btnSodexoCards.ForeColor = Color.Black

        Me.btnSodexoCpn.BackColor = Color.WhiteSmoke
        Me.btnSodexoCpn.ForeColor = Color.Black
        Me.btnPhonePe.BackColor = Color.WhiteSmoke
        Me.btnPhonePe.ForeColor = Color.Black
    End Sub

    Private Sub btnOther_Click(sender As Object, e As EventArgs) Handles btnOther.Click

        If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
            If CancelRequestOnTenderChange() = False Then
                If ObjonlinePayment.IsPaymentSuccess Then
                    btnSave_Click(sender, e)
                End If
                Exit Sub
            Else
                PhonePeButtonVisibility()
            End If
        End If

        TenderMode = "Other"
        IsCancelAcceptPayment = True
        txtCollectAmt.Enabled = True
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
    Public Sub Theme()

        Me.lblCollectAmount.TextAlign = ContentAlignment.MiddleLeft
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.btnCash.Dock = DockStyle.None
        Me.btnCash.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnCash.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnCash.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnCash.BringToFront()
        Me.btnCash.Image = Nothing
        Me.btnCash.Size = New System.Drawing.Size(105, 30)
        Me.btnCash.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCash.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnCash.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnCash.FlatStyle = FlatStyle.Flat
        Me.btnCash.FlatAppearance.BorderSize = 1
        Me.btnCash.Text = Me.btnCash.Text.ToUpper()

        Me.btnCard.Dock = DockStyle.None
        Me.btnCard.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnCard.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnCard.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnCard.BringToFront()
        Me.btnCard.Image = Nothing
        Me.btnCard.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCard.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnCard.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnCard.FlatStyle = FlatStyle.Flat
        Me.btnCard.FlatAppearance.BorderSize = 1
        Me.btnCard.Size = New System.Drawing.Size(105, 30)
        Me.btnCard.Text = Me.btnCard.Text.ToUpper()

        ' added by khusrao adil on 25-07-2018
        Me.btnCardInnoviti.Dock = DockStyle.None
        Me.btnCardInnoviti.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnCardInnoviti.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnCardInnoviti.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnCardInnoviti.BringToFront()
        Me.btnCardInnoviti.Image = Nothing
        Me.btnCardInnoviti.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCardInnoviti.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCardInnoviti.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnCardInnoviti.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnCardInnoviti.FlatStyle = FlatStyle.Flat
        Me.btnCardInnoviti.FlatAppearance.BorderSize = 1
        Me.btnCardInnoviti.Size = New System.Drawing.Size(105, 30)
        Me.btnCardInnoviti.Text = Me.btnCardInnoviti.Text.ToUpper()

        Me.btnCheque.Dock = DockStyle.None
        Me.btnCheque.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnCheque.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnCheque.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnCheque.BringToFront()
        Me.btnCheque.Image = Nothing
        Me.btnCheque.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCheque.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCheque.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnCheque.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnCheque.FlatStyle = FlatStyle.Flat
        Me.btnCheque.FlatAppearance.BorderSize = 1
        Me.btnCheque.Size = New System.Drawing.Size(105, 30)
        Me.btnCheque.Text = Me.btnCheque.Text.ToUpper()

        Me.btnPaytm.Dock = DockStyle.None
        Me.btnPaytm.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnPaytm.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnPaytm.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnPaytm.BringToFront()
        Me.btnPaytm.Image = Nothing
        Me.btnPaytm.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnPaytm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPaytm.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnPaytm.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnPaytm.FlatStyle = FlatStyle.Flat
        Me.btnPaytm.FlatAppearance.BorderSize = 1
        Me.btnPaytm.Size = New System.Drawing.Size(105, 30)
        Me.btnPaytm.Text = Me.btnPaytm.Text.ToUpper()

        Me.btnMobiKwik.Dock = DockStyle.None
        Me.btnMobiKwik.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnMobiKwik.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnMobiKwik.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnMobiKwik.BringToFront()
        Me.btnMobiKwik.Image = Nothing
        Me.btnMobiKwik.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnMobiKwik.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnMobiKwik.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnMobiKwik.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnMobiKwik.FlatStyle = FlatStyle.Flat
        Me.btnMobiKwik.FlatAppearance.BorderSize = 1
        Me.btnMobiKwik.Size = New System.Drawing.Size(105, 30)
        Me.btnMobiKwik.Text = Me.btnMobiKwik.Text.ToUpper()

        Me.btnCreditSale.Size = New System.Drawing.Size(105, 30)
        Me.btnCreditSale.Dock = DockStyle.None
        Me.btnCreditSale.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnCreditSale.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnCreditSale.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnCreditSale.BringToFront()
        Me.btnCreditSale.Image = Nothing
        Me.btnCreditSale.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCreditSale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCreditSale.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnCreditSale.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnCreditSale.FlatStyle = FlatStyle.Flat
        Me.btnCreditSale.FlatAppearance.BorderSize = 1
        Me.btnCreditSale.Text = Me.btnCreditSale.Text.ToUpper()

        Me.btnJioMoney.Dock = DockStyle.None
        Me.btnJioMoney.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnJioMoney.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnJioMoney.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnJioMoney.BringToFront()
        Me.btnJioMoney.Image = Nothing
        Me.btnJioMoney.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnJioMoney.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnJioMoney.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnJioMoney.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnJioMoney.FlatStyle = FlatStyle.Flat
        Me.btnJioMoney.FlatAppearance.BorderSize = 1
        Me.btnJioMoney.Size = New System.Drawing.Size(105, 30)
        Me.btnJioMoney.Text = Me.btnJioMoney.Text.ToUpper()



        '----------
        'code added on 31-07-2017 by vipul
        Me.btnSodexo.Dock = DockStyle.None
        Me.btnSodexo.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnSodexo.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnSodexo.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnSodexo.BringToFront()
        Me.btnSodexo.Image = Nothing
        Me.btnSodexo.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSodexo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSodexo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnSodexo.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnSodexo.FlatStyle = FlatStyle.Flat
        Me.btnSodexo.FlatAppearance.BorderSize = 1
        Me.btnSodexo.Size = New System.Drawing.Size(105, 30)
        Me.btnSodexo.Text = Me.btnSodexo.Text.ToUpper()


        Me.btnPhonePe.Dock = DockStyle.None
        Me.btnPhonePe.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnPhonePe.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnPhonePe.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnPhonePe.BringToFront()
        Me.btnPhonePe.Image = Nothing
        Me.btnPhonePe.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnPhonePe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPhonePe.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnPhonePe.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnPhonePe.FlatStyle = FlatStyle.Flat
        Me.btnPhonePe.FlatAppearance.BorderSize = 1
        Me.btnPhonePe.Size = New System.Drawing.Size(105, 30)
        Me.btnPhonePe.Text = Me.btnPaytm.Text.ToUpper()


        Me.btnTr.Dock = DockStyle.None
        Me.btnTr.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnTr.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnTr.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnTr.BringToFront()
        Me.btnTr.Image = Nothing
        Me.btnTr.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnTr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnTr.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnTr.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnTr.FlatStyle = FlatStyle.Flat
        Me.btnTr.FlatAppearance.BorderSize = 1
        Me.btnTr.Size = New System.Drawing.Size(105, 30)
        Me.btnTr.Text = Me.btnTr.Text.ToUpper()


        'code added on 15-05-2018 by vipul
        Me.btnPayso.Dock = DockStyle.None
        Me.btnPayso.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnPayso.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnPayso.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnPayso.BringToFront()
        Me.btnPayso.Image = Nothing
        Me.btnPayso.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnPayso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPayso.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnPayso.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnPayso.FlatStyle = FlatStyle.Flat
        Me.btnPayso.FlatAppearance.BorderSize = 1
        Me.btnPayso.Size = New System.Drawing.Size(105, 30)
        Me.btnPayso.Text = Me.btnPayso.Text.ToUpper()

        Me.btnTicketRestaurant.Dock = DockStyle.None
        Me.btnTicketRestaurant.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnTicketRestaurant.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnTicketRestaurant.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.btnTicketRestaurant.BringToFront()
        Me.btnTicketRestaurant.Image = Nothing
        Me.btnTicketRestaurant.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnTicketRestaurant.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnTicketRestaurant.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnTicketRestaurant.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnTicketRestaurant.FlatStyle = FlatStyle.Flat
        Me.btnTicketRestaurant.FlatAppearance.BorderSize = 1
        Me.btnTicketRestaurant.Size = New System.Drawing.Size(105, 30)
        Me.btnTicketRestaurant.Text = Me.btnTicketRestaurant.Text.ToUpper()


        Me.btnSodexoCards.Dock = DockStyle.None
        Me.btnSodexoCards.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnSodexoCards.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnSodexoCards.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.btnSodexoCards.BringToFront()
        Me.btnSodexoCards.Image = Nothing
        Me.btnSodexoCards.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSodexoCards.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSodexoCards.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnSodexoCards.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnSodexoCards.FlatStyle = FlatStyle.Flat
        Me.btnSodexoCards.FlatAppearance.BorderSize = 1
        Me.btnSodexoCards.Size = New System.Drawing.Size(105, 30)
        Me.btnSodexoCards.Text = Me.btnSodexoCards.Text.ToUpper()

        Me.btnSodexoCpn.Dock = DockStyle.None
        Me.btnSodexoCpn.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnSodexoCpn.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnSodexoCpn.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.btnSodexoCpn.BringToFront()
        Me.btnSodexoCpn.Image = Nothing
        Me.btnSodexoCpn.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSodexoCpn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSodexoCpn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnSodexoCpn.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnSodexoCpn.FlatStyle = FlatStyle.Flat
        Me.btnSodexoCpn.FlatAppearance.BorderSize = 1
        Me.btnSodexoCpn.Size = New System.Drawing.Size(105, 30)
        Me.btnSodexoCpn.Text = Me.btnSodexoCpn.Text.ToUpper()

        Me.btnFoodPandaCash.Dock = DockStyle.None
        Me.btnFoodPandaCash.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnFoodPandaCash.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnFoodPandaCash.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.btnFoodPandaCash.BringToFront()
        Me.btnFoodPandaCash.Image = Nothing
        Me.btnFoodPandaCash.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFoodPandaCash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnFoodPandaCash.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnFoodPandaCash.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnFoodPandaCash.FlatStyle = FlatStyle.Flat
        Me.btnFoodPandaCash.FlatAppearance.BorderSize = 1
        Me.btnFoodPandaCash.Size = New System.Drawing.Size(105, 50)
        Me.btnFoodPandaCash.Text = Me.btnSodexoCards.Text.ToUpper()

        Me.btnFoodPandaOnline.Dock = DockStyle.None
        Me.btnFoodPandaOnline.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnFoodPandaOnline.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnFoodPandaOnline.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.btnFoodPandaOnline.BringToFront()
        Me.btnFoodPandaOnline.Image = Nothing
        Me.btnFoodPandaOnline.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFoodPandaOnline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnFoodPandaOnline.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnFoodPandaOnline.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnFoodPandaOnline.FlatStyle = FlatStyle.Flat
        Me.btnFoodPandaOnline.FlatAppearance.BorderSize = 1
        Me.btnFoodPandaOnline.Size = New System.Drawing.Size(105, 50)
        Me.btnFoodPandaOnline.Text = Me.btnSodexoCards.Text.ToUpper()

        '---------------


        Me.btnOther.Size = New System.Drawing.Size(105, 30)
        Me.btnOther.Dock = DockStyle.None
        Me.btnOther.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnOther.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnOther.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnOther.BringToFront()
        Me.btnOther.Image = Nothing
        Me.btnOther.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnOther.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnOther.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnOther.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnOther.FlatStyle = FlatStyle.Flat
        Me.btnOther.FlatAppearance.BorderSize = 1
        Me.btnOther.Text = Me.btnOther.Text.ToUpper()


        'Me.btnSave.Dock = DockStyle.None
        Me.btnSave.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnSave.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnSave.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        ' MbtnSaveve.Location = New System.Drawing.Point(68, 3)
        'MebtnSavee.Size = New System.Drawing.Size(150, 51)
        Me.btnSave.BringToFront()
        Me.btnSave.Image = Nothing
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnSave.FlatStyle = FlatStyle.Flat
        Me.btnSave.FlatAppearance.BorderSize = 1


        ' Me.btnGift.Dock = DockStyle.None
        Me.btnGift.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnGift.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnGift.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'Me.btnGift.Location = New System.Drawing.Point(68, 3)
        ' Me.btnGift.Size = New System.Drawing.Size(150, 51)
        Me.btnGift.BringToFront()
        Me.btnGift.Image = Nothing
        Me.btnGift.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnGift.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnGift.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnGift.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnGift.FlatStyle = FlatStyle.Flat
        Me.btnGift.FlatAppearance.BorderSize = 1



        'btn Cancle


        Me.btnCancle.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnCancle.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnCancle.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'Me.btnGift.Location = New System.Drawing.Point(68, 3)
        ' Me.btnGift.Size = New System.Drawing.Size(150, 51)
        Me.btnCancle.BringToFront()
        Me.btnCancle.Image = Nothing
        Me.btnCancle.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCancle.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnCancle.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnCancle.FlatStyle = FlatStyle.Flat
        Me.btnCancle.FlatAppearance.BorderSize = 1


        '------------------
        CtrlBtn5.TextAlign = ContentAlignment.MiddleCenter
        CtrlBtn5.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        CtrlBtn5.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtn5.TextAlign = ContentAlignment.MiddleCenter
        CtrlBtn5.BackColor = Color.FromArgb(228, 37, 44)
        CtrlBtn5.ForeColor = Color.White
        CtrlBtn5.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CtrlBtn5.FlatStyle = FlatStyle.Flat
        CtrlBtn5.FlatAppearance.BorderSize = 0
        CtrlBtn5.Image = Nothing
        CtrlBtn5.Location = New Point(620, 3)
        Me.CtrlBtn5.Size = New System.Drawing.Size(50, 29)
        '------------------------




        'CtrlBtn5.FlatStyle = FlatStyle.Flat
        'CtrlBtn5.FlatAppearance.BorderSize = 0
        'Dim gp As New Drawing.Drawing2D.GraphicsPath
        'Dim rect As New Rectangle
        'rect.Location = New Point(3, 5)
        'rect.Size = New Size(27, 27)
        'rect.Inflate(-2, -2)
        'gp.AddEllipse(rect)
        ''  gp.AddEllipse(rect(New Point(3, 3), New Size(25, 26)).Inflate(-5, -5))
        'CtrlBtn5.Region = New Region(gp)
        'Me.CtrlBtn5.Text = ""
        'Me.CtrlBtn5.Dock = DockStyle.Right
        'Me.CtrlBtn5.Size = New System.Drawing.Size(32, 32)
        ''Me.CtrlBtn5.Location = New Point(388, 3)
        'Me.CtrlBtn5.TextImageRelation = TextImageRelation.Overlay
        'Me.CtrlBtn5.ImageAlign = ContentAlignment.MiddleCenter
        'Me.CtrlBtn5.Image = Global.Spectrum.My.Resources.Close_Hover
    End Sub

    Private Function IsAmountReturnTocustomer() As Boolean
        Try
            If (TotalCollectAmt > TotalBillAmt) Then
                Dim ReturnAmt As Decimal
                ReturnAmt = TotalCollectAmt - CDbl(TotalBillAmt)
                Dim strshowmsg As String = ""
                ShowMessage(True, String.Format(getValueByKey("ACPBCS07"), TotalBillAmt, TotalCollectAmt, ReturnAmt, clsAdmin.CurrencyCode), "ACPBCS07 - " & getValueByKey("CLAE04"))
                txtCalCollectAmount.Text = CDbl(txtCalCollectAmount.Text) - ReturnAmt
                Return True
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try


    End Function
    Private Function ValidateEntries(Optional ByVal tenderMode = "") As Boolean
        Try
            If Not (CheckInteger(txtCalCollectAmount.Text)) Then
                ShowMessage(getValueByKey("ACP002"), "ACP002 - " & getValueByKey("CLAE04"))
                Return False
            End If
            If Not (CheckInteger(txtCollectAmt.Text)) Then
                ShowMessage(getValueByKey("ACP002"), "ACP002 - " & getValueByKey("CLAE04"))
                Return False
            End If
            If tenderMode <> "Card" Then
                If TotalCollectAmt < TotalBillAmt Then 'added by adil 2014
                    txtCollectAmt.Focus()
                    ShowMessage("Amount is not Settle", "CM032 - " & "Information")
                    Return False
                End If
            Else
                '------------------------------------------------------------------------------------------------
                'added on 15 may - ashma - for Innoviti (for hiding Credit Card No)
                If Not clsDefaultConfiguration.PayFromInnoviti Then
                    '-------------------------------------------------------
                    If clsDefaultConfiguration.CreditCardInfo = False Then
                        If (String.IsNullOrEmpty(txtCardNo.Text.Trim())) Then
                            txtCardNo.Focus()
                            ShowMessage(getValueByKey("ACP006"), "ACP006 - " & getValueByKey("CLAE04"))
                            Return False
                        ElseIf Not (txtCardNo.Text.Length >= 4 AndAlso txtCardNo.Text.Length <= 16) Then
                            txtCardNo.Focus()
                            ShowMessage(getValueByKey("ACP021"), "ACP021 - " & getValueByKey("CLAE04"))
                            Return False
                        Else
                            Return True
                        End If
                    Else
                        If cmbBankName.SelectedIndex = -1 Then
                            cmbBankName.Focus()
                            ShowMessage(getValueByKey("CHKP09"), "CHKP09 - " & getValueByKey("CLAE04"))
                            Return False
                        End If

                        If (String.IsNullOrEmpty(txtCardNo.Text)) Then
                            txtCardNo.Focus()
                            ShowMessage(getValueByKey("ACP006"), "ACP006 - " & getValueByKey("CLAE04"))
                            Return False

                        ElseIf Not (txtCardNo.Text.Length >= 4 AndAlso txtCardNo.Text.Length <= 16) Then
                            txtCardNo.Focus()
                            ShowMessage(getValueByKey("ACP021"), "ACP021 - " & getValueByKey("CLAE04"))
                            Return False
                        Else
                            Return True
                        End If
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    'added by khusrao on 19-04-2017 for configurable tender
    Private Function TenderModeAvailable()
        Try
            btnCash.Visible = False
            btnCard.Visible = False
            'added by khusrao adil on 25-07-2018
            btnCardInnoviti.Visible = False
            btnCheque.Visible = False
            btnPaytm.Visible = False
            btnMobiKwik.Visible = False
            btnJioMoney.Visible = False
            btnOther.Visible = False
            btnCreditSale.Visible = False
            btnFoodPandaCash.Visible = False
            btnFoodPandaOnline.Visible = False

            'code added on 31-07-2017 by vipul
            btnSodexo.Visible = False
            btnTr.Visible = False
            'code added by vipul on 15-05-2018
            btnPayso.Visible = False
            btnTicketRestaurant.Visible = False

            btnSodexoCards.Visible = False
            btnSodexoCpn.Visible = False
            btnPhonePe.Visible = False
            Dim dtTenderVissible As DataTable
            dtTenderVissible = objClsComm.GetVisibleTender(clsAdmin.SiteCode)
            If dtTenderVissible.Rows.Count > 0 Then

                Dim visiblebuttoncount As Integer = 0
                '------------------------ middle code
                VisbilityOfButtonsChenge(dtTenderVissible:=dtTenderVissible, visiblebuttoncount:=visiblebuttoncount, VisibleButtonCountReq:=True)
                If visiblebuttoncount = 1 Then
                    If btnCash.Visible Then
                        tblButtonsPnl.Controls.Add(btnCash, 2, 0)
                    End If
                    If btnCard.Visible Then
                        tblButtonsPnl.Controls.Add(btnCard, 2, 0)
                    End If
                    'added by khusrao adil on 25-07-2018 for innviti with card functionality point _ natural client
                    If btnCardInnoviti.Visible Then
                        tblButtonsPnl.Controls.Add(btnCardInnoviti, 2, 0)
                    End If
                    If btnCheque.Visible Then
                        tblButtonsPnl.Controls.Add(btnCheque, 2, 0)
                    End If
                    If btnCreditSale.Visible Then
                        tblButtonsPnl.Controls.Add(btnCreditSale, 2, 0)
                    End If
                    If btnPaytm.Visible Then
                        tblButtonsPnl.Controls.Add(btnPaytm, 2, 0)
                    End If
                    If btnMobiKwik.Visible Then
                        tblButtonsPnl.Controls.Add(btnMobiKwik, 2, 0)
                    End If
                    If btnJioMoney.Visible Then
                        tblButtonsPnl.Controls.Add(btnJioMoney, 2, 0)
                    End If
                    'code added on 31-07-2017 by vipul
                    If btnSodexo.Visible Then
                        tblButtonsPnl.Controls.Add(btnSodexo, 2, 0)
                    End If
                    If btnTr.Visible Then
                        tblButtonsPnl.Controls.Add(btnTr, 2, 0)
                    End If

                    'code added by vipul on 15-05-2018
                    If btnPayso.Visible Then
                        tblButtonsPnl.Controls.Add(btnPayso, 2, 0)
                    End If
                    If btnTicketRestaurant.Visible Then
                        tblButtonsPnl.Controls.Add(btnTicketRestaurant, 2, 0)
                    End If

                    If btnSodexoCards.Visible Then
                        tblButtonsPnl.Controls.Add(btnSodexoCards, 2, 0)
                    End If
                    If btnSodexoCpn.Visible Then
                        tblButtonsPnl.Controls.Add(btnSodexoCpn, 2, 0)
                    End If
                    If btnOther.Visible Then
                        tblButtonsPnl.Controls.Add(btnOther, 2, 0)
                    End If
                    If btnFoodPandaCash.Visible Then
                        tblButtonsPnl.Controls.Add(btnFoodPandaCash, 2, 0)
                    End If
                    If btnFoodPandaOnline.Visible Then
                        tblButtonsPnl.Controls.Add(btnFoodPandaOnline, 2, 0)
                    End If
                    If btnPhonePe.Visible Then
                        tblButtonsPnl.Controls.Add(btnPhonePe, 2, 0)
                    End If
                ElseIf visiblebuttoncount = 2 Then
                    For i = 1 To 3
                        If btnCash.Visible Then
                            tblButtonsPnl.Controls.Add(btnCash, i, 0)
                            i = i + 1
                            btnCash.Visible = False
                            Continue For
                        End If
                        If btnCard.Visible Then
                            tblButtonsPnl.Controls.Add(btnCard, i, 0)
                            i = i + 1
                            btnCard.Visible = False
                            Continue For
                        End If
                        'code added by khusrao adil on 25-07-2018
                        If btnCardInnoviti.Visible Then
                            tblButtonsPnl.Controls.Add(btnCardInnoviti, i, 0)
                            i = i + 1
                            btnCardInnoviti.Visible = False
                            Continue For
                        End If
                        If btnCreditSale.Visible Then
                            tblButtonsPnl.Controls.Add(btnCreditSale, i, 0)
                            i = i + 1
                            btnCreditSale.Visible = False
                            Continue For
                        End If
                        If btnCheque.Visible Then
                            tblButtonsPnl.Controls.Add(btnCheque, i, 0)
                            i = i + 1
                            btnCheque.Visible = False
                            Continue For
                        End If
                        If btnPaytm.Visible Then
                            tblButtonsPnl.Controls.Add(btnPaytm, i, 0)
                            i = i + 1
                            btnPaytm.Visible = False
                            Continue For
                        End If
                        If btnMobiKwik.Visible Then
                            tblButtonsPnl.Controls.Add(btnMobiKwik, i, 0)
                            i = i + 1
                            btnMobiKwik.Visible = False
                            Continue For
                        End If
                        If btnJioMoney.Visible Then
                            tblButtonsPnl.Controls.Add(btnJioMoney, i, 0)
                            i = i + 1
                            btnJioMoney.Visible = False
                            Continue For
                        End If
                        'code added on 31-07-2017 by vipul
                        If btnSodexo.Visible Then
                            tblButtonsPnl.Controls.Add(btnSodexo, i, 0)
                            i = i + 1
                            btnSodexo.Visible = False
                            Continue For
                        End If

                        If btnTr.Visible Then
                            tblButtonsPnl.Controls.Add(btnTr, i, 0)
                            i = i + 1
                            btnTr.Visible = False
                            Continue For
                        End If


                        'code added by vipul on 15-05-2018
                        If btnPayso.Visible Then
                            tblButtonsPnl.Controls.Add(btnPayso, i, 0)
                            i = i + 1
                            btnPayso.Visible = False
                            Continue For
                        End If
                        If btnTicketRestaurant.Visible Then
                            tblButtonsPnl.Controls.Add(btnTicketRestaurant, i, 0)
                            i = i + 1
                            btnTicketRestaurant.Visible = False
                            Continue For
                        End If

                        If btnSodexoCards.Visible Then
                            tblButtonsPnl.Controls.Add(btnSodexoCards, i, 0)
                            i = i + 1
                            btnSodexoCards.Visible = False
                            Continue For
                        End If
                        If btnSodexoCpn.Visible Then
                            tblButtonsPnl.Controls.Add(btnSodexoCpn, i, 0)
                            i = i + 1
                            btnSodexoCpn.Visible = False
                            Continue For
                        End If
                        If btnOther.Visible Then
                            tblButtonsPnl.Controls.Add(btnOther, i, 0)
                            i = i + 1
                            btnOther.Visible = False
                            Continue For
                        End If
                        If btnFoodPandaCash.Visible Then
                            tblButtonsPnl.Controls.Add(btnFoodPandaCash, i, 0)
                            i = i + 1
                            btnFoodPandaCash.Visible = False
                            Continue For
                        End If
                        If btnFoodPandaOnline.Visible Then
                            tblButtonsPnl.Controls.Add(btnFoodPandaOnline, i, 0)
                            i = i + 1
                            btnFoodPandaOnline.Visible = False
                            Continue For
                        End If
                        If btnPhonePe.Visible Then
                            tblButtonsPnl.Controls.Add(btnPhonePe, i, 0)
                            i = i + 1
                            btnPhonePe.Visible = False
                            Continue For
                        End If

                    Next
                ElseIf visiblebuttoncount = 3 Or visiblebuttoncount = 4 Or visiblebuttoncount = 5 Then
                    Dim IValue As Integer = 0
                    If visiblebuttoncount = 3 Then
                        IValue = 1
                    End If
                    For i = IValue To 4
                        If btnCash.Visible Then
                            tblButtonsPnl.Controls.Add(btnCash, i, 0)
                            btnCash.Visible = False
                            Continue For
                        End If
                        If btnCard.Visible Then
                            tblButtonsPnl.Controls.Add(btnCard, i, 0)
                            btnCard.Visible = False
                            Continue For
                        End If
                        'code added by khusrao adil on 25-07-2018
                        If btnCardInnoviti.Visible Then
                            tblButtonsPnl.Controls.Add(btnCardInnoviti, i, 0)
                            btnCardInnoviti.Visible = False
                            Continue For
                        End If
                        If btnCreditSale.Visible Then
                            tblButtonsPnl.Controls.Add(btnCreditSale, i, 0)
                            btnCreditSale.Visible = False
                            Continue For
                        End If
                        If btnCheque.Visible Then
                            tblButtonsPnl.Controls.Add(btnCheque, i, 0)
                            btnCheque.Visible = False
                            Continue For
                        End If
                        If btnPaytm.Visible Then
                            tblButtonsPnl.Controls.Add(btnPaytm, i, 0)
                            btnPaytm.Visible = False
                            Continue For
                        End If
                        If btnMobiKwik.Visible Then
                            tblButtonsPnl.Controls.Add(btnMobiKwik, i, 0)
                            btnMobiKwik.Visible = False
                            Continue For
                        End If
                        If btnJioMoney.Visible Then
                            tblButtonsPnl.Controls.Add(btnJioMoney, i, 0)
                            btnJioMoney.Visible = False
                            Continue For
                        End If
                        'code added on 31-07-2017 by vipul
                        If btnSodexo.Visible Then
                            tblButtonsPnl.Controls.Add(btnSodexo, i, 0)
                            btnSodexo.Visible = False
                            Continue For
                        End If
                        If btnTr.Visible Then
                            tblButtonsPnl.Controls.Add(btnTr, i, 0)
                            btnTr.Visible = False
                            Continue For
                        End If

                        'code added by vipul on 15-05-2018
                        If btnPayso.Visible Then
                            tblButtonsPnl.Controls.Add(btnPayso, i, 0)
                            btnPayso.Visible = False
                            Continue For
                        End If

                        If btnTicketRestaurant.Visible Then
                            tblButtonsPnl.Controls.Add(btnTicketRestaurant, i, 0)
                            btnTicketRestaurant.Visible = False
                            Continue For
                        End If

                        If btnSodexoCards.Visible Then
                            tblButtonsPnl.Controls.Add(btnSodexoCards, i, 0)
                            btnSodexoCards.Visible = False
                            Continue For
                        End If

                        If btnSodexoCpn.Visible Then
                            tblButtonsPnl.Controls.Add(btnSodexoCpn, i, 0)
                            btnSodexoCpn.Visible = False
                            Continue For
                        End If
                        If btnOther.Visible Then
                            tblButtonsPnl.Controls.Add(btnOther, i, 0)
                            btnOther.Visible = False
                            Continue For
                        End If
                        If btnFoodPandaCash.Visible Then
                            tblButtonsPnl.Controls.Add(btnFoodPandaCash, i, 0)
                            btnFoodPandaCash.Visible = False
                            Continue For
                        End If
                        If btnFoodPandaOnline.Visible Then
                            tblButtonsPnl.Controls.Add(btnFoodPandaOnline, i, 0)
                            btnFoodPandaOnline.Visible = False
                            Continue For
                        End If

                        If btnPhonePe.Visible Then
                            tblButtonsPnl.Controls.Add(btnPhonePe, i, 0)
                            btnPhonePe.Visible = False
                            Continue For
                        End If
                    Next
                ElseIf visiblebuttoncount > 5 Then
                    For i = 0 To 3
                        For j = 0 To 4
                            If btnCash.Visible Then
                                tblButtonsPnl.Controls.Add(btnCash, j, i)
                                btnCash.Visible = False
                                Continue For
                            End If
                            If btnCard.Visible Then
                                tblButtonsPnl.Controls.Add(btnCard, j, i)
                                btnCard.Visible = False
                                Continue For
                            End If
                            'code added by khusrao adil on 25-07-2018
                            If btnCardInnoviti.Visible Then
                                tblButtonsPnl.Controls.Add(btnCardInnoviti, j, i)
                                btnCardInnoviti.Visible = False
                                Continue For
                            End If
                            If btnCreditSale.Visible Then
                                tblButtonsPnl.Controls.Add(btnCreditSale, j, i)
                                btnCreditSale.Visible = False
                                Continue For
                            End If
                            If btnCheque.Visible Then
                                tblButtonsPnl.Controls.Add(btnCheque, j, i)
                                btnCheque.Visible = False
                                Continue For
                            End If
                            If btnPaytm.Visible Then
                                tblButtonsPnl.Controls.Add(btnPaytm, j, i)
                                btnPaytm.Visible = False
                                Continue For
                            End If
                            If btnMobiKwik.Visible Then
                                tblButtonsPnl.Controls.Add(btnMobiKwik, j, i)
                                btnMobiKwik.Visible = False
                                Continue For
                            End If
                            If btnJioMoney.Visible Then
                                tblButtonsPnl.Controls.Add(btnJioMoney, j, i)
                                btnJioMoney.Visible = False
                                Continue For
                            End If
                            'code added on 31-07-2017 by vipul

                            If btnSodexo.Visible Then
                                tblButtonsPnl.Controls.Add(btnSodexo, j, i)
                                btnSodexo.Visible = False
                                Continue For
                            End If
                            If btnTr.Visible Then
                                tblButtonsPnl.Controls.Add(btnTr, j, i)
                                btnTr.Visible = False
                                Continue For
                            End If
                            'code added by vipul on 15-05-2018
                            If btnPayso.Visible Then
                                tblButtonsPnl.Controls.Add(btnPayso, j, i)
                                btnPayso.Visible = False
                                Continue For
                            End If

                            If btnTicketRestaurant.Visible Then
                                tblButtonsPnl.Controls.Add(btnTicketRestaurant, j, i)
                                btnTicketRestaurant.Visible = False
                                Continue For
                            End If


                            If btnSodexoCards.Visible Then
                                tblButtonsPnl.Controls.Add(btnSodexoCards, j, i)
                                btnSodexoCards.Visible = False
                                Continue For
                            End If


                            If btnSodexoCpn.Visible Then
                                tblButtonsPnl.Controls.Add(btnSodexoCpn, j, i)
                                btnSodexoCpn.Visible = False
                                Continue For
                            End If
                            If btnOther.Visible Then
                                tblButtonsPnl.Controls.Add(btnOther, j, i)
                                btnOther.Visible = False
                                Continue For
                            End If
                            If btnFoodPandaCash.Visible Then
                                tblButtonsPnl.Controls.Add(btnFoodPandaCash, j, i)
                                btnFoodPandaCash.Visible = False
                                Continue For
                            End If
                            If btnFoodPandaOnline.Visible Then
                                tblButtonsPnl.Controls.Add(btnFoodPandaOnline, j, i)
                                btnFoodPandaOnline.Visible = False
                                Continue For
                            End If
                            If btnPhonePe.Visible Then
                                tblButtonsPnl.Controls.Add(btnPhonePe, j, i)
                                btnPhonePe.Visible = False
                                Continue For
                            End If
                        Next
                    Next
                End If

                VisbilityOfButtonsChenge(dtTenderVissible:=dtTenderVissible, visiblebuttoncount:=visiblebuttoncount, VisibleButtonCountReq:=False)
            Else
                btnCash.Visible = True
                tblButtonsPnl.Controls.Add(btnCash, 2, 0)
            End If

            ' OtherVisibleTenderButton()


        Catch ex As Exception
            LogException(ex)
            Return ""
        End Try
    End Function
    Public Sub VisbilityOfButtonsChenge(ByRef dtTenderVissible As DataTable, ByRef visiblebuttoncount As Integer, Optional VisibleButtonCountReq As Boolean = False)
        Try
            Dim dataView As New DataView(dtTenderVissible)
            dataView.Sort = "TenderType ASC"
            Dim Dt As DataTable = dataView.ToTable()

            For Each dr As DataRow In Dt.Rows
                If dr("TenderType") = "Cash" Then
                    btnCash.Visible = True
                    btnCash.Text = dr("TenderHeadCode")
                    If VisibleButtonCountReq Then
                        visiblebuttoncount = visiblebuttoncount + 1
                    End If
                ElseIf dr("TenderType") = "Credit" Then
                    btnCreditSale.Visible = True
                    btnCreditSale.Text = dr("TenderHeadCode")
                    If VisibleButtonCountReq Then
                        visiblebuttoncount = visiblebuttoncount + 1
                    End If

                ElseIf dr("TenderType") = "CreditCard" Or dr("TenderType") = "DebitCard" Or dr("TenderType") = "Card" Then 'modified by khusrao adil on 25-07-2018
                    btnCard.Visible = True
                    'btnCard.Text = dr("TenderHeadCode")
                    'added by khusrao adil on 25-07-2018 for innviti with card functionality point _ natural client
                    Dim strtender = dr("TenderHeadCode")
                    btnCard.Text = strtender.Substring(strtender.Length - 4)
                    If VisibleButtonCountReq Then
                        visiblebuttoncount = visiblebuttoncount + 1
                    End If
                    'added by khusrao adil on 25-07-2018
                    If clsDefaultConfiguration.PayFromInnoviti = True And clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) Then
                        btnCardInnoviti.Visible = True
                        btnCardInnoviti.Text = "Card Inv"
                        If VisibleButtonCountReq Then
                            visiblebuttoncount = visiblebuttoncount + 1
                        End If
                    End If
                ElseIf dr("TenderType") = "Cheque" Then
                    btnCheque.Visible = True
                    btnCheque.Text = dr("TenderHeadCode")
                    If VisibleButtonCountReq Then
                        visiblebuttoncount = visiblebuttoncount + 1
                    End If

                ElseIf dr("TenderType") = "Paytm" Then
                    btnPaytm.Visible = True
                    btnPaytm.Text = dr("TenderHeadCode")
                    If VisibleButtonCountReq Then
                        visiblebuttoncount = visiblebuttoncount + 1
                    End If

                ElseIf dr("TenderType") = "MobiKwik" Then
                    btnMobiKwik.Visible = True
                    btnMobiKwik.Text = dr("TenderHeadCode")
                    If VisibleButtonCountReq Then
                        visiblebuttoncount = visiblebuttoncount + 1
                    End If
                ElseIf dr("TenderType") = "JioMoney" Then
                    btnJioMoney.Visible = True
                    btnJioMoney.Text = dr("TenderHeadCode")
                    If VisibleButtonCountReq Then
                        visiblebuttoncount = visiblebuttoncount + 1
                    End If

                ElseIf dr("TenderType") = "Sodexo" Then
                    btnSodexo.Visible = True
                    btnSodexo.Text = dr("TenderHeadCode")
                    If VisibleButtonCountReq Then
                        visiblebuttoncount = visiblebuttoncount + 1
                    End If
                ElseIf dr("TenderType") = "Tr" Then
                    btnTr.Visible = True
                    btnTr.Text = dr("TenderHeadCode")
                    If VisibleButtonCountReq Then
                        visiblebuttoncount = visiblebuttoncount + 1
                    End If
                ElseIf dr("TenderType") = "FoodPandaCash" Then
                    btnFoodPandaCash.Visible = True
                    btnFoodPandaCash.Text = dr("TenderHeadCode")
                    If VisibleButtonCountReq Then
                        visiblebuttoncount = visiblebuttoncount + 1
                    End If
                ElseIf dr("TenderType") = "FoodPandaOnline" Then
                    btnFoodPandaOnline.Visible = True
                    btnFoodPandaOnline.Text = dr("TenderHeadCode")
                    If VisibleButtonCountReq Then
                        visiblebuttoncount = visiblebuttoncount + 1
                    End If
                ElseIf dr("TenderType") = "Payso" Then
                    btnPayso.Visible = True
                    btnPayso.Text = dr("TenderHeadCode")
                    If VisibleButtonCountReq Then
                        visiblebuttoncount = visiblebuttoncount + 1
                    End If

                ElseIf dr("TenderType") = "TktRestaurant" Then
                    btnTicketRestaurant.Visible = True
                    btnTicketRestaurant.Text = dr("TenderHeadCode")
                    If VisibleButtonCountReq Then
                        visiblebuttoncount = visiblebuttoncount + 1
                    End If

                ElseIf dr("TenderType") = "SodexoCards" Then
                    btnSodexoCards.Visible = True
                    btnSodexoCards.Text = dr("TenderHeadCode")
                    If VisibleButtonCountReq Then
                        visiblebuttoncount = visiblebuttoncount + 1
                    End If
                ElseIf dr("TenderType") = "SodexoCpn" Then
                    btnSodexoCpn.Visible = True
                    btnSodexoCpn.Text = dr("TenderHeadCode")
                    If VisibleButtonCountReq Then
                        visiblebuttoncount = visiblebuttoncount + 1
                    End If

                ElseIf dr("TenderType") = "PhonePe" Then
                    btnPhonePe.Visible = True
                    btnPhonePe.Text = dr("TenderHeadCode")
                    If VisibleButtonCountReq Then
                        visiblebuttoncount = visiblebuttoncount + 1
                    End If

                ElseIf dr("TenderType") = "Others" Then
                    btnOther.Visible = True
                    btnOther.Text = dr("TenderHeadCode")
                    If VisibleButtonCountReq Then
                        visiblebuttoncount = visiblebuttoncount + 1
                    End If
                End If
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    ''added on 23-06-2017 by vipul
    Private Sub btnGift_Click(sender As Object, e As EventArgs) Handles btnGift.Click
        Try
            If TenderMode.ToString.ToUpper.Equals("CHEQUE") Then
                If CustName = "" Then
                    ShowMessage(getValueByKey("Crs015"), "CM030 - " & getValueByKey("CLAE05"))
                    Exit Sub
                End If

                If String.IsNullOrEmpty(txtCardNo.Text.ToString) Then
                    ShowMessage("Please enter cheque Number", "Information")
                    Exit Sub
                End If

                If String.IsNullOrEmpty(dtpExpiryDateCheque.Value.ToString) Then
                    ShowMessage("Please select the date", "Information")
                    Exit Sub
                End If

                If String.IsNullOrEmpty(txtmicrno.Text.ToString) Then
                    ShowMessage("Please enter MICR Number", "Information")
                    Exit Sub
                End If
                CreateDatatableforCheque(dtCheque)
            End If
            IsGiftVouchheReq = True
            TotalCollectAmt = txtCollectAmt.Text
            TotalBillAmt = txtCalCollectAmount.Text
            If ValidateEntries() Then

                If Not ValidateEntries("Cash") Then
                    IsCancelAcceptPayment = False
                    Exit Sub
                Else
                    If IsAmountReturnTocustomer() Then
                        Try
                            If clsAdmin.IsCashDrawer Then '---Code added by Mahesh for checking Cash Drawer is available or not
                                Dim cA4Print As New clsA4Print
                                cA4Print.OperateDevice("CashDrawer", CDflag:=clsDefaultConfiguration.CDBuildCode)
                            End If
                        Catch ex As Exception
                            LogException(ex)
                        End Try
                        IsCancelAcceptPayment = False
                        _Actiontype = My.Resources.AcceptPaymentActionTypeGift.ToString()
                        If DocumentType = "SO" Then
                            If _IsCashierPromoSelected = True AndAlso (String.IsNullOrEmpty(txtRemark.Text) Or HasAlphaNumericChar(txtRemark.Text) = False) Then
                                ShowMessage(getValueByKey("frmnacceptpaymentbycash.txtremarkvalid"), getValueByKey("CLAE04"))
                                IsCancelAcceptPayment = True
                            ElseIf (PaymentTransactionByShortCutForms(TotalCollectAmt, SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cash.ToString(), SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cash, dsRecieptType)) Then
                                GiftReceiptMessage = GetGiftMessage()
                                If GiftReceiptMessage = String.Empty Then
                                    IsCancelAcceptPayment = True
                                    Exit Sub
                                End If
                                Me.Close()
                            Else
                                ShowMessage(getValueByKey("ACPBCS01"), "ACPBCS01 - " & getValueByKey("CLAE05"))
                            End If
                        Else
                            If _IsCashierPromoSelected = True AndAlso (String.IsNullOrEmpty(txtRemark.Text) Or HasAlphaNumericChar(txtRemark.Text) = False) Then
                                ShowMessage(getValueByKey("frmnacceptpaymentbycash.txtremarkvalid"), getValueByKey("CLAE04"))
                                IsCancelAcceptPayment = True
                            ElseIf (PaymentTransactionByShortCutForms(TotalBillAmt, SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cash.ToString(), SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cash, dsRecieptType)) Then
                                GiftReceiptMessage = GetGiftMessage()
                                If GiftReceiptMessage = String.Empty Then
                                    IsCancelAcceptPayment = True
                                    dsRecieptType = New DataSet()
                                    Exit Sub
                                End If
                                Me.Close()
                            Else
                                ShowMessage(getValueByKey("ACPBCS01"), "ACPBCS01 - " & getValueByKey("CLAE05"))
                            End If
                        End If

                    Else
                        ShowMessage(getValueByKey("ACPBCS02"), "ACPBCS02 - " & getValueByKey("CLAE05"))
                    End If
                End If
                IsCancelAcceptPayment = True
            Else
                Exit Sub
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
    Private Sub frmNAcceptPaymentByCash_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F10 Then
                If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
                    If Not TenderMode.ToUpper().Equals("PHONEPE") Then
                        btnSave_Click(btnSave, New System.EventArgs)
                    End If
                Else
                    btnSave_Click(btnSave, New System.EventArgs)
                End If
            ElseIf e.KeyCode = Keys.Escape Then
                If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
                    If TenderMode.ToUpper() = "PHONEPE" Then
                        CtrlBtnCancle_Click(sender, e)
                    End If
                End If

            ElseIf e.KeyCode = Keys.F11 Then
                If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
                    If Not TenderMode.ToUpper().Equals("PHONEPE") Then
                        btnGift_Click(btnGift, New System.EventArgs)
                    End If
                Else
                    btnGift_Click(btnGift, New System.EventArgs)
                End If

            ElseIf e.KeyCode = Keys.Enter Then
                If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
                    If TenderMode.ToUpper() = "PHONEPE" Then
                        CtrlBtnRequest_Click(sender, e)
                    Else
                        btnSave_Click(btnSave, New System.EventArgs)
                    End If
                Else
                    btnSave_Click(btnSave, New System.EventArgs)
                End If

            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    'code added on 31-07-2017 by vipul
    Private Sub btnSodexo_Click(sender As Object, e As EventArgs) Handles btnSodexo.Click
        If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
            If CancelRequestOnTenderChange() = False Then
                If ObjonlinePayment.IsPaymentSuccess Then
                    btnSave_Click(sender, e)
                End If
                Exit Sub
            Else
                PhonePeButtonVisibility()
            End If
        End If

        tblCard.Visible = False
        tblCash.Visible = True
        lblPaymentModeVal.Text = "Sodexo"
        TenderMode = "Sodexo"

        'added by khusrao adil on 25-07-2018
        AllowInnovitiPayment = False
        Me.btnCardInnoviti.BackColor = Color.WhiteSmoke
        Me.btnCardInnoviti.ForeColor = Color.Black

        txtCollectAmt.Text = txtTotalAmt.Text.Trim()
        txtCollectAmt.Enabled = False
        ChangeBtnColor()
        Me.btnSodexo.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnSodexo.ForeColor = Color.FromArgb(255, 255, 255)

        Me.btnCard.BackColor = Color.WhiteSmoke
        Me.btnCard.ForeColor = Color.Black

        Me.btnFoodPandaCash.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaCash.ForeColor = Color.Black

        Me.btnFoodPandaOnline.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaOnline.ForeColor = Color.Black

        Me.btnOther.BackColor = Color.WhiteSmoke
        Me.btnOther.ForeColor = Color.Black

        Me.btnCash.BackColor = Color.WhiteSmoke
        Me.btnCash.ForeColor = Color.Black

        Me.btnCheque.BackColor = Color.WhiteSmoke
        Me.btnCheque.ForeColor = Color.Black

        Me.btnMobiKwik.BackColor = Color.WhiteSmoke
        Me.btnMobiKwik.ForeColor = Color.Black

        Me.btnJioMoney.BackColor = Color.WhiteSmoke
        Me.btnJioMoney.ForeColor = Color.Black

        Me.btnCreditSale.BackColor = Color.WhiteSmoke
        Me.btnCreditSale.ForeColor = Color.Black

        Me.btnTr.BackColor = Color.WhiteSmoke
        Me.btnTr.ForeColor = Color.Black

        'code added by vipul on 15-05-2018
        Me.btnPayso.BackColor = Color.WhiteSmoke
        Me.btnPayso.ForeColor = Color.Black

        Me.btnPaytm.BackColor = Color.WhiteSmoke
        Me.btnPaytm.ForeColor = Color.Black

        Me.btnTicketRestaurant.BackColor = Color.WhiteSmoke
        Me.btnTicketRestaurant.ForeColor = Color.Black

        Me.btnSodexoCards.BackColor = Color.WhiteSmoke
        Me.btnSodexoCards.ForeColor = Color.Black

        Me.btnSodexoCpn.BackColor = Color.WhiteSmoke
        Me.btnSodexoCpn.ForeColor = Color.Black
        Me.btnPhonePe.BackColor = Color.WhiteSmoke
        Me.btnPhonePe.ForeColor = Color.Black
    End Sub
    'code added on 31-07-2017 by vipul
    Private Sub btnTr_Click(sender As Object, e As EventArgs) Handles btnTr.Click
        If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
            If CancelRequestOnTenderChange() = False Then
                If ObjonlinePayment.IsPaymentSuccess Then
                    btnSave_Click(sender, e)
                End If
                Exit Sub
            Else
                PhonePeButtonVisibility()
            End If
        End If

        tblCard.Visible = False
        tblCash.Visible = True
        lblPaymentModeVal.Text = "Tr"
        TenderMode = "Tr"

        'added by khusrao adil on 25-07-2018
        AllowInnovitiPayment = False
        Me.btnCardInnoviti.BackColor = Color.WhiteSmoke
        Me.btnCardInnoviti.ForeColor = Color.Black

        txtCollectAmt.Text = txtTotalAmt.Text.Trim()
        txtCollectAmt.Enabled = False
        ChangeBtnColor()
        Me.btnTr.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnTr.ForeColor = Color.FromArgb(255, 255, 255)

        Me.btnCard.BackColor = Color.WhiteSmoke
        Me.btnCard.ForeColor = Color.Black

        Me.btnFoodPandaCash.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaCash.ForeColor = Color.Black

        Me.btnFoodPandaOnline.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaOnline.ForeColor = Color.Black

        Me.btnOther.BackColor = Color.WhiteSmoke
        Me.btnOther.ForeColor = Color.Black

        Me.btnCash.BackColor = Color.WhiteSmoke
        Me.btnCash.ForeColor = Color.Black

        Me.btnCheque.BackColor = Color.WhiteSmoke
        Me.btnCheque.ForeColor = Color.Black

        Me.btnMobiKwik.BackColor = Color.WhiteSmoke
        Me.btnMobiKwik.ForeColor = Color.Black

        Me.btnJioMoney.BackColor = Color.WhiteSmoke
        Me.btnJioMoney.ForeColor = Color.Black

        Me.btnCreditSale.BackColor = Color.WhiteSmoke
        Me.btnCreditSale.ForeColor = Color.Black

        Me.btnCreditSale.BackColor = Color.WhiteSmoke
        Me.btnCreditSale.ForeColor = Color.Black

        Me.btnSodexo.BackColor = Color.WhiteSmoke
        Me.btnSodexo.ForeColor = Color.Black

        'code added by vipul on 15-05-2018
        Me.btnPayso.BackColor = Color.WhiteSmoke
        Me.btnPayso.ForeColor = Color.Black

        Me.btnTicketRestaurant.BackColor = Color.WhiteSmoke
        Me.btnTicketRestaurant.ForeColor = Color.Black

        Me.btnSodexoCards.BackColor = Color.WhiteSmoke
        Me.btnSodexoCards.ForeColor = Color.Black

        Me.btnSodexoCpn.BackColor = Color.WhiteSmoke
        Me.btnSodexoCpn.ForeColor = Color.Black
        Me.btnPhonePe.BackColor = Color.WhiteSmoke
        Me.btnPhonePe.ForeColor = Color.Black
    End Sub
    Private Sub btnPayso_Click(sender As Object, e As EventArgs) Handles btnPayso.Click
        If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
            If CancelRequestOnTenderChange() = False Then
                If ObjonlinePayment.IsPaymentSuccess Then
                    btnSave_Click(sender, e)
                End If
                Exit Sub
            Else
                PhonePeButtonVisibility()
            End If
        End If

        tblCard.Visible = False
        tblCash.Visible = True
        lblPaymentModeVal.Text = "Payso"
        TenderMode = "Payso"

        'added by khusrao adil on 25-07-2018
        AllowInnovitiPayment = False
        Me.btnCardInnoviti.BackColor = Color.WhiteSmoke
        Me.btnCardInnoviti.ForeColor = Color.Black

        txtCollectAmt.Text = txtTotalAmt.Text.Trim()
        txtCollectAmt.Enabled = False
        ChangeBtnColor()
        Me.btnPayso.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnPayso.ForeColor = Color.FromArgb(255, 255, 255)

        Me.btnCard.BackColor = Color.WhiteSmoke
        Me.btnCard.ForeColor = Color.Black

        Me.btnFoodPandaCash.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaCash.ForeColor = Color.Black

        Me.btnFoodPandaOnline.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaOnline.ForeColor = Color.Black

        Me.btnOther.BackColor = Color.WhiteSmoke
        Me.btnOther.ForeColor = Color.Black

        Me.btnCash.BackColor = Color.WhiteSmoke
        Me.btnCash.ForeColor = Color.Black

        Me.btnCheque.BackColor = Color.WhiteSmoke
        Me.btnCheque.ForeColor = Color.Black

        Me.btnMobiKwik.BackColor = Color.WhiteSmoke
        Me.btnMobiKwik.ForeColor = Color.Black

        Me.btnJioMoney.BackColor = Color.WhiteSmoke
        Me.btnJioMoney.ForeColor = Color.Black

        Me.btnCreditSale.BackColor = Color.WhiteSmoke
        Me.btnCreditSale.ForeColor = Color.Black

        Me.btnTr.BackColor = Color.WhiteSmoke
        Me.btnTr.ForeColor = Color.Black

        Me.btnSodexo.BackColor = Color.WhiteSmoke
        Me.btnSodexo.ForeColor = Color.Black

        Me.btnPaytm.BackColor = Color.WhiteSmoke
        Me.btnPaytm.ForeColor = Color.Black

        Me.btnTicketRestaurant.BackColor = Color.WhiteSmoke
        Me.btnTicketRestaurant.ForeColor = Color.Black

        Me.btnSodexoCards.BackColor = Color.WhiteSmoke
        Me.btnSodexoCards.ForeColor = Color.Black

        Me.btnSodexoCpn.BackColor = Color.WhiteSmoke
        Me.btnSodexoCpn.ForeColor = Color.Black
        Me.btnPhonePe.BackColor = Color.WhiteSmoke
        Me.btnPhonePe.ForeColor = Color.Black
    End Sub

    Private Sub btnTicketRestaurant_Click(sender As Object, e As EventArgs) Handles btnTicketRestaurant.Click
        If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
            If CancelRequestOnTenderChange() = False Then
                If ObjonlinePayment.IsPaymentSuccess Then
                    btnSave_Click(sender, e)
                End If
                Exit Sub
            Else
                PhonePeButtonVisibility()
            End If
        End If

        tblCard.Visible = False
        tblCash.Visible = True
        lblPaymentModeVal.Text = "Ticket Restaurant"
        TenderMode = "TktRestaurant"

        'added by khusrao adil on 25-07-2018
        AllowInnovitiPayment = False
        Me.btnCardInnoviti.BackColor = Color.WhiteSmoke
        Me.btnCardInnoviti.ForeColor = Color.Black

        txtCollectAmt.Text = txtTotalAmt.Text.Trim()
        txtCollectAmt.Enabled = False
        ChangeBtnColor()
        Me.btnTicketRestaurant.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnTicketRestaurant.ForeColor = Color.FromArgb(255, 255, 255)

        Me.btnCard.BackColor = Color.WhiteSmoke
        Me.btnCard.ForeColor = Color.Black

        Me.btnFoodPandaCash.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaCash.ForeColor = Color.Black

        Me.btnFoodPandaOnline.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaOnline.ForeColor = Color.Black

        Me.btnOther.BackColor = Color.WhiteSmoke
        Me.btnOther.ForeColor = Color.Black

        Me.btnCash.BackColor = Color.WhiteSmoke
        Me.btnCash.ForeColor = Color.Black

        Me.btnCheque.BackColor = Color.WhiteSmoke
        Me.btnCheque.ForeColor = Color.Black

        Me.btnMobiKwik.BackColor = Color.WhiteSmoke
        Me.btnMobiKwik.ForeColor = Color.Black

        Me.btnJioMoney.BackColor = Color.WhiteSmoke
        Me.btnJioMoney.ForeColor = Color.Black

        Me.btnCreditSale.BackColor = Color.WhiteSmoke
        Me.btnCreditSale.ForeColor = Color.Black

        Me.btnTr.BackColor = Color.WhiteSmoke
        Me.btnTr.ForeColor = Color.Black

        Me.btnSodexo.BackColor = Color.WhiteSmoke
        Me.btnSodexo.ForeColor = Color.Black

        Me.btnPaytm.BackColor = Color.WhiteSmoke
        Me.btnPaytm.ForeColor = Color.Black

        Me.btnPayso.BackColor = Color.WhiteSmoke
        Me.btnPayso.ForeColor = Color.Black

        Me.btnSodexoCards.BackColor = Color.WhiteSmoke
        Me.btnSodexoCards.ForeColor = Color.Black

        Me.btnSodexoCpn.BackColor = Color.WhiteSmoke
        Me.btnSodexoCpn.ForeColor = Color.Black
        Me.btnPhonePe.BackColor = Color.WhiteSmoke
        Me.btnPhonePe.ForeColor = Color.Black
    End Sub

    Private Sub btnSodexoCards_Click(sender As Object, e As EventArgs) Handles btnSodexoCards.Click

        If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
            If CancelRequestOnTenderChange() = False Then
                If ObjonlinePayment.IsPaymentSuccess Then
                    btnSave_Click(sender, e)
                End If
                Exit Sub
            Else
                PhonePeButtonVisibility()
            End If
        End If

        tblCard.Visible = False
        tblCash.Visible = True
        lblPaymentModeVal.Text = "Sodexo Cards"
        TenderMode = "SodexoCards"

        'added by khusrao adil on 25-07-2018
        AllowInnovitiPayment = False
        Me.btnCardInnoviti.BackColor = Color.WhiteSmoke
        Me.btnCardInnoviti.ForeColor = Color.Black

        txtCollectAmt.Text = txtTotalAmt.Text.Trim()
        txtCollectAmt.Enabled = False
        ChangeBtnColor()
        Me.btnSodexoCards.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnSodexoCards.ForeColor = Color.FromArgb(255, 255, 255)

        Me.btnCard.BackColor = Color.WhiteSmoke
        Me.btnCard.ForeColor = Color.Black

        Me.btnFoodPandaCash.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaCash.ForeColor = Color.Black

        Me.btnFoodPandaOnline.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaOnline.ForeColor = Color.Black

        Me.btnOther.BackColor = Color.WhiteSmoke
        Me.btnOther.ForeColor = Color.Black

        Me.btnCash.BackColor = Color.WhiteSmoke
        Me.btnCash.ForeColor = Color.Black

        Me.btnCheque.BackColor = Color.WhiteSmoke
        Me.btnCheque.ForeColor = Color.Black

        Me.btnMobiKwik.BackColor = Color.WhiteSmoke
        Me.btnMobiKwik.ForeColor = Color.Black

        Me.btnJioMoney.BackColor = Color.WhiteSmoke
        Me.btnJioMoney.ForeColor = Color.Black

        Me.btnCreditSale.BackColor = Color.WhiteSmoke
        Me.btnCreditSale.ForeColor = Color.Black

        Me.btnTr.BackColor = Color.WhiteSmoke
        Me.btnTr.ForeColor = Color.Black

        Me.btnSodexo.BackColor = Color.WhiteSmoke
        Me.btnSodexo.ForeColor = Color.Black

        Me.btnPaytm.BackColor = Color.WhiteSmoke
        Me.btnPaytm.ForeColor = Color.Black

        Me.btnPayso.BackColor = Color.WhiteSmoke
        Me.btnPayso.ForeColor = Color.Black

        Me.btnTicketRestaurant.BackColor = Color.WhiteSmoke
        Me.btnTicketRestaurant.ForeColor = Color.Black

        Me.btnSodexoCpn.BackColor = Color.WhiteSmoke
        Me.btnSodexoCpn.ForeColor = Color.Black
        Me.btnPhonePe.BackColor = Color.WhiteSmoke
        Me.btnPhonePe.ForeColor = Color.Black
    End Sub
    Private Sub btnCancle_Click(sender As Object, e As EventArgs) Handles btnCancle.Click
        'added by khusrao adil on 25-07-2018
        AllowInnovitiPayment = False
        Me.Close()
        Exit Sub
    End Sub

    Private Sub CreateDatatableforCheque(ByRef dtCheque As DataTable)
        Try
            dtCheque.Clear()
            dtCheque.Columns.Clear()
            dtCheque.Columns.Add("CheckNo")
            dtCheque.Columns.Add("Amount")
            dtCheque.Columns.Add("DueDate", System.Type.GetType("System.DateTime"))
            dtCheque.Columns.Add("BankName")
            dtCheque.Columns.Add("CustomerName")

            Dim dataRow As DataRow
            dataRow = dtCheque.NewRow()
            dataRow("CheckNo") = txtCardNo.Text.ToString
            dataRow("Amount") = txtCalCollectAmount.Text.ToString
            dataRow("DueDate") = dtpExpiryDateCheque.Value.ToString
            dataRow("BankName") = cmbBankName.SelectedText.ToString
            dataRow("CustomerName") = txtmicrno.Text.ToString
            dtCheque.Rows.Add(dataRow)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub btnSodexoCpn_Click(sender As Object, e As EventArgs) Handles btnSodexoCpn.Click

        If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
            If CancelRequestOnTenderChange() = False Then
                If ObjonlinePayment.IsPaymentSuccess Then
                    btnSave_Click(sender, e)
                End If
                Exit Sub
            Else
                PhonePeButtonVisibility()
            End If
        End If

        tblCard.Visible = False
        tblCash.Visible = True
        lblPaymentModeVal.Text = "Sodexo Cpn"
        TenderMode = "SodexoCpn"

        'added by khusrao adil on 25-07-2018
        AllowInnovitiPayment = False
        Me.btnCardInnoviti.BackColor = Color.WhiteSmoke
        Me.btnCardInnoviti.ForeColor = Color.Black

        txtCollectAmt.Text = txtTotalAmt.Text.Trim()
        txtCollectAmt.Enabled = False
        ChangeBtnColor()
        Me.btnSodexoCpn.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnSodexoCpn.ForeColor = Color.FromArgb(255, 255, 255)

        Me.btnCard.BackColor = Color.WhiteSmoke
        Me.btnCard.ForeColor = Color.Black

        Me.btnFoodPandaCash.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaCash.ForeColor = Color.Black

        Me.btnFoodPandaOnline.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaOnline.ForeColor = Color.Black

        Me.btnOther.BackColor = Color.WhiteSmoke
        Me.btnOther.ForeColor = Color.Black

        Me.btnCash.BackColor = Color.WhiteSmoke
        Me.btnCash.ForeColor = Color.Black

        Me.btnCheque.BackColor = Color.WhiteSmoke
        Me.btnCheque.ForeColor = Color.Black

        Me.btnMobiKwik.BackColor = Color.WhiteSmoke
        Me.btnMobiKwik.ForeColor = Color.Black

        Me.btnJioMoney.BackColor = Color.WhiteSmoke
        Me.btnJioMoney.ForeColor = Color.Black

        Me.btnCreditSale.BackColor = Color.WhiteSmoke
        Me.btnCreditSale.ForeColor = Color.Black

        Me.btnTr.BackColor = Color.WhiteSmoke
        Me.btnTr.ForeColor = Color.Black

        Me.btnSodexo.BackColor = Color.WhiteSmoke
        Me.btnSodexo.ForeColor = Color.Black

        Me.btnPaytm.BackColor = Color.WhiteSmoke
        Me.btnPaytm.ForeColor = Color.Black

        Me.btnPayso.BackColor = Color.WhiteSmoke
        Me.btnPayso.ForeColor = Color.Black

        Me.btnTicketRestaurant.BackColor = Color.WhiteSmoke
        Me.btnTicketRestaurant.ForeColor = Color.Black

        Me.btnSodexoCards.BackColor = Color.WhiteSmoke
        Me.btnSodexoCards.ForeColor = Color.Black
        Me.btnPhonePe.BackColor = Color.WhiteSmoke
        Me.btnPhonePe.ForeColor = Color.Black
    End Sub

    Private Sub btnCardInnoviti_Click(sender As System.Object, e As System.EventArgs) Handles btnCardInnoviti.Click
        If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
            If CancelRequestOnTenderChange() = False Then
                If ObjonlinePayment.IsPaymentSuccess Then
                    btnSave_Click(sender, e)
                End If
                Exit Sub
            Else
                PhonePeButtonVisibility()
            End If
        End If

        tblCard.Visible = True
        tblCash.Visible = False
        lblPaymentModeVal.Text = "Card"
        TenderMode = "Card"
        txtCalCollectAmount.Enabled = False
        'added by khusrao adil on 22-11-2017
        AllowInnovitiPayment = True
        txtCollectAmt.Text = txtTotalAmt.Text.Trim()

        If clsDefaultConfiguration.PayFromInnoviti = True And clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) Then
            lblBankName.Visible = False
            lblCardNo.Visible = False
            lblExpiryDate.Visible = False
            cmbBankName.Visible = False
            txtCardNo.Visible = False
            dtpExpiryDate.Visible = False
            lblmicrno.Visible = False
            txtmicrno.Visible = False
            dtpExpiryDateCheque.Visible = False
        End If

        Me.btnCard.BackColor = Color.WhiteSmoke
        Me.btnCard.ForeColor = Color.Black
        ChangeBtnColor()
        Me.btnCardInnoviti.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnCardInnoviti.ForeColor = Color.FromArgb(255, 255, 255)

        Me.btnCash.BackColor = Color.WhiteSmoke
        Me.btnCash.ForeColor = Color.Black

        Me.btnFoodPandaCash.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaCash.ForeColor = Color.Black

        Me.btnFoodPandaOnline.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaOnline.ForeColor = Color.Black

        Me.btnOther.BackColor = Color.WhiteSmoke
        Me.btnOther.ForeColor = Color.Black

        Me.btnCheque.BackColor = Color.WhiteSmoke
        Me.btnCheque.ForeColor = Color.Black

        Me.btnPaytm.BackColor = Color.WhiteSmoke
        Me.btnPaytm.ForeColor = Color.Black

        Me.btnMobiKwik.BackColor = Color.WhiteSmoke
        Me.btnMobiKwik.ForeColor = Color.Black

        Me.btnJioMoney.BackColor = Color.WhiteSmoke
        Me.btnJioMoney.ForeColor = Color.Black

        Me.btnCreditSale.BackColor = Color.WhiteSmoke
        Me.btnCreditSale.ForeColor = Color.Black

        'code added on 31-07-2017 by vipul
        Me.btnSodexo.BackColor = Color.WhiteSmoke
        Me.btnSodexo.ForeColor = Color.Black

        Me.btnTr.BackColor = Color.WhiteSmoke
        Me.btnTr.ForeColor = Color.Black

        'code added by vipul on 15-05-2018
        Me.btnPayso.BackColor = Color.WhiteSmoke
        Me.btnPayso.ForeColor = Color.Black

        Me.btnTicketRestaurant.BackColor = Color.WhiteSmoke
        Me.btnTicketRestaurant.ForeColor = Color.Black

        Me.btnSodexoCards.BackColor = Color.WhiteSmoke
        Me.btnSodexoCards.ForeColor = Color.Black

        Me.btnSodexoCpn.BackColor = Color.WhiteSmoke
        Me.btnSodexoCpn.ForeColor = Color.Black
        Me.btnPhonePe.BackColor = Color.WhiteSmoke
        Me.btnPhonePe.ForeColor = Color.Black
    End Sub

    Private Sub btnFoodPandaCash_Click(sender As System.Object, e As System.EventArgs) Handles btnFoodPandaCash.Click
        If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
            If CancelRequestOnTenderChange() = False Then
                If ObjonlinePayment.IsPaymentSuccess Then
                    btnSave_Click(sender, e)
                End If
                Exit Sub
            Else
                PhonePeButtonVisibility()
            End If
        End If
        txtRemark.Focus()
        tblCard.Visible = False
        tblCash.Visible = True
        lblPaymentModeVal.Text = "Food Panda Cash"
        TenderMode = "FoodPandaCash"

        'added by khusrao adil on 25-07-2018
        AllowInnovitiPayment = False
        Me.btnCardInnoviti.BackColor = Color.WhiteSmoke
        Me.btnCardInnoviti.ForeColor = Color.Black

        txtCollectAmt.Text = txtTotalAmt.Text.Trim()
        txtCollectAmt.Enabled = False
        ChangeBtnColor()
        Me.btnFoodPandaCash.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnFoodPandaCash.ForeColor = Color.FromArgb(255, 255, 255)

        Me.btnFoodPandaOnline.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaOnline.ForeColor = Color.Black

        Me.btnSodexoCards.BackColor = Color.WhiteSmoke
        Me.btnSodexoCards.ForeColor = Color.Black

        Me.btnCard.BackColor = Color.WhiteSmoke
        Me.btnCard.ForeColor = Color.Black

        Me.btnOther.BackColor = Color.WhiteSmoke
        Me.btnOther.ForeColor = Color.Black

        Me.btnCash.BackColor = Color.WhiteSmoke
        Me.btnCash.ForeColor = Color.Black

        Me.btnCheque.BackColor = Color.WhiteSmoke
        Me.btnCheque.ForeColor = Color.Black

        Me.btnMobiKwik.BackColor = Color.WhiteSmoke
        Me.btnMobiKwik.ForeColor = Color.Black

        Me.btnJioMoney.BackColor = Color.WhiteSmoke
        Me.btnJioMoney.ForeColor = Color.Black

        Me.btnCreditSale.BackColor = Color.WhiteSmoke
        Me.btnCreditSale.ForeColor = Color.Black

        Me.btnTr.BackColor = Color.WhiteSmoke
        Me.btnTr.ForeColor = Color.Black

        Me.btnSodexo.BackColor = Color.WhiteSmoke
        Me.btnSodexo.ForeColor = Color.Black

        Me.btnPaytm.BackColor = Color.WhiteSmoke
        Me.btnPaytm.ForeColor = Color.Black

        Me.btnPayso.BackColor = Color.WhiteSmoke
        Me.btnPayso.ForeColor = Color.Black

        Me.btnTicketRestaurant.BackColor = Color.WhiteSmoke
        Me.btnTicketRestaurant.ForeColor = Color.Black

        Me.btnSodexoCpn.BackColor = Color.WhiteSmoke
        Me.btnSodexoCpn.ForeColor = Color.Black
        Me.btnPhonePe.BackColor = Color.WhiteSmoke
        Me.btnPhonePe.ForeColor = Color.Black
    End Sub

    Private Sub btnFoodPandaOnline_Click(sender As System.Object, e As System.EventArgs) Handles btnFoodPandaOnline.Click
        If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
            If CancelRequestOnTenderChange() = False Then
                If ObjonlinePayment.IsPaymentSuccess Then
                    btnSave_Click(sender, e)
                End If
                Exit Sub
            Else
                PhonePeButtonVisibility()
            End If
        End If

        tblCard.Visible = False
        tblCash.Visible = True
        lblPaymentModeVal.Text = "Food Panda Online"
        TenderMode = "FoodPandaOnline"

        'added by khusrao adil on 25-07-2018
        AllowInnovitiPayment = False
        Me.btnCardInnoviti.BackColor = Color.WhiteSmoke
        Me.btnCardInnoviti.ForeColor = Color.Black

        txtCollectAmt.Text = txtTotalAmt.Text.Trim()
        txtCollectAmt.Enabled = False
        ChangeBtnColor()
        Me.btnFoodPandaOnline.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnFoodPandaOnline.ForeColor = Color.FromArgb(255, 255, 255)

        Me.btnFoodPandaCash.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaCash.ForeColor = Color.Black

        Me.btnSodexoCards.BackColor = Color.WhiteSmoke
        Me.btnSodexoCards.ForeColor = Color.Black

        Me.btnCard.BackColor = Color.WhiteSmoke
        Me.btnCard.ForeColor = Color.Black

        Me.btnOther.BackColor = Color.WhiteSmoke
        Me.btnOther.ForeColor = Color.Black

        Me.btnCash.BackColor = Color.WhiteSmoke
        Me.btnCash.ForeColor = Color.Black

        Me.btnCheque.BackColor = Color.WhiteSmoke
        Me.btnCheque.ForeColor = Color.Black

        Me.btnMobiKwik.BackColor = Color.WhiteSmoke
        Me.btnMobiKwik.ForeColor = Color.Black

        Me.btnJioMoney.BackColor = Color.WhiteSmoke
        Me.btnJioMoney.ForeColor = Color.Black

        Me.btnCreditSale.BackColor = Color.WhiteSmoke
        Me.btnCreditSale.ForeColor = Color.Black

        Me.btnTr.BackColor = Color.WhiteSmoke
        Me.btnTr.ForeColor = Color.Black

        Me.btnSodexo.BackColor = Color.WhiteSmoke
        Me.btnSodexo.ForeColor = Color.Black

        Me.btnPaytm.BackColor = Color.WhiteSmoke
        Me.btnPaytm.ForeColor = Color.Black

        Me.btnPayso.BackColor = Color.WhiteSmoke
        Me.btnPayso.ForeColor = Color.Black

        Me.btnTicketRestaurant.BackColor = Color.WhiteSmoke
        Me.btnTicketRestaurant.ForeColor = Color.Black

        Me.btnSodexoCpn.BackColor = Color.WhiteSmoke
        Me.btnSodexoCpn.ForeColor = Color.Black

        Me.btnPhonePe.BackColor = Color.WhiteSmoke
        Me.btnPhonePe.ForeColor = Color.Black
    End Sub
    Private Sub btnPhonePe_Click(sender As Object, e As EventArgs) Handles btnPhonePe.Click

        tblCard.Visible = False
        tblCash.Visible = True
        lblPaymentModeVal.Text = "PhonePe"
        TenderMode = "PhonePe"
        Me.btnSave.Visible = True
        Me.btnGift.Visible = True
        Me.btnCancle.Visible = True
        Me.CtrlLabel2.Text = "COLLECT AMOUNT"
        Me.txtCollectAmt.Name = "txtCollectAmt"
        ' If txtCollectAmt.Text = "" Then
        txtCollectAmt.Focus()
        'End If

        CtrlLabel3.Visible = True
        txtRemark.Visible = True

        Me.CtrlBtnRequest.Visible = False
        Me.CtrlBtnCheck.Visible = False
        Me.CtrlBtnCancle.Visible = False

        AllowInnovitiPayment = False
        Me.btnCardInnoviti.BackColor = Color.WhiteSmoke
        Me.btnCardInnoviti.ForeColor = Color.Black

        txtCollectAmt.Text = txtTotalAmt.Text.Trim()
        txtCollectAmt.Enabled = True
        ChangeBtnColor()
        Me.btnPhonePe.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnPhonePe.ForeColor = Color.FromArgb(255, 255, 255)

        Me.btnPaytm.BackColor = Color.WhiteSmoke
        Me.btnPaytm.ForeColor = Color.Black


        Me.btnCard.BackColor = Color.WhiteSmoke
        Me.btnCard.ForeColor = Color.Black

        Me.btnFoodPandaCash.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaCash.ForeColor = Color.Black

        Me.btnFoodPandaOnline.BackColor = Color.WhiteSmoke
        Me.btnFoodPandaOnline.ForeColor = Color.Black

        Me.btnOther.BackColor = Color.WhiteSmoke
        Me.btnOther.ForeColor = Color.Black

        Me.btnCash.BackColor = Color.WhiteSmoke
        Me.btnCash.ForeColor = Color.Black

        Me.btnCheque.BackColor = Color.WhiteSmoke
        Me.btnCheque.ForeColor = Color.Black

        Me.btnMobiKwik.BackColor = Color.WhiteSmoke
        Me.btnMobiKwik.ForeColor = Color.Black

        Me.btnJioMoney.BackColor = Color.WhiteSmoke
        Me.btnJioMoney.ForeColor = Color.Black

        Me.btnCreditSale.BackColor = Color.WhiteSmoke
        Me.btnCreditSale.ForeColor = Color.Black


        'code added on 31-07-2017 by vipul
        Me.btnSodexo.BackColor = Color.WhiteSmoke
        Me.btnSodexo.ForeColor = Color.Black

        Me.btnTr.BackColor = Color.WhiteSmoke
        Me.btnTr.ForeColor = Color.Black


        'code added by vipul on 15-05-2018
        Me.btnPayso.BackColor = Color.WhiteSmoke
        Me.btnPayso.ForeColor = Color.Black


        Me.btnTicketRestaurant.BackColor = Color.WhiteSmoke
        Me.btnTicketRestaurant.ForeColor = Color.Black

        Me.btnSodexoCards.BackColor = Color.WhiteSmoke
        Me.btnSodexoCards.ForeColor = Color.Black

        Me.btnSodexoCpn.BackColor = Color.WhiteSmoke
        Me.btnSodexoCpn.ForeColor = Color.Black


        If clsDefaultConfiguration.EnablePhonePeIntegration = True Then

            txtCollectAmt.Name = "txtMobileno"
            txtCollectAmt.MaxLength = "10"
            txtCollectAmt.Enabled = True

            txtCollectAmt.Text = CustMobileno    'Mobile no

            CtrlLabel3.Visible = False
            txtRemark.Visible = False


            CtrlLabel2.Text = "MOBILE NO"
            lblTotalAmt.Enabled = False
            CtrlBtnCheck.Enabled = False
            CtrlBtnCancle.Enabled = False
            Me.btnSave.Visible = False
            Me.btnGift.Visible = False
            Me.btnCancle.Visible = False

            Me.CtrlBtnCancle.Visible = True
            Me.CtrlBtnCancle.BackColor = Color.FromArgb(0, 107, 163)
            Me.CtrlBtnCancle.ForeColor = Color.FromArgb(255, 255, 255)
            Me.CtrlBtnCancle.Font = New Font("Neo Sans", 9, FontStyle.Bold)
            Me.CtrlBtnCancle.BringToFront()
            Me.CtrlBtnCancle.Image = Nothing
            Me.CtrlBtnCancle.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.CtrlBtnCancle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.CtrlBtnCancle.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
            Me.CtrlBtnCancle.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
            Me.CtrlBtnCancle.FlatStyle = FlatStyle.Flat
            Me.CtrlBtnCancle.FlatAppearance.BorderSize = 1

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

            CtrlBtnRequest.Enabled = True
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
        End If



    End Sub
    Private Sub DynamicBtnClick(sender As System.Object, e As System.EventArgs)
        Try
            ' txtCollectAmt.Text = ""
            For i = 0 To tblButtonsPnl.RowCount - 1
                For j = 0 To tblButtonsPnl.ColumnCount - 1
                    Dim Control = tblButtonsPnl.GetControlFromPosition(j, i)
                    If Not Control Is Nothing Then
                        Dim btn As Spectrum.CtrlBtn = DirectCast(Control, Spectrum.CtrlBtn)
                        btn.BackColor = Color.WhiteSmoke
                        btn.ForeColor = Color.Black
                    End If
                Next
            Next
            ' Dim SelectedBtn As Spectrum.CtrlBtn = DirectCast(sender, Spectrum.CtrlBtn)
            If clsDefaultConfiguration.EnablePhonePeIntegration = True AndAlso CtrlBtnCancle.Enabled = True Then
                txtCollectAmt.Text = ""
                Dim ResponsefromUser As Int32
                ShowMessage("Are you sure want to close?", "Information", ResponsefromUser, "NO", "YES")
                If ResponsefromUser = 1 Then
                    CancelRsponse = ObjonlinePayment.POSTDataForCancel(TransactionID)
                    If ObjonlinePayment.IsPaymentSuccess Then
                        btnSave_Click(sender, e)
                    End If
                    'SavePhonePePaymentCancelRequestResponse(CancelRsponse)
                    objClsComm.UpdatePhonePeCancelRequestResponse(clsAdmin.SiteCode, TransactionID, CancelRsponse)
                    Dim jss As New JavaScriptSerializer()
                    Dim dictforCancel As Dictionary(Of String, Object) = jss.Deserialize(Of Dictionary(Of String, Object))(CancelRsponse)
                    Dim ResponseCancelCode As String
                    ResponseCancelCode = dictforCancel("code")
                    If ResponseCancelCode = "SUCCESS" Then
                        ShowMessage("Cancel Request sent sucessfully", getValueByKey("CLAE04"))
                        TransactionID = ""
                    ElseIf ResponseCancelCode = "INTERNAL_SERVER_ERROR" Then
                        ShowMessage("Something went wrong try after some time. Merchant should retry the cancel API request.", getValueByKey("CLAE04"))
                    ElseIf ResponseCancelCode = "INVALID_TRANSACTION_ID" Then
                        ShowMessage("TransactionId in request was duplicate.", getValueByKey("CLAE04"))
                    ElseIf ResponseCancelCode = "PAYMENT_ALREADY_COMPLETED" Then
                        ShowMessage("Payment has been successful hence can't cancel the request", getValueByKey("CLAE04"))
                        objClsComm.UpdatePhonePePaymentStatus(clsAdmin.SiteCode, TransactionID)
                        btnSave_Click(sender, e)
                        Me.Close()
                        TransactionID = ""
                    End If
                Else
                    Me.btnPhonePe.BackColor = Color.FromArgb(0, 107, 163)
                    Me.btnPhonePe.ForeColor = Color.FromArgb(255, 255, 255)
                    Exit Sub
                End If

            End If


            Dim SelectedBtn As Spectrum.CtrlBtn = DirectCast(sender, Spectrum.CtrlBtn)
            txtRemark.Focus()
            tblCard.Visible = False
            tblCash.Visible = True
            lblPaymentModeVal.Text = SelectedBtn.Text
            TenderMode = SelectedBtn.Text
            TenderTypeCode = SelectedBtn.Name
            AllowInnovitiPayment = False
            txtCollectAmt.Text = txtTotalAmt.Text.Trim()
            txtCollectAmt.Enabled = False
            SelectedBtn.BackColor = Color.FromArgb(0, 107, 163)
            SelectedBtn.ForeColor = Color.FromArgb(255, 255, 255)
            SelectedBtn.FlatAppearance.BorderSize = 0


            Me.btnSave.Visible = True
            Me.btnGift.Visible = True
            Me.btnCancle.Visible = True
            Me.CtrlLabel2.Text = "COLLECT AMOUNT"
            Me.txtCollectAmt.Name = "txtCollectAmt"

            CtrlLabel3.Visible = True
            txtRemark.Visible = True

            Me.CtrlBtnRequest.Visible = False
            Me.CtrlBtnCheck.Visible = False
            Me.CtrlBtnCancle.Visible = False

        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    Public Sub displaydtDynamicTender(ByVal dt As DataTable)
        Try
            For Each dr In dt.Rows
                For i = 0 To tblButtonsPnl.RowCount - 1
                    For j = 0 To tblButtonsPnl.ColumnCount - 1
                        Dim Control = tblButtonsPnl.GetControlFromPosition(j, i)
                        If Control Is Nothing Then
                            Dim btn As New CtrlBtn
                            btn.Text = dr("TenderHeadCode")
                            btn.Name = dr("TenderType")
                            btn.Dock = DockStyle.None
                            btn.BackColor = Color.WhiteSmoke
                            btn.ForeColor = Color.Black
                            btn.Font = New Font("Neo Sans", 9, FontStyle.Bold)

                            If dr("TenderHeadCode").ToString.Length > 12 Then
                                btn.Font = New Font("Neo Sans", 7, FontStyle.Bold)
                            End If
                            btn.BringToFront()
                            btn.Image = Nothing
                            btn.ImageAlign = System.Drawing.ContentAlignment.TopCenter
                            btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                            btn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                            btn.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
                            btn.FlatStyle = FlatStyle.Flat
                            btn.FlatAppearance.BorderSize = 1
                            btn.Size = New System.Drawing.Size(105, 30)
                            AddHandler btn.Click, AddressOf DynamicBtnClick
                            tblButtonsPnl.Controls.Add(btn, j, i)
                            GoTo line1
                        End If
                    Next
                Next
line1:
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub ChangeBtnColor()
        Try
            For i = 0 To tblButtonsPnl.RowCount - 1
                For j = 0 To tblButtonsPnl.ColumnCount - 1
                    Dim Control = tblButtonsPnl.GetControlFromPosition(j, i)
                    If Not Control Is Nothing Then
                        Dim btn As Spectrum.CtrlBtn = DirectCast(Control, Spectrum.CtrlBtn)
                        btn.BackColor = Color.WhiteSmoke
                        btn.ForeColor = Color.Black
                    End If
                Next
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

#Region "PhonePe"

    'Enum sampleinstrumentType
    '    Mobile
    'End Enum
    'Enum responsefromrequest
    '    SUCCESS
    '    INTERNAL_SERVER_ERROR
    '    INVALID_TRANSACTION_ID
    '    BAD_REQUEST
    '    AUTHORIZATION_FAILED
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
            'objReq.instrumentType = sampleinstrumentType.Mobile
            objReq.instrumentType = "MOBILE"
            objReq.instrumentReference = txtCollectAmt.Text   'Mobile no from payment window
            objReq.expiresIn = 180
            objReq.message = "collect for bill no " + CMBillno + "transaction id " + objReq.transactionId + " order"
            objReq.email = ""
            objReq.shortName = ""
            ' objReq.subMerchant = ""
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
            'JSONString.Append("""subMerchant""" + ":""" + objReq.subMerchant + """, ")
            JSONString.Append("""storedId""" + ":""" + objReq.storedId + """, ")
            JSONString.Append("""terminalId""" + ":""" + objReq.terminalId + """")
            JSONString.Append("}")
            Return JSONString.ToString()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Sub txtCollectAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCollectAmt.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub
    Public Function CancelRequestOnTenderChange() As Boolean
        Try
            If CtrlBtnCancle.Enabled = False Then
                Return True
            End If
            Dim userr As Boolean = False
            Dim ResponsefromUser As Int32
            ShowMessage("Are you sure want to close?", "Information", ResponsefromUser, "NO", "YES")
            If ResponsefromUser = 1 Then
                CancelRsponse = ObjonlinePayment.POSTDataForCancel(TransactionID)
                'SavePhonePePaymentCancelRequestResponse(CancelRsponse)
                objClsComm.UpdatePhonePeCancelRequestResponse(clsAdmin.SiteCode, TransactionID, CancelRsponse)
                Dim jss As New JavaScriptSerializer()
                Dim dictforCancel As Dictionary(Of String, Object) = jss.Deserialize(Of Dictionary(Of String, Object))(CancelRsponse)
                Dim ResponseCancelCode As String
                ResponseCancelCode = dictforCancel("code")

                If ResponseCancelCode = "SUCCESS" Then
                    ShowMessage("Cancel Request sent sucessfully", getValueByKey("CLAE04"))
                    Me.Close()
                    TransactionID = ""
                    Return True
                ElseIf ResponseCancelCode = "INTERNAL_SERVER_ERROR" Then
                    ShowMessage("Something went wrong try after some time. Again call Cancel API .", getValueByKey("CLAE04"))
                ElseIf ResponseCancelCode = "INVALID_TRANSACTION_ID" Then
                    ShowMessage("TransactionId in request was duplicate.", getValueByKey("CLAE04"))
                ElseIf ResponseCancelCode = "PAYMENT_ALREADY_COMPLETED" Then
                    ShowMessage("Payment has been succesful hence can't cancel the request", getValueByKey("CLAE04"))
                    TransactionID = ""
                End If

            Else

                Return False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Sub PhonePeButtonVisibility()
        Try
            Me.CtrlBtnRequest.Visible = False
            Me.CtrlBtnCheck.Visible = False
            Me.CtrlBtnCancle.Visible = False
            Me.CtrlBtnRequest.Enabled = False
            Me.CtrlBtnCheck.Enabled = False
            Me.CtrlBtnCancle.Enabled = False
            Me.btnSave.Visible = True
            Me.btnGift.Visible = True
            Me.btnCancle.Visible = True
            Me.btnSave.Enabled = True
            Me.btnGift.Enabled = True
            Me.btnCancle.Enabled = True
            Me.CtrlLabel2.Text = "COLLECT AMOUNT"
            Me.txtCollectAmt.Name = "txtCollectAmt"
            CtrlLabel3.Visible = True
            txtRemark.Visible = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


#End Region
#Region "PhonePeRequestResponseCheck"
    Private Sub CtrlBtnRequest_Click(sender As Object, e As EventArgs) Handles CtrlBtnRequest.Click

        Try
            If txtCollectAmt.Text.Length < 10 Then
                ShowMessage("Enter 10 digit mobile number.", getValueByKey("CLAE04"))
                Exit Sub
            End If

            Dim mobileNo As String
            mobileNo = txtCollectAmt.Text
            Dim dtcustmobdtl As New DataTable
            dtcustmobdtl = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, mobileNo, CustomerWisePrice:=clsDefaultConfiguration.customerwisepricemanagement, CustFormat:=clsDefaultConfiguration.DetailedCustomerCreationformat)
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
            JsonString = CreatePhonePePaymentRequestUrl()
            SavePhonePePaymentRequest(JsonString)
            Dim JSONTOBase64Result As String = objClsComm.EncodeTo64(JsonString)
            Dim BodyForRequestAPI As String = JSONTOBase64Result
            Dim InputforSHA256 As String = BodyForRequestAPI + "/v3/charge" + clsDefaultConfiguration.PhonepeAuthKey
            Dim HdrSHA256 As String = objClsComm.GenerateSHA256String(InputforSHA256) + "###" + clsDefaultConfiguration.PhonepeAuthIndex.ToString()
            Dim APIUrl As String = clsDefaultConfiguration.PhonePeRequestPaymentUrl
            BodyForRequestAPI = "{" + """" + "request" + """" + ":" + """" + BodyForRequestAPI + """" + "}"
            PhonePeRespons = ObjonlinePayment.postData(BodyForRequestAPI, HdrSHA256, clsDefaultConfiguration.PhonePeRequestPaymentUrl, TransactionID)
            If ObjonlinePayment.CheckPhonePeError Then
                Me.Close()
                Exit Sub
            End If
            objClsComm.UpdatePhonePeRequestResponse(clsAdmin.SiteCode, TransactionID, PhonePeRespons)

            CtrlBtnRequest.Enabled = False
            txtCollectAmt.Enabled = False
            Dim RequestJSSerializer As New JavaScriptSerializer()
            Dim dictforcheck As Dictionary(Of String, Object) = RequestJSSerializer.Deserialize(Of Dictionary(Of String, Object))(PhonePeRespons)
            Dim ResponseCodeFormRequestAPI As String

            ResponseCodeFormRequestAPI = dictforcheck("code")

            If ResponseCodeFormRequestAPI = "SUCCESS" Then
                ShowMessage("Request Sent sucessfully, Payment is in Progress, Do not press Escape otherwise you will loose Data", getValueByKey("CLAE04"))
                CtrlBtnCheck.Enabled = True
                CtrlBtnCancle.Enabled = True
            ElseIf ResponseCodeFormRequestAPI = "INTERNAL_SERVER_ERROR" Then
                ShowMessage("Invalid request.", getValueByKey("CLAE04"))

            ElseIf ResponseCodeFormRequestAPI = "INVALID_TRANSACTION_ID" Then
                ShowMessage("TransactionId in request was duplicate.", getValueByKey("CLAE04"))
                CtrlBtnRequest.Enabled = True

            ElseIf ResponseCodeFormRequestAPI = "BAD_REQUEST" Then
                ShowMessage("Some mandatory parameter was missing.", getValueByKey("CLAE04"))
                CtrlBtnRequest.Enabled = True
                CtrlBtnCheck.Enabled = False

                'ElseIf ResponseCodeFormRequestAPI = "PAYMENT_SUCCESS" Then
                '    ShowMessage("Payment is successful.", getValueByKey("CLAE04"))
                '    btnSave_Click(sender, e)

            ElseIf ResponseCodeFormRequestAPI = "AUTHORIZATION_FAILED" Then
                CtrlBtnRequest.Enabled = True
                ShowMessage("Authorization Failed. Please check with technical team.", getValueByKey("CLAE04"))
                Me.Close()
                TransactionID = ""
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub CtrlBtnCheck_Click(sender As Object, e As EventArgs) Handles CtrlBtnCheck.Click
        Try


            CheckRsponse = ObjonlinePayment.CheckPhonePeResponce(TransactionID)
            If String.IsNullOrEmpty(CheckRsponse) Then
                ShowMessage("Something Went Wrong ,Please try after some time", getValueByKey("CLAE04"))
                Exit Sub
            End If
            If ObjonlinePayment.CheckPhonePeError Then
                Me.Close()
                Exit Sub
            End If
            '  SavePhonePePaymentCheckRequestResponse(CheckRsponse)
            objClsComm.UpdatePhonePeCheckRequestResponse(clsAdmin.SiteCode, TransactionID, CheckRsponse)
            Dim CheckJSResponse As New JavaScriptSerializer()
            Dim dictforcheck As Dictionary(Of String, Object) = CheckJSResponse.Deserialize(Of Dictionary(Of String, Object))(CheckRsponse)
            Dim ResponseCode As String
            ResponseCode = dictforcheck("code")

            If ResponseCode = "TRANSACTION_NOT_FOUND" Then
                ShowMessage("Payment not initiated inside PhonePe", getValueByKey("CLAE04"))
                Me.Close()
            ElseIf ResponseCode = "BAD_REQUEST" Then
                ShowMessage("Some mandatory parameter was missing", getValueByKey("CLAE04"))
            ElseIf ResponseCode = "AUTHORIZATION_FAILED" Then
                ShowMessage("Authorization Failed. Please check with technical team.", getValueByKey("CLAE04"))
                Me.Close()
            ElseIf ResponseCode = "INTERNAL_SERVER_ERROR" Then
                ShowMessage("Something went wrong try after some time. Again call check status API for payment status.", getValueByKey("CLAE04"))
            ElseIf ResponseCode = "PAYMENT_SUCCESS" Then
                objClsComm.UpdatePhonePePaymentStatus(clsAdmin.SiteCode, TransactionID)
                ShowMessage("Payment is successful", getValueByKey("CLAE04"))
                btnSave_Click(sender, e)
            ElseIf ResponseCode = "PAYMENT_ERROR" Then
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
                ShowMessage("Payment cancelled by merchant", getValueByKey("CLAE04"))
                Me.Close()
            ElseIf ResponseCode = "PAYMENT_DECLINED" Then
                CtrlBtnRequest.Enabled = True
                ShowMessage("Payment declined by user", getValueByKey("CLAE04"))
                Me.Close()
            Else
                ShowMessage("Something Went Wrong ,Please try after sometime", getValueByKey("CLAE04"))
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
                CancelRsponse = ObjonlinePayment.POSTDataForCancel(TransactionID)
                If ObjonlinePayment.IsPaymentSuccess Then
                    objClsComm.UpdatePhonePePaymentStatus(clsAdmin.SiteCode, TransactionID)
                    'ShowMessage("Payment is successful", getValueByKey("CLAE04"))
                    btnSave_Click(sender, e)
                End If

                objClsComm.UpdatePhonePeCancelRequestResponse(clsAdmin.SiteCode, TransactionID, CancelRsponse)
                Dim jss As New JavaScriptSerializer()
                Dim dictforCancel As Dictionary(Of String, Object) = jss.Deserialize(Of Dictionary(Of String, Object))(CancelRsponse)
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
                    Me.Close()
                    TransactionID = ""
                End If
            Else
                Exit Sub
            End If

            Me.Close()

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
            drPhonePeDtl("STATUS") = True
            dtPhonePedtl.Rows.Add(drPhonePeDtl)
            objClsComm.SaveOnlineTenderData(dtPhonePedtl, clsAdmin.SiteCode)

        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub


#End Region

End Class