Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports C1.C1Pdf
Imports System.Drawing.Printing
Imports C1.Win.C1BarCode
Imports SpectrumBL
Imports SpectrumCommon
Imports SpectrumCommon.ExtensionModule
Imports Spire.Pdf
Imports Spire.License
Imports Microsoft.Reporting.WinForms
Imports System.Windows.Forms
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Net.Mail
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security
Imports System.Net


''' <summary>
''' This Class is used for Cash Memo Printing
''' </summary>
''' <createdby>Rama Ranjan Jena</createdby>
''' <Updatedby></Updatedby>
''' <UpdatedOn></UpdatedOn>
''' <remarks></remarks>
Public Class clsCashMemoPrint
#Region "Global Varible For Class"
    Dim CashMemoNo As String
    Dim _HeaderPrint As Boolean
    Dim _FooterPrint As Boolean
    Dim _SalesPerApplicable As Boolean
    Dim _TaxDetail As Boolean
    Dim _PrintPreview As Boolean
    Dim _printType As String
    Dim LineLength As Int32
    Dim _IsDisplayTotalSaving As Boolean
    Dim TotralNetSaving As String = String.Empty
    Dim strDayOpenDate As String = String.Empty
    Dim BarCodestring As String
    Dim CustomerSaleTypeDineIn As String
    Dim objClsCommon As clsCommon
#End Region
#Region "Public Methods & Functions"
    Private _TableNameForDineIn As Boolean = False
    Public Property TableNameForDineIn() As Boolean
        Get
            Return _TableNameForDineIn
        End Get
        Set(ByVal value As Boolean)
            _TableNameForDineIn = value
        End Set
    End Property
    Private _CompositeTaxReqOnPrint As Boolean = False
    Public Property CompositeTaxReqOnPrint() As Boolean
        Get
            Return _CompositeTaxReqOnPrint
        End Get
        Set(ByVal value As Boolean)
            _CompositeTaxReqOnPrint = value
        End Set
    End Property
    Private Shared _LoginUserName As String
    Public Shared Property LoginUserName() As String
        Get
            Return _LoginUserName
        End Get
        Set(ByVal value As String)
            _LoginUserName = value
        End Set
    End Property
    Private _IsDintInEnabled As Boolean = False
    Public Property IsDintInEnabled() As Boolean
        Get
            Return _IsDintInEnabled
        End Get
        Set(ByVal value As Boolean)
            _IsDintInEnabled = value
        End Set
    End Property

    Private _IsKotPrintRequired As Boolean = False
    Public Property IsKotPrintRequired() As Boolean
        Get
            Return _IsKotPrintRequired
        End Get
        Set(ByVal value As Boolean)
            _IsKotPrintRequired = value
        End Set
    End Property

    Private _IsRoundRequired As Boolean = False
    Public Property IsRoundRequired() As Boolean
        Get
            Return _IsRoundRequired
        End Get
        Set(ByVal value As Boolean)
            _IsRoundRequired = value
        End Set
    End Property

    Private _TaxDetailsRequired As Boolean = False
    Public Property TaxDetailsRequired() As Boolean
        Get
            Return _TaxDetailsRequired
        End Get
        Set(ByVal value As Boolean)
            _TaxDetailsRequired = value
        End Set
    End Property
    Private _DisplayArticleFullName As Boolean = False
    Public Property DisplayArticleFullName() As Boolean
        Get
            Return _DisplayArticleFullName
        End Get
        Set(ByVal value As Boolean)
            _DisplayArticleFullName = value
        End Set
    End Property
    'added by khusrao adil on 01-08-2018 for sharda restaurent
    Private _TableNoRequiredInDineInModuleBillPrint As Boolean = False
    Public Property TableNoRequiredInDineInModuleBillPrint() As Boolean
        Get
            Return _TableNoRequiredInDineInModuleBillPrint
        End Get
        Set(ByVal value As Boolean)
            _TableNoRequiredInDineInModuleBillPrint = value
        End Set
    End Property

    Private _AllowDecimalQty As Boolean = False
    Public Property AllowDecimalQty() As Boolean
        Get
            Return _AllowDecimalQty
        End Get
        Set(ByVal value As Boolean)
            _AllowDecimalQty = value
        End Set
    End Property
    Private _WeightScaleEnabled As Boolean = False
    Public Property WeightScaleEnabled() As Boolean
        Get
            Return _WeightScaleEnabled
        End Get
        Set(ByVal value As Boolean)
            _WeightScaleEnabled = value
        End Set
    End Property

    Public DeliveryPersonName As String
    'Public Property DeliveryPersonName() As String
    '    Get
    '        Return _DeliveryPersonName
    '    End Get
    '    Set(ByVal value As String)
    '        _DeliveryPersonName = value
    '    End Set
    'End Property
    Private _IsDeliveryCopyRequired As Boolean = False
    Public Property IsDeliveryCopyRequired() As Boolean
        Get
            Return _IsDeliveryCopyRequired
        End Get
        Set(ByVal value As Boolean)
            _IsDeliveryCopyRequired = value
        End Set
    End Property


    Private _KOTBillPrintingRequired As Boolean
    Public Property KOTBillPrintingRequired() As Boolean
        Get
            If IsDintInEnabled AndAlso CustomerSaleType = "Dine In" Then
                _KOTBillPrintingRequired = False
                Return _KOTBillPrintingRequired
            Else
                Return _KOTBillPrintingRequired
            End If
        End Get
        Set(ByVal value As Boolean)
            _KOTBillPrintingRequired = value
        End Set
    End Property

    Private _CashMemoResetonDayClose As Boolean
    Public Property CashMemoResetonDayClose() As Boolean
        Get
            Return _CashMemoResetonDayClose
        End Get
        Set(ByVal value As Boolean)
            _CashMemoResetonDayClose = value
        End Set
    End Property

    Private _CustomerNameRequiredInKOT As Boolean
    Public Property CustomerNameRequiredInKOT() As Boolean
        Get
            Return _CustomerNameRequiredInKOT
        End Get
        Set(ByVal value As Boolean)
            _CustomerNameRequiredInKOT = value
        End Set
    End Property
    Private _DisplayBrandWiseSale As Boolean 'vipul 
    Public Property DisplayBrandWiseSale() As Boolean
        Get
            Return _DisplayBrandWiseSale
        End Get
        Set(ByVal value As Boolean)
            _DisplayBrandWiseSale = value
        End Set
    End Property
    Private _TokenNoRequiredInKOT As Boolean
    Public Property TokenNoRequiredInKOT() As Boolean
        Get
            Return _TokenNoRequiredInKOT
        End Get
        Set(ByVal value As Boolean)
            _TokenNoRequiredInKOT = value
        End Set
    End Property
    Enum enumCustomerSaleType
        Dine_In = 1
        Home_Delivery = 2
        Take_Away = 3
    End Enum

    Private CustomerSaleType As String

    Private _MettlerConnString As String
    Public Property MettlerConnString() As String
        Get
            Return _MettlerConnString
        End Get
        Set(ByVal value As String)
            _MettlerConnString = value
        End Set
    End Property
    Private _PrintCouponNumberOnKOT As Boolean
    Public Property PrintCouponNumberForKOT() As Boolean
        Get
            Return _PrintCouponNumberOnKOT
        End Get
        Set(ByVal value As Boolean)
            _PrintCouponNumberOnKOT = value
        End Set
    End Property
    Private _SpectrumAsMettler As Boolean 'vipul 
    Public Property SpectrumAsMettler() As Boolean
        Get
            Return _SpectrumAsMettler
        End Get
        Set(ByVal value As Boolean)
            _SpectrumAsMettler = value
        End Set
    End Property
    Private _Terminalid As String = "" 'vipin 
    Public Property Terminalid() As String
        Get
            Return _Terminalid
        End Get
        Set(ByVal value As String)
            _Terminalid = value
        End Set
    End Property
    Private _UserID As String = "" 'vipin 
    Public Property UserID() As String
        Get
            Return _UserID
        End Get
        Set(ByVal value As String)
            _UserID = value
        End Set
    End Property
    Private _EnableMailReSend As Boolean = False 'vipin 
    Public Property EnableMailReSend() As Boolean
        Get
            Return _EnableMailReSend
        End Get
        Set(ByVal value As Boolean)
            _EnableMailReSend = value
        End Set
    End Property
    Private _DocumentType As String = "" 'vipin 
    Public Property DocumentType() As String
        Get
            Return _DocumentType
        End Get
        Set(ByVal value As String)
            _DocumentType = value
        End Set
    End Property
    'added by khusrao adil on 06-08-2018  software CR
    Private _KOTCustomerSaleType As String 'vipul 
    Public Property KOTCustomerSaleType() As String
        Get
            Return _KOTCustomerSaleType
        End Get
        Set(ByVal value As String)
            _KOTCustomerSaleType = value
        End Set
    End Property
    'Public Property CustomerSaleType() As String
    '    Get
    '        Return _CustomerSaleType
    '    End Get
    '    Set(ByVal value As String)
    '        _CustomerSaleType = value
    '    End Set
    'End Property

    Function GetCustomerSaleType(ByVal CustomerSaleType As Integer) As String
        GetCustomerSaleType = String.Empty
        Select Case CustomerSaleType
            Case enumCustomerSaleType.Dine_In
                GetCustomerSaleType = "Dine In"
            Case enumCustomerSaleType.Home_Delivery
                GetCustomerSaleType = "Home Delivery"
            Case enumCustomerSaleType.Take_Away
                GetCustomerSaleType = "Take Away"
            Case Else
                GetCustomerSaleType = ""
        End Select
    End Function

    Private Shared _AllowBillingOnlyAfterSelectionOfSalesType As Boolean
    Public Shared Property AllowBillingOnlyAfterSelectionOfSalesType() As Boolean
        Get
            Return _AllowBillingOnlyAfterSelectionOfSalesType
        End Get
        Set(ByVal value As Boolean)
            _AllowBillingOnlyAfterSelectionOfSalesType = value
        End Set
    End Property

    Private Shared _PrintFormatNo As Integer
    Public Shared Property PrintFormatNo() As Integer
        Get
            Return _PrintFormatNo
        End Get
        Set(ByVal value As Integer)
            _PrintFormatNo = value
        End Set
    End Property

    Private _KOTPrintEachlineItem As Boolean
    Public Property KOTPrintEachlineItem() As Boolean
        Get
            Return _KOTPrintEachlineItem
        End Get
        Set(ByVal value As Boolean)
            _KOTPrintEachlineItem = value
        End Set
    End Property

    Private _KOTPrintForEachQuantity As Boolean
    Public Property KOTPrintForEachQuantity() As Boolean
        Get
            Return _KOTPrintForEachQuantity
        End Get
        Set(ByVal value As Boolean)
            _KOTPrintForEachQuantity = value
        End Set
    End Property


    Private _KotFontBold As Boolean
    Public Property IsKotFontBold() As Boolean
        Get
            Return _KotFontBold
        End Get
        Set(ByVal value As Boolean)
            _KotFontBold = value
        End Set
    End Property

    Private _KotFontLarge As Boolean
    Public Property IsKotFontLarge() As Boolean
        Get
            Return _KotFontLarge
        End Get
        Set(ByVal value As Boolean)
            _KotFontLarge = value
        End Set
    End Property

    Private _KOTPrintFormatNo As Integer
    Public Property KOTPrintFormatNo() As Integer
        Get
            Return _KOTPrintFormatNo
        End Get
        Set(ByVal value As Integer)
            _KOTPrintFormatNo = value
        End Set
    End Property

    Private _RoundOff As Integer
    Public Property RoundOff() As Integer
        Get
            Return _RoundOff
        End Get
        Set(ByVal value As Integer)
            _RoundOff = value
        End Set
    End Property
    Private _IsInvoiceSendOnMailRequired As Boolean = False
    Public Property IsInvoiceSendOnMailRequired() As Boolean
        Get
            Return _IsInvoiceSendOnMailRequired
        End Get
        Set(ByVal value As Boolean)
            _IsInvoiceSendOnMailRequired = value
        End Set
    End Property
    'code is added by irfan on 30-7-2018
    Private _IsInvoicePrintRequired As Boolean
    Public Property IsInvoicePrintRequired() As Boolean
        Get
            Return _IsInvoicePrintRequired
        End Get
        Set(ByVal value As Boolean)
            _IsInvoicePrintRequired = value
        End Set
    End Property

    Private _IsInvoicePrintFlag As Boolean
    Public Property IsInvoicePrintFlag() As Boolean
        Get
            Return _IsInvoicePrintFlag
        End Get
        Set(ByVal value As Boolean)
            _IsInvoicePrintFlag = value
        End Set
    End Property

    Private _SendInvoiceSMS As Boolean = False 'vipul
    Public Property SendInvoiceSMS() As Boolean
        Get
            Return _SendInvoiceSMS
        End Get
        Set(ByVal value As Boolean)
            _SendInvoiceSMS = value
        End Set
    End Property

    ''' <summary>
    ''' Class intillisation 
    ''' </summary>
    ''' <param name="BillNo">BillNo</param>
    ''' <remarks></remarks>
    ''' 
    Public Sub New(ByVal BillNo As String, ByVal HeaderPrint As Boolean, ByVal FooterPrint As Boolean, ByVal SalesPerson As Boolean, _
                   ByVal TaxDetail As Boolean, ByVal printPreview As Boolean, ByVal PrintType As String, ByVal cmPrinterInfo As DataTable, ByVal IsDisplayTotalSaving As Boolean, Optional ByVal vBarCodeType As String = "0")
        CashMemoNo = BillNo
        _HeaderPrint = HeaderPrint
        _FooterPrint = FooterPrint
        _SalesPerApplicable = SalesPerson
        _TaxDetail = TaxDetail
        _PrintPreview = printPreview
        _printType = PrintType
        dtPrinterInfo1 = cmPrinterInfo
        _IsDisplayTotalSaving = IsDisplayTotalSaving

        If PrintType = "A4" Then
            LineLength = 80
        ElseIf PrintType = "L40" Then
            LineLength = 40
        End If

        PBarCodeType = vBarCodeType

    End Sub

    'modified by khusrao adil on 08-12-2017
    'new flag "JKPrintFormatEnable" added for jk sprint 32
    Public Sub CashMemoPrint(ByVal dayopenDate As Date, ByVal LangCode As String, ByVal Currency As String, ByVal Sitecode As String, ByVal Type As String, ByVal DuplicatePrinting As String, ByVal DeletedUserid As String, ByVal AuthorisedUser As String, ByRef errorMsg As String, Optional ByVal GiftMsg As String = "", Optional ByVal BillAmt As String = "0", Optional ByVal PaidAmt As String = "0", Optional ByVal HierarachyWisePrintFlag As Boolean = False, Optional ByVal IsSavoy As Boolean = False, Optional ByVal IsArticleWiseKot As Boolean = False, Optional ByVal IsCounterCopy As Boolean = False, Optional ByVal IsFinalReceipt As Boolean = False, Optional ByVal KOTAndBillGeneration As Boolean = False, Optional ByVal ClientName As String = "", Optional ByVal DayCloseReportPath As String = "", Optional ByVal IsCounterCopyKot As Boolean = False, Optional ByVal IsKOTBillGenerationFromPRintButton As Boolean = False, Optional ByVal EvasPizzaChanges As Boolean = False, Optional ByVal NoOFCopies As Integer = 0, Optional ByVal IsRateVisible As Boolean = False, Optional ByVal IsTendersVisibleInPrintFormat7 As Boolean = False, Optional ByVal IsHsnAndTaxVisibleInPrintFormat6 As Boolean = False, Optional ByVal JKPrintFormatEnable As Boolean = False, Optional ByVal TokenNoRequiredInKOT As Boolean = False, Optional ByVal _tableNo As String = "")
        Try
            If PrintFormatNo = 1 Then
                CashMemoPrintFormat_Naturals(dayopenDate, LangCode, Currency, Sitecode, Type, DuplicatePrinting, DeletedUserid, AuthorisedUser, errorMsg, GiftMsg, BillAmt, PaidAmt, IsSavoy, KOTAndBillGeneration, ClientName:=ClientName, EvasPizzaChanges:=EvasPizzaChanges, NoOfCopies:=NoOFCopies, _tableNo:=_tableNo)
            ElseIf PrintFormatNo = 2 Then
                CashMemoPrintFormat_JamboKing(dayopenDate, LangCode, Currency, Sitecode, Type, DuplicatePrinting, DeletedUserid, AuthorisedUser, errorMsg, GiftMsg, HierarachyWisePrintFlag, IsSavoy, ClientName:=ClientName, NoOfCopies:=NoOFCopies)
            ElseIf PrintFormatNo = 3 Then
                CashMemoPrintFormat_PC(dayopenDate, LangCode, Currency, Sitecode, Type, DuplicatePrinting, DeletedUserid, AuthorisedUser, errorMsg, GiftMsg, IsSavoy, MettlerConnString, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName, IsCounterCopyKot:=IsCounterCopyKot, NoOfCopies:=NoOFCopies, HierarachyWisePrintFlag:=HierarachyWisePrintFlag)
            ElseIf PrintFormatNo = 4 OrElse PrintFormatNo = 5 OrElse PrintFormatNo = 6 OrElse PrintFormatNo = 7 Then
                'CashMemoPrintFormat_SSRS(dayopenDate, LangCode, Currency, Sitecode, Type, DuplicatePrinting, DeletedUserid, AuthorisedUser, errorMsg, GiftMsg, IsSavoy, MettlerConnString, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName, DayCloseReportPath, IsCounterCopyKot:=IsCounterCopyKot, KOTAndBillGeneration:=IsKOTBillGenerationFromPRintButton, EvasPizzaChanges:=EvasPizzaChanges, NoOfCopies:=NoOFCopies)
                'added by khusrao adil on 01-12-2017 for jk sprint 32
                If PrintFormatNo = 7 AndAlso JKPrintFormatEnable = True Then 'temprarily create as false
                    CashMemoPrintFormat_JKPrintFormatWith7(dayopenDate, LangCode, Currency, Sitecode, Type, DuplicatePrinting, DeletedUserid, AuthorisedUser, errorMsg, GiftMsg, BillAmt, PaidAmt, IsSavoy, MettlerConnString, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName, DayCloseReportPath, IsCounterCopyKot:=IsCounterCopyKot, KOTAndBillGeneration:=IsKOTBillGenerationFromPRintButton, EvasPizzaChanges:=EvasPizzaChanges, isratevisible:=IsRateVisible, IsTendersVisibleInPrintFormat7:=IsTendersVisibleInPrintFormat7)
                Else
                    CashMemoPrintFormat_SSRS(dayopenDate, LangCode, Currency, Sitecode, Type, DuplicatePrinting, DeletedUserid, AuthorisedUser, errorMsg, GiftMsg, BillAmt, PaidAmt, IsSavoy, MettlerConnString, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName, DayCloseReportPath, IsCounterCopyKot:=IsCounterCopyKot, KOTAndBillGeneration:=IsKOTBillGenerationFromPRintButton, EvasPizzaChanges:=EvasPizzaChanges, NoOfCopies:=NoOFCopies, isratevisible:=IsRateVisible, IsTendersVisibleInPrintFormat7:=IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=IsHsnAndTaxVisibleInPrintFormat6, TokenNoRequiredInKOT:=TokenNoRequiredInKOT)
                End If
                '----------------------------------------------------------------------------------------
                'added on 16 may - ashma 
                'ElseIf PrintFormatNo = 6 Then
                ' CashMemoPrintFormat_SSRS_Innoviti(dayopenDate, LangCode, Currency, Sitecode, Type, DuplicatePrinting, DeletedUserid, AuthorisedUser, errorMsg, GiftMsg, IsSavoy, MettlerConnString, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName, DayCloseReportPath, IsCounterCopyKot:=IsCounterCopyKot, KOTAndBillGeneration:=IsKOTBillGenerationFromPRintButton)
            ElseIf PrintFormatNo = 8 Then
                CashMemoPrintFormatA4_SSRS(dayopenDate, LangCode, Currency, Sitecode, Type, DuplicatePrinting, DeletedUserid, AuthorisedUser, errorMsg, GiftMsg, BillAmt, PaidAmt, IsSavoy, MettlerConnString, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName, DayCloseReportPath, IsCounterCopyKot:=IsCounterCopyKot, KOTAndBillGeneration:=IsKOTBillGenerationFromPRintButton, EvasPizzaChanges:=EvasPizzaChanges, isratevisible:=IsRateVisible)


            ElseIf PrintFormatNo = 9 Then
                CashMemoPrintFormat_GuruKrupa(dayopenDate, LangCode, Currency, Sitecode, Type, DuplicatePrinting, DeletedUserid, AuthorisedUser, errorMsg, GiftMsg, BillAmt, PaidAmt, IsSavoy, MettlerConnString, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName, DayCloseReportPath, IsCounterCopyKot:=IsCounterCopyKot, KOTAndBillGeneration:=IsKOTBillGenerationFromPRintButton)
            ElseIf PrintFormatNo = 11 Then
                CashMemoPrintFormat_SSRS11(dayopenDate, LangCode, Currency, Sitecode, Type, DuplicatePrinting, DeletedUserid, AuthorisedUser, errorMsg, GiftMsg, IsSavoy, MettlerConnString, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName, DayCloseReportPath, IsCounterCopyKot:=IsCounterCopyKot, KOTAndBillGeneration:=IsKOTBillGenerationFromPRintButton)
            Else
                CashMemoPrintFormat(dayopenDate, LangCode, Currency, Sitecode, Type, DuplicatePrinting, DeletedUserid, AuthorisedUser, errorMsg, GiftMsg, IsSavoy, ClientName:=ClientName, NoOfCopies:=NoOFCopies)
            End If
        Catch ex As Exception
        End Try

    End Sub

    'added by khusrao adil on 08-12-2017 for jk sprint 32
    'CashMemoPrintFormat_SSRS(dayopenDate, LangCode, Currency, Sitecode, Type, DuplicatePrinting, DeletedUserid, AuthorisedUser, errorMsg,                                                                                                                                                                                                                            GiftMsg, BillAmt, PaidAmt, IsSavoy, MettlerConnString, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName, DayCloseReportPath, IsCounterCopyKot:=IsCounterCopyKot, KOTAndBillGeneration:=IsKOTBillGenerationFromPRintButton, EvasPizzaChanges:=EvasPizzaChanges, isratevisible:=IsRateVisible, IsTendersVisibleInPrintFormat7:=IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=IsHsnAndTaxVisibleInPrintFormat6)
    Public Sub CashMemoPrintFormat_JKPrintFormatWith7(ByVal dayopenDate As Date, ByVal LangCode As String, ByVal Currency As String, ByVal Sitecode As String,
                                                       ByVal Type As String, ByVal DuplicatePrinting As String, ByVal DeletedUserid As String, ByVal AuthorisedUser As String,
                                                       ByRef errorMsg As String, Optional ByVal GiftMsg As String = "", Optional ByVal BillAmt As String = "",
                                                       Optional ByVal PaidAmt As String = "", Optional ByVal IsSavoy As Boolean = False, Optional ByVal MettlerConn As String = "",
                                                       Optional ByVal IsArticleWiseKot As Boolean = False, Optional ByVal IsCounterCopy As Boolean = False,
                                                       Optional ByVal IsFinalReceipt As Boolean = False, Optional ByVal ClientName As String = "",
                                                       Optional ByVal DayCloseReportPath As String = "", Optional ByVal IsCounterCopyKot As Boolean = False,
                                                       Optional ByVal KOTAndBillGeneration As Boolean = False, Optional ByVal EvasPizzaChanges As Boolean = False,
                                                       Optional ByVal NoOfCopies As Integer = 1, Optional ByVal isratevisible As Boolean = False,
                                                       Optional ByVal IsTendersVisibleInPrintFormat7 As Boolean = False)
        Try

            Dim strPrintMsg, sbDeliveryBillPrint As StringBuilder
            Dim strLineDetail As New StringBuilder
            Dim StrHeader, StrSubHeader, StrBody, StrCompanyInfo, StrTagLine, StrSubFooter, StrFooter,
                StrDuplicate, StrDeleteLine, StrCashMemoLine, StrCashierLine, StrSalesPersonLine As String
            Dim StrTokenNo, StrCustomerName As String
            Dim StrCMNo, StrCMDate, StrCMTime, StrCashier, StrSalesPerson As String
            Dim SiteLine, StrSiteTelline, StrAdrressLine, StrSiteReg1Line, StrSiteReg2Line As String
            Dim CLpLine, LineItemHeading, StrLineItem As String
            Dim CustomerNameLine, CLPPointsLine, CustmerPhoneNo As String
            Dim ItemCode, Desc, Qty, Rate, DiscAmt, DiscPer, TaxAmt, NetAmt, NetAmtTemp, RateAfterDisc, GrossAmt, FinScaleNo As String
            Dim TotalQtyLine, TakeAwayQuantity, TotalGrossAmtLine, TotalDiscAmtLine, SubTotalLine, NetAmtLine, TotalTaxAmtLine As String
            Dim TotalFooterTaxDetail, FooterTaxLine As String
            Dim TenderDetails, TenderLine As String
            Dim TotalPromotionalMsg, PromoMsgLine, StrPrintHeaderLine, StrPrintFooterLine As String
            Dim TermsNcond As String
            Dim strLine As String
            Dim strDblLine As String
            Dim strHomeDilery As String
            Dim isTakeAwayQtyPrint As Boolean = False
            errorMsg = ""
            Dim dtView As New DataTable
            Dim BoldLineLength As Int32 = 27
            'added by khusrao adil on 10-07-2017
            Dim objcom As New clsCommon
            Dim IsPosPassKey As Boolean = objcom.GetCLPProgram()
            Dim obj As New SpectrumBL.clsCashMemo()
            dtView = obj.GetCashMemo(CashMemoNo, Sitecode, LangCode)

            Dim dvItemDetail As New DataView(dtView, "", "", DataViewRowState.CurrentRows)
            'Dim DtUnique As DataTable = dvItemDetail.ToTable(True, "ArticleCode", "DISCRIPTION", "QUANTITY", "SELLINGPRICE", "GROSSAMT", "TOTALDISCOUNT", "TOTALDISCPERCENTAGE", "NETAMOUNT", "TOTALTAXAMOUNT")
            Dim DtUnique As DataTable = obj.GetBillDetailsDataForPrint(CashMemoNo, Sitecode, LangCode, True, Type)
            Dim Dtcombo As DataTable = obj.GetComboDetailsDataForPrint(CashMemoNo, Sitecode, LangCode)

            '==========================================================================================================================================================
            '----- Code Added By Mahesh - Delivery Person and customer Sales Type value pick up from Database 
            If Not IsDBNull(dtView.Rows(0)("ServiceTaxAmount")) AndAlso Val(dtView.Rows(0)("ServiceTaxAmount")) > 0 Then
                CustomerSaleType = GetCustomerSaleType(dtView.Rows(0)("ServiceTaxAmount"))
            Else
                CustomerSaleType = ""
            End If
            If Not IsDBNull(dtView.Rows(0)("DeliveryPersonID")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("DeliveryPersonID")) Then
                DeliveryPersonName = dtView.Rows(0)("DeliveryPersonID").ToString()
            End If

            If DtUnique Is Nothing Then Exit Sub
            Dim TotaltakeAwayQty = DtUnique.Compute("SUM(TakeAwayQuantity)", "").ToString()
            If Val(TotaltakeAwayQty) > 0 Then
                isTakeAwayQtyPrint = True
            End If

            Dim StrSubHeader1, StrSubFooter1, strWelcomeMsg, strTaxInformation, strPromotionMsg As New StringBuilder

            PrinttHeaderAndFooter(StrSubHeader1, StrSubFooter1, strWelcomeMsg, strPromotionMsg, strTaxInformation, "CMInvc", _printType)

            ' Dim strheaderbold As String = "<B>" & StrSubHeader1.ToString & "</B>"

            Dim tempStrSubFooter1 As String = StrSubFooter1.ToString()

            If Not String.IsNullOrEmpty(tempStrSubFooter1) Then

                StrSubFooter1.Replace(StrSubFooter1.ToString(), tempStrSubFooter1)
            End If
            'StrSubFooter1.Replace(StrSubFooter1.ToString(), tempStrSubFooter1)
            'StrSubFooter1.Append(tempStrSubFooter1)
            If StrSubFooter1 IsNot Nothing AndAlso Not String.IsNullOrEmpty(StrSubFooter1.ToString()) Then
                StrTagLine = StrSubFooter1.ToString()
            End If
            If dtView Is Nothing Then Exit Sub
            '______________________________________________________________________________________________________________________
            If Not dtView Is Nothing Then
                strLine = "-".PadRight(LineLength, "-") & vbCrLf
                strDblLine = "=".PadRight(LineLength, "=") & vbCrLf
                If DuplicatePrinting <> String.Empty Then
                    StrDuplicate = DuplicatePrinting
                End If
                'Rakesh-12.09.2013:Issue-7750
                If Not StrSubHeader1 Is Nothing AndAlso StrSubHeader1.ToString().Length > 0 Then
                    'If _printType <> "L40" Then
                    'StrSubHeader = strLine & SplitString(StrSubHeader1.ToString(), LineLength).ToString().Trim & vbCrLf
                    'modifed by khusrao adil on 12-07-2017 for print format changes -BillNo,Tax Invoice
                    'modifed by khusrao adil on 15-12-2017 for print format changes -BillNo,Tax Invoice
                    Dim strheaderbold As String = "<B>" & StrSubHeader1.ToString & "</B>"
                    Dim eidtrue As Boolean = False
                    Dim strtrue As String = StrSubHeader1.ToString().Trim
                    Dim filter1 = "" & vbCrLf & "" & vbCrLf & ""
                    Do While InStr(1, strtrue, filter1)
                        strtrue = Replace(strtrue, filter1, vbCrLf).Trim
                    Loop
                    Do While InStr(1, strtrue, "  ")
                        strtrue = Replace(strtrue, "  ", " ").Trim
                    Loop
                    filter1 = "" & vbCrLf & ""
                    Do While InStr(1, strtrue, filter1)
                        strtrue = Replace(strtrue, filter1, " ").Trim
                    Loop
                    strtrue = Trim(strtrue)
                    Dim strInvoiceText As String = ""
                    If strtrue.Contains("TAX INVOICE").ToString() _
                        OrElse strtrue.Contains("Tax Invoice").ToString() _
                        OrElse strtrue.Contains("Tax invoice").ToString() _
                        OrElse strtrue.Contains("tax Invoice").ToString() _
                        OrElse strtrue.Contains("tax invoice").ToString() Then
                        Dim strTextInvoice = ""
                        If strtrue.Contains("TAX INVOICE") Then
                            strTextInvoice = "TAX INVOICE"
                        ElseIf strtrue.Contains("Tax Invoice") Then
                            strTextInvoice = "Tax Invoice"
                        ElseIf strtrue.Contains("Tax invoice") Then
                            strTextInvoice = "Tax invoice"
                        ElseIf strtrue.Contains("tax Invoice") Then
                            strTextInvoice = "tax Invoice"
                        ElseIf strtrue.Contains("tax invoice") Then
                            strTextInvoice = "tax invoice"
                        End If

                        StrSubHeader1.Length = 0
                        StrSubHeader1.Append(strtrue)
                        StrSubHeader1.Replace(strTextInvoice, "")

                        Dim StrTAXINVOICE As String = strTextInvoice
                        Dim WalStrTAXINVOICE = Val(39 - StrTAXINVOICE.Length)
                        Dim WalDivideStrTAXINVOICE = WalStrTAXINVOICE / 2
                        strInvoiceText = Space(WalDivideStrTAXINVOICE) & "<B>" & StrTAXINVOICE & "</B>" & Space(WalDivideStrTAXINVOICE) & vbCrLf
                        eidtrue = True
                    End If
                    If eidtrue = True Then
                        Dim strSample As String = StrSubHeader1.ToString().Trim
                        If strSample.Length <> 0 Then
                            Dim filter = "" & vbCrLf & "" & vbCrLf & ""
                            Do While InStr(1, strSample, filter)
                                strSample = Replace(StrSubHeader1.ToString(), filter, vbCrLf).Trim
                            Loop
                            filter = "" & vbCrLf & ""
                            Do While InStr(1, strSample, filter)
                                strSample = Replace(strSample, filter, " ").Trim
                            Loop
                            strSample = Trim(strSample)
                            StrSubHeader1.Length = 0
                            StrSubHeader1.Append(strSample)
                            StrSubHeader = strInvoiceText & SplitStringPC(StrSubHeader1.ToString(), 38, NoMoreSpace:=True, blankSpace:=0).ToString().Trim & vbCrLf
                        Else
                            'GoTo LineElse
                            StrSubHeader = strInvoiceText & SplitStringPC(StrSubHeader1.ToString(), 38, NoMoreSpace:=True, blankSpace:=0).ToString().Trim
                        End If
                        'StrSubHeader = strInvoiceText & SplitString(StrSubHeader1.ToString(), 39, blankSpace:=0).ToString().Trim & vbCrLf
                    Else
                        StrSubHeader = SplitStringPC(StrSubHeader1.ToString(), 39, NoMoreSpace:=True, blankSpace:=0).ToString().Trim '& vbCrLf
                        'StrSubHeader = SplitString(StrSubHeader1.ToString(), 39, blankSpace:=0).ToString().Trim '& vbCrLf
                    End If
                    'StrSubHeader = SplitString(strheaderbold.ToString(), LineLength).ToString().Trim & vbCrLf
                    '   StrSubHeader = SplitString(strheaderbold.ToString(), 28).ToString().Trim '& vbCrLf
                    StrSubHeader = StrSubHeader.PadLeft((Val(LineLength) + StrSubHeader.Length) / 2)
                    StrSubHeader = strLine & StrSubHeader
                    'End If
                    ' StrSubHeader = strLine & SplitString(StrSubHeader1.ToString(), LineLength).ToString().Trim & vbCrLf
                End If
                If DuplicatePrinting <> String.Empty Then
                    'StrHeader = "<B>" & StrDuplicate.PadLeft(((LineLength - StrDuplicate.Length) / 2) + StrDuplicate.Length, " ") & "</B>" & vbCrLf
                    ' StrHeader = "<B>" & StrDuplicate.PadLeft(15) & "</B>" & vbCrLf
                    'StrHeader = StrDuplicate.PadLeft(15) & vbCrLf
                    Dim StrDuplicateDD As String = StrDuplicate
                    Dim WalTotalSpaceDD = Val(39 - DuplicatePrinting.Length)
                    Dim WalDivideSpaceDD = WalTotalSpaceDD / 2
                    StrHeader &= Space(WalDivideSpaceDD) & StrDuplicateDD & Space(WalDivideSpaceDD) & vbCrLf
                    ' StrHeader = (Space(12) & StrDuplicate) & vbCrLf
                End If
                '---Put all code in DCM by Mahesh for performance Optimize ...
                If Type = "DCM" Then
                    StrDeleteLine = getValueByKey("CLSCMP001")
                    StrHeader = StrDeleteLine & vbCrLf
                    'Request ID: <Update / Delete Request ID>
                    'Authorized By : <User Name >       Deleted at Time: <CM Time>
                    'StrHeader = StrHeader & "Request ID    :" & DeletedUserid & vbCrLf
                    'StrHeader = StrHeader & "Authorized By :" & AuthorisedUser & vbCrLf
                    StrHeader = StrHeader & getValueByKey("CLSCMP002") & DeletedUserid & vbCrLf
                    StrHeader = StrHeader & getValueByKey("CLSCMP003") & AuthorisedUser & vbCrLf
                End If
                Dim SiteName, Site As String
                Dim TS_SiteName As StringBuilder
                'SiteName = "Store Name: " & dtView.Rows(0)("SITEOFFICIALNAME").ToString()
                'Site = "Code: " & dtView.Rows(0)("SITECODE").ToString()
                'StrSiteTelline = "Tel: " & dtView.Rows(0)("TELNO").ToString()
                'StrAdrressLine = "Add: " & dtView.Rows(0)("ADDRESS").ToString()

                SiteName = getValueByKey("CLSCMP004") & " " & dtView.Rows(0)("SITEOFFICIALNAME").ToString()
                SiteName = "(" & dtView.Rows(0)("SITEOFFICIALNAME").ToString() & ")"

                TS_SiteName = SplitString(SiteName)
                SiteName = TS_SiteName.ToString
                SiteName = SiteName.PadLeft(LineLength / 2 + (SiteName.Length / 2))
                Site = vbCrLf & getValueByKey("CLSCMP005") & " " & dtView.Rows(0)("SITECODE").ToString()
                'SiteLine = SiteName & Site
                If ClientName <> "" Then
                    Dim arrClientname = ClientName.Split(" ").ToArray()
                    For i = 0 To arrClientname.Length - 1
                        If SiteName.ToUpper.Contains(arrClientname(i).ToString.ToUpper) Then
                            SiteName = SiteName.ToUpper.Replace(arrClientname(i).ToString.ToUpper.Trim, "").Trim
                        End If
                    Next
                End If
                SiteName = SiteName.PadLeft((Val(LineLength - SiteName.Length) + SiteName.Length) / 2)
                Site = vbCrLf & getValueByKey("CLSCMP005") & " " & dtView.Rows(0)("SITECODE").ToString()
                ' SiteLine = SiteName.Trim.PadLeft((Val(55 - SiteName.Length) + SiteName.Length) / 2)
                SiteLine = SiteName

                StrSiteTelline = getValueByKey("CLSCMP006") & " " & dtView.Rows(0)("TELNO").ToString()
                StrAdrressLine = getValueByKey("CLSCMP007") & " " & dtView.Rows(0)("ADDRESS").ToString()
                'StrAdrressLine = SplitStringPC(StrAdrressLine, LineLength, NoMoreSpace:=True).ToString() 'SplitString(StrAdrressLine, LineLength).ToString()
                'code added for issue id 2148 by vipul
                'StrAdrressLine = SplitStringPC(StrAdrressLine, LineLength, NoMoreSpace:=True).ToString()
                'StrSiteReg1Line = "LST NO: " & dtView.Rows(0)("LOCALSALESTAXNO").ToString() & Space(2) & "LST Date: " & dtView.Rows(0)("LOCALSALESTAXDATE").ToString()
                'StrSiteReg2Line = "CST NO: " & dtView.Rows(0)("CENTRALSALESTAXNO").ToString() & Space(2) & "CST Date: " & dtView.Rows(0)("CENTRALSALESTAXDATE").ToString()
                'StrHeader &= "<B>" & ClientName.PadLeft(15) & "</B>" & vbCrLf
                'Dim strClientName As String = "<B>" & ClientName.PadLeft(1) & "</B>"
                Dim strClientName As String = ClientName
                Dim WalTotalSpace = Val(38 - ClientName.Length)
                Dim WalDivideSpace = WalTotalSpace / 2
                StrHeader &= Space(WalDivideSpace) & "<B1>" & strClientName & "</B1>" & Space(WalDivideSpace) & vbCrLf & vbCrLf
                'Dim strClientName As String = ClientName.PadLeft(1)
                'StrHeader &= Space(18 - Val(ClientName.Length)) & "<B>" & strClientName & "</B>" & vbCrLf
                StrHeader &= SiteLine & vbCrLf
                Dim strAddress As String = StrAdrressLine & StrSiteTelline & vbCrLf
                Dim strFullAddress As String = SplitStringPC(strAddress, LineLength, NoMoreSpace:=True, blankSpace:=4).ToString
                'StrHeader = StrHeader & StrAdrressLine.Trim & vbCrLf
                'StrHeader = StrHeader & StrSiteTelline & vbCrLf
                StrHeader = StrHeader & strFullAddress
                'StrHeader = StrHeader & strLine & vbCrLf
                'StrSubHeader = StrSubHeader & StrSiteReg1Line & vbCrLf
                'StrSubHeader = StrSubHeader & StrSiteReg2Line & vbCrLf
                'Dim kotDataLine As String = String.Empty
                '****Added by Rahul 30 nov 2009 night(Rashid) . start 
                If Type = "DCM" Then
                    'code adde by khusrao adil on 14-05-2018 jk sprint 36 CR
                    Dim _strShortBillNo = dtView.Rows(0)("Billno").ToString()
                    _strShortBillNo = _strShortBillNo.Substring(_strShortBillNo.Length - 5)
                    StrCMNo = "Void Bill No. :" & _strShortBillNo
                Else
                    Dim _strShortBillNo = dtView.Rows(0)("Billno").ToString()
                    _strShortBillNo = _strShortBillNo.Substring(_strShortBillNo.Length - 5)
                    StrCMNo = "Bill No:" & _strShortBillNo
                End If
                StrCMDate = dayopenDate.ToShortDateString()
                strDayOpenDate = dayopenDate.ToShortDateString()
                If DuplicatePrinting <> String.Empty Then
                    StrCMDate = Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())
                End If
                If PrintCouponNumberForKOT = True Then
                    'Dim CouponNo As String = strTillno.Substring(0, 1) & strTillno.Substring(strTillno.Length - 2, 2) & strBillno.Substring(strBillno.Length - 2, 2)
                    Dim tillNo As String = dtView.Rows(0)("TerminalId").ToString()
                    Dim billno As String = dtView.Rows(0)("Billno").ToString()
                    Dim CouponNo As String = tillNo.Substring(0, 1) & tillNo.Substring(tillNo.Length - 2, 2) & billno.Substring(billno.Length - 2, 2)
                    StrTokenNo = CouponNo
                Else
                    StrTokenNo = dtView.Rows(0)("Billno").ToString().Trim.Substring(dtView.Rows(0)("Billno").ToString().Trim.Length - 2, 2)
                End If
                ' StrTokenNo = "<B>" & StrTokenNo & "</B>"
                ' StrTokenNo = "" & StrTokenNo & ""
                'code adde by khusrao adil on 14-05-2018 jk sprint 36 CR
                StrCMTime = Format(dtView.Rows(0)("BillTime"), "hh:mm:sstt")
                Dim tcmdate = " Date:" & StrCMDate & " " & StrCMTime
                StrCashMemoLine = StrCMNo.PadRight(LineLength - 1 - tcmdate.Length) & tcmdate & vbCrLf
                'StrCashier = "Cashier:" & dtView.Rows(0)("Createdby").ToString()
                'StrCMTime = "Time:" & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")

                'StrCashier = getValueByKey("CLSPV006") & dtView.Rows(0)("Createdby").ToString()
                StrCashier = getValueByKey("CLSPV006") & LoginUserName
                StrCMTime = getValueByKey("CLSCMP032") & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")
                StrCashierLine = StrCashier.PadRight(LineLength - 1 - StrCMTime.Length) & StrCMTime & vbCrLf
                'StrSalesPerson = "Sales Person:" & dtView.Rows(0)("SALESEXECUTIVECODE").ToString()
                StrSalesPerson = getValueByKey("CLSCMP010") & dtView.Rows(0)("SALESEXECUTIVECODE").ToString()
                If _SalesPerApplicable = True Then
                    StrSalesPersonLine = StrSalesPerson & vbCrLf
                End If
                If (dtView.Rows.Count() > 0 And String.IsNullOrEmpty(DeliveryPersonName)) Then
                    If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                        ' strPrintMsg.Append(StrTokenNo)
                        '  StrTokenNo = "Token No. " & StrTokenNo
                        If Not AllowBillingOnlyAfterSelectionOfSalesType Then
                            '   strPrintMsg.Append(CustomerSaleType)
                            CustomerSaleType = String.Empty
                        End If
                        '  Dim StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - (CustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "")).Length) & CustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "") & vbCrLf
                        ' StrSubHeader = StrSubHeader & strLine
                        '  StrSubHeader = StrSubHeader '& StrTokenSalesTypeLine
                    Else
                        If AllowBillingOnlyAfterSelectionOfSalesType Then
                            StrSubHeader = StrSubHeader & CustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "") & vbCrLf
                        Else
                            StrSubHeader = StrSubHeader & CustomerSaleType + vbCrLf
                        End If
                    End If
                Else
                    If AllowBillingOnlyAfterSelectionOfSalesType Then
                        StrSubHeader = StrSubHeader & CustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "") + vbCrLf
                    Else
                        ' StrSubHeader = StrSubHeader & CustomerSaleType + vbCrLf
                    End If
                End If
                StrSubHeader = StrSubHeader & strLine
                StrSubHeader = StrSubHeader & StrCashMemoLine
                'StrSubHeader = StrSubHeader '& StrCashierLine
                If (Not String.IsNullOrEmpty(dtView.Rows(0)("SALESEXECUTIVECODE").ToString())) Then
                    StrSubHeader = StrSubHeader & StrSalesPersonLine
                End If
                Dim CLPaddress As String = ""
                Dim CLPBalancePoints As String
                Dim clpaddressnew As String = ""
                Dim customername As String = ""
                Dim customerPhoneno As String = ""
                Dim DeliveryName As String = ""
                Dim DeliveryDate As DateTime
                Dim ReminderComments As String = ""
                If dtView.Rows(0)("CLPNO").ToString() <> String.Empty Then

                    customerPhoneno = dtView.Rows(0)("MobileNo").ToString()
                    CLpLine = getValueByKey("CLSCMP011") & dtView.Rows(0)("CLPNO").ToString()

                    customername = dtView.Rows(0)("CLPNAME").ToString().Trim
                    clpaddressnew = dtView.Rows(0)("CLPAddress").ToString()
                    clpaddressnew = clpaddressnew.Trim
                    CLPPointsLine = getValueByKey("CLSCMP013") & FormatNumber(Val(dtView.Rows(0)("CLPPoints").ToString()))

                    ReminderComments = dtView.Rows(0)("ReminderComments").ToString()
                    Dim dblCLPPoints As Double
                    dblCLPPoints = Val(dtView.Rows(0)("BalancePoints").ToString())
                    If (dtView.Rows(0)("RedemptionPoints") IsNot DBNull.Value AndAlso dtView.Rows(0)("RedemptionPoints") > 0) Then
                        CLPPointsLine += vbCrLf + getValueByKey("CLSCMP034") & FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()))
                    End If
                    Dim RedemptionPoints As DataRow = dtView.Select("Tendertypecode = 'CLPPoint'").FirstOrDefault()
                    If (RedemptionPoints IsNot Nothing AndAlso RedemptionPoints("RedemptionPoints") IsNot DBNull.Value) Then
                        dblCLPPoints -= Val(RedemptionPoints("RedemptionPoints").ToString())
                    End If
                    StrSubFooter = ""
                    CLPBalancePoints = ""
                    CLPBalancePoints = "Balance Loyalty Points :"
                    CLPBalancePoints = SplitStringPC(CLPBalancePoints, LineLength, NoMoreSpace:=True, blankSpace:=(100 - CLPBalancePoints.Length)).ToString() & FormatNumber(dblCLPPoints.ToString())
                    CustomerNameLine = getValueByKey("CLSCMP012") & Space(2) & dtView.Rows(0)("CLPNAME").ToString()
                    CustmerPhoneNo = getValueByKey("RP187") & Space(4) & ":" & Space(3) & dtView.Rows(0)("Mobileno").ToString()
                    CLPBalancePoints = String.Empty
                    CLPBalancePoints = getValueByKey("CLSCMP015")
                    StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
                    If CustomerSaleType = "Home Delivery" Then
                        CLPaddress = dtView.Rows(0)("CLPAddress").ToString()
                        If CLPaddress <> String.Empty Then
                            'CLPaddress = getValueByKey("CLSCMP014") & Space(7) & dtView.Rows(0)("CLPAddress").ToString()
                            CLPaddress = "Address" & Space(6) & ":" & Space(3) & dtView.Rows(0)("CLPAddress").ToString()
                        End If
                        If CLPaddress <> String.Empty Then
                            ' CLPaddress = SplitString(CLPaddress, LineLength, ).ToString()
                            CLPaddress = SplitStringPC(CLPaddress, LineLength, NoMoreSpace:=True, blankSpace:=17).ToString()
                        End If
                        StrSubFooter = StrSubFooter & CustmerPhoneNo & vbCrLf & CLPaddress & vbCrLf
                    Else
                        Dim tempMobileno = customerPhoneno
                        If customerPhoneno.Length > 4 Then
                            Dim mobileLast4Digit = tempMobileno.Substring(customerPhoneno.Length - 4, 4)
                            Dim mobilemolength = customerPhoneno.Length
                            customerPhoneno = mobileLast4Digit.PadLeft(customerPhoneno.Length, "X")
                        End If
                        CustmerPhoneNo = getValueByKey("RP187") & Space(4) & ":" & Space(3) & customerPhoneno
                        StrSubFooter = StrSubFooter & CustmerPhoneNo & vbCrLf
                    End If

                    Dim retAmt As Double
                    If CDbl(PaidAmt) >= CDbl(BillAmt) Then
                        retAmt = CDbl(PaidAmt) - CDbl(BillAmt)
                    End If
                    If EvasPizzaChanges = True AndAlso Not String.IsNullOrEmpty(ReminderComments) Then
                        StrSubFooter = StrSubFooter & SplitStringPC("Cust. Instructions: " & ReminderComments, LineLength, NoMoreSpace:=True).ToString() & vbCrLf
                    End If
                    If (dtView.Rows.Count() > 0 And String.IsNullOrEmpty(DeliveryPersonName)) Then
                        If Val(PaidAmt) > 0 Then
                            StrSubFooter = StrSubFooter & getValueByKey("CLIST04") & " " & FormatNumber(CDbl(PaidAmt), 2) & vbCrLf
                            StrSubFooter = StrSubFooter & getValueByKey("CLIST05") & " " & FormatNumber(CDbl(retAmt), 2) & vbCrLf
                        End If
                    End If
                ElseIf dtView.Columns.Contains("CustomerNo") AndAlso dtView.Rows(0)("CustomerNo").ToString() <> String.Empty Then
                    CLpLine = getValueByKey("CLSCMP038") & dtView.Rows(0)("CustomerNo").ToString()
                    CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CustomerName").ToString()
                    If CLpLine <> Nothing AndAlso CLpLine.ToString().Length > 0 Then
                        StrSubFooter = StrSubFooter & CLpLine & vbCrLf
                    End If
                    StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
                End If
                If Type = "CMWAmt" Then
                    LineItemHeading = getValueByKey("CLSPSO020") & Space(17) & vbCrLf & getValueByKey("CLSCMP016") & Space(20) & getValueByKey("CLSPSO022") & " " & vbCrLf
                Else
                    LineItemHeading = String.Empty
                    If isratevisible = True Then
                        'LineItemHeading = getValueByKey("CLSCMP016") & Space(7) & getValueByKey("CLSPSO022") & " " & Space(4)
                        LineItemHeading = "Items" & Space(13) & getValueByKey("CLSPSO022") & " " & Space(4)
                        LineItemHeading &= "Rate" & Space(5)
                        LineItemHeading &= "Amt" & vbCrLf
                    Else
                        LineItemHeading = getValueByKey("CLSCMP016") & Space(15) & getValueByKey("CLSPSO022") & " " & Space(5)
                        LineItemHeading &= "Amt" & vbCrLf
                    End If
                End If
                Dim TotalQty, TGrossAmt, TDiscAmt, TRateAfterDisc, TTaxtAmt As String
                Dim TNetAmt As String = 0.0
                Dim BillLineNO As String
                DtScannedMettlerBills = obj.GetMettlerBillDetailsDataForPrint(CashMemoNo, Sitecode)
                Dim ScaleNo As String
                Dim objItemSch As New clsIteamSearch()
                Dim dtScanedBillArticle As New DataTable
                Dim dtNewArticleScaledtls As New DataTable
                Dim dtTender As DataTable 'sagar
                dtNewArticleScaledtls.Columns.Add("Article", System.Type.GetType("System.String"))
                dtNewArticleScaledtls.Columns.Add("ScaleNo", System.Type.GetType("System.String"))
                For ScanBillIndex = 0 To DtScannedMettlerBills.Rows.Count - 1
                    '------- GET Mettler Scanned Bill Details From Mettler DataBase 
                    With DtScannedMettlerBills
                        ScaleNo = Val(.Rows(ScanBillIndex)("SCALE_NO"))
                        dtScanedBillArticle = objItemSch.GetScanedBillArticle(ScaleNo:=ScaleNo, BillNo:=.Rows(ScanBillIndex)("Bill_No"), BillIntDate:=.Rows(ScanBillIndex)("MettlerScaleBillDate"), MettlerConn:=MettlerConnString) '    ScaleBillIntDate = Val(.Rows(ScanBillIndex)("MettlerScaleBillDate"))
                        If dtScanedBillArticle IsNot Nothing Then
                            For Each drscale As DataRow In dtScanedBillArticle.Rows
                                Dim drnew As DataRow = dtNewArticleScaledtls.NewRow
                                drnew("Article") = drscale("LegacyArticleCode")
                                drnew("ScaleNo") = ScaleNo
                                dtNewArticleScaledtls.Rows.Add(drnew)
                            Next
                        End If
                    End With
                Next
                DtUnique.Columns.Add("ScaleNo", System.Type.GetType("System.Int32"))
                For Each drscaleno As DataRow In dtNewArticleScaledtls.Rows
                    Dim dr() As DataRow = DtUnique.Select("ArticleCode= '" & drscaleno("Article") & "'")
                    If dr.Length > 0 Then
                        dr(0)("ScaleNo") = drscaleno("ScaleNo")
                    End If
                Next
                If Not dtNewArticleScaledtls Is Nothing Then
                    If dtNewArticleScaledtls.Rows.Count = 0 Then
                        For Each druniq As DataRow In DtUnique.Rows
                            druniq("ScaleNo") = 0
                        Next
                    Else
                        For Each druniq As DataRow In DtUnique.Rows
                            If Convert.ToString(druniq("ScaleNo")) = "" Then
                                druniq("ScaleNo") = 0
                            End If
                        Next
                    End If
                End If
                For Each dr As DataRow In DtUnique.Rows
                    ItemCode = dr("ArticleCode").ToString()
                    If (DisplayArticleFullName) Then
                        Desc = dr("ArticleFullName").ToString()
                    Else
                        Desc = dr("DISCRIPTION").ToString()
                    End If
                    BillLineNO = dr("BillLineNO").ToString()
                    If SpectrumCommon.ComboItemPrintingAllowed AndAlso Dtcombo IsNot Nothing AndAlso Dtcombo.Rows.Count > 0 Then
                        Dim drp() = Dtcombo.Select("ComboArticleCode='" & ItemCode & "' AND BillLineNO ='" & BillLineNO & "'")
                        If drp.Length > 0 Then
                            Dim articleDescriptionDictionary As New Dictionary(Of String, Integer)
                            For index = 0 To drp.Length - 1
                                If articleDescriptionDictionary.ContainsKey(drp(index)("ArticleName")) Then
                                    articleDescriptionDictionary(drp(index)("ArticleName")) = articleDescriptionDictionary(drp(index)("ArticleName")) + drp(index)("Quantity")
                                Else
                                    articleDescriptionDictionary.Add(drp(index)("ArticleName"), drp(index)("Quantity"))
                                End If
                            Next
                            Dim AddedDesc = GetMultilinedString(articleDescriptionDictionary)
                            Desc = Desc & vbCrLf & AddedDesc
                        End If
                    End If
                    FinScaleNo = dr("ScaleNo").ToString()
                    Qty = dr("Quantity").ToString()
                    Rate = dr("SellingPrice").ToString()
                    RateAfterDisc = (dr("SellingPrice") - dr("TOTALDISCOUNT")).ToString()
                    If CDbl(clsCommon.CheckIfBlank(Rate)) < 0 Then
                        Rate = CDbl(clsCommon.CheckIfBlank(Rate)) * -1
                    End If
                    DiscAmt = dr("TOTALDISCOUNT").ToString()
                    DiscPer = dr("TOTALDISCPERCENTAGE").ToString()
                    NetAmt = dr("NetAmount").ToString()
                    GrossAmt = dr("GROSSAMT").ToString()
                    TaxAmt = dr("TOTALTAXAMOUNT").ToString()
                    If (AllowDecimalQty) Then
                        If (WeightScaleEnabled) Then
                            Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 2)
                        Else
                            Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 2)
                        End If
                        'Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 0)  'temprarily
                    Else
                        Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 0)
                    End If
                    Rate = FormatNumber(CDbl(clsCommon.CheckIfBlank(Rate)), 2)
                    RateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(RateAfterDisc)), 2)
                    DiscAmt = FormatNumber(CDbl(IIf(DiscAmt <> String.Empty, DiscAmt, 0)), 2)
                    DiscPer = FormatNumber(CDbl(IIf(DiscPer <> String.Empty, DiscPer, 0)), 2)
                    DiscPer = DiscPer & "%"
                    'Format of NetAmt with No Digits after Decimal
                    NetAmtTemp = NetAmt
                    NetAmt = FormatNumber(CDbl(NetAmt), 0)
                    ' NetAmtTemp = FormatNumber(CDbl(NetAmt), 2)
                    ' NetAmt = FormatNumber(CDbl(NetAmt), 2)
                    GrossAmt = FormatNumber(CDbl(GrossAmt), 2)
                    'GrossAmt = FormatNumber(CDbl(GrossAmt), 0)
                    'For i = NetAmt.Length To 8
                    '    NetAmt = " " & NetAmt
                    'Next
                    TaxAmt = FormatNumber(CDbl(IIf(TaxAmt <> String.Empty, TaxAmt, 0)), 2)
                    Dim TempNetAmt As String = "0"
                    If Type = "CMWAmt" Then
                        StrLineItem = ItemCode.PadRight(26) & vbCrLf & Desc.PadRight(35) & Qty & vbCrLf
                    Else
                        StrLineItem = String.Empty
                        If 28 - Desc.Length < Qty.Length Then
                            StrLineItem = SplitStringPC(Desc, 18, NoMoreSpace:=True).ToString.Trim & vbCrLf
                            If isratevisible = True Then
                                'StrLineItem = SplitStringPC(Desc, 18, NoMoreSpace:=True).ToString.Trim & Space(18 - IIf(strLastLength > 16, 16, strLastLength)) & Qty.PadLeft(3) & Rate.PadLeft(8) & GrossAmt.PadLeft(10) & vbCrLf
                                If AllowDecimalQty = True Then
                                    If Qty.Length > 5 Then
                                        StrLineItem = SplitStringPC(Desc, 17, NoMoreSpace:=True).ToString.Trim & Space(17 - IIf(strLastLength > 16, 16, strLastLength)) & Qty.PadLeft(4) & Rate.PadLeft(7) & GrossAmt.PadLeft(9) & vbCrLf
                                    Else
                                        StrLineItem = SplitStringPC(Desc, 18, NoMoreSpace:=True).ToString.Trim & Space(18 - IIf(strLastLength > 16, 16, strLastLength)) & Qty.PadLeft(5) & Rate.PadLeft(8) & GrossAmt.PadLeft(8) & vbCrLf
                                    End If
                                Else
                                    StrLineItem = SplitStringPC(Desc, 18, NoMoreSpace:=True).ToString.Trim & Space(18 - IIf(strLastLength > 16, 16, strLastLength)) & Qty.PadLeft(3) & Rate.PadLeft(8) & GrossAmt.PadLeft(10) & vbCrLf
                                End If
                            Else
                                StrLineItem = StrLineItem & Qty.PadLeft(28) & GrossAmt.PadLeft(11) & vbCrLf
                            End If
                        Else
                            If isratevisible = True Then
                                StrLineItem = String.Empty
                                If AllowDecimalQty = True Then
                                    If Qty.Length > 5 Then
                                        StrLineItem = SplitStringPC(Desc, 17, NoMoreSpace:=True).ToString.Trim & Space(17 - IIf(strLastLength > 16, 16, strLastLength)) & Qty.PadLeft(4) & Rate.PadLeft(8) & GrossAmt.PadLeft(8) & vbCrLf
                                    Else
                                        StrLineItem = SplitStringPC(Desc, 18, NoMoreSpace:=True).ToString.Trim & Space(18 - IIf(strLastLength > 16, 16, strLastLength)) & Qty.PadLeft(5) & Rate.PadLeft(8) & GrossAmt.PadLeft(8) & vbCrLf
                                    End If
                                Else
                                    StrLineItem = SplitStringPC(Desc, 18, NoMoreSpace:=True).ToString.Trim & Space(18 - IIf(strLastLength > 16, 16, strLastLength)) & Qty.PadLeft(3) & Rate.PadLeft(8) & GrossAmt.PadLeft(10) & vbCrLf
                                End If
                            Else
                                StrLineItem = Desc & Qty.PadLeft(28 - Desc.Length) & GrossAmt.PadLeft(11) & vbCrLf
                            End If
                        End If
                    End If
                    strLineDetail.Append(StrLineItem)
                    If CDbl(Qty.Trim) > 0 Then
                        TotalQty = CDbl(clsCommon.CheckIfBlank(TotalQty)) + CDbl(Qty.Trim)
                    End If
                    TDiscAmt = CDbl(clsCommon.CheckIfBlank(TDiscAmt)) + CDbl(DiscAmt.Trim)
                    TGrossAmt = CDbl(clsCommon.CheckIfBlank(TGrossAmt)) + CDbl(dr("GROSSAMT").ToString())
                    'TNetAmt = CDbl(clsCommon.CheckIfBlank(TNetAmt)) + CDbl(NetAmt.Trim)
                    'TNetAmt = Format(Math.Round(Convert.ToDecimal(CDbl(clsCommon.CheckIfBlank(TNetAmt)) + CDbl(NetAmt.Trim))), "0.00")
                    TNetAmt = Decimal.Add(FormatNumber(CDbl(NetAmtTemp), 2).ToString(), TNetAmt)
                    TRateAfterDisc = CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)) + CDbl(RateAfterDisc.Trim)
                    TTaxtAmt = CDbl(clsCommon.CheckIfBlank(TTaxtAmt)) + CDbl(TaxAmt.Trim)
                Next
                TotalQty = FormatNumber(CDbl(clsCommon.CheckIfBlank(TotalQty)), 2)
                TDiscAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TDiscAmt)), 2)
                'TDiscAmt = FormatNumber(dtView.Rows(0)("Discount"), 2)
                TGrossAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TGrossAmt)), 2)
                'TNetAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TNetAmt)), 2)
                TNetAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TNetAmt)), RoundOff), 2)
                TRateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)), 2)
                TTaxtAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TTaxtAmt)), 2)
                StrBody = strLineDetail.ToString()
                StrBody = StrBody & strLine
                Dim TSubTotalAmt As String
                Dim TotalDisPercent As String
                If Type <> "CMWAmt" Then
                    If CDbl(clsCommon.CheckIfBlank(TDiscAmt)) > 0 Then
                        TotalDisPercent = Math.Round((CDbl(clsCommon.CheckIfBlank(TDiscAmt)) / CDbl(clsCommon.CheckIfBlank(TGrossAmt))) * 100, 1).ToString()
                        TSubTotalAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TGrossAmt)) - CDbl(clsCommon.CheckIfBlank(TDiscAmt)), RoundOff), 0)
                        TDiscAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TDiscAmt)), RoundOff), 0)
                        Dim ObjclsCommon As New clsCommon
                        Dim offerno As String = ObjclsCommon.GetOffernoForRoundOff()
                        If DtUnique.Rows(0)("PromotionId").ToString().Contains(offerno) And offerno <> "" Then
                            TotalDiscAmtLine = "Round Off" & Space(BoldLineLength / 2 - "Round Off".Length) & " :" & TDiscAmt.PadLeft(BoldLineLength - ("Round Off".Length + Space(BoldLineLength / 2 - "Round Off".Length).Length + ":".Length))
                        Else
                            TotalDiscAmtLine = String.Empty
                            Dim CheckIfPositiveNumber = IIf(BoldLineLength / 2 - (String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).Length) > 0, BoldLineLength / 2 - String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).Length, 0)
                            ' TotalDiscAmtLine = String.Format(getValueByKey("CLSCMP019"), TotalDisPercent) & Space(CheckIfPositiveNumber) & " :" & TDiscAmt.PadLeft(BoldLineLength - (String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).Length + ": ".Length))
                            ' Dim discLine As String = String.Format(getValueByKey("CLSCMP019"), TotalDisPercent) & Space(CheckIfPositiveNumber)
                            Dim discLine As String = " Discount:"
                            Dim dicLineAmt As String = "INR :" & TDiscAmt
                            TotalDiscAmtLine = "<B>" & discLine & dicLineAmt.PadLeft(28 - discLine.Length) & "</B>"
                        End If
                        '--------------------
                    End If
                    TNetAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TNetAmt)), RoundOff), 2)
                    TRateAfterDisc = TRateAfterDisc & " " & Currency
                    TTaxtAmt = TTaxtAmt & " " & Currency
                    '    TotalGrossAmtLine = "<B>" & Space(10) & "Sub Total :" & TGrossAmt.PadLeft(17 - Val("Sub Total :".Length + Space(1).Length)) & "</B>" & vbCrLf
                    ' TotalGrossAmtLine = Space(10) & "Sub Total" & ":" & Space(2) & TGrossAmt.PadLeft(17 - Val("Sub Total".Length + Space(2).Length)) & vbCrLf
                    TotalGrossAmtLine = ""
                    Dim strSubTotal = "Sub Total" & ":" & Space(2) & TGrossAmt
                    'TotalGrossAmtLine = Space(strSubTotal.Length) & strSubTotal & vbCrLf
                    TotalGrossAmtLine = Space(28 - Val(strSubTotal.Length)) & strSubTotal.PadLeft(1) & vbCrLf
                    NetAmtLine = ""
                    If (CDbl(TDiscAmt.Replace("INR", ""))) > 0 Then
                        ''NetAmtLine = getValueByKey("CLSCMP020") & Space(2) & ":" & TNetAmt.PadLeft(BoldLineLength - Val(getValueByKey("CLSCMP020").Length + Space(2).Length))
                        'NetAmtLine = Space(15) & "Total" & ":" & Space(2) & TNetAmt.PadLeft(12 - Val("Total".Length + Space(2).Length)) & vbCrLf
                        Dim strNetAmtLine = "Total" & ":" & Space(2) & TNetAmt & vbCrLf
                        NetAmtLine = Space(30 - Val(strNetAmtLine.Length)) & strNetAmtLine.PadLeft(1) '& vbCrLf
                    Else
                        'NetAmtLine = Space(15) & "Total" & ":" & Space(2) & TNetAmt.PadLeft(12 - Val("Total".Length + Space(2).Length)) & vbCrLf
                        Dim strNetAmtLine = "Total" & ":" & Space(2) & TNetAmt & vbCrLf
                        NetAmtLine = Space(30 - Val(strNetAmtLine.Length)) & strNetAmtLine.PadLeft(1) '& vbCrLf
                    End If
                    '--------------------
                    Dim taxDetailsForBill As DataTable
                    'this code is also use for print format no 6 done by ashma
                    If _TaxDetail = True Then
                        taxDetailsForBill = obj.GetTaxDetailsForBillNo(Sitecode, dtView.Rows(0)("Billno").ToString())
                        If taxDetailsForBill IsNot Nothing AndAlso taxDetailsForBill.Rows.Count > 0 Then
                            FooterTaxLine = ""
                            For Each row As DataRow In taxDetailsForBill.Rows

                                'If CompositeTaxReqOnPrint = False AndAlso IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                                If IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                                Dim taxZeroValueCheck = row("TaxVAlue").ToString()
                                '  If taxZeroValueCheck = "0" Or taxZeroValueCheck = "0.00" Or taxZeroValueCheck = "0.000" Then Continue For
                                Dim taxValue As String = row("TaxVAlue").ToString()
                                Dim taxName As String = row("TaxName").ToString() & ":"
                                taxValue = FormatNumber(CDbl(clsCommon.CheckIfBlank(taxValue)), 2)
                                taxValue = taxValue & " " & Currency
                                'FooterTaxLine = FooterTaxLine & row("TaxName").PadRight(LineLength - 16) & ":" & taxValue.PadLeft(14) & vbCrLf
                                FooterTaxLine = FooterTaxLine & Space(20) & taxName & taxValue.PadLeft(20 - Val(taxName.Length + Space(1).Length)) & vbNewLine
                            Next
                        End If
                    End If
                    TotalFooterTaxDetail = String.Empty
                    TotalFooterTaxDetail = FooterTaxLine
                    'StrCMNo.PadRight(LineLength - 1 - tcmdate.Length) & tcmdate & vbCrLf
                    TotalGrossAmtLine = "<B>" & TotalGrossAmtLine & "</B>"
                    StrBody = StrBody & TotalGrossAmtLine & strLine & vbCrLf
                    If TotalDiscAmtLine IsNot Nothing And TotalDiscAmtLine IsNot String.Empty Then
                        StrBody = StrBody.Trim & vbCrLf & TotalDiscAmtLine.Trim() & vbCrLf & strLine
                    End If
                    If SubTotalLine IsNot Nothing Then
                        StrBody = StrBody & strLine.Substring(30) & SubTotalLine & vbCrLf
                    End If
                    StrBody = StrBody & TotalFooterTaxDetail & strLine & vbCrLf
                    NetAmtLine = "<B>" & NetAmtLine & "</B>"
                    StrBody = StrBody.Trim & vbCrLf & NetAmtLine & strLine
                    TenderLine = "<B>" & getValueByKey("CLSCMP021") & "</B>" & vbCrLf
                    'Dim dtTender As DataTable = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                    ' Dim dtTender As New DataTable
                    If KOTAndBillGeneration = True Then
                        dtTender = obj.GetTenderTypeByRefBillNo(dtView.Rows(0)("BILLNO").ToString())
                        If dtTender.Rows.Count = 0 Then
                            dtTender = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                        End If
                    Else
                        dtTender = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                    End If
                    If IsTendersVisibleInPrintFormat7 = True Then
                        For Each drTender As DataRow In dtTender.Rows
                            If UCase(obj.GetDefaultConfigValue(Sitecode, "CVInfoReqOnPrint")) = "FALSE" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)") Then
                                Continue For
                            End If
                            Dim amounttendered As String = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(drTender("AMOUNTTENDERED").ToString)), RoundOff), 0)
                            Dim tender As String = drTender("CurrencyCode").ToString() & " " & amounttendered & vbCrLf
                            '----------------
                            Dim tenderName As String
                            'Added by Rohit
                            If drTender("CardNO").ToString() <> "" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(I)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(R)") Then
                                tenderName = drTender("TENDERHEADNAME").ToString() & "(" & drTender("CardNO").ToString() & ") "
                            Else
                                tenderName = drTender("TENDERHEADNAME").ToString()
                            End If
                            'End editing
                            If Not drTender("AMOUNTINCURRENCY") Is DBNull.Value AndAlso drTender("AMOUNTINCURRENCY") <> 0 Then '0000404
                                tender = drTender("CurrencyCode").ToString() & " " & FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(drTender("AMOUNTINCURRENCY").ToString())), RoundOff), 0) & vbCrLf
                            End If
                            If drTender("TENDERTYPECODE").ToString() = "CLPPoint" Then
                                tender = FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()), 0)
                            End If
                            'TenderDetails = String.Format("{0}{1}{2}", tenderName.Trim(), Space(LineLength - (tenderName.Trim().Length + tender.Trim().Length)), tender.Trim()) & vbNewLine
                            'TenderLine = TenderLine  & TenderDetails 
                            TenderDetails = String.Format("{0}{1}{2}", tenderName.Trim(), Space(28 - (tenderName.Trim().Length + tender.Trim().Length)), tender.Trim()) & vbNewLine
                            TenderLine = TenderLine & "<B>" & TenderDetails & "</B>"

                        Next
                        StrBody = StrBody & TenderLine.Trim ' & strLine
                    End If
                    If (_IsDisplayTotalSaving) Then
                        'If (Not String.IsNullOrEmpty(TDiscAmt) AndAlso Not String.Equals(TDiscAmt, "0.00")) Then
                        '    Dim totalSavingAmount As String = String.Format(getValueByKey("CLSCMP033"), Currency, TDiscAmt.Replace(Currency, String.Empty).Trim)
                        'Else
                        'End If
                        If (Not String.IsNullOrEmpty(TDiscAmt) AndAlso Not String.Equals(TDiscAmt, "0.00")) Then
                            Dim totalSavingAmount As String = String.Format(getValueByKey("CLSCMP033"), Currency, TDiscAmt.Replace(Currency, String.Empty).Trim)
                            StrBody = StrBody & vbCrLf & SplitString(totalSavingAmount).ToString().Trim & vbCrLf & strLine & vbCrLf
                        Else
                            StrBody = StrBody & vbCrLf
                        End If
                    Else
                        StrBody = StrBody & vbCrLf
                    End If
                End If
                'Company and Tin Info
                Dim NoOfReprints As String = IIf(IsDBNull(dtView.Rows(0)("NoofReprints")), String.Empty, dtView.Rows(0)("NoofReprints")).ToString()
                Dim CompanyName As String = dtView.Rows(0)("SITEOFFICIALNAME") 'obj.GetDefaultConfigValue(Sitecode, "CompanyName")
                'code added by khusrao adil on 17-08-2017 merge from 2.0.1.4
                'Dim strtna = getValueByKey("cashmemoprint.tin") & obj.GetTinNumberForASite(Sitecode)
                'added by khusrao adil on 04-07-2017 for GST No
                Dim GstNo = obj.GetTinNumberForASite(Sitecode)
                If (Not String.IsNullOrEmpty(GstNo)) AndAlso GstNo <> "0" Then
                    Dim strtna = "GST No.: " & GstNo
                    strtna = SplitString(strtna, LineLength).ToString() & vbCrLf
                    If Not String.IsNullOrEmpty(strtna) Then
                        strtna = strtna.Trim() & vbCrLf
                        StrCompanyInfo += strtna '.Trim()
                    End If
                End If
                If (Not String.IsNullOrEmpty(StrTagLine)) Then
                    StrCompanyInfo += StrTagLine.TrimEnd() & vbCrLf
                End If
                'Dim remarks As String = dtView.Rows(0)("Remark")
                If Not IsDBNull(dtView.Rows(0)("Remark")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("Remark")) Then
                    Dim remarks As String = dtView.Rows(0)("Remark")
                    remarks = SplitString(getValueByKey("cashmemoprint.discountremark") & remarks, LineLength).ToString()
                    StrCompanyInfo = StrCompanyInfo & remarks.TrimEnd() & vbCrLf
                End If

                'If Not String.IsNullOrEmpty(NoOfReprints) Then
                '    StrCompanyInfo = StrCompanyInfo & "Re-Print Number : " & NoOfReprints & vbCrLf
                '    StrCompanyInfo = StrCompanyInfo & "Re-Print Date : " & DateTime.Now.ToShortDateString() & vbCrLf
                'End If
                ''Company and Tin Info End
                'Dim dtPromo As DataTable = dvItemDetail.ToTable(True, "PROMOTIONALMSGID", "PROMOMESS")
                'For Each drTerms As DataRow In dtPromo.Select("", "PROMOTIONALMSGID", DataViewRowState.CurrentRows)
                '    If Not String.IsNullOrEmpty(drTerms("PROMOMESS").ToString()) Then
                '        PromoMsgLine = PromoMsgLine & drTerms("PROMOMESS").ToString() & vbCrLf
                '    End If
                'Next

                'TotalPromotionalMsg = PromoMsgLine
                strPrintMsg = New StringBuilder()
                sbDeliveryBillPrint = New StringBuilder()
                If dtView.Rows.Count() > 0 Then
                    '---- Changes By Mahesh : Customer Name is Not Mandatory for Home Delivery 
                    'If dtView.Rows(0)("HDName").ToString() <> String.Empty Then
                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        strHomeDilery = strHomeDilery & "Delivery Date   :" & Space(2) & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf
                        strHomeDilery = strHomeDilery & "Delivery Person :" & Space(2) & DeliveryPersonName & vbCrLf
                    Else
                        'If _CustomerNameRequiredInKOT AndAlso CustomerNameLine <> Nothing Then
                        '    StrCustomerName = CustomerNameLine.ToUpper.Replace("CUSTOMER", "").Trim()

                        'End If
                        'If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                        '    If Not AllowBillingOnlyAfterSelectionOfSalesType Then
                        '        CustomerSaleType = String.Empty
                        '    End If
                        '    Dim StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - CustomerSaleType.Length) & CustomerSaleType & vbCrLf & vbCrLf
                        'End If
                    End If
                End If
                StrFooter = StrFooter & TotalPromotionalMsg '& vbCrLf
                StrFooter = StrFooter & TermsNcond '& vbCrLf
                strPrintMsg.Append(StrHeader)
                '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                sbDeliveryBillPrint.Append(StrHeader)
                'strPrintMsg.Append(strLine)
                If StrSubHeader IsNot Nothing AndAlso Not String.IsNullOrEmpty(StrSubHeader.ToString()) Then
                    strPrintMsg.Append(StrSubHeader)
                    '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                    sbDeliveryBillPrint.Append(StrSubHeader)
                End If
                If (Not String.IsNullOrEmpty(strWelcomeMsg.ToString())) Then
                    strPrintMsg.Append(strLineL40 + vbCrLf)
                    strPrintMsg.Append(strWelcomeMsg.ToString())
                    '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                    sbDeliveryBillPrint.Append(strLineL40 + vbCrLf)
                    sbDeliveryBillPrint.Append(strWelcomeMsg.ToString())
                End If
                '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                sbDeliveryBillPrint.Append(strDblLine)
                strPrintMsg.Append(strDblLine)
                strPrintMsg.Append(LineItemHeading)
                strPrintMsg.Append(strDblLine)
                strPrintMsg.Append(StrBody)
                '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                sbDeliveryBillPrint.Append(LineItemHeading)
                sbDeliveryBillPrint.Append(strDblLine)
                sbDeliveryBillPrint.Append(StrBody)
                If StrCompanyInfo IsNot Nothing AndAlso Not String.IsNullOrEmpty(StrCompanyInfo.ToString()) Then
                    strPrintMsg.Append(strLine)
                    strPrintMsg.Append(StrCompanyInfo)
                    ' strPrintMsg.Append(strLine)

                End If
                If strTaxInformation IsNot Nothing AndAlso Not String.IsNullOrEmpty(strTaxInformation.ToString()) Then
                    strPrintMsg.Append(strTaxInformation.ToString().TrimEnd())
                    strPrintMsg.Append(vbCrLf + strLineL40 + vbCrLf)
                End If
                If strPromotionMsg IsNot Nothing AndAlso Not String.IsNullOrEmpty(strPromotionMsg.ToString()) Then
                    strPrintMsg.Append(strPromotionMsg.ToString().TrimEnd())
                    strPrintMsg.Append(vbCrLf + strLineL40 + vbCrLf)
                End If
                If StrSubFooter IsNot Nothing AndAlso Not String.IsNullOrEmpty(StrSubFooter.ToString()) Then
                    StrSubFooter = StrSubFooter.TrimEnd & vbCrLf
                    strPrintMsg.Append(strLine)

                    strPrintMsg.Append(StrSubFooter.ToString())
                End If
                strPrintMsg.Append(StrFooter)
                If (Not String.IsNullOrEmpty(strHomeDilery)) Then
                    strPrintMsg.Append(strLineL40 & vbCrLf)
                    strPrintMsg.Append(strHomeDilery)
                    ' ---- Added By Mahesh maintaining seperate DeliveryBillPrint
                    '--- Changes By Mahesh Not required in HD Print
                    'sbDeliveryBillPrint.Append(vbCrLf)
                    'sbDeliveryBillPrint.Append(strHomeDilery)
                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        'sbDeliveryBillPrint.Append(strPrintMsg)
                        sbDeliveryBillPrint.Append(strLineL40 & vbCrLf)
                        '--- Changes By Mahesh Not required in HD Print
                        sbDeliveryBillPrint.Append(getValueByKey("CLSCMP035") & DeliveryPersonName & vbCrLf)
                    End If
                End If
                Dim strDeliveryCopy As String = "Delivery Copy"
                Dim WalTotalSpaceD = Val(38 - strDeliveryCopy.Length)
                Dim WalDivideSpaceD = WalTotalSpace / 2
                strDeliveryCopy = Space(WalDivideSpaceD) & strDeliveryCopy & Space(WalDivideSpaceD) & vbCrLf
                'code added for Sbarro and Naturals, both print are same in case of home delivery
                sbDeliveryBillPrint.Remove(0, sbDeliveryBillPrint.Length - 1)
                'sbDeliveryBillPrint.Append(Space(12) & "Delivery Copy" & vbCrLf)
                sbDeliveryBillPrint.Append(strDeliveryCopy)
                sbDeliveryBillPrint.Append(strPrintMsg)
                If GiftMsg <> String.Empty Then
                    GiftMsg = SplitString(GiftMsg).ToString()
                    strPrintMsg.Append(GiftMsg.TrimEnd() & vbCrLf)
                    strPrintMsg.Append(strLine)
                    sbDeliveryBillPrint.Append(GiftMsg.TrimEnd() & vbCrLf & strLine)
                End If
                'code  added for issue id 1530 by vipul
                'Dim reason As String = ""
                'reason = "reason:-" & vbCrLf & dtView.Rows(0)("AuthUserRemarks").ToString & vbCrLf
                'If dtView.Rows(0)("AuthUserRemarks").ToString <> String.Empty Then
                '    strPrintMsg.Append(strLine)
                '    strPrintMsg.Append(SplitString(reason))
                'End If
                PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                If PrinterName = Nothing Then
                    Exit Sub
                End If
                Dim msg As String = String.Empty
                If _PrintPreview = True Then
                    If KOTAndBillGeneration = False Then
                        'If (KOTBillPrintingRequired) Then
                        '    Call CashMemoKOTPrint(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, CustomerComments:=ReminderComments, EvassPizza:=EvasPizzaChanges)
                        'End If
                        If IsCounterCopyKot = False Then
                            If IsKotPrintRequired = True Then
                                Call CashMemoKOTPrintBasedOnPrintFormatNoSSRS(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, dayopenDate, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, ClientName:=ClientName, DayCloseReportPath:=DayCloseReportPath, CustomerComments:=ReminderComments, EvassPizza:=EvasPizzaChanges)
                            End If
                        End If
                    End If
                    PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                    msg = fnPrint(strPrintMsg.ToString(), "PRV")
                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) AndAlso dtView.Rows(0)("BillIntermediateStatus").ToString <> "Deleted" Then
                        msg = fnPrint(sbDeliveryBillPrint.ToString(), "PRV")
                    End If
                Else
                    'If (KOTBillPrintingRequired) Then
                    '    Call CashMemoKOTPrint(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, CustomerComments:=ReminderComments, EvassPizza:=EvasPizzaChanges)
                    'End If
                    If KOTAndBillGeneration = False Then
                        If IsCounterCopyKot = False Then
                            If IsKotPrintRequired = True Then
                                Call CashMemoKOTPrintBasedOnPrintFormatNoSSRS(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, dayopenDate, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, ClientName:=ClientName, DayCloseReportPath:=DayCloseReportPath, CustomerComments:=ReminderComments, EvassPizza:=EvasPizzaChanges)
                            End If
                        End If
                    End If
                    strPrintMsg.Append(vbCrLf & vbCrLf & vbCrLf & vbCrLf)
                    PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                    If IsInvoicePrintFlag = False Then
                        msg = fnPrint(strPrintMsg.ToString(), "PRN", NoOfCopies:=NoOfCopies)
                    Else
                        If IsInvoicePrintRequired Then
                            msg = fnPrint(strPrintMsg.ToString(), "PRN", NoOfCopies:=NoOfCopies)
                        End If
                    End If
                   
                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)
                        ' msg = fnPrint(sbDeliveryBillPrint.ToString(), "PRN")
                        'code added for issue id 1528 by vipul
                        If (Not String.IsNullOrEmpty(DeliveryPersonName)) AndAlso dtView.Rows(0)("BillIntermediateStatus").ToString <> "Deleted" Then

                            msg = fnPrint(sbDeliveryBillPrint.ToString(), "PRN")
                        End If
                    End If
                End If
                If Not String.IsNullOrEmpty(msg) Then
                    errorMsg = msg
                End If
                'Added for fiscal printer
                Try
                    clsFiscalPrinting.fnFiscalPrint(strPrintMsg.ToString())
                Catch ex As Exception
                    Throw ex
                End Try
            Else
                errorMsg = getValueByKey("CLSCMP031")
            End If
        Catch ex As Exception
            errorMsg = ex.Message
        End Try
    End Sub
    Public Sub CashMemoPrintFormat_Naturals(ByVal dayopenDate As Date, ByVal LangCode As String, ByVal Currency As String, ByVal Sitecode As String, ByVal Type As String, ByVal DuplicatePrinting As String, ByVal DeletedUserid As String, ByVal AuthorisedUser As String, ByRef errorMsg As String, Optional ByVal GiftMsg As String = "", Optional ByVal BillAmt As String = "", Optional ByVal PaidAmt As String = "", Optional ByVal IsSavoy As Boolean = False, Optional ByVal KOTAndBillGeneration As Boolean = False, Optional ByVal ClientName As String = "", Optional ByVal EvasPizzaChanges As Boolean = False, Optional ByVal NoOfCopies As Integer = 1, Optional ByVal _tableNo As String = "")
        Try
            Dim strPrintMsg, sbDeliveryBillPrint As StringBuilder
            Dim strLineDetail As New StringBuilder
            Dim StrHeader, StrSubHeader, StrBody, StrCompanyInfo, StrTagLine, StrSubFooter, StrFooter, StrDuplicate, StrDeleteLine, StrCashMemoLine, StrCashierLine, StrSalesPersonLine As String
            Dim StrTokenNo, StrCustomerName As String
            Dim StrCMNo, StrCMDate, StrCMTime, StrCashier, StrSalesPerson As String
            Dim SiteLine, StrSiteTelline, StrAdrressLine, StrSiteReg1Line, StrSiteReg2Line As String
            Dim CLpLine, LineItemHeading, StrLineItem As String
            Dim CustomerNameLine, CLPPointsLine, CustmerPhoneNo As String
            Dim ItemCode, Desc, Qty, Rate, DiscAmt, DiscPer, TaxAmt, NetAmt, RateAfterDisc, GrossAmt As String
            Dim TotalQtyLine, TakeAwayQuantity, TotalGrossAmtLine, TotalDiscAmtLine, SubTotalLine, NetAmtLine, TotalTaxAmtLine As String
            Dim TotalFooterTaxDetail, FooterTaxLine As String
            Dim TenderDetails, TenderLine As String
            Dim TotalPromotionalMsg, PromoMsgLine, StrPrintHeaderLine, StrPrintFooterLine As String
            Dim TermsNcond As String
            Dim strLine As String
            Dim strDblLine As String
            Dim strHomeDilery As String
            Dim isTakeAwayQtyPrint As Boolean = False

            errorMsg = ""
            Dim dtView As New DataTable
            Dim obj As New SpectrumBL.clsCashMemo()
            dtView = obj.GetCashMemo(CashMemoNo, Sitecode, LangCode)

            Dim dvItemDetail As New DataView(dtView, "", "", DataViewRowState.CurrentRows)
            'Dim DtUnique As DataTable = dvItemDetail.ToTable(True, "ArticleCode", "DISCRIPTION", "QUANTITY", "SELLINGPRICE", "GROSSAMT", "TOTALDISCOUNT", "TOTALDISCPERCENTAGE", "NETAMOUNT", "TOTALTAXAMOUNT")
            Dim DtUnique As DataTable = obj.GetBillDetailsDataForPrint(CashMemoNo, Sitecode, LangCode)
            Dim Dtcombo As DataTable = obj.GetComboDetailsDataForPrint(CashMemoNo, Sitecode, LangCode)

            '----- Code Added By Mahesh - Delivery Person and customer Sales Type value pick up from Database 
            If Not IsDBNull(dtView.Rows(0)("ServiceTaxAmount")) AndAlso Val(dtView.Rows(0)("ServiceTaxAmount")) > 0 Then
                CustomerSaleType = GetCustomerSaleType(dtView.Rows(0)("ServiceTaxAmount"))
            Else
                CustomerSaleType = ""
            End If
            If Not IsDBNull(dtView.Rows(0)("DeliveryPersonID")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("DeliveryPersonID")) Then
                DeliveryPersonName = dtView.Rows(0)("DeliveryPersonID").ToString()
            End If

            If DtUnique Is Nothing Then Exit Sub
            Dim TotaltakeAwayQty = DtUnique.Compute("SUM(TakeAwayQuantity)", "").ToString()
            If Val(TotaltakeAwayQty) > 0 Then
                isTakeAwayQtyPrint = True
            End If

            Dim StrSubHeader1, StrSubFooter1, strWelcomeMsg, strTaxInformation, strPromotionMsg As New StringBuilder
            PrinttHeaderAndFooter(StrSubHeader1, StrSubFooter1, strWelcomeMsg, strPromotionMsg, strTaxInformation, "CMInvc", _printType)

            If StrSubFooter1 IsNot Nothing AndAlso Not String.IsNullOrEmpty(StrSubFooter1.ToString()) Then
                StrTagLine = StrSubFooter1.ToString()
            End If

            If Not dtView Is Nothing Then
                strLine = "-".PadRight(LineLength, "-") & vbCrLf
                strDblLine = "=".PadRight(LineLength, "=") & vbCrLf
                If DuplicatePrinting <> String.Empty Then
                    StrDuplicate = DuplicatePrinting
                End If
                'StrDeleteLine = "Cash Memo Status: Deleted"

                'Rakesh-12.09.2013:Issue-7750
                If Not StrSubHeader1 Is Nothing AndAlso StrSubHeader1.ToString().Length > 0 Then
                    'If _printType <> "L40" Then
                    StrSubHeader = strLine & SplitString(StrSubHeader1.ToString(), LineLength).ToString().Trim & vbCrLf
                    'End If
                    ' StrSubHeader = strLine & SplitString(StrSubHeader1.ToString(), LineLength).ToString().Trim & vbCrLf
                End If

                If DuplicatePrinting <> String.Empty Then
                    StrHeader = "<B>" & StrDuplicate.PadLeft(((LineLength - StrDuplicate.Length) / 2) + StrDuplicate.Length, " ") & "</B>" & vbCrLf
                End If

                '---Put all code in DCM by Mahesh for performance Optimize ...
                If Type = "DCM" Then
                    StrDeleteLine = getValueByKey("CLSCMP001")
                    StrHeader = StrDeleteLine & vbCrLf
                    'Request ID: <Update / Delete Request ID>
                    'Authorized By : <User Name >       Deleted at Time: <CM Time>
                    'StrHeader = StrHeader & "Request ID    :" & DeletedUserid & vbCrLf
                    'StrHeader = StrHeader & "Authorized By :" & AuthorisedUser & vbCrLf
                    StrHeader = StrHeader & getValueByKey("CLSCMP002") & DeletedUserid & vbCrLf
                    StrHeader = StrHeader & getValueByKey("CLSCMP003") & AuthorisedUser & vbCrLf
                End If

                Dim SiteName, Site As String
                Dim TS_SiteName As StringBuilder
                'SiteName = "Store Name: " & dtView.Rows(0)("SITEOFFICIALNAME").ToString()
                'Site = "Code: " & dtView.Rows(0)("SITECODE").ToString()
                'StrSiteTelline = "Tel: " & dtView.Rows(0)("TELNO").ToString()
                'StrAdrressLine = "Add: " & dtView.Rows(0)("ADDRESS").ToString()

                SiteName = getValueByKey("CLSCMP004") & " " & dtView.Rows(0)("SITEOFFICIALNAME").ToString()
                SiteName = "<B>" & dtView.Rows(0)("SITEOFFICIALNAME").ToString() & "</B>"
                ' SiteName = dtView.Rows(0)("SITEOFFICIALNAME").ToString()
                'TS_SiteName = SplitString(SiteName) 'commented by vipin Discuss with Santosh (Previously added to print only site name)
                ' SiteName = TS_SiteName.ToString 
                SiteName = SiteName.PadLeft(LineLength / 2 + (SiteName.Length / 2))
                Site = vbCrLf & getValueByKey("CLSCMP005") & " " & dtView.Rows(0)("SITECODE").ToString()
                'SiteLine = SiteName & Site
                If ClientName <> "" Then
                    Dim arrClientname = ClientName.Split(" ").ToArray()

                    For i = 0 To arrClientname.Length - 1
                        If SiteName.ToUpper.Contains(arrClientname(i).ToString.ToUpper) Then
                            SiteName = SiteName.ToUpper.Replace(arrClientname(i).ToString.ToUpper.Trim, "").Trim
                        End If
                    Next
                End If

                SiteName = SiteName.PadLeft((Val(LineLength - SiteName.Length) + SiteName.Length) / 2)
                '------------------
                Site = vbCrLf & getValueByKey("CLSCMP005") & " " & dtView.Rows(0)("SITECODE").ToString()
                'SiteLine = SiteName & Site
                'SiteLine = SiteName.Trim.PadLeft((Val(45 - SiteName.Length) + SiteName.Length) / 2)
                SiteLine = SiteName


                ' SiteLine = SiteName
                'SiteLine = SiteName.PadRight(LineLength - Site.Length) & Site

                StrSiteTelline = getValueByKey("CLSCMP006") & " " & dtView.Rows(0)("TELNO").ToString()
                StrSiteTelline = StrSiteTelline.PadLeft(LineLength / 2 + StrSiteTelline.Length / 2)
                StrAdrressLine = getValueByKey("CLSCMP007") & " " & dtView.Rows(0)("ADDRESS").ToString().Trim
                StrAdrressLine = SplitStringPC(StrAdrressLine, LineLength, NoMoreSpace:=True).ToString() 'SplitString(StrAdrressLine, LineLength).ToString()
                'StrSiteReg1Line = "LST NO: " & dtView.Rows(0)("LOCALSALESTAXNO").ToString() & Space(2) & "LST Date: " & dtView.Rows(0)("LOCALSALESTAXDATE").ToString()
                'StrSiteReg2Line = "CST NO: " & dtView.Rows(0)("CENTRALSALESTAXNO").ToString() & Space(2) & "CST Date: " & dtView.Rows(0)("CENTRALSALESTAXDATE").ToString()
                StrAdrressLine = StrAdrressLine.Replace("Add.:", "")
                StrHeader &= SiteLine & vbCrLf
                StrHeader = StrHeader & StrAdrressLine.TrimEnd & vbCrLf
                StrHeader = StrHeader & StrSiteTelline & vbCrLf
                'StrHeader = StrHeader & strLine & vbCrLf
                'StrSubHeader = StrSubHeader & StrSiteReg1Line & vbCrLf
                'StrSubHeader = StrSubHeader & StrSiteReg2Line & vbCrLf

                'Dim kotDataLine As String = String.Empty
                '****Added by Rahul 30 nov 2009 night(Rashid) . start 
                If Type = "DCM" Then
                    'StrCMNo = "Void Cash Memo:" & dtView.Rows(0)("Billno").ToString()
                    StrCMNo = getValueByKey("CLSCMP008") & dtView.Rows(0)("Billno").ToString()
                Else
                    'StrCMNo = "Cash Memo:" & dtView.Rows(0)("Billno").ToString()
                    StrCMNo = getValueByKey("CLSCMP009") & dtView.Rows(0)("Billno").ToString()
                End If
                '******End 

                'StrCMDate = "Date:" & Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())
                'StrCMDate = Format(dtView.Rows(0)("BillTime"), clsCommon.GetSystemDateFormat())
                StrCMDate = dayopenDate.ToShortDateString()
                strDayOpenDate = dayopenDate.ToShortDateString()
                If DuplicatePrinting <> String.Empty Then
                    StrCMDate = Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())
                End If
                If PrintCouponNumberForKOT = True Then
                    'Dim CouponNo As String = strTillno.Substring(0, 1) & strTillno.Substring(strTillno.Length - 2, 2) & strBillno.Substring(strBillno.Length - 2, 2)
                    Dim tillNo As String = dtView.Rows(0)("TerminalId").ToString()
                    Dim billno As String = dtView.Rows(0)("Billno").ToString()
                    Dim CouponNo As String = tillNo.Substring(0, 1) & tillNo.Substring(tillNo.Length - 2, 2) & billno.Substring(billno.Length - 2, 2)
                    StrTokenNo = CouponNo
                Else
                    StrTokenNo = dtView.Rows(0)("Billno").ToString().Trim.Substring(dtView.Rows(0)("Billno").ToString().Trim.Length - 2, 2)
                End If

                StrTokenNo = "<B>" & StrTokenNo & "</B>"
                ' StrTokenNo = "" & StrTokenNo & ""

                StrCashMemoLine = StrCMNo.PadRight(LineLength - 1 - StrCMDate.Length) & StrCMDate & vbCrLf
                'StrCashier = "Cashier:" & dtView.Rows(0)("Createdby").ToString()
                'StrCMTime = "Time:" & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")

                'StrCashier = getValueByKey("CLSPV006") & dtView.Rows(0)("Createdby").ToString()
                StrCashier = getValueByKey("CLSPV006") & LoginUserName
                StrCMTime = getValueByKey("CLSCMP032") & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")
                StrCashierLine = StrCashier.PadRight(LineLength - 1 - StrCMTime.Length) & StrCMTime & vbCrLf
                'StrSalesPerson = "Sales Person:" & dtView.Rows(0)("SALESEXECUTIVECODE").ToString()
                StrSalesPerson = getValueByKey("CLSCMP010") & dtView.Rows(0)("SALESEXECUTIVECODE").ToString()

                If _SalesPerApplicable = True Then
                    StrSalesPersonLine = StrSalesPerson & vbCrLf
                End If
                Dim _strTableNo As String = "Table No. " & _tableNo 'added by khusrao adil on 01-08-2018 for sharda restaurent
                Dim _tempCustomerSaleType As String = "<B>" & CustomerSaleType & "</B>"
                If (dtView.Rows.Count() > 0 And String.IsNullOrEmpty(DeliveryPersonName)) Then
                    If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                        ' strPrintMsg.Append(StrTokenNo)
                        StrTokenNo = "Token No. " & StrTokenNo
                        If Not AllowBillingOnlyAfterSelectionOfSalesType Then
                            '   strPrintMsg.Append(CustomerSaleType)
                            CustomerSaleType = String.Empty
                            _tempCustomerSaleType = String.Empty
                        End If
                        Dim StrTokenSalesTypeLine = ""
                        If TableNoRequiredInDineInModuleBillPrint AndAlso _tableNo <> "0" Then  'added by khusrao adil on 01-08-2018 for sharda restaurent
                            StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 5 - (_tempCustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "")).Length) & _tempCustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "")
                            ' StrTokenSalesTypeLine = StrTokenSalesTypeLine.TrimEnd & _strTableNo.PadLeft(StrTokenSalesTypeLine.Length - 10 - _strTableNo.Length) & vbCrLf
                            StrTokenSalesTypeLine = StrTokenSalesTypeLine.TrimEnd & _strTableNo.PadLeft(StrTokenSalesTypeLine.Length - _strTableNo.Length - 10) & vbCrLf
                        Else
                            StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - (_tempCustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "")).Length) & _tempCustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "") & vbCrLf
                        End If
                        StrSubHeader = StrSubHeader & strLine
                        StrSubHeader = StrSubHeader & StrTokenSalesTypeLine
                    Else
                        If AllowBillingOnlyAfterSelectionOfSalesType Then
                            StrSubHeader = StrSubHeader & _tempCustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "") & vbCrLf
                        Else
                            If TableNoRequiredInDineInModuleBillPrint AndAlso _tableNo <> "0" Then 'added by khusrao adil on 01-08-2018 for sharda restaurent
                                StrSubHeader = _tempCustomerSaleType + _strTableNo.PadLeft(LineLength - _strTableNo.Length - _tempCustomerSaleType.Length) + vbCrLf   ''StrSubHeader &
                            Else
                                StrSubHeader = StrSubHeader & _tempCustomerSaleType + vbCrLf
                            End If
                        End If
                    End If
                Else
                    If AllowBillingOnlyAfterSelectionOfSalesType Then
                        StrSubHeader = StrSubHeader & _tempCustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "") + vbCrLf
                    Else
                        StrSubHeader = StrSubHeader & _tempCustomerSaleType + vbCrLf
                    End If
                End If
                StrSubHeader = StrSubHeader & strLine
                StrSubHeader = StrSubHeader & StrCashMemoLine
                StrSubHeader = StrSubHeader & StrCashierLine

                If (Not String.IsNullOrEmpty(dtView.Rows(0)("SALESEXECUTIVECODE").ToString())) Then
                    StrSubHeader = StrSubHeader & StrSalesPersonLine
                    'StrSubHeader = StrSubHeader & strLine
                End If

                Dim CLPaddress As String = ""
                Dim ReminderComments As String = ""
                Dim CLPBalancePoints As String
                If dtView.Rows(0)("CLPNO").ToString() <> String.Empty Then
                    'CLpLine = "CLP Card No:" & dtView.Rows(0)("CLPNO").ToString()
                    'CustomerNameLine = "Customer Name: " & dtView.Rows(0)("CLPNAME").ToString()
                    'CLPPointsLine = "CLP Points Earned: " & FormatNumber(dtView.Rows(0)("CLPPoints").ToString())

                    CLpLine = getValueByKey("CLSCMP011") & dtView.Rows(0)("CLPNO").ToString()

                    CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CLPNAME").ToString()
                    CLPPointsLine = getValueByKey("CLSCMP013") & FormatNumber(Val(dtView.Rows(0)("CLPPoints").ToString()))

                    If (dtView.Rows(0)("RedemptionPoints") IsNot DBNull.Value AndAlso dtView.Rows(0)("RedemptionPoints") > 0) Then
                        CLPPointsLine += vbCrLf + getValueByKey("CLSCMP034") & FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()))
                    End If

                    '   CLPaddress = dtView.Rows(0)("CLPAddress").ToString()
                    ' If CLPaddress <> String.Empty Then
                    '  CLPaddress = getValueByKey("CLSCMP014") & dtView.Rows(0)("CLPAddress").ToString()
                    'End If
                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then 'vipul
                        CLPaddress = dtView.Rows(0)("HDAddress").ToString()
                    Else
                        CLPaddress = dtView.Rows(0)("CLPAddress").ToString()
                    End If
                    If CLPaddress <> String.Empty Then
                        If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                            CLPaddress = getValueByKey("CLSCMP014") & dtView.Rows(0)("HDAddress").ToString()
                        Else
                            CLPaddress = getValueByKey("CLSCMP014") & dtView.Rows(0)("CLPAddress").ToString()
                        End If
                    End If
                    ReminderComments = dtView.Rows(0)("ReminderComments").ToString()
                    CLPBalancePoints = String.Empty
                    CLPBalancePoints = getValueByKey("CLSCMP015")
                    Dim dblCLPPoints As Double

                    dblCLPPoints = Val(dtView.Rows(0)("BalancePoints").ToString())
                    'For Each drRow As DataRow In dtView.Select("Tendertypecode = 'CLPPoint'")
                    '    dblCLPPoints -= drRow("RedemptionPoints")
                    'Next

                    Dim RedemptionPoints As DataRow = dtView.Select("Tendertypecode = 'CLPPoint'").FirstOrDefault()
                    If (RedemptionPoints IsNot Nothing AndAlso RedemptionPoints("RedemptionPoints") IsNot DBNull.Value) Then
                        dblCLPPoints -= Val(RedemptionPoints("RedemptionPoints").ToString())
                    End If

                    CLPBalancePoints = CLPBalancePoints & FormatNumber(dblCLPPoints.ToString())
                    If CLPaddress <> String.Empty Then

                        CLPaddress = SplitStringPC(CLPaddress, LineLength, NoMoreSpace:=True).ToString()
                    End If
                    'If CLpLine <> Nothing AndAlso CLpLine.ToString().Length > 0 Then ' 3 lines commented for clp details
                    '    StrSubFooter = StrSubFooter & CLpLine & vbCrLf
                    'End If
                    CustmerPhoneNo = getValueByKey("RP187") & " " & dtView.Rows(0)("Mobileno").ToString()
                    StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
                    'StrSubFooter = StrSubFooter & CLPBalancePoints & vbCrLf ''' 2 lines commented for clp details
                    If CustomerSaleType = "Home Delivery" Then
                        StrSubFooter = StrSubFooter & CustmerPhoneNo & vbCrLf
                    End If

                    'StrSubFooter = StrSubFooter & CustmerPhoneNo & vbCrLf
                    'StrSubFooter = StrSubFooter & CLPPointsLine & vbCrLf

                    Dim retAmt As Double
                    If CDbl(PaidAmt) >= CDbl(BillAmt) Then
                        retAmt = CDbl(PaidAmt) - CDbl(BillAmt)
                    End If
                    StrSubFooter = StrSubFooter & CLPaddress & vbCrLf
                    If EvasPizzaChanges = True AndAlso Not String.IsNullOrEmpty(ReminderComments) Then
                        StrSubFooter = StrSubFooter & SplitStringPC("Cust. Instructions: " & ReminderComments, LineLength, NoMoreSpace:=True).ToString() & vbCrLf
                    End If
                    If (dtView.Rows.Count() > 0 And String.IsNullOrEmpty(DeliveryPersonName)) Then
                        If Val(PaidAmt) > 0 Then
                            StrSubFooter = StrSubFooter & getValueByKey("CLIST04") & " " & FormatNumber(CDbl(PaidAmt), 2) & vbCrLf
                            StrSubFooter = StrSubFooter & getValueByKey("CLIST05") & " " & FormatNumber(CDbl(retAmt), 2) & vbCrLf
                        End If
                    End If

                ElseIf dtView.Columns.Contains("CustomerNo") AndAlso dtView.Rows(0)("CustomerNo").ToString() <> String.Empty Then

                    CLpLine = getValueByKey("CLSCMP038") & dtView.Rows(0)("CustomerNo").ToString()

                    CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CustomerName").ToString()
                    If CLpLine <> Nothing AndAlso CLpLine.ToString().Length > 0 Then
                        StrSubFooter = StrSubFooter & CLpLine & vbCrLf
                    End If
                    StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
                End If

                If Type = "CMWAmt" Then
                    'LineItemHeading = "Item Code" & Space(17) & "Item Name" & Space(1) & "Qty " & vbCrLf
                    ' LineItemHeading = getValueByKey("CLSPSO020")  & vbCrLf & getValueByKey("CLSCMP016") & Space(1) & getValueByKey("CLSPSO022") & " " & vbCrLf
                    'to resolve Issue 448 
                    LineItemHeading = getValueByKey("CLSPSO020") & Space(17) & vbCrLf & getValueByKey("CLSCMP016") & Space(20) & getValueByKey("CLSPSO022") & " " & vbCrLf
                Else
                    'LineItemHeading = "Item Code" & Space(17) & "Item Name" & Space(5) & vbCrLf
                    'to resolve Issue 448 
                    'LineItemHeading = "  Item Code" & vbCrLf & "  Item Name" & vbCrLf
                    'LineItemHeading = LineItemHeading & Space(3) & "Qty " & Space(2) & "Price" & Space(4) & "Disc." & Space(4) & "Tax" & Space(5) & "Net" & vbCrLf

                    'LineItemHeading = "  " & getValueByKey("CLSPSO020") & vbCrLf & "  " & getValueByKey("CLSCMP016") & vbCrLf
                    'LineItemHeading = LineItemHeading & Space(3) & getValueByKey("CLSPSO022") & " " & Space(2) & getValueByKey("CLSPSO023") & Space(4) & getValueByKey("CLSPSO024") & Space(4) & getValueByKey("CLSPSO025") & Space(5) & getValueByKey("CLSPSO026") & vbCrLf
                    LineItemHeading = getValueByKey("CLSCMP016") & Space(13) & getValueByKey("CLSPSO022") & " " & Space(5)

                    LineItemHeading &= "Amt" & vbCrLf

                End If


                Dim TotalQty, TGrossAmt, TNetAmt, TDiscAmt, TRateAfterDisc, TTaxtAmt As String
                Dim BillLineNO As String

                For Each dr As DataRow In DtUnique.Rows
                    ItemCode = dr("ArticleCode").ToString()
                    If (DisplayArticleFullName) Then
                        Desc = dr("ArticleFullName").ToString()
                    Else
                        Desc = dr("DISCRIPTION").ToString()
                    End If
                    BillLineNO = dr("BillLineNO").ToString()
                    If SpectrumCommon.ComboItemPrintingAllowed AndAlso Dtcombo IsNot Nothing AndAlso Dtcombo.Rows.Count > 0 Then
                        Dim drp() = Dtcombo.Select("ComboArticleCode='" & ItemCode & "' AND BillLineNO ='" & BillLineNO & "'")
                        If drp.Length > 0 Then
                            Dim articleDescriptionDictionary As New Dictionary(Of String, Integer)
                            For index = 0 To drp.Length - 1
                                If articleDescriptionDictionary.ContainsKey(drp(index)("ArticleName")) Then
                                    articleDescriptionDictionary(drp(index)("ArticleName")) = articleDescriptionDictionary(drp(index)("ArticleName")) + drp(index)("Quantity")
                                Else
                                    articleDescriptionDictionary.Add(drp(index)("ArticleName"), drp(index)("Quantity"))
                                End If
                            Next
                            Dim AddedDesc = GetMultilinedString(articleDescriptionDictionary)
                            Desc = Desc & vbCrLf & AddedDesc
                        End If
                    End If


                    Qty = dr("Quantity").ToString()
                    Rate = dr("SellingPrice").ToString()
                    RateAfterDisc = (dr("SellingPrice") - dr("TOTALDISCOUNT")).ToString()
                    If CDbl(clsCommon.CheckIfBlank(Rate)) < 0 Then
                        Rate = CDbl(clsCommon.CheckIfBlank(Rate)) * -1
                    End If
                    DiscAmt = dr("TOTALDISCOUNT").ToString()
                    DiscPer = dr("TOTALDISCPERCENTAGE").ToString()
                    NetAmt = dr("NetAmount").ToString()
                    GrossAmt = dr("GROSSAMT").ToString()
                    TaxAmt = dr("TOTALTAXAMOUNT").ToString()
                    'Dim i As Integer = 0
                    'For i = ItemCode.Length To 26
                    '    ItemCode = ItemCode & " "
                    'Next
                    'For i = Desc.Length To 14
                    '    Desc = Desc & " "
                    'Next

                    If (AllowDecimalQty) Then
                        If (WeightScaleEnabled) Then
                            Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 3)
                        Else
                            Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 2)
                        End If
                    Else
                        Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 0)
                    End If

                    'For i = Qty.Length To 4
                    '    Qty = " " & Qty
                    'Next
                    Rate = FormatNumber(CDbl(clsCommon.CheckIfBlank(Rate)), 2)

                    RateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(RateAfterDisc)), 2)
                    'Rate = FormatCurrency(Rate, 2)
                    'For i = Rate.Length To 8
                    '    Rate = " " & Rate
                    'Next
                    DiscAmt = FormatNumber(CDbl(IIf(DiscAmt <> String.Empty, DiscAmt, 0)), 2)
                    'For i = DiscAmt.Length To 8
                    '    DiscAmt = " " & DiscAmt
                    'Next
                    DiscPer = FormatNumber(CDbl(IIf(DiscPer <> String.Empty, DiscPer, 0)), 2)
                    DiscPer = DiscPer & "%"
                    'For i = DiscPer.Length To 8
                    '    DiscPer = " " & DiscPer
                    'Next
                    NetAmt = FormatNumber(CDbl(NetAmt), 2)
                    GrossAmt = FormatNumber(CDbl(GrossAmt), 2)
                    'For i = NetAmt.Length To 8
                    '    NetAmt = " " & NetAmt
                    'Next
                    TaxAmt = FormatNumber(CDbl(IIf(TaxAmt <> String.Empty, TaxAmt, 0)), 2)
                    'For i = TaxAmt.Length To 7
                    '    TaxAmt = " " & TaxAmt
                    'Next

                    Dim TempNetAmt As String = "0"

                    If Type = "CMWAmt" Then
                        StrLineItem = ItemCode.PadRight(26) & vbCrLf & Desc.PadRight(35) & Qty & vbCrLf
                    Else
                        If 28 - Desc.Length < Qty.Length Then
                            StrLineItem = Desc & vbCrLf
                            StrLineItem = StrLineItem & Qty.PadLeft(28) & GrossAmt.PadLeft(11) & vbCrLf
                        Else
                            StrLineItem = Desc & Qty.PadLeft(28 - Desc.Length) & GrossAmt.PadLeft(11) & vbCrLf
                        End If
                    End If
                    strLineDetail.Append(StrLineItem)

                    If CDbl(Qty.Trim) > 0 Then
                        TotalQty = CDbl(clsCommon.CheckIfBlank(TotalQty)) + CDbl(Qty.Trim)
                    End If
                    TDiscAmt = CDbl(clsCommon.CheckIfBlank(TDiscAmt)) + CDbl(DiscAmt.Trim)
                    TGrossAmt = CDbl(clsCommon.CheckIfBlank(TGrossAmt)) + CDbl(dr("GROSSAMT").ToString())
                    TNetAmt = CDbl(clsCommon.CheckIfBlank(TNetAmt)) + CDbl(NetAmt.Trim)
                    TRateAfterDisc = CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)) + CDbl(RateAfterDisc.Trim)
                    TTaxtAmt = CDbl(clsCommon.CheckIfBlank(TTaxtAmt)) + CDbl(TaxAmt.Trim)
                Next

                TotalQty = FormatNumber(CDbl(clsCommon.CheckIfBlank(TotalQty)), 2)
                TDiscAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TDiscAmt)), 2)
                TGrossAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TGrossAmt)), 2)
                TNetAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TNetAmt)), 2)
                TRateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)), 2)
                TTaxtAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TTaxtAmt)), 2)
                StrBody = strLineDetail.ToString() & vbCrLf
                StrBody = StrBody & strLine

                'TotalQtyLine = "Total Qty" & Space(3) & ":" & TotalQty
                'TotalQtyLine = getValueByKey("CLSCMP017") & Space(3) & ":" & TotalQty
                'StrBody = StrBody & TotalQtyLine & vbCrLf
                Dim TSubTotalAmt As String
                If Type <> "CMWAmt" Then
                    If CDbl(clsCommon.CheckIfBlank(TDiscAmt)) > 0 Then
                        Dim TotalDisPercent As String = Math.Round((CDbl(clsCommon.CheckIfBlank(TDiscAmt)) / CDbl(clsCommon.CheckIfBlank(TGrossAmt))) * 100, 1).ToString()
                        TSubTotalAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TGrossAmt)) - CDbl(clsCommon.CheckIfBlank(TDiscAmt)))
                        TSubTotalAmt = Currency & " " & TSubTotalAmt
                        SubTotalLine = "Sub Total".PadRight(LineLength - 16) & ":" & TSubTotalAmt.PadLeft(14)
                        TDiscAmt = Currency & " " & TDiscAmt
                        TotalDiscAmtLine = String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).PadRight(LineLength - 16) & ":" & TDiscAmt.PadLeft(14)
                    End If
                    TGrossAmt = Currency & " " & TGrossAmt
                    TNetAmt = Currency & " " & TNetAmt
                    TRateAfterDisc = Currency & " " & TRateAfterDisc
                    TTaxtAmt = Currency & " " & TTaxtAmt

                    TotalGrossAmtLine = "Total Amt".PadRight(LineLength - 30) & ":" & TGrossAmt.PadLeft(14)

                    NetAmtLine = "<B>" & getValueByKey("CLSCMP020").PadRight(LineLength - 30) & ":" & TNetAmt.PadLeft(14) & "</B>"

                    If _TaxDetail = True Then
                        Dim taxDetailsForBill As DataTable = obj.GetTaxDetailsForBillNo(Sitecode, dtView.Rows(0)("Billno").ToString())
                        If taxDetailsForBill IsNot Nothing AndAlso taxDetailsForBill.Rows.Count > 0 Then
                            For Each row As DataRow In taxDetailsForBill.Rows
                                'If CompositeTaxReqOnPrint = False AndAlso IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                                If IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                                Dim taxValue As String = row("TaxVAlue").ToString()
                                taxValue = FormatNumber(CDbl(clsCommon.CheckIfBlank(taxValue)), 2)
                                taxValue = Currency & " " & taxValue
                                FooterTaxLine = FooterTaxLine & row("TaxName").PadRight(LineLength - 16) & ":" & taxValue.PadLeft(14) & vbCrLf
                            Next
                        End If
                    End If
                    TotalFooterTaxDetail = FooterTaxLine
                    StrBody = StrBody & TotalGrossAmtLine & vbCrLf
                    If TotalDiscAmtLine IsNot Nothing Then
                        StrBody = StrBody & TotalDiscAmtLine & vbCrLf
                    End If
                    If SubTotalLine IsNot Nothing Then
                        StrBody = StrBody & strLine.Substring(30) & SubTotalLine & vbCrLf
                    End If

                    StrBody = StrBody & TotalFooterTaxDetail & strLine.Substring(30) & NetAmtLine & vbCrLf & strLine

                    TenderLine = getValueByKey("CLSCMP021") & vbCrLf
                    'Dim dtTender As DataTable = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                    Dim dtTender As New DataTable
                    If KOTAndBillGeneration = True Then
                        dtTender = obj.GetTenderTypeByRefBillNo(dtView.Rows(0)("BILLNO").ToString())
                    Else
                        dtTender = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                    End If
                    For Each drTender As DataRow In dtTender.Rows
                        If UCase(obj.GetDefaultConfigValue(Sitecode, "CVInfoReqOnPrint")) = "FALSE" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)") Then
                            Continue For
                        End If
                        Dim tender As String = FormatNumber(CDbl(clsCommon.CheckIfBlank(drTender("AMOUNTTENDERED").ToString()))) & " " & drTender("CurrencyCode").ToString() & vbCrLf
                        Dim tenderName As String
                        'Added by Rohit
                        If drTender("CardNO").ToString() <> "" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(I)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(R)") Then
                            tenderName = drTender("TENDERHEADNAME").ToString() & "(" & drTender("CardNO").ToString() & ") "
                        Else
                            tenderName = drTender("TENDERHEADNAME").ToString()
                        End If
                        'End editing
                        If Not drTender("AMOUNTINCURRENCY") Is DBNull.Value AndAlso drTender("AMOUNTINCURRENCY") <> 0 Then '0000404
                            tender = drTender("CurrencyCode").ToString() & " " & FormatNumber(CDbl(drTender("AMOUNTINCURRENCY").ToString()), 2) & vbCrLf
                        Else                                                                                               'Code is added by irfan for Mantis issue on 31/01/2018
                            drTender("AMOUNTINCURRENCY") = drTender("AMOUNTTENDERED")
                            tender = drTender("CurrencyCode").ToString() & " " & FormatNumber(CDbl(drTender("AMOUNTINCURRENCY").ToString()), 2) & vbCrLf
                        End If
                        'tender = Format(CDbl(tender), "0.00")

                        'TenderDetails = tenderName.PadRight(LineLength - 19) & tender.PadLeft(20) 
                        '--- Code Added By Mahesh changes by on MOD clp changes
                        If drTender("TENDERTYPECODE").ToString() = "CLPPoint" Then
                            tender = FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()))
                        End If

                        TenderDetails = String.Format("{0}{1}{2}", tenderName.Trim(), Space(LineLength - (tenderName.Trim().Length + tender.Trim().Length)), tender.Trim()) & vbCrLf
                        TenderLine = TenderLine & TenderDetails
                    Next
                    StrBody = StrBody & TenderLine

                    'Rakesh-13.09.2013:KSL CR-7860--> Display total discount amount 
                    If (_IsDisplayTotalSaving) Then
                        If (Not String.IsNullOrEmpty(TDiscAmt) AndAlso Not String.Equals(TDiscAmt, "0.00")) Then
                            Dim totalSavingAmount As String = String.Format(getValueByKey("CLSCMP033"), Currency, TDiscAmt.Replace(Currency, String.Empty).Trim)
                            StrBody = StrBody & vbCrLf & SplitString(totalSavingAmount).ToString().Trim & vbCrLf & strLine & vbCrLf
                        Else
                            StrBody = StrBody & vbCrLf
                        End If
                    Else
                        StrBody = StrBody & vbCrLf
                    End If

                End If

                'Company and Tin Info
                Dim NoOfReprints As String = IIf(IsDBNull(dtView.Rows(0)("NoofReprints")), String.Empty, dtView.Rows(0)("NoofReprints")).ToString()
                Dim CompanyName As String = dtView.Rows(0)("SITEOFFICIALNAME") 'obj.GetDefaultConfigValue(Sitecode, "CompanyName")

                StrCompanyInfo = String.Empty
                If Not String.IsNullOrEmpty(CompanyName) Then
                    'StrCompanyInfo = CompanyName & vbCrLf
                End If

                'Dim strtna = getValueByKey("cashmemoprint.tin") & obj.GetTinNumberForASite(Sitecode)
                'strtna = SplitString(strtna, LineLength).ToString() & vbCrLf

                'If Not String.IsNullOrEmpty(strtna) Then
                '    StrCompanyInfo += strtna.TrimEnd() & vbCrLf
                'End If
                'Dim strtna = getValueByKey("cashmemoprint.tin") & obj.GetTinNumberForASite(Sitecode)
                'added by khusrao adil on 04-07-2017 for GST No
                Dim GstNo = obj.GetTinNumberForASite(Sitecode)
                If (Not String.IsNullOrEmpty(GstNo)) AndAlso GstNo <> "0" Then
                    Dim strtna = "GST No.: " & GstNo
                    strtna = SplitString(strtna, LineLength).ToString() & vbCrLf
                    If Not String.IsNullOrEmpty(strtna) Then
                        StrCompanyInfo += strtna.TrimEnd() & vbCrLf
                    End If
                End If

                If (Not String.IsNullOrEmpty(StrTagLine)) Then
                    StrCompanyInfo += StrTagLine.TrimEnd() & vbCrLf
                End If

                'Dim remarks As String = dtView.Rows(0)("Remark")
                If Not IsDBNull(dtView.Rows(0)("Remark")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("Remark")) Then
                    Dim remarks As String = dtView.Rows(0)("Remark")
                    remarks = SplitString(getValueByKey("cashmemoprint.discountremark") & remarks, LineLength).ToString()
                    StrCompanyInfo = StrCompanyInfo & remarks.TrimEnd() & vbCrLf
                End If

                If Not String.IsNullOrEmpty(NoOfReprints) Then
                    StrCompanyInfo = StrCompanyInfo & "Re-Print Number : " & NoOfReprints & vbCrLf
                    StrCompanyInfo = StrCompanyInfo & "Re-Print Date : " & DateTime.Now.ToShortDateString() & vbCrLf
                End If
                'Company and Tin Info End

                Dim dtPromo As DataTable = dvItemDetail.ToTable(True, "PROMOTIONALMSGID", "PROMOMESS")
                For Each drTerms As DataRow In dtPromo.Select("", "PROMOTIONALMSGID", DataViewRowState.CurrentRows)
                    If Not String.IsNullOrEmpty(drTerms("PROMOMESS").ToString()) Then
                        PromoMsgLine = PromoMsgLine & drTerms("PROMOMESS").ToString() & vbCrLf
                    End If
                Next

                TotalPromotionalMsg = PromoMsgLine

                If Not IsSavoy Then
                    Dim dtTerms As DataTable = dvItemDetail.ToTable(True, "TNCSRNO", "Terms")
                    For Each drTerms As DataRow In dtTerms.Select("", "TNCSRNO", DataViewRowState.CurrentRows)
                        If Not String.IsNullOrEmpty(drTerms("Terms").ToString()) Then
                            TermsNcond = TermsNcond & drTerms("Terms").ToString() & vbCrLf
                        End If
                    Next
                End If

                strPrintMsg = New StringBuilder()
                sbDeliveryBillPrint = New StringBuilder()

                If dtView.Rows.Count() > 0 Then
                    '---- Changes By Mahesh : Customer Name is Not Mandatory for Home Delivery 
                    'If dtView.Rows(0)("HDName").ToString() <> String.Empty Then
                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP022") & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf
                        'strHomeDilery = strHomeDilery & getValueByKey("CLSCMP023") & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf
                        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP035") & DeliveryPersonName & vbCrLf
                        'strHomeDilery = strHomeDilery & SplitString(getValueByKey("CLSCMP024") & dtView.Rows(0)("HDAddress").ToString(), LineLength).ToString() & vbCrLf
                        'strHomeDilery = strHomeDilery & getValueByKey("CLSCMP025") & dtView.Rows(0)("HDTelNo").ToString() & vbCrLf
                        'strHomeDilery = strHomeDilery & getValueByKey("CLSCMP026") & dtView.Rows(0)("HDEmail").ToString() & vbCrLf

                    Else
                        If _CustomerNameRequiredInKOT AndAlso CustomerNameLine <> Nothing Then
                            StrCustomerName = CustomerNameLine.ToUpper.Replace("CUSTOMER", "").Trim()

                        End If
                        If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                            If Not AllowBillingOnlyAfterSelectionOfSalesType Then
                                CustomerSaleType = String.Empty
                            End If
                            If CustomerSaleType <> Nothing Then
                                Dim StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - CustomerSaleType.Length) & CustomerSaleType & vbCrLf & vbCrLf
                            End If
                        End If
                    End If
                End If


                StrFooter = StrFooter & TotalPromotionalMsg '& vbCrLf
                StrFooter = StrFooter & TermsNcond '& vbCrLf


                strPrintMsg.Append(StrHeader)
                '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                sbDeliveryBillPrint.Append(StrHeader)
                'strPrintMsg.Append(strLine)


                If StrSubHeader IsNot Nothing AndAlso Not String.IsNullOrEmpty(StrSubHeader.ToString()) Then
                    strPrintMsg.Append(StrSubHeader)
                    '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                    sbDeliveryBillPrint.Append(StrSubHeader)
                End If

                If (Not String.IsNullOrEmpty(strWelcomeMsg.ToString())) Then
                    strPrintMsg.Append(strLineL40 + vbCrLf)
                    strPrintMsg.Append(strWelcomeMsg.ToString())
                    '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                    sbDeliveryBillPrint.Append(strLineL40 + vbCrLf)
                    sbDeliveryBillPrint.Append(strWelcomeMsg.ToString())
                End If

                '---- Added By Mahesh maintaining seperate DeliveryBillPrint

                sbDeliveryBillPrint.Append(strDblLine)
                strPrintMsg.Append(strDblLine)
                strPrintMsg.Append(LineItemHeading)
                strPrintMsg.Append(strDblLine)
                strPrintMsg.Append(StrBody)

                '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                sbDeliveryBillPrint.Append(LineItemHeading)
                sbDeliveryBillPrint.Append(strDblLine)
                sbDeliveryBillPrint.Append(StrBody)
                sbDeliveryBillPrint.Append(StrCompanyInfo)

                strPrintMsg.Append(StrCompanyInfo)
                strPrintMsg.Append(strLine)

                If strTaxInformation IsNot Nothing AndAlso Not String.IsNullOrEmpty(strTaxInformation.ToString()) Then
                    strPrintMsg.Append(strTaxInformation.ToString().TrimEnd())
                    strPrintMsg.Append(vbCrLf + strLineL40 + vbCrLf)
                End If

                If strPromotionMsg IsNot Nothing AndAlso Not String.IsNullOrEmpty(strPromotionMsg.ToString()) Then
                    strPrintMsg.Append(strPromotionMsg.ToString().TrimEnd())
                    strPrintMsg.Append(vbCrLf + strLineL40 + vbCrLf)
                End If

                If StrSubFooter IsNot Nothing AndAlso Not String.IsNullOrEmpty(StrSubFooter.ToString()) Then
                    strPrintMsg.Append(StrSubFooter.ToString())
                End If

                strPrintMsg.Append(StrFooter)
                sbDeliveryBillPrint.Append(StrFooter)

                If (Not String.IsNullOrEmpty(strHomeDilery)) Then
                    strPrintMsg.Append(strLineL40 & vbCrLf)
                    strPrintMsg.Append(strHomeDilery)

                    ' ---- Added By Mahesh maintaining seperate DeliveryBillPrint
                    '--- Changes By Mahesh Not required in HD Print
                    'sbDeliveryBillPrint.Append(vbCrLf)
                    'sbDeliveryBillPrint.Append(strHomeDilery)

                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        'sbDeliveryBillPrint.Append(strPrintMsg)
                        sbDeliveryBillPrint.Append(strLineL40 & vbCrLf)
                        '--- Changes By Mahesh Not required in HD Print
                        sbDeliveryBillPrint.Append(getValueByKey("CLSCMP035") & DeliveryPersonName & vbCrLf)
                    End If
                End If
                'code added for Sbarro and Naturals, both print are same in case of home delivery
                sbDeliveryBillPrint.Remove(0, sbDeliveryBillPrint.Length - 1)
                sbDeliveryBillPrint.Append(Space(12) & "Delivery Copy" & vbCrLf)
                sbDeliveryBillPrint.Append(strPrintMsg)


                If GiftMsg <> String.Empty Then
                    GiftMsg = SplitString(GiftMsg).ToString()
                    strPrintMsg.Append(GiftMsg.TrimEnd() & vbCrLf)
                    strPrintMsg.Append(strLine)

                    sbDeliveryBillPrint.Append(GiftMsg.TrimEnd() & vbCrLf & strLine)
                End If
                'code  added for issue id 1530 by vipul
                Dim reason As String = ""
                reason = "reason:-" & vbCrLf & dtView.Rows(0)("AuthUserRemarks").ToString & vbCrLf
                If dtView.Rows(0)("AuthUserRemarks").ToString <> String.Empty Then
                    strPrintMsg.Append(strLine)
                    strPrintMsg.Append(SplitString(reason))

                End If

                PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")

                If PrinterName = Nothing Then
                    Exit Sub
                End If
                Dim msg As String = String.Empty
                If _PrintPreview = True Then

                    If (KOTBillPrintingRequired) Then
                        Call CashMemoKOTPrint(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, CustomerComments:=ReminderComments, EvassPizza:=EvasPizzaChanges)
                    End If
                    If EvasPizzaChanges Then
                        Dim s As C1BarCode = GetBarcode(CashMemoNo)
                        VarBarcode = s
                        IsEvassChanges = True
                    End If
                    'PrinterName = "HP LaserJet P3005 PCL6"
                    PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                    msg = fnPrint(strPrintMsg.ToString(), "PRV")

                    'If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                    '    msg = fnPrint(sbDeliveryBillPrint.ToString(), "PRV")
                    'End If
                    'code added for issue id 1528 by vipul
                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) AndAlso dtView.Rows(0)("BillIntermediateStatus").ToString <> "Deleted" Then

                        msg = fnPrint(sbDeliveryBillPrint.ToString(), "PRV")
                    End If


                Else
                    If (KOTBillPrintingRequired) Then
                        Call CashMemoKOTPrint(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, CustomerComments:=ReminderComments, EvassPizza:=EvasPizzaChanges)
                    End If
                    If EvasPizzaChanges Then
                        Dim s As C1BarCode = GetBarcode(CashMemoNo)
                        VarBarcode = s
                        IsEvassChanges = True
                    End If
                    strPrintMsg.Append(vbCrLf & vbCrLf & vbCrLf & vbCrLf)
                    PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                    If IsInvoicePrintFlag = False Then
                        msg = fnPrint(strPrintMsg.ToString(), "PRN", NoOfCopies:=NoOfCopies)
                    Else
                        If IsInvoicePrintRequired Then       'code is added by irfan for whether bill print is required or not
                            msg = fnPrint(strPrintMsg.ToString(), "PRN", NoOfCopies:=NoOfCopies)
                        End If
                    End If
                  

                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)

                        'msg = fnPrint(sbDeliveryBillPrint.ToString(), "PRN")
                        If (Not String.IsNullOrEmpty(DeliveryPersonName)) AndAlso dtView.Rows(0)("BillIntermediateStatus").ToString <> "Deleted" Then

                            msg = fnPrint(sbDeliveryBillPrint.ToString(), "PRN")
                        End If

                    End If

                End If
                If Not String.IsNullOrEmpty(msg) Then
                    errorMsg = msg
                End If
                'Added for fiscal printer
                Try
                    clsFiscalPrinting.fnFiscalPrint(strPrintMsg.ToString())
                Catch ex As Exception

                End Try
                'Added for fiscal printer

            Else
                errorMsg = getValueByKey("CLSCMP031")
            End If
        Catch ex As Exception
            errorMsg = ex.Message
        End Try
    End Sub

    Public Sub CashMemoPrintFormat_JamboKing(ByVal dayopenDate As Date, ByVal LangCode As String, ByVal Currency As String, ByVal Sitecode As String, ByVal Type As String, ByVal DuplicatePrinting As String, ByVal DeletedUserid As String, ByVal AuthorisedUser As String, ByRef errorMsg As String, Optional ByVal GiftMsg As String = "", Optional ByVal HierarachyWisePrintFlag As Boolean = False, Optional ByVal IsSavoy As Boolean = False, Optional ByVal ClientName As String = "", Optional ByVal NoOfCopies As Integer = 1)
        Try
            Dim strPrintMsg, sbDeliveryBillPrint As StringBuilder
            Dim strLineDetail As New StringBuilder
            Dim StrHeader, StrSubHeader, StrBody, StrCompanyInfo, StrTagLine, StrSubFooter, StrFooter, StrDuplicate, StrDeleteLine, StrCashMemoLine, StrCashierLine, StrSalesPersonLine As String
            Dim StrTokenNo, StrCustomerName As String
            Dim StrCMNo, StrCMDate, StrCMTime, StrCashier, StrSalesPerson As String
            Dim SiteLine, StrSiteTelline, StrAdrressLine, StrSiteReg1Line, StrSiteReg2Line As String
            Dim CLpLine, LineItemHeading, StrLineItem As String
            Dim CustomerNameLine, CLPPointsLine, CustmerPhoneNo As String
            Dim ItemCode, Desc, Qty, Rate, DiscAmt, DiscPer, TaxAmt, NetAmt, RateAfterDisc, GrossAmt As String
            Dim TotalQtyLine, TakeAwayQuantity, TotalGrossAmtLine, TotalDiscAmtLine, SubTotalLine, NetAmtLine, TotalTaxAmtLine As String
            Dim TotalFooterTaxDetail, FooterTaxLine As String
            Dim TenderDetails, TenderLine As String
            Dim TotalPromotionalMsg, PromoMsgLine, StrPrintHeaderLine, StrPrintFooterLine As String
            Dim TermsNcond As String
            Dim strLine As String
            Dim strDblLine As String
            Dim strHomeDilery As String
            Dim isTakeAwayQtyPrint As Boolean = False
            Dim IsHierarachyWisePrintFlag As Boolean = HierarachyWisePrintFlag


            errorMsg = ""
            Dim dtView As New DataTable
            Dim obj As New SpectrumBL.clsCashMemo()
            dtView = obj.GetCashMemo(CashMemoNo, Sitecode, LangCode)

            Dim dvItemDetail As New DataView(dtView, "", "", DataViewRowState.CurrentRows)
            'Dim DtUnique As DataTable = dvItemDetail.ToTable(True, "ArticleCode", "DISCRIPTION", "QUANTITY", "SELLINGPRICE", "GROSSAMT", "TOTALDISCOUNT", "TOTALDISCPERCENTAGE", "NETAMOUNT", "TOTALTAXAMOUNT")
            Dim DtUnique As DataTable = obj.GetBillDetailsDataForPrint(CashMemoNo, Sitecode, LangCode)
            Dim Dtcombo As DataTable = obj.GetComboDetailsDataForPrint(CashMemoNo, Sitecode, LangCode)
            If DtUnique Is Nothing Then Exit Sub

            '----- Code Added By Mahesh - Delivery Person and customer Sales Type value pick up from Database 
            If Not IsDBNull(dtView.Rows(0)("ServiceTaxAmount")) AndAlso Val(dtView.Rows(0)("ServiceTaxAmount")) > 0 Then
                CustomerSaleType = GetCustomerSaleType(dtView.Rows(0)("ServiceTaxAmount"))
            Else
                CustomerSaleType = ""
            End If
            If Not IsDBNull(dtView.Rows(0)("DeliveryPersonID")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("DeliveryPersonID")) Then
                DeliveryPersonName = dtView.Rows(0)("DeliveryPersonID").ToString()
            End If

            Dim TotaltakeAwayQty = DtUnique.Compute("SUM(TakeAwayQuantity)", "").ToString()
            If Val(TotaltakeAwayQty) > 0 Then
                isTakeAwayQtyPrint = True
            End If

            Dim StrSubHeader1, StrSubFooter1, strWelcomeMsg, strTaxInformation, strPromotionMsg As New StringBuilder
            PrinttHeaderAndFooter(StrSubHeader1, StrSubFooter1, strWelcomeMsg, strPromotionMsg, strTaxInformation, "CMInvc", _printType)

            If StrSubFooter1 IsNot Nothing AndAlso Not String.IsNullOrEmpty(StrSubFooter1.ToString()) Then
                StrTagLine = StrSubFooter1.ToString()
            End If

            If Not dtView Is Nothing Then
                strLine = "-".PadRight(LineLength, "-") & vbCrLf
                strDblLine = "=".PadRight(LineLength, "=") & vbCrLf
                If DuplicatePrinting <> String.Empty Then
                    StrDuplicate = DuplicatePrinting
                End If
                'StrDeleteLine = "Cash Memo Status: Deleted"


                'Rakesh-12.09.2013:Issue-7750
                If Not StrSubHeader1 Is Nothing AndAlso StrSubHeader1.ToString().Length > 0 Then
                    ' StrSubHeader = strLine & SplitString(StrSubHeader1.ToString(), LineLength).ToString().Trim & vbCrLf
                    'If _printType <> "L40" Then
                    StrSubHeader = strLine & SplitString(StrSubHeader1.ToString(), LineLength).ToString().Trim & vbCrLf
                    'End If
                End If

                If DuplicatePrinting <> String.Empty Then
                    StrHeader = "<B>" & StrDuplicate.PadLeft(((LineLength - StrDuplicate.Length) / 2) + StrDuplicate.Length, " ") & "</B>" & vbCrLf
                End If

                If Type = "DCM" Then
                    StrDeleteLine = getValueByKey("CLSCMP001")
                    StrHeader = StrDeleteLine & vbCrLf
                    'Request ID: <Update / Delete Request ID>
                    'Authorized By : <User Name >       Deleted at Time: <CM Time>
                    'StrHeader = StrHeader & "Request ID    :" & DeletedUserid & vbCrLf
                    'StrHeader = StrHeader & "Authorized By :" & AuthorisedUser & vbCrLf
                    StrHeader = StrHeader & getValueByKey("CLSCMP002") & DeletedUserid & vbCrLf
                    StrHeader = StrHeader & getValueByKey("CLSCMP003") & AuthorisedUser & vbCrLf
                End If
                Dim SiteName, Site As String
                Dim TS_SiteName As StringBuilder
                'SiteName = "Store Name: " & dtView.Rows(0)("SITEOFFICIALNAME").ToString()
                'Site = "Code: " & dtView.Rows(0)("SITECODE").ToString()
                'StrSiteTelline = "Tel: " & dtView.Rows(0)("TELNO").ToString()
                'StrAdrressLine = "Add: " & dtView.Rows(0)("ADDRESS").ToString()

                SiteName = getValueByKey("CLSCMP004") & " " & dtView.Rows(0)("SITEOFFICIALNAME").ToString()
                SiteName = dtView.Rows(0)("SITEOFFICIALNAME").ToString()
                TS_SiteName = SplitString(SiteName)
                SiteName = TS_SiteName.ToString
                'SiteName = SiteName.PadLeft(LineLength / 2 + (SiteName.Length / 2))
                Site = vbCrLf & getValueByKey("CLSCMP005") & " " & dtView.Rows(0)("SITECODE").ToString()
                'SiteLine = SiteName & Site
                If ClientName <> "" Then
                    Dim arrClientname = ClientName.Split(" ").ToArray()

                    For i = 0 To arrClientname.Length - 1
                        If SiteName.ToUpper.Contains(arrClientname(i).ToString.ToUpper) Then
                            SiteName = SiteName.ToUpper.Replace(arrClientname(i).ToString.ToUpper.Trim, "").Trim
                        End If
                    Next
                End If

                SiteName = SiteName.PadLeft((Val(LineLength - SiteName.Length) + SiteName.Length) / 2)
                '------------------
                Site = vbCrLf & getValueByKey("CLSCMP005") & " " & dtView.Rows(0)("SITECODE").ToString()
                'SiteLine = SiteName & Site
                SiteLine = SiteName.Trim.PadLeft((Val(45 - SiteName.Length) + SiteName.Length) / 2)



                ' SiteLine = SiteName
                ' SiteLine = SiteName
                'SiteLine = SiteName.PadRight(LineLength - Site.Length) & Site

                StrSiteTelline = getValueByKey("CLSCMP006") & " " & dtView.Rows(0)("TELNO").ToString()
                StrAdrressLine = getValueByKey("CLSCMP007") & " " & dtView.Rows(0)("ADDRESS").ToString()
                StrAdrressLine = SplitString(StrAdrressLine, LineLength).ToString()
                'StrSiteReg1Line = "LST NO: " & dtView.Rows(0)("LOCALSALESTAXNO").ToString() & Space(2) & "LST Date: " & dtView.Rows(0)("LOCALSALESTAXDATE").ToString()
                'StrSiteReg2Line = "CST NO: " & dtView.Rows(0)("CENTRALSALESTAXNO").ToString() & Space(2) & "CST Date: " & dtView.Rows(0)("CENTRALSALESTAXDATE").ToString()

                StrHeader &= SiteLine & vbCrLf
                StrHeader = StrHeader & StrAdrressLine.Trim & vbCrLf
                StrHeader = StrHeader & StrSiteTelline & vbCrLf
                'StrHeader = StrHeader & strLine & vbCrLf
                'StrSubHeader = StrSubHeader & StrSiteReg1Line & vbCrLf
                'StrSubHeader = StrSubHeader & StrSiteReg2Line & vbCrLf

                'Dim kotDataLine As String = String.Empty
                '****Added by Rahul 30 nov 2009 night(Rashid) . start 
                If Type = "DCM" Then
                    'StrCMNo = "Void Cash Memo:" & dtView.Rows(0)("Billno").ToString()
                    StrCMNo = getValueByKey("CLSCMP008") & dtView.Rows(0)("Billno").ToString()
                Else
                    'StrCMNo = "Cash Memo:" & dtView.Rows(0)("Billno").ToString()
                    StrCMNo = getValueByKey("CLSCMP009") & dtView.Rows(0)("Billno").ToString()
                End If
                '******End 

                'StrCMDate = "Date:" & Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())
                'StrCMDate = Format(dtView.Rows(0)("BillTime"), clsCommon.GetSystemDateFormat())
                StrCMDate = dayopenDate.ToShortDateString()
                strDayOpenDate = dayopenDate.ToShortDateString()
                If DuplicatePrinting <> String.Empty Then
                    StrCMDate = Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())
                End If
                StrTokenNo = dtView.Rows(0)("Billno").ToString().Trim.Substring(dtView.Rows(0)("Billno").ToString().Trim.Length - 2, 2)

                Select Case PrintFormatNo
                    Case 1
                        StrTokenNo = "<B>" & StrTokenNo & "</B>"
                    Case 2

                    Case Else

                End Select


                StrCashMemoLine = StrCMNo.PadRight(LineLength - 1 - StrCMDate.Length) & StrCMDate & vbCrLf
                'StrCashier = "Cashier:" & dtView.Rows(0)("Createdby").ToString()
                'StrCMTime = "Time:" & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")

                'StrCashier = getValueByKey("CLSPV006") & dtView.Rows(0)("Createdby").ToString()
                StrCashier = getValueByKey("CLSPV006") & LoginUserName
                StrCMTime = getValueByKey("CLSCMP032") & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")
                StrCashierLine = StrCashier.PadRight(LineLength - 1 - StrCMTime.Length) & StrCMTime & vbCrLf
                'StrSalesPerson = "Sales Person:" & dtView.Rows(0)("SALESEXECUTIVECODE").ToString()
                StrSalesPerson = getValueByKey("CLSCMP010") & dtView.Rows(0)("SALESEXECUTIVECODE").ToString()

                If _SalesPerApplicable = True Then
                    StrSalesPersonLine = StrSalesPerson & vbCrLf
                End If

                If (dtView.Rows.Count() > 0 And String.IsNullOrEmpty(DeliveryPersonName)) Then
                    If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                        ' strPrintMsg.Append(StrTokenNo)
                        'StrTokenNo = "Token No. " & StrTokenNo
                        If Not AllowBillingOnlyAfterSelectionOfSalesType Then
                            '   strPrintMsg.Append(CustomerSaleType)
                            CustomerSaleType = String.Empty
                        End If
                        ' Dim StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - (CustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "")).Length) & CustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "") & vbCrLf
                        'StrSubHeader = StrSubHeader & strLine
                        ' StrSubHeader = StrSubHeader & StrTokenSalesTypeLine
                    Else
                        If AllowBillingOnlyAfterSelectionOfSalesType Then
                            StrSubHeader = StrSubHeader & CustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "") & vbCrLf
                        End If
                    End If
                Else
                    If AllowBillingOnlyAfterSelectionOfSalesType Then
                        StrSubHeader = StrSubHeader & CustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "") + vbCrLf
                    End If
                End If
                StrSubHeader = StrSubHeader & strLine
                StrSubHeader = StrSubHeader & StrCashMemoLine
                StrSubHeader = StrSubHeader & StrCashierLine

                If (Not String.IsNullOrEmpty(dtView.Rows(0)("SALESEXECUTIVECODE").ToString())) Then
                    StrSubHeader = StrSubHeader & StrSalesPersonLine
                    'StrSubHeader = StrSubHeader & strLine
                End If

                Dim CLPaddress As String = ""
                Dim CLPBalancePoints As String
                If dtView.Rows(0)("CLPNO").ToString() <> String.Empty Then
                    'CLpLine = "CLP Card No:" & dtView.Rows(0)("CLPNO").ToString()
                    'CustomerNameLine = "Customer Name: " & dtView.Rows(0)("CLPNAME").ToString()
                    'CLPPointsLine = "CLP Points Earned: " & FormatNumber(dtView.Rows(0)("CLPPoints").ToString())

                    Select Case PrintFormatNo
                        Case 1
                            CLpLine = getValueByKey("CLSCMP011") & dtView.Rows(0)("CLPNO").ToString()
                        Case 2

                        Case Else

                    End Select

                    CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CLPNAME").ToString()
                    CLPPointsLine = getValueByKey("CLSCMP013") & FormatNumber(Val(dtView.Rows(0)("CLPPoints").ToString()))

                    If (dtView.Rows(0)("RedemptionPoints") IsNot DBNull.Value AndAlso dtView.Rows(0)("RedemptionPoints") > 0) Then
                        CLPPointsLine += vbCrLf + getValueByKey("CLSCMP034") & FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()))
                    End If

                    'CLPaddress = dtView.Rows(0)("CLPAddress").ToString()
                    'If CLPaddress <> String.Empty Then
                    '    CLPaddress = getValueByKey("CLSCMP014") & dtView.Rows(0)("CLPAddress").ToString()
                    'End If

                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then 'vipul
                        CLPaddress = dtView.Rows(0)("HDAddress").ToString()
                    Else
                        CLPaddress = dtView.Rows(0)("CLPAddress").ToString()
                    End If
                    If CLPaddress <> String.Empty Then
                        If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                            CLPaddress = getValueByKey("CLSCMP014") & dtView.Rows(0)("HDAddress").ToString()
                        Else
                            CLPaddress = getValueByKey("CLSCMP014") & dtView.Rows(0)("CLPAddress").ToString()
                        End If
                    End If

                    CLPBalancePoints = String.Empty
                    CLPBalancePoints = getValueByKey("CLSCMP015")
                    Dim dblCLPPoints As Double

                    dblCLPPoints = Val(dtView.Rows(0)("BalancePoints").ToString())
                    'For Each drRow As DataRow In dtView.Select("Tendertypecode = 'CLPPoint'")
                    '    dblCLPPoints -= drRow("RedemptionPoints")
                    'Next

                    Dim RedemptionPoints As DataRow = dtView.Select("Tendertypecode = 'CLPPoint'").FirstOrDefault()
                    If (RedemptionPoints IsNot Nothing AndAlso RedemptionPoints("RedemptionPoints") IsNot DBNull.Value) Then
                        dblCLPPoints -= Val(RedemptionPoints("RedemptionPoints").ToString())
                    End If


                    CLPBalancePoints = CLPBalancePoints & FormatNumber(dblCLPPoints.ToString())
                    If CLPaddress <> String.Empty Then
                        CLPaddress = SplitString(CLPaddress, LineLength).ToString()
                    End If
                    If CLpLine <> Nothing AndAlso CLpLine.ToString().Length > 0 Then
                        StrSubFooter = StrSubFooter & CLpLine & vbCrLf
                    End If
                    If CustomerSaleType = "Home Delivery" Then
                        CustmerPhoneNo = getValueByKey("RP187") & " " & dtView.Rows(0)("Mobileno").ToString()
                    Else
                        Dim tempMobileno = dtView.Rows(0)("Mobileno").ToString()
                        If dtView.Rows(0)("Mobileno").ToString().Length > 4 Then
                            Dim mobileLast4Digit = tempMobileno.Substring(tempMobileno.Length - 4, 4)
                            Dim mobilemolength = tempMobileno.Length
                            tempMobileno = mobileLast4Digit.PadLeft(tempMobileno.Length, "X")
                        End If
                        CustmerPhoneNo = getValueByKey("RP187") & " " & tempMobileno
                    End If

                    'StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
                    StrSubFooter = StrSubFooter & SplitString(CustomerNameLine).ToString() + vbCrLf & vbCrLf
                    StrSubFooter = StrSubFooter & CustmerPhoneNo & vbCrLf
                    StrSubFooter = StrSubFooter & CLPBalancePoints & vbCrLf

                    StrSubFooter = StrSubFooter & CLPPointsLine & vbCrLf
                    'StrSubFooter = StrSubFooter & CLPaddress & vbCrLf '& vbCrLf & vbCrLf

                ElseIf dtView.Columns.Contains("CustomerNo") AndAlso dtView.Rows(0)("CustomerNo").ToString() <> String.Empty Then
                    Select Case PrintFormatNo
                        Case 1
                            CLpLine = getValueByKey("CLSCMP038") & dtView.Rows(0)("CustomerNo").ToString()
                        Case 2

                        Case Else

                    End Select
                    CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CustomerName").ToString()
                    If CLpLine <> Nothing AndAlso CLpLine.ToString().Length > 0 Then
                        StrSubFooter = StrSubFooter & CLpLine & vbCrLf
                    End If
                    StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
                End If

                If Type = "CMWAmt" Then
                    'LineItemHeading = "Item Code" & Space(17) & "Item Name" & Space(1) & "Qty " & vbCrLf
                    ' LineItemHeading = getValueByKey("CLSPSO020")  & vbCrLf & getValueByKey("CLSCMP016") & Space(1) & getValueByKey("CLSPSO022") & " " & vbCrLf
                    'to resolve Issue 448 
                    LineItemHeading = getValueByKey("CLSPSO020") & Space(17) & vbCrLf & getValueByKey("CLSCMP016") & Space(20) & getValueByKey("CLSPSO022") & " " & vbCrLf
                Else
                    'LineItemHeading = "Item Code" & Space(17) & "Item Name" & Space(5) & vbCrLf
                    'to resolve Issue 448 
                    'LineItemHeading = "  Item Code" & vbCrLf & "  Item Name" & vbCrLf
                    'LineItemHeading = LineItemHeading & Space(3) & "Qty " & Space(2) & "Price" & Space(4) & "Disc." & Space(4) & "Tax" & Space(5) & "Net" & vbCrLf

                    'LineItemHeading = "  " & getValueByKey("CLSPSO020") & vbCrLf & "  " & getValueByKey("CLSCMP016") & vbCrLf
                    'LineItemHeading = LineItemHeading & Space(3) & getValueByKey("CLSPSO022") & " " & Space(2) & getValueByKey("CLSPSO023") & Space(4) & getValueByKey("CLSPSO024") & Space(4) & getValueByKey("CLSPSO025") & Space(5) & getValueByKey("CLSPSO026") & vbCrLf
                    LineItemHeading = getValueByKey("CLSCMP016") & Space(13) & getValueByKey("CLSPSO022") & " " & Space(5)
                    Select Case PrintFormatNo
                        Case 1

                        Case 2
                            LineItemHeading &= "Amt" & vbCrLf
                        Case Else
                            LineItemHeading &= "Amt" & vbCrLf

                    End Select
                End If

                Dim TotalQty, TGrossAmt, TDiscAmt, TRateAfterDisc, TTaxtAmt As String
                Dim TNetAmt As String = 0.0
                Dim BillLineNO As String
                For Each dr As DataRow In DtUnique.Rows
                    ItemCode = dr("ArticleCode").ToString()

                    If (DisplayArticleFullName) Then
                        Desc = dr("ArticleFullName").ToString()
                    Else
                        Desc = dr("DISCRIPTION").ToString()
                    End If
                    BillLineNO = dr("BillLineNO").ToString()
                    If SpectrumCommon.ComboItemPrintingAllowed AndAlso Dtcombo IsNot Nothing AndAlso Dtcombo.Rows.Count > 0 Then
                        Dim drp() = Dtcombo.Select("ComboArticleCode='" & ItemCode & "' AND BillLineNO ='" & BillLineNO & "'")
                        If drp.Length > 0 Then
                            Dim articleDescriptionDictionary As New Dictionary(Of String, Integer)
                            For index = 0 To drp.Length - 1
                                If articleDescriptionDictionary.ContainsKey(drp(index)("ArticleName")) Then
                                    articleDescriptionDictionary(drp(index)("ArticleName")) = articleDescriptionDictionary(drp(index)("ArticleName")) + drp(index)("Quantity")
                                Else
                                    articleDescriptionDictionary.Add(drp(index)("ArticleName"), drp(index)("Quantity"))
                                End If
                            Next
                            Dim AddedDesc = GetMultilinedString(articleDescriptionDictionary)
                            Desc = Desc & vbCrLf & AddedDesc
                        End If
                    End If

                    Qty = dr("Quantity").ToString()
                    Rate = dr("SellingPrice").ToString()
                    RateAfterDisc = (dr("SellingPrice") - dr("TOTALDISCOUNT")).ToString()
                    If CDbl(clsCommon.CheckIfBlank(Rate)) < 0 Then
                        Rate = CDbl(clsCommon.CheckIfBlank(Rate)) * -1
                    End If
                    DiscAmt = dr("TOTALDISCOUNT").ToString()
                    DiscPer = dr("TOTALDISCPERCENTAGE").ToString()
                    NetAmt = dr("NetAmount").ToString()
                    GrossAmt = dr("GROSSAMT").ToString()
                    TaxAmt = dr("TOTALTAXAMOUNT").ToString()
                    'Dim i As Integer = 0
                    'For i = ItemCode.Length To 26
                    '    ItemCode = ItemCode & " "
                    'Next
                    'For i = Desc.Length To 14
                    '    Desc = Desc & " "
                    'Next

                    If (AllowDecimalQty) Then
                        If (WeightScaleEnabled) Then
                            Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 3)
                        Else
                            Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 2)
                        End If
                    Else
                        Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 0)
                    End If

                    'For i = Qty.Length To 4
                    '    Qty = " " & Qty
                    'Next
                    Rate = FormatNumber(CDbl(clsCommon.CheckIfBlank(Rate)), 2)

                    RateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(RateAfterDisc)), 2)
                    'Rate = FormatCurrency(Rate, 2)
                    'For i = Rate.Length To 8
                    '    Rate = " " & Rate
                    'Next
                    DiscAmt = FormatNumber(CDbl(IIf(DiscAmt <> String.Empty, DiscAmt, 0)), 2)
                    'For i = DiscAmt.Length To 8
                    '    DiscAmt = " " & DiscAmt
                    'Next
                    DiscPer = FormatNumber(CDbl(IIf(DiscPer <> String.Empty, DiscPer, 0)), 2)
                    DiscPer = DiscPer & "%"
                    'For i = DiscPer.Length To 8
                    '    DiscPer = " " & DiscPer
                    'Next
                    NetAmt = FormatNumber(CDbl(NetAmt), 2)
                    GrossAmt = FormatNumber(CDbl(GrossAmt), 2)
                    'For i = NetAmt.Length To 8
                    '    NetAmt = " " & NetAmt
                    'Next
                    TaxAmt = FormatNumber(CDbl(IIf(TaxAmt <> String.Empty, TaxAmt, 0)), 2)
                    'For i = TaxAmt.Length To 7
                    '    TaxAmt = " " & TaxAmt
                    'Next

                    Dim TempNetAmt As String = "0"

                    If Type = "CMWAmt" Then
                        StrLineItem = ItemCode.PadRight(26) & vbCrLf & Desc.PadRight(35) & Qty & vbCrLf
                    Else
                        If 28 - Desc.Length < Qty.Length Then
                            StrLineItem = Desc & vbCrLf
                            Select Case PrintFormatNo
                                Case 1

                                Case 2
                                    StrLineItem = StrLineItem & Qty.PadLeft(28) & GrossAmt.PadLeft(11) & vbCrLf
                                Case Else
                                    TempNetAmt = NetAmt
                                    If Val(NetAmt) > 0 AndAlso Val(TaxAmt) > 0 Then
                                        TempNetAmt = CDbl(GrossAmt) - CDbl(TaxAmt)
                                    End If
                                    StrLineItem = StrLineItem & Qty.PadLeft(28) & TempNetAmt.PadLeft(11) & vbCrLf
                            End Select
                        Else
                            Select Case PrintFormatNo
                                Case 1

                                Case 2
                                    StrLineItem = Desc & Qty.PadLeft(28 - Desc.Length) & GrossAmt.PadLeft(11) & vbCrLf
                                Case Else
                                    TempNetAmt = NetAmt
                                    If Val(NetAmt) > 0 AndAlso Val(TaxAmt) > 0 Then
                                        TempNetAmt = CDbl(GrossAmt) - CDbl(TaxAmt)
                                    End If
                                    StrLineItem = Desc & Qty.PadLeft(28 - Desc.Length) & TempNetAmt.PadLeft(11) & vbCrLf
                            End Select
                        End If
                    End If
                    strLineDetail.Append(StrLineItem)

                    If CDbl(Qty.Trim) > 0 Then
                        TotalQty = CDbl(clsCommon.CheckIfBlank(TotalQty)) + CDbl(Qty.Trim)
                    End If
                    TDiscAmt = CDbl(clsCommon.CheckIfBlank(TDiscAmt)) + CDbl(DiscAmt.Trim)
                    TGrossAmt = CDbl(clsCommon.CheckIfBlank(TGrossAmt)) + CDbl(dr("GROSSAMT").ToString())
                    'TNetAmt = CDbl(clsCommon.CheckIfBlank(TNetAmt)) + CDbl(NetAmt.Trim)
                    'TNetAmt = Format(Math.Round(Convert.ToDecimal(CDbl(clsCommon.CheckIfBlank(TNetAmt)) + CDbl(NetAmt.Trim))), "0.00")
                    TNetAmt = Decimal.Add(FormatNumber(CDbl(NetAmt), 2).ToString(), TNetAmt)
                    TRateAfterDisc = CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)) + CDbl(RateAfterDisc.Trim)
                    TTaxtAmt = CDbl(clsCommon.CheckIfBlank(TTaxtAmt)) + CDbl(TaxAmt.Trim)
                Next

                TotalQty = FormatNumber(CDbl(clsCommon.CheckIfBlank(TotalQty)), 2)
                'TDiscAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TDiscAmt)), 2)
                dtView.Rows(0)("Discount") = IIf(IsDBNull(dtView.Rows(0)("Discount")), 0, dtView.Rows(0)("Discount"))
                TDiscAmt = FormatNumber(dtView.Rows(0)("Discount"), 2)
                TGrossAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TGrossAmt)), 2)
                'TNetAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TNetAmt)), 2)
                TNetAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TNetAmt)), RoundOff), 2)
                TRateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)), 2)
                TTaxtAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TTaxtAmt)), 2)
                StrBody = strLineDetail.ToString() '& vbCrLf
                StrBody = StrBody & strLine

                'TotalQtyLine = "Total Qty" & Space(3) & ":" & TotalQty
                'TotalQtyLine = getValueByKey("CLSCMP017") & Space(3) & ":" & TotalQty
                'StrBody = StrBody & TotalQtyLine & vbCrLf
                Dim TSubTotalAmt As String
                If Type <> "CMWAmt" Then
                    If CDbl(clsCommon.CheckIfBlank(TDiscAmt)) > 0 Then
                        Dim TotalDisPercent As String = Math.Round((CDbl(clsCommon.CheckIfBlank(TDiscAmt)) / CDbl(clsCommon.CheckIfBlank(TGrossAmt))) * 100, 1).ToString()
                        TSubTotalAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TGrossAmt)) - CDbl(clsCommon.CheckIfBlank(TDiscAmt)))
                        TSubTotalAmt = TSubTotalAmt & " " & Currency
                        SubTotalLine = "Sub Total".PadRight(LineLength - 16) & ":" & TSubTotalAmt.PadLeft(14)
                        TDiscAmt = TDiscAmt & " " & Currency
                        Dim ObjclsCommon As New clsCommon
                        Dim offerno As String = ObjclsCommon.GetOffernoForRoundOff()
                        If DtUnique.Rows(0)("PromotionId").ToString().Contains(offerno) And offerno <> "" Then
                            TotalDiscAmtLine = "Round Off".PadRight(LineLength - 16) & ":" & TDiscAmt.PadLeft(14)
                        Else
                            TotalDiscAmtLine = String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).PadRight(LineLength - 16) & ":" & TDiscAmt.PadLeft(14)
                        End If
                        'TotalDiscAmtLine = String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).PadRight(LineLength - 16) & ":" & TDiscAmt.PadLeft(14)
                    End If
                    TGrossAmt = TGrossAmt & " " & Currency
                    TNetAmt = TNetAmt & " " & Currency
                    TRateAfterDisc = TRateAfterDisc & " " & Currency
                    TTaxtAmt = TTaxtAmt & " " & Currency

                    TotalGrossAmtLine = "Total Amt".PadRight(LineLength - 16) & ":" & TGrossAmt.PadLeft(14)

                    NetAmtLine = getValueByKey("CLSCMP020").PadRight(LineLength - 16) & ":" & TNetAmt.PadLeft(14)
                    If _TaxDetail = True Then
                        Dim taxDetailsForBill As DataTable = obj.GetTaxDetailsForBillNo(Sitecode, dtView.Rows(0)("Billno").ToString())
                        If taxDetailsForBill IsNot Nothing AndAlso taxDetailsForBill.Rows.Count > 0 Then
                            For Each row As DataRow In taxDetailsForBill.Rows
                                'If CompositeTaxReqOnPrint = False AndAlso IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                                If IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                                Dim taxValue As String = row("TaxVAlue").ToString()
                                taxValue = FormatNumber(CDbl(clsCommon.CheckIfBlank(taxValue)), 2)
                                taxValue = taxValue & " " & Currency
                                FooterTaxLine = FooterTaxLine & row("TaxName").PadRight(LineLength - 16) & ":" & taxValue.PadLeft(14) & vbCrLf
                            Next
                        End If
                    End If
                    TotalFooterTaxDetail = FooterTaxLine
                    StrBody = StrBody & TotalGrossAmtLine & vbCrLf
                    If TotalDiscAmtLine IsNot Nothing Then
                        StrBody = StrBody & TotalDiscAmtLine & vbCrLf
                    End If
                    If SubTotalLine IsNot Nothing Then
                        StrBody = StrBody & strLine.Substring(30) & SubTotalLine & vbCrLf
                    End If

                    '  StrBody = StrBody & TotalFooterTaxDetail & strLine.Substring(30) & NetAmtLine & vbCrLf & strLine


                    StrBody = StrBody & strLine

                    'TenderLine = getValueByKey("CLSCMP021") & vbCrLf
                    'Dim dtTender As DataTable = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                    'For Each drTender As DataRow In dtTender.Rows
                    '    If UCase(obj.GetDefaultConfigValue(Sitecode, "CVInfoReqOnPrint")) = "FALSE" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)") Then
                    '        Continue For
                    '    End If
                    '    Dim tender As String = FormatNumber(CDbl(clsCommon.CheckIfBlank(drTender("AMOUNTTENDERED").ToString()))) & " " & drTender("CurrencyCode").ToString() & vbCrLf
                    '    Dim tenderName As String
                    '    'Added by Rohit
                    '    If drTender("CardNO").ToString() <> "" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(I)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(R)") Then
                    '        tenderName = drTender("TENDERHEADNAME").ToString() & "(" & drTender("CardNO").ToString() & ") "
                    '    Else
                    '        tenderName = drTender("TENDERHEADNAME").ToString()
                    '    End If
                    '    'End editing
                    '    If Not drTender("AMOUNTINCURRENCY") Is DBNull.Value AndAlso drTender("AMOUNTINCURRENCY") <> 0 Then '0000404
                    '        tender = FormatNumber(CDbl(drTender("AMOUNTINCURRENCY").ToString()), 2) & " " & drTender("CurrencyCode").ToString() & vbCrLf
                    '    End If
                    '    'tender = Format(CDbl(tender), "0.00")

                    '    'TenderDetails = tenderName.PadRight(LineLength - 19) & tender.PadLeft(20) 
                    '    '--- Code Added By Mahesh changes by on MOD clp changes
                    '    If drTender("TENDERTYPECODE").ToString() = "CLPPoint" Then
                    '        tender = FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()))
                    '    End If

                    '    TenderDetails = String.Format("{0}{1}{2}", tenderName.Trim(), Space(LineLength - (tenderName.Trim().Length + tender.Trim().Length)), tender.Trim()) & vbCrLf
                    '    TenderLine = TenderLine & TenderDetails
                    'Next
                    'StrBody = StrBody & TenderLine

                    'Rakesh-13.09.2013:KSL CR-7860--> Display total discount amount 
                    If (_IsDisplayTotalSaving) Then
                        If (Not String.IsNullOrEmpty(TDiscAmt) AndAlso Not String.Equals(TDiscAmt, "0.00")) Then
                            Dim totalSavingAmount As String = String.Format(getValueByKey("CLSCMP033"), Currency, TDiscAmt.Replace(Currency, String.Empty).Trim)
                            StrBody = StrBody & vbCrLf & SplitString(totalSavingAmount).ToString().Trim & vbCrLf & strLine & vbCrLf
                        Else
                            StrBody = StrBody & vbCrLf
                        End If
                    Else
                        StrBody = StrBody '& vbCrLf
                    End If

                End If

                'Company and Tin Info
                Dim NoOfReprints As String = IIf(IsDBNull(dtView.Rows(0)("NoofReprints")), String.Empty, dtView.Rows(0)("NoofReprints")).ToString()
                Dim CompanyName As String = dtView.Rows(0)("SITEOFFICIALNAME") 'obj.GetDefaultConfigValue(Sitecode, "CompanyName")

                StrCompanyInfo = String.Empty
                If Not String.IsNullOrEmpty(CompanyName) Then
                    'StrCompanyInfo = CompanyName & vbCrLf
                End If

                'Dim strtna = "" 'getValueByKey("cashmemoprint.tin") & obj.GetTinNumberForASite(Sitecode)
                '' strtna = SplitString(strtna, LineLength).ToString() & vbCrLf

                'If Not String.IsNullOrEmpty(strtna) Then
                '    StrCompanyInfo += strtna.TrimEnd() & vbCrLf
                'End If

                'added by khusrao adil on 04-07-2017 for GST No
                Dim GstNo = obj.GetTinNumberForASite(Sitecode)
                If (Not String.IsNullOrEmpty(GstNo)) AndAlso GstNo <> "0" Then
                    Dim strtna = "GST No.: " & GstNo
                    strtna = SplitString(strtna, LineLength).ToString() & vbCrLf
                    If Not String.IsNullOrEmpty(strtna) Then
                        StrCompanyInfo += strtna.TrimEnd() & vbCrLf
                    End If
                End If

                If (Not String.IsNullOrEmpty(StrTagLine)) Then
                    StrCompanyInfo += StrTagLine.TrimEnd() & vbCrLf
                End If

                'Dim remarks As String = dtView.Rows(0)("Remark")
                If Not IsDBNull(dtView.Rows(0)("Remark")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("Remark")) Then
                    Dim remarks As String = dtView.Rows(0)("Remark")
                    remarks = SplitString(getValueByKey("cashmemoprint.discountremark") & remarks, LineLength).ToString()
                    StrCompanyInfo = StrCompanyInfo & remarks.TrimEnd() & vbCrLf
                End If

                If Not String.IsNullOrEmpty(NoOfReprints) Then
                    StrCompanyInfo = StrCompanyInfo & "Re-Print Number : " & NoOfReprints & vbCrLf
                    StrCompanyInfo = StrCompanyInfo & "Re-Print Date : " & DateTime.Now.ToShortDateString() & vbCrLf
                End If
                'Company and Tin Info End

                Dim dtPromo As DataTable = dvItemDetail.ToTable(True, "PROMOTIONALMSGID", "PROMOMESS")
                For Each drTerms As DataRow In dtPromo.Select("", "PROMOTIONALMSGID", DataViewRowState.CurrentRows)
                    If Not String.IsNullOrEmpty(drTerms("PROMOMESS").ToString()) Then
                        PromoMsgLine = PromoMsgLine & drTerms("PROMOMESS").ToString() & vbCrLf
                    End If
                Next

                TotalPromotionalMsg = PromoMsgLine

                If Not IsSavoy Then  'Terms 
                    Dim dtTerms As DataTable = dvItemDetail.ToTable(True, "TNCSRNO", "Terms")
                    For Each drTerms As DataRow In dtTerms.Select("", "TNCSRNO", DataViewRowState.CurrentRows)
                        If Not String.IsNullOrEmpty(drTerms("Terms").ToString()) Then
                            TermsNcond = TermsNcond & drTerms("Terms").ToString() & vbCrLf
                        End If
                    Next
                End If
                'If dtView.Rows.Count() > 0 Then
                '    If dtView.Rows(0)("HDName").ToString() <> String.Empty Then
                '        strHomeDilery = "Delivery Date:-" & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & "Name:-" & dtView.Rows(0)("HDName").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & "Address:-" & dtView.Rows(0)("HDAddress").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & "TelNo:-" & dtView.Rows(0)("HDTelNo").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & "Email:-" & dtView.Rows(0)("HDEmail").ToString() & vbCrLf
                '    End If
                'End If
                strPrintMsg = New StringBuilder()
                sbDeliveryBillPrint = New StringBuilder()

                If dtView.Rows.Count() > 0 Then
                    '---- Changes By Mahesh : Customer Name is Not Mandatory for Home Delivery 
                    'If dtView.Rows(0)("HDName").ToString() <> String.Empty Then
                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP022") & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf
                        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP023") & dtView.Rows(0)("HDName").ToString() & vbCrLf
                        strHomeDilery = strHomeDilery & SplitString(getValueByKey("CLSCMP024") & dtView.Rows(0)("HDAddress").ToString(), LineLength).ToString() & vbCrLf
                        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP025") & dtView.Rows(0)("HDTelNo").ToString() & vbCrLf
                        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP026") & dtView.Rows(0)("HDEmail").ToString() & vbCrLf

                    Else
                        If _CustomerNameRequiredInKOT AndAlso CustomerNameLine <> Nothing Then
                            StrCustomerName = CustomerNameLine.ToUpper.Replace("CUSTOMER", "").Trim()

                        End If
                        If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                            If Not AllowBillingOnlyAfterSelectionOfSalesType Then
                                CustomerSaleType = String.Empty
                            End If
                            Dim StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - CustomerSaleType.Length) & CustomerSaleType & vbCrLf & vbCrLf
                        End If
                    End If
                End If


                StrFooter = StrFooter & TotalPromotionalMsg '& vbCrLf
                StrFooter = StrFooter & TermsNcond '& vbCrLf


                strPrintMsg.Append(StrHeader)
                '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                sbDeliveryBillPrint.Append(StrHeader)
                'strPrintMsg.Append(strLine)


                If StrSubHeader IsNot Nothing AndAlso Not String.IsNullOrEmpty(StrSubHeader.ToString()) Then
                    strPrintMsg.Append(StrSubHeader)
                    '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                    sbDeliveryBillPrint.Append(StrSubHeader)
                End If

                If (Not String.IsNullOrEmpty(strWelcomeMsg.ToString())) Then
                    'strPrintMsg.Append(strLineL40)
                    strPrintMsg.Append(strWelcomeMsg.ToString())
                    '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                    sbDeliveryBillPrint.Append(strLineL40 + vbCrLf)
                    sbDeliveryBillPrint.Append(strWelcomeMsg.ToString())
                End If

                '---- Added By Mahesh maintaining seperate DeliveryBillPrint

                sbDeliveryBillPrint.Append(vbCrLf & strDblLine)
                'sbDeliveryBillPrint.Append(strDblLine)
                'strPrintMsg.Append(strDblLine)
                strPrintMsg.Append(vbCrLf & strDblLine)
                strPrintMsg.Append(LineItemHeading)
                strPrintMsg.Append(strDblLine)
                strPrintMsg.Append(StrBody)

                '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                sbDeliveryBillPrint.Append(LineItemHeading)
                sbDeliveryBillPrint.Append(strDblLine)
                sbDeliveryBillPrint.Append(StrBody)

                strPrintMsg.Append(StrCompanyInfo)
                strPrintMsg.Append(strLine)

                If strTaxInformation IsNot Nothing AndAlso Not String.IsNullOrEmpty(strTaxInformation.ToString()) Then
                    strPrintMsg.Append(strTaxInformation.ToString().TrimEnd())
                    strPrintMsg.Append(vbCrLf + strLineL40 + vbCrLf)
                End If

                If strPromotionMsg IsNot Nothing AndAlso Not String.IsNullOrEmpty(strPromotionMsg.ToString()) Then
                    strPrintMsg.Append(strPromotionMsg.ToString().TrimEnd())
                    strPrintMsg.Append(vbCrLf + strLineL40 + vbCrLf)
                End If

                If StrSubFooter IsNot Nothing AndAlso Not String.IsNullOrEmpty(StrSubFooter.ToString()) Then
                    strPrintMsg.Append(StrSubFooter.ToString())
                End If

                strPrintMsg.Append(StrFooter)

                If (Not String.IsNullOrEmpty(strHomeDilery)) Then
                    strPrintMsg.Append(strLineL40 & vbCrLf)
                    strPrintMsg.Append(strHomeDilery)

                    ' ---- Added By Mahesh maintaining seperate DeliveryBillPrint
                    '--- Changes By Mahesh Not required in HD Print
                    'sbDeliveryBillPrint.Append(vbCrLf)
                    'sbDeliveryBillPrint.Append(strHomeDilery)



                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        'sbDeliveryBillPrint.Append(strPrintMsg)
                        'sbDeliveryBillPrint.Append(strLineL40 & vbCrLf)
                        '--- Changes By Mahesh Not required in HD Print
                        'sbDeliveryBillPrint.Append(getValueByKey("CLSCMP035") & DeliveryPersonName & vbCrLf)
                    End If
                End If

                If GiftMsg <> String.Empty Then
                    GiftMsg = SplitString(GiftMsg).ToString()
                    strPrintMsg.Append(GiftMsg.TrimEnd() & vbCrLf)
                    strPrintMsg.Append(strLine)

                    sbDeliveryBillPrint.Append(GiftMsg.TrimEnd() & vbCrLf & strLine)
                End If

                PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")

                If PrinterName = Nothing Then
                    Exit Sub
                End If
                Dim msg As String = String.Empty
                If Not IsDBNull(dtView.Rows(0)("ServiceTaxAmount")) AndAlso Val(dtView.Rows(0)("ServiceTaxAmount")) > 0 Then
                    CustomerSaleType = GetCustomerSaleType(Convert.ToInt16(dtView.Rows(0)("ServiceTaxAmount")))
                Else
                    CustomerSaleType = ""
                End If
                If _PrintPreview = True Then

                    If (KOTBillPrintingRequired) Then
                        If IsDintInEnabled AndAlso CustomerSaleType = "Dine In" Then
                        Else
                            Call CashMemoKOTPrintBasedOnPrintFormatNo(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, , IsHierarachyWisePrintFlag)
                        End If
                    End If
                    'PrinterName = "HP LaserJet P3005 PCL6"
                    PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                    msg = fnPrint(strPrintMsg.ToString(), "PRV", 1)

                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        msg = fnPrint(sbDeliveryBillPrint.ToString(), "PRV", 1)
                    End If
                Else
                    If (KOTBillPrintingRequired) Then
                        If IsDintInEnabled AndAlso CustomerSaleType = "Dine In" Then
                        Else
                            Call CashMemoKOTPrintBasedOnPrintFormatNo(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, , IsHierarachyWisePrintFlag)
                        End If
                    End If
                    strPrintMsg.Append(vbCrLf & vbCrLf & vbCrLf & vbCrLf)
                    PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                    If IsInvoicePrintFlag = False Then
                        msg = fnPrint(strPrintMsg.ToString(), "PRN", 1, NoOfCopies:=NoOfCopies)
                    Else
                        If IsInvoicePrintRequired Then
                            msg = fnPrint(strPrintMsg.ToString(), "PRN", 1, NoOfCopies:=NoOfCopies)
                        End If
                    End If
                   

                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)

                        msg = fnPrint(sbDeliveryBillPrint.ToString(), "PRN", 1)
                    End If

                End If
                If Not String.IsNullOrEmpty(msg) Then
                    errorMsg = msg
                End If
                'Added for fiscal printer
                Try
                    clsFiscalPrinting.fnFiscalPrint(strPrintMsg.ToString())
                Catch ex As Exception

                End Try
                'Added for fiscal printer

            Else
                errorMsg = getValueByKey("CLSCMP031")
            End If
        Catch ex As Exception
            errorMsg = ex.Message
        End Try
    End Sub
    Dim strLastLength As Integer = 0
    Public Function SplitStringPC(ByVal strCheckString As String, ByVal stringLength As Int32, Optional ByVal blankSpace As Int32 = 0, Optional ByVal NoMoreSpace As Boolean = False) As StringBuilder
        Try
            Dim outputValue As New StringBuilder
            'stringLength = 39 - 23
            Dim currentLength As Integer = stringLength
            Dim IsAddedBlankSpace As Boolean
            Dim currentString As String = strCheckString
            Dim strLineValueOld As String = String.Empty, strLineValueNew As String = String.Empty

            If (strCheckString.Trim().Length < stringLength) Then
                outputValue.Append(strCheckString + vbCrLf)
                strLastLength = strCheckString.Trim.Length
            Else
                While (Not currentString.Trim().Length = 0)

                    For Each strValue As String In currentString.Split(Space(1))
                        strLineValueNew += strValue + Space(1)
                        strLastLength = strLineValueNew.Trim.Length
                        If (strLineValueNew.Length <= currentLength) Then
                            strLineValueOld = strLineValueNew
                            strLastLength = strLineValueOld.Trim.Length
                        Else
                            Exit For
                        End If
                    Next

                    If (Not String.IsNullOrEmpty(strLineValueOld.ToString().Trim())) Then
                        If NoMoreSpace Then
                            outputValue.Append(IIf(IsAddedBlankSpace, Space(blankSpace) + strLineValueOld.Trim().PadLeft(LineLength / 2 + strLineValueOld.Length / 2) + vbCrLf, strLineValueOld.Trim.PadLeft(LineLength / 2 + strLineValueOld.Length / 2) + vbCrLf))
                        Else
                            'outputValue.Append(IIf(IsAddedBlankSpace, Space(blankSpace) + strLineValueOld.TrimEnd().PadLeft(LineLength / 2 + strLineValueOld.Length / 2) + vbCrLf, strLineValueOld.Trim.PadLeft(LineLength / 2 + strLineValueOld.Length / 2) + vbCrLf + vbCrLf))
                            outputValue.Append(IIf(IsAddedBlankSpace, Space(blankSpace) + strLineValueOld.Trim().PadLeft(LineLength / 2 + strLineValueOld.Length / 2) + vbCrLf, strLineValueOld.Trim.PadLeft(LineLength / 2 + strLineValueOld.Length / 2) + vbCrLf + vbCrLf))
                        End If
                        currentString = currentString.Replace(strLineValueOld.TrimEnd(), String.Empty)
                        currentString = currentString.Trim
                    Else
                        If (strLineValueNew.Length > currentLength) Then
                            Dim newString1 As String = String.Empty
                            Dim newString2 As String = String.Empty

                            For Each charValue As Char In strLineValueNew.ToArray()
                                If (newString1.Length < currentLength) Then
                                    newString1 += charValue
                                Else
                                    newString2 += newString1 + vbCrLf
                                    newString1 = String.Empty
                                End If
                            Next

                            strLineValueOld = newString2 + newString1

                            outputValue.Append(strLineValueOld + Space(1) + vbCrLf)
                            currentString = currentString.Replace(strLineValueNew.TrimEnd(), String.Empty)
                        End If
                    End If

                    strLineValueNew = String.Empty
                    strLineValueOld = String.Empty

                    If (Not IsAddedBlankSpace AndAlso blankSpace > 0) Then
                        IsAddedBlankSpace = True
                        currentLength = stringLength - blankSpace
                    End If

                End While

            End If

            ' Dim output As String = outputValue.ToString().Substring(0, outputValue.ToString().Length - 1).Replace(vbCr, vbCrLf)
            Dim output As String = outputValue.ToString()

            outputValue = New StringBuilder
            outputValue.Append(output)

            Return outputValue

        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return Nothing
        End Try
    End Function
    Dim strLastLengthN As Integer = 0
    Public Function SplitStringPCWithNoLastNewLine(ByVal strCheckString As String, ByVal stringLength As Int32, Optional ByVal blankSpace As Int32 = 0) As StringBuilder
        Try
            Dim outputValue As New StringBuilder
            'stringLength = 39 - 23
            Dim currentLength As Integer = stringLength
            Dim IsAddedBlankSpace As Boolean
            Dim currentString As String = strCheckString
            Dim strLineValueOld As String = String.Empty, strLineValueNew As String = String.Empty
            Dim CutLine As Boolean = False
            If (strCheckString.Trim().Length < stringLength) Then
                outputValue.Append(strCheckString)
                strLastLengthN = strCheckString.Trim.Length
            Else
                While (Not currentString.Trim().Length = 0)

                    For Each strValue As String In currentString.Split(Space(1))
                        strLineValueNew += strValue + Space(1)
                        strLastLengthN = strLineValueNew.Trim.Length
                        If (strLineValueNew.Length <= currentLength) Then
                            strLineValueOld = strLineValueNew
                            CutLine = False
                        Else
                            CutLine = True
                            Exit For
                        End If
                    Next

                    If (Not String.IsNullOrEmpty(strLineValueOld.ToString().Trim())) Then
                        outputValue.Append(IIf(IsAddedBlankSpace, Space(blankSpace) + strLineValueOld.TrimEnd() + vbCrLf, strLineValueOld.Trim + IIf(CutLine = True, vbCrLf, "")))
                        currentString = currentString.Replace(strLineValueOld.TrimEnd(), String.Empty)
                        currentString = currentString.Trim
                    Else
                        If (strLineValueNew.Length > currentLength) Then
                            Dim newString1 As String = String.Empty
                            Dim newString2 As String = String.Empty

                            For Each charValue As Char In strLineValueNew.ToArray()
                                If (newString1.Length < currentLength) Then
                                    newString1 += charValue
                                Else
                                    newString2 += newString1 + vbCrLf
                                    newString1 = String.Empty
                                End If
                            Next

                            strLineValueOld = newString2 + newString1
                            outputValue.Append(strLineValueOld + Space(1) + vbCrLf)
                            currentString = currentString.Replace(strLineValueNew.TrimEnd(), String.Empty)
                        End If
                    End If

                    strLineValueNew = String.Empty
                    strLineValueOld = String.Empty

                    If (Not IsAddedBlankSpace AndAlso blankSpace > 0) Then
                        IsAddedBlankSpace = True
                        currentLength = stringLength - blankSpace
                    End If

                End While

            End If

            ' Dim output As String = outputValue.ToString().Substring(0, outputValue.ToString().Length - 1).Replace(vbCr, vbCrLf)
            Dim output As String = outputValue.ToString()

            outputValue = New StringBuilder
            outputValue.Append(output)

            Return outputValue

        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return Nothing
        End Try
    End Function
    Public Sub CashMemoPrintFormat_PC(ByVal dayopenDate As Date, ByVal LangCode As String, ByVal Currency As String, ByVal Sitecode As String, ByVal Type As String, ByVal DuplicatePrinting As String, ByVal DeletedUserid As String, ByVal AuthorisedUser As String, ByRef errorMsg As String, Optional ByVal GiftMsg As String = "", Optional ByVal IsSavoy As Boolean = False, Optional ByVal MettlerConn As String = "", Optional ByVal IsArticleWiseKot As Boolean = False, Optional ByVal IsCounterCopy As Boolean = False, Optional ByVal IsFinalReceipt As Boolean = False, Optional ByVal ClientName As String = "", Optional ByVal IsCounterCopyKot As Boolean = False, Optional ByVal NoOfCopies As Integer = 1, Optional ByVal HierarachyWisePrintFlag As Boolean = False)
        Try
            Dim strPrintMsg, sbDeliveryBillPrint As StringBuilder
            Dim strLineDetail As New StringBuilder
            Dim StrHeader, StrSubHeader, StrBody, StrCompanyInfo, StrTagLine, StrSubFooter, StrFooter, StrDuplicate, StrDeleteLine, StrCashMemoLine, StrCashierLine, StrSalesPersonLine As String
            Dim StrTokenNo, StrCustomerName As String
            Dim StrCMNo, StrCMDate, StrCMTime, StrCashier, StrSalesPerson As String
            Dim SiteLine, StrSiteTelline, StrAdrressLine, StrSiteReg1Line, StrSiteReg2Line As String
            Dim CLpLine, LineItemHeading, StrLineItem As String
            Dim CustomerNameLine, CLPPointsLine, CustmerPhoneNo As String
            Dim ItemCode, Desc, Qty, Rate, DiscAmt, DiscPer, TaxAmt, NetAmt, NetAmtTemp, RateAfterDisc, GrossAmt, FinScaleNo As String
            Dim TotalQtyLine, TakeAwayQuantity, TotalGrossAmtLine, TotalDiscAmtLine, SubTotalLine, NetAmtLine, TotalTaxAmtLine As String
            Dim TotalFooterTaxDetail, FooterTaxLine, FooterTaxOld As String
            Dim TenderDetails, TenderLine As String
            Dim TotalPromotionalMsg, PromoMsgLine, StrPrintHeaderLine, StrPrintFooterLine As String
            Dim TermsNcond As String
            Dim strLine As String
            Dim strDblLine As String
            Dim strHomeDilery As String
            Dim isTakeAwayQtyPrint As Boolean = False
            errorMsg = ""
            Dim dtView As New DataTable
            Dim BoldLineLength As Int32 = 27
            Dim obj As New SpectrumBL.clsCashMemo()
            dtView = obj.GetCashMemo(CashMemoNo, Sitecode, LangCode)

            Dim dvItemDetail As New DataView(dtView, "", "", DataViewRowState.CurrentRows)
            'Dim DtUnique As DataTable = dvItemDetail.ToTable(True, "ArticleCode", "DISCRIPTION", "QUANTITY", "SELLINGPRICE", "GROSSAMT", "TOTALDISCOUNT", "TOTALDISCPERCENTAGE", "NETAMOUNT", "TOTALTAXAMOUNT")
            Dim DtUnique As DataTable = obj.GetBillDetailsDataForPrint(CashMemoNo, Sitecode, LangCode, True)
            Dim Dtcombo As DataTable = obj.GetComboDetailsDataForPrint(CashMemoNo, Sitecode, LangCode)
            DtScannedMettlerBills = obj.GetMettlerBillDetailsDataForPrint(CashMemoNo, Sitecode)
            If DtUnique Is Nothing Then Exit Sub

            '----- Code Added By Mahesh - Delivery Person and customer Sales Type value pick up from Database 
            If Not IsDBNull(dtView.Rows(0)("ServiceTaxAmount")) AndAlso Val(dtView.Rows(0)("ServiceTaxAmount")) > 0 Then
                CustomerSaleType = GetCustomerSaleType(dtView.Rows(0)("ServiceTaxAmount"))
                CustomerSaleTypeDineIn = CustomerSaleType
            Else
                CustomerSaleType = ""
            End If
            If Not IsDBNull(dtView.Rows(0)("DeliveryPersonID")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("DeliveryPersonID")) Then
                DeliveryPersonName = dtView.Rows(0)("DeliveryPersonID").ToString()
            End If

            Dim TotaltakeAwayQty = DtUnique.Compute("SUM(TakeAwayQuantity)", "").ToString()
            If Val(TotaltakeAwayQty) > 0 Then
                isTakeAwayQtyPrint = True
            End If

            Dim StrSubHeader1, StrSubFooter1, strWelcomeMsg, strTaxInformation, strPromotionMsg As New StringBuilder
            PrinttHeaderAndFooter(StrSubHeader1, StrSubFooter1, strWelcomeMsg, strPromotionMsg, strTaxInformation, "CMInvc", _printType)

            If StrSubFooter1 IsNot Nothing AndAlso Not String.IsNullOrEmpty(StrSubFooter1.ToString()) Then
                StrTagLine = StrSubFooter1.ToString()
            End If

            If Not dtView Is Nothing Then
                strLine = "-".PadRight(LineLength, "-") & vbCrLf
                strDblLine = "=".PadRight(LineLength, "=") & vbCrLf
                If DuplicatePrinting <> String.Empty Then
                    StrDuplicate = DuplicatePrinting
                End If
                'StrDeleteLine = "Cash Memo Status: Deleted"


                'Rakesh-12.09.2013:Issue-7750
                If Not StrSubHeader1 Is Nothing AndAlso StrSubHeader1.ToString().Length > 0 Then
                    ' StrSubHeader = strLine & SplitString(StrSubHeader1.ToString(), LineLength).ToString().Trim & vbCrLf
                    'If _printType <> "L40" Then
                    StrSubHeader = strLine & SplitString(StrSubHeader1.ToString(), LineLength).ToString().Trim & vbCrLf
                    'End If
                End If

                If DuplicatePrinting <> String.Empty Then
                    'Added
                    ' StrHeader = "<B>" & StrDuplicate.PadLeft(((LineLength - StrDuplicate.Length) / 2) + StrDuplicate.Length - 6, " ") & "</B>" & vbCrLf
                    StrHeader = "<FontPCKOTArticle>" & StrDuplicate.PadLeft((Val(LineLength - StrDuplicate.Length) + StrDuplicate.Length - 6) / 2) & "</FontPCKOTArticle>" & vbCrLf & vbCrLf
                End If

                If Type = "DCM" Then
                    StrDeleteLine = getValueByKey("CLSCMP001")
                    StrHeader = StrDeleteLine & vbCrLf
                    'Request ID: <Update / Delete Request ID>
                    'Authorized By : <User Name >       Deleted at Time: <CM Time>
                    'StrHeader = StrHeader & "Request ID    :" & DeletedUserid & vbCrLf
                    'StrHeader = StrHeader & "Authorized By :" & AuthorisedUser & vbCrLf
                    StrHeader = StrHeader & getValueByKey("CLSCMP002") & DeletedUserid & vbCrLf
                    StrHeader = StrHeader & getValueByKey("CLSCMP003") & AuthorisedUser & vbCrLf
                End If
                Dim SiteName, Site As String
                Dim TS_SiteName As StringBuilder
                'SiteName = "Store Name: " & dtView.Rows(0)("SITEOFFICIALNAME").ToString()
                'Site = "Code: " & dtView.Rows(0)("SITECODE").ToString()
                'StrSiteTelline = "Tel: " & dtView.Rows(0)("TELNO").ToString()
                'StrAdrressLine = "Add: " & dtView.Rows(0)("ADDRESS").ToString()

                SiteName = getValueByKey("CLSCMP004") & " " & dtView.Rows(0)("SITEOFFICIALNAME").ToString()
                SiteName = dtView.Rows(0)("SITEOFFICIALNAME").ToString()

                '------------Added For Font Changes as Requested By Client
                'TS_SiteName = SplitString(SiteName)
                'SiteName = TS_SiteName.ToString
                ' SiteName = SiteName.PadLeft(LineLength / 2 + (SiteName.Length / 2))
                If ClientName <> "" Then
                    Dim arrClientname = ClientName.Split(" ").ToArray()

                    For i = 0 To arrClientname.Length - 1
                        If SiteName.ToUpper.Contains(arrClientname(i).ToString.ToUpper) Then
                            SiteName = SiteName.ToUpper.Replace(arrClientname(i).ToString.ToUpper.Trim, "").Trim
                        End If
                    Next
                End If
                SiteName = SiteName.PadLeft((Val(LineLength - SiteName.Length) + SiteName.Length) / 2)
                '------------------
                Site = vbCrLf & getValueByKey("CLSCMP005") & " " & dtView.Rows(0)("SITECODE").ToString()
                'SiteLine = SiteName & Site
                SiteLine = SiteName.Trim.PadLeft((Val(45 - SiteName.Length) + SiteName.Length) / 2)
                'SiteLine = SiteName.PadRight(LineLength - Site.Length) & Site

                'StrSiteTelline = getValueByKey("CLSCMP006") & " " & dtView.Rows(0)("TELNO").ToString()
                '
                Dim PhoneLine As String = "Ph:" & dtView.Rows(0)("TELNO").ToString()
                '-----------------Added For Font Changes as Requested By Client
                'StrSiteTelline = PhoneLine.PadLeft(LineLength / 2 + (PhoneLine.Length / 2))
                '        StrSiteTelline = PhoneLine.PadLeft((Val(BoldLineLength - PhoneLine.Length) + PhoneLine.Length) / 2)
                StrSiteTelline = PhoneLine.PadLeft(40 / 2)
                'StrSiteTelline = SplitStringCenterAlign(PhoneLine, LineLength).ToString
                '--------------------
                'StrAdrressLine = getValueByKey("CLSCMP007") & " " & dtView.Rows(0)("ADDRESS").ToString()
                '---------------Added For Font Changes as Requested By Client
                'StrAdrressLine = dtView.Rows(0)("ADDRESS").ToString()
                Dim addressLine1 As String = dtView.Rows(0)("ADDRESSLINE1").ToString().Trim
                Dim addressLine2 As String = dtView.Rows(0)("ADDRESSLINE2").ToString().Trim
                Dim DumyaddressLine As String = addressLine1 & Space(1) & addressLine2
                DumyaddressLine = SplitStringCenterAlign(DumyaddressLine, 28).ToString
                Dim addressLine3 As String = dtView.Rows(0)("ADDRESSPINCODE").ToString().Trim

                '   Dim addressPincode As String = addressLine2 & Space(1) & addressLine3
                '  StrAdrressLine = Space(1) & addressLine1.PadLeft((Val(BoldLineLength - addressLine1.Length) + addressLine1.Length) / 2) & vbCrLf & vbCrLf & addressPincode.PadLeft(Val(LineLength + addressLine3.Length) / 2)
                StrAdrressLine = DumyaddressLine & addressLine3.PadLeft(35 / 2)
                ' (LineLength / 2 + (SiteName.Length / 2)
                '----------------------
                ''Code for Address split and centre align
                'StrAdrressLine = SplitString(StrAdrressLine, LineLength).ToString()
                'StrAdrressLine = SplitString(StrAdrressLine, LineLength).ToString()
                '----------------Added For Font Changes as Requested By Client
                ' StrAdrressLine = SplitStringCenterAlign(StrAdrressLine, LineLength).ToString()
                '----------------------
                'StrSiteReg1Line = "LST NO: " & dtView.Rows(0)("LOCALSALESTAXNO").ToString() & Space(2) & "LST Date: " & dtView.Rows(0)("LOCALSALESTAXDATE").ToString()
                'StrSiteReg2Line = "CST NO: " & dtView.Rows(0)("CENTRALSALESTAXNO").ToString() & Space(2) & "CST Date: " & dtView.Rows(0)("CENTRALSALESTAXDATE").ToString()


                'Added For Font Changes as Requested By Client-------------------
                'StrHeader &= SiteLine & vbCrLf
                'StrHeader = StrHeader & StrAdrressLine & vbCrLf

                StrHeader &= "<FontPCBillHeader>" & SiteLine & "</FontPCBillHeader>" & vbCrLf & vbCrLf
                StrHeader = StrHeader & "<FontPCBillHeader>" & StrAdrressLine & "</FontPCBillHeader>" & vbCrLf '& vbCrLf


                '------------------------

                ' Added For Font Changes as Requested By Client-------------
                'StrHeader = StrHeader & StrSiteTelline & vbCrLf
                StrHeader = StrHeader & "<FontPCBillHeader>" & StrSiteTelline & "</FontPCBillHeader>" & vbCrLf
                '--------------------

                'StrHeader = StrHeader & strLine & vbCrLf
                'StrSubHeader = StrSubHeader & StrSiteReg1Line & vbCrLf
                'StrSubHeader = StrSubHeader & StrSiteReg2Line & vbCrLf

                'Dim kotDataLine As String = String.Empty
                '****Added by Rahul 30 nov 2009 night(Rashid) . start 
                If Type = "DCM" Then
                    'StrCMNo = "Void Cash Memo:" & dtView.Rows(0)("Billno").ToString()
                    StrCMNo = getValueByKey("CLSCMP008") & dtView.Rows(0)("Billno").ToString()
                Else
                    'StrCMNo = "Cash Memo:" & dtView.Rows(0)("Billno").ToString()
                    '---------Added For Font Changes as Requested By Client
                    'StrCMNo = getValueByKey("CLSCMP009") & dtView.Rows(0)("Billno").ToString()
                    StrCMNo = getValueByKey("CLSCMP039") & ":" & dtView.Rows(0)("Billno").ToString()
                    '---------------
                End If
                '******End 

                'StrCMDate = "Date:" & Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())
                'StrCMDate = Format(dtView.Rows(0)("BillTime"), clsCommon.GetSystemDateFormat())
                ''Format of Date changed to DD-MM-YY for Print
                'StrCMDate = dayopenDate.ToShortDateString()
                StrCMDate = dayopenDate.ToString("dd-MM-yyyy")
                strDayOpenDate = dayopenDate.ToShortDateString()
                If DuplicatePrinting <> String.Empty Then
                    StrCMDate = Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())
                End If
                StrTokenNo = dtView.Rows(0)("Billno").ToString().Trim.Substring(dtView.Rows(0)("Billno").ToString().Trim.Length - 2, 2)

                Select Case PrintFormatNo
                    Case 1
                        StrTokenNo = "<B>" & StrTokenNo & "</B>"
                    Case 2

                    Case Else

                End Select
                ' Added For Font Changes as Requested By Client-------------
                ' StrCashMemoLine = StrCMNo.PadRight(LineLength - 1 - StrCMDate.Length) & StrCMDate & vbCrLf
                ' StrCashMemoLine = StrCMNo.PadRight(LineLength - 1 - StrCMDate.Length) & vbCrLf & vbCrLf & "Date" & Space(5) & ":" & StrCMDate & vbCrLf & vbCrLf
                StrCashMemoLine = StrCMNo.PadRight(LineLength - 1 - StrCMDate.Length) & vbCrLf & "Date" & Space(5) & ":" & StrCMDate & vbCrLf
                '--------------------
                'StrCashier = "Cashier:" & dtView.Rows(0)("Createdby").ToString()
                'StrCMTime = "Time:" & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")

                'StrCashier = getValueByKey("CLSPV006") & dtView.Rows(0)("Createdby").ToString()
                '----------Added For Font Changes as Requested By Client
                'StrCashier = getValueByKey("CLSPV006") & LoginUserName
                'StrCMTime = getValueByKey("CLSCMP032") & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")
                StrCashier = getValueByKey("CLSCMP042") & Space(2) & ":" & LoginUserName
                StrCMTime = getValueByKey("CLSCMP041") & Space(5) & ":" & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")
                '----------------
                ' Added For Font Changes as Requested By Client-------------
                'StrCashierLine = StrCashier.PadRight(LineLength - 1 - StrCMTime.Length) & StrCMTime & vbCrLf
                'StrCashierLine = StrCMTime & vbCrLf & vbCrLf & StrCashier & vbCrLf & vbCrLf
                StrCashierLine = StrCMTime & vbCrLf & StrCashier & vbCrLf
                '--------------------
                'StrSalesPerson = "Sales Person:" & dtView.Rows(0)("SALESEXECUTIVECODE").ToString()
                StrSalesPerson = getValueByKey("CLSCMP010") & dtView.Rows(0)("SALESEXECUTIVECODE").ToString()

                If _SalesPerApplicable = True Then
                    StrSalesPersonLine = StrSalesPerson & vbCrLf
                End If

                If (dtView.Rows.Count() > 0 And String.IsNullOrEmpty(DeliveryPersonName)) Then
                    If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                        ' strPrintMsg.Append(StrTokenNo)
                        StrTokenNo = "Token No. " & StrTokenNo
                        If Not AllowBillingOnlyAfterSelectionOfSalesType Then
                            '   strPrintMsg.Append(CustomerSaleType)
                            CustomerSaleType = String.Empty
                        End If
                        Dim StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - (CustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "")).Length) & CustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "") & vbCrLf
                        ''Line Remove if token not required in print
                        ' StrSubHeader = StrSubHeader & strLine
                        ' StrSubHeader = StrSubHeader & StrTokenSalesTypeLine
                    Else
                        If AllowBillingOnlyAfterSelectionOfSalesType Then
                            StrSubHeader = StrSubHeader & CustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "") & vbCrLf
                        End If
                    End If
                Else
                    If AllowBillingOnlyAfterSelectionOfSalesType Then
                        StrSubHeader = StrSubHeader & CustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "") + vbCrLf
                    End If
                End If
                StrSubHeader = StrSubHeader & strLine
                ' Added For Font Changes as Requested By Client-------------
                'StrSubHeader = StrSubHeader & StrCashMemoLine
                'StrSubHeader = StrSubHeader & StrCashierLine
                StrSubHeader = StrSubHeader & "<FontPCKotBillDtl>" & StrCashMemoLine & "</FontPCKotBillDtl>"
                StrSubHeader = StrSubHeader & "<FontPCKotBillDtl>" & StrCashierLine & "<FontPCKotBillDtl>"
                '--------------------

                If (Not String.IsNullOrEmpty(dtView.Rows(0)("SALESEXECUTIVECODE").ToString())) Then
                    'StrSubHeader = StrSubHeader & StrSalesPersonLine
                    StrSubHeader = StrSubHeader & "<FontPCKotBillDtl>" & StrSalesPersonLine & "</FontPCKotBillDtl>"

                    'StrSubHeader = StrSubHeader & strLine
                End If

                Dim CLPaddress As String = ""
                Dim CLPBalancePoints As String
                If dtView.Rows(0)("CLPNO").ToString() <> String.Empty Then
                    'CLpLine = "CLP Card No:" & dtView.Rows(0)("CLPNO").ToString()
                    'CustomerNameLine = "Customer Name: " & dtView.Rows(0)("CLPNAME").ToString()
                    'CLPPointsLine = "CLP Points Earned: " & FormatNumber(dtView.Rows(0)("CLPPoints").ToString())

                    Select Case PrintFormatNo
                        Case 1
                            CLpLine = getValueByKey("CLSCMP011") & dtView.Rows(0)("CLPNO").ToString()
                        Case 2

                        Case Else

                    End Select

                    CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CLPNAME").ToString()
                    CLPPointsLine = getValueByKey("CLSCMP013") & FormatNumber(Val(dtView.Rows(0)("CLPPoints").ToString()))

                    If (dtView.Rows(0)("RedemptionPoints") IsNot DBNull.Value AndAlso dtView.Rows(0)("RedemptionPoints") > 0) Then
                        CLPPointsLine += vbCrLf + getValueByKey("CLSCMP034") & FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()))
                    End If

                    CLPaddress = dtView.Rows(0)("CLPAddress").ToString()
                    If CLPaddress <> String.Empty Then
                        CLPaddress = getValueByKey("CLSCMP014") & dtView.Rows(0)("CLPAddress").ToString()
                    End If
                    CLPBalancePoints = String.Empty
                    CLPBalancePoints = getValueByKey("CLSCMP015")
                    Dim dblCLPPoints As Double

                    dblCLPPoints = Val(dtView.Rows(0)("BalancePoints").ToString())
                    'For Each drRow As DataRow In dtView.Select("Tendertypecode = 'CLPPoint'")
                    '    dblCLPPoints -= drRow("RedemptionPoints")
                    'Next

                    Dim RedemptionPoints As DataRow = dtView.Select("Tendertypecode = 'CLPPoint'").FirstOrDefault()
                    If (RedemptionPoints IsNot Nothing AndAlso RedemptionPoints("RedemptionPoints") IsNot DBNull.Value) Then
                        dblCLPPoints -= Val(RedemptionPoints("RedemptionPoints").ToString())
                    End If


                    CLPBalancePoints = CLPBalancePoints & FormatNumber(dblCLPPoints.ToString())
                    If CLPaddress <> String.Empty Then
                        CLPaddress = SplitString(CLPaddress, LineLength).ToString()
                    End If
                    If CLpLine <> Nothing AndAlso CLpLine.ToString().Length > 0 Then
                        StrSubFooter = StrSubFooter & CLpLine & vbCrLf
                    End If
                    If CustomerSaleType = "Home Delivery" Then
                        CustmerPhoneNo = getValueByKey("RP187") & " " & dtView.Rows(0)("Mobileno").ToString()
                    Else
                        Dim tempMobileno = dtView.Rows(0)("Mobileno").ToString()
                        If dtView.Rows(0)("Mobileno").ToString().Length > 4 Then
                            Dim mobileLast4Digit = tempMobileno.Substring(tempMobileno.Length - 4, 4)
                            Dim mobilemolength = tempMobileno.Length
                            tempMobileno = mobileLast4Digit.PadLeft(tempMobileno.Length, "X")
                        End If
                        CustmerPhoneNo = getValueByKey("RP187") & " " & tempMobileno
                    End If

                    'StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
                    StrSubFooter = StrSubFooter & SplitString(CustomerNameLine).ToString() + vbCrLf
                    StrSubFooter = StrSubFooter & CustmerPhoneNo & vbCrLf
                    StrSubFooter = StrSubFooter & CLPBalancePoints & vbCrLf

                    StrSubFooter = StrSubFooter & CLPPointsLine & vbCrLf
                    'StrSubFooter = StrSubFooter & CLPaddress & vbCrLf '& vbCrLf & vbCrLf

                ElseIf dtView.Columns.Contains("CustomerNo") AndAlso dtView.Rows(0)("CustomerNo").ToString() <> String.Empty Then
                    Select Case PrintFormatNo
                        Case 1
                            CLpLine = getValueByKey("CLSCMP038") & dtView.Rows(0)("CustomerNo").ToString()
                        Case 2

                        Case Else

                    End Select
                    CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CustomerName").ToString()
                    If CLpLine <> Nothing AndAlso CLpLine.ToString().Length > 0 Then
                        StrSubFooter = StrSubFooter & CLpLine & vbCrLf
                    End If
                    StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
                End If

                If Type = "CMWAmt" Then
                    'LineItemHeading = "Item Code" & Space(17) & "Item Name" & Space(1) & "Qty " & vbCrLf
                    ' LineItemHeading = getValueByKey("CLSPSO020")  & vbCrLf & getValueByKey("CLSCMP016") & Space(1) & getValueByKey("CLSPSO022") & " " & vbCrLf
                    'to resolve Issue 448 
                    LineItemHeading = getValueByKey("CLSPSO020") & Space(17) & vbCrLf & getValueByKey("CLSCMP016") & Space(20) & getValueByKey("CLSPSO022") & " " & vbCrLf
                Else
                    'LineItemHeading = "Item Code" & Space(17) & "Item Name" & Space(5) & vbCrLf
                    'to resolve Issue 448 
                    'LineItemHeading = "  Item Code" & vbCrLf & "  Item Name" & vbCrLf
                    'LineItemHeading = LineItemHeading & Space(3) & "Qty " & Space(2) & "Price" & Space(4) & "Disc." & Space(4) & "Tax" & Space(5) & "Net" & vbCrLf

                    'LineItemHeading = "  " & getValueByKey("CLSPSO020") & vbCrLf & "  " & getValueByKey("CLSCMP016") & vbCrLf
                    'LineItemHeading = LineItemHeading & Space(3) & getValueByKey("CLSPSO022") & " " & Space(2) & getValueByKey("CLSPSO023") & Space(4) & getValueByKey("CLSPSO024") & Space(4) & getValueByKey("CLSPSO025") & Space(5) & getValueByKey("CLSPSO026") & vbCrLf
                    ''''''''Added For Font Changes as Requested By Client-------
                    'LineItemHeading = getValueByKey("CLSCMP016") & Space(13) & getValueByKey("CLSPSO022") & " " & Space(5)
                    If Not DtScannedMettlerBills Is Nothing AndAlso DtScannedMettlerBills.Rows.Count > 0 Then
                        LineItemHeading = getValueByKey("CLSCMP016") & Space(3) & getValueByKey("CLSPSO022") & " " & Space(0)
                    Else
                        'commented and some changes for satkar sweets
                        'LineItemHeading = getValueByKey("CLSCMP016") & Space(6) & getValueByKey("CLSPSO022") & " " & Space(2)
                        LineItemHeading = "Desc." & Space(6) & "Rate " & Space(1) & getValueByKey("CLSPSO022") & " " & Space(2)
                    End If
                    Select Case PrintFormatNo
                        Case 1
                            LineItemHeading &= "Amt" & vbCrLf
                        Case 2

                        Case Else

                            If Not DtScannedMettlerBills Is Nothing AndAlso DtScannedMettlerBills.Rows.Count > 0 Then
                                LineItemHeading &= "Amt" & Space(1) & "Scale" & vbCrLf
                            Else
                                LineItemHeading &= "Amt" & vbCrLf
                            End If

                    End Select
                End If

                Dim TotalQty, TGrossAmt, TDiscAmt, TRateAfterDisc, TTaxtAmt As String
                Dim TNetAmt As String = 0.0
                Dim BillLineNO As String
                DtScannedMettlerBills = obj.GetMettlerBillDetailsDataForPrint(CashMemoNo, Sitecode)
                Dim ScaleNo As String
                Dim objItemSch As New clsIteamSearch()
                Dim dtScanedBillArticle As New DataTable
                Dim dtNewArticleScaledtls As New DataTable
                dtNewArticleScaledtls.Columns.Add("Article", System.Type.GetType("System.String"))
                dtNewArticleScaledtls.Columns.Add("ScaleNo", System.Type.GetType("System.String"))
                For ScanBillIndex = 0 To DtScannedMettlerBills.Rows.Count - 1
                    '------- GET Mettler Scanned Bill Details From Mettler DataBase 
                    With DtScannedMettlerBills
                        ScaleNo = Val(.Rows(ScanBillIndex)("SCALE_NO"))
                        dtScanedBillArticle = objItemSch.GetScanedBillArticle(ScaleNo:=ScaleNo, BillNo:=.Rows(ScanBillIndex)("Bill_No"), BillIntDate:=.Rows(ScanBillIndex)("MettlerScaleBillDate"), MettlerConn:=MettlerConnString) '    ScaleBillIntDate = Val(.Rows(ScanBillIndex)("MettlerScaleBillDate"))
                        If dtScanedBillArticle IsNot Nothing Then
                            For Each drscale As DataRow In dtScanedBillArticle.Rows
                                Dim drnew As DataRow = dtNewArticleScaledtls.NewRow
                                drnew("Article") = drscale("LegacyArticleCode")
                                drnew("ScaleNo") = ScaleNo
                                dtNewArticleScaledtls.Rows.Add(drnew)
                            Next
                        End If
                    End With

                Next
                DtUnique.Columns.Add("ScaleNo", System.Type.GetType("System.String"))
                For Each drscaleno As DataRow In dtNewArticleScaledtls.Rows
                    Dim dr() As DataRow = DtUnique.Select("ArticleCode= '" & drscaleno("Article") & "'")
                    If dr.Length > 0 Then
                        dr(0)("ScaleNo") = drscaleno("ScaleNo")
                    End If
                Next
                For Each dr As DataRow In DtUnique.Rows
                    ItemCode = dr("ArticleCode").ToString()

                    If (DisplayArticleFullName) Then
                        Desc = dr("ArticleFullName").ToString()
                    Else
                        Desc = dr("DISCRIPTION").ToString()
                    End If
                    BillLineNO = dr("BillLineNO").ToString()
                    If SpectrumCommon.ComboItemPrintingAllowed AndAlso Dtcombo IsNot Nothing AndAlso Dtcombo.Rows.Count > 0 Then
                        Dim drp() = Dtcombo.Select("ComboArticleCode='" & ItemCode & "' AND BillLineNO ='" & BillLineNO & "'")
                        If drp.Length > 0 Then
                            Dim articleDescriptionDictionary As New Dictionary(Of String, Integer)
                            For index = 0 To drp.Length - 1
                                If articleDescriptionDictionary.ContainsKey(drp(index)("ArticleName")) Then
                                    articleDescriptionDictionary(drp(index)("ArticleName")) = articleDescriptionDictionary(drp(index)("ArticleName")) + drp(index)("Quantity")
                                Else
                                    articleDescriptionDictionary.Add(drp(index)("ArticleName"), drp(index)("Quantity"))
                                End If
                            Next
                            Dim AddedDesc = GetMultilinedString(articleDescriptionDictionary)
                            Desc = Desc & vbCrLf & AddedDesc
                        End If
                    End If
                    FinScaleNo = dr("ScaleNo").ToString()

                    Qty = dr("Quantity").ToString()
                    Rate = dr("SellingPrice").ToString()
                    RateAfterDisc = (dr("SellingPrice") - dr("TOTALDISCOUNT")).ToString()
                    If CDbl(clsCommon.CheckIfBlank(Rate)) < 0 Then
                        Rate = CDbl(clsCommon.CheckIfBlank(Rate)) * -1
                    End If
                    DiscAmt = dr("TOTALDISCOUNT").ToString()
                    DiscPer = dr("TOTALDISCPERCENTAGE").ToString()
                    NetAmt = dr("NetAmount").ToString()
                    GrossAmt = dr("GROSSAMT").ToString()
                    TaxAmt = dr("TOTALTAXAMOUNT").ToString()
                    'Dim i As Integer = 0
                    'For i = ItemCode.Length To 26
                    '    ItemCode = ItemCode & " "
                    'Next
                    'For i = Desc.Length To 14
                    '    Desc = Desc & " "
                    'Next

                    If (AllowDecimalQty) Then
                        If (WeightScaleEnabled) Then
                            Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 3)
                        Else
                            Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 2)
                        End If
                    Else
                        Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 0)
                    End If

                    'For i = Qty.Length To 4
                    '    Qty = " " & Qty
                    'Next
                    Rate = FormatNumber(CDbl(clsCommon.CheckIfBlank(Rate)), 2)

                    RateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(RateAfterDisc)), 2)
                    'Rate = FormatCurrency(Rate, 2)
                    'For i = Rate.Length To 8
                    '    Rate = " " & Rate
                    'Next
                    DiscAmt = FormatNumber(CDbl(IIf(DiscAmt <> String.Empty, DiscAmt, 0)), 2)
                    'For i = DiscAmt.Length To 8
                    '    DiscAmt = " " & DiscAmt
                    'Next
                    DiscPer = FormatNumber(CDbl(IIf(DiscPer <> String.Empty, DiscPer, 0)), 2)
                    DiscPer = DiscPer & "%"
                    'For i = DiscPer.Length To 8
                    '    DiscPer = " " & DiscPer
                    'Next
                    'Format of NetAmt with No Digits after Decimal
                    NetAmt = FormatNumber(CDbl(NetAmt), 0)
                    NetAmtTemp = FormatNumber(CDbl(NetAmt), 2)
                    ' NetAmt = FormatNumber(CDbl(NetAmt), 2)
                    GrossAmt = FormatNumber(CDbl(GrossAmt), 2)
                    'GrossAmt = FormatNumber(CDbl(GrossAmt), 0)
                    'For i = NetAmt.Length To 8
                    '    NetAmt = " " & NetAmt
                    'Next
                    TaxAmt = FormatNumber(CDbl(IIf(TaxAmt <> String.Empty, TaxAmt, 0)), 2)
                    'For i = TaxAmt.Length To 7
                    '    TaxAmt = " " & TaxAmt
                    'Next

                    Dim TempNetAmt As String = "0"

                    If Type = "CMWAmt" Then
                        StrLineItem = ItemCode.PadRight(26) & vbCrLf & Desc.PadRight(35) & Qty & vbCrLf
                    Else
                        If 28 - Desc.Length < Qty.Length Then
                            'commented and change by vipul for satkar sweets
                            'StrLineItem = Desc & vbCrLf
                            StrLineItem = Desc
                            Select Case PrintFormatNo
                                Case 1
                                    StrLineItem = StrLineItem & Qty.PadLeft(28) & GrossAmt.PadLeft(11) & vbCrLf
                                Case 2

                                Case Else
                                    TempNetAmt = NetAmt
                                    If Val(NetAmt) > 0 AndAlso Val(TaxAmt) > 0 Then
                                        TempNetAmt = CDbl(GrossAmt) - CDbl(TaxAmt)

                                    End If
                                    '-------Added For Font Changes as Requested By Client
                                    ' StrLineItem = StrLineItem & Qty.PadLeft(28) & TempNetAmt.PadLeft(11) & vbCrLf
                                    'StrLineItem = SplitString(StrLineItem, BoldLineLength).ToString().Trim & vbCrLf & vbCrLf & Qty.PadLeft(getValueByKey("CLSCMP016").Length + Space(9).Length) & TempNetAmt.PadLeft(getValueByKey("CLSPSO022").Length + Space(4).Length) & vbCrLf & vbCrLf

                                    If Not DtScannedMettlerBills Is Nothing AndAlso DtScannedMettlerBills.Rows.Count > 0 Then
                                        ' StrLineItem = "<FontPCBillItemDesc>" & SplitString(StrLineItem, 13).ToString().Trim & "</FontPCBillItemDesc>" & vbCrLf & vbCrLf & Qty.PadLeft(getValueByKey("CLSCMP016").Length + Space(11).Length) & TempNetAmt.PadLeft(getValueByKey("CLSPSO022").Length + Space(2).Length) & FinScaleNo.PadLeft(getValueByKey("CLSPSO022").Length + Space(6).Length) & vbCrLf & vbCrLf
                                        StrLineItem = "<FontPCBillItemDesc>" & SplitStringPC(StrLineItem, 13).ToString().Trim() & "</FontPCBillItemDesc>" & "<FontPCBillItemDescOther>" & Space(13 - strLastLength) & Qty & Space(CInt(Qty.Length - 3)) & TempNetAmt & Space(6 - TempNetAmt.Length) & FinScaleNo & vbCrLf & vbCrLf & "</FontPCBillItemDescOther>"
                                    Else
                                        Dim newlinelen As Integer = 0
                                        If Qty.Contains("-") Then
                                            newlinelen = 14
                                        Else
                                            newlinelen = 15
                                        End If
                                        ' StrLineItem = "<FontPCBillItemDesc>" & SplitStringPC(StrLineItem, 15).ToString().Trim() & "</FontPCBillItemDesc>" & "<FontPCBillItemDescOther>" & Space(newlinelen - strLastLength) & Qty & Space(CInt(Qty.Length - 1)) & TempNetAmt & Space(6 - TempNetAmt.Length) & vbCrLf & vbCrLf & "</FontPCBillItemDescOther>"
                                        'change for satkar sweets
                                        ' StrLineItem = "<FontPCBillItemDesc>" & SplitStringPC(StrLineItem, 15).ToString().Trim() & "</FontPCBillItemDesc>" & "<FontPCBillItemDescOther>" & Space(newlinelen - IIf(strLastLength > 15, 15, strLastLength)) & Qty & Space(CInt(8 - Qty.Length)) & TempNetAmt & vbCrLf & vbCrLf & "</FontPCBillItemDescOther>"
                                        StrLineItem = SplitStringPC(Desc, 15).ToString().Trim() & Space(newlinelen - IIf(strLastLength > 15, 17, strLastLength + 1)) & Space(IIf(Rate.Length < 8, 8 - Rate.Length, 0)) & Space(1) & Rate & Space(IIf(Qty.Length < 8, 8 - Qty.Length, 0)) & Qty & Space(IIf(TempNetAmt.Length < 8, 8 - TempNetAmt.Length, 0)) & Space(1) & TempNetAmt & vbCrLf
                                    End If
                            End Select
                        Else
                            Select Case PrintFormatNo
                                Case 1
                                    StrLineItem = Desc & Qty.PadLeft(28 - Desc.Length) & GrossAmt.PadLeft(11) & vbCrLf
                                Case 2

                                Case Else
                                    TempNetAmt = NetAmt
                                    If Val(NetAmt) > 0 AndAlso Val(TaxAmt) > 0 Then
                                        TempNetAmt = CDbl(GrossAmt) - CDbl(TaxAmt)
                                    End If

                                    ' Added For Font Changes as Requested By Client-------------
                                    ' StrLineItem = Desc & Qty.PadLeft(28 - Desc.Length) & TempNetAmt.PadLeft(11) & vbCrLf
                                    '      Dim DescBol As String = "<FontPCBillHeader>" & SplitString(Desc, BoldLineLength).ToString().Trim() & "</FontPCBillHeader>"
                                    If Not DtScannedMettlerBills Is Nothing AndAlso DtScannedMettlerBills.Rows.Count > 0 Then
                                        'StrLineItem = "<FontPCBillItemDesc>" & SplitString(Desc, 13).ToString().Trim() & "</FontPCBillItemDesc>" & vbCrLf & vbCrLf & Qty.PadLeft(getValueByKey("CLSCMP016").Length + Space(11).Length) & TempNetAmt.PadLeft(getValueByKey("CLSPSO022").Length + Space(2).Length) & FinScaleNo.PadLeft(getValueByKey("CLSPSO022").Length + Space(6).Length) & vbCrLf & vbCrLf
                                        'commented and changes by vipul for satkar sweets
                                        StrLineItem = "<FontPCBillItemDesc>" & SplitStringPC(Desc, 13).ToString().Trim() & "</FontPCBillItemDesc>" & "<FontPCBillItemDescOther>" & Space(13 - strLastLength) & Qty & Space(CInt(Qty.Length - 3)) & TempNetAmt & Space(5 - TempNetAmt.Length) & FinScaleNo & vbCrLf & vbCrLf & "</FontPCBillItemDescOther>"
                                        'StrLineItem = SplitStringPC(Desc, 13).ToString().Trim() & Space(13 - strLastLength) & Space(IIf(Rate.Length < 8, 8 - Rate.Length, 0)) & Rate & Space(IIf(Qty.Length < 8, 8 - Qty.Length, 0)) & Qty & Space(CInt(Qty.Length - 3)) & TempNetAmt & Space(5 - TempNetAmt.Length) & FinScaleNo & vbCrLf
                                    Else
                                        Dim newlinelen As Integer = 0
                                        If Qty.Contains("-") Then
                                            newlinelen = 14
                                        Else
                                            newlinelen = 15
                                        End If
                                        'commented and changes by vipul for satkar sweets
                                        ' StrLineItem = "<FontPCBillItemDesc>" & SplitStringPC(Desc, 15).ToString().Trim() & "</FontPCBillItemDesc>" & "<FontPCBillItemDescOther>" & Space(newlinelen - IIf(strLastLength > 15, 15, strLastLength)) & Qty & Space(CInt(Qty.Length - 1)) & TempNetAmt & vbCrLf & vbCrLf & "</FontPCBillItemDescOther>"
                                        StrLineItem = SplitStringPC(Desc, 15).ToString().Trim() & Space(newlinelen - IIf(strLastLength > 15, 17, strLastLength + 2)) & Space(IIf(Rate.Length < 8, 8 - Rate.Length, 0)) & Space(1) & Rate & Space(IIf(Qty.Length < 8, 8 - Qty.Length, 0)) & Qty & Space(IIf(TempNetAmt.Length < 8, 8 - TempNetAmt.Length, 0)) & Space(1) & TempNetAmt & vbCrLf
                                    End If

                            End Select
                        End If
                    End If
                    ' Added For Font Changes as Requested By Client-------------
                    ' strLineDetail.Append(StrLineItem)
                    strLineDetail.Append(StrLineItem)
                    '--------------------

                    If CDbl(Qty.Trim) > 0 Then
                        TotalQty = CDbl(clsCommon.CheckIfBlank(TotalQty)) + CDbl(Qty.Trim)
                    End If
                    TDiscAmt = CDbl(clsCommon.CheckIfBlank(TDiscAmt)) + CDbl(DiscAmt.Trim)
                    TGrossAmt = CDbl(clsCommon.CheckIfBlank(TGrossAmt)) + CDbl(dr("GROSSAMT").ToString())
                    'TNetAmt = CDbl(clsCommon.CheckIfBlank(TNetAmt)) + CDbl(NetAmt.Trim)
                    'TNetAmt = Format(Math.Round(Convert.ToDecimal(CDbl(clsCommon.CheckIfBlank(TNetAmt)) + CDbl(NetAmt.Trim))), "0.00")
                    TNetAmt = Decimal.Add(FormatNumber(CDbl(NetAmtTemp), 2).ToString(), TNetAmt)
                    TRateAfterDisc = CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)) + CDbl(RateAfterDisc.Trim)
                    TTaxtAmt = CDbl(clsCommon.CheckIfBlank(TTaxtAmt)) + CDbl(TaxAmt.Trim)
                Next

                TotalQty = FormatNumber(CDbl(clsCommon.CheckIfBlank(TotalQty)), 2)
                'TDiscAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TDiscAmt)), 2)
                TDiscAmt = FormatNumber(dtView.Rows(0)("Discount"), 2)
                TGrossAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TGrossAmt)), 2)
                'TNetAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TNetAmt)), 2)
                TNetAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TNetAmt)), RoundOff), 2)
                TRateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)), 2)
                TTaxtAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TTaxtAmt)), 2)
                '---------Added For Font Changes as Requested By Client
                'StrBody = strLineDetail.ToString() & vbCrLf
                '  StrBody = "<FontPCKotBillDtl>" & strLineDetail.ToString() & "</FontPCKotBillDtl>" & vbCrLf
                StrBody = strLineDetail.ToString()
                '-----------------
                StrBody = StrBody & strLine

                'TotalQtyLine = "Total Qty" & Space(3) & ":" & TotalQty
                'TotalQtyLine = getValueByKey("CLSCMP017") & Space(3) & ":" & TotalQty
                'StrBody = StrBody & TotalQtyLine & vbCrLf
                Dim TSubTotalAmt As String
                If Type <> "CMWAmt" Then
                    If CDbl(clsCommon.CheckIfBlank(TDiscAmt)) > 0 Then
                        Dim TotalDisPercent As String = Math.Round((CDbl(clsCommon.CheckIfBlank(TDiscAmt)) / CDbl(clsCommon.CheckIfBlank(TGrossAmt))) * 100, 1).ToString()
                        ' TSubTotalAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TGrossAmt)) - CDbl(clsCommon.CheckIfBlank(TDiscAmt)))
                        'TSubTotalAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TGrossAmt)) - CDbl(clsCommon.CheckIfBlank(TDiscAmt)), 0)
                        TSubTotalAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TGrossAmt)) - CDbl(clsCommon.CheckIfBlank(TDiscAmt)), RoundOff), 0)
                        '-------Added For Font Changes as Requested By Client
                        TSubTotalAmt = Currency & " " & TSubTotalAmt
                        'TSubTotalAmt = TSubTotalAmt & " " & Currency
                        '----------
                        ' Added For Font Changes as Requested By Client-------------
                        ' SubTotalLine = "Sub Total".PadRight(LineLength - 16) & ":" & TSubTotalAmt.PadLeft(14)
                        'SubTotalLine = "Sub Total" & Space(5) & ":" & TSubTotalAmt.PadLeft(LineLength / 3)
                        SubTotalLine = "Sub Total" & Space(5) & ":" & TSubTotalAmt.PadLeft(BoldLineLength - Val("Sub Total".Length + Space(5).Length))
                        '--------------------
                        '------Added For Font Changes as Requested By Client
                        'TDiscAmt = TDiscAmt & " " & Currency
                        TDiscAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TDiscAmt)), RoundOff), 0)
                        TDiscAmt = Currency & " " & TDiscAmt
                        '-------------------
                        Dim ObjclsCommon As New clsCommon
                        Dim offerno As String = ObjclsCommon.GetOffernoForRoundOff()
                        ' Added For Font Changes as Requested By Client-------------
                        'If DtUnique.Rows(0)("PromotionId").ToString().Contains(offerno) And offerno <> "" Then
                        '    TotalDiscAmtLine = "Round Off".PadRight(LineLength - 16) & ":" & TDiscAmt.PadLeft(14)
                        'Else
                        '    TotalDiscAmtLine = String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).PadRight(LineLength - 16) & ":" & TDiscAmt.PadLeft(14)
                        'End If
                        If DtUnique.Rows(0)("PromotionId").ToString().Contains(offerno) And offerno <> "" Then
                            TotalDiscAmtLine = "Round Off" & Space(BoldLineLength / 2 - "Round Off".Length) & " :" & TDiscAmt.PadLeft(BoldLineLength - ("Round Off".Length + Space(BoldLineLength / 2 - "Round Off".Length).Length + ":".Length))
                        Else
                            ' Dim CheckIfPositiveNumber = IIf(27.5 / 2 - (String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).Length) > 0, 27.5 / 2 - String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).Length, 0)
                            Dim StrstringLength = SplitStringPC(String.Format(getValueByKey("CLSCMP019"), TotalDisPercent), 15)
                            ' TotalDiscAmtLine = String.Format(getValueByKey("CLSCMP019"), TotalDisPercent) & Space(13 - strLastLength) & " :" & TDiscAmt.PadLeft(BoldLineLength - (String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).Length + ": ".Length))
                            TotalDiscAmtLine = String.Format(getValueByKey("CLSCMP019"), TotalDisPercent) & Space(IIf((13 - strLastLength) > 0, (13 - strLastLength), 0)) & " :" & Space(IIf((13 - strLastLength + 5) > 0, (13 - strLastLength + 5), 0)) & TDiscAmt
                        End If
                        '--------------------
                        'TotalDiscAmtLine = String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).PadRight(LineLength - 16) & ":" & TDiscAmt.PadLeft(14)
                    End If
                    ' TGrossAmt = FormatNumber(CDbl(TGrossAmt), 0)
                    TGrossAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TGrossAmt)), RoundOff), 0)
                    '----------Added For Font Changes as Requested By Client
                    'TGrossAmt = TGrossAmt & " " & Currency
                    TGrossAmt = Currency & " " & TGrossAmt
                    '----------------
                    'TNetAmt = FormatNumber(CDbl(TNetAmt), 0)
                    TNetAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TNetAmt)), RoundOff), 0)
                    '------Added For Font Changes as Requested By Client
                    'TNetAmt = TNetAmt & " " & Currency
                    TNetAmt = Currency & " " & TNetAmt
                    '----------
                    TRateAfterDisc = TRateAfterDisc & " " & Currency
                    TTaxtAmt = TTaxtAmt & " " & Currency
                    If ClientName.ToUpper = "hariom".ToUpper Then
                        TotalQtyLine = "Total Qty" & Space(4) & ":" & TotalQty & vbCrLf & vbCrLf
                        StrBody = StrBody & "<FontPCKOTArticle>" & TotalQtyLine & "</FontPCKOTArticle>" '& vbCrLf
                    End If
                    ' Added For Font Changes as Requested By Client-------------
                    ' TotalGrossAmtLine = "Total Amt".PadRight(LineLength - 16) & ":" & TGrossAmt.PadLeft(14)
                    'NetAmtLine = getValueByKey("CLSCMP020").PadRight(LineLength - 16) & ":" & TNetAmt.PadLeft(14)
                    If ClientName.ToUpper = "hariom".ToUpper Then
                        TotalGrossAmtLine = "Total Amt" & Space(4) & ":" & TGrossAmt.PadLeft(BoldLineLength - Val("Total Amt".Length + Space(7).Length)) & vbCrLf
                    Else
                        TotalGrossAmtLine = "Total Amt" & Space(5) & ":" & TGrossAmt.PadLeft(BoldLineLength - Val("Total Amt".Length + Space(5).Length)) & vbCrLf
                    End If

                    If (CDbl(TDiscAmt.Replace("INR", ""))) > 0 Then
                        NetAmtLine = getValueByKey("CLSCMP020") & Space(2) & ":" & TNetAmt.PadLeft(BoldLineLength - Val(getValueByKey("CLSCMP020").Length + Space(2).Length))
                    End If
                    '--------------------
                    If _TaxDetail = True Then
                        Dim taxDetailsForBill As DataTable = obj.GetTaxDetailsForBillNo(Sitecode, dtView.Rows(0)("Billno").ToString())
                        If taxDetailsForBill IsNot Nothing AndAlso taxDetailsForBill.Rows.Count > 0 Then
                            For Each row As DataRow In taxDetailsForBill.Rows
                                'If CompositeTaxReqOnPrint = False AndAlso IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                                If IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                                Dim taxValue As String = row("TaxVAlue").ToString()
                                taxValue = FormatNumber(CDbl(clsCommon.CheckIfBlank(taxValue)), 2)
                                taxValue = Currency & " " & taxValue.ToString
                                FooterTaxOld = SplitStringPCWithNoLastNewLine(row("TaxName"), 21).ToString & Space(20 - strLastLengthN) & ":" & taxValue.PadLeft(18) & vbCrLf
                                FooterTaxLine = FooterTaxLine & FooterTaxOld
                                'FooterTaxLine = FooterTaxLine & row("TaxName").PadRight(LineLength - 16) & ":" & taxValue.PadLeft(14) & vbCrLf
                            Next
                        End If
                    End If
                    TotalFooterTaxDetail = FooterTaxLine

                    ' Added For Font Changes as Requested By Client-------------
                    'StrBody = StrBody & "<FontPCBold>" & TotalGrossAmtLine & "</FontPCBold>" & vbCrLf
                    If ClientName.ToUpper = "hariom".ToUpper Then
                        StrBody = StrBody & "<FontPCKOTArticle>" & TotalGrossAmtLine & "</FontPCKOTArticle>" '& vbCrLf
                    Else
                        StrBody = StrBody & "<FontPCItemDesc>" & TotalGrossAmtLine & "</FontPCItemDesc>" '& vbCrLf
                    End If
                    '--------------------
                    'StrBody = StrBody & TotalGrossAmtLine & vbCrLf
                    ' Added For Font Changes as Requested By Client-------------
                    'If TotalDiscAmtLine IsNot Nothing Then
                    '    StrBody = StrBody & TotalDiscAmtLine & vbCrLf
                    'End If
                    'If SubTotalLine IsNot Nothing Then
                    '    StrBody = StrBody & strLine.Substring(30) & SubTotalLine & vbCrLf
                    'End If
                    If TotalDiscAmtLine IsNot Nothing Then
                        StrBody = StrBody & vbCrLf & "<FontPCItemDesc>" & TotalDiscAmtLine & "</FontPCItemDesc>" & vbCrLf
                    End If
                    If SubTotalLine IsNot Nothing Then
                        '-Added For Font Changes as Requested By Client
                        'StrBody = StrBody & strLine.Substring(30) & "<FontPCBillHeader>" & SubTotalLine & "</FontPCBillHeader>" & vbCrLf
                        StrBody = StrBody & strLine & "<FontPCItemDesc>" & SubTotalLine & "</FontPCItemDesc>" & vbCrLf
                        '-------
                    End If
                    '--------------------
                    ' Added For Font Changes as Requested By Client-------------
                    'StrBody = StrBody & TotalFooterTaxDetail & strLine.Substring(30) & "<FontPCBold>" & NetAmtLine & "</FontPCBold> " & vbCrLf & strLine
                    Dim LineAfterAmt = IIf(CDbl(TDiscAmt.Replace("INR", "")) > 0, "<B>" & strLine.Substring(12) & "</B>", String.Empty)
                    StrBody = StrBody & TotalFooterTaxDetail & LineAfterAmt & "<FontPCItemDesc>" & NetAmtLine & "</FontPCItemDesc> " & vbCrLf

                    If LineAfterAmt <> String.Empty Then
                        StrBody = StrBody & "<B>" & strLine.Substring(12) & "</B>"
                    Else
                        StrBody = StrBody & strLine
                    End If

                    '--------------------
                    'StrBody = StrBody & TotalFooterTaxDetail & strLine.Substring(30) & NetAmtLine & vbCrLf & strLine
                    TenderLine = getValueByKey("CLSCMP021") '& vbCrLf
                    Dim dtTender As DataTable = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                    For Each drTender As DataRow In dtTender.Rows
                        If UCase(obj.GetDefaultConfigValue(Sitecode, "CVInfoReqOnPrint")) = "FALSE" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)") Then
                            Continue For
                        End If
                        'Dim tender As String = FormatNumber(CDbl(clsCommon.CheckIfBlank(drTender("AMOUNTTENDERED").ToString()))) & " " & drTender("CurrencyCode").ToString() & vbCrLf

                        'Dim amounttendered As String = FormatNumber(CDbl(drTender("AMOUNTTENDERED").ToString), 0)
                        Dim amounttendered As String = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(drTender("AMOUNTTENDERED").ToString)), RoundOff), 0)
                        '----------Added For Font Changes as Requested By Client
                        'Dim tender As String = amounttendered & " " & drTender("CurrencyCode").ToString() & vbCrLf
                        Dim tender As String = drTender("CurrencyCode").ToString() & " " & amounttendered & vbCrLf
                        '----------------
                        Dim tenderName As String
                        'Added by Rohit
                        If drTender("CardNO").ToString() <> "" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(I)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(R)") Then
                            tenderName = drTender("TENDERHEADNAME").ToString() & "(" & drTender("CardNO").ToString() & ") "
                        Else
                            tenderName = drTender("TENDERHEADNAME").ToString()
                        End If
                        'End editing
                        If Not drTender("AMOUNTINCURRENCY") Is DBNull.Value AndAlso drTender("AMOUNTINCURRENCY") <> 0 Then '0000404
                            '  tender = FormatNumber(CDbl(drTender("AMOUNTINCURRENCY").ToString()), 2) & " " & drTender("CurrencyCode").ToString() & vbCrLf
                            'tender = FormatNumber(CDbl(drTender("AMOUNTINCURRENCY").ToString()), 0) & " " & drTender("CurrencyCode").ToString() & vbCrLf
                            '----------Added For Font Changes as Requested By Client
                            'tender = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(drTender("AMOUNTINCURRENCY").ToString())), RoundOff), 0) & " " & drTender("CurrencyCode").ToString() & vbCrLf
                            tender = drTender("CurrencyCode").ToString() & " " & FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(drTender("AMOUNTINCURRENCY").ToString())), RoundOff), 0) & vbCrLf
                            '----------------
                        End If
                        'tender = Format(CDbl(tender), "0.00")

                        'TenderDetails = tenderName.PadRight(LineLength - 19) & tender.PadLeft(20) 
                        '--- Code Added By Mahesh changes by on MOD clp changes
                        If drTender("TENDERTYPECODE").ToString() = "CLPPoint" Then
                            tender = FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()), 0)
                        End If

                        'TenderDetails = String.Format("{0}{1}{2}", tenderName.Trim(), Space(LineLength - (tenderName.Trim().Length + tender.Trim().Length)), tender.Trim()) & vbCrLf
                        ' Added For Font Changes as Requested By Client-------------
                        'TenderDetails = String.Format("{0}{1}{2}", tenderName.Trim(), Space(LineLength - (tenderName.Trim().Length + tender.Trim().Length)), "<FontPCBold>" & tender.Trim() & "</FontPCBold>") & vbCrLf
                        'TenderLine = TenderLine & TenderDetails
                        'TenderDetails = String.Format("{0}{1}{2}", tenderName.Trim(), Space(LineLength / 2 - tenderName.Length), tender.Trim()) & vbCrLf
                        ' TenderDetails = String.Format("{0}{1}{2}", tenderName.Trim(), Space(BoldLineLength - (tenderName.Trim().Length + tender.Trim().Length)), tender.Trim()) & vbCrLf
                        ' TenderDetails = tenderName & Space(2) & tender.PadLeft(BoldLineLength - Val(tenderName.Length + Space(2).Length))
                        '  "Total Amt" & Space(5) & ":" & TGrossAmt.PadLeft(BoldLineLength - Val("Total Amt".Length + Space(5).Length))
                        TenderDetails = tenderName & Space(BoldLineLength / 2 - tenderName.Length) & ": " & tender.PadLeft(BoldLineLength - Val(tenderName.Length + Val(BoldLineLength / 2 - tenderName.Length)))
                        TenderLine = TenderLine & vbCrLf & TenderDetails '& vbCrLf
                        '--------------------
                    Next
                    '--Added For Font Changes as Requested By Client
                    'StrBody = StrBody & TenderLine
                    StrBody = StrBody & "<FontPCItemDesc>" & TenderLine & "</FontPCItemDesc>" & strLine
                    '-------------
                    'Rakesh-13.09.2013:KSL CR-7860--> Display total discount amount 
                    If (_IsDisplayTotalSaving) Then
                        If (Not String.IsNullOrEmpty(TDiscAmt) AndAlso Not String.Equals(TDiscAmt, "0.00")) Then
                            Dim totalSavingAmount As String = String.Format(getValueByKey("CLSCMP033"), Currency, TDiscAmt.Replace(Currency, String.Empty).Trim)
                            StrBody = StrBody & vbCrLf & SplitString(totalSavingAmount).ToString().Trim & vbCrLf & strLine & vbCrLf
                        Else
                            StrBody = StrBody & vbCrLf
                        End If
                    Else
                        ' StrBody = StrBody & vbCrLf
                    End If

                End If
                ' Added For Font Changes as Requested By Client-------------
                Dim VatTanString As String = "VAT/TIN NO: 27120029370 U/V"
                Dim LbtString As String = "LBT NO: TMC-LBT0005578-13"
                Dim msgString As String = "THANK YOU .. VISIT AGAIN"
                '--------------------
                'Company and Tin Info
                Dim NoOfReprints As String = IIf(IsDBNull(dtView.Rows(0)("NoofReprints")), String.Empty, dtView.Rows(0)("NoofReprints")).ToString()
                Dim CompanyName As String = dtView.Rows(0)("SITEOFFICIALNAME") 'obj.GetDefaultConfigValue(Sitecode, "CompanyName")

                StrCompanyInfo = String.Empty
                'StrCompanyInfo.PadLeft(LineLength / 2 + (StrCompanyInfo.Length / 2))
                If Not String.IsNullOrEmpty(CompanyName) Then
                    ' StrCompanyInfo = CompanyName & vbCrLf
                    StrCompanyInfo = CompanyName.PadLeft(LineLength / 2 + (CompanyName.Length / 2)) & vbCrLf
                End If

                Dim strtna = getValueByKey("cashmemoprint.tin") & obj.GetTinNumberForASite(Sitecode)
                strtna = SplitString(strtna, LineLength).ToString() & vbCrLf
                ' strtna = SplitStringCenterAlign(strtna, LineLength).ToString() & vbCrLf
                If Not String.IsNullOrEmpty(strtna) Then
                    StrCompanyInfo += strtna.PadLeft(LineLength / 2 + (strtna.Length / 2))
                    'strtna.TrimEnd() & vbCrLf
                End If

                If (Not String.IsNullOrEmpty(StrTagLine)) Then
                    StrCompanyInfo += StrTagLine.TrimEnd() & vbCrLf
                End If

                'Dim remarks As String = dtView.Rows(0)("Remark")
                If Not IsDBNull(dtView.Rows(0)("Remark")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("Remark")) Then
                    Dim remarks As String = dtView.Rows(0)("Remark")
                    remarks = SplitString(getValueByKey("cashmemoprint.discountremark") & remarks, LineLength).ToString()
                    StrCompanyInfo = StrCompanyInfo & remarks.TrimEnd() & vbCrLf
                End If

                If Not String.IsNullOrEmpty(NoOfReprints) Then
                    StrCompanyInfo = StrCompanyInfo & "Re-Print Number : " & NoOfReprints & vbCrLf
                    StrCompanyInfo = StrCompanyInfo & "Re-Print Date : " & DateTime.Now.ToShortDateString() & vbCrLf
                End If
                'Company and Tin Info End

                Dim dtPromo As DataTable = dvItemDetail.ToTable(True, "PROMOTIONALMSGID", "PROMOMESS")
                For Each drTerms As DataRow In dtPromo.Select("", "PROMOTIONALMSGID", DataViewRowState.CurrentRows)
                    If Not String.IsNullOrEmpty(drTerms("PROMOMESS").ToString()) Then
                        PromoMsgLine = PromoMsgLine & drTerms("PROMOMESS").ToString() & vbCrLf
                    End If
                Next

                TotalPromotionalMsg = PromoMsgLine

                If Not IsSavoy Then
                    Dim dtTerms As DataTable = dvItemDetail.ToTable(True, "TNCSRNO", "Terms")
                    For Each drTerms As DataRow In dtTerms.Select("", "TNCSRNO", DataViewRowState.CurrentRows)
                        If Not String.IsNullOrEmpty(drTerms("Terms").ToString()) Then
                            'Added for word wrap
                            '--------Added For Font Changes as Requested By Client
                            'TermsNcond = TermsNcond & SplitString(drTerms("Terms").ToString(), LineLength).ToString
                            TermsNcond = TermsNcond & SplitString(drTerms("Terms").ToString(), BoldLineLength).ToString
                        End If
                    Next
                End If


                'If dtView.Rows.Count() > 0 Then
                '    If dtView.Rows(0)("HDName").ToString() <> String.Empty Then
                '        strHomeDilery = "Delivery Date:-" & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & "Name:-" & dtView.Rows(0)("HDName").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & "Address:-" & dtView.Rows(0)("HDAddress").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & "TelNo:-" & dtView.Rows(0)("HDTelNo").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & "Email:-" & dtView.Rows(0)("HDEmail").ToString() & vbCrLf
                '    End If
                'End If
                strPrintMsg = New StringBuilder()
                sbDeliveryBillPrint = New StringBuilder()

                If dtView.Rows.Count() > 0 Then
                    '---- Changes By Mahesh : Customer Name is Not Mandatory for Home Delivery 
                    'If dtView.Rows(0)("HDName").ToString() <> String.Empty Then
                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP022") & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf
                        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP023") & dtView.Rows(0)("HDName").ToString() & vbCrLf
                        strHomeDilery = strHomeDilery & SplitString(getValueByKey("CLSCMP024") & dtView.Rows(0)("HDAddress").ToString(), LineLength).ToString() & vbCrLf
                        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP025") & dtView.Rows(0)("HDTelNo").ToString() & vbCrLf
                        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP026") & dtView.Rows(0)("HDEmail").ToString() & vbCrLf

                    Else
                        If _CustomerNameRequiredInKOT AndAlso CustomerNameLine <> Nothing Then
                            StrCustomerName = CustomerNameLine.ToUpper.Replace("CUSTOMER", "").Trim()

                        End If
                        If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                            If Not AllowBillingOnlyAfterSelectionOfSalesType Then
                                CustomerSaleType = String.Empty
                            End If
                            Dim StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - CustomerSaleType.Length) & CustomerSaleType & vbCrLf & vbCrLf
                        End If
                    End If
                End If
                StrFooter = StrFooter & TotalPromotionalMsg '& vbCrLf
                ' Added For Font Changes as Requested By Client-------------
                'StrFooter = StrFooter & TermsNcond '& vbCrLf
                StrFooter = StrFooter & "<FontPCKotBillDtl>" & TermsNcond & "</FontPCKotBillDtl>" '& vbCrLf
                '--------------------
                'strPrintMsg.Append("<FontPCKotHeader>" & "PRASHANT CORNER".PadLeft((Val(LineLength - "PRASHANT CORNER".Length) + "PRASHANT CORNER".Length) / 2) & "</FontPCKotHeader>" & vbCrLf & vbCrLf)
                '--------Added For Font Changes as Requested By Client
                ' strPrintMsg.Append("<FontPCKotHeader>" & "PRASHANT CORNER".PadLeft(CInt((LineLength - "PRASHANT CORNER".Length) - 3), " ") & "</FontPCKotHeader>" & vbCrLf & vbCrLf)
                '---Comment for changes Clientname generic
                'strPrintMsg.Append("<FontPCKotHeader>" & "PRASHANT CORNER".PadLeft(Val(BoldLineLength - "PRASHANT CORNER".Length) + "PRASHANT CORNER".Length / 2 + 3) & "</FontPCKotHeader>" & vbCrLf & vbCrLf)
                strPrintMsg.Append("<CustomFontHeader>" & ClientName.ToUpper.PadLeft((BoldLineLength + ClientName.Length) / 2) & "</CustomFontHeader>" & vbCrLf & vbCrLf)
                sbDeliveryBillPrint.Append("<CustomFontHeader>" & ClientName.ToUpper.PadLeft((BoldLineLength + ClientName.Length) / 2) & "</CustomFontHeader>" & vbCrLf & vbCrLf)
                '-----------------

                strPrintMsg.Append(StrHeader)
                '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                '---------Added For Font Changes as Requested By Client
                'sbDeliveryBillPrint.Append("<FontPCKotHeader>" & "PRASHANT CORNER".PadLeft(CInt((LineLength - "PRASHANT CORNER".Length) - 3), " ") & "</FontPCKotHeader>" & vbCrLf & vbCrLf)
                'sbDeliveryBillPrint.Append("<FontPCKotHeader>" & "PRASHANT CORNER".PadLeft(Val(BoldLineLength - "PRASHANT CORNER".Length) + "PRASHANT CORNER".Length / 2 + 3) & "</FontPCKotHeader>" & vbCrLf & vbCrLf)
                '---------------
                'sbDeliveryBillPrint.Append("<FontPCKotHeader>" & "PRASHANT CORNER".PadLeft((Val(LineLength - "PRASHANT CORNER".Length) + "PRASHANT CORNER".Length) / 2) & "</FontPCKotHeader>" & vbCrLf & vbCrLf)
                sbDeliveryBillPrint.Append(StrHeader)
                'strPrintMsg.Append(strLine)
                ' Added For Font Changes as Requested By Client-------------
                ' strPrintMsg.Append(vbCrLf)
                strPrintMsg.Append("<FontPCKOTArticle>" & "FINAL RECEIPT".PadLeft((Val(BoldLineLength - "FINAL RECEIPT".Length) + "FINAL RECEIPT".Length) / 2 + 7) & "</FontPCKOTArticle>" & vbCrLf)
                strPrintMsg.Append(vbCrLf)
                sbDeliveryBillPrint.Append(vbCrLf)
                sbDeliveryBillPrint.Append("<FontPCKOTArticle>" & "FINAL RECEIPT".PadLeft((Val(BoldLineLength - "FINAL RECEIPT".Length) + "FINAL RECEIPT".Length) / 2 + 7) & "</FontPCKOTArticle>" & vbCrLf)
                sbDeliveryBillPrint.Append(vbCrLf)
                '--------------------
                If StrSubHeader IsNot Nothing AndAlso Not String.IsNullOrEmpty(StrSubHeader.ToString()) Then
                    strPrintMsg.Append(StrSubHeader)
                    '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                    sbDeliveryBillPrint.Append(StrSubHeader)
                End If

                If (Not String.IsNullOrEmpty(strWelcomeMsg.ToString())) Then
                    strPrintMsg.Append(strLineL40 + vbCrLf)
                    strPrintMsg.Append(strWelcomeMsg.ToString())
                    '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                    sbDeliveryBillPrint.Append(strLineL40 + vbCrLf)
                    sbDeliveryBillPrint.Append(strWelcomeMsg.ToString())
                End If

                '---- Added By Mahesh maintaining seperate DeliveryBillPrint

                sbDeliveryBillPrint.Append(strDblLine)
                strPrintMsg.Append(strDblLine)
                '-Added For Font Changes as Requested By Client
                'strPrintMsg.Append(LineItemHeading)
                strPrintMsg.Append("<FontPCBillFooter>" & LineItemHeading & "</FontPCBillFooter>")
                '--------
                strPrintMsg.Append(strDblLine)
                strPrintMsg.Append(StrBody)

                '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                '----------Added For Font Changes as Requested By Client
                'sbDeliveryBillPrint.Append(LineItemHeading)
                sbDeliveryBillPrint.Append("<FontPCBillFooter>" & LineItemHeading & "</FontPCBillFooter>")
                '----------------------
                sbDeliveryBillPrint.Append(strDblLine)
                sbDeliveryBillPrint.Append(StrBody)
                'strPrintMsg.Append(StrCompanyInfo)
                'Centre Align for TIN NO

                ' Added For Font Changes as Requested By Client-------------
                'strPrintMsg.Append(StrCompanyInfo.PadLeft(LineLength / 2 + (StrCompanyInfo.Length / 2)))
                ' strPrintMsg.Append("<FontPCKotBillDtl>" & Space(1) & VatTanString.PadLeft((Val(LineLength - VatTanString.Length) + VatTanString.Length) / 2) & "</FontPCKotBillDtl>" & vbCrLf)
                ' strPrintMsg.Append("<FontPCKotBillDtl>" & Space(2) & LbtString.PadLeft((Val(LineLength - LbtString.Length) + LbtString.Length) / 2) & "</FontPCKotBillDtl>" & vbCrLf)
                'strPrintMsg.Append("<FontPCKotBillDtl>" & Space(2) & msgString.PadLeft((Val(LineLength - msgString.Length) + msgString.Length) / 2) & "</FontPCKotBillDtl>" & vbCrLf)
                ' sbDeliveryBillPrint.Append("<FontPCKotBillDtl>" & Space(1) & VatTanString.PadLeft((Val(LineLength - VatTanString.Length) + VatTanString.Length) / 2) & "</FontPCKotBillDtl>" & vbCrLf)
                'sbDeliveryBillPrint.Append("<FontPCKotBillDtl>" & Space(2) & LbtString.PadLeft((Val(LineLength - LbtString.Length) + LbtString.Length) / 2) & "</FontPCKotBillDtl>" & vbCrLf)
                'sbDeliveryBillPrint.Append("<FontPCKotBillDtl>" & vbCrLf & Space(2) & msgString.PadLeft((Val(LineLength - msgString.Length) + msgString.Length) / 2) & "</FontPCKotBillDtl>" & vbCrLf)
                ' sbDeliveryBillPrint.Append("<FontPCKotBillDtl>" & Space(2) & msgString.PadLeft((Val(LineLength - msgString.Length) + msgString.Length) / 2) & "</FontPCKotBillDtl>" & vbCrLf)

                Dim dtPrint As New DataTable
                Dim objPrint As New SpectrumBL.clsCommon
                dtPrint = objPrint.GetPrintingDetails(Type)
                If Not dtPrint Is Nothing Then
                    Dim filter As String = ""
                    Dim dv As New DataView(dtPrint, filter, "Sequenceno", DataViewRowState.CurrentRows)
                    filter = "TOPBOTTOM='Bottom'"
                    dv.RowFilter = filter
                    For Each drview As DataRowView In dv
                        ' StrPrintFooterLine = drview("ReceiptText").ToString()
                        '  StrFooter = StrFooter & StrPrintFooterLine & vbNewLine
                        If drview("ReceiptText").ToString().Length >= 25 Then
                            'commented and change by vipul for satkar sweets 
                            'strPrintMsg.Append("<FontPCKotBillDtl>" & Space(1) & drview("ReceiptText").ToString().PadLeft((Val(LineLength - drview("ReceiptText").ToString().Length) + drview("ReceiptText").ToString().Length) / 2) & "</FontPCKotBillDtl>" & vbCrLf & vbCrLf)
                            strPrintMsg.Append("<FontPCKotBillDtl>" & Space(1) & drview("ReceiptText").ToString().PadLeft((Val(LineLength - drview("ReceiptText").ToString().Length) + drview("ReceiptText").ToString().Length) / 2) & "</FontPCKotBillDtl>" & vbCrLf)
                        Else
                            'changes for satkar sweets
                            'strPrintMsg.Append("<FontPCKotBillDtl>" & Space(2) & drview("ReceiptText").ToString().PadLeft((Val(LineLength - drview("ReceiptText").ToString().Length) + drview("ReceiptText").ToString().Length) / 2) & "</FontPCKotBillDtl>" & vbCrLf & vbCrLf)
                            strPrintMsg.Append("<FontPCKotBillDtl>" & Space(2) & drview("ReceiptText").ToString().PadLeft((Val(LineLength - drview("ReceiptText").ToString().Length) + drview("ReceiptText").ToString().Length) / 2) & "</FontPCKotBillDtl>" & vbCrLf)
                        End If

                        'strPrintMsg.Append("<FontPCKotBillDtl>" & drview("ReceiptText").ToString().PadLeft(24) & "</FontPCKotBillDtl>" & vbCrLf & vbCrLf)
                    Next

                End If



                '--------------------
                strPrintMsg.Append(strLine)

                If strTaxInformation IsNot Nothing AndAlso Not String.IsNullOrEmpty(strTaxInformation.ToString()) Then
                    strPrintMsg.Append(strTaxInformation.ToString().TrimEnd())
                    strPrintMsg.Append(vbCrLf + strLineL40 + vbCrLf)
                End If

                If strPromotionMsg IsNot Nothing AndAlso Not String.IsNullOrEmpty(strPromotionMsg.ToString()) Then
                    strPrintMsg.Append(strPromotionMsg.ToString().TrimEnd())
                    strPrintMsg.Append(vbCrLf + strLineL40 + vbCrLf)
                End If

                If StrSubFooter IsNot Nothing AndAlso Not String.IsNullOrEmpty(StrSubFooter.ToString()) Then
                    strPrintMsg.Append(StrSubFooter.ToString())
                End If

                strPrintMsg.Append(StrFooter)

                If (Not String.IsNullOrEmpty(strHomeDilery)) Then
                    strPrintMsg.Append(strLineL40 & vbCrLf)
                    strPrintMsg.Append(strHomeDilery)

                    ' ---- Added By Mahesh maintaining seperate DeliveryBillPrint
                    '--- Changes By Mahesh Not required in HD Print
                    'sbDeliveryBillPrint.Append(vbCrLf)
                    'sbDeliveryBillPrint.Append(strHomeDilery)



                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        'sbDeliveryBillPrint.Append(strPrintMsg)
                        'sbDeliveryBillPrint.Append(strLineL40 & vbCrLf)
                        '--- Changes By Mahesh Not required in HD Print
                        'sbDeliveryBillPrint.Append(getValueByKey("CLSCMP035") & DeliveryPersonName & vbCrLf)
                    End If
                End If

                If GiftMsg <> String.Empty Then
                    GiftMsg = SplitString(GiftMsg).ToString()
                    strPrintMsg.Append(GiftMsg.TrimEnd() & vbCrLf)
                    strPrintMsg.Append(strLine)

                    sbDeliveryBillPrint.Append(GiftMsg.TrimEnd() & vbCrLf & strLine)
                End If

                PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")

                If PrinterName = Nothing Then
                    Exit Sub
                End If
                Dim msg As String = String.Empty
                If Not IsDBNull(dtView.Rows(0)("ServiceTaxAmount")) AndAlso Val(dtView.Rows(0)("ServiceTaxAmount")) > 0 Then
                    CustomerSaleType = GetCustomerSaleType(Convert.ToInt16(dtView.Rows(0)("ServiceTaxAmount")))
                Else
                    CustomerSaleType = ""
                End If
                If _PrintPreview = True Then

                    'If (KOTBillPrintingRequired) Then
                    '    If IsDintInEnabled AndAlso CustomerSaleType = "Dine In" Then
                    '    Else
                    '        Call CashMemoKOTPrintBasedOnPrintFormatNo(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, dayopenDate)
                    '    End If
                    'End If
                    If (KOTBillPrintingRequired) Then
                        If IsCounterCopyKot = False Then
                            Call CashMemoKOTPrintBasedOnPrintFormatNo(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, dayopenDate, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy)
                        End If
                    End If
                   
                    'PrinterName = "HP LaserJet P3005 PCL6"
                    PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                    If IsFinalReceipt Then
                        msg = fnPrint(strPrintMsg.ToString(), "PRV", 1)
                    End If
                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        msg = fnPrint(sbDeliveryBillPrint.ToString(), "PRV")
                    End If
                Else
                    'If (KOTBillPrintingRequired) Then
                    '    If IsDintInEnabled AndAlso CustomerSaleType = "Dine In" Then
                    '    Else
                    '        Call CashMemoKOTPrintBasedOnPrintFormatNo(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, dayopenDate)
                    '    End If
                    'End If
                    If (KOTBillPrintingRequired) Then
                        If IsCounterCopyKot = False Then
                            Call CashMemoKOTPrintBasedOnPrintFormatNo(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, dayopenDate, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy)
                        End If
                    End If
                   

                    strPrintMsg.Append(vbCrLf & vbCrLf & vbCrLf & vbCrLf)
                    PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                    If IsFinalReceipt Then
                        msg = fnPrint(strPrintMsg.ToString(), "PRN", 1, NoOfCopies:=NoOfCopies)
                    End If

                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)

                        msg = fnPrint(sbDeliveryBillPrint.ToString(), "PRN")
                    End If

                End If
                If Not String.IsNullOrEmpty(msg) Then
                    errorMsg = msg
                End If
                'Added for fiscal printer
                Try
                    clsFiscalPrinting.fnFiscalPrint(strPrintMsg.ToString())
                Catch ex As Exception

                End Try
                'Added for fiscal printer

            Else
                errorMsg = getValueByKey("CLSCMP031")
            End If
        Catch ex As Exception
            errorMsg = ex.Message
        End Try
    End Sub


    Public Sub CashMemoPrintFormat_SSRS(ByVal dayopenDate As Date, ByVal LangCode As String, ByVal Currency As String, ByVal Sitecode As String, ByVal Type As String, ByVal DuplicatePrinting As String, ByVal DeletedUserid As String, ByVal AuthorisedUser As String, ByRef errorMsg As String, Optional ByVal GiftMsg As String = "", Optional ByVal BillAmt As String = "", Optional ByVal PaidAmt As String = "", Optional ByVal IsSavoy As Boolean = False, Optional ByVal MettlerConn As String = "", Optional ByVal IsArticleWiseKot As Boolean = False, Optional ByVal IsCounterCopy As Boolean = False, Optional ByVal IsFinalReceipt As Boolean = False, Optional ByVal ClientName As String = "", Optional ByVal DayCloseReportPath As String = "", Optional ByVal IsCounterCopyKot As Boolean = False, Optional ByVal KOTAndBillGeneration As Boolean = False, Optional ByVal EvasPizzaChanges As Boolean = False, Optional ByVal NoOfCopies As Integer = 1, Optional ByVal isratevisible As Boolean = False, Optional ByVal IsTendersVisibleInPrintFormat7 As Boolean = False, Optional ByVal IsHsnAndTaxVisibleInPrintFormat6 As Boolean = False, Optional ByVal TokenNoRequiredInKOT As Boolean = False)   'Code added by irfan on 15/9/2017 for  IsTendersVisibleInPrintFormat7 and IsHsnAndTaxVisibleInPrintFormat6
        Try
            Dim strPrintMsg, sbDeliveryBillPrint As StringBuilder
            Dim strLineDetail As New StringBuilder
            Dim StrHeader, StrSubHeader, StrBody, StrCompanyInfo, StrTagLine, StrSubFooter, StrFooter, StrDuplicate, StrDeleteLine, StrCashMemoLine, StrCashierLine, StrSalesPersonLine As String
            Dim StrTokenNo, StrCustomerName As String
            Dim StrCMNo, StrCMDate, StrCMTime, StrCashier, StrSalesPerson As String
            Dim SiteLine, StrSiteTelline, StrAdrressLine, StrSiteReg1Line, StrSiteReg2Line As String
            Dim CLpLine, LineItemHeading, StrLineItem As String
            Dim CustomerNameLine, CLPPointsLine, CustmerPhoneNo As String
            Dim ItemCode, Desc, Qty, Rate, DiscAmt, DiscPer, TaxAmt, NetAmt, NetAmtTemp, RateAfterDisc, GrossAmt, FinScaleNo As String
            Dim TotalQtyLine, TakeAwayQuantity, TotalGrossAmtLine, TotalDiscAmtLine, SubTotalLine, NetAmtLine, TotalTaxAmtLine As String
            Dim TotalFooterTaxDetail, FooterTaxLine As String
            Dim TenderDetails, TenderLine As String
            Dim TotalPromotionalMsg, PromoMsgLine, StrPrintHeaderLine, StrPrintFooterLine As String
            Dim TermsNcond As String
            Dim strLine As String
            Dim strDblLine As String
            Dim strHomeDilery As String
            Dim isTakeAwayQtyPrint As Boolean = False
            errorMsg = ""
            Dim dtView As New DataTable
            Dim BoldLineLength As Int32 = 27
            'added by khusrao adil on 10-07-2017
            Dim objcom As New clsCommon
            Dim IsPosPassKey As Boolean = objcom.GetCLPProgram()
            Dim obj As New SpectrumBL.clsCashMemo()
            dtView = obj.GetCashMemo(CashMemoNo, Sitecode, LangCode)

            Dim dvItemDetail As New DataView(dtView, "", "", DataViewRowState.CurrentRows)
            'Dim DtUnique As DataTable = dvItemDetail.ToTable(True, "ArticleCode", "DISCRIPTION", "QUANTITY", "SELLINGPRICE", "GROSSAMT", "TOTALDISCOUNT", "TOTALDISCPERCENTAGE", "NETAMOUNT", "TOTALTAXAMOUNT")
            ' Dim DtUnique As DataTable = obj.GetBillDetailsDataForPrint(CashMemoNo, Sitecode, LangCode, True)
            Dim DtUnique As DataTable = obj.GetBillDetailsDataForPrint(CashMemoNo, Sitecode, LangCode, True, Type)
            Dim Dtcombo As DataTable = obj.GetComboDetailsDataForPrint(CashMemoNo, Sitecode, LangCode)
            If Not IsDBNull(dtView.Rows(0)("ServiceTaxAmount")) AndAlso Val(dtView.Rows(0)("ServiceTaxAmount")) > 0 Then
                CustomerSaleType = GetCustomerSaleType(dtView.Rows(0)("ServiceTaxAmount"))
            Else
                CustomerSaleType = ""
            End If
            If Not IsDBNull(dtView.Rows(0)("DeliveryPersonID")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("DeliveryPersonID")) Then
                DeliveryPersonName = dtView.Rows(0)("DeliveryPersonID").ToString()
            End If
            If DtUnique Is Nothing Then Exit Sub
            If dtView Is Nothing Then Exit Sub
            StrDuplicate = DuplicatePrinting
            'StrDeleteLine = DeletedUserid
            Dim SiteName As String
            SiteName = dtView.Rows(0)("SITEOFFICIALNAME").ToString()
            If ClientName <> "" Then
                Dim arrClientname = ClientName.Split(" ").ToArray()

                For i = 0 To arrClientname.Length - 1
                    If SiteName.ToUpper.Contains(arrClientname(i).ToString.ToUpper) Then
                        SiteName = SiteName.ToUpper.Replace(arrClientname(i).ToString.ToUpper.Trim, "").Trim
                    End If
                Next
            End If
            SiteName = SiteName.PadLeft((Val(LineLength - SiteName.Length) + SiteName.Length) / 2)
            '------------------
            SiteLine = SiteName.Trim
            Dim PhoneLine As String = dtView.Rows(0)("TELNO").ToString()
            Dim addressLine1 As String = dtView.Rows(0)("ADDRESSLINE1").ToString().Trim
            Dim addressLine2 As String = dtView.Rows(0)("ADDRESSLINE2").ToString().Trim
            Dim addressLine3 As String = dtView.Rows(0)("ADDRESSPINCODE").ToString().Trim
            Dim CustomerGST As String = dtView.Rows(0)("orkutid").ToString().Trim 'Jayesh 25/03/2019
            If Type = "DCM" Then
                StrCMNo = getValueByKey("CLSCMP008") & dtView.Rows(0)("Billno").ToString()
            Else
                StrCMNo = getValueByKey("CLSCMP039") & ":" & dtView.Rows(0)("Billno").ToString()
                '---------------
            End If
            StrCMDate = dayopenDate.ToString("dd-MM-yyyy")
            strDayOpenDate = dayopenDate.ToShortDateString()
            If DuplicatePrinting <> String.Empty Then
                StrCMDate = Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())
            End If
            StrSalesPerson = getValueByKey("CLSCMP010") & dtView.Rows(0)("SALESEXECUTIVECODE").ToString()


            Dim CLPaddress As String = ""
            Dim CLPBalancePoints As String
            Dim clpaddressnew As String = ""
            Dim customername As String = ""
            Dim customeRGSTIN As String = ""   'Jayesh 25/03/2019
            Dim customerPhoneno As String = ""
            Dim DeliveryName As String = ""
            Dim DeliveryDate As DateTime
            Dim ReminderComments As String = ""
            If dtView.Rows(0)("CLPNO").ToString() <> String.Empty Then
                'CLpLine = "CLP Card No:" & dtView.Rows(0)("CLPNO").ToString()
                'CustomerNameLine = "Customer Name: " & dtView.Rows(0)("CLPNAME").ToString()
                'CLPPointsLine = "CLP Points Earned: " & FormatNumber(dtView.Rows(0)("CLPPoints").ToString())

                CLpLine = getValueByKey("CLSCMP011") & dtView.Rows(0)("CLPNO").ToString()

                CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CLPNAME").ToString()
                customername = dtView.Rows(0)("CLPNAME").ToString().Trim
                clpaddressnew = dtView.Rows(0)("CLPAddress").ToString()
                clpaddressnew = clpaddressnew.Trim
                customeRGSTIN = dtView.Rows(0)("orkutid").ToString() 'Jayesh 25/03/2019
                CLPPointsLine = getValueByKey("CLSCMP013") & FormatNumber(Val(dtView.Rows(0)("CLPPoints").ToString()))
                customerPhoneno = dtView.Rows(0)("MobileNo").ToString()
                If dtView.Rows.Count() > 0 Then
                    '---- Changes By Mahesh : Customer Name is Not Mandatory for Home Delivery 
                    'If dtView.Rows(0)("HDName").ToString() <> String.Empty Then
                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        DeliveryDate = dtView.Rows(0)("HDDeliveryDate").ToString()
                        'strHomeDilery = strHomeDilery & getValueByKey("CLSCMP023") & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf
                        DeliveryName = DeliveryPersonName.Trim
                    End If
                End If
                If (dtView.Rows(0)("RedemptionPoints") IsNot DBNull.Value AndAlso dtView.Rows(0)("RedemptionPoints") > 0) Then
                    CLPPointsLine += vbCrLf + getValueByKey("CLSCMP034") & FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()))
                End If

                'CLPaddress = dtView.Rows(0)("CLPAddress").ToString()
                'If CLPaddress <> String.Empty Then
                '    CLPaddress = getValueByKey("CLSCMP014") & dtView.Rows(0)("CLPAddress").ToString()
                'End If


                If PrintFormatNo = 6 Then

                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then 'vipul
                        CLPaddress = dtView.Rows(0)("HDAddress").ToString()
                    Else
                        CLPaddress = dtView.Rows(0)("CLPAddress").ToString()
                    End If
                    If CLPaddress <> String.Empty Then
                        If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                            CLPaddress = getValueByKey("CLSCMP014") & dtView.Rows(0)("HDAddress").ToString()
                        Else
                            CLPaddress = getValueByKey("CLSCMP014") & dtView.Rows(0)("CLPAddress").ToString()
                        End If
                    End If
                    clpaddressnew = CLPaddress
                Else
                    CLPaddress = dtView.Rows(0)("CLPAddress").ToString()
                    If CLPaddress <> String.Empty Then
                        CLPaddress = getValueByKey("CLSCMP014") & dtView.Rows(0)("CLPAddress").ToString()
                    End If

                End If


                ReminderComments = dtView.Rows(0)("ReminderComments").ToString()
                CLPBalancePoints = String.Empty
                CLPBalancePoints = getValueByKey("CLSCMP015")
                Dim dblCLPPoints As Double

                dblCLPPoints = Val(dtView.Rows(0)("BalancePoints").ToString())
                'For Each drRow As DataRow In dtView.Select("Tendertypecode = 'CLPPoint'")
                '    dblCLPPoints -= drRow("RedemptionPoints")
                'Next

                Dim RedemptionPoints As DataRow = dtView.Select("Tendertypecode = 'CLPPoint'").FirstOrDefault()
                If (RedemptionPoints IsNot Nothing AndAlso RedemptionPoints("RedemptionPoints") IsNot DBNull.Value) Then
                    dblCLPPoints -= Val(RedemptionPoints("RedemptionPoints").ToString())
                End If

                CLPBalancePoints = CLPBalancePoints & FormatNumber(dblCLPPoints.ToString())
                If CLPaddress <> String.Empty Then
                    CLPaddress = SplitString(CLPaddress, LineLength).ToString()
                End If
                'If CLpLine <> Nothing AndAlso CLpLine.ToString().Length > 0 Then ' 3 lines commented for clp details
                '    StrSubFooter = StrSubFooter & CLpLine & vbCrLf
                'End If
                CustmerPhoneNo = getValueByKey("RP187") & " " & dtView.Rows(0)("Mobileno").ToString()
                StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
                'StrSubFooter = StrSubFooter & CLPBalancePoints & vbCrLf ''' 2 lines commented for clp details
                If CustomerSaleType = "Home Delivery" Then
                    StrSubFooter = StrSubFooter & CustmerPhoneNo & vbCrLf
                End If

                'StrSubFooter = StrSubFooter & CustmerPhoneNo & vbCrLf
                'StrSubFooter = StrSubFooter & CLPPointsLine & vbCrLf


            ElseIf dtView.Columns.Contains("CustomerNo") AndAlso dtView.Rows(0)("CustomerNo").ToString() <> String.Empty Then

                CLpLine = getValueByKey("CLSCMP038") & dtView.Rows(0)("CustomerNo").ToString()

                CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CustomerName").ToString()
                If CLpLine <> Nothing AndAlso CLpLine.ToString().Length > 0 Then
                    StrSubFooter = StrSubFooter & CLpLine & vbCrLf
                End If
                StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
            End If


            Dim TotalQty, TGrossAmt, TDiscAmt, TRateAfterDisc, TTaxtAmt As String
            Dim TNetAmt As String = 0.0
            Dim BillLineNO As String
            '  DtScannedMettlerBills = obj.GetMettlerBillDetailsDataForPrint(CashMemoNo, Sitecode)
            If SpectrumAsMettler Then
                DtScannedMettlerBills = obj.GetSpectrumMettlerBillDetailsDataForPrint(CashMemoNo, Sitecode)
            Else
                DtScannedMettlerBills = obj.GetMettlerBillDetailsDataForPrint(CashMemoNo, Sitecode)
            End If
            Dim ScaleNo As String
            Dim objItemSch As New clsIteamSearch()
            Dim dtScanedBillArticle As New DataTable
            Dim dtNewArticleScaledtls As New DataTable
            Dim dtTender As DataTable 'sagar
            dtNewArticleScaledtls.Columns.Add("Article", System.Type.GetType("System.String"))
            dtNewArticleScaledtls.Columns.Add("ScaleNo", System.Type.GetType("System.String"))
            For ScanBillIndex = 0 To DtScannedMettlerBills.Rows.Count - 1
                '------- GET Mettler Scanned Bill Details From Mettler DataBase 
                With DtScannedMettlerBills
                    ScaleNo = Val(.Rows(ScanBillIndex)("SCALE_NO"))
                    'dtScanedBillArticle = objItemSch.GetScanedBillArticle(ScaleNo:=ScaleNo, BillNo:=.Rows(ScanBillIndex)("Bill_No"), BillIntDate:=.Rows(ScanBillIndex)("MettlerScaleBillDate"), MettlerConn:=MettlerConnString) '    ScaleBillIntDate = Val(.Rows(ScanBillIndex)("MettlerScaleBillDate"))
                    If SpectrumAsMettler Then 'vipiul for new Spectrum-Mettler changes
                        ScaleNo = DtScannedMettlerBills.Rows(ScanBillIndex)("SCALE_NO").ToString.Remove(0, 1)
                        dtScanedBillArticle = objItemSch.GetScanedBillArticleForSpectrumMettler(BillNo:=.Rows(ScanBillIndex)("Bill_No")) '    ScaleBillIntDate = Val(.Rows(ScanBillIndex)("MettlerScaleBillDate"))
                    Else
                        dtScanedBillArticle = objItemSch.GetScanedBillArticle(ScaleNo:=ScaleNo, BillNo:=.Rows(ScanBillIndex)("Bill_No"), BillIntDate:=.Rows(ScanBillIndex)("MettlerScaleBillDate"), MettlerConn:=MettlerConnString) '    ScaleBillIntDate = Val(.Rows(ScanBillIndex)("MettlerScaleBillDate"))
                    End If
                    If dtScanedBillArticle IsNot Nothing Then

                        For Each drscale As DataRow In dtScanedBillArticle.Rows
                            Dim drnew As DataRow = dtNewArticleScaledtls.NewRow
                            drnew("Article") = drscale("LegacyArticleCode")
                            drnew("ScaleNo") = ScaleNo
                            dtNewArticleScaledtls.Rows.Add(drnew)
                        Next

                    End If
                End With

            Next
            DtUnique.Columns.Add("ScaleNo", System.Type.GetType("System.Int32"))
            For Each drscaleno As DataRow In dtNewArticleScaledtls.Rows
                Dim dr() As DataRow = DtUnique.Select("ArticleCode= '" & drscaleno("Article") & "'")
                If dr.Length > 0 Then
                    dr(0)("ScaleNo") = drscaleno("ScaleNo")
                End If
            Next
            If Not dtNewArticleScaledtls Is Nothing Then
                If dtNewArticleScaledtls.Rows.Count = 0 Then
                    For Each druniq As DataRow In DtUnique.Rows
                        druniq("ScaleNo") = 0
                    Next
                Else
                    For Each druniq As DataRow In DtUnique.Rows
                        If Convert.ToString(druniq("ScaleNo")) = "" Then
                            druniq("ScaleNo") = 0
                        End If
                    Next
                End If
            End If
            For Each dr As DataRow In DtUnique.Rows
                ItemCode = dr("ArticleCode").ToString()

                If (DisplayArticleFullName) Then
                    Desc = dr("ArticleFullName").ToString()
                Else
                    Desc = dr("DISCRIPTION").ToString()
                End If
                BillLineNO = dr("BillLineNO").ToString()
                If SpectrumCommon.ComboItemPrintingAllowed AndAlso Dtcombo IsNot Nothing AndAlso Dtcombo.Rows.Count > 0 Then
                    Dim drp() = Dtcombo.Select("ComboArticleCode='" & ItemCode & "' AND BillLineNO ='" & BillLineNO & "'")
                    If drp.Length > 0 Then
                        Dim articleDescriptionDictionary As New Dictionary(Of String, Integer)
                        For index = 0 To drp.Length - 1
                            If articleDescriptionDictionary.ContainsKey(drp(index)("ArticleName")) Then
                                articleDescriptionDictionary(drp(index)("ArticleName")) = articleDescriptionDictionary(drp(index)("ArticleName")) + drp(index)("Quantity")
                            Else
                                articleDescriptionDictionary.Add(drp(index)("ArticleName"), drp(index)("Quantity"))
                            End If
                        Next
                        Dim AddedDesc = GetMultilinedString(articleDescriptionDictionary)
                        Desc = Desc & vbCrLf & AddedDesc
                    End If
                End If
                FinScaleNo = dr("ScaleNo").ToString()

                Qty = dr("Quantity").ToString()
                Rate = dr("SellingPrice").ToString()
                RateAfterDisc = (dr("SellingPrice") - dr("TOTALDISCOUNT")).ToString()
                If CDbl(clsCommon.CheckIfBlank(Rate)) < 0 Then
                    Rate = CDbl(clsCommon.CheckIfBlank(Rate)) * -1
                End If
                DiscAmt = dr("TOTALDISCOUNT").ToString()
                DiscPer = dr("TOTALDISCPERCENTAGE").ToString()
                NetAmt = dr("NetAmount").ToString()
                GrossAmt = dr("GROSSAMT").ToString()
                TaxAmt = dr("TOTALTAXAMOUNT").ToString()


                If (AllowDecimalQty) Then
                    If (WeightScaleEnabled) Then
                        Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 3)
                    Else
                        Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 2)
                    End If
                Else
                    Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 0)
                End If

                'For i = Qty.Length To 4
                '    Qty = " " & Qty
                'Next
                Rate = FormatNumber(CDbl(clsCommon.CheckIfBlank(Rate)), 2)

                RateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(RateAfterDisc)), 2)
                'Rate = FormatCurrency(Rate, 2)
                'For i = Rate.Length To 8
                '    Rate = " " & Rate
                'Next
                DiscAmt = FormatNumber(CDbl(IIf(DiscAmt <> String.Empty, DiscAmt, 0)), 2)
                'For i = DiscAmt.Length To 8
                '    DiscAmt = " " & DiscAmt
                'Next
                DiscPer = FormatNumber(CDbl(IIf(DiscPer <> String.Empty, DiscPer, 0)), 2)
                DiscPer = DiscPer & "%"
                'For i = DiscPer.Length To 8
                '    DiscPer = " " & DiscPer
                'Next
                'Format of NetAmt with No Digits after Decimal
                NetAmt = FormatNumber(CDbl(NetAmt), 0)
                NetAmtTemp = FormatNumber(CDbl(NetAmt), 2)
                ' NetAmt = FormatNumber(CDbl(NetAmt), 2)
                GrossAmt = FormatNumber(CDbl(GrossAmt), 2)
                'GrossAmt = FormatNumber(CDbl(GrossAmt), 0)
                'For i = NetAmt.Length To 8
                '    NetAmt = " " & NetAmt
                'Next
                TaxAmt = FormatNumber(CDbl(IIf(TaxAmt <> String.Empty, TaxAmt, 0)), 2)
                'For i = TaxAmt.Length To 7
                '    TaxAmt = " " & TaxAmt
                'Next

                Dim TempNetAmt As String = "0"

                If Type = "CMWAmt" Then
                Else
                    If 28 - Desc.Length < Qty.Length Then
                        Select Case PrintFormatNo
                            Case 1
                            Case 2

                            Case Else
                                TempNetAmt = NetAmt
                                If Val(NetAmt) > 0 AndAlso Val(TaxAmt) > 0 Then
                                    TempNetAmt = CDbl(GrossAmt) - CDbl(TaxAmt)

                                End If
                        End Select
                    Else
                        Select Case PrintFormatNo
                            Case 1

                            Case 2

                            Case Else
                                TempNetAmt = NetAmt
                                If Val(NetAmt) > 0 AndAlso Val(TaxAmt) > 0 Then
                                    TempNetAmt = CDbl(GrossAmt) - CDbl(TaxAmt)
                                End If
                        End Select
                    End If
                End If
                ' Added For Font Changes as Requested By Client-------------
                ' strLineDetail.Append(StrLineItem)
                strLineDetail.Append(StrLineItem)
                '--------------------

                If CDbl(Qty.Trim) > 0 Then
                    TotalQty = CDbl(clsCommon.CheckIfBlank(TotalQty)) + CDbl(Qty.Trim)
                End If
                TDiscAmt = CDbl(clsCommon.CheckIfBlank(TDiscAmt)) + CDbl(DiscAmt.Trim)
                TGrossAmt = CDbl(clsCommon.CheckIfBlank(TGrossAmt)) + CDbl(dr("GROSSAMT").ToString())
                'TNetAmt = CDbl(clsCommon.CheckIfBlank(TNetAmt)) + CDbl(NetAmt.Trim)
                'TNetAmt = Format(Math.Round(Convert.ToDecimal(CDbl(clsCommon.CheckIfBlank(TNetAmt)) + CDbl(NetAmt.Trim))), "0.00")
                TNetAmt = Decimal.Add(FormatNumber(CDbl(NetAmtTemp), 2).ToString(), TNetAmt)
                TRateAfterDisc = CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)) + CDbl(RateAfterDisc.Trim)
                TTaxtAmt = CDbl(clsCommon.CheckIfBlank(TTaxtAmt)) + CDbl(TaxAmt.Trim)
            Next

            TotalQty = FormatNumber(CDbl(clsCommon.CheckIfBlank(TotalQty)), 2)
            TDiscAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TDiscAmt)), 2)
            ' TDiscAmt = FormatNumber(dtView.Rows(0)("Discount"), 2)
            TGrossAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TGrossAmt)), 2)
            'TNetAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TNetAmt)), 2)
            TNetAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TNetAmt)), RoundOff), 2)
            TRateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)), 2)
            TTaxtAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TTaxtAmt)), 2)
            Dim TSubTotalAmt As String
            Dim TotalDisPercent As String
            If Type <> "CMWAmt" Then
                If CDbl(clsCommon.CheckIfBlank(TDiscAmt)) > 0 Then
                    TotalDisPercent = Math.Round((CDbl(clsCommon.CheckIfBlank(TDiscAmt)) / CDbl(clsCommon.CheckIfBlank(TGrossAmt))) * 100, 1).ToString()
                    TSubTotalAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TGrossAmt)) - CDbl(clsCommon.CheckIfBlank(TDiscAmt)), RoundOff), 0)
                    TDiscAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TDiscAmt)), RoundOff), 0)
                    Dim ObjclsCommon As New clsCommon
                    Dim offerno As String = ObjclsCommon.GetOffernoForRoundOff()
                    If DtUnique.Rows(0)("PromotionId").ToString().Contains(offerno) And offerno <> "" Then
                        ' TotalDiscAmtLine = "Round Off" & Space(BoldLineLength / 2 - "Round Off".Length) & " :" & TDiscAmt.PadLeft(BoldLineLength - ("Round Off".Length + Space(BoldLineLength / 2 - "Round Off".Length).Length + ":".Length))
                    Else
                        '  Dim CheckIfPositiveNumber = IIf(BoldLineLength / 2 - (String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).Length) > 0, BoldLineLength / 2 - String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).Length, 0)
                        '  TotalDiscAmtLine = String.Format(getValueByKey("CLSCMP019"), TotalDisPercent) & Space(CheckIfPositiveNumber) & " :" & TDiscAmt.PadLeft(BoldLineLength - (String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).Length + ": ".Length))
                    End If
                    '--------------------
                End If

                TGrossAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TGrossAmt)), RoundOff), 0)
                TNetAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TNetAmt)), RoundOff), 0)
                TRateAfterDisc = TRateAfterDisc & " " & Currency
                TTaxtAmt = TTaxtAmt & " " & Currency
                TotalGrossAmtLine = "Total Amt" & Space(5) & ":" & TGrossAmt.PadLeft(BoldLineLength - Val("Total Amt".Length + Space(5).Length))
                If (CDbl(TDiscAmt.Replace("INR", ""))) > 0 Then
                    NetAmtLine = getValueByKey("CLSCMP020") & Space(2) & ":" & TNetAmt.PadLeft(BoldLineLength - Val(getValueByKey("CLSCMP020").Length + Space(2).Length))
                End If
                '--------------------

                Dim taxDetailsForBill As DataTable
                'this code is also use for print format no 6 done by ashma
                If _TaxDetail = True Then
                    taxDetailsForBill = obj.GetTaxDetailsForBillNo(Sitecode, dtView.Rows(0)("Billno").ToString())
                    If taxDetailsForBill IsNot Nothing AndAlso taxDetailsForBill.Rows.Count > 0 Then
                        For Each row As DataRow In taxDetailsForBill.Rows
                            'If CompositeTaxReqOnPrint = False AndAlso IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                            If IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                            Dim taxValue As String = row("TaxVAlue").ToString()
                            taxValue = FormatNumber(CDbl(clsCommon.CheckIfBlank(taxValue)), 2)
                            taxValue = taxValue & " " & Currency
                            FooterTaxLine = FooterTaxLine & row("TaxName").PadRight(LineLength - 16) & ":" & taxValue.PadLeft(14) & vbCrLf
                        Next
                    End If
                End If
                TotalFooterTaxDetail = FooterTaxLine
                If KOTAndBillGeneration = True Then
                    dtTender = obj.GetTenderTypeByRefBillNo(dtView.Rows(0)("BILLNO").ToString())
                    If dtTender.Rows.Count = 0 Then
                        dtTender = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                    End If
                Else
                    dtTender = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                End If
                ' dtTender = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                For Each drTender As DataRow In dtTender.Rows
                    If UCase(obj.GetDefaultConfigValue(Sitecode, "CVInfoReqOnPrint")) = "FALSE" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)") Then
                        Continue For
                    End If
                    Dim amounttendered As String = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(drTender("AMOUNTTENDERED").ToString)), RoundOff), 0)
                    Dim tender As String = drTender("CurrencyCode").ToString() & " " & amounttendered & vbCrLf
                    '----------------
                    Dim tenderName As String
                    'Added by Rohit
                    If drTender("CardNO").ToString() <> "" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(I)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(R)") Then
                        tenderName = drTender("TENDERHEADNAME").ToString() & "(" & drTender("CardNO").ToString() & ") "
                    Else
                        tenderName = drTender("TENDERHEADNAME").ToString()
                    End If
                    'End editing
                    If Not drTender("AMOUNTINCURRENCY") Is DBNull.Value AndAlso drTender("AMOUNTINCURRENCY") <> 0 Then '0000404
                        tender = drTender("CurrencyCode").ToString() & " " & FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(drTender("AMOUNTINCURRENCY").ToString())), RoundOff), 0) & vbCrLf
                    End If
                    If drTender("TENDERTYPECODE").ToString() = "CLPPoint" Then
                        tender = FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()), 0)
                    End If

                Next

                If (_IsDisplayTotalSaving) Then
                    If (Not String.IsNullOrEmpty(TDiscAmt) AndAlso Not String.Equals(TDiscAmt, "0.00")) Then
                        Dim totalSavingAmount As String = String.Format(getValueByKey("CLSCMP033"), Currency, TDiscAmt.Replace(Currency, String.Empty).Trim)
                    Else
                    End If
                Else
                End If


                '--------------------
                'Company and Tin Info
                Dim NoOfReprints As String = IIf(IsDBNull(dtView.Rows(0)("NoofReprints")), String.Empty, dtView.Rows(0)("NoofReprints")).ToString()
                Dim CompanyName As String = dtView.Rows(0)("SITEOFFICIALNAME") 'obj.GetDefaultConfigValue(Sitecode, "CompanyName")
                ''Dim remarks As String = dtView.Rows(0)("Remark")
                'If Not IsDBNull(dtView.Rows(0)("Remark")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("Remark")) Then
                '    Dim remarks As String = dtView.Rows(0)("Remark")
                '    remarks = SplitString(getValueByKey("cashmemoprint.discountremark") & remarks, LineLength).ToString()
                '    StrCompanyInfo = StrCompanyInfo & remarks.TrimEnd() & vbCrLf
                'End If

                'If Not String.IsNullOrEmpty(NoOfReprints) Then
                '    StrCompanyInfo = StrCompanyInfo & "Re-Print Number : " & NoOfReprints & vbCrLf
                '    StrCompanyInfo = StrCompanyInfo & "Re-Print Date : " & DateTime.Now.ToShortDateString() & vbCrLf
                'End If
                'Company and Tin Info End

                Dim dtPromo As DataTable = dvItemDetail.ToTable(True, "PROMOTIONALMSGID", "PROMOMESS")
                For Each drTerms As DataRow In dtPromo.Select("", "PROMOTIONALMSGID", DataViewRowState.CurrentRows)
                    If Not String.IsNullOrEmpty(drTerms("PROMOMESS").ToString()) Then
                        PromoMsgLine = PromoMsgLine & drTerms("PROMOMESS").ToString() & vbCrLf
                    End If
                Next

                TotalPromotionalMsg = PromoMsgLine
                strPrintMsg = New StringBuilder()
                sbDeliveryBillPrint = New StringBuilder()

                'If dtView.Rows.Count() > 0 Then
                '    '---- Changes By Mahesh : Customer Name is Not Mandatory for Home Delivery 
                '    'If dtView.Rows(0)("HDName").ToString() <> String.Empty Then
                '    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                '        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP022") & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP023") & dtView.Rows(0)("HDName").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & SplitString(getValueByKey("CLSCMP024") & dtView.Rows(0)("HDAddress").ToString(), LineLength).ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP025") & dtView.Rows(0)("HDTelNo").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP026") & dtView.Rows(0)("HDEmail").ToString() & vbCrLf

                '    Else
                '        If _CustomerNameRequiredInKOT AndAlso CustomerNameLine <> Nothing Then
                '            StrCustomerName = CustomerNameLine.ToUpper.Replace("CUSTOMER", "").Trim()

                '        End If
                '        If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                '            If Not AllowBillingOnlyAfterSelectionOfSalesType Then
                '                CustomerSaleType = String.Empty
                '            End If
                '            Dim StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - CustomerSaleType.Length) & CustomerSaleType & vbCrLf & vbCrLf
                '        End If
                '    End If
                'End If
                Dim dtPrint As New DataTable
                Dim objPrint As New SpectrumBL.clsCommon
                dtPrint = objPrint.GetPrintingDetails(Type)

                'If Not dtPrint Is Nothing Then
                '    Dim filter As String = ""
                '    Dim dv As New DataView(dtPrint, filter, "Sequenceno", DataViewRowState.CurrentRows)
                '    filter = "TOPBOTTOM='Bottom'"
                '    dv.RowFilter = filter
                '    For Each drview As DataRowView In dv
                '        If drview("ReceiptText").ToString().Length >= 25 Then
                '        Else
                '        End If
                '    Next

                'End If
                'added by khusrao adil on 12-07-2017 all print format change BillNo ,tax invocie
                Dim TopBottomDisplayValue As String = "FINAL RECEIPT"
                If Not dtPrint Is Nothing Then
                    Dim filter As String = ""
                    Dim dv As New DataView(dtPrint, filter, "Sequenceno", DataViewRowState.CurrentRows)
                    filter = "TOPBOTTOM='Top'"
                    dv.RowFilter = filter
                    For Each drview As DataRowView In dv
                        'If drview("ReceiptText").ToString().Length >= 25 Then
                        '    Dim a = drview("ReceiptText").ToString()
                        'Else
                        'End If
                        If drview("ReceiptText").ToString().Length >= 4 Then
                            TopBottomDisplayValue = drview("ReceiptText").ToString()
                        Else
                        End If
                    Next
                End If
                'added by khusrao to print return amount on bill
                If (dtView.Rows.Count() > 0 And String.IsNullOrEmpty(DeliveryPersonName)) Then
                    If Val(PaidAmt) > 0 Then 'PaidAmt
                        Dim retAmt As Double
                        If CDbl(PaidAmt) >= CDbl(BillAmt) Then
                            retAmt = CDbl(PaidAmt) - CDbl(BillAmt)
                        End If
                        If retAmt <> 0 Then
                            Dim returnAmtFooter = vbCrLf & getValueByKey("CLIST04") & " " & FormatNumber(CDbl(PaidAmt), 2) & vbCrLf 'PaidAmt
                            returnAmtFooter = returnAmtFooter & getValueByKey("CLIST05") & " " & FormatNumber(CDbl(retAmt), 2) & vbCrLf 'retAmt
                            Dim dr = dtPrint.NewRow
                            If dtPrint.Rows.Count <> 0 Then
                                dr("TOPBOTTOM") = "RETURNAMTFOOTER"
                                dr("SEQUENCENO") = dtPrint.Rows(dtPrint.Rows.Count - 1)("SEQUENCENO") + 1
                                dr("RECEIPTTEXT") = returnAmtFooter.Trim
                                dr("ALIGN") = dtPrint.Rows(dtPrint.Rows.Count - 1)("ALIGN")
                                dr("WIDTH") = dtPrint.Rows(dtPrint.Rows.Count - 1)("WIDTH")
                                dr("HEIGHT") = dtPrint.Rows(dtPrint.Rows.Count - 1)("HEIGHT")
                                dr("BOLD") = dtPrint.Rows(dtPrint.Rows.Count - 1)("BOLD")
                            Else
                                dr("TOPBOTTOM") = "RETURNAMTFOOTER"
                                dr("SEQUENCENO") = dtPrint.Rows.Count + 1
                                dr("RECEIPTTEXT") = returnAmtFooter.Trim
                                dr("ALIGN") = "Left"
                                'dr("WIDTH") = dtPrint.Rows(0)("WIDTH")
                                'dr("HEIGHT") = dtPrint.Rows(0)("HEIGHT")
                                'dr("BOLD") = dtPrint.Rows(0)("BOLD")
                            End If
                            dtPrint.Rows.Add(dr)
                        End If

                    End If

                End If
                'added by khusrao adil on 04-07-2017 for GST No on bill print 
                Dim GstNo = obj.GetTinNumberForASite(Sitecode)
                Dim GstNoDisplay As String = ""
                If (Not String.IsNullOrEmpty(GstNo)) AndAlso GstNo <> "0" Then
                    Dim strtna = "GST No.: " & GstNo
                    strtna = SplitString(strtna, LineLength).ToString() & vbCrLf
                    If Not String.IsNullOrEmpty(strtna) Then
                        GstNoDisplay += strtna.Trim()
                    End If
                End If
                'added by sagar
                If DtUnique.Rows.Count > 0 Then
                    For index = 0 To DtUnique.Rows.Count - 1
                        'DtUnique(index)("GROSSAMT") = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(DtUnique(index)("GROSSAMT"))), RoundOff), 0)
                        If PrintFormatNo = 7 Then

                        Else
                            ' DtUnique(index)("GROSSAMT") = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(DtUnique(index)("GROSSAMT"))), RoundOff), 0)
                            DtUnique(index)("GROSSAMT") = FormatNumber(CDbl(DtUnique(index)("GROSSAMT")), 2)
                        End If
                        If Not String.IsNullOrEmpty(GstNoDisplay) Then
                            DtUnique(index)("GSTNo") = GstNoDisplay
                        End If
                    Next
                    DtUnique.AcceptChanges()
                End If

                'added by khusrao adil on 06-07-2017 for print Token no on print format 6
                If PrintFormatNo = 6 OrElse PrintFormatNo = 7 Then
                    If PrintCouponNumberForKOT = True Then
                        'Dim CouponNo As String = strTillno.Substring(0, 1) & strTillno.Substring(strTillno.Length - 2, 2) & strBillno.Substring(strBillno.Length - 2, 2)
                        Dim tillNo As String = dtView.Rows(0)("TerminalId").ToString()
                        Dim billno As String = dtView.Rows(0)("Billno").ToString()
                        Dim CouponNo As String = tillNo.Substring(0, 1) & tillNo.Substring(tillNo.Length - 2, 2) & billno.Substring(billno.Length - 2, 2)
                        StrTokenNo = CouponNo
                    Else
                        StrTokenNo = dtView.Rows(0)("Billno").ToString().Trim.Substring(dtView.Rows(0)("Billno").ToString().Trim.Length - 2, 2)
                    End If

                    ' StrTokenNo = "<B>" & StrTokenNo & "</B>"
                    '--------------------------------------------------------------------------------------------------
                    If (dtView.Rows.Count() > 0 And String.IsNullOrEmpty(DeliveryPersonName)) Then
                        If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                            ' strPrintMsg.Append(StrTokenNo)
                            '  StrTokenNo = "Token No. " & StrTokenNo
                            If Not AllowBillingOnlyAfterSelectionOfSalesType Then
                                '   strPrintMsg.Append(CustomerSaleType)
                                CustomerSaleType = String.Empty
                            End If
                        Else
                            'If AllowBillingOnlyAfterSelectionOfSalesType Then
                            '    StrSubHeader = StrSubHeader & CustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "") & vbCrLf
                            'Else
                            '    StrSubHeader = StrSubHeader & CustomerSaleType + vbCrLf
                            'End If
                        End If
                    Else
                        If AllowBillingOnlyAfterSelectionOfSalesType Then
                            StrSubHeader = StrSubHeader & CustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "") + vbCrLf
                        Else
                            StrSubHeader = StrSubHeader & CustomerSaleType + vbCrLf
                        End If
                    End If
                    '-------------------------------------------------------------------
                End If
                Dim dtHeaderDetails As New DataTable
                dtHeaderDetails.Columns.Add("Header", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("Footer", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("ClientName", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("INR", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("StoreName", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("Address", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("PhoneNo", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("FINALRECEIPT", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("TotalAmt", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("LessDisPer", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("LessDisAmt", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("SubTotal", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("TotalToPay", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("CashierName", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("CustomerName", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("CustomerAddress", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("CustomerPhoneNo", System.Type.GetType("System.String"))
                If PrintFormatNo = 6 Then
                    dtHeaderDetails.Columns.Add("CustomerGSTNO", System.Type.GetType("System.String")) 'Jayesh 25/03/2019
                End If
                dtHeaderDetails.Columns.Add("DeliveryDate", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("DeliveryName", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("SalesType", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("TokenNo", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("DeliveryNote", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("isDeliveryCopy", System.Type.GetType("System.Boolean"))
                dtHeaderDetails.Columns.Add("IsRoundRequired", System.Type.GetType("System.Boolean"))
                'added by khusrao adil for balance point display    IsRoundRequired
                'dtHeaderDetails.Columns.Add("IsPOSPasskey", System.Type.GetType("System.Boolean"))
                dtHeaderDetails.Columns.Add("CustomerRemark", System.Type.GetType("System.String"))

                If PrintFormatNo = 6 OrElse PrintFormatNo = 7 Then
                    dtHeaderDetails.Columns.Add("IsRateVisible", System.Type.GetType("System.Boolean"))
                    dtHeaderDetails.Columns.Add("IsPOSPasskey", System.Type.GetType("System.Boolean"))
                    'code added by irfan on 11/09/2017 visiblity of hsn and tax
                    If PrintFormatNo = 6 Then
                        dtHeaderDetails.Columns.Add("IsHsnAndTaxVisibleInPrintFormat6", System.Type.GetType("System.Boolean"))
                    End If
                    'Code added by irfan on 8/9/2017 against Tender visiblity
                    If PrintFormatNo = 7 Then
                        dtHeaderDetails.Columns.Add("IsTendersVisibleInPrintFormat7", System.Type.GetType("System.Boolean"))
                    End If
                    '--------------------------------------------------------
                End If
                If StrDuplicate <> "" Then
                    dtHeaderDetails.Columns.Add("DuplicateBill", System.Type.GetType("System.String"))
                End If
                Dim drnewS = dtHeaderDetails.NewRow
                drnewS("Header") = "Submit Counter Copy at Scale"
                drnewS("Footer") = "BILL PAID"
                drnewS("ClientName") = ClientName.Trim
                drnewS("INR") = 0
                drnewS("StoreName") = SiteName.Trim
                drnewS("Address") = addressLine1.Trim & Space(1) & addressLine2.Trim & Space(1) & addressLine3.Trim
                drnewS("PhoneNo") = PhoneLine.Trim
                drnewS("FINALRECEIPT") = TopBottomDisplayValue
                drnewS("TotalAmt") = TGrossAmt.Trim
                drnewS("IsRoundRequired") = IsRoundRequired
                If TotalDisPercent Is Nothing Then
                    drnewS("LessDisPer") = 0
                Else
                    drnewS("LessDisPer") = TotalDisPercent
                End If

                drnewS("LessDisAmt") = TDiscAmt
                If TSubTotalAmt Is Nothing Then
                    drnewS("SubTotal") = 0
                Else
                    drnewS("SubTotal") = TSubTotalAmt
                End If

                drnewS("TotalToPay") = TNetAmt
                drnewS("CashierName") = LoginUserName
                drnewS("CustomerName") = customername
                'added by khusrao adil on 10-07-2017 
                If CustomerSaleType = "Home Delivery" Then
                    CustmerPhoneNo = getValueByKey("RP187") & " " & dtView.Rows(0)("Mobileno").ToString()
                Else
                    CustmerPhoneNo = getValueByKey("RP187") & " " & customerPhoneno
                    Dim tempMobileno = dtView.Rows(0)("Mobileno").ToString()
                    If dtView.Rows(0)("Mobileno").ToString().Length > 4 Then
                        Dim mobileLast4Digit = tempMobileno.Substring(customerPhoneno.Length - 4, 4)
                        Dim mobilemolength = customerPhoneno.Length
                        customerPhoneno = mobileLast4Digit.PadLeft(customerPhoneno.Length, "X")
                    End If
                    CustmerPhoneNo = getValueByKey("RP187") & " " & tempMobileno
                End If
                drnewS("CustomerAddress") = clpaddressnew
                drnewS("CustomerPhoneNo") = customerPhoneno
                drnewS("DeliveryDate") = DeliveryDate
                drnewS("DeliveryName") = DeliveryName
                If TokenNoRequiredInKOT = False Then
                    If PrintFormatNo = 6 Then
                        drnewS("TokenNo") = ""
                    End If
                Else
                    drnewS("TokenNo") = StrTokenNo
                End If
                drnewS("DeliveryNote") = "DELIVERY COPY"
                drnewS("isDeliveryCopy") = False
                drnewS("CustomerRemark") = ReminderComments
                'code added by vipul add new rate column in print format 6

                If PrintFormatNo = 6 OrElse PrintFormatNo = 7 Then
                    drnewS("IsRateVisible") = isratevisible
                    'added by khusrao adil on 10-07-2017
                    'Dim isPosPasskey As Boolean = False
                    If IsPosPassKey = True Then
                        If CustomerSaleType = "Home Delivery" Then
                            drnewS("IsPOSPasskey") = True
                            drnewS("CustomerAddress") = clpaddressnew
                        Else 'dine in/take away
                            drnewS("IsPOSPasskey") = True
                            drnewS("CustomerAddress") = ""
                        End If
                    Else 'isPosPasskey=false
                        If CustomerSaleType = "Home Delivery" Then
                            drnewS("IsPOSPasskey") = False
                            drnewS("CustomerAddress") = clpaddressnew
                        Else 'dine in/take away
                            drnewS("IsPOSPasskey") = False
                            drnewS("CustomerAddress") = ""
                            'drnewS("CustomerName") = ""
                        End If
                    End If
                End If
                'code added by irfan on 11/09/2017 visiblity of hsn and tax
                If PrintFormatNo = 6 Then
                    drnewS("IsHsnAndTaxVisibleInPrintFormat6") = IsHsnAndTaxVisibleInPrintFormat6
                End If

                'Code Added by irfan against tender visiblity 9/8/2017
                If PrintFormatNo = 7 Then
                    drnewS("IsTendersVisibleInPrintFormat7") = IsTendersVisibleInPrintFormat7
                End If
                If StrDuplicate <> "" Then
                    drnewS("DuplicateBill") = StrDuplicate
                End If
                If PrintFormatNo = 6 Then
                    drnewS("CustomerGSTNO") = customeRGSTIN
                End If
                dtHeaderDetails.Rows.Add(drnewS)
                If EvasPizzaChanges Then
                    BarCodestring = ImageToBase64(CashMemoNo)
                End If
                'code added by irfan on 11/9/2017 against multiple tender
                If PrintFormatNo = 7 Then
                    If IsTendersVisibleInPrintFormat7 = False Then
                        dtTender.Clear()
                    End If
                End If
                'added by khusrao adil on 09-06-2017
                'for combo display description of combo article on  ssrs formats
                For Each dr As DataRow In DtUnique.Rows
                    Dim articleDescriptionDictionary As New Dictionary(Of String, Integer)
                    Dim drp() = Dtcombo.Select("ComboArticleCode='" & dr("ArticleCode") & "' AND BillLineNO ='" & dr("BillLineNO") & "'")
                    If drp.Count > 0 Then
                        For Each Row In drp
                            If articleDescriptionDictionary.ContainsKey(Row("ArticleName")) Then
                                articleDescriptionDictionary(Row("ArticleName")) = articleDescriptionDictionary(Row("ArticleName")) + Row("Quantity")
                            Else
                                articleDescriptionDictionary.Add(Row("ArticleName"), Row("Quantity"))
                            End If
                        Next
                        Dim AddedDesc = GetMultilinedString(articleDescriptionDictionary, isForComboAndRdl:=True)
                        dr("DISCRIPTION") = dr("DISCRIPTION") & vbCrLf & AddedDesc
                    End If
                Next
             
                If _PrintPreview = True Then
                    If KOTBillPrintingRequired = True Then
                        If KOTAndBillGeneration = False Then
                            If IsCounterCopyKot = False Then
                                If IsKotPrintRequired = True Then
                                    Call CashMemoKOTPrintBasedOnPrintFormatNoSSRS(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, dayopenDate, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, ClientName:=ClientName, DayCloseReportPath:=DayCloseReportPath, CustomerComments:=ReminderComments, EvassPizza:=EvasPizzaChanges)
                                End If
                            End If
                        End If
                    End If
                  
                    If IsFinalReceipt = True Then
                        If PrintFormatNo = 4 OrElse PrintFormatNo = 5 Then
                            If IsCounterCopyKot = False Then
                                If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                                    GenerateCashMemoFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath)
                                End If
                            End If
                        End If
                        '  GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo)
                        'new parameter added for print format 6 dttaxDetail:=taxDetailsForBill
                        If PrintFormatNo = 6 OrElse PrintFormatNo = 7 Then
                            Dim dtPrintCopy As DataTable
                            If dtPrint.Rows.Count > 0 Then
                                If dtPrint.Select("TOPBottom='Bottom'").Count > 0 Then
                                    'dtPrintCopy = dtPrint.Select("TOPBottom='Bottom'").CopyToDataTable()
                                    dtPrintCopy = dtPrint.Select("TOPBottom<>'TOP'").CopyToDataTable()
                                    'Code Added by irfan on 08-08-2018
                                    If IsHsnAndTaxVisibleInPrintFormat6 = True Then
                                        Dim dttemp1, dttemp2
                                        If Not dtPrintCopy.Select("TOPBottom='RETURNAMTFOOTER'") Is Nothing AndAlso dtPrintCopy.Select("TOPBottom='RETURNAMTFOOTER'").Length > 0 Then
                                            dttemp1 = dtPrintCopy.Select("TOPBottom='RETURNAMTFOOTER'").CopyToDataTable
                                        End If
                                        If Not dtPrintCopy.Select("TOPBottom<>'RETURNAMTFOOTER'") Is Nothing AndAlso dtPrintCopy.Select("TOPBottom<>'RETURNAMTFOOTER'").Length > 0 Then
                                            dttemp2 = dtPrintCopy.Select("TOPBottom <>'RETURNAMTFOOTER'").CopyToDataTable
                                        End If
                                        dtPrintCopy.Clear()
                                        If Not dttemp1 Is Nothing Then
                                            dtPrintCopy.Merge(dttemp1)
                                        End If
                                        If Not dttemp2 Is Nothing Then
                                            dtPrintCopy.Merge(dttemp2)
                                        End If
                                    End If
                                Else
                                    dtPrintCopy = dtPrint.Copy
                                    dtPrintCopy.Clear()
                                End If
                            Else
                                dtPrintCopy = dtPrint.Copy()
                            End If
                            GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrintCopy, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo, dttaxDetail:=taxDetailsForBill, IsHsnAndTaxVisibleInPrintFormat6:=IsHsnAndTaxVisibleInPrintFormat6) 'code added by irfan on 11/09/2017 visiblity of hsn and tax
                            If IsDeliveryCopyRequired = True Then
                                If Not (String.IsNullOrEmpty(DeliveryPersonName)) Then
                                    Threading.Thread.Sleep(1000)
                                    dtHeaderDetails.Rows(0)("isDeliveryCopy") = True
                                    dtHeaderDetails.AcceptChanges()
                                    GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrintCopy, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo, dttaxDetail:=taxDetailsForBill, IsHsnAndTaxVisibleInPrintFormat6:=IsHsnAndTaxVisibleInPrintFormat6, IsDeliveryNote:=True) 'code added by irfan on 11/09/2017 visiblity of hsn and tax
                                End If
                            End If
                            
                        Else
                            'GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo, dttaxDetail:=Nothing, IsHsnAndTaxVisibleInPrintFormat6:=IsHsnAndTaxVisibleInPrintFormat6)      'code added by irfan on 11/09/2017 visiblity of hsn and tax
                            'If PrintFormatNo = 4 Then
                            '    '' added by ketan pC Tax Invoice changes AFTER GST
                            '    Dim DSTaxInvoicedtl = obj.GetPCTaxInvoiceDetails(SiteCode:=Sitecode, BillNo:=dtView.Rows(0)("Billno").ToString())
                            '    GeneratePCTaxInvoicePrint(DSTaxInvoicedtl, dtView.Rows(0)("Billno").ToString(), Sitecode, DayCloseReportPath)
                            'Else
                            '    GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo, dttaxDetail:=Nothing, IsHsnAndTaxVisibleInPrintFormat6:=IsHsnAndTaxVisibleInPrintFormat6)      'code added by irfan on 11/09/2017 visiblity of hsn and tax
                            'End If
                            If PrintFormatNo = 4 Then
                                If DisplayBrandWiseSale Then
                                    Dim DtlAllBrandDtlInBill = obj.GetAllStoreBrand(Sitecode, BillNo:=dtView.Rows(0)("Billno").ToString())
                                    If Not DtlAllBrandDtlInBill Is Nothing AndAlso DtlAllBrandDtlInBill.Rows.Count > 0 Then
                                        For Each Dr1 In DtlAllBrandDtlInBill.Rows
                                            Dim DSTaxInvoicedtlBrandWise = obj.UDP_GSTCMTaxInvoiceBrandWise(SiteCode:=Sitecode, BillNo:=dtView.Rows(0)("Billno").ToString(), TreeId:=Dr1("BrandID"))
                                            GenerateaxInvoiceBrandWisePrint(DSTaxInvoicedtlBrandWise, dtView.Rows(0)("Billno").ToString(), Sitecode, DayCloseReportPath, Dr1("BrandName"))
                                        Next
                                    End If
                                Else
                                    Dim DSTaxInvoicedtl = obj.GetPCTaxInvoiceDetails(SiteCode:=Sitecode, BillNo:=dtView.Rows(0)("Billno").ToString())
                                    GeneratePCTaxInvoicePrint(DSTaxInvoicedtl, dtView.Rows(0)("Billno").ToString(), Sitecode, DayCloseReportPath)
                                End If
                                ' '' added by ketan pC Tax Invoice changes AFTER GST
                                'Dim DSTaxInvoicedtl = obj.GetPCTaxInvoiceDetails(SiteCode:=Sitecode, BillNo:=dtView.Rows(0)("Billno").ToString())
                                'GeneratePCTaxInvoicePrint(DSTaxInvoicedtl, dtView.Rows(0)("Billno").ToString(), Sitecode, DayCloseReportPath)
                            Else
                                GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo, dttaxDetail:=Nothing, IsHsnAndTaxVisibleInPrintFormat6:=IsHsnAndTaxVisibleInPrintFormat6)      'code added by irfan on 11/09/2017 visiblity of hsn and tax
                            End If
                        End If

                    End If
                Else
                    If KOTBillPrintingRequired = True Then
                        If KOTAndBillGeneration = False Then
                            If IsCounterCopyKot = False Then
                                If IsKotPrintRequired = True Then
                                    Call CashMemoKOTPrintBasedOnPrintFormatNoSSRS(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, dayopenDate, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, ClientName:=ClientName, DayCloseReportPath:=DayCloseReportPath, CustomerComments:=ReminderComments, EvassPizza:=EvasPizzaChanges)
                                End If
                            End If
                        End If
                    End If
                   
                    If IsFinalReceipt Then
                        If PrintFormatNo = 4 Or PrintFormatNo = 5 Then
                            If IsCounterCopyKot = False Then
                                If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                                    GenerateCashMemoFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath)
                                End If
                            End If
                        End If
                        ' GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo)
                        If PrintFormatNo = 6 Or PrintFormatNo = 7 Then
                            Dim dtPrintCopy As DataTable
                            If dtPrint.Rows.Count > 0 Then
                                If dtPrint.Select("TOPBottom='Bottom'").Count > 0 Then
                                    dtPrintCopy = dtPrint.Select("TOPBottom<>'TOP'").CopyToDataTable()
                                    If IsHsnAndTaxVisibleInPrintFormat6 = True Then
                                        Dim dttemp1, dttemp2
                                        If Not dtPrintCopy.Select("TOPBottom='RETURNAMTFOOTER'") Is Nothing AndAlso dtPrintCopy.Select("TOPBottom='RETURNAMTFOOTER'").Length > 0 Then
                                            dttemp1 = dtPrintCopy.Select("TOPBottom='RETURNAMTFOOTER'").CopyToDataTable
                                        End If
                                        If Not dtPrintCopy.Select("TOPBottom<>'RETURNAMTFOOTER'") Is Nothing AndAlso dtPrintCopy.Select("TOPBottom<>'RETURNAMTFOOTER'").Length > 0 Then
                                            dttemp2 = dtPrintCopy.Select("TOPBottom <>'RETURNAMTFOOTER'").CopyToDataTable
                                        End If
                                        dtPrintCopy.Clear()
                                        If Not dttemp1 Is Nothing Then
                                            dtPrintCopy.Merge(dttemp1)
                                        End If
                                        If Not dttemp2 Is Nothing Then
                                            dtPrintCopy.Merge(dttemp2)
                                        End If
                                    End If
                                Else
                                    dtPrintCopy = dtPrint.Copy()
                                    dtPrintCopy.Clear()
                                End If
                            Else
                                dtPrintCopy = dtPrint.Copy()
                            End If
                            GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrintCopy, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo, dttaxDetail:=taxDetailsForBill, IsHsnAndTaxVisibleInPrintFormat6:=IsHsnAndTaxVisibleInPrintFormat6)  'code added by irfan on 12/09/2017 visiblity of hsn and tax
                            'added by khusrao adil for natural
                            If IsDeliveryCopyRequired = True Then
                                If Not (String.IsNullOrEmpty(DeliveryPersonName)) Then
                                    Threading.Thread.Sleep(1000)
                                    dtHeaderDetails.Rows(0)("isDeliveryCopy") = True
                                    dtHeaderDetails.AcceptChanges()
                                    GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrintCopy, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo, dttaxDetail:=taxDetailsForBill, IsHsnAndTaxVisibleInPrintFormat6:=IsHsnAndTaxVisibleInPrintFormat6, IsDeliveryNote:=True)  'code added by irfan on 12/09/2017 visiblity of hsn and tax
                                End If
                            End If
                           
                        Else

                            If NoOfCopies = 0 Then
                                NoOfCopies = 1
                            End If
                            If NoOfCopies > 0 Then
                                For index = 1 To NoOfCopies

                                    'GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo)
                                    'new parameter added for print format 6 dttaxDetail:=taxDetailsForBill
                                    '  GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo, dttaxDetail:=taxDetailsForBill, IsHsnAndTaxVisibleInPrintFormat6:=IsHsnAndTaxVisibleInPrintFormat6)  'code added by irfan on 12/09/2017 visiblity of hsn and tax
                                    If PrintFormatNo = 4 Then
                                        ' '' added by ketan pC Tax Invoice changes AFTER GST
                                        'Dim DSTaxInvoicedtl = obj.GetPCTaxInvoiceDetails(SiteCode:=Sitecode, BillNo:=dtView.Rows(0)("Billno").ToString())
                                        'GeneratePCTaxInvoicePrint(DSTaxInvoicedtl, dtView.Rows(0)("Billno").ToString(), Sitecode, DayCloseReportPath)
                                        If DisplayBrandWiseSale Then
                                            Dim DtlAllBrandDtlInBill = obj.GetAllStoreBrand(Sitecode, BillNo:=dtView.Rows(0)("Billno").ToString())
                                            If Not DtlAllBrandDtlInBill Is Nothing AndAlso DtlAllBrandDtlInBill.Rows.Count > 0 Then
                                                For Each Dr1 In DtlAllBrandDtlInBill.Rows
                                                    Dim DSTaxInvoicedtlBrandWise = obj.UDP_GSTCMTaxInvoiceBrandWise(SiteCode:=Sitecode, BillNo:=dtView.Rows(0)("Billno").ToString(), TreeId:=Dr1("BrandID"))
                                                    GenerateaxInvoiceBrandWisePrint(DSTaxInvoicedtlBrandWise, dtView.Rows(0)("Billno").ToString(), Sitecode, DayCloseReportPath, Dr1("BrandName"))
                                                Next
                                            End If
                                        Else
                                            Dim DSTaxInvoicedtl = obj.GetPCTaxInvoiceDetails(SiteCode:=Sitecode, BillNo:=dtView.Rows(0)("Billno").ToString())
                                            GeneratePCTaxInvoicePrint(DSTaxInvoicedtl, dtView.Rows(0)("Billno").ToString(), Sitecode, DayCloseReportPath)
                                        End If
                                    Else
                                        GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo, dttaxDetail:=Nothing, IsHsnAndTaxVisibleInPrintFormat6:=IsHsnAndTaxVisibleInPrintFormat6)      'code added by irfan on 11/09/2017 visiblity of hsn and tax
                                    End If
                                Next
                            End If
                            End If


                    End If

                End If
            Else
                errorMsg = getValueByKey("CLSCMP031")
            End If
        Catch ex As Exception
            errorMsg = ex.Message
        End Try
    End Sub
    ' code adde by irfan for print format 11
    Public Sub CashMemoPrintFormat_SSRS11(ByVal dayopenDate As Date, ByVal LangCode As String, ByVal Currency As String, ByVal Sitecode As String, ByVal Type As String, ByVal DuplicatePrinting As String, ByVal DeletedUserid As String, ByVal AuthorisedUser As String, ByRef errorMsg As String, Optional ByVal GiftMsg As String = "", Optional ByVal IsSavoy As Boolean = False, Optional ByVal MettlerConn As String = "", Optional ByVal IsArticleWiseKot As Boolean = False, Optional ByVal IsCounterCopy As Boolean = False, Optional ByVal IsFinalReceipt As Boolean = False, Optional ByVal ClientName As String = "", Optional ByVal DayCloseReportPath As String = "", Optional ByVal IsCounterCopyKot As Boolean = False, Optional ByVal KOTAndBillGeneration As Boolean = False)
        Try
            Dim strPrintMsg, sbDeliveryBillPrint As StringBuilder
            Dim strLineDetail As New StringBuilder
            Dim StrHeader, StrSubHeader, StrBody, StrCompanyInfo, StrTagLine, StrSubFooter, StrFooter, StrDuplicate, StrDeleteLine, StrCashMemoLine, StrCashierLine, StrSalesPersonLine As String
            Dim StrTokenNo, StrCustomerName As String
            Dim StrCMNo, StrCMDate, StrCMTime, StrCashier, StrSalesPerson As String
            Dim SiteLine, StrSiteTelline, StrAdrressLine, StrSiteReg1Line, StrSiteReg2Line As String
            Dim CLpLine, LineItemHeading, StrLineItem As String
            Dim CustomerNameLine, CLPPointsLine, CustmerPhoneNo As String
            Dim ItemCode, Desc, Qty, Rate, DiscAmt, DiscPer, TaxAmt, NetAmt, NetAmtTemp, RateAfterDisc, GrossAmt, FinScaleNo As String
            Dim TotalQtyLine, TakeAwayQuantity, TotalGrossAmtLine, TotalDiscAmtLine, SubTotalLine, NetAmtLine, TotalTaxAmtLine As String
            Dim TotalFooterTaxDetail, FooterTaxLine As String
            Dim TenderDetails, TenderLine As String
            Dim TotalPromotionalMsg, PromoMsgLine, StrPrintHeaderLine, StrPrintFooterLine As String
            Dim TermsNcond As String
            Dim strLine As String
            Dim strDblLine As String
            Dim strHomeDilery As String
            Dim isTakeAwayQtyPrint As Boolean = False
            errorMsg = ""
            Dim dtView As New DataTable
            Dim BoldLineLength As Int32 = 27
            Dim obj As New SpectrumBL.clsCashMemo()
            dtView = obj.GetCashMemo(CashMemoNo, Sitecode, LangCode)

            Dim dvItemDetail As New DataView(dtView, "", "", DataViewRowState.CurrentRows)
            'Dim DtUnique As DataTable = dvItemDetail.ToTable(True, "ArticleCode", "DISCRIPTION", "QUANTITY", "SELLINGPRICE", "GROSSAMT", "TOTALDISCOUNT", "TOTALDISCPERCENTAGE", "NETAMOUNT", "TOTALTAXAMOUNT")
            Dim DtUnique As DataTable = obj.GetBillDetailsDataForPrint(CashMemoNo, Sitecode, LangCode, True)
            Dim Dtcombo As DataTable = obj.GetComboDetailsDataForPrint(CashMemoNo, Sitecode, LangCode)
            If Not IsDBNull(dtView.Rows(0)("ServiceTaxAmount")) AndAlso Val(dtView.Rows(0)("ServiceTaxAmount")) > 0 Then
                CustomerSaleType = GetCustomerSaleType(dtView.Rows(0)("ServiceTaxAmount"))
            Else
                CustomerSaleType = ""
            End If
            If Not IsDBNull(dtView.Rows(0)("DeliveryPersonID")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("DeliveryPersonID")) Then
                DeliveryPersonName = dtView.Rows(0)("DeliveryPersonID").ToString()
            End If
            If DtUnique Is Nothing Then Exit Sub
            If dtView Is Nothing Then Exit Sub
            Dim SiteName As String
            SiteName = dtView.Rows(0)("SITEOFFICIALNAME").ToString()
            If ClientName <> "" Then
                Dim arrClientname = ClientName.Split(" ").ToArray()

                For i = 0 To arrClientname.Length - 1
                    If SiteName.ToUpper.Contains(arrClientname(i).ToString.ToUpper) Then
                        SiteName = SiteName.ToUpper.Replace(arrClientname(i).ToString.ToUpper.Trim, "").Trim
                    End If
                Next
            End If
            SiteName = SiteName.PadLeft((Val(LineLength - SiteName.Length) + SiteName.Length) / 2)
            '------------------
            SiteLine = SiteName.Trim
            Dim PhoneLine As String = dtView.Rows(0)("TELNO").ToString()
            Dim addressLine1 As String = dtView.Rows(0)("ADDRESSLINE1").ToString().Trim
            Dim addressLine2 As String = dtView.Rows(0)("ADDRESSLINE2").ToString().Trim
            Dim addressLine3 As String = dtView.Rows(0)("ADDRESSPINCODE").ToString().Trim
            If Type = "DCM" Then
                StrCMNo = getValueByKey("CLSCMP008") & dtView.Rows(0)("Billno").ToString()
            Else
                StrCMNo = getValueByKey("CLSCMP039") & ":" & dtView.Rows(0)("Billno").ToString()
                '---------------
            End If
            StrCMDate = dayopenDate.ToString("dd-MM-yyyy")
            strDayOpenDate = dayopenDate.ToShortDateString()
            If DuplicatePrinting <> String.Empty Then
                StrCMDate = Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())
            End If
            StrSalesPerson = getValueByKey("CLSCMP010") & dtView.Rows(0)("SALESEXECUTIVECODE").ToString()


            Dim CLPaddress As String = ""
            Dim CLPBalancePoints As String
            Dim clpaddressnew As String = ""
            Dim customername As String = ""
            Dim customerPhoneno As String = ""
            Dim DeliveryName As String = ""
            Dim DeliveryDate As DateTime

            If dtView.Rows(0)("CLPNO").ToString() <> String.Empty Then
                'CLpLine = "CLP Card No:" & dtView.Rows(0)("CLPNO").ToString()
                'CustomerNameLine = "Customer Name: " & dtView.Rows(0)("CLPNAME").ToString()
                'CLPPointsLine = "CLP Points Earned: " & FormatNumber(dtView.Rows(0)("CLPPoints").ToString())

                CLpLine = getValueByKey("CLSCMP011") & dtView.Rows(0)("CLPNO").ToString()

                CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CLPNAME").ToString()
                customername = dtView.Rows(0)("CLPNAME").ToString().Trim
                clpaddressnew = dtView.Rows(0)("CLPAddress").ToString()
                clpaddressnew = clpaddressnew.Trim
                CLPPointsLine = getValueByKey("CLSCMP013") & FormatNumber(Val(dtView.Rows(0)("CLPPoints").ToString()))
                customerPhoneno = dtView.Rows(0)("MobileNo").ToString()
                If dtView.Rows.Count() > 0 Then
                    '---- Changes By Mahesh : Customer Name is Not Mandatory for Home Delivery 
                    'If dtView.Rows(0)("HDName").ToString() <> String.Empty Then
                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        DeliveryDate = dtView.Rows(0)("HDDeliveryDate").ToString()
                        'strHomeDilery = strHomeDilery & getValueByKey("CLSCMP023") & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf
                        DeliveryName = DeliveryPersonName.Trim
                    End If
                End If
                If (dtView.Rows(0)("RedemptionPoints") IsNot DBNull.Value AndAlso dtView.Rows(0)("RedemptionPoints") > 0) Then
                    CLPPointsLine += vbCrLf + getValueByKey("CLSCMP034") & FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()))
                End If

                CLPaddress = dtView.Rows(0)("CLPAddress").ToString()
                If CLPaddress <> String.Empty Then
                    CLPaddress = getValueByKey("CLSCMP014") & dtView.Rows(0)("CLPAddress").ToString()
                End If

                CLPBalancePoints = String.Empty
                CLPBalancePoints = getValueByKey("CLSCMP015")
                Dim dblCLPPoints As Double

                dblCLPPoints = Val(dtView.Rows(0)("BalancePoints").ToString())
                'For Each drRow As DataRow In dtView.Select("Tendertypecode = 'CLPPoint'")
                '    dblCLPPoints -= drRow("RedemptionPoints")
                'Next

                Dim RedemptionPoints As DataRow = dtView.Select("Tendertypecode = 'CLPPoint'").FirstOrDefault()
                If (RedemptionPoints IsNot Nothing AndAlso RedemptionPoints("RedemptionPoints") IsNot DBNull.Value) Then
                    dblCLPPoints -= Val(RedemptionPoints("RedemptionPoints").ToString())
                End If

                CLPBalancePoints = CLPBalancePoints & FormatNumber(dblCLPPoints.ToString())
                If CLPaddress <> String.Empty Then
                    CLPaddress = SplitString(CLPaddress, LineLength).ToString()
                End If
                'If CLpLine <> Nothing AndAlso CLpLine.ToString().Length > 0 Then ' 3 lines commented for clp details
                '    StrSubFooter = StrSubFooter & CLpLine & vbCrLf
                'End If
                CustmerPhoneNo = getValueByKey("RP187") & " " & dtView.Rows(0)("Mobileno").ToString()
                StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
                'StrSubFooter = StrSubFooter & CLPBalancePoints & vbCrLf ''' 2 lines commented for clp details
                If CustomerSaleType = "Home Delivery" Then
                    StrSubFooter = StrSubFooter & CustmerPhoneNo & vbCrLf
                End If

                'StrSubFooter = StrSubFooter & CustmerPhoneNo & vbCrLf
                'StrSubFooter = StrSubFooter & CLPPointsLine & vbCrLf


            ElseIf dtView.Columns.Contains("CustomerNo") AndAlso dtView.Rows(0)("CustomerNo").ToString() <> String.Empty Then

                CLpLine = getValueByKey("CLSCMP038") & dtView.Rows(0)("CustomerNo").ToString()

                CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CustomerName").ToString()
                If CLpLine <> Nothing AndAlso CLpLine.ToString().Length > 0 Then
                    StrSubFooter = StrSubFooter & CLpLine & vbCrLf
                End If
                StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
            End If


            Dim TotalQty, TGrossAmt, TDiscAmt, TRateAfterDisc, TTaxtAmt As String
            Dim TNetAmt As String = 0.0
            Dim BillLineNO As String
            DtScannedMettlerBills = obj.GetMettlerBillDetailsDataForPrint(CashMemoNo, Sitecode)
            Dim ScaleNo As String
            Dim objItemSch As New clsIteamSearch()
            Dim dtScanedBillArticle As New DataTable
            Dim dtNewArticleScaledtls As New DataTable
            Dim dtTender As DataTable 'sagar
            dtNewArticleScaledtls.Columns.Add("Article", System.Type.GetType("System.String"))
            dtNewArticleScaledtls.Columns.Add("ScaleNo", System.Type.GetType("System.String"))
            'dtNewArticleScaledtls.Columns.Add("BillNo", System.Type.GetType("System.String"))
            For ScanBillIndex = 0 To DtScannedMettlerBills.Rows.Count - 1
                '------- GET Mettler Scanned Bill Details From Mettler DataBase 
                With DtScannedMettlerBills
                    ScaleNo = Val(.Rows(ScanBillIndex)("SCALE_NO"))
                    dtScanedBillArticle = objItemSch.GetScanedBillArticle(ScaleNo:=ScaleNo, BillNo:=.Rows(ScanBillIndex)("Bill_No"), BillIntDate:=.Rows(ScanBillIndex)("MettlerScaleBillDate"), MettlerConn:=MettlerConnString) '    ScaleBillIntDate = Val(.Rows(ScanBillIndex)("MettlerScaleBillDate"))

                    If dtScanedBillArticle IsNot Nothing Then

                        For Each drscale As DataRow In dtScanedBillArticle.Rows
                            Dim drnew As DataRow = dtNewArticleScaledtls.NewRow
                            drnew("Article") = drscale("LegacyArticleCode")
                            drnew("ScaleNo") = ScaleNo
                            ' drnew("BillNo") = drscale("BillNo")
                            dtNewArticleScaledtls.Rows.Add(drnew)
                        Next

                    End If
                End With

            Next
            DtUnique.Columns.Add("ScaleNo", System.Type.GetType("System.Int32"))
            'DtUnique.Columns.Add("BillNo", System.Type.GetType("System.String"))
            For Each drscaleno As DataRow In dtNewArticleScaledtls.Rows
                Dim dr() As DataRow = DtUnique.Select("ArticleCode= '" & drscaleno("Article") & "'")
                If dr.Length > 0 Then
                    dr(0)("ScaleNo") = drscaleno("ScaleNo")
                    'dr(0)("BillNo") = drscaleno("BillNo")
                End If
            Next
            If Not dtNewArticleScaledtls Is Nothing Then
                If dtNewArticleScaledtls.Rows.Count = 0 Then
                    For Each druniq As DataRow In DtUnique.Rows
                        druniq("ScaleNo") = 0
                        'druniq("BillNo") = 0
                    Next
                Else
                    For Each druniq As DataRow In DtUnique.Rows
                        If Convert.ToString(druniq("ScaleNo")) = "" Then
                            druniq("ScaleNo") = 0
                            'druniq("BillNo") = 0
                        End If
                    Next
                End If
            End If
            For Each dr As DataRow In DtUnique.Rows
                ItemCode = dr("ArticleCode").ToString()

                If (DisplayArticleFullName) Then
                    Desc = dr("ArticleFullName").ToString()
                Else
                    Desc = dr("DISCRIPTION").ToString()
                End If
                BillLineNO = dr("BillLineNO").ToString()
                If SpectrumCommon.ComboItemPrintingAllowed AndAlso Dtcombo IsNot Nothing AndAlso Dtcombo.Rows.Count > 0 Then
                    Dim drp() = Dtcombo.Select("ComboArticleCode='" & ItemCode & "' AND BillLineNO ='" & BillLineNO & "'")
                    If drp.Length > 0 Then
                        Dim articleDescriptionDictionary As New Dictionary(Of String, Integer)
                        For index = 0 To drp.Length - 1
                            If articleDescriptionDictionary.ContainsKey(drp(index)("ArticleName")) Then
                                articleDescriptionDictionary(drp(index)("ArticleName")) = articleDescriptionDictionary(drp(index)("ArticleName")) + drp(index)("Quantity")
                            Else
                                articleDescriptionDictionary.Add(drp(index)("ArticleName"), drp(index)("Quantity"))
                            End If
                        Next
                        Dim AddedDesc = GetMultilinedString(articleDescriptionDictionary)
                        Desc = Desc & vbCrLf & AddedDesc
                    End If
                End If
                FinScaleNo = dr("ScaleNo").ToString()

                Qty = dr("Quantity").ToString()
                Rate = dr("SellingPrice").ToString()
                RateAfterDisc = (dr("SellingPrice") - dr("TOTALDISCOUNT")).ToString()
                If CDbl(clsCommon.CheckIfBlank(Rate)) < 0 Then
                    Rate = CDbl(clsCommon.CheckIfBlank(Rate)) * -1
                End If
                DiscAmt = dr("TOTALDISCOUNT").ToString()
                DiscPer = dr("TOTALDISCPERCENTAGE").ToString()
                NetAmt = dr("NetAmount").ToString()
                GrossAmt = dr("GROSSAMT").ToString()
                TaxAmt = dr("TOTALTAXAMOUNT").ToString()


                If (AllowDecimalQty) Then
                    If (WeightScaleEnabled) Then
                        Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 3)
                    Else
                        Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 2)
                    End If
                Else
                    Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 0)
                End If

                'For i = Qty.Length To 4
                '    Qty = " " & Qty
                'Next
                Rate = FormatNumber(CDbl(clsCommon.CheckIfBlank(Rate)), 2)

                RateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(RateAfterDisc)), 2)
                'Rate = FormatCurrency(Rate, 2)
                'For i = Rate.Length To 8
                '    Rate = " " & Rate
                'Next
                DiscAmt = FormatNumber(CDbl(IIf(DiscAmt <> String.Empty, DiscAmt, 0)), 2)
                'For i = DiscAmt.Length To 8
                '    DiscAmt = " " & DiscAmt
                'Next
                DiscPer = FormatNumber(CDbl(IIf(DiscPer <> String.Empty, DiscPer, 0)), 2)
                DiscPer = DiscPer & "%"
                'For i = DiscPer.Length To 8
                '    DiscPer = " " & DiscPer
                'Next
                'Format of NetAmt with No Digits after Decimal
                NetAmt = FormatNumber(CDbl(NetAmt), 0)
                NetAmtTemp = FormatNumber(CDbl(NetAmt), 2)
                ' NetAmt = FormatNumber(CDbl(NetAmt), 2)
                GrossAmt = FormatNumber(CDbl(GrossAmt), 2)
                'GrossAmt = FormatNumber(CDbl(GrossAmt), 0)
                'For i = NetAmt.Length To 8
                '    NetAmt = " " & NetAmt
                'Next
                TaxAmt = FormatNumber(CDbl(IIf(TaxAmt <> String.Empty, TaxAmt, 0)), 2)
                'For i = TaxAmt.Length To 7
                '    TaxAmt = " " & TaxAmt
                'Next

                Dim TempNetAmt As String = "0"

                If Type = "CMWAmt" Then
                Else
                    If 28 - Desc.Length < Qty.Length Then
                        Select Case PrintFormatNo
                            Case 1
                            Case 2

                            Case Else
                                TempNetAmt = NetAmt
                                If Val(NetAmt) > 0 AndAlso Val(TaxAmt) > 0 Then
                                    TempNetAmt = CDbl(GrossAmt) - CDbl(TaxAmt)

                                End If
                        End Select
                    Else
                        Select Case PrintFormatNo
                            Case 1

                            Case 2

                            Case Else
                                TempNetAmt = NetAmt
                                If Val(NetAmt) > 0 AndAlso Val(TaxAmt) > 0 Then
                                    TempNetAmt = CDbl(GrossAmt) - CDbl(TaxAmt)
                                End If
                        End Select
                    End If
                End If
                ' Added For Font Changes as Requested By Client-------------
                ' strLineDetail.Append(StrLineItem)
                strLineDetail.Append(StrLineItem)
                '--------------------

                If CDbl(Qty.Trim) > 0 Then
                    TotalQty = CDbl(clsCommon.CheckIfBlank(TotalQty)) + CDbl(Qty.Trim)
                End If
                TDiscAmt = CDbl(clsCommon.CheckIfBlank(TDiscAmt)) + CDbl(DiscAmt.Trim)
                TGrossAmt = CDbl(clsCommon.CheckIfBlank(TGrossAmt)) + CDbl(dr("GROSSAMT").ToString())
                'TNetAmt = CDbl(clsCommon.CheckIfBlank(TNetAmt)) + CDbl(NetAmt.Trim)
                'TNetAmt = Format(Math.Round(Convert.ToDecimal(CDbl(clsCommon.CheckIfBlank(TNetAmt)) + CDbl(NetAmt.Trim))), "0.00")
                TNetAmt = Decimal.Add(FormatNumber(CDbl(NetAmtTemp), 2).ToString(), TNetAmt)
                TRateAfterDisc = CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)) + CDbl(RateAfterDisc.Trim)
                TTaxtAmt = CDbl(clsCommon.CheckIfBlank(TTaxtAmt)) + CDbl(TaxAmt.Trim)
            Next

            TotalQty = FormatNumber(CDbl(clsCommon.CheckIfBlank(TotalQty)), 2)
            'TDiscAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TDiscAmt)), 2)
            TDiscAmt = FormatNumber(dtView.Rows(0)("Discount"), 2)
            TGrossAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TGrossAmt)), 2)
            'TNetAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TNetAmt)), 2)
            TNetAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TNetAmt)), RoundOff), 2)
            TRateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)), 2)
            TTaxtAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TTaxtAmt)), 2)
            Dim TSubTotalAmt As String
            Dim TotalDisPercent As String
            If Type <> "CMWAmt" Then
                If CDbl(clsCommon.CheckIfBlank(TDiscAmt)) > 0 Then
                    TotalDisPercent = Math.Round((CDbl(clsCommon.CheckIfBlank(TDiscAmt)) / CDbl(clsCommon.CheckIfBlank(TGrossAmt))) * 100, 1).ToString()
                    TSubTotalAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TGrossAmt)) - CDbl(clsCommon.CheckIfBlank(TDiscAmt)), RoundOff), 0)
                    TDiscAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TDiscAmt)), RoundOff), 0)
                    Dim ObjclsCommon As New clsCommon
                    Dim offerno As String = ObjclsCommon.GetOffernoForRoundOff()
                    If DtUnique.Rows(0)("PromotionId").ToString().Contains(offerno) And offerno <> "" Then
                        ' TotalDiscAmtLine = "Round Off" & Space(BoldLineLength / 2 - "Round Off".Length) & " :" & TDiscAmt.PadLeft(BoldLineLength - ("Round Off".Length + Space(BoldLineLength / 2 - "Round Off".Length).Length + ":".Length))
                    Else
                        '  Dim CheckIfPositiveNumber = IIf(BoldLineLength / 2 - (String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).Length) > 0, BoldLineLength / 2 - String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).Length, 0)
                        '  TotalDiscAmtLine = String.Format(getValueByKey("CLSCMP019"), TotalDisPercent) & Space(CheckIfPositiveNumber) & " :" & TDiscAmt.PadLeft(BoldLineLength - (String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).Length + ": ".Length))
                    End If
                    '--------------------
                End If

                TGrossAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TGrossAmt)), RoundOff), 0)
                TNetAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TNetAmt)), RoundOff), 0)
                TRateAfterDisc = TRateAfterDisc & " " & Currency
                TTaxtAmt = TTaxtAmt & " " & Currency
                TotalGrossAmtLine = "Total Amt" & Space(5) & ":" & TGrossAmt.PadLeft(BoldLineLength - Val("Total Amt".Length + Space(5).Length))
                If (CDbl(TDiscAmt.Replace("INR", ""))) > 0 Then
                    NetAmtLine = getValueByKey("CLSCMP020") & Space(2) & ":" & TNetAmt.PadLeft(BoldLineLength - Val(getValueByKey("CLSCMP020").Length + Space(2).Length))
                End If
                '--------------------
                If _TaxDetail = True Then
                    Dim taxDetailsForBill As DataTable = obj.GetTaxDetailsForBillNo(Sitecode, dtView.Rows(0)("Billno").ToString())
                    If taxDetailsForBill IsNot Nothing AndAlso taxDetailsForBill.Rows.Count > 0 Then
                        For Each row As DataRow In taxDetailsForBill.Rows
                            'If CompositeTaxReqOnPrint = False AndAlso IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                            If IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                            Dim taxValue As String = row("TaxVAlue").ToString()
                            taxValue = FormatNumber(CDbl(clsCommon.CheckIfBlank(taxValue)), 2)
                            taxValue = taxValue & " " & Currency
                            FooterTaxLine = FooterTaxLine & row("TaxName").PadRight(LineLength - 16) & ":" & taxValue.PadLeft(14) & vbCrLf
                        Next
                    End If
                End If
                TotalFooterTaxDetail = FooterTaxLine
                If KOTAndBillGeneration = True Then
                    dtTender = obj.GetTenderTypeByRefBillNo(dtView.Rows(0)("BILLNO").ToString())
                    If dtTender.Rows.Count = 0 Then
                        dtTender = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                    End If
                Else
                    dtTender = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                End If
                ' dtTender = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                For Each drTender As DataRow In dtTender.Rows
                    If UCase(obj.GetDefaultConfigValue(Sitecode, "CVInfoReqOnPrint")) = "FALSE" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)") Then
                        Continue For
                    End If
                    Dim amounttendered As String = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(drTender("AMOUNTTENDERED").ToString)), RoundOff), 0)
                    Dim tender As String = drTender("CurrencyCode").ToString() & " " & amounttendered & vbCrLf
                    '----------------
                    Dim tenderName As String
                    'Added by Rohit
                    If drTender("CardNO").ToString() <> "" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(I)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(R)") Then
                        tenderName = drTender("TENDERHEADNAME").ToString() & "(" & drTender("CardNO").ToString() & ") "
                    Else
                        tenderName = drTender("TENDERHEADNAME").ToString()
                    End If
                    'End editing
                    If Not drTender("AMOUNTINCURRENCY") Is DBNull.Value AndAlso drTender("AMOUNTINCURRENCY") <> 0 Then '0000404
                        tender = drTender("CurrencyCode").ToString() & " " & FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(drTender("AMOUNTINCURRENCY").ToString())), RoundOff), 0) & vbCrLf
                    End If
                    If drTender("TENDERTYPECODE").ToString() = "CLPPoint" Then
                        tender = FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()), 0)
                    End If

                Next

                If (_IsDisplayTotalSaving) Then
                    If (Not String.IsNullOrEmpty(TDiscAmt) AndAlso Not String.Equals(TDiscAmt, "0.00")) Then
                        Dim totalSavingAmount As String = String.Format(getValueByKey("CLSCMP033"), Currency, TDiscAmt.Replace(Currency, String.Empty).Trim)
                    Else
                    End If
                Else
                End If


                '--------------------
                'Company and Tin Info
                Dim NoOfReprints As String = IIf(IsDBNull(dtView.Rows(0)("NoofReprints")), String.Empty, dtView.Rows(0)("NoofReprints")).ToString()
                Dim CompanyName As String = dtView.Rows(0)("SITEOFFICIALNAME") 'obj.GetDefaultConfigValue(Sitecode, "CompanyName")
                ''Dim remarks As String = dtView.Rows(0)("Remark")
                'If Not IsDBNull(dtView.Rows(0)("Remark")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("Remark")) Then
                '    Dim remarks As String = dtView.Rows(0)("Remark")
                '    remarks = SplitString(getValueByKey("cashmemoprint.discountremark") & remarks, LineLength).ToString()
                '    StrCompanyInfo = StrCompanyInfo & remarks.TrimEnd() & vbCrLf
                'End If

                'If Not String.IsNullOrEmpty(NoOfReprints) Then
                '    StrCompanyInfo = StrCompanyInfo & "Re-Print Number : " & NoOfReprints & vbCrLf
                '    StrCompanyInfo = StrCompanyInfo & "Re-Print Date : " & DateTime.Now.ToShortDateString() & vbCrLf
                'End If
                'Company and Tin Info End

                Dim dtPromo As DataTable = dvItemDetail.ToTable(True, "PROMOTIONALMSGID", "PROMOMESS")
                For Each drTerms As DataRow In dtPromo.Select("", "PROMOTIONALMSGID", DataViewRowState.CurrentRows)
                    If Not String.IsNullOrEmpty(drTerms("PROMOMESS").ToString()) Then
                        PromoMsgLine = PromoMsgLine & drTerms("PROMOMESS").ToString() & vbCrLf
                    End If
                Next

                TotalPromotionalMsg = PromoMsgLine
                strPrintMsg = New StringBuilder()
                sbDeliveryBillPrint = New StringBuilder()

                'If dtView.Rows.Count() > 0 Then
                '    '---- Changes By Mahesh : Customer Name is Not Mandatory for Home Delivery 
                '    'If dtView.Rows(0)("HDName").ToString() <> String.Empty Then
                '    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                '        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP022") & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP023") & dtView.Rows(0)("HDName").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & SplitString(getValueByKey("CLSCMP024") & dtView.Rows(0)("HDAddress").ToString(), LineLength).ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP025") & dtView.Rows(0)("HDTelNo").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP026") & dtView.Rows(0)("HDEmail").ToString() & vbCrLf

                '    Else
                '        If _CustomerNameRequiredInKOT AndAlso CustomerNameLine <> Nothing Then
                '            StrCustomerName = CustomerNameLine.ToUpper.Replace("CUSTOMER", "").Trim()

                '        End If
                '        If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                '            If Not AllowBillingOnlyAfterSelectionOfSalesType Then
                '                CustomerSaleType = String.Empty
                '            End If
                '            Dim StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - CustomerSaleType.Length) & CustomerSaleType & vbCrLf & vbCrLf
                '        End If
                '    End If
                'End If
                Dim dtPrint As New DataTable
                Dim objPrint As New SpectrumBL.clsCommon
                dtPrint = objPrint.GetPrintingDetails(Type)
                'If Not dtPrint Is Nothing Then
                '    Dim filter As String = ""
                '    Dim dv As New DataView(dtPrint, filter, "Sequenceno", DataViewRowState.CurrentRows)
                '    filter = "TOPBOTTOM='Bottom'"
                '    dv.RowFilter = filter
                '    For Each drview As DataRowView In dv
                '        If drview("ReceiptText").ToString().Length >= 25 Then
                '        Else
                '        End If
                '    Next

                'End If
                If DtUnique.Rows.Count > 0 Then
                    For index = 0 To DtUnique.Rows.Count - 1
                        DtUnique(index)("GROSSAMT") = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(DtUnique(index)("GROSSAMT"))), RoundOff), 0)
                    Next
                    DtUnique.AcceptChanges()
                End If
                Dim dtHeaderDetails As New DataTable
                dtHeaderDetails.Columns.Add("Header", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("Footer", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("ClientName", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("INR", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("StoreName", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("Address", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("PhoneNo", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("FINALRECEIPT", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("TotalAmt", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("LessDisPer", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("LessDisAmt", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("SubTotal", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("TotalToPay", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("CashierName", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("CustomerName", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("CustomerAddress", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("CustomerPhoneNo", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("DeliveryDate", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("DeliveryName", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("SalesType", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("DeliveryNote", System.Type.GetType("System.String"))
                Dim drnewS = dtHeaderDetails.NewRow
                drnewS("Header") = "Submit Counter Copy at Scale"
                drnewS("Footer") = "BILL PAID"
                drnewS("ClientName") = ClientName.Trim
                drnewS("INR") = 0
                drnewS("StoreName") = SiteName.Trim
                drnewS("Address") = addressLine1.Trim & Space(1) & addressLine2.Trim & Space(1) & addressLine3.Trim
                drnewS("PhoneNo") = PhoneLine.Trim
                drnewS("FINALRECEIPT") = "FINAL RECEIPT"
                drnewS("TotalAmt") = TGrossAmt.Trim
                If TotalDisPercent Is Nothing Then
                    drnewS("LessDisPer") = 0
                Else
                    drnewS("LessDisPer") = TotalDisPercent
                End If

                drnewS("LessDisAmt") = TDiscAmt
                If TSubTotalAmt Is Nothing Then
                    drnewS("SubTotal") = 0
                Else
                    drnewS("SubTotal") = TSubTotalAmt
                End If

                drnewS("TotalToPay") = TNetAmt
                drnewS("CashierName") = LoginUserName
                drnewS("CustomerName") = customername
                drnewS("CustomerAddress") = clpaddressnew
                drnewS("CustomerPhoneNo") = customerPhoneno
                drnewS("DeliveryDate") = DeliveryDate
                drnewS("DeliveryName") = DeliveryName
                drnewS("SalesType") = CustomerSaleType
                drnewS("DeliveryNote") = "DELIVERY NOTE"
                dtHeaderDetails.Rows.Add(drnewS)

                '' added by ketan pC Tax Invoice changes AFTER GST
                '  Dim DSTaxInvoicedtl = obj.GetPCTaxInvoiceDetails(Sitecode:=Sitecode, BillNo:=dtView.Rows(0)("Billno").ToString())
                Dim DSTaxInvoicedtl
                If PrintFormatNo = 11 Then
                    DSTaxInvoicedtl = obj.GetPCTaxInvoiceDetailsforFormat11(Sitecode:=Sitecode, BillNo:=dtView.Rows(0)("Billno").ToString())
                End If

                If _PrintPreview = True Then
                    If KOTAndBillGeneration = False Then
                        If IsCounterCopyKot = False Then
                            Call CashMemoKOTPrintBasedOnPrintFormatNoSSRS(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, dayopenDate, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, ClientName:=ClientName, DayCloseReportPath:=DayCloseReportPath)
                        End If
                    End If
                    If IsFinalReceipt Then
                        If PrintFormatNo = 11 Then
                            GeneratePCTaxInvoicePrintformat11(DSTaxInvoicedtl, dtView.Rows(0)("Billno").ToString(), Sitecode, DayCloseReportPath)
                        End If
                        'GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath)
                        '' added by ketan pC Tax Invoice changes AFTER GST
                        ' GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath)

                    End If
                Else
                    If KOTAndBillGeneration = False Then
                        If IsCounterCopyKot = False Then
                            Call CashMemoKOTPrintBasedOnPrintFormatNoSSRS(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, dayopenDate, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, ClientName:=ClientName, DayCloseReportPath:=DayCloseReportPath)
                        End If
                    End If
                    If IsFinalReceipt Then
                        If PrintFormatNo = 11 Then
                            GeneratePCTaxInvoicePrintformat11(DSTaxInvoicedtl, dtView.Rows(0)("Billno").ToString(), Sitecode, DayCloseReportPath)
                        End If
                        'GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath)
                        '' added by ketan pC Tax Invoice changes AFTER GST
                        ' GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath)
                        '  GeneratePCTaxInvoicePrint(DSTaxInvoicedtl, dtView.Rows(0)("Billno").ToString(), Sitecode, DayCloseReportPath)
                    End If

                End If
            Else
                errorMsg = getValueByKey("CLSCMP031")
            End If
        Catch ex As Exception
            errorMsg = ex.Message
        End Try
    End Sub
    'code added by vipul for print format 9
    Public Sub CashMemoPrintFormat_GuruKrupa(ByVal dayopenDate As Date, ByVal LangCode As String, ByVal Currency As String, ByVal Sitecode As String, ByVal Type As String, ByVal DuplicatePrinting As String, ByVal DeletedUserid As String, ByVal AuthorisedUser As String, ByRef errorMsg As String, Optional ByVal GiftMsg As String = "", Optional ByVal BillAmt As String = "", Optional ByVal PaidAmt As String = "", Optional ByVal IsSavoy As Boolean = False, Optional ByVal MettlerConn As String = "", Optional ByVal IsArticleWiseKot As Boolean = False, Optional ByVal IsCounterCopy As Boolean = False, Optional ByVal IsFinalReceipt As Boolean = False, Optional ByVal ClientName As String = "", Optional ByVal DayCloseReportPath As String = "", Optional ByVal IsCounterCopyKot As Boolean = False, Optional ByVal KOTAndBillGeneration As Boolean = False)
        Try
            Dim strPrintMsg, sbDeliveryBillPrint As StringBuilder
            Dim strLineDetail As New StringBuilder
            Dim StrHeader, StrSubHeader, StrBody, StrCompanyInfo, StrTagLine, StrSubFooter, StrFooter, StrDuplicate, StrDeleteLine, StrCashMemoLine, StrCashierLine, StrSalesPersonLine As String
            Dim StrTokenNo, StrCustomerName As String
            Dim StrCMNo, StrCMDate, StrCMTime, StrCashier, StrSalesPerson As String
            Dim SiteLine, StrSiteTelline, StrAdrressLine, StrSiteReg1Line, StrSiteReg2Line As String
            Dim CLpLine, LineItemHeading, StrLineItem As String
            Dim CustomerNameLine, CLPPointsLine, CustmerPhoneNo As String
            Dim ItemCode, Desc, Qty, Rate, DiscAmt, DiscPer, TaxAmt, NetAmt, NetAmtTemp, RateAfterDisc, GrossAmt, FinScaleNo As String
            Dim TotalQtyLine, TakeAwayQuantity, TotalGrossAmtLine, TotalDiscAmtLine, SubTotalLine, NetAmtLine, TotalTaxAmtLine As String
            Dim TotalFooterTaxDetail, FooterTaxLine As String
            Dim TenderDetails, TenderLine As String
            Dim TotalPromotionalMsg, PromoMsgLine, StrPrintHeaderLine, StrPrintFooterLine As String
            Dim TermsNcond As String
            Dim strLine As String
            Dim strDblLine As String
            Dim strHomeDilery As String
            Dim isTakeAwayQtyPrint As Boolean = False
            errorMsg = ""
            Dim dtView As New DataTable
            Dim BoldLineLength As Int32 = 27
            Dim obj As New SpectrumBL.clsCashMemo()
            dtView = obj.GetCashMemo(CashMemoNo, Sitecode, LangCode)

            Dim dvItemDetail As New DataView(dtView, "", "", DataViewRowState.CurrentRows)
            'Dim DtUnique As DataTable = dvItemDetail.ToTable(True, "ArticleCode", "DISCRIPTION", "QUANTITY", "SELLINGPRICE", "GROSSAMT", "TOTALDISCOUNT", "TOTALDISCPERCENTAGE", "NETAMOUNT", "TOTALTAXAMOUNT")
            Dim DtUnique As DataTable = obj.GetBillDetailsDataForPrint(CashMemoNo, Sitecode, LangCode, True)
            Dim Dtcombo As DataTable = obj.GetComboDetailsDataForPrint(CashMemoNo, Sitecode, LangCode)
            If Not IsDBNull(dtView.Rows(0)("ServiceTaxAmount")) AndAlso Val(dtView.Rows(0)("ServiceTaxAmount")) > 0 Then
                CustomerSaleType = GetCustomerSaleType(dtView.Rows(0)("ServiceTaxAmount"))
            Else
                CustomerSaleType = ""
            End If
            If Not IsDBNull(dtView.Rows(0)("DeliveryPersonID")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("DeliveryPersonID")) Then
                DeliveryPersonName = dtView.Rows(0)("DeliveryPersonID").ToString()
            End If
            If DtUnique Is Nothing Then Exit Sub
            If dtView Is Nothing Then Exit Sub
            Dim SiteName As String
            SiteName = dtView.Rows(0)("SITEOFFICIALNAME").ToString()
            If ClientName <> "" Then
                Dim arrClientname = ClientName.Split(" ").ToArray()

                For i = 0 To arrClientname.Length - 1
                    If SiteName.ToUpper.Contains(arrClientname(i).ToString.ToUpper) Then
                        SiteName = SiteName.ToUpper.Replace(arrClientname(i).ToString.ToUpper.Trim, "").Trim
                    End If
                Next
            End If
            SiteName = SiteName.PadLeft((Val(LineLength - SiteName.Length) + SiteName.Length) / 2)
            '------------------
            SiteLine = SiteName.Trim
            Dim PhoneLine As String = dtView.Rows(0)("TELNO").ToString()
            Dim addressLine1 As String = dtView.Rows(0)("ADDRESSLINE1").ToString().Trim
            Dim addressLine2 As String = dtView.Rows(0)("ADDRESSLINE2").ToString().Trim
            Dim addressLine3 As String = dtView.Rows(0)("ADDRESSPINCODE").ToString().Trim
            If Type = "DCM" Then
                StrCMNo = getValueByKey("CLSCMP008") & dtView.Rows(0)("Billno").ToString()
            Else
                StrCMNo = getValueByKey("CLSCMP039") & ":" & dtView.Rows(0)("Billno").ToString()
                '---------------
            End If
            StrCMDate = dayopenDate.ToString("dd-MM-yyyy")
            strDayOpenDate = dayopenDate.ToShortDateString()
            If DuplicatePrinting <> String.Empty Then
                StrCMDate = Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())
            End If
            StrSalesPerson = getValueByKey("CLSCMP010") & dtView.Rows(0)("SALESEXECUTIVECODE").ToString()


            Dim CLPaddress As String = ""
            Dim CLPBalancePoints As String
            Dim clpaddressnew As String = ""
            Dim customername As String = ""
            Dim customerPhoneno As String = ""
            Dim DeliveryName As String = ""
            Dim DeliveryDate As DateTime

            If dtView.Rows(0)("CLPNO").ToString() <> String.Empty Then
                'CLpLine = "CLP Card No:" & dtView.Rows(0)("CLPNO").ToString()
                'CustomerNameLine = "Customer Name: " & dtView.Rows(0)("CLPNAME").ToString()
                'CLPPointsLine = "CLP Points Earned: " & FormatNumber(dtView.Rows(0)("CLPPoints").ToString())

                CLpLine = getValueByKey("CLSCMP011") & dtView.Rows(0)("CLPNO").ToString()

                CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CLPNAME").ToString()
                customername = dtView.Rows(0)("CLPNAME").ToString().Trim
                clpaddressnew = dtView.Rows(0)("CLPAddress").ToString()
                clpaddressnew = clpaddressnew.Trim
                CLPPointsLine = getValueByKey("CLSCMP013") & FormatNumber(Val(dtView.Rows(0)("CLPPoints").ToString()))
                customerPhoneno = dtView.Rows(0)("MobileNo").ToString()
                If dtView.Rows.Count() > 0 Then
                    '---- Changes By Mahesh : Customer Name is Not Mandatory for Home Delivery 
                    'If dtView.Rows(0)("HDName").ToString() <> String.Empty Then
                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        DeliveryDate = dtView.Rows(0)("HDDeliveryDate").ToString()
                        'strHomeDilery = strHomeDilery & getValueByKey("CLSCMP023") & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf
                        DeliveryName = DeliveryPersonName.Trim
                    End If
                End If
                If (dtView.Rows(0)("RedemptionPoints") IsNot DBNull.Value AndAlso dtView.Rows(0)("RedemptionPoints") > 0) Then
                    CLPPointsLine += vbCrLf + getValueByKey("CLSCMP034") & FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()))
                End If

                CLPaddress = dtView.Rows(0)("CLPAddress").ToString()
                If CLPaddress <> String.Empty Then
                    CLPaddress = getValueByKey("CLSCMP014") & dtView.Rows(0)("CLPAddress").ToString()
                End If

                CLPBalancePoints = String.Empty
                CLPBalancePoints = getValueByKey("CLSCMP015")
                Dim dblCLPPoints As Double

                dblCLPPoints = Val(dtView.Rows(0)("BalancePoints").ToString())
                'For Each drRow As DataRow In dtView.Select("Tendertypecode = 'CLPPoint'")
                '    dblCLPPoints -= drRow("RedemptionPoints")
                'Next

                Dim RedemptionPoints As DataRow = dtView.Select("Tendertypecode = 'CLPPoint'").FirstOrDefault()
                If (RedemptionPoints IsNot Nothing AndAlso RedemptionPoints("RedemptionPoints") IsNot DBNull.Value) Then
                    dblCLPPoints -= Val(RedemptionPoints("RedemptionPoints").ToString())
                End If

                CLPBalancePoints = CLPBalancePoints & FormatNumber(dblCLPPoints.ToString())
                If CLPaddress <> String.Empty Then
                    CLPaddress = SplitString(CLPaddress, LineLength).ToString()
                End If
                'If CLpLine <> Nothing AndAlso CLpLine.ToString().Length > 0 Then ' 3 lines commented for clp details
                '    StrSubFooter = StrSubFooter & CLpLine & vbCrLf
                'End If
                CustmerPhoneNo = getValueByKey("RP187") & " " & dtView.Rows(0)("Mobileno").ToString()
                StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
                'StrSubFooter = StrSubFooter & CLPBalancePoints & vbCrLf ''' 2 lines commented for clp details
                If CustomerSaleType = "Home Delivery" Then
                    StrSubFooter = StrSubFooter & CustmerPhoneNo & vbCrLf
                End If

                'StrSubFooter = StrSubFooter & CustmerPhoneNo & vbCrLf
                'StrSubFooter = StrSubFooter & CLPPointsLine & vbCrLf


            ElseIf dtView.Columns.Contains("CustomerNo") AndAlso dtView.Rows(0)("CustomerNo").ToString() <> String.Empty Then

                CLpLine = getValueByKey("CLSCMP038") & dtView.Rows(0)("CustomerNo").ToString()

                CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CustomerName").ToString()
                If CLpLine <> Nothing AndAlso CLpLine.ToString().Length > 0 Then
                    StrSubFooter = StrSubFooter & CLpLine & vbCrLf
                End If
                StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
            End If


            Dim TotalQty, TGrossAmt, TDiscAmt, TRateAfterDisc, TTaxtAmt As String
            Dim TNetAmt As String = 0.0
            Dim BillLineNO As String
            DtScannedMettlerBills = obj.GetMettlerBillDetailsDataForPrint(CashMemoNo, Sitecode)
            Dim ScaleNo As String
            Dim objItemSch As New clsIteamSearch()
            Dim dtScanedBillArticle As New DataTable
            Dim dtNewArticleScaledtls As New DataTable
            Dim dtTender As DataTable 'sagar
            dtNewArticleScaledtls.Columns.Add("Article", System.Type.GetType("System.String"))
            dtNewArticleScaledtls.Columns.Add("ScaleNo", System.Type.GetType("System.String"))
            'dtNewArticleScaledtls.Columns.Add("BillNo", System.Type.GetType("System.String"))
            For ScanBillIndex = 0 To DtScannedMettlerBills.Rows.Count - 1
                '------- GET Mettler Scanned Bill Details From Mettler DataBase 
                With DtScannedMettlerBills
                    ScaleNo = Val(.Rows(ScanBillIndex)("SCALE_NO"))
                    dtScanedBillArticle = objItemSch.GetScanedBillArticle(ScaleNo:=ScaleNo, BillNo:=.Rows(ScanBillIndex)("Bill_No"), BillIntDate:=.Rows(ScanBillIndex)("MettlerScaleBillDate"), MettlerConn:=MettlerConnString) '    ScaleBillIntDate = Val(.Rows(ScanBillIndex)("MettlerScaleBillDate"))

                    If dtScanedBillArticle IsNot Nothing Then

                        For Each drscale As DataRow In dtScanedBillArticle.Rows
                            Dim drnew As DataRow = dtNewArticleScaledtls.NewRow
                            drnew("Article") = drscale("LegacyArticleCode")
                            drnew("ScaleNo") = ScaleNo
                            ' drnew("BillNo") = drscale("BillNo")
                            dtNewArticleScaledtls.Rows.Add(drnew)
                        Next

                    End If
                End With

            Next
            DtUnique.Columns.Add("ScaleNo", System.Type.GetType("System.Int32"))
            'DtUnique.Columns.Add("BillNo", System.Type.GetType("System.String"))
            For Each drscaleno As DataRow In dtNewArticleScaledtls.Rows
                Dim dr() As DataRow = DtUnique.Select("ArticleCode= '" & drscaleno("Article") & "'")
                If dr.Length > 0 Then
                    dr(0)("ScaleNo") = drscaleno("ScaleNo")
                    'dr(0)("BillNo") = drscaleno("BillNo")
                End If
            Next
            If Not dtNewArticleScaledtls Is Nothing Then
                If dtNewArticleScaledtls.Rows.Count = 0 Then
                    For Each druniq As DataRow In DtUnique.Rows
                        druniq("ScaleNo") = 0
                        'druniq("BillNo") = 0
                    Next
                Else
                    For Each druniq As DataRow In DtUnique.Rows
                        If Convert.ToString(druniq("ScaleNo")) = "" Then
                            druniq("ScaleNo") = 0
                            'druniq("BillNo") = 0
                        End If
                    Next
                End If
            End If
            For Each dr As DataRow In DtUnique.Rows
                ItemCode = dr("ArticleCode").ToString()

                If (DisplayArticleFullName) Then
                    Desc = dr("ArticleFullName").ToString()
                Else
                    Desc = dr("DISCRIPTION").ToString()
                End If
                BillLineNO = dr("BillLineNO").ToString()
                If SpectrumCommon.ComboItemPrintingAllowed AndAlso Dtcombo IsNot Nothing AndAlso Dtcombo.Rows.Count > 0 Then
                    Dim drp() = Dtcombo.Select("ComboArticleCode='" & ItemCode & "' AND BillLineNO ='" & BillLineNO & "'")
                    If drp.Length > 0 Then
                        Dim articleDescriptionDictionary As New Dictionary(Of String, Integer)
                        For index = 0 To drp.Length - 1
                            If articleDescriptionDictionary.ContainsKey(drp(index)("ArticleName")) Then
                                articleDescriptionDictionary(drp(index)("ArticleName")) = articleDescriptionDictionary(drp(index)("ArticleName")) + drp(index)("Quantity")
                            Else
                                articleDescriptionDictionary.Add(drp(index)("ArticleName"), drp(index)("Quantity"))
                            End If
                        Next
                        Dim AddedDesc = GetMultilinedString(articleDescriptionDictionary)
                        Desc = Desc & vbCrLf & AddedDesc
                    End If
                End If
                FinScaleNo = dr("ScaleNo").ToString()

                Qty = dr("Quantity").ToString()
                Rate = dr("SellingPrice").ToString()
                RateAfterDisc = (dr("SellingPrice") - dr("TOTALDISCOUNT")).ToString()
                If CDbl(clsCommon.CheckIfBlank(Rate)) < 0 Then
                    Rate = CDbl(clsCommon.CheckIfBlank(Rate)) * -1
                End If
                DiscAmt = dr("TOTALDISCOUNT").ToString()
                DiscPer = dr("TOTALDISCPERCENTAGE").ToString()
                NetAmt = dr("NetAmount").ToString()
                GrossAmt = dr("GROSSAMT").ToString()
                TaxAmt = dr("TOTALTAXAMOUNT").ToString()


                If (AllowDecimalQty) Then
                    If (WeightScaleEnabled) Then
                        Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 3)
                    Else
                        Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 2)
                    End If
                Else
                    Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 0)
                End If

                'For i = Qty.Length To 4
                '    Qty = " " & Qty
                'Next
                Rate = FormatNumber(CDbl(clsCommon.CheckIfBlank(Rate)), 2)

                RateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(RateAfterDisc)), 2)
                'Rate = FormatCurrency(Rate, 2)
                'For i = Rate.Length To 8
                '    Rate = " " & Rate
                'Next
                DiscAmt = FormatNumber(CDbl(IIf(DiscAmt <> String.Empty, DiscAmt, 0)), 2)
                'For i = DiscAmt.Length To 8
                '    DiscAmt = " " & DiscAmt
                'Next
                DiscPer = FormatNumber(CDbl(IIf(DiscPer <> String.Empty, DiscPer, 0)), 2)
                DiscPer = DiscPer & "%"
                'For i = DiscPer.Length To 8
                '    DiscPer = " " & DiscPer
                'Next
                'Format of NetAmt with No Digits after Decimal
                NetAmt = FormatNumber(CDbl(NetAmt), 0)
                NetAmtTemp = FormatNumber(CDbl(NetAmt), 2)
                ' NetAmt = FormatNumber(CDbl(NetAmt), 2)
                GrossAmt = FormatNumber(CDbl(GrossAmt), 2)
                'GrossAmt = FormatNumber(CDbl(GrossAmt), 0)
                'For i = NetAmt.Length To 8
                '    NetAmt = " " & NetAmt
                'Next
                TaxAmt = FormatNumber(CDbl(IIf(TaxAmt <> String.Empty, TaxAmt, 0)), 2)
                'For i = TaxAmt.Length To 7
                '    TaxAmt = " " & TaxAmt
                'Next

                Dim TempNetAmt As String = "0"

                If Type = "CMWAmt" Then
                Else
                    If 28 - Desc.Length < Qty.Length Then
                        Select Case PrintFormatNo
                            Case 1
                            Case 2

                            Case Else
                                TempNetAmt = NetAmt
                                If Val(NetAmt) > 0 AndAlso Val(TaxAmt) > 0 Then
                                    TempNetAmt = CDbl(GrossAmt) - CDbl(TaxAmt)

                                End If
                        End Select
                    Else
                        Select Case PrintFormatNo
                            Case 1

                            Case 2

                            Case Else
                                TempNetAmt = NetAmt
                                If Val(NetAmt) > 0 AndAlso Val(TaxAmt) > 0 Then
                                    TempNetAmt = CDbl(GrossAmt) - CDbl(TaxAmt)
                                End If
                        End Select
                    End If
                End If
                ' Added For Font Changes as Requested By Client-------------
                ' strLineDetail.Append(StrLineItem)
                strLineDetail.Append(StrLineItem)
                '--------------------

                If CDbl(Qty.Trim) > 0 Then
                    TotalQty = CDbl(clsCommon.CheckIfBlank(TotalQty)) + CDbl(Qty.Trim)
                End If
                TDiscAmt = CDbl(clsCommon.CheckIfBlank(TDiscAmt)) + CDbl(DiscAmt.Trim)
                TGrossAmt = CDbl(clsCommon.CheckIfBlank(TGrossAmt)) + CDbl(dr("GROSSAMT").ToString())
                'TNetAmt = CDbl(clsCommon.CheckIfBlank(TNetAmt)) + CDbl(NetAmt.Trim)
                'TNetAmt = Format(Math.Round(Convert.ToDecimal(CDbl(clsCommon.CheckIfBlank(TNetAmt)) + CDbl(NetAmt.Trim))), "0.00")
                TNetAmt = Decimal.Add(FormatNumber(CDbl(NetAmtTemp), 2).ToString(), TNetAmt)
                TRateAfterDisc = CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)) + CDbl(RateAfterDisc.Trim)
                TTaxtAmt = CDbl(clsCommon.CheckIfBlank(TTaxtAmt)) + CDbl(TaxAmt.Trim)
            Next

            TotalQty = FormatNumber(CDbl(clsCommon.CheckIfBlank(TotalQty)), 2)
            'TDiscAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TDiscAmt)), 2)
            TDiscAmt = FormatNumber(dtView.Rows(0)("Discount"), 2)
            TGrossAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TGrossAmt)), 2)
            'TNetAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TNetAmt)), 2)
            TNetAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TNetAmt)), RoundOff), 2)
            TRateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)), 2)
            TTaxtAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TTaxtAmt)), 2)
            Dim TSubTotalAmt As String
            Dim TotalDisPercent As String
            If Type <> "CMWAmt" Then
                If CDbl(clsCommon.CheckIfBlank(TDiscAmt)) > 0 Then
                    TotalDisPercent = Math.Round((CDbl(clsCommon.CheckIfBlank(TDiscAmt)) / CDbl(clsCommon.CheckIfBlank(TGrossAmt))) * 100, 1).ToString()
                    TSubTotalAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TGrossAmt)) - CDbl(clsCommon.CheckIfBlank(TDiscAmt)), RoundOff), 0)
                    TDiscAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TDiscAmt)), RoundOff), 0)
                    Dim ObjclsCommon As New clsCommon
                    Dim offerno As String = ObjclsCommon.GetOffernoForRoundOff()
                    If DtUnique.Rows(0)("PromotionId").ToString().Contains(offerno) And offerno <> "" Then
                        ' TotalDiscAmtLine = "Round Off" & Space(BoldLineLength / 2 - "Round Off".Length) & " :" & TDiscAmt.PadLeft(BoldLineLength - ("Round Off".Length + Space(BoldLineLength / 2 - "Round Off".Length).Length + ":".Length))
                    Else
                        '  Dim CheckIfPositiveNumber = IIf(BoldLineLength / 2 - (String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).Length) > 0, BoldLineLength / 2 - String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).Length, 0)
                        '  TotalDiscAmtLine = String.Format(getValueByKey("CLSCMP019"), TotalDisPercent) & Space(CheckIfPositiveNumber) & " :" & TDiscAmt.PadLeft(BoldLineLength - (String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).Length + ": ".Length))
                    End If
                    '--------------------
                End If

                TGrossAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TGrossAmt)), RoundOff), 0)
                TNetAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TNetAmt)), RoundOff), 0)
                TRateAfterDisc = TRateAfterDisc & " " & Currency
                TTaxtAmt = TTaxtAmt & " " & Currency
                TotalGrossAmtLine = "Total Amt" & Space(5) & ":" & TGrossAmt.PadLeft(BoldLineLength - Val("Total Amt".Length + Space(5).Length))
                If (CDbl(TDiscAmt.Replace("INR", ""))) > 0 Then
                    NetAmtLine = getValueByKey("CLSCMP020") & Space(2) & ":" & TNetAmt.PadLeft(BoldLineLength - Val(getValueByKey("CLSCMP020").Length + Space(2).Length))
                End If
                '--------------------
                If _TaxDetail = True Then
                    Dim taxDetailsForBill As DataTable = obj.GetTaxDetailsForBillNo(Sitecode, dtView.Rows(0)("Billno").ToString())
                    If taxDetailsForBill IsNot Nothing AndAlso taxDetailsForBill.Rows.Count > 0 Then
                        For Each row As DataRow In taxDetailsForBill.Rows
                            'If CompositeTaxReqOnPrint = False AndAlso IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                            If IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                            Dim taxValue As String = row("TaxVAlue").ToString()
                            taxValue = FormatNumber(CDbl(clsCommon.CheckIfBlank(taxValue)), 2)
                            taxValue = taxValue & " " & Currency
                            FooterTaxLine = FooterTaxLine & row("TaxName").PadRight(LineLength - 16) & ":" & taxValue.PadLeft(14) & vbCrLf
                        Next
                    End If
                End If
                TotalFooterTaxDetail = FooterTaxLine
                If KOTAndBillGeneration = True Then
                    dtTender = obj.GetTenderTypeByRefBillNo(dtView.Rows(0)("BILLNO").ToString())
                    If dtTender.Rows.Count = 0 Then
                        dtTender = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                    End If
                Else
                    dtTender = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                End If
                ' dtTender = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                For Each drTender As DataRow In dtTender.Rows
                    If UCase(obj.GetDefaultConfigValue(Sitecode, "CVInfoReqOnPrint")) = "FALSE" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)") Then
                        Continue For
                    End If
                    Dim amounttendered As String = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(drTender("AMOUNTTENDERED").ToString)), RoundOff), 0)
                    Dim tender As String = drTender("CurrencyCode").ToString() & " " & amounttendered & vbCrLf
                    '----------------
                    Dim tenderName As String
                    'Added by Rohit
                    If drTender("CardNO").ToString() <> "" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(I)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(R)") Then
                        tenderName = drTender("TENDERHEADNAME").ToString() & "(" & drTender("CardNO").ToString() & ") "
                    Else
                        tenderName = drTender("TENDERHEADNAME").ToString()
                    End If
                    'End editing
                    If Not drTender("AMOUNTINCURRENCY") Is DBNull.Value AndAlso drTender("AMOUNTINCURRENCY") <> 0 Then '0000404
                        tender = drTender("CurrencyCode").ToString() & " " & FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(drTender("AMOUNTINCURRENCY").ToString())), RoundOff), 0) & vbCrLf
                    End If
                    If drTender("TENDERTYPECODE").ToString() = "CLPPoint" Then
                        tender = FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()), 0)
                    End If

                Next

                If (_IsDisplayTotalSaving) Then
                    If (Not String.IsNullOrEmpty(TDiscAmt) AndAlso Not String.Equals(TDiscAmt, "0.00")) Then
                        Dim totalSavingAmount As String = String.Format(getValueByKey("CLSCMP033"), Currency, TDiscAmt.Replace(Currency, String.Empty).Trim)
                    Else
                    End If
                Else
                End If


                '--------------------
                'Company and Tin Info
                Dim NoOfReprints As String = IIf(IsDBNull(dtView.Rows(0)("NoofReprints")), String.Empty, dtView.Rows(0)("NoofReprints")).ToString()
                Dim CompanyName As String = dtView.Rows(0)("SITEOFFICIALNAME") 'obj.GetDefaultConfigValue(Sitecode, "CompanyName")
                ''Dim remarks As String = dtView.Rows(0)("Remark")
                'If Not IsDBNull(dtView.Rows(0)("Remark")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("Remark")) Then
                '    Dim remarks As String = dtView.Rows(0)("Remark")
                '    remarks = SplitString(getValueByKey("cashmemoprint.discountremark") & remarks, LineLength).ToString()
                '    StrCompanyInfo = StrCompanyInfo & remarks.TrimEnd() & vbCrLf
                'End If

                'If Not String.IsNullOrEmpty(NoOfReprints) Then
                '    StrCompanyInfo = StrCompanyInfo & "Re-Print Number : " & NoOfReprints & vbCrLf
                '    StrCompanyInfo = StrCompanyInfo & "Re-Print Date : " & DateTime.Now.ToShortDateString() & vbCrLf
                'End If
                'Company and Tin Info End

                Dim dtPromo As DataTable = dvItemDetail.ToTable(True, "PROMOTIONALMSGID", "PROMOMESS")
                For Each drTerms As DataRow In dtPromo.Select("", "PROMOTIONALMSGID", DataViewRowState.CurrentRows)
                    If Not String.IsNullOrEmpty(drTerms("PROMOMESS").ToString()) Then
                        PromoMsgLine = PromoMsgLine & drTerms("PROMOMESS").ToString() & vbCrLf
                    End If
                Next

                TotalPromotionalMsg = PromoMsgLine
                strPrintMsg = New StringBuilder()
                sbDeliveryBillPrint = New StringBuilder()

                'If dtView.Rows.Count() > 0 Then
                '    '---- Changes By Mahesh : Customer Name is Not Mandatory for Home Delivery 
                '    'If dtView.Rows(0)("HDName").ToString() <> String.Empty Then
                '    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                '        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP022") & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP023") & dtView.Rows(0)("HDName").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & SplitString(getValueByKey("CLSCMP024") & dtView.Rows(0)("HDAddress").ToString(), LineLength).ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP025") & dtView.Rows(0)("HDTelNo").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP026") & dtView.Rows(0)("HDEmail").ToString() & vbCrLf

                '    Else
                '        If _CustomerNameRequiredInKOT AndAlso CustomerNameLine <> Nothing Then
                '            StrCustomerName = CustomerNameLine.ToUpper.Replace("CUSTOMER", "").Trim()

                '        End If
                '        If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                '            If Not AllowBillingOnlyAfterSelectionOfSalesType Then
                '                CustomerSaleType = String.Empty
                '            End If
                '            Dim StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - CustomerSaleType.Length) & CustomerSaleType & vbCrLf & vbCrLf
                '        End If
                '    End If
                'End If
                Dim dtPrint As New DataTable
                Dim objPrint As New SpectrumBL.clsCommon
                dtPrint = objPrint.GetPrintingDetails(Type)
                'If Not dtPrint Is Nothing Then
                '    Dim filter As String = ""
                '    Dim dv As New DataView(dtPrint, filter, "Sequenceno", DataViewRowState.CurrentRows)
                '    filter = "TOPBOTTOM='Bottom'"
                '    dv.RowFilter = filter
                '    For Each drview As DataRowView In dv
                '        If drview("ReceiptText").ToString().Length >= 25 Then
                '        Else
                '        End If
                '    Next

                'End If



                ''''''''
                Dim TopBottomDisplayValue As String = "FINAL RECEIPT"
                If Not dtPrint Is Nothing Then
                    Dim filter As String = ""
                    Dim dv As New DataView(dtPrint, filter, "Sequenceno", DataViewRowState.CurrentRows)
                    filter = "TOPBOTTOM='Top'"
                    dv.RowFilter = filter
                    For Each drview As DataRowView In dv
                        'If drview("ReceiptText").ToString().Length >= 25 Then
                        '    Dim a = drview("ReceiptText").ToString()
                        'Else
                        'End If
                        If drview("ReceiptText").ToString().Length >= 4 Then
                            TopBottomDisplayValue = drview("ReceiptText").ToString()
                        Else
                        End If
                    Next
                End If
                'added by khusrao to print return amount on bill
                If (dtView.Rows.Count() > 0 And String.IsNullOrEmpty(DeliveryPersonName)) Then
                    If Val(PaidAmt) > 0 Then 'PaidAmt
                        Dim retAmt As Double
                        If CDbl(PaidAmt) >= CDbl(BillAmt) Then
                            retAmt = CDbl(PaidAmt) - CDbl(BillAmt)
                        End If
                        If retAmt <> 0 Then
                            Dim returnAmtFooter = vbCrLf & getValueByKey("CLIST04") & " " & FormatNumber(CDbl(PaidAmt), 2) & vbCrLf 'PaidAmt
                            returnAmtFooter = returnAmtFooter & getValueByKey("CLIST05") & " " & FormatNumber(CDbl(retAmt), 2) & vbCrLf 'retAmt
                            Dim dr = dtPrint.NewRow
                            If dtPrint.Rows.Count <> 0 Then
                                dr("TOPBOTTOM") = "RETURNAMTFOOTER"
                                dr("SEQUENCENO") = dtPrint.Rows(dtPrint.Rows.Count - 1)("SEQUENCENO") + 1
                                dr("RECEIPTTEXT") = returnAmtFooter.Trim
                                dr("ALIGN") = dtPrint.Rows(dtPrint.Rows.Count - 1)("ALIGN")
                                dr("WIDTH") = dtPrint.Rows(dtPrint.Rows.Count - 1)("WIDTH")
                                dr("HEIGHT") = dtPrint.Rows(dtPrint.Rows.Count - 1)("HEIGHT")
                                dr("BOLD") = dtPrint.Rows(dtPrint.Rows.Count - 1)("BOLD")
                            Else
                                dr("TOPBOTTOM") = "RETURNAMTFOOTER"
                                dr("SEQUENCENO") = dtPrint.Rows.Count + 1
                                dr("RECEIPTTEXT") = returnAmtFooter
                                dr("ALIGN") = "Left"
                                'dr("WIDTH") = dtPrint.Rows(0)("WIDTH")
                                'dr("HEIGHT") = dtPrint.Rows(0)("HEIGHT")
                                'dr("BOLD") = dtPrint.Rows(0)("BOLD")
                            End If
                            dtPrint.Rows.Add(dr)
                        End If

                    End If

                End If

                'dulpicate bill
                StrDuplicate = DuplicatePrinting

                If Not String.IsNullOrEmpty(StrDuplicate) Then
                    Dim dr1 = dtPrint.NewRow
                    If dtPrint.Rows.Count <> 0 Then
                        dr1("TOPBOTTOM") = "DUPLICATEBILL"
                        dr1("SEQUENCENO") = dtPrint.Rows(dtPrint.Rows.Count - 1)("SEQUENCENO") + 1
                        dr1("RECEIPTTEXT") = StrDuplicate.Trim
                        dr1("ALIGN") = dtPrint.Rows(dtPrint.Rows.Count - 1)("ALIGN")
                        dr1("WIDTH") = dtPrint.Rows(dtPrint.Rows.Count - 1)("WIDTH")
                        dr1("HEIGHT") = dtPrint.Rows(dtPrint.Rows.Count - 1)("HEIGHT")
                        dr1("BOLD") = dtPrint.Rows(dtPrint.Rows.Count - 1)("BOLD")
                        dtPrint.Rows.Add(dr1)
                    Else
                        dr1("TOPBOTTOM") = "DUPLICATEBILL"
                        dr1("SEQUENCENO") = dtPrint.Rows.Count + 1
                        dr1("RECEIPTTEXT") = StrDuplicate.Trim
                        'dr1("ALIGN") = "Left"
                        'dr1("WIDTH") = "WIDTH"
                        'dr1("HEIGHT") = "HEIGHT"
                        'dr1("BOLD") = "BOLD"
                        dtPrint.Rows.Add(dr1)
                    End If

                End If

                '''''''''''''''''''''''''''''''''''
                If DtUnique.Rows.Count > 0 Then
                    For index = 0 To DtUnique.Rows.Count - 1
                        DtUnique(index)("GROSSAMT") = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(DtUnique(index)("GROSSAMT"))), RoundOff), 0)
                    Next
                    DtUnique.AcceptChanges()
                End If
                Dim dtHeaderDetails As New DataTable
                dtHeaderDetails.Columns.Add("Header", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("Footer", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("ClientName", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("INR", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("StoreName", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("Address", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("PhoneNo", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("FINALRECEIPT", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("TotalAmt", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("LessDisPer", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("LessDisAmt", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("SubTotal", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("TotalToPay", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("CashierName", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("CustomerName", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("CustomerAddress", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("CustomerPhoneNo", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("DeliveryDate", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("DeliveryName", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("SalesType", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("DeliveryNote", System.Type.GetType("System.String"))
                Dim drnewS = dtHeaderDetails.NewRow
                drnewS("Header") = "Submit Counter Copy at Scale"
                drnewS("Footer") = "BILL PAID"
                drnewS("ClientName") = ClientName.Trim
                drnewS("INR") = 0
                drnewS("StoreName") = SiteName.Trim
                drnewS("Address") = addressLine1.Trim & Space(1) & addressLine2.Trim & Space(1) & addressLine3.Trim
                drnewS("PhoneNo") = PhoneLine.Trim
                drnewS("FINALRECEIPT") = "FINAL RECEIPT"
                drnewS("TotalAmt") = TGrossAmt.Trim
                If TotalDisPercent Is Nothing Then
                    drnewS("LessDisPer") = 0
                Else
                    drnewS("LessDisPer") = TotalDisPercent
                End If

                drnewS("LessDisAmt") = TDiscAmt
                If TSubTotalAmt Is Nothing Then
                    drnewS("SubTotal") = 0
                Else
                    drnewS("SubTotal") = TSubTotalAmt
                End If

                drnewS("TotalToPay") = TNetAmt
                drnewS("CashierName") = LoginUserName
                drnewS("CustomerName") = customername
                drnewS("CustomerAddress") = clpaddressnew
                drnewS("CustomerPhoneNo") = customerPhoneno
                drnewS("DeliveryDate") = DeliveryDate
                drnewS("DeliveryName") = DeliveryName
                drnewS("SalesType") = CustomerSaleType
                drnewS("DeliveryNote") = "DELIVERY NOTE"
                dtHeaderDetails.Rows.Add(drnewS)

                '' added by ketan pC Tax Invoice changes AFTER GST
                Dim DSTaxInvoicedtl = obj.GetGuruKrupaTaxInvoiceDetails(SiteCode:=Sitecode, BillNo:=dtView.Rows(0)("Billno").ToString())



                ''

                'Dim dtPrintCopy As DataTable
                'If dtPrint.Rows.Count > 0 Then
                '    If dtPrint.Select("TOPBottom='Bottom'").Count > 0 Then
                '        dtPrintCopy = dtPrint.Select("TOPBottom<>'TOP'").CopyToDataTable()
                '    Else
                '        dtPrintCopy = dtPrint.Copy()
                '    End If
                'Else
                '    dtPrintCopy = dtPrint.Copy()
                'End If
                ''

                If _PrintPreview = True Then
                    If (KOTBillPrintingRequired) Then
                        If KOTAndBillGeneration = False Then
                            If IsCounterCopyKot = False Then
                                Call CashMemoKOTPrintBasedOnPrintFormatNoSSRS(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, dayopenDate, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, ClientName:=ClientName, DayCloseReportPath:=DayCloseReportPath)
                            End If
                        End If
                    End If
                    
                    If IsFinalReceipt Then
                        If PrintFormatNo = 5 Then
                            If IsCounterCopyKot = False Then
                                If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                                    GenerateCashMemoFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath)
                                End If
                            End If
                        End If
                        'GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath)
                        '' added by ketan pC Tax Invoice changes AFTER GST
                        ' GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath)
                        GenerateGuruKrupaTaxInvoicePrint(DSTaxInvoicedtl, dtView.Rows(0)("Billno").ToString(), Sitecode, DayCloseReportPath, dtPrint)
                    End If
                Else
                    If (KOTBillPrintingRequired) Then
                        If KOTAndBillGeneration = False Then
                            If IsCounterCopyKot = False Then
                                Call CashMemoKOTPrintBasedOnPrintFormatNoSSRS(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, dayopenDate, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, ClientName:=ClientName, DayCloseReportPath:=DayCloseReportPath)
                            End If
                        End If
                    End If
                   
                    If IsFinalReceipt Then
                        If PrintFormatNo = 5 Then
                            If IsCounterCopyKot = False Then
                                If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                                    GenerateCashMemoFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath)
                                End If
                            End If

                        End If
                        'GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath)
                        '' added by ketan pC Tax Invoice changes AFTER GST
                        ' GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath)
                        GenerateGuruKrupaTaxInvoicePrint(DSTaxInvoicedtl, dtView.Rows(0)("Billno").ToString(), Sitecode, DayCloseReportPath, dtPrint)
                    End If

                End If
            Else
                errorMsg = getValueByKey("CLSCMP031")
            End If
        Catch ex As Exception
            errorMsg = ex.Message
        End Try
    End Sub
    Private Function GenerateaxInvoiceBrandWisePrint(ByRef DSTaxInvoiceDetails As DataSet, ByVal BillNo As String, ByVal SiteCode As String, ByVal DayCloseReportPath As String, ByVal BrandName As String) As Boolean
        Try

            Dim path As String = ""
            Dim reportViewer2 As New LocalReport
            Dim appPath As String
            Dim dtView As New DataTable
            appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\GSTCMPrintBrandWsie.rdl")
            reportViewer2.ReportPath = appPath
            reportViewer2.SetParameters(New ReportParameter("BillNo", BillNo))
            reportViewer2.SetParameters(New ReportParameter("SiteCode", SiteCode))
            reportViewer2.SetParameters(New ReportParameter("BrandName", BrandName))

            Dim DataSource1 As New ReportDataSource("DSArticleDetails", DSTaxInvoiceDetails.Tables(0))
            Dim DataSource2 As New ReportDataSource("DSSubTotal", DSTaxInvoiceDetails.Tables(1))
            Dim DataSource3 As New ReportDataSource("DSTotal", DSTaxInvoiceDetails.Tables(2))
            Dim DataSource4 As New ReportDataSource("DSTender", DSTaxInvoiceDetails.Tables(3))
            Dim DataSource5 As New ReportDataSource("DSTAX", DSTaxInvoiceDetails.Tables(4))
            Dim DataSource6 As New ReportDataSource("DSClientDetails", DSTaxInvoiceDetails.Tables(5))
            Dim DataSource7 As New ReportDataSource("DSCustDetails", DSTaxInvoiceDetails.Tables(6))
            reportViewer2.DataSources.Add(DataSource1)
            reportViewer2.DataSources.Add(DataSource2)
            reportViewer2.DataSources.Add(DataSource3)
            reportViewer2.DataSources.Add(DataSource4)
            reportViewer2.DataSources.Add(DataSource5)
            reportViewer2.DataSources.Add(DataSource6)
            reportViewer2.DataSources.Add(DataSource7)
            reportViewer2.Refresh()

            If _PrintPreview = True Then
                Dim mybytes As [Byte]() = reportViewer2.Render("Pdf")
                'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
                Dim obj As New clsCommon
                'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
                path = DayCloseReportPath & "\TaxInvoice_" & BrandName & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                Using fs As FileStream = File.Create(path)
                    fs.Write(mybytes, 0, mybytes.Length)
                End Using
                Process.Start(path)
                If PrintFormatNo = 4 Then
                    dtView = BillNoSiteCodeDt(BillNo, SiteCode)
                    SendInvoiceOnMail(path, dtView)
                    SendSMSForCM(dtView)
                End If
            Else
                If IsInvoicePrintFlag = False Then
                    Export(reportViewer2)
                    Print()
                Else
                    If IsInvoicePrintRequired Then
                        Export(reportViewer2)
                        Print()
                    End If
                End If

                Dim mybytes As [Byte]() = reportViewer2.Render("Pdf") 'vipul
                path = DayCloseReportPath & "\FinalReceipt_" & BrandName & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                Using fs As FileStream = File.Create(path)
                    fs.Write(mybytes, 0, mybytes.Length)
                End Using
                If PrintFormatNo = 4 Then
                    dtView = BillNoSiteCodeDt(BillNo, SiteCode)
                    SendInvoiceOnMail(path, dtView)
                    SendSMSForCM(dtView)
                End If
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Function GeneratePCTaxInvoicePrint(ByRef DSTaxInvoiceDetails As DataSet, ByVal BillNo As String, ByVal SiteCode As String, ByVal DayCloseReportPath As String) As Boolean
        Try

            Dim path As String = ""
            Dim reportViewer2 As New LocalReport
            Dim appPath As String
            Dim dtView As New DataTable
            appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\GSTCMPrint.rdl")
            reportViewer2.ReportPath = appPath
            reportViewer2.SetParameters(New ReportParameter("BillNo", BillNo))
            reportViewer2.SetParameters(New ReportParameter("SiteCode", SiteCode))

            Dim DataSource1 As New ReportDataSource("DSArticleDetails", DSTaxInvoiceDetails.Tables(0))
            Dim DataSource2 As New ReportDataSource("DSSubTotal", DSTaxInvoiceDetails.Tables(1))
            Dim DataSource3 As New ReportDataSource("DSTotal", DSTaxInvoiceDetails.Tables(2))
            Dim DataSource4 As New ReportDataSource("DSTender", DSTaxInvoiceDetails.Tables(3))
            Dim DataSource5 As New ReportDataSource("DSTAX", DSTaxInvoiceDetails.Tables(4))
            Dim DataSource6 As New ReportDataSource("DSClientDetails", DSTaxInvoiceDetails.Tables(5))
            Dim DataSource7 As New ReportDataSource("DSCustDetails", DSTaxInvoiceDetails.Tables(6))
            reportViewer2.DataSources.Add(DataSource1)
            reportViewer2.DataSources.Add(DataSource2)
            reportViewer2.DataSources.Add(DataSource3)
            reportViewer2.DataSources.Add(DataSource4)
            reportViewer2.DataSources.Add(DataSource5)
            reportViewer2.DataSources.Add(DataSource6)
            reportViewer2.DataSources.Add(DataSource7)
            reportViewer2.Refresh()

            If _PrintPreview = True Then
                Dim mybytes As [Byte]() = reportViewer2.Render("Pdf")
                'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
                Dim obj As New clsCommon
                'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
                path = DayCloseReportPath & "\TaxInvoice_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                Using fs As FileStream = File.Create(path)
                    fs.Write(mybytes, 0, mybytes.Length)
                End Using
                Process.Start(path)
                If PrintFormatNo = 4 Then
                    dtView = BillNoSiteCodeDt(BillNo, SiteCode)
                    SendInvoiceOnMail(path, dtView)
                    SendSMSForCM(dtView)
                End If
            Else
                If IsInvoicePrintFlag = False Then
                    Export(reportViewer2)
                    Print()
                Else
                    If IsInvoicePrintRequired Then
                        Export(reportViewer2)
                        Print()
                    End If
                End If

                Dim mybytes As [Byte]() = reportViewer2.Render("Pdf") 'vipul
                path = DayCloseReportPath & "\FinalReceipt_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                Using fs As FileStream = File.Create(path)
                    fs.Write(mybytes, 0, mybytes.Length)
                End Using
                If PrintFormatNo = 4 Then
                    dtView = BillNoSiteCodeDt(BillNo, SiteCode)
                    SendInvoiceOnMail(path, dtView)
                    SendSMSForCM(dtView)
                End If
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function BillNoSiteCodeDt(ByVal BillNo As String, ByVal SiteCode As String) As DataTable
        Try
            Dim dtView As New DataTable
            dtView.Columns.Add("BILLNO", GetType(String))
            dtView.Columns.Add("SITECODE", GetType(String))
            dtView.Rows.Add(BillNo, SiteCode)
            Return dtView
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    'code added by vipul for barcode printing
    Public Function GeneratePCTaxInvoicSplitBillPrint(ByRef DSTaxInvoiceDetails As DataSet, ByVal BillNo As String, ByVal SiteCode As String, ByVal DayCloseReportPath As String, ByVal Liqueur As Boolean) As Boolean
        Try

            Dim path As String = ""
            Dim reportViewer2 As New LocalReport
            Dim appPath As String

            appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\GSTCMPrint.rdl")
            reportViewer2.ReportPath = appPath
            reportViewer2.SetParameters(New ReportParameter("BillNo", BillNo))
            reportViewer2.SetParameters(New ReportParameter("SiteCode", SiteCode))

            Dim DataSource1 As New ReportDataSource("DSArticleDetails", DSTaxInvoiceDetails.Tables(0))
            Dim DataSource2 As New ReportDataSource("DSSubTotal", DSTaxInvoiceDetails.Tables(1))
            Dim DataSource3 As New ReportDataSource("DSTotal", DSTaxInvoiceDetails.Tables(2))
            Dim DataSource4 As New ReportDataSource("DSTender", DSTaxInvoiceDetails.Tables(3))
            Dim DataSource5 As New ReportDataSource("DSTAX", DSTaxInvoiceDetails.Tables(4))
            Dim DataSource6 As New ReportDataSource("DSClientDetails", DSTaxInvoiceDetails.Tables(5))
            Dim DataSource7 As New ReportDataSource("DSCustDetails", DSTaxInvoiceDetails.Tables(6))
            reportViewer2.DataSources.Add(DataSource1)
            reportViewer2.DataSources.Add(DataSource2)
            reportViewer2.DataSources.Add(DataSource3)
            reportViewer2.DataSources.Add(DataSource4)
            reportViewer2.DataSources.Add(DataSource5)
            reportViewer2.DataSources.Add(DataSource6)
            reportViewer2.DataSources.Add(DataSource7)
            reportViewer2.Refresh()

            If _PrintPreview = True Then
                Dim mybytes As [Byte]() = reportViewer2.Render("Pdf")
                'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
                Dim obj As New clsCommon
                'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
                If Liqueur Then
                    path = DayCloseReportPath & "\TaxInvoiceLiqueur_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                Else
                    path = DayCloseReportPath & "\TaxInvoice_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                End If

                Using fs As FileStream = File.Create(path)
                    fs.Write(mybytes, 0, mybytes.Length)
                End Using
                Process.Start(path)
            Else
                Export(reportViewer2)
                Print()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Function GenerateArticleLabel(ByRef DSTaxInvoiceDetails As DataSet, ByVal ArticleCode As String, ByVal SiteCode As String, ByVal BarCode As String, ByVal DayCloseReportPath As String, Optional ByVal NoOfCopies As Integer = 1) As Boolean
        Try

            Dim path As String = ""
            Dim reportViewer2 As New LocalReport
            Dim appPath As String

            appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\BarcodeLbl.rdl")
            reportViewer2.ReportPath = appPath
            reportViewer2.SetParameters(New ReportParameter("ArticleCode", ArticleCode))
            reportViewer2.SetParameters(New ReportParameter("SiteCode", SiteCode))
            reportViewer2.SetParameters(New ReportParameter("BarCode", BarCode))

            Dim DataSource1 As New ReportDataSource("DSArticleLblDetails", DSTaxInvoiceDetails.Tables(0))
            Dim DataSource2 As New ReportDataSource("DSArtNeutritionDetails", DSTaxInvoiceDetails.Tables(1))

            reportViewer2.DataSources.Add(DataSource1)
            reportViewer2.DataSources.Add(DataSource2)

            reportViewer2.Refresh()

            'If _PrintPreview = True Then
            '    Dim mybytes As [Byte]() = reportViewer2.Render("Pdf")
            '    'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            '    Dim obj As New clsCommon
            '    'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
            '    path = DayCloseReportPath & "\BarcodeLbl_" & ArticleCode & "_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
            '    Using fs As FileStream = File.Create(path)
            '        fs.Write(mybytes, 0, mybytes.Length)
            '    End Using
            '    Process.Start(path)
            'Else

            '    For index = 1 To NoOfCopies
            '        Export(reportViewer2)
            '        Print(True)
            '    Next


            'End If
            Dim mybytes As [Byte]() = reportViewer2.Render("Pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            Dim obj As New clsCommon
            'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
            path = DayCloseReportPath & "\BarcodeLbl_" & ArticleCode & "_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            Process.Start(path)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    'code added by irfan on 24-08-2017 for printing format 8
    Public Sub CashMemoPrintFormatA4_SSRS(ByVal dayopenDate As Date, ByVal LangCode As String, ByVal Currency As String, ByVal Sitecode As String, ByVal Type As String, ByVal DuplicatePrinting As String, ByVal DeletedUserid As String, ByVal AuthorisedUser As String, ByRef errorMsg As String, Optional ByVal GiftMsg As String = "", Optional ByVal BillAmt As String = "", Optional ByVal PaidAmt As String = "", Optional ByVal IsSavoy As Boolean = False, Optional ByVal MettlerConn As String = "", Optional ByVal IsArticleWiseKot As Boolean = False, Optional ByVal IsCounterCopy As Boolean = False, Optional ByVal IsFinalReceipt As Boolean = False, Optional ByVal ClientName As String = "", Optional ByVal DayCloseReportPath As String = "", Optional ByVal IsCounterCopyKot As Boolean = False, Optional ByVal KOTAndBillGeneration As Boolean = False, Optional ByVal EvasPizzaChanges As Boolean = False, Optional ByVal isratevisible As Boolean = False)
        Try
            Dim strPrintMsg, sbDeliveryBillPrint As StringBuilder
            Dim strLineDetail As New StringBuilder
            Dim StrHeader, StrSubHeader, StrBody, StrCompanyInfo, StrTagLine, StrSubFooter, StrFooter, StrDuplicate, StrDeleteLine, StrCashMemoLine, StrCashierLine, StrSalesPersonLine As String
            Dim StrTokenNo, StrCustomerName As String
            Dim StrCMNo, StrCMDate, StrCMTime, StrCashier, StrSalesPerson As String
            Dim SiteLine, StrSiteTelline, StrAdrressLine, StrSiteReg1Line, StrSiteReg2Line As String
            Dim CLpLine, LineItemHeading, StrLineItem As String
            Dim CustomerNameLine, CLPPointsLine, CustmerPhoneNo As String
            Dim ItemCode, Desc, Qty, Rate, DiscAmt, DiscPer, TaxAmt, NetAmt, NetAmtTemp, RateAfterDisc, GrossAmt, FinScaleNo As String
            Dim TotalQtyLine, TakeAwayQuantity, TotalGrossAmtLine, TotalDiscAmtLine, SubTotalLine, NetAmtLine, TotalTaxAmtLine As String
            Dim TotalFooterTaxDetail, FooterTaxLine As String
            Dim TenderDetails, TenderLine As String
            Dim TotalPromotionalMsg, PromoMsgLine, StrPrintHeaderLine, StrPrintFooterLine As String
            Dim TermsNcond As String
            Dim strLine As String
            Dim strDblLine As String
            Dim strHomeDilery As String
            Dim isTakeAwayQtyPrint As Boolean = False
            errorMsg = ""
            Dim dtView As New DataTable
            Dim BoldLineLength As Int32 = 27
            'added by khusrao adil on 10-07-2017
            Dim objcom As New clsCommon
            Dim IsPosPassKey As Boolean = objcom.GetCLPProgram()
            Dim obj As New SpectrumBL.clsCashMemo()
            ' dtView = obj.GetCashMemo(CashMemoNo, Sitecode, LangCode)


            'Dim DtUnique As DataTable = dvItemDetail.ToTable(True, "ArticleCode", "DISCRIPTION", "QUANTITY", "SELLINGPRICE", "GROSSAMT", "TOTALDISCOUNT", "TOTALDISCPERCENTAGE", "NETAMOUNT", "TOTALTAXAMOUNT")
            Dim ds As New DataSet
            ' ds = obj.GetBillDetailsDataForPrint(CashMemoNo, Sitecode)             'new method is created for cakeology on 10/9/2017
            ds = obj.GetBillDetailsDataForPrintForA4(CashMemoNo, Sitecode, Type)
            Dim Dtheader As DataTable = ds.Tables("dsheader")
            Dim DtUnique As DataTable = ds.Tables("dsUnique")
            dtView = DtUnique.Copy
            Dim dvItemDetail As New DataView(dtView, "", "", DataViewRowState.CurrentRows)

            Dim Dtcombo As DataTable = obj.GetComboDetailsDataForPrint(CashMemoNo, Sitecode, LangCode)
            If Not IsDBNull(dtView.Rows(0)("ServiceTaxAmount")) AndAlso Val(dtView.Rows(0)("ServiceTaxAmount")) > 0 Then
                CustomerSaleType = GetCustomerSaleType(dtView.Rows(0)("ServiceTaxAmount"))
            Else
                CustomerSaleType = ""
            End If
            If Not IsDBNull(dtView.Rows(0)("DeliveryPersonID")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("DeliveryPersonID")) Then
                DeliveryPersonName = dtView.Rows(0)("DeliveryPersonID").ToString()
            End If

            If DtUnique Is Nothing Then Exit Sub
            If dtView Is Nothing Then Exit Sub

            Dim SiteName As String
            StrDuplicate = DuplicatePrinting
            'StrDeleteLine = DeletedUserid
            '  ----------------------------------------------- code commented--------------------------------------
            SiteName = dtView.Rows(0)("SITEOFFICIALNAME").ToString()
            'If ClientName <> "" Then
            '    Dim arrClientname = ClientName.Split(" ").ToArray()

            '    For i = 0 To arrClientname.Length - 1
            '        If SiteName.ToUpper.Contains(arrClientname(i).ToString.ToUpper) Then
            '            SiteName = SiteName.ToUpper.Replace(arrClientname(i).ToString.ToUpper.Trim, "").Trim
            '        End If
            '    Next
            'End If

            ' SiteName = SiteName.PadLeft((Val(LineLength - SiteName.Length) + SiteName.Length) / 2)
            ' ''------------------
            ''SiteLine = SiteName.Trim
            ''Dim PhoneLine As String = dtView.Rows(0)("TELNO").ToString()
            ''Dim addressLine1 As String = dtView.Rows(0)("ADDRESSLINE1").ToString().Trim
            ''Dim addressLine2 As String = dtView.Rows(0)("ADDRESSLINE2").ToString().Trim
            ''Dim addressLine3 As String = dtView.Rows(0)("ADDRESSPINCODE").ToString().Trim
            ''If Type = "DCM" Then
            ''    StrCMNo = getValueByKey("CLSCMP008") & dtView.Rows(0)("Billno").ToString()
            ''Else
            ''    StrCMNo = getValueByKey("CLSCMP039") & ":" & dtView.Rows(0)("Billno").ToString()
            ''    '---------------
            ''End If
            ''StrCMDate = dayopenDate.ToString("dd-MM-yyyy")
            ''strDayOpenDate = dayopenDate.ToShortDateString()
            ''If DuplicatePrinting <> String.Empty Then
            ''    StrCMDate = Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())
            ''End If


            ''StrSalesPerson = getValueByKey("CLSCMP010") & dtView.Rows(0)("SALESEXECUTIVECODE").ToString()


            Dim CLPaddress As String = ""
            Dim CLPBalancePoints As String
            Dim clpaddressnew As String = ""
            Dim customername As String = ""
            Dim customerPhoneno As String = ""
            Dim DeliveryName As String = ""
            Dim DeliveryDate As DateTime
            Dim ReminderComments As String = ""
            ''If dtView.Rows(0)("CLPNO").ToString() <> String.Empty Then
            ''    'CLpLine = "CLP Card No:" & dtView.Rows(0)("CLPNO").ToString()
            ''    'CustomerNameLine = "Customer Name: " & dtView.Rows(0)("CLPNAME").ToString()
            ''    'CLPPointsLine = "CLP Points Earned: " & FormatNumber(dtView.Rows(0)("CLPPoints").ToString())

            ''    CLpLine = getValueByKey("CLSCMP011") & dtView.Rows(0)("CLPNO").ToString()

            ''    CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CLPNAME").ToString()
            ''    customername = dtView.Rows(0)("CLPNAME").ToString().Trim
            ''    clpaddressnew = dtView.Rows(0)("CLPAddress").ToString()
            ''    clpaddressnew = clpaddressnew.Trim
            ''    CLPPointsLine = getValueByKey("CLSCMP013") & FormatNumber(Val(dtView.Rows(0)("CLPPoints").ToString()))
            ''    customerPhoneno = dtView.Rows(0)("MobileNo").ToString()
            ''    If dtView.Rows.Count() > 0 Then
            ''        '---- Changes By Mahesh : Customer Name is Not Mandatory for Home Delivery 
            ''        'If dtView.Rows(0)("HDName").ToString() <> String.Empty Then
            ''        If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
            ''            DeliveryDate = dtView.Rows(0)("HDDeliveryDate").ToString()
            ''            'strHomeDilery = strHomeDilery & getValueByKey("CLSCMP023") & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf
            ''            DeliveryName = DeliveryPersonName.Trim
            ''        End If
            ''    End If
            ''    If (dtView.Rows(0)("RedemptionPoints") IsNot DBNull.Value AndAlso dtView.Rows(0)("RedemptionPoints") > 0) Then
            ''        CLPPointsLine += vbCrLf + getValueByKey("CLSCMP034") & FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()))
            ''    End If

            ''    CLPaddress = dtView.Rows(0)("CLPAddress").ToString()
            ''    If CLPaddress <> String.Empty Then
            ''        CLPaddress = getValueByKey("CLSCMP014") & dtView.Rows(0)("CLPAddress").ToString()
            ''    End If
            ''    ReminderComments = dtView.Rows(0)("ReminderComments").ToString()
            ''    CLPBalancePoints = String.Empty
            ''    CLPBalancePoints = getValueByKey("CLSCMP015")
            ''    Dim dblCLPPoints As Double

            ''    dblCLPPoints = Val(dtView.Rows(0)("BalancePoints").ToString())
            ''    'For Each drRow As DataRow In dtView.Select("Tendertypecode = 'CLPPoint'")
            ''    '    dblCLPPoints -= drRow("RedemptionPoints")
            ''    'Next

            ''    Dim RedemptionPoints As DataRow = dtView.Select("Tendertypecode = 'CLPPoint'").FirstOrDefault()
            ''    If (RedemptionPoints IsNot Nothing AndAlso RedemptionPoints("RedemptionPoints") IsNot DBNull.Value) Then
            ''        dblCLPPoints -= Val(RedemptionPoints("RedemptionPoints").ToString())
            ''    End If

            ''    CLPBalancePoints = CLPBalancePoints & FormatNumber(dblCLPPoints.ToString())
            ''    If CLPaddress <> String.Empty Then
            ''        CLPaddress = SplitString(CLPaddress, LineLength).ToString()
            ''    End If
            ''    'If CLpLine <> Nothing AndAlso CLpLine.ToString().Length > 0 Then ' 3 lines commented for clp details
            ''    '    StrSubFooter = StrSubFooter & CLpLine & vbCrLf
            ''    'End If
            ''    CustmerPhoneNo = getValueByKey("RP187") & " " & dtView.Rows(0)("Mobileno").ToString()
            ''    StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
            ''    'StrSubFooter = StrSubFooter & CLPBalancePoints & vbCrLf ''' 2 lines commented for clp details
            ''    If CustomerSaleType = "Home Delivery" Then
            ''        StrSubFooter = StrSubFooter & CustmerPhoneNo & vbCrLf
            ''    End If

            ''    'StrSubFooter = StrSubFooter & CustmerPhoneNo & vbCrLf
            ''    'StrSubFooter = StrSubFooter & CLPPointsLine & vbCrLf


            ''ElseIf dtView.Columns.Contains("CustomerNo") AndAlso dtView.Rows(0)("CustomerNo").ToString() <> String.Empty Then

            ''    CLpLine = getValueByKey("CLSCMP038") & dtView.Rows(0)("CustomerNo").ToString()

            ''    CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CustomerName").ToString()
            ''    If CLpLine <> Nothing AndAlso CLpLine.ToString().Length > 0 Then
            ''        StrSubFooter = StrSubFooter & CLpLine & vbCrLf
            ''    End If
            ''    StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
            ''End If


            Dim TotalQty, TGrossAmt, TDiscAmt, TRateAfterDisc, TTaxtAmt As String
            Dim TNetAmt As String = 0.0
            Dim BillLineNO As String
            DtScannedMettlerBills = obj.GetMettlerBillDetailsDataForPrint(CashMemoNo, Sitecode)
            Dim ScaleNo As String
            Dim objItemSch As New clsIteamSearch()
            Dim dtScanedBillArticle As New DataTable
            Dim dtNewArticleScaledtls As New DataTable
            Dim dtTender As DataTable 'sagar
            dtNewArticleScaledtls.Columns.Add("Article", System.Type.GetType("System.String"))
            dtNewArticleScaledtls.Columns.Add("ScaleNo", System.Type.GetType("System.String"))
            For ScanBillIndex = 0 To DtScannedMettlerBills.Rows.Count - 1
                '------- GET Mettler Scanned Bill Details From Mettler DataBase 
                With DtScannedMettlerBills
                    ScaleNo = Val(.Rows(ScanBillIndex)("SCALE_NO"))
                    dtScanedBillArticle = objItemSch.GetScanedBillArticle(ScaleNo:=ScaleNo, BillNo:=.Rows(ScanBillIndex)("Bill_No"), BillIntDate:=.Rows(ScanBillIndex)("MettlerScaleBillDate"), MettlerConn:=MettlerConnString) '    ScaleBillIntDate = Val(.Rows(ScanBillIndex)("MettlerScaleBillDate"))
                    If dtScanedBillArticle IsNot Nothing Then

                        For Each drscale As DataRow In dtScanedBillArticle.Rows
                            Dim drnew As DataRow = dtNewArticleScaledtls.NewRow
                            drnew("Article") = drscale("LegacyArticleCode")
                            drnew("ScaleNo") = ScaleNo
                            dtNewArticleScaledtls.Rows.Add(drnew)
                        Next

                    End If
                End With

            Next
            DtUnique.Columns.Add("ScaleNo", System.Type.GetType("System.Int32"))
            For Each drscaleno As DataRow In dtNewArticleScaledtls.Rows
                Dim dr() As DataRow = DtUnique.Select("ArticleCode= '" & drscaleno("Article") & "'")
                If dr.Length > 0 Then
                    dr(0)("ScaleNo") = drscaleno("ScaleNo")
                End If
            Next
            If Not dtNewArticleScaledtls Is Nothing Then
                If dtNewArticleScaledtls.Rows.Count = 0 Then
                    For Each druniq As DataRow In DtUnique.Rows
                        druniq("ScaleNo") = 0
                    Next
                Else
                    For Each druniq As DataRow In DtUnique.Rows
                        If Convert.ToString(druniq("ScaleNo")) = "" Then
                            druniq("ScaleNo") = 0
                        End If
                    Next
                End If
            End If
            ''For Each dr As DataRow In DtUnique.Rows
            ''    ItemCode = dr("ArticleCode").ToString()

            ''    If (DisplayArticleFullName) Then
            ''        Desc = dr("ArticleFullName").ToString()
            ''    Else
            ''        Desc = dr("DISCRIPTION").ToString()
            ''    End If
            ''    BillLineNO = dr("BillLineNO").ToString()
            ''    If SpectrumCommon.ComboItemPrintingAllowed AndAlso Dtcombo IsNot Nothing AndAlso Dtcombo.Rows.Count > 0 Then
            ''        Dim drp() = Dtcombo.Select("ComboArticleCode='" & ItemCode & "' AND BillLineNO ='" & BillLineNO & "'")
            ''        If drp.Length > 0 Then
            ''            Dim articleDescriptionDictionary As New Dictionary(Of String, Integer)
            ''            For index = 0 To drp.Length - 1
            ''                If articleDescriptionDictionary.ContainsKey(drp(index)("ArticleName")) Then
            ''                    articleDescriptionDictionary(drp(index)("ArticleName")) = articleDescriptionDictionary(drp(index)("ArticleName")) + drp(index)("Quantity")
            ''                Else
            ''                    articleDescriptionDictionary.Add(drp(index)("ArticleName"), drp(index)("Quantity"))
            ''                End If
            ''            Next
            ''            Dim AddedDesc = GetMultilinedString(articleDescriptionDictionary)
            ''            Desc = Desc & vbCrLf & AddedDesc
            ''        End If
            ''    End If
            ''    FinScaleNo = dr("ScaleNo").ToString()

            ''    Qty = dr("Quantity").ToString()
            ''    Rate = dr("SellingPrice").ToString()
            ''    RateAfterDisc = (dr("SellingPrice") - dr("TOTALDISCOUNT")).ToString()
            ''    If CDbl(clsCommon.CheckIfBlank(Rate)) < 0 Then
            ''        Rate = CDbl(clsCommon.CheckIfBlank(Rate)) * -1
            ''    End If
            ''    DiscAmt = dr("TOTALDISCOUNT").ToString()
            ''    DiscPer = dr("TOTALDISCPERCENTAGE").ToString()
            ''    NetAmt = dr("NetAmount").ToString()
            ''    GrossAmt = dr("GROSSAMT").ToString()
            ''    TaxAmt = dr("TOTALTAXAMOUNT").ToString()


            ''    If (AllowDecimalQty) Then
            ''        If (WeightScaleEnabled) Then
            ''            Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 3)
            ''        Else
            ''            Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 2)
            ''        End If
            ''    Else
            ''        Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 0)
            ''    End If

            ''    'For i = Qty.Length To 4
            ''    '    Qty = " " & Qty
            ''    'Next
            ''    Rate = FormatNumber(CDbl(clsCommon.CheckIfBlank(Rate)), 2)

            ''    RateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(RateAfterDisc)), 2)
            ''    'Rate = FormatCurrency(Rate, 2)
            ''    'For i = Rate.Length To 8
            ''    '    Rate = " " & Rate
            ''    'Next
            ''    DiscAmt = FormatNumber(CDbl(IIf(DiscAmt <> String.Empty, DiscAmt, 0)), 2)
            ''    'For i = DiscAmt.Length To 8
            ''    '    DiscAmt = " " & DiscAmt
            ''    'Next
            ''    DiscPer = FormatNumber(CDbl(IIf(DiscPer <> String.Empty, DiscPer, 0)), 2)
            ''    DiscPer = DiscPer & "%"
            ''    'For i = DiscPer.Length To 8
            ''    '    DiscPer = " " & DiscPer
            ''    'Next
            ''    'Format of NetAmt with No Digits after Decimal
            ''    NetAmt = FormatNumber(CDbl(NetAmt), 0)
            ''    NetAmtTemp = FormatNumber(CDbl(NetAmt), 2)
            ''    ' NetAmt = FormatNumber(CDbl(NetAmt), 2)
            ''    GrossAmt = FormatNumber(CDbl(GrossAmt), 2)
            ''    'GrossAmt = FormatNumber(CDbl(GrossAmt), 0)
            ''    'For i = NetAmt.Length To 8
            ''    '    NetAmt = " " & NetAmt
            ''    'Next
            ''    TaxAmt = FormatNumber(CDbl(IIf(TaxAmt <> String.Empty, TaxAmt, 0)), 2)
            ''    'For i = TaxAmt.Length To 7
            ''    '    TaxAmt = " " & TaxAmt
            ''    'Next

            ''    Dim TempNetAmt As String = "0"

            ''    If Type = "CMWAmt" Then
            ''    Else
            ''        If 28 - Desc.Length < Qty.Length Then
            ''            Select Case PrintFormatNo
            ''                Case 1
            ''                Case 2

            ''                Case Else
            ''                    TempNetAmt = NetAmt
            ''                    If Val(NetAmt) > 0 AndAlso Val(TaxAmt) > 0 Then
            ''                        TempNetAmt = CDbl(GrossAmt) - CDbl(TaxAmt)

            ''                    End If
            ''            End Select
            ''        Else
            ''            Select Case PrintFormatNo
            ''                Case 1

            ''                Case 2

            ''                Case Else
            ''                    TempNetAmt = NetAmt
            ''                    If Val(NetAmt) > 0 AndAlso Val(TaxAmt) > 0 Then
            ''                        TempNetAmt = CDbl(GrossAmt) - CDbl(TaxAmt)
            ''                    End If
            ''            End Select
            ''        End If
            ''    End If
            ''    ' Added For Font Changes as Requested By Client-------------
            ''    ' strLineDetail.Append(StrLineItem)
            ''    strLineDetail.Append(StrLineItem)
            ''    '--------------------

            ''    If CDbl(Qty.Trim) > 0 Then
            ''        TotalQty = CDbl(clsCommon.CheckIfBlank(TotalQty)) + CDbl(Qty.Trim)
            ''    End If
            ''    TDiscAmt = CDbl(clsCommon.CheckIfBlank(TDiscAmt)) + CDbl(DiscAmt.Trim)
            ''    TGrossAmt = CDbl(clsCommon.CheckIfBlank(TGrossAmt)) + CDbl(dr("GROSSAMT").ToString())
            ''    'TNetAmt = CDbl(clsCommon.CheckIfBlank(TNetAmt)) + CDbl(NetAmt.Trim)
            ''    'TNetAmt = Format(Math.Round(Convert.ToDecimal(CDbl(clsCommon.CheckIfBlank(TNetAmt)) + CDbl(NetAmt.Trim))), "0.00")
            ''    TNetAmt = Decimal.Add(FormatNumber(CDbl(NetAmtTemp), 2).ToString(), TNetAmt)
            ''    TRateAfterDisc = CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)) + CDbl(RateAfterDisc.Trim)
            ''    TTaxtAmt = CDbl(clsCommon.CheckIfBlank(TTaxtAmt)) + CDbl(TaxAmt.Trim)
            ''Next

            TotalQty = FormatNumber(CDbl(clsCommon.CheckIfBlank(TotalQty)), 2)
            TDiscAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TDiscAmt)), 2)
            'TDiscAmt = FormatNumber(dtView.Rows(0)("Discount"), 2)
            TGrossAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TGrossAmt)), 2)
            'TNetAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TNetAmt)), 2)
            TNetAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TNetAmt)), RoundOff), 2)
            TRateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)), 2)
            TTaxtAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TTaxtAmt)), 2)
            Dim TSubTotalAmt As String
            Dim TotalDisPercent As String
            If Type <> "CMWAmt" Then
                If CDbl(clsCommon.CheckIfBlank(TDiscAmt)) > 0 Then
                    TotalDisPercent = Math.Round((CDbl(clsCommon.CheckIfBlank(TDiscAmt)) / CDbl(clsCommon.CheckIfBlank(TGrossAmt))) * 100, 1).ToString()
                    TSubTotalAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TGrossAmt)) - CDbl(clsCommon.CheckIfBlank(TDiscAmt)), RoundOff), 0)
                    TDiscAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TDiscAmt)), RoundOff), 0)
                    Dim ObjclsCommon As New clsCommon
                    Dim offerno As String = ObjclsCommon.GetOffernoForRoundOff()
                    ''If DtUnique.Rows(0)("PromotionId").ToString().Contains(offerno) And offerno <> "" Then
                    ''    ' TotalDiscAmtLine = "Round Off" & Space(BoldLineLength / 2 - "Round Off".Length) & " :" & TDiscAmt.PadLeft(BoldLineLength - ("Round Off".Length + Space(BoldLineLength / 2 - "Round Off".Length).Length + ":".Length))
                    ''Else
                    ''    '  Dim CheckIfPositiveNumber = IIf(BoldLineLength / 2 - (String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).Length) > 0, BoldLineLength / 2 - String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).Length, 0)
                    ''    '  TotalDiscAmtLine = String.Format(getValueByKey("CLSCMP019"), TotalDisPercent) & Space(CheckIfPositiveNumber) & " :" & TDiscAmt.PadLeft(BoldLineLength - (String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).Length + ": ".Length))
                    ''End If
                    '--------------------
                End If

                TGrossAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TGrossAmt)), RoundOff), 0)
                TNetAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TNetAmt)), RoundOff), 0)
                TRateAfterDisc = TRateAfterDisc & " " & Currency
                TTaxtAmt = TTaxtAmt & " " & Currency
                TotalGrossAmtLine = "Total Amt" & Space(5) & ":" & TGrossAmt.PadLeft(BoldLineLength - Val("Total Amt".Length + Space(5).Length))
                If (CDbl(TDiscAmt.Replace("INR", ""))) > 0 Then
                    NetAmtLine = getValueByKey("CLSCMP020") & Space(2) & ":" & TNetAmt.PadLeft(BoldLineLength - Val(getValueByKey("CLSCMP020").Length + Space(2).Length))
                End If
                '--------------------
                Dim taxDetailsForBill As DataTable
                'this code is also use for print format no 6 done by ashma
                If _TaxDetail = True Then
                    taxDetailsForBill = obj.GetTaxDetailsForBillNo(Sitecode, dtView.Rows(0)("Billno").ToString())
                    If taxDetailsForBill IsNot Nothing AndAlso taxDetailsForBill.Rows.Count > 0 Then
                        For Each row As DataRow In taxDetailsForBill.Rows
                            'If CompositeTaxReqOnPrint = False AndAlso IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                            If IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                            Dim taxValue As String = row("TaxVAlue").ToString()
                            taxValue = FormatNumber(CDbl(clsCommon.CheckIfBlank(taxValue)), 2)
                            taxValue = taxValue & " " & Currency
                            FooterTaxLine = FooterTaxLine & row("TaxName").PadRight(LineLength - 16) & ":" & taxValue.PadLeft(14) & vbCrLf
                        Next
                    End If
                End If
                TotalFooterTaxDetail = FooterTaxLine
                'If KOTAndBillGeneration = True Then
                '    dtTender = obj.GetTenderTypeByRefBillNo(dtView.Rows(0)("BILLNO").ToString())
                '    If dtTender.Rows.Count = 0 Then
                '        dtTender = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                '    End If
                'Else
                '    dtTender = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                'End If
                ' dtTender = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                'For Each drTender As DataRow In dtTender.Rows
                '    If UCase(obj.GetDefaultConfigValue(Sitecode, "CVInfoReqOnPrint")) = "FALSE" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)") Then
                '        Continue For
                '    End If
                '    Dim amounttendered As String = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(drTender("AMOUNTTENDERED").ToString)), RoundOff), 0)
                '    Dim tender As String = drTender("CurrencyCode").ToString() & " " & amounttendered & vbCrLf
                '    '----------------
                '    Dim tenderName As String
                '    'Added by Rohit
                '    If drTender("CardNO").ToString() <> "" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(I)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(R)") Then
                '        tenderName = drTender("TENDERHEADNAME").ToString() & "(" & drTender("CardNO").ToString() & ") "
                '    Else
                '        tenderName = drTender("TENDERHEADNAME").ToString()
                '    End If
                '    'End editing
                '    If Not drTender("AMOUNTINCURRENCY") Is DBNull.Value AndAlso drTender("AMOUNTINCURRENCY") <> 0 Then '0000404
                '        tender = drTender("CurrencyCode").ToString() & " " & FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(drTender("AMOUNTINCURRENCY").ToString())), RoundOff), 0) & vbCrLf
                '    End If
                '    If drTender("TENDERTYPECODE").ToString() = "CLPPoint" Then
                '        tender = FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()), 0)
                '    End If

                'Next

                If (_IsDisplayTotalSaving) Then
                    If (Not String.IsNullOrEmpty(TDiscAmt) AndAlso Not String.Equals(TDiscAmt, "0.00")) Then
                        Dim totalSavingAmount As String = String.Format(getValueByKey("CLSCMP033"), Currency, TDiscAmt.Replace(Currency, String.Empty).Trim)
                    Else
                    End If
                Else
                End If


                '--------------------
                'Company and Tin Info
                Dim NoOfReprints As String = IIf(IsDBNull(dtView.Rows(0)("NoofReprints")), String.Empty, dtView.Rows(0)("NoofReprints")).ToString()
                Dim CompanyName As String = dtView.Rows(0)("SITEOFFICIALNAME") 'obj.GetDefaultConfigValue(Sitecode, "CompanyName")
                'Dim dtPromo As DataTable = dvItemDetail.ToTable(True, "PROMOTIONALMSGID", "PROMOMESS")
                'For Each drTerms As DataRow In dtPromo.Select("", "PROMOTIONALMSGID", DataViewRowState.CurrentRows)
                '    If Not String.IsNullOrEmpty(drTerms("PROMOMESS").ToString()) Then
                '        PromoMsgLine = PromoMsgLine & drTerms("PROMOMESS").ToString() & vbCrLf
                '    End If
                'Next

                TotalPromotionalMsg = PromoMsgLine
                strPrintMsg = New StringBuilder()
                sbDeliveryBillPrint = New StringBuilder()

                Dim dtPrint As New DataTable
                Dim objPrint As New SpectrumBL.clsCommon
                dtPrint = objPrint.GetPrintingDetails(Type)
                'If Not dtPrint Is Nothing Then
                '    Dim filter As String = ""
                '    Dim dv As New DataView(dtPrint, filter, "Sequenceno", DataViewRowState.CurrentRows)
                '    filter = "TOPBOTTOM='Bottom'"
                '    dv.RowFilter = filter
                '    For Each drview As DataRowView In dv
                '        If drview("ReceiptText").ToString().Length >= 25 Then
                '        Else
                '        End If
                '    Next

                'End If
                'added by khusrao adil on 12-07-2017 all print format change BillNo ,tax invocie
                Dim TopBottomDisplayValue As String = "FINAL RECEIPT"
                If Not dtPrint Is Nothing Then
                    Dim filter As String = ""
                    Dim dv As New DataView(dtPrint, filter, "Sequenceno", DataViewRowState.CurrentRows)
                    filter = "TOPBOTTOM='Top'"
                    dv.RowFilter = filter
                    For Each drview As DataRowView In dv
                        'If drview("ReceiptText").ToString().Length >= 25 Then
                        '    Dim a = drview("ReceiptText").ToString()
                        'Else
                        'End If
                        If drview("ReceiptText").ToString().Length >= 4 Then
                            TopBottomDisplayValue = drview("ReceiptText").ToString()
                        Else
                        End If
                    Next
                End If
                'added by khusrao to print resturn amount on bill
                If (dtView.Rows.Count() > 0 And String.IsNullOrEmpty(DeliveryPersonName)) Then
                    If Val(PaidAmt) > 0 Then 'PaidAmt
                        Dim retAmt As Double
                        If CDbl(PaidAmt) >= CDbl(BillAmt) Then
                            retAmt = CDbl(PaidAmt) - CDbl(BillAmt)
                        End If
                        If retAmt <> 0 Then
                            Dim returnAmtFooter = vbCrLf & getValueByKey("CLIST04") & " " & FormatNumber(CDbl(PaidAmt), 2) & vbCrLf 'PaidAmt
                            returnAmtFooter = returnAmtFooter & getValueByKey("CLIST05") & " " & FormatNumber(CDbl(retAmt), 2) & vbCrLf 'retAmt
                            Dim dr = dtPrint.NewRow
                            If dtPrint.Rows.Count <> 0 Then
                                dr("TOPBOTTOM") = dtPrint.Rows(dtPrint.Rows.Count - 1)("TOPBOTTOM")
                                dr("SEQUENCENO") = dtPrint.Rows(dtPrint.Rows.Count - 1)("SEQUENCENO") + 1
                                dr("RECEIPTTEXT") = returnAmtFooter
                                dr("ALIGN") = dtPrint.Rows(dtPrint.Rows.Count - 1)("ALIGN")
                                dr("WIDTH") = dtPrint.Rows(dtPrint.Rows.Count - 1)("WIDTH")
                                dr("HEIGHT") = dtPrint.Rows(dtPrint.Rows.Count - 1)("HEIGHT")
                                dr("BOLD") = dtPrint.Rows(dtPrint.Rows.Count - 1)("BOLD")
                            Else
                                dr("TOPBOTTOM") = "Bottom"
                                dr("SEQUENCENO") = dtPrint.Rows.Count + 1
                                dr("RECEIPTTEXT") = returnAmtFooter
                                dr("ALIGN") = "Left"
                                'dr("WIDTH") = dtPrint.Rows(0)("WIDTH")
                                'dr("HEIGHT") = dtPrint.Rows(0)("HEIGHT")
                                'dr("BOLD") = dtPrint.Rows(0)("BOLD")
                            End If
                            dtPrint.Rows.Add(dr)
                        End If

                    End If

                End If
                'added by khusrao adil on 04-07-2017 for GST No on bill print 
                Dim GstNo = obj.GetTinNumberForASite(Sitecode)
                Dim GstNoDisplay As String = ""
                If (Not String.IsNullOrEmpty(GstNo)) AndAlso GstNo <> "0" Then
                    Dim strtna = "GST No.: " & GstNo
                    strtna = SplitString(strtna, LineLength).ToString() & vbCrLf
                    If Not String.IsNullOrEmpty(strtna) Then
                        GstNoDisplay += strtna.Trim()
                    End If
                End If
                'added by sagar 
                ''If DtUnique.Rows.Count > 0 Then
                ''    For index = 0 To DtUnique.Rows.Count - 1
                ''        ' DtUnique(index)("GROSSAMT") = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(DtUnique(index)("GROSSAMT"))), RoundOff), 0)
                ''        If PrintFormatNo = 7 Then

                ''        Else
                ''            DtUnique(index)("GROSSAMT") = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(DtUnique(index)("GROSSAMT"))), RoundOff), 0)
                ''        End If
                ''        'added by khusrao adil on 04-07-2017 for GST No on bill print 
                ''        If Not String.IsNullOrEmpty(GstNoDisplay) Then
                ''            DtUnique(index)("GSTNo") = GstNoDisplay
                ''        End If

                ''    Next

                ''    DtUnique.AcceptChanges()
                ''End If

                '-------------
                Dim DsHeader As New DataTable
                DsHeader.Columns.Add("ClientName", System.Type.GetType("System.String"))
                '  DsHeader.Columns.Add("SRNo", System.Type.GetType("System.String"))
                DsHeader.Columns.Add("SiteCode", System.Type.GetType("System.String"))
                DsHeader.Columns.Add("CompanyAddress", System.Type.GetType("System.String"))
                DsHeader.Columns.Add("INVOICENUMBER", System.Type.GetType("System.String"))
                DsHeader.Columns.Add("GSTN", System.Type.GetType("System.String"))
                DsHeader.Columns.Add("INVOICEDATE", System.Type.GetType("System.String"))
                DsHeader.Columns.Add("DUEDATE", System.Type.GetType("System.String"))
                DsHeader.Columns.Add("CustomerNo", System.Type.GetType("System.String"))
                DsHeader.Columns.Add("FirstName", System.Type.GetType("System.String"))
                DsHeader.Columns.Add("middlename", System.Type.GetType("System.String"))
                DsHeader.Columns.Add("SurName", System.Type.GetType("System.String"))
                DsHeader.Columns.Add("GSTCUSTOMER", System.Type.GetType("System.String"))
                DsHeader.Columns.Add("CustomerAddress", System.Type.GetType("System.String"))
                DsHeader.Columns.Add("GrossAmount", System.Type.GetType("System.Int64"))
                DsHeader.Columns.Add("TaxAmount", System.Type.GetType("System.Double"))
                DsHeader.Columns.Add("NetAmount", System.Type.GetType("System.Int64"))
                DsHeader.Columns.Add("StorePANCard", System.Type.GetType("System.String"))
                DsHeader.Columns.Add("StringValue", System.Type.GetType("System.String"))
                DsHeader.Columns.Add("AmountInWords", System.Type.GetType("System.String"))
                DsHeader.Columns.Add("StoreBankName", System.Type.GetType("System.String"))
                DsHeader.Columns.Add("StoreBankAccountNo", System.Type.GetType("System.String"))
                DsHeader.Columns.Add("StoreBranchName", System.Type.GetType("System.String"))


                Dim DsUnique As New DataTable
                DsUnique.Columns.Add("SITECODE", System.Type.GetType("System.String"))
                ' DsUnique.Columns.Add("SRNO", System.Type.GetType("System.String"))
                DsUnique.Columns.Add("BILLNO", System.Type.GetType("System.String"))
                DsUnique.Columns.Add("BILLDATE", System.Type.GetType("System.String"))
                DsUnique.Columns.Add("QTY", System.Type.GetType("System.String"))
                DsUnique.Columns.Add("ARTICLECODE", System.Type.GetType("System.String"))
                DsUnique.Columns.Add("ARTICLENAME", System.Type.GetType("System.String"))
                DsUnique.Columns.Add("BASICPRICE", System.Type.GetType("System.String"))
                DsUnique.Columns.Add("SELLINGPRICE", System.Type.GetType("System.String"))
                DsUnique.Columns.Add("TOTALDISCOUNT", System.Type.GetType("System.String"))
                DsUnique.Columns.Add("HSN", System.Type.GetType("System.String"))
                DsUnique.Columns.Add("CGSTTaxValue", System.Type.GetType("System.String"))
                DsUnique.Columns.Add("CGSTTaxAmount", System.Type.GetType("System.String"))
                DsUnique.Columns.Add("SGSTTaxValue", System.Type.GetType("System.String"))
                DsUnique.Columns.Add("SGSTTaxAmount", System.Type.GetType("System.String"))
                DsUnique.Columns.Add("IGSTTaxValue", System.Type.GetType("System.String"))
                DsUnique.Columns.Add("IGSTTaxAmount", System.Type.GetType("System.String"))


                '-------------------



                ''Dim dtHeaderDetails As New DataTable
                ''dtHeaderDetails.Columns.Add("Header", System.Type.GetType("System.String"))
                ''dtHeaderDetails.Columns.Add("Footer", System.Type.GetType("System.String"))
                ''dtHeaderDetails.Columns.Add("ClientName", System.Type.GetType("System.String"))
                ''dtHeaderDetails.Columns.Add("INR", System.Type.GetType("System.Single"))
                ''dtHeaderDetails.Columns.Add("StoreName", System.Type.GetType("System.String"))
                ''dtHeaderDetails.Columns.Add("Address", System.Type.GetType("System.String"))
                ''dtHeaderDetails.Columns.Add("PhoneNo", System.Type.GetType("System.String"))
                ''dtHeaderDetails.Columns.Add("FINALRECEIPT", System.Type.GetType("System.String"))
                ''dtHeaderDetails.Columns.Add("TotalAmt", System.Type.GetType("System.Single"))
                ''dtHeaderDetails.Columns.Add("LessDisPer", System.Type.GetType("System.Single"))
                ''dtHeaderDetails.Columns.Add("LessDisAmt", System.Type.GetType("System.Single"))
                ''dtHeaderDetails.Columns.Add("SubTotal", System.Type.GetType("System.Single"))
                ''dtHeaderDetails.Columns.Add("TotalToPay", System.Type.GetType("System.Single"))
                ''dtHeaderDetails.Columns.Add("CashierName", System.Type.GetType("System.String"))
                ''dtHeaderDetails.Columns.Add("CustomerName", System.Type.GetType("System.String"))
                ''dtHeaderDetails.Columns.Add("CustomerAddress", System.Type.GetType("System.String"))
                ''dtHeaderDetails.Columns.Add("CustomerPhoneNo", System.Type.GetType("System.String"))
                ''dtHeaderDetails.Columns.Add("DeliveryDate", System.Type.GetType("System.String"))
                ''dtHeaderDetails.Columns.Add("DeliveryName", System.Type.GetType("System.String"))
                ''dtHeaderDetails.Columns.Add("SalesType", System.Type.GetType("System.String"))
                ''dtHeaderDetails.Columns.Add("TokenNo", System.Type.GetType("System.String"))
                ''dtHeaderDetails.Columns.Add("DeliveryNote", System.Type.GetType("System.String"))
                ''dtHeaderDetails.Columns.Add("isDeliveryCopy", System.Type.GetType("System.Boolean"))
                ' ''added by khusrao adil for balance point display
                ' ''dtHeaderDetails.Columns.Add("IsPOSPasskey", System.Type.GetType("System.Boolean"))
                ''dtHeaderDetails.Columns.Add("CustomerRemark", System.Type.GetType("System.String"))

                ''If PrintFormatNo = 6 OrElse PrintFormatNo = 7 Then
                ''    dtHeaderDetails.Columns.Add("IsRateVisible", System.Type.GetType("System.Boolean"))
                ''    dtHeaderDetails.Columns.Add("IsPOSPasskey", System.Type.GetType("System.Boolean"))
                ''End If
                ''If StrDuplicate <> "" Then
                ''    dtHeaderDetails.Columns.Add("DuplicateBill", System.Type.GetType("System.String"))
                ''End If
                Dim drnewS1 = DsHeader.NewRow


                '   For Each i=0 to DsHeader.rows

                drnewS1("ClientName") = Dtheader.Rows(0)("ClientName")
                '  drnewS1("SRNo") = ""
                drnewS1("SiteCode") = Dtheader.Rows(0)("SiteCode")
                drnewS1("CompanyAddress") = Dtheader.Rows(0)("CompanyAddress")
                drnewS1("INVOICENUMBER") = Dtheader.Rows(0)("INVOICENUMBER")
                drnewS1("GSTN") = Dtheader.Rows(0)("GSTN")
                drnewS1("INVOICEDATE") = Dtheader.Rows(0)("INVOICEDATE")
                drnewS1("DUEDATE") = Dtheader.Rows(0)("DUEDATE")
                drnewS1("CustomerNo") = Dtheader.Rows(0)("CustomerNo")
                drnewS1("FirstName") = Dtheader.Rows(0)("FirstName")
                drnewS1("middlename") = Dtheader.Rows(0)("middlename")
                drnewS1("SurName") = Dtheader.Rows(0)("SurName")
                drnewS1("GSTCUSTOMER") = Dtheader.Rows(0)("GSTCUSTOMER")
                drnewS1("CustomerAddress") = Dtheader.Rows(0)("CustomerAddress")
                drnewS1("GrossAmount") = Dtheader.Rows(0)("GrossAmount")
                drnewS1("TaxAmount") = Dtheader.Rows(0)("TaxAmount")
                drnewS1("NetAmount") = Dtheader.Rows(0)("NetAmount")
                drnewS1("StorePANCard") = Dtheader.Rows(0)("StorePANCard")
                drnewS1("StringValue") = Dtheader.Rows(0)("StringValue")
                drnewS1("AmountInWords") = Dtheader.Rows(0)("AmountInWords")
                drnewS1("StoreBankName") = Dtheader.Rows(0)("StoreBankName")
                drnewS1("StoreBankAccountNo") = Dtheader.Rows(0)("StoreBankAccountNo")
                drnewS1("StoreBranchName") = Dtheader.Rows(0)("StoreBranchName")
                DsHeader.Rows.Add(drnewS1)
                '  Dim DtUnique As DataTable = ds.Tables("dsUnique")
                '

                For Each dr1 As DataRow In DtUnique.Rows

                    Dim drnewS2 = DsUnique.NewRow
                    drnewS2("SITECODE") = DtUnique.Rows(0)("SITECODE")
                    '  drnewS2("SRNO") = DtUnique.Rows(0)("SRNO")
                    drnewS2("BILLNO") = DtUnique.Rows(0)("BILLNO")
                    drnewS2("BILLDATE") = DtUnique.Rows(0)("BILLDATE")
                    drnewS2("QTY") = DtUnique.Rows(0)("QUANTITY")
                    drnewS2("ARTICLECODE") = DtUnique.Rows(0)("ARTICLECODE")
                    drnewS2("ARTICLENAME") = DtUnique.Rows(0)("DISCRIPTION")
                    drnewS2("BASICPRICE") = DtUnique.Rows(0)("GROSSAMT")
                    drnewS2("SELLINGPRICE") = DtUnique.Rows(0)("SELLINGPRICE")
                    drnewS2("TOTALDISCOUNT") = DtUnique.Rows(0)("TOTALDISCOUNT")
                    drnewS2("HSN") = DtUnique.Rows(0)("HSN")
                    drnewS2("CGSTTaxValue") = DtUnique.Rows(0)("CGSTTaxValue")
                    drnewS2("CGSTTaxAmount") = DtUnique.Rows(0)("CGSTTaxAmount")
                    drnewS2("SGSTTaxValue") = DtUnique.Rows(0)("SGSTTaxValue")
                    drnewS2("SGSTTaxAmount") = DtUnique.Rows(0)("SGSTTaxAmount")
                    drnewS2("IGSTTaxValue") = DtUnique.Rows(0)("IGSTTaxValue")
                    drnewS2("IGSTTaxAmount") = DtUnique.Rows(0)("IGSTTaxAmount")
                    DsUnique.Rows.Add(drnewS2)


                Next
                'Next


                '  Dim DtUnique As DataTable = ds.Tables("dsUnique")
                '








                '-----------------------------------------
                ''Dim drnewS = DsHeader.NewRow
                ''drnewS("Header") = "Submit Counter Copy at Scale"
                ''drnewS("Footer") = "BILL PAID"
                ''drnewS("ClientName") = ClientName.Trim

                ''drnewS("INR") = 0
                ''drnewS("StoreName") = SiteName.Trim
                ''drnewS("Address") = "" ' addressLine1.Trim & Space(1) & addressLine2.Trim & Space(1) & addressLine3.Trim
                ''drnewS("PhoneNo") = "" 'PhoneLine.Trim
                ''drnewS("FINALRECEIPT") = TopBottomDisplayValue
                ''drnewS("TotalAmt") = TGrossAmt.Trim
                ''If TotalDisPercent Is Nothing Then
                ''    drnewS("LessDisPer") = 0
                ''Else
                ''    drnewS("LessDisPer") = TotalDisPercent
                ''End If

                ''drnewS("LessDisAmt") = TDiscAmt
                ''If TSubTotalAmt Is Nothing Then
                ''    drnewS("SubTotal") = 0
                ''Else
                ''    drnewS("SubTotal") = TSubTotalAmt
                ''End If

                ''drnewS("TotalToPay") = TNetAmt
                ''drnewS("CashierName") = LoginUserName
                ''drnewS("CustomerName") = customername
                ' ''added by khusrao adil on 10-07-2017 
                'code commented by irfan on 24-08-2017

                ''If CustomerSaleType = "Home Delivery" Then
                ''    CustmerPhoneNo = getValueByKey("RP187") & " " & dtView.Rows(0)("Mobileno").ToString()
                ''Else
                ''    CustmerPhoneNo = getValueByKey("RP187") & " " & customerPhoneno
                ''    Dim tempMobileno = dtView.Rows(0)("Mobileno").ToString()
                ''    If dtView.Rows(0)("Mobileno").ToString().Length > 4 Then
                ''        customerPhoneno = dtView.Rows(0)("Mobileno").ToString()

                ''        Dim mobileLast4Digit = tempMobileno.Substring(customerPhoneno.Length - 4, 4)
                ''        Dim mobilemolength = customerPhoneno.Length
                ''        customerPhoneno = mobileLast4Digit.PadLeft(customerPhoneno.Length, "X")
                ''    End If
                ''    CustmerPhoneNo = getValueByKey("RP187") & " " & tempMobileno

                ''End If
                ''drnewS("CustomerAddress") = clpaddressnew
                ''drnewS("CustomerPhoneNo") = customerPhoneno
                ''drnewS("DeliveryDate") = DeliveryDate
                ''drnewS("DeliveryName") = DeliveryName
                ''drnewS("SalesType") = CustomerSaleType
                ''drnewS("TokenNo") = StrTokenNo
                ''drnewS("DeliveryNote") = "DELIVERY COPY"
                ''drnewS("isDeliveryCopy") = False

                ''drnewS("CustomerRemark") = ReminderComments

                'code added by vipul add new rate column in print format 6




                'If StrDuplicate <> "" Then
                '    drnewS("DuplicateBill") = StrDuplicate
                'End If
                'Dtheader.Rows.Add(drnewS)
                If EvasPizzaChanges Then
                    BarCodestring = ImageToBase64(CashMemoNo)
                End If
                If _PrintPreview = True Then
                    If KOTAndBillGeneration = False Then
                        If IsCounterCopyKot = False AndAlso KOTBillPrintingRequired Then
                            Call CashMemoKOTPrintBasedOnPrintFormatNoSSRS(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, dayopenDate, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, ClientName:=ClientName, DayCloseReportPath:=DayCloseReportPath, CustomerComments:=ReminderComments, EvassPizza:=EvasPizzaChanges)
                        End If
                    End If
                    If IsFinalReceipt = True Then
                        If PrintFormatNo = 5 OrElse PrintFormatNo = 6 OrElse PrintFormatNo = 7 Then
                            If IsCounterCopyKot = False AndAlso PrintFormatNo = 4 Then
                                If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                                    GenerateCashMemoFinalReceiptPrint(dtView, DtUnique, DsHeader, dtPrint, dtTender, DayCloseReportPath)
                                End If
                            End If
                        End If
                        '   GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo)
                        'new parameter added for print format 6 dttaxDetail:=taxDetailsForBill
                        If PrintFormatNo = 6 OrElse PrintFormatNo = 7 Then
                            Dim dtPrintCopy As DataTable
                            If dtPrint.Rows.Count > 0 Then
                                If dtPrint.Select("TOPBottom='Bottom'").Count > 0 Then
                                    dtPrintCopy = dtPrint.Select("TOPBottom='Bottom'").CopyToDataTable()
                                Else
                                    dtPrintCopy = dtPrint.Copy()
                                End If
                            Else
                                dtPrintCopy = dtPrint.Copy()
                            End If
                            GenerateFinalReceiptPrint(dtView, DtUnique, DsHeader, dtPrintCopy, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo, dttaxDetail:=taxDetailsForBill)
                            If Not (String.IsNullOrEmpty(DeliveryPersonName)) Then
                                Threading.Thread.Sleep(1000)
                                DsHeader.Rows(0)("isDeliveryCopy") = True
                                DsHeader.AcceptChanges()
                                GenerateFinalReceiptPrint(dtView, DtUnique, DsHeader, dtPrintCopy, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo, dttaxDetail:=taxDetailsForBill)
                            End If
                        ElseIf PrintFormatNo = 8 Then ''modified by khusrao adil for print format 8
                            GenerateFinalReceiptPrint(dtView, DtUnique, DsHeader, dtPrint, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo, dttaxDetail:=taxDetailsForBill)
                        Else
                            GenerateFinalReceiptPrint(dtView, DtUnique, DsHeader, dtPrint, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo, dttaxDetail:=Nothing)
                        End If

                    End If
                Else
                    If KOTAndBillGeneration = False Then
                        If IsCounterCopyKot = False AndAlso KOTBillPrintingRequired Then
                            Call CashMemoKOTPrintBasedOnPrintFormatNoSSRS(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, dayopenDate, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, ClientName:=ClientName, DayCloseReportPath:=DayCloseReportPath, CustomerComments:=ReminderComments, EvassPizza:=EvasPizzaChanges)
                        End If
                    End If
                    If IsFinalReceipt Then
                        If PrintFormatNo = 5 Or PrintFormatNo = 6 Or PrintFormatNo = 7 Then
                            If IsCounterCopyKot = False AndAlso PrintFormatNo = 4 Then
                                If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                                    GenerateCashMemoFinalReceiptPrint(dtView, DtUnique, DsHeader, dtPrint, dtTender, DayCloseReportPath)
                                End If
                            End If

                        End If
                        ' GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo)
                        'new parameter added for print format 6 dttaxDetail:=taxDetailsForBill
                        ' GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, dtPrint, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo, dttaxDetail:=taxDetailsForBill)
                        If PrintFormatNo = 6 Or PrintFormatNo = 7 Then
                            Dim dtPrintCopy As DataTable
                            If dtPrint.Rows.Count > 0 Then
                                If dtPrint.Select("TOPBottom='Bottom'").Count > 0 Then
                                    dtPrintCopy = dtPrint.Select("TOPBottom='Bottom'").CopyToDataTable()
                                Else
                                    dtPrintCopy = dtPrint.Copy()
                                End If
                            Else
                                dtPrintCopy = dtPrint.Copy()
                            End If
                            GenerateFinalReceiptPrint(dtView, DtUnique, DsHeader, dtPrintCopy, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo, dttaxDetail:=taxDetailsForBill)
                            If Not (String.IsNullOrEmpty(DeliveryPersonName)) Then
                                DsHeader.Rows(0)("isDeliveryCopy") = True
                                DsHeader.AcceptChanges()
                                GenerateFinalReceiptPrint(dtView, DtUnique, DsHeader, dtPrintCopy, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo, dttaxDetail:=taxDetailsForBill)
                            End If
                        ElseIf PrintFormatNo = 8 Then ''modified by khusrao adil for print format 8
                            GenerateFinalReceiptPrint(dtView, DtUnique, DsHeader, dtPrint, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo, dttaxDetail:=taxDetailsForBill)
                        Else
                            GenerateFinalReceiptPrint(dtView, DtUnique, DsHeader, dtPrint, dtTender, DayCloseReportPath, EvassPizzaChanges:=EvasPizzaChanges, PrintFormat:=PrintFormatNo, dttaxDetail:=Nothing)
                        End If
                    End If
                End If
            Else
                errorMsg = getValueByKey("CLSCMP031")
            End If
        Catch ex As Exception
            errorMsg = ex.Message
        End Try
    End Sub
    '----------------------------------------------------------
    'added on 16 may ashma

    Public Sub CashMemoPrintFormat_SSRS_Innoviti(ByVal dayopenDate As Date, ByVal LangCode As String, ByVal Currency As String, ByVal Sitecode As String, ByVal Type As String, ByVal DuplicatePrinting As String, ByVal DeletedUserid As String, ByVal AuthorisedUser As String, ByRef errorMsg As String, Optional ByVal GiftMsg As String = "", Optional ByVal IsSavoy As Boolean = False, Optional ByVal MettlerConn As String = "", Optional ByVal IsArticleWiseKot As Boolean = False, Optional ByVal IsCounterCopy As Boolean = False, Optional ByVal IsFinalReceipt As Boolean = False, Optional ByVal ClientName As String = "", Optional ByVal DayCloseReportPath As String = "", Optional ByVal IsCounterCopyKot As Boolean = False, Optional ByVal KOTAndBillGeneration As Boolean = False)
        Try
            Dim strPrintMsg, sbDeliveryBillPrint As StringBuilder
            Dim strLineDetail As New StringBuilder
            Dim StrHeader, StrSubHeader, StrBody, StrCompanyInfo, StrTagLine, StrSubFooter, StrFooter, StrDuplicate, StrDeleteLine, StrCashMemoLine, StrCashierLine, StrSalesPersonLine As String
            Dim StrTokenNo, StrCustomerName As String
            Dim StrCMNo, StrCMDate, StrCMTime, StrCashier, StrSalesPerson As String
            Dim SiteLine, StrSiteTelline, StrAdrressLine, StrSiteReg1Line, StrSiteReg2Line As String
            Dim CLpLine, LineItemHeading, StrLineItem As String
            Dim CustomerNameLine, CLPPointsLine, CustmerPhoneNo As String
            Dim ItemCode, Desc, Qty, Rate, DiscAmt, DiscPer, TaxAmt, NetAmt, NetAmtTemp, RateAfterDisc, GrossAmt, FinScaleNo As String
            Dim TotalQtyLine, TakeAwayQuantity, TotalGrossAmtLine, TotalDiscAmtLine, SubTotalLine, NetAmtLine, TotalTaxAmtLine As String
            Dim TotalFooterTaxDetail, FooterTaxLine As String
            Dim TenderDetails, TenderLine As String
            Dim TotalPromotionalMsg, PromoMsgLine, StrPrintHeaderLine, StrPrintFooterLine As String
            Dim TermsNcond As String
            Dim strLine As String
            Dim strDblLine As String
            Dim strHomeDilery As String
            Dim isTakeAwayQtyPrint As Boolean = False
            errorMsg = ""
            Dim dtView As New DataTable
            Dim BoldLineLength As Int32 = 27
            Dim obj As New SpectrumBL.clsCashMemo()
            dtView = obj.GetCashMemo(CashMemoNo, Sitecode, LangCode)

            Dim dvItemDetail As New DataView(dtView, "", "", DataViewRowState.CurrentRows)
            'Dim DtUnique As DataTable = dvItemDetail.ToTable(True, "ArticleCode", "DISCRIPTION", "QUANTITY", "SELLINGPRICE", "GROSSAMT", "TOTALDISCOUNT", "TOTALDISCPERCENTAGE", "NETAMOUNT", "TOTALTAXAMOUNT")
            Dim DtUnique As DataTable = obj.GetBillDetailsDataForPrint(CashMemoNo, Sitecode, LangCode, True)
            Dim Dtcombo As DataTable = obj.GetComboDetailsDataForPrint(CashMemoNo, Sitecode, LangCode)
            If Not IsDBNull(dtView.Rows(0)("ServiceTaxAmount")) AndAlso Val(dtView.Rows(0)("ServiceTaxAmount")) > 0 Then
                CustomerSaleType = GetCustomerSaleType(dtView.Rows(0)("ServiceTaxAmount"))
            Else
                CustomerSaleType = ""
            End If
            If Not IsDBNull(dtView.Rows(0)("DeliveryPersonID")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("DeliveryPersonID")) Then
                DeliveryPersonName = dtView.Rows(0)("DeliveryPersonID").ToString()
            End If
            If DtUnique Is Nothing Then Exit Sub
            If dtView Is Nothing Then
                Exit Sub
            Else
                'ashma
                strLine = "-".PadRight(LineLength, "-") & vbCrLf
                '----------
            End If

            Dim SiteName As String
            SiteName = dtView.Rows(0)("SITEOFFICIALNAME").ToString()
            If ClientName <> "" Then
                Dim arrClientname = ClientName.Split(" ").ToArray()

                For i = 0 To arrClientname.Length - 1
                    If SiteName.ToUpper.Contains(arrClientname(i).ToString.ToUpper) Then
                        SiteName = SiteName.ToUpper.Replace(arrClientname(i).ToString.ToUpper.Trim, "").Trim
                    End If
                Next
            End If
            SiteName = SiteName.PadLeft((Val(LineLength - SiteName.Length) + SiteName.Length) / 2)
            '------------------
            SiteLine = SiteName.Trim
            Dim PhoneLine As String = dtView.Rows(0)("TELNO").ToString()
            Dim addressLine1 As String = dtView.Rows(0)("ADDRESSLINE1").ToString().Trim
            Dim addressLine2 As String = dtView.Rows(0)("ADDRESSLINE2").ToString().Trim
            Dim addressLine3 As String = dtView.Rows(0)("ADDRESSPINCODE").ToString().Trim
            If Type = "DCM" Then
                StrCMNo = getValueByKey("CLSCMP008") & dtView.Rows(0)("Billno").ToString()
            Else
                StrCMNo = getValueByKey("CLSCMP039") & ":" & dtView.Rows(0)("Billno").ToString()
                '---------------
            End If
            StrCMDate = dayopenDate.ToString("dd-MM-yyyy")
            strDayOpenDate = dayopenDate.ToShortDateString()
            If DuplicatePrinting <> String.Empty Then
                StrCMDate = Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())
            End If
            StrSalesPerson = getValueByKey("CLSCMP010") & dtView.Rows(0)("SALESEXECUTIVECODE").ToString()


            Dim CLPaddress As String = ""
            Dim CLPBalancePoints As String
            Dim clpaddressnew As String = ""
            Dim customername As String = ""
            Dim customerPhoneno As String = ""
            Dim DeliveryName As String = ""
            Dim DeliveryDate As DateTime

            If dtView.Rows(0)("CLPNO").ToString() <> String.Empty Then
                'CLpLine = "CLP Card No:" & dtView.Rows(0)("CLPNO").ToString()
                'CustomerNameLine = "Customer Name: " & dtView.Rows(0)("CLPNAME").ToString()
                'CLPPointsLine = "CLP Points Earned: " & FormatNumber(dtView.Rows(0)("CLPPoints").ToString())

                CLpLine = getValueByKey("CLSCMP011") & dtView.Rows(0)("CLPNO").ToString()

                CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CLPNAME").ToString()
                customername = dtView.Rows(0)("CLPNAME").ToString().Trim
                clpaddressnew = dtView.Rows(0)("CLPAddress").ToString()
                clpaddressnew = clpaddressnew.Trim
                CLPPointsLine = getValueByKey("CLSCMP013") & FormatNumber(Val(dtView.Rows(0)("CLPPoints").ToString()))
                customerPhoneno = dtView.Rows(0)("MobileNo").ToString()
                If dtView.Rows.Count() > 0 Then
                    '---- Changes By Mahesh : Customer Name is Not Mandatory for Home Delivery 
                    'If dtView.Rows(0)("HDName").ToString() <> String.Empty Then
                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        DeliveryDate = dtView.Rows(0)("HDDeliveryDate").ToString()
                        'strHomeDilery = strHomeDilery & getValueByKey("CLSCMP023") & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf
                        DeliveryName = DeliveryPersonName.Trim
                    End If
                End If
                If (dtView.Rows(0)("RedemptionPoints") IsNot DBNull.Value AndAlso dtView.Rows(0)("RedemptionPoints") > 0) Then
                    CLPPointsLine += vbCrLf + getValueByKey("CLSCMP034") & FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()))
                End If

                CLPaddress = dtView.Rows(0)("CLPAddress").ToString()
                If CLPaddress <> String.Empty Then
                    CLPaddress = getValueByKey("CLSCMP014") & dtView.Rows(0)("CLPAddress").ToString()
                End If

                CLPBalancePoints = String.Empty
                CLPBalancePoints = getValueByKey("CLSCMP015")
                Dim dblCLPPoints As Double

                dblCLPPoints = Val(dtView.Rows(0)("BalancePoints").ToString())
                'For Each drRow As DataRow In dtView.Select("Tendertypecode = 'CLPPoint'")
                '    dblCLPPoints -= drRow("RedemptionPoints")
                'Next

                Dim RedemptionPoints As DataRow = dtView.Select("Tendertypecode = 'CLPPoint'").FirstOrDefault()
                If (RedemptionPoints IsNot Nothing AndAlso RedemptionPoints("RedemptionPoints") IsNot DBNull.Value) Then
                    dblCLPPoints -= Val(RedemptionPoints("RedemptionPoints").ToString())
                End If

                CLPBalancePoints = CLPBalancePoints & FormatNumber(dblCLPPoints.ToString())
                If CLPaddress <> String.Empty Then
                    CLPaddress = SplitString(CLPaddress, LineLength).ToString()
                End If
                'If CLpLine <> Nothing AndAlso CLpLine.ToString().Length > 0 Then ' 3 lines commented for clp details
                '    StrSubFooter = StrSubFooter & CLpLine & vbCrLf
                'End If
                CustmerPhoneNo = getValueByKey("RP187") & " " & dtView.Rows(0)("Mobileno").ToString()
                StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
                'StrSubFooter = StrSubFooter & CLPBalancePoints & vbCrLf ''' 2 lines commented for clp details
                If CustomerSaleType = "Home Delivery" Then
                    StrSubFooter = StrSubFooter & CustmerPhoneNo & vbCrLf
                End If

                'StrSubFooter = StrSubFooter & CustmerPhoneNo & vbCrLf
                'StrSubFooter = StrSubFooter & CLPPointsLine & vbCrLf


            ElseIf dtView.Columns.Contains("CustomerNo") AndAlso dtView.Rows(0)("CustomerNo").ToString() <> String.Empty Then

                CLpLine = getValueByKey("CLSCMP038") & dtView.Rows(0)("CustomerNo").ToString()

                CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CustomerName").ToString()
                If CLpLine <> Nothing AndAlso CLpLine.ToString().Length > 0 Then
                    StrSubFooter = StrSubFooter & CLpLine & vbCrLf
                End If
                StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
            End If


            Dim TotalQty, TGrossAmt, TDiscAmt, TRateAfterDisc, TTaxtAmt As String
            Dim TNetAmt As String = 0.0
            Dim BillLineNO As String
            DtScannedMettlerBills = obj.GetMettlerBillDetailsDataForPrint(CashMemoNo, Sitecode)
            Dim ScaleNo As String
            Dim objItemSch As New clsIteamSearch()
            Dim dtScanedBillArticle As New DataTable
            Dim dtNewArticleScaledtls As New DataTable
            Dim dtTender As DataTable 'sagar
            dtNewArticleScaledtls.Columns.Add("Article", System.Type.GetType("System.String"))
            dtNewArticleScaledtls.Columns.Add("ScaleNo", System.Type.GetType("System.String"))
            'dtNewArticleScaledtls.Columns.Add("BillNo", System.Type.GetType("System.String"))
            For ScanBillIndex = 0 To DtScannedMettlerBills.Rows.Count - 1
                '------- GET Mettler Scanned Bill Details From Mettler DataBase 
                With DtScannedMettlerBills
                    ScaleNo = Val(.Rows(ScanBillIndex)("SCALE_NO"))
                    dtScanedBillArticle = objItemSch.GetScanedBillArticle(ScaleNo:=ScaleNo, BillNo:=.Rows(ScanBillIndex)("Bill_No"), BillIntDate:=.Rows(ScanBillIndex)("MettlerScaleBillDate"), MettlerConn:=MettlerConnString) '    ScaleBillIntDate = Val(.Rows(ScanBillIndex)("MettlerScaleBillDate"))

                    If dtScanedBillArticle IsNot Nothing Then

                        For Each drscale As DataRow In dtScanedBillArticle.Rows
                            Dim drnew As DataRow = dtNewArticleScaledtls.NewRow
                            drnew("Article") = drscale("LegacyArticleCode")
                            drnew("ScaleNo") = ScaleNo
                            ' drnew("BillNo") = drscale("BillNo")
                            dtNewArticleScaledtls.Rows.Add(drnew)
                        Next

                    End If
                End With

            Next
            DtUnique.Columns.Add("ScaleNo", System.Type.GetType("System.Int32"))
            'DtUnique.Columns.Add("BillNo", System.Type.GetType("System.String"))
            For Each drscaleno As DataRow In dtNewArticleScaledtls.Rows
                Dim dr() As DataRow = DtUnique.Select("ArticleCode= '" & drscaleno("Article") & "'")
                If dr.Length > 0 Then
                    dr(0)("ScaleNo") = drscaleno("ScaleNo")
                    'dr(0)("BillNo") = drscaleno("BillNo")
                End If
            Next
            If Not dtNewArticleScaledtls Is Nothing Then
                If dtNewArticleScaledtls.Rows.Count = 0 Then
                    For Each druniq As DataRow In DtUnique.Rows
                        druniq("ScaleNo") = 0
                        'druniq("BillNo") = 0
                    Next
                Else
                    For Each druniq As DataRow In DtUnique.Rows
                        If Convert.ToString(druniq("ScaleNo")) = "" Then
                            druniq("ScaleNo") = 0
                            'druniq("BillNo") = 0
                        End If
                    Next
                End If
            End If
            For Each dr As DataRow In DtUnique.Rows
                ItemCode = dr("ArticleCode").ToString()

                If (DisplayArticleFullName) Then
                    Desc = dr("ArticleFullName").ToString()
                Else
                    Desc = dr("DISCRIPTION").ToString()
                End If
                BillLineNO = dr("BillLineNO").ToString()
                If SpectrumCommon.ComboItemPrintingAllowed AndAlso Dtcombo IsNot Nothing AndAlso Dtcombo.Rows.Count > 0 Then
                    Dim drp() = Dtcombo.Select("ComboArticleCode='" & ItemCode & "' AND BillLineNO ='" & BillLineNO & "'")
                    If drp.Length > 0 Then
                        Dim articleDescriptionDictionary As New Dictionary(Of String, Integer)
                        For index = 0 To drp.Length - 1
                            If articleDescriptionDictionary.ContainsKey(drp(index)("ArticleName")) Then
                                articleDescriptionDictionary(drp(index)("ArticleName")) = articleDescriptionDictionary(drp(index)("ArticleName")) + drp(index)("Quantity")
                            Else
                                articleDescriptionDictionary.Add(drp(index)("ArticleName"), drp(index)("Quantity"))
                            End If
                        Next
                        Dim AddedDesc = GetMultilinedString(articleDescriptionDictionary)
                        Desc = Desc & vbCrLf & AddedDesc
                    End If
                End If
                FinScaleNo = dr("ScaleNo").ToString()

                Qty = dr("Quantity").ToString()
                Rate = dr("SellingPrice").ToString()
                RateAfterDisc = (dr("SellingPrice") - dr("TOTALDISCOUNT")).ToString()
                If CDbl(clsCommon.CheckIfBlank(Rate)) < 0 Then
                    Rate = CDbl(clsCommon.CheckIfBlank(Rate)) * -1
                End If
                DiscAmt = dr("TOTALDISCOUNT").ToString()
                DiscPer = dr("TOTALDISCPERCENTAGE").ToString()
                NetAmt = dr("NetAmount").ToString()
                GrossAmt = dr("GROSSAMT").ToString()
                TaxAmt = dr("TOTALTAXAMOUNT").ToString()


                If (AllowDecimalQty) Then
                    If (WeightScaleEnabled) Then
                        Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 3)
                    Else
                        Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 2)
                    End If
                Else
                    Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 0)
                End If

                'For i = Qty.Length To 4
                '    Qty = " " & Qty
                'Next
                Rate = FormatNumber(CDbl(clsCommon.CheckIfBlank(Rate)), 2)

                RateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(RateAfterDisc)), 2)
                'Rate = FormatCurrency(Rate, 2)
                'For i = Rate.Length To 8
                '    Rate = " " & Rate
                'Next
                DiscAmt = FormatNumber(CDbl(IIf(DiscAmt <> String.Empty, DiscAmt, 0)), 2)
                'For i = DiscAmt.Length To 8
                '    DiscAmt = " " & DiscAmt
                'Next
                DiscPer = FormatNumber(CDbl(IIf(DiscPer <> String.Empty, DiscPer, 0)), 2)
                DiscPer = DiscPer & "%"
                'For i = DiscPer.Length To 8
                '    DiscPer = " " & DiscPer
                'Next
                'Format of NetAmt with No Digits after Decimal
                NetAmt = FormatNumber(CDbl(NetAmt), 0)
                NetAmtTemp = FormatNumber(CDbl(NetAmt), 2)
                ' NetAmt = FormatNumber(CDbl(NetAmt), 2)
                GrossAmt = FormatNumber(CDbl(GrossAmt), 2)
                'GrossAmt = FormatNumber(CDbl(GrossAmt), 0)
                'For i = NetAmt.Length To 8
                '    NetAmt = " " & NetAmt
                'Next
                TaxAmt = FormatNumber(CDbl(IIf(TaxAmt <> String.Empty, TaxAmt, 0)), 2)
                'For i = TaxAmt.Length To 7
                '    TaxAmt = " " & TaxAmt
                'Next

                Dim TempNetAmt As String = "0"

                If Type = "CMWAmt" Then
                    'ashma
                    StrLineItem = ItemCode.PadRight(26) & vbCrLf & Desc.PadRight(35) & Qty & vbCrLf
                    '------------
                Else
                    If 28 - Desc.Length < Qty.Length Then
                        Select Case PrintFormatNo
                            Case 1
                            Case 2

                            Case Else
                                TempNetAmt = NetAmt
                                If Val(NetAmt) > 0 AndAlso Val(TaxAmt) > 0 Then
                                    TempNetAmt = CDbl(GrossAmt) - CDbl(TaxAmt)
                                    'ashma
                                    StrLineItem = Desc & vbCrLf
                                    StrLineItem = StrLineItem & Qty.PadLeft(28) & GrossAmt.PadLeft(11) & vbCrLf
                                    '-----------
                                End If
                        End Select
                    Else
                        Select Case PrintFormatNo
                            Case 1

                            Case 2

                            Case Else
                                TempNetAmt = NetAmt
                                If Val(NetAmt) > 0 AndAlso Val(TaxAmt) > 0 Then
                                    TempNetAmt = CDbl(GrossAmt) - CDbl(TaxAmt)
                                    'ashma
                                    StrLineItem = Desc & Qty.PadLeft(28 - Desc.Length) & GrossAmt.PadLeft(11) & vbCrLf
                                    '---------------------
                                End If
                        End Select
                    End If
                End If
                ' Added For Font Changes as Requested By Client-------------
                ' strLineDetail.Append(StrLineItem)
                strLineDetail.Append(StrLineItem)
                '--------------------

                If CDbl(Qty.Trim) > 0 Then
                    TotalQty = CDbl(clsCommon.CheckIfBlank(TotalQty)) + CDbl(Qty.Trim)
                End If
                TDiscAmt = CDbl(clsCommon.CheckIfBlank(TDiscAmt)) + CDbl(DiscAmt.Trim)
                TGrossAmt = CDbl(clsCommon.CheckIfBlank(TGrossAmt)) + CDbl(dr("GROSSAMT").ToString())
                'TNetAmt = CDbl(clsCommon.CheckIfBlank(TNetAmt)) + CDbl(NetAmt.Trim)
                'TNetAmt = Format(Math.Round(Convert.ToDecimal(CDbl(clsCommon.CheckIfBlank(TNetAmt)) + CDbl(NetAmt.Trim))), "0.00")
                TNetAmt = Decimal.Add(FormatNumber(CDbl(NetAmtTemp), 2).ToString(), TNetAmt)
                TRateAfterDisc = CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)) + CDbl(RateAfterDisc.Trim)
                TTaxtAmt = CDbl(clsCommon.CheckIfBlank(TTaxtAmt)) + CDbl(TaxAmt.Trim)
            Next

            TotalQty = FormatNumber(CDbl(clsCommon.CheckIfBlank(TotalQty)), 2)
            'TDiscAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TDiscAmt)), 2)
            TDiscAmt = FormatNumber(dtView.Rows(0)("Discount"), 2)
            TGrossAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TGrossAmt)), 2)
            'TNetAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TNetAmt)), 2)
            TNetAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TNetAmt)), RoundOff), 2)
            TRateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)), 2)
            TTaxtAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TTaxtAmt)), 2)

            'ashma
            StrBody = strLineDetail.ToString() & vbCrLf
            StrBody = StrBody & strLine
            '---

            Dim TSubTotalAmt As String
            Dim TotalDisPercent As String
            If Type <> "CMWAmt" Then
                If CDbl(clsCommon.CheckIfBlank(TDiscAmt)) > 0 Then
                    TotalDisPercent = Math.Round((CDbl(clsCommon.CheckIfBlank(TDiscAmt)) / CDbl(clsCommon.CheckIfBlank(TGrossAmt))) * 100, 1).ToString()
                    TSubTotalAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TGrossAmt)) - CDbl(clsCommon.CheckIfBlank(TDiscAmt)), RoundOff), 0)
                    TDiscAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TDiscAmt)), RoundOff), 0)
                    Dim ObjclsCommon As New clsCommon
                    Dim offerno As String = ObjclsCommon.GetOffernoForRoundOff()
                    If DtUnique.Rows(0)("PromotionId").ToString().Contains(offerno) And offerno <> "" Then
                        ' TotalDiscAmtLine = "Round Off" & Space(BoldLineLength / 2 - "Round Off".Length) & " :" & TDiscAmt.PadLeft(BoldLineLength - ("Round Off".Length + Space(BoldLineLength / 2 - "Round Off".Length).Length + ":".Length))
                    Else
                        '  Dim CheckIfPositiveNumber = IIf(BoldLineLength / 2 - (String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).Length) > 0, BoldLineLength / 2 - String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).Length, 0)
                        '  TotalDiscAmtLine = String.Format(getValueByKey("CLSCMP019"), TotalDisPercent) & Space(CheckIfPositiveNumber) & " :" & TDiscAmt.PadLeft(BoldLineLength - (String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).Length + ": ".Length))
                    End If
                    '--------------------
                End If

                TGrossAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TGrossAmt)), RoundOff), 0)
                TNetAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TNetAmt)), RoundOff), 0)
                TRateAfterDisc = TRateAfterDisc & " " & Currency
                TTaxtAmt = TTaxtAmt & " " & Currency
                TotalGrossAmtLine = "Total Amt" & Space(5) & ":" & TGrossAmt.PadLeft(BoldLineLength - Val("Total Amt".Length + Space(5).Length))
                If (CDbl(TDiscAmt.Replace("INR", ""))) > 0 Then
                    NetAmtLine = getValueByKey("CLSCMP020") & Space(2) & ":" & TNetAmt.PadLeft(BoldLineLength - Val(getValueByKey("CLSCMP020").Length + Space(2).Length))
                End If
                '--------------------
                'ashma
                Dim taxDetailsForBill As DataTable = obj.GetTaxDetailsForBillNo(Sitecode, dtView.Rows(0)("Billno").ToString())
                If _TaxDetail = True Then
                    If taxDetailsForBill IsNot Nothing AndAlso taxDetailsForBill.Rows.Count > 0 Then
                        For Each row As DataRow In taxDetailsForBill.Rows
                            'If CompositeTaxReqOnPrint = False AndAlso IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                            If IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                            Dim taxValue As String = row("TaxVAlue").ToString()
                            taxValue = FormatNumber(CDbl(clsCommon.CheckIfBlank(taxValue)), 2)
                            taxValue = taxValue & " " & Currency
                            FooterTaxLine = FooterTaxLine & row("TaxName").PadRight(LineLength - 16) & ":" & taxValue.PadLeft(14) & vbCrLf
                        Next
                    End If
                End If
                TotalFooterTaxDetail = FooterTaxLine

                'ashma
                StrBody = StrBody & TotalGrossAmtLine & vbCrLf
                If TotalDiscAmtLine IsNot Nothing Then
                    StrBody = StrBody & TotalDiscAmtLine & vbCrLf
                End If
                If SubTotalLine IsNot Nothing Then
                    StrBody = StrBody & strLine.Substring(30) & SubTotalLine & vbCrLf
                End If

                StrBody = StrBody & TotalFooterTaxDetail & strLine.Substring(30) & NetAmtLine & vbCrLf & strLine

                TenderLine = getValueByKey("CLSCMP021") & vbCrLf
                'Dim dtTender As DataTable = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                '------------------------
                If KOTAndBillGeneration = True Then
                    dtTender = obj.GetTenderTypeByRefBillNo(dtView.Rows(0)("BILLNO").ToString())
                    If dtTender.Rows.Count = 0 Then
                        dtTender = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                    End If
                Else
                    dtTender = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                End If
                ' dtTender = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                For Each drTender As DataRow In dtTender.Rows
                    If UCase(obj.GetDefaultConfigValue(Sitecode, "CVInfoReqOnPrint")) = "FALSE" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)") Then
                        Continue For
                    End If
                    Dim amounttendered As String = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(drTender("AMOUNTTENDERED").ToString)), RoundOff), 0)
                    Dim tender As String = drTender("CurrencyCode").ToString() & " " & amounttendered & vbCrLf
                    '----------------
                    Dim tenderName As String
                    'Added by Rohit
                    If drTender("CardNO").ToString() <> "" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(I)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(R)") Then
                        tenderName = drTender("TENDERHEADNAME").ToString() & "(" & drTender("CardNO").ToString() & ") "
                    Else
                        tenderName = drTender("TENDERHEADNAME").ToString()
                    End If
                    'End editing
                    If Not drTender("AMOUNTINCURRENCY") Is DBNull.Value AndAlso drTender("AMOUNTINCURRENCY") <> 0 Then '0000404
                        tender = drTender("CurrencyCode").ToString() & " " & FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(drTender("AMOUNTINCURRENCY").ToString())), RoundOff), 0) & vbCrLf
                    End If
                    If drTender("TENDERTYPECODE").ToString() = "CLPPoint" Then
                        tender = FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()), 0)
                    End If

                Next

                If (_IsDisplayTotalSaving) Then
                    If (Not String.IsNullOrEmpty(TDiscAmt) AndAlso Not String.Equals(TDiscAmt, "0.00")) Then
                        Dim totalSavingAmount As String = String.Format(getValueByKey("CLSCMP033"), Currency, TDiscAmt.Replace(Currency, String.Empty).Trim)
                    Else
                    End If
                Else
                End If


                '--------------------
                'Company and Tin Info
                Dim NoOfReprints As String = IIf(IsDBNull(dtView.Rows(0)("NoofReprints")), String.Empty, dtView.Rows(0)("NoofReprints")).ToString()
                Dim CompanyName As String = dtView.Rows(0)("SITEOFFICIALNAME") 'obj.GetDefaultConfigValue(Sitecode, "CompanyName")
                ''Dim remarks As String = dtView.Rows(0)("Remark")
                'If Not IsDBNull(dtView.Rows(0)("Remark")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("Remark")) Then
                '    Dim remarks As String = dtView.Rows(0)("Remark")
                '    remarks = SplitString(getValueByKey("cashmemoprint.discountremark") & remarks, LineLength).ToString()
                '    StrCompanyInfo = StrCompanyInfo & remarks.TrimEnd() & vbCrLf
                'End If

                'If Not String.IsNullOrEmpty(NoOfReprints) Then
                '    StrCompanyInfo = StrCompanyInfo & "Re-Print Number : " & NoOfReprints & vbCrLf
                '    StrCompanyInfo = StrCompanyInfo & "Re-Print Date : " & DateTime.Now.ToShortDateString() & vbCrLf
                'End If
                'Company and Tin Info End

                Dim dtPromo As DataTable = dvItemDetail.ToTable(True, "PROMOTIONALMSGID", "PROMOMESS")
                For Each drTerms As DataRow In dtPromo.Select("", "PROMOTIONALMSGID", DataViewRowState.CurrentRows)
                    If Not String.IsNullOrEmpty(drTerms("PROMOMESS").ToString()) Then
                        PromoMsgLine = PromoMsgLine & drTerms("PROMOMESS").ToString() & vbCrLf
                    End If
                Next

                TotalPromotionalMsg = PromoMsgLine
                strPrintMsg = New StringBuilder()
                sbDeliveryBillPrint = New StringBuilder()

                'If dtView.Rows.Count() > 0 Then
                '    '---- Changes By Mahesh : Customer Name is Not Mandatory for Home Delivery 
                '    'If dtView.Rows(0)("HDName").ToString() <> String.Empty Then
                '    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                '        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP022") & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP023") & dtView.Rows(0)("HDName").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & SplitString(getValueByKey("CLSCMP024") & dtView.Rows(0)("HDAddress").ToString(), LineLength).ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP025") & dtView.Rows(0)("HDTelNo").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP026") & dtView.Rows(0)("HDEmail").ToString() & vbCrLf

                '    Else
                '        If _CustomerNameRequiredInKOT AndAlso CustomerNameLine <> Nothing Then
                '            StrCustomerName = CustomerNameLine.ToUpper.Replace("CUSTOMER", "").Trim()

                '        End If
                '        If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                '            If Not AllowBillingOnlyAfterSelectionOfSalesType Then
                '                CustomerSaleType = String.Empty
                '            End If
                '            Dim StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - CustomerSaleType.Length) & CustomerSaleType & vbCrLf & vbCrLf
                '        End If
                '    End If
                'End If
                Dim dtPrint As New DataTable
                Dim objPrint As New SpectrumBL.clsCommon
                dtPrint = objPrint.GetPrintingDetails(Type)
                'If Not dtPrint Is Nothing Then
                '    Dim filter As String = ""
                '    Dim dv As New DataView(dtPrint, filter, "Sequenceno", DataViewRowState.CurrentRows)
                '    filter = "TOPBOTTOM='Bottom'"
                '    dv.RowFilter = filter
                '    For Each drview As DataRowView In dv
                '        If drview("ReceiptText").ToString().Length >= 25 Then
                '        Else
                '        End If
                '    Next

                'End If
                If DtUnique.Rows.Count > 0 Then
                    For index = 0 To DtUnique.Rows.Count - 1
                        DtUnique(index)("GROSSAMT") = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(DtUnique(index)("GROSSAMT"))), RoundOff), 0)
                    Next
                    DtUnique.AcceptChanges()
                End If
                Dim dtHeaderDetails As New DataTable
                dtHeaderDetails.Columns.Add("Header", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("Footer", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("ClientName", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("INR", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("StoreName", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("Address", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("PhoneNo", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("FINALRECEIPT", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("TotalAmt", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("LessDisPer", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("LessDisAmt", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("SubTotal", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("TotalToPay", System.Type.GetType("System.Single"))
                dtHeaderDetails.Columns.Add("CashierName", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("CustomerName", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("CustomerAddress", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("CustomerPhoneNo", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("DeliveryDate", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("DeliveryName", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("SalesType", System.Type.GetType("System.String"))
                dtHeaderDetails.Columns.Add("DeliveryNote", System.Type.GetType("System.String"))

                Dim drnewS = dtHeaderDetails.NewRow
                drnewS("Header") = "Submit Counter Copy at Scale"
                drnewS("Footer") = "BILL PAID"
                drnewS("ClientName") = ClientName.Trim
                drnewS("INR") = 0
                drnewS("StoreName") = SiteName.Trim
                drnewS("Address") = addressLine1.Trim & Space(1) & addressLine2.Trim & Space(1) & addressLine3.Trim
                drnewS("PhoneNo") = PhoneLine.Trim
                drnewS("FINALRECEIPT") = "FINAL RECEIPT"
                drnewS("TotalAmt") = TGrossAmt.Trim
                If TotalDisPercent Is Nothing Then
                    drnewS("LessDisPer") = 0
                Else
                    drnewS("LessDisPer") = TotalDisPercent
                End If

                drnewS("LessDisAmt") = TDiscAmt
                If TSubTotalAmt Is Nothing Then
                    drnewS("SubTotal") = 0
                Else
                    drnewS("SubTotal") = TSubTotalAmt
                End If

                '-----------
                drnewS("TotalToPay") = TNetAmt
                drnewS("CashierName") = LoginUserName
                drnewS("CustomerName") = customername
                drnewS("CustomerAddress") = clpaddressnew
                drnewS("CustomerPhoneNo") = customerPhoneno
                drnewS("DeliveryDate") = DeliveryDate
                drnewS("DeliveryName") = DeliveryName
                drnewS("SalesType") = CustomerSaleType
                drnewS("DeliveryNote") = "DELIVERY NOTE"
                dtHeaderDetails.Rows.Add(drnewS)

                If _PrintPreview = True Then
                    If KOTAndBillGeneration = False Then
                        If IsCounterCopyKot = False Then
                            Call CashMemoKOTPrintBasedOnPrintFormatNoSSRS(Type, errorMsg, dtView, taxDetailsForBill, DtUnique, Dtcombo, CustomerNameLine, dayopenDate, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, ClientName:=ClientName, DayCloseReportPath:=DayCloseReportPath)
                        End If
                    End If
                    If IsFinalReceipt = False Then
                        If PrintFormatNo = 6 Then
                            If IsCounterCopyKot = False Then
                                If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                                    GenerateCashMemoFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, taxDetailsForBill, dtPrint, dtTender, DayCloseReportPath)
                                End If
                            End If
                        End If
                        'GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails,taxDetailsForBill, dtPrint, dtTender, DayCloseReportPath)
                    End If
                Else
                    If KOTAndBillGeneration = False Then
                        If IsCounterCopyKot = False Then
                            Call CashMemoKOTPrintBasedOnPrintFormatNoSSRS(Type, errorMsg, dtView, DtUnique, taxDetailsForBill, Dtcombo, CustomerNameLine, dayopenDate, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, ClientName:=ClientName, DayCloseReportPath:=DayCloseReportPath)
                        End If
                    End If
                    If IsFinalReceipt Then
                        If PrintFormatNo = 6 Then
                            If IsCounterCopyKot = False Then
                                If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                                    GenerateCashMemoFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, taxDetailsForBill, dtPrint, dtTender, DayCloseReportPath)
                                End If
                            End If

                        End If
                        '    GenerateFinalReceiptPrint(dtView, DtUnique, dtHeaderDetails, taxDetailsForBill, dtPrint, dtTender, DayCloseReportPath)
                    End If

                End If
            Else
                errorMsg = getValueByKey("CLSCMP031")
            End If
        Catch ex As Exception
            errorMsg = ex.Message
        End Try
    End Sub
    '----------------------------------------------------------
    'ashma
    Public Sub CashMemoKOTPrintMettlerBillWiseSSRS(ByVal Type As String, ByRef errorMsg As String, ByRef dtView As DataTable, ByRef taxDetailsForBill As DataTable, ByVal DtUnique As DataTable, ByVal Dtcombo As DataTable, CustomerNameLine As String, Optional ByVal dayopendate As Date = Nothing, Optional ByVal ClientName As String = "", Optional ByVal DayCloseReportPath As String = "")
        Try
            '------ Variable Declaration ----------------
            Dim strLineDetail, sbKOTBillPrintBase, sbKOTBillPrintArticleMain As New StringBuilder
            Dim StrCMNo, StrCustomerName As String
            Dim ItemCode, Desc, Qty, PricePerUnit, Disc, mettlerBillQty, BillNoList As String
            Dim dtFinalArticle As New DataTable
            Dim strLine, strDblLine, StrHeader, StrSubHeader As String
            Dim ValueMultiplyFactor As Double = 1, ItemAmt, BillTotalAmt As Double
            Dim isTakeAwayQtyPrint As Boolean = False
            Dim objItemSch As New clsIteamSearch()
            Dim ScaleNo, BillNo, ScaleBillIntDate, totalItems, colSpacelength, TotalLineItems As Long
            '------ Initilize Function ----------------
            LineLength = 30
            strLine = "-".PadRight(LineLength, "-") & vbCrLf
            strDblLine = "=".PadRight(LineLength, "=") & vbCrLf
            errorMsg = ""
            '---- Create Sub Header Variables -----
            If Type = "DCM" Then
                StrCMNo = getValueByKey("CLSCMP008") & dtView.Rows(0)("Billno").ToString()
            Else
                StrCMNo = getValueByKey("CLSCMP009") & dtView.Rows(0)("Billno").ToString()
            End If
            Dim StrCashMemoLine = StrCMNo & vbCrLf
            Dim StrCMTime = getValueByKey("CLSCMP032") & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")
            '---- Making Print for EACH Mettler Bill ----
            StrHeader = GetMettlerKOTBillHeader(dtView)

            For ScanBillIndex = 0 To DtScannedMettlerBills.Rows.Count - 1
                BillTotalAmt = 0
                Dim DtScannedMettlerBillNew As New DataTable
                DtScannedMettlerBillNew = DtScannedMettlerBills.Clone()
                '------- GET Mettler Scanned Bill Details From Mettler DataBase 
                With DtScannedMettlerBills
                    ScaleNo = Val(.Rows(ScanBillIndex)("SCALE_NO")) : BillNo = Val(.Rows(ScanBillIndex)("BILL_NO"))
                   
                    ScaleBillIntDate = Val(.Rows(ScanBillIndex)("MettlerScaleBillDate"))
                    TotalLineItems = Val(.Rows(ScanBillIndex)("TotalLineItems"))
                End With
                With DtScannedMettlerBills
                    Dim drScanNew = DtScannedMettlerBillNew.NewRow
                    drScanNew("MettlerScaleBillDate") = ScaleBillIntDate
                    drScanNew("SCALE_NO") = ScaleNo
                    drScanNew("BILL_NO") = BillNo
                   
                    drScanNew("TotalLineItems") = TotalLineItems
                    DtScannedMettlerBillNew.Rows.Add(drScanNew)
                End With

                BillNoList = BillNoList & BillNo.ToString() & ","
                Dim dtScanedBillArticle = objItemSch.GetScanedBillArticle(ScaleNo:=ScaleNo, BillNo:=BillNo, BillIntDate:=ScaleBillIntDate, MettlerConn:=MettlerConnString)

                Try
                    Dim test = (From BillItems In DtUnique.AsEnumerable() Select BillItems Join MettlerBillItems In dtScanedBillArticle.AsEnumerable()
                            On BillItems.Field(Of String)("ArticleCode") Equals MettlerBillItems.Field(Of Int64)("LegacyArticleCode").ToString()
                            Select BillItems).FirstOrDefault()

                    If test Is Nothing Then Continue For
                Catch ex As Exception
                    LogException(ex)
                End Try
                'BillNoList = BillNoList & BillNo.ToString() & ","
                'Dim dtScanedBillArticle = Nothing
                'If SpectrumAsMettler Then

                '    Dim dtScanedBillArticleSpectrumMettler = objItemSch.GetScanedBillArticleForSpectrumMettler(DtScannedMettlerBills.Rows(ScanBillIndex)("BILL_NO")) '    ScaleBillIntDate = Val(.Rows(ScanBillIndex)("MettlerScaleBillDate"))
                '    dtScanedBillArticle = dtScanedBillArticleSpectrumMettler
                '    Try
                '        Dim test = (From BillItems In DtUnique.AsEnumerable() Select BillItems Join MettlerBillItems In dtScanedBillArticleSpectrumMettler.AsEnumerable()
                '                On BillItems.Field(Of String)("ArticleCode") Equals MettlerBillItems.Field(Of String)("LegacyArticleCode").ToString()
                '                Select BillItems).FirstOrDefault()
                '        dtScanedBillArticle = dtScanedBillArticleSpectrumMettler
                '        If test Is Nothing Then Continue For
                '    Catch ex As Exception
                '        LogException(ex)
                '    End Try

                'Else
                '    Dim dtScanedBillArticleMettler = objItemSch.GetScanedBillArticle(ScaleNo:=ScaleNo, BillNo:=BillNo, BillIntDate:=ScaleBillIntDate, MettlerConn:=MettlerConnString)
                '    dtScanedBillArticle = dtScanedBillArticleMettler
                '    Try
                '        Dim test = (From BillItems In DtUnique.AsEnumerable() Select BillItems Join MettlerBillItems In dtScanedBillArticleMettler.AsEnumerable()
                '                On BillItems.Field(Of String)("ArticleCode") Equals MettlerBillItems.Field(Of Int64)("LegacyArticleCode").ToString()
                '                Select BillItems).FirstOrDefault()
                '        dtScanedBillArticle = dtScanedBillArticleMettler
                '        If test Is Nothing Then Continue For
                '    Catch ex As Exception
                '        LogException(ex)
                '    End Try

                'End If
                '' Addeed By ketan Change For Deleted Article Are display in Counter Copy 
                Dim dtArticlePrint As New DataTable
                For Each dr As DataRow In dtScanedBillArticle.Rows
                    Dim drSelArt() = DtUnique.Select("ArticleCode='" & dr("LegacyArticleCode") & "'")
                    'Dim Scale As Int16 = dr("ScaleNo")
                    'Dim drSelArt() = DtUnique.Select("ArticleCode='" & dr("LegacyArticleCode") & "' AND BillNo = '" & dr("BillNo") & "' AND ScaleNo = '" & Scale & "' ")
                    If drSelArt.Count = 0 Then
                        dr.Delete()
                    End If
                Next
                dtScanedBillArticle.AcceptChanges()
                TotalLineItems = dtScanedBillArticle.Rows.Count
                For Each dr As DataRow In DtScannedMettlerBillNew.Rows
                    dr("TotalLineItems") = TotalLineItems
                Next

                '---- Columns Heading 

                For BillRowIndex = 0 To dtScanedBillArticle.Rows.Count - 1 Step 1
                    Dim PrintDataRows() = DtUnique.Select("ArticleCode ='" & dtScanedBillArticle.Rows(BillRowIndex)("LegacyArticleCode") & "'")
                    If Not PrintDataRows Is Nothing And PrintDataRows.Count > 0 Then
                        '------ STEP-1:  collecting all Data With Format -----
                        For Each dr As DataRow In PrintDataRows
                            ItemCode = dr("ArticleCode").ToString()
                            Desc = IIf(DisplayArticleFullName, dr("ArticleFullName").ToString(), dr("DISCRIPTION").ToString())
                            Qty = dr("Quantity").ToString()
                            mettlerBillQty = dtScanedBillArticle.Rows(BillRowIndex)("Quantity")
                            'totalItems = Val(DtScannedMettlerBills.Rows(ScanBillIndex)("TotalLineItems")).ToString()
                            'ValueMultiplyFactor = Math.Round(Val(mettlerBillQty) / Val(Qty), 3)
                            totalItems = totalItems + 1 'Val(DtScannedMettlerBills.Rows(ScanBillIndex)("TotalLineItems")).ToString()
                            ValueMultiplyFactor = Val(mettlerBillQty) / Val(Qty)
                            'PricePerUnit = dr("SELLINGPRICE").ToString()
                            PricePerUnit = Math.Round(dr("SELLINGPRICE")).ToString
                            Disc = (Val(dr("TOTALDISCOUNT").ToString()) * ValueMultiplyFactor).ToString
                            ItemAmt = Math.Round(Val(dr("GrossAMT").ToString()) * ValueMultiplyFactor, 2).ToString
                            'BillTotalAmt = BillTotalAmt + Val(ItemAmt)
                            'ItemAmt = Math.Round(ItemAmt).ToString()

                           
                            ItemAmt = FormatNumber(ItemAmt, 2).ToString()
                            ' BillTotalAmt = Math.Round(BillTotalAmt + Val(ItemAmt)).ToString
                            BillTotalAmt = FormatNumber(BillTotalAmt + Val(ItemAmt)).ToString

                            Call SetQtyFormat(mettlerBillQty)
                        Next
                    Else
                        Continue For
                    End If
                Next BillRowIndex
                Dim dtCounterSeparateData As New DataTable
                dtCounterSeparateData.Columns.Add("Header", System.Type.GetType("System.String"))
                dtCounterSeparateData.Columns.Add("Footer", System.Type.GetType("System.String"))
                dtCounterSeparateData.Columns.Add("ClientName", System.Type.GetType("System.String"))
                dtCounterSeparateData.Columns.Add("INR", System.Type.GetType("System.Single"))
                Dim drnews = dtCounterSeparateData.NewRow
                drnews("Header") = "Submit Counter Copy at Scale"
                drnews("Footer") = "BILL PAID"
                drnews("ClientName") = ClientName
                drnews("INR") = BillTotalAmt
                dtCounterSeparateData.Rows.Add(drnews)
                GenerateMettlerCounterCopyPrint(dtView, dtScanedBillArticle, DtScannedMettlerBillNew, dtCounterSeparateData, DayCloseReportPath:=DayCloseReportPath)
                dtFinalArticle = dtScanedBillArticle.Clone()

            Next ScanBillIndex
            Dim isPrintOtherItems As Boolean

            Dim dtScanedBillArticleSum = objItemSch.GetScanedCashMemoBillsArticle(BillNoList:=BillNoList.Substring(0, BillNoList.Length - 1), BillIntDate:=ScaleBillIntDate, MettlerConn:=MettlerConnString)
            Dim DtScannedItemStruct As New DataTable
            Dim BillTotalAmtSepCopy As Double
            DtScannedItemStruct = DtScannedMettlerBills.Clone()
            totalItems = 0
            For Each dr As DataRow In DtUnique.Select("ScaleNo='0'")
                If Val(dr("ArticleCode")) > 0 Then
                    Dim PrintDataRows() = dtScanedBillArticleSum.Select("LegacyArticleCode ='" & dr("ArticleCode") & "'")
                    Dim PrintDataRowsCondition2() = dtScanedBillArticleSum.Select("LegacyArticleCode ='" & dr("ArticleCode") & "' AND Quantity < " & dr("Quantity"))
                    If (PrintDataRows.Count = 0) Or (PrintDataRowsCondition2.Count > 0) Then
                        isPrintOtherItems = True
                        totalItems = totalItems + 1
                        ItemCode = dr("ArticleCode").ToString()
                        Desc = IIf(DisplayArticleFullName, dr("ArticleFullName").ToString(), dr("DISCRIPTION").ToString())
                        If PrintDataRows.Count > 0 Then
                            Qty = (Val(dr("Quantity")) - Val(PrintDataRows(0)("Quantity"))).ToString
                            'ValueMultiplyFactor = Math.Round((Val(dr("Quantity")) - Val(PrintDataRows(0)("Quantity"))) / dr("Quantity"), 3)
                            ValueMultiplyFactor = (Val(dr("Quantity")) - Val(PrintDataRows(0)("Quantity"))) / dr("Quantity")
                        Else
                            Qty = dr("Quantity").ToString
                            ValueMultiplyFactor = 1
                        End If
                        PricePerUnit = dr("SELLINGPRICE").ToString()
                        Disc = Math.Round(Val(dr("TOTALDISCOUNT").ToString()) * ValueMultiplyFactor, 2).ToString
                        ItemAmt = Math.Round(Val(dr("NETAMOUNT").ToString()) * ValueMultiplyFactor, 2).ToString
                        BillTotalAmtSepCopy = BillTotalAmtSepCopy + Val(ItemAmt)
                        Call SetQtyFormat(Qty)
                        If dr("UnitofMeasure").ToString().ToUpper = "KGS" Then
                            Qty = Qty & " Kg"
                        End If
                    End If
                End If
            Next
            '-------------------------------------------------------------------
            Dim dtCounterSeparateDataSep As New DataTable
            dtCounterSeparateDataSep.Columns.Add("Header", System.Type.GetType("System.String"))
            dtCounterSeparateDataSep.Columns.Add("Footer", System.Type.GetType("System.String"))
            dtCounterSeparateDataSep.Columns.Add("ClientName", System.Type.GetType("System.String"))
            dtCounterSeparateDataSep.Columns.Add("INR", System.Type.GetType("System.Single"))
            Dim drnew = dtCounterSeparateDataSep.NewRow
            drnew("Header") = "Submit Counter Copy at Scale"
            drnew("Footer") = "BILL PAID"
            drnew("ClientName") = ClientName
            drnew("INR") = BillTotalAmtSepCopy
            dtCounterSeparateDataSep.Rows.Add(drnew)
            Dim dtArticleData As New DataTable
            If DtUnique.Select("ScaleNo='0'").Count > 0 Then
                dtArticleData = DtUnique.Select("ScaleNo='0'").CopyToDataTable
                For Each drscale As DataRow In dtArticleData.Rows
                    Dim drNewFinalArticle = dtFinalArticle.NewRow
                    drNewFinalArticle("ScaleNo") = DBNull.Value
                    drNewFinalArticle("BillNo") = drscale("BillLineNo")
                    drNewFinalArticle("SequenceNo") = "0"
                    drNewFinalArticle("LegacyArticleCode") = drscale("ArticleCode")
                    drNewFinalArticle("c_article_name") = drscale("DISCRIPTION")
                    drNewFinalArticle("Quantity") = drscale("Quantity")
                    dtFinalArticle.Rows.Add(drNewFinalArticle)
                Next
                Dim dr = DtScannedItemStruct.NewRow
                dr("TotalLineItems") = totalItems
                DtScannedItemStruct.Rows.Add(dr)
                GenerateMettlerCounterCopyPrint(dtView, dtFinalArticle, DtScannedItemStruct, dtCounterSeparateDataSep, DayCloseReportPath:=DayCloseReportPath)
            End If
        Catch ex As Exception
            errorMsg = ex.Message
        End Try
    End Sub
    Public Sub CashMemoKOTPrintMettlerBillWiseSSRS(ByVal Type As String, ByRef errorMsg As String, ByRef dtView As DataTable, ByVal DtUnique As DataTable, ByVal Dtcombo As DataTable, CustomerNameLine As String, Optional ByVal dayopendate As Date = Nothing, Optional ByVal ClientName As String = "", Optional ByVal DayCloseReportPath As String = "")
        Try
            '------ Variable Declaration ----------------
            Dim strLineDetail, sbKOTBillPrintBase, sbKOTBillPrintArticleMain As New StringBuilder
            Dim StrCMNo, StrCustomerName As String
            Dim ItemCode, Desc, Qty, PricePerUnit, Disc, mettlerBillQty, BillNoList, SpectrumMettlerScaleNo, SpectrumMettlerBillNo As String
            Dim dtFinalArticle As New DataTable
            Dim strLine, strDblLine, StrHeader, StrSubHeader As String
            Dim ValueMultiplyFactor As Double = 1, ItemAmt, BillTotalAmt As Double
            Dim isTakeAwayQtyPrint As Boolean = False
            Dim objItemSch As New clsIteamSearch()
            Dim ScaleNo, BillNo, ScaleBillIntDate, totalItems, colSpacelength, TotalLineItems As Long
            '------ Initilize Function ----------------
            LineLength = 30
            strLine = "-".PadRight(LineLength, "-") & vbCrLf
            strDblLine = "=".PadRight(LineLength, "=") & vbCrLf
            errorMsg = ""
            '---- Create Sub Header Variables -----
            If Type = "DCM" Then
                StrCMNo = getValueByKey("CLSCMP008") & dtView.Rows(0)("Billno").ToString()
            Else
                StrCMNo = getValueByKey("CLSCMP009") & dtView.Rows(0)("Billno").ToString()
            End If
            Dim StrCashMemoLine = StrCMNo & vbCrLf
            Dim StrCMTime = getValueByKey("CLSCMP032") & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")
            '---- Making Print for EACH Mettler Bill ----
            StrHeader = GetMettlerKOTBillHeader(dtView)

            For ScanBillIndex = 0 To DtScannedMettlerBills.Rows.Count - 1
                BillTotalAmt = 0
                Dim DtScannedMettlerBillNew As New DataTable
                DtScannedMettlerBillNew = DtScannedMettlerBills.Clone()
                '------- GET Mettler Scanned Bill Details From Mettler DataBase 
                With DtScannedMettlerBills
                    ScaleNo = Val(.Rows(ScanBillIndex)("SCALE_NO")) : BillNo = Val(.Rows(ScanBillIndex)("BILL_NO"))
                    If SpectrumAsMettler Then
                        SpectrumMettlerScaleNo = DtScannedMettlerBills.Rows(ScanBillIndex)("SCALE_NO").ToString
                        SpectrumMettlerBillNo = DtScannedMettlerBills.Rows(ScanBillIndex)("BILL_NO").ToString
                    End If
                    ScaleBillIntDate = Val(.Rows(ScanBillIndex)("MettlerScaleBillDate"))
                    TotalLineItems = Val(.Rows(ScanBillIndex)("TotalLineItems"))
                End With
                With DtScannedMettlerBills
                    Dim drScanNew = DtScannedMettlerBillNew.NewRow
                    drScanNew("MettlerScaleBillDate") = ScaleBillIntDate
                    drScanNew("SCALE_NO") = ScaleNo
                    drScanNew("BILL_NO") = BillNo
                    If SpectrumAsMettler Then

                        drScanNew("SCALE_NO") = SpectrumMettlerScaleNo
                        drScanNew("BILL_NO") = SpectrumMettlerBillNo.ToString().Trim.Substring(SpectrumMettlerBillNo.ToString().Trim.Length - 4, 4)

                    End If

                    drScanNew("TotalLineItems") = TotalLineItems
                    DtScannedMettlerBillNew.Rows.Add(drScanNew)
                End With

                'BillNoList = BillNoList & BillNo.ToString() & ","
                'Dim dtScanedBillArticle = objItemSch.GetScanedBillArticle(ScaleNo:=ScaleNo, BillNo:=BillNo, BillIntDate:=ScaleBillIntDate, MettlerConn:=MettlerConnString)

                'Try
                '    Dim test = (From BillItems In DtUnique.AsEnumerable() Select BillItems Join MettlerBillItems In dtScanedBillArticle.AsEnumerable()
                '            On BillItems.Field(Of String)("ArticleCode") Equals MettlerBillItems.Field(Of Int64)("LegacyArticleCode").ToString()
                '            Select BillItems).FirstOrDefault()

                '    If test Is Nothing Then Continue For
                'Catch ex As Exception
                '    LogException(ex)
                'End Try


                BillNoList = BillNoList & BillNo.ToString() & ","
                Dim dtScanedBillArticle = Nothing
                If SpectrumAsMettler Then

                    Dim dtScanedBillArticleSpectrumMettler = objItemSch.GetScanedBillArticleForSpectrumMettler(DtScannedMettlerBills.Rows(ScanBillIndex)("BILL_NO")) '    ScaleBillIntDate = Val(.Rows(ScanBillIndex)("MettlerScaleBillDate"))
                    dtScanedBillArticle = dtScanedBillArticleSpectrumMettler
                    Try
                        Dim test = (From BillItems In DtUnique.AsEnumerable() Select BillItems Join MettlerBillItems In dtScanedBillArticleSpectrumMettler.AsEnumerable()
                                On BillItems.Field(Of String)("ArticleCode") Equals MettlerBillItems.Field(Of String)("LegacyArticleCode").ToString()
                                Select BillItems).FirstOrDefault()
                        dtScanedBillArticle = dtScanedBillArticleSpectrumMettler
                        If test Is Nothing Then Continue For
                    Catch ex As Exception
                        LogException(ex)
                    End Try

                Else
                    Dim dtScanedBillArticleMettler = objItemSch.GetScanedBillArticle(ScaleNo:=ScaleNo, BillNo:=BillNo, BillIntDate:=ScaleBillIntDate, MettlerConn:=MettlerConnString)
                    dtScanedBillArticle = dtScanedBillArticleMettler
                    Try
                        Dim test = (From BillItems In DtUnique.AsEnumerable() Select BillItems Join MettlerBillItems In dtScanedBillArticleMettler.AsEnumerable()
                                On BillItems.Field(Of String)("ArticleCode") Equals MettlerBillItems.Field(Of Int64)("LegacyArticleCode").ToString()
                                Select BillItems).FirstOrDefault()
                        dtScanedBillArticle = dtScanedBillArticleMettler
                        If test Is Nothing Then Continue For
                    Catch ex As Exception
                        LogException(ex)
                    End Try

                End If

                dtScanedBillArticle.AcceptChanges()
                TotalLineItems = dtScanedBillArticle.Rows.Count
                For Each dr As DataRow In DtScannedMettlerBillNew.Rows
                    dr("TotalLineItems") = TotalLineItems
                Next

                '---- Columns Heading 

                For BillRowIndex = 0 To dtScanedBillArticle.Rows.Count - 1 Step 1
                    Dim PrintDataRows() = DtUnique.Select("ArticleCode ='" & dtScanedBillArticle.Rows(BillRowIndex)("LegacyArticleCode") & "'")
                    If Not PrintDataRows Is Nothing And PrintDataRows.Count > 0 Then
                        '------ STEP-1:  collecting all Data With Format -----
                        For Each dr As DataRow In PrintDataRows
                            ItemCode = dr("ArticleCode").ToString()
                            Desc = IIf(DisplayArticleFullName, dr("ArticleFullName").ToString(), dr("DISCRIPTION").ToString())
                            Qty = dr("Quantity").ToString()
                            mettlerBillQty = dtScanedBillArticle.Rows(BillRowIndex)("Quantity")
                            'totalItems = Val(DtScannedMettlerBills.Rows(ScanBillIndex)("TotalLineItems")).ToString()
                            'ValueMultiplyFactor = Math.Round(Val(mettlerBillQty) / Val(Qty), 3)
                            totalItems = totalItems + 1 'Val(DtScannedMettlerBills.Rows(ScanBillIndex)("TotalLineItems")).ToString()
                            ValueMultiplyFactor = Val(mettlerBillQty) / Val(Qty)
                            'PricePerUnit = dr("SELLINGPRICE").ToString()
                            PricePerUnit = Math.Round(dr("SELLINGPRICE")).ToString
                            Disc = (Val(dr("TOTALDISCOUNT").ToString()) * ValueMultiplyFactor).ToString
                            ItemAmt = Math.Round(Val(dr("GrossAMT").ToString()) * ValueMultiplyFactor, 2).ToString
                            'BillTotalAmt = BillTotalAmt + Val(ItemAmt)
                            'ItemAmt = Math.Round(ItemAmt).ToString()
                            ItemAmt = FormatNumber(ItemAmt, 2).ToString()
                            If SpectrumAsMettler Then
                                ItemAmt = Math.Round(Val(dr("NETAMOUNT").ToString()) * ValueMultiplyFactor, 2).ToString
                            End If
                            ' BillTotalAmt = Math.Round(BillTotalAmt + Val(ItemAmt)).ToString
                            BillTotalAmt = FormatNumber(BillTotalAmt + Val(ItemAmt)).ToString
                            Call SetQtyFormat(mettlerBillQty)
                        Next
                    Else
                        Continue For
                    End If
                Next BillRowIndex
                Dim dtCounterSeparateData As New DataTable
                dtCounterSeparateData.Columns.Add("Header", System.Type.GetType("System.String"))
                dtCounterSeparateData.Columns.Add("Footer", System.Type.GetType("System.String"))
                dtCounterSeparateData.Columns.Add("ClientName", System.Type.GetType("System.String"))
                dtCounterSeparateData.Columns.Add("INR", System.Type.GetType("System.Single"))
                Dim drnews = dtCounterSeparateData.NewRow
                drnews("Header") = "Submit Counter Copy at Scale"
                drnews("Footer") = "BILL PAID"
                drnews("ClientName") = ClientName
                drnews("INR") = BillTotalAmt
                dtCounterSeparateData.Rows.Add(drnews)
                GenerateMettlerCounterCopyPrint(dtView, dtScanedBillArticle, DtScannedMettlerBillNew, dtCounterSeparateData, DayCloseReportPath:=DayCloseReportPath)
                dtFinalArticle = dtScanedBillArticle.Clone()

            Next ScanBillIndex
            Dim isPrintOtherItems As Boolean

            '  Dim dtScanedBillArticleSum = objItemSch.GetScanedCashMemoBillsArticle(BillNoList:=BillNoList.Substring(0, BillNoList.Length - 1), BillIntDate:=ScaleBillIntDate, MettlerConn:=MettlerConnString)
            Dim dtScanedBillArticleSum As DataTable
            If SpectrumAsMettler Then 'vipin
                dtScanedBillArticleSum = objItemSch.GetScanedBillArticleForSpectrumMettler(SpectrumMettlerBillNo)
            Else
                dtScanedBillArticleSum = objItemSch.GetScanedCashMemoBillsArticle(BillNoList:=BillNoList.Substring(0, BillNoList.Length - 1), BillIntDate:=ScaleBillIntDate, MettlerConn:=MettlerConnString)
            End If
            Dim DtScannedItemStruct As New DataTable
            Dim BillTotalAmtSepCopy As Double
            DtScannedItemStruct = DtScannedMettlerBills.Clone()
            totalItems = 0
            For Each dr As DataRow In DtUnique.Select("ScaleNo='0'")
                If Val(dr("ArticleCode")) > 0 Or dr("ArticleCode").ToString.Substring(0, 4) = "0CCE" Then 'vipin
                    Dim PrintDataRows() = dtScanedBillArticleSum.Select("LegacyArticleCode ='" & dr("ArticleCode") & "'")
                    Dim PrintDataRowsCondition2() = dtScanedBillArticleSum.Select("LegacyArticleCode ='" & dr("ArticleCode") & "' AND Quantity < " & dr("Quantity"))
                    If (PrintDataRows.Count = 0) Or (PrintDataRowsCondition2.Count > 0) Then
                        isPrintOtherItems = True
                        totalItems = totalItems + 1
                        ItemCode = dr("ArticleCode").ToString()
                        Desc = IIf(DisplayArticleFullName, dr("ArticleFullName").ToString(), dr("DISCRIPTION").ToString())
                        If PrintDataRows.Count > 0 Then
                            Qty = (Val(dr("Quantity")) - Val(PrintDataRows(0)("Quantity"))).ToString
                            'ValueMultiplyFactor = Math.Round((Val(dr("Quantity")) - Val(PrintDataRows(0)("Quantity"))) / dr("Quantity"), 3)
                            ValueMultiplyFactor = (Val(dr("Quantity")) - Val(PrintDataRows(0)("Quantity"))) / dr("Quantity")
                        Else
                            Qty = dr("Quantity").ToString
                            ValueMultiplyFactor = 1
                        End If
                        PricePerUnit = dr("SELLINGPRICE").ToString()
                        Disc = Math.Round(Val(dr("TOTALDISCOUNT").ToString()) * ValueMultiplyFactor, 2).ToString
                        ItemAmt = Math.Round(Val(dr("NETAMOUNT").ToString()) * ValueMultiplyFactor, 2).ToString
                        BillTotalAmtSepCopy = BillTotalAmtSepCopy + Val(ItemAmt)
                        Call SetQtyFormat(Qty)
                        If dr("UnitofMeasure").ToString().ToUpper = "KGS" Then
                            Qty = Qty & " Kg"
                        End If
                    End If
                End If
            Next
            '-------------------------------------------------------------------
            Dim dtCounterSeparateDataSep As New DataTable
            dtCounterSeparateDataSep.Columns.Add("Header", System.Type.GetType("System.String"))
            dtCounterSeparateDataSep.Columns.Add("Footer", System.Type.GetType("System.String"))
            dtCounterSeparateDataSep.Columns.Add("ClientName", System.Type.GetType("System.String"))
            dtCounterSeparateDataSep.Columns.Add("INR", System.Type.GetType("System.Single"))
            Dim drnew = dtCounterSeparateDataSep.NewRow
            drnew("Header") = "Submit Counter Copy at Scale"
            drnew("Footer") = "BILL PAID"
            drnew("ClientName") = ClientName
            drnew("INR") = BillTotalAmtSepCopy
            dtCounterSeparateDataSep.Rows.Add(drnew)
            Dim dtArticleData As New DataTable
            If DtUnique.Select("ScaleNo='0'").Count > 0 Then
                dtArticleData = DtUnique.Select("ScaleNo='0'").CopyToDataTable
                For Each drscale As DataRow In dtArticleData.Rows
                    Dim drNewFinalArticle = dtFinalArticle.NewRow
                    drNewFinalArticle("ScaleNo") = DBNull.Value
                    drNewFinalArticle("BillNo") = drscale("BillLineNo")
                    drNewFinalArticle("SequenceNo") = "0"
                    drNewFinalArticle("LegacyArticleCode") = drscale("ArticleCode")
                    drNewFinalArticle("c_article_name") = drscale("DISCRIPTION")
                    drNewFinalArticle("Quantity") = drscale("Quantity")
                    dtFinalArticle.Rows.Add(drNewFinalArticle)
                Next
                Dim dr = DtScannedItemStruct.NewRow
                dr("TotalLineItems") = totalItems
                DtScannedItemStruct.Rows.Add(dr)
                '  GenerateMettlerCounterCopyPrint(dtView, dtFinalArticle, DtScannedItemStruct, dtCounterSeparateDataSep, DayCloseReportPath:=DayCloseReportPath)
                ' If Not SpectrumAsMettler Then
                GenerateMettlerCounterCopyPrint(dtView, dtFinalArticle, DtScannedItemStruct, dtCounterSeparateDataSep, DayCloseReportPath:=DayCloseReportPath)
                'End If
            End If
        Catch ex As Exception
            errorMsg = ex.Message
        End Try
    End Sub

#Region "Mettler Print New code "
    Private Function CreateStream(ByVal name As String, ByVal fileNameExtension As String, ByVal encoding As Encoding, ByVal mimeType As String, ByVal willSeek As Boolean) As Stream
        Try
            Dim stream As Stream = New MemoryStream()
            m_streams.Add(stream)
            Return stream
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    ' Export the given report as an EMF (Enhanced Metafile) file.
    Private Sub Export(ByVal report As LocalReport)
        Try
        Dim deviceInfo As String = "<DeviceInfo>" & _
            "<OutputFormat>EMF</OutputFormat>" & _
            "<PageWidth>3.78in</PageWidth>" & _
            "<PageHeight>11in</PageHeight>" & _
            "<MarginTop>0in</MarginTop>" & _
            "<MarginLeft>0.25in</MarginLeft>" & _
            "<MarginRight>0.25in</MarginRight>" & _
            "<MarginBottom>0.25in</MarginBottom>" & _
            "</DeviceInfo>"
        Dim warnings As Warning()
        m_streams = New List(Of Stream)()
        report.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
        For Each stream As Stream In m_streams
            stream.Position = 0
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    'added by khusrao adil on 14-09-2017 for print fomrat 8
    Private Sub ExportA4Format8(ByVal report As LocalReport)
        Try
            Dim deviceInfo As String = "<DeviceInfo>" & _
           "<OutputFormat>EMF</OutputFormat>" & _
           "<PageWidth>8.27in</PageWidth>" & _
           "<PageHeight>11.69in</PageHeight>" & _
           "<MarginTop>0.50in</MarginTop>" & _
           "<MarginLeft>0.25in</MarginLeft>" & _
           "<MarginRight>0.25in</MarginRight>" & _
           "<MarginBottom>0.25in</MarginBottom>" & _
           "</DeviceInfo>"

            Dim warnings As Warning()
            m_streams = New List(Of Stream)()
            report.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
            For Each stream As Stream In m_streams
                stream.Position = 0
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub Print(Optional ByVal Islabelprint As Boolean = False)
        Try
            If m_streams Is Nothing OrElse m_streams.Count = 0 Then
                Throw New Exception("Error: no stream to print.")
            End If
            Dim printDoc As New PrintDocument()
            If Not printDoc.PrinterSettings.IsValid Then
                Throw New Exception("Error: cannot find the default printer.")
            Else
                AddHandler printDoc.PrintPage, AddressOf PrintPage
                m_currentPageIndex = 0
                If Islabelprint Then
                    printDoc.PrinterSettings.PrinterName = SetPrinterName(dtPrinterInfo1, "LblPrint", "LblPrint")
                Else
                    printDoc.PrinterSettings.PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                End If
                If Not PrinterName = Nothing Then
                    printDoc.Print()
                End If
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private m_streams As IList(Of Stream)
    Private m_currentPageIndex As Integer
    Private Sub PrintPage(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
        Try
            Dim pageImage As New Metafile(m_streams(m_currentPageIndex))

            'Dim pageImage1 As New Bitmap(Image.FromStream(m_streams(m_currentPageIndex)))
            ' Adjust rectangular area with printer margins.
            Dim adjustedRect As New Rectangle(ev.PageBounds.Left - CInt(ev.PageSettings.HardMarginX), _
                                              ev.PageBounds.Top - CInt(ev.PageSettings.HardMarginY), _
                                              ev.PageBounds.Width, _
                                              ev.PageBounds.Height)

            ' Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect)
            ' Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect)
            ' Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex += 1
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
#End Region

    Private Function GenerateMettlerCounterCopyPrint(ByRef dtView As DataTable, ByRef dtScanedBillArticle As DataTable, ByRef DtScannedMettlerBills As DataTable, ByRef DtCounterSeperateData As DataTable, ByVal DayCloseReportPath As String) As Boolean
        Try
            Dim path As String = ""
            Dim reportViewer2 As New LocalReport
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\CounterCopy.rdl")
            reportViewer2.ReportPath = appPath

            ' reportViewer2.ProcessingMode = ProcessingMode.Local
            'reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource1 As New ReportDataSource("DS_View", dtView)
            Dim DataSource2 As New ReportDataSource("DS_ScannedBills", DtScannedMettlerBills)
            Dim DataSource3 As New ReportDataSource("DS_CounterClientName", DtCounterSeperateData)
            Dim DataSource4 As New ReportDataSource("DS_dtScanedBillArticle", dtScanedBillArticle)
            reportViewer2.DataSources.Add(DataSource1)
            reportViewer2.DataSources.Add(DataSource2)
            reportViewer2.DataSources.Add(DataSource3)
            reportViewer2.DataSources.Add(DataSource4)
            reportViewer2.Refresh()
            If _PrintPreview = True Then
                Dim mybytes As [Byte]() = reportViewer2.Render("Pdf")
                Dim obj As New clsCommon
                path = DayCloseReportPath & "\CounterCopy_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss-'" & cntMettler & "'") & ".pdf"
                cntMettler += 1
                Using fs As FileStream = File.Create(path)
                    fs.Write(mybytes, 0, mybytes.Length)
                End Using
                Process.Start(path)
            Else
                Export(reportViewer2)
                Print()
                'Code For Print SO
                'PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                'Dim pdfdocument As New PdfDocument()
                'pdfdocument.LoadFromFile(path)
                'pdfdocument.PrinterName = PrinterName
                'pdfdocument.PrintDocument.PrinterSettings.Copies = 1
                'pdfdocument.PrintDocument.Print()
                'pdfdocument.Dispose()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Function GenerateFinalReceiptPrint(ByRef dtView As DataTable, ByRef dtUnique As DataTable, ByRef dtHeader As DataTable, ByRef DtFinalRFooter As DataTable, ByRef DtAmountTender As DataTable, ByVal DayCloseReportPath As String, Optional ByVal EvassPizzaChanges As Boolean = False, Optional ByVal PrintFormat As String = "", Optional ByVal dttaxDetail As DataTable = Nothing, Optional ByVal IsHsnAndTaxVisibleInPrintFormat6 As Boolean = False, Optional IsDeliveryNote As Boolean = False) As Boolean 'code added by irfan on 11/09/2017 visiblity of hsn and tax
        Try
            Dim path As String = ""
            Dim reportViewer2 As New LocalReport
            Dim SumScaleNo As String = dtUnique.Compute("SUM(ScaleNo)", "")
            Dim appPath As String
            If PrintFormat = 5 Then
                appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\BillReceipt.rdl")
            ElseIf PrintFormat <> 8 AndAlso PrintFormat <> 5 Then ''modified by khusrao adil for print format 8
                If SumScaleNo = 0 Then
                    ' appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\FinalReceipt_WithoutScale.rdl")
                    If dttaxDetail IsNot Nothing Then
                        If PrintFormat = 6 Then
                            If IsHsnAndTaxVisibleInPrintFormat6 = True Then                                                  'code added by irfan on 11/09/2017 visiblity of hsn and tax
                                appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\FinalReceipt_Format6ForHSNAndTax.rdl")
                            Else
                                appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\FinalReceipt_Format6.rdl")
                            End If
                        End If
                        If PrintFormat = 7 Then
                            appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\FinalReceipt_Format7.rdl")
                        End If

                        'code added by irfan on 24-08-2017 
                        If PrintFormat = 8 Then
                            appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\FinalReceiptAndSalesInvoiceA4_Format8.rdl")
                        End If
                    Else
                        appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\FinalReceipt_WithoutScale.rdl")
                    End If


                Else
                    ' appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\FinalReceipt.rdl")
                    If dttaxDetail IsNot Nothing Then
                        If PrintFormat = 6 Then
                            If IsHsnAndTaxVisibleInPrintFormat6 = True Then                                                'code added by irfan on 11/09/2017 visiblity of hsn and tax
                                appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\FinalReceipt_Format6ForHSNAndTax.rdl")
                            Else
                                appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\FinalReceipt_Format6.rdl")
                            End If
                        End If
                        If PrintFormat = 7 Then
                            appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\FinalReceipt_Format7.rdl")
                        End If
                    Else
                        appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\FinalReceipt.rdl")
                    End If
                End If
            ElseIf PrintFormat = 8 Then ''modified by khusrao adil for print format 8
                appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\FinalReceiptAndSalesInvoiceA4_Format8.rdl")
            End If
            reportViewer2.ReportPath = appPath
            'code added by irfan on 24-08-2017
            If PrintFormat = 8 Then ''modified by khusrao adil for print format 8
                Dim DataSource1 As New ReportDataSource("DsUnique", dtUnique)
                Dim DataSource2 As New ReportDataSource("DsHeader", dtHeader)
                reportViewer2.DataSources.Add(DataSource1)
                reportViewer2.DataSources.Add(DataSource2)
                reportViewer2.Refresh()
            Else

                'reportViewer2.ProcessingMode = ProcessingMode.Local
                'reportViewer2.LocalReport.DataSources.Clear()
                'If EvassPizzaChanges Then
                '    reportViewer2.SetParameters(New ReportParameter("BarCode", BarCodestring))
                'End If
                Dim DataSource1 As New ReportDataSource("DS_View", dtView)
                Dim DataSource2 As New ReportDataSource("Ds_Unique", dtUnique)
                Dim DataSource3 As New ReportDataSource("DS_Header", dtHeader)
                reportViewer2.DataSources.Add(DataSource1)
                reportViewer2.DataSources.Add(DataSource2)
                reportViewer2.DataSources.Add(DataSource3)
                If dttaxDetail IsNot Nothing Then
                    Dim DataSource4 As New ReportDataSource("DS_Tax", dttaxDetail)
                    reportViewer2.DataSources.Add(DataSource4)
                End If

                Dim DataSource5 As New ReportDataSource("DS_FinalRFooter", DtFinalRFooter)
                Dim DataSource6 As New ReportDataSource("DS_AmountTender", DtAmountTender)

                reportViewer2.DataSources.Add(DataSource5)
                reportViewer2.DataSources.Add(DataSource6)
                reportViewer2.Refresh()
            End If
            If _PrintPreview = True Then
                Dim mybytes As [Byte]() = reportViewer2.Render("Pdf")
                'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
                Dim obj As New clsCommon
                'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
                path = DayCloseReportPath & "\FinalReceipt_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                Using fs As FileStream = File.Create(path)
                    fs.Write(mybytes, 0, mybytes.Length)
                End Using
              
                Process.Start(path)
                If IsDeliveryNote = False AndAlso PrintFormat = 6 Then
                    SendInvoiceOnMail(path, dtView)
                    SendSMSForCM(dtView)
                End If
            Else
                If PrintFormatNo = 8 Then
                    Dim A4FormatFinalRecieptPath = DayCloseReportPath + "\A4FormatFinalReciept"
                    If Not System.IO.Directory.Exists(A4FormatFinalRecieptPath) Then
                        System.IO.Directory.CreateDirectory(A4FormatFinalRecieptPath)
                    End If
                    Dim mybytes As [Byte]() = reportViewer2.Render("Pdf")
                    'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
                    Dim obj As New clsCommon
                    'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
                    Dim strdatetime = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss")
                    path = A4FormatFinalRecieptPath & "\FinalReceipt_" & strdatetime & ".pdf"
                    'path = Application.StartupPath & "\FinalReceipt_" & strdatetime & ".pdf"
                    Using fs As FileStream = File.Create(path)
                        fs.Write(mybytes, 0, mybytes.Length)
                    End Using
                    PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                    Dim pdfdocument As New PdfDocument()
                    path = A4FormatFinalRecieptPath & "\FinalReceipt_" & strdatetime & ".pdf"
                    pdfdocument.LoadFromFile(path)
                    pdfdocument.PrinterName = PrinterName
                    pdfdocument.PrintDocument.PrinterSettings.Copies = 1
                    pdfdocument.PrintDocument.Print()
                    pdfdocument.Dispose()
                    DeleteDirectory(A4FormatFinalRecieptPath)

                Else
                    Dim mybytes As [Byte]() = reportViewer2.Render("Pdf") 'vipul
                    path = DayCloseReportPath & "\FinalReceipt_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                    Using fs As FileStream = File.Create(path)
                        fs.Write(mybytes, 0, mybytes.Length)
                    End Using
                    If IsInvoicePrintFlag = False Then
                        Export(reportViewer2)
                        Print()
                    Else
                        If IsInvoicePrintRequired Then
                            Export(reportViewer2)
                            Print()
                        End If
                    End If
                   
                
                    If IsDeliveryNote = False AndAlso PrintFormat = 6 Then
                        SendInvoiceOnMail(path, dtView)
                        SendSMSForCM(dtView)
                    End If
                End If

                'Code For Print SO
                'PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                'Dim pdfdocument As New PdfDocument()
                'pdfdocument.LoadFromFile(path)
                'pdfdocument.PrinterName = PrinterName
                'pdfdocument.PrintDocument.PrinterSettings.Copies = 1
                'pdfdocument.PrintDocument.Print()
                'pdfdocument.Dispose()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    'added by khusrao adil on 13-09-2017 for print format 8 A4 bill delete
    Private Sub DeleteDirectory(A4FormatFinalRecieptPath As String)
        Try
            If Directory.Exists(A4FormatFinalRecieptPath) Then
                'Delete all files from the Directory
                For Each filepath As String In Directory.GetFiles(A4FormatFinalRecieptPath)
                    File.Delete(filepath)
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function GeneratePCTaxInvoicePrintformat11(ByRef DSTaxInvoiceDetails As DataSet, ByVal BillNo As String, ByVal SiteCode As String, ByVal DayCloseReportPath As String) As Boolean
        Try

            Dim path As String = ""
            Dim reportViewer2 As New LocalReport
            Dim appPath As String

            appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\GSTCMPrintFormat11.rdl")
            reportViewer2.ReportPath = appPath
            reportViewer2.SetParameters(New ReportParameter("BillNo", BillNo))
            reportViewer2.SetParameters(New ReportParameter("SiteCode", SiteCode))

            Dim DataSource1 As New ReportDataSource("DSArticleDetails", DSTaxInvoiceDetails.Tables(0))
            Dim DataSource2 As New ReportDataSource("DSSubTotal", DSTaxInvoiceDetails.Tables(1))
            Dim DataSource3 As New ReportDataSource("DSTotal", DSTaxInvoiceDetails.Tables(2))
            Dim DataSource4 As New ReportDataSource("DSTender", DSTaxInvoiceDetails.Tables(3))
            Dim DataSource5 As New ReportDataSource("DSTAX", DSTaxInvoiceDetails.Tables(4))
            Dim DataSource6 As New ReportDataSource("DSClientDetails", DSTaxInvoiceDetails.Tables(5))
            Dim DataSource7 As New ReportDataSource("DSCustDetails", DSTaxInvoiceDetails.Tables(6))
            Dim DataSource8 As New ReportDataSource("dsFooterDetail", DSTaxInvoiceDetails.Tables(7))
            reportViewer2.DataSources.Add(DataSource1)
            reportViewer2.DataSources.Add(DataSource2)
            reportViewer2.DataSources.Add(DataSource3)
            reportViewer2.DataSources.Add(DataSource4)
            reportViewer2.DataSources.Add(DataSource5)
            reportViewer2.DataSources.Add(DataSource6)
            reportViewer2.DataSources.Add(DataSource7)
            reportViewer2.DataSources.Add(DataSource8)
            reportViewer2.Refresh()

            If _PrintPreview = True Then
                Dim mybytes As [Byte]() = reportViewer2.Render("Pdf")
                'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
                Dim obj As New clsCommon
                'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
                path = DayCloseReportPath & "\TaxInvoice_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                Using fs As FileStream = File.Create(path)
                    fs.Write(mybytes, 0, mybytes.Length)
                End Using
                Process.Start(path)
            Else
                Export(reportViewer2)
                Print()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'ashma
    Private Function GenerateCashMemoFinalReceiptPrint(ByRef dtView As DataTable, ByRef dtUnique As DataTable, ByRef dtHeader As DataTable, ByRef taxDetailsForBill As DataTable, ByRef DtFinalRFooter As DataTable, ByRef DtAmountTender As DataTable, ByVal DayCloseReportPath As String) As Boolean
        Try
            Dim path As String = ""
            Dim reportViewer2 As New LocalReport
            Dim SumScaleNo As String = dtUnique.Compute("SUM(ScaleNo)", "")
            Dim appPath As String
            If SumScaleNo = 0 Then
                'appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\CashMemoFinalReceipt_WithoutScale.rdl")
                appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\FinalReceipt_Format6.rdl")
            Else
                appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\FinalReceipt_Format6.rdl")
            End If

            reportViewer2.ReportPath = appPath

            'reportViewer2.ProcessingMode = ProcessingMode.Local
            'reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource1 As New ReportDataSource("DS_View", dtView)
            Dim DataSource2 As New ReportDataSource("Ds_Unique", dtUnique)
            Dim DataSource3 As New ReportDataSource("DS_Header", dtHeader)
            Dim DataSource4 As New ReportDataSource("DS_Tax", taxDetailsForBill)
            Dim DataSource5 As New ReportDataSource("DS_FinalRFooter", DtFinalRFooter)
            Dim DataSource6 As New ReportDataSource("DS_AmountTender", DtAmountTender)
            reportViewer2.DataSources.Add(DataSource1)
            reportViewer2.DataSources.Add(DataSource2)
            reportViewer2.DataSources.Add(DataSource3)
            reportViewer2.DataSources.Add(DataSource4)
            reportViewer2.DataSources.Add(DataSource5)
            reportViewer2.DataSources.Add(DataSource6)
            reportViewer2.Refresh()

            If _PrintPreview = True Then
                Dim mybytes As [Byte]() = reportViewer2.Render("Pdf")
                'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
                Dim obj As New clsCommon
                'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
                path = DayCloseReportPath & "\CashMemoFinalReceipt_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                Using fs As FileStream = File.Create(path)
                    fs.Write(mybytes, 0, mybytes.Length)
                End Using
                Process.Start(path)
            Else
                Export(reportViewer2)
                Print()
                'Code For Print SO
                'PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                'Dim pdfdocument As New PdfDocument()
                'pdfdocument.LoadFromFile(path)
                'pdfdocument.PrinterName = PrinterName
                'pdfdocument.PrintDocument.PrinterSettings.Copies = 1
                'pdfdocument.PrintDocument.Print()
                'pdfdocument.Dispose()
            End If
            'need to pass this parameter in method
            'hope india
            'Dim EmailCashmemo As Boolean = False
            'If EmailCashmemo = True Then
            '    Dim _user As String = dtView.Rows(0)("CREATEDBY")
            '    Dim _billNo As String = dtView.Rows(0)("BILLNO")
            '    Dim _siteCode As String = dtView.Rows(0)("SITECODE")
            '    Dim _billDate As Date = dtView.Rows(0)("BILLDATE")
            '    Dim obj As New clsDayClose
            '    Dim idList As String = obj.GetSiteMailId(_siteCode)
            '    'Dim id1 As String = id
            '    'If Not Idlist = "" Then
            '    '    id = id & "," & Idlist
            '    'End If
            '    SendBillToMail(path, idList, _billNo, _siteCode, _billDate)
            'End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Function GenerateCashMemoFinalReceiptPrint(ByRef dtView As DataTable, ByRef dtUnique As DataTable, ByRef dtHeader As DataTable, ByRef DtFinalRFooter As DataTable, ByRef DtAmountTender As DataTable, ByVal DayCloseReportPath As String) As Boolean
        Try
            Dim path As String = ""
            Dim reportViewer2 As New LocalReport
            Dim SumScaleNo As String = dtUnique.Compute("SUM(ScaleNo)", "")
            Dim appPath As String
            If SumScaleNo = 0 Then
                appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\CashMemoFinalReceipt_WithoutScale.rdl")
            Else
                appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\CashMemoFinalReceipt.rdl")
            End If

            reportViewer2.ReportPath = appPath

            'reportViewer2.ProcessingMode = ProcessingMode.Local
            'reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource1 As New ReportDataSource("DS_View", dtView)
            Dim DataSource2 As New ReportDataSource("Ds_Unique", dtUnique)
            Dim DataSource3 As New ReportDataSource("DS_Header", dtHeader)
            Dim DataSource4 As New ReportDataSource("DS_FinalRFooter", DtFinalRFooter)
            Dim DataSource5 As New ReportDataSource("DS_AmountTender", DtAmountTender)
            reportViewer2.DataSources.Add(DataSource1)
            reportViewer2.DataSources.Add(DataSource2)
            reportViewer2.DataSources.Add(DataSource3)
            reportViewer2.DataSources.Add(DataSource4)
            reportViewer2.DataSources.Add(DataSource5)
            reportViewer2.Refresh()

            If _PrintPreview = True Then
                Dim mybytes As [Byte]() = reportViewer2.Render("Pdf")
                'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
                Dim obj As New clsCommon
                'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
                path = DayCloseReportPath & "\CashMemoFinalReceipt_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                Using fs As FileStream = File.Create(path)
                    fs.Write(mybytes, 0, mybytes.Length)
                End Using
                Process.Start(path)
            Else
                Export(reportViewer2)
                Print()
                'Code For Print SO
                'PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                'Dim pdfdocument As New PdfDocument()
                'pdfdocument.LoadFromFile(path)
                'pdfdocument.PrinterName = PrinterName
                'pdfdocument.PrintDocument.PrinterSettings.Copies = 1
                'pdfdocument.PrintDocument.Print()
                'pdfdocument.Dispose()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function


    Private Function GenerateGuruKrupaTaxInvoicePrint(ByRef DSTaxInvoiceDetails As DataSet, ByVal BillNo As String, ByVal SiteCode As String, ByVal DayCloseReportPath As String, ByRef dtPrintCopy As DataTable) As Boolean
        Try

            Dim path As String = ""
            Dim reportViewer2 As New LocalReport
            Dim appPath As String

            appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\FinalReceipt9.rdl")
            reportViewer2.ReportPath = appPath
            reportViewer2.SetParameters(New ReportParameter("BillNo", BillNo))
            reportViewer2.SetParameters(New ReportParameter("SiteCode", SiteCode))

            Dim DataSource1 As New ReportDataSource("DSArticleDetails", DSTaxInvoiceDetails.Tables(0))
            Dim DataSource2 As New ReportDataSource("DSSubTotal", DSTaxInvoiceDetails.Tables(1))
            Dim DataSource3 As New ReportDataSource("DSTotal", DSTaxInvoiceDetails.Tables(2))
            Dim DataSource4 As New ReportDataSource("DSTender", DSTaxInvoiceDetails.Tables(3))
            Dim DataSource5 As New ReportDataSource("DSTAX", DSTaxInvoiceDetails.Tables(4))
            Dim DataSource6 As New ReportDataSource("DSClientDetails", DSTaxInvoiceDetails.Tables(5))
            Dim DataSource7 As New ReportDataSource("DSCustDetails", DSTaxInvoiceDetails.Tables(6))
            Dim DataSource8 As New ReportDataSource("DSFooter", dtPrintCopy)
            reportViewer2.DataSources.Add(DataSource1)
            reportViewer2.DataSources.Add(DataSource2)
            reportViewer2.DataSources.Add(DataSource3)
            reportViewer2.DataSources.Add(DataSource4)
            reportViewer2.DataSources.Add(DataSource5)
            reportViewer2.DataSources.Add(DataSource6)
            reportViewer2.DataSources.Add(DataSource7)
            reportViewer2.DataSources.Add(DataSource8)
            reportViewer2.Refresh()

            If _PrintPreview = True Then
                Dim mybytes As [Byte]() = reportViewer2.Render("Pdf")
                'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
                Dim obj As New clsCommon
                'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
                path = DayCloseReportPath & "\TaxInvoice_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                Using fs As FileStream = File.Create(path)
                    fs.Write(mybytes, 0, mybytes.Length)
                End Using
                Process.Start(path)
            Else
                Export(reportViewer2)
                Print()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Public Sub CashMemoPrintFormat(ByVal dayopenDate As Date, ByVal LangCode As String, ByVal Currency As String, ByVal Sitecode As String, ByVal Type As String, ByVal DuplicatePrinting As String, ByVal DeletedUserid As String, ByVal AuthorisedUser As String, ByRef errorMsg As String, Optional ByVal GiftMsg As String = "", Optional ByVal IsSavoy As Boolean = False, Optional ByVal ClientName As String = "", Optional ByVal NoOfCopies As Integer = 1)
        Try
            Dim strPrintMsg, sbDeliveryBillPrint As StringBuilder
            Dim strLineDetail As New StringBuilder
            Dim StrHeader, StrSubHeader, StrBody, StrCompanyInfo, StrTagLine, StrSubFooter, StrFooter, StrDuplicate, StrDeleteLine, StrCashMemoLine, StrCashierLine, StrSalesPersonLine As String
            Dim StrTokenNo, StrCustomerName As String
            Dim StrCMNo, StrCMDate, StrCMTime, StrCashier, StrSalesPerson As String
            Dim SiteLine, StrSiteTelline, StrAdrressLine, StrSiteReg1Line, StrSiteReg2Line As String
            Dim CLpLine, LineItemHeading, StrLineItem As String
            Dim CustomerNameLine, CLPPointsLine, CustmerPhoneNo As String
            Dim ItemCode, Desc, Qty, Rate, DiscAmt, DiscPer, TaxAmt, NetAmt, RateAfterDisc, GrossAmt As String
            Dim TotalQtyLine, TakeAwayQuantity, TotalGrossAmtLine, TotalDiscAmtLine, SubTotalLine, NetAmtLine, TotalTaxAmtLine As String
            Dim TotalFooterTaxDetail, FooterTaxLine As String
            Dim TenderDetails, TenderLine As String
            Dim TotalPromotionalMsg, PromoMsgLine, StrPrintHeaderLine, StrPrintFooterLine As String
            Dim TermsNcond As String
            Dim strLine As String
            Dim strDblLine As String
            Dim strHomeDilery As String
            Dim isTakeAwayQtyPrint As Boolean = False

            errorMsg = ""
            Dim dtView As New DataTable
            Dim obj As New SpectrumBL.clsCashMemo()
            dtView = obj.GetCashMemo(CashMemoNo, Sitecode, LangCode)

            Dim dvItemDetail As New DataView(dtView, "", "", DataViewRowState.CurrentRows)
            'Dim DtUnique As DataTable = dvItemDetail.ToTable(True, "ArticleCode", "DISCRIPTION", "QUANTITY", "SELLINGPRICE", "GROSSAMT", "TOTALDISCOUNT", "TOTALDISCPERCENTAGE", "NETAMOUNT", "TOTALTAXAMOUNT")
            Dim DtUnique As DataTable = obj.GetBillDetailsDataForPrint(CashMemoNo, Sitecode, LangCode)
            Dim Dtcombo As DataTable = obj.GetComboDetailsDataForPrint(CashMemoNo, Sitecode, LangCode)
            If DtUnique Is Nothing Then Exit Sub

            '----- Code Added By Mahesh - Delivery Person and customer Sales Type value pick up from Database 
            If Not IsDBNull(dtView.Rows(0)("ServiceTaxAmount")) AndAlso Val(dtView.Rows(0)("ServiceTaxAmount")) > 0 Then
                CustomerSaleType = GetCustomerSaleType(dtView.Rows(0)("ServiceTaxAmount"))
            Else
                CustomerSaleType = ""
            End If
            If Not IsDBNull(dtView.Rows(0)("DeliveryPersonID")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("DeliveryPersonID")) Then
                DeliveryPersonName = dtView.Rows(0)("DeliveryPersonID").ToString()
            End If

            Dim TotaltakeAwayQty = DtUnique.Compute("SUM(TakeAwayQuantity)", "").ToString()
            If Val(TotaltakeAwayQty) > 0 Then
                isTakeAwayQtyPrint = True
            End If

            Dim StrSubHeader1, StrSubFooter1, strWelcomeMsg, strTaxInformation, strPromotionMsg As New StringBuilder
            PrinttHeaderAndFooter(StrSubHeader1, StrSubFooter1, strWelcomeMsg, strPromotionMsg, strTaxInformation, "CMInvc", _printType)

            If StrSubFooter1 IsNot Nothing AndAlso Not String.IsNullOrEmpty(StrSubFooter1.ToString()) Then
                StrTagLine = StrSubFooter1.ToString()
            End If

            If Not dtView Is Nothing Then
                strLine = "-".PadRight(LineLength, "-") & vbCrLf
                strDblLine = "=".PadRight(LineLength, "=") & vbCrLf
                If DuplicatePrinting <> String.Empty Then
                    StrDuplicate = DuplicatePrinting
                End If
                'StrDeleteLine = "Cash Memo Status: Deleted"


                'Rakesh-12.09.2013:Issue-7750
                If Not StrSubHeader1 Is Nothing AndAlso StrSubHeader1.ToString().Length > 0 Then
                    ' StrSubHeader = strLine & SplitString(StrSubHeader1.ToString(), LineLength).ToString().Trim & vbCrLf
                    'If _printType <> "L40" Then
                    StrSubHeader = strLine & SplitString(StrSubHeader1.ToString(), LineLength).ToString().Trim & vbCrLf
                    'End If
                End If

                If DuplicatePrinting <> String.Empty Then
                    StrHeader = "<B>" & StrDuplicate.PadLeft(((LineLength - StrDuplicate.Length) / 2) + StrDuplicate.Length, " ") & "</B>" & vbCrLf
                End If

                If Type = "DCM" Then
                    StrDeleteLine = getValueByKey("CLSCMP001")
                    StrHeader = StrDeleteLine & vbCrLf
                    'Request ID: <Update / Delete Request ID>
                    'Authorized By : <User Name >       Deleted at Time: <CM Time>
                    'StrHeader = StrHeader & "Request ID    :" & DeletedUserid & vbCrLf
                    'StrHeader = StrHeader & "Authorized By :" & AuthorisedUser & vbCrLf
                    StrHeader = StrHeader & getValueByKey("CLSCMP002") & DeletedUserid & vbCrLf
                    StrHeader = StrHeader & getValueByKey("CLSCMP003") & AuthorisedUser & vbCrLf
                End If
                Dim SiteName, Site As String
                Dim TS_SiteName As StringBuilder
                'SiteName = "Store Name: " & dtView.Rows(0)("SITEOFFICIALNAME").ToString()
                'Site = "Code: " & dtView.Rows(0)("SITECODE").ToString()
                'StrSiteTelline = "Tel: " & dtView.Rows(0)("TELNO").ToString()
                'StrAdrressLine = "Add: " & dtView.Rows(0)("ADDRESS").ToString()

                SiteName = getValueByKey("CLSCMP004") & " " & dtView.Rows(0)("SITEOFFICIALNAME").ToString()
                SiteName = dtView.Rows(0)("SITEOFFICIALNAME").ToString()
                TS_SiteName = SplitString(SiteName)
                SiteName = TS_SiteName.ToString
                ' SiteName = SiteName.PadLeft(LineLength / 2 + (SiteName.Length / 2))
                Site = vbCrLf & getValueByKey("CLSCMP005") & " " & dtView.Rows(0)("SITECODE").ToString()
                'SiteLine = SiteName & Site
                ' SiteLine = SiteName
                'SiteLine = SiteName.PadRight(LineLength - Site.Length) & Site

                Dim arrClientname = ClientName.Split(" ").ToArray()

                For i = 0 To arrClientname.Length - 1
                    If SiteName.ToUpper.Contains(arrClientname(i).ToString.ToUpper) Then
                        SiteName = SiteName.ToUpper.Replace(arrClientname(i).ToString.ToUpper.Trim, "").Trim
                    End If
                Next
                SiteName = SiteName.PadLeft((Val(LineLength - SiteName.Length) + SiteName.Length) / 2)
                '------------------
                Site = vbCrLf & getValueByKey("CLSCMP005") & " " & dtView.Rows(0)("SITECODE").ToString()
                'SiteLine = SiteName & Site
                SiteLine = SiteName.Trim.PadLeft((Val(45 - SiteName.Length) + SiteName.Length) / 2)



                ' SiteLine = SiteName

                StrSiteTelline = getValueByKey("CLSCMP006") & " " & dtView.Rows(0)("TELNO").ToString()
                StrAdrressLine = getValueByKey("CLSCMP007") & " " & dtView.Rows(0)("ADDRESS").ToString()
                StrAdrressLine = SplitString(StrAdrressLine, LineLength).ToString()
                'StrSiteReg1Line = "LST NO: " & dtView.Rows(0)("LOCALSALESTAXNO").ToString() & Space(2) & "LST Date: " & dtView.Rows(0)("LOCALSALESTAXDATE").ToString()
                'StrSiteReg2Line = "CST NO: " & dtView.Rows(0)("CENTRALSALESTAXNO").ToString() & Space(2) & "CST Date: " & dtView.Rows(0)("CENTRALSALESTAXDATE").ToString()

                StrHeader &= SiteLine & vbCrLf
                StrHeader = StrHeader & StrAdrressLine.Trim & vbCrLf
                StrHeader = StrHeader & StrSiteTelline & vbCrLf
                'StrHeader = StrHeader & strLine & vbCrLf
                'StrSubHeader = StrSubHeader & StrSiteReg1Line & vbCrLf
                'StrSubHeader = StrSubHeader & StrSiteReg2Line & vbCrLf

                'Dim kotDataLine As String = String.Empty
                '****Added by Rahul 30 nov 2009 night(Rashid) . start 
                If Type = "DCM" Then
                    'StrCMNo = "Void Cash Memo:" & dtView.Rows(0)("Billno").ToString()
                    StrCMNo = getValueByKey("CLSCMP008") & dtView.Rows(0)("Billno").ToString()
                Else
                    'StrCMNo = "Cash Memo:" & dtView.Rows(0)("Billno").ToString()
                    StrCMNo = getValueByKey("CLSCMP009") & dtView.Rows(0)("Billno").ToString()
                End If
                '******End 

                'StrCMDate = "Date:" & Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())
                'StrCMDate = Format(dtView.Rows(0)("BillTime"), clsCommon.GetSystemDateFormat())
                StrCMDate = dayopenDate.ToShortDateString()
                strDayOpenDate = dayopenDate.ToShortDateString()
                If DuplicatePrinting <> String.Empty Then
                    StrCMDate = Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())
                End If
                StrTokenNo = dtView.Rows(0)("Billno").ToString().Trim.Substring(dtView.Rows(0)("Billno").ToString().Trim.Length - 2, 2)

                Select Case PrintFormatNo
                    Case 1
                        StrTokenNo = "<B>" & StrTokenNo & "</B>"
                    Case 2

                    Case Else

                End Select


                StrCashMemoLine = StrCMNo.PadRight(LineLength - 1 - StrCMDate.Length) & StrCMDate & vbCrLf
                'StrCashier = "Cashier:" & dtView.Rows(0)("Createdby").ToString()
                'StrCMTime = "Time:" & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")

                'StrCashier = getValueByKey("CLSPV006") & dtView.Rows(0)("Createdby").ToString()
                StrCashier = getValueByKey("CLSPV006") & LoginUserName
                StrCMTime = getValueByKey("CLSCMP032") & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")
                StrCashierLine = StrCashier.PadRight(LineLength - 1 - StrCMTime.Length) & StrCMTime & vbCrLf
                'StrSalesPerson = "Sales Person:" & dtView.Rows(0)("SALESEXECUTIVECODE").ToString()
                StrSalesPerson = getValueByKey("CLSCMP010") & dtView.Rows(0)("SALESEXECUTIVECODE").ToString()

                If _SalesPerApplicable = True Then
                    StrSalesPersonLine = StrSalesPerson & vbCrLf
                End If

                If (dtView.Rows.Count() > 0 And String.IsNullOrEmpty(DeliveryPersonName)) Then
                    If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                        ' strPrintMsg.Append(StrTokenNo)
                        StrTokenNo = "Token No. " & StrTokenNo
                        If Not AllowBillingOnlyAfterSelectionOfSalesType Then
                            '   strPrintMsg.Append(CustomerSaleType)
                            CustomerSaleType = String.Empty
                        End If
                        Dim StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - (CustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "")).Length) & CustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "") & vbCrLf
                        StrSubHeader = StrSubHeader & strLine
                        StrSubHeader = StrSubHeader & StrTokenSalesTypeLine
                    Else
                        If AllowBillingOnlyAfterSelectionOfSalesType Then
                            StrSubHeader = StrSubHeader & CustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "") & vbCrLf
                        End If
                    End If
                Else
                    If AllowBillingOnlyAfterSelectionOfSalesType Then
                        StrSubHeader = StrSubHeader & CustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "") + vbCrLf
                    End If
                End If
                StrSubHeader = StrSubHeader & strLine
                StrSubHeader = StrSubHeader & StrCashMemoLine
                StrSubHeader = StrSubHeader & StrCashierLine

                If (Not String.IsNullOrEmpty(dtView.Rows(0)("SALESEXECUTIVECODE").ToString())) Then
                    StrSubHeader = StrSubHeader & StrSalesPersonLine
                    'StrSubHeader = StrSubHeader & strLine
                End If

                Dim CLPaddress As String = ""
                Dim CLPBalancePoints As String
                If dtView.Rows(0)("CLPNO").ToString() <> String.Empty Then
                    'CLpLine = "CLP Card No:" & dtView.Rows(0)("CLPNO").ToString()
                    'CustomerNameLine = "Customer Name: " & dtView.Rows(0)("CLPNAME").ToString()
                    'CLPPointsLine = "CLP Points Earned: " & FormatNumber(dtView.Rows(0)("CLPPoints").ToString())

                    Select Case PrintFormatNo
                        Case 1
                            CLpLine = getValueByKey("CLSCMP011") & dtView.Rows(0)("CLPNO").ToString()
                        Case 2

                        Case Else

                    End Select

                    CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CLPNAME").ToString()
                    CLPPointsLine = getValueByKey("CLSCMP013") & FormatNumber(Val(dtView.Rows(0)("CLPPoints").ToString()))

                    If (dtView.Rows(0)("RedemptionPoints") IsNot DBNull.Value AndAlso dtView.Rows(0)("RedemptionPoints") > 0) Then
                        CLPPointsLine += vbCrLf + getValueByKey("CLSCMP034") & FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()))
                    End If

                    CLPaddress = dtView.Rows(0)("CLPAddress").ToString()
                    If CLPaddress <> String.Empty Then
                        CLPaddress = getValueByKey("CLSCMP014") & dtView.Rows(0)("CLPAddress").ToString()
                    End If
                    CLPBalancePoints = String.Empty
                    CLPBalancePoints = getValueByKey("CLSCMP015")
                    Dim dblCLPPoints As Double

                    dblCLPPoints = Val(dtView.Rows(0)("BalancePoints").ToString())
                    'For Each drRow As DataRow In dtView.Select("Tendertypecode = 'CLPPoint'")
                    '    dblCLPPoints -= drRow("RedemptionPoints")
                    'Next

                    Dim RedemptionPoints As DataRow = dtView.Select("Tendertypecode = 'CLPPoint'").FirstOrDefault()
                    If (RedemptionPoints IsNot Nothing AndAlso RedemptionPoints("RedemptionPoints") IsNot DBNull.Value) Then
                        dblCLPPoints -= Val(RedemptionPoints("RedemptionPoints").ToString())
                    End If


                    CLPBalancePoints = CLPBalancePoints & FormatNumber(dblCLPPoints.ToString())
                    If CLPaddress <> String.Empty Then
                        CLPaddress = SplitString(CLPaddress, LineLength).ToString()
                    End If
                    If CLpLine <> Nothing AndAlso CLpLine.ToString().Length > 0 Then
                        StrSubFooter = StrSubFooter & CLpLine & vbCrLf
                    End If
                    If CustomerSaleType = "Home Delivery" Then
                        CustmerPhoneNo = getValueByKey("RP187") & " " & dtView.Rows(0)("Mobileno").ToString()
                    Else
                        Dim tempMobileno = dtView.Rows(0)("Mobileno").ToString()
                        If dtView.Rows(0)("Mobileno").ToString().Length > 4 Then
                            Dim mobileLast4Digit = tempMobileno.Substring(tempMobileno.Length - 4, 4)
                            Dim mobilemolength = tempMobileno.Length
                            tempMobileno = mobileLast4Digit.PadLeft(tempMobileno.Length, "X")
                        End If
                        CustmerPhoneNo = getValueByKey("RP187") & " " & tempMobileno
                    End If

                    'StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
                    StrSubFooter = StrSubFooter & SplitString(CustomerNameLine).ToString() + vbCrLf & vbCrLf
                    StrSubFooter = StrSubFooter & CustmerPhoneNo & vbCrLf
                    StrSubFooter = StrSubFooter & CLPBalancePoints & vbCrLf

                    StrSubFooter = StrSubFooter & CLPPointsLine & vbCrLf
                    'StrSubFooter = StrSubFooter & CLPaddress & vbCrLf '& vbCrLf & vbCrLf

                ElseIf dtView.Columns.Contains("CustomerNo") AndAlso dtView.Rows(0)("CustomerNo").ToString() <> String.Empty Then
                    Select Case PrintFormatNo
                        Case 1
                            CLpLine = getValueByKey("CLSCMP038") & dtView.Rows(0)("CustomerNo").ToString()
                        Case 2

                        Case Else

                    End Select
                    CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CustomerName").ToString()
                    If CLpLine <> Nothing AndAlso CLpLine.ToString().Length > 0 Then
                        StrSubFooter = StrSubFooter & CLpLine & vbCrLf
                    End If
                    StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
                End If

                If Type = "CMWAmt" Then
                    'LineItemHeading = "Item Code" & Space(17) & "Item Name" & Space(1) & "Qty " & vbCrLf
                    ' LineItemHeading = getValueByKey("CLSPSO020")  & vbCrLf & getValueByKey("CLSCMP016") & Space(1) & getValueByKey("CLSPSO022") & " " & vbCrLf
                    'to resolve Issue 448 
                    LineItemHeading = getValueByKey("CLSPSO020") & Space(17) & vbCrLf & getValueByKey("CLSCMP016") & Space(20) & getValueByKey("CLSPSO022") & " " & vbCrLf
                Else
                    'LineItemHeading = "Item Code" & Space(17) & "Item Name" & Space(5) & vbCrLf
                    'to resolve Issue 448 
                    'LineItemHeading = "  Item Code" & vbCrLf & "  Item Name" & vbCrLf
                    'LineItemHeading = LineItemHeading & Space(3) & "Qty " & Space(2) & "Price" & Space(4) & "Disc." & Space(4) & "Tax" & Space(5) & "Net" & vbCrLf

                    'LineItemHeading = "  " & getValueByKey("CLSPSO020") & vbCrLf & "  " & getValueByKey("CLSCMP016") & vbCrLf
                    'LineItemHeading = LineItemHeading & Space(3) & getValueByKey("CLSPSO022") & " " & Space(2) & getValueByKey("CLSPSO023") & Space(4) & getValueByKey("CLSPSO024") & Space(4) & getValueByKey("CLSPSO025") & Space(5) & getValueByKey("CLSPSO026") & vbCrLf
                    LineItemHeading = getValueByKey("CLSCMP016") & Space(13) & getValueByKey("CLSPSO022") & " " & Space(5)
                    Select Case PrintFormatNo
                        Case 1
                            LineItemHeading &= "Amt" & vbCrLf
                        Case 2

                        Case Else
                            LineItemHeading &= "Amt" & vbCrLf

                    End Select
                End If

                Dim TotalQty, TGrossAmt, TDiscAmt, TRateAfterDisc, TTaxtAmt As String
                Dim TNetAmt As String = 0.0
                Dim BillLineNO As String
                For Each dr As DataRow In DtUnique.Rows
                    ItemCode = dr("ArticleCode").ToString()

                    If (DisplayArticleFullName) Then
                        Desc = dr("ArticleFullName").ToString()
                    Else
                        Desc = dr("DISCRIPTION").ToString()
                    End If
                    BillLineNO = dr("BillLineNO").ToString()
                    If SpectrumCommon.ComboItemPrintingAllowed AndAlso Dtcombo IsNot Nothing AndAlso Dtcombo.Rows.Count > 0 Then
                        Dim drp() = Dtcombo.Select("ComboArticleCode='" & ItemCode & "' AND BillLineNO ='" & BillLineNO & "'")
                        If drp.Length > 0 Then
                            Dim articleDescriptionDictionary As New Dictionary(Of String, Integer)
                            For index = 0 To drp.Length - 1
                                If articleDescriptionDictionary.ContainsKey(drp(index)("ArticleName")) Then
                                    articleDescriptionDictionary(drp(index)("ArticleName")) = articleDescriptionDictionary(drp(index)("ArticleName")) + drp(index)("Quantity")
                                Else
                                    articleDescriptionDictionary.Add(drp(index)("ArticleName"), drp(index)("Quantity"))
                                End If
                            Next
                            Dim AddedDesc = GetMultilinedString(articleDescriptionDictionary)
                            Desc = Desc & vbCrLf & AddedDesc
                        End If
                    End If

                    Qty = dr("Quantity").ToString()
                    Rate = dr("SellingPrice").ToString()
                    RateAfterDisc = (dr("SellingPrice") - dr("TOTALDISCOUNT")).ToString()
                    If CDbl(clsCommon.CheckIfBlank(Rate)) < 0 Then
                        Rate = CDbl(clsCommon.CheckIfBlank(Rate)) * -1
                    End If
                    DiscAmt = dr("TOTALDISCOUNT").ToString()
                    DiscPer = dr("TOTALDISCPERCENTAGE").ToString()
                    NetAmt = dr("NetAmount").ToString()
                    GrossAmt = dr("GROSSAMT").ToString()
                    TaxAmt = dr("TOTALTAXAMOUNT").ToString()
                    'Dim i As Integer = 0
                    'For i = ItemCode.Length To 26
                    '    ItemCode = ItemCode & " "
                    'Next
                    'For i = Desc.Length To 14
                    '    Desc = Desc & " "
                    'Next

                    If (AllowDecimalQty) Then
                        If (WeightScaleEnabled) Then
                            Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 3)
                        Else
                            Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 2)
                        End If
                    Else
                        Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 0)
                    End If

                    'For i = Qty.Length To 4
                    '    Qty = " " & Qty
                    'Next
                    Rate = FormatNumber(CDbl(clsCommon.CheckIfBlank(Rate)), 2)

                    RateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(RateAfterDisc)), 2)
                    'Rate = FormatCurrency(Rate, 2)
                    'For i = Rate.Length To 8
                    '    Rate = " " & Rate
                    'Next
                    DiscAmt = FormatNumber(CDbl(IIf(DiscAmt <> String.Empty, DiscAmt, 0)), 2)
                    'For i = DiscAmt.Length To 8
                    '    DiscAmt = " " & DiscAmt
                    'Next
                    DiscPer = FormatNumber(CDbl(IIf(DiscPer <> String.Empty, DiscPer, 0)), 2)
                    DiscPer = DiscPer & "%"
                    'For i = DiscPer.Length To 8
                    '    DiscPer = " " & DiscPer
                    'Next
                    NetAmt = FormatNumber(CDbl(NetAmt), 2)
                    GrossAmt = FormatNumber(CDbl(GrossAmt), 2)
                    'For i = NetAmt.Length To 8
                    '    NetAmt = " " & NetAmt
                    'Next
                    TaxAmt = FormatNumber(CDbl(IIf(TaxAmt <> String.Empty, TaxAmt, 0)), 2)
                    'For i = TaxAmt.Length To 7
                    '    TaxAmt = " " & TaxAmt
                    'Next

                    Dim TempNetAmt As String = "0"

                    If Type = "CMWAmt" Then
                        StrLineItem = ItemCode.PadRight(26) & vbCrLf & Desc.PadRight(35) & Qty & vbCrLf
                    Else
                        If 28 - Desc.Length < Qty.Length Then
                            StrLineItem = Desc & vbCrLf
                            Select Case PrintFormatNo
                                Case 1
                                    StrLineItem = StrLineItem & Qty.PadLeft(28) & GrossAmt.PadLeft(11) & vbCrLf
                                Case 2

                                Case Else
                                    TempNetAmt = NetAmt
                                    If Val(NetAmt) > 0 AndAlso Val(TaxAmt) > 0 Then
                                        TempNetAmt = CDbl(GrossAmt) - CDbl(TaxAmt)
                                    End If
                                    StrLineItem = StrLineItem & Qty.PadLeft(28) & TempNetAmt.PadLeft(11) & vbCrLf
                            End Select
                        Else
                            Select Case PrintFormatNo
                                Case 1
                                    StrLineItem = Desc & Qty.PadLeft(28 - Desc.Length) & GrossAmt.PadLeft(11) & vbCrLf
                                Case 2

                                Case Else
                                    TempNetAmt = NetAmt
                                    If Val(NetAmt) > 0 AndAlso Val(TaxAmt) > 0 Then
                                        TempNetAmt = CDbl(GrossAmt) - CDbl(TaxAmt)
                                    End If
                                    StrLineItem = Desc & Qty.PadLeft(28 - Desc.Length) & TempNetAmt.PadLeft(11) & vbCrLf
                            End Select
                        End If
                    End If
                    strLineDetail.Append(StrLineItem)

                    If CDbl(Qty.Trim) > 0 Then
                        TotalQty = CDbl(clsCommon.CheckIfBlank(TotalQty)) + CDbl(Qty.Trim)
                    End If
                    TDiscAmt = CDbl(clsCommon.CheckIfBlank(TDiscAmt)) + CDbl(DiscAmt.Trim)
                    TGrossAmt = CDbl(clsCommon.CheckIfBlank(TGrossAmt)) + CDbl(dr("GROSSAMT").ToString())
                    'TNetAmt = CDbl(clsCommon.CheckIfBlank(TNetAmt)) + CDbl(NetAmt.Trim)
                    'TNetAmt = Format(Math.Round(Convert.ToDecimal(CDbl(clsCommon.CheckIfBlank(TNetAmt)) + CDbl(NetAmt.Trim))), "0.00")
                    TNetAmt = Decimal.Add(FormatNumber(CDbl(NetAmt), 2).ToString(), TNetAmt)
                    TRateAfterDisc = CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)) + CDbl(RateAfterDisc.Trim)
                    TTaxtAmt = CDbl(clsCommon.CheckIfBlank(TTaxtAmt)) + CDbl(TaxAmt.Trim)
                Next

                TotalQty = FormatNumber(CDbl(clsCommon.CheckIfBlank(TotalQty)), 2)
                'TDiscAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TDiscAmt)), 2)
                TDiscAmt = FormatNumber(dtView.Rows(0)("Discount"), 2)
                TGrossAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TGrossAmt)), 2)
                'TNetAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TNetAmt)), 2)
                TNetAmt = FormatNumber(MyRound(CDbl(clsCommon.CheckIfBlank(TNetAmt)), RoundOff), 2)
                TRateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)), 2)
                TTaxtAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TTaxtAmt)), 2)
                StrBody = strLineDetail.ToString() & vbCrLf
                StrBody = StrBody & strLine

                'TotalQtyLine = "Total Qty" & Space(3) & ":" & TotalQty
                'TotalQtyLine = getValueByKey("CLSCMP017") & Space(3) & ":" & TotalQty
                'StrBody = StrBody & TotalQtyLine & vbCrLf
                Dim TSubTotalAmt As String
                If Type <> "CMWAmt" Then
                    If CDbl(clsCommon.CheckIfBlank(TDiscAmt)) > 0 Then
                        Dim TotalDisPercent As String = Math.Round((CDbl(clsCommon.CheckIfBlank(TDiscAmt)) / CDbl(clsCommon.CheckIfBlank(TGrossAmt))) * 100, 1).ToString()
                        TSubTotalAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TGrossAmt)) - CDbl(clsCommon.CheckIfBlank(TDiscAmt)))
                        TSubTotalAmt = TSubTotalAmt & " " & Currency
                        SubTotalLine = "Sub Total".PadRight(LineLength - 16) & ":" & TSubTotalAmt.PadLeft(14)
                        TDiscAmt = TDiscAmt & " " & Currency
                        Dim ObjclsCommon As New clsCommon
                        Dim offerno As String = ObjclsCommon.GetOffernoForRoundOff()
                        If DtUnique.Rows(0)("PromotionId").ToString().Contains(offerno) And offerno <> "" Then
                            TotalDiscAmtLine = "Round Off".PadRight(LineLength - 16) & ":" & TDiscAmt.PadLeft(14)
                        Else
                            TotalDiscAmtLine = String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).PadRight(LineLength - 16) & ":" & TDiscAmt.PadLeft(14)
                        End If
                        'TotalDiscAmtLine = String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).PadRight(LineLength - 16) & ":" & TDiscAmt.PadLeft(14)
                    End If
                    TGrossAmt = TGrossAmt & " " & Currency
                    TNetAmt = TNetAmt & " " & Currency
                    TRateAfterDisc = TRateAfterDisc & " " & Currency
                    TTaxtAmt = TTaxtAmt & " " & Currency

                    TotalGrossAmtLine = "Total Amt".PadRight(LineLength - 16) & ":" & TGrossAmt.PadLeft(14) & vbCrLf

                    NetAmtLine = getValueByKey("CLSCMP020").PadRight(LineLength - 16) & ":" & TNetAmt.PadLeft(14)
                    If _TaxDetail = True Then
                        Dim taxDetailsForBill As DataTable = obj.GetTaxDetailsForBillNo(Sitecode, dtView.Rows(0)("Billno").ToString())
                        If taxDetailsForBill IsNot Nothing AndAlso taxDetailsForBill.Rows.Count > 0 Then
                            For Each row As DataRow In taxDetailsForBill.Rows
                                'If CompositeTaxReqOnPrint = False AndAlso IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                                If IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                                Dim taxValue As String = row("TaxVAlue").ToString()
                                taxValue = FormatNumber(CDbl(clsCommon.CheckIfBlank(taxValue)), 2)
                                taxValue = taxValue & " " & Currency
                                FooterTaxLine = FooterTaxLine & row("TaxName").PadRight(LineLength - 16) & ":" & taxValue.PadLeft(14) & vbCrLf
                            Next
                        End If
                    End If
                    TotalFooterTaxDetail = FooterTaxLine
                    StrBody = StrBody & TotalGrossAmtLine & vbCrLf
                    If TotalDiscAmtLine IsNot Nothing Then
                        StrBody = StrBody & TotalDiscAmtLine & vbCrLf
                    End If
                    If SubTotalLine IsNot Nothing Then
                        StrBody = StrBody & strLine.Substring(30) & SubTotalLine & vbCrLf
                    End If

                    StrBody = StrBody & TotalFooterTaxDetail & strLine.Substring(30) & NetAmtLine & vbCrLf & strLine

                    TenderLine = getValueByKey("CLSCMP021") & vbCrLf
                    Dim dtTender As DataTable = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                    For Each drTender As DataRow In dtTender.Rows
                        If UCase(obj.GetDefaultConfigValue(Sitecode, "CVInfoReqOnPrint")) = "FALSE" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)") Then
                            Continue For
                        End If
                        Dim tender As String = FormatNumber(CDbl(clsCommon.CheckIfBlank(drTender("AMOUNTTENDERED").ToString()))) & " " & drTender("CurrencyCode").ToString() & vbCrLf
                        Dim tenderName As String
                        'Added by Rohit
                        If drTender("CardNO").ToString() <> "" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(I)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(R)") Then
                            tenderName = drTender("TENDERHEADNAME").ToString() & "(" & drTender("CardNO").ToString() & ") "
                        Else
                            tenderName = drTender("TENDERHEADNAME").ToString()
                        End If
                        'End editing
                        If Not drTender("AMOUNTINCURRENCY") Is DBNull.Value AndAlso drTender("AMOUNTINCURRENCY") <> 0 Then '0000404
                            tender = FormatNumber(CDbl(drTender("AMOUNTINCURRENCY").ToString()), 2) & " " & drTender("CurrencyCode").ToString() & vbCrLf
                        End If
                        'tender = Format(CDbl(tender), "0.00")

                        'TenderDetails = tenderName.PadRight(LineLength - 19) & tender.PadLeft(20) 
                        '--- Code Added By Mahesh changes by on MOD clp changes
                        If drTender("TENDERTYPECODE").ToString() = "CLPPoint" Then
                            tender = FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()))
                        End If

                        TenderDetails = String.Format("{0}{1}{2}", tenderName.Trim(), Space(LineLength - (tenderName.Trim().Length + tender.Trim().Length)), tender.Trim()) & vbCrLf
                        TenderLine = TenderLine & TenderDetails
                    Next
                    StrBody = StrBody & TenderLine

                    'Rakesh-13.09.2013:KSL CR-7860--> Display total discount amount 
                    If (_IsDisplayTotalSaving) Then
                        If (Not String.IsNullOrEmpty(TDiscAmt) AndAlso Not String.Equals(TDiscAmt, "0.00")) Then
                            Dim totalSavingAmount As String = String.Format(getValueByKey("CLSCMP033"), Currency, TDiscAmt.Replace(Currency, String.Empty).Trim)
                            StrBody = StrBody & vbCrLf & SplitString(totalSavingAmount).ToString().Trim & vbCrLf & strLine & vbCrLf
                        Else
                            StrBody = StrBody & vbCrLf
                        End If
                    Else
                        StrBody = StrBody & vbCrLf
                    End If

                End If

                'Company and Tin Info
                Dim NoOfReprints As String = IIf(IsDBNull(dtView.Rows(0)("NoofReprints")), String.Empty, dtView.Rows(0)("NoofReprints")).ToString()
                Dim CompanyName As String = dtView.Rows(0)("SITEOFFICIALNAME") 'obj.GetDefaultConfigValue(Sitecode, "CompanyName")

                StrCompanyInfo = String.Empty
                If Not String.IsNullOrEmpty(CompanyName) Then
                    StrCompanyInfo = CompanyName & vbCrLf
                End If

                Dim strtna = getValueByKey("cashmemoprint.tin") & obj.GetTinNumberForASite(Sitecode)
                strtna = SplitString(strtna, LineLength).ToString() & vbCrLf

                If Not String.IsNullOrEmpty(strtna) Then
                    StrCompanyInfo += strtna.TrimEnd() & vbCrLf
                End If

                If (Not String.IsNullOrEmpty(StrTagLine)) Then
                    StrCompanyInfo += StrTagLine.TrimEnd() & vbCrLf
                End If

                'Dim remarks As String = dtView.Rows(0)("Remark")
                If Not IsDBNull(dtView.Rows(0)("Remark")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("Remark")) Then
                    Dim remarks As String = dtView.Rows(0)("Remark")
                    remarks = SplitString(getValueByKey("cashmemoprint.discountremark") & remarks, LineLength).ToString()
                    StrCompanyInfo = StrCompanyInfo & remarks.TrimEnd() & vbCrLf
                End If

                If Not String.IsNullOrEmpty(NoOfReprints) Then
                    StrCompanyInfo = StrCompanyInfo & "Re-Print Number : " & NoOfReprints & vbCrLf
                    StrCompanyInfo = StrCompanyInfo & "Re-Print Date : " & DateTime.Now.ToShortDateString() & vbCrLf
                End If
                'Company and Tin Info End

                Dim dtPromo As DataTable = dvItemDetail.ToTable(True, "PROMOTIONALMSGID", "PROMOMESS")
                For Each drTerms As DataRow In dtPromo.Select("", "PROMOTIONALMSGID", DataViewRowState.CurrentRows)
                    If Not String.IsNullOrEmpty(drTerms("PROMOMESS").ToString()) Then
                        PromoMsgLine = PromoMsgLine & drTerms("PROMOMESS").ToString() & vbCrLf
                    End If
                Next

                TotalPromotionalMsg = PromoMsgLine
                If Not IsSavoy Then
                    Dim dtTerms As DataTable = dvItemDetail.ToTable(True, "TNCSRNO", "Terms")
                    For Each drTerms As DataRow In dtTerms.Select("", "TNCSRNO", DataViewRowState.CurrentRows)
                        If Not String.IsNullOrEmpty(drTerms("Terms").ToString()) Then
                            TermsNcond = TermsNcond & drTerms("Terms").ToString() & vbCrLf
                        End If
                    Next
                End If

                'If dtView.Rows.Count() > 0 Then
                '    If dtView.Rows(0)("HDName").ToString() <> String.Empty Then
                '        strHomeDilery = "Delivery Date:-" & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & "Name:-" & dtView.Rows(0)("HDName").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & "Address:-" & dtView.Rows(0)("HDAddress").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & "TelNo:-" & dtView.Rows(0)("HDTelNo").ToString() & vbCrLf
                '        strHomeDilery = strHomeDilery & "Email:-" & dtView.Rows(0)("HDEmail").ToString() & vbCrLf
                '    End If
                'End If
                strPrintMsg = New StringBuilder()
                sbDeliveryBillPrint = New StringBuilder()

                If dtView.Rows.Count() > 0 Then
                    '---- Changes By Mahesh : Customer Name is Not Mandatory for Home Delivery 
                    'If dtView.Rows(0)("HDName").ToString() <> String.Empty Then
                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP022") & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf
                        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP023") & dtView.Rows(0)("HDName").ToString() & vbCrLf
                        strHomeDilery = strHomeDilery & SplitString(getValueByKey("CLSCMP024") & dtView.Rows(0)("HDAddress").ToString(), LineLength).ToString() & vbCrLf
                        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP025") & dtView.Rows(0)("HDTelNo").ToString() & vbCrLf
                        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP026") & dtView.Rows(0)("HDEmail").ToString() & vbCrLf

                    Else
                        If _CustomerNameRequiredInKOT AndAlso CustomerNameLine <> Nothing Then
                            StrCustomerName = CustomerNameLine.ToUpper.Replace("CUSTOMER", "").Trim()

                        End If
                        If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                            If Not AllowBillingOnlyAfterSelectionOfSalesType Then
                                CustomerSaleType = String.Empty
                            End If
                            Dim StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - CustomerSaleType.Length) & CustomerSaleType & vbCrLf & vbCrLf
                        End If
                    End If
                End If


                StrFooter = StrFooter & TotalPromotionalMsg '& vbCrLf
                StrFooter = StrFooter & TermsNcond '& vbCrLf


                strPrintMsg.Append(StrHeader)
                '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                sbDeliveryBillPrint.Append(StrHeader)
                'strPrintMsg.Append(strLine)


                If StrSubHeader IsNot Nothing AndAlso Not String.IsNullOrEmpty(StrSubHeader.ToString()) Then
                    strPrintMsg.Append(StrSubHeader)
                    '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                    sbDeliveryBillPrint.Append(StrSubHeader)
                End If

                If (Not String.IsNullOrEmpty(strWelcomeMsg.ToString())) Then
                    strPrintMsg.Append(strLineL40 + vbCrLf)
                    strPrintMsg.Append(strWelcomeMsg.ToString())
                    '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                    sbDeliveryBillPrint.Append(strLineL40 + vbCrLf)
                    sbDeliveryBillPrint.Append(strWelcomeMsg.ToString())
                End If

                '---- Added By Mahesh maintaining seperate DeliveryBillPrint

                sbDeliveryBillPrint.Append(strDblLine)
                strPrintMsg.Append(strDblLine)
                strPrintMsg.Append(LineItemHeading)
                strPrintMsg.Append(strDblLine)
                strPrintMsg.Append(StrBody)

                '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                sbDeliveryBillPrint.Append(LineItemHeading)
                sbDeliveryBillPrint.Append(strDblLine)
                sbDeliveryBillPrint.Append(StrBody)

                strPrintMsg.Append(StrCompanyInfo)
                strPrintMsg.Append(strLine)

                If strTaxInformation IsNot Nothing AndAlso Not String.IsNullOrEmpty(strTaxInformation.ToString()) Then
                    strPrintMsg.Append(strTaxInformation.ToString().TrimEnd())
                    strPrintMsg.Append(vbCrLf + strLineL40 + vbCrLf)
                End If

                If strPromotionMsg IsNot Nothing AndAlso Not String.IsNullOrEmpty(strPromotionMsg.ToString()) Then
                    strPrintMsg.Append(strPromotionMsg.ToString().TrimEnd())
                    strPrintMsg.Append(vbCrLf + strLineL40 + vbCrLf)
                End If

                If StrSubFooter IsNot Nothing AndAlso Not String.IsNullOrEmpty(StrSubFooter.ToString()) Then
                    strPrintMsg.Append(StrSubFooter.ToString())
                End If

                strPrintMsg.Append(StrFooter)

                If (Not String.IsNullOrEmpty(strHomeDilery)) Then
                    strPrintMsg.Append(strLineL40 & vbCrLf)
                    strPrintMsg.Append(strHomeDilery)

                    ' ---- Added By Mahesh maintaining seperate DeliveryBillPrint
                    '--- Changes By Mahesh Not required in HD Print
                    'sbDeliveryBillPrint.Append(vbCrLf)
                    'sbDeliveryBillPrint.Append(strHomeDilery)



                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        'sbDeliveryBillPrint.Append(strPrintMsg)
                        'sbDeliveryBillPrint.Append(strLineL40 & vbCrLf)
                        '--- Changes By Mahesh Not required in HD Print
                        'sbDeliveryBillPrint.Append(getValueByKey("CLSCMP035") & DeliveryPersonName & vbCrLf)
                    End If
                End If

                If GiftMsg <> String.Empty Then
                    GiftMsg = SplitString(GiftMsg).ToString()
                    strPrintMsg.Append(GiftMsg.TrimEnd() & vbCrLf)
                    strPrintMsg.Append(strLine)

                    sbDeliveryBillPrint.Append(GiftMsg.TrimEnd() & vbCrLf & strLine)
                End If

                PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")

                If PrinterName = Nothing Then
                    Exit Sub
                End If
                Dim msg As String = String.Empty
                If Not IsDBNull(dtView.Rows(0)("ServiceTaxAmount")) AndAlso Val(dtView.Rows(0)("ServiceTaxAmount")) > 0 Then
                    CustomerSaleType = GetCustomerSaleType(Convert.ToInt16(dtView.Rows(0)("ServiceTaxAmount")))
                Else
                    CustomerSaleType = ""
                End If
                If _PrintPreview = True Then

                    If (KOTBillPrintingRequired) Then
                        If IsDintInEnabled AndAlso CustomerSaleType = "Dine In" Then
                        Else
                            Call CashMemoKOTPrintBasedOnPrintFormatNo(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, dayopenDate)
                        End If
                    End If
                    'PrinterName = "HP LaserJet P3005 PCL6"
                    PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                    msg = fnPrint(strPrintMsg.ToString(), "PRV")

                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        msg = fnPrint(sbDeliveryBillPrint.ToString(), "PRV")
                    End If
                Else
                    If (KOTBillPrintingRequired) Then
                        If IsDintInEnabled AndAlso CustomerSaleType = "Dine In" Then
                        Else
                            Call CashMemoKOTPrintBasedOnPrintFormatNo(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, dayopenDate)
                        End If
                    End If
                    strPrintMsg.Append(vbCrLf & vbCrLf & vbCrLf & vbCrLf)
                    PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                    msg = fnPrint(strPrintMsg.ToString(), "PRN", NoOfCopies:=NoOfCopies)

                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)

                        msg = fnPrint(sbDeliveryBillPrint.ToString(), "PRN")
                    End If

                End If
                If Not String.IsNullOrEmpty(msg) Then
                    errorMsg = msg
                End If
                'Added for fiscal printer
                Try
                    clsFiscalPrinting.fnFiscalPrint(strPrintMsg.ToString())
                Catch ex As Exception

                End Try
                'Added for fiscal printer

            Else
                errorMsg = getValueByKey("CLSCMP031")
            End If
        Catch ex As Exception
            errorMsg = ex.Message
        End Try
    End Sub
    Dim DtScannedMettlerBills As DataTable = Nothing
    Public Sub CashMemoKOTPrintBasedOnPrintFormatNo(ByVal Type As String, ByRef errorMsg As String, ByRef dtView As DataTable, ByVal DtUnique As DataTable, ByVal Dtcombo As DataTable, CustomerNameLine As String, Optional ByVal dayopendate As Date = Nothing, Optional ByVal HierarachyWisePrintFlag As Boolean = False, Optional ByVal IsArticleWiseKot As Boolean = False, Optional ByVal IsCounterCopy As Boolean = False)
        Try
            Select Case KOTPrintFormatNo
                Case 0
                    Call CashMemoKOTPrint(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, HierarachyWisePrintFlag)
                Case 1
                    '------ GET ALL METTLER BILLS  IF THERE IS ANY METTLER BILL DATA THEN PROCEED ELSE FOLLOW OLD LOGIC ------
                    Dim obj As New SpectrumBL.clsCashMemo()
                    DtScannedMettlerBills = obj.GetMettlerBillDetailsDataForPrint(CashMemoNo, SiteCode)
                    If (Not DtScannedMettlerBills Is Nothing) AndAlso (DtScannedMettlerBills.Rows.Count > 0) Then
                        If IsCounterCopy Then
                            Call CashMemoKOTPrintMettlerBillWise(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, dayopendate)
                        End If
                    Else
                        If IsArticleWiseKot Then
                            Call CashMemoKOTPrint(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, HierarachyWisePrintFlag)
                        End If
                    End If
                Case 2

                Case Else
                    Call CashMemoKOTPrint(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, HierarachyWisePrintFlag)
            End Select
        Catch ex As Exception
            errorMsg = ex.Message
        End Try
    End Sub
    Dim cntMettler As Integer = 0


    'ashma
    Public Sub CashMemoKOTPrintBasedOnPrintFormatNoSSRS(ByVal Type As String, ByRef errorMsg As String, ByRef dtView As DataTable, ByVal DtUnique As DataTable, ByRef taxDetailsForBill As DataTable, ByVal Dtcombo As DataTable, CustomerNameLine As String, Optional ByVal dayopendate As Date = Nothing, Optional ByVal HierarachyWisePrintFlag As Boolean = False, Optional ByVal IsArticleWiseKot As Boolean = False, Optional ByVal IsCounterCopy As Boolean = False, Optional ByVal ClientName As String = "", Optional ByVal DayCloseReportPath As String = "")
        Try
            Select Case KOTPrintFormatNo
                Case 0
                    Call CashMemoKOTPrint(Type, errorMsg, dtView, DtUnique, taxDetailsForBill, Dtcombo, CustomerNameLine, HierarachyWisePrintFlag)
                Case 1
                    '------ GET ALL METTLER BILLS  IF THERE IS ANY METTLER BILL DATA THEN PROCEED ELSE FOLLOW OLD LOGIC ------
                    Dim obj As New SpectrumBL.clsCashMemo() 'vipin to changes table  for Mettler SpectrumASMettler
                    ' DtScannedMettlerBills = obj.GetMettlerBillDetailsDataForPrint(CashMemoNo, SiteCode)
                    If SpectrumAsMettler Then
                        DtScannedMettlerBills = obj.GetSpectrumMettlerBillDetailsDataForPrint(CashMemoNo, SiteCode)
                    Else
                        DtScannedMettlerBills = obj.GetMettlerBillDetailsDataForPrint(CashMemoNo, SiteCode)
                    End If

                    If (Not DtScannedMettlerBills Is Nothing) AndAlso (DtScannedMettlerBills.Rows.Count > 0) Then
                        If IsCounterCopy Then
                            cntMettler = 1
                            Call CashMemoKOTPrintMettlerBillWiseSSRS(Type, errorMsg, dtView, DtUnique, taxDetailsForBill, Dtcombo, CustomerNameLine, dayopendate, ClientName, DayCloseReportPath)
                        End If
                    Else
                        If IsArticleWiseKot Then
                            Call CashMemoKOTPrint(Type, errorMsg, dtView, DtUnique, taxDetailsForBill, Dtcombo, CustomerNameLine, HierarachyWisePrintFlag)
                        End If
                    End If
                Case 2

                Case Else
                    Call CashMemoKOTPrint(Type, errorMsg, dtView, DtUnique, taxDetailsForBill, Dtcombo, CustomerNameLine, HierarachyWisePrintFlag)
            End Select
        Catch ex As Exception
            errorMsg = ex.Message
        End Try
    End Sub
    Public Sub CashMemoKOTPrintBasedOnPrintFormatNoSSRS(ByVal Type As String, ByRef errorMsg As String, ByRef dtView As DataTable, ByVal DtUnique As DataTable, ByVal Dtcombo As DataTable, CustomerNameLine As String, Optional ByVal dayopendate As Date = Nothing, Optional ByVal HierarachyWisePrintFlag As Boolean = False, Optional ByVal IsArticleWiseKot As Boolean = False, Optional ByVal IsCounterCopy As Boolean = False, Optional ByVal ClientName As String = "", Optional ByVal DayCloseReportPath As String = "", Optional ByVal CustomerComments As String = "", Optional ByVal EvassPizza As Boolean = False)
        Try
            Select Case KOTPrintFormatNo
                Case 0
                    Call CashMemoKOTPrint(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, HierarachyWisePrintFlag, CustomerComments, EvassPizza)
                Case 1
                    '------ GET ALL METTLER BILLS  IF THERE IS ANY METTLER BILL DATA THEN PROCEED ELSE FOLLOW OLD LOGIC ------
                    Dim obj As New SpectrumBL.clsCashMemo()
                    '   DtScannedMettlerBills = obj.GetMettlerBillDetailsDataForPrint(CashMemoNo, SiteCode)
                    If SpectrumAsMettler Then
                        DtScannedMettlerBills = obj.GetSpectrumMettlerBillDetailsDataForPrint(CashMemoNo, SiteCode)
                    Else
                        DtScannedMettlerBills = obj.GetMettlerBillDetailsDataForPrint(CashMemoNo, SiteCode)
                    End If

                    If (Not DtScannedMettlerBills Is Nothing) AndAlso (DtScannedMettlerBills.Rows.Count > 0) Then
                        If IsCounterCopy Then
                            cntMettler = 1
                            Call CashMemoKOTPrintMettlerBillWiseSSRS(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, dayopendate, ClientName, DayCloseReportPath)
                        End If
                    Else
                        If IsArticleWiseKot Then
                            Call CashMemoKOTPrint(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, HierarachyWisePrintFlag, CustomerComments, EvassPizza)
                        End If
                    End If
                Case 2

                Case Else
                    Call CashMemoKOTPrint(Type, errorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, HierarachyWisePrintFlag, CustomerComments, EvassPizza)
            End Select
        Catch ex As Exception
            errorMsg = ex.Message
        End Try
    End Sub

    Dim isremark As Boolean = False
    'ashma
    Public Sub CashMemoKOTPrint(ByVal Type As String, ByRef errorMsg As String, ByRef dtView As DataTable, ByRef taxDetailsForBill As DataTable, ByVal DtUnique As DataTable, ByVal Dtcombo As DataTable, CustomerNameLine As String, Optional ByVal HierarachyWisePrintFlag As Boolean = False)
        Try
            errorMsg = ""
            Dim strLineDetail, sbKOTBillPrintBase, sbKOTBillPrintBaseTakeAway, sbKOTBillPrintArticleMain, sbKOTBillPrintArticleTakeAway, sbKOTBillPrintArticleQuantityWise As New StringBuilder
            Dim StrCMNo, StrTokenNo, StrCustomerName As String
            Dim LineItemHeading, StrSubHeader, StrLineItem As String
            Dim ItemCode, Desc, Qty, TakeAwayQty, remark As String
            Dim strLine, StrTokenSalesTypeLine, StrTokenTakeAwayLine As String
            Dim strDblLine As String
            Dim isTakeAwayQtyPrint As Boolean = False
            Dim timeDate As String = DateTime.Now.ToString("g")
            Dim IsHierarachyWisePrintFlag As Boolean = HierarachyWisePrintFlag

            Dim TotaltakeAwayQty = DtUnique.Compute("SUM(TakeAwayQuantity)", "").ToString()
            If Val(TotaltakeAwayQty) > 0 Then
                isTakeAwayQtyPrint = True
            End If

            errorMsg = ""
            If Not dtView Is Nothing Then
                strLine = "-".PadRight(LineLength, "-") & vbCrLf
                strDblLine = "=".PadRight(LineLength, "=") & vbCrLf
                '------ STEP-1:  collecting all Data With Format -----
                If Type = "DCM" Then
                    StrCMNo = getValueByKey("CLSCMP008") & dtView.Rows(0)("Billno").ToString()
                Else
                    StrCMNo = getValueByKey("CLSCMP009") & dtView.Rows(0)("Billno").ToString()
                    sbKOTBillPrintBase.Append(getValueByKey("CLSCMP009") & dtView.Rows(0)("Billno").ToString() + vbCrLf + vbCrLf)
                    'sbKOTBillPrintBase.Append(strLineL40 + vbCrLf)
                End If

                sbKOTBillPrintBase.Append(timeDate + vbCrLf + vbCrLf)
                sbKOTBillPrintBase.Append(strLineL40 + vbCrLf)

                If PrintCouponNumberForKOT = True Then
                    'Dim CouponNo As String = strTillno.Substring(0, 1) & strTillno.Substring(strTillno.Length - 2, 2) & strBillno.Substring(strBillno.Length - 2, 2)
                    Dim tillNo As String = dtView.Rows(0)("TerminalId").ToString()
                    Dim billno As String = dtView.Rows(0)("Billno").ToString()
                    Dim CouponNo As String = tillNo.Substring(0, 1) & tillNo.Substring(tillNo.Length - 2, 2) & billno.Substring(billno.Length - 2, 2)
                    StrTokenNo = CouponNo
                Else
                    StrTokenNo = dtView.Rows(0)("Billno").ToString().Trim.Substring(dtView.Rows(0)("Billno").ToString().Trim.Length - 2, 2)
                End If
                Select Case PrintFormatNo
                    Case 1
                        StrTokenNo = "<B>" & StrTokenNo & "</B>"
                    Case 2
                    Case Else
                End Select

                If Type = "CMWAmt" Then
                    'LineItemHeading = getValueByKey("CLSPSO020") & Space(17) & vbCrLf & getValueByKey("CLSCMP016") & Space(20) & getValueByKey("CLSPSO022") & " " & vbCrLf
                    LineItemHeading = "Article " & Space(17) & vbCrLf & getValueByKey("CLSCMP016") & Space(20) & getValueByKey("CLSPSO022") & " " & vbCrLf
                Else
                    LineItemHeading = getValueByKey("CLSCMP016") & Space(13) & getValueByKey("CLSPSO022") & " " & Space(5)
                    Select Case PrintFormatNo
                        Case 1
                            LineItemHeading &= "Amt" & vbCrLf
                        Case 2

                        Case Else
                            LineItemHeading &= "Amt" & vbCrLf
                    End Select

                End If

                If (dtView.Rows.Count() > 0 And String.IsNullOrEmpty(DeliveryPersonName)) Then
                    If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                        StrTokenNo = "Token No. " & StrTokenNo
                    End If
                End If

                'sbKOTBillPrintBase.Append(getValueByKey("CLSPSO020") + getValueByKey("CLSPSO022").PadLeft(LineLength - getValueByKey("CLSPSO020").Length - 4) + vbCrLf)
                sbKOTBillPrintBase.Append(getValueByKey("TT002") + getValueByKey("CLSPSO022").PadLeft(LineLength - getValueByKey("TT002").Length - 4) + vbCrLf)
                sbKOTBillPrintBase.Append(strLineL40 + vbCrLf)
                If Not IsDBNull(dtView.Rows(0)("DeliveryPersonID")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("DeliveryPersonID")) Then
                    DeliveryPersonName = dtView.Rows(0)("DeliveryPersonID").ToString()
                End If
                StrTokenTakeAwayLine = "Take Away" & vbCrLf
                If dtView.Rows.Count() > 0 Then
                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        'If AllowBillingOnlyAfterSelectionOfSalesType Then
                        StrTokenSalesTypeLine = CustomerSaleType & vbCrLf
                        'End If
                    Else
                        If _CustomerNameRequiredInKOT AndAlso CustomerNameLine <> Nothing Then
                            StrCustomerName = CustomerNameLine.ToUpper.Replace("CUSTOMER", "").Trim()
                            'sbKOTBillPrintBase.Insert(0, StrCustomerName & vbCrLf)
                            sbKOTBillPrintBase.Insert(0, SplitString(StrCustomerName).ToString() & vbCrLf)
                        End If
                        If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                            If Not AllowBillingOnlyAfterSelectionOfSalesType Then
                                CustomerSaleType = String.Empty

                            End If
                            StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - CustomerSaleType.Length) & CustomerSaleType & vbCrLf & vbCrLf
                            StrTokenTakeAwayLine = StrTokenNo.PadRight(LineLength - 1 - "Take Away".Length) & "Take Away" & vbCrLf & vbCrLf
                        Else
                            If AllowBillingOnlyAfterSelectionOfSalesType Then
                                StrTokenSalesTypeLine = CustomerSaleType & vbCrLf
                            End If
                        End If
                    End If
                Else
                    If AllowBillingOnlyAfterSelectionOfSalesType Then
                        StrTokenSalesTypeLine = CustomerSaleType & vbCrLf
                    End If
                End If

                sbKOTBillPrintBaseTakeAway.Append(sbKOTBillPrintBase)
                sbKOTBillPrintBase.Insert(0, StrTokenSalesTypeLine)
                sbKOTBillPrintBaseTakeAway.Insert(0, StrTokenTakeAwayLine)



                '---- Step 2- making 2 Instance 1 for DineIn 2 for TakeAway 
                '---- looping items and construct prints 
                ''printerwise article to print logic implememnted 
                Dim obj As New SpectrumBL.clsCashMemo()
                Dim hierarchyPrinters As New DataTable
                hierarchyPrinters = obj.GetPrinterHierarchyList()
                'temp articles and printer details table 
                Dim dtArticleDetailsToPrint As New DataTable
                dtArticleDetailsToPrint.TableName = "dtArticleDetailsToPrint"
                dtArticleDetailsToPrint.Columns.Add("ArticleCode", GetType(String))
                dtArticleDetailsToPrint.Columns.Add("ArticleName", GetType(String))
                dtArticleDetailsToPrint.Columns.Add("Qty", GetType(Double))
                dtArticleDetailsToPrint.Columns.Add("PrinterName", GetType(String))
                dtArticleDetailsToPrint.Columns.Add("ArticleHierarchy", GetType(String))
                Dim BILLLineNo1 As String
                Dim StrAricleHierarachy As List(Of String)

                Dim DefaultKotPrinterName As String = String.Empty
                Dim tempresult() As DataRow = dtPrinterInfo1.Select("PrinterDocument  = 'KOT'")
                If tempresult.Count > 0 Then
                    For Each row As DataRow In tempresult
                        DefaultKotPrinterName = row(3).ToString()
                    Next
                Else
                    DefaultKotPrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                End If

                For Each Printer In hierarchyPrinters.Rows
                    Dim dtHierarchyArticles = obj.GetHierarchyPrintersLastNodeCode(Printer("ArticleHierarchy").ToString().Trim())

                    sbKOTBillPrintArticleMain.Length = 0


                    For Each drBillarticle As DataRow In dtView.Rows

                        '---- Two Pass - First for articles that belong to configure Hierarchy list
                        Dim IsArticleAdded() As DataRow = dtArticleDetailsToPrint.Select("ArticleCode  = '" & drBillarticle("ArticleCode").ToString() & "'")
                        If IsArticleAdded.Count = 0 Then
                            If Not dtHierarchyArticles Is Nothing AndAlso dtHierarchyArticles.Rows.Count > 0 Then '' added by ketan 
                                Dim IsArticleInHierarchy() As DataRow = dtHierarchyArticles.Select("ArticleCode  = '" & drBillarticle("ArticleCode").ToString() & "'")
                                If IsArticleInHierarchy.Count > 0 Then
                                    Dim resultDelivery As DataRow() = dtHierarchyArticles.Select("ArticleCode  = '" & drBillarticle("ArticleCode").ToString() & "'")
                                    dtArticleDetailsToPrint.Rows.Add(drBillarticle("ArticleCode").ToString(), drBillarticle("DISCRIPTION").ToString(), drBillarticle("Quantity").ToString(), Printer("PrinterName"), resultDelivery(0)("LastNodeCodeName").ToString().Trim())
                                End If
                            End If
                        End If
                    Next
                Next

                For Each drBillarticle As DataRow In dtView.Rows
                    Dim IsArticleAdded() As DataRow = dtArticleDetailsToPrint.Select("ArticleCode  = '" & drBillarticle("ArticleCode").ToString() & "'")
                    If IsArticleAdded.Count = 0 Then
                        dtArticleDetailsToPrint.Rows.Add(drBillarticle("ArticleCode").ToString(), drBillarticle("DISCRIPTION").ToString(), drBillarticle("Quantity").ToString(), DefaultKotPrinterName)
                    End If
                Next

                '''''' passsing datatable for priner wise article printing  
                If dtArticleDetailsToPrint.Rows.Count > 0 AndAlso Not dtArticleDetailsToPrint Is Nothing Then
                    Dim dtPrinterforPrint As DataTable
                    Dim dvprinteers As New DataView(dtArticleDetailsToPrint)
                    dtPrinterforPrint = dvprinteers.ToTable(True, "printername")


                    ''''''' To print the articles as hierarachywise on the flag basis for the configured printer starts here 




                    Dim dtArticleHerirachyTable As DataTable
                    Dim dtArticleHerirachy As New DataView(dtArticleDetailsToPrint)

                    If IsHierarachyWisePrintFlag Then
                        dtArticleHerirachyTable = dtArticleHerirachy.ToTable(True, "ArticleHierarchy", "printername")
                    Else
                        dtArticleHerirachyTable = dtArticleHerirachy.ToTable(True, "printername")
                    End If



                    For Each printers As DataRow In dtArticleHerirachyTable.Rows
                        '---- Here Find All article in dtUnique that belong to that printer ...

                        Dim printerName = printers("printername").ToString()
                        Dim dtArticlePrint As DataTable
                        If IsHierarachyWisePrintFlag Then
                            Dim Articleherirachy = printers("ArticleHierarchy").ToString()
                            dtArticlePrint = (From art In DtUnique
                                           Join artPrint In dtArticleDetailsToPrint
                                           On art("ArticleCode").ToString() Equals artPrint("ArticleCode").ToString()
                            Where artPrint("printername").ToString = printerName And artPrint("ArticleHierarchy").ToString() = Articleherirachy
                                           Select art).CopyToDataTable
                        Else
                            dtArticlePrint = (From art In DtUnique
                                           Join artPrint In dtArticleDetailsToPrint
                                           On art("ArticleCode").ToString() Equals artPrint("ArticleCode").ToString()
                            Where artPrint("printername").ToString = printerName
                                           Select art).CopyToDataTable
                        End If


                        Dim BillLineNO As String = String.Empty




                        For Each dr As DataRow In dtArticlePrint.Rows
                            ItemCode = dr("ArticleCode").ToString()
                            Desc = IIf(DisplayArticleFullName, dr("ArticleFullName").ToString(), dr("DISCRIPTION").ToString())
                            remark = IIf(IsDBNull(dr("Remark")), String.Empty, dr("Remark"))
                            BillLineNO = dr("BillLineNO").ToString()
                            If SpectrumCommon.ComboItemPrintingAllowed AndAlso Dtcombo IsNot Nothing AndAlso Dtcombo.Rows.Count > 0 Then
                                Dim articleDescriptionDictionary As New Dictionary(Of String, Integer)
                                Dim drp() = Dtcombo.Select("ComboArticleCode='" & ItemCode & "' AND BillLineNO ='" & BillLineNO & "'")

                                If drp.Count > 0 Then
                                    For Each Row In drp
                                        If articleDescriptionDictionary.ContainsKey(Row("ArticleName")) Then
                                            articleDescriptionDictionary(Row("ArticleName")) = articleDescriptionDictionary(Row("ArticleName")) + Row("Quantity")
                                        Else
                                            articleDescriptionDictionary.Add(Row("ArticleName"), Row("Quantity"))
                                        End If
                                    Next
                                    Dim AddedDesc = GetMultilinedString(articleDescriptionDictionary)
                                    Desc = Desc & vbCrLf & AddedDesc
                                End If
                            End If

                            Qty = dr("Quantity").ToString()
                            Dim IsForEachQuantityOver = False


                            '// if qty > 1 && qty wise flag true && item eligible for each qty print 
                            If (KOTPrintForEachQuantity) Then
                                If Qty > 0 And dr("IsQuntityWiseRequired") = 1 Then
                                    Dim QtyArray As Array = Qty.Split(".")
                                    Dim MaxCnt As Integer = Convert.ToInt32(QtyArray(0))
                                    Qty = "1.00"

                                    'If (Desc.Length >= LineLength) Or (Desc.Length + Qty.Length >= LineLength) Then
                                    '    sbKOTBillPrintArticleQuantityWise.Append(MakeHtml(Desc) + vbCrLf)
                                    '    sbKOTBillPrintArticleQuantityWise.Append(MakeHtml(Qty.PadLeft(LineLength - 2)) + vbCrLf)
                                    'Else
                                    '    sbKOTBillPrintArticleQuantityWise.Append(MakeHtml(Desc + Qty.PadLeft(LineLength - Desc.Length - 2)) + vbCrLf)
                                    'End If

                                    ''add remark in KOT print added by ketan 
                                    ' sbKOTBillPrintArticleQuantityWise.Append(MakeHtml(Desc, Qty))
                                    If remark = "" Then
                                        isremark = True
                                        If IsKotFontLarge Then
                                            Desc = SplitString(Desc, IIf(IsKotFontLarge, 20, 40)).ToString
                                        Else
                                            Desc = SplitString(Desc, IIf(IsKotFontBold, 30, 40)).ToString
                                        End If
                                        sbKOTBillPrintArticleQuantityWise.Append(MakeHtml(Desc, Qty))
                                    Else
                                        isremark = True
                                        If IsKotFontLarge Then
                                            Desc = SplitString(Desc + "(" + remark + ")", IIf(IsKotFontLarge, 20, 40)).ToString
                                        Else
                                            Desc = SplitString(Desc + "(" + remark + ")", IIf(IsKotFontBold, 30, 40)).ToString
                                        End If
                                        sbKOTBillPrintArticleQuantityWise.Append(MakeHtml(Desc, Qty))
                                    End If
                                    Dim kotQty As Integer = 0
                                    While kotQty < MaxCnt
                                        Call fnKotPrint(sbKOTBillPrintBase, sbKOTBillPrintArticleQuantityWise, errorMsg, printerName)
                                        kotQty = kotQty + 1
                                    End While
                                    'For kotQty As Integer = 0 To MaxCnt - 1
                                    '    Call fnKotPrint(sbKOTBillPrintBase, sbKOTBillPrintArticleQuantityWise, errorMsg)
                                    'Next
                                    sbKOTBillPrintArticleQuantityWise.Length = 0
                                    IsForEachQuantityOver = True
                                    ' Continue For
                                End If
                            End If



                            TakeAwayQty = dr("TakeAwayQuantity").ToString()

                            Call SetQtyFormat(TakeAwayQty)
                            If Val(TakeAwayQty) > 0 Then
                                isTakeAwayQtyPrint = True
                                Qty = TakeAwayQty '(dr("Quantity") - dr("TakeAwayQuantity")).ToString
                                strLineDetail.Append(StrLineItem)

                                ''add remark in KOT print added by ketan 
                                '' sbKOTBillPrintArticleTakeAway.Append(MakeHtml(Desc, Qty))
                                If remark = "" Then
                                    isremark = True
                                    If IsKotFontLarge Then
                                        Desc = SplitString(Desc, IIf(IsKotFontLarge, 20, 40)).ToString
                                    Else
                                        Desc = SplitString(Desc, IIf(IsKotFontBold, 30, 40)).ToString
                                    End If
                                    sbKOTBillPrintArticleTakeAway.Append(MakeHtml(Desc, Qty))
                                Else
                                    isremark = True
                                    If IsKotFontLarge Then
                                        Desc = SplitString(Desc + "(" + remark + ")", IIf(IsKotFontLarge, 20, 40)).ToString
                                    Else
                                        Desc = SplitString(Desc + "(" + remark + ")", IIf(IsKotFontBold, 30, 40)).ToString
                                    End If
                                    sbKOTBillPrintArticleTakeAway.Append(MakeHtml(Desc, Qty))
                                End If

                                'If (Desc.Length >= LineLength) Or (Desc.Length + TakeAwayQty.Length >= LineLength) Then
                                '    sbKOTBillPrintArticleTakeAway.Append(MakeHtml(Desc) + vbCrLf)
                                '    sbKOTBillPrintArticleTakeAway.Append(MakeHtml(TakeAwayQty.PadLeft(LineLength - 2)) + vbCrLf)
                                'Else
                                '    sbKOTBillPrintArticleTakeAway.Append(MakeHtml(Desc + TakeAwayQty.PadLeft(LineLength - Desc.Length - 2)) + vbCrLf)
                                'End If
                            End If

                            strLineDetail.Append(StrLineItem)
                            Call SetQtyFormat(Qty)
                            Qty = FormatNumber((dr("Quantity") - Val(TakeAwayQty)), 3)
                            If Val(Qty) > 0 And (Not IsForEachQuantityOver) Then

                                ' sbKOTBillPrintArticleMain.Append(MakeHtml(Desc, Qty))
                                ''Changed by ketan add remark in kot print
                                If remark = "" Then
                                    isremark = True
                                    If IsKotFontLarge Then
                                        Desc = SplitString(Desc, IIf(IsKotFontLarge, 20, 40)).ToString
                                    Else
                                        Desc = SplitString(Desc, IIf(IsKotFontBold, 30, 40)).ToString
                                    End If
                                    sbKOTBillPrintArticleMain.Append(MakeHtml(Desc, Qty))
                                Else
                                    isremark = True
                                    If IsKotFontLarge Then
                                        Desc = SplitString(Desc + "(" + remark + ")", IIf(IsKotFontLarge, 20, 40)).ToString
                                    Else
                                        Desc = SplitString(Desc + "(" + remark + ")", IIf(IsKotFontBold, 30, 40)).ToString
                                    End If
                                    sbKOTBillPrintArticleMain.Append(MakeHtml(Desc, Qty))
                                End If

                                'If (Desc.Length >= LineLength) Or (Desc.Length + Qty.Length >= LineLength) Then
                                '    sbKOTBillPrintArticleMain.Append(MakeHtml(Desc) + vbCrLf)
                                '    'sbKOTBillPrintArticleMain.Append(Qty.PadLeft(LineLength - Qty.Length) + vbCrLf)
                                '    sbKOTBillPrintArticleMain.Append(MakeHtml(Qty.PadLeft(LineLength - 2)) + vbCrLf)
                                'Else
                                '    sbKOTBillPrintArticleMain.Append(MakeHtml(Desc + Qty.PadLeft(LineLength - Desc.Length - 2)) + vbCrLf)
                                'End If
                            End If

                            ' step 3---------- Now Calling for Print ----------------
                            ''Check item is this item require each Qty Print 

                            If (KOTPrintEachlineItem) And (Not IsForEachQuantityOver) Then
                                Call fnKotPrint(sbKOTBillPrintBase, sbKOTBillPrintArticleMain, errorMsg, printerName)
                                If Val(TakeAwayQty) > 0 Then
                                    Call fnKotPrint(sbKOTBillPrintBaseTakeAway, sbKOTBillPrintArticleTakeAway, errorMsg, printerName)
                                End If
                                sbKOTBillPrintArticleMain.Length = 0
                                Continue For
                            End If
                        Next

                        ' step 4--------- if till now not Called for Print , call now----------------
                        If (Not KOTPrintEachlineItem) Then
                            If sbKOTBillPrintArticleMain.Length > 0 Then
                                Call fnKotPrint(sbKOTBillPrintBase, sbKOTBillPrintArticleMain, errorMsg, printerName)
                            End If
                            If isTakeAwayQtyPrint Then
                                Call fnKotPrint(sbKOTBillPrintBaseTakeAway, sbKOTBillPrintArticleTakeAway, errorMsg, printerName)
                            End If
                        End If
                        sbKOTBillPrintArticleMain.Length = 0

                    Next

                    ''''''' To print the articles as hierarachywise on the flag basis for the configured printer ends here 

                End If
            Else
                errorMsg = getValueByKey("CLSCMP031")
            End If
        Catch ex As Exception
            errorMsg = ex.Message
        End Try

    End Sub

    Public Sub CashMemoKOTPrint(ByVal Type As String, ByRef errorMsg As String, ByRef dtView As DataTable, ByVal DtUnique As DataTable, ByVal Dtcombo As DataTable, CustomerNameLine As String, Optional ByVal HierarachyWisePrintFlag As Boolean = False, Optional ByVal CustomerComments As String = "", Optional ByVal EvassPizza As Boolean = False)
        Try
            Dim strLineDetail, sbKOTBillPrintBase, sbKOTBillPrintBaseTakeAway, sbKOTBillPrintArticleMain, sbKOTBillPrintArticleTakeAway, sbKOTBillPrintArticleQuantityWise As New StringBuilder
            Dim StrCMNo, StrTokenNo, StrCustomerName As String
            Dim LineItemHeading, StrSubHeader, StrLineItem As String
            Dim ItemCode, Desc, Qty, TakeAwayQty, remark As String
            Dim strLine, StrTokenSalesTypeLine, StrTokenTakeAwayLine As String
            Dim strDblLine As String
            Dim isTakeAwayQtyPrint As Boolean = False
            Dim timeDate As String = DateTime.Now.ToString("g")
            Dim IsHierarachyWisePrintFlag As Boolean = HierarachyWisePrintFlag
            If KOTPrintFormatNo = 6 Then
                If Not IsDBNull(dtView.Rows(0)("ServiceTaxAmount")) AndAlso Val(dtView.Rows(0)("ServiceTaxAmount")) > 0 Then
                    CustomerSaleType = GetCustomerSaleType(dtView.Rows(0)("ServiceTaxAmount"))
                Else
                    CustomerSaleType = ""
                End If
            End If
            Dim TotaltakeAwayQty = DtUnique.Compute("SUM(TakeAwayQuantity)", "").ToString()
            If Val(TotaltakeAwayQty) > 0 Then
                isTakeAwayQtyPrint = True
            End If
            errorMsg = ""
            If Not dtView Is Nothing Then
                strLine = "-".PadRight(LineLength, "-") & vbCrLf
                strDblLine = "=".PadRight(LineLength, "=") & vbCrLf
                '------ STEP-1:  collecting all Data With Format -----
                If Type = "DCM" Then
                    StrCMNo = getValueByKey("CLSCMP008") & dtView.Rows(0)("Billno").ToString()
                Else
                    StrCMNo = getValueByKey("CLSCMP009") & dtView.Rows(0)("Billno").ToString()
                    sbKOTBillPrintBase.Append(getValueByKey("CLSCMP009") & dtView.Rows(0)("Billno").ToString() + vbCrLf + vbCrLf)
                    'sbKOTBillPrintBase.Append(strLineL40 + vbCrLf)
                End If

                sbKOTBillPrintBase.Append(timeDate + vbCrLf + vbCrLf)
                sbKOTBillPrintBase.Append(strLineL40 + vbCrLf)

                If PrintCouponNumberForKOT = True Then
                    'Dim CouponNo As String = strTillno.Substring(0, 1) & strTillno.Substring(strTillno.Length - 2, 2) & strBillno.Substring(strBillno.Length - 2, 2)
                    Dim tillNo As String = dtView.Rows(0)("TerminalId").ToString()
                    Dim billno As String = dtView.Rows(0)("Billno").ToString()
                    Dim CouponNo As String = tillNo.Substring(0, 1) & tillNo.Substring(tillNo.Length - 2, 2) & billno.Substring(billno.Length - 2, 2)
                    StrTokenNo = CouponNo
                Else
                    StrTokenNo = dtView.Rows(0)("Billno").ToString().Trim.Substring(dtView.Rows(0)("Billno").ToString().Trim.Length - 2, 2)
                End If
                Select Case PrintFormatNo
                    Case 1
                        StrTokenNo = "<B>" & StrTokenNo & "</B>"
                    Case 2
                    Case Else
                End Select

                If Type = "CMWAmt" Then
                    'LineItemHeading = getValueByKey("CLSPSO020") & Space(17) & vbCrLf & getValueByKey("CLSCMP016") & Space(20) & getValueByKey("CLSPSO022") & " " & vbCrLf
                    LineItemHeading = "Article " & Space(17) & vbCrLf & getValueByKey("CLSCMP016") & Space(20) & getValueByKey("CLSPSO022") & " " & vbCrLf
                Else
                    LineItemHeading = getValueByKey("CLSCMP016") & Space(13) & getValueByKey("CLSPSO022") & " " & Space(5)
                    Select Case PrintFormatNo
                        Case 1
                            LineItemHeading &= "Amt" & vbCrLf
                        Case 2

                        Case Else
                            LineItemHeading &= "Amt" & vbCrLf
                    End Select

                End If

                If (dtView.Rows.Count() > 0 And String.IsNullOrEmpty(DeliveryPersonName)) Then
                    If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                        'added by khusrao adil on 29-08-2017 for natural
                        If PrintFormatNo = 6 Then
                            StrTokenNo = "<BT>" & "Token No. " & StrTokenNo & "</BT>"
                        Else
                            StrTokenNo = "Token No. " & StrTokenNo
                        End If
                    Else
                        If PrintFormatNo = 6 Then
                            StrTokenNo = "<BT>" & "Token No. " & StrTokenNo & "</BT>"
                        End If
                    End If
                End If

                'sbKOTBillPrintBase.Append(getValueByKey("CLSPSO020") + getValueByKey("CLSPSO022").PadLeft(LineLength - getValueByKey("CLSPSO020").Length - 4) + vbCrLf)
                sbKOTBillPrintBase.Append(getValueByKey("TT002") + getValueByKey("CLSPSO022").PadLeft(LineLength - getValueByKey("TT002").Length - 4) + vbCrLf)
                sbKOTBillPrintBase.Append(strLineL40 + vbCrLf)
                If Not IsDBNull(dtView.Rows(0)("DeliveryPersonID")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("DeliveryPersonID")) Then
                    DeliveryPersonName = dtView.Rows(0)("DeliveryPersonID").ToString()
                End If
                StrTokenTakeAwayLine = "Take Away" & vbCrLf
                If dtView.Rows.Count() > 0 Then
                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        'If AllowBillingOnlyAfterSelectionOfSalesType Then
                        'StrTokenSalesTypeLine = CustomerSaleType & vbCrLf
                        StrTokenSalesTypeLine = "<B>" & CustomerSaleType & "</B>" & vbCrLf
                        'End If
                    Else
                        If _CustomerNameRequiredInKOT AndAlso CustomerNameLine <> Nothing Then
                            StrCustomerName = CustomerNameLine.ToUpper.Replace("CUSTOMER", "").Trim()
                            'sbKOTBillPrintBase.Insert(0, StrCustomerName & vbCrLf)
                            sbKOTBillPrintBase.Insert(0, SplitString(StrCustomerName).ToString() & vbCrLf)
                        End If
                        If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                            If Not AllowBillingOnlyAfterSelectionOfSalesType Then
                                CustomerSaleType = String.Empty
                            Else
                                If CustomerSaleType Is Nothing Then
                                    CustomerSaleType = String.Empty
                                End If
                            End If
                            Dim _tempCustomerSaleType = "<B>" & CustomerSaleType & "</B>"
                            If CustomerSaleType.Trim.ToString = "Dine In" AndAlso KOTPrintFormatNo = 6 Then
                                _tempCustomerSaleType = String.Empty
                            End If
                            StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - _tempCustomerSaleType.Length) & _tempCustomerSaleType & vbCrLf & vbCrLf
                            StrTokenTakeAwayLine = StrTokenNo.PadRight(LineLength - 1 - "Take Away".Length) & "Take Away" & vbCrLf & vbCrLf
                        Else
                            If AllowBillingOnlyAfterSelectionOfSalesType Then
                                StrTokenSalesTypeLine = CustomerSaleType & vbCrLf
                            End If
                            If Not StrTokenSalesTypeLine Is Nothing AndAlso StrTokenSalesTypeLine.Trim.ToString = "Dine In" AndAlso KOTPrintFormatNo = 6 Then
                                StrTokenSalesTypeLine = String.Empty
                            End If
                            StrTokenSalesTypeLine += StrTokenNo.PadRight(LineLength - 1 - "Take Away".Length) & vbCrLf & vbCrLf
                        End If
                    End If
                Else
                    If AllowBillingOnlyAfterSelectionOfSalesType Then
                        StrTokenSalesTypeLine = CustomerSaleType & vbCrLf
                    End If
                End If
                If StrTokenSalesTypeLine Is Nothing Then
                    StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - "Take Away".Length) & vbCrLf & vbCrLf
                End If
                'If CustomerSaleType = "Dine In" AndAlso KOTPrintFormatNo = 6 Then
                '    StrTokenSalesTypeLine = String.Empty
                'End If

                sbKOTBillPrintBaseTakeAway.Append(sbKOTBillPrintBase)
                sbKOTBillPrintBase.Insert(0, StrTokenSalesTypeLine)
                sbKOTBillPrintBaseTakeAway.Insert(0, StrTokenTakeAwayLine)



                '---- Step 2- making 2 Instance 1 for DineIn 2 for TakeAway 
                '---- looping items and construct prints 
                ''printerwise article to print logic implememnted 
                Dim obj As New SpectrumBL.clsCashMemo()
                Dim hierarchyPrinters As New DataTable
                hierarchyPrinters = obj.GetPrinterHierarchyList()
                'temp articles and printer details table 
                Dim dtArticleDetailsToPrint As New DataTable
                dtArticleDetailsToPrint.TableName = "dtArticleDetailsToPrint"
                dtArticleDetailsToPrint.Columns.Add("ArticleCode", GetType(String))
                dtArticleDetailsToPrint.Columns.Add("ArticleName", GetType(String))
                dtArticleDetailsToPrint.Columns.Add("Qty", GetType(Double))
                dtArticleDetailsToPrint.Columns.Add("PrinterName", GetType(String))
                dtArticleDetailsToPrint.Columns.Add("ArticleHierarchy", GetType(String))
                Dim BILLLineNo1 As String
                Dim StrAricleHierarachy As List(Of String)

                Dim DefaultKotPrinterName As String = String.Empty
                Dim tempresult() As DataRow = dtPrinterInfo1.Select("PrinterDocument  = 'KOT'")
                If tempresult.Count > 0 Then
                    For Each row As DataRow In tempresult
                        DefaultKotPrinterName = row(3).ToString()
                    Next
                Else
                    DefaultKotPrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                End If

                For Each Printer In hierarchyPrinters.Rows
                    Dim dtHierarchyArticles = obj.GetHierarchyPrintersLastNodeCode(Printer("ArticleHierarchy").ToString().Trim())

                    sbKOTBillPrintArticleMain.Length = 0


                    For Each drBillarticle As DataRow In dtView.Rows

                        '---- Two Pass - First for articles that belong to configure Hierarchy list
                        Dim IsArticleAdded() As DataRow = dtArticleDetailsToPrint.Select("ArticleCode  = '" & drBillarticle("ArticleCode").ToString() & "'")
                        If IsArticleAdded.Count = 0 Then
                            If Not dtHierarchyArticles Is Nothing AndAlso dtHierarchyArticles.Rows.Count > 0 Then '' added by ketan 
                                Dim IsArticleInHierarchy() As DataRow = dtHierarchyArticles.Select("ArticleCode  = '" & drBillarticle("ArticleCode").ToString() & "'")
                                If IsArticleInHierarchy.Count > 0 Then
                                    Dim resultDelivery As DataRow() = dtHierarchyArticles.Select("ArticleCode  = '" & drBillarticle("ArticleCode").ToString() & "'")
                                    dtArticleDetailsToPrint.Rows.Add(drBillarticle("ArticleCode").ToString(), drBillarticle("DISCRIPTION").ToString(), drBillarticle("Quantity").ToString(), Printer("PrinterName"), resultDelivery(0)("LastNodeCodeName").ToString().Trim())
                                End If
                            End If
                        End If
                    Next
                Next

                For Each drBillarticle As DataRow In dtView.Rows
                    Dim IsArticleAdded() As DataRow = dtArticleDetailsToPrint.Select("ArticleCode  = '" & drBillarticle("ArticleCode").ToString() & "'")
                    If IsArticleAdded.Count = 0 Then
                        dtArticleDetailsToPrint.Rows.Add(drBillarticle("ArticleCode").ToString(), drBillarticle("DISCRIPTION").ToString(), drBillarticle("Quantity").ToString(), DefaultKotPrinterName)
                    End If
                Next

                '''''' passsing datatable for priner wise article printing  
                If dtArticleDetailsToPrint.Rows.Count > 0 AndAlso Not dtArticleDetailsToPrint Is Nothing Then
                    Dim dtPrinterforPrint As DataTable
                    Dim dvprinteers As New DataView(dtArticleDetailsToPrint)
                    dtPrinterforPrint = dvprinteers.ToTable(True, "printername")


                    ''''''' To print the articles as hierarachywise on the flag basis for the configured printer starts here 




                    Dim dtArticleHerirachyTable As DataTable
                    Dim dtArticleHerirachy As New DataView(dtArticleDetailsToPrint)

                    If IsHierarachyWisePrintFlag Then
                        dtArticleHerirachyTable = dtArticleHerirachy.ToTable(True, "ArticleHierarchy", "printername")
                    Else
                        dtArticleHerirachyTable = dtArticleHerirachy.ToTable(True, "printername")
                    End If



                    For Each printers As DataRow In dtArticleHerirachyTable.Rows
                        '---- Here Find All article in dtUnique that belong to that printer ...

                        Dim printerName = printers("printername").ToString()
                        Dim dtArticlePrint As DataTable
                        If IsHierarachyWisePrintFlag Then
                            Dim Articleherirachy = printers("ArticleHierarchy").ToString()
                            dtArticlePrint = (From art In DtUnique
                                           Join artPrint In dtArticleDetailsToPrint
                                           On art("ArticleCode").ToString() Equals artPrint("ArticleCode").ToString()
                            Where artPrint("printername").ToString = printerName And artPrint("ArticleHierarchy").ToString() = Articleherirachy
                                           Select art).CopyToDataTable
                        Else
                            dtArticlePrint = (From art In DtUnique
                                           Join artPrint In dtArticleDetailsToPrint
                                           On art("ArticleCode").ToString() Equals artPrint("ArticleCode").ToString()
                            Where artPrint("printername").ToString = printerName
                                           Select art).CopyToDataTable
                        End If


                        Dim BillLineNO As String = String.Empty




                        For Each dr As DataRow In dtArticlePrint.Rows
                            ItemCode = dr("ArticleCode").ToString()
                            'Desc = IIf(DisplayArticleFullName, dr("ArticleFullName").ToString(), dr("DISCRIPTION").ToString())
                            'code added by irfan on 24-08-2017
                            If Not dtArticlePrint.Columns.Contains("ArticleFullName") Then
                                Desc = dr("DISCRIPTION").ToString()
                            Else
                                Desc = IIf(DisplayArticleFullName, dr("ArticleFullName").ToString(), dr("DISCRIPTION").ToString())
                            End If
                            remark = IIf(IsDBNull(dr("Remark")), String.Empty, dr("Remark"))
                            BillLineNO = dr("BillLineNO").ToString()
                            If SpectrumCommon.ComboItemPrintingAllowed AndAlso Dtcombo IsNot Nothing AndAlso Dtcombo.Rows.Count > 0 Then
                                Dim articleDescriptionDictionary As New Dictionary(Of String, Integer)
                                Dim drp() = Dtcombo.Select("ComboArticleCode='" & ItemCode & "' AND BillLineNO ='" & BillLineNO & "'")

                                If drp.Count > 0 Then
                                    For Each Row In drp
                                        If articleDescriptionDictionary.ContainsKey(Row("ArticleName")) Then
                                            articleDescriptionDictionary(Row("ArticleName")) = articleDescriptionDictionary(Row("ArticleName")) + Row("Quantity")
                                        Else
                                            articleDescriptionDictionary.Add(Row("ArticleName"), Row("Quantity"))
                                        End If
                                    Next
                                    Dim AddedDesc = GetMultilinedString(articleDescriptionDictionary)
                                    Desc = Desc & vbCrLf & AddedDesc
                                End If
                            End If

                            Qty = dr("Quantity").ToString()
                            Dim IsForEachQuantityOver = False


                            '// if qty > 1 && qty wise flag true && item eligible for each qty print 
                            If (KOTPrintForEachQuantity) Then
                                If Qty > 0 And dr("IsQuntityWiseRequired") = 1 Then
                                    Dim QtyArray As Array = Qty.Split(".")
                                    Dim MaxCnt As Integer = Convert.ToInt32(QtyArray(0))
                                    Qty = "1.00"

                                    'If (Desc.Length >= LineLength) Or (Desc.Length + Qty.Length >= LineLength) Then
                                    '    sbKOTBillPrintArticleQuantityWise.Append(MakeHtml(Desc) + vbCrLf)
                                    '    sbKOTBillPrintArticleQuantityWise.Append(MakeHtml(Qty.PadLeft(LineLength - 2)) + vbCrLf)
                                    'Else
                                    '    sbKOTBillPrintArticleQuantityWise.Append(MakeHtml(Desc + Qty.PadLeft(LineLength - Desc.Length - 2)) + vbCrLf)
                                    'End If

                                    ''add remark in KOT print added by ketan 
                                    ' sbKOTBillPrintArticleQuantityWise.Append(MakeHtml(Desc, Qty))
                                    If remark = "" Then
                                        isremark = True
                                        If IsKotFontLarge Then
                                            Desc = SplitString(Desc, IIf(IsKotFontLarge, 20, 40)).ToString
                                        Else
                                            Desc = SplitString(Desc, IIf(IsKotFontBold, 30, 40)).ToString
                                        End If
                                        sbKOTBillPrintArticleQuantityWise.Append(MakeHtml(Desc, Qty))
                                    Else
                                        isremark = True
                                        If IsKotFontLarge Then
                                            Desc = SplitString(Desc + "(" + remark + ")", IIf(IsKotFontLarge, 20, 40)).ToString
                                        Else
                                            Desc = SplitString(Desc + "(" + remark + ")", IIf(IsKotFontBold, 30, 40)).ToString
                                        End If
                                        sbKOTBillPrintArticleQuantityWise.Append(MakeHtml(Desc, Qty))
                                    End If
                                    Dim kotQty As Integer = 0
                                    While kotQty < MaxCnt
                                        Call fnKotPrint(sbKOTBillPrintBase, sbKOTBillPrintArticleQuantityWise, errorMsg, printerName)
                                        kotQty = kotQty + 1
                                    End While
                                    'For kotQty As Integer = 0 To MaxCnt - 1
                                    '    Call fnKotPrint(sbKOTBillPrintBase, sbKOTBillPrintArticleQuantityWise, errorMsg)
                                    'Next
                                    sbKOTBillPrintArticleQuantityWise.Length = 0
                                    IsForEachQuantityOver = True
                                    ' Continue For
                                End If
                            End If



                            TakeAwayQty = dr("TakeAwayQuantity").ToString()

                            Call SetQtyFormat(TakeAwayQty)
                            If Val(TakeAwayQty) > 0 Then
                                isTakeAwayQtyPrint = True
                                Qty = TakeAwayQty '(dr("Quantity") - dr("TakeAwayQuantity")).ToString
                                strLineDetail.Append(StrLineItem)

                                ''add remark in KOT print added by ketan 
                                '' sbKOTBillPrintArticleTakeAway.Append(MakeHtml(Desc, Qty))
                                If remark = "" Then
                                    isremark = True
                                    If IsKotFontLarge Then
                                        Desc = SplitString(Desc, IIf(IsKotFontLarge, 20, 40)).ToString
                                    Else
                                        Desc = SplitString(Desc, IIf(IsKotFontBold, 30, 40)).ToString
                                    End If
                                    sbKOTBillPrintArticleTakeAway.Append(MakeHtml(Desc, Qty))
                                Else
                                    isremark = True
                                    If IsKotFontLarge Then
                                        Desc = SplitString(Desc + "(" + remark + ")", IIf(IsKotFontLarge, 20, 40)).ToString
                                    Else
                                        Desc = SplitString(Desc + "(" + remark + ")", IIf(IsKotFontBold, 30, 40)).ToString
                                    End If
                                    sbKOTBillPrintArticleTakeAway.Append(MakeHtml(Desc, Qty))
                                End If

                                'If (Desc.Length >= LineLength) Or (Desc.Length + TakeAwayQty.Length >= LineLength) Then
                                '    sbKOTBillPrintArticleTakeAway.Append(MakeHtml(Desc) + vbCrLf)
                                '    sbKOTBillPrintArticleTakeAway.Append(MakeHtml(TakeAwayQty.PadLeft(LineLength - 2)) + vbCrLf)
                                'Else
                                '    sbKOTBillPrintArticleTakeAway.Append(MakeHtml(Desc + TakeAwayQty.PadLeft(LineLength - Desc.Length - 2)) + vbCrLf)
                                'End If
                            End If

                            strLineDetail.Append(StrLineItem)
                            Call SetQtyFormat(Qty)
                            Qty = FormatNumber((dr("Quantity") - Val(TakeAwayQty)), 3)
                            If Val(Qty) > 0 And (Not IsForEachQuantityOver) Then

                                ' sbKOTBillPrintArticleMain.Append(MakeHtml(Desc, Qty))
                                ''Changed by ketan add remark in kot print
                                If remark = "" Then
                                    isremark = True
                                    If IsKotFontLarge Then
                                        Desc = SplitString(Desc, IIf(IsKotFontLarge, 20, 40)).ToString
                                    Else
                                        Desc = SplitString(Desc, IIf(IsKotFontBold, 30, 40)).ToString
                                    End If
                                    sbKOTBillPrintArticleMain.Append(MakeHtml(Desc, Qty))
                                Else
                                    isremark = True
                                    If IsKotFontLarge Then
                                        Desc = SplitString(Desc + "(" + remark + ")", IIf(IsKotFontLarge, 20, 40)).ToString
                                    Else
                                        Desc = SplitString(Desc + "(" + remark + ")", IIf(IsKotFontBold, 30, 40)).ToString
                                    End If
                                    sbKOTBillPrintArticleMain.Append(MakeHtml(Desc, Qty))
                                End If

                                'If (Desc.Length >= LineLength) Or (Desc.Length + Qty.Length >= LineLength) Then
                                '    sbKOTBillPrintArticleMain.Append(MakeHtml(Desc) + vbCrLf)
                                '    'sbKOTBillPrintArticleMain.Append(Qty.PadLeft(LineLength - Qty.Length) + vbCrLf)
                                '    sbKOTBillPrintArticleMain.Append(MakeHtml(Qty.PadLeft(LineLength - 2)) + vbCrLf)
                                'Else
                                '    sbKOTBillPrintArticleMain.Append(MakeHtml(Desc + Qty.PadLeft(LineLength - Desc.Length - 2)) + vbCrLf)
                                'End If
                            End If

                            ' step 3---------- Now Calling for Print ----------------
                            ''Check item is this item require each Qty Print 

                            If (KOTPrintEachlineItem) And (Not IsForEachQuantityOver) Then
                                Call fnKotPrint(sbKOTBillPrintBase, sbKOTBillPrintArticleMain, errorMsg, printerName, CustomerComments, EvassPizza)
                                If Val(TakeAwayQty) > 0 Then
                                    Call fnKotPrint(sbKOTBillPrintBaseTakeAway, sbKOTBillPrintArticleTakeAway, errorMsg, printerName, CustomerComments, EvassPizza)
                                End If
                                sbKOTBillPrintArticleMain.Length = 0
                                Continue For
                            End If
                        Next

                        ' step 4--------- if till now not Called for Print , call now----------------
                        If (Not KOTPrintEachlineItem) Then
                            If sbKOTBillPrintArticleMain.Length > 0 Then
                                Call fnKotPrint(sbKOTBillPrintBase, sbKOTBillPrintArticleMain, errorMsg, printerName, CustomerComments, EvassPizza)
                            End If
                            If isTakeAwayQtyPrint Then
                                Call fnKotPrint(sbKOTBillPrintBaseTakeAway, sbKOTBillPrintArticleTakeAway, errorMsg, printerName, CustomerComments, EvassPizza)
                            End If
                        End If
                        sbKOTBillPrintArticleMain.Length = 0

                    Next

                    ''''''' To print the articles as hierarachywise on the flag basis for the configured printer ends here 

                End If
            Else
                errorMsg = getValueByKey("CLSCMP031")
            End If
        Catch ex As Exception
            errorMsg = ex.Message
        End Try

    End Sub

    Public Sub CashMemoKOTPrintMettlerBillWise(ByVal Type As String, ByRef errorMsg As String, ByRef dtView As DataTable, ByVal DtUnique As DataTable, ByVal Dtcombo As DataTable, CustomerNameLine As String, Optional ByVal dayopendate As Date = Nothing)
        Try
            '------ Variable Declaration ----------------
            Dim strLineDetail, sbKOTBillPrintBase, sbKOTBillPrintArticleMain As New StringBuilder
            Dim StrCMNo, StrCustomerName As String
            Dim ItemCode, Desc, Qty, PricePerUnit, Disc, mettlerBillQty, BillNoList As String
            Dim strLine, strDblLine, StrHeader, StrSubHeader As String
            Dim ValueMultiplyFactor As Double = 1, ItemAmt, BillTotalAmt As Double
            Dim isTakeAwayQtyPrint As Boolean = False
            Dim objItemSch As New clsIteamSearch()
            Dim ScaleNo, BillNo, ScaleBillIntDate, totalItems, colSpacelength As Long
            '------ Initilize Function ----------------
            LineLength = 30
            strLine = "-".PadRight(LineLength, "-") & vbCrLf
            strDblLine = "=".PadRight(LineLength, "=") & vbCrLf
            errorMsg = ""
            '---- Create Sub Header Variables -----
            If Type = "DCM" Then
                StrCMNo = getValueByKey("CLSCMP008") & dtView.Rows(0)("Billno").ToString()
            Else
                StrCMNo = getValueByKey("CLSCMP009") & dtView.Rows(0)("Billno").ToString()
            End If
            Dim StrCashMemoLine = StrCMNo & vbCrLf
            Dim StrCMTime = getValueByKey("CLSCMP032") & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")
            '---- Making Print for EACH Mettler Bill ----
            StrHeader = GetMettlerKOTBillHeader(dtView)

            For ScanBillIndex = 0 To DtScannedMettlerBills.Rows.Count - 1
                '------- GET Mettler Scanned Bill Details From Mettler DataBase 
                With DtScannedMettlerBills
                    ScaleNo = Val(.Rows(ScanBillIndex)("SCALE_NO")) : BillNo = Val(.Rows(ScanBillIndex)("BILL_NO"))
                    ScaleBillIntDate = Val(.Rows(ScanBillIndex)("MettlerScaleBillDate"))
                End With
                BillNoList = BillNoList & BillNo.ToString() & ","
                Dim dtScanedBillArticle = objItemSch.GetScanedBillArticle(ScaleNo:=ScaleNo, BillNo:=BillNo, BillIntDate:=ScaleBillIntDate, MettlerConn:=MettlerConnString)

                Try
                    Dim test = (From BillItems In DtUnique.AsEnumerable() Select BillItems Join MettlerBillItems In dtScanedBillArticle.AsEnumerable()
                            On BillItems.Field(Of String)("ArticleCode") Equals MettlerBillItems.Field(Of Int64)("LegacyArticleCode").ToString()
                            Select BillItems).FirstOrDefault()

                    If test Is Nothing Then Continue For
                Catch ex As Exception
                    LogException(ex)
                End Try


                sbKOTBillPrintBase.Length = 0 : sbKOTBillPrintArticleMain.Length = 0 : BillTotalAmt = 0 : StrSubHeader = String.Empty
                'sbKOTBillPrintBase.Append(vbCrLf)

                '------ Adding Header 
                'sbKOTBillPrintBase.Append("<FontPCKotHeader>PRASHANT CORNER".PadLeft(LineLength / 2 + ("PRASHANT CORNER".Length / 2)) & "<FontPCKotHeader>" + vbCrLf)
                ''Added
                '   sbKOTBillPrintBase.Append("<FontPCKotArticleDetails>" & "Submit Counter Copy at".PadLeft(CInt((LineLength - "Submit Counter Copy at".Length) + 12), " ") & "</FontPCKotArticleDetails> " & vbCrLf)
                '  sbKOTBillPrintBase.Append("<FontPCKotArticleDetails>" & "scale".PadLeft(CInt((LineLength - "scale".Length) + 6), " ") & "</FontPCKotArticleDetails>" & "<FontPCKOTArticle>" & ScaleNo & "</FontPCKOTArticle>" & vbCrLf & vbCrLf)
                sbKOTBillPrintBase.Append(Space(3.5) & "<FontPCKotArticleDetails>" & "Submit Counter Copy at".PadLeft(20 / 2) & vbCrLf & Space(5) & "scale".PadLeft(20 / 2) & "</FontPCKotArticleDetails>" & "<FontPCKotBillPaid>" & Space(1) & ScaleNo & "</FontPCKotBillPaid>" & vbCrLf & vbCrLf)
                sbKOTBillPrintBase.Append("<FontPCKotHeader>" & "PRASHANT CORNER".PadLeft(CInt((LineLength - "PRASHANT CORNER".Length) + 6), " ") & "</FontPCKotHeader>" & vbCrLf)
                'sbKOTBillPrintBase.Append("<FontPCKotHeader>" & "PRASHANT CORNER".PadLeft((Val(LineLength - "PRASHANT CORNER".Length) + "PRASHANT CORNER".Length) / 2) & "</FontPCKotHeader>" & vbCrLf & vbCrLf)
                sbKOTBillPrintBase.Append(StrHeader)
                'Removed one  vbcrlf
                sbKOTBillPrintBase.Append(vbCrLf)
                'Added
                ' sbKOTBillPrintBase.Append("<FontPCKotHeader>" & "COUNTER COPY".PadLeft(CInt((LineLength - "COUNTER COPY".Length) + 1), " ") & "</FontPCKotHeader>" & vbCrLf & vbCrLf)
                'sbKOTBillPrintBase.Append(vbCrLf & vbCrLf)
                'sbKOTBillPrintBase.Append("<FontPCKotHeader>" & "COUNTER COPY".PadLeft((Val(LineLength - "COUNTER COPY".Length) + "COUNTER COPY".Length) / 2) & "</FontPCKotHeader>" & vbCrLf & vbCrLf)

                sbKOTBillPrintBase.Append(GetMettlerKotBillSubHeader(StrCashMemoLine, ScaleNo, BillNo, StrCMTime, dayopendate))
                'sbKOTBillPrintBase.Append(vbCrLf) : sbKOTBillPrintBase.Append(vbCrLf)


                '---- Columns Heading 
                sbKOTBillPrintBase.Append("<B>" & strLine & "</B>") '+ vbCrLf
                Call GetMettlerKOTBillColumnHeading(sbKOTBillPrintBase)
                sbKOTBillPrintBase.Append("<B>" & strLine & "</B>") '+ vbCrLf & vbCrLf
                totalItems = 0
                For BillRowIndex = 0 To dtScanedBillArticle.Rows.Count - 1 Step 1
                    Dim PrintDataRows() = DtUnique.Select("ArticleCode ='" & dtScanedBillArticle.Rows(BillRowIndex)("LegacyArticleCode") & "'")
                    If Not PrintDataRows Is Nothing And PrintDataRows.Count > 0 Then
                        '------ STEP-1:  collecting all Data With Format -----
                        For Each dr As DataRow In PrintDataRows
                            ItemCode = dr("ArticleCode").ToString()
                            Desc = IIf(DisplayArticleFullName, dr("ArticleFullName").ToString(), dr("DISCRIPTION").ToString())
                            Qty = dr("Quantity").ToString()
                            mettlerBillQty = dtScanedBillArticle.Rows(BillRowIndex)("Quantity")
                            'totalItems = Val(DtScannedMettlerBills.Rows(ScanBillIndex)("TotalLineItems")).ToString()
                            'ValueMultiplyFactor = Math.Round(Val(mettlerBillQty) / Val(Qty), 3)
                            totalItems = totalItems + 1 'Val(DtScannedMettlerBills.Rows(ScanBillIndex)("TotalLineItems")).ToString()
                            ValueMultiplyFactor = Val(mettlerBillQty) / Val(Qty)
                            'PricePerUnit = dr("SELLINGPRICE").ToString()
                            PricePerUnit = Math.Round(dr("SELLINGPRICE")).ToString
                            Disc = (Val(dr("TOTALDISCOUNT").ToString()) * ValueMultiplyFactor).ToString
                            ItemAmt = Math.Round(Val(dr("GrossAMT").ToString()) * ValueMultiplyFactor, 2).ToString
                            'BillTotalAmt = BillTotalAmt + Val(ItemAmt)
                            ItemAmt = Math.Round(ItemAmt).ToString()
                            BillTotalAmt = Math.Round(BillTotalAmt + Val(ItemAmt)).ToString
                            Call SetQtyFormat(mettlerBillQty)

                            ' If dr("UnitofMeasure").ToString().ToUpper = "KGS" Then mettlerBillQty = mettlerBillQty & " Kg"
                            Call AddMettlerKotBillItemLine(sbKOTBillPrintArticleMain, Desc, ItemCode, PricePerUnit, Disc, mettlerBillQty, ItemAmt.ToString())
                        Next
                    Else
                        Continue For
                    End If
                Next BillRowIndex
                '-----------------------------------------------------------------
                sbKOTBillPrintArticleMain.Append("<B>" & strLine & "</B>") '& vbCrLf
                GetMettlerKOTBillFooter(sbKOTBillPrintArticleMain, totalItems, BillTotalAmt)

                If sbKOTBillPrintArticleMain.Length > 0 Then
                    Call fnKotPrint(sbKOTBillPrintBase, sbKOTBillPrintArticleMain, errorMsg)
                End If

            Next ScanBillIndex
            '------ CODE FOR THOSE ITEMS IS NOT THE PART OF METTLER SCAN 
            '----- Now Process those element not belong to mettler Bills
            Dim isPrintOtherItems As Boolean

            sbKOTBillPrintBase.Length = 0 : sbKOTBillPrintArticleMain.Length = 0
            BillTotalAmt = 0 : totalItems = 0 : StrSubHeader = String.Empty
            sbKOTBillPrintBase.Append(vbCrLf)
            '------ Adding Header 
            sbKOTBillPrintBase.Append("PRASHANT CORNER".PadLeft(CInt((LineLength - "PRASHANT CORNER".Length) + 3), " ") & vbCrLf)
            sbKOTBillPrintBase.Append(StrHeader)
            '------- GET Mettler Scanned Bill Details From Mettler DataBase 
            sbKOTBillPrintBase.Append(vbCrLf) : sbKOTBillPrintBase.Append(vbCrLf)
            sbKOTBillPrintBase.Append(GetMettlerKotBillSubHeader(StrCashMemoLine, ScaleNo:=String.Empty, BillNo:=String.Empty, StrCMTime:=StrCMTime, dayopendate:=dayopendate))
            sbKOTBillPrintBase.Append(vbCrLf) : sbKOTBillPrintBase.Append(vbCrLf)
            '---- Columns Heading 
            sbKOTBillPrintBase.Append("<B>" & strLineL40 & "</B>" + vbCrLf)
            Call GetMettlerKOTBillColumnHeading(sbKOTBillPrintBase)
            sbKOTBillPrintBase.Append("<B>" & strLineL40 & "</B>" + vbCrLf & vbCrLf)

            Dim dtScanedBillArticleSum = objItemSch.GetScanedCashMemoBillsArticle(BillNoList:=BillNoList.Substring(0, BillNoList.Length - 1), BillIntDate:=ScaleBillIntDate, MettlerConn:=MettlerConnString)
            For Each dr As DataRow In DtUnique.Rows
                If Val(dr("ArticleCode")) > 0 Then
                    Dim PrintDataRows() = dtScanedBillArticleSum.Select("LegacyArticleCode ='" & dr("ArticleCode") & "'")
                    Dim PrintDataRowsCondition2() = dtScanedBillArticleSum.Select("LegacyArticleCode ='" & dr("ArticleCode") & "' AND Quantity < " & dr("Quantity"))
                    If (PrintDataRows.Count = 0) Or (PrintDataRowsCondition2.Count > 0) Then
                        isPrintOtherItems = True
                        totalItems = totalItems + 1
                        ItemCode = dr("ArticleCode").ToString()
                        Desc = IIf(DisplayArticleFullName, dr("ArticleFullName").ToString(), dr("DISCRIPTION").ToString())
                        If PrintDataRows.Count > 0 Then
                            Qty = (Val(dr("Quantity")) - Val(PrintDataRows(0)("Quantity"))).ToString
                            'ValueMultiplyFactor = Math.Round((Val(dr("Quantity")) - Val(PrintDataRows(0)("Quantity"))) / dr("Quantity"), 3)
                            ValueMultiplyFactor = (Val(dr("Quantity")) - Val(PrintDataRows(0)("Quantity"))) / dr("Quantity")
                        Else
                            Qty = dr("Quantity").ToString
                            ValueMultiplyFactor = 1
                        End If
                        PricePerUnit = dr("SELLINGPRICE").ToString()
                        Disc = Math.Round(Val(dr("TOTALDISCOUNT").ToString()) * ValueMultiplyFactor, 2).ToString
                        ItemAmt = Math.Round(Val(dr("NETAMOUNT").ToString()) * ValueMultiplyFactor, 2).ToString
                        BillTotalAmt = BillTotalAmt + Val(ItemAmt)
                        Call SetQtyFormat(Qty)
                        If dr("UnitofMeasure").ToString().ToUpper = "KGS" Then
                            Qty = Qty & " Kg"
                        End If
                        Call AddMettlerKotBillItemLine(sbKOTBillPrintArticleMain, Desc, ItemCode, PricePerUnit, Disc, Qty, ItemAmt.ToString())
                    End If
                End If
            Next
            '-------------------------------------------------------------------
            sbKOTBillPrintArticleMain.Append(strLine & vbCrLf)
            GetMettlerKOTBillFooter(sbKOTBillPrintArticleMain, totalItems, BillTotalAmt)
            If sbKOTBillPrintArticleMain.Length > 0 AndAlso isPrintOtherItems Then
                Call fnKotPrint(sbKOTBillPrintBase, sbKOTBillPrintArticleMain, errorMsg)
            End If

        Catch ex As Exception
            errorMsg = ex.Message
        End Try
    End Sub

    Private Sub AddMettlerKotBillItemLine(ByRef sbKOTBillPrintArticleMain As StringBuilder, Desc As String, ItemCode As String, PricePerUnit As String, Disc As String, Qty As String, ItemAmt As String)
        Try
            'Dim CurrentLineLength As Integer = 0
            ''--- Calculation For First Row(40 char) , Article(16) , Code(12) ,PricePerUnit(12) 
            'If (Desc.Length + ItemCode.Length > 20) Then
            '    sbKOTBillPrintArticleMain.Append("<B>" & Desc & "</B>" & vbCrLf)
            '    If ItemCode.Length + PricePerUnit.Length > 22 Then
            '        sbKOTBillPrintArticleMain.Append(ItemCode & vbCrLf)
            '    Else
            '        sbKOTBillPrintArticleMain.Append((ItemCode.PadLeft(20)))
            '        sbKOTBillPrintArticleMain.Append((PricePerUnit.PadLeft(18)))
            '    End If
            'Else
            '    sbKOTBillPrintArticleMain.Append("<B>" & Desc & "</B>")
            '    sbKOTBillPrintArticleMain.Append((ItemCode.PadLeft(20 - Desc.Length)))
            '    If PricePerUnit.Length > 18 Then
            '        sbKOTBillPrintArticleMain.Append(vbCrLf)
            '        sbKOTBillPrintArticleMain.Append(PricePerUnit.PadLeft(38))
            '    Else
            '        sbKOTBillPrintArticleMain.Append(PricePerUnit.PadLeft(18))
            '    End If
            'End If
            'sbKOTBillPrintArticleMain.Append(vbCrLf)
            '' --------- Calculation of Second Row 
            ''If (Disc.Length + Qty.Length > 19) Then
            ''    sbKOTBillPrintArticleMain.Append(Disc & vbCrLf)
            ''    If Qty.Length + ItemAmt.Length > 22 Then
            ''        sbKOTBillPrintArticleMain.Append(Qty & vbCrLf)
            ''    Else
            ''        sbKOTBillPrintArticleMain.Append((Qty.PadLeft(19)))
            ''        sbKOTBillPrintArticleMain.Append((ItemAmt.PadLeft(19)))
            ''    End If
            ''Else
            ''    sbKOTBillPrintArticleMain.Append(Disc)
            ''    sbKOTBillPrintArticleMain.Append((Qty.PadLeft(20 - Disc.Length)))
            ''    If ItemAmt.Length > 20 Then
            ''        sbKOTBillPrintArticleMain.Append(vbCrLf)
            ''        sbKOTBillPrintArticleMain.Append(ItemAmt.PadLeft(38))
            ''    Else
            ''        sbKOTBillPrintArticleMain.Append(ItemAmt.PadLeft(18))
            ''    End If
            ''End If
            'If (Qty.Length > 19) Then
            '    'sbKOTBillPrintArticleMain.Append(Disc & vbCrLf)
            '    If Qty.Length + ItemAmt.Length > 22 Then
            '        sbKOTBillPrintArticleMain.Append(Qty & vbCrLf)
            '    Else
            '        sbKOTBillPrintArticleMain.Append((Qty.PadLeft(19)))
            '        sbKOTBillPrintArticleMain.Append((ItemAmt.PadLeft(19)))
            '    End If
            'Else
            '    'sbKOTBillPrintArticleMain.Append(Disc)
            '    sbKOTBillPrintArticleMain.Append((Qty.PadLeft(20)))
            '    If ItemAmt.Length > 20 Then
            '        sbKOTBillPrintArticleMain.Append(vbCrLf)
            '        sbKOTBillPrintArticleMain.Append(ItemAmt.PadLeft(38))
            '    Else
            '        sbKOTBillPrintArticleMain.Append(ItemAmt.PadLeft(18))
            '    End If
            'End If
            ''sbKOTBillPrintArticleMain.Append(vbCrLf)
            sbKOTBillPrintArticleMain.Append("<FontPCKOTArticle>" & SplitStringPC(Desc, 15).ToString().Trim() & Space(20 - strLastLength) & Qty & vbCrLf & vbCrLf & "</FontPCKotArticle>")
            Dim CurrentLineLength As Integer = 0
            '--- Calculation For First Row(40 char) , Article(16) , Code(12) ,PricePerUnit(12) 
            '  If (Desc.Length > 19) Then
            'sbKOTBillPrintArticleMain.Append("<B>" & Desc & "</B>" & vbCrLf)
            ' If Desc.Length + Qty.Length >= 25 Then

            ' sbKOTBillPrintArticleMain.Append(Val(Desc.PadLeft(39)))


            'If Tender.Length + Value.Length > strLineL40.Length Then
            '    TenderNetCash = SplitString(Tender, strLineL40.Length).ToString().Trim & vbCrLf 'Tender & vbCrLf
            '    TenderNetCash = TenderNetCash & Value.PadLeft(strLineL40.Length) & vbCrLf
            'Else
            '    TenderNetCash = Tender & Space(39 - ((Value.Length) + (Tender.Length))) & Value.Replace("-", " ") & vbCrLf & vbCrLf
            'End If

            'TenderNetCash = Tender & Space(39 - ((Value.Length) + (Tender.Length))) & Value.Replace("-", " ") & vbCrLf & vbCrLf
            ' StrNetCash = StrNetCash & TenderNetCash
            '  Dim Description As String
            ' Description = SplitStringPC(Desc, 16).ToString().Trim & vbCrLf

            'sbKOTBillPrintArticleMain.Append("<FontPCBillItemDesc>" & Description & "</FontPCBillItemDesc>")

            '        'sbKOTBillPrintArticleMain.Append("<FontPCKotArticleDetails>" & (Qty.PadLeft(14)) & "</FontPCKotArticleDetails>")
            '        sbKOTBillPrintArticleMain.Append("<FontPCKotArticleDetails>" & (Qty.PadLeft(26)) & "</FontPCKotArticleDetails>")
            '    Else
            '        sbKOTBillPrintArticleMain.Append("<FontPCKOTArticle>" & Desc & "</FontPCKOTArticle>")
            '        'sbKOTBillPrintArticleMain.Append("<FontPCKotArticleDetails>" & (Qty.PadLeft(14)) & "</FontPCKotArticleDetails>")
            '        sbKOTBillPrintArticleMain.Append("<FontPCKotArticleDetails>" & (Qty.PadLeft(26)) & "</FontPCKotArticleDetails>")
            '    End If
            'Else
            '    sbKOTBillPrintArticleMain.Append("<FontPCBillItemDesc>" & Desc & "</FontPCBillItemDesc>")
            '    'sbKOTBillPrintArticleMain.Append((Desc.PadLeft(20 - Desc.Length)))
            '    If Qty.Length > 21 Then
            '        sbKOTBillPrintArticleMain.Append(vbCrLf)
            '        'sbKOTBillPrintArticleMain.Append(vbCrLf)
            '        sbKOTBillPrintArticleMain.Append("<FontPCKotArticleDetails>" & Qty.PadLeft(26) & "</FontPCKotArticleDetails>")
            '    Else
            '        'sbKOTBillPrintArticleMain.Append(vbCrLf)
            '        'sbKOTBillPrintArticleMain.Append(vbCrLf)
            '        'sbKOTBillPrintArticleMain.Append("<FontPCKotArticleDetails>" & (Qty.PadLeft(14)) & "</FontPCKotArticleDetails>")
            '        sbKOTBillPrintArticleMain.Append("<FontPCKotArticleDetails>" & (Qty.PadLeft(26)) & "</FontPCKotArticleDetails>")
            '    End If
            'End If



            '----Change By Mahesh As requested By client(Gaurav) Dated - 28-10-2015
            'If (PricePerUnit.Length > 21) Then
            '    'sbKOTBillPrintArticleMain.Append(Disc & vbCrLf)
            '    If PricePerUnit.Length + ItemAmt.Length > 23 Then
            '        sbKOTBillPrintArticleMain.Append("<FontPCKotArticleDetails>" & PricePerUnit.PadLeft(31) & "</FontPCKotArticleDetails>")
            '    Else
            '        sbKOTBillPrintArticleMain.Append("<FontPCKotArticleDetails>" & (PricePerUnit.PadLeft(11)) & "</FontPCKotArticleDetails>")
            '        sbKOTBillPrintArticleMain.Append("<FontPCKotArticleDetails>" & (ItemAmt.PadLeft(11)) & "</FontPCKotArticleDetails>")
            '    End If
            'Else
            '    'sbKOTBillPrintArticleMain.Append(Disc)
            '    sbKOTBillPrintArticleMain.Append("<FontPCKotArticleDetails>" & (PricePerUnit.PadLeft(6)) & "</FontPCKotArticleDetails>")
            '    If ItemAmt.Length > 11 Then
            '        sbKOTBillPrintArticleMain.Append(vbCrLf)
            '        sbKOTBillPrintArticleMain.Append("<FontPCKotArticleDetails>" & ItemAmt.PadLeft(29) & "</FontPCKotArticleDetails>")
            '    Else
            '        sbKOTBillPrintArticleMain.Append("<FontPCKotArticleDetails>" & ItemAmt.PadLeft(6) & "</FontPCKotArticleDetails>")
            '    End If
            'End If
            sbKOTBillPrintArticleMain.Append(vbCrLf & vbCrLf)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function GetMettlerKOTBillFooter(ByRef sbKOTBillPrintArticleMain As StringBuilder, TotalLineItems As String, BillTotalAmt As String) As Boolean
        Try
            'Dim VatTanString As String = "VAT/TIN NO: 27120029370 U/V"
            'Dim LbtString As String = "LBT NO: TMC-LBT0005578-13"
            'Dim msgString As String = "THANK YOU .. VISIT AGAIN"
            '----Change By Mahesh As requested By client(Gaurav) Dated - 28-10-2015
            Dim strLine As String
            strLine = "-".PadRight(LineLength, "-") '& vbCrLf
            sbKOTBillPrintArticleMain.Append("<B>" & "ITEMS: " & TotalLineItems & "</B>")
            If ("ITEMS: " & TotalLineItems).Length + ("INR: " & BillTotalAmt).Length > 40 Then
                sbKOTBillPrintArticleMain.Append("<B>" & "INR: ".PadLeft(LineLength - ("ITEMS: " & TotalLineItems).Length - 5) & "</B>" & vbCrLf)
                sbKOTBillPrintArticleMain.Append(BillTotalAmt.PadLeft(LineLength - 2))
            Else
                sbKOTBillPrintArticleMain.Append("<B>" & ("INR: " & BillTotalAmt).PadLeft(LineLength - ("ITEMS: " & TotalLineItems).Length - 3) & "</B>" & vbCrLf)
            End If
            ' sbKOTBillPrintArticleMain.Append("<B>" & strLine & "</B>" & vbCrLf)
            sbKOTBillPrintArticleMain.Append("<B>" & strLine & "</B>" & vbCrLf)
            'sbKOTBillPrintArticleMain.Append(vbCrLf & vbCrLf & "<FontPCKotBillPaid> BILL PAID </FontPCKotBillPaid>".PadLeft(64) & vbCrLf & vbCrLf & vbCrLf)
            'sbKOTBillPrintArticleMain.Append(vbCrLf & vbCrLf & "<FontPCKotBillPaid> BILL PAID </FontPCKotBillPaid>".PadLeft(Val(LineLength / 2) + "<FontPCKotBillPaid> BILL PAID </FontPCKotBillPaid>".Length / 2 + 18) & vbCrLf & vbCrLf & vbCrLf)
            sbKOTBillPrintArticleMain.Append("<FontPCKotBillPaid> BILL PAID </FontPCKotBillPaid>".PadLeft(Val(LineLength / 2) + "<FontPCKotBillPaid> BILL PAID </FontPCKotBillPaid>".Length / 2 + 18) & vbCrLf & vbCrLf & vbCrLf)
            'sbKOTBillPrintArticleMain.Append(vbCrLf & vbCrLf & "<L>BILL PAID</L>".PadLeft((Val(LineLength / 2) + Val("<L>BILL PAID</L>".Length / 2))) & vbCrLf & vbCrLf & vbCrLf)
            'sbKOTBillPrintArticleMain.Append(VatTanString.PadLeft((Val(LineLength / 2) + Val(VatTanString.Length / 2))) & vbCrLf)
            'sbKOTBillPrintArticleMain.Append(LbtString.PadLeft((Val(LineLength / 2) + Val(LbtString.Length / 2))) & vbCrLf)
            'sbKOTBillPrintArticleMain.Append(vbCrLf & msgString.PadLeft((Val(LineLength / 2) + Val(msgString.Length / 2))) & vbCrLf)
            'sbKOTBillPrintArticleMain.Append("<FontPCKotBillDtl>" & VatTanString.PadLeft((Val(LineLength / 2) + Val(VatTanString.Length / 2)) - 2) & "</FontPCKotBillDtl>" & vbCrLf)
            'sbKOTBillPrintArticleMain.Append("<FontPCKotBillDtl>" & LbtString.PadLeft((Val(LineLength / 2) + Val(LbtString.Length / 2)) - 1) & "</FontPCKotBillDtl>" & vbCrLf)
            'sbKOTBillPrintArticleMain.Append("<FontPCKotBillDtl>" & vbCrLf & msgString.PadLeft((Val(LineLength / 2) + Val(msgString.Length / 2)) - 1) & "</FontPCKotBillDtl>" & vbCrLf)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Function GetMettlerKOTBillHeader(ByRef dtView As DataTable) As String
        Try
            Dim SiteName = dtView.Rows(0)("SITEOFFICIALNAME").ToString()
            Dim TS_SiteName = SplitString(SiteName)
            SiteName = TS_SiteName.ToString
            SiteName = SiteName.PadLeft(LineLength / 2 + (SiteName.Length / 2) - 1)
            Dim PhoneLine = "PHONE: " & dtView.Rows(0)("TELNO").ToString()
            Dim StrSiteTelline = PhoneLine.PadLeft(LineLength / 2 + (PhoneLine.Length / 2))
            Dim StrAdrressLine = dtView.Rows(0)("ADDRESS").ToString()
            'StrAdrressLine = SplitString(StrAdrressLine, LineLength).ToString()
            StrAdrressLine = SplitStringCenterAlign(StrAdrressLine, LineLength + 1).ToString()
            ' GetMettlerKOTBillHeader = SiteName & vbCrLf & StrAdrressLine.Trim & vbCrLf & StrSiteTelline & vbCrLf
            '----Change By Mahesh As requested By client(Gaurav) Dated - 28-10-2015
            'GetMettlerKOTBillHeader = "<FontPCKotHeaderAddress>" & SiteName & vbCrLf & StrAdrressLine & StrSiteTelline & "</FontPCKotHeaderAddress>" & vbCrLf
            'GetMettlerKOTBillHeader = "<FontPCKotHeaderAddress>" & SiteName & vbCrLf & "</FontPCKotHeaderAddress>" & vbCrLf

        Catch ex As Exception
            LogException(ex)
        End Try

    End Function


    Function GetMettlerKotBillSubHeader(ByVal StrCashMemoLine As String, ByVal ScaleNo As String, ByVal BillNo As String, ByVal StrCMTime As String, ByVal dayopendate As Date) As String
        Try
            GetMettlerKotBillSubHeader &= "<FontPCKotBillDtl>" & StrCashMemoLine & "</FontPCKotBillDtl>" '& vbCrLf
            GetMettlerKotBillSubHeader &= "<FontPCKotBillDtl>Date:" & dayopendate.ToString("dd-MM-yyyy") & "</FontPCKotBillDtl>"
            '----Change By Mahesh As requested By client(Gaurav) Dated - 28-10-2015
            GetMettlerKotBillSubHeader &= "<FontPCKotBillDtl>" & ("No:" & BillNo).PadLeft(LineLength - ("Date:" & dayopendate.ToString("dd-MM-yyyy")).ToString.Length - 3) & "</FontPCKotBillDtl>" & vbCrLf '& vbCrLf
            GetMettlerKotBillSubHeader &= "<FontPCKotBillDtl>" & StrCMTime & "</FontPCKotBillDtl>"
            'GetMettlerKotBillSubHeader &= "<FontPCKotArticleDetails>" & ("Scale: " & ScaleNo).PadLeft(LineLength - StrCMTime.Length - 3) & "</FontPCKotArticleDetails>"
            GetMettlerKotBillSubHeader &= "<FontPCKotArticleDetails>" & ("Scale: " & ScaleNo).PadLeft(LineLength - StrCMTime.Length - 3) & "</FontPCKotArticleDetails>" & vbCrLf
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Function GetMettlerKOTBillColumnHeading(ByRef sbKOTBillPrintBase As StringBuilder)
        'sbKOTBillPrintBase.Append("Article" & Space(9) & "Code" & Space(8) & "Price/Unit" & vbCrLf)
        sbKOTBillPrintBase.Append("<FontPCKotBillDtl>" & "Article" & Space(17) & "Qty" & vbCrLf)
        'sbKOTBillPrintBase.Append(Space(4) & Space(12) & "Qty" & Space(14) & "Total" & vbCrLf) '---Disc removing puting 4 space
        'sbKOTBillPrintBase.Append(Space(6) & "Price" & Space(5) & "Total" & vbCrLf)
        sbKOTBillPrintBase.Append(Space(19) & "(Kgs/Nos)" & "</FontPCKotBillDtl>" & vbCrLf)

    End Function

    Sub SetQtyFormat(ByRef Qty As String)
        Try
            If (AllowDecimalQty) Then
                If (WeightScaleEnabled) Then
                    Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 3)
                Else
                    Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 2)
                End If
            Else
                Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 0)
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Sub fnKotPrint(sbKOTBillPrintBase As StringBuilder, sbKOTBillPrintArticle As StringBuilder, ByRef errorMsg As String, Optional ByVal ArticlePrinterName As String = "", Optional ByVal CustomerComments As String = "", Optional ByVal EvassPizza As Boolean = False, Optional ByVal SpectrumAsMettlerBarcode As Boolean = False)
        Try
            'printername = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")

            If ArticlePrinterName = "" Then
                If sbKOTBillPrintArticle.Length = 0 Then Exit Sub
                Dim result() As DataRow = dtPrinterInfo1.Select("PrinterDocument  = 'KOT'")
                If result.Count > 0 Then
                    For Each row As DataRow In result
                        PrinterName = row(3).ToString()
                    Next
                Else
                    PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                End If
            Else
                PrinterName = ArticlePrinterName
            End If
            'dtPrinterInfo1.DefaultViewdtPrinterInfo1RowFilter = "PrinterDocument contains ('KOT'))"
            ' PrinterName = SetPrinterName(dtPrinterInfo1, "KOT", "Billing")

            If PrinterName = Nothing Then
                Exit Sub
            End If
            Dim msg As String = String.Empty
            Dim sbKOTBillPrint As New StringBuilder
            sbKOTBillPrint.Append(vbCrLf & vbCrLf)
            'sbKOTBillPrintArticle.Append(vbCrLf & vbCrLf & vbCrLf & vbCrLf & "---------------------------------------")
            sbKOTBillPrintArticle.Append(vbCrLf & vbCrLf & vbCrLf & vbCrLf)
            If _PrintPreview = True Then

                sbKOTBillPrint.Append(sbKOTBillPrintBase)
                sbKOTBillPrint.Append(sbKOTBillPrintArticle)
                If EvassPizza = True AndAlso Not String.IsNullOrEmpty(CustomerComments) Then
                    sbKOTBillPrint.Append(SplitStringPC("<FontPCBillFooter>Cust. Instructions: " & CustomerComments & "</FontPCBillFooter>", LineLength, NoMoreSpace:=True))
                End If
                msg = fnPrint(sbKOTBillPrint.ToString(), "PRV", 1)
            Else

                sbKOTBillPrint.Append(sbKOTBillPrintBase)
                sbKOTBillPrint.Append(sbKOTBillPrintArticle)
                If EvassPizza = True AndAlso Not String.IsNullOrEmpty(CustomerComments) Then
                    sbKOTBillPrint.Append(SplitStringPC("<FontPCBillFooter>Cust. Instructions: " & CustomerComments & "</FontPCBillFooter>", LineLength, NoMoreSpace:=True))
                End If

                msg = fnPrint(sbKOTBillPrint.ToString(), "PRN", 1)
            End If

            If Not String.IsNullOrEmpty(msg) Then
                errorMsg = msg
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'new paramete added by khusrao adil on 09-06-2017
    ' for combo article display in ssrs reports formats
    Public Function GetMultilinedString(ByVal descriptionDictionary As Dictionary(Of String, Integer), Optional isForComboAndRdl As Boolean = False) As String
        Try
            Dim multilinedStringB As New StringBuilder
            Dim ArticleCount = descriptionDictionary.Count
            Dim LineArticleCount As Integer = 0
            For Each keyValue In descriptionDictionary
                If keyValue.Value <> 0 Then
                    If isForComboAndRdl = True Then
                        LineArticleCount += 1
                        If LineArticleCount = ArticleCount Then
                            multilinedStringB.Append(" *" & keyValue.Key & " - " & keyValue.Value)
                        Else
                            multilinedStringB.Append(" *" & keyValue.Key & " - " & keyValue.Value & vbCrLf)
                        End If
                    Else
                        multilinedStringB.Append("   *" & keyValue.Key & " - " & keyValue.Value & vbCrLf)
                    End If
                Else
                    multilinedStringB.Append(keyValue.Key & vbCrLf)
                End If
            Next
            'multilinedString = multilinedString.Remove(multilinedString.Count - 2, 1)
            Return multilinedStringB.ToString().Remove(multilinedStringB.ToString().Count - 2, 1)
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function



    'Public Sub CashMemoPrint(ByVal dayopenDate As Date, ByVal LangCode As String, ByVal Currency As String, ByVal Sitecode As String, ByVal Type As String, ByVal DuplicatePrinting As String, ByVal DeletedUserid As String, ByVal AuthorisedUser As String, ByRef errorMsg As String, Optional ByVal GiftMsg As String = "")
    '    Try

    '        Dim strPrintMsg As StringBuilder
    '        Dim strLineDetail As New StringBuilder
    '        Dim StrHeader, StrSubHeader, StrBody, StrCompanyInfo, StrTagLine, StrSubFooter, StrFooter, StrDuplicate, StrDeleteLine, StrCashMemoLine, StrCashierLine, StrSalesPersonLine As String
    '        Dim StrCMNo, StrCMDate, StrCMTime, StrCashier, StrSalesPerson As String
    '        Dim SiteLine, StrSiteTelline, StrAdrressLine, StrSiteReg1Line, StrSiteReg2Line As String
    '        Dim CLpLine, LineItemHeading, StrLineItem As String
    '        Dim CustomerNameLine, CLPPointsLine As String
    '        Dim ItemCode, Desc, Qty, Rate, DiscAmt, DiscPer, TaxAmt, NetAmt, RateAfterDisc, GrossAmt As String
    '        Dim TotalQtyLine, TotalGrossAmtLine, TotalDiscAmtLine, SubTotalLine, NetAmtLine, TotalTaxAmtLine As String
    '        Dim TotalFooterTaxDetail, FooterTaxLine As String
    '        Dim TenderDetails, TenderLine As String
    '        Dim TotalPromotionalMsg, PromoMsgLine, StrPrintHeaderLine, StrPrintFooterLine As String
    '        Dim TermsNcond As String
    '        Dim strLine As String
    '        Dim strDblLine As String
    '        Dim strHomeDilery As String
    '        errorMsg = ""
    '        Dim dtView As New DataTable
    '        Dim obj As New SpectrumBL.clsCashMemo()
    '        dtView = obj.GetCashMemo(CashMemoNo, Sitecode, LangCode)
    '        Dim dtPrint As DataTable
    '        dtPrint = obj.GetPrintingDetails("CMInvc")
    '        PrintConfig(Sitecode) 'this is add to get the preview value from reprint
    '        If Not dtPrint Is Nothing Then
    '            Dim filter As String = ""
    '            Dim dv As New DataView(dtPrint, filter, "Sequenceno", DataViewRowState.CurrentRows)
    '            If _HeaderPrint = True Then
    '                filter = "TOPBOTTOM='Top'"
    '                dv.RowFilter = filter
    '                For Each drview As DataRowView In dv
    '                    StrPrintHeaderLine = drview("ReceiptText").ToString()
    '                    StrSubHeader = StrSubHeader & StrPrintHeaderLine & vbNewLine
    '                Next
    '            End If
    '            If _FooterPrint = True Then
    '                filter = "TOPBOTTOM='Bottom'"
    '                dv.RowFilter = filter
    '                For Each drview As DataRowView In dv
    '                    StrPrintFooterLine = drview("ReceiptText").ToString()
    '                    StrTagLine = StrTagLine & StrPrintFooterLine & vbNewLine
    '                Next
    '            End If
    '        End If


    '        If Not StrSubHeader Is Nothing AndAlso StrSubHeader.Length > 0 Then
    '            StrSubHeader = SplitString(StrSubHeader, LineLength).ToString()
    '        End If
    '        'If Not StrTagLine Is Nothing AndAlso StrTagLine.Length > 0 Then
    '        '    StrTagLine = SplitString(StrTagLine, LineLength).ToString()
    '        'End If


    '        If Not dtView Is Nothing Then
    '            strLine = "-".PadRight(LineLength + 22, "-") & vbCrLf
    '            strDblLine = "=".PadRight(LineLength - 3, "=") & vbCrLf
    '            If DuplicatePrinting <> String.Empty Then
    '                StrDuplicate = DuplicatePrinting
    '            End If
    '            'StrDeleteLine = "Cash Memo Status: Deleted"
    '            StrDeleteLine = getValueByKey("CLSCMP001")

    '            If DuplicatePrinting <> String.Empty Then
    '                StrHeader = StrDuplicate & vbCrLf
    '            End If
    '            If Type = "DCM" Then
    '                StrHeader = StrDeleteLine & vbCrLf
    '            End If
    '            If Type = "DCM" Then
    '                'Request ID: <Update / Delete Request ID>
    '                'Authorized By : <User Name >       Deleted at Time: <CM Time>
    '                'StrHeader = StrHeader & "Request ID    :" & DeletedUserid & vbCrLf
    '                'StrHeader = StrHeader & "Authorized By :" & AuthorisedUser & vbCrLf

    '                StrHeader = StrHeader & getValueByKey("CLSCMP002") & DeletedUserid & vbCrLf
    '                StrHeader = StrHeader & getValueByKey("CLSCMP003") & AuthorisedUser & vbCrLf
    '            End If
    '            Dim SiteName, Site As String
    '            Dim TS_SiteName As StringBuilder
    '            'SiteName = "Store Name: " & dtView.Rows(0)("SITEOFFICIALNAME").ToString()
    '            'Site = "Code: " & dtView.Rows(0)("SITECODE").ToString()
    '            'StrSiteTelline = "Tel: " & dtView.Rows(0)("TELNO").ToString()
    '            'StrAdrressLine = "Add: " & dtView.Rows(0)("ADDRESS").ToString()

    '            SiteName = getValueByKey("CLSCMP004") & " " & dtView.Rows(0)("SITEOFFICIALNAME").ToString()
    '            SiteName = dtView.Rows(0)("SITEOFFICIALNAME").ToString()
    '            TS_SiteName = SplitString(SiteName)
    '            SiteName = TS_SiteName.ToString
    '            SiteName = SiteName.PadLeft(LineLength / 2 + (SiteName.Length / 2))
    '            Site = vbCrLf & getValueByKey("CLSCMP005") & " " & dtView.Rows(0)("SITECODE").ToString()
    '            'SiteLine = SiteName & Site
    '            SiteLine = SiteName
    '            'SiteLine = SiteName.PadRight(LineLength - Site.Length) & Site

    '            StrSiteTelline = getValueByKey("CLSCMP006") & " " & dtView.Rows(0)("TELNO").ToString()
    '            StrAdrressLine = getValueByKey("CLSCMP007") & " " & dtView.Rows(0)("ADDRESS").ToString()
    '            StrAdrressLine = SplitString(StrAdrressLine, LineLength).ToString()
    '            'StrSiteReg1Line = "LST NO: " & dtView.Rows(0)("LOCALSALESTAXNO").ToString() & Space(2) & "LST Date: " & dtView.Rows(0)("LOCALSALESTAXDATE").ToString()
    '            'StrSiteReg2Line = "CST NO: " & dtView.Rows(0)("CENTRALSALESTAXNO").ToString() & Space(2) & "CST Date: " & dtView.Rows(0)("CENTRALSALESTAXDATE").ToString()

    '            StrHeader = SiteLine & vbCrLf
    '            StrHeader = StrHeader & StrAdrressLine
    '            StrHeader = StrHeader & StrSiteTelline & vbCrLf
    '            'StrHeader = StrHeader & strLine & vbCrLf
    '            'StrSubHeader = StrSubHeader & StrSiteReg1Line & vbCrLf
    '            'StrSubHeader = StrSubHeader & StrSiteReg2Line & vbCrLf


    '            '****Added by Rahul 30 nov 2009 night(Rashid) . start 
    '            If Type = "DCM" Then
    '                'StrCMNo = "Void Cash Memo:" & dtView.Rows(0)("Billno").ToString()
    '                StrCMNo = getValueByKey("CLSCMP008") & dtView.Rows(0)("Billno").ToString()
    '            Else
    '                'StrCMNo = "Cash Memo:" & dtView.Rows(0)("Billno").ToString()
    '                StrCMNo = getValueByKey("CLSCMP009") & dtView.Rows(0)("Billno").ToString()
    '            End If
    '            '******End 

    '            'StrCMDate = "Date:" & Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())
    '            'StrCMDate = Format(dtView.Rows(0)("BillTime"), clsCommon.GetSystemDateFormat())
    '            StrCMDate = dayopenDate.ToShortDateString()
    '            If DuplicatePrinting <> String.Empty Then
    '                StrCMDate = Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())
    '            End If
    '            StrCashMemoLine = StrCMNo.PadRight(LineLength - StrCMDate.Length) & StrCMDate & vbCrLf
    '            'StrCashier = "Cashier:" & dtView.Rows(0)("Createdby").ToString()
    '            'StrCMTime = "Time:" & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")

    '            StrCashier = getValueByKey("CLSPV006") & dtView.Rows(0)("Createdby").ToString()
    '            StrCMTime = getValueByKey("CLSCMP032") & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")
    '            StrCashierLine = StrCashier.PadRight(LineLength - StrCMTime.Length) & StrCMTime.PadLeft(LineLength - StrCMTime.Length) & vbCrLf
    '            'StrSalesPerson = "Sales Person:" & dtView.Rows(0)("SALESEXECUTIVECODE").ToString()
    '            StrSalesPerson = getValueByKey("CLSCMP010") & dtView.Rows(0)("SALESEXECUTIVECODE").ToString()

    '            If _SalesPerApplicable = True Then
    '                StrSalesPersonLine = StrSalesPerson & vbCrLf
    '            End If

    '            StrSubHeader = StrSubHeader & strLine
    '            StrSubHeader = StrSubHeader & StrCashMemoLine
    '            StrSubHeader = StrSubHeader & StrCashierLine
    '            StrSubHeader = StrSubHeader & StrSalesPersonLine
    '            StrSubHeader = StrSubHeader & strLine
    '            Dim CLPaddress As String = ""
    '            Dim CLPBalancePoints As String
    '            If dtView.Rows(0)("CLPNO").ToString() <> String.Empty Then
    '                'CLpLine = "CLP Card No:" & dtView.Rows(0)("CLPNO").ToString()
    '                'CustomerNameLine = "Customer Name: " & dtView.Rows(0)("CLPNAME").ToString()
    '                'CLPPointsLine = "CLP Points Earned: " & FormatNumber(dtView.Rows(0)("CLPPoints").ToString())

    '                CLpLine = getValueByKey("CLSCMP011") & dtView.Rows(0)("CLPNO").ToString()
    '                CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CLPNAME").ToString()
    '                CLPPointsLine = getValueByKey("CLSCMP013") & FormatNumber(dtView.Rows(0)("CLPPoints").ToString())
    '                CLPaddress = dtView.Rows(0)("CLPAddress").ToString()
    '                If CLPaddress <> String.Empty Then
    '                    CLPaddress = getValueByKey("CLSCMP014") & dtView.Rows(0)("CLPAddress").ToString()
    '                End If
    '                CLPBalancePoints = String.Empty
    '                CLPBalancePoints = getValueByKey("CLSCMP015")
    '                Dim dblCLPPoints As Double
    '                dblCLPPoints = dtView.Rows(0)("BalancePoints")
    '                For Each drRow As DataRow In dtView.Select("Tendertypecode = 'CLPPoint'")
    '                    dblCLPPoints -= drRow("AmountTendered")
    '                Next

    '                CLPBalancePoints = CLPBalancePoints & FormatNumber(dblCLPPoints.ToString())
    '                If CLPaddress <> String.Empty Then
    '                    CLPaddress = SplitString(CLPaddress, LineLength).ToString()
    '                End If
    '                StrSubFooter = StrSubFooter & CLpLine & vbCrLf
    '                StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
    '                StrSubFooter = StrSubFooter & CLPBalancePoints & vbCrLf

    '                StrSubFooter = StrSubFooter & CLPPointsLine & vbCrLf
    '                StrSubFooter = StrSubFooter & CLPaddress & vbCrLf '& vbCrLf & vbCrLf
    '            End If
    '            If Type = "CMWAmt" Then
    '                'LineItemHeading = "Item Code" & Space(17) & "Item Name" & Space(1) & "Qty " & vbCrLf
    '                ' LineItemHeading = getValueByKey("CLSPSO020")  & vbCrLf & getValueByKey("CLSCMP016") & Space(1) & getValueByKey("CLSPSO022") & " " & vbCrLf
    '                'to resolve Issue 448 
    '                LineItemHeading = getValueByKey("CLSPSO020") & Space(17) & vbCrLf & getValueByKey("CLSCMP016") & Space(20) & getValueByKey("CLSPSO022") & " " & vbCrLf
    '            Else
    '                'LineItemHeading = "Item Code" & Space(17) & "Item Name" & Space(5) & vbCrLf
    '                'to resolve Issue 448 
    '                'LineItemHeading = "  Item Code" & vbCrLf & "  Item Name" & vbCrLf
    '                'LineItemHeading = LineItemHeading & Space(3) & "Qty " & Space(2) & "Price" & Space(4) & "Disc." & Space(4) & "Tax" & Space(5) & "Net" & vbCrLf

    '                'LineItemHeading = "  " & getValueByKey("CLSPSO020") & vbCrLf & "  " & getValueByKey("CLSCMP016") & vbCrLf
    '                'LineItemHeading = LineItemHeading & Space(3) & getValueByKey("CLSPSO022") & " " & Space(2) & getValueByKey("CLSPSO023") & Space(4) & getValueByKey("CLSPSO024") & Space(4) & getValueByKey("CLSPSO025") & Space(5) & getValueByKey("CLSPSO026") & vbCrLf
    '                LineItemHeading = getValueByKey("CLSCMP016") & Space(37) & getValueByKey("CLSPSO022") & " " & Space(2) & "Net" & vbCrLf
    '            End If

    '            Dim TotalQty, TGrossAmt, TNetAmt, TDiscAmt, TRateAfterDisc, TTaxtAmt As String

    '            Dim dvItemDetail As New DataView(dtView, "", "", DataViewRowState.CurrentRows)
    '            'Dim DtUnique As DataTable = dvItemDetail.ToTable(True, "ArticleCode", "DISCRIPTION", "QUANTITY", "SELLINGPRICE", "GROSSAMT", "TOTALDISCOUNT", "TOTALDISCPERCENTAGE", "NETAMOUNT", "TOTALTAXAMOUNT")
    '            Dim DtUnique As DataTable = obj.GetBillDetailsDataForPrint(CashMemoNo, Sitecode, LangCode)
    '            If DtUnique Is Nothing Then Exit Sub
    '            For Each dr As DataRow In DtUnique.Rows
    '                ItemCode = dr("ArticleCode").ToString()
    '                Desc = dr("DISCRIPTION").ToString()
    '                Qty = dr("Quantity").ToString()
    '                Rate = dr("SellingPrice").ToString()
    '                RateAfterDisc = (dr("SellingPrice") - dr("TOTALDISCOUNT")).ToString()
    '                If CDbl(clsCommon.CheckIfBlank(Rate)) < 0 Then
    '                    Rate = CDbl(clsCommon.CheckIfBlank(Rate)) * -1
    '                End If
    '                DiscAmt = dr("TOTALDISCOUNT").ToString()
    '                DiscPer = dr("TOTALDISCPERCENTAGE").ToString()
    '                NetAmt = dr("NetAmount").ToString()
    '                GrossAmt = dr("GROSSAMT").ToString()
    '                TaxAmt = dr("TOTALTAXAMOUNT").ToString()
    '                'Dim i As Integer = 0
    '                'For i = ItemCode.Length To 26
    '                '    ItemCode = ItemCode & " "
    '                'Next
    '                'For i = Desc.Length To 14
    '                '    Desc = Desc & " "
    '                'Next
    '                Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 2)
    '                'For i = Qty.Length To 4
    '                '    Qty = " " & Qty
    '                'Next
    '                Rate = FormatNumber(CDbl(clsCommon.CheckIfBlank(Rate)), 2)

    '                RateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(RateAfterDisc)), 2)
    '                'Rate = FormatCurrency(Rate, 2)
    '                'For i = Rate.Length To 8
    '                '    Rate = " " & Rate
    '                'Next
    '                DiscAmt = FormatNumber(CDbl(IIf(DiscAmt <> String.Empty, DiscAmt, 0)), 2)
    '                'For i = DiscAmt.Length To 8
    '                '    DiscAmt = " " & DiscAmt
    '                'Next
    '                DiscPer = FormatNumber(CDbl(IIf(DiscPer <> String.Empty, DiscPer, 0)), 2)
    '                DiscPer = DiscPer & "%"
    '                'For i = DiscPer.Length To 8
    '                '    DiscPer = " " & DiscPer
    '                'Next
    '                NetAmt = FormatNumber(CDbl(NetAmt), 2)
    '                GrossAmt = FormatNumber(CDbl(GrossAmt), 2)
    '                'For i = NetAmt.Length To 8
    '                '    NetAmt = " " & NetAmt
    '                'Next
    '                TaxAmt = FormatNumber(CDbl(IIf(TaxAmt <> String.Empty, TaxAmt, 0)), 2)
    '                'For i = TaxAmt.Length To 7
    '                '    TaxAmt = " " & TaxAmt
    '                'Next
    '                If Type = "CMWAmt" Then
    '                    StrLineItem = ItemCode.PadRight(26) & vbCrLf & Desc.PadRight(35) & Qty & vbCrLf
    '                    'StrLineItem = SplitString(StrLineItem, LineLength).ToString()
    '                    'StrLineItem = StrLineItem & Qty & Rate & DiscAmt & TaxAmt & NetAmt & vbCrLf
    '                Else
    '                    'Issue no 448 resolved 18-Mar-2010

    '                    'StrLineItem = ItemCode.PadRight(26) & Desc & vbCrLf
    '                    'StrLineItem = Space(2) & ItemCode.PadRight(26) & vbCrLf & Space(2) & Desc & vbCrLf

    '                    'If 41 - Desc.Length < Qty.Length Then
    '                    '    StrLineItem = Desc & vbCrLf
    '                    '    StrLineItem = StrLineItem & Qty.PadLeft(41) & GrossAmt.PadLeft(8) & vbCrLf
    '                    'Else
    '                    Dim qtyString As String = Qty & GrossAmt.PadLeft(8)
    '                    StrLineItem = Desc.PadRight(LineLength - qtyString.Length) & qtyString.PadLeft(LineLength - qtyString.Length) & vbCrLf
    '                    'End If

    '                    'StrLineItem = SplitString(StrLineItem, LineLength).ToString()

    '                    'Issue no 448 18-Mar-2010

    '                    'StrLineItem = StrLineItem & Qty.PadLeft(7) & Rate.PadLeft(9) & DiscAmt.PadLeft(7) & " " & TaxAmt.PadLeft(5) & NetAmt.PadLeft(9) & vbCrLf
    '                End If
    '        strLineDetail.Append(StrLineItem)

    '        If CDbl(Qty.Trim) > 0 Then
    '            TotalQty = CDbl(clsCommon.CheckIfBlank(TotalQty)) + CDbl(Qty.Trim)
    '        End If
    '        TDiscAmt = CDbl(clsCommon.CheckIfBlank(TDiscAmt)) + CDbl(DiscAmt.Trim)
    '        TGrossAmt = CDbl(clsCommon.CheckIfBlank(TGrossAmt)) + CDbl(dr("GROSSAMT").ToString())
    '        TNetAmt = CDbl(clsCommon.CheckIfBlank(TNetAmt)) + CDbl(NetAmt.Trim)
    '        TRateAfterDisc = CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)) + CDbl(RateAfterDisc.Trim)
    '        TTaxtAmt = CDbl(clsCommon.CheckIfBlank(TTaxtAmt)) + CDbl(TaxAmt.Trim)
    '            Next
    '            TotalQty = FormatNumber(CDbl(clsCommon.CheckIfBlank(TotalQty)), 2)
    '            TDiscAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TDiscAmt)), 2)
    '            TGrossAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TGrossAmt)), 2)
    '            TNetAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TNetAmt)), 2)
    '            TRateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)), 2)
    '            TTaxtAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TTaxtAmt)), 2)
    '            StrBody = strLineDetail.ToString() & vbCrLf
    '            StrBody = StrBody & strLine
    '            'TotalQtyLine = "Total Qty" & Space(3) & ":" & TotalQty
    '            'TotalQtyLine = getValueByKey("CLSCMP017") & Space(3) & ":" & TotalQty
    '            'StrBody = StrBody & TotalQtyLine & vbCrLf
    '            Dim TSubTotalAmt As String
    '            If Type <> "CMWAmt" Then
    '                If CDbl(clsCommon.CheckIfBlank(TDiscAmt)) > 0 Then
    '                    Dim TotalDisPercent As String = Math.Round((CDbl(clsCommon.CheckIfBlank(TDiscAmt)) / CDbl(clsCommon.CheckIfBlank(TGrossAmt))) * 100, 1).ToString()
    '                    TSubTotalAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TGrossAmt)) - CDbl(clsCommon.CheckIfBlank(TDiscAmt)))
    '                    TSubTotalAmt = TSubTotalAmt & " " & Currency
    '                    SubTotalLine = "Sub Total :".PadRight(LineLength - TSubTotalAmt.Length) & TSubTotalAmt.PadLeft(LineLength - TSubTotalAmt.Length)
    '                    TDiscAmt = TDiscAmt & " " & Currency
    '                    TotalDiscAmtLine = (String.Format(getValueByKey("CLSCMP019"), TotalDisPercent) & " :").PadRight(LineLength - 4 - TDiscAmt.Length) & TDiscAmt.PadLeft(LineLength - TDiscAmt.Length)
    '                End If
    '                TGrossAmt = TGrossAmt & " " & Currency
    '                TNetAmt = TNetAmt & " " & Currency
    '                TRateAfterDisc = TRateAfterDisc & " " & Currency
    '                TTaxtAmt = TTaxtAmt & " " & Currency
    '                'TotalGrossAmtLine = "Gross Amount".PadRight(LineLength - 16) & ":" & TGrossAmt.PadLeft(15)
    '                'TotalDiscAmtLine = "Discount".PadRight(LineLength - 16) & ":" & TDiscAmt.PadLeft(15)
    '                'NetAmtLine = "Total to Pay".PadRight(LineLength - 16) & ":" & TNetAmt.PadLeft(15)                
    '                TotalGrossAmtLine = "Net Amt :".PadRight(LineLength + 2 - TGrossAmt.Length) & TGrossAmt.PadLeft(LineLength - TGrossAmt.Length)
    '                'TotalTaxAmtLine = "MVAT 12.5%".PadRight(LineLength - 16) & ":" & TTaxtAmt.PadLeft(15)                  
    '                NetAmtLine = (getValueByKey("CLSCMP020") & " :").PadRight(LineLength - 1 - TNetAmt.Length) & TNetAmt.PadLeft(LineLength - TNetAmt.Length)
    '                If _TaxDetail = True Then
    '                    Dim taxDetailsForBill As DataTable = obj.GetTaxDetailsForBillNo(Sitecode, dtView.Rows(0)("Billno").ToString())
    '                    If taxDetailsForBill IsNot Nothing AndAlso taxDetailsForBill.Rows.Count > 0 Then
    '                        For Each row As DataRow In taxDetailsForBill.Rows
    '                            Dim taxValue As String = row("TaxVAlue").ToString()
    '                            taxValue = FormatNumber(CDbl(clsCommon.CheckIfBlank(taxValue)), 2)
    '                            taxValue = taxValue & " " & Currency
    '                            FooterTaxLine = FooterTaxLine & row("TaxName").PadRight(LineLength - 16) & ":" & taxValue.PadLeft(15) & vbCrLf
    '                        Next
    '                    End If
    '                End If
    '                TotalFooterTaxDetail = FooterTaxLine
    '                StrBody = StrBody & TotalGrossAmtLine & vbCrLf
    '                If TotalDiscAmtLine IsNot Nothing Then
    '                    StrBody = StrBody & TotalDiscAmtLine & vbCrLf
    '                End If
    '                If SubTotalLine IsNot Nothing Then
    '                    StrBody = StrBody & strLine.Substring(40)
    '                    StrBody = StrBody & SubTotalLine & vbCrLf
    '                End If
    '                StrBody = StrBody & TotalFooterTaxDetail
    '                StrBody = StrBody & strLine.Substring(40)
    '                StrBody = StrBody & NetAmtLine & vbCrLf
    '                StrBody = StrBody & strLine
    '                'TenderLine = "Payments:-" & vbCrLf
    '                TenderLine = getValueByKey("CLSCMP021") & vbCrLf
    '                Dim dtTender As DataTable = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
    '                For Each drTender As DataRow In dtTender.Rows
    '                    If UCase(obj.GetDefaultConfigValue(Sitecode, "CVInfoReqOnPrint")) = "FALSE" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)") Then
    '                        Continue For
    '                    End If
    '                    Dim tender As String = FormatNumber(CDbl(clsCommon.CheckIfBlank(drTender("AMOUNTTENDERED").ToString()))) & " " & drTender("CurrencyCode").ToString() & vbCrLf
    '                    Dim tenderName As String
    '                    'Added by Rohit
    '                    If drTender("CardNO").ToString() <> "" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(I)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(R)") Then
    '                        tenderName = drTender("TENDERHEADNAME").ToString() & "(" & drTender("CardNO").ToString() & ") "
    '                    Else
    '                        tenderName = drTender("TENDERHEADNAME").ToString()
    '                    End If
    '                    'End editing
    '                    If Not drTender("AMOUNTINCURRENCY") Is DBNull.Value AndAlso drTender("AMOUNTINCURRENCY") <> 0 Then '0000404
    '                        tender = FormatNumber(CDbl(drTender("AMOUNTINCURRENCY").ToString()), 2) & " " & drTender("CurrencyCode").ToString() & vbCrLf
    '                    End If
    '                    'tender = Format(CDbl(tender), "0.00")
    '                    TenderDetails = tenderName.PadRight(LineLength - tender.Length) & tender.PadLeft(LineLength - tender.Length) & vbCrLf
    '                    TenderLine = TenderLine & TenderDetails
    '                Next
    '                StrBody = StrBody & TenderLine
    '            End If

    '            'Company and Tin Info
    '            Dim NoOfReprints As String = IIf(IsDBNull(dtView.Rows(0)("NoofReprints")), String.Empty, dtView.Rows(0)("NoofReprints")).ToString()
    '            Dim CompanyName As String = obj.GetDefaultConfigValue(Sitecode, "CompanyName")
    '            If Not String.IsNullOrEmpty(CompanyName) Then
    '                StrCompanyInfo = CompanyName & vbCrLf
    '            End If
    '            StrCompanyInfo = StrCompanyInfo & getValueByKey("cashmemoprint.tin") & obj.GetTinNumberForASite(Sitecode) & vbCrLf
    '            StrCompanyInfo = StrCompanyInfo & StrTagLine
    '            'Dim remarks As String = dtView.Rows(0)("Remark")
    '            If Not IsDBNull(dtView.Rows(0)("Remark")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("Remark")) Then
    '                Dim remarks As String = dtView.Rows(0)("Remark")
    '                remarks = SplitString(getValueByKey("cashmemoprint.discountremark") & remarks, LineLength).ToString()
    '                StrCompanyInfo = StrCompanyInfo & remarks
    '            End If
    '            If Not String.IsNullOrEmpty(NoOfReprints) Then
    '                StrCompanyInfo = StrCompanyInfo & "Re-Print Number : " & NoOfReprints & vbCrLf
    '                StrCompanyInfo = StrCompanyInfo & "Re-Print Date : " & DateTime.Now.ToShortDateString() & vbCrLf
    '            End If
    '            'Company and Tin Info End

    '            Dim dtPromo As DataTable = dvItemDetail.ToTable(True, "PROMOTIONALMSGID", "PROMOMESS")
    '            For Each drTerms As DataRow In dtPromo.Select("", "PROMOTIONALMSGID", DataViewRowState.CurrentRows)
    '                If Not String.IsNullOrEmpty(drTerms("PROMOMESS").ToString()) Then
    '                    PromoMsgLine = PromoMsgLine & drTerms("PROMOMESS").ToString() & vbCrLf
    '                End If
    '            Next

    '            TotalPromotionalMsg = PromoMsgLine

    '            Dim dtTerms As DataTable = dvItemDetail.ToTable(True, "TNCSRNO", "Terms")
    '            For Each drTerms As DataRow In dtTerms.Select("", "TNCSRNO", DataViewRowState.CurrentRows)
    '                If Not String.IsNullOrEmpty(drTerms("Terms").ToString()) Then
    '                    TermsNcond = TermsNcond & drTerms("Terms").ToString() & vbCrLf
    '                End If
    '            Next

    '            'If dtView.Rows.Count() > 0 Then
    '            '    If dtView.Rows(0)("HDName").ToString() <> String.Empty Then
    '            '        strHomeDilery = "Delivery Date:-" & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf
    '            '        strHomeDilery = strHomeDilery & "Name:-" & dtView.Rows(0)("HDName").ToString() & vbCrLf
    '            '        strHomeDilery = strHomeDilery & "Address:-" & dtView.Rows(0)("HDAddress").ToString() & vbCrLf
    '            '        strHomeDilery = strHomeDilery & "TelNo:-" & dtView.Rows(0)("HDTelNo").ToString() & vbCrLf
    '            '        strHomeDilery = strHomeDilery & "Email:-" & dtView.Rows(0)("HDEmail").ToString() & vbCrLf
    '            '    End If
    '            'End If

    '            If dtView.Rows.Count() > 0 Then
    '                If dtView.Rows(0)("HDName").ToString() <> String.Empty Then
    '                    strHomeDilery = getValueByKey("CLSCMP022") & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf
    '                    strHomeDilery = strHomeDilery & getValueByKey("CLSCMP023") & dtView.Rows(0)("HDName").ToString() & vbCrLf
    '                    strHomeDilery = strHomeDilery & getValueByKey("CLSCMP024") & dtView.Rows(0)("HDAddress").ToString() & vbCrLf
    '                    strHomeDilery = strHomeDilery & getValueByKey("CLSCMP025") & dtView.Rows(0)("HDTelNo").ToString() & vbCrLf
    '                    strHomeDilery = strHomeDilery & getValueByKey("CLSCMP026") & dtView.Rows(0)("HDEmail").ToString() & vbCrLf
    '                End If
    '            End If


    '            StrFooter = StrFooter & TotalPromotionalMsg '& vbCrLf
    '            StrFooter = StrFooter & TermsNcond '& vbCrLf

    '            strPrintMsg = New StringBuilder()
    '            strPrintMsg.Append(StrHeader)
    '            'strPrintMsg.Append(strLine)
    '            strPrintMsg.Append(StrSubHeader)
    '            strPrintMsg.Append(strDblLine)
    '            strPrintMsg.Append(LineItemHeading)
    '            strPrintMsg.Append(strDblLine)
    '            strPrintMsg.Append(StrBody)
    '            strPrintMsg.Append(StrCompanyInfo)
    '            strPrintMsg.Append(strLine)
    '            strPrintMsg.Append(StrSubFooter)
    '            strPrintMsg.Append(StrFooter)
    '            strPrintMsg.Append(strHomeDilery) '& vbCrLf)
    '            If GiftMsg <> String.Empty Then
    '                GiftMsg = vbCrLf & GiftMsg
    '                GiftMsg = SplitString(GiftMsg).ToString()
    '                strPrintMsg.Append(GiftMsg & vbCrLf)
    '                strPrintMsg.Append("---------------------------")
    '            End If

    '            PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")

    '            If PrinterName = Nothing Then
    '                Exit Sub
    '            End If

    '            If _PrintPreview = True Then
    '                fnPrint(strPrintMsg.ToString(), "PRV")
    '            Else
    '                fnPrint(strPrintMsg.ToString(), "PRN")
    '            End If
    '            'Added for fiscal printer
    '            Try
    '                clsFiscalPrinting.fnFiscalPrint(strPrintMsg.ToString())
    '            Catch ex As Exception

    '            End Try
    '            'Added for fiscal printer

    '        Else
    '            errorMsg = getValueByKey("CLSCMP031")
    '        End If
    '    Catch ex As Exception
    '        errorMsg = ex.Message
    '    End Try
    'End Sub


    ''' <summary>
    ''' Printing Hold Cash Memo
    ''' </summary>
    ''' <Usedin>CashMemo</Usedin>
    ''' <Updatedby></Updatedby>
    ''' <Updatedon></Updatedon>
    ''' <remarks></remarks>
    Public Sub HoldCMPrint(ByVal SiteCode As String, ByVal Type As String, ByVal PrintBarcode As Boolean, ByRef ErrorMsg As String, Optional ByVal BarCodeType As String = "0")
        Try
            Dim strPrintMsg As StringBuilder
            Dim strLineDetail As New StringBuilder
            Dim StrHeader, StrSubHeader, StrBody, StrSubFooter, StrFooter, StrCashMemoLine, StrCashierLine, StrTerminalLine, StrCustomerLine As String
            Dim StrCMNo, StrCMDate, StrCMTime, StrCashier, StrTerminal As String
            Dim SiteLine, StrPrintHeaderLine, StrPrintFooterLine As String
            Dim LineItemHeading, StrLineItem As String
            Dim ItemCode, Desc, Qty, Rate, NetAmt As String
            Dim TotalQtyLine, TotalGrossAmtLine As String
            Dim TermsNcond As String
            Dim strLine As String
            Dim strDblLine As String
            Dim dtPrint As DataTable
            Dim dtView As New DataTable
            ErrorMsg = ""
            Dim obj As New SpectrumBL.clsCashMemo()
            dtView = obj.GetHoldDataForPrint(CashMemoNo, SiteCode)
            Dim IntTotItemQty As Integer = 0
            IntTotItemQty = IIf(dtView.Compute("SUM(QUANTITY)", "") Is DBNull.Value, 0, dtView.Compute("SUM(QUANTITY)", ""))

            Dim objPrint As New SpectrumBL.clsCommon
            dtPrint = objPrint.GetPrintingDetails(Type)
            If Not dtPrint Is Nothing Then
                Dim filter As String = ""
                Dim dv As New DataView(dtPrint, filter, "Sequenceno", DataViewRowState.CurrentRows)
                If _HeaderPrint = True Then
                    filter = "TOPBOTTOM='Top'"
                    dv.RowFilter = filter
                    For Each drview As DataRowView In dv
                        StrPrintHeaderLine = drview("ReceiptText").ToString()
                        StrSubHeader = StrSubHeader & StrPrintHeaderLine & vbNewLine
                    Next
                End If
                If _FooterPrint = True Then
                    filter = "TOPBOTTOM='Bottom'"
                    dv.RowFilter = filter
                    For Each drview As DataRowView In dv
                        StrPrintFooterLine = drview("ReceiptText").ToString()
                        StrFooter = StrFooter & StrPrintFooterLine & vbNewLine
                    Next
                End If
            End If
            If Not StrSubHeader Is Nothing AndAlso StrSubHeader.Length > 0 Then
                StrSubHeader = SplitString(StrSubHeader, LineLength).ToString()
            End If
            If Not StrFooter Is Nothing AndAlso StrFooter.Length > 0 Then
                StrFooter = SplitString(StrFooter, LineLength).ToString()
            End If

            If Not dtView Is Nothing Then
                strLine = "-".PadRight(LineLength, "-") & vbCrLf
                strDblLine = "=".PadRight(LineLength, "=") & vbCrLf

                'Dim SiteName, Site As String
                Dim SiteName, Site, StrSiteTelline, StrAdrressLine As String
                Dim TS_SiteName As StringBuilder
                'SiteName = "Store Name: " & dtView.Rows(0)("SITEOFFICIALNAME").ToString()
                'Site = "Code: " & dtView.Rows(0)("SITECODE").ToString()
                'SiteLine = SiteName.PadRight(LineLength - Site.Length) & Site

                'SiteName = getValueByKey("CLSCMP004") & " " & dtView.Rows(0)("SITEOFFICIALNAME").ToString()
                'Site = getValueByKey("CLSCMP005") & " " & dtView.Rows(0)("SITECODE").ToString()
                'SiteLine = SplitString(SiteName & Space(2) & Site, LineLength).ToString()

                SiteName = dtView.Rows(0)("SITEOFFICIALNAME").ToString()
                TS_SiteName = SplitString(SiteName)
                SiteName = TS_SiteName.ToString()
                SiteName = SiteName.PadLeft(LineLength / 2 + (SiteName.Length / 2))
                SiteLine = SiteName

                StrSiteTelline = getValueByKey("CLSCMP006") & " " & dtView.Rows(0)("TELNO").ToString()
                StrAdrressLine = getValueByKey("CLSCMP007") & " " & dtView.Rows(0)("ADDRESS").ToString()
                StrAdrressLine = SplitString(StrAdrressLine, LineLength).ToString()

                StrHeader = SiteLine & vbCrLf
                StrHeader = StrHeader & StrAdrressLine.Trim & vbCrLf
                StrHeader = StrHeader & StrSiteTelline & vbCrLf



                'StrHeader = SiteLine & vbCrLf
                StrHeader = StrHeader & strLine

                'StrCMNo = "Hold BillNo:" & dtView.Rows(0)("Billno").ToString()
                'StrCMDate = "Date:" & Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())

                StrCMNo = getValueByKey("CLSCMP027") & dtView.Rows(0)("Billno").ToString()
                StrCMDate = getValueByKey("CLSPV005") & Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())

                StrCashMemoLine = StrCMNo.PadRight(LineLength - StrCMDate.Length) & vbCrLf & StrCMDate & vbCrLf
                'StrCashier = "Cashier:" & dtView.Rows(0)("USERNAME").ToString()
                'StrCMTime = "Time:" & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")

                StrCashier = getValueByKey("CLSPV006") & dtView.Rows(0)("USERNAME").ToString()
                StrCMTime = getValueByKey("CLSCMP032") & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")

                StrTerminal = dtView.Rows(0)("TerminalId").ToString()
                StrCashierLine = StrCashier.PadRight(LineLength - StrCMTime.Length) & StrCMTime & vbCrLf
                'StrTerminalLine = "Issued At" & Space(2) & ":" & StrTerminal & vbCrLf
                'StrCustomerLine = "Customer Name:" & dtView.Rows(0)("RETRIEVEDFROMCUSTNAME").ToString() & vbCrLf

                StrTerminalLine = getValueByKey("CLSCMP028") & Space(2) & ":" & StrTerminal & vbCrLf
                StrCustomerLine = getValueByKey("MDCMFN002") & dtView.Rows(0)("RETRIEVEDFROMCUSTNAME").ToString() & vbCrLf

                'StrSubHeader = StrSubHeader & strLine
                StrHeader = StrHeader & StrCashMemoLine
                StrHeader = StrHeader & StrTerminalLine
                StrHeader = StrHeader & StrCashierLine
                'StrHeader = StrHeader & StrCustomerLine
                StrHeader = StrHeader & SplitString(StrCustomerLine).ToString() & vbCrLf
                StrHeader = StrHeader & strLine

                'LineItemHeading = "Item Code".PadRight(26) & "Item Name".PadRight(14) & vbCrLf
                'LineItemHeading = LineItemHeading & Space(3) & "Qty " & Space(5) & "Price " & Space(10) & "NetAmt" & vbCrLf

                'LineItemHeading = getValueByKey("CLSPSO020").PadRight(26) & getValueByKey("CLSCMP016").PadRight(14) & vbCrLf
                'LineItemHeading = LineItemHeading & Space(3) & getValueByKey("CLSPSO022") & " " & Space(5) & getValueByKey("CLSPSO023") & " " & Space(8) & getValueByKey("CLSCMP029") & " " & vbCrLf

                LineItemHeading = getValueByKey("CLSCMP016") & Space(13) & getValueByKey("CLSPSO022") & " " & Space(5) & "Amt " & vbCrLf
                Dim dvItemDetail As New DataView(dtView, "", "", DataViewRowState.CurrentRows)
                Dim DtUnique As DataTable = dvItemDetail.ToTable(True, "EAN", "ArticleName", "QUANTITY", "SELLINGPRICE", "NETAMOUNT")
                For Each dr As DataRow In DtUnique.Rows
                    ItemCode = dr("Ean").ToString()

                    If (dr("ArticleName").ToString().Length > 15) Then
                        Desc = dr("ArticleName").ToString().Substring(0, 15)
                    Else
                        Desc = dr("ArticleName").ToString()
                    End If

                    Qty = dr("Quantity").ToString()
                    Rate = dr("SellingPrice").ToString()
                    NetAmt = dr("NetAmount").ToString()
                    Dim i As Integer = 0
                    'For i = ItemCode.Length To 26
                    ItemCode = ItemCode
                    'Next
                    'For i = Desc.Length To 14
                    Desc = Desc
                    'Next
                    Qty = Format(CDbl(Qty), "0")
                    'For i = Qty.Length To 4
                    'Qty = " " & Qty
                    'Next
                    Rate = FormatNumber(CDbl(Rate), 2)
                    'Rate = FormatCurrency(Rate, 2)
                    'For i = Rate.Length To 8
                    '    Rate = " " & Rate
                    'Next

                    NetAmt = FormatNumber(CDbl(NetAmt), 2)
                    'For i = NetAmt.Length To 7
                    '    NetAmt = " " & NetAmt
                    'Next

                    ''StrLineItem = ItemCode.PadRight(26) & Desc & vbCrLf
                    ''StrLineItem = ItemCode & Space(2) & Desc & vbCrLf
                    'StrLineItem = ItemCode & Space(8) & Desc & vbCrLf
                    'StrLineItem = SplitString(StrLineItem, LineLength).ToString()
                    ''StrLineItem = StrLineItem & Qty.PadLeft(7) & Rate.PadLeft(10) & NetAmt.PadLeft(12) & vbCrLf
                    'StrLineItem = StrLineItem & Qty.PadLeft(7) & Rate.PadLeft(12) & NetAmt.PadLeft(18) & vbCrLf

                    If 28 - Desc.Length < Qty.Length Then
                        StrLineItem = Desc & vbCrLf

                        StrLineItem = StrLineItem & Qty.PadLeft(28) & NetAmt.PadLeft(11) & vbCrLf
                    Else
                        StrLineItem = Desc & Qty.PadLeft(28 - Desc.Length) & NetAmt.PadLeft(11) & vbCrLf
                    End If



                    strLineDetail.Append(StrLineItem)
                Next
                StrBody = strLineDetail.ToString() & vbCrLf
                StrBody = StrBody & strLine
                'TotalQtyLine = "Total Qty" & Space(3) & ":" & dtView.Rows(0)("TOTALITEMS").ToString()
                'TotalGrossAmtLine = "Gross Amt" & Space(3) & ":" & dtView.Rows(0)("GROSSAMT").ToString()

                TotalQtyLine = getValueByKey("CLSCMP017") & Space(3) & ":" & IntTotItemQty.ToString ' dtView.Rows(0)("TOTALITEMS").ToString()
                TotalGrossAmtLine = getValueByKey("CLSCMP030") & Space(3) & ":" & dtView.Rows(0)("GROSSAMT").ToString()
                StrBody = StrBody & TotalQtyLine & vbCrLf
                StrBody = StrBody & TotalGrossAmtLine & vbCrLf

                Dim dtTerms As DataTable = dvItemDetail.ToTable(True, "TNCSRNO", "Terms")
                For Each drTerms As DataRow In dtTerms.Select("", "TNCSRNO", DataViewRowState.CurrentRows)
                    TermsNcond = TermsNcond & drTerms("Terms").ToString() & vbCrLf
                Next

                StrFooter = StrFooter & TermsNcond & vbCrLf

                strPrintMsg = New StringBuilder()
                If PrintBarcode = True Then
                    strPrintMsg.Append(vbCrLf)
                    strPrintMsg.Append(vbCrLf)
                    strPrintMsg.Append(vbCrLf)
                    strPrintMsg.Append(vbCrLf)
                    strPrintMsg.Append(vbCrLf)
                End If
                strPrintMsg.Append(StrHeader)
                strPrintMsg.Append(StrSubHeader)
                strPrintMsg.Append(strDblLine)
                strPrintMsg.Append(LineItemHeading)
                strPrintMsg.Append(strDblLine)
                strPrintMsg.Append(StrBody)
                strPrintMsg.Append(strLine)
                strPrintMsg.Append(strLine)
                'strPrintMsg.Append(StrSubFooter)
                strPrintMsg.Append(StrFooter)

                If PrintBarcode = True Then
                    Dim s As C1BarCode = GetBarcode(CashMemoNo)
                    VarBarcode = s
                End If
                strPrintMsg.Append(" " & vbCrLf)
                strPrintMsg.Append(" " & vbCrLf)
                'fnPrint(strPrintMsg.ToString(), "PRN")

                PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Hold")

                If _PrintPreview = True Then
                    fnPrint(strPrintMsg.ToString(), "PRV")
                Else
                    fnPrint(strPrintMsg.ToString(), "PRN")
                End If
            Else
                ErrorMsg = getValueByKey("CLSCMP031")
            End If
        Catch ex As Exception
            ErrorMsg = ex.Message
        End Try
    End Sub

    Public Function PrintConfig(ByVal sitecode As String) As Boolean
        Try
            Dim dt As DataTable
            dt = GetDefaultSetting(sitecode, "CMS")
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If dr("FLDLABEL").ToString().ToUpper = "PRINTPREVIEWREQUIRED".ToUpper() Then
                        _PrintPreview = dr("FLDVALUE")
                    End If
                Next
            End If

            'dt = GetDefaultSetting(SiteCode, "0000")
            'If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
            '    For Each dr As DataRow In dt.Rows
            '        If dr("FLDLABEL").ToString().ToUpper = "ROUNDED_OFF_TO".ToUpper() Then
            '            DecimalDigits = dr("FLDVALUE")
            '        ElseIf dr("FLDLABEL").ToString().ToUpper = "IsWelcomeMsgPrint".ToUpper() Then
            '            IsWelComeMessagePrint = dr("FLDVALUE").ToString

            '        End If
            '    Next
            'End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '#Region "Hope india bill print mail"
    '    ' added by khurao adil on 23-12-2016 
    Private Shared Function customCertValidation(ByVal sender As Object, _
                                              ByVal cert As X509Certificate, _
                                              ByVal chain As X509Chain, _
                                              ByVal errors As SslPolicyErrors) As Boolean
        Return True
    End Function
    Public Sub DoStuff()
        'CALL THIS BEFORE ANY HTTPS CALLS THAT WILL FAIL WITH CERT ERROR
        ServicePointManager.ServerCertificateValidationCallback = _
            New System.Net.Security.RemoteCertificateValidationCallback(AddressOf customCertValidation)
        '
        '
        '
    End Sub
    Public Function SendBillToMail(ByVal Path As String, ByVal BillNo As String, ByVal SiteCode As String, Optional BillType As String = "CM") 'vipul
        Dim IsMailSendSuccess As Boolean = False
        Dim NanoId As Long = (DateTime.Now - New DateTime(1970, 1, 1)).TotalMilliseconds
        Dim objclscomn As New clsCommon
        Try

            Dim Userlist, MailMsg As String
            Dim obj As New clsCommon
            Dim dtMail As New DataSet
            dtMail = obj.GetInvoiceMailSendData(BillNo, SiteCode)
            If Not dtMail Is Nothing Then
                If dtMail.Tables(0).Rows.Count > 0 Then
                    If Not String.IsNullOrEmpty(dtMail.Tables(0).Rows(0)(0)) Then
                        Userlist = dtMail.Tables(0).Rows(0)(0).ToString
                        MailMsg = dtMail.Tables(1).Rows(0)(0).ToString
                        DoStuff()
                        Dim readerPath As New StreamReader(Path)
                        Using reasder As New StreamReader(Path)
                            Dim NetworkCred As New NetworkCredential()
                            Dim dt As New DataTable
                            Dim host, port As String
                            Dim objClsDayClose As New clsDayClose
                            dt = objClsDayClose.GetUsernamePassword()
                            For Each row In dt.Rows
                                If row("FldLabel").ToString() = "SMTP.Password" Then
                                    NetworkCred.Password = row("FldValue").ToString()
                                ElseIf row("FldLabel").ToString() = "SMTP.UserName" Then
                                    NetworkCred.UserName = row("FldValue").ToString()
                                ElseIf row("FldLabel").ToString() = "SMTP.HOST" Then
                                    host = row("FldValue").ToString()
                                ElseIf row("FldLabel").ToString() = "SMTP.IP" Then
                                    port = row("FldValue").ToString()
                                End If
                            Next
                            Dim mm As New MailMessage(NetworkCred.UserName, Userlist)
                            Dim sitename As String
                            sitename = objclscomn.GetSiteName(SiteCode)
                            mm.Subject = "Invoice No. #" & BillNo & ""
                            Dim msg As New StringBuilder
                            msg.Append(MailMsg)
                            mm.Body = msg.ToString()
                            mm.Attachments.Add(New Attachment(readerPath.BaseStream, "FinalReceipt_" & BillNo & ".pdf"))
                            mm.IsBodyHtml = False
                            Dim smtp As New SmtpClient()
                            smtp.Host = host
                            smtp.EnableSsl = True
                            smtp.UseDefaultCredentials = True
                            smtp.Credentials = NetworkCred
                            smtp.Port = port
                            If EnableMailReSend Then
                                objclscomn.insertMailFailedDtl(SiteCode, BillNo, DocumentType, NanoId, Path, Userlist, mm.Subject, IsMailSend:=False, UserId:=UserID, TerminalId:=Terminalid, MailBody:=msg.ToString())
                            End If
                            smtp.Send(mm)
                            IsMailSendSuccess = True
                            If EnableMailReSend Then
                                objclscomn.UpdateMailFailedDtl(SiteCode, BillNo, DocumentType, NanoId, IsMailSendSuccess, "Succes", UserID)
                            End If
                        End Using
                    End If
                End If
            End If
        Catch ex As Exception
            If EnableMailReSend Then
                If IsMailSendSuccess = False Then
                    objclscomn.UpdateMailFailedDtl(SiteCode, BillNo, DocumentType, NanoId, IsMailSendSuccess, ex.ToString(), UserID)
                End If
            End If
            LogException(ex)
        End Try
    End Function
    Public Function FailedMailReSend(ByVal SiteCode As String, ByVal BillNo As String, ByVal Path As String, ByVal Userlist As String, ByVal NanoID As String, ByVal DocumentTypeForMailResend As String, Optional ByVal MailSubject As String = "", Optional ByVal MailMsg As String = "", Optional ByVal CC As String = "", Optional ByVal BCC As String = "") 'vipul
        Dim IsMailSendSuccess As Boolean = False
        Dim objclscomn As New clsCommon
        Try

            DoStuff()

            Dim NetworkCred As New NetworkCredential()
            Dim dt As New DataTable
            Dim host, port As String
            Dim objClsDayClose As New clsDayClose
            dt = objClsDayClose.GetUsernamePassword()
            For Each row In dt.Rows
                If row("FldLabel").ToString() = "SMTP.Password" Then
                    NetworkCred.Password = row("FldValue").ToString()
                ElseIf row("FldLabel").ToString() = "SMTP.UserName" Then
                    NetworkCred.UserName = row("FldValue").ToString()
                ElseIf row("FldLabel").ToString() = "SMTP.HOST" Then
                    host = row("FldValue").ToString()
                ElseIf row("FldLabel").ToString() = "SMTP.IP" Then
                    port = row("FldValue").ToString()
                End If
            Next
            Dim mm As New MailMessage(NetworkCred.UserName, Userlist)
            Dim sitename As String
            sitename = objclscomn.GetSiteName(SiteCode)
            If Not String.IsNullOrEmpty(CC) Then
                mm.CC.Add(CC)
            End If
            If Not String.IsNullOrEmpty(BCC) Then
                mm.CC.Add(BCC)
            End If
            mm.Subject = MailSubject
            Dim msg As New StringBuilder
            msg.Append(MailMsg)
            mm.Body = msg.ToString()
            Dim NoOfMail() As String = Split(Path, ",")
            Dim IntNoOFmail As Integer = NoOfMail.Length
            Dim DocCountApend As Integer = 0
            For Each Dr As String In NoOfMail
                Dim readerPath As New StreamReader(Dr)
                mm.Attachments.Add(New Attachment(readerPath.BaseStream, NoOfMail(0).Substring(NoOfMail(0).ToString.LastIndexOf("\") + 1, NoOfMail(0).Length - NoOfMail(0).ToString.LastIndexOf("\") - 1)))
                DocCountApend = DocCountApend + 1
            Next
            mm.IsBodyHtml = False
            Dim smtp As New SmtpClient()
            smtp.Host = host
            smtp.EnableSsl = True
            smtp.UseDefaultCredentials = True
            smtp.Credentials = NetworkCred
            smtp.Port = port
            smtp.Send(mm)
            IsMailSendSuccess = True
            If IsMailSendSuccess Then
                objclscomn.UpdateMailFailedDtl(SiteCode, BillNo, DocumentTypeForMailResend, NanoID, IsMailSendSuccess, "Succes", UserID)
            End If
        Catch ex As Exception
            If IsMailSendSuccess = False Then
                objclscomn.UpdateMailFailedDtl(SiteCode, BillNo, DocumentTypeForMailResend, NanoID, IsMailSendSuccess, ex.ToString(), UserID)
            End If
            LogException(ex)
        End Try
    End Function
    Private Function SendInvoiceOnMail(ByVal path As String, ByVal dtView As DataTable) 'vipul
        Try
            If IsInvoiceSendOnMailRequired = True Then
                If Not dtView Is Nothing Then
                    Dim _billNo As String = dtView.Rows(0)("BILLNO")
                    Dim _siteCode As String = dtView.Rows(0)("SITECODE")
                    SendBillToMail(path, _billNo, _siteCode)
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
#Region "SendSMS"
    Dim request As HttpWebRequest
    Dim response As HttpWebResponse = Nothing
    Public Function SendCMInvoiceSMS(ByVal strUrl As String, ByVal Mobileno As String, ByVal msgStr As String) As Boolean
        Try
            If Trim(Mobileno) <> String.Empty AndAlso Trim(msgStr) <> String.Empty Then
                Dim strResp As String
                strUrl = strUrl.Replace("$number", Mobileno)
                strUrl = strUrl.Replace("$msg", msgStr)
                strUrl = strUrl
                request = DirectCast(WebRequest.Create(strUrl), HttpWebRequest)
                response = DirectCast(request.GetResponse(), HttpWebResponse)

            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Function SendSMSForCM(ByVal dtView As DataTable) 'vipul
        Try
            If SendInvoiceSMS = True Then
                Dim obj As New clsCommon
                Dim _billNo, _siteCode, Mobileno, SMSUrl, SMSTemplate As String
                _billNo = dtView.Rows(0)("BILLNO")
                _siteCode = dtView.Rows(0)("SITECODE")
                Dim Dt = obj.GetSMSSendData(_billNo, _siteCode)

                If Not Dt.Tables(1) Is Nothing Then
                    If Not String.IsNullOrEmpty(Dt.Tables(1).Rows(0)(0).ToString) Then
                        SendCMInvoiceSMS(Dt.Tables(1).Rows(0)(0).ToString, Dt.Tables(0).Rows(0)(0).ToString, Dt.Tables(2).Rows(0)(0).ToString)
                    End If
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
#End Region
    '    Private Function SendBillToMail(ByVal Path As String, ByVal Userlist As String, ByVal BillNo As String, ByVal SiteCode As String, ByVal DayOpenDate As Date)
    '        Try
    '            DoStuff()
    '            Dim readerPath As New StreamReader(Path)
    '            Using reasder As New StreamReader(Path)
    '                Dim NetworkCred As New NetworkCredential()
    '                Dim dt As New DataTable
    '                Dim host, port As String
    '                Dim objClsDayClose As New clsDayClose
    '                dt = objClsDayClose.GetUsernamePassword()
    '                For Each row In dt.Rows
    '                    If row("FldLabel").ToString() = "SMTP.Password" Then
    '                        NetworkCred.Password = row("FldValue").ToString()
    '                    ElseIf row("FldLabel").ToString() = "SMTP.UserName" Then
    '                        NetworkCred.UserName = row("FldValue").ToString()
    '                    ElseIf row("FldLabel").ToString() = "SMTP.HOST" Then
    '                        host = row("FldValue").ToString()
    '                    ElseIf row("FldLabel").ToString() = "SMTP.IP" Then
    '                        port = row("FldValue").ToString()
    '                    End If
    '                Next
    '                Dim mm As New MailMessage(NetworkCred.UserName, Userlist)
    '                Dim objclscomn As New clsCommon
    '                Dim sitename As String
    '                sitename = objclscomn.GetSiteName(SiteCode)
    '                mm.Subject = "final recipt for Bill no." & BillNo & " on " & DayOpenDate.ToString("dd-MMM-yyyy") & " from " & sitename & ":"
    '                Dim msg As New StringBuilder
    '                Dim str As String
    '                str = "Dear user,"
    '                msg.Append(str + vbCrLf)
    '                Dim str1 As String
    '                str1 = "Please find the attached final recipt for Bill no " & BillNo & " on " & DayOpenDate.ToString("dd-MMM-yyyy") & " from " & sitename & ":"
    '                msg.Append(str1 + vbCrLf)
    '                Dim str2 As String
    '                str2 = "Regards,"
    '                msg.Append(str2 + vbCrLf)
    '                mm.Body = msg.ToString()

    '                'mm.Attachments.Add(New Attachment(New MemoryStream(bytes), "D:\DayCloseReports\BankReport_Vertak Nagar_02-03-2015.pdf"))
    '                Dim ReportName As String = Path.Replace("D:\DayCloseReports\", "")
    '                mm.Attachments.Add(New Attachment(readerPath.BaseStream, ReportName))
    '                mm.IsBodyHtml = False
    '                Dim smtp As New SmtpClient()
    '                smtp.Host = host
    '                'smtp.Host = "smtp.gmail.com"
    '                smtp.EnableSsl = True
    '                smtp.UseDefaultCredentials = True
    '                smtp.Credentials = NetworkCred
    '                smtp.Port = port
    '                ' smtp.Port = 587
    '                smtp.Send(mm)
    '            End Using
    '        Catch ex As Exception
    '            LogException(ex)
    '        End Try
    '    End Function
    '#End Region
    ' ''' <summary>
    ' ''' Printing Credit Voucher
    ' ''' </summary>
    ' ''' <param name="type">document Type</param>
    ' ''' <param name="Amt"></param>
    ' ''' <remarks></remarks>
    'Public Sub PrintVoucher(ByVal type As String, ByVal Amt As Double, ByVal VoucherText As String, ByVal SiteCode As String, ByRef ErrorMsg As String)
    '    Try
    '        Dim strPrintMsg As StringBuilder
    '        Dim strLineDetail As New StringBuilder
    '        Dim StrHeader, StrSubHeader, StrBody, StrSubFooter, StrFooter, StrCashMemoLine, StrCashierLine As String
    '        Dim StrCMNo, StrCMDate, StrCMTime, StrCashier As String
    '        Dim TermsNcond, StrPrintHeaderLine, StrPrintFooterLine As String
    '        Dim strLine As String
    '        Dim strDblLine As String
    '        Dim VoucherNO As String
    '        Dim VoucherValiddate As DateTime
    '        Dim dtView, dtPrint As DataTable
    '        Dim obj As New SpectrumBL.clsCashMemo()
    '        StrCashMemoLine = ""
    '        dtView = obj.GetVoucherData(CashMemoNo, SiteCode, VoucherNO, VoucherValiddate)
    '        Dim objPrint As New SpectrumBL.clsCommon
    '        dtPrint = objPrint.GetPrintingDetails(type)
    '        If Not dtPrint Is Nothing Then
    '            Dim filter As String = ""
    '            Dim dv As New DataView(dtPrint, filter, "Sequenceno", DataViewRowState.CurrentRows)
    '            If _HeaderPrint = True Then
    '                filter = "TOPBOTTOM='Top'"
    '                dv.RowFilter = filter
    '                For Each drview As DataRowView In dv
    '                    StrPrintHeaderLine = drview("ReceiptText").ToString()
    '                    StrSubHeader = StrSubHeader & StrPrintHeaderLine & vbNewLine
    '                Next
    '            End If
    '            If _FooterPrint = True Then
    '                filter = "TOPBOTTOM='Bottom'"
    '                dv.RowFilter = filter
    '                For Each drview As DataRowView In dv
    '                    StrPrintFooterLine = drview("ReceiptText").ToString()
    '                    StrFooter = StrFooter & StrPrintFooterLine & vbNewLine
    '                Next
    '            End If
    '        End If
    '        If Not StrSubHeader Is Nothing AndAlso StrSubHeader.Length > 0 Then
    '            StrSubHeader = SplitString(StrSubHeader, LineLength).ToString()
    '        End If
    '        If Not StrFooter Is Nothing AndAlso StrFooter.Length > 0 Then
    '            StrFooter = SplitString(StrFooter, LineLength).ToString()
    '        End If
    '        If Not dtView Is Nothing Then
    '            strLine = "----------------------------------------" & vbCrLf
    '            strDblLine = "========================================" & vbCrLf
    '            If dtView.Rows.Count > 0 Then
    '                StrCMNo = dtView.Rows(0)("Billno").ToString()
    '                StrCMDate = Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())
    '                'StrCashMemoLine = "Credit Note/Voucher No:" & StrCMNo & Space(15 - StrCMNo.Length) & "Date:" & StrCMDate & vbCrLf
    '                StrCashier = dtView.Rows(0)("USERNAME").ToString()
    '                StrCMTime = Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")
    '            End If


    '            StrCashMemoLine = "Credit Voucher No:" & VoucherNO.ToString() & Space(15 - VoucherNO.Length) & "Voucher Valid Date:" & VoucherValiddate.ToShortDateString() & vbCrLf
    '            StrCashierLine = "Cashier:" & StrCashier & Space(5) & StrCMTime & vbNewLine


    '            StrHeader = StrHeader & StrCashMemoLine
    '            StrHeader = StrHeader & StrCashierLine

    '            Dim dvItemDetail As New DataView(dtView, "", "", DataViewRowState.CurrentRows)

    '            StrBody = VoucherText & Space(2) & Amt & vbNewLine
    '            Dim dtRefBill As DataTable = dvItemDetail.ToTable(True, "REFBILL", "REFBILLDATE")
    '            StrBody = StrBody & "Issued against " & vbCrLf
    '            For Each row As DataRow In dtRefBill.Rows
    '                StrBody = StrBody & "C.M. No:" & row("refBill").ToString() & Space(2) & "RefBillDate:" & row("REFBILLDATE").ToString() & vbNewLine
    '            Next

    '            'StrBody = StrBody & "Valid For  " & clsDefaultConfiguration.VoucherValidatedDays & " Days" & vbCrLf
    '            StrBody = StrBody & "Valid For  " & DateDiff(DateInterval.Day, Now, VoucherValiddate) & " Days" & vbCrLf
    '            StrBody = StrBody & "From issue date if stamped and signed." & vbCrLf & vbCrLf
    '            StrBody = StrBody & Space(10) & "-------------"

    '            Dim dtTerms As DataTable = dvItemDetail.ToTable(True, "TNCSRNO", "DESCRIPTION")
    '            For Each drTerms As DataRow In dtTerms.Select("", "TNCSRNO", DataViewRowState.CurrentRows)
    '                TermsNcond = TermsNcond & drTerms("DESCRIPTION").ToString() & vbCrLf
    '            Next

    '            StrFooter = StrFooter & TermsNcond & vbCrLf

    '            strPrintMsg = New StringBuilder()
    '            strPrintMsg.Append(StrHeader)
    '            strPrintMsg.Append(strLine)
    '            strPrintMsg.Append(StrSubHeader)

    '            strPrintMsg.Append(StrBody)

    '            strPrintMsg.Append(StrSubFooter)
    '            strPrintMsg.Append(StrFooter)

    '            'fnPrint(strPrintMsg.ToString(), "PRN")
    '            PrinterName = SetPrinterName(dtPrinterInfo1, "Voucher", "")

    '            If _PrintPreview = True Then
    '                fnPrint(strPrintMsg.ToString(), "PRV")
    '            Else
    '                fnPrint(strPrintMsg.ToString(), "PRN")
    '            End If
    '        Else
    '            ErrorMsg = "Unable to retrive Data"
    '        End If
    '    Catch ex As Exception
    '        ErrorMsg = ex.Message
    '    End Try
    'End Sub
    'Public Sub PrintGiftVoucher(ByVal VoucherNo As String, ByVal Amt As Double, ByVal ValidUpto As DateTime, ByRef ErrorMsg As String, Optional ByVal validDays As Int32 = 0)
    '    Try
    '        Dim strPrintMsg As StringBuilder
    '        Dim strLineDetail As New StringBuilder
    '        Dim StrHeader, StrBody, StrFooter As String
    '        Dim TermsNcond, StrPrintHeaderLine, StrPrintFooterLine As String
    '        Dim strLine As String
    '        Dim strDblLine As String

    '        StrHeader = "Gift Voucher No:- " & VoucherNo & vbCrLf
    '        strLine = "----------------------------------------" & vbCrLf
    '        strDblLine = "========================================" & vbCrLf
    '        StrBody = "Amount : " & Amt & vbCrLf
    '        StrFooter = "Valid Upto:-" & ValidUpto & Space(5) & "Valid days:-" & validDays & vbCrLf
    '        StrFooter = StrFooter & "Valid on all Store's"

    '        strPrintMsg = New StringBuilder()
    '        strPrintMsg.Append(StrHeader)
    '        strPrintMsg.Append(strLine)
    '        strPrintMsg.Append(StrBody)
    '        strPrintMsg.Append(strLine)
    '        strPrintMsg.Append(StrFooter)
    '        Dim s As C1BarCode = GetBarcode(VoucherNo)
    '        VarBarcode = s
    '        'fnPrint(strPrintMsg.ToString(), "PRN")
    '        PrinterName = SetPrinterName(dtPrinterInfo1, "Voucher", "")

    '        If _PrintPreview = True Then
    '            fnPrint(strPrintMsg.ToString(), "PRV")
    '        Else
    '            fnPrint(strPrintMsg.ToString(), "PRN")
    '        End If
    '    Catch ex As Exception
    '        ErrorMsg = ex.Message
    '    End Try
    'End Sub


#End Region

    Private TemplatefolderPath As String = Path.Combine(Environment.CurrentDirectory, "Templates")
    Private xmlfilPath As String = String.Empty
    Private Const billType As String = "CMInvc"
    Private Const BillPrintFileExtension As String = ".prn"

    Dim dsCashMemoDetails As DataSet
    Dim dsTemplateData = New DataSet("PrintTemplate")

    Private fieldLength As Integer = 25
    Private fieldAlignment As String = "Left"
    Private isCropText As Boolean
    Private DrawLine As String = StrDup(PrintLineLength, Convert.ToChar(DrawLineTextCode))

    Dim cashMemoPrintDetails As New clsCashMemoTemplatePrinting
    Dim propertyValue As Object
    Dim fieldValue As String = String.Empty

    Public Sub PrintTemplateCashMemoBillDetails(ByVal CashMemoNo As String, ByVal SiteCode As String, ByVal CurrencyText As String, ByVal CompanyName As String, Optional ByVal dsCashMemo As DataSet = Nothing, Optional ByVal IsBillReprint As Boolean = False, Optional ByVal ReprintReason As String = "")

        Dim context As IDictionary = New Hashtable()
        Dim dsCashMemoMaster As DataSet = New DataSet
        Dim articleCodeList As String = String.Empty
        Dim taxCodeList As String = String.Empty

        Try
            'Read Xml template data for print alignment
            xmlfilPath = Path.Combine(TemplatefolderPath, TemplateXmlName)

            If (File.Exists(xmlfilPath)) Then
                dsTemplateData.ReadXml(xmlfilPath)

                If (dsTemplateData Is Nothing) Then
                    Return
                End If
            End If

            'Get CashMemo Bill Details for printing

            dsCashMemoDetails = New DataSet

            If (clsCashMemo.dsCashMemoPrinting IsNot Nothing AndAlso clsCashMemo.dsCashMemoPrinting.Tables.Count > 0) Then
                dsCashMemoDetails = clsCashMemo.dsCashMemoPrinting.Copy()
                For tableIndex = 0 To dsCashMemoDetails.Tables.Count - 1
                    If (String.Equals(dsCashMemoDetails.Tables(tableIndex).TableName.ToUpper, "CASHMEMOHDR")) Then
                        dsCashMemoDetails.Tables(tableIndex).TableName = "CashMemoHeader"
                    ElseIf (String.Equals(dsCashMemoDetails.Tables(tableIndex).TableName.ToUpper, "CASHMEMODTL")) Then
                        dsCashMemoDetails.Tables(tableIndex).TableName = "CashMemoDetails"
                        For rowIndex = 0 To dsCashMemoDetails.Tables(tableIndex).Rows.Count - 1
                            Dim mrp = Val(clsCashMemo.GetMRPforEAN(dsCashMemoDetails.Tables(tableIndex).Rows(rowIndex)("EAN").ToString()))
                            If (articleCodeList.IndexOf(dsCashMemoDetails.Tables(tableIndex).Rows(rowIndex)("ArticleCode").ToString()) < 0) Then
                                articleCodeList += String.Format("''{0}'',", dsCashMemoDetails.Tables(tableIndex).Rows(rowIndex)("ArticleCode").ToString())
                            End If
                        Next
                    ElseIf (String.Equals(dsCashMemoDetails.Tables(tableIndex).TableName.ToUpper, "CASHMEMORECEIPT")) Then
                        dsCashMemoDetails.Tables(tableIndex).TableName = "CashMemoReceipts"

                    ElseIf (String.Equals(dsCashMemoDetails.Tables(tableIndex).TableName.ToUpper, "CASHMEMOTAXDTLS")) Then
                        dsCashMemoDetails.Tables(tableIndex).TableName = "CashMemoTaxDetails"

                        For rowIndex = 0 To dsCashMemoDetails.Tables(tableIndex).Rows.Count - 1
                            If (taxCodeList.IndexOf(dsCashMemoDetails.Tables(tableIndex).Rows(rowIndex)("TaxLabel").ToString()) < 0) Then
                                taxCodeList += String.Format("''{0}'',", dsCashMemoDetails.Tables(tableIndex).Rows(rowIndex)("TaxLabel").ToString())
                            End If
                        Next

                    ElseIf (String.Equals(dsCashMemoDetails.Tables(tableIndex).TableName.ToUpper, "CASHMEMOCOMBODTL")) Then
                        dsCashMemoDetails.Tables(tableIndex).TableName = "CashMemoComboDetails"

                        For rowIndex = 0 To dsCashMemoDetails.Tables(tableIndex).Rows.Count - 1
                            If (articleCodeList.IndexOf(dsCashMemoDetails.Tables(tableIndex).Rows(rowIndex)("ArticleCode").ToString()) < 0) Then
                                articleCodeList += String.Format("''{0}'',", dsCashMemoDetails.Tables(tableIndex).Rows(rowIndex)("ArticleCode").ToString())
                            End If
                        Next
                    End If
                Next

                'articleCodeList = IIf(String.IsNullOrEmpty(articleCodeList), String.Empty, articleCodeList.Substring(0, articleCodeList.Length - 1))
                If (Not String.IsNullOrEmpty(articleCodeList)) Then
                    articleCodeList = articleCodeList.Substring(0, articleCodeList.Length - 1)
                End If

                If Not (String.IsNullOrEmpty(taxCodeList)) Then
                    taxCodeList = taxCodeList.Substring(0, taxCodeList.Length - 1)
                    'taxCodeList = IIf(String.IsNullOrEmpty(taxCodeList), String.Empty,taxCodeList.Substring(0, taxCodeList.Length - 1) )
                End If

            End If

            dsCashMemoMaster = clsCashMemo.GetCashMemoBillDetails(CashMemoNo, SiteCode, billType, IsBillReprint, String.Format("'{0}'", articleCodeList), String.Format("'{0}'", taxCodeList))

            For tableIndex = 0 To dsCashMemoMaster.Tables.Count - 1
                dsCashMemoDetails.Tables.Add(dsCashMemoMaster.Tables(tableIndex).Copy)
            Next

            '---- Changes Done By Mahesh Fetch MRP also
            Dim TotalNetAmount As Double = 0.0
            Dim TotalAmount As Double = 0.0
            For tableIndex = 0 To dsCashMemoDetails.Tables.Count - 1
                If (String.Equals(dsCashMemoDetails.Tables(tableIndex).TableName.ToUpper, "CASHMEMOHEADER")) Then
                    TotalNetAmount = Val(dsCashMemoDetails.Tables(tableIndex).Rows(0)("NetAmt"))
                ElseIf (String.Equals(dsCashMemoDetails.Tables(tableIndex).TableName.ToUpper, "CASHMEMODETAILS")) Then
                    '---- Changes Done By Mahesh Fetch MRP also
                    dsCashMemoDetails.Tables(tableIndex).Columns.Add("MRP")
                    For rowIndex = 0 To dsCashMemoDetails.Tables(tableIndex).Rows.Count - 1
                        Dim mrp = Val(clsCashMemo.GetMRPforEAN(dsCashMemoDetails.Tables(tableIndex).Rows(rowIndex)("EAN").ToString()))
                        dsCashMemoDetails.Tables(tableIndex).Rows(rowIndex)("MRP") = mrp
                        TotalAmount = TotalAmount + mrp * dsCashMemoDetails.Tables(tableIndex).Rows(rowIndex)("Quantity")
                    Next
                    '------ Calculating TotalNetSaving = Sum(MRP * Qty)- NetAmount ---
                    TotralNetSaving = IIf(Val(TotalAmount - TotalNetAmount) = 0, "0.00", (TotalAmount - TotalNetAmount).ToString("##,##.00"))
                End If
            Next

            If (Not IsBillReprint) Then

                dsCashMemoDetails.Tables("CashMemoDetails").Columns.Add(New DataColumn("ArticleName", Type.GetType("System.String")))
                If dsCashMemoDetails.Tables("CashMemoComboDetails") IsNot Nothing AndAlso dsCashMemoDetails.Tables("CashMemoComboDetails").Rows.Count > 0 Then
                    dsCashMemoDetails.Tables("CashMemoComboDetails").Columns.Add(New DataColumn("ArticleName", Type.GetType("System.String")))
                End If

                dsCashMemoDetails.Tables("CashMemoTaxDetails").Columns.Add(New DataColumn("TaxType", Type.GetType("System.String")))
                dsCashMemoDetails.Tables("CashMemoTaxDetails").Columns.Add(New DataColumn("TaxName", Type.GetType("System.String")))

                For rowIndex = 0 To dsCashMemoDetails.Tables("CashMemoDetails").Rows.Count - 1
                    Dim drArticleDetails As DataRow = dsCashMemoMaster.Tables("ArticleMaster").Select(String.Format("ArticleCode='{0}'", dsCashMemoDetails.Tables("CashMemoDetails").Rows(rowIndex)("ArticleCode"))).FirstOrDefault()

                    If (drArticleDetails IsNot Nothing) Then
                        dsCashMemoDetails.Tables("CashMemoDetails").Rows(rowIndex)("ArticleName") = drArticleDetails("ArticleName")
                    End If
                Next
                If dsCashMemoDetails.Tables("CashMemoComboDetails") IsNot Nothing AndAlso dsCashMemoDetails.Tables("CashMemoComboDetails").Rows.Count > 0 Then
                    For rowIndex = 0 To dsCashMemoDetails.Tables("CashMemoComboDetails").Rows.Count - 1
                        Dim drArticleDetails As DataRow = dsCashMemoMaster.Tables("ArticleMaster").Select(String.Format("ArticleCode='{0}'", dsCashMemoDetails.Tables("CashMemoComboDetails").Rows(rowIndex)("ArticleCode"))).FirstOrDefault()

                        If (drArticleDetails IsNot Nothing) Then
                            dsCashMemoDetails.Tables("CashMemoComboDetails").Rows(rowIndex)("ArticleName") = drArticleDetails("ArticleName")
                        End If
                    Next
                End If


                For rowIndex = 0 To dsCashMemoDetails.Tables("CashMemoTaxDetails").Rows.Count - 1
                    Dim drTaxDetails As DataRow = dsCashMemoMaster.Tables("TaxMaster").Select(String.Format("TaxCode='{0}'", dsCashMemoDetails.Tables("CashMemoTaxDetails").Rows(rowIndex)("TaxLabel"))).FirstOrDefault()

                    If (drTaxDetails IsNot Nothing) Then
                        dsCashMemoDetails.Tables("CashMemoTaxDetails").Rows(rowIndex)("TaxType") = drTaxDetails("TaxType")
                        dsCashMemoDetails.Tables("CashMemoTaxDetails").Rows(rowIndex)("TaxName") = drTaxDetails("TaxName")
                    End If
                Next

            End If

            If (dsCashMemoDetails.Tables("PrintingDetails") IsNot Nothing AndAlso dsCashMemoDetails.Tables("PrintingDetails").Rows.Count > 0) Then

                Dim headerMessage = String.Empty, footerMessage = String.Empty, welcomeMessage = String.Empty,
                    promotionalMessage = String.Empty, taxMessage As String = String.Empty

                For Each drMessage As DataRow In dsCashMemoDetails.Tables("PrintingDetails").Select("TOPBOTTOM='Top'")
                    headerMessage += IIf(drMessage("ReceiptText") IsNot DBNull.Value, drMessage("ReceiptText").ToString().Trim(), String.Empty) + Space(1)
                Next
                cashMemoPrintDetails.HeaderMessage = SplitString(headerMessage, LineLength).ToString().TrimEnd()

                For Each drMessage As DataRow In dsCashMemoDetails.Tables("PrintingDetails").Select("TOPBOTTOM='Bottom'")
                    footerMessage = IIf(drMessage("ReceiptText") IsNot DBNull.Value, drMessage("ReceiptText").ToString().Trim(), String.Empty)
                    cashMemoPrintDetails.FooterMessage += SplitString(footerMessage, LineLength).ToString().TrimEnd() + vbCrLf
                Next
                cashMemoPrintDetails.FooterMessage = cashMemoPrintDetails.FooterMessage.TrimEnd()

                For Each drMessage As DataRow In dsCashMemoDetails.Tables("PrintingDetails").Select("TOPBOTTOM='Welcome'")
                    welcomeMessage += IIf(drMessage("ReceiptText") IsNot DBNull.Value, drMessage("ReceiptText").ToString().Trim(), String.Empty) + Space(1)
                Next
                cashMemoPrintDetails.WelcomeMessage = SplitString(welcomeMessage, LineLength).ToString().TrimEnd()

                For Each drMessage As DataRow In dsCashMemoDetails.Tables("PrintingDetails").Select("TOPBOTTOM='Promo'")
                    promotionalMessage += IIf(drMessage("ReceiptText") IsNot DBNull.Value, drMessage("ReceiptText").ToString().Trim(), String.Empty) + Space(1)
                Next
                cashMemoPrintDetails.PromotionalMessage = SplitString(promotionalMessage, LineLength).ToString().TrimEnd()

                For Each drMessage As DataRow In dsCashMemoDetails.Tables("PrintingDetails").Select("TOPBOTTOM='Tax'")
                    taxMessage += IIf(drMessage("ReceiptText") IsNot DBNull.Value, drMessage("ReceiptText").ToString().Trim(), String.Empty) + Space(1)
                Next
                cashMemoPrintDetails.TaxInformation = SplitString(taxMessage, LineLength).ToString().TrimEnd()

            End If

            If (dsCashMemoDetails.Tables("SiteDetails") IsNot Nothing AndAlso dsCashMemoDetails.Tables("SiteDetails").Rows.Count > 0) Then
                cashMemoPrintDetails.SiteDetails = New MstSite

                If (GetPrintFormatText("SiteDetails", cashMemoPrintDetails.SiteDetails, 0)) Then

                    Dim fullAddress As String = cashMemoPrintDetails.SiteDetails.SiteAddressLn1.Trim()
                    fullAddress += IIf(Not String.IsNullOrEmpty(cashMemoPrintDetails.SiteDetails.SiteAddressLn2), String.Concat(", ", cashMemoPrintDetails.SiteDetails.SiteAddressLn2.Trim()), String.Empty)
                    fullAddress += IIf(Not String.IsNullOrEmpty(cashMemoPrintDetails.SiteDetails.SiteAddressLn3), String.Concat(", ", cashMemoPrintDetails.SiteDetails.SiteAddressLn3.Trim()), String.Empty)
                    fullAddress += IIf(Not String.IsNullOrEmpty(cashMemoPrintDetails.SiteDetails.CityCode), String.Concat(", ", cashMemoPrintDetails.SiteDetails.CityCode.Trim()), String.Empty)
                    fullAddress += IIf(Not String.IsNullOrEmpty(cashMemoPrintDetails.SiteDetails.SitePinCode), String.Concat(", ", cashMemoPrintDetails.SiteDetails.SitePinCode.Trim()), String.Empty)

                    Dim drTemplateKey() As DataRow = dsTemplateData.Tables("SiteDetails").Select(String.Format("KeyName='{0}'", "FullAddress"))
                    If (Not IsDBNull(drTemplateKey(0)("Length"))) Then
                        fieldLength = drTemplateKey(0)("Length")
                    End If

                    fullAddress = System.Text.RegularExpressions.Regex.Replace(fullAddress, "\s{2,}", " ")
                    cashMemoPrintDetails.SiteDetails.FullAddress = fullAddress.FormatText(fieldLength, fieldAlignment, False)

                    cashMemoPrintDetails.SiteDetails.Telephone = cashMemoPrintDetails.SiteDetails.SiteTelephone1.Trim()
                    cashMemoPrintDetails.SiteDetails.Telephone += IIf(Not String.IsNullOrEmpty(cashMemoPrintDetails.SiteDetails.SiteTelephone2), String.Concat(", ", cashMemoPrintDetails.SiteDetails.SiteTelephone2.Trim()), String.Empty)
                End If
            End If

            If (dsCashMemoDetails.Tables("CashMemoHeader") IsNot Nothing AndAlso dsCashMemoDetails.Tables("CashMemoHeader").Rows.Count > 0) Then
                cashMemoPrintDetails.CashMemoHeader = New CashMemoHdr

                If (GetPrintFormatText("CashMemoHeader", cashMemoPrintDetails.CashMemoHeader, 0)) Then
                    cashMemoPrintDetails.CashMemoHeader.BillDate = Convert.ToDateTime(dsCashMemoDetails.Tables("CashMemoHeader").Rows(0)("BillDate")).ToString("dd/MM/yyyy").Trim()
                    cashMemoPrintDetails.CashMemoHeader.BillTime = Convert.ToDateTime(dsCashMemoDetails.Tables("CashMemoHeader").Rows(0)("BillTime")).ToString("hh:mm:ss tt").Trim()
                    cashMemoPrintDetails.CashMemoHeader.ReprintDate = DateTime.Now.ToString("dd/MM/yyyy")
                    cashMemoPrintDetails.CashMemoHeader.ReprintTime = DateTime.Now.ToString("hh:mm:ss tt")

                    If (IsBillReprint) Then
                        cashMemoPrintDetails.CashMemoHeader.ReprintReason = ReprintReason.FormatText(24, "Left", False)
                    End If
                    If (cashMemoPrintDetails.CashMemoHeader.DiscountAmt.Trim.Equals(strZero)) Then
                        cashMemoPrintDetails.CashMemoHeader.DiscountAmt = String.Empty
                    End If

                End If
            End If

            If (dsCashMemoDetails.Tables("CashMemoDetails") IsNot Nothing AndAlso dsCashMemoDetails.Tables("CashMemoDetails").Rows.Count > 0) Then
                cashMemoPrintDetails.CashMemoDetails = New List(Of CashMemoDtl)
                Dim cashMemoDetails As CashMemoDtl = Nothing

                For rowIndex = 0 To dsCashMemoDetails.Tables("CashMemoDetails").Rows.Count - 1
                    cashMemoDetails = New CashMemoDtl

                    If (GetPrintFormatText("CashMemoDetails", cashMemoDetails, rowIndex)) Then
                        cashMemoPrintDetails.CashMemoDetails.Add(cashMemoDetails)
                    End If
                Next
            End If

            If (dsCashMemoDetails.Tables("CashMemoReceipts") IsNot Nothing AndAlso dsCashMemoDetails.Tables("CashMemoReceipts").Rows.Count > 0) Then
                cashMemoPrintDetails.CashMemoReceipts = New List(Of CashMemoReceipt)
                Dim cashMemoReceipts As CashMemoReceipt = Nothing

                For rowIndex = 0 To dsCashMemoDetails.Tables("CashMemoReceipts").Rows.Count - 1
                    cashMemoReceipts = New CashMemoReceipt

                    If (GetPrintFormatText("CashMemoReceipts", cashMemoReceipts, rowIndex)) Then
                        cashMemoPrintDetails.CashMemoReceipts.Add(cashMemoReceipts)
                    End If
                Next
            End If

            If (dsCashMemoDetails.Tables("CashMemoTaxDetails") IsNot Nothing) Then
                cashMemoPrintDetails.CashMemoTaxDetails = New List(Of CashMemoTaxDtls)
                Dim cashMemoTaxDtls As CashMemoTaxDtls = Nothing

                For rowIndex = 0 To dsCashMemoDetails.Tables("CashMemoTaxDetails").Rows.Count - 1
                    cashMemoTaxDtls = New CashMemoTaxDtls

                    If (GetPrintFormatText("CashMemoTaxDetails", cashMemoTaxDtls, rowIndex)) Then
                        cashMemoPrintDetails.CashMemoTaxDetails.Add(cashMemoTaxDtls)
                    End If
                Next

                If (cashMemoPrintDetails.CashMemoTaxDetails IsNot Nothing AndAlso cashMemoPrintDetails.CashMemoTaxDetails.Count > 0) Then
                    cashMemoPrintDetails.CashMemoTaxSummary = New List(Of CashMemoTaxDtls)

                    Dim cmTaxDetails = From tax In cashMemoPrintDetails.CashMemoTaxDetails
                                        Group By tax.TaxName, tax.TaxLabel
                                        Into Sum(CDec(tax.TaxValue.Trim()))

                    For Each taxDtls As Object In cmTaxDetails
                        Dim drTemplateKey() As DataRow = dsTemplateData.Tables("CashMemoTaxDetails").Select(String.Format("KeyName='{0}'", "TaxValue"))
                        If (Not IsDBNull(drTemplateKey(0)("Length"))) Then
                            fieldLength = drTemplateKey(0)("Length")
                            fieldAlignment = drTemplateKey(0)("Alignment")
                        End If

                        cashMemoTaxDtls = New CashMemoTaxDtls
                        cashMemoTaxDtls.TaxName = taxDtls.TaxName.ToString()
                        cashMemoTaxDtls.TaxValue = taxDtls.Sum.ToString.FormatText(fieldLength, fieldAlignment, False)

                        cashMemoPrintDetails.CashMemoTaxSummary.Add(cashMemoTaxDtls)
                    Next
                End If

            End If

            If (dsCashMemoDetails.Tables("CashMemoComboDetails") IsNot Nothing AndAlso dsCashMemoDetails.Tables("CashMemoComboDetails").Rows.Count > 0) Then
                cashMemoPrintDetails.CashMemoComboDetails = New List(Of CashMemoComboDtl)
                Dim cashMemoComboDtls As CashMemoComboDtl = Nothing

                For rowIndex = 0 To dsCashMemoDetails.Tables("CashMemoComboDetails").Rows.Count - 1
                    cashMemoComboDtls = New CashMemoComboDtl

                    If (GetPrintFormatText("CashMemoComboDetails", cashMemoComboDtls, rowIndex)) Then
                        cashMemoPrintDetails.CashMemoComboDetails.Add(cashMemoComboDtls)
                    End If
                Next
            End If

            If (dsCashMemoDetails.Tables("SalesPersonDetails") IsNot Nothing) Then
                cashMemoPrintDetails.SalesPersonDetails = New MstSalesPerson

                GetPrintFormatText("SalesPersonDetails", cashMemoPrintDetails.SalesPersonDetails, 0)
            End If


            If (dsCashMemoDetails.Tables("TerminalDetails") IsNot Nothing) Then
                cashMemoPrintDetails.TerminalDetails = New MstTerminalID

                GetPrintFormatText("TerminalDetails", cashMemoPrintDetails.TerminalDetails, 0)
            End If

            If (cashMemoPrintDetails.CashMemoComboDetails IsNot Nothing AndAlso cashMemoPrintDetails.CashMemoComboDetails.Count > 0) Then

                For rowIndex = 0 To cashMemoPrintDetails.CashMemoDetails.Count - 1
                    Dim comboArticleCode = cashMemoPrintDetails.CashMemoDetails(rowIndex).ArticleCode.Trim()

                    Dim cashMemoComboDtlList = (From cd In cashMemoPrintDetails.CashMemoComboDetails
                                           Where cd.ComboArticleCode.Trim() = comboArticleCode
                                           Select cd).ToList()

                    If (cashMemoComboDtlList IsNot Nothing AndAlso cashMemoComboDtlList.Count > 0) Then
                        cashMemoPrintDetails.CashMemoDetails(rowIndex).IsComboArticle = "True"
                        cashMemoPrintDetails.CashMemoDetails(rowIndex).CashMemoComboDetails = cashMemoComboDtlList
                    End If
                Next
            End If

            cashMemoPrintDetails.CustomerDetails = New CustomerDetails
            If (dsCashMemoDetails.Tables("CustomerDetails") IsNot Nothing AndAlso dsCashMemoDetails.Tables("CustomerDetails").Rows.Count > 0) Then
                If (GetPrintFormatText("CustomerDetails", cashMemoPrintDetails.CustomerDetails, 0)) Then
                    'cashMemoPrintDetails.CustomerDetails.CustomerName = dsCashMemoDetails.Tables("CustomerDetails").Rows(0)("CustomerName").ToString().Trim()
                    'cashMemoPrintDetails.CustomerDetails.PhoneNo = dsCashMemoDetails.Tables("CustomerDetails").Rows(0)("PhoneNo").ToString().Trim()
                    'cashMemoPrintDetails.CustomerDetails.TotalBalancePoint = dsCashMemoDetails.Tables("CustomerDetails").Rows(0)("TotalBalancePoint").ToString().Trim()
                End If
            Else
                cashMemoPrintDetails.CustomerDetails.CustomerName = ""
                cashMemoPrintDetails.CustomerDetails.PhoneNo = ""
                cashMemoPrintDetails.CustomerDetails.TotalBalancePoint = ""
            End If

            cashMemoPrintDetails.CompanyName = cashMemoPrintDetails.SiteDetails.SiteShortName.Trim()
            cashMemoPrintDetails.HeaderPrintAllowed = _HeaderPrint
            cashMemoPrintDetails.FooterPrintAllowed = _FooterPrint
            cashMemoPrintDetails.TaxPrintAllowed = _TaxDetail
            cashMemoPrintDetails.SalesPersonAllowed = _SalesPerApplicable
            cashMemoPrintDetails.IsBillReprint = IsBillReprint
            cashMemoPrintDetails.ComboItemPrintAllowed = ComboItemPrintingAllowed

            context.Add("CMPrintDtls", cashMemoPrintDetails)
            context.Add("DrawLine", DrawLine)
            context.Add("Currency", CurrencyText)
            context.Add("TotalNetSaving", TotralNetSaving)

            'NVelocit engine process
            Dim fileEngine As NVelocityTemplateEngine.Interfaces.INVelocityEngine
            fileEngine = NVelocityTemplateEngine.NVelocityEngineFactory.CreateNVelocityFileEngine(TemplatefolderPath, False)
            Dim strPrintMessage = fileEngine.Process(context, TemplateName)

            'Generate template based cash memo bill text file
            Dim billTextFile As String = Path.Combine(TemplatefolderPath, String.Concat(CashMemoNo, BillPrintFileExtension))
            Dim billTextWriter As New System.IO.StreamWriter(billTextFile)

            billTextWriter.Write(strPrintMessage)
            billTextWriter.Close()

            'Print CashMemo bill using commond prompt
            'COPY A:\OUTPUT.PRN /B \\Computer_Name\Printer_Share_Name
            'System.Diagnostics.Process.Start(billTextFile, "")

            PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")

            If PrinterName = Nothing Then
                Exit Sub
            End If
            Dim msg As String = String.Empty
            If _PrintPreview = True Then
                msg = fnPrint(strPrintMessage.ToString(), "PRV")
            Else
                msg = fnPrint(strPrintMessage.ToString(), "PRN")
            End If

            If (File.Exists(billTextFile)) Then
                File.Delete(billTextFile)
            End If

            'Added for fiscal printer
            Try
                clsFiscalPrinting.fnFiscalPrint(strPrintMessage.ToString())
            Catch ex As Exception

            End Try
            'Added for fiscal printer
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Function GetPrintFormatText(ByVal tableName As String, clsObject As Object, ByVal rowIndex As Integer) As Boolean
        Try
            If (dsTemplateData.Tables(tableName) IsNot Nothing) Then
                For Each siteProperty As System.Reflection.PropertyInfo In clsObject.GetType().GetProperties()
                    Try
                        siteProperty.SetValue(clsObject, String.Empty, Nothing)
                        Dim drTemplateKey() As DataRow = dsTemplateData.Tables(tableName).Select(String.Format("KeyName='{0}'", siteProperty.Name))

                        If (drTemplateKey.Length > 0 AndAlso dsCashMemoDetails.Tables(tableName).Columns.Contains(drTemplateKey(0)("KeyName").ToString())) Then
                            propertyValue = dsCashMemoDetails.Tables(tableName).Rows(rowIndex)(siteProperty.Name)

                            If (propertyValue IsNot Nothing AndAlso propertyValue IsNot DBNull.Value) Then
                                If (Not IsDBNull(drTemplateKey(0)("Length"))) Then
                                    fieldLength = drTemplateKey(0)("Length")
                                End If
                                If (Not IsDBNull(drTemplateKey(0)("Alignment"))) Then
                                    fieldAlignment = drTemplateKey(0)("Alignment")
                                End If
                                If (Not IsDBNull(drTemplateKey(0)("CropText"))) Then
                                    isCropText = drTemplateKey(0)("CropText")
                                End If

                                fieldValue = GetFieldValueWithFormat(propertyValue.GetType(), propertyValue)
                                siteProperty.SetValue(clsObject, fieldValue.FormatText(fieldLength, fieldAlignment, isCropText), Nothing)
                            End If
                        End If
                    Catch ex As Exception
                    End Try
                Next

                Return True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Function GetFieldValueWithFormat(ByVal fieldType As Type, ByVal fieldValue As Object) As String
        Try
            Dim fieldValueText As String = String.Empty

            If (fieldType Is Type.GetType("System.String")) Then
                fieldValueText = fieldValue.ToString().Trim()

            ElseIf (fieldType Is Type.GetType("System.Integer")) Then
                fieldValueText = FormatNumber(fieldValue, DecimalDigits)

            ElseIf (fieldType Is Type.GetType("System.Decimal")) Then
                fieldValueText = FormatNumber(fieldValue, DecimalDigits)

            ElseIf (fieldType Is Type.GetType("System.Double")) Then
                fieldValueText = FormatNumber(fieldValue, DecimalDigits)

            ElseIf (fieldType Is Type.GetType("System.DateTime")) Then
                fieldValueText = CDate(fieldValue).ToString("dd/MM/yyyy hh:mm:ss tt")

            ElseIf (fieldType Is Type.GetType("System.Boolean")) Then
                fieldValueText = fieldValue.ToString()
            End If

            Return fieldValueText.Trim()

        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try

    End Function

    Private Function MakeHtml(Desc As String, Qty As String) As String
        Dim NewLineLength = 40
        If IsKotFontLarge Then
            NewLineLength = 20
        ElseIf IsKotFontBold Then
            NewLineLength = 30
        Else
            NewLineLength = 40
        End If
        If IsKotFontLarge Then
            If (Desc.Length + Qty.Length) > 17 Then
                MakeHtml = Desc & vbCrLf & vbCrLf
                MakeHtml &= Qty.PadLeft(LineLength - Qty.Length - 17) + vbCrLf + vbCrLf
                MakeHtml = "<L>" & MakeHtml & "</L>"
            Else
                'hierarchy print change
                'MakeHtml = "<L>" & Desc + Qty.PadLeft(NewLineLength - Desc.Length - 2) & "</L>" + vbCrLf + vbCrLf
                If Desc.Length > 10 Then
                    MakeHtml = "<L>" & Desc + Space(11) + Qty.PadLeft(NewLineLength - Desc.Length - 2) & "</L>" + vbCrLf + vbCrLf
                Else
                    MakeHtml = "<L>" & Desc + Space(6) + Qty.PadLeft(NewLineLength - Desc.Length - 2) & "</L>" + vbCrLf + vbCrLf
                End If
            End If
        ElseIf IsKotFontBold Then
            If isremark = True Then '' condition of is remark added by ketan
                MakeHtml = "<B>" & Desc + Qty.PadLeft(NewLineLength - 2) & "</B>" + vbCrLf
            Else
                MakeHtml = "<B>" & Desc + Qty.PadLeft(LineLength - Desc.Length - 2) & "</B>" + vbCrLf  ''old
            End If
        Else
            If LineLength - Desc.Length - 2 > 0 Then
                If isremark = True Then  'condition of is remark added by ketan
                    MakeHtml = Desc & Qty.PadLeft(LineLength - 2) + vbCrLf
                Else
                    MakeHtml = Desc + Qty.PadLeft(LineLength - Desc.Length - 2) + vbCrLf
                End If
            Else
                MakeHtml = Desc & Qty.PadLeft(LineLength - 2) + vbCrLf
            End If
        End If
    End Function

    'Public Sub HoldCMPrintDineIn(ByVal SiteCode As String, ByVal Type As String, ByVal PrintBarcode As Boolean, ByRef ErrorMsg As String, Optional ByVal BarCodeType As String = "0", Optional ByVal vbillno As String = "0", Optional ByVal Tableno As String = "0", Optional ByVal mergid As Int64 = 0, Optional ByVal GenerateBillTaxAmount As Decimal = 0, Optional ByVal GenerateBillDiscAmount As Decimal = 0)
    '    Try
    '        Dim strPrintMsg As StringBuilder
    '        Dim strLineDetail As New StringBuilder
    '        Dim strMergeLine As New StringBuilder
    '        'Dim StrHeader, StrSubHeader, StrBody, StrSubFooter, StrFooter, StrCashMemoLine, StrCashierLine, StrTerminalLine, StrCustomerLine, StrLineTax, StrLineDisc, StrTotalAmt As String
    '        Dim StrHeader, StrSubHeader, StrBody, StrSubFooter, StrFooter, StrCashMemoLine, StrCashierLine, StrTerminalLine, StrLineTax, StrLineDisc, StrCustomerLine, StrTotalAmt As String
    '        Dim StrCMNo, StrCMDate, StrCMTime, StrCashier, StrTerminal As String
    '        Dim SiteLine, StrPrintHeaderLine, StrPrintFooterLine As String
    '        Dim LineItemHeading, StrLineItem As String
    '        Dim ItemCode, Desc, Qty, Rate, NetAmt, GrossAmt As String
    '        Dim TotalQtyLine, TotalGrossAmtLine As String
    '        Dim TermsNcond As String
    '        Dim strLine As String
    '        Dim strSiteGSTNo As String
    '        Dim table As String
    '        Dim mergeHead As String
    '        Dim mergeDesc, mtableno, mbillno As String
    '        Dim LineMergeHeading, Strmerge As String
    '        Dim strDblLine As String
    '        Dim dtPrint As DataTable
    '        Dim dtView As New DataTable
    '        ErrorMsg = ""
    '        Dim obj As New SpectrumBL.clsCashMemo()
    '        If mergid > 0 Then
    '            dtView = obj.GetHoldDataForPrintForMerge(mergid, SiteCode)
    '        Else
    '            dtView = obj.GetDineInHoldDataForPrint(vbillno, SiteCode)
    '        End If

    '        Dim IntTotItemQty As Integer = 0
    '        IntTotItemQty = IIf(dtView.Compute("SUM(QUANTITY)", "") Is DBNull.Value, 0, dtView.Compute("SUM(QUANTITY)", ""))

    '        Dim objPrint As New SpectrumBL.clsCommon
    '        dtPrint = objPrint.GetPrintingDetails(Type)
    '        If Not dtPrint Is Nothing Then
    '            Dim filter As String = ""
    '            Dim dv As New DataView(dtPrint, filter, "Sequenceno", DataViewRowState.CurrentRows)
    '            If _HeaderPrint = True Then
    '                filter = "TOPBOTTOM='Top'"
    '                dv.RowFilter = filter
    '                For Each drview As DataRowView In dv
    '                    StrPrintHeaderLine = drview("ReceiptText").ToString()
    '                    StrSubHeader = StrSubHeader & StrPrintHeaderLine & vbNewLine
    '                Next
    '            End If
    '            If _FooterPrint = True Then
    '                filter = "TOPBOTTOM='Bottom'"
    '                dv.RowFilter = filter
    '                For Each drview As DataRowView In dv
    '                    StrPrintFooterLine = drview("ReceiptText").ToString()
    '                    StrFooter = StrFooter & StrPrintFooterLine & vbNewLine
    '                Next
    '            End If
    '        End If
    '        If Not StrSubHeader Is Nothing AndAlso StrSubHeader.Length > 0 Then
    '            StrSubHeader = SplitString(StrSubHeader, LineLength).ToString()
    '        End If
    '        If Not StrFooter Is Nothing AndAlso StrFooter.Length > 0 Then
    '            StrFooter = SplitString(StrFooter, LineLength).ToString()
    '        End If
    '        If dtView.Rows.Count > 0 Then
    '            If Not dtView Is Nothing Then
    '                strLine = "-".PadRight(LineLength - 1, "-") & vbCrLf
    '                strDblLine = "=".PadRight(LineLength - 1, "=") & vbCrLf


    '                Dim SiteName, Site, StrSiteTelline, StrAdrressLine As String
    '                Dim TS_SiteName As StringBuilder

    '                SiteName = dtView.Rows(0)("SITEOFFICIALNAME").ToString()
    '                TS_SiteName = SplitString(SiteName)
    '                SiteName = TS_SiteName.ToString()
    '                SiteName = SiteName.PadLeft(LineLength / 2 + (SiteName.Length / 2))
    '                SiteLine = SiteName

    '                StrSiteTelline = getValueByKey("CLSCMP006") & " " & dtView.Rows(0)("TELNO").ToString()
    '                StrAdrressLine = getValueByKey("CLSCMP007") & " " & dtView.Rows(0)("ADDRESS").ToString()
    '                StrAdrressLine = SplitString(StrAdrressLine, LineLength).ToString()

    '                StrHeader = SiteLine & vbCrLf
    '                StrHeader = StrHeader & StrAdrressLine.Trim & vbCrLf
    '                StrHeader = StrHeader & StrSiteTelline & vbCrLf
    '                'StrHeader = SiteLine & vbCrLf
    '                StrHeader = StrHeader & strLine
    '                StrCMDate = getValueByKey("CLSPV005") & Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())
    '                StrCMTime = getValueByKey("DINE0001") & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")
    '                If mergid > 0 Then
    '                    mergeHead = "Merged Orders".PadLeft(strLine.Length - 16) & vbCrLf
    '                    'LineMergeHeading = "Table No." & Space(8) & "Order No" & vbCrLf
    '                    LineMergeHeading = "Table No.".PadLeft(12) & "Order No".PadLeft(16) & vbCrLf
    '                    StrCashMemoLine = mergeHead & LineMergeHeading & vbCrLf
    '                    Dim dtMergOrder As DataTable = obj.GetMergeBillNo(mergid)
    '                    For Each dr As DataRow In dtMergOrder.Rows

    '                        mtableno = dr("TableNo").ToString()
    '                        mbillno = dr("BillNo").ToString()

    '                        'Strmerge = Space(2) & mtableno & Space(14) & mbillno & vbCrLf
    '                        Strmerge = mtableno.PadLeft(8) & mbillno.PadLeft(25) & vbCrLf
    '                        strMergeLine.Append(Strmerge)
    '                    Next
    '                    StrCashMemoLine = StrCashMemoLine & strMergeLine.ToString() & strLine & vbCrLf & StrCMDate & vbCrLf & StrCMTime

    '                Else
    '                    table = "Table No : " & Tableno & vbCrLf & strLine
    '                    'StrCMNo = "Hold BillNo:" & dtView.Rows(0)("Billno").ToString()
    '                    'StrCMDate = "Date:" & Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())
    '                    StrCMNo = "Order No:" & dtView.Rows(0)("Billno").ToString()
    '                    StrCashMemoLine = StrCMNo.PadRight(LineLength - StrCMDate.Length) & vbCrLf & StrCMDate & vbCrLf & StrCMTime & vbCrLf
    '                End If

    '                StrCashierLine = StrCashMemoLine & vbCrLf

    '                'StrSubHeader = StrSubHeader & strLine
    '                If mergid > 0 Then
    '                    StrHeader = StrHeader & StrCashierLine
    '                Else
    '                    StrHeader = StrHeader & table.ToString().Replace("Table No.", "") & StrCashierLine
    '                End If


    '                'StrHeader = StrHeader & strLine

    '                'LineItemHeading = "Item Code".PadRight(26) & "Item Name".PadRight(14) & vbCrLf
    '                'LineItemHeading = LineItemHeading & Space(3) & "Qty " & Space(5) & "Price " & Space(10) & "NetAmt" & vbCrLf

    '                'LineItemHeading = getValueByKey("CLSPSO020").PadRight(26) & getValueByKey("CLSCMP016").PadRight(14) & vbCrLf
    '                'LineItemHeading = LineItemHeading & Space(3) & getValueByKey("CLSPSO022") & " " & Space(5) & getValueByKey("CLSPSO023") & " " & Space(8) & getValueByKey("CLSCMP029") & " " & vbCrLf
    '                LineItemHeading = getValueByKey("CLSCMP016") & Space(13) & getValueByKey("CLSPSO022") & " " & Space(5) & "Amt " & vbCrLf
    '                'LineItemHeading = getValueByKey("CLSCMP016") & getValueByKey("CLSPSO022").PadLeft(10) & "Amt ".PadRight(4) & vbCrLf
    '                Dim dvItemDetail As New DataView(dtView, "", "", DataViewRowState.CurrentRows)
    '                Dim DtUnique As DataTable = dvItemDetail.ToTable(True, "EAN", "ArticleName", "QUANTITY", "SELLINGPRICE", "NETAMOUNT", "GROSSAMT")
    '                For Each dr As DataRow In DtUnique.Rows
    '                    ItemCode = dr("Ean").ToString()

    '                    'If (dr("ArticleName").ToString().Length > 15) Then
    '                    '    Desc = dr("ArticleName").ToString().Substring(0, 18)
    '                    'Else
    '                    Desc = dr("ArticleName").ToString()
    '                    'End If

    '                    Qty = dr("Quantity").ToString()
    '                    Rate = dr("SellingPrice").ToString()
    '                    NetAmt = dr("NetAmount").ToString()
    '                    GrossAmt = dr("GrossAmt").ToString()
    '                    Dim i As Integer = 0
    '                    'For i = ItemCode.Length To 26
    '                    ItemCode = ItemCode
    '                    'Next
    '                    'For i = Desc.Length To 14
    '                    Desc = Desc
    '                    'Next
    '                    Qty = Format(CDbl(Qty), "0")
    '                    'For i = Qty.Length To 4
    '                    'Qty = " " & Qty
    '                    'Next
    '                    Rate = FormatNumber(CDbl(Rate), 2)
    '                    'Rate = FormatCurrency(Rate, 2)
    '                    'For i = Rate.Length To 8
    '                    '    Rate = " " & Rate
    '                    'Next

    '                    NetAmt = FormatNumber(CDbl(NetAmt), 2)
    '                    GrossAmt = FormatNumber(CDbl(GrossAmt), 2)

    '                    Dim ItemValue As String
    '                    ItemValue = Convert.ToString(Rate * Qty)





    '                    If 28 - Desc.Length < Qty.Length Then
    '                        StrLineItem = Desc & vbCrLf

    '                        ' StrLineItem = StrLineItem & Qty.PadLeft(28) & NetAmt.PadLeft(11) & vbCrLf
    '                        'added for issue id 1270-changes in generated bill print format by vipul
    '                        StrLineItem = StrLineItem & Qty.PadLeft(28) & ItemValue.PadLeft(11) & vbCrLf
    '                    Else
    '                        ' StrLineItem = Desc & Qty.PadLeft(28 - Desc.Length) & NetAmt.PadLeft(11) & vbCrLf
    '                        'added for issue id 1270-changes in generated bill print format by vipul
    '                        StrLineItem = Desc & Qty.PadLeft(28 - Desc.Length) & ItemValue.PadLeft(11) & vbCrLf
    '                    End If




    '                    strLineDetail.Append(StrLineItem)
    '                Next
    '                StrBody = strLineDetail.ToString() & vbCrLf
    '                Dim tempStrLine = "-".PadRight(LineLength - 1, "-")  ' modified by khusrao adil khan on 12-09-2017 for removing extra space
    '                StrBody = StrBody & tempStrLine



    '                '  TotalGrossAmtLine = "Total Amt.".PadRight(31) & FormatNumber(IIf(dtView.Compute("SUM(NetAmount)", "") Is DBNull.Value, 0, dtView.Compute("SUM(NetAmount)", "")), 2)
    '                'added for issue id 1270-changes in generated bill print format by vipul
    '                TotalGrossAmtLine = "Total to Pay" & Space(22 - dtView.Compute("SUM(NetAmount)", "").ToString.Length) & FormatNumber(CDbl(dtView.Compute("SUM(NetAmount)", "")), 2) & Space(1) & " INR" & vbCrLf
    '                StrBody = StrBody & TotalQtyLine & vbCrLf
    '                Dim SiteGSTNo = dtView.Rows(0)("LocalSalesTaxNo").ToString()
    '                If SiteGSTNo <> "" Then
    '                    strSiteGSTNo = "GST No : " & SiteGSTNo & vbCrLf
    '                    ''Space(15 - SiteGSTNo.ToString.Length) & SiteGSTNo & Space(1) & vbCrLf
    '                    ' StrBody = StrBody & TotalQtyLine & vbCrLf
    '                    ' StrBody = StrBody & TotalGrossAmtLine & vbCrLf
    '                End If
    '                'StrBody = StrBody & TotalGrossAmtLine & vbCrLf

    '                'Dim dtTerms As DataTable = dvItemDetail.ToTable(True, "TNCSRNO", "Terms")
    '                'For Each drTerms As DataRow In dtTerms.Select("", "TNCSRNO", DataViewRowState.CurrentRows)
    '                '    TermsNcond = TermsNcond & drTerms("Terms").ToString() & vbCrLf
    '                'Next

    '                StrFooter = StrFooter & TermsNcond & vbCrLf

    '                strPrintMsg = New StringBuilder()
    '                If PrintBarcode = True Then
    '                    strPrintMsg.Append(vbCrLf)
    '                    strPrintMsg.Append(vbCrLf)
    '                    strPrintMsg.Append(vbCrLf)
    '                    strPrintMsg.Append(vbCrLf)
    '                    strPrintMsg.Append(vbCrLf)
    '                End If
    '                strPrintMsg.Append(StrHeader)
    '                strPrintMsg.Append(StrSubHeader)
    '                strPrintMsg.Append(strDblLine)
    '                strPrintMsg.Append(LineItemHeading)
    '                strPrintMsg.Append(strDblLine)
    '                strPrintMsg.Append(StrBody)
    '                strPrintMsg.Append(strLine)
    '                ' strPrintMsg.Append(strLine)
    '                'code added for issue id 1270-changes in generated bill print format by vipul
    '                Dim Totalamt1 As String = ""

    '                For Each dr As DataRow In dtView.Rows


    '                    Totalamt1 = dr("GrossAmt").ToString()
    '                    Exit For
    '                Next


    '                ' ItemCode = dr("Ean").ToString()


    '                'StrTotalAmt = "Total Amt" & Space(25 - dtView.Compute("SUM(SellingPrice)", "").ToString.Length) & FormatNumber(CDbl(dtView.Compute("SUM(SellingPrice)", "")), 2) & Space(1) & " INR" & vbCrLf




    '                StrTotalAmt = "Total Amt" & Space(25 - Totalamt1.ToString.Length) & FormatNumber(CDbl(Convert.ToDecimal(Totalamt1)), 2) & Space(1) & " INR" & vbCrLf & vbCrLf
    '                strPrintMsg.Append(StrTotalAmt)


    '                GenerateBillTaxAmount = FormatNumber(CDbl(GenerateBillTaxAmount), 2)
    '                GenerateBillDiscAmount = FormatNumber(CDbl(GenerateBillDiscAmount), 2)

    '                If GenerateBillTaxAmount > 0 Then

    '                    StrLineTax = "Total Tax Amt" & Space((20) - GenerateBillTaxAmount.ToString.Length) & GenerateBillTaxAmount & Space(1) & " INR" & vbCrLf
    '                    Dim strCGSTTaxLine As Double = GenerateBillTaxAmount / 2
    '                    Dim strSGSTTaxLine As Double = GenerateBillTaxAmount / 2
    '                    StrLineTax = ""
    '                    StrLineTax = "CGST Tax Amt" & Space((20) - strCGSTTaxLine.ToString.Length) & strCGSTTaxLine & Space(1) & " INR" & vbCrLf
    '                    StrLineTax += "SGST Tax Amt" & Space((20) - strSGSTTaxLine.ToString.Length) & strSGSTTaxLine & Space(1) & " INR" & vbCrLf
    '                    strPrintMsg.Append(StrLineTax)
    '                End If
    '                If GenerateBillDiscAmount > 0 Then
    '                    strPrintMsg.Append(strLine)
    '                    StrLineDisc = "Total Discount Amt" & Space((15) - GenerateBillDiscAmount.ToString.Length) & GenerateBillDiscAmount & Space(1) & " INR" & vbCrLf
    '                    strPrintMsg.Append(StrLineDisc)

    '                End If
    '                strPrintMsg.Append(StrLineTax)
    '                If GenerateBillDiscAmount > 0 Then
    '                    strPrintMsg.Append(StrLineDisc)
    '                End If

    '                strPrintMsg.Append("--------------" & vbCrLf)
    '                strPrintMsg.Append(TotalGrossAmtLine)
    '                If strSiteGSTNo <> "" Then
    '                    strPrintMsg.Append(strLine)
    '                    strPrintMsg.Append(strSiteGSTNo)
    '                End If
    '                strPrintMsg.Append(strLine)



    '                'strPrintMsg.Append(StrSubFooter)
    '                strPrintMsg.Append(StrFooter)

    '                If PrintBarcode = True Then
    '                    Dim s As C1BarCode = GetBarcode(CashMemoNo)
    '                    VarBarcode = s
    '                End If
    '                strPrintMsg.Append(" " & vbCrLf)
    '                strPrintMsg.Append(" " & vbCrLf)
    '                'fnPrint(strPrintMsg.ToString(), "PRN")

    '                PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Hold")

    '                If _PrintPreview = True Then
    '                    fnPrint(strPrintMsg.ToString(), "PRV")
    '                Else
    '                    fnPrint(strPrintMsg.ToString(), "PRN")
    '                End If
    '            Else
    '                ErrorMsg = getValueByKey("CLSCMP031")
    '            End If
    '        Else

    '        End If
    '    Catch ex As Exception
    '        ErrorMsg = ex.Message
    '    End Try
    'End Sub
    'Public Sub DineInKOTPrint(ByVal Type As String, ByRef errorMsg As String, ByVal dtOld As DataTable, ByVal dtNew As DataTable, BillNO As String, TableNo As String)
    '    Try
    '        Dim strLineDetail, sbKOTBillPrintBase, sbKOTBillPrintBaseTakeAway, sbKOTBillPrintArticleMain, sbKOTBillPrintArticleTakeAway, sbKOTBillPrintArticleQuantityWise As New StringBuilder
    '        Dim StrCMNo, StrTokenNo, StrCustomerName As String
    '        Dim LineItemHeading, StrSubHeader, StrLineItem As String
    '        Dim ItemCode, Desc, Qty, TakeAwayQty As String
    '        Dim strLine, StrTokenSalesTypeLine, StrTokenTakeAwayLine As String
    '        Dim strDblLine As String
    '        Dim isTakeAwayQtyPrint As Boolean = False
    '        Dim oldQty As Decimal
    '        Dim newQty As Decimal
    '        'Dim dtnew As New DataTable
    '        'Dim dtold As New DataTable








    '        errorMsg = ""
    '        If Not dtNew Is Nothing Then
    '            strLine = "-".PadRight(LineLength, "-") & vbCrLf
    '            strDblLine = "=".PadRight(LineLength, "=") & vbCrLf
    '            '------ STEP-1:  collecting all Data With Format -----
    '            If Type = "DCM" Then
    '                StrCMNo = getValueByKey("CLSCMP008") & BillNO
    '            Else
    '                'StrCMNo = getValueByKey("CLSCMP009") & BillNO
    '                sbKOTBillPrintBase.Append("Table No: " & TableNo.ToString().Replace("Table No.", "") + vbCrLf + vbCrLf)
    '                sbKOTBillPrintBase.Append("Bill No: " & BillNO + vbCrLf + vbCrLf)
    '                sbKOTBillPrintBase.Append(strLineL40 + vbCrLf)
    '            End If

    '            'StrTokenNo = dtView.Rows(0)("Billno").ToString().Trim.Substring(dtView.Rows(0)("Billno").ToString().Trim.Length - 2, 2)
    '            'Select Case PrintFormatNo
    '            '    Case 1
    '            '        StrTokenNo = "<B>" & StrTokenNo & "</B>"
    '            '    Case 2
    '            '    Case Else
    '            'End Select

    '            If Type = "CMWAmt" Then
    '                'LineItemHeading = getValueByKey("CLSPSO020") & Space(17) & vbCrLf & getValueByKey("CLSCMP016") & Space(20) & getValueByKey("CLSPSO022") & " " & vbCrLf
    '                LineItemHeading = "Article " & Space(17) & vbCrLf & getValueByKey("CLSCMP016") & Space(20) & getValueByKey("CLSPSO022") & " " & vbCrLf
    '            Else
    '                LineItemHeading = getValueByKey("CLSCMP016") & Space(13) & getValueByKey("CLSPSO022") & " " & Space(5)
    '                Select Case PrintFormatNo
    '                    Case 1
    '                        LineItemHeading &= "Amt" & vbCrLf
    '                    Case 2

    '                    Case Else
    '                        LineItemHeading &= "Amt" & vbCrLf
    '                End Select

    '            End If

    '            If (dtNew.Rows.Count() > 0 And String.IsNullOrEmpty(DeliveryPersonName)) Then
    '                If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
    '                    StrTokenNo = "Token No. " & StrTokenNo
    '                End If
    '            End If

    '            sbKOTBillPrintBase.Append(getValueByKey("CLSPSO020") + getValueByKey("CLSPSO022").PadLeft(LineLength - getValueByKey("CLSPSO020").Length - 4) + vbCrLf)
    '            sbKOTBillPrintBase.Append(strLineL40 + vbCrLf)



    '            sbKOTBillPrintBaseTakeAway.Append(sbKOTBillPrintBase)




    '            '---- Step 2- making 2 Instance 1 for DineIn 2 for TakeAway 
    '            '---- looping items and construct prints 
    '            Dim dvItemDetail As New DataView(dtNew, "", "", DataViewRowState.CurrentRows)
    '            Dim BillLineNO As String = String.Empty
    '            For Each dr As DataRow In dtNew.Rows
    '                oldQty = 0
    '                ItemCode = dr("ArticleCode").ToString()
    '                Desc = IIf(DisplayArticleFullName, dr("DISCRIPTION").ToString(), dr("DISCRIPTION").ToString())
    '                BillLineNO = dr("BillLineNO").ToString()
    '                Dim result As DataRow() = dtOld.[Select]("ArticleCode='" + dr("ArticleCode") & "'")
    '                If result.Count > 0 Then
    '                    oldQty = Convert.ToDecimal(result(0)("KotQuantity"))
    '                End If
    '                If oldQty > 0 Then
    '                    newQty = Convert.ToDecimal(dr("Quantity")) - oldQty
    '                Else
    '                    newQty = Convert.ToDecimal(dr("Quantity"))
    '                End If

    '                Qty = dr("Quantity").ToString()
    '                Dim IsForEachQuantityOver = False

    '                newQty = FormatNumber(CDbl(clsCommon.CheckIfBlank(newQty)), 3)
    '                'Call SetQtyFormat(newQty)
    '                If Val(newQty) > 0 Then

    '                    sbKOTBillPrintArticleMain.Append(MakeHtml(Desc, newQty))

    '                End If
    '                ''''''




    '                ''''''

    '            Next

    '            ' step 4--------- if till now not Called for Print , call now----------------

    '            If sbKOTBillPrintArticleMain.Length > 0 Then
    '                Call fnKotPrint(sbKOTBillPrintBase, sbKOTBillPrintArticleMain, errorMsg)
    '            End If


    '        Else
    '            errorMsg = getValueByKey("CLSCMP031")
    '        End If
    '    Catch ex As Exception
    '        errorMsg = ex.Message
    '    End Try

    'End Sub  '' commented by nikhil

    Public Sub HoldCMPrintDineIn(ByVal dayopenDate As Date, ByVal LangCode As String, ByVal Currency As String, ByVal SiteCode As String, ByVal Type As String, ByVal PrintBarcode As Boolean, ByRef ErrorMsg As String, Optional ByVal BarCodeType As String = "0", Optional ByVal vbillno As String = "0", Optional ByVal Tableno As String = "0", Optional ByVal mergid As Int64 = 0, Optional ByVal GenerateBillTaxAmount As Decimal = 0, Optional ByVal GenerateBillDiscAmount As Decimal = 0, Optional ByVal DisplayTaxInGenerateBill As Boolean = False, Optional ByVal GenerateBillGrossAmount As Decimal = 0, Optional ByVal DtMergeOrderTax As DataTable = Nothing)
        Try
            Dim strPrintMsg, sbDeliveryBillPrint As StringBuilder
            Dim strLineDetail As New StringBuilder
            Dim StrHeader, StrSubHeader, StrBody, StrCompanyInfo, StrTagLine, StrSubFooter, StrFooter, StrDuplicate, StrDeleteLine, StrCashMemoLine, StrCashierLine, StrSalesPersonLine As String
            Dim StrTokenNo, StrCustomerName As String
            Dim StrCMNo, StrCMDate, StrCMTime, StrCashier, StrSalesPerson As String
            Dim SiteLine, StrSiteTelline, StrAdrressLine, StrSiteReg1Line, StrSiteReg2Line As String
            Dim CLpLine, LineItemHeading, StrLineItem As String
            Dim CustomerNameLine, CLPPointsLine, CustmerPhoneNo As String
            Dim ItemCode, Desc, Qty, Rate, DiscAmt, DiscPer, TaxAmt, NetAmt, RateAfterDisc, GrossAmt As String
            Dim TotalQtyLine, TakeAwayQuantity, TotalGrossAmtLine, TotalDiscAmtLine, SubTotalLine, NetAmtLine, TotalTaxAmtLine, strTotalToPay As String
            Dim TotalFooterTaxDetail, FooterTaxLine As String
            Dim TenderDetails, TenderLine As String
            Dim TotalPromotionalMsg, PromoMsgLine, StrPrintHeaderLine, StrPrintFooterLine As String
            Dim TermsNcond As String
            Dim strLine As String
            Dim strDblLine As String
            Dim strHomeDilery, BillNoOfMergeId As String
            Dim isTakeAwayQtyPrint As Boolean = False


            Dim DTMergeOrder, DtMergeOrderTaxCopy As New DataTable   'vipul
            Dim TaxName As String
            Dim obj As New SpectrumBL.clsCashMemo()
            If mergid = 0 Then
            Else
                If Not DtMergeOrderTax Is Nothing Then
                    Dim DistinctTaxCode = DtMergeOrderTax.DefaultView.ToTable(True, "TaxCode")
                    If Not DistinctTaxCode Is Nothing Then

                        If DistinctTaxCode.Rows.Count > 0 Then
                            For Each dr As DataRow In DistinctTaxCode.Rows
                                TaxName = TaxName + "," + Convert.ToString(dr("TaxCode"))
                            Next
                            TaxName = TaxName.Substring(1, TaxName.Length - 1)
                        End If
                      
                    End If
                    DtMergeOrderTaxCopy = obj.GetTaxNameByTaxCode(SiteCode, TaxName)

                    For Each dr As DataRow In DtMergeOrderTaxCopy.Rows
                        dr("TaxValue") = DtMergeOrderTax.Compute("Sum(TAXAMOUNT)", "TaxCode='" & dr("TaxCode").ToString & "'")
                    Next
                    DtMergeOrderTaxCopy.AcceptChanges()
                    If DtMergeOrderTaxCopy.Columns.Contains("TaxCode") = True Then
                        DtMergeOrderTaxCopy.Columns.Remove("TaxCode")
                    End If
                End If
            End If


            Dim dtView As New DataTable
            '  Dim obj As New SpectrumBL.clsCashMemo()
            ' dtView = obj.GetDineCashMemo(vbillno, SiteCode, LangCode)

            If mergid = 0 Then
            Else
                DTMergeOrder = obj.GetMergeOrderBillNo(SiteCode, mergid)
                If Not DTMergeOrder Is Nothing Then
                    If DTMergeOrder.Rows.Count > 0 Then
                        BillNoOfMergeId = DTMergeOrder.Rows(0)("BillNoOfMergeId")
                    End If
                End If
            End If
            dtView = obj.GetDineCashMemo(vbillno, SiteCode, LangCode, MergId:=mergid, BillNoOfMergeId:=BillNoOfMergeId)


            Dim dvItemDetail As New DataView(dtView, "", "", DataViewRowState.CurrentRows)
            'Dim DtUnique As DataTable = dvItemDetail.ToTable(True, "ArticleCode", "DISCRIPTION", "QUANTITY", "SELLINGPRICE", "GROSSAMT", "TOTALDISCOUNT", "TOTALDISCPERCENTAGE", "NETAMOUNT", "TOTALTAXAMOUNT")
            Dim DtUnique As DataTable = obj.GetBillDetailsForDineDataForPrint(vbillno, SiteCode, LangCode, mergid:=mergid, BillNoOfMergeId:=BillNoOfMergeId)


            If Not mergid = 0 Then
                Dim COUNT As Integer = 1
                For Each dr In DtUnique.Rows
                    dr("BillLineNo") = COUNT
                    COUNT = COUNT + 1
                Next
                DtUnique.AcceptChanges()
            End If

            Dim Dtcombo As DataTable = obj.GetComboDetailsDataForDineInPrint(vbillno, SiteCode, LangCode)

            '----- Code Added By Mahesh - Delivery Person and customer Sales Type value pick up from Database 
            If Not IsDBNull(dtView.Rows(0)("ServiceTaxAmount")) AndAlso Val(dtView.Rows(0)("ServiceTaxAmount")) > 0 Then
                CustomerSaleType = GetCustomerSaleType(dtView.Rows(0)("ServiceTaxAmount"))
            Else
                CustomerSaleType = ""
            End If
            If Not IsDBNull(dtView.Rows(0)("DeliveryPersonID")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("DeliveryPersonID")) Then
                DeliveryPersonName = dtView.Rows(0)("DeliveryPersonID").ToString()
            End If

            If DtUnique Is Nothing Then Exit Sub
            'Dim TotaltakeAwayQty = DtUnique.Compute("SUM(TakeAwayQuantity)", "").ToString()
            'If Val(TotaltakeAwayQty) > 0 Then
            '    isTakeAwayQtyPrint = True
            'End If

            Dim StrSubHeader1, StrSubFooter1, strWelcomeMsg, strTaxInformation, strPromotionMsg As New StringBuilder
            PrinttHeaderAndFooter(StrSubHeader1, StrSubFooter1, strWelcomeMsg, strPromotionMsg, strTaxInformation, "CMInvc", _printType)
            Dim tempStrSubFooter1 As String = StrSubFooter1.ToString()

            If Not String.IsNullOrEmpty(tempStrSubFooter1) Then

                StrSubFooter1.Replace(StrSubFooter1.ToString(), tempStrSubFooter1)
            End If


            ' StrSubFooter1.Replace(StrSubFooter1.ToString(), tempStrSubFooter1)
            'StrSubFooter1.Append(tempStrSubFooter1)
            If StrSubFooter1 IsNot Nothing AndAlso Not String.IsNullOrEmpty(StrSubFooter1.ToString()) Then
                StrTagLine = StrSubFooter1.ToString()
            End If

            If Not dtView Is Nothing Then
                strLine = "-".PadRight(LineLength, "-") & vbCrLf
                strDblLine = "=".PadRight(LineLength, "=") & vbCrLf
                'If DuplicatePrinting <> String.Empty Then
                '    StrDuplicate = DuplicatePrinting
                'End If
                'StrDeleteLine = "Cash Memo Status: Deleted"

                'Rakesh-12.09.2013:Issue-7750
                If Not StrSubHeader1 Is Nothing AndAlso StrSubHeader1.ToString().Length > 0 Then
                    'If _printType <> "L40" Then
                    ' StrSubHeader = strLine & SplitString(StrSubHeader1.ToString(), LineLength).ToString().Trim & vbCrLf
                    'modifed by khusrao adil on 12-07-2017 for print format changes -BillNo,Tax Invoice
                    StrSubHeader = SplitString(StrSubHeader1.ToString(), LineLength).ToString().Trim & vbCrLf
                    StrSubHeader = StrSubHeader.PadLeft((Val(LineLength) + StrSubHeader.Length) / 2)
                    StrSubHeader = strLine & StrSubHeader
                    'End If
                    ' StrSubHeader = strLine & SplitString(StrSubHeader1.ToString(), LineLength).ToString().Trim & vbCrLf
                End If

                ' If DuplicatePrinting <> String.Empty Then

                'StrHeader = "<B>" & StrDuplicate.PadLeft(((LineLength - StrDuplicate.Length) / 2) + StrDuplicate.Length, " ") & "</B>" & vbCrLf
                ' End If

                '---Put all code in DCM by Mahesh for performance Optimize ...
                'If Type = "DCM" Then
                '    StrDeleteLine = getValueByKey("CLSCMP001")
                '    StrHeader = StrDeleteLine & vbCrLf
                '    'Request ID: <Update / Delete Request ID>
                '    'Authorized By : <User Name >       Deleted at Time: <CM Time>
                '    'StrHeader = StrHeader & "Request ID    :" & DeletedUserid & vbCrLf
                '    'StrHeader = StrHeader & "Authorized By :" & AuthorisedUser & vbCrLf
                '    StrHeader = StrHeader & getValueByKey("CLSCMP002") & DeletedUserid & vbCrLf
                '    StrHeader = StrHeader & getValueByKey("CLSCMP003") & AuthorisedUser & vbCrLf
                'End If

                Dim SiteName, Site As String
                Dim TS_SiteName As StringBuilder
                'SiteName = "Store Name: " & dtView.Rows(0)("SITEOFFICIALNAME").ToString()
                'Site = "Code: " & dtView.Rows(0)("SITECODE").ToString()
                'StrSiteTelline = "Tel: " & dtView.Rows(0)("TELNO").ToString()
                'StrAdrressLine = "Add: " & dtView.Rows(0)("ADDRESS").ToString()

                SiteName = getValueByKey("CLSCMP004") & " " & dtView.Rows(0)("SITEOFFICIALNAME").ToString()
                SiteName = "<B>" & dtView.Rows(0)("SITEOFFICIALNAME").ToString() & "</B>"
                TS_SiteName = SplitString(SiteName)
                SiteName = TS_SiteName.ToString
                SiteName = SiteName.PadLeft(LineLength / 2 + (SiteName.Length / 2))
                Site = vbCrLf & getValueByKey("CLSCMP005") & " " & dtView.Rows(0)("SITECODE").ToString()
                'SiteLine = SiteName & Site
                'If ClientName <> "" Then
                '    Dim arrClientname = ClientName.Split(" ").ToArray()

                '    For i = 0 To arrClientname.Length - 1
                '        If SiteName.ToUpper.Contains(arrClientname(i).ToString.ToUpper) Then
                '            SiteName = SiteName.ToUpper.Replace(arrClientname(i).ToString.ToUpper.Trim, "").Trim
                '        End If
                '    Next
                'End If

                SiteName = SiteName.PadLeft((Val(LineLength - SiteName.Length) + SiteName.Length) / 2)
                '------------------
                Site = vbCrLf & getValueByKey("CLSCMP005") & " " & dtView.Rows(0)("SITECODE").ToString()
                'SiteLine = SiteName & Site
                'SiteLine = SiteName.Trim.PadLeft((Val(45 - SiteName.Length) + SiteName.Length) / 2)
                SiteLine = SiteName


                ' SiteLine = SiteName
                'SiteLine = SiteName.PadRight(LineLength - Site.Length) & Site

                StrSiteTelline = getValueByKey("CLSCMP006") & " " & dtView.Rows(0)("TELNO").ToString()
                StrSiteTelline = StrSiteTelline.Trim.PadLeft((LineLength / 2 + StrSiteTelline.Length / 2))
                StrAdrressLine = getValueByKey("CLSCMP007").Trim() & " " & dtView.Rows(0)("ADDRESS").ToString().Trim()
                'StrAdrressLine = dtView.Rows(0)("ADDRESS").ToString().Trim()
                ' StrAdrressLine = SplitString(StrAdrressLine, LineLength).ToString()
                'code added for issue id 2148 by vipul
                StrAdrressLine = SplitStringPC(StrAdrressLine, LineLength, NoMoreSpace:=True).ToString()

                StrAdrressLine = StrAdrressLine.Replace("Add.:", "")
                'StrAdrressLine = StrAdrressLine.TrimStart
                'StrAdrressLine = StrAdrressLine.PadLeft(5)
                'StrAdrressLine = StrAdrressLine.Trim.PadLeft(LineLength / 2 + StrAdrressLine.Length / 2)
                'StrSiteReg1Line = "LST NO: " & dtView.Rows(0)("LOCALSALESTAXNO").ToString() & Space(2) & "LST Date: " & dtView.Rows(0)("LOCALSALESTAXDATE").ToString()
                'StrSiteReg2Line = "CST NO: " & dtView.Rows(0)("CENTRALSALESTAXNO").ToString() & Space(2) & "CST Date: " & dtView.Rows(0)("CENTRALSALESTAXDATE").ToString()

                StrHeader &= SiteLine & vbCrLf
                StrHeader = StrHeader & StrAdrressLine.TrimEnd & vbCrLf
                StrHeader = StrHeader & StrSiteTelline & vbCrLf
                'StrHeader = StrHeader & strLine & vbCrLf
                'StrSubHeader = StrSubHeader & StrSiteReg1Line & vbCrLf
                'StrSubHeader = StrSubHeader & StrSiteReg2Line & vbCrLf

                'Dim kotDataLine As String = String.Empty
                '****Added by Rahul 30 nov 2009 night(Rashid) . start 
                If Type = "DCM" Then
                    'StrCMNo = "Void Cash Memo:" & dtView.Rows(0)("Billno").ToString()
                    'modified by khusrao adil on 12-07-2017 for print format changes Bill No,Invoice tax
                    ' StrCMNo = getValueByKey("CLSCMP008") & dtView.Rows(0)("Billno").ToString()
                    StrCMNo = "Void Bill No. :" & dtView.Rows(0)("Billno").ToString()
                Else
                    'StrCMNo = "Cash Memo:" & dtView.Rows(0)("Billno").ToString()
                    'modified by khusrao adil on 12-07-2017 for print format changes Bill No,Invoice tax
                    'StrCMNo = getValueByKey("CLSCMP009") & dtView.Rows(0)("Billno").ToString()
                    StrCMNo = "Bill No. :" & dtView.Rows(0)("Billno").ToString()
                End If
                '******End 

                'StrCMDate = "Date:" & Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())
                'StrCMDate = Format(dtView.Rows(0)("BillTime"), clsCommon.GetSystemDateFormat())

                StrCMDate = dayopenDate.ToShortDateString()
                strDayOpenDate = dayopenDate.ToShortDateString()
                'If DuplicatePrinting <> String.Empty Then
                StrCMDate = Format(dtView.Rows(0)("BillDate"), clsCommon.GetSystemDateFormat())
                ' End If
                If PrintCouponNumberForKOT = True Then
                    'Dim CouponNo As String = strTillno.Substring(0, 1) & strTillno.Substring(strTillno.Length - 2, 2) & strBillno.Substring(strBillno.Length - 2, 2)
                    Dim tillNo As String = dtView.Rows(0)("TerminalId").ToString()
                    Dim billno As String = dtView.Rows(0)("Billno").ToString()
                    Dim CouponNo As String = tillNo.Substring(0, 1) & tillNo.Substring(tillNo.Length - 2, 2) & billno.Substring(billno.Length - 2, 2)
                    StrTokenNo = CouponNo
                Else
                    StrTokenNo = dtView.Rows(0)("Billno").ToString().Trim.Substring(dtView.Rows(0)("Billno").ToString().Trim.Length - 2, 2)
                End If

                StrTokenNo = "<B>" & StrTokenNo & "</B>"

                StrCashMemoLine = StrCMNo.PadRight(LineLength - 1 - StrCMDate.Length) & StrCMDate & vbCrLf
                'StrCashier = "Cashier:" & dtView.Rows(0)("Createdby").ToString()
                'StrCMTime = "Time:" & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")

                'StrCashier = getValueByKey("CLSPV006") & dtView.Rows(0)("Createdby").ToString()
                StrCashier = getValueByKey("CLSPV006") & LoginUserName
                StrCMTime = getValueByKey("CLSCMP032") & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")
                StrCashierLine = StrCashier.PadRight(LineLength - 1 - StrCMTime.Length) & StrCMTime & vbCrLf
                'StrSalesPerson = "Sales Person:" & dtView.Rows(0)("SALESEXECUTIVECODE").ToString()
                StrSalesPerson = getValueByKey("CLSCMP010") & dtView.Rows(0)("SALESEXECUTIVECODE").ToString()

                If _SalesPerApplicable = True Then
                    StrSalesPersonLine = StrSalesPerson & vbCrLf
                End If
                Dim _tableNo As String = "Table No. " & Tableno 'added by khusrao adil on 01-08-2018 for sharda restaurent
                Dim _tempCustomerSaleType As String = "<B>" & CustomerSaleType & "</B>"
                If (dtView.Rows.Count() > 0 And String.IsNullOrEmpty(DeliveryPersonName)) Then
                    If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                        ' strPrintMsg.Append(StrTokenNo)
                        StrTokenNo = "Token No. " & StrTokenNo
                        If Not AllowBillingOnlyAfterSelectionOfSalesType Then
                            '   strPrintMsg.Append(CustomerSaleType)
                            CustomerSaleType = String.Empty
                            _tempCustomerSaleType = String.Empty
                        End If
                        Dim StrTokenSalesTypeLine = ""
                        If TableNoRequiredInDineInModuleBillPrint Then 'added by khusrao adil on 01-08-2018 for sharda restaurent
                            StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - (_tempCustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "")).Length) & _tempCustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "")
                            StrTokenSalesTypeLine = StrTokenSalesTypeLine.TrimEnd & _tableNo.PadLeft(StrTokenSalesTypeLine.Length - 6 - _tableNo.Length) & vbCrLf
                        Else
                            StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - (_tempCustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "")).Length) & _tempCustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "") & vbCrLf
                        End If

                        StrSubHeader = StrSubHeader & strLine
                        StrSubHeader = StrSubHeader & StrTokenSalesTypeLine
                    Else
                        If AllowBillingOnlyAfterSelectionOfSalesType Then
                            If TableNoRequiredInDineInModuleBillPrint Then 'added by khusrao adil on 01-08-2018 for sharda restaurent
                                StrSubHeader = _tempCustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "") & _tableNo.PadLeft(LineLength - _tempCustomerSaleType.Length - _tableNo.Length) & vbCrLf   ''StrSubHeader &
                            Else
                                StrSubHeader = _tempCustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "") & vbCrLf   ''StrSubHeader &
                            End If
                        Else
                            If TableNoRequiredInDineInModuleBillPrint Then 'added by khusrao adil on 01-08-2018 for sharda restaurent
                                StrSubHeader = _tempCustomerSaleType + _tableNo.PadLeft(LineLength - _tableNo.Length - _tempCustomerSaleType.Length) + vbCrLf   ''StrSubHeader &
                            Else
                                StrSubHeader = _tempCustomerSaleType + vbCrLf  ''StrSubHeader &
                            End If
                        End If
                    End If
                Else
                    If AllowBillingOnlyAfterSelectionOfSalesType Then
                        StrSubHeader = StrSubHeader & _tempCustomerSaleType & IIf(isTakeAwayQtyPrint, ",Take Away", "") + vbCrLf
                    Else
                        If TableNoRequiredInDineInModuleBillPrint Then 'added by khusrao adil on 01-08-2018 for sharda restaurent
                            StrSubHeader = _tempCustomerSaleType + _tableNo.PadLeft(LineLength - _tableNo.Length - _tempCustomerSaleType.Length) + vbCrLf   ''StrSubHeader &
                        Else
                            StrSubHeader = StrSubHeader & _tempCustomerSaleType + vbCrLf
                        End If

                    End If
                End If

                StrSubHeader = StrSubHeader & strLine
                StrSubHeader = StrSubHeader & StrCashMemoLine
                StrSubHeader = StrSubHeader & StrCashierLine

                If (Not String.IsNullOrEmpty(dtView.Rows(0)("SALESEXECUTIVECODE").ToString())) Then
                    StrSubHeader = StrSubHeader & StrSalesPersonLine
                    'StrSubHeader = StrSubHeader & strLine
                End If

                Dim CLPaddress As String = ""
                Dim ReminderComments As String = ""
                Dim CLPBalancePoints As String
                If dtView.Rows(0)("CLPNO").ToString() <> String.Empty Then
                    'CLpLine = "CLP Card No:" & dtView.Rows(0)("CLPNO").ToString()
                    'CustomerNameLine = "Customer Name: " & dtView.Rows(0)("CLPNAME").ToString()
                    'CLPPointsLine = "CLP Points Earned: " & FormatNumber(dtView.Rows(0)("CLPPoints").ToString())

                    CLpLine = getValueByKey("CLSCMP011") & dtView.Rows(0)("CLPNO").ToString()
                    Dim dtCustomer As New DataTable
                    dtCustomer.Clear()
                    If dtView.Rows(0)("CLPNAME").ToString().Trim <> String.Empty Then
                        CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CLPNAME").ToString()
                    Else
                        dtCustomer = obj.GetDineCustomers(vbillno, SiteCode)
                        If Not dtCustomer Is Nothing AndAlso dtCustomer.Rows.Count > 0 Then
                            CustomerNameLine = getValueByKey("CLSCMP012") & dtCustomer.Rows(0)("CLPNAME").ToString()
                        End If

                    End If

                    'If dtView.Columns.Contains("CLPPoints") Then
                    '    CLPPointsLine = getValueByKey("CLSCMP013") & FormatNumber(Val(dtView.Rows(0)("CLPPoints").ToString()))
                    'End If


                    'If (dtView.Rows(0)("RedemptionPoints") IsNot DBNull.Value AndAlso dtView.Rows(0)("RedemptionPoints") > 0) Then
                    '    CLPPointsLine += vbCrLf + getValueByKey("CLSCMP034") & FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()))
                    'End If

                    'CLPaddress = dtView.Rows(0)("CLPAddress").ToString()
                    'If CLPaddress <> String.Empty Then
                    '    CLPaddress = getValueByKey("CLSCMP014") & dtView.Rows(0)("CLPAddress").ToString()
                    'End If
                    'ReminderComments = dtView.Rows(0)("ReminderComments").ToString()

                    'CLPBalancePoints = String.Empty
                    'CLPBalancePoints = getValueByKey("CLSCMP015")
                    ' Dim dblCLPPoints As Double

                    'dblCLPPoints = Val(dtView.Rows(0)("BalancePoints").ToString())
                    'For Each drRow As DataRow In dtView.Select("Tendertypecode = 'CLPPoint'")
                    '    dblCLPPoints -= drRow("RedemptionPoints")
                    'Next

                    ''Dim RedemptionPoints As DataRow = dtView.Select("Tendertypecode = 'CLPPoint'").FirstOrDefault()
                    'If (RedemptionPoints IsNot Nothing AndAlso RedemptionPoints("RedemptionPoints") IsNot DBNull.Value) Then
                    '    dblCLPPoints -= Val(RedemptionPoints("RedemptionPoints").ToString())
                    'End If

                    ' CLPBalancePoints = CLPBalancePoints & FormatNumber(dblCLPPoints.ToString())
                    If CLPaddress <> String.Empty Then
                        CLPaddress = SplitStringPC(CLPaddress, LineLength, NoMoreSpace:=True).ToString()
                    End If
                    'If CLpLine <> Nothing AndAlso CLpLine.ToString().Length > 0 Then ' 3 lines commented for clp details
                    '    StrSubFooter = StrSubFooter & CLpLine & vbCrLf
                    'End If
                    CustmerPhoneNo = getValueByKey("RP187") & " " & dtView.Rows(0)("Mobileno").ToString()
                    StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
                    'StrSubFooter = StrSubFooter & CLPBalancePoints & vbCrLf ''' 2 lines commented for clp details
                    If CustomerSaleType = "Home Delivery" Then
                        StrSubFooter = StrSubFooter & CustmerPhoneNo & vbCrLf
                    End If

                    'StrSubFooter = StrSubFooter & CustmerPhoneNo & vbCrLf
                    'StrSubFooter = StrSubFooter & CLPPointsLine & vbCrLf

                    Dim retAmt As Double
                    'If CDbl(PaidAmt) >= CDbl(BillAmt) Then
                    '    retAmt = CDbl(PaidAmt) - CDbl(BillAmt)
                    'End If
                    StrSubFooter = StrSubFooter & CLPaddress & vbCrLf
                    'If EvasPizzaChanges = True AndAlso Not String.IsNullOrEmpty(ReminderComments) Then
                    '    StrSubFooter = StrSubFooter & SplitStringPC("Cust. Instructions: " & ReminderComments, LineLength, NoMoreSpace:=True).ToString() & vbCrLf
                    'End If
                    If (dtView.Rows.Count() > 0 And String.IsNullOrEmpty(DeliveryPersonName)) Then
                        If Val(NetAmt) > 0 Then
                            StrSubFooter = StrSubFooter & getValueByKey("CLIST04") & " " & FormatNumber(CDbl(NetAmt), 2) & vbCrLf
                            StrSubFooter = StrSubFooter & getValueByKey("CLIST05") & " " & FormatNumber(CDbl(retAmt), 2) & vbCrLf
                        End If
                    End If

                ElseIf dtView.Columns.Contains("CustomerNo") AndAlso dtView.Rows(0)("CustomerNo").ToString() <> String.Empty Then


                    CLpLine = getValueByKey("CLSCMP038") & dtView.Rows(0)("CustomerNo").ToString()

                    CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CustomerName").ToString()
                    If CLpLine <> Nothing AndAlso CLpLine.ToString().Length > 0 Then
                        StrSubFooter = StrSubFooter & CLpLine & vbCrLf
                    End If
                    StrSubFooter = StrSubFooter & CustomerNameLine & vbCrLf
                ElseIf dtView.Columns.Contains("CustomerNo") AndAlso dtView.Rows(0)("CustomerNo").ToString().Trim = String.Empty Then
                    If dtView.Rows(0)("CLPNO").ToString().Trim <> String.Empty Then
                        Dim dtCustomer As New DataTable
                        dtCustomer.Clear()
                        If dtView.Rows(0)("CLPNAME").ToString().Trim <> String.Empty Then
                            CustomerNameLine = getValueByKey("CLSCMP012") & dtView.Rows(0)("CLPNAME").ToString()
                        Else
                            dtCustomer = obj.GetDineCustomers(vbillno, SiteCode)
                            CustomerNameLine = getValueByKey("CLSCMP012") & dtCustomer.Rows(0)("CLPNAME").ToString()
                        End If
                    End If


                End If

                If Type = "CMWAmt" Then
                    'LineItemHeading = "Item Code" & Space(17) & "Item Name" & Space(1) & "Qty " & vbCrLf
                    ' LineItemHeading = getValueByKey("CLSPSO020")  & vbCrLf & getValueByKey("CLSCMP016") & Space(1) & getValueByKey("CLSPSO022") & " " & vbCrLf
                    'to resolve Issue 448 
                    LineItemHeading = getValueByKey("CLSPSO020") & Space(17) & vbCrLf & getValueByKey("CLSCMP016") & Space(20) & getValueByKey("CLSPSO022") & " " & vbCrLf
                Else
                    'LineItemHeading = "Item Code" & Space(17) & "Item Name" & Space(5) & vbCrLf
                    'to resolve Issue 448 
                    'LineItemHeading = "  Item Code" & vbCrLf & "  Item Name" & vbCrLf
                    'LineItemHeading = LineItemHeading & Space(3) & "Qty " & Space(2) & "Price" & Space(4) & "Disc." & Space(4) & "Tax" & Space(5) & "Net" & vbCrLf

                    'LineItemHeading = "  " & getValueByKey("CLSPSO020") & vbCrLf & "  " & getValueByKey("CLSCMP016") & vbCrLf
                    'LineItemHeading = LineItemHeading & Space(3) & getValueByKey("CLSPSO022") & " " & Space(2) & getValueByKey("CLSPSO023") & Space(4) & getValueByKey("CLSPSO024") & Space(4) & getValueByKey("CLSPSO025") & Space(5) & getValueByKey("CLSPSO026") & vbCrLf
                    LineItemHeading = getValueByKey("CLSCMP016") & Space(13) & getValueByKey("CLSPSO022") & " " & Space(5)

                    LineItemHeading &= "Amt" & vbCrLf

                End If


                Dim TotalQty, TGrossAmt, TNetAmt, TDiscAmt, TRateAfterDisc, TTaxtAmt As String
                Dim BillLineNO As String

                For Each dr As DataRow In DtUnique.Rows
                    ItemCode = dr("ArticleCode").ToString()
                    If (DisplayArticleFullName) Then
                        Desc = dr("ArticleFullName").ToString()
                    Else
                        Desc = dr("DISCRIPTION").ToString()
                    End If
                    BillLineNO = dr("BillLineNO").ToString()
                    If SpectrumCommon.ComboItemPrintingAllowed AndAlso Dtcombo IsNot Nothing AndAlso Dtcombo.Rows.Count > 0 Then
                        Dim drp() = Dtcombo.Select("ComboArticleCode='" & ItemCode & "' AND BillLineNO ='" & BillLineNO & "'")
                        If drp.Length > 0 Then
                            Dim articleDescriptionDictionary As New Dictionary(Of String, Integer)
                            For index = 0 To drp.Length - 1
                                If articleDescriptionDictionary.ContainsKey(drp(index)("ArticleName")) Then
                                    articleDescriptionDictionary(drp(index)("ArticleName")) = articleDescriptionDictionary(drp(index)("ArticleName")) + drp(index)("Quantity")
                                Else
                                    articleDescriptionDictionary.Add(drp(index)("ArticleName"), drp(index)("Quantity"))
                                End If
                            Next
                            Dim AddedDesc = GetMultilinedString(articleDescriptionDictionary)
                            Desc = Desc & vbCrLf & AddedDesc
                        End If
                    End If


                    Qty = dr("Quantity").ToString()
                    Rate = dr("SellingPrice").ToString()
                    RateAfterDisc = (dr("SellingPrice") - dr("TOTALDISCOUNT")).ToString()
                    If CDbl(clsCommon.CheckIfBlank(Rate)) < 0 Then
                        Rate = CDbl(clsCommon.CheckIfBlank(Rate)) * -1
                    End If
                    DiscAmt = dr("TOTALDISCOUNT").ToString()
                    DiscPer = dr("TOTALDISCPERCENTAGE").ToString()
                    NetAmt = dr("NetAmount").ToString()
                    GrossAmt = dr("GROSSAMT").ToString()
                    TaxAmt = dr("TOTALTAXAMOUNT").ToString()
                    'Dim i As Integer = 0
                    'For i = ItemCode.Length To 26
                    '    ItemCode = ItemCode & " "
                    'Next
                    'For i = Desc.Length To 14
                    '    Desc = Desc & " "
                    'Next

                    If (AllowDecimalQty) Then
                        If (WeightScaleEnabled) Then
                            Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 3)
                        Else
                            Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 2)
                        End If
                    Else
                        Qty = FormatNumber(CDbl(clsCommon.CheckIfBlank(Qty)), 0)
                    End If

                    'For i = Qty.Length To 4
                    '    Qty = " " & Qty
                    'Next
                    Rate = FormatNumber(CDbl(clsCommon.CheckIfBlank(Rate)), 2)

                    RateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(RateAfterDisc)), 2)
                    'Rate = FormatCurrency(Rate, 2)
                    'For i = Rate.Length To 8
                    '    Rate = " " & Rate
                    'Next
                    DiscAmt = FormatNumber(CDbl(IIf(DiscAmt <> String.Empty, DiscAmt, 0)), 2)
                    'For i = DiscAmt.Length To 8
                    '    DiscAmt = " " & DiscAmt
                    'Next
                    DiscPer = FormatNumber(CDbl(IIf(DiscPer <> String.Empty, DiscPer, 0)), 2)
                    DiscPer = DiscPer & "%"
                    'For i = DiscPer.Length To 8
                    '    DiscPer = " " & DiscPer
                    'Next
                    NetAmt = FormatNumber(CDbl(NetAmt), 2)
                    GrossAmt = FormatNumber(CDbl(GrossAmt), 2)
                    'For i = NetAmt.Length To 8
                    '    NetAmt = " " & NetAmt
                    'Next
                    TaxAmt = FormatNumber(CDbl(IIf(TaxAmt <> String.Empty, TaxAmt, 0)), 2)
                    'For i = TaxAmt.Length To 7
                    '    TaxAmt = " " & TaxAmt
                    'Next

                    Dim TempNetAmt As String = "0"

                    If Type = "CMWAmt" Then
                        StrLineItem = ItemCode.PadRight(26) & vbCrLf & Desc.PadRight(35) & Qty & vbCrLf
                    Else
                        If 28 - Desc.Length < Qty.Length Then
                            StrLineItem = Desc & vbCrLf
                            StrLineItem = StrLineItem & Qty.PadLeft(28) & GrossAmt.PadLeft(11) & vbCrLf
                        Else
                            StrLineItem = Desc & Qty.PadLeft(28 - Desc.Length) & GrossAmt.PadLeft(11) & vbCrLf
                        End If
                    End If
                    strLineDetail.Append(StrLineItem)

                    If CDbl(Qty.Trim) > 0 Then
                        TotalQty = CDbl(clsCommon.CheckIfBlank(TotalQty)) + CDbl(Qty.Trim)
                    End If
                    TDiscAmt = CDbl(clsCommon.CheckIfBlank(TDiscAmt)) + CDbl(DiscAmt.Trim)
                    TGrossAmt = CDbl(clsCommon.CheckIfBlank(TGrossAmt)) + CDbl(dr("GROSSAMT").ToString())
                    TNetAmt = CDbl(clsCommon.CheckIfBlank(TNetAmt)) + CDbl(NetAmt.Trim)
                    TRateAfterDisc = CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)) + CDbl(RateAfterDisc.Trim)
                    TTaxtAmt = CDbl(clsCommon.CheckIfBlank(TTaxtAmt)) + CDbl(TaxAmt.Trim)
                Next

                TotalQty = FormatNumber(CDbl(clsCommon.CheckIfBlank(TotalQty)), 2)
                TDiscAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TDiscAmt)), 2)
                TGrossAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TGrossAmt)), 2)
                TNetAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TNetAmt)), 2)
                TRateAfterDisc = FormatNumber(CDbl(clsCommon.CheckIfBlank(TRateAfterDisc)), 2)
                TTaxtAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TTaxtAmt)), 2)
                StrBody = strLineDetail.ToString() & vbCrLf
                StrBody = StrBody & strLine
                strTotalToPay = Math.Round(CDbl(clsCommon.CheckIfBlank(TNetAmt)), 0, MidpointRounding.AwayFromZero)
                'TotalQtyLine = "Total Qty" & Space(3) & ":" & TotalQty
                'TotalQtyLine = getValueByKey("CLSCMP017") & Space(3) & ":" & TotalQty
                'StrBody = StrBody & TotalQtyLine & vbCrLf
                Dim TSubTotalAmt As String
                If Type <> "CMWAmt" Then
                    If CDbl(clsCommon.CheckIfBlank(TDiscAmt)) > 0 Then
                        Dim TotalDisPercent As String = Math.Round((CDbl(clsCommon.CheckIfBlank(TDiscAmt)) / CDbl(clsCommon.CheckIfBlank(TGrossAmt))) * 100, 1).ToString()
                        TSubTotalAmt = FormatNumber(CDbl(clsCommon.CheckIfBlank(TGrossAmt)) - CDbl(clsCommon.CheckIfBlank(TDiscAmt)))
                        TSubTotalAmt = TSubTotalAmt & " " & Currency
                        SubTotalLine = "Sub Total".PadRight(LineLength - 16) & ":" & TSubTotalAmt.PadLeft(14)
                        TDiscAmt = TDiscAmt & " " & Currency
                        TotalDiscAmtLine = String.Format(getValueByKey("CLSCMP019"), TotalDisPercent).PadRight(LineLength - 16) & ":" & TDiscAmt.PadLeft(14)
                    End If
                    TGrossAmt = TGrossAmt & " " & Currency
                    TNetAmt = TNetAmt & " " & Currency
                    TRateAfterDisc = TRateAfterDisc & " " & Currency
                    TTaxtAmt = TTaxtAmt & " " & Currency

                    TotalGrossAmtLine = "Total Amt".PadRight(LineLength - 14) & ":" & TGrossAmt.PadLeft(14)

                    NetAmtLine = "Total ".PadRight(LineLength - 14) & ":" & TNetAmt.PadLeft(14)
                    If _TaxDetail = True Then
                        '  Dim taxDetailsForBill As DataTable = obj.GetTaxDetailsForDineInBillNo(SiteCode, dtView.Rows(0)("Billno").ToString())
                        Dim taxDetailsForBill As DataTable
                        If mergid = 0 Then
                            taxDetailsForBill = obj.GetTaxDetailsForDineInBillNo(SiteCode, dtView.Rows(0)("Billno").ToString())
                        Else
                            taxDetailsForBill = DtMergeOrderTaxCopy
                        End If

                        If taxDetailsForBill IsNot Nothing AndAlso taxDetailsForBill.Rows.Count > 0 Then
                            For Each row As DataRow In taxDetailsForBill.Rows
                                'If CompositeTaxReqOnPrint = False AndAlso IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                                If IsDBNull(row("TaxType")) = False AndAlso row("TaxType") = "Composite" Then Continue For
                                Dim taxValue As String = row("TaxVAlue").ToString()
                                taxValue = FormatNumber(CDbl(clsCommon.CheckIfBlank(taxValue)), 2)
                                taxValue = taxValue & " " & Currency
                                FooterTaxLine = FooterTaxLine & row("TaxName").PadRight(LineLength - 16) & ":" & taxValue.PadLeft(14) & vbCrLf
                            Next
                        End If
                    End If
                    TotalFooterTaxDetail = FooterTaxLine
                    StrBody = StrBody & TotalGrossAmtLine & vbCrLf
                    If TotalDiscAmtLine IsNot Nothing Then
                        StrBody = StrBody & TotalDiscAmtLine & vbCrLf
                    End If
                    If SubTotalLine IsNot Nothing Then
                        StrBody = StrBody & strLine.Substring(30) & SubTotalLine & vbCrLf
                    End If

                    StrBody = StrBody & TotalFooterTaxDetail & strLine.Substring(30) & NetAmtLine & vbCrLf & strLine

                    TenderLine = getValueByKey("CLSCMP021") & vbCrLf
                    'Dim dtTender As DataTable = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                    Dim dtTender As New DataTable
                    ' If KOTAndBillGeneration = True Then
                    'dtTender = obj.GetTenderTypeByRefBillNo(dtView.Rows(0)("BILLNO").ToString())
                    ' Else
                    dtTender = dvItemDetail.ToTable(True, "TENDERHEADNAME", "AMOUNTTENDERED", "AMOUNTINCURRENCY", "CurrencyCode", "CMRecptLineno", "CardNO", "TENDERTYPECODE")
                    ' End If
                    For Each drTender As DataRow In dtTender.Rows
                        If UCase(obj.GetDefaultConfigValue(SiteCode, "CVInfoReqOnPrint")) = "FALSE" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)") Then
                            Continue For
                        End If
                        Dim tender As String = FormatNumber(CDbl(clsCommon.CheckIfBlank(drTender("AMOUNTTENDERED").ToString()))) & " " & drTender("CurrencyCode").ToString() & vbCrLf
                        Dim tenderName As String
                        'Added by Rohit
                        If drTender("CardNO").ToString() <> "" And (drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(I)" Or drTender("TENDERTYPECODE").ToString() = "CreditVouc(R)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(I)" Or drTender("TENDERTYPECODE").ToString() = "GiftVoucher(R)") Then
                            tenderName = drTender("TENDERHEADNAME").ToString() & "(" & drTender("CardNO").ToString() & ") "
                        Else
                            tenderName = drTender("TENDERHEADNAME").ToString()
                        End If
                        'End editing
                        If Not drTender("AMOUNTINCURRENCY") Is DBNull.Value AndAlso drTender("AMOUNTINCURRENCY") <> 0 Then '0000404
                            tender = FormatNumber(CDbl(drTender("AMOUNTINCURRENCY").ToString()), 2) & " " & drTender("CurrencyCode").ToString() & vbCrLf    '''
                        End If
                        'tender = Format(CDbl(tender), "0.00")

                        'TenderDetails = tenderName.PadRight(LineLength - 19) & tender.PadLeft(20) 
                        '--- Code Added By Mahesh changes by on MOD clp changes
                        If drTender("TENDERTYPECODE").ToString() = "CLPPoint" Then
                            tender = FormatNumber(Val(dtView.Rows(0)("RedemptionPoints").ToString()))
                        End If

                        TenderDetails = String.Format("{0}{1}{2}", tenderName.Trim(), Space(LineLength - (tenderName.Trim().Length + tender.Trim().Length)), tender.Trim()) & vbCrLf
                        TenderLine = TenderLine & TenderDetails
                    Next
                    ' StrBody = StrBody & TenderLine
                    strTotalToPay = strTotalToPay & " " & Currency
                    strTotalToPay = "<B>" & getValueByKey("CLSCMP020").PadRight(LineLength - 30) & ":" & strTotalToPay.PadLeft(14) & "</B>"
                    StrBody = StrBody & strTotalToPay & vbCrLf '& TenderLine
                    'Rakesh-13.09.2013:KSL CR-7860--> Display total discount amount 
                    If (_IsDisplayTotalSaving) Then
                        If (Not String.IsNullOrEmpty(TDiscAmt) AndAlso Not String.Equals(TDiscAmt, "0.00")) Then
                            Dim totalSavingAmount As String = String.Format(getValueByKey("CLSCMP033"), Currency, TDiscAmt.Replace(Currency, String.Empty).Trim)
                            StrBody = StrBody & vbCrLf & SplitString(totalSavingAmount).ToString().Trim & vbCrLf & strLine & vbCrLf
                        Else
                            StrBody = StrBody & vbCrLf
                        End If
                    Else
                        StrBody = StrBody & vbCrLf
                    End If

                End If

                'Company and Tin Info
                Dim NoOfReprints As String = IIf(IsDBNull(dtView.Rows(0)("NoofReprints")), String.Empty, dtView.Rows(0)("NoofReprints")).ToString()
                Dim CompanyName As String = dtView.Rows(0)("SITEOFFICIALNAME") 'obj.GetDefaultConfigValue(Sitecode, "CompanyName")

                StrCompanyInfo = String.Empty
                If Not String.IsNullOrEmpty(CompanyName) Then
                    ' StrCompanyInfo = CompanyName & vbCrLf
                End If

                'Dim strtna = getValueByKey("cashmemoprint.tin") & obj.GetTinNumberForASite(Sitecode)
                'added by khusrao adil on 04-07-2017 for GST No
                Dim GstNo = obj.GetTinNumberForASite(SiteCode)
                If (Not String.IsNullOrEmpty(GstNo)) AndAlso GstNo <> "0" Then
                    Dim strtna = "GST No.: " & GstNo
                    strtna = SplitString(strtna, LineLength).ToString() & vbCrLf
                    If Not String.IsNullOrEmpty(strtna) Then
                        strtna = strtna.Trim() & vbCrLf
                        StrCompanyInfo += strtna '.Trim()
                    End If
                End If

                If (Not String.IsNullOrEmpty(StrTagLine)) Then
                    StrCompanyInfo += StrTagLine.TrimEnd() & vbCrLf
                End If

                'Dim remarks As String = dtView.Rows(0)("Remark")
                'If Not IsDBNull(dtView.Rows(0)("Remark")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("Remark")) Then
                '    Dim remarks As String = dtView.Rows(0)("Remark")
                '    remarks = SplitString(getValueByKey("cashmemoprint.discountremark") & remarks, LineLength).ToString()
                '    StrCompanyInfo = StrCompanyInfo & remarks.TrimEnd() & vbCrLf
                'End If

                If Not String.IsNullOrEmpty(NoOfReprints) Then
                    StrCompanyInfo = StrCompanyInfo & "Re-Print Number : " & NoOfReprints & vbCrLf
                    StrCompanyInfo = StrCompanyInfo & "Re-Print Date : " & DateTime.Now.ToShortDateString() & vbCrLf
                End If
                'Company and Tin Info End

                Dim dtPromo As DataTable = dvItemDetail.ToTable(True, "PROMOTIONALMSGID", "PROMOMESS")
                For Each drTerms As DataRow In dtPromo.Select("", "PROMOTIONALMSGID", DataViewRowState.CurrentRows)
                    If Not String.IsNullOrEmpty(drTerms("PROMOMESS").ToString()) Then
                        PromoMsgLine = PromoMsgLine & drTerms("PROMOMESS").ToString() & vbCrLf
                    End If
                Next

                TotalPromotionalMsg = PromoMsgLine

                'If Not IsSavoy Then
                '    Dim dtTerms As DataTable = dvItemDetail.ToTable(True, "TNCSRNO", "Terms")
                '    For Each drTerms As DataRow In dtTerms.Select("", "TNCSRNO", DataViewRowState.CurrentRows)
                '        If Not String.IsNullOrEmpty(drTerms("Terms").ToString()) Then
                '            TermsNcond = TermsNcond & drTerms("Terms").ToString() & vbCrLf
                '        End If
                '    Next
                'End If

                strPrintMsg = New StringBuilder()
                sbDeliveryBillPrint = New StringBuilder()

                If dtView.Rows.Count() > 0 Then
                    '---- Changes By Mahesh : Customer Name is Not Mandatory for Home Delivery 
                    'If dtView.Rows(0)("HDName").ToString() <> String.Empty Then
                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        '   strHomeDilery = strHomeDilery & getValueByKey("CLSCMP022") & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf 'commeeneted by vipin
                        'strHomeDilery = strHomeDilery & getValueByKey("CLSCMP023") & dtView.Rows(0)("HDDeliveryDate").ToString() & vbCrLf
                        strHomeDilery = strHomeDilery & getValueByKey("CLSCMP035") & DeliveryPersonName & vbCrLf
                        'strHomeDilery = strHomeDilery & SplitString(getValueByKey("CLSCMP024") & dtView.Rows(0)("HDAddress").ToString(), LineLength).ToString() & vbCrLf
                        'strHomeDilery = strHomeDilery & getValueByKey("CLSCMP025") & dtView.Rows(0)("HDTelNo").ToString() & vbCrLf
                        'strHomeDilery = strHomeDilery & getValueByKey("CLSCMP026") & dtView.Rows(0)("HDEmail").ToString() & vbCrLf

                    Else
                        If _CustomerNameRequiredInKOT AndAlso CustomerNameLine <> Nothing Then
                            StrCustomerName = CustomerNameLine.ToUpper.Replace("CUSTOMER", "").Trim()

                        End If
                        If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                            If Not AllowBillingOnlyAfterSelectionOfSalesType Then
                                CustomerSaleType = String.Empty
                            End If
                            If CustomerSaleType <> Nothing Then
                                Dim StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - CustomerSaleType.Length) & CustomerSaleType & vbCrLf & vbCrLf
                            End If
                        End If
                    End If
                End If


                StrFooter = StrFooter & TotalPromotionalMsg '& vbCrLf
                StrFooter = StrFooter & TermsNcond '& vbCrLf


                strPrintMsg.Append(StrHeader)
                '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                sbDeliveryBillPrint.Append(StrHeader)
                'strPrintMsg.Append(strLine)


                If StrSubHeader IsNot Nothing AndAlso Not String.IsNullOrEmpty(StrSubHeader.ToString()) Then
                    strPrintMsg.Append(StrSubHeader)
                    '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                    sbDeliveryBillPrint.Append(StrSubHeader)
                End If

                If (Not String.IsNullOrEmpty(strWelcomeMsg.ToString())) Then
                    strPrintMsg.Append(strLineL40 + vbCrLf)
                    strPrintMsg.Append(strWelcomeMsg.ToString())
                    '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                    sbDeliveryBillPrint.Append(strLineL40 + vbCrLf)
                    sbDeliveryBillPrint.Append(strWelcomeMsg.ToString())
                End If

                '---- Added By Mahesh maintaining seperate DeliveryBillPrint

                sbDeliveryBillPrint.Append(strDblLine)
                strPrintMsg.Append(strDblLine)
                strPrintMsg.Append(LineItemHeading)
                strPrintMsg.Append(strDblLine)
                strPrintMsg.Append(StrBody)

                '---- Added By Mahesh maintaining seperate DeliveryBillPrint
                sbDeliveryBillPrint.Append(LineItemHeading)
                sbDeliveryBillPrint.Append(strDblLine)
                sbDeliveryBillPrint.Append(StrBody)

                strPrintMsg.Append(StrCompanyInfo)
                strPrintMsg.Append(strLine)

                If strTaxInformation IsNot Nothing AndAlso Not String.IsNullOrEmpty(strTaxInformation.ToString()) Then
                    strPrintMsg.Append(strTaxInformation.ToString().TrimEnd())
                    strPrintMsg.Append(vbCrLf + strLineL40 + vbCrLf)
                End If

                If strPromotionMsg IsNot Nothing AndAlso Not String.IsNullOrEmpty(strPromotionMsg.ToString()) Then
                    strPrintMsg.Append(strPromotionMsg.ToString().TrimEnd())
                    strPrintMsg.Append(vbCrLf + strLineL40 + vbCrLf)
                End If

                If StrSubFooter IsNot Nothing AndAlso Not String.IsNullOrEmpty(StrSubFooter.ToString()) Then
                    strPrintMsg.Append(StrSubFooter.ToString())
                End If

                strPrintMsg.Append(StrFooter)

                If (Not String.IsNullOrEmpty(strHomeDilery)) Then
                    strPrintMsg.Append(strLineL40 & vbCrLf)
                    strPrintMsg.Append(strHomeDilery)

                    ' ---- Added By Mahesh maintaining seperate DeliveryBillPrint
                    '--- Changes By Mahesh Not required in HD Print
                    'sbDeliveryBillPrint.Append(vbCrLf)
                    'sbDeliveryBillPrint.Append(strHomeDilery)

                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        'sbDeliveryBillPrint.Append(strPrintMsg)
                        sbDeliveryBillPrint.Append(strLineL40 & vbCrLf)
                        'If EvasPizzaChanges AndAlso Not String.IsNullOrEmpty(ReminderComments) Then
                        '    sbDeliveryBillPrint.Append(SplitString("Customer Comments: " & ReminderComments, LineLength).ToString() & vbCrLf)
                        'End If
                        '--- Changes By Mahesh Not required in HD Print
                        sbDeliveryBillPrint.Append(getValueByKey("CLSCMP035") & DeliveryPersonName & vbCrLf)
                    End If
                End If



                'code added for Sbarro and Naturals, both print are same in case of home delivery
                sbDeliveryBillPrint.Remove(0, sbDeliveryBillPrint.Length - 1)
                sbDeliveryBillPrint.Append(Space(12) & "Delivery Copy" & vbCrLf)
                sbDeliveryBillPrint.Append(strPrintMsg)


                'If GiftMsg <> String.Empty Then
                '    GiftMsg = SplitString(GiftMsg).ToString()
                '    strPrintMsg.Append(GiftMsg.TrimEnd() & vbCrLf)
                '    strPrintMsg.Append(strLine)

                '    sbDeliveryBillPrint.Append(GiftMsg.TrimEnd() & vbCrLf & strLine)
                'End If
                'code  added for issue id 1530 by vipul
                Dim reason As String = ""
                ' reason = "reason:-" & vbCrLf & dtView.Rows(0)("AuthUserRemarks").ToString & vbCrLf
                'If dtView.Rows(0)("AuthUserRemarks").ToString <> String.Empty Then
                '    strPrintMsg.Append(strLine)
                '    strPrintMsg.Append(SplitString(reason))

                'End If


                PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")

                If PrinterName = Nothing Then
                    Exit Sub
                End If
                Dim msg As String = String.Empty
                If _PrintPreview = True Then

                    If (KOTBillPrintingRequired) Then
                        'Call CashMemoKOTPrint(Type, ErrorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, CustomerComments:=ReminderComments, EvassPizza:=EvasPizzaChanges)
                    End If
                    'PrinterName = "HP LaserJet P3005 PCL6"
                    'If EvasPizzaChanges Then
                    '    Dim s As C1BarCode = GetBarcode(CashMemoNo)
                    '    VarBarcode = s
                    '    IsEvassChanges = True
                    'End If
                    PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                    msg = fnPrint(strPrintMsg.ToString(), "PRV")

                    'If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                    '    msg = fnPrint(sbDeliveryBillPrint.ToString(), "PRV")
                    'End If

                    'code added for issue id 1528 by vipul
                    If dtView.Columns.Contains("BillIntermediateStatus") Then
                        If (Not String.IsNullOrEmpty(DeliveryPersonName)) AndAlso dtView.Rows(0)("BillIntermediateStatus").ToString <> "Deleted" Then

                            msg = fnPrint(sbDeliveryBillPrint.ToString(), "PRV")
                        End If
                    End If


                Else
                    If (KOTBillPrintingRequired) Then
                        ' Call CashMemoKOTPrint(Type, ErrorMsg, dtView, DtUnique, Dtcombo, CustomerNameLine, CustomerComments:=ReminderComments, EvassPizza:=EvasPizzaChanges)
                    End If
                    strPrintMsg.Append(vbCrLf & vbCrLf & vbCrLf & vbCrLf)
                    'If EvasPizzaChanges Then
                    '    Dim s As C1BarCode = GetBarcode(CashMemoNo)
                    '    VarBarcode = s
                    '    IsEvassChanges = True
                    'End If
                    PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                    msg = fnPrint(strPrintMsg.ToString(), "PRN")

                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)
                        sbDeliveryBillPrint.Append(vbCrLf)


                        If (Not String.IsNullOrEmpty(DeliveryPersonName)) AndAlso dtView.Rows(0)("BillIntermediateStatus").ToString <> "Deleted" Then

                            msg = fnPrint(sbDeliveryBillPrint.ToString(), "PRN")
                        End If
                    End If

                End If
                If Not String.IsNullOrEmpty(msg) Then
                    ErrorMsg = msg
                End If
                'Added for fiscal printer
                Try
                    clsFiscalPrinting.fnFiscalPrint(strPrintMsg.ToString())
                Catch ex As Exception

                End Try
                'Added for fiscal printer

            Else
                ErrorMsg = getValueByKey("CLSCMP031")
            End If
        Catch ex As Exception
            ErrorMsg = ex.Message
        End Try
    End Sub

    Public Sub DineInKOTPrint(ByVal Type As String, ByRef errorMsg As String, ByVal dtOld As DataTable, ByVal dtDefault As DataTable, BillNO As String, TableNo As String, strSiteCode As String)
        Try
            Dim strLineDetail, sbKOTBillPrintBase, sbKOTBillPrintBaseTakeAway, sbKOTBillPrintArticleMain, sbCustomHeader As New StringBuilder
            Dim StrCMNo, StrTokenNo, StrCustomerName As String
            Dim LineItemHeading, StrSubHeader, StrLineItem As String
            Dim ItemCode, Desc, Qty, TakeAwayQty As String
            Dim strLine, StrCenter As String
            Dim strDblLine As String
            Dim isTakeAwayQtyPrint As Boolean = False
            Dim TableName As String = ""
            Dim reprintreason As String
            'Dim dtnew As New DataTable
            'Dim dtold As New DataTable
            ''tempdata''
            If Type = "KOT" Then
                StrCenter = "                KOT   "
            ElseIf Type = "VOID" Then
                StrCenter = "              VOID   "
            ElseIf Type = "REPRINT" Then
                StrCenter = "              REPRINT   "
            End If


            ''
            Dim obj As New SpectrumBL.clsCashMemo()
            Dim hierarchyPrinters As New DataTable
            hierarchyPrinters = obj.GetPrinterHierarchyList()
            TableName = obj.GetTableNameFromTableNumber(strSiteCode, TableNo)
            Dim kotNO As String
            'PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")

            'Dim result() As DataRow = dtPrinterInfo1.Select("PrinterDocument  = 'KOT'")
            'If result.Count > 0 Then
            '    For Each row As DataRow In result
            '        PrinterName = row(3).ToString()
            '    Next
            'Else
            '    PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
            'End If
            KOTCustomerSaleType = "<B>" & KOTCustomerSaleType & "</B>"
            sbKOTBillPrintBase.Append("" & KOTCustomerSaleType + vbCrLf)
            'sbKOTBillPrintBase.Append("Table No: " & TableNo.ToString().Replace("Table No.", "") + vbCrLf)
            If TableNameForDineIn = True Then
                If String.IsNullOrEmpty(TableName) Then
                    sbKOTBillPrintBase.Append("Table No: " & TableNo.ToString().Replace("Table No.", "") + vbCrLf)
                Else
                    sbKOTBillPrintBase.Append("Table No: " & TableName.ToString().Replace("Table No.", "") + vbCrLf)
                End If
            Else
                sbKOTBillPrintBase.Append("Table No: " & TableNo.ToString().Replace("Table No.", "") + vbCrLf)
            End If
            sbKOTBillPrintBase.Append("Bill No: " & BillNO + vbCrLf)
            sbKOTBillPrintBase.Append("Time: " & DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + vbCrLf) 'vipin added Datetime on KOT

            'dtNew.Columns.Add("IsProcessed", GetType(String))
            If Not hierarchyPrinters Is Nothing Then
                For Each dr As DataRow In hierarchyPrinters.Rows
                    Dim dtArticles = obj.GetHierarchyPrinters(dr("ArticleHierarchy").ToString().Trim())
                    sbCustomHeader.Length = 0
                    sbKOTBillPrintArticleMain.Length = 0

                    Dim dvItemDetail As New DataView(dtDefault)
                    Dim uniqeKOT As New DataTable

                    uniqeKOT = dvItemDetail.ToTable(True, "KotNO")
                    For Each drKot As DataRow In uniqeKOT.Rows
                        kotNO = drKot("KotNO").ToString()
                        Dim myNewTable As DataTable = dtDefault.[Select]("KotNO='" & drKot("KotNO").ToString() & "'").CopyToDataTable()
                        ' Dim result As DataRow() = dtNew.[Select]("Quantity='" & drKot("Quantity").ToString() & "'")
                        Dim IsPrintRequired As Boolean = False
                        If myNewTable.Rows.Count > 0 Then
                            For Each row As DataRow In myNewTable.Rows
                                reprintreason = ""
                                Dim result() As DataRow = dtArticles.Select("ArticleCode  = '" & row("ArticleCode").ToString() & "'")
                                If result.Count > 0 Then
                                    IsPrintRequired = True
                                    If Type = "VOID" Then
                                        sbKOTBillPrintArticleMain.Append(MakeHtml(row("DISCRIPTION").ToString(), row("voidQuantity").ToString()))
                                        If (row("Reason").ToString() <> "") Then
                                            sbKOTBillPrintArticleMain.Append("( " & row("Reason").ToString() & ")" & vbCrLf)
                                        End If
                                    ElseIf Type = "REPRINT" Or Type = "KOT" Then
                                        If Type = "REPRINT" AndAlso row("Reason").ToString() <> "" Then
                                            reprintreason = row("Reason").ToString()
                                        End If
                                        sbKOTBillPrintArticleMain.Append(MakeHtml(row("DISCRIPTION").ToString(), row("KotQuantity").ToString()))
                                        If (row("Remark").ToString() <> "") Then
                                            sbKOTBillPrintArticleMain.Append("( " & row("Remark").ToString() & ")" & vbCrLf)
                                        End If
                                    End If

                                    'row.Delete()
                                    'dtArticles.Rows.Remove()
                                    For Each drDel As DataRow In dtDefault.Rows
                                        If drDel.Item("KotNO").ToString() = kotNO AndAlso drDel.Item("ArticleCode").ToString() = row("ArticleCode").ToString() Then
                                            'drDel.Delete()
                                            dtDefault.Rows.Remove(drDel)
                                            'GoTo Nextlabel
                                            Exit For
                                        End If
                                    Next
                                End If
                                'Nextlabel:
                            Next
                        End If
                        If IsPrintRequired Then
                            sbCustomHeader.Append(StrCenter & vbCrLf & vbCrLf & vbCrLf)
                            sbCustomHeader.Append(sbKOTBillPrintBase.ToString())
                            sbCustomHeader.Append("KOT No. " & kotNO & vbCrLf)
                            sbCustomHeader.Append(DeliveryPersonName & vbCrLf)
                            sbCustomHeader.Append(strLineL40 + vbCrLf)
                            sbCustomHeader.Append(getValueByKey("CLSPSO020") + getValueByKey("CLSPSO022").PadLeft(LineLength - getValueByKey("CLSPSO020").Length - 4) + vbCrLf)
                            sbCustomHeader.Append(strLineL40 + vbCrLf)
                            If reprintreason <> "" Then
                                sbKOTBillPrintArticleMain.Append(vbCrLf + "Reason : " & reprintreason)
                            End If
                            fnDineInKotPrint(sbCustomHeader, sbKOTBillPrintArticleMain, dr("PrinterName").ToString(), errorMsg)
                            sbCustomHeader.Length = 0
                            sbKOTBillPrintArticleMain.Length = 0
                        End If
                    Next
                    'For Each drArticles As DataRow In dtNew.Rows
                    '    Dim result() As DataRow = dtArticles.Select("ArticleCode  = '" & drArticles("ArticleCode").ToString() & "'")
                    '    If result.Count > 0 Then

                    '    End If
                    '    drArticles.Delete()
                    'Next
                Next
            End If

            ''default printer
            If dtDefault.Rows.Count > 0 Then


                Dim dvItemDetail As New DataView(dtDefault)
                Dim uniqeKOT As New DataTable

                uniqeKOT = dvItemDetail.ToTable(True, "KotNO")
                For Each drKot As DataRow In uniqeKOT.Rows
                    kotNO = drKot("KotNO").ToString()
                    Dim myNewTable As DataTable = dtDefault.[Select]("KotNO='" & drKot("KotNO").ToString() & "'").CopyToDataTable()
                    ' Dim result As DataRow() = dtNew.[Select]("Quantity='" & drKot("Quantity").ToString() & "'")

                    If myNewTable.Rows.Count > 0 Then

                        reprintreason = ""
                        For Each row As DataRow In myNewTable.Rows

                            If Type = "VOID" Then
                                sbKOTBillPrintArticleMain.Append(MakeHtml(row("DISCRIPTION").ToString(), row("voidQuantity").ToString()))
                                If (row("Reason").ToString() <> "") Then
                                    sbKOTBillPrintArticleMain.Append("( " & row("Reason").ToString() & ")" & vbCrLf)
                                End If
                            ElseIf Type = "REPRINT" Or Type = "KOT" Then
                                If Type = "REPRINT" AndAlso row("Reason").ToString() <> "" Then
                                    reprintreason = row("Reason").ToString()
                                End If
                                sbKOTBillPrintArticleMain.Append(MakeHtml(row("DISCRIPTION").ToString(), row("KotQuantity").ToString()))
                                If (row("Remark").ToString() <> "") Then
                                    sbKOTBillPrintArticleMain.Append("( " & row("Remark").ToString() & ")" & vbCrLf)
                                End If
                            End If


                        Next
                        sbCustomHeader.Append(StrCenter & vbCrLf & vbCrLf & vbCrLf)
                        sbCustomHeader.Append(sbKOTBillPrintBase.ToString())
                        sbCustomHeader.Append("KOT No. " & kotNO & vbCrLf)
                        sbCustomHeader.Append(DeliveryPersonName & vbCrLf)
                        sbCustomHeader.Append(strLineL40 + vbCrLf)
                        sbCustomHeader.Append(getValueByKey("CLSPSO020") + getValueByKey("CLSPSO022").PadLeft(LineLength - getValueByKey("CLSPSO020").Length - 4) + vbCrLf)
                        sbCustomHeader.Append(strLineL40 + vbCrLf)


                        Dim result() As DataRow = dtPrinterInfo1.Select("PrinterDocument  = 'KOT'")
                        If result.Count > 0 Then
                            For Each row As DataRow In result
                                PrinterName = row(3).ToString()
                            Next
                        Else
                            PrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                        End If
                        If reprintreason <> "" Then
                            sbKOTBillPrintArticleMain.Append(vbCrLf + "Reason : " & reprintreason)
                        End If
                        fnDineInKotPrint(sbCustomHeader, sbKOTBillPrintArticleMain, PrinterName, errorMsg)
                        sbCustomHeader.Length = 0
                        sbKOTBillPrintArticleMain.Length = 0
                    End If
                Next
            End If


        Catch ex As Exception
            LogException(ex)
            errorMsg = ex.Message
        End Try

    End Sub
    Sub fnDineInKotPrint(sbKOTBillPrintBase As StringBuilder, sbKOTBillPrintArticle As StringBuilder, ByVal CustomPrint As String, ByRef errorMsg As String)
        Try

            'dtPrinterInfo1.DefaultViewdtPrinterInfo1RowFilter = "PrinterDocument contains ('KOT'))"
            ' PrinterName = SetPrinterName(dtPrinterInfo1, "KOT", "Billing")

            If CustomPrint = Nothing Then
                Exit Sub
            Else
                PrinterName = CustomPrint
            End If
            Dim msg As String = String.Empty
            Dim sbKOTBillPrint As New StringBuilder
            'sbKOTBillPrint.Append(vbCrLf & vbCrLf)
            sbKOTBillPrintArticle.Append(vbCrLf & vbCrLf & vbCrLf & vbCrLf & "---------------------------------------")
            If _PrintPreview = True Then

                sbKOTBillPrint.Append(sbKOTBillPrintBase)
                sbKOTBillPrint.Append(sbKOTBillPrintArticle)

                msg = fnPrint(sbKOTBillPrint.ToString(), "PRV")
            Else

                sbKOTBillPrint.Append(sbKOTBillPrintBase)
                sbKOTBillPrint.Append(sbKOTBillPrintArticle)


                msg = fnPrint(sbKOTBillPrint.ToString(), "PRN")
            End If

            If Not String.IsNullOrEmpty(msg) Then
                errorMsg = msg
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ImageToBase64(ByVal CodeString As String) As String
        Try
            Dim VarBarcode As C1BarCode
            Dim s As C1BarCode = GetBarcode(CodeString)
            VarBarcode = s
            Dim mImage = VarBarcode.Image
            Dim uPix As GraphicsUnit = GraphicsUnit.Pixel
            Using ms As New MemoryStream()
                ' Convert Image to byte[]
                mImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Dim imageBytes As Byte() = ms.ToArray()
                ' Convert byte[] to Base64 String
                Dim base64String As String = Convert.ToBase64String(imageBytes)
                Return base64String
            End Using
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Sub SpectrumMettlerKOT(ByVal Type As String, ByRef errorMsg As String, ByRef dtView As DataTable, ByVal DtUnique As DataTable, ByVal Dtcombo As DataTable, CustomerNameLine As String, Optional ByVal HierarachyWisePrintFlag As Boolean = False, Optional ByVal CustomerComments As String = "", Optional ByVal EvassPizza As Boolean = False)
        Try
            Dim strLineDetail, sbKOTBillPrintBase, sbKOTBillPrintBaseTakeAway, sbKOTBillPrintArticleMain, sbKOTBillPrintArticleTakeAway, sbKOTBillPrintArticleQuantityWise As New StringBuilder
            Dim StrCMNo, StrTokenNo, StrCustomerName As String
            Dim LineItemHeading, StrSubHeader, StrLineItem As String
            Dim ItemCode, Desc, Qty, TakeAwayQty, remark As String
            Dim strLine, StrTokenSalesTypeLine, StrTokenTakeAwayLine As String
            Dim strDblLine As String
            Dim isTakeAwayQtyPrint As Boolean = False
            Dim timeDate As String = DateTime.Now.ToString("g")
            Dim IsHierarachyWisePrintFlag As Boolean = HierarachyWisePrintFlag

            ' Dim TotaltakeAwayQty = DtUnique.Compute("SUM(TakeAwayQuantity)", "").ToString()
            Dim TotaltakeAwayQty = 1
            If Val(TotaltakeAwayQty) > 0 Then
                isTakeAwayQtyPrint = True
            End If

            errorMsg = ""
            If Not dtView Is Nothing Then
                strLine = "-".PadRight(LineLength, "-") & vbCrLf
                strDblLine = "=".PadRight(LineLength, "=") & vbCrLf

                If Type = "DCM" Then

                    StrCMNo = "Void Bill No.:" & dtView.Rows(0)("Billno").ToString()
                Else
                    'StrCMNo = getValueByKey("CLSCMP009") & dtView.Rows(0)("Billno").ToString()
                    'sbKOTBillPrintBase.Append(getValueByKey("CLSCMP009") & dtView.Rows(0)("Billno").ToString() + vbCrLf + vbCrLf)
                    'modified by khusrao adil on 12-07-2017 for print formate changes Bill No
                    StrCMNo = "Bill No.:" & dtView.Rows(0)("Billno").ToString()



                    'sbKOTBillPrintBase.Append(strLineL40 + vbCrLf)
                End If



                If PrintCouponNumberForKOT = True Then
                    'Dim CouponNo As String = strTillno.Substring(0, 1) & strTillno.Substring(strTillno.Length - 2, 2) & strBillno.Substring(strBillno.Length - 2, 2)
                    Dim tillNo As String = dtView.Rows(0)("TerminalId").ToString()
                    Dim billno As String = dtView.Rows(0)("Billno").ToString()
                    Dim CouponNo As String = tillNo.Substring(0, 1) & tillNo.Substring(tillNo.Length - 2, 2) & billno.Substring(billno.Length - 2, 2)
                    StrTokenNo = CouponNo
                Else
                    StrTokenNo = dtView.Rows(0)("Billno").ToString().Trim.Substring(dtView.Rows(0)("Billno").ToString().Trim.Length - 4, 4)
                End If


                'sbKOTBillPrintBase.Append("Token No.:" & dtView.Rows(0)("Billno").ToString() + vbCrLf)
                sbKOTBillPrintBase.Append("" + vbCrLf)
                sbKOTBillPrintBase.Append("Token No.:" & StrTokenNo.ToString + vbCrLf)
                sbKOTBillPrintBase.Append("Counter No.:" & dtView.Rows(0)("terminalid").ToString() + vbCrLf + vbCrLf)


                sbKOTBillPrintBase.Append(timeDate + vbCrLf + vbCrLf)
                sbKOTBillPrintBase.Append(strLineL40 + vbCrLf)
                Select Case PrintFormatNo
                    Case 1
                        StrTokenNo = "<B>" & StrTokenNo & "</B>"
                    Case 2
                    Case Else
                End Select

                If Type = "CMWAmt" Then
                    'LineItemHeading = getValueByKey("CLSPSO020") & Space(17) & vbCrLf & getValueByKey("CLSCMP016") & Space(20) & getValueByKey("CLSPSO022") & " " & vbCrLf
                    LineItemHeading = "Article " & Space(17) & vbCrLf & getValueByKey("CLSCMP016") & Space(20) & getValueByKey("CLSPSO022") & " " & vbCrLf
                Else
                    LineItemHeading = getValueByKey("CLSCMP016") & Space(13) & getValueByKey("CLSPSO022") & " " & Space(5)
                    Select Case PrintFormatNo
                        Case 1
                            LineItemHeading &= "Amt" & vbCrLf
                        Case 2

                        Case Else
                            LineItemHeading &= "Amt" & vbCrLf
                    End Select

                End If

                If (dtView.Rows.Count() > 0 And String.IsNullOrEmpty(DeliveryPersonName)) Then
                    If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                        'added by khusrao adil on 29-08-2017 for natural
                        If PrintFormatNo = 6 Then
                            StrTokenNo = "<BT>" & "Token No. " & StrTokenNo & "</BT>"
                        Else
                            StrTokenNo = "Token No. " & StrTokenNo
                        End If
                    End If
                End If

                'sbKOTBillPrintBase.Append(getValueByKey("CLSPSO020") + getValueByKey("CLSPSO022").PadLeft(LineLength - getValueByKey("CLSPSO020").Length - 4) + vbCrLf)
                sbKOTBillPrintBase.Append(getValueByKey("TT002") + getValueByKey("CLSPSO022").PadLeft(LineLength - getValueByKey("TT002").Length - 4) + vbCrLf)
                sbKOTBillPrintBase.Append(strLineL40 + vbCrLf)
                If Not IsDBNull(dtView.Rows(0)("DeliveryPersonID")) AndAlso Not String.IsNullOrEmpty(dtView.Rows(0)("DeliveryPersonID")) Then
                    DeliveryPersonName = dtView.Rows(0)("DeliveryPersonID").ToString()
                End If
                StrTokenTakeAwayLine = "Take Away" & vbCrLf
                If dtView.Rows.Count() > 0 Then
                    If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
                        'If AllowBillingOnlyAfterSelectionOfSalesType Then
                        StrTokenSalesTypeLine = CustomerSaleType & vbCrLf
                        'End If
                    Else
                        If _CustomerNameRequiredInKOT AndAlso CustomerNameLine <> Nothing Then
                            StrCustomerName = CustomerNameLine.ToUpper.Replace("CUSTOMER", "").Trim()
                            'sbKOTBillPrintBase.Insert(0, StrCustomerName & vbCrLf)
                            sbKOTBillPrintBase.Insert(0, SplitString(StrCustomerName).ToString() & vbCrLf)
                        End If
                        If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
                            If Not AllowBillingOnlyAfterSelectionOfSalesType Then
                                CustomerSaleType = String.Empty

                            End If
                            StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - CustomerSaleType.Length) & CustomerSaleType & vbCrLf & vbCrLf
                            StrTokenTakeAwayLine = StrTokenNo.PadRight(LineLength - 1 - "Take Away".Length) & "Take Away" & vbCrLf & vbCrLf
                        Else
                            If AllowBillingOnlyAfterSelectionOfSalesType Then
                                StrTokenSalesTypeLine = CustomerSaleType & vbCrLf
                            End If
                        End If
                    End If
                Else
                    If AllowBillingOnlyAfterSelectionOfSalesType Then
                        StrTokenSalesTypeLine = CustomerSaleType & vbCrLf
                    End If
                End If

                sbKOTBillPrintBaseTakeAway.Append(sbKOTBillPrintBase)
                sbKOTBillPrintBase.Insert(0, StrTokenSalesTypeLine)
                sbKOTBillPrintBaseTakeAway.Insert(0, StrTokenTakeAwayLine)



                '---- Step 2- making 2 Instance 1 for DineIn 2 for TakeAway 
                '---- looping items and construct prints 
                ''printerwise article to print logic implememnted 
                Dim obj As New SpectrumBL.clsCashMemo()
                Dim hierarchyPrinters As New DataTable
                hierarchyPrinters = obj.GetPrinterHierarchyList()
                'temp articles and printer details table 
                Dim dtArticleDetailsToPrint As New DataTable
                dtArticleDetailsToPrint.TableName = "dtArticleDetailsToPrint"
                dtArticleDetailsToPrint.Columns.Add("ArticleCode", GetType(String))
                dtArticleDetailsToPrint.Columns.Add("ArticleName", GetType(String))
                dtArticleDetailsToPrint.Columns.Add("Qty", GetType(Double))
                dtArticleDetailsToPrint.Columns.Add("PrinterName", GetType(String))
                dtArticleDetailsToPrint.Columns.Add("ArticleHierarchy", GetType(String))
                Dim BILLLineNo1 As String
                Dim StrAricleHierarachy As List(Of String)

                Dim DefaultKotPrinterName As String = String.Empty
                Dim tempresult() As DataRow = dtPrinterInfo1.Select("PrinterDocument  = 'KOT'")
                If tempresult.Count > 0 Then
                    For Each row As DataRow In tempresult
                        DefaultKotPrinterName = row(3).ToString()
                    Next
                Else
                    DefaultKotPrinterName = SetPrinterName(dtPrinterInfo1, "CashMemo", "Billing")
                End If

                For Each Printer In hierarchyPrinters.Rows
                    Dim dtHierarchyArticles = obj.GetHierarchyPrintersLastNodeCode(Printer("ArticleHierarchy").ToString().Trim())

                    sbKOTBillPrintArticleMain.Length = 0


                    For Each drBillarticle As DataRow In dtView.Rows

                        '---- Two Pass - First for articles that belong to configure Hierarchy list
                        Dim IsArticleAdded() As DataRow = dtArticleDetailsToPrint.Select("ArticleCode  = '" & drBillarticle("ArticleCode").ToString() & "'")
                        If IsArticleAdded.Count = 0 Then
                            If Not dtHierarchyArticles Is Nothing AndAlso dtHierarchyArticles.Rows.Count > 0 Then '' added by ketan 
                                Dim IsArticleInHierarchy() As DataRow = dtHierarchyArticles.Select("ArticleCode  = '" & drBillarticle("ArticleCode").ToString() & "'")
                                If IsArticleInHierarchy.Count > 0 Then
                                    Dim resultDelivery As DataRow() = dtHierarchyArticles.Select("ArticleCode  = '" & drBillarticle("ArticleCode").ToString() & "'")
                                    dtArticleDetailsToPrint.Rows.Add(drBillarticle("ArticleCode").ToString(), drBillarticle("DISCRIPTION").ToString(), drBillarticle("Quantity").ToString(), Printer("PrinterName"), resultDelivery(0)("LastNodeCodeName").ToString().Trim())
                                End If
                            End If
                        End If
                    Next
                Next

                For Each drBillarticle As DataRow In dtView.Rows
                    Dim IsArticleAdded() As DataRow = dtArticleDetailsToPrint.Select("ArticleCode  = '" & drBillarticle("ArticleCode").ToString() & "'")
                    If IsArticleAdded.Count = 0 Then
                        dtArticleDetailsToPrint.Rows.Add(drBillarticle("ArticleCode").ToString(), drBillarticle("DISCRIPTION").ToString(), drBillarticle("Quantity").ToString(), DefaultKotPrinterName)
                    End If
                Next

                '''''' passsing datatable for priner wise article printing  
                If dtArticleDetailsToPrint.Rows.Count > 0 AndAlso Not dtArticleDetailsToPrint Is Nothing Then
                    Dim dtPrinterforPrint As DataTable
                    Dim dvprinteers As New DataView(dtArticleDetailsToPrint)
                    dtPrinterforPrint = dvprinteers.ToTable(True, "printername")


                    ''''''' To print the articles as hierarachywise on the flag basis for the configured printer starts here 




                    Dim dtArticleHerirachyTable As DataTable
                    Dim dtArticleHerirachy As New DataView(dtArticleDetailsToPrint)

                    If IsHierarachyWisePrintFlag Then
                        dtArticleHerirachyTable = dtArticleHerirachy.ToTable(True, "ArticleHierarchy", "printername")
                    Else
                        dtArticleHerirachyTable = dtArticleHerirachy.ToTable(True, "printername")
                    End If



                    For Each printers As DataRow In dtArticleHerirachyTable.Rows
                        '---- Here Find All article in dtUnique that belong to that printer ...

                        Dim printerName = printers("printername").ToString()
                        Dim dtArticlePrint As DataTable
                        If IsHierarachyWisePrintFlag Then
                            Dim Articleherirachy = printers("ArticleHierarchy").ToString()
                            dtArticlePrint = (From art In DtUnique
                                           Join artPrint In dtArticleDetailsToPrint
                                           On art("ArticleCode").ToString() Equals artPrint("ArticleCode").ToString()
                            Where artPrint("printername").ToString = printerName And artPrint("ArticleHierarchy").ToString() = Articleherirachy
                                           Select art).CopyToDataTable
                        Else
                            dtArticlePrint = (From art In DtUnique
                                           Join artPrint In dtArticleDetailsToPrint
                                           On art("ArticleCode").ToString() Equals artPrint("ArticleCode").ToString()
                            Where artPrint("printername").ToString = printerName
                                           Select art).CopyToDataTable
                        End If


                        Dim BillLineNO As String = String.Empty




                        For Each dr As DataRow In dtArticlePrint.Rows
                            ItemCode = dr("ArticleCode").ToString()
                            'code added by irfan on 24-08-2017
                            If Not dtArticlePrint.Columns.Contains("ArticleFullName") Then
                                Desc = dr("DISCRIPTION").ToString()
                            Else
                                Desc = IIf(DisplayArticleFullName, dr("ArticleFullName").ToString(), dr("DISCRIPTION").ToString())
                            End If
                            '  remark = IIf(IsDBNull(dr("Remark")), String.Empty, dr("Remark"))
                            ' BillLineNO = dr("BillLineNO").ToString()
                            If SpectrumCommon.ComboItemPrintingAllowed AndAlso Dtcombo IsNot Nothing AndAlso Dtcombo.Rows.Count > 0 Then
                                Dim articleDescriptionDictionary As New Dictionary(Of String, Integer)
                                Dim drp() = Dtcombo.Select("ComboArticleCode='" & ItemCode & "' AND BillLineNO ='" & BillLineNO & "'")

                                If drp.Count > 0 Then
                                    For Each Row In drp
                                        If articleDescriptionDictionary.ContainsKey(Row("ArticleName")) Then
                                            articleDescriptionDictionary(Row("ArticleName")) = articleDescriptionDictionary(Row("ArticleName")) + Row("Quantity")
                                        Else
                                            articleDescriptionDictionary.Add(Row("ArticleName"), Row("Quantity"))
                                        End If
                                    Next
                                    Dim AddedDesc = GetMultilinedString(articleDescriptionDictionary)
                                    Desc = Desc & vbCrLf & AddedDesc
                                End If
                            End If

                            Qty = dr("Quantity").ToString()
                            Dim IsForEachQuantityOver = False


                            '// if qty > 1 && qty wise flag true && item eligible for each qty print 
                            If (KOTPrintForEachQuantity) Then
                                If Qty > 0 And dr("IsQuntityWiseRequired") = 1 Then
                                    Dim QtyArray As Array = Qty.Split(".")
                                    Dim MaxCnt As Integer = Convert.ToInt32(QtyArray(0))
                                    Qty = "1.00"

                                    'If (Desc.Length >= LineLength) Or (Desc.Length + Qty.Length >= LineLength) Then
                                    '    sbKOTBillPrintArticleQuantityWise.Append(MakeHtml(Desc) + vbCrLf)
                                    '    sbKOTBillPrintArticleQuantityWise.Append(MakeHtml(Qty.PadLeft(LineLength - 2)) + vbCrLf)
                                    'Else
                                    '    sbKOTBillPrintArticleQuantityWise.Append(MakeHtml(Desc + Qty.PadLeft(LineLength - Desc.Length - 2)) + vbCrLf)
                                    'End If

                                    ''add remark in KOT print added by ketan 
                                    ' sbKOTBillPrintArticleQuantityWise.Append(MakeHtml(Desc, Qty))
                                    If remark = "" Then
                                        isremark = True
                                        If IsKotFontLarge Then
                                            Desc = SplitString(Desc, IIf(IsKotFontLarge, 20, 40)).ToString
                                        Else
                                            Desc = SplitString(Desc, IIf(IsKotFontBold, 30, 40)).ToString
                                        End If
                                        sbKOTBillPrintArticleQuantityWise.Append(MakeHtml(Desc, Qty))
                                    Else
                                        isremark = True
                                        If IsKotFontLarge Then
                                            Desc = SplitString(Desc + "(" + remark + ")", IIf(IsKotFontLarge, 20, 40)).ToString
                                        Else
                                            Desc = SplitString(Desc + "(" + remark + ")", IIf(IsKotFontBold, 30, 40)).ToString
                                        End If
                                        sbKOTBillPrintArticleQuantityWise.Append(MakeHtml(Desc, Qty))
                                    End If
                                    Dim kotQty As Integer = 0
                                    While kotQty < MaxCnt
                                        Call fnKotPrint(sbKOTBillPrintBase, sbKOTBillPrintArticleQuantityWise, errorMsg, printerName)
                                        kotQty = kotQty + 1
                                    End While
                                    'For kotQty As Integer = 0 To MaxCnt - 1
                                    '    Call fnKotPrint(sbKOTBillPrintBase, sbKOTBillPrintArticleQuantityWise, errorMsg)
                                    'Next
                                    sbKOTBillPrintArticleQuantityWise.Length = 0
                                    IsForEachQuantityOver = True
                                    ' Continue For
                                End If
                            End If



                            '  TakeAwayQty = dr("TakeAwayQuantity").ToString()

                            ' Call SetQtyFormat(TakeAwayQty)
                            If Val(TakeAwayQty) > 0 Then
                                isTakeAwayQtyPrint = True
                                Qty = TakeAwayQty '(dr("Quantity") - dr("TakeAwayQuantity")).ToString
                                strLineDetail.Append(StrLineItem)

                                ''add remark in KOT print added by ketan 
                                '' sbKOTBillPrintArticleTakeAway.Append(MakeHtml(Desc, Qty))
                                If remark = "" Then
                                    isremark = True
                                    If IsKotFontLarge Then
                                        Desc = SplitString(Desc, IIf(IsKotFontLarge, 20, 40)).ToString
                                    Else
                                        Desc = SplitString(Desc, IIf(IsKotFontBold, 30, 40)).ToString
                                    End If
                                    sbKOTBillPrintArticleTakeAway.Append(MakeHtml(Desc, Qty))
                                Else
                                    isremark = True
                                    If IsKotFontLarge Then
                                        Desc = SplitString(Desc + "(" + remark + ")", IIf(IsKotFontLarge, 20, 40)).ToString
                                    Else
                                        Desc = SplitString(Desc + "(" + remark + ")", IIf(IsKotFontBold, 30, 40)).ToString
                                    End If
                                    sbKOTBillPrintArticleTakeAway.Append(MakeHtml(Desc, Qty))
                                End If

                                'If (Desc.Length >= LineLength) Or (Desc.Length + TakeAwayQty.Length >= LineLength) Then
                                '    sbKOTBillPrintArticleTakeAway.Append(MakeHtml(Desc) + vbCrLf)
                                '    sbKOTBillPrintArticleTakeAway.Append(MakeHtml(TakeAwayQty.PadLeft(LineLength - 2)) + vbCrLf)
                                'Else
                                '    sbKOTBillPrintArticleTakeAway.Append(MakeHtml(Desc + TakeAwayQty.PadLeft(LineLength - Desc.Length - 2)) + vbCrLf)
                                'End If
                            End If

                            strLineDetail.Append(StrLineItem)
                            Call SetQtyFormat(Qty)
                            Qty = FormatNumber((dr("Quantity") - Val(TakeAwayQty)), 3)
                            If Val(Qty) > 0 And (Not IsForEachQuantityOver) Then

                                ' sbKOTBillPrintArticleMain.Append(MakeHtml(Desc, Qty))
                                ''Changed by ketan add remark in kot print
                                If remark = "" Then
                                    isremark = True
                                    If IsKotFontLarge Then
                                        Desc = SplitString(Desc, IIf(IsKotFontLarge, 20, 40)).ToString
                                    Else
                                        Desc = SplitString(Desc, IIf(IsKotFontBold, 30, 40)).ToString
                                    End If
                                    sbKOTBillPrintArticleMain.Append(MakeHtml(Desc, Qty))
                                Else
                                    isremark = True
                                    If IsKotFontLarge Then
                                        Desc = SplitString(Desc + "(" + remark + ")", IIf(IsKotFontLarge, 20, 40)).ToString
                                    Else
                                        Desc = SplitString(Desc + "(" + remark + ")", IIf(IsKotFontBold, 30, 40)).ToString
                                    End If
                                    sbKOTBillPrintArticleMain.Append(MakeHtml(Desc, Qty))
                                End If

                            End If

                            '

                            If (KOTPrintEachlineItem) And (Not IsForEachQuantityOver) Then
                                Call fnKotPrint(sbKOTBillPrintBase, sbKOTBillPrintArticleMain, errorMsg, printerName, CustomerComments, EvassPizza)
                                If Val(TakeAwayQty) > 0 Then
                                    Call fnKotPrint(sbKOTBillPrintBaseTakeAway, sbKOTBillPrintArticleTakeAway, errorMsg, printerName, CustomerComments, EvassPizza)
                                End If
                                sbKOTBillPrintArticleMain.Length = 0
                                Continue For
                            End If
                        Next

                        ' step 4--------- if till now not Called for Print , call now----------------
                        If (Not KOTPrintEachlineItem) Then
                            If sbKOTBillPrintArticleMain.Length > 0 Then

                                Dim spectrummettlerBarCode As Long = dtView.Rows(0)("Billno").ToString().Remove(0, 4)
                                Dim s As C1BarCode = GetBarcode(spectrummettlerBarCode)
                                VarBarcode = s
                                IsEvassChanges = True
                                SpectrumAsMettlerBarcode = True
                                Call fnKotPrint(sbKOTBillPrintBase, sbKOTBillPrintArticleMain, errorMsg, printerName, CustomerComments, EvassPizza, SpectrumAsMettlerBarcode)
                            End If
                            'If isTakeAwayQtyPrint Then
                            '    Call fnKotPrint(sbKOTBillPrintBaseTakeAway, sbKOTBillPrintArticleTakeAway, errorMsg, printerName, CustomerComments, EvassPizza)
                            'End If
                        End If
                        sbKOTBillPrintArticleMain.Length = 0

                    Next



                End If
            Else
                errorMsg = getValueByKey("CLSCMP031")
            End If
        Catch ex As Exception
            errorMsg = ex.Message
        End Try

    End Sub

    Public Sub SpectrumArticleLabel(ByVal ArticleCode As String, ByVal SiteCode As String, ByVal DayCloseReportPath As String, ByVal IsPKDRequired As Boolean, ByVal PKDDate As Date, Optional ByVal NoOfCopies As Integer = 1)
        Try

            Dim ObjCom As New SpectrumBL.clsCashMemo()

            Dim DSTaxInvoicedtl = ObjCom.GetArticleLabelDetailsForPrint(ArticleCode, SiteCode, IsPKDRequired, PKDDate)

            If Not DSTaxInvoicedtl Is Nothing Then
                If DSTaxInvoicedtl.Tables(0).Rows.Count > 0 Then 'And DSTaxInvoicedtl.Tables(1).Rows.Count > 0
                    BarCodestring = ImageToBase64(DSTaxInvoicedtl.Tables(0).Rows(0)("EAN").ToString)
                    For index = 1 To NoOfCopies
                        GenerateArticleLabel(DSTaxInvoicedtl, ArticleCode, SiteCode, BarCodestring, DayCloseReportPath, NoOfCopies)
                    Next
                End If
            End If



        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    Public Function BarcodePrintingFormat4(ByVal ArticleCode As String, ByVal SiteCode As String, ByVal DayCloseReportPath As String, ByVal IsPKDRequired As Boolean, ByVal PKDDate As Date, Optional ByVal NoOfCopies As Integer = 1) As Boolean
        Try
            Dim articlecode1 As String
            Dim ObjCom As New SpectrumBL.clsCashMemo()
            articlecode1 = ArticleCode.TrimEnd(",")
            Dim dsBarcode = ObjCom.GetArticleBarcodeDetailsForPrinting(articlecode1, SiteCode, IsPKDRequired, PKDDate)
            If Not dsBarcode Is Nothing And dsBarcode.Rows.Count > 0 Then
                For Each dr As DataRow In dsBarcode.Rows
                    BarCodestring = ImageToBase64ForBarcode(dsBarcode.Rows(0)("EAN").ToString())
                    dr("BarCodeString") = BarCodestring
                Next
                dsBarcode.AcceptChanges()
                If GenerateBarcodeFormat4(dsBarcode, SiteCode, DayCloseReportPath, "Barcode") = True Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function
    Public Function LabelPrintingFormat4(ByVal ArticleCode As String, ByVal SiteCode As String, ByVal DayCloseReportPath As String, ByVal IsPKDRequired As Boolean, ByVal PKDDate As Date, Optional ByVal NoOfCopies As Integer = 1) As Boolean
        Try
            Dim articlecode1 As String
            Dim ObjCom As New SpectrumBL.clsCashMemo()
            articlecode1 = ArticleCode.TrimEnd(",")
            Dim dsLabel = ObjCom.GetArticleLabelDetailsForPrinting(articlecode1, SiteCode, IsPKDRequired, PKDDate)

            If dsLabel.Rows.Count > 0 And Not dsLabel Is Nothing Then
                For Each dr As DataRow In dsLabel.Rows
                    BarCodestring = ImageToBase64ForBarcode(dsLabel.Rows(0)("EAN").ToString)
                    dr("BarCodeString") = BarCodestring
                Next
                dsLabel.AcceptChanges()
                GenerateLabelPrintFormat4(dsLabel, SiteCode, DayCloseReportPath, "Label", IsPKDRequired, PKDDate)
            End If

            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function
    Private Function GenerateLabelPrintFormat4(ByRef DSTaxInvoiceDetails As DataTable, ByVal SiteCode As String, ByVal DayCloseReportPath As String, ByVal PrintOpt As String, ByVal IsPKDRequired As Boolean, ByVal PKDDate As Date) As Boolean
        Try
            Dim path As String = ""
            Dim PKDADATE1 = PKDDate.ToString("dd-MM-yyyy")
            Dim reportViewer2 As New LocalReport
            Dim appPath As String
            appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\BatchLabelF1.rdl")
            reportViewer2.ReportPath = appPath
            reportViewer2.SetParameters(New ReportParameter("Sitecode", SiteCode))
            reportViewer2.SetParameters(New ReportParameter("BatchBarcode", SiteCode))
            reportViewer2.SetParameters(New ReportParameter("PKDDate", PKDDate))
            reportViewer2.SetParameters(New ReportParameter("IsPKDRequired", IsPKDRequired))
            Dim DataSource1 As New ReportDataSource("DataSet1", DSTaxInvoiceDetails)
            reportViewer2.DataSources.Add(DataSource1)
            reportViewer2.Refresh()
            If _PrintPreview = True Then
                Dim mybytes As [Byte]() = reportViewer2.Render("Pdf")
                Dim obj As New clsCommon
                path = DayCloseReportPath & "\Label_ &_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                Using fs As FileStream = File.Create(path)
                    fs.Write(mybytes, 0, mybytes.Length)
                End Using
                Process.Start(path)
            Else
                ExportLabelPrintFormat4(reportViewer2)
                Print(True)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Function GenerateBarcodeFormat4(ByRef DSTaxInvoiceDetails As DataTable, ByVal SiteCode As String, ByVal DayCloseReportPath As String, ByVal PrintOpt As String) As Boolean
        Try

            Dim path As String = ""
            Dim reportViewer2 As New LocalReport
            Dim appPath As String
            appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\BarcodePrint1PDF.rdl")
            reportViewer2.ReportPath = appPath
            reportViewer2.SetParameters(New ReportParameter("Sitecode", SiteCode))
            reportViewer2.SetParameters(New ReportParameter("BatchBarcode", SiteCode))
            Dim DataSource1 As New ReportDataSource("DataSet1", DSTaxInvoiceDetails)
            reportViewer2.DataSources.Add(DataSource1)
            reportViewer2.Refresh()
            If _PrintPreview = True Then
                Dim mybytes As [Byte]() = reportViewer2.Render("Pdf")
                Dim obj As New clsCommon
                path = DayCloseReportPath & "\Barcode_ &_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                Using fs As FileStream = File.Create(path)
                    fs.Write(mybytes, 0, mybytes.Length)
                End Using
                Process.Start(path)
            Else
                ExportBarcodePrintFormat4(reportViewer2)
                Print(True)
            End If

            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function ImageToBase64ForBarcode(ByVal CodeString As String) As String
        Try
            Dim VarBarcode As C1BarCode
            Dim s As C1BarCode = GetBarcodeNew(CodeString)
            VarBarcode = s
            Dim mImage = VarBarcode.Image
            Dim uPix As GraphicsUnit = GraphicsUnit.Pixel
            Using ms As New MemoryStream()
                ' Convert Image to byte[]
                mImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Dim imageBytes As Byte() = ms.ToArray()
                ' Convert byte[] to Base64 String
                Dim base64String As String = Convert.ToBase64String(imageBytes)
                Return base64String
            End Using
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function GetBarcodeNew(ByVal BarcodeValue As String) As C1BarCode

        Dim BarCode As New C1BarCode()
        BarCode.CodeType = CodeTypeEnum.Code128
        BarCode.Text = BarcodeValue
        Return BarCode

    End Function
    Private Sub ExportLabelPrintFormat4(ByVal report As LocalReport)
        Try
            Dim deviceInfo As String = "<DeviceInfo>" & _
                "<OutputFormat>EMF</OutputFormat>" & _
                "<PageWidth>1.96in</PageWidth>" & _
                "<PageHeight>1.96in</PageHeight>" & _
                "<MarginTop>0.00in</MarginTop>" & _
                "<MarginLeft>0.00in</MarginLeft>" & _
                "<MarginRight>0.00in</MarginRight>" & _
                "<MarginBottom>0.00in</MarginBottom>" & _
                "</DeviceInfo>"
            Dim warnings As Warning()
            m_streams = New List(Of Stream)()
            report.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
            For Each stream As Stream In m_streams
                stream.Position = 0
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub ExportBarcodePrintFormat4(ByVal report As LocalReport)
        Try
            Dim deviceInfo As String = "<DeviceInfo>" & _
                "<OutputFormat>EMF</OutputFormat>" & _
                "<PageWidth>1.49in</PageWidth>" & _
                "<PageHeight>0.59in</PageHeight>" & _
                "<MarginTop>0.00in</MarginTop>" & _
                "<MarginLeft>0.00in</MarginLeft>" & _
                "<MarginRight>0.00in</MarginRight>" & _
                "<MarginBottom>0.00in</MarginBottom>" & _
                "</DeviceInfo>"
            Dim warnings As Warning()
            m_streams = New List(Of Stream)()
            report.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
            For Each stream As Stream In m_streams
                stream.Position = 0
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
End Class

Public Class MettlerKOTBillData
    Private _ArticleCode As String
    Public Property ArticleCode() As String
        Get
            Return _ArticleCode
        End Get
        Set(ByVal value As String)
            _ArticleCode = value
        End Set
    End Property

    Private _ArticleName As String
    Public Property ArticleName() As String
        Get
            Return _ArticleName
        End Get
        Set(ByVal value As String)
            _ArticleName = value
        End Set
    End Property

    Private _PricePerUnit As Double
    Public Property PricePerUnit() As Double
        Get
            Return _PricePerUnit
        End Get
        Set(ByVal value As Double)
            _PricePerUnit = value
        End Set
    End Property

    Private _Disc As Double
    Public Property Disc() As Double
        Get
            Return _Disc
        End Get
        Set(ByVal value As Double)
            _Disc = value
        End Set
    End Property

    Private _Qty As Double
    Public Property Qty() As Double
        Get
            Return _Qty
        End Get
        Set(ByVal value As Double)
            _Qty = value
        End Set
    End Property

    Private _OUM As String
    Public Property OUM() As String
        Get
            Return _OUM
        End Get
        Set(ByVal value As String)
            _OUM = value
        End Set
    End Property

    Private _Total As Double
    Public Property Total() As Double
        Get
            Return _Total
        End Get
        Set(ByVal value As Double)
            _Total = value
        End Set
    End Property

End Class


'Public Sub CashMemoKOTPrint(ByVal Type As String, ByRef errorMsg As String, ByRef dtView As DataTable, ByVal DtUnique As DataTable, ByVal Dtcombo As DataTable, CustomerNameLine As String)
'    Try
'        Dim strLineDetail, sbKOTBillPrintBase, sbKOTBillPrintBaseTakeAway, sbKOTBillPrintArticleMain, sbKOTBillPrintArticleTakeAway, sbKOTBillPrintArticleQuantityWise As New StringBuilder
'        Dim StrCMNo, StrTokenNo, StrCustomerName As String
'        Dim LineItemHeading, StrSubHeader, StrLineItem As String
'        Dim ItemCode, Desc, Qty, TakeAwayQty As String
'        Dim strLine, StrTokenSalesTypeLine, StrTokenTakeAwayLine As String
'        Dim strDblLine As String
'        Dim isTakeAwayQtyPrint As Boolean = False

'        Dim TotaltakeAwayQty = DtUnique.Compute("SUM(TakeAwayQuantity)", "").ToString()
'        If Val(TotaltakeAwayQty) > 0 Then
'            isTakeAwayQtyPrint = True
'        End If

'        errorMsg = ""
'        If Not dtView Is Nothing Then
'            strLine = "-".PadRight(LineLength, "-") & vbCrLf
'            strDblLine = "=".PadRight(LineLength, "=") & vbCrLf
'            '------ STEP-1:  collecting all Data With Format -----
'            If Type = "DCM" Then
'                StrCMNo = getValueByKey("CLSCMP008") & dtView.Rows(0)("Billno").ToString()
'            Else
'                StrCMNo = getValueByKey("CLSCMP009") & dtView.Rows(0)("Billno").ToString()
'                sbKOTBillPrintBase.Append(getValueByKey("CLSCMP009") & dtView.Rows(0)("Billno").ToString() + vbCrLf + vbCrLf)
'                sbKOTBillPrintBase.Append(strLineL40 + vbCrLf)
'            End If

'            StrTokenNo = dtView.Rows(0)("Billno").ToString().Trim.Substring(dtView.Rows(0)("Billno").ToString().Trim.Length - 2, 2)
'            Select Case PrintFormatNo
'                Case 1
'                    StrTokenNo = "<B>" & StrTokenNo & "</B>"
'                Case 2
'                Case Else
'            End Select

'            If Type = "CMWAmt" Then
'                LineItemHeading = getValueByKey("CLSPSO020") & Space(17) & vbCrLf & getValueByKey("CLSCMP016") & Space(20) & getValueByKey("CLSPSO022") & " " & vbCrLf
'            Else
'                LineItemHeading = getValueByKey("CLSCMP016") & Space(13) & getValueByKey("CLSPSO022") & " " & Space(5)
'                Select Case PrintFormatNo
'                    Case 1
'                        LineItemHeading &= "Amt" & vbCrLf
'                    Case 2

'                    Case Else
'                        LineItemHeading &= "Amt" & vbCrLf
'                End Select

'            End If

'            If (dtView.Rows.Count() > 0 And String.IsNullOrEmpty(DeliveryPersonName)) Then
'                If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
'                    StrTokenNo = "Token No. " & StrTokenNo
'                End If
'            End If

'            sbKOTBillPrintBase.Append(getValueByKey("CLSPSO020") + getValueByKey("CLSPSO022").PadLeft(LineLength - getValueByKey("CLSPSO020").Length - 4) + vbCrLf)
'            sbKOTBillPrintBase.Append(strLineL40 + vbCrLf)

'            StrTokenTakeAwayLine = "Take Away" & vbCrLf
'            If dtView.Rows.Count() > 0 Then
'                If (Not String.IsNullOrEmpty(DeliveryPersonName)) Then
'                    If AllowBillingOnlyAfterSelectionOfSalesType Then
'                        StrTokenSalesTypeLine = CustomerSaleType & vbCrLf
'                    End If
'                Else
'                    If _CustomerNameRequiredInKOT AndAlso CustomerNameLine <> Nothing Then
'                        StrCustomerName = CustomerNameLine.ToUpper.Replace("CUSTOMER", "").Trim()
'                        sbKOTBillPrintBase.Insert(0, StrCustomerName & vbCrLf)
'                    End If
'                    If CashMemoResetonDayClose AndAlso TokenNoRequiredInKOT Then
'                        If Not AllowBillingOnlyAfterSelectionOfSalesType Then
'                            CustomerSaleType = String.Empty
'                        End If
'                        StrTokenSalesTypeLine = StrTokenNo.PadRight(LineLength - 1 - CustomerSaleType.Length) & CustomerSaleType & vbCrLf & vbCrLf
'                        StrTokenTakeAwayLine = StrTokenNo.PadRight(LineLength - 1 - "Take Away".Length) & "Take Away" & vbCrLf & vbCrLf
'                    Else
'                        If AllowBillingOnlyAfterSelectionOfSalesType Then
'                            StrTokenSalesTypeLine = CustomerSaleType & vbCrLf
'                        End If
'                    End If
'                End If
'            Else
'                If AllowBillingOnlyAfterSelectionOfSalesType Then
'                    StrTokenSalesTypeLine = CustomerSaleType & vbCrLf
'                End If
'            End If

'            sbKOTBillPrintBaseTakeAway.Append(sbKOTBillPrintBase)
'            sbKOTBillPrintBase.Insert(0, StrTokenSalesTypeLine)
'            sbKOTBillPrintBaseTakeAway.Insert(0, StrTokenTakeAwayLine)



'            '---- Step 2- making 2 Instance 1 for DineIn 2 for TakeAway 
'            '---- looping items and construct prints 
'            Dim dvItemDetail As New DataView(dtView, "", "", DataViewRowState.CurrentRows)
'            Dim BillLineNO As String = String.Empty
'            For Each dr As DataRow In DtUnique.Rows
'                ItemCode = dr("ArticleCode").ToString()
'                Desc = IIf(DisplayArticleFullName, dr("ArticleFullName").ToString(), dr("DISCRIPTION").ToString())
'                BillLineNO = dr("BillLineNO").ToString()
'                If SpectrumCommon.ComboItemPrintingAllowed AndAlso Dtcombo IsNot Nothing AndAlso Dtcombo.Rows.Count > 0 Then
'                    Dim articleDescriptionDictionary As New Dictionary(Of String, Integer)
'                    Dim drp() = Dtcombo.Select("ComboArticleCode='" & ItemCode & "' AND BillLineNO ='" & BillLineNO & "'")

'                    If drp.Count > 0 Then
'                        For Each Row In drp
'                            If articleDescriptionDictionary.ContainsKey(Row("ArticleName")) Then
'                                articleDescriptionDictionary(Row("ArticleName")) = articleDescriptionDictionary(Row("ArticleName")) + Row("Quantity")
'                            Else
'                                articleDescriptionDictionary.Add(Row("ArticleName"), Row("Quantity"))
'                            End If
'                        Next
'                        Dim AddedDesc = GetMultilinedString(articleDescriptionDictionary)
'                        Desc = Desc & vbCrLf & AddedDesc
'                    End If
'                End If

'                Qty = dr("Quantity").ToString()
'                Dim IsForEachQuantityOver = False
'                '// if qty > 1 && qty wise flag true && item eligible for each qty print 
'                If (KOTPrintForEachQuantity) Then
'                    If Qty > 0 And dr("IsQuntityWiseRequired") = 1 Then
'                        Dim QtyArray As Array = Qty.Split(".")
'                        Dim MaxCnt As Integer = Convert.ToInt32(QtyArray(0))
'                        Qty = "1.00"
'                        If (Desc.Length >= LineLength) Or (Desc.Length + Qty.Length >= LineLength) Then
'                            sbKOTBillPrintArticleQuantityWise.Append(Desc + vbCrLf)
'                            sbKOTBillPrintArticleQuantityWise.Append(Qty.PadLeft(LineLength - 2) + vbCrLf)
'                        Else
'                            sbKOTBillPrintArticleQuantityWise.Append(Desc + Qty.PadLeft(LineLength - Desc.Length - 2) + vbCrLf)
'                        End If
'                        Dim kotQty As Integer = 0
'                        While kotQty < MaxCnt
'                            Call fnKotPrint(sbKOTBillPrintBase, sbKOTBillPrintArticleQuantityWise, errorMsg)
'                            kotQty = kotQty + 1
'                        End While
'                        'For kotQty As Integer = 0 To MaxCnt - 1
'                        '    Call fnKotPrint(sbKOTBillPrintBase, sbKOTBillPrintArticleQuantityWise, errorMsg)
'                        'Next
'                        sbKOTBillPrintArticleQuantityWise.Length = 0
'                        IsForEachQuantityOver = True
'                        ' Continue For
'                    End If
'                End If



'                TakeAwayQty = dr("TakeAwayQuantity").ToString()
'                Call SetQtyFormat(TakeAwayQty)
'                If Val(TakeAwayQty) > 0 Then
'                    isTakeAwayQtyPrint = True
'                    Qty = (dr("Quantity") - dr("TakeAwayQuantity")).ToString
'                    strLineDetail.Append(StrLineItem)
'                    If (Desc.Length >= LineLength) Or (Desc.Length + TakeAwayQty.Length >= LineLength) Then
'                        sbKOTBillPrintArticleTakeAway.Append(Desc + vbCrLf)
'                        sbKOTBillPrintArticleTakeAway.Append(TakeAwayQty.PadLeft(LineLength - 2) + vbCrLf)
'                    Else
'                        sbKOTBillPrintArticleTakeAway.Append(Desc + TakeAwayQty.PadLeft(LineLength - Desc.Length - 2) + vbCrLf)
'                    End If
'                End If

'                strLineDetail.Append(StrLineItem)
'                Call SetQtyFormat(Qty)
'                If Val(Qty) > 0 And (Not IsForEachQuantityOver) Then
'                    If (Desc.Length >= LineLength) Or (Desc.Length + Qty.Length >= LineLength) Then
'                        sbKOTBillPrintArticleMain.Append(Desc + vbCrLf)
'                        'sbKOTBillPrintArticleMain.Append(Qty.PadLeft(LineLength - Qty.Length) + vbCrLf)
'                        sbKOTBillPrintArticleMain.Append(Qty.PadLeft(LineLength - 2) + vbCrLf)
'                    Else
'                        sbKOTBillPrintArticleMain.Append(Desc + Qty.PadLeft(LineLength - Desc.Length - 2) + vbCrLf)
'                    End If
'                End If

'                ' step 3---------- Now Calling for Print ----------------
'                ''Check item is this item require each Qty Print 

'                If (KOTPrintEachlineItem) And (Not IsForEachQuantityOver) Then
'                    Call fnKotPrint(sbKOTBillPrintBase, sbKOTBillPrintArticleMain, errorMsg)
'                    If Val(TakeAwayQty) > 0 Then
'                        Call fnKotPrint(sbKOTBillPrintBaseTakeAway, sbKOTBillPrintArticleTakeAway, errorMsg)
'                    End If
'                    sbKOTBillPrintArticleMain.Length = 0
'                    Continue For
'                End If
'            Next

'            ' step 4--------- if till now not Called for Print , call now----------------
'            If (Not KOTPrintEachlineItem) Then
'                If sbKOTBillPrintArticleMain.Length > 0 Then
'                    Call fnKotPrint(sbKOTBillPrintBase, sbKOTBillPrintArticleMain, errorMsg)
'                End If
'                If isTakeAwayQtyPrint Then
'                    Call fnKotPrint(sbKOTBillPrintBaseTakeAway, sbKOTBillPrintArticleTakeAway, errorMsg)
'                End If
'            End If

'        Else
'            errorMsg = getValueByKey("CLSCMP031")
'        End If
'    Catch ex As Exception
'        errorMsg = ex.Message
'    End Try

'End Sub


'Public Sub CashMemoKOTPrintMettlerBillWise(ByVal Type As String, ByRef errorMsg As String, ByRef dtView As DataTable, ByVal DtUnique As DataTable, ByVal Dtcombo As DataTable, CustomerNameLine As String)
'    Try
'        Dim strLineDetail, sbKOTBillPrintBase, sbKOTBillPrintBaseTakeAway, sbKOTBillPrintArticleMain, sbKOTBillPrintArticleTakeAway, sbKOTBillPrintArticleQuantityWise As New StringBuilder
'        Dim StrCMNo, StrTokenNo, StrCustomerName As String
'        Dim LineItemHeading, StrSubHeader, StrLineItem As String
'        Dim ItemCode, Desc, Qty, mettlerBillQty, BillNoList As String
'        Dim PricePerUnit, Disc As String
'        Dim strLine, StrTokenSalesTypeLine, StrTokenTakeAwayLine As String
'        Dim strDblLine As String
'        Dim StrHeader As String
'        Dim ValueMultiplyFactor As Double = 1
'        Dim ItemAmt, BillTotalAmt As Double
'        Dim isTakeAwayQtyPrint As Boolean = False
'        Dim objItemSch As New clsIteamSearch()
'        Dim colSpacelength As Int16 = 5
'        Dim ScaleBillIntDate As Long
'        Dim SiteName = getValueByKey("CLSCMP004") & " " & dtView.Rows(0)("SITEOFFICIALNAME").ToString()
'        SiteName = dtView.Rows(0)("SITEOFFICIALNAME").ToString()
'        Dim TS_SiteName = SplitString(SiteName)
'        SiteName = TS_SiteName.ToString
'        SiteName = SiteName.PadLeft(LineLength / 2 + (SiteName.Length / 2))
'        Dim Site = vbCrLf & getValueByKey("CLSCMP005") & " " & dtView.Rows(0)("SITECODE").ToString()
'        Dim SiteLine = SiteName

'        Dim StrSiteTelline = Space(5) & "PHONE: " & dtView.Rows(0)("TELNO").ToString()
'        Dim StrAdrressLine = Space(5) & dtView.Rows(0)("ADDRESS").ToString()
'        StrAdrressLine = SplitString(StrAdrressLine, LineLength).ToString()

'        StrHeader &= SiteLine & vbCrLf
'        StrHeader = StrHeader & StrAdrressLine.Trim & vbCrLf
'        StrHeader = StrHeader & StrSiteTelline & vbCrLf


'        Dim TotaltakeAwayQty = DtUnique.Compute("SUM(TakeAwayQuantity)", "").ToString()
'        strLine = "-".PadRight(LineLength, "-") & vbCrLf
'        strDblLine = "=".PadRight(LineLength, "=") & vbCrLf
'        errorMsg = ""
'        '----Create Sub Header -----
'        If Type = "DCM" Then
'            StrCMNo = getValueByKey("CLSCMP008") & dtView.Rows(0)("Billno").ToString()
'        Else
'            StrCMNo = getValueByKey("CLSCMP009") & dtView.Rows(0)("Billno").ToString()
'        End If
'        Dim StrCashMemoLine = StrCMNo & vbCrLf
'        Dim StrCMTime = getValueByKey("CLSCMP032") & Format(dtView.Rows(0)("BillTime"), "hh:mm:ss tt")

'        For ScanBillIndex = 0 To DtScannedMettlerBills.Rows.Count - 1
'            sbKOTBillPrintBase.Length = 0
'            BillTotalAmt = 0
'            sbKOTBillPrintArticleMain.Length = 0
'            StrSubHeader = String.Empty
'            sbKOTBillPrintBase.Append(vbCrLf)
'            '------ Adding Header 
'            sbKOTBillPrintBase.Append("PRASHANT CORNER".PadLeft(CInt((LineLength - "PRASHANT CORNER".Length)), " ") & vbCrLf)
'            sbKOTBillPrintBase.Append(StrHeader)
'            '------- GET Mettler Scanned Bill Details From Mettler DataBase 
'            Dim ScaleNo As Integer = Val(DtScannedMettlerBills.Rows(ScanBillIndex)("SCALE_NO"))
'            Dim BillNo As Integer = Val(DtScannedMettlerBills.Rows(ScanBillIndex)("BILL_NO"))
'            ScaleBillIntDate = Val(DtScannedMettlerBills.Rows(ScanBillIndex)("MettlerScaleBillDate"))
'            BillNoList = BillNoList & BillNo.ToString() & ","
'            sbKOTBillPrintBase.Append(vbCrLf)
'            sbKOTBillPrintBase.Append(vbCrLf)

'            StrSubHeader = StrSubHeader & StrCashMemoLine
'            StrSubHeader = StrSubHeader & "DATE:" & "<B>" & strDayOpenDate & "</B>"
'            StrSubHeader = StrSubHeader & ("<B>" & "No:" & BillNo).PadLeft(LineLength - ("DATE:" & strDayOpenDate).ToString.Length + 3) & "</B>" & vbCrLf
'            StrSubHeader = StrSubHeader & StrCMTime
'            StrSubHeader = StrSubHeader & ("Scale: " & ScaleNo).PadLeft(LineLength - StrCMTime.Length)
'            sbKOTBillPrintBase.Append(StrSubHeader)
'            sbKOTBillPrintBase.Append(vbCrLf)
'            sbKOTBillPrintBase.Append(vbCrLf)
'            Dim dtScanedBillArticle = objItemSch.GetScanedBillArticle(ScaleNo:=ScaleNo, BillNo:=BillNo, BillIntDate:=ScaleBillIntDate, MettlerConn:=MettlerConnString)
'            '---- Get Date And Print
'            '---- Columns Heading 
'            sbKOTBillPrintBase.Append(strLineL40 + vbCrLf)
'            sbKOTBillPrintBase.Append("Article" & Space(8) & "Code" & Space(8) & "Price/Unit" & vbCrLf)
'            sbKOTBillPrintBase.Append("Disc" & Space(11) & "Qty" & Space(14) & "Total" & vbCrLf)
'            'sbKOTBillPrintBase.Append(getValueByKey("CLSPSO020") + getValueByKey("CLSPSO022").PadLeft(LineLength - getValueByKey("CLSPSO020").Length - 4) + vbCrLf)
'            sbKOTBillPrintBase.Append(strLineL40 + vbCrLf)
'            sbKOTBillPrintBase.Append(vbCrLf)

'            For BillRowIndex = 0 To dtScanedBillArticle.Rows.Count - 1 Step 1
'                Dim PrintDataRows() = DtUnique.Select("ArticleCode ='" & dtScanedBillArticle.Rows(BillRowIndex)("LegacyArticleCode") & "'")
'                If Not PrintDataRows Is Nothing And PrintDataRows.Count > 0 Then
'                    '------ STEP-1:  collecting all Data With Format -----
'                    For Each dr As DataRow In PrintDataRows
'                        ItemCode = dr("ArticleCode").ToString()
'                        Desc = "<B> " & IIf(DisplayArticleFullName, dr("ArticleFullName").ToString(), dr("DISCRIPTION").ToString()) & "</B>"
'                        Qty = dr("Quantity").ToString()
'                        mettlerBillQty = dtScanedBillArticle.Rows(BillRowIndex)("Quantity")
'                        ValueMultiplyFactor = Math.Round(Val(mettlerBillQty) / Val(Qty), 3)
'                        PricePerUnit = Math.Round(dr("SELLINGPRICE"), 2).ToString()
'                        Disc = Math.Round(Val(dr("TOTALDISCOUNT").ToString()) * ValueMultiplyFactor, 2).ToString
'                        ItemAmt = Math.Round(Val(dr("NETAMOUNT").ToString()) * ValueMultiplyFactor, 2).ToString
'                        BillTotalAmt = BillTotalAmt + Val(ItemAmt)
'                        'If dr("UnitofMeasure").ToString().ToUpper = "KGS" Then
'                        '    mettlerBillQty = (Val(mettlerBillQty) / 1000).ToString
'                        'End If
'                        Call SetQtyFormat(mettlerBillQty)
'                        If dr("UnitofMeasure").ToString().ToUpper = "KGS" Then
'                            mettlerBillQty = mettlerBillQty & " Kg"
'                        End If

'                        Dim CurrentLineLength As Integer = 0
'                        '-- Code PRICE , DISC , Qty , Total 
'                        If (Desc.Length >= LineLength) Or (Desc.Length + ItemCode.Length >= LineLength) Then
'                            sbKOTBillPrintArticleMain.Append(Desc + vbCrLf)
'                            sbKOTBillPrintArticleMain.Append(ItemCode + Space(colSpacelength))
'                            CurrentLineLength = ItemCode.Length + colSpacelength
'                        Else
'                            sbKOTBillPrintArticleMain.Append(Desc + ItemCode.PadLeft(LineLength - Desc.Length - 2) + vbCrLf)
'                        End If
'                        sbKOTBillPrintArticleMain.Append(PricePerUnit + Space(colSpacelength))
'                        CurrentLineLength = CurrentLineLength + PricePerUnit.Length + colSpacelength

'                        If CurrentLineLength + Disc.Length > 40 Then
'                            sbKOTBillPrintArticleMain.Append(vbCrLf)
'                            sbKOTBillPrintArticleMain.Append(Disc + Space(colSpacelength))
'                            CurrentLineLength = PricePerUnit.Length + colSpacelength
'                        Else
'                            sbKOTBillPrintArticleMain.Append(Disc + Space(colSpacelength))
'                            CurrentLineLength = CurrentLineLength + Disc.Length + colSpacelength
'                        End If

'                        If CurrentLineLength + mettlerBillQty.Length > 40 Then
'                            sbKOTBillPrintArticleMain.Append(vbCrLf)
'                            sbKOTBillPrintArticleMain.Append(mettlerBillQty + Space(colSpacelength))
'                            CurrentLineLength = mettlerBillQty.Length + colSpacelength
'                        Else
'                            sbKOTBillPrintArticleMain.Append(mettlerBillQty + Space(colSpacelength))
'                            CurrentLineLength = CurrentLineLength + mettlerBillQty.Length + colSpacelength
'                        End If

'                        If CurrentLineLength + ItemAmt.ToString().Length > 41 Then
'                            sbKOTBillPrintArticleMain.Append(vbCrLf)
'                            sbKOTBillPrintArticleMain.Append(ItemAmt.ToString().PadLeft(LineLength - 2) & vbCrLf)
'                        Else
'                            sbKOTBillPrintArticleMain.Append(ItemAmt.ToString().PadLeft(LineLength - CurrentLineLength - 2) & vbCrLf)
'                        End If
'                        sbKOTBillPrintArticleMain.Append(vbCrLf)
'                    Next
'                Else
'                    Continue For
'                End If
'            Next BillRowIndex
'            '-----------------------------------------------------------------
'            sbKOTBillPrintArticleMain.AppendLine(strLine & vbCrLf)
'            Dim totalItems = Val(DtScannedMettlerBills.Rows(ScanBillIndex)("TotalLineItems")).ToString()
'            sbKOTBillPrintArticleMain.Append("<B>" & "ITEMS: " & totalItems & "</B>")
'            sbKOTBillPrintArticleMain.Append(("<B>" & "TOTAL: " & Val(BillTotalAmt)).PadLeft(LineLength - ("ITEMS: " & totalItems).Length + 2) & "</B>" & vbCrLf)

'            sbKOTBillPrintArticleMain.Append(vbCrLf & vbCrLf & "<L>BILL PAID</L>".PadLeft(CInt((LineLength - "BILL PAID".Length) / 2) + "BILL PAID".Length) & vbCrLf & vbCrLf & vbCrLf)
'            sbKOTBillPrintArticleMain.Append(Space(4) & "VAT/TIN NO: 27120029370 U/V" & vbCrLf)
'            sbKOTBillPrintArticleMain.Append(Space(5) & "LBT NO: TMC-LBT0005578-13" & vbCrLf)
'            sbKOTBillPrintArticleMain.Append(vbCrLf & Space(6) & "THANK YOU .. VISIT AGAIN " & vbCrLf)
'            If sbKOTBillPrintArticleMain.Length > 0 Then
'                Call fnKotPrint(sbKOTBillPrintBase, sbKOTBillPrintArticleMain, errorMsg)
'            End If

'        Next ScanBillIndex
'        '------ CODE FOR THOSE ITEMS IS NOT THE PART OF METTLER SCAN 
'        '----- Now Process those element not belong to mettler Bills
'        Dim isPrintOtherItems As Boolean
'        Dim totalOtherItems As Integer = 0
'        sbKOTBillPrintArticleMain.Length = 0
'        BillTotalAmt = 0
'        sbKOTBillPrintBase.Length = 0
'        BillTotalAmt = 0
'        sbKOTBillPrintArticleMain.Length = 0
'        StrSubHeader = String.Empty
'        sbKOTBillPrintBase.Append(vbCrLf)
'        '------ Adding Header 
'        sbKOTBillPrintBase.Append("PRASHANT CORNER".PadLeft(CInt((LineLength - "PRASHANT CORNER".Length)), " ") & vbCrLf)
'        sbKOTBillPrintBase.Append(StrHeader)
'        '------- GET Mettler Scanned Bill Details From Mettler DataBase 
'        sbKOTBillPrintBase.Append(vbCrLf)
'        sbKOTBillPrintBase.Append(vbCrLf)

'        StrSubHeader = StrSubHeader & StrCashMemoLine
'        StrSubHeader = StrSubHeader & "DATE:" & "<B>" & strDayOpenDate & "</B>"
'        StrSubHeader = StrSubHeader & ("<B>" & "No:" & "").PadLeft(LineLength - ("DATE:" & strDayOpenDate).Length - 2) & "</B>" & vbCrLf
'        StrSubHeader = StrSubHeader & StrCMTime
'        StrSubHeader = StrSubHeader & ("Scale: " & "").PadLeft(LineLength - StrCMTime.Length - 2)
'        sbKOTBillPrintBase.Append(StrSubHeader)
'        sbKOTBillPrintBase.Append(vbCrLf)
'        sbKOTBillPrintBase.Append(vbCrLf)
'        '---- Columns Heading 
'        sbKOTBillPrintBase.Append(strLineL40 + vbCrLf)
'        sbKOTBillPrintBase.Append("Article" & Space(8) & "Code" & Space(8) & "Price/Unit" & vbCrLf)
'        sbKOTBillPrintBase.Append("Disc" & Space(11) & "Qty" & Space(14) & "Total" & vbCrLf)
'        'sbKOTBillPrintBase.Append(getValueByKey("CLSPSO020") + getValueByKey("CLSPSO022").PadLeft(LineLength - getValueByKey("CLSPSO020").Length - 4) + vbCrLf)
'        sbKOTBillPrintBase.Append(strLineL40 + vbCrLf)
'        sbKOTBillPrintBase.Append(vbCrLf)

'        Dim dtScanedBillArticleSum = objItemSch.GetScanedCashMemoBillsArticle(BillNoList:=BillNoList.Substring(0, BillNoList.Length - 1), BillIntDate:=ScaleBillIntDate, MettlerConn:=MettlerConnString)
'        For Each dr As DataRow In DtUnique.Rows
'            Dim PrintDataRows() = dtScanedBillArticleSum.Select("LegacyArticleCode ='" & dr("ArticleCode") & "'")
'            Dim PrintDataRowsCondition2() = dtScanedBillArticleSum.Select("LegacyArticleCode ='" & dr("ArticleCode") & "' AND Quantity < " & dr("Quantity"))
'            If (PrintDataRows.Count = 0) Or (PrintDataRowsCondition2.Count > 0) Then
'                isPrintOtherItems = True
'                totalOtherItems = totalOtherItems + 1
'                ItemCode = dr("ArticleCode").ToString()
'                Desc = "<B> " & IIf(DisplayArticleFullName, dr("ArticleFullName").ToString(), dr("DISCRIPTION").ToString()) & "</B>"
'                If PrintDataRows.Count > 0 Then
'                    Qty = (Val(dr("Quantity")) - Val(PrintDataRows(0)("Quantity"))).ToString
'                    ValueMultiplyFactor = Math.Round((Val(dr("Quantity")) - Val(PrintDataRows(0)("Quantity"))) / dr("Quantity"), 3)
'                Else
'                    Qty = dr("Quantity").ToString
'                    ValueMultiplyFactor = 1
'                End If
'                PricePerUnit = Math.Round(dr("SELLINGPRICE"), 2).ToString()
'                Disc = Math.Round(Val(dr("TOTALDISCOUNT").ToString()) * ValueMultiplyFactor, 2).ToString
'                ItemAmt = Math.Round(Val(dr("NETAMOUNT").ToString()) * ValueMultiplyFactor, 2).ToString
'                BillTotalAmt = BillTotalAmt + Val(ItemAmt)
'                Call SetQtyFormat(Qty)
'                Dim CurrentLineLength As Integer = 0
'                '-- Code PRICE , DISC , Qty , Total 
'                If (Desc.Length >= LineLength) Or (Desc.Length + ItemCode.Length >= LineLength) Then
'                    sbKOTBillPrintArticleMain.Append(Desc + vbCrLf)
'                    sbKOTBillPrintArticleMain.Append(ItemCode + Space(colSpacelength))
'                    CurrentLineLength = ItemCode.Length + colSpacelength
'                Else
'                    sbKOTBillPrintArticleMain.Append(Desc + ItemCode.PadLeft(LineLength - Desc.Length - 2) + vbCrLf)
'                End If
'                sbKOTBillPrintArticleMain.Append(PricePerUnit + Space(colSpacelength))
'                CurrentLineLength = CurrentLineLength + PricePerUnit.Length + colSpacelength

'                If CurrentLineLength + Disc.Length > 40 Then
'                    sbKOTBillPrintArticleMain.Append(vbCrLf)
'                    sbKOTBillPrintArticleMain.Append(Disc + Space(colSpacelength))
'                    CurrentLineLength = PricePerUnit.Length + colSpacelength
'                Else
'                    sbKOTBillPrintArticleMain.Append(Disc + Space(colSpacelength))
'                    CurrentLineLength = CurrentLineLength + Disc.Length + colSpacelength
'                End If

'                If CurrentLineLength + Qty.Length > 40 Then
'                    sbKOTBillPrintArticleMain.Append(vbCrLf)
'                    sbKOTBillPrintArticleMain.Append(Qty + Space(colSpacelength))
'                    CurrentLineLength = mettlerBillQty.Length + colSpacelength
'                Else
'                    sbKOTBillPrintArticleMain.Append(Qty + Space(colSpacelength))
'                    CurrentLineLength = CurrentLineLength + Qty.Length + colSpacelength
'                End If

'                If CurrentLineLength + ItemAmt.ToString().Length > 41 Then
'                    sbKOTBillPrintArticleMain.Append(vbCrLf)
'                    sbKOTBillPrintArticleMain.Append(ItemAmt.ToString().PadLeft(LineLength - 2) & vbCrLf)
'                Else
'                    sbKOTBillPrintArticleMain.Append(ItemAmt.ToString().PadLeft(LineLength - CurrentLineLength - 2) & vbCrLf)
'                End If
'                sbKOTBillPrintArticleMain.Append(vbCrLf)
'            End If
'        Next
'        sbKOTBillPrintArticleMain.AppendLine(strLine & vbCrLf)
'        sbKOTBillPrintArticleMain.Append("<B>" & "ITEMS: " & totalOtherItems & "</B>")
'        sbKOTBillPrintArticleMain.Append(("<B>" & "TOTAL: " & Val(BillTotalAmt)).PadLeft(LineLength - ("ITEMS: " & totalOtherItems).Length) & "</B>" & vbCrLf)

'        sbKOTBillPrintArticleMain.Append(vbCrLf & vbCrLf & "<L>BILL PAID</L>".PadLeft(CInt((LineLength - "BILL PAID".Length) / 2) + "BILL PAID".Length) & vbCrLf & vbCrLf & vbCrLf)
'        sbKOTBillPrintArticleMain.Append(Space(4) & "VAT/TIN NO: 27120029370 U/V" & vbCrLf)
'        sbKOTBillPrintArticleMain.Append(Space(5) & "LBT NO: TMC-LBT0005578-13" & vbCrLf)
'        sbKOTBillPrintArticleMain.Append(vbCrLf & Space(6) & "THANK YOU .. VISIT AGAIN " & vbCrLf)

'        If sbKOTBillPrintArticleMain.Length > 0 AndAlso isPrintOtherItems Then
'            Call fnKotPrint(sbKOTBillPrintBase, sbKOTBillPrintArticleMain, errorMsg)
'        End If

'    Catch ex As Exception
'        errorMsg = ex.Message
'    End Try
'End Sub
