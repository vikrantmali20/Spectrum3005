Imports SpectrumBL
Imports SpectrumBL.clsAcceptPayment
Imports SpectrumPrint
Imports SpectrumCommon
Imports System.Text
Imports System.Globalization

Public Class frmNAcceptPaymentByNEFTRTGS
    Private objCreditSales As New ClsCreditSale

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub
    Dim ObjclsCommon As New clsCommon
    Dim dtHeader As New DataTable

    Private dsCreditInvoice As DataSet
    Private _billAmount As Double
    Private _IsCreditSale As Boolean = False  'vipin
    Public Property IsCreditSale() As Double
        Get
            Return _IsCreditSale
        End Get
        Set(ByVal value As Double)
            _IsCreditSale = value
        End Set
    End Property
    Public Property BillAmount() As Double
        Get
            Return _billAmount
        End Get
        Set(ByVal value As Double)
            _billAmount = value
        End Set
    End Property

    Private _balAmount As Double
    Public Property BalAmount() As Double
        Get
            Return _balAmount
        End Get
        Set(ByVal value As Double)
            _balAmount = value
        End Set
    End Property

    Private _billNumber As String
    Public Property BillNumber() As String
        Get
            Return _billNumber
        End Get
        Set(ByVal value As String)
            _billNumber = value
        End Set
    End Property

    Private _invoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return _invoiceNumber
        End Get
        Set(ByVal value As String)
            _invoiceNumber = value
        End Set
    End Property

    Private _tenderTypeCode As String
    Public Property TenderTypeCode() As String
        Get
            Return _tenderTypeCode
        End Get
        Set(ByVal value As String)
            _tenderTypeCode = value
        End Set
    End Property

    Private _tenderHeadCode As String
    Public Property TenderHeadCode() As String
        Get
            Return _tenderHeadCode
        End Get
        Set(ByVal value As String)
            _tenderHeadCode = value
        End Set
    End Property

    Private _TranTypeCode As String
    Public Property TranTypeCode() As String
        Get
            Return _TranTypeCode
        End Get
        Set(ByVal value As String)
            _TranTypeCode = value
        End Set
    End Property

    Private _FloatAmtReturned As Double
    Public Property FloatAmtReturned() As Double
        Get
            Return _FloatAmtReturned
        End Get
        Set(ByVal value As Double)
            _FloatAmtReturned = value
        End Set
    End Property

    Private _DeliveryPerson As String
    Public Property DeliveryPerson() As String
        Get
            Return _DeliveryPerson
        End Get
        Set(ByVal value As String)
            _DeliveryPerson = value
        End Set
    End Property
    Public _IsCashierPromoSelected As Boolean
    Dim _Actiontype As String
    Public ReadOnly Property Action() As String
        Get
            Return _Actiontype
        End Get
    End Property
    Protected dsRecieptType As New DataSet()
    Public ReadOnly Property ReciptTotalAmount() As DataSet
        Get
            Return dsRecieptType
        End Get
    End Property
    Private _IsCancelAcceptPayment As Boolean = True
    Public Property IsCancelAcceptPayment() As Boolean
        Get
            Return _IsCancelAcceptPayment
        End Get
        Set(ByVal value As Boolean)
            _IsCancelAcceptPayment = value
        End Set
    End Property
    Private _decTotalBillAmount As Decimal
    Public Property TotalBillAmount() As Decimal
        Get
            Return _decTotalBillAmount
        End Get
        Set(ByVal value As Decimal)
            _decTotalBillAmount = value
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
    Dim _DocumentType As String
    Public Property DocumentType() As String
        Get
            Return _DocumentType
        End Get
        Set(ByVal value As String)
            _DocumentType = value
        End Set
    End Property
    Private _decTotalMinAmount As Decimal 'vipin
    Public Property TotalMinAmount() As Decimal
        Get
            Return _decTotalMinAmount
        End Get
        Set(ByVal value As Decimal)
            _decTotalMinAmount = value
        End Set
    End Property
    Private Function IsAmountReturnTocustomer() As Boolean
        Try
            If (txtCollectAmount.Text > BalAmount) Then
                Dim ReturnAmt As Decimal
                ReturnAmt = Format(txtCollectAmount.Text - CDbl(BalAmount), "0.00")
                Dim strshowmsg As String = ""
                ShowMessage(True, String.Format(getValueByKey("ACPBCS07"), BalAmount, txtCollectAmount.Text, ReturnAmt, clsAdmin.CurrencyCode), "ACPBCS07 - " & getValueByKey("CLAE04"))
                txtCollectAmount.Text = CDbl(txtCollectAmount.Text) - ReturnAmt
                Return True
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Private Function AddPaymentIntoDataTable(ByVal acceptAmount As Double, Optional ByVal docType As String = "") As DataSet
        Try
            'Dim dtInvoice As New DataTable
            'dtInvoice = objCreditSales.GetBillInvoiceDtlsAgainstSO(BillNumber, docType)
            'If Not dtInvoice Is Nothing AndAlso dtInvoice.Rows.Count > 0 Then

            '    Dim tenderAmt As Decimal

            '    For i = 0 To dtInvoice.Rows.Count - 1
            '   Dim tenderAmt1 = CDec(dtInvoice.Rows(i)("AmountTendered"))
            dsCreditInvoice = objCreditSales.GetBillInvoiceDtls()
            'tenderAmt = acceptAmount - tenderAmt1
            Dim objCM As New clsCashMemo()
            Dim ServerDate = ObjclsCommon.GetCurrentDate()
            Dim objType = "FO_DOC"
            Dim docno As String = objCM.getDocumentNo("SalesInvoice", clsAdmin.SiteCode, objType)
            '  newBillNo = GenDocNo("SI" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
            newBillNo = GenDocNo("S" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 3, 3) & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)

            Dim dtCurrency = New clsAcceptPayment().LoadCurrency(clsAdmin.SiteCode)
            Dim currencySymbol As String = IIf((dtCurrency IsNot Nothing AndAlso dtCurrency.Rows.Count > 0), dtCurrency.Rows(0)("CurrencySymbol").ToString(), "")
            Dim DtCreditTender As DataTable = objCreditSales.SattleCreditSaleUsingSingleTender(TenderTypeCode, BillNumber, acceptAmount) 'vipin
            If Not DtCreditTender Is Nothing Then
                If DtCreditTender.Rows.Count > 0 Then
                    Dim CMRecptLineNo As Integer = IIf(DtCreditTender.Rows(0)(0) Is System.DBNull.Value, 1, DtCreditTender.Rows(0)(0))
                    For Each drCreditTender In DtCreditTender.Rows
                        Dim drReceipt As DataRow = dsCreditInvoice.Tables("CreditReceipt").NewRow
                        drReceipt("BillNo") = newBillNo
                        drReceipt("SiteCode") = clsAdmin.SiteCode
                        drReceipt("FinYear") = clsAdmin.Financialyear
                        drReceipt("RefBillNo") = BillNumber
                        drReceipt("TypeCode") = TranTypeCode

                        drReceipt("CMRecptLineNo") = CMRecptLineNo
                        drReceipt("TerminalID") = clsAdmin.TerminalID
                        drReceipt("CardNo") = txtReferenceNo.Text
                        drReceipt("BankAccNO") = DBNull.Value
                        drReceipt("ExchangeRate") = 1
                        drReceipt("TenderTypeCode") = TenderTypeCode
                        drReceipt("AmountTendered") = drCreditTender("AmountPaid") ''acceptAmount
                        drReceipt("CurrencyCode") = clsAdmin.CurrencyCode
                        drReceipt("AmountInCurrency") = drCreditTender("AmountPaid") ''acceptAmount
                        drReceipt("CmRcptDateTime") = clsAdmin.DayOpenDate
                        drReceipt("CreatedAt") = clsAdmin.SiteCode
                        drReceipt("CreatedBy") = clsAdmin.UserCode
                        drReceipt("CreatedOn") = ServerDate
                        drReceipt("UpdatedAt") = clsAdmin.SiteCode
                        drReceipt("UpdatedBy") = clsAdmin.UserCode
                        drReceipt("UpdatedOn") = ServerDate
                        drReceipt("Status") = True
                        'If dtInvoice.Columns.Contains("SaleInvNumber") = True Then   '' added by nikhil
                        '    drReceipt("RefBillInvNumber") = dtInvoice.Rows(i)("SaleInvNumber").ToString
                        'Else
                        '    drReceipt("RefBillInvNumber") = dtInvoice.Rows(i)("BillNo").ToString
                        'End If
                        drReceipt("RefBillInvNumber") = drCreditTender("SaleInvNumber")
                        drReceipt("TenderHeadCode") = TenderHeadCode
                        drReceipt("Remark") = txtRemark.Text 'vipin
                        dsCreditInvoice.Tables("CreditReceipt").Rows.Add(drReceipt)
                        CMRecptLineNo = CMRecptLineNo + 1
                    Next

                End If
            End If

            Return dsCreditInvoice

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    Public Shared Function CapitalizeWords(value As String) As String

        Try
            'Dim MyString = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value)
            'Return MyString
            Dim str As String = value
            str = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str.ToLower())
            Return str
            'Dim Emtxt As String = ""
            'Dim Remtxt As String = ""
            '' Dim val() As String

            '' val = value.Split(value)
            'Dim val As String() = value.Split(" "c)
            'Dim Txt As String = value
            'For Each i As String In val
            '    If i.ToString = "0" Then
            '        Emtxt = Emtxt + val(i).ToUpper
            '    Else
            '        Remtxt = Remtxt + i.ToString
            '    End If
            'Next
            'value = Emtxt + Remtxt
            'Return value
        Catch ex As Exception
            LogException(ex)
        End Try

        'If value Is Nothing Then
        '    Throw New ArgumentNullException("value")
        'End If
        'If value.Length = 0 Then
        '    Return value
        'End If

        'Dim result As New StringBuilder(value)
        'result(0) = Char.ToUpper(result(0))
        'For i As Integer = 0 To result.Length - 1
        '    If Char.IsWhiteSpace(result(i - 1)) Then
        '        result(i) = Char.ToUpper(result(i))
        '    Else
        '        result(i) = Char.ToLower(result(i))
        '    End If
        'Next
        'Return result.ToString()

    End Function

    Private newBillNo As String = String.Empty
    Private objBLLAcceptPayment As New clsAcceptPayment
    Dim objPrint As New frmNReprint
    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.None
            ' If IsAmountReturnTocustomer() = False Then
            _Actiontype = My.Resources.AcceptPaymentActionTypeSave.ToString()
            IsCancelAcceptPayment = False
            If (String.IsNullOrEmpty(txtCollectAmount.Text.Trim())) Then  'vipin 01122017
                txtCollectAmount.Text = 0
                ShowMessage("Please enter collect amount", "ACP006 - " & getValueByKey("CLAE04"))
                txtReferenceNo.Focus()
                Exit Sub
            End If
            If (CDbl(txtCollectAmount.Text.Trim) = 0) Then
                txtCollectAmount.Text = 0
                ShowMessage("Please enter collect amount", "ACP006 - " & getValueByKey("CLAE04"))
                txtReferenceNo.Focus()
                Exit Sub
            End If
            If DocumentType = "SO" Then
                If txtCollectAmount.Text.Trim < TotalMinAmount Then
                    ShowMessage(getValueByKey("ACP020"), "ACP020 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
            End If
            If DocumentType = "CMS" Then
                If txtCollectAmount.Text.Trim < txtTotalAmount.Text.Trim Then
                    ShowMessage("Payment not settled", "Information")
                    Exit Sub
                Else
                End If
            End If
            If (txtCollectAmount.Text > BalAmount) Then
                ShowMessage("Received amount cannot be greater than bill amount", "CHKP07 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            If Not (CheckInteger(txtCollectAmount.Text)) Then
                ShowMessage("Please Enter to Collect Amount", "ACP002 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If

            If (String.IsNullOrEmpty(txtReferenceNo.Text.Trim())) Then
                ShowMessage("Please enter reference number", "ACP006 - " & getValueByKey("CLAE04"))
                txtReferenceNo.Focus()
                Exit Sub
            End If
            '  BalAmount = txtCollectAmount.Text
            If DocumentType = "SO" Or DocumentType = "CMS" Then
                If DocumentType = "SO" Then
                    If _IsCashierPromoSelected = True AndAlso (String.IsNullOrEmpty(txtRemark.Text) Or HasAlphaNumericChar(txtRemark.Text) = False) Then
                        ShowMessage(getValueByKey("frmnacceptpaymentbycash.txtremarkvalid"), getValueByKey("CLAE04"))
                        IsCancelAcceptPayment = True
                    ElseIf TenderTypeCode.ToUpper = "NEFT" Then
                        If (PaymentTransactionByShortCutForms(BalAmount, SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Neft.ToString(), SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Neft, dsRecieptType, Nothing, txtReferenceNo.Text, Remark:=txtRemark.Text)) Then
                            Me.Close()
                        End If

                    ElseIf TenderTypeCode.ToUpper = "RTGS" Then
                        If (PaymentTransactionByShortCutForms(BalAmount, SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Rtgs.ToString(), SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Rtgs, dsRecieptType, Nothing, txtReferenceNo.Text, Remark:=txtRemark.Text)) Then
                            Me.Close()
                        End If
                    Else
                        ShowMessage(getValueByKey("ACPBCS01"), "ACPBCS01 - " & getValueByKey("CLAE05"))
                    End If
                ElseIf DocumentType = "CMS" Then
                    If _IsCashierPromoSelected = True AndAlso (String.IsNullOrEmpty(txtRemark.Text) Or HasAlphaNumericChar(txtRemark.Text) = False) Then
                        ShowMessage(getValueByKey("frmnacceptpaymentbycash.txtremarkvalid"), getValueByKey("CLAE04"))
                        IsCancelAcceptPayment = True
                    ElseIf TenderTypeCode = "Neft" Then
                        If (PaymentTransactionByShortCutForms(BalAmount, SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Neft.ToString(), SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Neft, dsRecieptType, txtRemark.Text, Nothing, txtReferenceNo.Text)) Then
                            Me.Close()
                        End If

                    ElseIf TenderTypeCode = "Rtgs" Then
                        If (PaymentTransactionByShortCutForms(BalAmount, SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Rtgs.ToString(), SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Rtgs, dsRecieptType, txtRemark.Text, Nothing, txtReferenceNo.Text)) Then
                            Me.Close()
                        End If
                    Else
                        ShowMessage(getValueByKey("ACPBCS01"), "ACPBCS01 - " & getValueByKey("CLAE05"))
                    End If
                End If
            Else
                Dim DocType As String = ""
                If BillNumber <> "" Then
                    DocType = BillNumber.Substring(0, 2)
                End If
                dsCreditInvoice = AddPaymentIntoDataTable(txtCollectAmount.Text, DocType)  ''' for credit sale data print
                If objCreditSales.UpdateCreditInvoice(dsCreditInvoice) Then
                    If FloatAmtReturned <> 0 Then
                        Dim BillDate = clsAdmin.DayOpenDate
                        Dim dsVoucher As DataSet
                        Dim objcm As New clsCashMemo
                        dsVoucher = objcm.GetVoucherFloatData(BillNumber, BillDate)

                        ' objCreditSales.SaveFloatAmtReturnedData(FloatAmtReturned, DeliveryPerson, BillNumber, InvoiceNumber, BillDate, clsAdmin.SiteCode, clsAdmin.UserCode, clsAdmin.Financialyear, dsVoucher)
                        objCreditSales.SaveFloatAmtReturnedData(FloatAmtReturned, DeliveryPerson, BillNumber, BillNumber, BillDate, clsAdmin.SiteCode, clsAdmin.UserCode, clsAdmin.Financialyear, dsVoucher) 'vipin
                    End If
                    Dim trans = PrepareData(dsCreditInvoice)
                    If clsDefaultConfiguration.IsNewCreditSale Then
                        '  dtHeader = ObjclsCommon.GetCreditSaleSettledDataToPrint(clsAdmin.SiteCode)
                        Dim AmtInRs As String = ""

                        AmtInRs = AmtInWord(txtCollectAmount.Text, Nothing, Nothing)
                        ' AmtInRs = objPrint.NumberToText(txtCollectAmount.Text)
                        If Not dtHeader Is Nothing AndAlso dtHeader.Rows.Count > 0 Then
                            '    objPrint.GenerateCreditSaleSettledReportPrint(clsAdmin.SiteCode, BalAmount, txtCollectAmount.Text, TenderTypeCode, txtRemark.Text, AmtInRs, clsAdmin.TerminalID, BillNumber, clsAdmin.UserName, dtHeader, BillNumber)
                        End If
                    Else
                        If clsDefaultConfiguration.KOTAndBillGeneration = False Then
                            Dim Printer As New clsPrintCreditSettlementNote()
                            Printer.PrintNote(trans, dtPrinterInfo, clsAdmin.CurrencyCode, clsAdmin.CurrencyDescription)
                            MessageBox.Show(getValueByKey("Crs008"))
                        End If
                    End If



                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show(getValueByKey("Crs009"))
                End If
            End If

            '   Else
            ' ShowMessage(getValueByKey("ACPBCS02"), "ACPBCS02 - " & getValueByKey("CLAE05"))
            '   End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    'Public Function RupeesInWords(ByVal Num As Decimal) As String
    '    'This two dimensional array store the primary word convertion of number.
    '    RupeesInWords = ""
    '    Dim ArrWordList(,) As Object = {{0, ""}, {1, "One"}, {2, "Two"}, {3, "Three"}, {4, "Four"}, _
    '                                    {5, "Five"}, {6, "Six"}, {7, "Seven"}, {8, "Eight"}, {9, "Nine"}, _
    '                                    {10, "Ten"}, {11, "Eleven"}, {12, "Twelve"}, {13, "Thirteen"}, {14, "Fourteen"}, _
    '                                    {15, "Fifteen"}, {16, "Sixteen"}, {17, "Seventeen"}, {18, "Eighteen"}, {19, "Nineteen"}, _
    '                                    {20, "Twenty"}, {30, "Thirty"}, {40, "Forty"}, {50, "Fifty"}, {60, "Sixty"}, _
    '                                    {70, "Seventy"}, {80, "Eighty"}, {90, "Ninety"}, {100, "Hundred"}, {1000, "Thousand"}, _
    '                                    {100000, "Lakh"}, {10000000, "Crore"}}

    '    Dim i As Integer
    '    For i = 0 To UBound(ArrWordList)
    '        If Num = ArrWordList(i, 0) Then
    '            RupeesInWords = ArrWordList(i, 1)
    '            Exit For
    '        End If
    '    Next
    '    Return RupeesInWords
    'End Function






    Private Function PrepareData(ByVal ds As DataSet) As List(Of SalesInvoice)
        Dim ServerDate = ObjclsCommon.GetCurrentDate()
        Dim ReceiptList As New List(Of SalesInvoice)
        Dim cnt As Int16 = 0
        For Each dr As DataRow In ds.Tables(0).Rows
            cnt = cnt + 1
            Dim salesinvoice As New SalesInvoice()
            salesinvoice.BillNO = dr("BillNo")
            salesinvoice.AmountTendered = dr("AmountTendered")
            salesinvoice.BankNO = dr("BankAccNO").ToString()
            salesinvoice.CardNO = dr("CardNo").ToString()
            salesinvoice.TerminalID = clsAdmin.TerminalID
            salesinvoice.AmountInCurrency = If(dr("AmountInCurrency").ToString() = "", 0.0, CDbl(dr("AmountInCurrency").ToString()))
            salesinvoice.CurrencyCode = dr("CurrencyCode").ToString()
            salesinvoice.DocType = dr("TypeCode")
            salesinvoice.ExchangeRate = If(dr("ExchangeRate").ToString() = "", 0.0, CDec(dr("ExchangeRate").ToString()))
            salesinvoice.FinYear = clsAdmin.Financialyear
            salesinvoice.InvoiceNumber = dr("RefBillNo")
            salesinvoice.IsNew = True
            salesinvoice.LineNo = cnt
            salesinvoice.RecTime = clsAdmin.DayOpenDate
            salesinvoice.SiteCode = clsAdmin.SiteCode
            salesinvoice.Status = True
            salesinvoice.TenderHeadCode = dr("TenderHeadCode")
            salesinvoice.TenderTypeCode = dr("TenderTypeCode")
            salesinvoice.CreatedAt = clsAdmin.SiteCode
            salesinvoice.CreatedBy = clsAdmin.UserCode
            salesinvoice.CreatedOn = ServerDate

            salesinvoice.UpdatedAt = clsAdmin.SiteCode
            salesinvoice.UpdatedBy = clsAdmin.UserCode
            salesinvoice.UpdatedOn = ServerDate
            ReceiptList.Add(salesinvoice)

        Next
        Return ReceiptList
    End Function

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Try
            IsCancelAcceptPayment = True
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub frmNAcceptPaymentByNEFTRTGS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Call setResources()
            Dim dtPayment = objBLLAcceptPayment.LoadRecieptType(PaymentType.Accept, clsAdmin.SiteCode)

            Dim dr() = dtPayment.Select("TenderType=  '" & TenderTypeCode & "'")

            If dr.Count > 0 Then
                TenderHeadCode = dr(0)("TenderHeadCode")
            End If
            txtCollectAmount.DataType = Type.GetType("System.Double")
            txtTotalAmount.Text = BillAmount
            txtBalAmount.Text = BalAmount
            txtCollectAmount.Text = BalAmount
            txtTotalAmount.Enabled = False
            txtBalAmount.Enabled = False
            If DocumentType = "CMS" Or DocumentType = "SO" Then
                '  Me.cmdOK.Width = 120
                'Me.cmdOK.Size = New System.Drawing.Size(75, 30)

                Me.cmdOK.Text = "Save and Print"
            Else
                Me.cmdOK.Text = "OK"
            End If
            If TenderTypeCode.ToUpper = "NEFT" Then
            Else
                lblReference.Text = "RTGS Reference No *"
            End If
            Select Case UCase(TenderTypeCode)
                Case "NEFT"
                    Me.Text = "Accept Payment By NEFT"
                Case "RTGS"
                    Me.Text = "Accept Payment By RTGS"
                Case Else

            End Select

            txtCollectAmount.Focus()
            txtCollectAmount.Select()


            If _IsCreditSale = False Then 'vipin
                txtBalAmount.Visible = False
                lblBalAmt.Visible = False
            End If


            If DocumentType = "SO" Then 'vipin 30112017
                txtCollectAmount.Enabled = False
            End If

            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Function Themechange()
        Me.BackColor = Color.FromArgb(134, 134, 134)
        cmdOK.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdOK.BackColor = Color.Transparent
        cmdOK.BackColor = Color.FromArgb(0, 107, 163)
        cmdOK.ForeColor = Color.FromArgb(255, 255, 255)
        cmdOK.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdOK.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdOK.FlatStyle = FlatStyle.Flat
        cmdOK.FlatAppearance.BorderSize = 0
        cmdOK.TextAlign = ContentAlignment.MiddleCenter

        cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdCancel.BackColor = Color.Transparent
        cmdCancel.BackColor = Color.FromArgb(0, 107, 163)
        cmdCancel.ForeColor = Color.FromArgb(255, 255, 255)
        cmdCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdCancel.FlatStyle = FlatStyle.Flat
        cmdCancel.FlatAppearance.BorderSize = 0
        cmdCancel.TextAlign = ContentAlignment.MiddleCenter


        lblTotalAmt.BackColor = Color.FromArgb(212, 212, 212)
        lblTotalAmt.AutoSize = False
        '  lblTotalAmt.Size = New Size(88, 12)
        lblTotalAmt.Font = New Font("Neo Sans", 8, FontStyle.Regular)


        lblBalAmt.BackColor = Color.FromArgb(212, 212, 212)
        lblBalAmt.AutoSize = False
        '  lblBalAmt.Size = New Size(88, 12)
        lblBalAmt.Font = New Font("Neo Sans", 8, FontStyle.Regular)




        lblReference.BackColor = Color.FromArgb(212, 212, 212)
        lblReference.AutoSize = False
        '  lblReference.Size = New Size(88, 12)
        lblReference.Font = New Font("Neo Sans", 8, FontStyle.Regular)


        lblCollectAmt.BackColor = Color.FromArgb(212, 212, 212)
        lblCollectAmt.AutoSize = False
        lblCollectAmt.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        '    lblCollectAmt.Size = New Size(88, 12)

        lblRemark.BackColor = Color.FromArgb(212, 212, 212)
        lblRemark.AutoSize = False
        lblRemark.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        '  lblRemark.Size = New Size(88, 12)


    End Function

    Private Sub setResources()
        Try
            Me.Text = getValueByKey("frmPaymentCreditSales")
            Me.lblTotalAmt.Text = getValueByKey("frmPaymentCreditSales.lbltotalAmount")
            Me.lblCollectAmt.Text = getValueByKey("frmPaymentCreditSales.lblCollectAmount")
            Me.lblRemark.Text = getValueByKey("frmPaymentCreditSales.lblRemark")
            Me.cmdOK.Text = getValueByKey("frmPaymentCreditSales.btnOk")
            Me.cmdCancel.Text = getValueByKey("frmPaymentCreditSales.btnExit")
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub txtRemark_KeyDown(sender As Object, e As KeyEventArgs)
        Try
            ''Dim result As String = CapitalizeWords(txtRemark.Text)
            ''txtRemark.SelectionStart = Len(result)
            'Dim str As String = txtRemark1.Text
            'txtRemark1.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str.ToLower())
            'txtRemark1.SelectionStart = Len(txtRemark1.Text)
            'txtRemark.Select()

            'txtRemark.Text = result.ToString
            'txtRemark.Focus()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtRemark1_TextChanged(sender As Object, e As EventArgs)
        Dim str As String = txtRemark.Text
        txtRemark.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str.ToLower())
        txtRemark.SelectionStart = Len(txtRemark.Text)
    End Sub

    Private Sub txtRemark_TextChanged(sender As Object, e As EventArgs) Handles txtRemark.TextChanged
        Dim str As String = txtRemark.Text
        txtRemark.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str.ToLower())
        txtRemark.SelectionStart = Len(txtRemark.Text)
    End Sub
    Private Sub txtReferenceNo_keyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtReferenceNo.KeyPress
        If Not Char.IsLetterOrDigit(e.KeyChar) Then
            If e.KeyChar = Convert.ToChar(8) Then
            Else
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub txtCollectAmount_keyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCollectAmount.KeyPress
        If Not Char.IsNumber(e.KeyChar) Then
            If e.KeyChar = Convert.ToChar(8) Then
            Else
                e.Handled = True
            End If
        End If
    End Sub
End Class