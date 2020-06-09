Imports System.Text
Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports Microsoft.PointOfService
Imports System.Drawing

Imports System.Linq
Imports SpectrumBL

Public Class clsBirthList

    ''' <summary>
    '''  Birthlist transaction for printing 
    ''' </summary>
    ''' <remarks></remarks>

    Private _printPreview As Boolean
    Private _PrintTransaction As PrintBLTransactionSet
    Private _dtCustomerDetails As DataTable
    Private strBLDocumentType As String = "BLS"
    Private _strBirthListId As String
    Private _dtBirthListItemDetails As DataTable
    Private _IsDeliveryItem As Boolean
    Private _strEventDate As String

    Private _strEventName As String

    Public Enum PrintBLTransactionSet
        CreateBirthList
        SaleBirthListItem
        EditBirthListItem
        ReturnBirthListItem
        BirthListStatus
        GiftVoucher
    End Enum

    ''' <summary>
    ''' Customer Print for sales,retrun or create BL items 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks> 
    ''' 

#Region "Properties"

    Public Property PrintBLTransaction() As PrintBLTransactionSet
        Get
            Return _PrintTransaction
        End Get
        Set(ByVal value As PrintBLTransactionSet)
            _PrintTransaction = value
        End Set
    End Property

    Public Property BirthListId() As String
        Get
            Return _strBirthListId
        End Get
        Set(ByVal value As String)
            _strBirthListId = value
        End Set
    End Property

    Public Property EventDate() As String
        Get
            Return _strEventDate
        End Get
        Set(ByVal value As String)
            _strEventDate = value
        End Set
    End Property

    Public Property EventName() As String
        Get
            Return _strEventName
        End Get
        Set(ByVal value As String)
            _strEventName = value
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

    Public Property BirthListItemDetails() As DataTable
        Get
            Return _dtBirthListItemDetails
        End Get
        Set(ByVal value As DataTable)
            _dtBirthListItemDetails = value
        End Set
    End Property

    Public Property IsDeliveredItem() As Boolean
        Get
            Return _IsDeliveryItem
        End Get
        Set(ByVal value As Boolean)
            _IsDeliveryItem = value
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
    ''' <summary>
    ''' Voucher Information 
    ''' </summary>
    ''' <remarks></remarks>
    Private _dtVoucherSales As DataTable
    Private Property VoucherDetail() As DataTable
        Get
            Return _dtVoucherSales
        End Get
        Set(ByVal value As DataTable)
            _dtVoucherSales = value
        End Set
    End Property
    ''' <summary>
    '''  Payment History  
    ''' </summary>
    ''' <remarks></remarks>
    Private _dtPaymentHistory As DataTable
    Private Property PaymentHistory() As DataTable
        Get
            Return _dtPaymentHistory
        End Get
        Set(ByVal value As DataTable)
            _dtPaymentHistory = value
        End Set
    End Property
    Private _OrderNumber As String
    Private _InvoiceNumber As String
    Private Property InvoiceNumber() As String
        Get
            Return _InvoiceNumber
        End Get
        Set(ByVal value As String)
            _InvoiceNumber = value
        End Set
    End Property
    ''' <summary>
    '''  Order Number for Created new item information
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property OrderNumber() As String
        Get
            Return _OrderNumber
        End Get
        Set(ByVal value As String)
            _OrderNumber = value
        End Set
    End Property

    ''' <summary>
    '''  Page for printing 
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared _strPrintBLPageSetup As String
    Public Shared Property PrintBLPageSetup() As String
        Get
            Return _strPrintBLPageSetup
        End Get
        Set(ByVal value As String)
            _strPrintBLPageSetup = value
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
    Private _GiftReceiptMessage As String
    Private Property GiftReceiptMessage() As String
        Get
            Return _GiftReceiptMessage
        End Get
        Set(ByVal value As String)
            _GiftReceiptMessage = value
        End Set
    End Property

    Private _dtCreationDate As Date
    Private Property CreationDate() As Date
        Get
            Return _dtCreationDate
        End Get
        Set(ByVal value As Date)
            _dtCreationDate = value
        End Set
    End Property


    Private _decFinalDiscount As Decimal
    Public Property FinalDiscount() As Decimal
        Get
            Return _decFinalDiscount
        End Get
        Set(ByVal value As Decimal)
            _decFinalDiscount = value
        End Set
    End Property

    Private _iBLRoundOff As Integer
    Public Property BLRoundOff() As Integer
        Get
            Return _iBLRoundOff
        End Get
        Set(ByVal value As Integer)
            _iBLRoundOff = value
        End Set
    End Property

    Private _isBlBillRoundOffRequired As Boolean
    Public Property IsBillRoundOffRequired() As Boolean
        Get
            Return _isBlBillRoundOffRequired
        End Get
        Set(ByVal value As Boolean)
            _isBlBillRoundOffRequired = value
        End Set
    End Property
#End Region


#Region "Size Properties for A4Printing format "
    Protected w_ArticleCode As Single
    Protected Property WArticleCode() As Single
        Get
            Return 26
        End Get
        Set(ByVal value As Single)
            w_ArticleCode = value
        End Set
    End Property

    Protected w_ArticleDescription As Single
    Protected Property WArticleDescription() As Single
        Get
            Return 33
        End Get
        Set(ByVal value As Single)
            w_ArticleDescription = value
        End Set
    End Property

    Protected w_Qty As Single
    Protected Property WQty() As Single
        Get
            Return 10
        End Get
        Set(ByVal value As Single)
            w_Qty = value
        End Set
    End Property

    Protected w_TaxAmount As Single
    Protected Property WTaxAmount() As Single
        Get
            Return 10
        End Get
        Set(ByVal value As Single)
            w_TaxAmount = value
        End Set
    End Property

    Protected w_NetAmount As Single
    Protected Property WNetAmount() As Single
        Get
            Return 12
        End Get
        Set(ByVal value As Single)
            w_NetAmount = value
        End Set
    End Property

    Protected w_ResevedQty As Single
    Protected Property WReservedQty() As Single
        Get
            Return 9
        End Get
        Set(ByVal value As Single)
            w_ResevedQty = value
        End Set
    End Property

    Protected w_Status As Single
    Protected Property WStatus() As Single
        Get
            Return 9
        End Get
        Set(ByVal value As Single)
            w_Status = value
        End Set
    End Property

    Protected w_Rate As Single
    Protected Property WRate() As Single
        Get
            Return 10
        End Get
        Set(ByVal value As Single)
            w_Rate = value
        End Set
    End Property
#End Region


    Private _strBirthListStatus As String = ""

    Private stringToPrint, documentContents As String
    Private printPreviewDialog1 As New PrintPreviewDialog()
    Private WithEvents printDocument1 As New PrintDocument()
    Private WithEvents explorer As New PosExplorer
    'WithEvents explorer As PosExplorer
    WithEvents gOposScanner As Scanner
    WithEvents gOposMSR As Msr
    Public gOposPolDisplay As LineDisplay
    Dim gOposPrinter As PosPrinter
    Dim gOposCashDrawer As CashDrawer
    Private Property BirthListStatus() As String
        Get
            Return _strBirthListStatus
        End Get
        Set(ByVal value As String)
            _strBirthListStatus = value
        End Set
    End Property

    Private _dtTaxDetails As DataTable
    Private Property TaxDetails() As DataTable
        Get
            Return _dtTaxDetails
        End Get
        Set(ByVal value As DataTable)
            _dtTaxDetails = value
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

#Region "Constructors"
    ''' <summary>
    ''' This constructor is used for Create BirthList and BirthListStatus print out 
    ''' </summary>
    ''' <param name="enumTransactionDetails">only BirthListCreate</param>
    ''' <param name="strSiteCode">Current Site Code </param>
    ''' <param name="strBirthListID">BirthList ID </param>
    ''' <param name="dtCustomerDetail">Customer Details </param>
    ''' <param name="dtSalesItem">BirthList Sales Item</param>
    ''' <param name="strErrorMsg">Optinal Error Msg</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal enumTransactionDetails As PrintBLTransactionSet, ByVal strSiteCode As String, ByVal strDefaultCurrency As String, ByVal strUserID As String, ByVal strBirthListID As String, ByVal dtCustomerDetail As DataTable, ByVal dtSalesItem As DataTable, Optional ByVal _dtVoucherDetails As DataTable = Nothing, Optional ByRef strErrorMsg As String = "", Optional ByVal strEvntDate As String = "", Optional ByVal strEvntname As String = "", Optional ByVal decFinalDiscount As Decimal = 0, Optional ByVal iBLRoundOFF As Integer = 2, Optional ByVal blPrinterInfo As DataTable = Nothing, Optional ByVal strBirthListStatus As String = "", Optional ByVal dtTaxDetails As DataTable = Nothing, Optional ByVal terminal As String = "")
        Try

            'If Not PrintBLTransaction = PrintBLTransactionSet.CreateBirthList Then
            '    If Not BirthListItemDetails Is Nothing Then
            '        If BirthListItemDetails.Rows.Count > 0 AndAlso BirthListItemDetails.Columns.Contains("PickUpQty") Then
            '            Dim iPickUpQty As Integer = BirthListItemDetails.Compute("sum(PickUpQty)", " ")
            '            If iPickUpQty > 0 Then
            '                IsDeliveredItem = True
            '            End If
            '        End If
            '    End If
            'End If

            PrintBLTransaction = enumTransactionDetails
            If PrintBLTransaction = PrintBLTransactionSet.SaleBirthListItem Or PrintBLTransaction = PrintBLTransactionSet.EditBirthListItem Or PrintBLTransaction = PrintBLTransactionSet.GiftVoucher Or PrintBLTransaction = PrintBLTransactionSet.ReturnBirthListItem Then
                If IsDeliveredItem Then
                    strBLDocumentType = "BLOB"
                Else
                    strBLDocumentType = "BLInvc" 'BLINV.
                End If
            ElseIf PrintBLTransaction = PrintBLTransactionSet.BirthListStatus Or PrintBLTransaction = PrintBLTransactionSet.CreateBirthList Then
                strBLDocumentType = "BLStatus"
            End If


            PrintBLPageSetup = (GetPrintFormat(terminal, strBLDocumentType)).Rows(0)(0)
            If enumTransactionDetails = PrintBLTransactionSet.CreateBirthList Or enumTransactionDetails = PrintBLTransactionSet.BirthListStatus Then
                BirthListId = strBirthListID
                CustomerDetails = dtCustomerDetail
                BirthListItemDetails = dtSalesItem
                SiteCode = strSiteCode
                CashierId = strUserID
                DefaultCurrency = strDefaultCurrency
                VoucherDetail = _dtVoucherDetails
                EventDate = strEvntDate
                EventName = strEvntname
                CreationDate = GetCurrentDate()
                BLRoundOff = iBLRoundOFF
                dtPrinterInfo1 = blPrinterInfo
                BirthListStatus = strBirthListStatus
                TaxDetails = dtTaxDetails
                If PrintConfig() Then
                    If Not PrintBLPageSetup = "L40" Then
                        If Not A4Print(CustomerDetails, BirthListItemDetails) Then
                            'MsgBox("Printing Problem")
                        End If
                    Else
                        If Not L40Print(CustomerDetails, BirthListItemDetails) Then
                            'MsgBox("Printing Problem")
                        End If
                    End If
                Else
                    MsgBox(getValueByKey("CLBL02"), , "CLBL02 - " & getValueByKey("CLAE04"))
                End If

            End If
        Catch ex As Exception
            MsgBox(getValueByKey("CLPBL01"), , "CLPBL01 - " & getValueByKey("CLAE05"))
        End Try
    End Sub

    Public Sub New(ByVal enumTransactionDetails As PrintBLTransactionSet, ByVal strSiteCode As String, ByVal strDefaultCurrency As String, ByVal strUserID As String, ByVal strBirthListID As String, ByVal dtcustomerDetails As DataTable, ByVal dtbirthlistitem As DataTable, ByVal strInvoiceNumber As String, ByVal strOrderNumber As String, Optional ByVal dtVoucherDetail As DataTable = Nothing, Optional ByVal dtBirthListCustomerInfo As DataRow = Nothing, Optional ByVal dtPaymentReciept As DataTable = Nothing, Optional ByVal isReprint As Boolean = True, Optional ByVal strEvntDate As String = "", Optional ByVal strEvntname As String = "", Optional ByVal strGiftReceiptMessage As String = "", Optional ByVal decFinalDiscount As Decimal = 0, Optional ByVal iBLRoundOFF As Integer = 2, Optional ByVal blPrinterInfo As DataTable = Nothing, Optional ByVal strBirthListStatus As String = "", Optional ByVal dtTaxDetails As DataTable = Nothing, Optional ByVal terminal As String = "")
        Try
            PrintBLTransaction = enumTransactionDetails
            CustomerDetails = dtcustomerDetails
            BirthListItemDetails = dtbirthlistitem
            VoucherDetail = dtVoucherDetail
            InvoiceNumber = strInvoiceNumber
            OrderNumber = strOrderNumber
            SiteCode = strSiteCode
            BirthListId = strBirthListID
            DefaultCurrency = strDefaultCurrency
            'Reprint = isReprint
            BirthListCustomerInfo = dtBirthListCustomerInfo
            PaymentHistory = dtPaymentReciept
            CashierId = strUserID
            EventDate = strEvntDate
            EventName = strEvntname
            FinalDiscount = decFinalDiscount
            GiftReceiptMessage = strGiftReceiptMessage
            CreationDate = GetCurrentDate()
            BLRoundOff = iBLRoundOFF
            dtPrinterInfo1 = blPrinterInfo
            BirthListStatus = strBirthListStatus
            TaxDetails = dtTaxDetails


            'If enumTransactionDetails = PrintBLTransactionSet.EditBirthListItem Then
            '    If Not dtbirthlistitem Is Nothing AndAlso dtbirthlistitem.Columns.Contains("PickupQty") Then
            '        Dim iPickupQty As Integer = dtbirthlistitem.Compute("sum(PickupQty)", "")
            '        Dim PurchasedQty As Integer = dtbirthlistitem.Compute("sum(PurchasedQty)", "")
            '        If PurchasedQty = 0 AndAlso iPickupQty > 0 Then
            '            IsDeliveredItem = True
            '        End If
            '    End If
            'End If


            'If PrintConfig() Then
            '    If PageSetup = "A4" Then
            '        If Not A4Print(CustomerDetails, BirthListItemDetails) Then
            '            MsgBox("Printing Problem")
            '        End If
            '    ElseIf PageSetup = "L40" Then

            '    End If

            'Else
            '    MsgBox("Printing Configuration problem ")
            'End If

            If PrintBLTransaction = PrintBLTransactionSet.SaleBirthListItem Or PrintBLTransaction = PrintBLTransactionSet.EditBirthListItem Or PrintBLTransaction = PrintBLTransactionSet.GiftVoucher Or PrintBLTransaction = PrintBLTransactionSet.ReturnBirthListItem Then

                If IsDeliveredItem Then
                    strBLDocumentType = "BLOB"
                Else
                    strBLDocumentType = "BLInvc" 'BLINV.
                End If
            ElseIf PrintBLTransaction = PrintBLTransactionSet.BirthListStatus Or PrintBLTransaction = PrintBLTransactionSet.CreateBirthList Then
                strBLDocumentType = "BLStatus"
            End If

            PrintBLPageSetup = (GetPrintFormat(terminal, strBLDocumentType)).Rows(0)(0)

            If PrintConfig() Then
                If PrintBLTransaction = PrintBLTransactionSet.BirthListStatus Then
                    PrintBLPageSetup = "A4"
                    If Not A4Print(CustomerDetails, BirthListItemDetails) Then
                        MsgBox(getValueByKey("CLPBL01"), , "CLPBL01 - " & getValueByKey("CLAE04"))
                    End If
                ElseIf Not PrintBLPageSetup = "L40" Then
                    If Not A4Print(CustomerDetails, BirthListItemDetails) Then
                        MsgBox(getValueByKey("CLPBL01"), , getValueByKey("CLAE04"))
                    End If
                Else
                    If Not L40Print(CustomerDetails, BirthListItemDetails) Then
                        MsgBox(getValueByKey("CLPBL01"), , getValueByKey("CLAE04"))
                    End If
                End If

            Else
                MsgBox(getValueByKey("CLBL02"), , "CLBL02 - " & getValueByKey("CLAE04"))
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "A4Printing Format and Function "
    Public Function A4Print(ByVal dtCustomerDetails As DataTable, ByVal dtGridData As DataTable, Optional ByVal strBirthListID As String = "") As Boolean
        Try

            Dim sbHdrItemInfo As New System.Text.StringBuilder
            Dim sbHdrItem As New StringBuilder
            Dim iItemDetailHeight As Integer
            If PrintBLTransaction = PrintBLTransactionSet.CreateBirthList Or PrintBLTransaction = PrintBLTransactionSet.BirthListStatus Then
                If A4GetSalesItemsDetails(BirthListItemDetails, sbHdrItemInfo, sbHdrItem, iItemDetailHeight) Then
                    If A4PrintFormat(sbHdrItemInfo, sbHdrItem, iItemDetailHeight) Then
                        Return True
                    End If
                End If
            ElseIf PrintBLTransaction = PrintBLTransactionSet.SaleBirthListItem Or PrintBLTransaction = PrintBLTransactionSet.EditBirthListItem Then
                Dim OnlyPickuPPrint As Boolean = IsDeliveredItem
                If A4GetSalesItemsDetails(BirthListItemDetails, sbHdrItemInfo, sbHdrItem, iItemDetailHeight) Then
                    If A4PrintFormat(sbHdrItemInfo, sbHdrItem, iItemDetailHeight) Then
                        If OnlyPickuPPrint = False Then
                            If IsDeliveredItem Then
                                sbHdrItem.Length = 0
                                sbHdrItemInfo.Length = 0
                                'Delivery Print 

                                'Change by Ashish on 26 Nov 2010
                                'This change is to check the transaction set and appropriately set the document type and print format
                                If PrintBLTransaction = PrintBLTransactionSet.SaleBirthListItem Or PrintBLTransaction = PrintBLTransactionSet.EditBirthListItem Or PrintBLTransaction = PrintBLTransactionSet.GiftVoucher Or PrintBLTransaction = PrintBLTransactionSet.ReturnBirthListItem Then
                                    If IsDeliveredItem Then
                                        strBLDocumentType = "BLOB"
                                    Else
                                        strBLDocumentType = "BLInvc" 'BLINV.
                                    End If
                                ElseIf PrintBLTransaction = PrintBLTransactionSet.BirthListStatus Or PrintBLTransaction = PrintBLTransactionSet.CreateBirthList Then
                                    strBLDocumentType = "BLStatus"
                                End If
                                PrintBLPageSetup = (GetPrintFormat(dtPrinterInfo1.Rows(0)(0), strBLDocumentType)).Rows(0)(0)
                                If PrintBLPageSetup = "A4" Then
                                    If A4GetSalesItemsDetails(BirthListItemDetails, sbHdrItemInfo, sbHdrItem, iItemDetailHeight) Then
                                        If A4PrintFormat(sbHdrItemInfo, sbHdrItem, iItemDetailHeight, True) Then
                                            IsDeliveredItem = False
                                            Return True
                                        End If
                                    End If
                                End If
                                If PrintBLPageSetup = "L40" Then
                                    If L40GetSalesItemDetails(BirthListItemDetails, sbHdrItemInfo, sbHdrItem) Then
                                        If L40PrintFormat(sbHdrItemInfo, sbHdrItem, True) Then
                                            IsDeliveredItem = False
                                            Return True
                                        End If
                                    End If
                                End If
                                'End of change

                            Else
                                Return True
                            End If
                        Else
                            Return True
                        End If

                    End If
                End If
                Return True
            ElseIf PrintBLTransaction = PrintBLTransactionSet.GiftVoucher Then
                If A4GetSalesItemsDetails(BirthListItemDetails, sbHdrItemInfo, sbHdrItem, iItemDetailHeight) Then
                    If A4PrintFormat(sbHdrItemInfo, sbHdrItem, iItemDetailHeight) Then
                        Return True
                    End If
                End If
            End If
            Return True
        Catch ex As Exception

        End Try
    End Function

    Public Function A4Title(Optional ByVal soldItems As Boolean = False) As String
        Try
            Dim sbTitle As New StringBuilder
            If PrintBLPageSetup = "A4" Then
                If (PrintBLTransaction = PrintBLTransactionSet.CreateBirthList Or PrintBLTransaction = PrintBLTransactionSet.BirthListStatus) Then
                    sbTitle.Append(strLineA4 + vbCrLf)
                    Dim strTitleInfo As String

                    'strTitleInfo = String.Format("BIRTHLIST {0} STATUS: {1} ", _strBirthListId, BirthListStatus)
                    strTitleInfo = String.Format(getValueByKey("CLSBL001") & " ", _strBirthListId, BirthListStatus)
                    strTitleInfo.PadRight(20)
                    'strTitleInfo = String.Format("{0} DATE {1}", strTitleInfo, CreationDate)
                    strTitleInfo = String.Format(getValueByKey("CLSPSO002"), strTitleInfo, CreationDate)

                    sbTitle.Append(strTitleInfo + vbCrLf)
                    sbTitle.Append(strLineA4 + vbCrLf)
                ElseIf PrintBLTransaction = PrintBLTransactionSet.SaleBirthListItem Or PrintBLTransaction = PrintBLTransactionSet.EditBirthListItem Then
                    sbTitle.Append(strLineA4 + vbCrLf)
                    Dim strTitleInfo As String
                    If Not soldItems Then
                        'strTitleInfo = String.Format("BIRTHLIST {0} SOLD ITEMS ", _strBirthListId)
                        strTitleInfo = String.Format(getValueByKey("CLSBL002") & " ", _strBirthListId)
                    Else
                        'strTitleInfo = String.Format("BIRTHLIST {0} DELIVERY NOTE ", _strBirthListId)
                        strTitleInfo = String.Format(getValueByKey("CLSBL003") & " ", _strBirthListId)
                    End If
                    strTitleInfo.PadRight(20)
                    'strTitleInfo = String.Format("{0} DATE {1}", strTitleInfo, Now.Date)
                    strTitleInfo = String.Format(getValueByKey("CLSPSO002"), strTitleInfo, Now.Date)
                    sbTitle.Append(strTitleInfo + vbCrLf)
                    sbTitle.Append(strLineA4 + vbCrLf)
                ElseIf (PrintBLTransaction = PrintBLTransactionSet.GiftVoucher) Then
                    sbTitle.Append(strLineA4 + vbCrLf)
                    Dim strTitleInfo As String
                    'strTitleInfo = String.Format("BIRTHLIST {0} Gift Voucher ", _strBirthListId)
                    strTitleInfo = String.Format(getValueByKey("CLSBL004") & " ", _strBirthListId)
                    strTitleInfo.PadRight(20)
                    'strTitleInfo = String.Format("{0} DATE {1}", strTitleInfo, CreationDate)
                    strTitleInfo = String.Format(getValueByKey("CLSPSO002"), strTitleInfo, CreationDate)
                    sbTitle.Append(strTitleInfo + vbCrLf)
                    sbTitle.Append(strLineA4 + vbCrLf)
                End If
            ElseIf PrintBLPageSetup = "L40" Then
                If (PrintBLTransaction = PrintBLTransactionSet.CreateBirthList) Then
                    Dim strTitleInfo As String
                    'strTitleInfo = String.Format("BIRTHLIST {0} PAYMENT ", _strBirthListId)
                    strTitleInfo = String.Format(getValueByKey("CLSBL005") & " ", _strBirthListId)
                    'strTitleInfo = String.Format("{0}CREATION DATE {1}", strTitleInfo + vbCrLf, CreationDate)
                    strTitleInfo = String.Format(getValueByKey("CLSBL006"), strTitleInfo + vbCrLf, CreationDate)
                    sbTitle.Append(strLineL40 + vbCrLf)
                    sbTitle.Append(strTitleInfo + vbCrLf)
                    sbTitle.Append(strLineL40 + vbCrLf)
                ElseIf PrintBLTransaction = PrintBLTransactionSet.SaleBirthListItem Or PrintBLTransaction = PrintBLTransactionSet.EditBirthListItem Then
                    Dim strTitleInfo As String
                    If Not soldItems Then
                        'strTitleInfo = String.Format("BIRTHLIST {0} SOLD ITEMS ", _strBirthListId)
                        strTitleInfo = String.Format(getValueByKey("CLSBL002") & " ", _strBirthListId)
                    Else
                        'strTitleInfo = String.Format("BIRTHLIST {0} DELIVERY NOTE ", _strBirthListId)
                        strTitleInfo = String.Format(getValueByKey("CLSBL003") & " ", _strBirthListId)
                    End If
                    strTitleInfo.PadRight(20)
                    'strTitleInfo = String.Format("{0}CREATION DATE {1}", strTitleInfo + vbCrLf, CreationDate)
                    strTitleInfo = String.Format(getValueByKey("CLSBL006"), strTitleInfo + vbCrLf, CreationDate)
                    sbTitle.Append(strLineL40 + vbCrLf)
                    sbTitle.Append(strTitleInfo + vbCrLf)
                    sbTitle.Append(strLineL40 + vbCrLf)
                ElseIf (PrintBLTransaction = PrintBLTransactionSet.GiftVoucher) Then
                    Dim strTitleInfo As String
                    'strTitleInfo = String.Format("BIRTHLIST {0} GIFT VOUCHER ", _strBirthListId)
                    strTitleInfo = String.Format(getValueByKey("CLSBL004") & " ", _strBirthListId)
                    'strTitleInfo = String.Format("{0}CREATION DATE {1}", strTitleInfo + vbCrLf, CreationDate)
                    strTitleInfo = String.Format(getValueByKey("CLSBL006"), strTitleInfo + vbCrLf, CreationDate)
                    sbTitle.Append(strLineL40 + vbCrLf)
                    sbTitle.Append(strTitleInfo + vbCrLf)
                    sbTitle.Append(strLineL40 + vbCrLf)

                End If
            End If
            Return sbTitle.ToString()
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Public Function A4Footer(ByVal _dtBirthListItemDetails As DataTable, Optional ByVal soldItems As Boolean = False) As String
        Try
            Dim sbFooter As New StringBuilder
            Dim decTotal As Decimal

            If (PrintBLTransaction = PrintBLTransactionSet.CreateBirthList) Then
                Dim strFooterInfo As String
                Dim decTotalAmount As Decimal
                'strFooterInfo = "TOTAL"
                strFooterInfo = getValueByKey("CLSPSO034")

                strFooterInfo = strFooterInfo.PadRight(80)
                decTotalAmount = _dtBirthListItemDetails.Compute("sum(Amount)", "")
                strFooterInfo = strFooterInfo & PrintFormatCurrency(MyRound(decTotalAmount, BLRoundOff, IsBillRoundOffRequired), DefaultCurrency, BLRoundOff)
                sbFooter.Append(strFooterInfo + vbCrLf)
                'strFooterInfo = "FINAL DISCOUNT"
                strFooterInfo = getValueByKey("CLSBL007")
                strFooterInfo = strFooterInfo.PadRight(80)
                strFooterInfo = strFooterInfo & strZero
                ' sbFooter.Append(vbCrLf + "Terms and Conditions" + vbCrLf)
            ElseIf PrintBLTransaction = PrintBLTransactionSet.SaleBirthListItem Then
                Dim strFooterInfo As String
                If Not soldItems Then
                    decTotal = 0.0
                    'strFooterInfo = "TOTAL OF SOLD ITEMS "

                    strFooterInfo = getValueByKey("CLSBL008") & " "

                    For Each drGrid As DataRow In _dtBirthListItemDetails.Rows
                        If Not (drGrid.RowState = DataRowState.Deleted) Then
                            If drGrid("PurchasedQty") > 0 Then
                                decTotal += drGrid("NetAmount")
                            End If
                        End If
                    Next
                    If Not _dtVoucherSales Is Nothing Then

                        If _dtVoucherSales.Rows.Count > 0 Then
                            decTotal = Decimal.Add(decTotal, _dtVoucherSales.Compute("Sum(NetAmount)", " "))
                        End If
                        'If _dtVoucherSales.Columns.Contains("ExclusiveTax") Then
                        '    decTotal = Decimal.Add(decTotal, _dtVoucherSales.Compute("Sum(ExclusiveTax)", " "))
                        'End If
                    End If
                Else
                    'strFooterInfo = "TOTAL OF DELIVERED ITEMS "
                    strFooterInfo = getValueByKey("CLSBL009") & " "
                    For Each drGrid As DataRow In _dtBirthListItemDetails.Rows
                        If Not (drGrid.RowState = DataRowState.Deleted) Then
                            If drGrid("PickupQty") > 0 Then
                                If Not drGrid("SellingPrice") Is Nothing AndAlso Not drGrid("SellingPrice") Is DBNull.Value Then
                                    decTotal += drGrid("SellingPrice") * drGrid("PickupQty")
                                End If
                                'If Not drGrid("ExclusiveTax") Is Nothing AndAlso Not drGrid("ExclusiveTax") Is DBNull.Value Then
                                '    decTotal += drGrid("ExclusiveTax")
                                'End If

                            End If

                        End If
                    Next
                End If
                strFooterInfo = strFooterInfo.PadRight(80)
                strFooterInfo = strFooterInfo & PrintFormatCurrency(MyRound(decTotal, BLRoundOff, IsBillRoundOffRequired), DefaultCurrency, BLRoundOff)
                ' strFooterInfo.PadRight(20)
                sbFooter.Append(strFooterInfo + vbCrLf)
            ElseIf PrintBLTransaction = PrintBLTransactionSet.EditBirthListItem Then
                Dim strFooterInfo As String
                If Not soldItems Then
                    'strFooterInfo = "TOTAL OF SOLD ITEMS "
                    strFooterInfo = getValueByKey("CLSBL008") & " "
                    decTotal = 0.0
                    For Each drGrid As DataRow In _dtBirthListItemDetails.Rows
                        If Not (drGrid.RowState = DataRowState.Deleted) Then
                            If drGrid("PurchasedQty") > 0 Then

                                If Not drGrid("SellingPrice") Is Nothing AndAlso Not drGrid("SellingPrice") Is DBNull.Value Then
                                    'If drGrid("PurchasedQty") = 0 AndAlso drGrid("BookedQty") = 0 Then
                                    decTotal += (drGrid("PurchasedQty")) * drGrid("SellingPrice")
                                    If Not drGrid("ExclusiveTax") Is Nothing AndAlso Not drGrid("ExclusiveTax") Is DBNull.Value Then
                                        decTotal += drGrid("ExclusiveTax")
                                    End If
                                    '    'ElseIf drGrid("BookedQty") = 0 Then
                                    '    decTotal += drGrid("PurchasedQty") * drGrid("SellingPrice")
                                    'End If
                                End If



                                ''#Chat with JC
                                ''If drGrid("CurrentPurchasedAmount") = Decimal.Zero Then
                                'If Not drGrid("SellingPrice") Is Nothing AndAlso Not drGrid("SellingPrice") Is DBNull.Value Then
                                '    'If drGrid("PurchasedQty") = 0 AndAlso drGrid("BookedQty") = 0 Then
                                '    decTotal += (drGrid("PurchasedQty") + drGrid("BookedQty")) * drGrid("SellingPrice")
                                '    If Not drGrid("ExclusiveTax") Is Nothing AndAlso Not drGrid("ExclusiveTax") Is DBNull.Value Then
                                '        decTotal += drGrid("ExclusiveTax")
                                '    End If
                                '    '    'ElseIf drGrid("BookedQty") = 0 Then
                                '    '    decTotal += drGrid("PurchasedQty") * drGrid("SellingPrice")
                                '    'End If
                                'End If
                                ''# Chat with JC



                                'Else
                                '    decTotal += drGrid("CurrentPurchasedAmount")
                                'End If
                                'Else
                                '    If drGrid("PickupQty") > 0 Then
                                '        If drGrid("CurrentPurchasedAmount") = Decimal.Zero Then
                                '            If Not drGrid("SellingPrice") Is Nothing AndAlso Not drGrid("SellingPrice") Is DBNull.Value Then
                                '                decTotal += drGrid("PickupQty") * drGrid("SellingPrice")
                                '            End If
                                '        Else
                                '            decTotal += drGrid("CurrentPurchasedAmount")
                                '        End If
                                '    End If
                            End If
                        End If
                    Next
                Else
                    'strFooterInfo = "TOTAL OF DELIVERED ITEMS "
                    strFooterInfo = getValueByKey("CLSBL009") & " "
                    decTotal = 0.0
                    For Each drGrid As DataRow In _dtBirthListItemDetails.Rows
                        If Not (drGrid.RowState = DataRowState.Deleted) Then
                            If drGrid("PickupQty") > 0 Then
                                'If drGrid("CurrentPurchasedAmount") = Decimal.Zero Then
                                If Not drGrid("SellingPrice") Is Nothing AndAlso Not drGrid("SellingPrice") Is DBNull.Value Then
                                    decTotal += drGrid("PickupQty") * drGrid("SellingPrice")
                                    If Not drGrid("ExclusiveTax") Is Nothing AndAlso Not drGrid("ExclusiveTax") Is DBNull.Value Then
                                        decTotal += drGrid("ExclusiveTax")
                                    End If
                                End If
                                'Else
                                '    decTotal += drGrid("CurrentPurchasedAmount")
                                'End If

                            End If
                        End If
                    Next
                End If
                strFooterInfo = strFooterInfo.PadRight(80)
                strFooterInfo = strFooterInfo & PrintFormatCurrency(MyRound(decTotal, BLRoundOff, IsBillRoundOffRequired), DefaultCurrency, BLRoundOff)
                ' strFooterInfo.PadRight(20)
                sbFooter.Append(strFooterInfo + vbCrLf)
            End If
            Return sbFooter.ToString()

        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Public Function A4GiftVoucherDetails() As String
        Try
            If PrintBLTransaction = PrintBLTransactionSet.BirthListStatus Then
                If Not VoucherDetail Is Nothing AndAlso VoucherDetail.Rows.Count > 0 Then
                    Dim sbGiftVoucher As New StringBuilder
                    Dim strInvoiceNumber As String = ""
                    Dim dateCreated As Date
                    Dim strAmount As String = ""
                    Dim strQty As String = ""
                    Dim strPayedBy As String = ""
                    For Each drGiftVoucher As DataRow In _dtVoucherSales.Rows
                        strInvoiceNumber = drGiftVoucher("SaleInvNumber")
                        strInvoiceNumber = strInvoiceNumber.PadRight(20)
                        dateCreated = drGiftVoucher("CreatedOn")
                        strAmount = PrintFormatCurrency(drGiftVoucher("NetAmt"), DefaultCurrency, BLRoundOff)
                        strAmount = strAmount.PadLeft(60)
                        'strPayedBy = "Info:payd by " & drGiftVoucher("CustomerName")
                        strPayedBy = getValueByKey("CLSBL010") & " " & drGiftVoucher("CustomerName")
                        strPayedBy = strPayedBy.PadLeft(40)
                        'sbGiftVoucher.Append(vbCrLf + strInvoiceNumber & "created on " & dateCreated.ToString(clsCommon.GetSystemDateFormat()) & strAmount + vbCrLf)
                        sbGiftVoucher.Append(vbCrLf + strInvoiceNumber & getValueByKey("CLSBL017") & " " & dateCreated.ToString(clsCommon.GetSystemDateFormat()) & strAmount + vbCrLf)
                        sbGiftVoucher.Append(strPayedBy)
                    Next
                    Return sbGiftVoucher.ToString()
                Else
                    Return String.Empty
                End If
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Public Function A4GetOpenAmount() As String
        Try
            If PrintBLTransaction = PrintBLTransactionSet.BirthListStatus Then
                If Not BirthListCustomerInfo Is Nothing Then

                    Dim objOpenAmount As Object = BirthListCustomerInfo("OpenAmount")
                    If Not objOpenAmount Is Nothing AndAlso Not objOpenAmount Is DBNull.Value Then
                        Dim strOpenAmount As String = CStr(objOpenAmount)
                        strOpenAmount.PadLeft(80)
                        Return strOpenAmount
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Function

    Public Function A4PrintFormat(ByVal sbHdrItemInfo As StringBuilder, ByVal sbHdrItem As StringBuilder, ByVal iItemDetailHeight As Integer, Optional ByVal soldItems As Boolean = False) As Boolean
        Try
            CheckDocumentType()

            Dim strHeader As New StringBuilder
            Dim strFooter As New StringBuilder
            Dim strWelcomeMsg As New StringBuilder
            Dim strTaxInformation As New StringBuilder
            Dim strPromotionMsg As New StringBuilder
            PrinttHeaderAndFooter(strHeader, strFooter, strWelcomeMsg, strPromotionMsg, strTaxInformation, strBLDocumentType, PrintBLPageSetup)

            Dim strFooterPayment As String = A4Footer(BirthListItemDetails, soldItems)

            ' Title 
            Dim sbTitle As New StringBuilder

            sbTitle.Append(A4Title(soldItems))

            Dim strCashierDetails As String = A4CashierDetails(CreationDate, InvoiceNumber, CashierId, "BirthList", EventDate, EventName)

            Dim strCustomerDetails As String
            Dim strTermsNConditions As String
            strCustomerDetails = A4GetCustomerDetails(_dtCustomerDetails)

            Dim strCustomerDeliveryDetails As String
            strCustomerDeliveryDetails = A4GetCustomerDeliveryDetails(_dtCustomerDetails)

            'Remark 
            Dim sbRemark As New StringBuilder
            'Dim strRemark As String = "Remark"
            Dim strRemark As String = getValueByKey("CLSBL018")
            sbRemark.Append(strRemark + vbCrLf)

            Dim objclsA4Print As New clsA4Print

            Dim strPrint As New StringBuilder
            strPrint.Length = 0
            If IsHeaderPrinting Then
                objclsA4Print.A4Header = strHeader.ToString()
            End If
            objclsA4Print.A4Title = sbTitle.ToString()
            objclsA4Print.A4CustomerDetails = strCustomerDetails
            objclsA4Print.A4DeliveryAddress = strCustomerDeliveryDetails
            objclsA4Print.A4Remark = sbRemark.ToString()
            objclsA4Print.A4CashierDetails = strCashierDetails

            If PrintBLTransaction = PrintBLTransactionSet.BirthListStatus Then
                Dim strGiftVoucher As String = A4GiftVoucherDetails()
                Dim strOpenAmount As String = A4GetOpenAmount()
                objclsA4Print.DocumentType = clsA4Print.DocumentTypeList.BirthListStatus
                objclsA4Print.A4OpenAmount = PrintFormatCurrency(strOpenAmount, DefaultCurrency, BLRoundOff)
                objclsA4Print.A4GiftVoucher = strGiftVoucher
                objclsA4Print.FinalDiscount = PrintFormatCurrency(FinalDiscount, DefaultCurrency, BLRoundOff)
            End If

            objclsA4Print.A4ItemHeader = sbHdrItem.ToString()
            objclsA4Print.A4ItemDetails = sbHdrItemInfo.ToString()
            objclsA4Print.A4PaymentDetails = strFooterPayment
            strPrint.Append(strFooterPayment)

            If Not IsDeliveredItem Then
                strTermsNConditions = A4GetStringTermsCondition(strBLDocumentType, SiteCode, PrintBLPageSetup)
                objclsA4Print.A4TermsNConditions = strTermsNConditions
            End If

            objclsA4Print.A4WelcomeMessage = strWelcomeMsg.ToString()
            objclsA4Print.A4PromotionInformation = strPromotionMsg.ToString()
            objclsA4Print.A4TaxInformation = strTaxInformation.ToString()


            objclsA4Print.GiftReceiptMessage = GiftReceiptMessage
            If IsFooterPrinting Then
                objclsA4Print.A4Footer = strFooter.ToString()
            End If


            '--Commented by rama on 25-Oct-2010 for wrong condtion matching 
            ' PrinterName = SetPrinterName(dtPrinterInfo1, "BirthList", PrintBLTransaction.ToString)
            PrinterName = SetPrinterName(dtPrinterInfo1, "BirthList", strBLDocumentType)
            If _printPreview = True Then
                objclsA4Print.fnPrint("PRV")
            Else
                objclsA4Print.fnPrint("PRN")
            End If

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub CheckDocumentType()

        'Change by Ashish on 26 Nov 2010
        'Commenting out the lines below since they are no longer required
        '
        'If PrintBLTransaction = PrintBLTransactionSet.SaleBirthListItem Or PrintBLTransaction = PrintBLTransactionSet.EditBirthListItem Or PrintBLTransaction = PrintBLTransactionSet.GiftVoucher Or PrintBLTransaction = PrintBLTransactionSet.ReturnBirthListItem Then
        '    If IsDeliveredItem Then
        '        strBLDocumentType = "BLOB"
        '    Else
        '        strBLDocumentType = "BLInvc" 'BLINV.
        '    End If
        'ElseIf PrintBLTransaction = PrintBLTransactionSet.BirthListStatus Or PrintBLTransaction = PrintBLTransactionSet.CreateBirthList Then
        '    strBLDocumentType = "BLStatus"

        'End If
        '
        'End of change
    End Sub

    ''' <summary>
    ''' Default configuration parameters 
    ''' </summary>

    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 

    Public Function PrintConfig() As Boolean
        Try
            Dim dt As DataTable
            dt = GetDefaultSetting(SiteCode, "BLS")
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If dr("FLDLABEL").ToString().ToUpper = "BLISPRINTFOOTER" Then
                        IsFooterPrinting = dr("FLDVALUE").ToString
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "BLIsPromotionMsgPrint".ToUpper() Then
                        IsPromotionMessagePrint = dr("FLDVALUE").ToString
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "BLIsTaxInformationMsgPrint".ToUpper() Then
                        IsTaxInformation = dr("FLDVALUE").ToString
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "BLISPRINTHEADER" Then
                        IsHeaderPrinting = dr("FLDVALUE").ToString
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "BLRoundOffRequired".ToUpper() Then
                        IsBillRoundOffRequired = dr("FLDVALUE").ToString
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "BLPrintPreivew".ToUpper() Then
                        _printPreview = dr("FLDVALUE").ToString
                    End If
                Next
            End If
            dt = GetDefaultSetting(SiteCode, "0000")
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If dr("FLDLABEL").ToString().ToUpper = "ROUNDED_OFF_TO".ToUpper() Then
                        DecimalDigits = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "IsWelcomeMsgPrint".ToUpper() Then
                        IsWelComeMessagePrint = dr("FLDVALUE").ToString
                        'ElseIf dr("FLDLABEL").ToString().ToUpper = "PrintBLPageSetup".ToUpper() Then
                        '   PrintBLPageSetup = dr("FLDVALUE").ToString
                    End If
                Next
            End If


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Pass parameters to the function 
    ''' </summary>

    ''' <param name="enumTransactionDetails">Select Transaction from transaction set </param>
    ''' <param name="strSiteCode">SiteCode</param>
    ''' <param name="strBirthListID">BirthListID</param>
    ''' <param name="dtCustomerDetail">BirthList Customer details </param>
    ''' <param name="dtSalesItem">BirthList Sales Item details</param>
    ''' <param name="strErrorMsg">Store error messages if there are any</param>

    ''' <remarks></remarks>

    ''' <remarks></remarks>       


    Public Function A4SalesItemHdr(ByRef sbHdrItem As StringBuilder) As String
        Try
            If PrintBLTransaction = PrintBLTransactionSet.CreateBirthList Or PrintBLTransaction = PrintBLTransactionSet.BirthListStatus Then
                sbHdrItem.Append(vbCrLf + strLineA4 + vbCrLf)
                Dim strHdrItem As String = ""
                'strHdrItem = "Item Code"
                strHdrItem = getValueByKey("CLSPSO020")

                'strHdrItem = strHdrItem.PadRight(WArticleCode) + "Description"
                strHdrItem = strHdrItem.PadRight(WArticleCode) + getValueByKey("CLSPSO021")
                sbHdrItem.Append(strHdrItem)

                Dim strHdrItemQty As String = ""
                If PrintBLTransaction = PrintBLTransactionSet.BirthListStatus Then
                    'strHdrItemQty = strHdrItemQty.PadLeft(WArticleDescription + WQty - 16) + "Qty"
                    'strHdrItemQty = strHdrItemQty.PadRight(WArticleDescription + WQty - 5) + "Price"
                    strHdrItemQty = strHdrItemQty.PadLeft(WArticleDescription + WQty - 16) + getValueByKey("CLSPSO022")
                    strHdrItemQty = strHdrItemQty.PadRight(WArticleDescription + WQty - 5) + getValueByKey("CLSPSO023")
                Else
                    'strHdrItemQty = strHdrItemQty.PadLeft(WArticleDescription + WQty - 14) + "Qty"
                    'strHdrItemQty = strHdrItemQty.PadRight(WArticleDescription + WQty - 4) + "Price"

                    strHdrItemQty = strHdrItemQty.PadLeft(WArticleDescription + WQty - 14) + getValueByKey("CLSPSO022")
                    strHdrItemQty = strHdrItemQty.PadRight(WArticleDescription + WQty - 4) + getValueByKey("CLSPSO023")
                End If

                sbHdrItem.Append(strHdrItemQty)

                Dim strHdrItemNet As String = ""
                'strHdrItemNet = strHdrItemNet.PadLeft(7) + "Net"
                'strHdrItemNet = strHdrItemNet.PadRight(16) + "Status"

                strHdrItemNet = strHdrItemNet.PadLeft(7) + getValueByKey("CLSPSO026")
                strHdrItemNet = strHdrItemNet.PadRight(16) + getValueByKey("CLSBL011")
                sbHdrItem.Append(strHdrItemNet)

                Dim strHdrItemRes As String = ""

                If PrintBLTransaction = PrintBLTransactionSet.BirthListStatus Then
                    'strHdrItemRes = strHdrItemRes.PadLeft(8) + "Res"
                    'strHdrItemRes = strHdrItemRes.PadRight(10) + "CLP"

                    strHdrItemRes = strHdrItemRes.PadLeft(8) + getValueByKey("CLSPSO028")
                    strHdrItemRes = strHdrItemRes.PadRight(10) + getValueByKey("CLSPSO029")
                    sbHdrItem.Append(strHdrItemRes)
                Else
                    'strHdrItemRes = strHdrItemRes.PadLeft(6) + "Res"
                    'strHdrItemRes = strHdrItemRes.PadRight(12) + "CLP"
                    strHdrItemRes = strHdrItemRes.PadLeft(6) + getValueByKey("CLSPSO028")
                    strHdrItemRes = strHdrItemRes.PadRight(12) + getValueByKey("CLSPSO029")
                    sbHdrItem.Append(strHdrItemRes)
                End If


                'If PrintBLTransaction = PrintBLTransactionSet.BirthListStatus Then
                '    Dim strHdrItemFinalDiscount As String = ""
                '    strHdrItemFinalDiscount = strHdrItemFinalDiscount.PadRight(2) + "Final Dis."
                '    sbHdrItem.Append(strHdrItemFinalDiscount)
                'End If


                'Dim strFinalDiscount As String = ""
                'strFinalDiscount = strFinalDiscount.PadLeft(1) + "Final Discount"
                'strFinalDiscount = strFinalDiscount.PadRight(10)
                'sbHdrItem.Append(strFinalDiscount)

            ElseIf PrintBLTransaction = PrintBLTransactionSet.SaleBirthListItem Or PrintBLTransaction = PrintBLTransactionSet.EditBirthListItem Then
                sbHdrItem.Append(vbCrLf + strLineA4 + vbCrLf)
                Dim strHdrItem As String = ""
                'strHdrItem = "Item Code"
                strHdrItem = getValueByKey("CLSPSO020")

                'strHdrItem = strHdrItem.PadRight(WArticleCode) + "Description"
                strHdrItem = strHdrItem.PadRight(WArticleCode) + getValueByKey("CLSPSO021")
                sbHdrItem.Append(strHdrItem)

                Dim strHdrItemQty As String = ""
                'strHdrItemQty = strHdrItemQty.PadLeft(WArticleDescription + WQty - 12) + "Qty"
                'strHdrItemQty = strHdrItemQty.PadRight(WArticleDescription + WQty) + "Price"
                strHdrItemQty = strHdrItemQty.PadLeft(WArticleDescription + WQty - 12) + getValueByKey("CLSPSO022")
                strHdrItemQty = strHdrItemQty.PadRight(WArticleDescription + WQty) + getValueByKey("CLSPSO023")
                sbHdrItem.Append(strHdrItemQty)

                Dim strHdrItemNet As String = ""
                'strHdrItemNet = strHdrItemNet.PadLeft(WRate + 2) + "Tax"
                'strHdrItemNet = strHdrItemNet.PadRight(WTaxAmount + WRate + 8) + "Net"

                strHdrItemNet = strHdrItemNet.PadLeft(WRate + 2) + getValueByKey("CLSPSO025")
                strHdrItemNet = strHdrItemNet.PadRight(WTaxAmount + WRate + 8) + getValueByKey("CLSPSO026")
                sbHdrItem.Append(strHdrItemNet)

            ElseIf PrintBLTransaction = PrintBLTransactionSet.GiftVoucher Then
                sbHdrItem.Append(vbCrLf + strLineA4 + vbCrLf)
                Dim strHdrItem As String = ""
                'strHdrItem = "Item Code"
                strHdrItem = getValueByKey("CLSPSO020")
                'strHdrItem = strHdrItem.PadRight(WArticleCode) + "Description"
                strHdrItem = strHdrItem.PadRight(WArticleCode) + getValueByKey("CLSPSO021")
                sbHdrItem.Append(strHdrItem)
                Dim strHdrItemQty As String = ""
                'strHdrItemQty = strHdrItemQty.PadLeft(WArticleDescription + WQty - 12) + "Qty"
                'strHdrItemQty = strHdrItemQty.PadRight(WArticleDescription + WQty - 4) + "PickUp"

                strHdrItemQty = strHdrItemQty.PadLeft(WArticleDescription + WQty - 12) + getValueByKey("CLSPSO022")
                strHdrItemQty = strHdrItemQty.PadRight(WArticleDescription + WQty - 4) + getValueByKey("CLSPSO027")
                sbHdrItem.Append(strHdrItemQty)

            End If
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Public Function A4GetSalesItemsDetails(ByVal _dtBirthListItemDetails As DataTable, ByRef sbHdrItemInfo As StringBuilder, ByRef sbHdrItem As StringBuilder, ByRef iItemDetailHeight As Single) As Boolean
        Try
            A4SalesItemHdr(sbHdrItem)
            Dim strArticleCode As String = ""
            Dim strArticleDesc As String = ""
            Dim iPurchasedQty As Integer
            Dim strPurchasedQty As String = ""
            Dim iPrice As String = strZero
            Dim strNetAmount As String = strZero
            Dim strReservedQty As String = strZero
            Dim strCLP As String = "0"
            Dim itemCount As Integer = 1
            Dim strItemStatus As String = ""
            Dim strFinalDiscount As String = ""

            Dim headerArray(_dtBirthListItemDetails.Rows.Count - 1) As String
            Dim cnt As Integer = 0
            If PrintBLTransaction = PrintBLTransactionSet.CreateBirthList Then
                'For all items 
                For Each drGrid As DataRow In _dtBirthListItemDetails.Rows
                    If Not (drGrid.RowState = DataRowState.Deleted) Then

                        ValidateDataRow(drGrid("ArticleCode"), strArticleCode)

                        strArticleCode = strArticleCode.PadRight(WArticleCode)

                        ValidateDataRow(drGrid("ArticleDescription"), strArticleDesc)

                        ValidateDataRow(drGrid("Rate"), iPrice)
                        iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, BLRoundOff)
                        ValidateDataRow(drGrid("RequstedQty"), strPurchasedQty)
                        ValidateDataRow(drGrid("Amount"), strNetAmount)
                        strNetAmount = PrintFormatCurrency(strNetAmount, DefaultCurrency, BLRoundOff)
                        strArticleDesc = strArticleDesc.PadRight(WArticleDescription)


                        strPurchasedQty = strPurchasedQty.ToString()
                        strPurchasedQty = strPurchasedQty.PadLeft(WQty - 2)
                        strPurchasedQty = strPurchasedQty.PadRight(WQty - 2)

                        iPrice = iPrice.PadLeft(WRate + 4)
                        iPrice = iPrice.PadRight(WRate + 4)

                        strNetAmount = strNetAmount.PadLeft(WNetAmount)
                        strNetAmount = strNetAmount.PadRight(WNetAmount)



                        If drGrid("BookedQty") > 0 Then
                            strItemStatus = "Sold"
                        Else
                            strItemStatus = "Open"
                        End If
                        strItemStatus = strItemStatus.PadLeft(WStatus)
                        strItemStatus = strItemStatus.PadRight(WStatus)

                        ValidateDataRow(drGrid("ReservedQty"), strReservedQty)
                        strReservedQty = strReservedQty.PadLeft(WReservedQty)
                        strReservedQty = strReservedQty.PadRight(WReservedQty)

                        ValidateDataRow(drGrid("IsCLP"), strCLP)

                        strCLP = strCLP.PadLeft(6)
                        strCLP = strCLP.PadRight(6)


                        sbHdrItemInfo.Append(strArticleCode & strArticleDesc + strPurchasedQty.ToString() & iPrice & strNetAmount & strItemStatus & strReservedQty & strCLP + vbCrLf)


                    End If
                Next
            ElseIf PrintBLTransaction = PrintBLTransactionSet.BirthListStatus Then
                Dim objTotalNetAMount As Decimal
                Dim strCustomerNamePicked As StringBuilder
                Dim strCustomerNamePaid As StringBuilder

                For Each drGrid As DataRow In _dtBirthListItemDetails.Rows
                    If Not (drGrid.RowState = DataRowState.Deleted) Then
                        ValidateDataRow(drGrid("ArticleCode"), strArticleCode)
                        strArticleCode = strArticleCode.PadRight(WArticleCode)
                        ValidateDataRow(drGrid("description"), strArticleDesc)
                        'iPrice = PrintFormatCurrency(drGrid("Rate"), DefaultCurrency, BLRoundOff)
                        ValidateDataRow(drGrid("Rate"), iPrice)
                        If Not iPrice.Trim() = String.Empty Then
                            iPrice = PrintFormatCurrency(CDbl(clsCommon.CheckIfBlank(iPrice)), DefaultCurrency)
                        Else
                            iPrice = PrintFormatCurrency(0, DefaultCurrency)
                        End If
                        iPrice = "#" & iPrice
                        ValidateDataRow(drGrid("DisplayQty"), strPurchasedQty)
                        ValidateDataRow(drGrid("NetAmt"), objTotalNetAMount)
                        'strNetAmount = PrintFormatCurrency(CDbl(objTotalNetAMount), DefaultCurrency)
                        strArticleDesc = strArticleDesc.PadRight(WArticleDescription)
                        strPurchasedQty = strPurchasedQty.ToString()
                        strPurchasedQty = strPurchasedQty.PadLeft(WQty - 2)
                        strPurchasedQty = strPurchasedQty.PadRight(WQty - 2)
                        If iPrice Is Nothing Then
                            iPrice = ""
                        End If
                        iPrice = iPrice.PadLeft(WRate + 4)
                        iPrice = iPrice.PadRight(WRate + 4)

                        'If strNetAmount Is Nothing Then
                        'strNetAmount = ""
                        'End If
                        'strNetAmount = strNetAmount.PadLeft(WNetAmount)
                        'strNetAmount = strNetAmount.PadRight(WNetAmount)
                        'ValidateDataRow(drGrid("Status"), strItemStatus)
                        'strItemStatus = strItemStatus.PadLeft(WStatus + 2)
                        'strItemStatus = strItemStatus.PadRight(WStatus + 2)
                        ValidateDataRow(drGrid("ReservedQty"), strReservedQty)
                        strReservedQty = strReservedQty.PadLeft(WReservedQty)
                        strReservedQty = strReservedQty.PadRight(WReservedQty)
                        ValidateDataRow(drGrid("IsCLP"), strCLP)

                        strCLP = strCLP.PadLeft(6)
                        strCLP = strCLP.PadRight(6)

                        strCustomerNamePicked = New StringBuilder(String.Empty)
                        strCustomerNamePaid = New StringBuilder(String.Empty)
                        Dim strFilterCondition As String = "Rate =" & clsCommon.ConvertToEnglish(drGrid("Rate")) & " and DisplayQty=0 and ArticleCode = '" & strArticleCode.Trim & "' and Status in ('Paid','Picked up')"
                        Dim dvPicked As New DataView(_dtBirthListItemDetails, strFilterCondition, "CustomerName Asc", DataViewRowState.CurrentRows)
                        If drGrid("DisplayQty") > 0 Then
                            Dim cust As String = String.Empty
                            For Each drv As DataRow In dvPicked.ToTable.Rows
                                If Not cust = drv("CustomerName") Then
                                    strCustomerNamePicked.Append(getValueByKey("CLBL08") & " :  ".PadLeft(24) & getValueByKey("CLBL04") & " " & drv("CustomerName") & vbCrLf)
                                    cust = drv("CustomerName")
                                End If
                            Next
                        End If

                        strFilterCondition = "Rate =" & clsCommon.ConvertToEnglish(drGrid("Rate")) & " and ArticleCode = '" & strArticleCode.Trim & "' and Status in ('Paid','Picked up')"
                        Dim dvPaid As New DataView(_dtBirthListItemDetails, strFilterCondition, "CustomerName Asc", DataViewRowState.CurrentRows)
                        If drGrid("DisplayQty") > 0 Then
                            Dim cust As String = String.Empty
                            For Each drv As DataRow In dvPaid.ToTable.Rows
                                If Not cust = drv("CustomerName") Then
                                    strCustomerNamePaid.Append(getValueByKey("CLBL08") & " :  ".PadLeft(24) & getValueByKey("CLBL04") & " " & drv("CustomerName") & vbCrLf)
                                    cust = drv("CustomerName")
                                End If
                            Next
                            'ValidateDataRow(drGrid("CustomerName"), strCustomerName)
                        End If

                        Dim strpickup As String = String.Empty
                        Dim stropen As String = String.Empty
                        Dim strpaid As String = String.Empty

                        Dim flg As Boolean = True

                        Dim x As Integer = 0
                        For x = 0 To headerArray.Count - 1
                            If Not headerArray(x) = Nothing Then
                                If drGrid("Status") = "PAID" Then
                                    If headerArray(x).Contains("#" & drGrid("Rate").ToString()) And headerArray(x).ToUpper().Contains(drGrid("Status")) And headerArray(x).Contains(drGrid("ArticleCode")) Then
                                        flg = False
                                        Exit For
                                    End If
                                Else
                                    If headerArray(x).Contains("#" & drGrid("Rate").ToString()) And headerArray(x).Contains(drGrid("Status")) And headerArray(x).Contains(drGrid("ArticleCode")) Then
                                        flg = False
                                        Exit For
                                    End If
                                End If

                            End If
                        Next

                        'rohit
                        'If Not sbHdrItemInfo.ToString.Contains(strArticleDesc) Then
                        If flg And drGrid("DisplayQty") > 0 Then
                            strFilterCondition = "Rate =" & clsCommon.ConvertToEnglish(drGrid("Rate")) & " and ArticleCode = '" & strArticleCode.Trim & "' and Status = 'Picked up'"
                            Dim iPickedup As Integer = IIf(BirthListItemDetails.Compute("sum(DisplayQty)", strFilterCondition) Is DBNull.Value, 0, BirthListItemDetails.Compute("sum(DisplayQty)", strFilterCondition))

                            strFilterCondition = "Rate =" & clsCommon.ConvertToEnglish(drGrid("Rate")) & " and ArticleCode = '" & strArticleCode.Trim & "' and Status = 'Paid'"
                            Dim iPaid As Integer = IIf(BirthListItemDetails.Compute("sum(DisplayQty)", strFilterCondition) Is DBNull.Value, 0, BirthListItemDetails.Compute("sum(DisplayQty)", strFilterCondition))

                            strFilterCondition = "Rate =" & clsCommon.ConvertToEnglish(drGrid("Rate")) & " and ArticleCode = '" & strArticleCode.Trim & "' and Status = 'Open'"
                            Dim iOpen As Integer = IIf(BirthListItemDetails.Compute("sum(DisplayQty)", strFilterCondition) Is DBNull.Value, 0, BirthListItemDetails.Compute("sum(DisplayQty)", strFilterCondition))
                            If iPickedup > 0 Then
                                'Change by ashish on Dec 1, 2010
                                'This change is to handle the null value for sum(netamt)
                                'sbHdrItemInfo.Append(strArticleCode & strArticleDesc + iPickedup.ToString().PadLeft(WQty - 2).PadRight(WQty - 2) & iPrice & PrintFormatCurrency(CDbl(BirthListItemDetails.Compute("sum(NetAmt)", "ArticleCode = '" & strArticleCode & "' and Status = 'Picked up'")), DefaultCurrency).PadLeft(WNetAmount).PadRight(WNetAmount) & (getValueByKey("CLBL05").PadLeft(WStatus + 2)).PadRight(WStatus + 2) & strReservedQty & strCLP + vbCrLf & strCustomerNamePicked.ToString & vbCrLf)
                                'sbHdrItemInfo.Append(strArticleCode & strArticleDesc + iPickedup.ToString().PadLeft(WQty - 2).PadRight(WQty - 2) & iPrice & PrintFormatCurrency(CDbl(IIf(BirthListItemDetails.Compute("sum(NetAmt)", "ArticleCode = '" & strArticleCode & "' and Status = 'Picked up'") Is DBNull.Value, 0, BirthListItemDetails.Compute("sum(NetAmt)", "ArticleCode = '" & strArticleCode & "' and Status = 'Picked up'"))), DefaultCurrency).PadLeft(WNetAmount).PadRight(WNetAmount) & (getValueByKey("CLBL05").PadLeft(WStatus + 2)).PadRight(WStatus + 2) & strReservedQty & strCLP + vbCrLf & strCustomerNamePicked.ToString & vbCrLf)
                                strFilterCondition = "Rate =" & clsCommon.ConvertToEnglish(drGrid("Rate")) & " and ArticleCode = '" & strArticleCode.Trim & "' and Status = 'Picked up'"
                                strpickup = strArticleCode & strArticleDesc + iPickedup.ToString().PadLeft(WQty - 2).PadRight(WQty - 2) & iPrice & PrintFormatCurrency(CDbl(IIf(BirthListItemDetails.Compute("sum(NetAmt)", strFilterCondition) Is DBNull.Value, 0, BirthListItemDetails.Compute("sum(NetAmt)", strFilterCondition))), DefaultCurrency).PadLeft(WNetAmount).PadRight(WNetAmount) & (getValueByKey("CLBL05").PadLeft(WStatus + 2)).PadRight(WStatus + 2) & strReservedQty & strCLP + vbCrLf & strCustomerNamePicked.ToString & vbCrLf
                                'end of change
                            End If

                            If iPaid > 0 Then
                                'Change by ashish on Dec 1, 2010
                                'This change is to handle the null value for sum(netamt)
                                'sbHdrItemInfo.Append(strArticleCode & strArticleDesc + iPaid.ToString().PadLeft(WQty - 2).PadRight(WQty - 2) & iPrice & PrintFormatCurrency(CDbl(BirthListItemDetails.Compute("sum(NetAmt)", "ArticleCode = '" & strArticleCode & "' and Status = 'Paid'")), DefaultCurrency).PadLeft(WNetAmount).PadRight(WNetAmount) & (getValueByKey("CLBL06").PadLeft(WStatus + 2)).PadRight(WStatus + 2) & strReservedQty & strCLP + vbCrLf & strCustomerNamePaid.ToString & vbCrLf)
                                'sbHdrItemInfo.Append(strArticleCode & strArticleDesc + iPaid.ToString().PadLeft(WQty - 2).PadRight(WQty - 2) & iPrice & PrintFormatCurrency(CDbl(IIf(BirthListItemDetails.Compute("sum(NetAmt)", "ArticleCode = '" & strArticleCode & "' and Status = 'Paid'") Is DBNull.Value, 0, BirthListItemDetails.Compute("sum(NetAmt)", "ArticleCode = '" & strArticleCode & "' and Status = 'Paid'"))), DefaultCurrency).PadLeft(WNetAmount).PadRight(WNetAmount) & (getValueByKey("CLBL06").PadLeft(WStatus + 2)).PadRight(WStatus + 2) & strReservedQty & strCLP + vbCrLf & strCustomerNamePaid.ToString & vbCrLf)
                                strFilterCondition = "Rate =" & clsCommon.ConvertToEnglish(drGrid("Rate")) & " and ArticleCode = '" & strArticleCode.Trim & "' and Status = 'Paid'"
                                strpaid = strArticleCode & strArticleDesc + iPaid.ToString().PadLeft(WQty - 2).PadRight(WQty - 2) & iPrice & PrintFormatCurrency(CDbl(IIf(BirthListItemDetails.Compute("sum(NetAmt)", strFilterCondition) Is DBNull.Value, 0, BirthListItemDetails.Compute("sum(NetAmt)", strFilterCondition))), DefaultCurrency).PadLeft(WNetAmount).PadRight(WNetAmount) & (getValueByKey("CLBL06").PadLeft(WStatus + 2)).PadRight(WStatus + 2) & strReservedQty & strCLP + vbCrLf & strCustomerNamePaid.ToString & vbCrLf
                                'end of change
                            End If

                            If iOpen > 0 Then
                                'Change by ashish on Dec 1, 2010
                                'This change is to handle the null value for sum(netamt)
                                'sbHdrItemInfo.Append(strArticleCode & strArticleDesc + iOpen.ToString().PadLeft(WQty - 2).PadRight(WQty - 2) & iPrice & PrintFormatCurrency(CDbl(BirthListItemDetails.Compute("sum(NetAmt)", "ArticleCode = '" & strArticleCode & "' and Status = 'Open'")), DefaultCurrency).PadLeft(WNetAmount).PadRight(WNetAmount) & (getValueByKey("CLBL07").PadLeft(WStatus + 2)).PadRight(WStatus + 2) & strReservedQty & strCLP + vbCrLf)
                                'sbHdrItemInfo.Append(strArticleCode & strArticleDesc + iOpen.ToString().PadLeft(WQty - 2).PadRight(WQty - 2) & iPrice & PrintFormatCurrency(CDbl(IIf(BirthListItemDetails.Compute("sum(NetAmt)", "ArticleCode = '" & strArticleCode & "' and Status = 'Open'") Is DBNull.Value, 0, BirthListItemDetails.Compute("sum(NetAmt)", "ArticleCode = '" & strArticleCode & "' and Status = 'Open'"))), DefaultCurrency).PadLeft(WNetAmount).PadRight(WNetAmount) & (getValueByKey("CLBL07").PadLeft(WStatus + 2)).PadRight(WStatus + 2) & strReservedQty & strCLP + vbCrLf)
                                strFilterCondition = "Rate =" & clsCommon.ConvertToEnglish(drGrid("Rate")) & " and ArticleCode = '" & strArticleCode.Trim & "' and Status = 'Open'"
                                stropen = strArticleCode & strArticleDesc + iOpen.ToString().PadLeft(WQty - 2).PadRight(WQty - 2) & iPrice & PrintFormatCurrency(CDbl(IIf(BirthListItemDetails.Compute("sum(NetAmt)", strFilterCondition) Is DBNull.Value, 0, BirthListItemDetails.Compute("sum(NetAmt)", strFilterCondition))), DefaultCurrency).PadLeft(WNetAmount).PadRight(WNetAmount) & (getValueByKey("CLBL07").PadLeft(WStatus + 2)).PadRight(WStatus + 2) & strReservedQty & strCLP + vbCrLf
                                'end of change
                            End If
                            Dim sb As New StringBuilder
                            sb.Append(strpickup)
                            sb.Append(strpaid)
                            sb.Append(stropen)

                            headerArray(cnt) = sb.ToString()

                            cnt += 1
                        End If

                        itemCount += 1
                    End If
                Next
            ElseIf PrintBLTransaction = PrintBLTransactionSet.SaleBirthListItem Then

                If Not IsDeliveredItem Then ' Only for Sold items 
                    For Each drGrid As DataRow In _dtBirthListItemDetails.Rows
                        If Not (drGrid.RowState = DataRowState.Deleted) Then
                            If drGrid("PurchasedQty") > 0 Then
                                ValidateDataRow(drGrid("ArticleCode"), strArticleCode)
                                strArticleCode = strArticleCode.PadRight(WArticleCode)

                                ValidateDataRow(drGrid("Discription"), strArticleDesc)
                                strArticleDesc = strArticleDesc.PadRight(WArticleDescription)

                                ValidateDataRow(drGrid("PurchasedQty"), strPurchasedQty)
                                strPurchasedQty = strPurchasedQty.ToString()
                                strPurchasedQty = strPurchasedQty.PadLeft(WQty)
                                strPurchasedQty = strPurchasedQty.PadRight(WQty)

                                ValidateDataRow(drGrid("SellingPrice"), iPrice)
                                iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, BLRoundOff)
                                iPrice = iPrice.PadLeft(WRate + 5)
                                iPrice = iPrice.PadRight(WRate + 5)

                                ValidateDataRow(drGrid("TaxAmt"), strItemStatus)
                                strItemStatus = PrintFormatCurrency(strItemStatus, DefaultCurrency, BLRoundOff)
                                strItemStatus = strItemStatus.PadLeft(WTaxAmount + 5)
                                strItemStatus = strItemStatus.PadRight(WTaxAmount + 5)

                                ValidateDataRow(drGrid("NetAmount"), strNetAmount)
                                strNetAmount = PrintFormatCurrency(strNetAmount, DefaultCurrency, BLRoundOff)
                                strNetAmount = strNetAmount.PadLeft(WNetAmount + 5)
                                strNetAmount = strNetAmount.PadRight(WNetAmount + 5)
                                sbHdrItemInfo.Append(strArticleCode & strArticleDesc + strPurchasedQty.ToString() & iPrice & strItemStatus & strNetAmount + vbCrLf)

                            End If
                        End If
                    Next
                    A4VocuherSales(sbHdrItemInfo)
                ElseIf IsDeliveredItem Then
                    Dim decTotalPickUpItemNetAmount As Decimal
                    ' For Delivery item
                    For Each drGrid As DataRow In _dtBirthListItemDetails.Rows
                        If Not (drGrid.RowState = DataRowState.Deleted) Then
                            If drGrid("PickupQty") > 0 Then


                                ValidateDataRow(drGrid("ArticleCode"), strArticleCode)
                                strArticleCode = strArticleCode.PadRight(WArticleCode)

                                ValidateDataRow(drGrid("Discription"), strArticleDesc)
                                strArticleDesc = strArticleDesc.PadRight(WArticleDescription)

                                ValidateDataRow(drGrid("PickupQty"), strPurchasedQty)
                                strPurchasedQty = strPurchasedQty.ToString()
                                strPurchasedQty = strPurchasedQty.PadLeft(WQty)
                                strPurchasedQty = strPurchasedQty.PadRight(WQty)

                                ValidateDataRow(drGrid("SellingPrice"), iPrice)
                                iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, BLRoundOff)
                                iPrice = iPrice.PadLeft(WRate + 5)
                                iPrice = iPrice.PadRight(WRate + 5)

                                ValidateDataRow(drGrid("TaxAmt"), strItemStatus)
                                strItemStatus = PrintFormatCurrency(strItemStatus, DefaultCurrency, BLRoundOff)
                                strItemStatus = strItemStatus.PadLeft(WTaxAmount + 5)
                                strItemStatus = strItemStatus.PadRight(WTaxAmount + 5)

                                If Not drGrid("SellingPrice") Is Nothing AndAlso Not drGrid("SellingPrice") Is DBNull.Value Then
                                    decTotalPickUpItemNetAmount = drGrid("SellingPrice") * drGrid("PickupQty")

                                    If Not drGrid("ExclusiveTax") Is Nothing AndAlso Not drGrid("ExclusiveTax") Is DBNull.Value Then
                                        decTotalPickUpItemNetAmount += drGrid("ExclusiveTax")
                                    End If
                                Else
                                    decTotalPickUpItemNetAmount = Decimal.Zero
                                End If
                                strNetAmount = PrintFormatCurrency(decTotalPickUpItemNetAmount, DefaultCurrency, BLRoundOff)
                                strNetAmount = strNetAmount.PadLeft(WNetAmount + 5)
                                strNetAmount = strNetAmount.PadRight(WNetAmount + 5)
                                sbHdrItemInfo.Append(strArticleCode & strArticleDesc + strPurchasedQty.ToString() & iPrice & strItemStatus & strNetAmount + vbCrLf)
                                itemCount += 1
                            End If

                        End If
                    Next
                End If

            ElseIf PrintBLTransaction = PrintBLTransactionSet.EditBirthListItem Then

                Dim decTotalNetAmount As Decimal = 0
                Dim decSellingPrice As Decimal = 0
                If Not IsDeliveredItem Then
                    For Each drGrid As DataRow In _dtBirthListItemDetails.Rows
                        If Not (drGrid.RowState = DataRowState.Deleted) Then
                            If drGrid("PurchasedQty") > 0 Or drGrid("PickUpQty") > 0 Then


                                decTotalNetAmount = 0
                                strPurchasedQty = 0
                                ValidateDataRow(drGrid("ArticleCode"), strArticleCode)
                                strArticleCode = strArticleCode.PadRight(WArticleCode)

                                ValidateDataRow(drGrid("Discription"), strArticleDesc)
                                strArticleDesc = strArticleDesc.PadRight(WArticleDescription)


                                If Not drGrid("SellingPrice") Is Nothing AndAlso Not drGrid("SellingPrice") Is DBNull.Value Then
                                    decSellingPrice = drGrid("SellingPrice")
                                    iPrice = decSellingPrice
                                Else
                                    decSellingPrice = 0
                                    iPrice = 0

                                End If
                                iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, BLRoundOff)
                                iPrice = iPrice.PadLeft(WRate + 5)
                                iPrice = iPrice.PadRight(WRate + 5)


                                If Not drGrid("PurchasedQty") Is Nothing AndAlso Not drGrid("PurchasedQty") Is DBNull.Value Then

                                    ValidateDataRow(drGrid("CurrentPurchasedAmount"), decTotalNetAmount)
                                    ValidateDataRow(drGrid("PurchasedQty"), strPurchasedQty)
                                    If CInt(strPurchasedQty) = 0 Then
                                        Dim decSellingPrice1 As Decimal = 0
                                        Dim decExclusiveTax As Decimal = 0
                                        ValidateDataRow(drGrid("PickUpQty"), strPurchasedQty)
                                        ValidateDataRow(drGrid("SellingPrice"), decSellingPrice1)
                                        ValidateDataRow(drGrid("ExclusiveTax"), decExclusiveTax)
                                        decTotalNetAmount = Decimal.Add(CInt(strPurchasedQty) * decSellingPrice1, decExclusiveTax)

                                    End If
                                Else
                                    Dim decSellingPrice_PickUP As Decimal = 0
                                    Dim decExclusiveTax As Decimal = 0
                                    ValidateDataRow(drGrid("PickUpQty"), strPurchasedQty)
                                    ValidateDataRow(drGrid("SellingPrice"), decSellingPrice_PickUP)
                                    ValidateDataRow(drGrid("ExclusiveTax"), decExclusiveTax)
                                    If strPurchasedQty = String.Empty Then
                                        strPurchasedQty = 0
                                    End If
                                    decTotalNetAmount = Decimal.Add(CInt(strPurchasedQty) * decSellingPrice_PickUP, decExclusiveTax)
                                End If



                                'If Not drGrid("PurchasedQty") Is Nothing AndAlso Not drGrid("PurchasedQty") Is DBNull.Value Then
                                '    If Not drGrid("BookedQty") Is Nothing AndAlso Not drGrid("BookedQty") Is DBNull.Value Then
                                '        strPurchasedQty = Decimal.Add(drGrid("PurchasedQty"), drGrid("BookedQty"))
                                '        decTotalNetAmount = Decimal.Add(drGrid("CurrentPurchasedAmount"), (drGrid("BookedQty") * decSellingPrice))
                                '    Else
                                '        decTotalNetAmount = drGrid("CurrentPurchasedAmount")
                                '        strPurchasedQty = drGrid("PurchasedQty")
                                '    End If
                                'Else
                                '    If Not drGrid("BookedQty") Is Nothing AndAlso Not drGrid("BookedQty") Is DBNull.Value Then
                                '        decTotalNetAmount = drGrid("BookedQty") * decSellingPrice
                                '        strPurchasedQty = drGrid("BookedQty")
                                '    Else
                                '        decTotalNetAmount = 0
                                '        strPurchasedQty = 0
                                '    End If

                                'End If

                                strPurchasedQty = strPurchasedQty.ToString()
                                strPurchasedQty = strPurchasedQty.PadLeft(WQty)
                                strPurchasedQty = strPurchasedQty.PadRight(WQty)

                                ValidateDataRow(drGrid("TaxAmt"), strItemStatus)
                                strItemStatus = PrintFormatCurrency(strItemStatus, DefaultCurrency, BLRoundOff)
                                strItemStatus = strItemStatus.PadLeft(WTaxAmount + 5)
                                strItemStatus = strItemStatus.PadRight(WTaxAmount + 5)
                                strNetAmount = PrintFormatCurrency(decTotalNetAmount, DefaultCurrency, BLRoundOff)
                                strNetAmount = strNetAmount.PadLeft(WNetAmount + 5)
                                strNetAmount = strNetAmount.PadRight(WNetAmount + 5)

                                sbHdrItemInfo.Append(strArticleCode & strArticleDesc + strPurchasedQty.ToString() & iPrice & strItemStatus & strNetAmount + vbCrLf)
                                itemCount += 1
                            End If
                        End If
                    Next
                Else
                    Dim iCalNetAmount As Decimal
                    Dim iCalTotolNetAmt As Decimal = Decimal.Zero
                    For Each drDtl As DataRow In _dtBirthListItemDetails.Rows
                        If Not drDtl.RowState = DataRowState.Deleted Then
                            If (drDtl("PickUpQty") > 0) Then
                                ValidateDataRow(drDtl("ArticleCode"), strArticleCode)
                                strArticleCode = strArticleCode.PadRight(WArticleCode)

                                ValidateDataRow(drDtl("Discription"), strArticleDesc)
                                strArticleDesc = strArticleDesc.PadRight(WArticleDescription)

                                ValidateDataRow(drDtl("PickUpQty"), iPurchasedQty)
                                strPurchasedQty = iPurchasedQty.ToString()
                                strPurchasedQty = strPurchasedQty.PadLeft(WQty)
                                strPurchasedQty = strPurchasedQty.PadRight(WQty)

                                ValidateDataRow(drDtl("SellingPrice"), iPrice)
                                iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, BLRoundOff)
                                iPrice = iPrice.PadLeft(WRate + 5)
                                iPrice = iPrice.PadRight(WRate + 5)

                                ValidateDataRow(drDtl("TaxAmt"), strItemStatus)
                                strItemStatus = PrintFormatCurrency(strItemStatus, DefaultCurrency, BLRoundOff)
                                strItemStatus = strItemStatus.PadLeft(WTaxAmount + 5)
                                strItemStatus = strItemStatus.PadRight(WTaxAmount + 5)

                                If Not drDtl("SellingPrice") Is Nothing AndAlso Not drDtl("SellingPrice") Is DBNull.Value Then
                                    iCalNetAmount = drDtl("SellingPrice") * drDtl("PickUpQty")
                                    If Not drDtl("ExclusiveTax") Is Nothing AndAlso Not drDtl("ExclusiveTax") Is DBNull.Value Then
                                        iCalNetAmount += drDtl("ExclusiveTax")
                                    End If
                                Else
                                    iCalNetAmount = Decimal.Zero
                                End If
                                strNetAmount = PrintFormatCurrency(iCalNetAmount, DefaultCurrency, BLRoundOff)
                                strNetAmount = strNetAmount.PadLeft(WNetAmount + 5)
                                strNetAmount = strNetAmount.PadRight(WNetAmount + 5)
                                iItemDetailHeight = iItemDetailHeight + 15

                                sbHdrItemInfo.Append(strArticleCode & strArticleDesc & strPurchasedQty & iPrice & strItemStatus & strNetAmount + vbCrLf)
                            End If
                            If (drDtl("CurrentReturnQty") > 0) Then

                                ValidateDataRow(drDtl("ArticleCode"), strArticleCode)
                                strArticleCode = strArticleCode.PadRight(WArticleCode)

                                ValidateDataRow(drDtl("Discription"), strArticleDesc)
                                strArticleDesc = strArticleDesc.PadRight(WArticleDescription)

                                ValidateDataRow(drDtl("CurrentReturnQty"), iPurchasedQty)
                                strPurchasedQty = iPurchasedQty.ToString()
                                strPurchasedQty = strPurchasedQty.PadRight(WQty)
                                strPurchasedQty = strPurchasedQty.PadLeft(WQty)

                                ValidateDataRow(drDtl("SellingPrice"), iPrice)
                                iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, BLRoundOff)
                                iPrice = iPrice.PadRight(WRate + 5)
                                iPrice = iPrice.PadLeft(WRate + 5)

                                ValidateDataRow(drDtl("TaxAmt"), strItemStatus)
                                strItemStatus = PrintFormatCurrency(strItemStatus, DefaultCurrency, BLRoundOff)
                                strItemStatus = strItemStatus.PadRight(WTaxAmount + 5)
                                strItemStatus = strItemStatus.PadRight(WTaxAmount + 5)

                                ValidateDataRow(drDtl("CurrentReturnQty"), iCalNetAmount)
                                iCalNetAmount = iCalNetAmount * CDbl(clsCommon.CheckIfBlank(iPrice))

                                strNetAmount = PrintFormatCurrency(iCalNetAmount, DefaultCurrency, BLRoundOff)
                                strNetAmount = strNetAmount.PadRight(WNetAmount + 5)
                                strNetAmount = strNetAmount.PadLeft(WNetAmount + 5)
                                sbHdrItemInfo.Append(strArticleCode & strArticleDesc & strPurchasedQty & iPrice & strItemStatus & strNetAmount + vbCrLf)
                            End If
                        End If
                    Next
                End If
            ElseIf PrintBLTransaction = PrintBLTransactionSet.GiftVoucher Then
                Dim iPickupQty As Decimal
                Dim strPickUpQTy As String = ""
                For Each drGrid As DataRow In _dtBirthListItemDetails.Rows
                    If Not (drGrid.RowState = DataRowState.Deleted) Then
                        If drGrid("PurchasedQty") > 0 Then
                            ValidateDataRow(drGrid("ArticleCode"), strArticleCode)
                            strArticleCode = strArticleCode.PadRight(WArticleCode)

                            ValidateDataRow(drGrid("Discription"), strArticleDesc)
                            strArticleDesc = strArticleDesc.PadRight(WArticleDescription)

                            ValidateDataRow(drGrid("PurchasedQty"), strPurchasedQty)
                            strPurchasedQty = strPurchasedQty.ToString()
                            strPurchasedQty = strPurchasedQty.PadLeft(WQty)
                            strPurchasedQty = strPurchasedQty.PadRight(WQty)

                            If _dtBirthListItemDetails.Columns.Contains("PickUpQty") Then
                                If Not drGrid("PickUpQty") Is DBNull.Value Then
                                    iPickupQty = drGrid("PickUpQty")
                                    strPickUpQTy = iPickupQty.ToString()
                                    strPickUpQTy = strPickUpQTy.PadLeft(WQty)
                                    strPickUpQTy = strPickUpQTy.PadRight(WQty)
                                End If
                            End If
                            iItemDetailHeight = iItemDetailHeight + 15
                            sbHdrItemInfo.Append(strArticleCode & strArticleDesc + strPurchasedQty.ToString() & strPickUpQTy + vbCrLf)
                            itemCount += 1
                        End If
                    End If
                Next
            End If

            Dim y As Integer = 0
            For y = 0 To headerArray.Count - 1
                If Not headerArray(y) = Nothing Then
                    Dim tmpString As String = headerArray(y).Replace("#", "")
                    sbHdrItemInfo.Append(tmpString)
                End If
            Next
            If sbHdrItemInfo.Length = 0 Then
                Return False
            End If

            sbHdrItemInfo.Append(vbCrLf + strLineA4)

            If Not PrintBLTransaction = PrintBLTransactionSet.CreateBirthList And Not PrintBLTransaction = PrintBLTransactionSet.BirthListStatus Then
                If Not BirthListItemDetails Is Nothing Then
                    If BirthListItemDetails.Columns.Contains("PickUpQty") AndAlso BirthListItemDetails.Rows.Count > 0 Then
                        Dim iPickUpQty As Integer = BirthListItemDetails.Compute("sum(PickUpQty)", " ")
                        If iPickUpQty > 0 Then
                            IsDeliveredItem = True
                        End If
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            MsgBox(ex.Message, , getValueByKey("CLAE05"))
            Return False
        End Try

    End Function

    Private Function A4VocuherSales(ByRef strHdrItemInfo As StringBuilder, Optional ByRef iTotalBirthListItem As Decimal = 0.0, Optional ByRef decNetAmt As Decimal = 0.0, Optional ByRef strTotalQty As String = "", Optional ByRef strNetAmt As String = "", Optional ByRef strTermsCond As String = "", Optional ByRef strLineEnd As String = "") As Boolean
        Try
            If Not IsDeliveredItem Then
                Dim strArticleCode As String = ""
                Dim strArticleDesc As String = ""
                Dim iPurchasedQty As Integer = 0
                Dim iPrice As String = strZero
                Dim iNetAmount As String = strZero
                Dim strPurchasedQty As String = "0"
                Dim strTaxAmount As String = "0"
                If Not _dtVoucherSales Is Nothing Then
                    For Each drGV As DataRow In _dtVoucherSales.Rows

                        ValidateDataRow(drGV("ArticleCode"), strArticleCode)
                        strArticleCode = strArticleCode.PadRight(WArticleCode)

                        ValidateDataRow(drGV("Discription"), strArticleDesc)
                        strArticleDesc = strArticleDesc.PadRight(WArticleDescription)

                        ValidateDataRow(drGV("BookedQty"), iPurchasedQty)
                        strPurchasedQty = iPurchasedQty
                        strPurchasedQty = strPurchasedQty.PadLeft(WQty)
                        strPurchasedQty = strPurchasedQty.PadRight(WQty)

                        ValidateDataRow(drGV("NetAmount"), iNetAmount)
                        iNetAmount = PrintFormatCurrency(iNetAmount, DefaultCurrency, BLRoundOff)
                        iNetAmount = iNetAmount.PadLeft(WNetAmount + 5)
                        iNetAmount = iNetAmount.PadRight(WNetAmount + 5)

                        ValidateDataRow(drGV("SellingPrice"), iPrice)
                        iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, BLRoundOff)
                        iPrice = iPrice.PadLeft(WRate + 5)
                        iPrice = iPrice.PadRight(WRate + 5)

                        strTaxAmount = PrintFormatCurrency(strTaxAmount, DefaultCurrency, BLRoundOff)
                        strTaxAmount = strTaxAmount.PadLeft(WTaxAmount + 5)
                        strTaxAmount = strTaxAmount.PadRight(WTaxAmount + 5)

                        strHdrItemInfo.Append(strArticleCode & strArticleDesc & strPurchasedQty & iPrice & strTaxAmount & iNetAmount + vbCrLf)



                    Next
                End If
                Return True
            Else
                Return True
            End If


        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region


    Public Function L40GetTaxDetails(ByRef strTaxDetailsForInvoice As String) As Boolean
        Try

            If Not _dtTaxDetails Is Nothing AndAlso _dtTaxDetails.Rows.Count > 0 Then

                Dim strTaxName As String = ""
                Dim decTaxvalue As Decimal = Decimal.Zero
                Dim strTaxValue As String = ""
                Dim strTaxCode As String = ""
                Dim KeySortPairs = From d In _dtTaxDetails _
                Group By Key = d("TaxCode").ToString(), SortOrder = d("TaxCode").ToString() _
                Into Group _
                Select Key, TaxAmount = Group.Sum(Function(d) CType(d("TaxAmount"), Nullable(Of Decimal)))
                For Each row In KeySortPairs
                    If (GetTaxName(row.Key.ToString(), strTaxName)) Then
                        strTaxValue = PrintFormatCurrency(row.TaxAmount, DefaultCurrency, DecimalDigits)
                        strTaxDetailsForInvoice = strTaxDetailsForInvoice & vbCrLf + strTaxName.PadRight(18) & strTaxValue.PadLeft(2)
                    Else
                        strTaxValue = PrintFormatCurrency(0.0, DefaultCurrency, DecimalDigits)
                        strTaxDetailsForInvoice = strTaxDetailsForInvoice & vbCrLf + "Not FOund" & strTaxValue.PadLeft(2)
                    End If
                    'strTaxValue = PrintFormatCurrency(row.TaxAmount, DefaultCurrency, DecimalDigits)
                    'strTaxDetailsForInvoice = strTaxDetailsForInvoice + vbCrLf + row.Key.PadRight(18) + strTaxValue.PadLeft(2)
                Next
            End If
            Return True


        Catch ex As Exception

        End Try
    End Function

    Public Function L40Print(ByVal dtCustomerDetails As DataTable, ByVal dtGridData As DataTable, Optional ByVal strBirthListID As String = "") As Boolean
        Try
            Dim sbHdrItemInfo As New System.Text.StringBuilder
            Dim sbHdrItem As New StringBuilder
            If PrintBLTransaction = PrintBLTransactionSet.CreateBirthList Then
                If L40GetSalesItemDetails(BirthListItemDetails, sbHdrItemInfo, sbHdrItem) Then
                    If L40PrintFormat(sbHdrItemInfo, sbHdrItem) Then
                        Return True
                    End If
                End If
            ElseIf PrintBLTransaction = PrintBLTransactionSet.SaleBirthListItem Or PrintBLTransaction = PrintBLTransactionSet.EditBirthListItem Then
                If L40GetSalesItemDetails(BirthListItemDetails, sbHdrItemInfo, sbHdrItem) Then
                    If L40PrintFormat(sbHdrItemInfo, sbHdrItem) Then

                        Dim onlyPickupPrint As Boolean = IsDeliveredItem

                        If Not PrintBLTransaction = PrintBLTransactionSet.CreateBirthList Then
                            If Not BirthListItemDetails Is Nothing Then
                                If BirthListItemDetails.Rows.Count > 0 AndAlso BirthListItemDetails.Columns.Contains("PickUpQty") Then
                                    Dim iPickUpQty As Integer = BirthListItemDetails.Compute("sum(PickUpQty)", " ")
                                    If iPickUpQty > 0 Then
                                        IsDeliveredItem = True
                                    End If
                                End If
                            End If
                        End If

                        If onlyPickupPrint = False Then
                            If IsDeliveredItem Then
                                sbHdrItem.Length = 0
                                sbHdrItemInfo.Length = 0
                                'Delivery Print 

                                'Change by Ashish on 26 Nov 2010
                                'This change is to check the transaction set and appropriately set the document type and print format
                                If PrintBLTransaction = PrintBLTransactionSet.SaleBirthListItem Or PrintBLTransaction = PrintBLTransactionSet.EditBirthListItem Or PrintBLTransaction = PrintBLTransactionSet.GiftVoucher Or PrintBLTransaction = PrintBLTransactionSet.ReturnBirthListItem Then
                                    If IsDeliveredItem Then
                                        strBLDocumentType = "BLOB"
                                    Else
                                        strBLDocumentType = "BLInvc" 'BLINV.
                                    End If
                                ElseIf PrintBLTransaction = PrintBLTransactionSet.BirthListStatus Or PrintBLTransaction = PrintBLTransactionSet.CreateBirthList Then
                                    strBLDocumentType = "BLStatus"
                                End If
                                PrintBLPageSetup = (GetPrintFormat(dtPrinterInfo1.Rows(0)(0), strBLDocumentType)).Rows(0)(0)
                                If PrintBLPageSetup = "A4" Then
                                    Dim iItemDetailHeight As Integer
                                    If A4GetSalesItemsDetails(BirthListItemDetails, sbHdrItemInfo, sbHdrItem, iItemDetailHeight) Then
                                        If A4PrintFormat(sbHdrItemInfo, sbHdrItem, iItemDetailHeight, True) Then
                                            IsDeliveredItem = False
                                            Return True
                                        End If
                                    End If

                                End If
                                If PrintBLPageSetup = "L40" Then
                                    If L40GetSalesItemDetails(BirthListItemDetails, sbHdrItemInfo, sbHdrItem) Then
                                        If L40PrintFormat(sbHdrItemInfo, sbHdrItem, True) Then
                                            IsDeliveredItem = False
                                            Return True
                                        End If
                                    End If
                                End If
                                'End of change

                            Else
                                Return True
                            End If
                        Else
                            Return True
                        End If


                    End If
                    'ElseIf PrintBLTransaction = PrintBLTransactionSet.EditBirthListItem Then

                    '    If Not BirthListItemDetails Is Nothing Then
                    '        If BirthListItemDetails.Rows.Count > 0 AndAlso BirthListItemDetails.Columns.Contains("PickUpQty") Then
                    '            Dim iPickUpQty As Integer = BirthListItemDetails.Compute("sum(PickUpQty)", " ")
                    '            If iPickUpQty > 0 Then
                    '                IsDeliveredItem = True
                    '            End If
                    '        End If
                    '    End If
                    '    If IsDeliveredItem Then
                    '        sbHdrItem.Length = 0
                    '        sbHdrItemInfo.Length = 0
                    '        'Delivery Print 
                    '        If L40GetSalesItemDetails(BirthListItemDetails, sbHdrItemInfo, sbHdrItem) Then
                    '            If L40PrintFormat(sbHdrItemInfo, sbHdrItem, True) Then
                    '                IsDeliveredItem = False
                    '                Return True
                    '            End If
                    '        End If
                    '    End If

                End If
            ElseIf PrintBLTransaction = PrintBLTransactionSet.GiftVoucher Then
                If L40GetSalesItemDetails(BirthListItemDetails, sbHdrItemInfo, sbHdrItem) Then
                    If L40PrintFormat(sbHdrItemInfo, sbHdrItem) Then
                        Return True
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Function

    Public Function L40GetSalesItemDetails(ByVal _dtBirthListItemDetails As DataTable, ByRef sbHdrItemInfo As StringBuilder, ByRef sbHdrItem As StringBuilder) As Boolean
        Try
            L40SalesItemHdr(sbHdrItem)
            Dim strArticleCode As String = ""
            Dim strArticleDesc As String = ""
            Dim iPurchasedQty As Integer = 0
            Dim strPurchasedQty As String = "0"
            Dim iPrice As String = "0"
            Dim iNetAmount As String = strZero
            Dim strReservedQty As String = "0"
            Dim strCLP As String = "0"
            Dim itemCount As Integer = 1
            Dim strItemStatus As String = ""
            If PrintBLTransaction = PrintBLTransactionSet.CreateBirthList Then
                'For all items 
                Dim sbArticleInfo As New StringBuilder
                For Each drGrid As DataRow In _dtBirthListItemDetails.Rows
                    If Not (drGrid.RowState = DataRowState.Deleted) Then

                        ValidateDataRow(drGrid("ArticleCode"), strArticleCode)
                        strArticleCode = strArticleCode.PadRight(WArticleCode)

                        ValidateDataRow(drGrid("ArticleDescription"), strArticleDesc)
                        strArticleDesc = strArticleDesc.PadRight(33)
                        'ISSUE 448
                        'sbArticleInfo = SplitString(strArticleCode + strArticleDesc)
                        sbArticleInfo = SplitString(strArticleCode & vbCrLf)
                        sbArticleInfo = sbArticleInfo.Append(SplitString(strArticleDesc & vbCrLf).ToString())
                        'ISSUE 448

                        ValidateDataRow(drGrid("RequstedQty"), strPurchasedQty)
                        strPurchasedQty = drGrid("RequstedQty")
                        strPurchasedQty = strPurchasedQty.PadRight(8)

                        ValidateDataRow(drGrid("Rate"), iPrice)
                        iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, BLRoundOff)
                        iPrice = iPrice.PadLeft(9)
                        iPrice = iPrice.PadRight(9)

                        ValidateDataRow(drGrid("Amount"), iNetAmount)
                        iNetAmount = PrintFormatCurrency(iNetAmount, DefaultCurrency, BLRoundOff)
                        iNetAmount = iNetAmount.PadLeft(14)
                        iNetAmount = iNetAmount.PadRight(14)

                        'strItemStatus = "Open"
                        strItemStatus = "Open"
                        strItemStatus = strItemStatus.PadLeft(7)
                        strItemStatus = strItemStatus.PadRight(7)

                        ValidateDataRow(drGrid("ReservedQty"), strReservedQty)
                        strReservedQty = strReservedQty.PadLeft(8)
                        strReservedQty = strReservedQty.PadRight(8)
                        'strCLP = drGrid("IsCLP").ToString()
                        'strCLP = strCLP.PadRight(4)
                        sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(strPurchasedQty.ToString() & iPrice & iNetAmount & strReservedQty).ToString() + vbCrLf)
                        itemCount += 1
                    End If
                Next
            ElseIf PrintBLTransaction = PrintBLTransactionSet.SaleBirthListItem Then
                Dim sbArticleInfo As New StringBuilder
                If Not IsDeliveredItem Then ' Only for Sold items 
                    For Each drGrid As DataRow In _dtBirthListItemDetails.Rows
                        If Not (drGrid.RowState = DataRowState.Deleted) Then
                            If drGrid("PurchasedQty") > 0 Then
                                ValidateDataRow(drGrid("ArticleCode"), strArticleCode)
                                strArticleCode = strArticleCode.PadRight(26)

                                ValidateDataRow(drGrid("Discription"), strArticleDesc)
                                strArticleDesc = strArticleDesc.PadRight(34)

                                'ISSUE 448
                                'sbArticleInfo = SplitString(strArticleCode + strArticleDesc)
                                sbArticleInfo = SplitString(strArticleCode & vbCrLf)
                                sbArticleInfo = sbArticleInfo.Append(SplitString(strArticleDesc & vbCrLf).ToString())
                                'ISSUE 448


                                ValidateDataRow(drGrid("PurchasedQty"), strPurchasedQty)
                                strPurchasedQty = strPurchasedQty.ToString()
                                strPurchasedQty = strPurchasedQty.PadRight(8)

                                ValidateDataRow(drGrid("SellingPrice"), iPrice)
                                iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, BLRoundOff)
                                iPrice = iPrice.PadLeft(9)
                                iPrice = iPrice.PadRight(9)

                                ValidateDataRow(drGrid("TaxAmt"), strItemStatus)
                                strItemStatus = PrintFormatCurrency(strItemStatus, DefaultCurrency, BLRoundOff)
                                strItemStatus = strItemStatus.PadLeft(WLTaxAmount)
                                strItemStatus = strItemStatus.PadRight(WLTaxAmount)

                                ValidateDataRow(drGrid("NetAmount"), iNetAmount)
                                iNetAmount = PrintFormatCurrency(iNetAmount, DefaultCurrency, BLRoundOff)
                                iNetAmount = iNetAmount.PadLeft(14)
                                iNetAmount = iNetAmount.PadRight(14)
                                sbHdrItemInfo.Append(sbArticleInfo.ToString() & strPurchasedQty.ToString() & iPrice & strItemStatus & iNetAmount + vbCrLf)

                                itemCount += 1
                            End If

                        End If
                    Next
                    L40VocuherSales(sbHdrItemInfo)
                ElseIf IsDeliveredItem Then
                    ' For Delivery item
                    For Each drGrid As DataRow In _dtBirthListItemDetails.Rows
                        If Not (drGrid.RowState = DataRowState.Deleted) Then
                            If drGrid("PickupQty") > 0 Then

                                ValidateDataRow(drGrid("ArticleCode"), strArticleCode)

                                strArticleCode = strArticleCode.PadRight(26)

                                ValidateDataRow(drGrid("Discription"), strArticleDesc)
                                strArticleDesc = strArticleDesc.PadRight(34)

                                'ISSUE 448
                                'sbArticleInfo = SplitString(strArticleCode + strArticleDesc)
                                sbArticleInfo = SplitString(strArticleCode & vbCrLf)
                                sbArticleInfo = sbArticleInfo.Append(SplitString(strArticleDesc & vbCrLf).ToString())
                                'ISSUE 448



                                ValidateDataRow(drGrid("PickupQty"), strPurchasedQty)
                                strPurchasedQty = strPurchasedQty.ToString()
                                strPurchasedQty = strPurchasedQty.PadRight(8)

                                ValidateDataRow(drGrid("SellingPrice"), iPrice)
                                iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, BLRoundOff)
                                iPrice = iPrice.PadLeft(10)
                                iPrice = iPrice.PadRight(10)

                                ValidateDataRow(drGrid("TaxAmt"), strItemStatus)
                                strItemStatus = PrintFormatCurrency(strItemStatus, DefaultCurrency, BLRoundOff)
                                strItemStatus = strItemStatus.PadLeft(WLTaxAmount)
                                strItemStatus = strItemStatus.PadRight(WLTaxAmount)

                                ValidateDataRow(drGrid("NetAmount"), iNetAmount)
                                iNetAmount = PrintFormatCurrency(iNetAmount, DefaultCurrency, BLRoundOff)
                                iNetAmount = iNetAmount.PadLeft(14)
                                iNetAmount = iNetAmount.PadRight(14)
                                sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(strPurchasedQty.ToString() & iPrice & strItemStatus & iNetAmount).ToString() + vbCrLf)
                                itemCount += 1
                            End If

                        End If
                    Next
                End If

            ElseIf PrintBLTransaction = PrintBLTransactionSet.EditBirthListItem Then
                Dim decTotalNetAmount As Decimal = 0
                Dim decSellingPrice As Decimal = 0
                Dim sbArticleInfo As New StringBuilder
                If Not IsDeliveredItem Then
                    For Each drGrid As DataRow In _dtBirthListItemDetails.Rows
                        If Not (drGrid.RowState = DataRowState.Deleted) Then
                            If drGrid("PurchasedQty") > 0 Or (drGrid("PickUpQty") > 0) Then

                                ValidateDataRow(drGrid("ArticleCode"), strArticleCode)
                                strArticleCode = strArticleCode.PadRight(26)

                                ValidateDataRow(drGrid("Discription"), strArticleDesc)
                                strArticleDesc = strArticleDesc.PadRight(36)
                                'sbArticleInfo = SplitString(strArticleCode + strArticleDesc)
                                'ISSUE 448
                                'sbArticleInfo = SplitString(strArticleCode + strArticleDesc)
                                sbArticleInfo = SplitString(strArticleCode & vbCrLf)
                                sbArticleInfo = sbArticleInfo.Append(SplitString(strArticleDesc & vbCrLf).ToString())
                                'ISSUE 448

                                ValidateDataRow(drGrid("SellingPrice"), iPrice)
                                decSellingPrice = CDbl(clsCommon.CheckIfBlank(iPrice))
                                iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, BLRoundOff)
                                iPrice = iPrice.PadLeft(10)
                                iPrice = iPrice.PadRight(10)



                                If Not drGrid("PurchasedQty") Is Nothing AndAlso Not drGrid("PurchasedQty") Is DBNull.Value Then

                                    ValidateDataRow(drGrid("CurrentPurchasedAmount"), decTotalNetAmount)
                                    ValidateDataRow(drGrid("PurchasedQty"), strPurchasedQty)
                                    If CInt(strPurchasedQty) = 0 Then
                                        Dim decSellingPrice1 As Decimal = 0
                                        Dim decExclusiveTax As Decimal = 0
                                        ValidateDataRow(drGrid("PickUpQty"), strPurchasedQty)
                                        ValidateDataRow(drGrid("SellingPrice"), decSellingPrice1)
                                        ValidateDataRow(drGrid("ExclusiveTax"), decExclusiveTax)
                                        decTotalNetAmount = Decimal.Add(CInt(strPurchasedQty) * decSellingPrice1, decExclusiveTax)

                                    End If
                                Else

                                    decTotalNetAmount = 0
                                    strPurchasedQty = 0


                                End If

                                'If Not drGrid("PurchasedQty") Is Nothing AndAlso Not drGrid("PurchasedQty") Is DBNull.Value Then
                                '    If Not drGrid("BookedQty") Is Nothing AndAlso Not drGrid("BookedQty") Is DBNull.Value Then
                                '        strPurchasedQty = Decimal.Add(drGrid("PurchasedQty"), drGrid("BookedQty"))
                                '        decTotalNetAmount = Decimal.Add(drGrid("CurrentPurchasedAmount"), (drGrid("BookedQty") * decSellingPrice))
                                '    Else
                                '        decTotalNetAmount = drGrid("CurrentPurchasedAmount")
                                '        strPurchasedQty = drGrid("PurchasedQty")
                                '    End If
                                'Else
                                '    If Not drGrid("BookedQty") Is Nothing AndAlso Not drGrid("BookedQty") Is DBNull.Value Then
                                '        decTotalNetAmount = drGrid("BookedQty") * decSellingPrice
                                '        strPurchasedQty = drGrid("BookedQty")
                                '    Else
                                '        decTotalNetAmount = 0
                                '        strPurchasedQty = 0
                                '    End If

                                'End If

                                strPurchasedQty = strPurchasedQty.ToString()
                                strPurchasedQty = strPurchasedQty.PadRight(8)


                                ValidateDataRow(drGrid("TaxAmt"), strItemStatus)
                                strItemStatus = PrintFormatCurrency(strItemStatus, DefaultCurrency, BLRoundOff)
                                strItemStatus = strItemStatus.PadLeft(WLTaxAmount)
                                strItemStatus = strItemStatus.PadRight(WLTaxAmount)


                                iNetAmount = PrintFormatCurrency(decTotalNetAmount, DefaultCurrency, BLRoundOff)
                                iNetAmount = iNetAmount.PadLeft(14)
                                iNetAmount = iNetAmount.PadRight(14)
                                sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(strPurchasedQty.ToString() & iPrice & strItemStatus & iNetAmount).ToString() + vbCrLf)
                                itemCount += 1
                            End If
                        End If
                    Next
                Else
                    Dim iCalNetAmount As Decimal
                    Dim iCalTotolNetAmt As Decimal = Decimal.Zero
                    For Each drDtl As DataRow In _dtBirthListItemDetails.Rows
                        If Not drDtl.RowState = DataRowState.Deleted Then
                            If (drDtl("PickUpQty") > 0) Then

                                ValidateDataRow(drDtl("ArticleCode"), strArticleCode)
                                strArticleCode = strArticleCode.PadRight(26)

                                ValidateDataRow(drDtl("Discription"), strArticleDesc)
                                strArticleDesc = strArticleDesc.PadRight(34)

                                'sbArticleInfo = SplitString(strArticleCode + strArticleDesc)

                                'ISSUE 448
                                'sbArticleInfo = SplitString(strArticleCode + strArticleDesc)
                                sbArticleInfo = SplitString(strArticleCode & vbCrLf)
                                sbArticleInfo = sbArticleInfo.Append(SplitString(strArticleDesc & vbCrLf).ToString())
                                'ISSUE 448

                                ValidateDataRow(drDtl("PickUpQty"), iPurchasedQty)

                                strPurchasedQty = iPurchasedQty.ToString()
                                strPurchasedQty = strPurchasedQty.PadRight(6)
                                strPurchasedQty = strPurchasedQty.PadLeft(6)

                                ValidateDataRow(drDtl("SellingPrice"), iPrice)
                                iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, BLRoundOff)
                                iPrice = iPrice.PadLeft(10)
                                iPrice = iPrice.PadRight(10)

                                ValidateDataRow(drDtl("TaxAmt"), strItemStatus)
                                strItemStatus = PrintFormatCurrency(strItemStatus, DefaultCurrency, BLRoundOff)
                                strItemStatus = strItemStatus.PadLeft(WLTaxAmount)
                                strItemStatus = strItemStatus.PadRight(WLTaxAmount)

                                If Not drDtl("SellingPrice") Is Nothing AndAlso Not drDtl("SellingPrice") Is DBNull.Value Then
                                    iCalNetAmount = drDtl("SellingPrice") * drDtl("PickUpQty")
                                    If Not drDtl("ExclusiveTax") Is Nothing AndAlso Not drDtl("ExclusiveTax") Is DBNull.Value Then
                                        iCalNetAmount += drDtl("ExclusiveTax")
                                    End If
                                Else
                                    iCalNetAmount = Decimal.Zero
                                End If
                                iNetAmount = FormatNumber(iCalNetAmount, 2)
                                iNetAmount = PrintFormatCurrency(iCalNetAmount, DefaultCurrency, BLRoundOff)
                                iNetAmount = iNetAmount.PadLeft(14)
                                iNetAmount = iNetAmount.PadRight(14)
                                sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(strPurchasedQty & iPrice & strItemStatus & iNetAmount).ToString() + vbCrLf)
                            End If
                            If (drDtl("CurrentReturnQty") > 0) Then
                                ValidateDataRow(drDtl("ArticleCode"), strArticleCode)
                                strArticleCode = strArticleCode.PadRight(26)


                                ValidateDataRow(drDtl("Discription"), strArticleDesc)
                                strArticleDesc = strArticleDesc.PadRight(34)

                                ValidateDataRow(drDtl("CurrentReturnQty"), iPurchasedQty)
                                strPurchasedQty = iPurchasedQty.ToString()
                                strPurchasedQty = strPurchasedQty.PadLeft(8)
                                strPurchasedQty = strPurchasedQty.PadRight(8)

                                ValidateDataRow(drDtl("SellingPrice"), iPrice)
                                iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, BLRoundOff)
                                iPrice = iPrice.PadLeft(10)
                                iPrice = iPrice.PadRight(10)

                                ValidateDataRow(drDtl("TaxAmt"), strItemStatus)
                                strItemStatus = PrintFormatCurrency(strItemStatus, DefaultCurrency, BLRoundOff)
                                strItemStatus = strItemStatus.PadLeft(WLTaxAmount)
                                strItemStatus = strItemStatus.PadRight(WLTaxAmount)

                                iCalNetAmount = drDtl("CurrentReturnQty") * drDtl("SellingPrice")
                                iNetAmount = iCalNetAmount
                                iNetAmount = PrintFormatCurrency(iCalNetAmount, DefaultCurrency, BLRoundOff)
                                iNetAmount = iNetAmount.PadLeft(14)
                                iNetAmount = iNetAmount.PadRight(14)
                                sbHdrItemInfo.Append(strArticleCode & vbCrLf & strArticleDesc & vbCrLf & SplitString(strPurchasedQty & iPrice & strItemStatus & iNetAmount).ToString() & vbCrLf)
                            End If
                        End If
                    Next
                End If
            ElseIf PrintBLTransaction = PrintBLTransactionSet.GiftVoucher Then
                Dim sbArticleInfo As New StringBuilder
                Dim strPickUpQty As String = ""

                For Each drGrid As DataRow In _dtBirthListItemDetails.Rows
                    If Not (drGrid.RowState = DataRowState.Deleted) Then
                        If drGrid("PurchasedQty") > 0 Then
                            ValidateDataRow(drGrid("ArticleCode"), strArticleCode)
                            strArticleCode = strArticleCode.PadRight(26)


                            ValidateDataRow(drGrid("Discription"), strArticleDesc)
                            strArticleDesc = strArticleDesc.PadRight(36)
                            'ISSUE 448
                            'sbArticleInfo = SplitString(strArticleCode + strArticleDesc)
                            sbArticleInfo = SplitString(strArticleCode & vbCrLf)
                            sbArticleInfo = sbArticleInfo.Append(SplitString(strArticleDesc & vbCrLf).ToString())
                            'ISSUE 448

                            'sbArticleInfo = SplitString(strArticleCode + strArticleDesc)

                            ValidateDataRow(drGrid("PurchasedQty"), strPurchasedQty)
                            strPurchasedQty = strPurchasedQty.ToString()
                            strPurchasedQty = strPurchasedQty.PadRight(10)
                            If _dtBirthListItemDetails.Columns.Contains("PickUpQty") Then
                                If Not drGrid("PickUpQty") Is DBNull.Value Then

                                    strPickUpQty = strPickUpQty.PadRight(12)
                                End If
                            End If
                            sbHdrItemInfo.Append(sbArticleInfo.ToString() & SplitString(strPurchasedQty.ToString() & strPickUpQty).ToString() & vbCrLf)
                            itemCount += 1
                        End If
                    End If
                Next
            End If


            If sbHdrItemInfo.Length = 0 Then
                Return False
            End If



            Return True
        Catch ex As Exception
            MsgBox(ex.Message, , getValueByKey("CLAE05"))
            Return False
        End Try
    End Function

    Private Function L40VocuherSales(ByRef strHdrItemInfo As StringBuilder, Optional ByRef iTotalBirthListItem As Decimal = 0.0, Optional ByRef decNetAmt As Decimal = 0.0, Optional ByRef strTotalQty As String = "", Optional ByRef strNetAmt As String = "", Optional ByRef strTermsCond As String = "", Optional ByRef strLineEnd As String = "") As Boolean
        Try
            Dim strArticleCode As String = ""
            Dim strArticleDesc As String = ""
            Dim iPurchasedQty As Integer = 0
            Dim iPrice As String = "0"
            Dim iNetAmount As String = "0.0"
            Dim strPurchasedQty As String
            Dim strTaxAmount As String = "0"
            Dim sbArticleInfo As New StringBuilder
            Dim sbAmountInfo As New StringBuilder
            If Not _dtVoucherSales Is Nothing Then
                For Each drGV As DataRow In _dtVoucherSales.Rows
                    ValidateDataRow(drGV("ArticleCode"), strArticleCode)

                    strArticleCode = strArticleCode.PadRight(WArticleCode)

                    ValidateDataRow(drGV("Discription"), strArticleDesc)

                    strArticleDesc = strArticleDesc.PadRight(WArticleDescription)
                    sbArticleInfo = SplitString(strArticleCode & vbCrLf)
                    sbArticleInfo = sbArticleInfo.Append(SplitString(strArticleDesc & vbCrLf).ToString())

                    ValidateDataRow(drGV("BookedQty"), iPurchasedQty)

                    strPurchasedQty = iPurchasedQty
                    strPurchasedQty = strPurchasedQty.PadRight(WQty)


                    iNetAmount = PrintFormatCurrency(drGV("NetAmount"), DefaultCurrency, BLRoundOff)
                    iNetAmount = iNetAmount.PadLeft(WNetAmount - 2)
                    iNetAmount = iNetAmount.PadRight(WNetAmount - 2)

                    ValidateDataRow(drGV("SellingPrice"), iPrice)


                    iPrice = PrintFormatCurrency(iPrice, DefaultCurrency, BLRoundOff)
                    iPrice = iPrice.PadLeft(WRate)
                    iPrice = iPrice.PadRight(WRate)
                    strTaxAmount = PrintFormatCurrency(strTaxAmount, DefaultCurrency, BLRoundOff)
                    strTaxAmount = strTaxAmount.PadLeft(WLTaxAmount)
                    strTaxAmount = strTaxAmount.PadRight(WTaxAmount)

                    sbAmountInfo = SplitString(strPurchasedQty & iPrice & strTaxAmount & iNetAmount)
                    strHdrItemInfo.Append(sbArticleInfo.ToString() & sbAmountInfo.ToString() + vbCrLf)
                Next
            End If


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function L40SalesItemHdr(ByRef sbHdrItem As StringBuilder) As String
        Try
            If PrintBLTransaction = PrintBLTransactionSet.CreateBirthList Then
                Dim strHdrItem As String = ""
                'strHdrItem = "Item "
                strHdrItem = "Item "

                'Issue 448 
                'strHdrItem = strHdrItem.PadRight(WArticleCode) + "Description"
                'sbHdrItem.Append(strHdrItem + vbCrLf)

                ' strHdrItem = strHdrItem.PadRight(WArticleCode) & vbCrLf + "Description"

                strHdrItem = strHdrItem.PadRight(WArticleCode) & vbCrLf + getValueByKey("CLSPSO021")
                sbHdrItem.Append(strHdrItem + vbCrLf)
                'Issue 448

                Dim strHdrItemQty As String = ""
                'strHdrItemQty = "Qty"
                'strHdrItemQty = strHdrItemQty.PadRight(12) + "Price"

                strHdrItemQty = getValueByKey("CLSPSO022")
                strHdrItemQty = strHdrItemQty.PadRight(12) + getValueByKey("CLSPSO023")
                sbHdrItem.Append(strHdrItemQty)

                Dim strHdrItemNet As String = ""
                'strHdrItemNet = strHdrItemNet.PadLeft(9) + "Net"
                'strHdrItemNet = strHdrItemNet.PadRight(20) + "Res "

                strHdrItemNet = strHdrItemNet.PadLeft(9) + getValueByKey("CLSPSO026")
                strHdrItemNet = strHdrItemNet.PadRight(20) + "Res "
                sbHdrItem.Append(strHdrItemNet)

            ElseIf PrintBLTransaction = PrintBLTransactionSet.SaleBirthListItem Or PrintBLTransaction = PrintBLTransactionSet.EditBirthListItem Then
                Dim strHdrItem As String = ""
                'strHdrItem = "Item "
                strHdrItem = "Item "

                'ISSUE 448
                'strHdrItem = strHdrItem.PadRight(WArticleCode) + "Description"
                'sbHdrItem.Append(strHdrItem + vbCrLf)

                'strHdrItem = strHdrItem.PadRight(WArticleCode) & vbCrLf + "Description"
                strHdrItem = strHdrItem.PadRight(WArticleCode) & vbCrLf + getValueByKey("CLSPSO021")
                sbHdrItem.Append(strHdrItem + vbCrLf)
                'ISSUE 448

                Dim strHdrItemQty As String = ""
                'strHdrItemQty = "Qty"
                'strHdrItemQty = strHdrItemQty.PadRight(12) + "Price"

                strHdrItemQty = getValueByKey("CLSPSO022")
                strHdrItemQty = strHdrItemQty.PadRight(12) + getValueByKey("CLSPSO023")
                sbHdrItem.Append(strHdrItemQty)

                Dim strHdrItemNet As String = ""
                'strHdrItemNet = strHdrItemNet.PadLeft(6) + "Tax"
                'strHdrItemNet = strHdrItemNet.PadRight(20) + "Net"

                strHdrItemNet = strHdrItemNet.PadLeft(6) + getValueByKey("CLSPSO025")
                strHdrItemNet = strHdrItemNet.PadRight(20) + getValueByKey("CLSPSO026")
                sbHdrItem.Append(strHdrItemNet)
            ElseIf PrintBLTransaction = PrintBLTransactionSet.GiftVoucher Then
                Dim strHdrItem As String = ""
                'strHdrItem = "Item "
                strHdrItem = "Item "

                'ISSUE 448
                'strHdrItem = strHdrItem.PadRight(WArticleCode) + "Description"
                'sbHdrItem.Append(strHdrItem + vbCrLf)

                'strHdrItem = strHdrItem.PadRight(WArticleCode) & vbCrLf + "Description"
                strHdrItem = strHdrItem.PadRight(WArticleCode) & vbCrLf + getValueByKey("CLSPSO021")
                sbHdrItem.Append(strHdrItem + vbCrLf)

                'ISSUE 448

                Dim strHdrItemQty As String = ""
                'strHdrItemQty = "Qty"
                'strHdrItemQty = strHdrItemQty.PadRight(12) + "PickUpQty"

                strHdrItemQty = getValueByKey("CLSPSO022")
                strHdrItemQty = strHdrItemQty.PadRight(12) + getValueByKey("CLSBL016")
                sbHdrItem.Append(strHdrItemQty)
            End If

        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Public Function L40PrintFormat(ByVal sbHdrItemInfo As StringBuilder, ByVal sbHdrItem As StringBuilder, Optional ByVal soldItems As Boolean = False) As Boolean
        Try
            CheckDocumentType()
            Dim strHeader As New StringBuilder
            Dim strFooter As New StringBuilder
            Dim strWelcomeMsg As New StringBuilder
            Dim strTaxInformation As New StringBuilder
            Dim strPromotionMsg As New StringBuilder
            PrinttHeaderAndFooter(strHeader, strFooter, strWelcomeMsg, strPromotionMsg, strTaxInformation, strBLDocumentType, PrintBLPageSetup)

            ' Title 
            Dim sbTitle As New StringBuilder

            sbTitle.Append(A4Title(soldItems))

            Dim strCustomerDetails As String


            strCustomerDetails = L40GetCustomerDetails(_dtCustomerDetails)


            Dim strCustomerDeliveryDetails As String
            strCustomerDeliveryDetails = L40GetCustomerDeliveryDetails(_dtCustomerDetails)

            'Remark 


            Dim strPaymentInfo As String = ""
            If IsDeliveredItem = False Then
                GetPaymentInfo(strPaymentInfo)
            End If


            Dim strPrint As New StringBuilder
            strPrint.Length = 0
            If IsHeaderPrinting Then
                strPrint.Append(strHeader)
            End If
            strPrint.Append(strLineL40 + vbCrLf)
            Dim strMsg As String = L40Messages(strWelcomeMsg.ToString())
            strPrint.Append(strMsg)
            strPrint.Append(vbCrLf + strLineL40 + vbCrLf)

            strPrint.Append(strTaxInformation.ToString())

            Dim strCashierDetails As String = L40CashierDetails(CreationDate, InvoiceNumber, CashierId)
            strPrint.Append(strCashierDetails)
            strPrint.Append(sbTitle)

            strPrint.Append(strCustomerDetails)
            strPrint.Append(strCustomerDeliveryDetails)
            strPrint.Append(vbCrLf + strLineL40 + vbCrLf)

            strPrint.Append(sbHdrItem.ToString() + vbCrLf)

            strPrint.Append(vbCrLf + strLineL40 + vbCrLf)
            strPrint.Append(sbHdrItemInfo.ToString() + vbCrLf)

            ' this is add to remove the payment info from gift print 
            'add by ram on 3-apr-2010
            If Not GiftReceiptMessage Is Nothing And Not GiftReceiptMessage = String.Empty Then
                strPaymentInfo = ""
            End If
            ' this is add to remove the payment info from gift print 


            If IsDeliveredItem = False Then
                strPrint.Append(vbCrLf + strLineL40 + vbCrLf)
                strPrint.Append(strPaymentInfo)
                strPrint.Append(vbCrLf + strLineL40 + vbCrLf)
            End If



            If PrintBLTransaction = PrintBLTransactionSet.SaleBirthListItem Or PrintBLTransaction = PrintBLTransactionSet.EditBirthListItem Then
                If IsDeliveredItem = False Then
                    Dim strPaymentDetails As String = ""
                    L40GetTaxDetails(strPaymentDetails)
                    strPrint.Append(strPaymentDetails)
                End If

            End If
            strPrint.Append(A4GetStringTermsCondition(strBLDocumentType, SiteCode, PrintBLPageSetup))
            If Not GiftReceiptMessage Is Nothing And Not GiftReceiptMessage = String.Empty Then
                strPrint.Append(vbCrLf + SplitString(GiftReceiptMessage).ToString() + vbCrLf)
            End If


            strPrint.Append(vbCrLf + strLineL40 + vbCrLf)

            If IsPromotionMessagePrint Then
                strPrint.Append(strPromotionMsg.ToString())
            End If


            If IsFooterPrinting Then
                strPrint.Append(strFooter)
            End If
            strPrint.Append(vbCrLf + strLineL40 + vbCrLf)

            'If PrintBLTransaction = PrintBLTransactionSet.SaleBirthListItem Then 
            '--Commented by rama on 25-Oct-2010 for wrong condtion matching 
            ' PrinterName = SetPrinterName(dtPrinterInfo1, "BirthList", PrintBLTransaction.ToString)
            PrinterName = SetPrinterName(dtPrinterInfo1, "BirthList", strBLDocumentType)
            If (_printPreview = True) Then
                fnPrint(strPrint.ToString(), "PRV")
            Else
                fnPrint(strPrint.ToString(), "PRN")
            End If


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function L40Messages(ByVal strWelcomemsg As String) As String
        Try
            Dim sbMessages As New StringBuilder
            sbMessages.Append(strWelcomemsg)
            Return sbMessages.ToString()
        Catch ex As Exception

        End Try
    End Function

    Public Function GetPaymentInfo(ByRef strPaymentInfo As String) As Boolean
        Try
            Dim sbPaymentInfo As New StringBuilder
            'Dim strTotal2Pay As String = "TOTAL TO PAY "
            Dim strTotal2Pay As String = getValueByKey("CLSCMP020") & " "
            Dim decTotal2Pay As Decimal
            If PrintBLTransaction = PrintBLTransactionSet.SaleBirthListItem Then
                If Not BirthListItemDetails Is Nothing AndAlso BirthListItemDetails.Rows.Count > 0 Then
                    decTotal2Pay = BirthListItemDetails.Compute("sum(NetAmount)", "")
                    If IsDeliveredItem Then
                        decTotal2Pay = BirthListItemDetails.Compute("sum(NetAmount)", "Pickupqty>0")
                    End If
                End If
                If Not _dtVoucherSales Is Nothing Then
                    If _dtVoucherSales.Rows.Count > 0 Then
                        decTotal2Pay = decTotal2Pay + _dtVoucherSales.Compute("Sum(NetAmount)", " ")
                    End If
                End If
            ElseIf PrintBLTransaction = PrintBLTransactionSet.EditBirthListItem Then

                If Not IsDeliveredItem Then
                    If Not BirthListItemDetails Is Nothing AndAlso BirthListItemDetails.Rows.Count > 0 Then

                        For Each drGrid As DataRow In _dtBirthListItemDetails.Rows
                            If Not (drGrid.RowState = DataRowState.Deleted) Then

                                If drGrid("PurchasedQty") > 0 Then
                                    'If drGrid("CurrentPurchasedAmount") = Decimal.Zero Then
                                    If Not drGrid("SellingPrice") Is Nothing AndAlso Not drGrid("SellingPrice") Is DBNull.Value Then
                                        'If drGrid("PurchasedQty") = 0 AndAlso drGrid("BookedQty") = 0 Then
                                        decTotal2Pay += (drGrid("PurchasedQty")) * drGrid("SellingPrice")
                                        If Not drGrid("ExclusiveTax") Is Nothing AndAlso Not drGrid("ExclusiveTax") Is DBNull.Value Then
                                            decTotal2Pay += drGrid("ExclusiveTax")
                                        End If
                                        '    'ElseIf drGrid("BookedQty") = 0 Then
                                        '    decTotal += drGrid("PurchasedQty") * drGrid("SellingPrice")
                                        'End If
                                    End If

                                End If

                                'If drGrid("PurchasedQty") > 0 Or drGrid("BookedQty") > 0 Then
                                '    'If drGrid("CurrentPurchasedAmount") = Decimal.Zero Then
                                '    If Not drGrid("SellingPrice") Is Nothing AndAlso Not drGrid("SellingPrice") Is DBNull.Value Then
                                '        'If drGrid("PurchasedQty") = 0 AndAlso drGrid("BookedQty") = 0 Then
                                '        decTotal2Pay += (drGrid("PurchasedQty") + drGrid("BookedQty")) * drGrid("SellingPrice")
                                '        If Not drGrid("ExclusiveTax") Is Nothing AndAlso Not drGrid("ExclusiveTax") Is DBNull.Value Then
                                '            decTotal2Pay += drGrid("ExclusiveTax")
                                '        End If
                                '        '    'ElseIf drGrid("BookedQty") = 0 Then
                                '        '    decTotal += drGrid("PurchasedQty") * drGrid("SellingPrice")
                                '        'End If
                                '    End If
                                '# End  "Chat With jc"


                                'Else
                                '    decTotal += drGrid("CurrentPurchasedAmount")
                                'End If
                                'Else
                                '    If drGrid("PickupQty") > 0 Then
                                '        If drGrid("CurrentPurchasedAmount") = Decimal.Zero Then
                                '            If Not drGrid("SellingPrice") Is Nothing AndAlso Not drGrid("SellingPrice") Is DBNull.Value Then
                                '                decTotal += drGrid("PickupQty") * drGrid("SellingPrice")
                                '            End If
                                '        Else
                                '            decTotal += drGrid("CurrentPurchasedAmount")
                                '        End If
                                '    End If
                            End If

                        Next
                        'decTotal2Pay = BirthListItemDetails.Compute("sum(CurrentPurchasedAmount )", "")
                    End If
                Else
                    If IsDeliveredItem Then
                        decTotal2Pay = 0.0
                        For Each drGrid As DataRow In _dtBirthListItemDetails.Rows
                            If Not (drGrid.RowState = DataRowState.Deleted) Then
                                If drGrid("PickupQty") > 0 Then
                                    'If drGrid("CurrentPurchasedAmount") = Decimal.Zero Then
                                    If Not drGrid("SellingPrice") Is Nothing AndAlso Not drGrid("SellingPrice") Is DBNull.Value Then
                                        decTotal2Pay += drGrid("PickupQty") * drGrid("SellingPrice")
                                        If Not drGrid("ExclusiveTax") Is Nothing AndAlso Not drGrid("ExclusiveTax") Is DBNull.Value Then
                                            decTotal2Pay += drGrid("ExclusiveTax")
                                        End If
                                    End If
                                    'Else
                                    '    decTotal += drGrid("CurrentPurchasedAmount")
                                    'End If

                                End If
                            End If
                        Next
                        'decTotal2Pay = BirthListItemDetails.Compute("sum(CurrentPurchasedAmount)", "Pickupqty>0")
                    End If
                End If


            ElseIf PrintBLTransaction = PrintBLTransactionSet.CreateBirthList Then
                If Not BirthListItemDetails Is Nothing AndAlso BirthListItemDetails.Rows.Count > 0 Then
                    decTotal2Pay = BirthListItemDetails.Compute("sum(Amount)", "")
                End If
            End If
            sbPaymentInfo.Append(strTotal2Pay)
            sbPaymentInfo.Append(PrintFormatCurrency(MyRound(decTotal2Pay, BLRoundOff, IsBillRoundOffRequired), DefaultCurrency, BLRoundOff).PadLeft(4))
            Dim sbPaymentSummary As New StringBuilder
            If Not PaymentHistory Is Nothing AndAlso PaymentHistory.Rows.Count > 0 Then
                Dim strReceiptCode As String = ""
                Dim strAmount As String = ""
                Dim strCurrencyCode As String = ""
                Dim strNumber As String = ""
                Dim FinalString As String = ""
                For Each drPayd As DataRow In PaymentHistory.Rows
                    strReceiptCode = ""
                    strNumber = ""

                    ValidateDataRow(drPayd("RecieptType"), strReceiptCode)

                    If strReceiptCode.ToUpper() = CreditVoucher_R Or strReceiptCode.ToUpper() = CreditVoucher_I Or strReceiptCode.ToUpper() = GIFTVOUCHE_I Or strReceiptCode.ToUpper() = GIFTVOUCHE_R Then
                        ValidateDataRow(drPayd("Number"), strNumber)
                        strReceiptCode = strReceiptCode + "(" + strNumber + ")"

                    End If

                    strReceiptCode = strReceiptCode.PadRight(27)
                    strReceiptCode = strReceiptCode.PadLeft(27)

                    If Not drPayd("CurrencyCode") Is DBNull.Value Then
                        If drPayd("CurrencyCode") = DefaultCurrency Then
                            'strAmount = drPayd("Amount")
                            ValidateDataRow(drPayd("Amount"), strAmount)
                        ElseIf drPayd("CurrencyCode") = String.Empty Then
                            ValidateDataRow(drPayd("Amount"), strAmount)
                        Else
                            ValidateDataRow(drPayd("AmountInCurrency"), strAmount)

                        End If
                    Else
                        ValidateDataRow(drPayd("Amount"), strAmount)

                    End If
                    ValidateDataRow(drPayd("CurrencyCode"), strCurrencyCode)


                    strAmount = strAmount.PadRight(16)
                    strAmount = strAmount.PadLeft(16)

                    strAmount = PrintFormatCurrency(strAmount, strCurrencyCode, BLRoundOff)
                    FinalString = strReceiptCode + ":" + strAmount

                    sbPaymentSummary.Append(SplitString(vbCrLf + FinalString).ToString())
                    FinalString = ""

                Next
                sbPaymentInfo.Append(sbPaymentSummary.ToString())
            End If

            strPaymentInfo = sbPaymentInfo.ToString()

        Catch ex As Exception

        End Try
    End Function

    Public Function BLPrint(Optional ByVal strPrintOrPreview As String = "") As String
        ' strPrintOrPreview = 'PRN' that means Print
        ' strPrintOrPreview = 'PRV' that means Preview
        printDocument1.DefaultPageSettings.Margins.Left = 20
        printDocument1.DefaultPageSettings.Margins.Right = 20
        Try
            printDocument1.PrinterSettings.PrinterName = PrinterName
            If strPrintOrPreview = "PRN" Then
                printDocument1.PrinterSettings.PrintToFile = True
                printDocument1.Print()
            Else
                printPreviewDialog1.Document = printDocument1
                printPreviewDialog1.Select()
                printPreviewDialog1.ShowDialog()
            End If

        Catch ex As Exception
            If printDocument1.PrinterSettings.IsValid = False Then
                MessageBox.Show(String.Format(getValueByKey("CLBL03"), PrinterName), "CLBL03 - " & getValueByKey("CLAE05"))
            End If

        End Try
    End Function

    Private Sub printDocument1_PrintPage(ByVal sender As Object, _
      ByVal e As PrintPageEventArgs) Handles printDocument1.PrintPage
        'Dim g As Graphics = e.Graphics
        'g.PageUnit = GraphicsUnit.Inch

        Dim MyFont As New System.Drawing.Font("Verdana", 7.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Dim charactersOnPage As Integer = 0
        Dim linesPerPage As Integer = 0
        'If VarBarcode.Text <> String.Empty Then
        '    e.Graphics.DrawImage(VarBarcode.Image, 0, 0)
        'End If
        ' Sets the value of charactersOnPage to the number of characters 
        ' of stringToPrint that will fit within the bounds of the page.

        ' e.Graphics.MeasureString(stringToPrint, MyFont, e.MarginBounds.Size, _
        'StringFormat.GenericTypographic, charactersOnPage, linesPerPage)
        ' ' Draws the string within the bounds of the page.
        ' e.Graphics.DrawString(stringToPrint, MyFont, Brushes.Black, _
        '     e.MarginBounds, StringFormat.GenericTypographic)
        ' ' Remove the portion of the string that has been printed.
        ' stringToPrint = stringToPrint.Substring(charactersOnPage)
        ' ' Check to see if more pages are to be printed.

        e.HasMorePages = stringToPrint.Length > 0

        ' If there are no more pages, reset the string to be printed.
        If Not e.HasMorePages Then
            stringToPrint = documentContents
        End If
        'A4DocumentWrite(e)
    End Sub

    'Public Function A4DocumentWrite(ByVal e As PrintPageEventArgs) As Boolean
    '    Try

    '        Dim strHeader As New StringBuilder
    '        Dim strFooter As New StringBuilder
    '        Dim strWelcomeMsg As New StringBuilder
    '        Dim strTaxInformation As New StringBuilder
    '        Dim strPromotionMsg As New StringBuilder
    '        PrinttHeaderAndFooter(strHeader, strFooter, strWelcomeMsg, strPromotionMsg, strTaxInformation, strBLDocumentType, PrintBLPageSetup)



    '        Dim strFooterPayment As String = A4Footer(BirthListItemDetails, IsDeliveredItem)




    '        ' Title 
    '        Dim sbTitle As New StringBuilder

    '        sbTitle.Append(A4Title(IsDeliveredItem))


    '        Dim strCustomerDetails As String
    '        strCustomerDetails = A4GetCustomerDetails(_dtCustomerDetails)

    '        Dim strCustomerDeliveryDetails As String
    '        strCustomerDeliveryDetails = A4GetCustomerDeliveryDetails(_dtCustomerDetails)

    '        'Remark 
    '        Dim sbRemark As New StringBuilder
    '        Dim strRemark As String = "Remark"
    '        sbRemark.Append(strRemark + vbCrLf)


    '        Dim objclsA4Print As New clsA4Print


    '        Dim strPrint As New StringBuilder
    '        strPrint.Length = 0
    '        If IsHeaderPrinting Then
    '            objclsA4Print.A4Header = strHeader.ToString()
    '            'strPrint.Append(strHeader.Append(vbCrLf))
    '        End If

    '        'objclsA4Print.A4Title = sbTitle.ToString()
    '        ''objclsA4Print.A4CashierDetails=
    '        ''strPrint.Append(sbTitle)

    '        ''Rohit Start 
    '        'objclsA4Print.A4CustomerDetails = strCustomerDetails
    '        'objclsA4Print.A4DeliveryAddress = strCustomerDeliveryDetails
    '        ''strPrint.Append(strCustomerDetails)
    '        ''strPrint.Append(strCustomerDeliveryDetails)

    '        ''Rohit End
    '        'objclsA4Print.A4Remark = sbRemark.ToString()
    '        ' ''strPrint.Append(sbRemark)
    '        ''strPrint.Append(vbCrLf + strLineA4 + vbCrLf)

    '        'objclsA4Print.A4ItemHeader = sbHdrItem.ToString()
    '        'objclsA4Print.A4ItemDetails = sbHdrItemInfo
    '        ''strPrint.Append(sbHdrItem.ToString() + vbCrLf)
    '        ''strPrint.Append(sbHdrItemInfo.ToString() + vbCrLf)
    '        ''strPrint.Append(vbCrLf + strLineA4 + vbCrLf)
    '        'objclsA4Print.A4PaymentDetails = strFooterPayment
    '        strPrint.Append(strFooterPayment)
    '        If IsFooterPrinting Then
    '            objclsA4Print.A4Footer = strFooter.ToString()
    '        End If
    '        'objclsA4Print.fnPrint("PRN")
    '        'Return True

    '        Dim currentPos As Integer = e.PageBounds.Y + 5
    '        e.Graphics.DrawString(strHeader.ToString(), SpectrumPrintFont.Header, Brushes.Black, BoundsSpecified.X, currentPos, StringFormat.GenericTypographic)

    '        currentPos = currentPos + 60

    '        e.Graphics.DrawString(A4Title, SpectrumPrintFont.Title, Brushes.Black, BoundsSpecified.X, currentPos, StringFormat.GenericTypographic)

    '        currentPos = currentPos + 35
    '        e.Graphics.DrawString("", SpectrumPrintFont.CashierDetails, Brushes.Black, BoundsSpecified.X, currentPos, StringFormat.GenericTypographic)

    '        currentPos = currentPos + 15

    '        e.Graphics.DrawString(strCustomerDetails, SpectrumPrintFont.CustomerInfo, Brushes.Black, BoundsSpecified.X, currentPos, StringFormat.GenericTypographic)
    '        currentPos = currentPos + 85

    '        e.Graphics.DrawString(strCustomerDeliveryDetails, SpectrumPrintFont.DeliveryAddress, Brushes.Black, BoundsSpecified.X, currentPos, StringFormat.GenericTypographic)
    '        currentPos = currentPos + 75

    '        e.Graphics.DrawString(sbRemark.ToString(), SpectrumPrintFont.Remark, Brushes.Black, BoundsSpecified.X, currentPos, StringFormat.GenericTypographic)
    '        currentPos = currentPos + 45

    '        'e.Graphics.DrawString(, SpectrumPrintFont.ItemHeader, Brushes.Black, BoundsSpecified.X, currentPos, StringFormat.GenericTypographic)
    '        currentPos = currentPos + 25

    '        'e.Graphics.DrawString(A4ItemDetails.ToString(), SpectrumPrintFont.ItemDetail, Brushes.Black, BoundsSpecified.X, currentPos, StringFormat.GenericDefault)
    '        'currentPos = currentPos + 145
    '        Dim sbHdrItemInfo As New StringBuilder
    '        Dim sbHdrItem As New StringBuilder

    '        DrawA4GetSalesItemsDetails(_dtBirthListItemDetails, e, sbHdrItemInfo, sbHdrItem)

    '        currentPos = currentPos + 210

    '        e.Graphics.DrawString(strFooterPayment, SpectrumPrintFont.PaymentDetail, Brushes.Black, BoundsSpecified.X, currentPos, StringFormat.GenericTypographic)
    '        currentPos = currentPos + 150

    '        e.Graphics.DrawString(strFooter.ToString(), SpectrumPrintFont.Footer, Brushes.Black, BoundsSpecified.X, currentPos, StringFormat.GenericTypographic)

    '    Catch ex As Exception

    '    End Try
    'End Function

    Public Function DrawA4GetSalesItemsDetails(ByVal _dtBirthListItemDetails As DataTable, ByVal e As PrintPageEventArgs, ByRef sbHdrItemInfo As StringBuilder, ByRef sbHdrItem As StringBuilder) As Boolean
        Try
            A4SalesItemHdr(sbHdrItem)
            Dim strArticleCode As String = ""
            Dim strArticleDesc As String = ""
            Dim iPurchasedQty As Integer
            Dim strPurchasedQty As String
            Dim iPrice As String
            Dim iNetAmount As String
            Dim strReservedQty As String
            Dim strCLP As String
            Dim itemCount As Integer = 1
            Dim strItemStatus As String
            Dim fString As String
            If PrintBLTransaction = PrintBLTransactionSet.CreateBirthList Then
                'For all items 
                Dim currentPos As Integer = 300
                Dim iLenght As Integer
                e.Graphics.DrawString(sbHdrItem.ToString(), SpectrumPrintFont.ItemDetail, Brushes.Black, BoundsSpecified.X, 290, StringFormat.GenericDefault)
                Dim myStringFormat As New StringFormat
                'myStringFormat.Alignment = StringAlignment.Near
                myStringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces
                'myStringFormat.LineAlignment = StringAlignment.Near
                'myStringFormat.Trimming = StringTrimming.Word



                For Each drGrid As DataRow In _dtBirthListItemDetails.Rows
                    If Not (drGrid.RowState = DataRowState.Deleted) Then
                        strArticleCode = drGrid("ArticleCode")
                        strArticleCode = strArticleCode.PadRight(22)

                        strArticleDesc = drGrid("ArticleDescription")
                        strArticleDesc = strArticleDesc.PadRight(51)


                        iLenght = strArticleDesc.Length
                        strPurchasedQty = drGrid("RequstedQty")
                        'strPurchasedQty = strPurchasedQty.ToString()
                        strPurchasedQty = strPurchasedQty.PadRight(10)
                        strPurchasedQty = strPurchasedQty.PadLeft(4)
                        iLenght = strPurchasedQty.Length
                        'strPurchasedQty = strPurchasedQty.PadLeft(2)

                        iPrice = drGrid("Rate")
                        iPrice = iPrice.PadRight(14)
                        iNetAmount = FormatNumber(drGrid("Amount").ToString(), 2).ToString
                        iNetAmount = iNetAmount.PadRight(18)

                        strItemStatus = "Open"
                        strItemStatus = strItemStatus.PadRight(9)
                        strReservedQty = drGrid("ReservedQty")
                        strReservedQty = strReservedQty.PadRight(9)
                        strCLP = drGrid("IsCLP").ToString()
                        strCLP = strCLP.PadRight(9)
                        fString = strArticleCode & strArticleDesc & strPurchasedQty & iPrice & iNetAmount & strItemStatus & strReservedQty & strCLP + vbCrLf
                        'fString = String.Concat(strArticleCode, strArticleDesc, strPurchasedQty)
                        'fString = String.Concat(fString, iPrice, iNetAmount)
                        'fString = String.Concat(fString, strItemStatus, strReservedQty, strCLP)
                        'fString = String.Concat(fString, strCLP + vbCrLf)
                        iLenght = fString.Length
                        e.Graphics.DrawString(fString, SpectrumPrintFont.ItemDetail, Brushes.Black, BoundsSpecified.X, currentPos, myStringFormat)

                        sbHdrItemInfo.Append(fString)
                        itemCount += 1
                        currentPos = currentPos + 20
                    End If
                Next
            ElseIf PrintBLTransaction = PrintBLTransactionSet.SaleBirthListItem Then

                If Not IsDeliveredItem Then ' Only for Sold items 
                    For Each drGrid As DataRow In _dtBirthListItemDetails.Rows
                        If Not (drGrid.RowState = DataRowState.Deleted) Then
                            If drGrid("PurchasedQty") > 0 Then
                                strArticleCode = drGrid("ArticleCode")
                                strArticleCode = strArticleCode.PadRight(26)
                                strArticleDesc = drGrid("Discription")
                                strArticleDesc = strArticleDesc.PadRight(55)
                                strPurchasedQty = drGrid("PurchasedQty")
                                strPurchasedQty = strPurchasedQty.ToString()
                                strPurchasedQty = strPurchasedQty.PadRight(8)
                                iPrice = drGrid("SellingPrice")
                                iPrice = iPrice.PadRight(10)
                                strItemStatus = FormatNumber(drGrid("TaxAmt").ToString(), 2)
                                strItemStatus = strItemStatus.PadRight(7)
                                iNetAmount = FormatNumber(drGrid("NetAmount").ToString(), 2)
                                iNetAmount = iNetAmount.PadRight(6)

                                sbHdrItemInfo.Append(strArticleCode & strArticleDesc + strPurchasedQty.ToString() & iPrice & strItemStatus & iNetAmount + vbCrLf)
                                itemCount += 1
                            End If

                        End If
                    Next
                ElseIf IsDeliveredItem Then
                    ' For Delivery item
                    For Each drGrid As DataRow In _dtBirthListItemDetails.Rows
                        If Not (drGrid.RowState = DataRowState.Deleted) Then
                            If drGrid("PickupQty") > 0 Then
                                strArticleCode = drGrid("ArticleCode")
                                strArticleCode = strArticleCode.PadRight(26)
                                strArticleDesc = drGrid("Discription")
                                strArticleDesc = strArticleDesc.PadRight(55)
                                strPurchasedQty = drGrid("PickupQty")
                                strPurchasedQty = strPurchasedQty.ToString()
                                strPurchasedQty = strPurchasedQty.PadRight(8)
                                iPrice = drGrid("SellingPrice")
                                iPrice = iPrice.PadRight(10)
                                strItemStatus = FormatNumber(drGrid("TaxAmt").ToString(), 2)
                                strItemStatus = strItemStatus.PadRight(7)
                                iNetAmount = FormatNumber(drGrid("NetAmount").ToString(), 2)
                                iNetAmount = iNetAmount.PadRight(6)

                                sbHdrItemInfo.Append(strArticleCode & strArticleDesc + strPurchasedQty.ToString() & iPrice & strItemStatus & iNetAmount + vbCrLf)
                                itemCount += 1
                            End If

                        End If
                    Next
                End If

                A4VocuherSales(sbHdrItemInfo)
            ElseIf PrintBLTransaction = PrintBLTransactionSet.EditBirthListItem Then
                If Not IsDeliveredItem Then
                    For Each drGrid As DataRow In _dtBirthListItemDetails.Rows
                        If Not (drGrid.RowState = DataRowState.Deleted) Then
                            If drGrid("PurchasedQty") > 0 Then
                                strArticleCode = drGrid("ArticleCode")
                                strArticleCode = strArticleCode.PadRight(26)
                                strArticleDesc = drGrid("Discription")
                                strArticleDesc = strArticleDesc.PadRight(55)
                                strPurchasedQty = drGrid("PurchasedQty")
                                strPurchasedQty = strPurchasedQty.ToString()
                                strPurchasedQty = strPurchasedQty.PadRight(8)
                                iPrice = drGrid("SellingPrice")
                                iPrice = iPrice.PadRight(10)
                                strItemStatus = FormatNumber(drGrid("TaxAmt").ToString(), 2)
                                strItemStatus = strItemStatus.PadRight(7)
                                iNetAmount = FormatNumber(drGrid("CurrentPurchasedAmount").ToString(), 2)
                                iNetAmount = iNetAmount.PadRight(6)
                                sbHdrItemInfo.Append(strArticleCode & strArticleDesc + strPurchasedQty.ToString() & iPrice & strItemStatus & iNetAmount + vbCrLf)
                                itemCount += 1
                            End If
                        End If
                    Next
                Else
                    Dim iCalNetAmount As Decimal
                    Dim iCalTotolNetAmt As Decimal = Decimal.Zero
                    For Each drDtl As DataRow In _dtBirthListItemDetails.Rows
                        If Not drDtl.RowState = DataRowState.Deleted Then
                            If (drDtl("PickUpQty") > 0) Then
                                strArticleCode = drDtl("ArticleCode")
                                strArticleCode = strArticleCode.PadRight(26)
                                strArticleDesc = drDtl("Discription")
                                strArticleDesc = strArticleDesc.PadRight(55)

                                iPurchasedQty = drDtl("PickUpQty")
                                strPurchasedQty = iPurchasedQty.ToString()
                                strPurchasedQty = strPurchasedQty.PadRight(6)

                                iPrice = drDtl("SellingPrice")
                                iPrice = iPrice.PadRight(10)

                                strItemStatus = FormatNumber(drDtl("TaxAmt").ToString(), 2)
                                strItemStatus = strItemStatus.PadRight(7)

                                iCalNetAmount = drDtl("PickUpQty") * drDtl("SellingPrice")
                                iNetAmount = FormatNumber(iCalNetAmount, 2)
                                iNetAmount = iNetAmount.PadRight(6)
                                sbHdrItemInfo.Append(strArticleCode & strArticleDesc & strPurchasedQty & iPrice & strItemStatus & iNetAmount + vbCrLf)
                            End If
                            If (drDtl("CurrentReturnQty") > 0) Then
                                strArticleCode = drDtl("ArticleCode")
                                strArticleCode = strArticleCode.PadRight(26)

                                strArticleDesc = drDtl("Discription")
                                strArticleDesc = strArticleDesc.PadRight(55)

                                iPurchasedQty = drDtl("CurrentReturnQty")
                                strPurchasedQty = iPurchasedQty.ToString()
                                strPurchasedQty = strPurchasedQty.PadRight(8)

                                iPrice = drDtl("SellingPrice")
                                iPrice = iPrice.PadRight(10)

                                strItemStatus = FormatNumber(drDtl("TaxAmt").ToString(), 2)
                                strItemStatus = strItemStatus.PadRight(7)

                                iCalNetAmount = drDtl("CurrentReturnQty") * drDtl("SellingPrice")
                                iNetAmount = FormatNumber(iCalNetAmount, 2)
                                iNetAmount = iNetAmount.PadRight(6)
                                sbHdrItemInfo.Append(strArticleCode & strArticleDesc & strPurchasedQty & iPrice & strItemStatus & iNetAmount + vbCrLf)
                            End If
                        End If
                    Next
                End If

            End If


            If sbHdrItemInfo.Length = 0 Then
                Return False
            End If

            If Not PrintBLTransaction = PrintBLTransactionSet.CreateBirthList Then
                If Not BirthListItemDetails Is Nothing Then
                    If BirthListItemDetails.Rows.Count > 0 Then
                        Dim iPickUpQty As Integer = BirthListItemDetails.Compute("sum(PickUpQty)", " ")
                        If iPickUpQty > 0 Then
                            IsDeliveredItem = True
                        End If
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            MsgBox(ex.Message, , getValueByKey("CLAE05"))
            Return False
        End Try

    End Function


End Class


