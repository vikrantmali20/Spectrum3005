Imports SpectrumBL
Imports SpectrumBL.clsAcceptPayment
Imports SpectrumPrint
Imports SpectrumCommon

Public Class frmPaymentCreditSales
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

    Private _billAmount As Double
    Public Property BillAmount() As Double
        Get
            Return _billAmount
        End Get
        Set(ByVal value As Double)
            _billAmount = value
        End Set
    End Property
    'added by khusroa adil on 26-07-2017
    Private _allowInnovitiPayment As Boolean = False
    Public Property AllowInnovitiPayment() As Boolean
        Get
            Return _allowInnovitiPayment
        End Get
        Set(ByVal value As Boolean)
            _allowInnovitiPayment = value
        End Set
    End Property
    'added by khusrao adil on 21-06-2017 for natural juhu CR
    Private _CSAAuthUserName As String

    Public Property CSAAuthUserName() As String
        Get
            Return _CSAAuthUserName
        End Get
        Set(ByVal value As String)
            _CSAAuthUserName = value
        End Set
    End Property
    ''added by ashma for innoviti
    'Private _CardNumber As String
    'Public Property CardNumber() As String
    '    Get
    '        Return _CardNumber
    '    End Get
    '    Set(ByVal value As String)
    '        _CardNumber = value
    '    End Set
    'End Property

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
    'added by ashma for innoviti
    'added by khusrao adil on 05-07-2017 for innovati
    Private _CardNumber As String
    Public Property CardNumber() As String
        Get
            Return _CardNumber
        End Get
        Set(ByVal value As String)
            _CardNumber = value
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
    Private _DeliveryPartner As String
    Public Property DeliveryPartner() As String
        Get
            Return _DeliveryPartner
        End Get
        Set(ByVal value As String)
            _DeliveryPartner = value
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

    Private newBillNo As String = String.Empty
    Private objBLLAcceptPayment As New clsAcceptPayment

    Private Sub frmPaymentCreditSales_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            AllowInnovitiPayment = False
            Call setResources()
            Dim dtPayment = objBLLAcceptPayment.LoadRecieptType(PaymentType.Accept, clsAdmin.SiteCode)

            Dim dr() = dtPayment.Select("TenderType=  '" & TenderTypeCode & "'")

            If dr.Count > 0 Then
                TenderHeadCode = dr(0)("TenderHeadCode")
            End If
            txtCollectAmount.DataType = Type.GetType("System.Double")


            txttotalAmount.Text = BillAmount
            txtCollectAmount.Text = BalAmount
            CtrlChequeDetails1.Visible = False
            CtrlCreditCard1.Visible = False

            Select Case UCase(TenderTypeCode)
                Case "CASH"
                    Me.Text = getValueByKey("frmPaymentCreditSales.Title.TenderTypeCode.Cash")
                    btnCard.Visible = False
                    btnCardInnoviti.Visible = False
                    AllowInnovitiPayment = False
                Case "CHEQUE"
                    Me.Text = getValueByKey("frmPaymentCreditSales.Title.TenderTypeCode.Cheque")
                    CtrlChequeDetails1.Visible = True
                    CtrlCreditCard1.Visible = False
                    btnCard.Visible = False
                    btnCardInnoviti.Visible = False
                    AllowInnovitiPayment = False
                Case "CREDITCARD"
                    Me.Text = getValueByKey("frmPaymentCreditSales.Title.TenderTypeCode.CREDITCARD")
                    CtrlChequeDetails1.Visible = False
                    CtrlCreditCard1.Visible = True
                    '----------------------------------------------------------------------------------------------
                    'added on 15 may - ashma - for Innoviti (for hiding Credit Card No)
                    If clsDefaultConfiguration.PayFromInnoviti = True And clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) Then
                        'CtrlCreditCard1.Visible = False
                        ' txttotalAmount.Enabled = False
                        'txtCollectAmount.Enabled = False

                        btnCard.Visible = True
                        btnCardInnoviti.Visible = True
                        btnCardInnoviti.Focus()
                    Else
                        btnCard.Visible = False
                        btnCardInnoviti.Visible = False
                        txtCollectAmount.Enabled = True
                        CtrlCreditCard1.Visible = True
                        txttotalAmount.Enabled = True
                    End If
                    '----------------------------------------------------------------------------------------------
                Case Else

            End Select

            txtCollectAmount.Focus()
            txtCollectAmount.Select()

            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub setResources()
        Try
            Me.Text = getValueByKey("frmPaymentCreditSales")
            Me.lbltotalAmount.Text = getValueByKey("frmPaymentCreditSales.lbltotalAmount")
            Me.lblCollectAmount.Text = getValueByKey("frmPaymentCreditSales.lblCollectAmount")
            Me.lblRemark.Text = getValueByKey("frmPaymentCreditSales.lblRemark")
            Me.btnOk.Text = getValueByKey("frmPaymentCreditSales.btnOk")
            Me.btnExit.Text = getValueByKey("frmPaymentCreditSales.btnExit")
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub frmPaymentCreditSales_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Dim ObjclsCommon As New clsCommon
    Private dsCreditInvoice As DataSet

    Private Function AddPaymentIntoDataTable(ByVal acceptAmount As Double) As DataSet
        Try
            Dim objCM As New clsCashMemo()
            Dim ServerDate = ObjclsCommon.GetCurrentDate()
            Dim objType = "FO_DOC"
            Dim docno As String = objCM.getDocumentNo("SalesInvoice", clsAdmin.SiteCode, objType)
            'newBillNo = GenDocNo("SI" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
            ' newBillNo = GenDocNo("SI" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
            newBillNo = GenDocNo("S" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 3, 3) & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)

            Dim dtCurrency = New clsAcceptPayment().LoadCurrency(clsAdmin.SiteCode)
            Dim currencySymbol As String = IIf((dtCurrency IsNot Nothing AndAlso dtCurrency.Rows.Count > 0), dtCurrency.Rows(0)("CurrencySymbol").ToString(), "")

            dsCreditInvoice = objCreditSales.GetBillInvoiceDtls()

            Dim drReceipt As DataRow = dsCreditInvoice.Tables("CreditReceipt").NewRow

            drReceipt("BillNo") = newBillNo
            drReceipt("SiteCode") = clsAdmin.SiteCode
            drReceipt("FinYear") = clsAdmin.Financialyear
            drReceipt("RefBillNo") = BillNumber
            drReceipt("TypeCode") = TranTypeCode
            drReceipt("CMRecptLineNo") = 1
            drReceipt("TerminalID") = clsAdmin.TerminalID
            drReceipt("CardNo") = String.Format("{0} {1}", currencySymbol, FormatNumber(acceptAmount, 2))
            drReceipt("BankAccNO") = DBNull.Value
            drReceipt("ExchangeRate") = 1
            drReceipt("TenderTypeCode") = TenderTypeCode
            drReceipt("AmountTendered") = acceptAmount
            drReceipt("CurrencyCode") = clsAdmin.CurrencyCode
            drReceipt("AmountInCurrency") = acceptAmount
            drReceipt("CmRcptDateTime") = clsAdmin.DayOpenDate
            drReceipt("CreatedAt") = clsAdmin.SiteCode
            drReceipt("CreatedOn") = ServerDate
            drReceipt("UpdatedAt") = clsAdmin.SiteCode
            drReceipt("UpdatedOn") = ServerDate
            If clsDefaultConfiguration.AuthReqForCreditSalesAdjustmentOnCSA = True Then
                drReceipt("CreatedBy") = CSAAuthUserName
                drReceipt("UpdatedBy") = CSAAuthUserName
            Else
                drReceipt("CreatedBy") = clsAdmin.UserCode
                drReceipt("UpdatedBy") = clsAdmin.UserCode
            End If
            drReceipt("Status") = True
            drReceipt("RefBillInvNumber") = InvoiceNumber
            Select Case UCase(TenderTypeCode)
                Case "CHEQUE"
                    drReceipt("CardNo") = CtrlChequeDetails1.txtChequeNo.Text
                    drReceipt("BankAccNO") = CtrlChequeDetails1.cmbBankName.SelectedValue
                    drReceipt("TenderHeadCode") = TenderHeadCode
                Case "CREDITCARD"
                    'added for Innoviti
                    If clsDefaultConfiguration.PayFromInnoviti = True And clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) AndAlso AllowInnovitiPayment = True Then
                        drReceipt("CardNo") = _CardNumber
                    Else
                        drReceipt("CardNo") = CtrlCreditCard1.txtCreditCardNo.Text
                        drReceipt("BankAccNO") = CtrlCreditCard1.cmbBankName.SelectedValue
                    End If
                    ' drReceipt("BankAccNO") = CtrlCreditCard1.cmbBankName.SelectedValue
                    drReceipt("TenderHeadCode") = TenderHeadCode
                Case "CASH"
                    drReceipt("TenderHeadCode") = TenderHeadCode
                Case Else

            End Select

            dsCreditInvoice.Tables("CreditReceipt").Rows.Add(drReceipt)
            Return dsCreditInvoice

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function


    Private Sub btnCard_Click(sender As Object, e As EventArgs) Handles btnCard.Click
        Me.btnCard.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnCard.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnCardInnoviti.BackColor = Color.WhiteSmoke
        Me.btnCardInnoviti.ForeColor = Color.Black

        txttotalAmount.Text = BillAmount
        txtCollectAmount.Text = BalAmount
        CtrlChequeDetails1.Visible = False
        txtCollectAmount.Enabled = True
        CtrlCreditCard1.Visible = True
        txttotalAmount.Enabled = True
        AllowInnovitiPayment = False
    End Sub
    Private Sub btnCardInnoviti_Click(sender As Object, e As EventArgs) Handles btnCardInnoviti.Click
        Me.btnCardInnoviti.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnCardInnoviti.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnCard.BackColor = Color.WhiteSmoke
        Me.btnCard.ForeColor = Color.Black

        txttotalAmount.Text = BillAmount
        txtCollectAmount.Text = BalAmount
        CtrlChequeDetails1.Visible = False
        txtCollectAmount.Enabled = False
        CtrlCreditCard1.Visible = False
        txttotalAmount.Enabled = False
        AllowInnovitiPayment = True
    End Sub
    Private Sub btnOk_Click(sender As System.Object, e As System.EventArgs) Handles btnOk.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.None

            'If Val(txtCollectAmount.Text) <= 0 Or Val(txtCollectAmount.Text) > BalAmount Then
            '    ShowMessage(getValueByKey("PCS.0001") & BalAmount, getValueByKey("CLAE04"))
            '    Exit Sub
            'End If
            'Code is added by irfan on 01032018 for payment settlement in CSA Screen=========
            'code commented by vipul for CSA payment issue (we can settle lower amount than actual bill amount in credit sale)
            If TenderTypeCode <> "CASH" Then 'cash conditionadded by vipin to restict more payment other than cash
                If (txtCollectAmount.Text > BalAmount) Then
                    ShowMessage(getValueByKey("ACPCD01"), "ACPCD01 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
            End If
            'If (txtCollectAmount.Text < BalAmount) Then
            '    ShowMessage(getValueByKey("ACPCD01"), "ACPCD01 - " & getValueByKey("CLAE04"))
            '    Exit Sub
            'End If
            '================================================================================
            If IsAmountReturnTocustomer() Then
                Select Case UCase(TenderTypeCode)
                    Case "CHEQUE"
                        Dim ChequeExpiryDays As Int32 = clsDefaultConfiguration.CheckExpiryMonth
                        Dim prviousday As DateTime = DateAdd(DateInterval.Month, ChequeExpiryDays * -1, Now)
                        Dim futureday As DateTime = DateAdd(DateInterval.Month, ChequeExpiryDays, Now)

                        If (clsDefaultConfiguration.ChequeInfomation) Then
                            If String.IsNullOrEmpty(CtrlChequeDetails1.cmbBankName.SelectedValue) Then
                                ShowMessage(getValueByKey("CHKP09"), "CHKP09 - " & getValueByKey("CLAE04"))
                                CtrlChequeDetails1.cmbBankName.Focus()
                                Exit Sub

                            ElseIf String.IsNullOrEmpty(CtrlChequeDetails1.txtChequeNo.Text) Then
                                ShowMessage(getValueByKey("CHKP06"), "CHKP06 - " & getValueByKey("CLAE04"))
                                CtrlChequeDetails1.txtChequeNo.Focus()
                                Exit Sub

                            ElseIf String.IsNullOrEmpty(CtrlChequeDetails1.txtMicrNumber.Text) Then
                                ShowMessage(getValueByKey("CHKP10"), "CHKP10 - " & getValueByKey("CLAE04"))
                                CtrlChequeDetails1.txtMicrNumber.Focus()
                                Exit Sub
                            ElseIf CtrlChequeDetails1.dtChequeDate.Value IsNot DBNull.Value _
                                        AndAlso (CtrlChequeDetails1.dtChequeDate.Value > futureday _
                                                 Or CtrlChequeDetails1.dtChequeDate.Value < prviousday) Then
                                ShowMessage(getValueByKey("CHKP05"), "CHKP05 - " & getValueByKey("CLAE04"))
                                CtrlChequeDetails1.dtChequeDate.Focus()
                                Exit Sub
                            End If
                        End If
                    Case "CREDITCARD"
                        If Not (CheckInteger(txtCollectAmount.Text)) Then
                            ShowMessage(getValueByKey("ACP002"), "ACP002 - " & getValueByKey("CLAE04"))
                            Exit Sub
                        End If
                        'If Not cboSelectCardType.SelectedIndex > -1 Then
                        '    ShowMessage(getValueByKey("ACP017"), "ACP017 - " & getValueByKey("CLAE04"))
                        '    Exit Sub
                        'End If

                        '----------------------------------------------------------------------------------------------
                        'added on 12 may - ashma - for Innoviti (If condition)
                        If Not clsDefaultConfiguration.PayFromInnoviti Then

                            If (String.IsNullOrEmpty(CtrlCreditCard1.txtCreditCardNo.Text.Trim())) Then
                                ShowMessage(getValueByKey("ACP006"), "ACP006 - " & getValueByKey("CLAE04"))
                                CtrlCreditCard1.txtCreditCardNo.Focus()
                                Exit Sub

                            ElseIf Not (CtrlCreditCard1.txtCreditCardNo.Text.Trim().Length >= 4 AndAlso CtrlCreditCard1.txtCreditCardNo.Text.Trim().Length <= 16) Then
                                ShowMessage(getValueByKey("ACP021"), "ACP021 - " & getValueByKey("CLAE04"))
                                CtrlCreditCard1.txtCreditCardNo.Focus()
                                Exit Sub
                            End If
                        End If
                        '----------------------------------------------------------------------------------------------

                    Case "CASH"

                    Case Else

                End Select
                'added by khusrao adil on 05-07-2017 for innovati
                Dim objCM As New clsCashMemo()
                Dim objType = "FO_DOC"
                Dim docno As String = objCM.getDocumentNo("SalesInvoice", clsAdmin.SiteCode, objType)
                newBillNo = GenDocNo("SI" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)

                Dim RetrievalReferenceNumber As String
                Dim TransactionTime As String
                If TenderTypeCode.ToUpper = "CREDITCARD".ToUpper Then
                    ''----------------------------------------------------------------------------------------------
                    ''added on 12 may - ashma - for Innoviti (If condition)
                    If clsDefaultConfiguration.PayFromInnoviti = True And clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) AndAlso AllowInnovitiPayment = True Then
                        Dim responseFromInnov As New Dictionary(Of String, String)
                        responseFromInnov = modCommanFunction.PaymentToCallEDC(newBillNo, txtCollectAmount.Text.Trim)
                        If responseFromInnov Is Nothing Then
                            Dim ex As New Exception(modCommanFunction.InnovitiResponseError.ToString())
                            LogException(ex)
                            ShowMessage("Error in Card payment", "Error")
                            Exit Sub
                        Else
                            For Each kvp As KeyValuePair(Of String, String) In responseFromInnov
                                If kvp.Key = "RetrievalReferenceNumber" Then
                                    RetrievalReferenceNumber = kvp.Value.ToUpper
                                ElseIf kvp.Key = "TransactionTime" Then
                                    TransactionTime = kvp.Value.ToUpper
                                ElseIf kvp.Key = "CardNumber" Then
                                    _CardNumber = kvp.Value.ToUpper
                                End If
                            Next
                        End If
                    End If
                End If
                '----------------------------------------------------------------------------------------------

                dsCreditInvoice = AddPaymentIntoDataTable(txtCollectAmount.Text)


                ''added on 12 may - ashma - for Innoviti
                If objCreditSales.UpdateCreditInvoice(dsCreditInvoice, Billno:=newBillNo, SiteCode:=clsAdmin.SiteCode, finyear:=clsAdmin.Financialyear, _
                                                      DocumentType:="CSA", UserId:=clsAdmin.UserCode, RetrievalReferenceNumber:=RetrievalReferenceNumber, _
                                                      TransactionTime:=TransactionTime, PayByInnovitii:=clsDefaultConfiguration.PayFromInnoviti, _
                                                       InnovatiiForTerminals:=clsDefaultConfiguration.InnovitiForTerminals, TerminalId:=clsAdmin.TerminalID, _
                                                      AllowInnovitiPayment:=AllowInnovitiPayment) Then
                    'If objCreditSales.UpdateCreditInvoice(dsCreditInvoice) Then
                    '-----------Saving Data of Float Return to Petty cash Entry
                    If FloatAmtReturned <> 0 Then
                        Dim BillDate = clsAdmin.DayOpenDate
                        Dim dsVoucher As DataSet

                        dsVoucher = objCM.GetVoucherFloatData(BillNumber, BillDate)

                        objCreditSales.SaveFloatAmtReturnedData(FloatAmtReturned, DeliveryPerson, BillNumber, InvoiceNumber, BillDate, clsAdmin.SiteCode, clsAdmin.UserCode, clsAdmin.Financialyear, dsVoucher)
                    End If
                    Dim trans = PrepareData(dsCreditInvoice)
                    'If clsDefaultConfiguration.KOTAndBillGeneration = False Then
                    '    Dim Printer As New clsPrintCreditSettlementNote()
                    '    Printer.PrintNote(trans, dtPrinterInfo, clsAdmin.CurrencyCode, clsAdmin.CurrencyDescription)
                    '    MessageBox.Show(getValueByKey("Crs008"))
                    'End If
                    If clsDefaultConfiguration.IsNewCreditSale Then    '''''''  added by nikhil for PC '''''

                        Dim dtHeader As New DataTable
                        Dim objPrint As New frmNReprint
                        dtHeader = ObjclsCommon.GetCreditSaleSettledDataToPrint(clsAdmin.SiteCode)
                        Dim AmtInRs As String = ""
                        AmtInRs = AmtInWord(txtCollectAmount.Text, Nothing, Nothing) '' NumberToText
                        If Not dtHeader Is Nothing AndAlso dtHeader.Rows.Count > 0 Then
                            ' objPrint.GenerateCreditSaleSettledReportPrint(clsAdmin.SiteCode, txttotalAmount.Text, txtCollectAmount.Text, TenderTypeCode, txtRemark.Text, AmtInRs, clsAdmin.TerminalID, InvoiceNumber, clsAdmin.UserName, dtHeader, BillNumber)
                            objPrint.GenerateCreditSaleSettledReportPrint(clsAdmin.SiteCode, BalAmount, txtCollectAmount.Text, TenderTypeCode, txtRemark.Text, AmtInRs, clsAdmin.TerminalID, BillNumber, clsAdmin.UserName, dtHeader, BillNumber) 'vipin
                            MessageBox.Show(getValueByKey("Crs008"))
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
            Else
                ShowMessage(getValueByKey("ACPBCS02"), "ACPBCS02 - " & getValueByKey("CLAE05"))
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function IsAmountReturnTocustomer() As Boolean
        Try
            'If _customerpay > 0 Then
            '    If TotalCollectAmount > _customerpay Then
            '        Dim ReturnAmt As Decimal
            '        ReturnAmt = TotalCollectAmount - _customerpay
            '        Dim strshowmsg As String = ""
            '        ' Commented by Rama ranjan on 05-oct-2009
            '        'strshowmsg = strshowmsg & "Customer Paid         " & CurrencyFormat(TotalCollectAmount) & vbLf
            '        'strshowmsg = strshowmsg & "Return to Customer    " & CurrencyFormat(ReturnAmt) & "  Amount" & vbLf

            '        'Commented by Rohit on 02nd Dec
            '        'strshowmsg = getValueByKey("ACPBCS03") & " " & TotalBillAmount & vbLf
            '        'strshowmsg = strshowmsg & getValueByKey("ACPBCS04") & " " & TotalCollectAmount & vbLf
            '        'strshowmsg = strshowmsg & getValueByKey("ACPBCS05") & " " & ReturnAmt & " " & getValueByKey("ACPBCS06") & vbLf
            '        'ShowMessage(strshowmsg, getValueByKey("CLAE04"), True)
            '        '''getValueByKey("ACPBCS07")
            '        ShowMessage(String.Format(getValueByKey("ACPBCS07"), TotalBillAmount, TotalCollectAmount, ReturnAmt, clsAdmin.CurrencyCode), "ACPBCS07 - " & getValueByKey("CLAE04"))

            '        txtCalCollectAmount.Text = CDbl(txtCalCollectAmount.Text) - ReturnAmt
            '        Return True
            '    End If
            'End If

            If (txtCollectAmount.Text > BalAmount) Then
                'Code is added by irfan on 01032018 for payment settlement in CSA Screen=========
                Dim ReturnAmt As Decimal
                ReturnAmt = Format(txtCollectAmount.Text - CDbl(BalAmount), "0.00")
                Dim strshowmsg As String = ""

                ' Commented by Rama ranjan on 05-oct-2009
                'strshowmsg = strshowmsg & "Customer Paid         " & CurrencyFormat(TotalCollectAmount) & vbLf
                'strshowmsg = strshowmsg & "Return to Customer    " & CurrencyFormat(ReturnAmt) & "  Amount" & vbLf

                'Commented by Rohit on 02nd Dec
                'strshowmsg = getValueByKey("ACPBCS03") & " " & TotalBillAmount & vbLf
                'strshowmsg = strshowmsg & getValueByKey("ACPBCS04") & " " & TotalCollectAmount & vbLf
                'strshowmsg = strshowmsg & getValueByKey("ACPBCS05") & " " & ReturnAmt & " " & getValueByKey("ACPBCS06") & vbLf
                'ShowMessage(strshowmsg, getValueByKey("CLAE04"), True)

                ShowMessage(True, String.Format(getValueByKey("ACPBCS07"), BalAmount, txtCollectAmount.Text, ReturnAmt, clsAdmin.CurrencyCode), "ACPBCS07 - " & getValueByKey("CLAE04"))
                txtCollectAmount.Text = CDbl(txtCollectAmount.Text) - ReturnAmt
                '=================================================================================
                Return True
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try


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
            salesinvoice.CreatedOn = ServerDate
            salesinvoice.UpdatedAt = clsAdmin.SiteCode
            salesinvoice.UpdatedOn = ServerDate
            If clsDefaultConfiguration.AuthReqForCreditSalesAdjustmentOnCSA = True Then
                salesinvoice.UpdatedBy = CSAAuthUserName
                salesinvoice.CreatedBy = CSAAuthUserName
            Else
                salesinvoice.UpdatedBy = clsAdmin.UserCode
                salesinvoice.CreatedBy = clsAdmin.UserCode
            End If
            ReceiptList.Add(salesinvoice)

        Next
        Return ReceiptList
    End Function

    Private Sub txtCollectAmount_PreviewKeyDown(sender As System.Object, e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtCollectAmount.PreviewKeyDown
        Try
            If e.KeyCode = Keys.Enter AndAlso UCase(TenderTypeCode) = "CASH" Then
                btnOk_Click(btnOk, New System.EventArgs)
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Function Themechange()
        Me.BackColor = Color.FromArgb(134, 134, 134)
        btnOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnOk.BackColor = Color.Transparent
        btnOk.BackColor = Color.FromArgb(0, 107, 163)
        btnOk.ForeColor = Color.FromArgb(255, 255, 255)
        btnOk.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnOk.FlatStyle = FlatStyle.Flat
        btnOk.FlatAppearance.BorderSize = 0
        btnOk.TextAlign = ContentAlignment.MiddleCenter

        btnExit.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnExit.BackColor = Color.Transparent
        btnExit.BackColor = Color.FromArgb(0, 107, 163)
        btnExit.ForeColor = Color.FromArgb(255, 255, 255)
        btnExit.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnExit.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnExit.FlatStyle = FlatStyle.Flat
        btnExit.FlatAppearance.BorderSize = 0
        btnExit.TextAlign = ContentAlignment.MiddleCenter
        CtrlCreditCard1.BackColor = Color.FromArgb(134, 134, 134)
        CtrlChequeDetails1.BackColor = Color.FromArgb(134, 134, 134)

        lbltotalAmount.BackColor = Color.FromArgb(212, 212, 212)
        lbltotalAmount.AutoSize = False
        lbltotalAmount.Size = New Size(88, 8)
        lbltotalAmount.Font = New Font("Neo Sans", 9, FontStyle.Bold)


        lblCollectAmount.BackColor = Color.FromArgb(212, 212, 212)
        lblCollectAmount.AutoSize = False
        lblCollectAmount.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblCollectAmount.Size = New Size(88, 8)

        lblRemark.BackColor = Color.FromArgb(212, 212, 212)
        lblRemark.AutoSize = False
        lblRemark.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblRemark.Size = New Size(88, 8)
        'added by khusrao adil on 27-11-2017
        Me.btnCard.Dock = DockStyle.None
        Me.btnCard.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnCard.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnCard.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnCard.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnCard.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnCard.BringToFront()
        Me.btnCard.Image = Nothing
        'Me.btnCard.Size = New System.Drawing.Size(105, 30)
        Me.btnCard.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCard.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnCard.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnCard.FlatStyle = FlatStyle.Flat
        Me.btnCard.FlatAppearance.BorderSize = 1

        Me.btnCardInnoviti.Dock = DockStyle.None
        Me.btnCardInnoviti.BackColor = Color.WhiteSmoke
        Me.btnCardInnoviti.ForeColor = Color.Black
        Me.btnCardInnoviti.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnCardInnoviti.BringToFront()
        Me.btnCardInnoviti.Image = Nothing
        ' Me.btnCardInnoviti.Size = New System.Drawing.Size(105, 30)
        Me.btnCardInnoviti.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCardInnoviti.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCardInnoviti.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnCardInnoviti.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnCardInnoviti.FlatStyle = FlatStyle.Flat
        Me.btnCardInnoviti.FlatAppearance.BorderSize = 1

    End Function
End Class