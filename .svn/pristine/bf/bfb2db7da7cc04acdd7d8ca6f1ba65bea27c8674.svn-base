Imports System.Text
Imports SpectrumBL

Public Class clsPrintSalesOrder
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
    Private _PrintingTaxInfoAllowed As Boolean
    Private _CustomerReference As String
    Private _strInvoiceNumber As String
    Private _strSOCurrentInvoiceNumber As String
    Private _TotalTaxAmt As String
    Private DisplayArticleFullName As Boolean

    Dim ObjclsCommon As New clsCommon


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

    Public Property PrintingTaxInfoAllowed() As Boolean
        Get
            Return _PrintingTaxInfoAllowed
        End Get
        Set(ByVal value As Boolean)
            _PrintingTaxInfoAllowed = value
        End Set
    End Property

    Private _IsQuotation As Boolean
    Public Property IsQuotation() As Boolean
        Get
            Return _IsQuotation
        End Get
        Set(ByVal value As Boolean)
            _IsQuotation = value
        End Set
    End Property

    Public Property CustomerReference() As String
        Get
            Return _CustomerReference
        End Get
        Set(ByVal value As String)
            _CustomerReference = value
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

    Private _IsBillRoundOffRequired As Boolean
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


    Private _SoBulkComboHdr As DataTable
    Public Property DtSoBulkComboHdr() As DataTable
        Get
            Return _SoBulkComboHdr
        End Get
        Set(ByVal value As DataTable)
            _SoBulkComboHdr = value
        End Set
    End Property

    Private _SoBulkComboDtl As DataTable
    Public Property DtSoBulkComboDtl() As DataTable
        Get
            Return _SoBulkComboDtl
        End Get
        Set(ByVal value As DataTable)
            _SoBulkComboDtl = value
        End Set
    End Property

    Private _CreditSale As Double
    Public Property CreditSale As Double
        Get
            Return _CreditSale
        End Get
        Set(value As Double)
            _CreditSale = value
        End Set
    End Property

    Private _InvoiceTo As String
    Public Property InvoiceTo() As String
        Get
            Return _InvoiceTo
        End Get
        Set(ByVal value As String)
            _InvoiceTo = value
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
    '        dt = GetDefaultSetting(SiteCode, "SalesOrder")
    '        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
    '            For Each dr As DataRow In dt.Rows
    '                'If dr("FLDLABEL").ToString() = "PrintingTaxInfoAllowed" Then
    '                '    PrintingTaxInfoAllowed = dr("FLDVALUE")
    '                '    'ElseIf dr("FLDLABEL").ToString().ToUpper = "SOIsPromotionMsgPrint".ToUpper() Then
    '                '    '    IsPromotionMessagePrint = dr("FLDVALUE").ToString
    '                '    'ElseIf dr("FLDLABEL").ToString().ToUpper = "SOIsTaxInformationMsgPrint".ToUpper() Then
    '                '    '    IsTaxInformation = dr("FLDVALUE").ToString
    '                'ElseIf dr("FLDLABEL").ToString().ToUpper = "SORoundOffRequired".ToUpper() Then
    '                '    IsBillRoundOffRequired = dr("FLDVALUE")
    '                If dr("FLDLABEL").ToString().ToUpper = "PrintPreviewAllowed".ToUpper() AndAlso IsQuotation = False Then
    '                    _printPreview = dr("FLDVALUE")
    '                End If
    '            Next
    '        End If

    '        'dt = GetDefaultSetting(SiteCode, "0000")
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

    Private _soCreationON As DateTime
    Public Property SOCreationON() As DateTime
        Get
            Return _soCreationON
        End Get
        Set(ByVal value As DateTime)
            _soCreationON = value
        End Set
    End Property
    Private _soDeliveredON As DateTime
    Public Property SODeliveredON() As DateTime
        Get
            Return _soDeliveredON
        End Get
        Set(ByVal value As DateTime)
            _soDeliveredON = value
        End Set
    End Property

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

    Private _isQuotationPrint As Boolean
    Public Sub New()

    End Sub
    Private _salesPersonName As String
    Public Property SalesPerson() As String
        Get
            Return _salesPersonName
        End Get
        Set(ByVal value As String)
            _salesPersonName = value
        End Set
    End Property
    Private _paymentDate As String
    Public Property PaymentDate() As String
        Get
            Return _paymentDate
        End Get
        Set(ByVal value As String)
            _paymentDate = value
        End Set
    End Property
    Public Sub New(ByVal IsPrintPreview As Boolean, ByVal RoundOffRequired As Boolean, ByVal enumTransactionDetails As PrintSOTransactionSet, ByVal strSiteCode As String, ByVal strDefaultCurrency As String, ByVal strCashierID As String, ByVal strSalesOrderNo As String, ByVal dtCustomerDetail As DataTable, ByVal dtSalesItem As DataTable, ByVal dtPayment As DataTable, Optional ByVal strCustomerReference As String = "", Optional ByVal strInvoiceNumber As String = "", Optional ByVal strGiftReceiptMessage As String = "", Optional ByVal iSoRoundOff As Integer = 2, Optional ByRef strErrorMsg As String = "", Optional ByVal soPrinterInfo As DataTable = Nothing, Optional ByVal strStatus As String = "", Optional ByVal dsOtherCost As DataSet = Nothing, Optional ByVal strReturnReason As String = "", Optional ByVal dtSalesOrderTaxdtls As DataTable = Nothing, Optional ByVal isQuotationPrint As Boolean = False, Optional ByVal ShowFullName As Boolean = False _
                                                         , Optional ByRef dtSOBulkComboHDRPrint As DataTable = Nothing, Optional ByRef dtSOBulkComboDtlPrint As DataTable = Nothing, Optional SalesOrderCreationDate As DateTime = Nothing, Optional SalesOrderDeliveryDate As DateTime = Nothing, Optional ByVal strSalesPerson As String = "", Optional ByVal SalesPaymentDate As DateTime = Nothing, Optional ByVal CreditSale As Double = 0, Optional ByVal strInvoiceTo As String = "")

        Try
            If SalesOrderCreationDate = Nothing Then
                SalesOrderCreationDate = DateTime.Now
            End If
            SOCreationON = SalesOrderCreationDate
            If SalesOrderDeliveryDate = Nothing Then
                SalesOrderDeliveryDate = DateTime.Now
            End If
            SODeliveredON = SalesOrderDeliveryDate
            If SalesPaymentDate = Nothing Then
                SalesPaymentDate = DateTime.Now
            End If
            PaymentDate = SalesPaymentDate
            IsQuotation = isQuotationPrint
            _printPreview = IsPrintPreview
            ItemReturnReason = strReturnReason
            PrintSOTransaction = enumTransactionDetails
            SalesOrderNo = strSalesOrderNo
            CustomerDetails = dtCustomerDetail
            PaymentDetails = dtPayment
            CashierId = strCashierID
            SalesPerson = strSalesPerson
            SalesItemDetails = dtSalesItem
            SiteCode = strSiteCode
            _dsOtherCost = dsOtherCost
            'IsFooterPrinting = True
            'IsHeaderPrinting = True
            DefaultCurrency = strDefaultCurrency
            CustomerReference = strCustomerReference
            InvoiceNumber = strInvoiceNumber
            GiftReceiptMessage = strGiftReceiptMessage
            CreationDate = GetCurrentDate()
            SORoundOff = iSoRoundOff
            dtPrinterInfo1 = soPrinterInfo
            _SOStatus = strStatus
            DataTableSalesOrderTaxDtls = dtSalesOrderTaxdtls
            _isQuotationPrint = isQuotationPrint
            _IsBillRoundOffRequired = RoundOffRequired
            DisplayArticleFullName = ShowFullName
            DtSoBulkComboHdr = dtSOBulkComboHDRPrint
            DtSoBulkComboDtl = dtSOBulkComboDtlPrint
            '--showing credit sale in sales invoice
            _CreditSale = CreditSale
            InvoiceTo = strInvoiceTo
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

            If (DisplayArticleFullName) Then
                Dim articleCodeList As String = String.Empty
                Dim articleCodeLst As New List(Of String)
                For rowIndex = 0 To _dtSalesItemDetails.Rows.Count - 1
                    If Not (articleCodeLst.Contains(_dtSalesItemDetails.Rows(rowIndex)("ArticleCode").ToString())) Then
                        articleCodeList += String.Format("'{0}',", _dtSalesItemDetails.Rows(rowIndex)("ArticleCode").ToString())
                        articleCodeLst.Add(String.Format("'{0}',", _dtSalesItemDetails.Rows(rowIndex)("ArticleCode").ToString()))
                    End If
                Next
                articleCodeLst.Clear()
                If (Not String.IsNullOrEmpty(articleCodeList)) Then
                    articleCodeList = articleCodeList.Substring(0, articleCodeList.Length - 1)

                    Dim dtArticles = clsCommon.GetArticleDetails(articleCodeList)

                    For rowIndex = 0 To _dtSalesItemDetails.Rows.Count - 1
                        Dim drArticle = dtArticles.Select(String.Format("ArticleCode='{0}'", _dtSalesItemDetails.Rows(rowIndex)("ArticleCode"))).FirstOrDefault()

                        _dtSalesItemDetails.Rows(rowIndex)("ArticleDiscription") = drArticle("ArticleDiscription")
                    Next
                End If
            End If

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
                sbTitle.Append(strLineA4 + vbCrLf)
                Dim strTitleInfo As String
                'strTitleInfo = String.Format("SALES ORDER  {0} STATUS:{1}", SalesOrderNo, SOStatus)
                strTitleInfo = String.Format(getValueByKey("CLSPSO001"), SalesOrderNo, SOStatus)
                strTitleInfo.PadRight(22)
                'strTitleInfo = String.Format("{0} DATE {1}", strTitleInfo, CreationDate)
                ' strTitleInfo = String.Format(getValueByKey("CLSPSO002"), strTitleInfo, CreationDate)
                strTitleInfo = String.Format(getValueByKey("CLSPSO002"), strTitleInfo, PaymentDate)

                If Not InvoiceNumber = Nothing Then
                    Dim strPaymentHdr As String = String.Format("             " & getValueByKey("CLSPSO006"), InvoiceNumber)
                    _strSOCurrentInvoiceNumber = InvoiceNumber
                    sbTitle.Append(strPaymentHdr + vbCrLf)
                End If
                sbTitle.Append(strTitleInfo + vbCrLf)
                sbTitle.Append(strLineA4 + vbCrLf)
                Return sbTitle.ToString()
            ElseIf PrintSOTransaction = PrintSOTransactionSet.SOReturnStatus Then
                Dim sbTitle As New StringBuilder
                sbTitle.Append(strLineA4 + vbCrLf)
                Dim strTitleInfo As String
                'strTitleInfo = String.Format("SALES ORDER  {0} STATUS:{1} (Return)", SalesOrderNo, SOStatus)
                strTitleInfo = String.Format(getValueByKey("CLSPSO003"), SalesOrderNo, SOStatus)
                strTitleInfo.PadRight(22)
                'strTitleInfo = String.Format("{0} DATE {1}", strTitleInfo, CreationDate)
                strTitleInfo = String.Format(getValueByKey("CLSPSO002"), strTitleInfo, CreationDate)
                sbTitle.Append(strTitleInfo + vbCrLf)
                sbTitle.Append(strLineA4 + vbCrLf)
                Return sbTitle.ToString()

            ElseIf PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                Dim sbTitle As New StringBuilder
                sbTitle.Append(strLineA4 + vbCrLf)
                Dim strTitleInfo As String
                'strTitleInfo = String.Format("SALES ORDER  {0} DELIVERY NOTE ", SalesOrderNo)
                strTitleInfo = String.Format(getValueByKey("CLSPSO004"), SalesOrderNo)
                strTitleInfo.PadRight(22)
                'strTitleInfo = String.Format("{0} DATE {1}", strTitleInfo, CreationDate)
                'strTitleInfo = String.Format(getValueByKey("CLSPSO002"), strTitleInfo, CreationDate)
                strTitleInfo = String.Format(getValueByKey("CLSPSO002"), strTitleInfo, SOCreationON)
                sbTitle.Append(strTitleInfo + vbCrLf)
                sbTitle.Append(strLineA4 + vbCrLf)
                Return sbTitle.ToString()
            ElseIf PrintSOTransaction = PrintSOTransactionSet.Payment Then
                Dim sbTitle As New StringBuilder
                sbTitle.Append(strLineA4 + vbCrLf)
                Dim strTitleInfo As String
                'strTitleInfo = String.Format("SALES ORDER  {0} PAYMENT ", SalesOrderNo)
                'strTitleInfo = String.Format(getValueByKey("CLSPSO005") & " ", SalesOrderNo)
                strTitleInfo = String.Format(getValueByKey("CLSPSO001"), SalesOrderNo, SOStatus)
                strTitleInfo.PadRight(22)
                'strTitleInfo = String.Format("{0} DATE {1}", strTitleInfo, CreationDate)
                'strTitleInfo = String.Format(getValueByKey("CLSPSO002"), strTitleInfo, CreationDate)
                strTitleInfo = String.Format(getValueByKey("CLSPSO002"), strTitleInfo, PaymentDate)
                'Dim strPaymentHdr As String = String.Format("             CASH MEMO Nr {0}", InvoiceNumber)
                Dim strPaymentHdr As String = String.Format("             " & getValueByKey("CLSPSO006"), InvoiceNumber)
                _strSOCurrentInvoiceNumber = InvoiceNumber
                sbTitle.Append(strPaymentHdr + vbCrLf)
                sbTitle.Append(strTitleInfo + vbCrLf)
                sbTitle.Append(strLineA4 + vbCrLf)
                Return sbTitle.ToString()
            ElseIf PrintSOTransaction = PrintSOTransactionSet.GiftVoucherDocumentPrint Then
                Dim sbTitle As New StringBuilder
                sbTitle.Append(strLineA4 + vbCrLf)
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
                sbTitle.Append(strLineA4 + vbCrLf)
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
            Dim strFooter As New StringBuilder
            Dim strWelcomeMsg As New StringBuilder
            Dim strTaxInformation As New StringBuilder
            Dim strPromotionMsg As New StringBuilder
            PrinttHeaderAndFooter(strHeader, strFooter, strWelcomeMsg, strPromotionMsg, strTaxInformation, strSODocumentType, PrintSOPageSetup)



            'Title 

            Dim sbTitle As New StringBuilder
            sbTitle.Append(A4GetTitle)


            'Dim strCashierDetails As String = A4CashierDetails(CreationDate, InvoiceNumber, CashierId)
            Dim strCashierDetails As String = A4CashierDetails(SOCreationON, InvoiceNumber, CashierId)
            If SalesPerson <> "" Then
                strCashierDetails &= vbCrLf & String.Format(getValueByKey("Crs023")) & "  : " & SalesPerson
            End If
            'CustomerDetails
            Dim strCustomerDetails As String
            strCustomerDetails = A4GetCustomerDetails(_dtCustomerDetails)
            'Customer Delivery Details
            Dim strCustomerDeliveryDetails As String
            strCustomerDeliveryDetails = String.Format(getValueByKey("CLSCMP022")) & " " & SODeliveredON & vbCrLf  'CLSCMP022
            strCustomerDeliveryDetails &= A4GetCustomerDeliveryDetails(_dtCustomerDetails)

            Dim sbInvoiceInfo As New StringBuilder
            '----Added for invoiceto field
            Dim strInvoiceTo As String = String.Format(getValueByKey("CLSPSO008") & " : ")
            sbInvoiceInfo.Append(strInvoiceTo + InvoiceTo.ToString() + vbCrLf)
            If PrintSOTransaction = PrintSOTransactionSet.Status Then
                'sbInvoiceInfo.Append("Invoice to " + vbCrLf)
                '---removed for invoiceto field
                'sbInvoiceInfo.Append(getValueByKey("CLSPSO008") & " " + vbCrLf)                     
                'sbInvoiceInfo.Append(strInvoiceTo + vbCrLf)
                'Dim strInvoiceNumber As String = String.Format("Customer reference : {0}", CustomerReference)
                Dim strInvoiceNumber As String = String.Format(getValueByKey("CLSPSO009"), CustomerReference)
                sbInvoiceInfo.Append(strInvoiceNumber + vbCrLf)
            End If

            'Remark 
            Dim sbRemark As New StringBuilder
            'Dim strRemark As String = "Remark"
            Dim strRemark As String = getValueByKey("CLSPSO010") & " : "
            sbRemark.Append(sbInvoiceInfo.ToString())
            'If PrintSOTransaction = PrintSOTransactionSet.SOReturnStatus Then

            sbRemark.Append(strRemark + ItemReturnReason.ToString() + vbCrLf)


            'Else
            'sbRemark.Append(strRemark + vbCrLf)
            'End If



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

            Dim objclsA4Print As New clsA4Print
            objclsA4Print.A4Header = strHeader.ToString()
            objclsA4Print.A4Title = sbTitle.ToString()
            objclsA4Print.A4CashierDetails = strCashierDetails
            objclsA4Print.A4CustomerDetails = strCustomerDetails
            objclsA4Print.A4DeliveryAddress = strCustomerDeliveryDetails
            objclsA4Print.A4Remark = sbRemark.ToString()
            objclsA4Print.A4ItemHeader = strItemHeader
            objclsA4Print.A4ItemDetails = strItemSales
            objclsA4Print.A4PaymentDetails = strPaymentInfo
            objclsA4Print.A4WelcomeMessage = strWelcomeMsg.ToString()
            objclsA4Print.A4TaxInformation = strTaxInformation.ToString()
            If PrintSOTransaction = PrintSOTransactionSet.Payment Then
                objclsA4Print.A4TaxDetailsInvoice = strTaxDetailsAgainstInvoice
            End If

            objclsA4Print.A4PromotionInformation = strPromotionMsg.ToString()
            If Not PrintSOTransaction = PrintSOTransactionSet.Payment Then
                objclsA4Print.A4Footer = strFooter.ToString()
            End If

            If PrintSOTransaction = PrintSOTransactionSet.Status Or Not PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                strTermsNConditions = A4GetStringTermsCondition(strDocTypeforTermsnConditions, SiteCode, PrintSOPageSetup)
                objclsA4Print.A4TermsNConditions = strTermsNConditions
            End If


            objclsA4Print.GiftReceiptMessage = GiftReceiptMessage

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
            Dim strDiscount As String

            If PrintSOTransaction = PrintSOTransactionSet.Status Or PrintSOTransaction = PrintSOTransactionSet.Payment Then

                'Dim strTotal2Pay As String = "TOTAL TO PAY "
                Dim strTotal2Pay As String = getValueByKey("CLSPSO011") & " "
                Dim objTotal2Pay As Object
                If _dtSalesItemDetails.Columns.Contains("IsStatus") Then
                    objTotal2Pay = _dtSalesItemDetails.Compute("sum(NetAmount)", "IsStatus <> 'Deleted'")
                Else
                    objTotal2Pay = _dtSalesItemDetails.Compute("sum(NetAmount)", "")
                End If

                _TotalTaxAmt = getValueByKey("CLSPSO037")
                _TotalTaxAmt = _TotalTaxAmt.PadRight(iGap)
                Dim tax = ""
                If _dtSalesItemDetails.Columns.Contains("IsStatus") AndAlso _dtSalesItemDetails.Columns.Contains("TotalTaxAmt") Then
                    tax = If(_dtSalesItemDetails.Compute("sum(TotalTaxAmt)", "IsStatus <> 'Deleted'") Is DBNull.Value, 0.0, _dtSalesItemDetails.Compute("sum(TotalTaxAmt)", "IsStatus <> 'Deleted'"))
                End If
                tax = PrintFormatCurrency(tax, DefaultCurrency, SORoundOff)
                PadingString(tax)
                _TotalTaxAmt = _TotalTaxAmt & tax
                Dim decTotal2Pay As Decimal
                If Not objTotal2Pay Is DBNull.Value AndAlso Not objTotal2Pay Is Nothing Then
                    decTotal2Pay = CDbl(objTotal2Pay)
                End If
                'Other Charges adding into NetAmount 
                If Not _dsOtherCost Is Nothing AndAlso _dsOtherCost.Tables.Contains("NewOtherCharges") AndAlso _dsOtherCost.Tables("NewOtherCharges").Rows.Count > 0 Then
                    'decTotal2Pay = decTotal2Pay + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(ChargeAmount)", " ") + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " ")
                    Try
                        decTotal2Pay = decTotal2Pay + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(ChargeAmount)", " ") + IIf(Not _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " ") Is DBNull.Value, _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", ""), 0)
                    Catch ex As Exception

                    End Try
                End If
                If Not _dtSalesItemDetails.Columns.Contains("TotalPickupAmt") Then
                    _dtSalesItemDetails.Columns.Add("TotalPickupAmt", System.Type.GetType("System.Decimal"))
                End If

                _dtSalesItemDetails.Columns("TotalPickupAmt").Expression = "(NetAmount/IIF(quantity=0,1,quantity))* (PickUpQty + DeliveredQty)"

                Dim objTotalPickAmt As Object
                If _dtSalesItemDetails.Columns.Contains("IsStatus") Then
                    objTotalPickAmt = _dtSalesItemDetails.Compute("sum(TotalPickupAmt)", "PickUpQty > 0 or  DeliveredQty>0 and IsStatus <> 'Deleted'")
                Else
                    objTotalPickAmt = _dtSalesItemDetails.Compute("sum(TotalPickupAmt)", "PickUpQty > 0 or  DeliveredQty>0 ")
                End If
                Dim decTotalPickAmt As Decimal
                If Not objTotalPickAmt Is DBNull.Value AndAlso Not objTotalPickAmt Is Nothing Then
                    decTotalPickAmt = CDbl(objTotalPickAmt)
                End If

                _dtSalesItemDetails.Columns("TotalPickupAmt").Expression = Nothing
                _dtSalesItemDetails.Columns("TotalPickupAmt").ReadOnly = False
                decTotal2Pay = MyRound(decTotal2Pay, SORoundOff, _IsBillRoundOffRequired)
                strTotal2Pay = strTotal2Pay.PadRight(iGap)
                Dim strvalueTotal2Pay As String = PrintFormatCurrency(decTotal2Pay, DefaultCurrency, SORoundOff)
                PadingString(strvalueTotal2Pay)
                'CStr(decTotal2Pay) + DefaultCurrency.PadLeft(2)
                'sbPaymentInfo.Append(strTotal2Pay)
                'sbPaymentInfo.Append(strvalueTotal2Pay + vbCrLf)
                'sbPaymentInfo.Append(strLineA4 + vbCrLf)

                'Dim strTotalGross As String = "GROSS TOTAL"
                Dim strTotalGross As String = getValueByKey("CLSPSO012")
                Dim objTotalGross As Object
                If _dtSalesItemDetails.Columns.Contains("IsStatus") Then
                    objTotalGross = _dtSalesItemDetails.Compute("sum(GrossAmt)", "IsStatus <> 'Deleted'")
                Else
                    objTotalGross = _dtSalesItemDetails.Compute("sum(GrossAmt)", "")
                End If
                Dim decTotalGross As Decimal

                If Not objTotalGross Is Nothing AndAlso Not objTotalGross Is DBNull.Value Then
                    decTotalGross = CDbl(objTotalGross)
                End If
                If Not _dsOtherCost Is Nothing AndAlso _dsOtherCost.Tables.Contains("NewOtherCharges") AndAlso _dsOtherCost.Tables("NewOtherCharges").Rows.Count > 0 Then
                    'decTotalGross = decTotalGross + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(ChargeAmount)", " ") + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " ")
                    Try
                        decTotalGross = decTotalGross + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(ChargeAmount)", " ") + IIf(Not _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " ") Is DBNull.Value, _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " "), 0)
                    Catch ex As Exception

                    End Try

                End If
                decTotalGross = MyRound(decTotalGross, SORoundOff, _IsBillRoundOffRequired)
                strTotalGross = strTotalGross.PadRight(iGap)
                Dim strValueTotalGross As String = PrintFormatCurrency(decTotalGross, DefaultCurrency, SORoundOff)
                PadingString(strValueTotalGross)
                strTotalGross = strTotalGross & strValueTotalGross ' CStr(decTotalDiscount) + DefaultCurrency.PadLeft(2)
                sbPaymentInfo.Append(strTotalGross + vbCrLf)

                'Dim strDiscount As String = "DISCOUNT"
                Dim ObjclsCommon As New clsCommon
                Dim offerno As String = ObjclsCommon.GetOffernoForRoundOff()
                If _dtSalesItemDetails.Rows(0)("PromotionId").ToString().Contains(offerno) And offerno <> "" Then
                    strDiscount = "Round Off"
                Else
                    strDiscount = getValueByKey("CLSPSO013")
                End If
                'Dim strDiscount As String = getValueByKey("CLSPSO013")
                Dim decDiscount As Decimal
                Dim objDiscount As Object
                If _dtSalesItemDetails.Columns.Contains("IsStatus") Then
                    objDiscount = _dtSalesItemDetails.Compute("sum(Discount)", "IsStatus <> 'Deleted'")
                Else
                    objDiscount = _dtSalesItemDetails.Compute("sum(Discount)", "")
                End If
                If Not objDiscount Is Nothing AndAlso Not objDiscount Is DBNull.Value Then
                    decDiscount = CDbl(objDiscount)
                End If
                decDiscount = MyRound(decDiscount, SORoundOff, _IsBillRoundOffRequired)
                strDiscount = strDiscount.PadRight(iGap)
                Dim strvalueTotalDiscount As String = PrintFormatCurrency(decDiscount, DefaultCurrency, SORoundOff)
                strvalueTotalDiscount = "-" + strvalueTotalDiscount
                PadingString(strvalueTotalDiscount)
                ' CStr(decDiscount) + DefaultCurrency.PadLeft(2) + vbCrLf
                sbPaymentInfo.Append(strDiscount)
                'sbPaymentInfo.Append("-")
                sbPaymentInfo.Append(strvalueTotalDiscount & vbCrLf)
                sbPaymentInfo.Append(_TotalTaxAmt)
                sbPaymentInfo.Append(vbCrLf + lnDotpayment + vbCrLf)

                'Dim strTotalNetAmount As String = "NET TOTAL"
                Dim strTotalNetAmount As String = getValueByKey("CLSPSO014")


                Dim decTotalNetAmount As Decimal
                Dim objTotalNetAmount As Object
                If _dtSalesItemDetails.Columns.Contains("IsStatus") Then
                    objTotalNetAmount = _dtSalesItemDetails.Compute("sum(NetAmount)", "IsStatus <> 'Deleted'")
                Else
                    objTotalNetAmount = _dtSalesItemDetails.Compute("sum(NetAmount)", "")
                End If
                If Not objTotalNetAmount Is Nothing AndAlso Not objTotalNetAmount Is DBNull.Value Then
                    decTotalNetAmount = CDbl(objTotalNetAmount)
                End If

                If Not _dsOtherCost Is Nothing AndAlso _dsOtherCost.Tables.Contains("NewOtherCharges") AndAlso _dsOtherCost.Tables("NewOtherCharges").Rows.Count > 0 Then
                    'decTotalNetAmount = decTotalNetAmount + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(ChargeAmount)", " ") + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " ")
                    Try
                        decTotalNetAmount = decTotalNetAmount + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(ChargeAmount)", " ") + IIf(Not _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " ") Is DBNull.Value, _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " "), 0)
                    Catch ex As Exception

                    End Try

                End If
                decTotalNetAmount = MyRound(decTotalNetAmount, SORoundOff, _IsBillRoundOffRequired)
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
                    If _CreditSale > 0 Then
                        decBalance2Pay = Decimal.Subtract(decBalance2Pay, _CreditSale)
                    End If

                    sbPaymentInfo.Append(lnDotpayment + vbCrLf)
                    strBalance2Pay = strBalance2Pay.PadRight(iGap)
                    Dim strValuebalance2Pay As String = PrintFormatCurrency(MyRound(decBalance2Pay, SORoundOff, _IsBillRoundOffRequired), DefaultCurrency, SORoundOff)

                    PadingString(strValuebalance2Pay)
                    sbPaymentInfo.Append(strBalance2Pay)
                    sbPaymentInfo.Append(strValuebalance2Pay + vbCrLf)


                    Dim _CreditSaleValue As String = PrintFormatCurrency(MyRound(_CreditSale, SORoundOff, _IsBillRoundOffRequired), DefaultCurrency, SORoundOff)

                    Dim CreditSaleText As String = getValueByKey("CLSPSO038")

                    CreditSaleText = CreditSaleText.PadRight(iGap)
                    PadingString(_CreditSaleValue)
                    If _CreditSale <> 0.0 Then
                        sbPaymentInfo.Append(CreditSaleText)
                        sbPaymentInfo.Append(_CreditSaleValue)
                    End If


                    ' PadingString(CreditSaleText)


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
                        Dim strvalueBalance2pay As String = PrintFormatCurrency(MyRound(decBalance2Pay, SORoundOff, _IsBillRoundOffRequired), DefaultCurrency, SORoundOff) 'MyRound(decBalance2Pay, SORoundOff).ToString() + DefaultCurrency.PadLeft(2)
                        PadingString(strvalueBalance2pay)


                        sbPaymentSummary.Append(vbCrLf + strBalance2Pay)
                        sbPaymentSummary.Append(strvalueBalance2pay + vbCrLf)
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


                    Dim strvalue As String = PrintFormatCurrency(MyRound(decTotalOpenAmount, SORoundOff, _IsBillRoundOffRequired), DefaultCurrency, SORoundOff)
                    PadingString(strvalue)

                    strOpenAmount = strOpenAmount  ' Decimal.Zero.ToString + DefaultCurrency.PadLeft(2)
                    sbPaymentInfo.Append(strOpenAmount)
                    sbPaymentInfo.Append(strvalue)
                End If
                sbPaymentInfo.Append(vbCrLf)
                'sbPaymentInfo.Append(strLineA4 + vbCrLf)
                strPaymentInfo = sbPaymentInfo.ToString()
            ElseIf PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                Dim decAdvancedPayed As Decimal = Decimal.Zero
                If Not SalesItemDetails Is Nothing AndAlso SalesItemDetails.Rows.Count > 0 Then
                    Dim clNetPickupAMount As New DataColumn("NetAmountPickUp", System.Type.GetType("System.Decimal"))
                    'clNetPickupAMount.Expression = "sellingPrice * PickUpQty"
                    clNetPickupAMount.Expression = "(NetAmount / Quantity) * PickUpQty"
                    If Not SalesItemDetails.Columns.Contains("NetAmountPickUp") Then
                        SalesItemDetails.Columns.Add(clNetPickupAMount)
                    End If
                    decAdvancedPayed = SalesItemDetails.Compute("sum(NetAmountPickUp)", "IsStatus <> 'Deleted'")
                End If
                'NEED TO DISCUSS THERE IS NO AMOUNT FOR PICKUP QTY 
                'sbPaymentInfo.Append("TOTAL OF PICKED UP ITEMS ".PadRight(iGap))
                sbPaymentInfo.Append(getValueByKey("CLSPSO019") & " ".PadRight(iGap))
                sbPaymentInfo.Append(PrintFormatCurrency(decAdvancedPayed, DefaultCurrency, SORoundOff))
                strPaymentInfo = sbPaymentInfo.ToString()
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


    Protected w_ArticleCode As Single = 16
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
                sbHdrItem.Append(vbCrLf + strLineA4 + vbCrLf)
                'strHdrItem = "Item Code"
                strHdrItem = getValueByKey("CLSPSO020")
                'strHdrItem = strHdrItem.PadRight(22) + "Description"
                strHdrItem = strHdrItem.PadRight(22) + getValueByKey("CLSPSO021")
                sbHdrItem.Append(strHdrItem)
                'strHdrItemQty = strHdrItemQty.PadLeft(24) + "Qty"
                strHdrItemQty = strHdrItemQty.PadLeft(24) + getValueByKey("CLSPSO022")
                sbHdrItem.Append(strHdrItemQty)
            ElseIf PrintSOTransaction = PrintSOTransactionSet.SOReturnStatus Then
                sbHdrItem.Append(vbCrLf + strLineA4 + vbCrLf)
                'strHdrItem = "Item Code"
                strHdrItem = getValueByKey("CLSPSO020")
                'strHdrItem = strHdrItem.PadRight(22) + "Description"
                strHdrItem = strHdrItem.PadRight(22) + getValueByKey("CLSPSO021")
                sbHdrItem.Append(strHdrItem)
                'strHdrItemQty = strHdrItemQty.PadLeft(24) + "Qty"
                'strHdrItemQty = strHdrItemQty.PadRight(34) + "Price"
                strHdrItemQty = strHdrItemQty.PadLeft(24 - 3) + getValueByKey("CLSPSO022")
                strHdrItemQty = strHdrItemQty.PadRight(34 - 3 - 1) + getValueByKey("CLSPSO023")
                sbHdrItem.Append(strHdrItemQty)
                'strHdrItemDisc = strHdrItemDisc.PadLeft(5) + "Disc"
                'strHdrItemDisc = strHdrItemDisc.PadRight(14) + "Tax"
                'strHdrItemDisc = strHdrItemDisc.PadLeft(5) + getValueByKey("CLSPSO024")
                strHdrItemDisc = strHdrItemDisc.PadLeft(5 + 4) + getValueByKey("CLSPSO025")
                sbHdrItem.Append(strHdrItemDisc)
                'strHdrItemNet = strHdrItemNet.PadLeft(9) + "Net"
                strHdrItemNet = strHdrItemNet.PadLeft(9 + 2) + getValueByKey("CLSPSO026")
            Else
                sbHdrItem.Append(vbCrLf + strLineA4 + vbCrLf)
                'strHdrItem = "Item Code"
                'strHdrItem = strHdrItem.PadRight(22) + "Description"
                strHdrItem = getValueByKey("CLSPSO020")
                strHdrItem = strHdrItem.PadRight(22) + getValueByKey("CLSPSO021")

                sbHdrItem.Append(strHdrItem)
                'strHdrItemQty = strHdrItemQty.PadLeft(24) + "Qty"
                'strHdrItemQty = strHdrItemQty.PadRight(34) + "Price"
                strHdrItemQty = strHdrItemQty.PadLeft(24 - 3) + getValueByKey("CLSPSO022")
                strHdrItemQty = strHdrItemQty.PadRight(34 - 3 - 1) + getValueByKey("CLSPSO023")
                sbHdrItem.Append(strHdrItemQty)
                'strHdrItemDisc = strHdrItemDisc.PadLeft(5) + "Disc"
                'strHdrItemDisc = strHdrItemDisc.PadRight(14) + "Tax"
                strHdrItemDisc = strHdrItemDisc.PadLeft(5 + 4) + getValueByKey("CLSPSO024")
                strHdrItemDisc = strHdrItemDisc.PadRight(14 + 5) + getValueByKey("CLSPSO025")
                sbHdrItem.Append(strHdrItemDisc)
                'strHdrItemNet = strHdrItemNet.PadLeft(9) + "Net"
                strHdrItemNet = strHdrItemNet.PadLeft(9 + 2) + getValueByKey("CLSPSO026")
            End If

            Dim sbHdrItemInfo As New StringBuilder
            Dim strArticleCode As String = ""
            Dim strArticleDesc As String = ""
            'Dim iPurchasedQty As Integer = 0
            Dim iPurchasedQty As Double = 0
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
            Dim decTotalPickupQty As Decimal
            Dim decPickupQty As Decimal
            Dim decDeliveredQty As Decimal
            If PrintSOTransaction = PrintSOTransactionSet.Status Then
                'strHdrItemNet = strHdrItemNet.PadRight(16) + "Pick up"
                'strHdrItemNet = strHdrItemNet.PadRight(16 + 3) + getValueByKey("CLSPSO027")
                'strHdrItemNet = strHdrItemNet.PadLeft(16 + 7) + getValueByKey("CLSPSO027")

                'Rakesh- 10.10.2013-7581-> No need pickup qty info in quotation status bill print
                If (_isQuotationPrint) Then
                    strHdrItemNet = strHdrItemNet.PadRight(16 + 3)
                Else
                    strHdrItemNet = strHdrItemNet.PadRight(16 + 3) + getValueByKey("CLSPSO027")
                End If
                sbHdrItem.Append(strHdrItemNet)

                Dim strHdrItemRes As String = ""
                'strHdrItemRes = strHdrItemRes.PadLeft(1) + "Res"
                strHdrItemRes = strHdrItemRes.PadLeft(1) + getValueByKey("CLSPSO028")
                strHdrItemRes = strHdrItemRes.PadRight(5)
                'sbHdrItem.Append(strHdrItemRes)

                Dim strHdrItemCLP As String = ""
                'strHdrItemCLP = "CLP"
                strHdrItemCLP = getValueByKey("CLSPSO029")
                'strHdrItemCLP = strHdrItemCLP.PadRight(4)
                strHdrItemCLP = strHdrItemCLP.PadLeft(4)
                'sbHdrItem.Append(strHdrItemCLP)
               

                For Each drGrid As DataRow In _dtSalesItemDetails.Rows
                    If Not (drGrid.RowState = DataRowState.Deleted) Then
                        If Not drGrid("IsStatus") Is DBNull.Value Then
                            If drGrid("IsStatus") = "Deleted" Then
                                Continue For
                            End If
                        End If

                        ValidateDataRow(drGrid("ArticleCode"), strArticleCode)
                        strArticleCode = strArticleCode.PadRight(20 - 1)

                        If (DisplayArticleFullName) Then
                            ValidateDataRow(drGrid("ArticleDiscription"), strArticleDesc)
                        Else
                            ValidateDataRow(drGrid("Discription"), strArticleDesc)
                        End If

                        strArticleDesc = strArticleDesc.PadRight(31)

                        If DtSoBulkComboHdr IsNot Nothing AndAlso DtSoBulkComboHdr.Rows.Count > 0 AndAlso Not IsDBNull(drGrid("BillLineNo")) Then

                            Dim HdrRowFilter As String = " ComboSrNo =" & drGrid("BillLineNo")

                            Dim dr() = DtSoBulkComboHdr.Select(HdrRowFilter)
                            If dr.Count > 0 Then
                                Dim BulkComboMstId As Int64 = dr(0)("BulkComboMstId")
                                Dim DtlRowFilter As String = "BulkComboMstId=" & BulkComboMstId
                                Dim dvresult As New DataView(DtSoBulkComboDtl, DtlRowFilter, "", DataViewRowState.CurrentRows)
                                If dvresult.ToTable.Rows.Count > 0 Then
                                    Dim articleDescriptionDictionary As New Dictionary(Of String, String)
                                    articleDescriptionDictionary.Add(strArticleDesc.ToString, 0)
                                    GetArticleDecriptionDictionary(articleDescriptionDictionary, dvresult.ToTable)

                                    strArticleDesc = GetMultilinedString(articleDescriptionDictionary)
                                End If
                            End If
                        End If

                        ValidateDataRow(drGrid("Quantity"), iPurchasedQty)
                        strPurchasedQty = iPurchasedQty.ToString()
                        strPurchasedQty = strPurchasedQty.PadLeft(8 - 2)
                        strPurchasedQty = strPurchasedQty.PadRight(8 - 2)

                        ValidateDataRow(drGrid("SellingPrice"), iPrice)


                        iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, SORoundOff)
                        iPrice = iPrice.PadLeft(12 + 2)
                        iPrice = iPrice.PadRight(12 + 2)

                        ValidateDataRow(drGrid("totaldiscpercentage"), strDisc)

                        'strDisc = PrintFormatCurrency(strDisc, DefaultCurrency, SORoundOff)
                        If strDisc <> String.Empty Then
                            strDisc = FormatNumber(strDisc, 2)
                            strDisc = strDisc & " %"
                        End If

                        strDisc = strDisc.PadLeft(12 + 1)
                        strDisc = strDisc.PadRight(12 + 1)

                        ValidateDataRow(drGrid("TotalTaxAmt"), decTax)


                        strTax = PrintFormatCurrency(decTax, DefaultCurrency, SORoundOff)
                        strTax = "  " & strTax
                        strTax = strTax.PadLeft(10 + 2)
                        strTax = strTax.PadRight(10 + 2)

                        ValidateDataRow(drGrid("NetAmount"), iNetAmount)

                        iNetAmount = PrintFormatCurrency(iNetAmount, DefaultCurrency, SORoundOff)

                        iNetAmount = iNetAmount.PadLeft(14)
                        iNetAmount = iNetAmount.PadRight(14)


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
                        strPickUp = Math.Round(decTotalPickupQty, 3).ToString()
                        'End change

                        strPickUp = strPickUp.PadLeft(6)
                        strPickUp = strPickUp.PadRight(6)
                        ValidateDataRow(drGrid("ReservedQty"), strReservedQty)
                        strReservedQty = strReservedQty.PadLeft(7 - 1)
                        strReservedQty = strReservedQty.PadRight(7 - 1)

                        ValidateDataRow(drGrid("IsCLP"), strCLP)
                        strCLP = strCLP.PadLeft(6)
                        strCLP = strCLP.PadRight(6)
                        iItemDetailHeight = iItemDetailHeight + 15
                        'sbHdrItemInfo.Append(strArticleCode & strArticleDesc & strPurchasedQty.ToString() & iPrice & strDisc & strTax & iNetAmount & strPickUp & strReservedQty & strCLP + vbCrLf)

                        If (_isQuotationPrint) Then
                            sbHdrItemInfo.Append(strArticleCode & strArticleDesc & strPurchasedQty.ToString() & iPrice & strDisc & strTax & iNetAmount + vbCrLf)
                        Else
                            sbHdrItemInfo.Append(strArticleCode & strArticleDesc & strPurchasedQty.ToString() & iPrice & strDisc & strTax & iNetAmount & strPickUp + vbCrLf)
                        End If

                        itemCount += 1
                    End If
                Next
                A4GetOtherCharges(strArticleCode, strArticleDesc, strPurchasedQty, iPrice, strDisc, strTax, iNetAmount, sbHdrItemInfo)
            ElseIf PrintSOTransaction = PrintSOTransactionSet.SOReturnStatus Then
                'strHdrItemNet = strHdrItemNet.PadRight(16) + "ReturnedQty"
                strHdrItemNet = strHdrItemNet.PadRight(16 + 3) + getValueByKey("CLSPSO036")
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

                        strArticleCode = strArticleCode.PadRight(20 - 1)

                        If (DisplayArticleFullName) Then
                            ValidateDataRow(drGrid("ArticleDiscription"), strArticleDesc)
                        Else
                            ValidateDataRow(drGrid("Discription"), strArticleDesc)
                        End If

                        strArticleDesc = strArticleDesc.PadRight(31)

                        ValidateDataRow(drGrid("Quantity"), iPurchasedQty)

                        strPurchasedQty = iPurchasedQty.ToString()
                        strPurchasedQty = strPurchasedQty.PadLeft(8 - 2)
                        strPurchasedQty = strPurchasedQty.PadRight(8 - 2)

                        ValidateDataRow(drGrid("SellingPrice"), iPrice)
                        iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, SORoundOff)
                        iPrice = iPrice.PadLeft(12 + 2)
                        iPrice = iPrice.PadRight(12 + 2)

                        ValidateDataRow(drGrid("totaldiscpercentage"), strDisc)
                        'strDisc = PrintFormatCurrency(strDisc, DefaultCurrency, SORoundOff)
                        If strDisc <> String.Empty Then
                            strDisc = FormatNumber(strDisc, 2)
                            strDisc = strDisc & " %"
                        End If

                        strDisc = strDisc.PadLeft(12 + 1)
                        strDisc = strDisc.PadRight(12 + 1)

                        ValidateDataRow(drGrid("TotalTaxAmt"), decTax)
                        strTax = PrintFormatCurrency(decTax * (-1), DefaultCurrency, SORoundOff)
                        strTax = "   " + strTax
                        strTax = strTax.PadLeft(12)
                        strTax = strTax.PadRight(12)

                        ValidateDataRow(drGrid("ExclTaxAmt"), decTax)

                        ValidateDataRow((-1) * drGrid("PickUpQty"), strPickUp)

                        strPickUp = strPickUp.PadLeft(6 + 3)
                        strPickUp = strPickUp.PadRight(7 + 2)

                        ValidateDataRow(drGrid("ReservedQty"), strReservedQty)
                        strReservedQty = strReservedQty.PadLeft(3 + 3)
                        strReservedQty = strReservedQty.PadRight(5 + 1)

                        If PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                            Dim decPrice As Decimal = 0
                            ValidateDataRow(drGrid("SellingPrice"), decPrice)
                            Dim iPickupQty As Double = 0
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
                        iNetAmount = iNetAmount.PadLeft(13 + 1)
                        iNetAmount = iNetAmount.PadRight(13 + 1)

                        ValidateDataRow(drGrid("IsCLP"), strCLP)
                        strCLP = strCLP.PadLeft(4 + 2)
                        strCLP = strCLP.PadRight(4 + 2)
                        iItemDetailHeight = iItemDetailHeight + 15
                        If PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                            sbHdrItemInfo.Append(strArticleCode & strArticleDesc & strPickUp & iPrice & strDisc & strTax & iNetAmount + vbCrLf)
                        Else
                            'sbHdrItemInfo.Append(strArticleCode & strArticleDesc & strPurchasedQty.ToString() & iPrice & strDisc & strTax & iNetAmount & strPickUp + vbCrLf)
                            'Added by Rohit for So return print correction
                            sbHdrItemInfo.Append(strArticleCode & strArticleDesc & strPurchasedQty.ToString() & iPrice & strTax & iNetAmount & strPickUp + vbCrLf)
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
                    strHdrItemNet = strHdrItemNet.PadRight(18 + 2) + getValueByKey("CLSPSO027")
                    strFilter = "Quantity>0"
                End If
                sbHdrItem.Append(strHdrItemNet)


                For Each drGrid As DataRow In _dtSalesItemDetails.Select(strFilter)
                    Dim index As Integer = 0
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
                        strArticleCode = strArticleCode.PadRight(20 - 1)

                        If (DisplayArticleFullName) Then
                            ValidateDataRow(drGrid("ArticleDiscription"), strArticleDesc)
                        Else
                            ValidateDataRow(drGrid("Discription"), strArticleDesc)
                        End If

                        strArticleDesc = strArticleDesc.PadRight(31)
                        Try
                            If DtSoBulkComboHdr IsNot Nothing AndAlso DtSoBulkComboHdr.Rows.Count > 0 AndAlso Not IsDBNull(drGrid("BillLineNo")) Then
                                Dim HdrRowFilter As String = " ComboSrNo =" & drGrid("BillLineNo")
                                Dim dr() = DtSoBulkComboHdr.Select(HdrRowFilter)
                                If dr.Count > 0 Then
                                    Dim BulkComboMstId As Int64 = dr(0)("BulkComboMstId")
                                    Dim DtlRowFilter As String = "BulkComboMstId=" & BulkComboMstId
                                    Dim dvresult As New DataView(DtSoBulkComboDtl, DtlRowFilter, "", DataViewRowState.CurrentRows)
                                    If dvresult.ToTable.Rows.Count > 0 Then
                                        Dim articleDescriptionDictionary As New Dictionary(Of String, String)
                                        articleDescriptionDictionary.Add(strArticleDesc.ToString, 0)
                                        GetArticleDecriptionDictionary(articleDescriptionDictionary, dvresult.ToTable)
                                        strArticleDesc = GetMultilinedString(articleDescriptionDictionary)
                                    End If
                                End If
                            End If

                        Catch ex As Exception
                            LogException(ex)
                        End Try

                        ValidateDataRow(drGrid("Quantity"), iPurchasedQty)

                        strPurchasedQty = iPurchasedQty.ToString()
                        strPurchasedQty = strPurchasedQty.PadLeft(8 - 2)
                        strPurchasedQty = strPurchasedQty.PadRight(8 - 2)

                        ValidateDataRow(drGrid("SellingPrice"), iPrice)

                        iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, SORoundOff)
                        iPrice = iPrice.PadLeft(12 + 2 + 1)
                        iPrice = iPrice.PadRight(12 + 2 + 1)

                        ValidateDataRow(drGrid("totaldiscpercentage"), strDisc)


                        'strDisc = PrintFormatCurrency(IIf(strDisc <> "", strDisc, 0), DefaultCurrency, SORoundOff)
                        If strDisc <> String.Empty Then
                            strDisc = FormatNumber(strDisc, 2)
                            strDisc = strDisc & " %"
                        End If

                        strDisc = strDisc.PadLeft(12 + 1)
                        strDisc = strDisc.PadRight(12 + 1)

                        'Dim iPickupQty As Double = 0
                        Dim iPickupQty As Decimal = 0
                        If PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                            'Dim decPrice As Decimal = 0
                            'ValidateDataRow(drGrid("SellingPrice"), decPrice)
                            'Dim iPickupQty As Integer = 0
                            'ValidateDataRow(drGrid("PickUpQty"), iPickupQty)

                            'iNetAmount = (decPrice * iPickupQty) - IIf(drGrid("Discount") Is DBNull.Value, 0, drGrid("Discount"))
                            'iNetAmount = Decimal.Add(iNetAmount, decTax)
                            ValidateDataRow(drGrid("TotalTaxAmt"), decTax)
                            Dim PricePerUnit As Decimal = 0
                            PricePerUnit = drGrid("SellingPrice") ' drGrid("Quantity")
                            ValidateDataRow(drGrid("PickUpQty"), iPickupQty)
                            iNetAmount = (PricePerUnit * iPickupQty) - IIf(drGrid("Discount") Is DBNull.Value, 0, (drGrid("Discount") / drGrid("Quantity")) * drGrid("PickUpQty"))
                            Dim decTaxcopy = (decTax / iPurchasedQty) * iPickupQty
                            iNetAmount = Decimal.Add(iNetAmount, decTaxcopy)

                            'Dim clNetPickupAMount As New DataColumn("NetAmountPickUp", System.Type.GetType("System.Decimal"))
                            'clNetPickupAMount.Expression = "(NetAmount / Quantity) * PickUpQty"
                            'If Not SalesItemDetails.Columns.Contains("NetAmountPickUp") Then
                            '    SalesItemDetails.Columns.Add(clNetPickupAMount)
                            'End If
                            'iNetAmount = FormatNumber(SalesItemDetails.Rows(itemCount)("NetAmountPickUp").ToString(), 2)
                            ' iNetAmount = SalesItemDetails.Compute("NetAmountPickUp", "IsStatus <> 'Deleted'")
                        Else
                            ValidateDataRow(drGrid("NetAmount"), iNetAmount)
                            iNetAmount = Decimal.Add(iNetAmount, decTax)
                        End If
                        iNetAmount = PrintFormatCurrency(iNetAmount, DefaultCurrency, SORoundOff)
                        iNetAmount = iNetAmount.PadLeft(14)
                        iNetAmount = iNetAmount.PadRight(14)


                        If PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                            ValidateDataRow(drGrid("TotalTaxAmt"), decTax)
                            decTax = (decTax / iPurchasedQty) * iPickupQty
                            strTax = PrintFormatCurrency(decTax, DefaultCurrency, SORoundOff)
                            strTax = strTax.PadLeft(10 + 2)
                            strTax = strTax.PadRight(10 + 2)

                            ValidateDataRow(drGrid("ExclTaxAmt"), decTax)
                        Else
                            ValidateDataRow(drGrid("TotalTaxAmt"), decTax)

                            strTax = PrintFormatCurrency(decTax, DefaultCurrency, SORoundOff)
                            strTax = strTax.PadLeft(10 + 2)
                            strTax = strTax.PadRight(10 + 2)

                            ValidateDataRow(drGrid("ExclTaxAmt"), decTax)
                        End If


                        'Changed By Rohit for Issue No. 0005950 (Remove decimal places from qty value)
                        'ValidateDataRow(drGrid("PickUpQty"), strPickUp)
                        ValidateDataRow(Math.Round(drGrid("PickUpQty"), 3), strPickUp)
                        'Change End
                        If Not drGrid("PickUpQty") Is DBNull.Value Then
                            decPickupQty = 0
                            decDeliveredQty = 0
                            decPickupQty = drGrid("PickUpQty")
                            If Not drGrid("DeliveredQty") Is DBNull.Value Then
                                decDeliveredQty = drGrid("DeliveredQty")
                            End If
                            decTotalPickupQty = decPickupQty + decDeliveredQty
                        End If
                        ' strPickUp = decTotalPickupQty.ToString()
                        strPickUp = strPickUp.PadLeft(7 - 1)
                        strPickUp = strPickUp.PadRight(7 - 1)

                        ValidateDataRow(drGrid("ReservedQty"), strReservedQty)
                        strReservedQty = strReservedQty.PadLeft(7 - 1)
                        strReservedQty = strReservedQty.PadRight(7 - 1)

                        ValidateDataRow(drGrid("IsCLP"), strCLP)
                        strCLP = strCLP.PadLeft(4 + 2)
                        strCLP = strCLP.PadRight(4 + 2)
                        iItemDetailHeight = iItemDetailHeight + 15
                        If PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                            sbHdrItemInfo.Append(strArticleCode & strArticleDesc & strPickUp & iPrice & strDisc & strTax & iNetAmount + vbCrLf)
                        Else
                            sbHdrItemInfo.Append(strArticleCode & strArticleDesc & strPurchasedQty.ToString() & iPrice & strDisc & strTax & iNetAmount & strPickUp + vbCrLf)
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

                        If (DisplayArticleFullName) Then
                            ValidateDataRow(drGrid("ArticleDiscription"), strArticleDesc)
                        Else
                            ValidateDataRow(drGrid("Discription"), strArticleDesc)
                        End If

                        strArticleDesc = strArticleDesc.PadRight(31)

                        ValidateDataRow(drGrid("Quantity"), iPurchasedQty)
                        strPurchasedQty = iPurchasedQty.ToString()
                        strPurchasedQty = strPurchasedQty.PadLeft(8)
                        strPurchasedQty = strPurchasedQty.PadRight(8)

                        'Changed By Rohit for Issue No. 0005950 (Remove decimal places from qty value)
                        'ValidateDataRow(drGrid("PickUpQty"), strPickUp)
                        ValidateDataRow(Math.Round(drGrid("PickUpQty"), 3), strPickUp)
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

                        If (DisplayArticleFullName) Then
                            ValidateDataRow(drGrid("ArticleDiscription"), strArticleDesc)
                        Else
                            ValidateDataRow(drGrid("Discription"), strArticleDesc)
                        End If

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
                        Dim iPickupQty As Double = 0
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
            sbHdrItemInfo.Append(strLineA4)
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
            Dim iPurchasedQty As Double

            Dim objNetAmount As Object
            Dim decTax As Decimal
            If Not _dsOtherCost Is Nothing AndAlso _dsOtherCost.Tables.Contains("NewOtherCharges") Then
                For Each drGrid As DataRow In _dsOtherCost.Tables("NewOtherCharges").Rows
                    If Not (drGrid.RowState = DataRowState.Deleted) Then

                        'strArticleCode = "Add. Cost"
                        strArticleCode = getValueByKey("CLSPSO030")
                        strArticleCode = strArticleCode.PadRight(20 - 1)
                        If Not drGrid("ChargeName") Is DBNull.Value Then
                            strArticleDesc = drGrid("ChargeName")
                        End If

                        strArticleDesc = strArticleDesc.PadRight(31)
                        iPurchasedQty = "1"
                        strPurchasedQty = iPurchasedQty.ToString()
                        strPurchasedQty = strPurchasedQty.PadLeft(8 - 2)
                        strPurchasedQty = strPurchasedQty.PadRight(8 - 2)
                        iPrice = drGrid("ChargeAmount")

                        iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, SORoundOff)
                        If PrintSOTransaction = PrintSOTransactionSet.Payment Then
                            iPrice = iPrice.PadLeft(12 + 2 + 1)
                            iPrice = iPrice.PadRight(12 + 2 + 1)
                        Else
                            iPrice = iPrice.PadLeft(12 + 2)
                            iPrice = iPrice.PadRight(12 + 2)
                        End If
                        'iPrice = iPrice.PadLeft(12 + 2)
                        'iPrice = iPrice.PadRight(12 + 2)
                        strDisc = Decimal.Zero
                        strDisc = PrintFormatCurrency(strDisc, DefaultCurrency, SORoundOff)
                        strDisc = strDisc.PadLeft(12 + 1)
                        strDisc = strDisc.PadRight(12 + 1)
                        decTax = 0.0
                        If Not drGrid("TaxAmt") Is DBNull.Value Then
                            decTax = drGrid("TaxAmt")
                        End If

                        strTax = PrintFormatCurrency(decTax, DefaultCurrency, SORoundOff)

                        strTax = strTax.PadLeft(10 + 2)
                        strTax = strTax.PadRight(10 + 2)

                        iNetAmount = ""
                        objNetAmount = drGrid("ChargeAmount")
                        If Not objNetAmount Is Nothing AndAlso Not objNetAmount Is DBNull.Value Then
                            iNetAmount = Decimal.Add(CDbl(objNetAmount), decTax).ToString()
                        Else
                            iNetAmount = drGrid("ChargeAmount")
                        End If


                        iNetAmount = PrintFormatCurrency(iNetAmount, DefaultCurrency, SORoundOff)

                        iNetAmount = iNetAmount.PadLeft(14)
                        iNetAmount = iNetAmount.PadRight(14)

                        sbHdrItemInfo.Append(strArticleCode & strArticleDesc & strPurchasedQty.ToString() & iPrice & strDisc & strTax & iNetAmount + vbCrLf)

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
            If PrintSOTransaction = PrintSOTransactionSet.Status Then
                Dim sbTitle As New StringBuilder
                sbTitle.Append(strLineL40 + vbCrLf)
                Dim strTitleInfo As String
                'strTitleInfo = String.Format("SALES ORDER {0} STATUS:{1}  ", SalesOrderNo, SOStatus)
                strTitleInfo = String.Format(getValueByKey("CLSPSO001") & "", SalesOrderNo, SOStatus)
                'strTitleInfo.PadRight(22)
                sbTitle.Append(SplitString(strTitleInfo).ToString() + vbCrLf)
                'strTitleInfo = String.Format("CREATION DATE {0}", Now.Date)
                strTitleInfo = String.Format(getValueByKey("CLSPSO031"), SOCreationON)
                sbTitle.Append(SplitString(strTitleInfo).ToString() + vbCrLf)
                strTitleInfo = String.Format(getValueByKey("CLSCMP022")) & " " & SODeliveredON 'CLSCMP022
                sbTitle.Append(SplitString(strTitleInfo).ToString() + vbCrLf)
                sbTitle.Append(strLineL40 + vbCrLf)
                Return sbTitle.ToString()
            ElseIf PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                Dim sbTitle As New StringBuilder
                sbTitle.Append(strLineL40 + vbCrLf)
                Dim strTitleInfo As String
                'strTitleInfo = String.Format("SALES ORDER  {0} DELIVERY NOTE ", SalesOrderNo)
                strTitleInfo = String.Format(getValueByKey("CLSPSO004") & " ", SalesOrderNo)
                strTitleInfo.PadRight(22)
                sbTitle.Append(SplitString(strTitleInfo).ToString() + vbCrLf)
                'strTitleInfo = String.Format("CREATION DATE {0}", Now.Date)
                strTitleInfo = String.Format(getValueByKey("CLSPSO031"), Now.Date)
                sbTitle.Append(SplitString(strTitleInfo).ToString() + vbCrLf)
                sbTitle.Append(strLineL40 + vbCrLf)
                Return sbTitle.ToString()
            ElseIf PrintSOTransaction = PrintSOTransactionSet.Payment Then
                Dim sbTitle As New StringBuilder
                sbTitle.Append(strLineL40 + vbCrLf)
                Dim strTitleInfo As String
                'Dim strPaymentHdr As String = String.Format("CASH MEMO Nr {0}", InvoiceNumber)
                'Dim strPaymentHdr As String = String.Format(getValueByKey("CLSPSO006"), InvoiceNumber)
                Dim strPaymentHdr As String = String.Format(getValueByKey("CLSPSO006"), "") & InvoiceNumber.ToString.PadLeft(strLineL40.Length - String.Format(getValueByKey("CLSPSO006"), "").Length)
                ' String.Format(getValueByKey("CLSPSO006"), InvoiceNumber)
                sbTitle.Append(SplitString(strPaymentHdr).ToString() + vbCrLf)
                'strTitleInfo = String.Format("SALES ORDER  {0} PAYMENT ", SalesOrderNo)
                'strTitleInfo = "SALES ORDER " & "SALES ORDER ".ToString.PadLeft(strLineL40.Length - ("SALES ORDER ").Length).Length
                strTitleInfo = "SALES ORDER " & SalesOrderNo.ToString.PadLeft(strLineL40.Length - ("SALES ORDER ").Length) 'String.Format("SALES ORDER      {0} " & " ", SalesOrderNo)
                strTitleInfo.PadRight(22)
                sbTitle.Append(SplitString(strTitleInfo).ToString() & vbCrLf)
                'strTitleInfo = String.Format("DATE {0}", strTitleInfo, Now.Date)
                strTitleInfo = String.Format(getValueByKey("CLSPSO032"), "")
                strTitleInfo = strTitleInfo & SOCreationON.ToString.PadLeft(strLineL40.Length - String.Format(getValueByKey("CLSPSO032"), "").Length)
                sbTitle.Append(strTitleInfo.ToString() & vbCrLf)
                sbTitle.Append(strLineL40 + vbCrLf)
                Return sbTitle.ToString()
            ElseIf PrintSOTransaction = PrintSOTransactionSet.GiftVoucherDocumentPrint Then
                Dim sbTitle As New StringBuilder
                sbTitle.Append(strLineL40 + vbCrLf)
                Dim strTitleInfo As String
                'Dim strPaymentHdr As String = String.Format("CASH MEMO Nr {0}", InvoiceNumber)
                Dim strPaymentHdr As String = String.Format(getValueByKey("CLSPSO006"), InvoiceNumber)
                sbTitle.Append(SplitString(strPaymentHdr).ToString() + vbCrLf)
                'strTitleInfo = String.Format("SALES ORDER  {0} Gift Voucher ", SalesOrderNo)
                strTitleInfo = String.Format(getValueByKey("CLSPSO007"), SalesOrderNo)
                strTitleInfo.PadRight(22)
                sbTitle.Append(SplitString(strTitleInfo).ToString() + vbCrLf)
                'strTitleInfo = String.Format("DATE {0}", strTitleInfo, Now.Date)
                strTitleInfo = String.Format(getValueByKey("CLSPSO032"), strTitleInfo, Now.Date)
                sbTitle.Append(SplitString(strTitleInfo).ToString() + vbCrLf)


                sbTitle.Append(strLineL40 + vbCrLf)
                Return sbTitle.ToString()
            End If

        Catch ex As Exception

        End Try
    End Function

    Private Function L40SOStatusPrint(ByVal PrintPreview As Boolean) As Boolean

        Try
            CheckDocumentType()
            'Comman Print 
            Dim strHeader As New StringBuilder
            Dim strFooter As New StringBuilder
            Dim strWelcomeMsg As New StringBuilder
            Dim strTaxInformation As New StringBuilder
            Dim strPromotionMsg As New StringBuilder
            PrinttHeaderAndFooter(strHeader, strFooter, strWelcomeMsg, strPromotionMsg, strTaxInformation, strSODocumentType, PrintSOPageSetup)


            'Title 
            'Dim strLine As String = "----------------------------------------"

            Dim sbTitle As New StringBuilder
            sbTitle.Append(L40GetTitle())

            'CustomerDetails
            Dim strCustomerDetails As String
            strCustomerDetails = L40GetCustomerDetails(_dtCustomerDetails)
            'Customer Delivery Details
            Dim strCustomerDeliveryDetails As String
            strCustomerDeliveryDetails = L40GetCustomerDeliveryDetails(_dtCustomerDetails)
            Dim sbRemark As New StringBuilder
            'Dim strRemark As String = "Remark"
            Dim strRemark As String = getValueByKey("CLSPSO010") & "   : "

            sbRemark.Append(strRemark + ItemReturnReason.ToString() + vbCrLf)
            strCustomerDeliveryDetails = sbRemark.ToString()

            Dim strCashierDetails As String = L40CashierDetails(PaymentDate, InvoiceNumber, CashierId)
            If SalesPerson <> "" Then
                strCashierDetails &= vbCrLf & String.Format(getValueByKey("Crs023")) & "  : " & SalesPerson
            End If
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
            sbFinalPrint.Append(strHeader)
            sbFinalPrint.Append(vbCrLf + strLineL40 + vbCrLf)
            sbFinalPrint.Append(strMessages)
            sbFinalPrint.Append(vbCrLf + strLineL40 + vbCrLf)
            sbFinalPrint.Append(strCashierDetails)
            sbFinalPrint.Append(vbCrLf)

            sbFinalPrint.Append(sbTitle.ToString() + vbCrLf)
            sbFinalPrint.Append(strCustomerDetails + vbCrLf)
            sbFinalPrint.Append(strCustomerDeliveryDetails + vbCrLf)
            sbFinalPrint.Append(vbCrLf + strLineL40 + vbCrLf)
            If PrintSOTransaction = PrintSOTransactionSet.SOReturnStatus Then
                sbFinalPrint.Append(SplitString(getValueByKey("CLSPSO033") + ItemReturnReason.ToString()).ToString() + vbCrLf)
                sbFinalPrint.Append(strLineL40 + vbCrLf)
            End If

            sbFinalPrint.Append(strItemHeader + vbCrLf)
            sbFinalPrint.Append(strItemSales + vbCrLf)

            sbFinalPrint.Append(strPaymentInfo + vbCrLf)
            sbFinalPrint.Append(vbCrLf + strLineL40)
            If PrintSOTransaction = PrintSOTransactionSet.Payment Then
                Dim strTaxDetailsInvoice As String = ""
                L40GetTaxDetails(strTaxDetailsInvoice)
                sbFinalPrint.Append(strTaxDetailsInvoice + vbCrLf)
            End If

            Dim strTermsNConditions As String = ""

            strTermsNConditions = A4GetStringTermsCondition(strDocTypeforTermsnConditions, SiteCode, PrintSOPageSetup)
            sbFinalPrint.Append(strTermsNConditions + vbCrLf)

            sbFinalPrint.Append(strFooter.ToString() + vbCrLf)

            If (Not String.IsNullOrEmpty(GiftReceiptMessage)) Then
                sbFinalPrint.Append(SplitString(GiftReceiptMessage).ToString() + vbCrLf)
                sbFinalPrint.Append(vbCrLf + strLineL40 + vbCrLf)
            End If

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
            'vStmtQry.Append(" From SalesInvoice Where SiteCode='" & vSiteNo & "' And DocumentNumber='" & vSalesNo & "' and TenderTypeCode <> 'Credit' " & vbCrLf)
            'removed sitecode from query - Gaurav danani
            vStmtQry.Append(" From SalesInvoice Where status=1 and DocumentNumber='" & vSalesNo & "' and TenderTypeCode <> 'Credit' " & vbCrLf)
            vStmtQry.Append(" union all select RefBillNo as SalesNo,BillNo as InvoiceNo,CurrencyCode,'' as DocumentType, dbo.FnGetDesc('Terminal',TerminalID,SiteCode) as TerminalID,TenderTypeCode as TenderType,AmountTendered as InvoiceAmt,createdby as UserName, CmRcptDateTime  as InvoiceDate   from CreditReceipt where RefBillNo='" & vSalesNo & "'")
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
                        strInvoiceAmt = MyRound(strInvoiceAmt, SORoundOff, _IsBillRoundOffRequired)
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

                'Dim strDefaultText As String = "ADVANCE PAYD"
                Dim strDefaultText As String = getValueByKey("CLSPSO015")
                Dim strTerminalInfo As String
                Dim strInvoiceAmt As String = ""


                Dim decInvoiceAmt As Decimal = 0.0

                Dim strInvoiceNumber As String
                Dim strTerminalInfoLine As String = ""
                Dim dateTimeinvoiceDate As Date
                Dim strL1 As String
                Dim strCurrencyCode As String
                Dim strFinalString As String

                '------ Change By Mahesh Advanced payments shown acc Cash Memo No only 
                Dim dvDetail As New DataView(dtInvoiceData, "", "", DataViewRowState.CurrentRows)
                'Dim DtUnique As DataTable = dvDetail.ToTable(True, "InvoiceDate", "TerminalID", "InvoiceNo", "CurrencyCode")
                Dim DtUnique As DataTable = dvDetail.ToTable(True, "TerminalID", "InvoiceNo", "CurrencyCode")
                For Each drInvoiceData As DataRow In DtUnique.Rows

                    strTerminalInfo = ""
                    strInvoiceAmt = ""
                    strInvoiceNumber = drInvoiceData("InvoiceNo")
                    'dateTimeinvoiceDate = drInvoiceData("InvoiceDate")
                    'dateTimeinvoiceDate = dtInvoiceData.Compute("Max(InvoiceDate)", "InvoiceNo='" & strInvoiceNumber & "'")

                    Dim drMaxValue() = dtInvoiceData.Select(" InvoiceNo='" & strInvoiceNumber & "'")
                    If drMaxValue.Count > 0 Then
                        dateTimeinvoiceDate = drMaxValue(0).Item("InvoiceDate")
                    End If

                    strTerminalInfo = drInvoiceData("TerminalID")

                    strTerminalInfoLine = strTerminalInfo & "/" & strInvoiceNumber

                    'decInvoiceAmt = drInvoiceData("InvoiceAmt")


                    decInvoiceAmt = dtInvoiceData.Compute("sum(InvoiceAmt)", "InvoiceNo='" & strInvoiceNumber & "'")
                    decInvoiceAmt = MyRound(decInvoiceAmt, SORoundOff, _IsBillRoundOffRequired)


                    strCurrencyCode = drInvoiceData("CurrencyCode")
                    strL1 = strDefaultText + dateTimeinvoiceDate.ToString(clsCommon.GetSystemDateFormat()).PadLeft(12) & " " & strTerminalInfoLine.PadLeft(4)
                    strL1 = SplitString(strL1).ToString()

                    strInvoiceAmt = strInvoiceAmt.PadLeft(iPaymentHeight) + decInvoiceAmt.ToString()
                    strFinalString = PrintFormatCurrency(strInvoiceAmt, strCurrencyCode, SORoundOff)
                    'strFinalString = strFinalString.PadLeft(iPaymentHeight - 16)
                    'strFinalString = strFinalString.PadRight(iPaymentHeight - 16)
                    strFinalString = strFinalString.PadLeft(strLineL40.Length)
                    sbAdvancePayment.Append(vbCrLf & strL1 & vbCrLf)
                    sbAdvancePayment.Append(strFinalString)

                    'sbAdvancePayment.Append(strInvoiceAmt & "".PadRight(1) & strCurrencyCode.PadLeft(4) + vbCrLf)

                Next
                decTotalAdvancePayment = dtInvoiceData.Compute("sum(InvoiceAmt)", "")
                decTotalAdvancePayment = MyRound(decTotalAdvancePayment, SORoundOff, _IsBillRoundOffRequired)
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
                        strTaxDetailsForInvoice = strTaxDetailsForInvoice + vbCrLf + strTaxName.PadRight(18) + strTaxValue.PadLeft(2)
                    Else
                        strTaxValue = PrintFormatCurrency(0.0, DefaultCurrency, DecimalDigits)
                        strTaxDetailsForInvoice = strTaxDetailsForInvoice + vbCrLf + "Not FOund" + strTaxValue.PadLeft(2)
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

    Public Function L40GetPaymentInfo(ByRef strPaymentInfo As String, Optional ByVal iGap As Integer = 25) As Boolean
        Try

            Dim sbPaymentInfo As New StringBuilder
            Dim strOpenAmount As String
            Dim strvalue As String
            If PrintSOTransaction = PrintSOTransactionSet.Status Or PrintSOTransaction = PrintSOTransactionSet.Payment Then
                sbPaymentInfo.Append(strLineL40 + vbCrLf)
                'Dim strTotal2Pay As String = "TOTAL  "
                Dim strTotal2Pay As String = getValueByKey("CLSPSO034") & "  "

                Dim decTotal2Pay As Decimal = _dtSalesItemDetails.Compute("sum(NetAmount)", "IsStatus<>'Deleted'")
                decTotal2Pay = MyRound(decTotal2Pay, SORoundOff, _IsBillRoundOffRequired)
                'Other Charges adding into NetAmount 
                If Not _dsOtherCost Is Nothing AndAlso _dsOtherCost.Tables.Contains("NewOtherCharges") AndAlso _dsOtherCost.Tables("NewOtherCharges").Rows.Count > 0 Then
                    'decTotal2Pay = decTotal2Pay + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(ChargeAmount)", " ") + IIf(Not _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " ") Is DBNull.Value, _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " "), 0) '_dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " ")
                    Try
                        decTotal2Pay = decTotal2Pay + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(ChargeAmount)", " ") + IIf(Not _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " ") Is DBNull.Value, _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", ""), 0)
                    Catch ex As Exception

                    End Try
                End If

                'strTotal2Pay = strTotal2Pay.PadRight(iGap) + PrintFormatCurrency(decTotal2Pay, DefaultCurrency, SORoundOff)
                strTotal2Pay = strTotal2Pay.PadRight(strLineL40.Length - PrintFormatCurrency(decTotal2Pay, DefaultCurrency, SORoundOff).Length) + PrintFormatCurrency(decTotal2Pay, DefaultCurrency, SORoundOff)
                sbPaymentInfo.Append(strTotal2Pay + vbCrLf)

                'Dim strTotalNetAmount As String = "NET TOTAL"
                Dim strTotalNetAmount As String = getValueByKey("CLSPSO014")
                Dim decTotalNetAmount As Decimal = _dtSalesItemDetails.Compute("sum(NetAmount)", "IsStatus<>'Deleted'")
                'Other Charges adding into NetAmount 
                If Not _dsOtherCost Is Nothing AndAlso _dsOtherCost.Tables.Contains("NewOtherCharges") AndAlso _dsOtherCost.Tables("NewOtherCharges").Rows.Count > 0 Then
                    'decTotalNetAmount = decTotalNetAmount + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(ChargeAmount)", " ") + IIf(Not _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " ") Is DBNull.Value, _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " "), 0) '_dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " ")
                    Try
                        decTotalNetAmount = decTotalNetAmount + _dsOtherCost.Tables("NewOtherCharges").Compute("sum(ChargeAmount)", " ") + IIf(Not _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", " ") Is DBNull.Value, _dsOtherCost.Tables("NewOtherCharges").Compute("sum(TaxAmt)", ""), 0)
                    Catch ex As Exception

                    End Try
                End If
                decTotalNetAmount = MyRound(decTotalNetAmount, SORoundOff, _IsBillRoundOffRequired)
                strTotalNetAmount = strTotalNetAmount.PadRight(iGap) + PrintFormatCurrency(decTotalNetAmount, DefaultCurrency, SORoundOff) + vbCrLf

                Dim strAdvancedPayed As String = ""
                Dim decAdvancedPayed As Decimal = Decimal.Zero

                Dim dtAdvancePayment As DataTable = GetInvoiceAdvancePayment(SiteCode, SalesOrderNo)
                If Not dtAdvancePayment Is Nothing AndAlso dtAdvancePayment.Rows.Count > 0 Then
                    strAdvancedPayed = L40GetAdvancePayment(dtAdvancePayment, decAdvancedPayed)
                End If

                sbPaymentInfo.Append(strAdvancedPayed + vbCrLf)
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
                decStay2Pay = Decimal.Subtract(decTotalNetAmount, decAdvancedPayed)
                decStay2Pay = MyRound(decStay2Pay, _RoundOff, _IsBillRoundOffRequired)
                'strSTAYTOPAY = strSTAYTOPAY.PadRight(iGap) + PrintFormatCurrency(decStay2Pay, DefaultCurrency, SORoundOff)
                strSTAYTOPAY = strSTAYTOPAY.PadRight(strLineL40.Length - PrintFormatCurrency(decStay2Pay, DefaultCurrency, SORoundOff).Length) + PrintFormatCurrency(decStay2Pay, DefaultCurrency, SORoundOff)
                'sbPaymentInfo.Append("----------".PadLeft(iGap + 10) + vbCrLf)
                sbPaymentInfo.Append(strSTAYTOPAY + vbCrLf)

                Dim objTotalPickAmt As Object
                If _dtSalesItemDetails.Columns.Contains("IsStatus") Then
                    objTotalPickAmt = _dtSalesItemDetails.Compute("sum(TotalPickupAmt)", "PickUpQty > 0 or  DeliveredQty>0 and IsStatus <> 'Deleted'")
                Else
                    objTotalPickAmt = _dtSalesItemDetails.Compute("sum(TotalPickupAmt)", "PickUpQty > 0 or  DeliveredQty>0 ")
                End If
                Dim decTotalPickAmt As Decimal
                If Not objTotalPickAmt Is DBNull.Value AndAlso Not objTotalPickAmt Is Nothing Then
                    decTotalPickAmt = CDbl(objTotalPickAmt)
                End If
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

          
                'strOpenAmount = "OPEN AMOUNT (PAID - PICKED UP)"
                strOpenAmount = getValueByKey("CLSPSO018")
                strOpenAmount = strOpenAmount.PadRight(iGap)
                strvalue = PrintFormatCurrency(MyRound(decTotalOpenAmount, SORoundOff, _IsBillRoundOffRequired), DefaultCurrency, SORoundOff)
                'PadingString(strvalue)
                strvalue = strvalue.PadLeft(strLineL40.Length)

                strOpenAmount = strOpenAmount  ' Decimal.Zero.ToString + DefaultCurrency.PadLeft(2)
               
                Dim sbPaymentSummary As New StringBuilder
                If Not PaymentDetails Is Nothing AndAlso PaymentDetails.Rows.Count > 0 Then
                    Dim strReceiptCode As String = ""
                    Dim strAmount As String = ""
                    Dim strCurrencyCode As String = ""
                    Dim strNumber As String = ""
                    Dim FinalString As String = ""
                    For Each drPayd As DataRow In PaymentDetails.Rows
                        ValidateDataRow(drPayd("RecieptType"), strReceiptCode)

                        strReceiptCode = strReceiptCode.PadRight(iGap)
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


                        'strAmount = strAmount.PadRight(16)
                        'strAmount = strAmount.PadLeft(16)

                        strAmount = PrintFormatCurrency(strAmount, strCurrencyCode, SORoundOff)
                        FinalString = strReceiptCode + ":" + strAmount.PadLeft(strLineL40.Length - (strReceiptCode + ":").Length)

                        'sbPaymentSummary.Append(SplitString(vbCrLf + FinalString).ToString())
                        sbPaymentSummary.Append(vbCrLf + FinalString.ToString())
                        FinalString = ""

                    Next
                    'sbPaymentSummary.Append("----------".PadLeft(iGap + 10))
                    'Dim strBalance2Pay As String = vbCrLf & "BALANCE TO PAY"
                    Dim strBalance2Pay As String = vbCrLf & getValueByKey("CLSPSO034")
                    Dim decBalance2Pay As Decimal = Decimal.Subtract(decTotalNetAmount, decAdvancedPayed)
                    decBalance2Pay = MyRound(decBalance2Pay, _RoundOff, _IsBillRoundOffRequired)
                    strBalance2Pay = strBalance2Pay.PadRight(iGap) + MyRound(decBalance2Pay, SORoundOff, _IsBillRoundOffRequired).ToString()
                    strBalance2Pay = PrintFormatCurrency(strBalance2Pay, DefaultCurrency, SORoundOff)
                    sbPaymentSummary.Append(vbCrLf + strBalance2Pay + vbCrLf)
                    sbPaymentInfo.Append(sbPaymentSummary.ToString())
                End If
            End If
            sbPaymentInfo.Append(vbCrLf)
            sbPaymentInfo.Append(strOpenAmount & vbCrLf)
            sbPaymentInfo.Append(strvalue.PadLeft(strLineL40.Length))

            strPaymentInfo = sbPaymentInfo.ToString()


            'ElseIf PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then

            'End If


        Catch ex As Exception

        End Try
    End Function

    Public Function L40GetItemInfo(ByRef strItemHeader As String, ByRef strItemSales As String) As Boolean
        Try
            If PrintSOTransaction = PrintSOTransactionSet.GiftVoucherDocumentPrint Then
                Dim sbHdrItem As New StringBuilder()
                Dim strHdrItem As String = ""
                'strHdrItem = "Item"
                'strHdrItem = strHdrItem.PadRight(WArticleCode) + "Description"
                strHdrItem = "Item"
                strHdrItem = strHdrItem.PadRight(WArticleCode) & getValueByKey("CLSPSO021")
                sbHdrItem.Append(strHdrItem + vbCrLf)

                Dim strHdrItemQty As String = ""
                'strHdrItemQty = "Qty"
                strHdrItemQty = "  " & getValueByKey("CLSPSO022")

                sbHdrItem.Append(strHdrItemQty)

                'Dim strHdrItemDisc As String = ""
                'strHdrItemDisc = strHdrItemDisc.PadLeft(4) + "Disc"
                'strHdrItemDisc = strHdrItemDisc.PadRight(12) + "Tax%"
                'sbHdrItem.Append(strHdrItemDisc)

                Dim strHdrItemNet As String = ""
                'strHdrItemNet = strHdrItemNet.PadRight(17) + "Pick up"

                If (_isQuotationPrint) Then
                    strHdrItemNet = strHdrItemNet.PadRight(17)
                Else
                    strHdrItemNet = strHdrItemNet.PadRight(17) + getValueByKey("CLSPSO027")
                End If

                sbHdrItem.Append(strHdrItemNet)

                Dim strHdrItemRes As String = ""
                'strHdrItemRes = strHdrItemRes.PadLeft(4) + "Res"
                strHdrItemRes = strHdrItemRes.PadLeft(4) + getValueByKey("CLSPSO028")
                strHdrItemRes = strHdrItemRes.PadRight(4)
                sbHdrItem.Append(strHdrItemRes)

                'Dim strHdrItemCLP As String = ""
                'strHdrItemCLP = strHdrItemCLP.PadLeft(2) + "CLP"
                'strHdrItemCLP = strHdrItemCLP.PadRight(4)
                'sbHdrItem.Append(strHdrItemCLP) 
                'Return header 
                strItemHeader = sbHdrItem.ToString()
            ElseIf PrintSOTransaction = PrintSOTransactionSet.SOReturnStatus Then

                Dim sbHdrItem As New StringBuilder()
                Dim strHdrItem As String = ""

                'strHdrItem = "Item"
                strHdrItem = strHdrItem.PadRight(WArticleCode) & vbCrLf & getValueByKey("CLSPSO021")
                sbHdrItem.Append(strHdrItem + vbCrLf)

                Dim strHdrItemQty As String = ""
                'strHdrItemQty = "Qty"
                'strHdrItemQty = strHdrItemQty.PadRight(12) + "Price"
                strHdrItemQty = getValueByKey("CLSPSO022")
                strHdrItemQty = strHdrItemQty.PadRight(12) + getValueByKey("CLSPSO023")
                sbHdrItem.Append(strHdrItemQty)

                'Dim strHdrItemDisc As String = ""
                'strHdrItemDisc = strHdrItemDisc.PadLeft(4) + "Disc"
                'strHdrItemDisc = strHdrItemDisc.PadRight(12) + "Tax%"
                'sbHdrItem.Append(strHdrItemDisc)
                Dim strHdrItemTax As String = ""

                Dim strHdrItemNet As String = ""
                'strHdrItemNet = strHdrItemNet.PadLeft(10) + "Tax"
                'strHdrItemNet = strHdrItemNet.PadRight(17) + "Net"
                strHdrItemTax = strHdrItemTax.PadRight(4) + getValueByKey("CLSPSO025")
                strHdrItemNet = strHdrItemNet.PadLeft(10) + getValueByKey("CLSPSO026")

                sbHdrItem.Append(strHdrItemTax)
                sbHdrItem.Append(strHdrItemNet)


                strItemHeader = sbHdrItem.ToString()
            Else
                Dim sbHdrItem As New StringBuilder()
                Dim strHdrItem As String = ""
                'strHdrItem = "Item"
                'strHdrItem = strHdrItem.PadRight(WArticleCode) + "Description"
                '---Changes Done By Mahesh ()
                'strHdrItem = "Item"
                'strHdrItem = strHdrItem.PadRight(WArticleCode) & vbCrLf & getValueByKey("CLSPSO021")
                strHdrItem = getValueByKey("CLSPSO021")
                sbHdrItem.Append(strHdrItem + vbCrLf)

                Dim strHdrItemQty As String = ""
                'strHdrItemQty = "Qty"
                'strHdrItemQty = strHdrItemQty.PadRight(12) + "Price"
                strHdrItemQty = getValueByKey("CLSPSO022").PadRight(1)
                'strHdrItemQty = strHdrItemQty.PadRight(12) + getValueByKey("CLSPSO023")
                strHdrItemQty = strHdrItemQty.PadRight(8) + getValueByKey("CLSPSO023").PadLeft(7) + getValueByKey("CLSPSO025").PadLeft(7)
                sbHdrItem.Append(strHdrItemQty)

                'Dim strHdrItemDisc As String = ""
                'strHdrItemDisc = strHdrItemDisc.PadLeft(4) + "Disc"
                'strHdrItemDisc = strHdrItemDisc.PadRight(12) + "Tax%"
                'sbHdrItem.Append(strHdrItemDisc)

                Dim strHdrItemNet As String = ""
                'strHdrItemNet = strHdrItemNet.PadLeft(10) + "Net"
                'strHdrItemNet = strHdrItemNet.PadRight(17) + "Pick up"
                'strHdrItemNet = strHdrItemNet.PadLeft(8) + getValueByKey("CLSPSO026")
                strHdrItemNet = getValueByKey("CLSPSO026")

                If (_isQuotationPrint) Then
                    'strHdrItemNet = strHdrItemNet.PadRight(16)
                    strHdrItemNet = strHdrItemNet.PadLeft(8)
                Else
                    'strHdrItemNet = strHdrItemNet.PadRight(16) + getValueByKey("CLSPSO027")
                    strHdrItemNet = strHdrItemNet.PadLeft(8) + getValueByKey("CLSPSO027").PadLeft(9)
                End If

                sbHdrItem.Append(strHdrItemNet)

                'Dim strHdrItemRes As String = ""
                'strHdrItemRes = strHdrItemRes.PadLeft(4) + "Res"
                'strHdrItemRes = strHdrItemRes.PadRight(4)
                'sbHdrItem.Append(strHdrItemRes)

                'Dim strHdrItemCLP As String = ""
                'strHdrItemCLP = strHdrItemCLP.PadLeft(2) + "CLP"
                'strHdrItemCLP = strHdrItemCLP.PadRight(4)
                'sbHdrItem.Append(strHdrItemCLP)


                'Return header 
                strItemHeader = sbHdrItem.ToString()
            End If



            Dim sbHdrItemInfo As New StringBuilder
            Dim strArticleCode As String = ""
            Dim strArticleDesc As String = ""
            Dim iPurchasedQty As Double = 0
            Dim strPurchasedQty As String = ""
            Dim iPrice As String = ""
            Dim iNetAmount As String = ""
            Dim strReservedQty As String = ""
            Dim strCLP As String = ""
            Dim itemCount As Integer = 1
            'Dim strPickUp As String = ""
            Dim strPickUp As Decimal = 0.0
            Dim strDisc As String = ""
            Dim strTax As String = ""
            Dim sbArticleInfo As New StringBuilder
            Dim decTotalPickupQty As Decimal
            Dim decPickupQty As Decimal
            Dim decDeliveredQty As Decimal
            Dim decTax As Decimal = 0.0

            If PrintSOTransaction = PrintSOTransactionSet.Status Then
                For Each drGrid As DataRow In _dtSalesItemDetails.Rows
                    If Not (drGrid.RowState = DataRowState.Deleted) Then
                        If Not drGrid("IsStatus") Is DBNull.Value Then
                            If drGrid("IsStatus") = "Deleted" Then
                                Continue For
                            End If
                        End If
                        '--- ValidateDataRow(drGrid("ArticleCode"), strArticleCode)
                        '--- strArticleCode = strArticleCode.PadRight(WArticleCode)

                        If (DisplayArticleFullName) Then
                            ValidateDataRow(drGrid("ArticleDiscription"), strArticleDesc)
                        Else
                            ValidateDataRow(drGrid("Discription"), strArticleDesc)
                        End If

                        strArticleDesc = strArticleDesc.PadRight(30)
                        strArticleDesc = strArticleDesc

                        'sbArticleInfo = SplitString(strArticleCode + strArticleDesc)
                        'comment to bring the desciptin in second line
                        sbArticleInfo = SplitString(strArticleCode & vbCrLf)
                        sbArticleInfo = sbArticleInfo.Append(SplitString(strArticleDesc & vbCrLf).ToString())


                        If DtSoBulkComboHdr IsNot Nothing AndAlso DtSoBulkComboHdr.Rows.Count > 0 AndAlso Not IsDBNull(drGrid("BillLineNo")) Then
                            Dim HdrRowFilter As String
                            'If _dtSalesItemDetails.Columns.Contains("rowindex") Then
                            '    HdrRowFilter = " ComboSrNo =" & drGrid("rowindex")
                            'Else
                            HdrRowFilter = " ComboSrNo =" & drGrid("BillLineNo")
                            ' End If


                            Dim dr() = DtSoBulkComboHdr.Select(HdrRowFilter)
                            If dr.Count > 0 Then
                                Dim BulkComboMstId As Int64 = dr(0)("BulkComboMstId")
                                Dim DtlRowFilter As String = "BulkComboMstId=" & BulkComboMstId
                                Dim dvresult As New DataView(DtSoBulkComboDtl, DtlRowFilter, "", DataViewRowState.CurrentRows)
                                If dvresult.ToTable.Rows.Count > 0 Then
                                    Dim articleDescriptionDictionary As New Dictionary(Of String, String)
                                    articleDescriptionDictionary.Add(sbArticleInfo.ToString, 0)
                                    GetArticleDecriptionDictionary(articleDescriptionDictionary, dvresult.ToTable)
                                    sbArticleInfo.Length = 0
                                    sbArticleInfo.Append(GetMultilinedString(articleDescriptionDictionary))
                                End If
                            End If
                        End If

                        ValidateDataRow(drGrid("Quantity"), strPurchasedQty)

                        strPurchasedQty = strPurchasedQty.ToString()
                        strPurchasedQty = strPurchasedQty.PadLeft(1)
                        strPurchasedQty = strPurchasedQty.PadRight(8)

                        ValidateDataRow(drGrid("SellingPrice"), iPrice)
                        '---Changes By Mahesh as disscussed with manish mail Dated 08-10-2014
                        'iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, SORoundOff)
                        iPrice = FormatNumber(iPrice, DecimalDigits, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
                        'iPrice = iPrice.PadLeft(10)
                        'iPrice = iPrice.PadRight(10)
                        iPrice = iPrice.PadLeft(7)

                        ValidateDataRow(drGrid("totaldiscpercentage"), strDisc)
                        'strDisc = PrintFormatCurrency(strDisc, DefaultCurrency, SORoundOff)
                        If strDisc <> String.Empty Then
                            strDisc = FormatNumber(strDisc, 2)
                            strDisc = strDisc & " %"
                        End If

                        strDisc = strDisc.PadLeft(10)
                        strDisc = strDisc.PadRight(10)


                        'ValidateDataRow(drGrid("TotalTaxAmt"), strTax)

                        'strTax = PrintFormatCurrency(strTax, DefaultCurrency, SORoundOff)
                        'If strTax = Nothing Then
                        '    strTax = " "
                        'End If
                        'strTax = strTax.PadLeft(5)
                        'strTax = strTax.PadRight(5)
                        ValidateDataRow(drGrid("TotalTaxAmt"), decTax)

                        strTax = FormatNumber(decTax, DecimalDigits, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
                        strTax = strTax.PadLeft(7)
                        'strTax = strTax.PadRight(7)

                        ValidateDataRow(drGrid("NetAmount"), iNetAmount)
                        'iNetAmount = PrintFormatCurrency(iNetAmount, DefaultCurrency, SORoundOff)
                        iNetAmount = FormatNumber(iNetAmount, DecimalDigits, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
                        iNetAmount = iNetAmount.PadLeft(10)
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
                        strPickUp = decTotalPickupQty.ToString()
                        'strPickUp = strPickUp.ToString().PadLeft(8)
                        'strPickUp = strPickUp.ToString().PadRight(8)

                        ValidateDataRow(drGrid("ReservedQty"), strReservedQty)

                        strReservedQty = strReservedQty.PadLeft(4)
                        strReservedQty = strReservedQty.PadRight(4)

                        ValidateDataRow(drGrid("IsCLP"), strCLP)
                        strCLP = strCLP.PadRight(4)

                        If (_isQuotationPrint) Then
                            'sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(strPurchasedQty.ToString() & iPrice & iNetAmount).ToString() + vbCrLf)
                            sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(strPurchasedQty.ToString() & iPrice & strTax & iNetAmount).ToString() + vbCrLf)
                        Else
                            If (MakeHtml(strPurchasedQty, iPrice, strTax, iNetAmount, strPickUp)) Then
                                sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(strPurchasedQty.ToString() & iPrice & strTax & iNetAmount & strPickUp.ToString().PadLeft(7)).ToString() + vbCrLf)
                            Else
                                sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(strPurchasedQty.ToString() & iPrice & strTax & iNetAmount).ToString & vbCrLf & SplitString(strPickUp.ToString().PadLeft(strLineL40.Length)).ToString() + vbCrLf)
                            End If
                        End If

                        itemCount += 1
                    End If
                Next
                L40GetOtherCharges(strArticleCode, strArticleDesc, strPurchasedQty, iPrice, strDisc, strTax, iNetAmount, sbHdrItemInfo)
            ElseIf PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Or PrintSOTransaction = PrintSOTransactionSet.Payment Then
                Dim strFilterCondition As String = ""
                If PrintSOTransaction = PrintSOTransactionSet.Payment Then
                    strFilterCondition = ""
                Else
                    strFilterCondition = "PickUpQty > 0"
                End If
                For Each drGrid As DataRow In _dtSalesItemDetails.Select(strFilterCondition)
                    If Not (drGrid.RowState = DataRowState.Deleted) Then
                        If _dtSalesItemDetails.Columns.Contains("IsStatus") AndAlso Not drGrid("IsStatus") Is DBNull.Value Then
                            If drGrid("IsStatus") = "Deleted" Then
                                Continue For
                            End If
                        End If

                        ValidateDataRow(drGrid("ArticleCode"), strArticleCode)
                        strArticleCode = strArticleCode.PadRight(26)

                        If (DisplayArticleFullName) Then
                            ValidateDataRow(drGrid("ArticleDiscription"), strArticleDesc)
                        Else
                            ValidateDataRow(drGrid("Discription"), strArticleDesc)
                        End If

                        strArticleDesc = strArticleDesc.PadRight(33)

                        If DtSoBulkComboHdr IsNot Nothing AndAlso DtSoBulkComboHdr.Rows.Count > 0 AndAlso Not IsDBNull(drGrid("BillLineNo")) Then

                            Dim HdrRowFilter As String = " ComboSrNo =" & drGrid("BillLineNo")

                            Dim dr() = DtSoBulkComboHdr.Select(HdrRowFilter)
                            If dr.Count > 0 Then
                                Dim BulkComboMstId As Int64 = dr(0)("BulkComboMstId")
                                Dim DtlRowFilter As String = "BulkComboMstId=" & BulkComboMstId
                                Dim dvresult As New DataView(DtSoBulkComboDtl, DtlRowFilter, "", DataViewRowState.CurrentRows)
                                If dvresult.ToTable.Rows.Count > 0 Then
                                    Dim articleDescriptionDictionary As New Dictionary(Of String, String)
                                    articleDescriptionDictionary.Add(strArticleDesc.ToString, 0)
                                    GetArticleDecriptionDictionary(articleDescriptionDictionary, dvresult.ToTable)

                                    strArticleDesc = GetMultilinedString(articleDescriptionDictionary)
                                End If
                            End If
                        End If


                        If PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                            ValidateDataRow(drGrid("PickUpQty"), strPurchasedQty)

                        Else
                            ValidateDataRow(drGrid("Quantity"), strPurchasedQty)
                        End If
                        strPurchasedQty = strPurchasedQty.ToString()
                        strPurchasedQty = strPurchasedQty.PadLeft(1)
                        strPurchasedQty = strPurchasedQty.PadRight(8)

                        'sbArticleInfo = SplitString(strArticleCode + strArticleDesc)
                        'comment to bring the desciptin in second line
                        '---Changes Done By Mahesh ()
                        'sbArticleInfo = SplitString(strArticleCode & vbCrLf)
                        '----Change by Mahesh because its showing problem for bulk combo 
                        '    sbArticleInfo = sbArticleInfo.Append(SplitString(strArticleDesc & vbCrLf).ToString())

                        'sbArticleInfo = SplitString(strArticleCode & vbCrLf)
                        sbArticleInfo.Length = 0
                        sbArticleInfo = sbArticleInfo.Append(strArticleDesc & vbCrLf.ToString())

                        ValidateDataRow(drGrid("SellingPrice"), iPrice)

                        'iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, SORoundOff)
                        iPrice = FormatNumber(iPrice, DecimalDigits, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
                        iPrice = iPrice.PadLeft(7)
                        'iPrice = iPrice.PadRight(10)

                        ValidateDataRow(drGrid("totaldiscpercentage"), strDisc)

                        'strDisc = PrintFormatCurrency(strDisc, DefaultCurrency, SORoundOff)
                        If strDisc <> String.Empty Then
                            strDisc = FormatNumber(strDisc, 2)
                            strDisc = strDisc & " %"
                        End If

                        strDisc = strDisc.PadLeft(10)
                        strDisc = strDisc.PadRight(10)

                        ValidateDataRow(drGrid("TotalTaxAmt"), strTax)
                        ValidateDataRow(drGrid("ExclTaxAmt"), decTax)


                        If PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                            ValidateDataRow(drGrid("TotalTaxAmt"), decTax)
                            decTax = (decTax / drGrid("Quantity")) * drGrid("PickUpQty")
                            strTax = FormatNumber(decTax, DecimalDigits, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
                            strTax = strTax.PadLeft(7)


                            ValidateDataRow(drGrid("ExclTaxAmt"), decTax)
                        Else
                            ValidateDataRow(drGrid("TotalTaxAmt"), decTax)

                            strTax = PrintFormatCurrency(decTax, DefaultCurrency, SORoundOff)
                            strTax = strTax.PadLeft(7)


                            ValidateDataRow(drGrid("ExclTaxAmt"), decTax)
                        End If


                        ValidateDataRow(drGrid("PickUpQty"), strPickUp)

                        If Not drGrid("PickUpQty") Is DBNull.Value Then
                            decPickupQty = 0
                            decDeliveredQty = 0
                            decPickupQty = drGrid("PickUpQty")
                            If Not drGrid("DeliveredQty") Is DBNull.Value Then
                                decDeliveredQty = drGrid("DeliveredQty")
                            End If
                            decTotalPickupQty = decPickupQty + decDeliveredQty
                        End If
                        strPickUp = decTotalPickupQty.ToString()

                        'strPickUp = strPickUp.ToString().PadLeft(8)
                        'strPickUp = strPickUp.ToString().PadRight(8)

                        ValidateDataRow(drGrid("ReservedQty"), strReservedQty)
                        strReservedQty = strReservedQty.PadLeft(4)
                        strReservedQty = strReservedQty.PadRight(4)



                        If PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                            'Dim decPrice As Decimal = 0
                            'ValidateDataRow(drGrid("SellingPrice"), decPrice)
                            ''Dim iPickupQty As Double = 0
                            Dim iPickupQty As Decimal = 0
                            'ValidateDataRow(drGrid("PickUpQty"), iPickupQty)
                            'iNetAmount = decPrice * iPickupQty
                            'iNetAmount = Decimal.Add(iNetAmount, strTax)
                            Dim PricePerUnit As Decimal = 0

                            ' PricePerUnit = drGrid("SellingPrice") / drGrid("Quantity")
                            ' ValidateDataRow(drGrid("PickUpQty"), iPickupQty)
                            'iNetAmount = (PricePerUnit * iPickupQty) - IIf(drGrid("Discount") Is DBNull.Value, 0, drGrid("Discount"))
                            PricePerUnit = drGrid("SellingPrice") ' drGrid("Quantity")
                            ValidateDataRow(drGrid("PickUpQty"), iPickupQty)
                            iNetAmount = (PricePerUnit * iPickupQty) - IIf(drGrid("Discount") Is DBNull.Value, 0, (drGrid("Discount") / drGrid("Quantity")) * drGrid("PickUpQty"))
                            iNetAmount = Decimal.Add(iNetAmount, strTax)

                        Else
                            ValidateDataRow(drGrid("NetAmount"), iNetAmount)
                            iNetAmount = Decimal.Add(iNetAmount, decTax)
                        End If
                        iNetAmount = FormatNumber(iNetAmount, DecimalDigits, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
                        iNetAmount = iNetAmount.PadLeft(10)
                        'iNetAmount = iNetAmount.PadRight(12)

                        ValidateDataRow(drGrid("IsCLP"), strCLP)
                        strCLP = strCLP.PadRight(4)
                        If PrintSOTransaction = PrintSOTransactionSet.DeliveryNote Then
                            If (MakeHtml(strPurchasedQty, iPrice, strTax, iNetAmount, strPickUp)) Then
                                sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(strPurchasedQty.ToString() & iPrice & strTax & iNetAmount & strPickUp.ToString().PadLeft(7)).ToString() + vbCrLf)
                            Else
                                sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(strPurchasedQty.ToString() & iPrice & strTax & iNetAmount).ToString & vbCrLf & SplitString(strPickUp.ToString().PadLeft(strLineL40.Length)).ToString() + vbCrLf)
                            End If

                        ElseIf PrintSOTransaction = PrintSOTransactionSet.Payment Then
                            'sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(strPurchasedQty.ToString() & iPrice & iNetAmount & strPickUp.ToString().PadLeft(8)).ToString() + vbCrLf)
                            If (MakeHtml(strPurchasedQty, iPrice, strTax, iNetAmount, strPickUp)) Then
                                sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(strPurchasedQty.ToString() & iPrice & strTax & iNetAmount & strPickUp.ToString().PadLeft(7)).ToString() + vbCrLf)
                            Else
                                sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(strPurchasedQty.ToString() & iPrice & strTax & iNetAmount).ToString & vbCrLf & SplitString(strPickUp.ToString().PadLeft(strLineL40.Length)).ToString() + vbCrLf)
                            End If
                        Else
                            sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(strPurchasedQty.ToString() & iPrice & iNetAmount & strReservedQty & strPickUp.ToString().PadLeft(8)).ToString() + vbCrLf)

                        End If
                        itemCount += 1
                    End If
                Next
                If PrintSOTransaction = PrintSOTransactionSet.Payment Then
                    L40GetOtherCharges(strArticleCode, strArticleDesc, strPurchasedQty, iPrice, strDisc, strTax, iNetAmount, sbHdrItemInfo)
                End If
            ElseIf PrintSOTransaction = PrintSOTransactionSet.GiftVoucherDocumentPrint Then
                For Each drGrid As DataRow In _dtSalesItemDetails.Rows
                    If Not (drGrid.RowState = DataRowState.Deleted) Then
                        If Not drGrid("IsStatus") Is DBNull.Value Then
                            If drGrid("IsStatus") = "Deleted" Then
                                Continue For
                            End If
                        End If

                        ValidateDataRow(drGrid("ArticleCode"), strArticleCode)
                        strArticleCode = strArticleCode.PadRight(26)

                        If (DisplayArticleFullName) Then
                            ValidateDataRow(drGrid("ArticleDiscription"), strArticleDesc)
                        Else
                            ValidateDataRow(drGrid("Discription"), strArticleDesc)
                        End If

                        strArticleDesc = strArticleDesc.PadRight(33)

                        ValidateDataRow(drGrid("Quantity"), strPurchasedQty)
                        strPurchasedQty = strPurchasedQty.ToString()

                        strPurchasedQty = strPurchasedQty.PadRight(8)

                        'sbArticleInfo = SplitString(strArticleCode + strArticleDesc)
                        'comment to bring the desciptin in second line
                        'sbArticleInfo = SplitString(strArticleCode & vbCrLf)
                        'sbArticleInfo = sbArticleInfo.Append(SplitString(strArticleDesc & vbCrLf).ToString())

                        sbArticleInfo.Length = 0
                        sbArticleInfo = sbArticleInfo.Append(vbCrLf & SplitString(strArticleDesc & vbCrLf).ToString())

                        ValidateDataRow(drGrid("PickUpQty"), strPickUp)


                        strPickUp = strPickUp.ToString().PadLeft(8)
                        strPickUp = strPickUp.ToString().PadRight(8)

                        ValidateDataRow(drGrid("ReservedQty"), strReservedQty)
                        strReservedQty = strReservedQty.PadLeft(8)
                        strReservedQty = strReservedQty.PadRight(8)
                        sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(strPurchasedQty.ToString() & strPickUp.ToString().PadLeft(8) & strReservedQty).ToString() + vbCrLf)
                        itemCount += 1
                    End If
                Next
            ElseIf PrintSOTransaction = PrintSOTransactionSet.SOReturnStatus Then
                Dim strFilterCondition As String = ""
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

                        ValidateDataRow(drGrid("ArticleCode"), strArticleCode)

                        strArticleCode = strArticleCode.PadRight(26)

                        If (DisplayArticleFullName) Then
                            ValidateDataRow(drGrid("ArticleDiscription"), strArticleDesc)
                        Else
                            ValidateDataRow(drGrid("Discription"), strArticleDesc)
                        End If

                        strArticleDesc = strArticleDesc.PadRight(33)
                        strPurchasedQty = strPurchasedQty.ToString()
                        strPurchasedQty = strPurchasedQty.PadLeft(4)
                        strPurchasedQty = strPurchasedQty.PadRight(8)


                        'sbArticleInfo = SplitString(strArticleCode & vbCrLf)
                        sbArticleInfo = sbArticleInfo.Append(vbCrLf & SplitString(strArticleDesc & vbCrLf).ToString())




                        ValidateDataRow(drGrid("SellingPrice"), iPrice)

                        'iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, SORoundOff)
                        iPrice = FormatNumber(iPrice, DecimalDigits, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
                        iPrice = iPrice.PadLeft(10)
                        iPrice = iPrice.PadRight(10)


                        ValidateDataRow(drGrid("TotalTaxAmt"), strTax)


                        'strTax = PrintFormatCurrency(strTax, DefaultCurrency, SORoundOff)
                        strTax = FormatNumber(strTax, DecimalDigits, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
                        strTax = strTax.PadLeft(8)
                        strTax = strTax.PadRight(8)

                        ValidateDataRow(drGrid("PickUpQty"), strPickUp)
                        strPickUp = strPickUp.ToString()
                        strPickUp = strPickUp.ToString().PadRight(8)
                        Dim decPrice As Decimal = 0
                        ValidateDataRow(drGrid("SellingPrice"), decPrice)
                        Dim iPickupQty As Double = 0
                        ValidateDataRow(drGrid("PickUpQty"), iPickupQty)
                        iNetAmount = decPrice * iPickupQty


                        iNetAmount = FormatNumber(iNetAmount, DecimalDigits, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
                        iNetAmount = iNetAmount.PadLeft(12)
                        iNetAmount = iNetAmount.PadRight(12)


                        sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(strPickUp.ToString().PadRight(8) & iPrice & strTax & iNetAmount).ToString() + vbCrLf)

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


    Private Sub GetArticleDecriptionDictionary(ByRef articleDescriptionDictionary As Dictionary(Of String, String), ByRef dataTable As DataTable)
        Try
            For Each Row In dataTable.Rows
                Dim Qty As String = ""
                If Row("PackagedUOM").ToString() = Row("BaseUOM").ToString() Then
                    Qty = Row("ItemQtyBaseUOM").ToString()
                Else
                    Qty = Row("Qty").ToString() & "," & Row("PackagedUOM").ToString()
                End If
                If articleDescriptionDictionary.ContainsKey(Row("ARTICLEDESCRIPTION")) Then
                    articleDescriptionDictionary(Row("ARTICLEDESCRIPTION")) = articleDescriptionDictionary(Row("ARTICLEDESCRIPTION")) + Qty ' Row("ItemQtyBaseUOM")
                Else
                    articleDescriptionDictionary.Add(Row("ARTICLEDESCRIPTION"), Qty) 'Row("ItemQtyBaseUOM")
                End If
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Function GetMultilinedString(ByRef descriptionDictionary As Dictionary(Of String, String)) As String
        Try
            Dim multilinedString As String = String.Empty
            Dim count As Integer = 1
            For Each keyValue In descriptionDictionary
                Dim tempKeyArray As Array
                tempKeyArray = keyValue.Value.Split(",")
                If tempKeyArray.Length > 0 Then

                    If tempKeyArray(0) <> 0 Then
                        'If keyValue.Value <> 0 Then
                        If count = descriptionDictionary.Count Then
                            If Not PrintSOPageSetup = "L40" Then
                                multilinedString += ("   *" & keyValue.Key & " - " & keyValue.Value.ToString().Replace(",", " ") & "").PadRight(51)
                            Else
                                multilinedString += "   *" & keyValue.Key & " - " & keyValue.Value.ToString().Replace(",", " ") & vbCrLf
                            End If
                        Else
                            multilinedString += ("   *" & keyValue.Key & " - " & keyValue.Value.ToString().Replace(",", " ") & vbCrLf)
                        End If
                    Else
                        If count = descriptionDictionary.Count Then
                            If Not PrintSOPageSetup = "L40" Then
                                multilinedString += keyValue.Key.PadRight(51)
                            Else
                                multilinedString += keyValue.Key & vbCrLf
                            End If
                        Else
                            multilinedString += keyValue.Key & vbCrLf
                        End If
                    End If
                End If
                count = count + 1
            Next
            multilinedString = multilinedString.Remove(multilinedString.Count - 2, 1)
            Return multilinedString
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function

    'Public Function GetMultilinedString(ByRef descriptionDictionary As Dictionary(Of String, Double)) As String
    '    Try
    '        Dim multilinedString As String = String.Empty
    '        Dim count As Integer = 1
    '        For Each keyValue In descriptionDictionary
    '            If keyValue.Value <> 0 Then
    '                If count = descriptionDictionary.Count Then
    '                    If Not PrintSOPageSetup = "L40" Then
    '                        multilinedString += ("   *" & keyValue.Key & " - " & keyValue.Value & "").PadRight(51)
    '                    Else
    '                        multilinedString += "   *" & keyValue.Key & " - " & keyValue.Value & vbCrLf
    '                    End If
    '                Else
    '                    multilinedString += ("   *" & keyValue.Key & " - " & keyValue.Value & vbCrLf)
    '                End If
    '            Else
    '                If count = descriptionDictionary.Count Then
    '                    If Not PrintSOPageSetup = "L40" Then
    '                        multilinedString += keyValue.Key.PadRight(51)
    '                    Else
    '                        multilinedString += keyValue.Key & vbCrLf
    '                    End If
    '                Else
    '                    multilinedString += keyValue.Key & vbCrLf
    '                End If
    '            End If
    '            count = count + 1
    '        Next
    '        multilinedString = multilinedString.Remove(multilinedString.Count - 2, 1)
    '        Return multilinedString
    '    Catch ex As Exception
    '        LogException(ex)
    '        Return String.Empty
    '    End Try
    'End Function
    Private Function L40GetOtherCharges(ByVal strArticleCode As String, ByVal strArticleDesc As String, ByVal strPurchasedQty As String, ByVal iPrice As String, ByVal strDisc As String, ByVal strTax As String, ByVal iNetAmount As String, ByRef sbHdrItemInfo As StringBuilder) As Boolean
        Try
            If Not _dsOtherCost Is Nothing AndAlso _dsOtherCost.Tables.Count > 0 Then
                Dim strFilter As String = ""
                Dim objNetAmount As Object
                For Each dt As DataTable In _dsOtherCost.Tables

                    If PrintSOTransaction = PrintSOTransactionSet.Status Or PrintSOTransaction = PrintSOTransactionSet.Payment Then
                        'If PrintSOTransaction = PrintSOTransactionSet.Payment Then
                        '    If _dsOtherCost.Tables("NewOtherCharges").Columns.Contains("Status") Then
                        '        strFilter = "Status is null"
                        '    End If

                        'End If
                        For Each drGrid As DataRow In _dsOtherCost.Tables("NewOtherCharges").Select(strFilter)
                            Dim sbArticleInfo As New StringBuilder
                            If Not (drGrid.RowState = DataRowState.Deleted) Then

                                'strArticleCode = "Add.Cost"
                                'strArticleCode = getValueByKey("CLSPSO030")
                                strArticleCode = vbCrLf & getValueByKey("CLSPSO030") & " : "
                                strArticleCode = strArticleCode.PadRight(WArticleCode)
                                If Not drGrid("ChargeName") Is Nothing AndAlso Not drGrid("ChargeName") Is DBNull.Value Then
                                    strArticleDesc = drGrid("ChargeName")
                                End If

                                'strArticleDesc = strArticleDesc.PadRight(30)
                                strArticleDesc = strArticleDesc
                                sbArticleInfo = SplitString(strArticleCode + strArticleDesc)

                                strPurchasedQty = "1"
                                strPurchasedQty = strPurchasedQty.ToString()
                                strPurchasedQty = strPurchasedQty.PadLeft(1)
                                strPurchasedQty = strPurchasedQty.PadRight(8)

                                If Not drGrid("ChargeAmount") Is Nothing AndAlso Not drGrid("ChargeAmount") Is DBNull.Value Then
                                    iPrice = drGrid("ChargeAmount")
                                End If


                                iPrice = FormatNumber(iPrice, DecimalDigits, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
                                iPrice = iPrice.PadLeft(7)
                                'iPrice = iPrice.PadRight(10)

                                strDisc = Decimal.Zero
                                strDisc = FormatNumber(strDisc, DecimalDigits, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
                                strDisc = strDisc.PadLeft(10)
                                strDisc = strDisc.PadRight(10)
                                If Not drGrid("TaxAmt") Is Nothing AndAlso Not drGrid("TaxAmt") Is DBNull.Value Then
                                    strTax = drGrid("TaxAmt")
                                Else
                                    strTax = Decimal.Zero
                                End If


                                strTax = FormatNumber(strTax, DecimalDigits, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
                                strTax = strTax.PadLeft(7)
                                'strTax = strTax.PadRight(8)

                                'If Not drGrid("ChargeAmount") Is Nothing AndAlso Not drGrid("ChargeAmount") Is DBNull.Value Then
                                '    iNetAmount = drGrid("ChargeAmount")
                                'End If
                                iNetAmount = ""
                                objNetAmount = drGrid("ChargeAmount")
                                If Not objNetAmount Is Nothing AndAlso Not objNetAmount Is DBNull.Value Then
                                    iNetAmount = Decimal.Add(CDbl(objNetAmount), strTax).ToString()
                                Else
                                    iNetAmount = drGrid("ChargeAmount")
                                End If

                                iNetAmount = FormatNumber(iNetAmount, DecimalDigits, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
                                iNetAmount = iNetAmount.PadLeft(10)
                                'iNetAmount = iNetAmount.PadRight(8)


                                sbHdrItemInfo.Append(sbArticleInfo.ToString() & vbCrLf & strPurchasedQty.ToString() & iPrice & strTax & iNetAmount + vbCrLf)

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
    Public Sub SalesComboPrintL40(ByRef errorMsg As String, ByRef printPriview As Boolean, ByVal dtcomboHdr As DataTable, ByVal DtcomboDtl As DataTable, Optional ByVal dtPrinterInfo As DataTable = Nothing)
        Try
            Dim strLineDetail, soComboBillPrintBase, soComboBillPrintArticleMain As New StringBuilder
            Dim StrCMNo, StrTokenNo, StrCustomerName As String
            Dim LineItemHeading, StrSubHeader, StrLineItem As String
            Dim ItemCode, Desc, Qty, Comments As String
            Dim strLine, StrTokenSalesTypeLine, StrTokenTakeAwayLine, QtyUOM As String
            Dim strDblLine, strTitleInfo As String
            Dim LineLength As Integer = 40
            errorMsg = ""
            _printPreview = printPriview
            Dim ArticleName As String = "Article Name"
            Dim UOM As String = "UOM"
            If Not dtcomboHdr Is Nothing Then
                strLine = "-".PadRight(LineLength, "-") & vbCrLf
                strDblLine = "=".PadRight(LineLength, "=") & vbCrLf

                strTitleInfo = String.Format("SALES ORDER : " & dtcomboHdr.Rows(0).Item("SaleOrderNumber").ToString())

                soComboBillPrintBase.Append(strTitleInfo + vbCrLf)
                soComboBillPrintBase.Append("Packaging Box : " & dtcomboHdr.Rows(0).Item("PackagingBox").ToString() + vbCrLf)
                soComboBillPrintBase.Append("Combo Quantity : " & dtcomboHdr.Rows(0).Item("OrderQty").ToString() + vbCrLf)
                Comments = "Additional Comments : " & dtcomboHdr.Rows(0).Item("AdditionComments").ToString()

                For index = 0 To Comments.Length Step LineLength

                    Dim substringlength = 0
                    If Comments.Length - index > LineLength Then
                        substringlength = LineLength
                    Else
                        substringlength = Comments.Length - index
                    End If
                    Dim temp As String = Comments.Substring(index, substringlength)
                    soComboBillPrintBase.Append(temp & vbCrLf)
                Next
                '     soComboBillPrintBase.Append(vbCrLf)
                'If (Comments.Length >= LineLength) Then
                '    Comments = Comments.PadRight(LineLength - 1 - Comments.Length) & Comments & vbCrLf
                'End If

                UOM = UOM & "   " & getValueByKey("CLSPSO022")
                soComboBillPrintBase.Append(strLineL40 + vbCrLf)
                soComboBillPrintBase.Append(ArticleName + UOM.PadLeft(LineLength - ArticleName.Length - 6) + vbCrLf)
                soComboBillPrintBase.Append(strLineL40 + vbCrLf)


                For Each dr As DataRow In DtcomboDtl.Rows
                    ItemCode = dr("ArticleCode").ToString()
                    Desc = dr("ArticleDescription").ToString()

                    If dr("PackagedUOM").ToString() = dr("BaseUOM").ToString() Then
                        Qty = dr("ItemQtyBaseUOM").ToString() & " " & dr("PackagedUOM").ToString()
                    Else
                        Qty = dr("Qty").ToString() & "  " & dr("PackagedUOM").ToString()
                    End If

                    QtyUOM = dr("BaseUOM").ToString() & "  " & Qty

                    strLineDetail.Append(StrLineItem)

                    If (Desc.Length >= LineLength) Or (Desc.Length + QtyUOM.Length >= LineLength) Then
                        soComboBillPrintArticleMain.Append(Desc + vbCrLf)
                        'soComboBillPrintArticleMain.Append(Qty.PadLeft(LineLength - Qty.Length - 2, ".") + vbCrLf)
                        'soComboBillPrintArticleMain.Append(Qty.PadLeft(LineLength - 2, " ") + vbCrLf)
                        soComboBillPrintArticleMain.Append(QtyUOM.PadLeft(LineLength - 2, " ") + vbCrLf)
                    Else
                        soComboBillPrintArticleMain.Append(Desc + QtyUOM.PadLeft(LineLength - Desc.Length - 2) + vbCrLf)
                    End If

                Next


                PrinterName = SetPrinterName(dtPrinterInfo, "SalesOrder", strSODocumentType)
                Call fnBulkComboPrint(soComboBillPrintBase, soComboBillPrintArticleMain, errorMsg)


            Else
                errorMsg = getValueByKey("CLSCMP031")
            End If
        Catch ex As Exception
            errorMsg = ex.Message
        End Try


    End Sub
    Public Sub SalesComboPrintL80(ByRef errorMsg As String, ByRef printPriview As Boolean, ByVal dtcomboHdr As DataTable, ByVal DtcomboDtl As DataTable, Optional ByVal dtPrinterInfo As DataTable = Nothing)
        Try
            Dim strLineDetail, soComboBillPrintBase, soComboBillPrintArticleMain As New StringBuilder
            Dim StrCMNo, StrTokenNo, StrCustomerName As String
            Dim LineItemHeading, StrSubHeader, StrLineItem As String
            Dim ItemCode, Desc, Qty, Comments As String
            Dim strLine, StrTokenSalesTypeLine, StrTokenTakeAwayLine, QtyUOM As String
            Dim strDblLine, strTitleInfo As String
            Dim LineLength As Integer = 80
            errorMsg = ""
            _printPreview = printPriview
            Dim ArticleName As String = "Article Name"
            Dim UOM As String = "UOM"
            If Not dtcomboHdr Is Nothing Then
                strLine = "-".PadRight(LineLength, "-") & vbCrLf
                strDblLine = "=".PadRight(LineLength, "=") & vbCrLf

                strTitleInfo = String.Format("SALES ORDER : " & dtcomboHdr.Rows(0).Item("SaleOrderNumber").ToString())

                soComboBillPrintBase.Append(strTitleInfo + vbCrLf)
                soComboBillPrintBase.Append("Packaging Box : " & dtcomboHdr.Rows(0).Item("PackagingBox").ToString() + vbCrLf)
                soComboBillPrintBase.Append("Combo Quantity : " & dtcomboHdr.Rows(0).Item("OrderQty").ToString() + vbCrLf)
                Comments = "Additional Comments : " & dtcomboHdr.Rows(0).Item("AdditionComments").ToString()

                For index = 0 To Comments.Length Step LineLength

                    Dim substringlength = 0
                    If Comments.Length - index > LineLength Then
                        substringlength = LineLength
                    Else
                        substringlength = Comments.Length - index
                    End If
                    Dim temp As String = Comments.Substring(index, substringlength)
                    soComboBillPrintBase.Append(temp & vbCrLf)
                Next
                '     soComboBillPrintBase.Append(vbCrLf)
                'If (Comments.Length >= LineLength) Then
                '    Comments = Comments.PadRight(LineLength - 1 - Comments.Length) & Comments & vbCrLf
                'End If

                UOM = UOM & "  " & getValueByKey("CLSPSO022")
                soComboBillPrintBase.Append(strLine + vbCrLf)
                soComboBillPrintBase.Append(ArticleName + UOM.PadLeft(LineLength - ArticleName.Length - 7) + vbCrLf)
                soComboBillPrintBase.Append(strLine + vbCrLf)


                For Each dr As DataRow In DtcomboDtl.Rows
                    ItemCode = dr("ArticleCode").ToString()
                    Desc = dr("ArticleDescription").ToString()

                    If dr("PackagedUOM").ToString() = dr("BaseUOM").ToString() Then
                        Qty = dr("ItemQtyBaseUOM").ToString() & " " & dr("PackagedUOM").ToString()
                    Else
                        Qty = dr("Qty").ToString() & " " & dr("PackagedUOM").ToString()
                    End If

                    QtyUOM = dr("BaseUOM").ToString() & "  " & Qty

                    strLineDetail.Append(StrLineItem)

                    If (Desc.Length >= LineLength) Or (Desc.Length + QtyUOM.Length >= LineLength) Then
                        soComboBillPrintArticleMain.Append(Desc + vbCrLf)
                        'soComboBillPrintArticleMain.Append(Qty.PadLeft(LineLength - Qty.Length - 2, ".") + vbCrLf)
                        'soComboBillPrintArticleMain.Append(Qty.PadLeft(LineLength - 2, " ") + vbCrLf)
                        soComboBillPrintArticleMain.Append(QtyUOM.PadLeft(LineLength - 2, " ") + vbCrLf)
                    Else
                        soComboBillPrintArticleMain.Append(Desc + QtyUOM.PadLeft(LineLength - Desc.Length - 2) + vbCrLf)
                    End If

                Next


                PrinterName = SetPrinterName(dtPrinterInfo, "SalesOrder", strSODocumentType)
                Call fnBulkComboPrint(soComboBillPrintBase, soComboBillPrintArticleMain, errorMsg)


            Else
                errorMsg = getValueByKey("CLSCMP031")
            End If
        Catch ex As Exception
            errorMsg = ex.Message
        End Try
    End Sub
    Sub fnBulkComboPrint(soComboBillPrintBase As StringBuilder, soComboBillPrintArticle As StringBuilder, ByRef errorMsg As String)
        Try
            'PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")



            'dtPrinterInfo1.DefaultViewdtPrinterInfo1RowFilter = "PrinterDocument contains ('KOT'))"
            ' PrinterName = SetPrinterName(dtPrinterInfo1, "KOT", "Billing")

            If PrinterName = Nothing Then
                Exit Sub
            End If
            Dim msg As String = String.Empty
            Dim sbKOTBillPrint As New StringBuilder

            If _printPreview = True Then
                sbKOTBillPrint.Append(soComboBillPrintBase)
                sbKOTBillPrint.Append(soComboBillPrintArticle)

                msg = fnPrint(sbKOTBillPrint.ToString(), "PRV")
            Else
                soComboBillPrintArticle.Append(vbCrLf & vbCrLf & vbCrLf & vbCrLf)
                sbKOTBillPrint.Append(soComboBillPrintBase)
                sbKOTBillPrint.Append(soComboBillPrintArticle)

                msg = fnPrint(sbKOTBillPrint.ToString(), "PRN")
            End If

            If Not String.IsNullOrEmpty(msg) Then
                errorMsg = msg
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region
    Private Function MakeHtml(Qty As String, Price As String, Tax As String, Net As String, Pickup As String) As Boolean

        Dim Length As Decimal = Qty.Length + Price.Length + Tax.Length + Net.Length + Pickup.Length
        If (Length < strLineL40.Length) Then
            Return True
        Else
            Return False
        End If

    End Function

End Class




