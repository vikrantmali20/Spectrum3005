Imports System.Text
Imports SpectrumBL

Public Class clsPrintSalesOrderNew
    ''' <summary>
    '''  Birthlist transaction for printing 
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum PrintSOTransactionSet
        Status
        Payment
        DeliveryNote
        GiftVoucherDocumentPrint
        SOReturnStatus
    End Enum

    ' ''' <summary>
    ' ''' Customer Print for sales,retrun or create BL items 
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    Private _printPreview As Boolean
    Private _PrintTransaction As PrintSOTransactionSet
    Private strSODocumentType As String = "SO"
    Private strDocTypeforTermsnConditions As String = "SO201"
    Private _strSalesOrderNo As String
    Private _dtSalesItemDetails As DataTable
    Private _dtCustomerDetails As DataTable
    Private _iSORoundOff As Integer
    Private _dtPaymentDetails As DataTable
    'Private _PrintingTaxInfoAllowed As Boolean
    Private _CustomerReference As String
    Private _CustomerRemarks As String
    Private _strInvoiceNumber As String
    Private _strSOCurrentInvoiceNumber As String



#Region "Properties"

    Public Property PrintSOTransaction() As PrintSOTransactionSet
        Get
            Return _PrintTransaction
        End Get
        Set(ByVal value As PrintSOTransactionSet)
            _PrintTransaction = value
        End Set
    End Property

    Public Property InvoiceNumber() As String
        Get
            Return _strInvoiceNumber
        End Get
        Set(ByVal value As String)
            _strInvoiceNumber = value
        End Set
    End Property
    Public Property SalesOrderNo() As String
        Get
            Return _strSalesOrderNo
        End Get
        Set(ByVal value As String)
            _strSalesOrderNo = value
        End Set
    End Property

    Public Property CustomerDetails() As DataTable
        Get
            Return _dtCustomerDetails
        End Get
        Set(ByVal value As DataTable)
            _dtCustomerDetails = value
        End Set
    End Property

    Public Property SalesItemDetails() As DataTable
        Get
            Return _dtSalesItemDetails
        End Get
        Set(ByVal value As DataTable)
            _dtSalesItemDetails = value
        End Set
    End Property

    Public Property SORoundOff() As Integer
        Get
            Return _iSORoundOff
        End Get
        Set(ByVal value As Integer)
            _iSORoundOff = value
        End Set
    End Property

    Public Property PaymentDetails() As DataTable
        Get
            Return _dtPaymentDetails
        End Get
        Set(ByVal value As DataTable)
            _dtPaymentDetails = value
        End Set
    End Property

    'Public Property PrintingTaxInfoAllowed() As Boolean
    '    Get
    '        Return _PrintingTaxInfoAllowed
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _PrintingTaxInfoAllowed = value
    '    End Set
    'End Property

    Public Property CustomerReference() As String
        Get
            Return _CustomerReference
        End Get
        Set(ByVal value As String)
            _CustomerReference = value
        End Set
    End Property
    Public Property CustomerRemarks() As String
        Get
            Return _CustomerRemarks
        End Get
        Set(ByVal value As String)
            _CustomerRemarks = value
        End Set
    End Property

    Private Shared _strPrintSOPageSetup As String
    Public Shared Property PrintSOPageSetup() As String
        Get
            Return _strPrintSOPageSetup
        End Get
        Set(ByVal value As String)
            _strPrintSOPageSetup = value
        End Set
    End Property
    Private _cashierId As String
    Public Property CashierId() As String
        Get
            Return _cashierId
        End Get
        Set(ByVal value As String)
            _cashierId = value
        End Set
    End Property

    Private _DefaultCurrency As String

    Public Property DefaultCurrency() As String
        Get
            Return _DefaultCurrency
        End Get
        Set(ByVal value As String)
            _DefaultCurrency = value
        End Set
    End Property
    ''' <summary>
    ''' Creation Date for printed document.
    ''' </summary>
    ''' <remarks></remarks>
    Private _dtCreationDate As Date
    Private Property CreationDate() As Date
        Get
            Return _dtCreationDate
        End Get
        Set(ByVal value As Date)
            _dtCreationDate = value
        End Set
    End Property

    Private _RoundOffRequired As Boolean
    Private _PrintPreviewAllowed As Boolean

    'Private _isBlBillRoundOffRequired As Boolean
    'Public Property IsBillRoundOffRequired() As Boolean
    '    Get
    '        Return _isBlBillRoundOffRequired
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _isBlBillRoundOffRequired = value
    '    End Set
    'End Property

    ''' <summary>
    ''' Other cost added into Sales Order.
    ''' </summary>
    ''' <remarks></remarks>
    Private _dsOtherCost As DataSet
    Public Property OtherCost() As DataSet
        Get
            Return _dsOtherCost
        End Get
        Set(ByVal value As DataSet)
            _dsOtherCost = value
        End Set
    End Property

    ''' <summary>
    '''  Tax information against current invoice 
    ''' </summary>
    ''' <remarks></remarks>
    Private _dtSalesOrderTaxDtls As DataTable
    Public Property DataTableSalesOrderTaxDtls() As DataTable
        Get
            Return _dtSalesOrderTaxDtls
        End Get
        Set(ByVal value As DataTable)
            _dtSalesOrderTaxDtls = value
        End Set
    End Property

#End Region

    ''' <summary>
    ''' Printing setting 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 

    'Public Function PrintConfig() As Boolean
    '    Try
    '        Dim dt As DataTable
    '        dt = modCommanFunction.GetDefaultSetting(SiteCode, "SalesOrder")
    '        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
    '            For Each dr As DataRow In dt.Rows
    '                'If dr("FLDLABEL").ToString() = "PrintingTaxInfoAllowed" Then
    '                '    PrintingTaxInfoAllowed = dr("FLDVALUE")
    '                'ElseIf dr("FLDLABEL").ToString().ToUpper = "SOIsPromotionMsgPrint".ToUpper() Then
    '                '    IsPromotionMessagePrint = dr("FLDVALUE").ToString
    '                'ElseIf dr("FLDLABEL").ToString().ToUpper = "SOIsTaxInformationMsgPrint".ToUpper() Then
    '                '    IsTaxInformation = dr("FLDVALUE").ToString
    '                'ElseIf dr("FLDLABEL").ToString().ToUpper = "SORoundOffRequired".ToUpper() Then
    '                '    IsBillRoundOffRequired = dr("FLDVALUE")
    '                If dr("FLDLABEL").ToString().ToUpper = "PrintPreviewAllowed".ToUpper() Then
    '                    _printPreview = dr("FLDVALUE")
    '                End If
    '            Next
    '        End If

    '        'dt = modCommanFunction.GetDefaultSetting(Sitecode, "0000")
    '        'If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
    '        '    For Each dr As DataRow In dt.Rows
    '        '        If dr("FLDLABEL").ToString().ToUpper = "ROUNDED_OFF_TO".ToUpper() Then
    '        '            DecimalDigits = dr("FLDVALUE")
    '        '            'ElseIf dr("FLDLABEL").ToString().ToUpper = "IsWelcomeMsgPrint".ToUpper() Then
    '        '            '    IsWelComeMessagePrint = dr("FLDVALUE").ToString

    '        '        End If
    '        '    Next
    '        'End If
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    Private _isGiftVoucher As Boolean
    Private Property IsGiftVoucherDocument() As Boolean
        Get
            Return _isGiftVoucher
        End Get
        Set(ByVal value As Boolean)
            _isGiftVoucher = value
        End Set
    End Property

    Private _GiftReceiptMessage As String
    Private Property GiftReceiptMessage() As String
        Get
            Return _GiftReceiptMessage
        End Get
        Set(ByVal value As String)
            _GiftReceiptMessage = value
        End Set
    End Property
    ''' <summary>
    '''  Sales order Status message.
    ''' </summary>
    ''' <remarks></remarks>
    Private _SOStatus As String = ""
    Private Property SOStatus() As String
        Get
            Return _SOStatus
        End Get
        Set(ByVal value As String)
            _SOStatus = value
        End Set
    End Property

    Private _SOItemReturnReason As String
    Private Property ItemReturnReason() As String
        Get
            Return _SOItemReturnReason
        End Get
        Set(ByVal value As String)
            _SOItemReturnReason = value
        End Set
    End Property
    Public Sub New(ByVal IsPrintPreview As Boolean, ByVal RoundOffRequired As Boolean, ByVal enumTransactionDetails As PrintSOTransactionSet, ByVal strSiteCode As String, ByVal strDefaultCurrency As String, ByVal strCashierID As String, ByVal strSalesOrderNo As String, ByVal dtCustomerDetail As DataTable, ByVal dtSalesItem As DataTable, ByVal dtPayment As DataTable, Optional ByVal strCustomerReference As String = "", Optional ByVal strRemarks As String = "", Optional ByVal strInvoiceNumber As String = "", Optional ByVal strGiftReceiptMessage As String = "", Optional ByVal iSoRoundOff As Integer = 2, Optional ByRef strErrorMsg As String = "", Optional ByVal soPrinterInfo As DataTable = Nothing, Optional ByVal strStatus As String = "", Optional ByVal dsOtherCost As DataSet = Nothing, Optional ByVal strReturnReason As String = "", Optional ByVal dtSalesOrderTaxdtls As DataTable = Nothing)

        Try
            ItemReturnReason = strReturnReason
            PrintSOTransaction = enumTransactionDetails
            SalesOrderNo = strSalesOrderNo
            CustomerDetails = dtCustomerDetail
            PaymentDetails = dtPayment
            CashierId = strCashierID
            SalesItemDetails = dtSalesItem
            SiteCode = strSiteCode
            _dsOtherCost = dsOtherCost
            'IsFooterPrinting = True
            'IsHeaderPrinting = True
            DefaultCurrency = strDefaultCurrency
            CustomerReference = strCustomerReference
            CustomerRemarks = strRemarks
            InvoiceNumber = strInvoiceNumber
            GiftReceiptMessage = strGiftReceiptMessage
            CreationDate = GetCurrentDate()
            SORoundOff = iSoRoundOff
            dtPrinterInfo1 = soPrinterInfo
            _SOStatus = strStatus
            DataTableSalesOrderTaxDtls = dtSalesOrderTaxdtls
            _RoundOffRequired = RoundOffRequired
            _printPreview = IsPrintPreview

            Dim str As String = String.Empty
            If PrintSOTransaction = PrintSOTransactionSet.Payment Then
                str = "SOInvc"
            End If
            If PrintSOTransaction = PrintSOTransactionSet.Status Then
                str = "SOStatus"
            End If
            If PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                str = "SOOB"
            End If

            If PrintSOTransaction = PrintSOTransactionSet.SOReturnStatus Then
                str = "SOStatus"
            End If

            If PrintSOTransaction = PrintSOTransactionSet.GiftVoucherDocumentPrint Then
                str = "Voucher"
            End If
            PrintSOPageSetup = (GetPrintFormat(dtPrinterInfo1.Rows(0)(0), str)).Rows(0)(0)

            'If PrintConfig() Then
            If Not PrintSOPageSetup = "L40" Then
                A4SOStatusPrint(_printPreview)
            Else
                L40SOStatusPrint(_printPreview)
            End If
            'End If

        Catch ex As Exception

        End Try
    End Sub

#Region "A4 size print"
    Public Function A4GetTitle() As String
        Try

            If PrintSOTransaction = PrintSOTransactionSet.Status Then
                Dim sbTitle As New StringBuilder
                'sbTitle.Append(strLineA4 + vbCrLf)
                Dim strTitleInfo As String
                'strTitleInfo = String.Format("SALES ORDER  {0} STATUS:{1}", SalesOrderNo, SOStatus)
                strTitleInfo = String.Format(getValueByKey("CLSPSO001"), SalesOrderNo, SOStatus)
                strTitleInfo.PadRight(22)
                'strTitleInfo = String.Format("{0} DATE {1}", strTitleInfo, CreationDate)
                strTitleInfo = String.Format(getValueByKey("CLSPSO002"), strTitleInfo, CreationDate)
                sbTitle.Append(strTitleInfo + vbCrLf)
                'sbTitle.Append(strLineA4 + vbCrLf)
                Return sbTitle.ToString()
            ElseIf PrintSOTransaction = PrintSOTransactionSet.SOReturnStatus Then
                Dim sbTitle As New StringBuilder
                'sbTitle.Append(strLineA4 + vbCrLf)
                Dim strTitleInfo As String
                'strTitleInfo = String.Format("SALES ORDER  {0} STATUS:{1} (Return)", SalesOrderNo, SOStatus)
                strTitleInfo = String.Format(getValueByKey("CLSPSO003"), SalesOrderNo, SOStatus)
                strTitleInfo.PadRight(22)
                'strTitleInfo = String.Format("{0} DATE {1}", strTitleInfo, CreationDate)
                strTitleInfo = String.Format(getValueByKey("CLSPSO002"), strTitleInfo, CreationDate)
                sbTitle.Append(strTitleInfo + vbCrLf)
                'sbTitle.Append(strLineA4 + vbCrLf)
                Return sbTitle.ToString()

            ElseIf PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                Dim sbTitle As New StringBuilder
                'sbTitle.Append(strLineA4 + vbCrLf)
                Dim strTitleInfo As String
                'strTitleInfo = String.Format("SALES ORDER  {0} DELIVERY NOTE ", SalesOrderNo)
                strTitleInfo = String.Format(getValueByKey("CLSPSO004"), SalesOrderNo)
                strTitleInfo.PadRight(22)
                'strTitleInfo = String.Format("{0} DATE {1}", strTitleInfo, CreationDate)
                strTitleInfo = String.Format(getValueByKey("CLSPSO002"), strTitleInfo, CreationDate)
                sbTitle.Append(strTitleInfo + vbCrLf)
                'sbTitle.Append(strLineA4 + vbCrLf)
                Return sbTitle.ToString()
            ElseIf PrintSOTransaction = PrintSOTransactionSet.Payment Then
                Dim sbTitle As New StringBuilder
                'sbTitle.Append(strLineA4 + vbCrLf)
                Dim strTitleInfo As String
                'strTitleInfo = String.Format("SALES ORDER  {0} PAYMENT ", SalesOrderNo)
                strTitleInfo = String.Format(getValueByKey("CLSPSO005") & " ", SalesOrderNo)
                strTitleInfo.PadRight(22)
                'strTitleInfo = String.Format("{0} DATE {1}", strTitleInfo, CreationDate)
                strTitleInfo = String.Format(getValueByKey("CLSPSO002"), strTitleInfo, CreationDate)
                'Dim strPaymentHdr As String = String.Format("             CASH MEMO Nr {0}", InvoiceNumber)

                If (Not String.IsNullOrEmpty(InvoiceNumber)) Then
                    Dim strPaymentHdr As String = String.Format("             " & getValueByKey("CLSPSO006"), InvoiceNumber)
                    _strSOCurrentInvoiceNumber = InvoiceNumber
                    sbTitle.Append(strPaymentHdr + vbCrLf)
                End If
                sbTitle.Append(strTitleInfo + vbCrLf)
                'sbTitle.Append(strLineA4 + vbCrLf)
                Return sbTitle.ToString()
            ElseIf PrintSOTransaction = PrintSOTransactionSet.GiftVoucherDocumentPrint Then
                Dim sbTitle As New StringBuilder
                'sbTitle.Append(strLineA4 + vbCrLf)
                Dim strTitleInfo As String
                'strTitleInfo = String.Format("SALES ORDER  {0} Gift Voucher ", SalesOrderNo)
                strTitleInfo = String.Format(getValueByKey("CLSPSO007") & " ", SalesOrderNo)
                strTitleInfo.PadRight(22)
                'strTitleInfo = String.Format("{0} DATE {1}", strTitleInfo, CreationDate)
                strTitleInfo = String.Format(getValueByKey("CLSPSO002"), strTitleInfo, CreationDate)
                'Dim strPaymentHdr As String = String.Format("             CASH MEMO Nr {0}", InvoiceNumber)
                Dim strPaymentHdr As String = String.Format("             " & getValueByKey("CLSPSO006"), InvoiceNumber)
                sbTitle.Append(strPaymentHdr + vbCrLf)
                sbTitle.Append(strTitleInfo + vbCrLf)
                'sbTitle.Append(strLineA4 + vbCrLf)
                Return sbTitle.ToString()

            End If
        Catch ex As Exception

        End Try
    End Function

    Private Function A4SOStatusPrint(ByVal PrintPreview As Boolean) As Boolean
        Try
            CheckDocumentType()
            'Comman Print

            Dim strHeader As New StringBuilder
            Dim strSubHeader As New StringBuilder
            Dim strFooter As New StringBuilder
            Dim strWelcomeMsg As New StringBuilder
            Dim strTaxInformation As New StringBuilder
            Dim strPromotionMsg As New StringBuilder

            PrinttHeaderAndFooter(strSubHeader, strFooter, strWelcomeMsg, strPromotionMsg, strTaxInformation, strSODocumentType, PrintSOPageSetup)

            'Added :Rakesh-17/07/2012: Site Information
            Dim siteInfo As String = L40OrA4GetSiteDetails(SiteCode, "A4").ToString()
            strHeader.Append(siteInfo)

            If (Not String.IsNullOrEmpty(strSubHeader.ToString())) Then
                strHeader.Append(strLineA4 + vbCrLf)
                strHeader.Append(SplitString(strSubHeader.ToString(), 80).ToString().Trim() + vbCrLf)
                'strHeader.Append(strLineA4 + vbCrLf)
            End If

            'Title 
            Dim sbTitle As New StringBuilder
            sbTitle.Append(A4GetTitle)

            Dim strCashierDetails As String = A4CashierDetails(CreationDate, InvoiceNumber, CashierId)

            'CustomerDetails
            Dim strCustomerDetails As String
            strCustomerDetails = A4GetCustomerDetails(_dtCustomerDetails)
            'Customer Delivery Details
            Dim strCustomerDeliveryDetails As String
            strCustomerDeliveryDetails = A4GetCustomerDeliveryDetails(_dtCustomerDetails)

            Dim sbInvoiceInfo As New StringBuilder
            If PrintSOTransaction = PrintSOTransactionSet.Status Then
                'sbInvoiceInfo.Append("Invoice to " + vbCrLf)
                sbInvoiceInfo.Append(getValueByKey("CLSPSO008") & "         : " + InvoiceNumber + vbCrLf)
                'Dim strInvoiceNumber As String = String.Format("Customer reference : {0}", CustomerReference)
                Dim strInvoiceNumber As String = String.Format(getValueByKey("CLSPSO009"), CustomerReference)
                sbInvoiceInfo.Append(strInvoiceNumber + vbCrLf)
            End If

            'Remark 
            Dim sbRemark As New StringBuilder
            Dim strRemark As String = getValueByKey("CLSPSO010") + "            : " + CustomerRemarks
            sbRemark.Append(sbInvoiceInfo.ToString())

            If PrintSOTransaction = PrintSOTransactionSet.SOReturnStatus Then
                If (Not String.IsNullOrEmpty(CustomerRemarks)) Then
                    sbRemark.Append(strRemark + ", " + SplitString(ItemReturnReason.ToString(), 80, 21).ToString())
                Else
                    sbRemark.Append(strRemark + SplitString(ItemReturnReason.ToString(), 80, 21).ToString())
                End If
            Else
                sbRemark.Append(strRemark)
            End If

            'Sales Item 
            Dim strItemHeader As String = ""
            Dim strItemSales As String = ""

            Dim iItemDetailHeight As Single
            If Not (GetItemInfo(strItemHeader, strItemSales, iItemDetailHeight)) Then
                Return False
            End If

            Dim strPaymentInfo As String = ""
            Dim strTermsNConditions As String = ""

            A4GetPaymentInfo(strPaymentInfo, 62)

            Dim sbFinalPrint As New StringBuilder

            Dim strTaxDetailsAgainstInvoice As String = ""
            If PrintSOTransaction = PrintSOTransactionSet.Payment Then

                A4GetTaxDetails(strTaxDetailsAgainstInvoice)
            End If

            Dim objclsA4Print As New clsA4PrintNew
            objclsA4Print.A4Header = String.Empty 'strHeader.ToString()
            objclsA4Print.A4SiteHeader = strHeader.ToString().Trim()
            objclsA4Print.A4Title = sbTitle.ToString().Trim()
            objclsA4Print.A4CashierDetails = strCashierDetails.Trim()
            objclsA4Print.A4CustomerDetails = strCustomerDetails.Trim()
            objclsA4Print.A4DeliveryAddress = strCustomerDeliveryDetails.Trim()
            objclsA4Print.A4Remark = sbRemark.ToString().Trim()
            objclsA4Print.A4ItemHeader = strItemHeader.Trim()
            objclsA4Print.A4ItemDetails = strItemSales.Trim()
            objclsA4Print.A4PaymentDetails = strPaymentInfo.Trim()
            objclsA4Print.A4WelcomeMessage = strWelcomeMsg.ToString().Trim()
            objclsA4Print.A4TaxInformation = strTaxInformation.ToString().Trim()
            If PrintSOTransaction = PrintSOTransactionSet.Payment Then
                objclsA4Print.A4TaxDetailsInvoice = strTaxDetailsAgainstInvoice.Trim()
            End If

            objclsA4Print.A4PromotionInformation = strPromotionMsg.ToString().Trim()
            If Not PrintSOTransaction = PrintSOTransactionSet.Payment Then
                objclsA4Print.A4Footer = strFooter.ToString().Trim()
            End If

            If PrintSOTransaction = PrintSOTransactionSet.Status Or Not PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                strTermsNConditions = A4GetStringTermsCondition(strDocTypeforTermsnConditions, SiteCode, PrintSOPageSetup)
                objclsA4Print.A4TermsNConditions = strTermsNConditions.Trim()
            End If

            If Not GiftReceiptMessage Is Nothing Then
                objclsA4Print.GiftReceiptMessage = GiftReceiptMessage.Trim()
            End If

            PrinterName = SetPrinterName(dtPrinterInfo1, "SalesOrder", strSODocumentType)

            If PrintPreview = True Then
                objclsA4Print.fnPrint("PRV")
            Else
                objclsA4Print.fnPrint("PRN")
            End If

            Return True
        Catch ex As Exception

        End Try

    End Function

    Private Sub CheckDocumentType()
        If PrintSOTransaction = PrintSOTransactionSet.Payment Then
            strSODocumentType = "SOInvc"
        ElseIf PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
            strSODocumentType = "SOOB"
        ElseIf PrintSOTransaction = PrintSOTransactionSet.Status Then
            strSODocumentType = "SOStatus"
        ElseIf PrintSOTransaction = PrintSOTransactionSet.SOReturnStatus Then
            'strSODocumentType = "SOReturnStatus"
            'Commented the above line since there is no status called SOReturnStatus in the MStPrinterDoc table
            'Commeted by Ashish
            strSODocumentType = "SOStatus"
            'Added the above line
            'By Ashish
        End If
    End Sub

    Public Function A4GetPaymentInfo(ByRef strPaymentInfo As String, ByVal iGap As Integer) As Boolean
        Try
            Dim sbPaymentInfo As New StringBuilder
            Dim iLeftPadLine As Integer = 22
            Dim lnDotpayment As String = "---------------"
            lnDotpayment = lnDotpayment.PadLeft(iGap + iLeftPadLine)
            lnDotpayment = lnDotpayment.PadRight(iGap + iLeftPadLine)


            If PrintSOTransaction = PrintSOTransactionSet.Status Or PrintSOTransaction = PrintSOTransactionSet.Payment Then

                'Dim strTotal2Pay As String = "TOTAL TO PAY "
                Dim strTotal2Pay As String = getValueByKey("CLSPSO011") & " "
                Dim objTotal2Pay As Object = _dtSalesItemDetails.Compute("sum(NetAmount)", "IsStatus <> 'Deleted'")
                Dim decTotal2Pay As Decimal
                If Not objTotal2Pay Is DBNull.Value AndAlso Not objTotal2Pay Is Nothing Then
                    decTotal2Pay = CDbl(objTotal2Pay)
                End If
                'Other Charges adding into NetAmount 
                If Not _dsOtherCost Is Nothing AndAlso _dsOtherCost.Tables.Contains("NewOtherCharges") AndAlso _dsOtherCost.Tables("NewOtherCharges").Rows.Count > 0 Then
                    decTotal2Pay = decTotal2Pay + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(ChargeAmount)", " ") + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " ")
                End If
                If Not _dtSalesItemDetails.Columns.Contains("TotalPickupAmt") Then
                    _dtSalesItemDetails.Columns.Add("TotalPickupAmt", System.Type.GetType("System.Decimal"))
                End If
                _dtSalesItemDetails.Columns("TotalPickupAmt").Expression = "(NetAmount/quantity)* (PickUpQty + DeliveredQty)"
                Dim objTotalPickAmt As Object = _dtSalesItemDetails.Compute("sum(TotalPickupAmt)", "PickUpQty > 0 or  DeliveredQty>0 and IsStatus <> 'Deleted'")

                Dim decTotalPickAmt As Decimal
                If Not objTotalPickAmt Is DBNull.Value AndAlso Not objTotalPickAmt Is Nothing Then
                    decTotalPickAmt = CDbl(objTotalPickAmt)
                End If

                _dtSalesItemDetails.Columns("TotalPickupAmt").Expression = Nothing
                _dtSalesItemDetails.Columns("TotalPickupAmt").ReadOnly = False
                decTotal2Pay = MyRound(decTotal2Pay, SORoundOff, _RoundOffRequired)
                strTotal2Pay = strTotal2Pay.PadRight(iGap)
                Dim strvalueTotal2Pay As String = PrintFormatCurrency(decTotal2Pay, DefaultCurrency, SORoundOff)
                PadingString(strvalueTotal2Pay)
                'CStr(decTotal2Pay) + DefaultCurrency.PadLeft(2)
                sbPaymentInfo.Append(strTotal2Pay)
                sbPaymentInfo.Append(strvalueTotal2Pay + vbCrLf)
                'sbPaymentInfo.Append(strLineA4 + vbCrLf)

                'Dim strTotalGross As String = "GROSS TOTAL"
                Dim strTotalGross As String = getValueByKey("CLSPSO012")
                Dim objTotalGross As Object = _dtSalesItemDetails.Compute("sum(GrossAmt)", "IsStatus <> 'Deleted'")
                Dim decTotalGross As Decimal

                If Not objTotalGross Is Nothing AndAlso Not objTotalGross Is DBNull.Value Then
                    decTotalGross = CDbl(objTotalGross)
                End If
                If Not _dsOtherCost Is Nothing AndAlso _dsOtherCost.Tables.Contains("NewOtherCharges") AndAlso _dsOtherCost.Tables("NewOtherCharges").Rows.Count > 0 Then
                    decTotalGross = decTotalGross + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(ChargeAmount)", " ") + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " ")
                End If
                decTotalGross = MyRound(decTotalGross, SORoundOff, _RoundOffRequired)
                strTotalGross = strTotalGross.PadRight(iGap)
                Dim strValueTotalGross As String = PrintFormatCurrency(decTotalGross, DefaultCurrency, SORoundOff)
                PadingString(strValueTotalGross)
                strTotalGross = strTotalGross & strValueTotalGross ' CStr(decTotalDiscount) + DefaultCurrency.PadLeft(2)
                sbPaymentInfo.Append(strTotalGross + vbCrLf)

                'Dim strDiscount As String = "DISCOUNT"
                Dim strDiscount As String = getValueByKey("CLSPSO013")
                Dim decDiscount As Decimal
                Dim objDiscount As Object = _dtSalesItemDetails.Compute("sum(Discount)", "IsStatus <> 'Deleted'")
                If Not objDiscount Is Nothing AndAlso Not objDiscount Is DBNull.Value Then
                    decDiscount = CDbl(objDiscount)
                End If
                decDiscount = MyRound(decDiscount, SORoundOff, _RoundOffRequired)
                strDiscount = strDiscount.PadRight(iGap)
                Dim strvalueTotalDiscount As String = PrintFormatCurrency(decDiscount, DefaultCurrency, SORoundOff)
                strvalueTotalDiscount = "-" + strvalueTotalDiscount
                PadingString(strvalueTotalDiscount)
                ' CStr(decDiscount) + DefaultCurrency.PadLeft(2) + vbCrLf
                sbPaymentInfo.Append(strDiscount)
                'sbPaymentInfo.Append("-")
                sbPaymentInfo.Append(strvalueTotalDiscount)
                sbPaymentInfo.Append(vbCrLf + lnDotpayment + vbCrLf)

                'Dim strTotalNetAmount As String = "NET TOTAL"
                Dim strTotalNetAmount As String = getValueByKey("CLSPSO014")


                Dim decTotalNetAmount As Decimal
                Dim objTotalNetAmount As Object = _dtSalesItemDetails.Compute("sum(NetAmount)", "IsStatus <> 'Deleted'")
                If Not objTotalNetAmount Is Nothing AndAlso Not objTotalNetAmount Is DBNull.Value Then
                    decTotalNetAmount = CDbl(objTotalNetAmount)
                End If

                If Not _dsOtherCost Is Nothing AndAlso _dsOtherCost.Tables.Contains("NewOtherCharges") AndAlso _dsOtherCost.Tables("NewOtherCharges").Rows.Count > 0 Then
                    decTotalNetAmount = decTotalNetAmount + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(ChargeAmount)", " ") + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " ")
                End If
                decTotalNetAmount = MyRound(decTotalNetAmount, SORoundOff, _RoundOffRequired)
                strTotalNetAmount = strTotalNetAmount.PadRight(iGap)
                Dim strValueTotalNetAmt As String = PrintFormatCurrency(decTotalNetAmount, DefaultCurrency, SORoundOff)
                PadingString(strValueTotalNetAmt)

                sbPaymentInfo.Append(strTotalNetAmount)
                sbPaymentInfo.Append(strValueTotalNetAmt)
                sbPaymentInfo.Append(vbCrLf + lnDotpayment + vbCrLf)

                'Dim strAdvancedPayed As String = "ADVANCE PAYED"
                Dim strAdvancedPayed As String = getValueByKey("CLSPSO015")
                Dim decAdvancedPayed As Decimal = Decimal.Zero

                Dim dtAdvancePayment As DataTable = GetInvoiceAdvancePayment(SiteCode, SalesOrderNo)
                If Not dtAdvancePayment Is Nothing AndAlso dtAdvancePayment.Rows.Count > 0 Then
                    strAdvancedPayed = GetAdvancePayment(dtAdvancePayment, decAdvancedPayed)
                End If
                Dim strAdvancedPayedDate As String = ""
                sbPaymentInfo.Append(strAdvancedPayed + vbCrLf)
                If Not PrintSOTransaction = PrintSOTransactionSet.Payment Then
                    'Dim strBalance2Pay As String = "BALANCE TO PAY"
                    Dim strBalance2Pay As String = getValueByKey("CLSPSO016")
                    Dim decBalance2Pay As Decimal = Decimal.Subtract(decTotalNetAmount, decAdvancedPayed)
                    sbPaymentInfo.Append(lnDotpayment + vbCrLf)
                    strBalance2Pay = strBalance2Pay.PadRight(iGap)
                    Dim strValuebalance2Pay As String = PrintFormatCurrency(MyRound(decBalance2Pay, SORoundOff, _RoundOffRequired), DefaultCurrency, SORoundOff)

                    PadingString(strValuebalance2Pay)
                    sbPaymentInfo.Append(strBalance2Pay)
                    sbPaymentInfo.Append(strValuebalance2Pay + vbCrLf)

                ElseIf PrintSOTransaction = PrintSOTransactionSet.Payment Then
                    'Dim strPaidOnThisReceipt As String = "PAYED ON THIS RECEIPT"
                    Dim strPaidOnThisReceipt As String = getValueByKey("CLSPSO017")

                    strPaidOnThisReceipt = strPaidOnThisReceipt.PadRight(iGap + 9) + lnDotpayment
                    sbPaymentInfo.Append(vbCrLf + strPaidOnThisReceipt + vbCrLf)
                    Dim sbPaymentSummary As New StringBuilder

                    Dim decCurrentPayed As Decimal
                    If Not PaymentDetails Is Nothing AndAlso PaymentDetails.Rows.Count > 0 Then
                        decCurrentPayed = PaymentDetails.Compute("sum(Amount)", "")
                        Dim strReceiptCode As String = ""
                        Dim strAmount As String = ""
                        Dim strCurrencyCode As String = ""
                        Dim strFinalAMount As String = ""
                        Dim FinalString As String = ""
                        Dim strNumber As String = ""
                        For Each drPayd As DataRow In PaymentDetails.Rows

                            ValidateDataRow(drPayd("RecieptType"), strReceiptCode)

                            If Not drPayd("CurrencyCode") Is DBNull.Value Then
                                If drPayd("CurrencyCode") = DefaultCurrency Then

                                    ValidateDataRow(drPayd("Amount"), strAmount)
                                ElseIf drPayd("CurrencyCode") = String.Empty Then
                                    ValidateDataRow(drPayd("Amount"), strAmount)
                                Else
                                    ValidateDataRow(drPayd("AmountInCurrency"), strAmount)
                                End If
                            Else
                                ValidateDataRow(drPayd("Amount"), strAmount)
                            End If
                            If strReceiptCode.ToUpper().Trim() = CreditVoucher_R Or strReceiptCode.ToUpper().Trim() = CreditVoucher_I Or strReceiptCode.ToUpper().Trim() = GIFTVOUCHE_I Or strReceiptCode.ToUpper().Trim() = GIFTVOUCHE_R Then
                                ValidateDataRow(drPayd("Number"), strNumber)
                                strReceiptCode = strReceiptCode + "(" + strNumber + ")"

                            End If
                            ValidateDataRow(drPayd("CurrencyCode"), strCurrencyCode)
                            strReceiptCode = strReceiptCode.PadRight(iGap)
                            strReceiptCode = strReceiptCode.PadLeft(iGap)
                            strFinalAMount = PrintFormatCurrency(strAmount, strCurrencyCode, SORoundOff)
                            PadingString(strFinalAMount)
                            FinalString = strReceiptCode + ":" + strFinalAMount
                            sbPaymentSummary.Append(strReceiptCode)
                            sbPaymentSummary.Append(strFinalAMount + vbCrLf)

                            FinalString = ""
                        Next
                        sbPaymentSummary.Append(lnDotpayment)
                        'Dim strBalance2Pay As String = "BALANCE TO PAY"
                        Dim strBalance2Pay As String = getValueByKey("CLSPSO016")
                        '                        Dim decBalance2Pay As Decimal = Decimal.Subtract(decTotalNetAmount, Decimal.Add(decAdvancedPayed, decCurrentPayed))
                        Dim decBalance2Pay As Decimal = Decimal.Subtract(decTotalNetAmount, decAdvancedPayed)

                        strBalance2Pay = strBalance2Pay.PadRight(iGap)
                        Dim strvalueBalance2pay As String = PrintFormatCurrency(MyRound(decBalance2Pay, SORoundOff, _RoundOffRequired), DefaultCurrency, SORoundOff) 'MyRound(decBalance2Pay, SORoundOff).ToString() + DefaultCurrency.PadLeft(2)
                        PadingString(strvalueBalance2pay)
                        sbPaymentSummary.Append(vbCrLf + strBalance2Pay)
                        sbPaymentSummary.Append(strvalueBalance2pay)
                        'sbPaymentSummary.Append(strvalueBalance2pay + vbCrLf)
                        sbPaymentInfo.Append(sbPaymentSummary.ToString())
                    End If
                End If
                sbPaymentInfo.Append(vbCrLf)
                If Not PrintSOTransaction = PrintSOTransactionSet.Payment Then
                    Dim strOpenAmount As String
                    'strOpenAmount = "OPEN AMOUNT (PAID - PICKED UP)"
                    strOpenAmount = getValueByKey("CLSPSO018")
                    strOpenAmount = strOpenAmount.PadRight(iGap)
                    Dim dChargeAmount, dTaxAmt, dOtherCharges, decTotalOpenAmount As Decimal
                    dOtherCharges = 0
                    Try
                        dChargeAmount = IIf(Not _dsOtherCost.Tables("NewOtherCharges").Compute("sum(ChargeAmount)", " ") Is DBNull.Value, _dsOtherCost.Tables("NewOtherCharges").Compute("sum(ChargeAmount)", " "), 0)
                        dTaxAmt = IIf(Not _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " ") Is DBNull.Value, _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " "), 0)
                        dOtherCharges = dChargeAmount + dTaxAmt
                    Catch ex As Exception

                    End Try

                    If dOtherCharges > 0 Then
                        decTotalOpenAmount = Decimal.Subtract(decAdvancedPayed, (decTotalPickAmt + dOtherCharges))
                        If decTotalOpenAmount < 0 Then
                            decTotalOpenAmount = 0
                        End If
                    Else
                        decTotalOpenAmount = Decimal.Subtract(decAdvancedPayed, decTotalPickAmt)
                    End If


                    Dim strvalue As String = PrintFormatCurrency(MyRound(decTotalOpenAmount, SORoundOff, _RoundOffRequired), DefaultCurrency, SORoundOff)
                    PadingString(strvalue)

                    strOpenAmount = strOpenAmount  ' Decimal.Zero.ToString + DefaultCurrency.PadLeft(2)
                    sbPaymentInfo.Append(strOpenAmount)
                    sbPaymentInfo.Append(strvalue)
                End If
                'sbPaymentInfo.Append(vbCrLf)
                'sbPaymentInfo.Append(strLineA4 + vbCrLf)
                strPaymentInfo = sbPaymentInfo.ToString()
            ElseIf PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                Dim decAdvancedPayed As Decimal = Decimal.Zero
                If Not SalesItemDetails Is Nothing AndAlso SalesItemDetails.Rows.Count > 0 Then
                    Dim clNetPickupAMount As New DataColumn("NetAmountPickUp", System.Type.GetType("System.Decimal"))
                    clNetPickupAMount.Expression = "sellingPrice * PickUpQty"
                    If Not SalesItemDetails.Columns.Contains("NetAmountPickUp") Then
                        SalesItemDetails.Columns.Add(clNetPickupAMount)
                    End If
                    decAdvancedPayed = SalesItemDetails.Compute("sum(NetAmountPickUp)", "IsStatus <> 'Deleted'")
                End If
                'NEED TO DISCUSS THERE IS NO AMOUNT FOR PICKUP QTY 
                'sbPaymentInfo.Append("TOTAL OF PICKED UP ITEMS ".PadRight(iGap))
                Dim pickedArticleLabel = "", pickedArticleValue As String = ""

                pickedArticleLabel = getValueByKey("CLSPSO019")
                pickedArticleValue = PrintFormatCurrency(decAdvancedPayed, DefaultCurrency, SORoundOff)

                strPaymentInfo = pickedArticleLabel.PadRight(106 - pickedArticleValue.Length) + pickedArticleValue
                pickedArticleLabel = ""
            End If

        Catch ex As Exception

        End Try
    End Function

    Public Function A4GetTaxDetails(ByRef strTaxDetailsForInvoice As String) As Boolean
        Try
            If Not _dtSalesOrderTaxDtls Is Nothing AndAlso _dtSalesOrderTaxDtls.Rows.Count > 0 Then

                Dim strTaxName As String = ""
                Dim decTaxvalue As Decimal = Decimal.Zero
                Dim strTaxValue As String = ""

                Dim KeySortPairs = From d In _dtSalesOrderTaxDtls _
               Group By Key = d("TaxLabel").ToString(), SortOrder = d("TaxLabel").ToString() _
               Into Group _
              Select Key, TaxAmount = Group.Sum(Function(d) CType(d("TaxValue"), Nullable(Of Decimal)))
                For Each row In KeySortPairs
                    If (GetTaxName(row.Key.ToString(), strTaxName)) Then
                        strTaxValue = PrintFormatCurrency(row.TaxAmount, DefaultCurrency, DecimalDigits)
                        strTaxDetailsForInvoice = strTaxDetailsForInvoice + vbCrLf + strTaxName.PadRight(74) + strTaxValue
                    Else
                        strTaxValue = PrintFormatCurrency(0.0, DefaultCurrency, DecimalDigits)
                        strTaxDetailsForInvoice = strTaxDetailsForInvoice + vbCrLf + "Not FOund" + strTaxValue
                    End If


                Next


            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function PadingString(ByRef objStrin As Object) As Boolean
        Try
            objStrin = CStr(objStrin).PadLeft(22)
            objStrin = CStr(objStrin).PadRight(19)

        Catch ex As Exception

        End Try
    End Function


    Protected w_ArticleCode As Single = 26
    Protected Property WArticleCode() As Single
        Get
            Return w_ArticleCode
        End Get
        Set(ByVal value As Single)
            w_ArticleCode = value
        End Set
    End Property

    Protected w_ArticleDescription As Single
    Protected Property WArticleDescription() As Single
        Get
            Return w_ArticleDescription
        End Get
        Set(ByVal value As Single)
            w_ArticleDescription = value
        End Set
    End Property


    Protected w_Qty As Single
    Protected Property WQty() As Single
        Get
            Return w_Qty
        End Get
        Set(ByVal value As Single)
            w_Qty = value
        End Set
    End Property


    Private _WLTaxAmount As Single
    Public Property WLTaxAmount() As Single
        Get
            Return 9
        End Get
        Set(ByVal value As Single)
            _WLTaxAmount = value
        End Set
    End Property


    Public Function GetItemInfo(ByRef strItemHeader As String, ByRef strItemSales As String, ByRef iItemDetailHeight As Single) As Boolean
        Try
            Dim sbHdrItem As New StringBuilder()
            Dim strHdrItem As String = ""
            Dim strHdrItemQty As String = ""
            Dim strHdrItemDisc As String = ""
            Dim strHdrItemNet As String = ""

            If PrintSOTransaction = PrintSOTransactionSet.GiftVoucherDocumentPrint Then
                'sbHdrItem.Append(strLineA4 + vbCrLf)
                'strHdrItem = "Item Code"
                strHdrItem = getValueByKey("CLSPSO020")
                'strHdrItem = strHdrItem.PadRight(22) + "Description"
                strHdrItem = strHdrItem.PadRight(19) + getValueByKey("CLSPSO021")
                sbHdrItem.Append(strHdrItem)
                'strHdrItemQty = strHdrItemQty.PadLeft(24) + "Qty"
                strHdrItemQty = strHdrItemQty.PadLeft(24) + getValueByKey("CLSPSO022")
                sbHdrItem.Append(strHdrItemQty)
            ElseIf PrintSOTransaction = PrintSOTransactionSet.SOReturnStatus Then
                'sbHdrItem.Append(strLineA4 + vbCrLf)
                'strHdrItem = "Item Code"
                'strHdrItem = strHdrItem.PadRight(22) + "Description"
                strHdrItem = getValueByKey("CLSPSO020").PadRight(19)
                strHdrItem += getValueByKey("CLSPSO021").PadRight(33)
 
                'strHdrItemQty = strHdrItemQty.PadLeft(24) + "Qty"
                'strHdrItemQty = strHdrItemQty.PadRight(34) + "Price"
                strHdrItem += getValueByKey("CLSPSO022").PadRight(12)
                strHdrItem += getValueByKey("CLSPSO023").PadRight(14)

                'strHdrItemDisc = strHdrItemDisc.PadLeft(5) + "Disc"
                'strHdrItemDisc = strHdrItemDisc.PadRight(14) + "Tax"
                'strHdrItem += getValueByKey("CLSPSO024").PadRight(14)
                strHdrItem += getValueByKey("CLSPSO025").PadRight(14)

                'strHdrItemNet = strHdrItemNet.PadLeft(9) + "Net"
                'strHdrItemNet = strHdrItemNet.PadLeft(9 + 2) + getValueByKey("CLSPSO026")
                strHdrItem += getValueByKey("CLSPSO026").PadRight(12)

                strHdrItemNet = strHdrItem
            Else
                'sbHdrItem.Append(strLineA4 + vbCrLf)
                'strHdrItem = "Item Code"
                'strHdrItem = strHdrItem.PadRight(22) + "Description"
                strHdrItem = getValueByKey("CLSPSO020").PadRight(19)
                strHdrItem += getValueByKey("CLSPSO021").PadRight(33)

                'sbHdrItem.Append(strHdrItem)
                'strHdrItemQty = strHdrItemQty.PadLeft(24) + "Qty"
                'strHdrItemQty = strHdrItemQty.PadRight(34) + "Price"
                strHdrItem += getValueByKey("CLSPSO022").PadRight(9)
                strHdrItem += getValueByKey("CLSPSO023").PadRight(12)
                'sbHdrItem.Append(strHdrItemQty)
                'strHdrItemDisc = strHdrItemDisc.PadLeft(5) + "Disc"
                'strHdrItemDisc = strHdrItemDisc.PadRight(14) + "Tax"
                strHdrItem += getValueByKey("CLSPSO024").PadRight(12)
                strHdrItem += getValueByKey("CLSPSO025").PadRight(13)

                'strHdrItemNet = strHdrItemNet.PadLeft(9) + "Net"
                strHdrItem += getValueByKey("CLSPSO026").PadRight(12)

                sbHdrItem.Append(strHdrItem)
            End If

            Dim sbHdrItemInfo As New StringBuilder
            Dim strArticleCode As String = ""
            Dim strArticleDesc As String = ""
            Dim iPurchasedQty As Integer = 0
            Dim strPurchasedQty As String = ""
            Dim iPrice As String = ""
            Dim iNetAmount As String = ""
            Dim strReservedQty As String = ""
            Dim strCLP As String = ""
            Dim itemCount As Integer = 1
            Dim strPickUp As String = ""
            Dim strDisc As String = ""
            Dim strTax As String = ""
            Dim decTax As Decimal = 0.0

            If PrintSOTransaction = PrintSOTransactionSet.Status Then
                'strHdrItemNet = strHdrItemNet.PadRight(16) + "Pick up"
                'strHdrItemNet = strHdrItemNet.PadRight(16 + 3) + getValueByKey("CLSPSO027")
                'strHdrItemNet = strHdrItemNet.PadLeft(16 + 7) + getValueByKey("CLSPSO027")
                sbHdrItem.Append(getValueByKey("CLSPSO027"))

                'Dim strHdrItemRes As String = ""
                ''strHdrItemRes = strHdrItemRes.PadLeft(1) + "Res"
                'strHdrItemRes = strHdrItemRes.PadLeft(1) + getValueByKey("CLSPSO028")
                'strHdrItemRes = strHdrItemRes.PadRight(5)
                'sbHdrItem.Append(strHdrItemRes)

                'Dim strHdrItemCLP As String = ""
                ''strHdrItemCLP = "CLP"
                'strHdrItemCLP = getValueByKey("CLSPSO029")
                ''strHdrItemCLP = strHdrItemCLP.PadRight(4)
                'strHdrItemCLP = strHdrItemCLP.PadLeft(4)
                'sbHdrItem.Append(strHdrItemCLP)

                Dim decTotalPickupQty As Decimal
                Dim decPickupQty As Decimal
                Dim decDeliveredQty As Decimal

                For Each drGrid As DataRow In _dtSalesItemDetails.Rows
                    If Not (drGrid.RowState = DataRowState.Deleted) Then
                        If Not drGrid("IsStatus") Is DBNull.Value Then
                            If drGrid("IsStatus") = "Deleted" Then
                                Continue For
                            End If
                        End If

                        ValidateDataRow(drGrid("ArticleCode"), strArticleCode)
                        'strArticleCode = strArticleCode.PadRight(20 - 1)

                        ValidateDataRow(drGrid("Discription"), strArticleDesc)
                        strArticleDesc = strArticleDesc.PadRight(31)

                        ValidateDataRow(drGrid("Quantity"), iPurchasedQty)
                        strPurchasedQty = iPurchasedQty.ToString()
                        'strPurchasedQty = strPurchasedQty.PadLeft(4)
                        'strPurchasedQty = strPurchasedQty.PadRight(4)

                        ValidateDataRow(drGrid("SellingPrice"), iPrice)


                        iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, SORoundOff)
                        'iPrice = iPrice.PadLeft(12 + 2)
                        'iPrice = iPrice.PadRight(12 + 2)

                        ValidateDataRow(drGrid("totaldiscpercentage"), strDisc)

                        'strDisc = PrintFormatCurrency(strDisc, DefaultCurrency, SORoundOff)
                        If strDisc <> String.Empty Then
                            strDisc = FormatNumber(strDisc, 2)
                            strDisc = strDisc & " %"
                        End If

                        'strDisc = strDisc.PadLeft(11)
                        'strDisc = strDisc.PadRight(11)

                        ValidateDataRow(drGrid("TotalTaxAmt"), decTax)


                        strTax = PrintFormatCurrency(decTax, DefaultCurrency, SORoundOff)
                        strTax = "  " & strTax
                        'strTax = strTax.PadLeft(10 + 2)
                        'strTax = strTax.PadRight(10 + 2)

                        ValidateDataRow(drGrid("NetAmount"), iNetAmount)

                        iNetAmount = PrintFormatCurrency(iNetAmount, DefaultCurrency, SORoundOff)

                        'iNetAmount = iNetAmount.PadLeft(12)
                        'iNetAmount = iNetAmount.PadRight(12)


                        If Not drGrid("PickUpQty") Is DBNull.Value Then
                            decPickupQty = 0
                            decDeliveredQty = 0
                            decPickupQty = drGrid("PickUpQty")
                            If Not drGrid("DeliveredQty") Is DBNull.Value Then
                                decDeliveredQty = drGrid("DeliveredQty")
                            End If
                            decTotalPickupQty = decPickupQty + decDeliveredQty
                        End If

                        'Changed by Rohit to remove decimal places from pickup qty value
                        strPickUp = Math.Round(decTotalPickupQty, 0).ToString()
                        'End change

                        'strPickUp = strPickUp.PadLeft(6)
                        'strPickUp = strPickUp.PadRight(6)

                        'strReservedQty = IIf(drGrid("ReservedQty") Is DBNull.Value, 0, drGrid("ReservedQty"))
                        ''ValidateDataRow(drGrid("ReservedQty"), strReservedQty)

                        'strReservedQty = strReservedQty.PadLeft(7 - 1)
                        'strReservedQty = strReservedQty.PadRight(7)

                        'strCLP = IIf(drGrid("IsCLP") Is DBNull.Value, "False", "True")
                        ''ValidateDataRow(drGrid("IsCLP"), strCLP)
                        'strCLP = strCLP.PadLeft(6)
                        'strCLP = strCLP.PadRight(6)

                        iItemDetailHeight = iItemDetailHeight + 15

                        Dim itemDetails As String = String.Empty
                        itemDetails = strArticleCode
                        itemDetails += strArticleDesc.PadLeft(50 - itemDetails.Length)
                        itemDetails += strPurchasedQty.PadLeft(56 - itemDetails.Length)
                        itemDetails += iPrice.PadLeft(69 - itemDetails.Length)
                        itemDetails += strDisc.PadLeft(79 - itemDetails.Length)
                        itemDetails += strTax.PadLeft(91 - itemDetails.Length)
                        itemDetails += iNetAmount.PadLeft(106 - itemDetails.Length)
                        itemDetails += strPickUp.PadLeft(117 - itemDetails.Length)

                        sbHdrItemInfo.Append(itemDetails + vbCrLf)
                        'sbHdrItemInfo.Append(strArticleCode & strArticleDesc & strPurchasedQty.ToString() & iPrice & strDisc & strTax & iNetAmount & strPickUp & strReservedQty & strCLP + vbCrLf)
                        itemCount += 1
                    End If
                Next
                A4GetOtherCharges(strArticleCode, strArticleDesc, strPurchasedQty, iPrice, strDisc, strTax, iNetAmount, sbHdrItemInfo)
            ElseIf PrintSOTransaction = PrintSOTransactionSet.SOReturnStatus Then
                'strHdrItemNet = strHdrItemNet.PadRight(16) + "ReturnedQty"
                strHdrItemNet += getValueByKey("CLSPSO036")
                sbHdrItem.Append(strHdrItemNet)

                Dim strFilter As String

                'Changed by Rohit for SO return printout
                'strFilter = "PickUpQty > 0"
                strFilter = "PickUpQty < 0"
                'Changed by Rohit for SO return printout

                For Each drGrid As DataRow In _dtSalesItemDetails.Select(strFilter)


                    If Not (drGrid.RowState = DataRowState.Deleted) Then
                        If Not drGrid("IsStatus") Is DBNull.Value Then
                            If drGrid("IsStatus") = "Deleted" Then
                                Continue For
                            End If
                        End If

                        ValidateDataRow(drGrid("ArticleCode"), strArticleCode)
                        'strArticleCode = strArticleCode.PadRight(20 - 1)

                        ValidateDataRow(drGrid("Discription"), strArticleDesc)
                        'strArticleDesc = strArticleDesc.PadRight(30 + 3)

                        ValidateDataRow(drGrid("Quantity"), iPurchasedQty)
                        strPurchasedQty = iPurchasedQty.ToString()
                        'strPurchasedQty = strPurchasedQty.PadLeft(8 - 2)
                        'strPurchasedQty = strPurchasedQty.PadRight(7)

                        ValidateDataRow(drGrid("SellingPrice"), iPrice)
                        iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, SORoundOff)
                        'iPrice = iPrice.PadLeft(12 + 2)
                        'iPrice = iPrice.PadRight(10)

                        ValidateDataRow(drGrid("totaldiscpercentage"), strDisc)
                        'strDisc = PrintFormatCurrency(strDisc, DefaultCurrency, SORoundOff)
                        If strDisc <> String.Empty Then
                            strDisc = FormatNumber(strDisc, 2)
                            strDisc = strDisc & " %"
                        End If

                        'strDisc = strDisc.PadLeft(12 + 1)
                        'strDisc = strDisc.PadRight(12 + 1)

                        ValidateDataRow(drGrid("TotalTaxAmt"), decTax)
                        strTax = PrintFormatCurrency(decTax * (-1), DefaultCurrency, SORoundOff)
                        strTax = "   " + strTax
                        'strTax = strTax.PadLeft(12)
                        'strTax = strTax.PadRight(17)

                        ValidateDataRow(drGrid("ExclTaxAmt"), decTax)

                        ValidateDataRow((-1) * drGrid("PickUpQty"), strPickUp)

                        'strPickUp = strPickUp.PadLeft(6 + 3)
                        'strPickUp = strPickUp.PadRight(7 + 2)

                        ValidateDataRow(drGrid("ReservedQty"), strReservedQty)
                        'strReservedQty = strReservedQty.PadLeft(3 + 3)
                        'strReservedQty = strReservedQty.PadRight(5 + 1)

                        If PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                            Dim decPrice As Decimal = 0
                            ValidateDataRow(drGrid("SellingPrice"), decPrice)
                            Dim iPickupQty As Integer = 0
                            ValidateDataRow(drGrid("PickUpQty"), iPickupQty)
                            iNetAmount = decPrice * iPickupQty
                            iNetAmount = Decimal.Add(iNetAmount, decTax)
                        Else
                            ValidateDataRow(drGrid("NetAmount"), iNetAmount)
                            iNetAmount = Decimal.Add(iNetAmount, decTax)
                        End If

                        'Added by Rohit for So return print correction
                        Dim tempnetamt As String = String.Empty
                        tempnetamt = iNetAmount
                        iNetAmount = PrintFormatCurrency(iNetAmount * (-1), DefaultCurrency, SORoundOff)
                        'Added by Rohit for So return print correction
                        'iNetAmount = iNetAmount.PadLeft(13 + 1)
                        'iNetAmount = iNetAmount.PadRight(13 + 1)

                        ValidateDataRow(drGrid("IsCLP"), strCLP)
                        'strCLP = strCLP.PadLeft(4 + 2)
                        'strCLP = strCLP.PadRight(4 + 2)

                        iItemDetailHeight = iItemDetailHeight + 15
                        Dim itemDetails As String = String.Empty

                        If PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then

                            itemDetails = strArticleCode
                            itemDetails += strArticleDesc.PadLeft(50 - itemDetails.Length)
                            itemDetails += strPurchasedQty.PadLeft(56 - itemDetails.Length)
                            itemDetails += iPrice.PadLeft(69 - itemDetails.Length)
                            itemDetails += strDisc.PadLeft(79 - itemDetails.Length)
                            itemDetails += strTax.PadLeft(91 - itemDetails.Length)
                            itemDetails += iNetAmount.PadLeft(106 - itemDetails.Length)
                            itemDetails += strPickUp.PadLeft(117 - itemDetails.Length)

                            sbHdrItemInfo.Append(itemDetails + vbCrLf)
                            'sbHdrItemInfo.Append(strArticleCode & strArticleDesc & strPickUp & iPrice & strDisc & strTax & iNetAmount + vbCrLf)
                        Else
                            'sbHdrItemInfo.Append(strArticleCode & strArticleDesc & strPurchasedQty.ToString() & iPrice & strDisc & strTax & iNetAmount & strPickUp + vbCrLf)
                            'Added by Rohit for So return print correction

                            itemDetails = strArticleCode.PadRight(18)
                            itemDetails += strArticleDesc.PadLeft(49 - itemDetails.Length)
                            itemDetails += strPurchasedQty.PadLeft(56 - itemDetails.Length)
                            itemDetails += iPrice.PadLeft(72 - itemDetails.Length)
                            itemDetails += strTax.PadLeft(84 - itemDetails.Length)
                            itemDetails += iNetAmount.PadLeft(100 - itemDetails.Length)
                            itemDetails += strPickUp.PadLeft(116 - itemDetails.Length)

                            sbHdrItemInfo.Append(itemDetails + vbCrLf)
                            'sbHdrItemInfo.Append(strArticleCode & strArticleDesc & strPurchasedQty.ToString() & iPrice & strTax & iNetAmount & strPickUp + vbCrLf)
                            iNetAmount = tempnetamt
                            'Added by Rohit for So return print correction
                        End If
                        itemCount += 1
                    End If
                Next

            ElseIf PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Or PrintSOTransaction = PrintSOTransactionSet.Payment Then


                Dim strFilter As String
                If PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                    strFilter = "PickUpQty > 0"
                Else
                    'strHdrItemNet = strHdrItemNet.PadRight(18) + "Pick up"
                    strHdrItemNet = getValueByKey("CLSPSO027")

                    strFilter = "Quantity>0"
                End If
                sbHdrItem.Append(strHdrItemNet)


                For Each drGrid As DataRow In _dtSalesItemDetails.Select(strFilter)
                    If Not (drGrid.RowState = DataRowState.Deleted) Then
                        Try
                            If Not drGrid("IsStatus") Is DBNull.Value Then
                                If drGrid("IsStatus") = "Deleted" Then
                                    Continue For
                                End If
                            End If

                        Catch ex As Exception

                        End Try

                        ValidateDataRow(drGrid("ArticleCode"), strArticleCode)

                        'Change for issue 5949
                        'strArticleCode = strArticleCode.PadRight(20 - 1)

                        ValidateDataRow(drGrid("Discription"), strArticleDesc)
                        'strArticleDesc = strArticleDesc.PadRight(30 + 3)

                        ValidateDataRow(drGrid("Quantity"), iPurchasedQty)
                        strPurchasedQty = iPurchasedQty.ToString()
                        'strPurchasedQty = strPurchasedQty.PadLeft(8 - 2)
                        'strPurchasedQty = strPurchasedQty.PadRight(7)

                        ValidateDataRow(drGrid("SellingPrice"), iPrice)
                        iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, SORoundOff)
                        'iPrice = iPrice.PadLeft(12 + 1)
                        'iPrice = iPrice.PadRight(12 + 2)

                        ValidateDataRow(drGrid("totaldiscpercentage"), strDisc)
                        'strDisc = PrintFormatCurrency(IIf(strDisc <> "", strDisc, 0), DefaultCurrency, SORoundOff)
                        If strDisc <> String.Empty Then
                            strDisc = FormatNumber(strDisc, 2)
                            strDisc = strDisc & " %"
                        End If
                        'strDisc = strDisc.PadLeft(11)
                        'strDisc = strDisc.PadRight(9)

                        ValidateDataRow(drGrid("TotalTaxAmt"), decTax)
                        strTax = PrintFormatCurrency(decTax, DefaultCurrency, SORoundOff)
                        'strTax = strTax.PadLeft(11)
                        'strTax = strTax.PadRight(11)

                        ValidateDataRow(drGrid("ExclTaxAmt"), decTax)

                        'Changed By Rohit for Issue No. 0005950 (Remove decimal places from qty value)
                        'ValidateDataRow(drGrid("PickUpQty"), strPickUp)
                        ValidateDataRow(Math.Round(drGrid("PickUpQty"), 0), strPickUp)
                        'Change End
                        'strPickUp = strPickUp.PadLeft(5)
                        'strPickUp = strPickUp.PadRight(7)

                        ValidateDataRow(drGrid("ReservedQty"), strReservedQty)
                        'strReservedQty = strReservedQty.PadLeft(7 - 1)
                        'strReservedQty = strReservedQty.PadRight(7 - 1)


                        If PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                            Dim decPrice As Decimal = 0
                            ValidateDataRow(drGrid("SellingPrice"), decPrice)
                            Dim iPickupQty As Integer = 0
                            ValidateDataRow(drGrid("PickUpQty"), iPickupQty)

                            iNetAmount = (decPrice * iPickupQty) - IIf(drGrid("Discount") Is DBNull.Value, 0, drGrid("Discount"))
                            iNetAmount = Decimal.Add(iNetAmount, decTax)
                        Else
                            ValidateDataRow(drGrid("NetAmount"), iNetAmount)
                            iNetAmount = Decimal.Add(iNetAmount, decTax)
                        End If

                        iNetAmount = PrintFormatCurrency(iNetAmount, DefaultCurrency, SORoundOff)
                        'iNetAmount = iNetAmount.PadLeft(12)
                        'iNetAmount = iNetAmount.PadRight(12)

                        ValidateDataRow(drGrid("IsCLP"), strCLP)
                        'strCLP = strCLP.PadLeft(4 + 2)
                        'strCLP = strCLP.PadRight(4 + 2)
                        iItemDetailHeight = iItemDetailHeight + 15

                        Dim itemDetails As String = String.Empty

                        If PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then

                            itemDetails = strArticleCode
                            itemDetails += strArticleDesc.PadLeft(50 - itemDetails.Length)
                            itemDetails += strPickUp.PadLeft(56 - itemDetails.Length)
                            itemDetails += iPrice.PadLeft(69 - itemDetails.Length)
                            itemDetails += strDisc.PadLeft(79 - itemDetails.Length)
                            itemDetails += strTax.PadLeft(91 - itemDetails.Length)
                            itemDetails += iNetAmount.PadLeft(106 - itemDetails.Length)
                            'itemDetails += strPickUp.PadLeft(117 - itemDetails.Length)

                            sbHdrItemInfo.Append(itemDetails + vbCrLf)
                            'sbHdrItemInfo.Append(strArticleCode & strArticleDesc & strPickUp & iPrice & strDisc & strTax & iNetAmount + vbCrLf)
                        Else

                            itemDetails = strArticleCode
                            itemDetails += strArticleDesc.PadLeft(50 - itemDetails.Length)
                            itemDetails += strPurchasedQty.PadLeft(56 - itemDetails.Length)
                            itemDetails += iPrice.PadLeft(69 - itemDetails.Length)
                            itemDetails += strDisc.PadLeft(79 - itemDetails.Length)
                            itemDetails += strTax.PadLeft(91 - itemDetails.Length)
                            itemDetails += iNetAmount.PadLeft(106 - itemDetails.Length)
                            itemDetails += strPickUp.PadLeft(117 - itemDetails.Length)

                            sbHdrItemInfo.Append(itemDetails + vbCrLf)
                            'sbHdrItemInfo.Append(strArticleCode & strArticleDesc & strPurchasedQty.ToString() & iPrice & strDisc & strTax & iNetAmount & strPickUp + vbCrLf)
                        End If
                        itemCount += 1
                    End If
                Next
                If PrintSOTransaction = PrintSOTransactionSet.Payment Then
                    A4GetOtherCharges(strArticleCode, strArticleDesc, strPurchasedQty, iPrice, strDisc, strTax, iNetAmount, sbHdrItemInfo)
                End If
            ElseIf PrintSOTransaction = PrintSOTransactionSet.GiftVoucherDocumentPrint Then

                'strHdrItemNet = strHdrItemNet.PadRight(4) + "Pick up"
                strHdrItemNet = strHdrItemNet.PadRight(4) + getValueByKey("CLSPSO027")
                sbHdrItem.Append(strHdrItemNet)
                Dim strHdrItemRes As String = ""
                'strHdrItemRes = strHdrItemRes.PadLeft(4) + "Res"
                strHdrItemRes = strHdrItemRes.PadLeft(4) + getValueByKey("CLSPSO028")
                strHdrItemRes = strHdrItemRes.PadRight(9)
                sbHdrItem.Append(strHdrItemRes)

                'Dim strHdrItemCLP As String = ""
                'strHdrItemCLP = "CLP"
                'strHdrItemCLP = strHdrItemCLP.PadRight(4)
                'sbHdrItem.Append(strHdrItemCLP)

                For Each drGrid As DataRow In _dtSalesItemDetails.Rows
                    If Not (drGrid.RowState = DataRowState.Deleted) Then
                        If Not drGrid("IsStatus") Is DBNull.Value Then
                            If drGrid("IsStatus") = "Deleted" Then
                                Continue For
                            End If
                        End If
                        ValidateDataRow(drGrid("ArticleCode"), strArticleCode)
                        strArticleCode = strArticleCode.PadRight(20)

                        ValidateDataRow(drGrid("Discription"), strArticleDesc)
                        strArticleDesc = strArticleDesc.PadRight(31)

                        ValidateDataRow(drGrid("Quantity"), iPurchasedQty)
                        strPurchasedQty = iPurchasedQty.ToString()
                        strPurchasedQty = strPurchasedQty.PadLeft(8)
                        strPurchasedQty = strPurchasedQty.PadRight(8)

                        'Changed By Rohit for Issue No. 0005950 (Remove decimal places from qty value)
                        'ValidateDataRow(drGrid("PickUpQty"), strPickUp)
                        ValidateDataRow(Math.Round(drGrid("PickUpQty"), 0), strPickUp)
                        'Change End
                        strPickUp = strPickUp.PadLeft(8)
                        strPickUp = strPickUp.PadRight(8)

                        ValidateDataRow(drGrid("ReservedQty"), strReservedQty)
                        strReservedQty = strReservedQty.PadLeft(12)
                        strReservedQty = strReservedQty.PadRight(12)
                        iItemDetailHeight = iItemDetailHeight + 15
                        sbHdrItemInfo.Append(strArticleCode & strArticleDesc & strPurchasedQty.ToString() & strPickUp & strReservedQty + vbCrLf)
                        itemCount += 1
                    End If
                Next
            ElseIf PrintSOTransaction = PrintSOTransactionSet.SOReturnStatus Then

                Dim strFilter As String
                If PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                    strFilter = "PickUpQty > 0"
                Else
                    'strHdrItemNet = strHdrItemNet.PadRight(18) + "Pick up"
                    strHdrItemNet = strHdrItemNet.PadRight(18) + getValueByKey("CLSPSO027")
                    strFilter = "PickUpQty>0"
                End If
                sbHdrItem.Append(strHdrItemNet)


                For Each drGrid As DataRow In _dtSalesItemDetails.Select(strFilter)
                    If Not (drGrid.RowState = DataRowState.Deleted) Then


                        Try
                            If Not drGrid("IsStatus") Is DBNull.Value Then
                                If drGrid("IsStatus") = "Deleted" Then
                                    Continue For
                                End If
                            End If

                        Catch ex As Exception

                        End Try

                        ValidateDataRow(drGrid("ArticleCode"), strArticleCode)

                        strArticleCode = strArticleCode.PadRight(20)
                        ValidateDataRow(drGrid("Discription"), strArticleDesc)

                        strArticleDesc = strArticleDesc.PadRight(31)

                        ValidateDataRow(drGrid("PickUpQty"), iPurchasedQty)

                        strPurchasedQty = iPurchasedQty.ToString()
                        strPurchasedQty = strPurchasedQty.PadLeft(8)
                        strPurchasedQty = strPurchasedQty.PadRight(8)

                        ValidateDataRow(drGrid("SellingPrice"), iPrice)

                        iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, SORoundOff)
                        iPrice = iPrice.PadLeft(12)
                        iPrice = iPrice.PadRight(12)

                        'ValidateDataRow(drGrid("totaldiscpercentage"), strDisc)


                        'strDisc = PrintFormatCurrency(strDisc, DefaultCurrency, SORoundOff)
                        'strDisc = strDisc.PadLeft(12)
                        'strDisc = strDisc.PadRight(12)

                        ValidateDataRow(drGrid("TotalTaxAmt"), decTax)

                        strTax = PrintFormatCurrency(decTax, DefaultCurrency, SORoundOff)
                        strTax = strTax.PadLeft(9)
                        strTax = strTax.PadRight(9)

                        ValidateDataRow(drGrid("ExclTaxAmt"), decTax)

                        'ValidateDataRow(drGrid("PickUpQty"), strPickUp)
                        'strPickUp = strPickUp.PadLeft(6)
                        'strPickUp = strPickUp.PadRight(7)

                        ValidateDataRow(drGrid("ReservedQty"), strReservedQty)
                        strReservedQty = strReservedQty.PadLeft(3)
                        strReservedQty = strReservedQty.PadRight(5)



                        Dim decPrice As Decimal = 0
                        ValidateDataRow(drGrid("SellingPrice"), decPrice)
                        Dim iPickupQty As Integer = 0
                        ValidateDataRow(drGrid("PickUpQty"), iPickupQty)
                        iNetAmount = decPrice * iPickupQty

                        iNetAmount = PrintFormatCurrency(iNetAmount, DefaultCurrency, SORoundOff)
                        iNetAmount = iNetAmount.PadLeft(13)
                        iNetAmount = iNetAmount.PadRight(13)

                        sbHdrItemInfo.Append(strArticleCode & strArticleDesc & iPurchasedQty.ToString() & iPrice & strTax & iNetAmount + vbCrLf)

                        itemCount += 1
                    End If
                Next
            End If
            'Sales Item 
            'sbHdrItemInfo.Append(strLineA4)
            strItemSales = sbHdrItemInfo.ToString()
            'Return header 
            strItemHeader = sbHdrItem.ToString()
            Return True
        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    '''  Other Cost Printing into grid .
    ''' </summary>
    ''' <param name="strArticleCode"></param>
    ''' <param name="strArticleDesc"></param>
    ''' <param name="strPurchasedQty"></param>
    ''' <param name="iPrice"></param>
    ''' <param name="strDisc"></param>
    ''' <param name="strTax"></param>
    ''' <param name="iNetAmount"></param>
    ''' <param name="sbHdrItemInfo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function A4GetOtherCharges(ByVal strArticleCode As String, ByVal strArticleDesc As String, ByVal strPurchasedQty As String, ByVal iPrice As String, ByVal strDisc As String, ByVal strTax As String, ByVal iNetAmount As String, ByRef sbHdrItemInfo As StringBuilder) As Boolean
        Try
            Dim objNetAmount As Object
            Dim decTax As Decimal
            If Not _dsOtherCost Is Nothing AndAlso _dsOtherCost.Tables.Contains("NewOtherCharges") Then
                For Each drGrid As DataRow In _dsOtherCost.Tables("NewOtherCharges").Rows
                    If Not (drGrid.RowState = DataRowState.Deleted) Then

                        'strArticleCode = "Add. Cost"
                        strArticleCode = getValueByKey("CLSPSO030")

                        strArticleDesc = IIf(Not (drGrid("ChargeName") Is DBNull.Value), drGrid("ChargeName"), String.Empty)
                        'strArticleDesc = strArticleDesc.PadRight(30 + 3)

                        strPurchasedQty = "1"

                        iPrice = drGrid("ChargeAmount")

                        iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, SORoundOff)
                        If PrintSOTransaction = PrintSOTransactionSet.Payment Then
                            'iPrice = iPrice.PadLeft(12 + 2 + 1)
                            'iPrice = iPrice.PadRight(12 + 2 + 1)
                        Else
                            'iPrice = iPrice.PadLeft(12 + 2)
                            'iPrice = iPrice.PadRight(12 + 2)
                        End If
                
                        strDisc = Decimal.Zero
                        strDisc = PrintFormatCurrency(strDisc, DefaultCurrency, SORoundOff)
                        'strDisc = strDisc.PadLeft(10 + 1)
                        'strDisc = strDisc.PadRight(10 - 1)

                        decTax = 0.0
                        If Not drGrid("TaxAmt") Is DBNull.Value Then
                            decTax = drGrid("TaxAmt")
                        End If
                        strTax = PrintFormatCurrency(decTax, DefaultCurrency, SORoundOff)
                        'strTax = strTax.PadLeft(10 + 2)
                        'strTax = strTax.PadRight(10 + 1)

                        iNetAmount = ""
                        objNetAmount = drGrid("ChargeAmount")
                        If Not objNetAmount Is Nothing AndAlso Not objNetAmount Is DBNull.Value Then
                            iNetAmount = Decimal.Add(CDbl(objNetAmount), decTax).ToString()
                        Else
                            iNetAmount = drGrid("ChargeAmount")
                        End If

                        iNetAmount = PrintFormatCurrency(iNetAmount, DefaultCurrency, SORoundOff)
                        'iNetAmount = iNetAmount.PadLeft(14)
                        'iNetAmount = iNetAmount.PadRight(14)

                        Dim itemDetails As String = String.Empty

                        itemDetails = strArticleCode.PadRight(20)
                        itemDetails += strArticleDesc.PadLeft(23 - itemDetails.Length)
                        itemDetails += strPurchasedQty.PadLeft(56 - itemDetails.Length)
                        itemDetails += iPrice.PadLeft(69 - itemDetails.Length)
                        itemDetails += strDisc.PadLeft(79 - itemDetails.Length)
                        itemDetails += strTax.PadLeft(91 - itemDetails.Length)
                        itemDetails += iNetAmount.PadLeft(106 - itemDetails.Length)

                        sbHdrItemInfo.Append(itemDetails + vbCrLf)
                        'sbHdrItemInfo.Append(strArticleCode & strArticleDesc & strPurchasedQty & iPrice & strDisc & strTax & iNetAmount + vbCrLf)

                    End If
                Next
            End If

        Catch ex As Exception

        End Try
    End Function

#End Region


#Region "L40 "
    Public Function L40GetTitle() As String
        Try
            Dim sbTitle As New StringBuilder
            Dim strTitleInfo As String

            If PrintSOTransaction = PrintSOTransactionSet.Status Then
                sbTitle.Append(strLineL40 + vbCrLf)

                'strTitleInfo = String.Format("SALES ORDER {0}", SalesOrderNo)
                strTitleInfo = String.Format(getValueByKey("CLSPS4001") + vbCrLf, SalesOrderNo)

                'strTitleInfo = String.Format("STATUS {0}",SOStatus)
                strTitleInfo += String.Format(getValueByKey("CLSPS4002") + vbCrLf, SOStatus)

                'strTitleInfo = String.Format("CREATION DATE {0}", Now.Date)
                strTitleInfo += String.Format(getValueByKey("CLSPSO031") + vbCrLf, DateTime.Now)

                sbTitle.Append(strTitleInfo)
                sbTitle.Append(strLineL40)

            ElseIf PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                sbTitle.Append(strLineL40 + vbCrLf)

                'strTitleInfo = String.Format("SALES ORDER {0}", SalesOrderNo)
                strTitleInfo = String.Format(getValueByKey("CLSPS4001") + vbCrLf, SalesOrderNo)

                'strTitleInfo = String.Format("STATUS {0}",DELIVERY NOTE)
                strTitleInfo += String.Format(getValueByKey("CLSPS4002") + vbCrLf, getValueByKey("CLSBL024"))

                'strTitleInfo = String.Format("CREATION DATE {0}", Now.Date)
                strTitleInfo += String.Format(getValueByKey("CLSPSO031") + vbCrLf, DateTime.Now)

                sbTitle.Append(strTitleInfo)
                sbTitle.Append(strLineL40)

            ElseIf PrintSOTransaction = PrintSOTransactionSet.Payment Then
                sbTitle.Append(strLineL40 + vbCrLf)

                'Dim strPaymentHdr As String = String.Format("CASH MEMO Nr {0}", InvoiceNumber)

                'Dim strPaymentHdr As String = String.Format(getValueByKey("CLSPSO006"), InvoiceNumber)
                'If (Not String.IsNullOrEmpty(InvoiceNumber)) Then
                '    sbTitle.Append(SplitString(strPaymentHdr).ToString() + vbCrLf)
                'End If

                'strTitleInfo = String.Format("SALES ORDER {0}", SalesOrderNo)
                strTitleInfo = String.Format(getValueByKey("CLSPS4001") + vbCrLf, SalesOrderNo)

                'strTitleInfo = String.Format("STATUS {0}",DELIVERY NOTE)
                strTitleInfo += String.Format(getValueByKey("CLSPS4002") + vbCrLf, getValueByKey("CLSBL022"))

                'strTitleInfo = String.Format("CREATION DATE {0}", Now.Date)
                strTitleInfo += String.Format(getValueByKey("CLSPSO031") + vbCrLf, DateTime.Now)

                sbTitle.Append(strTitleInfo)
                sbTitle.Append(strLineL40)

            ElseIf PrintSOTransaction = PrintSOTransactionSet.GiftVoucherDocumentPrint Then
                sbTitle.Append(strLineL40 + vbCrLf)

                'Dim strPaymentHdr As String = String.Format("CASH MEMO Nr {0}", InvoiceNumber)
                Dim strPaymentHdr As String = String.Format(getValueByKey("CLSPSO006"), InvoiceNumber)
                sbTitle.Append(SplitString(strPaymentHdr).ToString() + vbCrLf)
                'strTitleInfo = String.Format("SALES ORDER  {0} Gift Voucher ", SalesOrderNo)
                strTitleInfo = String.Format(getValueByKey("CLSPSO007"), SalesOrderNo)
                strTitleInfo.PadRight(22)
                sbTitle.Append(SplitString(strTitleInfo).ToString() + vbCrLf)
                'strTitleInfo = String.Format("DATE {0}", strTitleInfo, Now.Date)
                strTitleInfo = String.Format(getValueByKey("CLSPSO032"), strTitleInfo, DateTime.Now)
                sbTitle.Append(SplitString(strTitleInfo).ToString() + vbCrLf)

                sbTitle.Append(strLineL40)
            End If

            Return sbTitle.ToString()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return String.Empty
        End Try
    End Function

    Private Function L40SOStatusPrint(ByVal PrintPreview As Boolean) As Boolean

        Try
            CheckDocumentType()
            'Comman Print 
            Dim strHeader As New StringBuilder
            Dim strSiteHeader As New StringBuilder
            Dim strFooter As New StringBuilder
            Dim strWelcomeMsg As New StringBuilder
            Dim strTaxInformation As New StringBuilder
            Dim strPromotionMsg As New StringBuilder
            PrinttHeaderAndFooter(strHeader, strFooter, strWelcomeMsg, strPromotionMsg, strTaxInformation, strSODocumentType, PrintSOPageSetup)

            'Added :Rakesh-17/07/2012: Site Information
            strSiteHeader.Append(L40OrA4GetSiteDetails(SiteCode, "L40"))

            'Title 
            'Dim strLine As String = "----------------------------------------"
            Dim sbTitle As New StringBuilder
            sbTitle.Append(L40GetTitle)

            'CustomerDetails
            Dim strCustomerDetails As String
            strCustomerDetails = L40GetCustomerDetails(_dtCustomerDetails)

            'Customer Delivery Details
            Dim strCustomerDeliveryDetails As String
            strCustomerDeliveryDetails = L40GetCustomerDeliveryDetails(_dtCustomerDetails)

            Dim strCashierDetails As String = L40CashierDetails(CreationDate, InvoiceNumber, CashierId)

            Dim strMessages As String = "" '  L40Messages()

            'Sales Item 
            Dim strItemHeader As String = ""
            Dim strItemSales As String = ""

            If Not (L40GetItemInfo(strItemHeader, strItemSales)) Then
                Return False
            End If

            Dim strPaymentInfo As String = ""
            L40GetPaymentInfo(strPaymentInfo)

            Dim sbFinalPrint As New StringBuilder
            sbFinalPrint.Append(strSiteHeader)
            sbFinalPrint.Append(strLineL40 + vbCrLf)

            If (Not String.IsNullOrEmpty(strHeader.ToString().Trim())) Then
                sbFinalPrint.Append(strHeader.ToString().Trim())
                sbFinalPrint.Append(vbCrLf + strLineL40 + vbCrLf)
            End If
            If (Not String.IsNullOrEmpty(strWelcomeMsg.ToString().Trim())) Then
                sbFinalPrint.Append(strWelcomeMsg.ToString().Trim())
                sbFinalPrint.Append(vbCrLf + strLineL40 + vbCrLf)
            End If
            
            sbFinalPrint.Append(strCashierDetails)

            If (Not String.IsNullOrEmpty(sbTitle.ToString())) Then
                sbFinalPrint.Append(vbCrLf + sbTitle.ToString() + vbCrLf)
            Else
                sbFinalPrint.Append(vbCrLf + strLineL40 + vbCrLf)
            End If

            If (Not String.IsNullOrEmpty(strTaxInformation.ToString().Trim())) Then
                sbFinalPrint.Append(strTaxInformation.ToString().Trim())
                sbFinalPrint.Append(vbCrLf + strLineL40 + vbCrLf)
            End If

            sbFinalPrint.Append(strCustomerDetails)

            If (Not String.IsNullOrEmpty(strCustomerDeliveryDetails)) Then
                sbFinalPrint.Append(strCustomerDeliveryDetails + vbCrLf)
            End If

            sbFinalPrint.Append(vbCrLf + strLineL40 + vbCrLf)
            If PrintSOTransaction = PrintSOTransactionSet.SOReturnStatus Then
                sbFinalPrint.Append(SplitString(getValueByKey("CLSPSO033") + ItemReturnReason.ToString()).ToString() + vbCrLf)
                sbFinalPrint.Append(strLineL40 + vbCrLf)
            End If

            sbFinalPrint.Append(strItemHeader + vbCrLf)
            sbFinalPrint.Append(strLineL40 + vbCrLf)
            sbFinalPrint.Append(strItemSales)

            If (Not String.IsNullOrEmpty(strPaymentInfo)) Then
                sbFinalPrint.Append(strPaymentInfo + vbCrLf)
            End If

            sbFinalPrint.Append(strLineL40 + vbCrLf)
            If PrintSOTransaction = PrintSOTransactionSet.Payment Then
                Dim strTaxDetailsInvoice As String = ""
                L40GetTaxDetails(strTaxDetailsInvoice)

                If (Not String.IsNullOrEmpty(strTaxDetailsInvoice)) Then
                    sbFinalPrint.Append(strTaxDetailsInvoice + vbCrLf)
                    sbFinalPrint.Append(strLineL40 + vbCrLf)
                End If
            End If

            If (Not String.IsNullOrEmpty(strPromotionMsg.ToString().Trim())) Then
                sbFinalPrint.Append(strPromotionMsg.ToString().Trim())
                sbFinalPrint.Append(vbCrLf + strLineL40 + vbCrLf)
            End If

            If (Not String.IsNullOrEmpty(GiftReceiptMessage)) Then
                sbFinalPrint.Append(SplitString(GiftReceiptMessage).ToString())
            End If

            Dim strTermsNConditions As String = A4GetStringTermsCondition(strDocTypeforTermsnConditions, SiteCode, PrintSOPageSetup)
            If (Not String.IsNullOrEmpty(strTermsNConditions)) Then
                sbFinalPrint.Append(strTermsNConditions + vbCrLf)
            End If

            If (Not String.IsNullOrEmpty(strFooter.ToString())) Then
                'sbFinalPrint.Append(strLineL40 + vbCrLf)
                sbFinalPrint.Append(strFooter.ToString() + vbCrLf)
            End If

            'sbFinalPrint.Append(strLineL40)

            PrinterName = SetPrinterName(dtPrinterInfo1, "SalesOrder", PrintSOTransaction.ToString)

            If PrintPreview = True Then
                fnPrint(sbFinalPrint.ToString(), "PRV")
            Else
                fnPrint(sbFinalPrint.ToString(), "PRN")
            End If
            Return True
        Catch ex As Exception

        End Try

    End Function

    Private _RoundOff As Integer = 2
    Public Property RoundOff() As Integer
        Get
            Return _RoundOff
        End Get
        Set(ByVal value As Integer)
            _RoundOff = value
        End Set
    End Property
    Public Function GetInvoiceAdvancePayment(ByVal vSiteNo As String, ByVal vSalesNo As String) As DataTable
        Try
            Dim dsScan As DataSet
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0
            vStmtQry.Append(" Select DocumentNumber as SalesNo, SaleInvNumber as InvoiceNo, CurrencyCode, DocumentType, " & vbCrLf)
            vStmtQry.Append(" dbo.FnGetDesc('Terminal',TerminalID,SiteCode) as TerminalID, " & vbCrLf)
            vStmtQry.Append(" TenderTypeCode as TenderType, " & vbCrLf)
            vStmtQry.Append(" AmountTendered as InvoiceAmt, UserName, SOInvTime as InvoiceDate " & vbCrLf)
            vStmtQry.Append(" From SalesInvoice Where status=1 and SiteCode='" & vSiteNo & "' And DocumentNumber='" & vSalesNo & "' " & vbCrLf)
            Dim daScan As SqlClient.SqlDataAdapter
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dsScan = New DataSet
            daScan.Fill(dsScan, "InvoiceDetails")
            If Not dsScan Is Nothing Then
                Return dsScan.Tables("InvoiceDetails")
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function GetAdvancePayment(ByVal dtInvoiceData As DataTable, ByRef decTotalAdvancePayment As Decimal, Optional ByRef iPaymentHeight As Single = 0) As String
        Try
            iPaymentHeight = 62
            Dim sbAdvancePayment As New StringBuilder
            If Not dtInvoiceData Is Nothing AndAlso dtInvoiceData.Rows.Count > 0 Then

                'Dim strDefaultText As String = "ADVANCE PAYD"
                Dim strDefaultText As String = getValueByKey("CLSPSO015")
                Dim strTerminalInfo As String
                Dim strInvoiceAmt As String = ""


                Dim decInvoiceAmt As Decimal = 0.0

                Dim strInvoiceNumber As String = " "
                Dim strTerminalInfoLine As String = ""
                Dim dateTimeinvoiceDate As Date
                Dim strL1 As String = ""
                Dim strCurrencyCode As String
                Dim strFinalString As String
                For Each drInvoiceData As DataRow In dtInvoiceData.Rows
                    If strInvoiceNumber <> drInvoiceData("InvoiceNo") Then
                        strTerminalInfo = ""
                        strInvoiceAmt = ""
                        dateTimeinvoiceDate = drInvoiceData("InvoiceDate")
                        strTerminalInfo = drInvoiceData("TerminalID")
                        strInvoiceNumber = drInvoiceData("InvoiceNo")
                        strTerminalInfoLine = strTerminalInfo & "/" & strInvoiceNumber
                        decInvoiceAmt = drInvoiceData("InvoiceAmt")
                        strCurrencyCode = drInvoiceData("CurrencyCode")
                        strL1 = strL1.PadRight(iPaymentHeight)
                        strL1 = strDefaultText + dateTimeinvoiceDate.ToString(clsCommon.GetSystemDateFormat()).PadLeft(14) + strTerminalInfoLine.PadLeft(4)
                        'strL1 = SplitString(strL1).ToString()
                        strL1 = strL1.PadRight(iPaymentHeight)

                        strInvoiceAmt = dtInvoiceData.Compute("sum(InvoiceAmt)", "InvoiceNo='" & strInvoiceNumber & "'")
                        strInvoiceAmt = MyRound(strInvoiceAmt, SORoundOff, _RoundOffRequired)
                        strFinalString = PrintFormatCurrency(strInvoiceAmt, strCurrencyCode, SORoundOff)
                        PadingString(strFinalString)
                        If PrintSOTransaction = PrintSOTransactionSet.Payment Then
                            If strInvoiceNumber <> _strSOCurrentInvoiceNumber Then
                                sbAdvancePayment.Append(strL1)
                                sbAdvancePayment.Append(strFinalString & vbCrLf)
                            End If
                        Else
                            sbAdvancePayment.Append(strL1)
                            sbAdvancePayment.Append(strFinalString & vbCrLf)
                        End If
                    End If
                Next
                decTotalAdvancePayment = dtInvoiceData.Compute("sum(InvoiceAmt)", "")
            End If
            Return sbAdvancePayment.ToString()
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function L40GetAdvancePayment(ByVal dtInvoiceData As DataTable, ByRef decTotalAdvancePayment As Decimal, Optional ByRef iPaymentHeight As Single = 0) As String
        Try
            iPaymentHeight = 52
            Dim sbAdvancePayment As New StringBuilder
            If Not dtInvoiceData Is Nothing AndAlso dtInvoiceData.Rows.Count > 0 Then

                ''Dim strDefaultText As String = "ADVANCE PAYD"
                'Dim strDefaultText As String = getValueByKey("CLSPSO015")

                Dim strTerminalInfo As String
                Dim strInvoiceAmt As String = ""
                Dim decInvoiceAmt As Decimal = 0.0
                Dim strInvoiceNumber As String
                Dim dateTimeinvoiceDate As Date
                Dim strCurrencyCode As String

                For Each drInvoiceData As DataRow In dtInvoiceData.Rows
                    strTerminalInfo = ""
                    strInvoiceAmt = ""

                    dateTimeinvoiceDate = drInvoiceData("InvoiceDate")
                    strTerminalInfo = drInvoiceData("TerminalID")
                    strInvoiceNumber = drInvoiceData("InvoiceNo")

                    decInvoiceAmt = dtInvoiceData.Compute("sum(InvoiceAmt)", "InvoiceNo='" & strInvoiceNumber & "'")
                    decInvoiceAmt = MyRound(decInvoiceAmt, SORoundOff, _RoundOffRequired)
                    strCurrencyCode = drInvoiceData("CurrencyCode")
                    strInvoiceAmt = PrintFormatCurrency(decInvoiceAmt, strCurrencyCode, SORoundOff)

                    sbAdvancePayment.Append(dateTimeinvoiceDate.ToString(clsCommon.GetSystemDateFormat()) + Space(12) + strTerminalInfo + vbCrLf)
                    sbAdvancePayment.Append(strInvoiceNumber + Space(39 - strInvoiceNumber.Length - strInvoiceAmt.Length) + strInvoiceAmt + vbCrLf + vbCrLf)
                Next

                decTotalAdvancePayment = dtInvoiceData.Compute("sum(InvoiceAmt)", "")
                decTotalAdvancePayment = MyRound(decTotalAdvancePayment, SORoundOff, _RoundOffRequired)
            End If
            Return sbAdvancePayment.ToString()
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function L40GetTaxDetails(ByRef strTaxDetailsForInvoice As String) As Boolean
        Try

            If Not _dtSalesOrderTaxDtls Is Nothing AndAlso _dtSalesOrderTaxDtls.Rows.Count > 0 Then

                Dim strTaxName As String = ""
                Dim decTaxvalue As Decimal = Decimal.Zero
                Dim strTaxValue As String = ""
                Dim KeySortPairs = From d In _dtSalesOrderTaxDtls _
                Group By Key = d("TaxLabel").ToString(), SortOrder = d("TaxLabel").ToString() _
                Into Group _
                Select Key, TaxAmount = Group.Sum(Function(d) CType(d("TaxValue"), Nullable(Of Decimal)))

                For Each row In KeySortPairs

                    If (GetTaxName(row.Key.ToString(), strTaxName)) Then
                        strTaxValue = PrintFormatCurrency(row.TaxAmount, DefaultCurrency, DecimalDigits)
                        strTaxDetailsForInvoice = strTaxName + Space(39 - strTaxName.Length - strTaxValue.Length) + strTaxValue
                    Else
                        strTaxValue = PrintFormatCurrency(0.0, DefaultCurrency, DecimalDigits)
                        strTaxDetailsForInvoice = strTaxDetailsForInvoice + vbCrLf + "Not Found" + strTaxValue.PadLeft(2)
                    End If
                    'strTaxValue = PrintFormatCurrency(row.TaxAmount, DefaultCurrency, DecimalDigits)
                    'strTaxDetailsForInvoice = strTaxDetailsForInvoice + vbCrLf + row.Key.PadRight(18) + strTaxValue.PadLeft(2)
                Next
                'For Each drTaxDtls As DataRow In _dtSalesOrderTaxDtls.Rows
                '    ValidateDataRow(drTaxDtls("TaxLabel"), strTaxName)
                '    ValidateDataRow(drTaxDtls("TaxValue"), decTaxvalue)
                '    strTaxValue = PrintFormatCurrency(decTaxvalue, DefaultCurrency, DecimalDigits)
                '    strTaxDetailsForInvoice = strTaxDetailsForInvoice + vbCrLf + strTaxName.PadRight(18) + strTaxValue.PadLeft(2)
                'Next
            End If
            Return True


        Catch ex As Exception

        End Try
    End Function

    Public Function L40GetPaymentInfo(ByRef strPaymentInfo As String, Optional ByVal iGap As Integer = 27) As Boolean
        Try

            Dim sbPaymentInfo As New StringBuilder
            If PrintSOTransaction = PrintSOTransactionSet.Status Or PrintSOTransaction = PrintSOTransactionSet.Payment Then
                sbPaymentInfo.Append(strLineL40 + vbCrLf)
                'Dim strTotal2Pay As String = "TOTAL  "
                Dim strTotal2Pay As String = getValueByKey("CLSPSO034") & "  "

                Dim decTotal2Pay As Decimal = _dtSalesItemDetails.Compute("sum(NetAmount)", "IsStatus<>'Deleted'")
                decTotal2Pay = MyRound(decTotal2Pay, SORoundOff, _RoundOffRequired)
                'Other Charges adding into NetAmount 
                If Not _dsOtherCost Is Nothing AndAlso _dsOtherCost.Tables.Contains("NewOtherCharges") AndAlso _dsOtherCost.Tables("NewOtherCharges").Rows.Count > 0 Then
                    decTotal2Pay = decTotal2Pay + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(ChargeAmount)", " ") + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " ")
                End If

                Dim totalValue As String = PrintFormatCurrency(decTotal2Pay, DefaultCurrency, SORoundOff)
                strTotal2Pay = strTotal2Pay.PadRight(39 - totalValue.Length) + totalValue
                sbPaymentInfo.Append(strTotal2Pay + vbCrLf)

                'Dim strTotalNetAmount As String = "NET TOTAL"
                Dim strTotalNetAmount As String = getValueByKey("CLSPSO014")
                Dim decTotalNetAmount As Decimal = _dtSalesItemDetails.Compute("sum(NetAmount)", "IsStatus<>'Deleted'")
                'Other Charges adding into NetAmount 
                If Not _dsOtherCost Is Nothing AndAlso _dsOtherCost.Tables.Contains("NewOtherCharges") AndAlso _dsOtherCost.Tables("NewOtherCharges").Rows.Count > 0 Then
                    decTotalNetAmount = decTotalNetAmount + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(ChargeAmount)", " ") + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " ")
                End If
                decTotalNetAmount = MyRound(decTotalNetAmount, SORoundOff, _RoundOffRequired)
                strTotalNetAmount = strTotalNetAmount.PadRight(iGap) + PrintFormatCurrency(decTotalNetAmount, DefaultCurrency, SORoundOff) + vbCrLf

                Dim strAdvancedPayed As String = getValueByKey("CLSPSO015") 'ADVANCE PAYD
                Dim strAdvancedPayDetails As String = String.Empty
                Dim decAdvancedPayed As Decimal = Decimal.Zero
                Dim dtAdvancePayment As DataTable = GetInvoiceAdvancePayment(SiteCode, SalesOrderNo)

                If Not dtAdvancePayment Is Nothing AndAlso dtAdvancePayment.Rows.Count > 0 Then
                    strAdvancedPayDetails = L40GetAdvancePayment(dtAdvancePayment, decAdvancedPayed)
                End If

                sbPaymentInfo.Append(vbCrLf + strAdvancedPayed + vbCrLf + strAdvancedPayDetails)
                'If Not PrintSOTransaction = PrintSOTransactionSet.Payment Then
                '    Dim strBalance2Pay As String = "BALANCE TO PAY"
                '    Dim decBalance2Pay As Decimal = Decimal.Subtract(decTotalNetAmount, decAdvancedPayed)
                '    decBalance2Pay = MyRound(decBalance2Pay, _RoundOff)
                '    sbPaymentInfo.Append("----------".PadLeft(35) + vbCrLf)
                '    strBalance2Pay = strBalance2Pay.PadRight(iGap) + MyRound(decBalance2Pay, SORoundOff).ToString()
                '    sbPaymentInfo.Append(strBalance2Pay + vbCrLf)
                'If PrintSOTransaction = PrintSOTransactionSet.Payment Then
                'Dim strSTAYTOPAY As String = "STAY TO PAY "
                Dim strSTAYTOPAY As String = getValueByKey("CLSPSO035") & " "
                Dim decStay2Pay As Decimal
                Dim strStay2Pay As String

                decStay2Pay = Decimal.Subtract(decTotalNetAmount, decAdvancedPayed)
                decStay2Pay = MyRound(decStay2Pay, _RoundOff, _RoundOffRequired)
                strStay2Pay = PrintFormatCurrency(decStay2Pay, DefaultCurrency, SORoundOff)

                strSTAYTOPAY = strSTAYTOPAY + Space(39 - strSTAYTOPAY.Length - strStay2Pay.Length) + strStay2Pay
                'sbPaymentInfo.Append("----------".PadLeft(iGap + 10) + vbCrLf)
                sbPaymentInfo.Append(strSTAYTOPAY)
                'sbPaymentInfo.Append(strSTAYTOPAY + vbCrLf)

                Dim sbPaymentSummary As New StringBuilder
                If Not PaymentDetails Is Nothing AndAlso PaymentDetails.Rows.Count > 0 Then
                    Dim strReceiptCode As String = ""
                    Dim strAmount As String = ""
                    Dim strCurrencyCode As String = ""
                    Dim strNumber As String = ""
                    Dim FinalString As String = ""
                    For Each drPayd As DataRow In PaymentDetails.Rows
                        ValidateDataRow(drPayd("RecieptType"), strReceiptCode)

                        strReceiptCode = strReceiptCode.Trim
                        If Not drPayd("CurrencyCode") Is DBNull.Value Then
                            If drPayd("CurrencyCode") = DefaultCurrency Then
                                ValidateDataRow(drPayd("Amount"), strAmount)


                            Else
                                ValidateDataRow(drPayd("AmountInCurrency"), strAmount)

                            End If
                        Else
                            ValidateDataRow(drPayd("Amount"), strAmount)
                        End If
                        If strReceiptCode.ToUpper() = CreditVoucher_R Or strReceiptCode.ToUpper() = CreditVoucher_I Or strReceiptCode.ToUpper() = GIFTVOUCHE_I Or strReceiptCode.ToUpper() = GIFTVOUCHE_R Then
                            ValidateDataRow(drPayd("Number"), strNumber)
                            strReceiptCode = strReceiptCode + "(" + strNumber + ")"
                        End If

                        ValidateDataRow(drPayd("CurrencyCode"), strCurrencyCode)

                        strAmount = PrintFormatCurrency(strAmount, strCurrencyCode, SORoundOff).Trim()
                        FinalString = strReceiptCode + Space(39 - strReceiptCode.Length - strAmount.Length) + strAmount

                        sbPaymentSummary.Append(vbCrLf + FinalString)
                        FinalString = ""

                    Next
                    'sbPaymentSummary.Append("----------".PadLeft(iGap + 10))
                    'Dim strBalance2Pay As String = vbCrLf & "BALANCE TO PAY"
                    Dim strBalance2Pay As String = vbCrLf & getValueByKey("CLSPSO034")
                    Dim decBalance2Pay As Decimal = Decimal.Subtract(decTotalNetAmount, decAdvancedPayed)
                    decBalance2Pay = MyRound(decBalance2Pay, _RoundOff, _RoundOffRequired)
                    strBalance2Pay = strBalance2Pay.PadRight(iGap) + MyRound(decBalance2Pay, SORoundOff, _RoundOffRequired).ToString()
                    strBalance2Pay = PrintFormatCurrency(strBalance2Pay, DefaultCurrency, SORoundOff)

                    If (Not String.IsNullOrEmpty(strBalance2Pay)) Then
                        sbPaymentSummary.Append(vbCrLf + strBalance2Pay + vbCrLf)
                    End If

                    sbPaymentInfo.Append(sbPaymentSummary.ToString())
                End If
            End If
            'sbPaymentInfo.Append(vbCrLf)

            strPaymentInfo = sbPaymentInfo.ToString()

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

    End Function

    Public Function L40GetItemInfo(ByRef strItemHeader As String, ByRef strItemSales As String) As Boolean
        Try
            Dim sbItemHeader As New StringBuilder()
            Dim hdrItem, hdrDescription, hdrQty, hdrPrice, hdrDisc, hdrTax, hdrNet, hdrPickup, hdrRes, hdrCLP As String

            hdrItem = getValueByKey("CLSPSO037")
            hdrDescription = getValueByKey("CLSPSO021")
            hdrQty = getValueByKey("CLSPSO022")
            hdrPrice = getValueByKey("CLSPSO023")
            hdrDisc = getValueByKey("CLSPSO024")
            hdrTax = getValueByKey("CLSPSO025")
            hdrNet = getValueByKey("CLSPSO026")
            hdrPickup = getValueByKey("CLSPSO027")
            hdrRes = getValueByKey("CLSPSO028")
            hdrCLP = getValueByKey("CLSPSO029")

            If PrintSOTransaction = PrintSOTransactionSet.GiftVoucherDocumentPrint Then
                'Item       Description
                'Qty        Pickup      Res
                sbItemHeader.Append(hdrItem.PadRight(WArticleCode) + hdrDescription + vbCrLf)
                sbItemHeader.Append(hdrQty.PadRight(10) + hdrPickup.PadRight(15) + hdrRes)

            ElseIf PrintSOTransaction = PrintSOTransactionSet.SOReturnStatus Then
                'Item
                'Description
                'Qty        Price       Tax         Net
                sbItemHeader.Append(hdrItem + vbCrLf + hdrDescription + vbCrLf)
                sbItemHeader.Append(hdrQty.PadRight(7) + hdrPrice.PadRight(13) + hdrTax.PadRight(11) + hdrNet)

            Else
                'Item
                'Description
                'Qty        Price       Net         Pickup
                sbItemHeader.Append(hdrItem + vbCrLf + hdrDescription + vbCrLf)
                sbItemHeader.Append(hdrQty.PadRight(7) + hdrPrice.PadRight(15) + hdrNet.PadRight(10) + hdrPickup)

            End If

            strItemHeader = sbItemHeader.ToString()

            Dim sbHdrItemInfo As New StringBuilder
            Dim sbArticleInfo As New StringBuilder

            Dim valArticleCode = String.Empty, valArticleDesc = String.Empty, valPurchasedQty = String.Empty, valPrice = String.Empty, valDisc As String = String.Empty
            Dim valTax = String.Empty, valNetAmount = String.Empty, valPickUp = String.Empty, valReservedQty = String.Empty, valCLP As String = String.Empty

            Dim decTotalPickupQty, decPickupQty, decDeliveredQty, decTax As Decimal
            Dim itemCount As Integer = 1

            Dim strFilterCondition As String = String.Empty

            If PrintSOTransaction = PrintSOTransactionSet.Status Then
                For Each drGrid As DataRow In _dtSalesItemDetails.Rows
                    valArticleCode = String.Empty
                    valArticleDesc = String.Empty
                    valPurchasedQty = String.Empty
                    valPrice = String.Empty
                    valDisc = String.Empty
                    valTax = String.Empty
                    valNetAmount = String.Empty
                    valPickUp = String.Empty
                    valReservedQty = String.Empty
                    valCLP = String.Empty
                    decTotalPickupQty = 0
                    decPickupQty = 0
                    decDeliveredQty = 0
                    decTax = 0

                    If Not (drGrid.RowState = DataRowState.Deleted) Then
                        If Not drGrid("IsStatus") Is DBNull.Value Then
                            If drGrid("IsStatus") = "Deleted" Then
                                Continue For
                            End If
                        End If

                        valArticleCode = IIf(Not drGrid("ArticleCode") Is DBNull.Value, drGrid("ArticleCode").ToString().PadRight(WArticleCode), String.Empty)
                        valArticleDesc = IIf(Not drGrid("Discription") Is DBNull.Value, drGrid("Discription").ToString().PadRight(30), String.Empty)
                        valPurchasedQty = IIf(Not drGrid("Quantity") Is DBNull.Value, Convert.ToInt32(drGrid("Quantity")), "0")
                        valPurchasedQty = valPurchasedQty.ToString()

                        valPrice = IIf(Not drGrid("SellingPrice") Is DBNull.Value, drGrid("SellingPrice"), "0.00")
                        valPrice = PrintFormatCurrency(valPrice, DefaultCurrency, SORoundOff)

                        valDisc = IIf(Not drGrid("TotalDiscPercentage") Is DBNull.Value, drGrid("TotalDiscPercentage").ToString(), "0.00")
                        valDisc = IIf(Not String.IsNullOrEmpty(valDisc), FormatNumber(valDisc, 2) + " %", String.Empty)

                        valTax = IIf(Not drGrid("TotalTaxAmt") Is DBNull.Value, drGrid("TotalTaxAmt").ToString(), "0.00")
                        valTax = PrintFormatCurrency(valTax, DefaultCurrency, SORoundOff)

                        valNetAmount = IIf(Not drGrid("NetAmount") Is DBNull.Value, drGrid("NetAmount").ToString(), "0.00")
                        valNetAmount = PrintFormatCurrency(valNetAmount, DefaultCurrency, SORoundOff)

                        decPickupQty = IIf(Not drGrid("PickUpQty") Is DBNull.Value, drGrid("PickUpQty"), 0)
                        decDeliveredQty = IIf(Not drGrid("DeliveredQty") Is DBNull.Value, drGrid("DeliveredQty"), 0)
                        decTotalPickupQty = decPickupQty + decDeliveredQty
                        valPickUp = Convert.ToInt32(decTotalPickupQty).ToString()

                        valReservedQty = IIf(Not drGrid("ReservedQty") Is DBNull.Value, drGrid("ReservedQty").ToString().PadRight(4), "False")
                        valCLP = IIf(Not drGrid("IsCLP") Is DBNull.Value, drGrid("IsCLP").ToString(), "False")

                        'comment to bring the desciptin in second line
                        sbHdrItemInfo.Append(SplitString(valArticleCode & vbCrLf))
                        sbHdrItemInfo.Append(SplitString(valArticleDesc & vbCrLf))

                        Dim itemDetails As String = String.Empty
                        itemDetails = valPurchasedQty
                        itemDetails += valPrice.PadLeft(15 - itemDetails.Length)
                        itemDetails += valNetAmount.PadLeft(30 - itemDetails.Length)
                        itemDetails += valPickUp.PadLeft(39 - itemDetails.Length)

                        sbHdrItemInfo.Append(SplitString(itemDetails, 40).ToString() + vbCrLf)
                        itemCount += 1
                    End If
                Next
                L40GetOtherCharges(valArticleCode, valArticleDesc, valPurchasedQty, valPrice, valDisc, valTax, valNetAmount, sbHdrItemInfo)
            ElseIf PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Or PrintSOTransaction = PrintSOTransactionSet.Payment Then

                If PrintSOTransaction = PrintSOTransactionSet.Payment Then
                    strFilterCondition = ""
                Else
                    strFilterCondition = "PickUpQty > 0"
                End If

                If (Not _dtSalesItemDetails.Columns.Contains("IsStatus")) Then
                    _dtSalesItemDetails.Columns.Add(New DataColumn("IsStatus", Type.GetType("System.String")))
                End If

                For Each drGrid As DataRow In _dtSalesItemDetails.Select(strFilterCondition)
                    If Not (drGrid.RowState = DataRowState.Deleted) Then
                        If Not drGrid("IsStatus") Is DBNull.Value Then
                            If drGrid("IsStatus") = "Deleted" Then
                                Continue For
                            End If
                        End If

                        ValidateDataRow(drGrid("ArticleCode"), valArticleCode)
                        valArticleCode = valArticleCode.PadRight(26)
                        ValidateDataRow(drGrid("Discription"), valArticleDesc)
                        valArticleDesc = valArticleDesc.PadRight(33)

                        If PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                            valPurchasedQty = IIf(Not drGrid("PickUpQty") Is DBNull.Value, Convert.ToInt32(drGrid("PickUpQty")).ToString(), "0")
                            'ValidateDataRow(drGrid("PickUpQty"), valPurchasedQty)
                        Else
                            valPurchasedQty = IIf(Not drGrid("Quantity") Is DBNull.Value, Convert.ToInt32(drGrid("Quantity")).ToString(), "0")
                            'ValidateDataRow(drGrid("Quantity"), valPurchasedQty)
                        End If
                        valPurchasedQty = valPurchasedQty
                        valPurchasedQty = valPurchasedQty

                        'sbArticleInfo = SplitString(strArticleCode + valArticleDesc)
                        'comment to bring the desciptin in second line
                        sbArticleInfo = SplitString(valArticleCode & vbCrLf)
                        sbArticleInfo = sbArticleInfo.Append(SplitString(valArticleDesc & vbCrLf).ToString())

                        ValidateDataRow(drGrid("SellingPrice"), valPrice)

                        valPrice = PrintFormatCurrency(valPrice, DefaultCurrency, SORoundOff)
                        valPrice = valPrice

                        ValidateDataRow(drGrid("totaldiscpercentage"), valDisc)

                        'valDisc = PrintFormatCurrency(valDisc, DefaultCurrency, SORoundOff)
                        If valDisc <> String.Empty Then
                            valDisc = FormatNumber(valDisc, 2)
                            valDisc = valDisc & " %"
                        End If

                        'valDisc = valDisc.PadLeft(10)
                        'valDisc = valDisc.PadRight(10)

                        ValidateDataRow(drGrid("TotalTaxAmt"), valTax)
                        ValidateDataRow(drGrid("ExclTaxAmt"), decTax)

                        valTax = PrintFormatCurrency(valTax, DefaultCurrency, SORoundOff)
                        'valTax = valTax.PadLeft(5)
                        'valTax = valTax.PadRight(5)

                        valPickUp = IIf(Not drGrid("PickUpQty") Is DBNull.Value, Convert.ToInt32(drGrid("PickUpQty")).ToString(), "0")
                        'ValidateDataRow(drGrid("PickUpQty"), valPickUp)
                        'valPickUp = valPickUp.PadRight(5)

                        ValidateDataRow(drGrid("ReservedQty"), valReservedQty)
                        'valReservedQty = valReservedQty.PadLeft(4)
                        'valReservedQty = valReservedQty.PadRight(4)

                        If PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                            Dim decPrice As Decimal = 0
                            ValidateDataRow(drGrid("SellingPrice"), decPrice)
                            Dim iPickupQty As Integer = 0
                            ValidateDataRow(drGrid("PickUpQty"), iPickupQty)
                            valNetAmount = decPrice * iPickupQty
                            valNetAmount = Decimal.Add(valNetAmount, decTax)
                        Else
                            ValidateDataRow(drGrid("NetAmount"), valNetAmount)
                            valNetAmount = Decimal.Add(valNetAmount, decTax)
                        End If
                        valNetAmount = PrintFormatCurrency(valNetAmount, DefaultCurrency, SORoundOff)
                        'valNetAmount = valNetAmount.PadRight(15)

                        ValidateDataRow(drGrid("IsCLP"), valCLP)
                        'valCLP = valCLP.PadRight(4)

                        Dim itemDetails As String = String.Empty
                        
                        If PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                            itemDetails = valPurchasedQty
                            itemDetails += valPrice.PadLeft(15 - itemDetails.Length)
                            itemDetails += valNetAmount.PadLeft(30 - itemDetails.Length)
                            itemDetails += valPickUp.PadLeft(39 - itemDetails.Length)

                            sbHdrItemInfo.Append(sbArticleInfo.ToString() + SplitString(itemDetails, 40).ToString() + vbCrLf)
                            'sbHdrItemInfo.Append(sbArticleInfo.ToString() & valPurchasedQty.ToString() & valPrice & valNetAmount & valPickUp + vbCrLf)
                        ElseIf PrintSOTransaction = PrintSOTransactionSet.Payment Then

                            itemDetails = valPurchasedQty
                            itemDetails += valPrice.PadLeft(15 - itemDetails.Length)
                            itemDetails += valNetAmount.PadLeft(30 - itemDetails.Length)
                            itemDetails += valPickUp.PadLeft(39 - itemDetails.Length)

                            sbHdrItemInfo.Append(sbArticleInfo.ToString() + SplitString(itemDetails, 40).ToString() + vbCrLf)
                            'sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(valPurchasedQty.ToString() & valPrice & valNetAmount & valPickUp).ToString() + vbCrLf)
                        Else

                            itemDetails = valPurchasedQty
                            itemDetails += valPrice.PadLeft(15 - itemDetails.Length)
                            itemDetails += valNetAmount.PadLeft(26 - itemDetails.Length)
                            itemDetails += valReservedQty.PadLeft(32 - itemDetails.Length)
                            itemDetails += valPickUp.PadLeft(39 - itemDetails.Length)

                            sbHdrItemInfo.Append(sbArticleInfo.ToString() + SplitString(itemDetails, 40).ToString() + vbCrLf)
                            'sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(valPurchasedQty.ToString() & valPrice & valNetAmount & valReservedQty & valPickUp).ToString() + vbCrLf)
                        End If
                        itemCount += 1
                    End If
                Next
                If PrintSOTransaction = PrintSOTransactionSet.Payment Then
                    L40GetOtherCharges(valArticleCode, valArticleDesc, valPurchasedQty, valPrice, valDisc, valTax, valNetAmount, sbHdrItemInfo)
                End If
            ElseIf PrintSOTransaction = PrintSOTransactionSet.GiftVoucherDocumentPrint Then
                For Each drGrid As DataRow In _dtSalesItemDetails.Rows
                    If Not (drGrid.RowState = DataRowState.Deleted) Then
                        If Not drGrid("IsStatus") Is DBNull.Value Then
                            If drGrid("IsStatus") = "Deleted" Then
                                Continue For
                            End If
                        End If

                        ValidateDataRow(drGrid("ArticleCode"), valArticleCode)
                        valArticleCode = valArticleCode.PadRight(26)

                        ValidateDataRow(drGrid("Discription"), valArticleDesc)
                        valArticleDesc = valArticleDesc.PadRight(33)

                        ValidateDataRow(drGrid("Quantity"), valPurchasedQty)
                        valPurchasedQty = valPurchasedQty.ToString()

                        valPurchasedQty = valPurchasedQty.PadRight(8)

                        'sbArticleInfo = SplitString(strArticleCode + valArticleDesc)
                        'comment to bring the desciptin in second line
                        sbArticleInfo = SplitString(valArticleCode & vbCrLf)
                        sbArticleInfo = sbArticleInfo.Append(SplitString(valArticleDesc & vbCrLf).ToString())


                        ValidateDataRow(drGrid("PickUpQty"), valPickUp)


                        valPickUp = valPickUp.PadLeft(10)
                        valPickUp = valPickUp.PadRight(10)

                        ValidateDataRow(drGrid("ReservedQty"), valReservedQty)
                        valReservedQty = valReservedQty.PadLeft(8)
                        valReservedQty = valReservedQty.PadRight(8)
                        sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(valPurchasedQty.ToString() & valPickUp & valReservedQty).ToString() + vbCrLf)
                        itemCount += 1
                    End If
                Next
            ElseIf PrintSOTransaction = PrintSOTransactionSet.SOReturnStatus Then

                If PrintSOTransaction = PrintSOTransactionSet.Payment Then
                    strFilterCondition = ""
                Else
                    strFilterCondition = "PickUpQty <> 0"
                End If
                For Each drGrid As DataRow In _dtSalesItemDetails.Select(strFilterCondition)
                    If Not (drGrid.RowState = DataRowState.Deleted) Then
                        If Not drGrid("IsStatus") Is DBNull.Value Then
                            If drGrid("IsStatus") = "Deleted" Then
                                Continue For
                            End If
                        End If

                        ValidateDataRow(drGrid("ArticleCode"), valArticleCode)

                        valArticleCode = valArticleCode.PadRight(26)
                        ValidateDataRow(drGrid("Discription"), valArticleDesc)
                        valArticleDesc = valArticleDesc.PadRight(33)

                        valPurchasedQty = valPurchasedQty.ToString()
                        'valPurchasedQty = valPurchasedQty.PadLeft(4)
                        ' valPurchasedQty = valPurchasedQty.PadRight(8)


                        sbArticleInfo = SplitString(valArticleCode & vbCrLf)
                        sbArticleInfo = sbArticleInfo.Append(SplitString(valArticleDesc & vbCrLf).ToString())

                        ValidateDataRow(drGrid("SellingPrice"), valPrice)

                        valPrice = PrintFormatCurrency(valPrice, DefaultCurrency, SORoundOff)
                        'valPrice = valPrice.PadLeft(9)
                        'valPrice = valPrice.PadRight(10)


                        ValidateDataRow(drGrid("TotalTaxAmt"), valTax)


                        valTax = PrintFormatCurrency(valTax, DefaultCurrency, SORoundOff)
                        'valTax = valTax.PadRight(10)

                        ValidateDataRow(drGrid("PickUpQty"), valPickUp)
                        valPickUp = valPickUp.ToString()
                        'valPickUp = valPickUp.PadRight(8)

                        Dim decPrice As Decimal = 0
                        ValidateDataRow(drGrid("SellingPrice"), decPrice)
                        Dim iPickupQty As Integer = 0
                        ValidateDataRow(drGrid("PickUpQty"), iPickupQty)
                        valNetAmount = decPrice * iPickupQty


                        valNetAmount = PrintFormatCurrency(valNetAmount, DefaultCurrency, SORoundOff)
                        'valNetAmount = valNetAmount.PadLeft(10)
                        'valNetAmount = valNetAmount.PadRight(10)

                        Dim itemDetails As String = String.Empty

                        itemDetails = valPickUp
                        itemDetails += valPrice.PadLeft(15 - itemDetails.Length)
                        itemDetails += valTax.PadLeft(26 - itemDetails.Length)
                        itemDetails += valNetAmount.PadLeft(39 - itemDetails.Length)

                        'valPickUp & valPrice & valTax & valNetAmount
                        sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(itemDetails, 40).ToString() + vbCrLf)

                        itemCount += 1
                    End If
                Next
            End If
            'Sales Item 
            strItemSales = sbHdrItemInfo.ToString()
            Return True
        Catch ex As Exception

        End Try
    End Function

    Private Function L40GetOtherCharges(ByVal strArticleCode As String, ByVal strArticleDesc As String, ByVal strPurchasedQty As String, ByVal iPrice As String, ByVal strDisc As String, ByVal strTax As String, ByVal iNetAmount As String, ByRef sbHdrItemInfo As StringBuilder) As Boolean
        Try
            If Not _dsOtherCost Is Nothing AndAlso _dsOtherCost.Tables.Count > 0 Then
                Dim strFilter As String = ""
                For Each dt As DataTable In _dsOtherCost.Tables

                    If PrintSOTransaction = PrintSOTransactionSet.Status Or PrintSOTransaction = PrintSOTransactionSet.Payment Then
                        If PrintSOTransaction = PrintSOTransactionSet.Payment Then
                            If _dsOtherCost.Tables("NewOtherCharges").Columns.Contains("Status") Then
                                strFilter = "Status is null"
                            End If

                        End If
                        For Each drGrid As DataRow In _dsOtherCost.Tables("NewOtherCharges").Select(strFilter)
                            Dim sbArticleInfo As New StringBuilder
                            If Not (drGrid.RowState = DataRowState.Deleted) Then

                                'strArticleCode = "Add.Cost"
                                strArticleCode = getValueByKey("CLSPSO030")
                                'strArticleCode = strArticleCode.PadRight(WArticleCode)
                                If Not drGrid("ChargeName") Is Nothing AndAlso Not drGrid("ChargeName") Is DBNull.Value Then
                                    strArticleDesc = drGrid("ChargeName")
                                End If
                                strArticleDesc = SplitString(strArticleDesc, 39).ToString()

                                strPurchasedQty = "1"
                                'strPurchasedQty = strPurchasedQty.ToString()
                                'strPurchasedQty = strPurchasedQty.PadLeft(8)
                                strPurchasedQty = strPurchasedQty

                                If Not drGrid("ChargeAmount") Is Nothing AndAlso Not drGrid("ChargeAmount") Is DBNull.Value Then
                                    iPrice = drGrid("ChargeAmount")
                                End If


                                iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, SORoundOff)
                                'iPrice = iPrice.PadLeft(10)
                                'iPrice = iPrice.PadRight(10 + 2)

                                strDisc = Decimal.Zero
                                strDisc = PrintFormatCurrency(strDisc, DefaultCurrency, SORoundOff)
                                'strDisc = strDisc.PadLeft(10)
                                'strDisc = strDisc.PadRight(10)

                                If Not drGrid("TaxAmt") Is Nothing AndAlso Not drGrid("TaxAmt") Is DBNull.Value Then
                                    strTax = drGrid("TaxAmt")
                                Else
                                    strTax = Decimal.Zero
                                End If


                                strTax = PrintFormatCurrency(strTax, DefaultCurrency, SORoundOff)
                                'strTax = strTax.PadLeft(5)
                                'strTax = strTax.PadRight(5)

                                If Not drGrid("ChargeAmount") Is Nothing AndAlso Not drGrid("ChargeAmount") Is DBNull.Value Then
                                    iNetAmount = drGrid("ChargeAmount")
                                End If

                                iNetAmount = PrintFormatCurrency(iNetAmount, DefaultCurrency, SORoundOff)
                                'iNetAmount = iNetAmount.PadLeft(12)
                                'iNetAmount = iNetAmount.PadRight(12)

                                sbHdrItemInfo.Append(strArticleCode + vbCrLf + strArticleDesc.Trim() + vbCrLf)

                                Dim itemDetails As String = String.Empty

                                itemDetails = strPurchasedQty
                                itemDetails += iPrice.PadLeft(15 - itemDetails.Length)
                                itemDetails += iNetAmount.PadLeft(30 - itemDetails.Length)

                                sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(itemDetails, 40).ToString() + vbCrLf)
                                'sbHdrItemInfo.Append(sbArticleInfo.ToString() & strPurchasedQty.ToString() & iPrice & iNetAmount + vbCrLf)
                            End If
                        Next
                    End If
                Next

            End If
        Catch ex As Exception

        End Try
    End Function

    'Private Function A4PrintSalesOrdersReturn() As Boolean
    '    Try
    '        CheckDocumentType()

    '        Dim PrintSo As New System.Text.StringBuilder
    '        Dim strHeader As New StringBuilder
    '        Dim strFooter As New StringBuilder
    '        Dim strWelcomeMsg As New StringBuilder
    '        Dim strTaxInformation As New StringBuilder
    '        Dim strPromotionMsg As New StringBuilder
    '        Dim strTitle As String
    '        PrinttHeaderAndFooter(strHeader, strFooter, strWelcomeMsg, strPromotionMsg, strTaxInformation, strSODocumentType, PrintSOPageSetup)

    '        PrintSo.Length = 0
    '        PrintSo.Append(strLineA4 & vbCrLf)
    '        PrintSo.Append("                          SALES ORDER RETURN                                " & vbCrLf)
    '        PrintSo.Append(strLineA4 & vbCrLf)
    '        '   PrintSo.Append("Company Name   : " & "CreativeIT India Ltd" & "     Company Code : " & "CRITI02" & vbCrLf)
    '        strTitle = A4GetTitle()


    '        Dim strCashierDetails As String = A4CashierDetails(CreationDate, InvoiceNumber, CashierId)

    '        'CustomerDetails
    '        Dim strCustomerDetails As String
    '        strCustomerDetails = A4GetCustomerDetails(_dtCustomerDetails)
    '        'Customer Delivery Details
    '        Dim strCustomerDeliveryDetails As String
    '        strCustomerDeliveryDetails = A4GetCustomerDeliveryDetails(_dtCustomerDetails)

    '        Dim sbInvoiceInfo As New StringBuilder
    '        If PrintSOTransaction = PrintSOTransactionSet.Status Then
    '            sbInvoiceInfo.Append("Invoice to " + vbCrLf)
    '            Dim strInvoiceNumber As String = String.Format("Customer reference : {0}", CustomerReference)
    '            sbInvoiceInfo.Append(strInvoiceNumber + vbCrLf)
    '        End If

    '        'Remark 
    '        Dim sbRemark As New StringBuilder
    '        Dim strRemark As String = "Remark"
    '        sbRemark.Append(sbInvoiceInfo.ToString())
    '        sbRemark.Append(strRemark + vbCrLf)





    '        Dim strPaymentInfo As String = ""
    '        Dim strTermsNConditions As String = ""

    '        A4GetPaymentInfo(strPaymentInfo, 62)


    '        Dim sbFinalPrint As New StringBuilder

    '        Dim objclsA4Print As New clsA4Print
    '        PrintSo.Append(strHeader.ToString() & vbCrLf)
    '        PrintSo.Append(strTitle.ToString() & vbCrLf)
    '        PrintSo.Append(strCashierDetails & vbCrLf)
    '        PrintSo.Append(strCustomerDetails & vbCrLf)
    '        PrintSo.Append(strCustomerDeliveryDetails & vbCrLf)
    '        PrintSo.Append(sbRemark.ToString() & vbCrLf)



    '        PrintSo.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)
    '        PrintSo.Append("Item Code       Item Desc                               Order Qty       ReturnQty       Price      Disc%  Tax%   NetAmt" & vbCrLf)
    '        PrintSo.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)

    '        For Each drDtl As DataRow In SalesItemDetails.Select("PickUpQty>0 and IsStatus='Inserted'")
    '            PrintSo.Append(drDtl("ArticleCode") & Space(16 - drDtl("ArticleCode").ToString.Length) & _
    '                          drDtl("Discription") & Space(40 - drDtl("Discription").ToString.Length) & _
    '                          drDtl("Quantity") & Space(10 - drDtl("Quantity").ToString.Length) & _
    '                          drDtl("PickUpQty") & Space(10 - drDtl("PickUpQty").ToString.Length) & _
    '                          Format(drDtl("SellingPrice"), "0.0") & Space(10 - drDtl("SellingPrice").ToString.Length) & _
    '                          drDtl("Discount") & Space(10 - drDtl("Discount").ToString.Length) & _
    '                          drDtl("ExclTaxAmt") & Space(10 - drDtl("ExclTaxAmt").ToString.Length) & Format(drDtl("NetAmount"), "0.0") & vbCrLf)
    '        Next

    '        PrintSo.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)


    '        PrintSo.Append(strPaymentInfo & vbCrLf)
    '        PrintSo.Append(strWelcomeMsg.ToString() & vbCrLf)
    '        PrintSo.Append(strTaxInformation.ToString() & vbCrLf)
    '        PrintSo.Append(strPromotionMsg.ToString & vbCrLf)


    '        PrinterName = SetPrinterName(dtPrinterInfo1, "SalesOrder", PrintSOTransaction.ToString)

    '        If _printPreview = True Then
    '            fnPrint(PrintSo.ToString, "PRV")
    '        Else
    '            fnPrint(PrintSo.ToString, "PRN")
    '        End If

    '        Return True

    '    Catch ex As Exception
    '        MsgBox(ex.Message, getValueByKey("CLAE05"))
    '    End Try
    'End Function
#End Region
End Class
