Imports SpectrumBL
Imports SpectrumBL.clsAcceptPayment
Imports SpectrumPrint
Imports SpectrumCommon
Imports System.Text

Public Class frmNAcceptPaymentByDiscount

#Region "Global Variables"
    Dim dtCreditWriteOff As DataTable
    Dim ObjclsCommon As New clsCommon
    Dim objCreditSales As New ClsCreditSale
    Private WriteOffNo As String = String.Empty

    Private newBillNo As String = String.Empty
    Private objBLLAcceptPayment As New clsAcceptPayment
    Dim objPrint As New frmNReprint
    Dim dtHeader As New DataTable
#End Region


#Region "Properties"
    Private dsCreditInvoice As DataTable
    Private _billAmount As Double
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
#End Region

   

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

    Private Function IsAmountReturnTocustomer() As Boolean
        Try
            If (txtDiscountAmount.Text > BalAmount) Then
                Dim ReturnAmt As Decimal
                ReturnAmt = Format(txtDiscountAmount.Text - CDbl(BalAmount), "0.00")
                Dim strshowmsg As String = ""
                ShowMessage(True, String.Format(getValueByKey("ACPBCS07"), BalAmount, txtDiscountAmount.Text, ReturnAmt, clsAdmin.CurrencyCode), "ACPBCS07 - " & getValueByKey("CLAE04"))
                txtDiscountAmount.Text = CDbl(txtDiscountAmount.Text) - ReturnAmt
                Return True
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Private Function AddWriteOffDataIntoDataTable(ByVal DiscAmt As Double, ByVal remark As String) As DataTable
        Try
            Dim objCM As New clsCashMemo()
            Dim ServerDate = ObjclsCommon.GetCurrentDate()
            Dim objType = "FO_DOC"
            Dim docno As String = objCM.getDocumentNo("CreditSaleWriteOff", clsAdmin.SiteCode, objType)
            WriteOffNo = GenDocNo("WO" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)

            Dim dtCurrency = New clsAcceptPayment().LoadCurrency(clsAdmin.SiteCode)
            Dim currencySymbol As String = IIf((dtCurrency IsNot Nothing AndAlso dtCurrency.Rows.Count > 0), dtCurrency.Rows(0)("CurrencySymbol").ToString(), "")
            dtCreditWriteOff = objCreditSales.GetBillWriteOffDtls()

            Dim DtDiscountDtl As DataTable = objCreditSales.SattleCreditSaleUsingSingleTender("WriteOff", BillNumber, DiscAmt)     'vipin 11.10.2017
            If Not DtDiscountDtl Is Nothing Then
                If DtDiscountDtl.Rows.Count > 0 Then
                    For Each DrDiscDtl In DtDiscountDtl.Rows
                        Dim drWriteOff As DataRow = dtCreditWriteOff.NewRow()
                        drWriteOff("WriteOffNumber") = WriteOffNo
                        drWriteOff("SiteCode") = clsAdmin.SiteCode
                        drWriteOff("FinYear") = clsAdmin.Financialyear
                        drWriteOff("WriteOffDateTime") = clsAdmin.DayOpenDate
                        drWriteOff("WriteOffLineNo") = 1
                        drWriteOff("TypeCode") = TranTypeCode
                        drWriteOff("TerminalID") = clsAdmin.TerminalID
                        drWriteOff("RefBillNo") = BillNumber
                        ' drWriteOff("RefBillInvNumber") = InvoiceNumber
                        drWriteOff("RefBillInvNumber") = DrDiscDtl("SaleinvNumber").ToString
                        drWriteOff("ExchangeRate") = 1
                        'drWriteOff("AmountTendered") = DiscAmt
                        'drWriteOff("DiscAmountTendered") = DiscAmt
                        drWriteOff("AmountTendered") = DrDiscDtl("AmountPaid")
                        drWriteOff("DiscAmountTendered") = DrDiscDtl("AmountPaid")
                        drWriteOff("CurrencyCode") = clsAdmin.CurrencyCode
                        drWriteOff("AmountInCurrency") = DiscAmt
                        drWriteOff("Remark") = remark
                        drWriteOff("TenderType") = "Discount"  'vipin
                        drWriteOff("CreatedAt") = clsAdmin.SiteCode
                        drWriteOff("CreatedBy") = clsAdmin.UserCode
                        drWriteOff("CreatedOn") = ServerDate
                        drWriteOff("UpdatedAt") = clsAdmin.SiteCode
                        drWriteOff("UpdatedBy") = clsAdmin.UserCode
                        drWriteOff("UpdatedOn") = ServerDate
                        drWriteOff("Status") = True
                        dtCreditWriteOff.Rows.Add(drWriteOff)
                    Next
                End If        'vipin
            End If
            Return dtCreditWriteOff

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    Public Shared Function CapitalizeWords(value As String) As String
        If value Is Nothing Then
            Throw New ArgumentNullException("value")
        End If
        If value.Length = 0 Then
            Return value
        End If

        Dim result As New StringBuilder(value)
        result(0) = Char.ToUpper(result(0))
        For i As Integer = 1 To result.Length - 1
            If Char.IsWhiteSpace(result(i - 1)) Then
                result(i) = Char.ToUpper(result(i))
            Else
                result(i) = Char.ToLower(result(i))
            End If
        Next
        Return result.ToString()

    End Function
    Public Shared Function Capitalize(value As String) As String
        Return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value)
    End Function

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.None

            If txtBalAmount.Text.Trim = txtDiscountAmount.Text.Trim Then
            Else
                ShowMessage("Discount amount should be same as balance amount", "ACP002 - " & getValueByKey("CLAE04"))
                ' txtDiscountAmount.Clear()
                Exit Sub
            End If
            
            If MsgBox("Are you sure want to discount the due amount?", MsgBoxStyle.YesNo, "CM036 - " & getValueByKey("CLAE04")) = MsgBoxResult.No Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
                Exit Sub
            End If
            dtCreditWriteOff = AddWriteOffDataIntoDataTable(txtDiscountAmount.Text, txtRemark.Text)
            If objCreditSales.UpdateCreditSaleWriteOff(dtCreditWriteOff) Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
                'Else
                '    MessageBox.Show(getValueByKey("Crs009"))
                'End If

                dtHeader = ObjclsCommon.GetCreditSaleSettledDataToPrint(clsAdmin.SiteCode)
                Dim AmtInRs As String = ""
                AmtInRs = NumberToText(txtDiscountAmount.Text)
                If Not dtHeader Is Nothing AndAlso dtHeader.Rows.Count > 0 Then
                    objPrint.GenerateCreditSaleSettledReportPrint(clsAdmin.SiteCode, txtTotalAmount.Text, txtDiscountAmount.Text, "Discount", TxtRemark.Text, AmtInRs, clsAdmin.TerminalID, BillNumber, clsAdmin.UserName, dtHeader, BillNumber, True)
                End If

            Else 'vipin
                MessageBox.Show(getValueByKey("Crs009"))
            End If
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
            'Else
            'MessageBox.Show(getValueByKey("Crs009"))
            'End If
            'Else
            '    ShowMessage(getValueByKey("ACPBCS02"), "ACPBCS02 - " & getValueByKey("CLAE05"))
            'End If
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
    Public Function NumberToText(ByVal num As Long) As String
        Dim BigNumbers() As String = {"", " Thousand", " Million", " Billion", " Trillion"}
        Dim TextParts() As String = {}
        If num < 0 Then
            Return "Checks cannot be written for negative amounts."
        ElseIf num >= 10 ^ ((BigNumbers.Length) * 3) Then
            Return "This number exceeds the current maximum value of " & NumberToText(10 ^ ((BigNumbers.Length) * 3) - 1) & "."
        End If
        Dim LoopCount As Integer = 0
        While num >= 1000
            ReDim Preserve TextParts(TextParts.Length)
            If num Mod 1000 > 0 Then
                TextParts(TextParts.GetUpperBound(0)) = ThreeDigitText(num Mod 1000) & BigNumbers(LoopCount)
            End If
            num = num \ 1000
            LoopCount += 1
        End While
        ReDim Preserve TextParts(TextParts.Length)
        TextParts(TextParts.GetUpperBound(0)) = ThreeDigitText(num) & BigNumbers(LoopCount)
        If Array.IndexOf(TextParts, "Error") > -1 Then
            Return "An unknown error occurred while converting this number to text."
        Else
            Array.Reverse(TextParts)
            Return Join(TextParts)
        End If
    End Function

    Private Function ThreeDigitText(ByVal num As Integer) As String
        If num > 999 Or num < 0 Then
            Return "Error"
        Else
            Dim h As Integer = num \ 100 'Hundreds place
            Dim tempText As String = ""
            If h > 0 Then
                tempText = OneDigitText(h) & " Hundred"
            End If
            num -= h * 100
            If num > 0 And Not tempText = "" Then
                tempText &= " "
            End If
            If num > 9 And num < 20 Then
                Dim DoubleDigits() As String = {"Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"}
                Return tempText & DoubleDigits(num - 10)
            Else
                Dim TensPlace() As String = {"Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"}
                Dim t As Integer = num \ 10 'Tens place
                num -= t * 10
                If t > 1 Then
                    tempText &= TensPlace(t - 2)
                    If num > 0 Then
                        tempText &= " "
                    End If
                End If
                If num > 0 Then
                    tempText &= OneDigitText(num)
                End If
                Return tempText
            End If
        End If
    End Function

    Private Function OneDigitText(ByVal num As Integer) As String
        If num > 9 Or num < 0 Then
            Return "Error"
        Else
            Dim SingleDigits() As String = {"Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine"}
            Return SingleDigits(num)
        End If
    End Function



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
            Me.Close()
        Catch ex As Exception
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
        '  lblTotalAmt.Size = New Size(88, 8)
        lblTotalAmt.Font = New Font("Neo Sans", 8)


        lblCollectAmt.BackColor = Color.FromArgb(212, 212, 212)
        lblCollectAmt.AutoSize = False
        lblCollectAmt.Font = New Font("Neo Sans", 8)
        '  lblCollectAmt.Size = New Size(88, 8)

        lblRemark.BackColor = Color.FromArgb(212, 212, 212)
        lblRemark.AutoSize = False
        lblRemark.Font = New Font("Neo Sans", 8)
        ' lblRemark.Size = New Size(88, 8)

        'lblBalAmt

        lblBalAmt.BackColor = Color.FromArgb(212, 212, 212)
        lblBalAmt.AutoSize = False
        lblBalAmt.Font = New Font("Neo Sans", 8)





    End Function

    Private Sub setResources()
        Try
            Me.Text = "Apply Discount on Credit Sale"
            Me.lblTotalAmt.Text = getValueByKey("frmPaymentCreditSales.lbltotalAmount")
            Me.lblCollectAmt.Text = getValueByKey("frmNAcceptPaymentByDiscount.lblCollectAmount")
            Me.lblRemark.Text = getValueByKey("frmPaymentCreditSales.lblRemark")
            Me.cmdOK.Text = getValueByKey("frmPaymentCreditSales.btnOk")
            Me.cmdCancel.Text = getValueByKey("frmPaymentCreditSales.btnExit")
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub txtRemark_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRemark.KeyDown
        Try
            'Dim result As String = CapitalizeWords(txtRemark.Text)
            ' txtRemark.Text = result.ToString
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub frmNAcceptPaymentByDiscount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            lblCollectAmt.Visible = True
            '' Call setResources()
            Dim dtPayment = objBLLAcceptPayment.LoadRecieptType(PaymentType.Accept, clsAdmin.SiteCode)

            Dim dr() = dtPayment.Select("TenderType=  '" & TenderTypeCode & "'")

            If dr.Count > 0 Then
                TenderHeadCode = dr(0)("TenderHeadCode")
            End If

            txtDiscountAmount.DataType = Type.GetType("System.Double")
            txtTotalAmount.Text = BillAmount
            txtBalAmount.Text = BalAmount
            txtDiscountAmount.Text = BalAmount
            txtTotalAmount.Enabled = False
            txtBalAmount.Enabled = False
            txtDiscountAmount.Focus()
            txtDiscountAmount.Select()

            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub txtRemark_KeyDown(sender As System.Object, e As System.EventArgs) Handles TxtRemark.KeyDown
        Try
            Dim str As String = txtRemark.Text
            txtRemark.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str.ToLower())
            txtRemark.SelectionStart = Len(txtRemark.Text)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub txtDiscountAmount_keyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDiscountAmount.KeyPress
        If Not Char.IsNumber(e.KeyChar) Then
            If e.KeyChar = Convert.ToChar(8) Then
            Else
                e.Handled = True
            End If
        End If
    End Sub
End Class