﻿Public Class clsDefaultConfiguration
#Region "Shared Varibles"
    Shared _SalesPersonReq As Boolean = False
    Shared _TaxDetailsReq As Boolean = False
    Shared _ManualPromo As Boolean = False
    Shared _ItemFullName As Boolean = False
    'Shared _PrintDiscPerc As Boolean = False
    Shared _PriceChange As Boolean = False
    Shared _AdvanceSalesAllowed As Boolean = False
    'Shared _NagativeBillAllowed As Boolean = False
    Shared _VoucherDays As Int32
    Shared _voucherText As String
    Shared _CLPIntimation As Boolean = False
    Shared _CLPRegistration As Boolean = False
    Shared _WithoutBill As Boolean = False
    Shared _TaxAplicable As Boolean = False
    Shared _CashRefund As Boolean = False
    Shared _creditCard As Boolean = False
    Shared _PrintingTax As Boolean = False
    Shared _PromMessase As Boolean = False
    Shared _HoldBill As Boolean = False
    'Shared _PrintingType As String
    Shared _CheckTillOpenAmount As String
    Shared _InclusiveTaxDisplay As String
    'Shared _ZeroTax As Boolean = False
    Shared _IssCrVoucher As Boolean = False
    'Shared _CLPPointSale As Boolean = False
    Shared _GVSale As Boolean = False
    Shared _OtherCharges As Boolean = False
    'Shared _HeaderPrint As Boolean = False
    'Shared _FooterPrint As Boolean = False
    Shared _PrintPreview As Boolean = False
    Shared _TillClosePrintPreivewReq As Boolean = False

    Shared _CashMemoPrinter As String = ""
    'Shared _OtherPrinter As String = ""
    Shared _CLPRegistrationAmount As Double = 0
    'Shared _CMStorageLoc As String = ""
    'Shared _IsArticleTaxAllowed As Boolean = False
    'Shared _BLStorageLoc As String = ""
    'Shared _SOStorageLoc As String = ""
    Shared _TaxAfterDisc As Boolean = False
    'Shared _RoundAt As Int32
    Shared _ShowDiscAmt As Boolean = False
    Shared _ReturnReason As Boolean = False
    Shared _UserAmountPrint As Boolean = False
    Shared _TillOpenNClose As Boolean = False
    Shared _CheckExpiryMonth As Int16
    Shared _CheckTillClose As Boolean = False
    Shared _TillOperation As Boolean = False
    Shared _TillOpen As Boolean = False
    Shared _GVArticle As String
    Shared _CLPArticle As String
    'Shared _CVArticle As String
    Shared _HoldBillBarcode As Boolean = False
    Shared _ChequeInfo As Boolean = False
    Shared _IsCreditCardRefundAllowed As Boolean = False
    Shared _IsChequeRefundAllowed As Boolean = False
    'Shared _RoundOffRequired As Boolean = False
    'Shared _BLRoundOffRequired As Boolean = False
    Shared _isMAPbasedCost As Boolean = False
    Shared _SignOnRequired As Boolean = False
    'Shared _CompanyName As String
    Shared _DecimalPlaces As Integer
    Shared _DayCloseOtherScreens As Boolean
    Shared _BankTotalCheck As Boolean
    Shared _IsDeliveryCopyRequired As Boolean
    Shared _CLP_Applicable_Edit As Boolean
    Shared _CLP_Point_On_redeemption As Boolean
    Public Shared _WeightBarcodeSequence As String
    Shared _WeightBarcodeRateDecimalLength As Int32
    Shared _WeightBarcodeRateWholeNOLength As Int32
    Shared _WeightBarcodeRateLength As Int32
    Shared _WeightBarcodeRateApplicable As Boolean
    Shared _WeightBarcodedecimalLength As String
    Shared _WeightBarcodeWholeNOLength As Int32
    Shared _WeightBarcodeLength As Int32
    Shared _WeightBarcodePrefixDigits As Int32
    Shared _WeightBarcodePrefix As String
    Shared _WeightScaleEnabled As Boolean

    Public Shared NotificationTimerStart As Boolean

#End Region
#Region "Shared Property's"
    Private Shared _HideControlsFromStockTake As Boolean  'Jayesh 03/05/2019
    Public Shared Property HideControlsFromStockTake() As Boolean
        Get
            Return _HideControlsFromStockTake
        End Get
        Set(ByVal value As Boolean)
            _HideControlsFromStockTake = value
        End Set
    End Property
    Private Shared _xProviderId As String
    Public Shared Property xProviderId() As String
        Get
            Return _xProviderId
        End Get
        Set(ByVal value As String)
            _xProviderId = value
        End Set
    End Property
    Private Shared _DisableAmountPaidToVendorsInTillClose As Boolean  'Jayesh 03/05/2019
    Public Shared Property DisableAmountPaidToVendorsInTillClose() As Boolean
        Get
            Return _DisableAmountPaidToVendorsInTillClose
        End Get
        Set(ByVal value As Boolean)
            _DisableAmountPaidToVendorsInTillClose = value
        End Set
    End Property
    Private Shared _ISDineInKOTRequired As Boolean  'Jayesh 25/01/2019
    Public Shared Property ISDineInKOTRequired() As Boolean
        Get
            Return _ISDineInKOTRequired
        End Get
        Set(ByVal value As Boolean)
            _ISDineInKOTRequired = value
        End Set
    End Property
    Private Shared _EnableOnlineOrderNotification As Boolean = False
    Public Shared Property EnableOnlineOrderNotification() As Boolean
        Get
            Return _EnableOnlineOrderNotification
        End Get
        Set(ByVal value As Boolean)
            _EnableOnlineOrderNotification = value
        End Set
    End Property


    Private Shared _OnlineOrderNotificationInterval As Integer
    Public Shared Property OnlineOrderNotificationInterval() As Integer
        Get
            Return _OnlineOrderNotificationInterval
        End Get
        Set(ByVal value As Integer)
            _OnlineOrderNotificationInterval = value
        End Set
    End Property
    Private Shared _EnableRejectOrderNotification As Boolean = False
    Public Shared Property EnableRejectOrderNotification() As Boolean
        Get
            Return _EnableRejectOrderNotification
        End Get
        Set(ByVal value As Boolean)
            _EnableRejectOrderNotification = value
        End Set
    End Property

    Private Shared _OnlineOrderRejectionInterval As Integer
    Public Shared Property OnlineOrderRejectionInterval() As Integer
        Get
            Return _OnlineOrderRejectionInterval
        End Get
        Set(ByVal value As Integer)
            _OnlineOrderRejectionInterval = value
        End Set
    End Property

    Private Shared _ZomatoConformOrderAPI As String
    Public Shared Property ZomatoConformOrderAPI() As String
        Get
            Return _ZomatoConformOrderAPI
        End Get
        Set(ByVal value As String)
            _ZomatoConformOrderAPI = value
        End Set
    End Property
    Private Shared _ZomatoRejectOrderAPI As String
    Public Shared Property ZomatoRejectOrderAPI() As String
        Get
            Return _ZomatoRejectOrderAPI
        End Get
        Set(ByVal value As String)
            _ZomatoRejectOrderAPI = value
        End Set
    End Property
    Private Shared _labelPrintFormatNo As Integer
    Public Shared Property labelPrintFormatNo() As Integer
        Get
            Return _labelPrintFormatNo
        End Get
        Set(ByVal value As Integer)
            _labelPrintFormatNo = value
        End Set
    End Property
    Private Shared _EnableZomatoIntegration As Boolean = False
    Public Shared Property EnableZomatoIntegration() As Boolean
        Get
            Return _EnableZomatoIntegration
        End Get
        Set(ByVal value As Boolean)
            _EnableZomatoIntegration = value
        End Set
    End Property
    Private Shared _ZomatoAPIKey As String = "" 'vipin 24.08.2018
    Public Shared Property ZomatoAPIKey() As String
        Get
            Return _ZomatoAPIKey
        End Get
        Set(ByVal value As String)
            _ZomatoAPIKey = value
        End Set
    End Property
    Private Shared _HierarchyforKOT4eachQuantity As String 'Jayesh 26/12/2018
    Public Shared Property HierarchyforKOT4eachQuantity() As String
        Get
            Return _HierarchyforKOT4eachQuantity
        End Get
        Set(ByVal value As String)
            _HierarchyforKOT4eachQuantity = value
        End Set
    End Property
    Private Shared _DetailedCustomerCreationformat As String
    Public Shared Property DetailedCustomerCreationformat() As String
        Get
            Return _DetailedCustomerCreationformat
        End Get
        Set(ByVal value As String)
            _DetailedCustomerCreationformat = value
        End Set
    End Property
    'add by khusrao adil on 15-09-2017 for for om ganesha
    Public Shared _DisplayTaxInGenerateBill As Boolean = False
    Public Shared Property DisplayTaxInGenerateBill() As Boolean
        Get
            Return _DisplayTaxInGenerateBill
        End Get
        Set(ByVal value As Boolean)
            _DisplayTaxInGenerateBill = value
        End Set
    End Property
    Private Shared _UrlForCheckingInternetConnection As String = "http://www.google.com" 'vipul
    Public Shared Property UrlForCheckingInternetConnection() As String
        Get
            Return _UrlForCheckingInternetConnection
        End Get
        Set(ByVal value As String)
            _UrlForCheckingInternetConnection = value
        End Set
    End Property
    Private Shared _NumberOfDaysApplicableForReprint As String
    Public Shared Property NumberOfDaysApplicableForReprint() As String
        Get
            Return _NumberOfDaysApplicableForReprint
        End Get
        Set(ByVal value As String)
            _NumberOfDaysApplicableForReprint = value
        End Set
    End Property

    '------
    Private Shared _BatchFileProcessName As String
    Public Shared Property BatchFileProcessName() As String
        Get
            Return _BatchFileProcessName
        End Get
        Set(ByVal value As String)
            _BatchFileProcessName = value
        End Set
    End Property

    Private Shared _DefaultSaleType As String
    Public Shared Property DefaultSaleType() As String
        Get
            Return _DefaultSaleType
        End Get
        Set(ByVal value As String)
            _DefaultSaleType = value
        End Set
    End Property
    Shared _updateStockOnDayCloseWastage As Boolean = False
    Public Shared Property UpdateStockOnDayCloseWastage() As Boolean
        Get
            Return _updateStockOnDayCloseWastage
        End Get
        Set(ByVal value As Boolean)
            _updateStockOnDayCloseWastage = value
        End Set
    End Property
    Shared _AllowDayCloseInOfflineMode As Boolean = False 'vipin
    Public Shared Property AllowDayCloseInOfflineMode() As Boolean
        Get
            Return _AllowDayCloseInOfflineMode
        End Get
        Set(ByVal value As Boolean)
            _AllowDayCloseInOfflineMode = value
        End Set
    End Property
    Shared _BarcodeDisplayAllowed As Boolean = False
    Public Shared Property BarcodeDisplayAllowed() As Boolean
        Get
            Return _BarcodeDisplayAllowed
        End Get
        Set(ByVal value As Boolean)
            _BarcodeDisplayAllowed = value
        End Set
    End Property

    Private Shared _RoundOffRequired As Boolean = False
    Public Shared Property RoundOffRequired() As Boolean
        Get
            Return _RoundOffRequired
        End Get
        Set(ByVal value As Boolean)
            _RoundOffRequired = value
        End Set
    End Property
    Private Shared _DirectCashPayment As Boolean = False
    Public Shared Property DirectCashPayment() As Boolean
        Get
            Return _DirectCashPayment
        End Get
        Set(ByVal value As Boolean)
            _DirectCashPayment = value
        End Set
    End Property
    Private Shared _AutoUpdate As Boolean = False
    Public Shared Property AutoUpdate() As Boolean
        Get
            Return _AutoUpdate
        End Get
        Set(ByVal value As Boolean)
            _AutoUpdate = value
        End Set
    End Property
    Private Shared _BillRoundOffAt As Integer
    Public Shared Property BillRoundOffAt() As Integer
        Get
            Return _BillRoundOffAt
        End Get
        Set(ByVal value As Integer)
            _BillRoundOffAt = value
        End Set
    End Property
    Private Shared _PCRoundOff As Boolean = False
    Public Shared Property PCRoundOff() As Boolean
        Get
            Return _PCRoundOff
        End Get
        Set(ByVal value As Boolean)
            _PCRoundOff = value
        End Set
    End Property
    '---for baharin client
    Private Shared _BillRoundOffNotRequired As Boolean = False
    Public Shared Property BillRoundOffNotRequired() As Boolean
        Get
            Return _BillRoundOffNotRequired
        End Get
        Set(ByVal value As Boolean)
            _BillRoundOffNotRequired = value
        End Set
    End Property
    Private Shared _ArticleTaxAllowed As Boolean = False
    Public Shared Property ArticleTaxAllowed() As Boolean
        Get
            Return _ArticleTaxAllowed
        End Get
        Set(ByVal value As Boolean)
            _ArticleTaxAllowed = value
        End Set
    End Property
    Public Shared Property IsDeliveryCopyRequired() As Boolean
        Get
            Return _IsDeliveryCopyRequired
        End Get
        Set(ByVal value As Boolean)
            _IsDeliveryCopyRequired = value
        End Set
    End Property

    Private Shared _NegativeInventoryAllowed As Boolean = False
    Public Shared Property NegativeInventoryAllowed() As Boolean
        Get
            Return _NegativeInventoryAllowed
        End Get
        Set(ByVal value As Boolean)
            _NegativeInventoryAllowed = value
        End Set
    End Property

    Private Shared _CLPPointSaleAllowed As Boolean = False
    Public Shared Property CLPPointSaleAllowed() As Boolean
        Get
            Return _CLPPointSaleAllowed
        End Get
        Set(ByVal value As Boolean)
            _CLPPointSaleAllowed = value
        End Set
    End Property

    Private Shared _StockStorageLocation As String = "System"
    Public Shared ReadOnly Property StockStorageLocation() As String
        Get
            Return _StockStorageLocation
        End Get
    End Property
    Private Shared _DatabaseBackupPath As String
    Public Shared Property DatabaseBackupPath() As String
        Get
            Return _DatabaseBackupPath
        End Get
        Set(ByVal value As String)
            _DatabaseBackupPath = value
        End Set
    End Property
    Private Shared _AutoDBBackupOnShiftClose As Boolean
    Public Shared Property AutoDBBackupOnShiftClose() As Boolean
        Get
            Return _AutoDBBackupOnShiftClose
        End Get
        Set(ByVal value As Boolean)
            _AutoDBBackupOnShiftClose = value
        End Set
    End Property

    Public Shared Property WeightScaleEnabled() As Boolean
        Get
            Return _WeightScaleEnabled
        End Get
        Set(ByVal value As Boolean)
            _WeightScaleEnabled = value
        End Set
    End Property

    Public Shared Property WeightBarcodePrefix() As String
        Get
            Return _WeightBarcodePrefix
        End Get
        Set(ByVal value As String)
            _WeightBarcodePrefix = value
        End Set
    End Property

    Public Shared Property WeightBarcodePrefixDigits() As Int32
        Get
            Return _WeightBarcodePrefixDigits
        End Get
        Set(ByVal value As Int32)
            _WeightBarcodePrefixDigits = value
        End Set
    End Property


    Public Shared Property WeightBarcodeLength() As Int32
        Get
            Return _WeightBarcodeLength
        End Get
        Set(ByVal value As Int32)
            _WeightBarcodeLength = value
        End Set
    End Property


    Public Shared Property WeightBarcodeWholeNOLength() As Int32
        Get
            Return _WeightBarcodeWholeNOLength
        End Get
        Set(ByVal value As Int32)
            _WeightBarcodeWholeNOLength = value
        End Set
    End Property

    Public Shared Property WeightBarcodedecimalLength() As String
        Get
            Return _WeightBarcodedecimalLength
        End Get
        Set(ByVal value As String)
            _WeightBarcodedecimalLength = value
        End Set
    End Property


    Public Shared Property WeightBarcodeRateApplicable() As Boolean
        Get
            Return _WeightBarcodeRateApplicable
        End Get
        Set(ByVal value As Boolean)
            _WeightBarcodeRateApplicable = value
        End Set
    End Property

    Public Shared Property WeightBarcodeRateLength() As Int32
        Get
            Return _WeightBarcodeRateLength
        End Get
        Set(ByVal value As Int32)
            _WeightBarcodeRateLength = value
        End Set
    End Property

    Public Shared Property WeightBarcodeRateWholeNOLength() As Int32
        Get
            Return _WeightBarcodeRateWholeNOLength
        End Get
        Set(ByVal value As Int32)
            _WeightBarcodeRateWholeNOLength = value
        End Set
    End Property
    Private Shared _DisplayBrandWiseSale As Boolean = False
    Public Shared Property DisplayBrandWiseSale() As Boolean
        Get
            Return _DisplayBrandWiseSale
        End Get
        Set(ByVal value As Boolean)
            _DisplayBrandWiseSale = value
        End Set
    End Property
    Public Shared Property WeightBarcodeRateDecimalLength() As Int32
        Get
            Return _WeightBarcodeRateDecimalLength
        End Get
        Set(ByVal value As Int32)
            _WeightBarcodeRateDecimalLength = value
        End Set
    End Property

    Private Shared _Comport As String
    Public Shared Property Comport() As String
        Get
            Return _Comport
            'Return "COM1"
        End Get
        Set(ByVal value As String)
            _Comport = value
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

    Private Shared _PackagingBoxLastNodeCode As String
    Public Shared Property PackagingBoxLastNodeCode() As String
        Get
            Return _PackagingBoxLastNodeCode
        End Get
        Set(ByVal value As String)
            _PackagingBoxLastNodeCode = value
        End Set
    End Property
    Private Shared _SyncOnDayClose As Boolean
    Public Shared Property SyncOnDayClose() As Boolean
        Get
            Return _SyncOnDayClose
        End Get
        Set(ByVal value As Boolean)
            _SyncOnDayClose = value
        End Set
    End Property
    Private Shared _DineInProcess As Boolean = False
    Public Shared Property DineInProcess() As Boolean
        Get
            Return _DineInProcess
        End Get
        Set(ByVal value As Boolean)
            _DineInProcess = value
        End Set
    End Property
    Private Shared _NO_OF_BACK_DAYS_FOR_SYNC As String = ""
    Public Shared Property NO_OF_BACK_DAYS_FOR_SYNC() As String
        Get
            Return _NO_OF_BACK_DAYS_FOR_SYNC
        End Get
        Set(ByVal value As String)
            _NO_OF_BACK_DAYS_FOR_SYNC = value
        End Set
    End Property



    Private Shared _ALLOWED_CSV_TERMINALS_FOR_UPDATE_SYNCH As String = ""
    Public Shared Property ALLOWED_CSV_TERMINALS_FOR_UPDATE_SYNCH() As String
        Get
            Return _ALLOWED_CSV_TERMINALS_FOR_UPDATE_SYNCH
        End Get
        Set(ByVal value As String)
            _ALLOWED_CSV_TERMINALS_FOR_UPDATE_SYNCH = value
        End Set
    End Property

    ''' <summary>
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared _ArticleHierarchySynchronizer As String = ""
    Public Shared Property ArticleHierarchySynchronizer() As String
        Get
            Return _ArticleHierarchySynchronizer
        End Get
        Set(ByVal value As String)
            _ArticleHierarchySynchronizer = value
        End Set
    End Property

    Private Shared _ArticleSynchronizer As String = ""
    Public Shared Property ArticleSynchronizer() As String
        Get
            Return _ArticleSynchronizer
        End Get
        Set(ByVal value As String)
            _ArticleSynchronizer = value
        End Set
    End Property
    Private Shared _AuthenticationSynchronizer As String = ""
    Public Shared Property AuthenticationSynchronizer() As String
        Get
            Return _AuthenticationSynchronizer
        End Get
        Set(ByVal value As String)
            _AuthenticationSynchronizer = value
        End Set
    End Property
    Private Shared _BillingSynchronizer As String = ""
    Public Shared Property BillingSynchronizer() As String
        Get
            Return _BillingSynchronizer
        End Get
        Set(ByVal value As String)
            _BillingSynchronizer = value
        End Set
    End Property
    Private Shared _CsvFileSyncronizer As String = ""
    Public Shared Property CsvFileSyncronizer() As String
        Get
            Return _CsvFileSyncronizer
        End Get
        Set(ByVal value As String)
            _CsvFileSyncronizer = value
        End Set
    End Property
    Private Shared _IndependentMastersSynchronizer As String = ""
    Public Shared Property IndependentMastersSynchronizer() As String
        Get
            Return _IndependentMastersSynchronizer
        End Get
        Set(ByVal value As String)
            _IndependentMastersSynchronizer = value
        End Set
    End Property
    Private Shared _UomSynchronizer As String = ""
    Public Shared Property UomSynchronizer() As String
        Get
            Return _UomSynchronizer
        End Get
        Set(ByVal value As String)
            _UomSynchronizer = value
        End Set
    End Property
    Private Shared _SbarroStockApplicable As Boolean
    Public Shared Property SbarroStockApplicable() As Boolean
        Get
            Return _SbarroStockApplicable
        End Get
        Set(ByVal value As Boolean)
            _SbarroStockApplicable = value
        End Set
    End Property
    Private Shared _BillFontSelection As String
    Public Shared Property BillFontSelection() As String
        Get
            Return _BillFontSelection
        End Get
        Set(ByVal value As String)
            _BillFontSelection = value
        End Set
    End Property

    Private Shared _CustSearchParameter As String
    Public Shared Property CustSearchParameter() As String
        Get
            Return _CustSearchParameter
        End Get
        Set(ByVal value As String)
            _CustSearchParameter = value
        End Set
    End Property

    Private Shared _CustSearchCharpos As Integer
    Public Shared Property CustSearchCharpos() As Integer
        Get
            Return _CustSearchCharpos
        End Get
        Set(ByVal value As Integer)
            _CustSearchCharpos = value
        End Set
    End Property
    Private Shared _CUSTOMERSEARCHUSINGPHONENUMBER As Boolean = False
    Public Shared Property CUSTOMERSEARCHUSINGPHONENUMBER() As Boolean
        Get
            Return _CUSTOMERSEARCHUSINGPHONENUMBER
        End Get
        Set(ByVal value As Boolean)
            _CUSTOMERSEARCHUSINGPHONENUMBER = value
        End Set
    End Property

    Private Shared _IsAfterSelectSaleTypeCmSelectionRequired As Boolean ' added by ketan
    Public Shared Property IsAfterSelectSaleTypeCmSelectionRequired() As Boolean
        Get
            Return _IsAfterSelectSaleTypeCmSelectionRequired
        End Get
        Set(ByVal value As Boolean)
            _IsAfterSelectSaleTypeCmSelectionRequired = value
        End Set
    End Property
    Private Shared _StockAnalysisReportEmails As String
    Public Shared Property StockAnalysisReportEmails() As String
        Get
            Return _StockAnalysisReportEmails
        End Get
        Set(ByVal value As String)
            _StockAnalysisReportEmails = value
        End Set
    End Property

    Private Shared _PrintSeparateKotFoReachHierarchy As Boolean
    Public Shared Property PrintSeparateKotFoReachHierarchy() As Boolean
        Get
            Return _PrintSeparateKotFoReachHierarchy
        End Get
        Set(ByVal value As Boolean)
            _PrintSeparateKotFoReachHierarchy = value
        End Set
    End Property
    Private Shared _AllowTaxMappingBasedOnOrderType As Boolean
    Public Shared Property AllowTaxMappingBasedOnOrderType() As Boolean
        Get
            Return _AllowTaxMappingBasedOnOrderType
        End Get
        Set(ByVal value As Boolean)
            _AllowTaxMappingBasedOnOrderType = value
        End Set
    End Property
    'added by khusrao adil on 24-05-208 for Shaolin client table name requierment
    'Table Name Instead of ID in DineIn
    Private Shared _tableNameForDineIn As Boolean
    Public Shared Property TableNameForDineIn() As Boolean
        Get
            Return _tableNameForDineIn
        End Get
        Set(ByVal value As Boolean)
            _tableNameForDineIn = value
        End Set
    End Property
    Private Shared _CDBuildCode As Integer
    Public Shared Property CDBuildCode() As Integer
        Get
            Return _CDBuildCode
        End Get
        Set(ByVal value As Integer)
            _CDBuildCode = value
        End Set
    End Property
    Private Shared _GrievanceRemarkAttachment As String
    Public Shared Property GrievanceRemarkAttachment() As String
        Get
            Return _GrievanceRemarkAttachment
        End Get
        Set(ByVal value As String)
            _GrievanceRemarkAttachment = value
        End Set
    End Property
    Private Shared _AttachmentTicket As Integer
    Public Shared Property TicketingAttachment() As Integer
        Get
            Return _AttachmentTicket
        End Get
        Set(ByVal value As Integer)
            _AttachmentTicket = value
        End Set
    End Property
    Private Shared _UpdateStockAtStoreLevel As Boolean = False
    Public Shared Property UpdateStockAtStoreLevel() As Boolean
        Get
            Return _UpdateStockAtStoreLevel
        End Get
        Set(ByVal value As Boolean)
            _UpdateStockAtStoreLevel = value
        End Set
    End Property
    Private Shared _IsArticleWiseKOT As String
    Public Shared Property IsArticleWiseKOT() As String
        Get
            Return _IsArticleWiseKOT
        End Get
        Set(ByVal value As String)
            _IsArticleWiseKOT = value
        End Set
    End Property

    Private Shared _IsCounterCopy As String
    Public Shared Property IsCounterCopy() As String
        Get
            Return _IsCounterCopy
        End Get
        Set(ByVal value As String)
            _IsCounterCopy = value
        End Set
    End Property

    Private Shared _IsFinalReceipt As String
    Public Shared Property IsFinalReceipt() As String
        Get
            Return _IsFinalReceipt
        End Get
        Set(ByVal value As String)
            _IsFinalReceipt = value
        End Set
    End Property
    '------
    Private Shared _ExtendScreen As Boolean = False
    Public Shared Property ExtendScreen() As Boolean
        Get
            Return _ExtendScreen
        End Get
        Set(ByVal value As Boolean)
            _ExtendScreen = value
        End Set
    End Property
    Private Shared _HariOmExtendScreen As Boolean = False
    Public Shared Property HariOmExtendScreen() As Boolean
        Get
            Return _HariOmExtendScreen
        End Get
        Set(ByVal value As Boolean)
            _HariOmExtendScreen = value
        End Set
    End Property
    Private Shared _IsPoleDisply As Boolean = False
    Public Shared Property IsPoleDisply() As Boolean
        Get
            Return _IsPoleDisply
        End Get
        Set(ByVal value As Boolean)
            _IsPoleDisply = value
        End Set
    End Property
    'added by khusrao adil on 13-03-2018 for spectrum new developement 
    Private Shared _EnableCreditSalesPopup As Boolean = False
    Public Shared Property EnableCreditSalesPopup() As Boolean
        Get
            Return _EnableCreditSalesPopup
        End Get
        Set(ByVal value As Boolean)
            _EnableCreditSalesPopup = value
        End Set
    End Property
    'added by khusrao adil on 13-03-2018 for spectrum new developement 
    Private Shared _CreditSalesPopupInterval As Integer
    Public Shared Property CreditSalesPopupInterval() As Integer
        Get
            Return _CreditSalesPopupInterval
        End Get
        Set(ByVal value As Integer)
            _CreditSalesPopupInterval = value
        End Set
    End Property
    Private Shared _EnableExpiryProductPopup As Boolean = False
    Public Shared Property EnableExpiryProductPopup() As Boolean
        Get
            Return _EnableExpiryProductPopup
        End Get
        Set(ByVal value As Boolean)
            _EnableExpiryProductPopup = value
        End Set
    End Property
    Private Shared _EnableExpiryProductDaysPopup As Integer
    Public Shared Property EnableExpiryProductDaysPopup() As Integer
        Get
            Return _EnableExpiryProductDaysPopup
        End Get
        Set(ByVal value As Integer)
            _EnableExpiryProductDaysPopup = value
        End Set
    End Property
    'added by khusrao adil on 15-03-2018 for spectrum new developement 
    Private Shared _CreditSalesPopupRecordsBeforeHours As Integer
    Public Shared Property CreditSalesPopupRecordsBeforeHours() As Integer
        Get
            Return _CreditSalesPopupRecordsBeforeHours
        End Get
        Set(ByVal value As Integer)
            _CreditSalesPopupRecordsBeforeHours = value
        End Set
    End Property

    'added by Roshan on 16-03-2018 for spectrum new developement 
    Private Shared _EnableLowStockNotificatonPopup As Boolean = False
    Public Shared Property EnableLowStockNotificatonPopup() As Boolean
        Get
            Return _EnableLowStockNotificatonPopup
        End Get
        Set(ByVal value As Boolean)
            _EnableLowStockNotificatonPopup = value
        End Set
    End Property
    'added by Roshan on 16-03-2018 for spectrum new developement 
    Private Shared _LowStockNotificationInterval As Integer
    Public Shared Property LowStockNotificationInterval() As Integer
        Get
            Return _LowStockNotificationInterval
        End Get
        Set(ByVal value As Integer)
            _LowStockNotificationInterval = value
        End Set
    End Property

    '
    Private Shared _EnableMailReSend As Boolean = False 'vipin 6.08.2018
    Public Shared Property EnableMailReSend() As Boolean
        Get
            Return _EnableMailReSend
        End Get
        Set(ByVal value As Boolean)
            _EnableMailReSend = value
        End Set
    End Property
    Private Shared _FailedMailReSendInterval As Integer
    Public Shared Property FailedMailReSendInterval() As Integer
        Get
            Return _FailedMailReSendInterval
        End Get
        Set(ByVal value As Integer)
            _FailedMailReSendInterval = value
        End Set
    End Property
    Private Shared _EnableSalesOrderPopup As Boolean = False
    Public Shared Property EnableSalesOrderPopup() As Boolean
        Get
            Return _EnableSalesOrderPopup
        End Get
        Set(ByVal value As Boolean)
            _EnableSalesOrderPopup = value
        End Set
    End Property
    'added by Roshan on 27-03-2018 for spectrum new developement 
    Private Shared _SalesOrderPopupInterval As Integer
    Public Shared Property SalesOrderPopupInterval() As Integer
        Get
            Return _SalesOrderPopupInterval
        End Get
        Set(ByVal value As Integer)
            _SalesOrderPopupInterval = value
        End Set
    End Property
    'added by khusrao adil on 15-03-2018 for spectrum new developement 
    Private Shared _SalesOrderPopupRecordsBeforeHours As Integer
    Public Shared Property SalesOrderPopupRecordsBeforeHours() As Integer
        Get
            Return _SalesOrderPopupRecordsBeforeHours
        End Get
        Set(ByVal value As Integer)
            _SalesOrderPopupRecordsBeforeHours = value
        End Set
    End Property
    '
    Private Shared _EnableScanQRCode As Boolean = False 'vipin 02.04.2018
    Public Shared Property EnableScanQRCode() As Boolean
        Get
            Return _EnableScanQRCode
        End Get
        Set(ByVal value As Boolean)
            _EnableScanQRCode = value
        End Set
    End Property
    Private Shared _QRCodeTrailer As String = "" 'vipin 02.04.2018
    Public Shared Property QRCodeTrailer() As String
        Get
            Return _QRCodeTrailer
        End Get
        Set(ByVal value As String)
            _QRCodeTrailer = value
        End Set
    End Property
    Private Shared _IsNewCreditSale As Boolean = False 'vipin 02.04.2018
    Public Shared Property IsNewCreditSale() As Boolean
        Get
            Return _IsNewCreditSale
        End Get
        Set(ByVal value As Boolean)
            _IsNewCreditSale = value
        End Set
    End Property
    'added by vipin on 05-12-2017 for PC  sprint 4 
    Private Shared _OrderPackagingScreenScrollTimeInterval As String
    Public Shared Property OrderPackagingScreenScrollTimeInterval() As String
        Get
            Return _OrderPackagingScreenScrollTimeInterval
        End Get
        Set(ByVal value As String)
            _OrderPackagingScreenScrollTimeInterval = value
        End Set
    End Property
    Private Shared _SerialPort As String
    Public Shared Property SerialPort() As String
        Get
            Return _SerialPort
        End Get
        Set(ByVal value As String)
            _SerialPort = value
        End Set
    End Property
    Public Shared _EvasPizzaChanges As Boolean = False
    Public Shared Property EvasPizzaChanges() As Boolean
        Get
            Return _EvasPizzaChanges
        End Get
        Set(ByVal value As Boolean)
            _EvasPizzaChanges = value
        End Set
    End Property
    'add by khusrao adil on 14-06-2017 for natural juhu store
    Public Shared _AuthReqForCreditSalesAdjustmentOnCSA As Boolean = False
    Public Shared Property AuthReqForCreditSalesAdjustmentOnCSA() As Boolean
        Get
            Return _AuthReqForCreditSalesAdjustmentOnCSA
        End Get
        Set(ByVal value As Boolean)
            _AuthReqForCreditSalesAdjustmentOnCSA = value
        End Set
    End Property
    'add by khusrao adil on 18-04-2017 for natural new delopment
    Public Shared _EnableNewTouchCashMemo As Boolean = False
    Public Shared Property EnableNewTouchCashMemo() As Boolean
        Get
            Return _EnableNewTouchCashMemo
        End Get
        Set(ByVal value As Boolean)
            _EnableNewTouchCashMemo = value
        End Set
    End Property
    'add by khusrao adil on 01-12-2017 for jk sprint 32
    Public Shared _jkPrintFormatEnable As Boolean = False
    Public Shared Property JKPrintFormatEnable() As Boolean
        Get
            Return _jkPrintFormatEnable
        End Get
        Set(ByVal value As Boolean)
            _jkPrintFormatEnable = value
        End Set
    End Property
    'added by khusrao adil for host terminals
    Private Shared _hostTerminals As String
    Public Shared Property HostTerminals() As String
        Get
            Return _hostTerminals
        End Get
        Set(ByVal value As String)
            _hostTerminals = value
        End Set
    End Property

    '' added by nikhil for Hari OM
    Private Shared _IsHariOM As Boolean
    Public Shared Property IsHariOM() As Boolean
        Get
            Return _IsHariOM
        End Get
        Set(value As Boolean)
            _IsHariOM = value
        End Set
    End Property
    Public Shared _ISReservationCancelOnTimer As Boolean = False
    Public Shared Property ISReservationCancelOnTimer() As Boolean
        Get
            Return _ISReservationCancelOnTimer
        End Get
        Set(ByVal value As Boolean)
            _ISReservationCancelOnTimer = value
        End Set
    End Property
    Private Shared _ReservationCancelTiming As Double
    Public Shared Property ReservationCancelTiming() As Double
        Get
            Return _ReservationCancelTiming
        End Get
        Set(ByVal value As Double)
            _ReservationCancelTiming = value
        End Set
    End Property
    Private Shared _ReservationTime As Integer
    Public Shared Property ReservationTime() As Integer
        Get
            Return _ReservationTime
        End Get
        Set(ByVal value As Integer)
            _ReservationTime = value
        End Set
    End Property
    Private Shared _ExpiryTime As Integer
    Public Shared Property ExpiryTime() As Integer
        Get
            Return _ExpiryTime
        End Get
        Set(ByVal value As Integer)
            _ExpiryTime = value
        End Set
    End Property
    Private Shared _CustStayTime As Integer
    Public Shared Property CustStayTime() As Integer
        Get
            Return _CustStayTime
        End Get
        Set(ByVal value As Integer)
            _CustStayTime = value
        End Set
    End Property
    Private Shared _ExtendTime As Integer
    Public Shared Property ExtendTime() As Integer
        Get
            Return _ExtendTime
        End Get
        Set(ByVal value As Integer)
            _ExtendTime = value
        End Set
    End Property
    Private Shared _LockTime As Integer
    Public Shared Property LockTime() As Integer
        Get
            Return _LockTime
        End Get
        Set(ByVal value As Integer)
            _LockTime = value
        End Set
    End Property

    Public Shared _EnableImprestCash As Boolean = False
    Public Shared Property EnableImprestCash() As Boolean
        Get
            Return _EnableImprestCash
        End Get
        Set(ByVal value As Boolean)
            _EnableImprestCash = value
        End Set
    End Property
    Private Shared _ImprestCashTill As String
    Public Shared Property ImprestCashTill() As String
        Get
            Return _ImprestCashTill
        End Get
        Set(ByVal value As String)
            _ImprestCashTill = value
        End Set
    End Property
    Private Shared _ImprestCashAmount As Integer
    Public Shared Property ImprestCashAmount() As Integer
        Get
            Return _ImprestCashAmount
        End Get
        Set(ByVal value As Integer)
            _ImprestCashAmount = value
        End Set
    End Property

    ''added by nikhil
    Private Shared _EDCSummaryinput As Boolean = False
    Public Shared Property EDCSummaryinput() As Boolean
        Get
            Return _EDCSummaryinput
        End Get
        Set(ByVal value As Boolean)
            _EDCSummaryinput = value
        End Set
    End Property
    Private Shared _PrintDineInBillOnSettlement As String
    Public Shared Property PrintDineInBillOnSettlement() As String
        Get
            Return _PrintDineInBillOnSettlement
        End Get
        Set(ByVal value As String)
            _PrintDineInBillOnSettlement = value
        End Set
    End Property
    'added by khusrao adil on 01-08-2018 for sharda resturent
    Private Shared _TableNoRequiredInDineInModuleBillPrint As Boolean = False
    Public Shared Property TableNoRequiredInDineInModuleBillPrint() As Boolean
        Get
            Return _TableNoRequiredInDineInModuleBillPrint
        End Get
        Set(ByVal value As Boolean)
            _TableNoRequiredInDineInModuleBillPrint = value
        End Set
    End Property
    'added for innovati
    Private Shared _PayFromInnoviti As Boolean = False
    Public Shared Property PayFromInnoviti() As Boolean
        Get
            Return _PayFromInnoviti
        End Get
        Set(ByVal value As Boolean)
            _PayFromInnoviti = value
        End Set
    End Property

    Private Shared _InnovitiForTerminals As String = False
    Public Shared Property InnovitiForTerminals() As String
        Get
            Return _InnovitiForTerminals
        End Get
        Set(ByVal value As String)
            _InnovitiForTerminals = value
        End Set
    End Property

    Private Shared _IsNewMembership As Boolean = False
    Public Shared Property IsNewMembership() As Boolean
        Get
            Return _IsNewMembership
        End Get
        Set(ByVal value As Boolean)
            _IsNewMembership = value
        End Set
    End Property
    Private Shared _ChangeQtyBasedOnPriceChange As Boolean = False 'vipin 11.07.2018
    Public Shared Property ChangeQtyBasedOnPriceChange() As Boolean
        Get
            Return _ChangeQtyBasedOnPriceChange
        End Get
        Set(ByVal value As Boolean)
            _ChangeQtyBasedOnPriceChange = value
        End Set
    End Property
    Private Shared _OnTooltipDisplayTheKitArticleIngredients As Boolean = False       'vipul
    Public Shared Property OnTooltipDisplayTheKitArticleIngredients() As Boolean
        Get
            Return _OnTooltipDisplayTheKitArticleIngredients
        End Get
        Set(ByVal value As Boolean)
            _OnTooltipDisplayTheKitArticleIngredients = value
        End Set
    End Property
    Private Shared _SplitBillParentNodeCode As String = ""       'vipul
    Public Shared Property SplitBillParentNodeCode() As String
        Get
            Return _SplitBillParentNodeCode
        End Get
        Set(ByVal value As String)
            _SplitBillParentNodeCode = value
        End Set
    End Property
    Private Shared _PaxSelectionOnTable As Boolean = False 'vipin 24.08.2018
    Public Shared Property PaxSelectionOnTable() As Boolean
        Get
            Return _PaxSelectionOnTable
        End Get
        Set(ByVal value As Boolean)
            _PaxSelectionOnTable = value
        End Set
    End Property
    Private Shared _KotWiseKds As Boolean = False 'vipin 24.08.2018
    Public Shared Property KotWiseKds() As Boolean
        Get
            Return _KotWiseKds
        End Get
        Set(ByVal value As Boolean)
            _KotWiseKds = value
        End Set
    End Property
    Private Shared _KDSScreenTimeInterval As String = "" 'vipin 24.08.2018
    Public Shared Property KDSScreenTimeInterval() As String
        Get
            Return _KDSScreenTimeInterval
        End Get
        Set(ByVal value As String)
            _KDSScreenTimeInterval = value
        End Set
    End Property
    Public Shared ReadOnly Property WeightBarcodeSequence() As List(Of SpectrumCommon.Sequence)
        Get
            If _WeightBarcodeSequence IsNot String.Empty OrElse _WeightBarcodeSequence <> "" Then
                Dim List As New List(Of SpectrumCommon.Sequence)

                Dim Sequences = _WeightBarcodeSequence.Split("|")
                For index = 0 To Sequences.Length - 1
                    Dim seq As New SpectrumCommon.Sequence()
                    Dim currentseq = Sequences(index).Split(":")
                    If currentseq.Length > 0 Then
                        seq.Element = currentseq(0)
                        seq.Sequence = currentseq(1)
                        Select Case seq.Element
                            Case SpectrumCommon.WeighingScaleBarcodeSections.EAN.ToString()
                                seq.SeqLength = 0
                            Case SpectrumCommon.WeighingScaleBarcodeSections.Rate.ToString()
                                seq.SeqLength = WeightBarcodeRateLength
                            Case SpectrumCommon.WeighingScaleBarcodeSections.Qty.ToString()
                                seq.SeqLength = WeightBarcodeLength
                            Case SpectrumCommon.WeighingScaleBarcodeSections.Prefix.ToString()
                                seq.SeqLength = WeightBarcodePrefixDigits
                        End Select

                    End If
                    List.Add(seq)
                Next
                Return List.OrderBy(Function(w) w.Sequence).ToList()
            Else
                Return New List(Of SpectrumCommon.Sequence)
            End If
        End Get
    End Property

    Private Shared _GiftVoucherReturnAllowed As Boolean
    Public Shared Property GiftVoucherReturnAllowed() As Boolean
        Get
            Return _GiftVoucherReturnAllowed
        End Get
        Set(ByVal value As Boolean)
            _GiftVoucherReturnAllowed = value
        End Set
    End Property

    Public Shared ReadOnly Property MaxQuantity() As Integer
        Get
            Return 999999999
        End Get
    End Property
    Public Shared Property isMAPbasedCost() As Boolean
        Get
            Return _isMAPbasedCost
        End Get

        Set(ByVal value As Boolean)
            _isMAPbasedCost = value
        End Set
    End Property

    Public Shared Property DayCloseOtherScreens() As Boolean
        Get
            Return _DayCloseOtherScreens
        End Get

        Set(ByVal value As Boolean)
            _DayCloseOtherScreens = value
        End Set
    End Property

    Public Shared _JKDayCloseReport As Boolean
    Public Shared Property JKDayCloseReport() As Boolean
        Get
            Return _JKDayCloseReport
        End Get
        Set(ByVal value As Boolean)
            _JKDayCloseReport = value
        End Set
    End Property
    Private Shared _DayCloseReportFormat As String
    Public Shared Property DayCloseReportFormat() As String
        Get
            Return _DayCloseReportFormat
        End Get
        Set(ByVal value As String)
            _DayCloseReportFormat = value
        End Set
    End Property
    Private Shared _PromotionBasedOn As String
    Public Shared Property PromotionBasedOn() As String
        Get
            Return _PromotionBasedOn
        End Get
        Set(ByVal value As String)
            _PromotionBasedOn = value
        End Set
    End Property

    Private Shared _SeperateSaleTerminalId As String
    Public Shared Property SeperateSaleTerminalId() As String
        Get
            Return _SeperateSaleTerminalId
        End Get
        Set(ByVal value As String)
            _SeperateSaleTerminalId = value
        End Set
    End Property
    Public Shared Property BankTotalCheck() As Boolean
        Get
            Return _BankTotalCheck
        End Get

        Set(ByVal value As Boolean)
            _BankTotalCheck = value
        End Set
    End Property
    Shared _AutoUpdateonDayClose As Boolean = False
    Public Shared Property AutoUpdateonDayClose() As Boolean
        Get
            Return _AutoUpdateonDayClose
        End Get
        Set(ByVal value As Boolean)
            _AutoUpdateonDayClose = value
        End Set
    End Property
    Shared _AutoUpdateOnLogin As Boolean = False
    Public Shared Property AutoUpdateOnLogin() As Boolean
        Get
            Return _AutoUpdateOnLogin
        End Get
        Set(ByVal value As Boolean)
            _AutoUpdateOnLogin = value
        End Set
    End Property
    Shared _AutoPopulateQtyForAllDayCloseScreens As Boolean = False
    Public Shared Property AutoPopulateQtyForAllDayCloseScreens() As Boolean
        Get
            Return _AutoPopulateQtyForAllDayCloseScreens
        End Get
        Set(ByVal value As Boolean)
            _AutoPopulateQtyForAllDayCloseScreens = value
        End Set
    End Property
    Private Shared _IsSavoy As Boolean
    Public Shared Property IsSavoy() As Boolean
        Get
            Return _IsSavoy
        End Get
        Set(ByVal value As Boolean)
            _IsSavoy = value
        End Set
    End Property
    Private Shared _DefaultModuleAfterLogin As String 'vipin 15.06.2018
    Public Shared Property DefaultModuleAfterLogin() As String
        Get
            Return _DefaultModuleAfterLogin
        End Get
        Set(ByVal value As String)
            _DefaultModuleAfterLogin = value
        End Set
    End Property
    Private Shared _CustomerClassSelection As Boolean
    Public Shared Property CustomerClassSelection() As Boolean
        Get
            Return _CustomerClassSelection
        End Get
        Set(ByVal value As Boolean)
            _CustomerClassSelection = value
        End Set
    End Property
    Private Shared _ThemeSelect As String
    Public Shared Property ThemeSelect() As String
        Get
            Return _ThemeSelect
        End Get
        Set(ByVal value As String)
            _ThemeSelect = value
        End Set
    End Property

    'added by khusrao adil on 12-10-2017 for jk sprint 30
    Private Shared _SynchBatFile As String
    Public Shared Property SynchBatFile() As String
        Get
            Return _SynchBatFile
        End Get
        Set(ByVal value As String)
            _SynchBatFile = value
        End Set
    End Property
    Private Shared _DineInButtonWithText As Boolean
    Public Shared Property DineInButtonWithText() As Boolean
        Get
            Return _DineInButtonWithText
        End Get
        Set(ByVal value As Boolean)
            _DineInButtonWithText = value
        End Set
    End Property
    Private Shared _AllowOnScreenKeyBoard As Boolean = False
    Public Shared Property AllowOnScreenKeyBoard() As Boolean
        Get
            Return _AllowOnScreenKeyBoard
        End Get
        Set(ByVal value As Boolean)
            _AllowOnScreenKeyBoard = value
        End Set
    End Property
    Private Shared _DoneSystemApplicable As Boolean = False
    Public Shared Property DoneSystemApplicable() As Boolean
        Get
            Return _DoneSystemApplicable
        End Get
        Set(ByVal value As Boolean)
            _DoneSystemApplicable = value
        End Set
    End Property
    Private Shared _ExternalOrdersTillNo As String
    Public Shared Property ExternalOrdersTillNo() As String
        Get
            Return _ExternalOrdersTillNo
        End Get
        Set(ByVal value As String)
            _ExternalOrdersTillNo = value
        End Set
    End Property
    'Public Shared ReadOnly Property BLRoundOffRequired() As Boolean
    '    Get
    '        Return _BLRoundOffRequired
    '    End Get
    'End Property
    'Public Shared ReadOnly Property RoundOffRequired() As Boolean
    '    Get
    '        Return _RoundOffRequired
    '    End Get
    'End Property
    '----For DayCloseSMS
    Private Shared _SendDayCloseSMS As Boolean
    Public Shared Property SendDayCloseSMS() As Boolean
        Get
            Return _SendDayCloseSMS
        End Get
        Set(ByVal value As Boolean)
            _SendDayCloseSMS = value
        End Set
    End Property
    Private Shared _DayCloseRecipients As String
    Public Shared Property DayCloseRecipients() As String
        Get
            Return _DayCloseRecipients
        End Get
        Set(ByVal value As String)
            _DayCloseRecipients = value
        End Set
    End Property
    Private Shared _DayCloseSMSFormat As Integer
    Public Shared Property DayCloseSMSFormat() As Integer
        Get
            Return _DayCloseSMSFormat
        End Get
        Set(ByVal value As Integer)
            _DayCloseSMSFormat = value
        End Set
    End Property
    Public Shared Property IsCreditCardRefundAllowed() As Boolean
        Get
            Return _IsCreditCardRefundAllowed
        End Get
        Set(ByVal value As Boolean)
            _IsCreditCardRefundAllowed = value
        End Set
    End Property

    Public Shared Property IsChequeRefundAllowed() As Boolean
        Get
            Return _IsChequeRefundAllowed
        End Get
        Set(ByVal value As Boolean)
            _IsChequeRefundAllowed = value
        End Set
    End Property

    Public Shared Property ChequeInfomation() As Boolean
        Get
            Return _ChequeInfo
        End Get
        Set(ByVal value As Boolean)
            _ChequeInfo = value
        End Set
    End Property
    '------------ for hope india
    Private Shared _EnableHealthCare As Boolean
    Public Shared Property EnableHealthCare() As Boolean
        Get
            Return _EnableHealthCare
        End Get
        Set(ByVal value As Boolean)
            _EnableHealthCare = value
        End Set
    End Property

    Public Shared ReadOnly Property HoldBillPrintBarcode() As Boolean
        Get
            Return _HoldBillBarcode
        End Get
    End Property

    'Public Shared Property CVBaseArticle() As String
    '    Get
    '        Return _CVArticle
    '    End Get
    '    Set(ByVal value As String)
    '        _CVArticle = value
    '    End Set
    'End Property
    Public Shared Property GVBaseArticle() As String
        Get
            Return _GVArticle
        End Get
        Set(ByVal value As String)
            _GVArticle = value
        End Set
    End Property
    Public Shared Property ClpBaseArticle() As String
        Get
            Return _CLPArticle
        End Get
        Set(ByVal value As String)
            _CLPArticle = value
        End Set
    End Property
    Public Shared Property TillOpenDone() As Boolean
        Get
            Return _TillOpen
        End Get
        Set(ByVal value As Boolean)
            _TillOpen = value
        End Set
    End Property
    Public Shared Property TillOperationRequired() As Boolean
        Get
            Return _TillOperation
        End Get
        Set(ByVal value As Boolean)
            _TillOperation = value
        End Set
    End Property

    Private Shared _IsManualCLPCustomerSearch As Boolean
    Shared Property IsManualCLPCustomerSearch() As Boolean
        Get
            Return _IsManualCLPCustomerSearch
        End Get
        Set(ByVal value As Boolean)
            _IsManualCLPCustomerSearch = value
        End Set
    End Property
    Public Shared Property CheckExpiryMonth() As Int32
        Get
            Return _CheckExpiryMonth
        End Get
        Set(ByVal value As Int32)
            _CheckExpiryMonth = value
        End Set
    End Property
    Public Shared ReadOnly Property TillOpenNClose() As Boolean
        Get
            Return _TillOpenNClose
        End Get
    End Property
    Public Shared ReadOnly Property CheckTillCloseAmount() As Boolean
        Get
            Return _CheckTillClose
        End Get
    End Property
    Public Shared ReadOnly Property UserAmountPrint() As Boolean
        Get
            Return _UserAmountPrint
        End Get
    End Property
    Public Shared ReadOnly Property ReasonRequired() As Boolean
        Get
            Return _ReturnReason
        End Get
    End Property
    Public Shared ReadOnly Property ShowDiscountAmount() As Boolean
        Get
            Return _ShowDiscAmt
        End Get
    End Property
    'Public Shared ReadOnly Property BillRoundAt() As Int32
    '    Get
    '        Return _RoundAt
    '    End Get
    'End Property
    Public Shared Property OtherChargesEditable() As Boolean
        Get
            Return _OtherCharges
        End Get
        Set(ByVal value As Boolean)
            _OtherCharges = value
        End Set
    End Property
    Public Shared Property ExclusiveTaxAfterDisc() As Boolean
        Get
            Return _TaxAfterDisc
        End Get
        Set(ByVal value As Boolean)
            _TaxAfterDisc = value
        End Set
    End Property
    'Public Shared Property SOStorageLocation() As String
    '    Get
    '        Return _SOStorageLoc
    '    End Get
    '    Set(ByVal value As String)
    '        _SOStorageLoc = value
    '    End Set
    'End Property
    'Public Shared Property IsArticleTaxAllowed() As Boolean
    '    Get
    '        Return _IsArticleTaxAllowed
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _IsArticleTaxAllowed = value
    '    End Set
    'End Property
    'Public Shared Property BLStorageLocation() As String
    '    Get
    '        Return _BLStorageLoc
    '    End Get
    '    Set(ByVal value As String)
    '        _BLStorageLoc = value
    '    End Set
    'End Property

    'Public Shared Property CashMemoStorageLocation() As String
    '    Get
    '        Return _CMStorageLoc
    '    End Get
    '    Set(ByVal value As String)
    '        _CMStorageLoc = value
    '    End Set
    'End Property
    Public Shared Property CLPRegistrationAmt() As Double
        Get
            Return _CLPRegistrationAmount
        End Get
        Set(ByVal value As Double)
            _CLPRegistrationAmount = value
        End Set
    End Property

    'Public Shared Property OtherPrinter() As String
    '    Get
    '        Return _OtherPrinter
    '    End Get
    '    Set(ByVal value As String)
    '        _OtherPrinter = value
    '    End Set
    'End Property
    Public Shared Property CashMemoPrinter() As String
        Get
            Return _CashMemoPrinter
        End Get
        Set(ByVal value As String)
            _CashMemoPrinter = value
        End Set
    End Property
    Public Shared Property PrintPreivewReq() As Boolean
        Get
            Return _PrintPreview
        End Get
        Set(ByVal value As Boolean)
            _PrintPreview = value
        End Set
    End Property

    Public Shared Property TillClosePrintPreivewReq() As Boolean
        Get
            Return _TillClosePrintPreivewReq
        End Get
        Set(ByVal value As Boolean)
            _TillClosePrintPreivewReq = value
        End Set
    End Property

    'Public Shared Property FooterPrinting() As Boolean
    '    Get
    '        Return _FooterPrint
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _FooterPrint = value
    '    End Set
    'End Property
    'Public Shared Property HeaderPrinting() As Boolean
    '    Get
    '        Return _HeaderPrint
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _HeaderPrint = value
    '    End Set
    'End Property

    Private Shared _BLPrintPreviewAllowed As Boolean
    Public Shared Property BLPrintPreviewAllowed() As Boolean
        Get
            Return _BLPrintPreviewAllowed
        End Get
        Set(ByVal value As Boolean)
            _BLPrintPreviewAllowed = value
        End Set
    End Property

    Private Shared _SOPrintPreviewAllowed As Boolean
    Public Shared Property SOPrintPreviewAllowed() As Boolean
        Get
            Return _SOPrintPreviewAllowed
        End Get
        Set(ByVal value As Boolean)
            _SOPrintPreviewAllowed = value
        End Set
    End Property

    Public Shared Property GVsaleAllowed() As Boolean
        Get
            Return _GVSale
        End Get
        Set(ByVal value As Boolean)
            _GVSale = value
        End Set
    End Property
    'Public Shared Property CLPPointSaleAllowed() As Boolean
    '    Get
    '        Return _CLPPointSale
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _CLPPointSale = value
    '    End Set
    'End Property
    Public Shared Property IssueCreditVoucher() As Boolean
        Get
            Return _IssCrVoucher
        End Get
        Set(ByVal value As Boolean)
            _IssCrVoucher = value
        End Set
    End Property
    'Public Shared Property ZeroTaxs() As Boolean
    '    Get
    '        Return _ZeroTax
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _ZeroTax = value
    '    End Set
    'End Property
    'Public Shared Property PrintingType() As String
    '    Get
    '        Return _PrintingType
    '    End Get
    '    Set(ByVal value As String)
    '        _PrintingType = value
    '    End Set
    'End Property

    Public Shared Property CheckTillOpenAmount() As String
        Get
            Return _CheckTillOpenAmount
        End Get
        Set(ByVal value As String)
            _CheckTillOpenAmount = value
        End Set
    End Property

    Public Shared Property InclusiveTaxDisplay() As String
        Get
            Return _InclusiveTaxDisplay
        End Get
        Set(ByVal value As String)
            _InclusiveTaxDisplay = value
        End Set
    End Property

    'Public Shared Property CompanyName() As String
    '    Get
    '        Return _CompanyName
    '    End Get
    '    Set(ByVal value As String)
    '        _CompanyName = value
    '    End Set
    'End Property

    Private Shared _DayCloseReportPath As String
    Public Shared Property DayCloseReportPath() As String
        Get
            Return _DayCloseReportPath
        End Get
        Set(ByVal value As String)
            _DayCloseReportPath = value
        End Set
    End Property
    Private Shared _ShiftManagement As Boolean = False
    Public Shared Property ShiftManagement() As Boolean
        Get
            Return _ShiftManagement
        End Get
        Set(ByVal value As Boolean)
            _ShiftManagement = value
        End Set
    End Property
    Private Shared _ClientName As String
    Public Shared Property ClientName() As String
        Get
            Return _ClientName
        End Get
        Set(ByVal value As String)
            _ClientName = value
        End Set
    End Property
    Private Shared _ClientForMail As String
    Public Shared Property ClientForMail() As String
        Get
            Return _ClientForMail
        End Get
        Set(ByVal value As String)
            _ClientForMail = value
        End Set
    End Property
    Private Shared _IsMailSend As Boolean = False
    Public Shared Property IsMailSend() As Boolean
        Get
            Return _IsMailSend
        End Get
        Set(ByVal value As Boolean)
            _IsMailSend = value
        End Set
    End Property

    Private Shared _DayCloseEmailNotifiaction As String
    Public Shared Property DayCloseEmailNotifiaction() As String
        Get
            Return _DayCloseEmailNotifiaction
        End Get
        Set(ByVal value As String)
            _DayCloseEmailNotifiaction = value
        End Set
    End Property
    Private Shared _DAYCLOSEEMAILAMOUNTGOINGTOBANK As String
    Public Shared Property DAYCLOSEEMAILAMOUNTGOINGTOBANK() As String
        Get
            Return _DAYCLOSEEMAILAMOUNTGOINGTOBANK
        End Get
        Set(ByVal value As String)
            _DAYCLOSEEMAILAMOUNTGOINGTOBANK = value
        End Set
    End Property
    Public Shared Property DecimalPlaces() As Integer
        Get
            Return _DecimalPlaces
        End Get
        Set(ByVal value As Integer)
            _DecimalPlaces = value
        End Set
    End Property

    Public Shared Property HoldBillPrint() As Boolean
        Get
            Return _HoldBill
        End Get
        Set(ByVal value As Boolean)
            _HoldBill = value
        End Set
    End Property

    Private Shared _WebserviceStockURL As String
    Public Shared Property WebserviceStockURL() As String
        Get
            Return _WebserviceStockURL
        End Get
        Set(ByVal value As String)
            _WebserviceStockURL = value
        End Set
    End Property

    Private Shared _SMSUrl As String
    Public Shared Property SMSUrl() As String
        Get
            Return _SMSUrl
        End Get
        Set(ByVal value As String)
            _SMSUrl = value
        End Set
    End Property
    Private Shared _SMSUrlParameters As String
    Public Shared Property SMSUrlParameters() As String
        Get
            Return _SMSUrlParameters
        End Get
        Set(ByVal value As String)
            _SMSUrlParameters = value
        End Set
    End Property

    Public Shared Property ShowPromotionalMessage() As Boolean
        Get
            Return _PromMessase
        End Get
        Set(ByVal value As Boolean)
            _PromMessase = value
        End Set
    End Property
    'Public Shared Property PrintingTaxInfo() As Boolean
    '    Get
    '        Return _PrintingTax
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _PrintingTax = value
    '    End Set
    'End Property
    Public Shared Property CreditCardInfo() As Boolean
        Get
            Return _creditCard
        End Get
        Set(ByVal value As Boolean)
            _creditCard = value
        End Set
    End Property
    Public Shared Property CashRefund() As Boolean
        Get
            Return _CashRefund
        End Get
        Set(ByVal value As Boolean)
            _CashRefund = value
        End Set
    End Property

    Public Shared Property TaxApplicable() As Boolean
        Get
            Return _TaxAplicable
        End Get
        Set(ByVal value As Boolean)
            _TaxAplicable = value
        End Set
    End Property
    Public Shared Property WithoutBillAllowed() As Boolean
        Get
            Return _WithoutBill
        End Get
        Set(ByVal value As Boolean)
            _WithoutBill = value
        End Set
    End Property
    Public Shared Property CLPRegistration() As Boolean
        Get
            Return _CLPRegistration
        End Get
        Set(ByVal value As Boolean)
            _CLPRegistration = value
        End Set
    End Property
    Public Shared Property CLPIntimation() As Boolean
        Get
            Return _CLPIntimation
        End Get
        Set(ByVal value As Boolean)
            _CLPIntimation = value
        End Set
    End Property
    Public Shared Property AdvanceSalesAllowed() As Boolean
        Get
            Return _AdvanceSalesAllowed
        End Get
        Set(ByVal value As Boolean)
            _AdvanceSalesAllowed = value
        End Set
    End Property
    Public Shared Property PriceChageAllowed() As Boolean
        Get
            Return _PriceChange
        End Get
        Set(ByVal value As Boolean)
            _PriceChange = value
        End Set
    End Property
    'Public Shared Property NagativeBillingAllowed() As Boolean
    '    Get
    '        Return _NagativeBillAllowed
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _NagativeBillAllowed = value
    '    End Set
    'End Property
    Public Shared Property SalesPersonApplicable() As Boolean
        Get
            Return _SalesPersonReq
        End Get
        Set(ByVal value As Boolean)
            _SalesPersonReq = value
        End Set
    End Property
    Public Shared Property TaxDetailsRequired() As Boolean
        Get
            Return _TaxDetailsReq
        End Get
        Set(ByVal value As Boolean)
            _TaxDetailsReq = value
        End Set
    End Property
    Public Shared Property ManualPromotionAllowed() As Boolean
        Get
            Return _ManualPromo
        End Get
        Set(ByVal value As Boolean)
            _ManualPromo = value
        End Set
    End Property
    Public Shared Property PrintItemFullName() As Boolean
        Get
            Return _ItemFullName
        End Get
        Set(ByVal value As Boolean)
            _ItemFullName = value
        End Set
    End Property
    'Public Shared Property PrintDiscountPercentage() As Boolean
    '    Get
    '        Return _PrintDiscPerc
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _PrintDiscPerc = value
    '    End Set
    'End Property
    Public Shared Property VoucherValidatedDays() As Int32
        Get
            Return _VoucherDays
        End Get
        Set(ByVal value As Int32)
            _VoucherDays = value
        End Set
    End Property
    Public Shared Property VoucherText() As String
        Get
            Return _voucherText
        End Get
        Set(ByVal value As String)
            _VoucherDays = value
        End Set
    End Property

    Private Shared _BLIsSalesPersonApplicable As Boolean
    Public Shared Property BLIsSalesPersonApplicable() As Boolean
        Get
            Return _BLIsSalesPersonApplicable
        End Get
        Set(ByVal value As Boolean)
            _BLIsSalesPersonApplicable = value
        End Set
    End Property


    'Private Shared _BLNegativeInventoryApplicable As Boolean
    'Public Shared Property BLNegativeInventoryApplicable() As Boolean
    '    Get
    '        Return _BLNegativeInventoryApplicable
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _BLNegativeInventoryApplicable = value
    '    End Set
    'End Property

    'Private Shared _BLIsCLPApplicable As Boolean
    'Public Shared Property BLIsCLPApplicable() As Boolean
    '    Get
    '        Return _BLIsCLPApplicable
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _BLIsCLPApplicable = value
    '    End Set
    'End Property

    'Private Shared _BLCreditVoucherText As String
    'Public Shared Property BLCreditVoucherText() As String
    '    Get
    '        Return _BLCreditVoucherText
    '    End Get
    '    Set(ByVal value As String)
    '        _BLCreditVoucherText = value
    '    End Set
    'End Property

    'Private Shared _BLCreditVoucherExpiryInDays As Integer
    'Public Shared Property BLCreditVoucherExpiryInDays() As Integer
    '    Get
    '        Return _BLCreditVoucherExpiryInDays
    '    End Get
    '    Set(ByVal value As Integer)
    '        _BLCreditVoucherExpiryInDays = value
    '    End Set
    'End Property

    'Private Shared _BLZeroTaxAllowed As Boolean
    'Public Shared Property BLArticleZeroTaxAllowed() As Boolean
    '    Get
    '        Return _BLZeroTaxAllowed
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _BLZeroTaxAllowed = value
    '    End Set
    'End Property

    'Private Shared _BLBillRoundOff As Integer
    'Public Shared Property BLBillRoundOff() As Integer
    '    Get
    '        Return _BLBillRoundOff
    '    End Get
    '    Set(ByVal value As Integer)
    '        _BLBillRoundOff = value
    '    End Set
    'End Property

    Private Shared _BLGVSale As Boolean
    Public Shared Property BLGVSale() As Boolean
        Get
            Return _BLGVSale
        End Get
        Set(ByVal value As Boolean)
            _BLGVSale = value
        End Set
    End Property
    Shared _BLCloseDiscountPercentage As Integer
    Public Shared Property BLCloseDiscountPercentage() As Integer
        Get
            Return _BLCloseDiscountPercentage
        End Get
        Set(ByVal value As Integer)
            _BLCloseDiscountPercentage = value
        End Set
    End Property
    Private Shared _ReportTerminalId As String = ""
    Public Shared Property ReportTerminalId() As String
        Get
            Return _ReportTerminalId
        End Get
        Set(ByVal value As String)
            _ReportTerminalId = value
        End Set
    End Property

    Shared _BLCLPIntimation As Boolean
    Public Shared Property BLCLPIntimation() As Boolean
        Get
            Return _BLCLPIntimation
        End Get
        Set(ByVal value As Boolean)
            _BLCLPIntimation = value
        End Set
    End Property



    Private Shared _BLIsCheckDeliveryDate As Boolean

    Public Shared Property BLIsCheckDeliveryDate() As Boolean
        Get
            Return _BLIsCheckDeliveryDate
        End Get
        Set(ByVal value As Boolean)
            _BLIsCheckDeliveryDate = value
        End Set
    End Property

    'Private Shared _BLPromtionID As String
    'Public Shared Property BLPromtionID() As String
    '    Get
    '        Return _BLPromtionID
    '    End Get
    '    Set(ByVal value As String)
    '        _BLPromtionID = value
    '    End Set
    'End Property

    Private Shared _IsPettyCashOnSameTerminal As Boolean
    Public Shared Property IsPettyCashOnSameTerminal() As Boolean
        Get
            Return _IsPettyCashOnSameTerminal
        End Get
        Set(ByVal value As Boolean)
            _IsPettyCashOnSameTerminal = value
        End Set
    End Property

    Private Shared _PettyCashTerminalId As String
    Public Shared Property PettyCashTerminalId() As String
        Get
            Return _PettyCashTerminalId
        End Get
        Set(ByVal value As String)
            _PettyCashTerminalId = value
        End Set
    End Property

    Private Shared _IsPettyCashApplicable As Boolean
    Public Shared Property IsPettyCashApplicable() As Boolean
        Get
            Return _IsPettyCashApplicable
        End Get
        Set(ByVal value As Boolean)
            _IsPettyCashApplicable = value
        End Set
    End Property

    Private Shared _AddSalesToPettyCash As Boolean
    Public Shared Property AddSalesToPettyCash() As Boolean
        Get
            Return _AddSalesToPettyCash
        End Get
        Set(ByVal value As Boolean)
            _AddSalesToPettyCash = value
        End Set
    End Property

    Private Shared _AskForMoreCustomerInfo As Boolean
    Public Shared Property AskForMoreCustomerInfo() As Boolean
        Get
            Return _AskForMoreCustomerInfo
        End Get
        Set(ByVal value As Boolean)
            _AskForMoreCustomerInfo = value
        End Set
    End Property

    Private Shared _IsPrviewRequiredForVchr As Boolean
    Public Shared Property IsPrviewRequiredForVchr() As Boolean
        Get
            Return _IsPrviewRequiredForVchr
        End Get
        Set(ByVal value As Boolean)
            _IsPrviewRequiredForVchr = value
        End Set
    End Property

    Private Shared _IsCustNameReadonly As Boolean
    Public Shared Property IsCustNameReadonly() As Boolean
        Get
            Return _IsCustNameReadonly
        End Get
        Set(ByVal value As Boolean)
            _IsCustNameReadonly = value
        End Set
    End Property

    Private Shared _MaxNextDayFloatAmount As Double
    Public Shared Property MaxNextDayFloatAmount() As Double
        Get
            Return _MaxNextDayFloatAmount
        End Get
        Set(ByVal value As Double)
            _MaxNextDayFloatAmount = value
        End Set
    End Property

    Private Shared _AutoRunPathAfterDayClose As String
    Public Shared Property AutoRunPathAfterDayClose() As String
        Get
            Return _AutoRunPathAfterDayClose
        End Get
        Set(ByVal value As String)
            _AutoRunPathAfterDayClose = value
        End Set
    End Property

    Private Shared _CSTTaxCode As String
    Public Shared Property CSTTaxCode() As String
        Get
            Return _CSTTaxCode
        End Get
        Set(ByVal value As String)
            _CSTTaxCode = value
        End Set
    End Property


    Private Shared _IsPreviewReqForQuotation As Boolean
    Public Shared Property IsPreviewReqForQuotation() As Boolean
        Get
            Return _IsPreviewReqForQuotation
        End Get
        Set(ByVal value As Boolean)
            _IsPreviewReqForQuotation = value
        End Set
    End Property

    Private Shared _CLPOnPriceChange As Boolean
    Public Shared Property CLPOnPriceChange() As Boolean
        Get
            Return _CLPOnPriceChange
        End Get
        Set(ByVal value As Boolean)
            _CLPOnPriceChange = value
        End Set
    End Property

    '* CLP Required Properties Start
    Public Shared Property CLP_Applicable_Edit() As Boolean
        Get
            Return _CLP_Applicable_Edit
        End Get
        Set(ByVal value As Boolean)
            _CLP_Applicable_Edit = value
        End Set
    End Property
    Public Shared Property CLP_Point_On_redeemption() As Boolean
        Get
            Return _CLP_Point_On_redeemption
        End Get
        Set(ByVal value As Boolean)
            _CLP_Point_On_redeemption = value
        End Set
    End Property
    '* CLP Required Properties End

    Public Shared _printMarginTop As Integer
    Public Shared Property PrintMarginTop() As Integer
        Get
            Return _printMarginTop
        End Get
        Set(ByVal value As Integer)
            _printMarginTop = value
        End Set
    End Property

    Public Shared _printMarginBottom As Integer
    Public Shared Property PrintMarginBottom() As Integer
        Get
            Return _printMarginBottom
        End Get
        Set(ByVal value As Integer)
            _printMarginBottom = value
        End Set
    End Property

    'Rakesh:09-July-2013-->Start: Template based cashmemo bill printing parameter
    Private Shared _TemplatePrintingAllowed As Boolean
    Public Shared Property TemplatePrintingAllowed() As Boolean
        Get
            Return _TemplatePrintingAllowed
        End Get
        Set(ByVal value As Boolean)
            _TemplatePrintingAllowed = value
        End Set
    End Property

    'Rakesh:09-July-2013-->End: Template based cashmemo bill printing parameter

    Private Shared _IsDisplayTotalSaving As Boolean
    Public Shared Property IsDisplayTotalSaving() As Boolean
        Get
            Return _IsDisplayTotalSaving
        End Get
        Set(ByVal value As Boolean)
            _IsDisplayTotalSaving = value
        End Set
    End Property

    Private Shared _PenaltyPercentageInClose As Decimal
    Public Shared Property PenaltyPercentageInClose() As Decimal
        Get
            Return _PenaltyPercentageInClose
        End Get
        Set(ByVal value As Decimal)
            _PenaltyPercentageInClose = value
        End Set
    End Property

    Private Shared _PenaltyPercentageInCancel As Decimal
    Public Shared Property PenaltyPercentageInCancel() As Decimal
        Get
            Return _PenaltyPercentageInCancel
        End Get
        Set(ByVal value As Decimal)
            _PenaltyPercentageInCancel = value
        End Set
    End Property
    Private Shared _PackageFiedlsAllowed As Boolean
    Public Shared Property PackageFiedlsAllowed() As Boolean
        Get
            Return _PackageFiedlsAllowed
        End Get
        Set(ByVal value As Boolean)
            _PackageFiedlsAllowed = value
        End Set
    End Property
    Private Shared _IsNewSalesOrder As Boolean
    Public Shared Property IsNewSalesOrder() As Boolean
        Get
            Return _IsNewSalesOrder
        End Get
        Set(ByVal value As Boolean)
            _IsNewSalesOrder = value
        End Set
    End Property
    'added by khusrao adil on 06-10-2017 for  product sepcific changes
    Private Shared _EnablewildSearch As Boolean
    Public Shared Property EnablewildSearch() As Boolean
        Get
            Return _EnablewildSearch
        End Get
        Set(ByVal value As Boolean)
            _EnablewildSearch = value
        End Set
    End Property
    Private Shared _NoofdaysAfterPenaltyApplicable As Int16
    Public Shared Property NoofdaysAfterPenaltyApplicable() As Int16
        Get
            Return _NoofdaysAfterPenaltyApplicable
        End Get
        Set(ByVal value As Int16)
            _NoofdaysAfterPenaltyApplicable = value
        End Set
    End Property


    Private Shared _KOTPrintRequired As Boolean
    Public Shared Property KOTPrintRequired() As Boolean
        Get
            Return _KOTPrintRequired
        End Get
        Set(ByVal value As Boolean)
            _KOTPrintRequired = value
        End Set
    End Property

    Private Shared _KotFontBold As Boolean
    Public Shared Property IsKotFontBold() As Boolean
        Get
            Return _KotFontBold
        End Get
        Set(ByVal value As Boolean)
            _KotFontBold = value
        End Set
    End Property

    Private Shared _KotFontLarge As Boolean
    Public Shared Property IsKotFontLarge() As Boolean
        Get
            Return _KotFontLarge
        End Get
        Set(ByVal value As Boolean)
            _KotFontLarge = value
        End Set
    End Property

    Private Shared _KOTPrintFormatNo As Integer
    Public Shared Property KOTPrintFormatNo() As Integer
        Get
            Return _KOTPrintFormatNo
        End Get
        Set(ByVal value As Integer)
            _KOTPrintFormatNo = value
        End Set
    End Property

    Private Shared _CashMemoResetonDayClose As Boolean
    Public Shared Property CashMemoResetonDayClose() As Boolean
        Get
            Return _CashMemoResetonDayClose
        End Get
        Set(ByVal value As Boolean)
            _CashMemoResetonDayClose = value
        End Set
    End Property

    Private Shared _AllowMobileNoEditable As Boolean
    Public Shared Property AllowMobileNoEditable() As Boolean
        Get
            Return _AllowMobileNoEditable
        End Get
        Set(ByVal value As Boolean)
            _AllowMobileNoEditable = value
        End Set
    End Property


    Private Shared _AllowBillingOnlyAfterSelectionOfSalesType As Boolean
    Public Shared Property AllowBillingOnlyAfterSelectionOfSalesType() As Boolean
        Get
            Return _AllowBillingOnlyAfterSelectionOfSalesType
        End Get
        Set(ByVal value As Boolean)
            _AllowBillingOnlyAfterSelectionOfSalesType = value
        End Set
    End Property

    Private Shared _TokenNoRequiredInKOT As Boolean
    Public Shared Property TokenNoRequiredInKOT() As Boolean
        Get
            Return _TokenNoRequiredInKOT
        End Get
        Set(ByVal value As Boolean)
            _TokenNoRequiredInKOT = value
        End Set
    End Property

    Private Shared _allowBillPrintForCreditSales As Boolean
    Public Shared Property AllowBillPrintForCreditSales() As Boolean
        Get
            Return _allowBillPrintForCreditSales
        End Get
        Set(ByVal value As Boolean)
            _allowBillPrintForCreditSales = value
        End Set
    End Property


    Private Shared _CustomerNameRequiredInKOT As Boolean
    Public Shared Property CustomerNameRequiredInKOT() As Boolean
        Get
            Return _CustomerNameRequiredInKOT
        End Get
        Set(ByVal value As Boolean)
            _CustomerNameRequiredInKOT = value
        End Set
    End Property
    Private Shared _DayCloseProceedOnTillClose As Boolean
    Public Shared Property DayCloseProceedOnTillClose() As Boolean
        Get
            Return _DayCloseProceedOnTillClose
        End Get
        Set(ByVal value As Boolean)
            _DayCloseProceedOnTillClose = value
        End Set
    End Property
    Private Shared _AllowCreditSaleWriteOff As String
    Public Shared Property AllowCreditSaleWriteOff() As String
        Get
            Return _AllowCreditSaleWriteOff
        End Get
        Set(ByVal value As String)
            _AllowCreditSaleWriteOff = value
        End Set
    End Property

    Private Shared _UpdateBillTime As Boolean
    Public Shared Property UpdateBillTime() As Boolean
        Get
            Return _UpdateBillTime
        End Get
        Set(ByVal value As Boolean)
            _UpdateBillTime = value
        End Set
    End Property
    Private Shared _IsCustAddWild As Boolean
    Public Shared Property IsCustAddWild() As Boolean
        Get
            Return _IsCustAddWild
        End Get
        Set(ByVal value As Boolean)
            _IsCustAddWild = value
        End Set
    End Property
    Private Shared _IsMembership As Boolean
    Public Shared Property IsMembership() As Boolean
        Get
            Return _IsMembership
        End Get
        Set(ByVal value As Boolean)
            _IsMembership = value
        End Set
    End Property
    Private Shared _IsHostManagementEnable As Boolean
    Public Shared Property IsHostManagementEnable() As Boolean
        Get
            Return _IsHostManagementEnable
        End Get
        Set(ByVal value As Boolean)
            _IsHostManagementEnable = value
        End Set
    End Property

    'code added by vipul add new rate column in print format 6
    Public Shared _IsRatevisibleInPrintFormat6 As Boolean
    Public Shared Property IsRatevisibleInPrintFormat6() As Boolean
        Get
            Return _IsRatevisibleInPrintFormat6
        End Get
        Set(ByVal value As Boolean)
            _IsRatevisibleInPrintFormat6 = value
        End Set
    End Property
    'code added by irfan on 9/8/2017 agianst tender visiblity and _IsHsnAndTaxVisibleInPrintFormat6
    Public Shared _IsTendersVisibleInPrintFormat7 As Boolean = False
    Public Shared Property IsTendersVisibleInPrintFormat7() As Boolean
        Get
            Return _IsTendersVisibleInPrintFormat7
        End Get
        Set(ByVal value As Boolean)
            _IsTendersVisibleInPrintFormat7 = value
        End Set
    End Property
    'code added by irfan on 30-7-2018 
    Public Shared _WhetherBillPrintisRequiredornot As Boolean = False
    Public Shared Property WhetherBillPrintisRequiredornot() As Boolean
        Get
            Return _WhetherBillPrintisRequiredornot
        End Get
        Set(ByVal value As Boolean)
            _WhetherBillPrintisRequiredornot = value
        End Set
    End Property
    Public Shared _IsHsnAndTaxVisibleInPrintFormat6 As Boolean = False
    Public Shared Property IsHsnAndTaxVisibleInPrintFormat6() As Boolean
        Get
            Return _IsHsnAndTaxVisibleInPrintFormat6
        End Get
        Set(ByVal value As Boolean)
            _IsHsnAndTaxVisibleInPrintFormat6 = value
        End Set
    End Property
    'code added by vipul for spectrum work as mettler
    Private Shared _SpectrumAsMettler As Boolean = False
    Public Shared Property SpectrumAsMettler() As Boolean
        Get
            Return _SpectrumAsMettler
        End Get
        Set(ByVal value As Boolean)
            _SpectrumAsMettler = value
        End Set
    End Property
    Private Shared _SpectrumMettlerPaymentTill As String
    Public Shared Property SpectrumMettlerPaymentTill() As String
        Get
            Return _SpectrumMettlerPaymentTill
        End Get
        Set(ByVal value As String)
            _SpectrumMettlerPaymentTill = value
        End Set
    End Property
    ''Sales Order 
    'Shared _IsCashRefundApplicable As Boolean = False
    'Shared _IsCLPCardSwapeAllowed As Boolean = False
    'Shared _IsCLPApplicable As Boolean = False
    'Shared _IsCustomerTypeApplicable As Boolean = False
    Shared _IsDeliveryDateApplicable As Boolean = False
    Shared _IsLessMinAdvAmountAllowed As Boolean = False
    'Shared _IsCLPCardSwape As Boolean
    Shared _ChkDeliveryDate As Integer = 0
    'Shared _IsNegativeInventoryAllowed As Boolean = False
    Shared _IsSalesPersonApplicable As Boolean = False
    'Shared _IsSaleReturnAllowed As Boolean = False
    Shared _IsSaleAdvanceAllowed As Double = False
    'Shared _IsReturnWithoutOldSOAllowed As Boolean = False
    'Shared _IsPromotionManually As Boolean = False
    'Shared _IsPromotionalMessage As Boolean = False
    'Shared _IsPrintPreviewAllowed As Boolean = False
    'Shared _IsPrintingTaxInfoAllowed As Boolean = False
    Shared _IsOtherChargesAllowed As Boolean = False
    'Shared _IsManualCLPSearchAllowed As Boolean = False
    'Shared _Supplier As String
    'Shared _SOBillRoundOff As Int32
    'Shared _SORoundOffRequired As Boolean = False
    Shared _SOPRINTER As String
    Shared _BLPRINTER As String
    Shared _CMSPRINTER As String
    Private Shared _SOBookingScreenTills As String
    Public Shared Property SOBookingScreenTills() As String
        Get
            Return _SOBookingScreenTills
        End Get
        Set(ByVal value As String)
            _SOBookingScreenTills = value
        End Set
    End Property
    Private Shared _ISAllowSOBooking As Boolean
    Public Shared Property ISAllowSOBooking() As Boolean
        Get
            Return _ISAllowSOBooking
        End Get
        Set(ByVal value As Boolean)
            _ISAllowSOBooking = value
        End Set
    End Property
    ' added by ketan For so Booking Print Priview Flag
    Private Shared _PrintPreviewRequiredForSOBooking As Boolean
    Public Shared Property PrintPreviewRequiredForSOBooking() As Boolean
        Get
            Return _PrintPreviewRequiredForSOBooking
        End Get
        Set(ByVal value As Boolean)
            _PrintPreviewRequiredForSOBooking = value
        End Set
    End Property
    Public Shared Property SOPRINTER() As String
        Get
            Return _SOPRINTER
        End Get
        Set(ByVal value As String)
            _SOPRINTER = value
        End Set
    End Property

    Public Shared Property BLPRINTER() As String
        Get
            Return _BLPRINTER
        End Get
        Set(ByVal value As String)
            _BLPRINTER = value
        End Set
    End Property

    Public Shared Property CMSPRINTER() As String
        Get
            Return _CMSPRINTER
        End Get
        Set(ByVal value As String)
            _CMSPRINTER = value
        End Set
    End Property

    'Public Shared ReadOnly Property SORoundOffRequired() As Boolean
    '    Get
    '        Return _SORoundOffRequired
    '    End Get
    'End Property

    'Public Shared ReadOnly Property SalesBillRoundOff() As Int32
    '    Get
    '        Return _SOBillRoundOff
    '    End Get
    'End Property

    'Public Shared Property SupplierCode() As String
    '    Get
    '        Return _Supplier
    '    End Get
    '    Set(ByVal value As String)
    '        _Supplier = value
    '    End Set
    'End Property
    'Public Shared Property IsCashRefundApplicable() As Boolean
    '    Get
    '        Return _IsCashRefundApplicable
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _IsCashRefundApplicable = value
    '    End Set
    'End Property
    'Public Shared Property IsCLPCardSwapeAllowed() As Boolean
    '    Get
    '        Return _IsCLPCardSwapeAllowed
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _IsCLPCardSwapeAllowed = value
    '    End Set
    'End Property
    'Public Shared Property IsCLPApplicable() As Boolean
    '    Get
    '        Return _IsCLPApplicable
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _IsCLPApplicable = value
    '    End Set
    'End Property
    'Public Shared Property IsCustomerTypeApplicable() As Boolean
    '    Get
    '        Return _IsCustomerTypeApplicable
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _IsCustomerTypeApplicable = value
    '    End Set
    'End Property
    Public Shared Property IsDeliveryDateApplicable() As Boolean
        Get
            Return _IsDeliveryDateApplicable
        End Get
        Set(ByVal value As Boolean)
            _IsDeliveryDateApplicable = value
        End Set
    End Property
    Public Shared Property IsLessMinAdvAmountAllowed() As Boolean
        Get
            Return _IsLessMinAdvAmountAllowed
        End Get
        Set(ByVal value As Boolean)
            _IsLessMinAdvAmountAllowed = value
        End Set
    End Property

    'Public Shared Property IsManualCLPSearchAllowed() As Boolean
    '    Get
    '        Return _IsManualCLPSearchAllowed
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _IsManualCLPSearchAllowed = value
    '    End Set
    'End Property

    Public Shared Property IsOtherChargesAllowed() As Boolean
        Get
            Return _IsOtherChargesAllowed
        End Get
        Set(ByVal value As Boolean)
            _IsOtherChargesAllowed = value
        End Set
    End Property

    'Public Shared Property IsPrintingTaxInfoAllowed() As Boolean
    '    Get
    '        Return _IsPrintingTaxInfoAllowed
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _IsPrintingTaxInfoAllowed = value
    '    End Set
    'End Property

    'Public Shared Property IsPrintPreviewAllowed() As Boolean
    '    Get
    '        Return _IsPrintPreviewAllowed
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _IsPrintPreviewAllowed = value
    '    End Set
    'End Property

    'Public Shared Property IsPromotionalMessage() As Boolean
    '    Get
    '        Return _IsPromotionalMessage
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _IsPromotionalMessage = value
    '    End Set
    'End Property

    'Public Shared Property IsPromotionManually() As Boolean
    '    Get
    '        Return _IsPromotionManually
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _IsPromotionManually = value
    '    End Set
    'End Property

    'Public Shared Property IsReturnWithoutOldSOAllowed() As Boolean
    '    Get
    '        Return _IsReturnWithoutOldSOAllowed
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _IsReturnWithoutOldSOAllowed = value
    '    End Set
    'End Property

    Public Shared Property IsSaleAdvanceAllowed() As Double
        Get
            Return _IsSaleAdvanceAllowed
        End Get
        Set(ByVal value As Double)
            _IsSaleAdvanceAllowed = value
        End Set
    End Property

    'Public Shared Property IsSaleReturnAllowed() As Boolean
    '    Get
    '        Return _IsSaleReturnAllowed
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _IsSaleReturnAllowed = value
    '    End Set
    'End Property

    Public Shared Property IsSalesPersonApplicable() As Boolean
        Get
            Return _IsSalesPersonApplicable
        End Get
        Set(ByVal value As Boolean)
            _IsSalesPersonApplicable = value
        End Set
    End Property


    'Public Shared Property IsNegativeInventoryAllowed() As Boolean
    '    Get
    '        Return _IsNegativeInventoryAllowed
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _IsNegativeInventoryAllowed = value
    '    End Set
    'End Property

    Public Shared Property ChkDeliveryDate() As Integer
        Get
            Return _ChkDeliveryDate
        End Get
        Set(ByVal value As Integer)
            _ChkDeliveryDate = value
        End Set
    End Property


    'Public Shared Property IsCLPCardSwape() As Boolean
    '    Get
    '        Return _IsCLPCardSwape
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _IsCLPCardSwape = value
    '    End Set
    'End Property
    ''' <summary>
    '''  Decimal points 
    ''' </summary>
    ''' <remarks>SO,BL,CM</remarks>
    Private Shared _iDecimalDigits As Integer
    Public Shared Property DecimalDigits() As Integer
        Get
            Return _iDecimalDigits
        End Get
        Set(ByVal value As Integer)
            _iDecimalDigits = value
        End Set
    End Property

    'Added By Rohit for CR-6001
    Public Shared Property SignOnRequired() As Boolean
        Get
            Return _SignOnRequired
        End Get
        Set(ByVal value As Boolean)
            _SignOnRequired = value
        End Set
    End Property

    'Add End

    'Added By Gaurav Danani
    Shared _AllowPrePrintedGV As Boolean = False
    Shared _AllowNonPrePrintedGV As Boolean = False
    Public Shared Property AllowPrePrintedGV() As Boolean
        Get
            Return _AllowPrePrintedGV
        End Get
        Set(ByVal value As Boolean)
            _AllowPrePrintedGV = value
        End Set
    End Property

    Public Shared Property AllowNonPrePrintedGV() As Boolean
        Get
            Return _AllowNonPrePrintedGV
        End Get
        Set(ByVal value As Boolean)
            _AllowNonPrePrintedGV = value
        End Set
    End Property
    'Add End
    Shared _CVInfoRequiredOnPrint As Boolean = False
    Public Shared Property CVInfoRequiredOnPrint() As Boolean
        Get
            Return _CVInfoRequiredOnPrint
        End Get
        Set(ByVal value As Boolean)
            _CVInfoRequiredOnPrint = value
        End Set
    End Property

    Shared _AllowDecimalQty As Boolean = False
    Public Shared Property AllowDecimalQty() As Boolean
        Get
            Return _AllowDecimalQty
        End Get
        Set(ByVal value As Boolean)
            _AllowDecimalQty = value
        End Set
    End Property

    Shared _IsCstTaxRequired As Boolean = False
    Public Shared Property IsCstTaxRequired() As Boolean
        Get
            Return _IsCstTaxRequired
        End Get
        Set(ByVal value As Boolean)
            _IsCstTaxRequired = value
        End Set
    End Property

    Shared _IsBatchManagementReq As Boolean = False
    Public Shared Property IsBatchManagementReq() As Boolean
        Get
            Return _IsBatchManagementReq
        End Get
        Set(ByVal value As Boolean)
            _IsBatchManagementReq = value
        End Set
    End Property

    Private Shared _IsOtherCustomerRequired As Boolean = False
    Public Shared Property IsOtherCustomerRequired() As Boolean
        Get
            Return _IsOtherCustomerRequired
        End Get
        Set(ByVal value As Boolean)
            _IsOtherCustomerRequired = value
        End Set
    End Property

    Private Shared _IsGVSellAllowedWithOtherArticle As Boolean
    Public Shared Property IsGVSellAllowedWithOtherArticle() As Boolean
        Get
            Return _IsGVSellAllowedWithOtherArticle
        End Get
        Set(ByVal value As Boolean)
            _IsGVSellAllowedWithOtherArticle = value
        End Set
    End Property

    'Shared _AdsrReportProcedureName As String
    'Public Shared Property AdsrReportProcedureName() As String
    '    Get
    '        Return _AdsrReportProcedureName
    '    End Get
    '    Set(ByVal value As String)
    '        _AdsrReportProcedureName = value
    '    End Set
    'End Property

    'Shared _CompositeTaxReqOnPrint As Boolean = False
    'Public Shared Property CompositeTaxReqOnPrint() As Boolean
    '    Get
    '        Return _CompositeTaxReqOnPrint
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _CompositeTaxReqOnPrint = value
    '    End Set
    'End Property

    Shared _NumpadRequired As Boolean = False
    Public Shared Property NumpadRequired() As Boolean
        Get
            Return _NumpadRequired
        End Get
        Set(ByVal value As Boolean)
            _NumpadRequired = value
        End Set
    End Property

    Shared _DisplayArticleAlphabetChar As Boolean = False
    Public Shared Property DisplayArticleAlphabetChar() As Boolean
        Get
            Return _DisplayArticleAlphabetChar
        End Get
        Set(ByVal value As Boolean)
            _DisplayArticleAlphabetChar = value
        End Set
    End Property

    Private Shared _SpectrumLicenseRequired As Boolean = False
    Public Shared Property SpectrumLicenseRequired() As Boolean
        Get
            Return _SpectrumLicenseRequired
        End Get
        Set(ByVal value As Boolean)
            _SpectrumLicenseRequired = value
        End Set
    End Property

    Private Shared _ClpSMSAllowed As Boolean = False
    Public Shared Property ClpSMSAllowed() As Boolean
        Get
            Return _ClpSMSAllowed
        End Get
        Set(ByVal value As Boolean)
            _ClpSMSAllowed = value
        End Set
    End Property

    Private Shared _CustomerWebServiceUrl As String
    Public Shared Property CustomerWebServiceUrl() As String
        Get
            Return _CustomerWebServiceUrl
        End Get
        Set(ByVal value As String)
            _CustomerWebServiceUrl = value
        End Set
    End Property

    Private Shared _SpectrumLiteAllowed As Boolean = False
    Public Shared Property SpectrumLiteAllowed() As Boolean
        Get
            Return _SpectrumLiteAllowed
        End Get
        Set(ByVal value As Boolean)
            _SpectrumLiteAllowed = value
        End Set
    End Property


    Private Shared _IsBillScanApplicable As Boolean
    Public Shared Property IsBillScanApplicable() As Boolean
        Get
            Return _IsBillScanApplicable
        End Get
        Set(ByVal value As Boolean)
            _IsBillScanApplicable = value
        End Set
    End Property

    Private Shared _IsKOTPrintEachlineItem As Boolean
    Public Shared Property IsKOTPrintEachlineItem() As Boolean
        Get
            Return _IsKOTPrintEachlineItem
        End Get
        Set(ByVal value As Boolean)
            _IsKOTPrintEachlineItem = value
        End Set
    End Property
    Private Shared _IsKOTPrintQuantityWise As Boolean
    Public Shared Property IsKOTPrintQuantityWise() As Boolean
        Get
            Return _IsKOTPrintQuantityWise
        End Get
        Set(ByVal value As Boolean)
            _IsKOTPrintQuantityWise = value
        End Set
    End Property
    Private Shared _ChecklistOnTillOpen As Boolean
    Public Shared Property ChecklistOnTillOpen() As Boolean
        Get
            Return _ChecklistOnTillOpen
        End Get
        Set(ByVal value As Boolean)
            _ChecklistOnTillOpen = value
        End Set
    End Property

    Private Shared _NotificationPopUpRequired As Boolean
    Public Shared Property NotificationPopUpRequired() As Boolean
        Get
            Return _NotificationPopUpRequired
        End Get
        Set(ByVal value As Boolean)
            _NotificationPopUpRequired = value
        End Set
    End Property

    Private Shared _NotificationText As String
    Public Shared Property NotificationText() As String
        Get
            Return _NotificationText
        End Get
        Set(ByVal value As String)
            _NotificationText = value
        End Set
    End Property

    Private Shared _NotificationTiming As Double
    Public Shared Property NotificationTiming() As Double
        Get
            Return _NotificationTiming
        End Get
        Set(ByVal value As Double)
            _NotificationTiming = value
        End Set
    End Property


    Private Shared _MettlerConnString As String
    Public Shared Property MettlerConnString() As String
        Get
            Return _MettlerConnString
        End Get
        Set(ByVal value As String)
            _MettlerConnString = value
        End Set
    End Property

    Private Shared _JkTicketingSystem As Boolean
    Public Shared Property JkTicketingSystem() As Boolean
        Get
            Return _JkTicketingSystem
        End Get
        Set(ByVal value As Boolean)
            _JkTicketingSystem = value
        End Set
    End Property


    Private Shared _ordPrepTime As Integer
    Public Shared Property OrdPrepTime() As Integer
        Get
            Return _ordPrepTime
        End Get
        Set(ByVal value As Integer)
            _ordPrepTime = value
        End Set
    End Property
    Private Shared _TicketingSystemPopupInterval As Integer
    Public Shared Property TicketingSystemPopupInterval() As Integer
        Get
            Return _TicketingSystemPopupInterval
        End Get
        Set(ByVal value As Integer)
            _TicketingSystemPopupInterval = value
        End Set
    End Property
    Private Shared _IsTablet As Boolean
    Public Shared Property IsTablet() As Boolean
        Get
            Return _IsTablet
        End Get
        Set(ByVal value As Boolean)
            _IsTablet = value
        End Set
    End Property
    Private Shared _IsCustomerMandatoryForCreditSale As Boolean
    Public Shared Property IsCustomerMandatoryForCreditSale() As Boolean
        Get
            Return _IsCustomerMandatoryForCreditSale
        End Get
        Set(ByVal value As Boolean)
            _IsCustomerMandatoryForCreditSale = value
        End Set
    End Property
    Private Shared _KOTAndBillGeneration As Boolean
    Public Shared Property KOTAndBillGeneration() As Boolean
        Get
            Return _KOTAndBillGeneration
        End Get
        Set(ByVal value As Boolean)
            _KOTAndBillGeneration = value
        End Set
    End Property
    'RecordDaysToDelete
    Private Shared _RecordDaysToDelete As Boolean
    Public Shared Property RecordDaysToDelete() As String
        Get
            Return _RecordDaysToDelete
        End Get
        Set(ByVal value As String)
            _RecordDaysToDelete = value
        End Set
    End Property
    Private Shared _IsGSTForComboArticle As Boolean       'vipin Changes for Combo 07.05.2017
    Public Shared Property IsGSTForComboArticle() As Boolean
        Get
            Return IsGSTForComboArticle
        End Get
        Set(ByVal value As Boolean)
            _IsGSTForComboArticle = value
        End Set
    End Property
    Private Shared _renderGrievance As Boolean
    Public Shared Property RenderGrievance() As Boolean
        Get
            Return _renderGrievance
        End Get
        Set(ByVal value As Boolean)
            _renderGrievance = value
        End Set
    End Property
    Private Shared _customerwisepricemanagement As Boolean = False
    Public Shared Property customerwisepricemanagement() As Boolean
        Get
            Return _customerwisepricemanagement
        End Get
        Set(ByVal value As Boolean)
            _customerwisepricemanagement = value
        End Set
    End Property


    Private Shared _NoOfCopiesforHomeDelivery As Integer
    Public Shared Property NoOfCopiesforHomeDelivery() As Integer
        Get
            Return _NoOfCopiesforHomeDelivery
        End Get
        Set(ByVal value As Integer)
            _NoOfCopiesforHomeDelivery = value
        End Set
    End Property
    Private Shared _NoOfCopiesforDineIn As Integer
    Public Shared Property NoOfCopiesforDineIn() As Integer
        Get
            Return _NoOfCopiesforDineIn
        End Get
        Set(ByVal value As Integer)
            _NoOfCopiesforDineIn = value
        End Set
    End Property
    Private Shared _NoOfCopiesforTakeAway As Integer
    Public Shared Property NoOfCopiesforTakeAway() As Integer
        Get
            Return _NoOfCopiesforTakeAway
        End Get
        Set(ByVal value As Integer)
            _NoOfCopiesforTakeAway = value
        End Set
    End Property
    'code added for jk sprint 25
    Private Shared _AutoSyncOnDSR As Boolean = False
    Public Shared Property AutoSyncOnDSR() As Boolean
        Get
            Return _AutoSyncOnDSR
        End Get
        Set(ByVal value As Boolean)
            _AutoSyncOnDSR = value
        End Set
    End Property
    'code   added for pizza etc 
    Private Shared _DineInAllowUpdateAfterGenerateBill As Boolean = False
    Public Shared Property DineInAllowUpdateAfterGenerateBill() As Boolean
        Get
            Return _DineInAllowUpdateAfterGenerateBill
        End Get
        Set(ByVal value As Boolean)
            _DineInAllowUpdateAfterGenerateBill = value
        End Set
    End Property



    'code added for Jk sprint 28 
    Private Shared _OpenBackOfficeFromFO As String
    Public Shared Property OpenBackOfficeFromFO() As String
        Get
            Return _OpenBackOfficeFromFO
        End Get
        Set(ByVal value As String)
            _OpenBackOfficeFromFO = value
        End Set
    End Property
    Private Shared _EnableAmmyyAdmin As String
    Public Shared Property EnableAmmyyAdmin() As String
        Get
            Return _EnableAmmyyAdmin
        End Get
        Set(ByVal value As String)
            _EnableAmmyyAdmin = value
        End Set
    End Property

    Private Shared _ColabaSOPrint As Boolean = False
    Public Shared Property ColabaSOPrint() As Boolean
        Get
            Return _ColabaSOPrint
        End Get
        Set(ByVal value As Boolean)
            _ColabaSOPrint = value
        End Set
    End Property
    Private Shared _IsInvoiceSendOnMailRequired As Boolean = False
    Public Shared Property IsInvoiceSendOnMailRequired() As Boolean
        Get
            Return _IsInvoiceSendOnMailRequired
        End Get
        Set(ByVal value As Boolean)
            _IsInvoiceSendOnMailRequired = value
        End Set
    End Property
    Private Shared _SendInvoiceSMS As Boolean = False
    Public Shared Property SendInvoiceSMS() As Boolean
        Get
            Return _SendInvoiceSMS
        End Get
        Set(ByVal value As Boolean)
            _SendInvoiceSMS = value
        End Set
    End Property
    Private Shared _AutoPopulateFloatingDetails As Boolean = False
    Public Shared Property AutoPopulateFloatingDetails() As Boolean
        Get
            Return _AutoPopulateFloatingDetails
        End Get
        Set(ByVal value As Boolean)
            _AutoPopulateFloatingDetails = value
        End Set
    End Property
    Public Shared _so_sms_applicable As String = ""
    Public Shared Property so_sms_applicable() As String
        Get
            Return _so_sms_applicable
        End Get
        Set(ByVal value As String)
            _so_sms_applicable = value
        End Set
    End Property
    Public Shared _AutoKOTGenerateTimeInterval As String = ""
    Public Shared Property AutoKOTGenerateTimeInterval() As String
        Get
            Return _AutoKOTGenerateTimeInterval
        End Get
        Set(ByVal value As String)
            _AutoKOTGenerateTimeInterval = value
        End Set
    End Property
    'transfer Home delivery and take away order from dine-in module to KDS screen
    Private Shared _TransferHomeDeliveryandTakeAwayOrdertoKDS As Boolean = False
    Public Shared Property TransferHomeDeliveryandTakeAwayOrdertoKDS() As Boolean
        Get
            Return _TransferHomeDeliveryandTakeAwayOrdertoKDS
        End Get
        Set(ByVal value As Boolean)
            _TransferHomeDeliveryandTakeAwayOrdertoKDS = value
        End Set
    End Property
    'TenderForCashDrawer
    Private Shared _TenderForCashDrawer As String = ""
    Public Shared Property TenderForCashDrawer() As String
        Get
            Return _TenderForCashDrawer
        End Get
        Set(ByVal value As String)
            _TenderForCashDrawer = value
        End Set
    End Property
    Private Shared _EnablePhonePeIntegration As Boolean
    Public Shared Property EnablePhonePeIntegration() As Boolean
        Get
            Return _EnablePhonePeIntegration
        End Get
        Set(ByVal value As Boolean)
            _EnablePhonePeIntegration = value
        End Set
    End Property
    Private Shared _PhonePeMerchantId As String
    Public Shared Property PhonePeMerchantId() As String
        Get
            Return _PhonePeMerchantId
        End Get
        Set(ByVal value As String)
            _PhonePeMerchantId = value
        End Set
    End Property

    Private Shared _PhonePeRequestPaymentUrl As String
    Public Shared Property PhonePeRequestPaymentUrl() As String
        Get
            Return _PhonePeRequestPaymentUrl
        End Get
        Set(ByVal value As String)
            _PhonePeRequestPaymentUrl = value
        End Set
    End Property

    Private Shared _PhonePeCheckPaymentUrl As String
    Public Shared Property PhonePeCheckPaymentUrl() As String
        Get
            Return _PhonePeCheckPaymentUrl
        End Get
        Set(ByVal value As String)
            _PhonePeCheckPaymentUrl = value
        End Set
    End Property

    Private Shared _PhonePeCancelPaymentUrl As String
    Public Shared Property PhonePeCancelPaymentUrl() As String
        Get
            Return _PhonePeCancelPaymentUrl
        End Get
        Set(ByVal value As String)
            _PhonePeCancelPaymentUrl = value
        End Set
    End Property

    Private Shared _PhonepeAuthKey As String
    Public Shared Property PhonepeAuthKey() As String
        Get
            Return _PhonepeAuthKey
        End Get
        Set(ByVal value As String)
            _PhonepeAuthKey = value
        End Set
    End Property

    Private Shared _PhonepeAuthIndex As Integer
    Public Shared Property PhonepeAuthIndex() As Integer
        Get
            Return _PhonepeAuthIndex
        End Get
        Set(ByVal value As Integer)
            _PhonepeAuthIndex = value
        End Set
    End Property
#Region "Hashtag"
    Private Shared _EnableHashTagIntegration As Boolean = False
    Public Shared Property EnableHashTagIntegration() As Boolean
        Get
            Return _EnableHashTagIntegration
        End Get
        Set(ByVal value As Boolean)
            _EnableHashTagIntegration = value
        End Set
    End Property
    Private Shared _HashTagIntegrationId As String = ""
    Public Shared Property HashTagIntegrationID() As String
        Get
            Return _HashTagIntegrationId
        End Get
        Set(ByVal value As String)
            _HashTagIntegrationId = value
        End Set
    End Property
    Private Shared _EnableHashTagLoyaltyPoint As Boolean = False
    Public Shared Property EnableHashTagLoyaltyPoint() As Boolean
        Get
            Return _EnableHashTagLoyaltyPoint
        End Get
        Set(ByVal value As Boolean)
            _EnableHashTagLoyaltyPoint = value
        End Set
    End Property
    Private Shared _ConversionFactorForHashtagLoyaltyPoint As Double = 1
    Public Shared Property ConversionFactorForHashtagLoyaltyPoint() As Double
        Get
            Return _ConversionFactorForHashtagLoyaltyPoint
        End Get
        Set(ByVal value As Double)
            _ConversionFactorForHashtagLoyaltyPoint = value
        End Set
    End Property
    Private Shared _HashTagRewardsUrl As String = ""
    Public Shared Property HashTagRewardsUrl() As String
        Get
            Return _HashTagRewardsUrl
        End Get
        Set(ByVal value As String)
            _HashTagRewardsUrl = value
        End Set
    End Property
    Private Shared _OrderAcceptReadyInMinutes As Integer
    Public Shared Property OrderAcceptReadyInMinutes() As Integer
        Get
            Return _OrderAcceptReadyInMinutes
        End Get
        Set(ByVal value As Integer)
            _OrderAcceptReadyInMinutes = value
        End Set
    End Property
    Private Shared _UberGenerateTokenAPI As String = ""
    Public Shared Property UberGenerateTokenAPI() As String
        Get
            Return _UberGenerateTokenAPI
        End Get
        Set(ByVal value As String)
            _UberGenerateTokenAPI = value
        End Set
    End Property

    Private Shared _UberOrderAcceptAPI As String = ""
    Public Shared Property UberOrderAcceptAPI() As String
        Get
            Return _UberOrderAcceptAPI
        End Get
        Set(ByVal value As String)
            _UberOrderAcceptAPI = value
        End Set
    End Property
#End Region

#End Region
    Dim DocType As String = ""

    ''' <summary>
    ''' Get the Site default Settings And Set Default Config Object
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetDefaultSettings()
        Try
            Dim dt As DataTable
            Dim objCommann As New SpectrumBL.clsCashMemo
            dt = objCommann.GetDefaultSetting(clsAdmin.SiteCode, DocType)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                If DocType = "BLS" Then
                    For Each dr As DataRow In dt.Rows
                        'If dr("FLDLABEL").ToString().ToUpper = "BLISPRINTFOOTER" Then
                        '    clsDefaultConfiguration.FooterPrinting = dr("FLDVALUE").ToString
                        'ElseIf dr("FLDLABEL").ToString().ToUpper = "BLISPRINTHEADER" Then
                        '    clsDefaultConfiguration.HeaderPrinting = dr("FLDVALUE").ToString

                        If dr("FLDLABEL").ToString().ToUpper = "BLISSALESPERSONAPPLICABLE" Then
                            clsDefaultConfiguration.BLIsSalesPersonApplicable = dr("FLDVALUE")
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "BLNEGATIVEINVENTORYAPPLICABLE" Then
                            '    clsDefaultConfiguration.BLNegativeInventoryApplicable = dr("FLDVALUE")
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "BLISCLPAPPLICABLE" Then
                            '    clsDefaultConfiguration.BLIsCLPApplicable = dr("FLDVALUE")

                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "BLSTORAGELOC" Then
                            '    clsDefaultConfiguration.BLStorageLocation = dr("FLDVALUE")

                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "BLCREDITVOUCHERTEXT" Then
                            '    clsDefaultConfiguration.BLCreditVoucherText = dr("FLDVALUE")

                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "BLCREDITVOUCHEREXPIRYINDAYS" Then
                            '    clsDefaultConfiguration.BLCreditVoucherExpiryInDays = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "BLGVSALE" Then
                            clsDefaultConfiguration.BLGVSale = dr("FLDVALUE")

                        ElseIf dr("FLDLABEL").ToString().ToUpper = "BLCLOSEDISCOUNTPERCENTAGE" Then
                            clsDefaultConfiguration.BLCloseDiscountPercentage = dr("FLDVALUE")

                        ElseIf dr("FLDLABEL").ToString().ToUpper = "BLISCHECKDELIVERYDATE" Then
                            clsDefaultConfiguration.BLIsCheckDeliveryDate = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "BLPRINTPREIVEW".ToUpper() And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            clsDefaultConfiguration.BLPrintPreviewAllowed = True
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "BLZEROTAX" Then
                            '    clsDefaultConfiguration.BLArticleZeroTaxAllowed = dr("FLDVALUE")

                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "BLPROMOTIONID" Then
                            '    clsDefaultConfiguration.BLPromtionID = dr("FLDVALUE")

                        ElseIf dr("FLDLABEL").ToString().ToUpper = "BLCLPINTIMATION" Then
                            clsDefaultConfiguration.BLCLPIntimation = dr("FLDVALUE")
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "BLBILLROUNDOFF" Then
                            '    clsDefaultConfiguration.BLBillRoundOff = dr("FLDVALUE")
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "BLSCLPMANUALSEARCH" Then
                            '    clsDefaultConfiguration.IsManualCLPCustomerSearch = dr("FLDVALUE")
                            'ElseIf dr("FLDLABEL").ToString.ToUpper() = "BLRoundOffRequired".ToUpper() Then
                            '    _BLRoundOffRequired = dr("FldValue")
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "PRINTINGTYPE" Then
                            '    clsDefaultConfiguration.PrintingType = dr("FLDVALUE").ToString

                            'Rakesh-23.08.2013:Set value of CLP
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "CLPOnPriceChange".ToUpper() Then
                            clsDefaultConfiguration.CLPOnPriceChange = Convert.ToBoolean(dr("FldValue"))
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "CLP_APPLICABLE_EDIT".ToUpper() Then
                            clsDefaultConfiguration.CLP_Applicable_Edit = Convert.ToBoolean(dr("FldValue"))
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "ALLOW_POINT_ON_REDEEMPTION".ToUpper() Then
                            clsDefaultConfiguration.CLP_Point_On_redeemption = Convert.ToBoolean(dr("FldValue"))
                        End If
                    Next
                ElseIf DocType = "TillOpenNClose" Then
                    For Each dr As DataRow In dt.Rows
                        If dr("FLDLABEL").ToString().ToUpper = "USERAMOUNTPRINT" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            _UserAmountPrint = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "TILLOPENNCLOSE" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            _TillOpenNClose = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "CheckTillCloseValue".ToUpper() And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            _CheckTillClose = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "CHECKTILLOPENAMOUNT".ToUpper() Then
                            clsDefaultConfiguration.CheckTillOpenAmount = dr("FLDVALUE").ToString
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "MAXNEXTDAYFLOATAMT".ToUpper() Then
                            MaxNextDayFloatAmount = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "SeperateSaleTerminalId".ToUpper() Then
                            SeperateSaleTerminalId = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "EDCSummaryinput".ToUpper() Then  '' added by nikhil for EDC card Summary
                            _EDCSummaryinput = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "EnableImprestCash".ToUpper() Then
                            EnableImprestCash = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "ImprestCashTill".ToUpper() Then
                            ImprestCashTill = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "ImprestCashAmount".ToUpper() Then
                            ImprestCashAmount = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "AutoPopulateFloatingDetails".ToUpper() Then 'vipul
                            AutoPopulateFloatingDetails = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "DisableAmountPaidToVendorsInTillClose".ToUpper() Then
                            DisableAmountPaidToVendorsInTillClose = dr("FLDVALUE")
                        End If
                    Next
                ElseIf DocType = "CMS" Then
                    For Each dr As DataRow In dt.Rows
                        If dr("FLDLABEL").ToString().ToUpper = "TAXDETAIL" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            clsDefaultConfiguration.TaxDetailsRequired = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "SALESPERSONALLOWED" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            clsDefaultConfiguration.SalesPersonApplicable = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "labelPrintFormatNo".ToUpper() Then   ' vipin 23.08.2018 
                            clsDefaultConfiguration.labelPrintFormatNo = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "MANUALPROMOALLOWED" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            clsDefaultConfiguration.ManualPromotionAllowed = True
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "ITEMFULLNAME" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            '    clsDefaultConfiguration.PrintItemFullName = True
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "PRINTDISCPER" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            '    clsDefaultConfiguration.PrintDiscountPercentage = True
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "CMNEGATIVEINVENTORYALLOWED" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            '    clsDefaultConfiguration.NagativeBillingAllowed = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "ADVANCESALEALLOWED" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            clsDefaultConfiguration.AdvanceSalesAllowed = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "CASHREFUNDALLOWED" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            clsDefaultConfiguration.CashRefund = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "CLPINTIMATION" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            clsDefaultConfiguration.CLPIntimation = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "CLPREGISTRATION" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            clsDefaultConfiguration.CLPRegistration = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "ROUNDOFFREQUIRED" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            _RoundOffRequired = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "HOLDBILLPRINT" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            clsDefaultConfiguration.HoldBillPrint = True
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "PRINTINGTAXINFO" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            '    clsDefaultConfiguration.PrintingTaxInfo = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "WITHOUTBILLALLOWED" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            clsDefaultConfiguration.WithoutBillAllowed = True
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "CLPPOINTSALE" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            '    clsDefaultConfiguration.CLPPointSaleAllowed = True
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "HEADERPRINT" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            '    clsDefaultConfiguration.HeaderPrinting = True
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "FOOTERPRINT" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            '    clsDefaultConfiguration.FooterPrinting = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "ISSUECREDITVOUCHER" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            clsDefaultConfiguration.IssueCreditVoucher = True
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "ZEROTAX" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            '    clsDefaultConfiguration.ZeroTaxs = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "GVSALE" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            clsDefaultConfiguration.GVsaleAllowed = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "OTHERCHARGESAMOUNTEDITABLE" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            _OtherCharges = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "SHOWDISCOUNTAMT" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            _ShowDiscAmt = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "RETURNREASONREUIRED" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            _ReturnReason = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "HOLDBILLBARCODE".ToUpper() And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            _HoldBillBarcode = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "PRINTPREVIEWREQUIRED" Then
                            clsDefaultConfiguration.PrintPreivewReq = dr("FLDVALUE")
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "PRINTINGTYPE" Then
                            '    clsDefaultConfiguration.PrintingType = dr("FLDVALUE").ToString
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "CLPREGISTRATIONAMOUNT" Then
                            clsDefaultConfiguration.CLPRegistrationAmt = dr("FLDVALUE").ToString
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "CMSTORAGELOC" Then
                            '    clsDefaultConfiguration.CashMemoStorageLocation = dr("FLDVALUE").ToString
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "BILLROUNDOFF" Then
                            '    _RoundAt = dr("FLDVALUE").ToString
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "CMSCLPMANUALSEARCH" Then
                            '    clsDefaultConfiguration.IsManualCLPCustomerSearch = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "ALLOW NON PRE-PRINTED GV".ToUpper() Then
                            clsDefaultConfiguration.AllowNonPrePrintedGV = dr("FldValue")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "ALLOW PRE-PRINTED GV".ToUpper() Then
                            clsDefaultConfiguration.AllowPrePrintedGV = dr("FldValue")
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "INCLUSIVETAXDISPLAY".ToUpper() Then
                            '    clsDefaultConfiguration.InclusiveTaxDisplay = dr("FLDVALUE").ToString
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "COMPANYNAME".ToUpper() Then
                            '    clsDefaultConfiguration.CompanyName = dr("FLDVALUE").ToString

                        ElseIf dr("FLDLABEL").ToString().ToUpper = "CVInfoReqOnPrint".ToUpper() Then
                            clsDefaultConfiguration.CVInfoRequiredOnPrint = dr("FLDVALUE").ToString
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "ALLOWDECIMALQUANTITY".ToUpper() Then
                            clsDefaultConfiguration.AllowDecimalQty = dr("FLDVALUE").ToString
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "ISCUSTNAMEREADONLY".ToUpper() Then
                            clsDefaultConfiguration.IsCustNameReadonly = dr("FLDVALUE").ToString
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "CSTTAXCODE".ToUpper() Then
                            '    clsDefaultConfiguration.CSTTaxCode = dr("FLDVALUE").ToString
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "IsCstTaxRequired".ToUpper() Then
                            '    clsDefaultConfiguration.IsCstTaxRequired = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "IsGVSellAllowedWithOtherArticle".ToUpper() Then
                            clsDefaultConfiguration.IsGVSellAllowedWithOtherArticle = dr("FLDVALUE")
                            'ElseIf dr("FLDLABEL").ToString().ToUpper = "CompositeTaxReqOnPrint".ToUpper() Then
                            '    clsDefaultConfiguration.CompositeTaxReqOnPrint = dr("FLDVALUE").ToString
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "DISPLAYTOTALSAVING" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            clsDefaultConfiguration.IsDisplayTotalSaving = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "NumpadRequired".ToUpper() Then
                            clsDefaultConfiguration.NumpadRequired = dr("FLDVALUE").ToString
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "DisplayArticleAlphabetChar".ToUpper() Then
                            clsDefaultConfiguration.DisplayArticleAlphabetChar = dr("FLDVALUE").ToString
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "KOTPrintRequired".ToUpper() Then
                            clsDefaultConfiguration.KOTPrintRequired = dr("FLDVALUE").ToString
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "KOTPRINTEACHLINEITEM".ToUpper() Then
                            clsDefaultConfiguration.IsKOTPrintEachlineItem = dr("FLDVALUE").ToString
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "KOTTicketPrint4eachQuantity".ToUpper() Then
                            clsDefaultConfiguration.IsKOTPrintQuantityWise = dr("FLDVALUE").ToString
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "KotFontBold".ToUpper() Then
                            clsDefaultConfiguration.IsKotFontBold = dr("FLDVALUE").ToString
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "KotFontLarge".ToUpper() Then
                            clsDefaultConfiguration.IsKotFontLarge = dr("FLDVALUE").ToString
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "KOTPrintFormatNo".ToUpper() Then
                            clsDefaultConfiguration.KOTPrintFormatNo = dr("FLDVALUE").ToString
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "TokenNoRequiredInKOT".ToUpper() Then
                            clsDefaultConfiguration.TokenNoRequiredInKOT = dr("FLDVALUE").ToString
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "CustomerNameRequiredInKOT".ToUpper() Then
                            clsDefaultConfiguration.CustomerNameRequiredInKOT = dr("FLDVALUE").ToString
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "AllowMobileNoEditable".ToUpper() Then
                            clsDefaultConfiguration.AllowMobileNoEditable = dr("FLDVALUE").ToString
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "AllowBillingOnlyAfterSelectionOfSalesType".ToUpper() Then
                            clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType = dr("FLDVALUE").ToString

                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "PrintFormatNo".ToUpper() Then
                            clsDefaultConfiguration.PrintFormatNo = Val(dr("FLDVALUE"))
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "BILLSCANAPPLICABLE".ToUpper() Then
                            clsDefaultConfiguration.IsBillScanApplicable = dr("FLDVALUE").ToString

                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "AllowBillPrintForCreditSales".ToUpper() Then
                            clsDefaultConfiguration.AllowBillPrintForCreditSales = dr("FLDVALUE").ToString
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "GiftVoucherReturnAllowed".ToUpper() Then
                            clsDefaultConfiguration.GiftVoucherReturnAllowed = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "DineInProcess".ToUpper() Then
                            clsDefaultConfiguration.DineInProcess = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "DefaultSaleType".ToUpper() Then
                            clsDefaultConfiguration.DefaultSaleType = dr("FLDVALUE")
                            'added by ketan
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "AfterSelectSaleTypeCmSelectionRequired".ToUpper() Then
                            clsDefaultConfiguration.IsAfterSelectSaleTypeCmSelectionRequired = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "PrintSeparateKotFoReachHierarchy".ToUpper() Then
                            clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "Allow_Tax_Mapping_Based_On_Order_Type".ToUpper() Then
                            clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType = dr("FLDVALUE")
                            '--added by sagar new flag for round off isuue at om ganesh
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "BillRoundOffNotRequired".ToUpper() Then
                            clsDefaultConfiguration.BillRoundOffNotRequired = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "DirectCashPayment".ToUpper() Then
                            clsDefaultConfiguration.DirectCashPayment = dr("FLDVALUE")
                            '---------------PC
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "ArticlewiseKOT".ToUpper() Then
                            clsDefaultConfiguration.IsArticleWiseKOT = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "CounterCopy".ToUpper() Then
                            clsDefaultConfiguration.IsCounterCopy = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "FinalReceipt".ToUpper() Then
                            clsDefaultConfiguration.IsFinalReceipt = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "ExtendScreen".ToUpper() Then
                            clsDefaultConfiguration.ExtendScreen = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "HariOmExtendScreen".ToUpper() Then
                            clsDefaultConfiguration.HariOmExtendScreen = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "Serial_Port".ToUpper() Then
                            clsDefaultConfiguration.SerialPort = dr("FLDVALUE")
                            '---------------Evas pizza changes on 25-11-2016
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "EvasPizzaChanges".ToUpper() Then
                            clsDefaultConfiguration.EvasPizzaChanges = dr("FLDVALUE")
                            '---------------Hope india on 06-12-2016
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "EnableHealthCare".ToUpper() Then
                            clsDefaultConfiguration.EnableHealthCare = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "ReservationTime".ToUpper() Then
                            clsDefaultConfiguration.ReservationTime = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "ExpiryTime".ToUpper() Then
                            clsDefaultConfiguration.ExpiryTime = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "ExtendTime".ToUpper() Then
                            clsDefaultConfiguration.ExtendTime = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "LockTime".ToUpper() Then
                            clsDefaultConfiguration.LockTime = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "CustStayTime".ToUpper() Then
                            clsDefaultConfiguration.CustStayTime = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "customerwisepricemanagement".ToUpper() Then
                            clsDefaultConfiguration.customerwisepricemanagement = dr("FLDVALUE")
                            '----number of print copies
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "NoOfCopiesforHomeDelivery".ToUpper() Then
                            clsDefaultConfiguration.NoOfCopiesforHomeDelivery = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "NoOfCopiesforDineIn".ToUpper() Then
                            clsDefaultConfiguration.NoOfCopiesforDineIn = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "NoOfCopiesforTakeAway".ToUpper() Then
                            clsDefaultConfiguration.NoOfCopiesforTakeAway = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "PrintDineInBillOnSettlement".ToUpper() Then
                            clsDefaultConfiguration.PrintDineInBillOnSettlement = dr("FLDVALUE")
                            '---------------added by khusrao adil on 24-05-2018  for shaolin
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "TableNameForDineIn".ToUpper() Then
                            clsDefaultConfiguration.TableNameForDineIn = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "NewTouchCashMemo".ToUpper() Then
                            clsDefaultConfiguration.EnableNewTouchCashMemo = dr("FLDVALUE")
                            '---------------added by khusrao adil on 01-12-2017 for jk sprint 32
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "JKPrintFormatEnable".ToUpper() Then
                            clsDefaultConfiguration.JKPrintFormatEnable = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "PayFromInnoviti".ToUpper() Then
                            clsDefaultConfiguration.PayFromInnoviti = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "InnovitiForTerminals".ToUpper() Then
                            clsDefaultConfiguration.InnovitiForTerminals = dr("FLDVALUE")
                            '---------------aded by khusrao adil on 01-08-2018 for sharda resturent
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "TableNoRequiredInDineInModuleBillPrint".ToUpper() Then
                            clsDefaultConfiguration.TableNoRequiredInDineInModuleBillPrint = dr("FLDVALUE")
                            '---------------aded by khusrao adil on 14-06-2017 for natural juhu store
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "AuthReqForCreditSalesAdjustmentOnCSA".ToUpper() Then
                            clsDefaultConfiguration.AuthReqForCreditSalesAdjustmentOnCSA = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "DineInAllowUpdateAfterGenerateBill".ToUpper() Then   'code added for pizza etc client
                            clsDefaultConfiguration.DineInAllowUpdateAfterGenerateBill = dr("FLDVALUE")

                            'code added by vipul add new rate column in print format 6
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "IsRatevisibleInPrintFormat6".ToUpper() Then
                            clsDefaultConfiguration.IsRatevisibleInPrintFormat6 = dr("FLDVALUE")
                            'code added by irfan on 8/9/2017 against tender visiblity and  IsHsnAndTaxVisibleInPrintFormat6
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "IsTendersVisibleInPrintFormat7".ToUpper() Then
                            clsDefaultConfiguration.IsTendersVisibleInPrintFormat7 = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "WhetherBillPrintisRequiredornot".ToUpper() Then
                            clsDefaultConfiguration.WhetherBillPrintisRequiredornot = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "IsHsnAndTaxVisibleInPrintFormat6".ToUpper() Then
                            clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6 = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "SpectrumAsMettler".ToUpper() Then   'code added vipul for spectrum work as mettler
                            clsDefaultConfiguration.SpectrumAsMettler = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "SpectrumMettlerPaymentTill".ToUpper() Then   'code added vipul 
                            clsDefaultConfiguration.SpectrumMettlerPaymentTill = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "IsNewMembership".ToUpper() Then   'code added vipul 
                            clsDefaultConfiguration.IsNewMembership = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "ChangeQtyBasedOnPriceChange".ToUpper() Then   'code added vipin 
                            clsDefaultConfiguration.ChangeQtyBasedOnPriceChange = dr("FLDVALUE")
                            'ChangeQtyBasedOnPriceChange
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "OnTooltipDisplayTheKitArticleIngredients".ToUpper() Then
                            clsDefaultConfiguration.OnTooltipDisplayTheKitArticleIngredients = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "IsInvoiceSendOnMailRequired".ToUpper() Then   'code added vipul 
                            clsDefaultConfiguration.IsInvoiceSendOnMailRequired = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "SendInvoiceSMS".ToUpper() Then   ' vipul 
                            clsDefaultConfiguration.SendInvoiceSMS = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "SplitBillParentNodeCode".ToUpper() Then   ' vipul 
                            clsDefaultConfiguration.SplitBillParentNodeCode = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "PaxSelectionOnTable".ToUpper() Then   ' vipin 23.08.2018 
                            clsDefaultConfiguration.PaxSelectionOnTable = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "HierarchyforKOT4eachQuantity".ToUpper() Then 'Jayesh 26/12/2018
                            clsDefaultConfiguration.HierarchyforKOT4eachQuantity = dr("FLDVALUE").ToString
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "DeliveryCopyRequired".ToUpper() Then
                            clsDefaultConfiguration.IsDeliveryCopyRequired = dr("FLDVALUE").ToString
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "KotWiseKDS".ToUpper() Then   ' vipin 23.08.2018 
                            clsDefaultConfiguration.KotWiseKds = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "ISDineInKOTRequired".ToUpper() Then
                            clsDefaultConfiguration.ISDineInKOTRequired = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "ALLOWED_CSV_TERMINALS_FOR_UPDATE_SYNCH".ToUpper() Then   ' vipul 
                            clsDefaultConfiguration.ALLOWED_CSV_TERMINALS_FOR_UPDATE_SYNCH = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "NO_OF_BACK_DAYS_FOR_SYNC".ToUpper() Then   ' vipul 
                            clsDefaultConfiguration.NO_OF_BACK_DAYS_FOR_SYNC = dr("FLDVALUE")


                        ElseIf dr("FLDLABEL").ToString().ToUpper = "ArticleHierarchySynchronizer".ToUpper() Then   ' vipul 
                            clsDefaultConfiguration.ArticleHierarchySynchronizer = dr("FLDVALUE")

                        ElseIf dr("FLDLABEL").ToString().ToUpper = "ArticleSynchronizer".ToUpper() Then   ' vipul 
                            clsDefaultConfiguration.ArticleSynchronizer = dr("FLDVALUE")

                        ElseIf dr("FLDLABEL").ToString().ToUpper = "AuthenticationSynchronizer".ToUpper() Then   ' vipul 
                            clsDefaultConfiguration.AuthenticationSynchronizer = dr("FLDVALUE")

                        ElseIf dr("FLDLABEL").ToString().ToUpper = "BillingSynchronizer".ToUpper() Then   ' vipul 
                            clsDefaultConfiguration.BillingSynchronizer = dr("FLDVALUE")

                        ElseIf dr("FLDLABEL").ToString().ToUpper = "CsvFileSyncronizer".ToUpper() Then   ' vipul 
                            clsDefaultConfiguration.CsvFileSyncronizer = dr("FLDVALUE")

                        ElseIf dr("FLDLABEL").ToString().ToUpper = "IndependentMastersSynchronizer".ToUpper() Then   ' vipul 
                            clsDefaultConfiguration.IndependentMastersSynchronizer = dr("FLDVALUE")

                        ElseIf dr("FLDLABEL").ToString().ToUpper = "UomSynchronizer".ToUpper() Then   ' vipul 
                            clsDefaultConfiguration.UomSynchronizer = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "TransferHomeDeliveryandTakeAwayOrdertoKDS".ToUpper() Then
                            clsDefaultConfiguration.TransferHomeDeliveryandTakeAwayOrdertoKDS = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "TenderForCashDrawer".ToUpper() Then
                            clsDefaultConfiguration.TenderForCashDrawer = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "EnableHashTagLoyaltyPoint".ToUpper() Then
                            clsDefaultConfiguration.EnableHashTagLoyaltyPoint = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "HideControlsFromStockTake".ToUpper() Then
                            clsDefaultConfiguration.HideControlsFromStockTake = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper() = "ConversionFactorForHashtagLoyaltyPoint".ToUpper() Then
                            clsDefaultConfiguration.ConversionFactorForHashtagLoyaltyPoint = dr("FLDVALUE")
                        End If

                    Next
                ElseIf DocType = "SalesOrder" Then
                    For Each drChkDefault As DataRow In dt.Rows
                        'If drChkDefault("FldLabel").ToString.ToUpper() = "ArticleTaxAllowed".ToUpper() Then
                        '    _IsArticleTaxAllowed = drChkDefault("FldValue")
                        'Else
                        'ElseIf drChkDefault("FldLabel").ToString.ToUpper() = "CashRefundApplicable".ToUpper() Then
                        '    _IsCashRefundApplicable = drChkDefault("FldValue")
                        'ElseIf drChkDefault("FldLabel").ToString.ToUpper() = "CLPCardSwapeAllowed".ToUpper() Then
                        '    _IsCLPCardSwape = drChkDefault("FldValue")
                        'ElseIf drChkDefault("FldLabel").ToString.ToUpper() = "CLPMemberApplicable".ToUpper() Then
                        '    _IsCLPApplicable = drChkDefault("FldValue")
                        'ElseIf drChkDefault("FldLabel").ToString.ToUpper() = "CustomerTypeApplicable".ToUpper() Then
                        '    _IsCustomerTypeApplicable = drChkDefault("FldValue")
                        If drChkDefault("FldLabel").ToString.ToUpper() = "DeliveryDateApplicable".ToUpper() Then
                            _ChkDeliveryDate = drChkDefault("FldValue")
                        ElseIf drChkDefault("FldLabel").ToString.ToUpper() = "LessMinAdvAmountAllowed".ToUpper() Then
                            _IsLessMinAdvAmountAllowed = drChkDefault("FldValue")
                            'ElseIf drChkDefault("FldLabel").ToString.ToUpper() = "ManualCLPSearchAllowed".ToUpper() Then
                            '    _IsManualCLPSearchAllowed = drChkDefault("FldValue")
                        ElseIf drChkDefault("FldLabel").ToString.ToUpper() = "OtherChargesAllowed".ToUpper() Then
                            _IsOtherChargesAllowed = drChkDefault("FldValue")
                            'ElseIf drChkDefault("FldLabel").ToString.ToUpper() = "PrintingTaxInfoAllowed".ToUpper() Then
                            '    _IsPrintingTaxInfoAllowed = drChkDefault("FldValue")
                        ElseIf drChkDefault("FldLabel").ToString.ToUpper() = "PrintPreviewAllowed".ToUpper() And drChkDefault("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            _SOPrintPreviewAllowed = drChkDefault("FldValue")

                            'ElseIf drChkDefault("FldLabel").ToString.ToUpper() = "PromotionalMessage".ToUpper() Then
                            '    _IsPromotionalMessage = drChkDefault("FldValue")
                            'ElseIf drChkDefault("FldLabel").ToString.ToUpper() = "PromotionManually".ToUpper() Then
                            '    _IsPromotionManually = drChkDefault("FldValue")
                            'ElseIf drChkDefault("FldLabel").ToString.ToUpper() = "ReturnWithoutOldSOAllowed".ToUpper() Then
                            '    _IsReturnWithoutOldSOAllowed = drChkDefault("FldValue")

                        ElseIf drChkDefault("FldLabel").ToString.ToUpper() = "SaleAdvanceAllowed".ToString.ToUpper() Then
                            _IsSaleAdvanceAllowed = IIf(drChkDefault("FldValue").ToString() = String.Empty, 0, drChkDefault("FldValue"))
                            'ElseIf drChkDefault("FldLabel").ToString.ToUpper() = "SaleReturnAllowed".ToString.ToUpper() Then
                            '    _IsSaleReturnAllowed = drChkDefault("FldValue")
                        ElseIf drChkDefault("FldLabel").ToString.ToUpper() = "SalesPersonApplicable".ToString.ToUpper() Then
                            _IsSalesPersonApplicable = drChkDefault("FldValue")
                            'ElseIf drChkDefault("FldLabel").ToString.ToUpper() = "NegativeInventoryAllowed".ToString.ToUpper() Then
                            '    _IsNegativeInventoryAllowed = drChkDefault("FldValue")
                            'ElseIf drChkDefault("FldLabel").ToString.ToUpper() = "SOStorageLoc".ToString.ToUpper() Then
                            '    _SOStorageLoc = drChkDefault("FldValue")
                            'ElseIf drChkDefault("FldLabel").ToString.ToUpper() = "SOBillRoundOff".ToString.ToUpper() Then
                            '    _SOBillRoundOff = drChkDefault("FldValue")
                            'ElseIf drChkDefault("FldLabel").ToString.ToUpper() = "SORoundOffRequired".ToUpper() Then
                            '    _SORoundOffRequired = drChkDefault("FldValue")
                            'ElseIf drChkDefault("FLDLABEL").ToString().ToUpper = "PRINTINGTYPE" Then
                            '    clsDefaultConfiguration.PrintingType = drChkDefault("FLDVALUE").ToString
                        ElseIf drChkDefault("FLDLABEL").ToString().ToUpper = "SOOTHERCHARGESAMOUNTEDITABLE" And drChkDefault("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            _OtherCharges = True
                        ElseIf drChkDefault("FLDLABEL").ToString().ToUpper = "DECIMALSUPTO".ToUpper() Then
                            clsDefaultConfiguration.DecimalPlaces = drChkDefault("FLDVALUE").ToString
                            'ElseIf drChkDefault("FLDLABEL").ToString().ToUpper = "IsCstTaxRequired".ToUpper() Then
                            '    clsDefaultConfiguration.IsCstTaxRequired = drChkDefault("FLDVALUE")
                        ElseIf drChkDefault("FLDLABEL").ToString().ToUpper = "PRINTPRVIEWREQUIREDFORQUOTATION" Then
                            IsPreviewReqForQuotation = drChkDefault("FLDVALUE")

                        ElseIf drChkDefault("FLDLABEL").ToString().ToUpper = "PenaltyPercentageInClose".ToUpper() Then
                            _PenaltyPercentageInClose = drChkDefault("FLDVALUE")
                        ElseIf drChkDefault("FLDLABEL").ToString().ToUpper = "PenaltyPercentageInCancel".ToUpper() Then
                            _PenaltyPercentageInCancel = drChkDefault("FLDVALUE")
                        ElseIf drChkDefault("FLDLABEL").ToString().ToUpper = "NoofdaysAfterPenaltyApplicable".ToUpper() Then
                            _NoofdaysAfterPenaltyApplicable = drChkDefault("FLDVALUE")
                        ElseIf drChkDefault("FLDLABEL").ToString().ToUpper = "PackagingBoxLastNodeCode".ToUpper() Then
                            PackagingBoxLastNodeCode = drChkDefault("FLDVALUE")
                        ElseIf drChkDefault("FLDLABEL").ToString().ToUpper = "PackagingFieldsSO".ToUpper() Then
                            PackageFiedlsAllowed = drChkDefault("FLDVALUE")
                        ElseIf drChkDefault("FLDLABEL").ToString().ToUpper = "AllowCreditSaleWriteOff".ToUpper() Then
                            clsDefaultConfiguration.AllowCreditSaleWriteOff = drChkDefault("FLDVALUE").ToString
                        ElseIf drChkDefault("FLDLABEL").ToString().ToUpper = "SOBookingTills".ToUpper() Then
                            clsDefaultConfiguration.SOBookingScreenTills = drChkDefault("FLDVALUE").ToString
                        ElseIf drChkDefault("FLDLABEL").ToString().ToUpper = "PrintPreviewRequiredForSOBooking".ToUpper() Then
                            clsDefaultConfiguration.PrintPreviewRequiredForSOBooking = drChkDefault("FLDVALUE").ToString
                        ElseIf drChkDefault("FLDLABEL").ToString().ToUpper = "ColabaSOPrint".ToUpper() Then 'vipul
                            clsDefaultConfiguration.ColabaSOPrint = drChkDefault("FLDVALUE").ToString
                        End If

                        'If drChkDefault("FldLabel").ToString.ToUpper() = "SOSupplier".ToUpper() Then
                        '    _Supplier = drChkDefault("FldValue") 
                        'End If
                    Next
                ElseIf DocType = "DC" Then
                    For Each dr As DataRow In dt.Rows
                        If dr("FLDLABEL").ToString().ToUpper = "DC_Recipe_Enable".ToUpper() And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            DayCloseOtherScreens = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "BANK_TOTAL_CHECK_FO".ToUpper() And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                            BankTotalCheck = True
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "AUTORUNPATHAFTERDAYCLOSE" Then
                            AutoRunPathAfterDayClose = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "JKDAYCLOSEREPORT" Then
                            JKDayCloseReport = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "DAYCLOSEREPORTFORMAT" Then
                            DayCloseReportFormat = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "IsMailSend".ToUpper() Then
                            IsMailSend = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "DAYCLOSEEMAILNOTIFICATION" Then
                            DayCloseEmailNotifiaction = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "DAYCLOSEEMAILAMOUNTGOINGTOBANK" Then
                            DAYCLOSEEMAILAMOUNTGOINGTOBANK = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "ClientForMail".ToUpper() Then
                            ClientForMail = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "SyncOnDayClose".ToUpper() Then
                            clsDefaultConfiguration.SyncOnDayClose = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "AutoUpdateOnDayClose".ToUpper() Then
                            clsDefaultConfiguration.AutoUpdateonDayClose = dr("FLDVALUE")
                            '--------sms
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "SendDayCloseSMS".ToUpper() Then
                            clsDefaultConfiguration.SendDayCloseSMS = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "DayCloseRecipients".ToUpper() Then
                            clsDefaultConfiguration.DayCloseRecipients = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "DayCloseSMSFormat".ToUpper() Then
                            clsDefaultConfiguration.DayCloseSMSFormat = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "SbarroStockReportApplicable".ToUpper() Then
                            clsDefaultConfiguration.SbarroStockApplicable = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "stock_analysis_report_emails".ToUpper() Then
                            clsDefaultConfiguration.StockAnalysisReportEmails = dr("FLDVALUE")
                            '------ for jk
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "AutoPopulateQtyForAllDayCloseScreens".ToUpper() Then
                            clsDefaultConfiguration.AutoPopulateQtyForAllDayCloseScreens = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "UpdateStockOnDayCloseWastage".ToUpper() Then
                            clsDefaultConfiguration.UpdateStockOnDayCloseWastage = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "AllowDayCloseOffline".ToUpper() Then 'vipin
                            clsDefaultConfiguration._AllowDayCloseInOfflineMode = dr("FLDVALUE")
                        ElseIf dr("FLDLABEL").ToString().ToUpper = "DisplayBrandWiseSale".ToUpper() Then 'added by khusrao adil on 03-09-2018
                            clsDefaultConfiguration.DisplayBrandWiseSale = dr("FLDVALUE")
                        End If

                    Next
                    'ElseIf DocType = "Quotation" Then
                    '    For Each dr As DataRow In dt.Rows
                    '        If dr("FLDLABEL").ToString().ToUpper = "PrintPrviewRequiredForQuotation".ToUpper() Then
                    '            IsPreviewReqForQuotation = dr("FLDVALUE")
                    '        End If
                    '    Next
                ElseIf DocType = "Integration" Then
                    For Each drIn As DataRow In dt.Rows
                        If drIn("FLDLABEL").ToString().ToUpper = "ZomatoConfirmOrderAPI".ToUpper() Then
                            clsDefaultConfiguration.ZomatoConformOrderAPI = drIn("FLDVALUE")
                        ElseIf drIn("FLDLABEL").ToString().ToUpper = "ZomatoRejectOrderAPI".ToUpper() Then
                            clsDefaultConfiguration.ZomatoRejectOrderAPI = drIn("FLDVALUE")
                        ElseIf drIn("FLDLABEL").ToString().ToUpper = "EnableZomatoIntegration".ToUpper() Then
                            clsDefaultConfiguration.EnableZomatoIntegration = drIn("FLDVALUE")
                        ElseIf drIn("FLDLABEL").ToString().ToUpper = "ZomatoAPIKey".ToUpper() Then
                            clsDefaultConfiguration.ZomatoAPIKey = drIn("FLDVALUE")
                        ElseIf drIn("FLDLABEL").ToString().ToUpper = "xProviderId".ToUpper() Then
                            clsDefaultConfiguration.xProviderId = drIn("FLDVALUE")
                        ElseIf drIn("FLDLABEL").ToString().ToUpper = "EnablePhonePeIntegration".ToUpper() Then
                            clsDefaultConfiguration.EnablePhonePeIntegration = drIn("FLDVALUE")
                        ElseIf drIn("FLDLABEL").ToString().ToUpper = "PhonePeMerchantId".ToUpper() Then
                            clsDefaultConfiguration.PhonePeMerchantId = drIn("FLDVALUE")
                        ElseIf drIn("FLDLABEL").ToString().ToUpper = "PhonePeRequestPaymentUrl".ToUpper() Then
                            clsDefaultConfiguration.PhonePeRequestPaymentUrl = drIn("FLDVALUE")
                        ElseIf drIn("FLDLABEL").ToString().ToUpper = "PhonePeCheckPaymentUrl".ToUpper() Then
                            clsDefaultConfiguration.PhonePeCheckPaymentUrl = drIn("FLDVALUE")
                        ElseIf drIn("FLDLABEL").ToString().ToUpper = "PhonePeCancelPaymentUrl".ToUpper() Then
                            clsDefaultConfiguration.PhonePeCancelPaymentUrl = drIn("FLDVALUE")
                        ElseIf drIn("FLDLABEL").ToString().ToUpper = "PhonepeAuthKey".ToUpper() Then
                            clsDefaultConfiguration.PhonepeAuthKey = drIn("FLDVALUE")
                        ElseIf drIn("FLDLABEL").ToString().ToUpper = "PhonepeAuthIndex".ToUpper() Then
                            clsDefaultConfiguration.PhonepeAuthIndex = drIn("FLDVALUE")
                        ElseIf drIn("FLDLABEL").ToString().ToUpper = "OrderAcceptReadyInMinutes".ToUpper() Then
                            clsDefaultConfiguration.OrderAcceptReadyInMinutes = drIn("FLDVALUE")
                        ElseIf drIn("FLDLABEL").ToString().ToUpper = "UberGenerateTokenAPI".ToUpper() Then
                            clsDefaultConfiguration.UberGenerateTokenAPI = drIn("FLDVALUE")
                        ElseIf drIn("FLDLABEL").ToString().ToUpper = "UberOrderAcceptAPI".ToUpper() Then
                            clsDefaultConfiguration.UberOrderAcceptAPI = drIn("FLDVALUE")

                        End If


                    Next
                End If
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CLDC01"), "CLDC01 - " & getValueByKey("CLAE05"))
        End Try
    End Sub
    Public Sub New(ByVal DocumentType As String)
        DocType = DocumentType
    End Sub



End Class


