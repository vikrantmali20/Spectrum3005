Imports System.Data
Imports System.Data.SqlClient
Imports C1.Win.C1FlexGrid
Imports SpectrumBL
Imports SpectrumBL.clsAcceptPayment
Imports SpectrumPrint
Public Class frmQSRPayment
#Region "Page Variables"
    Protected dsRecieptType As New DataSet()
    Protected dtRecieptType As New DataTable()
    Protected IsCreateNewTable As Boolean = True
    Private _totalAmount As Double
    Private _totalReceivedAmount As Double
    Private _minimumBillAmount As Decimal
    Private objBLLAcceptPayment As New clsAcceptPayment
    Private IsRecieptAmountClose As Boolean = False
    Dim _paymentType As PaymentType = clsAcceptPayment.PaymentType.Accept
    Dim strErrorMsg As String = ""
    Dim _IsSwipeCreditCard As Boolean
    Protected dsAcceptEditBillDataSet As DataSet
    Protected _IsCancelAcceptPayment As Boolean = False
    Dim dtPayment As DataTable
    Dim dvPayment As DataView
    Dim IsFunctinalKeyActivate As Boolean = True
    Dim _IsPositiveTenderType As Boolean
    Dim filter As String = "Positive_Negative='+'"
    Dim _Actiontype As String
    Dim _tenderHeadName, _tenderHeadCode, _tenderType As String
    Dim _currencySymbol, _currencyDesc As String
    Private _ReturnToCustomer As Decimal
    Dim _PaymentBy As String = ""
    Dim _CardNo As String
    Dim _ParentForm As String = ""
    Public intReturnCashToCust As Double = 0.0
    Public blnChangeModeFormOk As Boolean = False
    Public _IsCashierPromoSelected As Boolean


    Private _strRemarks As String

    Public Property strRemarks As String
        Get
            Return _strRemarks
        End Get
        Set(ByVal value As String)
            _strRemarks = value
        End Set
    End Property

    Private _dDueDate As Date
    Public Property dDueDate As Date
        Get
            Return _dDueDate
        End Get
        Set(ByVal value As Date)
            _dDueDate = value
        End Set
    End Property
#End Region
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        dgGridReciept.Cols(1).Name = "tendertype"
        dgGridReciept.Cols(3).Name = "as above_0"
        dgGridReciept.Cols(4).Name = "ActualAmount"
        dgGridReciept.Cols(2).Visible = False
        'dgGridReciept.Cols(5).Name = "as above_1"


        'Grid_DisplaySetting()
        frmLoad()
        SetCulture(Me, Me.Name)
        ' change to remove the problem of \n in resource file.
        btnF5Cash.Text = "F5" & vbCrLf & getValueByKey("frmnacceptpayment.btnf5cash")
        btnF3.Text = "F7" & vbCrLf & getValueByKey("frmnacceptpayment.btnf3")
        'btnF4CreditNote.Text = "F4" & vbCrLf & getValueByKey("frmnacceptpayment.btnf4creditnote")
        btnF4CreditNote.Text = "F4" & vbCrLf & "Credit Card"
        btnF6.Text = "F6" & vbCrLf & getValueByKey("frmnacceptpayment.btnf6")
        btnSave.Text = "F10" & vbCrLf & getValueByKey("frmnacceptpayment.btnsave")
        btnGift.Text = "F11" & vbCrLf & getValueByKey("frmnacceptpayment.btngift")
        ' change to remove the problem of \n in resource file.

        ' Add any initialization after the InitializeComponent() call.
        Me.Location = New Point(Screen.PrimaryScreen.Bounds.Width - Me.Width, 50)
        Me.AutoSize = False
    End Sub

#Region "Property"
    Private _Roundat As Int32
    Public WriteOnly Property RoundAt()
        Set(ByVal value)
            _Roundat = value
        End Set
    End Property
    Private _customerpay As Double
    Public WriteOnly Property CustomerWantPay()
        Set(ByVal value)
            _customerpay = value
        End Set
    End Property
    Public WriteOnly Property ParentRelation() As String
        Set(ByVal value As String)
            _ParentForm = value
        End Set
    End Property
    Public Property CLPCustomerCardNumber() As String
        Get
            Return _CardNo
        End Get
        Set(ByVal value As String)
            _CardNo = value
        End Set
    End Property

    Private _CLPCustomerName As String
    Public Property CLPCustomerName() As String
        Get
            Return _CLPCustomerName
        End Get
        Set(ByVal value As String)
            _CLPCustomerName = value
        End Set
    End Property

    Public Property PaymentBy() As String
        Get
            Return _PaymentBy
        End Get
        Set(ByVal value As String)
            _PaymentBy = value
        End Set
    End Property
    ''' <summary>
    ''' Check TotalAmount sign and assgin value to this property
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property IsPostiveTenderType() As Boolean
        Get
            Return _IsPositiveTenderType
        End Get
        Set(ByVal value As Boolean)
            _IsPositiveTenderType = value
        End Set
    End Property
    Public ReadOnly Property Action() As String
        Get
            Return _Actiontype
        End Get
    End Property
    ''' <summary>
    '''  Set Payment Type
    ''' </summary>
    ''' <value>Default is "Accept"</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PaymentType() As PaymentType
        Get
            Return _paymentType
        End Get
        Set(ByVal value As PaymentType)
            _paymentType = value
        End Set
    End Property
    ''' <summary>
    ''' Property need to set whenever to push bill cash breakup to display into Grid
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AcceptEditBillDataSet() As DataSet
        Get
            Return dsRecieptType
        End Get
        Set(ByVal value As DataSet)
            dsRecieptType = value
        End Set
    End Property
    ''' <summary>
    ''' Total Bill Amount to accept from user 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Each time you need to set </remarks>
    Public Property TotalBillAmount() As Double
        Get
            Return FormatNumber(_totalAmount, 2)
        End Get
        Set(ByVal value As Double)
            _totalAmount = Math.Round(value, 2)
            ctrlPayCash.txtCash.Value = value
            If value > 0 Then
                _IsPositiveTenderType = True
            End If
        End Set
    End Property
    ''' <summary>
    '''  Advance payment against total bill amount
    ''' </summary>
    ''' <value> </value>
    ''' <returns></returns>
    ''' <remarks>Sales order </remarks>
    Public Property MinimumBillAmount() As Decimal
        Get
            Return FormatNumber(_minimumBillAmount, 2)
        End Get
        Set(ByVal value As Decimal)
            _minimumBillAmount = value
        End Set
    End Property
    ''' <summary>
    ''' Return total received amount from customer
    ''' </summary>
    ''' <value>Decimal</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property TotalReceivedAmount() As Decimal
        Get
            Try
                If (dtRecieptType.Rows.Count > 0) Then
                    _totalReceivedAmount = dtRecieptType.Compute("sum(Amount)", " ")
                Else
                    _totalReceivedAmount = Decimal.Zero
                End If

                Return _totalReceivedAmount
            Catch ex As Exception
                LogException(ex)
                Return _totalReceivedAmount
            End Try
        End Get
        Set(ByVal value As Decimal)
            _totalReceivedAmount = value
        End Set
    End Property
    Private _TotalCashTendered As Decimal

    ''' <summary>
    ''' Return Cash Amount Tendered
    ''' </summary>
    ''' <param name="index"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Public Property TotalCashTendered() As Decimal
        Get
            Try
                If TenderHeadCode <> "Cash" Then
                    If (dtRecieptType.Rows.Count > 0) Then
                        _TotalCashTendered = IIf(IsDBNull(dtRecieptType.Compute("sum(Amount)", "reciept='Cash'")), 0, dtRecieptType.Compute("sum(Amount)", "reciept='Cash'"))
                    Else
                        _TotalCashTendered = Decimal.Zero
                    End If
                End If
                Return _TotalCashTendered
            Catch ex As Exception
                LogException(ex)
                Return _TotalCashTendered
            End Try
        End Get
        Set(ByVal value As Decimal)
            _TotalCashTendered = value
        End Set
    End Property




    Private ReadOnly Property TenderHeadCode() As String
        Get
            Return _tenderHeadCode
        End Get
    End Property
    ''' <summary>
    '''  Receipt type for amount 
    ''' </summary>
    ''' <value>String</value>
    ''' <returns></returns>
    ''' <remarks>ReadOnly</remarks>
    Private ReadOnly Property SelectedReceiptType() As String
        Get
            'Dim dataRow As DataRowView = cboRecieptType.SelectedItem
            Return _tenderHeadName
            'Return dataRow.Item("TenderHeadName").ToString()
        End Get
    End Property
    ''' <summary>
    '''  Receipt type code for amount 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private ReadOnly Property SelectedReceiptTypeCode() As String
        Get
            'Dim dataRow As DataRowView = cboRecieptType.SelectedItem
            'Return dataRow.Item("TenderHeadCode").ToString()
            Return _tenderType
        End Get
    End Property
    ''' <summary>
    '''  Currency symbol 
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
    ''' <remarks>ReadOnly</remarks>
    Private ReadOnly Property SelectedCurrencySymbol() As String
        Get
            'Dim dataRow As DataRowView = cboCurrency.SelectedItem
            'Return dataRow.Item("CurrencySymbol").ToString()
            Return _currencySymbol
        End Get
    End Property
    ''' <summary>
    ''' Currency description
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
    ''' <remarks>ReadOnly</remarks>
    Private ReadOnly Property SelectedCurrencyDescription() As String
        Get
            'Dim dataRow As DataRowView = cboCurrency.SelectedItem
            'Return dataRow.Item("CurrencyDescription").ToString()
            Return _currencyDesc
        End Get
    End Property
    ''' <summary>
    ''' CurrencyIndex
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
    ''' <remarks>ReadOnly</remarks>
    Private ReadOnly Property SelectedCurrencyIndex() As String
        Get
            Return cboCurrency.SelectedValue
        End Get
    End Property
    ''' <summary>
    ''' Reciept summary 
    ''' </summary>
    ''' <value></value>
    ''' <returns>DataSet</returns>
    ''' <remarks>ReadOnly</remarks>
    Public ReadOnly Property ReciptTotalAmount() As DataSet
        Get
            Return dsRecieptType
        End Get
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

    '''' <summary>
    '''' Default currency for site 
    '''' </summary>
    '''' <value>String</value>
    '''' <returns>String</returns>
    '''' <remarks>ReadOnly</remarks>
    'Private ReadOnly Property clsAdmin.CurrencyCode() As String
    '    Get
    '        Dim iBaseCurrency As String = clsAdmin.CurrencyCode
    '        'commented by rama on 26-06-2009 as base currency set on login
    '        'Dim iBaseCurrency As String = objBLLAcceptPayment.GetLocalCurrency(clsAdmin.SiteCode)
    '        Return iBaseCurrency
    '    End Get
    'End Property
    '''' <summary>
    '''' Validate entered amount.(only integer)
    '''' </summary>
    '''' <value>Boolean</value>
    '''' <returns>Boolean</returns>
    '''' <remarks>ReadOnly</remarks>
    'Private ReadOnly Property IsAmountInteger() As Boolean
    '    Get
    '        Try
    '            Dim descAmount As Decimal = CDbl(ctrlPayCash.txtCash.Text)

    '            Dim str As String = System.Text.RegularExpressions.Regex.Replace(ctrlPayCash.txtCash.Text, "-", "")
    '            ctrlPayCash.txtCash.Text = str
    '            Return True
    '        Catch ex As Exception
    '            Return False
    '        End Try
    '    End Get
    'End Property
    ''' <summary>
    ''' Entered amount 
    ''' </summary>
    ''' <value></value>
    ''' <returns>Decimal</returns>
    ''' <remarks>ReadOnly</remarks>
    ReadOnly Property EnteredAmount() As Decimal
        Get
            Try
                If lblSign.Text = String.Empty Then
                    Return CDbl(ctrlPayCash.txtCash.Text)
                Else
                    Return CDbl(ctrlPayCash.txtCash.Text) * -1
                End If

            Catch ex As Exception
                LogException(ex)
                Return Decimal.Zero
            End Try
        End Get
    End Property
    ''' <summary>
    ''' Check for IsCreditCard swap or not 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsSwipeCreditCard() As Boolean
        Get
            Return _IsSwipeCreditCard
        End Get
        Set(ByVal value As Boolean)
            _IsSwipeCreditCard = False
        End Set
    End Property
    ''' <summary>
    '''  Check payment is cancel or not 
    ''' </summary>
    ''' <value>Default False</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsCancelAcceptPayment() As Boolean
        Get
            Return _IsCancelAcceptPayment
        End Get
        Protected Set(ByVal value As Boolean)
            _IsCancelAcceptPayment = value
        End Set
    End Property
    ''' <summary>
    ''' if there is cash receipt type , and amount is exceeds than it's bill amount .
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property ReturnTOCustomer() As Decimal
        Get
            Return _ReturnToCustomer
        End Get
    End Property
    'Dim objclscomman As New clsCommon
    '''' <summary>
    '''' Check for  credit card Date
    '''' </summary>
    '''' <value></value>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private ReadOnly Property IsCreditCardDateValid() As Boolean
    '    Get
    '        Try
    '            If (FormatDateTime(dtpCCExpirydate.Text) >= DateAndTime.DateValue((objclscomman.GetCurrentDate()))) Then
    '                Return True
    '            Else
    '                MessageBox.Show("Check your Credit Card Expiry date ", "CreditCard")
    '                Return False
    '            End If
    '        Catch ex As Exception
    '            MessageBox.Show("Enter CreditCard Expiry Date. ")
    '            Return False
    '        End Try
    '    End Get
    'End Property
    '''' <summary>
    ''''  Validate ChequeDate
    '''' </summary>
    '''' <value>Boolean</value>
    '''' <returns>Boolean</returns>
    '''' <remarks>ReadOnly</remarks>
    'Private ReadOnly Property IsChequeDateValid() As Boolean
    '    Get
    '        Try
    '            If (FormatDateTime(dtpcheque.Text, DateFormat.ShortDate) >= DateAndTime.DateValue(objclscomman.GetCurrentDate())) Then
    '                Return True
    '            Else
    '                MessageBox.Show("Invalid Date", "AcceptPayment")
    '                Return False
    '            End If
    '        Catch ex As Exception
    '            MessageBox.Show("Enter  Date ")
    '            Return False
    '        End Try
    '    End Get
    'End Property
#End Region

#Region "Methods"
    ''' <summary>
    ''' Check entered amount is null 
    ''' </summary>
    ''' <param name="strErrorMsg">Return error message</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Function CheckAmountTextBox_NullOrInteger(ByRef strErrorMsg As String) As Boolean
        If (String.IsNullOrEmpty(ctrlPayCash.txtCash.Text)) Then
            Dim balanceDue As Double
            If Double.TryParse(lblbalanceDue.Text, balanceDue) Then
                If balanceDue = 0 Then
                    strErrorMsg = "Please Press F10 to Save and Print Cash Memo"
                    Return True
                End If
            End If           
                strErrorMsg = getValueByKey("EM014")           
            'strErrorMsg = "Please enter Amount "

            Return True
            'ElseIf Not (IsAmountInteger) Then
            '    strErrorMsg = "Only digits are allowed"
            '    Return True
        ElseIf (ctrlPayCash.txtCash.Text = Decimal.Zero) Then
            'strErrorMsg = "Enter valid amount"
            strErrorMsg = getValueByKey("EM005")
            Return True
        Else
            Return False
        End If
    End Function

    '''' <summary>
    '''' When form is activate ,move focus on amount textbox
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub frmAcceptPayment_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
    '    ctrlPayCash.txtCash.Focus()
    'End Sub
    ''' <summary>
    ''' Called when form load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmAcceptPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            'Dim decCurrenctCurrencyAgainst As Decimal
            'txtBillAmount.Text = objBLLAcceptPayment.CalculateTotalBillAmount_InCurrency(TotalBillAmount, clsAdmin.CurrencyCode, decCurrenctCurrencyAgainst, clsAdmin.CurrencyCode)                       
            If _CardNo = String.Empty Then
                For i As Int32 = dvPayment.Count - 1 To 0 Step -1
                    If dvPayment(i)("TenderType") = "CLPPoint" Then
                        dvPayment(i).Delete()
                    End If
                Next
            End If
            
            lblBillAmt.Text = TotalBillAmount
            If PaymentType = clsAcceptPayment.PaymentType.Advance Then
                ctrlPayCash.txtCash.Text = MinimumBillAmount
            Else
                ctrlPayCash.txtCash.Text = TotalBillAmount
            End If
            lblbalanceDue.Text = TotalBillAmount
            If _customerpay > 0 Then
                TotalBillAmount = _customerpay
            End If

            If TotalBillAmount < 0 Then
                filter = "Positive_Negative='-'"
                ''Changed by rama Ranjan on 26-may-2009 for SystemConfiguration check
                If clsDefaultConfiguration.CashRefund = False Then
                    filter = filter & " And TenderType<>'Cash'"
                End If
                ''
                dvPayment.RowFilter = filter
                cboRecieptType_SelectedIndexChanged(sender, e)
                ctrlPayCash.txtCash.Text = TotalBillAmount
                IsPostiveTenderType = False
                'lblTotalReciepts.Text = "Total Issue :"
                lblTotalReciepts.Text = getValueByKey("frmnacceptpayment.lbltotalreciepts1")
                cboCurrency.Visible = False
                ''added for bug no 1537
                For Each dr As DataRow In dtPayment.Select("TenderType='" & AcceptPaymentTenderType.NegativeTenderType.CreditVoucherI & "'", "", DataViewRowState.CurrentRows)
                    cboRecieptType.SelectedValue = dr("TenderHeadCode").ToString()
                    Exit For
                Next
            Else
                If _PaymentBy = String.Empty Then
                    If _CardNo = String.Empty Or CLPRedemptionAllowed = False Then
                        filter = filter & " And TenderType <> 'CLPPoint'"
                    End If
                    dvPayment.RowFilter = filter

                    cboRecieptType.SelectedValue = "Cash"
                    IsPostiveTenderType = True
                    ctrlPayCash.txtCash.Text = TotalBillAmount
                ElseIf (_PaymentBy = "Cheque") Then
                    cboRecieptType.SelectedValue = "Cheque"
                    IsPostiveTenderType = True
                    dvPayment.RowFilter = filter
                    FunctionKeyEnabled()
                    ctrlPayCash.txtCash.Text = TotalBillAmount
                End If
            End If

            If _ParentForm = "CashMemo" Then
                btnOK.Visible = False
                btnCancel.Visible = True
                lblMinimumBalanceAmount.Visible = False
                lblCalMinBalDue.Visible = False
                lblCurrencyMinimumBalAmt.Visible = False
                lblCalCurrencyMiniBalDue.Visible = False
            ElseIf _ParentForm = "SalesOrder" Or _ParentForm = "BirthList" Then
                btnGift.Visible = False
            End If

            AddHandler CtrlPayCreditCheque.txtRemarks.KeyDown, AddressOf txtRemarks_PreviewKeyDown
            AddHandler CtrlChequeDetails.txtTelephoneNo.KeyDown, AddressOf txtTelephoneNo_PreviewKeyDown
            AddHandler CtrlChequeDetails.txtBankName.KeyDown, AddressOf txtTelephoneNo_PreviewKeyDown
            AddHandler CtrlChequeDetails.txtChequeNo.KeyDown, AddressOf txtTelephoneNo_PreviewKeyDown
            AddHandler CtrlChequeDetails.txtChequeNo.LostFocus, AddressOf txtChequeNo_LostFocus
            AddHandler CtrlChequeDetails.txtCustomerName.KeyDown, AddressOf txtTelephoneNo_PreviewKeyDown
            AddHandler CtrlChequeDetails.dtChequeDate.KeyDown, AddressOf txtTelephoneNo_PreviewKeyDown
            AddHandler ctrlPayCash.txtCash.KeyDown, AddressOf txtamount_PreviewKeyDown
            AddHandler ctrlPayCash.txtCash.Leave, AddressOf txtamount_lostfocus
            AddHandler ctrlPayCredit.txtAuthCode.KeyDown, AddressOf txtCCAuthCode_PreviewKeyDown
            AddHandler ctrlPayCredit.txtCreditCardNo.KeyDown, AddressOf txtCCAuthCode_PreviewKeyDown
            AddHandler ctrlPayCredit.txtSlipNO.KeyDown, AddressOf txtCCAuthCode_PreviewKeyDown
            AddHandler ctrlPayCredit.dtpExpiryDate.KeyDown, AddressOf txtCCAuthCode_PreviewKeyDown
            AddHandler ctrlPayCheque.ctrlGiftVouc.KeyDown, AddressOf dtpcheque_PreviewKeyDown
            AddHandler ctrlPayCheque.cmbVoucherProgram.KeyDown, AddressOf dtpcheque_PreviewKeyDown
            AddHandler CtrlGiftVoucherIssue.cmbVoucherProgram.KeyDown, AddressOf txtamount_PreviewKeyDown
            AddHandler ctrlPayCheque.txtChequeNo.KeyDown, AddressOf dtpcheque_PreviewKeyDown


            Check_PaymentTypeForScreen()

            Me.TopMost = False
            ctrlPayCash.txtCash.Select()
            cboRecieptType_Leave(cboRecieptType, e)
            ctrlPayCash.txtCash.Value = TotalBillAmount
            lblbalanceDue.Text = TotalBillAmount
            ''added for bug no 1537
            If IsPostiveTenderType = False Then
                ctrlPayCash.txtCash.Value = IIf(CDbl(ctrlPayCash.txtCash.Value) < 0, CDbl(ctrlPayCash.txtCash.Value) * -1, CDbl(ctrlPayCash.txtCash.Value))
            End If

            CtrlPayCreditCheque.Width = dgGridReciept.Width - CtrlPayCreditCheque.Location.X
            CtrlPayCreditCheque.txtRemarks.Width = dgGridReciept.Width - CtrlPayCreditCheque.txtRemarks.Location.X
            If Not String.IsNullOrEmpty(CLPCustomerCardNumber) Then
                Dim objClsClpCustomer As New clsCLPCustomer
                Dim clpPointInfo As DataTable = objClsClpCustomer.GetClpPointsForaCardNumber(CLPCustomerCardNumber)
                If Not clpPointInfo Is Nothing AndAlso clpPointInfo.Rows.Count > 0 Then
                    conversionRatio = (clpPointInfo.Rows(0)("AmtValue") / clpPointInfo.Rows(0)("Points"))
                    totalBalancePoints = clpPointInfo.Rows(0)("TotalBalancePoint")
                Else
                    totalBalancePoints = objClsClpCustomer.GetClpPoints(CLPCustomerCardNumber, clsAdmin.CLPProgram)
                End If
                CtrlCLPPoint.lblPointsValue.Text = totalBalancePoints
            End If
            If clsDefaultConfiguration.IsCustNameReadonly AndAlso Not String.IsNullOrEmpty(CLPCustomerName) Then
                CtrlChequeDetails.txtCustomerName.TextDetached = True
                CtrlChequeDetails.txtCustomerName.Text = CLPCustomerName
                CtrlChequeDetails.txtCustomerName.ReadOnly = True
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub
    ''' <summary>
    ''' Sub method of form calling 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub frmLoad()

        Try
            cboRecieptType.DisplayMember = "TenderHeadName"
            dtPayment = objBLLAcceptPayment.LoadRecieptType(PaymentType, clsAdmin.SiteCode)
            dvPayment = New DataView(dtPayment, filter, "", DataViewRowState.CurrentRows)
            cboRecieptType.DataSource = dvPayment
            cboRecieptType.DisplayMember = "TenderHeadName"
            cboRecieptType.ValueMember = "TenderHeadCode"
            pC1ComboSetDisplayMember(cboRecieptType)
            cboCurrency.DisplayMember = "CurrencyDescription"
            cboCurrency.ValueMember = "currencyCode"
            cboCurrency.DataSource = objBLLAcceptPayment.LoadCurrency(clsAdmin.SiteCode)
            pC1ComboSetDisplayMember(cboCurrency)
            cboCurrency.SelectedValue = clsAdmin.CurrencyCode  'objBLLAcceptPayment.GetLocalCurrency(clsAdmin.SiteCode)
            cboRecieptType.Focus()
            rtxtSwipeCard.Enabled = False
            'Dim dataRow As DataRowView = cboCurrency.SelectedItem
            lblDefaultCurrencyTrack.Text = cboCurrency.Text   'dataRow.Item("CurrencyDescription").ToString()
        Catch ex As Exception
            LogException(ex)
        Finally
            Debug.WriteLine("Form Load success")
        End Try
    End Sub
    ''' <summary>
    ''' Called when make payment by function key 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SwipeCard()
        Try
            If (IsPostiveTenderType) Then
                Dim TenderHead As String
                'If EnableOrDisable(AcceptPaymentTenderType.PositiveTenderType.CreditCard) Then
                For Each dr As DataRow In dtPayment.Select("TenderType='" & AcceptPaymentTenderType.PositiveTenderType.CreditCard & "'", "", DataViewRowState.CurrentRows)
                    TenderHead = dr("TenderHeadName").ToString()
                    Exit For
                Next
                Dim index As Int32 = cboRecieptType.FindStringExact(TenderHead)
                _IsSwipeCreditCard = False
                cboRecieptType.SelectedIndex = index   'Credit Card index'

                cboRecieptType.Enabled = False
                'sizBottom.Enabled = False
                IsFunctinalKeyActivate = False
                'Else
                'MessageBox.Show("DATA NOT FOUND")
                'End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    '''  Checking What type of payment
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Function Check_PaymentTypeForScreen() As Boolean
        Select Case (PaymentType)
            Case clsAcceptPayment.PaymentType.Advance
                Return ScreenSetting_AdvancePayment()
            Case clsAcceptPayment.PaymentType.EditBill
                Return PaymentType_EditBill()
            Case Else
                dsRecieptType.Clear()
                dtRecieptType.Clear()
                IsTotalAmountReceived()
        End Select
    End Function
    ''' <summary>
    '''  Display settings for scrren according to PaymentType
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ScreenSetting_AdvancePayment() As Boolean
        Try
            If (MinimumBillAmount > 0) Then
                lblMinimumBalanceAmount.Visible = True
                lblCurrencyMinimumBalAmt.Visible = True
                lblCalMinBalDue.Visible = True
                lblCalCurrencyMiniBalDue.Visible = True
                If lblDefaultCurrency.Text = cboCurrency.SelectedValue Then
                    Dim decCurrenctCurrencyAgainst As Decimal
                    lblCalMinBalDue.Text = objBLLAcceptPayment.CalculateTotalBillAmount_InCurrency(MinimumBillAmount, clsAdmin.CurrencyCode, decCurrenctCurrencyAgainst, clsAdmin.CurrencyCode)
                Else
                    lblCalMinBalDue.Text = MinimumBillAmount
                End If
                'Me.Width = 710
                'Me.Height = 540
                'btnOK.Location = New Point(467, 91)
                'grpboxBottom.Width = 705
                'grpBoxCurrency.Width = 705
                'grpboxBottom.Height = 120
                'btnCancel.Visible = True
                'btnCancel.Location = New Point(545, 91)
                Return True
            Else
                lblMinimumBalanceAmount.Visible = False
                lblCalMinBalDue.Visible = False
                'MessageBox.Show("You need to set minimum amount", "Spectrum")
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    ''' <summary>
    ''' Changes into last payment summary
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PaymentType_EditBill() As Boolean
        Try
            Return PaymentType_EditBill_DisplayDataIntoGrid()
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    ''' <summary>
    ''' sub method of  PaymentType_EditBill()
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PaymentType_EditBill_DisplayDataIntoGrid() As Boolean
        Try
            dsAcceptEditBillDataSet = AcceptEditBillDataSet
            dsRecieptType = objBLLAcceptPayment.GetDataset()
            dtRecieptType = dsRecieptType.Tables("MSTRecieptType")
            dtRecieptType.Merge(dsAcceptEditBillDataSet.Tables("MSTRecieptType"), False, MissingSchemaAction.Ignore)
            dgGridReciept.DataSource = dtRecieptType
            If (dtRecieptType.Rows.Count > 0) Then
                TotalReceivedAmount = dtRecieptType.Compute("Sum(Amount)", " ")
            End If
            'ctrlPayCash.txtCash.Text = TotalBillAmount - TotalReceivedAmount
            Grid_DisplaySetting()
            ctrlPayCash.txtCash.Text = 0
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function
    ''' <summary>
    '''  Enabled textbox for credit card entries 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReceiptTypeCreditCard_ManullyDisplaySetting()
        'Dim msgResult As DialogResult
        'msgResult = MessageBox.Show(Me, "Do you want insert credit card information manually ?", "Spectrum", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
        'If (msgResult = Windows.Forms.DialogResult.Yes) Then
        '    ctrlPayCheque.Visible = False
        '    ctrlPayCredit.Visible = True
        '    rtxtSwipeCard.Enabled = False
        '    If clsDefaultConfiguration.CreditCardInfo = False Then
        '        ctrlPayCredit.Visible = False
        '    End If
        'Else
        '    _IsSwipeCreditCard = True
        '    ReceiptTypeCreditCard_SwipeDisplaySetting()
        'End If

        'above code is comment because by default it will ask for cardinfo entering manually. if the pos mechine has swiper user need to click on swipe text and swipe the card
        ' while the card is swipe manully the card number will be populated in card no of manually. 
        ctrlPayCheque.Visible = False
        CtrlChequeDetails.Visible = False
        ctrlPayCredit.Visible = True
        rtxtSwipeCard.Enabled = True


        If clsDefaultConfiguration.CreditCardInfo = False Then
            'ctrlPayCredit.Visible = False ''commmented by rama 0n 27-nov-2009 as per rashid instrustion
            ctrlPayCash.Select()
        Else
            ctrlPayCredit.txtCreditCardNo.Select()
        End If
    End Sub
    ''' <summary>
    '''  Enabled textbox for credit card entries 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReceiptTypeCreditCard_SwipeDisplaySetting()
        ctrlPayCheque.Visible = False
        CtrlChequeDetails.Visible = False
        ctrlPayCredit.Visible = False
        rtxtSwipeCard.Enabled = True
        rtxtSwipeCard.Focus()
    End Sub
    ''' <summary>
    ''' Inserting credit card details into grid 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InsertCreditCardDeatils()
        Try
            If (Not IsSwipeCreditCard Or CheckAmountTextBox_NullOrInteger(strErrorMsg)) Then
                If clsDefaultConfiguration.CreditCardInfo = False Then
                    If clsDefaultConfiguration.IsCreditCardRefundAllowed = True Then
                        AddRowRecipetAmountInGrid(TenderHeadCode, EnteredAmount, ctrlPayCredit.txtCreditCardNo.Text, ctrlPayCredit.dtpExpiryDate.Value, ctrlPayCredit.cmbBankName.SelectedValue)
                        ClearTextBox()
                        ctrlPayCash.txtCash.Focus()
                        Exit Sub
                    End If
                    If ((TotalBillAmount - TotalReceivedAmount) >= EnteredAmount) Then
                        AddRowRecipetAmountInGrid(TenderHeadCode, EnteredAmount, ctrlPayCredit.txtCreditCardNo.Text, ctrlPayCredit.dtpExpiryDate.Value, ctrlPayCredit.cmbBankName.SelectedValue)
                        ClearTextBox()
                        ctrlPayCash.txtCash.Focus()
                    Else
                        ShowMessage(getValueByKey("ACP009"), "ACP009 - " & getValueByKey("CLAE04"))
                        ctrlPayCash.txtCash.Focus()
                        Exit Sub
                    End If
                Else
                    If ValidateCreditCardEntries() = True Then
                        If clsDefaultConfiguration.IsCreditCardRefundAllowed = True Then
                            AddRowRecipetAmountInGrid(TenderHeadCode, EnteredAmount, ctrlPayCredit.txtCreditCardNo.Text, ctrlPayCredit.dtpExpiryDate.Value, ctrlPayCredit.cmbBankName.SelectedValue)
                            ClearTextBox()
                            ctrlPayCash.txtCash.Focus()
                            Exit Sub
                        End If
                        If ((TotalBillAmount - TotalReceivedAmount) >= EnteredAmount) Then
                            AddRowRecipetAmountInGrid(TenderHeadCode, EnteredAmount, ctrlPayCredit.txtCreditCardNo.Text, ctrlPayCredit.dtpExpiryDate.Value, ctrlPayCredit.cmbBankName.SelectedValue)
                            ClearTextBox()
                            ctrlPayCash.txtCash.Focus()
                        Else
                            ShowMessage(getValueByKey("ACP009"), "ACP009 - " & getValueByKey("CLAE04"))
                            ctrlPayCash.txtCash.Focus()
                            Exit Sub
                        End If
                    Else
                        ctrlPayCredit.txtCreditCardNo.Focus()
                        Exit Sub
                    End If
                End If
                ctrlPayCash.txtCash.Focus()
            Else
                If (ValidateSwapedCreditCardEntries()) Then
                    If clsDefaultConfiguration.IsCreditCardRefundAllowed = True Then
                        AddRowRecipetAmountInGrid(TenderHeadCode, EnteredAmount, ctrlPayCredit.txtCreditCardNo.Text, ctrlPayCredit.dtpExpiryDate.Value, ctrlPayCredit.cmbBankName.SelectedValue)
                        ClearTextBox()
                        ctrlPayCash.txtCash.Focus()
                        Exit Sub
                    End If
                    If ((TotalBillAmount - TotalReceivedAmount) >= EnteredAmount) Then 'bug no 898

                        AddRowRecipetAmountInGrid(TenderHeadCode, EnteredAmount, ctrlPayCredit.txtCreditCardNo.Text, ctrlPayCredit.dtpExpiryDate.Value, ctrlPayCredit.cmbBankName.SelectedValue)
                        ClearTextBox()
                        ctrlPayCash.txtCash.Focus()
                    Else
                        ShowMessage(getValueByKey("ACP009"), "ACP009 - " & getValueByKey("CLAE04"))
                        ctrlPayCash.txtCash.Focus()
                        Exit Sub
                    End If
                End If
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Function ValidateCreditCardEntries() As Boolean
        Try
            If (String.IsNullOrEmpty(ctrlPayCredit.txtCreditCardNo.Text)) Then
                ctrlPayCredit.txtCreditCardNo.Focus()
                ShowMessage(getValueByKey("ACP006"), "ACP006 - " & getValueByKey("CLAE04"))
                'ElseIf (String.IsNullOrEmpty(ctrlPayCredit.txtSlipNO.Text)) Then
                '    ctrlPayCredit.txtSlipNO.Focus()
                '    ShowMessage(getValueByKey("ACP024"), "ACP024 - " & getValueByKey("CLAE04"))
                'ElseIf (String.IsNullOrEmpty(ctrlPayCredit.txtAuthCode.Text)) Then
                '    ctrlPayCredit.txtAuthCode.Focus()
                '    ShowMessage(getValueByKey("ACP024"), "ACP024 - " & getValueByKey("CLAE04"))
            ElseIf Not ctrlPayCredit.txtCreditCardNo.Text.Length >= 4 OrElse IsNumeric(ctrlPayCredit.txtCreditCardNo.Text) = False Then
                ctrlPayCredit.txtCreditCardNo.Focus()
                ShowMessage(getValueByKey("ACP021"), "ACP021 - " & getValueByKey("CLAE04"))
                'ElseIf (String.IsNullOrEmpty(ctrlPayCredit.dtpExpiryDate.Text)) Then
                '    ctrlPayCredit.dtpExpiryDate.Focus()
                '    ShowMessage(getValueByKey("ACP021"), "ACP021 - " & getValueByKey("CLAE04"))
                'ElseIf ctrlPayCredit.DateValid = False Then
                '    Return False
            Else
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try


    End Function
    Private Function ValidateSwapedCreditCardEntries() As Boolean
        Try
            If (String.IsNullOrEmpty(rtxtSwipeCard.Text)) Then
                rtxtSwipeCard.Focus()
                ShowMessage(getValueByKey("ACP022"), "ACP022 - " & getValueByKey("CLAE04"))
            Else
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try


    End Function
    Public Sub InsertCheque(ByVal Amt As Double, ByVal ChequeNo As String, ByVal ChequeDate As DateTime, ByVal MicrNo As String, ByVal BankName As String)
        Try
            ctrlPayCash.txtCash.Value = Amt
            ctrlPayCheque.txtChequeNo.Value = ChequeNo
            ctrlPayCheque.dtpExpiryDate.Value = ChequeDate
            InsertChequeDetails()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Inserting ChequeDetails into grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InsertChequeDetails()
        Try
            If SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.Cheque And TenderHeadCode = "CreditCheque" And CtrlPayCreditCheque.Visible Then

                Try
                    Dim bCheckExists As Boolean = False
                    If ctrlPayCheque.txtChequeNo.Text <> String.Empty Then
                        Dim strCheckNo As String = ctrlPayCheque.txtChequeNo.Text
                        If dsRecieptType.Tables.Contains("CheckDtls") Then
                            For Each drRow As DataRow In dsRecieptType.Tables("CheckDtls").Rows
                                If drRow("CheckNo") = strCheckNo And bCheckExists = False Then
                                    bCheckExists = True
                                End If
                            Next
                        End If
                    Else
                        ShowMessage(getValueByKey("ACP031"), "ACP031 - " & getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                    If bCheckExists = True Then
                        ShowMessage(getValueByKey("ACP033"), "ACP033 - " & getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                    dDueDate = CtrlPayCreditCheque.dtpDueDate.Value
                    strRemarks = CtrlPayCreditCheque.txtRemarks.Text
                    If CtrlPayCreditCheque.dtpDueDate.Value < Now.Date Then
                        ShowMessage(getValueByKey("CTR01"), "CTR01 - " & getValueByKey("CLAE05"))
                        Exit Sub
                    ElseIf ctrlPayCheque.txtChequeNo.Text = String.Empty Then
                        ShowMessage(getValueByKey("ACP031"), "ACP031 - " & getValueByKey("CLAE05"))
                        Exit Sub
                    End If


                Catch ex As Exception
                    LogException(ex)
                    Exit Sub
                End Try

                AddRowRecipetAmountInGrid(TenderHeadCode, EnteredAmount, ctrlPayCheque.txtChequeNo.Text, ctrlPayCheque.dtpExpiryDate.Value)
                ClearTextBox()
                Exit Sub

            ElseIf SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.Cheque And clsDefaultConfiguration.ChequeInfomation = False Then
                If (CheckAmountTextBox_NullOrInteger(strErrorMsg)) Then
                    ShowMessage(strErrorMsg, getValueByKey("CLAE04"))
                    ctrlPayCash.txtCash.Focus()
                    Exit Sub
                ElseIf clsDefaultConfiguration.IsChequeRefundAllowed = False AndAlso (EnteredAmount > (TotalBillAmount - TotalReceivedAmount)) Then
                    ShowMessage(getValueByKey("ACP009"), "ACP009 - " & getValueByKey("CLAE04"))
                    ctrlPayCash.txtCash.Clear()
                    ctrlPayCash.txtCash.Focus()
                    Exit Sub
                ElseIf String.IsNullOrEmpty(CtrlChequeDetails.txtChequeNo.Text) Or String.IsNullOrEmpty(CtrlChequeDetails.txtBankName.Text) _
                    Or String.IsNullOrEmpty(CtrlChequeDetails.txtCustomerName.Text) Then
                    ShowMessage(getValueByKey("frmqsrpayment.chequenoempty"), getValueByKey("CLAE04"))
                    CtrlChequeDetails.txtChequeNo.Focus()
                    Exit Sub
                ElseIf IsNumeric(CtrlChequeDetails.txtChequeNo.Text) = False Then
                    ShowMessage(getValueByKey("ctrlchequedetails.txtchequenovalid"), getValueByKey("CLAE04"))
                    CtrlChequeDetails.txtChequeNo.Focus()
                    Exit Sub
                Else
                    If SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.Cheque Then
                        AddRowRecipetAmountInGrid(TenderHeadCode, EnteredAmount, "", Now, CtrlChequeDetails.cmbBankName.SelectedValue)
                        Dim chkDetails As DataTable = objBLLAcceptPayment.GetCheckDetailsTableStruture()
                        Dim dataRow As DataRow
                        If dsRecieptType.Tables.Contains("CheckDtls") Then
                            dataRow = dsRecieptType.Tables("CheckDtls").NewRow()
                        Else
                            dataRow = chkDetails.NewRow()
                        End If
                        dataRow("PayLineNo") = dtRecieptType.Rows(dtRecieptType.Rows.Count - 1)("SrNo")
                        dataRow("CheckNo") = CtrlChequeDetails.txtChequeNo.Text
                        dataRow("Amount") = dtRecieptType.Rows(dtRecieptType.Rows.Count - 1)("Amount")
                        dataRow("DueDate") = CtrlChequeDetails.dtChequeDate.Value
                        dataRow("BankName") = CtrlChequeDetails.txtBankName.Text
                        dataRow("CustomerName") = CtrlChequeDetails.txtCustomerName.Text
                        dataRow("TelephoneNumber") = CtrlChequeDetails.txtTelephoneNo.Text
                        dataRow("STATUS") = 1
                        dtRecieptType.Rows(dtRecieptType.Rows.Count - 1)("Number") = CtrlChequeDetails.txtChequeNo.Text
                        chkDetails.TableName = "CheckDtls"
                        If dsRecieptType.Tables.Contains("CheckDtls") Then
                            dsRecieptType.Tables("CheckDtls").Rows.Add(dataRow)
                        Else
                            chkDetails.Rows.Add(dataRow)
                            dsRecieptType.Tables.Add(chkDetails)
                        End If
                        ClearTextBox()
                        Exit Sub
                    End If
                End If
            End If

            If Not (SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.CreditVoucR) And (CheckAmountTextBox_NullOrInteger(strErrorMsg)) Then
                ShowMessage(strErrorMsg, getValueByKey("CLAE05"))
                ctrlPayCash.txtCash.Focus()
            ElseIf (String.IsNullOrEmpty(ctrlPayCheque.txtChequeNo.Text)) Then
                Select Case (SelectedReceiptTypeCode)
                    Case AcceptPaymentTenderType.PositiveTenderType.GiftVoucher
                        ShowMessage(getValueByKey("ACP010"), "ACP010 - " & getValueByKey("CLAE04"))
                    Case AcceptPaymentTenderType.PositiveTenderType.CreditVoucR
                        ShowMessage(getValueByKey("ACP011"), "ACP011 - " & getValueByKey("CLAE04"))
                    Case AcceptPaymentTenderType.NegativeTenderType.CreditVoucherI
                        ShowMessage(getValueByKey("ACP011"), "ACP011 - " & getValueByKey("CLAE04"))
                    Case "Cheque"
                        ShowMessage(getValueByKey("ACP012"), "ACP012 - " & getValueByKey("CLAE04"))
                    Case Else
                        Exit Select
                End Select
            ElseIf ctrlPayCheque.DateValid = False Then
                Exit Sub
            Else
                If (SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.CreditVoucR) Then
                    InsertDataIntoGrid_CreditNoteR()
                ElseIf (SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.GiftVoucher) Then
                    InsertGiftVoucherDetails()
                ElseIf SelectedReceiptTypeCode = AcceptPaymentTenderType.NegativeTenderType.CreditVoucherI Then
                    InsertDataIntoGrid_CreditNoteI()
                ElseIf SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.Cheque Then
                    'If (IsChequeDateValid) Then
                    AddRowRecipetAmountInGrid(TenderHeadCode, EnteredAmount, ctrlPayCheque.txtChequeNo.Text, ctrlPayCheque.dtpExpiryDate.Value)
                    ClearTextBox()
                    'End If
                End If
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Accepting GiftVoucher Against Bill as tender mode
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InsertGiftVoucherDetails()
        Try
            'commented for bug no 538
            'If (CheckAmountTextBox_NullOrInteger(strErrorMsg)) Then
            '   Showmessage(strErrorMsg, getvaluebykey("CLAE05"))
            '    ctrlPayCash.txtCash.Focus()
            '    Exit Sub
            'Else
            If (String.IsNullOrEmpty(ctrlPayCheque.txtChequeNo.Text)) Then
                ShowMessage(getValueByKey("ACP010"), "ACP010 - " & getValueByKey("CLAE04"))
                ctrlPayCheque.txtChequeNo.Focus()
                Exit Sub
            ElseIf ctrlPayCheque.cmbVoucherProgram.SelectedIndex < 0 Then
                ShowMessage(getValueByKey("ACP029"), "ACP029 - " & getValueByKey("CLAE04"))
                ctrlPayCheque.cmbVoucherProgram.Focus()
                Exit Sub
            ElseIf ctrlPayCheque.DateValid = False Then
                Exit Sub
            Else
                Dim msg As String = ""
                Dim Amt As Double = 0
                Dim ExpiryDate As DateTime
                If objBLLAcceptPayment.FnGiftVoucherValidate(ctrlPayCheque.txtChequeNo.Text, msg, Amt, ExpiryDate, False, ctrlPayCheque.cmbVoucherProgram.SelectedValue) = False Then
                    If msg = "Voucher is Already Expired" Then
                        If MsgBox(getValueByKey("ACP025"), MsgBoxStyle.YesNo, "ACP025 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                            If CheckInterTransactionAuth("ACP_EXP_GV") = False Then
                                Exit Sub
                            Else
                                ExpiryDate = clsAdmin.CurrentDate.Date
                            End If
                        Else
                            Exit Sub
                        End If
                    Else
                        ShowMessage(msg, getValueByKey("CLAE04"))
                        ctrlPayCheque.txtChequeNo.Focus()
                        Exit Sub
                    End If
                Else
                    If objBLLAcceptPayment.IsCreditNoteUsed(ctrlPayCheque.txtChequeNo.Text, Amt) = True Then
                        ShowMessage(getValueByKey("ACP026"), "ACP026 - " & getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                    'If Amt > ctrlPayCash.txtCash.Text Then
                    ctrlPayCash.txtCash.Text = Amt
                    'End If
                End If
                'ctrlPayCheque.dtpExpiryDate.Text = ExpiryDate
                ctrlPayCheque.dtpExpiryDate.Value = ExpiryDate
                'If (IsChequeDateValid) Then
                AddRowRecipetAmountInGrid(TenderHeadCode, Amt, ctrlPayCheque.txtChequeNo.Text, ctrlPayCheque.dtpExpiryDate.Value)
                ClearTextBox()
                'End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Issue GiftVoucher Against Bill as tender mode
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InsertGiftVoucherIssue()
        Try
            If (CheckAmountTextBox_NullOrInteger(strErrorMsg)) Then
                ShowMessage(strErrorMsg, getValueByKey("CLAE05"))
                ctrlPayCash.txtCash.Focus()
            ElseIf (String.IsNullOrEmpty(CtrlGiftVoucherIssue.cmbVoucherProgram.Text)) Then
                ShowMessage(getValueByKey("ACP010"), "ACP010 - " & getValueByKey("CLAE04"))
                CtrlGiftVoucherIssue.cmbVoucherProgram.Focus()
                'ElseIf ctrlPayCheque.DateValid = False Then
                '    Exit Sub
            Else
                'Dim msg As String = ""
                'Dim Amt As Double = 0
                'Dim ExpiryDate As DateTime
                'If objBLLAcceptPayment.FnGiftVoucherValidate(ctrlPayCheque.txtChequeNo.Text, msg, Amt, ExpiryDate) = False Then
                '    ShowMessage(msg, "Information")
                '    ctrlPayCheque.txtChequeNo.Focus()
                '    Exit Sub
                'Else
                '    If objBLLAcceptPayment.IsCreditNoteUsed(ctrlPayCheque.txtChequeNo.Text, Amt) = True Then
                '        ShowMessage("Already used in current transaction", "Information")
                '        Exit Sub
                '    End If
                '    If Amt < ctrlPayCash.txtCash.Text Then
                '        ctrlPayCash.txtCash.Text = Amt
                '    End If
                'End If
                'ctrlPayCheque.dtpExpiryDate.Text = ExpiryDate
                'ctrlPayCheque.dtpExpiryDate.Value = ExpiryDate
                'If (IsChequeDateValid) Then

                AddRowRecipetAmountInGrid(TenderHeadCode, EnteredAmount, CtrlGiftVoucherIssue.cmbVoucherProgram.SelectedValue, CtrlGiftVoucherIssue.ExpiryDate)
                ClearTextBox()
                'End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE04"))
            LogException(ex)
        End Try
    End Sub
    Dim conversionRatio As Decimal = 1
    Dim totalBalancePoints As Double
    ''' <summary>
    '''  Inserting Cash Details into grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InsertReceiptCashDetails()
        Try
            If (SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.CLPPoint) Then
                'If (CheckAmountTextBox_NullOrInteger(strErrorMsg)) Then
                '    ShowMessage(True, strErrorMsg, getValueByKey("CLAE05"))
                'Else
                '    Dim AcceptPoints As Double
                '    Try
                '        For Each Row As DataRow In dgGridReciept.DataSource.rows
                '            If Row("RecieptTypeCode") = AcceptPaymentTenderType.PositiveTenderType.CLPPoint Then
                '                AcceptPoints = AcceptPoints + Row("Amount")
                '            End If
                '        Next
                '    Catch ex As Exception
                '    End Try

                '    AcceptPoints = AcceptPoints + CDbl(CheckIfBlank(ctrlPayCash.txtCash.Text))
                'If objBLLAcceptPayment.ValidateCLP(_CardNo, clsAdmin.CLPProgram, AcceptPoints) = False Then
                '        ShowMessage(getValueByKey("ACP027"), "ACP027 - " & getValueByKey("CLAE04"))
                '        ctrlPayCash.txtCash.Clear()
                '        Exit Sub
                '    End If
                '    AddRowRecipetAmountInGrid(_tenderHeadCode, EnteredAmount)
                '    ClearTextBox()
                '    ctrlPayCash.txtCash.Clear()
                'End If
                Dim totalPoints As Double
                If (CheckAmountTextBox_NullOrInteger(strErrorMsg)) Then
                    ShowMessage(True, strErrorMsg, getValueByKey("CLAE05"))
                Else
                    If dgGridReciept.Cols.Contains("Points") Then
                        For Each Row As DataRow In dgGridReciept.DataSource.rows
                            If Row("RecieptTypeCode") = AcceptPaymentTenderType.PositiveTenderType.CLPPoint Then
                                totalPoints = totalPoints + Row("Points")
                            End If
                        Next
                    End If
                    If totalPoints + (EnteredAmount / conversionRatio) > totalBalancePoints Then
                        ShowMessage(getValueByKey("ACP027"), "ACP027 - " & getValueByKey("CLAE04"))
                        ctrlPayCash.txtCash.Clear()
                        Exit Sub
                    End If
                    CtrlCLPPoint.lblPointsValue.Text = CDbl(CheckIfBlank(CtrlCLPPoint.lblPointsValue.Text)) - (EnteredAmount / conversionRatio)
                    CtrlCLPPoint.lblPointAmount.Text = CDbl(CheckIfBlank(CtrlCLPPoint.lblPointsValue.Text)) * conversionRatio
                    Dim previousAmount As Double = EnteredAmount
                    AddRowRecipetAmountInGrid(_tenderHeadCode, EnteredAmount, True)
                    Dim dataRow As DataRow = dgGridReciept.DataSource.rows(dgGridReciept.Rows.Count - 2)
                    dataRow("Points") = previousAmount / conversionRatio
                    'objClsClpCustomer.UpdateClpPointsInfo(CLPCustomerCardNumber, clsAdmin.CLPProgram, totalBalancePoints - dataRow("Points"))
                    ClearTextBox()
                    ctrlPayCash.txtCash.Clear()
                End If
            ElseIf (SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.Cash) Then 'Or SelectedReceiptTypeCode = AcceptPaymentTenderType.NegativeTenderType.Cash) Then 'Or SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.ParkingCoupen) Then
                If (CheckAmountTextBox_NullOrInteger(strErrorMsg)) Then
                    ShowMessage(True, strErrorMsg, getValueByKey("CLAE05"))
                Else
                    If Not (IsRecieptAmountClose) Then
                        Dim Rema As Double
                        If clsAdmin.CurrencyCode = cboCurrency.SelectedValue Then
                            Dim currentamt As Double
                            currentamt = EnteredAmount
                            'If lblSign.Text <> String.Empty Then
                            '    currentamt = currentamt * -1
                            'End If
                            Rema = currentamt + TotalReceivedAmount
                        Else
                            Rema = 0
                        End If

                        If Rema > TotalBillAmount Then
                            If TenderHeadCode = "Cash" Then
                                TotalCashTendered = EnteredAmount
                            End If
                            If TotalCashTendered > (Rema - TotalBillAmount) Then
                                intReturnCashToCust = (Rema - TotalBillAmount)
                                Rema = EnteredAmount - (Rema - TotalBillAmount)
                                'MessageOnAnotherTheared()
                                dsRecieptType = objBLLAcceptPayment.AddRowRecipetAmountInGrid_Cash(_tenderHeadName, TenderHeadCode, Rema, SelectedCurrencyIndex, clsAdmin.CurrencyCode, SelectedCurrencySymbol, SelectedReceiptTypeCode, cboCurrency.SelectedValue)
                                dtRecieptType = dsRecieptType.Tables("MSTRecieptType")
                                dgGridReciept.DataSource = dtRecieptType
                                'ctrlPayCash.txtCash.Text = 0
                                Grid_DisplaySetting()
                                ClearTextBox()
                                'End If

                                'If MsgBox("Return Customer " & Rema - TotalBillAmount, MsgBoxStyle.YesNo, "Information") = MsgBoxResult.Yes Then
                                '    'Rakesh: 28-05-09 - Add cboCurrency.SelectedValue for same receipt type with diffrent currency
                                '    Rema = EnteredAmount - (Rema - TotalBillAmount)
                                '    dsRecieptType = objBLLAcceptPayment.AddRowRecipetAmountInGrid_Cash(_tenderHeadName, TenderHeadCode, Rema, SelectedCurrencyIndex, clsAdmin.CurrencyCode, SelectedCurrencySymbol, SelectedReceiptTypeCode, cboCurrency.SelectedValue)
                                '    dtRecieptType = dsRecieptType.Tables("MSTRecieptType")
                                '    dgGridReciept.DataSource = dtRecieptType
                                '    'ctrlPayCash.txtCash.Text = 0
                                '    Grid_DisplaySetting()
                                '    ClearTextBox()
                            Else
                                dsRecieptType = objBLLAcceptPayment.AddRowRecipetAmountInGrid_Cash(_tenderHeadName, TenderHeadCode, EnteredAmount, SelectedCurrencyIndex, clsAdmin.CurrencyCode, SelectedCurrencySymbol, SelectedReceiptTypeCode, cboCurrency.SelectedValue)
                                dtRecieptType = dsRecieptType.Tables("MSTRecieptType")
                                dgGridReciept.DataSource = dtRecieptType
                                Grid_DisplaySetting()
                                ClearTextBox()
                            End If
                            ctrlPayCash.txtCash.Clear()
                        Else
                            dsRecieptType = objBLLAcceptPayment.AddRowRecipetAmountInGrid_Cash(_tenderHeadName, TenderHeadCode, EnteredAmount, SelectedCurrencyIndex, clsAdmin.CurrencyCode, SelectedCurrencySymbol, SelectedReceiptTypeCode, cboCurrency.SelectedValue)
                            dtRecieptType = dsRecieptType.Tables("MSTRecieptType")
                            dgGridReciept.DataSource = dtRecieptType
                            Grid_DisplaySetting()
                            ClearTextBox()
                        End If
                        ctrlPayCash.txtCash.Clear()
                    Else
                        ShowMessage(getValueByKey("ACP013"), "ACP013 - " & getValueByKey("CLAE04"))
                    End If
                End If
                ctrlPayCash.txtCash.Clear()
                'Rohit Enter code
            ElseIf (SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.Cheque) Then
                If TenderHeadCode = "CreditCheque" Then
                    If ctrlPayCheque.txtChequeNo.Text = String.Empty Then
                        ShowMessage(getValueByKey("ACP031"), "ACP031 - " & getValueByKey("CLAE04"))
                        ctrlPayCash.txtCash.Clear()
                        Exit Sub
                    ElseIf CtrlPayCreditCheque.dtpDueDate.Value < Now.Date Then
                        ShowMessage(getValueByKey("ACP032"), "ACP032 - " & getValueByKey("CLAE04"))
                        ctrlPayCash.txtCash.Clear()
                        Exit Sub
                    End If
                End If


                If (CheckAmountTextBox_NullOrInteger(strErrorMsg)) Then
                    ShowMessage(True, strErrorMsg, getValueByKey("CLAE05"))
                    ctrlPayCash.txtCash.Clear()
                Else
                    'Change by Ashish on Dec 1, 2010
                    'Fix for Bug 6034 on mantis
                    'Added a check for IsRecieptAmountClose before InsertChequeDetails
                    If Not (IsRecieptAmountClose) Then
                        InsertChequeDetails()
                    Else
                        ShowMessage(getValueByKey("ACP013"), "ACP013 - " & getValueByKey("CLAE04"))
                    End If
                    'End of change                   
                End If
                ctrlPayCash.txtCash.Clear()
            ElseIf (SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.CreditCard) Then
                If (CheckAmountTextBox_NullOrInteger(strErrorMsg)) Then
                    ShowMessage(True, strErrorMsg, getValueByKey("CLAE05"))
                Else
                    If clsDefaultConfiguration.IsCreditCardRefundAllowed = True Then
                        If (IsSwipeCreditCard) Then
                            If Not (IsRecieptAmountClose) Then
                                AddRowRecipetAmountInGrid(SelectedReceiptTypeCode, EnteredAmount, "102341234", "01/02/2010")
                                ctrlPayCash.txtCash.Clear()
                                Exit Sub
                            End If
                        Else
                            'Change by Ashish on Dec 1, 2010
                            'Fix for Bug 6034 on mantis
                            'Added a check for IsRecieptAmountClose before InsertChequeDetails
                            If Not (IsRecieptAmountClose) Then
                                InsertCreditCardDeatils()
                                ctrlPayCash.txtCash.Clear()
                                Exit Sub
                            Else
                                ShowMessage(getValueByKey("ACP013"), "ACP013 - " & getValueByKey("CLAE04"))
                            End If
                            'End of change
                        End If
                    End If

                    If ((TotalBillAmount - TotalReceivedAmount) >= EnteredAmount) Then
                        If (IsSwipeCreditCard) Then
                            If Not (IsRecieptAmountClose) Then
                                AddRowRecipetAmountInGrid(SelectedReceiptTypeCode, EnteredAmount, "102341234", "01/02/2010")
                                ctrlPayCash.txtCash.Clear()
                                Exit Sub
                            End If
                        Else
                            'Change by Ashish on Dec 1, 2010
                            'Fix for Bug 6034 on mantis
                            'Added a check for IsRecieptAmountClose before InsertChequeDetails
                            If Not (IsRecieptAmountClose) Then
                                InsertCreditCardDeatils()
                                ctrlPayCash.txtCash.Clear()
                                Exit Sub
                            Else
                                ShowMessage(getValueByKey("ACP013"), "ACP013 - " & getValueByKey("CLAE04"))
                            End If
                            'End of change

                        End If
                    Else
                        ShowMessage(getValueByKey("ACP009"), "ACP009 - " & getValueByKey("CLAE04"))
                    End If
                End If
                ctrlPayCash.txtCash.Clear()
            ElseIf (SelectedReceiptTypeCode = AcceptPaymentTenderType.NegativeTenderType.CreditVoucherI) Then

                InsertDataIntoGrid_CreditNoteI()
                'byram cboRecieptType_SelectedValueChanged(cboRecieptType, New EventArgs)
                cboRecieptType_Leave(cboRecieptType, New EventArgs)
                ctrlPayCash.txtCash.Clear()
            ElseIf (SelectedReceiptTypeCode = AcceptPaymentTenderType.NegativeTenderType.GiftVoucherI) Then
                InsertGiftVoucherIssue()
                ctrlPayCash.txtCash.Clear()
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Inseritng credit note details into grid 
    ''' </summary>
    ''' <remarks></remarks>
    Sub InsertDataIntoGrid_CreditNoteR()
        Dim errorMsg As String = String.Empty
        If Not (IsRecieptAmountClose) Then
            Dim msg As String = ""
            Dim Amt As Double = 0
            Dim ExpiryDate As DateTime
            If objBLLAcceptPayment.FnGiftVoucherValidate(ctrlPayCheque.txtChequeNo.Text, msg, Amt, ExpiryDate) = False Then
                msg = msg.Replace("Gift Voucher", "Credit Voucher")
                ShowMessage(msg, getValueByKey("CLAE04"))
                ctrlPayCheque.txtChequeNo.Focus()
                Exit Sub
            End If
            ctrlPayCash.txtCash.Value = Amt
            'ctrlPayCheque.dtpExpiryDate.Text = ExpiryDate
            ctrlPayCheque.dtpExpiryDate.Value = ExpiryDate
            dsRecieptType = objBLLAcceptPayment.AddRowRecieptAmountInGrid_CreditNote(clsAdmin.CreditValidDays, _tenderHeadName, clsAdmin.CurrencyCode, TenderHeadCode, EnteredAmount, ctrlPayCheque.txtChequeNo.Text, errorMsg, SelectedReceiptTypeCode, clsAdmin.SiteCode, clsAdmin.CVProgram)
            dtRecieptType = dsRecieptType.Tables("MSTRecieptType")
            dgGridReciept.DataSource = dtRecieptType
            Grid_DisplaySetting()
            If Not (String.IsNullOrEmpty(errorMsg)) Then
                ShowMessage(errorMsg, getValueByKey("CLAE05"))
            End If
        Else
            ShowMessage(getValueByKey("ACP013"), "ACP013 - " & getValueByKey("CLAE04"))
        End If

    End Sub
    ''' <summary>
    ''' Inseritng credit note details into grid 
    ''' </summary>
    ''' <remarks></remarks>
    Sub InsertDataIntoGrid_CreditNoteI()
        Dim errorMsg As String = String.Empty
        If Not (IsRecieptAmountClose) Then
            dsRecieptType = objBLLAcceptPayment.AddRowRecieptAmountInGrid_CreditNote(clsAdmin.CreditValidDays, _tenderHeadName, clsAdmin.CurrencyCode, TenderHeadCode, EnteredAmount, " ", errorMsg, SelectedReceiptTypeCode, clsAdmin.SiteCode, clsAdmin.CVProgram)
            dtRecieptType = dsRecieptType.Tables("MSTRecieptType")
            dgGridReciept.DataSource = dtRecieptType
            Grid_DisplaySetting()
            If Not (String.IsNullOrEmpty(errorMsg)) Then
                ShowMessage(errorMsg, getValueByKey("CLAE05"))
            End If
        Else
            ShowMessage(getValueByKey("ACP013"), "ACP013 - " & getValueByKey("CLAE04"))
        End If
    End Sub
    ''' <summary>
    '''  Display grid 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Grid_DisplaySetting()
        Try
            'dgGridReciept.Cols.Count = 6
            dgGridReciept.Cols(0).Visible = False

            dgGridReciept.Cols(1).Caption = "Tender Type"
            dgGridReciept.Cols(3).Caption = "Base Amount"
            dgGridReciept.Cols(4).Caption = "Actual Amount"
            dgGridReciept.Cols(5).Caption = "Details"

            dgGridReciept.Cols(1).Caption = modCommonFunc.getValueByKey("frmnacceptpayment.dggridreciept.tendertype")
            dgGridReciept.Cols(3).Caption = modCommonFunc.getValueByKey("frmnacceptpayment.dggridreciept.as above_0")
            dgGridReciept.Cols(4).Caption = modCommonFunc.getValueByKey("frmnacceptpayment.dggridreciept.ActualAmount")
            dgGridReciept.Cols(5).Caption = modCommonFunc.getValueByKey("frmnacceptpayment.dggridreciept.as above_1")

            dgGridReciept.Refresh()
            IsTotalAmountReceived()
            dgGridReciept.Cols("Date").Visible = True
            dgGridReciept.Cols("AmountInCurrency").Visible = True
            dgGridReciept.Cols(3).AllowEditing = False
            dgGridReciept.Cols(2).AllowEditing = False
            dgGridReciept.Cols(0).AllowEditing = False
            dgGridReciept.Cols("RecieptType").Visible = False
            dgGridReciept.AllowEditing = False
            dgGridReciept.Cols(4).Visible = False
            dgGridReciept.Cols(7).Visible = False
            dgGridReciept.Cols(8).Visible = False
            dgGridReciept.Cols(9).Visible = False

            'Rakesh-10.11.2013-7615->Set default column caption
            dgGridReciept.Cols("BankAccNo").Visible = False
            dgGridReciept.Cols("NOCLP").Visible = False
            dgGridReciept.Cols("IssuedForCLP").Visible = False
            dgGridReciept.Cols(10).Caption = ""
            dgGridReciept.Cols(11).Caption = ""

            dgGridReciept.AutoSizeCol(1, 10)
            dgGridReciept.AutoSizeCol(3, 10)
            dgGridReciept.AutoSizeCol(4, 10)
            dgGridReciept.AutoSizeCol(5, 10)



            'SetCulture(Me, Me.Name)
        Catch ex As Exception

        End Try

        'Try
        '    dgGridReciept.Cols.Count = 6
        '    dgGridReciept.Cols(0).Visible = False
        '    dgGridReciept.Cols(3).Caption = "As above"
        '    dgGridReciept.Cols(4).Caption = "As above"
        '    dgGridReciept.Refresh()
        '    IsTotalAmountReceived()
        '    dgGridReciept.Cols("Date").Visible = True
        '    dgGridReciept.Cols(3).AllowEditing = False
        '    dgGridReciept.Cols(2).AllowEditing = False
        '    dgGridReciept.Cols(0).AllowEditing = False
        '    dgGridReciept.Cols("RecieptType").Visible = False
        '    dgGridReciept.AllowEditing = False
        'Catch ex As Exception

        'End Try
    End Sub
    ''' <summary>
    '''  On Selection changed of currency.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DisplaySettings_CurrencyCalculation()
        pnlBoxCurrency.Visible = True
    End Sub
    ''' <summary>
    ''' Clear TextBox 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearTextBox()
        Try
            If Not ctrlPayCash.txtCash.Value Is Nothing AndAlso lblbalanceDue.Text = 0 Then
                ctrlPayCash.txtCash.Value = String.Empty
                lblSign.Text = String.Empty
            End If

            'IIf(Not ctrlPayCash.txtCash.Value Is Nothing, , Nothing)
            ctrlPayCredit.txtAuthCode.Text = String.Empty
            If CtrlPayCreditCheque.Visible = False Then
                ctrlPayCheque.txtChequeNo.Value = String.Empty
            End If
            ctrlPayCredit.txtCreditCardNo.Value = String.Empty
            ctrlPayCredit.txtSlipNO.Text = String.Empty

            ctrlPayCash.txtCash.Focus()
        Catch ex As Exception

        End Try
    End Sub
    ''' <summary>
    ''' Inserting CLP details into grid 
    ''' </summary>
    ''' <param name="strRecieptType"></param>
    ''' <param name="strAmount"></param>
    ''' <remarks></remarks>
    Protected Sub AddRowRecipetAmountInGrid(ByVal strRecieptType As String, ByVal strAmount As String, Optional ByVal isClpPoint As Boolean = False)
        If Not (IsRecieptAmountClose) Then
            dsRecieptType = objBLLAcceptPayment.AddRowRecipetAmountInGrid(_tenderHeadName, strRecieptType, strAmount, SelectedReceiptTypeCode)
            dtRecieptType = dsRecieptType.Tables("MSTRecieptType")
            If isClpPoint Then
                If Not dtRecieptType.Columns.Contains("Points") Then
                    dtRecieptType.Columns.Add(New DataColumn("Points"))
                    dtRecieptType.Columns("Points").Caption = "Points"
                End If
            End If
            dgGridReciept.DataSource = dtRecieptType
            ctrlPayCash.txtCash.Text = 0
            Grid_DisplaySetting()
        Else
            ShowMessage(getValueByKey("ACP013"), "ACP013 - " & getValueByKey("CLAE04"))
        End If
    End Sub

    ''' <summary>
    '''  checking Credit Card,Cheque Details 
    ''' </summary>
    ''' <param name="strRecieptType"></param>
    ''' <param name="strAmount"></param>
    ''' <param name="strNumber"></param>
    ''' <param name="dtDate"></param>
    ''' <remarks></remarks>

    Protected Sub AddRowRecipetAmountInGrid(ByVal strRecieptType As String, ByVal strAmount As String, ByVal strNumber As String, ByVal dtDate As Object, Optional ByVal bankName As String = "")
        If Not (IsRecieptAmountClose) Then

            'Added by Rohit for CR-5938

            objBLLAcceptPayment.dDueDate = dDueDate
            objBLLAcceptPayment.strRemarks = strRemarks
            'dsRecieptType = objBLLAcceptPayment.AddRowRecipetAmountInGrid(_tenderHeadName, clsAdmin.CurrencyCode, strRecieptType, strAmount, strNumber, dtDate, SelectedReceiptTypeCode)

            If CtrlPayCreditCheque.Visible = True Then
                dsRecieptType = objBLLAcceptPayment.AddRowRecipetAmountInGrid(_tenderHeadName, clsAdmin.CurrencyCode, strRecieptType, strAmount, strNumber, dtDate, SelectedReceiptTypeCode, dDueDate, strRemarks, bankName)
            Else
                dsRecieptType = objBLLAcceptPayment.AddRowRecipetAmountInGrid(_tenderHeadName, clsAdmin.CurrencyCode, strRecieptType, strAmount, strNumber, dtDate, SelectedReceiptTypeCode, Nothing, "", bankName)
            End If
            'Added by Rohit for CR-5938

            dtRecieptType = dsRecieptType.Tables("MSTRecieptType")
            dgGridReciept.DataSource = dtRecieptType



            Grid_DisplaySetting()
        Else
            ShowMessage(getValueByKey("ACP013"), "ACP013 - " & getValueByKey("CLAE04"))
        End If
    End Sub
    ''' <summary>
    '''  Check Total amount is received
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsTotalAmountReceived() As Boolean
        CalculateAmountInCurrency(True)
        lblRetrunAmount.Text = String.Empty
        If (dtRecieptType.Rows.Count > 0) Then

            If IsPostiveTenderType Then
                Dim strSum As Object
                strSum = dtRecieptType.Compute("Sum(Amount)", " ")
                Dim decReceivedAmount As Decimal = Decimal.Zero
                If Not strSum Is Nothing Then
                    decReceivedAmount = strSum
                    Dim diffAmt As Double = (TotalBillAmount - strSum)
                    Dim originalDiffamt As Double = diffAmt
                    diffAmt = IIf(diffAmt < 0, diffAmt * -1, diffAmt)
                    If TotalBillAmount <> strSum AndAlso diffAmt < (_Roundat / 100) Then
                        strSum = strSum + originalDiffamt
                    End If
                End If
                lblTotalReciept.Text = FormatNumber(strSum, 2)
                lblbalanceDue.Text = FormatNumber(Decimal.Subtract(TotalBillAmount, CDbl(strSum)), 2)

                If (TotalBillAmount <= decReceivedAmount) Then
                    Dim ireturnAmount As Decimal = Decimal.Subtract(TotalBillAmount, CDbl(strSum))
                    ireturnAmount = Math.Round(ireturnAmount, 2)
                    If (CDbl(strSum) > TotalBillAmount) Then
                        IsRecieptAmountClose = False
                        filter = "Positive_Negative='*'"
                        dvPayment.RowFilter = filter
                        If Not cboRecieptType.SelectedValue Is Nothing Then
                            cboRecieptType_SelectedIndexChanged(cboRecieptType, New System.EventArgs)
                        End If
                        cboRecieptType.SelectedIndex = 0
                        cboRecieptType_Leave(cboRecieptType, New System.EventArgs)
                        cboCurrency.SelectedValue = clsAdmin.CurrencyCode
                        cboRecieptType_Leave(cboRecieptType, New System.EventArgs)
                        'cboRecieptType_SelectedValueChanged(cboRecieptType, New System.EventArgs)
                    ElseIf (CDbl(strSum) = TotalBillAmount) Then
                        IsRecieptAmountClose = True
                        'lblRetrunAmount.Text = String.Format("Total Receipt Amount has been collected.", Strings.Replace(ireturnAmount, "-", ""))
                        lblRetrunAmount.Text = getValueByKey("frmnacceptpayment.lblretrunamount1")
                        ctrlPayCash.txtCash.Text = 0
                    Else
                        IsRecieptAmountClose = False
                    End If
                Else
                    IsRecieptAmountClose = False
                    ctrlPayCash.txtCash.Value = String.Empty
                    If clsAdmin.CurrencyCode <> cboCurrency.SelectedValue Then
                        ctrlPayCash.txtCash.Value = lblCalCurrencyBalanceDue.Text
                    Else
                        ctrlPayCash.txtCash.Value = lblbalanceDue.Text
                    End If

                    ctrlPayCash.txtCash.Enabled = True
                End If
            Else
                RefundAmountCalculation()
            End If

        Else
            IsRecieptAmountClose = False
            ctrlPayCash.txtCash.Enabled = True
            ctrlPayCash.txtCash.Text = TotalBillAmount
        End If
    End Function

    Private Function RefundAmountCalculation() As Boolean
        If Not IsPostiveTenderType Then

            Dim strSum As Object
            strSum = dtRecieptType.Compute("Sum(Amount)", " ")
            'strSum = -strSum
            lblTotalReciept.Text = FormatNumber(-strSum, 2)
            lblbalanceDue.Text = FormatNumber(Decimal.Subtract(TotalBillAmount, CDbl(strSum)), 2)
            If (TotalBillAmount >= CDbl(strSum)) Then
                Dim ireturnAmount As Decimal = Decimal.Subtract(TotalBillAmount, CDbl(strSum))
                If (CDbl(strSum) < TotalBillAmount) Then
                    filter = "Positive_Negative='*'"
                    dvPayment.RowFilter = filter
                    cboRecieptType_SelectedIndexChanged(cboRecieptType, New System.EventArgs)
                    cboRecieptType.SelectedIndex = 0
                ElseIf (CDbl(strSum) = TotalBillAmount) Then
                    IsRecieptAmountClose = True
                    'lblRetrunAmount.Text = String.Format("Total Refund Amount has been given.", Strings.Replace(ireturnAmount, "-", ""))
                    lblRetrunAmount.Text = getValueByKey("frmnacceptpayment.lblretrunamount2")
                Else
                    IsRecieptAmountClose = False
                End If
            Else
                IsRecieptAmountClose = False
                ctrlPayCash.txtCash.Enabled = True
            End If
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Calculate Total Reciept Summray and display
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CalculateAmountInCurrency(ByVal reverse As Boolean) As Boolean
        'lblCalCurrencyHeader.Text = "Bill in " + SelectedCurrencyDescription
        lblCalCurrencyHeader.Text = getValueByKey("ACP030") & " " + SelectedCurrencyDescription
        Dim decSelectedCurrencyRate As Decimal
        Dim decBillAmount As Decimal = objBLLAcceptPayment.CalculateTotalBillAmount_InCurrency(TotalBillAmount, SelectedCurrencyIndex, decSelectedCurrencyRate, clsAdmin.CurrencyCode)
        decBillAmount = FormatNumber(decBillAmount, 2)
        lblCalCurrencyBillAmount.Text = decBillAmount
        Dim decRecievedTotalAmount As Decimal
        decRecievedTotalAmount = CDbl(objBLLAcceptPayment.CalculateTotalRecieptAmount_InCurrency(decSelectedCurrencyRate, reverse))
        TotalReceivedAmount = decRecievedTotalAmount
        lblCalCurrencyTotalReciepts.Text = FormatNumber(decRecievedTotalAmount, 2)
        Dim decBalanceDue As Decimal
        If IsPostiveTenderType Then
            decBalanceDue = objBLLAcceptPayment.CalculateBalanceDue(decBillAmount, decRecievedTotalAmount)
        Else
            decRecievedTotalAmount = -decRecievedTotalAmount
            decBalanceDue = objBLLAcceptPayment.CalculateBalanceDue(decBillAmount, decRecievedTotalAmount)
        End If

        lblCalCurrencyBalanceDue.Text = FormatNumber(decBalanceDue, 2)
        ctrlPayCash.txtCash.Value = FormatNumber(decBalanceDue, 2)
        If (PaymentType = clsAcceptPayment.PaymentType.Advance) Then
            lblCalCurrencyMiniBalDue.Text = objBLLAcceptPayment.CalculateTotalBillAmount_InCurrency(MinimumBillAmount, SelectedCurrencyIndex, decSelectedCurrencyRate, clsAdmin.CurrencyCode)
            lblCalCurrencyMiniBalDue.Text = FormatNumber(CDbl(lblCalCurrencyMiniBalDue.Text), 2)
            '--added by rama for bug-0000784
            If lblCalMinBalDue.Text <> String.Empty Then
                Dim minVal As Double = objBLLAcceptPayment.CalculateBalanceDue(MinimumBillAmount, decRecievedTotalAmount)
                If minVal > 0 Then
                    lblCalMinBalDue.Text = minVal
                Else
                    lblCalMinBalDue.Text = 0
                End If
                'If CDbl(strSum) >= CDbl(lblCalMinBalDue.Text) Then
                '    lblCalMinBalDue.Text = 0.0
                'Else
                '    lblCalMinBalDue.Text = Format(Decimal.Subtract(MinimumBillAmount, CDbl(strSum)), "0.00")
                'End If
            End If
            '---
        End If
    End Function
#End Region

#Region "Events"
    ''' <summary>
    '''  Called when all information receipt type is filled
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtCCAuthCode_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If (e.KeyCode = Keys.Enter) Then
            If (CheckAmountTextBox_NullOrInteger(strErrorMsg)) Then
                ShowMessage(True, strErrorMsg, getValueByKey("CLAE05"))
                ctrlPayCash.txtCash.Focus()
            Else
                InsertCreditCardDeatils()
            End If
        End If
    End Sub
    ''' <summary>
    ''' On AmountTextBox
    '''  Called when all information receipt type is filled. 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub txtamount_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If (e.KeyCode = Keys.Enter) AndAlso ctrlPayCheque.Visible = False AndAlso CtrlChequeDetails.Visible = False Then
            InsertReceiptCashDetails()
        ElseIf (e.KeyCode = Keys.Tab) And ctrlPayCheque.Visible = True Then
            ctrlPayCheque.txtChequeNo.Focus()
        ElseIf (e.KeyCode = Keys.Tab) And CtrlChequeDetails.Visible = True Then
            CtrlChequeDetails.txtChequeNo.Focus()
        ElseIf (e.KeyCode = Keys.Enter) And ctrlPayCheque.Visible = True Then
            ctrlPayCheque.txtChequeNo.Focus()
        ElseIf (e.KeyCode = Keys.Enter) And CtrlChequeDetails.Visible = True Then
            CtrlChequeDetails.txtChequeNo.Focus()
        End If
    End Sub

    Private Sub txtRemarks_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If (e.KeyCode = Keys.Enter) And ctrlPayCheque.Visible = True Then
            InsertChequeDetails()
        End If
    End Sub

    Private Sub txtTelephoneNo_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If (e.KeyCode = Keys.Enter) And CtrlChequeDetails.Visible = True Then
            InsertChequeDetails()
        End If
    End Sub

    Private Sub txtChequeNo_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not String.IsNullOrEmpty(CtrlChequeDetails.txtChequeNo.Text) Then
            If CtrlChequeDetails.txtChequeNo.Visible Then
                If IsNumeric(CtrlChequeDetails.txtChequeNo.Text) = False Then
                    CtrlChequeDetails.txtChequeNo.Clear()
                    'CtrlChequeDetails.txtChequeNo.Focus()
                    ShowMessage(getValueByKey("ctrlchequedetails.txtchequenovalid"), getValueByKey("CLAE04"))
                End If
            End If

        End If
    End Sub

    ''' <summary>
    '''  Event of  dtpcheque
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dtpcheque_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If (e.KeyCode = Keys.Enter) AndAlso ctrlPayCheque.DateValid Then

            If CtrlPayCreditCheque.Visible = False Then
                InsertChequeDetails()
            Else
                CtrlPayCreditCheque.dtpDueDate.Focus()
            End If

        ElseIf (e.KeyCode = Keys.Tab) Then
            If CtrlPayCreditCheque.Visible = True Then
                CtrlPayCreditCheque.dtpDueDate.Focus()
            End If
        ElseIf (e.KeyCode = Keys.Enter) Then
            If CtrlPayCreditCheque.Visible = True Then
                CtrlPayCreditCheque.dtpDueDate.Focus()
            End If
        End If

    End Sub

    Private Sub HidePointsColumn()
        If dgGridReciept.Cols.Contains("Points") Then
            dgGridReciept.Cols("Points").Visible = False
        End If
    End Sub
    ''' <summary>
    '''  Event of ReceiptType  combobox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboRecieptType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ClearTextBox()
        rtxtSwipeCard.Enabled = False
        Select Case (SelectedReceiptTypeCode)
            Case AcceptPaymentTenderType.PositiveTenderType.Cash ', AcceptPaymentTenderType.NegativeTenderType.Cash ', AcceptPaymentTenderType.PositiveTenderType.ParkingCoupen
                'HidePointsColumn()
                ctrlPayCheque.Visible = False
                CtrlChequeDetails.Visible = False
                CtrlPayCreditCheque.Visible = False
                ctrlPayCredit.Visible = False
                lblSelectCurrency.Visible = True
                cboCurrency.Visible = True
                CtrlGiftVoucherIssue.Visible = False
                CtrlCLPPoint.Visible = False
                'grBoxTopMenu.Height = 54
            Case "Cheque"
                'HidePointsColumn()
                CtrlCLPPoint.Visible = False
                lblSelectCurrency.Visible = False
                cboCurrency.Visible = False
                ctrlPayCredit.Visible = False
                CtrlPayCreditCheque.Visible = False
                'lblchequeNo.Text = "Cheque No"
                'lblDate.Text = "Date"
                CtrlGiftVoucherIssue.Visible = False
                'ctrlPayCheque.Visible = True
                ctrlPayCheque.PaymentType = "Cheque"
                'If clsDefaultConfiguration.ChequeInfomation = False Then
                '    ctrlPayCheque.Visible = False
                'End If
                If TenderHeadCode = "CreditCheque" Then
                    ctrlPayCheque.Visible = True
                    CtrlPayCreditCheque.Visible = True
                    CtrlChequeDetails.Visible = False
                Else
                    CtrlChequeDetails.Visible = True
                    CLearChequeDetails()
                End If
                'grBoxTopMenu.Height = 24
            Case AcceptPaymentTenderType.PositiveTenderType.GiftVoucher
                'HidePointsColumn()
                CtrlCLPPoint.Visible = False
                lblSelectCurrency.Visible = False
                cboCurrency.Visible = False
                ctrlPayCredit.Visible = False
                'lblchequeNo.Text = "G.V No."
                'lblDate.Text = "Expiry Date"
                ctrlPayCheque.Visible = True
                CtrlChequeDetails.Visible = False
                CtrlPayCreditCheque.Visible = False
                CtrlGiftVoucherIssue.Visible = False
                ctrlPayCheque.PaymentType = "GiftVoucher"
                ctrlPayCheque.txtChequeNo.Select()
                'grBoxTopMenu.Height = 27
            Case AcceptPaymentTenderType.NegativeTenderType.CreditVoucherI
                'HidePointsColumn()
                CtrlCLPPoint.Visible = False
                ctrlPayCheque.Visible = False
                CtrlPayCreditCheque.Visible = False
                ctrlPayCredit.Visible = False
                lblSelectCurrency.Visible = True
                cboCurrency.Visible = True
                CtrlGiftVoucherIssue.Visible = False
                CtrlChequeDetails.Visible = False
                'grBoxTopMenu.Height = 54
            Case AcceptPaymentTenderType.PositiveTenderType.CreditVoucR
                'HidePointsColumn()
                CtrlChequeDetails.Visible = False
                CtrlCLPPoint.Visible = False
                lblSelectCurrency.Visible = False
                cboCurrency.Visible = False
                CtrlGiftVoucherIssue.Visible = False
                ctrlPayCredit.Visible = False
                'lblchequeNo.Text = "CreditNote No."
                'lblDate.Text = "Credit Date"
                ctrlPayCheque.Visible = True
                CtrlPayCreditCheque.Visible = False
                ctrlPayCheque.PaymentType = "CreditVouc"
                ctrlPayCheque.txtChequeNo.Select()
                'grBoxTopMenu.Height = 27
            Case AcceptPaymentTenderType.PositiveTenderType.CreditCard ', AcceptPaymentTenderType.PositiveTenderType.MASTERCARD, AcceptPaymentTenderType.PositiveTenderType.VISACARD
                'HidePointsColumn()
                CtrlCLPPoint.Visible = False
                CtrlChequeDetails.Visible = False
                lblSelectCurrency.Visible = False
                cboCurrency.Visible = False
                ctrlPayCheque.Visible = False
                CtrlPayCreditCheque.Visible = False
                CtrlGiftVoucherIssue.Visible = False
                If (IsSwipeCreditCard) Then
                    ReceiptTypeCreditCard_SwipeDisplaySetting()
                    _IsSwipeCreditCard = False
                Else
                    ReceiptTypeCreditCard_ManullyDisplaySetting()
                End If
            Case AcceptPaymentTenderType.NegativeTenderType.GiftVoucherI
                'HidePointsColumn()
                CtrlChequeDetails.Visible = False
                CtrlCLPPoint.Visible = False
                CtrlGiftVoucherIssue.Visible = True
                ctrlPayCheque.Visible = False
                CtrlPayCreditCheque.Visible = False
                ctrlPayCredit.Visible = False
                Dim dtGVDet As DataTable
                Dim dvGV As DataView
                Dim obj As New clsAdvanceSale()
                dtGVDet = obj.GetVoucherProg(clsAdmin.SiteCode, AcceptPaymentTenderType.NegativeTenderType.GiftVoucherI.ToString())
                If Not dtGVDet Is Nothing Then
                    dvGV = New DataView(dtGVDet, "ISPREPRINTED=False", "VOUCHERCODE", DataViewRowState.CurrentRows)
                    If Not dtGVDet Is Nothing Then
                        CtrlGiftVoucherIssue.cmbVoucherProgram.DataSource = dvGV
                        CtrlGiftVoucherIssue.cmbVoucherProgram.DisplayMember = "VOUCHERDESC"
                        CtrlGiftVoucherIssue.cmbVoucherProgram.ValueMember = "VOUCHERCODE"
                        pC1ComboSetDisplayMember(CtrlGiftVoucherIssue.cmbVoucherProgram)
                        CtrlGiftVoucherIssue.cmbVoucherProgram.SelectedIndex = 0
                    End If
                End If
            Case AcceptPaymentTenderType.PositiveTenderType.CLPPoint
                CtrlCLPPoint.Visible = True
                Dim totalPoints As Double
                If dgGridReciept.Cols.Contains("Points") Then
                    For Each Row As DataRow In dgGridReciept.DataSource.rows
                        If Row("RecieptTypeCode") = AcceptPaymentTenderType.PositiveTenderType.CLPPoint Then
                            totalPoints = totalPoints + Row("Points")
                        End If
                    Next
                End If
                CtrlCLPPoint.lblPointsValue.Text = totalBalancePoints - totalPoints
                CtrlCLPPoint.lblPointAmount.Text = (totalBalancePoints - totalPoints) * conversionRatio
                lblSelectCurrency.Visible = False
                cboCurrency.Visible = False
                ctrlPayCheque.Visible = False
                CtrlChequeDetails.Visible = False
                CtrlPayCreditCheque.Visible = False
                ctrlPayCredit.Visible = False
            Case Else
                'HidePointsColumn()
                CtrlCLPPoint.Visible = False
                lblSelectCurrency.Visible = False
                cboCurrency.Visible = False
                ctrlPayCheque.Visible = False
                CtrlChequeDetails.Visible = False
                CtrlPayCreditCheque.Visible = False
                ctrlPayCredit.Visible = False
                'grBoxTopMenu.Height = 27
        End Select
    End Sub

    Private Sub CLearChequeDetails()
        CtrlChequeDetails.txtChequeNo.Text = String.Empty
        CtrlChequeDetails.txtBankName.Text = String.Empty
        If CtrlChequeDetails.txtCustomerName.ReadOnly = False Then
            CtrlChequeDetails.txtCustomerName.Text = String.Empty
        End If
        CtrlChequeDetails.txtTelephoneNo.Text = String.Empty
        CtrlChequeDetails.dtChequeDate.Value = DateTime.Now
    End Sub

    ''' <summary>
    ''' Delete payment entry in grid 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        dgGridReciept.Redraw = False               ' Delete selected row(s).   
        Dim r As C1.Win.C1FlexGrid.Row
        Dim strSelectedValue As String
        For Each r In dgGridReciept.Rows.Selected  ' Remove the selected row(s) from the collection.            
            'Added by Rohit for CD-5938

            If r.Index >= 0 Then
                If dsRecieptType.Tables.Contains("CheckDtls") Then
                    For Each drrow As DataRow In dsRecieptType.Tables("CheckDtls").Select("PayLineNo = '" & dtRecieptType.Rows(r.Index - 1)("SrNo") & "' and CheckNo = '" & dtRecieptType.Rows(r.Index - 1)("Number") & "'")
                        drrow.Delete()
                        dsRecieptType.Tables("CheckDtls").AcceptChanges()
                    Next
                End If
            End If
            'Added by Rohit for CD-5938
            If dgGridReciept.Cols.Contains("Points") Then
                If Not IsDBNull(dgGridReciept.Rows(r.Index)) AndAlso Not dgGridReciept.Rows(r.Index) Is Nothing Then
                    CtrlCLPPoint.lblPointsValue.Text = CDbl(CheckIfBlank(CtrlCLPPoint.lblPointsValue.Text)) + CDbl(dgGridReciept.Rows(r.Index)("Points"))
                    CtrlCLPPoint.lblPointAmount.Text = CtrlCLPPoint.lblPointsValue.Text * conversionRatio
                End If
            End If
            dgGridReciept.Rows.Remove(r.Index)
            dtRecieptType.AcceptChanges()
        Next
        dgGridReciept.Redraw = True
        cboCurrency.SelectedValue = clsAdmin.CurrencyCode
        If (dtRecieptType.Rows.Count > 0) Then
            If IsTotalAmountReceived() = False Then
                If CDbl(lblbalanceDue.Text) > 0 Then
                    strSelectedValue = cboRecieptType.SelectedValue
                    filter = "Positive_Negative='+'"
                    dvPayment.RowFilter = filter
                    cboRecieptType_SelectedIndexChanged(cboRecieptType, New System.EventArgs)
                    If Not strSelectedValue = Nothing Then
                        cboRecieptType.SelectedValue = strSelectedValue
                    End If
                    cboRecieptType_Leave(sender, New System.EventArgs)
                Else
                    strSelectedValue = cboRecieptType.SelectedValue
                    filter = "Positive_Negative='*'"
                    dvPayment.RowFilter = filter
                    cboRecieptType_SelectedIndexChanged(cboRecieptType, New System.EventArgs)
                    If Not strSelectedValue = Nothing Then
                        cboRecieptType.SelectedValue = strSelectedValue
                    End If
                    cboRecieptType_Leave(sender, New System.EventArgs)
                End If
            End If

        Else
            If TotalBillAmount > 0 And IsTotalAmountReceived() = False Then
                strSelectedValue = cboRecieptType.SelectedValue
                filter = "Positive_Negative='+'"
                dvPayment.RowFilter = filter
                lblSign.Text = ""
                cboRecieptType_SelectedIndexChanged(cboRecieptType, New System.EventArgs)
                If Not strSelectedValue = Nothing Then
                    cboRecieptType.SelectedValue = strSelectedValue
                End If
            Else
                strSelectedValue = cboRecieptType.SelectedValue
                filter = "Positive_Negative='-'"
                If clsDefaultConfiguration.CashRefund = False Then
                    filter = filter & " And TenderType<>'Cash'"
                End If
                lblSign.Text = "-"
                dvPayment.RowFilter = filter
                cboRecieptType_SelectedIndexChanged(cboRecieptType, New System.EventArgs)
                If Not strSelectedValue = Nothing Then
                    cboRecieptType.SelectedValue = strSelectedValue
                End If
            End If
            lblbalanceDue.Text = TotalBillAmount
            lblTotalReciept.Text = "0"
            lblRetrunAmount.Text = "0"
            lblCalCurrencyBalanceDue.Text = lblCalCurrencyBillAmount.Text
            lblCalCurrencyTotalReciepts.Text = "0"
            IsRecieptAmountClose = False
        End If
    End Sub
    ''' <summary>
    '''  Insert payment details into grid 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("Insert payment details into grid")> _
    Private Sub btnapprove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnapprove.Click
        Select Case (SelectedReceiptTypeCode)
            Case AcceptPaymentTenderType.PositiveTenderType.CLPPoint
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.Cash  ', AcceptPaymentTenderType.NegativeTenderType.Cash ', AcceptPaymentTenderType.PositiveTenderType.ParkingCoupen
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.Cheque
                InsertChequeDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.GiftVoucher
                InsertGiftVoucherDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.CreditVoucR
                InsertDataIntoGrid_CreditNoteR()
            Case AcceptPaymentTenderType.NegativeTenderType.CreditVoucherI
                InsertDataIntoGrid_CreditNoteI()
                cboRecieptType_Leave(cboRecieptType, New EventArgs)
            Case AcceptPaymentTenderType.PositiveTenderType.CreditCard ', AcceptPaymentTenderType.PositiveTenderType.MASTERCARD, AcceptPaymentTenderType.PositiveTenderType.VISACARD
                InsertCreditCardDeatils()
            Case AcceptPaymentTenderType.NegativeTenderType.GiftVoucherI
                InsertGiftVoucherIssue()
            Case Else
                Exit Select
        End Select
    End Sub
    ''' <summary>
    ''' Checks when payment is done 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            ' for change mode form only
            blnChangeModeFormOk = True
            ' for change mode form only



            If IsPostiveTenderType Then
                If (PaymentType = clsAcceptPayment.PaymentType.Advance) Then

                    'Start- Rakesh Gautam -> comment if ...else Statement

                    'If (TotalReceivedAmount < lblCalCurrencyMiniBalDue.Text) Then
                    If (TotalReceivedAmount < TotalBillAmount) Then
                        If SelectedCurrencyDescription <> clsAdmin.CurrencyDescription Then
                            ShowMessage(String.Format(getValueByKey("ACP015"), SelectedCurrencySymbol, lblCalCurrencyMiniBalDue.Text), "ACP015 - " & getValueByKey("CLAE05"))
                        Else
                            ShowMessage(String.Format(getValueByKey("ACP015"), SelectedCurrencySymbol, lblCalMinBalDue.Text), "ACP015 - " & getValueByKey("CLAE05"))

                        End If

                    ElseIf (TotalReceivedAmount > TotalBillAmount) Then
                        ShowMessage(getValueByKey("ACP028"), "ACP028 - " & getValueByKey("CLAE04"))
                        Exit Sub
                    ElseIf _IsCashierPromoSelected = True AndAlso (String.IsNullOrEmpty(remarksTextbox.Text) Or HasAlphaNumericChar(remarksTextbox.Text) = False) Then
                        ShowMessage("Please Enter Remarks", "Warning")
                        Exit Sub
                    Else
                        'DebuteExceedAmount_AdvancePayment()
                        'Dim dialogResult As DialogResult = MessageBox.Show("Do you want to close ? ", "Spectrum", MessageBoxButtons.YesNo)
                        'If (dialogResult = Windows.Forms.DialogResult.Yes) Then
                        '    Me.Close()
                        'End If
                        Me.Close()
                    End If
                    'End- Rakesh Gautam -> comment if ...else Statement
                ElseIf (PaymentType = clsAcceptPayment.PaymentType.Accept Or PaymentType = clsAcceptPayment.PaymentType.EditBill) Then
                    If (TotalReceivedAmount < TotalBillAmount) Then
                        ShowMessage(String.Format(getValueByKey("ACP015"), SelectedCurrencySymbol, TotalBillAmount), "ACP015 - " & getValueByKey("CLAE05"))
                        Exit Sub
                    ElseIf TotalReceivedAmount > TotalBillAmount Then
                        ShowMessage(getValueByKey("ACP028"), "ACP028 - " & getValueByKey("CLAE04"))
                        Exit Sub
                    ElseIf _IsCashierPromoSelected = True AndAlso (String.IsNullOrEmpty(remarksTextbox.Text) Or HasAlphaNumericChar(remarksTextbox.Text) = False) Then
                        ShowMessage("Please Enter Remarks", "Warning")
                        Exit Sub
                    Else
                        CheckDataset_Changes()
                        Me.Close()
                    End If
                End If
            Else
                If _IsCashierPromoSelected = True AndAlso (String.IsNullOrEmpty(remarksTextbox.Text) Or HasAlphaNumericChar(remarksTextbox.Text) = False) Then
                    ShowMessage("Please Enter Remarks", "Warning")
                    Exit Sub
                End If
                If (IsRecieptAmountClose) Then
                    CheckDataset_Changes()
                    If _IsCashierPromoSelected = True AndAlso Not (String.IsNullOrEmpty(remarksTextbox.Text) Or HasAlphaNumericChar(remarksTextbox.Text) = False) Then
                        Me.Close()
                    ElseIf _IsCashierPromoSelected Then
                        ShowMessage("Please Enter Remarks", "Warning")
                        Exit Sub
                    Else
                        Me.Close()
                    End If
                Else
                    ShowMessage(String.Format(getValueByKey("ACP015"), SelectedCurrencySymbol, TotalBillAmount), "ACP015 - " & getValueByKey("CLAE05"))
                End If
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Checking dataset set changes only updated row will added into dataset 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CheckDataset_Changes() As Boolean
        Try
            Dim dvCheck As DataView
            Dim StrFilter As String = ""
            If Not (dsAcceptEditBillDataSet Is Nothing) Then
                If dsAcceptEditBillDataSet.Tables.Count > 0 And dtRecieptType.Rows.Count > 0 Then
                    For Each Row As DataRow In dsRecieptType.Tables(0).Rows
                        If Row.RowState <> DataRowState.Deleted Then
                            StrFilter = StrFilter & Row("SRNO").ToString() & ","
                        End If
                    Next
                    StrFilter = StrFilter.Substring(0, StrFilter.Length - 1)
                    dvCheck = New DataView(dsAcceptEditBillDataSet.Tables(0), "SRNO NOT IN (" & StrFilter & ")", "", DataViewRowState.CurrentRows)
                    If dvCheck.Count > 0 Then
                        dvCheck.AllowDelete = True
                        For Each drview As DataRowView In dvCheck
                            drview.Delete()
                        Next
                    End If
                    StrFilter = ""
                    For Each Row As DataRow In dsAcceptEditBillDataSet.Tables(0).Rows
                        If Row.RowState <> DataRowState.Deleted Then
                            StrFilter = StrFilter & Row("SRNO").ToString() & ","
                        End If
                    Next
                    If (StrFilter.Length > 0) Then
                        StrFilter = StrFilter.Substring(0, StrFilter.Length - 1)
                    End If
                    If (StrFilter.Length > 0) Then
                        dvCheck = New DataView(dsRecieptType.Tables(0), "SRNO  IN (" & StrFilter & ")", "", DataViewRowState.CurrentRows)
                        If dvCheck.Count > 0 Then
                            dvCheck.AllowDelete = True
                            For Each drview As DataRowView In dvCheck
                                drview.Delete()
                            Next
                        End If
                        dsRecieptType.AcceptChanges()
                    End If
                    For Each Row As DataRow In dsRecieptType.Tables(0).Rows
                        Row.AcceptChanges()
                        Row.SetAdded()
                    Next
                    dsAcceptEditBillDataSet.Tables(0).Merge(dsRecieptType.Tables(0), False, MissingSchemaAction.Ignore)

                    'Added by Rohit for Cr-5938
                    Dim tempdtCheckDtls As New DataTable
                    If dsRecieptType.Tables.Contains("CheckDtls") Then
                        tempdtCheckDtls = dsRecieptType.Tables("CheckDtls").Copy
                        tempdtCheckDtls.TableName = "CheckDtls"
                        tempdtCheckDtls.AcceptChanges()
                    End If
                    dsRecieptType = dsAcceptEditBillDataSet
                    If Not dsRecieptType.Tables.Contains("CheckDtls") Then
                        dsRecieptType.Tables.Add(tempdtCheckDtls)
                    Else
                        dsRecieptType.Tables("CheckDtls").Merge(tempdtCheckDtls)
                    End If
                    'DebuteExceedAmount()
                Else
                    dsAcceptEditBillDataSet = dsRecieptType
                    'DebuteExceedAmount()
                End If
            Else
                dsAcceptEditBillDataSet = dsRecieptType
                'DebuteExceedAmount()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    ''' <summary>
    ''' Functional key for cash
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnF5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnF5Cash.Click
        Try
            'Commented by rama on sept 15 as there is only cash tender type no cash(R)or cash(return) these are tender's
            ' If (IsPostiveTenderType) Then
            cboRecieptType.SelectedValue = AcceptPaymentTenderType.PositiveTenderType.Cash
            ' Else
            'cboRecieptType.SelectedValue = AcceptPaymentTenderType.NegativeTenderType.Cash
            'End If
            cboRecieptType_Leave(cboRecieptType, e)
            ctrlPayCash.txtCash.Focus()
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    ''' <summary>
    ''' handle functional key event on Form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmAcceptPayment_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If IsFunctinalKeyActivate = True Then
                Select Case (e.KeyCode)
                    Case Keys.F5
                        btnF5_Click(sender, e)
                        'If (IsPostiveTenderType) Then
                        '    cboRecieptType.SelectedValue = AcceptPaymentTenderType.PositiveTenderType.Cash
                        '    ctrlPayCash.txtCash.Focus()
                        '    'Else
                        '    '    cboRecieptType.SelectedValue = AcceptPaymentTenderType.NegativeTenderType.Cash
                        '    '    ctrlPayCash.txtCash.Focus()

                        'End If
                    Case Keys.F7
                        btnF3_Click(sender, e)
                        'If (IsPostiveTenderType) Then
                        '    btnF3_Click(sender, e)
                        '    'If EnableOrDisable(AcceptPaymentTenderType.PositiveTenderType.Cheque) Then
                        '    '    ctrlPayCash.txtCash.Focus()
                        '    'End If
                        'End If
                    Case Keys.F6
                        btnF6_Click(sender, e)
                        'If (IsPostiveTenderType) Then
                        '    btnF6_Click(sender, e)
                        '    'If EnableOrDisable(AcceptPaymentTenderType.PositiveTenderType.GiftVoucher) Then
                        '    '    ctrlPayCash.txtCash.Focus()
                        '    'End If
                        'End If
                    Case Keys.F4
                        btnF4CreditNote_Click(sender, e)
                        'If (IsPostiveTenderType) Then
                        '    If EnableOrDisable(AcceptPaymentTenderType.PositiveTenderType.CreditVoucR) Then
                        '        ctrlPayCash.txtCash.Focus()
                        '    End If
                        'Else
                        '    If EnableOrDisable(AcceptPaymentTenderType.NegativeTenderType.CreditVoucherI) Then
                        '        ctrlPayCash.txtCash.Focus()
                        '    End If
                        'End If
                    Case Keys.F10
                        btnSave_Click(sender, e)
                    Case Keys.F11
                        btnGift_Click(sender, e)
                End Select
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub FunctionKeyEnabled()
        Try
            If (IsPostiveTenderType) Then
                EnableOrDisable(AcceptPaymentTenderType.PositiveTenderType.Cash, btnF5Cash)
                EnableOrDisable("Cheque", btnF3)
                EnableOrDisable(AcceptPaymentTenderType.PositiveTenderType.CreditVoucR, btnF4CreditNote)
                'EnableOrDisable("CashRedifed", btnF9)
                EnableOrDisable("Food Coupons", btnF6)
                'EnableOrDisable(AcceptPaymentTenderType.PositiveTenderType.GiftVoucher, btnF6)
            Else
                'EnableOrDisable(AcceptPaymentTenderType.NegativeTenderType.Cash, btnF1Cash)
                EnableOrDisable("Cheque", btnF3)
                EnableOrDisable(AcceptPaymentTenderType.NegativeTenderType.CreditVoucherI, btnF4CreditNote)
                'EnableOrDisable("CashRedifed", btnF9)
                EnableOrDisable("Food Coupons", btnF6)
                'EnableOrDisable("GiftVoucher", btnF6)
            End If



        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    ''' <summary>
    ''' Find RecieptType inside ComboBox if available 
    ''' </summary>
    ''' <param name="strNameToFind">Name of Receipt find inside Combobox </param>
    ''' <param name="btnToEorD">Send button if you want enable or disable</param>
    ''' <returns>If find return True otherwise Flase</returns>
    ''' <remarks></remarks>
    Private Function EnableOrDisable(ByVal strNameToFind As String, Optional ByVal btnToEorD As Button = Nothing)
        Try
            Dim iIndex As Integer
            Dim dtview As DataView = cboRecieptType.DataSource
            Dim dt As DataTable = dtview.ToTable()
            For Each dr As DataRow In dt.Select("TenderType='" & strNameToFind & "'", "TenderHeadCode", DataViewRowState.CurrentRows)
                strNameToFind = dr("TenderHeadCode").ToString()
            Next
            cboRecieptType.SelectedValue = strNameToFind
            'iIndex = cboRecieptType.FindStringExact(strNameToFind)
            iIndex = cboRecieptType.SelectedIndex
            If iIndex < 0 Then
                If Not btnToEorD Is Nothing Then
                    btnToEorD.Enabled = False
                End If
                Return False
            Else
                If Not btnToEorD Is Nothing Then
                    btnToEorD.Enabled = True
                End If
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    ''' <summary>
    '''  Edit into grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cflexGridReciept_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
        Try
            If (dtRecieptType.Rows.Count > 0) Then
                IsTotalAmountReceived()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    ''' <summary>
    ''' ShortCut key for accepting payment by GiftVoucher
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub btnF6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnF6.Click
    '    Try
    '        If (IsPostiveTenderType) Then
    '            cboRecieptType.SelectedValue = AcceptPaymentTenderType.PositiveTenderType.GiftVoucher
    '        End If

    '        ctrlPayCash.txtCash.Focus()
    '    Catch ex As Exception

    '    End Try

    'End Sub
    ''' <summary>
    ''' ShortCut key for accepting payment by Cheque
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnF3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnF3.Click
        Try
            If (IsPostiveTenderType) Then
                If EnableOrDisable(AcceptPaymentTenderType.PositiveTenderType.Cheque) Then
                    cboRecieptType_Leave(cboRecieptType, e)
                    ctrlPayCash.txtCash.Focus()
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    ''' <summary>
    ''' Selection changed from currency combobox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub cboCurrency_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCurrency.SelChange
    '    If Not (SelectedCurrencyIndex = clsAdmin.CurrencyCode) Then
    '        DisplaySettings_CurrencyCalculation()
    '        CalculateAmountInCurrency()
    '    Else
    '        pnlBoxCurrency.Visible = False
    '    End If
    'End Sub
    ''' <summary>
    ''' ShortCut key for accepting payment by CreditNote
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnF4CreditNote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnF4CreditNote.Click
        'cboRecieptType.SelectedIndex =  
        Try
            If EnableOrDisable(AcceptPaymentTenderType.PositiveTenderType.CreditCard) Then
                cboRecieptType.Focus()
                cboRecieptType_Leave(cboRecieptType, e)
                ctrlPayCash.txtCash.Focus()
                ctrlPayCheque.txtChequeNo.Focus()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    ''' <summary>
    ''' You cant allowed to change Credit Voucher row in grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cflexGridReciept_BeforeEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
        Try
            Dim r As C1.Win.C1FlexGrid.Row
            For Each r In dgGridReciept.Rows.Selected
                Dim strRecieptType As String = dgGridReciept.Rows(r.Index)(0).ToString()
                If (strRecieptType = "CreditVoucher") Then
                    ShowMessage(getValueByKey("ACP016"), "ACP016 - " & getValueByKey("CLAE04"))
                End If
            Next
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    ''' <summary>
    ''' Cancel payment transactions  and close the form.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            If Not (PaymentType = clsAcceptPayment.PaymentType.EditBill) Then
                dsRecieptType.Clear()
                dtRecieptType.Clear()
            End If
            Me.Close()
            _IsCancelAcceptPayment = True
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    Dim isSelectedValueChanged As Boolean
    Private Sub cboRecieptType_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRecieptType.SelectedValueChanged
        cboRecieptType_Leave(sender, New EventArgs())
        'isSelectedValueChanged = True
    End Sub
    Private Sub cboRecieptType_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRecieptType.Leave
        Try
            'If Not isSelectedValueChanged Then
            Dim value As String = cboRecieptType.SelectedValue
            For Each dr As DataRow In dtPayment.Select("TenderHeadCode='" & value & "'", "", DataViewRowState.CurrentRows)
                _tenderHeadName = dr("TenderHeadName").ToString()
                _tenderType = dr("TenderType").ToString()
                _tenderHeadCode = dr("TenderHeadCode").ToString()
            Next
            If Not cboRecieptType.SelectedValue Is Nothing Then
                cboRecieptType_SelectedIndexChanged(sender, New System.EventArgs)
            End If
            If IsPostiveTenderType = False Then
                cboCurrency.Visible = False
                lblSelectCurrency.Visible = False
            End If
            If cboCurrency.SelectedValue = clsAdmin.CurrencyCode Then
                ctrlPayCash.txtCash.Text = lblbalanceDue.Text
                ctrlPayCash.txtCash.Value = lblbalanceDue.Text
            Else
                ctrlPayCash.txtCash.Text = lblCalCurrencyBalanceDue.Text
                ctrlPayCash.txtCash.Value = lblCalCurrencyBalanceDue.Text
            End If


            'ctrlPayCash.txtCash.Value = IIf(TotalBillAmount < 0, CDbl(lblbalanceDue.Text) * -1, CDbl(lblbalanceDue.Text))
            If IsDBNull(ctrlPayCash.txtCash.Value) Then
                ctrlPayCash.txtCash.Value = 0
            End If
            lblSign.Text = IIf(CDbl(ctrlPayCash.txtCash.Value) < 0, "-", "")
            If IsRecieptAmountClose = False Then
                ctrlPayCash.txtCash.Value = IIf(CDbl(ctrlPayCash.txtCash.Value) < 0, CDbl(ctrlPayCash.txtCash.Value) * -1, CDbl(ctrlPayCash.txtCash.Value))
            Else
                ctrlPayCash.txtCash.Value = 0
            End If
            ''Else
            'isSelectedValueChanged = False
            'End If        
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    'Public Function IsAmountReturnToCustomer() As Boolean
    '    Try
    '        Dim dvCheck As DataView
    '        'Commented by rama on sept 15 as there is only cash tender type no cash(R)or cash(return) these are tender's
    '        'dvCheck = New DataView(dtRecieptType, "RecieptType='" + AcceptPaymentTenderType.PositiveTenderType.Cash + "' or RecieptType='" + AcceptPaymentTenderType.NegativeTenderType.Cash + "'", "", DataViewRowState.CurrentRows)
    '        dvCheck = New DataView(dtRecieptType, "RecieptTypeCode='" + AcceptPaymentTenderType.PositiveTenderType.Cash + "'", "", DataViewRowState.CurrentRows)
    '        If dvCheck.Count > 0 Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '        dtRecieptType.AcceptChanges()
    '    Catch ex As Exception
    '        LogException(ex)
    '    End Try
    'End Function
    'Private Function DebuteExceedAmount() As Boolean
    '    Try
    '        If (IsAmountReturnToCustomer()) Then
    '            Dim dvCheck As DataView
    '            Dim decReturnAmount As Decimal = Decimal.Subtract(0, _ReturnToCustomer)
    '            dvCheck = New DataView(dsAcceptEditBillDataSet.Tables(0), "RecieptType='" + AcceptPaymentTenderType.PositiveTenderType.Cash + "' or RecieptType='" + AcceptPaymentTenderType.NegativeTenderType.Cash + "' and AmountInCurrency > '" & decReturnAmount & "'", "", DataViewRowState.CurrentRows)
    '            If dvCheck.Count > 0 Then
    '                If IsPostiveTenderType Then
    '                    For Each drview As DataRowView In dvCheck
    '                        drview("Amount") = Decimal.Add(drview("Amount"), _ReturnToCustomer)
    '                        If Not drview("AmountInCurrency") Is DBNull.Value Then drview("AmountInCurrency") = Decimal.Add(drview("AmountInCurrency"), _ReturnToCustomer)
    '                        drview("Number") = CurrencyFormat(drview("Amount"))
    '                        Exit For
    '                    Next
    '                Else
    '                    For Each drview As DataRowView In dvCheck
    '                        drview("Amount") = Decimal.Subtract(drview("Amount"), _ReturnToCustomer)
    '                        If Not drview("AmountInCurrency") Is DBNull.Value Then drview("AmountInCurrency") = Decimal.Subtract(drview("AmountInCurrency"), _ReturnToCustomer)
    '                        drview("Number") = CurrencyFormat(drview("Amount"))
    '                        Exit For
    '                    Next
    '                End If

    '            End If
    '            'dsRecieptType.AcceptChanges()
    '        End If
    '    Catch ex As Exception
    '        LogException(ex)
    '    End Try
    'End Function
    'Private Function DebuteExceedAmount_AdvancePayment() As Boolean
    '    Try
    '        If (IsAmountReturnToCustomer()) Then
    '            Dim dvCheck As DataView
    '            Dim decReturnAmount As Decimal = Decimal.Subtract(0, _ReturnToCustomer)
    '            dvCheck = New DataView(dtRecieptType, "RecieptType='" + AcceptPaymentTenderType.PositiveTenderType.Cash + "' and AmountInCurrency > '" & decReturnAmount & "'", "", DataViewRowState.CurrentRows)
    '            If dvCheck.Count > 0 Then
    '                For Each drview As DataRowView In dvCheck
    '                    drview("Amount") = Decimal.Add(drview("Amount"), _ReturnToCustomer)
    '                    drview("AmountInCurrency") = Decimal.Add(drview("AmountInCurrency"), _ReturnToCustomer)
    '                    drview("Number") = CurrencyFormat(drview("Amount"))
    '                    Exit For
    '                Next
    '            End If
    '            'dsRecieptType.AcceptChanges()
    '        End If
    '    Catch ex As Exception
    '        LogException(ex)
    '    End Try
    'End Function
    'Private Sub cboRecieptType_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRecieptType.SelectedValueChanged
    '    Try
    '        Dim value As String = cboRecieptType.SelectedValue
    '        For Each dr As DataRow In dtPayment.Select("TenderHeadCode='" & value & "'", "", DataViewRowState.CurrentRows)
    '            _tenderHeadName = dr("TenderHeadName").ToString()
    '            _tenderType = dr("TenderType").ToString()
    '            _tenderHeadCode = dr("TenderHeadCode").ToString()
    '        Next
    '        If Not cboRecieptType.SelectedValue Is Nothing Then
    '            cboRecieptType_SelectedIndexChanged(sender, New System.EventArgs)
    '        End If
    '        If IsPostiveTenderType = False Then
    '            cboCurrency.Visible = False
    '            lblSelectCurrency.Visible = False
    '        End If
    '        ctrlPayCash.txtCash.Value = lblbalanceDue.Text

    '        'ctrlPayCash.txtCash.Value = IIf(TotalBillAmount < 0, CDbl(lblbalanceDue.Text) * -1, CDbl(lblbalanceDue.Text))
    '        lblSign.Text = IIf(CDbl(ctrlPayCash.txtCash.Value) < 0, "-", "")
    '        ctrlPayCash.txtCash.Value = IIf(CDbl(ctrlPayCash.txtCash.Value) < 0, CDbl(ctrlPayCash.txtCash.Value) * -1, CDbl(ctrlPayCash.txtCash.Value))
    '    Catch ex As Exception
    '        ShowMessage(ex.Message, "Error")
    '        LogException(ex)
    '    End Try

    'End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        _Actiontype = "Save"
        btnOK_Click(sender, e)
        ReturnMsg()
        Try
            Dim cA4Print As New clsA4Print
            cA4Print.OperateDevice("CashDrawer")

        Catch ex As Exception

        End Try

    End Sub
    Private Sub btnGift_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGift.Click
        Try
            GiftReceiptMessage = GetGiftMessage()
            If GiftReceiptMessage = String.Empty Then Exit Sub
            _Actiontype = "Gift"
            btnOK_Click(sender, e)
            Try
                Dim cA4Print As New clsA4Print
                cA4Print.OperateDevice("CashDrawer")

            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try

    End Sub

    Private Sub cboCurrency_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCurrency.Leave
        Try
            'ctrlPayCash.txtCash.Value = lblbalanceDue.Text
            'lblSign.Text = IIf(CDbl(ctrlPayCash.txtCash.Value) < 0, "-", "")
            'ctrlPayCash.txtCash.Value = IIf(CDbl(ctrlPayCash.txtCash.Value) < 0, CDbl(ctrlPayCash.txtCash.Value) * -1, CDbl(ctrlPayCash.txtCash.Value))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboCurrency_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCurrency.SelectedValueChanged
        Try
            Dim value As String = cboCurrency.SelectedValue
            Dim dt As DataTable = cboCurrency.DataSource
            For Each dr As DataRow In dt.Select("currencyCode='" & value & "'", "", DataViewRowState.CurrentRows)
                _currencyDesc = dr("CurrencyDescription").ToString()
                _currencySymbol = dr("CurrencySymbol").ToString()
            Next
            If Not (SelectedCurrencyIndex = clsAdmin.CurrencyCode) Then
                DisplaySettings_CurrencyCalculation()
                If dtRecieptType.Rows.Count > 0 Then
                    CalculateAmountInCurrency(True)
                Else
                    CalculateAmountInCurrency(False)
                End If
                ' CalculateAmountInCurrency(False)
                lblSign.Text = IIf(CDbl(ctrlPayCash.txtCash.Value) < 0, "-", "")
                ctrlPayCash.txtCash.Value = IIf(CDbl(ctrlPayCash.txtCash.Value) < 0, CDbl(ctrlPayCash.txtCash.Value) * -1, CDbl(ctrlPayCash.txtCash.Value))
            Else

                ctrlPayCash.txtCash.Value = IIf(TotalBillAmount < 0, CDbl(lblbalanceDue.Text) * -1, CDbl(lblbalanceDue.Text))
                pnlBoxCurrency.Visible = False
                'lblSign.Text = IIf(TotalBillAmount < 0, "-", "")
                'ctrlPayCash.txtCash.Value = CDbl(lblbalanceDue.Text)
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
#End Region


    Private Sub btnF6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnF6.Click
        Try
            If (IsPostiveTenderType) Then
                If EnableOrDisable(AcceptPaymentTenderType.PositiveTenderType.GiftVoucher) Then
                    cboRecieptType.Focus()
                    cboRecieptType_Leave(cboRecieptType, e)
                    ctrlPayCash.txtCash.Focus()
                    ctrlPayCheque.txtChequeNo.Focus()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReturnMsg()
        'MsgBox("Return Amount " & intReturnCashToCust)

        If intReturnCashToCust > 0 Then
            ShowMessage(True, String.Format(getValueByKey("ACP014"), SelectedCurrencySymbol, FormatNumber(intReturnCashToCust, 2)), "ACP014 - " & getValueByKey("CLAE04"))
        End If

    End Sub

    'Protected Overrides Function ProcessKeyPreview(ByRef m As System.Windows.Forms.Message) As Boolean
    '    Const WM_KEYDOWN As Integer = &H100
    '    If m.Msg = 260 Then
    '        Select Case m.WParam.ToInt32
    '            Case 18
    '                If My.Computer.Keyboard.AltKeyDown Then
    '                    Me.Select()
    '                End If
    '        End Select
    '    End If
    '    Return MyBase.ProcessKeyPreview(m)
    'End Function

    Private Sub txtamount_lostfocus(ByVal sender As Object, ByVal e As EventArgs)
        If cboRecieptType.SelectedIndex > 0 Then
            Select Case (SelectedReceiptTypeCode)
                Case "Cheque"
                    Me.ctrlPayCheque.txtChequeNo.Focus()
                    Me.CtrlChequeDetails.txtChequeNo.Focus()
                Case AcceptPaymentTenderType.PositiveTenderType.GiftVoucher
                    Me.ctrlPayCheque.ctrlGiftVouc.Focus()
                Case AcceptPaymentTenderType.NegativeTenderType.CreditVoucherI
                    Me.ctrlPayCredit.txtCreditCardNo.Focus()
                Case AcceptPaymentTenderType.PositiveTenderType.CreditVoucR
                    Me.ctrlPayCredit.txtCreditCardNo.Focus()
                Case AcceptPaymentTenderType.PositiveTenderType.CreditCard ', AcceptPaymentTenderType.PositiveTenderType.MASTERCARD, AcceptPaymentTenderType.PositiveTenderType.VISACARD
                    Me.ctrlPayCredit.txtCreditCardNo.Focus()
            End Select

        End If


        'Throw New NotImplementedException
    End Sub

    Private Sub frmQSRPayment_Move(sender As Object, e As System.EventArgs) Handles Me.Move
        Me.Location = New Point(Screen.PrimaryScreen.Bounds.Width - Me.Width, 50)
    End Sub
End Class