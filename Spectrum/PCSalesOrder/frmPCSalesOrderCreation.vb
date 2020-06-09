Imports System.Text
Imports System.IO
Imports SpectrumBL
Imports System.Resources 
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports SpectrumCommon
Imports System.ComponentModel
Imports C1.Win.C1FlexGrid
Imports Microsoft.Reporting.WinForms
Imports SpectrumPrint
Imports Spire.Pdf
Imports System.Drawing.Printing
Imports C1.Win.C1BarCode
Imports System.Net


Public Class frmPCSalesOrderCreation
    Inherits CtrlRbnBaseForm
    Dim clsVoucher As New SpectrumPrint.clsPrintVoucher
    'Sales order tax details 
    Dim dtSalesOrderTaxDetails As DataTable
    Protected controlList As New ArrayList
    Dim _pickupHistory As New DataSet
    Dim IsComboDoubleClicked As Boolean = False 'vipin
    Dim StrTaxInvoicePath As String = "" 'vipin  21.02.2016
    Public Enum PrintSOTransactionSet
        Status
        Payment
        DeliveryNote
        GiftVoucherDocumentPrint
        SOReturnStatus
    End Enum
    Public Property PrintSOTransaction() As PrintSOTransactionSet
        Get
            Return _PrintTransaction
        End Get
        Set(ByVal value As PrintSOTransactionSet)
            _PrintTransaction = value
        End Set
    End Property
    Public Shared _paymentTermId As String
    Public Shared Property PaymentTermId() As String
        Get
            Return _paymentTermId
        End Get
        Set(value As String)
            _paymentTermId = value
        End Set
    End Property
    Public Shared _lblArticleCombo As New DataTable
    Public Shared Property lblArticleCombo() As DataTable
        Get
            Return _lblArticleCombo
        End Get
        Set(value As DataTable)
            _lblArticleCombo = value
        End Set
    End Property
    Public Shared _ComboTax As New DataTable
    Public Shared Property ComboTax() As DataTable
        Get
            Return _ComboTax
        End Get
        Set(value As DataTable)
            _ComboTax = value
        End Set
    End Property
    Public Shared _DiscAmt As Double = 0
    Public Shared Property DiscAmt() As Double
        Get
            Return _DiscAmt
        End Get
        Set(value As Double)
            _DiscAmt = value
        End Set
    End Property
    Public Shared _QtyChange As Boolean = False
    Public Shared Property QtyChange() As Boolean
        Get
            Return _QtyChange
        End Get
        Set(value As Boolean)
            _QtyChange = value
        End Set
    End Property
    Private _IsCopied As Boolean = False '' $$ added on 18/08/17
    Public Property IsCopied() As Boolean
        Get
            Return _IsCopied
        End Get
        Set(ByVal value As Boolean)
            _IsCopied = value
        End Set
    End Property
#Region "Declare Varables"
    Dim dtSTRFactoryRemark As New DataTable
    Dim BarCodestring As String
    Dim ObjclsCommon As New clsCommon
    Private _PrintTransaction As PrintSOTransactionSet
    Dim objSoPc As New clsSalesOrderPC
    Dim Batchbarcode As List(Of SpectrumCommon.BtachbarcodeInfo)
    Dim CVoucherNo As String
    Dim _iArticleQtyBeforeChange As Double = 0
    Dim CVVoucherDay As Int32 = clsAdmin.CreditValidDays
    Dim CLPCardType, CLPCustomerId As String
    Dim vSiteCode As String = clsAdmin.SiteCode
    Dim vTerminalID As String = clsAdmin.TerminalID
    Dim vCurrencyDescription As String = clsAdmin.CurrencyDescription
    Dim vCurrencyCode As String = clsAdmin.CurrencyCode
    Dim vUserName As String = clsAdmin.UserName
    'Dim vfinancialYear As String = clsAdmin.Financialyear
    Dim vDateFormat As String = clsAdmin.SqlDBDateFormat
    Dim vPrinterSelection As Boolean = False
    Dim vPrintPaperType As String = String.Empty
    Dim vPrintLayoutselection As Boolean = False
    Dim vHeaderNote As Boolean = False
    Dim vFooterNote As Boolean = False
    Dim vResetTransNumbers As Boolean = False
    Dim vIsPrintingTaxInfoAllowed As Boolean = False
    Dim vIsPrintPreviewAllowed As Boolean = False
    Dim vIsPromotionalMessageAllowed As Boolean = False
    Dim vIsPrintOfficialAddressAllowed As Boolean = False
    Dim vCurrentDate As Date
    Dim vCurrentSODateTime As DateTime
    Dim consDeliveryDate As Date
    Dim vDocType As String
    Dim IsEditItem As Boolean = False
    Dim IsMRPOpen As Boolean = False
    Dim IsNewRow As Boolean = False
    Dim IsApplyPromotion As Boolean = False
    Dim IsSelectedPromotion As Boolean = False
    Dim IsDefaultPromotion As Boolean = False
    Dim vStmtQry As String = ""
    Dim vCustmCode As String = ""
    Dim vCardType As String = ""
    Dim vClpProgramId As String = clsAdmin.CLPProgram
    Dim CLPNo_ProgId_Point As String = ""
    Dim vAddressType As String = ""
    Dim vSalesInvcNo As String = ""
    Dim vArticleImagesCode As String = ""
    Dim vAuthUserId As String = String.Empty
    Dim vAuthUserRemarks As String = String.Empty
    Dim strImagesUrl As String = ""
    Dim vExclTaxAmt As Double = 0.0
    Dim vmDeliveredAmt As Double = 0.0
    Dim vMinAdvancePay As Double = 0.0
    Dim TotalSalesQty As Double = 0.0
    Dim NetArticleRate As Double = 0.0
    Dim StockQty As Double = 0
    Dim TotalPoints As Double = 0.0
    Private IsTenderCredit As Boolean = False
    Private IsTenderCash As Boolean = False
    Private IsTenderCheque As Boolean = False
    Private IsTenderCreditCard As Boolean = False
    Dim IsBtnSave As Boolean = True
    Dim IsRoundOffMsg As Boolean = False
    Dim IsRoundOfflabel As Boolean = False
    Dim dtUserAuth As DataTable
    Dim dtCashMemoDtls As DataTable
    Dim dtOrderAddresses As New DataTable
    Dim dtTempOrderAddresses As New DataTable
    Dim GridWidth As Integer = 0
    Dim GridHeight As Integer = 0
    Dim vRowIndex As Integer = 1
    Dim vtaxIndex As Integer = 1
    Dim vSrno As Integer = 0
    Dim vPackagingIndex As Integer = 1
    Dim vDeliveryIndex As Integer = 1
    Dim isCustSelected As Boolean = False
    Dim dsMain As New DataSet
    Dim dsMainCLP As New DataSet
    Dim IsUpdateSuccess As Boolean = True
    Dim dtItemSch As New DataTable
    Dim dtPrintingDetails As New DataTable
    Dim dtOtherCharges As New DataTable
    Dim dtSTR As New DataTable
    Dim dtSalesPerson As New DataTable
    Dim dtAddressType As New DataTable
    Dim dtPackagingBox As New DataTable
    Dim dtFactoryTbl As New DataSet
    Dim dtDocTaxes As New DataTable
    Dim dtArticleTaxes As New DataTable
    Dim dtStrResult As New DataSet
    Dim dvCurrentQty As DataView
    Dim dvAddressType As DataView
    Dim dvEditDeleteItems As DataView
    Dim dvEditDeleteSTRItems As DataView
    Dim dvEditDeletePackagingItems As DataView
    Dim dvEditTaxVariation As DataView
    Dim dvHeaderQty As DataView
    Dim dvEditDeletePackagingDeliveryItems As DataView
    Dim dvRemarks As DataView
    Dim dvDeleteTaxOnItem As DataView
    Dim dvDeleteTaxOnItemLevelTax As DataView
    Dim dtPackagingPrintBox As New DataTable
    Dim drHomeAdds As DataRow
    Dim drDelvAdds As DataRow

    Dim drAddItemExists() As DataRow
    Dim drItemSch As DataRow
    Dim drPackgVariation As DataRow
    Dim drDeliveryVariation As DataRow
    Dim drTax As DataRow
    Dim drTaxExist() As DataRow
    Dim drAddress As DataRow
    Dim drSearchItem As DataRow

    Dim objCM As New clsCashMemo
    Dim objComn As New clsCommon
    Dim objItemSch As New clsIteamSearch
    Dim objCustm As New clsCLPCustomer()

    Dim _dvDisplayItem As New DataView

    '----- temporary label----'
    Dim lblPickupQty As New Label
    Dim lblTotalItem As New Label
    Dim lblOrderQty As New Label
    'Dim lblPickupQty As New Label
    ' Dim CtrlCashSummary1.lbl5 As New Label
    Dim lblGrossAmt1 As New Label
    ' Dim lblDiscAmt As New Label
    'Dim lblOtherCharges As New Label

    'Dim lblNetAmount As New Label
    'Dim lblMinAdvancePaid As New Label
    'Dim lblAdvancePaid As New Label
    'Dim lblbalanceamt As New Label

    'Dim btnsonew As New Button
    'Dim btnsearchitem As New Button
    Dim btnSOSave As New Button
    'Dim BtnSOApplyPromotion As New Button
    Dim BtnSOAcceptPayment As New Button
    '----- temporary label ----'
    Dim dtMainTax As DataTable
    Dim defaultconfig As New clsDefaultConfiguration("SalesOrder")
    Dim IsCSTApplicable As Boolean = False
    Dim isInclusiveCalcReq As Boolean = False
    Private _dDueDate As Date
    Private _strRemarks As String
    Private DeliverySiteCode As String
    Private _QuotationOtherCharges As DataTable
    Dim isEditLoad As Boolean = False
    Dim SingleArticleCode As String
    Dim IsCombo As Boolean = False
    Dim DtSoBulkComboHdr As New DataTable
    Dim DtSoBulkComboDtl As New DataTable
    Dim DtSoBulkRemarks As New DataTable
    Dim IsSTRGenerate As Boolean = False
    Dim IsSOSaved As Boolean = False
    Dim vSalesOrderExpectedDeliveryDate As DateTime = DateTime.Now
    Dim SalesPersonName As String = ""
    Dim strSOStatus As String = ""
    Dim IsNewComboAdd As Boolean = False
    Const DeliveryBlinkFrequency As Int32 = 250
    Private deliveryTimer As New Timer()
    Dim DtSOStr As New DataTable
    Dim dtPackagingcopiedfrom As New DataTable

    Public Property QuotationOtherCharges() As DataTable
        Get
            Return _QuotationOtherCharges
        End Get
        Set(ByVal value As DataTable)
            _QuotationOtherCharges = value
        End Set
    End Property

    Public Property dvDisplayItem() As DataView
        Get
            Return _dvDisplayItem
        End Get
        Set(ByVal value As DataView)
            _dvDisplayItem = value
        End Set
    End Property
    Dim _strIndex As Integer = 1
    Public Property StrIndex() As Integer
        Get
            Return _strIndex
        End Get
        Set(ByVal value As Integer)
            _strIndex = value
        End Set
    End Property
    Dim _dsScan As New DataSet
    Public Property dsScan() As DataSet
        Get
            Return _dsScan
        End Get
        Set(ByVal value As DataSet)
            _dsScan = value
        End Set
    End Property
    Dim _dsPackagingVar As New DataSet
    Public Property dsPackagingVar() As DataSet
        Get
            Return _dsPackagingVar
        End Get
        Set(ByVal value As DataSet)
            _dsPackagingVar = value
        End Set
    End Property
    Dim _dsPackagingDelivery As New DataSet
    Public Property dsPackagingDelivery() As DataSet
        Get
            Return _dsPackagingDelivery
        End Get
        Set(ByVal value As DataSet)
            _dsPackagingDelivery = value
        End Set
    End Property
    Dim _dsPayment As New DataSet
    Public Property dsPayment() As DataSet
        Get
            Return _dsPayment
        End Get
        Set(ByVal value As DataSet)
            _dsPayment = value
        End Set
    End Property

    Dim _drSiteInfo As DataRow
    Public Property drSiteInfo() As DataRow
        Get
            Return _drSiteInfo
        End Get
        Set(ByVal value As DataRow)
            _drSiteInfo = value
        End Set
    End Property
    Private _customerNo As String
    Public Property CustomerNo() As String
        Get
            Return _customerNo
        End Get
        Set(ByVal value As String)
            _customerNo = value
        End Set
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

    Private _ISQuotationConversion As Boolean = False
    Public Property ISQuotationConversion() As Boolean
        Get
            Return _ISQuotationConversion
        End Get
        Set(ByVal value As Boolean)
            _ISQuotationConversion = value
            If value = False Then
                QuotationNumber = ""
                CustID = ""
                salesexecutive = ""
                QuotationOtherCharges = Nothing
            End If

        End Set
    End Property

    Private _QuotationNumber As String
    Public Property QuotationNumber() As String
        Get
            Return _QuotationNumber
        End Get
        Set(ByVal value As String)
            _QuotationNumber = value
        End Set
    End Property

    Private _CustID As String
    Public Property CustID() As String
        Get
            Return _CustID
        End Get
        Set(ByVal value As String)
            _CustID = value
        End Set
    End Property

    Private _salesexecutive As String
    Public Property salesexecutive() As String
        Get
            Return _salesexecutive
        End Get
        Set(ByVal value As String)
            _salesexecutive = value
        End Set
    End Property

    Dim IsFormClosing As Boolean = False

    Private _IsBooking As Boolean
    Public Property IsBooking() As Boolean
        Get
            Return _IsBooking
        End Get
        Set(ByVal value As Boolean)
            _IsBooking = value
        End Set
    End Property

#End Region

#Region "Load Sales Order Application"

    Private Sub frmSalesOrderCreation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            grdScanItem.AllowSorting = False  'vipin disable Grid sort 20.02.2018
            dgDeliveryLocation.AllowSorting = False

            '---- Set Tab Index START
            SetFormTabStop(Me, tabStopValue:=False)
            lblCompName.Text = ""
            CtrlLabel18.Text = ""
            CtrltxrCust.Text = ""
            grdScanItem.Rows.MinSize = 28
            dgDeliveryLocation.Rows.MinSize = 28
            'CtrldtOrderDt.Value.Format = "g"
            ' CtrldtOrderDt.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDateShortTime
            CtrldtOrderDt.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
            CtrldtOrderDt.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
            CtrldtOrderDt.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
            CtrldtOrderDt.DisplayFormat.CustomFormat = "dd-MM-yy HH:mm"
            CtrldtOrderDt.EditFormat.CustomFormat = " dd-MM-yy HH:mm"
            ctrlDtDeliveryDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
            ctrlDtDeliveryDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
            ctrlDtDeliveryDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
            ctrlDtDeliveryDate.DisplayFormat.CustomFormat = "dd-MM-yy HH:mm"
            ctrlDtDeliveryDate.EditFormat.CustomFormat = " dd-MM-yy HH:mm"

            vCurrentSODateTime = objComn.GetCurrentDate
            CtrldtOrderDt.Value = vCurrentSODateTime
            CtrldtOrderDt.ReadOnly = True
            Dim ctrTablIndex As New Dictionary(Of Object, Int16)

            '----bind copy from datatable
            dtPackagingcopiedfrom = objPCSO.CopyFromStructure()
            If dtPackagingcopiedfrom IsNot Nothing Then
                dtPackagingcopiedfrom.Clear()
            End If
            'ctrTablIndex.Add(CtrlSalesInfo1, 0)
            'ctrTablIndex.Add(CtrlSalesInfo1.CtrlDtExpDelDate, 0)
            'ctrTablIndex.Add(CtrlSalesInfo1.CtrlTxtCustOrdRef, 1)
            'ctrTablIndex.Add(CtrlSalesInfo1.CtrlTxtRemarks, 2)
            'ctrTablIndex.Add(CtrlSalesInfo1.CtrlTxtInvoice, 3)

            ' ctrTablIndex.Add(Me.tabSalesOrder, 1)
            'ctrTablIndex.Add(Me.TabPageOrderedItems, 1)
            'ctrTablIndex.Add(Me.c1SizerGrid, 1)
            ctrTablIndex.Add(Me.CtrlSalesPersons, 1)
            ctrTablIndex.Add(Me.CtrlSalesPersons.CtrlSalesPersons, 0)
            ctrTablIndex.Add(Me.CtrlSalesPersons.CtrlTxtBox, 1)
            ctrTablIndex.Add(Me.CtrlSalesPersons.CtrlCmdSearch, 2)

            ctrTablIndex.Add(Me.grdScanItem, 2)

            'ctrTablIndex.Add(Me.CtrlCustSearch1, 3)
            'ctrTablIndex.Add(Me.CtrlCustSearch1.rbOtherCust, 0)
            'ctrTablIndex.Add(Me.CtrlCustSearch1.rbCLPMember, 1)
            'ctrTablIndex.Add(Me.CtrlCustSearch1.CtrlBtn1, 2)
            'ctrTablIndex.Add(Me.CtrlCustSearch1.CtrlBtnNew, 3)
            'ctrTablIndex.Add(Me.CtrlCustSearch1.CtrlTxtCustNo, 4)
            'ctrTablIndex.Add(Me.CtrlCustSearch1.CtrlTxtSwapeCard, 5)

            '  ctrTablIndex.Add(Me.C1Sizer2, 4)
            ctrTablIndex.Add(Me.CtrlBtnStockCheck, 4)
            ctrTablIndex.Add(Me.CtrlbtnSOOtherCharges, 5)
            ctrTablIndex.Add(Me.CtrlBtnSearchCLP, 6)
            ' ctrTablIndex.Add(Me.BtnSelectDeliveryLoc, 7)

            SetFormTabIndex(ctrTablIndex:=ctrTablIndex)
            Me.grdScanItem.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.None
            'Me.tabSalesOrder.TabStop = False
            'Me.TabPageOrderedItems.TabStop = False
            'Me.TabPageDeliveryLocation.TabStop = False
            'c1SizerGrid.TabStop = False
            ' C1Sizer2.TabStop = False
            '---- Set Tab Index END 
            lblStrRaised.Text = ""
            rbnGrpCST.Text = getValueByKey("CST003")
            rbnCST.Text = getValueByKey("CST004")
            ' BtnSelectDeliveryLoc.Image = My.Resources.Select_Delivery_Location
            ' BtnSelectDeliveryLoc.Text = getValueByKey("frmnsalesordercreation.BtnSelectDeliveryLoc")
            rbnCST.LargeImage = My.Resources.ApplyCSTTax

            AddHandler CtrlSalesPersons.CtrlTxtBox.PreviewKeyDown, AddressOf txtSearchItem_PreviewKeyDown
            AddHandler CtrlSalesPersons.CtrlTxtBox.Leave, AddressOf txtSearchItem_Leave
            AddHandler CtrlSalesPersons.CtrlCmdSearch.Click, AddressOf BtnSearchItem_Click
            'add for Wild Search
            AddHandler CtrlSalesPersons.CtrlTxtBox.TextChanged, AddressOf txtSearchItem_textchange
            '---AndroidSearchTextBox_Leave commeted by aJAY 
            'AddHandler CtrlSalesPersons.AndroidSearchTextBox.Leave, AddressOf AndroidSearchTextBox_Leave
            ' AddHandler CtrlSalesPersons.AndroidSearchTextBox.PreviewKeyDown, AddressOf AndroidSearchTextBox_PreviewKeyDown
            AddHandler CtrlSalesPersons.AndroidSearchTextBox.TextChanged, AddressOf AndroidSearchTextBox_Textchange

            'AddHandler CtrlSalesInfo1.CtrlDtExpDelDate.Leave, AddressOf dtpExpDeliveryDate_Leave
            'AddHandler CtrlSalesInfo1.CtrlDtExpDelDate.Calendar.DateValueSelected, AddressOf dtpExpDeliveryDate_Leave

            AddHandler CtrlbtnSOOtherCharges.Click, AddressOf BtnAddOtherCharges_Click
            ' this uses othercharge form , 

            AddHandler rbbtnSelectPromo.Click, AddressOf rbbtnDefaultPromo_Click
            AddHandler rbbtnSave.Click, AddressOf BtnSaveSalesOrder_Click
            AddHandler rbBtnAddCombo.Click, AddressOf rbBtnAddCombo_Click

            AddHandler rbbtnPrint.Click, AddressOf BtnSOPrint_Click
            AddHandler CtrlBtnStockCheck.Click, AddressOf BtnStockCheck_Click

            Call SetScreenAsBooking()

            AddHandler grdScanItem.StartEdit, AddressOf grdScanItem_StartEdit
            'AddHandler dgDeliveryLocation.StartEdit, AddressOf grdScanItem_StartEdit


            ''' delivery blinker start
            AddHandler deliveryTimer.Tick, AddressOf timer_Tick
            deliveryTimer.Interval = DeliveryBlinkFrequency
            CtrlLblDelivery.Visible = True
            deliveryTimer.Start()
            ''' delivery blinker end

            dtOrderAddresses = objComn.GetSOAddresses("", "", False)
            dtTempOrderAddresses = dtOrderAddresses.Copy
            dtPackagingPrintBox = objComn.GetPackagingBox(clsAdmin.SiteCode, 2)

            'CtrlCustDtls1.cboAddrType.DropMode = 1
            'CtrlCustSearch1.rbCLPMember.Checked = True
            'CtrlCustSearch1.rbOtherCust.Visible = clsDefaultConfiguration.IsOtherCustomerRequired

            'AddHandler CtrlCustSearch1.CustomerChanged, AddressOf CustomerSearch_Completed
            Dim objdefault As New clsDefaultConfiguration("SalesOrder")
            objdefault.GetDefaultSettings()
            dsMain = objPCSO.GetSOTableStruct(vSiteCode, 0, , ISQuotationConversion, QuotationNumber)

            objPCSO.GetSODefaultConfig(vSiteCode)
            dtFactoryTbl = objPCSO.GetFactoryNodeCodes()
            dtArticleTaxes = objPCSO.GetAllTaxesAppliedToSiteArticleLevel(clsAdmin.SiteCode, "SO201")
            dtPrintingDetails = objPCSO.GetPrintingDetail
            rbbtnSONew.Tag = "NEW"
            If clsDefaultConfiguration.IsOtherChargesAllowed = False Then
                CtrlbtnSOOtherCharges.Visible = False
            Else
                CtrlbtnSOOtherCharges.Visible = True
            End If
            vIsPrintPreviewAllowed = clsDefaultConfiguration.SOPrintPreviewAllowed

            vDocType = objPCSO.SOCreation
            vCurrentDate = objComn.GetCurrentDate

            _dsScan = objPCSO.GetCollectionOfItems
            _dsPackagingVar = objPCSO.GetCollectionOfPackagingMaterial

            If Not (dsScan Is Nothing) Then
                _dsScan.Clear()
            End If
            If Not (_dsPackagingVar Is Nothing) Then
                _dsPackagingVar.Clear()
            End If
            _dsPackagingDelivery = objPCSO.GetCollectionOfPackagingDelivery
            If Not (_dsPackagingDelivery Is Nothing) Then
                _dsPackagingDelivery.Clear()
            End If

            grdScanItem.Rows.RemoveRange(1, grdScanItem.Rows.Count - 1)
            'CtrlCustSearch1.CustmType = "CLP"
            RefreshScanData(dsScan)

            DtSoBulkRemarks = objPCSO.RmearksStructure()
            If Not (DtSoBulkRemarks Is Nothing) Then
                DtSoBulkRemarks.Clear()
            End If

            'CtrlSalesInfo1.CtrldtOrderDt.Value = vCurrentDate
            'CtrlSalesInfo1.CtrldtOrderDt.ReadOnly = True

            consDeliveryDate = vCurrentDate.AddDays(clsDefaultConfiguration.ChkDeliveryDate)
            'dtpExpDeliveryDate.DisplayFormat.CustomFormat = vDateFormat
            'dtpExpDeliveryDate.EditFormat.CustomFormat = vDateFormat
            'CtrlSalesInfo1.CtrlDtExpDelDate.Value = vCurrentDate.AddDays(clsDefaultConfiguration.ChkDeliveryDate)
            DeliverySiteCode = clsAdmin.SiteCode
            grdScanItem_Resize(sender, New System.EventArgs)

            If Not ISQuotationConversion Then
                BtnSONew_Click(sender, e)
            Else
                GetNewSalesOrderNumber()
            End If


            'CtrlProductImage.ClearImage()
            'CtrlProductImage.ShowArticleImage("xx")

            'PSetDefaultCurrencyOfCashMemoSummary(CtrlCashSummary1)
            PrintSetProperty()
            dgDeliveryLocation.AutoGenerateColumns = False
            'dgDeliveryLocation.DataSource = SoDeliveryInfo
            objCM.DecimalDigits = clsDefaultConfiguration.DecimalPlaces
            defaultconfig.GetDefaultSettings()
            If IsBooking = False Then
                If defaultconfig.IsCstTaxRequired Then
                    rbnGrpCST.Visible = True
                Else
                    rbnGrpCST.Visible = False
                End If
            End If


            If ISQuotationConversion Then
                SetQuotationArticles()
            End If

            'code added by Mahesh for add bulk combo 
            Call objPCSO.GetSOBulkComboTablestructure(DtSoBulkComboHdr, DtSoBulkComboDtl)
            Call EnableDiableTenderIcons()
            CtrlRbn1.DbtnPayNEFT.Enabled = True '' addded by nikhil for PC
            CtrlRbn1.DbtnPayRTGS.Enabled = True
            If clsDefaultConfiguration.IsSavoy Then
                IsTenderCredit = False
            End If
            '---------PcRoundoff
            If IsBooking = False Then
                RibbonGroup2.Visible = clsDefaultConfiguration.PCRoundOff
            End If

            'CtrlSalesInfo1.CtrlTxtOrderNo.Enabled = False
            'CtrlSalesInfo1.CtrldtOrderDt.Enabled = False
            DeliveryGridSetting()
            rdDelNo.Checked = True
            dgDeliveryLocation.Cols("DeliveryDate").AllowEditing = False
            dgDeliveryLocation.Cols("DeliveryTime").AllowEditing = False
            ctrlDtDeliveryDate.Visible = True
            isEditLoad = True
            ctrlDtDeliveryDate.Value = vCurrentSODateTime
            isEditLoad = False
            '----05042016 check user authorization for so booking new and edit -sagar
            'If CheckAuthorisationForTran(clsAdmin.SiteCode, "SoBooking") = True AndAlso clsDefaultConfiguration.ISAllowSOBooking = True AndAlso clsDefaultConfiguration.IsNewSalesOrder = True Then
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "SoBooking") = True AndAlso CheckAuthorisation(clsAdmin.UserCode, "SoBooking") AndAlso clsDefaultConfiguration.ISAllowSOBooking = True AndAlso clsDefaultConfiguration.IsNewSalesOrder = True Then
                rbbtnSOBooking.Visible = True
            Else
                rbbtnSOBooking.Visible = False
            End If
            'If CheckAuthorisationForTran(clsAdmin.SiteCode, "SoBookingEdit") = True AndAlso clsDefaultConfiguration.ISAllowSOBooking = True AndAlso clsDefaultConfiguration.IsNewSalesOrder = True Then
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "SoBookingEdit") = True AndAlso CheckAuthorisation(clsAdmin.UserCode, "SoBookingEdit") AndAlso clsDefaultConfiguration.ISAllowSOBooking = True AndAlso clsDefaultConfiguration.IsNewSalesOrder = True Then
                rbbtnSOBookingEdit.Visible = True
            Else
                rbbtnSOBookingEdit.Visible = False
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
        SetCulture(Me, Me.Name, CtrlRbn1)

        'CtrlSalesPersons.CtrlTxtBox.Select()
        CtrlSalesPersons.AndroidSearchTextBox.Select()
        Me.Select()
        Dim condition As String
        Dim objItem As New clsIteamSearch
        condition = " AND A.ArticalTypeCode<>'Combo' AND A.ArticleCode <>'" & clsDefaultConfiguration.GVBaseArticle & "' AND A.ArticleCode <>'" & clsDefaultConfiguration.ClpBaseArticle & "' AND A.ArticleCode <>'" & clsAdmin.CVBaseArticle & "'"
        Dim dtBind = objItem.GetEANData(clsAdmin.SiteCode, "", clsAdmin.LangCode, condition)
        If dtBind.Rows.Count > 1 Then
            'Dim listSource As List(Of [String]) = (From row In dtBind Select Convert.ToString(row("ArticleCode")) + " " + Convert.ToString(row("Discription"))).Distinct().ToList()
            'CtrlSalesPersons.AndroidSearchTextBox.lstNames = listSource
            Call SetWildSearchTextBox(dtBind, CtrlSalesPersons.AndroidSearchTextBox, key:="ArticleCode", Value:="Discription", searchData:="ArticleCodeDesc")
        End If
        dtSTRFactoryRemark = objPCSO.GetFactoryRemarks(CtrlTxtOrderNo.Text)
        'BtnSONew_Click(sender, e)
        'added by sagar for screen resoltn issue
        Me.tabSalesOrder.Size = New System.Drawing.Size(My.Computer.Screen.WorkingArea.Width - 10, My.Computer.Screen.WorkingArea.Height - 410)
        If clsDefaultConfiguration.IsSavoy Then
            DisablebuttonsForSavoy()
        End If
        'code added by vipul on 29-08-2017 for increase the lenght of dis percent label
        CtrllblDiscPerc.MinimumSize = New Size(80, 18)
        Me.CtrllblDiscPerc.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    End Sub
    '' added by ketan Validation for SO Number at time of SAVE
    Private Function ValidationForSONumber(ByVal SONumber As String) As Boolean
        Dim No As String = SONumber
        If No <> "" Then
            Dim arr As String() = Split(No, "-")
            'arr(2) = ""
            If arr(2) = "" Then
                ShowMessage("Due to poor network connectivity, system could not save the Sales Order. Please try saving the sales order again.", getValueByKey("CLAE05"))
                Return False
            End If
            Return True
        End If
        Return True
    End Function
    Private Sub SetScreenAsBooking()
        Try
            RemoveHandler CtrlRbn1.DbtnPay.Click, AddressOf BtnAcceptPayment_Click
            RemoveHandler CtrlRbn1.DbtnPayCard.Click, AddressOf BtnPayCard_Click
            RemoveHandler CtrlRbn1.DbtnPayCash.Click, AddressOf BtnPayCash_Click
            RemoveHandler CtrlRbn1.DbtnpayCheque.Click, AddressOf BtnPayCheque_Click
            RemoveHandler CtrlRbn1.DbtnPayNEFT.Click, AddressOf BtnPayNEFT_Click   '' added by nikhil
            RemoveHandler CtrlRbn1.DbtnPayRTGS.Click, AddressOf BtnPayRTGS_Click   '' added by nikhil
        Catch ex As Exception
            LogException(ex)
        End Try

        If IsBooking = True Then
            CtrlRbn1.Tabs(1).Groups("grpPayments").Visible = False
            rbbtnSONew.Enabled = False
            rbbtnSOEdit.Enabled = False
            rbbtnSOCancel.Enabled = False
            rbnGrpCMPromotion.Visible = False
            RibbonGroup2.Visible = False
            rbnGrpCST.Visible = False
            Me.Text = "SO Booking Screen"
        Else
            CtrlRbn1.Tabs(1).Groups("grpPayments").Visible = True
            rbnGrpCMPromotion.Visible = True
            RibbonGroup2.Visible = True
            rbnGrpCST.Visible = True
            Me.Text = "Create Sales Order"
            AddHandler CtrlRbn1.DbtnPay.Click, AddressOf BtnAcceptPayment_Click
            AddHandler CtrlRbn1.DbtnPayCard.Click, AddressOf BtnPayCard_Click
            AddHandler CtrlRbn1.DbtnPayCash.Click, AddressOf BtnPayCash_Click
            AddHandler CtrlRbn1.DbtnpayCheque.Click, AddressOf BtnPayCheque_Click
            AddHandler CtrlRbn1.DbtnPayNEFT.Click, AddressOf BtnPayNEFT_Click   '' added by nikhil
            AddHandler CtrlRbn1.DbtnPayRTGS.Click, AddressOf BtnPayRTGS_Click   '' added by nikhil
        End If
    End Sub

    Private Sub frmSalesOrderCreation_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F3 Then
                'BtnSearchCustm_Click(sender, New System.EventArgs)

                'ElseIf e.KeyCode = Keys.F11 Then
                ' this comment because now the default promotion is called my CTL+D key
                '    rbbtnDefaultPromo_Click(sender, New System.EventArgs)
            ElseIf e.KeyCode = Keys.F7 Then
                BtnStockCheck_Click(sender, New System.EventArgs)
            ElseIf e.KeyCode = Keys.F12 Then
                'BtnAcceptPayment_Click(sender, New System.EventArgs)
                '-----Added By Prasad For Checking if Header then Call Price Change else no
                If grdScanItem.Item(grdScanItem.Row, "IsHeader") = True AndAlso grdScanItem.Item(grdScanItem.Row, "ArticleType") = "Single" Then
                    PriceChange()
                ElseIf grdScanItem.Item(grdScanItem.Row, "IsHeader") = True AndAlso grdScanItem.Item(grdScanItem.Row, "ArticleType") = "Combo" Then
                    ShowMessage("Price cannot be changed for combo articles.", "BOC001 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
                'ElseIf e.KeyCode = Keys.F9 Then
                '    'cmdCash_Click(sender, New System.EventArgs)
                'ElseIf e.KeyCode = Keys.F8 Then
                '    'cmdCard_Click(sender, New System.EventArgs)
            ElseIf e.Alt And e.KeyCode = Keys.S Then
                BtnSaveSalesOrder_Click(Nothing, New System.EventArgs)
            ElseIf e.KeyCode = Keys.F8 And IsTenderCredit Then
                If Not IsBooking = True Then
                    CreditSales(Nothing, New System.EventArgs)
                End If
            ElseIf e.KeyCode = Keys.F9 Then
                If clsDefaultConfiguration.IsNewCreditSale Then
                    Dim objCreditSales As New frmNCreditSalesNew(False)
                    objCreditSales.ShowDialog()
                Else
                    Dim objCreditSales As New frmNCreditSales(False)
                    objCreditSales.ShowDialog()
                End If
                ElseIf e.KeyCode = Keys.F1 Then
                    Dim objClsCommon As New clsCommon
                    objClsCommon.DisplayHelpFile(ParentForm, "create-sales-order.htm")
                End If
        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub

    Private Sub frmNSalesOrderCreation_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If grdScanItem.Rows.Count > 1 AndAlso Not IsFormClosing Then
                If MsgBox(getValueByKey("SO047"), MsgBoxStyle.YesNo, "SO047 - " & getValueByKey("CLAE04")) = MsgBoxResult.No Then
                    e.Cancel = True
                Else
                    If CtrlSalesPersons.AndroidSearchTextBox.IsListBoxVisible Then
                        CtrlSalesPersons.AndroidSearchTextBox.ResetListBox()
                    End If
                End If
            Else
                If CtrlSalesPersons.AndroidSearchTextBox.IsListBoxVisible Then
                    CtrlSalesPersons.AndroidSearchTextBox.ResetListBox()
                End If
            End If

        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub

    ''' <summary>
    ''' Get the Site default Settings And Set Default Config Object
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub CustomerSearch_Completed(ByVal Customer As CLPCustomerDTO)

        ' If Customer.AddressList IsNot Nothing AndAlso Customer.AddressList.Count > 0 Then
        ' If clsDefaultConfiguration.IsCstTaxRequired AndAlso objCustm.CheckIfCstApplicable(clsAdmin.SiteCode, Customer.AddressList.FirstOrDefault().State) Then

        'End If

        '    Else
        '        IsCSTApplicable = False
        '        ResetTax(False)
        '    End If



    End Sub
    Private Sub ResetDocTax(ByVal considerCst As Boolean, ByVal dtDocTAxes As DataTable)

        dvDeleteTaxOnItem = New DataView(dsMain.Tables("SalesOrderTaxDtls"), "IsDocumentLevel=True", "", DataViewRowState.CurrentRows)
        vtaxIndex = dsMain.Tables("SalesOrderTaxDtls").Compute("MAX(taxLineNo)", "") + 1
        If dvDeleteTaxOnItem.Count > 0 Then
            ' If IsHeader Then
            dvDeleteTaxOnItem.AllowDelete = True
            For Each dr As DataRowView In dvDeleteTaxOnItem
                dr.Delete()
            Next
            dtDocTAxes.Rows.Clear()
            dsMain.Tables("SalesOrderTaxDtls").AcceptChanges()
        End If
        'vtaxIndex=
        'Dim foundRow As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("IsDocumentLevel = True")
        'If foundRow.Count > 0 Then


        '    For Each row As DataRow In foundRow
        '        row.Delete()
        '    Next
        '    'dsMain.Tables("SalesOrderTaxDtls").AcceptChanges()
        '    vtaxIndex = dsMain.Tables("SalesOrderTaxDtls").Rows.Count + 1
        '    For drindex = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1
        '        Dim vnewNetAmt As Decimal = 0.0
        '        Dim vnewExclTaxAmt As Decimal = 0.0
        '        Dim vnewIncTaxAmt As Decimal = 0.0
        '        Dim vnewTotalTaxAmt As Decimal = 0.0
        '        Dim vnewNetAmount As Decimal = 0.0
        '        Dim vnewSellingPrice As Decimal = 0.0
        '        Dim vnewGrossAmt As Decimal = 0.0
        '        vnewNetAmt = Math.Round(_dsScan.Tables("ItemScanDetails").Rows(drindex)("SellingPrice") * _dsScan.Tables("ItemScanDetails").Rows(drindex)("Quantity"), 3)
        '        _dsScan.Tables("ItemScanDetails").Rows(drindex)("ExclTaxAmt") = vnewExclTaxAmt
        '        _dsScan.Tables("ItemScanDetails").Rows(drindex)("IncTaxAmt") = vnewIncTaxAmt
        '        _dsScan.Tables("ItemScanDetails").Rows(drindex)("TotalTaxAmt") = vnewTotalTaxAmt

        '        _dsScan.Tables("ItemScanDetails").Rows(drindex)("NetAmount") = vnewNetAmt
        '        _dsScan.Tables("ItemScanDetails").Rows(drindex)("SellingPrice") = _dsScan.Tables("ItemScanDetails").Rows(drindex)("SellingPrice")
        '        _dsScan.Tables("ItemScanDetails").Rows(drindex)("GrossAmt") = vnewNetAmt

        '        Dim dvDtl As New DataView
        '        dvDtl = New DataView(dsMain.Tables("SalesOrderTaxDtls"), "EAN='" & _dsScan.Tables("ItemScanDetails").Rows(drindex)("EAN").ToString() & "'", "", DataViewRowState.CurrentRows)

        '        For Each drView As DataRowView In dvDtl
        '            Dim result As DataRow() = dtArticleTaxes.Select("TaxCode='" + drView("TaxLabel").ToString() + "' ")
        '            If result.Length > 0 Then
        '                If result(0)("Inclusive") Then
        '                    _dsScan.Tables("ItemScanDetails").Rows(drindex)("IncTaxAmt") = drView("TaxValue")
        '                End If

        '                Dim resultsOt As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("EAN='" + _dsScan.Tables("ItemScanDetails").Rows(drindex)("EAN") + "' and RowIndex='" + _dsScan.Tables("ItemScanDetails").Rows(drindex)("RowIndex") + "' ")
        '                Dim taxVal As Decimal
        '                If resultsOt.Length > 0 Then
        '                    Dim itrQtyRow As DataRow() = dsPackagingVar.Tables(0).Select("EAN='" & _dsScan.Tables("ItemScanDetails").Rows(drindex)("EAN") & "' And RowIndex='" + _dsScan.Tables("ItemScanDetails").Rows(drindex)("RowIndex") + "'  and PackagingIndex <> '" + _dsScan.Tables("ItemScanDetails").Rows(drindex)("PackagingIndex").ToString() + "' ")

        '                    Dim QtyOthers As Object
        '                    Dim QtyExcAmt As Object
        '                    If itrQtyRow.Length > 0 Then
        '                        QtyOthers = dsPackagingVar.Tables(0).Compute("Sum(Quantity)", "EAN='" & _dsScan.Tables("ItemScanDetails").Rows(drindex)("EAN") & "' And RowIndex='" + _dsScan.Tables("ItemScanDetails").Rows(drindex)("RowIndex") + "'   ")

        '                    End If
        '                    If QtyOthers > 0 Then
        '                        taxVal = (drView("TaxValue") / QtyOthers) * _dsScan.Tables("ItemScanDetails").Rows(drindex)("Quantity")
        '                    End If
        '                End If

        '                If result(0)("Inclusive") = False Then
        '                    _dsScan.Tables("ItemScanDetails").Rows(drindex)("ExclTaxAmt") = taxVal
        '                End If

        '                vnewTotalTaxAmt = vnewTotalTaxAmt + taxVal
        '                _dsScan.Tables("ItemScanDetails").Rows(drindex)("TotalTaxAmt") = vnewTotalTaxAmt

        '                _dsScan.Tables("ItemScanDetails").Rows(drindex)("NetAmount") = vnewNetAmt + vnewTotalTaxAmt
        '                _dsScan.Tables("ItemScanDetails").Rows(drindex)("SellingPrice") = _dsScan.Tables("ItemScanDetails").Rows(drindex)("SellingPrice")
        '                _dsScan.Tables("ItemScanDetails").Rows(drindex)("GrossAmt") = vnewNetAmt
        '            End If
        '        Next

        '        _dsScan.Tables("ItemScanDetails").AcceptChanges()

        '    Next

        '    For drindex = 0 To _dsPackagingVar.Tables("PackagingMaterial").Rows.Count - 1
        '        Dim vnewNetAmt As Decimal = 0.0
        '        Dim vnewExclTaxAmt As Decimal = 0.0
        '        Dim vnewIncTaxAmt As Decimal = 0.0
        '        Dim vnewTotalTaxAmt As Decimal = 0.0
        '        Dim vnewNetAmount As Decimal = 0.0
        '        Dim vnewSellingPrice As Decimal = 0.0
        '        Dim vnewGrossAmt As Decimal = 0.0
        '        vnewNetAmt = Math.Round(_dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("SellingPrice") * _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("Quantity"), 3)
        '        _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("ExclTaxAmt") = vnewExclTaxAmt
        '        _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("IncTaxAmt") = vnewIncTaxAmt
        '        _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("TotalTaxAmt") = vnewTotalTaxAmt

        '        _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("NetAmount") = vnewNetAmt
        '        _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("SellingPrice") = _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("SellingPrice")
        '        _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("GrossAmt") = vnewNetAmt

        '        Dim dvDtl As New DataView
        '        dvDtl = New DataView(dsMain.Tables("SalesOrderTaxDtls"), "EAN='" & _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("EAN").ToString() & "'", "", DataViewRowState.CurrentRows)

        '        For Each drView As DataRowView In dvDtl
        '            Dim result As DataRow() = dtArticleTaxes.Select("TaxCode='" + drView("TaxLabel").ToString() + "' ")
        '            If result.Length > 0 Then
        '                If result(0)("Inclusive") Then
        '                    _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("IncTaxAmt") = drView("TaxValue")
        '                End If
        '                Dim resultsOt As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("EAN='" + dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("EAN") + "' and RowIndex='" + dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("RowIndex") + "'")
        '                Dim taxVal As Decimal
        '                If resultsOt.Length > 0 Then
        '                    Dim itrQtyRow As DataRow() = dsPackagingVar.Tables(0).Select("EAN='" & dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("EAN") & "' And RowIndex='" + dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("RowIndex") + "'  and PackagingIndex <> '" + dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("PackagingIndex").ToString() + "' ")

        '                    Dim QtyOthers As Object
        '                    Dim QtyExcAmt As Object
        '                    If itrQtyRow.Length > 0 Then
        '                        QtyOthers = dsPackagingVar.Tables(0).Compute("Sum(Quantity)", "EAN='" & dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("EAN") & "' And RowIndex='" + dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("RowIndex") + "'   ")

        '                    End If
        '                    If QtyOthers > 0 Then
        '                        taxVal = (drView("TaxValue") / QtyOthers) * dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("Quantity")
        '                    End If
        '                End If

        '                If result(0)("Inclusive") = False Then
        '                    _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("ExclTaxAmt") = taxVal
        '                End If

        '                vnewTotalTaxAmt = vnewTotalTaxAmt + taxVal
        '                _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("TotalTaxAmt") = vnewTotalTaxAmt

        '                _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("NetAmount") = vnewNetAmt + vnewTotalTaxAmt
        '                _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("SellingPrice") = dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("SellingPrice")
        '                _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("GrossAmt") = vnewNetAmt
        '            End If




        '        Next


        '        _dsPackagingVar.Tables("PackagingMaterial").AcceptChanges()
        '    Next
        '    _dsScan.AcceptChanges()
        '    _dsPackagingVar.AcceptChanges()
        '    dsMain.Tables("SalesOrderTaxDtls").AcceptChanges()

        'End If
    End Sub
    ''' <summary>
    ''' This Method is written for apply extra on Items --article level tax with vat,service,govt,etc..
    ''' </summary>
    ''' <param name="considerCst"></param>
    ''' <remarks></remarks>
    Private Sub ResetNewTax(ByVal considerCst As Boolean, ByVal dtDocTAxes As DataTable)
        'Tax Reset
        'For drindex = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1
        '    Dim dt = objCM.getTax(vSiteCode, String.Empty, "SO201", _dsScan.Tables("ItemScanDetails").Rows(drindex)("Quantity"), _dsScan.Tables("ItemScanDetails").Rows(drindex)("EAN"), clsDefaultConfiguration.CSTTaxCode, considerCst)
        '    If dt Is Nothing OrElse Not dt.Rows.Count > 0 Then
        '        Exit Sub
        '    End If

        'Next
        ''start: reset tax with article level only

        Dim foundRow As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("IsDocumentLevel = True")
        If foundRow.Count > 0 Then


            For Each row As DataRow In foundRow
                row.Delete()
            Next
            dsMain.Tables("SalesOrderTaxDtls").AcceptChanges()
            vtaxIndex = dsMain.Tables("SalesOrderTaxDtls").Rows.Count + 1
            'For drindex = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1
            '    Dim vnewNetAmt As Decimal = 0.0
            '    Dim vnewExclTaxAmt As Decimal = 0.0
            '    Dim vnewIncTaxAmt As Decimal = 0.0
            '    Dim vnewTotalTaxAmt As Decimal = 0.0
            '    Dim vnewNetAmount As Decimal = 0.0
            '    Dim vnewSellingPrice As Decimal = 0.0
            '    Dim vnewGrossAmt As Decimal = 0.0
            '    vnewNetAmt = Math.Round(_dsScan.Tables("ItemScanDetails").Rows(drindex)("SellingPrice") * _dsScan.Tables("ItemScanDetails").Rows(drindex)("Quantity"), 3)
            '    _dsScan.Tables("ItemScanDetails").Rows(drindex)("ExclTaxAmt") = vnewExclTaxAmt
            '    _dsScan.Tables("ItemScanDetails").Rows(drindex)("IncTaxAmt") = vnewIncTaxAmt
            '    _dsScan.Tables("ItemScanDetails").Rows(drindex)("TotalTaxAmt") = vnewTotalTaxAmt

            '    ' _dsScan.Tables("ItemScanDetails").Rows(drindex)("NetAmount") = vnewNetAmt
            '    _dsScan.Tables("ItemScanDetails").Rows(drindex)("NetAmount") = vnewNetAmt - IIf(_dsScan.Tables("ItemScanDetails").Rows(drindex)("Discount") Is DBNull.Value, 0, _dsScan.Tables("ItemScanDetails").Rows(drindex)("Discount"))
            '    _dsScan.Tables("ItemScanDetails").Rows(drindex)("SellingPrice") = _dsScan.Tables("ItemScanDetails").Rows(drindex)("SellingPrice")
            '    _dsScan.Tables("ItemScanDetails").Rows(drindex)("GrossAmt") = vnewNetAmt

            '    Dim dvDtl As New DataView
            '    dvDtl = New DataView(dsMain.Tables("SalesOrderTaxDtls"), "EAN='" & _dsScan.Tables("ItemScanDetails").Rows(drindex)("EAN").ToString() & "'", "", DataViewRowState.CurrentRows)

            '    For Each drView As DataRowView In dvDtl
            '        Dim result As DataRow() = dtArticleTaxes.Select("TaxCode='" + drView("TaxLabel").ToString() + "' ")
            '        If result.Length > 0 Then
            '            If result(0)("Inclusive") Then
            '                _dsScan.Tables("ItemScanDetails").Rows(drindex)("IncTaxAmt") = drView("TaxValue")
            '            End If
            '            If result(0)("Inclusive") = False Then
            '                _dsScan.Tables("ItemScanDetails").Rows(drindex)("ExclTaxAmt") = drView("TaxValue")
            '            End If

            '            vnewTotalTaxAmt = vnewTotalTaxAmt + drView("TaxValue")
            '            _dsScan.Tables("ItemScanDetails").Rows(drindex)("TotalTaxAmt") = vnewTotalTaxAmt

            '            _dsScan.Tables("ItemScanDetails").Rows(drindex)("NetAmount") = vnewNetAmt + vnewTotalTaxAmt - IIf(_dsScan.Tables("ItemScanDetails").Rows(drindex)("Discount") Is DBNull.Value, 0, _dsScan.Tables("ItemScanDetails").Rows(drindex)("Discount"))
            '            _dsScan.Tables("ItemScanDetails").Rows(drindex)("SellingPrice") = _dsScan.Tables("ItemScanDetails").Rows(drindex)("SellingPrice")
            '            _dsScan.Tables("ItemScanDetails").Rows(drindex)("GrossAmt") = vnewNetAmt
            '        End If
            '    Next

            '    _dsScan.Tables("ItemScanDetails").AcceptChanges()

            'Next

            'For drindex = 0 To _dsPackagingVar.Tables("PackagingMaterial").Rows.Count - 1
            '    Dim vnewNetAmt As Decimal = 0.0
            '    Dim vnewExclTaxAmt As Decimal = 0.0
            '    Dim vnewIncTaxAmt As Decimal = 0.0
            '    Dim vnewTotalTaxAmt As Decimal = 0.0
            '    Dim vnewNetAmount As Decimal = 0.0
            '    Dim vnewSellingPrice As Decimal = 0.0
            '    Dim vnewGrossAmt As Decimal = 0.0
            '    vnewNetAmt = Math.Round(_dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("SellingPrice") * _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("Quantity"), 3)
            '    _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("ExclTaxAmt") = vnewExclTaxAmt
            '    _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("IncTaxAmt") = vnewIncTaxAmt
            '    _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("TotalTaxAmt") = vnewTotalTaxAmt

            '    _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("NetAmount") = vnewNetAmt - IIf(_dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("Discount") Is DBNull.Value, 0, _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("Discount"))
            '    _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("SellingPrice") = _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("SellingPrice")
            '    _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("GrossAmt") = vnewNetAmt

            '    Dim dvDtl As New DataView
            '    dvDtl = New DataView(dsMain.Tables("SalesOrderTaxDtls"), "EAN='" & _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("EAN").ToString() & "'", "", DataViewRowState.CurrentRows)

            '    For Each drView As DataRowView In dvDtl
            '        Dim result As DataRow() = dtArticleTaxes.Select("TaxCode='" + drView("TaxLabel").ToString() + "' ")
            '        If result.Length > 0 Then
            '            If result(0)("Inclusive") Then
            '                _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("IncTaxAmt") = drView("TaxValue")
            '            End If
            '            If result(0)("Inclusive") = False Then
            '                _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("ExclTaxAmt") = drView("TaxValue")
            '            End If

            '            vnewTotalTaxAmt = vnewTotalTaxAmt + drView("TaxValue")
            '            _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("TotalTaxAmt") = vnewTotalTaxAmt

            '            _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("NetAmount") = vnewNetAmt + vnewTotalTaxAmt - IIf(_dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("Discount") Is DBNull.Value, 0, _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("Discount"))
            '            _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("SellingPrice") = _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("SellingPrice")
            '            _dsPackagingVar.Tables("PackagingMaterial").Rows(drindex)("GrossAmt") = vnewNetAmt
            '        End If




            '    Next


            '    _dsPackagingVar.Tables("PackagingMaterial").AcceptChanges()
            'Next
        End If
        ''end: reset tax with article level only


        If _dsScan.Tables("ItemScanDetails").Rows.Count > 0 Then


            'If dsMain.Tables("SalesOrderTaxDtls").Rows.Count > 0 Then
            '    dsMain.Tables("SalesOrderTaxDtls").Clear()
            '    dsMain.Tables("SalesOrderTaxDtls").AcceptChanges()
            'End If
            For drTaxIndex = 0 To dtDocTAxes.Rows.Count - 1

                'Dim resultsDocs As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("isdocumentlevel=1 and TaxLabel='" + dt.Rows(iRowTax)("TaxCode") + "'")
                'Dim resultsDocs As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("isdocumentlevel=1 and status=true and TaxLabel='" + dt.Rows(iRowTax)("TaxCode") + "'")
                'If resultsDocs.Length = 0 Then
                Dim drTax1 As DataRow
                'If drTaxExists Is Nothing Then
                drTax1 = dsMain.Tables("SalesOrderTaxDtls").NewRow()
                drTax1("SiteCode") = clsAdmin.SiteCode
                drTax1("FinYear") = clsAdmin.Financialyear
                drTax1("SaleOrderNumber") = CtrlTxtOrderNo.Text
                drTax1("EAN") = ""
                drTax1("TaxLineNo") = vtaxIndex
                drTax1("TaxLabel") = dtDocTAxes.Rows(drTaxIndex)("TaxCode")
                drTax1("TaxValue") = (dtDocTAxes.Rows(drTaxIndex)("TaxAmt"))
                drTax1("IsDocumentLevel") = True
                drTax1("Status") = True


                dsMain.Tables("SalesOrderTaxDtls").Rows.Add(drTax1)
                vtaxIndex = vtaxIndex + 1
                '   Else
                ' drTaxExists("TaxValue") = drTaxExists("TaxValue") + Math.Round(dt.Rows(iRowTax)("TaxAmount"), 2)
                '  End If


                ' End If
            Next

            _dsScan.AcceptChanges()
            _dsPackagingVar.AcceptChanges()
            dsMain.Tables("SalesOrderTaxDtls").AcceptChanges()
            'For index = 1 To grdScanItem.Rows.Count - 1
            '    grdScanItem_AfterEdit(dsMain, New C1.Win.C1FlexGrid.RowColEventArgs(index, grdScanItem.Cols("PickUpQty").Index))
            'Next

            ' RemoveApplyPromotion(_dsScan, _dsPackagingVar, False)
            'CalculateSalesOrderSummary(_dsScan)
        End If
    End Sub
    ''' <summary>
    ''' This Method is written for Resetting tax for CST application on Items
    ''' </summary>
    ''' <param name="considerCst"></param>
    ''' <remarks></remarks>
    Private Sub ResetTax(ByVal considerCst As Boolean)
        'Tax Reset
        For drindex = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1
            Dim dt = objCM.getTax(vSiteCode, String.Empty, "SO201", _dsScan.Tables("ItemScanDetails").Rows(drindex)("Quantity"), _dsScan.Tables("ItemScanDetails").Rows(drindex)("EAN"), clsDefaultConfiguration.CSTTaxCode, considerCst)
            If dt Is Nothing OrElse Not dt.Rows.Count > 0 Then
                Exit Sub
            End If

        Next

        If _dsScan.Tables("ItemScanDetails").Rows.Count > 0 Then
            If dsMain.Tables("SalesOrderTaxDtls").Rows.Count > 0 Then
                dsMain.Tables("SalesOrderTaxDtls").Clear()
                dsMain.Tables("SalesOrderTaxDtls").AcceptChanges()
            End If

            For drindex = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1
                Dim inclTax As Decimal = 0.0
                Dim ExcelTax As Decimal = 0.0
                Dim taxableamt = Math.Round(_dsScan.Tables("ItemScanDetails").Rows(drindex)("SellingPrice") * _dsScan.Tables("ItemScanDetails").Rows(drindex)("Quantity"), 3)
                'Dim dt = objCM.getTax(vSiteCode, _dsScan.Tables("ItemScanDetails").Rows(drindex)("ARTICLECODE"), "SO201", _dsScan.Tables("ItemScanDetails").Rows(drindex)("Quantity"), _dsScan.Tables("ItemScanDetails").Rows(drindex)("EAN"), clsDefaultConfiguration.CSTTaxCode, considerCst)
                Dim dt = objCM.getTax(vSiteCode, String.Empty, "SO201", _dsScan.Tables("ItemScanDetails").Rows(drindex)("Quantity"), _dsScan.Tables("ItemScanDetails").Rows(drindex)("EAN"), clsDefaultConfiguration.CSTTaxCode, considerCst)
                'taxableamt = taxableamt - GetTaxableAmountForCst(_dsScan.Tables("ItemScanDetails").Rows(drindex)("ARTICLECODE"), _dsScan.Tables("ItemScanDetails").Rows(drindex)("EAN"), _dsScan.Tables("ItemScanDetails").Rows(drindex)("Quantity"), taxableamt)
                dt.Rows(0)("TAXABLE_AMOUNT") = taxableamt
                objCM.getCalculatedDataSet(dt)
                For iRowTax = 0 To dt.Rows.Count - 1

                    If CDbl(dt.Rows(iRowTax)("VALUE").ToString) <> 0 Then
                        If dt.Rows(iRowTax)("INCLUSIVE") = True Then
                            inclTax = inclTax + CDbl(dt.Rows(iRowTax)("TAXAMOUNT"))
                        Else
                            ExcelTax = ExcelTax + CDbl(dt.Rows(iRowTax)("TAXAMOUNT"))
                        End If
                    End If

                    Dim drTax1 = dsMain.Tables("SalesOrderTaxDtls").NewRow

                    drTax1("SiteCode") = clsAdmin.SiteCode
                    drTax1("FinYear") = clsAdmin.Financialyear
                    drTax1("SaleOrderNumber") = CtrlTxtOrderNo.Text
                    drTax1("EAN") = _dsScan.Tables("ItemScanDetails").Rows(drindex)("EAN")
                    drTax1("TaxLineNo") = iRowTax
                    drTax1("TaxLabel") = dt.Rows(iRowTax)("TaxCode")
                    drTax1("TaxValue") = Math.Round(dt.Rows(iRowTax)("TaxAmount"), 2)

                    dsMain.Tables("SalesOrderTaxDtls").Rows.Add(drTax1)
                Next

                _dsScan.Tables("ItemScanDetails").Rows(drindex)("ExclTaxAmt") = ExcelTax
                _dsScan.Tables("ItemScanDetails").Rows(drindex)("IncTaxAmt") = inclTax
                _dsScan.Tables("ItemScanDetails").Rows(drindex)("TotalTaxAmt") = ExcelTax + inclTax

                _dsScan.Tables("ItemScanDetails").Rows(drindex)("NetAmount") = FormatNumber(taxableamt + ExcelTax + inclTax, 2)
                _dsScan.Tables("ItemScanDetails").Rows(drindex)("SellingPrice") = FormatNumber(taxableamt / _dsScan.Tables("ItemScanDetails").Rows(drindex)("Quantity"), 2)
                _dsScan.Tables("ItemScanDetails").Rows(drindex)("GrossAmt") = taxableamt
            Next
            _dsScan.AcceptChanges()
            dsMain.Tables("SalesOrderTaxDtls").AcceptChanges()
            For index = 1 To grdScanItem.Rows.Count - 1
                grdScanItem_AfterEdit(dsMain, New C1.Win.C1FlexGrid.RowColEventArgs(index, grdScanItem.Cols("PickUpQty").Index))
            Next

            RemoveApplyPromotion(_dsScan)
            CalculateSalesOrderSummary(_dsScan)
        End If
    End Sub


    ''' <summary>
    ''' Resize DataGrid for Display Scan Article Details
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdScanItem_Resize(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles grdScanItem.Resize

        GridWidth = 0
        GridWidth = (grdScanItem.Width * 1) / 100
        grdScanItem.Cols(1).WidthDisplay = GridWidth * 12.7
        grdScanItem.Cols(2).WidthDisplay = GridWidth * 20.05
        grdScanItem.Cols(3).WidthDisplay = GridWidth * 6.68
        grdScanItem.Cols(4).WidthDisplay = GridWidth * 9.36
        grdScanItem.Cols(5).WidthDisplay = GridWidth * 10.7
        grdScanItem.Cols(6).WidthDisplay = GridWidth * 8.02
        grdScanItem.Cols(7).WidthDisplay = GridWidth * 9.36
        grdScanItem.Cols(8).WidthDisplay = GridWidth * 12.7
        grdScanItem.Cols(9).WidthDisplay = GridWidth * 8.02
        grdScanItem.ExtendLastCol = True
        grdScanItem.Refresh()
    End Sub

    ''' <summary>
    ''' Check Delivery Date
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>If Delivery Date is back date then show massage.</remarks>
    Private Sub dtpExpDeliveryDate_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        ''Handles  dtpExpDeliveryDate.Leave 
        'CtrlSalesInfo1.CtrlDtExpDelDate.ValidateText()
        'If CtrlSalesInfo1.CtrlDtExpDelDate.Value Is DBNull.Value Then
        '    ShowMessage(getValueByKey("SO051"), "SO051 - " & getValueByKey("CLAE04"))
        '    CtrlSalesInfo1.CtrlDtExpDelDate.Value = consDeliveryDate
        'Else
        '    If DateDiff(DateInterval.Day, vCurrentDate, CtrlSalesInfo1.CtrlDtExpDelDate.Value) < 0 Then
        '        ShowMessage(getValueByKey("SO010"), "SO010 - " & getValueByKey("CLAE04"))
        '        CtrlSalesInfo1.CtrlDtExpDelDate.Value = consDeliveryDate
        '    ElseIf (vCurrentDate > Convert.ToDateTime(CtrlSalesInfo1.CtrlDtExpDelDate.Value)) Then
        '        ShowMessage(getValueByKey("SO010"), "SO010 - " & getValueByKey("CLAE04"))
        '        CtrlSalesInfo1.CtrlDtExpDelDate.Value = consDeliveryDate

        '    Else
        '        vSalesOrderExpectedDeliveryDate = CtrlSalesInfo1.CtrlDtExpDelDate.Value
        '        For Each drGridRow As C1.Win.C1FlexGrid.Row In grdScanItem.Rows
        '            If Not (drGridRow.Index = 0) Then
        '                drGridRow.Item("ExpDelDate") = CtrlSalesInfo1.CtrlDtExpDelDate.Value
        '            End If
        '        Next
        '    End If
        'End If
    End Sub


    ''' <summary>
    ''' Set Shortcut Button in Action Button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    ''' <summary>
    ''' Get Sales Person data
    ''' </summary>
    ''' <remarks></remarks>
    'Private Sub LoadSalesPersonDetails()
    '    Try
    '        dtSalesPerson = objCM.GetSalesPerson(vSiteCode)
    '        If Not dtSalesPerson Is Nothing And dtSalesPerson.Rows.Count > 0 Then
    '            cboSalesPerson.DataSource = dtSalesPerson
    '            cboSalesPerson.DisplayMember = dtSalesPerson.Columns("SalesPersonName").ToString()
    '            cboSalesPerson.ValueMember = dtSalesPerson.Columns("EmpCode").ToString()
    '            cboSalesPerson.SelectedIndex = -1
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub
    Private _SoDeliveryInfo As New BindingList(Of SODeliveryLocationInfo)
    Public Property SoDeliveryInfo As BindingList(Of SODeliveryLocationInfo)
        Get
            If _SoDeliveryInfo Is Nothing Then
                _SoDeliveryInfo = New BindingList(Of SODeliveryLocationInfo)()
            End If
            Return _SoDeliveryInfo
        End Get
        Set(ByVal value As BindingList(Of SODeliveryLocationInfo))
            _SoDeliveryInfo = value
        End Set
    End Property
    Public Sub New()
        NewCall()
    End Sub
    Private Sub NewCall()
        If clsDefaultConfiguration.TillOperationRequired = True And clsDefaultConfiguration.TillOpenDone = False Then
            ShowMessage(getValueByKey("SO052"), "SO052 - " & getValueByKey("CLAE04"))
            Me.Dispose()
            Me.Close()
            Exit Sub
        End If
        ' This call is required by the Windows Form Designer.
        ''Changed By Prasad
        'If CheckAuthorisation(clsAdmin.UserCode, "SOCreation") = False Then
        '    ShowMessage(getValueByKey("SO053"), "SO053 - " & getValueByKey("CLAE04"))
        '    Me.Dispose()
        '    Me.Close()
        '    Exit Sub
        'End If

        InitializeComponent()
        Me.ClientSize = New System.Drawing.Size(gmdiclientwidth, gmdiclientheight)
        CtrlRbn1.pInitRbn()
        If Batchbarcode Is Nothing Then
            Batchbarcode = New List(Of SpectrumCommon.BtachbarcodeInfo)
        End If

        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            CtrlRbn1.DbtnF12.LargeImage = Global.Spectrum.My.Resources.Resources.PriceChange
            CtrlRbn1.DbtnF2.LargeImage = Global.Spectrum.My.Resources.Resources.ChangeQuantity
        Else
            CtrlRbn1.DbtnF12.LargeImage = Global.Spectrum.My.Resources.Resources.price_change
            CtrlRbn1.DbtnF2.LargeImage = Global.Spectrum.My.Resources.Resources.change_qty
        End If
        'CtrlSalesInfo1.CtrlTxtOrderNo.TextDetached = False
        'CtrlSalesInfo1.CtrlTxtOrderNo.ReadOnly = True
        'CtrlSalesInfo1.CtrldtOrderDt.ReadOnly = True
        'If CtrlSalesInfo1.C1Sizer1.Grid.Columns.Count = 4 Then
        '    CtrlSalesInfo1.C1Sizer1.Grid.Columns.Remove(3)
        'End If

        'LbSalesNo.Text = gResourceMgr.GetString("frmSalesOrderCreation_LbSalesNo", gCI)
        'LbSalesDate.Text = gResourceMgr.GetString("frmSalesOrderCreation_LbSalesDate", gCI)
        'LbExpDelDate.Text = gResourceMgr.GetString("frmSalesOrderCreation_LbExpDelDate", gCI)
        'LbRemarks.Text = gResourceMgr.GetString("frmSalesOrderCreation_LbRemarks", gCI)
        'LbCustomerRef.Text = gResourceMgr.GetString("frmSalesOrderCreation_LbCustomerRef", gCI)
        'LbSalesPerson.Text = gResourceMgr.GetString("frmSalesOrderCreation_LbSalesPerson", gCI)

        'GroupBoxSOCustomerInfo.Text = gResourceMgr.GetString("frmSalesOrderCreation_GroupBoxSOCustomerInfo", gCI)
        'LbCustomerNo.Text = gResourceMgr.GetString("frmSalesOrderCreation_LbCustomerNo", gCI)
        'LbCName.Text = gResourceMgr.GetString("frmSalesOrderCreation_LbCName", gCI)
        'LbCAddressType.Text = gResourceMgr.GetString("frmSalesOrderCreation_LbCAddressType", gCI)
        'LbCAddress.Text = gResourceMgr.GetString("frmSalesOrderCreation_LbCAddress", gCI)
        'LbCTelephone.Text = gResourceMgr.GetString("frmSalesOrderCreation_LbCTelephone", gCI)

        'LbItemScan.Text = gResourceMgr.GetString("frmSalesOrderCreation_LbItemScan", gCI)
        'LbTotal.Text = gResourceMgr.GetString("frmSalesOrderCreation_LbTotal", gCI)

        ''TabPageItemDetails.Text = gResourceMgr.GetString("frmSalesOrderCreation_TabPageItemDetails", gCI)
        ''TabPageInvoiceDetails.Text = gResourceMgr.GetString("frmSalesOrderCreation_TabPageInvoiceDetails", gCI)

        'GroupBoxSOSummary.Text = gResourceMgr.GetString("frmSalesOrderCreation_GroupBoxSOSummary", gCI)
        'LbGrossAmt.Text = gResourceMgr.GetString("frmSalesOrderCreation_LbGrossAmt", gCI)
        'LbDiscAmt.Text = gResourceMgr.GetString("frmSalesOrderCreation_LbDiscAmt", gCI)
        'LbOtherCharges.Text = gResourceMgr.GetString("frmSalesOrderCreation_LbOtherCharges", gCI)
        'LbNetAmount.Text = gResourceMgr.GetString("frmSalesOrderCreation_LbNetAmount", gCI)
        'LbMinAdvanceAmt.Text = gResourceMgr.GetString("frmSalesOrderCreation_LbMinAdvanceAmt", gCI)
        'LbAdvancePaid.Text = gResourceMgr.GetString("frmSalesOrderCreation_LbAdvancePaid", gCI)
        'LbBalanceAmt.Text = gResourceMgr.GetString("frmSalesOrderCreation_LbBalanceAmt", gCI)
        ''LbReceivedAmt.Text = gResourceMgr.GetString("frmSalesOrderCreation_LbReceivedAmt", gCI)

        'BtnSONew.Text = gResourceMgr.GetString("frmSalesOrderCreation_BtnSONew", gCI)
        'BtnSOSave.Text = gResourceMgr.GetString("frmSalesOrderCreation_BtnSOSave", gCI)
        'BtnSOPrint.Text = gResourceMgr.GetString("frmSalesOrderCreation_BtnSOPrint", gCI)
        'BtnSOApplyPromotion.Text = gResourceMgr.GetString("frmSalesOrderCreation_BtnSOApplyPromotion", gCI)
        'BtnSOOtherCharges.Text = gResourceMgr.GetString("frmSalesOrderCreation_BtnSOOtherCharges", gCI)
        'BtnSOAcceptPayment.Text = gResourceMgr.GetString("frmSalesOrderCreation_BtnSOAcceptPayment", gCI)
        'BtnSOReturn.Text = gResourceMgr.GetString("frmSalesOrderCreation_BtnSOReturn", gCI)
        'BtnSOStockCheck.Text = gResourceMgr.GetString("frmSalesOrderCreation_BtnSOStockCheck", gCI)
        'BtnSOCalculater.Text = gResourceMgr.GetString("frmSalesOrderCreation_BtnSOCalculater", gCI)       

    End Sub

    Public Sub New(ByVal IsQuotationConv As Boolean, ByVal QuotationNo As String, ByVal SalesExec As String, ByVal CustomerID As String, ByVal OtherCharges As DataTable)
        NewCall()
        ISQuotationConversion = IsQuotationConv
        QuotationNumber = QuotationNo
        salesexecutive = SalesExec
        CustID = CustomerID
        QuotationOtherCharges = OtherCharges
    End Sub
#End Region

    Private _IsScanningWoBarcodeSelected As Boolean
    Private ArticleScanWithBatchBarcode As Boolean
    Dim vSalesOrderCreationDate As DateTime = DateTime.Now

#Region "Add Items in Sales Order"
    ''' <summary>
    ''' Get the Item Details 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnSearchItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles BtnSearchItem.Click
        Try

            If clsDefaultConfiguration.IsSalesPersonApplicable = True AndAlso CtrlSalesPersons.CtrlSalesPersons.SelectedValue = Nothing Then
                ShowMessage(getValueByKey("SO014"), "SO014 - " & getValueByKey("CLAE04"))
                CtrlSalesPersons.CtrlTxtBox.Text = ""
                CtrlSalesPersons.CtrlSalesPersons.Select()
                Exit Sub
            End If

            Dim FetchData As New frmNItemSearch()
            FetchData.ShowDialog()

            drSearchItem = FetchData.ItemRow
            If drSearchItem Is Nothing Then
                Exit Sub
            End If

            vArticleImagesCode = drSearchItem.Item("ArticleCode")

            If Not (drSearchItem Is Nothing) Then
                If IsApplyPromotion = True Then
                    RemoveApplyPromotion(dsScan, _dsPackagingVar)
                End If
                Dim foundRow As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("IsDocumentLevel = True")
                If foundRow.Count > 0 Then
                    ResetDocTax(True, dtDocTaxes)
                    dtDocTaxes.Clear()
                End If
                IsEditItem = False
                Dim ean As String = String.Empty
                If clsDefaultConfiguration.IsBatchManagementReq Then
                    ean = SearchAvailableBarcodes(drSearchItem.Item("ArticleCode").ToString())
                    If String.IsNullOrEmpty(ean) Then
                        'Dim EventType As Int32
                        'ShowMessage(getValueByKey("frmnsalesorder.scaningreqmsg"), getValueByKey("CLAE04"), EventType, True, getValueByKey("mod009"))
                        'If EventType = 1 Then
                        _IsScanningWoBarcodeSelected = True
                        ean = drSearchItem("EAN").ToString()
                        'Else
                        '    Exit Sub
                        'End If
                    End If
                End If
                If String.IsNullOrEmpty(ean) Then
                    ean = drSearchItem("EAN").ToString()
                End If
                CtrlSalesPersons.CtrlTxtBox.Text = ean
                txtSearchItem_Leave(ean, New KeyEventArgs(Keys.Enter))
                _IsScanningWoBarcodeSelected = False
                RefreshScanData(dsScan)
                'grdScanItem_Resize(sender, New System.EventArgs)
                drItemsRow = Nothing
                CtrlSalesPersons.CtrlTxtBox.Text = ""

                'strImagesUrl = objComn.GetArticleImage(vArticleImagesCode, My.Settings.ArticleImageFolder)
                'PictureBoxImages.ImageLocation = strImagesUrl
                'CtrlProductImage.ShowArticleImage(vArticleImagesCode)
                GridSetting()
                'AddButtonControlInGrid(1)
            End If
        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub

    Private Function SearchAvailableBarcodes(ByRef articleCode As String) As String
        Dim barCode As String = String.Empty
        Dim objFrmBarcode As New frmSelectBarcode
        objFrmBarcode.ArticleCode = articleCode
        objFrmBarcode.ShowDialog()
        If objFrmBarcode.SelectedRow IsNot Nothing Then
            barCode = objFrmBarcode.SelectedRow.Cells("BatchBarcode").Value
            ArticleScanWithBatchBarcode = True
        Else
            ArticleScanWithBatchBarcode = False
        End If
        Return barCode
    End Function

    Private Sub AndroidSearchTextBox_Textchange(sender As Object, e As EventArgs)
        If CtrlSalesPersons.AndroidSearchTextBox.IsItemSelected Then
            CtrlSalesPersons.CtrlTxtBox.Text = CtrlSalesPersons.AndroidSearchTextBox.Text
        End If
    End Sub

    Private Sub txtSearchItem_textchange(sender As Object, e As EventArgs)
        If Not String.IsNullOrEmpty(CtrlSalesPersons.CtrlTxtBox.Text) AndAlso CtrlSalesPersons.AndroidSearchTextBox.IsItemSelected Then
            CtrlSalesPersons.AndroidSearchTextBox.IsItemSelected = False
            txtSearchItem_Leave(sender, New System.EventArgs)
        End If
    End Sub
    'Private Sub AndroidSearchTextBox_Leave(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles txtSearchItem.Leave
    '    CtrlSalesPersons.AndroidSearchTextBox.Text = ""
    'End Sub
    ''' <summary>
    ''' Get the Item Details 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtSearchItem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles txtSearchItem.Leave
        Try
            If clsDefaultConfiguration.customerwisepricemanagement Then 'vipin PC SO Merge 03-05-2018
                If String.IsNullOrEmpty(CustomerNo) Then
                    ShowMessage("Please Select Customer First", "SO014 - " & getValueByKey("CLAE04"))
                    CtrlSalesPersons.AndroidSearchTextBox.Text = ""
                    CtrlSalesPersons.CtrlTxtBox.Text = ""
                    Exit Sub
                End If
            End If
            If CtrlSalesPersons.CtrlTxtBox.Text.Trim.Length > 0 Then
                If clsDefaultConfiguration.IsSalesPersonApplicable = True AndAlso CtrlSalesPersons.CtrlSalesPersons.SelectedValue = Nothing Then
                    ShowMessage(getValueByKey("SO014"), "SO014 - " & getValueByKey("CLAE04"))
                    CtrlSalesPersons.AndroidSearchTextBox.Text = ""
                    CtrlSalesPersons.CtrlTxtBox.Text = ""
                    'CtrlSalesPersons.CtrlSalesPersons.Select()
                    Exit Sub
                End If
                CtrlSalesPersons.CtrlTxtBox.Text = CtrlSalesPersons.CtrlTxtBox.Text.ToString().Split(" ")(0)
                If IsApplyPromotion = True Then
                    rbbtnClrAllPromo_Click(sender, e)
                End If

                If clsDefaultConfiguration.IsBatchManagementReq Then
                    Dim articleCode As String = objItemSch.GetArticleCodeFromBarcode(clsAdmin.SiteCode, CtrlSalesPersons.CtrlTxtBox.Text.Trim)
                    If String.IsNullOrEmpty(articleCode) Then
                        'ShowMessage(getValueByKey("SO055"), "SO055 - " & getValueByKey("CLAE04"))
                        'CtrlSalesPersons.CtrlTxtBox.Text = ""
                        'Exit Sub
                        articleCode = objItemSch.GetArticleCodeFromEAN(CtrlSalesPersons.CtrlTxtBox.Text.Trim)
                        If String.IsNullOrEmpty(articleCode) Then
                            articleCode = CtrlSalesPersons.CtrlTxtBox.Text.Trim
                        End If
                        Dim barCode As String
                        If _IsScanningWoBarcodeSelected = False Then
                            barCode = SearchAvailableBarcodes(articleCode)
                        End If

                        If String.IsNullOrEmpty(barCode) AndAlso _IsScanningWoBarcodeSelected = False Then

                            'Dim EventType As Int32
                            'ShowMessage(getValueByKey("frmnsalesorder.scaningreqmsg"), getValueByKey("CLAE04"), EventType, True, getValueByKey("mod009"))
                            'If EventType = 1 Then
                            dtItemSch = objItemSch.GetEANData(clsAdmin.SiteCode, articleCode, clsAdmin.LangCode, "", dtItemScanData)
                            For Each item In dtItemSch.Rows
                                item("BatchBarcode") = DBNull.Value
                            Next
                            'Else
                            '    Exit Sub
                            'End If
                        Else
                            dtItemSch = objItemSch.GetEANData(clsAdmin.SiteCode, articleCode, clsAdmin.LangCode, "", dtItemScanData, False, True, barCode)
                            If Batchbarcode IsNot Nothing Then
                                If _IsScanningWoBarcodeSelected = False Then
                                    If Batchbarcode.Any(Function(w) w.BatchBarcode = barCode) Then
                                        Batchbarcode.Find(Function(w) w.BatchBarcode = barCode).Qty = Batchbarcode.Find(Function(w) w.BatchBarcode = barCode).Qty + 1
                                    Else
                                        Dim dvEan As New DataView(dtItemSch, "Ean='" & CtrlSalesPersons.CtrlTxtBox.Text & "'", "", DataViewRowState.CurrentRows)
                                        If dvEan.Count > 0 Then
                                            Batchbarcode.Add(New SpectrumCommon.BtachbarcodeInfo() With {.ArticleCode = dvEan(0)("ArticleCode"), .BatchBarcode = barCode, .EAN = dvEan(0)("EAN"), .LineNO = vRowIndex, .Qty = 1, .Status = True})
                                        Else
                                            Batchbarcode.Add(New SpectrumCommon.BtachbarcodeInfo() With {.ArticleCode = dtItemSch(0)("ArticleCode"), .BatchBarcode = barCode, .EAN = dtItemSch(0)("EAN"), .LineNO = vRowIndex, .Qty = 1, .Status = True})
                                        End If
                                    End If
                                End If

                                For Each item In dtItemSch.Rows
                                    item("BatchBarcode") = DBNull.Value
                                Next
                            End If

                        End If
                    Else
                        dtItemSch = objItemSch.GetEANData(clsAdmin.SiteCode, articleCode, clsAdmin.LangCode, "", dtItemScanData, False, True, CtrlSalesPersons.CtrlTxtBox.Text.Trim)
                        'For Each item In dtItemSch.Rows
                        '    item("BatchBarcode") = CtrlSalesPersons.CtrlTxtBox.Text.Trim
                        'Next

                        If Batchbarcode IsNot Nothing Then
                            If Batchbarcode.Any(Function(w) w.BatchBarcode = CtrlSalesPersons.CtrlTxtBox.Text.Trim) Then
                                Batchbarcode.Find(Function(w) w.BatchBarcode = CtrlSalesPersons.CtrlTxtBox.Text.Trim).Qty = Batchbarcode.Find(Function(w) w.BatchBarcode = CtrlSalesPersons.CtrlTxtBox.Text.Trim).Qty + 1
                            Else
                                Batchbarcode.Add(New SpectrumCommon.BtachbarcodeInfo() With {.ArticleCode = dtItemSch(0)("ArticleCode"), .BatchBarcode = CtrlSalesPersons.CtrlTxtBox.Text.Trim, .EAN = dtItemSch(0)("EAN"), .LineNO = vRowIndex, .Qty = 1, .Status = True})
                            End If
                            For Each item In dtItemSch.Rows
                                item("BatchBarcode") = DBNull.Value
                            Next

                            ArticleScanWithBatchBarcode = True
                        End If
                    End If
                Else
                    dtItemSch = objItemSch.GetEANData(clsAdmin.SiteCode, CtrlSalesPersons.CtrlTxtBox.Text.Trim, clsAdmin.LangCode, "", dtItemScanData)
                    For Each item In dtItemSch.Rows
                        item("BatchBarcode") = DBNull.Value
                    Next
                End If

                If clsDefaultConfiguration.customerwisepricemanagement Then
                    Dim CustWisePrice As DataTable = objComn.GetCustomerWisePriceForSO(clsAdmin.SiteCode, CustomerNo, CtrlSalesPersons.CtrlTxtBox.Text.Trim)
                    If Not CustWisePrice Is Nothing And CustWisePrice.Rows.Count > 0 Then
                        For Each DrRow In dtItemSch.Rows
                            DrRow("SellingPrice") = CustWisePrice.Rows(0)(0)
                        Next
                    End If
                End If
       
                If dtItemSch Is Nothing Then
                    ShowMessage(getValueByKey("SO055"), "SO055 - " & getValueByKey("CLAE04"))
                    CtrlSalesPersons.CtrlTxtBox.Text = ""
                    Exit Sub
                End If
                If dtItemSch.Rows.Count = 0 Then
                    ShowMessage(getValueByKey("SO055"), "SO055 - " & getValueByKey("CLAE04"))
                    CtrlSalesPersons.CtrlTxtBox.Text = ""
                    Exit Sub
                End If


                'Changed by rama on sep 16 sep 2009 for bug no 1107
                If dtItemSch.Rows.Count > 1 Then
                    Dim dvEan As New DataView(dtItemSch, "Ean='" & CtrlSalesPersons.CtrlTxtBox.Text & "'", "", DataViewRowState.CurrentRows)
                    If dvEan.Count > 0 Then
                        dvEan.RowFilter = "EAN<>'" & CtrlSalesPersons.CtrlTxtBox.Text & "'"
                        If dvEan.Count > 0 Then
                            dvEan.AllowDelete = True
                            For Each dr As DataRowView In dvEan
                                dr.Delete()
                            Next
                            dtItemSch.AcceptChanges()
                        End If
                    Else
                        Dim dv As New DataView(dtItemSch, " DefaultEAN <> 1 ", "", DataViewRowState.CurrentRows)
                        'Dim dv As New DataView(dtItemSch, "EanType<>'" & EanType & "'", "", DataViewRowState.CurrentRows)
                        If dv.Count > 0 Then
                            dv.AllowDelete = True
                            For Each dr As DataRowView In dv
                                dr.Delete()
                            Next
                            dtItemSch.AcceptChanges()
                            If dtItemSch.Rows.Count <= 0 Then
                                ShowMessage(getValueByKey("SO056"), "SO056 - " & getValueByKey("CLAE04"))
                                Exit Sub
                            End If
                            If dtItemSch.Rows.Count > 1 Then
                                Dim objEan As New frmNCommonView
                                objEan.SetData = dtItemSch
                                Array.Resize(objEan.ColumnName, dtItemSch.Columns.Count)
                                Dim i As Integer = 0
                                For Each col As DataColumn In dtItemSch.Columns
                                    If col.ColumnName <> "EAN" And col.ColumnName <> "ARTICLECODE" And col.ColumnName <> "SELLINGPRICE" Then
                                        objEan.ColumnName(i) = col.ColumnName
                                    End If
                                    i = i + 1
                                Next
                                objEan.ShowDialog()
                                Dim dtTemp As DataTable = dtItemSch.Clone()
                                dtTemp.ImportRow(objEan.GetResultRow)
                                dtItemSch.Clear()
                                dtItemSch = dtTemp
                                'For i = dtItemSch.Rows.Count - 1 To 1 Step -1
                                '    dtItemSch.Rows.RemoveAt(i)
                                'Next
                                'If Not objEan.search Is Nothing Then
                                '    dtItemSch.Rows(0)("SellingPrice") = objEan.search(5)
                                '    dtItemSch.Rows(0)("EAN") = objEan.search(0)
                                'Else
                                '    Exit Sub
                                'End If
                            End If
                        End If
                    End If
                End If
                '---
                If dtItemSch.Rows.Count > 0 Then
                    If dtItemSch.Rows(0)("FreezeSB") = True Then
                        ShowMessage(getValueByKey("SO079"), "SO079 - " & getValueByKey("CLAE04"))
                        CtrlSalesPersons.CtrlTxtBox.Text = ""
                        Exit Sub
                    End If
                End If

                If dtItemSch.Rows.Count > 1 Then
                    Dim objPrice As New frmNCommonView
                    objPrice.SetData = dtItemSch
                    objPrice.ShowDialog()

                    If Not objPrice.search Is Nothing Then
                        CtrlSalesPersons.CtrlTxtBox.Text = objPrice.search(5)
                        drItemSch = dtItemSch.Select("SELLINGPRICE='" & objPrice.search(9) & "'")(0)
                    End If
                Else
                    If dtItemSch.Rows.Count = 1 Then
                        drItemSch = dtItemSch.Rows(0)
                        IsMRPOpen = drItemSch("IsMRPOpen")
                    End If
                End If

                If dtItemSch.Rows.Count > 0 AndAlso Not (drItemSch Is Nothing) Then
                    SetScanItemInSO(drItemSch)
                    CalculateSalesOrderSummary(dsScan)
                    RefreshScanData(dsScan)
                End If

                IsMRPOpen = False
                dtItemSch.Clear()
                'CtrlSalesPersons.CtrlTxtBox.Text = ""
                CtrlSalesPersons.AndroidSearchTextBox.IsItemSelected = False
                CtrlSalesPersons.AndroidSearchTextBox.Text = ""
                CtrlSalesPersons.CtrlTxtBox.Text = ""
                'CtrlSalesPersons.CtrlTxtBox.Select()
                CtrlSalesPersons.AndroidSearchTextBox.Select()
                grdScanItem.Select(grdScanItem.Rows.Count - 1, 1, True)
            End If
            GridSetting()
        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE04"))
        End Try

    End Sub

    ''' <summary>
    ''' Get the Item Details 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtSearchItem_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) 'Handles txtSearchItem.PreviewKeyDown
        If e.KeyCode = Keys.Enter Then

            If clsDefaultConfiguration.IsSalesPersonApplicable = True AndAlso CtrlSalesPersons.CtrlSalesPersons.SelectedValue = Nothing Then
                ShowMessage(getValueByKey("SO014"), "SO014 - " & getValueByKey("CLAE04"))
                CtrlSalesPersons.AndroidSearchTextBox.Text = ""
                CtrlSalesPersons.CtrlSalesPersons.Select()
                Exit Sub
            End If

            If IsApplyPromotion = True Then
                RemoveApplyPromotion(dsScan, _dsPackagingVar)
            End If

            txtSearchItem_Leave(sender, New System.EventArgs)
            'AddButtonControlInGrid(1)
        End If
        GridSetting()

        'CtrlSalesPersons.CtrlTxtBox.Select()
        CtrlSalesPersons.AndroidSearchTextBox.Select()
    End Sub
    Private Sub dgDeliveryLocation_BeforeEdit(sender As Object, e As RowColEventArgs) Handles dgDeliveryLocation.BeforeEdit
        Try
            Dim CurrentCell As Integer = e.Col
            Dim CurrentRow As Integer = e.Row
            If (dgDeliveryLocation.Cols(CurrentCell).Name = "Quantity") Then
                Dim isHeader As String = IIf(dgDeliveryLocation.Item(CurrentRow, "IsHeader") Is DBNull.Value, 0, dgDeliveryLocation.Item(CurrentRow, "IsHeader"))
                If isHeader = "True" Then
                    e.Cancel = True
                End If

            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub grdScanItem_BeforeEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdScanItem.BeforeEdit
        Try
            Dim CurrentCell As Integer = e.Col
            Dim CurrentRow As Integer = e.Row
            Dim articleType As String = IIf(grdScanItem.Item(CurrentRow, "ArticleType") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "ArticleType"))
            If (grdScanItem.Cols(CurrentCell).Name = "DeliverySiteCode" AndAlso IsDBNull(grdScanItem.Item(CurrentRow, "BatchBarcode")) = False) OrElse (grdScanItem.Cols(CurrentCell).Name = "PickUpQty" AndAlso IsDBNull(grdScanItem.Item(CurrentRow, "BatchBarcode"))) Then

                e.Cancel = True
            End If
            If (grdScanItem.Cols(CurrentCell).Name = "SellingPrice") Then
                'Dim articleType As String = IIf(grdScanItem.Item(CurrentRow, "ArticleType") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "ArticleType"))
                If articleType = "Single" Then
                    e.Cancel = True
                End If
            End If
            '------

            If clsDefaultConfiguration.PackageFiedlsAllowed Then
                If (grdScanItem.Cols(CurrentCell).Name = "PckgSize") Then
                    If articleType = "Combo" Then
                        Dim articleName As String = IIf(grdScanItem.Item(CurrentRow, "DISCRIPTION") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "DISCRIPTION"))
                        Dim resultCombo As DataRow() = dtPackagingPrintBox.Select("KeyValue='" + articleName + "' and isPackagingMandatory=1")
                        If resultCombo.Length > 0 Then
                            e.Cancel = True
                        End If
                    Else
                        If grdScanItem.Item(CurrentRow, "UOM") = "NOS" Then
                            e.Cancel = True
                        End If
                    End If
                End If

                If (grdScanItem.Cols(CurrentCell).Name = "PckgQty") Then

                    If articleType = "Combo" Then
                        e.Cancel = True
                    Else
                        If grdScanItem.Item(CurrentRow, "UOM") = "NOS" Then
                            e.Cancel = True
                        End If
                    End If

                End If
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Update Scan Orders
    ''' </summary>
    ''' <param name="sender">Selected Row</param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgDeliveryLocation_AfterEdit(sender As Object, e As RowColEventArgs) Handles dgDeliveryLocation.AfterEdit
        Try
            If dgDeliveryLocation.Row = -1 Then Exit Sub
            Dim CurrentCell As Integer = e.Col
            Dim CurrentRow As Integer = dgDeliveryLocation.Row '-- e.Row
            Dim UpdatePackagingIndex As Integer = IIf(dgDeliveryLocation.Item(CurrentRow, "PackagingIndex") Is DBNull.Value, 0, dgDeliveryLocation.Item(CurrentRow, "PackagingIndex"))
            Dim UpdateDeliveryIndex As Integer = IIf(dgDeliveryLocation.Item(CurrentRow, "DeliveryIndex") Is DBNull.Value, 0, dgDeliveryLocation.Item(CurrentRow, "DeliveryIndex"))
            Dim ISAddressValid As Boolean = False
            If dgDeliveryLocation.Cols(CurrentCell).Name = "Quantity" Then
                Dim isHeader As String = IIf(dgDeliveryLocation.Item(CurrentRow, "IsHeader") Is DBNull.Value, 0, dgDeliveryLocation.Item(CurrentRow, "IsHeader"))
                If isHeader = "False" Then
                    'Dim dtAscii As New DataTable
                    Dim baseQty As Decimal
                    Dim currentQty As Decimal
                    Dim headerQty As Decimal
                    Dim resultPackQuntity As DataRow() = _dsPackagingVar.Tables(0).Select("PackagingIndex='" + UpdatePackagingIndex.ToString() + "' ")
                    If resultPackQuntity.Length > 0 Then
                        baseQty = resultPackQuntity(0)("Quantity")
                    End If
                    currentQty = IIf(dgDeliveryLocation.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, dgDeliveryLocation.Item(CurrentRow, "Quantity"))
                    If currentQty > baseQty Then
                        ShowMessage("Order quantity is getting exceeded", "SO009 - " & getValueByKey("CLAE04"))
                        Dim resultPrevQty As DataRow() = _dsPackagingDelivery.Tables(0).Select("DeliveryIndex='" + UpdateDeliveryIndex.ToString() + "' ")
                        If resultPrevQty.Length > 0 Then
                            dgDeliveryLocation.Item(CurrentRow, "Quantity") = resultPrevQty(0)("Quantity")
                        Else
                            dgDeliveryLocation.Item(CurrentRow, "Quantity") = 0
                        End If
                        Exit Sub
                    End If
                    '-------Added By Prasad for Checking If Single NOS Has Decimal Validation
                    Dim resultSingleNos As DataRow() = _dsPackagingDelivery.Tables(0).Select("DeliveryIndex='" + UpdateDeliveryIndex.ToString() + "' ")

                    Dim drCheckIfSingleNOSSubVariation As New DataView(_dsPackagingDelivery.Tables("PackagingDelivery"), "PackagingIndex='" & UpdatePackagingIndex.ToString() & "' AND IsCombo=False AND UOM='NOS' AND DeliveryIndex='" + UpdateDeliveryIndex.ToString() + "'", "", DataViewRowState.CurrentRows)
                    If drCheckIfSingleNOSSubVariation.Count > 0 Then

                        Dim OrderQty = currentQty.ToString.Split(".")
                        If OrderQty.Length = 2 Then
                            If OrderQty(1) > 0 Then
                                ShowMessage("Order Qty For Single Where UOM is NOS should not be in decimal", "Information")
                                dgDeliveryLocation.Item(dgDeliveryLocation.Row, "Quantity") = drCheckIfSingleNOSSubVariation(0)("Quantity")
                                If resultSingleNos.Length > 0 Then
                                    resultSingleNos(0)("Quantity") = IIf(dgDeliveryLocation.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, drCheckIfSingleNOSSubVariation(0)("Quantity"))
                                    _dsPackagingDelivery.Tables("PackagingDelivery").AcceptChanges()
                                End If
                                'If drView1.Length > 0 Then
                                '    drView1(0)("Quantity") = IIf(dgDeliveryLocation.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, 1)
                                '    dsPackagingVar.AcceptChanges()
                                'End If
                                Exit Sub
                            End If
                        End If

                    End If


                    '-------Added By Prasad for Checking If Combo Has Decimal Validation
                    Dim resultCombo As DataRow() = _dsPackagingDelivery.Tables(0).Select("DeliveryIndex='" + UpdateDeliveryIndex.ToString() + "' ")

                    Dim drCheckIfComboSubVariation As New DataView(_dsPackagingDelivery.Tables("PackagingDelivery"), "PackagingIndex='" & UpdatePackagingIndex.ToString() & "' AND IsCombo=True AND DeliveryIndex='" + UpdateDeliveryIndex.ToString() + "'", "", DataViewRowState.CurrentRows)
                    If drCheckIfComboSubVariation.Count > 0 Then

                        Dim OrderQty = currentQty.ToString.Split(".")
                        If OrderQty.Length = 2 Then
                            If OrderQty(1) > 0 Then
                                ShowMessage("Order Qty For Combo should not be in decimal", "Information")
                                dgDeliveryLocation.Item(dgDeliveryLocation.Row, "Quantity") = drCheckIfComboSubVariation(0)("Quantity")
                                If resultCombo.Length > 0 Then
                                    resultCombo(0)("Quantity") = IIf(dgDeliveryLocation.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, drCheckIfComboSubVariation(0)("Quantity"))
                                    _dsPackagingDelivery.Tables("PackagingDelivery").AcceptChanges()
                                End If
                                'If drView1.Length > 0 Then
                                '    drView1(0)("Quantity") = IIf(dgDeliveryLocation.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, 1)
                                '    dsPackagingVar.AcceptChanges()
                                'End If
                                Exit Sub
                            End If
                        End If

                    End If
                    ''dtAscii = _dsPackagingDelivery.Tables("PackagingDelivery").Select("PackagingIndex='" & UpdatePackagingIndex.ToString() & "'").CopyToDataTable()
                    Dim resultSum As DataRow() = dsPackagingDelivery.Tables(0).Select("IsHeader = False and PackagingIndex='" + UpdatePackagingIndex.ToString() + "' and DeliveryIndex <>'" + UpdateDeliveryIndex.ToString() + "'")
                    If resultSum.Length > 0 Then
                        Dim isValidSumObject As Object
                        isValidSumObject = _dsPackagingDelivery.Tables(0).Compute("Sum(Quantity)", "IsHeader = False and PackagingIndex='" + UpdatePackagingIndex.ToString() + "' and DeliveryIndex <>'" + UpdateDeliveryIndex.ToString() + "'")
                        If currentQty + isValidSumObject > baseQty Then
                            ShowMessage("Order quantity is getting exceeded", "SO009 - " & getValueByKey("CLAE04"))
                            Dim resultPrevQty As DataRow() = _dsPackagingDelivery.Tables(0).Select("DeliveryIndex='" + UpdateDeliveryIndex.ToString() + "' ")
                            If resultPrevQty.Length > 0 Then
                                dgDeliveryLocation.Item(CurrentRow, "Quantity") = resultPrevQty(0)("Quantity")
                            Else
                                dgDeliveryLocation.Item(CurrentRow, "Quantity") = 0
                            End If
                            Exit Sub

                        End If
                    End If

                    Dim sumObject As Object
                    sumObject = _dsPackagingDelivery.Tables(0).Compute("Sum(Quantity)", "IsHeader = False and PackagingIndex='" + UpdatePackagingIndex.ToString() + "'")

                    Dim result As DataRow() = _dsPackagingDelivery.Tables(0).Select("DeliveryIndex='" + UpdateDeliveryIndex.ToString() + "' ")

                    If baseQty - ((currentQty + sumObject) - result(0)("Quantity")) > 0 Then
                        headerQty = baseQty - ((currentQty + sumObject) - result(0)("Quantity"))
                    Else
                        If result.Length > 0 Then
                            dgDeliveryLocation.Item(CurrentRow, "Quantity") = result(0)("Quantity")
                        End If
                    End If


                    Dim dvEditorderPickupQty As DataView
                    dvEditorderPickupQty = New DataView(_dsPackagingDelivery.Tables("PackagingDelivery"), "PackagingIndex='" & UpdatePackagingIndex.ToString() & "'", "", DataViewRowState.CurrentRows)
                    If dvEditorderPickupQty.Count > 0 Then
                        For Each drView1 As DataRowView In dvEditorderPickupQty
                            If drView1("IsHeader") = "True" Then
                                '----Added By Prasad if Header Qty 0 then update last qty as Header Qty,header qty can't be 0
                                If headerQty = 0 Then
                                    drView1("Quantity") = currentQty
                                Else
                                    drView1("Quantity") = headerQty
                                End If
                                'drView1("Quantity") = headerQty
                                If drView1("PickUpQty") > headerQty Then
                                    drView1("PickUpQty") = headerQty

                                End If
                                drView1("PendingQty") = drView1("Quantity") - drView1("PickUpQty")
                                '-----New
                                If clsDefaultConfiguration.PackageFiedlsAllowed Then
                                    If dgDeliveryLocation.Item(CurrentRow, "UOM") = "KGS" Then
                                        Dim pickBaseUom = drView1("PickUpQty") * drView1("PckgSize")
                                        Dim pendingQty = (headerQty - pickBaseUom)
                                        drView1("PendingQty") = pendingQty
                                        If drView1("PckgSize") > 0 Then
                                            drView1("PckgQty") = FormatNumber((pendingQty * 1 / drView1("PckgSize")), 3) & " " & drView1("PackagingType")
                                        Else
                                            drView1("PckgQty") = 0
                                        End If
                                    End If
                                End If
                            ElseIf drView1("DeliveryIndex") = UpdateDeliveryIndex.ToString() Then

                                If headerQty = 0 Then
                                    drView1("Quantity") = headerQty
                                Else
                                    drView1("Quantity") = currentQty
                                End If
                                'drView1("Quantity") = currentQty
                                If drView1("PickUpQty") > currentQty Then
                                    drView1("PickUpQty") = currentQty

                                End If
                                drView1("PendingQty") = drView1("Quantity") - drView1("PickUpQty")
                                '--------New
                                If clsDefaultConfiguration.PackageFiedlsAllowed Then
                                    If dgDeliveryLocation.Item(CurrentRow, "UOM") = "KGS" Then
                                        Dim pickBaseUom = drView1("PickUpQty") * drView1("PckgSize")
                                        Dim pendingQty = (currentQty - pickBaseUom)
                                        drView1("PendingQty") = pendingQty
                                        'drView1("PckgQty") = (pendingQty * 1 / drView1("PckgSize")) & " " & drView1("PackagingType")
                                        If drView1("PckgSize") > 0 Then
                                            drView1("PckgQty") = FormatNumber((pendingQty * 1 / drView1("PckgSize")), 3) & " " & drView1("PackagingType")
                                        Else
                                            drView1("PckgQty") = 0
                                        End If
                                    End If
                                End If
                            End If
                            'drView1("Quantity") = 1
                            'drView1.Delete()
                        Next
                        _dsPackagingDelivery.AcceptChanges()
                        'CalculateSalesOrderSummary(dsScan)
                        BindSODeliveryGridData(dsPackagingDelivery.Tables(0))
                        AddButtonControlInDeliveryGrid(1)
                        AddSTRButtonControlInDeliveryGrid(1)
                        dgDeliveryLocation.Select(CurrentRow, 5, True)
                        'DeliveryGridSetting()
                    End If
                    'If dtAscii.Rows.Count > 0 Then
                    '    dtAscii = _dsPackagingDelivery.Tables("PackagingDelivery").Select("packagingindex='" & dtAscii.Rows(0)("packagingindex") & "'").CopyToDataTable()
                    'End If
                    'dgDeliveryLocation.Item(CurrentRow, "Quantity") = 10
                End If
            End If
            If dgDeliveryLocation.Cols(CurrentCell).Name = "DeliveryAddress" Then

                Dim result As DataRow() = _dsPackagingDelivery.Tables(0).Select("DeliveryIndex='" + UpdateDeliveryIndex.ToString() + "' ")
                If result.Length > 0 Then
                    If rbDPNo.Checked Then
                        If rdDelNo.Checked Then
                            Dim dr() = dtTempOrderAddresses.Select("AddressKey='" & clsAdmin.SiteCode & "'")
                            If dr.Length > 0 Then
                                If rbDPYes.Checked = True Then    ''' added by nikhil for popup hide on 13/06/17
                                    If (dr(0)("AddressValue") <> dgDeliveryLocation.Item(CurrentRow, "DeliveryAddress").ToString()) Then
                                        ShowMessage("Please select multi Pickup radio button input as 'YES'", getValueByKey("CLAE04"))
                                        dgDeliveryLocation.Item(CurrentRow, "DeliveryAddress") = dr(0)("AddressValue")
                                        Exit Sub
                                    End If
                                End If
                            End If

                        Else
                            Dim dr() = dtTempOrderAddresses.Select("DefaultAddress=True")
                            If dr.Length > 0 Then
                                If (dr(0)("AddressValue") <> dgDeliveryLocation.Item(CurrentRow, "DeliveryAddress").ToString()) Then
                                    ShowMessage("Please select multi Delivery radio button input as 'YES'", getValueByKey("CLAE04"))
                                    dgDeliveryLocation.Item(CurrentRow, "DeliveryAddress") = dr(0)("AddressValue")
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                    If dgDeliveryLocation.Item(CurrentRow, "DeliveryAddress").ToString() = "-----------------------------" Then
                        dgDeliveryLocation.Item(CurrentRow, "DeliveryAddress") = dtTempOrderAddresses.Rows(0)("AddressValue").ToString
                        result(0)("DeliveryAddress") = dtTempOrderAddresses.Rows(0)("AddressKey").ToString
                        _dsPackagingDelivery.AcceptChanges()
                        Exit Sub
                    End If
                    Dim resultAddr As DataRow() = dtTempOrderAddresses.Select("AddressValue='" + dgDeliveryLocation.Item(CurrentRow, "DeliveryAddress").ToString() + "' and addresstype='Store' ")
                    If resultAddr.Length > 0 Then
                        result(0)("IsCustAddress") = "1"
                        result(0)("DeliveryAddress") = resultAddr(0)("AddressKey") 'dgDeliveryLocation.Item(CurrentRow, "DeliveryAddress")
                    Else
                        Dim resultAddr1 As DataRow() = dtTempOrderAddresses.Select("AddressValue='" + dgDeliveryLocation.Item(CurrentRow, "DeliveryAddress").ToString() + "' and addresstype='Address' ")
                        result(0)("IsCustAddress") = "2"  ''2=cust address,1=store,''=NA
                        result(0)("DeliveryAddress") = resultAddr1(0)("AddressKey")
                    End If
                    '----added by prasad for delivery adress issue
                    For Each resCombAddr As DataRow In _dsPackagingDelivery.Tables(0).Select("PackagingIndex='" + UpdatePackagingIndex.ToString + "'")
                        Dim ResCrntAddress As DataRow() = dtTempOrderAddresses.Select("AddressKey='" + resCombAddr("DeliveryAddress") + "'")
                        If resCombAddr("DeliveryIndex") <> UpdateDeliveryIndex.ToString() Then
                            Dim crntaddr As String
                            If ResCrntAddress.Length > 0 Then
                                crntaddr = ResCrntAddress(0)("AddressValue")
                            Else
                                crntaddr = Nothing
                            End If
                            If dgDeliveryLocation.Item(CurrentRow, "DeliveryAddress") = crntaddr Then
                                If Convert.ToDateTime(resCombAddr("DeliveryDate")).ToShortDateString = Convert.ToDateTime(dgDeliveryLocation.Item(CurrentRow, "DeliveryDate")).ToShortDateString AndAlso Convert.ToDateTime(resCombAddr("DeliveryTime")).ToString("hh:mm tt") = Convert.ToDateTime(dgDeliveryLocation.Item(CurrentRow, "DeliveryTime")).ToString("hh:mm tt") Then
                                    ShowMessage("Combination of Delivery Address cannot be same for a variation added for an item. Please change either of it", "SO009 - " & getValueByKey("CLAE04"))
                                    dgDeliveryLocation.Item(CurrentRow, "DeliveryAddress") = Nothing
                                    result(0)("DeliveryAddress") = Nothing
                                    _dsPackagingDelivery.AcceptChanges()
                                    Exit Sub
                                End If
                            End If
                        End If
                    Next
                    _dsPackagingDelivery.AcceptChanges()
                End If
            End If
            If dgDeliveryLocation.Cols(CurrentCell).Name = "DeliveryDate" Then
                If dgDeliveryLocation.Item(CurrentRow, "DeliveryDate").Date < vCurrentSODateTime.Date Then
                    ShowMessage("Delivery date can not be back dated", "SO009 - " & getValueByKey("CLAE04"))
                    dgDeliveryLocation.Item(CurrentRow, "DeliveryDate") = Nothing
                    ' Disable date
                    Exit Sub
                End If
                Dim result As DataRow() = _dsPackagingDelivery.Tables(0).Select("DeliveryIndex='" + UpdateDeliveryIndex.ToString() + "' ")
                If result.Length > 0 Then
                    result(0)("DeliveryDate") = dgDeliveryLocation.Item(CurrentRow, "DeliveryDate")
                    _dsPackagingDelivery.AcceptChanges()
                End If
                '----added by prasad for delivery adress issue
                For Each resCombAddr As DataRow In _dsPackagingDelivery.Tables(0).Select("PackagingIndex='" + UpdatePackagingIndex.ToString + "'")
                    Dim ResCrntAddress As DataRow() = dtTempOrderAddresses.Select("AddressKey='" + resCombAddr("DeliveryAddress") + "'")
                    If resCombAddr("DeliveryIndex") <> UpdateDeliveryIndex.ToString() Then
                        Dim crntaddr As String
                        If ResCrntAddress.Length > 0 Then
                            crntaddr = ResCrntAddress(0)("AddressValue")
                        Else
                            crntaddr = Nothing
                        End If
                        If dgDeliveryLocation.Item(CurrentRow, "DeliveryAddress") = crntaddr Then
                            If Convert.ToDateTime(resCombAddr("DeliveryDate")).ToShortDateString = Convert.ToDateTime(dgDeliveryLocation.Item(CurrentRow, "DeliveryDate")).ToShortDateString AndAlso Convert.ToDateTime(resCombAddr("DeliveryTime")).ToString("hh:mm tt") = Convert.ToDateTime(dgDeliveryLocation.Item(CurrentRow, "DeliveryTime")).ToString("hh:mm tt") Then
                                ShowMessage("Combination of Delivery Address cannot be same for a variation added for an item. Please change either of it", "SO009 - " & getValueByKey("CLAE04"))
                                dgDeliveryLocation.Item(CurrentRow, "DeliveryAddress") = Nothing
                                result(0)("DeliveryAddress") = Nothing
                                _dsPackagingDelivery.AcceptChanges()
                                Exit Sub
                            End If
                        End If
                    End If
                Next
            End If
            If dgDeliveryLocation.Cols(CurrentCell).Name = "DeliveryTime" Then
                If dgDeliveryLocation.Item(CurrentRow, "DeliveryDate").Date = vCurrentSODateTime.Date Then
                    If dgDeliveryLocation.Item(CurrentRow, "DeliveryTime") < vCurrentSODateTime Then
                        ShowMessage("Delivery time can not be back dated", "SO009 - " & getValueByKey("CLAE04"))
                        dgDeliveryLocation.Item(CurrentRow, "DeliveryTime") = vCurrentSODateTime
                    End If
                End If


                Dim result As DataRow() = _dsPackagingDelivery.Tables(0).Select("DeliveryIndex='" + UpdateDeliveryIndex.ToString() + "' ")
                If result.Length > 0 Then
                    result(0)("DeliveryTime") = dgDeliveryLocation.Item(CurrentRow, "DeliveryTime")
                    _dsPackagingDelivery.AcceptChanges()
                End If
                '----added by prasad for delivery adress issue
                For Each resCombAddr As DataRow In _dsPackagingDelivery.Tables(0).Select("PackagingIndex='" + UpdatePackagingIndex.ToString + "'")
                    Dim ResCrntAddress As DataRow() = dtTempOrderAddresses.Select("AddressKey='" + resCombAddr("DeliveryAddress") + "'")
                    If resCombAddr("DeliveryIndex") <> UpdateDeliveryIndex.ToString() Then
                        Dim crntaddr As String
                        If ResCrntAddress.Length > 0 Then
                            crntaddr = ResCrntAddress(0)("AddressValue")
                        Else
                            crntaddr = Nothing
                        End If
                        If dgDeliveryLocation.Item(CurrentRow, "DeliveryAddress") = crntaddr Then
                            If Convert.ToDateTime(resCombAddr("DeliveryDate")).ToShortDateString = Convert.ToDateTime(dgDeliveryLocation.Item(CurrentRow, "DeliveryDate")).ToShortDateString AndAlso Convert.ToDateTime(resCombAddr("DeliveryTime")).ToString("hh:mm tt") = Convert.ToDateTime(dgDeliveryLocation.Item(CurrentRow, "DeliveryTime")).ToString("hh:mm tt") Then
                                ShowMessage("Combination of Delivery Address cannot be same for a variation added for an item. Please change either of it", "SO009 - " & getValueByKey("CLAE04"))
                                dgDeliveryLocation.Item(CurrentRow, "DeliveryAddress") = Nothing
                                result(0)("DeliveryAddress") = Nothing
                                _dsPackagingDelivery.AcceptChanges()
                                Exit Sub
                            End If
                        End If
                    End If
                Next
            End If
            If dgDeliveryLocation.Cols(CurrentCell).Name.ToUpper() = "PICKUP" Then
                'PickUpQty()
                'Pickup()

                '---User goes to order details tab pckg size (kg) & pckg Qty.(Nos.) columns will be displayed.
                'Pckg qty (Nos.) will be displayed along with its packaging type as per mentioned in add items tab e.g. 20 Tin. (Pckg. Qty. is 20 & Packaging Type is Tin)

                Dim Pickup As Double = IIf(dgDeliveryLocation.Item(CurrentRow, "Pickup") Is DBNull.Value, 0, dgDeliveryLocation.Item(CurrentRow, "Pickup"))
                If dgDeliveryLocation.Item(CurrentRow, "UOM") = "NOS" Then
                    Dim PkupQty = Pickup.ToString.Split(".")
                    If PkupQty.Length = 2 Then
                        If PkupQty(1) > 0 Then
                            ShowMessage("Pickup qty should not be in decimal", "Information")
                            dgDeliveryLocation.Item(dgDeliveryLocation.Row, "PICKUP") = 0.0
                            Exit Sub
                        End If
                    End If
                End If
                If clsDefaultConfiguration.PackageFiedlsAllowed Then
                    If dgDeliveryLocation.Item(CurrentRow, "UOM") = "KGS" Then
                        Pickup = Pickup * dgDeliveryLocation.Item(CurrentRow, "PckgSize")
                    End If
                End If

                dgDeliveryLocation.Item(CurrentRow, "PickUpQty") = Pickup


                Dim vPickupQty As Double = IIf(dgDeliveryLocation.Item(CurrentRow, "PickUpQty") Is DBNull.Value, 0, dgDeliveryLocation.Item(CurrentRow, "PickUpQty"))
                Dim orderQty As Double = IIf(dgDeliveryLocation.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, dgDeliveryLocation.Item(CurrentRow, "Quantity"))
                Dim pendingQty As Double = IIf(dgDeliveryLocation.Item(CurrentRow, "PendingQty") Is DBNull.Value, 0, dgDeliveryLocation.Item(CurrentRow, "PendingQty"))
                Dim result As DataRow() = _dsPackagingDelivery.Tables(0).Select("DeliveryIndex='" + UpdateDeliveryIndex.ToString() + "' ")
                If vPickupQty > orderQty Then
                    ShowMessage(getValueByKey("SO009"), "SO009 - " & getValueByKey("CLAE04"))

                    If result.Length > 0 Then
                        dgDeliveryLocation.Item(CurrentRow, "PickUpQty") = result(0)("PickUpQty")
                        If clsDefaultConfiguration.PackageFiedlsAllowed Then
                            If dgDeliveryLocation.Item(CurrentRow, "UOM") = "KGS" Then
                                If result(0)("PckgSize") > 0 Then
                                    dgDeliveryLocation.Item(CurrentRow, "PickUp") = result(0)("PickUpQty") / result(0)("PckgSize")
                                End If
                            End If
                        End If
                    End If
                    Exit Sub
                Else
                    dgDeliveryLocation.Item(CurrentRow, "PickUpQty") = vPickupQty
                    If result.Length > 0 Then
                        result(0)("PickUpQty") = vPickupQty
                        _dsPackagingDelivery.AcceptChanges()
                    End If

                End If
                ''------------
                If clsDefaultConfiguration.PackageFiedlsAllowed Then
                    If dgDeliveryLocation.Item(CurrentRow, "UOM") = "KGS" Then
                        Dim pickBaseUom = vPickupQty
                        pendingQty = (orderQty - pickBaseUom)
                        dgDeliveryLocation.Item(CurrentRow, "PendingQty") = pendingQty
                        If dgDeliveryLocation.Item(CurrentRow, "PckgSize") > 0 Then
                            dgDeliveryLocation.Item(CurrentRow, "PckgQty") = FormatNumber((pendingQty * 1 / dgDeliveryLocation.Item(CurrentRow, "PckgSize")), 3) & " " & dgDeliveryLocation.Item(CurrentRow, "PackagingType")
                        Else
                            dgDeliveryLocation.Item(CurrentRow, "PckgQty") = 0
                        End If

                        result(0)("PendingQty") = dgDeliveryLocation.Item(CurrentRow, "PendingQty")
                        result(0)("PckgQty") = dgDeliveryLocation.Item(CurrentRow, "PckgQty")
                        result(0)("PickUpQty") = pickBaseUom

                        _dsPackagingDelivery.AcceptChanges()
                    End If
                Else
                    dgDeliveryLocation.Item(CurrentRow, "PendingQty") = orderQty - vPickupQty
                End If


                dgDeliveryLocation.Item(CurrentRow, "PendingQty") = orderQty - vPickupQty

                Dim resultPack1 As DataRow() = _dsPackagingVar.Tables(0).Select("packagingindex='" + UpdatePackagingIndex.ToString() + "' ") 'vipin 05.10.2017
                '   Dim resultPack1 As DataRow() = _dsPackagingVar.Tables(0).Select("DeliveryIndex='" + UpdatePackagingIndex.ToString() + "' ")

                ' Dim resultPack1 As DataRow() = _dsPackagingVar.Tables(0).Select("ArticleType='Combo'")     ''.Select("DeliveryIndex='" + UpdatePackagingIndex.ToString() + "' ")
                If _dsPackagingDelivery.Tables("PackagingDelivery").Rows(0)("ArticleType") = "Combo" Then
                    If resultPack1.Length > 0 Then
                        Dim editPickQty = CDbl(_dsPackagingDelivery.Tables("PackagingDelivery").Compute("SUM(PickUpQty)", "PackagingIndex='" + UpdatePackagingIndex.ToString() + "'"))
                        ' Dim editPickQty = CDbl(_dsPackagingDelivery.Tables("PackagingDelivery").Compute("SUM(PickUpQty)", ""))   ''PackagingIndex='" + UpdatePackagingIndex.ToString() + "'
                        If editPickQty > 0 Then
                            'TotalSalesQty = resultPack(0)("PickupQty") + resultPack(0)("DeliveredQty")
                            NetArticleRate = Math.Round(resultPack1(0)("NetAmount") / resultPack1(0)("Quantity"), 3)
                            'drPickupQty("MinPayAmt") = Math.Round(((drPickupQty("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate), 3)
                            ''drPickupQty("PickupQty") = grdScanItem.Item(CurrentRow, "PickupQty")
                            ''RecalculateLine(drPickupQty.Row)
                            'drPickupQty("TotalPickUpAmt") = (drPickupQty("PickupQty") * NetArticleRate)

                            resultPack1(0)("PickUpQty") = editPickQty
                            resultPack1(0)("minpayamt") = NetArticleRate * editPickQty
                            resultPack1(0)("totalpickupamt") = NetArticleRate * editPickQty
                            '-----Added By Prasad for Calculating Tax PickupWise of Savoy Client
                            If clsDefaultConfiguration.IsSavoy Then
                                Dim TaxCalc As Double = 0
                                Dim TaxValue As Double = 0
                                If Not dsMain.Tables("SalesOrderTaxDtls") Is Nothing AndAlso dsMain.Tables("SalesOrderTaxDtls").Rows.Count > 0 Then
                                    Dim dr() = dsMain.Tables("SalesOrderTaxDtls").Select("Status=True")
                                    If dr.Count > 0 Then
                                        TaxValue = dr(0)("TaxValue")
                                    End If
                                End If
                                TaxCalc = ((resultPack1(0)("SellingPrice") * resultPack1(0)("PickupQty")) / dsPackagingVar.Tables(0).Compute("Sum(GrossAmt)", " ")) * TaxValue
                                resultPack1(0)("minpayamt") = resultPack1(0)("minpayamt") + TaxCalc
                                resultPack1(0)("totalpickupamt") = resultPack1(0)("totalpickupamt") + TaxCalc
                            End If
                            '----------------------------------------------------------------------

                        Else
                            resultPack1(0)("PickUpQty") = 0
                            resultPack1(0)("minpayamt") = 0
                            resultPack1(0)("totalpickupamt") = 0
                        End If
                        _dsPackagingVar.AcceptChanges()
                    End If
                Else
                    ' Dim resultPack As DataRow() = _dsPackagingVar.Tables(0).Select("ArticleType='Single'")
                    Dim resultPack As DataRow() = _dsPackagingVar.Tables(0).Select("packagingindex='" + UpdatePackagingIndex.ToString() + "' ") 'vipin
                    If resultPack.Length > 0 Then
                        Dim editPickQty = CDbl(_dsPackagingDelivery.Tables("PackagingDelivery").Compute("SUM(PickUpQty)", "PackagingIndex='" + UpdatePackagingIndex.ToString() + "'"))
                        ' Dim editPickQty = CDbl(_dsPackagingDelivery.Tables("PackagingDelivery").Compute("SUM(PickUpQty)", ""))
                        If editPickQty > 0 Then
                            'TotalSalesQty = resultPack(0)("PickupQty") + resultPack(0)("DeliveredQty")
                            NetArticleRate = Math.Round(resultPack(0)("NetAmount") / resultPack(0)("Quantity"), 3)
                            'drPickupQty("MinPayAmt") = Math.Round(((drPickupQty("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate), 3)
                            ''drPickupQty("PickupQty") = grdScanItem.Item(CurrentRow, "PickupQty")
                            ''RecalculateLine(drPickupQty.Row)
                            'drPickupQty("TotalPickUpAmt") = (drPickupQty("PickupQty") * NetArticleRate)

                            resultPack(0)("PickUpQty") = editPickQty
                            resultPack(0)("minpayamt") = NetArticleRate * editPickQty
                            resultPack(0)("totalpickupamt") = NetArticleRate * editPickQty
                            '-----Added By Prasad for Calculating Tax PickupWise of Savoy Client
                            If clsDefaultConfiguration.IsSavoy Then
                                Dim TaxCalc As Double = 0
                                Dim TaxValue As Double = 0
                                If Not dsMain.Tables("SalesOrderTaxDtls") Is Nothing AndAlso dsMain.Tables("SalesOrderTaxDtls").Rows.Count > 0 Then
                                    Dim dr() = dsMain.Tables("SalesOrderTaxDtls").Select("Status=True")
                                    If dr.Count > 0 Then
                                        TaxValue = dr(0)("TaxValue")
                                    End If
                                End If
                                TaxCalc = ((resultPack(0)("SellingPrice") * resultPack(0)("PickupQty")) / dsPackagingVar.Tables(0).Compute("Sum(GrossAmt)", " ")) * TaxValue
                                resultPack(0)("minpayamt") = resultPack(0)("minpayamt") + TaxCalc
                                resultPack(0)("totalpickupamt") = resultPack(0)("totalpickupamt") + TaxCalc
                            End If
                            '----------------------------------------------------------------------

                        Else
                            resultPack(0)("PickUpQty") = 0
                            resultPack(0)("minpayamt") = 0
                            resultPack(0)("totalpickupamt") = 0
                        End If

                        _dsPackagingVar.AcceptChanges()
                    End If
                End If

                Try

                    'StockQty = objCM.GetStocks(clsAdmin.SiteCode, grdScanItem.Item(CurrentRow, "EAN"), grdScanItem.Item(CurrentRow, "ArticleCode"), clsDefaultConfiguration.IsBatchManagementReq, grdScanItem.Item(CurrentRow, "BatchBarcode"))
                    'If CDbl(StockQty) <= 0 Then
                    '    If clsDefaultConfiguration.NegativeInventoryAllowed = False Then
                    '        ShowMessage(getValueByKey("SO001"), "SO001 - " & getValueByKey("CLAE04"))
                    '        'ShowMessage("Article out of Stock.", "Information")                        
                    '        e.Cancel = True
                    '        Exit Sub
                    '    End If
                    'End If
                    If Not (vPickupQty >= 0) Then
                        ShowMessage(getValueByKey("SO008"), "SO008 - " & getValueByKey("CLAE05"))
                        'ShowMessage("PickUp Quantity cannot less than 1.", "PickUp Quantity Information")
                        grdScanItem.Item(CurrentRow, "PickupQty") = 0
                    End If

                    Dim dvPickupQty As DataView
                    'If IsDBNull(grdScanItem.Item(CurrentRow, "BatchBarcode")) = False Then
                    '    dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode = '" & grdScanItem.Item(CurrentRow, "BatchBarcode") & "'" & addCondtionRow, "", DataViewRowState.CurrentRows)
                    'Else
                    dvPickupQty = New DataView(_dsPackagingVar.Tables("PackagingMaterial"), "DeliveryIndex='" + UpdateDeliveryIndex.ToString() + "' ", "", DataViewRowState.CurrentRows)
                    'End If

                    If dvPickupQty.Count > 0 Then
                        dvPickupQty.AllowEdit = True

                        'Rakesh-01.10.2013:7835-->Check article stock balance quantity
                        If (Not clsDefaultConfiguration.NegativeInventoryAllowed) Then
                            Dim objCommon As New clsCommon
                            Dim articleCode = dvPickupQty(0)("articlecode")
                            Dim articleEAN = dvPickupQty(0)("ean")
                            Dim iPickUpQty = dvPickupQty(0)("PickUpQty")

                            Dim StockQty As Double = objCommon.GetStocks(clsAdmin.SiteCode, articleEAN, articleCode, True)

                            If (StockQty < iPickUpQty) Then
                                ShowMessage(String.Format(getValueByKey("SB015"), StockQty), "SB015 - " & getValueByKey("CLAE04"))
                                dgDeliveryLocation.Item(CurrentRow, "PickUpQty") = 0
                            End If
                        End If

                        For Each drPickupQty As DataRowView In dvPickupQty

                            If dgDeliveryLocation.Item(CurrentRow, "PickupQty") <= dgDeliveryLocation.Item(CurrentRow, "Quantity") Then
                                'drPickupQty("PickupQty") = dgDeliveryLocation.Item(CurrentRow, "PickupQty")

                                'TotalSalesQty = drPickupQty("PickupQty") '+ drPickupQty("DeliveredQty")
                                'NetArticleRate = Math.Round(drPickupQty("NetAmount") / drPickupQty("Quantity"), 3)
                                'drPickupQty("MinPayAmt") = Math.Round(((drPickupQty("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate), 3)
                                ''drPickupQty("PickupQty") = grdScanItem.Item(CurrentRow, "PickupQty")
                                ''RecalculateLine(drPickupQty.Row)
                                'drPickupQty("TotalPickUpAmt") = (drPickupQty("PickupQty") * NetArticleRate)
                            Else
                                dgDeliveryLocation.Item(CurrentRow, "PickupQty") = 0
                                'dgDeliveryLocation.Item(CurrentRow, "TotalPickUpAmt") = 0
                                ShowMessage(getValueByKey("SO009"), "SO009 - " & getValueByKey("CLAE04"))
                                'ShowMessage("Pick Up Quantity cannot greater than Order Quantity.", "Information")
                            End If
                        Next
                        _dsPackagingVar.AcceptChanges()
                    End If
                    UpdateItemData()
                    CalculateSalesOrderSummary(dsScan)

                Catch ex As Exception
                    ShowMessage(ex.Message, getValueByKey("CLAE05"))
                End Try



            End If

            BindTextValue() 'aaded by vipin

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Update Scan Article Quantity, PickupQty and Delivery Date
    ''' </summary>
    ''' <param name="sender">Selected Row</param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdScanItem_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdScanItem.AfterEdit
        Try
            Dim x As New DataSet
            x = dsMain
            If grdScanItem.Row = -1 Then Exit Sub
            Dim CurrentCell As Integer = e.Col
            Dim CurrentRow As Integer = grdScanItem.Row '-- e.Row
            Dim UpdatePackagingIndex As Integer = IIf(grdScanItem.Item(CurrentRow, "PackagingIndex") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PackagingIndex"))
            Dim result As DataRow() = dsScan.Tables(0).Select("PackagingIndex='" + UpdatePackagingIndex.ToString() + "' ")
            Dim resultPackVar As DataRow() = dsPackagingVar.Tables(0).Select("PackagingIndex='" + UpdatePackagingIndex.ToString() + "' ")
            Dim IsComboRow As String = IIf(grdScanItem.Item(CurrentRow, "IsCombo") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "IsCombo"))
            Dim CmbSrNo As String = grdScanItem.Item(grdScanItem.Row, "RowIndex")  '' $$ added by nikhil 

            ComboTax = dsScan.Tables(0).Select("SrNo='" + CmbSrNo.ToString() + "' ").CopyToDataTable  '' $$ added by nikhil 

            If grdScanItem.Cols(CurrentCell).Name = "PackagingMaterial" AndAlso IsComboRow = "True" Then
                Dim dtCheck As New DataTable
                dtCheck = dtPackagingBox
                Dim PackMaterailName As String = IIf(grdScanItem.Item(CurrentRow, "PackagingMaterial") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PackagingMaterial"))
                Dim resultCombo As DataRow() = dtPackagingBox.Select("ArticleShortName='" + PackMaterailName + "' ")
                If resultCombo.Length > 0 Then
                    dtCheck = objItemSch.GetEANData(clsAdmin.SiteCode, resultCombo(0)("articleCode"), clsAdmin.LangCode, "")
                End If


                ''update dsscan and packvar for combo
                Dim resultDsScan As DataRow() = dsScan.Tables(0).Select("PackagingIndex='" + UpdatePackagingIndex.ToString() + "' ")
                If result.Length > 0 Then
                    'dtCheck = objItemSch.GetEANData(clsAdmin.SiteCode, resultCombo(0)("articleCode"), clsAdmin.LangCode, "")
                    result(0)("PackagingMaterial") = IIf(grdScanItem.Item(CurrentRow, "PackagingMaterial") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PackagingMaterial"))
                    resultPackVar(0)("PackagingMaterial") = IIf(grdScanItem.Item(CurrentRow, "PackagingMaterial") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PackagingMaterial"))
                    '' Added  by nikhil

                    If clsDefaultConfiguration.IsNewSalesOrder Then

                    Else
                        result(0)("SellingPrice") = dtCheck(0)("SellingPrice")
                    End If

                    result(0)("EAN") = dtCheck(0)("EAN")
                    result(0)("articlecode") = dtCheck(0)("articlecode")
                    ' resultPackVar(0)("SellingPrice") = dtCheck(0)("SellingPrice")
                    resultPackVar(0)("EAN") = dtCheck(0)("EAN")
                    ' result(0)("Discount") = 0  ##
                    '' added by nikhil
                    If clsDefaultConfiguration.IsNewSalesOrder Then
                        If result(0)("ArticleType") = "Combo" Then
                            If lblArticleCombo.Rows.Count > 0 Then
                                '   result(0)("GrossAmt") = CDbl(lblArticleCombo.Rows(0)("GrossAmount"))
                                'remove roundoff from grid for net amount 
                                result(0)("NetAmount") = Math.Round(CDbl((result(0)("GrossAmt") * result(0)("Quantity")) - CDbl(result(0)("Discount"))) + (CDbl(result(0)("TotalTaxAmt"))), 2)

                                ' result(0)("ArticleLevelTax") = ((result(0)("SellingPrice") * result(0)("Qunatity")) - result(0)("Discount")) * (CDbl(lblArticleCombo.Rows(0)("Tax")) / 100)
                            End If

                            ' resultPackVar(0)("Discount") = 0   ##
                            resultPackVar(0)("articlecode") = dtCheck(0)("articlecode")

                        Else


                            result(0)("NetAmount") = dtCheck(0)("SellingPrice") * (IIf(grdScanItem.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "Quantity")))
                            result(0)("GrossAmt") = dtCheck(0)("SellingPrice") * (IIf(grdScanItem.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "Quantity")))
                            resultPackVar(0)("Discount") = 0
                            resultPackVar(0)("articlecode") = dtCheck(0)("articlecode")
                            resultPackVar(0)("NetAmount") = Math.Round((dtCheck(0)("SellingPrice") * (IIf(grdScanItem.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "Quantity")))) - grdScanItem.Item(CurrentRow, "Discount"), 2) '##
                            resultPackVar(0)("GrossAmt") = dtCheck(0)("SellingPrice") * (IIf(grdScanItem.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "Quantity")))
                        End If
                    End If
                End If
                If resultPackVar.Length > 0 Then
                    '  resultPackVar(0)("SellingPrice") = dtCheck(0)("SellingPrice")   ''' need to check
                    '' resultPackVar(0)("Discount") = 0  ### 
                    '' added by nikhil  29/06/17 
                    If clsDefaultConfiguration.IsNewSalesOrder Then
                        If resultPackVar(0)("ArticleType") = "Combo" Then
                            '' comment by ketan prodution Issue assign wrong data to gross amt 
                            'resultPackVar(0)("SellingPrice") = CDbl(lblArticleCombo.Rows(0)("GrossAmount"))
                            'resultPackVar(0)("GrossAmt") = CDbl(lblArticleCombo.Rows(0)("GrossAmount"))
                            'resultPackVar(0)("NetAmount") = Math.Round(CDbl((lblArticleCombo.Rows(0)("GrossAmount") * IIf(grdScanItem.Item(CurrentRow, "Quantity") Is DBNull.Value, 1, grdScanItem.Item(CurrentRow, "Quantity"))) - CDbl(resultPackVar(0)("Discount"))) + CDbl(resultPackVar(0)("TotalTaxAmt")), 2)
                            ' resultPackVar(0)("ArticleLevelTax") = lblArticleCombo.Rows(0)("TaxAmount")
                        Else
                            resultPackVar(0)("GrossAmt") = Math.Round(dtCheck(0)("SellingPrice") * (IIf(grdScanItem.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "Quantity"))), 2)
                            resultPackVar(0)("NetAmount") = Math.Round(dtCheck(0)("SellingPrice") * (IIf(grdScanItem.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "Quantity"))), 2)

                        End If
                    End If
                    resultPackVar(0)("EAN") = dtCheck(0)("EAN")
                    resultPackVar(0)("articlecode") = dtCheck(0)("articlecode")
                    resultPackVar(0)("PackagingMaterial") = IIf(grdScanItem.Item(CurrentRow, "PackagingMaterial") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PackagingMaterial"))
                End If

                'For drindex = 0 To dtDocTaxes.Rows.Count - 1
                '    'dr("TaxValue") = dr("TaxValue") + dtDocTaxes.Rows(drindex)("TaxAmt")
                '    result(0)("TotalTaxAmt") = result(0)("TotalTaxAmt") + dtDocTaxes.Rows(drindex)("TaxAmt")
                '    result(0)("NetAmount") = result(0)("NetAmount") + dtDocTaxes.Rows(drindex)("TaxAmt")
                '    If dtDocTaxes.Rows(drindex)("Type") = "Exclusive" Then
                '        result(0)("ExclTaxAmt") = result(0)("ExclTaxAmt") + dtDocTaxes.Rows(drindex)("TaxAmt")
                '    Else
                '        result(0)("IncTaxAmt") = result(0)("IncTaxAmt") + dtDocTaxes.Rows(drindex)("TaxAmt")
                '    End If


                '    resultPackVar(0)("TotalTaxAmt") = resultPackVar(0)("TotalTaxAmt") + dtDocTaxes.Rows(drindex)("TaxAmt")
                '    resultPackVar(0)("NetAmount") = resultPackVar(0)("NetAmount") + dtDocTaxes.Rows(drindex)("TaxAmt")
                '    If dtDocTaxes.Rows(drindex)("Type") = "Exclusive" Then
                '        resultPackVar(0)("ExclTaxAmt") = resultPackVar(0)("ExclTaxAmt") + dtDocTaxes.Rows(drindex)("TaxAmt")
                '    Else
                '        resultPackVar(0)("IncTaxAmt") = resultPackVar(0)("IncTaxAmt") + dtDocTaxes.Rows(drindex)("TaxAmt")
                '    End If



                'Next

                'ResetNewTax(True, dtDocTaxes)
                dsScan.AcceptChanges()
                dsPackagingVar.AcceptChanges()

                ''
            Else
                If grdScanItem.Cols(CurrentCell).Name = "PackagingMaterial" Then
                    If result.Length > 0 Then
                        result(0)("PackagingMaterial") = IIf(grdScanItem.Item(CurrentRow, "PackagingMaterial") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PackagingMaterial"))
                        dsScan.AcceptChanges()
                    End If


                    If dsPackagingVar.Tables(0).Rows.Count > 0 Then
                        Dim resultVar As DataRow() = dsPackagingVar.Tables(0).Select("PackagingIndex='" + UpdatePackagingIndex.ToString() + "' ")
                        resultVar(0)("PackagingMaterial") = IIf(grdScanItem.Item(CurrentRow, "PackagingMaterial") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PackagingMaterial"))
                        dsPackagingVar.AcceptChanges()
                    End If
                    'MessageBox.Show(IIf(grdScanItem.Item(CurrentRow, "PackagingMaterial") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PackagingMaterial")))
                End If
            End If

            Dim ComboSrNo = grdScanItem.Item(grdScanItem.Row, "RowIndex")
            Dim addCondtionRow As String = String.Empty
            If DtSoBulkComboHdr.Rows.Count > 0 Then
                Dim drHdr() = DtSoBulkComboHdr.Select("ComboSrNo=" & ComboSrNo)
                If drHdr.Count > 0 Then
                    addCondtionRow = " AND RowIndex =" & ComboSrNo
                End If
            End If

            If grdScanItem.Cols(CurrentCell).Name = "PackagingType" Then
                If result.Length > 0 Then
                    result(0)("PackagingType") = IIf(grdScanItem.Item(CurrentRow, "PackagingType") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PackagingType"))
                    dsScan.AcceptChanges()
                End If



                If resultPackVar.Length > 0 Then
                    resultPackVar(0)("PackagingType") = IIf(grdScanItem.Item(CurrentRow, "PackagingType") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PackagingType"))
                    dsPackagingVar.AcceptChanges()
                End If

                'MessageBox.Show(IIf(grdScanItem.Item(CurrentRow, "PackagingMaterial") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PackagingMaterial")))
            End If
            If grdScanItem.Cols(CurrentCell).Name = "PckgSize" Then
                Dim packSize = IIf(grdScanItem.Item(CurrentRow, "PckgSize") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PckgSize"))
                Dim cQty = IIf(grdScanItem.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "Quantity"))
                '------------------------------------
                If clsDefaultConfiguration.PackageFiedlsAllowed Then
                    If grdScanItem.Item(CurrentRow, "UOM") = "KGS" Then
                        If packSize > 0 Then
                            Dim var = cQty / packSize
                            Dim varQty = var.ToString.Split(".")
                            If varQty.Length = 2 Then
                                If varQty(1) > 0 Then
                                    ShowMessage("Package Quantity should not be in decimal", "Information")
                                    grdScanItem.Item(CurrentRow, "PckgSize") = 0
                                    grdScanItem.Item(CurrentRow, "PckgQty") = 0
                                    If result.Length > 0 Then
                                        result(0)("PckgSize") = 0
                                        result(0)("PckgQty") = 0
                                    End If
                                    If resultPackVar.Length > 0 Then
                                        resultPackVar(0)("PckgSize") = 0
                                        resultPackVar(0)("PckgQty") = 0
                                    End If
                                    dsScan.AcceptChanges()
                                    dsPackagingVar.AcceptChanges()
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                End If
                '------------------------------------
                If IsApplyPromotion = True Then
                    rbbtnClrAllPromo_Click(sender, e)
                End If
                If result.Length > 0 Then
                    result(0)("PckgSize") = packSize
                    resultPackVar(0)("PckgSize") = packSize
                    dsScan.AcceptChanges()
                End If


                If resultPackVar.Length > 0 Then
                    resultPackVar(0)("PckgSize") = packSize
                End If
                ' Dim cQty = IIf(grdScanItem.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "Quantity"))
                Dim cPckSize = packSize
                'Dim cPckQty = IIf(grdScanItem.Item(CurrentRow, "PckgQty") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PckgQty"))

                If cQty > 0 AndAlso cPckSize > 0 Then
                    If result.Length > 0 Then
                        result(0)("PckgQty") = cQty / cPckSize

                    End If
                    resultPackVar(0)("PckgQty") = cQty / cPckSize
                End If
                If clsDefaultConfiguration.PackageFiedlsAllowed Then
                    If grdScanItem.Item(CurrentRow, "ArticleType") = "Combo" Then
                        If result.Length > 0 Then
                            result(0)("PckgQty") = 0
                        End If

                        resultPackVar(0)("PckgQty") = 0
                    End If
                End If

                If packSize = 0 Then
                    If result.Length > 0 Then
                        result(0)("PckgQty") = 0
                    End If
                    resultPackVar(0)("PckgQty") = 0
                End If

                dsScan.AcceptChanges()

                dsPackagingVar.AcceptChanges()
                'MessageBox.Show(IIf(grdScanItem.Item(CurrentRow, "PackagingMaterial") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PackagingMaterial")))
            End If
            If grdScanItem.Cols(CurrentCell).Name = "PckgQty" Then
                Dim packQty = IIf(grdScanItem.Item(CurrentRow, "PckgQty") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PckgQty"))
                If clsDefaultConfiguration.PackageFiedlsAllowed Then
                    If grdScanItem.Item(CurrentRow, "UOM") = "KGS" Then
                        '------------
                        Dim pkgQty = packQty.ToString.Split(".")
                        If pkgQty.Length = 2 Then
                            If pkgQty(1) > 0 Then
                                ShowMessage("Package Quantity should not be in decimal", "Information")
                                grdScanItem.Item(CurrentRow, "PckgSize") = 0
                                grdScanItem.Item(CurrentRow, "PckgQty") = 0
                                If result.Length > 0 Then
                                    result(0)("PckgSize") = 0
                                    result(0)("PckgQty") = 0
                                End If
                                If resultPackVar.Length > 0 Then
                                    resultPackVar(0)("PckgSize") = 0
                                    resultPackVar(0)("PckgQty") = 0
                                End If
                                dsScan.AcceptChanges()
                                dsPackagingVar.AcceptChanges()
                                Exit Sub
                            End If
                        End If
                        '----------------
                    End If
                End If
                If IsApplyPromotion = True Then
                    rbbtnClrAllPromo_Click(sender, e)
                End If
                Dim foundRow As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("IsDocumentLevel = True")
                If foundRow.Count > 0 Then
                    ResetDocTax(True, dtDocTaxes)
                    dtDocTaxes.Clear()
                End If


                If result.Length > 0 Then
                    result(0)("PckgQty") = packQty
                    resultPackVar(0)("PckgQty") = packQty
                    dsScan.AcceptChanges()
                End If
                If resultPackVar.Length > 0 Then
                    resultPackVar(0)("PckgQty") = packQty
                End If

                'Dim cQty = IIf(grdScanItem.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "Quantity"))
                Dim cPckSize = IIf(grdScanItem.Item(CurrentRow, "PckgSize") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PckgSize"))
                Dim cPckQty = packQty
                If cPckQty > 0 AndAlso cPckSize > 0 Then
                    If result.Length > 0 Then
                        result(0)("Quantity") = cPckQty * cPckSize
                        'result(0)("NetAmount") = IIf(grdScanItem.Item(CurrentRow, "SellingPrice") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "SellingPrice")) * result(0)("Quantity")
                        'result(0)("GrossAmt") = IIf(grdScanItem.Item(CurrentRow, "SellingPrice") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "SellingPrice")) * result(0)("Quantity")

                    End If
                    resultPackVar(0)("Quantity") = cPckQty * cPckSize
                    'resultPackVar(0)("NetAmount") = IIf(grdScanItem.Item(CurrentRow, "SellingPrice") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "SellingPrice")) * resultPackVar(0)("Quantity")
                    'resultPackVar(0)("GrossAmt") = IIf(grdScanItem.Item(CurrentRow, "SellingPrice") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "SellingPrice")) * resultPackVar(0)("Quantity")
                    GoTo QtyUpdate
                End If


                dsScan.AcceptChanges()

                dsPackagingVar.AcceptChanges()


                'MessageBox.Show(IIf(grdScanItem.Item(CurrentRow, "PackagingMaterial") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PackagingMaterial")))
            End If

            'If IsDBNull(grdScanItem.Item(CurrentRow, "BatchBarcode")) = False Then
            '    dvCurrentQty = New DataView(dsScan.Tables(0), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode = '" & grdScanItem.Item(CurrentRow, "BatchBarcode") & "'" & addCondtionRow, "", DataViewRowState.CurrentRows)
            'Else
            dvCurrentQty = New DataView(dsScan.Tables(0), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "'" & addCondtionRow, "", DataViewRowState.CurrentRows) ' & "' And BatchBarcode IS NULL"
            'End If
            'Dim CurrentQty As Double = dvCurrentQty.ToTable.Rows(0).Item("Quantity")

            'If grdScanItem.Cols(CurrentCell).Name = "Quantity" Then  commented to correct get netamount calculate after price change
            If grdScanItem.Cols(CurrentCell).Name = "Quantity" Or grdScanItem.Cols(CurrentCell).Name = "SellingPrice" Then
QtyUpdate:
                Try
                    Dim foundRow As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("IsDocumentLevel = True")
                    If foundRow.Count > 0 Then
                        ResetDocTax(True, dtDocTaxes)
                        dtDocTaxes.Rows.Clear()
                    End If



                    Dim vOrderQty As Double = IIf(grdScanItem.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "Quantity"))
                    Dim vPickupQty As Double = IIf(grdScanItem.Item(CurrentRow, "PickupQty") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PickupQty"))
                    Dim vStock As Double = IIf(grdScanItem.Item(CurrentRow, "Stock") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "Stock"))
                    Dim cQty = IIf(grdScanItem.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "Quantity"))
                    Dim cPckSize = IIf(grdScanItem.Item(CurrentRow, "PckgSize") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PckgSize"))
                    Dim ArticleType = IIf(grdScanItem.Item(CurrentRow, "ArticleType") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "ArticleType"))
                    Dim SellingPrice As Double = IIf(grdScanItem.Item(CurrentRow, "SellingPrice") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "SellingPrice"))
                    Dim Discount As Double = IIf(grdScanItem.Item(CurrentRow, "Discount") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "Discount"))
                    If Not (vOrderQty > 0) Then
                        ShowMessage(getValueByKey("SO005"), "SO005 - " & getValueByKey("CLAE04"))
                        'ShowMessage("Order Quantity cannot less than 1.", "Order Quantity Information")
                        grdScanItem.Item(CurrentRow, "Quantity") = 1
                        Exit Sub
                    ElseIf Not (vOrderQty >= vPickupQty) Then
                        ShowMessage(getValueByKey("SO006"), "SO006 - " & getValueByKey("CLAE04"))
                        'ShowMessage("Order Quantity cannot less than PickUp Quantity.", "Order Quantity Information")
                        grdScanItem.Rows(e.Row)("Quantity") = _iArticleQtyBeforeChange

                        ''result(0)("ArticleLevelTax") = ((result(0)("SellingPrice") * result(0)("Qunatity")) - result(0)("Discount")) * (CDbl(lblArticleCombo.Rows(0)("Tax")) / 100)
                        Exit Sub

                        'Rakesh-01.10.2013:7998-->Commented for Check article stock balance quantity
                        'ElseIf Not (vOrderQty <= vStock) AndAlso clsDefaultConfiguration.NegativeInventoryAllowed = False Then
                        '    ShowMessage(getValueByKey("SO007"), "SO007 - " & getValueByKey("CLAE04"))
                        '    'ShowMessage("Order Quantity cannot greater than Stock Quantity.", "Order Quantity Information")
                        '    grdScanItem.Item(CurrentRow, "Quantity") = CurrentQty
                        '    Exit Sub
                    End If
                    '-----------------------------------
                    If clsDefaultConfiguration.PackageFiedlsAllowed Then
                        If grdScanItem.Item(CurrentRow, "UOM") = "KGS" Then
                            If cPckSize > 0 Then
                                Dim var = cQty / cPckSize
                                Dim varQty = var.ToString.Split(".")
                                If varQty.Length = 2 Then
                                    If varQty(1) > 0 Then
                                        ShowMessage("Package Quantity should not be in decimal", "Information")
                                        grdScanItem.Item(CurrentRow, "Quantity") = 1
                                        grdScanItem.Item(CurrentRow, "PckgSize") = 0
                                        grdScanItem.Item(CurrentRow, "PckgQty") = 0
                                        If result.Length > 0 Then
                                            result(0)("PckgSize") = 0
                                            result(0)("PckgQty") = 0
                                        End If
                                        If resultPackVar.Length > 0 Then
                                            resultPackVar(0)("PckgSize") = 0
                                            resultPackVar(0)("PckgQty") = 0
                                        End If
                                        dsScan.AcceptChanges()
                                        dsPackagingVar.AcceptChanges()
                                        Exit Sub
                                    End If
                                End If
                            End If
                        End If
                    End If
                    '-----------------------------------
                    If IsApplyPromotion = True Then
                        RemoveApplyPromotion(_dsScan, _dsPackagingVar)
                    End If

                    'Rakesh-01.10.2013:7835-->Check article stock balance quantity
                    If (Not clsDefaultConfiguration.NegativeInventoryAllowed) Then
                        Dim objCommon As New clsCommon
                        Dim articleCode = grdScanItem.Item(CurrentRow, "ArticleCode")
                        Dim articleEAN = grdScanItem.Item(CurrentRow, "EAN")

                        Dim StockQty As Double = objCommon.GetStocks(clsAdmin.SiteCode, articleEAN, articleCode, True)

                        If ((StockQty < vOrderQty) AndAlso grdScanItem.Item(CurrentRow, "ReservedQty")) Then
                            ShowMessage(String.Format(getValueByKey("SB015"), StockQty), "SB015 - " & getValueByKey("CLAE04"))
                            grdScanItem.Item(CurrentRow, "PickUpQty") = 0
                            grdScanItem.Item(CurrentRow, "ReservedQty") = False
                            Exit Sub
                        End If
                    End If
                    ''Qty DS Change
                    If grdScanItem.Cols(CurrentCell).Name = "Quantity" Then
                        If result.Length > 0 Then
                            result(0)("Quantity") = IIf(grdScanItem.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "Quantity"))
                            If ArticleType = "Combo" Then
                                If IsApplyPromotion = True Then
                                    Discount = "0"
                                    result(0)("TotalTaxAmt") = Math.Round((((SellingPrice * vOrderQty) - Discount) * (CDbl(ComboTax.Rows(0)("TaxInPer")) / 100)), 2)  '' $$  added  by nik
                                    ' result(0)("TotalTaxAmt") = (((SellingPrice * vOrderQty) - Discount) * (CDbl(lblArticleCombo.Rows(0)("Tax")) / 100))  '' $$  added  by nik
                                Else
                                    result(0)("TotalTaxAmt") = Math.Round((((SellingPrice * vOrderQty) - Discount) * (CDbl(ComboTax.Rows(0)("TaxInPer")) / 100)), 2)  '' $$  added  by nik
                                    'result(0)("TotalTaxAmt") = (((SellingPrice * vOrderQty) - Discount) * (CDbl(lblArticleCombo.Rows(0)("Tax")) / 100))  '' $$  added  by nik
                                End If

                                result(0)("ExclTaxAmt") = result(0)("TotalTaxAmt")   '' $$  added  by nik
                                'result(0)("NetAmount") = ((SellingPrice * vOrderQty) - Discount) + result(0)("TotalTaxAmt")
                            End If
                            dsScan.AcceptChanges()
                        End If

                        If dsPackagingVar.Tables(0).Rows.Count > 0 Then
                            Dim resultVar As DataRow() = dsPackagingVar.Tables(0).Select("PackagingIndex='" + UpdatePackagingIndex.ToString() + "' ")
                            resultVar(0)("Quantity") = IIf(grdScanItem.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "Quantity"))
                            If ArticleType = "Combo" Then
                                If lblArticleCombo.Rows.Count > 0 Then
                                    If IsApplyPromotion = True Then
                                        Discount = "0"
                                        resultVar(0)("TotalTaxAmt") = Math.Round((((SellingPrice * vOrderQty) - Discount) * (CDbl(ComboTax.Rows(0)("TaxInPer")) / 100)), 2)  '' $$  added  by nik
                                        'resultVar(0)("TotalTaxAmt") = (((SellingPrice * vOrderQty) - Discount) * (CDbl(lblArticleCombo.Rows(0)("Tax")) / 100))  '' $$  added  by nik
                                    Else
                                        'resultVar(0)("TotalTaxAmt") = (((SellingPrice * vOrderQty) - Discount) * (CDbl(lblArticleCombo.Rows(0)("Tax")) / 100))  '' $$  added  by nik
                                        resultVar(0)("TotalTaxAmt") = Math.Round((((SellingPrice * vOrderQty) - Discount) * (CDbl(ComboTax.Rows(0)("TaxInPer")) / 100)), 2)  '' $$  added  by nik
                                    End If

                                    resultVar(0)("ExclTaxAmt") = resultVar(0)("TotalTaxAmt")   '' $$  added  by nik
                                    'result(0)("NetAmount") = Math.Round((((SellingPrice * vOrderQty) - Discount) + CDbl(resultVar(0)("TotalTaxAmt"))))
                                End If
                                End If

                                If vOrderQty < vPickupQty Then  '## vipin
                                    resultVar(0)("PickUpQty") = 0
                                    resultVar(0)("MinPayAmr") = 0
                                    resultVar(0)("TotalPickUpAmt") = 0
                                End If
                                If vPickupQty = 0 Then  '## 
                                    resultVar(0)("PickUpQty") = 0
                                    resultVar(0)("MinPayAmt") = 0
                                    resultVar(0)("TotalPickUpAmt") = 0
                                End If
                                dsPackagingVar.AcceptChanges()
                            End If

                            '-----------Added By Prasad for Checking if Order Qty in AddItems Tab is Less than total qty including subvariations in
                            '-------------- Order Details Tab then Update Header Qty and Others as 0
                            If dsPackagingDelivery.Tables(0).Rows.Count > 0 Then

                                Dim resultHeaderVar As DataRow() = dsPackagingDelivery.Tables(0).Select("PackagingIndex='" + UpdatePackagingIndex.ToString() + "'")
                                Dim VarSum = IIf(dsPackagingDelivery.Tables(0).Compute("sum(Quantity)", "PackagingIndex='" + UpdatePackagingIndex.ToString() + "' AND isHeader=False") Is DBNull.Value, 0, dsPackagingDelivery.Tables(0).Compute("sum(Quantity)", "PackagingIndex='" + UpdatePackagingIndex.ToString() + "' AND isHeader=False"))
                                Dim currentQty = vOrderQty
                                Dim CalcOrderQtyVar = currentQty - VarSum
                                If CalcOrderQtyVar <= 0 Then
                                    For Each resultvar As DataRow In dsPackagingDelivery.Tables(0).Select("PackagingIndex='" + UpdatePackagingIndex.ToString() + "' AND IsHeader=False")
                                        '------Added By Prasad if Pickup Qty then consider it as Qty else 0 Qty
                                        If resultvar("PickupQty") > 0 Then
                                            resultvar("PickupQty") = 0
                                            resultvar("Quantity") = 0
                                        Else
                                            resultvar("Quantity") = 0
                                        End If
                                    Next

                                    Dim SumVarQty = dsPackagingDelivery.Tables(0).Compute("sum(Quantity)", "PackagingIndex='" + UpdatePackagingIndex.ToString() + "' AND isHeader=False")
                                    If resultHeaderVar.Length > 0 Then
                                        resultHeaderVar(0)("Quantity") = currentQty - IIf(SumVarQty Is DBNull.Value, 0, SumVarQty)
                                    End If
                                End If
                                If resultHeaderVar.Length > 0 Then
                                    If vOrderQty < resultHeaderVar(0)("PickupQty") Then
                                        resultHeaderVar(0)("PickupQty") = 0
                                    End If
                                End If
                            End If



                            'Dim cPckQty = IIf(grdScanItem.Item(CurrentRow, "PckgQty") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PckgQty"))
                            If cQty > 0 AndAlso cPckSize > 0 Then
                                If result.Length > 0 Then
                                    result(0)("PckgQty") = cQty / cPckSize
                                End If
                                resultPackVar(0)("PckgQty") = cQty / cPckSize
                            End If
                            '----Added By Prasad for Checking if for Combo, Decimal Qty it gives error Msg
                            Dim drCheckIfCombo As New DataView(_dsScan.Tables("ItemScanDetails"), "ArticleCode='" & grdScanItem.Item(grdScanItem.Row, "ArticleCode") & "' AND ArticleType='Combo'", "", DataViewRowState.CurrentRows)
                            If drCheckIfCombo.Count > 0 Then

                                Dim OrderQty = vOrderQty.ToString.Split(".")
                                If OrderQty.Length = 2 Then
                                    If OrderQty(1) > 0 Then
                                        ShowMessage("Order Qty For Combo should not be in decimal", "Information")
                                        grdScanItem.Item(grdScanItem.Row, "Quantity") = 1
                                        If result.Length > 0 Then
                                            result(0)("Quantity") = IIf(grdScanItem.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, 1)
                                            dsScan.AcceptChanges()
                                        End If
                                        If resultPackVar.Length > 0 Then
                                            resultPackVar(0)("Quantity") = IIf(grdScanItem.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, 1)
                                            dsPackagingVar.AcceptChanges()
                                        End If
                                        Exit Sub
                                    End If
                                End If

                            End If
                        '----Added By Prasad for Checking if forSingle NOS, Decimal Qty it gives error Msg
                        'changed by vipin 26102017
                        '   Dim drCheckIfSingleNos As New DataView(_dsScan.Tables("ItemScanDetails"), "ArticleCode='" & grdScanItem.Item(grdScanItem.Row, "ArticleCode") & "' AND ArticleType='Single' AND UOM='NOS'", "", DataViewRowState.CurrentRows)
                        Dim drCheckIfSingleNos As New DataView(_dsScan.Tables("ItemScanDetails"), "ArticleCode='" & grdScanItem.Item(grdScanItem.Row, "ArticleCode") & "' AND ArticleType in('Single','Kit') AND UOM='NOS'", "", DataViewRowState.CurrentRows)
                            If drCheckIfSingleNos.Count > 0 Then

                                Dim OrderQty = vOrderQty.ToString.Split(".")
                                If OrderQty.Length = 2 Then
                                    If OrderQty(1) > 0 Then
                                        ShowMessage("Order Qty For Single Where UOM is NOS should not be in decimal", "Information")
                                        grdScanItem.Item(grdScanItem.Row, "Quantity") = 1
                                        If result.Length > 0 Then
                                            result(0)("Quantity") = IIf(grdScanItem.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, 1)
                                            dsScan.AcceptChanges()
                                        End If
                                        If resultPackVar.Length > 0 Then
                                            resultPackVar(0)("Quantity") = IIf(grdScanItem.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, 1)
                                            dsPackagingVar.AcceptChanges()
                                        End If
                                        Exit Sub
                                    End If
                                End If

                            End If
                            If clsDefaultConfiguration.PackageFiedlsAllowed Then
                                If grdScanItem.Item(CurrentRow, "ArticleType") = "Combo" Then
                                    If result.Length > 0 Then
                                        result(0)("PckgQty") = 0
                                    End If

                                    resultPackVar(0)("PckgQty") = 0
                                End If
                            End If
                            dsScan.AcceptChanges()

                            dsPackagingVar.AcceptChanges()

                        End If
                        If grdScanItem.Cols(CurrentCell).Name = "SellingPrice" Then
                            If result.Length > 0 Then
                                result(0)("SellingPrice") = IIf(grdScanItem.Item(CurrentRow, "SellingPrice") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "SellingPrice"))
                                '-----Added bY Prasad for Updating Netamt when Changing Price 
                                result(0)("NetAmount") = (IIf(grdScanItem.Item(CurrentRow, "SellingPrice") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "SellingPrice")) * grdScanItem.Item(CurrentRow, "Quantity")) + IIf(grdScanItem.Item(CurrentRow, "TotalTaxAmt") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "TotalTaxAmt"))
                                dsScan.AcceptChanges()
                            End If

                            If dsPackagingVar.Tables(0).Rows.Count > 0 Then
                                '-----Added bY Prasad for Updating Netamt & Selling Price For Sub Variations when Changing Price 

                                For Each resultVar As DataRow In dsPackagingVar.Tables(0).Select("ArticleCode='" + grdScanItem.Item(CurrentRow, "ArticleCode") + "' AND Discription='" + grdScanItem.Item(CurrentRow, "Discription") + "' and Rowindex='" + ComboSrNo.ToString() + "'")
                                    resultVar("SellingPrice") = IIf(grdScanItem.Item(CurrentRow, "SellingPrice") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "SellingPrice"))
                                    resultVar("NetAmount") = (IIf(grdScanItem.Item(CurrentRow, "SellingPrice") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "SellingPrice")) * resultVar("Quantity")) + IIf(grdScanItem.Item(CurrentRow, "TotalTaxAmt") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "TotalTaxAmt"))
                                    resultVar("GrossAmt") = IIf(grdScanItem.Item(CurrentRow, "SellingPrice") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "SellingPrice")) * resultVar("Quantity")
                                Next
                                dsPackagingVar.AcceptChanges()
                            End If
                        End If
                        'If IsDBNull(grdScanItem.Item(CurrentRow, "BatchBarcode")) = False Then
                        '    drAddItemExists = _dsScan.Tables("ItemScanDetails").Select("EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode = '" & grdScanItem.Item(CurrentRow, "BatchBarcode") & "'")
                        'Else
                        'drAddItemExists = _dsScan.Tables("ItemScanDetails").Select("EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode IS NULL")
                        drAddItemExists = _dsScan.Tables("ItemScanDetails").Select("EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And RowIndex='" & ComboSrNo & "'") ' And BatchBarcode IS NULL
                        'End If
                        Dim IsHeader As String = IIf(grdScanItem.Item(CurrentRow, "IsHeader") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "IsHeader"))
                        If IsHeader = "False" Then
                            Dim drAddPackageItemExists() As DataRow
                            drAddPackageItemExists = _dsPackagingVar.Tables("PackagingMaterial").Select("EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And PackagingIndex='" & UpdatePackagingIndex & "'") ' And BatchBarcode IS NULL
                            Dim itrQtyRow As DataRow() = dsPackagingVar.Tables(0).Select("EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And PackagingIndex<>'" + UpdatePackagingIndex.ToString() + "' ")

                            Dim QtyOthers As Object
                            Dim QtyExcAmt As Object
                            If itrQtyRow.Length > 0 Then
                                QtyOthers = dsPackagingVar.Tables(0).Compute("Sum(Quantity)", "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And PackagingIndex<>'" + UpdatePackagingIndex.ToString() + "' ")
                                QtyExcAmt = dsPackagingVar.Tables(0).Compute("Sum(ExclTaxAmt)", "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And PackagingIndex<>'" + UpdatePackagingIndex.ToString() + "' ")
                            Else
                                QtyOthers = 0
                                QtyExcAmt = 0
                            End If


                            IsEditItem = True
                            'drAddItemExists(0)("GrossAmt") = grdScanItem.Item(CurrentRow, "Quantity") * grdScanItem.Item(CurrentRow, "SellingPrice")
                            Dim obj As New clsSaleOrderCommon
                            obj.IsCSTApplicable = IsCSTApplicable
                        If drAddPackageItemExists(0)("IsCombo") Then
                        Else
                            obj.RecalculateLineP(drAddPackageItemExists(0), CtrlTxtOrderNo.Text, dsMain, False, False, _iArticleQtyBeforeChange, UpdatePackagingIndex, QtyOthers, QtyExcAmt)  '' $$added by nikhil
                        End If
                            TotalSalesQty = drAddPackageItemExists(0)("PickupQty") + drAddPackageItemExists(0)("DeliveredQty")
                            Dim ArticleRate As Double = Math.Round(drAddPackageItemExists(0)("NetAmount") / drAddPackageItemExists(0)("Quantity"), 3)
                            drAddPackageItemExists(0)("MinPayAmt") = ((drAddPackageItemExists(0)("Quantity") - TotalSalesQty) * ArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * ArticleRate)
                            'SetScanItemInSO(drAddItemExists(0))
                            'For Each dr As DataRow In dsMain.Tables("SalesOrderTaxDtls").Select("SiteCode='" & clsAdmin.SiteCode & "' And Finyear='" & clsAdmin.Financialyear & "' And SaleOrderNumber='" & CtrlTxtOrderNo.Text & "' And EAN='" & drAddPackageItemExists(0)("EAN") & "'")
                            'dr("TaxValue") = (dr("TaxValue") / _iArticleQtyBeforeChange) * drAddItemExists(0)("Quantity")
                            'For drindex = 0 To dtDocTaxes.Rows.Count - 1
                            '    'dr("TaxValue") = dr("TaxValue") + dtDocTaxes.Rows(drindex)("TaxAmt")
                            '    drAddPackageItemExists(0)("TotalTaxAmt") = drAddPackageItemExists(0)("TotalTaxAmt") + dtDocTaxes.Rows(drindex)("TaxAmt")
                            '    drAddPackageItemExists(0)("NetAmount") = drAddPackageItemExists(0)("NetAmount") + dtDocTaxes.Rows(drindex)("TaxAmt")
                            '    If dtDocTaxes.Rows(drindex)("Type") = "Exclusive" Then
                            '        drAddPackageItemExists(0)("ExclTaxAmt") = drAddPackageItemExists(0)("ExclTaxAmt") + dtDocTaxes.Rows(drindex)("TaxAmt")
                            '    Else
                            '        drAddPackageItemExists(0)("IncTaxAmt") = drAddPackageItemExists(0)("IncTaxAmt") + dtDocTaxes.Rows(drindex)("TaxAmt")
                            '    End If


                            'Next
                            ResetNewTax(True, dtDocTaxes)
                            'ResetNewTax(True, dtDocTaxes)
                            dsScan.AcceptChanges()
                            _dsPackagingVar.AcceptChanges()
                            'Next
                        Else
                            If drAddItemExists.Length > 0 Then
                                Dim drAddPackageItemExists() As DataRow
                                drAddPackageItemExists = _dsPackagingVar.Tables("PackagingMaterial").Select("EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And RowIndex='" & ComboSrNo & "' And PackagingIndex='" & UpdatePackagingIndex & "'") ' And BatchBarcode IS NULL

                                IsEditItem = True
                                'drAddItemExists(0)("GrossAmt") = grdScanItem.Item(CurrentRow, "Quantity") * grdScanItem.Item(CurrentRow, "SellingPrice")
                                Dim obj As New clsSaleOrderCommon
                                obj.IsCSTApplicable = IsCSTApplicable
                                Dim itrQtyRow As DataRow() = dsPackagingVar.Tables(0).Select("EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And PackagingIndex<>'" + UpdatePackagingIndex.ToString() + "' ")

                                Dim QtyOthers As Object
                                Dim QtyExcAmt As Object
                                If itrQtyRow.Length > 0 Then
                                    QtyOthers = dsPackagingVar.Tables(0).Compute("Sum(Quantity)", "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And PackagingIndex<>'" + UpdatePackagingIndex.ToString() + "' ")
                                    QtyExcAmt = dsPackagingVar.Tables(0).Compute("Sum(ExclTaxAmt)", "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And PackagingIndex<>'" + UpdatePackagingIndex.ToString() + "' ")
                                Else
                                    QtyOthers = 0
                                    QtyExcAmt = 0
                                End If

                            'obj.RecalculateLineP(drAddItemEx   ists(0), CtrlTxtOrderNo.Text, dsMain, False, False, _iArticleQtyBeforeChange, UpdatePackagingIndex, QtyOthers, QtyExcAmt)   '' False added by nikhil
                            'obj.RecalculateLineP(drAddPackageItemExists(0), CtrlTxtOrderNo.Text, dsMain, , False, _iArticleQtyBeforeChange, UpdatePackagingIndex, QtyOthers, QtyExcAmt)

                            If drAddPackageItemExists(0)("IsCombo") Then 'vipin 06102017
                            Else
                                obj.RecalculateLineP(drAddItemExists(0), CtrlTxtOrderNo.Text, dsMain, False, False, _iArticleQtyBeforeChange, UpdatePackagingIndex, QtyOthers, QtyExcAmt)   '' False added by nikhil
                                obj.RecalculateLineP(drAddPackageItemExists(0), CtrlTxtOrderNo.Text, dsMain, , False, _iArticleQtyBeforeChange, UpdatePackagingIndex, QtyOthers, QtyExcAmt)
                            End If

                            TotalSalesQty = drAddItemExists(0)("PickupQty") + IIf(drAddItemExists(0)("DeliveredQty") Is DBNull.Value, 0, drAddItemExists(0)("DeliveredQty"))
                                Dim ArticleRate As Double = Math.Round(drAddItemExists(0)("NetAmount") / drAddItemExists(0)("Quantity"), 3)
                                drAddItemExists(0)("MinPayAmt") = ((drAddItemExists(0)("Quantity") - TotalSalesQty) * ArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * ArticleRate)
                                'SetScanItemInSO(drAddItemExists(0))
                                'For Each dr As DataRow In dsMain.Tables("SalesOrderTaxDtls").Select("SiteCode='" & clsAdmin.SiteCode & "' And Finyear='" & clsAdmin.Financialyear & "' And SaleOrderNumber='" & CtrlTxtOrderNo.Text & "' And EAN='" & drAddItemExists(0)("EAN") & "'")
                                'dr("TaxValue") = dr("TaxValue") ' (dr("TaxValue") / _iArticleQtyBeforeChange) '* drAddItemExists(0)("Quantity")
                                'For drindex = 0 To dtDocTaxes.Rows.Count - 1
                                '    'dr("TaxValue") = dr("TaxValue") + dtDocTaxes.Rows(drindex)("TaxAmt")
                                '    drAddItemExists(0)("TotalTaxAmt") = drAddItemExists(0)("TotalTaxAmt") + dtDocTaxes.Rows(drindex)("TaxAmt")
                                '    drAddItemExists(0)("NetAmount") = drAddItemExists(0)("NetAmount") + dtDocTaxes.Rows(drindex)("TaxAmt")
                                '    If dtDocTaxes.Rows(drindex)("Type") = "Exclusive" Then
                                '        drAddItemExists(0)("ExclTaxAmt") = drAddItemExists(0)("ExclTaxAmt") + dtDocTaxes.Rows(drindex)("TaxAmt")
                                '    Else
                                '        drAddItemExists(0)("IncTaxAmt") = drAddItemExists(0)("IncTaxAmt") + dtDocTaxes.Rows(drindex)("TaxAmt")
                                '    End If


                                '    drAddPackageItemExists(0)("TotalTaxAmt") = drAddPackageItemExists(0)("TotalTaxAmt") + dtDocTaxes.Rows(drindex)("TaxAmt")
                                '    drAddPackageItemExists(0)("NetAmount") = drAddPackageItemExists(0)("NetAmount") + dtDocTaxes.Rows(drindex)("TaxAmt")
                                '    If dtDocTaxes.Rows(drindex)("Type") = "Exclusive" Then
                                '        drAddPackageItemExists(0)("ExclTaxAmt") = drAddPackageItemExists(0)("ExclTaxAmt") + dtDocTaxes.Rows(drindex)("TaxAmt")
                                '    Else
                                '        drAddPackageItemExists(0)("IncTaxAmt") = drAddPackageItemExists(0)("IncTaxAmt") + dtDocTaxes.Rows(drindex)("TaxAmt")
                                '    End If



                                'Next
                                ResetNewTax(True, dtDocTaxes)
                                dsScan.AcceptChanges()
                                _dsPackagingVar.AcceptChanges()

                                'Next

                                'ResetNewTax(True, dtDocTaxes)
                                'CalculateSalesOrderSummary(dsScan)
                                'RefreshScanData(dsScan)
                                'GridSetting()
                            End If
                        End If

                        'If IsDBNull(grdScanItem.Item(CurrentRow, "BatchBarcode")) = False Then
                        '    grdScanItem.Item(CurrentRow, "PickUpQty") = vOrderQty
                        '    grdScanItem_AfterEdit(grdScanItem, New C1.Win.C1FlexGrid.RowColEventArgs(CurrentRow, grdScanItem.Cols("PickUpQty").Index))
                        'End If
                Catch ex As Exception
                    ShowMessage(ex.Message, getValueByKey("CLAE05"))
                End Try
            End If

            If grdScanItem.Cols(CurrentCell).Name = "PickUpQty" Then
                Try
                    Dim vPickupQty As Double = IIf(grdScanItem.Item(CurrentRow, "PickupQty") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PickupQty"))
                    'StockQty = objCM.GetStocks(clsAdmin.SiteCode, grdScanItem.Item(CurrentRow, "EAN"), grdScanItem.Item(CurrentRow, "ArticleCode"), clsDefaultConfiguration.IsBatchManagementReq, grdScanItem.Item(CurrentRow, "BatchBarcode"))
                    'If CDbl(StockQty) <= 0 Then
                    '    If clsDefaultConfiguration.NegativeInventoryAllowed = False Then
                    '        ShowMessage(getValueByKey("SO001"), "SO001 - " & getValueByKey("CLAE04"))
                    '        'ShowMessage("Article out of Stock.", "Information")                        
                    '        e.Cancel = True
                    '        Exit Sub
                    '    End If
                    'End If
                    If Not (vPickupQty >= 0) Then
                        ShowMessage(getValueByKey("SO008"), "SO008 - " & getValueByKey("CLAE05"))
                        'ShowMessage("PickUp Quantity cannot less than 1.", "PickUp Quantity Information")
                        grdScanItem.Item(CurrentRow, "PickupQty") = 0
                    End If

                    Dim dvPickupQty As DataView
                    'If IsDBNull(grdScanItem.Item(CurrentRow, "BatchBarcode")) = False Then
                    '    dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode = '" & grdScanItem.Item(CurrentRow, "BatchBarcode") & "'" & addCondtionRow, "", DataViewRowState.CurrentRows)
                    'Else
                    dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode IS NULL" & addCondtionRow, "", DataViewRowState.CurrentRows)
                    'End If

                    If dvPickupQty.Count > 0 Then
                        dvPickupQty.AllowEdit = True

                        'Rakesh-01.10.2013:7835-->Check article stock balance quantity
                        If (Not clsDefaultConfiguration.NegativeInventoryAllowed) Then
                            Dim objCommon As New clsCommon
                            Dim articleCode = grdScanItem.Item(CurrentRow, "ArticleCode")
                            Dim articleEAN = grdScanItem.Item(CurrentRow, "EAN")
                            Dim iPickUpQty = grdScanItem.Item(CurrentRow, "PickUpQty")

                            Dim StockQty As Double = objCommon.GetStocks(clsAdmin.SiteCode, articleEAN, articleCode, True)

                            If (StockQty < iPickUpQty) Then
                                ShowMessage(String.Format(getValueByKey("SB015"), StockQty), "SB015 - " & getValueByKey("CLAE04"))
                                grdScanItem.Item(CurrentRow, "PickUpQty") = 0
                            End If
                        End If

                        For Each drPickupQty As DataRowView In dvPickupQty 'need to check for package variation

                            If grdScanItem.Item(CurrentRow, "PickupQty") <= grdScanItem.Item(CurrentRow, "Quantity") Then
                                drPickupQty("PickupQty") = grdScanItem.Item(CurrentRow, "PickupQty")

                                TotalSalesQty = drPickupQty("PickupQty") + IIf(drPickupQty("DeliveredQty") Is DBNull.Value, 0, drPickupQty("DeliveredQty"))
                                NetArticleRate = Math.Round(drPickupQty("NetAmount") / drPickupQty("Quantity"), 3)
                                drPickupQty("MinPayAmt") = Math.Round(((drPickupQty("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate), 3)
                                'drPickupQty("PickupQty") = grdScanItem.Item(CurrentRow, "PickupQty")
                                'RecalculateLine(drPickupQty.Row)
                                drPickupQty("TotalPickUpAmt") = (drPickupQty("PickupQty") * NetArticleRate)
                            Else
                                grdScanItem.Item(CurrentRow, "PickupQty") = 0
                                grdScanItem.Item(CurrentRow, "TotalPickUpAmt") = 0
                                ShowMessage(getValueByKey("SO009"), "SO009 - " & getValueByKey("CLAE04"))
                                'ShowMessage("Pick Up Quantity cannot greater than Order Quantity.", "Information")
                            End If
                        Next
                        _dsScan.AcceptChanges()
                    End If

                    ' CalculateSalesOrderSummary(dsScan)

                Catch ex As Exception
                    LogException(ex)
                    ShowMessage(ex.Message, getValueByKey("CLAE05"))
                End Try
            End If

            If grdScanItem.Cols(CurrentCell).Name = "ExpDelDate" Then
                Try
                    If Not (grdScanItem.Item(CurrentRow, CurrentCell) Is DBNull.Value) Then
                        ''--changed by rama on 10-jun-2009
                        grdScanItem.EndUpdate()
                        If DateDiff(DateInterval.Day, vCurrentDate, grdScanItem.Item(CurrentRow, CurrentCell)) < 0 Then
                            ShowMessage(getValueByKey("SO010"), "SO010 - " & getValueByKey("CLAE04"))
                            'ShowMessage("Delivery Date cannot be backdated.", "Delivery Date")
                            'grdScanItem.Item(CurrentRow, "ExpDelDate") = CtrlSalesInfo1.CtrlDtExpDelDate.Value  --abc
                        ElseIf (vCurrentDate.AddSeconds(-1) > Convert.ToDateTime(grdScanItem.Item(CurrentRow, CurrentCell))) Then
                            ShowMessage(getValueByKey("SO010"), "SO010 - " & getValueByKey("CLAE04"))
                            'grdScanItem.Item(CurrentRow, CurrentCell) = CtrlSalesInfo1.CtrlDtExpDelDate.Value  --abc
                        Else
                            Dim dvDelivery As DataView
                            'If IsDBNull(grdScanItem.Item(CurrentRow, "BatchBarcode")) = False Then
                            '    dvDelivery = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode = '" & grdScanItem.Item(CurrentRow, "BatchBarcode") & "'" & addCondtionRow, "", DataViewRowState.CurrentRows)
                            'Else
                            dvDelivery = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode IS NULL" & addCondtionRow, "", DataViewRowState.CurrentRows)
                            'End If
                            If dvDelivery.Count > 0 Then
                                dvDelivery.AllowEdit = True
                                For Each drPickupQty As DataRowView In dvDelivery
                                    drPickupQty("ExpDelDate") = grdScanItem.Item(CurrentRow, CurrentCell)
                                Next
                                _dsScan.AcceptChanges()
                            End If
                        End If
                        ''--
                        'If Format(grdScanItem.Item(CurrentRow, CurrentCell), vDateFormat) < Format(vCurrentDate, vDateFormat) Then
                        '    ShowMessage("Delivery Date cannot be backdated.", "Delivery Date")
                        '    grdScanItem.Item(CurrentRow, "ExpDelDate") = dtpExpDeliveryDate.Value
                        'End If
                    End If
                Catch ex As Exception
                    LogException(ex)
                    ShowMessage(ex.Message, getValueByKey("CLAE05"))
                End Try
            End If

            If grdScanItem.Cols(CurrentCell).Name = "ReservedQty" Then
                Try
                    Dim dvPickupQty As New DataView
                    'If IsDBNull(grdScanItem.Item(CurrentRow, "BatchBarcode")) = False Then
                    '    dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode = '" & grdScanItem.Item(CurrentRow, "BatchBarcode") & "'", "", DataViewRowState.CurrentRows)
                    'Else
                    dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode IS NULL", "", DataViewRowState.CurrentRows)
                    'End If

                    If dvPickupQty.Count > 0 Then

                        If (grdScanItem.Item(CurrentRow, "ReservedQty")) Then

                            Dim objCommon As New clsCommon
                            Dim articleCode = grdScanItem.Item(CurrentRow, "ArticleCode")
                            Dim articleEAN = grdScanItem.Item(CurrentRow, "EAN")
                            Dim Quantity = grdScanItem.Item(CurrentRow, "Quantity")

                            Dim StockQty As Double = objCommon.GetStocks(clsAdmin.SiteCode, articleEAN, articleCode, False)

                            If (StockQty < Quantity) Then
                                ShowMessage(String.Format(getValueByKey("SB016"), StockQty), "SB016 - " & getValueByKey("CLAE04"))
                                'grdScanItem.Item(CurrentRow, "Quantity") = IIf(StockQty < 0, Quantity, StockQty)
                                'grdScanItem.Item(CurrentRow, "ReservedQty") = IIf(StockQty > 0, True, False)
                                grdScanItem.Item(CurrentRow, "ReservedQty") = False
                                Return
                            End If

                        End If

                        dvPickupQty.AllowEdit = True
                        For Each drPickupQty As DataRowView In dvPickupQty
                            drPickupQty("ReservedQty") = grdScanItem.Item(CurrentRow, "ReservedQty")
                        Next
                        _dsScan.AcceptChanges()
                    End If
                Catch ex As Exception
                    LogException(ex)
                    ShowMessage(ex.Message, getValueByKey("CLAE05"))
                End Try
            End If

            If grdScanItem.Cols(CurrentCell).Name = "IsCLP" Then
                Try
                    Dim dvPickupQty As DataView
                    'If IsDBNull(grdScanItem.Item(CurrentRow, "BatchBarcode")) = False Then
                    '    dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode = '" & grdScanItem.Item(CurrentRow, "BatchBarcode") & "'", "", DataViewRowState.CurrentRows)
                    'Else
                    dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode IS NULL", "", DataViewRowState.CurrentRows)
                    'End If
                    If dvPickupQty.Count > 0 Then
                        dvPickupQty.AllowEdit = True
                        For Each drPickupQty As DataRowView In dvPickupQty
                            drPickupQty("IsCLP") = grdScanItem.Item(CurrentRow, "IsCLP")
                        Next
                        _dsScan.AcceptChanges()
                    End If
                    '------Added By Prasad for CLP Selection as Per Variation
                    Dim dvVariationCLP As DataView
                    dvVariationCLP = New DataView(_dsPackagingVar.Tables("PackagingMaterial"), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode IS NULL", "", DataViewRowState.CurrentRows)
                    If dvPickupQty(0)("IsCLP") Then
                        'End If
                        If dvVariationCLP.Count > 0 Then
                            dvPickupQty.AllowEdit = True
                            For Each drVariationCLP As DataRowView In dvVariationCLP
                                drVariationCLP("IsCLP") = True
                            Next
                            _dsPackagingVar.AcceptChanges()
                        End If

                    Else
                        If dvVariationCLP.Count > 0 Then
                            dvVariationCLP.AllowEdit = True
                            If dvPickupQty(0)("IsCLP") = False Then
                                For Each drVariationCLP As DataRowView In dvVariationCLP
                                    drVariationCLP("IsCLP") = False
                                Next
                                _dsPackagingVar.AcceptChanges()
                            End If
                        End If
                    End If
                Catch ex As Exception
                    LogException(ex)
                    ShowMessage(ex.Message, getValueByKey("CLAE05"))
                End Try
            End If
            If IsComboRow = "True" Then
                If result.Length > 0 Then
                    If result(0)("PackagingMaterial") = "" Then
                        ShowMessage("Please select Packaging material for Combo article", " " & getValueByKey("CLAE04"))
                        Exit Sub

                    End If
                End If

            End If
            If IsComboRow = "False" Then
                If grdScanItem.Cols(CurrentCell).Name = "PackagingMaterial" Or grdScanItem.Cols(CurrentCell).Name = "PackagingType" Then
                Else
                    CalculateSalesOrderSummary(dsScan)
                    RefreshScanData(dsScan)
                    GridSetting()
                    AddButtonControlInGrid(1)
                End If
            Else
                If grdScanItem.Cols(CurrentCell).Name = "PackagingType" Then
                Else
                    CalculateSalesOrderSummary(dsScan)
                    RefreshScanData(dsScan)
                    GridSetting()

                    AddButtonControlInGrid(1)
                End If

            End If
            IsApplyPromotion = False  '' $$ added by nikhil
            grdScanItem.Select(CurrentRow, CurrentCell, CurrentRow, CurrentCell, True)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub dgDeliveryLocation_CellButtonClick(sender As Object, e As RowColEventArgs) Handles dgDeliveryLocation.CellButtonClick
        Try

            Dim rowIndex = dgDeliveryLocation.Item(dgDeliveryLocation.Row, "RowIndex")
            Dim packagingIndex = dgDeliveryLocation.Item(dgDeliveryLocation.Row, "PackagingIndex")
            Dim deliveryIndex = dgDeliveryLocation.Item(dgDeliveryLocation.Row, "DeliveryIndex")
            Dim SrNo As String = dgDeliveryLocation.Item(dgDeliveryLocation.Row, "SrNo")
            Dim vEAN As String = "" 'dgDeliveryLocation.Item(dgDeliveryLocation.Row, "EAN")
            Dim IsHeader = dgDeliveryLocation.Item(dgDeliveryLocation.Row, "IsHeader")
            Dim IsCombo = dgDeliveryLocation.Item(dgDeliveryLocation.Row, "IsCombo")
            Dim addCondtion As String = String.Empty
            Dim baseAddCondtion As String = String.Empty
            Dim resultEAN As DataRow() = _dsScan.Tables("ItemScanDetails").Select("1=1  AND RowIndex = " & rowIndex)
            vEAN = resultEAN(0)("EAN")

            Dim resultMasterRow As DataRow() = _dsScan.Tables("ItemScanDetails").Select("1=1  AND PackagingIndex = " & packagingIndex & " AND DeliveryIndex = " & deliveryIndex)

            If rowIndex > 0 Then
                addCondtion = " And  RowIndex ='" & rowIndex & "'"
            End If


            If packagingIndex > 0 AndAlso resultMasterRow.Length = 0 Then
                addCondtion &= " AND PackagingIndex ='" & packagingIndex & "'"
                baseAddCondtion = addCondtion
            End If
            If deliveryIndex > 0 AndAlso IsHeader = "False" Then
                addCondtion &= " AND DeliveryIndex = '" & deliveryIndex & "'"
            End If
            If IsHeader = "True" Then 'String.IsNullOrEmpty(batchBarcode)
                dvEditDeleteItems = New DataView(_dsScan.Tables("ItemScanDetails"), "1=1 " & addCondtion, "", DataViewRowState.CurrentRows)

            End If
            If dvEditDeleteItems IsNot Nothing Then
                If dvEditDeleteItems.Count > 0 Then
                    For Each drView2 As DataRowView In dvEditDeleteItems
                        drView2.Delete()
                    Next
                    If DtSOStr.Rows.Count > 0 Then
                        dvEditDeleteSTRItems = New DataView(DtSOStr, "DeliveryIndex=" & rowIndex, "", DataViewRowState.CurrentRows)
                        For Each drViewSTR As DataRowView In dvEditDeleteSTRItems
                            drViewSTR.Delete()
                        Next
                        DtSOStr.AcceptChanges()
                        dtSTR = DtSOStr.Copy()
                    End If
                End If
            End If

            If IsHeader = "True" Then 'String.IsNullOrEmpty(batchBarcode)
                dvEditDeletePackagingItems = New DataView(_dsPackagingVar.Tables("PackagingMaterial"), "1=1 " & addCondtion, "", DataViewRowState.CurrentRows)

            End If


            dvEditDeletePackagingDeliveryItems = New DataView(_dsPackagingDelivery.Tables("PackagingDelivery"), "1=1 " & addCondtion, "", DataViewRowState.CurrentRows)

            vtaxIndex = IIf(dsMain.Tables("SalesOrderTaxDtls").Compute("MAX(taxLineNo)", "") Is DBNull.Value, 0, dsMain.Tables("SalesOrderTaxDtls").Compute("MAX(taxLineNo)", "")) + 1
            If dvEditDeletePackagingItems IsNot Nothing Then
                If dvEditDeletePackagingItems.Count > 0 Then
                    dvDeleteTaxOnItem = New DataView(dsMain.Tables("SalesOrderTaxDtls"), "IsDocumentLevel=True", "", DataViewRowState.CurrentRows)
                    If dvDeleteTaxOnItem.Count > 0 Then
                        ' If IsHeader Then
                        dvDeleteTaxOnItem.AllowDelete = True
                        For Each dr As DataRowView In dvDeleteTaxOnItem
                            dr.Delete()
                        Next
                        dtDocTaxes.Rows.Clear()
                    End If
                End If
            End If

            dvDeleteTaxOnItemLevelTax = New DataView(dsMain.Tables("SalesOrderTaxDtls"), "EAN='" & vEAN & "' And IsDocumentLevel=False", "", DataViewRowState.CurrentRows)
            If resultMasterRow.Length = 1 Then
                For Each drView As DataRowView In dvDeleteTaxOnItemLevelTax
                    drView.Delete()
                Next
                dsMain.Tables("SalesOrderTaxDtls").AcceptChanges()
            Else
                If dvEditDeletePackagingItems IsNot Nothing Then
                    Dim totaltaxvaluereults As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("EAN='" & vEAN & "' And IsDocumentLevel=False")
                    If totaltaxvaluereults.Length > 0 Then
                        Dim totaltaxvalue = totaltaxvaluereults(0)("TaxValue")
                        Dim totalquantityreults As DataRow() = _dsPackagingDelivery.Tables(0).Select("EAN='" & vEAN & "' and DeliveryIndex = " & deliveryIndex)
                        Dim totalQuantity = _dsPackagingVar.Tables("PackagingMaterial").Compute("Sum(Quantity)", "Rowindex=" & rowIndex & "")
                        Dim resulttax = FormatNumber((totaltaxvalue / totalQuantity), 2)
                        Dim totalquantityreduce = totalquantityreults(0)("Quantity")

                        Dim finalTaxonremainingQuantity = resulttax * (totalQuantity - totalquantityreduce)
                        totaltaxvaluereults(0)("TaxValue") = finalTaxonremainingQuantity
                    End If
                End If

            End If

            If dvEditDeletePackagingItems IsNot Nothing Then
                For Each drView2 As DataRowView In dvEditDeletePackagingItems
                    drView2.Delete()
                Next
            End If


            If dvEditDeletePackagingDeliveryItems.Count > 0 Then
                Dim resultPrevQty As DataRow() = _dsPackagingDelivery.Tables(0).Select("1=1 " & addCondtion)
                Dim resultbaseQty As DataRow() = _dsPackagingDelivery.Tables(0).Select("1=1 " & baseAddCondtion) 'to update header quantity if child deleted

                If resultPrevQty.Length > 0 AndAlso resultbaseQty.Length > 0 Then
                    resultbaseQty(0)("Quantity") = resultbaseQty(0)("Quantity") + resultPrevQty(0)("Quantity")
                    If clsDefaultConfiguration.PackageFiedlsAllowed Then
                        '----Incase Delete Deliverylocation calulate pkgQty
                        If resultbaseQty(0)("UOM") = "KGS" Then
                            If resultbaseQty(0)("PckgSize") > 0 Then
                                resultbaseQty(0)("PckgQty") = resultbaseQty(0)("Quantity") / resultbaseQty(0)("PckgSize") & " " & resultbaseQty(0)("PackagingType")
                            End If
                        End If
                    End If
                End If
                For Each drView2 As DataRowView In dvEditDeletePackagingDeliveryItems
                    drView2.Delete()
                Next


                _dsPackagingDelivery.AcceptChanges()
                _dsPackagingVar.AcceptChanges()
                _dsScan.AcceptChanges()

            End If
            '------Added By Prasad for Removing Combo Article if Deleted from Grid
            ''----Delete Bulk Combo Details As Well...
            If IsHeader = True And resultMasterRow.Length = 1 Then
                If IsApplyPromotion = True Then
                    rbbtnClrAllPromo_Click(sender, e)
                End If
                'Dim foundRow As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("IsDocumentLevel = True")
                'If foundRow.Count > 0 Then
                '    ResetNewTax(True, dtDocTaxes)
                '    dtDocTaxes.Clear()
                '    CalculateSalesOrderSummary(dsScan)
                'End If
                If DtSoBulkRemarks IsNot Nothing Then
                    dvRemarks = New DataView(DtSoBulkRemarks, "SrNo='" & rowIndex & "' ", "", DataViewRowState.CurrentRows)
                    If dvRemarks.Count > 0 Then
                        For Each drView2 As DataRowView In dvRemarks
                            drView2.Delete()
                        Next
                        DtSoBulkRemarks.AcceptChanges()

                        Dim index As Integer = 1
                        Dim dtTemp = _dsScan.Tables("ItemScanDetails")
                        Dim dataView As New DataView(dtTemp)
                        dataView.Sort = "SrNo ASC"
                        dtTemp = dataView.ToTable()
                        For Each dr As DataRow In dtTemp.Rows
                            If DtSoBulkRemarks IsNot Nothing Then
                                If DtSoBulkRemarks.Rows.Count > 0 Then
                                    Dim resultRemark As DataRow() = DtSoBulkRemarks.Select("Articletype='Combo' and SrNo='" + dr("Rowindex").ToString() + "'  ")
                                    If resultRemark.Length > 0 Then
                                        Dim rmkArrary As Array = resultRemark(0)("Articlename").ToString().Split("-")
                                        If rmkArrary.Length > 0 Then
                                            resultRemark(0)("Articlename") = index.ToString() & "-" & rmkArrary(1)
                                        End If
                                    End If
                                End If
                            End If
                            index += 1
                        Next
                        LoadRemarks(DtSoBulkRemarks, True)
                    End If
                End If
                If DtSoBulkComboHdr.Rows.Count > 0 And IsCombo = "True" Then
                    Dim dvCombo = New DataView(DtSoBulkComboHdr, "ComboSrNo='" & rowIndex & "' ", "", DataViewRowState.CurrentRows)
                    If dvCombo.Count > 0 Then
                        DeleteBulkCombo(rowIndex)
                    End If
                End If

                '----------Added By Prasad for Delete remarks for combo, if removed from grid

                'dvRemarks = New DataView(DtSoBulkRemarks, "SrNo='" & rowIndex & "' ", "", DataViewRowState.CurrentRows)
                'If dvRemarks.Count > 0 Then
                '    For Each drView2 As DataRowView In dvRemarks
                '        drView2.Delete()
                '    Next
                '    DtSoBulkRemarks.AcceptChanges()
                '    'LoadRemarks(DtSoBulkRemarks, True)
                'End If
            End If
            CalculateSalesOrderSummary(dsScan)
            ' UpdateSODeliveryGridData()
            BindSOItemGridData(_dsScan.Tables("ItemScanDetails"))
            BindSODeliveryGridData(_dsPackagingDelivery.Tables(0), True)
            AddButtonControlInGrid(1)
            AddButtonControlInDeliveryGrid(1)
            AddSTRButtonControlInDeliveryGrid(1)

        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub
    ''' <summary>
    ''' Delete Selected Article from DataGrid
    ''' </summary>
    ''' <param name="sender">Select Row</param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdScanItem_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdScanItem.CellButtonClick
        Try
            Dim BulkComboMstId As Int64 = 0
            Dim SrNo As String = grdScanItem.Item(grdScanItem.Row, "SrNo")
            Dim IsHeader = grdScanItem.Item(grdScanItem.Row, "IsHeader")
            Dim IsCombo = grdScanItem.Item(grdScanItem.Row, "IsCombo")
            Dim ComboSrNo = grdScanItem.Item(grdScanItem.Row, "RowIndex")
            Dim deleteRowNo As Integer = 0
            If DtSoBulkComboHdr.Rows.Count > 0 Then
                Dim drHdr() = DtSoBulkComboHdr.Select("ComboSrNo=" & ComboSrNo)
                If drHdr.Count > 0 Then
                    deleteRowNo = ComboSrNo
                End If
            End If

            If MsgBox(getValueByKey("SO011"), MsgBoxStyle.YesNo, "SO011 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                If IsDBNull(grdScanItem.Item(grdScanItem.Row, "BatchBarcode")) = False Then
                    'DeleteScanItemInSO(grdScanItem.Item(grdScanItem.Row, "EAN"), grdScanItem.Item(grdScanItem.Row, "BatchBarcode"), deleteRowNo)
                    DeleteScanItemInSO(grdScanItem.Item(grdScanItem.Row, "EAN"), grdScanItem.Item(grdScanItem.Row, "BatchBarcode"), ComboSrNo, grdScanItem.Item(grdScanItem.Row, "PackagingIndex"), IsCombo, IsHeader)
                Else
                    'DeleteScanItemInSO(grdScanItem.Item(grdScanItem.Row, "EAN"), , deleteRowNo)
                    DeleteScanItemInSO(grdScanItem.Item(grdScanItem.Row, "EAN"), , ComboSrNo, grdScanItem.Item(grdScanItem.Row, "PackagingIndex"), IsCombo, IsHeader)
                End If
                ''----Delete Bulk Combo Details As Well...
                If IsHeader Then
                    If DtSoBulkComboHdr.Rows.Count > 0 And IsCombo = "True" Then
                        DeleteBulkCombo(ComboSrNo)
                    End If
                End If
                If IsApplyPromotion = True Then
                    rbbtnClrAllPromo_Click(sender, e)
                End If
            End If

            CtrlSalesPersons.AndroidSearchTextBox.Select()
        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Sub

    ''' <summary>
    ''' Delete Selected Article from DataGrid
    ''' </summary>
    ''' <param name="sender">Select Row</param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdScanItem_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdScanItem.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Dim SrNo As String = grdScanItem.Item(grdScanItem.Row, "SrNo")
                Dim IsHeader = grdScanItem.Item(grdScanItem.Row, "IsHeader")
                Dim IsCombo = grdScanItem.Item(grdScanItem.Row, "IsCombo")
                Dim ComboSrNo = grdScanItem.Item(grdScanItem.Row, "RowIndex")
                If MsgBox(getValueByKey("SO011"), MsgBoxStyle.YesNo, "SO011 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                    If IsDBNull(grdScanItem.Item(grdScanItem.Row, "BatchBarcode")) = False Then
                        ' DeleteScanItemInSO(grdScanItem.Item(grdScanItem.Row, "EAN"), grdScanItem.Item(grdScanItem.Row, "BatchBarcode"))
                        DeleteScanItemInSO(grdScanItem.Item(grdScanItem.Row, "EAN"), grdScanItem.Item(grdScanItem.Row, "BatchBarcode"), ComboSrNo, grdScanItem.Item(grdScanItem.Row, "PackagingIndex"), IsCombo, IsHeader)
                    Else
                        DeleteScanItemInSO(grdScanItem.Item(grdScanItem.Row, "EAN"))
                    End If
                End If
            End If
        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Sub

    ''' <summary>
    ''' Show the image of the Current Selected Article
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>s
    ''' <remarks></remarks>
    ''' 
    Dim dtArticle As New DataTable
    Private Sub grdScanItem_RowColChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdScanItem.RowColChange
        Try
            If (grdScanItem.Row >= 1) Then

                'grdScanItem.Cols("ArticleCode").Visible = True
                vArticleImagesCode = grdScanItem.Item(grdScanItem.Row, "ArticleCode")

                If grdScanItem.Item(grdScanItem.Row, "ArticleType") = "Combo" Then
                    grdScanItem.Cols("SellingPrice").AllowEditing = False
                Else
                    grdScanItem.Cols("SellingPrice").AllowEditing = True
                End If
                'grdScanItem.Cols("ArticleCode").Visible = False

                'strImagesUrl = objComn.GetArticleImage(vArticleImagesCode, My.Settings.ArticleImageFolder)
                'PictureBoxImages.ImageLocation = strImagesUrl
                ' CtrlProductImage.ShowArticleImage(vArticleImagesCode)
                'If clsDefaultConfiguration.ClientForMail = "PC" Then  ' commented out by vipin 
                '    dtArticle = _dsScan.Tables("ItemScanDetails")

                '    Dim obCombo As New frmPCBulkOrderCombo
                '    obCombo.ArticleComboDtl = dtArticle


                'End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Delete Selected Article from DataGrid
    ''' </summary>
    ''' <param name="vEAN">Selected EAN</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function DeleteScanItemInSO(ByVal vEAN As String, Optional batchBarcode As String = "", Optional rowIndex As Integer = 0, Optional userSrNo As String = "0", Optional IsCombo As String = "0", Optional IsHeader As Boolean = False) As Boolean
        Try
            '---Add Condtion by Mahesh incase of combo only one combo need to deleted 
            Dim addCondtion As String = String.Empty
            If rowIndex > 0 Then
                addCondtion = " AND RowIndex =" & rowIndex
            End If
            If userSrNo > "0" And Not IsHeader Then
                addCondtion &= " AND PackagingIndex = " & userSrNo
            End If

            If IsCombo = "True" Then 'String.IsNullOrEmpty(batchBarcode)
                dvEditDeleteItems = New DataView(_dsScan.Tables("ItemScanDetails"), "1=1 " & addCondtion, "", DataViewRowState.CurrentRows)
            Else
                dvEditDeleteItems = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & vEAN & "' And BatchBarcode IS NULL" & addCondtion, "", DataViewRowState.CurrentRows)
            End If


            If IsCombo = "True" Then 'String.IsNullOrEmpty(batchBarcode)
                dvEditDeletePackagingItems = New DataView(_dsPackagingVar.Tables("PackagingMaterial"), "1=1 " & addCondtion, "", DataViewRowState.CurrentRows)
            Else
                dvEditDeletePackagingItems = New DataView(_dsPackagingVar.Tables("PackagingMaterial"), "EAN='" & vEAN & "' And BatchBarcode IS NULL" & addCondtion, "", DataViewRowState.CurrentRows)

            End If
            'Dim foundRow As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("IsDocumentLevel = True")
            'If foundRow.Count > 0 Then
            '    ResetDocTax(True, dtDocTaxes)
            '    dtDocTaxes.Clear()
            'End If
            vtaxIndex = IIf(dsMain.Tables("SalesOrderTaxDtls").Compute("MAX(taxLineNo)", "") Is DBNull.Value, 0, dsMain.Tables("SalesOrderTaxDtls").Compute("MAX(taxLineNo)", "")) + 1
            dvDeleteTaxOnItem = New DataView(dsMain.Tables("SalesOrderTaxDtls"), "IsDocumentLevel=True", "", DataViewRowState.CurrentRows)
            If dvDeleteTaxOnItem.Count > 0 Then
                ' If IsHeader Then
                dvDeleteTaxOnItem.AllowDelete = True
                For Each dr As DataRowView In dvDeleteTaxOnItem
                    dr.Delete()
                Next
                dtDocTaxes.Rows.Clear()
            End If
            dvDeleteTaxOnItemLevelTax = New DataView(dsMain.Tables("SalesOrderTaxDtls"), "EAN='" & vEAN & "' And IsDocumentLevel=False", "", DataViewRowState.CurrentRows)
            If IsHeader Then
                For Each drView As DataRowView In dvDeleteTaxOnItemLevelTax
                    drView.Delete()
                Next
                dsMain.Tables("SalesOrderTaxDtls").AcceptChanges()
            Else
                Dim totaltaxvaluereults As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("EAN='" & vEAN & "' And IsDocumentLevel=False")
                If totaltaxvaluereults.Length > 0 Then
                    Dim totaltaxvalue = totaltaxvaluereults(0)("TaxValue")
                    Dim totalquantityreults As DataRow() = _dsPackagingVar.Tables("PackagingMaterial").Select("EAN='" & vEAN & "'" & addCondtion)
                    Dim totalQuantity = _dsPackagingVar.Tables("PackagingMaterial").Compute("Sum(Quantity)", "Rowindex=" & rowIndex & "")
                    Dim resulttax = FormatNumber((totaltaxvalue / totalQuantity), 2)
                    Dim totalquantityreduce = totalquantityreults(0)("Quantity")

                    Dim finalTaxonremainingQuantity = resulttax * (totalQuantity - totalquantityreduce)
                    totaltaxvaluereults(0)("TaxValue") = finalTaxonremainingQuantity
                End If

            End If

            For Each drView As DataRowView In dvEditDeletePackagingItems

                drView.Delete()

            Next

            If Not IsHeader Then
                dvEditDeletePackagingDeliveryItems = New DataView(_dsPackagingDelivery.Tables("PackagingDelivery"), "1=1 " & addCondtion, "", DataViewRowState.CurrentRows)
            Else
                dvEditDeletePackagingDeliveryItems = New DataView(_dsPackagingDelivery.Tables("PackagingDelivery"), "1=1  AND RowIndex =" & rowIndex, "", DataViewRowState.CurrentRows)
            End If



            If grdScanItem.Controls.Find("btnPlus" + userSrNo, True).Count() > 0 Then
                For Each c As Control In grdScanItem.Controls.Find("btnPlus" + userSrNo, True)
                    grdScanItem.Controls.Remove(c)
                    Me.Controls.Remove(c)
                Next
                'Me.Batchbarcode.RemoveAll(Function(w) w.userSrNo = userSrNo)

            End If
            If dvEditDeletePackagingDeliveryItems.Count > 0 Then
                For Each drView2 As DataRowView In dvEditDeletePackagingDeliveryItems
                    drView2.Delete()
                Next
                _dsPackagingDelivery.AcceptChanges()
                'CalculateSalesOrderSummary(dsScan)
                'RefreshScanData(dsScan)
                'GridSetting()
            End If
            If dvEditDeleteItems.Count > 0 Then
                dvEditDeleteItems.AllowEdit = True

                For Each drView As DataRowView In dvEditDeleteItems

                    drView.Delete()

                Next
                If DtSOStr.Rows.Count > 0 Then
                    dvEditDeleteSTRItems = New DataView(DtSOStr, "DeliveryIndex=" & rowIndex, "", DataViewRowState.CurrentRows)
                    For Each drViewSTR As DataRowView In dvEditDeleteSTRItems
                        drViewSTR.Delete()
                    Next
                    DtSOStr.AcceptChanges()
                    dtSTR = DtSOStr.Copy()
                End If
                If Me.Batchbarcode.Where(Function(w) w.EAN = vEAN).FirstOrDefault() IsNot Nothing Then
                    Me.Batchbarcode.RemoveAll(Function(w) w.EAN = vEAN)
                End If


            End If
            If IsHeader Then
                If DtSoBulkRemarks.Rows.Count > 0 Then
                    dvRemarks = New DataView(DtSoBulkRemarks, "SrNo='" & rowIndex & "' ", "", DataViewRowState.CurrentRows)
                    If dvRemarks.Count > 0 Then
                        For Each drView2 As DataRowView In dvRemarks
                            drView2.Delete()
                        Next
                        DtSoBulkRemarks.AcceptChanges()
                    End If
                End If
            End If
            dsMain.Tables("SalesOrderTaxDtls").AcceptChanges()
            dsPackagingVar.AcceptChanges()
            _dsPackagingDelivery.AcceptChanges()
            _dsScan.AcceptChanges()

            CalculateSalesOrderSummary(dsScan)
            RefreshScanData(dsScan)
            GridSetting()
            LoadRemarks(DtSoBulkRemarks, True)


            If grdScanItem.Rows.Count = 1 Then
                ' CtrlProductImage.ClearImage()
                'PictureBoxImages.Image = Nothing

                lblTotalItem.Text = 0
                lblOrderQty.Text = 0
                lblPickupQty.Text = 0

                'CtrlCashSummary1.lbltxt1 = strZero
                lblGrossAmt1.Text = strZero
                CtrllblBaltoPay.Text = "0.00"
                CtrllblTaxAmt.Text = "0.00"
                CtrllblNetAmt.Text = "0.00"
                CtrllblAmtPaid.Text = "0.00"
                CtrllblTaxAmt.Text = "0.00"
                CtrllblMinAdv.Text = "0.00"
                CtrllblDiscAmt.Text = "0.00"
                CtrllblDiscAmt.Text = "0.00"
                CtrllblDiscPerc.Text = "0.00"
                CtrllblOtherCharges.Text = "0.00"
                lblOrderQty.Text = "0.00"
                lblPickupQty.Text = "0.00"
                CtrllblGrossAmt.Text = "0.00"



            End If

        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    ''' <summary>
    ''' Add Article In Scan DataGrid
    ''' </summary>
    ''' <param name="drItemsRow">Data Row</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    ''' 
    Private Function SetScanItemInSOTemp(ByVal drItemsRow As DataRow) As Boolean
        Dim foundRow As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("IsDocumentLevel = True")
        If foundRow.Count > 0 Then
            ResetDocTax(True, dtDocTaxes)
            dtDocTaxes.Clear()
        End If
        Dim findkeyTax(4) As Object

        Dim dtTaxCalc As DataTable
        Dim drAddItem As DataRow
        Dim drAddItemPackaging As DataRow

        Dim vTotalNetAmt As Double = 0.0
        Dim vIncTaxAmt As Double = 0.0
        Dim vExclTaxAmt As Double = 0.0
        Dim vGetArtilcePrice As Double = 0.0
        Dim prevQty As Integer = 0
        Dim ReservedQtyAllowed As Boolean

        Try

            Dim drRowTax As DataRow
            If IsCSTApplicable Then
                dtTaxCalc = objCM.getTax(vSiteCode, String.Empty, "SO201", drAddItem("Quantity"), drAddItem("EAN"), clsDefaultConfiguration.CSTTaxCode, True)
            Else
                dtTaxCalc = objCM.getTax(vSiteCode, drItemsRow.Item("ARTICLECODE"), "SO201", drItemsRow.Item("Quantity"), drItemsRow("EAN"), clsDefaultConfiguration.CSTTaxCode, False)
            End If

            For Each DRRemoverow In dsMain.Tables("SalesOrderTaxDtls").Rows
                If IsDBNull(DRRemoverow("RowIndex")) Then
                    DRRemoverow.Delete()
                End If
            Next

            If dtTaxCalc.Rows.Count <> 0 Then
                Dim vTaxLineNo As Integer = 0
                For Each drRowTax In dtTaxCalc.Rows
                    '   Dim results As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("RowIndex='" + drItemsRow("RowIndex").ToString() + "' and packagingindex='" + drItemsRow("PackagingIndex").ToString() + "' and TaxLabel='" + drRowTax("TaxCode") + "'")
                    Dim results As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("RowIndex='" + drItemsRow("RowIndex").ToString() + "'  and TaxLabel='" + drRowTax("TaxCode") + "'")
                    vTaxLineNo += 1
                    If results.Length > 0 Then
                        '  If IsHeader Then
                        '   results(0)("TaxValue") = ((Math.Round(drItemsRow("SellingPrice")) * drItemsRow("Quantity")) - drItemsRow("Discount")) * (drRowTax("Value") / 100)
                        results(0)("TaxValue") = Math.Round((Math.Round((drItemsRow("SellingPrice") * drItemsRow("Quantity")), 2) - drItemsRow("Discount")) * (drRowTax("Value") / 100), 2)
                        '  results(0)("TaxValue") = ((Math.Round(drItemsRow("SellingPrice"))) - drItemsRow("Discount")) * (drRowTax("Value") / 100)
                    End If
                Next
            End If
            dsMain.Tables("SalesOrderTaxDtls").AcceptChanges()

        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

        Return True
    End Function
    Private Function SetScanItemInSO(ByVal drItemsRow As DataRow) As Boolean
        Dim foundRow As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("IsDocumentLevel = True")
        If foundRow.Count > 0 Then
            ResetDocTax(True, dtDocTaxes)
            dtDocTaxes.Clear()
        End If
        Dim findkeyTax(4) As Object

        Dim dtTaxCalc As DataTable
        Dim drAddItem As DataRow
        Dim drAddItemPackaging As DataRow

        Dim vTotalNetAmt As Double = 0.0
        Dim vIncTaxAmt As Double = 0.0
        Dim vExclTaxAmt As Double = 0.0
        Dim vGetArtilcePrice As Double = 0.0
        Dim prevQty As Integer = 0
        Dim ReservedQtyAllowed As Boolean

        Try
            If Not (drItemsRow Is Nothing) Then
                StockQty = objCM.GetStocks(vSiteCode, drItemsRow.Item("EAN"), drItemsRow.Item("ArticleCode"), True, False, IIf(IsDBNull(drItemsRow.Item("BatchBarcode")) = False, drItemsRow.Item("BatchBarcode"), String.Empty))

                'Rakesh:06.11.2013-->7895 : Avoid stock check validation when order place from SO & BL
                'If CDbl(StockQty) <= 0 Then
                '    If clsDefaultConfiguration.NegativeInventoryAllowed = False Then
                '        ShowMessage(getValueByKey("SO001"), "SO001 - " & getValueByKey("CLAE04"))
                '        'ShowMessage("Article out of Stock.", "Information")
                '        Exit Function
                '    End If
                'End If

                If IsMRPOpen = True Then
                    Dim objPrompt As New frmSpecialPrompt(getValueByKey("CMR15"))
                    objPrompt.ShowMessage = False
                    objPrompt.ShowTextBox = True
                    objPrompt.AllowDecimal = True
                    objPrompt.txtValue.MaxLength = 14
                    objPrompt.ShowDialog()
                    If IsNumeric(objPrompt.GetResult()) = True Then
                        vGetArtilcePrice = objPrompt.GetResult()
                    Else
                        ShowMessage("", "")
                        Exit Function
                    End If

                    objPrompt.Dispose()

                    If CDbl(vGetArtilcePrice) <= 0 Then
                        ShowMessage(getValueByKey("SO002"), "SO002 - " & getValueByKey("CLAE04"))
                        'ShowMessage("Article is Removing As no Price found on it.", "Information")
                        Exit Function
                    End If
                Else
                    vGetArtilcePrice = drItemsRow.Item("SELLINGPRICE")
                End If
                ''---- If it is case of combo then need to add new record each time 
                'Dim dr() = DtSoBulkComboHdr.Select("PackagingBoxCode='" & drItemsRow.Item("ArticleCode") & "'")
                'If dr.Count > 1 Then
                '    drAddItemExists = dsScan.Tables("ItemScanDetails").Select("EAN='A00 df sfs000NOT'")
                'Else
                '    If IsDBNull(drItemsRow.Item("BatchBarcode")) = False Then
                '        drAddItemExists = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "' And BatchBarcode = '" & drItemsRow.Item("BatchBarcode") & "'")
                '    Else
                '        drAddItemExists = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "' And BatchBarcode IS NULL")
                '    End If
                'End If
                drAddItemExists = fnAnalyzeItem(drItemsRow)

                If (grdScanItem.Rows.Count > 1) Then
                    ReservedQtyAllowed = grdScanItem.Item(grdScanItem.Rows.Count - 1, "ReservedQty")

                    If (drAddItemExists.Count = 0) Then
                        ReservedQtyAllowed = False
                    End If

                    If (ReservedQtyAllowed) Then
                        Dim OrderQty As Decimal = grdScanItem.Item(grdScanItem.Rows.Count - 1, "Quantity")

                        If (StockQty < OrderQty + 1) Then
                            ShowMessage(getValueByKey("SO001"), "SO001 - " & getValueByKey("CLAE04"))
                            Return False
                        End If
                    End If
                End If


                If drAddItemExists.Length = 0 Then
                    drAddItem = _dsScan.Tables("ItemScanDetails").NewRow
                    drAddItem("Quantity") = 1
                    drAddItem("PickUpQty") = 0
                    drAddItem("SrNo") = vRowIndex
                    drAddItem("PackagingIndex") = vPackagingIndex
                    drAddItem("DeliveryIndex") = vDeliveryIndex
                    drAddItem("RowIndex") = vRowIndex
                Else
                    drAddItem = drAddItemExists(0)
                    vGetArtilcePrice = drAddItem("SELLINGPRICE")
                    'If IsEditItem = True Then
                    '    drAddItem("Quantity") = CDbl(grdScanItem.Item(grdScanItem.Row, "Quantity"))
                    '    IsEditItem = False
                    'Else
                    prevQty = drAddItem("Quantity")
                    drAddItem("Quantity") = drAddItem("Quantity") + 1
                    'End If
                End If

                drAddItem("IsImageReq") = 1
                drAddItem("ArticleType") = drItemsRow.Item("ArticalTypeCode")
                If drItemsRow.Item("ArticalTypeCode") = "Combo" Then
                    drAddItem("IsCombo") = True
                Else
                    drAddItem("IsCombo") = False
                End If
                drAddItem("EAN") = drItemsRow.Item("EAN")
                drAddItem("DeliverySiteCode") = DeliverySiteCode
                drAddItem("Discription") = drItemsRow.Item("DISCRIPTION")
                drAddItem("BatchBarcode") = drItemsRow.Item("BatchBarcode")
                drAddItem("IsHeader") = True

                If drAddItemExists.Length = 0 Then
                    drAddItem("SellingPrice") = FormatNumber(Math.Round(vGetArtilcePrice, 3), 2)
                End If

                drAddItem("Discount") = 0
                drAddItem("LastNodeCode") = drItemsRow.Item("Nodes").ToString()

                If IsCSTApplicable Then
                    dtTaxCalc = objCM.getTax(vSiteCode, String.Empty, "SO201", drAddItem("Quantity"), drAddItem("EAN"), clsDefaultConfiguration.CSTTaxCode, True)
                Else
                    dtTaxCalc = objCM.getTax(vSiteCode, drItemsRow.Item("ARTICLECODE"), "SO201", drAddItem("Quantity"), drAddItem("EAN"), clsDefaultConfiguration.CSTTaxCode, False)
                End If

                If dtTaxCalc.Rows.Count > 0 AndAlso Not dtTaxCalc Is Nothing Then 'vipin
                    drAddItem("TaxInPer") = dtTaxCalc.Compute("Sum(Value)", "")
                    drAddItem("TaxPer") = dtTaxCalc.Compute("Sum(Value)", "")
                Else
                    drAddItem("TaxInPer") = 0
                    drAddItem("TaxPer") = 0
                End If
                vTotalNetAmt = Math.Round(vGetArtilcePrice * drAddItem("Quantity"), 3)

                If dtTaxCalc.Rows.Count <> 0 Then
                    If IsCSTApplicable Then
                        Dim inctax = GetTaxableAmountForCst(drItemsRow.Item("ARTICLECODE"), drItemsRow.Item("EAN"), drAddItem("Quantity"), vTotalNetAmt)
                        vTotalNetAmt = vTotalNetAmt - inctax
                        dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = vTotalNetAmt
                        objCM.getCalculatedDataSet(dtTaxCalc)
                        vTotalNetAmt = FormatNumber(vTotalNetAmt + dtTaxCalc(0)("TAXAMOUNT"), 2)

                        If drAddItemExists.Length = 0 Then
                            drAddItem("SellingPrice") = FormatNumber(vGetArtilcePrice - (inctax / dtTaxCalc(0)("ITEMQTY")), 2)
                        End If
                    Else
                        dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = vTotalNetAmt
                        objCM.getCalculatedDataSet(dtTaxCalc)
                    End If

                    If drAddItemExists.Length = 0 Then
                        For iRowTax = 0 To dtTaxCalc.Rows.Count - 1

                            If CDbl(dtTaxCalc.Rows(iRowTax)("VALUE").ToString) <> 0 Then
                                If dtTaxCalc.Rows(iRowTax)("INCLUSIVE") = True Then
                                    vIncTaxAmt = FormatNumber(vIncTaxAmt + CDbl(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT")), 2)
                                Else
                                    vExclTaxAmt = FormatNumber(vExclTaxAmt + CDbl(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT")), 2)
                                End If
                            End If
                        Next
                    Else
                        If (drAddItemExists(0)("IncTaxAmt") IsNot DBNull.Value) Then
                            vIncTaxAmt = FormatNumber((drAddItemExists(0)("IncTaxAmt") / prevQty) * drAddItemExists(0)("Quantity"), 2)
                        End If

                        If (drAddItemExists(0)("ExclTaxAmt") IsNot DBNull.Value) Then
                            vExclTaxAmt = FormatNumber((drAddItemExists(0)("ExclTaxAmt") / prevQty) * drAddItemExists(0)("Quantity"), 2)
                        End If
                    End If

                    vIncTaxAmt = Math.Round(vIncTaxAmt, 2)
                    vExclTaxAmt = Math.Round(vExclTaxAmt, 2)
                    '---- Commented By Mahesh Now not showing Reduced Price ...
                    'If IsCSTApplicable = False AndAlso drAddItemExists.Length = 0 Then
                    '    drAddItem("SellingPrice") = FormatNumber(vGetArtilcePrice - (vIncTaxAmt / dtTaxCalc(0)("ITEMQTY")), 2)
                    'Else
                    'End If
                    Dim drRowTax As DataRow
                    If dtTaxCalc.Rows.Count <> 0 Then

                        Dim vTaxLineNo As Integer = 0

                        For Each drRowTax In dtTaxCalc.Rows
                            Dim results As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("RowIndex='" + drAddItem("RowIndex").ToString() + "' and packagingindex='" + drAddItem("PackagingIndex").ToString() + "' and TaxLabel='" + drRowTax("TaxCode") + "'")
                            vTaxLineNo += 1

                            findkeyTax(0) = vSiteCode
                            findkeyTax(1) = clsAdmin.Financialyear
                            findkeyTax(2) = CtrlTxtOrderNo.Text
                            findkeyTax(3) = drAddItem("EAN")

                            'findkeyTax(4) = drRowTax("TaxCode")
                            If drAddItemExists.Length = 0 Then
                                findkeyTax(4) = vtaxIndex
                            Else

                                findkeyTax(4) = results(0)("TaxLineNo")
                            End If
                            drTax = dsMain.Tables("SalesOrderTaxDtls").Rows.Find(findkeyTax)

                            If drTax Is Nothing Then
                                drTax = dsMain.Tables("SalesOrderTaxDtls").NewRow

                                drTax("SiteCode") = vSiteCode
                                drTax("FinYear") = clsAdmin.Financialyear
                                drTax("SaleOrderNumber") = CtrlTxtOrderNo.Text
                                drTax("EAN") = drAddItem("EAN")
                                drTax("TaxLineNo") = vtaxIndex
                                drTax("TaxLabel") = drRowTax("TaxCode")
                                drTax("TaxValue") = Math.Round(drRowTax("TaxAmount"), 2)
                                drTax("IsDocumentLevel") = False
                                drTax("RowIndex") = vRowIndex
                                drTax("PackagingIndex") = vPackagingIndex
                                drTax("IsHeader") = True
                                dsMain.Tables("SalesOrderTaxDtls").Rows.Add(drTax)
                                vtaxIndex = vtaxIndex + 1
                            Else
                                drTax("SiteCode") = vSiteCode
                                drTax("SaleOrderNumber") = CtrlTxtOrderNo.Text
                                drTax("EAN") = drAddItem("EAN")
                                drTax("TaxLineNo") = results(0)("TaxLineNo")
                                drTax("TaxLabel") = drRowTax("TaxCode")
                                drTax("TaxValue") = Math.Round(drRowTax("TaxAmount"), 2)
                                drTax("IsDocumentLevel") = False
                            End If
                        Next
                    End If

                    drAddItem("ExclTaxAmt") = vExclTaxAmt
                    drAddItem("IncTaxAmt") = vIncTaxAmt
                    drAddItem("TotalTaxAmt") = vExclTaxAmt + vIncTaxAmt
                Else
                    drAddItem("ExclTaxAmt") = 0
                    drAddItem("IncTaxAmt") = 0
                    drAddItem("TotalTaxAmt") = 0
                End If

                If Not (vExclTaxAmt > 0) And Not (vIncTaxAmt > 0) Then
                    If clsDefaultConfiguration.ArticleTaxAllowed = False Then
                        ShowMessage(getValueByKey("CM019"), "CM019 - " & getValueByKey("CLAE04"))
                        'ShowMessage("Article cannot be billed because there is no tax attached with Article", "Article Information")
                        Exit Function
                    End If
                End If

                drAddItem("NetAmount") = FormatNumber(vTotalNetAmt + vExclTaxAmt, 2)
                'drAddItem("ExpDelDate") = CtrlSalesInfo1.CtrlDtExpDelDate.Value
                drAddItem("Stock") = StockQty
                drAddItem("IsCLP") = True

                drAddItem("ArticleCode") = drItemsRow.Item("ARTICLECODE")
                drAddItem("UOM") = drItemsRow.Item("UOM")
                drAddItem("GrossAmt") = Math.Round(drAddItem("SellingPrice") * drAddItem("Quantity"), 3)

                drAddItem("DeliveredQty") = 0
                drAddItem("ReservedQty") = ReservedQtyAllowed
                drAddItem("CLPPoints") = 0
                drAddItem("CLPDiscount") = 0

                TotalSalesQty = drAddItem("PickUpQty") + drAddItem("DeliveredQty")
                NetArticleRate = drAddItem("NetAmount") / drAddItem("Quantity")
                drAddItem("MinPayAmt") = Math.Round(((drAddItem("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate), 3)
                drAddItem("PromotionId") = 0
                drAddItem("PckgSize") = 0
                drAddItem("PckgQty") = 0
                drAddItem("LineDiscount") = 0
                drAddItem("TotalDiscPercentage") = 0
                drAddItem("FirstLevel") = String.Empty
                drAddItem("TopLevel") = String.Empty
                drAddItem("IsStatus") = "Inserted"
                drAddItem("CostAmount") = drItemsRow.Item("CostAmt")




                If Not (drAddItem("NetAmount") = 0.0) Then
                    If drAddItemExists.Length = 0 Then

                        'Change by Ashish on 29 Nov 2010
                        'Commenting the below line since it was adding rows one below other in the grid
                        'instead of adding the recent scanned item on top of the grid
                        '_dsScan.Tables("ItemScanDetails").Rows.Add(drAddItem)
                        _dsScan.Tables("ItemScanDetails").Rows.InsertAt(drAddItem, 0)
                        'drAddItemPackaging = _dsPackagingVar.Tables("PackagingMaterial").NewRow
                        'drAddItemPackaging.ItemArray = drAddItem.ItemArray
                        '_dsPackagingVar.Tables("PackagingMaterial").Rows.InsertAt(drAddItemPackaging, 0)
                        'end of change


                        SetPackagingBaseVarionInSO(drAddItem)

                        vRowIndex = vRowIndex + 1
                        vPackagingIndex = vPackagingIndex + 1
                        vDeliveryIndex = vDeliveryIndex + 1
                    Else
                        Dim result As DataRow() = _dsPackagingVar.Tables("PackagingMaterial").Select("RowIndex='" + drAddItem("RowIndex").ToString() + "' and packagingindex='" + drAddItem("PackagingIndex").ToString() + "'")
                        If result.Length > 0 Then
                            result(0)("Quantity") = drAddItem("Quantity")
                            result(0)("GrossAmt") = drAddItem("GrossAmt")
                            result(0)("NetAmount") = drAddItem("NetAmount")
                            result(0)("IncTaxAmt") = drAddItem("IncTaxAmt")
                            result(0)("ExclTaxAmt") = drAddItem("ExclTaxAmt")
                            result(0)("TotalTaxAmt") = drAddItem("TotalTaxAmt")
                            result(0)("TaxInPer") = drAddItem("TaxInPer")   'vipin
                            result(0)("TaxPer") = drAddItem("TaxInPer") '' added by ketan PC GST UI Changes 

                        End If
                        _dsPackagingVar.AcceptChanges()
                    End If
                Else
                    ShowMessage(getValueByKey("SO004"), "SO004 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Article Tax Details does not Found. ", "Tax Details")
                End If

                _dsScan.AcceptChanges()
            End If
            'grdScanItem.Select(grdScanItem.Rows.Count - 1, 1, True)
            'grdScanItem.Select(1, 1, True)
            'If IsDBNull(drAddItem("BatchBarcode")) = False Then
            '    drAddItem("PickUpQty") = drAddItem("Quantity")
            '    grdScanItem_AfterEdit(grdScanItem, New C1.Win.C1FlexGrid.RowColEventArgs(1, grdScanItem.Cols("PickUpQty").Index))

            '    '_dsScan.AcceptChanges()
            'End If
            'For drindex = 0 To dtDocTaxes.Rows.Count - 1
            '    'dr("TaxValue") = dr("TaxValue") + dtDocTaxes.Rows(drindex)("TaxAmt")
            '    result(0)("TotalTaxAmt") = result(0)("TotalTaxAmt") + dtDocTaxes.Rows(drindex)("TaxAmt")
            '    result(0)("NetAmount") = result(0)("NetAmount") + dtDocTaxes.Rows(drindex)("TaxAmt")
            '    If dtDocTaxes.Rows(drindex)("Type") = "Exclusive" Then
            '        result(0)("ExclTaxAmt") = result(0)("ExclTaxAmt") + dtDocTaxes.Rows(drindex)("TaxAmt")
            '    Else
            '        result(0)("IncTaxAmt") = result(0)("IncTaxAmt") + dtDocTaxes.Rows(drindex)("TaxAmt")
            '    End If


            '    resultPackVar(0)("TotalTaxAmt") = resultPackVar(0)("TotalTaxAmt") + dtDocTaxes.Rows(drindex)("TaxAmt")
            '    resultPackVar(0)("NetAmount") = resultPackVar(0)("NetAmount") + dtDocTaxes.Rows(drindex)("TaxAmt")
            '    If dtDocTaxes.Rows(drindex)("Type") = "Exclusive" Then
            '        resultPackVar(0)("ExclTaxAmt") = resultPackVar(0)("ExclTaxAmt") + dtDocTaxes.Rows(drindex)("TaxAmt")
            '    Else
            '        resultPackVar(0)("IncTaxAmt") = resultPackVar(0)("IncTaxAmt") + dtDocTaxes.Rows(drindex)("TaxAmt")
            '    End If



            'Next

            ResetNewTax(True, dtDocTaxes)
            If Batchbarcode IsNot Nothing AndAlso Batchbarcode.Select(Function(w) w.EAN = drAddItem("EAN").ToString()).Count() > 0 Then

                If (ArticleScanWithBatchBarcode AndAlso StockQty > drAddItem("PickUpQty")) Then
                    drAddItem("PickUpQty") = Batchbarcode.Where(Function(w) w.EAN = drAddItem("EAN").ToString()).Sum(Function(w) w.Qty)
                ElseIf (ArticleScanWithBatchBarcode AndAlso ReservedQtyAllowed) Then
                    ShowMessage(getValueByKey("SO001"), "SO001 - " & getValueByKey("CLAE04"))
                End If

                grdScanItem_AfterEdit(grdScanItem, New C1.Win.C1FlexGrid.RowColEventArgs(1, grdScanItem.Cols("PickUpQty").Index))
            End If

            ArticleScanWithBatchBarcode = False

        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

        Return True
    End Function

    Private Function AddComoInScanItemInSO(ByVal drItemsRow As DataRow) As Boolean
        Dim findkeyTax(4) As Object
        Dim dtTaxCalc As DataTable
        Dim drAddItem As DataRow
        Dim drAddItemPackaging As DataRow

        Dim vTotalNetAmt As Double = 0.0
        Dim vIncTaxAmt As Double = 0.0
        Dim vExclTaxAmt As Double = 0.0
        Dim vGetArtilcePrice As Double = 0.0
        Dim prevQty As Integer = 0
        Dim ReservedQtyAllowed As Boolean
        Dim vArticleCode As String = ""
        Try

            drAddItem = _dsScan.Tables("ItemScanDetails").NewRow

            drAddItem("SrNo") = vRowIndex
            drAddItem("PackagingIndex") = vPackagingIndex
            drAddItem("DeliveryIndex") = vDeliveryIndex
            drAddItem("IsImageReq") = 1
            drAddItem("ArticleType") = "Combo"
            '' added by nikhil
            If clsDefaultConfiguration.IsNewSalesOrder Then
                If drAddItem("ArticleType") = "Combo" Then
                    If lblArticleCombo.Rows.Count > 0 Then
                        drAddItem("SellingPrice") = CDbl(lblArticleCombo.Rows(0)("GrossAmount"))
                        drAddItem("TaxInPer") = lblArticleCombo.Rows(0)("Tax") '' $$ added by nikhil
                        drAddItem("TaxPer") = lblArticleCombo.Rows(0)("Tax") ''' added by ketan GST UI Chnages

                        'drAddItem("TotalTaxAmt") = lblArticleCombo.Rows(0)("TaxAmount").ToString    ''' highest taxamt for Article wise in combo
                        'drAddItem("ExclTaxAmt") = lblArticleCombo.Rows(0)("TaxAmount").ToString
                        drAddItem("Discount") = lblArticleCombo.Rows(0)("Discount").ToString
                    End If

                End If

            End If
            'drAddItem("EAN") = drItemsRow.Item("EAN")
            drAddItem("DeliverySiteCode") = DeliverySiteCode
            drAddItem("Discription") = drItemsRow.Item("PackagingBoxPrintName")
            'drAddItem("BatchBarcode") = drItemsRow.Item("BatchBarcode")
            drAddItem("IsHeader") = True
            drAddItem("IsCombo") = True
            drAddItem("PackagingMaterial") = ""
            drAddItem("CLPDiscount") = 0
            'drAddItem("TotalTaxAmt") = 0 '' commented by nikhil
            drAddItem("IncTaxAmt") = 0
            'drAddItem("ExclTaxAmt") = 0
            drAddItem("FirstLevel") = ""
            drAddItem("TopLevel") = ""
            drAddItem("LineDiscount") = 0
            drAddItem("TotalDiscPercentage") = 0

            drAddItem("Quantity") = 1
            If clsDefaultConfiguration.IsNewSalesOrder Then
                If drAddItem("ArticleType") = "Combo" Then
                    If lblArticleCombo.Rows.Count > 0 Then
                        drAddItem("TotalTaxAmt") = Math.Round(((drAddItem("SellingPrice") * drAddItem("Quantity")) - drAddItem("LineDiscount")) * (CDbl(lblArticleCombo.Rows(0)("Tax") / 100)), 2) ''' highest taxamt for Article wise in combo
                        drAddItem("ExclTaxAmt") = drAddItem("TotalTaxAmt")

                    End If
                End If
            End If
            drAddItem("PickUpQty") = 0
            drAddItem("DeliveredQty") = 0
            drAddItem("PckgSize") = 0
            drAddItem("PckgQty") = 0
            drAddItem("IsClp") = True
            Dim resultCombo As DataRow() = dtPackagingPrintBox.Select("KeyValue='" + drItemsRow.Item("PackagingBoxPrintName") + "' ")
            If resultCombo.Length > 0 Then
                vArticleCode = resultCombo(0)("KeyCode")
                drAddItem("ArticleCode") = vArticleCode
            Else
                drAddItem("ArticleCode") = ""
            End If
            drAddItem("Discount") = 0

            If clsDefaultConfiguration.IsNewSalesOrder Then
                If drAddItem("ArticleType") = "Combo" Then
                    If lblArticleCombo.Rows.Count > 0 Then
                        drAddItem("GrossAmt") = CDbl(lblArticleCombo.Rows(0)("GrossAmount"))
                        drAddItem("NetAmount") = Math.Round(CDbl(lblArticleCombo.Rows(0)("GrossAmount")) + CDbl(drAddItem("TotalTaxAmt")), 2) 'vipul

                    End If

                End If

            End If
            drAddItem("IsStatus") = "Inserted"

            drAddItem("UOM") = "NOS"

            drAddItem("RowIndex") = vRowIndex

            _dsScan.Tables("ItemScanDetails").Rows.InsertAt(drAddItem, 0)




            _dsScan.AcceptChanges()



            ''''Add in package variation



            drAddItemPackaging = _dsPackagingVar.Tables("PackagingMaterial").NewRow
            drAddItemPackaging("Quantity") = 1
            drAddItemPackaging("PickUpQty") = 0
            drAddItemPackaging("DeliveredQty") = 0




            drAddItemPackaging("SrNo") = vRowIndex

            drAddItemPackaging("PackagingIndex") = vPackagingIndex
            drAddItemPackaging("DeliveryIndex") = vDeliveryIndex
            drAddItemPackaging("ArticleType") = "Combo"
            ' drAddItemPackaging("EAN") = drItemsRow.Item("EAN")
            drAddItemPackaging("DeliverySiteCode") = DeliverySiteCode
            drAddItemPackaging("Discription") = drItemsRow.Item("PackagingBoxPrintName")


            drAddItemPackaging("IsHeader") = True
            drAddItemPackaging("TotalTaxAmt") = 0  '' commented by nikhil
            If clsDefaultConfiguration.IsNewSalesOrder Then
                'drAddItemPackaging("ArticleLevelTax") = lblArticleCombo.Rows(0)("TaxAmount").ToString
                Dim dtCombo As New DataTable                      '' added on 21/07/17
                dtCombo.Clear()
                'dtCombo = _dsScan.Tables("ItemScanDetails").Select("ArticleType='Combo'").CopyToDataTable
                If grdScanItem.Rows.Count > 1 Then
                    Dim displaySrNo As String = "0"
                    '  displaySrNo = IIf(grdScanItem.Item(grdScanItem.Row, "SrNo") Is DBNull.Value, 0, grdScanItem.Item(grdScanItem.Row, "rowindex"))  '' $$ added by nikhil for tax calculation
                    '  dtCombo = _dsScan.Tables("ItemScanDetails").Select("ArticleType='Combo' And SrNo <> '" & displaySrNo & "'").CopyToDataTable
                    ''added by ketan for unique tax line no 
                    dtCombo = _dsScan.Tables("ItemScanDetails").Select("").CopyToDataTable
                    For Each row As C1.Win.C1FlexGrid.Row In grdScanItem.Rows
                        displaySrNo = IIf(row("SrNo").ToString() Is DBNull.Value, 0, row("rowindex").ToString())
                        If displaySrNo = "Sr.No" Or displaySrNo = "RowIndex" Then
                        Else
                            dtCombo = dtCombo.Select("ArticleType='Combo' And SrNo <> '" & displaySrNo & "'").CopyToDataTable
                        End If
                    Next
                Else
                    dtCombo = _dsScan.Tables("ItemScanDetails").Select("ArticleType='Combo'").CopyToDataTable
                End If

                If Not dtCombo Is Nothing And dtCombo.Rows.Count > 0 Then
                    drAddItemPackaging("SellingPrice") = dtCombo.Rows(0)("SellingPrice")
                    drAddItemPackaging("NetAmount") = dtCombo.Rows(0)("NetAmount")
                    drAddItemPackaging("GrossAmt") = dtCombo.Rows(0)("GrossAmt")
                    drAddItemPackaging("TotalTaxAmt") = dtCombo.Rows(0)("TotalTaxAmt")
                    drAddItemPackaging("ExclTaxAmt") = dtCombo.Rows(0)("TotalTaxAmt")
                    drAddItemPackaging("TaxInPer") = dtCombo.Rows(0)("TaxInPer") '' $$ added by nikhil
                End If

            End If
            drAddItemPackaging("IncTaxAmt") = 0
            drAddItemPackaging("IsCombo") = True
            ' drAddItemPackaging("ExclTaxAmt") = 0 '' commented by nikhil
            drAddItemPackaging("IsStatus") = "Inserted"
            drAddItemPackaging("CLPDiscount") = 0

            drAddItemPackaging("FirstLevel") = ""
            drAddItemPackaging("TopLevel") = ""


            drAddItemPackaging("LineDiscount") = 0
            drAddItemPackaging("TotalDiscPercentage") = 0

            drAddItemPackaging("Discount") = 0

            drAddItemPackaging("PckgSize") = 0
            drAddItemPackaging("PckgQty") = 0



            ' drAddItemPackaging("NetAmount") = drItemsRow.Item("NetAmount").ToString()
            'drAddItem("ExpDelDate") = CtrlSalesInfo1.CtrlDtExpDelDate.Value

            drAddItemPackaging("IsCLP") = True

            drAddItemPackaging("ArticleCode") = vArticleCode
            drAddItemPackaging("UOM") = "NOS"


            drAddItemPackaging("RowIndex") = vRowIndex
            _dsPackagingVar.Tables("PackagingMaterial").Rows.InsertAt(drAddItemPackaging, 0)
            _dsPackagingVar.AcceptChanges()

            vRowIndex = vRowIndex + 1
            vPackagingIndex = vPackagingIndex + 1
            vDeliveryIndex = vDeliveryIndex + 1
        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

        Return True
    End Function


    Private Function UpdateComoInScanItemInSO(ByVal drItemsRow As DataRow) As Boolean
        Dim findkeyTax(4) As Object
        Dim dtTaxCalc As DataTable
        Dim drAddItem As DataRow
        Dim drAddItemPackaging As DataRow

        Dim vTotalNetAmt As Double = 0.0
        Dim vIncTaxAmt As Double = 0.0
        Dim vExclTaxAmt As Double = 0.0
        Dim vGetArtilcePrice As Double = 0.0
        Dim prevQty As Integer = 0
        Dim ReservedQtyAllowed As Boolean

        Try
            If Not (drItemsRow Is Nothing) Then
                StockQty = objCM.GetStocks(vSiteCode, drItemsRow.Item("EAN"), drItemsRow.Item("ArticleCode"), True, False, IIf(IsDBNull(drItemsRow.Item("BatchBarcode")) = False, drItemsRow.Item("BatchBarcode"), String.Empty))

                'Rakesh:06.11.2013-->7895 : Avoid stock check validation when order place from SO & BL
                'If CDbl(StockQty) <= 0 Then
                '    If clsDefaultConfiguration.NegativeInventoryAllowed = False Then
                '        ShowMessage(getValueByKey("SO001"), "SO001 - " & getValueByKey("CLAE04"))
                '        'ShowMessage("Article out of Stock.", "Information")
                '        Exit Function
                '    End If
                'End If

                If IsMRPOpen = True Then
                    Dim objPrompt As New frmSpecialPrompt(getValueByKey("CMR15"))
                    objPrompt.ShowMessage = False
                    objPrompt.ShowTextBox = True
                    objPrompt.AllowDecimal = True
                    objPrompt.txtValue.MaxLength = 14
                    objPrompt.ShowDialog()
                    If IsNumeric(objPrompt.GetResult()) = True Then
                        vGetArtilcePrice = objPrompt.GetResult()
                    Else
                        ShowMessage("", "")
                        Exit Function
                    End If

                    objPrompt.Dispose()

                    If CDbl(vGetArtilcePrice) <= 0 Then
                        ShowMessage(getValueByKey("SO002"), "SO002 - " & getValueByKey("CLAE04"))
                        'ShowMessage("Article is Removing As no Price found on it.", "Information")
                        Exit Function
                    End If
                Else
                    vGetArtilcePrice = drItemsRow.Item("SELLINGPRICE")
                End If
                ''---- If it is case of combo then need to add new record each time 
                'Dim dr() = DtSoBulkComboHdr.Select("PackagingBoxCode='" & drItemsRow.Item("ArticleCode") & "'")
                'If dr.Count > 1 Then
                '    drAddItemExists = dsScan.Tables("ItemScanDetails").Select("EAN='A00 df sfs000NOT'")
                'Else
                '    If IsDBNull(drItemsRow.Item("BatchBarcode")) = False Then
                '        drAddItemExists = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "' And BatchBarcode = '" & drItemsRow.Item("BatchBarcode") & "'")
                '    Else
                '        drAddItemExists = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "' And BatchBarcode IS NULL")
                '    End If
                'End If
                drAddItemExists = fnAnalyzeItem(drItemsRow)

                If (grdScanItem.Rows.Count > 1) Then
                    ReservedQtyAllowed = grdScanItem.Item(grdScanItem.Rows.Count - 1, "ReservedQty")

                    If (drAddItemExists.Count = 0) Then
                        ReservedQtyAllowed = False
                    End If

                    If (ReservedQtyAllowed) Then
                        Dim OrderQty As Decimal = grdScanItem.Item(grdScanItem.Rows.Count - 1, "Quantity")

                        If (StockQty < OrderQty + 1) Then
                            ShowMessage(getValueByKey("SO001"), "SO001 - " & getValueByKey("CLAE04"))
                            Return False
                        End If
                    End If
                End If


                If drAddItemExists.Length = 0 Then
                    drAddItem = _dsScan.Tables("ItemScanDetails").NewRow
                    drAddItem("Quantity") = 1
                    drAddItem("PickUpQty") = 0

                Else
                    drAddItem = drAddItemExists(0)

                    'If IsEditItem = True Then
                    '    drAddItem("Quantity") = CDbl(grdScanItem.Item(grdScanItem.Row, "Quantity"))
                    '    IsEditItem = False
                    'Else
                    prevQty = drAddItem("Quantity")
                    drAddItem("Quantity") = drAddItem("Quantity") + 1
                    'End If
                End If
                drAddItem("SrNo") = vRowIndex
                drAddItem("PackagingIndex") = vPackagingIndex
                drAddItem("DeliveryIndex") = vDeliveryIndex
                drAddItem("IsImageReq") = 1
                drAddItem("ArticleType") = drItemsRow.Item("ArticalTypeCode")
                drAddItem("EAN") = drItemsRow.Item("EAN")
                drAddItem("DeliverySiteCode") = DeliverySiteCode
                drAddItem("Discription") = drItemsRow.Item("DISCRIPTION")
                drAddItem("BatchBarcode") = drItemsRow.Item("BatchBarcode")
                drAddItem("IsHeader") = True

                If drAddItemExists.Length = 0 Then
                    drAddItem("SellingPrice") = FormatNumber(Math.Round(vGetArtilcePrice, 3), 2)
                End If

                drAddItem("Discount") = 0
                drAddItem("LastNodeCode") = drItemsRow.Item("Nodes").ToString()

                If IsCSTApplicable Then
                    dtTaxCalc = objCM.getTax(vSiteCode, String.Empty, "SO201", drAddItem("Quantity"), drAddItem("EAN"), clsDefaultConfiguration.CSTTaxCode, True)
                Else
                    dtTaxCalc = objCM.getTax(vSiteCode, drItemsRow.Item("ARTICLECODE"), "SO201", drAddItem("Quantity"), drAddItem("EAN"), clsDefaultConfiguration.CSTTaxCode, False)
                End If

                vTotalNetAmt = Math.Round(vGetArtilcePrice * drAddItem("Quantity"), 3)

                If dtTaxCalc.Rows.Count <> 0 Then
                    If IsCSTApplicable Then
                        Dim inctax = GetTaxableAmountForCst(drItemsRow.Item("ARTICLECODE"), drItemsRow.Item("EAN"), drAddItem("Quantity"), vTotalNetAmt)
                        vTotalNetAmt = vTotalNetAmt - inctax
                        dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = vTotalNetAmt
                        objCM.getCalculatedDataSet(dtTaxCalc)
                        vTotalNetAmt = FormatNumber(vTotalNetAmt + dtTaxCalc(0)("TAXAMOUNT"), 2)

                        If drAddItemExists.Length = 0 Then
                            drAddItem("SellingPrice") = FormatNumber(vGetArtilcePrice - (inctax / dtTaxCalc(0)("ITEMQTY")), 2)
                        End If
                    Else
                        dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = vTotalNetAmt
                        objCM.getCalculatedDataSet(dtTaxCalc)
                    End If

                    If drAddItemExists.Length = 0 Then
                        For iRowTax = 0 To dtTaxCalc.Rows.Count - 1

                            If CDbl(dtTaxCalc.Rows(iRowTax)("VALUE").ToString) <> 0 Then
                                If dtTaxCalc.Rows(iRowTax)("INCLUSIVE") = True Then
                                    vIncTaxAmt = FormatNumber(vIncTaxAmt + CDbl(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT")), 2)
                                Else
                                    vExclTaxAmt = FormatNumber(vExclTaxAmt + CDbl(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT")), 2)
                                End If
                            End If
                        Next
                    Else
                        If (drAddItemExists(0)("IncTaxAmt") IsNot DBNull.Value) Then
                            vIncTaxAmt = FormatNumber((drAddItemExists(0)("IncTaxAmt") / prevQty) * drAddItemExists(0)("Quantity"), 2)
                        End If

                        If (drAddItemExists(0)("ExclTaxAmt") IsNot DBNull.Value) Then
                            vExclTaxAmt = FormatNumber((drAddItemExists(0)("ExclTaxAmt") / prevQty) * drAddItemExists(0)("Quantity"), 2)
                        End If
                    End If

                    vIncTaxAmt = Math.Round(vIncTaxAmt, 2)
                    vExclTaxAmt = Math.Round(vExclTaxAmt, 2)
                    '---- Commented By Mahesh Now not showing Reduced Price ...
                    'If IsCSTApplicable = False AndAlso drAddItemExists.Length = 0 Then
                    '    drAddItem("SellingPrice") = FormatNumber(vGetArtilcePrice - (vIncTaxAmt / dtTaxCalc(0)("ITEMQTY")), 2)
                    'Else
                    'End If
                    Dim drRowTax As DataRow
                    If dtTaxCalc.Rows.Count <> 0 Then

                        Dim vTaxLineNo As Integer = 0

                        For Each drRowTax In dtTaxCalc.Rows
                            vTaxLineNo += 1

                            findkeyTax(0) = vSiteCode
                            findkeyTax(1) = clsAdmin.Financialyear
                            'findkeyTax(2) = CtrlSalesInfo1.CtrlTxtOrderNo.Text
                            findkeyTax(3) = drAddItem("EAN")
                            findkeyTax(4) = vTaxLineNo
                            drTax = dsMain.Tables("SalesOrderTaxDtls").Rows.Find(findkeyTax)

                            If drTax Is Nothing Then
                                drTax = dsMain.Tables("SalesOrderTaxDtls").NewRow

                                drTax("SiteCode") = vSiteCode
                                drTax("FinYear") = clsAdmin.Financialyear
                                ' drTax("SaleOrderNumber") = CtrlSalesInfo1.CtrlTxtOrderNo.Text
                                drTax("EAN") = drAddItem("EAN")
                                drTax("TaxLineNo") = vTaxLineNo
                                drTax("TaxLabel") = drRowTax("TaxCode")
                                drTax("TaxValue") = Math.Round(drRowTax("TaxAmount"), 2)

                                dsMain.Tables("SalesOrderTaxDtls").Rows.Add(drTax)
                            Else
                                drTax("SiteCode") = vSiteCode
                                'drTax("SaleOrderNumber") = CtrlSalesInfo1.CtrlTxtOrderNo.Text
                                drTax("EAN") = drAddItem("EAN")
                                drTax("TaxLineNo") = vTaxLineNo
                                drTax("TaxLabel") = drRowTax("TaxCode")
                                drTax("TaxValue") = Math.Round(drRowTax("TaxAmount"), 2)
                            End If
                        Next
                    End If

                    drAddItem("ExclTaxAmt") = vExclTaxAmt
                    drAddItem("IncTaxAmt") = vIncTaxAmt
                    drAddItem("TotalTaxAmt") = vExclTaxAmt + vIncTaxAmt
                Else
                    drAddItem("ExclTaxAmt") = 0
                    drAddItem("IncTaxAmt") = 0
                End If

                If Not (vExclTaxAmt > 0) And Not (vIncTaxAmt > 0) Then
                    If clsDefaultConfiguration.ArticleTaxAllowed = False Then
                        ShowMessage(getValueByKey("CM019"), "CM019 - " & getValueByKey("CLAE04"))
                        'ShowMessage("Article cannot be billed because there is no tax attached with Article", "Article Information")
                        Exit Function
                    End If
                End If

                drAddItem("NetAmount") = FormatNumber(vTotalNetAmt + vExclTaxAmt, 2)
                'drAddItem("ExpDelDate") = CtrlSalesInfo1.CtrlDtExpDelDate.Value
                drAddItem("Stock") = StockQty
                drAddItem("IsCLP") = True

                drAddItem("ArticleCode") = drItemsRow.Item("ARTICLECODE")
                drAddItem("UOM") = drItemsRow.Item("UOM")
                drAddItem("GrossAmt") = Math.Round(drAddItem("SellingPrice") * drAddItem("Quantity"), 3)

                drAddItem("DeliveredQty") = 0
                drAddItem("ReservedQty") = ReservedQtyAllowed
                drAddItem("CLPPoints") = 0
                drAddItem("CLPDiscount") = 0

                TotalSalesQty = drAddItem("PickUpQty") + drAddItem("DeliveredQty")
                NetArticleRate = drAddItem("NetAmount") / drAddItem("Quantity")
                drAddItem("MinPayAmt") = Math.Round(((drAddItem("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate), 3)

                drAddItem("PromotionId") = 0
                drAddItem("LineDiscount") = 0
                drAddItem("TotalDiscPercentage") = 0
                drAddItem("FirstLevel") = String.Empty
                drAddItem("TopLevel") = String.Empty
                drAddItem("IsStatus") = "Inserted"
                drAddItem("CostAmount") = drItemsRow.Item("CostAmt")
                If Not (drAddItem("NetAmount") = 0.0) Then
                    If drAddItemExists.Length = 0 Then
                        drAddItem("RowIndex") = vRowIndex
                        'Change by Ashish on 29 Nov 2010
                        'Commenting the below line since it was adding rows one below other in the grid
                        'instead of adding the recent scanned item on top of the grid
                        '_dsScan.Tables("ItemScanDetails").Rows.Add(drAddItem)
                        _dsScan.Tables("ItemScanDetails").Rows.InsertAt(drAddItem, 0)
                        'drAddItemPackaging = _dsPackagingVar.Tables("PackagingMaterial").NewRow
                        'drAddItemPackaging.ItemArray = drAddItem.ItemArray
                        '_dsPackagingVar.Tables("PackagingMaterial").Rows.InsertAt(drAddItemPackaging, 0)
                        'end of change


                        SetPackagingBaseVarionInSO(drAddItem)
                        vRowIndex = vRowIndex + 1
                        vPackagingIndex = vPackagingIndex + 1
                        vDeliveryIndex = vDeliveryIndex + 1
                    End If
                Else
                    ShowMessage(getValueByKey("SO004"), "SO004 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Article Tax Details does not Found. ", "Tax Details")
                End If

                _dsScan.AcceptChanges()
            End If
            'grdScanItem.Select(grdScanItem.Rows.Count - 1, 1, True)
            'grdScanItem.Select(1, 1, True)
            'If IsDBNull(drAddItem("BatchBarcode")) = False Then
            '    drAddItem("PickUpQty") = drAddItem("Quantity")
            '    grdScanItem_AfterEdit(grdScanItem, New C1.Win.C1FlexGrid.RowColEventArgs(1, grdScanItem.Cols("PickUpQty").Index))

            '    '_dsScan.AcceptChanges()
            'End If

            If Batchbarcode IsNot Nothing AndAlso Batchbarcode.Select(Function(w) w.EAN = drAddItem("EAN").ToString()).Count() > 0 Then

                If (ArticleScanWithBatchBarcode AndAlso StockQty > drAddItem("PickUpQty")) Then
                    drAddItem("PickUpQty") = Batchbarcode.Where(Function(w) w.EAN = drAddItem("EAN").ToString()).Sum(Function(w) w.Qty)
                ElseIf (ArticleScanWithBatchBarcode AndAlso ReservedQtyAllowed) Then
                    ShowMessage(getValueByKey("SO001"), "SO001 - " & getValueByKey("CLAE04"))
                End If

                grdScanItem_AfterEdit(grdScanItem, New C1.Win.C1FlexGrid.RowColEventArgs(1, grdScanItem.Cols("PickUpQty").Index))
            End If

            ArticleScanWithBatchBarcode = False

        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

        Return True
    End Function

    Private Function SetPackagingBaseVarionInSO(ByVal drItemsRow As DataRow, Optional ByVal leadIndex As Double = 0, Optional ByVal isVaiationAdded As Boolean = False) As Boolean
        Try
            Dim drAddItemPackaging As DataRow
            'If drAddItemExists.Length = 0 Then
            drAddItemPackaging = _dsPackagingVar.Tables("PackagingMaterial").NewRow
            If drItemsRow.Item("Quantity") > 0 Then
                drAddItemPackaging("Quantity") = 1 'drItemsRow.Item("Quantity")
            Else
                drAddItemPackaging("Quantity") = 1
            End If
            Dim x As New DataSet
            x = dsMain
            drAddItemPackaging("PickUpQty") = 0

            'Else
            '    drAddItemPackaging = drAddItemExists(0)

            '    ''If IsEditItem = True Then
            '    ''    drAddItem("Quantity") = CDbl(grdScanItem.Item(grdScanItem.Row, "Quantity"))
            '    ''    IsEditItem = False
            '    ''Else
            '    'prevQty = drAddItem("Quantity")
            '    'drAddItem("Quantity") = drAddItem("Quantity") + 1
            '    'End If
            'End If

            If isVaiationAdded AndAlso leadIndex > 0 Then
                drAddItemPackaging("SrNo") = leadIndex
            Else
                drAddItemPackaging("SrNo") = vRowIndex
            End If
            drAddItemPackaging("SaleOrderNumber") = CtrlTxtOrderNo.Text
            drAddItemPackaging("FinYear") = clsAdmin.Financialyear
            drAddItemPackaging("SiteCode") = clsAdmin.SiteCode
            drAddItemPackaging("PackagingIndex") = vPackagingIndex
            drAddItemPackaging("DeliveryIndex") = vDeliveryIndex
            drAddItemPackaging("ArticleType") = drItemsRow.Item("ArticleType")
            drAddItemPackaging("EAN") = drItemsRow.Item("EAN")
            drAddItemPackaging("DeliverySiteCode") = DeliverySiteCode
            drAddItemPackaging("Discription") = drItemsRow.Item("DISCRIPTION")
            drAddItemPackaging("BatchBarcode") = drItemsRow.Item("BatchBarcode")
            If isVaiationAdded Then
                drAddItemPackaging("IsHeader") = False
            Else
                drAddItemPackaging("IsHeader") = True
            End If
            If drItemsRow.Item("ArticleType") = "Combo" Then
                drAddItemPackaging("IsCombo") = True
            Else
                drAddItemPackaging("IsCombo") = False
            End If






            drAddItemPackaging("Discount") = 0
            drAddItemPackaging("LastNodeCode") = drItemsRow.Item("LastNodeCode").ToString()


            If isVaiationAdded Then
                Dim results As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("EAN='" + drItemsRow.Item("EAN") + "' and RowIndex='" + drItemsRow.Item("RowIndex") + "'  and packagingindex='" + drItemsRow("PackagingIndex").ToString() + "'")
                If results.Length > 0 Then
                    Dim itrQtyRow As DataRow() = dsPackagingVar.Tables(0).Select("EAN='" & drItemsRow.Item("EAN") & "' And RowIndex='" + drItemsRow.Item("RowIndex") + "' ")

                    Dim QtyOthers As Object
                    Dim QtyExcAmt As Object
                    If itrQtyRow.Length > 0 Then
                        QtyOthers = dsPackagingVar.Tables(0).Compute("Sum(Quantity)", "EAN='" & drItemsRow.Item("EAN") & "' And RowIndex='" + drItemsRow.Item("RowIndex") + "' ")

                    End If
                    dvEditTaxVariation = New DataView(dsMain.Tables("SalesOrderTaxDtls"), "EAN='" & drItemsRow.Item("EAN") & "'  and RowIndex='" + drItemsRow.Item("RowIndex") + "'  and packagingindex='" + drItemsRow("PackagingIndex").ToString() + "'", "", DataViewRowState.CurrentRows)


                    For Each drView As DataRowView In dvEditTaxVariation
                        Dim taxVal As Decimal = drView("TaxValue")
                        If QtyOthers > 0 Then
                            taxVal = (taxVal / QtyOthers) * 1
                        End If
                        'drView.Delete()
                        drView("TaxValue") = drView("TaxValue") + taxVal
                    Next

                    'drTax("TaxValue") = drTax("TaxValue") + (Math.Round(results(0)("TaxValue"), 2))  'Math.Round(drRowTax("TaxAmount"), 2)

                End If

            End If



            drAddItemPackaging("ExclTaxAmt") = drItemsRow.Item("ExclTaxAmt") / drItemsRow.Item("Quantity") 'drItemsRow.Item("ExclTaxAmt")

            drAddItemPackaging("IncTaxAmt") = drItemsRow.Item("IncTaxAmt")

            drAddItemPackaging("TotalTaxAmt") = drItemsRow.Item("ExclTaxAmt") / drItemsRow.Item("Quantity")
            drAddItemPackaging("IsImageReq") = 0

            If drItemsRow.Item("ArticleType") = "Combo" Then
                If isVaiationAdded Then
                    drAddItemPackaging("SellingPrice") = drItemsRow.Item("SellingPrice")
                    drAddItemPackaging("NetAmount") = Math.Round((drItemsRow.Item("SellingPrice") * 1) + CDbl(drAddItemPackaging("ExclTaxAmt"))) '' drItemsRow.Item("SellingPrice") * 1 + drAddItemPackaging("ExclTaxAmt")  ''' commented by nikhil
                    drAddItemPackaging("GrossAmt") = drItemsRow.Item("SellingPrice") * 1
                Else
                    drAddItemPackaging("SellingPrice") = 0
                    drAddItemPackaging("NetAmount") = 0
                    drAddItemPackaging("GrossAmt") = 0
                End If

            Else
                drAddItemPackaging("NetAmount") = drItemsRow.Item("SellingPrice") * 1 + drAddItemPackaging("ExclTaxAmt")
                drAddItemPackaging("SellingPrice") = drItemsRow.Item("SellingPrice")
                drAddItemPackaging("GrossAmt") = drItemsRow.Item("SellingPrice") * 1
            End If



            'drAddItem("ExpDelDate") = CtrlSalesInfo1.CtrlDtExpDelDate.Value
            If drItemsRow.Item("Stock") Is DBNull.Value Then
                drAddItemPackaging("Stock") = 0
            Else
                drAddItemPackaging("Stock") = drItemsRow.Item("Stock")
            End If
            If drItemsRow.Item("IsCLP") = True Then
                drAddItemPackaging("IsCLP") = True
            Else
                drAddItemPackaging("IsCLP") = False
            End If

            drAddItemPackaging("PckgSize") = "0.000" 'drItemsRow.Item("PckgSize")
            drAddItemPackaging("PckgQty") = "0.000" 'drItemsRow.Item("PckgQty")
            drAddItemPackaging("PackagingType") = Nothing 'drItemsRow.Item("PackagingType")
            drAddItemPackaging("PackagingMaterial") = drItemsRow.Item("PackagingMaterial")
            drAddItemPackaging("ArticleCode") = drItemsRow.Item("ARTICLECODE")
            drAddItemPackaging("UOM") = drItemsRow.Item("UOM")

            drAddItemPackaging("DeliveredQty") = 0
            drAddItemPackaging("ReservedQty") = drItemsRow.Item("ReservedQty")
            drAddItemPackaging("CLPPoints") = 0
            drAddItemPackaging("CLPDiscount") = 0


            drAddItemPackaging("MinPayAmt") = drItemsRow.Item("MinPayAmt")

            drAddItemPackaging("PromotionId") = 0
            drAddItemPackaging("LineDiscount") = 0
            drAddItemPackaging("TotalDiscPercentage") = 0
            drAddItemPackaging("FirstLevel") = String.Empty
            drAddItemPackaging("TopLevel") = String.Empty
            drAddItemPackaging("IsStatus") = "Inserted"
            drAddItemPackaging("CostAmount") = drItemsRow.Item("CostAmount")

            drAddItemPackaging("RowIndex") = drItemsRow.Item("RowIndex")
            drAddItemPackaging("TaxInPer") = drItemsRow.Item("TaxInPer") 'vipin - for veriation tax in per isnot updating
            _dsPackagingVar.Tables("PackagingMaterial").Rows.InsertAt(drAddItemPackaging, 0)
            _dsPackagingVar.AcceptChanges()
        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Function
    Private Function SetDeliveryBaseVariationInSO(ByVal drItemsRow As DataRow, Optional ByVal leadIndex As Double = 0, Optional ByVal leadDispIndex As String = "", Optional ByVal rowindex As Integer = 0, Optional ByVal isVaiationAdded As Boolean = False) As Boolean
        Try
            Dim drAddItemPackaging As DataRow
            'If drAddItemExists.Length = 0 Then
            drAddItemPackaging = _dsPackagingDelivery.Tables("PackagingDelivery").NewRow
            drAddItemPackaging("Quantity") = 0
            'drAddItemPackaging("PickUpQty") = 0

            'Else
            'drAddItemPackaging = drAddItemExists(0)

            ' ''If IsEditItem = True Then
            ' ''    drAddItem("Quantity") = CDbl(grdScanItem.Item(grdScanItem.Row, "Quantity"))
            ' ''    IsEditItem = False
            ' ''Else
            ''prevQty = drAddItem("Quantity")
            ''drAddItem("Quantity") = drAddItem("Quantity") + 1
            ''End If
            'End If

            drAddItemPackaging("SaleOrderNumber") = CtrlTxtOrderNo.Text
            drAddItemPackaging("SrNo") = leadDispIndex
            drAddItemPackaging("DispSrNo") = leadIndex
            drAddItemPackaging("PackagingIndex") = drItemsRow.Item("PackagingIndex")
            drAddItemPackaging("EAN") = drItemsRow.Item("EAN")
            drAddItemPackaging("finyear") = clsAdmin.Financialyear
            drAddItemPackaging("SiteCode") = drItemsRow.Item("SiteCode")
            drAddItemPackaging("IsCombo") = drItemsRow.Item("IsCombo")
            drAddItemPackaging("ArticleCode") = drItemsRow.Item("ArticleCode")
            drAddItemPackaging("DeliveryIndex") = vDeliveryIndex
            drAddItemPackaging("ArticleType") = drItemsRow.Item("ArticleType")
            drAddItemPackaging("PackagingMaterial") = drItemsRow.Item("PackagingMaterial")
            '-------
            drAddItemPackaging("DeliveryAddress") = drItemsRow.Item("DeliveryAddress").ToString()
            drAddItemPackaging("IsCustAddress") = drItemsRow.Item("IsCustAddress").ToString()
            drAddItemPackaging("PckgSize") = drItemsRow.Item("PckgSize")
            If drItemsRow.Item("IsCombo") Or drItemsRow.Item("UOM") = "NOS" Then
                drAddItemPackaging("PckgQty") = drItemsRow.Item("PckgQty")
            Else
                drAddItemPackaging("PckgQty") = "0 " & drItemsRow.Item("PackagingType")
            End If

            drAddItemPackaging("PackagingType") = drItemsRow.Item("PackagingType")
            '-------
            drAddItemPackaging("Discription") = drItemsRow.Item("DISCRIPTION")
            If rbDPNo.Checked Then
                drAddItemPackaging("DeliveryDate") = ctrlDtDeliveryDate.Value
                drAddItemPackaging("DeliveryTime") = ctrlDtDeliveryDate.Value

            Else
                drAddItemPackaging("DeliveryDate") = DateTime.Now
                drAddItemPackaging("DeliveryTime") = DateTime.Now
            End If
            drAddItemPackaging("PickUpQty") = 0
            drAddItemPackaging("PendingQty") = 0
            'drAddItemPackaging("LastPickDate") = ""
            drAddItemPackaging("IsHeader") = "False"





            drAddItemPackaging("UOM") = drItemsRow.Item("UOM")
            'drAddItemPackaging("GrossAmt") = drItemsRow.Item("GrossAmt").ToString()

            'drAddItemPackaging("DeliveredQty") = 0
            'drAddItemPackaging("ReservedQty") = drItemsRow.Item("ReservedQty").ToString()
            'drAddItemPackaging("CLPPoints") = 0
            'drAddItemPackaging("CLPDiscount") = 0


            'drAddItemPackaging("MinPayAmt") = drItemsRow.Item("MinPayAmt").ToString()

            'drAddItemPackaging("PromotionId") = 0
            'drAddItemPackaging("LineDiscount") = 0
            'drAddItemPackaging("TotalDiscPercentage") = 0
            'drAddItemPackaging("FirstLevel") = String.Empty
            'drAddItemPackaging("TopLevel") = String.Empty
            'drAddItemPackaging("IsStatus") = "Inserted"
            'drAddItemPackaging("CostAmount") = drItemsRow.Item("CostAmount")
            'Dim result As DataRow() = dsPackagingDelivery.Tables(0).Select("RowIndex='" + dr("RowIndex").ToString() + "'  and PackagingIndex ='" + dr("PackagingIndex").ToString() + "'")


            'If dsPackagingDelivery.Tables(0).Rows.Count > 0 Then
            '    If result.Count > 0 Then
            '        Dim c As DataRow = _dsPackagingDelivery.Tables("PackagingDelivery").Select("RowIndex='" + dr("RowIndex").ToString() + "'  and PackagingIndex ='" + dr("PackagingIndex").ToString() + "'")(0)
            '        Dim x As Integer = dsPackagingDelivery.Tables("PackagingDelivery").Rows.IndexOf(c)
            '    End If


            'End If
            drAddItemPackaging("RowIndex") = drItemsRow.Item("RowIndex")
            _dsPackagingDelivery.Tables("PackagingDelivery").Rows.InsertAt(drAddItemPackaging, rowindex)
            _dsPackagingDelivery.AcceptChanges()
        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Function
#End Region

    Private Sub rbBtnAddCombo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal flg As Boolean = False) 'Handles BtnSOSave.Click
        Try
            If clsDefaultConfiguration.customerwisepricemanagement Then 'vipin PC SO Merge 03-05-2018
                If String.IsNullOrEmpty(CustomerNo) Then
                    ShowMessage("Please Select Customer First", "SO014 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
            End If

            If clsDefaultConfiguration.IsSalesPersonApplicable = True AndAlso CtrlSalesPersons.CtrlSalesPersons.SelectedValue = Nothing Then
                ShowMessage(getValueByKey("SO014"), "SO014 - " & getValueByKey("CLAE04"))
                CtrlSalesPersons.CtrlTxtBox.Text = ""
                CtrlSalesPersons.CtrlSalesPersons.Select()
                Exit Sub
            End If

            '.  It will be enabled only when user selects customer.

            'If CtrltxrCust.Text.Trim() = String.Empty Then
            '    ShowMessage(getValueByKey("SO048"), "SO048 - " & getValueByKey("CLAE04"))
            '    CtrltxrCust.Select()
            '    Exit Sub
            'End If
            If tabSalesOrder.SelectedIndex = 1 Then
                tabSalesOrder.SelectedIndex = 0
            End If
            Dim combosrNo As Integer = vRowIndex

            vSrno = IIf(dsScan.Tables(0).Compute("Max(SrNo)", "") Is DBNull.Value, 0, dsScan.Tables(0).Compute("Max(SrNo)", "")) + 1


            Dim objBulkOrderCombo As New frmPCBulkOrderCombo
            objBulkOrderCombo.CardNo = CustomerNo 'vipin PC SO Merge 03-05-2018
            objBulkOrderCombo.DtSoBulkComboHdr = DtSoBulkComboHdr
            objBulkOrderCombo.DtSoBulkComboDtl = DtSoBulkComboDtl
            objBulkOrderCombo.DtSoBulkRemarks = DtSoBulkRemarks
            objBulkOrderCombo.BulkComboMstId = combosrNo
            objBulkOrderCombo.ComboSrNo = combosrNo
            objBulkOrderCombo.displaySrNo = vSrno
            objBulkOrderCombo.SalesOrderNo = CtrlTxtOrderNo.Text
            objBulkOrderCombo.dtPackagingcopiedfrom = dtPackagingcopiedfrom
            'objBulkOrderCombo.displaySrNo = DtSoBulkComboHdr.Rows.Count + 1
            'objBulkOrderCombo.btnPrint.Enabled = False
            'objBulkOrderCombo.btnPrint.Enabled = False

            If objBulkOrderCombo.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If objBulkOrderCombo.IsCombo = False Then
                    If objBulkOrderCombo.SingleArticleCode <> "" Then
                        CtrlSalesPersons.CtrlTxtBox.Text = objBulkOrderCombo.SingleArticleCode
                        Dim foundRow As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("IsDocumentLevel = True")
                        If foundRow.Count > 0 Then
                            ResetDocTax(True, dtDocTaxes)
                            dtDocTaxes.Clear()
                        End If
                        txtSearchItem_Leave(CtrlSalesPersons.CtrlTxtBox, e)

                    End If

                Else

                    DtSoBulkComboHdr = objBulkOrderCombo.DtSoBulkComboHdr
                    DtSoBulkComboDtl = objBulkOrderCombo.DtSoBulkComboDtl
                    'SingleArticleCode = objBulkOrderCombo.SingleArticleCode

                    If DtSoBulkComboHdr.Rows.Count > 0 Then
                        Dim firstRow As DataRow = DtSoBulkComboHdr.Select("ComboSrNo='" + combosrNo.ToString() + "'").FirstOrDefault()
                        Dim foundRow As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("IsDocumentLevel = True")
                        If foundRow.Count > 0 Then
                            ResetDocTax(True, dtDocTaxes)
                            dtDocTaxes.Clear()
                        End If
                        If IsApplyPromotion = True Then
                            rbbtnClrAllPromo_Click(sender, e)
                        End If
                        AddComoInScanItemInSO(firstRow)
                        RefreshScanData(dsScan)
                        'CtrlSalesPersons.CtrlTxtBox.Text = DtSoBulkComboHdr.Rows(DtSoBulkComboHdr.Rows.Count - 1)("PackagingBoxCode")
                        'IsNewComboAdd = True
                        'txtSearchItem_Leave(CtrlSalesPersons.CtrlTxtBox, e)
                        'IsNewComboAdd = False
                    End If
                    'If vRowIndex = combosrNo Then
                    '    '---Record was not added to scanGrid need to delete from combo table as well 
                    '    DeleteBulkCombo(combosrNo)
                    'End If
                End If


                LoadRemarks(objBulkOrderCombo.DtSoBulkRemarks, False)



            End If
        Catch ex As Exception
            LogException(ex)
            IsNewComboAdd = False
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub LoadRemarks(ByVal dt As DataTable, ByVal IsDeleted As Boolean)

        remarkPanel.Controls.Clear()


        If Not IsDeleted Then


            For Each drRemark As DataRow In dt.Rows
                Dim result As DataRow() = DtSoBulkRemarks.Select("SrNo='" + drRemark("SrNo").ToString() + "'")

                If result.Length = 0 Then
                    Dim rowDeliveryAddr = DtSoBulkRemarks.NewRow
                    rowDeliveryAddr("SrNo") = drRemark("SrNo")
                    rowDeliveryAddr("ArticleType") = drRemark("ArticleType")
                    rowDeliveryAddr("ArticleCode") = drRemark("ArticleCode")
                    rowDeliveryAddr("ArticleName") = drRemark("ArticleName")
                    rowDeliveryAddr("Remark") = drRemark("Remark")

                    DtSoBulkRemarks.Rows.InsertAt(rowDeliveryAddr, DtSoBulkRemarks.Rows.Count + 1)
                Else
                    If drRemark("Remark") <> "" Then
                        result(0)("Remark") = drRemark("Remark")
                    Else

                    End If
                End If

                DtSoBulkRemarks.AcceptChanges()
            Next

        End If
        For Each dr As DataRow In DtSoBulkRemarks.Rows
            AddRemarks(dr("ArticleName").ToString(), dr("Remark").ToString())
        Next
        AddBlankTableLayout()

    End Sub
    Dim lb As Label
    Dim lbl1 As Label

    Dim pn As TableLayoutPanel
    Private Sub AddRemarks(ByVal ArticleName As String, ByVal Remark As String)
        Try
            lb = New Label()

            lb.MaximumSize = New Size(0, 0)
            lb.AutoSize = True
            lb.Margin = New Padding(3, 2, 0, 0)
            lb.Name = "Remark"
            lb.Text = ArticleName
            lb.TextAlign = ContentAlignment.TopLeft
            lb.Dock = DockStyle.Fill
            lb.MinimumSize = New Size(130, 15)

            lbl1 = New Label()
            lbl1.MinimumSize = New Size(310, 15)
            lbl1.MaximumSize = New Size(0, 0)
            lbl1.Margin = New Padding(3, 2, 0, 0)
            lbl1.Name = "Remark"
            lbl1.Text = Remark
            lbl1.TextAlign = ContentAlignment.TopLeft
            lbl1.Dock = DockStyle.Fill
            lbl1.AutoSize = True


            pn = New TableLayoutPanel()
            pn.SuspendLayout()
            'pn.MaximumSize = New Size(0, 0)
            pn.Margin = New Padding(0)
            pn.Padding = New Padding(0)
            pn.AutoSize = True
            pn.MaximumSize = New Point(510, 800)
            pn.MaximumSize = New Point(My.Computer.Screen.WorkingArea.Width / 2.68, 800)

            'pn.Height = lbl1.PreferredHeight + 15
            pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
            pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
            pn.RowCount = 1
            pn.ColumnCount = 2
            pn.Controls.Add(lb, 0, 0)
            pn.Controls.Add(lbl1, 1, 0)
            pn.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize))
            pn.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            remarkPanel.Controls.Add(pn)
            remarkPanel.HorizontalScroll.Visible = False
            pn.ResumeLayout()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub AddBlankTableLayout()
        Try

            '''''Do a trick for avoiding conflict at UI ..Showing scroll on adding article remark 
            pn = New TableLayoutPanel()
            pn.SuspendLayout()
            pn.Margin = New Padding(0)
            pn.Padding = New Padding(0)
            pn.Size = New Point(remarkPanel.Size.Width - 20, remarkPanel.Size.Height - 10)
            pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
            pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
            pn.RowCount = 1
            pn.ColumnCount = 2

            remarkPanel.Controls.Add(pn)
            remarkPanel.HorizontalScroll.Visible = False
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function DeleteBulkCombo(combosrNo As Integer) As Boolean
        DeleteBulkCombo = False
        Try
            Dim BulkComboMstId As Int64 = 0
            If DtSoBulkComboHdr.Rows.Count > 0 Then
                Dim drHdr() = DtSoBulkComboHdr.Select("ComboSrNo=" & combosrNo)
                If drHdr.Count > 0 Then
                    'BulkComboMstId = drHdr(0)("BulkComboMstId")
                    For Each row As DataRow In drHdr
                        DtSoBulkComboHdr.Rows.Remove(row)
                    Next
                End If
                Dim drDtl() = DtSoBulkComboDtl.Select("ComboSrNo=" & combosrNo)
                If drDtl.Count > 0 Then
                    For Each row As DataRow In drDtl
                        DtSoBulkComboDtl.Rows.Remove(row)
                    Next
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function


    Private Sub grdScanItem_DoubleClick(sender As Object, e As EventArgs) Handles grdScanItem.DoubleClick
        Try
            If grdScanItem.Row = -2 Then Exit Sub
            If grdScanItem.Item(grdScanItem.Row, "IsHeader") = True Then
                If grdScanItem.Cols(grdScanItem.Col).Name = "DISCRIPTION" Or grdScanItem.Cols(grdScanItem.Col).Name = "ArticleCode" Then
                    Dim SrNo As String = grdScanItem.Item(grdScanItem.Row, "rowindex")
                    Dim displaySrNo As String = grdScanItem.Item(grdScanItem.Row, "SrNo")
                    If grdScanItem.Item(grdScanItem.Row, "ArticleType") = "Single" Then
                        Dim objBulkOrderCombo As New frmPCBulkOrderCombo
                        objBulkOrderCombo.CardNo = CustomerNo 'vipin PC SO Merge 03-05-2018
                        objBulkOrderCombo.ArticleCode = grdScanItem.Item(grdScanItem.Row, "ArticleCode")
                        Dim result As DataRow() = DtSoBulkRemarks.Select("ArticleCode='" + grdScanItem.Item(grdScanItem.Row, "ArticleCode").ToString() + "'")
                        If result.Length = 0 Then
                            objBulkOrderCombo.remark = ""
                        Else
                            objBulkOrderCombo.remark = result(0)("remark")
                        End If
                        objBulkOrderCombo.IsEdit = True
                        objBulkOrderCombo.DtSoBulkRemarks = DtSoBulkRemarks
                        objBulkOrderCombo.IsCombo = False
                        objBulkOrderCombo.SalesOrderNo = CtrlTxtOrderNo.Text
                        objBulkOrderCombo.EditedSrNo = SrNo
                        objBulkOrderCombo.ComboSrNo = vRowIndex
                        If objBulkOrderCombo.ShowDialog() = Windows.Forms.DialogResult.OK Then
                            LoadRemarks(DtSoBulkRemarks, False)
                        End If
                    Else
                        If DtSoBulkComboHdr.Rows.Count > 0 Then

                            Dim objBulkOrderCombo As New frmPCBulkOrderCombo
                            objBulkOrderCombo.CardNo = CustomerNo 'vipin PC SO Merge 03-05-2018
                            objBulkOrderCombo.IsCombo = True
                            objBulkOrderCombo.DtSoBulkComboHdr = DtSoBulkComboHdr.Copy()
                            If DtSoBulkComboDtl.Columns.Contains("GrossAmt") = True Then

                            Else
                                DtSoBulkComboDtl.Columns.Add("GrossAmt", GetType(Decimal))

                            End If
                            For i = 0 To DtSoBulkComboDtl.Rows.Count - 1
                                DtSoBulkComboDtl.Rows(i)("GrossAmt") = CDbl(DtSoBulkComboDtl(i)("Price")) * CDbl(DtSoBulkComboDtl(i)("Qty"))
                            Next
                            If IsCopied = True Then  ''$$ added on 18/08/17
                                objBulkOrderCombo.DtSoBulkComboDtl = DtSoBulkComboDtl.Copy()
                            Else
                                objBulkOrderCombo.DtSoBulkComboDtl = DtSoBulkComboDtl.Select("ComboSrNo ='" & SrNo & "'").CopyToDataTable
                            End If
                            objBulkOrderCombo.DtSoBulkRemarks = DtSoBulkRemarks.Copy()
                            objBulkOrderCombo.SalesOrderNo = CtrlTxtOrderNo.Text
                            'objBulkOrderCombo.BulkComboMstId = BulkComboMstId
                            objBulkOrderCombo.IsEdit = True
                            objBulkOrderCombo.ComboSrNo = vRowIndex
                            objBulkOrderCombo.displaySrNo = displaySrNo
                            objBulkOrderCombo.EditedSrNo = SrNo
                            objBulkOrderCombo.dtPackagingcopiedfrom = dtPackagingcopiedfrom
                            'objBulkOrderCombo.BulkComboMode = enumBulkComboMode.AddDoubleClick
                            'objBulkOrderCombo.CboPakagingBox.Enabled = False
                            'objBulkOrderCombo.btnPrint.Enabled = False
                            '' added by nikhil 
                            If clsDefaultConfiguration.IsNewSalesOrder Then
                                If String.IsNullOrEmpty(CtrllblDiscAmt.Text.ToString.Trim) Or CInt(CtrllblDiscAmt.Text.ToString.Trim) = "0" Then  '##
                                    For Each desc11 In dsScan.Tables(0).Rows
                                        desc11("Discount") = 0
                                    Next
                                End If
                                If IsCopied = True Then  ''$$ added on 18/08/17
                                    objBulkOrderCombo.ArticleComboDtl = dsScan.Tables("ItemScanDetails")
                                Else
                                    objBulkOrderCombo.ArticleComboDtl = dsScan.Tables("ItemScanDetails").Select("SrNo ='" & SrNo & "'").CopyToDataTable
                                End If
                                ' objBulkOrderCombo.lblGross.Text = lblArticleCombo.Rows(0)("GrossAmount")
                                '  objBulkOrderCombo.lblNetValue.Text = Math.Round((CDbl(lblArticleCombo.Rows(0)("GrossAmount")) - CDbl(dsScan.Tables("ItemScanDetails").Rows(0)("Discount"))) + (CDbl(lblArticleCombo.Rows(0)("TaxAmount"))))
                                ''  objBulkOrderCombo.txtTaxAmount.Text = lblArticleCombo.Rows(0)("TaxAmount")

                            End If
                            IsComboDoubleClicked = True
                            If objBulkOrderCombo.ShowDialog() = Windows.Forms.DialogResult.OK Then
                                DtSoBulkComboHdr = objBulkOrderCombo.DtSoBulkComboHdr
                                ' Dim Combo1 As DataTable = DtSoBulkComboDtl.Select("ComboSrNo <>'" + SrNo.ToString() + "'").CopyToDataTable   ''' $$ added by nikhil 
                                'If Combo1.Rows.Count > 0 Then
                                '    For Each dr As DataRow In Combo1.Rows
                                '        Dim row As Object() = dr.ItemArray
                                '        objBulkOrderCombo.DtSoBulkComboDtl.Rows.Add(row)
                                '    Next
                                'End If
                                '' added by ketan 
                                If QtyChange = True And IsComboDoubleClicked = True Then
                                    RemoveApplyPromotion(dsScan, dsPackagingVar)
                                End If
                                Dim Combo1 As New DataTable
                                If DtSoBulkComboDtl.Select("ComboSrNo <>'" + SrNo.ToString() + "'").Length > 0 Then
                                    Combo1 = DtSoBulkComboDtl.Select("ComboSrNo <>'" + SrNo.ToString() + "'").CopyToDataTable   ''' $$ added by nikhil 
                                    If Not Combo1 Is Nothing AndAlso Combo1.Rows.Count > 0 Then
                                        For Each dr As DataRow In Combo1.Rows
                                            Dim row As Object() = dr.ItemArray
                                            objBulkOrderCombo.DtSoBulkComboDtl.Rows.Add(row)
                                        Next
                                    End If
                                End If
                                DtSoBulkComboDtl = objBulkOrderCombo.DtSoBulkComboDtl
                                DtSoBulkRemarks = objBulkOrderCombo.DtSoBulkRemarks
                                Dim result As DataRow() = dsScan.Tables("ItemScanDetails").Select("SrNo='" + SrNo.ToString() + "'")
                                Dim resultPkg As DataRow() = dsPackagingVar.Tables("PackagingMaterial").Select("RowIndex='" + SrNo.ToString() + "'")
                                Dim resultDelviry As DataRow() = _dsPackagingDelivery.Tables("PackagingDelivery").Select("RowIndex='" + SrNo.ToString() + "'")
                                Dim resultHdr As DataRow() = DtSoBulkComboHdr.Select("ComboSrNo='" + SrNo.ToString() + "'")
                                If result.Length > 0 AndAlso resultHdr.Length > 0 Then
                                    result(0)("Discription") = resultHdr(0)("PackagingBoxPrintName")
                                End If

                                '' added by nikhil on 03/07/17
                                If clsDefaultConfiguration.IsNewSalesOrder Then
                                    result(0)("SellingPrice") = lblArticleCombo.Rows(0)("GrossAmount")
                                    If QtyChange = True And IsComboDoubleClicked = True Then '##
                                        result(0)("Discount") = 0

                                        For Each DsScanDelete In dsScan.Tables(0).Rows '#
                                            DsScanDelete("Discount") = 0
                                            DsScanDelete("LineDiscount") = 0
                                        Next
                                        For Each dsPackDekete In dsPackagingVar.Tables(0).Rows
                                            dsPackDekete("Discount") = 0
                                            dsPackDekete("LineDiscount") = 0
                                        Next
                                    End If
                                    If lblArticleCombo.Rows.Count > 0 Then  ''''$$ added by nikhil
                                        Dim CmbTax = ((result(0)("SellingPrice") * result(0)("Quantity")) - result(0)("Discount")) * (CDbl(lblArticleCombo.Rows(0)("Tax") / 100))
                                        If QtyChange = True And IsComboDoubleClicked = True Then '##
                                            result(0)("NetAmount") = ((result(0)("SellingPrice") * result(0)("Quantity")) - result(0)("Discount")) + CmbTax
                                            result(0)("TotalTaxAmt") = Math.Round(CmbTax, 2) 'CmbTax
                                            result(0)("ExclTaxAmt") = result(0)("TotalTaxAmt")
                                            result(0)("TaxInPer") = lblArticleCombo.Rows(0)("Tax") 'added by vipin
                                            result(0)("TaxPer") = Convert.ToDecimal(lblArticleCombo.Rows(0)("Tax")) '' added by ketan GST UI Changes 

                                        Else
                                            result(0)("TotalTaxAmt") = Math.Round(CmbTax, 2)
                                            result(0)("ExclTaxAmt") = result(0)("TotalTaxAmt")
                                            result(0)("NetAmount") = ((result(0)("SellingPrice") * result(0)("Quantity")) - result(0)("Discount")) + CmbTax
                                            result(0)("TaxInPer") = lblArticleCombo.Rows(0)("Tax") 'added by vipin
                                            result(0)("TaxPer") = Convert.ToDecimal(lblArticleCombo.Rows(0)("Tax")) '' added by ketan GST UI Changes 

                                        End If
                                    Else
                                        result(0)("NetAmount") = ((result(0)("SellingPrice") * result(0)("Quantity")) - result(0)("Discount"))
                                    End If
                                    ' result(0)("ArticleLevelTax") = lblArticleCombo.Rows(0)("TaxAmount")
                                    ' result(0)("Discount") = 0
                                    CtrllblGrossAmt.Text = lblArticleCombo.Rows(0)("GrossAmount")
                                    CtrllblNetAmt.Text = lblArticleCombo.Rows(0)("NetAmount")
                                    ' CtrllblDiscAmt.Text = "0"
                                    'CtrllblDiscPerc.Text = "0" & "%"
                                End If
                                If resultPkg.Length > 0 AndAlso resultHdr.Length > 0 Then
                                    ' resultPkg(0)("Discription") = resultHdr(0)("PackagingBoxPrintName")
                                    For Each drr As DataRow In resultPkg
                                        drr("Discription") = resultHdr(0)("PackagingBoxPrintName")
                                        drr("SellingPrice") = lblArticleCombo.Rows(0)("GrossAmount") '##
                                        ' drr("Quantity") = drr("Quantity")
                                        Dim CmbTax = ((drr("SellingPrice") * drr("Quantity")) - drr("Discount")) * (CDbl(lblArticleCombo.Rows(0)("Tax") / 100))
                                        If QtyChange = True And IsComboDoubleClicked = True Then '##

                                            drr("Discount") = 0
                                            drr("NetAmount") = ((drr("SellingPrice") * drr("Quantity")) - drr("Discount")) + CmbTax
                                            drr("TotalTaxAmt") = Math.Round(CmbTax, 2) '' CmbTax
                                            drr("ExclTaxAmt") = drr("TotalTaxAmt")
                                            drr("TaxInPer") = lblArticleCombo.Rows(0)("Tax") 'added by vipin
                                            drr("TaxPer") = Convert.ToDecimal(lblArticleCombo.Rows(0)("Tax")) '' added by ketan GST UI Changes 
                                        Else
                                            drr("TotalTaxAmt") = Math.Round(CmbTax, 2) ''CmbTax
                                            drr("ExclTaxAmt") = drr("TotalTaxAmt")

                                            drr("NetAmount") = ((drr("Quantity") * drr("SellingPrice")) - drr("Discount")) + CmbTax  ''  $$  Added by nik
                                            drr("TaxInPer") = lblArticleCombo.Rows(0)("Tax") 'added by vipin
                                            drr("TaxPer") = Convert.ToDecimal(lblArticleCombo.Rows(0)("Tax")) '' added by ketan GST UI Changes 
                                        End If
                                        '  drr("Discount") = lblArticleCombo.Rows(0)("Discount") '##

                                    Next
                                End If
                                If resultDelviry.Length > 0 AndAlso resultHdr.Length > 0 Then
                                    ' resultDelviry(0)("Discription") = resultHdr(0)("PackagingBoxPrintName")
                                    For Each drr As DataRow In resultDelviry
                                        drr("Discription") = resultHdr(0)("PackagingBoxPrintName")
                                    Next
                                End If
                                '-------Reset pckgsize to 0 when print name changes
                                If clsDefaultConfiguration.PackageFiedlsAllowed Then
                                    Dim resultCombo As DataRow() = dtPackagingPrintBox.Select("KeyValue='" + resultHdr(0)("PackagingBoxPrintName") + "' and isPackagingMandatory=1")
                                    If resultCombo.Length > 0 Then
                                        result(0)("Pckgsize") = 0.0
                                        If resultPkg.Length > 0 Then
                                            'resultPkg(0)("Pckgsize") = 0.0
                                            For Each drr As DataRow In resultPkg
                                                drr("Pckgsize") = 0.0
                                            Next
                                        End If
                                        If resultDelviry.Length > 0 Then
                                            'resultDelviry(0)("Pckgsize") = 0.0
                                            For Each drr As DataRow In resultDelviry
                                                drr("Pckgsize") = 0.0
                                            Next
                                        End If
                                    End If
                                End If
                                dsScan.AcceptChanges()
                                dsPackagingVar.AcceptChanges()
                                _dsPackagingDelivery.AcceptChanges()

                                RefreshScanData(dsScan)
                                GridSetting()
                                AddButtonControlInGrid(1)
                                LoadRemarks(DtSoBulkRemarks, False)
                            End If

                        End If
                    End If
                End If

            End If
            If clsDefaultConfiguration.IsNewSalesOrder Then
                BindTextValue()
            End If
            'Dim TotalAdvmount As Decimal = 0 '##
            'Dim cint1 As Integer = 1
            'For Each de11 In grdScanItem.Rows   '##
            '    If dgDeliveryLocation.Rows.Count > cint1 Then
            '        TotalAdvmount = TotalAdvmount + (((CDbl(grdScanItem.Rows(cint1)("SellingPrice")) - (CDbl(grdScanItem.Rows(cint1)("Discount")) / grdScanItem.Rows(cint1)("Quantity")) + (CDbl(grdScanItem.Rows(cint1)("TotalTaxAmt")) / CDbl(grdScanItem.Rows(cint1)("Quantity")))) * CDbl(dgDeliveryLocation.Rows(cint1)("PickUpQty"))))
            '    End If

            '    '' cint1 = cint1 + 1  '' $$ Commented by nikhil
            'Next
            'CtrllblMinAdv.Text = FormatNumber(TotalAdvmount.ToString.Trim, 0) '##

        Catch ex As Exception
            LogException(ex)
            MsgBox(ex.Message)
        End Try
    End Sub

#Region "Refresh Data Load "

    ''' <summary>
    ''' Calculate Sales Order Summary and Show in Screen
    ''' </summary>
    ''' <param name="dsScanTemp"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CalculateSalesOrderSummary(ByVal dsScanTemp As DataSet) As Boolean
        vmDeliveredAmt = 0.0
        Try
            If Not (dsScan.Tables("ItemScanDetails") Is Nothing) AndAlso dsScan.Tables("ItemScanDetails").Rows.Count > 0 Then

                lblTotalItem.Text = dsScan.Tables("ItemScanDetails").Rows.Count & " Items"

                lblOrderQty.Text = CDbl(dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(Quantity)", ""))
                lblPickupQty.Text = CDbl(IIf(dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(PickUpQty)", "") Is DBNull.Value, 0, dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(PickUpQty)", "")))
                For Each Pck In dsPackagingVar.Tables(0).Rows '##
                    Pck("MinPayAmt") = Math.Round((((Pck("SellingPrice") - (Pck("Discount") / Pck("Quantity")) + (Pck("TotalTaxAmt") / Pck("Quantity"))) * Pck("PickUpQty")))) '' $$ added by nikhil
                    Pck("GrossAmt") = Pck("SellingPrice") * Pck("Quantity")
                Next

                'CtrllblGrossAmt.Text = CDbl(dsScan.Tables("ItemScanDetails").Compute("SUM(GrossAmt)", ""))
                'CtrllblGrossAmt.Text = CDbl(IIf(dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(GrossAmt)", "") Is DBNull.Value, 0, dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(GrossAmt)", "")))
                CtrllblGrossAmt.Text = CDbl(IIf(dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(GrossAmt)", "") Is DBNull.Value, 0, dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(GrossAmt)", "")))
                'CtrlCashSummary1.lbltxt1 = CDbl(dsScan.Tables("ItemScanDetails").Compute("SUM(GrossAmt)", ""))
                '    lblGrossAmt1.Text = CtrlCashSummary1.lbltxt1
                If IsRoundOfflabel Then
                    ctlDispDisc.Text = "Roundoff Amt."
                    ctlDispDisc.Tag = "Roundoff Amt."
                    'IsRoundOffMsg = False
                Else
                    ctlDispDisc.Text = "Disc Amt."
                    ctlDispDisc.Tag = "Disc Amt."
                End If
                Dim vdisAmount As Double = CDbl(IIf(dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(LineDiscount)", "") Is DBNull.Value, 0, dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(LineDiscount)", "")))
                CtrllblDiscAmt.Text = FormatNumber(CDbl(IIf(dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(LineDiscount)", "") Is DBNull.Value, 0, dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(LineDiscount)", ""))), 2)
                If vdisAmount > 0 Then
                    CtrllblDiscPerc.Text = FormatNumber((vdisAmount * 100) / CtrllblGrossAmt.Text, 2)
                Else
                    CtrllblDiscPerc.Text = 0
                End If

                '  CtrllblNetAmt.Text = FormatNumber(CDbl(IIf(dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(NetAmount)", "") Is DBNull.Value, 0, dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(NetAmount)", ""))), 0)
                CtrllblNetAmt.Text = FormatNumber(CDbl(IIf(dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(NetAmount)", "") Is DBNull.Value, 0, dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(NetAmount)", ""))), 0)
                If clsDefaultConfiguration.RoundOffRequired = True Then
                    CtrllblNetAmt.Text = MyRound(CDbl(CtrllblNetAmt.Text), clsDefaultConfiguration.BillRoundOffAt)
                Else
                    CtrllblNetAmt.Text = CDbl(CtrllblNetAmt.Text)
                End If
                If Not (dtOtherCharges Is Nothing) AndAlso dtOtherCharges.Rows.Count > 0 Then
                    Dim vChargeAmount As String = IIf((dtOtherCharges.Compute("Sum(ChargeAmount)", "") Is DBNull.Value), 0, dtOtherCharges.Compute("Sum(ChargeAmount)", ""))
                    ' Dim vTaxAmount As String = IIf((dtOtherCharges.Compute("Sum(TaxAmt)", "") Is DBNull.Value), 0, dtOtherCharges.Compute("Sum(TaxAmt)", ""))
                    ''tax related changes comment by ketan issue  :-SO ISSUE  Pay Popup comming again and again
                    CtrllblOtherCharges.Text = FormatNumber(CDbl(vChargeAmount), 0) '+ CDbl(vTaxAmount), 0)
                Else
                    CtrllblOtherCharges.Text = "0"
                End If

                If Not (CDbl(IIf(CtrllblOtherCharges.Text <> String.Empty, CtrllblOtherCharges.Text, 0)) = 0) Then
                    '-Commented By Prasad TicketId No. GCCCE1600000034
                    ' CtrllblGrossAmt.Text = FormatNumber(CDbl(CtrllblGrossAmt.Text) + CDbl(CtrllblOtherCharges.Text), 0)
                    CtrllblGrossAmt.Text = FormatNumber(CDbl(CtrllblGrossAmt.Text), 0)
                    '-------------------------------------------------
                    CtrllblNetAmt.Text = FormatNumber(CDbl(CtrllblNetAmt.Text) + CDbl(CtrllblOtherCharges.Text), 0)
                    '-------------------------------
                End If
                '-------------------------------SO ISSUE  Pay Popup comming again and again ---------added by ketan
                Dim vTaxAmount As String = Math.Round(IIf(dsMain.Tables("SalesOrderTaxDtls").Compute("SUM(TaxValue)", "IsDocumentLevel=1") Is DBNull.Value, 0, dsMain.Tables("SalesOrderTaxDtls").Compute("SUM(taxValue)", "IsDocumentLevel=1")), 2)
                If Not (CDbl(IIf(vTaxAmount <> String.Empty, vTaxAmount, 0)) = 0) Then

                    CtrllblNetAmt.Text = FormatNumber(CDbl(CtrllblNetAmt.Text) + CDbl(vTaxAmount), 0)
                End If
                '-------------------------------------------------------------------------------------

                Dim VminOtherAdjamt As Double
                vMinAdvancePay = Math.Round(IIf(dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(MinPayAmt)", "") Is DBNull.Value, 0, dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(MinPayAmt)", "")), 2)

                If Not (CDbl(IIf(CtrllblOtherCharges.Text <> String.Empty, CtrllblOtherCharges.Text, 0)) = 0) Then
                    VminOtherAdjamt = CDbl(CtrllblOtherCharges.Text) * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)
                    vMinAdvancePay = vMinAdvancePay + VminOtherAdjamt
                End If

                If (Val(lblOrderQty.Text) = Val(lblPickupQty.Text)) Then
                    CtrllblMinAdv.Text = FormatNumber(CDbl(CtrllblNetAmt.Text), 0) '+ +Math.Round(IIf(dsMain.Tables("SalesOrderTaxDtls").Compute("SUM(TaxValue)", "IsDocumentLevel=1") Is DBNull.Value, 0, dsMain.Tables("SalesOrderTaxDtls").Compute("SUM(taxValue)", "IsDocumentLevel=1")), 2)
                Else
                    CtrllblMinAdv.Text = FormatNumber(vMinAdvancePay, 0)
                End If

                If Not (dsPayment Is Nothing) AndAlso dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                    CtrllblAmtPaid.Text = FormatNumber(CDbl(dsPayment.Tables("MSTRecieptType").Compute("Sum(Amount)", "")), 0)
                    If Not (CDbl(CtrllblNetAmt.Text) > CDbl(CtrllblAmtPaid.Text)) AndAlso CDbl(CtrllblMinAdv.Text) < CDbl(CtrllblAmtPaid.Text) Then
                        CtrllblAmtPaid.Text = FormatNumber(CDbl(CtrllblNetAmt.Text), 0)
                    End If
                    'CtrllblNetAmt.Text = FormatNumber(CDbl(CtrllblNetAmt.Text) - CDbl(CtrllblAmtPaid.Text), 2)
                Else
                    CtrllblAmtPaid.Text = "0"
                    CtrllblAmtPaid.Text = "0"
                End If
                CtrllblNetAmt.Text = FormatNumber(MyRound(CDbl(CtrllblNetAmt.Text), clsDefaultConfiguration.BillRoundOffAt), 0) '+ Math.Round(IIf(dsMain.Tables("SalesOrderTaxDtls").Compute("SUM(TaxValue)", "IsDocumentLevel=1") Is DBNull.Value, 0, dsMain.Tables("SalesOrderTaxDtls").Compute("SUM(taxValue)", "IsDocumentLevel=1")), 2)
                CtrllblNetAmt.Text = MyRound(CtrllblNetAmt.Text, clsDefaultConfiguration.BillRoundOffAt)
                CtrllblMinAdv.Text = MyRound(CtrllblMinAdv.Text, clsDefaultConfiguration.BillRoundOffAt)
                CtrllblTaxAmt.Text = FormatNumber(CDbl(dsPackagingVar.Tables("PackagingMaterial").Compute("Sum(TotalTaxAmt)", "")), 0) + Math.Round(IIf(dsMain.Tables("SalesOrderTaxDtls").Compute("SUM(TaxValue)", "IsDocumentLevel=1") Is DBNull.Value, 0, dsMain.Tables("SalesOrderTaxDtls").Compute("SUM(taxValue)", "IsDocumentLevel=1")), 2)
                CtrllblBaltoPay.Text = MyRound(CtrllblBaltoPay.Text, clsDefaultConfiguration.BillRoundOffAt)
                If CtrllblBaltoPay.Text = String.Empty Then
                    CtrllblBaltoPay.Text = "0"
                End If
                CtrllblNetAmt.Text = CDbl(CtrllblNetAmt.Text)
                CtrllblNetAmt.Text = IIf(CtrllblNetAmt.Text = 0, 0, Val(CtrllblNetAmt.Text).ToString("##,##,##,###", New System.Globalization.CultureInfo("hi-IN")))
                If CtrllblMinAdv.Text = String.Empty Or CtrllblMinAdv.Text = "0" Then
                    CtrllblMinAdv.Text = 0
                Else
                    CtrllblMinAdv.Text = CDbl(CtrllblMinAdv.Text)
                    CtrllblMinAdv.Text = Val(CtrllblMinAdv.Text).ToString("##,##,##,###", New System.Globalization.CultureInfo("hi-IN"))
                End If
                CtrllblAmtPaid.Text = CDbl(CtrllblAmtPaid.Text)
                CtrllblAmtPaid.Text = IIf(CtrllblAmtPaid.Text = 0, 0, Val(CtrllblAmtPaid.Text).ToString("##,##,##,###", New System.Globalization.CultureInfo("hi-IN")))
                CtrllblBaltoPay.Text = CDbl(CtrllblBaltoPay.Text)
                CtrllblBaltoPay.Text = IIf(CtrllblBaltoPay.Text = 0, 0, Val(CtrllblBaltoPay.Text).ToString("##,##,##,###", New System.Globalization.CultureInfo("hi-IN")))
                CtrllblTaxAmt.Text = CDbl(CtrllblTaxAmt.Text)
                CtrllblTaxAmt.Text = IIf(CtrllblTaxAmt.Text = 0, 0, Val(CtrllblTaxAmt.Text).ToString("##,##,##,###", New System.Globalization.CultureInfo("hi-IN")))
                CtrllblGrossAmt.Text = CDbl(CtrllblGrossAmt.Text)
                CtrllblGrossAmt.Text = IIf(CtrllblGrossAmt.Text = 0, 0, Val(CtrllblGrossAmt.Text).ToString("##,##,##,###", New System.Globalization.CultureInfo("hi-IN")))
                CtrllblOtherCharges.Text = CDbl(CtrllblOtherCharges.Text)
                CtrllblOtherCharges.Text = IIf(CtrllblOtherCharges.Text = 0, 0, Val(CtrllblOtherCharges.Text).ToString("##,##,##,###", New System.Globalization.CultureInfo("hi-IN")))
                CtrllblCreditSale.Text = CDbl(CtrllblCreditSale.Text)
                CtrllblCreditSale.Text = IIf(CtrllblCreditSale.Text = 0, 0, Val(CtrllblCreditSale.Text).ToString("##,##,##,###", New System.Globalization.CultureInfo("hi-IN")))
                CtrllblDiscAmt.Text = CDbl(CtrllblDiscAmt.Text)
                'CtrllblDiscAmt.Text = IIf(CtrllblDiscAmt.Text = 0, 0, Val(CtrllblDiscAmt.Text).ToString("##,##,##,###", New System.Globalization.CultureInfo("hi-IN")))
                CtrllblDiscPerc.Text = CDbl(CtrllblDiscPerc.Text)
                ' CtrllblDiscPerc.Text = IIf(CtrllblDiscPerc.Text = 0, 0, Val(CtrllblDiscPerc.Text).ToString("##,##,##,###", New System.Globalization.CultureInfo("hi-IN")))
                CtrllblDiscPerc.Text = IIf(CtrllblDiscPerc.Text = 0, 0, Val(CtrllblDiscPerc.Text)) 'vipul
                CtrllblDiscPerc.Text = CtrllblDiscPerc.Text & "%"

            Else
                CtrllblBaltoPay.Text = "0.00"
                CtrllblTaxAmt.Text = "0.00"
                CtrllblNetAmt.Text = "0.00"
                CtrllblAmtPaid.Text = "0.00"
                CtrllblTaxAmt.Text = "0.00"
                CtrllblMinAdv.Text = "0.00"
                CtrllblDiscAmt.Text = "0.00"
                CtrllblDiscAmt.Text = "0.00"
                CtrllblDiscPerc.Text = "0.00" & "%"
                CtrllblOtherCharges.Text = "0.00"
                lblOrderQty.Text = "0.00"
                lblPickupQty.Text = "0.00"
                CtrllblGrossAmt.Text = "0.00"
            End If


        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Function

    ''' <summary>
    ''' Remove Apply Promotion on Current Sales Order
    ''' </summary>
    ''' <param name="dsScanTemp"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RemoveApplyPromotion(ByRef dsScanTemp As DataSet, Optional ByRef dsPackVar As DataSet = Nothing, Optional ByVal IsMesReq As Boolean = True) As Boolean
        If Not IsMesReq Then
            Exit Function
        End If
        If dsScanTemp.Tables(0).Rows(0)("Discount") <> 0 Then 'vipin
            If MsgBox(getValueByKey("CM064"), MsgBoxStyle.Information, "CM064") = MsgBoxResult.Ok Then

                For Each drRem As DataRow In dsScanTemp.Tables(0).Rows
                    'drRem("GrossAmt") = 0
                    'drRem("MinPayAmt") = 0

                    drRem("Discount") = 0
                    drRem("PromotionId") = 0
                    drRem("LineDiscount") = 0
                    drRem("TotalDiscPercentage") = 0
                    drRem("FirstLevel") = String.Empty
                    drRem("TopLevel") = String.Empty
                    drRem("TopLevelDisc") = 0
                    drRem("ExclTaxAmt") = Math.Round((drRem("sellingPrice") * drRem("quantity")) * IIf(drRem("TaxInPer") Is DBNull.Value, 1, drRem("TaxInPer")) / 100, 2) 'vipin
                    drRem("TotalTaxAmt") = Math.Round((drRem("sellingPrice") * drRem("quantity")) * IIf(drRem("TaxInPer") Is DBNull.Value, 1, drRem("TaxInPer")) / 100, 2) 'vipin
                    drRem("NetAmount") = drRem("sellingPrice") * drRem("quantity") + drRem("ExclTaxAmt")
                    drRem("GrossAmt") = drRem("sellingPrice") * drRem("quantity")
                    Dim obj As New clsSaleOrderCommon
                    obj.IsCSTApplicable = Me.IsCSTApplicable
                    ' obj.RecalculateLine(drRem, CtrlSalesInfo1.CtrlTxtOrderNo.Text, dsMain)
                Next
                For Each drPackvar As DataRow In dsPackVar.Tables(0).Rows
                    'drRem("GrossAmt") = 0
                    'drRem("MinPayAmt") = 0

                    drPackvar("Discount") = 0
                    drPackvar("PromotionId") = 0
                    drPackvar("LineDiscount") = 0
                    drPackvar("TotalDiscPercentage") = 0
                    drPackvar("FirstLevel") = String.Empty
                    drPackvar("TopLevel") = String.Empty
                    drPackvar("TopLevelDisc") = 0
                    drPackvar("ExclTaxAmt") = Math.Round((drPackvar("sellingPrice") * drPackvar("quantity")) * IIf(drPackvar("TaxInPer") Is DBNull.Value, 1, drPackvar("TaxInPer")) / 100, 2) 'vipin
                    drPackvar("TotalTaxAmt") = Math.Round((drPackvar("sellingPrice") * drPackvar("quantity")) * IIf(drPackvar("TaxInPer") Is DBNull.Value, 1, drPackvar("TaxInPer")) / 100, 2) 'vipin
                    drPackvar("NetAmount") = drPackvar("sellingPrice") * drPackvar("quantity") + drPackvar("ExclTaxAmt")
                    'drPackvar("GrossAmount") = drPackvar("sellingPrice") * drPackvar("quantity")
                    drPackvar("GrossAmt") = drPackvar("sellingPrice") * drPackvar("quantity")
                    Dim obj As New clsSaleOrderCommon
                    obj.IsCSTApplicable = Me.IsCSTApplicable
                    ' obj.RecalculateLine(drRem, CtrlSalesInfo1.CtrlTxtOrderNo.Text, dsMain)
                Next
                dsScanTemp.AcceptChanges()
                dsPackVar.AcceptChanges()
                ' IsApplyPromotion = False   $$ commented by nikhil
                IsSelectedPromotion = False
                IsDefaultPromotion = False
                rbnCST.Enabled = True
                IsRoundOffMsg = False
                IsRoundOfflabel = False
            End If
        End If
    End Function

    ''' <summary>
    ''' Refresh Article Scan Data in DataGrid
    ''' </summary>
    ''' <param name="dsScan"></param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function RefreshScanData(ByVal dsScan As DataSet) As Boolean
        Try
            If Not (dsScan Is Nothing) Then

                Dim objSiteInfo As New clsSiteInfo
                Dim dt1 = objSiteInfo.GetAllSitesForDelivery()
                Dim hash As New System.Collections.Hashtable
                For Each row In dt1.Rows
                    hash.Add(row("SiteCode"), row("SiteShortName"))
                Next

                '_dvDisplayItem = New DataView(_dsScan.Tables("ItemScanDetails"), "", "RowIndex Desc", DataViewRowState.CurrentRows)
                Dim dtSource As DataTable = _dsScan.Tables("ItemScanDetails") '_dvDisplayItem.ToTable(False, "DEL", "ArticleCode", "Discription", "SellingPrice", "Quantity", "PickUpQty", "Discount", "NetAmount", "ExpDelDate", "Stock", "IsCLP", "ReservedQty", "EAN")
                If Not dtSource.Columns.Contains("Blankclm") Then
                    AddBlankColumn(dtSource)
                End If
                If clsDefaultConfiguration.PrintItemFullName = True Then
                    Dim objClsCommon As New clsCommon
                    If dtSource.Rows.Count > 0 Then
                        If dtSource.Rows(0)("ArticleCode").ToString().Trim() <> "" AndAlso dtSource.Rows(0)("ArticleType").ToString().Trim() <> "Combo" Then
                            dtSource(0)("DISCRIPTION") = objClsCommon.GetArticleFullName(dtSource.Rows(0)("ArticleCode").ToString())
                        End If

                    End If
                End If
                'If dtSource.Rows.Count > 0 Then '
                BindSOItemGridData(dtSource)
                If dtSource.Rows.Count > 0 Then
                    grdScanItem.Cols("DeliverySiteCode").DataMap = hash

                    grdScanItem.Cols("DeliverySiteCode").AllowEditing = False
                End If
                If clsDefaultConfiguration.IsNewSalesOrder Then  '' added on 24/07/17
                    BindTextValue()
                End If
                ' CalculateSalesOrderSummary(dsScan)
                'grdScanItem.DataSource = dtSource


                'grdScanItem.Cols("ArticleCode").Visible = False
                'grdScanItem.Cols("ExpDelDate").StyleDisplay.Format = DateFormat.ShortDate
                ' grdScanItem.Cols("ExpDelDate").UserData = CtrlSalesInfo1.CtrlDtExpDelDate.Value
                GridSetting()
                If dtSource.Rows.Count > 0 Then
                    AddButtonControlInGrid(1)
                End If

            Else
                'grdScanItem.DataSource = Nothing
            End If
        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function
    Public Sub BindTextValue()
        Try
            Dim total As Double = 0
            Dim totalGross As Double = 0
            Dim TotalAdvmount As Double = 0 '##
            Dim TotDiscount As Double = 0
            Dim obCombo As New clsArticleCombo
            If grdScanItem.Rows.Count > 1 Then
                For i = 1 To grdScanItem.Rows.Count - 1
                    ' Dim dtTax As DataTable = obCombo.getTaxData(grdScanItem.Rows(i)("ArticleCode"), clsAdmin.SiteCode, "SO201")  ''Dim Tax As Double 
                    ' Dim Tax As Double = CDbl(dtTax.Compute("Sum(TaxValue)", ""))
                    'total = total + (((CDbl(grdScanItem.Rows(i)("SellingPrice")) * CDbl(grdScanItem.Rows(i)("Quantity"))) - CDbl(grdScanItem.Rows(i)("Discount"))) * (CDbl(lblArticleCombo.Rows(0)("Tax")) / 100))
                    total = total + Convert.ToDouble(grdScanItem.Rows(i)("TotalTaxAmt"))
                    TotDiscount = TotDiscount + Convert.ToDouble(grdScanItem.Rows(i)("Discount"))
                    totalGross = totalGross + (CDbl(grdScanItem.Rows(i)("SellingPrice")) * CDbl(grdScanItem.Rows(i)("Quantity")))
                    'If dgDeliveryLocation.Rows.Count > i Then '##
                    '    'TotalAdvmount = TotalAdvmount + (CDbl(grdScanItem.Rows(i)("SellingPrice")) - (CDbl(grdScanItem.Rows(i)("Discount")) / grdScanItem.Rows(i)("Quantity")) + (CDbl(grdScanItem.Rows(i)("TotalTaxAmt")) / CDbl(grdScanItem.Rows(i)("Quantity")) * CDbl(dgDeliveryLocation.Rows(i)("PickUpQty")))) '' $$ added by nikhl
                    '    TotalAdvmount = TotalAdvmount + (((CDbl(grdScanItem.Rows(i)("SellingPrice")) - (CDbl(grdScanItem.Rows(i)("Discount")) / grdScanItem.Rows(i)("Quantity")) + (CDbl(grdScanItem.Rows(i)("TotalTaxAmt")) / CDbl(grdScanItem.Rows(i)("Quantity")))) * CDbl(dgDeliveryLocation.Rows(i)("PickUpQty"))))
                    'End If
                Next
            End If
            TotalAdvmount = CDbl(CtrllblMinAdv.Text) 'vipin
            CtrllblTaxAmt.Text = Math.Round(total, 2)
            CtrllblGrossAmt.Text = totalGross.ToString
            If CtrllblDiscAmt.Text = "" Then
                CtrllblDiscAmt.Text = "0"
            End If
            If String.IsNullOrEmpty(CtrllblTaxAmt.Text.ToString.Trim) Then
                CtrllblTaxAmt.Text = "0"
            End If

            If String.IsNullOrEmpty(CtrllblDiscAmt.Text.ToString.Trim) Then
                CtrllblDiscAmt.Text = "0"
            End If

            CtrllblDiscAmt.Text = FormatNumber(TotDiscount, 2) 'Math.Round(TotDiscount)
            'CtrllblDiscAmt.Text = Math.Round(TotDiscount)
            If CDbl(TotDiscount) = 0 Then
                CtrllblDiscPerc.Text = "0%"
            Else
                CtrllblDiscPerc.Text = Math.Round((TotDiscount / CDbl(CtrllblGrossAmt.Text)) * 100, 2) 'vipin
                CtrllblDiscPerc.Text = CtrllblDiscPerc.Text + "%"
            End If
            CtrllblNetAmt.Text = Math.Round(((CDbl(CtrllblGrossAmt.Text) - CDbl(CtrllblDiscAmt.Text)) + CDbl(CtrllblTaxAmt.Text)), MidpointRounding.AwayFromZero)
            '' added by ketan Add other charges in Net amount
            If Not (CDbl(IIf(CtrllblOtherCharges.Text <> String.Empty, CtrllblOtherCharges.Text, 0)) = 0) Then
                CtrllblNetAmt.Text = Math.Round(((CDbl(CtrllblGrossAmt.Text) - CDbl(CtrllblDiscAmt.Text)) + CDbl(CtrllblTaxAmt.Text) + CDbl(CtrllblOtherCharges.Text)))
            End If

            CtrllblMinAdv.Text = FormatNumber(TotalAdvmount, 0) '##
            '' added by ketan Add other charges in Net amount in last Pickup Value
            If (Val(lblOrderQty.Text) = Val(lblPickupQty.Text)) Then
                CtrllblMinAdv.Text = FormatNumber(TotalAdvmount + CDbl(CtrllblOtherCharges.Text), 0)
            Else
                CtrllblMinAdv.Text = FormatNumber(TotalAdvmount, 0)
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub BindSOItemGridData(ByVal dt As DataTable)
        Try
            grdScanItem.SuspendLayout()
            'Dim dataView As New DataView(dt)
            'dataView.Sort = "SrNo ASC"
            'dt = dataView.ToTable()
            'dvRemarks = New DataView(DtSoBulkRemarks, "SrNo='" & ComboSrNo & "' ", "", DataViewRowState.CurrentRows)
            'If dvRemarks.Count > 0 Then
            '    For Each drView2 As DataRowView In dvRemarks
            '        drView2.Delete()
            '    Next
            '    DtSoBulkRemarks.AcceptChanges()
            '    LoadRemarks(DtSoBulkRemarks, True)
            'End If
            grdScanItem.Rows.RemoveRange(1, grdScanItem.Rows.Count - 1)
            dtPackagingcopiedfrom.Rows.Clear()
            If dt.Rows.Count > 0 Then
                dt = dt.Select("", "SrNo").CopyToDataTable()

            End If
            Dim index As Integer = 1
            Dim indexP As Integer = 1
            For Each dr As DataRow In dt.Rows
                grdScanItem.Rows.Add()
                grdScanItem.Rows(indexP)("SrNo") = index
                If DtSoBulkRemarks IsNot Nothing Then
                    If DtSoBulkRemarks.Rows.Count > 0 Then
                        Dim resultRemark As DataRow() = DtSoBulkRemarks.Select("Articletype='Combo' and SrNo='" + dr("Rowindex").ToString() + "'  ")
                        If resultRemark.Length > 0 Then
                            Dim rmkArrary As Array = resultRemark(0)("Articlename").ToString().Split("-")
                            If rmkArrary.Length > 0 Then
                                resultRemark(0)("Articlename") = index.ToString() & "-" & rmkArrary(1)
                            End If
                        End If
                    End If
                End If
                '---bind copy from datatable
                If dr("ArticleType").ToString() = "Combo" Then
                    Dim result As DataRow() = DtSoBulkComboDtl.Select("ComboSrNo='" + dr("Rowindex").ToString() + "' ")
                    'Dim result1 As DataRow() = DtSoBulkComboHdr.Select("ComboSrNo='" + dr("Rowindex").ToString() + "' ")
                    Dim rowDeliveryAddr = dtPackagingcopiedfrom.NewRow()
                    rowDeliveryAddr("ComboSrNo") = dr("Rowindex").ToString()
                    rowDeliveryAddr("PackagingBoxPrintName") = index & "-" & dr("DISCRIPTION").ToString() & " (" & result.Length & ")"
                    dtPackagingcopiedfrom.Rows.Add(rowDeliveryAddr)
                    'grdScanItem.Cols("SellingPrice").AllowEditing = False
                End If

                grdScanItem.Rows(indexP)("ArticleType") = dr("ArticleType").ToString()
                grdScanItem.Rows(indexP)("ArticleCode") = dr("ArticleCode").ToString()
                grdScanItem.Rows(indexP)("DISCRIPTION") = dr("DISCRIPTION").ToString()
                grdScanItem.Rows(indexP)("UOM") = dr("UOM").ToString()
                grdScanItem.Rows(indexP)("Quantity") = dr("Quantity").ToString()
                grdScanItem.Rows(indexP)("SellingPrice") = dr("SellingPrice").ToString()
                If clsDefaultConfiguration.IsNewSalesOrder Then
                    If dr("ArticleType").ToString() = "Combo" Then
                        If lblArticleCombo.Rows.Count > 0 Then
                            If lblArticleCombo.Rows(0)("Discount").ToString.Trim <> "0" Then
                                grdScanItem.Rows(indexP)("Discount") = dr("Discount").ToString() ''''lblArticleCombo.Rows(0)("Discount").ToString
                            ElseIf CInt(DiscAmt.ToString.Trim) = CInt("0") Then
                                grdScanItem.Rows(indexP)("Discount") = "0"
                            Else
                                grdScanItem.Rows(indexP)("Discount") = dr("Discount").ToString()
                                If lblArticleCombo.Rows(0)("Discount").ToString.Trim = "0" Then
                                    grdScanItem.Rows(indexP)("Discount") = dr("Discount").ToString()
                                End If
                            End If
                        End If
                    Else
                        grdScanItem.Rows(indexP)("Discount") = dr("Discount").ToString()

                    End If
                    If QtyChange = True And IsComboDoubleClicked = True Then  'vipin
                        grdScanItem.Rows(indexP)("Discount") = 0
                        dr("Discount") = 0
                    End If
                End If
                If Not String.IsNullOrEmpty(dr("TaxPer").ToString()) Then
                    grdScanItem.Rows(indexP)("TaxPer") = CInt(dr("TaxPer").ToString()) '' addded by ketan GSt UI Chnages
                Else
                    grdScanItem.Rows(indexP)("TaxPer") = 0
                End If

                If Not String.IsNullOrEmpty(dr("TotalTaxAmt").ToString()) Then
                    grdScanItem.Rows(indexP)("TotalTaxAmt") = dr("TotalTaxAmt").ToString()
                Else
                    grdScanItem.Rows(indexP)("TotalTaxAmt") = 0
                End If


                ' grdScanItem.Rows(indexP)("NetAmount") = Math.Round(CDbl(dr("NetAmount")), 2)  '' $$ decimal added by nikhil
                '$vipul
                '     grdScanItem.Rows(indexP)("NetAmount") = Math.Round(CDbl(dr("NetAmount")), 3)
                grdScanItem.Rows(indexP)("NetAmount") = Math.Round((grdScanItem.Rows(indexP)("Quantity") * grdScanItem.Rows(indexP)("SellingPrice")) - grdScanItem.Rows(indexP)("Discount") + grdScanItem.Rows(indexP)("TotalTaxAmt"), 2)
                ' grdScanItem.Rows(indexP)("NetAmount") = (dr("SellingPrice") * dr("Quantity")) - dr("Discount") 'vipin


                If dr("PckgSize").ToString() = "" Then
                    grdScanItem.Rows(indexP)("PckgSize") = 0
                Else
                    grdScanItem.Rows(indexP)("PckgSize") = dr("PckgSize").ToString()
                End If
                If dr("PckgQty").ToString() = "" Then
                    grdScanItem.Rows(indexP)("PckgQty") = 0
                Else
                    grdScanItem.Rows(indexP)("PckgQty") = dr("PckgQty").ToString()
                End If
                ' If clsDefaultConfiguration.ClientForMail = "PC" Then   '' added by nikhil to article wise tax amt

                ' If clsDefaultConfiguration.ClientForMail = "PC" Then   '' added by nikhil to article wise tax amt
                '    Dim obCombo As New clsArticleCombo
                '    Dim Tax As Double = obCombo.getTaxData(dr("ArticleCode").ToString(), clsAdmin.SiteCode)
                '    Tax = CDbl(((dr("SellingPrice").ToString() * dr("Quantity").ToString()) - dr("Discount").ToString()) * Tax / 100)
                '    If dr("ArticleType").ToString() = "Combo" Then
                '        grdScanItem.Rows(indexP)("TotalTaxAmt") = dr("ArticleLevelTax").ToString
                '    Else
                '        grdScanItem.Rows(indexP)("TotalTaxAmt") = Tax.ToString
                '    End If



                'Else
                'grdScanItem.Rows(indexP)("TotalTaxAmt") = dr("TotalTaxAmt").ToString()
                ' End If
                'End If
                ' grdScanItem.Rows(indexP)("PckgQty") = dr("PckgQty").ToString()
                grdScanItem.Rows(indexP)("PackagingType") = dr("PackagingType").ToString()
                grdScanItem.Rows(indexP)("PackagingMaterial") = dr("PackagingMaterial").ToString()
                grdScanItem.Rows(indexP)("PickUpQty") = 0 'dr("PickUpQty").ToString()
                'grdScanItem.Rows(indexP)("TotalTaxAmt") = dr("TotalTaxAmt").ToString()
                grdScanItem.Rows(indexP)("IsCLP") = dr("IsCLP").ToString()
                grdScanItem.Rows(indexP)("ReservedQty") = dr("ReservedQty").ToString()
                grdScanItem.Rows(indexP)("DeliverySiteCode") = dr("DeliverySiteCode").ToString()
                grdScanItem.Rows(indexP)("EAN") = dr("EAN").ToString()
                grdScanItem.Rows(indexP)("RowIndex") = dr("RowIndex").ToString()
                grdScanItem.Rows(indexP)("PackagingIndex") = dr("PackagingIndex").ToString()
                grdScanItem.Rows(indexP)("DeliveryIndex") = dr("DeliveryIndex").ToString()
                grdScanItem.Rows(indexP)("BatchBarcode") = dr("BatchBarcode").ToString()
                grdScanItem.Rows(indexP)("IsImageReq") = "1"
                grdScanItem.Rows(indexP)("IsHeader") = True
                grdScanItem.Rows(indexP)("IsCombo") = dr("IsCombo").ToString()
                grdScanItem.Rows(indexP)("NetAmt") = (grdScanItem.Rows(indexP)("Quantity") * grdScanItem.Rows(indexP)("SellingPrice")) - grdScanItem.Rows(indexP)("Discount")
                indexP = indexP + 1
                Dim dtPackageVar As New DataTable
                If _dsPackagingVar.Tables("PackagingMaterial").Rows.Count > 0 Then
                    dtPackageVar = _dsPackagingVar.Tables("PackagingMaterial").Select("rowindex='" & dr("RowIndex").ToString() & "'").CopyToDataTable()
                End If
                If dtPackageVar.Rows.Count > 1 Then
                    Dim x As Integer = 1
                    For Each drPAckVar As DataRow In dtPackageVar.Rows

                        If drPAckVar("SrNo").ToString().IndexOf(".") = -1 Then
                            'x is an Integer!'
                        Else

                            grdScanItem.Rows.Add()
                            grdScanItem.Rows(indexP)("SrNo") = index & "." & x
                            grdScanItem.Rows(indexP)("ArticleType") = dr("ArticleType").ToString()
                            grdScanItem.Rows(indexP)("ArticleCode") = dr("ArticleCode").ToString()
                            grdScanItem.Rows(indexP)("DISCRIPTION") = dr("DISCRIPTION").ToString()
                            grdScanItem.Rows(indexP)("UOM") = dr("UOM").ToString()
                            grdScanItem.Rows(indexP)("Quantity") = drPAckVar("Quantity").ToString()
                            grdScanItem.Rows(indexP)("SellingPrice") = drPAckVar("SellingPrice").ToString()
                            grdScanItem.Rows(indexP)("Discount") = drPAckVar("Discount").ToString()
                            grdScanItem.Rows(indexP)("UOM") = dr("UOM").ToString()
                            'grdScanItem.Rows(indexP)("NetAmount") = drPAckVar("NetAmount").ToString()
                            grdScanItem.Rows(indexP)("NetAmount") = Math.Round((grdScanItem.Rows(indexP)("Quantity") * grdScanItem.Rows(indexP)("SellingPrice")) - grdScanItem.Rows(indexP)("Discount") + CDbl(drPAckVar("TotalTaxAmt")), 2)
                            grdScanItem.Rows(indexP)("PckgSize") = drPAckVar("PckgSize").ToString()
                            grdScanItem.Rows(indexP)("PckgQty") = drPAckVar("PckgQty").ToString()
                            grdScanItem.Rows(indexP)("PackagingType") = drPAckVar("PackagingType").ToString()
                            grdScanItem.Rows(indexP)("PackagingMaterial") = drPAckVar("PackagingMaterial").ToString()
                            grdScanItem.Rows(indexP)("PickUpQty") = 0
                            grdScanItem.Rows(indexP)("TotalTaxAmt") = drPAckVar("TotalTaxAmt").ToString()
                            grdScanItem.Rows(indexP)("TaxPer") = CInt(dr("TaxPer").ToString()) '' added by ketan GST UI CHANGES

                            grdScanItem.Rows(indexP)("IsCLP") = drPAckVar("IsCLP").ToString()
                            grdScanItem.Rows(indexP)("ReservedQty") = drPAckVar("ReservedQty").ToString()
                            grdScanItem.Rows(indexP)("DeliverySiteCode") = dr("DeliverySiteCode").ToString()
                            grdScanItem.Rows(indexP)("EAN") = drPAckVar("EAN").ToString()
                            grdScanItem.Rows(indexP)("RowIndex") = dr("RowIndex").ToString()
                            grdScanItem.Rows(indexP)("PackagingIndex") = drPAckVar("PackagingIndex").ToString()
                            grdScanItem.Rows(indexP)("DeliveryIndex") = drPAckVar("DeliveryIndex").ToString()
                            grdScanItem.Rows(indexP)("BatchBarcode") = drPAckVar("BatchBarcode").ToString()
                            grdScanItem.Rows(indexP)("IsCombo") = dr("IsCombo").ToString()
                            grdScanItem.Rows(indexP)("IsImageReq") = "0"
                            grdScanItem.Rows(indexP)("NetAmt") = (grdScanItem.Rows(indexP)("Quantity") * grdScanItem.Rows(indexP)("SellingPrice")) - grdScanItem.Rows(indexP)("Discount") 'vipin
                            grdScanItem.Rows(indexP)("IsHeader") = False
                            If QtyChange = True And IsComboDoubleClicked = True Then  '##vipin
                                grdScanItem.Rows(indexP)("Discount") = 0
                                drPAckVar("Discount") = 0
                            End If
                            indexP = indexP + 1
                            x = x + 1
                        End If

                    Next

                End If
                index = index + 1
                'dgTax.Rows((dgTax.Rows.Count - 1))(CType(enumTax.TaxName, Integer)) = item.TaxName.ToString
                'dgTax.Rows((dgTax.Rows.Count - 1))(CType(enumTax.TaxValue, Integer)) = Convert.ToDecimal(item.Value)
                'dgTax.Rows((dgTax.Rows.Count - 1))(CType(enumTax.TaxValueType, Integer)) = item.Type.ToString
            Next

            If QtyChange = True Then   '## vipin
                QtyChange = False
            End If

            grdScanItem.ResumeLayout()
        Catch ex As Exception
            LogException(ex)
        End Try


    End Sub
    Public Sub BindSODeliveryGridData(ByVal dt As DataTable, Optional ByVal isDeleted As Boolean = False)
        Try
            dgDeliveryLocation.SuspendLayout()
            'Dim dataView As New DataView(dt)
            'dataView.Sort = "SrNo ASC"
            'dt = dataView.ToTable()
            If dgDeliveryLocation.Rows.Count > 1 Then
                dgDeliveryLocation.Rows.RemoveRange(1, dgDeliveryLocation.Rows.Count - 1)
            End If

            If dt.Rows.Count > 0 Then
                dt = dt.Select("", "DispSrNo").CopyToDataTable()

            End If
            Dim indexS As Integer = 0
            Dim index As Integer = 1
            Dim indexP As Integer = 1
            ''update display serial no
            Dim isFirstRow = True
            Dim Ean As String
            Dim del As Integer = 1
            For drindex = 0 To dt.Rows.Count - 1


                If dt.Rows(drindex)("SrNo").ToString().IndexOf(".") <> -1 Then
                    If Ean = dt.Rows(drindex)("Ean").ToString() Then
                        Dim s As String() = dt.Rows(drindex)("SrNo").ToString().Split(".")
                        If s.Length > 1 And dt.Rows(drindex)("IsHeader").ToString() = "False" Then
                            If isDeleted Then
                                If s.Length = 2 Then
                                    If Char.IsLetter(s(1).ToString()) = True Then
                                        dt.Rows(drindex)("SrNo") = indexS & "." & Chr(64 + del).ToString()
                                        del = del + 1
                                    Else
                                        dt.Rows(drindex)("SrNo") = indexS & "." & indexP
                                        indexP = indexP + 1
                                    End If

                                End If
                                If s.Length = 3 Then
                                    dt.Rows(drindex)("SrNo") = indexS & "." & s(1).ToString() & "." & Chr(64 + del).ToString()
                                    del = del + 1
                                End If

                            Else
                                If s.Length = 2 Then
                                    If Char.IsLetter(s(1).ToString()) = True Then
                                        dt.Rows(drindex)("SrNo") = indexS & "." & s(1).ToString()
                                    Else
                                        dt.Rows(drindex)("SrNo") = indexS & "." & indexP
                                        indexP = indexP + 1
                                    End If

                                End If
                                If s.Length = 3 Then
                                    dt.Rows(drindex)("SrNo") = indexS & "." & s(1).ToString() & "." & s(2).ToString()
                                End If
                            End If

                        Else

                            dt.Rows(drindex)("SrNo") = indexS & "." & indexP
                            indexP = indexP + 1
                            del = 1
                        End If

                    End If
                Else
                    dt.Rows(drindex)("SrNo") = index
                    Ean = dt.Rows(drindex)("Ean").ToString()
                    index = index + 1
                    indexP = 1
                    indexS = indexS + 1
                    del = 1
                End If
            Next


            dt.AcceptChanges()
            index = 1
            indexP = 1
            For Each dr As DataRow In dt.Rows
                dgDeliveryLocation.Rows.Add()
                dgDeliveryLocation.Rows(indexP)("SrNo") = dr("SrNo")
                dgDeliveryLocation.Rows(indexP)("DispSrNo") = dr("DispSrNo")
                dgDeliveryLocation.Rows(indexP)("ArticleType") = dr("ArticleType").ToString()
                'dgDeliveryLocation.Rows(indexP)("ArticleCode") = dr("ArticleCode").ToString()

                dgDeliveryLocation.Rows(indexP)("DISCRIPTION") = dr("DISCRIPTION").ToString()
                'Dim rmkArrary As Array = dr("DISCRIPTION").ToString().Split(":")
                'If rmkArrary.Length > 0 Then
                '    If dr("IsImageReq") Is DBNull.Value Then
                '        dgDeliveryLocation.Rows(indexP)("DISCRIPTION") = dr("DISCRIPTION").ToString()
                '    Else
                '        If dr("IsImageReq") Then
                '            dgDeliveryLocation.Rows(indexP)("DISCRIPTION") = "Order " & dr("SrNo") & ":" & rmkArrary(1)
                '        Else
                '            dgDeliveryLocation.Rows(indexP)("DISCRIPTION") = dr("DISCRIPTION").ToString()
                '        End If
                '    End If

                'Else
                '    dgDeliveryLocation.Rows(indexP)("DISCRIPTION") = dr("DISCRIPTION").ToString()
                'End If




                dgDeliveryLocation.Rows(indexP)("Quantity") = dr("Quantity").ToString()
                dgDeliveryLocation.Rows(indexP)("UOM") = dr("UOM").ToString()
                'grdScanItem.Rows(indexP)("SellingPrice") = dr("SellingPrice").ToString()
                dgDeliveryLocation.Rows(indexP)("DeliveryDate") = dr("DeliveryDate")
                dgDeliveryLocation.Rows(indexP)("DeliveryTime") = dr("DeliveryTime") ' Now.Date() 'dr("DeliveryTime").ToString()


                Dim resultAddr As DataRow() = dtTempOrderAddresses.Select("AddressKey='" + dr("DeliveryAddress").ToString() + "'  ")
                'If resultAddr.Length > 0 Then
                '    result(0)("IsCustAddress") = "1"
                '    result(0)("DeliveryAddress") = resultAddr(0)("AddressKey") 'dgDeliveryLocation.Item(CurrentRow, "DeliveryAddress")
                'Else
                '    Dim resultAddr1 As DataRow() = dtOrderAddresses.Select("AddressValue='" + dgDeliveryLocation.Item(CurrentRow, "DeliveryAddress").ToString() + "' and addresstype='Address' ")
                '    result(0)("IsCustAddress") = "2"  ''2=cust address,1=store,''=NA
                '    result(0)("DeliveryAddress") = resultAddr1(0)("AddressKey")
                'End If
                If resultAddr.Length > 0 Then

                    dgDeliveryLocation.Rows(indexP)("DeliveryAddress") = resultAddr(0)("AddressValue").ToString()
                Else
                    dgDeliveryLocation.Rows(indexP)("DeliveryAddress") = ""
                End If

                dgDeliveryLocation.Rows(indexP)("PackagingMaterial") = dr("PackagingMaterial").ToString()

                If clsDefaultConfiguration.PackageFiedlsAllowed Then
                    '----
                    dgDeliveryLocation.Rows(indexP)("PckgSize") = dr("PckgSize").ToString()
                    dgDeliveryLocation.Rows(indexP)("PckgQty") = dr("PckgQty").ToString()
                    dgDeliveryLocation.Rows(indexP)("PackagingType") = dr("PackagingType").ToString()
                    '--------------
                    If dr("UOM").ToString() = "KGS" Then
                        dgDeliveryLocation.Rows(indexP)("PickUpQty") = dr("PickUpQty")
                        If dr("PckgSize") > 0 Then
                            dgDeliveryLocation.Rows(indexP)("PickUp") = dr("PickUpQty") / dr("PckgSize")
                        Else
                            dgDeliveryLocation.Rows(indexP)("PickUp") = 0
                        End If
                    Else
                        dgDeliveryLocation.Rows(indexP)("PickUpQty") = dr("PickUpQty").ToString()
                        dgDeliveryLocation.Rows(indexP)("PickUp") = dr("PickUpQty").ToString()
                    End If
                Else
                    dgDeliveryLocation.Rows(indexP)("PickUpQty") = dr("PickUpQty").ToString()
                    dgDeliveryLocation.Rows(indexP)("PickUp") = dr("PickUpQty").ToString()
                End If

                'If dr("PendingQty").ToString() = "0" Then
                '    dgDeliveryLocation.Rows(indexP)("PendingQty") = dr("Quantity").ToString()
                'Else

                dgDeliveryLocation.Rows(indexP)("PendingQty") = dr("Quantity") - dr("PickUpQty")
                'End If

                dgDeliveryLocation.Rows(indexP)("LastPickDate") = dr("LastPickDate").ToString()
                'dgDeliveryLocation.Rows(indexP)("PendingQty") = dr("IsCLP").ToString()
                dgDeliveryLocation.Rows(indexP)("RowIndex") = dr("RowIndex").ToString()
                dgDeliveryLocation.Rows(indexP)("DeliveryIndex") = dr("DeliveryIndex").ToString()
                dgDeliveryLocation.Rows(indexP)("PackagingIndex") = dr("PackagingIndex").ToString()
                dgDeliveryLocation.Rows(indexP)("IsHeader") = dr("IsHeader").ToString()
                dgDeliveryLocation.Rows(indexP)("IsCombo") = dr("IsCombo").ToString()
                dgDeliveryLocation.Rows(indexP)("IsImageReq") = dr("IsImageReq").ToString()

                indexP = indexP + 1



                index = index + 1
                'dgTax.Rows((dgTax.Rows.Count - 1))(CType(enumTax.TaxName, Integer)) = item.TaxName.ToString
                'dgTax.Rows((dgTax.Rows.Count - 1))(CType(enumTax.TaxValue, Integer)) = Convert.ToDecimal(item.Value)
                'dgTax.Rows((dgTax.Rows.Count - 1))(CType(enumTax.TaxValueType, Integer)) = item.Type.ToString
            Next


            dgDeliveryLocation.ResumeLayout()
        Catch ex As Exception
            LogException(ex)
        End Try


    End Sub
    ''' <summary>
    ''' Clears All Resource for new sales order
    ''' </summary>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function ResetSalesOrder() As Boolean

        If Not (dsScan Is Nothing) AndAlso dsScan.Tables("ItemScanDetails").Rows.Count > 0 Then
            dsScan.Clear()
        End If
        If Not (dsPackagingVar Is Nothing) AndAlso dsPackagingVar.Tables("PackagingMaterial").Rows.Count > 0 Then
            dsPackagingVar.Clear()
        End If
        If Not (dsPackagingDelivery Is Nothing) AndAlso dsPackagingDelivery.Tables("PackagingDelivery").Rows.Count > 0 Then
            dsPackagingDelivery.Clear()
        End If
        RefreshScanData(dsScan)
        GridSetting()

        If Not (dsMain Is Nothing) Then
            dsMain.Clear()
        End If
        If Not (DtSoBulkRemarks Is Nothing) Then
            DtSoBulkRemarks.Clear()
        End If
        If Not (DtSOStr Is Nothing) Then
            DtSOStr.Clear()
        End If
        remarkPanel.Controls.Clear()
        If Not (dtDocTaxes Is Nothing) Then
            dtDocTaxes.Clear()
        End If
        If Not dtOtherCharges Is Nothing AndAlso dtOtherCharges.Rows.Count > 0 Then
            dtOtherCharges.Clear()
        End If

        If Not (dsPayment Is Nothing) Then
            dsPayment.Clear()
        End If
        If Not (DtSOStr Is Nothing) Then
            DtSOStr.Clear()
        End If

        vCurrentSODateTime = objComn.GetCurrentDate
        CtrldtOrderDt.Value = vCurrentSODateTime
        If dsPayment.Tables.Contains("CheckDtls") Then
            dsPayment.Tables.Remove("CheckDtls")
        End If

        If dsMain.Tables.Contains("CheckDtls") Then
            dsMain.Tables.Remove("CheckDtls")
        End If

        If dsMain.Tables.Contains("QuotationHdr") Then
            dsMain.Tables.Remove("QuotationHdr")
            ISQuotationConversion = False

        End If
        'If controlList.Count > 0 Then
        '    controlList.Clear()
        'End If
        'CtrlSalesInfo1.CtrldtOrderDt.Value = vCurrentDate
        'CtrlSalesInfo1.CtrlDtExpDelDate.Value = vCurrentDate.AddDays(clsDefaultConfiguration.ChkDeliveryDate)
        'CtrlSalesInfo1.CtrlTxtRemarks.Text = ""

        'CtrlSalesInfo1.CtrlTxtCustOrdRef.Text = ""

        'CtrlProductImage.ClearImage()
        CtrlSalesPersons.CtrlSalesPersons.SelectedIndex = -1
        'CtrlCustDtls.pClear()

        'CtrlSalesInfo1.CtrlTxtInvoice.Text = ""
        lblTotalItem.Text = 0
        lblOrderQty.Text = 0
        lblPickupQty.Text = 0
        'CtrlCashSummary1.lbl5 = "0.00"
        'lblGrossAmt1.Text = strZero

        CtrllblMinAdv.Text = strZero
        CtrllblNetAmt.Text = strZero
        CtrllblAmtPaid.Text = strZero
        CtrllblGrossAmt.Text = strZero
        CtrllblBaltoPay.Text = strZero
        CtrllblTaxAmt.Text = strZero
        CtrllblCreditSale.Text = strZero
        CtrllblOtherCharges.Text = strZero
        CtrllblDiscAmt.Text = strZero
        CtrllblDiscPerc.Text = strZero & "%"
        CtrltxrCust.Text = ""
        'CtrlCustSearch1.rbOtherCust.Checked = True
        'CtrlCustSearch1.rbCLPMember.Checked = True
        'CtrlCustSearch1.CustmType = "CLP"
        'CtrlCashSummary1.CtrlLabel2.Text = "Disc Amt."
        IsRoundOfflabel = False
        vCardType = ""
        CLPCardType = ""
        vClpProgramId = ""
        CLPCustomerId = ""
        vRowIndex = 1
        vDeliveryIndex = 1
        vPackagingIndex = 1
        IsBtnSave = True
        vtaxIndex = 1
        vSalesInvcNo = String.Empty
        TotalPoints = 0
        IsDefaultPromotion = False
        IsOutboundCreated = False
        SoDeliveryInfo = New BindingList(Of SODeliveryLocationInfo)()
        'dgDeliveryLocation.DataSource = SoDeliveryInfo
        DeliverySiteCode = clsAdmin.SiteCode
        Batchbarcode = New List(Of SpectrumCommon.BtachbarcodeInfo)
        'If Not (CtrlCustDtls1.dtCustmInfo Is Nothing) Then
        '    CtrlCustDtls1.dtCustmInfo.Clear()
        'End If
        dtItemScanData = Nothing
        lblCompName.Text = ""
        CtrlLabel18.Text = ""
        CtrltxrCust.Text = ""
    End Function

#End Region

#Region "Save/Update Sales Order "
    Private Function isValidData(Optional ByVal IsDeliveryAddressValid As Boolean = False) As Boolean
        Try
            If Not clsDefaultConfiguration.IsSavoy Then
                For drindex = 0 To dsPackagingVar.Tables(0).Rows.Count - 1
                    If dsPackagingVar.Tables(0).Rows(drindex)("PackagingMaterial").ToString() = "" Then
                        ShowMessage("Please select Packaging material for article", " " & getValueByKey("CLAE04"))
                        Return False

                    End If
                    If clsDefaultConfiguration.PackageFiedlsAllowed Then
                        If dsPackagingVar.Tables(0).Rows(drindex)("ArticleType") = "Single" Then
                            If dsPackagingVar.Tables(0).Rows(drindex)("UOM") = "KGS" Then
                                If dsPackagingVar.Tables(0).Rows(drindex)("PckgSize") = 0 Then
                                    ShowMessage("Please select Packaging Size for article", " " & getValueByKey("CLAE04"))
                                    Return False
                                End If
                                If dsPackagingVar.Tables(0).Rows(drindex)("PckgQty") = 0 Then
                                    ShowMessage("Please select Packaging Quantity for article", " " & getValueByKey("CLAE04"))
                                    Return False
                                End If
                            End If
                        End If

                        If dsPackagingVar.Tables(0).Rows(drindex)("ArticleType") = "Combo" Then
                            '---In case of combo check isPackagingMandatory is true or false ... if true do not validate for pckgsize
                            Dim resultCombo As DataRow() = dtPackagingPrintBox.Select("KeyValue='" + dsPackagingVar.Tables(0).Rows(drindex)("Discription") + "'and isPackagingMandatory=1 ")
                            If resultCombo.Length = 0 Then
                                If dsPackagingVar.Tables(0).Rows(drindex)("PckgSize") = 0 Then
                                    ShowMessage("Please select Packaging Size for article", " " & getValueByKey("CLAE04"))
                                    Return False
                                End If
                            End If
                            If dsPackagingVar.Tables(0).Rows(drindex)("PackagingType").ToString() = "" Then
                                ShowMessage("Please select Packaging Type for article", " " & getValueByKey("CLAE04"))
                                Return False
                            End If

                        End If
                    End If
                Next
            End If
            IsDeliveryAddressValid = True 'vipin
            If Not IsDeliveryAddressValid = True Then
                For Each dr As DataRow In dsPackagingVar.Tables(0).Rows
                    Dim result As DataRow() = dsPackagingDelivery.Tables(0).Select("rowindex='" + dr("rowindex").ToString() + "' and packagingindex='" + dr("packagingindex").ToString() + "' and deliveryindex='" + dr("deliveryindex").ToString() + "'")
                    If result.Length = 0 Then
                        ShowMessage("Please select delivery address ", " " & getValueByKey("CLAE04"))
                        tabSalesOrder.SelectedIndex = 1
                        Exit Function
                    End If
                Next
                For Each dr As DataRow In dsPackagingDelivery.Tables(0).Rows
                    If IIf(dr("deliveryaddress") Is DBNull.Value, Nothing, dr("deliveryaddress")) = "" Then
                        ShowMessage("Please select delivery address ", " " & getValueByKey("CLAE04"))
                        tabSalesOrder.SelectedIndex = 1
                        Exit Function
                    End If
                Next
            End If
            Return True

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    ''' <summary>
    ''' Preapring the Sales Order data for save
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnSaveSalesOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal flg As Boolean = False) 'Handles BtnSOSave.Click
        Try

            ''added by ketan PC delivery date issue 
            CtrlSalesPersons.AndroidSearchTextBox.Focus()
            CtrlSalesPersons.AndroidSearchTextBox.Select()
            If IsBackDelDate = True Then
                IsBackDelDate = False
                Exit Sub
            End If
            If Not isValidData() Then
                Exit Sub
            End If
            isEditLoad = True
            If rbDPNo.Checked Then
                If DateTime.Compare(Convert.ToDateTime(ctrlDtDeliveryDate.Value.Date), Convert.ToDateTime(DateTime.Now.Date)) < 0 Then
                    ShowMessage("Delivery Date Can't be Backdated", "Information")
                    ctrlDtDeliveryDate.Value = DateTime.Now
                    isEditLoad = False
                    Exit Sub
                ElseIf DateTime.Compare(Convert.ToDateTime(ctrlDtDeliveryDate.Value.Date), Convert.ToDateTime(DateTime.Now.Date)) = 0 Then
                    If DateTime.Compare((Convert.ToDateTime(ctrlDtDeliveryDate.Value).ToShortTimeString), Convert.ToDateTime(DateTime.Now).ToShortTimeString) < 0 Then
                        ShowMessage("Delivery Date Can't be Backdated", "Information")
                        ctrlDtDeliveryDate.Value = DateTime.Now
                        isEditLoad = False
                        Exit Sub
                    End If
                End If
            End If
            isEditLoad = False
            grdScanItem.FinishEditing()

            If Not (dsScan.Tables(0).Rows.Count > 0) Then

                ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If

            Dim s As String = CtrllblNetAmt.Text
            If CtrltxrCust.Text.Trim() = String.Empty Then
                ShowMessage(getValueByKey("SO013"), "SO013 - " & getValueByKey("CLAE04"))
                CtrltxrCust.Select()

                Exit Sub
            ElseIf clsDefaultConfiguration.IsSalesPersonApplicable = True AndAlso CtrlSalesPersons.CtrlSalesPersons.SelectedValue = Nothing Then
                ShowMessage(getValueByKey("SO014"), "SO014 - " & getValueByKey("CLAE04"))
                CtrlSalesPersons.CtrlSalesPersons.Select()
                Exit Sub
            End If


            'Comment by Ashish on 25 Nov 2010
            'Adding a check for variable "flg" to enter or skip the "If CDbl(CtrlCashSummary1.lbltxt6) < CDbl(CtrlCashSummary1.lbltxt5) Then" condition
            'This is for allowing saving of SO when override amount = 0
            If flg = False Then
                '********************************************************
                If CDbl(CtrllblAmtPaid.Text) < CDbl(CtrllblMinAdv.Text) Then
                    If Not IsBooking = True Then
                        BtnAcceptPayment_Click(sender, e)
                        Exit Sub
                    End If
                End If
                '********************************************************
            End If
            'End of change
            Dim s1 As String = CtrllblNetAmt.Text


            GetNewSalesOrderNumber()
            If ValidationForSONumber(CtrlTxtOrderNo.Text) = False Then
                'Me.Close()
                Exit Sub
            End If
            'If OnlineConnect = True Then
            '    'Changed by Rohit to generate Document No. for proper sorting
            '    Try
            '        CtrlSalesInfo1.CtrlTxtOrderNo.Text = GenDocNo("SO" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode))
            '    Catch ex As Exception
            '        CtrlSalesInfo1.CtrlTxtOrderNo.Text = "SO" & clsAdmin.TerminalID & objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode)
            '    End Try

            '    Try
            '        rbnLStatus.SmallImage = Spectrum.My.Resources.connected1.ToBitmap
            '        rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus2")
            '    Catch ex As Exception

            '    End Try

            '    'End Change by Rohit
            'Else
            '    'Changed by Rohit to generate Document No. for proper sorting
            '    Try
            '        CtrlSalesInfo1.CtrlTxtOrderNo.Text = GenDocNo("OSO" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode))
            '    Catch ex As Exception
            '        CtrlSalesInfo1.CtrlTxtOrderNo.Text = "OSO" & clsAdmin.TerminalID & objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode)
            '    End Try

            '    Try
            '        rbnLStatus.SmallImage = Spectrum.My.Resources.disconnected1.ToBitmap
            '        rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus1")
            '    Catch ex As Exception

            '    End Try
            '    'End Change by Rohit
            'End If

            '    'CtrlSalesInfo1.CtrlTxtOrderNo.Text = "SO" & clsAdmin.TerminalID & objComn.getDocumentNo("SalesOrder")
            If OnlineConnect = True Then
                If Val(lblOrderQty.Text) = Val(lblPickupQty.Text) Then 'CtrlCustSearch1.CustmType = "CLP" AndAlso
                    CalCulateCLP(vCardType, dsScan.Tables("itemscandetails"), "pickupqty>0")
                End If

                Try
                    rbnLStatus.SmallImage = Spectrum.My.Resources.connected1.ToBitmap
                    rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus2")
                Catch ex As Exception
                    LogException(ex)
                End Try

            Else
                Try
                    rbnLStatus.SmallImage = Spectrum.My.Resources.disconnected1.ToBitmap
                    rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus1")
                Catch ex As Exception
                    LogException(ex)
                End Try
            End If
            If IsBtnSave Then
                rbbtnDefaultPromo_Click(rbbtnDefaultPromo, New System.EventArgs)
                IsBtnSave = False
            End If

            If Not (PrepareHdrDataforSave(dsMain) = True) Then
                Exit Sub
            End If
            If Not (PrepareDtlDataforSave(dsMain) = True) Then
                Exit Sub
            End If
            If Not (PrepareComboHeaderDataforSave(dsMain) = True) Then
                Exit Sub
            End If
            If Not (PrepareComboDetailDataforSave(dsMain) = True) Then
                Exit Sub
            End If
            If Not (PrepareSOSTRtDataforSave(dsMain) = True) Then
                Exit Sub
            End If
            If Not (PreparePackageVariationDataforSave(dsMain) = True) Then
                Exit Sub
            End If
            If Not (PrepareSODiscountDataforSave(dsMain) = True) Then
                Exit Sub
            End If
            If Not (PreparePackageVariationDeliveryDataforSave(dsMain) = True) Then
                Exit Sub
            End If
            If IsBooking Then
                If Not (PreparePackageVariationTempDeliveryDataforSave(dsMain) = True) Then
                    Exit Sub
                End If
            End If

            If Not (PrepareInvcDataforSave(dsMain) = True) Then
                Exit Sub
            End If
            If clsDefaultConfiguration.IsSavoy Then
                If Not (PrepareSaleOrderTermNConditionsSave(dsMain) = True) Then
                    Exit Sub
                End If
            End If
            If Not (PrepareOtherTaxDataforSave(dsMain) = True) Then
                Exit Sub
            End If
            If Not (PreparTaxDataforSave(dsMain) = True) Then
                Exit Sub
            End If
            If Not (PrepareSOTaxDataforSave(dsMain) = True) Then
                Exit Sub
            End If
            If Not (PreparSTRFactoryRemarkforSave(dsMain) = True) Then
                Exit Sub
            End If

            If Not IsBooking Then

                If Val(lblPickupQty.Text) > 0 Then
                    'If clsDefaultConfiguration.SupplierCode = Nothing Then
                    '    ShowMessage(getValueByKey("SO080"), "SO080 - " & getValueByKey("CLAE04"))
                    '    Exit Sub
                    'End If

                    IsOutboundCreated = True
                    If Not (PrepareOrderHdrDataforSave(dsMain) = True) Then
                        Exit Sub
                    End If
                    If Not (PrepareOrderDtlDataforSave(dsMain) = True) Then
                        Exit Sub
                    End If
                    If Not (PrepareOrderVariationDeliveryDataforSave(dsMain) = True) Then
                        Exit Sub
                    End If
                End If
            End If
            dsMain.Tables("SalesOrderTaxDtls").AcceptChanges()
            For Each drTax As DataRow In dsMain.Tables("SalesOrderTaxDtls").Rows
                drTax("SaleOrderNumber") = CtrlTxtOrderNo.Text
            Next

            '   ------------------------------------ vipin ----------------------------------
            Dim TotaltaxAmtForPrint As Decimal = 0
            dsMain.Tables("SalesOrderTaxDtls").Clear()
            Dim TaxLineNo As Integer = 1
            Dim _dsPackagingVarTemp As New DataSet
            _dsPackagingVarTemp = _dsPackagingVar.Copy

            For Each DrUpdateSrNo In _dsPackagingVarTemp.Tables(0).Rows
                DrUpdateSrNo("SrNo") = Math.Round(CDbl(DrUpdateSrNo("SrNo")), 0)
            Next
            For Each DrUpdateTaxDtl In _dsPackagingVarTemp.Tables(0).Select("IsHEader =true")

                '  Dim DtTaxDtl As DataTable = objComn.FunGetTaxDtlForEdit(clsAdmin.SiteCode, DrUpdateTaxDtl("ArticleCode"), DrUpdateTaxDtl("TotalTaxAmt"), DrUpdateTaxDtl("IsCombo"), DrUpdateTaxDtl("PackagingMaterial"))
                Dim DtTaxDtl As DataTable
                If DrUpdateTaxDtl("IsCombo") = True Then
                    Dim Tem123 As DataTable = dsMain.Tables("SalesOrderBulkComboDtl").Select("ComboSrNo = '" & DrUpdateTaxDtl("RowIndex") & "'").CopyToDataTable()
                    Dim dataView As New DataView(Tem123)
                    dataView.Sort = "TAX DESC"
                    Tem123 = dataView.ToTable()
                    If IsCSTApplicable Then
                        DtTaxDtl = objCM.getTax(vSiteCode, String.Empty, "SO201", DrUpdateTaxDtl("Quantity"), DrUpdateTaxDtl("EAN"), clsDefaultConfiguration.CSTTaxCode, True)
                    Else
                        DtTaxDtl = objCM.getTax(vSiteCode, Tem123.Rows(0)("ArticleCode"), "SO201", DrUpdateTaxDtl("Quantity"), DrUpdateTaxDtl("EAN"), clsDefaultConfiguration.CSTTaxCode, False)
                    End If
                Else
                    If IsCSTApplicable Then
                        DtTaxDtl = objCM.getTax(vSiteCode, String.Empty, "SO201", DrUpdateTaxDtl("Quantity"), DrUpdateTaxDtl("EAN"), clsDefaultConfiguration.CSTTaxCode, True)
                    Else
                        DtTaxDtl = objCM.getTax(vSiteCode, DrUpdateTaxDtl.Item("ArticleCode"), "SO201", DrUpdateTaxDtl("Quantity"), DrUpdateTaxDtl("EAN"), clsDefaultConfiguration.CSTTaxCode, False)
                    End If
                End If
                'Dim DtIsVariationPresent As DataRow() = _dsPackagingVarTemp.Tables(0).Select("EAN = '" + DrUpdateTaxDtl("EAN").ToString + "' and ArticleCOde = '" + DrUpdateTaxDtl("ArticleCOde") + "' AND IsHEader =  False ")
                Dim DtIsVariationPresent As DataRow() = _dsPackagingVarTemp.Tables(0).Select("SRNO = '" + DrUpdateTaxDtl("Srno") + "' AND IsHEader =  False ")
                TotaltaxAmtForPrint = 0
                If DtIsVariationPresent.Length > 0 Then
                    ' DrUpdateTaxDtl("TotalTaxAmt") = _dsPackagingVarTemp.Tables(0).Compute("SUM(TotalTaxAmt)", "EAN = '" + DrUpdateTaxDtl("EAN").ToString + "' and ArticleCOde = '" + DrUpdateTaxDtl("ArticleCOde") + "'")
                    TotaltaxAmtForPrint = _dsPackagingVarTemp.Tables(0).Compute("SUM(TotalTaxAmt)", "SRNO = '" + DrUpdateTaxDtl("Srno") + "'")
                Else
                    TotaltaxAmtForPrint = DrUpdateTaxDtl("TotalTaxAmt")
                End If
                If Not DtTaxDtl Is Nothing AndAlso DtTaxDtl.Rows.Count > 0 Then
                    For Each dtTaxdtlTemp In DtTaxDtl.Rows
                        Dim DrTaxDtlRow As DataRow = dsMain.Tables("SalesOrderTaxDtls").NewRow
                        DrTaxDtlRow("SiteCode") = clsAdmin.SiteCode
                        DrTaxDtlRow("FinYear") = clsAdmin.Financialyear
                        DrTaxDtlRow("SaleOrderNumber") = CtrlTxtOrderNo.Text
                        DrTaxDtlRow("EAN") = DrUpdateTaxDtl("EAN")
                        '  DrTaxDtlRow("TaxLineNo") = dtTaxdtlTemp("Stepno")
                        DrTaxDtlRow("TaxLineNo") = TaxLineNo 'dtTaxdtlTemp("Stepno")
                        DrTaxDtlRow("Taxlabel") = dtTaxdtlTemp("TaxCode")
                        DrTaxDtlRow("TaxValue") = Math.Round(TotaltaxAmtForPrint / 2, 2)
                        DrTaxDtlRow("CREATEDAT") = clsAdmin.SiteCode
                        DrTaxDtlRow("CREATEDBY") = clsAdmin.UserCode
                        DrTaxDtlRow("CREATEDON") = DateTime.Now
                        DrTaxDtlRow("UPDATEDAT") = clsAdmin.SiteCode
                        DrTaxDtlRow("UPDATEDON") = DateTime.Now
                        DrTaxDtlRow("UPDATEDBY") = clsAdmin.UserCode
                        DrTaxDtlRow("Status") = True
                        DrTaxDtlRow("DocumentType") = "SalesOrder"
                        DrTaxDtlRow("IsDocumentLevel") = False
                        DrTaxDtlRow("RowIndex") = DrUpdateTaxDtl("RowIndex")
                        DrTaxDtlRow("PackagingIndex") = DrUpdateTaxDtl("PackagingIndex")
                        DrTaxDtlRow("IsHeader") = DrUpdateTaxDtl("IsHeader")
                        dsMain.Tables("SalesOrderTaxDtls").Rows.Add(DrTaxDtlRow)
                        TaxLineNo = TaxLineNo + 1
                    Next
                End If

            Next
            dsMain.Tables("SalesOrderTaxDtls").AcceptChanges()

            For Each dt As DataTable In dsMain.Tables
                If dt.TableName.ToUpper() <> "QuotationHdr".ToUpper() Then
                    For Each dr As DataRow In dt.Rows
                        dr.AcceptChanges()
                        dr.SetAdded()
                    Next
                End If

            Next
            dtSalesOrderTaxDetails = dsMain.Tables("SalesOrderTaxDtls").Copy()
            Dim totalPaidAmt As Decimal
            If Not (dsPayment Is Nothing) AndAlso dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                For Each drPayment As DataRow In dsPayment.Tables("MSTRecieptType").Rows
                    totalPaidAmt += drPayment("Amount")
                Next
                RecalculateDeliveryAmt(CDbl(CtrllblNetAmt.Text), totalPaidAmt)
            End If
            '    For Each item In SoDeliveryInfo
            '        If item.Quantity IsNot Nothing Then
            '            item.ReservedQuantity = item.Quantity
            '        End If
            '    Next

            If DtSoBulkComboHdr.Rows.Count > 0 Then
                For index = 0 To DtSoBulkComboHdr.Rows.Count - 1
                    DtSoBulkComboHdr(index)("saleordernumber") = CtrlTxtOrderNo.Text

                    'dim billno = (from x in dsmain.tables("salesorderdtl")
                    '               where x("rowindex") = dtsobulkcombohdr(index)("combosrno")
                    '               select x("billlineno")).firstordefault()

                    Dim billno = dsMain.Tables("salesorderdtl").Select("rowindex=" & DtSoBulkComboHdr(index)("combosrno"))(0)("billlineno")
                    If Not billno Is Nothing Then
                        DtSoBulkComboHdr(index)("combosrno") = billno
                    End If
                Next

                objPCSO.DtSoBulkComboHdr = DtSoBulkComboHdr
                objPCSO.DtSoBulkComboDtl = DtSoBulkComboDtl
            End If

            dsMain.Tables("SalesOrderDTL").Columns.Remove("RowIndex")
            '    '--- for SO Generate
            objPCSO.IsStrGenerate = IsSTRGenerate

            Dim SOStatus As String
            SOStatus = dsMain.Tables("SalesOrderhDR").Rows(0)("SOstatus")

            If objPCSO.PrepareSaveData(vSalesInvcNo, clsAdmin.DayOpenDate, clsAdmin.CLPProgram, CLPCustomerId, dsMain, True, True, vSiteCode, CtrlTxtOrderNo.Text, clsDefaultConfiguration.StockStorageLocation, clsAdmin.CVProgram, "SalesOrder", clsAdmin.Financialyear, clsAdmin.UserCode, clsAdmin.CurrentDate, IsOutboundCreated, CVoucherNo, CVVoucherDay, IsApplyPromotion, Nothing, SoDeliveryInfo.ToList(), clsDefaultConfiguration.IsBatchManagementReq, Batchbarcode, dtStrResult:=dtStrResult) = True Then

                Dim vSalesNo As String = CtrlTxtOrderNo.Text
                Dim dtSiteInfo As DataTable = objComn.GetSiteInfo(clsAdmin.SiteCode)
                _pickupHistory = objPCSO.GetSalesOrderPickupHistory(vSiteCode, IIf(vSalesNo = String.Empty, 0, vSalesNo))
                dtStrPrint = objPCSO.GetSalesOrderSTRPrint(clsAdmin.SiteCode, IIf(vSalesNo = String.Empty, 0, vSalesNo))
                SOStrDetails()
                GetSOPrintAddress()
                SOPrintRemarks()
                SOPrintDeliveryDetails()
                SOPrintPaymentDetails()
                SOPrintOrderDetails()
                SOPrintOrderComboDetails()
                SOPrintHeader(dtSiteInfo, _dtCustmInfo) 'ketan 
                DtCustDtlForSOPrint = objComn.GetCustDetailForSoPrint(_dtCustmInfo.Rows(0)("CustomerNo").ToString())
                DtComboGridData = objComn.GetComboDtlForSoPrint(lblSalesOrderNo.Text)

                SOPrintPaymentDetails1()
                SOPrintPickupHistory()
                soprintarticlepaymentwisedetails()
                SOPrintReturnOrderComboDetails()
                '-----Added By Prasad for Invoice and Delivery Challan of Savoy Client
                If clsDefaultConfiguration.IsSavoy Then
                    SOPrintInvoiceChallanHeader(dtSiteInfo, _dtCustmInfo)
                    SOPrintInvoiceChallan()
                End If

                '---------------------------------------------------------------------
                BarCodestring = ImageToBase64(CtrlTxtOrderNo.Text)
                If IsBooking = True Then
                    Dim sitename As String = objComn.GetSiteName(clsAdmin.SiteCode)
                    ''Change by ketan check So booking Print required  flag is true or not 
                    'Dim objprint As New SpectrumPrint.clsPrintDenomination(clsDefaultConfiguration.SOPrintPreviewAllowed)
                    Dim objprint As New SpectrumPrint.clsPrintDenomination(clsDefaultConfiguration.PrintPreviewRequiredForSOBooking)
                    objprint.PrintSOBarcode(sitename, CtrlTxtOrderNo.Text, CtrltxrCust.Text, DateTime.Now.ToString("g"), clsDefaultConfiguration.ClientName, dtPrinterInfo)
                    '----printInvoice
                    GenerateSOPrint()
                    ActivityLogForShift(Nothing, "Print generated", "")


                Else
                    Dim pckupQty As Double = 0
                    If dsPackagingVar IsNot Nothing Then
                        If dsPackagingVar.Tables(0).Rows.Count > 0 Then
                            pckupQty = CDbl(dsPackagingVar.Tables(0).Compute("Sum(PickUpQty)", ""))
                            '-----Added By Prasad for Invoice and Delivery Challan of Savoy Client
                            If clsDefaultConfiguration.IsSavoy = True Then
                                If pckupQty > 0 Then
                                    GenerateSoInvoiceChallanPrint()
                                    GenerateSoDeliveryChallanPrint()
                                End If
                                '------------------------------------------------------------------
                            Else
                                If clsCommon.checkDiscSpce(clsDefaultConfiguration.DayCloseReportPath.Substring(0, 3)) = False Then
                                    ShowMessage("Insufficient disk space for saving files", "")
                                End If
                                GenerateSOPrint()
                                'GenerateOrderPreparationPrint()
                                GenerateOrderPreparationAsPerDeliveryDetails(dtDeliveryDetails)
                                If pckupQty > 0 Then
                                    GenerateSoDeliveryPrint()
                                End If
                            End If
                        End If
                    End If



                    '        'Apply Customer loyalty Point
                    '        If OnlineConnect = True Then
                    '            If CtrlCustSearch1.CustmType = "CLP" AndAlso Val(lblOrderQty.Text) = Val(lblPickupQty.Text) Then
                    '                If dsMainCLP.Tables.Count = 0 Then
                    '                    dsMainCLP.Tables.Add(dsMain.Tables("CLPTransaction").Clone())
                    '                    dsMainCLP.Tables.Add(dsMain.Tables("CLPTransactionsDetails").Clone)
                    '                End If

                    '                dsMainCLP.Clear()
                    '                TotalPoints = CDbl(dsScan.Tables("ItemScanDetails").Compute("Sum(CLPPoints)", "PickUpQty>0"))

                    '                If TotalPoints > 0 AndAlso PrepareClpHdrDataforSave(dsMainCLP) = True AndAlso PrepareClpDtlDataforSave(dsMainCLP) = True Then
                    '                    If objPCSO.PrepareSaveClpData(dsMainCLP, vClpProgramId, CtrlCustSearch1.CtrlTxtCustNo.Text, TotalPoints, vSiteCode, CtrlSalesInfo1.CtrlTxtOrderNo.Text) = False Then

                    '                        ShowMessage(getValueByKey("SO018"), "SO018 - " & getValueByKey("CLAE04"))
                    '                        'ShowMessage("CLP Data is not Saved....", "Information")
                    '                    End If
                    '                End If

                    '                dsMainCLP.Clear()
                    '            End If

                    '            Try
                    '                rbnLStatus.SmallImage = Spectrum.My.Resources.connected1.ToBitmap
                    '                rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus2")
                    '            Catch ex As Exception

                    '            End Try
                    '        Else
                    '            Try
                    '                rbnLStatus.SmallImage = Spectrum.My.Resources.disconnected1.ToBitmap
                    '                rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus1")
                    '            Catch ex As Exception

                    '            End Try
                    '        End If


                    '        _drSiteInfo = objComn.GetSiteInfo(vSiteCode).Rows(0)

                    '        'If CtrlCustDtls1.dtCustmInfo.Rows.Count > 2 Then
                    '        '    drHomeAdds = CtrlCustDtls1.dtCustmInfo.Select("AddressType='1'")(0)
                    '        'Else
                    '        '    drHomeAdds = CtrlCustDtls1.dtCustmInfo.Rows(0)
                    '        'End If
                    '        'If Not IsDBNull(CtrlCustDtls1.cboAddrType.SelectedValue) AndAlso (CtrlCustDtls1.cboAddrType.SelectedValue.ToString() <> String.Empty) Then
                    '        '    ' If Not (CtrlCustDtls1.cboAddrType.SelectedValue Is DBNull.Value) AndAlso Not (CtrlCustDtls1.cboAddrType.SelectedValue = 99) Then
                    '        '    drDelvAdds = CtrlCustDtls1.dtCustmInfo.Select("AddressType='" & CtrlCustDtls1.cboAddrType.SelectedValue & "' ")(0)
                    '        'Else
                    '        '    drDelvAdds = CtrlCustDtls1.dtCustmInfo.Rows(0)
                    '        'End If


                    '        'Print Sales Order Information.-----------------------------------
                    '        PrintSalesOrders(drSiteInfo, drHomeAdds, drDelvAdds, SOStatus)
                    '        Dim totalPay As Double
                    '        'Print Sales Invoice Information----------------------------------
                    '        If Not (dsPayment.Tables("MSTRecieptType") Is Nothing) AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                    '            '---- Commented By Mahesh Not required as we are showing on bill itself disscussed with B.A. Manish
                    '            ' PrintSalesOrdersInvoice(drSiteInfo, drHomeAdds, drDelvAdds)

                    '            For Each dr As DataRow In dsPayment.Tables("MSTRecieptType").Select("RecieptTypeCode='CreditVouc(I)'", "", DataViewRowState.CurrentRows)
                    '                totalPay = IIf(dr("Amount") > 0, dr("Amount"), dr("Amount") * -1)

                    '                'PrintCreditVoucher(drSiteInfo, totalPay)
                    '                clsVoucher.PrintGiftVoucherAndCreditNote("SalesOrder", clsAdmin.SiteCode, "CreditNote", String.Empty, totalPay, String.Empty, clsAdmin.UserName, CtrlSalesInfo1.CtrlTxtOrderNo.Text, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                    '            Next
                    '            For Each dr As DataRow In dsPayment.Tables("MSTRecieptType").Select("RecieptTypeCode='GiftVoucher(I)'", "", DataViewRowState.CurrentRows)
                    '                totalPay = IIf(dr("Amount") > 0, dr("Amount"), dr("Amount") * -1)

                    '                'PrintCreditVoucher(drSiteInfo, totalPay)
                    '                clsVoucher.PrintGiftVoucherAndCreditNote("SalesOrder", clsAdmin.SiteCode, "GiftVoucher", String.Empty, totalPay, String.Empty, clsAdmin.UserName, CtrlSalesInfo1.CtrlTxtOrderNo.Text, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                    '            Next
                    '        End If

                    '        '---- Commented By Mahesh Not required as we are showing on bill itself disscussed with B.A. Manish
                    '        'Print Sales Delivery Information---------------------------------
                    '        If CDbl(lblPickupQty.Text) > 0 Then
                    '            PrintSalesOrdersDelivery(drSiteInfo, drHomeAdds, drDelvAdds)
                    '        End If

                    '        If IsGiftVoucher Then
                    '            PrintGiftReceipt()
                    '        End If

                    'ShowMessage(getValueByKey("SO019"), "SO019 - " & getValueByKey("CLAE04"))
                    IsSOSaved = True
                    '        'ShowMessage("Sales Order Created", "Sales Order")
                    '        If clsDefaultConfiguration.AskForMoreCustomerInfo Then
                    '            'Dim missingInfo As String = CustomerMissingInfoFinder.Instance.FindMissingParameter(CtrlCustSearch1.dtCustmInfo)
                    '            'If Not String.IsNullOrEmpty(missingInfo) Then
                    '            '    Dim EventType As Int32
                    '            '    ShowMessage("Please ask " & missingInfo & " from customer", getValueByKey("CLAE04"), EventType, True, getValueByKey("mod009"))
                    '            '    If EventType = 1 Then
                    '            '        Dim objCreateNewCustm As New frmNSOCustomer
                    '            '        objCreateNewCustm.Tag = String.Empty
                    '            '        objCreateNewCustm.pSearchCust = "SEARCH"
                    '            '        objCreateNewCustm._CustomerNoToSearch = CtrlCustSearch1.CtrlTxtCustNo.Text
                    '            '        objCreateNewCustm.ShowDialog()
                    '            '    End If
                    '            'End If
                    '        End If
                End If
                If clsDefaultConfiguration.IsSavoy = False Then
                    If _dtCustmInfo.Rows.Count > 0 Then
                        '----Email
                        If _dtCustmInfo(0)("EmailId") IsNot DBNull.Value And _dtCustmInfo(0)("EmailId") <> "-" Then
                            ActivityLogForShift(Nothing, "mail is sending", "")
                            'SendMail(path, _dtCustmInfo(0)("Email Address")) 'vipin Mantis 2879
                            SendMail(StrTaxInvoicePath, _dtCustmInfo(0)("EmailId"))
                            ActivityLogForShift(Nothing, "mail is sent", "")
                        End If
                        '----SMS
                        If _dtCustmInfo(0)("MobileNo") IsNot DBNull.Value Then
                            Dim msg = SMSTemplate()
                            Dim mobileno = _dtCustmInfo(0)("MobileNo")
                            'Dim urlString = clsDefaultConfiguration.SMSUrl
                            'Dim urlContent = clsDefaultConfiguration.SMSUrlParameters
                            'Dim url = urlString & urlContent
                            ActivityLogForShift(Nothing, "message is sending", "")
                            'SendSMSForPCSO(url, _dtCustmInfo(0)("MobileNo"), msg)

                            Dim IpAddr, port As String
                            Dim objs As New clsCommon
                            Dim dtInfo As New DataTable
                            dtInfo = objs.GetRemoteIpPort()
                            If dtInfo IsNot Nothing Then
                                ActivityLogForShift(Nothing, "Checking google.com as internet connected", "")
                                Try
                                    Using client = New WebClient()
                                        Using stream = client.OpenRead("http://www.google.com")
                                        End Using
                                    End Using

                                    ActivityLogForShift(Nothing, "Checking google.com as internet connected sucess", "")
                                    If dtInfo.Rows.Count > 0 Then
                                        For Each row In dtInfo.Rows
                                            If row("FldLabel").ToString() = "WebService.Remote.IP" Then
                                                IpAddr = row("FldValue").ToString()
                                            ElseIf row("FldLabel").ToString() = "WebService.Remote.PORT" Then
                                                port = row("FldValue").ToString()
                                            End If
                                        Next
                                        'IpAddr = "10.10.180.181"
                                        'port = "10080"
                                        Dim client As New WebClient()
                                        'client.Headers("Content-type") = "application/json"
                                        '-- invoke the REST method
                                        If clsDefaultConfiguration._so_sms_applicable.ToUpper = "YES" Then
                                            Dim data As Byte()
                                            Try
                                                data = client.DownloadData("http://" & IpAddr & ":" & port & "/posSeam/rest/sendProcess/sendSMS?number=" & mobileno & "&message=" & msg)
                                                ActivityLogForShift(Nothing, "message web servive call successful", "")
                                            Catch ex As Exception
                                                LogException(ex)
                                                ActivityLogForShift(Nothing, "message web servive call unsuccessful", "")
                                            End Try
                                        End If
                                    End If
                                Catch ex As Exception
                                    LogException(ex)
                                    ShowMessage("Weak or No Internet Connection,Message can not be sent", getValueByKey("CLAE04"))
                                End Try

                            End If
                        End If
                    End If
                End If
                If IsBooking = False Then
                    ShowMessage(getValueByKey("SO019"), "SO019 - " & getValueByKey("CLAE04"))
                Else
                    ShowMessage("Sales Order Booked", "SO019 - " & getValueByKey("CLAE04"))
                End If
                ActivityLogForShift(Nothing, "Reset", "")
                ResetSalesOrder()
                BtnSONew_Click(sender, e)
                '        'CtrlSalesInfo1.CtrlTxtOrderNo.Text = CtrlSalesInfo1.CtrlTxtOrderNo.Text + 1
                EnableButton(True)
                AutoLogout(FrmTranCode, Me, lblLoggedIn)
            Else
                If IsBooking = True Then
                    ShowMessage("Sales Order is not Booked", "SO019 - " & getValueByKey("CLAE04"))
                Else
                    ShowMessage(getValueByKey("SO020"), "SO020 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Sales Order is not created", "Sales Order")
                End If

            End If
        Catch ex As Exception
            LogException(ex)
            ShowMessage(getValueByKey("SO020"), "SO020 - " & getValueByKey("CLAE04"))
        End Try
    End Sub
    Dim NEWdtDeliveryDetails As DataTable
    Dim NEWdtOrderComboDetails As DataTable
    Dim NewdtRemark As DataTable
    Private Function GenerateOrderPreparationAsPerDeliveryDetails(ByVal dtDeliveryDetails As DataTable)
        Try
            ' Dim TEMPdtDeliveryDetails = dtDeliveryDetails
            Dim DVdtOrderComboDetails As New DataView(dtOrderComboDetails)
            Dim NewdvRemark As New DataView(dtRemark)
            NewdtRemark = dtRemark.Copy
            Dim dvDeliveryDate As New DataView(dtDeliveryDetails)
            Dim dvDeliveryDateNEW As New DataView(dtDeliveryDetails)
            NEWdtOrderComboDetails = dtOrderComboDetails.Copy
            NEWdtDeliveryDetails = dtDeliveryDetails
            dvDeliveryDate = dvDeliveryDate.ToTable(True, "DeliveryTime", "DeliveryAddress").DefaultView
            dvDeliveryDate.Sort = "DeliveryTime ASC"
            '' SO Preparation Print call from here for Consolidated purpose 
            GenerateOrderPreparationPrint(True)

            If dvDeliveryDate.Count > 1 Then
                Dim i As Integer = 1
                For Each dr As DataRowView In dvDeliveryDate
                    dvDeliveryDateNEW.RowFilter = "DeliveryAddress='" & dr("DeliveryAddress").ToString & "' AND DeliveryTime='" & dr("DeliveryTime").ToString & "'"

                    'dvDeliveryDateNEW.RowFilter = "DeliveryTime='" & dr("DeliveryTime").ToString & "'"
                    NEWdtOrderComboDetails.Rows.Clear()
                    NEWdtDeliveryDetails = dvDeliveryDateNEW.ToTable()
                    Dim datatable As DataTable
                    For Each drCombo As DataRowView In dvDeliveryDateNEW
                        DVdtOrderComboDetails.RowFilter = "SrNo='" & drCombo("SrNo").ToString & "'"
                        datatable = DVdtOrderComboDetails.ToTable
                        For Each drPrintCombo As DataRow In datatable.Rows
                            NEWdtOrderComboDetails.ImportRow(drPrintCombo)
                        Next
                    Next
                    If Not dtRemark Is Nothing AndAlso dtRemark.Rows.Count > 0 Then
                        NewdtRemark.Rows.Clear()
                        For Each drRemark As DataRowView In dvDeliveryDateNEW
                            NewdvRemark.RowFilter = "BillLineNo='" & drRemark("BillLineNo").ToString & "'"
                            datatable = NewdvRemark.ToTable
                            For Each drPrintRemark As DataRow In datatable.Rows
                                NewdtRemark.ImportRow(drPrintRemark)
                            Next
                        Next

                    End If
                    '' added by ketan Combo variation detail artical add in print 
                    objPCSO.SOPrintComboVariationDetails(dtOrderComboDetails, NEWdtOrderComboDetails)
                    ''SO Preparation Print call from here for Individual bifurcations  
                    GenerateOrderPreparationPrint(False, i)
                    i = i + 1
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Dim drSOPrintHeader As DataRow
    Dim DtCustDtlForSOPrint As DataTable ' vipin
    Dim DtComboGridData As DataTable 'vipin
    Dim dtHeaderDetails As DataTable
    Dim dtHeaderDetailsChallan As DataTable
    Dim dtOrderDetails As DataTable
    Dim dtOrderDetailsChallan As DataTable
    Dim dtOrderComboDetails As DataTable
    Dim dtPaymentDetails1 As DataTable
    Dim dtPaymentDetails As DataTable
    Dim dtDeliveryDetails As DataTable
    Dim dtRemark As DataTable
    Dim dtAddress As DataTable
    Dim dtStrDetails As DataTable
    Dim dtStrPrint As DataTable
    Dim dtPickupHistory As DataTable
    Dim dtArticleWisePaymentDetails As DataTable 'vipin GST TAx changes
    Dim path As String = ""
    Private Function SOPrintHeader(ByRef dtSiteInfo As DataTable, ByRef dtCustDetail As DataTable) As Boolean 'by ketan 
        Try
            Dim deliverydate As String
            dtHeaderDetails = objSoPc.GetSOPrintHeaderTableStruc()
            If dtSiteInfo.Rows.Count > 0 And dtCustDetail.Rows.Count > 0 Then
                dtHeaderDetails.Rows.Clear()
                drSOPrintHeader = dtHeaderDetails.NewRow()
                drSOPrintHeader("CompanyName") = clsDefaultConfiguration.ClientName
                drSOPrintHeader("SiteName") = dtSiteInfo.Rows(0)("SiteOfficialName")
                drSOPrintHeader("Address") = dtSiteInfo.Rows(0)("SiteAddressLn1") + "," + dtSiteInfo.Rows(0)("SiteAddressLn2") + " " + dtSiteInfo.Rows(0)("SiteAddressLn3")
                drSOPrintHeader("City") = dtSiteInfo.Rows(0)("CityCode")
                drSOPrintHeader("PinCode") = dtSiteInfo.Rows(0)("SitePinCode")
                drSOPrintHeader("Contact") = dtSiteInfo.Rows(0)("SiteTelephone1")
                drSOPrintHeader("CustomerName") = dtCustDetail.Rows(0)("CUSTOMERNAME")
                drSOPrintHeader("CustomerCompanyName") = dtCustDetail.Rows(0)("CompanyName")
                drSOPrintHeader("CustomerCellNo") = dtCustDetail.Rows(0)("MOBILENO")
                drSOPrintHeader("CustomerDept") = dtCustDetail.Rows(0)("DepartMent")
                drSOPrintHeader("OtherContacts") = objPCSO.GetClpContactDetails(dtSiteInfo.Rows(0)("SiteCode"), dtCustDetail.Rows(0)("CUSTOMERNO"))
                drSOPrintHeader("EmailId") = dtCustDetail.Rows(0)("Email Address")
                drSOPrintHeader("SalesOrderNo") = CtrlTxtOrderNo.Text
                drSOPrintHeader("OrderDate") = CtrldtOrderDt.Value
                If rdDelYes.Checked Then
                    drSOPrintHeader("DeliveryRequired") = "Yes"
                Else
                    drSOPrintHeader("DeliveryRequired") = "No"
                End If
                For i = 0 To dsMain.Tables("SOPackagingBoxDeliveryDtl").Rows.Count - 1
                    If dsMain.Tables("SOPackagingBoxDeliveryDtl").Rows(i)("STATUS") = True Then
                        If Convert.ToDateTime(dsMain.Tables("SOPackagingBoxDeliveryDtl").Rows(0)("DeliveryTime")) = Convert.ToDateTime(dsMain.Tables("SOPackagingBoxDeliveryDtl").Rows(i)("DeliveryTime")) Then
                            deliverydate = dsMain.Tables("SOPackagingBoxDeliveryDtl").Rows(0)("DeliveryTime")
                            deliverydate = Convert.ToDateTime(deliverydate).ToString("dd-MM-yyyy HH:mm ")
                        Else
                            deliverydate = "Multiple"
                            Exit For
                        End If
                    End If

                Next
                drSOPrintHeader("DeliveryDate") = deliverydate
                drSOPrintHeader("FooterMassage") = "This is computer generated invoice"
                drSOPrintHeader("GeneratedDate") = DateTime.Now
                drSOPrintHeader("TillNo") = clsAdmin.TerminalID
                drSOPrintHeader("BookedBy") = IIf(CtrlSalesPersons.CtrlSalesPersons.SelectedIndex < 0, clsAdmin.UserName, CtrlSalesPersons.CtrlSalesPersons.Text)
                drSOPrintHeader("CHSN") = dtCustDetail.Rows(0)("CHSN")
                drSOPrintHeader("LocalSalesTaxNo") = dtSiteInfo.Rows(0)("LocalSalesTaxNo")
                dtHeaderDetails.Rows.Add(drSOPrintHeader)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Dim dtReturnOrderComboDtl As DataTable '' added by ketan SO return chnages 
    Public Function SOPrintReturnOrderComboDetails() As DataTable
        Try
            dtReturnOrderComboDtl = objSoPc.GetSOPrintOrderDetailsTableStruc()
            dtReturnOrderComboDtl.Rows.Clear()
            Return dtReturnOrderComboDtl
        Catch ex As Exception
        End Try
    End Function
    Private Function SOPrintOrderDetails() As Boolean 'by ketan 
        Try

            dtOrderDetails = objSoPc.GetSOPrintOrderDetailsTableStruc()
            dtOrderDetails.Rows.Clear()
            Dim i As Integer = 1
            If dsMain.Tables("SOPackagingBoxDeliveryDtl").Rows.Count > 0 Then
                For Each dr As DataRow In dsMain.Tables("SOPackagingBoxDeliveryDtl").Select("", "BillLineNo")
                    Dim drResult As DataRow() = dsMain.Tables("SOitemPackagingBoxDtl").Select("pkgLineNo='" + dr("pkgLineNo").ToString + "'")
                    drSOPrintHeader = dtOrderDetails.NewRow()
                    drSOPrintHeader("BillLineNo") = dr("BillLineNo")
                    Dim drSrNo As DataRow() = dtOrderDetails.Select("BillLineNo='" + dr("BillLineNo").ToString + "'")
                    If drSrNo.Length = 0 Then
                        drSOPrintHeader("SrNo") = i
                    Else
                        i = i - 1
                        drSOPrintHeader("SrNo") = i & "." & drSrNo.Length
                    End If
                    'drSOPrintHeader("ArticleDescription") = clsCommon.GetArticleFullName(dr("ArticleCode")).ToString()
                    Dim discription = dsMain.Tables("SalesOrderBulkComboHdr").Select("ComboSrNo='" & dr("BillLineNo") & "'")
                    If discription.Length > 0 Then
                        drSOPrintHeader("ArticleDescription") = discription(0)("PackagingBoxName")
                    Else
                        drSOPrintHeader("ArticleDescription") = clsCommon.GetArticleFullName(dr("ArticleCode")).ToString()
                    End If
                    drSOPrintHeader("Quantity") = dr("Quantity")
                    drSOPrintHeader("UnitofMeasure") = dr("UnitOfMeasure")
                    drSOPrintHeader("Price") = drResult(0)("SellingPrice")
                    drSOPrintHeader("Total") = drResult(0)("SellingPrice") * dr("Quantity")
                    'drSOPrintHeader("PckgSize") = drResult(0)("PckgSize")
                    If clsDefaultConfiguration.PackageFiedlsAllowed = True AndAlso discription.Length > 0 Then
                        Dim resultCombo As DataRow() = dtPackagingPrintBox.Select("KeyValue='" + discription(0)("PackagingBoxName") + "' and isPackagingMandatory=1")
                        If resultCombo.Length > 0 Then
                            drSOPrintHeader("Pckgsize") = 0
                        Else
                            drSOPrintHeader("Pckgsize") = drResult(0)("Pckgsize")
                        End If
                    Else
                        drSOPrintHeader("Pckgsize") = drResult(0)("Pckgsize")
                    End If
                    If drResult(0)("PckgSize") = 0 Then
                        drSOPrintHeader("PckgQty") = 0
                    Else
                        drSOPrintHeader("PckgQty") = dr("Quantity") / drResult(0)("PckgSize")
                        If clsDefaultConfiguration.PackageFiedlsAllowed = True AndAlso drResult(0)("UnitOfMeasure") = "NOS" AndAlso drResult(0)("IsCombo") = True Then
                            drSOPrintHeader("PckgQty") = 0
                        End If
                    End If
                    drSOPrintHeader("PckgType") = drResult(0)("PckgType")
                    If IsBooking Then
                        Dim dr1 As DataRow() = dsMain.Tables("SOPackagingBoxDeliveryTempDtl").Select("pkglineNo='" + dr("pkglineNo").ToString + "' and deliverylineNo='" + dr("deliverylineNo").ToString + "'")
                        If dr1.Length > 0 Then
                            drSOPrintHeader("PickupQuantity") = dr1(0)("PickupQty")
                        End If
                    Else
                        drSOPrintHeader("PickupQuantity") = dr("DeliveredQty")
                    End If
                    drSOPrintHeader("IsCombo") = dr("IsCombo")
                    drSOPrintHeader("header") = 1
                    drSOPrintHeader("PackagingMaterial") = drResult(0)("pckgMaterial")
                    dtOrderDetails.Rows.Add(drSOPrintHeader)
                    i = i + 1
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    Private Function SOPrintOrderComboDetails() As Boolean 'by ketan 
        Try

            dtOrderComboDetails = objSoPc.GetSOPrintOrderDetailsTableStruc()
            dtOrderComboDetails.Rows.Clear()
            Dim i As Long = 1
            'dtOrderComboDetails.Columns.Add("HSN", GetType(String))
            If dsMain.Tables("SalesOrderBulkComboDtl").Rows.Count > 0 Then
                Dim DtComboDtlForSoPrint As DataTable = objComn.GetComboDtlForSoPrint(dsMain.Tables("SalesOrderBulkComboDtl").Rows(0)("SaleOrderNumber").ToString())
                'For Each dr As DataRow In dsMain.Tables("SalesOrderBulkComboDtl").Select("Status=True", "ComboSrNo")
                '    Dim drResult As DataRow() = dsMain.Tables("SOPackagingBoxDeliveryDtl").Select("BillLineNo='" + dr("ComboSrNo").ToString + "'")
                '    drSOPrintHeader = dtOrderComboDetails.NewRow()
                '    drSOPrintHeader("BillLineNo") = dr("ComboSrNo")
                '    Dim drSrNo As DataRow() = dtOrderComboDetails.Select("BillLineNo='" + dr("ComboSrNo").ToString + "'")
                '    If drSrNo.Length = 0 Then
                '        i = 1
                '        drSOPrintHeader("SrNo") = Chr(i + 96)
                '    Else
                '        'i = i - 1
                '        drSOPrintHeader("SrNo") = Chr(i + 96)
                '    End If
                '    drSOPrintHeader("ArticleDescription") = dr("ArticleDescription")
                '    drSOPrintHeader("Quantity") = dr("QTy")
                '    drSOPrintHeader("UnitofMeasure") = dr("PackagedUOM")
                '    drSOPrintHeader("Price") = 0
                '    drSOPrintHeader("Total") = 0
                '    drSOPrintHeader("PckgSize") = 0
                '    drSOPrintHeader("PckgQty") = 0
                '    drSOPrintHeader("PckgType") = 0
                '    drSOPrintHeader("PickupQuantity") = drResult(0)("DeliveredQty")
                '    drSOPrintHeader("IsCombo") = 1
                '    drSOPrintHeader("header") = 0
                '    drSOPrintHeader("HSN") = objComn.GetHSNbyArticle(dr("ArticleCode"))    'vipin 27/07/2017 GST
                '    dtOrderComboDetails.Rows.Add(drSOPrintHeader)
                '    i = i + 1
                'Next


                For Each dr As DataRow In DtComboDtlForSoPrint.Rows
                    drSOPrintHeader = dtOrderComboDetails.NewRow()
                    drSOPrintHeader("SrNo") = dr("ComboSrNo")
                    drSOPrintHeader("BillLineNo") = dr("ComboSrNo")
                    drSOPrintHeader("ArticleDescription") = dr("Combo1")
                    drSOPrintHeader("PckgType") = dr("Combo2")
                    drSOPrintHeader("PackagingMaterial") = dr("Combo3")
                    drSOPrintHeader("HSN") = dr("Combo4")

                    drSOPrintHeader("Price") = 0
                    drSOPrintHeader("Total") = 0
                    drSOPrintHeader("PckgSize") = 0
                    drSOPrintHeader("PckgQty") = 0
                    drSOPrintHeader("IsCombo") = 1
                    drSOPrintHeader("header") = 0

                    '     drSOPrintHeader("HSN") = objComn.GetHSNbyArticle(dr("ArticleCode"))    'vipin 27/07/2017 GST
                    dtOrderComboDetails.Rows.Add(drSOPrintHeader)
                    i = i + 1
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
        dtOrderComboDetails.Merge(dtOrderDetails)
        If dtOrderComboDetails.Rows.Count > 0 Then
            dtOrderComboDetails = dtOrderComboDetails.Select("", "BillLineNo").CopyToDataTable()
        End If
        dtOrderComboDetails.AcceptChanges()
    End Function
    Private Function SOPrintPaymentDetails1() As Boolean 'by ketan 
        Try
            Dim otherCharges As Double
            If dsMain.Tables("SalesOrderOtherCharges").Rows.Count() > 0 Then
                otherCharges = dsMain.Tables("SalesOrderOtherCharges").Compute("Sum(ChargeAmount)", "")
            End If
            dtPaymentDetails1 = objSoPc.GetSOPrintPaymenytDetails1TableStruc()
            dtPaymentDetails1.Rows.Clear()
            If dsMain.Tables("SalesOrderHdr").Rows.Count > 0 Then
                'otherCharges = dsMain.Tables("SalesOrderOtherCharges").Compute("Sum(ChargeAmount)", "")
                For Each dr As DataRow In dsMain.Tables("SalesOrderHdr").Rows
                    drSOPrintHeader = dtPaymentDetails1.NewRow()
                    drSOPrintHeader("GrossAmt") = dr("GrossAmt")
                    drSOPrintHeader("Discount") = dr("TotalDiscount")
                    drSOPrintHeader("Tax") = dr("TaxAmount")
                    drSOPrintHeader("OtherCharges") = otherCharges
                    drSOPrintHeader("NetAmt") = dr("NetAmt")
                    drSOPrintHeader("paidAmt") = dr("AdvanceAmt") - IIf(dsMain.Tables("Salesinvoice").Compute("Sum(AmountTendered)", "Tendertypecode='Credit'") Is DBNull.Value, 0, dsMain.Tables("Salesinvoice").Compute("Sum(AmountTendered)", "Tendertypecode='Credit'"))
                    drSOPrintHeader("BalAmt") = dr("NetAmt") - dr("AdvanceAmt") + IIf(dsMain.Tables("Salesinvoice").Compute("Sum(AmountTendered)", "Tendertypecode='Credit'") Is DBNull.Value, 0, dsMain.Tables("Salesinvoice").Compute("Sum(AmountTendered)", "Tendertypecode='Credit'"))
                    drSOPrintHeader("ReturnAmt") = 0
                    dtPaymentDetails1.Rows.Add(drSOPrintHeader)
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Private Function SOPrintPaymentDetails() As Boolean 'by ketan 
        Try

            dtPaymentDetails = objSoPc.GetSOPrintPaymenytDetailsTableStruc()
            dtPaymentDetails.Rows.Clear()
            If dsMain.Tables("SalesInvoice").Rows.Count > 0 Then
                For Each dr As DataRow In dsMain.Tables("SalesInvoice").Rows
                    drSOPrintHeader = dtPaymentDetails.NewRow()
                    drSOPrintHeader("Shift") = ""
                    drSOPrintHeader("Till") = dr("TerminalID")
                    drSOPrintHeader("CashierName") = clsAdmin.UserName
                    drSOPrintHeader("InvoiceNo") = dr("SaleInvNumber")
                    drSOPrintHeader("PymentDateAndTime") = dr("SOInvTime")
                    drSOPrintHeader("Tender") = dr("TenderTypeCode")
                    drSOPrintHeader("Amt") = dr("AmountTendered")
                    dtPaymentDetails.Rows.Add(drSOPrintHeader)
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Private Function SOPrintDeliveryDetails() As Boolean 'by ketan 
        Try

            dtDeliveryDetails = objSoPc.GetSOPrintDeliveryDetailsTableStruc()
            dtDeliveryDetails.Rows.Clear()
            Dim i As Long = 1
            If dsMain.Tables("SOItemPackagingBoxDtl").Rows.Count > 0 Then
                For Each dr As DataRow In dsMain.Tables("SOPackagingBoxDeliveryDtl").Select("Status =True", "BillLineNo")
                    'Dim drResult As DataRow() = dsMain.Tables("SOPackagingBoxDeliveryDtl").Select("pkglineNo='" + dr("pkglineNo").ToString + "'")
                    drSOPrintHeader = dtDeliveryDetails.NewRow()
                    Dim discription = dsMain.Tables("SalesOrderBulkComboHdr").Select("ComboSrNo='" & dr("BillLineNo") & "'")
                    If discription.Length > 0 Then
                        drSOPrintHeader("Orders") = discription(0)("PackagingBoxName")
                    Else
                        drSOPrintHeader("Orders") = clsCommon.GetArticleFullName(dr("ArticleCode")).ToString
                    End If
                    drSOPrintHeader("Orderqty") = dr("Quantity")
                    Dim drResult As DataRow() = dsMain.Tables("SOItemPackagingBoxDtl").Select("pkglineNo='" + dr("pkglineNo").ToString + "'")
                    drSOPrintHeader("BillLineNo") = dr("BillLineNo")
                    Dim drSrNo As DataRow() = dtDeliveryDetails.Select("BillLineNo='" + dr("BillLineNo").ToString + "'")
                    If drSrNo.Length = 0 Then
                        drSOPrintHeader("SrNo") = i
                    Else
                        i = i - 1
                        drSOPrintHeader("SrNo") = i & "." & drSrNo.Length
                    End If
                    'drSOPrintHeader("Pckgsize") = drResult(0)("Pckgsize")
                    If clsDefaultConfiguration.PackageFiedlsAllowed = True AndAlso discription.Length > 0 Then
                        Dim resultCombo As DataRow() = dtPackagingPrintBox.Select("KeyValue='" + discription(0)("PackagingBoxName") + "' and isPackagingMandatory=1")
                        If resultCombo.Length > 0 Then
                            drSOPrintHeader("Pckgsize") = 0
                        Else
                            drSOPrintHeader("Pckgsize") = drResult(0)("Pckgsize")
                        End If
                    Else
                        drSOPrintHeader("Pckgsize") = drResult(0)("Pckgsize")
                    End If
                    If drResult(0)("PckgSize") = 0 Then
                        drSOPrintHeader("PckgQty") = 0
                    Else
                        drSOPrintHeader("PckgQty") = dr("Quantity") / drResult(0)("PckgSize")
                        If clsDefaultConfiguration.PackageFiedlsAllowed = True AndAlso drResult(0)("UnitOfMeasure") = "NOS" AndAlso drResult(0)("IsCombo") = True Then
                            drSOPrintHeader("PckgQty") = 0
                        End If
                    End If

                    drSOPrintHeader("PckgType") = drResult(0)("PckgType")
                    Dim drAddress As DataRow() = dtTempOrderAddresses.Select("AddressKey='" & dr("DeliveryAddress") & "'")
                    If drAddress.Length = 0 Then
                        drSOPrintHeader("DeliveryAddress") = "-"
                    Else
                        drSOPrintHeader("DeliveryAddress") = drAddress(0)("AddressValue")
                    End If
                    drSOPrintHeader("DeliveryDate") = dr("DeliveryDate")
                    drSOPrintHeader("DeliveryTime") = dr("DeliveryTime")
                    If IsBooking Then
                        Dim dr1 As DataRow() = dsMain.Tables("SOPackagingBoxDeliveryTempDtl").Select("pkglineNo='" + dr("pkglineNo").ToString + "' and deliverylineNo='" + dr("deliverylineNo").ToString + "'")
                        If dr1.Length > 0 Then
                            drSOPrintHeader("pickupQty") = dr1(0)("PickupQty")
                            drSOPrintHeader("PendingQty") = dr("Quantity") - dr1(0)("PickupQty")
                        End If
                    Else
                        'drSOPrintHeader("pickupQty") = drResult(0)("DeliveredQty")
                        drSOPrintHeader("pickupQty") = dr("DeliveredQty")
                        drSOPrintHeader("PendingQty") = dr("Quantity") - dr("DeliveredQty")
                    End If
                    dtDeliveryDetails.Rows.Add(drSOPrintHeader)
                    i = i + 1
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Private Function soprintarticlepaymentwisedetails() As Boolean 'by ketan 
        Try
            Dim a = dspackagingdelivery
            dtarticlewisepaymentdetails = objsopc.getsoprintarticlewisepaymenttablestruc()
            dtArticleWisePaymentDetails.Rows.Clear()
            Dim otherCharges As Double
            If dsMain.Tables("SalesOrderOtherCharges").Rows.Count() > 0 Then
                otherCharges = dsMain.Tables("SalesOrderOtherCharges").Compute("Sum(ChargeAmount)", "")
            End If
            Dim i As Integer = 1
            If dsmain.tables("sopackagingboxdeliverydtl").rows.count > 0 Then
                For Each dr As datarow In dsmain.tables("sopackagingboxdeliverydtl").select("status=true", "billlineno")
                    Dim drresult As datarow() = dsmain.tables("soitempackagingboxdtl").select("pkglineno='" + dr("pkglineno").tostring + "'")
                    Dim drpickupqty As datarow() = (dspackagingdelivery.tables(0)).select("deliverylineno='" + dr("deliverylineno").tostring + "'")
                    Dim drdisc As datarow() = dsmain.tables("sopackagingdiscdtl").select("pkglineno='" + dr("pkglineno").tostring + "' and status=true")
                    Dim drtax As DataRow() = dsMain.Tables("salesordertaxdtls").Select("packagingindex='" + dr("pkglineno").ToString + "' and status=true")
                    Dim DsPackVar As DataRow() = _dsPackagingVar.Tables(0).Select("packagingindex='" + dr("pkglineno").ToString + "'")
                    Dim dttx As datatable
                    If drtax.length > 0 Then
                        Dim taxtlabel As String = String.empty
                        For index = 0 To drtax.length - 1
                            taxtlabel = taxtlabel & ",'" & drtax(index)("taxlabel").tostring & "'"

                        Next
                        taxtlabel = taxtlabel.substring(1, taxtlabel.length - 1)
                        dttx = clscommon.getarticlegsttax(taxtlabel)
                    End If

                    drsoprintheader = dtarticlewisepaymentdetails.newrow()
                    drsoprintheader("billlineno") = dr("billlineno")
                    Dim drsrno As datarow() = dtarticlewisepaymentdetails.select("billlineno='" + dr("billlineno").tostring + "'")
                    If drsrno.length = 0 Then
                        drsoprintheader("srno") = i
                    Else
                        i = i - 1
                        drsoprintheader("srno") = i & "." & drsrno.length
                    End If
                    Dim discription = dsmain.tables("salesorderbulkcombohdr").select("combosrno='" & dr("billlineno") & "'")
                    If discription.length > 0 Then
                        drsoprintheader("articledescription") = discription(0)("packagingboxname")
                    Else
                        drsoprintheader("articledescription") = clscommon.getarticlefullname(dr("articlecode")).tostring()
                    End If
                    drsoprintheader("quantity") = dr("quantity")
                    drsoprintheader("price") = drresult(0)("sellingprice")
                    drsoprintheader("netamt") = drresult(0)("sellingprice") * dr("quantity")
                    drSOPrintHeader("othercharges") = otherCharges
                    '       If drdisc.Length > 0 Then
                    If drresult.Length > 0 Then
                        'drsoprintheader("discount") = drdisc(0)("discountamount")
                        'drsoprintheader("discountper") = drdisc(0)("discountper")
                        'drsoprintheader("taxableamount") = (drresult(0)("sellingprice") * dr("quantity")) - (drdisc(0)("discountamount"))
                        drSOPrintHeader("discount") = (drresult(0)("discountamount") / DsPackVar(0)("Quantity")) * dr("quantity") 'vipin
                        ' drSOPrintHeader("discountper") = drresult(0)("discountpercentage")
                        If drresult(0)("discountamount") = 0 Then 'vipin
                            drSOPrintHeader("discountper") = Math.Round(0, 2)
                        Else
                            drSOPrintHeader("discountper") = (((drresult(0)("discountamount") / DsPackVar(0)("Quantity")) * dr("quantity")) * 100) / (drresult(0)("sellingprice") * dr("quantity")) 'Math.Round((drresult(0)("discountamount") / CDbl(CtrllblDiscAmt.Text)) * 100, 2) 'vipin
                            'drSOPrintHeader("discountper") = ((DsPackVar(0)("Discount") / DsPackVar(0)("Quantity")) * dr("quantity"))
                        End If
                        drSOPrintHeader("taxableamount") = ((drresult(0)("sellingprice") * dr("quantity")) - ((drresult(0)("discountamount") / DsPackVar(0)("Quantity")) * dr("quantity")))
                    Else
                        drSOPrintHeader("discount") = 0
                        drSOPrintHeader("discountper") = 0
                        drSOPrintHeader("taxableamount") = (drresult(0)("sellingprice") * dr("quantity"))
                    End If


                    'If Not dttx Is Nothing AndAlso dttx.Rows.Count > 0 Then
                    '    Dim drcgsttax As DataRow() = dttx.Select("taxname='cgst'")
                    '    Dim drsgsttax As DataRow() = dttx.Select("taxname='sgst'")

                    '    drSOPrintHeader("cgst") = drcgsttax(0)("value")
                    '    drSOPrintHeader("sgst") = drsgsttax(0)("value")

                    '    drSOPrintHeader("cgstvalue") = drSOPrintHeader("taxableamount") * (drcgsttax(0)("value") / 100)
                    '    drSOPrintHeader("sgstvalue") = drSOPrintHeader("taxableamount") * (drsgsttax(0)("value") / 100)

                    'Else
                    '    drSOPrintHeader("cgst") = 0
                    '    drSOPrintHeader("sgst") = 0
                    '    drSOPrintHeader("cgstvalue") = 0
                    '    drSOPrintHeader("sgstvalue") = 0
                    'End If

                    'added by vipin
                    '' change By ketan In SO Tax Invoice Print 
                    'drSOPrintHeader("cgstvalue") = DsPackVar(0)("TotalTaxAmt") / 2
                    'drSOPrintHeader("sgstvalue") = DsPackVar(0)("TotalTaxAmt") / 2
                    drSOPrintHeader("cgstvalue") = ((DsPackVar(0)("TotalTaxAmt") / DsPackVar(0)("Quantity")) * dr("quantity")) / 2 'DsPackVar(0)("TotalTaxAmt") / 2
                    drSOPrintHeader("sgstvalue") = ((DsPackVar(0)("TotalTaxAmt") / DsPackVar(0)("Quantity")) * dr("quantity")) / 2 'DsPackVar(0)("TotalTaxAmt") / 2

                    drSOPrintHeader("cgst") = (drSOPrintHeader("cgstvalue") / drSOPrintHeader("taxableamount")) * 100
                    drSOPrintHeader("sgst") = (drSOPrintHeader("sgstvalue") / drSOPrintHeader("taxableamount")) * 100


                    drSOPrintHeader("grossamt") = drSOPrintHeader("taxableamount") + drSOPrintHeader("cgstvalue") + drSOPrintHeader("sgstvalue")
                    ' drSOPrintHeader("hsncode") = objComn.GetHSNbyArticle(dr("articlecode"))
                    If dr("IsCombo") = True Then  'vipin to hide HSN in case of combo
                        drSOPrintHeader("hsncode") = ""
                        drSOPrintHeader("IsCombo") = "Y"
                    Else
                        drSOPrintHeader("hsncode") = objComn.GetHSNbyArticle(dr("articlecode"))
                        drSOPrintHeader("IsCombo") = "N"
                    End If
                    'drSOPrintHeader("hsncode") = objComn.GetHSNbyArticle(dr("articlecode"))
                    dtArticleWisePaymentDetails.Rows.Add(drSOPrintHeader)
                    i = i + 1
                Next
            End If
        Catch ex As Exception

        End Try

    End Function
    Private Function SOPrintPickupHistory() As Boolean 'by ketan 
        Try
            dtPickupHistory = objSoPc.GetSOPrintPickupHistoryTableStruc()
            dtPickupHistory.Rows.Clear()
            If _pickupHistory.Tables("PickupHistory").Rows.Count > 0 Then
                For Each dr As DataRow In _pickupHistory.Tables("PickupHistory").Rows
                    drSOPrintHeader = dtPickupHistory.NewRow()
                    Dim drResult As DataRow() = dsMain.Tables("SOitemPackagingBoxDtl").Select("pkgLineNo='" + dr("PkgLineNo").ToString + "'")
                    Dim discription = dsMain.Tables("SalesOrderBulkComboHdr").Select("ComboSrNo='" & dr("BillLineNo") & "'")
                    If discription.Length > 0 Then
                        drSOPrintHeader("Orders") = discription(0)("PackagingBoxName")
                    Else
                        drSOPrintHeader("Orders") = clsCommon.GetArticleFullName(dr("Orders")).ToString
                    End If
                    drSOPrintHeader("Orderqty") = dr("Orderqty")
                    ' drSOPrintHeader("Pckgsize") = dr("Pckgsize")
                    If clsDefaultConfiguration.PackageFiedlsAllowed = True AndAlso discription.Length > 0 Then
                        Dim resultCombo As DataRow() = dtPackagingPrintBox.Select("KeyValue='" + discription(0)("PackagingBoxName") + "' and isPackagingMandatory=1")
                        If resultCombo.Length > 0 Then
                            drSOPrintHeader("Pckgsize") = 0
                        Else
                            drSOPrintHeader("Pckgsize") = dr("Pckgsize")
                        End If
                    Else
                        drSOPrintHeader("Pckgsize") = dr("Pckgsize")
                    End If
                    If clsDefaultConfiguration.PackageFiedlsAllowed = True AndAlso drResult(0)("UnitOfMeasure") = "NOS" AndAlso drResult(0)("IsCombo") = True Then
                        drSOPrintHeader("PckgQty") = 0
                    Else
                        drSOPrintHeader("PckgQty") = dr("PckgQty")
                    End If
                    drSOPrintHeader("PckgType") = dr("PckgType")
                    drSOPrintHeader("DeliveryDate") = dr("DeliveryDate")
                    drSOPrintHeader("DeliveryTime") = DateTime.Now
                    drSOPrintHeader("pickupQty") = dr("pickupQty")
                    drSOPrintHeader("PendingQty") = dr("PendingQty")
                    dtPickupHistory.Rows.Add(drSOPrintHeader)
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Function SOStrDetails() As Boolean 'by ketan 
        Try

            dtStrDetails = objSoPc.GetStrDetailsTableStruc()
            dtStrDetails.Rows.Clear()
            If dtStrPrint.Rows.Count > 0 Then
                For Each dr As DataRow In dtStrPrint.Rows
                    drSOPrintHeader = dtStrDetails.NewRow()
                    Dim STRRaised As String = dr("IsSTRRaised")
                    If STRRaised = True Then
                        drSOPrintHeader("STRRaised") = "Yes"
                    Else
                        drSOPrintHeader("STRRaised") = "No"
                    End If
                    drSOPrintHeader("STRRequestedfromSite") = dr("Requested")
                    drSOPrintHeader("STRNo") = dr("STRNo")
                    drSOPrintHeader("STRDeliveryDate") = dr("StrDate")
                    drSOPrintHeader("STRDeliveryTime") = dr("StrTime") '.ToString().Substring(0, dr("StrTime").ToString().Length - 1)
                    drSOPrintHeader("CREATEDON") = dr("GeneratedOn")
                    drSOPrintHeader("CREATEDBY") = dr("GeneratedBy")
                    dtStrDetails.Rows.Add(drSOPrintHeader)
                Next
            End If

        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Private Function GetSOPrintAddress() As Boolean 'by ketan 
        Try
            dtAddress = objSoPc.GetSOPrintAddressTableStruc()
            dtAddress.Rows.Clear()
            If dsMain.Tables("SOPackagingBoxDeliveryDtl").Rows.Count > 0 Then
                For Each dr As DataRow In dsMain.Tables("SOPackagingBoxDeliveryDtl").Select("Status=True", "BillLineNo")
                    Dim drAddress As DataRow() = dtTempOrderAddresses.Select("AddressKey='" & dr("DeliveryAddress") & "'")
                    drSOPrintHeader = dtAddress.NewRow()
                    If drAddress.Length > 0 Then
                        drSOPrintHeader("Address") = drAddress(0)("AddressValue")
                    End If
                    dtAddress.Rows.Add(drSOPrintHeader)
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    Private Function SOPrintRemarks() As Boolean 'by ketan 
        Try
            If dsMain.Tables("SalesOrderdtl").Rows.Count > 0 Then
                dtRemark = objSoPc.GetSOPrintRemarksTableStruc()
                dtRemark.Rows.Clear()
                For Each dr As DataRow In dsMain.Tables("SalesOrderdtl").Select("Status=True", "BillLineNo")
                    drSOPrintHeader = dtRemark.NewRow()
                    Dim discription = dsMain.Tables("SalesOrderBulkComboHdr").Select("ComboSrNo='" & dr("BillLineNo") & "'")
                    If discription.Length > 0 Then
                        drSOPrintHeader("ArticleShortName") = discription(0)("PackagingBoxName")
                    Else
                        drSOPrintHeader("ArticleShortName") = clsCommon.GetArticleFullName(dr("ArticleCode")).ToString()
                    End If
                    drSOPrintHeader("Remarks") = dr("Remarks")
                    drSOPrintHeader("BillLineNo") = dr("BillLineNo")
                    If dr("Remarks") <> "" Then
                        dtRemark.Rows.Add(drSOPrintHeader)
                    End If
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    '-----Added By Prasad for Invoice and Delivery Challan of Savoy Client
    ''' <summary>
    '''  Header Information of Invoice and Delivery Challan 
    ''' </summary>
    ''' <param name="dtSiteInfo"></param>
    ''' <param name="dtCustDetail"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Function SOPrintInvoiceChallanHeader(ByRef dtSiteInfo As DataTable, ByRef dtCustDetail As DataTable) As Boolean
        Try
            Dim deliverydate As String
            Dim PymnetTerm As String
            dtHeaderDetailsChallan = objSoPc.GetSOPrintInvoiceDeliveryChallanHeaderTableStruc()
            dtHeaderDetailsChallan.Rows.Clear()
            If dtSiteInfo.Rows.Count > 0 And dtCustDetail.Rows.Count > 0 Then
                drSOPrintHeader = dtHeaderDetailsChallan.NewRow()
                If dsMain.Tables("SalesInvoice").Rows.Count > 0 Then
                    For Each dr As DataRow In dsMain.Tables("SalesInvoice").Rows
                        drSOPrintHeader("InvoiceNo") = dr("SaleInvNumber")
                        drSOPrintHeader("InvoiceDate") = dr("SOInvTime")
                        drSOPrintHeader("TotalSum") = dr("AmountTendered")
                    Next
                End If
                If dsMain.Tables("SaleOrderTermNConditions").Rows.Count > 0 Then
                    Dim drInvCnt() = dsMain.Tables("SaleOrderTermNConditions").Select("RefInvNumber='" & dsMain.Tables("SalesInvoice").Rows(0)("SaleInvNumber") & "'")
                    If drInvCnt.Count > 0 Then
                        PymnetTerm = objPCSO.GetPaymentTermName(drInvCnt(0)("TnCcode")).ToString
                        drSOPrintHeader("ModeOfPayment") = PymnetTerm
                    Else
                        drSOPrintHeader("ModeOfPayment") = " - "
                    End If
                Else
                    drSOPrintHeader("ModeOfPayment") = " - "
                End If
                drSOPrintHeader("ClientName") = clsDefaultConfiguration.ClientName
                drSOPrintHeader("ClientAddress") = dtSiteInfo.Rows(0)("SiteAddressLn1") + "," + dtSiteInfo.Rows(0)("SiteAddressLn2") + " " + dtSiteInfo.Rows(0)("SiteAddressLn3") + "," + dtSiteInfo.Rows(0)("CityCode") + "-" + dtSiteInfo.Rows(0)("SitePinCode") + ", " + dtSiteInfo.Rows(0)("StateCode")
                drSOPrintHeader("ClientPhoneNo") = dtSiteInfo.Rows(0)("SiteTelephone1")
                drSOPrintHeader("Consignee") = dtCustDetail.Rows(0)("CUSTOMERNAME")
                drSOPrintHeader("ConsigneeAddress") = dtCustDetail.Rows(0)("ADDRESS") + "," + dtCustDetail.Rows(0)("City") + "," + dtCustDetail.Rows(0)("Pincode") + "," + dtCustDetail.Rows(0)("State") + "," + dtCustDetail.Rows(0)("Country")
                drSOPrintHeader("ConsigneePhoneNo") = dtCustDetail.Rows(0)("MobileNo")
                drSOPrintHeader("CurrencySymbol") = clsAdmin.CurrencySymbol
                drSOPrintHeader("AmountinWords") = AmtInWord(drSOPrintHeader("TotalSum"), clsAdmin.CurrencyCode, clsAdmin.CurrencyDescription)
                Dim Promoid = ""
                Dim taxid = ""
                If dsMain.Tables("SalesOrderTaxdtls").Rows.Count > 0 Then
                    taxid = dsMain.Tables("SalesOrderTaxdtls").Rows(0)("TaxLabel").ToString
                End If
                If Not dsMain.Tables("SalesDiscDtl") Is Nothing AndAlso dsMain.Tables("SalesDiscDtl").Rows.Count > 0 Then
                    Promoid = dsMain.Tables("SalesDiscDtl").Rows(0)("Promotionid").ToString
                End If
                Dim ds = objPCSO.GetSOPrintTaxDiscDtls(Promoid, taxid)
                If Not dsMain.Tables("SalesOrderTaxdtls") Is Nothing AndAlso dsMain.Tables("SalesOrderTaxdtls").Rows.Count > 0 Then
                    drSOPrintHeader("TaxName") = ds.Tables("msttax").Rows(0)("TaxName").ToString
                End If
                If Not dsMain.Tables("SalesDiscDtl") Is Nothing AndAlso dsMain.Tables("SalesDiscDtl").Rows.Count > 0 Then
                    drSOPrintHeader("DiscountName") = ds.Tables("Promotions").Rows(0)("OfferName").ToString
                End If
                Dim newdisc As Double = 0
                For Each dr1 As DataRow In dsMain.Tables("SOPackagingBoxDeliveryDtl").Select("DeliveredQty>0", "BillLineNo")
                    Dim drResultDisc As DataRow() = dsMain.Tables("SOPackagingDiscDtl").Select("pkgLineNo='" + dr1("pkgLineNo").ToString + "'")
                    Dim ss As Double = 0
                    If drResultDisc.Count > 0 Then
                        ss = drResultDisc(0)("DiscountAmount")
                    End If
                    newdisc = newdisc + ss
                Next
                drSOPrintHeader("Discount") = IIf(newdisc = 0, String.Empty, newdisc)
                Dim newtax As Double = 0
                For Each drnew As DataRow In dsMain.Tables("SOPackagingBoxDeliveryDtl").Select("DeliveredQty>0", "BillLineNo")
                    Dim drResultDisc As DataRow() = dsMain.Tables("SOPackagingTaxDtl").Select("pkgLineNo='" + drnew("pkgLineNo").ToString + "'")
                    Dim TaxCalc As Double = 0
                    If drResultDisc.Count > 0 Then
                        TaxCalc = drResultDisc(0)("TaxAmount")
                    End If
                    newtax = newtax + TaxCalc
                Next
                drSOPrintHeader("Tax") = FormatNumber(newtax, 2)
                dtHeaderDetailsChallan.Rows.Add(drSOPrintHeader)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    '-----Added By Prasad for Invoice and Delivery Challan of Savoy Client
    ''' <summary>
    ''' Detail information Pickupwise 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SOPrintInvoiceChallan() As Boolean
        Try

            dtOrderDetailsChallan = objSoPc.GetSOPrintInvoiceDeliveryChallanTableStruc()
            dtOrderDetailsChallan.Rows.Clear()
            If dsMain.Tables("SOPackagingBoxDeliveryDtl").Rows.Count > 0 Then
                For Each dr As DataRow In dsMain.Tables("SOPackagingBoxDeliveryDtl").Select("DeliveredQty>0", "BillLineNo")
                    Dim drResult As DataRow() = dsMain.Tables("SOitemPackagingBoxDtl").Select("pkgLineNo='" + dr("pkgLineNo").ToString + "'")
                    drSOPrintHeader = dtOrderDetailsChallan.NewRow()
                    Dim discription = dsMain.Tables("SalesOrderBulkComboHdr").Select("ComboSrNo='" & dr("BillLineNo") & "'")
                    If discription.Length > 0 Then
                        drSOPrintHeader("ArticleDescription") = discription(0)("PackagingBoxName")
                    Else
                        drSOPrintHeader("ArticleDescription") = clsCommon.GetArticleFullName(dr("ArticleCode")).ToString()
                    End If
                    drSOPrintHeader("Quantity") = dr("DeliveredQty")
                    drSOPrintHeader("UnitofMeasure") = dr("UnitOfMeasure")
                    drSOPrintHeader("Price") = drResult(0)("SellingPrice")
                    drSOPrintHeader("Total") = drResult(0)("SellingPrice") * dr("DeliveredQty")
                    dtOrderDetailsChallan.Rows.Add(drSOPrintHeader)

                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    Private Function GenerateSOPrint() As Boolean
        Try
            Dim DtshiftID As DataTable
            If Not dtHeaderDetails Is Nothing AndAlso dtHeaderDetails.Rows.Count > 0 Then
                dtHeaderDetails.Rows(0)("FooterMassage") = "This is computer generated invoice"
            End If
            Dim reportViewer2 As New ReportViewer()
            '  Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\SalesOrderPrint.rdl")
            Dim appPath As String

            If clsDefaultConfiguration.ColabaSOPrint Then
                appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\ColabaSOPrint.rdl")
            Else
                appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\SalesOrderPrint.rdl")
            End If
            reportViewer2.LocalReport.ReportPath = appPath

            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            reportViewer2.LocalReport.SetParameters(New ReportParameter("BarCode", BarCodestring))

            DtshiftID = clsCommon.GetShiftName(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID)

            'dtPaymentDetails.Columns.Add("Shift", GetType(String))
            If DtshiftID.Rows.Count > 0 Then
                For Each dtPaymentDetails12 In dtPaymentDetails.Rows
                    dtPaymentDetails12("Shift") = DtshiftID.Rows(0)(0).ToString.Trim
                Next
            End If

            Dim DataSource As New ReportDataSource("DS_SalesOrderPrintHeader", dtHeaderDetails)
            Dim DataSource1 As New ReportDataSource("DS_SalesOrderPrintPaymentDetails", dtPaymentDetails)
            Dim DataSource2 As New ReportDataSource("DS_salesOrderPrintPaymentsDetails1", dtPaymentDetails1)
            Dim DataSource3 As New ReportDataSource("DS_SalesOrderPrintDeliveryDetails", dtDeliveryDetails)
            Dim DataSource4 As New ReportDataSource("DS_SalesOrderPrintRemarks", dtRemark)
            Dim DataSource5 As New ReportDataSource("DS_salesOrderPrintOrderDetails", dtOrderComboDetails)
            Dim DataSource6 As New ReportDataSource("Ds_salesOrderPrintAddress", dtAddress)
            Dim DataSource7 As New ReportDataSource("DS_ArticleWiseGST", dtArticleWisePaymentDetails)
            Dim DataSource8 As New ReportDataSource("DS_salesReturnOrderPrintOrderDetails", dtReturnOrderComboDtl)
            Dim DataSource9 As New ReportDataSource("CustDetail", DtCustDtlForSOPrint)

            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.LocalReport.DataSources.Add(DataSource2)
            reportViewer2.LocalReport.DataSources.Add(DataSource3)
            reportViewer2.LocalReport.DataSources.Add(DataSource4)
            reportViewer2.LocalReport.DataSources.Add(DataSource5)
            reportViewer2.LocalReport.DataSources.Add(DataSource6)
            reportViewer2.LocalReport.DataSources.Add(DataSource7)
            reportViewer2.LocalReport.DataSources.Add(DataSource8)
            reportViewer2.LocalReport.DataSources.Add(DataSource9)
            reportViewer2.Refresh()
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            Dim obj As New clsCommon
            'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
            'If IsBooking = True Then
            '    Dim sitename As String
            '    sitename = objComn.GetSiteName(clsAdmin.SiteCode)
            '    path = clsDefaultConfiguration.DayCloseReportPath & "\PrashantCorner_" & sitename & "_" & CtrlTxtOrderNo.Text & ".pdf"
            'Else
            '    path = clsDefaultConfiguration.DayCloseReportPath & "\SOInvoice_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm") & ".pdf"
            'End If
            Dim SOPath As String = clsDefaultConfiguration.DayCloseReportPath
            Dim Newpath As String = ""
            Newpath = objSoPc.CreatePathForPrint(dtHeaderDetails, NEWdtDeliveryDetails, SOPath, "SOInvoice", False, False)
            path = Newpath
            StrTaxInvoicePath = Newpath  'vipin
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using

            If Not IsBooking = True Then
                If vIsPrintPreviewAllowed = True Then
                    Process.Start(path)
                Else
                    'Code For Print SO
                    PrinterName = SetPrinterName(dtPrinterInfo, "SalesOrder", "SOInvc")
                    Dim pdfdocument As New PdfDocument()
                    pdfdocument.LoadFromFile(path)
                    pdfdocument.PrinterName = PrinterName
                    pdfdocument.PrintDocument.PrinterSettings.Copies = 1
                    pdfdocument.PrintDocument.Print()
                    pdfdocument.Dispose()
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Function GenerateOrderPreparationPrint(Optional ByVal consolidated As Boolean = False, Optional ByVal PrintID As Integer = 0) As Boolean
        Try

            If Not dtHeaderDetails Is Nothing AndAlso dtHeaderDetails.Rows.Count > 0 Then
                dtHeaderDetails.Rows(0)("FooterMassage") = "This document is printed "
            End If
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\SalesOrderPreparation.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource As New ReportDataSource("dsSalesPreparationHeader", dtHeaderDetails)
            Dim DataSource1 As New ReportDataSource("dsSalesPreparationDeliveryDetails", NEWdtDeliveryDetails)
            Dim DataSource2 As New ReportDataSource("dsSalesPreparationRemarks", NewdtRemark)
            Dim DataSource3 As New ReportDataSource("dsSalesPreparationOrderDetails", NEWdtOrderComboDetails)
            Dim DataSource4 As New ReportDataSource("dsSalesPreparationSTRDetails", dtStrDetails)
            Dim DataSource5 As New ReportDataSource("SdBalToPay", dtPaymentDetails1)
            Dim DataSource6 As New ReportDataSource("Ds_salesOrderPrintAddress", dtAddress)
            Dim DataSource7 As New ReportDataSource("DS_salesReturnOrderPrintOrderDetails", dtReturnOrderComboDtl)
            Dim DataSource8 As New ReportDataSource("CustDetail", DtCustDtlForSOPrint)

            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.LocalReport.DataSources.Add(DataSource2)
            reportViewer2.LocalReport.DataSources.Add(DataSource3)
            reportViewer2.LocalReport.DataSources.Add(DataSource4)
            reportViewer2.LocalReport.DataSources.Add(DataSource5)
            reportViewer2.LocalReport.DataSources.Add(DataSource6)
            reportViewer2.LocalReport.DataSources.Add(DataSource7)
            reportViewer2.LocalReport.DataSources.Add(DataSource8)


            reportViewer2.Refresh()
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            Dim obj As New clsCommon
            'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
            ' path = clsDefaultConfiguration.DayCloseReportPath & "\SOPreparationPrint_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm") & ".pdf"
            Dim Newpath As String = ""
            Dim SOPath As String = clsDefaultConfiguration.DayCloseReportPath
            If consolidated = True Then
                Newpath = objSoPc.CreatePathForPrint(dtHeaderDetails, NEWdtDeliveryDetails, SOPath, "Consolidated-Order_prep", consolidated, True)
            Else
                Newpath = objSoPc.CreatePathForPrint(dtHeaderDetails, NEWdtDeliveryDetails, SOPath, "Order_prep" & PrintID & "", consolidated, True)
            End If
            path = Newpath
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using

            If vIsPrintPreviewAllowed = True Then
                Process.Start(path)
            Else
                'Code For Print SO
                PrinterName = SetPrinterName(dtPrinterInfo, "SalesOrder", "SOInvc")
                Dim pdfdocument As New PdfDocument()
                pdfdocument.LoadFromFile(path)
                pdfdocument.PrinterName = PrinterName
                pdfdocument.PrintDocument.PrinterSettings.Copies = 1
                pdfdocument.PrintDocument.Print()
                pdfdocument.Dispose()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Function GenerateSoDeliveryPrint() As Boolean
        Try

            If Not dtHeaderDetails Is Nothing AndAlso dtHeaderDetails.Rows.Count > 0 Then
                dtHeaderDetails.Rows(0)("FooterMassage") = "This is computer generated delivery note"
            End If
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\DeliveryNote.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            reportViewer2.LocalReport.SetParameters(New ReportParameter("BarCode", BarCodestring))
            Dim DataSource As New ReportDataSource("Header", dtHeaderDetails)
            Dim DataSource1 As New ReportDataSource("PrintDeliveryDetails", dtDeliveryDetails)
            Dim DataSource2 As New ReportDataSource("PaymentDetails", dtPaymentDetails1)
            Dim DataSource3 As New ReportDataSource("PaymentInvoiceDetails", dtPaymentDetails)
            Dim DataSource4 As New ReportDataSource("PickUpHistory", dtPickupHistory)
            Dim DataSource5 As New ReportDataSource("Ds_salesOrderPrintAddress", dtAddress)
            Dim DataSource6 As New ReportDataSource("CustDetail", DtCustDtlForSOPrint)

            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.LocalReport.DataSources.Add(DataSource2)
            reportViewer2.LocalReport.DataSources.Add(DataSource3)
            reportViewer2.LocalReport.DataSources.Add(DataSource4)
            reportViewer2.LocalReport.DataSources.Add(DataSource5)
            reportViewer2.LocalReport.DataSources.Add(DataSource6)
            reportViewer2.Refresh()
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            Dim obj As New clsCommon
            'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
            ' path = clsDefaultConfiguration.DayCloseReportPath & "\SODeliveryNote_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm") & ".pdf"
            Dim SOPath As String = clsDefaultConfiguration.DayCloseReportPath
            Dim Newpath As String = ""
            Newpath = objSoPc.CreatePathForPrint(dtHeaderDetails, NEWdtDeliveryDetails, SOPath, "SODeliveryNote", False, False)
            path = Newpath
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using

            If vIsPrintPreviewAllowed = True Then
                Process.Start(path)
            Else
                'Code For Print SO
                PrinterName = SetPrinterName(dtPrinterInfo, "SalesOrder", "SOInvc")
                Dim pdfdocument As New PdfDocument()
                pdfdocument.LoadFromFile(path)
                pdfdocument.PrinterName = PrinterName
                pdfdocument.PrintDocument.PrinterSettings.Copies = 1
                pdfdocument.PrintDocument.Print()
                pdfdocument.Dispose()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    '-----Added By Prasad for Invoice and Delivery Challan of Savoy Client
    ''' <summary>
    ''' Generating Invoice 
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Function GenerateSoInvoiceChallanPrint() As Boolean
        Try

            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\InvoiceSavoy.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource1 As New ReportDataSource("dsInvoiceChallanHeader", dtHeaderDetailsChallan)
            Dim DataSource2 As New ReportDataSource("dsInvoiceChallan", dtOrderDetailsChallan)

            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.LocalReport.DataSources.Add(DataSource2)
            reportViewer2.Refresh()
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")
            Dim obj As New clsCommon
            '  path = clsDefaultConfiguration.DayCloseReportPath & "\InvoiceChallan" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm") & ".pdf"
            Dim Newpath As String = ""
            Dim SOPath As String = clsDefaultConfiguration.DayCloseReportPath 'NEWdtDeliveryDetails, 
            Newpath = objSoPc.CreatePathForPrint(dtHeaderDetails, NEWdtDeliveryDetails, SOPath, "SODeliveryNote", False, False)
            path = Newpath
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using

            If vIsPrintPreviewAllowed = True Then
                Process.Start(path)
            Else
                'Code For Print SO
                PrinterName = SetPrinterName(dtPrinterInfo, "SalesOrder", "SOInvc")
                Dim pdfdocument As New PdfDocument()
                pdfdocument.LoadFromFile(path)
                pdfdocument.PrinterName = PrinterName
                pdfdocument.PrintDocument.PrinterSettings.Copies = 1
                pdfdocument.PrintDocument.Print()
                pdfdocument.Dispose()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    '-----Added By Prasad for Invoice and Delivery Challan of Savoy Client
    ''' <summary>
    ''' Generating Delivery Challan
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateSoDeliveryChallanPrint() As Boolean
        Try

            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\DeliveryChallanSavoy.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource1 As New ReportDataSource("dsDeliveryChallanHeader", dtHeaderDetailsChallan)
            Dim DataSource2 As New ReportDataSource("dsDeliveryChallanDetails", dtOrderDetailsChallan)

            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.LocalReport.DataSources.Add(DataSource2)
            reportViewer2.Refresh()
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")
            Dim obj As New clsCommon
            path = clsDefaultConfiguration.DayCloseReportPath & "\DeliveryChallan" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm") & ".pdf"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using

            If vIsPrintPreviewAllowed = True Then
                Process.Start(path)
            Else
                'Code For Print SO
                PrinterName = SetPrinterName(dtPrinterInfo, "SalesOrder", "SOInvc")
                Dim pdfdocument As New PdfDocument()
                pdfdocument.LoadFromFile(path)
                pdfdocument.PrinterName = PrinterName
                pdfdocument.PrintDocument.PrinterSettings.Copies = 1
                pdfdocument.PrintDocument.Print()
                pdfdocument.Dispose()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function


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
    Private Function PrintGiftReceipt() As Boolean
        'Try
        '    If Not dsPayment Is Nothing AndAlso dsPayment.Tables.Contains("MSTRecieptType") Then

        '        Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.GiftVoucherDocumentPrint, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserCode, CtrlSalesInfo1.CtrlTxtOrderNo.Text, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo1.CtrlTxtCustOrdRef.Text, vSalesInvcNo, GiftReceiptMessage, Nothing, Nothing, dtPrinterInfo, ShowFullName:=clsDefaultConfiguration.PrintItemFullName)

        '        'Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, vSalesInvcNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo1.CtrlTxtCustOrdRef.Text)

        '    Else

        '        Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.GiftVoucherDocumentPrint, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserCode, CtrlSalesInfo1.CtrlTxtOrderNo.Text, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), Nothing, CtrlSalesInfo1.CtrlTxtCustOrdRef.Text, vSalesInvcNo, GiftReceiptMessage, Nothing, Nothing, dtPrinterInfo, ShowFullName:=clsDefaultConfiguration.PrintItemFullName)

        '        'Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, vSalesInvcNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), Nothing, CtrlSalesInfo1.CtrlTxtCustOrdRef.Text)


        '    End If
        '    IsGiftVoucher = False
        'Catch ex As Exception
        'Finally

        '    IsGiftVoucher = False

        'End Try

    End Function


    'Private Function PrintCreditVoucher(ByVal drSite As DataRow, ByVal VoucherAmt As Double) As Boolean
    '    Try
    '        Dim PrintSo As New System.Text.StringBuilder
    '        If dsScan Is Nothing Then
    '            Exit Function
    '        End If

    '        PrintSo.Length = 0
    '        PrintSo.Append("                          CREDIT VOUCHER AGAINST CUSTOMER RETURN                                " & vbCrLf & vbCrLf)
    '        '--changed by rama on 12-aug-2009 as voucher no print wrong bug no-0000841
    '        'PrintSo.Append("Credit Note / Voucher No : " & vTerminalID & CtrlSalesInfo.CtrlTxtOrderNo.Text & "     Date    : " & Format(vCurrentDate, vDateFormat) & vbCrLf)
    '        PrintSo.Append("Credit Note / Voucher No : " & CVoucherNo & "     Date    : " & Format(vCurrentDate, vDateFormat) & vbCrLf)
    '        '--
    '        PrintSo.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)

    '        If vHeaderNote = True Then
    '            PrintSo.Append(vbCrLf & "Header Information is Welcome" & vbCrLf)
    '        End If

    '        If Not (drSite Is Nothing) Then
    '            PrintSo.Append("Store Name         : " & drSite.Item("SiteOfficialName") & "                Store Code   : " & vSiteCode & vbCrLf)
    '            PrintSo.Append("Store Address       : " & drSite.Item("SiteAddressLn1") & vbCrLf & "                            " & _
    '                         drSite.Item("SiteAddressLn2") & " " & drSite.Item("SiteAddressLn3") & _
    '                         drSite.Item("SitePinCode") & vbCrLf)
    '        End If
    '        PrintSo.Append("Store Tax Name1 :       Date    : " & Format(vCurrentDate, vDateFormat) & vbCrLf)
    '        PrintSo.Append("Store Tax NameN :       Date    : " & Format(vCurrentDate, vDateFormat) & vbCrLf)

    '        If Not dsPayment.Tables("MSTRecieptType") Is Nothing AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
    '            PrintSo.Append("Text Credit Note :" & vCurrencyCode & " " & VoucherAmt & vbCrLf)
    '        End If
    '        PrintSo.Append("         Applicable at :" & drSite.Item("SiteOfficialName") & vbCrLf)
    '        PrintSo.Append("Issued against S.O. No. :" & CtrlSalesInfo1.CtrlTxtOrderNo.Text & "   Date: " & Format(vCurrentDate, vDateFormat) & vbCrLf)

    '        'bug no-0000841
    '        PrintSo.Append("Valid for " & CVVoucherDay & " days " & vbCrLf)
    '        PrintSo.Append("From issue date if stamped and signed." & vbCrLf)
    '        PrintSo.Append("Specific and unique numbering (same as the Voucher number)" & vbCrLf & vbCrLf)

    '        PrintSo.Append("                                          _______________" & vbCrLf & vbCrLf)
    '        PrintSo.Append("                                          Signed" & vbCrLf)

    '        PrintSo.Append("<Terms & Condition If Any>" & vbCrLf & vbCrLf)

    '        If vFooterNote = True Then
    '            PrintSo.Append(vbCrLf & "Footer Information is Welcome" & vbCrLf)
    '        End If

    '        If vIsPrintPreviewAllowed = True Then
    '            fnPrint(PrintSo.ToString, "")          'Print Preview
    '        End If
    '        fnPrint(PrintSo.ToString, "PRN")   'Direct Print

    '        'PrintSo.Append("")                 'Set Debug Point

    '<<<<<<< .mine
    '    '    Catch ex As Exception
    '    '        ShowMessage(ex.Message, resourceMgr.GetString("CLAE05"))
    '    '    End Try
    '    'End Function
    '=======
    '        Catch ex As Exception
    '            ShowMessage(ex.Message, getValueByKey("CLAE05"))
    '        End Try
    '    End Function
    '>>>>>>> .r14440
    ''' <summary>
    ''' Preapring the Sales Order Header data for save
    ''' </summary>
    ''' <param name="dsMain"></param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PrepareHdrDataforSave(ByRef dsMain As DataSet) As Boolean
        strSOStatus = ""
        Dim drSOHdr As DataRow
        Dim findKey(2) As Object

        Try
            findKey(0) = vSiteCode
            findKey(1) = clsAdmin.Financialyear
            findKey(2) = CtrlTxtOrderNo.Text
            drSOHdr = dsMain.Tables("SalesOrderHDR").Rows.Find(findKey)
            If drSOHdr Is Nothing Then
                drSOHdr = dsMain.Tables("SalesOrderHDR").NewRow()
                IsNewRow = True
            End If
            drSOHdr("SaleOrderNumber") = CtrlTxtOrderNo.Text
            drSOHdr("SiteCode") = vSiteCode
            drSOHdr("FinYear") = clsAdmin.Financialyear
            drSOHdr("TerminalID") = vTerminalID
            drSOHdr("TransactionCode") = vDocType

            drSOHdr("CustomerNo") = CustomerNo
            drSOHdr("CustomerType") = "CLP"
            drSOHdr("NetAmt") = CDbl(CtrllblNetAmt.Text.Trim())
            drSOHdr("CostAmt") = CDbl(CtrllblNetAmt.Text.Trim())
            drSOHdr("GrossAmt") = CDbl(CtrllblGrossAmt.Text.Trim())
            drSOHdr("IsCSTApplied") = IsCSTApplicable
            drSOHdr("CSTTaxCode") = clsDefaultConfiguration.CSTTaxCode
            If Not (dsPayment Is Nothing) AndAlso dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables(0).Rows.Count > 0 Then
                drSOHdr("AdvanceAmt") = CType(IIf(drSOHdr("AdvanceAmt") Is DBNull.Value, 0, drSOHdr("AdvanceAmt")), Double) + CType(dsPayment.Tables(0).Compute("Sum(Amount)", ""), Double)
            Else
                drSOHdr("AdvanceAmt") = 0.0
            End If

            drSOHdr("DiscountPercentage") = FormatNumber(dsPackagingVar.Tables("PackagingMaterial").Compute("Sum(TotalDiscPercentage)", "") / dsPackagingVar.Tables("PackagingMaterial").Rows.Count, 2) 'FormatNumber(dsScan.Tables("ItemScanDetails").Compute("Sum(TotalDiscPercentage)", "") / dsScan.Tables("ItemScanDetails").Rows.Count, 2)
            drSOHdr("DiscountAmt") = CDbl(CtrllblDiscAmt.Text.Trim)
            drSOHdr("TotalDiscount") = CDbl(IIf(CtrllblDiscAmt.Text Is DBNull.Value, 0, CtrllblDiscAmt.Text.Trim))
            'drSOHdr("BalanceAmount") = CDbl(CtrlCashSummary1.lbltxt7.Trim)
            drSOHdr("BalanceAmount") = Math.Round(CDbl(CtrllblNetAmt.Text.Trim) - CDbl(drSOHdr("AdvanceAmt")), 2)
            drSOHdr("RoundedAmt") = CDbl(CtrllblNetAmt.Text)

            drSOHdr("LineItems") = dsScan.Tables("ItemScanDetails").Rows.Count
            drSOHdr("CreditNoteNo") = DBNull.Value
            drSOHdr("TransCurrency") = vCurrencyDescription
            drSOHdr("PostingStatus") = "Posted"

            drSOHdr("ExchangeRate") = "0"
            drSOHdr("CurrencyCode") = clsAdmin.CurrencyCode
            drSOHdr("TaxAmount") = IIf(dsPackagingVar.Tables("PackagingMaterial").Compute("Sum(TotalTaxAmt)", "") Is DBNull.Value, 0, dsPackagingVar.Tables("PackagingMaterial").Compute("Sum(TotalTaxAmt)", "")) 'IIf(dsScan.Tables("ItemScanDetails").Compute("Sum(TotalTaxAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("Sum(TotalTaxAmt)", ""))
            drSOHdr("TaxAmount") = IIf(dsPackagingVar.Tables("PackagingMaterial").Compute("Sum(TotalTaxAmt)", "") Is DBNull.Value, 0, dsPackagingVar.Tables("PackagingMaterial").Compute("Sum(TotalTaxAmt)", "")) + Math.Round(IIf(dsMain.Tables("SalesOrderTaxDtls").Compute("SUM(TaxValue)", "IsDocumentLevel=1") Is DBNull.Value, 0, dsMain.Tables("SalesOrderTaxDtls").Compute("SUM(taxValue)", "IsDocumentLevel=1")), 2) 'IIf(dsScan.Tables("ItemScanDetails").Compute("Sum(TotalTaxAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("Sum(TotalTaxAmt)", ""))
            If Not (dtOtherCharges Is Nothing) AndAlso dtOtherCharges.Rows.Count > 0 Then
                drSOHdr("ServiceTaxAmount") = FormatNumber(IIf(dtOtherCharges.Compute("Sum(TaxAmt)", "") Is DBNull.Value, 0, dtOtherCharges.Compute("Sum(TaxAmt)", "")), 2)
            End If

            'If clsDefaultConfiguration.IsSalesPersonApplicable = True Then
            If CtrlSalesPersons.CtrlSalesPersons.SelectedIndex >= 0 Then
                drSOHdr("SalesExecutiveCode") = CtrlSalesPersons.CtrlSalesPersons.SelectedValue
            Else
                drSOHdr("SalesExecutiveCode") = DBNull.Value
            End If

            drSOHdr("NoofReprints") = 1
            drSOHdr("ReprintReason") = DBNull.Value
            drSOHdr("ReprintDate") = DBNull.Value
            drSOHdr("ReprintTime") = DBNull.Value 'Format(Now, "hh:mm:ss tt")
            drSOHdr("DeletionDate") = DBNull.Value
            drSOHdr("DeletionTime") = DBNull.Value

            drSOHdr("IsExported") = 0
            drSOHdr("PromisedDeliveryDate") = DateTime.Now  'CtrlSalesInfo1.CtrlDtExpDelDate.Value
            drSOHdr("ActualDeliveryDate") = DateTime.Now 'CtrlSalesInfo1.CtrlDtExpDelDate.Value

            'vSalesOrderExpectedDeliveryDate = drSOHdr("ActualDeliveryDate")
            vSalesOrderExpectedDeliveryDate = DateTime.Now 'CtrlSalesInfo1.CtrlDtExpDelDate.Value
            'If IIf(IsDBNull(CtrlCustDtls1.cboAddrType.SelectedValue), "1", (CtrlCustDtls1.cboAddrType.SelectedValue)) = 99 Then
            '    drSOHdr("OtherDeliveryAdd") = CtrlCustDtls1.lblAddressValue.Text.Trim & ", " & CtrlCustDtls1.lblEmailIdValue.Value.Trim & ", " & CtrlCustDtls1.lblTelNoValue.Value.Trim
            '    drSOHdr("DeliveryAtCustAddressType") = 0
            'Else
            '    drSOHdr("DeliveryAtCustAddressType") = IIf(IsDBNull(CtrlCustDtls1.cboAddrType.SelectedValue), "1", (CtrlCustDtls1.cboAddrType.SelectedValue))
            'End If
            drSOHdr("OtherDeliveryAdd") = "" 'CtrlCustDtls1.lblAddressValue.Text.Trim & ", " & CtrlCustDtls1.lblEmailIdValue.Value.ToString().Trim & ", " & CtrlCustDtls1.lblTelNoValue.Value.ToString().Trim
            drSOHdr("DeliveryAtCustAddressType") = "0" ' IIf(IsDBNull(CtrlCustDtls1.cboAddrType.SelectedValue), "0", (CtrlCustDtls1.cboAddrType.SelectedValue))

            drSOHdr("CustomerOrderRef") = "" ' IIf(CtrlSalesInfo1.CtrlTxtCustOrdRef.Text.Trim = "", DBNull.Value, CtrlSalesInfo1.CtrlTxtCustOrdRef.Text.Trim)
            drSOHdr("Remarks") = "" ' IIf(CtrlSalesInfo1.CtrlTxtRemarks.Text.Trim = "", DBNull.Value, CtrlSalesInfo1.CtrlTxtRemarks.Text.Trim)
            If CtrltxrCust.Text.Length > 100 Then
                CtrltxrCust.Text = CtrltxrCust.Text.Substring(0, 100)
            End If
            drSOHdr("InvoiceCustName") = CtrltxrCust.Text ' IIf(CtrlSalesInfo1.CtrlTxtInvoice.Text.Trim = "", CtrlCustDtls1.lblCustNameValue.Text.Trim, CtrlSalesInfo1.CtrlTxtInvoice.Text.Trim)
            drSOHdr("AuthUserId") = vAuthUserId
            drSOHdr("AuthUserRemarks") = vAuthUserRemarks
            If IsBooking Then
                drSOHdr("SOStatus") = "Open"
                drSOHdr("CLPPoints") = 0
                drSOHdr("CLPDiscount") = 0
            Else
                If CDbl(CtrllblBaltoPay.Text) = 0 AndAlso Val(lblPickupQty.Text) = Val(lblOrderQty.Text) Then
                    drSOHdr("SOStatus") = "Closed"
                    drSOHdr("CLPPoints") = CDbl(IIf(dsPackagingVar.Tables("PackagingMaterial").Compute("Sum(CLPPoints)", "") Is DBNull.Value, 0, dsPackagingVar.Tables("PackagingMaterial").Compute("Sum(CLPPoints)", ""))) 'CDbl(dsScan.Tables("ItemScanDetails").Compute("Sum(CLPPoints)", ""))
                    drSOHdr("CLPDiscount") = CDbl(dsPackagingVar.Tables("PackagingMaterial").Compute("Sum(CLPDiscount)", ""))

                Else
                    drSOHdr("SOStatus") = "Open"
                    drSOHdr("CLPPoints") = 0
                    drSOHdr("CLPDiscount") = 0

                End If
            End If


            drSOHdr("BalancePoints") = DBNull.Value
            drSOHdr("AmendedNo") = 0

            drSOHdr("CREATEDAT") = vSiteCode
            drSOHdr("CREATEDBY") = clsAdmin.UserCode 'vUserName
            drSOHdr("CREATEDON") = objComn.GetCurrentDate
            drSOHdr("UPDATEDAT") = vSiteCode
            drSOHdr("UPDATEDBY") = clsAdmin.UserCode 'vUserName
            drSOHdr("UPDATEDON") = objComn.GetCurrentDate
            drSOHdr("STATUS") = True
            If rdDelYes.Checked Then
                drSOHdr("IsDelivery") = True
            Else
                drSOHdr("IsDelivery") = False
            End If
            drSOHdr("IsSTRRaised") = IsSTRGenerate
            If lblMultiPickupDel.Text = "Multi Pickup" AndAlso rbDPYes.Checked Then
                drSOHdr("IsMultiPickUp") = True
            Else
                drSOHdr("IsMultiPickUp") = False
            End If
            If lblMultiPickupDel.Text = "Multi Delivery" AndAlso rbDPYes.Checked Then
                drSOHdr("IsMultiDelivery") = True
            Else
                drSOHdr("IsMultiDelivery") = False
            End If

            If IsNewRow = True Then
                dsMain.Tables("SalesOrderHDR").Rows.Add(drSOHdr)
                IsNewRow = False
            End If

            Return True
        Catch ex As Exception
            IsNewRow = False
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    ''' <summary>
    ''' Preapring the Sales Order Details data for save
    ''' </summary>
    ''' <param name="dsMain"></param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PrepareDtlDataforSave(ByRef dsMain As DataSet) As Boolean
        strSOStatus = ""
        Dim drSODtl As DataRow
        Dim findKey(4) As Object
        ''---- Code Added By Mahesh for Bulk Combo ----

        If Not dsMain.Tables("SalesOrderDTL").Columns.Contains("RowIndex") Then
            dsMain.Tables("SalesOrderDTL").Columns.Add("RowIndex", Type.GetType("System.Int16"))
        End If
        Try
            For Each drScan As DataRow In dsScan.Tables("ItemScanDetails").Rows
                'Dim dvPackageItems As New DataView
                'dvPackageItems = New DataView(_dsPackagingVar.Tables("PackagingMaterial"), "SrNo='" & drScan("SrNo").ToString() & "'", "", DataViewRowState.CurrentRows)
                Dim Quantity = _dsPackagingVar.Tables(0).Compute("Sum(Quantity)", " RowIndex='" + drScan("RowIndex").ToString() + "'")
                Dim PickUpQuantity = _dsPackagingVar.Tables(0).Compute("Sum(PickUpQty)", " RowIndex='" + drScan("RowIndex").ToString() + "'")
                Dim CostAmount = _dsPackagingVar.Tables(0).Compute("Sum(CostAmount)", " RowIndex='" + drScan("RowIndex").ToString() + "'")
                Dim GrossAmount = _dsPackagingVar.Tables(0).Compute("Sum(GrossAmt)", " RowIndex='" + drScan("RowIndex").ToString() + "'")
                Dim NetAmount = _dsPackagingVar.Tables(0).Compute("Sum(NetAmount)", " RowIndex='" + drScan("RowIndex").ToString() + "'")
                '' added by ketan Worng data sav in salesorderdtl
                Dim TaxAmount = _dsPackagingVar.Tables(0).Compute("Sum(TotalTaxAmt)", " RowIndex='" + drScan("RowIndex").ToString() + "'")
                Dim LineDiscount = _dsPackagingVar.Tables(0).Compute("Sum(LineDiscount)", " RowIndex='" + drScan("RowIndex").ToString() + "'")

                Dim billLineNo As Integer = drScan("RowIndex") 'dsMain.Tables("SalesOrderDTL").Rows.Count + 1
                findKey(0) = vSiteCode
                findKey(1) = clsAdmin.Financialyear
                findKey(2) = CtrlTxtOrderNo.Text
                findKey(3) = drScan("EAN").ToString
                findKey(4) = billLineNo
                drSODtl = dsMain.Tables("SalesOrderDTL").Rows.Find(findKey)

                If drSODtl Is Nothing Then
                    drSODtl = dsMain.Tables("SalesOrderDTL").NewRow()
                    IsNewRow = True
                End If
                'drSODtl("BatchBarcode") = drScan("BatchBarcode")
                drSODtl("BillLineNo") = billLineNo
                drSODtl("RowIndex") = billLineNo 'drScan("RowIndex")
                '---- NEED to put BillNo in scan table to for print 
                drScan("BillLineNo") = billLineNo

                drSODtl("SaleOrderNumber") = CtrlTxtOrderNo.Text
                drSODtl("SiteCode") = vSiteCode
                drSODtl("DeliverySiteCode") = drScan("DeliverySiteCode")
                drSODtl("FinYear") = clsAdmin.Financialyear
                drSODtl("EAN") = drScan("EAN")
                drSODtl("ArticleCode") = drScan("ArticleCode")
                drSODtl("BatchBarcode") = drScan("BatchBarcode")
                ' drSODtl("PromisedDeliveryDate") = CtrlSalesInfo1.CtrlDtExpDelDate.Value
                drSODtl("ActualDeliveryDate") = drScan("ExpDelDate")
                'drSODtl("SalesStaffID") = IIf(CtrlSalesPersons.CtrlSalesPersons.SelectedValue = String.Empty, "", CtrlSalesPersons.CtrlSalesPersons.SelectedValue)

                drSODtl("SellingPrice") = drScan("SellingPrice")


                drSODtl("Quantity") = Quantity  'drScan("Quantity")
                If dsPayment.Tables("MSTRecieptType") IsNot Nothing AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                    drSODtl("DeliveredQty") = PickUpQuantity  'drScan("PickUpQty")
                    drSODtl("Delivered_Qty") = PickUpQuantity
                Else
                    drSODtl("DeliveredQty") = 0  'drScan("PickUpQty") for create booking
                    drSODtl("Delivered_Qty") = 0
                End If
                'drScan("PickUpQty")
                'If Not drScan("ReservedQty") Is DBNull.Value AndAlso drScan("ReservedQty") = True AndAlso drScan("Quantity") > drScan("PickUpQty") Then
                If Not drScan("ReservedQty") Is DBNull.Value AndAlso drScan("ReservedQty") = True Then
                    Dim otherSiteDeliveryQty As Decimal = SoDeliveryInfo.Sum(Function(x) IIf(x.ArticleCode = drScan("ArticleCode").ToString(), x.Quantity, 0))
                    drSODtl("ReservedQty") = CDbl(drScan("Quantity")) - CDbl(drScan("PickUpQty")) - otherSiteDeliveryQty
                    drSODtl("Reserved_Qty") = CDbl(drScan("Quantity")) - CDbl(drScan("PickUpQty")) - otherSiteDeliveryQty
                Else
                    drSODtl("ReservedQty") = 0
                    drSODtl("Reserved_Qty") = 0
                End If

                drSODtl("CostAmount") = CostAmount  'drScan("CostAmount") 'DBNull.Value
                drSODtl("GrossAmount") = GrossAmount  'Math.Round(drScan("GrossAmt"), 3)
                drSODtl("NetAmount") = NetAmount 'Math.Round(drScan("NetAmount"), 3)
                If dsPayment.Tables("MSTRecieptType") IsNot Nothing AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                    If Val(lblOrderQty.Text) = Val(lblPickupQty.Text) Then
                        drSODtl("ArticleStatus") = "Closed"
                        strSOStatus = "Closed"
                    Else
                        drSODtl("ArticleStatus") = "Open"       'need to confirm
                        strSOStatus = "Open"
                    End If
                Else
                    drSODtl("ArticleStatus") = "Open"       'need to confirm
                    strSOStatus = "Open"
                End If

                drScan("PromotionId") = IIf(drScan("PromotionId") Is DBNull.Value, 0, drScan("PromotionId"))
                drSODtl("OfferNo") = IIf(drScan("PromotionId") = "0,0", 0, drScan("PromotionId"))
                drSODtl("TransactionCode") = vDocType
                drSODtl("IsCLPApplicable") = drScan("IsCLP")
                drSODtl("CLPPoints") = CDbl(IIf(drScan("CLPPoints") Is DBNull.Value, 0, drScan("CLPPoints")))
                drSODtl("CLPDiscount") = CDbl(IIf(drScan("CLPDiscount") Is DBNull.Value, 0, drScan("CLPDiscount")))

                drSODtl("LineDiscount") = LineDiscount 'drScan("LineDiscount")
                drSODtl("DiscountAmount") = LineDiscount 'CDbl(IIf(drScan("LineDiscount") Is DBNull.Value, 0, drScan("LineDiscount"))) + CDbl(IIf(drSODtl("CLPDiscount") Is DBNull.Value, 0, drSODtl("CLPDiscount")))
                drSODtl("DiscountPercentage") = drScan("TotalDiscPercentage")

                drSODtl("UnitofMeasure") = IIf(drScan("UOM") Is DBNull.Value, 0, drScan("UOM"))
                drSODtl("ExclTaxAmt") = TaxAmount 'drScan("ExclTaxAmt")
                'drSODtl("TotalTaxAmount") = Math.Round(IIf(drScan("ExclTaxAmt") Is DBNull.Value, 0, drScan("ExclTaxAmt")) + IIf(drScan("IncTaxAmt") Is DBNull.Value, 0, drScan("IncTaxAmt")), 3)
                drSODtl("TotalTaxAmount") = TaxAmount ' Math.Round(IIf(drScan("TotalTaxAmt") Is DBNull.Value, 0, drScan("TotalTaxAmt")), 3)
                'TotalTaxAmt
                drSODtl("Section") = DBNull.Value
                drSODtl("Shelf") = DBNull.Value
                drSODtl("ScaleItem") = DBNull.Value
                drSODtl("ReturnNoSale") = DBNull.Value
                drSODtl("SerialNo") = DBNull.Value
                drSODtl("SerialNbrNotValid") = DBNull.Value
                drSODtl("IFBNo") = DBNull.Value
                If drScan("SalesStaffID") Is DBNull.Value Or drScan("SalesStaffID").ToString() = "" Then
                    drSODtl("SalesStaffID") = IIf(CtrlSalesPersons.CtrlSalesPersons.SelectedIndex < 0, "", CtrlSalesPersons.CtrlSalesPersons.SelectedValue)
                End If
                drSODtl("SalesReturnReasonCode") = DBNull.Value
                drSODtl("ReturnSaleOrderNo") = DBNull.Value
                drSODtl("ReturnSaleOrderDt") = DBNull.Value
                drSODtl("ReturnQty") = DBNull.Value
                drSODtl("AmendedNo") = 0


                drSODtl("FIRSTLEVELDISC") = CDbl(IIf(drScan("FIRSTLEVELDISC") Is DBNull.Value, 0, drScan("FIRSTLEVELDISC")))
                drSODtl("TOPLEVELDISC") = CDbl(IIf(drScan("TOPLEVELDISC") Is DBNull.Value, 0, drScan("TOPLEVELDISC")))
                drSODtl("FIRSTLEVEL") = IIf(drScan("FIRSTLEVEL") Is DBNull.Value, "", drScan("FIRSTLEVEL"))
                drSODtl("TopLevel") = IIf(drScan("TopLevel") Is DBNull.Value, "", drScan("TopLevel"))


                drSODtl("CREATEDAT") = vSiteCode
                drSODtl("CREATEDBY") = clsAdmin.UserCode 'vUserName
                drSODtl("CREATEDON") = objComn.GetCurrentDate
                drSODtl("UPDATEDAT") = vSiteCode
                drSODtl("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                drSODtl("UPDATEDON") = objComn.GetCurrentDate
                drSODtl("STATUS") = True
                drSODtl("IsCombo") = drScan("IsCombo")
                If drScan("IsCombo") Then
                    ' Dim result As DataRow() = DtSoBulkRemarks.Select("ArticleCode='" + drScan("Discription").ToString() + "'")
                    Dim result As DataRow() = DtSoBulkRemarks.Select("ArticleCode='" + drScan("Discription").ToString() + "' and SrNo='" + drScan("SrNo").ToString() + "'")
                    If result.Length > 0 Then
                        drSODtl("remarks") = result(0)("remark")
                    Else
                        drSODtl("remarks") = ""
                    End If
                Else
                    Dim result As DataRow() = DtSoBulkRemarks.Select("ArticleCode='" + drScan("ArticleCode").ToString() + "'")
                    If result.Length > 0 Then
                        drSODtl("remarks") = result(0)("remark")
                    Else
                        drSODtl("remarks") = ""
                    End If
                End If


                If IsNewRow = True Then
                    dsMain.Tables("SalesOrderDTL").Rows.Add(drSODtl)
                    IsNewRow = False
                End If
            Next

            ' to set the costprice

            SetCostPrice(clsDefaultConfiguration.isMAPbasedCost, dsMain.Tables("SalesOrderDTL"), clsAdmin.SiteCode, "CostAmount", clsDefaultConfiguration.IsBatchManagementReq)
            'SetCostPrice(clsDefaultConfiguration.isMAPbasedCost, dsMain.Tables("SalesOrderDTL"), clsAdmin.SiteCode, "CostAmount")            
            ' to set the costprice 
            For Each item In SoDeliveryInfo
                ' item.SalesOrderNumber = CtrlSalesInfo1.CtrlTxtOrderNo.Text
            Next
            Return True
        Catch ex As Exception
            IsNewRow = False
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function
    ''' <summary>
    ''' Preapring the Sales Order Packaging Variations data for save
    ''' </summary>
    ''' <param name="dsMain"></param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PreparePackageVariationDataforSave(ByRef dsMain As DataSet) As Boolean
        strSOStatus = ""

        Dim findKey(5) As Object
        Dim drSOPackDtl As DataRow
        ''---- Code Added By Mahesh for Bulk Combo ----

        'If Not dsPackagingVar.Tables("PackagingMaterial").Columns.Contains("RowIndex") Then
        '    dsMain.Tables("SalesOrderDTL").Columns.Add("RowIndex", Type.GetType("System.Int16"))
        'End If
        Try
            For Each drScan As DataRow In dsPackagingVar.Tables("PackagingMaterial").Rows


                Dim billLineNo As Integer = drScan("SrNo") 'dsMain.Tables("SalesOrderDTL").Rows.Count + 1
                findKey(0) = vSiteCode
                findKey(1) = clsAdmin.Financialyear
                findKey(2) = CtrlTxtOrderNo.Text
                findKey(3) = drScan("EAN").ToString
                findKey(4) = drScan("rowindex")
                findKey(5) = drScan("packagingindex")
                drSOPackDtl = dsMain.Tables("SOItemPackagingBoxDtl").Rows.Find(findKey)

                If drSOPackDtl Is Nothing Then
                    drSOPackDtl = dsMain.Tables("SOItemPackagingBoxDtl").NewRow()
                    IsNewRow = True
                End If
                'drSODtl("BatchBarcode") = drScan("BatchBarcode")
                drSOPackDtl("BillLineNo") = drScan("rowindex")
                drSOPackDtl("PkgLineNo") = drScan("packagingindex")
                'drSODtl("RowIndex") = billLineNo 'drScan("RowIndex")
                '---- NEED to put BillNo in scan table to for print 
                'drScan("BillLineNo") = billLineNo

                drSOPackDtl("SaleOrderNumber") = CtrlTxtOrderNo.Text
                drSOPackDtl("SiteCode") = vSiteCode
                drSOPackDtl("FinYear") = clsAdmin.Financialyear
                drSOPackDtl("EAN") = drScan("EAN")
                Dim result As DataRow() = dtPackagingBox.Select("ArticleShortName='" + drScan("PackagingMaterial").ToString() + "'")
                If drScan("ArticleType").ToString() = "Combo" Then

                    If result.Length > 0 Then
                        drSOPackDtl("ArticleCode") = result(0)("ArticleCode")
                        drSOPackDtl("PackageBaseUOM") = result(0)("BaseUnitofMeasure")
                        drSOPackDtl("PackagedEAN") = result(0)("EAN")
                        drSOPackDtl("PackagingBoxCode") = result(0)("ArticleCode")
                    Else
                        drSOPackDtl("ArticleCode") = drScan("ArticleCode")
                        drSOPackDtl("PackageBaseUOM") = ""
                        drSOPackDtl("PackagedEAN") = ""
                        drSOPackDtl("PackagingBoxCode") = ""
                    End If
                Else
                    drSOPackDtl("ArticleCode") = drScan("ArticleCode")
                    If result.Length > 0 Then
                        drSOPackDtl("PackageBaseUOM") = result(0)("BaseUnitofMeasure")
                        drSOPackDtl("PackagedEAN") = result(0)("EAN")
                        drSOPackDtl("PackagingBoxCode") = result(0)("ArticleCode")
                    Else
                        drSOPackDtl("PackageBaseUOM") = ""
                        drSOPackDtl("PackagedEAN") = ""
                        drSOPackDtl("PackagingBoxCode") = ""
                    End If

                End If
                drSOPackDtl("SellingPrice") = drScan("SellingPrice")
                drSOPackDtl("BatchBarcode") = drScan("BatchBarcode")
                drSOPackDtl("DeliverySiteCode") = drScan("DeliverySiteCode")
                drSOPackDtl("CostAmount") = drScan("CostAmount") 'DBNull.Value
                drSOPackDtl("GrossAmount") = Math.Round(drScan("GrossAmt"), 3)
                drSOPackDtl("NetAmount") = Math.Round(drScan("NetAmount"), 3)
                If dsPayment.Tables("MSTRecieptType") IsNot Nothing AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                    If Val(lblOrderQty.Text) = Val(lblPickupQty.Text) Then
                        drSOPackDtl("ArticleStatus") = "Closed"
                        strSOStatus = "Closed"
                    Else
                        drSOPackDtl("ArticleStatus") = "Open"       'need to confirm
                        strSOStatus = "Open"
                    End If
                Else
                    drSOPackDtl("ArticleStatus") = "Open"       'need to confirm
                    strSOStatus = "Open"
                End If

                drScan("PromotionId") = IIf(drScan("PromotionId") Is DBNull.Value, 0, drScan("PromotionId"))
                drSOPackDtl("OfferNo") = IIf(drScan("PromotionId") = "0,0", 0, drScan("PromotionId"))
                drSOPackDtl("TransactionCode") = vDocType
                drSOPackDtl("IsCLPApplicable") = drScan("IsCLP")
                drSOPackDtl("CLPPoints") = CDbl(IIf(drScan("CLPPoints") Is DBNull.Value, 0, drScan("CLPPoints")))
                drSOPackDtl("CLPDiscount") = CDbl(IIf(drScan("CLPDiscount") Is DBNull.Value, 0, drScan("CLPDiscount")))

                drSOPackDtl("LineDiscount") = drScan("LineDiscount")
                drSOPackDtl("DiscountAmount") = CDbl(IIf(drScan("LineDiscount") Is DBNull.Value, 0, drScan("LineDiscount"))) + CDbl(IIf(drSOPackDtl("CLPDiscount") Is DBNull.Value, 0, drSOPackDtl("CLPDiscount")))
                drSOPackDtl("DiscountPercentage") = drScan("TotalDiscPercentage")

                drSOPackDtl("UnitofMeasure") = IIf(drScan("UOM") Is DBNull.Value, 0, drScan("UOM"))
                drSOPackDtl("ExclTaxAmt") = drScan("ExclTaxAmt")
                'drSODtl("TotalTaxAmount") = Math.Round(IIf(drScan("ExclTaxAmt") Is DBNull.Value, 0, drScan("ExclTaxAmt")) + IIf(drScan("IncTaxAmt") Is DBNull.Value, 0, drScan("IncTaxAmt")), 3)
                drSOPackDtl("TotalTaxAmount") = Math.Round(IIf(drScan("TotalTaxAmt") Is DBNull.Value, 0, drScan("TotalTaxAmt")), 3)
                'TotalTaxAmt

                If drScan("SalesStaffID") Is DBNull.Value Or drScan("SalesStaffID").ToString() = "" Then
                    drSOPackDtl("SalesStaffID") = IIf(CtrlSalesPersons.CtrlSalesPersons.SelectedIndex < 0, "", CtrlSalesPersons.CtrlSalesPersons.SelectedValue)
                End If
                drSOPackDtl("SalesReturnReasonCode") = DBNull.Value
                drSOPackDtl("ReturnSaleOrderNo") = DBNull.Value
                drSOPackDtl("ReturnSaleOrderDt") = DBNull.Value
                drSOPackDtl("ReturnQty") = DBNull.Value




                'drSOPackDtl("PackagingBoxCode") = drScan("PackagingBoxCode")
                drSOPackDtl("PckgSize") = drScan("PckgSize")
                drSOPackDtl("PckgType") = drScan("PackagingType")
                drSOPackDtl("PckgMaterial") = drScan("PackagingMaterial")
                drSOPackDtl("PckgQty") = drScan("PckgQty")
                drSOPackDtl("IsHeader") = drScan("IsHeader")
                drSOPackDtl("IsCombo") = drScan("IsCombo")
                'drSOPackDtl("IsStatus") = drScan("IsStatus")

                drSOPackDtl("Quantity") = drScan("Quantity")  'drScan("Quantity")
                If dsPayment.Tables("MSTRecieptType") IsNot Nothing AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                    drSOPackDtl("DeliveredQty") = drScan("PickUpQty")
                Else
                    drSOPackDtl("DeliveredQty") = 0
                End If


                'If Not drScan("ReservedQty") Is DBNull.Value AndAlso drScan("ReservedQty") = True AndAlso drScan("Quantity") > drScan("PickUpQty") Then
                If Not drScan("ReservedQty") Is DBNull.Value AndAlso drScan("ReservedQty") = True Then
                    Dim otherSiteDeliveryQty As Decimal = SoDeliveryInfo.Sum(Function(x) IIf(x.ArticleCode = drScan("ArticleCode").ToString(), x.Quantity, 0))
                    drSOPackDtl("ReservedQty") = CDbl(drScan("Quantity")) - CDbl(drScan("PickUpQty")) - otherSiteDeliveryQty

                Else
                    drSOPackDtl("ReservedQty") = 0

                End If

                'drSOPackDtl("CostAmount") = CostAmount  'drScan("CostAmount") 'DBNull.Value
                'drSOPackDtl("GrossAmount") = GrossAmount  'Math.Round(drScan("GrossAmt"), 3)
                'drSOPackDtl("NetAmount") = NetAmount 'Math.Round(drScan("NetAmount"), 3)

                'drSOPackDtl("PrintName") = drScan("Discription")

                drSOPackDtl("CREATEDAT") = vSiteCode
                drSOPackDtl("CREATEDBY") = clsAdmin.UserCode 'vUserName
                drSOPackDtl("CREATEDON") = objComn.GetCurrentDate
                drSOPackDtl("UPDATEDAT") = vSiteCode
                drSOPackDtl("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                drSOPackDtl("UPDATEDON") = objComn.GetCurrentDate
                drSOPackDtl("STATUS") = True


                If IsNewRow = True Then
                    dsMain.Tables("SOItemPackagingBoxDtl").Rows.Add(drSOPackDtl)
                    IsNewRow = False
                End If
            Next

            Return True
        Catch ex As Exception
            IsNewRow = False
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function


    ''' <summary>
    ''' Preapring the Sales Order Discount data for save
    ''' </summary>
    ''' <param name="dsMain"></param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PrepareSODiscountDataforSave(ByRef dsMain As DataSet) As Boolean
        strSOStatus = ""

        Dim findKey(6) As Object
        Dim drSOPackDtl As DataRow


        'If Not dsPackagingVar.Tables("PackagingMaterial").Columns.Contains("RowIndex") Then
        '    dsMain.Tables("SalesOrderDTL").Columns.Add("RowIndex", Type.GetType("System.Int16"))
        'End If
        Try
            If dsPayment.Tables("MSTRecieptType") IsNot Nothing AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                Dim srno As Integer = 1

                For Each drScan As DataRow In dsPackagingVar.Tables("PackagingMaterial").Rows
                    If drScan("PickUpQty") > 0 Then  'AndAlso drScan("Discount")



                        Dim billLineNo As Integer = drScan("rowindex") 'dsMain.Tables("SalesOrderDTL").Rows.Count + 1
                        findKey(0) = vSiteCode
                        findKey(1) = clsAdmin.Financialyear
                        findKey(2) = CtrlTxtOrderNo.Text
                        findKey(3) = drScan("EAN").ToString
                        findKey(4) = drScan("rowindex")
                        findKey(5) = drScan("packagingindex")
                        findKey(6) = srno
                        drSOPackDtl = dsMain.Tables("SOPackagingDiscDtl").Rows.Find(findKey)

                        If drSOPackDtl Is Nothing Then
                            drSOPackDtl = dsMain.Tables("SOPackagingDiscDtl").NewRow()
                            IsNewRow = True
                        End If
                        'drSODtl("BatchBarcode") = drScan("BatchBarcode")
                        drSOPackDtl("BillLineNo") = drScan("rowindex")
                        drSOPackDtl("PkgLineNo") = drScan("packagingindex")
                        'drSODtl("RowIndex") = billLineNo 'drScan("RowIndex")
                        '---- NEED to put BillNo in scan table to for print 
                        'drScan("BillLineNo") = billLineNo
                        drSOPackDtl("SrNo") = srno
                        drSOPackDtl("SaleOrderNumber") = CtrlTxtOrderNo.Text
                        drSOPackDtl("SiteCode") = vSiteCode
                        drSOPackDtl("FinYear") = clsAdmin.Financialyear
                        drSOPackDtl("EAN") = drScan("EAN")
                        If drScan("ArticleType").ToString() = "Combo" Then
                            Dim result As DataRow() = dtPackagingBox.Select("ArticleShortName='" + drScan("PackagingMaterial").ToString() + "'")
                            If result.Length > 0 Then
                                drSOPackDtl("ArticleCode") = result(0)("ArticleCode")
                                'drSOPackDtl("BaseUnitofMeasure") = result(0)("BaseUnitofMeasure")
                            Else
                                drSOPackDtl("ArticleCode") = drScan("ArticleCode")
                            End If
                        Else
                            drSOPackDtl("ArticleCode") = drScan("ArticleCode")
                        End If






                        drSOPackDtl("PickUpQuantity") = drScan("PickUpQty")

                        drSOPackDtl("DiscountAmount") = (drScan("Discount") / drScan("Quantity")) * drScan("PickUpQty")
                        drSOPackDtl("DiscountPer") = drScan("TotalDiscPercentage") '((drScan("Discount") / drScan("Quantity")) * drScan("PickUpQty") / drScan("Discount")) * 100

                        drSOPackDtl("CREATEDAT") = vSiteCode
                        drSOPackDtl("CREATEDBY") = clsAdmin.UserCode 'vUserName
                        drSOPackDtl("CREATEDON") = objComn.GetCurrentDate
                        drSOPackDtl("UPDATEDAT") = vSiteCode
                        drSOPackDtl("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                        drSOPackDtl("UPDATEDON") = objComn.GetCurrentDate
                        drSOPackDtl("STATUS") = True


                        If IsNewRow = True Then
                            dsMain.Tables("SOPackagingDiscDtl").Rows.Add(drSOPackDtl)
                            IsNewRow = False
                        End If
                        srno = srno + 1
                    End If
                Next
            End If


            Return True
        Catch ex As Exception
            IsNewRow = False
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function
    ''' <summary>
    ''' Preapring the Sales Order STR data for save
    ''' </summary>
    ''' <param name="dsMain"></param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PrepareSOSTRtDataforSave(ByRef dsMain As DataSet) As Boolean
        strSOStatus = ""

        Dim findKey(3) As Object
        Dim drSOPackDtl As DataRow


        'If Not dsPackagingVar.Tables("PackagingMaterial").Columns.Contains("RowIndex") Then
        '    dsMain.Tables("SalesOrderDTL").Columns.Add("RowIndex", Type.GetType("System.Int16"))
        'End If
        Try



            For Each drScan As DataRow In DtSOStr.Rows
                ' If drScan("PickUpQty") > 0 AndAlso drScan("Discount") Then



                Dim billLineNo As Integer = drScan("strindex") 'dsMain.Tables("SalesOrderDTL").Rows.Count + 1
                findKey(0) = vSiteCode
                findKey(1) = clsAdmin.Financialyear
                findKey(2) = CtrlTxtOrderNo.Text
                findKey(3) = drScan("STRIndex").ToString

                drSOPackDtl = dsMain.Tables("SalesOrderStrDetails").Rows.Find(findKey)

                If drSOPackDtl Is Nothing Then
                    drSOPackDtl = dsMain.Tables("SalesOrderStrDetails").NewRow()
                    IsNewRow = True
                End If

                drSOPackDtl("STRIndex") = drScan("STRIndex")
                drSOPackDtl("SaleOrderNumber") = CtrlTxtOrderNo.Text
                drSOPackDtl("SiteCode") = vSiteCode
                drSOPackDtl("FinYear") = clsAdmin.Financialyear
                drSOPackDtl("EAN") = drScan("EAN")

                drSOPackDtl("ArticleCode") = drScan("ArticleCode")







                drSOPackDtl("ArticleDiscription") = drScan("Discription")

                drSOPackDtl("QtyPerBox") = drScan("QtyPerBox")
                drSOPackDtl("WtPerPiece") = drScan("WtPerPiece")


                drSOPackDtl("WtPerBox") = drScan("WtPerBox")
                drSOPackDtl("STRUOM") = drScan("STRUOM")
                If drScan("IsImageReq") = "1" Then
                    drSOPackDtl("IsHeader") = True
                Else
                    drSOPackDtl("IsHeader") = False
                End If
                drSOPackDtl("SrNo") = drScan("SrNo")
                drSOPackDtl("DeliveryIndex") = drScan("DeliveryIndex")
                drSOPackDtl("IsCombo") = drScan("IsCombo")
                drSOPackDtl("STRQuantity") = IIf(drScan("STRQty") Is DBNull.Value, 0, drScan("STRQty"))
                drSOPackDtl("STRDate") = drScan("STRDate")
                Dim date1 As Date
                Dim time1 As DateTime
                If drSOPackDtl("STRQuantity") = 0 Then
                    Dim dr() = DtSOStr.Select("STRQty>0")
                    If dr.Count > 0 Then
                        date1 = dr(0)("STRDate")
                        date1 = date1.ToShortDateString()
                        time1 = dr(0)("STRTime")
                        time1 = time1.ToShortTimeString()
                        drSOPackDtl("STRDate") = dr(0)("STRDate")
                    Else
                        date1 = Date.Now
                        date1 = date1.ToShortDateString()
                        time1 = Date.Now
                        time1 = time1.ToShortTimeString()
                    End If
                Else
                    date1 = drScan("STRDate")
                    date1 = date1.ToShortDateString()
                    time1 = drScan("STRTime")
                    time1 = time1.ToShortTimeString()
                End If
                drSOPackDtl("STRTime") = date1 & " " & time1 'drScan("STRTime")


                drSOPackDtl("CREATEDAT") = vSiteCode
                drSOPackDtl("CREATEDBY") = clsAdmin.UserCode 'vUserName
                drSOPackDtl("CREATEDON") = objComn.GetCurrentDate
                drSOPackDtl("UPDATEDAT") = vSiteCode
                drSOPackDtl("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                drSOPackDtl("UPDATEDON") = objComn.GetCurrentDate
                drSOPackDtl("STATUS") = True
                drSOPackDtl("StrNumber") = ""

                If IsNewRow = True Then
                    dsMain.Tables("SalesOrderStrDetails").Rows.Add(drSOPackDtl)
                    IsNewRow = False
                End If

                'End If
            Next

            Return True
        Catch ex As Exception
            IsNewRow = False
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    ''' <summary>
    ''' Preapring the Sales Order Packaging Variations delivery data for save
    ''' </summary>
    ''' <param name="dsMain"></param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PreparePackageVariationDeliveryDataforSave(ByRef dsMain As DataSet) As Boolean

        Try
            If UpdateSODeliveryGridData() Then
                strSOStatus = ""
                Dim drSOPackDtl As DataRow
                Dim findKey(6) As Object
                ''---- Code Added By Mahesh for Bulk Combo ----

                'If Not dsPackagingVar.Tables("PackagingMaterial").Columns.Contains("RowIndex") Then
                '    dsMain.Tables("SalesOrderDTL").Columns.Add("RowIndex", Type.GetType("System.Int16"))
                'End If
                '' added by ketan Print issue In 1 and 1.1 geting wrong
                Dim dtTemp As New DataTable
                If Not dsPackagingDelivery.Tables("PackagingDelivery") Is Nothing AndAlso dsPackagingDelivery.Tables("PackagingDelivery").Rows.Count > 0 Then
                    Dim dataView As New DataView(dsPackagingDelivery.Tables("PackagingDelivery"))
                    dataView.Sort = "SrNo ASC"
                    dtTemp = dataView.ToTable()
                End If
                ' For Each drScan As DataRow In dsPackagingDelivery.Tables("PackagingDelivery").Rows
                For Each drScan As DataRow In dtTemp.Rows


                    If drScan("Quantity") > 0 Then

                        'Dim billLineNo As Integer = drScan("SrNo") 'dsMain.Tables("SalesOrderDTL").Rows.Count + 1
                        findKey(0) = vSiteCode
                        findKey(1) = clsAdmin.Financialyear
                        findKey(2) = CtrlTxtOrderNo.Text
                        findKey(3) = drScan("EAN").ToString
                        findKey(4) = drScan("rowindex")
                        findKey(5) = drScan("packagingindex")
                        findKey(6) = drScan("deliveryindex")
                        drSOPackDtl = dsMain.Tables("SOPackagingBoxDeliveryDtl").Rows.Find(findKey)

                        If drSOPackDtl Is Nothing Then
                            drSOPackDtl = dsMain.Tables("SOPackagingBoxDeliveryDtl").NewRow()
                            IsNewRow = True
                        End If
                        'drSODtl("BatchBarcode") = drScan("BatchBarcode")
                        drSOPackDtl("BillLineNo") = drScan("rowindex")
                        drSOPackDtl("PkgLineNo") = drScan("packagingindex")
                        drSOPackDtl("DeliveryLineNo") = drScan("deliveryindex")
                        'drSODtl("RowIndex") = billLineNo 'drScan("RowIndex")
                        '---- NEED to put BillNo in scan table to for print 
                        'drScan("BillLineNo") = billLineNo
                        drSOPackDtl("SrNo") = drScan("SrNo")
                        drSOPackDtl("SaleOrderNumber") = CtrlTxtOrderNo.Text
                        drSOPackDtl("SiteCode") = vSiteCode
                        drSOPackDtl("FinYear") = clsAdmin.Financialyear
                        drSOPackDtl("EAN") = drScan("EAN")
                        drSOPackDtl("ArticleCode") = drScan("ArticleCode")
                        drSOPackDtl("IsHeader") = drScan("IsHeader")
                        drSOPackDtl("IsCombo") = drScan("IsCombo")

                        drSOPackDtl("DeliveryDate") = drScan("DeliveryDate")
                        Dim date1 As Date = drScan("DeliveryDate")
                        date1 = date1.ToShortDateString()
                        Dim time1 As DateTime = drScan("DeliveryTime")
                        time1 = time1.ToShortTimeString()
                        drSOPackDtl("DeliveryTime") = date1 & " " & time1 'drScan("STRTime")

                        'drSOPackDtl("DeliveryTime") = drScan("DeliveryTime")



                        drSOPackDtl("IsCustAddress") = drScan("IsCustAddress")  'drScan("Quantity")
                        If dsPayment.Tables("MSTRecieptType") IsNot Nothing AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                            drSOPackDtl("DeliveredQty") = IIf(drScan("PickUpQty") Is DBNull.Value, 0, drScan("PickUpQty")) ' drScan("PickUpQty")
                        Else
                            drSOPackDtl("DeliveredQty") = 0
                        End If

                        drSOPackDtl("Quantity") = drScan("Quantity")
                        drSOPackDtl("UnitofMeasure") = drScan("UOM")
                        drSOPackDtl("DeliveryAddress") = drScan("DeliveryAddress")

                        If drScan("PickUpQty") > 0 AndAlso dsPayment.Tables("MSTRecieptType") IsNot Nothing AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                            drSOPackDtl("LastPickUpDateTime") = DateTime.Now
                        End If


                        drSOPackDtl("CREATEDAT") = vSiteCode
                        drSOPackDtl("CREATEDBY") = clsAdmin.UserCode 'vUserName
                        drSOPackDtl("CREATEDON") = objComn.GetCurrentDate
                        drSOPackDtl("UPDATEDAT") = vSiteCode
                        drSOPackDtl("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                        drSOPackDtl("UPDATEDON") = objComn.GetCurrentDate
                        drSOPackDtl("STATUS") = True


                        If IsNewRow = True Then
                            dsMain.Tables("SOPackagingBoxDeliveryDtl").Rows.Add(drSOPackDtl)
                            IsNewRow = False
                        End If
                    End If
                Next

                Return True
            End If

            Return False
        Catch ex As Exception
            IsNewRow = False
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    Private Function PreparePackageVariationTempDeliveryDataforSave(ByRef dsMain As DataSet) As Boolean

        Try
            'If UpdateSODeliveryGridData() Then
            strSOStatus = ""
            Dim drSOPackDtl As DataRow
            Dim findKey(6) As Object
            ''---- Code Added By Mahesh for Bulk Combo ----

            'If Not dsPackagingVar.Tables("PackagingMaterial").Columns.Contains("RowIndex") Then
            '    dsMain.Tables("SalesOrderDTL").Columns.Add("RowIndex", Type.GetType("System.Int16"))
            'End If

            For Each drScan As DataRow In dsPackagingDelivery.Tables("PackagingDelivery").Rows


                If drScan("Quantity") > 0 Then

                    'Dim billLineNo As Integer = drScan("SrNo") 'dsMain.Tables("SalesOrderDTL").Rows.Count + 1
                    findKey(0) = vSiteCode
                    findKey(1) = clsAdmin.Financialyear
                    findKey(2) = CtrlTxtOrderNo.Text
                    findKey(3) = drScan("EAN").ToString
                    findKey(4) = drScan("rowindex")
                    findKey(5) = drScan("packagingindex")
                    findKey(6) = drScan("deliveryindex")
                    drSOPackDtl = dsMain.Tables("SOPackagingBoxDeliveryTempDtl").Rows.Find(findKey)

                    If drSOPackDtl Is Nothing Then
                        drSOPackDtl = dsMain.Tables("SOPackagingBoxDeliveryTempDtl").NewRow()
                        IsNewRow = True
                    End If
                    'drSODtl("BatchBarcode") = drScan("BatchBarcode")
                    drSOPackDtl("BillLineNo") = drScan("rowindex")
                    drSOPackDtl("PkgLineNo") = drScan("packagingindex")
                    drSOPackDtl("DeliveryLineNo") = drScan("deliveryindex")
                    'drSODtl("RowIndex") = billLineNo 'drScan("RowIndex")
                    '---- NEED to put BillNo in scan table to for print 
                    'drScan("BillLineNo") = billLineNo
                    drSOPackDtl("SrNo") = drScan("SrNo")
                    drSOPackDtl("SaleOrderNumber") = CtrlTxtOrderNo.Text
                    drSOPackDtl("SiteCode") = vSiteCode
                    drSOPackDtl("FinYear") = clsAdmin.Financialyear
                    drSOPackDtl("EAN") = drScan("EAN")
                    drSOPackDtl("ArticleCode") = drScan("ArticleCode")
                    drSOPackDtl("IsHeader") = drScan("IsHeader")
                    drSOPackDtl("IsCombo") = drScan("IsCombo")

                    drSOPackDtl("DeliveryDate") = drScan("DeliveryDate")
                    Dim date1 As Date = drScan("DeliveryDate")
                    date1 = date1.ToShortDateString()
                    Dim time1 As DateTime = drScan("DeliveryTime")
                    time1 = time1.ToShortTimeString()
                    drSOPackDtl("DeliveryTime") = date1 & " " & time1 'drScan("STRTime")

                    'drSOPackDtl("DeliveryTime") = drScan("DeliveryTime")



                    drSOPackDtl("IsCustAddress") = drScan("IsCustAddress")  'drScan("Quantity")

                    drSOPackDtl("DeliveredQty") = 0
                    drSOPackDtl("PickupQty") = IIf(drScan("PickUpQty") Is DBNull.Value, 0, drScan("PickUpQty"))

                    drSOPackDtl("Quantity") = drScan("Quantity")
                    drSOPackDtl("UnitofMeasure") = drScan("UOM")
                    drSOPackDtl("DeliveryAddress") = drScan("DeliveryAddress")

                    If drScan("PickUpQty") > 0 Then
                        drSOPackDtl("LastPickUpDateTime") = DateTime.Now
                    End If


                    drSOPackDtl("CREATEDAT") = vSiteCode
                    drSOPackDtl("CREATEDBY") = clsAdmin.UserCode 'vUserName
                    drSOPackDtl("CREATEDON") = objComn.GetCurrentDate
                    drSOPackDtl("UPDATEDAT") = vSiteCode
                    drSOPackDtl("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                    drSOPackDtl("UPDATEDON") = objComn.GetCurrentDate
                    drSOPackDtl("STATUS") = True


                    If IsNewRow = True Then
                        dsMain.Tables("SOPackagingBoxDeliveryTempDtl").Rows.Add(drSOPackDtl)
                        IsNewRow = False
                    End If
                End If
            Next

            Return True
            'End If


        Catch ex As Exception
            IsNewRow = False
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function
    ''' <summary>
    ''' Preapring the Sales Order Packaging pickup history data for save
    ''' </summary>
    ''' <param name="dsMain"></param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PrepareOrderVariationDeliveryDataforSave(ByRef dsMain As DataSet) As Boolean

        Try

            strSOStatus = ""
            Dim drSOPackDtl As DataRow
            Dim findKey(7) As Object
            ''---- Code Added By Mahesh for Bulk Combo ----

            'If Not dsPackagingVar.Tables("PackagingMaterial").Columns.Contains("RowIndex") Then
            '    dsMain.Tables("SalesOrderDTL").Columns.Add("RowIndex", Type.GetType("System.Int16"))
            'End If

            For Each drScan As DataRow In dsPackagingDelivery.Tables("PackagingDelivery").Rows


                If drScan("pickupqty") > 0 Then

                    'Dim billLineNo As Integer = drScan("SrNo") 'dsMain.Tables("SalesOrderDTL").Rows.Count + 1
                    findKey(0) = vSiteCode
                    findKey(1) = clsAdmin.Financialyear
                    findKey(2) = CtrlTxtOrderNo.Text
                    findKey(3) = drScan("EAN").ToString
                    findKey(4) = drScan("rowindex")
                    findKey(5) = drScan("packagingindex")
                    findKey(6) = drScan("deliveryindex")
                    findKey(7) = dsMain.Tables("OrderHdr").Rows(0)("DocumentNumber")
                    Dim result As DataRow() = dsPackagingVar.Tables(0).Select("PackagingIndex='" + drScan("PackagingIndex").ToString() + "'")
                    drSOPackDtl = dsMain.Tables("PkgOrderDtl").Rows.Find(findKey)

                    If drSOPackDtl Is Nothing Then
                        drSOPackDtl = dsMain.Tables("PkgOrderDtl").NewRow()
                        IsNewRow = True
                    End If
                    'drSODtl("BatchBarcode") = drScan("BatchBarcode")
                    drSOPackDtl("BillLineNo") = drScan("rowindex")
                    drSOPackDtl("PkgLineNo") = drScan("packagingindex")
                    drSOPackDtl("DeliveryLineNo") = drScan("deliveryindex")
                    If result.Length > 0 Then
                        drSOPackDtl("PckgSize") = result(0)("PckgSize")
                        If drScan("Quantity") > 0 AndAlso result(0)("PckgSize") > 0 Then
                            drSOPackDtl("PckgQty") = result(0)("PckgSize") * drScan("Quantity")
                        End If


                        drSOPackDtl("PckgType") = result(0)("PackagingType")
                    End If

                    'drSODtl("RowIndex") = billLineNo 'drScan("RowIndex")
                    '---- NEED to put BillNo in scan table to for print 
                    'drScan("BillLineNo") = billLineNo

                    drSOPackDtl("SaleOrderNumber") = CtrlTxtOrderNo.Text
                    drSOPackDtl("SiteCode") = vSiteCode
                    drSOPackDtl("FinYear") = clsAdmin.Financialyear
                    drSOPackDtl("EAN") = drScan("EAN")
                    drSOPackDtl("DocumentNo") = dsMain.Tables("OrderHdr").Rows(0)("DocumentNumber")
                    drSOPackDtl("ArticleCode") = drScan("ArticleCode")
                    drSOPackDtl("IsHeader") = drScan("IsHeader")
                    drSOPackDtl("IsCombo") = drScan("IsCombo")


                    drSOPackDtl("DeliveredQty") = drScan("PickUpQty")
                    drSOPackDtl("Quantity") = drScan("Quantity")
                    drSOPackDtl("UnitofMeasure") = drScan("UOM")



                    drSOPackDtl("CREATEDAT") = vSiteCode
                    drSOPackDtl("CREATEDBY") = clsAdmin.UserCode 'vUserName
                    drSOPackDtl("CREATEDON") = objComn.GetCurrentDate
                    drSOPackDtl("UPDATEDAT") = vSiteCode
                    drSOPackDtl("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                    drSOPackDtl("UPDATEDON") = objComn.GetCurrentDate
                    drSOPackDtl("STATUS") = True


                    If IsNewRow = True Then
                        dsMain.Tables("PkgOrderDtl").Rows.Add(drSOPackDtl)
                        IsNewRow = False
                    End If
                End If
            Next

            Return True



        Catch ex As Exception
            IsNewRow = False
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    ''' <summary>
    ''' Preapring the Sales Order Combo Header data for save
    ''' </summary>
    ''' <param name="dsMain"></param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PrepareComboHeaderDataforSave(ByRef dsMain As DataSet) As Boolean
        strSOStatus = ""
        Dim drSOComboHdr As DataRow
        Dim findKey(3) As Object
        ''---- Code Added By Mahesh for Bulk Combo ----

        'If Not dsPackagingVar.Tables("PackagingMaterial").Columns.Contains("RowIndex") Then
        '    dsMain.Tables("SalesOrderDTL").Columns.Add("RowIndex", Type.GetType("System.Int16"))
        'End If
        Try
            For Each drScan As DataRow In DtSoBulkComboHdr.Rows   'dsPackagingDelivery.Tables("PackagingDelivery").Rows


                Dim billLineNo As Integer = drScan("ComboSrNo") 'dsMain.Tables("SalesOrderDTL").Rows.Count + 1
                findKey(0) = CtrlTxtOrderNo.Text
                findKey(1) = vSiteCode
                findKey(2) = clsAdmin.Financialyear
                findKey(3) = drScan("ComboSrNo").ToString


                drSOComboHdr = dsMain.Tables("SalesOrderBulkComboHdr").Rows.Find(findKey)

                If drSOComboHdr Is Nothing Then
                    drSOComboHdr = dsMain.Tables("SalesOrderBulkComboHdr").NewRow()
                    IsNewRow = True
                End If
                'drSODtl("BatchBarcode") = drScan("BatchBarcode")
                drSOComboHdr("ComboSrNo") = drScan("ComboSrNo")

                'drSODtl("RowIndex") = billLineNo 'drScan("RowIndex")
                '---- NEED to put BillNo in scan table to for print 
                'drScan("BillLineNo") = billLineNo

                drSOComboHdr("SaleOrderNumber") = CtrlTxtOrderNo.Text
                drSOComboHdr("SiteCode") = vSiteCode
                drSOComboHdr("FinYear") = clsAdmin.Financialyear
                drSOComboHdr("PackagingBoxName") = drScan("PackagingBoxPrintName")
                drSOComboHdr("PackagingBoxCode") = drScan("PackagingBoxCode")
                drSOComboHdr("AdditionComments") = drScan("AdditionComments")



                drSOComboHdr("CREATEDAT") = vSiteCode
                drSOComboHdr("CREATEDBY") = clsAdmin.UserCode 'vUserName
                drSOComboHdr("CREATEDON") = objComn.GetCurrentDate
                drSOComboHdr("UPDATEDAT") = vSiteCode
                drSOComboHdr("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                drSOComboHdr("UPDATEDON") = objComn.GetCurrentDate
                drSOComboHdr("STATUS") = True
                drSOComboHdr("IsFixedPrice") = drScan("IsFixedPrice") 'vipin

                If IsNewRow = True Then
                    dsMain.Tables("SalesOrderBulkComboHdr").Rows.Add(drSOComboHdr)
                    IsNewRow = False
                End If
            Next

            Return True
        Catch ex As Exception
            IsNewRow = False
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    ''' <summary>
    ''' Preapring the Sales Order Combo detail data for save
    ''' </summary>
    ''' <param name="dsMain"></param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PrepareComboDetailDataforSave(ByRef dsMain As DataSet) As Boolean
        strSOStatus = ""
        Dim drSOComboDtl As DataRow
        Dim findKey(4) As Object
        ''---- Code Added By Mahesh for Bulk Combo ----

        'If Not dsPackagingVar.Tables("PackagingMaterial").Columns.Contains("RowIndex") Then
        '    dsMain.Tables("SalesOrderDTL").Columns.Add("RowIndex", Type.GetType("System.Int16"))
        'End If
        Try
            For Each drScan As DataRow In DtSoBulkComboDtl.Rows   'dsPackagingDelivery.Tables("PackagingDelivery").Rows


                Dim billLineNo As Integer = drScan("ComboSrNo") 'dsMain.Tables("SalesOrderDTL").Rows.Count + 1
                findKey(0) = CtrlTxtOrderNo.Text
                findKey(1) = vSiteCode
                findKey(2) = clsAdmin.Financialyear
                findKey(3) = drScan("ComboSrNo").ToString
                findKey(4) = drScan("Ean").ToString


                drSOComboDtl = dsMain.Tables("SalesOrderBulkComboDtl").Rows.Find(findKey)

                If drSOComboDtl Is Nothing Then
                    drSOComboDtl = dsMain.Tables("SalesOrderBulkComboDtl").NewRow()
                    IsNewRow = True
                End If
                'drSODtl("BatchBarcode") = drScan("BatchBarcode")
                drSOComboDtl("ComboSrNo") = drScan("ComboSrNo")

                'drSODtl("RowIndex") = billLineNo 'drScan("RowIndex")
                '---- NEED to put BillNo in scan table to for print 
                'drScan("BillLineNo") = billLineNo

                drSOComboDtl("SaleOrderNumber") = CtrlTxtOrderNo.Text
                drSOComboDtl("SiteCode") = vSiteCode
                drSOComboDtl("FinYear") = clsAdmin.Financialyear
                drSOComboDtl("ean") = drScan("ean")
                drSOComboDtl("ARTICLECODE") = drScan("ARTICLECODE")
                drSOComboDtl("ArticleDescription") = drScan("ArticleDescription")
                drSOComboDtl("PackagedUOM") = drScan("PackagedUOM")
                drSOComboDtl("Qty") = drScan("Qty")
                drSOComboDtl("Weight") = drScan("Weight")
                drSOComboDtl("STRQty") = drScan("STRQty")
                If clsDefaultConfiguration.IsNewSalesOrder Then   ''' added by nikhil
                    drSOComboDtl("Price") = drScan("Price")
                    ' drSOComboDtl("Discount") = drScan("Discount")  '' $$ added by nikhil on 18/08/17
                    drSOComboDtl("Tax") = drScan("Tax")
                    ' drSOComboDtl("TaxAmount") = drScan("TaxAmount")

                    Dim cmb As DataRow() = _dsPackagingVar.Tables(0).Select("SrNo='" + drScan("ComboSrNo").ToString() + "'")
                    Dim DrCOmboHdr As DataRow() = dsMain.Tables("SalesOrderBulkComboHDR").Select("ComboSrno ='" + drScan("ComboSrNo").ToString() + "'")
                    Dim DrCount As Integer = DtSoBulkComboDtl.Compute("Count(ComboSrNo)", "ComboSrno ='" + drScan("ComboSrNo").ToString() + "'")
                    If DrCOmboHdr.Length > 0 Then
                        If DrCOmboHdr(0)("IsFixedPrice") = True Then
                            drSOComboDtl("Discount") = (cmb(0)("Discount") / cmb(0)("Quantity")) / (DrCount)
                        Else
                            drSOComboDtl("Discount") = ((drScan("Qty") * drScan("Price") / cmb(0)("SellingPrice")) * cmb(0)("Discount")) / cmb(0)("Quantity")
                        End If
                    End If
                    drSOComboDtl("TaxAmount") = Math.Round((CDbl(drScan("Price") * drScan("Qty")) - drSOComboDtl("Discount")) * CDbl(drScan("Tax")) / 100, 2)
                End If
                drSOComboDtl("StrExcludeCheck") = drScan("StrExcludeCheck")
                drSOComboDtl("BaseUOM") = drScan("BaseUOM")
                drSOComboDtl("ItemQtyBaseUOM") = drScan("ItemQtyBaseUOM")


                drSOComboDtl("CREATEDAT") = vSiteCode
                drSOComboDtl("CREATEDBY") = clsAdmin.UserCode 'vUserName
                drSOComboDtl("CREATEDON") = objComn.GetCurrentDate
                drSOComboDtl("UPDATEDAT") = vSiteCode
                drSOComboDtl("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                drSOComboDtl("UPDATEDON") = objComn.GetCurrentDate
                drSOComboDtl("STATUS") = True


                If IsNewRow = True Then
                    dsMain.Tables("SalesOrderBulkComboDtl").Rows.Add(drSOComboDtl)
                    IsNewRow = False
                End If
            Next

            Return True
        Catch ex As Exception
            IsNewRow = False
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    ''' <summary>
    ''' Preapring the Sales Order Invoice data for save
    ''' </summary>
    ''' <param name="dsMain"></param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PrepareInvcDataforSave(ByRef dsMain As DataSet) As Boolean
        Try


            If Not (dsPayment Is Nothing) AndAlso dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                'vSalesInvcNo = "CM" & clsAdmin.TerminalID & objComn.getDocumentNo("CM")
                If OnlineConnect = True Then
                    'Changed by Rohit to generate Document No. for proper sorting
                    Try
                        ' vSalesInvcNo = GenDocNo("CM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, objComn.getDocumentNo("CM", clsAdmin.SiteCode))
                        ''GST changes by ketan add sitecode 3 digit in billno                               
                        vSalesInvcNo = GenDocNo("C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 3, 3) & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, objComn.getDocumentNo("CM", clsAdmin.SiteCode))
                    Catch ex As Exception
                        'vSalesInvcNo = "CM" & clsAdmin.TerminalID & objComn.getDocumentNo("CM", clsAdmin.SiteCode)                 
                        vSalesInvcNo = "C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 3, 3) & objComn.getDocumentNo("CM", clsAdmin.SiteCode)
                    End Try

                    Try
                        rbnLStatus.SmallImage = Spectrum.My.Resources.connected1.ToBitmap
                        rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus2")
                    Catch ex As Exception
                        LogException(ex)
                    End Try
                    'End Change by Rohit
                Else
                    'Changed by Rohit to generate Document No. for proper sorting
                    Try
                        vSalesInvcNo = GenDocNo("OCM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, objComn.getDocumentNo("CM", clsAdmin.SiteCode))
                    Catch ex As Exception
                        vSalesInvcNo = "OCM" & clsAdmin.TerminalID & objComn.getDocumentNo("CM", clsAdmin.SiteCode)
                    End Try

                    Try
                        rbnLStatus.SmallImage = Spectrum.My.Resources.disconnected1.ToBitmap
                        rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus1")
                    Catch ex As Exception
                        LogException(ex)
                    End Try
                    'End Change by Rohit
                End If
                Dim findKey(4) As Object
                Dim drInvc As DataRow
                Dim drSalesOrderPaymentTerm As DataRow
                For Each drPayment As DataRow In dsPayment.Tables("MSTRecieptType").Rows

                    findKey(0) = vSiteCode
                    findKey(1) = clsAdmin.Financialyear
                    findKey(2) = CtrlTxtOrderNo.Text
                    findKey(3) = vSalesInvcNo
                    findKey(4) = drPayment("SrNo")

                    drInvc = dsMain.Tables("SalesInvoice").Rows.Find(findKey)
                    If drInvc Is Nothing Then
                        drInvc = dsMain.Tables("SalesInvoice").NewRow()
                        IsNewRow = True
                    End If

                    drInvc("SiteCode") = vSiteCode
                    drInvc("FinYear") = clsAdmin.Financialyear
                    drInvc("TerminalID") = vTerminalID
                    drInvc("DocumentNumber") = CtrlTxtOrderNo.Text
                    drInvc("SaleInvNumber") = vSalesInvcNo
                    drInvc("SaleInvLineNumber") = drPayment("SrNo")
                    drInvc("BankAccNo") = drPayment("BankAccNo")
                    drInvc("DocumentType") = vDocType
                    drInvc("TerminalID") = vTerminalID
                    drInvc("TenderHeadCode") = drPayment("RecieptType")
                    drInvc("TenderTypeCode") = drPayment("RecieptTypeCode")
                    drInvc("AmountTendered") = drPayment("Amount")

                    drInvc("ExchangeRate") = drPayment("ExchangeRate")
                    drInvc("CurrencyCode") = drPayment("CurrencyCode")
                    drInvc("SOInvDate") = clsAdmin.DayOpenDate.Date 'vCurrentDate
                    drInvc("SOInvTime") = Format(objComn.GetCurrentDate, "hh:mm:ss tt")
                    drInvc("UserName") = vUserName

                    drInvc("ManagersKeytoUpdate") = DBNull.Value
                    drInvc("ChangeLine") = DBNull.Value
                    drInvc("RefNo_1") = clsCommon.ConvertToEnglish(IIf(drPayment("AMOUNTINCURRENCY") Is DBNull.Value, 0, drPayment("AMOUNTINCURRENCY"))) 'drPayment("Number")
                    drInvc("RefNo_2") = drPayment("Number")
                    drInvc("RefNo_3") = DBNull.Value
                    'drInvc("RefNo_4") = DBNull.Value
                    drInvc("RefNo_4") = drPayment("RefNo_4") 'vipin
                    drInvc("RefDate") = drPayment("date")
                    'drInvc("RefDate") = vCurrentDate

                    drInvc("CREATEDAT") = vSiteCode
                    drInvc("CREATEDBY") = clsAdmin.UserCode 'vUserName
                    drInvc("CREATEDON") = objComn.GetCurrentDate
                    drInvc("UPDATEDAT") = vSiteCode
                    drInvc("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                    drInvc("UPDATEDON") = objComn.GetCurrentDate
                    drInvc("STATUS") = True
                    'added by khusrao
                    ' for savoy
                    'If clsDefaultConfiguration.IsSavoy Then
                    '    drInvc("PaymentTermName") = drPayment("PaymentTermName")
                    'Else
                    '    drInvc("PaymentTermName") = DBNull.Value
                    'End If
                    If IsNewRow = True Then
                        dsMain.Tables("SalesInvoice").Rows.Add(drInvc)
                        IsNewRow = False
                    End If
                Next

            End If

            'Added by Rohit for CR - 5938

            'If dsMain.Tables.Contains("CheckDtls") Then
            '    dsMain.Tables.Remove("CheckDtls")
            'End If
            If dsPayment.Tables.Contains("CheckDtls") Then
                Dim dtCheckDtls As New DataTable
                dtCheckDtls = dsPayment.Tables("CheckDtls").Copy
                dtCheckDtls.TableName = "CheckDtls"
                dtCheckDtls.AcceptChanges()
                If Not dsMain.Tables.Contains("CheckDtls") Then
                    dsMain.Tables.Add(dtCheckDtls)
                Else
                    dsMain.Tables("CheckDtls").Merge(dtCheckDtls)
                End If

            End If

            objComn.PrepareCreditCheckData(dsMain, vSiteCode, vUserName, clsAdmin.Financialyear, vDocType, vSalesInvcNo, CtrlTxtOrderNo.Text, objComn.GetCurrentDate, _dDueDate, _strRemarks, "SO", clsAdmin.DayOpenDate)
            objComn.AddMode(dsMain.Tables("CheckDtls"))

            Return True

        Catch ex As Exception
            IsNewRow = False
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function
    'for savoy
    Private Function PrepareSaleOrderTermNConditionsSave(ByRef dsMain As DataSet) As Boolean
        Try
            If Not (dsPayment Is Nothing) AndAlso dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                Dim findKeySOPT(4) As Object
                Dim _srNo As Integer = 1
                Dim drSalesOrderPaymentTerm As DataRow
                For Each drPayment As DataRow In dsPayment.Tables("MSTRecieptType").Rows
                    'dsMain.Tables("SaleOrderTermNConditions").Clear()
                    If drPayment.Item("RecieptType").Equals("Credit Sales") Then
                        findKeySOPT(0) = vSiteCode
                        findKeySOPT(1) = clsAdmin.Financialyear
                        findKeySOPT(2) = CtrlTxtOrderNo.Text
                        findKeySOPT(3) = vSalesInvcNo
                        findKeySOPT(4) = _srNo
                        drSalesOrderPaymentTerm = dsMain.Tables("SaleOrderTermNConditions").Rows.Find(findKeySOPT)
                        If drSalesOrderPaymentTerm Is Nothing Then
                            drSalesOrderPaymentTerm = dsMain.Tables("SaleOrderTermNConditions").NewRow()
                            IsNewRow = True
                        End If
                        drSalesOrderPaymentTerm("SiteCode") = vSiteCode
                        drSalesOrderPaymentTerm("FinYear") = clsAdmin.Financialyear
                        drSalesOrderPaymentTerm("SaleOrderNumber") = CtrlTxtOrderNo.Text
                        drSalesOrderPaymentTerm("RefInvNumber") = vSalesInvcNo
                        drSalesOrderPaymentTerm("TnCcode") = PaymentTermId
                        drSalesOrderPaymentTerm("SrNo") = _srNo
                        drSalesOrderPaymentTerm("CREATEDAT") = vSiteCode
                        drSalesOrderPaymentTerm("CREATEDBY") = clsAdmin.UserCode 'vUserName
                        drSalesOrderPaymentTerm("CREATEDON") = objComn.GetCurrentDate
                        drSalesOrderPaymentTerm("UPDATEDAT") = vSiteCode
                        drSalesOrderPaymentTerm("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                        drSalesOrderPaymentTerm("UPDATEDON") = objComn.GetCurrentDate
                        drSalesOrderPaymentTerm("STATUS") = True
                        If IsNewRow = True Then
                            dsMain.Tables("SaleOrderTermNConditions").Rows.Add(drSalesOrderPaymentTerm)
                            IsNewRow = False
                        End If
                    End If

                Next
            End If
            If dsPayment.Tables.Contains("CheckDtls") Then
                Dim dtCheckDtls As New DataTable
                dtCheckDtls = dsPayment.Tables("CheckDtls").Copy
                dtCheckDtls.TableName = "CheckDtls"
                dtCheckDtls.AcceptChanges()
                If Not dsMain.Tables.Contains("CheckDtls") Then
                    dsMain.Tables.Add(dtCheckDtls)
                Else
                    dsMain.Tables("CheckDtls").Merge(dtCheckDtls)
                End If

            End If
            objComn.PrepareCreditCheckData(dsMain, vSiteCode, vUserName, clsAdmin.Financialyear, vDocType, vSalesInvcNo, CtrlTxtOrderNo.Text, objComn.GetCurrentDate, _dDueDate, _strRemarks, "SO", clsAdmin.DayOpenDate)
            objComn.AddMode(dsMain.Tables("CheckDtls"))
            Return True
        Catch ex As Exception
            IsNewRow = False
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function
    ''' <summary>
    ''' Preapring the Sales Order Other Tax data for save
    ''' </summary>
    ''' <param name="dsMain"></param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PrepareOtherTaxDataforSave(ByRef dsMain As DataSet) As Boolean
        Try
            Dim findKey(3) As Object
            Dim drOtherTax As DataRow
            Dim RowNo As Integer = 1

            If Not (dtOtherCharges Is Nothing) AndAlso dtOtherCharges.Rows.Count > 0 Then
                For Each drGetOTaxRow As DataRow In dtOtherCharges.Rows

                    If Not (drGetOTaxRow("ChargeName") Is DBNull.Value) AndAlso Not (drGetOTaxRow("ChargeAmount") Is DBNull.Value) Or _
                    Not (drGetOTaxRow("TaxName") Is DBNull.Value) AndAlso Not (drGetOTaxRow("TaxAmt") Is DBNull.Value) Then

                        findKey(0) = vSiteCode
                        findKey(1) = clsAdmin.Financialyear
                        findKey(2) = CtrlTxtOrderNo.Text
                        findKey(3) = RowNo

                        drOtherTax = dsMain.Tables("SalesOrderOtherCharges").Rows.Find(findKey)
                        If drOtherTax Is Nothing Then
                            drOtherTax = dsMain.Tables("SalesOrderOtherCharges").NewRow()
                            IsNewRow = True
                        End If

                        drOtherTax("SiteCode") = vSiteCode
                        drOtherTax("FinYear") = clsAdmin.Financialyear
                        drOtherTax("SaleOrderNumber") = CtrlTxtOrderNo.Text
                        drOtherTax("SerailNo") = RowNo
                        drOtherTax("ChargeName") = drGetOTaxRow("ChargeName")
                        drOtherTax("ChargeAmount") = IIf(drGetOTaxRow("ChargeAmount") Is DBNull.Value, 0.0, drGetOTaxRow("ChargeAmount"))
                        drOtherTax("TaxName") = drGetOTaxRow("TaxName")
                        drOtherTax("TaxAmt") = IIf(drGetOTaxRow("TaxAmt") Is DBNull.Value, 0.0, drGetOTaxRow("TaxAmt"))

                        drOtherTax("CREATEDAT") = vSiteCode
                        drOtherTax("CREATEDBY") = clsAdmin.UserCode 'vUserName
                        drOtherTax("CREATEDON") = objComn.GetCurrentDate
                        drOtherTax("UPDATEDAT") = vSiteCode
                        drOtherTax("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                        drOtherTax("UPDATEDON") = objComn.GetCurrentDate
                        drOtherTax("STATUS") = True

                        If IsNewRow = True Then
                            dsMain.Tables("SalesOrderOtherCharges").Rows.Add(drOtherTax)
                            RowNo += 1
                            IsNewRow = False
                        End If
                    End If

                Next
            End If

            Return True
        Catch ex As Exception
            IsNewRow = False
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function
    Private Function PreparSTRFactoryRemarkforSave(ByRef dsMain As DataSet) As Boolean
        strSOStatus = ""
        Dim drSOComboDtl As DataRow
        Dim findKey(3) As Object

        Try
            For Each drScan As DataRow In dtSTRFactoryRemark.Rows


                ' Dim billLineNo As Integer = drScan("ComboSrNo") 'dsMain.Tables("SalesOrderDTL").Rows.Count + 1
                findKey(0) = vSiteCode
                findKey(1) = clsAdmin.Financialyear
                findKey(2) = CtrlTxtOrderNo.Text
                findKey(3) = drScan("FactoryCode").ToString


                drSOComboDtl = dsMain.Tables("SalesOrderSTRFactoryRemark").Rows.Find(findKey)

                If drSOComboDtl Is Nothing Then
                    drSOComboDtl = dsMain.Tables("SalesOrderSTRFactoryRemark").NewRow()
                    IsNewRow = True
                End If

                drSOComboDtl("SiteCode") = vSiteCode
                drSOComboDtl("FinYear") = clsAdmin.Financialyear
                drSOComboDtl("SaleOrderNumber") = CtrlTxtOrderNo.Text
                drSOComboDtl("FactorySiteCode") = drScan("FactoryCode").ToString
                drSOComboDtl("Remark") = drScan("Remark").ToString

                drSOComboDtl("CREATEDAT") = vSiteCode
                drSOComboDtl("CREATEDBY") = clsAdmin.UserCode 'vUserName
                drSOComboDtl("CREATEDON") = objComn.GetCurrentDate
                drSOComboDtl("UPDATEDAT") = vSiteCode
                drSOComboDtl("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                drSOComboDtl("UPDATEDON") = objComn.GetCurrentDate
                drSOComboDtl("STATUS") = True


                If IsNewRow = True Then
                    dsMain.Tables("SalesOrderSTRFactoryRemark").Rows.Add(drSOComboDtl)
                    IsNewRow = False
                End If
            Next

            Return True
        Catch ex As Exception
            IsNewRow = False
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function
    ''' <summary>
    ''' Preapring the Sales Order Other Tax data for save
    ''' </summary>
    ''' <param name="dsMain"></param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PreparTaxDataforSave(ByRef dsMain As DataSet) As Boolean
        strSOStatus = ""
        'dsMain.Tables("SalesOrderTaxDtls").AcceptChanges()
        Dim findKey(5) As Object
        Dim drSOPackDtl As DataRow


        'If Not dsPackagingVar.Tables("PackagingMaterial").Columns.Contains("RowIndex") Then
        '    dsMain.Tables("SalesOrderDTL").Columns.Add("RowIndex", Type.GetType("System.Int16"))
        'End If
        Try
            Dim srno As Integer = 1

            dsMain.Tables("SalesOrderTaxDtls").AcceptChanges()
            For Each drScan As DataRow In dsMain.Tables("SalesOrderTaxDtls").Rows
                drScan("CREATEDAT") = vSiteCode
                drScan("CREATEDBY") = clsAdmin.UserCode 'vUserName
                drScan("CREATEDON") = objComn.GetCurrentDate
                drScan("UPDATEDAT") = vSiteCode
                drScan("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                drScan("UPDATEDON") = objComn.GetCurrentDate
                drScan("STATUS") = True
                drScan("DocumentType") = "SalesOrder"

            Next
            dsMain.Tables("SalesOrderTaxDtls").AcceptChanges()
            Return True
        Catch ex As Exception
            IsNewRow = False
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function
    '-----Added By Prasad for Calculating Tax PickupWise
    ''' <summary>
    ''' Preparing the Sales Order Tax data for save
    ''' </summary>
    ''' <param name="dsMain"></param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PrepareSOTaxDataforSave(ByRef dsMain As DataSet) As Boolean
        strSOStatus = ""

        Dim findKey(6) As Object
        Dim drSOPackDtl As DataRow

        Try
            If dsPayment.Tables("MSTRecieptType") IsNot Nothing AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                Dim srno As Integer = 1

                For Each drScan As DataRow In dsPackagingVar.Tables("PackagingMaterial").Rows
                    If drScan("PickUpQty") > 0 Then  'AndAlso drScan("Discount")

                        Dim billLineNo As Integer = drScan("rowindex") 'dsMain.Tables("SalesOrderDTL").Rows.Count + 1
                        findKey(0) = vSiteCode
                        findKey(1) = clsAdmin.Financialyear
                        findKey(2) = CtrlTxtOrderNo.Text
                        findKey(3) = drScan("EAN").ToString
                        findKey(4) = drScan("rowindex")
                        findKey(5) = drScan("packagingindex")
                        findKey(6) = srno
                        drSOPackDtl = dsMain.Tables("SOPackagingTaxDtl").Rows.Find(findKey)

                        If drSOPackDtl Is Nothing Then
                            drSOPackDtl = dsMain.Tables("SOPackagingTaxDtl").NewRow()
                            IsNewRow = True
                        End If
                        'drSODtl("BatchBarcode") = drScan("BatchBarcode")
                        drSOPackDtl("BillLineNo") = drScan("rowindex")
                        drSOPackDtl("PkgLineNo") = drScan("packagingindex")
                        'drSODtl("RowIndex") = billLineNo 'drScan("RowIndex")
                        '---- NEED to put BillNo in scan table to for print 
                        'drScan("BillLineNo") = billLineNo
                        drSOPackDtl("SrNo") = srno
                        drSOPackDtl("SaleOrderNumber") = CtrlTxtOrderNo.Text
                        drSOPackDtl("SiteCode") = vSiteCode
                        drSOPackDtl("FinYear") = clsAdmin.Financialyear
                        drSOPackDtl("EAN") = drScan("EAN")
                        If drScan("ArticleType").ToString() = "Combo" Then
                            Dim result As DataRow() = dtPackagingBox.Select("ArticleShortName='" + drScan("PackagingMaterial").ToString() + "'")
                            If result.Length > 0 Then
                                drSOPackDtl("ArticleCode") = result(0)("ArticleCode")
                                'drSOPackDtl("BaseUnitofMeasure") = result(0)("BaseUnitofMeasure")
                            Else
                                drSOPackDtl("ArticleCode") = drScan("ArticleCode")
                            End If
                        Else
                            drSOPackDtl("ArticleCode") = drScan("ArticleCode")
                        End If

                        drSOPackDtl("PickUpQuantity") = drScan("PickUpQty")
                        drSOPackDtl("TaxAmount") = (drScan("TotalTaxAmt") / drScan("Quantity")) * drScan("PickUpQty")
                        'drSOPackDtl("TaxAmount") = ((drScan("PickUpQty") * drScan("SellingPrice")) / CDbl(CtrllblGrossAmt.Text.Trim())) * CDbl(CtrllblTaxAmt.Text.Trim)
                        drSOPackDtl("CREATEDAT") = vSiteCode
                        drSOPackDtl("CREATEDBY") = clsAdmin.UserCode 'vUserName
                        drSOPackDtl("CREATEDON") = objComn.GetCurrentDate
                        drSOPackDtl("UPDATEDAT") = vSiteCode
                        drSOPackDtl("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                        drSOPackDtl("UPDATEDON") = objComn.GetCurrentDate
                        drSOPackDtl("STATUS") = True
                        If drSOPackDtl("TaxAmount") > 0 Then
                            Dim dr() = dsMain.Tables("SalesOrderTaxDtls").Select("SaleOrderNumber='" & CtrlTxtOrderNo.Text & "'")
                            If dr.Length > 0 Then
                                drSOPackDtl("TaxId") = dr(0)("TaxLabel")
                            End If
                        End If

                        If IsNewRow = True Then
                            dsMain.Tables("SOPackagingTaxDtl").Rows.Add(drSOPackDtl)
                            IsNewRow = False
                        End If
                        srno = srno + 1
                    End If

                Next
            End If


            Return True
        Catch ex As Exception
            IsNewRow = False
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function


    ''' <summary>
    ''' Preapring the Order Header data for save
    ''' </summary>
    ''' <param name="dsMain"></param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PrepareOrderHdrDataforSave(ByRef dsMain As DataSet) As Boolean
        Dim drOrderHdr As DataRow
        Dim findKey(2) As Object
        Try
            Dim clsCommon As New clsCommon
            Dim docnumber As String
            'docnumber = "OB" & clsAdmin.TerminalID & clsCommon.getDocumentNo("OutBound")
            If OnlineConnect = True Then
                'Changed by Rohit to generate Document No. for proper sorting
                Try
                    docnumber = GenDocNo("OB" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, clsCommon.getDocumentNo("OutBound", clsAdmin.SiteCode))
                Catch ex As Exception
                    docnumber = "OB" & clsAdmin.TerminalID & clsCommon.getDocumentNo("OutBound", clsAdmin.SiteCode)
                End Try

                Try
                    rbnLStatus.SmallImage = Spectrum.My.Resources.connected1.ToBitmap
                    rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus2")
                Catch ex As Exception

                End Try
                'End Change by Rohit
            Else
                'Changed by Rohit to generate Document No. for proper sorting
                Try
                    docnumber = GenDocNo("OOB" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, clsCommon.getDocumentNo("OutBound", clsAdmin.SiteCode))
                Catch ex As Exception
                    docnumber = "OOB" & clsAdmin.TerminalID & clsCommon.getDocumentNo("OutBound", clsAdmin.SiteCode)
                End Try

                Try
                    rbnLStatus.SmallImage = Spectrum.My.Resources.disconnected1.ToBitmap
                    rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus1")
                Catch ex As Exception

                End Try
                'End Change by Rohit
            End If
            findKey(0) = vSiteCode
            findKey(1) = clsAdmin.Financialyear
            findKey(2) = docnumber

            drOrderHdr = dsMain.Tables("OrderHdr").Rows.Find(findKey)
            If drOrderHdr Is Nothing Then
                drOrderHdr = dsMain.Tables("OrderHdr").NewRow
                IsNewRow = True
            End If

            drOrderHdr("SiteCode") = vSiteCode
            drOrderHdr("FinYear") = clsAdmin.Financialyear
            drOrderHdr("DocumentNumber") = docnumber
            drOrderHdr("DocumentType") = vDocType
            drOrderHdr("DocDate") = objComn.GetCurrentDate
            'drOrderHdr("SupplierCode") = clsDefaultConfiguration.SupplierCode
            drOrderHdr("PurchaseGroupCode") = DBNull.Value
            drOrderHdr("DeliverySiteCode") = vSiteCode
            drOrderHdr("ExpectedDeliveryDt") = DateTime.Now

            drOrderHdr("PaymentAfterDelDays") = DBNull.Value
            drOrderHdr("AdditionalTermsNConditions") = DBNull.Value
            drOrderHdr("AdditionalPaymentTerms") = DBNull.Value
            drOrderHdr("DocNetValue") = CDbl(CtrllblNetAmt.Text)
            drOrderHdr("DocGrossValue") = CDbl(CtrllblGrossAmt.Text)
            drOrderHdr("IsClosed") = True
            drOrderHdr("IsFreezed") = True

            drOrderHdr("ReturnReasonCode") = " "
            drOrderHdr("Remarks") = ""
            drOrderHdr("ApprovedDate") = DBNull.Value
            drOrderHdr("ApprovedLevel") = DBNull.Value
            drOrderHdr("ApprovalLevel") = DBNull.Value
            drOrderHdr("AmmendmentNo") = DBNull.Value

            drOrderHdr("ClosedDate") = DBNull.Value
            drOrderHdr("Transporter") = DBNull.Value
            drOrderHdr("DocApprovalID") = DBNull.Value
            drOrderHdr("ParentOrderNo") = DBNull.Value

            drOrderHdr("CREATEDAT") = vSiteCode
            drOrderHdr("CREATEDBY") = clsAdmin.UserCode 'vUserName
            drOrderHdr("CREATEDON") = objComn.GetCurrentDate
            drOrderHdr("UPDATEDAT") = vSiteCode
            drOrderHdr("UPDATEDBY") = clsAdmin.UserCode 'vUserName
            drOrderHdr("UPDATEDON") = objComn.GetCurrentDate
            drOrderHdr("STATUS") = True
            drOrderHdr("SupplierCode") = " "

            If IsNewRow = True Then
                dsMain.Tables("OrderHdr").Rows.Add(drOrderHdr)
                IsNewRow = False
            End If

            Return True
        Catch ex As Exception
            IsNewRow = False
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    ''' <summary>
    ''' Preapring the Order Details data for save
    ''' </summary>
    ''' <param name="dsMain"></param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PrepareOrderDtlDataforSave(ByRef dsMain As DataSet) As Boolean
        Dim drOrderDtl As DataRow
        Dim rowIndex As Integer = 1
        Dim findKey(4) As Object
        Try

            If Not clsDefaultConfiguration.IsBatchManagementReq Then
                For Each drScan As DataRow In dsScan.Tables(0).Rows
                    If drScan("PickUpQty") > 0 Then

                        findKey(0) = vSiteCode
                        findKey(1) = clsAdmin.Financialyear
                        findKey(2) = dsMain.Tables("OrderHdr").Rows(0)("DocumentNumber")
                        findKey(3) = drScan("EAN")
                        findKey(4) = drScan("BillLineNo") 'rowIndex ''change by ketan add billline no in orderdtl table 

                        drOrderDtl = dsMain.Tables("OrderDtl").Rows.Find(findKey)
                        If drOrderDtl Is Nothing Then
                            drOrderDtl = dsMain.Tables("OrderDtl").NewRow
                            IsNewRow = True
                        End If
                        'Dim dvPackageItems As New DataView
                        'dvPackageItems = New DataView(_dsPackagingVar.Tables("PackagingMaterial"), "SrNo='" & drScan("SrNo").ToString() & "'", "", DataViewRowState.CurrentRows)
                        Dim Quantity = _dsPackagingDelivery.Tables(0).Compute("Sum(Quantity)", " RowIndex='" + drScan("RowIndex").ToString() + "'")
                        Dim PickUpQuantity = _dsPackagingDelivery.Tables(0).Compute("Sum(PickUpQty)", " RowIndex='" + drScan("RowIndex").ToString() + "'")
                        Dim CostAmount = _dsPackagingVar.Tables(0).Compute("Sum(CostAmount)", " RowIndex='" + drScan("RowIndex").ToString() + "'")
                        Dim GrossAmount = _dsPackagingVar.Tables(0).Compute("Sum(GrossAmt)", " RowIndex='" + drScan("RowIndex").ToString() + "'")
                        Dim NetAmount = _dsPackagingVar.Tables(0).Compute("Sum(NetAmount)", " RowIndex='" + drScan("RowIndex").ToString() + "'")
                        drOrderDtl("SiteCode") = vSiteCode
                        drOrderDtl("FinYear") = clsAdmin.Financialyear
                        drOrderDtl("DocumentNumber") = dsMain.Tables("OrderHdr").Rows(0)("DocumentNumber")
                        drOrderDtl("ArticleCode") = drScan("ArticleCode")
                        drOrderDtl("EAN") = drScan("EAN")
                        drOrderDtl("LineNumber") = drScan("BillLineNo") 'rowIndex ''change by ketan add billline no in orderdtl table 
                        drOrderDtl("Qty") = Quantity
                        drOrderDtl("UnitofMeasure") = drScan("UOM")
                        drOrderDtl("BarCode") = drScan("BatchBarCode")
                        drOrderDtl("OpenQty") = DBNull.Value
                        drOrderDtl("DeliveredQty") = PickUpQuantity
                        drOrderDtl("DeliveryCompleted") = DBNull.Value
                        drOrderDtl("PurchaseGroupCode") = DBNull.Value
                        drOrderDtl("RefDocument") = DBNull.Value
                        drOrderDtl("RefDocumentNo") = DBNull.Value

                        drOrderDtl("PONo") = DBNull.Value
                        drOrderDtl("BirthListId") = DBNull.Value
                        drOrderDtl("SaleOrderNumber") = CtrlTxtOrderNo.Text
                        drOrderDtl("AllocationRule") = DBNull.Value
                        'drOrderDtl("MRP") = drScan("SellingPrice")
                        '   drOrderDtl("MRP") = Math.Round(NetAmount / Quantity, clsDefaultConfiguration.DecimalPlaces)
                        drOrderDtl("MRP") = Math.Round(NetAmount / Quantity, 3)
                        drOrderDtl("CostPrice") = drScan("SellingPrice")
                        drOrderDtl("NetCostPrice") = drScan("SellingPrice")

                        drOrderDtl("ExciseDutyAmt") = DBNull.Value
                        drOrderDtl("ExciseDutyRate") = DBNull.Value
                        drOrderDtl("PurchaseTaxAmt") = DBNull.Value
                        drOrderDtl("PurchaseTaxRate") = DBNull.Value
                        drOrderDtl("AdditionalChargeAmt") = DBNull.Value
                        drOrderDtl("DiscountAmount") = drScan("LineDiscount")
                        drOrderDtl("AdditionalChargeRate") = DBNull.Value 'drScan("ExclTaxAmt")
                        drOrderDtl("DocValue") = DBNull.Value
                        drOrderDtl("InboundQty") = DBNull.Value
                        drOrderDtl("DayOpenDate") = clsAdmin.DayOpenDate
                        drOrderDtl("CREATEDAT") = vSiteCode
                        drOrderDtl("CREATEDBY") = clsAdmin.UserCode 'vUserName
                        drOrderDtl("CREATEDON") = objComn.GetCurrentDate
                        drOrderDtl("UPDATEDAT") = vSiteCode
                        drOrderDtl("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                        drOrderDtl("UPDATEDON") = objComn.GetCurrentDate
                        drOrderDtl("STATUS") = True

                        If IsNewRow = True Then
                            dsMain.Tables("OrderDtl").Rows.Add(drOrderDtl)
                            IsNewRow = False
                        End If
                        rowIndex += 1
                    End If
                Next
            Else
                For Each drScan As SpectrumCommon.BtachbarcodeInfo In Batchbarcode
                    'If drScan("PickUpQty") > 0 Then
                    Dim itemobj = dsScan.Tables(0).Select("EAN='" & drScan.EAN & "'")(0)
                    If itemobj IsNot Nothing Then
                        findKey(0) = vSiteCode
                        findKey(1) = clsAdmin.Financialyear
                        findKey(2) = dsMain.Tables("OrderHdr").Rows(0)("DocumentNumber")
                        findKey(3) = drScan.EAN
                        findKey(4) = itemobj("BillLineNo") 'rowIndex ''change by ketan add billline no in orderdtl table 

                        drOrderDtl = dsMain.Tables("OrderDtl").Rows.Find(findKey)
                        If drOrderDtl Is Nothing Then
                            drOrderDtl = dsMain.Tables("OrderDtl").NewRow
                            IsNewRow = True
                        End If

                        drOrderDtl("SiteCode") = vSiteCode
                        drOrderDtl("FinYear") = clsAdmin.Financialyear
                        drOrderDtl("DocumentNumber") = dsMain.Tables("OrderHdr").Rows(0)("DocumentNumber")
                        drOrderDtl("ArticleCode") = drScan.ArticleCode
                        drOrderDtl("EAN") = drScan.EAN
                        drOrderDtl("LineNumber") = itemobj("BillLineNo") 'rowIndex ''change by ketan add billline no in orderdtl table 
                        drOrderDtl("Qty") = itemobj("Quantity")
                        drOrderDtl("UnitofMeasure") = itemobj("UOM")
                        drOrderDtl("BarCode") = drScan.BatchBarcode
                        drOrderDtl("OpenQty") = DBNull.Value
                        drOrderDtl("DeliveredQty") = drScan.Qty
                        drOrderDtl("DeliveryCompleted") = DBNull.Value
                        drOrderDtl("PurchaseGroupCode") = DBNull.Value
                        drOrderDtl("RefDocument") = DBNull.Value
                        drOrderDtl("RefDocumentNo") = DBNull.Value

                        drOrderDtl("PONo") = DBNull.Value
                        drOrderDtl("BirthListId") = DBNull.Value
                        drOrderDtl("SaleOrderNumber") = CtrlTxtOrderNo.Text
                        drOrderDtl("AllocationRule") = DBNull.Value
                        'drOrderDtl("MRP") = drScan("SellingPrice")
                        '  drOrderDtl("MRP") = Math.Round(itemobj("NetAmount") / itemobj("Quantity"), clsDefaultConfiguration.DecimalPlaces)
                        drOrderDtl("MRP") = Math.Round(itemobj("NetAmount") / itemobj("Quantity"), 3)
                        drOrderDtl("CostPrice") = itemobj("SellingPrice")
                        drOrderDtl("NetCostPrice") = itemobj("SellingPrice")

                        drOrderDtl("ExciseDutyAmt") = DBNull.Value
                        drOrderDtl("ExciseDutyRate") = DBNull.Value
                        drOrderDtl("PurchaseTaxAmt") = DBNull.Value
                        drOrderDtl("PurchaseTaxRate") = DBNull.Value
                        drOrderDtl("AdditionalChargeAmt") = DBNull.Value
                        drOrderDtl("DiscountAmount") = itemobj("LineDiscount")
                        drOrderDtl("AdditionalChargeRate") = DBNull.Value 'drScan("ExclTaxAmt")
                        drOrderDtl("DocValue") = DBNull.Value
                        drOrderDtl("InboundQty") = DBNull.Value
                        drOrderDtl("DayOpenDate") = clsAdmin.DayOpenDate
                        drOrderDtl("CREATEDAT") = vSiteCode
                        drOrderDtl("CREATEDBY") = clsAdmin.UserCode 'vUserName
                        drOrderDtl("CREATEDON") = objComn.GetCurrentDate
                        drOrderDtl("UPDATEDAT") = vSiteCode
                        drOrderDtl("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                        drOrderDtl("UPDATEDON") = objComn.GetCurrentDate
                        drOrderDtl("STATUS") = True

                        If IsNewRow = True Then
                            dsMain.Tables("OrderDtl").Rows.Add(drOrderDtl)
                            IsNewRow = False
                        End If
                        rowIndex += 1
                    End If

                    'End If
                Next
            End If


            Return True
        Catch ex As Exception
            IsNewRow = False
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function


    Private Function PrepareClpHdrDataforSave(ByRef dsMain As DataSet) As Boolean
        Dim drClpHdr As DataRow
        Try
            drClpHdr = dsMain.Tables("CLPTransaction").NewRow

            drClpHdr("SiteCode") = vSiteCode
            ' drClpHdr("BillNo") = CtrlSalesInfo1.CtrlTxtOrderNo.Text
            drClpHdr("BillDate") = objComn.GetCurrentDate().Date
            drClpHdr("AccumLationPoints") = TotalPoints
            drClpHdr("RedemptionPoints") = 0
            drClpHdr("BalAccumlationPoints") = TotalPoints
            drClpHdr("ClpProgramId") = vClpProgramId
            ' drClpHdr("ClpCustomerId") = CtrlCustSearch1.CtrlTxtCustNo.Text.Trim
            drClpHdr("IsRedemptionProcess") = False
            drClpHdr("CREATEDAT") = vSiteCode
            drClpHdr("CREATEDBY") = clsAdmin.UserCode 'vUserName
            drClpHdr("CREATEDON") = objComn.GetCurrentDate
            drClpHdr("UPDATEDAT") = vSiteCode
            drClpHdr("UPDATEDBY") = clsAdmin.UserCode 'vUserName
            drClpHdr("UPDATEDON") = objComn.GetCurrentDate
            drClpHdr("STATUS") = True

            dsMain.Tables("CLPTransaction").Rows.Add(drClpHdr)
            Return True

        Catch ex As Exception
            IsNewRow = False
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    Private Function PrepareClpDtlDataforSave(ByRef dsMain As DataSet) As Boolean
        Dim drClpDtl As DataRow
        Try
            Dim vRowNo As Integer = 1

            For Each drCLP As DataRow In dsScan.Tables(0).Select("PickUpQty>0")
                drClpDtl = dsMain.Tables("CLPTransactionsDetails").NewRow

                drClpDtl("SiteCode") = vSiteCode
                ' drClpDtl("BillNo") = CtrlSalesInfo1.CtrlTxtOrderNo.Text
                drClpDtl("BillLineNo") = vRowNo
                drClpDtl("EAN") = drCLP("EAN")
                drClpDtl("ArticleCode") = drCLP("ArticleCode")
                drClpDtl("SellingPrice") = drCLP("SellingPrice")
                drClpDtl("Quantity") = drCLP("Quantity")
                drClpDtl("CLPPoints") = drCLP("CLPPoints")
                drClpDtl("CLPDiscount") = drCLP("CLPDiscount")
                drClpDtl("CREATEDAT") = vSiteCode
                drClpDtl("CREATEDBY") = clsAdmin.UserCode 'vUserName
                drClpDtl("CREATEDON") = objComn.GetCurrentDate
                drClpDtl("UPDATEDAT") = vSiteCode
                drClpDtl("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                drClpDtl("UPDATEDON") = objComn.GetCurrentDate
                drClpDtl("STATUS") = True
                dsMain.Tables("CLPTransactionsDetails").Rows.Add(drClpDtl)

                vRowNo += 1
            Next

            Return True
        Catch ex As Exception
            IsNewRow = False
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function


    ''' <summary>
    ''' Print Sales Order
    ''' </summary>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PrintSalesOrders(ByVal drSite As DataRow, ByVal drHAdds As DataRow, Optional ByVal drDAdds As DataRow = Nothing, Optional ByVal status As String = Nothing) As Boolean
        Try
            'Dim PrintSalesOrder As New System.Text.StringBuilder
            'If dsScan Is Nothing Then
            '    Exit Function
            'End If

            'PrintSalesOrder.Length = 0
            'PrintSalesOrder.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)
            'PrintSalesOrder.Append("                          SALES ORDER                                 " & vbCrLf)
            'PrintSalesOrder.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)
            'PrintSalesOrder.Append("Company Name   : " & "CreativeIT India Ltd" & "     Company Code : " & "CRITI02" & vbCrLf)

            'If vIsPrintOfficialAddressAllowed = False Then

            '    If Not (drSite Is Nothing) Then
            '        PrintSalesOrder.Append("Store Name         : " & drSite.Item("SiteOfficialName") & "                Store Code   : " & vSiteCode & vbCrLf)
            '        PrintSalesOrder.Append("Store Address       : " & drSite.Item("SiteAddressLn1") & vbCrLf & "                            " & _
            '                     drSite.Item("SiteAddressLn2") & " " & drSite.Item("SiteAddressLn3") & _
            '                     drSite.Item("SitePinCode") & vbCrLf)
            '    End If
            'Else
            '    PrintSalesOrder.Append(vbCrLf & "Print Official Address " & vbCrLf)
            'End If

            'If vHeaderNote = True Then
            '    PrintSalesOrder.Append(vbCrLf & "Header Information is Welcome" & vbCrLf)
            'End If

            'PrintSalesOrder.Append(vbCrLf & "----------------------------------------------------------------------------------------------------" & vbCrLf)
            'PrintSalesOrder.Append("Sales Order No              : " & CtrlSalesInfo1.CtrlTxtOrderNo.Text & "                    Date    : " & Format(vCurrentDate.Date, vDateFormat) & vbCrLf)
            'PrintSalesOrder.Append("Expected Delivery Date  : " & Format(CtrlSalesInfo1.CtrlDtExpDelDate.Value, vDateFormat) & vbCrLf)
            'PrintSalesOrder.Append("Cashier Name                : " & vUserName & vbCrLf & vbCrLf)

            'PrintSalesOrder.Append("Customer Name    : " & CtrlCustDtls1.lblCustNameValue.Text & "          Customer Code : " & CtrlCustSearch1.CtrlTxtCustNo.Text & vbCrLf)

            'PrintSalesOrder.Append("Home Address       : " & drHAdds.Item("Address").ToString & vbCrLf)
            'PrintSalesOrder.Append("                           : " & drHAdds.Item("City") & ", " & drHAdds.Item("State") & ", " & drHAdds.Item("Country") & ", " & drHAdds.Item("PinCode") & vbCrLf & vbCrLf)

            'If drDelvAdds Is Nothing Then
            '    PrintSalesOrder.Append("Delivery Address : " & CtrlCustDtls1.lblAddressValue.Text)
            '    PrintSalesOrder.Append("Tel. No.  	   : " & CtrlCustDtls1.lblTelNoValue.Text & vbCrLf & vbCrLf)
            'Else
            '    PrintSalesOrder.Append("Delivery Address : " & drDAdds.Item("Address").ToString & vbCrLf)
            '    PrintSalesOrder.Append("                          : " & drDAdds.Item("City") & ", " & drDAdds.Item("State") & ", " & drDAdds.Item("Country") & ", " & drDAdds.Item("PinCode").ToString.Trim & vbCrLf)
            '    PrintSalesOrder.Append("Tel. No.  	             : " & drDAdds.Item("OfficeNo") & vbCrLf & vbCrLf)
            'End If

            'PrintSalesOrder.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)
            'PrintSalesOrder.Append("Item Code       Item Desc                               Qty       Price      Disc%  Tax%   NetAmt" & vbCrLf)
            'PrintSalesOrder.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)

            'For Each drDtl As DataRow In dsScan.Tables("ItemScanDetails").Rows
            '    PrintSalesOrder.Append(drDtl("ArticleCode") & Space(16 - drDtl("ArticleCode").ToString.Length) & _
            '                 drDtl("Discription") & Space(40 - drDtl("Discription").ToString.Length) & _
            '                 drDtl("Quantity") & Space(10 - drDtl("Quantity").ToString.Length) & _
            '                 Format(drDtl("SellingPrice"), "0.0") & Space(11 - drDtl("SellingPrice").ToString.Length) & _
            '                 drDtl("Discount") & Space(10 - drDtl("Discount").ToString.Length) & _
            '                 drDtl("ExclTaxAmt") & Space(11 - drDtl("ExclTaxAmt").ToString.Length) & Format(drDtl("NetAmount"), "0.0"))
            '    PrintSalesOrder.Append(vbCrLf)
            'Next
            'PrintSalesOrder.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)

            'PrintSalesOrder.Append("Total Qty    : " & lblOrderQty.Text & vbCrLf)
            'PrintSalesOrder.Append("Gross Amt  : " & CtrlCashSummary1.lbltxt1 & vbCrLf)
            'PrintSalesOrder.Append("Disc  Amt   : " & CtrlCashSummary1.CtrllblTaxAmt.Text & vbCrLf)
            'PrintSalesOrder.Append("Incl. Amt   : " & Format(IIf(dsScan.Tables("ItemScanDetails").Compute("Sum(IncTaxAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("Sum(IncTaxAmt)", ""))), "0.00" & vbCrLf)
            'PrintSalesOrder.Append("Excl. Amt   : " & Format(IIf(dsScan.Tables("ItemScanDetails").Compute("Sum(ExclTaxAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("Sum(ExclTaxAmt)", ""))), "0.00" & vbCrLf) 'Format(CDbl(dsScan.Tables("ItemScanDetails").Compute("Sum(ExclTaxAmt)", ""))), "0.00" & vbCrLf)
            'PrintSalesOrder.Append("Net   Amt   : " & CtrlCashSummary1.lbltxt4 & vbCrLf & vbCrLf)

            'If vIsPrintingTaxInfoAllowed = True Then
            '    PrintSalesOrder.Append("Print Tax Details..............." & vbCrLf & vbCrLf)
            'End If

            'PrintSalesOrder.Append("<Terms & Condition>" & vbCrLf & vbCrLf)

            'PrintSalesOrder.Append("Authorized Sign:...............            Customer Sign:................")

            'If vIsPromotionalMessageAllowed = True Then
            '    PrintSalesOrder.Append(vbCrLf & "Promotional Message is Welcome" & vbCrLf)
            'End If
            'If vFooterNote = True Then
            '    PrintSalesOrder.Append(vbCrLf & "Footer Information is Welcome" & vbCrLf)
            'End If


            'If vIsPrintPreviewAllowed = True Then
            '    fnPrint(PrintSalesOrder.ToString, "")          'Print Preview
            'End If
            'fnPrint(PrintSalesOrder.ToString, "PRN")       'Direct Print

            ''PrintSalesOrder.Append("Print")                'Set Debug Point
            ' Dim strRemark As String = CtrlSalesInfo1.CtrlTxtRemarks.Text

            SalesPersonName = IIf(CtrlSalesPersons.CtrlSalesPersons.SelectedIndex < 0, "", CtrlSalesPersons.CtrlSalesPersons.Text)
            Dim dsOtherCharges As New DataSet
            Dim dt As DataTable = dtOtherCharges.Copy()
            dsOtherCharges.Tables.Add(dt)
            dt.TableName = "NewOtherCharges"


            'changes to show credit sale to be adjusted in sales invoice
            Dim crdsale As Double = 0
            Dim crdsaleadjustamount As Double = 0
            '  Dim salesordernumber As String = CtrlSalesInfo1.CtrlTxtOrderNo.Text


            Dim dtCreditSaleData As New DataTable
            Dim objclsReturn As New clsCashMemoReturn
            ' dtCreditSaleData = objclsReturn.getCreditSaleBillData("'" + salesordernumber + "'")
            If dtCreditSaleData.Rows.Count > 0 AndAlso Not dtCreditSaleData Is Nothing Then
                For Each y In dtCreditSaleData.Rows
                    crdsaleadjustamount = y("CreditSaleAdjustment")
                    crdsale = y("CreditSale")
                Next
            End If
            If crdsaleadjustamount > 0 Then
                crdsale = crdsale - crdsaleadjustamount
            End If


            'If Not dsPayment Is Nothing AndAlso dsPayment.Tables.Contains("MSTRecieptType") Then

            '    'Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserCode, CtrlSalesInfo1.CtrlTxtOrderNo.Text, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo1.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, "Open", dsOtherCharges, ShowFullName:=clsDefaultConfiguration.PrintItemFullName, dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, SalesOrderDeliveryDate:=vSalesOrderExpectedDeliveryDate, strSalesPerson:=SalesPersonName)
            '    Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, CtrlSalesInfo1.CtrlTxtOrderNo.Text, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo1.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, status, dsOtherCharges, ShowFullName:=clsDefaultConfiguration.PrintItemFullName, strReturnReason:=strRemark, dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, SalesOrderDeliveryDate:=vSalesOrderExpectedDeliveryDate, strSalesPerson:=SalesPersonName, CreditSale:=crdsale)

            'Else

            '    'Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserCode, CtrlSalesInfo1.CtrlTxtOrderNo.Text, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), Nothing, CtrlSalesInfo1.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, "Open", dsOtherCharges, ShowFullName:=clsDefaultConfiguration.PrintItemFullName, dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, SalesOrderDeliveryDate:=vSalesOrderExpectedDeliveryDate, strSalesPerson:=SalesPersonName)
            '    Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, CtrlSalesInfo1.CtrlTxtOrderNo.Text, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), Nothing, CtrlSalesInfo1.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, status, dsOtherCharges, ShowFullName:=clsDefaultConfiguration.PrintItemFullName, strReturnReason:=strRemark, dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, SalesOrderDeliveryDate:=vSalesOrderExpectedDeliveryDate, strSalesPerson:=SalesPersonName, CreditSale:=crdsale)

            'End If

        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    ''' <summary>
    ''' Print Sales Order Invoice
    ''' </summary>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PrintSalesOrdersInvoice(ByVal drSite As DataRow, ByVal drHAdds As DataRow, Optional ByVal drDAdds As DataRow = Nothing) As Boolean
        'Dim PrintInvoice As New System.Text.StringBuilder

        Try
            '    If dsScan Is Nothing Then
            '        Exit Function
            '    End If

            '    PrintInvoice.Length = 0
            '    If vHeaderNote = True Then
            '        PrintInvoice.Append(vbCrLf & "Header Information is Welcome" & vbCrLf)
            '    End If
            '    If vIsPrintOfficialAddressAllowed = False Then
            '        If Not (drSite Is Nothing) Then
            '            PrintInvoice.Append(drSite.Item("SiteOfficialName") & "  (" & vSiteCode & ") " & vbCrLf)
            '            PrintInvoice.Append(drSite.Item("SiteAddressLn1") & vbCrLf & _
            '                                drSite.Item("SiteAddressLn2") & vbCrLf & _
            '                                drSite.Item("SiteAddressLn3") & vbCrLf & _
            '                                drSite.Item("SitePinCode") & vbCrLf)
            '        End If
            '    Else
            '        PrintInvoice.Append(vbCrLf & "Print Official Address " & vbCrLf)
            '    End If

            '    PrintInvoice.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)
            '    PrintInvoice.Append("                   Sales Invoice No  - " & CtrlSalesInfo1.CtrlTxtOrderNo.Text & vbCrLf & vbCrLf)

            '    PrintInvoice.Append("Cashier Name  : " & vUserName & vbTab & vbTab & vbTab & "Invoice Date : " & Format(vCurrentDate.Date, vDateFormat) & vbCrLf)
            '    PrintInvoice.Append("Sales Order   : " & CtrlSalesInfo1.CtrlTxtOrderNo.Text & "   		Sales Date   : " & Format(vCurrentDate.Date, vDateFormat) & vbCrLf)
            '    PrintInvoice.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)

            '    PrintInvoice.Append("Customer Name    : " & CtrlCustDtls1.lblCustNameValue.Text.Trim & vbCrLf)
            '    PrintInvoice.Append("Home Address     : " & drHAdds.Item("Address").ToString & vbCrLf)
            '    PrintInvoice.Append("                 : " & drHAdds.Item("City") & ", " & drHAdds.Item("State") & ", " & drHAdds.Item("Country") & ", " & drHAdds.Item("PinCode") & vbCrLf)
            '    PrintInvoice.Append("Phone            : " & drHAdds.Item("ResPhone") & vbCrLf)
            '    PrintInvoice.Append("Email            : " & drHAdds.Item("EmailId") & vbCrLf & vbCrLf)

            '    PrintInvoice.Append("Delivery Address : " & CtrlCustDtls1.lblAddressValue.Text & vbCrLf)
            '    PrintInvoice.Append("Phone         	 : " & CtrlCustDtls1.lblTelNoValue.Text & vbCrLf)
            '    PrintInvoice.Append("Email            : " & CtrlCustDtls1.lblEmailIdValue.Text & vbCrLf & vbCrLf)

            '    PrintInvoice.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)
            '    PrintInvoice.Append("Item Code       Item Desc                               Qty       Price    Disc%        Tax%  NetAmt " & vbCrLf)
            '    PrintInvoice.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)


            '    For Each drDtl As DataRow In dsScan.Tables("ItemScanDetails").Rows
            '        PrintInvoice.Append(drDtl("ArticleCode") & Space(16 - drDtl("ArticleCode").ToString.Length) & _
            '                     drDtl("Discription") & Space(40 - drDtl("Discription").ToString.Length) & _
            '                     drDtl("Quantity") & Space(10 - drDtl("Quantity").ToString.Length) & _
            '                     Format(drDtl("SellingPrice"), "0.0") & Space(10 - drDtl("SellingPrice").ToString.Length) & _
            '                     drDtl("Discount") & Space(13 - drDtl("Discount").ToString.Length) & _
            '                     IIf(drDtl("ExclTaxAmt") Is DBNull.Value, "0", drDtl("ExclTaxAmt")) & Space(5 - drDtl("ExclTaxAmt").ToString.Length) & Format(drDtl("NetAmount"), "0.0") & vbCrLf)
            '    Next

            'For Each drDtl As DataRow In dsScan.Tables("ItemScanDetails").Rows
            '    'PrintInvoice.Append(drDtl("ArticleCode") & Space(16 - drDtl("ArticleCode").ToString.Length) & _
            '    '             drDtl("Discription") & Space(40 - drDtl("Discription").ToString.Length) & _
            '    '             drDtl("Quantity") & Space(10 - drDtl("Quantity").ToString.Length) & _
            '    '             Format(drDtl("SellingPrice"), "0.0") & Space(10 - drDtl("SellingPrice").ToString.Length) & _
            '    '             drDtl("Discount") & Space(13 - drDtl("Discount").ToString.Length) & _
            '    '             IIf(drDtl("ExclTaxAmt") Is DBNull.Value, "0", drDtl("ExclTaxAmt")) & Space(5 - drDtl("ExclTaxAmt").ToString.Length) & Format(drDtl("NetAmount"), "0.0") & vbCrLf)
            '    PrintInvoice.Append(drDtl("ArticleCode").ToString.PadRight(16) & _
            '                        drDtl("Discription").ToString.PadRight(40) & _
            '                        drDtl("Quantity").ToString.PadRight(8) & _
            '                        Format(drDtl("SellingPrice"), "0.0").ToString.PadRight(8) & _
            '                        drDtl("Discount").ToString.PadRight(10) & _
            '                        IIf(drDtl("ExclTaxAmt") Is DBNull.Value, "0      ", drDtl("ExclTaxAmt").ToString.PadRight(8)) & _
            '                         Format(drDtl("NetAmount"), "0.0").ToString.PadRight(10) & vbCrLf)


            'Next


            '    PrintInvoice.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)

            '    'PrintInvoice.Append("Total Qty   : " & lblOrderQty.Text & vbCrLf)
            '    'PrintInvoice.Append("Gross Amt   : " & CtrlCashSummary1.lbltxt1 & vbCrLf)
            '    'PrintInvoice.Append("Disc  Amt   : " & CtrlCashSummary1.CtrllblTaxAmt.Text & vbCrLf)
            '    'PrintInvoice.Append("Incl. Amt   : " & Format(IIf(dsScan.Tables("ItemScanDetails").Compute("Sum(IncTaxAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("Sum(IncTaxAmt)", ""))), "0.00" & vbCrLf)
            '    'PrintInvoice.Append("Excl. Amt   : " & Format(IIf(dsScan.Tables("ItemScanDetails").Compute("Sum(IncTaxAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("Sum(IncTaxAmt)", ""))), "0.00" & vbCrLf)
            '    'PrintInvoice.Append("Net   Amt   : " & CtrlCashSummary1.lbltxt4 & vbCrLf & vbCrLf)

            '    PrintInvoice.Append("Total To Pay									:  " & CtrlCashSummary1.lbltxt4 & vbCrLf & vbCrLf)

            '    PrintInvoice.Append("Gross Total 									:  " & CtrlCashSummary1.lbltxt1 & vbCrLf)
            '    PrintInvoice.Append("Discount    									:  " & CtrlCashSummary1.CtrllblTaxAmt.Text & vbCrLf)
            '    PrintInvoice.Append("									-------------------" & vbCrLf)
            '    PrintInvoice.Append("Net Total   									:  " & CtrlCashSummary1.lbltxt4 & vbCrLf & vbCrLf)

            '    If dsMain.Tables("SalesInvoice").Rows.Count > 0 Then
            '        For Each drPayment As DataRow In dsMain.Tables("SalesInvoice").Rows
            '            PrintInvoice.Append("Advance Payd				" & Format(drPayment("SOInvDate"), vDateFormat) & "				:  " & drPayment("AmountTendered") & vbCrLf)
            '        Next

            '        PrintInvoice.Append("									-------------------" & vbCrLf)
            '    End If

            '    Dim vReceivedAmount As Double = Math.Round(dsPayment.Tables("MSTRecieptType").Compute("Sum(Amount)", ""), 2)

            '    PrintInvoice.Append("Payd on this receipt:								:  " & vReceivedAmount.ToString("0.00") & vbCrLf)
            '    PrintInvoice.Append("									-------------------" & vbCrLf)
            '    PrintInvoice.Append("Balance to pay									:  " & Format(CDbl(CtrlCashSummary1.lbltxt4) - vReceivedAmount, "0.00") & vbCrLf & vbCrLf & vbCrLf)

            '    'If dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
            '    '    For Each drPayment As DataRow In dsPayment.Tables("MSTRecieptType").Rows
            '    '        PrintInvoice.Append("Advance Payd				" & Format(vCurrentDate, vDateFormat) & "				:  " & drPayment("Amount") & vbCrLf)
            '    '    Next
            '    'End If

            '    'PrintInvoice.Append(vbCrLf & "Total Paid Amount:" & vbCrLf)
            '    'PrintInvoice.Append("Tender      :" & vbCrLf)
            '    'If dsMain.Tables("SalesInvoice").Rows.Count > 0 Then
            '    '    PrintInvoice.Append("Tender Info :" & dsMain.Tables("SalesInvoice").Compute("Sum(AmountTendered)", "") & "  Date : " & dsMain.Tables("SalesInvoice").Rows(0).Item("CREATEDON") & vbCrLf)
            '    '    PrintInvoice.Append("Tender Info :" & vbCrLf)
            '    'Else
            '    '    PrintInvoice.Append("Tender Info :" & CtrlCashSummary1.lbltxt5 & vbCrLf)
            '    'End If

            '    If vIsPrintingTaxInfoAllowed = True Then
            '        PrintInvoice.Append("Print Tax Details..............." & vbCrLf & vbCrLf)
            '    End If

            '    'PrintInvoice.Append("<Terms & Condition>" & vbCrLf & vbCrLf)

            '    'PrintInvoice.Append("Authorized Sign:...............            Customer Sign:................" & vbCrLf)

            '    If vIsPromotionalMessageAllowed = True Then
            '        PrintInvoice.Append(vbCrLf & "Promotional Message is Welcome" & vbCrLf)
            '    End If
            '    If vFooterNote = True Then
            '        PrintInvoice.Append(vbCrLf & "Footer Information is Welcome" & vbCrLf)
            '    End If

            '    If vIsPrintPreviewAllowed = True Then
            '        fnPrint(PrintInvoice.ToString, "")          'Print Preview
            '    End If
            '    fnPrint(PrintInvoice.ToString, "PRN")       'Direct Print

            '    'PrintInvoice.Append("Print")                'Set Debug Point
            'Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Payment, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserCode, vSalesInvcNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo1.CtrlTxtCustOrdRef.Text)
            ' Dim strRemark As String = CtrlSalesInfo1.CtrlTxtRemarks.Text

            Dim dsOtherCharges As New DataSet
            Dim dt As DataTable = dtOtherCharges.Copy()
            dt.TableName = "NewOtherCharges"
            dsOtherCharges.Tables.Add(dt)
            'Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Payment, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserCode, CtrlSalesInfo1.CtrlTxtOrderNo.Text, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo1.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, "Open", dsOtherCharges, "", dtSalesOrderTaxDetails, ShowFullName:=clsDefaultConfiguration.PrintItemFullName, dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl)
            ' Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Payment, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserCode, CtrlSalesInfo1.CtrlTxtOrderNo.Text, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo1.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, strSOStatus, dsOtherCharges, "", dtSalesOrderTaxDetails, ShowFullName:=clsDefaultConfiguration.PrintItemFullName, dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, SalesOrderDeliveryDate:=vSalesOrderExpectedDeliveryDate)

        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Function
    Private Function PrintSalesOrdersInvoiceBackup(ByVal drSite As DataRow, ByVal drHAdds As DataRow, Optional ByVal drDAdds As DataRow = Nothing) As Boolean
        'Dim PrintInvoice As New System.Text.StringBuilder

        'Try
        '    If dsScan Is Nothing Then
        '        Exit Function
        '    End If

        '    PrintInvoice.Length = 0
        '    PrintInvoice.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)
        '    PrintInvoice.Append("                          SALES INVOICE                                 " & vbCrLf)
        '    PrintInvoice.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)

        '    If vHeaderNote = True Then
        '        PrintInvoice.Append(vbCrLf & "Header Information is Welcome" & vbCrLf)
        '    End If

        '    PrintInvoice.Append("Company Name   : " & "CreativeIT India Ltd" & "             Company Code : " & "CRITI02" & vbCrLf)

        '    If vIsPrintOfficialAddressAllowed = False Then
        '        If Not (drSite Is Nothing) Then
        '            PrintInvoice.Append("Store Name     : " & drSite.Item("SiteOfficialName") & "                Store Code   : " & vSiteCode & vbCrLf)
        '            PrintInvoice.Append("Store Address  : " & drSite.Item("SiteAddressLn1") & vbCrLf & "                 " & _
        '                         drSite.Item("SiteAddressLn2") & " " & drSite.Item("SiteAddressLn3") & _
        '                         drSite.Item("SitePinCode") & vbCrLf)
        '        End If
        '    Else
        '        PrintInvoice.Append(vbCrLf & "Print Official Address " & vbCrLf)
        '    End If

        '    PrintInvoice.Append(vbCrLf & "----------------------------------------------------------------------------------------------------" & vbCrLf)
        '    PrintInvoice.Append("Sales Invoice No        : " & CtrlSalesInfo1.CtrlTxtOrderNo.Text & "  Reference Sales Order : " & CtrlSalesInfo1.CtrlTxtOrderNo.Text & "   Date : " & Format(vCurrentDate.Date, vDateFormat) & vbCrLf)
        '    PrintInvoice.Append("Expected Delivery Date  : " & Format(CtrlSalesInfo1.CtrlDtExpDelDate.Value, vDateFormat) & vbCrLf)
        '    PrintInvoice.Append("Cashier Name            : " & vUserName & vbCrLf & vbCrLf)

        '    PrintInvoice.Append("Customer Name    : " & IIf(CtrlSalesInfo1.CtrlTxtInvoice.Text.Trim = "", CtrlCustDtls1.lblCustNameValue.Text.Trim, CtrlSalesInfo1.CtrlTxtInvoice.Text.Trim) & "          Customer Code : " & CtrlCustSearch1.CtrlTxtCustNo.Text & vbCrLf)

        '    PrintInvoice.Append("Home Address     : " & drHAdds.Item("Address").ToString & vbCrLf)
        '    PrintInvoice.Append("                 : " & drHAdds.Item("City") & ", " & drHAdds.Item("State") & ", " & drHAdds.Item("Country") & ", " & drHAdds.Item("PinCode") & vbCrLf & vbCrLf)

        '    If drDelvAdds Is Nothing Then
        '        PrintInvoice.Append("Delivery Address : " & CtrlCustDtls1.lblAddressValue.Text & vbCrLf)
        '        PrintInvoice.Append("Tel. No.  	 : " & CtrlCustDtls1.lblTelNoValue.Text & vbCrLf & vbCrLf)
        '    Else
        '        PrintInvoice.Append("Delivery Address : " & drDAdds.Item("Address").ToString & vbCrLf)
        '        PrintInvoice.Append("                 : " & drDAdds.Item("City") & ", " & drDAdds.Item("State") & ", " & drDAdds.Item("Country") & ", " & drDAdds.Item("PinCode") & vbCrLf)
        '        PrintInvoice.Append("Tel. No.  	 : " & drDAdds.Item("OfficeNo") & vbCrLf & vbCrLf)
        '    End If

        '    PrintInvoice.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)
        '    PrintInvoice.Append("Item Code       Item Desc                               Qty       Price    Disc%        Tax%  NetAmt " & vbCrLf)
        '    PrintInvoice.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)

        '    For Each drDtl As DataRow In dsScan.Tables("ItemScanDetails").Rows
        '        PrintInvoice.Append(drDtl("ArticleCode") & Space(16 - drDtl("ArticleCode").ToString.Length) & _
        '                     drDtl("Discription") & Space(40 - drDtl("Discription").ToString.Length) & _
        '                     drDtl("Quantity") & Space(10 - drDtl("Quantity").ToString.Length) & _
        '                     Format(drDtl("SellingPrice"), "0.0") & Space(10 - drDtl("SellingPrice").ToString.Length) & _
        '                     drDtl("Discount") & Space(13 - drDtl("Discount").ToString.Length) & _
        '                     IIf(drDtl("ExclTaxAmt") Is DBNull.Value, "0", drDtl("ExclTaxAmt")) & Space(5 - drDtl("ExclTaxAmt").ToString.Length) & FormatNumber(drDtl("NetAmount"), 1) & vbCrLf)
        '    Next

        '    PrintInvoice.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)

        '    PrintInvoice.Append("Total Qty   : " & lblOrderQty.Text & vbCrLf)
        '    'PrintInvoice.Append("Gross Amt   : " & CtrlCashSummary1.lbltxt1 & vbCrLf)
        '    'PrintInvoice.Append("Disc  Amt   : " & CtrlCashSummary1.CtrllblTaxAmt.Text & vbCrLf)
        '    'PrintInvoice.Append("Incl. Amt   : " & FormatNumber(IIf(dsScan.Tables("ItemScanDetails").Compute("Sum(IncTaxAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("Sum(IncTaxAmt)", ""))), strZero & vbCrLf)
        '    'PrintInvoice.Append("Excl. Amt   : " & FormatNumber(IIf(dsScan.Tables("ItemScanDetails").Compute("Sum(IncTaxAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("Sum(IncTaxAmt)", ""))), strZero & vbCrLf)
        '    'PrintInvoice.Append("Net   Amt   : " & CtrlCashSummary1.lbltxt4 & vbCrLf & vbCrLf)

        '    'PrintInvoice.Append("Payment Received :" & vbCrLf)
        '    'PrintInvoice.Append("Minimum Advance To Pay: (INR)  " & CtrlCashSummary1.lbltxt5 & vbCrLf & vbCrLf)

        '    PrintInvoice.Append("Advance Amount Paid :" & vbCrLf)


        '    If dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
        '        Dim RowIndexPayment As Integer = 0
        '        For Each drPayment As DataRow In dsPayment.Tables("MSTRecieptType").Rows
        '            PrintInvoice.Append("Tender      :" & drPayment("RecieptType").ToString() & vbCrLf)
        '            If RowIndexPayment = 0 Then
        '                PrintInvoice.Append("Tender Info :" & drPayment("Amount") & "  Date : " & Format(vCurrentDate, vDateFormat) & vbCrLf)
        '            End If
        '            If RowIndexPayment > 0 Then
        '                PrintInvoice.Append("Tender Info :" & drPayment("Amount") & vbCrLf)
        '            End If
        '            RowIndexPayment += 1
        '        Next

        '    End If

        '    PrintInvoice.Append(vbCrLf & "Total Paid Amount:" & vbCrLf)
        '    PrintInvoice.Append("Tender      :" & vbCrLf)
        '    If dsMain.Tables("SalesInvoice").Rows.Count > 0 Then
        '        PrintInvoice.Append("Tender Info :" & dsMain.Tables("SalesInvoice").Compute("Sum(AmountTendered)", "") & "  Date : " & dsMain.Tables("SalesInvoice").Rows(0).Item("CREATEDON") & vbCrLf)
        '        PrintInvoice.Append("Tender Info :" & vbCrLf)
        '    Else
        '        PrintInvoice.Append("Tender Info :" & CtrlCashSummary1.lbltxt5 & vbCrLf)
        '    End If

        '    If vIsPrintingTaxInfoAllowed = True Then
        '        PrintInvoice.Append("Print Tax Details..............." & vbCrLf & vbCrLf)
        '    End If

        '    PrintInvoice.Append("Balance Amount Due: " & CtrlCashSummary1.lbltxt7 & vbCrLf & vbCrLf)

        '    PrintInvoice.Append("<Terms & Condition>" & vbCrLf & vbCrLf)

        '    PrintInvoice.Append("Authorized Sign:...............            Customer Sign:................" & vbCrLf)

        '    If vIsPromotionalMessageAllowed = True Then
        '        PrintInvoice.Append(vbCrLf & "Promotional Message is Welcome" & vbCrLf)
        '    End If
        '    If vFooterNote = True Then
        '        PrintInvoice.Append(vbCrLf & "Footer Information is Welcome" & vbCrLf)
        '    End If

        '    If vIsPrintPreviewAllowed = True Then
        '        fnPrint(PrintInvoice.ToString, "")          'Print Preview
        '    End If
        '    fnPrint(PrintInvoice.ToString, "PRN")       'Direct Print

        '    'PrintInvoice.Append("Print")                'Set Debug Point

        'Catch ex As Exception
        '    ShowMessage(ex.Message, getValueByKey("CLAE05"))
        'End Try

    End Function

    ''' <summary>
    ''' Print Outbound Delivery for Sales Order
    ''' </summary>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PrintSalesOrdersDelivery(ByVal drSite As DataRow, ByVal drHAdds As DataRow, Optional ByVal drDAdds As DataRow = Nothing) As Boolean
        'Dim PrintDeliveryInvc As New System.Text.StringBuilder
        Try
            '    If dsScan Is Nothing Then
            '        Exit Function
            '    End If
            '    PrintDeliveryInvc.Length = 0
            '    PrintDeliveryInvc.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)
            '    PrintDeliveryInvc.Append("                          SALES OUTBOUND DELIVERY                       " & vbCrLf)
            '    PrintDeliveryInvc.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)
            '    If vHeaderNote = True Then
            '        PrintDeliveryInvc.Append(vbCrLf & "Header Information is Welcome" & vbCrLf)
            '    End If
            '    PrintDeliveryInvc.Append("Company Name   : " & "CreativeIT India Ltd" & "     Company Code : " & "CRITI02" & vbCrLf)
            '    If vIsPrintOfficialAddressAllowed = False Then
            '        If Not (drSite Is Nothing) Then
            '            PrintDeliveryInvc.Append("Store Name         : " & drSite.Item("SiteOfficialName") & "                Store Code   : " & vSiteCode & vbCrLf)
            '            PrintDeliveryInvc.Append("Store Address       : " & drSite.Item("SiteAddressLn1") & vbCrLf & "                            " & _
            '                         drSite.Item("SiteAddressLn2") & " " & drSite.Item("SiteAddressLn3") & _
            '                         drSite.Item("SitePinCode") & vbCrLf)
            '        End If
            '    Else
            '        PrintDeliveryInvc.Append(vbCrLf & "Print Official Address " & vbCrLf)
            '    End If

            '    PrintDeliveryInvc.Append(vbCrLf & "----------------------------------------------------------------------------------------------------" & vbCrLf)
            '    PrintDeliveryInvc.Append("Sales Invoice No        : " & vSalesInvcNo & "  Reference Sales Order : " & CtrlSalesInfo1.CtrlTxtOrderNo.Text & "   Date : " & Format(vCurrentDate, vDateFormat) & vbCrLf)
            '    PrintDeliveryInvc.Append("Expected Delivery Date  : " & Format(CtrlSalesInfo1.CtrlDtExpDelDate.Value, vDateFormat) & vbCrLf)
            '    PrintDeliveryInvc.Append("Cashier Name            : " & vUserName & vbCrLf & vbCrLf)

            '    PrintDeliveryInvc.Append("Customer Name    : " & CtrlCustDtls1.lblCustNameValue.Text & "          Customer Code : " & CtrlCustSearch1.CtrlTxtCustNo.Text & vbCrLf)

            '    PrintDeliveryInvc.Append("Home Address       : " & drHAdds.Item("Address").ToString & vbCrLf)
            '    PrintDeliveryInvc.Append("                   : " & drHAdds.Item("City") & ", " & drHAdds.Item("State") & ", " & drHAdds.Item("Country") & ", " & drHAdds.Item("PinCode") & vbCrLf & vbCrLf)

            '    If drDelvAdds Is Nothing Then
            '        PrintDeliveryInvc.Append("Delivery Address : " & CtrlCustDtls1.lblAddressValue.Text)
            '        PrintDeliveryInvc.Append("Tel. No.  	   : " & CtrlCustDtls1.lblTelNoValue.Text & vbCrLf & vbCrLf)
            '    Else
            '        PrintDeliveryInvc.Append("Delivery Address : " & drDAdds.Item("Address").ToString & vbCrLf)
            '        PrintDeliveryInvc.Append("                 : " & drDAdds.Item("City") & ", " & drDAdds.Item("State") & ", " & drDAdds.Item("Country") & ", " & drDAdds.Item("PinCode") & vbCrLf)
            '        PrintDeliveryInvc.Append("Tel. No.  	   : " & drDAdds.Item("OfficeNo") & vbCrLf & vbCrLf)
            '    End If

            '    PrintDeliveryInvc.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)
            '    PrintDeliveryInvc.Append("Item Code       Item Desc                               Qty       Price      Disc%  Tax%   NetAmt" & vbCrLf)
            '    PrintDeliveryInvc.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)

            '    Dim vNetAmount As Double = 0.0
            '    Dim vDiscount As Double = 0.0
            '    Dim vExclTaxAmt As Double = 0.0

            '    For Each drDtl As DataRow In dsScan.Tables("ItemScanDetails").Select("PickUpQty > 0")

            '        vDiscount = drDtl("PickUpQty") * (IIf(drDtl("LineDiscount") Is DBNull.Value, 0, drDtl("LineDiscount")) / drDtl("Quantity"))
            '        vExclTaxAmt = drDtl("PickUpQty") * (IIf(drDtl("ExclTaxAmt") Is DBNull.Value, 0, drDtl("ExclTaxAmt")) / drDtl("Quantity"))
            '        vNetAmount = (drDtl("PickUpQty") * drDtl("SellingPrice")) + vExclTaxAmt - vDiscount

            '        PrintDeliveryInvc.Append(drDtl("ArticleCode") & Space(16 - drDtl("ArticleCode").ToString.Length) & _
            '                     drDtl("Discription") & Space(40 - drDtl("Discription").ToString.Length) & _
            '                     drDtl("PickUpQty") & Space(10 - drDtl("PickUpQty").ToString.Length) & _
            '                     Format(drDtl("SellingPrice"), "0.0") & Space(10 - drDtl("SellingPrice").ToString.Length) & _
            '                     drDtl("Discount") & Space(10 - drDtl("Discount").ToString.Length) & _
            '                     drDtl("ExclTaxAmt") & Space(10 - drDtl("ExclTaxAmt").ToString.Length) & Format(vNetAmount, "0.0") & vbCrLf)
            '    Next
            '    PrintDeliveryInvc.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)

            '    PrintDeliveryInvc.Append("Total Qty : " & lblOrderQty.Text & vbCrLf)
            '    PrintDeliveryInvc.Append("PickUpQty : " & lblPickupQty.Text & vbCrLf)
            '    PrintDeliveryInvc.Append("Gross Amt : " & CtrlCashSummary1.lbltxt1 & vbCrLf)
            '    PrintDeliveryInvc.Append("Disc  Amt : " & CtrlCashSummary1.CtrllblTaxAmt.Text & vbCrLf)
            '    PrintDeliveryInvc.Append("Incl. Amt : " & dsScan.Tables("ItemScanDetails").Compute("SUM(IncTaxAmt)", "") & vbCrLf)
            '    PrintDeliveryInvc.Append("Excl. Amt : " & dsScan.Tables("ItemScanDetails").Compute("SUM(ExclTaxAmt)", "") & vbCrLf)
            '    PrintDeliveryInvc.Append("Net   Amt : " & CtrlCashSummary1.lbltxt4 & vbCrLf & vbCrLf)

            '    PrintDeliveryInvc.Append("Payment Received :" & vbCrLf)
            '    PrintDeliveryInvc.Append("Minimum Advance To Pay: (INR)  " & CtrlCashSummary1.lbltxt5 & vbCrLf & vbCrLf)

            '    PrintDeliveryInvc.Append("Advance Amount Paid :" & vbCrLf)
            '    PrintDeliveryInvc.Append("Tender      :" & vbCrLf)

            '    If dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
            '        Dim RowIndexPayment As Integer = 0
            '        For Each drPayment As DataRow In dsPayment.Tables("MSTRecieptType").Rows
            '            If RowIndexPayment = 0 Then
            '                PrintDeliveryInvc.Append("Tender Info :" & drPayment("Amount") & "  Date : " & vCurrentDate & vbCrLf)
            '            End If
            '            If RowIndexPayment > 0 Then
            '                PrintDeliveryInvc.Append("Tender Info :" & drPayment("Amount") & vbCrLf)
            '            End If
            '            RowIndexPayment += 1
            '        Next

            '    End If

            '    PrintDeliveryInvc.Append(vbCrLf & "Total Paid Amount:" & vbCrLf)
            '    PrintDeliveryInvc.Append("Tender      :" & vbCrLf)
            '    If dsMain.Tables("SalesInvoice").Rows.Count > 0 Then
            '        PrintDeliveryInvc.Append("Tender Info :" & dsMain.Tables("SalesInvoice").Compute("Sum(AmountTendered)", "") & "  Date : " & dsMain.Tables("SalesInvoice").Rows(0).Item("CREATEDON") & vbCrLf)
            '        PrintDeliveryInvc.Append("Tender Info :" & vbCrLf)
            '    Else
            '        PrintDeliveryInvc.Append("Tender Info :" & CtrlCashSummary1.lbltxt5 & vbCrLf)
            '    End If

            '    If vIsPrintingTaxInfoAllowed = True Then
            '        PrintDeliveryInvc.Append("Print Tax Details..............." & vbCrLf & vbCrLf)
            '    End If

            '    PrintDeliveryInvc.Append("Balance Amount Due: " & CtrlCashSummary1.lbltxt7 & vbCrLf & vbCrLf)

            '    PrintDeliveryInvc.Append("<Terms & Condition>" & vbCrLf & vbCrLf)

            '    PrintDeliveryInvc.Append("Authorized Sign:..............." & vbTab & "Customer Sign:................")

            '    If vIsPromotionalMessageAllowed = True Then
            '        PrintDeliveryInvc.Append(vbCrLf & "Promotional Message is Welcome" & vbCrLf)
            '    End If
            '    If vFooterNote = True Then
            '        PrintDeliveryInvc.Append(vbCrLf & "Footer Information is Welcome" & vbCrLf)
            '    End If

            '    If vIsPrintPreviewAllowed = True Then
            '        fnPrint(PrintDeliveryInvc.ToString, "")          'Print Preview
            '    End If
            '    fnPrint(PrintDeliveryInvc.ToString, "PRN")       'Direct Print

            'PrintDeliveryInvc.Append("Print")                'Set Debug Point

            'Dim dtSalesItem As DataTable = dsScan.Tables("ItemScanDetails").Select("PickUpQty > 0")
            'Dim strRemark As String = CtrlSalesInfo1.CtrlTxtRemarks.Text
            'SalesPersonName = IIf(CtrlSalesPersons.CtrlSalesPersons.SelectedIndex < 0, "", CtrlSalesPersons.CtrlSalesPersons.Text)
            'Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.DeliveryNote, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, CtrlSalesInfo1.CtrlTxtOrderNo.Text, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), Nothing, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, ShowFullName:=clsDefaultConfiguration.PrintItemFullName, strReturnReason:=strRemark, dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, SalesOrderDeliveryDate:=vSalesOrderExpectedDeliveryDate, strSalesPerson:=SalesPersonName)

        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

#End Region

#Region "Common Button Action"
    ''' <summary>
    ''' Set Enable/Disable Property to Standerd Button
    ''' </summary>
    ''' <param name="IsEnable">True/False</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function EnableButton(ByVal IsEnable As Boolean) As Boolean
        CtrlSalesPersons.CtrlCmdSearch.Enabled = True
        btnSOSave.Enabled = IsEnable
        'rbGrpCM.Enabled = IsEnable
        CtrlbtnSOOtherCharges.Enabled = True
        BtnSOAcceptPayment.Enabled = IsEnable
    End Function

    ''' <summary>
    ''' Reset Sales Order form for New Sales Order
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 

    Private Sub rbbtnSONew_Click(sender As Object, e As EventArgs) Handles rbbtnSONew.Click
        Try
            Try
                If dsScan.Tables(0).Rows.Count > 0 Then
                    If MsgBox(getValueByKey("SO057"), MsgBoxStyle.YesNo, "SO057 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                        If Me.Name <> "frmPCSalesOrderCreation" Then
                            Dim frm As New frmPCSalesOrderCreation
                            frm.IsBooking = False
                            MDISpectrum.ShowChildForm(frm, True)
                        Else
                            ResetSalesOrder()
                            BtnSONew_Click(sender, e)
                            IsBooking = False
                            Call SetScreenAsBooking()
                            dtSTRFactoryRemark = objPCSO.GetFactoryRemarks(CtrlTxtOrderNo.Text)
                        End If
                    End If
                Else
                    If Me.Name <> "frmPCSalesOrderCreation" Then
                        Dim frm As New frmPCSalesOrderCreation
                        frm.IsBooking = False
                        MDISpectrum.ShowChildForm(frm, True)
                    Else
                        ResetSalesOrder()
                        BtnSONew_Click(sender, e)
                        IsBooking = False
                        Call SetScreenAsBooking()
                        dtSTRFactoryRemark = objPCSO.GetFactoryRemarks(CtrlTxtOrderNo.Text)
                    End If
                End If
            Catch ex As Exception
                LogException(ex)
            End Try

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Private Sub BtnSONew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles rbbtnSONew.Click '  BtnSONew.Click
        If rbbtnSONew.Tag = "NEW" Then
            Try
                EnableButton(True)
                rbbtnSONew.Tag = "Cancel"
                'ResetSalesOrder()
                CtrlTxtOrderNo.Text = "SO" & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2) & "-" & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Length - 3) & "-" & objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode)
                'dtpExpDeliveryDate.Value = consDeveliveryDt
                'CtrlSalesPersons.CtrlTxtBox.Select()
                CtrlSalesPersons.AndroidSearchTextBox.Select()
                If grdScanItem.Rows.Count > 1 Then
                    If MsgBox(getValueByKey("SO057"), MsgBoxStyle.YesNo, "SO057 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                        EnableButton(False)
                        rbbtnSONew.Tag = "NEW"
                        CtrlTxtOrderNo.Text = 0 'CtrlSalesInfo1.CtrlTxtOrderNo.Text = 0
                        'dtpExpDeliveryDate.Value = consDeveliveryDt
                        ResetSalesOrder()
                        dtSTRFactoryRemark = objPCSO.GetFactoryRemarks(CtrlTxtOrderNo.Text)
                    Else
                        rbbtnSONew.Tag = "NEW"
                    End If
                Else
                    EnableButton(True)
                    rbbtnSONew.Tag = "NEW"
                    CtrlTxtOrderNo.Text = CtrlTxtOrderNo.Text
                    'dtpExpDeliveryDate.Value = consDeveliveryDt
                    ResetSalesOrder()
                    dtSTRFactoryRemark = objPCSO.GetFactoryRemarks(CtrlTxtOrderNo.Text)
                End If
                IsCSTApplicable = False
                rbnCST.Enabled = True
                clsDefaultConfiguration.CSTTaxCode = ""
                IsApplyPromotion = False
                GetNewSalesOrderNumber()

                DtSoBulkComboHdr.Rows.Clear()
                DtSoBulkComboDtl.Rows.Clear()

                'If OnlineConnect = True Then
                '    Try
                '        CtrlSalesInfo1.CtrlTxtOrderNo.Text = GenDocNo("SO" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode))
                '    Catch ex As Exception
                '        CtrlSalesInfo1.CtrlTxtOrderNo.Text = "SO" & clsAdmin.TerminalID & objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode)
                '    End Try
                'Else
                '    Try
                '        CtrlSalesInfo1.CtrlTxtOrderNo.Text = GenDocNo("OSO" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode))
                '    Catch ex As Exception
                '        CtrlSalesInfo1.CtrlTxtOrderNo.Text = "OSO" & clsAdmin.TerminalID & objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode)
                '    End Try
                'End If

            Catch ex As Exception
                LogException(ex)
                ShowMessage(ex.Message, getValueByKey("CLAE05"))
            End Try

        ElseIf rbbtnSONew.Tag.Trim = "Cancel" Then
            Try
                If Not (dsScan.Tables(0).Rows.Count > 0) Then
                    'ShowMessage(getValueByKey("SO012"), "SO012")
                    'ShowMessage("Please Scan Articles first", "Sales Order Information")

                End If
                'If grdScanItem.Rows.Count > 1 Then
                '    If MsgBox(getValueByKey("SO057"), MsgBoxStyle.YesNo, "SO057 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                '        EnableButton(False)
                '        rbbtnSONew.Tag = "NEW"
                '        CtrlSalesInfo1.CtrlTxtOrderNo.Text = 0
                '        'dtpExpDeliveryDate.Value = consDeveliveryDt
                '        ResetSalesOrder()
                '    End If
                'Else
                '    EnableButton(True)
                '    rbbtnSONew.Tag = "NEW"
                '    CtrlSalesInfo1.CtrlTxtOrderNo.Text = 0
                '    'dtpExpDeliveryDate.Value = consDeveliveryDt
                '    ResetSalesOrder()
                'End If

            Catch ex As Exception
                ShowMessage(ex.Message, getValueByKey("CLAE05"))
                LogException(ex)
            End Try
        End If
        If dgDeliveryLocation.Rows.Count > 1 Then
            dgDeliveryLocation.Rows.RemoveRange(1, dgDeliveryLocation.Rows.Count - 1)
        End If
        tabSalesOrder.SelectedIndex = 0
    End Sub

    ''' <summary>
    ''' Add Other Charges and Tax in Sales Order
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnAddOtherCharges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Handles CtrlbtnSOOtherCharges.Click
        Try
            If Not (dsScan.Tables(0).Rows.Count > 0) Then
                ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE042"))
                'ShowMessage("Please Scan Article first", "Sales Order Information")
                Exit Sub
            End If

            If dsScan.Tables(0).Rows.Count > 0 Then
                Dim objOpenAddOtherCharges As New frmNAddOthrChrgForSO
                If Not dtOtherCharges Is Nothing AndAlso dtOtherCharges.Rows.Count > 0 Then
                    objOpenAddOtherCharges.dtOtherCharge = dtOtherCharges
                End If

                objOpenAddOtherCharges.ShowDialog()
                If objOpenAddOtherCharges.CancelOthercharges = True Then
                    Exit Sub
                End If
                dtOtherCharges = objOpenAddOtherCharges.dtOtherCharge
            Else
                ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                'ShowMessage("Please Scan Article", "Sales Order Information")
            End If
            CalculateSalesOrderSummary(dsScan)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Accept Payment for current Sales Order
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnAcceptPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles BtnSOAcceptPayment.Click
        Try
            If Not isValidData() Then
                Exit Sub
            End If
            grdScanItem.FinishEditing()

            If CtrltxrCust.Text = String.Empty Then
                ShowMessage(getValueByKey("SO048"), "SO048 - " & getValueByKey("CLAE04"))
                CtrltxrCust.Select()
                Exit Sub
            End If

            If Not (dsScan.Tables(0).Rows.Count > 0) Then
                'ShowMessage("Please Scan Article first", "Sales Order Information")
                ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                Exit Sub
                'Rakesh-20.Dec.2013-As per Rama's suggestion
                'ElseIf CDbl(CtrlCashSummary1.lbltxt4) <= Decimal.Zero Then
                '    ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                '    Exit Sub
            End If

            'Dim obj As New frmSpecialPrompt("How much you want to pay")
            'obj.ShowTextBox = True
            'obj.ShowDialog()

            'New form for accepting payment in sales order.
            If IsBtnSave Then
                rbbtnDefaultPromo_Click(rbbtnDefaultPromo, New System.EventArgs)
            End If

            'IsBtnSave = False
            Dim Obj As New frmNHowMuchtoPay
            Obj.CtrlTxtMinAmt.Text = CDbl(CtrllblMinAdv.Text)
            Obj.CtrlTxtPickAmt.Text = Math.Round(IIf(dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(TotalPickUpAmt)", "") Is DBNull.Value, 0, dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(TotalPickUpAmt)", "")), 2)
            If CDbl(Obj.CtrlTxtPickAmt.Text) < CDbl(Obj.CtrlTxtMinAmt.Text) Then
                Obj.CtrlTxtPickAmt.Text = Obj.CtrlTxtMinAmt.Text
            End If
            Obj.CtrlTxtPickAmt.Text = MyRound(CDbl(Obj.CtrlTxtPickAmt.Text), clsDefaultConfiguration.BillRoundOffAt)
            Obj.ctrlTxtHowMuchPay.Text = CDbl(CtrllblMinAdv.Text)
            Obj.TotalBalAmt = CDbl(CtrllblNetAmt.Text)
            Obj.ShowDialog(Me)
            IsBtnSave = Obj.IsNew
            '"ME"  is add on 24.feb.2010 because this screen appear alone if user toggle with ALT key

            If Not Obj Is Nothing Then
                If Obj.blnAllowtoGoPaymentScreen = False Then
                    Exit Sub
                Else
                    'CtrlCashSummary1.lbltxt5 = Obj.CtrlTxtMinAmt.Text

                    'Change by Ashish on 25 Nov 2010
                    'Adding below lines for skipping Payment form if override amount = 0
                    If Obj.ctrlTxtHowMuchPay.Text = Decimal.Zero Then
                        BtnSaveSalesOrder_Click(sender, e, True)
                        Exit Sub
                    End If
                    'End of change

                End If
            Else
                Exit Sub
            End If
            ' end New form for accepting payment in sales order.
            'Dim objPayment As New frmNAcceptPayment()
            Dim objPayment As New frmNAcceptPaymentPC(True)  '' added by nikhil for temporary
            getclpsettings()
            objPayment.ParentRelation = "SalesOrder"
            objPayment.TotalBillAmount = CDbl(CtrllblNetAmt.Text)
            'objPayment.MinimumBillAmount = CDbl(CtrlCashSummary1.lbltxt5)
            objPayment.MinimumBillAmount = CDbl(Obj.CtrlTxtMinAmt.Text)
            objPayment.TotalPick = CDbl(Obj.CtrlTxtPickAmt.Text)
            objPayment.IsChangeTender = True
            Dim dtCustomer As New DataTable   '' added by nikhil to get customerdetails for PC
            dtCustomer = ObjclsCommon.GetCustomerDetails(CustomerNo, clsAdmin.SiteCode)
            If Not dtCustomer Is Nothing AndAlso dtCustomer.Rows.Count > 0 Then
                objPayment.CustName = dtCustomer.Rows(0)("NameOnCard").ToString
                objPayment.CompName = dtCustomer.Rows(0)("CompanyName").ToString
                objPayment.MobNumber = dtCustomer.Rows(0)("Mobileno").ToString
            End If
            If Val(lblPickupQty.Text) = 0 Then
                objPayment.AvoidCreditSalesTender = True
            End If

            If Not Obj.ctrlTxtHowMuchPay Is Nothing Then
                objPayment.CustomerWantPay = Obj.ctrlTxtHowMuchPay.Text
                objPayment.ctrlPayCash.txtCash.Value = Obj.ctrlTxtHowMuchPay.Text
            End If

            'If CtrlCustSearch1.rbCLPMember.Checked = True AndAlso CtrlCustSearch1.CtrlTxtCustNo.Text <> String.Empty Then
            '    objPayment.CLPCustomerCardNumber = CtrlCustSearch1.CtrlTxtCustNo.Text
            'End If

            If CustomerNo <> String.Empty Then
                objPayment.CLPCustomerCardNumber = CustomerNo
            End If

            objPayment.PaymentType = clsAcceptPayment.PaymentType.Advance
            objPayment.RoundAt = clsDefaultConfiguration.BillRoundOffAt
            objPayment.IsFastCashMemo = False 'vipin
            objPayment.IsCreditSale = True
            objPayment.ShowDialog(Me)
            PaymentTermId = objPayment.PaymentTermNameId
            _dsPayment = New DataSet
            _dsPayment = objPayment.ReciptTotalAmount()

            'If _dsPayment.Tables.Count > 0 Then
            '    If _dsPayment.Tables(0).Rows.Count > 0 Then
            '        For Each dr2 In _dsPayment.Tables(0).Rows  'vipin
            '            If dr2("NEFTReferenceNo") <> "-" Then
            '                dr2("Number") = dr2("NEFTReferenceNo")
            '            End If

            '            If dr2("RTGSReferenceNo") <> "-" Then
            '                dr2("Number") = dr2("NEFTReferenceNo")
            '            End If
            '            dr2("RefNo_4") = dr2("Remarks")
            '        Next
            '    End If
            'End If

            If dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables(0).Rows.Count > 0 Then
                objPayment.Close()
                CalculateSalesOrderSummary(dsScan)
            End If



            If objPayment.Action = "Save" Then
                ' reset the mininum amount add on 11/11/2009 by ram
                CtrllblMinAdv.Text = Obj.CtrlTxtMinAmt.Text
                'MsgBox(objPayment.intReturnCashToCust)
                ' end of reset

                'Added by Rohit for CR5938
                _dDueDate = objPayment.dDueDate
                _strRemarks = objPayment.strRemarks

                BtnSaveSalesOrder_Click(sender, e)
            Else
                IsBtnSave = True
            End If

            If objPayment.Action = "Gift" Then
                ' reset the mininum amount add on 11/11/2009 by ram
                CtrllblMinAdv.Text = Obj.CtrlTxtMinAmt.Text
                'MsgBox(objPayment.intReturnCashToCust)
                ' end of reset
                IsGiftVoucher = True
                GiftReceiptMessage = objPayment.GiftReceiptMessage

                'Added by Rohit for CR5938
                _dDueDate = objPayment.dDueDate
                _strRemarks = objPayment.strRemarks
                BtnSaveSalesOrder_Click(sender, e)
            End If

            objPayment.Close()

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)

        Finally

        End Try
    End Sub

    Private IsGiftVoucher As Boolean

    Private Sub BtnPayCash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Not isValidData() Then
                Exit Sub
            End If
            grdScanItem.FinishEditing()

            If CtrltxrCust.Text = String.Empty Then
                ShowMessage(getValueByKey("SO048"), "SO048 - " & getValueByKey("CLAE04"))
                CtrltxrCust.Select()
                Exit Sub
            End If

            If Not (dsScan.Tables(0).Rows.Count > 0) Then
                'ShowMessage("Please Scan Article first", "Sales Order Information")
                ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                Exit Sub
            ElseIf CDbl(CtrllblNetAmt.Text) <= Decimal.Zero Then
                ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            For drindex = 0 To dsPackagingVar.Tables(0).Rows.Count - 1

                If dsPackagingVar.Tables(0).Rows(drindex)("ArticleType") = "Combo" Then
                    If dsPackagingVar.Tables(0).Rows(drindex)("PackagingMaterial").ToString() = "" Then
                        ShowMessage("Please select Packaging material for Combo article", " " & getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                End If
            Next
            'Dim obj As New frmSpecialPrompt("What you want to pay")
            'obj.ShowTextBox = True
            'obj.ShowDialog()
            rbbtnDefaultPromo_Click(rbbtnDefaultPromo, New System.EventArgs)
            'IsBtnSave = False
            Dim Obj As New frmNHowMuchtoPay
            Obj.CtrlTxtMinAmt.Text = CDbl(CtrllblMinAdv.Text)
            Obj.CtrlTxtPickAmt.Text = Math.Round(IIf(dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(TotalPickUpAmt)", "") Is DBNull.Value, 0, dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(TotalPickUpAmt)", "")), 2)
            If CDbl(Obj.CtrlTxtPickAmt.Text) < CDbl(Obj.CtrlTxtMinAmt.Text) Then
                Obj.CtrlTxtPickAmt.Text = Obj.CtrlTxtMinAmt.Text
            End If
            Obj.CtrlTxtPickAmt.Text = MyRound(CDbl(Obj.CtrlTxtPickAmt.Text), clsDefaultConfiguration.BillRoundOffAt)
            Obj.ctrlTxtHowMuchPay.Text = CDbl(CtrllblMinAdv.Text)
            Obj.TotalBalAmt = CDbl(CtrllblNetAmt.Text)
            Obj.ShowDialog()
            IsBtnSave = Obj.IsNew
            If Not Obj Is Nothing Then
                CtrllblMinAdv.Text = Obj.CtrlTxtMinAmt.Text
                If Obj.blnAllowtoGoPaymentScreen = False Then
                    Exit Sub
                End If
                If Obj.ctrlTxtHowMuchPay.Text = Decimal.Zero Then
                    BtnSaveSalesOrder_Click(sender, e, True)
                    Exit Sub
                End If
            Else
                Exit Sub
            End If


            Dim objPaymentByCash As New frmNAcceptPaymentByCash("SO")
            objPaymentByCash.TotalBillAmount = CDbl(CtrllblNetAmt.Text)
            objPaymentByCash.TotalMinimumAmount = CDbl(CtrllblBaltoPay.Text)

            If Not Obj.ctrlTxtHowMuchPay Is Nothing Then
                objPaymentByCash.CustomerWantPay = Obj.ctrlTxtHowMuchPay.Text
            End If

            objPaymentByCash.ShowDialog()

            If Not (objPaymentByCash.IsCancelAcceptPayment) Then
                If Not objPaymentByCash.ReciptTotalAmount Is Nothing And objPaymentByCash.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                    _dsPayment = objPaymentByCash.ReciptTotalAmount
                    objPaymentByCash.Close()
                    CalculateSalesOrderSummary(dsScan)

                    ' reset the mininum amount add on 11/11/2009 by ram
                    CtrllblMinAdv.Text = Obj.CtrlTxtMinAmt.Text
                    ' end of reset

                    If objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeGift Then
                        IsGiftVoucher = True
                        GiftReceiptMessage = objPaymentByCash.GiftReceiptMessage
                        BtnSaveSalesOrder_Click(sender, e)

                    ElseIf objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeSave Then
                        IsGiftVoucher = False
                        BtnSaveSalesOrder_Click(sender, e)
                    End If

                Else
                    ShowMessage(getValueByKey("SO070"), "SO070 - " & getValueByKey("CLAE05"))
                End If
            Else
                IsBtnSave = True
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CM033"), "CM033 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in Updating cash payment data ", "Information")
        End Try
    End Sub
    Private Sub CreditSales(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Not isValidData() Then
                Exit Sub
            End If
            grdScanItem.FinishEditing()

            If CtrltxrCust.Text = String.Empty Then
                ShowMessage(getValueByKey("SO048"), "SO048 - " & getValueByKey("CLAE04"))
                CtrltxrCust.Select()
                Exit Sub
            End If

            If Not (dsScan.Tables(0).Rows.Count > 0) Then
                'ShowMessage("Please Scan Article first", "Sales Order Information")
                ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                Exit Sub
            ElseIf CDbl(CtrllblNetAmt.Text.Trim) <= Decimal.Zero Then
                ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            'Dim obj As New frmSpecialPrompt("What you want to pay")
            'obj.ShowTextBox = True
            'obj.ShowDialog()
            rbbtnDefaultPromo_Click(rbbtnDefaultPromo, New System.EventArgs)
            IsBtnSave = False
            Dim Obj As New frmNHowMuchtoPay
            Obj.CtrlTxtMinAmt.Text = CDbl(CtrllblMinAdv.Text.Trim)
            Obj.CtrlTxtPickAmt.Text = MyRound(IIf(dsPackagingVar.Tables(0).Compute("SUM(TotalPickUpAmt)", "") Is DBNull.Value, 0, dsPackagingVar.Tables(0).Compute("SUM(TotalPickUpAmt)", "")), clsDefaultConfiguration.BillRoundOffAt)
            Obj.CtrlTxtPickAmt.Text = MyRound(CDbl(Obj.CtrlTxtPickAmt.Text), clsDefaultConfiguration.BillRoundOffAt)
            Obj.ctrlTxtHowMuchPay.Text = CDbl(CtrllblMinAdv.Text.Trim)
            Obj.TotalBalAmt = CDbl(CtrllblNetAmt.Text.Trim)
            Obj.ShowDialog()
            IsBtnSave = Obj.IsNew
            If Not Obj Is Nothing Then
                CtrllblAmtPaid.Text = Obj.CtrlTxtMinAmt.Text
                If Obj.blnAllowtoGoPaymentScreen = False Then
                    CtrllblAmtPaid.Text = 0
                    IsBtnSave = True
                    Exit Sub
                End If
            Else
                IsBtnSave = True
                Exit Sub
            End If


            ' Dim objPaymentByCash As New frmNAcceptPaymentByCash("SO")
            'objPaymentByCash.TotalBillAmount = CDbl(CtrlCashSummary1.lbltxt4)
            'objPaymentByCash.TotalMinimumAmount = CDbl(CtrlCashSummary1.lbltxt5)

            'If Not Obj.ctrlTxtHowMuchPay Is Nothing Then
            '    objPaymentByCash.CustomerWantPay = Obj.ctrlTxtHowMuchPay.Text
            'End If

            'objPaymentByCash.ShowDialog()

            'If Not (objPaymentByCash.IsCancelAcceptPayment) Then
            'If Not objPaymentByCash.ReciptTotalAmount Is Nothing And objPaymentByCash.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
            '_dsPayment = objPaymentByCash.ReciptTotalAmount


            'Dim Table1 As DataTable
            'Table1 = New DataTable("MstRecieptType")

            'Dim dsPayment As DataRow = _dsPayment.Tables("MSTRecieptType").NewRow() '.t("MstRecieptType").NewRow()
            'dsPayment("SrNo") = 1

            ' If(


            'If Not _dsPayment Is Nothing And _dsPayment.Tables.Contains("MstRecieptType") Then

            Dim dtCreditReciept As DataTable
            dtCreditReciept = New DataTable("MstRecieptType")

            If _dsPayment.Tables.Contains("MstRecieptType") = False Then

                _dsPayment.Tables.Add(dtCreditReciept)
            Else 'vipin
                If Not _dsPayment.Tables(0).Columns.Contains("RefNO_2") Then
                    _dsPayment.Tables(0).Columns.Add("RefNO_2", GetType(String))
                End If
            End If




            dtCreditReciept.Columns.Add("SrNO", GetType(String))
            dtCreditReciept.Columns.Add("Reciept", GetType(String))
            dtCreditReciept.Columns.Add("RecieptType", GetType(String))
            dtCreditReciept.Columns.Add("Amount", GetType(Integer))
            dtCreditReciept.Columns.Add("AmountInCurrency", GetType(Integer))
            dtCreditReciept.Columns.Add("Number", GetType(String))
            dtCreditReciept.Columns.Add("Date", GetType(DateTime))
            dtCreditReciept.Columns.Add("RecieptTypeCode", GetType(String))
            dtCreditReciept.Columns.Add("ExchangeRate", GetType(Integer))
            dtCreditReciept.Columns.Add("CurrencyCode", GetType(String))
            dtCreditReciept.Columns.Add("RefNO_2", GetType(String))
            dtCreditReciept.Columns.Add("RefNO_3", GetType(String))
            dtCreditReciept.Columns.Add("RefNO_4", GetType(String))
            dtCreditReciept.Columns.Add("BankAccNo", GetType(String))
            dtCreditReciept.Columns.Add("NOCLP", GetType(Boolean))
            dtCreditReciept.Columns.Add("IssuedForCLP", GetType(Boolean))

            Dim dsPayment As DataRow = _dsPayment.Tables("MstRecieptType").NewRow()

            dsPayment("SrNO") = 1
            dsPayment("Reciept") = ""
            dsPayment("RecieptType") = "Credit Sales"
            dsPayment("Amount") = CDbl(CtrllblAmtPaid.Text.Trim)
            dsPayment("AmountInCurrency") = CDbl(CtrllblAmtPaid.Text.Trim)
            dsPayment("Number") = "Rs. " & CDbl(CtrllblAmtPaid.Text.Trim)
            dsPayment("Date") = clsAdmin.CurrentDate.Date
            dsPayment("RecieptTypeCode") = "Credit"
            dsPayment("ExchangeRate") = 1
            dsPayment("CurrencyCode") = "INR"
            dsPayment("RefNO_2") = CDbl(CtrllblAmtPaid.Text.Trim)
            dsPayment("RefNO_3") = ""
            dsPayment("RefNO_4") = ""
            dsPayment("BankAccNo") = ""
            dsPayment("NOCLP") = False
            dsPayment("IssuedForCLP") = False
            _dsPayment.Tables("MSTRecieptType").Rows.Add(dsPayment)

            ' objPaymentByCash.Close()
            CalculateSalesOrderSummary(dsScan)
            If Not Obj Is Nothing Then
                CtrllblAmtPaid.Text = Obj.CtrlTxtMinAmt.Text
            End If
            ' reset the mininum amount add on 11/11/2009 by ram
            'CtrlCashSummary1.lbltxt5 = Obj.CtrlTxtMinAmt.Text
            ' end of reset

            'If objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeGift Then
            '    IsGiftVoucher = True
            '    GiftReceiptMessage = objPaymentByCash.GiftReceiptMessage
            '    BtnSaveSalesOrder_Click(sender, e)

            'ElseIf objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeSave Then
            '    IsGiftVoucher = False
            BtnSaveSalesOrder_Click(sender, e)
            ' End If

            'Else
            'ShowMessage(getValueByKey("SO070"), "SO070 - " & getValueByKey("CLAE05"))
            'End If



        Catch ex As Exception
            ShowMessage(getValueByKey("CM033"), "CM033 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in Updating cash payment data ", "Information")
        End Try
    End Sub
    Private Sub BtnPayCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Not isValidData() Then
                Exit Sub
            End If
            grdScanItem.FinishEditing()

            If CtrltxrCust.Text = String.Empty Then
                ShowMessage(getValueByKey("SO048"), "SO048 - " & getValueByKey("CLAE04"))
                CtrltxrCust.Select()
                Exit Sub
            End If

            If Not (dsScan.Tables(0).Rows.Count > 0) Then
                'ShowMessage("Please Scan Article first", "Sales Order Information")
                ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                Exit Sub
            ElseIf CDbl(CtrllblNetAmt.Text) <= Decimal.Zero Then
                ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            For drindex = 0 To dsPackagingVar.Tables(0).Rows.Count - 1

                If dsPackagingVar.Tables(0).Rows(drindex)("ArticleType") = "Combo" Then
                    If dsPackagingVar.Tables(0).Rows(drindex)("PackagingMaterial").ToString() = "" Then
                        ShowMessage("Please select Packaging material for Combo article", " " & getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                End If
            Next
            rbbtnDefaultPromo_Click(rbbtnDefaultPromo, New System.EventArgs)
            IsBtnSave = False
            Dim Obj As New frmNHowMuchtoPay
            Obj.CtrlTxtMinAmt.Text = CDbl(CtrllblMinAdv.Text)
            Obj.CtrlTxtPickAmt.Text = Math.Round(IIf(dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(TotalPickUpAmt)", "") Is DBNull.Value, 0, dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(TotalPickUpAmt)", "")), 2)
            If CDbl(Obj.CtrlTxtPickAmt.Text) < CDbl(Obj.CtrlTxtMinAmt.Text) Then
                Obj.CtrlTxtPickAmt.Text = Obj.CtrlTxtMinAmt.Text
            End If
            Obj.CtrlTxtPickAmt.Text = MyRound(CDbl(Obj.CtrlTxtPickAmt.Text), clsDefaultConfiguration.BillRoundOffAt)
            Obj.ctrlTxtHowMuchPay.Text = CDbl(CtrllblMinAdv.Text)
            Obj.TotalBalAmt = CDbl(CtrllblNetAmt.Text)
            Obj.ShowDialog()
            IsBtnSave = Obj.IsNew
            If Not Obj Is Nothing Then
                CtrllblMinAdv.Text = Obj.CtrlTxtMinAmt.Text
                If Obj.blnAllowtoGoPaymentScreen = False Then
                    IsBtnSave = True
                    Exit Sub
                End If
                If Obj.ctrlTxtHowMuchPay.Text = Decimal.Zero Then
                    BtnSaveSalesOrder_Click(sender, e, True)
                    Exit Sub
                End If
            Else
                IsBtnSave = True
                Exit Sub
            End If
            Dim objPayment As New frmNAcceptPaymentByCard("SO")
            If clsDefaultConfiguration.IsNewSalesOrder Then
                objPayment.TotalBillAmount = CDbl(Obj.CtrlTxtMinAmt.Text)
                objPayment.lblBillAmount.Text = CDbl(CtrllblNetAmt.Text)
            Else
                objPayment.TotalBillAmount = CDbl(CtrllblNetAmt.Text)
            End If

            ' objPayment.TotalBillAmount = CDbl(CtrllblNetAmt.Text)
            objPayment.TotalMinAmount = CDbl(CtrllblBaltoPay.Text)
            'AS Discussed with Santosh code Comment for PC CR 
            'If Not Obj.ctrlTxtHowMuchPay Is Nothing Then
            '    objPayment.CustomerWantPay = Obj.ctrlTxtHowMuchPay.Text
            'End If
            objPayment.ShowDialog()

            objPayment.Close()
            If Not (objPayment.IsCancelAcceptPayment) Then
                If Not objPayment.ReciptTotalAmount Is Nothing And objPayment.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                    _dsPayment = objPayment.ReciptTotalAmount
                    objPayment.Close()
                    CalculateSalesOrderSummary(dsScan)
                    If objPayment.Action = My.Resources.AcceptPaymentActionTypeSave Then
                        BtnSaveSalesOrder_Click(sender, e)
                    ElseIf objPayment.Action = My.Resources.AcceptPaymentActionTypeGift Then
                        IsGiftVoucher = True
                        GiftReceiptMessage = objPayment.GiftReceiptMessage
                        BtnSaveSalesOrder_Click(sender, e)
                    End If
                Else
                    ShowMessage(getValueByKey("SO070"), "SO070 - " & getValueByKey("CLAE05"))
                End If
            Else
                IsBtnSave = True
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CM034"), "CM034 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in Updating card payment data ", "Information")
        End Try
    End Sub
    Private Sub BtnPayCheque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Not isValidData() Then
                Exit Sub
            End If
            grdScanItem.FinishEditing()
            If CtrltxrCust.Text = String.Empty Then
                ShowMessage(getValueByKey("SO048"), "SO048 - " & getValueByKey("CLAE04"))
                CtrltxrCust.Select()
                Exit Sub
            End If
            If Not (dsScan.Tables(0).Rows.Count > 0) Then
                'ShowMessage("Please Scan Article first", "Sales Order Information")
                ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                Exit Sub
            ElseIf CDbl(CtrllblNetAmt.Text) <= Decimal.Zero Then
                ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            For drindex = 0 To dsPackagingVar.Tables(0).Rows.Count - 1

                If dsPackagingVar.Tables(0).Rows(drindex)("ArticleType") = "Combo" Then
                    If dsPackagingVar.Tables(0).Rows(drindex)("PackagingMaterial").ToString() = "" Then
                        ShowMessage("Please select Packaging material for Combo article", " " & getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                End If
            Next
            rbbtnDefaultPromo_Click(rbbtnDefaultPromo, New System.EventArgs)
            IsBtnSave = False
            Dim Obj As New frmNHowMuchtoPay
            Obj.CtrlTxtMinAmt.Text = CDbl(CtrllblMinAdv.Text)
            Obj.CtrlTxtPickAmt.Text = Math.Round(IIf(dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(TotalPickUpAmt)", "") Is DBNull.Value, 0, dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(TotalPickUpAmt)", "")), 2)
            If CDbl(Obj.CtrlTxtPickAmt.Text) < CDbl(Obj.CtrlTxtMinAmt.Text) Then
                Obj.CtrlTxtPickAmt.Text = Obj.CtrlTxtMinAmt.Text
            End If
            Obj.CtrlTxtPickAmt.Text = MyRound(CDbl(Obj.CtrlTxtPickAmt.Text), clsDefaultConfiguration.BillRoundOffAt)
            Obj.ctrlTxtHowMuchPay.Text = CDbl(CtrllblMinAdv.Text)
            Obj.TotalBalAmt = CDbl(CtrllblNetAmt.Text)
            Obj.ShowDialog()
            IsBtnSave = Obj.IsNew
            If Not Obj Is Nothing Then
                CtrllblMinAdv.Text = Obj.CtrlTxtMinAmt.Text
                If Obj.blnAllowtoGoPaymentScreen = False Then
                    IsBtnSave = True
                    Exit Sub
                End If
                If Obj.ctrlTxtHowMuchPay.Text = Decimal.Zero Then
                    BtnSaveSalesOrder_Click(sender, e, True)
                    Exit Sub
                End If
            Else
                IsBtnSave = True
                Exit Sub
            End If
            Dim objCheck As New frmNCheckPayment("SO")
            If clsDefaultConfiguration.IsNewSalesOrder Then
                objCheck.BillAmount = CDbl(Obj.CtrlTxtMinAmt.Text)
                objCheck.txtBillAmount.Text = CDbl(CtrllblNetAmt.Text)
            Else
                objCheck.BillAmount = CDbl(CtrllblNetAmt.Text)
            End If
            ' objCheck.BillAmount = CDbl(CtrllblNetAmt.Text)
            objCheck.TotalMinAmount = CDbl(CtrllblBaltoPay.Text)
            objCheck.lblTotalAmount.Text = CtrllblNetAmt.Text.ToString()
            'AS Discussed with Santosh code Comment for PC CR 
            'If Not Obj.ctrlTxtHowMuchPay Is Nothing Then
            '    objCheck.CustomerWantPay = Obj.ctrlTxtHowMuchPay.Text
            'End If
            objCheck.ShowDialog()

            If objCheck.IsCancelAcceptPayment = False Then
                If Not objCheck.ReciptTotalAmount Is Nothing And objCheck.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                    _dsPayment = New DataSet
                    _dsPayment = objCheck.ReciptTotalAmount
                    'Dim ds As New DataSet()
                    'ds.Tables.Add(dt)
                    objCheck.Close()
                    'If Not ds Is Nothing Then
                    If dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables(0).Rows.Count > 0 Then
                        CalculateSalesOrderSummary(dsScan)

                        If objCheck.Action = My.Resources.AcceptPaymentActionTypeSave Then
                            IsGiftVoucher = False
                            BtnSaveSalesOrder_Click(sender, e)
                        ElseIf objCheck.Action = My.Resources.AcceptPaymentActionTypeGift Then
                            IsGiftVoucher = True
                            GiftReceiptMessage = objCheck.GiftReceiptMessage
                            BtnSaveSalesOrder_Click(sender, e)
                        End If

                    End If
                Else
                    ShowMessage(getValueByKey("SO070"), "SO070 - " & getValueByKey("CLAE05"))
                End If
            Else
                IsBtnSave = True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    '' added by nikhil
    Private Sub BtnPayNEFT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Not isValidData() Then
                Exit Sub
            End If
            grdScanItem.FinishEditing()

            If CtrltxrCust.Text = String.Empty Then
                ShowMessage(getValueByKey("SO048"), "SO048 - " & getValueByKey("CLAE04"))
                CtrltxrCust.Select()
                Exit Sub
            End If

            If Not (dsScan.Tables(0).Rows.Count > 0) Then
                'ShowMessage("Please Scan Article first", "Sales Order Information")
                ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                Exit Sub
            ElseIf CDbl(CtrllblNetAmt.Text) <= Decimal.Zero Then
                ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            For drindex = 0 To dsPackagingVar.Tables(0).Rows.Count - 1

                If dsPackagingVar.Tables(0).Rows(drindex)("ArticleType") = "Combo" Then
                    If dsPackagingVar.Tables(0).Rows(drindex)("PackagingMaterial").ToString() = "" Then
                        ShowMessage("Please select Packaging material for Combo article", " " & getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                End If
            Next
            'Dim obj As New frmSpecialPrompt("What you want to pay")
            'obj.ShowTextBox = True
            'obj.ShowDialog()
            rbbtnDefaultPromo_Click(rbbtnDefaultPromo, New System.EventArgs)
            'IsBtnSave = False
            Dim Obj As New frmNHowMuchtoPay
            Obj.CtrlTxtMinAmt.Text = CDbl(CtrllblMinAdv.Text)
            Obj.CtrlTxtPickAmt.Text = Math.Round(IIf(dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(TotalPickUpAmt)", "") Is DBNull.Value, 0, dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(TotalPickUpAmt)", "")), 2)
            If CDbl(Obj.CtrlTxtPickAmt.Text) < CDbl(Obj.CtrlTxtMinAmt.Text) Then
                Obj.CtrlTxtPickAmt.Text = Obj.CtrlTxtMinAmt.Text
            End If
            Obj.CtrlTxtPickAmt.Text = MyRound(CDbl(Obj.CtrlTxtPickAmt.Text), clsDefaultConfiguration.BillRoundOffAt)
            Obj.ctrlTxtHowMuchPay.Text = CDbl(CtrllblMinAdv.Text)
            Obj.TotalBalAmt = CDbl(CtrllblNetAmt.Text)
            Obj.ShowDialog()
            IsBtnSave = Obj.IsNew
            If Not Obj Is Nothing Then
                CtrllblMinAdv.Text = Obj.CtrlTxtMinAmt.Text
                If Obj.blnAllowtoGoPaymentScreen = False Then
                    Exit Sub
                End If
                If Obj.ctrlTxtHowMuchPay.Text = Decimal.Zero Then
                    BtnSaveSalesOrder_Click(sender, e, True)
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
            Dim objPaymentByCash As New frmNAcceptPaymentByNEFTRTGS
            objPaymentByCash.BillAmount = CDbl(CtrllblNetAmt.Text)
            objPaymentByCash.BalAmount = CDbl(CtrllblBaltoPay.Text)
            objPaymentByCash.TenderTypeCode = "Neft"
            objPaymentByCash.DocumentType = "SO"

            If Not Obj.ctrlTxtHowMuchPay Is Nothing Then
                objPaymentByCash.BalAmount = Obj.ctrlTxtHowMuchPay.Text
            End If
            objPaymentByCash.TotalMinAmount = CDbl(CtrllblBaltoPay.Text) 'vipin
            objPaymentByCash.ShowDialog()

            If Not (objPaymentByCash.IsCancelAcceptPayment) Then
                If Not objPaymentByCash.ReciptTotalAmount Is Nothing And objPaymentByCash.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                    _dsPayment = objPaymentByCash.ReciptTotalAmount
                    objPaymentByCash.Close()
                    CalculateSalesOrderSummary(dsScan)

                    ' reset the mininum amount add on 11/11/2009 by ram
                    CtrllblMinAdv.Text = Obj.CtrlTxtMinAmt.Text
                    ' end of reset

                    If objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeSave Then
                        IsGiftVoucher = False
                        BtnSaveSalesOrder_Click(sender, e)
                    End If

                Else
                    ShowMessage(getValueByKey("SO070"), "SO070 - " & getValueByKey("CLAE05"))
                End If
            Else
                IsBtnSave = True
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CM033"), "CM033 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in Updating cash payment data ", "Information")
        End Try

    End Sub
    Private Sub BtnPayRTGS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Not isValidData() Then
                Exit Sub
            End If
            grdScanItem.FinishEditing()

            If CtrltxrCust.Text = String.Empty Then
                ShowMessage(getValueByKey("SO048"), "SO048 - " & getValueByKey("CLAE04"))
                CtrltxrCust.Select()
                Exit Sub
            End If

            If Not (dsScan.Tables(0).Rows.Count > 0) Then
                'ShowMessage("Please Scan Article first", "Sales Order Information")
                ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                Exit Sub
            ElseIf CDbl(CtrllblNetAmt.Text) <= Decimal.Zero Then
                ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            For drindex = 0 To dsPackagingVar.Tables(0).Rows.Count - 1

                If dsPackagingVar.Tables(0).Rows(drindex)("ArticleType") = "Combo" Then
                    If dsPackagingVar.Tables(0).Rows(drindex)("PackagingMaterial").ToString() = "" Then
                        ShowMessage("Please select Packaging material for Combo article", " " & getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                End If
            Next
            'Dim obj As New frmSpecialPrompt("What you want to pay")
            'obj.ShowTextBox = True
            'obj.ShowDialog()
            rbbtnDefaultPromo_Click(rbbtnDefaultPromo, New System.EventArgs)
            'IsBtnSave = False
            Dim Obj As New frmNHowMuchtoPay
            Obj.CtrlTxtMinAmt.Text = CDbl(CtrllblMinAdv.Text)
            Obj.CtrlTxtPickAmt.Text = Math.Round(IIf(dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(TotalPickUpAmt)", "") Is DBNull.Value, 0, dsPackagingVar.Tables("PackagingMaterial").Compute("SUM(TotalPickUpAmt)", "")), 2)
            If CDbl(Obj.CtrlTxtPickAmt.Text) < CDbl(Obj.CtrlTxtMinAmt.Text) Then
                Obj.CtrlTxtPickAmt.Text = Obj.CtrlTxtMinAmt.Text
            End If
            Obj.CtrlTxtPickAmt.Text = MyRound(CDbl(Obj.CtrlTxtPickAmt.Text), clsDefaultConfiguration.BillRoundOffAt)
            Obj.ctrlTxtHowMuchPay.Text = CDbl(CtrllblMinAdv.Text)
            Obj.TotalBalAmt = CDbl(CtrllblNetAmt.Text)
            Obj.ShowDialog()
            IsBtnSave = Obj.IsNew
            If Not Obj Is Nothing Then
                CtrllblMinAdv.Text = Obj.CtrlTxtMinAmt.Text
                If Obj.blnAllowtoGoPaymentScreen = False Then
                    Exit Sub
                End If
                If Obj.ctrlTxtHowMuchPay.Text = Decimal.Zero Then
                    BtnSaveSalesOrder_Click(sender, e, True)
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
            Dim objPaymentByCash As New frmNAcceptPaymentByNEFTRTGS
            objPaymentByCash.BillAmount = CDbl(CtrllblNetAmt.Text)
            objPaymentByCash.BalAmount = CDbl(CtrllblBaltoPay.Text)
            objPaymentByCash.TenderTypeCode = "Rtgs"
            objPaymentByCash.DocumentType = "SO"

            If Not Obj.ctrlTxtHowMuchPay Is Nothing Then
                objPaymentByCash.BalAmount = Obj.ctrlTxtHowMuchPay.Text
            End If

            objPaymentByCash.ShowDialog()

            If Not (objPaymentByCash.IsCancelAcceptPayment) Then
                If Not objPaymentByCash.ReciptTotalAmount Is Nothing And objPaymentByCash.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                    _dsPayment = objPaymentByCash.ReciptTotalAmount
                    objPaymentByCash.Close()
                    CalculateSalesOrderSummary(dsScan)

                    ' reset the mininum amount add on 11/11/2009 by ram
                    CtrllblMinAdv.Text = Obj.CtrlTxtMinAmt.Text
                    ' end of reset

                    If objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeSave Then
                        IsGiftVoucher = False
                        BtnSaveSalesOrder_Click(sender, e)
                    End If

                Else
                    ShowMessage(getValueByKey("SO070"), "SO070 - " & getValueByKey("CLAE05"))
                End If
            Else
                IsBtnSave = True
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CM033"), "CM033 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in Updating cash payment data ", "Information")
        End Try
    End Sub

    ''' <summary>
    ''' Applying the Promotion in Current Sales Order
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Manual Promotion may be(%,fixed price off,Fixed price sale) </remarks>
    Private Sub rbbtnDefaultPromo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbbtnDefaultPromo.Click
        Try
            For drindex = 0 To dsPackagingVar.Tables(0).Rows.Count - 1

                If dsPackagingVar.Tables(0).Rows(drindex)("ArticleType") = "Combo" Then
                    If dsPackagingVar.Tables(0).Rows(drindex)("PackagingMaterial").ToString() = "" Then
                        ShowMessage("Please select Packaging material for Combo article", " " & getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                End If
            Next
            If dsScan.Tables(0).Rows.Count > 0 Then
                'If IsApplyPromotion = True Then
                '    RemoveApplyPromotion(_dsScan)
                'End If           
            Else
                'ShowMessage("Please Scan Article first", "Sales Order Information")
                ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            Dim obj As New clsApplyPromotion
            obj.DecimalDigits = clsDefaultConfiguration.DecimalPlaces
            obj.MainTable = "PackagingMaterial"
            obj.ExclusiveTaxFieldName = "ExclTaxAmt"
            'obj.TotalDiscountField = "DiscountAmount"
            obj.TotalDiscountField = "Discount"
            obj.GrossAmtField = "GrossAmt"

            ' If clsDefaultConfiguration.IsPromotionManually = True Then
            'If MsgBox(getValueByKey("SO021"), MsgBoxStyle.YesNo, "Customer Information") = MsgBoxResult.Yes Then
            If UCase(sender.id) = UCase("rbbtnSelectPromo") Then
                If IsDefaultPromotion Then
                    If MsgBox(getValueByKey("SO094"), MsgBoxStyle.OkCancel, "SO094 - " & getValueByKey("CLAE04")) = MsgBoxResult.Ok Then
                    Else
                        Exit Sub
                    End If
                End If
                Dim dtList As DataTable
                dtList = obj.GetListofActivePromotions(vSiteCode)

                If Not dtList Is Nothing Then
                    Dim objView As New frmNCommonSearch
                    objView.SetData = dtList
                    objView.ShowDialog()

                    If Not objView.search Is Nothing Then
                        Dim offerno As String = objView.search(0)
                        IsSelectedPromotion = True
                        If obj.CheckValidations(offerno) = True Then
                            Dim dtValidation As DataTable = obj.GetAllQuestions(offerno)
                            Dim StrQues As String = ""

                            For Each dr As DataRow In dtValidation.Rows
                                StrQues = StrQues & dr("QuestionName").ToString() & ","
                            Next
                            IsComboDoubleClicked = False
                            Dim p As Object = "Clear"
                            rbbtnClrAllPromo_Click("Clear", Nothing)
                            If StrQues.Contains("Autho") = True AndAlso StrQues.Contains("Voucher") = True Then
                                '_dsScan.Tables(0).Columns("Discount").ColumnName = "TotalDiscount"
                                dsPackagingVar.Tables(0).Columns("Discount").ColumnName = "TotalDiscount"
                                '_dsScan.Tables(0).Columns("ExclTaxAmt").ColumnName = "EXCLUSIVETAX"
                                dsPackagingVar.Tables(0).Columns("ExclTaxAmt").ColumnName = "EXCLUSIVETAX"

                                'CheckInterTransactionAuth("ORD", _dsScan.Tables(0), 0, 0, 0, offerno)
                                CheckInterTransactionAuth("ORD", dsPackagingVar.Tables(0), 0, 0, 0, offerno, isNewSO:=True)
                                ' _dsScan.Tables(0).Columns("TotalDiscount").ColumnName = "Discount"
                                '_dsScan.Tables(0).Columns("EXCLUSIVETAX").ColumnName = "ExclTaxAmt"
                                _dsPackagingVar.Tables(0).Columns("TotalDiscount").ColumnName = "Discount"
                                _dsPackagingVar.Tables(0).Columns("EXCLUSIVETAX").ColumnName = "ExclTaxAmt"
                            ElseIf StrQues.Contains("Autho") = True Then
                                If CheckInterTransactionAuth("DAUTH", _dsPackagingVar.Tables(0)) = True Then 'CheckInterTransactionAuth("DAUTH", _dsScan.Tables(0))
                                    'obj.ApplySelectedPromotion(offerno, _dsScan, vSiteCode)
                                    obj.ApplySelectedPromotion(offerno, _dsPackagingVar, vSiteCode)
                                End If
                            End If
                        Else
                            Dim p As Object = "Clear"
                            rbbtnClrAllPromo_Click("Clear", Nothing)
                            'obj.ApplySelectedPromotion(offerno, _dsScan, vSiteCode)
                            obj.ApplySelectedPromotion(offerno, _dsPackagingVar, vSiteCode)
                        End If
                    Else
                        Exit Sub
                    End If
                End If
            Else
                If IsSelectedPromotion = False Then
                    ShowMessage(getValueByKey("SO022"), "SO022 - " & getValueByKey("CLAE04"))
                    IsDefaultPromotion = True
                End If
                'ShowMessage("Default Schemes is applied Now", "Message")
                'obj.CalculatedDs(_dsScan, vSiteCode)
                obj.CalculatedDs(_dsPackagingVar, vSiteCode)
            End If
            'Else
            '    ShowMessage(getValueByKey("SO022"), "SO022 - " & getValueByKey("CLAE04"))
            '    'ShowMessage("Default Schemes is applied Now", "Message")
            '    obj.CalculatedDs(_dsScan, vSiteCode)
            'End If
            For Each drUpdateScan As DataRow In _dsScan.Tables(0).Rows
                'drRem("GrossAmt") = 0
                'drRem("MinPayAmt") = 0
                Dim resultPackQuntity As DataRow() = _dsPackagingVar.Tables(0).Select("RowIndex='" + drUpdateScan("RowIndex").ToString() + "' and IsHeader=True ")
                If resultPackQuntity.Length > 0 Then
                    drUpdateScan("Discount") = Math.Round(resultPackQuntity(0)("Discount"), 2)
                    drUpdateScan("NetAmount") = ((resultPackQuntity(0)("SellingPrice") * resultPackQuntity(0)("Quantity")) - (resultPackQuntity(0)("Discount"))) + (resultPackQuntity(0)("TotalTaxAmt")) '' resultPackQuntity(0)("NetAmount")
                    drUpdateScan("LineDiscount") = Math.Round(resultPackQuntity(0)("LineDiscount"), 2)
                    drUpdateScan("TotalDiscPercentage") = resultPackQuntity(0)("TotalDiscPercentage")
                    drUpdateScan("FirstLevel") = resultPackQuntity(0)("FirstLevel")
                    drUpdateScan("TopLevel") = resultPackQuntity(0)("TopLevel")
                    drUpdateScan("TopLevelDisc") = resultPackQuntity(0)("TopLevelDisc")
                End If



            Next
            Dim dtTaxCalc As DataTable

            For Each drDisc As DataRow In _dsPackagingVar.Tables(0).Rows

                If IsCSTApplicable Then
                    dtTaxCalc = objCM.getTax(vSiteCode, String.Empty, "SO201", drDisc("Quantity"), drDisc("EAN"), clsDefaultConfiguration.CSTTaxCode, True)
                Else
                    dtTaxCalc = objCM.getTax(vSiteCode, drDisc.Item("ArticleCode"), "SO201", drDisc("Quantity"), drDisc("EAN"), clsDefaultConfiguration.CSTTaxCode, False)
                End If
                If drDisc("ArticleType") = "Combo" Then
                    If lblArticleCombo.Rows.Count > 0 Then  ''$$ added by nikhil
                        'Dim TaxAmt As Double = (Math.Round(drDisc.Item("SellingPrice") * drDisc.Item("Quantity"), 3) - drDisc.Item("Discount")) * CDbl(lblArticleCombo.Rows(0)("Tax") / 100)
                        Dim TaxAmt As Double = (Math.Round(drDisc.Item("SellingPrice") * drDisc.Item("Quantity"), 3) - drDisc.Item("Discount")) * CDbl(drDisc.Item("TaxInPer") / 100)
                        Dim vTotalNetAmt = (Math.Round(drDisc.Item("SellingPrice") * drDisc.Item("Quantity"), 3) - drDisc.Item("Discount") + TaxAmt) ''' added by nikhil
                        'dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = vTotalNetAmt.ToString
                        ' objCM.getCalculatedDataSet(dtTaxCalc)
                        If drDisc("ArticleType") = "Combo" Then
                            drDisc("TotalTaxAmt") = Math.Round(TaxAmt, 2)
                            drDisc("ExclTaxAmt") = Math.Round(TaxAmt, 2) '''Math.Round(dtTaxCalc.Compute("SUM(TAXAMOUNT)", "")) $$ added by nikhil
                            drDisc("GrossAmt") = (drDisc.Item("SellingPrice") * drDisc.Item("Quantity"))  '$$ added by nikhil
                            drDisc("NetAmount") = vTotalNetAmt   '$$ added by nikhil

                        Else
                            drDisc("ExclTaxAmt") = Math.Round(dtTaxCalc.Compute("SUM(TAXAMOUNT)", ""), 2)
                            drDisc("TotalTaxAmt") = Math.Round(dtTaxCalc.Compute("SUM(TAXAMOUNT)", ""), 2)
                        End If

                        ''' drDisc("NetAmount") = vTotalNetAmt
                    End If
                Else
                    If dtTaxCalc.Rows.Count > 0 Then
                        Dim vTotalNetAmt = (Math.Round(drDisc.Item("SellingPrice") * drDisc.Item("Quantity"), 3) - drDisc.Item("Discount"))   'drDisc("TotalTaxAmt")'' added by nikhil
                        dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = vTotalNetAmt
                        objCM.getCalculatedDataSet(dtTaxCalc)
                        drDisc("ExclTaxAmt") = Math.Round(dtTaxCalc.Compute("SUM(TAXAMOUNT)", ""), 2)
                        drDisc("TotalTaxAmt") = Math.Round(dtTaxCalc.Compute("SUM(TAXAMOUNT)", ""), 2)
                        drDisc("NetAmount") = vTotalNetAmt + drDisc("TotalTaxAmt")
                        '   SetScanItemInSOTemp(drDisc) 'vipin
                    End If
                End If
                drDisc("Discount") = Math.Round(drDisc("Discount"), 2)
                drDisc("LineDiscount") = Math.Round(drDisc("LineDiscount"), 2)
            Next
            _dsPackagingVar.AcceptChanges()

            For Each drDisc1 As DataRow In _dsScan.Tables(0).Rows
                If IsCSTApplicable Then
                    dtTaxCalc = objCM.getTax(vSiteCode, String.Empty, "SO201", drDisc1("Quantity"), drDisc1("EAN"), clsDefaultConfiguration.CSTTaxCode, True)
                Else
                    dtTaxCalc = objCM.getTax(vSiteCode, drDisc1.Item("ArticleCode"), "SO201", drDisc1("Quantity"), drDisc1("EAN"), clsDefaultConfiguration.CSTTaxCode, False)
                End If
                If drDisc1("ArticleType") = "Combo" Then
                    If lblArticleCombo.Rows.Count > 0 Then  ''$$ added by nikhil
                        'Dim TaxAmt As Double = (Math.Round(drDisc1.Item("SellingPrice") * drDisc1.Item("Quantity"), 3) - drDisc1.Item("Discount")) * CDbl(lblArticleCombo.Rows(0)("Tax") / 100)
                        Dim TaxAmt As Double = (Math.Round(drDisc1.Item("SellingPrice") * drDisc1.Item("Quantity"), 3) - drDisc1.Item("Discount")) * CDbl(drDisc1.Item("TaxInPer") / 100)  '' $$ added by nikhil 
                        Dim vTotalNetAmt = (Math.Round(drDisc1.Item("SellingPrice") * drDisc1.Item("Quantity"), 3) - drDisc1.Item("Discount") + TaxAmt) ''' added by nikhil
                        'dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = vTotalNetAmt.ToString
                        ' objCM.getCalculatedDataSet(dtTaxCalc)
                        If drDisc1("ArticleType") = "Combo" Then
                            drDisc1("TotalTaxAmt") = Math.Round(TaxAmt, 2)
                            drDisc1("ExclTaxAmt") = Math.Round(TaxAmt, 2) '''Math.Round(dtTaxCalc.Compute("SUM(TAXAMOUNT)", "")) $$ added by nikhil
                            drDisc1("GrossAmt") = (Math.Round(drDisc1.Item("SellingPrice") * drDisc1.Item("Quantity"), 3))
                            drDisc1("NetAmount") = vTotalNetAmt  '' $$ added by nik
                        Else
                            drDisc1("ExclTaxAmt") = Math.Round(dtTaxCalc.Compute("SUM(TAXAMOUNT)", ""))
                            drDisc1("TotalTaxAmt") = Math.Round(dtTaxCalc.Compute("SUM(TAXAMOUNT)", ""))
                        End If

                        'drDisc1("NetAmount") = vTotalNetAmt
                    End If
                Else
                    If dtTaxCalc.Rows.Count > 0 Then
                        Dim vTotalNetAmt = (Math.Round(drDisc1.Item("SellingPrice") * drDisc1.Item("Quantity"), 3) - drDisc1.Item("Discount"))   ''drDisc("TotalTaxAmt") ' added by nikhil
                        dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = vTotalNetAmt
                        objCM.getCalculatedDataSet(dtTaxCalc)
                        If drDisc1("ArticleType") = "Combo" Then
                            drDisc1("TotalTaxAmt") = (Math.Round(drDisc1.Item("SellingPrice") * drDisc1.Item("Quantity"), 3) - drDisc1.Item("Discount")) * (CDbl(lblArticleCombo.Rows(0)("Tax") / 100))
                            drDisc1("ExclTaxAmt") = drDisc1("TotalTaxAmt")
                            drDisc1("NetAmount") = vTotalNetAmt + drDisc1("TotalTaxAmt")
                        Else
                            drDisc1("TotalTaxAmt") = Math.Round(dtTaxCalc.Compute("SUM(TAXAMOUNT)", ""), 3)
                            drDisc1("ExclTaxAmt") = Math.Round(dtTaxCalc.Compute("SUM(TAXAMOUNT)", ""), 3)
                        End If


                        'drDisc1("NetAmount") = vTotalNetAmt + drDisc1("TotalTaxAmt")
                    End If
                    End If
            Next
            _dsScan.AcceptChanges()
            For Each drDisc As DataRow In _dsScan.Tables(0).Rows

                If Not (IIf(IsDBNull(drDisc("TotalDiscPercentage")), 0, drDisc("TotalDiscPercentage")) = 0) Then

                    If (Not String.IsNullOrEmpty(drDisc("CLPDiscount").ToString())) Then
                        drDisc("Discount") = CDbl(drDisc("LineDiscount").ToString()) + CDbl(drDisc("CLPDiscount").ToString())
                    End If

                    drDisc("TotalDiscPercentage") = (IIf(drDisc("Discount") Is DBNull.Value, 0, drDisc("Discount")) * 100) / drDisc("GrossAmt")
                    drDisc("PromotionId") = IIf(drDisc("FirstLevel") = String.Empty, 0, drDisc("FirstLevel")) & "," & IIf(drDisc("TopLevel") = String.Empty, 0, drDisc("TopLevel"))

                    'If drDisc("PromotionId") = "0,0" Then
                    '    drDisc("LineDiscount") = (drDisc("GrossAmt") * drDisc("TotalDiscPercentage")) / 100
                    'End If
                    Dim totalamt As Decimal = 0
                    If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                        totalamt = drDisc("GrossAmt") - drDisc("Discount")
                        Dim objcom As New clsSaleOrderCommon
                        objcom.IsCSTApplicable = IsCSTApplicable
                        ' objcom.CreateDataSetForTaxCalculation(drDisc("ARTICLECODE"), totalamt, drDisc, dsMain, CtrlSalesInfo1.CtrlTxtOrderNo.Text, drDisc("EAN"), True)
                    End If

                    drDisc("NetAmount") = Math.Round(drDisc("GrossAmt") - drDisc("LineDiscount") + IIf(drDisc("ExclTaxAmt") Is DBNull.Value, 0, drDisc("ExclTaxAmt")) + IIf(drDisc("IncTaxAmt") Is DBNull.Value, 0, drDisc("IncTaxAmt")), 3) 'vipul
                    'drDisc("MinPayAmt") = Math.Round((drDisc("NetAmount") / drDisc("Quantity")) * drDisc("PickUpQty"), 3)

                    TotalSalesQty = drDisc("PickUpQty") + IIf(drDisc("DeliveredQty") IsNot DBNull.Value, drDisc("DeliveredQty"), 0)
                    NetArticleRate = drDisc("NetAmount") / drDisc("Quantity")
                    ' drDisc("MinPayAmt") = ((drDisc("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate)
                    ' drDisc("TotalPickUpAmt") = (drDisc("PickupQty") * NetArticleRate)
                    drDisc("MinPayAmt") = Math.Round(((drDisc("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate)) '##vioin
                    drDisc("TotalPickUpAmt") = Math.Round((drDisc("PickupQty") * NetArticleRate)) '## added by vipin
                    '-----Added By Prasad for Calculating Tax PickupWise of Savoy Client
                    If clsDefaultConfiguration.IsSavoy Then
                        Dim TaxCalc As Double = 0
                        Dim TaxValue As Double
                        If Not dsMain.Tables("SalesOrderTaxDtls") Is Nothing AndAlso dsMain.Tables("SalesOrderTaxDtls").Rows.Count > 0 Then
                            Dim dr() = dsMain.Tables("SalesOrderTaxDtls").Select("Status=True")
                            If dr.Count > 0 Then
                                TaxValue = dr(0)("TaxValue")
                            End If
                        End If
                        TaxCalc = ((drDisc("SellingPrice") * drDisc("PickupQty")) / dsPackagingVar.Tables(0).Compute("Sum(GrossAmt)", " ")) * TaxValue
                        drDisc("MinPayAmt") = drDisc("MinPayAmt") + TaxCalc
                        drDisc("TotalPickUpAmt") = drDisc("TotalPickUpAmt") + TaxCalc
                    End If
                End If

            Next
            For Each drDisc As DataRow In _dsPackagingVar.Tables(0).Rows

                If Not (IIf(IsDBNull(drDisc("TotalDiscPercentage")), 0, drDisc("TotalDiscPercentage")) = 0) Then

                    If (Not String.IsNullOrEmpty(drDisc("CLPDiscount").ToString())) Then
                        drDisc("Discount") = CDbl(drDisc("LineDiscount").ToString()) + CDbl(drDisc("CLPDiscount").ToString())
                    End If

                    drDisc("TotalDiscPercentage") = (IIf(drDisc("Discount") Is DBNull.Value, 0, drDisc("Discount")) * 100) / drDisc("GrossAmt")
                    drDisc("PromotionId") = IIf(drDisc("FirstLevel") = String.Empty, 0, drDisc("FirstLevel")) & "," & IIf(drDisc("TopLevel") = String.Empty, 0, drDisc("TopLevel"))

                    'If drDisc("PromotionId") = "0,0" Then
                    '    drDisc("LineDiscount") = (drDisc("GrossAmt") * drDisc("TotalDiscPercentage")) / 100
                    'End If
                    Dim totalamt As Decimal = 0
                    If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                        totalamt = drDisc("GrossAmt") - drDisc("Discount")
                        Dim objcom As New clsSaleOrderCommon
                        objcom.IsCSTApplicable = IsCSTApplicable
                        ' objcom.CreateDataSetForTaxCalculation(drDisc("ARTICLECODE"), totalamt, drDisc, dsMain, CtrlSalesInfo1.CtrlTxtOrderNo.Text, drDisc("EAN"), True)
                    End If

                    drDisc("NetAmount") = Math.Round(drDisc("GrossAmt") - drDisc("LineDiscount") + IIf(drDisc("ExclTaxAmt") Is DBNull.Value, 0, drDisc("ExclTaxAmt")) + IIf(drDisc("IncTaxAmt") Is DBNull.Value, 0, drDisc("IncTaxAmt")), 3) 'vipul
                    'drDisc("MinPayAmt") = Math.Round((drDisc("NetAmount") / drDisc("Quantity")) * drDisc("PickUpQty"), 3)

                    TotalSalesQty = drDisc("PickUpQty") + IIf(drDisc("DeliveredQty") IsNot DBNull.Value, drDisc("DeliveredQty"), 0)
                    NetArticleRate = drDisc("NetAmount") / drDisc("Quantity")
                    drDisc("MinPayAmt") = ((drDisc("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate)
                    drDisc("TotalPickUpAmt") = (drDisc("PickupQty") * NetArticleRate)

                    '-----Added By Prasad for Calculating Tax PickupWise of Savoy Client
                    If clsDefaultConfiguration.IsSavoy Then
                        Dim TaxCalc As Double = 0
                        Dim TaxValue As Double
                        If Not dsMain.Tables("SalesOrderTaxDtls") Is Nothing AndAlso dsMain.Tables("SalesOrderTaxDtls").Rows.Count > 0 Then
                            Dim dr() = dsMain.Tables("SalesOrderTaxDtls").Select("Status=True")
                            If dr.Count > 0 Then
                                TaxValue = dr(0)("TaxValue")
                            End If
                        End If
                        TaxCalc = ((drDisc("SellingPrice") * drDisc("PickupQty")) / dsPackagingVar.Tables(0).Compute("Sum(GrossAmt)", " ")) * TaxValue
                        drDisc("MinPayAmt") = drDisc("MinPayAmt") + TaxCalc
                        drDisc("TotalPickUpAmt") = drDisc("TotalPickUpAmt") + TaxCalc
                    End If
                End If

            Next
            IsApplyPromotion = True
            CalculateSalesOrderSummary(dsScan)
            RefreshScanData(dsScan)
            GridSetting()

        Catch ex As Exception
            ShowMessage(getValueByKey("SO023"), "SO023 - " & getValueByKey("CLAE04"))
            LogException(ex)
            'ShowMessage("Promotion is not applied properly", "Error")
        End Try

    End Sub

    Public Class myLocalPrinter
        Friend TextToBePrinted As String

        Public Sub PrintDocuments(ByVal text As String)
            TextToBePrinted = text
            Dim prn As New Printing.PrintDocument

            Using (prn)
                prn.PrinterSettings.PrinterName = "HP LaserJet 2420 PCL 6"
                Dim ps As New Printing.PageSettings

                ps.Landscape = False
                ps.Margins.Top = 1.0
                ps.Margins.Bottom = 1.0
                ps.Margins.Left = 0.75
                ps.Margins.Right = 0.75
                ps.PaperSize = New System.Drawing.Printing.PaperSize("A4", 210, 297)
                prn.DefaultPageSettings = ps

                Dim pf As Font
                pf = New Font("Courier New", 10)

                AddHandler prn.PrintPage, AddressOf Me.PrintPageHandler
                prn.Print()
                RemoveHandler prn.PrintPage, AddressOf Me.PrintPageHandler
            End Using
        End Sub

        Private Sub PrintPageHandler(ByVal sender As Object, ByVal args As Printing.PrintPageEventArgs)
            Dim myFont As New Font("Microsoft San Serif", 10)
            args.Graphics.DrawString(TextToBePrinted, New Font(myFont, FontStyle.Regular), Brushes.Black, 50, 50)
        End Sub

    End Class

    Private Sub BtnStockCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles BtnSOStockCheck.Click
        'ShowMessage(getValueByKey("SO024"), "SO024 - " & getValueByKey("CLAE04"))
        'ShowMessage("Stock Check service currently not available", "Stock Check Informaion")
        'Return Sales Order service currently not available
        'This Return Sales Order service is not active
    End Sub

    Private Sub BtnSOPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles BtnSOPrint.Click
        'PrintSalesOrders(drSiteInfo, drHomeAdds, drDelvAdds)
        ShowMessage(getValueByKey("SO025"), "SO025 - " & getValueByKey("CLAE04"))
        'ShowMessage("Print Sales Order service currently not available", "Print Sales Order Informaion")
    End Sub

    Private Sub BtnSOClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles BtnSOClose.Click
        If dsScan.Tables(0).Rows.Count > 0 Then
            If MsgBox(getValueByKey("SO026"), MsgBoxStyle.YesNo, "SO026 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub CtrlBtnSearchCLP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlBtnSearchCLP.Click
        Try
            'If CustInfo.CtrlTxtCustomerNo.Text <> String.Empty Then
            ' CLPCardType = CtrlCustSearch1.CardType
            ClearCLP()
            CalCulateCLP(CLPCardType, _dsScan.Tables("ItemScanDetails"), "ISCLP=TRUE")
            'lblBalPoint.Text = dsMain.Tables("CashMemoDtl").Compute("SUM(CLPPoints)", "")
            'CustInfo.ctrlTxtPoints.Text = dsMain.Tables("CashMemoDtl").Compute("SUM(CLPPoints)", "")
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    'this is comment to use othercharge form
    'Private Sub CtrlbtnSOOtherCharges_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlbtnSOOtherCharges.Click
    '    Try
    '        Dim objAdj As New frmNAdjustment
    '        objAdj.ShowDialog()
    '        Dim dtOthercharge As DataTable = objAdj.GetCharges
    '        If Not dtOthercharge Is Nothing AndAlso dtOthercharge.Rows.Count > 0 Then
    '            For Each dr As DataRow In dtOthercharge.Rows
    '                Dim dritem As DataRow = _dsScan.Tables("ItemScanDetails").NewRow()
    '                dritem("Articlecode") = dr("article").ToString()
    '                dritem("EAN") = dr("EAN").ToString()
    '                dritem("Quantity") = 1
    '                dritem("Discription") = dr("AdjType").ToString()
    '                dritem("SellingPrice") = dr("AdjAmount").ToString()
    '                dritem("GrossAmt") = dritem("SellingPrice")
    '                dritem("NetAmount") = dritem("SellingPrice")
    '                dritem("PickUpQty") = 0
    '                _dsScan.Tables("ItemScanDetails").Rows.Add(dritem)
    '            Next
    '        End If
    '        _dsScan.AcceptChanges()
    '        CalculateSalesOrderSummary(dsScan)
    '        RefreshScanData(dsScan)
    '        GridSetting()
    '        ' grdScanItem.Refresh()
    '    Catch ex As Exception
    '        ShowMessage(ex.Message, getValueByKey("CLAE05"))
    '        LogException(ex)
    '    End Try
    'End Sub

    Private Sub rbbtnSOEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbbtnSOEdit.Click
        Try
            If (dsScan.Tables(0).Rows.Count > 0) Then
                If MsgBox(getValueByKey("SO047"), MsgBoxStyle.YesNo, "SO047 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then

                    IsFormClosing = True
                    Dim frm As New frmPCNSalesOrderUpdate
                    MDISpectrum.ShowChildForm(frm, True)
                End If
            Else
                Dim frm As New frmPCNSalesOrderUpdate
                MDISpectrum.ShowChildForm(frm, True)
            End If

        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    Private Sub CtrlBtnStockCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlBtnStockCheck.Click
        Try
            Dim objfrmStockCheck As New frmNStockCheck
            objfrmStockCheck.ShowDialog()
            'Dim objStockInfo As New frmStockInfo
            'objStockInfo.ArticleCodeToSearch = grdScanItem.Rows(grdScanItem.Row)("ArticleCode")
            'Dim existingQty As Decimal = SoDeliveryInfo.Sum(Function(x) IIf(x.ArticleCode = objStockInfo.ArticleCodeToSearch And x.Quantity IsNot Nothing, x.Quantity, 0))
            'Dim totalQty As Decimal = grdScanItem.Rows(grdScanItem.Row)("Quantity") - grdScanItem.Rows(grdScanItem.Row)("PickUpQty")
            'objStockInfo.RequiredQuantity = totalQty - existingQty

            'objStockInfo.ShowDialog()
            'If objStockInfo.GridSource.Count > 0 Then
            '    For Each item In objStockInfo.GridSource
            '        If item.Quantity IsNot Nothing AndAlso item.Quantity > 0 Then                                          
            '            item.CreatedAt = clsAdmin.SiteCode
            '            item.SiteCode = clsAdmin.SiteCode
            '            item.CreatedBy = clsAdmin.UserCode
            '            item.IsNew = True
            '            item.Status = True
            '            item.Amount = CalculateDeliveryAmount(grdScanItem.Rows(grdScanItem.Row)("SellingPrice"), item.Quantity)
            '            Dim isExist = SoDeliveryInfo.Where(Function(x) x.ArticleCode = item.ArticleCode AndAlso x.DeliverySiteCode = item.DeliverySiteCode).FirstOrDefault()
            '            If isExist IsNot Nothing Then
            '                isExist.Quantity = item.Quantity + isExist.Quantity
            '                isExist.Amount = CalculateDeliveryAmount(grdScanItem.Rows(grdScanItem.Row)("SellingPrice"), isExist.Quantity)
            '                If isExist.IsNew = False Then
            '                    isExist.IsDirty = True
            '                    isExist.UpdatedAt = clsAdmin.SiteCode
            '                End If
            '            Else
            '                SoDeliveryInfo.Add(item)
            '            End If
            '        End If
            '    Next
            '    dgDeliveryLocation.DataSource = SoDeliveryInfo.Where(Function(x) x.Status = True).ToBindingList()
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function CalculateDeliveryAmount(ByVal rate As Decimal, ByVal qty As Decimal) As Decimal
        'Dim advanceAMount As Double
        'Dim drSOHdr As DataRow
        'Dim findKey(2) As Object
        'findKey(0) = vSiteCode
        'findKey(1) = clsAdmin.Financialyear
        'findKey(2) = CtrlSalesInfo1.CtrlTxtOrderNo.Text
        'drSOHdr = dsMain.Tables("SalesOrderHDR").Rows.Find(findKey)
        ''If Not (dsPayment Is Nothing) AndAlso dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables(0).Rows.Count > 0 Then
        ''    If drSOHdr IsNot Nothing Then
        ''        advanceAMount = CType(IIf(drSOHdr("AdvanceAmt") Is DBNull.Value, 0, drSOHdr("AdvanceAmt")), Double) + CType(dsPayment.Tables(0).Compute("Sum(Amount)", ""), Double)
        ''    Else
        ''        advanceAMount = CType(dsPayment.Tables(0).Compute("Sum(Amount)", ""), Double)
        ''    End If
        ''Else
        ''    advanceAMount = 0.0
        ''End If
        'Dim assignedDeliveryAmount As Decimal = SoDeliveryInfo.Sum(Function(x) x.Amount)
        'Dim totalRemainingAmount As Double = CDbl(CtrlCashSummary1.lbltxt4) - CDbl(CtrlCashSummary1.lbltxt5) - assignedDeliveryAmount
        'Dim deliveryAmount As Decimal
        'If qty * rate < totalRemainingAmount Then
        '    deliveryAmount = qty * rate
        'Else
        '    deliveryAmount = totalRemainingAmount
        'End If
        'Return deliveryAmount
    End Function

    Private Sub RecalculateDeliveryAmt(ByVal totalBillAmt As Decimal, ByVal paidAmt As Decimal)
        Try
            Dim remainingAmt As Decimal = totalBillAmt - paidAmt
            For Each item In SoDeliveryInfo
                If remainingAmt > 0 Then
                    If item.Amount <= remainingAmt Then
                        remainingAmt -= item.Amount
                    Else
                        item.Amount = remainingAmt
                        remainingAmt = 0
                    End If
                Else
                    item.Amount = 0
                End If
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub dgDeliveryInfo_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgDeliveryLocation.DoubleClick
        Try
            Dim objStockInfo As New frmStockInfo
            Dim sellingPrice As Double
            Dim selectedItem As SODeliveryLocationInfo
            If dgDeliveryLocation.Row > 1 Then
                selectedItem = SoDeliveryInfo(dgDeliveryLocation.Row - 1)
            End If
            If selectedItem IsNot Nothing Then
                For Each drScan As DataRow In dsScan.Tables("ItemScanDetails").Rows
                    If drScan("ArticleCode") = selectedItem.ArticleCode Then
                        sellingPrice = drScan("SellingPrice")
                        Exit For
                    End If
                Next
                objStockInfo.RequiredQuantity = selectedItem.Quantity
                objStockInfo.ArticleCodeToSearch = selectedItem.ArticleCode
                objStockInfo.GridSource.Add(selectedItem)
                objStockInfo.ShowDialog()
                If objStockInfo.GridSource.Count > 0 Then
                    Dim insertIndex As Integer = -1
                    If objStockInfo.GridSource.Any(Function(x) x.Quantity IsNot Nothing AndAlso x.Quantity > 0 AndAlso x.DeliverySiteCode = selectedItem.DeliverySiteCode) = False Then
                        insertIndex = SoDeliveryInfo.IndexOf(selectedItem)
                        If selectedItem.IsNew Then
                            SoDeliveryInfo.Remove(selectedItem)
                        Else
                            selectedItem.IsDirty = True
                            selectedItem.Status = False
                        End If
                    End If

                    For Each item In objStockInfo.GridSource
                        If item.Quantity IsNot Nothing AndAlso item.Quantity > 0 Then
                            item.CreatedAt = clsAdmin.SiteCode
                            item.SiteCode = clsAdmin.SiteCode
                            item.CreatedBy = clsAdmin.UserCode
                            item.IsNew = True
                            item.Status = True
                            item.Amount = CalculateDeliveryAmount(sellingPrice, item.Quantity)
                            Dim isExist = SoDeliveryInfo.Where(Function(x) x.ArticleCode = item.ArticleCode AndAlso x.DeliverySiteCode = item.DeliverySiteCode).FirstOrDefault()
                            If isExist IsNot Nothing Then
                                If insertIndex <> -1 Then
                                    isExist.Quantity = item.Quantity + isExist.Quantity
                                Else
                                    isExist.Quantity = item.Quantity
                                End If
                                isExist.Amount = CalculateDeliveryAmount(sellingPrice, isExist.Quantity)
                                If isExist.IsNew = False Then
                                    isExist.IsDirty = True
                                    isExist.UpdatedAt = clsAdmin.SiteCode
                                End If
                            Else
                                'SoDeliveryInfo.Insert(insertIndex, item)
                                SoDeliveryInfo.Add(item)
                            End If
                        End If
                    Next
                    dgDeliveryLocation.DataSource = SoDeliveryInfo.Where(Function(x) x.Status = True).ToBindingList()
                End If
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
#End Region
    Private Function GetTaxableAmountForCst(ByVal strMatcode As String, ByVal EAN As String, ByVal Quantity As Double, ByVal TaxableAmount As Double) As Double
        Dim dtTaxCalc As DataTable
        dtTaxCalc = New DataTable
        dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "SO201", Quantity, EAN, clsDefaultConfiguration.CSTTaxCode, False)
        If dtTaxCalc.Rows.Count > 0 Then
            dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount
            objCM.getCalculatedDataSet(dtTaxCalc)
            Return dtTaxCalc.Compute("SUM(TAXAMOUNT)", "")
        Else
            Return 0
        End If
    End Function
    Private Sub GridSetting()
        Try
            'AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "EAN".ToUpper() _
            For colno = 1 To grdScanItem.Cols.Count - 1
                If grdScanItem.Cols(colno).Name.ToUpper() <> "DISCRIPTION".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "ArticleCode".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "SellingPrice".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "Quantity".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "PickUpQty".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "IsCLP".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "Discount".ToUpper() _
                      AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "TaxPer".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "TotalTaxAmt".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "DEL".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "PLUS".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "SrNo".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "ArticleType".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "UOM".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "ReservedQty".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "DeliverySiteCode".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "PckgSize".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "PckgQty".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "PackagingType".ToUpper() _
                     AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "PackagingMaterial".ToUpper() _
                         AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "NetAmt".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "NetAmount".ToUpper() Then
                    HideColumns(grdScanItem, False, grdScanItem.Cols(colno).Name)
                End If
            Next

            grdScanItem.Cols("DEL").Caption = ""
            grdScanItem.Cols("DEL").Width = 20
            grdScanItem.Cols("DEL").ComboList = "..."
            grdScanItem.Cols("PLUS").Caption = ""
            grdScanItem.Cols("PLUS").Width = 25
            grdScanItem.Cols("SrNo").Width = 45
            grdScanItem.Cols("SrNo").Caption = "Sr. No."
            grdScanItem.Cols("SrNo").DataType = Type.GetType("System.Decimal")
            ''IIf(resourceMgr Is Nothing, "Item Code", getValueByKey("frmcashmemo.DgBulkComboGrid.articlecode"))
            grdScanItem.Cols("SrNo").AllowEditing = False

            If (clsDefaultConfiguration.BarcodeDisplayAllowed) Then
                'added by khusrao Adil
                ' for savoy
                If clsDefaultConfiguration.IsSavoy Then
                    grdScanItem.Cols("EAN").Caption = "Model No."
                Else
                    grdScanItem.Cols("EAN").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.ean")
                End If
                grdScanItem.Cols("EAN").Width = 130
                grdScanItem.Cols("EAN").AllowEditing = False
                grdScanItem.Cols("EAN").Visible = True
            Else
                grdScanItem.Cols("EAN").Visible = False
            End If
            'grdScanItem.Cols("PLUS").ComboList = "..."
            'If (clsDefaultConfiguration.BarcodeDisplayAllowed) Then
            '    grdScanItem.Cols("EAN").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.ean")
            '    grdScanItem.Cols("EAN").Width = 90
            '    grdScanItem.Cols("EAN").AllowEditing = False
            '    grdScanItem.Cols("EAN").Visible = True
            'Else
            '    grdScanItem.Cols("EAN").Visible = False
            'End If
            grdScanItem.Cols("ArticleType").Caption = "Article Type"
            grdScanItem.Cols("ArticleType").Width = 80
            grdScanItem.Cols("ArticleType").AllowEditing = False
            grdScanItem.Cols("ArticleType").Visible = True
            grdScanItem.Cols("ArticleCode").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.articlecode")
            grdScanItem.Cols("ArticleCode").Width = 81
            grdScanItem.Cols("ArticleCode").AllowEditing = False
            grdScanItem.Cols("ArticleCode").Visible = True
            grdScanItem.Cols("Discription").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.discription")
            grdScanItem.Cols("Discription").Width = 303
            grdScanItem.Cols("Discription").AllowEditing = False
            grdScanItem.Cols("UOM").Caption = "UOM"
            grdScanItem.Cols("UOM").Width = 44
            grdScanItem.Cols("UOM").AllowEditing = False
            grdScanItem.Cols("UOM").Visible = True
            'grdScanItem.Cols("SellingPrice").Caption = "Price" 'getValueByKey("frmnsalesordercreation.grdscanitem.sellingprice")
            grdScanItem.Cols("SellingPrice").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.sellingprice")
            grdScanItem.Cols("SellingPrice").Width = 60
            

            ' grdScanItem.Cols("SellingPrice").DataType = Type.GetType("System.Int32")
            grdScanItem.Cols("SellingPrice").DataType = Type.GetType("System.Decimal")
            grdScanItem.Cols("SellingPrice").AllowEditing = False
            'grdScanItem.Cols("SellingPrice").Format = "0"
            grdScanItem.Cols("SellingPrice").Format = "0.00"
            grdScanItem.Cols("Quantity").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.quantity")
            grdScanItem.Cols("Quantity").Width = 69
            'grdScanItem.Cols("Quantity").EditMask = "999999999"
            grdScanItem.Cols("Quantity").DataType = Type.GetType("System.Decimal")
            grdScanItem.Cols("Quantity").Format = "0.000"
            grdScanItem.Cols("PickUpQty").Caption = "Delivered Qty"
            grdScanItem.Cols("PickUpQty").Width = 90
            grdScanItem.Cols("PickUpQty").DataType = Type.GetType("System.Decimal")
            grdScanItem.Cols("PickUpQty").Format = "0.000"
            grdScanItem.Cols("PickUpQty").AllowEditing = False

            'grdScanItem.Cols("PckgSize").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.pickupqty")
            grdScanItem.Cols("PckgSize").Width = 89
            grdScanItem.Cols("PckgSize").DataType = Type.GetType("System.Decimal")
            grdScanItem.Cols("PckgSize").Format = "0.000"
            grdScanItem.Cols("PckgSize").Name = "PckgSize"

            'grdScanItem.Cols("PckgQty").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.pickupqty")
            grdScanItem.Cols("PckgQty").Width = 102
            grdScanItem.Cols("PckgQty").DataType = Type.GetType("System.Decimal")
            grdScanItem.Cols("PckgQty").Format = "0.000"
            grdScanItem.Cols("PckgQty").Name = "PckgQty"
            grdScanItem.Cols("Discount").AllowEditing = False

            grdScanItem.Cols("Discount").DataType = Type.GetType("System.Decimal")
            grdScanItem.Cols("Discount").Format = "0.00"
            grdScanItem.Cols("Discount").Width = 55
            If clsDefaultConfiguration.IsBatchManagementReq Then
                grdScanItem.Cols("PickUpQty").AllowEditing = False
            End If

            Dim DtPackgingTypes = objComn.GetPackagingBox(clsAdmin.SiteCode, 1)  '1=packaging types
            Dim PackagingTypeList As String
            PackagingTypeList = PackagingTypeList & " " & "|"
            For index = 0 To DtPackgingTypes.Rows.Count - 1
                PackagingTypeList = PackagingTypeList & DtPackgingTypes(index)(0) & "|"
            Next index
            If PackagingTypeList.Length > 0 Then
                PackagingTypeList = PackagingTypeList.Substring(0, PackagingTypeList.Length - 1)
            End If

            grdScanItem.Cols("PackagingType").Width = 99
            grdScanItem.Cols("PackagingType").Caption = "Packaging Type"
            '// IIf(resourceMgr Is Nothing, "Disc%", getValueByKey("frmcashmemo.DgBulkComboGrid.quantity"))
            grdScanItem.Cols("PackagingType").AllowEditing = True
            grdScanItem.Cols("PackagingType").ComboList = PackagingTypeList



            dtPackagingBox = objComn.GetPackagingBoxDataSet(clsDefaultConfiguration.PackagingBoxLastNodeCode)
            Dim PackagingMaterialList As String
            For index = 0 To dtPackagingBox.Rows.Count - 1
                PackagingMaterialList = PackagingMaterialList & dtPackagingBox(index)(2) & "|"
            Next index
            If PackagingMaterialList.Length > 0 Then
                PackagingMaterialList = PackagingMaterialList.Substring(0, PackagingMaterialList.Length - 1)
            End If

            grdScanItem.Cols("PackagingMaterial").Width = 123
            grdScanItem.Cols("PackagingMaterial").Caption = "Packaging Material"
            '// IIf(resourceMgr Is Nothing, "Disc%", getValueByKey("frmcashmemo.DgBulkComboGrid.quantity"))
            grdScanItem.Cols("PackagingMaterial").AllowEditing = True
            grdScanItem.Cols("PackagingMaterial").ComboList = PackagingMaterialList

            'grdScanItem.Cols("TOTALDISCPERCENTAGE").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.totaldiscpercentage")
            'grdScanItem.Cols("TOTALDISCPERCENTAGE").Width = 45
            'grdScanItem.Cols("TOTALDISCPERCENTAGE").Format = "0.00"
            'grdScanItem.Cols("TOTALDISCPERCENTAGE").AllowEditing = False
            grdScanItem.Cols("NetAmount").Caption = "Gross Amt" 'getValueByKey("frmnsalesordercreation.grdscanitem.netamount")
            grdScanItem.Cols("NetAmount").Width = 56
            grdScanItem.Cols("NetAmount").DataType = Type.GetType("System.Decimal")  ''"System.Int32"
            grdScanItem.Cols("NetAmount").Format = "0.00"  '' $$ decimal added by nikhil
            grdScanItem.Cols("NetAmount").AllowEditing = False
            grdScanItem.Cols("NetAmount").Width = 90
            '  ---- added newly Column in grid as Net AMount Above Net Amount will display as Amount in fornt End
            grdScanItem.Cols("NetAmt").Caption = "Net Amt"
            grdScanItem.Cols("NetAmt").Width = 56
            grdScanItem.Cols("NetAmt").DataType = Type.GetType("System.Decimal")  ''"System.Int32"
            grdScanItem.Cols("NetAmt").Format = "0.00"  '' $$ decimal added by nikhil
            grdScanItem.Cols("NetAmt").AllowEditing = False
            grdScanItem.Cols("NetAmt").Visible = True
            grdScanItem.Cols("NetAmt").Width = 90
            'grdScanItem.Cols("ExpDelDate").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.expdeldate")
            'grdScanItem.Cols("ExpDelDate").Width = 140
            'grdScanItem.Cols("ExpDelDate").Format = "g"
            'grdScanItem.Cols("Stock").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.stock")
            'grdScanItem.Cols("Stock").Width = 45
            'grdScanItem.Cols("Stock").AllowEditing = False
            grdScanItem.Cols("IsCLP").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.isclp")
            grdScanItem.Cols("IsCLP").Width = 125
            grdScanItem.Cols("ReservedQty").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.reservedqty")
            grdScanItem.Cols("ReservedQty").Width = 90
            grdScanItem.Cols("ReservedQty").Format = "0.000"
            'grdScanItem.Cols("ReservedQty").DataType = Type.GetType("System.Boolean")
            'grdScanItem.Cols("IsCLP").DataType = Type.GetType("System.Boolean")
            'ExclTaxAmt
            grdScanItem.Cols("TotalTaxAmt").Caption = "Article Level Tax" 'getValueByKey("frmnsalesordercreation.grdscanitem.excltaxamt")
            grdScanItem.Cols("TotalTaxAmt").Width = 115
            grdScanItem.Cols("TotalTaxAmt").AllowEditing = False
            grdScanItem.Cols("TotalTaxAmt").Format = "0.00"

            grdScanItem.Cols("TaxPer").Caption = "Tax %" 'getValueByKey("frmnsalesordercreation.grdscanitem.excltaxamt")
            grdScanItem.Cols("TaxPer").Width = 55
            grdScanItem.Cols("TaxPer").AllowEditing = False
            grdScanItem.Cols("TaxPer").Format = "0.00"

            grdScanItem.Cols("DeliverySiteCode").Width = 120
            '  grdScanItem.AutoSizeCols()
            grdScanItem.Cols("Del").Width = 20
            grdScanItem.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.None
            If clsDefaultConfiguration.IsSavoy Then
                grdScanItem.Cols("PackagingMaterial").Visible = False
                grdScanItem.Cols("PckgSize").Visible = False
                grdScanItem.Cols("PckgQty").Visible = False
                grdScanItem.Cols("PackagingType").Visible = False
            End If
            'If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            '    For i = 0 To grdScanItem.Cols.Count - 1
            '        grdScanItem.Cols(i).Caption = grdScanItem.Cols(i).Caption.ToUpper
            '    Next
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub DeliveryGridSetting()
        Try
            For colno = 1 To dgDeliveryLocation.Cols.Count - 1
                If dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "DISCRIPTION".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "EAN".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "ArticleCode".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "Quantity".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "PickUpQty".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "IsCLP".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "DEL".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "PLUS".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "SrNo".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "DispSrNo".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "ArticleType".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "UOM".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "ReservedQty".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "DeliverySiteCode".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "PckgSize".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "PickUpQty".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "PendingQty".ToUpper() _
                     AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "STR".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "DeliveryDate".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "DeliveryTime".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "DeliveryAddress".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "PckgQty".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "PackagingType".ToUpper() _
                     AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "PackagingMaterial".ToUpper() _
                     AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "PickUp".ToUpper() _
                    AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "NetAmount".ToUpper() Then
                    HideColumns(dgDeliveryLocation, False, dgDeliveryLocation.Cols(colno).Name)
                End If
            Next
            ''AndAlso dgDeliveryLocation.Cols(colno).Name.ToUpper() <> "LastPickDate".ToUpper() _

            If clsDefaultConfiguration.PackageFiedlsAllowed Then
                dgDeliveryLocation.Cols("PcKgSize").Visible = True
                dgDeliveryLocation.Cols("PcKgQty").Visible = True
                dgDeliveryLocation.Cols("PcKgSize").AllowEditing = False
                dgDeliveryLocation.Cols("PcKgQty").AllowEditing = False
                dgDeliveryLocation.Cols("PcKgSize").Width = 93
                dgDeliveryLocation.Cols("PcKgQty").Width = 99
            Else
                dgDeliveryLocation.Cols("PcKgSize").Visible = False
                dgDeliveryLocation.Cols("PcKgQty").Visible = False
            End If
            dgDeliveryLocation.Cols("DEL").Caption = ""
            dgDeliveryLocation.Cols("DEL").Width = 20
            dgDeliveryLocation.Cols("DEL").ComboList = "..."
            dgDeliveryLocation.Cols("PLUS").Caption = ""
            dgDeliveryLocation.Cols("PLUS").Width = 23
            dgDeliveryLocation.Cols("SrNo").Width = 47
            dgDeliveryLocation.Cols("SrNo").Caption = "Sr. No."
            dgDeliveryLocation.Cols("SrNo").TextAlign = TextAlignEnum.RightCenter
            ''IIf(resourceMgr Is Nothing, "Item Code", getValueByKey("frmcashmemo.DgBulkComboGrid.articlecode"))
            dgDeliveryLocation.Cols("SrNo").AllowEditing = False
            dgDeliveryLocation.Cols("ArticleType").AllowEditing = False
            dgDeliveryLocation.Cols("ArticleType").Width = 70
            'dgDeliveryLocation.Cols("ArticleType").Width = 40
            dgDeliveryLocation.Cols("Quantity").Width = 70
            'grdScanItem.Cols("Quantity").EditMask = "999999999"
            dgDeliveryLocation.Cols("Quantity").DataType = Type.GetType("System.Decimal")
            dgDeliveryLocation.Cols("Quantity").Format = "0.000"
            dgDeliveryLocation.Cols("Discription").AllowEditing = False
            dgDeliveryLocation.Cols("Discription").Width = 235
            '  dgDeliveryLocation.Cols("Quantity").Width = 45
            dgDeliveryLocation.Cols("PickUpQty").DataType = Type.GetType("System.Decimal")
            dgDeliveryLocation.Cols("PickUpQty").Format = "0.000"
            dgDeliveryLocation.Cols("PickUpQty").Width = 90
            dgDeliveryLocation.Cols("PickUpQty").Caption = "Pckg pickUp" '"Pick Up Qty"
            dgDeliveryLocation.Cols("PickUpQty").Visible = False
            '-------
            dgDeliveryLocation.Cols("PickUp").DataType = Type.GetType("System.Decimal")
            dgDeliveryLocation.Cols("PickUp").Format = "0.000"
            dgDeliveryLocation.Cols("PickUp").Width = 90
            dgDeliveryLocation.Cols("PickUp").Caption = "Pick Up Qty"
            'grdScanItem.Cols("Quantity").EditMask = "999999999"
            dgDeliveryLocation.Cols("PendingQty").DataType = Type.GetType("System.Decimal")
            dgDeliveryLocation.Cols("PendingQty").Format = "0.000"
            dgDeliveryLocation.Cols("PendingQty").AllowEditing = False
            dgDeliveryLocation.Cols("PendingQty").Width = 80
            ' grdScanItem.Cols("DeliveryDate").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.expdeldate")
            dgDeliveryLocation.Cols("DeliveryDate").Width = 115
            dgDeliveryLocation.Cols("DeliveryDate").DataType = Type.GetType("System.DateTime")
            dgDeliveryLocation.Cols("DeliveryDate").TextAlign = TextAlignEnum.CenterCenter
            dgDeliveryLocation.Cols("DeliveryDate").Format = "d"
            dgDeliveryLocation.Cols("DeliveryTime").Width = 115
            dgDeliveryLocation.Cols("DeliveryTime").DataType = Type.GetType("System.DateTime")
            dgDeliveryLocation.Cols("DeliveryTime").TextAlign = TextAlignEnum.CenterCenter
            dgDeliveryLocation.Cols("DeliveryTime").Format = "HH:mm"
            dgDeliveryLocation.Cols("UOM").AllowEditing = False
            dgDeliveryLocation.Cols("PackagingMaterial").AllowEditing = False
            Dim isFirstSite As Integer = 0
            Dim AddressList As String
            Dim IsAddresAdded As Boolean = False
          For index = 0 To dtTempOrderAddresses.Rows.Count - 1
                If dtTempOrderAddresses(index)(1).trim <> "" AndAlso dtTempOrderAddresses(index)(1).trim <> "" Then
                    If dtTempOrderAddresses(index)(2) = "Store" AndAlso isFirstSite = 0 Then
                        isFirstSite = 1
                    End If
                    If isCustSelected AndAlso isFirstSite = 1 AndAlso dtTempOrderAddresses(index)(2) = "Store" AndAlso IsAddresAdded Then
                        isFirstSite = 2
                        AddressList = AddressList & "-----------------------------" & "|"
                        AddressList = AddressList & dtTempOrderAddresses(index)(1) & "|"
                    Else
                        If dtTempOrderAddresses(index)(2) = "Address" Then
                            IsAddresAdded = True
                        End If

                        AddressList = AddressList & dtTempOrderAddresses(index)(1) & "|"
                    End If

                End If

            Next index
            If AddressList.Length > 0 Then
                AddressList = AddressList.Substring(0, AddressList.Length - 1)
            End If
            dgDeliveryLocation.Cols("PackagingMaterial").Width = 140
            dgDeliveryLocation.Cols("DeliveryAddress").Width = 215
            'grdScanItem.Cols("DeliveryAddress").Caption = "Packaging Type"
            '// IIf(resourceMgr Is Nothing, "Disc%", getValueByKey("frmcashmemo.DgBulkComboGrid.quantity"))
            dgDeliveryLocation.Cols("DeliveryAddress").AllowEditing = True
            dgDeliveryLocation.Cols("DeliveryAddress").ComboList = AddressList
            dgDeliveryLocation.Cols("DispSrNo").DataType = Type.GetType("System.Decimal")
            'grdScanItem.Cols("PLUS").ComboList = "..."
            'If (clsDefaultConfiguration.BarcodeDisplayAllowed) Then
            '    grdScanItem.Cols("EAN").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.ean")
            '    grdScanItem.Cols("EAN").Width = 90
            '    grdScanItem.Cols("EAN").AllowEditing = False
            '    grdScanItem.Cols("EAN").Visible = True
            'Else
            '    grdScanItem.Cols("EAN").Visible = False
            'End If
            'grdScanItem.Cols("ArticleType").Caption = "Article Type"
            grdScanItem.Cols("DISCRIPTION").Width = 220
            'added by Khusrao Adil
            ' for savoy
            If clsDefaultConfiguration.BarcodeDisplayAllowed Then
                If clsDefaultConfiguration.IsSavoy Then
                    grdScanItem.Cols("EAN").Caption = "Model No."
                    grdScanItem.Cols("EAN").AllowEditing = False
                    grdScanItem.Cols("EAN").Width = 150
                    grdScanItem.Cols("EAN").Visible = True
                End If

            End If
            'grdScanItem.Cols("ArticleType").AllowEditing = False
            'grdScanItem.Cols("ArticleType").Visible = True
            'grdScanItem.Cols("ArticleCode").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.articlecode")
            ' grdScanItem.Cols("ArticleCode").Width = 260
            'grdScanItem.Cols("ArticleCode").AllowEditing = False
            'grdScanItem.Cols("ArticleCode").Visible = True
            'grdScanItem.Cols("Discription").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.discription")
            'grdScanItem.Cols("Discription").Width = 150
            'grdScanItem.Cols("Discription").AllowEditing = False
            'grdScanItem.Cols("UOM").Caption = "UOM"
            'grdScanItem.Cols("UOM").Width = 90
            'grdScanItem.Cols("UOM").AllowEditing = False
            'grdScanItem.Cols("UOM").Visible = True
            ''grdScanItem.Cols("SellingPrice").Caption = "Price" 'getValueByKey("frmnsalesordercreation.grdscanitem.sellingprice")
            'grdScanItem.Cols("SellingPrice").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.sellingprice")
            'grdScanItem.Cols("SellingPrice").Width = 60
            'grdScanItem.Cols("SellingPrice").AllowEditing = False
            'grdScanItem.Cols("Quantity").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.quantity")
            'grdScanItem.Cols("Quantity").Width = 45
            ''grdScanItem.Cols("Quantity").EditMask = "999999999"
            'grdScanItem.Cols("Quantity").Format = "0.000"
            'grdScanItem.Cols("PickUpQty").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.pickupqty")
            'grdScanItem.Cols("PickUpQty").Width = 45
            'grdScanItem.Cols("PickUpQty").Format = "0.000"
            'If clsDefaultConfiguration.IsBatchManagementReq Then
            '    grdScanItem.Cols("PickUpQty").AllowEditing = False
            'End If

            ''grdScanItem.Cols("TOTALDISCPERCENTAGE").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.totaldiscpercentage")
            ''grdScanItem.Cols("TOTALDISCPERCENTAGE").Width = 45
            ''grdScanItem.Cols("TOTALDISCPERCENTAGE").Format = "0.00"
            ''grdScanItem.Cols("TOTALDISCPERCENTAGE").AllowEditing = False
            'grdScanItem.Cols("NetAmount").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.netamount")
            'grdScanItem.Cols("NetAmount").Width = 70
            'grdScanItem.Cols("NetAmount").Format = "0.00"
            'grdScanItem.Cols("NetAmount").AllowEditing = False
            ''grdScanItem.Cols("ExpDelDate").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.expdeldate")
            ''grdScanItem.Cols("ExpDelDate").Width = 140
            ''grdScanItem.Cols("ExpDelDate").Format = "g"
            ''grdScanItem.Cols("Stock").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.stock")
            ''grdScanItem.Cols("Stock").Width = 45
            ''grdScanItem.Cols("Stock").AllowEditing = False
            'grdScanItem.Cols("IsCLP").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.isclp")
            'grdScanItem.Cols("IsCLP").Width = 45
            'grdScanItem.Cols("ReservedQty").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.reservedqty")
            'grdScanItem.Cols("ReservedQty").Width = 45
            'grdScanItem.Cols("ReservedQty").Format = "0.000"
            ''grdScanItem.Cols("ReservedQty").DataType = Type.GetType("System.Boolean")
            ''grdScanItem.Cols("IsCLP").DataType = Type.GetType("System.Boolean")
            ''ExclTaxAmt
            'grdScanItem.Cols("TotalTaxAmt").Caption = "Article Level Tax" 'getValueByKey("frmnsalesordercreation.grdscanitem.excltaxamt")
            'grdScanItem.Cols("TotalTaxAmt").Width = 45
            'grdScanItem.Cols("TotalTaxAmt").AllowEditing = False
            'grdScanItem.Cols("TotalTaxAmt").Format = "0.00"
            grdScanItem.AutoSizeCols()
            'grdScanItem.Cols("Del").Width = 20
            dgDeliveryLocation.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.None
            If clsDefaultConfiguration.IsSavoy Then
                dgDeliveryLocation.Cols("PackagingMaterial").Visible = False
                dgDeliveryLocation.Cols("PckgSize").Visible = False
                dgDeliveryLocation.Cols("PckgQty").Visible = False
                dgDeliveryLocation.Cols("PackagingType").Visible = False
                dgDeliveryLocation.Cols("STR").Visible = False
            End If
            'If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            '    For i = 0 To dgDeliveryLocation.Cols.Count - 1
            '        dgDeliveryLocation.Cols(i).Caption = dgDeliveryLocation.Cols(i).Caption.ToUpper
            '    Next
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Sub DisablebuttonsForSavoy()
        cmdGenerateSTR.Visible = False
        lblStrRaised.Visible = False
        lblDispStrRaised.Visible = False
    End Sub
    Private Sub ClearCLP()
        Try
            For Each dr As DataRow In _dsScan.Tables("ItemScanDetails").Rows
                dr("CLPPoints") = DBNull.Value
                dr("CLPDiscount") = DBNull.Value
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub rbbtnClrAllPromo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbbtnClrAllPromo.Click
        If dsScan.Tables(0).Rows.Count > 0 Then
            If IsApplyPromotion = True Then
                RemoveApplyPromotion(_dsScan, _dsPackagingVar)
            End If
            '' Reset Tax before add discount Changes By PC tax Issue Added by Ketan
            Dim foundRow As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("IsDocumentLevel = True")
            If foundRow.Count > 0 Then
                ResetDocTax(True, dtDocTaxes)
                dtDocTaxes.Rows.Clear()
            End If
            dsScan.AcceptChanges()
            _dsPackagingVar.AcceptChanges()
            For Each drDisc As DataRow In _dsScan.Tables(0).Rows

                ' If Not (IIf(IsDBNull(drDisc("TotalDiscPercentage")), 0, drDisc("TotalDiscPercentage")) = 0) Then

                If (Not String.IsNullOrEmpty(drDisc("CLPDiscount").ToString())) Then
                    drDisc("Discount") = CDbl(drDisc("LineDiscount").ToString()) + CDbl(drDisc("CLPDiscount").ToString())
                End If

                If drDisc("GrossAmt") > 0 Then
                    drDisc("TotalDiscPercentage") = (IIf(drDisc("Discount") Is DBNull.Value, 0, drDisc("Discount")) * 100) / drDisc("GrossAmt")
                Else
                    drDisc("TotalDiscPercentage") = 0
                End If
                drDisc("PromotionId") = IIf(drDisc("FirstLevel") = String.Empty, 0, drDisc("FirstLevel")) & "," & IIf(drDisc("TopLevel") = String.Empty, 0, drDisc("TopLevel"))

                'If drDisc("PromotionId") = "0,0" Then
                '    drDisc("LineDiscount") = (drDisc("GrossAmt") * drDisc("TotalDiscPercentage")) / 100
                'End If
                Dim totalamt As Decimal = 0
                If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                    totalamt = drDisc("GrossAmt") - drDisc("Discount")
                    Dim objcom As New clsSaleOrderCommon
                    objcom.IsCSTApplicable = IsCSTApplicable
                    '  objcom.CreateDataSetForTaxCalculation(drDisc("ARTICLECODE"), totalamt, drDisc, dsMain, CtrlSalesInfo1.CtrlTxtOrderNo.Text, drDisc("EAN"), True)
                End If

                drDisc("NetAmount") = drDisc("GrossAmt") - drDisc("LineDiscount") + IIf(drDisc("ExclTaxAmt") Is DBNull.Value, 0, drDisc("ExclTaxAmt")) + IIf(drDisc("IncTaxAmt") Is DBNull.Value, 0, drDisc("IncTaxAmt"))
                'drDisc("MinPayAmt") = Math.Round((drDisc("NetAmount") / drDisc("Quantity")) * drDisc("PickUpQty"), 3)

                TotalSalesQty = drDisc("PickUpQty") + IIf(drDisc("DeliveredQty") IsNot DBNull.Value, drDisc("DeliveredQty"), 0)
                NetArticleRate = drDisc("NetAmount") / drDisc("Quantity")
                drDisc("MinPayAmt") = ((drDisc("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate)
                drDisc("TotalPickUpAmt") = (drDisc("PickupQty") * NetArticleRate)

                '    End If
                Dim dtTaxCalc As DataTable
                If IsCSTApplicable Then
                    dtTaxCalc = objCM.getTax(vSiteCode, String.Empty, "SO201", drDisc("Quantity"), drDisc("EAN"), clsDefaultConfiguration.CSTTaxCode, True)
                Else
                    dtTaxCalc = objCM.getTax(vSiteCode, drDisc.Item("ArticleCode"), "SO201", drDisc("Quantity"), drDisc("EAN"), clsDefaultConfiguration.CSTTaxCode, False)
                End If
                If drDisc("ArticleType") = "Combo" Then
                    If lblArticleCombo.Rows.Count > 0 Then  ''$$ added by nikhil
                        Dim TaxAmt As Double = (Math.Round(drDisc.Item("SellingPrice") * drDisc.Item("Quantity"), 3) - drDisc.Item("Discount")) * CDbl(lblArticleCombo.Rows(0)("Tax") / 100)
                        Dim vTotalNetAmt = (Math.Round(drDisc.Item("SellingPrice") * drDisc.Item("Quantity"), 3) - drDisc.Item("Discount") + TaxAmt) ''' added by nikhil
                        'dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = vTotalNetAmt.ToString
                        ' objCM.getCalculatedDataSet(dtTaxCalc)
                        If drDisc("ArticleType") = "Combo" Then
                            drDisc("TaxInPer") = lblArticleCombo.Rows(0)("Tax") '' $$ added by nikhil
                            drDisc("TotalTaxAmt") = Math.Round(TaxAmt, 2)
                            drDisc("ExclTaxAmt") = Math.Round(TaxAmt, 2) '''Math.Round(dtTaxCalc.Compute("SUM(TAXAMOUNT)", "")) $$ added by nikhil
                        Else
                            drDisc("ExclTaxAmt") = Math.Round(dtTaxCalc.Compute("SUM(TAXAMOUNT)", ""))
                            drDisc("TotalTaxAmt") = Math.Round(dtTaxCalc.Compute("SUM(TAXAMOUNT)", ""))
                        End If

                        drDisc("NetAmount") = vTotalNetAmt
                    End If
                Else
                    If dtTaxCalc.Rows.Count > 0 Then
                        Dim vTotalNetAmt = Math.Round(drDisc.Item("SellingPrice") * drDisc.Item("Quantity"), 3) - drDisc.Item("Discount")
                        dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = vTotalNetAmt
                        objCM.getCalculatedDataSet(dtTaxCalc)
                        drDisc("ExclTaxAmt") = Math.Round(dtTaxCalc.Compute("SUM(TAXAMOUNT)", ""), 2)
                        drDisc("TotalTaxAmt") = Math.Round(dtTaxCalc.Compute("SUM(TAXAMOUNT)", ""), 2)
                        drDisc("NetAmount") = vTotalNetAmt + drDisc("TotalTaxAmt")
                        TotalSalesQty = drDisc("PickUpQty") + IIf(drDisc("DeliveredQty") IsNot DBNull.Value, drDisc("DeliveredQty"), 0)
                        NetArticleRate = drDisc("NetAmount") / drDisc("Quantity")
                        drDisc("MinPayAmt") = ((drDisc("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate)

                    End If
                End If
            Next
            _dsScan.AcceptChanges()
            For Each drDisc As DataRow In _dsPackagingVar.Tables(0).Rows

                ' If Not (IIf(IsDBNull(drDisc("TotalDiscPercentage")), 0, drDisc("TotalDiscPercentage")) = 0) Then

                If (Not String.IsNullOrEmpty(drDisc("CLPDiscount").ToString())) Then
                    drDisc("Discount") = CDbl(drDisc("LineDiscount").ToString()) + CDbl(drDisc("CLPDiscount").ToString())
                End If

                If drDisc("GrossAmt") > 0 Then
                    drDisc("TotalDiscPercentage") = (IIf(drDisc("Discount") Is DBNull.Value, 0, drDisc("Discount")) * 100) / drDisc("GrossAmt")
                Else
                    drDisc("TotalDiscPercentage") = 0
                End If
                drDisc("PromotionId") = IIf(drDisc("FirstLevel") = String.Empty, 0, drDisc("FirstLevel")) & "," & IIf(drDisc("TopLevel") = String.Empty, 0, drDisc("TopLevel"))

                'If drDisc("PromotionId") = "0,0" Then
                '    drDisc("LineDiscount") = (drDisc("GrossAmt") * drDisc("TotalDiscPercentage")) / 100
                'End If
                Dim totalamt As Decimal = 0
                If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                    totalamt = drDisc("GrossAmt") - drDisc("Discount")
                    Dim objcom As New clsSaleOrderCommon
                    objcom.IsCSTApplicable = IsCSTApplicable
                    '  objcom.CreateDataSetForTaxCalculation(drDisc("ARTICLECODE"), totalamt, drDisc, dsMain, CtrlSalesInfo1.CtrlTxtOrderNo.Text, drDisc("EAN"), True)
                End If

                drDisc("NetAmount") = drDisc("GrossAmt") - drDisc("LineDiscount") + IIf(drDisc("ExclTaxAmt") Is DBNull.Value, 0, drDisc("ExclTaxAmt")) + IIf(drDisc("IncTaxAmt") Is DBNull.Value, 0, drDisc("IncTaxAmt"))
                'drDisc("MinPayAmt") = Math.Round((drDisc("NetAmount") / drDisc("Quantity")) * drDisc("PickUpQty"), 3)

                TotalSalesQty = drDisc("PickUpQty") + IIf(drDisc("DeliveredQty") IsNot DBNull.Value, drDisc("DeliveredQty"), 0)
                NetArticleRate = drDisc("NetAmount") / drDisc("Quantity")
                drDisc("MinPayAmt") = ((drDisc("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate)
                drDisc("TotalPickUpAmt") = (drDisc("PickupQty") * NetArticleRate)

                '    End If
                Dim dtTaxCalc As DataTable
                If IsCSTApplicable Then
                    dtTaxCalc = objCM.getTax(vSiteCode, String.Empty, "SO201", drDisc("Quantity"), drDisc("EAN"), clsDefaultConfiguration.CSTTaxCode, True)
                Else
                    dtTaxCalc = objCM.getTax(vSiteCode, drDisc.Item("ArticleCode"), "SO201", drDisc("Quantity"), drDisc("EAN"), clsDefaultConfiguration.CSTTaxCode, False)
                End If
                If drDisc("ArticleType") = "Combo" Then
                    If lblArticleCombo.Rows.Count > 0 Then  ''$$ added by nikhil
                        Dim TaxAmt As Double = (Math.Round(drDisc.Item("SellingPrice") * drDisc.Item("Quantity"), 3) - drDisc.Item("Discount")) * CDbl(lblArticleCombo.Rows(0)("Tax") / 100)
                        Dim vTotalNetAmt = (Math.Round(drDisc.Item("SellingPrice") * drDisc.Item("Quantity"), 3) - drDisc.Item("Discount") + TaxAmt) ''' added by nikhil
                        'dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = vTotalNetAmt.ToString
                        ' objCM.getCalculatedDataSet(dtTaxCalc)
                        If drDisc("ArticleType") = "Combo" Then
                            drDisc("TaxInPer") = lblArticleCombo.Rows(0)("Tax")   ''$$ added by nikhil
                            drDisc("TotalTaxAmt") = Math.Round(TaxAmt, 2)
                            drDisc("ExclTaxAmt") = Math.Round(TaxAmt, 2) '''Math.Round(dtTaxCalc.Compute("SUM(TAXAMOUNT)", "")) $$ added by nikhil
                        Else
                            drDisc("ExclTaxAmt") = Math.Round(dtTaxCalc.Compute("SUM(TAXAMOUNT)", ""))
                            drDisc("TotalTaxAmt") = Math.Round(dtTaxCalc.Compute("SUM(TAXAMOUNT)", ""))
                        End If

                        drDisc("NetAmount") = vTotalNetAmt
                    End If
                Else
                    If dtTaxCalc.Rows.Count > 0 Then
                        Dim vTotalNetAmt = Math.Round(drDisc.Item("SellingPrice") * drDisc.Item("Quantity"), 3) - drDisc.Item("Discount")
                        dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = vTotalNetAmt
                        objCM.getCalculatedDataSet(dtTaxCalc)
                        drDisc("ExclTaxAmt") = Math.Round(dtTaxCalc.Compute("SUM(TAXAMOUNT)", ""))
                        drDisc("TotalTaxAmt") = Math.Round(dtTaxCalc.Compute("SUM(TAXAMOUNT)", ""))
                        drDisc("NetAmount") = vTotalNetAmt + drDisc("TotalTaxAmt")
                        TotalSalesQty = drDisc("PickUpQty") + IIf(drDisc("DeliveredQty") IsNot DBNull.Value, drDisc("DeliveredQty"), 0)
                        NetArticleRate = drDisc("NetAmount") / drDisc("Quantity")
                        drDisc("MinPayAmt") = ((drDisc("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate)
                        drDisc("TotalPickUpAmt") = (drDisc("PickupQty") * NetArticleRate)

                    End If
                End If
            Next
            _dsPackagingVar.AcceptChanges()

            CalculateSalesOrderSummary(dsScan)
            RefreshScanData(dsScan)
            GridSetting()
        End If
    End Sub

    Private Sub rbbtnClearSelectedPromo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbbtnClearSelectedPromo.Click
        Try
            For Each dr As C1.Win.C1FlexGrid.Row In grdScanItem.Rows.Selected
                If IsApplyPromotion = True Then
                    For Each drdata As DataRow In dsScan.Tables(0).Select("EAN='" & dr("EAN").ToString() & "' AND ArticleCode='" & dr("ArticleCode").ToString() & "'", "", DataViewRowState.CurrentRows)
                        drdata("Discount") = 0
                        drdata("PromotionId") = 0
                        drdata("LineDiscount") = 0
                        drdata("TotalDiscPercentage") = 0
                        drdata("FirstLevel") = String.Empty
                        drdata("TopLevel") = String.Empty
                        Dim obj As New clsSaleOrderCommon
                        ' obj.RecalculateLine(drdata, CtrlSalesInfo1.CtrlTxtOrderNo.Text, dsMain)
                    Next
                End If
            Next
            CalculateSalesOrderSummary(dsScan)
            RefreshScanData(dsScan)
            GridSetting()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    'Private Sub grdScanItem_StartEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdScanItem.StartEdit
    '    Try
    '        If grdScanItem.Row >= 1 AndAlso (grdScanItem.Cols(e.Col).Name = "Quantity" Or grdScanItem.Cols(e.Col).Name = "PickUpQty") Then
    '            grdScanItem.Rows(e.Row)(e.Col).select()
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub rbbtnSOCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbbtnSOCancel.Click
        Try
            Try
                If dsScan.Tables(0).Rows.Count > 0 Then
                    If MsgBox(getValueByKey("SO028"), MsgBoxStyle.YesNo, "SO028 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                        Dim frm As New frmPCNSalesOrderUpdate
                        frm.SoCancel = True
                        MDISpectrum.ShowChildForm(frm, True)
                    End If
                Else
                    'Dim frm As New frmNSalesOrderCancel
                    'MDISpectrum.ShowChildForm(frm, True)
                    Dim frm As New frmPCNSalesOrderUpdate
                    frm.SoCancel = True
                    MDISpectrum.ShowChildForm(frm, True)
                End If

            Catch ex As Exception
                LogException(ex)
            End Try

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Protected Overrides Function ProcessKeyPreview(ByRef m As System.Windows.Forms.Message) As Boolean
        Const WM_KEYDOWN As Integer = &H100
        '    MsgBox(Me.ActiveControl.ToString & " = " & Me.ActiveControl.TabStop)
        If m.Msg = WM_KEYDOWN Then
            Select Case m.WParam.ToInt32
                Case Keys.F
                    If My.Computer.Keyboard.CtrlKeyDown Then
                        'Debug.Print("Ctrl + F")
                        BtnSearchItem_Click(CtrlSalesPersons.CtrlTxtBox, New KeyEventArgs(Keys.Enter))
                    End If
                Case Keys.M
                    If My.Computer.Keyboard.CtrlKeyDown Then
                        'Debug.Print("Ctrl + M")
                        '   CtrlCustDtls1.CtrlLabel3_Click(Nothing, New KeyEventArgs(Keys.Enter))
                    End If
                Case Keys.Q
                    If My.Computer.Keyboard.CtrlKeyDown Then
                        'Debug.Print("Ctrl + Q") ' Direct to Cash Memo
                        ' Create a new instance of the child form.
                        Dim ChildForm As New Spectrum.frmCashMemo
                        If ChildForm.Name <> String.Empty Then
                            MDISpectrum.MenuStrip.Hide()
                            MDISpectrum.ShowChildForm(ChildForm, True)
                            MDISpectrum.MenuStrip.Hide()
                        End If
                    End If
                Case Keys.N
                    If My.Computer.Keyboard.CtrlKeyDown Then
                        If IsBooking = False Then
                            BtnSONew_Click(Nothing, New System.EventArgs)
                        End If
                    End If
                Case Keys.B AndAlso rbbtnSOBooking.Visible
                        If My.Computer.Keyboard.CtrlKeyDown Then
                            rbbtnSOBooking_Click(Nothing, New System.EventArgs)
                        End If
                Case Keys.G AndAlso rbbtnSOBookingEdit.Visible
                        If My.Computer.Keyboard.CtrlKeyDown Then
                            rbbtnSOBookingEdit_Click(Nothing, New System.EventArgs)
                        End If
                Case Keys.E
                    If My.Computer.Keyboard.CtrlKeyDown Then
                        If IsBooking = False Then
                            rbbtnSOEdit_Click(Nothing, New System.EventArgs)
                        End If
                    End If

                Case Keys.X
                    If My.Computer.Keyboard.CtrlKeyDown Then
                        If IsBooking = False Then
                            rbbtnSOCancel_Click(Nothing, New System.EventArgs)
                        End If
                    End If
                Case Keys.S
                        If My.Computer.Keyboard.CtrlKeyDown Then
                            BtnSaveSalesOrder_Click(Nothing, New System.EventArgs)
                            '   CtrlCustSearch1.CtrlBtn1_Click(CtrlCustSearch1.CtrlBtn1, New System.EventArgs)
                        End If
                Case Keys.F2
                        ChangeQty()
            End Select
        End If
        Return MyBase.ProcessKeyPreview(m)
    End Function
    Private Sub ChangeQty()
        Try
            If grdScanItem.Rows.Count >= 1 Then
                grdScanItem.Focus()
                grdScanItem.Select(1, 4)
            End If

        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    Private Sub PriceChange()
        If clsDefaultConfiguration.PriceChageAllowed = True Then
            If CheckInterTransactionAuth("PriceChange", dsMain.Tables("CashMemoDtl")) = True Then
                Dim frm As New frmSpecialPrompt(getValueByKey("SP002"))
                frm.ShowTextBox = True
                frm.txtValue.MaxLength = 14
                frm.AllowDecimal = True
                frm.AcceptButton = frm.cmdOk
                frm.IsNumeric = True
                frm.ShowDialog()
                If frm.GetResult IsNot Nothing Then
                    'grdScanItem.Rows(1)("SellingPrice") = IIf(frm.GetResult Is Nothing, 1, frm.GetResult)
                    grdScanItem.Rows(grdScanItem.RowSel)("SellingPrice") = IIf(frm.GetResult Is Nothing, 1, frm.GetResult)
                    Dim index As Int32 = grdScanItem.Cols("SellingPrice").Index
                    grdScanItem_AfterEdit(grdScanItem, New C1.Win.C1FlexGrid.RowColEventArgs(grdScanItem.RowSel, index))
                End If
            End If
        End If
    End Sub

    Private Sub grdScanItem_ValidateEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.ValidateEditEventArgs) Handles grdScanItem.ValidateEdit
        If grdScanItem.Cols(e.Col).Name.ToUpper = "QUANTITY" Then
            If grdScanItem.Editor.Text.Length > 9 Then
                'CM059() " Qty cannot be greater then 999999999
                If Val(grdScanItem.Editor.Text) > 999999999 Then
                    ShowMessage(getValueByKey("CM059"), "CM059 - " & getValueByKey("CLAE05"))
                    e.Cancel = True
                End If
            End If
        End If

    End Sub


    Private Sub BtnSelectDeliveryLoc_Click(sender As System.Object, e As System.EventArgs)
        Try
            Dim objStockInfo As New frmStockInfo
            objStockInfo.ShowDialog()
            If Not String.IsNullOrEmpty(objStockInfo._SelectedValue) Then
                DeliverySiteCode = objStockInfo._SelectedValue
                For Each drScan As DataRow In dsScan.Tables("ItemScanDetails").Rows
                    If IsDBNull(drScan("BatchBarcode")) Then
                        drScan("DeliverySiteCode") = objStockInfo._SelectedValue
                    End If
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub rbnCST_Click(sender As System.Object, e As System.EventArgs) Handles rbnCST.Click
        Try
            If dsScan.Tables(0).Rows.Count = 0 Then
                ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If

            For drindex = 0 To dsPackagingVar.Tables(0).Rows.Count - 1

                If dsPackagingVar.Tables(0).Rows(drindex)("ArticleType") = "Combo" Then
                    If dsPackagingVar.Tables(0).Rows(drindex)("PackagingMaterial").ToString() = "" Then
                        ShowMessage("Please select Packaging material for Combo article", " " & getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                End If
            Next

            Dim EventType As Int32
            'ShowMessage(getValueByKey("CST001"), getValueByKey("CLAE04"), EventType, True, getValueByKey("mod009"))
            'If EventType = 1 Then
            Dim dtList = objComn.GetAllTaxesAppliedToSiteDocumentLevel(clsAdmin.SiteCode, "SO201")
            If dtList Is Nothing OrElse dtList.Rows.Count <= 0 Then
                ShowMessage("No Taxes available for this document type", getValueByKey("CLAE04"))
                Exit Sub
            End If
            'Dim objview As New frmNCommonSearch
            'objview.SetData = dtList
            'objview.showdialog()
            Dim objView As New frmPCApplyTax
            objView.dtBind = dtDocTaxes.Copy()
            '' Minus Discount from Gross amount For tax Calculation Added by Ketan
            Dim DiscountValue = CtrllblDiscAmt.Text
            objView.GrossAmt = CtrllblGrossAmt.Text - DiscountValue
            'objView.SetData = dtList
            If (objView.ShowDialog() = Windows.Forms.DialogResult.OK) Then

                dtDocTaxes = objView.dtBind
                '' added By ketan as discussed with prasad Savoy tax Changes ...
                ResetNewTax(True, dtDocTaxes)
                If clsDefaultConfiguration.IsSavoy Then
                    Dim TaxCalc As Double = 0
                    Dim TaxValue As Double
                    If Not dsMain.Tables("SalesOrderTaxDtls") Is Nothing AndAlso dsMain.Tables("SalesOrderTaxDtls").Rows.Count > 0 Then
                        Dim dr() = dsMain.Tables("SalesOrderTaxDtls").Select("Status=True")
                        If dr.Count > 0 Then
                            TaxValue = dr(0)("TaxValue")
                        End If
                    End If
                    For Each drDisc As DataRow In dsPackagingVar.Tables(0).Rows
                        TaxCalc = ((drDisc("SellingPrice") * drDisc("PickupQty")) / dsPackagingVar.Tables(0).Compute("Sum(GrossAmt)", " ")) * TaxValue
                        drDisc("MinPayAmt") = (drDisc("SellingPrice") * drDisc("PickupQty")) + TaxCalc
                        drDisc("TotalPickUpAmt") = (drDisc("SellingPrice") * drDisc("PickupQty")) + TaxCalc
                    Next
                End If
                'If Not objview.search Is Nothing Then
                '    clsDefaultConfiguration.CSTTaxCode = objview.search(1)
                'Else
                '    Exit Sub
                'End If
                'IsCSTApplicable = True
                'ResetNewTax(True, dtDocTaxes)
                CalculateSalesOrderSummary(_dsScan)
                RefreshScanData(dsScan)
                'rbnCST.Enabled = False
            End If
            'objView.ShowDialog()
            'If Not objView.search Is Nothing Then
            '    clsDefaultConfiguration.CSTTaxCode = objView.search(1)
            'Else
            '    Exit Sub
            'End If

            'End If


            'Dim res = MessageBox.Show(getValueByKey("CST001"), getValueByKey("CLAE04"), MessageBoxButtons.YesNo)
            'If res = Windows.Forms.DialogResult.Yes Then
            '    IsCSTApplicable = True
            '    ResetTax(True)

            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub SetQuotationArticles()
        CtrlSalesPersons.CtrlSalesPersons.SelectedIndex = 0
        If Not dsMain.Tables("Quotationdtl") Is Nothing AndAlso dsMain.Tables("Quotationdtl").Rows.Count > 0 Then
            Boolean.TryParse(dsMain.Tables("QuotationHDR")(0)("IsCSTApplied"), IsCSTApplicable)

            If IsCSTApplicable Then
                clsDefaultConfiguration.CSTTaxCode = dsMain.Tables("QuotationHDR")(0)("CSTTaxCode")
                rbnCST.Enabled = False
            End If

            dsMain.Tables("Quotationdtl").Columns("PromisedDeliveryDate").ColumnName = "ExpDelDate"
            dsMain.Tables("Quotationdtl").Columns("TotalTaxAmount").ColumnName = "TotalTaxAmt"
            dsMain.Tables("Quotationdtl").Columns("DiscountAmount").ColumnName = "Discount"
            dsMain.Tables("Quotationdtl").Columns("DiscountPercentage").ColumnName = "TotalDiscPercentage"
            dsMain.Tables("Quotationdtl").Columns("GrossAmount").ColumnName = "GrossAmt"
            dsMain.Tables("Quotationdtl").Columns("UnitofMeasure").ColumnName = "UOM"
            ' dsMain.Tables("Quotationdtl").Columns.Add("PickUpQty", vbDecimal.GetType())

            _dsScan.Tables("ItemScanDetails").Columns("PickUpQty").DefaultValue = 0
            _dsScan.Tables("ItemScanDetails").Columns("DeliverySiteCode").DefaultValue = clsAdmin.SiteCode
            _dsScan.Tables("ItemScanDetails").Merge(dsMain.Tables("Quotationdtl"), True, MissingSchemaAction.Ignore)



            For Each dr As DataRow In _dsScan.Tables("ItemScanDetails").Rows
                TotalSalesQty = IIf(dr("PickUpQty").ToString() = "", 0, dr("PickUpQty")) + IIf(dr("DeliveredQty").ToString() = "", 0, dr("DeliveredQty"))
                NetArticleRate = dr("NetAmount") / dr("Quantity")
                dr("MinPayAmt") = ((dr("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate)
                dr("IsCLP") = True
            Next

            If Not QuotationOtherCharges Is Nothing Then
                dtOtherCharges = QuotationOtherCharges
            End If

            dsMain.Tables.Remove("Quotationdtl")
            'If dsMain.Tables("Quotationhdr").Rows.Count > 0 Then
            '    dsMain.Tables("Quotationhdr")(0)("SOStatus") = "Closed"

            '    If dsMain.Tables("quotationhdr")(0)("CustomerType").ToString().ToUpper() = "CLP" Then
            '        CtrlCustSearch1.rbCLPMember.Checked = True
            '    Else
            '        CtrlCustSearch1.rbOtherCust.Checked = True
            '    End If

            '    CtrlCustSearch1.CtrlTxtCustNo.Text = dsMain.Tables("quotationhdr")(0)("CustomerNo")
            '    CtrlCustDtls1.lblCustNoValue.Text = dsMain.Tables("quotationhdr")(0)("CustomerNo")
            '    CtrlCustSearch1.CustmType = dsMain.Tables("quotationhdr")(0)("CustomerType")


            '    Dim dtCustmInfo = objCustm.GetCustomerInformation(dsMain.Tables("quotationhdr")(0)("CustomerType"), vSiteCode, clsAdmin.CLPProgram, dsMain.Tables("quotationhdr")(0)("CustomerNo"))
            '    CtrlCustDtls1.pDisplayDtls(dtCustmInfo)
            '    RefreshScanData(dsScan)
            '    CalculateSalesOrderSummary(dsScan)
            'End If
        End If
    End Sub

    Protected Sub grdScanItem_StartEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
        _iArticleQtyBeforeChange = grdScanItem.Rows(e.Row)("Quantity")
    End Sub

    Private Sub GetNewSalesOrderNumber()
        If OnlineConnect = True Then
            'Changed by Rohit to generate Document No. for proper sorting
            Dim objType = "FO_DOC"
            'Try
            '    CtrlSalesInfo1.CtrlTxtOrderNo.Text = GenDocNo("SO" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode, objType))
            'Catch ex As Exception
            '    CtrlSalesInfo1.CtrlTxtOrderNo.Text = "SO" & clsAdmin.TerminalID & objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode, objType)
            'End Try
            CtrlTxtOrderNo.Text = "SO" & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2) & "-" & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Length - 3) & "-" & objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode, objType)
            Try
                rbnLStatus.SmallImage = Spectrum.My.Resources.connected1.ToBitmap
                rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus2")
            Catch ex As Exception

            End Try

            'End Change by Rohit
        Else
            'Changed by Rohit to generate Document No. for proper sorting
            'Try
            '    CtrlSalesInfo1.CtrlTxtOrderNo.Text = GenDocNo("OSO" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode))
            'Catch ex As Exception
            '    CtrlSalesInfo1.CtrlTxtOrderNo.Text = "OSO" & clsAdmin.TerminalID & objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode)
            'End Try
            CtrlTxtOrderNo.Text = "OSO" & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2) & "-" & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Length - 3) & "-" & objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode)
            Try
                rbnLStatus.SmallImage = Spectrum.My.Resources.disconnected1.ToBitmap
                rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus1")
            Catch ex As Exception

            End Try
            'End Change by Rohit
        End If
    End Sub


    Private Sub cmdGenerateSTR_Click(sender As Object, e As EventArgs) Handles cmdGenerateSTR.Click
        Try
            If Not isValidData() Then
                Exit Sub
            End If
            dtStrResult = Nothing
            '----Check if pickup qty is there or not if there then if user put some pick up quantity then Generate  STR is not applicable &  show a message .
            If dsScan.Tables("ItemScanDetails").Rows.Count > 0 Then
                'Dim dr() = dsScan.Tables("ItemScanDetails").Select("PickUpQty>0")
                'If dr.Length > 0 Then
                '    MessageBox.Show("You cannot generate STR as few items in this sales order have been received by customer", getValueByKey("CLAE04"))
                'Else
                If MessageBox.Show("This sales order is not saved. Click OK to save sales order and generate the STR.", getValueByKey("CLAE04"), MessageBoxButtons.OKCancel) = Windows.Forms.DialogResult.OK Then
                    IsSTRGenerate = True
                    If Not IsBooking = True Then
                        BtnAcceptPayment_Click(Nothing, Nothing)
                    Else
                        BtnSaveSalesOrder_Click(sender, e, True)
                    End If
                    If IsSOSaved Then
                        IsSOSaved = False
                    End If
                    Dim strNo As String = ""
                    If dtStrResult IsNot Nothing Then
                        If dtStrResult.Tables(0).Rows.Count > 0 Then
                            Dim isLastNo As Integer = 1
                            For Each drs As DataRow In dtStrResult.Tables(0).Rows
                                If isLastNo = dtStrResult.Tables(0).Rows.Count Then
                                    If Not drs("closeddocument").ToString().Contains("OB") Then
                                        strNo = strNo & drs("closeddocument").ToString()
                                    End If
                                Else
                                    If Not drs("closeddocument").ToString().Contains("OB") Then
                                        strNo = strNo & drs("closeddocument").ToString() & ","
                                    End If
                                End If

                                isLastNo = isLastNo + 1
                            Next
                            ShowBigMessagewithOK("New STRs Generated : " & vbCrLf & vbCrLf & strNo, getValueByKey("CLAE04"))
                        End If

                    End If

                    IsSOSaved = False
                End If
            End If
            'End If
            IsSTRGenerate = False
        Catch ex As Exception
            LogException(ex)
            MsgBox(ex.Message)
            IsSTRGenerate = False
        End Try
    End Sub
    ''' <summary>
    '''  IF IsNewComboAdd true add new row IF IsNewComboAdd false then check is this item is already added in grid if No then Add new row ELSE then check is this items in combo if not Qty + else count no of comboes in grid if items rows are more than combo count then Qty++ else combo Count = 1  then Qty ++ else add New Row 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>   IF IsNewComboAdd true add new row IF IsNewComboAdd false then check is this item is already added in grid if No then Add new row ELSE then check is this items in combo if not Qty + else count no of comboes in grid if items rows are more than combo count then Qty++ else combo Count = 1  then Qty ++ else add New Row </remarks>
    Private Function fnAnalyzeItem(ByVal drItemsRow As DataRow) As DataRow()
        Try
            Dim drItemExists() As DataRow
            If IsNewComboAdd Then
                fnAnalyzeItem = dsScan.Tables("ItemScanDetails").Select("EAN='A00 df sfs000NOT'")
            Else
                If IsDBNull(drItemsRow.Item("BatchBarcode")) = False Then
                    '    drItemExists = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "' And BatchBarcode = '" & drItemsRow.Item("BatchBarcode") & "'")
                    drItemExists = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "' And BatchBarcode = '" & drItemsRow.Item("BatchBarcode") & "'and ArticleType='Single'")
                Else
                    'drItemExists = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "' And BatchBarcode IS NULL")
                    drItemExists = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "' And BatchBarcode IS NULL and ArticleType='Single'")
                End If
                If drItemExists.Count = 0 Then
                    fnAnalyzeItem = dsScan.Tables("ItemScanDetails").Select("EAN='A00 df sfs000NOT'")
                Else
                    Dim comboCount = DtSoBulkComboHdr.Select("PackagingBoxCode='" & drItemsRow.Item("ArticleCode") & "'").Count
                    Dim TotalItemExist = drItemExists.Count
                    If TotalItemExist > comboCount Then
                        Dim packazingBoxRowNos As String = String.Empty
                        '---- Get The EMPTY PACKAZING BOX row for Qty ++
                        For Each drCombo As DataRow In DtSoBulkComboHdr.Select("PackagingBoxCode='" & drItemsRow.Item("ArticleCode") & "'")
                            packazingBoxRowNos &= drCombo("ComboSrNo") & ","
                        Next
                        If Not packazingBoxRowNos = String.Empty Then
                            packazingBoxRowNos = packazingBoxRowNos.Substring(0, packazingBoxRowNos.Length - 1)
                        End If
                        If IsDBNull(drItemsRow.Item("BatchBarcode")) = False Then
                            fnAnalyzeItem = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "' and ArticleType='Single' And BatchBarcode = '" & drItemsRow.Item("BatchBarcode") & "'" & IIf(packazingBoxRowNos = String.Empty, "", "AND rowIndex Not In(" & packazingBoxRowNos & ")"))
                        Else
                            fnAnalyzeItem = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "' and ArticleType='Single' And BatchBarcode IS NULL " & IIf(packazingBoxRowNos = String.Empty, "", "AND rowIndex Not In(" & packazingBoxRowNos & ")"))
                        End If
                    ElseIf comboCount = 1 Then
                        '---- Get The row for Qty ++
                        If IsDBNull(drItemsRow.Item("BatchBarcode")) = False Then
                            fnAnalyzeItem = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "' and ArticleType='Single' And BatchBarcode = '" & drItemsRow.Item("BatchBarcode") & "'")
                        Else
                            fnAnalyzeItem = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "' and ArticleType='Single' And BatchBarcode IS NULL")
                        End If
                    Else
                        fnAnalyzeItem = dsScan.Tables("ItemScanDetails").Select("EAN='A00 df sfs000NOT'")
                    End If
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Private Sub EnableDiableTenderIcons()
        '--- Added by Mahesh for disable credit sale if tender not assign
        Dim DtTender As DataTable = GetTenderInfo(clsAdmin.SiteCode)
        '--- Credit sale 
        Dim dr() = DtTender.Select("TenderType='" & "Credit" & "'")
        If dr IsNot Nothing AndAlso dr.Count > 0 Then
            IsTenderCredit = True
        End If
        '----Cash
        Dim dt() = DtTender.Select("TenderType='" & "Cash" & "'")
        If Not (dt IsNot Nothing AndAlso dt.Count > 0) Then
            CtrlRbn1.DbtnPayCash.Enabled = False
        End If
        '----Cheque
        Dim dq() = DtTender.Select("TenderType='" & "Cheque" & "'")
        If Not (dq IsNot Nothing AndAlso dq.Count > 0) Then
            CtrlRbn1.DbtnpayCheque.Enabled = False
        End If
        '----CreditCard
        Dim dw() = DtTender.Select("TenderType='" & "CreditCard" & "'")
        If Not (dw IsNot Nothing AndAlso dw.Count > 0) Then
            CtrlRbn1.DbtnPayCard.Enabled = False
        End If
        '----NEFT   '' added by nikhil
        Dim dN() = DtTender.Select("TenderType='" & "NEFT" & "'")
        If dN IsNot Nothing AndAlso dN.Count > 0 Then
            CtrlRbn1.DbtnPayNEFT.Enabled = False

        End If
        '----RTGS
        Dim dRT() = DtTender.Select("TenderType='" & "RTGS" & "'")
        If dRT IsNot Nothing AndAlso dRT.Count > 0 Then
            CtrlRbn1.DbtnPayRTGS.Enabled = False
        End If
    End Sub

    Private Sub rbBtnRoundOff_Click(sender As Object, e As EventArgs) Handles rbBtnRoundOff.Click
        Try
            'If Not CtrlCashSummary1.lbltxt4 Mod 5 = 0 Then
            Dim p As Object = "ClearPromWithoutMessage"
            rbbtnClrAllPromo_Click(p, Nothing)
            'End If
            If CtrllblNetAmt.Text Mod 5 = 0 Then
                Exit Sub
            End If
            Dim FilterCondition As String = " isnull(Toplevel,'')='' "
            Dim totalGAmount As Double
            Dim percentage, totalDiscValue As Double
            totalDiscValue = CtrllblNetAmt.Text Mod 5
            Dim dtUserAuth As DataTable = _dsPackagingVar.Tables(0).Copy
            Dim dtCashMemoDtls As DataTable = _dsPackagingVar.Tables(0)

            'totalDiscValue = txtValue.Text.Trim
            'If (totalGAmount - totalDiscValue) < 0 Then
            '    ShowMessage(getValueByKey("UA008"), "UA008 - " & getValueByKey("CLAE04"))
            '    'ShowMessage("Discount Percent Greater than 100 is not Possible", "Information")
            '    Exit Sub
            'End If
            Dim ObjclsCommon As New clsCommon
            Dim offerno As String = ObjclsCommon.GetOffernoForRoundOff()
            totalGAmount = dtUserAuth.Compute("Sum(GROSSAMT)", FilterCondition)
            For Each dr As DataRow In dtUserAuth.Select(FilterCondition, "Ean", DataViewRowState.CurrentRows)
                percentage = (dr("GROSSAMT") / totalGAmount) * 100
                dr("LINEDISCOUNT") = (percentage * totalDiscValue) / 100
                dr("TOTALDISCPERCENTAGE") = (dr("LINEDISCOUNT") / dr("GrossAmt")) * 100
                dr("DISCOUNT") = dr("LINEDISCOUNT")
                dr("NETAMOUNT") = (dr("GROSSAMT") + IIf(dr("ExclTaxAmt") Is DBNull.Value, 0, dr("ExclTaxAmt"))) - dr("DISCOUNT")
                dr("AuthUserId") = clsAdmin.UserCode
                dr("AuthUserRemarks") = clsAdmin.UserCode
                dr("PROMOTIONID") = offerno
                dr("TOPLEVEL") = offerno
                dr("FirstLEVEL") = offerno

            Next


            Dim dvDtls = dtUserAuth.Select(String.Empty, String.Empty, DataViewRowState.ModifiedCurrent)
            If (dvDtls.Count > 0) Then
                For rowIndex = 0 To dtUserAuth.Rows.Count - 1
                    For colIndex = 0 To dtUserAuth.Columns.Count - 2
                        dtCashMemoDtls(rowIndex)(colIndex) = dtUserAuth(rowIndex)(colIndex)
                    Next
                Next
            End If

            For Each drUpdateScan As DataRow In _dsScan.Tables(0).Rows
                'drRem("GrossAmt") = 0
                'drRem("MinPayAmt") = 0
                Dim resultPackQuntity As DataRow() = _dsPackagingVar.Tables(0).Select("RowIndex='" + drUpdateScan("RowIndex").ToString() + "' and IsHeader=True ")
                If resultPackQuntity.Length > 0 Then
                    drUpdateScan("Discount") = resultPackQuntity(0)("Discount")
                    drUpdateScan("NetAmount") = resultPackQuntity(0)("NetAmount")
                    drUpdateScan("LineDiscount") = resultPackQuntity(0)("LineDiscount")
                    drUpdateScan("TotalDiscPercentage") = resultPackQuntity(0)("TotalDiscPercentage")
                    drUpdateScan("FirstLevel") = resultPackQuntity(0)("FirstLevel")
                    drUpdateScan("TopLevel") = resultPackQuntity(0)("TopLevel")
                    drUpdateScan("TopLevelDisc") = resultPackQuntity(0)("TopLevelDisc")
                End If



            Next
            _dsScan.AcceptChanges()
            For Each drDisc As DataRow In _dsScan.Tables(0).Rows

                If Not (IIf(IsDBNull(drDisc("TotalDiscPercentage")), 0, drDisc("TotalDiscPercentage")) = 0) Then

                    If (Not String.IsNullOrEmpty(drDisc("CLPDiscount").ToString())) Then
                        drDisc("Discount") = CDbl(drDisc("LineDiscount").ToString()) + CDbl(drDisc("CLPDiscount").ToString())
                    End If

                    drDisc("TotalDiscPercentage") = (IIf(drDisc("Discount") Is DBNull.Value, 0, drDisc("Discount")) * 100) / drDisc("GrossAmt")
                    drDisc("PromotionId") = IIf(drDisc("FirstLevel") = String.Empty, 0, drDisc("FirstLevel")) & "," & IIf(drDisc("TopLevel") = String.Empty, 0, drDisc("TopLevel"))

                    'If drDisc("PromotionId") = "0,0" Then
                    '    drDisc("LineDiscount") = (drDisc("GrossAmt") * drDisc("TotalDiscPercentage")) / 100
                    'End If
                    Dim totalamt As Decimal = 0
                    If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                        totalamt = drDisc("GrossAmt") - drDisc("Discount")
                        Dim objcom As New clsSaleOrderCommon
                        objcom.IsCSTApplicable = IsCSTApplicable
                        ' objcom.CreateDataSetForTaxCalculation(drDisc("ARTICLECODE"), totalamt, drDisc, dsMain, CtrlSalesInfo1.CtrlTxtOrderNo.Text, drDisc("EAN"), True)
                    End If

                    drDisc("NetAmount") = drDisc("GrossAmt") - drDisc("LineDiscount") + IIf(drDisc("ExclTaxAmt") Is DBNull.Value, 0, drDisc("ExclTaxAmt")) + IIf(drDisc("IncTaxAmt") Is DBNull.Value, 0, drDisc("IncTaxAmt"))
                    'drDisc("MinPayAmt") = Math.Round((drDisc("NetAmount") / drDisc("Quantity")) * drDisc("PickUpQty"), 3)

                    TotalSalesQty = drDisc("PickUpQty") + IIf(drDisc("DeliveredQty") IsNot DBNull.Value, drDisc("DeliveredQty"), 0)
                    NetArticleRate = drDisc("NetAmount") / drDisc("Quantity")
                    drDisc("MinPayAmt") = ((drDisc("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate)
                    drDisc("TotalPickUpAmt") = (drDisc("PickupQty") * NetArticleRate)

                End If

            Next
            For Each drDisc As DataRow In _dsPackagingVar.Tables(0).Rows

                If Not (IIf(IsDBNull(drDisc("TotalDiscPercentage")), 0, drDisc("TotalDiscPercentage")) = 0) Then

                    If (Not String.IsNullOrEmpty(drDisc("CLPDiscount").ToString())) Then
                        drDisc("Discount") = CDbl(drDisc("LineDiscount").ToString()) + CDbl(drDisc("CLPDiscount").ToString())
                    End If

                    drDisc("TotalDiscPercentage") = (IIf(drDisc("Discount") Is DBNull.Value, 0, drDisc("Discount")) * 100) / drDisc("GrossAmt")
                    drDisc("PromotionId") = IIf(drDisc("FirstLevel") = String.Empty, 0, drDisc("FirstLevel")) & "," & IIf(drDisc("TopLevel") = String.Empty, 0, drDisc("TopLevel"))

                    'If drDisc("PromotionId") = "0,0" Then
                    '    drDisc("LineDiscount") = (drDisc("GrossAmt") * drDisc("TotalDiscPercentage")) / 100
                    'End If
                    Dim totalamt As Decimal = 0
                    If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                        totalamt = drDisc("GrossAmt") - drDisc("Discount")
                        Dim objcom As New clsSaleOrderCommon
                        objcom.IsCSTApplicable = IsCSTApplicable
                        ' objcom.CreateDataSetForTaxCalculation(drDisc("ARTICLECODE"), totalamt, drDisc, dsMain, CtrlSalesInfo1.CtrlTxtOrderNo.Text, drDisc("EAN"), True)
                    End If

                    drDisc("NetAmount") = drDisc("GrossAmt") - drDisc("LineDiscount") + IIf(drDisc("ExclTaxAmt") Is DBNull.Value, 0, drDisc("ExclTaxAmt")) + IIf(drDisc("IncTaxAmt") Is DBNull.Value, 0, drDisc("IncTaxAmt"))
                    'drDisc("MinPayAmt") = Math.Round((drDisc("NetAmount") / drDisc("Quantity")) * drDisc("PickUpQty"), 3)

                    TotalSalesQty = drDisc("PickUpQty") + IIf(drDisc("DeliveredQty") IsNot DBNull.Value, drDisc("DeliveredQty"), 0)
                    NetArticleRate = drDisc("NetAmount") / drDisc("Quantity")
                    drDisc("MinPayAmt") = ((drDisc("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate)
                    drDisc("TotalPickUpAmt") = (drDisc("PickupQty") * NetArticleRate)

                End If

            Next

            IsRoundOfflabel = True
            IsRoundOffMsg = True
            IsApplyPromotion = True
            CalculateSalesOrderSummary(dsScan)
            RefreshScanData(dsScan)
            GridSetting()
            'CtrlSalesPersons.CtrlTxtBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Select()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Function AddButtonControlInGrid(ByVal rowIndex As Integer) As Boolean
        Try
            'controlList.RemoveRange(0, controlList.Count)
            'If controlList.Count > 0 Then
            '    'controlList.RemoveRange(0, controlList.Count - 1)
            '    controlList.Clear()
            'End If

            For Each drGridRow As C1.Win.C1FlexGrid.Row In grdScanItem.Rows

                If Not (drGridRow.Index = 0) Then
                    If grdScanItem.Rows(drGridRow.Index)("IsImageReq").ToString() = "1" Then


                        Dim getColumnType As String = String.Empty

                        'Create styles with data types, formats, etc
                        Dim cellStyle As C1.Win.C1FlexGrid.CellStyle

                        cellStyle = grdScanItem.Styles.Add("CellImageType")
                        cellStyle.DataType = Type.GetType("System.String")
                        cellStyle.TextAlign = TextAlignEnum.LeftCenter
                        cellStyle.WordWrap = True

                        'Assign styles to editable cells
                        Dim assignCellStyles As CellRange
                        grdScanItem.Rows(drGridRow.Index).HeightDisplay = 16

                        Dim ButtonX As Integer = grdScanItem.Cols("PLUS").WidthDisplay

                        'Create some new controls
                        'Dim btnBrowse As New CtrlBtn()
                        Dim btnBrowse As New Button
                        btnBrowse.Tag = grdScanItem.Rows(drGridRow.Index)("rowindex").ToString()
                        btnBrowse.MaximumSize = New System.Drawing.Size(18, 28)
                        btnBrowse.Anchor = AnchorStyles.None
                        Dim s As Size = New System.Drawing.Size(8, 7)
                        Dim img As Image
                        'btn.imageLayout = Nothing

                        img = Image.FromFile(Application.StartupPath & "\images\yes.png")

                        's.Height = btnBrowse.Height
                        's.Width = btnBrowse.Width







                        btnBrowse.Image = New Bitmap(img, s)

                        btnBrowse.FlatAppearance.BorderColor = Color.AliceBlue
                        btnBrowse.FlatAppearance.BorderSize = 2
                        'btnBrowse.SetRowIndex = rowIndex
                        'btnBrowse.Text = "Barcode"
                        'btnBrowse.Name = "btnPlus" + grdScanItem.Rows(drGridRow.Index)("EAN").ToString()
                        btnBrowse.Name = "btnPlus" + grdScanItem.Rows(drGridRow.Index)("PackagingIndex").ToString()
                        'btnBrowse.Dock = DockStyle.Fill
                        btnBrowse.ImageAlign = ContentAlignment.MiddleCenter
                        btnBrowse.Padding = New Padding(4)
                        btnBrowse.BackgroundImage = Nothing
                        btnBrowse.BackColor = Color.AliceBlue
                        'btnBrowse.
                        'Insert hosted control into grid
                        btnBrowse.TabStop = False
                        grdScanItem.Controls.Add(btnBrowse)

                        'host them in the C1FlexGrid
                        controlList.Add(New HostedControl(grdScanItem, btnBrowse, drGridRow.Index, grdScanItem.Cols("PLUS").Index, ButtonX, ButtonX))

                        'ImagePathRowIndex = rowIndex
                        assignCellStyles = grdScanItem.GetCellRange(drGridRow.Index, grdScanItem.Cols("PLUS").Index)
                        assignCellStyles.Style = grdScanItem.Styles("CellImageType")
                        AddHandler btnBrowse.Click, AddressOf BrowseImagePath
                        'Else
                        '    controlList.Remove(New HostedControl(grdScanItem, btnBrowse, drGridRow.Index, grdScanItem.Cols("PLUS").Index, ButtonX, ButtonX))
                    End If
                End If
            Next


            ' AddHandler btnBrowse.Click, AddressOf BrowseImagePath



        Catch ex As Exception
            LogException(ex)
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Public Function AddButtonControlInDeliveryGrid(ByVal rowIndex As Integer) As Boolean
        Try
            'controlList.RemoveRange(0, controlList.Count)
            'If controlList.Count > 0 Then
            '    'controlList.RemoveRange(0, controlList.Count - 1)
            '    controlList.Clear()
            'End If

            For Each drGridRow As C1.Win.C1FlexGrid.Row In dgDeliveryLocation.Rows

                If Not (drGridRow.Index = 0) Then

                    If dgDeliveryLocation.Rows(drGridRow.Index)("IsHeader").ToString() = "True" Then



                        Dim getColumnType As String = String.Empty

                        'Create styles with data types, formats, etc
                        Dim cellStyle As C1.Win.C1FlexGrid.CellStyle

                        cellStyle = dgDeliveryLocation.Styles.Add("CellImageType")
                        cellStyle.DataType = Type.GetType("System.String")
                        cellStyle.TextAlign = TextAlignEnum.LeftCenter
                        cellStyle.WordWrap = True

                        'Assign styles to editable cells
                        Dim assignCellStyles As CellRange
                        dgDeliveryLocation.Rows(drGridRow.Index).HeightDisplay = 16

                        Dim ButtonX As Integer = dgDeliveryLocation.Cols("PLUS").WidthDisplay

                        'Create some new controls
                        Dim btnBrowse As New Button
                        btnBrowse.Tag = dgDeliveryLocation.Rows(drGridRow.Index)("DeliveryIndex").ToString()
                       btnBrowse.MaximumSize = New System.Drawing.Size(18, 28)
                        btnBrowse.Anchor = AnchorStyles.None
                        Dim s As Size = New System.Drawing.Size(8, 7)
                        Dim img As Image
                        'btn.imageLayout = Nothing

                        img = Image.FromFile(Application.StartupPath & "\images\yes.png")






                        btnBrowse.Image = New Bitmap(img, s)

                        'btnBrowse.SetRowIndex = rowIndex
                        'btnBrowse.Text = "Barcode"
                        'btnBrowse.Name = "btnPlus" + grdScanItem.Rows(drGridRow.Index)("EAN").ToString()
                        btnBrowse.Name = "btnPlus" + dgDeliveryLocation.Rows(drGridRow.Index)("DeliveryIndex").ToString()
                        'Insert hosted control into grid
                        btnBrowse.ImageAlign = ContentAlignment.MiddleCenter
                        btnBrowse.Padding = New Padding(4)
                        btnBrowse.BackgroundImage = Nothing
                        btnBrowse.BackColor = Color.AliceBlue
                        'btnBrowse.
                        'Insert hosted control into grid
                        btnBrowse.TabStop = False
                        dgDeliveryLocation.Controls.Add(btnBrowse)

                        'host them in the C1FlexGrid
                        controlList.Add(New HostedControl(dgDeliveryLocation, btnBrowse, drGridRow.Index, dgDeliveryLocation.Cols("PLUS").Index, ButtonX, ButtonX))

                        'ImagePathRowIndex = rowIndex
                        assignCellStyles = grdScanItem.GetCellRange(drGridRow.Index, dgDeliveryLocation.Cols("PLUS").Index)
                        ' assignCellStyles.Style = dgDeliveryLocation.Styles("CellImageType")
                        AddHandler btnBrowse.Click, AddressOf BrowseImageDeliveryPath
                        'Else
                        '    controlList.Remove(New HostedControl(grdScanItem, btnBrowse, drGridRow.Index, grdScanItem.Cols("PLUS").Index, ButtonX, ButtonX))
                    End If
                End If
            Next


            ' AddHandler btnBrowse.Click, AddressOf BrowseImagePath



        Catch ex As Exception
            LogException(ex)
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Public Function AddSTRButtonControlInDeliveryGrid(ByVal rowIndex As Integer) As Boolean
        Try
            'controlList.RemoveRange(0, controlList.Count)
            'If controlList.Count > 0 Then
            '    'controlList.RemoveRange(0, controlList.Count - 1)
            '    controlList.Clear()
            'End If

            For Each drGridRow As C1.Win.C1FlexGrid.Row In dgDeliveryLocation.Rows

                If Not (drGridRow.Index = 0) Then

                    If dgDeliveryLocation.Rows(drGridRow.Index)("IsImageReq").ToString() = "True" Then



                        Dim getColumnType As String = String.Empty

                        'Create styles with data types, formats, etc
                        Dim cellStyle As C1.Win.C1FlexGrid.CellStyle

                        cellStyle = dgDeliveryLocation.Styles.Add("CellImageType")
                        cellStyle.DataType = Type.GetType("System.String")
                        cellStyle.TextAlign = TextAlignEnum.LeftCenter
                        cellStyle.WordWrap = True

                        'Assign styles to editable cells
                        Dim assignCellStyles As CellRange
                        dgDeliveryLocation.Rows(drGridRow.Index).HeightDisplay = 16

                        Dim ButtonX As Integer = dgDeliveryLocation.Cols("STR").WidthDisplay

                        'Create some new controls
                        Dim btnBrowse As New CtrlBtn()
                        btnBrowse.Tag = dgDeliveryLocation.Rows(drGridRow.Index)("DeliveryIndex").ToString()
                        btnBrowse.MaximumSize = New System.Drawing.Size(ButtonX, 25)

                        Dim s As Size
                        Dim img As Image
                        'btn.imageLayout = Nothing

                        ' img = Image.FromFile("D:\spectrumMain\Spectrum\Resources\yes.gif")

                        s.Height = btnBrowse.Height
                        s.Width = btnBrowse.Width
                        btnBrowse.TextAlign = ContentAlignment.MiddleCenter
                        btnBrowse.ImageAlign = ContentAlignment.MiddleCenter

                        ' btnBrowse.Image = New Bitmap(img, s)
                        'btnBrowse.ImageAlign = ContentAlignment.MiddleLeft
                        'btnBrowse.SetRowIndex = rowIndex
                        btnBrowse.Text = "Details"
                        'btnBrowse.Name = "btnPlus" + grdScanItem.Rows(drGridRow.Index)("EAN").ToString()
                        btnBrowse.Name = "btnSTR" + dgDeliveryLocation.Rows(drGridRow.Index)("DeliveryIndex").ToString()
                        'Insert hosted control into grid
                        btnBrowse.TabStop = False
                        dgDeliveryLocation.Controls.Add(btnBrowse)

                        'host them in the C1FlexGrid
                        controlList.Add(New HostedControl(dgDeliveryLocation, btnBrowse, drGridRow.Index, dgDeliveryLocation.Cols("STR").Index, ButtonX, ButtonX))

                        'ImagePathRowIndex = rowIndex
                        assignCellStyles = grdScanItem.GetCellRange(drGridRow.Index, dgDeliveryLocation.Cols("STR").Index)
                        'assignCellStyles.Style = dgDeliveryLocation.Styles("CellImageType")
                        AddHandler btnBrowse.Click, AddressOf BrowseSTRDeliveryPath
                        'Else
                        '    controlList.Remove(New HostedControl(grdScanItem, btnBrowse, drGridRow.Index, grdScanItem.Cols("PLUS").Index, ButtonX, ButtonX))
                        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                            btnBrowse.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                            btnBrowse.BackColor = Color.Transparent
                            btnBrowse.BackColor = Color.FromArgb(0, 107, 163)
                            btnBrowse.ForeColor = Color.FromArgb(255, 255, 255)
                            btnBrowse.Font = New Font("Neo Sans", 8, FontStyle.Bold)
                            btnBrowse.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
                            btnBrowse.FlatStyle = FlatStyle.Flat
                            btnBrowse.FlatAppearance.BorderSize = 0
                            btnBrowse.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
                        End If
                    End If
                End If
            Next


            ' AddHandler btnBrowse.Click, AddressOf BrowseImagePath



        Catch ex As Exception
            LogException(ex)
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Private Sub grdScanItem_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles grdScanItem.Paint
        For Each hosted As HostedControl In controlList
            'If hosted._row.Index > 0 Then
            hosted.UpdatePosition()
            'End If

        Next
    End Sub
    Private Sub dgDeliveryLocation_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles dgDeliveryLocation.Paint
        For Each hosted As HostedControl In controlList
            hosted.UpdatePosition()
        Next
    End Sub
    Private Sub BrowseImagePath(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim leadIndex As Double = 0
            drPackgVariation = _dsScan.Tables("ItemScanDetails").Select("rowindex='" & DirectCast(sender, Button).Tag & "'")(0)

            If drPackgVariation.Item("Articletype") = "Combo" Then
                If drPackgVariation.Item("PackagingMaterial") = "" Then
                    ShowMessage("Please select Packaging material for Combo article", " " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
            End If
            
            Dim foundRow As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("IsDocumentLevel = True")
            If foundRow.Count > 0 Then
                ResetDocTax(True, dtDocTaxes)
                dtDocTaxes.Clear()
            End If
            If IsApplyPromotion = True Then
                rbbtnClrAllPromo_Click(sender, e)
            End If
            Dim drPackgVariationCount As DataRow()
            drPackgVariationCount = _dsPackagingVar.Tables("PackagingMaterial").Select("rowindex='" & DirectCast(sender, Button).Tag & "'")
            If drPackgVariationCount.Length > 0 Then
                leadIndex = Convert.ToDouble(DirectCast(sender, Button).Tag & "." & drPackgVariationCount.Length)
            End If
            SetPackagingBaseVarionInSO(drPackgVariation, leadIndex, True)
            'If drPackgVariationCount.Length > 0 Then
            vPackagingIndex = vPackagingIndex + 1
            vDeliveryIndex = vDeliveryIndex + 1
            'End If
            BindSOItemGridData(_dsScan.Tables("ItemScanDetails"))
            CalculateSalesOrderSummary(_dsScan)
            AddButtonControlInGrid(1)
            If clsDefaultConfiguration.IsNewSalesOrder Then  '' added on 24/07/17
                BindTextValue()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub BrowseSTRDeliveryPath(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim isPartialStr As Boolean = False
            Dim STRArticles As String = ""
            Dim NotSTRArticles As String = ""
            If dsScan.Tables(0).Rows.Count > 0 Then

                Dim resulArticle As DataRow() = dsScan.Tables(0).Select("rowindex='" + DirectCast(sender, Button).Tag.ToString() + "'")
                If resulArticle.Length > 0 Then
                    If Not resulArticle(0)("IsCombo") Then
                        ' Dim resultoldFactorynodecode As DataRow() = dtFactoryTbl.Tables(1).Select("articlecode='" + drScan("articlecode").ToString() + "'")
                        Dim resultoldFactorynodecodecheck As DataRow() = dtFactoryTbl.Tables(0).Select("nodecode='" + resulArticle(0)("lastnodecode").ToString() + "'")
                        If resultoldFactorynodecodecheck.Length = 0 Then
                            ShowMessage("Factory is not mapped for the item, you cannot add STR details for this item", getValueByKey("CLAE04"))
                            Exit Sub
                        End If
                    Else
                        Dim resulComboArticle As DataRow() = DtSoBulkComboDtl.Select("Combosrno='" + DirectCast(sender, Button).Tag.ToString() + "'")
                        If resulComboArticle.Length > 0 Then


                            For i As Integer = 0 To resulComboArticle.Length - 1
                                Dim resultoldFactorynodecode As DataRow() = dtFactoryTbl.Tables(1).Select("articlecode='" + resulComboArticle(i)("articlecode").ToString() + "'")
                                If resultoldFactorynodecode.Length > 0 Then
                                    Dim resultnewFactorynodecodecheck As DataRow() = dtFactoryTbl.Tables(0).Select("nodecode='" + resultoldFactorynodecode(0)("lastnodecode").ToString() + "'")
                                    If resultnewFactorynodecodecheck.Length > 0 Then
                                        STRArticles &= resulComboArticle(i)("articlecode").ToString() & ","
                                        isPartialStr = True
                                    Else
                                        NotSTRArticles &= resulComboArticle(i)("articlecode").ToString() & ","
                                    End If
                                End If
                            Next
                            If Not isPartialStr Then
                                ShowMessage("Factory is not mapped for the item, you cannot add STR details for this item", getValueByKey("CLAE04"))
                                Exit Sub
                            Else
                                If STRArticles <> "" Then
                                    STRArticles = STRArticles.Substring(0, STRArticles.Length - 1)
                                End If
                                If NotSTRArticles <> "" Then
                                    NotSTRArticles = NotSTRArticles.Substring(0, NotSTRArticles.Length - 1)
                                End If
                                Dim MesStr As String
                                If NotSTRArticles <> "" Then
                                    MesStr = MesStr & "Factory is not mapped to Item " & NotSTRArticles & " ,you cannot add STR details for this items, " & vbCrLf
                                End If
                                If STRArticles <> "" Then
                                    MesStr = MesStr & " you can add STR details for Item " & STRArticles
                                End If
                                If NotSTRArticles <> "" Then
                                    ShowBigMessagewithOK(MesStr, getValueByKey("CLAE04"))
                                End If

                            End If
                        End If

                    End If

                End If

            End If
            Dim objStr As New frmPCAddSTRDetails
            objStr.DtFactoryRemarks = dtSTRFactoryRemark
            objStr.dsPackagingDeliverySTR = dsPackagingDelivery
            objStr.dtCombo = DtSoBulkComboDtl
            objStr.comboNonSTrArticles = NotSTRArticles
            objStr.filter = DirectCast(sender, Button).Tag
            objStr.SONumber = CtrlTxtOrderNo.Text
            objStr.VsoDate = vCurrentSODateTime
            objStr.StrIndex = StrIndex
            If DtSOStr IsNot Nothing Then
                objStr.dtSTR = dtSTR
                objStr.dtFinalSTR = DtSOStr
            End If
            If (objStr.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                StrIndex = objStr.StrIndex
                If (objStr.dtFinalSTR IsNot Nothing AndAlso objStr.dtFinalSTR.Rows.Count > 0) Then
                    DtSOStr = objStr.dtFinalSTR
                    dtSTR = objStr.dtSTR
                End If
            Else
                StrIndex = objStr.StrIndex
                dtSTR = DtSOStr.Copy()
            End If
            dtSTRFactoryRemark = objStr.DtFactoryRemarks
            objStr.Dispose()

           
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub BrowseImageDeliveryPath(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim leadDispIndex As String
            Dim leadIndex As Double = 0
            Dim rowindex As Integer
            drDeliveryVariation = _dsPackagingDelivery.Tables("PackagingDelivery").Select("deliveryindex='" & DirectCast(sender, Button).Tag & "'")(0)
            Dim drPackgVariationCount As DataRow()
            drPackgVariationCount = _dsPackagingDelivery.Tables("PackagingDelivery").Select("deliveryindex='" & DirectCast(sender, Button).Tag & "'")
            Dim dtAscii As New DataTable
            dtAscii = _dsPackagingDelivery.Tables("PackagingDelivery").Select("deliveryindex='" & DirectCast(sender, Button).Tag & "'").CopyToDataTable()
            If dtAscii.Rows.Count > 0 Then
                dtAscii = _dsPackagingDelivery.Tables("PackagingDelivery").Select("packagingindex='" & dtAscii.Rows(0)("packagingindex") & "'").CopyToDataTable()
            End If
            Dim i As Integer = 64 + dtAscii.Rows.Count 'Asc("A") ' Convert to ASCII integer.
            Dim x As String = Chr(i)

            'Dim result As DataRow() = dsPackagingDelivery.Tables(0).Select("RowIndex='" + dr("RowIndex").ToString() + "'  and PackagingIndex ='" + dr("PackagingIndex").ToString() + "'")


            If dsPackagingDelivery.Tables(0).Rows.Count > 0 Then
                If drPackgVariationCount.Count > 0 Then
                    Dim c As DataRow = _dsPackagingDelivery.Tables("PackagingDelivery").Select("deliveryindex='" & DirectCast(sender, Button).Tag & "'")(0)
                    rowindex = dsPackagingDelivery.Tables("PackagingDelivery").Rows.IndexOf(c) + 1
                End If
            End If
            If drPackgVariationCount.Length > 0 Then
                leadDispIndex = drPackgVariationCount(0)("SrNo") & "." & x
                leadIndex = drPackgVariationCount(0)("DispSrNo") 'Convert.ToDouble(DirectCast(sender, Button).Tag)
            End If
            SetDeliveryBaseVariationInSO(drDeliveryVariation, leadIndex, leadDispIndex, rowindex, True)
            'If drPackgVariationCount.Length > 0 Then
            vDeliveryIndex = vDeliveryIndex + 1
            'End If
            BindSODeliveryGridData(dsPackagingDelivery.Tables(0), True)
            AddButtonControlInDeliveryGrid(1)
            AddSTRButtonControlInDeliveryGrid(1)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub rdDelYes_CheckedChanged(sender As Object, e As EventArgs) Handles rdDelYes.CheckedChanged
        deliveryTimer.Start()
        CtrlLblDelivery.Visible = True
        CtrlLblDelivery.Text = "Delivery"
        If rdDelYes.Checked Then
            lblMultiPickupDel.Text = "Multi Delivery"
            DeliveryPickupChange()
            ClearDeliveryAddress()
        End If
        '-----Added BY Prasad for Loading Customer Address in grid
        'If Not _dtCustmInfo Is Nothing AndAlso _dtCustmInfo.Rows.Count > 0 AndAlso isCustSelected = True Then
        '    dtOrderAddresses = objComn.GetSOAddresses(_dtCustmInfo.Rows(0)("CUSTOMERNO"), _dtCustmInfo.Rows(0)("CLPPROGRAMID"), True)
        'End If
        'If Not dtOrderAddresses Is Nothing AndAlso dtOrderAddresses.Rows.Count > 0 Then
        '    Dim drTo() As DataRow = dtOrderAddresses.Select("AddressValue='To Be Decided'")
        '    If Not drTo.Length > 0 Then
        '        Dim NewRow As DataRow = dtOrderAddresses.NewRow()
        '        NewRow(0) = 0
        '        NewRow(1) = "To Be Decided"
        '        NewRow(2) = "Address"
        '        ' dtOrderAddresses.Rows.Add(NewRow)
        '        dtOrderAddresses.Rows.InsertAt(NewRow, 0)
        '    End If
        '    DeliveryGridSetting()
        'End If
    End Sub

    Private Sub rdDelNo_CheckedChanged(sender As Object, e As EventArgs) Handles rdDelNo.CheckedChanged
        deliveryTimer.Start()
        'CtrlLblDelivery.Visible = False
        CtrlLblDelivery.Text = "Store Pickup"
        If rdDelNo.Checked Then
            lblMultiPickupDel.Text = "Multi Pickup"
            DeliveryPickupChange()
            ClearDeliveryAddress()
        End If
        '-----Added BY Prasad for Loading Only Store name in grid
        'If Not _dtCustmInfo Is Nothing AndAlso _dtCustmInfo.Rows.Count > 0 Then
        '    dtOrderAddresses = objComn.GetSOAddresses(_dtCustmInfo.Rows(0)("CUSTOMERNO"), _dtCustmInfo.Rows(0)("CLPPROGRAMID"), False)
        'End If
        'If Not dtOrderAddresses Is Nothing AndAlso dtOrderAddresses.Rows.Count > 0 Then
        '    Dim drTo() As DataRow = dtOrderAddresses.Select("AddressValue='To Be Decided'")

        '    If Not drTo.Length > 0 Then
        '        Dim NewRow As DataRow = dtOrderAddresses.NewRow()
        '        NewRow(0) = 0
        '        NewRow(1) = "To Be Decided"
        '        NewRow(2) = "Address"
        '        ' dtOrderAddresses.Rows.Add(NewRow)
        '        dtOrderAddresses.Rows.InsertAt(NewRow, 0)
        '    End If
        '    DeliveryGridSetting()
        'End If
    End Sub
    Private Sub timer_Tick(ByVal sender As Object, e As EventArgs)
        CtrlLblDelivery.Visible = Not CtrlLblDelivery.Visible
    End Sub

    Private Sub frmPCSalesOrderCreation_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        TableLayoutPanel1.Width = Me.Width
    End Sub
    Dim _dtCustmInfo As DataTable
    Dim tooltip As New ToolTip
    Private Sub BtnSearchCust_Click(sender As Object, e As EventArgs) Handles BtnSearchCust.Click
        If clsDefaultConfiguration.IsManualCLPCustomerSearch = True Or clsDefaultConfiguration.IsManualCLPCustomerSearch Then
            If OnlineConnect = False Then
                ShowMessage(getValueByKey("CLCSTS01"), getValueByKey("CLAE04"))
                Exit Sub
            Else

                'Dim objCust As New frmNSearchCustomer
                'objCust.ShowSO = False
                'objCust.IsCustSearch = True
                'objCust.BtnSearchCustomer.Enabled = True
                'objCust.txtCustomerCode.ReadOnly = False
                'objCust.ShowDialog()
                Dim dtCust As New DataTable
                'If clsDefaultConfiguration.DetailedCustomerCreationformat = "0" Then
                '    Dim objCust As New frmNSearchCustomer
                '    objCust.ShowSO = False
                '    objCust.IsCustSearch = True
                '    objCust.BtnSearchCustomer.Enabled = True
                '    objCust.txtCustomerCode.ReadOnly = False
                '    objCust.ShowDialog()
                '    dtCust = objCust.dtCustmInfo()
                'Else
                '    Dim objCust As New frmNewCustomer
                '    objCust.IsCustSearch = True
                '    objCust.ShowDialog()
                '    dtCust = objCust.dtCustmInfo()
                'End If
                Dim objCust As New frmSearchCustomer
                Dim result As DialogResult = objCust.ShowDialog()
                dtCust = objCust.dtCustmInfo()
                If dtCust IsNot Nothing Then
                    _dtCustmInfo = dtCust
                    If _dtCustmInfo.Rows.Count > 0 Then

                        '------Added by Prasad if Delivery selected 'yes' load customer address else store names 
                        'If rdDelYes.Checked = True Then
                        '    dtOrderAddresses = objComn.GetSOAddresses(_dtCustmInfo.Rows(0)("CUSTOMERNO"), _dtCustmInfo.Rows(0)("CLPPROGRAMID"), True)
                        'Else
                        '    dtOrderAddresses = objComn.GetSOAddresses(_dtCustmInfo.Rows(0)("CUSTOMERNO"), _dtCustmInfo.Rows(0)("CLPPROGRAMID"), False)
                        'End If

                        '' Dim addressType() As DataRow = dtOrderAddresses.Select("AddressType='Address'")
                        '' If addressType.Length > 0 Then
                        'Dim drTo() As DataRow = dtOrderAddresses.Select("AddressValue='To Be Decided'")

                        'If Not drTo.Length > 0 Then
                        '    Dim NewRow As DataRow = dtOrderAddresses.NewRow()
                        '    NewRow(0) = 0
                        '    NewRow(1) = "To Be Decided"
                        '    NewRow(2) = "Address"
                        '    ' dtOrderAddresses.Rows.Add(NewRow)
                        '    dtOrderAddresses.Rows.InsertAt(NewRow, 0)
                        'End If
                        ''End If
                        'isCustSelected = True
                        'DeliveryGridSetting()
                        isCustSelected = True
                        If rdDelYes.Checked Then
                            rdDelYes_CheckedChanged(sender, e)
                        End If
                        DeliveryPickupChange()
                    End If
                   
                    Dim custName As String
                    CustomerNo = _dtCustmInfo.Rows(0)("CustomerNo")
                    If _dtCustmInfo.Rows(0)("FIRSTNAME") <> String.Empty Then
                        custName = _dtCustmInfo.Rows(0)("FIRSTNAME")
                    End If
                    If _dtCustmInfo.Rows(0)("SURNAME") <> String.Empty Then
                        custName &= " " & _dtCustmInfo.Rows(0)("SURNAME")
                    End If
                    If custName.Trim = String.Empty Then
                        CtrltxrCust.Text = "NA"
                    Else
                        CtrltxrCust.Text = custName
                        tooltip.SetToolTip(CtrltxrCust, CtrltxrCust.Text)
                    End If
                    If _dtCustmInfo.Rows(0)("CardType").ToString() <> String.Empty Then
                        vCardType = _dtCustmInfo.Rows(0)("CardType")
                        CLPCardType = _dtCustmInfo.Rows(0)("CardType")
                    End If
                    If _dtCustmInfo.Rows(0)("CompanyName") <> String.Empty Then
                        lblCompName.Text = _dtCustmInfo.Rows(0)("CompanyName")
                    Else
                        lblCompName.Text = "-"
                    End If
                    tooltip.SetToolTip(lblCompName, lblCompName.Text)
                    If _dtCustmInfo.Rows(0)("Department") <> String.Empty Then
                        CtrlLabel18.Text = _dtCustmInfo.Rows(0)("Department")
                    Else
                        CtrlLabel18.Text = "-"
                    End If
                    tooltip.SetToolTip(CtrlLabel18, CtrlLabel18.Text)

                    '    ----  vipin
                    If _dtCustmInfo.Rows(0)("CHSN") = "-" Then
                        _dtCustmInfo.Rows(0)("CHSN") = "Not Available"
                    End If
                    If _dtCustmInfo.Rows(0)("CHSN") <> String.Empty Then
                        CGSTIN.Font = New Font("Neo Sans", 9, FontStyle.Bold)
                        CGSTIN.Text = _dtCustmInfo.Rows(0)("CHSN")
                    Else
                        CGSTIN.Text = "-"
                    End If
                    tooltip.SetToolTip(CGSTIN, CGSTIN.Text)

                    'vCardType
                    'CLPCardType
                End If
            End If

        Else
            MsgBox(getValueByKey("CLCSTS03"), MsgBoxStyle.Critical, getValueByKey("CLAE04"))
        End If

    End Sub


    Private Sub tabSalesOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabSalesOrder.SelectedIndexChanged
        Try
            Dim selectedTab = tabSalesOrder.SelectedIndex
            If selectedTab <> 0 Then
                If Not isValidData(True) Then
                    tabSalesOrder.SelectedIndex = 0
                    Exit Sub
                End If
            End If



            If dsPackagingVar IsNot Nothing Then
                If dsPackagingVar.Tables(0).Rows.Count > 0 AndAlso selectedTab = 1 Then



                    If UpdateSODeliveryGridData() Then
                        BindSODeliveryGridData(dsPackagingDelivery.Tables(0), True)
                        AddButtonControlInDeliveryGrid(1)
                        AddSTRButtonControlInDeliveryGrid(1)
                        DeliveryGridSetting()
                    End If

                End If
                If selectedTab = 0 And IsUpdateSuccess = True And dsPackagingVar.Tables(0).Rows.Count > 0 Then

                    UpdateItemData()
                    RefreshScanData(dsScan)
                    CalculateSalesOrderSummary(_dsScan)
                    If DtSoBulkRemarks.Rows.Count > 0 Then
                        LoadRemarks(DtSoBulkRemarks, True)
                    End If
                End If
            End If
            BindTextValue() '##
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub UpdateItemData()
        Try

            For Each dr As DataRow In dsPackagingDelivery.Tables(0).Rows

                If dr("IsHeader") = "True" Then
                    Dim sumObject As Object
                    'sumObject = dsPackagingDelivery.Tables(0).Compute("Sum(PickUpQty)", " PackagingIndex='" + dr("PackagingIndex").ToString() + "'")
                    sumObject = dsPackagingDelivery.Tables(0).Compute("Sum(PickUpQty)", " RowIndex='" + dr("RowIndex").ToString() + "'")
                    Dim result As DataRow() = dsScan.Tables(0).Select("PackagingIndex='" + dr("PackagingIndex").ToString() + "'")
                    If result.Length > 0 Then 'And sumObject > 0
                        result(0)("PickUpQty") = sumObject
                        dsScan.AcceptChanges()
                    End If
                End If
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Function UpdateSODeliveryGridData() As Boolean
        Try
            Dim isFirstTimeIncrement As Boolean = True




            For Each dr As DataRow In dsPackagingVar.Tables(0).Rows


                Dim result As DataRow() = dsPackagingDelivery.Tables(0).Select("RowIndex='" + dr("RowIndex").ToString() + "'  and PackagingIndex ='" + dr("PackagingIndex").ToString() + "' and IsHeader=True")


                If result.Count = 0 Then
                    Dim rowDeliveryAddr = dsPackagingDelivery.Tables(0).NewRow()
                    rowDeliveryAddr("SrNo") = dr("SrNo")
                    rowDeliveryAddr("DispSrNo") = dr("SrNo")

                    rowDeliveryAddr("ArticleType") = dr("ArticleType")
                    'If dr("IsHeader").ToString() = "True" Then
                    '    rowDeliveryAddr("DISCRIPTION") = "Order " & dr("RowIndex").ToString() & ":" & dr("DISCRIPTION").ToString()
                    'Else
                    '    rowDeliveryAddr("DISCRIPTION") = dr("DISCRIPTION").ToString()
                    'End If
                    rowDeliveryAddr("DISCRIPTION") = dr("DISCRIPTION").ToString()
                    rowDeliveryAddr("Quantity") = dr("Quantity")
                    rowDeliveryAddr("UOM") = dr("UOM")
                    rowDeliveryAddr("EAN") = dr("EAN")
                    rowDeliveryAddr("FinYear") = dr("FinYear")
                    rowDeliveryAddr("ArticleType") = dr("ArticleType")
                    rowDeliveryAddr("ArticleCode") = dr("ArticleCode")
                    rowDeliveryAddr("SiteCode") = clsAdmin.SiteCode
                    rowDeliveryAddr("SaleOrderNumber") = dr("SaleOrderNumber")
                    If rbDPNo.Checked Then
                        rowDeliveryAddr("DeliveryDate") = ctrlDtDeliveryDate.Value
                        rowDeliveryAddr("DeliveryTime") = ctrlDtDeliveryDate.Value
                        If lblMultiPickupDel.Text = "Multi Pickup" Then
                            Dim drdefaddr() = dtTempOrderAddresses.Select("AddressKey='" & clsAdmin.SiteCode & "'")
                            If drdefaddr.Count > 0 Then
                                rowDeliveryAddr("DeliveryAddress") = drdefaddr(0)("AddressKey").ToString
                                rowDeliveryAddr("IsCustAddress") = "1"
                            End If
                        Else
                            Dim drdefaddr() = dtTempOrderAddresses.Select("DefaultAddress=1")
                            If drdefaddr.Count > 0 Then
                                rowDeliveryAddr("DeliveryAddress") = drdefaddr(0)("AddressKey").ToString
                                rowDeliveryAddr("IsCustAddress") = "2"
                            End If
                        End If
                    Else
                        rowDeliveryAddr("DeliveryAddress") = ""
                        rowDeliveryAddr("DeliveryDate") = DateTime.Now
                        rowDeliveryAddr("DeliveryTime") = DateTime.Now
                    End If
                    rowDeliveryAddr("PackagingMaterial") = dr("PackagingMaterial")
                    '--------
                    If clsDefaultConfiguration.PackageFiedlsAllowed Then
                        If dr("ArticleType") = "Single" Then
                            rowDeliveryAddr("PckgSize") = dr("PckgSize")
                        Else
                            Dim resultCombo As DataRow() = dtPackagingPrintBox.Select("KeyValue='" + dr("Discription") + "' and isPackagingMandatory=1")
                            If resultCombo.Length > 0 Then
                                rowDeliveryAddr("PckgSize") = "          -"
                            Else
                                rowDeliveryAddr("PckgSize") = dr("PckgSize")
                            End If
                        End If


                        If dr("UOM") Is DBNull.Value Then
                            rowDeliveryAddr("PckgQty") = "          -"
                        Else
                            If dr("UOM") = "KGS" Then
                                rowDeliveryAddr("PckgQty") = FormatNumber(dr("PckgQty"), 3) & " " & dr("PackagingType")
                            Else
                                rowDeliveryAddr("PckgQty") = "          -"
                            End If
                        End If


                        rowDeliveryAddr("PackagingType") = dr("PackagingType")
                    End If

                    rowDeliveryAddr("PickUpQty") = dr("PickUpQty")
                    rowDeliveryAddr("LastPickDate") = DateTime.Now
                    rowDeliveryAddr("PendingQty") = 0
                    rowDeliveryAddr("RowIndex") = dr("RowIndex")
                    rowDeliveryAddr("PackagingIndex") = dr("PackagingIndex")
                    rowDeliveryAddr("DeliveryIndex") = dr("DeliveryIndex")
                    rowDeliveryAddr("ISHeader") = "True"
                    rowDeliveryAddr("IsCombo") = dr("IsCombo")
                    rowDeliveryAddr("IsImageReq") = dr("IsHeader")
                    dsPackagingDelivery.Tables(0).Rows.Add(rowDeliveryAddr)
                    If isFirstTimeIncrement Then
                        'vDeliveryIndex = vDeliveryIndex + 1
                    End If
                    isFirstTimeIncrement = False
                Else
                    result(0)("PackagingMaterial") = dr("PackagingMaterial")
                    result(0)("Quantity") = dr("Quantity")
                    Dim baseQty As Decimal
                    baseQty = dr("Quantity")
                    If baseQty > 0 Then '- ((currentQty + sumObject) - result(0)("Quantity")) 
                        Dim resultSum As DataRow() = dsPackagingDelivery.Tables(0).Select("IsHeader = False and PackagingIndex='" + dr("PackagingIndex").ToString() + "'")
                        Dim sumObject As Object
                        sumObject = _dsPackagingDelivery.Tables(0).Compute("Sum(Quantity)", "IsHeader = False and PackagingIndex='" + dr("PackagingIndex").ToString() + "'")
                        If resultSum.Length > 0 Then
                            If baseQty - sumObject > 0 Then
                                result(0)("Quantity") = baseQty - sumObject
                            End If
                        End If
                        If clsDefaultConfiguration.PackageFiedlsAllowed Then
                            If result(0)("UOM").ToString() = "KGS" Then
                                result(0)("PckgSize") = dr("PckgSize")
                                If dr("PckgSize") = 0 Then
                                    result(0)("PckgQty") = 0 & " " & dr("PackagingType")
                                Else
                                    result(0)("PckgQty") = FormatNumber(((result(0)("Quantity") - result(0)("PickUpQty")) / dr("PckgSize")), 3) & " " & dr("PackagingType")
                                End If
                                '((dr("Quantity") - dr("PickUpQty")) / dr("PckgSize")) & " " & dr("PackagingType")
                                result(0)("PackagingType") = dr("PackagingType")
                                '----for variation in delivery
                                Dim resultdel As DataRow() = dsPackagingDelivery.Tables(0).Select("RowIndex='" + dr("RowIndex").ToString() + "'  and PackagingIndex ='" + dr("PackagingIndex").ToString() + "' and IsHeader=False")
                                If resultdel.Length > 0 Then
                                    For Each drr As DataRow In resultdel
                                        drr("PckgSize") = dr("PckgSize")

                                        If drr("PckgSize") = 0 Then
                                            drr("PckgQty") = 0 & " " & dr("PackagingType")
                                        Else
                                            drr("PckgQty") = FormatNumber(((drr("Quantity") - drr("PickUpQty")) / dr("PckgSize")), 3) & " " & dr("PackagingType")
                                        End If

                                        drr("PackagingType") = dr("PackagingType")
                                    Next
                                    '---------------------------------------
                                    'resultdel(0)("PckgSize") = dr("PckgSize")
                                    'If dr("PckgSize") = 0 Then
                                    '    resultdel(0)("PckgQty") = 0 & " " & dr("PackagingType")
                                    'Else
                                    '    resultdel(0)("PckgQty") = ((resultdel(0)("Quantity") - resultdel(0)("PickUpQty")) / dr("PckgSize")) & " " & dr("PackagingType")
                                    'End If

                                    'resultdel(0)("PackagingType") = dr("PackagingType")
                                End If
                            Else

                                ' result(0)("PckgSize") = dr("PckgSize")
                                If dr("ArticleType") = "Single" Then
                                    result(0)("PckgSize") = dr("PckgSize")
                                    Dim resultdel As DataRow() = dsPackagingDelivery.Tables(0).Select("RowIndex='" + dr("RowIndex").ToString() + "'  and PackagingIndex ='" + dr("PackagingIndex").ToString() + "' and IsHeader=False")
                                    If resultdel.Length > 0 Then
                                        For Each drr As DataRow In resultdel
                                            drr("PckgSize") = dr("PckgSize")
                                        Next
                                    End If
                                Else
                                    Dim resultCombo As DataRow() = dtPackagingPrintBox.Select("KeyValue='" + dr("Discription") + "' and isPackagingMandatory=1")
                                    If resultCombo.Length > 0 Then
                                        result(0)("PckgSize") = "          -"
                                    Else
                                        result(0)("PckgSize") = dr("PckgSize")
                                    End If
                                    '-----update delivery variation pckgqty 1.A 1.B
                                    Dim resultdel As DataRow() = dsPackagingDelivery.Tables(0).Select("RowIndex='" + dr("RowIndex").ToString() + "'  and PackagingIndex ='" + dr("PackagingIndex").ToString() + "' and IsHeader=False")
                                    If resultdel.Length > 0 Then
                                        For Each drr As DataRow In resultdel
                                            If resultCombo.Length > 0 Then
                                                drr("PckgSize") = "          -"
                                            Else
                                                drr("PckgSize") = dr("PckgSize")
                                            End If
                                        Next
                                    End If
                                End If

                            End If

                        End If

                    End If

                    'dvHeaderQty = New DataView(dsPackagingDelivery.Tables(0), "RowIndex='" + dr("RowIndex").ToString() + "'  and PackagingIndex ='" + dr("PackagingIndex").ToString() + "'", "", DataViewRowState.CurrentRows)


                    'If dvHeaderQty.Count > 0 Then
                    '    For Each drView1 As DataRowView In dvHeaderQty
                    '        '("IsHeader")
                    '    Next
                    'End If


                    'result(0)("Quantity") = dr("Quantity")
                    'result(0)("PackagingMaterial") = dr("PackagingMaterial")

                End If

            Next
            dsPackagingDelivery.AcceptChanges()

            IsUpdateSuccess = True
            Return True

        Catch ex As Exception
            LogException(ex)
        End Try


    End Function

    Private Function SendMail(ByVal Path As String, ByVal mailid As String)
        Dim IsMailSendSuccess As Boolean = False
        Dim NanoId As Long = (DateTime.Now - New DateTime(1970, 1, 1)).TotalMilliseconds
        Dim objclscomn As New clsCommon
        Try

            ' Dim Idlist As String = clsDefaultConfiguration.DayCloseEmailNotifiaction
            Using reader As New StreamReader(Path)
                Dim NetworkCred As New Net.NetworkCredential()
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

                Dim mm As New Net.Mail.MailMessage(NetworkCred.UserName, mailid) 'umed.barekar@criti.in
                ' Dim objclscomn As New clsCommon
                Dim sitename As String
                sitename = objclscomn.GetSiteName(clsAdmin.SiteCode)
                mm.Subject = " " & sitename & " Sales Order :" & CtrlTxtOrderNo.Text
                Dim msg As New StringBuilder
                Dim str As String
                str = "Dear " & CtrltxrCust.Text '& " - " & lblCompName.Text & ","
                msg.Append(str + vbCrLf + vbCrLf)
                Dim str1 As String
                str1 = "Greetings from  " & sitename & "!!"
                msg.Append(str1 + vbCrLf + vbCrLf)
                Dim str2 As String
                str2 = "We have successfully booked your sales order " & CtrlTxtOrderNo.Text & " of value Rs." & CtrllblNetAmt.Text.Trim()
                msg.Append(str2 + vbCrLf + vbCrLf)
                Dim str3 As String
                str3 = "Site: " & sitename & vbCrLf & "Booking Date: " & DateTime.Now.ToString("dd-MM-yyyy")
                msg.Append(str3 + vbCrLf + vbCrLf)
                If IsBooking Then
                    Dim str4 As String
                    str4 = "Please find the attached PDF copy for the same."
                    msg.Append(str4 + vbCrLf + vbCrLf + vbCrLf)
                End If

                Dim str5 As String
                '  str5 = "Regards," & vbCrLf & "Prashant Corner Team " & vbCrLf & "www.Prashantcorner.com"
                msg.Append(str5 + vbCrLf)




                mm.Body = msg.ToString()
                'mm.Attachments.Add(New Attachment(New MemoryStream(bytes), "D:\DayCloseReports\BankReport_Vertak Nagar_02-03-2015.pdf"))
                'mm.Attachments.Add(New Net.Mail.Attachment(reader.BaseStream, Path))

                '   If IsBooking Then 'vipin 22.02.2018 Send mail even on Create SO
                Dim attachment As New System.Net.Mail.Attachment(reader.BaseStream, Path)
                attachment.Name = "" & sitename & "_" & CtrlTxtOrderNo.Text & ".pdf"
                mm.Attachments.Add(attachment)
                '   End If

                mm.IsBodyHtml = False
                Dim smtp As New Net.Mail.SmtpClient()

                smtp.Host = host
                '  smtp.Host = "smtp.gmail.com"
                smtp.EnableSsl = True
                smtp.UseDefaultCredentials = True
                smtp.Credentials = NetworkCred
                smtp.Port = port
                ' smtp.Port = 587
                If clsDefaultConfiguration.EnableMailReSend Then
                    objclscomn.insertMailFailedDtl(clsAdmin.SiteCode, CtrlTxtOrderNo.Text, "SO", NanoId, Path, mailid, mm.Subject, IsMailSend:=False, UserId:=clsAdmin.UserCode, TerminalId:=clsAdmin.TerminalID, MailBody:=msg.ToString())
                End If
                smtp.Send(mm)
                IsMailSendSuccess = True
                If clsDefaultConfiguration.EnableMailReSend Then
                    objclscomn.UpdateMailFailedDtl(clsAdmin.SiteCode, CtrlTxtOrderNo.Text, "SO", NanoId, IsMailSendSuccess, "Succes", UserID:=clsAdmin.UserCode)
                End If
            End Using
        Catch ex As Exception
            If clsDefaultConfiguration.EnableMailReSend Then
                If IsMailSendSuccess = False Then
                    objclscomn.UpdateMailFailedDtl(clsAdmin.SiteCode, CtrlTxtOrderNo.Text, "SO", NanoId, IsMailSendSuccess, ex.ToString(), UserID:=clsAdmin.UserCode)
                End If
            End If
            LogException(ex)
        End Try
    End Function
   
    
   
    Private Sub lnkCustDetails_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCustDetails.LinkClicked
        If CtrltxrCust.Text = String.Empty Then
            ShowMessage(getValueByKey("SO048"), "SO048 - " & getValueByKey("CLAE04"))
            CtrltxrCust.Select()
            Exit Sub
        End If

        Dim objClpCustomer As New frmNewCustomer
        objClpCustomer.CustomerNo = _dtCustmInfo(0)("CustomerNo")
        objClpCustomer.ViewCust = True
        objClpCustomer.ShowDialog()
        objClpCustomer.Dispose()


    End Sub

    Private Sub rbbtnSOBooking_Click(sender As Object, e As EventArgs) Handles rbbtnSOBooking.Click
        Try
            Try
                If dsScan.Tables(0).Rows.Count > 0 Then
                    If MsgBox(getValueByKey("SO028"), MsgBoxStyle.YesNo, "SO028 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                        If Me.Name <> "frmPCSalesOrderCreation" Then
                            Dim frm As New frmPCSalesOrderCreation
                            frm.IsBooking = True
                            MDISpectrum.ShowChildForm(frm, True)
                        Else
                            ResetSalesOrder()
                            dtSTRFactoryRemark = objPCSO.GetFactoryRemarks(CtrlTxtOrderNo.Text)
                            BtnSONew_Click(sender, e)
                            IsBooking = True
                            Call SetScreenAsBooking()
                        End If
                    End If
                Else
                    If Me.Name <> "frmPCSalesOrderCreation" Then
                        Dim frm As New frmPCSalesOrderCreation
                        frm.IsBooking = True
                        MDISpectrum.ShowChildForm(frm, True)
                    Else
                        ResetSalesOrder()
                        dtSTRFactoryRemark = objPCSO.GetFactoryRemarks(CtrlTxtOrderNo.Text)
                        BtnSONew_Click(sender, e)
                        IsBooking = True
                        Call SetScreenAsBooking()
                    End If
                End If
            Catch ex As Exception
                LogException(ex)
            End Try
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub rbbtnSOBookingEdit_Click(sender As Object, e As EventArgs) Handles rbbtnSOBookingEdit.Click
        Try
            If dsScan.Tables(0).Rows.Count > 0 Then
                If MsgBox(getValueByKey("SO028"), MsgBoxStyle.YesNo, "SO028 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                    Dim frm As New frmPCNSalesOrderUpdate
                    frm.IsBookingEdit = True
                    MDISpectrum.ShowChildForm(frm, True)
                End If
            Else
                Dim frm As New frmPCNSalesOrderUpdate
                frm.IsBookingEdit = True
                MDISpectrum.ShowChildForm(frm, True)
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Function SMSTemplate() As String
        Try
            Dim str8 As String = ""
            Dim count As Integer = 0
            '  Dim Str9 As String = ""
            If Not dsMain Is Nothing Then
                If dsMain.Tables("SOPackagingBoxDeliveryDtl").Rows.Count > 0 Then
                    Dim dvDeliveryDate As New DataView(dsMain.Tables("SOPackagingBoxDeliveryDtl"))
                    dvDeliveryDate = dvDeliveryDate.ToTable(True, "DeliveryTime").DefaultView
                    dvDeliveryDate.Sort = "DeliveryTime ASC"
                    For Each dr As DataRowView In dvDeliveryDate

                        'Next
                        ' For Each dr As DataViewRowState In dvDeliveryDate
                        Dim Str9 = Convert.ToDateTime(dr("DeliveryTime")).ToString("dd-MMM-yyyy HH:mm")
                        If Str9 <> str8 Then
                            str8 = str8 + Str9 & "%0A"
                            count = count + 1
                        End If

                    Next
                End If

            End If


            Dim sitename As String
            Dim objclscomn As New clsCommon
            sitename = objclscomn.GetSiteName(clsAdmin.SiteCode)
            Dim msg As New StringBuilder
            Dim str As String

            str = "Dear " & CtrltxrCust.Text
            If lblCompName.Text <> "-" Then
                str = str + " - " & lblCompName.Text & "," & "%0A"
            Else
                str = str + ",%0A"
            End If
            msg.Append(str)
            Dim str1 As String
            str1 = "Greetings from Prashant Corner!!" & "%0A"
            msg.Append(str1)
            Dim str2 As String
            str2 = "We have successfully booked your sales order " & CtrlTxtOrderNo.Text & " of value Rs." & CtrllblNetAmt.Text.Trim() & "%0A"
            msg.Append(str2)
            Dim str3 As String
            str3 = "Site: " & sitename & "%0A"
            msg.Append(str3)

            Dim telphone = objclscomn.GetSiteTelphoneNo(clsAdmin.SiteCode)
            Dim str4 As String
            str4 = "Site telephone : " & telphone & "%0A"
            msg.Append(str4)

            Dim str5 As String
            str5 = "Booking Date : " & DateTime.Now.ToString("dd-MMM-yyyy HH:mm") & " " & "%0A" & "%0A"
            msg.Append(str5)

            If count = 1 Then
                str8 = "Delivery Date : " & "%0A" & str8 & "" & " %0A"
                msg.Append(str8)
            Else
                str8 = "Delivery Dates : " & "%0A" & str8 & "" & " %0A"
                msg.Append(str8)
            End If



            Dim str6 As String
            str6 = "Regards," & "%0A" & vbCrLf & "Prashant Corner Team " & "%0A" & vbCrLf & "www.Prashantcorner.com"
            msg.Append(str6)
            Return msg.ToString

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Function Themechange()
        rdDelYes.CheckAlign = ContentAlignment.MiddleRight
        rdDelYes.TextAlign = ContentAlignment.MiddleLeft
        rdDelNo.TextAlign = ContentAlignment.MiddleLeft
        rdDelNo.CheckAlign = ContentAlignment.MiddleCenter
        rbnTabSO.Text = rbnTabSO.Text.ToUpper
        rbnTabSO.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        CtrlRbn1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black
        CtrlSalesPersons.AlignChange = "Sales Order Old"
        '   CtrlCashSummary.AlignChangeForCashSummary = "Sales Order Old"
        rbbtnDefaultPromo.LargeImage = Global.Spectrum.My.Resources.Resources.defaultPromo_Normal
        rbbtnSelectPromo.LargeImage = Global.Spectrum.My.Resources.Resources.SelectPromo_Normal
        rbbtnClrAllPromo.LargeImage = Global.Spectrum.My.Resources.Resources.ClearPromo_Normal
        CtrlRbn1.DbtnPay.LargeImage = Global.Spectrum.My.Resources.Resources.payment_Normal
        CtrlRbn1.DbtnPayCash.LargeImage = Global.Spectrum.My.Resources.Resources.Cash_Normal
        CtrlRbn1.DbtnPayCard.LargeImage = Global.Spectrum.My.Resources.Resources.Card_Normal
        CtrlRbn1.DbtnpayCheque.LargeImage = Global.Spectrum.My.Resources.PayByCheque
        CtrlRbn1.DbtnPayNEFT.LargeImage = Global.Spectrum.My.Resources.Resources.RTGS_Blue  '' added by nikhil
        CtrlRbn1.DbtnPayRTGS.LargeImage = Global.Spectrum.My.Resources.Resources.RTGS_Blue  '' added by nikhil
        rbnCST.LargeImage = Global.Spectrum.My.Resources.ApplyTax
        rbBtnRoundOff.LargeImage = Global.Spectrum.My.Resources.RoundOff
        Me.rbgrpSO.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        ' Me.rbgrpSO.Image = Global.Spectrum.My.Resources.defaultPromo_Normal
        Me.rbgrpSO.ForeColorInner = Color.FromArgb(37, 37, 37)
        Me.rbnGrpCST.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        ' Me.rbnGrpCST.Image = Global.Spectrum.My.Resources.cancel_so
        Me.rbnGrpCST.ForeColorInner = Color.FromArgb(37, 37, 37)

        rbnCST.LargeImage = My.Resources.ApplyTax
        Me.rbnGrpAddCombo.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        'Me.rbnGrpAddCombo.Image = Global.Spectrum.My.Resources.defaultPromo_Normal
        rbBtnAddCombo.LargeImage = Global.Spectrum.My.Resources.AddCombonew
        Me.rbnGrpAddCombo.ForeColorInner = Color.FromArgb(37, 37, 37)
        Me.rbnGrpCMPromotion.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        Me.rbnGrpCMPromotion.Image = Global.Spectrum.My.Resources.defaultPromo_Normal
        Me.rbnGrpCMPromotion.ForeColorInner = Color.FromArgb(37, 37, 37)
        CtrlRbn1.DgrpPayments.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        Me.rbgrpSaveAndPrint.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        '  Me.rbnGrpPayments.Image = Global.Spectrum.My.Resources.payment_Normal
        Me.rbgrpSaveAndPrint.ForeColorInner = Color.FromArgb(37, 37, 37)
        Me.RibbonGroup2.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        Me.RibbonGroup2.ForeColorInner = Color.FromArgb(37, 37, 37)
        rbbtnSONew.LargeImage = Global.Spectrum.My.Resources.NewSO1

        Me.RibbonGroupNew.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        '  Me.RibbonGroupNew.Image = Global.Spectrum.My.Resources.payment_Normal
        Me.RibbonGroupNew.ForeColorInner = Color.FromArgb(37, 37, 37)
        RibbonGroupNew.Text = "Extra"
        rbbtnSOEdit.LargeImage = Global.Spectrum.My.Resources.EditSO1

        rbbtnSOCancel.LargeImage = Global.Spectrum.My.Resources.CancelSO1
        rbbtnSave.LargeImage = Global.Spectrum.My.Resources.SaveSO1
        rbbtnPrint.LargeImage = Global.Spectrum.My.Resources.PrintSO1
        rbbtnSOBooking.LargeImage = My.Resources.NewBooking
        rbbtnSOBookingEdit.LargeImage = My.Resources.EditBooking
        rbbtnDefaultPromo.Text = rbbtnDefaultPromo.Text.ToUpper
        rbbtnSelectPromo.Text = rbbtnSelectPromo.Text.ToUpper
        rbbtnClrAllPromo.Text = rbbtnClrAllPromo.Text.ToUpper
        CtrlRbn1.DbtnPay.Text = CtrlRbn1.DbtnPay.Text.ToUpper
        CtrlRbn1.DbtnPayCash.Text = CtrlRbn1.DbtnPayCash.Text.ToUpper
        CtrlRbn1.DbtnPayCard.Text = CtrlRbn1.DbtnPayCard.Text.ToUpper
        CtrlRbn1.DbtnpayCheque.Text = CtrlRbn1.DbtnpayCheque.Text.ToUpper
        rbnCST.Text = rbnCST.Text.ToUpper
        rbBtnRoundOff.Text = rbBtnRoundOff.Text.ToUpper


        Me.rbgrpSO.Text = Me.rbgrpSO.Text.ToUpper
        Me.rbnGrpCST.Text = Me.rbnGrpCST.Text.ToUpper
        rbBtnAddCombo.Text = rbBtnAddCombo.Text.ToUpper
        Me.rbnGrpAddCombo.Text = Me.rbnGrpAddCombo.Text.ToUpper
        Me.rbnGrpCMPromotion.Text = Me.rbnGrpCMPromotion.Text.ToUpper
        CtrlRbn1.DgrpPayments.Text = CtrlRbn1.DgrpPayments.Text.ToUpper
        Me.rbgrpSaveAndPrint.Text = Me.rbgrpSaveAndPrint.Text.ToUpper
        Me.RibbonGroup2.Text = Me.RibbonGroup2.Text.ToUpper
        rbbtnSONew.Text = rbbtnSONew.Text.ToUpper
        rbbtnSOEdit.Text = rbbtnSOEdit.Text.ToUpper
        rbbtnSOCancel.Text = rbbtnSOCancel.Text.ToUpper
        rbbtnSave.Text = rbbtnSave.Text.ToUpper
        rbbtnPrint.Text = rbbtnPrint.Text.ToUpper
        RibbonGroupNew.Text = RibbonGroupNew.Text.ToUpper
        rbbtnSOBooking.Text = "NEW BOOKING(CTRL+B)".ToUpper
        rbbtnSOBookingEdit.Text = "EDIT BOOKING(CTRL+G)".ToUpper
        cmdGenerateSTR.Text = cmdGenerateSTR.Text.Trim.ToUpper
        CtrlBtnSearchCLP.Text = CtrlBtnSearchCLP.Text.ToUpper()
        CtrlBtnStockCheck.Text = CtrlBtnStockCheck.Text.ToUpper()
        CtrlbtnSOOtherCharges.Text = CtrlbtnSOOtherCharges.Text.ToUpper
        rbgrpSO.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbnGrpCST.ForeColorInner = Color.FromArgb(54, 54, 54)
        RibbonGroup2.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbgrpSaveAndPrint.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbnGrpCMPromotion.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbnGrpAddCombo.ForeColorInner = Color.FromArgb(54, 54, 54)
        RibbonGroupNew.ForeColorInner = Color.FromArgb(54, 54, 54)


        RibbonGroupNew.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbgrpSO.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbnGrpCST.ForeColorOuter = Color.FromArgb(0, 107, 163)
        RibbonGroup2.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbgrpSaveAndPrint.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbnGrpCMPromotion.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbnGrpAddCombo.ForeColorOuter = Color.FromArgb(0, 107, 163)

        TabPageDeliveryLocation.TabBackColorSelected = Color.FromArgb(0, 81, 120)
        TabPageDeliveryLocation.TabForeColorSelected = Color.White
        TabPageAddItems.TabBackColorSelected = Color.FromArgb(0, 81, 120)
        TabPageAddItems.TabForeColorSelected = Color.White
        '  TabPageDeliveryLocation.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        ' TabPageAddItems.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        TabPageAddItems.Text = TabPageAddItems.Text.ToUpper
        TabPageDeliveryLocation.Text = TabPageDeliveryLocation.Text.ToUpper

        grdScanItem.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        grdScanItem.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212)
        grdScanItem.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        grdScanItem.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        grdScanItem.Rows.MinSize = 30
        grdScanItem.Styles.Normal.Font = New Font("Neo Sans", 8.5, FontStyle.Regular)
        grdScanItem.Styles.Fixed.Font = New Font("Neo Sans", 8.5, FontStyle.Bold)
        grdScanItem.Styles.Highlight.Font = New Font("Neo Sans", 8.5, FontStyle.Bold)
        grdScanItem.Styles.Highlight.Font = New Font("Neo Sans", 8.5, FontStyle.Bold)
        grdScanItem.Styles.Focus.Font = New Font("Neo Sans", 8.5, FontStyle.Bold)
        grdScanItem.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdScanItem.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        grdScanItem.Styles.Highlight.ForeColor = Color.Black
        grdScanItem.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 8.5, FontStyle.Bold)
        grdScanItem.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 8.5, FontStyle.Bold)
        grdScanItem.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdScanItem.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdScanItem.CellButtonImage = Global.Spectrum.My.Resources.Delete
        dgDeliveryLocation.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        dgDeliveryLocation.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212)
        dgDeliveryLocation.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgDeliveryLocation.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        dgDeliveryLocation.Rows.MinSize = 30
        dgDeliveryLocation.Styles.Normal.Font = New Font("Neo Sans", 8.5, FontStyle.Regular)
        dgDeliveryLocation.Styles.Fixed.Font = New Font("Neo Sans", 8.5, FontStyle.Bold)
        dgDeliveryLocation.Styles.Highlight.Font = New Font("Neo Sans", 8.5, FontStyle.Bold)
        dgDeliveryLocation.Styles.Highlight.Font = New Font("Neo Sans", 8.5, FontStyle.Bold)
        dgDeliveryLocation.Styles.Focus.Font = New Font("Neo Sans", 8.5, FontStyle.Bold)
        dgDeliveryLocation.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgDeliveryLocation.Styles.Highlight.ForeColor = Color.Black
        dgDeliveryLocation.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        dgDeliveryLocation.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 8.5, FontStyle.Bold)
        dgDeliveryLocation.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 8.5, FontStyle.Bold)
        dgDeliveryLocation.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgDeliveryLocation.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgDeliveryLocation.CellButtonImage = Global.Spectrum.My.Resources.Delete

        cmdGenerateSTR.LargeImage = My.Resources.GenerateSTRnew
        CtrlBtnSearchCLP.LargeImage = My.Resources.LoyaltyPoints

        CtrlBtnStockCheck.LargeImage = My.Resources.StockCheck

        CtrlbtnSOOtherCharges.LargeImage = My.Resources.AdditionalCost
        lblDelReq.BorderStyle = BorderStyle.None
        lblDelReq.BackColor = Color.FromArgb(212, 212, 212)
        lblSelCust.BorderStyle = BorderStyle.None
        lblSelCust.BackColor = Color.FromArgb(212, 212, 212)
        lblSelCust.BorderStyle = BorderStyle.None
        lblDispCompany.BackColor = Color.FromArgb(212, 212, 212)
        lblDispCompany.BorderStyle = BorderStyle.None
        lblSalesOrderNo.BorderStyle = BorderStyle.None
        lblSalesOrderNo.BackColor = Color.FromArgb(212, 212, 212)
        lblDispStrRaised.BackColor = Color.FromArgb(212, 212, 212)
        lblDispStrRaised.BorderStyle = BorderStyle.None
        lblRemarks.BackColor = Color.FromArgb(212, 212, 212)
        lblRemarks.BorderStyle = BorderStyle.None
        lblDispDepart.BackColor = Color.FromArgb(212, 212, 212)
        lblDispDepart.BorderStyle = BorderStyle.None
        lblOrderDate.BackColor = Color.FromArgb(212, 212, 212)
        lblOrderDate.BorderStyle = BorderStyle.None
        lblMultiPickupDel.BackColor = Color.FromArgb(212, 212, 212)
        lblMultiPickupDel.BorderStyle = BorderStyle.None
        rbDPNo.ForeColor = Color.White
        rbDPYes.ForeColor = Color.White
        lblDeliveryDate.BackColor = Color.FromArgb(212, 212, 212)
        lblDeliveryDate.BorderStyle = BorderStyle.None
        lblMultiPickupDel.TextAlign = ContentAlignment.MiddleLeft
        lblDeliveryDate.TextAlign = ContentAlignment.MiddleLeft
        ' lblMultiPickupDel.Margin = New Padding(0, 0, 0, 0)
        ' lblDeliveryDate.Margin = New Padding(0, 0, 0, 0)
        CtrlLabel15.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel15.Margin = New Padding(3, 0, 3, 1)
        CtrlLabel4.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel4.Margin = New Padding(3, 2, 3, 3)
        CtrlLabel6.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel6.Margin = New Padding(3, 0, 3, 2)
        ctlDispDisc.BackColor = Color.FromArgb(212, 212, 212)
        ctlDispDisc.Margin = New Padding(3, 3, 3, 1)
        CtrlLabel17.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel17.Margin = New Padding(3, 0, 3, 2)
        CtrlLabel3.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel3.Margin = New Padding(3, 0, 3, 2)
        CtrlLabel2.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel2.Margin = New Padding(3, 0, 3, 3)
        CtrlLabel1.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel1.Margin = New Padding(3, 0, 3, 2)
        CtrlLabel10.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel10.Margin = New Padding(3, 2, 3, 1)
        BtnSearchCust.Image = Nothing
        BtnSearchCust.BackgroundImage = My.Resources.SearchItems1
        BtnSearchCust.VisualStyle = C1.Win.C1Input.VisualStyle.System
        BtnSearchCust.BackgroundImageLayout = ImageLayout.Stretch
        BtnSearchCust.FlatAppearance.BorderSize = 0
        BtnSearchCust.FlatStyle = FlatStyle.Flat
        BtnSearchCust.Size = New Size(30, 25)
        CtrlLabel5.ForeColor = Color.White
        CtrlLabel7.ForeColor = Color.White
        rdDelYes.ForeColor = Color.White
        rdDelNo.ForeColor = Color.White
        lblStrRaised.ForeColor = Color.White

        CtrltxrCust.MaximumSize = New Size(0, 0)
        lblCompName.MaximumSize = New Size(0, 0)
        CtrlTxtOrderNo.MaximumSize = New Size(0, 0)
        remarkPanel.MaximumSize = New Size(0, 0)
        CtrlLabel18.MaximumSize = New Size(0, 0)
        CtrldtOrderDt.MaximumSize = New Size(0, 0)
        CtrllblGrossAmt.MaximumSize = New Size(0, 0)
        CtrlLabel7.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlLabel5.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        'Panel3.MaximumSize = New Size(0, 0)
        'Panel3.Size = New Size(137, 25)
        ctlDispDisc.MaximumSize = New Size(0, 0)
        CtrllblTaxAmt.MaximumSize = New Size(0, 0)
        CtrllblOtherCharges.MaximumSize = New Size(0, 0)
        CtrllblMinAdv.MaximumSize = New Size(0, 0)
        CtrllblNetAmt.MaximumSize = New Size(0, 0)
        CtrllblAmtPaid.MaximumSize = New Size(0, 0)
        CtrllblCreditSale.MaximumSize = New Size(0, 0)
        CtrllblBaltoPay.MaximumSize = New Size(0, 0)

        CtrllblDiscAmt.MaximumSize = New Size(0, 0)
        CtrllblDiscPerc.MaximumSize = New Size(0, 0)


        TableLayoutPanel2.Margin = New Padding(3, 0, 3, 0)
        TableLayoutPanel3.Margin = New Padding(3, 0, 3, 0)
        TableLayoutPanel4.Margin = New Padding(3, 0, 3, 0)
        TableLayoutPanel5.Margin = New Padding(3, 0, 3, 0)
        TableLayoutPanel6.Margin = New Padding(3, 0, 3, 0)
        TableLayoutPanel2.Size = New Size(151, 27)
        TableLayoutPanel3.Size = New Size(151, 27)
        TableLayoutPanel4.Size = New Size(151, 27)
        TableLayoutPanel5.Size = New Size(151, 27)
        TableLayoutPanel6.Size = New Size(151, 27)


        CtrltxrCust.Margin = New Padding(3, 3, 3, 1)
        lblCompName.Margin = New Padding(3, 3, 3, 1)
        CtrlTxtOrderNo.Margin = New Padding(3, 3, 3, 1)
        remarkPanel.Margin = New Padding(3, 3, 3, 1)
        CtrlLabel18.Margin = New Padding(3, 3, 3, 1)
        CtrldtOrderDt.Margin = New Padding(3, 3, 3, 1)
        CtrllblGrossAmt.Margin = New Padding(3, 3, 3, 1)
        'Panel3.Margin = New Padding(3, 3, 3, 0)
        ctlDispDisc.Margin = New Padding(3, 3, 3, 1)
        CtrllblTaxAmt.Margin = New Padding(3, 3, 3, 1)
        CtrllblOtherCharges.Margin = New Padding(3, 3, 3, 1)
        CtrllblMinAdv.Margin = New Padding(3, 3, 3, 1)
        CtrllblNetAmt.Margin = New Padding(3, 3, 3, 1)
        CtrllblAmtPaid.Margin = New Padding(3, 3, 3, 1)
        CtrllblCreditSale.Margin = New Padding(3, 3, 3, 1)
        CtrllblBaltoPay.Margin = New Padding(3, 3, 3, 1)
        CtrllblDiscAmt.Margin = New Padding(3, 3, 3, 0)
        CtrllblDiscPerc.Margin = New Padding(3, 3, 3, 0)
        CtrllblDiscAmt.MaximumSize = New Size(81, 27)
        CtrllblDiscPerc.MaximumSize = New Size(60, 27)
        CtrllblDiscAmt.BackColor = Color.FromArgb(255, 234, 242, 251)
        CtrllblDiscPerc.BackColor = Color.FromArgb(255, 234, 242, 251)

        CtrllblDiscAmt.Size = New Size(81, 27)
        CtrllblDiscPerc.Size = New Size(60, 27)
        ' CtrllblDiscAmt.BorderStyle = BorderStyle.None
        CtrllblDiscPerc.BorderStyle = BorderStyle.None

        lblDelReq.TextAlign = ContentAlignment.MiddleLeft
        lblSelCust.TextAlign = ContentAlignment.MiddleLeft
        lblDispCompany.TextAlign = ContentAlignment.MiddleLeft
        lblSalesOrderNo.TextAlign = ContentAlignment.MiddleLeft
        lblDispStrRaised.TextAlign = ContentAlignment.MiddleLeft
        lblRemarks.TextAlign = ContentAlignment.MiddleLeft
        lblDispDepart.TextAlign = ContentAlignment.MiddleLeft
        lblOrderDate.TextAlign = ContentAlignment.MiddleLeft

        CtrlLabel15.TextAlign = ContentAlignment.MiddleLeft
        CtrlLabel4.TextAlign = ContentAlignment.MiddleLeft
        CtrlLabel6.TextAlign = ContentAlignment.MiddleLeft
        ctlDispDisc.TextAlign = ContentAlignment.MiddleLeft
        CtrlLabel17.TextAlign = ContentAlignment.MiddleLeft
        CtrlLabel3.TextAlign = ContentAlignment.MiddleLeft
        CtrlLabel2.TextAlign = ContentAlignment.MiddleLeft
        CtrlLabel1.TextAlign = ContentAlignment.MiddleLeft
        CtrlLabel10.TextAlign = ContentAlignment.MiddleLeft
        CtrlLabel8.Visible = False
        CtrlLabel9.Visible = False
        CtrlLabel11.Visible = False
        CtrlLabel12.Visible = False

        Label1.BorderStyle = BorderStyle.None
        Label1.BackColor = Color.FromArgb(212, 212, 212)

        'Me.TableLayoutPanel1.ColumnStyles.Insert(0, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        'Me.TableLayoutPanel1.ColumnStyles.Insert(1, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.76471!))
        'Me.TableLayoutPanel1.ColumnStyles.Insert(2, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.536332!))
        'Me.TableLayoutPanel1.ColumnStyles.Insert(3, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.19398!))
        'Me.TableLayoutPanel1.ColumnStyles.Insert(4, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.6688963!))
        'Me.TableLayoutPanel1.ColumnStyles.Insert(5, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.09699!))
        'Me.TableLayoutPanel1.ColumnStyles.Insert(6, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.113712!))
        'Me.TableLayoutPanel1.ColumnStyles.Insert(7, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.04013!))
        'Me.TableLayoutPanel1.ColumnStyles.Insert(8, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.7525083!))
        'Me.TableLayoutPanel1.ColumnStyles.Insert(9, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.4319129!))
        'Me.TableLayoutPanel1.ColumnStyles.Insert(10, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.003345!))
        'Me.TableLayoutPanel1.ColumnStyles.Insert(11, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.1137123!))
        'Me.TableLayoutPanel1.ColumnStyles.Insert(12, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.4515!))
        'Me.TableLayoutPanel1.ColumnStyles.Insert(13, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.5433987!))
        'Me.TableLayoutPanel1.ColumnStyles.Insert(14, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.37124!))
        'Me.TableLayoutPanel1.ColumnStyles.Insert(15, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.62207!))
        'Me.TableLayoutPanel1.ColumnStyles.Insert(16, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.103679!))

        ' Me.c1SizerGrid.Controls.Remove(Me.CtrlSalesPersons)
        '  Me.CtrlSalesPersons.Size = New System.Drawing.Size(504, 22)
        '  Me.CtrlSalesPersons.Location = New System.Drawing.Point(504, 420)
        '  Me.Controls.Add(Me.CtrlSalesPersons)
        '  Me.Controls.SetChildIndex(Me.CtrlSalesPersons, 0)
        '  c1SizerGrid.Controls.Remove(grdScanItem)
        '  c1SizerGrid.Hide()
        ' Me.TabPageAddItems.Controls.Add(Me.grdScanItem)
        ' grdScanItem.Dock = DockStyle.Fill
        ' Me.grdScanItem.Size = New System.Drawing.Size(1354, 280)
        'Me.grdScanItem.Location = New System.Drawing.Point(4, 3)

    End Function
    Private Sub rbDPNo_CheckedChanged(sender As Object, e As EventArgs) Handles rbDPNo.CheckedChanged
        lblDeliveryDate.Visible = True
        'If Not _dtCustmInfo Is Nothing AndAlso _dtCustmInfo.Rows.Count > 0 Then
        '    dtOrderAddresses = objComn.GetSOAddresses(_dtCustmInfo.Rows(0)("CUSTOMERNO"), _dtCustmInfo.Rows(0)("CLPPROGRAMID"), False)
        'End If
        'If lblMultiPickupDel.Text = "Multi Pickup" Then
        '    If Not dtOrderAddresses Is Nothing AndAlso dtOrderAddresses.Rows.Count > 0 Then
        '        dtTempOrderAddresses = dtOrderAddresses.Select("AddressKey='" & clsAdmin.SiteCode & "'").CopyToDataTable
        '    End If
        'Else
        '    If Not _dtCustmInfo Is Nothing AndAlso _dtCustmInfo.Rows.Count > 0 AndAlso isCustSelected = True Then
        '        dtOrderAddresses = objComn.GetSOAddresses(_dtCustmInfo.Rows(0)("CUSTOMERNO"), _dtCustmInfo.Rows(0)("CLPPROGRAMID"), True)
        '    Else
        '        dtTempOrderAddresses.Clear()
        '        If dtTempOrderAddresses.Columns.Count = 0 Then
        '            Exit Sub
        '        End If
        '        'If Not dtTempOrderAddresses Is Nothing AndAlso dtTempOrderAddresses.Rows.Count > 0 Then
        '        '    Dim drTo() As DataRow = dtTempOrderAddresses.Select("AddressValue='To Be Decided'")

        '        '    If Not drTo.Length > 0 Then
        '        '        Dim NewRow As DataRow = dtTempOrderAddresses.NewRow()
        '        '        NewRow(0) = 0
        '        '        NewRow(1) = "To Be Decided"
        '        '        NewRow(2) = "Address"
        '        '        ' dtOrderAddresses.Rows.Add(NewRow)
        '        '        dtTempOrderAddresses.Rows.InsertAt(NewRow, 0)
        '        '    End If
        '        '    DeliveryGridSetting()
        '        'End If
        '    End If
        'End If



        'If Not dtTempOrderAddresses Is Nothing AndAlso dtTempOrderAddresses.Rows.Count > 0 Then
        '    Dim drTo() As DataRow = dtTempOrderAddresses.Select("AddressValue='To Be Decided'")

        '    If Not drTo.Length > 0 Then
        '        Dim NewRow As DataRow = dtTempOrderAddresses.NewRow()
        '        NewRow(0) = 0
        '        NewRow(1) = "To Be Decided"
        '        NewRow(2) = "Address"
        '        ' dtOrderAddresses.Rows.Add(NewRow)
        '        dtTempOrderAddresses.Rows.InsertAt(NewRow, 0)
        '    End If
        '    DeliveryGridSetting()
        DeliveryPickupChange()
        ClearDeliveryAddress()
        If Not dtTempOrderAddresses Is Nothing AndAlso dtTempOrderAddresses.Rows.Count > 0 Then
            ctrlDtDeliveryDate.Visible = True
            isEditLoad = True
            ctrlDtDeliveryDate.Value = vCurrentSODateTime
            Dim format As DateTime
            format.ToString("dd/MM/yyyy HH:mm:ss")
            'ctrlDtDeliveryDate.CustomFormat = "dd/MM/yyyy HH:mm:ss"
            ctrlDtDeliveryDate.Value = vCurrentSODateTime.ToString("dd/MM/yyyy HH:mm:ss")
            isEditLoad = False
            dgDeliveryLocation.Cols("DeliveryDate").AllowEditing = False
            dgDeliveryLocation.Cols("DeliveryTime").AllowEditing = False
        End If
        '  End If
    End Sub

    Private Sub rbDPYes_CheckedChanged(sender As Object, e As EventArgs) Handles rbDPYes.CheckedChanged
        lblDeliveryDate.Visible = False
        ctrlDtDeliveryDate.Visible = False
        'If Not _dtCustmInfo Is Nothing AndAlso _dtCustmInfo.Rows.Count > 0 Then
        '    dtOrderAddresses = objComn.GetSOAddresses(_dtCustmInfo.Rows(0)("CUSTOMERNO"), _dtCustmInfo.Rows(0)("CLPPROGRAMID"), False)
        'End If

        'If lblMultiPickupDel.Text = "Multi Pickup" Then
        '    If Not dtOrderAddresses Is Nothing AndAlso dtOrderAddresses.Rows.Count > 0 Then
        '        dtTempOrderAddresses = dtOrderAddresses.Copy
        '    End If
        'Else
        '    If Not dtOrderAddresses Is Nothing AndAlso dtOrderAddresses.Rows.Count > 0 Then

        '        dtTempOrderAddresses.Clear()
        '        If dtTempOrderAddresses.Columns.Count = 0 Then
        '            Exit Sub
        '        End If
        '        Dim drTo() As DataRow = dtTempOrderAddresses.Select("AddressValue='To Be Decided'")
        '        If Not drTo.Length > 0 Then
        '            Dim NewRow As DataRow = dtTempOrderAddresses.NewRow()
        '            NewRow(0) = 0
        '            NewRow(1) = "To Be Decided"
        '            NewRow(2) = "Address"
        '            ' dtOrderAddresses.Rows.Add(NewRow)
        '            dtTempOrderAddresses.Rows.InsertAt(NewRow, 0)
        '        End If
        '        DeliveryGridSetting()
        '    End If
        'End If
        'If Not dtTempOrderAddresses Is Nothing AndAlso dtTempOrderAddresses.Rows.Count > 0 Then
        '    Dim drTo() As DataRow = dtTempOrderAddresses.Select("AddressValue='To Be Decided'")

        '    If Not drTo.Length > 0 Then
        '        Dim NewRow As DataRow = dtTempOrderAddresses.NewRow()
        '        NewRow(0) = 0
        '        NewRow(1) = "To Be Decided"
        '        NewRow(2) = "Address"
        '        ' dtOrderAddresses.Rows.Add(NewRow)
        '        dtTempOrderAddresses.Rows.InsertAt(NewRow, 0)
        '    End If

        ' DeliveryGridSetting()
        DeliveryPickupChange()
        ClearDeliveryAddress()
        If Not dtTempOrderAddresses Is Nothing AndAlso dtTempOrderAddresses.Rows.Count > 0 Then
            dgDeliveryLocation.Cols("DeliveryDate").AllowEditing = True
            dgDeliveryLocation.Cols("DeliveryTime").AllowEditing = True
        End If

    End Sub

    Sub ClearDeliveryAddress()
        If Not dsPackagingDelivery.Tables.Count = 0 Then

            If rbDPNo.Checked Then
                If lblMultiPickupDel.Text = "Multi Pickup" Then
                    For Each drAdd As DataRow In dsPackagingDelivery.Tables(0).Rows
                        Dim drdefaddr() = dtTempOrderAddresses.Select("AddressKey='" & clsAdmin.SiteCode & "'")
                        If drdefaddr.Count > 0 Then
                            drAdd("DeliveryAddress") = drdefaddr(0)("AddressKey").ToString
                            drAdd("IsCustAddress") = "1"
                        End If
                    Next
                Else
                    For Each drAdd As DataRow In dsPackagingDelivery.Tables(0).Rows
                        Dim drdefaddr() = dtTempOrderAddresses.Select("DefaultAddress=1")
                        If drdefaddr.Count > 0 Then
                            drAdd("DeliveryAddress") = drdefaddr(0)("AddressKey").ToString
                            drAdd("IsCustAddress") = "2"
                        End If
                    Next
                End If
                
            Else
                For Each drAdd As DataRow In dsPackagingDelivery.Tables(0).Rows
                    drAdd("DeliveryAddress") = ""
                Next
            End If
            
            BindSODeliveryGridData(dsPackagingDelivery.Tables(0), True)
            AddButtonControlInDeliveryGrid(1)
            AddSTRButtonControlInDeliveryGrid(1)
        End If
    End Sub
    Sub DeliveryPickupChange()
        ' dtOrderAddresses = objComn.GetSOAddresses("", "", False)
        If lblMultiPickupDel.Text = "Multi Pickup" Then
            If rdDelNo.Checked Then
                dtOrderAddresses = objComn.GetSOAddresses("", "", False)
            End If
            If Not dtOrderAddresses Is Nothing AndAlso dtOrderAddresses.Rows.Count > 0 Then
                Dim dr() = dtOrderAddresses.Select("addresstype='Store'")
                If dr.Length > 0 Then
                    dtTempOrderAddresses = dtOrderAddresses.Select("addresstype='Store'").CopyToDataTable
                End If
                'If rbDPNo.Checked Then
                '    'Dim dr() = dtOrderAddresses.Select("AddressKey='" & clsAdmin.SiteCode & "'")
                '    'If dr.Length > 0 Then
                '    '    dtTempOrderAddresses = dtOrderAddresses.Select("AddressKey='" & clsAdmin.SiteCode & "'").CopyToDataTable
                '    'Else

                '    'End If
                'Else
                'Dim dr() = dtOrderAddresses.Select("addresstype='Store'")
                'If dr.Length > 0 Then
                '    dtTempOrderAddresses = dtOrderAddresses.Select("addresstype='Store'").CopyToDataTable
                'End If

                'End If
            End If

            'If Not dtOrderAddresses Is Nothing AndAlso dtOrderAddresses.Rows.Count > 0 Then
            '    dtTempOrderAddresses = dtOrderAddresses.Copy
            'End If
        Else
            If Not _dtCustmInfo Is Nothing AndAlso _dtCustmInfo.Rows.Count > 0 AndAlso isCustSelected = True Then
                dtOrderAddresses = objComn.GetSOAddresses(_dtCustmInfo.Rows(0)("CUSTOMERNO"), _dtCustmInfo.Rows(0)("CLPPROGRAMID"), True, True)
                If dtOrderAddresses.Rows.Count > 0 Then
                    dtTempOrderAddresses = dtOrderAddresses.Copy
                End If
                'If rbDPNo.Checked Then
                '    'Dim dr() = dtOrderAddresses.Select("AddressKey=1")
                '    'If dr.Length > 0 Then
                '    '    dtTempOrderAddresses = dtOrderAddresses.Select("AddressKey=1").CopyToDataTable
                '    'End If
                'Else
                '    'If dtOrderAddresses.Rows.Count > 0 Then
                '    '    dtTempOrderAddresses = dtOrderAddresses.Copy
                '    'End If

                'End If

            Else
                dtTempOrderAddresses.Clear()
                If dtTempOrderAddresses.Columns.Count = 0 Then
                    Exit Sub
                Else
                    Dim drTo() As DataRow = dtTempOrderAddresses.Select("AddressValue='To Be Decided'")
                    If Not drTo.Length > 0 Then
                        Dim NewRow As DataRow = dtTempOrderAddresses.NewRow()
                        NewRow(0) = 1001
                        NewRow(1) = "To Be Decided"
                        NewRow(2) = "Address"
                        ' dtOrderAddresses.Rows.Add(NewRow)
                        dtTempOrderAddresses.Rows.InsertAt(NewRow, 0)
                    End If
                    DeliveryGridSetting()
                End If
            End If
        End If
        If Not dtTempOrderAddresses Is Nothing AndAlso dtTempOrderAddresses.Rows.Count > 0 Then
            Dim drTo() As DataRow = dtTempOrderAddresses.Select("AddressValue='To Be Decided'")

            If Not drTo.Length > 0 Then
                Dim NewRow As DataRow = dtTempOrderAddresses.NewRow()
                NewRow(0) = 1001
                NewRow(1) = "To Be Decided"
                NewRow(2) = "Address"
                ' dtOrderAddresses.Rows.Add(NewRow)
                dtTempOrderAddresses.Rows.InsertAt(NewRow, 0)
            End If

            DeliveryGridSetting()
        End If

    End Sub
    Dim IsBackDelDate As Boolean
    Private Sub ctrlDtDeliveryDate_ValueChanged(sender As Object, e As EventArgs) Handles ctrlDtDeliveryDate.ValueChanged
        If isEditLoad = False Then
            If DateTime.Compare(Convert.ToDateTime(ctrlDtDeliveryDate.Value.Date), Convert.ToDateTime(DateTime.Now.Date)) < 0 Then
                ShowMessage("Delivery Date Can't be Backdated", "Information")
                ctrlDtDeliveryDate.Value = DateTime.Now
                IsBackDelDate = True
            ElseIf DateTime.Compare(Convert.ToDateTime(ctrlDtDeliveryDate.Value.Date), Convert.ToDateTime(DateTime.Now.Date)) = 0 Then
                If DateTime.Compare((Convert.ToDateTime(ctrlDtDeliveryDate.Value).ToShortTimeString), Convert.ToDateTime(DateTime.Now).ToShortTimeString) < 0 Then
                    ShowMessage("Delivery Date Can't be Backdated", "Information")
                    ctrlDtDeliveryDate.Value = DateTime.Now
                    IsBackDelDate = True
                End If
            End If
        End If
        If Not dsPackagingDelivery Is Nothing AndAlso dsPackagingDelivery.Tables.Count <> 0 Then
            If dsPackagingDelivery.Tables(0).Rows.Count > 0 Then
                If rbDPNo.Checked Then
                    For Each drDel As DataRow In dsPackagingDelivery.Tables(0).Rows
                        drDel("DeliveryDate") = ctrlDtDeliveryDate.Value
                        drDel("DeliveryTime") = ctrlDtDeliveryDate.Value
                    Next
                Else
                    For Each drDel As DataRow In dsPackagingDelivery.Tables(0).Rows
                        drDel("DeliveryDate") = DateTime.Now
                        drDel("DeliveryTime") = DateTime.Now
                    Next
                End If
                BindSODeliveryGridData(dsPackagingDelivery.Tables(0), True)
                AddButtonControlInDeliveryGrid(1)
                AddSTRButtonControlInDeliveryGrid(1)
            End If
        End If

    End Sub

End Class