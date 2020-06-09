'Accept Payment 
'Created by :Rahul Katkar
'Created Date : 22 Jan 2009
'Updated by:----------
'Updated Date:--------

Imports System.Data
Imports System.Data.SqlClient
Imports C1.Win.C1FlexGrid
Imports SpectrumBL
Imports SpectrumBL.clsAcceptPayment
Imports SpectrumPrint

'Imports System.Threading
Public Class frmNAcceptPaymentPC
    Dim ObjComm As New clsCommon
    Public _Addresstype As String
    Public Property Addresstype() As String
        Get
            Return _Addresstype
        End Get
        Set(value As String)
            _Addresstype = value
        End Set
    End Property
    Public _IsCreditSale As Boolean = False 'vipin
    Public Property IsCreditSale() As Boolean
        Get
            Return _IsCreditSale
        End Get
        Set(value As Boolean)
            _IsCreditSale = value
        End Set
    End Property
    Public _custType As String
    Public Property custType() As String
        Get
            Return _custType
        End Get
        Set(value As String)
            _custType = value
        End Set
    End Property
    Public _dtCust As DataTable
    Public Property dtCust() As DataTable
        Get
            Return _dtCust
        End Get
        Set(value As DataTable)
            _dtCust = value
        End Set
    End Property
    Public _isChangeTenderMode As Boolean = False
    Public Property isChangeTenderMode() As Boolean
        Get
            Return _isChangeTenderMode
        End Get
        Set(value As Boolean)
            _isChangeTenderMode = value
        End Set
    End Property
    Public Shared _paymentTermNameId As String
    Public Property PaymentTermNameId() As String
        Get
            Return _paymentTermNameId
        End Get
        Set(value As String)
            _paymentTermNameId = value
        End Set
    End Property
    Public Shared _DtCheckDetail As DataSet
    Public Property DtCheckDetail() As DataSet
        Get
            Return _DtCheckDetail
        End Get
        Set(value As DataSet)
            _DtCheckDetail = value
        End Set
    End Property
    Private Property _altWasPressed As Boolean = False
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
    Private _CreditCardNumber As String
    Public Property CreditCardNumber() As String
        Get
            Return _CreditCardNumber
        End Get
        Set(ByVal value As String)
            _CreditCardNumber = value
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
    'added by khusrao adil on 25-07-2018
    Private _allowInnovitiPayment As Boolean = False
    Public Property AllowInnovitiPayment() As Boolean
        Get
            Return _allowInnovitiPayment
        End Get
        Set(ByVal value As Boolean)
            _allowInnovitiPayment = value
        End Set
    End Property
    Private _allowInnovitiWithOtherTender As Boolean = False
    Public Property AllowInnovitiWithOtherTender() As Boolean
        Get
            Return _allowInnovitiWithOtherTender
        End Get
        Set(ByVal value As Boolean)
            _allowInnovitiWithOtherTender = value
        End Set
    End Property
#Region "Page Variables"
    Protected dsRecieptType As New DataSet()
    Protected dtRecieptType As New DataTable()
    Dim RemoveAdddtPayment As New DataTable
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
    Dim defaultRefundTender As String = String.Empty

    Private _IsFastCashMemo As Boolean = False
    Public Property IsFastCashMemo() As Boolean
        Get
            Return _IsFastCashMemo
        End Get
        Set(ByVal value As Boolean)
            _IsFastCashMemo = value
        End Set
    End Property

    Private _IsTenderChange As Boolean = False
    Public Property IsTenderChange() As Boolean
        Get
            Return _IsTenderChange
        End Get
        Set(ByVal value As Boolean)
            _IsTenderChange = value
        End Set
    End Property


    ''' <summary>
    ''' This Variable is created to store Previous Select Index of CMB
    ''' </summary>
    ''' <remarks></remarks>
    Private _cboreceiptPreIndex As Integer = 0

    Private _CreditSettlement As Boolean
    Public Property CreditSettlement() As Boolean
        Get
            Return _CreditSettlement
        End Get
        Set(ByVal value As Boolean)
            _CreditSettlement = value
        End Set
    End Property

    Private _CreditSaleLimitSOPickUpAmt As Double
    Public Property CreditSaleLimitSOPickUpAmt() As Double
        Get
            Return _CreditSaleLimitSOPickUpAmt
        End Get
        Set(ByVal value As Double)
            _CreditSaleLimitSOPickUpAmt = value
        End Set
    End Property

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

    Private _AvoidCreditSalesTender As Boolean
    Public Property AvoidCreditSalesTender() As Boolean
        Get
            Return _AvoidCreditSalesTender
        End Get
        Set(ByVal value As Boolean)
            _AvoidCreditSalesTender = value
        End Set
    End Property

    Private _decTotalCustomerPadiAmount As Decimal
    Public Property TotalCustomerPadiAmount() As Decimal
        Get
            Return _decTotalCustomerPadiAmount
        End Get
        Set(ByVal value As Decimal)
            _decTotalCustomerPadiAmount = value
        End Set
    End Property
    Public _IsGiftVoucherIssued As Dictionary(Of String, String)
    Public Property IsGiftVoucherIssued() As Dictionary(Of String, String)
        Get
            Return _IsGiftVoucherIssued
        End Get
        Set(ByVal value As Dictionary(Of String, String))
            _IsGiftVoucherIssued = value
        End Set
    End Property

    Private _TotalPick As Integer
    Public Property TotalPick() As Integer
        Get
            Return _TotalPick
        End Get
        Set(ByVal value As Integer)
            _TotalPick = value
        End Set
    End Property
    Private _IsChangeTender As Boolean = False
    Public Property IsChangeTender() As Boolean
        Get
            Return _IsChangeTender
        End Get
        Set(ByVal value As Boolean)
            _IsChangeTender = value
        End Set
    End Property

    Private _MobNumber As String
    Public Property MobNumber() As String
        Get
            Return _MobNumber
        End Get
        Set(ByVal value As String)
            _MobNumber = value
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
    Private _CompName As String
    Public Property CompName() As String
        Get
            Return _CompName
        End Get
        Set(ByVal value As String)
            _CompName = value
        End Set
    End Property

#End Region
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(Optional ByVal isFastCashMemo As Boolean = False)
        Init(isFastCashMemo)
    End Sub

    Public Sub New(ByVal dtitm As DataTable, Optional ByVal isFastCashMemo As Boolean = False)
        Init(isFastCashMemo)
        dtitems = dtitm
    End Sub

    Private Sub Init(ByVal IsCallFastCashMemo As Boolean)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Dim objdefaultCM As New clsDefaultConfiguration("CMS")
        objdefaultCM.GetDefaultSettings()
        If (IsCallFastCashMemo) Then
            Me.ControlBox = True
            IsFastCashMemo = True
            Me.WindowState = FormWindowState.Normal
            Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog

            'Dim winSize As New Size(870, 610)  '''760, 560
            Dim winSize As New Size(760, 560)
            Me.MinimumSize = winSize
            Me.Size = winSize
            Me.Location = New Point(Screen.PrimaryScreen.Bounds.Width - Me.Width, 0)

            '     Dim comboSize As New Size(120, 22)
            Dim comboSize As New Size(100, 22)
            Me.cboRecieptType.MinimumSize = comboSize
            Me.cboRecieptType.Size = comboSize

            Me.cboCurrency.MinimumSize = comboSize
            Me.cboCurrency.Size = comboSize

            Me.rtxtSwipeCard.Size = New System.Drawing.Size(836, 25)

            sizboxBottom.Grid.Columns(6).IsFixedSize = False
            sizboxBottom.Grid.Columns(6).Size = 3

            sizboxBottom.Grid.Columns(7).IsFixedSize = False
            sizboxBottom.Grid.Columns(7).Size = 3

            Dim GridSize As New Size(755, 555)
            Me.dgGridReciept.Size = GridSize
        End If

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
        btnF4CreditNote.Text = "F4" & vbCrLf & getValueByKey("frmnacceptpayment.btnf4creditnote")
        btnF6.Text = "F6" & vbCrLf & getValueByKey("frmnacceptpayment.btnf6")
        btnSave.Text = "F10" & vbCrLf & getValueByKey("frmnacceptpayment.btnsave")
        btnGift.Text = "F11" & vbCrLf & getValueByKey("frmnacceptpayment.btngift")
        ' change to remove the problem of \n in resource file.

        ' Add any initialization after the InitializeComponent() call.

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
            _totalAmount = MyRound(value, clsDefaultConfiguration.BillRoundOffAt)
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

    Private _dtitems As DataTable
    Public Property dtitems() As DataTable
        Get
            Return _dtitems
        End Get
        Set(ByVal value As DataTable)
            _dtitems = value
        End Set
    End Property

#End Region

#Region "Methods"
    ''' <summary>
    ''' Check entered amount is null 
    ''' </summary>
    ''' <param name="strErrorMsg">Return error message</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Function CheckAmountTextBox_NullOrInteger(ByRef strErrorMsg As String) As Boolean
        'Call CheckCreditSale()
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
    Private Sub frmAcceptPaymentPC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'added by khusrao adil on 26-07-2018
            AllowInnovitiPayment = False
            AllowInnovitiWithOtherTender = False
            objBLLAcceptPayment.IsNewSalesOrder = clsDefaultConfiguration.IsNewSalesOrder
            lblCustomer.Text = ""
            lblCompanyName.Text = ""
            lblContactNo.Text = ""

            BtnCustSearch.Visible = True 'vipin
            If _IsCreditSale = True Then 'vipin
                BtnCustSearch.Visible = False
            End If

            BtnCustSearch.Visible = True 'vipin
            'Dim decCurrenctCurrencyAgainst As Decimal
            'txtBillAmount.Text = objBLLAcceptPayment.CalculateTotalBillAmount_InCurrency(TotalBillAmount, clsAdmin.CurrencyCode, decCurrenctCurrencyAgainst, clsAdmin.CurrencyCode)                       
            If _CardNo = String.Empty Then
                For i As Int32 = dvPayment.Count - 1 To 0 Step -1
                    If dvPayment(i)("TenderType") = "CLPPoint" Then
                        dvPayment(i).Delete()
                    End If
                Next
            End If

            If (AvoidCreditSalesTender) Then
                For i As Int32 = dvPayment.Count - 1 To 0 Step -1
                    If dvPayment(i)("TenderType") = "Credit" Then
                        dvPayment(i).Delete()
                    End If
                Next
            End If

            If CustName <> String.Empty Then
                lblCustomer.Text = CustName
            End If
            If CompName <> String.Empty Then
                lblCompanyName.Text = CompName
            Else
                lblCompanyName.Text = "" 'vipin
            End If
            If MobNumber <> String.Empty Then
                lblContactNo.Text = MobNumber
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
                filter = "Positive_Negative='*' OR Positive_Negative='-'"
                ''Changed by rama Ranjan on 26-may-2009 for SystemConfiguration check
                If clsDefaultConfiguration.CashRefund = False Then
                    filter = filter & " And TenderType<>'Cash'"
                End If
                ''
                If CreditSettlement Then
                    filter = filter & " and TenderType<>'Credit' "
                End If

                dvPayment.RowFilter = filter
                cboRecieptType_SelectedIndexChanged(sender, e)
                ctrlPayCash.txtCash.Text = TotalBillAmount
                IsPostiveTenderType = False
                'lblTotalReciepts.Text = "Total Issue :"
                lblTotalReciepts.Text = getValueByKey("frmnacceptpayment.lbltotalreciepts1")
                cboCurrency.Visible = False

                If (Val(TotalBillAmount) < 0) Then
                    ctrlPayCash.txtCash.Value = Val(lblbalanceDue.Text) * -1
                End If

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
                    If CreditSettlement Then
                        filter = filter & " and TenderType<>'Credit'"
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
            'added by khusrao adil on 26-07-2018
            Dim clsCommon As New SpectrumBL.clsCommon()
            dtInnoviti = clsCommon.GetInnovitiStruc()
            dtInnoviti.Rows.Clear()
            If _ParentForm = "CashMemo" Then
                BtnOk.Visible = False
                btnCancel.Visible = True
                lblMinimumBalanceAmount.Visible = False
                lblCalMinBalDue.Visible = False
                lblCurrencyMinimumBalAmt.Visible = False
                lblCalCurrencyMiniBalDue.Visible = False
            ElseIf _ParentForm = "SalesOrder" Then
                lblCurrencyMinimumBalAmt.Visible = False
                lblCalCurrencyMiniBalDue.Visible = False
                btnGift.Visible = False
            ElseIf _ParentForm = "BirthList" Then
                btnGift.Visible = False
            End If

            'AddHandler CtrlChequeDetails.txtTelephoneNo.KeyDown, AddressOf txtTelephoneNo_PreviewKeyDown
            'AddHandler CtrlChequeDetails.txtBankName.KeyDown, AddressOf txtTelephoneNo_PreviewKeyDown
            AddHandler CtrlChequeDetails.txtChequeNo.KeyDown, AddressOf txtTelephoneNo_PreviewKeyDown
            AddHandler CtrlChequeDetails.txtChequeNo.LostFocus, AddressOf txtChequeNo_LostFocus
            AddHandler CtrlChequeDetails.txtMicrNumber.KeyDown, AddressOf txtTelephoneNo_PreviewKeyDown
            AddHandler CtrlChequeDetails.dtChequeDate.KeyDown, AddressOf txtTelephoneNo_PreviewKeyDown
            AddHandler CtrlPayCreditCheque.txtRemarks.KeyDown, AddressOf txtRemarks_PreviewKeyDown
            AddHandler ctrlPayCash.txtCash.KeyDown, AddressOf txtamount_PreviewKeyDown
            AddHandler ctrlPayCash.txtCash.Leave, AddressOf txtamount_lostfocus
            '  AddHandler ctrlPayCredit.txtAuthCode.KeyDown, AddressOf txtCCAuthCode_PreviewKeyDown
            AddHandler ctrlPayCredit.txtCreditCardNo.KeyDown, AddressOf txtCCAuthCode_PreviewKeyDown
            '  AddHandler ctrlPayCredit.txtSlipNO.KeyDown, AddressOf txtCCAuthCode_PreviewKeyDown
            AddHandler ctrlPayCredit.dtpExpiryDate.KeyDown, AddressOf txtCCAuthCode_PreviewKeyDown
            AddHandler ctrlPayCheque.ctrlGiftVouc.KeyDown, AddressOf dtpcheque_PreviewKeyDown
            AddHandler ctrlPayCheque.cmbVoucherProgram.KeyDown, AddressOf dtpcheque_PreviewKeyDown
            AddHandler CtrlGiftVoucherIssue.cmbVoucherProgram.KeyDown, AddressOf txtamount_PreviewKeyDown
            AddHandler ctrlPayCheque.txtChequeNo.KeyDown, AddressOf dtpcheque_PreviewKeyDown



            '' added by ketan
            AddHandler CtrlChequeDetails.txtChequeNo.KeyPress, AddressOf txtChequeNo_KeyPress
            AddHandler CtrlChequeDetails.txtMicrNumber.KeyPress, AddressOf txtMicrNumber_KeyPress

            '   CtrlChequeDetails.txtChequeNo.MaxLength = 20
            CtrlChequeDetails.txtChequeNo.MaxLength = 6                    'vipin 21-06-2017

            Me.CtrlChequeDetails.txtMicrNumber.MaxLength = 9     'vipin
            Me.CtrlChequeDetails.txtMicrNumber.DataType = GetType(String)
            Me.CtrlChequeDetails.txtChequeNo.MaxLength = 6     'vipin
            Me.CtrlChequeDetails.txtChequeNo.DataType = GetType(String)

            Check_PaymentTypeForScreen()

            Me.TopMost = False
            ctrlPayCash.txtCash.Select()
            cboRecieptType_Leave(cboRecieptType, e)
            ctrlPayCash.txtCash.Value = TotalBillAmount
            lblbalanceDue.Text = TotalBillAmount

            If (TotalBillAmount > 0) Then
                btnF5_Click(sender, e)
            Else
                If (clsDefaultConfiguration.CashRefund) Then
                    defaultRefundTender = AcceptPaymentTenderType.NegativeTenderType.CashR
                Else
                    defaultRefundTender = AcceptPaymentTenderType.NegativeTenderType.CreditVoucherI
                End If

                _tenderType = defaultRefundTender
                cboRecieptType.SelectedValue = defaultRefundTender
                cboRecieptType_SelectedIndexChanged(cboRecieptType, New System.EventArgs)
            End If

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
            'If clsDefaultConfiguration.IsCustNameReadonly AndAlso Not String.IsNullOrEmpty(CLPCustomerName) Then
            '    CtrlChequeDetails.txtMicrNumber.TextDetached = True
            '    CtrlChequeDetails.txtMicrNumber.Text = CLPCustomerName
            '    CtrlChequeDetails.txtMicrNumber.ReadOnly = True
            'End If

            If (dgGridReciept.DataSource Is Nothing) Then

                dgGridReciept.Cols(0).Caption = "Tender Type"
                dgGridReciept.Cols(1).Caption = "Amount"
                dgGridReciept.Cols(3).Caption = "Bank Name"
                dgGridReciept.Cols(4).Caption = "Card No."
                dgGridReciept.Cols(5).Caption = "Cheque No."
                dgGridReciept.Cols(6).Caption = "MICR No."
                dgGridReciept.Cols(7).Caption = "Cheque Date"
                dgGridReciept.Cols(8).Caption = "Neft Ref no."
                dgGridReciept.Cols(9).Caption = "Rtgs Ref no."
                dgGridReciept.Cols(10).Caption = "Credit Voucher no."
                dgGridReciept.Cols(10).Width = 145 'vipin 01122017
                dgGridReciept.Cols(11).Caption = "Remarks"



            Else
                Grid_DisplaySetting()
            End If

            Call SetTabSequence()
            ''add data in Payment Term Drop down List added by ketan 
            If clsDefaultConfiguration.IsSavoy Then
                Call GetCreditPaymentTerms()
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            BtnCustSearch.Visible = True 'vipin
            If _IsCreditSale = True Then 'vipin
                BtnCustSearch.Visible = False
            End If
            Me.KeyPreview = True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

        rtxtSwipeCard.Visible = False
        If _IsFastCashMemo = False Then

            Me.ControlBox = False
            Me.WindowState = FormWindowState.Maximized
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Dim FormWidth As Integer = Me.Width
            Dim GridSize As New Size(FormWidth - 29, 450)
            Me.dgGridReciept.Size = GridSize
            TxtRemark.Width = 291
        Else
            Dim GridSize As New Size(741, 150)
            Me.dgGridReciept.Size = GridSize
            TxtRemark.Width = 150
        End If
        TextBox1.Visible = False
        Label1.Visible = False
    End Sub
    Private Sub txtMicrNumber_KeyPress(sender As Object, e As KeyPressEventArgs)
        If (Not Char.IsDigit(e.KeyChar)) AndAlso Not (e.KeyChar = Convert.ToChar(8)) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtChequeNo_KeyPress(sender As Object, e As KeyPressEventArgs)
        If (Not Char.IsDigit(e.KeyChar)) AndAlso Not (e.KeyChar = Convert.ToChar(8)) Then
            e.Handled = True
        End If
    End Sub
    Private Sub SetTabSequence()
        Try
            '---- Set Tab Index START

            Call SetFormTabStop(Me, tabStopValue:=False)
            Dim ctrTablIndex As New Dictionary(Of Object, Int16)

            ctrTablIndex.Add(Me.sizDetail, 0)
            ctrTablIndex.Add(Me.C1SizerPaymentTypes, 0)
            ctrTablIndex.Add(Me.C1SizerPaymentMode, 0)

            ctrTablIndex.Add(Me.CtrlChequeDetails, 1)
            ctrTablIndex.Add(Me.CtrlChequeDetails.cmbBankName, 0)
            ctrTablIndex.Add(Me.CtrlChequeDetails.txtChequeNo, 1)
            ctrTablIndex.Add(Me.CtrlChequeDetails.dtChequeDate, 2)
            ctrTablIndex.Add(Me.CtrlChequeDetails.txtMicrNumber, 3)
            ctrTablIndex.Add(Me.CtrlCLPPoint, 2)

            ctrTablIndex.Add(Me.CtrlPayCreditCheque, 3)
            ctrTablIndex.Add(Me.CtrlPayCreditCheque.dtpDueDate, 0)
            ctrTablIndex.Add(Me.CtrlPayCreditCheque.txtRemarks, 1)

            ctrTablIndex.Add(Me.CtrlGiftVoucherIssue, 4)
            ctrTablIndex.Add(Me.CtrlGiftVoucherIssue.cmbVoucherProgram, 0)


            ctrTablIndex.Add(Me.ctrlPayCheque, 5)
            ctrTablIndex.Add(Me.ctrlPayCheque.txtChequeNo, 0)
            ctrTablIndex.Add(Me.ctrlPayCheque.dtpExpiryDate, 1)
            ctrTablIndex.Add(Me.ctrlPayCheque.cmbVoucherProgram, 2)


            ctrTablIndex.Add(Me.ctrlPayCredit, 6)
            ctrTablIndex.Add(Me.ctrlPayCredit.cmbBankName, 0)
            ctrTablIndex.Add(Me.ctrlPayCredit.txtCreditCardNo, 1)
            ctrTablIndex.Add(Me.ctrlPayCredit.dtpExpiryDate, 2)



            ctrTablIndex.Add(Me.cboRecieptType, 1)
            ctrTablIndex.Add(Me.cboCurrency, 2)
            ctrTablIndex.Add(Me.ctrlPayCash, 3)
            ctrTablIndex.Add(Me.ctrlPayCash.txtCash, 0)
            ctrTablIndex.Add(Me.btnapprove, 4)
            ctrTablIndex.Add(Me.btndelete, 5)

            ctrTablIndex.Add(Me.TableLayoutPanel1, 1)
            ctrTablIndex.Add(Me.TxtRemark, 0)

            ctrTablIndex.Add(Me.dgGridReciept, 2)
            Call SetFormTabIndex(ctrTablIndex:=ctrTablIndex)
            Me.dgGridReciept.KeyActionTab = KeyActionEnum.None
            Me.sizDetail.TabStop = False
            Me.C1SizerPaymentTypes.TabStop = False
            Me.C1SizerPaymentMode.TabStop = False
            cboRecieptType.Select()

            '---- Set Tab Index END 

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub
    ''' <summary>
    ''' theme chenging function
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Themechange() As String

        'Dim pnlTopHeading As New Panel
        'Me.C1SizerMain.Controls.Add(pnlTopHeading)
        'pnlTopHeading.Location = New System.Drawing.Point(1, 1)
        'pnlTopHeading.BackColor = Color.YellowGreen
        'pnlTopHeading.Size=New Size
        'pnlTopHeading.Size = New System.Drawing.Size(892, 20)
        'pnlTopHeading.BringToFront()
        lblRetrunAmount.ForeColor = Color.White
        ''---------- color changing---------
        Me.BackgroundColor = Color.FromArgb(76, 76, 76)
        'lblDefaultCurrency
        '
        Me.lblDefaultCurrency.BackColor = Color.Transparent
        Me.lblDefaultCurrency.BorderStyle = BorderStyle.None
        Me.lblDefaultCurrency.ForeColor = Color.White
        Me.lblDefaultCurrency.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblDefaultCurrency.Text = Me.lblDefaultCurrency.Text.ToUpper()

        'lblDefaultCurrencyTrack
        '
        Me.lblDefaultCurrencyTrack.BackColor = Color.Transparent
        Me.lblDefaultCurrencyTrack.BorderColor = Color.FromArgb(76, 76, 76)
        Me.lblDefaultCurrencyTrack.ForeColor = Color.White
        Me.lblDefaultCurrencyTrack.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblDefaultCurrencyTrack.Text = Me.lblDefaultCurrencyTrack.Text.ToUpper()

        'lblTotalReciepts
        '
        Me.lblTotalReciepts.BackColor = Color.Transparent
        Me.lblTotalReciepts.BorderStyle = BorderStyle.None
        Me.lblTotalReciepts.ForeColor = Color.White
        Me.lblTotalReciepts.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblTotalReciepts.Text = Me.lblTotalReciepts.Text.ToUpper()

        'lblTotalReciept
        '
        Me.lblTotalReciept.BackColor = Color.Transparent
        Me.lblTotalReciept.BorderStyle = BorderStyle.None
        Me.lblTotalReciept.ForeColor = Color.White
        Me.lblTotalReciept.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblTotalReciept.Text = Me.lblTotalReciept.Text.ToUpper()

        'lblDefaultBalanceDue
        '
        Me.lblDefaultBalanceDue.BackColor = Color.Transparent
        Me.lblDefaultBalanceDue.BorderStyle = BorderStyle.None
        Me.lblDefaultBalanceDue.ForeColor = Color.White
        Me.lblDefaultBalanceDue.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblDefaultBalanceDue.Text = Me.lblDefaultBalanceDue.Text.ToUpper()

        'lblbalanceDue
        '
        Me.lblbalanceDue.BackColor = Color.Transparent
        Me.lblbalanceDue.BorderStyle = BorderStyle.None
        Me.lblbalanceDue.ForeColor = Color.White
        Me.lblbalanceDue.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblbalanceDue.Text = Me.lblbalanceDue.Text.ToUpper()

        'lblBillAmount
        '
        Me.lblBillAmount.BackColor = Color.Transparent
        Me.lblBillAmount.BorderStyle = BorderStyle.None
        Me.lblBillAmount.ForeColor = Color.White
        Me.lblBillAmount.ForeColor = Color.White
        Me.lblBillAmount.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblBillAmount.Text = Me.lblBillAmount.Text.ToUpper()

        'lblBillAmt
        '
        Me.lblBillAmt.BackColor = Color.Transparent
        Me.lblBillAmt.BorderStyle = BorderStyle.None
        Me.lblBillAmt.ForeColor = Color.White
        Me.lblBillAmt.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblBillAmt.Text = Me.lblBillAmt.Text.ToUpper()

        'lblCalCurrencyHeader
        '
        Me.lblCalCurrencyHeader.BackColor = Color.Transparent
        Me.lblCalCurrencyHeader.BorderStyle = BorderStyle.None
        Me.lblCalCurrencyHeader.ForeColor = Color.White
        Me.lblCalCurrencyHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblCalCurrencyHeader.Text = Me.lblCalCurrencyHeader.Text.ToUpper()

        'lblCurrencyBillAmount
        '
        Me.lblCurrencyBillAmount.BackColor = Color.Transparent
        Me.lblCurrencyBillAmount.BorderStyle = BorderStyle.None
        Me.lblCurrencyBillAmount.ForeColor = Color.White
        Me.lblCurrencyBillAmount.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblCurrencyBillAmount.Text = Me.lblCurrencyBillAmount.Text.ToUpper()

        'lblCalCurrencyBillAmount
        '
        Me.lblCalCurrencyBillAmount.BackColor = Color.Transparent
        Me.lblCalCurrencyBillAmount.BorderStyle = BorderStyle.None
        Me.lblCalCurrencyBillAmount.ForeColor = Color.White
        Me.lblCalCurrencyBillAmount.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblCalCurrencyBillAmount.Text = Me.lblCalCurrencyBillAmount.Text.ToUpper()

        'lblCurrencyTotalReciept
        '
        Me.lblCurrencyTotalReciept.BackColor = Color.Transparent
        Me.lblCurrencyTotalReciept.BorderStyle = BorderStyle.None
        Me.lblCurrencyTotalReciept.ForeColor = Color.White
        Me.lblCurrencyTotalReciept.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblCurrencyTotalReciept.Text = Me.lblCurrencyTotalReciept.Text.ToUpper()

        'lblCalCurrencyTotalReciepts
        '
        Me.lblCalCurrencyTotalReciepts.BackColor = Color.Transparent
        Me.lblCalCurrencyTotalReciepts.BorderStyle = BorderStyle.None
        Me.lblCalCurrencyTotalReciepts.ForeColor = Color.White
        Me.lblCalCurrencyTotalReciepts.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblCalCurrencyTotalReciepts.Text = Me.lblCalCurrencyTotalReciepts.Text.ToUpper()

        'lblCurrencyBalanceDue
        '
        Me.lblCurrencyBalanceDue.BackColor = Color.Transparent
        Me.lblCurrencyBalanceDue.BorderStyle = BorderStyle.None
        Me.lblCurrencyBalanceDue.ForeColor = Color.White
        Me.lblCurrencyBalanceDue.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblCurrencyBalanceDue.Text = Me.lblCurrencyBalanceDue.Text.ToUpper()

        'lblCalCurrencyBalanceDue
        '
        Me.lblCalCurrencyBalanceDue.BackColor = Color.Transparent
        Me.lblCalCurrencyBalanceDue.BorderStyle = BorderStyle.None
        Me.lblCalCurrencyBalanceDue.ForeColor = Color.White
        Me.lblCalCurrencyBalanceDue.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblCalCurrencyBalanceDue.Text = Me.lblCalCurrencyBalanceDue.Text.ToUpper()

        'lblReceiptType
        '
        Me.lblReceiptType.BackColor = Color.FromArgb(76, 76, 76)
        Me.lblReceiptType.BorderColor = Color.FromArgb(76, 76, 76)
        Me.lblReceiptType.ForeColor = Color.White
        Me.lblReceiptType.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.lblReceiptType.Text = Me.lblReceiptType.Text.ToUpper()

        'lblAmount
        '
        Me.lblAmount.AutoSize = False
        Me.lblAmount.BackColor = Color.FromArgb(76, 76, 76)
        Me.lblAmount.BorderColor = Color.FromArgb(76, 76, 76)
        Me.lblAmount.ForeColor = Color.White
        Me.lblAmount.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.lblAmount.Text = Me.lblAmount.Text.ToUpper()

        'lblSign
        '
        Me.lblSign.BackColor = Color.FromArgb(76, 76, 76)
        Me.lblSign.BorderColor = Color.FromArgb(76, 76, 76)
        Me.lblSign.ForeColor = Color.White
        Me.lblSign.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblSign.Text = Me.lblSign.Text.ToUpper()

        'lblMinimumBalanceAmount
        '
        Me.lblMinimumBalanceAmount.BackColor = Color.FromArgb(76, 76, 76)
        Me.lblMinimumBalanceAmount.BorderColor = Color.FromArgb(76, 76, 76)
        Me.lblMinimumBalanceAmount.ForeColor = Color.White
        Me.lblMinimumBalanceAmount.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblMinimumBalanceAmount.Text = Me.lblMinimumBalanceAmount.Text.ToUpper()

        'Paymentterm
        '
        Me.LblPayTerm.BackColor = Color.FromArgb(76, 76, 76)
        Me.LblPayTerm.BorderColor = Color.FromArgb(76, 76, 76)
        Me.LblPayTerm.ForeColor = Color.White
        Me.LblPayTerm.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.LblPayTerm.Text = Me.LblPayTerm.Text.ToUpper()


        'lblCalMinBalDue
        '
        Me.lblCalMinBalDue.BackColor = Color.FromArgb(76, 76, 76)
        Me.lblCalMinBalDue.BorderColor = Color.FromArgb(76, 76, 76)
        Me.lblCalMinBalDue.ForeColor = Color.White
        Me.lblCalMinBalDue.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblCalMinBalDue.Text = Me.lblCalMinBalDue.Text.ToUpper()

        'lblCurrencyMinimumBalAmt
        '
        Me.lblCurrencyMinimumBalAmt.BackColor = Color.FromArgb(76, 76, 76)
        Me.lblCurrencyMinimumBalAmt.BorderColor = Color.FromArgb(76, 76, 76)
        Me.lblCurrencyMinimumBalAmt.ForeColor = Color.White
        Me.lblCurrencyMinimumBalAmt.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblCurrencyMinimumBalAmt.Text = Me.lblCurrencyMinimumBalAmt.Text.ToUpper()

        'lblCalCurrencyMiniBalDue
        '
        Me.lblCalCurrencyMiniBalDue.BackColor = Color.FromArgb(76, 76, 76)
        Me.lblCalCurrencyMiniBalDue.BorderColor = Color.FromArgb(76, 76, 76)
        Me.lblCalCurrencyMiniBalDue.ForeColor = Color.White
        Me.lblCalCurrencyMiniBalDue.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblCalCurrencyMiniBalDue.Text = Me.lblCalCurrencyMiniBalDue.Text.ToUpper()
        '--------------------------------
        '  lbltender



        Me.lbltender.BackColor = Color.FromArgb(76, 76, 76)
        Me.lbltender.BorderColor = Color.FromArgb(76, 76, 76)
        Me.lbltender.ForeColor = Color.White
        '   Me.lbltender.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        '  Me.lbltender.Text = Me.lblCalCurrencyMiniBalDue.Text.ToUpper()
        ' --------------------------------

        Me.lblCust.BackColor = Color.FromArgb(76, 76, 76)
        Me.lblCust.BorderColor = Color.FromArgb(76, 76, 76)
        Me.lblCust.ForeColor = Color.White







        Me.lblComp.BackColor = Color.FromArgb(76, 76, 76)
        Me.lblComp.BorderColor = Color.FromArgb(76, 76, 76)
        Me.lblComp.ForeColor = Color.White


        Me.lblContact.BackColor = Color.FromArgb(76, 76, 76)
        Me.lblContact.BorderColor = Color.FromArgb(76, 76, 76)
        Me.lblContact.ForeColor = Color.White


        Me.lblContactNo.BackColor = Color.FromArgb(76, 76, 76)
        Me.lblContactNo.BorderColor = Color.FromArgb(76, 76, 76)
        Me.lblContactNo.ForeColor = Color.White

        Me.lblCompanyName.BackColor = Color.FromArgb(76, 76, 76)
        Me.lblCompanyName.BorderColor = Color.FromArgb(76, 76, 76)
        Me.lblCompanyName.ForeColor = Color.White


        Me.lblCustomer.BackColor = Color.FromArgb(76, 76, 76)
        Me.lblCustomer.BorderColor = Color.FromArgb(76, 76, 76)
        Me.lblCustomer.ForeColor = Color.White


        Me.Label1.BackColor = Color.FromArgb(76, 76, 76)
        ' Me.Label1.BorderColor = Color.FromArgb(76, 76, 76)
        Me.Label1.ForeColor = Color.White




        Me.GbCustomer.ForeColor = Color.White
        'ctrlPayCash
        '
        Me.ctrlPayCash.BackColor = Color.FromArgb(76, 76, 76)
        Me.CtrlChequeDetails.BackColor = Color.FromArgb(76, 76, 76)

        'lblSelectCurrency
        '
        Me.lblSelectCurrency.BackColor = Color.FromArgb(76, 76, 76)
        Me.lblSelectCurrency.BorderColor = Color.FromArgb(76, 76, 76)
        Me.lblSelectCurrency.ForeColor = Color.White
        Me.lblSelectCurrency.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.lblSelectCurrency.Text = Me.lblSelectCurrency.Text.ToUpper()

        'lblRetrunAmount
        '
        Me.lblRetrunAmount.BackColor = Color.FromArgb(76, 76, 76)
        Me.lblRetrunAmount.BorderColor = Color.FromArgb(76, 76, 76)

        'lblSwapCard
        '
        '   Me.lblSwapCard.BackColor = Color.FromArgb(76, 76, 76)
        '    Me.lblSwapCard.BorderColor = Color.FromArgb(76, 76, 76)

        'lblRemark
        '
        '  Me.Label1.Size = New System.Drawing.Size(75, 18)
        'Me.Label1.ForeColor = Color.White
        'Me.Label1.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        'Me.Label1.Text = Me.Label1.Text.ToUpper()

        'Button delete/approve
        '
        Me.btndelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
        Me.btndelete.Image = Nothing
        Me.btndelete.BackgroundImageLayout = ImageLayout.Stretch
        Me.btndelete.BackgroundImage = My.Resources.Cancelnew
        btndelete.FlatStyle = FlatStyle.Flat

        Me.btnapprove.VisualStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnapprove.Image = Nothing
        Me.btnapprove.BackgroundImageLayout = ImageLayout.Stretch
        Me.btnapprove.BackgroundImage = My.Resources.Oknew
        btnapprove.FlatStyle = FlatStyle.Flat
        'sizDetail
        '
        Me.sizDetail.BackColor = Color.FromArgb(76, 76, 76)
        Me.sizDetail.Border.Color = Color.FromArgb(76, 76, 76)

        Me.sizboxBottom.Border.Color = Color.FromArgb(76, 76, 76)
        Me.sizboxBottom.BackColor = Color.FromArgb(76, 76, 76)
        Me.sizboxBottom.Border.Color = Color.FromArgb(76, 76, 76)

        Me.C1SizerPaymentTypes.BackColor = Color.FromArgb(76, 76, 76)

        'C1SizerPaymentMode
        '
        Me.C1SizerPaymentMode.BackColor = Color.FromArgb(76, 76, 76)
        Me.C1SizerPaymentMode.Border.Color = Color.FromArgb(76, 76, 76)

        'ctrlPayCheque
        '
        Me.ctrlPayCheque.BackColor = Color.FromArgb(76, 76, 76)
        Me.ctrlPayCheque.lblExpiryDate.BackColor = Color.Transparent
        Me.ctrlPayCheque.lblExpiryDate.BorderStyle = BorderStyle.None
        Me.ctrlPayCheque.lblExpiryDate.ForeColor = Color.White
        Me.ctrlPayCheque.lblExpiryDate.Font = New Font("Neo Sans", 10, FontStyle.Bold)


        Me.ctrlPayCheque.lblChequeNo.BackColor = Color.Transparent
        Me.ctrlPayCheque.lblChequeNo.BorderStyle = BorderStyle.None
        Me.ctrlPayCheque.lblChequeNo.ForeColor = Color.White
        Me.ctrlPayCheque.lblChequeNo.Font = New Font("Neo Sans", 10, FontStyle.Bold)

        Me.ctrlPayCheque.ctrlGiftVouc.BackColor = Color.Transparent
        Me.ctrlPayCheque.ctrlGiftVouc.BorderStyle = BorderStyle.None
        Me.ctrlPayCheque.ctrlGiftVouc.ForeColor = Color.White
        Me.ctrlPayCheque.ctrlGiftVouc.Font = New Font("Neo Sans", 8, FontStyle.Bold)

        'CtrlChequeDetails
        '
        Me.CtrlChequeDetails.BackColor = Color.FromArgb(76, 76, 76)
        Me.CtrlChequeDetails.lblBankName.BackColor = Color.Transparent
        Me.CtrlChequeDetails.lblBankName.BorderStyle = BorderStyle.None
        Me.CtrlChequeDetails.lblBankName.ForeColor = Color.White
        Me.CtrlChequeDetails.lblBankName.Font = New Font("Neo Sans", 10, FontStyle.Bold)

        Me.CtrlChequeDetails.lblChequeNo.BackColor = Color.Transparent
        Me.CtrlChequeDetails.lblChequeNo.BorderStyle = BorderStyle.None
        Me.CtrlChequeDetails.lblChequeNo.ForeColor = Color.White
        Me.CtrlChequeDetails.lblChequeNo.Font = New Font("Neo Sans", 10, FontStyle.Bold)

        Me.CtrlChequeDetails.lblChequeDate.BackColor = Color.Transparent
        Me.CtrlChequeDetails.lblChequeDate.BorderStyle = BorderStyle.None
        Me.CtrlChequeDetails.lblChequeDate.ForeColor = Color.White
        Me.CtrlChequeDetails.lblChequeDate.Font = New Font("Neo Sans", 10, FontStyle.Bold)

        Me.CtrlChequeDetails.lblMicrNumber.BackColor = Color.Transparent
        Me.CtrlChequeDetails.lblMicrNumber.BorderStyle = BorderStyle.None
        Me.CtrlChequeDetails.lblMicrNumber.ForeColor = Color.White
        Me.CtrlChequeDetails.lblMicrNumber.Font = New Font("Neo Sans", 10, FontStyle.Bold)

        'CtrlGiftVoucherIssue
        '
        Me.CtrlGiftVoucherIssue.BackColor = Color.FromArgb(76, 76, 76)
        'ctrlPayCredit
        '
        Me.ctrlPayCredit.BackColor = Color.FromArgb(76, 76, 76)
        Me.ctrlPayCredit.lblBankName.BackColor = Color.Transparent
        Me.ctrlPayCredit.lblBankName.BorderStyle = BorderStyle.None
        Me.ctrlPayCredit.lblBankName.ForeColor = Color.White
        Me.ctrlPayCredit.lblBankName.Font = New Font("Neo Sans", 10, FontStyle.Bold)


        Me.ctrlPayCredit.lblCreditCardNo.BackColor = Color.Transparent
        Me.ctrlPayCredit.lblCreditCardNo.BorderStyle = BorderStyle.None
        Me.ctrlPayCredit.lblCreditCardNo.ForeColor = Color.White
        Me.ctrlPayCredit.lblCreditCardNo.Font = New Font("Neo Sans", 10, FontStyle.Bold)

        Me.ctrlPayCredit.lblExpiryDate.BackColor = Color.Transparent
        Me.ctrlPayCredit.lblExpiryDate.BorderStyle = BorderStyle.None
        Me.ctrlPayCredit.lblExpiryDate.ForeColor = Color.White
        Me.ctrlPayCredit.lblExpiryDate.Font = New Font("Neo Sans", 10, FontStyle.Bold)


        'C1SizerMain
        '
        Me.C1SizerMain.BackColor = Color.FromArgb(76, 76, 76)
        '  Me.C1SizerMain.Location = New System.Drawing.Point(0, 33)
        '  Me.C1SizerMain.Size = New System.Drawing.Size(700, 545)
        ' Me.C1SizerMain.AutoSizeMode = C1.Win.C1Sizer.AutoSizeEnum.None

        ''----color changing end-----------------


        'sizboxBottom
        '
        ' Me.sizboxBottom.Dock = DockStyle.None
        ' ' Me.sizboxBottom.Location = New System.Drawing.Point(2, 2)
        ' Me.sizboxBottom.Width = 900

        'dgGridReciept
        '
        dgGridReciept.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        dgGridReciept.Styles.Highlight.BackColor = Color.FromArgb(153, 255, 255)
        dgGridReciept.Styles.Highlight.ForeColor = Color.Black
        dgGridReciept.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgGridReciept.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        dgGridReciept.Rows.MinSize = 26
        dgGridReciept.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        dgGridReciept.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgGridReciept.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgGridReciept.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgGridReciept.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgGridReciept.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgGridReciept.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        dgGridReciept.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgGridReciept.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgGridReciept.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgGridReciept.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)

        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = Color.FromArgb(76, 76, 76)
        ' Me.TableLayoutPanel1.Dock = DockStyle.None
        '  Me.TableLayoutPanel1.Location = New System.Drawing.Point(904, 2)
        ' Me.TableLayoutPanel1.Size = New System.Drawing.Size(465, 100)

        'sizDetail
        '
        'Me.sizDetail.Height = 500
        'Me.sizDetail.Grid.Rows(0).IsFixedSize = True
        'Me.sizDetail.Grid.Rows(0).Size = 165
        'Me.sizDetail.Grid.Rows(1).IsFixedSize = True
        'Me.sizDetail.Grid.Rows(1).Size = 300
        'Me.sizDetail.Grid.Rows(2).IsFixedSize = True
        'Me.sizDetail.Grid.Rows(2).Size = 19
        'Me.sizDetail.Grid.Rows(3).IsFixedSize = True
        'Me.sizDetail.Grid.Rows(3).Size = 28
        'Me.sizDetail.Grid.Rows.Remove(2)

        'pnlSwapCard
        '
        Dim pnlSwapCard As New Panel
        '  pnlSwapCard.Location = New System.Drawing.Point(1, 498)
        '  pnlSwapCard.Size = New System.Drawing.Size(128, 28)
        pnlSwapCard.BackColor = Color.FromArgb(76, 76, 76)
        '  sizDetail.Controls.Add(pnlSwapCard)
        '  pnlSwapCard.BringToFront()


        '  pnlSwapCard.Controls.Add(lblSwapCard)
        '  lblSwapCard.Location = New System.Drawing.Point(4, 5)
        '  lblSwapCard.Size = New System.Drawing.Size(169, 18)
        '  pnlSwapCard.Controls.Add(rtxtSwipeCard)

        '  Me.rtxtSwipeCard.Location = New System.Drawing.Point(175, 4)
        '  Me.rtxtSwipeCard.MaximumSize = New Size(400, 0)
        ' Me.rtxtSwipeCard.Size = New System.Drawing.Size(400, 20)

        'lblSwapCard
        '
        '  Me.lblSwapCard.Height = 30
        '  Me.lblSwapCard.Size = New System.Drawing.Size(50, 30)
        '  Me.lblSwapCard.BackColor = Color.Transparent ' Color.FromArgb(134, 134, 134)
        ' Me.lblSwapCard.BorderStyle = BorderStyle.None
        ' Me.lblSwapCard.ForeColor = Color.White
        'Me.lblSwapCard.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        'Me.lblSwapCard.Text = Me.lblSwapCard.Text.ToUpper()
        ' Me.lblSwapCard.BringToFront()

        'sizSaveBtn
        '
        '  Me.sizSaveBtn.SplitterWidth = 0
        ' Me.sizSaveBtn.Dock = DockStyle.None
        '  Me.sizSaveBtn.Location = New System.Drawing.Point(2, 650)
        '   Me.sizSaveBtn.Size = New System.Drawing.Size(1200, 100) '756
        '   Me.sizSaveBtn.BringToFront()
        '    Me.sizSaveBtn.GridDefinition = <data name="sizSaveBtn.GridDefinition" xml:space="preserve">
        '                                   <value>43.4782608695652:False:False;44.3478260869565:False:False;	11.5348837209302:False:True;11.5348837209302:False:True;11.5348837209302:False:True;11.5348837209302:False:True;11.5348837209302:False:True;11.5348837209302:False:True;11.5348837209302:False:True;11.5348837209302:False:True;11.5348837209302:False:True;</value>
        '                              </data>
        '  Me.sizSaveBtn.Grid.Rows(0).Size = 75
        '   Me.sizSaveBtn.Grid.Rows.Remove(1)
        Me.sizSaveBtn.Border.Color = Color.Transparent
        'btnF5Cash
        '
        '  Me.btnF5Cash.Dock = DockStyle.None
        'Me.btnF5Cash.Location = New System.Drawing.Point(5, 5)
        '  Me.btnF5Cash.Size = New System.Drawing.Size(100, 50)
        Me.btnF5Cash.BringToFront()
        Me.btnF5Cash.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.btnF5Cash.Image = Global.Spectrum.My.Resources.Resources.Cash_Normal1
        btnF5Cash.TextImageRelation = TextImageRelation.Overlay
        btnF5Cash.Text = "F5 Cash"
        Me.btnF5Cash.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnF5Cash.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnF5Cash.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System

        'btnF3
        '
        '  Me.btnF3.Dock = DockStyle.None
        '  Me.btnF3.Location = New System.Drawing.Point(133, 5)
        '  Me.btnF3.Size = New System.Drawing.Size(100, 50)
        Me.btnF3.BringToFront()
        Me.btnF3.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.btnF3.Image = Global.Spectrum.My.Resources.Resources.Cheque_Normal
        btnF3.Text = "F7 Check"
        Me.btnF3.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnF3.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnF3.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System

        'btnF4CreditNote
        '
        '  Me.btnF4CreditNote.Dock = DockStyle.None
        '  Me.btnF4CreditNote.Location = New System.Drawing.Point(261, 5)
        '  Me.btnF4CreditNote.Size = New System.Drawing.Size(100, 50)
        Me.btnF4CreditNote.BringToFront()
        Me.btnF4CreditNote.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.btnF4CreditNote.Image = Global.Spectrum.My.Resources.Resources.CreditNote_Normal
        btnF4CreditNote.Text = "F4 Credit Note"
        Me.btnF4CreditNote.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnF4CreditNote.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnF4CreditNote.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System

        'btnF6
        '
        ' Me.btnF6.Dock = DockStyle.None
        ' Me.btnF6.Location = New System.Drawing.Point(389, 5)
        ' Me.btnF6.Size = New System.Drawing.Size(100, 50)
        Me.btnF6.BringToFront()
        Me.btnF6.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.btnF6.Image = Global.Spectrum.My.Resources.Resources.GiftVoucher_Normal
        btnF6.Text = "F6 Gift Voucher"
        Me.btnF6.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnF6.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnF6.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System

        'btnSave
        '
        ' Me.btnSave.Dock = DockStyle.None
        ' Me.btnSave.Location = New System.Drawing.Point(517, 5)
        ' Me.btnSave.Size = New System.Drawing.Size(100, 50)
        Me.btnSave.BringToFront()
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.btnSave.Image = Global.Spectrum.My.Resources.Resources.Save_Print_Normal
        btnSave.Text = "F10 Save and Print"
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System

        'btnGift
        '
        ' Me.btnGift.Dock = DockStyle.None
        ' Me.btnGift.Location = New System.Drawing.Point(645, 5)
        ' Me.btnGift.Size = New System.Drawing.Size(100, 50)
        Me.btnGift.BringToFront()
        Me.btnGift.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.btnGift.Image = Global.Spectrum.My.Resources.Resources.Save_GiftPoint_Normal
        btnGift.Text = "F11 Save and Gift Print"
        Me.btnGift.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnGift.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnGift.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System

        'btnCancel
        '
        ' Me.btnCancel.Size = New System.Drawing.Size(100, 50)
        Me.btnCancel.BringToFront()
        Me.btnCancel.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.btnCancel.Image = Global.Spectrum.My.Resources.Resources.Cancel_Normal
        ' btnCancel.Text = "F11 Save and Gift Print"
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System

        'vipin 16.05.2018
        Me.BtnCustSearch.BringToFront()
        Me.BtnCustSearch.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.BtnCustSearch.Image = Global.Spectrum.My.Resources.Resources.Cancel_Normal
        Me.BtnCustSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCustSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnCustSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        '  btnCancel.Dock = DockStyle.None
        If btnGift.Visible = True Then
        Else
            '  btnCancel.Location = New System.Drawing.Point(645, 5)
            '   btnOK.Location = New System.Drawing.Point(980, 5)
        End If
        '   btnCancel.Size = New System.Drawing.Size(100, 50)
        ' btnCancel.BringToFront()
        '  btnOK.Size = New System.Drawing.Size(100, 50)
        '   btnOK.BringToFront()

        Return ""
    End Function
    ''' <summary>
    ''' Sub method of form calling 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub frmLoad()

        Try
            cboRecieptType.DisplayMember = "TenderHeadName"
            Dim enableinnoviti As Boolean = False
            If clsDefaultConfiguration.PayFromInnoviti AndAlso clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) Then
                enableinnoviti = True
            End If
            dtPayment = objBLLAcceptPayment.LoadRecieptType(PaymentType, clsAdmin.SiteCode, enableinnoviti)
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

    Private Sub txtTelephoneNo_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If (e.KeyCode = Keys.Enter) And CtrlChequeDetails.Visible = True Then
            InsertChequeDetails()
        End If
    End Sub

    Private Sub txtChequeNo_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If CtrlChequeDetails.txtChequeNo.Visible Then
            If Not String.IsNullOrEmpty(CtrlChequeDetails.txtChequeNo.Text) Then
                If IsNumeric(CtrlChequeDetails.txtChequeNo.Text) = False Then
                    CtrlChequeDetails.txtChequeNo.Clear()
                    'CtrlChequeDetails.txtChequeNo.Focus()
                    ShowMessage(getValueByKey("ctrlchequedetails.txtchequenovalid"), getValueByKey("CLAE04"))
                End If
            End If
        End If
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
                'lblCurrencyMinimumBalAmt.Visible = True
                lblCalMinBalDue.Visible = True
                'lblCalCurrencyMiniBalDue.Visible = True
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
            '--  removeCLPOPtion comment by Mahesh as per required if bills Amount is less then Zero i.e. Return case only Neg tender will show n we have set that earliar 
            '-- and in case amt is Positive no need to do it again as we have set earliar 
            '   removeCLPOPtion()
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
        ctrlPayCredit.cmbBankName.Select()

        'If clsDefaultConfiguration.CreditCardInfo = False Then
        '    'ctrlPayCredit.Visible = False ''commmented by rama 0n 27-nov-2009 as per rashid instrustion
        '    ctrlPayCredit.cmbBankName.Select()
        'Else
        '    ctrlPayCredit.cmbBankName.Select()
        'End If


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
                    objBLLAcceptPayment.BankName = ctrlPayCredit.cmbBankName.SelectedText
                    If ValidateCreditCardEntries() = True Then
                        If clsDefaultConfiguration.IsCreditCardRefundAllowed = True Then
                            AddRowRecipetAmountInGrid(TenderHeadCode, EnteredAmount, ctrlPayCredit.txtCreditCardNo.Text, ctrlPayCredit.dtpExpiryDate.Value, ctrlPayCredit.cmbBankName.SelectedValue)
                            ClearTextBox()
                            ctrlPayCash.txtCash.Focus()
                            Exit Sub
                        End If
                        If ((MyRound(TotalBillAmount - TotalReceivedAmount, clsDefaultConfiguration.BillRoundOffAt)) >= EnteredAmount) Then
                            AddRowRecipetAmountInGrid(TenderHeadCode, EnteredAmount, ctrlPayCredit.txtCreditCardNo.Text, ctrlPayCredit.dtpExpiryDate.Value, ctrlPayCredit.cmbBankName.SelectedValue)  '' commented by nikhil  SelectedValue
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
                Else
                    If ValidateCreditCardEntries() = True Then
                        If clsDefaultConfiguration.IsCreditCardRefundAllowed = True Then
                            AddRowRecipetAmountInGrid(TenderHeadCode, EnteredAmount, ctrlPayCredit.txtCreditCardNo.Text, ctrlPayCredit.dtpExpiryDate.Value, ctrlPayCredit.cmbBankName.SelectedValue)
                            ClearTextBox()
                            ctrlPayCash.txtCash.Focus()
                            Exit Sub
                        End If
                        If (MyRound(TotalBillAmount - TotalReceivedAmount, clsDefaultConfiguration.BillRoundOffAt) >= EnteredAmount) Then
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
                    If (MyRound(TotalBillAmount - TotalReceivedAmount, clsDefaultConfiguration.BillRoundOffAt) >= EnteredAmount) Then 'bug no 898

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
            ''----------------------------------------------------------------------------------------------
            ''added on 12 may - ashma - for Innoviti (If condition)
            If clsDefaultConfiguration.PayFromInnoviti AndAlso clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) AndAlso AllowInnovitiPayment = True Then
                Return True
            End If
            ''----------------------------------------------------------------------------------------------
            If clsDefaultConfiguration.CreditCardInfo = False Then
                If (String.IsNullOrEmpty(ctrlPayCredit.txtCreditCardNo.Text)) Then
                    ctrlPayCredit.txtCreditCardNo.Focus()
                    ShowMessage(getValueByKey("ACP006"), "ACP006 - " & getValueByKey("CLAE04"))
                ElseIf Not (ctrlPayCredit.txtCreditCardNo.Text.Length >= 4 AndAlso ctrlPayCredit.txtCreditCardNo.Text.Length <= 16) Then
                    ctrlPayCredit.txtCreditCardNo.Focus()
                    ShowMessage(getValueByKey("ACP021"), "ACP021 - " & getValueByKey("CLAE04"))
                Else
                    Return True
                End If
            Else
                If ctrlPayCredit.cmbBankName.SelectedIndex = -1 Then
                    ctrlPayCredit.cmbBankName.Focus()
                    ShowMessage(getValueByKey("CHKP09"), "CHKP09 - " & getValueByKey("CLAE04"))
                    Return False
                End If
                If (String.IsNullOrEmpty(ctrlPayCredit.txtCreditCardNo.Text)) Then
                    ctrlPayCredit.txtCreditCardNo.Focus()
                    ShowMessage(getValueByKey("ACP006"), "ACP006 - " & getValueByKey("CLAE04"))
                    'ElseIf (String.IsNullOrEmpty(ctrlPayCredit.txtSlipNO.Text)) Then
                    '    ctrlPayCredit.txtSlipNO.Focus()
                    '    ShowMessage(getValueByKey("ACP024"), "ACP024 - " & getValueByKey("CLAE04"))
                    'ElseIf (String.IsNullOrEmpty(ctrlPayCredit.txtAuthCode.Text)) Then
                    '    ctrlPayCredit.txtAuthCode.Focus()
                    ' ShowMessage(getValueByKey("ACP024"), "ACP024 - " & getValueByKey("CLAE04"))
                ElseIf Not (ctrlPayCredit.txtCreditCardNo.Text.Length >= 4 AndAlso ctrlPayCredit.txtCreditCardNo.Text.Length <= 16) Then
                    ctrlPayCredit.txtCreditCardNo.Focus()
                    ShowMessage(getValueByKey("ACP021"), "ACP021 - " & getValueByKey("CLAE04"))
                    'ElseIf String.IsNullOrEmpty(ctrlPayCredit.dtpExpiryDate.Text) OrElse ctrlPayCredit.dtpExpiryDate.Value < clsAdmin.CurrentDate.Date Then
                    '    ctrlPayCredit.dtpExpiryDate.Focus()
                    '    ShowMessage(getValueByKey("ACP034"), "ACP034 - " & getValueByKey("CLAE04"))
                ElseIf ctrlPayCredit.DateValid = False Then
                    Return False
                Else
                    Return True
                End If
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

        ''Online Payment Integration
        'Dim cardNo = "4377486995847843", expiryDate = "0416", paymentAmount As String = "725" 'Rakesh
        ''Dim cardNo = "4591510071301407", expiryDate = "0917", paymentAmount As String = "125" 'Vinit

        'Dim onlinePaymentResponse = Spectrum.OnlinePay.PaymentGateway.CardPayment(cardNo, expiryDate, paymentAmount)

        '' 1 = Approved 2 = Declined 3 = Error 4 = Held for Review
        'If onlinePaymentResponse(0) <> 1 Then
        '    MsgBox(onlinePaymentResponse(3), MsgBoxStyle.Exclamation, "PaymentGateway")
        'Else
        '    MsgBox(onlinePaymentResponse(3), MsgBoxStyle.Exclamation, "PaymentGateway")
        'End If
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

                AddRowRecipetAmountInGrid(TenderHeadCode, EnteredAmount, ctrlPayCheque.txtChequeNo.Text, ctrlPayCheque.dtpExpiryDate.Value, String.Empty)
                ClearTextBox()
                Exit Sub

            ElseIf SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.Cheque AndAlso clsDefaultConfiguration.ChequeInfomation Then

                Dim ChequeExpiryDays As Int32 = clsDefaultConfiguration.CheckExpiryMonth
                Dim prviousday As DateTime = DateAdd(DateInterval.Month, ChequeExpiryDays * -1, Now)
                Dim futureday As DateTime = DateAdd(DateInterval.Month, ChequeExpiryDays, Now)

                If String.IsNullOrEmpty(CtrlChequeDetails.cmbBankName.SelectedValue) Then
                    ShowMessage(getValueByKey("CHKP09"), "CHKP09 - " & getValueByKey("CLAE04"))
                    CtrlChequeDetails.cmbBankName.Focus()
                    Exit Sub

                ElseIf String.IsNullOrEmpty(CtrlChequeDetails.txtChequeNo.Text) Then
                    ShowMessage(getValueByKey("CHKP06"), "CHKP06 - " & getValueByKey("CLAE04"))
                    CtrlChequeDetails.txtChequeNo.Focus()
                    Exit Sub

                ElseIf (CtrlChequeDetails.txtChequeNo.Text.Length < 6) Then     'vipin PC
                    ShowMessage("Cheque No. Should Be of 6 digit.", getValueByKey("CHKP06"))
                    CtrlChequeDetails.txtChequeNo.Focus()
                    Exit Sub
                ElseIf CDate(CDate(CtrlChequeDetails.dtChequeDate.Value).ToString("yyyy-MM-dd")) < CDate(DateAdd(DateInterval.Day, -85, clsAdmin.DayOpenDate).ToString("yyyy-MM-dd")) Then   'vipin
                    ShowMessage("Cheque date has lapsed 85 days period", getValueByKey("CHKP06"))
                    CtrlChequeDetails.dtChequeDate.Focus()
                    Exit Sub

                ElseIf String.IsNullOrEmpty(CtrlChequeDetails.txtMicrNumber.Text) Then
                    ShowMessage(getValueByKey("CHKP10"), "CHKP10 - " & getValueByKey("CLAE04"))
                    CtrlChequeDetails.txtMicrNumber.Focus()
                    Exit Sub

                ElseIf (CtrlChequeDetails.txtMicrNumber.Text.Length < 9) Then     'vipin PC
                    ShowMessage("MICR No. Should Be of 9 digit.", getValueByKey("CHKP06"))
                    CtrlChequeDetails.txtMicrNumber.Focus()
                    Exit Sub

                ElseIf CtrlChequeDetails.dtChequeDate.Value IsNot DBNull.Value AndAlso (CtrlChequeDetails.dtChequeDate.Value > futureday Or CtrlChequeDetails.dtChequeDate.Value < prviousday) Then
                    ShowMessage(getValueByKey("CHKP05"), "CHKP05 - " & getValueByKey("CLAE04"))
                    CtrlChequeDetails.dtChequeDate.Focus()
                    Exit Sub

                ElseIf clsDefaultConfiguration.IsChequeRefundAllowed = False AndAlso (EnteredAmount > MyRound(TotalBillAmount - TotalReceivedAmount, clsDefaultConfiguration.BillRoundOffAt)) Then
                    ShowMessage(getValueByKey("ACP009"), "ACP009 - " & getValueByKey("CLAE04"))
                    ctrlPayCash.txtCash.Clear()
                    ctrlPayCash.txtCash.Focus()
                    Exit Sub
                ElseIf EnteredAmount = 0 Then 'vipin 24.11.2017
                    ShowMessage("Enter Valid Amount", "ACP009 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
            End If

            If SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.Cheque Then
                If isChangeTenderMode Then 'For Change Tender Mode

                    If ((MyRound(TotalBillAmount - TotalReceivedAmount, clsDefaultConfiguration.BillRoundOffAt)) >= EnteredAmount) Then
                        AddRowRecipetAmountInGrid(TenderHeadCode, EnteredAmount, "", Now, CtrlChequeDetails.cmbBankName.SelectedValue)
                        ClearTextBox()
                        ctrlPayCash.txtCash.Focus()
                    Else
                        ShowMessage(getValueByKey("ACP009"), "ACP009 - " & getValueByKey("CLAE04"))
                        ctrlPayCash.txtCash.Focus()
                        Exit Sub
                    End If
                Else
                    AddRowRecipetAmountInGrid(TenderHeadCode, EnteredAmount, "", Now, CtrlChequeDetails.cmbBankName.SelectedValue)
                End If
                '  AddRowRecipetAmountInGrid(TenderHeadCode, EnteredAmount, "", Now, CtrlChequeDetails.cmbBankName.SelectedValue)
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
                '  dataRow("BankName") = CtrlChequeDetails.cmbBankName.SelectedValue
                dataRow("BankName") = CtrlChequeDetails.cmbBankName.SelectedText     'vipin 07.06.2017
                dataRow("CustomerName") = CtrlChequeDetails.txtMicrNumber.Text
                dataRow("TelephoneNumber") = String.Empty 'CtrlChequeDetails.txtTelephoneNo.Text
                dataRow("STATUS") = 1
                dtRecieptType.Rows(dtRecieptType.Rows.Count - 1)("Number") = CtrlChequeDetails.txtChequeNo.Text
                chkDetails.TableName = "CheckDtls"
                If dsRecieptType.Tables.Contains("CheckDtls") Then
                    dsRecieptType.Tables("CheckDtls").Rows.Add(dataRow)
                Else
                    chkDetails.Rows.Add(dataRow)
                    dsRecieptType.Tables.Add(chkDetails)
                End If
                Dim TempCheckDetail As New DataSet
                TempCheckDetail.Tables.Add(dsRecieptType.Tables("CheckDtls").Copy)
                DtCheckDetail = TempCheckDetail
                ClearChequeTextBox()
                Exit Sub
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
                        If (String.IsNullOrEmpty(CtrlChequeDetails.txtChequeNo.Text.Trim())) Then
                            ShowMessage(getValueByKey("ACP012"), "ACP012 - " & getValueByKey("CLAE04"))
                        End If
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
                    AddRowRecipetAmountInGrid(TenderHeadCode, EnteredAmount, ctrlPayCheque.txtChequeNo.Text, ctrlPayCheque.dtpExpiryDate.Value, String.Empty)
                    ClearTextBox()
                    'End If
                End If
            End If

            If (Val(lblbalanceDue.Text) < 0) Then
                ctrlPayCash.txtCash.Value = Val(lblbalanceDue.Text) * -1
            Else
                ctrlPayCash.txtCash.Value = Val(lblbalanceDue.Text)
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
            If IsGiftVoucherIssued IsNot Nothing Then
                For Each kvp As KeyValuePair(Of String, String) In IsGiftVoucherIssued
                    If kvp.Value.ToUpper = ctrlPayCheque.txtChequeNo.Text.ToUpper Then
                        ShowMessage(getValueByKey("ACP035"), "ACP035 - " & getValueByKey("CLAE04"))
                        ctrlPayCheque.txtChequeNo.Focus()
                        Exit Sub
                    End If
                Next
            End If
            Dim dvProgram As DataView = DirectCast(ctrlPayCheque.cmbVoucherProgram.DataSource, System.Data.DataView)

            'Rakesh-24.09.2013:Issue-7932=>Check GV program exist or not
            If (Not dvProgram Is Nothing AndAlso dvProgram.Count = 0) Then
                ShowMessage(getValueByKey("CM063"), getValueByKey("CLAE04"))
                Exit Sub
            ElseIf (String.IsNullOrEmpty(ctrlPayCheque.txtChequeNo.Text)) Then
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
                Dim IsGvHasExpiry As Boolean = True
                If objBLLAcceptPayment.FnGiftVoucherValidate(ctrlPayCheque.txtChequeNo.Text, msg, Amt, ExpiryDate, False, ctrlPayCheque.cmbVoucherProgram.SelectedValue, , , IsGvHasExpiry) = False Then
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
                    If isChangeTenderMode Then 'For Change Tender Mode
                        If ((MyRound(TotalBillAmount - TotalReceivedAmount, clsDefaultConfiguration.BillRoundOffAt)) < EnteredAmount) Then
                            ShowMessage(getValueByKey("ACP009"), "ACP009 - " & getValueByKey("CLAE04"))
                            ctrlPayCash.txtCash.Focus()
                            Exit Sub
                        End If
                    End If
                    ctrlPayCash.txtCash.Text = Amt
                    'End If
                End If
                'ctrlPayCheque.dtpExpiryDate.Text = ExpiryDate
                If IsGvHasExpiry Then
                    ctrlPayCheque.dtpExpiryDate.Value = ExpiryDate
                Else
                    ctrlPayCheque.dtpExpiryDate.Value = Nothing
                End If
                'If (IsChequeDateValid) Then
                AddRowRecipetAmountInGrid(TenderHeadCode, Amt, ctrlPayCheque.txtChequeNo.Text, ctrlPayCheque.dtpExpiryDate.Value, String.Empty)
                ClearTextBox()
                'End If
            End If

            If (dtRecieptType IsNot Nothing AndAlso dtRecieptType.Rows.Count > 0 AndAlso Val(lblbalanceDue.Text) < 0) Then
                If (clsDefaultConfiguration.CashRefund) Then
                    defaultRefundTender = AcceptPaymentTenderType.NegativeTenderType.CashR
                Else
                    defaultRefundTender = AcceptPaymentTenderType.NegativeTenderType.CreditVoucherI
                End If

                _tenderType = defaultRefundTender
                cboRecieptType.SelectedValue = defaultRefundTender
                cboRecieptType_SelectedIndexChanged(cboRecieptType, New System.EventArgs)

                ctrlPayCash.txtCash.Value = Val(lblbalanceDue.Text) * -1
            Else
                ctrlPayCash.txtCash.Value = Val(lblbalanceDue.Text)
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

                AddRowRecipetAmountInGrid(TenderHeadCode, EnteredAmount, CtrlGiftVoucherIssue.cmbVoucherProgram.SelectedValue, CtrlGiftVoucherIssue.ExpiryDate, String.Empty)
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
            objBLLAcceptPayment.strRemarks = ""
            If (SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.CLPPoint) Then
                Dim AcceptpassKey As Boolean = False

                If (dsRecieptType.Tables("MSTRecieptType") IsNot Nothing) Then
                    Dim valPassKeyRow = dsRecieptType.Tables("MSTRecieptType").Select("RecieptTypeCode='CLPPoint'")

                    If (valPassKeyRow.Count > 0) Then
                        ShowMessage(getValueByKey("CM068"), getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                End If

                getclpsettings()
                AcceptpassKey = CLP_Data.CLPConfigdata.Tables("CLPHeader")(0)("IsPOSPasskey") Or CLP_Data.CLPConfigdata.Tables("CLPHeader")(0)("IsOnlinePasskey")

                Dim totalPoints, AcceptPoints, CurrentAcceptPoints As Double
                If (AcceptpassKey = False AndAlso CheckAmountTextBox_NullOrInteger(strErrorMsg)) Then
                    ShowMessage(True, strErrorMsg, getValueByKey("CLAE05"))
                Else
                    Try
                        For Each Row As DataRow In dgGridReciept.DataSource.rows
                            If Row("RecieptTypeCode") = AcceptPaymentTenderType.PositiveTenderType.CLPPoint Then
                                ' AcceptPoints = AcceptPoints + Row("Amount")
                                If Not getclpsettings.Tables("CLPHeader") Is Nothing Then
                                    If CLP_Data.CLPConfigdata.Tables("CLPHeader")(0)("RedemptionType").ToString().ToLower() <> "rdt3" Then
                                        AcceptPoints = AcceptPoints + Row("Points")
                                    End If
                                End If
                            End If
                        Next
                    Catch ex As Exception
                    End Try

                    Dim dt As New DataTable
                    If Not getclpsettings.Tables("CLPHeader") Is Nothing Then
                        dt = CLP_Data.CLPConfigdata.Tables("CLPHeader")
                    End If

                    If dt.Rows.Count > 0 Then
                        Dim IsMinPointsForRedemption = dt.Rows(0)("IsMinPointsForRedemption")
                        Dim ValueMinPointsForRedemption = IIf(dt.Rows(0)("ValueMinPointsForRedemption") Is DBNull.Value, 0, dt.Rows(0)("ValueMinPointsForRedemption"))

                        If (IsMinPointsForRedemption AndAlso totalBalancePoints < ValueMinPointsForRedemption) Then
                            MessageBox.Show(String.Format(getValueByKey("LOY014"), ValueMinPointsForRedemption))
                            Return
                        End If

                        Dim IsDayLimtOnRedemption = dt.Rows(0)("IsDayLimtOnRedemption")

                        If (AcceptpassKey = False AndAlso IsDayLimtOnRedemption) Then

                            Dim ValueDayLimtRedemption = IIf(dt.Rows(0)("ValueDayLimtRedemption") Is DBNull.Value, 0, dt.Rows(0)("ValueDayLimtRedemption"))
                            Dim DayRedemptionpoints = CLP_Data.GetdayRedemptionValue(_CardNo, clsAdmin.CurrentDate)

                            If ValueDayLimtRedemption < DayRedemptionpoints + CDec(ctrlPayCash.txtCash.Text) Then
                                MessageBox.Show(String.Format(getValueByKey("LOY015"), ValueDayLimtRedemption))
                                Return
                            End If
                        End If

                        If Not AcceptpassKey Then
                            AcceptPoints = AcceptPoints + CDec(ctrlPayCash.txtCash.Text)
                            CurrentAcceptPoints = CDec(ctrlPayCash.txtCash.Text)
                        End If

                        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                            If dt(0)("RedemptionType").ToString().ToLower() = "rdt3" AndAlso AcceptpassKey = False Then
                                If Not dsRecieptType.Tables("MSTRecieptType") Is Nothing Then

                                    If dsRecieptType.Tables("MSTRecieptType").Select("RecieptTypeCode='CLPPoint'").Count > 0 Then

                                        Exit Sub
                                    End If
                                End If
                                Dim clp As New CLP_Logic
                                Dim clpVW As New DataView
                                Dim ErrMsg As String = ""
                                Dim ReedType As String = ""
                                Dim POPUP As Boolean
                                clpVW = clp.CLPRedCalc(_CardNo, ErrMsg, ReedType, dsRecieptType, Nothing, clsDefaultConfiguration.BillRoundOffAt, POPUP)

                                If clpVW Is Nothing And ReedType.ToLower() = "rdt3" And POPUP Then
                                    'MessageBox.Show("Test")
                                    Dim balpoint As Decimal = Math.Floor(CDec(getCLPcustomerdata(_CardNo, CLP_Data.CLPConfigdata.Tables("CLPHeader").Rows(0)("CLPPROGRAMID"))(0)("TotalBalancePoint")))
                                    resourceMgr.GetString("LOY006")
                                    Dim frm As New frmSpecialPrompt(getValueByKey("LOY001") + CStr(balpoint) + " ")
                                    frm.ShowTextBox = True
                                    frm.txtValue.MaxLength = 6
                                    frm.AcceptButton = frm.cmdOk
                                    frm.AllowDecimal = False
                                    frm.ShowDialog()
                                    Dim amt As Integer = frm.GetResult
                                    If CDec(balpoint >= amt) Then
                                        AddRowRecipetAmountInGrid(_tenderHeadCode, amt, True, CurrentAcceptPoints)
                                    Else
                                        MessageBox.Show(getValueByKey("LOY007"))
                                        Exit Sub
                                    End If


                                ElseIf Not clpVW Is Nothing And POPUP = False Then
                                    'IF Slab is satisfied

                                    If EnteredAmount > CDec(clpVW(0)("redemptionamount")) Then

                                        If clsDefaultConfiguration.CLP_Point_On_redeemption Then
                                            AddRowRecipetAmountInGrid(_tenderHeadCode, CDec(clpVW(0)("redemptionamount")), False, CurrentAcceptPoints)
                                        Else
                                            AddRowRecipetAmountInGrid(_tenderHeadCode, CDec(clpVW(0)("redemptionamount")), True, CurrentAcceptPoints)
                                        End If
                                        ClearTextBox()

                                    ElseIf EnteredAmount < CDec(clpVW(0)("redemptionamount")) Then
                                        Dim errorMsg As New String(String.Empty)
                                        'Dim accpay As New clsAcceptPayment
                                        'Dim details As New String(String.Empty)
                                        objBLLAcceptPayment.strRemarks = getValueByKey("LOY003") 'Need to take from resource


                                        If clsDefaultConfiguration.CLP_Point_On_redeemption Then
                                            AddRowRecipetAmountInGrid(_tenderHeadCode, CDec(clpVW(0)("redemptionamount")), False, CurrentAcceptPoints)
                                        Else
                                            AddRowRecipetAmountInGrid(_tenderHeadCode, CDec(clpVW(0)("redemptionamount")), True, CurrentAcceptPoints)
                                        End If
                                        'CalculateAmountInCurrency(True)
                                        Grid_DisplaySetting()

                                        If dtPayment.Rows.Count > 0 Then
                                            Dim dr1 As DataRow = dtPayment.Select("Tendertype='CreditVouc(I)' and Positive_Negative='*'")(0)

                                            objBLLAcceptPayment.strRemarks = getValueByKey("LOY002")
                                            dsRecieptType = objBLLAcceptPayment.AddRowRecieptAmountInGrid_CreditNote(clsAdmin.CreditValidDays, dr1("TenderHeadName").ToString(), clsAdmin.CurrencyCode, dr1("TenderHeadCode").ToString(), EnteredAmount, " ", errorMsg, dr1("TenderType").ToString(), clsAdmin.SiteCode, clsAdmin.CVProgram, True)
                                            dsRecieptType.Tables("MSTRecieptType").Rows(dsRecieptType.Tables("MSTRecieptType").Rows.Count - 1)("IssuedForCLP") = True
                                            dtRecieptType = dsRecieptType.Tables("MSTRecieptType")
                                            dgGridReciept.DataSource = dtRecieptType
                                            'Dim i As Integer = dgGridReciept.RowSel
                                            'dgGridReciept.Rows(dgGridReciept.BottomRow).Item("Number") = "Against loyalty"
                                            ClearTextBox()
                                            IsTotalAmountReceived()
                                        End If

                                    Else

                                        If clsDefaultConfiguration.CLP_Point_On_redeemption Then
                                            AddRowRecipetAmountInGrid(_tenderHeadCode, CDec(clpVW(0)("redemptionamount")), False, CurrentAcceptPoints)
                                        Else
                                            AddRowRecipetAmountInGrid(_tenderHeadCode, CDec(clpVW(0)("redemptionamount")), True, CurrentAcceptPoints)
                                        End If
                                        ClearTextBox()
                                    End If

                                Else
                                    MessageBox.Show(getValueByKey(ErrMsg), getValueByKey("LOY005"))
                                    Exit Sub
                                End If


                            ElseIf CLP_Data.CLPConfigdata.Tables("CLPHeader")(0)("RedemptionType").ToString().ToLower() = "rdt2" Then
                                '---- Changed by Mahesh Case 1 AcceptpassKey is on then no need to check total balance point else must check redeem point must be less or equal to totalBalancePoint 
                                AcceptPoints = Val(ctrlPayCash.txtCash.Text)
                                If AcceptpassKey Then
                                    Dim key = CLP_Data.GetPassKeydata(ctrlPayCash.txtCash.Text.Trim())
                                    If key Is Nothing Then
                                        MessageBox.Show("Please enter correct passkey")
                                        Exit Sub
                                    End If

                                    If key.ExpiryDateTime < System.DateTime.Now Then
                                        MessageBox.Show("Passkey is expired")
                                        Exit Sub
                                    End If

                                    If key.IsRedeemed Then
                                        MessageBox.Show("Passkey is already redeemed")
                                        Exit Sub
                                    End If
                                    AcceptPoints = key.PasskeyValue
                                    If CLP_Data.CLPConfigdata.Tables("CLPHeader").Rows.Count > 0 Then
                                        AcceptPoints = (AcceptPoints / CLP_Data.CLPConfigdata.Tables("CLPRedDetails")(0)("Points")) * CLP_Data.CLPConfigdata.Tables("CLPRedDetails")(0)("AmtValue")
                                    End If

                                    objBLLAcceptPayment.strRemarks = key.Passkey
                                    CLP_Data._SlabPoints = key.PasskeyValue
                                    AddRowRecipetAmountInGrid(_tenderHeadCode, AcceptPoints, True, CurrentAcceptPoints)
                                Else
                                    If CDec(getCLPcustomerdata(_CardNo, CLP_Data.CLPConfigdata.Tables("CLPHeader").Rows(0)("CLPPROGRAMID"))(0)("TotalBalancePoint")) >= AcceptPoints Then
                                        If CLP_Data.CLPConfigdata.Tables("CLPHeader").Rows.Count > 0 Then
                                            AcceptPoints = (AcceptPoints / CLP_Data.CLPConfigdata.Tables("CLPRedDetails")(0)("Points")) * CLP_Data.CLPConfigdata.Tables("CLPRedDetails")(0)("AmtValue")
                                        End If
                                        AddRowRecipetAmountInGrid(_tenderHeadCode, AcceptPoints, True, CurrentAcceptPoints)
                                    Else
                                        MessageBox.Show(getValueByKey("LOY006"), getValueByKey("LOY005"))
                                    End If
                                End If
                            ElseIf CLP_Data.CLPConfigdata.Tables("CLPHeader")(0)("RedemptionType").ToString().ToLower() = "rdt1" Then
                                Dim clp As New CLP_Logic
                                Dim clpVW As New DataView
                                Dim ErrMsg As String = ""
                                Dim ReedType As String = ""
                                Dim POPUP As Boolean
                                If AcceptpassKey Then
                                    Dim key = CLP_Data.GetPassKeydata(ctrlPayCash.txtCash.Text.Trim())
                                    If key Is Nothing Then
                                        MessageBox.Show(getValueByKey("LOY011"))
                                        Exit Sub
                                    End If

                                    If key.ExpiryDateTime < System.DateTime.Now Then
                                        MessageBox.Show(getValueByKey("LOY012"))
                                        Exit Sub
                                    End If

                                    If key.IsRedeemed Then
                                        MessageBox.Show(getValueByKey("LOY012"))
                                        Exit Sub
                                    End If
                                    AcceptPoints = key.PasskeyValue
                                    objBLLAcceptPayment.strRemarks = key.Passkey
                                End If

                                clpVW = clp.CLPRedCalc(_CardNo, ErrMsg, ReedType, dsRecieptType, dtitems, clsDefaultConfiguration.BillRoundOffAt, POPUP, AcceptPoints)
                                If clpVW IsNot Nothing AndAlso POPUP = False Then
                                    AddRowRecipetAmountInGrid(_tenderHeadCode, clpVW(0)("Amount"), True, AcceptPoints)
                                    ClearTextBox()

                                    Grid_DisplaySetting()

                                    If dtPayment.Rows.Count > 0 Then
                                        Dim dr1 As DataRow = dtPayment.Select("Tendertype='CreditVouc(I)' and Positive_Negative='*'")(0)

                                        objBLLAcceptPayment.strRemarks = getValueByKey("LOY002")
                                        dsRecieptType = objBLLAcceptPayment.AddRowRecieptAmountInGrid_CreditNote(clsAdmin.CreditValidDays, dr1("TenderHeadName").ToString(), clsAdmin.CurrencyCode, dr1("TenderHeadCode").ToString(), clpVW(0)("CVAmount"), " ", ErrMsg, dr1("TenderType").ToString(), clsAdmin.SiteCode, clsAdmin.CVProgram, True)

                                        Dim rowIndex = dsRecieptType.Tables("MSTRecieptType").Rows.Count - 1
                                        dsRecieptType.Tables("MSTRecieptType").Rows(rowIndex)("IssuedForCLP") = True
                                        dsRecieptType.Tables("MSTRecieptType").Rows(rowIndex)("Points") = clpVW(0)("Points")
                                        dsRecieptType.Tables("MSTRecieptType").Rows(rowIndex)("CVAmountAgainstPoint") = clpVW(0)("CVAmount")

                                        dtRecieptType = dsRecieptType.Tables("MSTRecieptType")
                                        dgGridReciept.DataSource = dtRecieptType
                                        'Dim i As Integer = dgGridReciept.RowSel
                                        'dgGridReciept.Rows(dgGridReciept.BottomRow).Item("Number") = "Against loyalty"
                                        ClearTextBox()
                                        IsTotalAmountReceived()
                                        Grid_DisplaySetting()
                                    End If

                                Else
                                    If (String.Equals("LOY020", ErrMsg)) Then
                                        MessageBox.Show(String.Format(getValueByKey(ErrMsg), AcceptPoints))
                                        ctrlPayCash.txtCash.Text = String.Empty
                                        ctrlPayCash.txtCash.Value = String.Empty
                                        Exit Sub
                                    Else
                                        MessageBox.Show(getValueByKey(ErrMsg))
                                        ctrlPayCash.txtCash.Text = String.Empty
                                        ctrlPayCash.txtCash.Value = String.Empty
                                        Exit Sub
                                    End If
                                End If

                            End If

                        End If
                        'If objBLLAcceptPayment.ValidateCLP(_CardNo, clsAdmin.CLPProgram, AcceptPoints) = False Then
                        '    ShowMessage(getValueByKey("ACP027"), "ACP027 - " & getValueByKey("CLAE04"))
                        '    Exit Sub
                        'End If


                    Else
                        MessageBox.Show(getValueByKey("LOY008"))
                    End If

                    If (CLP_Data.CLPConfigdata.Tables("CLPHeader")(0)("IsPOSPasskey")) Then
                        ctrlPayCash.txtCash.DataType = System.Type.GetType("System.String")
                        lblAmount.Text = getValueByKey("LOY019")
                    End If

                End If
            ElseIf (SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.Cash Or SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.Credit _
                    Or SelectedReceiptTypeCode.ToUpper() = AcceptPaymentTenderType.PositiveTenderType.Neft.ToUpper() Or _
                    SelectedReceiptTypeCode.ToUpper() = AcceptPaymentTenderType.PositiveTenderType.Rtgs.ToUpper() Or _
                    SelectedReceiptTypeCode.ToUpper() = AcceptPaymentTenderType.PositiveTenderType.MealPass.ToUpper() _
                    Or SelectedReceiptTypeCode.ToUpper() = AcceptPaymentTenderType.PositiveTenderType.Paytm.ToUpper() Or _
                    SelectedReceiptTypeCode.ToUpper() = AcceptPaymentTenderType.PositiveTenderType.QuickWallet.ToUpper() Or _
                    SelectedReceiptTypeCode.ToUpper() = AcceptPaymentTenderType.PositiveTenderType.Sodexo.ToUpper() Or _
                    SelectedReceiptTypeCode.ToUpper() = AcceptPaymentTenderType.PositiveTenderType.TicketRestaurant.ToUpper() Or _
                    SelectedReceiptTypeCode.ToUpper() = AcceptPaymentTenderType.PositiveTenderType.OnlinePayment.ToUpper() Or _
                    SelectedReceiptTypeCode.ToUpper() = AcceptPaymentTenderType.PositiveTenderType.Others.ToUpper() Or _
                    SelectedReceiptTypeCode.ToUpper() = AcceptPaymentTenderType.PositiveTenderType.Payso.ToUpper() Or _
                    SelectedReceiptTypeCode.ToUpper() = AcceptPaymentTenderType.PositiveTenderType.SodexoCards.ToUpper() Or _
                    SelectedReceiptTypeCode.ToUpper() = AcceptPaymentTenderType.PositiveTenderType.MobiKwik.ToUpper() Or _
                    SelectedReceiptTypeCode.ToUpper() = AcceptPaymentTenderType.PositiveTenderType.SodexoCpn.ToUpper() Or _
                    SelectedReceiptTypeCode.ToUpper() = AcceptPaymentTenderType.PositiveTenderType.ZomatoCash.ToUpper() Or _
                    SelectedReceiptTypeCode.ToUpper() = AcceptPaymentTenderType.PositiveTenderType.SwiggyCash.ToUpper() Or _
                    SelectedReceiptTypeCode.ToUpper() = AcceptPaymentTenderType.PositiveTenderType.ScootsyOnline.ToUpper() Or _
                    SelectedReceiptTypeCode.ToUpper() = AcceptPaymentTenderType.PositiveTenderType.ScootsyCash.ToUpper() Or _
                    SelectedReceiptTypeCode.ToUpper() = AcceptPaymentTenderType.PositiveTenderType.UberEatsOnline.ToUpper() Or _
                    SelectedReceiptTypeCode.ToUpper() = AcceptPaymentTenderType.PositiveTenderType.UberEatsCash.ToUpper() Or _
                    SelectedReceiptTypeCode.ToUpper() = AcceptPaymentTenderType.PositiveTenderType.SwiggyOnline.ToUpper() Or _
                    SelectedReceiptTypeCode.ToUpper() = AcceptPaymentTenderType.PositiveTenderType.ZomatoOnline.ToUpper() Or _
                    SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.FoodPandaCash Or _
                    SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.FoodPandaOnline Or _
                    SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.PhonePe Or _
                    SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.DynTenderType) Then 'Or SelectedReceiptTypeCode = AcceptPaymentTenderType.NegativeTenderType.Cash) Then 'Or SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.ParkingCoupen) Then

                If IsChangeTender Then
                    If SelectedReceiptTypeCode = "Credit" Then
                        If (TotalPick - TotalReceivedAmount) < EnteredAmount Then
                            ShowMessage(String.Format(getValueByKey("ACP036"), TotalPick), "ACP015 - " & getValueByKey("CLAE04"))
                            Exit Sub
                        End If
                    End If
                End If
                If SelectedReceiptTypeCode = "Neft" Or SelectedReceiptTypeCode = "Rtgs" Then 'vipin
                    If String.IsNullOrEmpty(txtReferenceNo.Text) Then
                        ShowMessage("Please Enter Reference Number", "ACP015 - " & getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                End If
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
                                TotalCustomerPadiAmount = Rema
                                Rema = EnteredAmount - (Rema - TotalBillAmount)

                                'MessageOnAnotherTheared()
                                If isChangeTenderMode Then ''For Change Tender Mode
                                    If (MyRound(TotalBillAmount - TotalReceivedAmount, clsDefaultConfiguration.BillRoundOffAt) >= EnteredAmount) Then 'bug no 898
                                        dsRecieptType = objBLLAcceptPayment.AddRowRecipetAmountInGrid_Cash(_tenderHeadName, TenderHeadCode, Rema, SelectedCurrencyIndex, clsAdmin.CurrencyCode, SelectedCurrencySymbol, SelectedReceiptTypeCode, cboCurrency.SelectedValue)
                                        dtRecieptType = dsRecieptType.Tables("MSTRecieptType")
                                        dgGridReciept.DataSource = dtRecieptType
                                        'ctrlPayCash.txtCash.Text = 0
                                        Grid_DisplaySetting()
                                        ClearTextBox()
                                        ctrlPayCash.txtCash.Focus()
                                    Else
                                        ShowMessage(getValueByKey("ACP009"), "ACP009 - " & getValueByKey("CLAE04"))
                                        ctrlPayCash.txtCash.Focus()
                                        Exit Sub
                                    End If
                                Else
                                    dsRecieptType = objBLLAcceptPayment.AddRowRecipetAmountInGrid_Cash(_tenderHeadName, TenderHeadCode, Rema, SelectedCurrencyIndex, clsAdmin.CurrencyCode, SelectedCurrencySymbol, SelectedReceiptTypeCode, cboCurrency.SelectedValue)
                                    dtRecieptType = dsRecieptType.Tables("MSTRecieptType")
                                    dgGridReciept.DataSource = dtRecieptType
                                    'ctrlPayCash.txtCash.Text = 0
                                    Grid_DisplaySetting()
                                    ClearTextBox()
                                End If
                                'dsRecieptType = objBLLAcceptPayment.AddRowRecipetAmountInGrid_Cash(_tenderHeadName, TenderHeadCode, Rema, SelectedCurrencyIndex, clsAdmin.CurrencyCode, SelectedCurrencySymbol, SelectedReceiptTypeCode, cboCurrency.SelectedValue)
                                'dtRecieptType = dsRecieptType.Tables("MSTRecieptType")
                                'dgGridReciept.DataSource = dtRecieptType
                                ''ctrlPayCash.txtCash.Text = 0
                                'Grid_DisplaySetting()
                                'ClearTextBox()
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
                                If isChangeTenderMode Then ''For Change Tender Mode
                                    If (MyRound(TotalBillAmount - TotalReceivedAmount, clsDefaultConfiguration.BillRoundOffAt) < EnteredAmount) Then 'bug no 898
                                        ShowMessage(getValueByKey("ACP009"), "ACP009 - " & getValueByKey("CLAE04"))
                                        ctrlPayCash.txtCash.Focus()
                                        Exit Sub
                                    End If
                                End If
                                dsRecieptType = objBLLAcceptPayment.AddRowRecipetAmountInGrid_Cash(_tenderHeadName, TenderHeadCode, EnteredAmount, SelectedCurrencyIndex, clsAdmin.CurrencyCode, SelectedCurrencySymbol, SelectedReceiptTypeCode, cboCurrency.SelectedValue)
                                dtRecieptType = dsRecieptType.Tables("MSTRecieptType")
                                dgGridReciept.DataSource = dtRecieptType
                                '' added by ketan  Outstanding  savoy changes
                                If clsDefaultConfiguration.IsSavoy Then
                                    If TenderHeadCode = "Credit Sales" Then
                                        PaymentTermNameId = CtrlPayTerm.SelectedValue
                                    End If
                                End If
                                Grid_DisplaySetting()
                                ClearTextBox()
                            End If
                            ctrlPayCash.txtCash.Clear()
                        Else
                            dsRecieptType = objBLLAcceptPayment.AddRowRecipetAmountInGrid_Cash(_tenderHeadName, TenderHeadCode, EnteredAmount, SelectedCurrencyIndex, clsAdmin.CurrencyCode, SelectedCurrencySymbol, SelectedReceiptTypeCode, cboCurrency.SelectedValue, CtrlPayTerm.Text)
                            dtRecieptType = dsRecieptType.Tables("MSTRecieptType")
                            dgGridReciept.DataSource = dtRecieptType
                            Grid_DisplaySetting()
                            ClearTextBox()
                            If clsDefaultConfiguration.IsSavoy Then
                                If TenderHeadCode = "Credit Sales" Then
                                    PaymentTermNameId = CtrlPayTerm.SelectedValue
                                End If
                            End If
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
                                AddRowRecipetAmountInGrid(SelectedReceiptTypeCode, EnteredAmount, "102341234", "01/02/2010", String.Empty)
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

                    If (MyRound(TotalBillAmount - TotalReceivedAmount, clsDefaultConfiguration.BillRoundOffAt) >= EnteredAmount) Then
                        If (IsSwipeCreditCard) Then
                            If Not (IsRecieptAmountClose) Then
                                AddRowRecipetAmountInGrid(SelectedReceiptTypeCode, EnteredAmount, "102341234", "01/02/2010", String.Empty)
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
            ctrlPayCash.txtCash.Value = Math.Abs(Val(lblbalanceDue.Text.Replace(",", "")))
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
            Dim NOCLP As Boolean = False
            If objBLLAcceptPayment.FnGiftVoucherValidate(ctrlPayCheque.txtChequeNo.Text, msg, Amt, ExpiryDate, , , clsAdmin.SiteCode) = False Then
                msg = msg.Replace("Gift Voucher", "Credit Voucher")
                ShowMessage(msg, getValueByKey("CLAE04"))
                ctrlPayCheque.txtChequeNo.Focus()
                Exit Sub
            End If

            If clsDefaultConfiguration.CLP_Point_On_redeemption Then
                NOCLP = False
            End If
            If isChangeTenderMode Then
                If (MyRound(TotalBillAmount - TotalReceivedAmount, clsDefaultConfiguration.BillRoundOffAt) < EnteredAmount) Then
                    ShowMessage(getValueByKey("ACP009"), "ACP009 - " & getValueByKey("CLAE04"))
                    ctrlPayCash.txtCash.Focus()
                    Exit Sub
                End If
            End If
            ctrlPayCash.txtCash.Value = Amt
            'ctrlPayCheque.dtpExpiryDate.Text = ExpiryDate
            ctrlPayCheque.dtpExpiryDate.Value = ExpiryDate
            dsRecieptType = objBLLAcceptPayment.AddRowRecieptAmountInGrid_CreditNote(clsAdmin.CreditValidDays, _tenderHeadName, clsAdmin.CurrencyCode, TenderHeadCode, EnteredAmount, ctrlPayCheque.txtChequeNo.Text, errorMsg, SelectedReceiptTypeCode, clsAdmin.SiteCode, clsAdmin.CVProgram, NOCLP)
            dtRecieptType = dsRecieptType.Tables("MSTRecieptType")
            dgGridReciept.DataSource = dtRecieptType
            Grid_DisplaySetting()
            If Not (String.IsNullOrEmpty(errorMsg)) Then
                ShowMessage(errorMsg, getValueByKey("CLAE05"))
            End If

            If (Val(lblbalanceDue.Text) > 0) Then
                _tenderType = AcceptPaymentTenderType.PositiveTenderType.Cash
                _tenderHeadName = AcceptPaymentTenderType.PositiveTenderType.Cash
                _tenderHeadCode = AcceptPaymentTenderType.PositiveTenderType.Cash
                cboRecieptType.SelectedValue = AcceptPaymentTenderType.PositiveTenderType.Cash
                cboRecieptType_SelectedIndexChanged(cboRecieptType, New System.EventArgs)
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
            '--- Code Added(if CheckVoucherSiteMapForVoucjherType) By Mahesh for if vouchersitemap dont have voucher code for this site ... 
            If CheckVoucherSiteMapForVoucherType(clsAdmin.SiteCode, TenderHeadCode) Then
                dsRecieptType = objBLLAcceptPayment.AddRowRecieptAmountInGrid_CreditNote(clsAdmin.CreditValidDays, _tenderHeadName, clsAdmin.CurrencyCode, TenderHeadCode, EnteredAmount, " ", errorMsg, SelectedReceiptTypeCode, clsAdmin.SiteCode, clsAdmin.CVProgram)
                dtRecieptType = dsRecieptType.Tables("MSTRecieptType")
                dgGridReciept.DataSource = dtRecieptType
                Grid_DisplaySetting()
                If Not (String.IsNullOrEmpty(errorMsg)) Then
                    ShowMessage(errorMsg, getValueByKey("CLAE05"))
                End If
            Else
                ShowMessage(getValueByKey("CM062"), "CM062 - " & getValueByKey("CLAE04"))
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
            dgGridReciept.Refresh()
            IsTotalAmountReceived()

            dgGridReciept.AllowEditing = False
            dgGridReciept.Cols("RecieptType").Caption = "Tender Type" '' modCommonFunc.getValueByKey("frmnacceptpayment.dggridreciept.RecieptType")   'col index 1
            dgGridReciept.AutoSizeCol(1, 10)

            dgGridReciept.Cols("Amount").Caption = "Amount"     '''modCommonFunc.getValueByKey("frmnacceptpayment.dggridreciept.asabove_0")     'col index 3
            dgGridReciept.AutoSizeCol(3, 10)

            dgGridReciept.Cols("BankName").Caption = "Bank Name"   '' modCommonFunc.getValueByKey("frmnacceptpayment.dggridreciept.asabove_1")     'col index 5
            dgGridReciept.Cols("BankName").Width = 170

            dgGridReciept.Cols("CardNo").Visible = True                                                                           'col index 6
            dgGridReciept.Cols("CardNo").Caption = "Card No."    ''modCommonFunc.getValueByKey("frmnacceptpayment.dggridreciept.date")
            dgGridReciept.AutoSizeCol(6, 10)

            'dgGridReciept.Cols(8).Visible = False
            dgGridReciept.Cols("NEFTReferenceNo").Caption = "NEFT Ref no."
            dgGridReciept.AutoSizeCol(6, 10)

            dgGridReciept.Cols("RTGSReferenceNo").Caption = "RTGS Ref no."
            dgGridReciept.AutoSizeCol(6, 10)


            'dgGridReciept.Cols(5).Visible = False
            dgGridReciept.Cols("ChequeNo").Caption = "Cheque No."
            dgGridReciept.AutoSizeCol(6, 10)

            'dgGridReciept.Cols(6).Visible = False
            dgGridReciept.Cols("MICRNo").Caption = "MICR No."
            dgGridReciept.AutoSizeCol(6, 10)

            'dgGridReciept.Cols(7).Visible = False
            dgGridReciept.Cols("ChequeDate").Caption = "Cheque Date"
            dgGridReciept.AutoSizeCol(6, 10)



            'dgGridReciept.Cols(9).Visible = False
            'dgGridReciept.Cols(9).Caption = "Rtgs Ref no."
            'dgGridReciept.AutoSizeCol(6, 10)



            'dgGridReciept.Cols(10).Visible = False
            dgGridReciept.Cols("CreditVoucherNo").Caption = "Credit voucher no."
            dgGridReciept.Cols("CreditVoucherNo").Width = 145

            dgGridReciept.Cols("Remarks").Visible = True
            dgGridReciept.Cols("Remarks").Caption = "Remarks"
            dgGridReciept.AutoSizeCol(6, 10)



            'added by Khusrao Adil
            'for savoy
            If clsDefaultConfiguration.IsSavoy Then
                dgGridReciept.Cols("PaymentTermName").Caption = "Payment Term Name"
                dgGridReciept.Cols("PaymentTermName").Visible = True
            Else
                dgGridReciept.Cols("PaymentTermName").Visible = False
            End If
            'dtRecieptType()
            '' added by nikhil
            dgGridReciept.Cols("Reciept").Visible = False
            dgGridReciept.Cols("Number").Visible = False
            dgGridReciept.Cols("Date").Visible = False
            dgGridReciept.Cols("RefNo_3").Visible = False
            dgGridReciept.Cols("RefNo_4").Visible = False
            ' dgGridReciept.Cols("Amount").Visible = False

            dgGridReciept.Cols("SrNo").Visible = False              'col index 0
            dgGridReciept.Cols("TenderType").Visible = False       'col index 2
            dgGridReciept.Cols("AmountInCurrency").Visible = False  'col index 4
            dgGridReciept.Cols("RecieptTypeCode").Visible = False   'col index 7
            dgGridReciept.Cols("ExchangeRate").Visible = False      'col index 8
            dgGridReciept.Cols("CurrencyCode").Visible = False      'col index 9
            dgGridReciept.Cols("BankAccNo").Visible = False          'col index 12    
            dgGridReciept.Cols("NOCLP").Visible = False             'col index 13
            dgGridReciept.Cols("IssuedForCLP").Visible = False      'col index 14

            If (dgGridReciept.Cols.Contains("Points")) Then
                dgGridReciept.Cols("Points").Visible = False
            End If
            If (dgGridReciept.Cols.Contains("CVAmountAgainstPoint")) Then
                dgGridReciept.Cols("CVAmountAgainstPoint").Visible = False
            End If
            dgGridReciept.Font = New Font("Verdana", 10, FontStyle.Regular)
            dgGridReciept.Styles.SelectedColumnHeader.Font = New Font("Verdana", 10, FontStyle.Bold)
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
    Private Sub DisplaySettings_CurrencyCalculation(ByVal isVisible As Boolean)
        'pnlBoxCurrency.Visible = True
        lblCalCurrencyHeader.Visible = isVisible
        lblCurrencyBillAmount.Visible = isVisible
        lblCalCurrencyBillAmount.Visible = isVisible
        lblCurrencyTotalReciept.Visible = isVisible
        lblCalCurrencyTotalReciepts.Visible = isVisible
        lblCurrencyBalanceDue.Visible = isVisible
        lblCalCurrencyBalanceDue.Visible = isVisible

        If _ParentForm = "SalesOrder" Then
            lblCurrencyMinimumBalAmt.Visible = isVisible
            lblCalCurrencyMiniBalDue.Visible = isVisible
        End If

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
            ' ctrlPayCredit.txtAuthCode.Text = String.Empty
            If CtrlPayCreditCheque.Visible = False Then
                ctrlPayCheque.txtChequeNo.Value = String.Empty
            End If
            ctrlPayCredit.txtCreditCardNo.Value = String.Empty
            ' ctrlPayCredit.txtSlipNO.Text = String.Empty
            txtReferenceNo.Text = String.Empty
            TxtRemark.Text = String.Empty

            ctrlPayCash.txtCash.Focus()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ClearChequeTextBox()
        Try
            If Not ctrlPayCash.txtCash.Value Is Nothing AndAlso lblbalanceDue.Text = 0 Then
                ctrlPayCash.txtCash.Value = String.Empty
                lblSign.Text = String.Empty
            End If

            CtrlChequeDetails.txtChequeNo.Value = String.Empty
            CtrlChequeDetails.dtChequeDate.Value = Nothing
            CtrlChequeDetails.txtMicrNumber.Value = String.Empty
            CtrlChequeDetails.cmbBankName.SelectedIndex = -1

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
    Protected Sub AddRowRecipetAmountInGrid(ByVal strRecieptType As String, ByVal strAmount As String, Optional ByVal NOCLP As Boolean = False, Optional clpPoints As Double = 0)
        If Not (IsRecieptAmountClose) Then
            dsRecieptType = objBLLAcceptPayment.AddRowRecipetAmountInGrid(_tenderHeadName, strRecieptType, strAmount, SelectedReceiptTypeCode, clpPoints, NOCLP)

            dtRecieptType = dsRecieptType.Tables("MSTRecieptType")
            If NOCLP Then
                If Not dtRecieptType.Columns.Contains("Points") Then
                    dtRecieptType.Columns.Add(New DataColumn("Points", Type.GetType("System.Double")))
                End If
                dtRecieptType.Columns("Points").Caption = String.Empty 'Points

                If Not dtRecieptType.Columns.Contains("CVAmountAgainstPoint") Then
                    dtRecieptType.Columns.Add(New DataColumn("CVAmountAgainstPoint", GetType(Decimal)))
                End If
                dtRecieptType.Columns("CVAmountAgainstPoint").Caption = String.Empty 'Amount Against Point

                For Each drClp As DataRow In dtRecieptType.Select("RecieptTypeCode='CLPPoint'")
                    If (Not String.IsNullOrEmpty(drClp("RefNo_3").ToString())) Then
                        drClp("Points") = CDbl(drClp("RefNo_3"))
                        drClp("IssuedForCLP") = True
                    End If
                Next
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

    Protected Sub AddRowRecipetAmountInGrid(ByVal strRecieptType As String, ByVal strAmount As Decimal, ByVal strNumber As String, ByVal dtDate As Object, ByVal bankName As String)
        If Not (IsRecieptAmountClose) Then
            strAmount = FormatCurrency(strAmount, 2) 'vipin
            'Added by Rohit for CR-5938
            objBLLAcceptPayment.IsNewSalesOrder = clsDefaultConfiguration.IsNewSalesOrder
            objBLLAcceptPayment.dDueDate = dDueDate
            objBLLAcceptPayment.strRemarks = strRemarks
            'added by khusrao adil on 26-07-2018 for innviti with card functionality point _ natural client
            If strRecieptType.ToLower = "card inv" Then
                strRecieptType = "CreditCard"  'modified by khusrao adil on 07-09-2018 for natural client
                _tenderHeadName = "Credit Card"
                dtInnoviti.Clear()
                Dim InnvoitiLineNo = 1
                Dim TenderSequenceLineNo = 1
                If dsRecieptType.Tables.Cast(Of DataTable)().Any(Function(table) table.Rows.Count <> 0) Then
                    If dsRecieptType.Tables(0).Rows.Count > 0 Then
                        If PaymentType = clsAcceptPayment.PaymentType.EditBill Then
                            TenderSequenceLineNo = dsRecieptType.Tables(0).Rows.Count + 2
                        Else
                            TenderSequenceLineNo = dsRecieptType.Tables(0).Rows.Count + 1
                        End If
                    End If
                Else
                    If PaymentType = clsAcceptPayment.PaymentType.EditBill Then
                        TenderSequenceLineNo = dsRecieptType.Tables(0).Rows.Count + 2
                        '                    Else
                        ' TenderSequenceLineNo = dsRecieptType.Tables(0).Rows.Count + 1
                    End If
                End If
                Dim dr As DataRow = dtInnoviti.NewRow
                dr("TenderSequenceLineNo") = TenderSequenceLineNo
                dr("LineNo") = InnvoitiLineNo
                dr("TenderType") = strRecieptType
                dr("BillNo") = Billno
                dr("AmountTendered") = strAmount
                dr("CardNo") = ""
                dr("RetrievalReferenceNumber") = ""
                dr("InnvoitiApplicable") = True
                dtInnoviti.Rows.Add(dr)
            End If
            If CtrlChequeDetails.cmbBankName.SelectedText <> "" Then
                objBLLAcceptPayment.BankName = CtrlChequeDetails.cmbBankName.SelectedText
            End If
            If CtrlChequeDetails.txtChequeNo.Text.Trim <> "" Then
                objBLLAcceptPayment.ChequeNo = CtrlChequeDetails.txtChequeNo.Text
            End If
            If Convert.ToString(CtrlChequeDetails.dtChequeDate.Value) <> "" Then
                objBLLAcceptPayment.ChequeDate = Convert.ToString(CtrlChequeDetails.dtChequeDate.Value)
            End If
            If CtrlChequeDetails.txtMicrNumber.Text.Trim <> "" Then
                objBLLAcceptPayment.MICRNo = CtrlChequeDetails.txtMicrNumber.Text
            End If

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
                Dim strSum As Object = Decimal.Zero
                strSum = dtRecieptType.Compute("Sum(Amount)", " ")
                If (strSum Is DBNull.Value) Then
                    strSum = 0
                End If

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
                    ireturnAmount = MyRound(ireturnAmount, clsDefaultConfiguration.BillRoundOffAt)
                    If (CDbl(strSum) > TotalBillAmount) Then
                        IsRecieptAmountClose = False
                        filter = "Positive_Negative='*' OR Positive_Negative='-'"
                        dvPayment.RowFilter = filter
                        If Not cboRecieptType.SelectedValue Is Nothing Then
                            cboRecieptType_SelectedIndexChanged(cboRecieptType, New System.EventArgs)
                        End If
                        cboRecieptType.SelectedIndex = 0
                        cboRecieptType_Leave(cboRecieptType, New System.EventArgs)
                        cboCurrency.SelectedValue = clsAdmin.CurrencyCode
                        cboRecieptType_Leave(cboRecieptType, New System.EventArgs)

                        If (clsDefaultConfiguration.CashRefund) Then
                            defaultRefundTender = AcceptPaymentTenderType.NegativeTenderType.CashR
                        Else
                            defaultRefundTender = AcceptPaymentTenderType.NegativeTenderType.CreditVoucherI
                        End If

                        _tenderType = defaultRefundTender
                        cboRecieptType.SelectedValue = defaultRefundTender
                        cboRecieptType_SelectedIndexChanged(cboRecieptType, New System.EventArgs)

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
                        ctrlPayCash.txtCash.Value = Convert.ToDecimal(lblbalanceDue.Text)
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
            'strSum = -strSum
            strSum = dtRecieptType.Compute("Sum(Amount)", " ")
            lblTotalReciept.Text = FormatNumber(-strSum, 2)
            lblbalanceDue.Text = FormatNumber(Decimal.Subtract(TotalBillAmount, CDbl(strSum)), 2)
            If (TotalBillAmount >= CDbl(strSum)) Then
                Dim ireturnAmount As Decimal = Decimal.Subtract(TotalBillAmount, CDbl(strSum))
                If (CDbl(strSum) < TotalBillAmount) Then
                    filter = "Positive_Negative='*' OR Positive_Negative='-'"
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
        'added two more parameter to FormatNumber function in order to show proper value in payment window in case of credit sale return adjustment
        ctrlPayCash.txtCash.Value = FormatNumber(decBalanceDue, 2, , TriState.True)
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
            ' added by Khusrao Adil
            ' for savoy
            If clsDefaultConfiguration.IsSavoy Then
                If cboRecieptType.SelectedValue = "Credit Sales" AndAlso CtrlPayTerm.SelectedValue = "" AndAlso CtrlPayTerm.SelectedValue = Nothing Then
                    ShowMessage("Paymant term not provided", "Information")
                    Exit Sub
                Else
                    InsertReceiptCashDetails()
                End If
            Else
                InsertReceiptCashDetails()
            End If
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

    Function CheckCreditSale() As Boolean
        'dtRecieptType
        CheckCreditSale = False
        '---- Credit sale amount cannot greater then Minimum Balance 
        If (SelectedReceiptTypeCode = AcceptPaymentTenderType.PositiveTenderType.Credit) Then
            Dim minbalance As Double
            If Double.TryParse(lblCalMinBalDue.Text, minbalance) Then
                If minbalance < Val(ctrlPayCash.txtCash.Text) Then
                    'strErrorMsg = "Enter valid amount"
                    strErrorMsg = getValueByKey("EM005")
                    CheckCreditSale = True
                End If
            End If
        End If


    End Function


    Private Sub txtRemarks_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If (e.KeyCode = Keys.Enter) And ctrlPayCheque.Visible = True Then
            InsertChequeDetails()
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
    'added by Khusrao Adil
    'for savoy
    Private Sub PaymentTermCombo(ByVal _condition As Boolean)
        CtrlPayTerm.Visible = _condition
        LblPayTerm.Visible = _condition
        If Not _condition Then
            CtrlPayTerm.Text = ""
        End If
    End Sub
    ''' <summary>
    '''  Event of ReceiptType  combobox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboRecieptType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRecieptType.SelectedValueChanged
        'Rakesh-24.09.2013: Issue-7507
        ClearTextBox()
        Dim TempAllowInnovitiPayment As Boolean = False 'added by khusrao adil on 26-07-2018
        rtxtSwipeCard.Enabled = False
        lblAmount.Text = getValueByKey("frmnacceptpayment.lblamount")
        ctrlPayCash.txtCash.Size = New System.Drawing.Size(72, 21)
        ctrlPayCash.txtCash.MaxLength = 20

        If (C1SizerPaymentTypes.Size.Width < 800) Then
            C1SizerPaymentTypes.Grid.Columns(0).Size = 0.6 * C1SizerPaymentTypes.Size.Width
            C1SizerPaymentTypes.Grid.Columns(1).Size = 0.4 * C1SizerPaymentTypes.Size.Width
        End If
        PaymentTermCombo(False)
        Dim dtTend As DataTable = ObjComm.GetTenderTypes(clsAdmin.SiteCode)
        For Each dr As DataRow In dtTend.Select("TenderHeadCode='" & cboRecieptType.SelectedValue & "'", "", DataViewRowState.CurrentRows)
            AcceptPaymentTenderType.PositiveTenderType.DynTenderType = dr("TenderType").ToString()
        Next
        Select Case (SelectedReceiptTypeCode)
            Case AcceptPaymentTenderType.PositiveTenderType.Cash, AcceptPaymentTenderType.PositiveTenderType.Neft, _
                AcceptPaymentTenderType.PositiveTenderType.Rtgs, AcceptPaymentTenderType.PositiveTenderType.MealPass, _
                AcceptPaymentTenderType.PositiveTenderType.Paytm, AcceptPaymentTenderType.PositiveTenderType.QuickWallet, _
                AcceptPaymentTenderType.PositiveTenderType.Sodexo, AcceptPaymentTenderType.PositiveTenderType.TicketRestaurant, _
                AcceptPaymentTenderType.PositiveTenderType.OnlinePayment, AcceptPaymentTenderType.PositiveTenderType.Others, _
                AcceptPaymentTenderType.PositiveTenderType.Payso, AcceptPaymentTenderType.PositiveTenderType.SodexoCards, _
                AcceptPaymentTenderType.PositiveTenderType.MobiKwik, AcceptPaymentTenderType.PositiveTenderType.SodexoCpn, _
                AcceptPaymentTenderType.PositiveTenderType.ZomatoCash, AcceptPaymentTenderType.PositiveTenderType.SwiggyCash, _
                AcceptPaymentTenderType.PositiveTenderType.ScootsyOnline, AcceptPaymentTenderType.PositiveTenderType.ScootsyCash, _
                AcceptPaymentTenderType.PositiveTenderType.UberEatsOnline, AcceptPaymentTenderType.PositiveTenderType.UberEatsCash, _
                AcceptPaymentTenderType.PositiveTenderType.ZomatoOnline, AcceptPaymentTenderType.PositiveTenderType.FoodPandaCash, _
                AcceptPaymentTenderType.PositiveTenderType.FoodPandaOnline, _
                AcceptPaymentTenderType.PositiveTenderType.SwiggyOnline, AcceptPaymentTenderType.PositiveTenderType.DynTenderType, AcceptPaymentTenderType.PositiveTenderType.PhonePe  ', AcceptPaymentTenderType.NegativeTenderType.Cash ', AcceptPaymentTenderType.PositiveTenderType.ParkingCoupen
                ctrlPayCheque.Visible = False
                CtrlChequeDetails.Visible = False
                CtrlPayCreditCheque.Visible = False
                ctrlPayCredit.Visible = False
                lblSelectCurrency.Visible = True
                cboCurrency.Visible = True
                CtrlGiftVoucherIssue.Visible = False
                CtrlCLPPoint.Visible = False
                'If SelectedReceiptTypeCode = "Cash" Then
                '    CustomerRefPanel.Visible = False  '' added by nikhil for PC
                'Else
                '    CustomerRefPanel.Visible = True  '' added by nikhil for PC
                'End If

                If String.IsNullOrEmpty(lblContactNo.Text) Then
                    GbCustomer.Visible = False
                Else
                    GbCustomer.Visible = True
                End If

                'added by Khusrao Adil
                'for savoy
                If clsDefaultConfiguration.IsSavoy Then
                    PaymentTermCombo(False)
                End If
                'grBoxTopMenu.Height = 54
            Case "Cheque"
                'HidePointsColumn()
                'GbCustomer.Visible = False   '' added by nikhil 'uncommented by vipin
                'CustomerRefPanel.Visible = False

                CtrlCLPPoint.Visible = False
                lblSelectCurrency.Visible = False
                cboCurrency.Visible = False
                ctrlPayCredit.Visible = False
                'ctrlPayCheque.Visible = False
                CtrlPayCreditCheque.Visible = False
                'lblchequeNo.Text = "Cheque No"
                'lblDate.Text = "Date"
                CtrlGiftVoucherIssue.Visible = False
                'ctrlPayCheque.Visible = True
                ctrlPayCheque.PaymentType = "Cheque"
                'If clsDefaultConfiguration.ChequeInfomation = False Then
                '    ctrlPayCheque.Visible = False
                'End If
                'added by Khusrao Adil
                'for savoy
                If clsDefaultConfiguration.IsSavoy Then
                    PaymentTermCombo(False)
                End If
                If TenderHeadCode = "CreditCheque" Then
                    'GbCustomer.Visible = False   '' added by nikhil  Uncommented by vipin
                    'CustomerRefPanel.Visible = False
                    ctrlPayCheque.Visible = True
                    CtrlPayCreditCheque.Visible = True
                    CtrlChequeDetails.Visible = False
                Else
                    CtrlChequeDetails.Visible = True
                    CLearChequeDetails()
                End If
                'grBoxTopMenu.Height = 24
            Case AcceptPaymentTenderType.PositiveTenderType.GiftVoucher

                If (Not CheckCreditVoucherProgramExist(NegativeTenderType.GiftVoucherI.ToString())) Then
                    _tenderType = AcceptPaymentTenderType.PositiveTenderType.Cash
                    cboRecieptType.SelectedValue = AcceptPaymentTenderType.PositiveTenderType.Cash

                    Dim value As String = cboRecieptType.SelectedValue
                    For Each dr As DataRow In dtPayment.Select("TenderHeadCode='" & value & "'", "", DataViewRowState.CurrentRows)
                        _tenderHeadName = dr("TenderHeadName").ToString()
                        _tenderType = dr("TenderType").ToString()
                        _tenderHeadCode = dr("TenderHeadCode").ToString()
                    Next
                Else
                    CtrlCLPPoint.Visible = False
                    lblSelectCurrency.Visible = False
                    cboCurrency.Visible = False
                    ctrlPayCredit.Visible = False
                    ctrlPayCheque.Visible = True
                    CtrlChequeDetails.Visible = False
                    CtrlPayCreditCheque.Visible = False
                    CtrlGiftVoucherIssue.Visible = False
                    ctrlPayCheque.PaymentType = "GiftVoucher"
                    ctrlPayCheque.txtChequeNo.Select()
                    'added by Khusrao Adil
                    'for savoy
                    If clsDefaultConfiguration.IsSavoy Then
                        PaymentTermCombo(False)
                    End If
                End If
            Case AcceptPaymentTenderType.NegativeTenderType.CreditVoucherI
                'HidePointsColumn()
                CtrlCLPPoint.Visible = False
                ctrlPayCheque.Visible = False
                CtrlPayCreditCheque.Visible = False
                ctrlPayCredit.Visible = False
                CtrlChequeDetails.Visible = False
                lblSelectCurrency.Visible = True
                cboCurrency.Visible = True
                CtrlGiftVoucherIssue.Visible = False
                'added by Khusrao Adil
                'for savoy
                If clsDefaultConfiguration.IsSavoy Then
                    PaymentTermCombo(False)
                End If
            Case AcceptPaymentTenderType.PositiveTenderType.CreditVoucR
                'Rakesh-24.09.2013:Issue-7932=>Check CV program exist or not

                If (Not CheckCreditVoucherProgramExist(NegativeTenderType.CreditVoucherI.ToString())) Then
                    _tenderType = AcceptPaymentTenderType.PositiveTenderType.Cash
                    cboRecieptType.SelectedValue = AcceptPaymentTenderType.PositiveTenderType.Cash

                    Dim value As String = cboRecieptType.SelectedValue
                    For Each dr As DataRow In dtPayment.Select("TenderHeadCode='" & value & "'", "", DataViewRowState.CurrentRows)
                        _tenderHeadName = dr("TenderHeadName").ToString()
                        _tenderType = dr("TenderType").ToString()
                        _tenderHeadCode = dr("TenderHeadCode").ToString()
                    Next
                Else
                    lblSelectCurrency.Visible = False
                    CtrlChequeDetails.Visible = False
                    CtrlCLPPoint.Visible = False
                    cboCurrency.Visible = False
                    CtrlGiftVoucherIssue.Visible = False
                    ctrlPayCredit.Visible = False
                    ctrlPayCheque.Visible = True
                    CtrlPayCreditCheque.Visible = False
                    ctrlPayCheque.PaymentType = "CreditVouc"
                    ctrlPayCheque.txtChequeNo.Select()
                    'added by Khusrao Adil
                    'for savoy
                    If clsDefaultConfiguration.IsSavoy Then
                        PaymentTermCombo(False)
                    End If
                End If
            Case AcceptPaymentTenderType.PositiveTenderType.CreditCard ', AcceptPaymentTenderType.PositiveTenderType.MASTERCARD, AcceptPaymentTenderType.PositiveTenderType.VISACARD
                'GbCustomer.Visible = False   '' added by nikhil  uncommented by vipin
                'CustomerRefPanel.Visible = False

                CtrlCLPPoint.Visible = False
                lblSelectCurrency.Visible = False
                CtrlChequeDetails.Visible = False
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
                'added by Khusrao Adil
                'for savoy
                If clsDefaultConfiguration.IsSavoy Then
                    PaymentTermCombo(False)
                End If
                If cboRecieptType.Text.ToLower = "card inv" Then
                    AllowInnovitiPayment = True
                    TempAllowInnovitiPayment = True
                Else
                    TempAllowInnovitiPayment = False
                End If
                '----------------------------------------------------------------------------------------------
                'added on 15 may - ashma - for Innoviti (for hiding Credit Card No)
                If clsDefaultConfiguration.PayFromInnoviti = True And clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) AndAlso _
                    (AllowInnovitiPayment = True Or TempAllowInnovitiPayment = True) Then
                    'added by khusrao adil on 26-07-2018 for innviti with card functionality point _ natural client
                    If dtInnoviti.Rows.Count > 0 And cboRecieptType.Text = "Credit Card" Then
                        ctrlPayCredit.Visible = True
                    Else
                        ctrlPayCredit.Visible = False
                    End If
                End If
                '----------------------------------------------------------------------------------------------
            Case AcceptPaymentTenderType.NegativeTenderType.GiftVoucherI
                CtrlGiftVoucherIssue.Visible = True
                CtrlChequeDetails.Visible = False
                CtrlCLPPoint.Visible = False
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

                'added by Khusrao Adil
                'for savoy
                If clsDefaultConfiguration.IsSavoy Then
                    PaymentTermCombo(False)
                End If
            Case AcceptPaymentTenderType.PositiveTenderType.Credit ', AcceptPaymentTenderType.PositiveTenderType.ParkingCoupen
                'GbCustomer.Visible = False   '' added by nikhil Uncommented by vipin
                'CustomerRefPanel.Visible = False

                ctrlPayCheque.Visible = False
                CtrlChequeDetails.Visible = False
                CtrlPayCreditCheque.Visible = False
                ctrlPayCredit.Visible = False
                lblSelectCurrency.Visible = True
                cboCurrency.Visible = True
                CtrlGiftVoucherIssue.Visible = False

                'added by Khusrao Adil
                'for savoy
                If clsDefaultConfiguration.IsSavoy Then
                    PaymentTermCombo(True)
                End If
            Case AcceptPaymentTenderType.PositiveTenderType.CLPPoint

                If (CLP_Data.CLPConfigdata.Tables("CLPHeader") IsNot Nothing AndAlso CLP_Data.CLPConfigdata.Tables("CLPHeader").Rows.Count > 0) Then
                    Dim IsPosPasskey = CLP_Data.CLPConfigdata.Tables("CLPHeader")(0)("IsPOSPasskey")

                    If (Not IsDBNull(IsPosPasskey) AndAlso IsPosPasskey = True) Then
                        ctrlPayCash.txtCash.DataType = System.Type.GetType("System.String")
                        ctrlPayCash.txtCash.MaxLength = 15
                        ctrlPayCash.txtCash.Size = New System.Drawing.Size(130, 21)
                        lblAmount.Text = getValueByKey("LOY019")  'Pass Key

                        If (C1SizerPaymentTypes.Size.Width < 800) Then
                            C1SizerPaymentTypes.Grid.Columns(0).Size = 0.8 * C1SizerPaymentTypes.Size.Width
                            C1SizerPaymentTypes.Grid.Columns(1).Size = 0.2 * C1SizerPaymentTypes.Size.Width
                        End If

                    End If
                End If

                'CtrlCLPPoint.Visible = True
                Dim totalPoints As Double
                If dgGridReciept.Cols.Contains("Points") Then
                    For Each Row As DataRow In dgGridReciept.DataSource.rows
                        If Row("RecieptTypeCode") = AcceptPaymentTenderType.PositiveTenderType.CLPPoint Then
                            totalPoints = totalPoints + IIf(Row("Points") Is DBNull.Value, 0, Row("Points"))
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

        Try
            Dim value As String = cboRecieptType.SelectedValue.ToString()
            For Each dr As DataRow In dtPayment.Select("TenderHeadCode='" & value & "'", "", DataViewRowState.CurrentRows)
                _tenderHeadName = dr("TenderHeadName").ToString()
                _tenderType = dr("TenderType").ToString()
                _tenderHeadCode = dr("TenderHeadCode").ToString()
                cboRecieptType.SelectedValue = _tenderHeadCode
            Next
        Catch ex As Exception
        End Try

        Dim balanceAmount = lblbalanceDue.Text.Replace(",", String.Empty)

        If (Val(balanceAmount) < 0) Then
            ctrlPayCash.txtCash.Value = Val(balanceAmount) * -1
        Else
            ctrlPayCash.txtCash.Value = Val(balanceAmount)
        End If
        cboRecieptType.Focus()
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            lblAmount.Text = lblAmount.Text.ToUpper
        End If
    End Sub

    Private Function CheckCreditVoucherProgramExist(ByVal voucherType As String) As Boolean
        Try
            Dim obj As New clsAdvanceSale()
            Dim dtCVProgram As DataTable = obj.GetVoucherProg(clsAdmin.SiteCode, voucherType)

            If (dtCVProgram Is Nothing OrElse dtCVProgram.Rows.Count = 0) Then
                Dim dtActiveCVProgram As DataTable = obj.CheckActiveVoucherProg(clsAdmin.SiteCode, voucherType)
                If (dtActiveCVProgram IsNot Nothing OrElse dtActiveCVProgram.Rows.Count > 0) Then
                    If (String.Equals(voucherType, AcceptPaymentTenderType.NegativeTenderType.CreditVoucherI)) Then
                        ShowMessage(getValueByKey("CM069"), getValueByKey("CLAE04"))
                    ElseIf (String.Equals(voucherType, AcceptPaymentTenderType.NegativeTenderType.GiftVoucherI)) Then
                        ShowMessage(getValueByKey("CM070"), getValueByKey("CLAE04"))
                    End If
                    Return False
                End If


                If (String.Equals(voucherType, AcceptPaymentTenderType.NegativeTenderType.CreditVoucherI)) Then
                    ShowMessage(getValueByKey("CM062"), getValueByKey("CLAE04"))
                ElseIf (String.Equals(voucherType, AcceptPaymentTenderType.NegativeTenderType.GiftVoucherI)) Then
                    ShowMessage(getValueByKey("CM063"), getValueByKey("CLAE04"))
                End If

                Return False
            End If

            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Sub CLearChequeDetails()

        CtrlChequeDetails.txtChequeNo.Value = String.Empty
        'CtrlChequeDetails.txtBankName.Text = String.Empty
        'If CtrlChequeDetails.txtMicrNumber.ReadOnly = False Then
        CtrlChequeDetails.txtMicrNumber.Value = String.Empty
        'End If
        'CtrlChequeDetails.txtTelephoneNo.Text = String.Empty
        CtrlChequeDetails.dtChequeDate.Value = DateTime.Now

        CtrlChequeDetails.cmbBankName.Select()
    End Sub

    ''' <summary>
    ''' Delete payment entry in grid 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    '''
    Private Sub BtnCustSearch_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCustSearch.Click 'vipin
        Dim objCust As New frmSearchCustomer
        Dim result As DialogResult = objCust.ShowDialog()
        Dim dtCust As New DataTable
        Dim type As String
        Dim custType As String
        type = objCust.AddressType
        custType = objCust.CustmType
        dtCust = objCust.dtCustmInfo()

        _dtCust = dtCust
        _Addresstype = objCust.AddressType
        _custType = objCust.CustmType

        If Not dtCust Is Nothing Then
            If dtCust.Rows.Count > 0 Then
                GbCustomer.Visible = True
                CustomerPanel.Visible = True
                lblCustomer.Text = dtCust.Rows(0)("CustomerName").ToString
                lblCompanyName.Text = dtCust.Rows(0)("CompanyName").ToString
                lblContactNo.Text = dtCust.Rows(0)("mobileNo").ToString
            End If
        End If
    End Sub
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

            If PaymentType = clsAcceptPayment.PaymentType.EditBill Then
                If r("RecieptTypeCode") <> "CLPPoint" Then
                    'added by khusrao adil on 26-07-2018 for innoviti
                    If clsDefaultConfiguration.PayFromInnoviti AndAlso clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) Then
                        If r.Item("RecieptType").ToString() = "Credit Card" Or r.Item("RecieptType").ToString() = "CreditCard" Then
                            Dim clsCommon As New SpectrumBL.clsCommon()
                            If clsCommon.CheckEditBillAvailable(billno:=Billno, siteCode:=clsAdmin.SiteCode, tenderType:="CreditCard") = True Then
                                ShowMessage("You can not change this reciept payment", "Information")
                                '  Exit Sub
                            Else
                                dgGridReciept.Rows.Remove(r.Index)
                            End If
                        Else
                            dgGridReciept.Rows.Remove(r.Index)
                        End If
                    Else
                        dgGridReciept.Rows.Remove(r.Index)
                    End If
                    dtRecieptType.AcceptChanges()
                End If

                If dgGridReciept.Cols.Contains("Points") Then
                    If Not IsDBNull(dgGridReciept.Rows(r.Index)) AndAlso Not dgGridReciept.Rows(r.Index) Is Nothing Then
                        CtrlCLPPoint.lblPointsValue.Text = CDbl(CheckIfBlank(CtrlCLPPoint.lblPointsValue.Text)) + CDbl(dgGridReciept.Rows(r.Index)("Points"))
                        CtrlCLPPoint.lblPointAmount.Text = CtrlCLPPoint.lblPointsValue.Text * conversionRatio
                    End If
                End If

            Else
                'added by khsrao adil on 26-07-2018 for credit card value remove and add from drop down
                If clsDefaultConfiguration.PayFromInnoviti AndAlso clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) Then
                    '  If dt.Select("TenderType='CreditCard'").Count > 0 Then
                    If dtRecieptType.Rows.Count > 0 Then
                        For Each dr As DataRow In dtRecieptType.Rows
                            If dr("RecieptTypeCode") = "CreditCard" Then
                                ' Dim dt = objBLLAcceptPayment.LoadRecieptType(PaymentType, clsAdmin.SiteCode)
                                Dim dt = objBLLAcceptPayment.LoadRecieptType(PaymentType, clsAdmin.SiteCode, True)
                                dvPayment = New DataView(dt, filter, "", DataViewRowState.CurrentRows)
                                cboRecieptType.DataSource = dvPayment
                                cboRecieptType.DisplayMember = "TenderHeadName"
                                cboRecieptType.ValueMember = "TenderHeadCode"
                                pC1ComboSetDisplayMember(cboRecieptType)

                                Exit For
                            End If
                        Next

                    End If
                End If
                dgGridReciept.Rows.Remove(r.Index)
                dtRecieptType.AcceptChanges()

                If (dtRecieptType.Columns.Contains("Points")) Then
                    Dim clpPoints = dtRecieptType.Compute("Sum(Points)", "RecieptTypeCode='CLPPoint'")
                    CLP_Data._SlabPoints = IIf(clpPoints IsNot DBNull.Value, clpPoints, 0)
                End If

            End If
        Next
        dgGridReciept.Redraw = True
        cboCurrency.SelectedValue = clsAdmin.CurrencyCode
        If (dtRecieptType.Rows.Count > 0) Then
            If IsTotalAmountReceived() = False Then
                If CDbl(lblbalanceDue.Text) > 0 Then
                    strSelectedValue = cboRecieptType.SelectedValue
                    If Not cboRecieptType.SelectedValue Is Nothing Then
                        If strSelectedValue.ToString.ToUpper.Equals("CASH") Then
                            intReturnCashToCust = 0
                        End If
                    End If
                    filter = "Positive_Negative='+'"
                    If CreditSettlement Then 'NEW Added
                        filter = filter & " and TenderType<>'Credit' "
                    End If
                    dvPayment.RowFilter = filter
                    cboRecieptType_SelectedIndexChanged(cboRecieptType, New System.EventArgs)
                    If Not strSelectedValue = Nothing Then
                        cboRecieptType.SelectedValue = strSelectedValue
                    End If
                    cboRecieptType_Leave(sender, New System.EventArgs)
                Else
                    strSelectedValue = cboRecieptType.SelectedValue
                    'filter = "Positive_Negative='*' OR Positive_Negative='-'"
                    If clsDefaultConfiguration.PayFromInnoviti AndAlso clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) Then
                    Else
                        filter = "Positive_Negative='*' OR Positive_Negative='-'"
                    End If
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
                If Not cboRecieptType.SelectedValue Is Nothing Then
                    If strSelectedValue.ToString.ToUpper.Equals("CASH") Then
                        intReturnCashToCust = 0
                    End If
                End If
                filter = "Positive_Negative='+'"
                If CreditSettlement Then 'NEW Added
                    filter = filter & " and TenderType<>'Credit' "
                End If
                dvPayment.RowFilter = filter
                lblSign.Text = ""
                cboRecieptType_SelectedIndexChanged(cboRecieptType, New System.EventArgs)
                If Not strSelectedValue = Nothing Then
                    cboRecieptType.SelectedValue = strSelectedValue
                End If
            Else
                strSelectedValue = cboRecieptType.SelectedValue
                filter = "Positive_Negative='*' OR Positive_Negative='-'"

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
            lblRetrunAmount.Text = ""
            lblCalCurrencyBalanceDue.Text = lblCalCurrencyBillAmount.Text
            lblCalCurrencyTotalReciepts.Text = "0"
            IsRecieptAmountClose = False
        End If

        'Rakesh-01.10.2013:7975-Balance amount is not set in amount field
        If (Not String.IsNullOrEmpty(lblbalanceDue.Text.Trim)) Then
            ctrlPayCash.txtCash.Value = Math.Abs(Val(lblbalanceDue.Text.Replace(",", "")))
        End If

        If (dtRecieptType IsNot Nothing AndAlso dtRecieptType.Rows.Count = 0) Then
            _tenderType = AcceptPaymentTenderType.PositiveTenderType.Cash
            cboRecieptType.SelectedValue = AcceptPaymentTenderType.PositiveTenderType.Cash
            cboRecieptType_SelectedIndexChanged(cboRecieptType, New System.EventArgs)
        End If
        dgGridReciept.Font = New Font("Verdana", 10, FontStyle.Bold)
    End Sub
    ''' <summary>
    '''  Insert payment details into grid 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("Insert payment details into grid")> _
    Private Sub btnapprove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnapprove.Click
        If IsChangeTender Then
            If dsRecieptType.Tables().Count > 0 Then
                If dsRecieptType.Tables("MSTRecieptType").Rows.Count > 0 Then
                    For index = 0 To dsRecieptType.Tables("MSTRecieptType").Rows.Count - 1
                        If dsRecieptType.Tables("MSTRecieptType")(index)("RecieptTypeCode") = "Credit" Then
                            If TotalPick < TotalReceivedAmount + EnteredAmount Then
                                ShowMessage(getValueByKey("ACP037"), "ACP015 - " & getValueByKey("CLAE04"))
                                Exit Sub
                            End If
                        End If
                    Next
                End If
            End If
        End If
        '   If remarksTextbox.Text.Trim <> "" Then
        objBLLAcceptPayment.MainRemarks = TxtRemark.Text.Trim
        '  End If
        'added by vipin 09.01.2018 More PAyment validation for all tender expect Cash 
        If SelectedReceiptTypeCode <> AcceptPaymentTenderType.PositiveTenderType.Cash Then
            If (MyRound(TotalBillAmount - TotalReceivedAmount, clsDefaultConfiguration.BillRoundOffAt) >= EnteredAmount) = False Then
                ShowMessage(getValueByKey("ACP009"), "ACP009 - " & getValueByKey("CLAE04"))
                ctrlPayCash.txtCash.Clear()
                ctrlPayCash.txtCash.Value = Math.Abs(Val(lblbalanceDue.Text.Replace(",", "")))
                btnSave.Focus()
                Exit Sub
            End If
        End If
        Select Case (SelectedReceiptTypeCode)
            Case AcceptPaymentTenderType.PositiveTenderType.CLPPoint
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.Cash  ', AcceptPaymentTenderType.NegativeTenderType.Cash ', AcceptPaymentTenderType.PositiveTenderType.ParkingCoupen
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.OnlinePayment ', AcceptPaymentTenderType.NegativeTenderType.Cash ', AcceptPaymentTenderType.PositiveTenderType.ParkingCoupen
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.Neft
                If CustomerRefPanel.Visible = True Then
                    If Not String.IsNullOrEmpty(txtReferenceNo.Text.Trim) Then
                        objBLLAcceptPayment.ReferenceNo = txtReferenceNo.Text.Trim
                        objBLLAcceptPayment.TenderRemarks = TxtRemark.Text.Trim

                        If (MyRound(TotalBillAmount - TotalReceivedAmount, clsDefaultConfiguration.BillRoundOffAt) >= EnteredAmount) = False Then
                            ShowMessage(getValueByKey("ACP009"), "ACP009 - " & getValueByKey("CLAE04"))
                            ctrlPayCash.txtCash.Clear()
                            ctrlPayCash.txtCash.Value = Math.Abs(Val(lblbalanceDue.Text.Replace(",", "")))
                            btnSave.Focus()
                            Exit Sub
                        End If

                        InsertReceiptCashDetails()
                    Else
                        ShowMessage("Please enter reference number", "Information")
                        Exit Sub
                    End If
                End If
            Case AcceptPaymentTenderType.PositiveTenderType.Rtgs
                If CustomerRefPanel.Visible = True Then
                    If Not String.IsNullOrEmpty(txtReferenceNo.Text.Trim) Then
                        objBLLAcceptPayment.ReferenceNo = txtReferenceNo.Text.Trim
                        objBLLAcceptPayment.TenderRemarks = TxtRemark.Text.Trim
                        If (MyRound(TotalBillAmount - TotalReceivedAmount, clsDefaultConfiguration.BillRoundOffAt) >= EnteredAmount) = False Then
                            ShowMessage(getValueByKey("ACP009"), "ACP009 - " & getValueByKey("CLAE04"))
                            ctrlPayCash.txtCash.Clear()
                            ctrlPayCash.txtCash.Value = Math.Abs(Val(lblbalanceDue.Text.Replace(",", "")))
                            btnSave.Focus()
                            Exit Sub
                        End If
                        InsertReceiptCashDetails()
                    Else
                        ShowMessage("Please enter reference number", "Information")
                        Exit Sub
                    End If
                End If
            Case AcceptPaymentTenderType.PositiveTenderType.MealPass
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.Paytm
                If (MyRound(TotalBillAmount - TotalReceivedAmount, clsDefaultConfiguration.BillRoundOffAt) >= EnteredAmount) = False Then
                    ShowMessage(getValueByKey("ACP009"), "ACP009 - " & getValueByKey("CLAE04"))
                    ctrlPayCash.txtCash.Clear()
                    ctrlPayCash.txtCash.Value = Math.Abs(Val(lblbalanceDue.Text.Replace(",", "")))
                    btnSave.Focus()
                    Exit Sub
                End If
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.QuickWallet
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.Sodexo
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.FoodPandaCash
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.FoodPandaOnline
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.TicketRestaurant
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.PhonePe
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.OnlinePayment
                If (MyRound(TotalBillAmount - TotalReceivedAmount, clsDefaultConfiguration.BillRoundOffAt) >= EnteredAmount) = False Then
                    ShowMessage(getValueByKey("ACP009"), "ACP009 - " & getValueByKey("CLAE04"))
                    ctrlPayCash.txtCash.Clear()
                    ctrlPayCash.txtCash.Value = Math.Abs(Val(lblbalanceDue.Text.Replace(",", "")))
                    btnSave.Focus()
                    Exit Sub
                End If
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.Others
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.Payso
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.SodexoCards
                If (MyRound(TotalBillAmount - TotalReceivedAmount, clsDefaultConfiguration.BillRoundOffAt) >= EnteredAmount) = False Then
                    ShowMessage(getValueByKey("ACP009"), "ACP009 - " & getValueByKey("CLAE04"))
                    ctrlPayCash.txtCash.Clear()
                    ctrlPayCash.txtCash.Value = Math.Abs(Val(lblbalanceDue.Text.Replace(",", "")))
                    btnSave.Focus()
                    Exit Sub
                End If
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.MobiKwik
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.SodexoCpn
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.ZomatoCash
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.SwiggyCash
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.ScootsyOnline
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.ScootsyCash
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.UberEatsOnline
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.UberEatsCash
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.ZomatoOnline
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.SwiggyOnline
                InsertReceiptCashDetails()
            Case AcceptPaymentTenderType.PositiveTenderType.Credit  ', AcceptPaymentTenderType.NegativeTenderType.Cash ', AcceptPaymentTenderType.PositiveTenderType.ParkingCoupen
                If (MyRound(TotalBillAmount - TotalReceivedAmount, clsDefaultConfiguration.BillRoundOffAt) >= EnteredAmount) = False Then
                    ShowMessage(getValueByKey("ACP009"), "ACP009 - " & getValueByKey("CLAE04"))
                    ctrlPayCash.txtCash.Clear()
                    ctrlPayCash.txtCash.Value = Math.Abs(Val(lblbalanceDue.Text.Replace(",", "")))
                    btnSave.Focus()
                    Exit Sub
                End If
                'added by Khusrao Adil
                'for savoy
                If clsDefaultConfiguration.IsSavoy Then
                    If cboRecieptType.SelectedValue = "Credit Sales" AndAlso CtrlPayTerm.SelectedValue = "" AndAlso CtrlPayTerm.SelectedValue = Nothing Then
                        ShowMessage("Paymant term not provided", "Information")
                        Exit Sub
                    Else
                        InsertReceiptCashDetails()
                    End If
                Else
                    InsertReceiptCashDetails()
                End If
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
                'added by Khusrao Adil
                ' for savoy
                If clsDefaultConfiguration.IsSavoy Then
                    If cboRecieptType.SelectedValue = "Credit Sales" AndAlso CtrlPayTerm.SelectedValue = "" AndAlso CtrlPayTerm.SelectedValue = Nothing Then
                        ShowMessage("Paymant term not provided", "Information")
                        Exit Sub
                    Else
                        InsertReceiptCashDetails()
                    End If
                Else
                    InsertReceiptCashDetails()
                End If
                'added by khusrao adil on 19-05-2017 fro add and remove credit card value
                If clsDefaultConfiguration.PayFromInnoviti AndAlso clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) AndAlso AllowInnovitiPayment = True Then
                    cboRecieptType.DisplayMember = "TenderHeadName"
                    '  Dim dt = objBLLAcceptPayment.LoadRecieptType(0, clsAdmin.SiteCode)
                    Dim dt = objBLLAcceptPayment.LoadRecieptType(0, clsAdmin.SiteCode, True)
                    If dt.Rows.Count > 0 Then
                        If dt.Select("TenderType='CreditCard'").Count > 0 Then
                            RemoveAdddtPayment.Clear()
                            'RemoveAdddtPayment = dt.Select("TenderType='CreditCard'").CopyToDataTable 'added by khusrao adil on 18-05-2017
                            RemoveAdddtPayment = dt.Select("TenderHeadCode='Card Inv'").CopyToDataTable 'modified by khusrao adil on 14-02-2018
                            For Each dr As DataRow In dt.Rows
                                If dr("TenderHeadCode").ToString().ToLower = "card inv" Then
                                    dt.Rows.Remove(dr)
                                    Exit For
                                End If
                            Next
                            dvPayment = New DataView(dt, filter, "", DataViewRowState.CurrentRows)
                            cboRecieptType.DataSource = dvPayment
                            cboRecieptType.DisplayMember = "TenderHeadName"
                            cboRecieptType.ValueMember = "TenderHeadCode"
                            pC1ComboSetDisplayMember(cboRecieptType)
                        End If
                    End If
                End If
            Case AcceptPaymentTenderType.NegativeTenderType.GiftVoucherI
                InsertGiftVoucherIssue()
            Case AcceptPaymentTenderType.PositiveTenderType.DynTenderType
                InsertReceiptCashDetails()
            Case Else
                Exit Select
        End Select
        If (Val(lblbalanceDue.Text) < 0) Then
            ctrlPayCash.txtCash.Value = Val(lblbalanceDue.Text.Replace(",", "")) * -1
        Else
            ctrlPayCash.txtCash.Value = Val(lblbalanceDue.Text.Replace(",", ""))
        End If

    End Sub
    ''' <summary>
    ''' Checks when payment is done 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        Try

            ' for change mode form only
            blnChangeModeFormOk = True
            ' for change mode form only

            ''----------------------------------------------------------------------------------------------
            ''added on 12 may - ashma - for Innoviti (If condition)
            'If IsChangeTender Then
            '    If clsDefaultConfiguration.PayFromInnoviti Then

            '        Dim row = dsRecieptType.Tables("MSTRecieptType").Select("RecieptTypeCode='CreditCard'")
            '        If row.Count > 0 Then
            '            Dim responseFromInnov As New Dictionary(Of String, String)
            '            responseFromInnov = modCommanFunction.PaymentToCallEDC(Billno, row(0)("Amount"))
            '            If responseFromInnov Is Nothing Then
            '                ShowMessage("Error in Card payment", "Error")
            '                'Me.DialogResult = Windows.Forms.DialogResult.Cancel
            '                Exit Sub
            '            Else
            '                resonseInnoviti = New Dictionary(Of String, String)
            '                resonseInnoviti = responseFromInnov
            '            End If
            '        End If
            '    End If
            'End If
            ''----------------------------------------------------------------------------------------------
            If dsRecieptType.Tables.Count > 0 Then
                For Each DrTender In dsRecieptType.Tables(0).Rows 'vipin 14.05.2018 Only one remark ismandatory
                    If HasAlphaNumericChar(DrTender("Remarks").ToString.Trim()) Then
                        TxtRemark.Text = DrTender("Remarks").ToString.Trim()
                    End If
                Next
            End If
            If IsPostiveTenderType Then
                If (PaymentType = clsAcceptPayment.PaymentType.Advance) Then

                    'Start- Rakesh Gautam -> comment if ...else Statement

                    'If (TotalReceivedAmount < lblCalCurrencyMiniBalDue.Text) Then
                    If (TotalReceivedAmount < TotalBillAmount) Then
                        If SelectedCurrencyDescription <> clsAdmin.CurrencyDescription Then
                            ShowMessage(String.Format(getValueByKey("ACP015"), SelectedCurrencySymbol, TotalBillAmount), "ACP015 - " & getValueByKey("CLAE04"))
                        Else
                            ShowMessage(String.Format(getValueByKey("ACP015"), SelectedCurrencySymbol, TotalBillAmount), "ACP015 - " & getValueByKey("CLAE04"))
                            Exit Sub
                        End If

                    ElseIf (TotalReceivedAmount > TotalBillAmount) Then
                        ShowMessage(getValueByKey("ACP028"), "ACP028 - " & getValueByKey("CLAE04"))
                        Exit Sub
                    ElseIf _IsCashierPromoSelected = True AndAlso (String.IsNullOrEmpty(TxtRemark.Text) Or HasAlphaNumericChar(TxtRemark.Text) = False) Then
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
                    If (TotalReceivedAmount < TotalBillAmount) And _IsCreditSale = True Then   'Partial credit sale' vipin 15.12.2017
                        MessageBox.Show("You have paid partial amount to adjust credit sale.Remaining amount Rs." + (CDbl(TotalBillAmount) - CDbl(TotalReceivedAmount)).ToString + " will be considered as credit sale.You can adjust the remaining amount again anytime.")
                    End If
                    If (TotalReceivedAmount < TotalBillAmount) And _IsCreditSale = False Then
                        'If Then
                        ShowMessage(String.Format(getValueByKey("ACP015"), SelectedCurrencySymbol, TotalBillAmount), "ACP015 - " & getValueByKey("CLAE04"))
                        Exit Sub
                        'End If
                        ' If _IsCreditSale = True And TotalReceivedAmount = 0 Then 'vipin 16.11.2017
                    ElseIf _IsCreditSale = True And TotalReceivedAmount = 0 Then
                        ShowMessage("Please select any tender", "ACP015 - " & getValueByKey("CLAE04"))
                        Exit Sub
                    ElseIf TotalReceivedAmount > TotalBillAmount Then
                        ShowMessage(getValueByKey("ACP028"), "ACP028 - " & getValueByKey("CLAE04"))
                        Exit Sub
                    ElseIf _IsCashierPromoSelected = True AndAlso (String.IsNullOrEmpty(TxtRemark.Text) Or HasAlphaNumericChar(TxtRemark.Text) = False) Then
                        ShowMessage("Please Enter Remarks", "Warning")
                        Exit Sub
                    Else
                        CheckDataset_Changes()
                        Me.Close()
                    End If
                End If
            Else
                If _IsCashierPromoSelected = True AndAlso (String.IsNullOrEmpty(TxtRemark.Text) Or HasAlphaNumericChar(TxtRemark.Text) = False) Then
                    ShowMessage("Please Enter Remarks", "Warning")
                    Exit Sub
                End If
                If (IsRecieptAmountClose) Then
                    CheckDataset_Changes()
                    If _IsCashierPromoSelected = True AndAlso Not (String.IsNullOrEmpty(TxtRemark.Text) Or HasAlphaNumericChar(TxtRemark.Text) = False) Then
                        Me.Close()
                    ElseIf _IsCashierPromoSelected Then
                        ShowMessage("Please Enter Remarks", "Warning")
                        Exit Sub
                    Else
                        Me.Close()
                    End If
                Else
                    ShowMessage(String.Format(getValueByKey("ACP015"), SelectedCurrencySymbol, TotalBillAmount), "ACP015 - " & getValueByKey("CLAE04"))
                End If
            End If

            Me.DialogResult = Windows.Forms.DialogResult.OK
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
                    For Each drRecipt In dsRecieptType.Tables(0).Rows 'vipin
                        drRecipt("RefNo_4") = drRecipt("Remarks")
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
            _tenderType = AcceptPaymentTenderType.PositiveTenderType.Cash
            cboRecieptType_SelectedIndexChanged(cboRecieptType, e)
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
            _tenderType = strNameToFind
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
            If Me._CardNo Is Nothing Then
                If _IsTenderChange Then
                    MessageBox.Show(getValueByKey("Crs025"), getValueByKey("Crs016"))
                Else
                    If String.IsNullOrEmpty(lblContactNo.Text.ToString.Trim) Then  'vipin
                        MessageBox.Show(getValueByKey("Crs024"), getValueByKey("Crs016"))
                    End If

                End If
                ctrlPayCash.txtCash.Focus()
                Exit Sub
            End If

            If (IsPostiveTenderType) Then
                If EnableOrDisable(AcceptPaymentTenderType.PositiveTenderType.Cheque) Then
                    cboRecieptType_SelectedIndexChanged(cboRecieptType, e)
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
            If EnableOrDisable(AcceptPaymentTenderType.PositiveTenderType.CreditVoucR) Then
                cboRecieptType.Focus()
                cboRecieptType_SelectedIndexChanged(cboRecieptType, e)
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
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub


    Private Sub cboRecieptType_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRecieptType.Leave
        Try
            If Not cboRecieptType.SelectedValue Is Nothing Then

                Dim tenders() As DataRow = dtPayment.Select("TenderHeadCode='" & cboRecieptType.SelectedValue & "'", "", DataViewRowState.CurrentRows)

                If tenders.Count() > 0 AndAlso (tenders(0)("TenderType") = AcceptPaymentTenderType.PositiveTenderType.Credit Or tenders(0)("TenderType") = AcceptPaymentTenderType.PositiveTenderType.Cheque) Then
                    If Me._CardNo Is Nothing And String.IsNullOrEmpty(lblContactNo.Text.ToString.Trim) Then
                        If (tenders(0)("TenderType").Equals(AcceptPaymentTenderType.PositiveTenderType.Credit.ToString())) Then
                            MessageBox.Show(getValueByKey("Crs022"), getValueByKey("Crs016"))
                        ElseIf (tenders(0)("TenderType").Equals(AcceptPaymentTenderType.PositiveTenderType.Cheque.ToString())) Then
                            If _IsTenderChange Then
                                MessageBox.Show(getValueByKey("Crs025"), getValueByKey("Crs016"))
                            Else
                                If String.IsNullOrEmpty(lblContactNo.Text.ToString.Trim) Then  'vipin
                                    MessageBox.Show(getValueByKey("Crs024"), getValueByKey("Crs016"))
                                End If

                            End If
                        End If

                        cboRecieptType.SelectedIndex = _cboreceiptPreIndex
                        Exit Sub
                    End If
                End If
            End If


            Dim value As String = cboRecieptType.SelectedValue
            For Each dr As DataRow In dtPayment.Select("TenderHeadCode='" & value & "'", "", DataViewRowState.CurrentRows)
                _tenderHeadName = dr("TenderHeadName").ToString()
                _tenderType = dr("TenderType").ToString()
                _tenderHeadCode = dr("TenderHeadCode").ToString()
            Next
            'If Not cboRecieptType.SelectedValue Is Nothing Then
            '    cboRecieptType_SelectedIndexChanged(sender, New System.EventArgs)
            'End If
            If IsPostiveTenderType = False Then
                cboCurrency.Visible = False
                lblSelectCurrency.Visible = False
            End If
            If cboCurrency.SelectedValue = clsAdmin.CurrencyCode Then
                ctrlPayCash.txtCash.Text = lblbalanceDue.Text
                ctrlPayCash.txtCash.Value = Convert.ToDecimal(lblbalanceDue.Text)
            Else
                ctrlPayCash.txtCash.Text = lblCalCurrencyBalanceDue.Text
                ctrlPayCash.txtCash.Value = Convert.ToDecimal(lblCalCurrencyBalanceDue.Text)
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
            If value = "Neft" Or value = "Rtgs" Then
                CustomerRefPanel.Visible = True
            Else
                CustomerRefPanel.Visible = False
            End If
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
      
        If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
            Dim IsBillPaidByPhonepe As Boolean = False
            Dim PhonePeTenderAmount As Double = 0
            If (dtRecieptType.Columns.Contains("TenderType")) Then
                For Each drrow In dtRecieptType.Rows
                    If drrow("TenderType") = "PhonePe" Then
                        PhonePeTenderAmount = drrow("Amount")
                        IsBillPaidByPhonepe = True
                    End If
                Next
            End If


            If IsBillPaidByPhonepe Then
                Dim objonline As New FrmOnlinePayment
                objonline.CMBillno = Billno
                objonline.CustName = lblContactNo.Text
                objonline.TotalBillAmt = PhonePeTenderAmount
                objonline.ShowDialog()
                If Not objonline.IsPaymentSuccess Then
                    Exit Sub
                End If
            End If
        End If

        btnOK_Click(sender, e)
        '----------------------------------------------------------------------------------------------
        Try
            'added on 12 may - ashma - for Innoviti (If condition)
            If Me.DialogResult = Windows.Forms.DialogResult.OK Then

                'added by khusrao adil on 27-11-2017
                Dim aplicableWithInnvo As Boolean = False
                If AllowInnovitiWithOtherTender = True And AllowInnovitiPayment = True Then
                    aplicableWithInnvo = True
                ElseIf AllowInnovitiWithOtherTender = False And AllowInnovitiPayment = True Then
                    aplicableWithInnvo = True
                ElseIf AllowInnovitiWithOtherTender = False And AllowInnovitiPayment = False Then
                    aplicableWithInnvo = False
                End If

                'added on 12 may - ashma - for Innoviti (If condition)
                If clsDefaultConfiguration.PayFromInnoviti = True And clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) AndAlso aplicableWithInnvo = True Then
                    Dim row = dsRecieptType.Tables("MSTRecieptType").Select("RecieptTypeCode='CreditCard'")
                    If row.Count > 0 Then
                        Dim objClsComm As New clsCommon
                        If objClsComm.CheckEditBillAvailable(billno:=Billno, siteCode:=clsAdmin.SiteCode, tenderType:="CreditCard") = False Then

                            Dim responseFromInnov As New Dictionary(Of String, String)
                            responseFromInnov = modCommanFunction.PaymentToCallEDC(Billno, row(0)("Amount"))
                            If responseFromInnov Is Nothing Then
                                Dim ex As New Exception(modCommanFunction.InnovitiResponseError.ToString())
                                LogException(ex)
                                ShowMessage("Error in Card payment", "Information")
                                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                                Exit Sub
                            Else
                                resonseInnoviti = New Dictionary(Of String, String)
                                resonseInnoviti = responseFromInnov
                                CreditCardNumber = resonseInnoviti("CardNumber")
                                ' ''------------------------- added by khusrao adil on 26-07-2018
                                If dtInnoviti.Rows.Count > 0 Then
                                    Dim drResult = dtInnoviti.Select("BillNo='" + Billno + "'")
                                    drResult(0)("CardNo") = CreditCardNumber
                                    drResult(0)("RetrievalReferenceNumber") = resonseInnoviti("RetrievalReferenceNumber")
                                Else
                                    Dim InnvoitiLineNo = 1
                                    Dim dr As DataRow = dtInnoviti.NewRow
                                    dr("LineNo") = InnvoitiLineNo
                                    dr("TenderType") = row(0)("RecieptTypeCode")
                                    dr("BillNo") = Billno
                                    dr("AmountTendered") = row(0)("Amount")
                                    dr("CardNo") = resonseInnoviti("CardNumber")
                                    dr("RetrievalReferenceNumber") = resonseInnoviti("RetrievalReferenceNumber")
                                    dr("InnvoitiApplicable") = True
                                    dtInnoviti.Rows.Add(dr)
                                End If
                            End If
                        End If
                    End If
                End If

                '  End If

                'If clsDefaultConfiguration.PayFromInnoviti = True And clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) Then
                '    Dim row = dsRecieptType.Tables("MSTRecieptType").Select("RecieptTypeCode='CreditCard'")
                '    If row.Count > 0 Then
                '        Dim responseFromInnov As New Dictionary(Of String, String)
                '        responseFromInnov = modCommanFunction.PaymentToCallEDC(Billno, row(0)("Amount"))
                '        If responseFromInnov Is Nothing Then
                '            ShowMessage("Error in Card payment", "Error")
                '            'Me.DialogResult = Windows.Forms.DialogResult.Cancel
                '            Exit Sub
                '        Else
                '            resonseInnoviti = New Dictionary(Of String, String)
                '            resonseInnoviti = responseFromInnov
                '            CreditCardNumber = resonseInnoviti("CardNumber")
                '        End If
                '    End If
                'End If

            End If
        Catch ex As Exception
            LogException(ex)
        End Try
        ''-------------------------------------------------------------------------------------------------

        ReturnMsg()
        Try
            If clsAdmin.IsCashDrawer Then '---Code added by Mahesh for checking Cash Drawer is available or not
                Dim cA4Print As New clsA4Print
                cA4Print.OperateDevice("CashDrawer", CDflag:=clsDefaultConfiguration.CDBuildCode)
            End If

        Catch ex As Exception

        End Try

    End Sub
    Private Sub btnGift_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGift.Click
        Try
            GiftReceiptMessage = GetGiftMessage()

            If GiftReceiptMessage = String.Empty Then Exit Sub
            If clsDefaultConfiguration.EnablePhonePeIntegration = True Then
                Dim IsBillPaidByPhonepe As Boolean = False
                Dim PhonePeTenderAmount As Double = 0
                If (dtRecieptType.Columns.Contains("TenderType")) Then
                    For Each drrow In dtRecieptType.Rows
                        If drrow("TenderType") = "PhonePe" Then
                            PhonePeTenderAmount = drrow("Amount")
                            IsBillPaidByPhonepe = True
                        End If
                    Next
                End If
                If IsBillPaidByPhonepe Then
                    Dim objonline As New FrmOnlinePayment
                    objonline.CMBillno = Billno
                    objonline.CustName = lblContactNo.Text
                    objonline.TotalBillAmt = PhonePeTenderAmount
                    objonline.ShowDialog()
                    If Not objonline.IsPaymentSuccess Then
                        Exit Sub
                    End If
                End If
            End If


            _Actiontype = "Gift"
            btnOK_Click(sender, e)
            Try
                If clsAdmin.IsCashDrawer Then '---Code added by Mahesh for checking Cash Drawer is available or not
                    Dim cA4Print As New clsA4Print
                    cA4Print.OperateDevice("CashDrawer", CDflag:=clsDefaultConfiguration.CDBuildCode)
                End If
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
                DisplaySettings_CurrencyCalculation(True)
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
                'pnlBoxCurrency.Visible = False
                DisplaySettings_CurrencyCalculation(False)

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
                    cboRecieptType_SelectedIndexChanged(cboRecieptType, e)
                    ctrlPayCash.txtCash.Focus()
                    ctrlPayCheque.txtChequeNo.Focus()
                End If
            End If
        Catch ex As Exception
            LogException(ex)
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
                    'Me.ctrlPayCheque.txtChequeNo.Focus()
                    'Me.CtrlChequeDetails.txtChequeNo.Focus()
                    Me.CtrlChequeDetails.cmbBankName.Focus()
                Case AcceptPaymentTenderType.PositiveTenderType.GiftVoucher
                    Me.ctrlPayCheque.ctrlGiftVouc.Focus()
                Case AcceptPaymentTenderType.NegativeTenderType.CreditVoucherI
                    Me.ctrlPayCredit.txtCreditCardNo.Focus()
                Case AcceptPaymentTenderType.PositiveTenderType.CreditVoucR
                    Me.ctrlPayCredit.txtCreditCardNo.Focus()
                Case AcceptPaymentTenderType.PositiveTenderType.CreditCard ', AcceptPaymentTenderType.PositiveTenderType.MASTERCARD, AcceptPaymentTenderType.PositiveTenderType.VISACARD
                    'Me.ctrlPayCredit.txtCreditCardNo.Focus()
                    Me.ctrlPayCredit.cmbBankName.Focus()
                Case AcceptPaymentTenderType.PositiveTenderType.Paytm
                    Me.CtrlChequeDetails.cmbBankName.Focus()
                Case AcceptPaymentTenderType.PositiveTenderType.QuickWallet
                    Me.CtrlChequeDetails.cmbBankName.Focus()
            End Select

        End If


        'Throw New NotImplementedException
    End Sub

    'Throw New NotImplementedException




    ''' <summary>
    ''' Taking Current Index of CMB
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboRecieptType_Enter(sender As System.Object, e As System.EventArgs) Handles cboRecieptType.Enter
        _cboreceiptPreIndex = DirectCast(sender, Spectrum.ctrlCombo).SelectedIndex
    End Sub

    Public Function removeCLPOPtion() As DataTable
        Dim dv As New DataView(dtPayment, "TenderType<>'CLPPoint' and  Positive_Negative='+'", "", DataViewRowState.CurrentRows)
        cboRecieptType.DataSource = dv
        cboRecieptType.DisplayMember = "TenderHeadName"
        cboRecieptType.ValueMember = "TenderHeadCode"
        pC1ComboSetDisplayMember(cboRecieptType)
    End Function

    Private Sub frmNAcceptPayment_Move(sender As Object, e As System.EventArgs) Handles Me.Move
        Me.Location = New Point(Screen.PrimaryScreen.Bounds.Width - Me.Width, 50)
    End Sub

    Private Sub dgGridReciept_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles dgGridReciept.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
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

                    If PaymentType = clsAcceptPayment.PaymentType.EditBill Then
                        If r("RecieptTypeCode") <> "CLPPoint" Then
                            dgGridReciept.Rows.Remove(r.Index)
                            dtRecieptType.AcceptChanges()
                        End If

                        If dgGridReciept.Cols.Contains("Points") Then
                            If Not IsDBNull(dgGridReciept.Rows(r.Index)) AndAlso Not dgGridReciept.Rows(r.Index) Is Nothing Then
                                CtrlCLPPoint.lblPointsValue.Text = CDbl(CheckIfBlank(CtrlCLPPoint.lblPointsValue.Text)) + CDbl(dgGridReciept.Rows(r.Index)("Points"))
                                CtrlCLPPoint.lblPointAmount.Text = CtrlCLPPoint.lblPointsValue.Text * conversionRatio
                            End If
                        End If

                    Else
                        dgGridReciept.Rows.Remove(r.Index)
                        dtRecieptType.AcceptChanges()

                        If (dtRecieptType.Columns.Contains("Points")) Then
                            Dim clpPoints = dtRecieptType.Compute("Sum(Points)", "RecieptTypeCode='CLPPoint'")
                            CLP_Data._SlabPoints = IIf(clpPoints IsNot DBNull.Value, clpPoints, 0)
                        End If

                    End If
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
                            filter = "Positive_Negative='*' OR Positive_Negative='-'"
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
                        filter = "Positive_Negative='*' OR Positive_Negative='-'"

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

                'Rakesh-01.10.2013:7975-Balance amount is not set in amount field
                If (Not String.IsNullOrEmpty(lblbalanceDue.Text.Trim)) Then
                    ctrlPayCash.txtCash.Value = Math.Abs(Val(lblbalanceDue.Text))
                End If

                If (dtRecieptType IsNot Nothing AndAlso dtRecieptType.Rows.Count = 0) Then
                    _tenderType = AcceptPaymentTenderType.PositiveTenderType.Cash
                    cboRecieptType.SelectedValue = AcceptPaymentTenderType.PositiveTenderType.Cash
                    cboRecieptType_SelectedIndexChanged(cboRecieptType, New System.EventArgs)
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Dim test As Boolean = False

    Private Sub remarksTextbox_Click(sender As Object, e As EventArgs) Handles TxtRemark.Click
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub
    Private Sub remarksTextbox_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TxtRemark.KeyUp
        Try
            If e.Alt AndAlso (e.KeyCode = Keys.D0 Or e.KeyCode = Keys.D1 Or e.KeyCode = Keys.D2 _
                       Or e.KeyCode = Keys.D3 Or e.KeyCode = Keys.D4 Or e.KeyCode = Keys.D5 _
                       Or e.KeyCode = Keys.D6 Or e.KeyCode = Keys.D7 Or e.KeyCode = Keys.D8 _
                       Or e.KeyCode = Keys.D9 Or e.KeyCode = Keys.NumPad0 _
                       Or e.KeyCode = Keys.NumPad1 Or e.KeyCode = Keys.NumPad2 Or e.KeyCode = Keys.NumPad3 _
                       Or e.KeyCode = Keys.NumPad4 Or e.KeyCode = Keys.NumPad5 Or e.KeyCode = Keys.NumPad6 _
                       Or e.KeyCode = Keys.NumPad7 Or e.KeyCode = Keys.NumPad8 Or e.KeyCode = Keys.NumPad9) Then

                _altWasPressed = True
                'Else

                '    Dim ax As New ApplicationException(e.Alt.ToString & "/" & e.KeyCode.ToString)
                '    LogException(ax)
                '    If test = True Then
                '        MsgBox("testit")
                '    End If

                '    If e.Alt.ToString = "False" AndAlso e.KeyCode.ToString = "Menu" Then
                '        test = True
                '    End If


            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub remarksTextbox_TextChanged(sender As Object, e As System.EventArgs) Handles TxtRemark.TextChanged
        Try
            Dim textEnd As String = TxtRemark.Text.Substring(TxtRemark.SelectionStart - 1, 1)
            If textEnd = "☺" Or textEnd = "☻" Or textEnd = "♥" Or textEnd = "♦" _
                                        Or textEnd = "♣" Or textEnd = "♠" Or textEnd = "•" Or textEnd = "◘" Or textEnd = "○" Or textEnd = "§" Or textEnd = "╚" Or textEnd = "▲" Or textEnd = "ä" Or textEnd = "╤" Or textEnd = "♀" Then

                _altWasPressed = True
            End If

            If (_altWasPressed) Then
                ' remove the added character
                Dim textBox = CType(sender, RichTextBox)
                Dim caretPos = TxtRemark.SelectionStart
                If caretPos = 0 Then Exit Sub
                Dim text = TxtRemark.Text
                Dim textStart = text.Substring(0, caretPos - 1)
                If (caretPos <= text.Length) Then
                    textEnd = text.Substring(caretPos, text.Length - caretPos)
                    TxtRemark.Text = textStart + textEnd
                    TxtRemark.SelectionStart = caretPos - 1
                    ' Dim ax As New ApplicationException(text & "/" & textStart & "/" & caretPos & "/" & textEnd & "/" & Me.Text)
                    'LogException(ax)
                    _altWasPressed = False
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    ' Function added by ketan for Outstanding Report (Payment Terms Functionality)  for Client Savoy
    Private Sub txtRemark_TextChanged(sender As Object, e As EventArgs) Handles TxtRemark.TextChanged  'vipin 16112017
        Dim str As String = TxtRemark.Text
        TxtRemark.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str.ToLower())
        TxtRemark.SelectionStart = Len(TxtRemark.Text)
    End Sub
    Public Sub GetCreditPaymentTerms()
        Try
            Dim clsCommon As New SpectrumBL.clsCommon()
            Dim bankDetails = clsCommon.GetCreditPaymentTermDetails(clsAdmin.SiteCode, _ParentForm)
            CtrlPayTerm.DataSource = bankDetails
            CtrlPayTerm.DisplayMember = "Name"
            CtrlPayTerm.ValueMember = "Id"
            pC1ComboSetDisplayMember(CtrlPayTerm)
            ' CtrlPayTerm.Splits(0).DisplayColumns(0).Visible = False
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub rtxtSwipeCard_Click(sender As Object, e As EventArgs)
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub
    Private Sub txtReferenceNo_keyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtReferenceNo.KeyPress

        If Not Char.IsLetterOrDigit(e.KeyChar) Then  'vipin 
            If e.KeyChar = Convert.ToChar(8) Then
            Else
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub frmNAcceptPaymentPC_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                    Case Keys.Escape
                        btnCancel_Click(sender, e)
                    Case Keys.F1
                        Dim objClsCommon As New clsCommon
                        objClsCommon.DisplayHelpFile(ParentForm, "accept-payment.htm")
                End Select
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.Escape Then
            _IsCancelAcceptPayment = True
            Me.Close()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
End Class

