Imports System.IO
Imports SpectrumBL
Imports System.Resources
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports C1.Win.C1FlexGrid
Imports System.Text
Imports SpectrumPrint

Public Class frmNSalesOrderUpdate
    Dim objCustm As New clsCLPCustomer()
    Dim clsVoucher As New SpectrumPrint.clsPrintVoucher
    Dim dtTaxCalc As DataTable
    Dim IsIGSTApplicableForOutsideCustomer As Boolean = False
    Dim dtSalesOrderTaxDetails As DataTable
    Protected controlList As New ArrayList
    Private ArticleScanWithBatchBarcode As Boolean
    Dim IsFormClosing As Boolean = False
    Dim IsStrGenerateApplicable As Boolean = False
    Dim IsSTRGenerate As Boolean = False
    Dim IsSOSaved As Boolean = False
    Dim IsNewComboAdd As Boolean = False

#Region "Declare Varables"
    Dim Batchbarcode As List(Of SpectrumCommon.BtachbarcodeInfo)
    Dim ReturnBatchbarcode As List(Of SpectrumCommon.BtachbarcodeInfo)
    Dim _iArticleQtyBeforeChange As Double = 0
    Dim CVoucherNo As String
    Dim CVVoucherDay As Int32 = clsAdmin.CreditValidDays
    Dim IssuingCV As Boolean = False
    Dim vSiteCode As String = clsAdmin.SiteCode
    Dim vfinancialYear As String = clsAdmin.Financialyear
    Dim vTerminalID As String = clsAdmin.TerminalID
    Dim vCurrencyDescription As String = clsAdmin.CurrencyDescription
    Dim vCurrencyCode As String = clsAdmin.CurrencyCode
    Dim vUserName As String = clsAdmin.UserName
    Dim vDateFormat As String = clsAdmin.SqlDBDateFormat
    Dim vCurrentDate As Date
    Dim consDeliveryDate As Date
    Dim DtDeletedData As DataTable
    Dim vPrinterSelection As Boolean = False
    Dim vPrintPaperType As String = String.Empty
    Dim vPrintLayoutselection As Boolean = False
    Dim vHeaderNote As Boolean = False
    Dim vFooterNote As Boolean = False
    Dim vResetTransNumbers As Boolean = False

    Private IsTenderCredit As Boolean = False
    Private IsTenderCash As Boolean = False
    Private IsTenderCheque As Boolean = False
    Private IsTenderCreditCard As Boolean = False

    Dim vIsPrintingTaxInfoAllowed As Boolean = False
    Dim vIsPrintPreviewAllowed As Boolean = False
    Dim vIsPromotionalMessageAllowed As Boolean = False
    Dim vIsPrintOfficialAddressAllowed As Boolean = False
    Dim vIsReturnWithoutOldSOAllowed As Boolean = False
    Dim vIsNegativeInventoryAllowed As Boolean = False
    Dim isLeaved As Boolean = False

    Dim tabSales, tabPayment, tabReturn As TabPage
    Dim isPromotionApplied As Boolean = False
    Dim objClpCustm As New clsCLPCustomer
    Dim objComn As New clsCommon
    Dim objCM As New clsCashMemo
    Dim objItemSch As New clsIteamSearch
    Dim objPaymentByCash As New frmNAcceptPaymentByCash("SO")
    Dim Obj As New frmNHowMuchtoPay
    Dim IsFinalReceipt As Boolean = False

    Dim dsScanReturn As New DataSet
    Dim dsScanTemp As New DataSet
    Dim dsScanProm As New DataSet
    Dim dsInvoice As New DataSet
    Dim dsMainCLP As New DataSet

    Dim dtOtherCharges As New DataTable
    Dim dtItemSch As New DataTable
    Dim dtPrintingDetails As New DataTable
    Dim dtCustmInfo As New DataTable

    Dim drAddItemExists() As DataRow
    Dim drItemSch As DataRow
    Dim drProm() As DataRow
    Dim drTax As DataRow
    Dim dvCurrentQty As DataView
    Dim drHomeAdds As DataRow
    Dim drDelvAdds As DataRow

    Dim vDocTypeCreation As String
    Dim vDocTypeReturn As String
    Dim vDocType As String = ""
    Dim vCardType As String = ""

    Dim vSOStatus As String = ""
    Dim vClpProgramId As String = clsAdmin.CLPProgram
    Dim CLPNo_ProgId_Point As String = ""

    Dim vSalesNo As String = ""
    Dim vSalesOrderCreationDate As DateTime = DateTime.Now
    Dim vSalesOrderExpectedDeliveryDate As DateTime = DateTime.Now
    Dim vCustomerNo As String
    Dim vArticleCode As String = ""
    Dim vCAddress As String = ""
    Dim vUOM As String = "EACH"
    Dim vEANList As String = ""
    Dim vSalesPerson As String = ""
    Dim vSalesInvcNo As String = ""
    Dim vFilterValue As String = "('Inserted','Updated')"

    Dim vAddressType As String
    Dim GridWidth As Integer = 0
    Dim GridHeight As Integer = 0
    Dim vRowIndex As Integer = 1
    Dim vAmendedNo As Integer = 0

    Dim vGrossAmount As Double = 0.0
    Dim vDiscAmount As Double = 0.0
    Dim vReceivedAmt As Double = 0.0
    Dim vCurrMinAdvanceAmt As Double = 0.0
    Dim vOtherChargesOld As Double = 0.0
    Dim vOtherChargesNew As Double = 0.0
    Dim vTotalOtherCharges As Double = 0.0
    Dim StockQty As Double = 0
    Dim TotalPoints As Double = 0.0

    Dim vAdvanceAmount As Double = 0.0
    Dim vBalanceAmount As Double = 0.0
    Dim TotalSalesQty As Double = 0.0
    Dim NetArticleRate As Double = 0.0

    Dim IsAllowedSalesReturn As Boolean = False
    Dim IsQuantityChange As Boolean = False
    Dim IsEditItem As Boolean = False
    Dim IsNewArticle As Boolean = False
    Dim IsMRPOpen As Boolean = False
    Dim IsNewRow As Boolean = False
    Dim IsApplyPromotion As Boolean = False
    Dim IsNextInvoiceNo As Boolean = False
    Dim TempOtherChargesTable As DataTable

    ''--- temporary variable 
    Dim lblOrderQty As New Label
    Dim lblPickupQty As New Label
    'Dim lblGrossAmt As New Label
    'Dim lblDiscAmt As New Label
    'Dim lblOtherCharges As New Label

    'Dim lblNetAmount As New Label
    Dim lblReceivedAmt As New Label
    Dim ItemScan As New Label
    Dim lbltotalitem As New Label
    Dim lbldeliveredqty As New Label
    Dim lblgrossamt1 As New Label
    'Dim lbladvancepaid As New Label
    Dim lblminadvancepaid As New Label
    'Dim lblbalanceamt As New Label

    Dim NCuurentQty As Double
    Dim SalesPersonName As String = ""

    Dim BtnSOSave As New Button
    Dim BtnSOPrint As New Button
    'Dim BtnSOApplyPromotion As New Button
    Dim BtnSOAcceptPayment As New Button
    Dim BtnSOOtherCharges As New Button

    Dim BtnSOReturn As New Button
    Dim BtnSOCancel As New Button
    Dim LbReturnReason As New Button
    Dim BtnSOStockCheck As New Button
    Dim BtnSOCalculater As New Button
    Dim BtnSearchItem As New Button

    '' ----- end of temporory variable
    Private DeliverySiteCode As String
    Private _dDueDate As Date
    Private _strRemarks As String
    Private boolIsReturn As Boolean = False
    Private IsCSTApplicable As Boolean = False

    Dim DtSoBulkComboHdr As New DataTable
    Dim DtSoBulkComboDtl As New DataTable


    Dim _dsMain As New DataSet
    Public Property dsMain() As DataSet
        Get
            Return _dsMain
        End Get
        Set(ByVal value As DataSet)
            _dsMain = value
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

    Dim _dvDisplayItem As New DataView
    Public Property dvDisplayItem() As DataView
        Get
            Return _dvDisplayItem
        End Get
        Set(ByVal value As DataView)
            _dvDisplayItem = value
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
    Private _SalesORderNumberFromPopup As String
    Public Property SalesORderNumberFromPopup() As String
        Get
            Return _SalesORderNumberFromPopup
        End Get
        Set(ByVal value As String)
            _SalesORderNumberFromPopup = value
        End Set
    End Property

    Private _EditCallFromPopup As Boolean
    Public Property EditCallFromPopup() As Boolean
        Get
            Return _EditCallFromPopup
        End Get
        Set(ByVal value As Boolean)
            _EditCallFromPopup = value
        End Set
    End Property

    Private _IsBookingEdit As Boolean
    Public Property IsBookingEdit() As Boolean
        Get
            Return _IsBookingEdit
        End Get
        Set(ByVal value As Boolean)
            _IsBookingEdit = value
        End Set
    End Property

#End Region
#Region "Load Sales Order Application"

    Private Sub frmNSalesOrderUpdate_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If grdSOItems.Rows.Count > 1 AndAlso Not IsFormClosing Then
            If MsgBox(getValueByKey("SO047"), MsgBoxStyle.YesNo, "SO047 - " & getValueByKey("CLAE04")) = MsgBoxResult.No Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmNSalesOrderUpdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F12 Then
                PriceChange()
            End If
        Catch ex As Exception

        End Try
    End Sub


    ''' <summary>
    ''' Get the Site default Settings And Set Default Config Object
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSalesOrderUpdation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            CtrlSalesPerson.AndroidSearchTextBox.Visible = False
            AddHandler CtrlSalesPerson.CtrlCmdSearch.Click, AddressOf BtnSearchItem_Click
            AddHandler CtrlSalesPerson.CtrlTxtBox.KeyDown, AddressOf txtSearchItem_Leave
            AddHandler CtrlBtnAddExtraCost.Click, AddressOf BtnAddOtherCharges_Click
            AddHandler cmdApplySelectedPromo.Click, AddressOf cmdDefaultPromo_Click
            AddHandler cmdSave.Click, AddressOf BtnSaveSalesOrder_Click
            AddHandler CtrlSalesInfo.CtrlBtn1.Click, AddressOf BtnSearchSalesOrder_Click
            AddHandler CmdSOClose.Click, AddressOf BtnSOCancel_Click
            AddHandler CmdSOClose.Click, AddressOf BtnSOCloseManualSO_Click
            AddHandler cmdPrint.Click, AddressOf BtnSOPrint_Click
            AddHandler CtrlBtnReturn.Click, AddressOf BtnSOReturn_Click
            AddHandler CtrlBtnStockCheck.Click, AddressOf BtnSOStockCheck_Click
            AddHandler CtrlRbn1.DbtnPay.Click, AddressOf BtnAcceptPayment_Click
            AddHandler CtrlRbn1.DbtnPayCard.Click, AddressOf BtnPayCard_Click
            AddHandler CtrlRbn1.DbtnPayCash.Click, AddressOf BtnPayCash_Click
            AddHandler CtrlRbn1.DbtnpayCheque.Click, AddressOf BtnPayCheque_Click
            lblBarcode.Text = getValueByKey("")
            AddHandler CtrlSalesInfo.CtrlTxtOrderNo.KeyDown, AddressOf FillSalesOrder
            'AddHandler CtrlSalesPerson.CtrlTxtBox.KeyDown, AddressOf txtSearchItem_KeyDown
            AddHandler CtrlRbn1.DbtnF2.Click, AddressOf ChangeQty
            AddHandler grdSOItems.StartEdit, AddressOf grdScanItem_StartEdit
            AddHandler rbnAddCombo.Click, AddressOf rbnAddCombo_Click
            AddHandler CtrlSalesInfo.CtrlDtExpDelDate.Leave, AddressOf dtpExpDeliveryDate_Leave
            AddHandler CtrlSalesInfo.CtrlDtExpDelDate.Calendar.DateValueSelected, AddressOf dtpExpDeliveryDate_Leave
            Dim objdefault As New clsDefaultConfiguration("SalesOrder")
            objdefault.GetDefaultSettings()
            dsMain = objSO.GetSOTableStruct(vSiteCode, 0)
            objSO.GetSODefaultConfig(vSiteCode)
            dtPrintingDetails = objSO.GetPrintingDetail
            _drSiteInfo = objComn.GetSiteInfo(vSiteCode).Rows(0)
            lblBarcode.Text = getValueByKey("frmnsalesordercreation.grdSOItems.ean")
            vDocTypeCreation = objSO.SOCreation
            vDocTypeReturn = objSO.SOReturn
            vDocType = vDocTypeCreation
            vIsPrintPreviewAllowed = clsDefaultConfiguration.SOPrintPreviewAllowed
            CtrReturnBarcode.Visible = clsDefaultConfiguration.IsBatchManagementReq
            lblBarcode.Visible = clsDefaultConfiguration.IsBatchManagementReq
            vCurrentDate = objComn.GetCurrentDate

            _dsScan = objSO.GetCollectionOfItems
            _dsScan.Clear()

            dsScanProm.Merge(dsScan)

            dsInvoice = objSO.SetInvoiceInSOCancel(vSiteCode, IIf(vSalesNo = String.Empty, 0, vSalesNo))

            RefreshLoadSOData()
            RefreshLoadInvcData()

            dsScanReturn.Merge(dsScan)

            grdSOItemRetuns.DataSource = dsScanReturn.Tables(0)
            grdSOItems.DataSource = dsScan.Tables(0)
            CtrlSalesPerson.CtrlSalesPersons.Enabled = False
            'tabSales = TabPageItemDetails
            'tabPayment = TabPageInvoiceDetails
            'tabReturn = TabPageItemDetailsReturn
            'TabSalesOrder.TabPages.Remove(tabReturn)
            TabSalesOrder.TabPages("TabPageItemDetailsReturn").TabVisible = False
            TabSalesOrder.SelectedTab = TabSalesOrder.TabPages("TabPageItemDetails")
            'TabSalesOrder.TabPages("TabPageItemDetails").Select()
            TabSalesOrder.pInit()
            GridItemSetting()
            GridInvoiceSetting()
            GridDeliverdSetting()
            PSetDefaultCurrencyOfCashMemoSummary(CtrlCashSummary1)
            '--added by rama on 4-aug-2009 for bug no 0000584
            If clsDefaultConfiguration.IsOtherChargesAllowed = False Then
                CtrlBtnAddExtraCost.Visible = False
            Else
                CtrlBtnAddExtraCost.Visible = True
            End If
            PSetDefaultCurrencyOfCashMemoSummary(CtrlCashSummary1)
            '--
            PrintSetProperty()
            CtrlBtnReturn.Enabled = False
            'code added by Mahesh for add bulk combo 
            Call objSO.GetSOBulkComboTablestructure(DtSoBulkComboHdr, DtSoBulkComboDtl)
            '--- Set tab sequence
            Call SetTabSequence()
            Call EnableDiableTenderIcons()
           
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

        SetCulture(Me, Me.Name, CtrlRbn1)
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
        End If
        'CtrlSalesInfo.CtrlTxtOrderNo.TabIndex = 0
        'CtrlSalesInfo.Focus()
        'CtrlSalesInfo.CtrlTxtOrderNo.TabIndex = 1
        'CtrlSalesInfo.CtrlTxtOrderNo.Focus()
        CtrlCashSummary1.CtrlLabel6.Location = New Point(5, 120)
        CtrlCashSummary1.CtrlLabelTxt6.Location = New Point(130, 128)
        CtrlCashSummary1.CtrlLabel7.Location = New Point(5, 145)
        CtrlCashSummary1.CtrlLabelTxt7.Location = New Point(130, 142)
        If EditCallFromPopup = True Then
            If SalesORderNumberFromPopup <> "" Then
                BtnSearchSalesOrder_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub SetTabSequence()
        Try
            '---- Set Tab Index START
            SetFormTabStop(Me, tabStopValue:=False)

            Dim ctrTablIndex As New Dictionary(Of Object, Int16)

            ctrTablIndex.Add(CtrlSalesInfo, 0)
            ctrTablIndex.Add(CtrlSalesInfo.CtrlTxtOrderNo, 0)
            ctrTablIndex.Add(CtrlSalesInfo.CtrlBtn1, 1)
            ctrTablIndex.Add(CtrlSalesInfo.CtrldtOrderDt, 2)
            ctrTablIndex.Add(CtrlSalesInfo.CtrlDtExpDelDate, 3)
            ctrTablIndex.Add(CtrlSalesInfo.CtrlTxtCustOrdRef, 4)
            ctrTablIndex.Add(CtrlSalesInfo.CtrlTxtRemarks, 5)
            ctrTablIndex.Add(CtrlSalesInfo.CtrlTxtInvoice, 6)

            'ctrTablIndex.Add(Me.TabSalesOrder, 1)
            ctrTablIndex.Add(Me.TabPageInvoiceDetails, 0)
            ctrTablIndex.Add(Me.TabPageItemDetails, 1)
            ctrTablIndex.Add(Me.TabPageItemDetailsReturn, 2)
            ctrTablIndex.Add(Me.C1Sizer3, 0)
            ctrTablIndex.Add(Me.CtrlSalesPerson, 0)
            ctrTablIndex.Add(Me.CtrlSalesPerson.CtrlSalesPersons, 0)
            ctrTablIndex.Add(Me.CtrlSalesPerson.CtrlTxtBox, 1)
            ctrTablIndex.Add(Me.CtrlSalesPerson.CtrlCmdSearch, 2)

            ctrTablIndex.Add(Me.grdSOItems, 1)

            ctrTablIndex.Add(Me.C1Sizer2, 2)
            ctrTablIndex.Add(Me.CtrlBtnStockCheck, 3)
            ctrTablIndex.Add(Me.CtrlBtnAddExtraCost, 4)
            ctrTablIndex.Add(Me.CtrlBtnReturn, 5)

            SetFormTabIndex(ctrTablIndex:=ctrTablIndex)
            Me.grdSOItems.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.None
            Me.TabSalesOrder.TabStop = False
            Me.C1Sizer3.TabStop = False
            C1Sizer2.TabStop = False
            Me.TabPageInvoiceDetails.TabStop = False
            Me.TabPageItemDetails.TabStop = False
            Me.TabPageItemDetailsReturn.TabStop = False

            '---- Set Tab Index END 
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Sub



    Private Sub frmNSalesOrderUpdate_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        CtrlSalesInfo.CtrlTxtOrderNo.TabIndex = 0
        CtrlSalesInfo.Focus()
        CtrlSalesInfo.CtrlTxtOrderNo.TabIndex = 1
        CtrlSalesInfo.CtrlTxtOrderNo.Focus()
    End Sub

    ''' <summary>
    ''' Resize DataGrid for Display Items Details
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TabPageItemDetails_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPageItemDetails.Resize
        GridWidth = 0
        GridHeight = 0

        grdSOItems.Width = TabSalesOrder.TabPages(0).Width - 3
        grdSOItems.Height = TabSalesOrder.TabPages(0).Height - 3
        GridWidth = (TabSalesOrder.TabPages(0).Width * 1) / 100
        GridHeight = (TabSalesOrder.TabPages(0).Height * 1) / 100

        grdSOItems.Cols(0).WidthDisplay = GridWidth * 2.27
        grdSOItems.Cols(1).WidthDisplay = GridWidth * 11.35
        grdSOItems.Cols(2).WidthDisplay = GridWidth * 17.65
        grdSOItems.Cols(3).WidthDisplay = GridWidth * 6.31
        grdSOItems.Cols(4).WidthDisplay = GridWidth * 8.83
        grdSOItems.Cols(5).WidthDisplay = GridWidth * 10.09
        grdSOItems.Cols(6).WidthDisplay = GridWidth * 7.57
        grdSOItems.Cols(7).WidthDisplay = GridWidth * 7.57
        grdSOItems.Cols(8).WidthDisplay = GridWidth * 8.83
        grdSOItems.Cols(9).WidthDisplay = GridWidth * 11.98

        grdSOItems.Refresh()
    End Sub

    ''' <summary>
    ''' Resize DataGrid for Display Items Details
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TabPageItemDetailsReturn_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPageItemDetailsReturn.Resize
        GridWidth = 0
        GridHeight = 0

        grdSOItemRetuns.Width = TabSalesOrder.TabPages(1).Width - 3
        grdSOItemRetuns.Height = TabSalesOrder.TabPages(1).Height - 3
        GridWidth = (TabSalesOrder.TabPages(1).Width * 1) / 100
        GridHeight = (TabSalesOrder.TabPages(1).Height * 1) / 100

        grdSOItemRetuns.Cols(0).WidthDisplay = GridWidth * 2.27
        grdSOItemRetuns.Cols(1).WidthDisplay = GridWidth * 11.35
        grdSOItemRetuns.Cols(2).WidthDisplay = GridWidth * 17.65
        grdSOItemRetuns.Cols(3).WidthDisplay = GridWidth * 6.31
        grdSOItemRetuns.Cols(4).WidthDisplay = GridWidth * 8.83
        grdSOItemRetuns.Cols(5).WidthDisplay = GridWidth * 10.09
        grdSOItemRetuns.Cols(6).WidthDisplay = GridWidth * 7.57
        grdSOItemRetuns.Cols(7).WidthDisplay = GridWidth * 7.57
        grdSOItemRetuns.Cols(8).WidthDisplay = GridWidth * 8.83
        grdSOItemRetuns.Cols(9).WidthDisplay = GridWidth * 11.98
        grdSOItemRetuns.Refresh()

    End Sub

    ''' <summary>
    ''' Resize DataGrid for Display Invoice Details
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TabPageInvoiceDetails_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPageInvoiceDetails.Resize
        GridWidth = 0
        GridHeight = 0

        grdSOInvoice.Width = TabSalesOrder.TabPages(2).Width - 3
        grdSOInvoice.Height = TabSalesOrder.TabPages(2).Height - 3
        GridWidth = (TabSalesOrder.TabPages(2).Width * 1) / 100
        GridHeight = (TabSalesOrder.TabPages(2).Height * 1) / 100

        grdSOInvoice.Cols(1).WidthDisplay = GridWidth * 9.47
        grdSOInvoice.Cols(2).WidthDisplay = GridWidth * 12.18
        grdSOInvoice.Cols(3).WidthDisplay = GridWidth * 14.88
        grdSOInvoice.Cols(4).WidthDisplay = GridWidth * 11.5
        grdSOInvoice.Cols(5).WidthDisplay = GridWidth * 12.86
        grdSOInvoice.Cols(6).WidthDisplay = GridWidth * 11.5
        grdSOInvoice.Cols(7).WidthDisplay = GridWidth * 14.75
        grdSOInvoice.Cols(8).WidthDisplay = GridWidth * 12.86
        grdSOInvoice.Refresh()
    End Sub

    Private Sub dtpExpDeliveryDate_Leave(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles CtrlSalesInfo.CtrlDtExpDelDate.Leave
        CtrlSalesInfo.CtrlDtExpDelDate.ValidateText()
        If isLeaved = True Then
            isLeaved = False
            Return
        End If
        If CtrlSalesInfo.CtrlDtExpDelDate.Value Is DBNull.Value Then
            ShowMessage(getValueByKey("SO032"), "SO032 - " & getValueByKey("CLAE04"))
            'ShowMessage("Delivery Date cannot be Blank.", "Delivery Date Information")
            CtrlSalesInfo.CtrlDtExpDelDate.Value = consDeliveryDate
        Else
            If CtrlSalesInfo.CtrlDtExpDelDate.Value < vCurrentDate Then
                ShowMessage(getValueByKey("SO010"), "SO010 - " & getValueByKey("CLAE04"))
                'ShowMessage("Delivery Date cannot be backdated.", "Delivery Date Information")
                CtrlSalesInfo.CtrlDtExpDelDate.Value = consDeliveryDate
            Else
                vSalesOrderExpectedDeliveryDate = CtrlSalesInfo.CtrlDtExpDelDate.Value
                For Each drGridRow As C1.Win.C1FlexGrid.Row In grdSOItems.Rows
                    If Not (drGridRow.Index = 0) Then
                        drGridRow.Item("ExpDelDate") = CtrlSalesInfo.CtrlDtExpDelDate.Value
                    End If
                Next
            End If
        End If

    End Sub

    ''' <summary>
    ''' Set Languadge in Sales Order Window
    ''' </summary>
    ''' <remarks></remarks>

    Public Sub New()
        If clsDefaultConfiguration.TillOperationRequired = True And clsDefaultConfiguration.TillOpenDone = False Then
            ShowMessage(getValueByKey("SO052"), "SO052 - " & getValueByKey("CLAE04"))
            Me.Dispose()
            Me.Close()
            Exit Sub
        End If
        ' This call is required by the Windows Form Designer.
        If CheckAuthorisation(clsAdmin.UserCode, "SOUpdation") = False Then

            ShowMessage(getValueByKey("SPCM001"), "SPCM001 - " & getValueByKey("CLAE04"))
            'ShowMessage("You have not Sufficent Rights", "Information")
            Me.Dispose()
            Me.Close()
            Exit Sub
        End If

        InitializeComponent()
        Me.ClientSize = New System.Drawing.Size(gmdiclientwidth, gmdiclientheight)
        CtrlRbn1.pInitRbn()
        If Batchbarcode Is Nothing Then
            Batchbarcode = New List(Of SpectrumCommon.BtachbarcodeInfo)
            ReturnBatchbarcode = New List(Of SpectrumCommon.BtachbarcodeInfo)
        End If

        CtrlRbn1.DbtnF12.LargeImage = Global.Spectrum.My.Resources.Resources.price_change
        CtrlRbn1.DbtnF2.LargeImage = Global.Spectrum.My.Resources.Resources.change_qty

    End Sub

#End Region

    Private _IsScanningWoBarcodeSelected As Boolean

#Region "Add Items in Sales Order"

    ''' <summary>
    ''' Get the Item Details 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnSearchItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles BtnSearchItem.Click
        Try
            If (String.IsNullOrEmpty(vSalesNo) Or CtrlSalesInfo.CtrlTxtOrderNo.Text.Trim <> vSalesNo) Then
                ShowMessage(getValueByKey("frmnquotationupdate.Warning"), getValueByKey("CLAE04"))
                Exit Sub
            End If
            Dim FetchData As New frmNItemSearch()
            FetchData.ShowDialog()

            Dim drSearch As DataRow = FetchData.ItemRow

            If Not (drSearch Is Nothing) Then
                If Not (vEANList.IndexOf(drSearch("EAN").ToString) = -1) Then
                    IsNewArticle = False
                Else
                    IsNewArticle = True
                End If
                IsEditItem = False
                Dim ean As String = String.Empty
                If clsDefaultConfiguration.IsBatchManagementReq Then
                    ean = SearchAvailableBarcodes(drSearch.Item("ArticleCode").ToString())
                    If String.IsNullOrEmpty(ean) Then
                        'Dim EventType As Int32
                        'ShowMessage(getValueByKey("frmnsalesorder.scaningreqmsg"), getValueByKey("CLAE04"), EventType, True, getValueByKey("mod009"))
                        'If EventType = 1 Then
                        _IsScanningWoBarcodeSelected = True
                        ean = drSearch("EAN").ToString()
                        'Else
                        '    Exit Sub
                        'End If
                    End If
                End If
                If String.IsNullOrEmpty(ean) Then
                    ean = drSearch("EAN").ToString()
                End If
                CtrlSalesPerson.CtrlTxtBox.Text = ean
                txtSearchItem_Leave(ean, New KeyEventArgs(Keys.Enter))
                _IsScanningWoBarcodeSelected = False
                drItemsRow = Nothing
                CtrlSalesPerson.CtrlTxtBox.Text = ""

                TabPageItemDetails_Resize(sender, New System.EventArgs)
                fnGridColAutoSize(grdSOItems)
            End If

        Catch ex As Exception
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

    ''' <summary>
    ''' Get the Item Details 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtSearchItem_Leave(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) 'Handles CtrlSalesPerson.CtrlTxtBox.Leave
        Try
            If e.KeyCode = Keys.Enter Then
                If (vSalesNo = "" Or vSalesNo = String.Empty) Then
                    ShowMessage(getValueByKey("frmnquotationupdate.Warning"), getValueByKey("CLAE04"))
                    Exit Sub
                End If

                If CtrlSalesPerson.CtrlTxtBox.Text.Length >= 1 Then
                    If clsDefaultConfiguration.IsBatchManagementReq Then
                        Dim articleCode As String = objItemSch.GetArticleCodeFromBarcode(clsAdmin.SiteCode, CtrlSalesPerson.CtrlTxtBox.Text.Trim)
                        If String.IsNullOrEmpty(articleCode) Then
                            articleCode = objItemSch.GetArticleCodeFromEAN(CtrlSalesPerson.CtrlTxtBox.Text.Trim)
                            If String.IsNullOrEmpty(articleCode) Then
                                articleCode = CtrlSalesPerson.CtrlTxtBox.Text.Trim
                            End If
                            Dim barCode As String
                            If _IsScanningWoBarcodeSelected = False Then
                                barCode = SearchAvailableBarcodes(articleCode)
                            End If

                            ArticleScanWithBatchBarcode = False

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
                                If Batchbarcode IsNot Nothing AndAlso Not String.IsNullOrEmpty(barCode) Then
                                    If Batchbarcode.Any(Function(w) w.BatchBarcode = barCode) Then
                                        Batchbarcode.Find(Function(w) w.BatchBarcode = barCode).Qty = Batchbarcode.Find(Function(w) w.BatchBarcode = barCode).Qty + 1
                                    Else
                                        Dim dvEan As New DataView(dtItemSch, "Ean='" & CtrlSalesPerson.CtrlTxtBox.Text.Trim & "'", "", DataViewRowState.CurrentRows)
                                        If dvEan.Count > 0 Then
                                            Batchbarcode.Add(New SpectrumCommon.BtachbarcodeInfo() With {.ArticleCode = dvEan(0)("ArticleCode"), .BatchBarcode = barCode, .EAN = dvEan(0)("EAN"), .LineNO = vRowIndex, .Qty = 1, .Status = True, .ArticleName = dvEan(0)("DISCRIPTION")})
                                        Else
                                            Batchbarcode.Add(New SpectrumCommon.BtachbarcodeInfo() With {.ArticleCode = dtItemSch(0)("ArticleCode"), .BatchBarcode = barCode, .EAN = dtItemSch(0)("EAN"), .LineNO = vRowIndex, .Qty = 1, .Status = True, .ArticleName = dtItemSch(0)("DISCRIPTION")})
                                        End If
                                    End If
                                    For Each item In dtItemSch.Rows
                                        item("BatchBarcode") = DBNull.Value
                                    Next
                                    ArticleScanWithBatchBarcode = True
                                End If
                            End If
                        Else
                            dtItemSch = objItemSch.GetEANData(clsAdmin.SiteCode, articleCode, clsAdmin.LangCode, "", dtItemScanData, False, True, CtrlSalesPerson.CtrlTxtBox.Text.Trim)

                            If Batchbarcode IsNot Nothing AndAlso Not String.IsNullOrEmpty(CtrlSalesPerson.CtrlTxtBox.Text.Trim) Then
                                If Batchbarcode.Any(Function(w) w.BatchBarcode = CtrlSalesPerson.CtrlTxtBox.Text.Trim) Then
                                    Batchbarcode.Find(Function(w) w.BatchBarcode = CtrlSalesPerson.CtrlTxtBox.Text.Trim).Qty = Batchbarcode.Find(Function(w) w.BatchBarcode = CtrlSalesPerson.CtrlTxtBox.Text.Trim).Qty + 1
                                Else
                                    Batchbarcode.Add(New SpectrumCommon.BtachbarcodeInfo() With {.ArticleCode = dtItemSch(0)("ArticleCode"), .BatchBarcode = CtrlSalesPerson.CtrlTxtBox.Text.Trim, .EAN = dtItemSch(0)("EAN"), .LineNO = vRowIndex, .Qty = 1, .Status = True, .ArticleName = dtItemSch(0)("DISCRIPTION")})
                                End If
                                For Each item In dtItemSch.Rows
                                    item("BatchBarcode") = DBNull.Value
                                Next

                                ArticleScanWithBatchBarcode = True
                            End If
                        End If
                    Else
                        dtItemSch = objItemSch.GetEANData(clsAdmin.SiteCode, CtrlSalesPerson.CtrlTxtBox.Text.Trim, clsAdmin.LangCode, "", dtItemScanData)
                        For Each item In dtItemSch.Rows
                            item("BatchBarcode") = DBNull.Value
                        Next

                        ArticleScanWithBatchBarcode = False
                    End If
                    If dtItemSch Is Nothing Or dtItemSch.Rows.Count < 1 Then
                        ShowMessage(getValueByKey("SO055"), "SO055 - " & getValueByKey("CLAE04"))
                        CtrlSalesPerson.CtrlTxtBox.Text = ""
                        Exit Sub
                    End If
                    'Changed by rama on sep 16 sep 2009 for bug no 1107
                    If dtItemSch.Rows.Count > 1 Then
                        Dim dvEan As New DataView(dtItemSch, "Ean='" & CtrlSalesPerson.CtrlTxtBox.Text & "'", "", DataViewRowState.CurrentRows)
                        If dvEan.Count > 0 Then
                            dvEan.RowFilter = "EAN<>'" & CtrlSalesPerson.CtrlTxtBox.Text & "'"
                            If dvEan.Count > 0 Then
                                dvEan.AllowDelete = True
                                For Each dr As DataRowView In dvEan
                                    dr.Delete()
                                Next
                                dtItemSch.AcceptChanges()
                            End If
                        Else
                            Dim dv As New DataView(dtItemSch, "DefaultEAN <> 1", "", DataViewRowState.CurrentRows)
                            'Dim dv As New DataView(dtItemSch, "EanType<>'" & EanType & "'", "", DataViewRowState.CurrentRows)
                            If dv.Count > 0 Then
                                dv.AllowDelete = True
                                For Each dr As DataRowView In dv
                                    dr.Delete()
                                Next
                                dtItemSch.AcceptChanges()
                                If dtItemSch.Rows.Count <= 0 Then
                                    ShowMessage(getValueByKey("SO056"), "SO056 - " & getValueByKey("CLAE04"))
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
                            Exit Sub
                        End If
                    End If
                    If dtItemSch.Rows.Count > 1 Then
                        Dim objPrice As New frmNCommonView
                        objPrice.SetData = dtItemSch
                        objPrice.ShowDialog()

                        If Not objPrice.search Is Nothing Then
                            CtrlSalesPerson.CtrlTxtBox.Text = objPrice.search(5)

                            drItemSch = dtItemSch.Select("SELLINGPRICE='" & ConvertToEnglish(objPrice.search(7)) & "'")(0)
                        End If
                    Else
                        If dtItemSch.Rows.Count = 1 Then
                            drItemSch = dtItemSch.Rows(0)
                            IsMRPOpen = drItemSch("IsMRPOpen")
                        End If
                    End If
                    If dtItemSch.Rows.Count > 1 AndAlso Not (vEANList.IndexOf(drItemSch("EAN").ToString) = -1) Then
                        IsNewArticle = False
                    Else
                        IsNewArticle = True
                    End If

                    If Not drItemSch Is Nothing AndAlso drItemSch.RowState <> DataRowState.Detached Then
                        SetScanItemInSO(drItemSch)
                    End If

                    dtItemSch.Clear()
                    CtrlSalesPerson.CtrlTxtBox.Text = ""
                    IsMRPOpen = False
                    ArticleScanWithBatchBarcode = False
                    CtrlSalesPerson.CtrlTxtBox.Focus()
                    GridItemSetting()
                End If

            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Sub
    'Private Sub txtSearchItem_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    Try
    '        If e.KeyCode = Keys.Enter Then
    '            txtSearchItem_Leave(sender, New System.EventArgs)
    '        End If
    '        'If CtrlSalesPerson.CtrlTxtBox.Text.Length > 1 Then
    '        '    dtItemSch = objItemSch.GetEANData(clsAdmin.SiteCode, CtrlSalesPerson.CtrlTxtBox.Text)

    '        '    If dtItemSch.Rows.Count > 1 Then
    '        '        Dim objPrice As New frmNCommonView
    '        '        objPrice.SetData = dtItemSch
    '        '        objPrice.ShowDialog()

    '        '        If Not objPrice.search Is Nothing Then
    '        '            CtrlSalesPerson.CtrlTxtBox.Text = objPrice.search(5)

    '        '            drItemSch = dtItemSch.Select("SELLINGPRICE='" & objPrice.search(5) & "'")(0)
    '        '        End If
    '        '    Else
    '        '        If dtItemSch.Rows.Count = 1 Then
    '        '            drItemSch = dtItemSch.Rows(0)
    '        '            IsMRPOpen = drItemSch("IsMRPOpen")
    '        '        End If
    '        '    End If
    '        '    If Not (vEANList.IndexOf(drItemSch("EAN").ToString) = -1) Then
    '        '        IsNewArticle = False
    '        '    Else
    '        '        IsNewArticle = True
    '        '    End If
    '        '    If drItemSch.RowState <> DataRowState.Detached Then
    '        '        SetScanItemInSO(drItemSch)
    '        '    End If


    '        '    dtItemSch.Clear()
    '        '    CtrlSalesPerson.CtrlTxtBox.Text = ""
    '        '    IsMRPOpen = False
    '        '    CtrlSalesPerson.CtrlTxtBox.Focus()
    '        '    GridItemSetting()
    '        'End If

    '    Catch ex As Exception
    '        ShowMessage(ex.Message, "Add Selected Item in Grid...")
    '    End Try

    'End Sub

    ''' <summary>
    ''' Get the Item Details 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtSearchItem_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) 'Handles CtrlSalesPerson.CtrlTxtBox.PreviewKeyDown
        If e.KeyCode = Keys.Enter AndAlso Not (CtrlSalesPerson.CtrlTxtBox.Text = String.Empty) Then
            txtSearchItem_Leave(sender, New System.EventArgs)
            CtrlSalesPerson.CtrlTxtBox.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Update Scan Article Quantity, PickupQty and Delivery Date
    ''' </summary>
    ''' <param name="sender">Selected Row</param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdSOItems_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdSOItems.AfterEdit
        Try
            If grdSOItems.Row = -1 Then Exit Sub
        If Not (vEANList.IndexOf(grdSOItems.Item(grdSOItems.Row, "EAN")) = -1) Then
            IsNewArticle = False
        Else
            IsNewArticle = True
        End If
        Dim ComboSrNo = grdSOItems.Item(grdSOItems.Row, "RowIndex")
        Dim addCondtionRow As String = " AND RowIndex =" & ComboSrNo ' String.Empty

        'Dim addCondtionRow As String = String.Empty
        'If DtSoBulkComboHdr.Rows.Count > 0 Then
        '    Dim drHdr() = DtSoBulkComboHdr.Select("ComboSrNo=" & ComboSrNo)
        '    If drHdr.Count > 0 Then
        '        addCondtionRow = " AND RowIndex =" & ComboSrNo
        '    End If
        'End If

        If IsDBNull(grdSOItems.Item(grdSOItems.Row, "BatchBarcode")) = False Then
            dvCurrentQty = New DataView(dsScan.Tables(0), "EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode = '" & grdSOItems.Item(grdSOItems.Row, "BatchBarcode") & "'" & addCondtionRow, "", DataViewRowState.CurrentRows)
        Else
            dvCurrentQty = New DataView(dsScan.Tables(0), "EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode IS NULL" & addCondtionRow, "", DataViewRowState.CurrentRows)
        End If

        Dim CurrentQty As Double = 0
        If dvCurrentQty.Count > 0 Then
            CurrentQty = dvCurrentQty.ToTable.Rows(0).Item("Quantity")
        End If
        If grdSOItems.Cols(grdSOItems.Col).Name = "PickUpQty" Then
            If Not grdSOItems.Rows(e.Row)("FreezeOB") Is DBNull.Value AndAlso Not grdSOItems.Rows(e.Row)("FreezeSB") Is DBNull.Value Then
                If grdSOItems.Rows(e.Row)("FreezeOB") = True Or grdSOItems.Rows(e.Row)("FreezeSB") = True Then
                    ShowMessage(getValueByKey("SO079"), "SO079 - " & getValueByKey("CLAE04"))
                    grdSOItems.Rows(e.Row)("PickUpQty") = 0
                End If
            End If
        End If
        If grdSOItems.Cols(grdSOItems.Col).Name = "Quantity" Then
            Try
                Dim vOrderQty As Double = IIf(grdSOItems.Item(grdSOItems.Row, "Quantity") Is DBNull.Value, 0, grdSOItems.Item(grdSOItems.Row, "Quantity"))
                Dim vDeliveredQty As Double = IIf(grdSOItems.Item(grdSOItems.Row, "DeliveredQty") Is DBNull.Value, 0, grdSOItems.Item(grdSOItems.Row, "DeliveredQty"))
                Dim vPickUpQty As Double = IIf(grdSOItems.Item(grdSOItems.Row, "PickUpQty") Is DBNull.Value, 0, grdSOItems.Item(grdSOItems.Row, "PickUpQty"))

                IsStrGenerateApplicable = True

                If Not (vOrderQty > 0) Then

                    ShowMessage(getValueByKey("SO005"), "SO005 - " & getValueByKey("CLAE04"))
                    If CurrentQty <= 0 Then CurrentQty = _iArticleQtyBeforeChange

                    'ShowMessage("Order Quantity cannot less than 1.", "Order Quantity Information")
                    grdSOItems.Item(grdSOItems.Row, "Quantity") = CurrentQty

                ElseIf Not (vOrderQty >= vDeliveredQty + vPickUpQty) Then

                    ShowMessage(getValueByKey("SO033"), "SO033 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Order Quantity cannot less than PickUp + Delivered Quantity.", "Order Quantity Information")
                    grdSOItems.Item(grdSOItems.Row, "Quantity") = vDeliveredQty + vPickUpQty

                ElseIf IsDBNull(grdSOItems.Item(grdSOItems.Row, "BatchBarcode")) = False Then
                    Dim BarCodeTable As DataTable = objCM.GetBardCodesForArticle(clsAdmin.SiteCode, grdSOItems.Item(grdSOItems.Row, "ArticleCode"))
                    Dim barcode = BarCodeTable.Select("BatchBarcode='" & grdSOItems.Item(grdSOItems.Row, "BatchBarcode") & "'", "", DataViewRowState.CurrentRows).FirstOrDefault()
                    If Not barcode("QtyAllocated") >= grdSOItems.Rows(e.Row)("Quantity") Then
                        'Dim EventType As Integer
                        ShowMessage(getValueByKey("BatchBarcode003"), getValueByKey("CLAE04"))
                        'If EventType = 1 Then
                        '    Dim code = SearchAvailableBarcodes(grdSOItems.Item(grdSOItems.Row, "ArticleCode"))
                        '    barcode = BarCodeTable.Select("BatchBarcode='" & code & "'", "", DataViewRowState.CurrentRows).FirstOrDefault()
                        '    If Not barcode Is Nothing Then
                        '        If Not barcode("QtyAllocated") >= grdSOItems.Rows(e.Row)("Quantity") Then
                        '            Dim Eve1 As Integer
                        '            ShowMessage(getValueByKey("BatchBarcode002"), getValueByKey("CLAE04"), Eve1, True, getValueByKey("mod009"))
                        '            If Eve1 = 1 Then
                        '                Dim dvPickupQty As DataView
                        '                If IsDBNull(grdSOItems.Item(grdSOItems.Row, "BatchBarcode")) = False Then
                        '                    dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode = '" & grdSOItems.Item(grdSOItems.Row, "BatchBarcode") & "'", "", DataViewRowState.CurrentRows)
                        '                Else
                        '                    dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode IS NULL", "", DataViewRowState.CurrentRows)
                        '                End If
                        '                dvPickupQty(0)("BatchBarcode") = DBNull.Value
                        '                grdSOItems.Item(grdSOItems.Row, "PickUpQty") = 0
                        '            Else
                        '                grdSOItems.Rows(e.Row)("Quantity") = _iArticleQtyBeforeChange
                        '                vOrderQty = _iArticleQtyBeforeChange
                        '            End If

                        '        Else
                        '            Dim dvPickupQty As DataView
                        '            If IsDBNull(grdSOItems.Item(grdSOItems.Row, "BatchBarcode")) = False Then
                        '                dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode = '" & grdSOItems.Item(grdSOItems.Row, "BatchBarcode") & "'", "", DataViewRowState.CurrentRows)
                        '            Else
                        '                dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode IS NULL", "", DataViewRowState.CurrentRows)
                        '            End If
                        '            dvPickupQty(0)("BatchBarcode") = barcode("BatchBarcode")
                        '        End If
                        '    Else
                        '        grdSOItems.Rows(e.Row)("Quantity") = _iArticleQtyBeforeChange
                        '        vOrderQty = _iArticleQtyBeforeChange
                        '    End If

                        'Else
                        '  grdSOItems.Item(grdSOItems.Row, "BatchBarcode") = DBNull.Value
                        grdSOItems.Rows(e.Row)("Quantity") = _iArticleQtyBeforeChange
                        vOrderQty = _iArticleQtyBeforeChange
                    End If

                    'End If

                End If


                If IsApplyPromotion = True Then
                    ' If MsgBox(getValueByKey("SO034"), MsgBoxStyle.YesNo, "SO034") = MsgBoxResult.Yes Then
                    'RemoveApplyPromotion(_dsScan)                   
                    If IsDBNull(grdSOItems.Item(grdSOItems.Row, "BatchBarcode")) = False Then
                        drAddItemExists = _dsScan.Tables("ItemScanDetails").Select("EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode = '" & grdSOItems.Item(grdSOItems.Row, "BatchBarcode") & "' and IsStatus <> 'Deleted' ")
                    Else
                            'drAddItemExists = _dsScan.Tables("ItemScanDetails").Select("EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' and IsStatus <> 'Deleted'  And BatchBarcode IS NULL")
                            drAddItemExists = _dsScan.Tables("ItemScanDetails").Select("EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "'And RowIndex='" & ComboSrNo & "' and IsStatus <> 'Deleted'  And BatchBarcode IS NULL")
                    End If
                    'drAddItemExists = _dsScan.Tables("ItemScanDetails").Select("EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' and IsStatus <> 'Deleted' ")
                    If drAddItemExists.Length > 0 Then
                        IsEditItem = True
                        drAddItemExists(0)("Quantity") = grdSOItems.Item(grdSOItems.Row, "Quantity")
                        drAddItemExists(0)("GrossAmt") = grdSOItems.Item(grdSOItems.Row, "Quantity") * grdSOItems.Item(grdSOItems.Row, "SellingPrice")
                        'SetScanItemInSO(drAddItemExists(0))
                        Dim obj As New clsSaleOrderCommon
                        obj.IsCSTApplicable = IsCSTApplicable
                        obj.RecalculateLine(drAddItemExists(0), CtrlSalesInfo.CtrlTxtOrderNo.Text, dsMain, , False, _iArticleQtyBeforeChange)
                        TotalSalesQty = drAddItemExists(0)("PickupQty") + drAddItemExists(0)("DeliveredQty")
                        Dim ArticleRate As Double = Math.Round(drAddItemExists(0)("NetAmount") / drAddItemExists(0)("Quantity"), 3)
                        drAddItemExists(0)("MinPayAmt") = ((drAddItemExists(0)("Quantity") - TotalSalesQty) * ArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * ArticleRate)
                        drAddItemExists(0)("IsStatus") = "Updated"
                        For Each dr As DataRow In dsMain.Tables("SalesOrderTaxDtls").Select("SiteCode='" & clsAdmin.SiteCode & "' And Finyear='" & clsAdmin.Financialyear & "' And SaleOrderNumber='" & CtrlSalesInfo.CtrlTxtOrderNo.Text & "' And EAN='" & drAddItemExists(0)("EAN") & "'")
                            dr("TaxValue") = (dr("TaxValue") / _iArticleQtyBeforeChange) * drAddItemExists(0)("Quantity")
                        Next
                        RefreshLoadSOData()
                        CalculateSalesOrderSummory(dsScan)
                        GridItemSetting()
                    End If
                    If IsDBNull(grdSOItems.Item(grdSOItems.Row, "BatchBarcode")) = False Then
                        grdSOItems.Item(grdSOItems.Row, "PickUpQty") = vOrderQty
                        grdSOItems.Col = grdSOItems.Cols("PickUpQty").Index
                        grdSOItems_AfterEdit(grdSOItems, New C1.Win.C1FlexGrid.RowColEventArgs(grdSOItems.Row, grdSOItems.Cols("PickUpQty").Index))
                    End If
                    IsQuantityChange = True
                    'Else
                    '    grdSOItems.Item(grdSOItems.Row, "Quantity") = vDeliveredQty + vPickUpQty
                    '    Exit Sub
                    'End If
                End If

            Catch ex As Exception
                ShowMessage(ex.Message, getValueByKey("CLAE05"))
            End Try
        End If

        If grdSOItems.Cols(grdSOItems.Col).Name = "PickUpQty" Then
            Try
                Dim vPickupQty As Double = IIf(grdSOItems.Item(grdSOItems.Row, "PickupQty") Is DBNull.Value, -1, grdSOItems.Item(grdSOItems.Row, "PickupQty"))
                If Not (vPickupQty >= 0) Then
                    ShowMessage(getValueByKey("SO008"), "SO008 - " & getValueByKey("CLAE04"))
                    'ShowMessage("PickUp Quantity cannot less than 1.", "PickUp Quantity Information")
                    grdSOItems.Item(grdSOItems.Row, "PickupQty") = 0
                End If
                Dim dvPickupQty As DataView
                If IsDBNull(grdSOItems.Item(grdSOItems.Row, "BatchBarcode")) = False Then
                    dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode = '" & grdSOItems.Item(grdSOItems.Row, "BatchBarcode") & "'" & addCondtionRow, "", DataViewRowState.CurrentRows)
                Else
                    dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode IS NULL" & addCondtionRow, "", DataViewRowState.CurrentRows)
                End If

                If dvPickupQty.Count > 0 Then
                        dvPickupQty.AllowEdit = True
                        '----------------------------------
                        If IsCSTApplicable Then
                            dtTaxCalc = objCM.getTax(vSiteCode, String.Empty, "SO201", vPickupQty, grdSOItems.Item(grdSOItems.Row, "EAN"), clsDefaultConfiguration.CSTTaxCode, True)
                        Else
                            dtTaxCalc = objCM.getTax(vSiteCode, grdSOItems.Item(grdSOItems.Row, "ArticleCode"), "SO201", vPickupQty, grdSOItems.Item(grdSOItems.Row, "EAN"), clsDefaultConfiguration.CSTTaxCode, False)
                        End If

                        'code added by irfan on 9/10/2017 for ca
                        Dim _strCustNo = vCustomerNo 'CtrlCustSearch1.CtrlTxtCustNo.Text.Trim()

                        ''Dim state As DataTable = objCM.getSiteStateCode(vSiteCode)
                        IsIGSTApplicableForOutsideCustomer = objComn.checkIGSTAplicableForOutSideStateCustomer(clsAdmin.SiteCode, _strCustNo)
                        Dim IGSTtaxCode As String = objComn.ReturnIGSTTaxID(dtTaxCalc)
                        If Not dtTaxCalc Is Nothing AndAlso dtTaxCalc.Rows.Count > 0 Then
                            If IsIGSTApplicableForOutsideCustomer = True Then
                                If IGSTtaxCode <> "" Then
                                    'code by irfan on 12/13/2017
                                    'Dim index As Integer
                                    'For index = 0 To dtTaxCalc.Rows.Count - 1
                                    '    index = 0
                                    '    If dtTaxCalc.Rows(0)("TAXCODE").ToString <> IGSTtaxCode Then
                                    '        dtTaxCalc.Rows.RemoveAt(index)
                                    '        dtTaxCalc.AcceptChanges()
                                    '    Else
                                    '        Exit For
                                    '    End If
                                    'Next
                                    Dim dv As New DataView(dtTaxCalc, "TAXCODE='" & IGSTtaxCode & "'", "", DataViewRowState.CurrentRows)
                                    dtTaxCalc = dv.ToTable
                                    'commented by irfan 
                                    'Else
                                    '    Dim index As Integer
                                    '    For index = 0 To dtTaxCalc.Rows.Count - 1
                                    '        If dtTaxCalc.Rows.Count > 0 Then
                                    '            index = 0
                                    '            dtTaxCalc.Rows.RemoveAt(index)
                                    '            dtTaxCalc.AcceptChanges()
                                    '        Else
                                    '            Exit For
                                    '        End If
                                    '    Next
                                End If
                            Else
                                If dtTaxCalc.Rows.Count > 0 Then
                                    Dim index As Integer
                                    For index = 0 To dtTaxCalc.Rows.Count - 1
                                        If dtTaxCalc.Rows(index)("TAXCODE").ToString.Trim = IGSTtaxCode Then
                                            dtTaxCalc.Rows.RemoveAt(index)
                                            dtTaxCalc.AcceptChanges()
                                            Exit For
                                        End If
                                    Next
                                End If
                            End If
                        End If


                        'Rakesh-01.10.2013:7835-->Check article stock balance quantity
                        If (Not clsDefaultConfiguration.NegativeInventoryAllowed) Then
                            Dim objCommon As New clsCommon

                            Dim articleCode = grdSOItems.Item(grdSOItems.Row, "ArticleCode")
                            Dim articleEAN = grdSOItems.Item(grdSOItems.Row, "EAN")
                            Dim iPickUpQty = grdSOItems.Item(grdSOItems.Row, "PickUpQty")

                            Dim StockQty As Double = objCommon.GetStocks(clsAdmin.SiteCode, articleEAN, articleCode, False)

                            If (StockQty < iPickUpQty) Then
                                ShowMessage(String.Format(getValueByKey("SB015"), StockQty), "SB015 - " & getValueByKey("CLAE04"))
                                grdSOItems.Item(grdSOItems.Row, "PickUpQty") = 0
                            End If
                        End If

                        For Each drPickupQty As DataRowView In dvPickupQty
                            If Val(grdSOItems.Item(grdSOItems.Row, "PickupQty")) + Val(grdSOItems.Item(grdSOItems.Row, "DeliveredQty")) <= grdSOItems.Item(grdSOItems.Row, "Quantity") Then
                                drPickupQty("PickupQty") = grdSOItems.Item(grdSOItems.Row, "PickupQty")
                                drPickupQty("IsStatus") = "Updated"
                                If drPickupQty("PickupQty") = 0 Then
                                    drPickupQty("BatchBarcode") = DBNull.Value
                                End If
                                'If clsDefaultConfiguration.IsBatchManagementReq AndAlso drPickupQty("PickupQty") > 0 AndAlso IsDBNull(drPickupQty("BatchBarcode")) Then
                                '    Dim barcodeScan As New frmSpecialPrompt(getValueByKey("SP0010"))
                                '    barcodeScan.ShowTextBox = True
                                '    barcodeScan.txtValue.Text = ""
                                '    barcodeScan.AcceptButton = barcodeScan.cmdOk
                                '    barcodeScan.AllowText = True
                                '    barcodeScan.ShowDialog()
                                '    Dim str As String = barcodeScan.GetResult()
                                '    Dim BarCodeTable As DataTable = objCM.GetBardCodesForArticle(clsAdmin.SiteCode, drPickupQty("ArticleCode"))

                                '    If Not BarCodeTable Is Nothing AndAlso BarCodeTable.Rows.Count > 0 Then
                                '        If String.IsNullOrEmpty(str) = False Then
                                '            Dim barcode = BarCodeTable.Select("BatchBarcode='" & str & "'", "", DataViewRowState.CurrentRows).FirstOrDefault()
                                '            If Not barcode Is Nothing Then
                                '                If barcode("QtyAllocated") > 0 Then
                                '                    If drPickupQty("PickupQty") <= barcode("QtyAllocated") Then
                                '                        drPickupQty("BatchBarcode") = str
                                '                    Else
                                '                        drPickupQty("BatchBarcode") = str
                                '                        drPickupQty("PickupQty") = barcode("QtyAllocated")
                                '                    End If
                                '                Else
                                '                    MessageBox.Show(getValueByKey("BatchBarcode003"), getValueByKey("CLAE04"))
                                '                    drPickupQty("PickupQty") = 0
                                '                    drPickupQty("BatchBarcode") = DBNull.Value
                                '                End If
                                '            Else
                                '                MessageBox.Show(getValueByKey("BatchBarcode004"), getValueByKey("CLAE04"))
                                '                drPickupQty("PickupQty") = 0
                                '                drPickupQty("BatchBarcode") = DBNull.Value
                                '            End If
                                '        Else
                                '            drPickupQty("PickupQty") = 0
                                '            drPickupQty("BatchBarcode") = DBNull.Value
                                '        End If
                                '    Else
                                '        MessageBox.Show(getValueByKey("BatchBarcode005"), getValueByKey("CLAE04"))
                                '        drPickupQty("PickupQty") = 0
                                '        drPickupQty("BatchBarcode") = DBNull.Value
                                '    End If
                                'End If
                                TotalSalesQty = drPickupQty("PickupQty") + drPickupQty("DeliveredQty")
                                NetArticleRate = Math.Round(drPickupQty("NetAmount") / drPickupQty("Quantity"), 3)
                                drPickupQty("MinPayAmt") = Math.Round(((drPickupQty("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate), 3)
                                drPickupQty("TotalPickUpAmt") = (TotalSalesQty * NetArticleRate)
                            Else
                                grdSOItems.Item(grdSOItems.Row, "PickupQty") = 0
                                ShowMessage(getValueByKey("SO009"), "SO009 - " & getValueByKey("CLAE04"))
                                'ShowMessage("Pick Up Quantity cannot greater than Order Quantity.", "Information")
                            End If
                            'lblPickupQty.Text = CDbl(dsScan.Tables("ItemScanDetails").Compute("SUM(PickUpQty)", ""))
                        Next
                        _dsScan.AcceptChanges()
                    End If

                    CalculateSalesOrderSummory(dsScan)

                Catch ex As Exception
                    ShowMessage(ex.Message, getValueByKey("CLAE05"))
                End Try
        End If

        If grdSOItems.Cols(grdSOItems.Col).Name = "ExpDelDate" Then
            Try
                grdSOItems.EndUpdate()
                ' If Not (Format(grdSOItems.Item(grdSOItems.Row, grdSOItems.Col), vDateFormat) >= Format(vCurrentDate, vDateFormat)) Then
                If Not grdSOItems.Item(grdSOItems.Row, grdSOItems.Col) >= vCurrentDate.Date Then
                    ShowMessage(getValueByKey("SO010"), "SO010 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Delivery Date cannot be backdated.", "Delivery Date")
                    grdSOItems.Item(grdSOItems.Row, "ExpDelDate") = CtrlSalesInfo.CtrlDtExpDelDate.Value
                ElseIf (vCurrentDate.AddSeconds(-1) > Convert.ToDateTime(grdSOItems.Item(grdSOItems.Row, grdSOItems.Col))) Then
                    ShowMessage(getValueByKey("SO010"), "SO010 - " & getValueByKey("CLAE04"))
                    grdSOItems.Item(grdSOItems.Row, "ExpDelDate") = CtrlSalesInfo.CtrlDtExpDelDate.Value
                Else
                    Dim dvPickupQty As DataView
                    If IsDBNull(grdSOItems.Item(grdSOItems.Row, "BatchBarcode")) = False Then
                        dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode = '" & grdSOItems.Item(grdSOItems.Row, "BatchBarcode") & "'" & addCondtionRow, "", DataViewRowState.CurrentRows)
                    Else
                        dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode IS NULL" & addCondtionRow, "", DataViewRowState.CurrentRows)
                    End If

                    If dvPickupQty.Count > 0 Then
                        dvPickupQty.AllowEdit = True

                        For Each drPickupQty As DataRowView In dvPickupQty
                            drPickupQty("ExpDelDate") = grdSOItems.Item(grdSOItems.Row, "ExpDelDate")
                        Next
                        _dsScan.AcceptChanges()
                    End If
                End If
            Catch ex As Exception
                ShowMessage(ex.Message, getValueByKey("CLAE05"))
            End Try
        End If

        If grdSOItems.Cols(grdSOItems.Col).Name = "ReservedQty" Then
            Try
                Dim dvPickupQty As DataView
                If IsDBNull(grdSOItems.Item(grdSOItems.Row, "BatchBarcode")) = False Then
                    dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode = '" & grdSOItems.Item(grdSOItems.Row, "BatchBarcode") & "'", "", DataViewRowState.CurrentRows)
                Else
                    dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode IS NULL", "", DataViewRowState.CurrentRows)
                End If

                If dvPickupQty.Count > 0 Then
                    dvPickupQty.AllowEdit = True
                    For Each drPickupQty As DataRowView In dvPickupQty
                        drPickupQty("ReservedQty") = grdSOItems.Item(grdSOItems.Row, "ReservedQty")
                    Next
                    _dsScan.AcceptChanges()
                End If
            Catch ex As Exception
                ShowMessage(ex.Message, getValueByKey("CLAE05"))
            End Try
        End If

        If grdSOItems.Cols(grdSOItems.Col).Name = "IsCLP" Then
            Try
                Dim dvPickupQty As DataView
                If IsDBNull(grdSOItems.Item(grdSOItems.Row, "BatchBarcode")) = False Then
                    dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode = '" & grdSOItems.Item(grdSOItems.Row, "BatchBarcode") & "'", "", DataViewRowState.CurrentRows)
                Else
                    dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode IS NULL", "", DataViewRowState.CurrentRows)
                End If

                If dvPickupQty.Count > 0 Then
                    dvPickupQty.AllowEdit = True
                    For Each drPickupQty As DataRowView In dvPickupQty
                        drPickupQty("IsCLP") = grdSOItems.Item(grdSOItems.Row, "IsCLP")
                    Next
                    _dsScan.AcceptChanges()
                End If
            Catch ex As Exception
                ShowMessage(ex.Message, getValueByKey("CLAE05"))
            End Try
        End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete Selected Article from DataGrid
    ''' </summary>
    ''' <param name="sender">Select Row</param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdScanItem_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdSOItems.CellButtonClick
        Try
            Dim BulkComboMstId As Int64 = 0
            Dim ComboSrNo = grdSOItems.Item(grdSOItems.Row, "RowIndex")
            'Dim deleteRowNo As Integer = 0
            If DtSoBulkComboHdr.Rows.Count > 0 Then
                Dim drHdr() = DtSoBulkComboHdr.Select("ComboSrNo=" & ComboSrNo)
                'If drHdr.Count > 0 Then
                '    deleteRowNo = ComboSrNo
                'End If
            End If

            If MsgBox(getValueByKey("SO011"), MsgBoxStyle.YesNo, "SO011 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                If Not (grdSOItems.Item(grdSOItems.Row, "DeliveredQty") > 0) Then
                    If IsDBNull(grdSOItems.Item(grdSOItems.Row, "BatchBarcode")) = False Then
                        DeleteScanItemInSO(grdSOItems.Item(grdSOItems.Row, "EAN"), grdSOItems.Item(grdSOItems.Row, "BatchBarcode"), ComboSrNo)
                    Else
                        DeleteScanItemInSO(grdSOItems.Item(grdSOItems.Row, "EAN"), , ComboSrNo)
                    End If
                Else
                    ShowMessage(getValueByKey("SO035"), "SO035 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Items cannot deleted.", "Delivery Date Information")
                End If
                ''----Delete Bulk Combo Details As Well...
                If DtSoBulkComboHdr.Rows.Count > 0 Then
                    DeleteBulkCombo(ComboSrNo)
                    IsStrGenerateApplicable = True
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Sub

    '---- Commented By Mahesh Write new one 
    'Private Sub grdScanItem_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdSOItems.CellButtonClick
    '    Try
    '        'commented by rama for bug no 0000563
    '        'If grdSOItems.Item(grdSOItems.Row, "ExpDelDate") >= Format(vCurrentDate, vDateFormat) Then
    '        If MsgBox(getValueByKey("SO011"), MsgBoxStyle.YesNo, "SO011 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
    '            If Not (grdSOItems.Item(grdSOItems.Row, "DeliveredQty") > 0) Then
    '                If IsDBNull(grdSOItems.Item(grdSOItems.Row, "BatchBarcode")) = False Then
    '                    DeleteScanItemInSO(grdSOItems.Item(grdSOItems.Row, "EAN"), grdSOItems.Item(grdSOItems.Row, "BatchBarcode"))
    '                Else
    '                    DeleteScanItemInSO(grdSOItems.Item(grdSOItems.Row, "EAN"))
    '                End If
    '            Else
    '                ShowMessage(getValueByKey("SO035"), "SO035 - " & getValueByKey("CLAE04"))
    '                'ShowMessage("Items cannot deleted.", "Delivery Date Information")
    '            End If
    '        End If

    '        'Else
    '        'ShowMessage(getValueByKey("SO010"), "SO010")
    '        'ShowMessage("Delivery Date of Item cannot be backdated.", "Delivery Date Information")
    '        'End If
    '    Catch ex As Exception
    '        ShowMessage(ex.Message, getValueByKey("CLAE05"))
    '    End Try

    'End Sub

    ''' <summary>
    ''' Delete Selected Article from DataGrid
    ''' </summary>
    ''' <param name="sender">Select Row</param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdSOItems_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSOItems.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                '    If grdSOItems.Item(grdSOItems.Row, "ExpDelDate") >= Format(vCurrentDate, vDateFormat) Then
                '        If MsgBox(getValueByKey("SO011"), MsgBoxStyle.YesNo, "SO011 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                '            If Not (grdSOItems.Item(grdSOItems.Row, "DeliveredQty") > 0) Then
                '                If IsDBNull(grdSOItems.Item(grdSOItems.Row, "BatchBarcode")) = False Then
                '                    DeleteScanItemInSO(grdSOItems.Item(grdSOItems.Row, "EAN"), grdSOItems.Item(grdSOItems.Row, "BatchBarcode"))
                '                Else
                '                    DeleteScanItemInSO(grdSOItems.Item(grdSOItems.Row, "EAN"))
                '                End If
                '            Else
                '                ShowMessage(getValueByKey("SO035"), "SO035 - " & getValueByKey("CLAE04"))
                '                'ShowMessage("Items cannot deleted.", "Delivery Date Information")
                '            End If
                '        End If
                '    Else
                '        ShowMessage(getValueByKey("SO010"), "SO010 - " & getValueByKey("CLAE04"))
                '        'ShowMessage("Delivery Date of Item cannot be backdated.", "Delivery Date Information")
                '    End If
                'End If

                Dim BulkComboMstId As Int64 = 0
                Dim ComboSrNo = grdSOItems.Item(grdSOItems.Row, "RowIndex")
                'Dim deleteRowNo As Integer = 0
                If DtSoBulkComboHdr.Rows.Count > 0 Then
                    Dim drHdr() = DtSoBulkComboHdr.Select("ComboSrNo=" & ComboSrNo)
                    'If drHdr.Count > 0 Then
                    '    deleteRowNo = ComboSrNo
                    'End If
                End If
                If grdSOItems.Item(grdSOItems.Row, "ExpDelDate") >= Format(vCurrentDate, vDateFormat) Then
                    If MsgBox(getValueByKey("SO011"), MsgBoxStyle.YesNo, "SO011 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                        If Not (grdSOItems.Item(grdSOItems.Row, "DeliveredQty") > 0) Then
                            If IsDBNull(grdSOItems.Item(grdSOItems.Row, "BatchBarcode")) = False Then
                                'DeleteScanItemInSO(grdSOItems.Item(grdSOItems.Row, "EAN"), grdSOItems.Item(grdSOItems.Row, "BatchBarcode"), deleteRowNo)
                                DeleteScanItemInSO(grdSOItems.Item(grdSOItems.Row, "EAN"), grdSOItems.Item(grdSOItems.Row, "BatchBarcode"), ComboSrNo)
                            Else
                                'DeleteScanItemInSO(grdSOItems.Item(grdSOItems.Row, "EAN"), , deleteRowNo)
                                DeleteScanItemInSO(grdSOItems.Item(grdSOItems.Row, "EAN"), , ComboSrNo)
                            End If
                        Else
                            ShowMessage(getValueByKey("SO035"), "SO035 - " & getValueByKey("CLAE04"))
                            'ShowMessage("Items cannot deleted.", "Delivery Date Information")
                        End If
                        ''----Delete Bulk Combo Details As Well...
                        If DtSoBulkComboHdr.Rows.Count > 0 Then
                            DeleteBulkCombo(ComboSrNo)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Sub

    ''' <summary>
    ''' Show the image of the Current Selected Article
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdSOItems_RowColChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSOItems.RowColChange
        Try
            If (grdSOItems.Row >= 1) Then

                'grdSOItems.Cols("ArticleCode").Visible = True
                vArticleCode = grdSOItems.Item(grdSOItems.Row, "ArticleCode")
                'grdSOItems.Cols("ArticleCode").Visible = False

                'Dim strUrl As String = objComn.GetArticleImage(vArticleCode, My.Settings.ArticleImageFolder)
                Dim strUrl As String = objComn.GetArticleImage(vArticleCode, ReadSpectrumParamFile("ArticleImageFolder"))
                'PictureBoxImages.ImageLocation = strUrl
                CtrlProductImage.ShowArticleImage(vArticleCode)
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try


    End Sub

    ''' <summary>
    ''' Delete Selected Article from DataGrid
    ''' </summary>
    ''' <param name="vEAN">Selected EAN</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function DeleteScanItemInSO(ByVal vEAN As String, Optional batchBarcode As String = "", Optional rowIndex As Integer = 0) As Boolean
        Try
            '---Add Condtion by Mahesh incase of combo only one combo need to deleted 
            Dim addCondtionRow As String = String.Empty
            Dim addCondtionBill As String = String.Empty
            If rowIndex > 0 Then
                addCondtionRow = " AND RowIndex =" & rowIndex
                addCondtionBill = " AND BillLineNo =" & rowIndex
            End If
            If String.IsNullOrEmpty(batchBarcode) = False Then
                For Each drRow As DataRow In dsMain.Tables("SalesOrderDtl").Select("EAN='" & vEAN & "' And BatchBarcode = '" & batchBarcode & "'" & addCondtionBill, "", DataViewRowState.CurrentRows)
                    'Dim btn = grdSOItems.Controls.OfType(Of Button).Where(Function(w) w.Tag = drRow("EAN").ToString()).FirstOrDefault()
                    drRow.Delete()
                Next
            Else
                For Each drRow As DataRow In dsMain.Tables("SalesOrderDtl").Select("EAN='" & vEAN & "' And BatchBarcode IS NULL" & addCondtionBill, "", DataViewRowState.CurrentRows)
                    drRow.Delete()
                Next
            End If
            If grdSOItems.Controls.Find("btnBarcode" + vEAN, True).Count() > 0 Then
                For Each c As Control In grdSOItems.Controls.Find("btnBarcode" + vEAN, True)
                    grdSOItems.Controls.Remove(c)
                    Me.Controls.Remove(c)
                Next
                Me.Batchbarcode.RemoveAll(Function(w) w.EAN = vEAN)

            End If

            Dim dvEdit As DataView
            If String.IsNullOrEmpty(batchBarcode) = False Then
                dvEdit = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & vEAN & "' And BatchBarcode = '" & batchBarcode & "'" & addCondtionRow, "", DataViewRowState.CurrentRows)
            Else
                dvEdit = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & vEAN & "' And BatchBarcode IS NULL" & addCondtionRow, "", DataViewRowState.CurrentRows)
            End If

            If dvEdit.Count > 0 Then
                dvEdit.AllowEdit = True

                For Each drView As DataRowView In dvEdit
                    'If Not (vEANList.IndexOf(grdSOItems.Item(grdSOItems.Row, "EAN")) = -1) Then
                    '    drView("IsStatus") = "Deleted"
                    '    grdSOItems.Item(grdSOItems.Row, "IsStatus") = "Deleted"
                    '    drView.Delete()
                    'Else
                    Dim dvTax As New DataView(dsMain.Tables("SalesOrderTaxDtls"), "EAN='" & vEAN & "'", "", DataViewRowState.CurrentRows)
                    If dvTax.Count > 0 Then
                        dvTax.AllowDelete = True
                        For Each dr As DataRowView In dvTax
                            dr.Delete()
                        Next
                    End If

                    drView.Delete()
                    ' End If
                Next

                _dsScan.AcceptChanges()
            End If

            RefreshLoadSOData()
            CalculateSalesOrderSummory(dsScan)
            GridItemSetting()
            If grdSOItems.Rows.Count = 1 Then
                CtrlProductImage.ShowArticleImage("")
                'PictureBoxImages.Image = Nothing
                lblOrderQty.Text = 0
                lblPickupQty.Text = 0
                CtrlCashSummary1.lbltxt1 = strZero
                CtrlCashSummary1.lbltxt2 = strZero
                CtrlCashSummary1.lbltxt3 = strZero
                CtrlCashSummary1.lbltxt4 = strZero
                lblReceivedAmt.Text = strZero
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    ''' <summary>
    ''' Add Article In Scan DataGrid
    ''' </summary>
    ''' <param name="drItemsRow">Data Row</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function SetScanItemInSO(ByVal drItemsRow As DataRow) As Boolean
        Dim dtTaxCalc As DataTable
        Dim drAddItem As DataRow
        Dim findkeyTax(4) As Object

        Dim vTotalNetAmt As Double = 0.0
        Dim vIncTaxAmt As Double = 0.0
        Dim vExclTaxAmt As Double = 0.0
        Dim vGetArtilcePrice As Double = 0.0

        Try
            If Not (drItemsRow Is Nothing) Then

                StockQty = objCM.GetStocks(vSiteCode, drItemsRow.Item("EAN"), drItemsRow.Item("ArticleCode"), True, False, IIf(IsDBNull(drItemsRow.Item("BatchBarcode")) = False, drItemsRow.Item("BatchBarcode"), String.Empty))

                'Rakesh:06.11.2013-->7895 : Avoid stock check validation when order place from SO & BL
                'If CDbl(StockQty) <= 0 Then
                '    If clsDefaultConfiguration.NegativeInventoryAllowed = False Then
                '        ShowMessage(getValueByKey("SO001"), "SO001 - " & getValueByKey("CLAE04"))
                '        Exit Function
                '    End If
                'End If

                If IsMRPOpen = True Then
                    Dim objPrompt As New frmSpecialPrompt(getValueByKey("CMR15"))
                    objPrompt.ShowMessage = False
                    objPrompt.ShowTextBox = True
                    objPrompt.AllowDecimal = True
                    objPrompt.ShowDialog()

                    vGetArtilcePrice = objPrompt.GetResult()
                    objPrompt.Dispose()

                    If CDbl(vGetArtilcePrice) <= 0 Then
                        ShowMessage(getValueByKey("SO036"), "SO036 - " & getValueByKey("CLAE04"))
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

                'If IsDBNull(drItemsRow.Item("BatchBarcode")) = False Then
                '    drAddItemExists = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "' And BatchBarcode = '" & drItemsRow.Item("BatchBarcode") & "'")
                'Else
                '    drAddItemExists = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "'  And BatchBarcode IS NULL")
                'End If
                'drAddItemExists = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "'")              

                If clsDefaultConfiguration.IsBatchManagementReq Then

                    If (drAddItemExists.Count > 0 AndAlso Not (StockQty > drAddItemExists(0)("PickUpQty"))) Then
                        If (ArticleScanWithBatchBarcode) Then
                            ShowMessage(getValueByKey("SO001"), "SO001 - " & getValueByKey("CLAE04"))
                            drAddItemExists(0)("Quantity") = drAddItemExists(0)("Quantity") + 1
                            'Exit Function
                        End If
                    End If
                End If

                If IsNewArticle = True Then
                    If drAddItemExists.Length = 0 Then
                        drAddItem = _dsScan.Tables("ItemScanDetails").NewRow
                        drAddItem("Quantity") = 1
                        drAddItem("DeliverySiteCode") = DeliverySiteCode
                        drAddItem("PickUpQty") = 0
                        drAddItem("DeliveredQty") = 0
                        drAddItem("IsStatus") = "Updated"
                    Else
                        drAddItem = drAddItemExists(0)
                        drAddItem("IsStatus") = "Updated"
                        'If IsEditItem = False Then
                        If clsDefaultConfiguration.IsBatchManagementReq Then
                            Dim OrderQty As Decimal = Batchbarcode.Where(Function(w) w.EAN = drAddItem("EAN").ToString()).Sum(Function(w) w.Qty) + drAddItem("DeliveredQty")

                            If drAddItem("Quantity") < OrderQty Then
                                drAddItem("Quantity") = drAddItem("Quantity") + 1
                            End If

                            If (Not ArticleScanWithBatchBarcode) Then
                                drAddItem("Quantity") = drAddItem("Quantity") + 1
                            End If
                        Else
                            drAddItem("Quantity") = drAddItem("Quantity") + 1
                        End If

                        '    Else
                        '    drAddItem("Quantity") = grdSOItems.Item(grdSOItems.Row, "Quantity")
                        'End If
                    End If
                Else

                    If drAddItemExists.Length = 0 Or (drAddItemExists.Length > 0 AndAlso drAddItemExists(0).Item("IsStatus") = "Deleted") Then
                        drAddItem = drAddItemExists(0)
                        drAddItem("Quantity") = drAddItem("DeliveredQty") + 1
                        drAddItem("IsStatus") = "Updated"
                    Else
                        drAddItem = drAddItemExists(0)
                        drAddItem("IsStatus") = "Updated"
                        'If IsEditItem = False Then
                        If clsDefaultConfiguration.IsBatchManagementReq Then
                            If drAddItem("Quantity") < (Batchbarcode.Where(Function(w) w.EAN = drAddItem("EAN").ToString()).Sum(Function(w) w.Qty) + drAddItem("DeliveredQty")) Then
                                drAddItem("Quantity") = drAddItem("Quantity") + 1
                            End If

                        Else
                            drAddItem("Quantity") = drAddItem("Quantity") + 1
                        End If


                        'Else
                        '    drAddItem("Quantity") = grdSOItems.Item(grdSOItems.Row, "Quantity")
                        'End If
                    End If
                End If

                drAddItem("Discount") = 0
                drAddItem("EAN") = drItemsRow.Item("EAN")
                drAddItem("Discription") = drItemsRow.Item("DISCRIPTION")
                drAddItem("BatchBarcode") = drItemsRow.Item("BatchBarcode")
                If drAddItemExists.Length = 0 Then
                    drAddItem("SellingPrice") = FormatNumber(vGetArtilcePrice, 2)
                End If

                drAddItem("LastNodeCode") = drItemsRow.Item("Nodes").ToString()
                If IsCSTApplicable Then
                    dtTaxCalc = objCM.getTax(vSiteCode, String.Empty, "SO201", drAddItem("Quantity"), drAddItem("EAN"), clsDefaultConfiguration.CSTTaxCode, True)
                Else
                    dtTaxCalc = objCM.getTax(vSiteCode, drItemsRow.Item("ARTICLECODE"), "SO201", drAddItem("Quantity"), drAddItem("EAN"))
                End If
                'code added by irfan on 23/10/2017 for IGST CAKEKRAFT
                Dim objso As New clsSaleOrderCommon
                Dim _strCustNo = CtrlCustDtls1.lblCustNoValue.Text
                'objso.cardno = _strCustNo

                ''Dim state As DataTable = objCM.getSiteStateCode(vSiteCode)
                IsIGSTApplicableForOutsideCustomer = objComn.checkIGSTAplicableForOutSideStateCustomer(clsAdmin.SiteCode, _strCustNo)
                Dim IGSTtaxCode As String = objComn.ReturnIGSTTaxID(dtTaxCalc)

                If Not dtTaxCalc Is Nothing AndAlso dtTaxCalc.Rows.Count > 0 Then
                    If IsIGSTApplicableForOutsideCustomer = True Then
                        If IGSTtaxCode <> "" Then
                            'code by irfan on 12/13/2017
                            'Dim index As Integer
                            'For index = 0 To dtTaxCalc.Rows.Count - 1
                            '    index = 0
                            '    If dtTaxCalc.Rows(0)("TAXCODE").ToString <> IGSTtaxCode Then
                            '        dtTaxCalc.Rows.RemoveAt(index)
                            '        dtTaxCalc.AcceptChanges()
                            '    Else
                            '        Exit For
                            '    End If
                            'Next
                            Dim dv As New DataView(dtTaxCalc, "TAXCODE='" & IGSTtaxCode & "'", "", DataViewRowState.CurrentRows)
                            dtTaxCalc = dv.ToTable
                            'commented by irfan 
                            'Else
                            '    Dim index As Integer
                            '    For index = 0 To dtTaxCalc.Rows.Count - 1
                            '        If dtTaxCalc.Rows.Count > 0 Then
                            '            index = 0
                            '            dtTaxCalc.Rows.RemoveAt(index)
                            '            dtTaxCalc.AcceptChanges()
                            '        Else
                            '            Exit For
                            '        End If
                            '    Next
                        End If
                    Else
                        If dtTaxCalc.Rows.Count > 0 Then
                            Dim index As Integer
                            For index = 0 To dtTaxCalc.Rows.Count - 1
                                If dtTaxCalc.Rows(index)("TAXCODE").ToString.Trim = IGSTtaxCode Then
                                    dtTaxCalc.Rows.RemoveAt(index)
                                    dtTaxCalc.AcceptChanges()
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If


                objCM.DecimalDigits = clsDefaultConfiguration.DecimalPlaces

                vTotalNetAmt = Math.Round(vGetArtilcePrice * drAddItem("Quantity"), 3)
                If dtTaxCalc.Rows.Count <> 0 Then

                    If IsCSTApplicable Then
                        'vTotalNetAmt = vTotalNetAmt - GetTaxableAmountForCst(drItemsRow.Item("ARTICLECODE"), drItemsRow.Item("EAN"), drAddItem("Quantity"), vTotalNetAmt)
                        'dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = vTotalNetAmt
                        'objCM.getCalculatedDataSet(dtTaxCalc, drAddItem("Quantity"), True)

                        Dim inctax = GetTaxableAmountForCst(drItemsRow.Item("ARTICLECODE"), drItemsRow.Item("EAN"), drAddItem("Quantity"), vTotalNetAmt)
                        vTotalNetAmt = vTotalNetAmt - inctax
                        dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = vTotalNetAmt
                        objCM.getCalculatedDataSet(dtTaxCalc)
                        vTotalNetAmt = vTotalNetAmt + dtTaxCalc(0)("TAXAMOUNT")
                        If drAddItemExists.Length = 0 Then
                            drAddItem("SellingPrice") = vGetArtilcePrice - (inctax / dtTaxCalc(0)("ITEMQTY"))
                        End If



                    Else
                        dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = vTotalNetAmt
                        objCM.getCalculatedDataSet(dtTaxCalc)
                    End If

                    'dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = vTotalNetAmt
                    'objCM.getCalculatedDataSet(dtTaxCalc, drAddItem("Quantity"))

                    For iRowTax = 0 To dtTaxCalc.Rows.Count - 1

                        If CDbl(dtTaxCalc.Rows(iRowTax)("VALUE").ToString) <> 0 Then
                            If dtTaxCalc.Rows(iRowTax)("INCLUSIVE") = True Then
                                vIncTaxAmt = vIncTaxAmt + CDbl(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT"))
                            Else
                                vExclTaxAmt = vExclTaxAmt + CDbl(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT"))
                            End If
                        End If
                    Next

                    vIncTaxAmt = Math.Round(vIncTaxAmt, 3)
                    vExclTaxAmt = Math.Round(vExclTaxAmt, 3)

                    Dim drRowTax As DataRow
                    If dtTaxCalc.Rows.Count <> 0 Then

                        Dim vTaxLineNo As Integer = 0

                        For Each drRowTax In dtTaxCalc.Rows
                            vTaxLineNo += 1

                            findkeyTax(0) = vSiteCode
                            findkeyTax(1) = vfinancialYear
                            findkeyTax(2) = CtrlSalesInfo.CtrlTxtOrderNo.Text
                            findkeyTax(3) = drAddItem("EAN")
                            findkeyTax(4) = vTaxLineNo

                            drTax = dsMain.Tables("SalesOrderTaxDtls").Rows.Find(findkeyTax)

                            If drTax Is Nothing Then
                                drTax = dsMain.Tables("SalesOrderTaxDtls").NewRow

                                drTax("SiteCode") = vSiteCode
                                drTax("FinYear") = vfinancialYear
                                drTax("SaleOrderNumber") = CtrlSalesInfo.CtrlTxtOrderNo.Text
                                drTax("EAN") = drAddItem("EAN")
                                drTax("TaxLineNo") = vTaxLineNo
                                drTax("TaxLabel") = drRowTax("TaxCode")
                                drTax("TaxValue") = drRowTax("TaxAmount")

                                dsMain.Tables("SalesOrderTaxDtls").Rows.Add(drTax)
                            Else
                                drTax("SiteCode") = vSiteCode
                                drTax("SaleOrderNumber") = CtrlSalesInfo.CtrlTxtOrderNo.Text
                                drTax("EAN") = drAddItem("EAN")
                                drTax("TaxLineNo") = vTaxLineNo
                                drTax("TaxLabel") = drRowTax("TaxCode")
                                drTax("TaxValue") = drRowTax("TaxAmount")
                            End If
                        Next
                    End If

                    drAddItem("ExclTaxAmt") = vExclTaxAmt
                    drAddItem("IncTaxAmt") = vIncTaxAmt
                    'TotalTaxAmt
                    drAddItem("TotalTaxAmt") = vIncTaxAmt + vExclTaxAmt
                Else
                    drAddItem("ExclTaxAmt") = 0
                    drAddItem("IncTaxAmt") = 0
                End If

                drAddItem("NetAmount") = FormatNumber(vTotalNetAmt + vExclTaxAmt, 2)
                drAddItem("ExpDelDate") = CtrlSalesInfo.CtrlDtExpDelDate.Value
                drAddItem("Stock") = StockQty
                drAddItem("IsCLP") = True

                drAddItem("ArticleCode") = drItemsRow.Item("ARTICLECODE")
                drAddItem("GrossAmt") = Math.Round(drAddItem("SellingPrice") * drAddItem("Quantity"), 3)
                'drAddItem("ReservedQty") = 0
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
                drAddItem("CostAmount") = drItemsRow.Item("CostAmt")
                'Rakesh-01.10.2013:8006--> Allow to add new article in sales order
                If Not (drAddItem("NetAmount") = 0.0) Then
                    If drAddItemExists.Length = 0 Then
                        drAddItem("RowIndex") = vRowIndex
                        'Change by Ashish on 29 Nov 2010
                        'Commenting the below line since it was adding rows one below other in the grid
                        'instead of adding the recent scanned item on top of the grid
                        _dsScan.Tables("ItemScanDetails").Rows.Add(drAddItem)
                        '_dsScan.Tables("ItemScanDetails").Rows.InsertAt(drAddItem, 0)
                        'end of change
                        vRowIndex = vRowIndex + 1
                        '---- New Row Added can Generate STR 
                        IsStrGenerateApplicable = True
                        If Batchbarcode IsNot Nothing AndAlso Batchbarcode.Select(Function(w) w.EAN = drAddItem("EAN").ToString()).Count() > 0 Then
                            drAddItem("PickUpQty") = Batchbarcode.Where(Function(w) w.EAN = drAddItem("EAN").ToString()).Sum(Function(w) w.Qty)
                            grdSOItems_AfterEdit(grdSOItems, New C1.Win.C1FlexGrid.RowColEventArgs(grdSOItems.RowSel, grdSOItems.Cols("PickUpQty").Index))
                        End If
                    Else

                        If Batchbarcode IsNot Nothing AndAlso Batchbarcode.Select(Function(w) w.EAN = drAddItem("EAN").ToString()).Count() > 0 Then

                            If (ArticleScanWithBatchBarcode AndAlso StockQty > drAddItem("PickUpQty")) Then
                                drAddItem("PickUpQty") = Batchbarcode.Where(Function(w) w.EAN = drAddItem("EAN").ToString()).Sum(Function(w) w.Qty)
                            ElseIf (ArticleScanWithBatchBarcode AndAlso drAddItem("ReservedQty")) Then
                                ShowMessage(getValueByKey("SO001"), "SO001 - " & getValueByKey("CLAE04"))
                            End If

                            grdSOItems_AfterEdit(grdSOItems, New C1.Win.C1FlexGrid.RowColEventArgs(grdSOItems.RowSel, grdSOItems.Cols("PickUpQty").Index))
                        End If

                        'If IsDBNull(drAddItem("BatchBarcode")) = False Then
                        '    'If drAddItem("PickUpQty") > 0 Then
                        '    drAddItem("PickUpQty") = drAddItem("Quantity")
                        '    grdSOItems.Col = grdSOItems.Cols("PickUpQty").Index
                        '    Dim _index As Integer = 0
                        '    For i As Integer = 0 To dsScan.Tables("ItemScanDetails").Rows.Count - 1 Step 1
                        '        If If(dsScan.Tables("ItemScanDetails").Rows(i)("BatchBarcode") Is DBNull.Value, String.Empty, dsScan.Tables("ItemScanDetails").Rows(i)("BatchBarcode")) = drAddItem("BatchBarcode") AndAlso dsScan.Tables("ItemScanDetails").Rows(i)("ArticleCode") = drAddItem("ArticleCode") Then
                        '            _index = i + 1
                        '            Exit For
                        '        End If
                        '    Next
                        '    grdSOItems.Row = _index
                        '    grdSOItems_AfterEdit(grdSOItems, New C1.Win.C1FlexGrid.RowColEventArgs(_index, grdSOItems.Cols("PickUpQty").Index))
                        '    'End If
                        'End If
                    End If
                Else
                    ShowMessage(getValueByKey("SO004"), "SO004 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Article Tax Details does not Found. ", "Tax Details")
                End If
                'If Batchbarcode IsNot Nothing AndAlso Batchbarcode.Select(Function(w) w.EAN = drAddItem("EAN").ToString()).Count() > 0 Then
                '    drAddItem("PickUpQty") = Batchbarcode.Where(Function(w) w.EAN = drAddItem("EAN").ToString()).Sum(Function(w) w.Qty)
                '    'grdScanItems_AfterEdit(grdSOItems, New C1.Win.C1FlexGrid.RowColEventArgs(1, grdSOItems.Cols("PickUpQty").Index))
                '    grdSOItems_AfterEdit(grdSOItems, New C1.Win.C1FlexGrid.RowColEventArgs(grdSOItems.RowSel, grdSOItems.Cols("PickUpQty").Index))
                'End If
                If _dsScan.Tables("ItemScanDetails").Columns.Contains("TaxPer") = True Then
                    If IsIGSTApplicableForOutsideCustomer = True Then

                        For i = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1

                            _dsScan.Tables("ItemScanDetails").Rows(i)("TaxPer") = IIf(dtTaxCalc.Rows.Count > 0, dtTaxCalc.Compute("sum(Value)", ""), 0)  'dtTaxCalc.Rows(0)("Value")
                        Next
                    Else
                        ' _dsScan.Tables("ItemScanDetails").Columns.Add("TaxPer", System.Type.GetType("System.String"))
                        For i = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1
                            _dsScan.Tables("ItemScanDetails").Rows(i)("TaxPer") = IIf(dtTaxCalc.Rows.Count > 0, dtTaxCalc.Compute("sum(Value)", ""), 0)    'dtTaxCalc.Compute("sum(Value)", "")
                        Next
                    End If
                Else
                    _dsScan.Tables("ItemScanDetails").Columns.Add("TaxPer", System.Type.GetType("System.String"))
                    If IsIGSTApplicableForOutsideCustomer = True Then

                        For i = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1

                            _dsScan.Tables("ItemScanDetails").Rows(i)("TaxPer") = IIf(dtTaxCalc.Rows.Count > 0, dtTaxCalc.Compute("sum(Value)", ""), 0)            'dtTaxCalc.Rows(0)("Value")
                        Next
                    Else
                        '    _dsScan.Tables("ItemScanDetails").Columns.Add("TaxPer", System.Type.GetType("System.String"))
                        For i = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1
                            _dsScan.Tables("ItemScanDetails").Rows(i)("TaxPer") = IIf(dtTaxCalc.Rows.Count > 0, dtTaxCalc.Compute("sum(Value)", ""), 0)   ' dtTaxCalc.Compute("sum(Value)", "")
                        Next
                    End If
                End If

                RefreshLoadSOData()
            End If
                CalculateSalesOrderSummory(dsScan)
                IsEditItem = False
                IsNewArticle = False

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

        Return True
    End Function

#End Region
#Region "Search Sales Order"

    ''' <summary>
    ''' Search Old Sales Order
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnSearchSalesOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles BtnSearchSalesOrder.Click
        Try
            'Commented by rama on 1-jul-2009
            'Dim objSearchSO As New frmSearchSO
            'objSearchSO.ShowDialog()
            'vSalesNo = SearchSalesOrderCustomer(clsAdmin.SiteCode)
            Dim tempsaleorderNo As String = ""
            If EditCallFromPopup = True AndAlso SalesORderNumberFromPopup <> "" AndAlso vSalesNo = "" Then
                tempsaleorderNo = SalesORderNumberFromPopup
            Else
                tempsaleorderNo = SearchSalesOrderCustomer(clsAdmin.SiteCode)
            End If

            If Not (tempsaleorderNo = "" Or tempsaleorderNo = String.Empty) Then
                vSalesNo = tempsaleorderNo
                If boolIsReturn = True Then
                    CtrlBtnReturn.Tag = "Cancel Return"
                    CtrlBtnReturn_Click(sender, e)
                End If
                ResetSalesOrder()
                SetSalesOrderInForm(vSalesNo)
                CtrlSalesPerson.CtrlTxtBox.Focus()
                IsStrGenerateApplicable = False
                ItemScan.Visible = True
                CtrlSalesPerson.CtrlTxtBox.Visible = True
                BtnSearchItem.Visible = True
                BtnSOSave.Enabled = True
                BtnSOPrint.Enabled = True
                rbnGrpCMPromotion.Enabled = True
                BtnSOAcceptPayment.Enabled = True
                BtnSOOtherCharges.Enabled = True
                'BtnSOReturn.Enabled = True
                BtnSOStockCheck.Enabled = True
                BtnSOCalculater.Enabled = True
            Else
                Exit Sub
            End If

            GridInvoiceSetting()
            GridItemSetting()
            GridDeliverdSetting()
            IssuingCV = False
            TabSalesOrder.SelectedTab = TabSalesOrder.TabPages("TabPageItemDetails")
            'CtrlSalesPerson.CtrlTxtBox.TabIndex = 8
            fnGridColAutoSize(grdSOItems)
            CtrlSalesPerson.CtrlTxtBox.Focus()
            CtrlSalesInfo.CtrlDtExpDelDate.Enabled = True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub
    Private Sub FillSalesOrder(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            vSalesNo = CtrlSalesInfo.CtrlTxtOrderNo.Text
            If e.KeyCode = Keys.Enter AndAlso vSalesNo <> "" Then
                ResetSalesOrder()
                SetSalesOrderInForm(vSalesNo)
                CtrlSalesPerson.CtrlTxtBox.Focus()

                ItemScan.Visible = True
                CtrlSalesPerson.CtrlTxtBox.Visible = True
                BtnSearchItem.Visible = True

                BtnSOSave.Enabled = True
                BtnSOPrint.Enabled = True
                rbnGrpCMPromotion.Enabled = True
                BtnSOAcceptPayment.Enabled = True
                BtnSOOtherCharges.Enabled = True
                'BtnSOReturn.Enabled = True
                BtnSOStockCheck.Enabled = True
                BtnSOCalculater.Enabled = True
                GridInvoiceSetting()
                GridItemSetting()
                GridDeliverdSetting()
                IssuingCV = False
                TabSalesOrder.SelectedTab = TabSalesOrder.TabPages("TabPageItemDetails")
                CtrlSalesPerson.CtrlTxtBox.Focus()

            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub

    ''' <summary>
    ''' Search Old Sales Order
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtSalesNo_Leave(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles CtrlSalesInfo.CtrlTxtOrderNo.Leave
        Try
            If Not (CtrlSalesInfo.CtrlTxtOrderNo.Text = String.Empty) AndAlso CtrlSalesInfo.CtrlTxtOrderNo.Text.Length > 2 Then
                ResetSalesOrder()

                SetSalesOrderInForm(CtrlSalesInfo.CtrlTxtOrderNo.Text)
                grdSOItems.Select(grdSOItems.Rows.Count - 1, 1, True)
                If dsMain.Tables.Count > 0 Then
                    If vSOStatus = "Closed" Or vSOStatus = "Return" Or vSOStatus = "Cancel" Then
                        ItemScan.Visible = False
                        CtrlSalesPerson.CtrlTxtBox.Visible = False
                        BtnSearchItem.Visible = False

                        BtnSOSave.Enabled = False
                        BtnSOPrint.Enabled = False
                        rbnGrpCMPromotion.Enabled = False
                        BtnSOAcceptPayment.Enabled = False

                        'BtnSOReturn.Enabled = False
                        BtnSOOtherCharges.Enabled = False
                        BtnSOStockCheck.Enabled = False
                        BtnSOCalculater.Enabled = False
                    Else
                        CtrlSalesPerson.CtrlTxtBox.Visible = True
                        BtnSearchItem.Visible = True

                        BtnSOSave.Enabled = True
                        BtnSOPrint.Enabled = True
                        rbnGrpCMPromotion.Enabled = True
                        BtnSOAcceptPayment.Enabled = True

                        'BtnSOReturn.Enabled = True
                        BtnSOOtherCharges.Enabled = True
                        BtnSOStockCheck.Enabled = True
                        BtnSOCalculater.Enabled = True
                    End If
                    'Open   Closed  Return  Cancel
                End If
            Else
                ResetSalesOrder()
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

        CtrlSalesPerson.CtrlTxtBox.TabIndex = 8
        CtrlSalesPerson.CtrlTxtBox.Focus()
    End Sub

    ''' <summary>
    ''' Search Old Sales Order
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtSalesNo_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) 'Handles CtrlSalesInfo.CtrlTxtOrderNo.PreviewKeyDown
        If (e.KeyCode = Keys.Enter) Then
            txtSalesNo_Leave(sender, New System.EventArgs)

            CtrlSalesPerson.CtrlTxtBox.TabIndex = 8
            CtrlSalesPerson.CtrlTxtBox.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Load Old Sales Order in window
    ''' </summary>
    ''' <param name="vSalesNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSalesOrderInForm(ByVal vSalesNo As String) As Boolean
        Try
            CtrlSalesInfo.CtrlTxtRemarks.Enabled = True
            If Not (vSalesNo > "0" Or vSalesNo = String.Empty) Then
                Exit Function
            Else
                Dim findKey(2) As Object
                Dim drSearchHdr As DataRow
                Dim dvAddsInfo As New DataView
                _dsMain = objSO.GetSOTableStruct(vSiteCode, IIf(vSalesNo = String.Empty, 0, vSalesNo), "Cancel")

                '''--- Add By Mahesh Nagar IF Pick Up Qty is >0 then stock Transfer disabled ---
                IsSTRGenerate = False
                Dim dr() = dsMain.Tables("SalesOrderDtl").Select("DeliveredQty>0")
                If dr.Length > 0 Then
                    cmdGenerateSTR.Enabled = False
                Else
                    cmdGenerateSTR.Enabled = True
                End If
                If Not (dsMain Is Nothing) AndAlso dsMain.Tables("SalesOrderHDR").Rows.Count > 0 Then
                    If dsMain.Tables("SalesOrderHDR").Rows(0)("SOStatus").ToString.ToUpper() = "CLOSED" Then
                        ShowMessage(getValueByKey("SO082"), "SO082 - " & getValueByKey("CLAE04"))
                        'this is allowed populate for historical data.
                        rbgrpSaveNprint.Enabled = False
                        C1Sizer2.Enabled = False
                        CtrlSalesPerson.Enabled = False
                        CtrlRbn1.DgrpPayments.Enabled = False
                        grdSOItems.Enabled = False
                    ElseIf dsMain.Tables("SalesOrderHDR").Rows(0)("SOStatus").ToString.ToUpper() = "CANCEL" Then
                        ShowMessage(getValueByKey("SO082"), "SO082 - " & getValueByKey("CLAE04"))
                        'this is allowed populate for historical data.
                        rbgrpSaveNprint.Enabled = False
                        C1Sizer2.Enabled = False
                        CtrlSalesPerson.Enabled = False
                        CtrlRbn1.DgrpPayments.Enabled = False
                        grdSOItems.Enabled = False
                    Else
                        rbgrpSaveNprint.Enabled = True
                        cmdPrint.Enabled = True
                        C1Sizer2.Enabled = True
                        CtrlSalesPerson.Enabled = True
                        CtrlRbn1.DgrpPayments.Enabled = True
                        grdSOItems.Enabled = True
                        'End this is allowed populate for historical data.
                        'Exit Function
                    End If
                End If

                If Not (dsMain Is Nothing) AndAlso dsMain.Tables("SalesOrderHDR").Rows.Count > 0 Then
                    CtrlSalesInfo.CtrlTxtOrderNo.Text = vSalesNo
                    BtnSearchItem.Enabled = True
                    vfinancialYear = dsMain.Tables("SalesOrderHDR").Rows(0)("FinYear")
                    findKey(0) = vSiteCode
                    findKey(1) = vfinancialYear
                    findKey(2) = vSalesNo
                    drSearchHdr = dsMain.Tables("SalesOrderHDR").Rows.Find(findKey)
                    vSalesOrderCreationDate = dsMain.Tables("SalesOrderHDR").Rows(0)("CREATEDON")
                    vSalesOrderExpectedDeliveryDate = IIf(drSearchHdr("ActualDeliveryDate") Is DBNull.Value, "", drSearchHdr("ActualDeliveryDate"))
                    'vSalesOrderExpectedDeliveryDate = IIf(CtrlSalesInfo.CtrlDtExpDelDate.Value Is DBNull.Value, DateTime.Now, CtrlSalesInfo.CtrlDtExpDelDate.Value)
                    If Not (drSearchHdr Is Nothing) Then

                        CtrlSalesInfo.CtrldtOrderDt.Value = Format(IIf(drSearchHdr("CREATEDON") Is DBNull.Value, "", drSearchHdr("CREATEDON")), vDateFormat)
                        CtrlSalesInfo.CtrlDtExpDelDate.Visible = True

                        consDeliveryDate = IIf(drSearchHdr("ActualDeliveryDate") Is DBNull.Value, "", drSearchHdr("ActualDeliveryDate"))
                        vBalanceAmount = IIf(drSearchHdr("BalanceAmount") Is DBNull.Value, "", drSearchHdr("BalanceAmount"))
                        vAdvanceAmount = IIf(drSearchHdr("AdvanceAmt") Is DBNull.Value, "", drSearchHdr("AdvanceAmt"))

                        CtrlSalesInfo.CtrlDtExpDelDate.DisplayFormat.CustomFormat = DateFormat.ShortDate
                        CtrlSalesInfo.CtrlDtExpDelDate.EditFormat.CustomFormat = DateFormat.ShortDate
                        CtrlSalesInfo.CtrlDtExpDelDate.Value = consDeliveryDate

                        vSOStatus = drSearchHdr("SOStatus").ToString
                        CtrlSalesInfo.CtrlTxtRemarks.Text = IIf(drSearchHdr("Remarks") Is DBNull.Value, "", drSearchHdr("Remarks"))
                        CtrlSalesInfo.CtrlTxtCustOrdRef.Text = IIf(drSearchHdr("CustomerOrderRef") Is DBNull.Value, "", drSearchHdr("CustomerOrderRef"))
                        CtrlSalesInfo.CtrlTxtInvoice.Text = IIf(drSearchHdr("InvoiceCustName") Is DBNull.Value, "", drSearchHdr("InvoiceCustName"))

                        vSalesPerson = IIf(drSearchHdr("SalesExecutiveCode") Is DBNull.Value, "", drSearchHdr("SalesExecutiveCode"))
                        vCustomerNo = IIf(drSearchHdr("CustomerNo") Is DBNull.Value, "", drSearchHdr("CustomerNo"))
                        CtrlCustDtls1.lblCustTypeValue.Text = drSearchHdr("CustomerType")
                        If vSalesPerson <> "" Then
                            CtrlSalesPerson.CtrlSalesPersons.SelectedValue = vSalesPerson
                        End If


                        'If CtrlCustDtls1.CustmType = "CLP" Then
                        '    CtrlCustDtls1.rbCLPMember.Checked = True
                        'Else
                        '    CtrlCustDtls1.rbOtherCust.Checked = True
                        'End If

                        vAddressType = IIf(drSearchHdr("DeliveryAtCustAddressType") Is DBNull.Value, "", drSearchHdr("DeliveryAtCustAddressType"))

                        If vAddressType = "" Then
                            vCAddress = IIf(drSearchHdr("OtherDeliveryAdd") Is DBNull.Value, "", drSearchHdr("OtherDeliveryAdd"))
                        End If
                    End If

                    'Dim dt As DataTable
                    'dt = objCM.GetSalesPerson(vSiteCode, vSalesPerson)
                    'If Not (dt Is Nothing) AndAlso dt.Rows.Count > 0 Then
                    '    CtrlSalesPerson.CtrlSalesPersons.SelectedValue = dt.Rows(0).Item("SalesPersonName").ToString
                    'End If

                    dsScanTemp = objSO.SetSalesOrderInSOCancel(vSiteCode, IIf(vSalesNo = String.Empty, 0, vSalesNo))
                    _dsScan.Tables("ItemScanDetails").Clear()
                    vEANList = ""

                    For Each drScanTemp As DataRow In dsScanTemp.Tables("ItemScanDetails").Rows
                        Dim drScan As DataRow = _dsScan.Tables("ItemScanDetails").NewRow
                        drScan("EAN") = drScanTemp("EAN")
                        drScan("Discription") = drScanTemp("Discription")
                        drScan("SellingPrice") = FormatNumber(drScanTemp("SellingPrice"), 2)
                        drScan("Quantity") = drScanTemp("Quantity")
                        drScan("BatchBarcode") = drScanTemp("BatchBarcode")
                        drScan("DeliverySiteCode") = drScanTemp("DeliverySiteCode")
                        drScan("PickupQty") = 0
                        drScan("DeliveredQty") = IIf(drScanTemp("DeliveredQty") Is DBNull.Value, 0, drScanTemp("DeliveredQty"))
                        drScan("Discount") = Math.Round(IIf(IsDBNull(drScanTemp("Discount")), 0, drScanTemp("Discount")), 2)
                        drScan("NetAmount") = FormatNumber(drScanTemp("NetAmount"), 2)
                        drScan("ExpDelDate") = drScanTemp("ExpDelDate")
                        drScan("BillLineNo") = drScanTemp("BillLineNo")
                        '---Apply Row Index by Mahesh because need to mapping for Combo ....
                        drScan("rowIndex") = drScanTemp("BillLineNo")
                        Dim Stock As Double = objCM.GetStocks(vSiteCode, drScanTemp("EAN"), drScanTemp("Articlecode"), True, False, IIf(IsDBNull(drScanTemp.Item("BatchBarcode")) = False, drScanTemp.Item("BatchBarcode"), String.Empty))
                        drScan("Stock") = Stock
                        drScan("IsCLP") = drScanTemp("IsCLPApplicable")
                        drScan("ClpPoints") = IIf(drScanTemp("ClpPoints") Is DBNull.Value, 0, drScanTemp("ClpPoints"))
                        drScan("ClpDiscount") = IIf(drScanTemp("ClpDiscount") Is DBNull.Value, 0, drScanTemp("ClpDiscount"))

                        'this is comment because not found any use of rowindex
                        'drScan("RowIndex") = drScanTemp("ArticleCode")

                        drScan("ArticleCode") = drScanTemp("ArticleCode")
                        drScan("UOM") = drScanTemp("UnitOfMeasure")
                        drScan("GrossAmt") = drScanTemp("GrossAmt") '+ drScanTemp("ExclTaxAmt")
                        If drScanTemp("ReservedQty") Is DBNull.Value Then
                            drScanTemp("ReservedQty") = False
                        End If
                        drScan("ReservedQty") = IIf(drScanTemp("ReservedQty") > 0, True, False)
                        drScan("Reserved") = IIf(drScanTemp("ReservedQty") > 0, True, False)

                        TotalSalesQty = IIf(drScanTemp("PickUpQty") Is DBNull.Value, 0, drScanTemp("PickUpQty")) + IIf(drScanTemp("DeliveredQty") Is DBNull.Value, 0, drScanTemp("DeliveredQty"))
                        NetArticleRate = drScanTemp("NetAmount") / drScanTemp("Quantity")
                        drScan("MinPayAmt") = Math.Round(((drScanTemp("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate), 3)
                        drScan("FreezeSB") = drScanTemp("FreezeSB")
                        drScan("FreezeOB") = drScanTemp("FreezeOB")
                        drScan("ExclTaxAmt") = drScanTemp("ExclTaxAmt")
                        drScan("TotalTaxAmt") = IIf(drScanTemp("TotalTaxAmount") Is DBNull.Value, 0, drScanTemp("TotalTaxAmount"))
                        drScan("IncTaxAmt") = 0
                        drScan("PromotionId") = drScanTemp("OfferNo")
                        drScan("LineDiscount") = drScanTemp("LineDiscount")
                        drScan("TotalDiscPercentage") = drScanTemp("DiscountPercentage")
                        drScan("FirstLevel") = 0
                        drScan("TopLevel") = 0
                        drScan("CostAmount") = drScanTemp("CostAmount")
                        drScan("IsStatus") = IIf(drScanTemp("Status") = False, "Deleted", "Inserted")
                        drScan("SalesStaffID") = drScanTemp("SalesStaffID")
                        vEANList = vEANList & "'" & drScanTemp("EAN") & "', "

                        _dsScan.Tables("ItemScanDetails").Rows.Add(drScan)


                        '------------------------------------------------------------------------------------------------------
                        '   If dtTaxCalc Is Nothing Then
                        If IsCSTApplicable Then
                            dtTaxCalc = objCM.getTax(vSiteCode, String.Empty, "SO201", drScan("PickupQty"), drScan("EAN"), clsDefaultConfiguration.CSTTaxCode, True)
                        Else
                            dtTaxCalc = objCM.getTax(vSiteCode, drScan("ArticleCode"), "SO201", drScan("PickupQty"), drScan("EAN"))
                        End If

                        'code added by irfan on 23/10/2017 for IGST CAKEKRAFT
                        Dim objso As New clsSaleOrderCommon
                        Dim _strCustNo = vCustomerNo ' CtrlCustDtls1.lblCustNoValue.Text 


                        ''Dim state As DataTable = objCM.getSiteStateCode(vSiteCode)
                        IsIGSTApplicableForOutsideCustomer = objComn.checkIGSTAplicableForOutSideStateCustomer(clsAdmin.SiteCode, _strCustNo)
                        Dim IGSTtaxCode As String = objComn.ReturnIGSTTaxID(dtTaxCalc)

                        If Not dtTaxCalc Is Nothing AndAlso dtTaxCalc.Rows.Count > 0 Then
                            If IsIGSTApplicableForOutsideCustomer = True Then
                                If IGSTtaxCode <> "" Then
                                    'code by irfan on 12/13/2017
                                    'Dim index As Integer
                                    'For index = 0 To dtTaxCalc.Rows.Count - 1
                                    '    index = 0
                                    '    If dtTaxCalc.Rows(0)("TAXCODE").ToString <> IGSTtaxCode Then
                                    '        dtTaxCalc.Rows.RemoveAt(index)
                                    '        dtTaxCalc.AcceptChanges()
                                    '    Else
                                    '        Exit For
                                    '    End If
                                    'Next
                                    Dim dv As New DataView(dtTaxCalc, "TAXCODE='" & IGSTtaxCode & "'", "", DataViewRowState.CurrentRows)
                                    dtTaxCalc = dv.ToTable
                                    'commented by irfan 
                                    'Else
                                    '    Dim index As Integer
                                    '    For index = 0 To dtTaxCalc.Rows.Count - 1
                                    '        If dtTaxCalc.Rows.Count > 0 Then
                                    '            index = 0
                                    '            dtTaxCalc.Rows.RemoveAt(index)
                                    '            dtTaxCalc.AcceptChanges()
                                    '        Else
                                    '            Exit For
                                    '        End If
                                    '    Next
                                End If
                            Else
                                If dtTaxCalc.Rows.Count > 0 Then
                                    Dim index As Integer
                                    For index = 0 To dtTaxCalc.Rows.Count - 1
                                        If dtTaxCalc.Rows(index)("TAXCODE").ToString.Trim = IGSTtaxCode Then
                                            dtTaxCalc.Rows.RemoveAt(index)
                                            dtTaxCalc.AcceptChanges()
                                            Exit For
                                        End If
                                    Next
                                End If
                            End If
                        End If

                        '   End If
                        '------------------------------------------------------------------------------------------------------

                    Next
                    _dsScan.AcceptChanges()

                    '===========================================================================================================
                    If _dsScan.Tables("ItemScanDetails").Columns.Contains("TaxPer") = True Then
                        If IsIGSTApplicableForOutsideCustomer = True Then

                            For i = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1

                                _dsScan.Tables("ItemScanDetails").Rows(i)("TaxPer") = IIf(dtTaxCalc.Rows.Count > 0, dtTaxCalc.Compute("sum(Value)", ""), 0)  'dtTaxCalc.Rows(0)("Value")
                            Next
                        Else
                            ' _dsScan.Tables("ItemScanDetails").Columns.Add("TaxPer", System.Type.GetType("System.String"))
                            For i = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1
                                _dsScan.Tables("ItemScanDetails").Rows(i)("TaxPer") = IIf(dtTaxCalc.Rows.Count > 0, dtTaxCalc.Compute("sum(Value)", ""), 0)    'dtTaxCalc.Compute("sum(Value)", "")
                            Next
                        End If
                    Else
                        _dsScan.Tables("ItemScanDetails").Columns.Add("TaxPer", System.Type.GetType("System.String"))
                        If IsIGSTApplicableForOutsideCustomer = True Then

                            For i = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1

                                _dsScan.Tables("ItemScanDetails").Rows(i)("TaxPer") = IIf(dtTaxCalc.Rows.Count > 0, dtTaxCalc.Compute("sum(Value)", ""), 0)            'dtTaxCalc.Rows(0)("Value")
                            Next
                        Else
                            '    _dsScan.Tables("ItemScanDetails").Columns.Add("TaxPer", System.Type.GetType("System.String"))
                            For i = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1
                                _dsScan.Tables("ItemScanDetails").Rows(i)("TaxPer") = IIf(dtTaxCalc.Rows.Count > 0, dtTaxCalc.Compute("sum(Value)", ""), 0)   ' dtTaxCalc.Compute("sum(Value)", "")
                            Next
                        End If
                    End If

                    '===========================================================================================================

                    If _dsScan.Tables("ItemScanDetails").Rows.Count > 0 Then
                        vRowIndex = _dsScan.Tables("ItemScanDetails").Compute("MAX(rowIndex)", "") + 1
                    End If

                    dsScanReturn.Tables(0).Rows.Clear()

                    For Each drReturn As DataRow In dsScan.Tables(0).Select("DeliveredQty>0")
                        Dim drAddReturn As DataRow = dsScanReturn.Tables(0).NewRow()
                        Dim col As DataColumn
                        'For Each col In dsScan.Tables(0).Columns
                        '    drAddReturn(col.ColumnName) = drReturn(col.ColumnName)
                        'Next
                        For ColumnNo As Integer = 1 To drReturn.ItemArray.Count - 1
                            Try
                                drAddReturn(ColumnNo) = drReturn(ColumnNo)
                            Catch ex As Exception
                            End Try
                        Next
                        dsScanReturn.Tables(0).Rows.Add(drAddReturn)
                        AddButtonControlInGrid(dsScanReturn.Tables(0).Rows.Count, "SalesReturn")

                    Next

                    If (dsScanReturn.Tables(0).Rows.Count > 0) Then
                        CtrlBtnReturn.Enabled = True
                    Else
                        CtrlBtnReturn.Enabled = False
                    End If

                    'dsScanReturn.Merge(dsScan)
                    RefreshLoadSOData()
                    dtOtherCharges = dsMain.Tables("SalesOrderOthercharges").Copy()
                    CalculateSalesOrderSummory(_dsScan)
                    GridDeliverdSetting()
                    dsInvoice = objSO.SetInvoiceInSOCancel(vSiteCode, IIf(vSalesNo = String.Empty, 0, vSalesNo))
                    RefreshLoadInvcData()

                    If clsDefaultConfiguration.IsCstTaxRequired AndAlso dsMain.Tables("SalesOrderhdr").Rows.Count > 0 AndAlso CBool(If(dsMain.Tables("SalesOrderhdr").Rows(0)("IsCSTApplied") Is DBNull.Value, False, dsMain.Tables("SalesOrderhdr").Rows(0)("IsCSTApplied"))) = True Then
                        MessageBox.Show(getValueByKey("CST002"), getValueByKey("CLAE04"))
                        IsCSTApplicable = True
                        clsDefaultConfiguration.CSTTaxCode = dsMain.Tables("SalesOrderhdr").Rows(0)("CSTTaxCode")
                    Else
                        IsCSTApplicable = False
                        clsDefaultConfiguration.CSTTaxCode = ""
                    End If

                    CtrlCustDtls1.lblCustNoValue.Text = vCustomerNo
                    dtCustmInfo = objCustm.GetCustomerInformation(CtrlCustDtls1.lblCustTypeValue.Text, vSiteCode, clsAdmin.CLPProgram, vCustomerNo)

                    'CtrlCustDtls1.rbCLPMember.Enabled = False
                    'CtrlCustDtls1.rbOtherCust.Enabled = False

                    CtrlCustDtls1.pDisplayDtls(dtCustmInfo)
                    CtrlCustDtls1.cboAddrType.SelectedValue = vAddressType
                    If dsMain.Tables("SalesOrderdtl").Rows.Count > 0 Then
                        DeliverySiteCode = dsMain.Tables("SalesOrderdtl").Rows(0)("DeliverySiteCode").ToString()
                    End If

                    IsNextInvoiceNo = False
                    IsApplyPromotion = True
                End If

                Dim Dstemp = objSO.GetSOBulkComboTableStruct(vSiteCode, IIf(vSalesNo = String.Empty, 0, vSalesNo))
                DtSoBulkComboHdr = Dstemp.Tables("SoBulkComboHdr")
                DtSoBulkComboDtl = Dstemp.Tables("SoBulkComboDtl")

            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Function

#End Region
#Region "Refresh Data Load "

    ''' <summary>
    ''' Refresh Article Scan Data in DataGrid
    ''' </summary>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function RefreshLoadSOData() As Boolean
        Dim rowStyle As C1.Win.C1FlexGrid.CellStyle
        rowStyle = grdSOItems.Styles.Add("RowBackColorItemDelete")
        rowStyle.BackColor = Color.DarkGray

        Dim rowStylefullQty As C1.Win.C1FlexGrid.CellStyle
        rowStylefullQty = grdSOItems.Styles.Add("RowBackColorfullQty")
        rowStylefullQty.BackColor = Color.LightSteelBlue

        Try

            '_dvDisplayItem = New DataView(dsScan.Tables("ItemScanDetails"), "", "RowIndex Desc", DataViewRowState.CurrentRows)
            'grdSOItems.DataSource = dvDisplayItem.ToTable(False, "DEL", "EAN", "Discription", "SellingPrice", "Quantity", "PickUpQty", "DeliveredQty", "Discount", "NetAmount", "ExpDelDate", "Stock", "IsCLP", "ReservedQty", "ArticleCode", "IsStatus")
            'to get article code in grid.
            Dim objSiteInfo As New clsSiteInfo
            Dim dt1 = objSiteInfo.GetAllSitesForDelivery()
            Dim hash As New System.Collections.Hashtable
            For Each row In dt1.Rows
                hash.Add(row("SiteCode"), row("SiteShortName"))
            Next

            Dim dtSource As DataTable = dsScan.Tables("ItemScanDetails") 'dvDisplayItem.ToTable(False, "DEL", "ArticleCode", "Discription", "SellingPrice", "Quantity", "PickUpQty", "DeliveredQty", "Discount", "NetAmount", "ExpDelDate", "Stock", "IsCLP", "ReservedQty", "EAN", "IsStatus")

            If Not dtSource.Columns.Contains("Blankclm") Then
                AddBlankColumn(dtSource)
            End If

            grdSOItems.DataSource = dtSource
            grdSOItems.Cols("DeliverySiteCode").DataMap = hash

            grdSOItems.Cols("DeliverySiteCode").AllowEditing = False
            For Each drGridRow As C1.Win.C1FlexGrid.Row In grdSOItems.Rows
                If Not (drGridRow.Index = 0) Then

                    If Not (vEANList.IndexOf(drGridRow.Item("EAN")) = -1) Then
                        If drGridRow.Item("IsStatus") = "Deleted" Then
                            grdSOItems.Rows(drGridRow.Index).Style = grdSOItems.Styles("RowBackColorItemDelete")
                            grdSOItems.Rows(drGridRow.Index).AllowEditing = False
                        End If
                        'If drGridRow.Item("Quantity") = drGridRow.Item("DeliveredQty") Then
                        '    grdSOItems.Rows(drGridRow.Index).Style = grdSOItems.Styles("RowBackColorfullQty")
                        '    grdSOItems.Rows(drGridRow.Index).AllowEditing = False
                        'End If
                    End If

                    AddButtonControlInGrid(drGridRow.Index, "Sales")
                End If
            Next

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    ''' <summary>
    ''' Refresh Invoice Details Data in DataGrid
    ''' </summary>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function RefreshLoadInvcData() As Boolean
        Try
            Dim dvDisplayInvc As New DataView(dsInvoice.Tables("InvoiceDetails"))
            Dim dtSource As DataTable = dvDisplayInvc.ToTable(False, "SalesNo", "InvoiceNo", "DocumentType", "TerminalID", "TenderType", "InvoiceAmt", "UserName", "InvoiceDate")
            AddBlankColumn(dtSource)
            grdSOInvoice.DataSource = dtSource
            dvDisplayInvc.Dispose()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Function



    ''' <summary>
    ''' Calculate Sales Order Summary and Show in Screen
    ''' </summary>
    ''' <param name="dsScanTemp"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CalculateSalesOrderSummory(ByVal dsScanTemp As DataSet) As Boolean
        Try
            If Not (dsScan.Tables("ItemScanDetails") Is Nothing) AndAlso dsScan.Tables("ItemScanDetails").Rows.Count > 0 Then

                lbltotalitem.Text = dsScan.Tables("ItemScanDetails").Rows.Count & " Items"
                lblOrderQty.Text = CDbl(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(Quantity)", "IsStatus In " & vFilterValue & "") Is DBNull.Value, 0, _
                                           dsScan.Tables("ItemScanDetails").Compute("SUM(Quantity)", "IsStatus In " & vFilterValue & "")))

                lblPickupQty.Text = CDbl(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(PickUpQty)", "IsStatus In " & vFilterValue & "") Is DBNull.Value, 0, _
                                            dsScan.Tables("ItemScanDetails").Compute("SUM(PickUpQty)", "IsStatus In " & vFilterValue & "")))

                lbldeliveredqty.Text = CDbl(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(DeliveredQty)", "") Is DBNull.Value, 0, _
                                            dsScan.Tables("ItemScanDetails").Compute("SUM(DeliveredQty)", "")))
                If Not dsScan.Tables("ItemScanDetails").Compute("SUM(GrossAmt)", "IsStatus In " & vFilterValue & "") Is DBNull.Value Then
                    vGrossAmount = CDbl(dsScan.Tables("ItemScanDetails").Compute("SUM(GrossAmt)", "IsStatus In " & vFilterValue & ""))
                Else
                    vGrossAmount = 0
                End If


                lblgrossamt1.Text = FormatNumber(vGrossAmount, 2)
                CtrlCashSummary1.lbltxt1 = FormatNumber(vGrossAmount, 2)

                vDiscAmount = CDbl(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(LineDiscount)", "IsStatus In " & vFilterValue & "") Is DBNull.Value, 0, _
                                                 dsScan.Tables("ItemScanDetails").Compute("SUM(LineDiscount)", "IsStatus In " & vFilterValue & "")))
                CtrlCashSummary1.lbltxt2 = FormatNumber(vDiscAmount, 2)
                vOtherChargesOld = 0 '0000196
                vOtherChargesNew = 0 '0000196

                'Start==========Get OtherCharges from database===============
                'If Not (dsMain.Tables("SalesOrderOtherCharges") Is Nothing) AndAlso dsMain.Tables("SalesOrderOtherCharges").Rows.Count > 0 Then
                '    Dim vChargeAmountOld As String = IIf((dsMain.Tables("SalesOrderOtherCharges").Compute("Sum(ChargeAmount)", "") Is DBNull.Value), 0, dsMain.Tables("SalesOrderOtherCharges").Compute("Sum(ChargeAmount)", ""))
                '    Dim vTaxAmountOld As String = IIf((dsMain.Tables("SalesOrderOtherCharges").Compute("Sum(TaxAmt)", "") Is DBNull.Value), 0, dsMain.Tables("SalesOrderOtherCharges").Compute("Sum(TaxAmt)", ""))

                '    vOtherChargesOld = CDbl(CDbl(vChargeAmountOld) + CDbl(vTaxAmountOld))
                'End If

                If Not (dtOtherCharges Is Nothing) AndAlso dtOtherCharges.Rows.Count > 0 Then
                    Dim vChargeAmountOld As String = IIf((dtOtherCharges.Compute("Sum(ChargeAmount)", "") Is DBNull.Value), 0, dtOtherCharges.Compute("Sum(ChargeAmount)", ""))
                    Dim vTaxAmountOld As String = IIf((dtOtherCharges.Compute("Sum(TaxAmt)", "") Is DBNull.Value), 0, dtOtherCharges.Compute("Sum(TaxAmt)", ""))

                    vOtherChargesOld = CDbl(CDbl(vChargeAmountOld) + CDbl(vTaxAmountOld))
                End If

                vTotalOtherCharges = CDbl(vOtherChargesOld + vOtherChargesNew)
                CtrlCashSummary1.lbltxt3 = FormatNumber(vTotalOtherCharges, 2)
                'End============Get OtherCharges from Application============
                Dim AdvanceAmt As Double = 0
                If vTotalOtherCharges > 0.0 Then
                    CtrlCashSummary1.lbltxt1 = FormatNumber((vGrossAmount + vTotalOtherCharges), 2)
                    If Val(lblOrderQty.Text) = Val(lblPickupQty.Text) + Val(lbldeliveredqty.Text) Then
                        AdvanceAmt = vTotalOtherCharges
                    Else
                        AdvanceAmt = (vTotalOtherCharges * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100))
                    End If
                End If
                If Not dsScan.Tables("ItemScanDetails").Compute("SUM(NetAmount)", "IsStatus In " & vFilterValue & "") Is DBNull.Value Then
                    CtrlCashSummary1.lbltxt4 = Format((CDbl(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(NetAmount)", "IsStatus In " & vFilterValue & "") Is DBNull.Value, 0, _
                                                  dsScan.Tables("ItemScanDetails").Compute("SUM(NetAmount)", "IsStatus In " & vFilterValue & "") + vTotalOtherCharges))), "0.00")
                Else
                    CtrlCashSummary1.lbltxt4 = 0
                End If




                'Start==========Calculate Min Advance Paid===================
                If Not dsScan.Tables("ItemScanDetails").Compute("SUM(MinPayAmt)", "IsStatus<> 'Deleted'") Is DBNull.Value Then
                    vCurrMinAdvanceAmt = CDbl(dsScan.Tables("ItemScanDetails").Compute("SUM(MinPayAmt)", "IsStatus<> 'Deleted'"))
                Else
                    vCurrMinAdvanceAmt = 0
                End If

                vCurrMinAdvanceAmt = MyRound(vCurrMinAdvanceAmt, clsDefaultConfiguration.BillRoundOffAt)
                If AdvanceAmt > 0 Then
                    vCurrMinAdvanceAmt = CDbl(vCurrMinAdvanceAmt) + AdvanceAmt
                End If
                If vCurrMinAdvanceAmt > vAdvanceAmount Then
                    lblminadvancepaid.Text = FormatNumber(vCurrMinAdvanceAmt - vAdvanceAmount, 2)
                Else
                    lblminadvancepaid.Text = strZero
                End If

                'End============Calculate Min Advance Paid===================
                If clsDefaultConfiguration.RoundOffRequired = True Then
                    CtrlCashSummary1.lbltxt4 = MyRound(CtrlCashSummary1.lbltxt4, clsDefaultConfiguration.BillRoundOffAt)
                    CtrlCashSummary1.lbltxt5 = MyRound(CtrlCashSummary1.lbltxt5, clsDefaultConfiguration.BillRoundOffAt)
                End If

                CtrlCashSummary1.lbltxt6 = FormatNumber((CDbl(CtrlCashSummary1.lbltxt4) - vAdvanceAmount), 2)

                If dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                    If Not dsPayment.Tables("MSTRecieptType").Compute("Sum(Amount)", "") Is DBNull.Value Then
                        vReceivedAmt = FormatNumber(CDbl(dsPayment.Tables("MSTRecieptType").Compute("Sum(Amount)", "")), 2)
                    End If

                    If CDbl(CtrlCashSummary1.lbltxt6) < 0 Then
                        lblReceivedAmt.Text = FormatNumber(vReceivedAmt, 2)
                    Else
                        lblReceivedAmt.Text = FormatNumber(vReceivedAmt, 2)
                    End If
                Else
                    lblReceivedAmt.Text = strZero
                End If
                If clsDefaultConfiguration.RoundOffRequired = True Then
                    CtrlCashSummary1.lbltxt4 = MyRound(CtrlCashSummary1.lbltxt4, clsDefaultConfiguration.BillRoundOffAt)
                    CtrlCashSummary1.lbltxt5 = MyRound(CtrlCashSummary1.lbltxt5, clsDefaultConfiguration.BillRoundOffAt)
                    'CtrlCashSummary1.lbltxt6 = MyRound(CtrlCashSummary1.lbltxt6, clsDefaultConfiguration.BillRoundOffAt)
                End If
                Dim crdsale As Double = 0.0
                Dim crdsaleadjustamount As Double = 0
                Dim salesordernumber As String = ""
                Dim NetCrdSale As Double = 0
                Dim AmtPaid As Double = 0
                If dsMain.Tables("SalesInvoice").Rows.Count > 0 AndAlso Not dsMain.Tables("SalesInvoice") Is Nothing Then
                    If dsMain.Tables("SalesInvoice").Select("TenderHeadCode='Credit Sales'").Length > 0 Then
                        Dim dt As DataTable = dsMain.Tables("SalesInvoice").Select("TenderHeadCode='Credit Sales'").CopyToDataTable
                        If dt.Rows.Count > 0 AndAlso Not dt Is Nothing Then
                            For Each x In dt.Rows
                                crdsale += x("AmountTendered")
                                salesordernumber = x("DocumentNumber")
                            Next
                        End If
                    Else
                        'SwipeBckDefaultPosition(CtrlCashSummary1)
                    End If
                End If
                AmtPaid = vAdvanceAmount
                If crdsale > 0 Then
                    'vAdvanceAmount = vAdvanceAmount - crdsale
                    AmtPaid = AmtPaid - crdsale '- crdsaleadjustamount
                End If
                Dim dtCreditSaleData As New DataTable
                Dim objclsReturn As New clsCashMemoReturn
                dtCreditSaleData = objclsReturn.getCreditSaleBillData("'" + salesordernumber + "'")
                If dtCreditSaleData.Rows.Count > 0 AndAlso Not dtCreditSaleData Is Nothing Then
                    For Each y In dtCreditSaleData.Rows
                        crdsaleadjustamount += y("CreditSaleAdjustment")
                    Next
                End If
                If crdsaleadjustamount > 0 Or crdsale > 0 Then
                    NetCrdSale = crdsale - crdsaleadjustamount
                    'vAdvanceAmount = vAdvanceAmount + crdsaleadjustamount
                    AmtPaid = AmtPaid + crdsaleadjustamount
                End If
                If NetCrdSale = 0 Then
                    CtrlCashSummary1.lblVisible10 = False
                    CtrlCashSummary1.lbltxtVisible10 = False
                Else
                    ' SwipePosition(CtrlCashSummary1)
                End If
                CtrlCashSummary1.lbltxt7 = FormatNumber(NetCrdSale, 2)
                CtrlCashSummary1.lbltxt5 = FormatNumber(AmtPaid, 2)
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Function

    ''' <summary>
    ''' Remove Apply Promotion on Current Sales Order
    ''' </summary>
    ''' <param name="dsScanTemp"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RemoveApplyPromotion(ByRef dsScanTemp As DataSet) As Boolean
        For Each drRem As DataRow In dsScanTemp.Tables(0).Select("IsStatus <> 'Deleted'")
            drRem("Discount") = 0
            drRem("MinPayAmt") = 0
            drRem("PromotionId") = 0
            drRem("LineDiscount") = 0
            drRem("TotalDiscPercentage") = 0
            drRem("FirstLevel") = 0
            drRem("TopLevel") = 0
            Dim obj As New clsSaleOrderCommon
            obj.RecalculateLine(drRem, CtrlSalesInfo.CtrlTxtOrderNo.Text, dsMain)
        Next

        IsApplyPromotion = False
    End Function

    ''' <summary>
    ''' Clears All Resource for new sales order
    ''' </summary>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function ResetSalesOrder() As Boolean

        If Not (dsScan Is Nothing) AndAlso dsScan.Tables("ItemScanDetails").Rows.Count > 0 Then
            dsScan.Tables(0).Rows.Clear()

        End If
        If Not (dsScanReturn Is Nothing) AndAlso dsScanReturn.Tables("ItemScanDetails").Rows.Count > 0 Then
            dsScanReturn.Clear()
        End If

        RefreshLoadSOData()

        If dsInvoice.Tables.Count > 0 AndAlso dsInvoice.Tables("InvoiceDetails").Rows.Count > 0 Then
            dsInvoice.Clear()
        End If
        RefreshLoadInvcData()

        If Not (dtOtherCharges Is Nothing) AndAlso dtOtherCharges.Rows.Count > 0 Then
            dtOtherCharges.Rows.Clear()
        End If

        If Not (dsMain Is Nothing) Then
            dsMain.Clear()
        End If
        If Not (dsPayment Is Nothing) Then
            dsPayment.Clear()
        End If
        If Not (CtrlCustDtls1.dtCustmInfo Is Nothing) Then
            CtrlCustDtls1.dtCustmInfo.Clear()
        End If

        If dsPayment.Tables.Contains("CheckDtls") Then
            dsPayment.Tables.Remove("CheckDtls")
        End If

        If dsMain.Tables.Contains("CheckDtls") Then
            dsMain.Tables.Remove("CheckDtls")
        End If

        ' BtnSearchItem.Enabled = False
        CtrlSalesInfo.CtrldtOrderDt.Value = ""
        CtrlSalesInfo.CtrlDtExpDelDate.Visible = True
        CtrlSalesInfo.CtrlDtExpDelDate.Value = DBNull.Value
        CtrlSalesInfo.CtrlTxtRemarks.Text = ""
        CtrlSalesInfo.CtrlTxtCustOrdRef.Text = ""
        CtrlSalesPerson.CtrlSalesPersons.SelectedIndex = -1

        CtrlProductImage.ClearImage()
        CtrlCustDtls1.pClear()
        CtrlSalesInfo.CtrlTxtInvoice.Text = ""

        lbltotalitem.Text = 0
        lblOrderQty.Text = 0
        lblPickupQty.Text = 0
        lbldeliveredqty.Text = 0

        CtrlCashSummary1.lbltxt1 = strZero
        lblgrossamt1.Text = strZero
        CtrlCashSummary1.lbltxt2 = strZero
        CtrlCashSummary1.lbltxt4 = strZero

        CtrlCashSummary1.lbltxt3 = strZero
        CtrlCashSummary1.lbltxt5 = strZero
        lblminadvancepaid.Text = strZero
        CtrlCashSummary1.lbltxt6 = strZero
        lblReceivedAmt.Text = strZero
        CtrlCashSummary1.lbltxt7 = strZero

        vReceivedAmt = 0.0
        vBalanceAmount = 0.0
        vAdvanceAmount = 0.0
        vDiscAmount = 0.0
        vCurrMinAdvanceAmt = 0.0
        vAmendedNo = 0

        vSOStatus = ""
        vCardType = ""
        vDocType = vDocTypeCreation

        txtReturnReason.Text = ""
        LbReturnReason.Visible = False
        txtReturnReason.Visible = False

        IsAllowedSalesReturn = False
        IsOutboundCreated = False
        Batchbarcode = New List(Of SpectrumCommon.BtachbarcodeInfo)
        ReturnBatchbarcode = New List(Of SpectrumCommon.BtachbarcodeInfo)
    End Function

#End Region
#Region "Save/Update Sales Order "
    ''' <summary>
    ''' Uodate sales order
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnSaveSalesOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal flg As Boolean = False) 'Handles BtnSOSave.Click
        Try
            isLeaved = True
            grdSOItems.FinishEditing()

            If dsScan.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            If (String.IsNullOrEmpty(vSalesNo) Or CtrlSalesInfo.CtrlTxtOrderNo.Text.Trim <> vSalesNo Or grdSOItems.Rows.Count <= 1) Then
                ShowMessage(getValueByKey("frmnquotationupdate.Warning"), getValueByKey("CLAE04"))
                Exit Sub
            End If

            If CtrlCustDtls1.dtCustmInfo.Rows.Count > 2 Then
                drHomeAdds = CtrlCustDtls1.dtCustmInfo.Select("AddressType='1'")(0)
            ElseIf CtrlCustDtls1.dtCustmInfo.Rows.Count > 0 Then
                drHomeAdds = CtrlCustDtls1.dtCustmInfo.Rows(0)
            Else
                ShowMessage(getValueByKey("BL001"), "BL001 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If

            If Not IsDBNull(CtrlCustDtls1.cboAddrType.SelectedValue) AndAlso (CtrlCustDtls1.cboAddrType.SelectedValue.ToString() <> String.Empty) Then
                ' If Not (CtrlCustDtls1.cboAddrType.SelectedValue Is DBNull.Value) AndAlso Not (CtrlCustDtls1.cboAddrType.SelectedValue = 99) Then
                drDelvAdds = CtrlCustDtls1.dtCustmInfo.Select("AddressType='" & CtrlCustDtls1.cboAddrType.SelectedValue & "' ")(0)
            Else
                drDelvAdds = CtrlCustDtls1.dtCustmInfo.Rows(0)
            End If

            vCurrentDate = objComn.GetCurrentDate()
            If IsAllowedSalesReturn = False Then

                If Not (dsScan.Tables(0).Rows.Count > 0) Then
                    ShowMessage(getValueByKey("SO027"), "SO027 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Please Select Sales Order first", "Sales Order Information")
                    Exit Sub
                End If

                'Comment by Ashish on 25 Nov 2010
                'Adding a check for variable "flg" to enter or skip the "If CDbl(CtrlCashSummary1.lbltxt6) < CDbl(CtrlCashSummary1.lbltxt5) Then" condition
                'This is for allowing saving of SO when override amount = 0
                If flg = False Then
                    If Not (CDbl(lblReceivedAmt.Text.Trim) >= CDbl(lblminadvancepaid.Text.Trim)) Then
                        If CDbl(CtrlCashSummary1.lbltxt6.Trim) > 0 Then
                            'ShowMessage(getValueByKey("SO017") & CDbl(CDbl(lblminadvancepaid.Text.Trim)), "SO017 - " & getValueByKey("CLAE04"))
                            'If Not (CDbl(lblReceivedAmt.Text.Trim) >= CDbl(lblminadvancepaid.Text.Trim)) Then
                            '    If CDbl(CtrlCashSummary1.lbltxt6.Trim) > 0 Then
                            '        ShowMessage(getValueByKey("SO017") & CDbl(lblminadvancepaid.Text.Trim), getValueByKey("CLAE04"))
                            BtnAcceptPayment_Click(sender, e)
                            Exit Sub
                            'ShowMessage("Need to pay min advance amount.", "Min Advance Amount")
                        End If
                    End If
                End If
                'End of change


                'Comment by Ashish on 25 Nov 2010
                'Adding a check for variable "flg" to enter or skip the "If CDbl(CtrlCashSummary1.lbltxt6) < CDbl(CtrlCashSummary1.lbltxt5) Then" condition
                'This is for allowing saving of SO when override amount = 0
                If flg = False Then
                    If CDbl(CtrlCashSummary1.lbltxt6.Trim) < 0 AndAlso Not (CDbl(CtrlCashSummary1.lbltxt6.Trim) = CDbl(lblReceivedAmt.Text.Trim)) Then
                        ShowMessage(getValueByKey("SO037"), "SO037 - " & getValueByKey("CLAE04"))
                        BtnAcceptPayment_Click(sender, e)
                        Exit Sub
                        'ShowMessage("Need to Return Balance Amount.", "Return Balance Amount")
                    End If
                End If
                'End of change

                'Comment by Ashish on 25 Nov 2010
                'Adding a check for variable "flg" to enter or skip the "If CDbl(CtrlCashSummary1.lbltxt6) < CDbl(CtrlCashSummary1.lbltxt5) Then" condition
                'This is for allowing saving of SO when override amount = 0
                If flg = False Then
                    If Not (CDbl(CtrlCashSummary1.lbltxt6.Trim) >= CDbl(lblReceivedAmt.Text.Trim)) Then
                        ShowMessage(getValueByKey("SO038"), "SO038 - " & getValueByKey("CLAE04"))
                        BtnAcceptPayment_Click(sender, e)
                        Exit Sub
                        'ShowMessage("Need to pay Balance Amount.", "Return Balance Amount")
                    End If
                End If
                'End of change

                If OnlineConnect = True Then
                    'Start=================Apply Customer Loyalty Program======================
                    If CtrlCustDtls1.lblCustTypeValue.Text = "CLP" AndAlso Val(lblOrderQty.Text) = Val(lblPickupQty.Text) + Val(lbldeliveredqty.Text) Then
                        vCardType = CtrlCustDtls1.dtCustmInfo.Rows(0)("CARDTYPE").ToString()
                        CalCulateCLP(vCardType, dsScan.Tables("ItemScanDetails"), "(PickUpQty>0 Or DeliveredQty>0) and IsCLP='True' and IsStatus<>'Deleted'")
                        Dim Point As Object
                        Point = dsScan.Tables("ItemScanDetails").Compute("Sum(CLPPoints)", "(PickUpQty>0 Or DeliveredQty>0) and IsCLP='True' and IsStatus<>'Deleted'")
                        If Not Point Is DBNull.Value Then
                            TotalPoints = CDbl(Point)
                        End If
                    End If

                    Try
                        rbnLStatus.SmallImage = Spectrum.My.Resources.connected1.ToBitmap
                        rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus2")
                    Catch ex As Exception

                    End Try
                Else
                    Try
                        rbnLStatus.SmallImage = Spectrum.My.Resources.disconnected1.ToBitmap
                        rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus1")
                    Catch ex As Exception

                    End Try
                End If

                'If clsDefaultConfiguration.SupplierCode = Nothing Then
                '    ShowMessage(getValueByKey("SO080"), "SO080 - " & getValueByKey("CLAE04"))
                '    Exit Sub
                'End If
                If Not (PrepareHdrDataforSave(dsMain) = True) Then
                    Exit Sub
                End If
                If Not (PrepareDtlDataforSave(dsMain) = True) Then
                    Exit Sub
                End If

                If Not (PrepareInvcDataforSave(dsMain) = True) Then
                    Exit Sub
                End If
                If Not (PrepareOtherTaxDataforSave(dsMain) = True) Then
                    Exit Sub
                End If

                If Val(lblPickupQty.Text) > 0 Then
                    IsOutboundCreated = True
                    If Not (PrepareOrderHdrDataforSave(dsMain) = True) Then
                        Exit Sub
                    End If
                    If Not (PrepareOrderDtlDataforSave(dsMain) = True) Then
                        Exit Sub
                    End If
                End If



                'Start=================Change History Information==========================
                Dim dtUnchange As New DataTable
                dtUnchange.Merge(dsMain.Tables("SalesOrderDTL"))

                Dim dvHdrAudit As New DataView(dsMain.Tables("SalesOrderHDR"), "", "", DataViewRowState.ModifiedOriginal)
                Dim dvDtlUnchanged As New DataView(dtUnchange, "", "ArticleCode", DataViewRowState.OriginalRows)
                Dim dvDtlchanged As New DataView(dtUnchange, "", "ArticleCode", DataViewRowState.ModifiedCurrent)

                If dvHdrAudit.ToTable.Rows.Count > 0 Then

                    dvDtlchanged.AllowEdit = True
                    For Each drChanged As DataRowView In dvDtlchanged
                        Dim drIsChages As DataRow = dvDtlUnchanged.ToTable.Select("EAN='" & drChanged("EAN") & "'")(0)

                        If drChanged("Quantity") = drIsChages("Quantity") AndAlso drChanged("ArticleStatus") <> "Deleted" Then
                            drChanged.Delete()
                        ElseIf drChanged("Status") = True Then

                        End If
                    Next

                    If dvDtlchanged.Count > 0 Then
                        dsMain.Tables("SalesOrderHdrAudit").Merge(dvHdrAudit.ToTable(), False, MissingSchemaAction.Ignore)
                        'dsMain.Tables("SalesOrderDtlAudit").Merge(dvDtlchanged.ToTable(), False, MissingSchemaAction.Ignore)
                        For Each drAudit As DataRowView In dvDtlchanged
                            Dim drNew As DataRow = dsMain.Tables("SalesOrderDtlAudit").NewRow
                            For Each col As DataColumn In dsMain.Tables("SalesOrderDtlAudit").Columns
                                If dvDtlchanged.ToTable.Columns.Contains(col.ColumnName) = True Then
                                    drNew(col.ColumnName) = drAudit(col.ColumnName)
                                End If
                            Next
                            drNew("AmendedNo") = vAmendedNo
                            dsMain.Tables("SalesOrderDtlAudit").Rows.Add(drNew)
                        Next
                        'For Each drDtl As DataRow In dsMain.Tables("SalesOrderDtlAudit").Select("", "", DataViewRowState.Added)
                        '    drDtl("AmendedNo") = vAmendedNo
                        'Next
                    End If

                End If
                'End===================Change History Information==========================
                If IssuingCV = True And clsAdmin.CVProgram = String.Empty Then
                    ShowMessage(getValueByKey("SO069"), "SO069 - " & getValueByKey("CLAE05"))
                    Exit Sub
                End If

                If DtSoBulkComboHdr.Rows.Count > 0 Then
                    For index = 0 To DtSoBulkComboHdr.Rows.Count - 1
                        DtSoBulkComboHdr(index)("SaleOrderNumber") = CtrlSalesInfo.CtrlTxtOrderNo.Text

                        'Dim BillNo = (From x In dsMain.Tables("SalesOrderDTL")
                        '               Where x("RowIndex") = DtSoBulkComboHdr(index)("ComboSrNo")
                        '               Select x("BillLineNo")).FirstOrDefault()

                        Dim dr() = dsMain.Tables("SalesOrderDTL").Select("RowIndex=" & DtSoBulkComboHdr(index)("ComboSrNo"))
                        Dim BillNo As Integer = 0
                        If dr.Length > 0 Then
                            BillNo = dr(0)("BillLineNo")
                        Else
                            Throw New ApplicationException("Error in saving")
                        End If
                        If BillNo > 0 Then
                            DtSoBulkComboHdr(index)("ComboSrNo") = BillNo
                        End If
                    Next

                    objSO.DtSoBulkComboHdr = DtSoBulkComboHdr
                    objSO.DtSoBulkComboDtl = DtSoBulkComboDtl
                End If

                dsMain.Tables("SalesOrderDTL").Columns.Remove("RowIndex")

                'code added by irfan on 2/10/2017 for IGST CAKEKRAFT=====================================
                If dsMain.Tables("SalesOrderTaxDtls").Columns.Contains("CustomerNo") = True Then
                    dsMain.Tables("SalesOrderTaxDtls").Columns.Remove("CustomerNo")
                End If
                '=========================================================================================



                dtSalesOrderTaxDetails = dsMain.Tables("SalesOrderTaxDtls").Copy()

                '--- for SO Generate
                objSO.IsStrGenerate = IsSTRGenerate

                If objSO.PrepareSaveData(vSalesInvcNo, clsAdmin.DayOpenDate, clsAdmin.CLPProgram, CtrlCustDtls1.lblCustNo.Text, dsMain, False, IsNextInvoiceNo, vSiteCode, CtrlSalesInfo.CtrlTxtOrderNo.Text, clsDefaultConfiguration.StockStorageLocation, clsAdmin.CVProgram, "SalesOrder", vfinancialYear, clsAdmin.UserCode, clsAdmin.CurrentDate, IsOutboundCreated, CVoucherNo, CVVoucherDay, isPromotionApplied, DtDeletedData, , clsDefaultConfiguration.IsBatchManagementReq, Batchbarcode) = True Then
                    If OnlineConnect = True Then
                        'Apply Customer loyalty Point
                        If CtrlCustDtls1.lblCustTypeValue.Text = "CLP" AndAlso Val(lblOrderQty.Text) = Val(lblPickupQty.Text) + Val(lbldeliveredqty.Text) Then
                            'CalCulateCLP(vCardType, dsScan.Tables("ItemScanDetails"), "PickUpQty>0 Or DeliveredQty>0 ")

                            If dsMainCLP.Tables.Count = 0 Then
                                dsMainCLP.Tables.Add(dsMain.Tables("CLPTransaction").Clone())
                                dsMainCLP.Tables.Add(dsMain.Tables("CLPTransactionsDetails").Clone)
                            End If

                            dsMainCLP.Clear()

                            If TotalPoints > 0 AndAlso PrepareClpHdrDataforSave(dsMainCLP) = True AndAlso PrepareClpDtlDataforSave(dsMainCLP) = True Then
                                If objSO.PrepareSaveClpData(dsMainCLP, vClpProgramId, CtrlCustDtls1.lblCustNoValue.Text, TotalPoints, vSiteCode, CtrlSalesInfo.CtrlTxtOrderNo.Text) = False Then
                                    ShowMessage(getValueByKey("SO018"), "SO018 - " & getValueByKey("CLAE04"))
                                    'ShowMessage("CLP Data is not Saved....", "Information")
                                End If
                            End If

                            dsMainCLP.Clear()
                        End If

                        Try
                            rbnLStatus.SmallImage = Spectrum.My.Resources.connected1.ToBitmap
                            rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus2")
                        Catch ex As Exception

                        End Try
                    Else
                        Try
                            rbnLStatus.SmallImage = Spectrum.My.Resources.disconnected1.ToBitmap
                            rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus1")
                        Catch ex As Exception

                        End Try
                    End If


                    'Print Sales Order Information.-----------------------------------
                    PrintSalesOrders(drSiteInfo, drHomeAdds, drDelvAdds)
                    Dim totalPay As Double

                    'Print Sales Invoice Information----------------------------------
                    If Not (dsPayment.Tables("MSTRecieptType") Is Nothing) AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                        '---- Commented By Mahesh Not required as we are showing on bill itself disscussed with B.A. Manish
                        '  PrintSalesOrdersInvoice(drSiteInfo, drHomeAdds, drDelvAdds)

                        For Each dr As DataRow In dsPayment.Tables("MSTRecieptType").Select("RecieptTypeCode='CreditVouc(I)'", "", DataViewRowState.CurrentRows)
                            totalPay = IIf(dr("Amount") > 0, dr("Amount"), dr("Amount") * -1)

                            'PrintCreditVoucher(drSiteInfo, totalPay)
                            clsVoucher.PrintGiftVoucherAndCreditNote("SalesOrder", clsAdmin.SiteCode, "CreditNote", String.Empty, totalPay, String.Empty, clsAdmin.UserName, CtrlSalesInfo.CtrlTxtOrderNo.Text, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                        Next
                        For Each dr As DataRow In dsPayment.Tables("MSTRecieptType").Select("RecieptTypeCode='GiftVoucher(I)'", "", DataViewRowState.CurrentRows)
                            totalPay = IIf(dr("Amount") > 0, dr("Amount"), dr("Amount") * -1)

                            'PrintCreditVoucher(drSiteInfo, totalPay)
                            clsVoucher.PrintGiftVoucherAndCreditNote("SalesOrder", clsAdmin.SiteCode, "GiftVoucher", String.Empty, totalPay, String.Empty, clsAdmin.UserName, CtrlSalesInfo.CtrlTxtOrderNo.Text, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                        Next
                        'If CInt(CDbl(CtrlCashSummary1.lbltxt6)) < 0 Then
                        '    PrintCreditVoucher(drSiteInfo)
                        'Else
                        '    PrintSalesOrdersInvoice(drSiteInfo, drHomeAdds, drDelvAdds)
                        'End If
                    End If
                    '---- Commented By Mahesh Not required as we are showing on bill itself disscussed with B.A. Manish
                    'Print Sales Delivery Information---------------------------------
                    If CDbl(lblPickupQty.Text) > 0 Then
                        PrintSalesOrdersDelivery(drSiteInfo, drHomeAdds, drDelvAdds)
                    End If

                    'If CDbl(lblPickupQty.Text) > 0 AndAlso dsPayment.Tables("MSTRecieptType") Is Nothing Then
                    '    Dim dtInvoice As New DataTable
                    '    dtInvoice = objComn.GetPaymentInfo

                    '    PrintSalesOrdersInvoice(drSiteInfo, drHomeAdds, drDelvAdds, dtInvoice)
                    'End If

                    If IsGiftVoucher Then
                        PrintGiftReceipt()
                    End If

                    ShowMessage(getValueByKey("SO039"), "SO039 - " & getValueByKey("CLAE04"))
                    IsSOSaved = True
                    'ShowMessage("Sales Order Updated", "Sales Order")
                    ResetSalesOrder()
                    isLeaved = False
                    CtrlSalesInfo.CtrlTxtOrderNo.Text = String.Empty
                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                Else
                    ShowMessage(getValueByKey("SO040"), "SO040 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Sales Order does not Updated", "Sales Order")
                End If

            Else
                ' function for sales order return **********************************
                Dim tempPurchaseQty As Decimal = 0
                Dim IsReturnValid = False
                For Each drGridRow As C1.Win.C1FlexGrid.Row In grdSOItemRetuns.Rows
                    If Not (drGridRow.Index = 0) Then
                        If drGridRow.Item("PickUpQty") > 0 Then
                            IsReturnValid = True
                            Exit For
                        End If
                    End If
                Next

                Dim returnQty As Double = grdSOItemRetuns.Item(1, "PickUpQty")
                If Not (dsScan.Tables(0).Rows.Count > 0) Then
                    ShowMessage(getValueByKey("SO027"), "SO027 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Please Select Sales Order first", "Sales Order Information")
                    Exit Sub
                ElseIf txtReturnReason.Text.Trim = "" Then
                    ShowMessage(getValueByKey("SO041"), "SO041 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Please Enter Reason for Return Sales Order Articles", "Sales Order Information")
                    txtReturnReason.Select()
                    Exit Sub
                ElseIf IsReturnValid = False Then
                    ShowMessage(getValueByKey("SO093"), "SO093 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Please Enter Return Quantity")
                    Exit Sub
                End If

                dsMain.Tables("SalesOrderHDR").RejectChanges()
                dsMain.Tables("SalesOrderHDRAudit").RejectChanges()
                dsMain.Tables("SalesOrderDTL").RejectChanges()
                dsMain.Tables("SalesOrderDTLAudit").RejectChanges()
                dsMain.Tables("SalesInvoice").RejectChanges()
                dsMain.Tables("SalesOrderTaxDtls").RejectChanges()
                dsMain.Tables("SalesOrderOtherCharges").RejectChanges()
                dsMain.Tables("OrderHdr").RejectChanges()
                dsMain.Tables("OrderDtl").RejectChanges()

                Dim drNewReturnHdr As DataRow
                Dim drOldReturnHdr As DataRow
                Dim findKeyh(2) As Object
                Dim IsNewReturn As Boolean = False

                If dsScanReturn.Tables(0).Select("PickUpQty>0").Count > 0 Then

                    'Changed by Rohit to generate Document No. for proper sorting
                    Dim objType = "FO_DOC"
                    Dim NewSalesNo As String = String.Empty
                    Try
                        NewSalesNo = GenDocNo("SO" & clsAdmin.TerminalID & vfinancialYear.ToString.Substring(vfinancialYear.ToString.Length - 2, 2), 15, objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode, objType))
                    Catch ex As Exception
                        NewSalesNo = "SO" & clsAdmin.TerminalID & objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode, objType)
                    End Try
                    'End Change by Rohit

                    For Each drReturnSales As DataRow In dsScanReturn.Tables(0).Select("PickUpQty>0")
                        Dim drNewReturnDtl As DataRow
                        Dim drOldReturnDtl As DataRow
                        Dim findKeyd(4) As Object
                        Dim billLineNo As Integer
                        If IsDBNull(drReturnSales("BillLineNo")) = False Then
                            billLineNo = drReturnSales("BillLineNo")
                        Else
                            billLineNo = dsMain.Tables("SalesOrderDTL").Rows.Count + 1
                        End If
                        'Start- Update Old Sales Order for Return Sales Order Article
                        findKeyd(0) = vSiteCode
                        findKeyd(1) = vfinancialYear
                        findKeyd(2) = CtrlSalesInfo.CtrlTxtOrderNo.Text
                        findKeyd(3) = drReturnSales("EAN").ToString
                        findKeyd(4) = billLineNo
                        drOldReturnDtl = dsMain.Tables("SalesOrderDTL").Rows.Find(findKeyd)

                        drOldReturnDtl("DeliveredQty") = drOldReturnDtl("DeliveredQty") - drReturnSales("PickUpQty")
                        drOldReturnDtl("ReturnSaleOrderNo") = NewSalesNo
                        drOldReturnDtl("ReturnSaleOrderDt") = vCurrentDate
                        drOldReturnDtl("ReturnQty") = IIf(drOldReturnDtl("ReturnQty") Is DBNull.Value, 0, drOldReturnDtl("ReturnQty")) + drReturnSales("PickUpQty")
                        drOldReturnDtl("SalesReturnReasonCode") = IIf(drReturnSales("ReturnReasonCode") Is DBNull.Value, txtReturnReason.Text.Trim, drReturnSales("ReturnReasonCode"))
                        'drOldReturnDtl("ReservedQty") = drOldReturnDtl("ReservedQty") + drReturnSales("PickUpQty")
                        drOldReturnDtl("UpdatedAt") = vSiteCode
                        drOldReturnDtl("UpdatedBy") = vUserName
                        drOldReturnDtl("UpdatedOn") = vCurrentDate


                        'Start- Add New Sales Order for Return Sales Order Article
                        findKeyd(0) = vSiteCode
                        findKeyd(1) = vfinancialYear
                        findKeyd(2) = NewSalesNo
                        findKeyd(3) = drReturnSales("EAN").ToString
                        findKeyd(4) = billLineNo
                        drNewReturnDtl = dsMain.Tables("SalesOrderDTL").Rows.Find(findKeyd)

                        If drNewReturnDtl Is Nothing Then
                            drNewReturnDtl = dsMain.Tables("SalesOrderDTL").NewRow()
                            IsNewReturn = True
                        End If

                        drNewReturnDtl("SiteCode") = drOldReturnDtl("SiteCode")
                        drNewReturnDtl("SaleOrderNumber") = NewSalesNo
                        drNewReturnDtl("EAN") = drOldReturnDtl("EAN")
                        drNewReturnDtl("BillLineNo") = drOldReturnDtl("BillLineNo")
                        drNewReturnDtl("BatchBarcode") = drOldReturnDtl("BatchBarcode")
                        For NewRowIndex = 3 To drOldReturnDtl.ItemArray.Count - 1
                            drNewReturnDtl(NewRowIndex) = drOldReturnDtl(NewRowIndex)
                        Next

                        drNewReturnDtl("NetSellingPrice") = FormatNumber(drOldReturnDtl("NetAmount") / drOldReturnDtl("Quantity"), 2)
                        tempPurchaseQty = drReturnSales("Quantity")
                        drNewReturnDtl("Quantity") = drReturnSales("PickUpQty") * -1
                        drNewReturnDtl("GrossAmount") = FormatNumber(drNewReturnDtl("SellingPrice") * drNewReturnDtl("Quantity"), 2)
                        drNewReturnDtl("NetAmount") = FormatNumber(drNewReturnDtl("NetSellingPrice") * drNewReturnDtl("Quantity"), 2)
                        drNewReturnDtl("DeliveredQty") = 0
                        drNewReturnDtl("ReservedQty") = 0 'drReturnSales("PickUpQty")
                        drNewReturnDtl("OfferNo") = ""
                        drNewReturnDtl("IsCLPApplicable") = 0
                        drNewReturnDtl("CLPPoints") = 0
                        drNewReturnDtl("Delivered_Qty") = drNewReturnDtl("Quantity")
                        'drNewReturnDtl("Reserved_Qty") = drReturnSales("PickUpQty")
                        drNewReturnDtl("DiscountAmount") = FormatNumber(IIf(CDbl(drOldReturnDtl("DiscountAmount").ToString()) = 0, 0, (drOldReturnDtl("DiscountAmount") / drOldReturnDtl("Quantity")) * drNewReturnDtl("Quantity")), 2)
                        drNewReturnDtl("LineDiscount") = FormatNumber(IIf(CDbl(drOldReturnDtl("LineDiscount").ToString()) = 0, 0, (drOldReturnDtl("LineDiscount") / drOldReturnDtl("Quantity")) * drNewReturnDtl("Quantity")), 2)
                        drNewReturnDtl("DiscountPercentage") = FormatNumber(IIf(CDbl(drOldReturnDtl("DiscountAmount").ToString()) = 0, 0, (drNewReturnDtl("DiscountAmount") / drNewReturnDtl("GrossAmount")) * 100), 2)
                        drNewReturnDtl("ExclTaxAmt") = FormatNumber(IIf(CDbl(drOldReturnDtl("ExclTaxAmt").ToString()) = 0, 0, (drOldReturnDtl("ExclTaxAmt") / drOldReturnDtl("Quantity")) * drNewReturnDtl("Quantity")), 2)
                        drNewReturnDtl("TotalTaxAmount") = FormatNumber(IIf(CDbl(drOldReturnDtl("TotalTaxAmount").ToString()) = 0, 0, (drOldReturnDtl("TotalTaxAmount") / drOldReturnDtl("Quantity")) * drNewReturnDtl("Quantity")), 2)

                        drNewReturnDtl("TransactionCode") = vDocType
                        drNewReturnDtl("ReturnSaleOrderNo") = CtrlSalesInfo.CtrlTxtOrderNo.Text
                        drNewReturnDtl("ReturnSaleOrderDt") = vCurrentDate
                        'drNewReturnDtl("ReturnQty") = drReturnSales("PickUpQty")
                        drNewReturnDtl("SalesReturnReasonCode") = IIf(drReturnSales("ReturnReasonCode") Is DBNull.Value, txtReturnReason.Text.Trim, drReturnSales("ReturnReasonCode"))

                        drNewReturnDtl("ArticleStatus") = "Return"
                        drNewReturnDtl("CreatedAt") = vSiteCode
                        drNewReturnDtl("CreatedBy") = clsAdmin.UserCode 'vUserName
                        drNewReturnDtl("CreatedOn") = vCurrentDate
                        drNewReturnDtl("UpdatedAt") = vSiteCode
                        drNewReturnDtl("UpdatedBy") = clsAdmin.UserCode 'vUserName
                        drNewReturnDtl("UpdatedOn") = vCurrentDate
                        drNewReturnDtl("FinYear") = vfinancialYear

                        If IsNewReturn = True Then
                            dsMain.Tables("SalesOrderDTL").Rows.Add(drNewReturnDtl)
                            IsNewReturn = False
                        End If

                        'End- Add New Sales Order for Return Sales Order Article


                    Next
                    findKeyh(0) = vSiteCode
                    findKeyh(1) = vfinancialYear
                    findKeyh(2) = NewSalesNo
                    drNewReturnHdr = dsMain.Tables("SalesOrderHDR").Rows.Find(findKeyh)

                    If drNewReturnHdr Is Nothing Then
                        drNewReturnHdr = dsMain.Tables("SalesOrderHDR").NewRow()

                        drOldReturnHdr = dsMain.Tables("SalesOrderHDR").Rows(0)

                        drNewReturnHdr("SiteCode") = drOldReturnHdr("SiteCode")
                        drNewReturnHdr("SaleOrderNumber") = NewSalesNo
                        drNewReturnHdr("TerminalID") = vTerminalID
                        drNewReturnHdr("TransactionCode") = vDocType

                        For NewRowIndex = 5 To drOldReturnHdr.ItemArray.Count - 1
                            drNewReturnHdr(NewRowIndex) = drOldReturnHdr(NewRowIndex)
                        Next

                        drNewReturnHdr("NetAmt") = dsMain.Tables("SalesOrderDTL").Compute("Sum(NetAmount)", "ArticleStatus='Return'")
                        drNewReturnHdr("CostAmt") = dsMain.Tables("SalesOrderDTL").Compute("Sum(CostAmount)", "ArticleStatus='Return'")
                        drNewReturnHdr("GrossAmt") = dsMain.Tables("SalesOrderDTL").Compute("Sum(GrossAmount)", "ArticleStatus='Return'")
                        drNewReturnHdr("DiscountPercentage") = 0
                        drNewReturnHdr("DiscountAmt") = dsMain.Tables("SalesOrderDTL").Compute("Sum(LineDiscount)", "ArticleStatus='Return'")
                        drNewReturnHdr("TotalDiscount") = dsMain.Tables("SalesOrderDTL").Compute("Sum(DiscountAmount)", "ArticleStatus='Return'")
                        drNewReturnHdr("CurrencyCode") = clsAdmin.CurrencyCode
                        drNewReturnHdr("TaxAmount") = dsMain.Tables("SalesOrderDTL").Compute("Sum(TotalTaxAmount)", "ArticleStatus='Return'")
                        drNewReturnHdr("SOStatus") = "Return"
                        drNewReturnHdr("CreatedAt") = vSiteCode
                        drNewReturnHdr("CreatedBy") = clsAdmin.UserCode 'vUserName
                        drNewReturnHdr("CreatedOn") = vCurrentDate
                        drNewReturnHdr("UpdatedAt") = vSiteCode
                        drNewReturnHdr("UpdatedBy") = clsAdmin.UserCode 'vUserName
                        drNewReturnHdr("UpdatedOn") = vCurrentDate
                        drNewReturnHdr("FinYear") = vfinancialYear
                        dsMain.Tables("SalesOrderHDR").Rows.Add(drNewReturnHdr)
                    End If




                    Dim drOrderHdr As DataRow
                    Dim findKey(2) As Object

                    Dim clsCommon As New clsCommon
                    Dim vOBNumber As String = String.Empty
                    'vOBNumber = "OB" & clsAdmin.TerminalID & clsCommon.getDocumentNo("OutBound")            

                    If OnlineConnect = True Then
                        'Changed by Rohit to generate Document No. for proper sorting
                        Try
                            vOBNumber = "OB" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2)
                            vOBNumber = GenDocNo(vOBNumber, 15, clsCommon.getDocumentNo("OutBound", clsAdmin.SiteCode))
                        Catch ex As Exception
                            vOBNumber = "OB" & clsAdmin.TerminalID & clsCommon.getDocumentNo("OutBound", clsAdmin.SiteCode)
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
                            vOBNumber = "OOB" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2)
                            vOBNumber = GenDocNo(vOBNumber, 15, clsCommon.getDocumentNo("OutBound", clsAdmin.SiteCode))
                        Catch ex As Exception
                            vOBNumber = "OOB" & clsAdmin.TerminalID & clsCommon.getDocumentNo("OutBound", clsAdmin.SiteCode)
                        End Try

                        Try
                            rbnLStatus.SmallImage = Spectrum.My.Resources.disconnected1.ToBitmap
                            rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus1")
                        Catch ex As Exception

                        End Try
                        'End Change by Rohit
                    End If

                    findKey(0) = vSiteCode
                    findKey(1) = vfinancialYear
                    findKey(2) = vOBNumber

                    drOrderHdr = dsMain.Tables("OrderHdr").Rows.Find(findKey)
                    If drOrderHdr Is Nothing Then
                        drOrderHdr = dsMain.Tables("OrderHdr").NewRow
                        IsNewRow = True
                    End If

                    drOrderHdr("SiteCode") = vSiteCode
                    If IsNewRow = False Then
                        drOrderHdr("FinYear") = vfinancialYear
                    Else
                        drOrderHdr("FinYear") = clsAdmin.Financialyear
                    End If

                    drOrderHdr("DocumentNumber") = vOBNumber
                    drOrderHdr("DocumentType") = vDocType
                    drOrderHdr("DocDate") = vCurrentDate
                    'drOrderHdr("SupplierCode") = clsDefaultConfiguration.SupplierCode
                    drOrderHdr("PurchaseGroupCode") = DBNull.Value
                    drOrderHdr("DeliverySiteCode") = vSiteCode
                    drOrderHdr("ExpectedDeliveryDt") = CtrlSalesInfo.CtrlDtExpDelDate.Value

                    drOrderHdr("PaymentAfterDelDays") = DBNull.Value
                    drOrderHdr("AdditionalTermsNConditions") = DBNull.Value
                    drOrderHdr("AdditionalPaymentTerms") = DBNull.Value
                    drOrderHdr("DocNetValue") = CDbl(CtrlCashSummary1.lbltxt4) * -1
                    drOrderHdr("DocGrossValue") = CDbl(CtrlCashSummary1.lbltxt1.Trim) * -1
                    drOrderHdr("IsClosed") = True
                    drOrderHdr("IsFreezed") = True
                    drOrderHdr("SalesOrderRef") = CtrlSalesInfo.CtrlTxtOrderNo.Text
                    drOrderHdr("ReturnReasonCode") = " "
                    drOrderHdr("Remarks") = CtrlSalesInfo.CtrlTxtRemarks.Text.Trim
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
                    drOrderHdr("CREATEDON") = vCurrentDate
                    drOrderHdr("UPDATEDAT") = vSiteCode
                    drOrderHdr("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                    drOrderHdr("UPDATEDON") = vCurrentDate
                    drOrderHdr("STATUS") = True
                    drOrderHdr("SupplierCode") = " "

                    If IsNewRow = True Then
                        dsMain.Tables("OrderHdr").Rows.Add(drOrderHdr)
                        IsNewRow = False
                    End If



                    If clsDefaultConfiguration.IsBatchManagementReq Then
                        Dim drOrderDtl As DataRow
                        Dim rowIndex As Integer = 1
                        Dim findKey1(4) As Object

                        For Each drScan As SpectrumCommon.BtachbarcodeInfo In ReturnBatchbarcode
                            'If drScan("PickUpQty") > 0 Then
                            Dim dr = dsScanReturn.Tables(0).Select("PickUpQty>0 AND EAN='" & drScan.EAN & "'").FirstOrDefault()
                            findKey1(0) = vSiteCode
                            findKey1(1) = vfinancialYear
                            findKey1(2) = dsMain.Tables("OrderHdr").Rows(0)("DocumentNumber")
                            findKey1(3) = drScan.EAN
                            findKey1(4) = rowIndex

                            drOrderDtl = dsMain.Tables("OrderDtl").Rows.Find(findKey1)
                            If drOrderDtl Is Nothing Then
                                drOrderDtl = dsMain.Tables("OrderDtl").NewRow
                                IsNewRow = True
                            End If

                            drOrderDtl("SiteCode") = vSiteCode

                            If IsNewRow = False Then
                                drOrderDtl("FinYear") = vfinancialYear
                            Else
                                drOrderDtl("FinYear") = clsAdmin.Financialyear
                            End If

                            drOrderDtl("DocumentNumber") = dsMain.Tables("OrderHdr").Rows(0)("DocumentNumber")
                            drOrderDtl("ArticleCode") = drScan.ArticleCode
                            drOrderDtl("EAN") = drScan.EAN

                            drOrderDtl("Qty") = dr("Quantity")
                            drOrderDtl("UnitofMeasure") = vUOM
                            drOrderDtl("LineNumber") = rowIndex
                            drOrderDtl("OpenQty") = DBNull.Value
                            drOrderDtl("DeliveredQty") = drScan.Qty
                            drOrderDtl("DeliveryCompleted") = DBNull.Value
                            drOrderDtl("PurchaseGroupCode") = DBNull.Value
                            drOrderDtl("RefDocument") = DBNull.Value
                            drOrderDtl("RefDocumentNo") = DBNull.Value
                            drOrderDtl("BarCode") = drScan.BatchBarcode
                            drOrderDtl("PONo") = DBNull.Value
                            drOrderDtl("BirthListId") = DBNull.Value
                            drOrderDtl("SaleOrderNumber") = CtrlSalesInfo.CtrlTxtOrderNo.Text
                            drOrderDtl("AllocationRule") = DBNull.Value
                            'drOrderDtl("MRP") = drScan("SellingPrice")
                            drOrderDtl("MRP") = Math.Round(dr("NetAmount") / dr("Quantity"), clsDefaultConfiguration.DecimalPlaces)
                            drOrderDtl("CostPrice") = dr("SellingPrice")
                            drOrderDtl("NetCostPrice") = dr("SellingPrice")

                            drOrderDtl("ExciseDutyAmt") = DBNull.Value
                            drOrderDtl("ExciseDutyRate") = DBNull.Value
                            drOrderDtl("PurchaseTaxAmt") = DBNull.Value
                            drOrderDtl("PurchaseTaxRate") = DBNull.Value
                            drOrderDtl("AdditionalChargeAmt") = DBNull.Value
                            drOrderDtl("DiscountAmount") = dr("LineDiscount")
                            drOrderDtl("AdditionalChargeRate") = DBNull.Value  'drScan("ExclTaxAmt")
                            drOrderDtl("DocValue") = DBNull.Value
                            drOrderDtl("InboundQty") = DBNull.Value
                            drOrderDtl("DayOpenDate") = clsAdmin.DayOpenDate

                            drOrderDtl("CREATEDAT") = vSiteCode
                            drOrderDtl("CREATEDBY") = clsAdmin.UserCode 'vUserName
                            drOrderDtl("CREATEDON") = vCurrentDate
                            drOrderDtl("UPDATEDAT") = vSiteCode
                            drOrderDtl("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                            drOrderDtl("UPDATEDON") = vCurrentDate
                            drOrderDtl("STATUS") = True

                            If IsNewRow = True Then
                                dsMain.Tables("OrderDtl").Rows.Add(drOrderDtl)
                                IsNewRow = False
                            End If
                            rowIndex = rowIndex + 1
                            'End If
                            Batchbarcode.Add(drScan)
                        Next


                    Else
                        Dim drOrderDtl As DataRow
                        Dim rowIndex As Integer = 1
                        Dim findKey1(4) As Object

                        For Each drScan As DataRow In dsScanReturn.Tables(0).Select("PickUpQty>0")
                            'If drScan("PickUpQty") > 0 Then

                            findKey1(0) = vSiteCode
                            findKey1(1) = vfinancialYear
                            findKey1(2) = dsMain.Tables("OrderHdr").Rows(0)("DocumentNumber")
                            findKey1(3) = drScan("EAN")
                            findKey1(4) = rowIndex

                            drOrderDtl = dsMain.Tables("OrderDtl").Rows.Find(findKey1)
                            If drOrderDtl Is Nothing Then
                                drOrderDtl = dsMain.Tables("OrderDtl").NewRow
                                IsNewRow = True
                            End If

                            drOrderDtl("SiteCode") = vSiteCode

                            If IsNewRow = False Then
                                drOrderDtl("FinYear") = vfinancialYear
                            Else
                                drOrderDtl("FinYear") = clsAdmin.Financialyear
                            End If

                            drOrderDtl("DocumentNumber") = dsMain.Tables("OrderHdr").Rows(0)("DocumentNumber")
                            drOrderDtl("ArticleCode") = drScan("ArticleCode")
                            drOrderDtl("EAN") = drScan("EAN")

                            drOrderDtl("Qty") = drScan("Quantity")
                            drOrderDtl("UnitofMeasure") = vUOM
                            drOrderDtl("LineNumber") = rowIndex
                            drOrderDtl("OpenQty") = DBNull.Value
                            drOrderDtl("DeliveredQty") = drScan("PickUpQty") * -1
                            drOrderDtl("DeliveryCompleted") = DBNull.Value
                            drOrderDtl("PurchaseGroupCode") = DBNull.Value
                            drOrderDtl("RefDocument") = DBNull.Value
                            drOrderDtl("RefDocumentNo") = DBNull.Value
                            drOrderDtl("BarCode") = drScan("BatchBarCode")
                            drOrderDtl("PONo") = DBNull.Value
                            drOrderDtl("BirthListId") = DBNull.Value
                            drOrderDtl("SaleOrderNumber") = CtrlSalesInfo.CtrlTxtOrderNo.Text
                            drOrderDtl("AllocationRule") = DBNull.Value
                            'drOrderDtl("MRP") = drScan("SellingPrice")
                            drOrderDtl("MRP") = Math.Round(drScan("NetAmount") / drScan("Quantity"), clsDefaultConfiguration.DecimalPlaces)
                            drOrderDtl("CostPrice") = drScan("SellingPrice")
                            drOrderDtl("NetCostPrice") = drScan("SellingPrice")

                            drOrderDtl("ExciseDutyAmt") = DBNull.Value
                            drOrderDtl("ExciseDutyRate") = DBNull.Value
                            drOrderDtl("PurchaseTaxAmt") = DBNull.Value
                            drOrderDtl("PurchaseTaxRate") = DBNull.Value
                            drOrderDtl("AdditionalChargeAmt") = DBNull.Value
                            drOrderDtl("DiscountAmount") = drScan("LineDiscount")
                            drOrderDtl("AdditionalChargeRate") = DBNull.Value  'drScan("ExclTaxAmt")
                            drOrderDtl("DocValue") = DBNull.Value
                            drOrderDtl("InboundQty") = DBNull.Value
                            drOrderDtl("DayOpenDate") = clsAdmin.DayOpenDate

                            drOrderDtl("CREATEDAT") = vSiteCode
                            drOrderDtl("CREATEDBY") = clsAdmin.UserCode 'vUserName
                            drOrderDtl("CREATEDON") = vCurrentDate
                            drOrderDtl("UPDATEDAT") = vSiteCode
                            drOrderDtl("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                            drOrderDtl("UPDATEDON") = vCurrentDate
                            drOrderDtl("STATUS") = True

                            If IsNewRow = True Then
                                dsMain.Tables("OrderDtl").Rows.Add(drOrderDtl)
                                IsNewRow = False
                            End If
                            rowIndex = rowIndex + 1
                            'End If
                        Next


                    End If



                End If
                Dim dstemp As DataSet = dsMain.Copy()
                If objSO.PrepareSaveData(vSalesInvcNo, clsAdmin.DayOpenDate, clsAdmin.CLPProgram, CtrlCustDtls1.lblCustNo.Text, dsMain, True, IsNextInvoiceNo, vSiteCode, CtrlSalesInfo.CtrlTxtOrderNo.Text, clsDefaultConfiguration.StockStorageLocation, clsAdmin.CVProgram, "SalesOrder", vfinancialYear, clsAdmin.UserCode, clsAdmin.CurrentDate, True, CVoucherNo, CVVoucherDay, , , , clsDefaultConfiguration.IsBatchManagementReq, Batchbarcode) = True Then
                    Dim dttemp As DataTable = dsScanReturn.Tables(0).Copy()
                    dsScanReturn.Tables(0).Clear()

                    For Each dr As DataRow In dstemp.Tables("SalesOrderDTL").Select("ArticleStatus='Return'", "", DataViewRowState.CurrentRows)
                        Dim drNew As DataRow = dsScanReturn.Tables(0).NewRow
                        drNew("ArticleCode") = dr("ArticleCode")
                        drNew("Ean") = dr("EAN")
                        'drNew("") = dr("")
                        For Each drART As DataRow In dttemp.Select("ean='" & drNew("Ean") & "'", "", DataViewRowState.CurrentRows)
                            drNew("DISCRIPTION") = drART("DISCRIPTION")
                        Next

                        drNew("sellingprice") = dr("sellingprice")
                        drNew("quantity") = tempPurchaseQty
                        drNew("NetAmount") = dr("NetAmount")
                        drNew("Discount") = dr("DiscountAmount")
                        drNew("PickUpQty") = dr("quantity")
                        drNew("totalDiscPercentage") = dr("DiscountPercentage")
                        drNew("excltaxamt") = dr("ExclTaxAmt")
                        drNew("totalTaxamt") = dr("TotalTaxAmount")
                        drNew("GrossAmt") = dr("GrossAmount")
                        drNew("LineDiscount") = dr("LineDiscount")
                        dsScanReturn.Tables(0).Rows.Add(drNew)
                    Next

                    PrintSalesOrdersReturn(drSiteInfo, drHomeAdds)

                    ShowMessage(getValueByKey("SO042"), "SO042 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Updated Successfully", "Sales Order Return")

                    ResetSalesOrder()
                    isLeaved = False
                    CtrlSalesInfo.CtrlTxtOrderNo.Text = String.Empty

                    BtnSOSave.Visible = True
                    BtnSOPrint.Visible = True
                    rbnGrpCMPromotion.Visible = False
                    BtnSOAcceptPayment.Visible = True
                    BtnSOOtherCharges.Visible = True
                    'BtnSOReturn.Visible = True
                    BtnSOCancel.Visible = False
                    If Not tabReturn Is Nothing Then
                        TabSalesOrder.TabPages.Remove(tabReturn)
                    End If
                    If Not tabPayment Is Nothing Then
                        TabSalesOrder.TabPages.Remove(tabPayment)
                    End If
                    If Not tabSales Is Nothing Then
                        TabSalesOrder.TabPages.Add(tabSales)
                    End If
                    If Not tabPayment Is Nothing Then
                        TabSalesOrder.TabPages.Add(tabPayment)
                    End If
                    CtrlBtnReturn_Click(sender, e)
                    'grdSOItems.Cols(2).Caption
                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                End If
            End If
            GridItemSetting()
            GridDeliverdSetting()
            GridInvoiceSetting()
            isLeaved = False
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Sub


    Private Function PrintGiftReceipt() As Boolean
        Try
            If Not dsPayment Is Nothing AndAlso dsPayment.Tables.Contains("MSTRecieptType") Then

                Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.GiftVoucherDocumentPrint, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, vSalesInvcNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo.CtrlTxtCustOrdRef.Text, vSalesInvcNo, GiftReceiptMessage, Nothing, Nothing, dtPrinterInfo, ShowFullName:=clsDefaultConfiguration.PrintItemFullName)

                'Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, vSalesInvcNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo1.CtrlTxtCustOrdRef.Text)

            Else

                Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.GiftVoucherDocumentPrint, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, vSalesInvcNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo.CtrlTxtCustOrdRef.Text, vSalesInvcNo, GiftReceiptMessage, Nothing, Nothing, dtPrinterInfo, ShowFullName:=clsDefaultConfiguration.PrintItemFullName)

                'Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, vSalesInvcNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), Nothing, CtrlSalesInfo1.CtrlTxtCustOrdRef.Text)


            End If

        Catch ex As Exception

        End Try

    End Function

    ''' <summary>
    ''' Preapring the Sales Order Header data for save
    ''' </summary>
    ''' <param name="dsMain"></param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PrepareHdrDataforSave(ByRef dsMain As DataSet) As Boolean
        Dim drSOHdr As DataRow
        Dim findKey(2) As Object

        Try

            findKey(0) = vSiteCode
            findKey(1) = vfinancialYear
            findKey(2) = CtrlSalesInfo.CtrlTxtOrderNo.Text

            drSOHdr = _dsMain.Tables("SalesOrderHDR").Rows.Find(findKey)
            If Not (drSOHdr Is Nothing) Then

                drSOHdr("SaleOrderNumber") = CtrlSalesInfo.CtrlTxtOrderNo.Text

                drSOHdr("SiteCode") = vSiteCode
                drSOHdr("FinYear") = vfinancialYear
                drSOHdr("TerminalID") = vTerminalID
                drSOHdr("TransactionCode") = vDocType

                drSOHdr("NetAmt") = CDbl(CtrlCashSummary1.lbltxt4.Trim)
                drSOHdr("CostAmt") = CDbl(CtrlCashSummary1.lbltxt4.Trim)
                drSOHdr("GrossAmt") = CDbl(CtrlCashSummary1.lbltxt1.Trim)

                If Not (dsPayment Is Nothing) AndAlso dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables(0).Rows.Count > 0 Then
                    drSOHdr("AdvanceAmt") = CType(IIf(drSOHdr("AdvanceAmt") Is DBNull.Value, 0, drSOHdr("AdvanceAmt")), Double) + CType(dsPayment.Tables(0).Compute("Sum(Amount)", ""), Double)
                Else
                    drSOHdr("AdvanceAmt") = drSOHdr("AdvanceAmt")
                End If

                drSOHdr("DiscountPercentage") = FormatNumber(CDbl(dsScan.Tables(0).Compute("Sum(Discount)", "") / (grdSOItems.Rows.Count - 1)), 2)
                drSOHdr("DiscountAmt") = CDbl(CtrlCashSummary1.lbltxt2.Trim)
                drSOHdr("TotalDiscount") = CDbl(CtrlCashSummary1.lbltxt2.Trim)
                drSOHdr("BalanceAmount") = Math.Round(CDbl(CtrlCashSummary1.lbltxt4.Trim) - CDbl(drSOHdr("AdvanceAmt")), 2)
                drSOHdr("RoundedAmt") = CDbl(CDbl(CtrlCashSummary1.lbltxt4.Trim))

                drSOHdr("LineItems") = CType(grdSOItems.Rows.Count - 1, Integer)
                drSOHdr("CreditNoteNo") = DBNull.Value
                drSOHdr("TransCurrency") = vCurrencyDescription
                drSOHdr("PostingStatus") = DBNull.Value

                drSOHdr("ExchangeRate") = 0
                drSOHdr("CurrencyCode") = vCurrencyCode
                If Not dsScan.Tables(0).Compute("Sum(TotalTaxAmt)", "IsStatus<>'Deleted'") Is DBNull.Value Then
                    drSOHdr("TaxAmount") = dsScan.Tables(0).Compute("Sum(TotalTaxAmt)", "IsStatus<>'Deleted'")
                End If

                If Not (dtOtherCharges Is Nothing) AndAlso dtOtherCharges.Rows.Count > 0 Then
                    drSOHdr("ServiceTaxAmount") = Math.Round(IIf(IsDBNull(drSOHdr("ServiceTaxAmount")), 0, drSOHdr("ServiceTaxAmount")) + IIf(dtOtherCharges.Compute("Sum(TaxAmt)", "") Is DBNull.Value, 0, dtOtherCharges.Compute("Sum(TaxAmt)", "")), 3)
                End If

                drSOHdr("NoofReprints") = drSOHdr("NoofReprints") + 1
                drSOHdr("ReprintReason") = "Changed"
                drSOHdr("ReprintDate") = vCurrentDate
                drSOHdr("ReprintTime") = Format(vCurrentDate, "hh:mm:ss tt") 'Format(Now, "dd-MM-yyyy hh:mm:ss tt")

                drSOHdr("DeletionDate") = DBNull.Value
                drSOHdr("DeletionTime") = DBNull.Value

                drSOHdr("IsExported") = 0
                drSOHdr("PromisedDeliveryDate") = CtrlSalesInfo.CtrlDtExpDelDate.Value
                drSOHdr("ActualDeliveryDate") = CtrlSalesInfo.CtrlDtExpDelDate.Value

                If CtrlCustDtls1.cboAddrType.SelectedValue = "" Then
                    drSOHdr("OtherDeliveryAdd") = CtrlCustDtls1.lblAddressValue.Text.Trim & "  " & CtrlCustDtls1.lblEmailIdValue.Text.Trim & "  " & CtrlCustDtls1.lblTelNoValue.Text.Trim

                    drSOHdr("DeliveryAtCustAddressType") = ""
                Else
                    drSOHdr("DeliveryAtCustAddressType") = CtrlCustDtls1.cboAddrType.SelectedValue
                End If

                If Val(lblOrderQty.Text) = Val(lblPickupQty.Text) + Val(lbldeliveredqty.Text) Then
                    drSOHdr("SOStatus") = "Closed"
                    If Not dsScan.Tables("ItemScanDetails").Compute("Sum(CLPPoints)", "(PickUpQty>0 Or DeliveredQty>0) And IsCLP='True'") Is DBNull.Value Then
                        drSOHdr("CLPPoints") = dsScan.Tables("ItemScanDetails").Compute("Sum(CLPPoints)", "(PickUpQty>0 Or DeliveredQty>0) And IsCLP='True'")
                    End If
                    If Not dsScan.Tables("ItemScanDetails").Compute("Sum(CLPDiscount)", "(PickUpQty>0 Or DeliveredQty>0) And IsCLP='True'") Is DBNull.Value Then
                        drSOHdr("CLPDiscount") = dsScan.Tables("ItemScanDetails").Compute("Sum(CLPDiscount)", "(PickUpQty>0 Or DeliveredQty>0) And IsCLP='True'")
                    End If
                Else
                    drSOHdr("SOStatus") = "Open"
                    drSOHdr("CLPPoints") = 0
                    drSOHdr("CLPDiscount") = 0
                End If

                drSOHdr("BalancePoints") = DBNull.Value

                drSOHdr("CustomerOrderRef") = CtrlSalesInfo.CtrlTxtCustOrdRef.Text
                drSOHdr("Remarks") = CtrlSalesInfo.CtrlTxtRemarks.Text.Trim

                vAmendedNo = drSOHdr("AmendedNo")
                drSOHdr("AmendedNo") = vAmendedNo + 1

                drSOHdr("UPDATEDAT") = vSiteCode
                drSOHdr("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                drSOHdr("UPDATEDON") = vCurrentDate

                drSOHdr("STATUS") = True
            End If

            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    ''' <summary>
    ''' Preapring the Sales Order Details data for save
    ''' </summary>
    ''' <param name="dsMain"></param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PrepareDtlDataforSave(ByRef dsMain As DataSet, Optional ByVal Pickall As Boolean = False) As Boolean
        Dim IsAddNewRow As Boolean = False
        Dim drSODtl As DataRow
        Dim findKey(4) As Object
        Dim MaxBilllineno As Integer = 0
        If IsDBNull(dsMain.Tables("SalesOrderDTL").Compute("Max(BillLineNo)", "")) Then
            MaxBilllineno = 0
        Else
            MaxBilllineno = dsMain.Tables("SalesOrderDTL").Compute("Max(BillLineNo)", "")
        End If
        ''---- Code Added By Mahesh for Bulk Combo ----
        If Not dsMain.Tables("SalesOrderDTL").Columns.Contains("RowIndex") Then
            dsMain.Tables("SalesOrderDTL").Columns.Add("RowIndex", Type.GetType("System.Int16"))
        End If
        Try
            'For Each drScan As DataRow In dsScan.Tables("ItemScanDetails").Select("IsStatus In ('Deleted','Updated') ")
            For Each drScan As DataRow In dsScan.Tables("ItemScanDetails").Rows
                Try
                    Dim billLineNo As Integer
                    If IsDBNull(drScan("BillLineNo")) = False Then
                        billLineNo = drScan("BillLineNo")
                    Else
                        MaxBilllineno = MaxBilllineno + 1
                        billLineNo = MaxBilllineno 'dsMain.Tables("SalesOrderDTL").Rows.Count + 1
                    End If
                    findKey(0) = vSiteCode
                    findKey(1) = vfinancialYear
                    findKey(2) = CtrlSalesInfo.CtrlTxtOrderNo.Text
                    findKey(3) = drScan("EAN").ToString
                    findKey(4) = billLineNo
                    drSODtl = dsMain.Tables("SalesOrderDTL").Rows.Find(findKey)

                    If drSODtl Is Nothing Then
                        drSODtl = dsMain.Tables("SalesOrderDTL").NewRow
                        drSODtl("STATUS") = True
                        IsAddNewRow = True
                    Else
                        If drScan("IsStatus") = "Deleted" Then
                            drSODtl("ArticleStatus") = "Deleted"
                            drSODtl("STATUS") = False
                        Else
                            drSODtl("STATUS") = True
                            drSODtl("ArticleStatus") = "Open"
                        End If
                    End If
                    If Pickall = True Then
                        drScan("PickUpQty") = drScan("Quantity") - IIf(drScan("DeliveredQty") Is DBNull.Value, 0, drScan("DeliveredQty"))
                    End If
                    If Val(drScan("DeliveredQty")) + Val(drScan("PickUpQty")) = Val(drScan("Quantity")) Then
                        drSODtl("ArticleStatus") = "Closed"
                    End If
                    drSODtl("BillLineNo") = billLineNo
                    drScan("BillLineNo") = billLineNo
                    drSODtl("RowIndex") = drScan("RowIndex")
                    drSODtl("SaleOrderNumber") = CtrlSalesInfo.CtrlTxtOrderNo.Text
                    drSODtl("SiteCode") = vSiteCode
                    drSODtl("FinYear") = vfinancialYear
                    drSODtl("EAN") = drScan("EAN")
                    drSODtl("ArticleCode") = drScan("ArticleCode")
                    drSODtl("BatchBarcode") = drScan("BatchBarcode")
                    drSODtl("PromisedDeliveryDate") = drScan("ExpDelDate")
                    drSODtl("ActualDeliveryDate") = drScan("ExpDelDate")

                    drSODtl("SellingPrice") = drScan("SellingPrice")
                    drSODtl("Quantity") = drScan("Quantity")

                    If drSODtl("Quantity") = drScan("PickUpQty") + drScan("DeliveredQty") Then
                        drSODtl("Delivered_Qty") = drSODtl("Quantity") - IIf(drSODtl("DeliveredQty") Is DBNull.Value, 0, drSODtl("DeliveredQty"))
                        drSODtl("DeliveredQty") = drSODtl("Quantity")
                        drSODtl("ArticleStatus") = "Close"
                    Else
                        If Not (drScan("PickUpQty") Is DBNull.Value) AndAlso drScan("PickUpQty") > 0 Then
                            drSODtl("DeliveredQty") = IIf(drScan("PickUpQty") = 0, drScan("DeliveredQty"), drScan("DeliveredQty") + drScan("PickUpQty"))
                            drSODtl("Delivered_Qty") = drScan("PickUpQty")
                            'drSODtl("Delivered_Qty") = IIf(drScan("PickUpQty") = 0, drScan("DeliveredQty"), drScan("DeliveredQty") + drScan("PickUpQty"))
                        Else
                            drSODtl("DeliveredQty") = drScan("DeliveredQty")
                            drSODtl("Delivered_Qty") = drScan("DeliveredQty")
                        End If
                    End If
                    If drScan("Reserved") Is DBNull.Value Then
                        If Not drScan("ReservedQty") Is DBNull.Value AndAlso drScan("ReservedQty") = True Then
                            drSODtl("ReservedQty") = drScan("Quantity")
                            drSODtl("Reserved_Qty") = drScan("Quantity")
                        End If
                    ElseIf drScan("Reserved") = drScan("ReservedQty") Then
                        If Not drScan("ReservedQty") Is DBNull.Value AndAlso drScan("ReservedQty") = True Then
                            drSODtl("ReservedQty") = IIf(drSODtl("ReservedQty") Is DBNull.Value, 0, drSODtl("ReservedQty")) + drScan("PickUpQty") * -1
                            drSODtl("Reserved_Qty") = drScan("PickUpQty") * -1
                        End If
                    Else
                        If Not drScan("ReservedQty") Is DBNull.Value AndAlso drScan("ReservedQty") = True Then
                            drSODtl("ReservedQty") = IIf(drSODtl("ReservedQty") Is DBNull.Value, 0, drSODtl("ReservedQty")) + CDbl(drScan("Quantity")) - CDbl(drScan("PickUpQty") + drScan("DeliveredQty"))
                            drSODtl("Reserved_Qty") = CDbl(drScan("Quantity")) - CDbl(drScan("PickUpQty") + drScan("DeliveredQty"))
                        End If
                        If Not drScan("ReservedQty") Is DBNull.Value AndAlso drScan("ReservedQty") = False Then
                            drSODtl("ReservedQty") = IIf(drSODtl("ReservedQty") Is DBNull.Value, 0, drSODtl("ReservedQty")) + (drScan("Quantity") - drScan("DeliveredQty")) * -1
                            drSODtl("Reserved_Qty") = (drScan("Quantity") - drScan("DeliveredQty")) * -1
                        End If
                    End If


                    'If drScan("ReservedQty") > 0 AndAlso drScan("Quantity") > drScan("PickUpQty") + drScan("DeliveredQty") Then
                    '    drSODtl("ReservedQty") = CDbl(drScan("Quantity")) - CDbl(drScan("PickUpQty") + drScan("DeliveredQty"))
                    'Else
                    '    drSODtl("ReservedQty") = 0
                    'End If

                    drSODtl("CostAmount") = drScan("CostAmount") 'DBNull.Value
                    drSODtl("GrossAmount") = Math.Round(drScan("GrossAmt"), 3)
                    drSODtl("NetAmount") = Math.Round(drScan("NetAmount"), 3)

                    drSODtl("OfferNo") = IIf(drScan("PromotionId") = "0,0", 0, drScan("PromotionId"))
                    drSODtl("Section") = DBNull.Value
                    drSODtl("Shelf") = DBNull.Value

                    drSODtl("TransactionCode") = vDocType
                    drSODtl("IsCLPApplicable") = drScan("IsCLP")
                    drSODtl("CLPPoints") = CDbl(drScan("CLPPoints"))
                    drSODtl("CLPDiscount") = CDbl(drScan("CLPDiscount"))

                    drSODtl("LineDiscount") = IIf(drScan("LineDiscount") Is DBNull.Value, 0, drScan("LineDiscount"))
                    drSODtl("CLPDiscount") = DBNull.Value
                    drSODtl("DiscountAmount") = CDbl(drScan("LineDiscount")) + CDbl(IIf(drSODtl("CLPDiscount") Is DBNull.Value, 0, drSODtl("CLPDiscount")))
                    drSODtl("DiscountPercentage") = drScan("TotalDiscPercentage")

                    drSODtl("UnitofMeasure") = IIf(drScan("UOM") Is DBNull.Value, 0, drScan("UOM"))
                    drSODtl("ExclTaxAmt") = drScan("ExclTaxAmt")
                    drSODtl("TotalTaxAmount") = Math.Round(IIf(drScan("TotalTaxAmt") Is DBNull.Value, 0, drScan("TotalTaxAmt")), 3)

                    drSODtl("IFBNo") = DBNull.Value
                    drSODtl("SalesReturnReasonCode") = DBNull.Value
                    drSODtl("ReturnSaleOrderNo") = DBNull.Value
                    drSODtl("ReturnSaleOrderDt") = DBNull.Value
                    drSODtl("ReturnQty") = DBNull.Value
                    drSODtl("ScaleItem") = DBNull.Value
                    drSODtl("ReturnNoSale") = DBNull.Value
                    drSODtl("SerialNo") = DBNull.Value
                    drSODtl("SerialNbrNotValid") = DBNull.Value

                    drSODtl("FIRSTLEVELDISC") = CDbl(IIf(drScan("FIRSTLEVELDISC") Is DBNull.Value, 0, drScan("FIRSTLEVELDISC")))
                    drSODtl("TOPLEVELDISC") = CDbl(IIf(drScan("TOPLEVELDISC") Is DBNull.Value, 0, drScan("TOPLEVELDISC")))
                    drSODtl("FIRSTLEVEL") = IIf(drScan("FIRSTLEVEL") Is DBNull.Value, "", drScan("FIRSTLEVEL"))
                    drSODtl("TopLevel") = IIf(drScan("TopLevel") Is DBNull.Value, "", drScan("TopLevel"))
                    If drScan("SalesStaffID") Is DBNull.Value Or drScan("SalesStaffID").ToString() = "" Then
                        drSODtl("SalesStaffID") = IIf(CtrlSalesPerson.CtrlSalesPersons.SelectedIndex < 0, "", CtrlSalesPerson.CtrlSalesPersons.SelectedValue)
                    End If
                    drSODtl("UPDATEDAT") = vSiteCode
                    drSODtl("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                    drSODtl("UPDATEDON") = vCurrentDate

                    If IsAddNewRow = True Then
                        drSODtl("CREATEDAT") = vSiteCode
                        drSODtl("CREATEDBY") = clsAdmin.UserCode 'vUserName
                        drSODtl("CREATEDON") = vCurrentDate
                        drSODtl("DeliverySiteCode") = drScan("DeliverySiteCode")
                        dsMain.Tables("SalesOrderDTL").Rows.Add(drSODtl)
                    End If

                Catch ex As Exception
                    ShowMessage(ex.Message, getValueByKey("CLAE05"))
                End Try
            Next
            '-----deleted data code for reversing reserve Qty---------
            Dim dvDeleted As New DataView(dsScan.Tables("ItemScanDetails"), "IsStatus='Deleted'", "", DataViewRowState.CurrentRows)
            If dvDeleted.Count > 0 Then
                DtDeletedData = dvDeleted.ToTable().Copy()
            End If
            '---------end here for reserve-----------
            ' to set the costprice 
            SetCostPrice(clsDefaultConfiguration.isMAPbasedCost, dsMain.Tables("SalesOrderDTL"), clsAdmin.SiteCode, "CostAmount", clsDefaultConfiguration.IsBatchManagementReq)
            ' to set the costprice 

            Return True
        Catch ex As Exception
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
            '---Add by rama, so that no old data updated once again
            dsMain.Tables("SalesInvoice").Clear()
            '---
            If Not (dsPayment Is Nothing) AndAlso dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                'vSalesInvcNo = "CM" & clsAdmin.TerminalID & objComn.getDocumentNo("CM")

                If OnlineConnect = True Then
                    'Changed by Rohit to generate Document No. for proper sorting
                    Try
                        vSalesInvcNo = "CM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2)
                        vSalesInvcNo = GenDocNo(vSalesInvcNo, 15, objComn.getDocumentNo("CM", clsAdmin.SiteCode))
                    Catch ex As Exception
                        vSalesInvcNo = "CM" & clsAdmin.TerminalID & objComn.getDocumentNo("CM", clsAdmin.SiteCode)
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
                        vSalesInvcNo = "OCM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2)
                        vSalesInvcNo = GenDocNo(vSalesInvcNo, 15, objComn.getDocumentNo("CM", clsAdmin.SiteCode))
                    Catch ex As Exception
                        vSalesInvcNo = "OCM" & clsAdmin.TerminalID & objComn.getDocumentNo("CM", clsAdmin.SiteCode)
                    End Try

                    Try
                        rbnLStatus.SmallImage = Spectrum.My.Resources.disconnected1.ToBitmap
                        rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus1")
                    Catch ex As Exception

                    End Try
                    'End Change by Rohit
                End If


                Dim findKey(4) As Object
                Dim drInvc As DataRow

                For Each drPayment As DataRow In dsPayment.Tables("MSTRecieptType").Rows

                    findKey(0) = vSiteCode
                    findKey(1) = vfinancialYear
                    findKey(2) = CtrlSalesInfo.CtrlTxtOrderNo.Text
                    findKey(3) = vSalesInvcNo
                    findKey(4) = drPayment("SrNo")

                    drInvc = dsMain.Tables("SalesInvoice").Rows.Find(findKey)
                    If drInvc Is Nothing Then
                        drInvc = dsMain.Tables("SalesInvoice").NewRow()
                        IsNewRow = True
                        IsNextInvoiceNo = True
                    End If

                    drInvc("SiteCode") = vSiteCode
                    If IsNewRow = False Then
                        drInvc("FinYear") = vfinancialYear
                    Else
                        drInvc("FinYear") = clsAdmin.Financialyear
                    End If

                    drInvc("DocumentNumber") = CtrlSalesInfo.CtrlTxtOrderNo.Text
                    drInvc("SaleInvNumber") = vSalesInvcNo
                    drInvc("SaleInvLineNumber") = drPayment("SrNo")

                    drInvc("DocumentType") = vDocType
                    drInvc("TerminalID") = vTerminalID
                    drInvc("TenderTypeCode") = drPayment("RecieptTypeCode")
                    drInvc("TenderHeadCode") = drPayment("RecieptType")
                    drInvc("BankAccNo") = drPayment("BankAccNo")
                    ' this amount has to go negative as it is coming negative from payment form so not required below if condidation

                    'If drInvc("TenderTypeCode") = "CreditVouc(I)" Or drInvc("TenderTypeCode") = "Cash(R)" Then
                    '    drInvc("AmountTendered") = CDbl(drPayment("Amount") * -1)
                    'Else
                    '    drInvc("AmountTendered") = drPayment("Amount")
                    'End If

                    drInvc("AmountTendered") = drPayment("Amount")
                    drInvc("ExchangeRate") = drPayment("ExchangeRate")
                    drInvc("CurrencyCode") = drPayment("CurrencyCode")
                    drInvc("SOInvDate") = clsAdmin.DayOpenDate.Date ' vCurrentDate
                    drInvc("SOInvTime") = Format(vCurrentDate, "hh:mm:ss tt")
                    drInvc("UserName") = vUserName

                    drInvc("ManagersKeytoUpdate") = DBNull.Value
                    drInvc("ChangeLine") = DBNull.Value
                    drInvc("RefNo_1") = clsCommon.ConvertToEnglish(IIf(drPayment("AMOUNTINCURRENCY") Is DBNull.Value, 0, drPayment("AMOUNTINCURRENCY"))) 'drPayment("Number")
                    drInvc("RefNo_2") = drPayment("Number")
                    drInvc("RefNo_3") = DBNull.Value
                    drInvc("RefNo_4") = DBNull.Value
                    ' change to get the cv/gv old redemeeption date
                    'drInvc("RefDate") = vCurrentDate
                    drInvc("RefDate") = drPayment("date")

                    drInvc("CREATEDAT") = vSiteCode
                    drInvc("CREATEDBY") = clsAdmin.UserCode 'vUserName
                    drInvc("CREATEDON") = vCurrentDate
                    drInvc("UPDATEDAT") = vSiteCode
                    drInvc("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                    drInvc("UPDATEDON") = vCurrentDate
                    drInvc("STATUS") = True

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

            objComn.PrepareCreditCheckData(dsMain, vSiteCode, vUserName, clsAdmin.Financialyear, vDocType, vSalesInvcNo, CtrlSalesInfo.CtrlTxtOrderNo.Text, vCurrentDate, _dDueDate, _strRemarks, "SO", clsAdmin.DayOpenDate)
            objComn.AddMode(dsMain.Tables("CheckDtls"))

            Return True

        Catch ex As Exception
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
            dtOtherCharges.TableName = "SalesOrderOtherCharges"
            If dsMain.Tables.Contains("Table1") Then
                dsMain.Tables("Table1").TableName = "SalesOrderOtherCharges"
            End If
            If dsMain.Tables.Contains("SalesOrderOtherCharges") Then
                dsMain.Tables.Remove("SalesOrderOtherCharges")
                dsMain.Tables.Add(dtOtherCharges)
                TempOtherChargesTable = dtOtherCharges.Copy()
                For Each dr As DataRow In dsMain.Tables("SalesOrderOtherCharges").Rows
                    If dr.RowState <> DataRowState.Deleted Then
                        dr("CREATEDAT") = vSiteCode
                        dr("CREATEDBY") = clsAdmin.UserCode 'vUserName
                        dr("CREATEDON") = vCurrentDate
                        dr("STATUS") = True
                        dr("UpdatedAT") = vSiteCode
                        dr("UpdatedBY") = clsAdmin.UserCode 'vUserName
                        dr("UpdatedON") = vCurrentDate
                        dr("STATUS") = True
                    End If
                Next
            End If
            'If Not dsMain.Tables("SalesOrderOtherCharges") Is Nothing AndAlso dsMain.Tables("SalesOrderOtherCharges").Rows.Count <= 0 Then
            '    dsMain.Tables.Remove("SalesOrderOtherCharges")
            'End If
            Return True
        Catch ex As Exception
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
            Dim vOBNumber As String = String.Empty
            'vOBNumber = "OB" & clsAdmin.TerminalID & clsCommon.getDocumentNo("OutBound")            

            If OnlineConnect = True Then
                'Changed by Rohit to generate Document No. for proper sorting
                Try
                    vOBNumber = "OB" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2)
                    vOBNumber = GenDocNo(vOBNumber, 15, clsCommon.getDocumentNo("OutBound", clsAdmin.SiteCode))
                Catch ex As Exception
                    vOBNumber = "OB" & clsAdmin.TerminalID & clsCommon.getDocumentNo("OutBound", clsAdmin.SiteCode)
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
                    vOBNumber = "OOB" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2)
                    vOBNumber = GenDocNo(vOBNumber, 15, clsCommon.getDocumentNo("OutBound", clsAdmin.SiteCode))
                Catch ex As Exception
                    vOBNumber = "OOB" & clsAdmin.TerminalID & clsCommon.getDocumentNo("OutBound", clsAdmin.SiteCode)
                End Try

                Try
                    rbnLStatus.SmallImage = Spectrum.My.Resources.disconnected1.ToBitmap
                    rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus1")
                Catch ex As Exception

                End Try
                'End Change by Rohit
            End If

            findKey(0) = vSiteCode
            findKey(1) = vfinancialYear
            findKey(2) = vOBNumber

            drOrderHdr = dsMain.Tables("OrderHdr").Rows.Find(findKey)
            If drOrderHdr Is Nothing Then
                drOrderHdr = dsMain.Tables("OrderHdr").NewRow
                IsNewRow = True
            End If

            drOrderHdr("SiteCode") = vSiteCode
            If IsNewRow = False Then
                drOrderHdr("FinYear") = vfinancialYear
            Else
                drOrderHdr("FinYear") = clsAdmin.Financialyear
            End If

            drOrderHdr("DocumentNumber") = vOBNumber
            drOrderHdr("DocumentType") = vDocType
            drOrderHdr("DocDate") = vCurrentDate
            'drOrderHdr("SupplierCode") = clsDefaultConfiguration.SupplierCode
            drOrderHdr("PurchaseGroupCode") = DBNull.Value
            drOrderHdr("DeliverySiteCode") = vSiteCode
            drOrderHdr("ExpectedDeliveryDt") = CtrlSalesInfo.CtrlDtExpDelDate.Value

            drOrderHdr("PaymentAfterDelDays") = DBNull.Value
            drOrderHdr("AdditionalTermsNConditions") = DBNull.Value
            drOrderHdr("AdditionalPaymentTerms") = DBNull.Value
            drOrderHdr("DocNetValue") = CDbl(CtrlCashSummary1.lbltxt4)
            drOrderHdr("DocGrossValue") = CDbl(CtrlCashSummary1.lbltxt1.Trim)
            drOrderHdr("IsClosed") = True
            drOrderHdr("IsFreezed") = True
            drOrderHdr("SalesOrderRef") = CtrlSalesInfo.CtrlTxtOrderNo.Text
            drOrderHdr("ReturnReasonCode") = " "
            drOrderHdr("Remarks") = CtrlSalesInfo.CtrlTxtRemarks.Text.Trim
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
            drOrderHdr("CREATEDON") = vCurrentDate
            drOrderHdr("UPDATEDAT") = vSiteCode
            drOrderHdr("UPDATEDBY") = clsAdmin.UserCode 'vUserName
            drOrderHdr("UPDATEDON") = vCurrentDate
            drOrderHdr("STATUS") = True
            drOrderHdr("SupplierCode") = " "

            If IsNewRow = True Then
                dsMain.Tables("OrderHdr").Rows.Add(drOrderHdr)
                IsNewRow = False
            End If

            Return True
        Catch ex As Exception
            IsNewRow = False
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
            If clsDefaultConfiguration.IsBatchManagementReq Then
                For Each barcode As SpectrumCommon.BtachbarcodeInfo In Batchbarcode
                    Dim drScan = dsScan.Tables(0).Select("EAN='" & barcode.EAN & "'")(0)
                    findKey(0) = vSiteCode
                    findKey(1) = vfinancialYear
                    findKey(2) = dsMain.Tables("OrderHdr").Rows(0)("DocumentNumber")
                    findKey(3) = drScan("EAN")
                    findKey(4) = rowIndex

                    drOrderDtl = dsMain.Tables("OrderDtl").Rows.Find(findKey)
                    If drOrderDtl Is Nothing Then
                        drOrderDtl = dsMain.Tables("OrderDtl").NewRow
                        IsNewRow = True
                    End If

                    drOrderDtl("SiteCode") = vSiteCode

                    If IsNewRow = False Then
                        drOrderDtl("FinYear") = vfinancialYear
                    Else
                        drOrderDtl("FinYear") = clsAdmin.Financialyear
                    End If

                    drOrderDtl("DocumentNumber") = dsMain.Tables("OrderHdr").Rows(0)("DocumentNumber")
                    drOrderDtl("ArticleCode") = drScan("ArticleCode")
                    drOrderDtl("EAN") = drScan("EAN")

                    drOrderDtl("Qty") = drScan("Quantity")
                    drOrderDtl("UnitofMeasure") = vUOM
                    drOrderDtl("LineNumber") = rowIndex
                    drOrderDtl("OpenQty") = DBNull.Value
                    drOrderDtl("DeliveredQty") = barcode.Qty
                    drOrderDtl("DeliveryCompleted") = DBNull.Value
                    drOrderDtl("PurchaseGroupCode") = DBNull.Value
                    drOrderDtl("RefDocument") = DBNull.Value
                    drOrderDtl("RefDocumentNo") = DBNull.Value
                    drOrderDtl("BarCode") = barcode.BatchBarcode
                    drOrderDtl("PONo") = DBNull.Value
                    drOrderDtl("BirthListId") = DBNull.Value
                    drOrderDtl("SaleOrderNumber") = CtrlSalesInfo.CtrlTxtOrderNo.Text
                    drOrderDtl("AllocationRule") = DBNull.Value
                    'drOrderDtl("MRP") = drScan("SellingPrice")
                    drOrderDtl("MRP") = Math.Round(drScan("NetAmount") / drScan("Quantity"), clsDefaultConfiguration.DecimalPlaces)
                    drOrderDtl("CostPrice") = drScan("SellingPrice")
                    drOrderDtl("NetCostPrice") = drScan("SellingPrice")

                    drOrderDtl("ExciseDutyAmt") = DBNull.Value
                    drOrderDtl("ExciseDutyRate") = DBNull.Value
                    drOrderDtl("PurchaseTaxAmt") = DBNull.Value
                    drOrderDtl("PurchaseTaxRate") = DBNull.Value
                    drOrderDtl("AdditionalChargeAmt") = DBNull.Value
                    drOrderDtl("DiscountAmount") = drScan("LineDiscount")
                    drOrderDtl("AdditionalChargeRate") = DBNull.Value  'drScan("ExclTaxAmt")
                    drOrderDtl("DocValue") = DBNull.Value
                    drOrderDtl("InboundQty") = DBNull.Value
                    drOrderDtl("DayOpenDate") = clsAdmin.DayOpenDate

                    drOrderDtl("CREATEDAT") = vSiteCode
                    drOrderDtl("CREATEDBY") = clsAdmin.UserCode 'vUserName
                    drOrderDtl("CREATEDON") = vCurrentDate
                    drOrderDtl("UPDATEDAT") = vSiteCode
                    drOrderDtl("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                    drOrderDtl("UPDATEDON") = vCurrentDate
                    drOrderDtl("STATUS") = True

                    If IsNewRow = True Then
                        dsMain.Tables("OrderDtl").Rows.Add(drOrderDtl)
                        IsNewRow = False
                    End If
                    rowIndex = rowIndex + 1
                    'End If
                Next
            Else
                For Each drScan As DataRow In dsScan.Tables(0).Select("PickUpQty>0", "", DataViewRowState.CurrentRows)
                    'If drScan("PickUpQty") > 0 Then

                    findKey(0) = vSiteCode
                    findKey(1) = vfinancialYear
                    findKey(2) = dsMain.Tables("OrderHdr").Rows(0)("DocumentNumber")
                    findKey(3) = drScan("EAN")
                    findKey(4) = rowIndex

                    drOrderDtl = dsMain.Tables("OrderDtl").Rows.Find(findKey)
                    If drOrderDtl Is Nothing Then
                        drOrderDtl = dsMain.Tables("OrderDtl").NewRow
                        IsNewRow = True
                    End If

                    drOrderDtl("SiteCode") = vSiteCode

                    If IsNewRow = False Then
                        drOrderDtl("FinYear") = vfinancialYear
                    Else
                        drOrderDtl("FinYear") = clsAdmin.Financialyear
                    End If

                    drOrderDtl("DocumentNumber") = dsMain.Tables("OrderHdr").Rows(0)("DocumentNumber")
                    drOrderDtl("ArticleCode") = drScan("ArticleCode")
                    drOrderDtl("EAN") = drScan("EAN")

                    drOrderDtl("Qty") = drScan("Quantity")
                    drOrderDtl("UnitofMeasure") = vUOM
                    drOrderDtl("LineNumber") = rowIndex
                    drOrderDtl("OpenQty") = DBNull.Value
                    drOrderDtl("DeliveredQty") = drScan("PickUpQty")
                    drOrderDtl("DeliveryCompleted") = DBNull.Value
                    drOrderDtl("PurchaseGroupCode") = DBNull.Value
                    drOrderDtl("RefDocument") = DBNull.Value
                    drOrderDtl("RefDocumentNo") = DBNull.Value
                    drOrderDtl("BarCode") = drScan("BatchBarCode")
                    drOrderDtl("PONo") = DBNull.Value
                    drOrderDtl("BirthListId") = DBNull.Value
                    drOrderDtl("SaleOrderNumber") = CtrlSalesInfo.CtrlTxtOrderNo.Text
                    drOrderDtl("AllocationRule") = DBNull.Value
                    'drOrderDtl("MRP") = drScan("SellingPrice")
                    drOrderDtl("MRP") = Math.Round(drScan("NetAmount") / drScan("Quantity"), clsDefaultConfiguration.DecimalPlaces)
                    drOrderDtl("CostPrice") = drScan("SellingPrice")
                    drOrderDtl("NetCostPrice") = drScan("SellingPrice")

                    drOrderDtl("ExciseDutyAmt") = DBNull.Value
                    drOrderDtl("ExciseDutyRate") = DBNull.Value
                    drOrderDtl("PurchaseTaxAmt") = DBNull.Value
                    drOrderDtl("PurchaseTaxRate") = DBNull.Value
                    drOrderDtl("AdditionalChargeAmt") = DBNull.Value
                    drOrderDtl("DiscountAmount") = drScan("LineDiscount")
                    drOrderDtl("AdditionalChargeRate") = DBNull.Value  'drScan("ExclTaxAmt")
                    drOrderDtl("DocValue") = DBNull.Value
                    drOrderDtl("InboundQty") = DBNull.Value
                    drOrderDtl("DayOpenDate") = clsAdmin.DayOpenDate

                    drOrderDtl("CREATEDAT") = vSiteCode
                    drOrderDtl("CREATEDBY") = clsAdmin.UserCode 'vUserName
                    drOrderDtl("CREATEDON") = vCurrentDate
                    drOrderDtl("UPDATEDAT") = vSiteCode
                    drOrderDtl("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                    drOrderDtl("UPDATEDON") = vCurrentDate
                    drOrderDtl("STATUS") = True

                    If IsNewRow = True Then
                        dsMain.Tables("OrderDtl").Rows.Add(drOrderDtl)
                        IsNewRow = False
                    End If
                    rowIndex = rowIndex + 1
                    'End If
                Next
            End If


            Return True
        Catch ex As Exception
            IsNewRow = False
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    Private Function PrepareClpHdrDataforSave(ByRef dsMain As DataSet) As Boolean
        Dim drClpHdr As DataRow
        Try
            drClpHdr = dsMain.Tables("CLPTransaction").NewRow

            drClpHdr("SiteCode") = vSiteCode
            drClpHdr("BillNo") = CtrlSalesInfo.CtrlTxtOrderNo.Text
            drClpHdr("BillDate") = vCurrentDate.Date
            drClpHdr("AccumLationPoints") = TotalPoints
            drClpHdr("RedemptionPoints") = 0
            drClpHdr("BalAccumlationPoints") = TotalPoints
            drClpHdr("ClpProgramId") = clsAdmin.CLPProgram
            drClpHdr("ClpCustomerId") = CtrlCustDtls1.lblCustNoValue.Text
            drClpHdr("IsRedemptionProcess") = False
            drClpHdr("CREATEDAT") = vSiteCode
            drClpHdr("CREATEDBY") = clsAdmin.UserCode 'vUserName
            drClpHdr("CREATEDON") = vCurrentDate
            drClpHdr("UPDATEDAT") = vSiteCode
            drClpHdr("UPDATEDBY") = clsAdmin.UserCode 'vUserName
            drClpHdr("UPDATEDON") = vCurrentDate
            drClpHdr("STATUS") = True

            dsMain.Tables("CLPTransaction").Rows.Add(drClpHdr)
            Return True

        Catch ex As Exception
            IsNewRow = False
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    Private Function PrepareClpDtlDataforSave(ByRef dsMain As DataSet) As Boolean
        Dim drClpDtl As DataRow
        Try
            Dim vRowNo As Integer = 1

            For Each drCLP As DataRow In dsScan.Tables(0).Select("(PickUpQty>0 Or DeliveredQty>0) And IsCLP='True'")
                drClpDtl = dsMain.Tables("CLPTransactionsDetails").NewRow

                drClpDtl("SiteCode") = vSiteCode
                drClpDtl("BillNo") = CtrlSalesInfo.CtrlTxtOrderNo.Text
                drClpDtl("BillLineNo") = vRowNo
                drClpDtl("EAN") = drCLP("EAN")
                drClpDtl("ArticleCode") = drCLP("ArticleCode")
                drClpDtl("SellingPrice") = drCLP("SellingPrice")
                drClpDtl("Quantity") = drCLP("Quantity")
                drClpDtl("CLPPoints") = drCLP("CLPPoints")
                drClpDtl("CLPDiscount") = drCLP("CLPDiscount")
                drClpDtl("CREATEDAT") = vSiteCode
                drClpDtl("CREATEDBY") = clsAdmin.UserCode 'vUserName
                drClpDtl("CREATEDON") = vCurrentDate
                drClpDtl("UPDATEDAT") = vSiteCode
                drClpDtl("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                drClpDtl("UPDATEDON") = vCurrentDate
                drClpDtl("STATUS") = True
                dsMain.Tables("CLPTransactionsDetails").Rows.Add(drClpDtl)

                vRowNo += 1
            Next

            Return True
        Catch ex As Exception
            IsNewRow = False
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function


    Private Function PrintSalesOrders(ByVal drSite As DataRow, ByVal drHAdds As DataRow, Optional ByVal drDAdds As DataRow = Nothing) As Boolean
        Try
            'Dim PrintSo As New System.Text.StringBuilder
            'If dsScan Is Nothing Then
            '    Exit Function
            'End If

            'PrintSo.Length = 0
            'PrintSo.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)
            'PrintSo.Append("                          SALES ORDER                                 " & vbCrLf)
            'PrintSo.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)
            'PrintSo.Append("Company Name   : " & "CreativeIT India Ltd" & "     Company Code : " & "CRITI02" & vbCrLf)

            'If vIsPrintOfficialAddressAllowed = False Then
            '    If Not (drSite Is Nothing) Then
            '        PrintSo.Append("Store Name         : " & drSite.Item("SiteOfficialName") & "                Store Code   : " & vSiteCode & vbCrLf)
            '        PrintSo.Append("Store Address       : " & drSite.Item("SiteAddressLn1") & vbCrLf & "                            " & _
            '                     drSite.Item("SiteAddressLn2") & " " & drSite.Item("SiteAddressLn3") & _
            '                     drSite.Item("SitePinCode") & vbCrLf)
            '    End If
            'Else
            '    PrintSo.Append(vbCrLf & "Print Official Address " & vbCrLf)
            'End If

            'If vHeaderNote = True Then
            '    PrintSo.Append(vbCrLf & "Header Information is Welcome" & vbCrLf)
            'End If

            'PrintSo.Append(vbCrLf & "----------------------------------------------------------------------------------------------------" & vbCrLf)
            'PrintSo.Append("Sales Order No              : " & CtrlSalesInfo.CtrlTxtOrderNo.Text & "                    Date    : " & Format(vCurrentDate, vDateFormat) & vbCrLf)
            'PrintSo.Append("Expected Delivery Date  : " & Format(CtrlSalesInfo.CtrlDtExpDelDate.Value, vDateFormat) & vbCrLf)
            'PrintSo.Append("Cashier Name                : " & vUserName & vbCrLf & vbCrLf)

            'PrintSo.Append("Customer Name    : " & CtrlCustDtls1.lblCustNameValue.Text & "          Customer Code : " & vCustomerNo & vbCrLf)
            'If Not drHAdds Is Nothing Then
            '    PrintSo.Append("Home Address       : " & drHAdds.Item("Address").ToString & vbCrLf)
            '    PrintSo.Append("                   : " & drHAdds.Item("City") & ", " & drHAdds.Item("State") & ", " & drHAdds.Item("Country") & ", " & drHAdds.Item("PinCode") & vbCrLf & vbCrLf)
            'End If

            'If drDelvAdds Is Nothing Then
            '    PrintSo.Append("Delivery Address : " & CtrlCustDtls1.lblAddressValue.Text)
            '    PrintSo.Append("Tel. No.  	   : " & CtrlCustDtls1.lblTelNoValue.Text & vbCrLf & vbCrLf)
            'Else
            '    PrintSo.Append("Delivery Address : " & drDAdds.Item("Address").ToString & vbCrLf)
            '    PrintSo.Append("                          : " & drDAdds.Item("City") & ", " & drDAdds.Item("State") & ", " & drDAdds.Item("Country") & ", " & drDAdds.Item("PinCode").ToString.Trim & vbCrLf)
            '    PrintSo.Append("Tel. No.  	             : " & drDAdds.Item("OfficeNo") & vbCrLf & vbCrLf)
            'End If

            'PrintSo.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)
            'PrintSo.Append("Item Code       Item Desc                               Qty       Price      Disc%  Tax%   NetAmt" & vbCrLf)
            'PrintSo.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)

            'For Each drDtl As DataRow In dsScan.Tables("ItemScanDetails").Select("IsStatus IN ('Inserted','Updated')")
            '    PrintSo.Append(drDtl("ArticleCode") & Space(16 - drDtl("ArticleCode").ToString.Length) & _
            '                  drDtl("Discription") & Space(40 - drDtl("Discription").ToString.Length) & _
            '                  drDtl("Quantity") & Space(10 - drDtl("Quantity").ToString.Length) & _
            '                  Format(drDtl("SellingPrice"), "0.0") & Space(10 - drDtl("SellingPrice").ToString.Length) & _
            '                  drDtl("Discount") & Space(10 - drDtl("Discount").ToString.Length) & _
            '                  drDtl("ExclTaxAmt") & Space(10 - drDtl("ExclTaxAmt").ToString.Length) & Format(drDtl("NetAmount"), "0.0") & vbCrLf)
            'Next

            'PrintSo.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)

            'PrintSo.Append("Total Qty    : " & lblOrderQty.Text & vbCrLf)
            'PrintSo.Append("Gross Amt  : " & CtrlCashSummary1.lbltxt1 & vbCrLf)
            'PrintSo.Append("Disc  Amt   : " & CtrlCashSummary1.lbltxt2 & vbCrLf)
            'PrintSo.Append("Incl. Amt   : " & Format(CDbl(dsScan.Tables("ItemScanDetails").Compute("Sum(IncTaxAmt)", ""))), "0.00" & vbCrLf)
            'PrintSo.Append("Excl. Amt   : " & Format(CDbl(dsScan.Tables("ItemScanDetails").Compute("Sum(ExclTaxAmt)", ""))), "0.00" & vbCrLf)
            'PrintSo.Append("Net   Amt   : " & CtrlCashSummary1.lbltxt4 & vbCrLf & vbCrLf)

            'If vIsPrintingTaxInfoAllowed = True Then
            '    PrintSo.Append("Print Tax Details..............." & vbCrLf & vbCrLf)
            'End If

            'PrintSo.Append("<Terms & Condition>" & vbCrLf & vbCrLf)

            'PrintSo.Append("Authorized Sign:...............            Customer Sign:................")

            'If vIsPromotionalMessageAllowed = True Then
            '    PrintSo.Append(vbCrLf & "Promotional Message is Welcome" & vbCrLf)
            'End If
            'If vFooterNote = True Then
            '    PrintSo.Append(vbCrLf & "Footer Information is Welcome" & vbCrLf)
            'End If

            'If vIsPrintPreviewAllowed = True Then
            '    fnPrint(PrintSo.ToString, "")          'Print Preview
            'End If
            'fnPrint(PrintSo.ToString, "PRN")   'Direct Print

            ''PrintSo.Append("")                 'Set Debug Point
            SalesPersonName = IIf(CtrlSalesPerson.CtrlSalesPersons.SelectedIndex < 0, "", CtrlSalesPerson.CtrlSalesPersons.Text)
            Dim dsOtherCharges As New DataSet
            dsOtherCharges.Clear()
            Dim dt As DataTable
            If dsMain.Tables.Contains("SalesOrderOtherCharges") Then
                dt = dsMain.Tables("SalesOrderOtherCharges").Copy()
            End If
            If Not dt Is Nothing AndAlso dt.Rows.Count = 0 AndAlso Not TempOtherChargesTable Is Nothing Then
                dt = TempOtherChargesTable.Copy()
            ElseIf dt Is Nothing Then
                dt = TempOtherChargesTable.Copy()
            End If
            dsOtherCharges.Tables.Add(dt)
            dt.TableName = "NewOtherCharges"
            Dim strRemark As String = CtrlSalesInfo.CtrlTxtRemarks.Text
            Dim strInvoiceTo As String = CtrlSalesInfo.CtrlTxtInvoice.Text

            'Dim dtOld As DataTable = dsMain.Tables("SalesOrderOtherCharges").Copy()
            'dsOtherCharges.Tables.Add(dtOld)
            Dim strSOStatus As String = ""


            '----Changes to show credit sale amount in sales invoice
            Dim crdsaleadjustamount As Double = 0
            Dim salesordernumber As String = ""
            If Not dsMain.Tables("SalesInvoice") Is Nothing AndAlso dsMain.Tables("SalesInvoice").Rows.Count > 0 Then
                For Each x In dsMain.Tables("SalesInvoice").Rows
                    salesordernumber = x("DocumentNumber")
                Next
            End If

            Dim crdsale As Double = 0
            Dim dtCreditSaleData As New DataTable
            Dim objclsReturn As New clsCashMemoReturn
            dtCreditSaleData = objclsReturn.getCreditSaleBillData("'" + salesordernumber + "'")
            If dtCreditSaleData.Rows.Count > 0 AndAlso Not dtCreditSaleData Is Nothing Then
                For Each y In dtCreditSaleData.Rows
                    crdsaleadjustamount = y("CreditSaleAdjustment")
                    crdsale = y("CreditSale")
                Next
            End If
            If crdsaleadjustamount > 0 Then
                crdsale = crdsale - crdsaleadjustamount
            End If

            'code added by irfan for cakeology on 4/10/2017
            Dim IsArticleWiseKot As Boolean = False
            Dim IsCounterCopy As Boolean = False

            If clsDefaultConfiguration.IsFinalReceipt.Contains(clsAdmin.TerminalID) Then
                IsFinalReceipt = True
            Else
                IsFinalReceipt = False
            End If

            If Not dsPayment Is Nothing AndAlso dsPayment.Tables.Contains("MSTRecieptType") Then
                If clsDefaultConfiguration.PrintFormatNo = "8" Then
                    'code added by irfan for cakeology on 4/10/2017
                    Dim objt As New clsCashMemoPrint(salesordernumber, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving)
                    objt.PrintFormatNo = clsDefaultConfiguration.PrintFormatNo
                    objt.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "SO", "", "", "", "", "", Obj.TotalBalAmt, objPaymentByCash.TotalMinimumAmount, clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6)   'code added by irfan on 9/9/2017 visiblity of hsn and tax
                    '------------------------------------------------------------------------------------------------

                Else
                    If Val(lblOrderQty.Text) = Val(lblPickupQty.Text) + Val(lbldeliveredqty.Text) Then
                        strSOStatus = "Closed"
                    Else
                        strSOStatus = "Open"

                    End If

                    Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, vSalesNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, strSOStatus, dsOtherCharges, ShowFullName:=clsDefaultConfiguration.PrintItemFullName _
                                                                         , strReturnReason:=strRemark, dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, SalesOrderCreationDate:=vSalesOrderCreationDate, SalesOrderDeliveryDate:=vSalesOrderExpectedDeliveryDate, strSalesPerson:=SalesPersonName, CreditSale:=crdsale, strInvoiceTo:=strInvoiceTo)
                End If
            Else
            ' this is add to get status in print of SO document when call in edit
            'bug 251
            If clsDefaultConfiguration.PrintFormatNo = "8" Then
                'code added by irfan for cakeology on 4/10/2017
                Dim objt As New clsCashMemoPrint(salesordernumber, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving)
                objt.PrintFormatNo = clsDefaultConfiguration.PrintFormatNo
                objt.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "SO", "", "", "", "", "", Obj.TotalBalAmt, objPaymentByCash.TotalMinimumAmount, clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6)   'code added by irfan on 9/9/2017 visiblity of hsn and tax
                '------------------------------------------------------------------------------------------------

            Else
                If lblPickupQty.Text <> String.Empty And lbldeliveredqty.Text <> String.Empty Then
                    If Val(lblOrderQty.Text) = Val(lblPickupQty.Text) + Val(lbldeliveredqty.Text) Then
                        strSOStatus = "Closed"
                    Else
                        strSOStatus = "Open"

                    End If
                End If

                ' this is add to get status in print of SO document when call in edit

                Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, vSalesNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), Nothing, CtrlSalesInfo.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, strSOStatus, dsOtherCharges, ShowFullName:=clsDefaultConfiguration.PrintItemFullName _
                                                                       , strReturnReason:=strRemark, dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, SalesOrderCreationDate:=vSalesOrderCreationDate, SalesOrderDeliveryDate:=vSalesOrderExpectedDeliveryDate, strSalesPerson:=SalesPersonName, CreditSale:=crdsale, strInvoiceTo:=strInvoiceTo)

                End If
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    Private Function PrintSalesOrdersReturn(ByVal drSite As DataRow, ByVal drHAdds As DataRow) As Boolean
        Try
            SalesPersonName = IIf(CtrlSalesPerson.CtrlSalesPersons.SelectedIndex < 0, "", CtrlSalesPerson.CtrlSalesPersons.Text)
            Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.SOReturnStatus, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, vSalesNo, CtrlCustDtls1.dtCustmInfo, dsScanReturn.Tables("ItemScanDetails"), Nothing, CtrlSalesInfo.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, "Open", Nothing, txtReturnReason.Text, ShowFullName:=clsDefaultConfiguration.PrintItemFullName, strSalesPerson:=SalesPersonName)

            'Dim PrintSo As New System.Text.StringBuilder
            'If dsScan Is Nothing Then
            '    Exit Function
            'End If

            'PrintSo.Length = 0
            'PrintSo.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)
            'PrintSo.Append("                          SALES ORDER RETURN                                " & vbCrLf)
            'PrintSo.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)
            'PrintSo.Append("Company Name   : " & "CreativeIT India Ltd" & "     Company Code : " & "CRITI02" & vbCrLf)

            'If vIsPrintOfficialAddressAllowed = False Then
            '    If Not (drSite Is Nothing) Then
            '        PrintSo.Append("Store Name         : " & drSite.Item("SiteOfficialName") & "                Store Code   : " & vSiteCode & vbCrLf)
            '        PrintSo.Append("Store Address       : " & drSite.Item("SiteAddressLn1") & vbCrLf & "                            " & _
            '                     drSite.Item("SiteAddressLn2") & " " & drSite.Item("SiteAddressLn3") & _
            '                     drSite.Item("SitePinCode") & vbCrLf)
            '    End If
            'Else
            '    PrintSo.Append(vbCrLf & "Print Official Address " & vbCrLf)
            'End If

            'If vHeaderNote = True Then
            '    PrintSo.Append(vbCrLf & "Header Information is Welcome" & vbCrLf)
            'End If

            'PrintSo.Append(vbCrLf & "----------------------------------------------------------------------------------------------------" & vbCrLf)
            'PrintSo.Append("Sales Order No              : " & CtrlSalesInfo.CtrlTxtOrderNo.Text & "                    Date    : " & Format(vCurrentDate, vDateFormat) & vbCrLf)
            'PrintSo.Append("Cashier Name                : " & vUserName & vbCrLf & vbCrLf)

            'PrintSo.Append("Customer Name    : " & CtrlCustDtls1.lblCustNameValue.Text & "          Customer Code : " & vCustomerNo & vbCrLf)

            'PrintSo.Append("Home Address     : " & drHAdds.Item("Address").ToString & vbCrLf)
            'PrintSo.Append("                 : " & drHAdds.Item("City") & ", " & drHAdds.Item("State") & ", " & drHAdds.Item("Country") & ", " & drHAdds.Item("PinCode") & vbCrLf & vbCrLf)

            'PrintSo.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)
            'PrintSo.Append("Item Code       Item Desc                               Order Qty       ReturnQty       Price      Disc%  Tax%   NetAmt" & vbCrLf)
            'PrintSo.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)

            'For Each drDtl As DataRow In dsScanReturn.Tables("ItemScanDetails").Select("PickUpQty>0 and IsStatus='Inserted'")
            '    PrintSo.Append(drDtl("ArticleCode") & Space(16 - drDtl("ArticleCode").ToString.Length) & _
            '                  drDtl("Discription") & Space(40 - drDtl("Discription").ToString.Length) & _
            '                  drDtl("Quantity") & Space(10 - drDtl("Quantity").ToString.Length) & _
            '                  drDtl("PickUpQty") & Space(10 - drDtl("PickUpQty").ToString.Length) & _
            '                  Format(drDtl("SellingPrice"), "0.0") & Space(10 - drDtl("SellingPrice").ToString.Length) & _
            '                  drDtl("Discount") & Space(10 - drDtl("Discount").ToString.Length) & _
            '                  drDtl("ExclTaxAmt") & Space(10 - drDtl("ExclTaxAmt").ToString.Length) & Format(drDtl("NetAmount"), "0.0") & vbCrLf)
            'Next

            'PrintSo.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)

            'If vIsPrintingTaxInfoAllowed = True Then
            '    PrintSo.Append("Print Tax Details..............." & vbCrLf & vbCrLf)
            'End If

            'PrintSo.Append("<Terms & Condition>" & vbCrLf & vbCrLf)

            'PrintSo.Append("Authorized Sign:...............            Customer Sign:................")

            'If vIsPromotionalMessageAllowed = True Then
            '    PrintSo.Append(vbCrLf & "Promotional Message is Welcome" & vbCrLf)
            'End If
            'If vFooterNote = True Then
            '    PrintSo.Append(vbCrLf & "Footer Information is Welcome" & vbCrLf)
            'End If

            'If vIsPrintPreviewAllowed = True Then
            '    fnPrint(PrintSo.ToString, "")          'Print Preview
            'End If
            'fnPrint(PrintSo.ToString, "PRN")   'Direct Print

            ''PrintSo.Append("")                 'Set Debug Point

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    'Private Function PrintCreditVoucher(ByVal drSite As DataRow, ByVal VoucherAmt As Decimal) As Boolean
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
    '        PrintSo.Append("Issued against S.O. No. :" & CtrlSalesInfo.CtrlTxtOrderNo.Text & "   Date: " & Format(vCurrentDate, vDateFormat) & vbCrLf)

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

    '    Catch ex As Exception
    '        ShowMessage(ex.Message, getValueByKey("CLAE05"))
    '    End Try
    'End Function

    Private Function PrintSalesOrdersInvoice(ByVal drSite As DataRow, ByVal drHAdds As DataRow, Optional ByVal drDAdds As DataRow = Nothing, Optional ByVal dtZInvc As DataTable = Nothing) As Boolean
        'Dim PrintInvc As New System.Text.StringBuilder

        'Try
        '    If dsScan Is Nothing Then
        '        Exit Function
        '    End If

        '    PrintInvc.Length = 0
        '    PrintInvc.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)
        '    PrintInvc.Append("                          SALES INVOICE                                 " & vbCrLf)
        '    PrintInvc.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)

        '    PrintInvc.Append("Company Name   : " & "CreativeIT India Ltd" & "     Company Code : " & "CRITI02" & vbCrLf)

        '    If vIsPrintOfficialAddressAllowed = False Then
        '        If Not (drSite Is Nothing) Then
        '            PrintInvc.Append("Store Name         : " & drSite.Item("SiteOfficialName") & "                Store Code   : " & vSiteCode & vbCrLf)
        '            PrintInvc.Append("Store Address       : " & drSite.Item("SiteAddressLn1") & vbCrLf & "                            " & _
        '                         drSite.Item("SiteAddressLn2") & " " & drSite.Item("SiteAddressLn3") & _
        '                         drSite.Item("SitePinCode") & vbCrLf)
        '        End If
        '    Else
        '        PrintInvc.Append(vbCrLf & "Print Official Address " & vbCrLf)
        '    End If

        '    If vHeaderNote = True Then
        '        PrintInvc.Append(vbCrLf & "Header Information is Welcome" & vbCrLf)
        '    End If

        '    PrintInvc.Append(vbCrLf & "----------------------------------------------------------------------------------------------------" & vbCrLf)
        '    PrintInvc.Append("Sales Invoice No            : " & vSalesInvcNo & "  Reference Sales Order : " & CtrlSalesInfo.CtrlTxtOrderNo.Text & "   Date : " & Format(vCurrentDate, "dd-MM-yyyy") & vbCrLf)
        '    PrintInvc.Append("Expected Delivery Date  : " & Format(CtrlSalesInfo.CtrlDtExpDelDate.Value, vDateFormat) & vbCrLf)
        '    PrintInvc.Append("Cashier Name                : " & vUserName & vbCrLf & vbCrLf)

        '    PrintInvc.Append("Customer Name    : " & CtrlCustDtls1.lblCustNameValue.Text & "          Customer Code : " & vCustomerNo & vbCrLf)

        '    PrintInvc.Append("Home Address       : " & drHAdds.Item("Address").ToString & vbCrLf)
        '    PrintInvc.Append("                           : " & drHAdds.Item("City") & ", " & drHAdds.Item("State") & ", " & drHAdds.Item("Country") & ", " & drHAdds.Item("PinCode") & vbCrLf & vbCrLf)

        '    If drDelvAdds Is Nothing Then
        '        PrintInvc.Append("Delivery Address : " & CtrlCustDtls1.lblAddressValue.Text)
        '        PrintInvc.Append("Tel. No.  	   : " & CtrlCustDtls1.lblTelNoValue.Text & vbCrLf & vbCrLf)
        '    Else
        '        PrintInvc.Append("Delivery Address : " & drDAdds.Item("Address").ToString & vbCrLf)
        '        PrintInvc.Append("                          : " & drDAdds.Item("City") & ", " & drDAdds.Item("State") & ", " & drDAdds.Item("Country") & ", " & drDAdds.Item("PinCode").ToString.Trim & vbCrLf)
        '        PrintInvc.Append("Tel. No.  	             : " & drDAdds.Item("OfficeNo") & vbCrLf & vbCrLf)
        '    End If

        '    PrintInvc.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)
        '    PrintInvc.Append("Item Code       Item Desc                               Qty       Price      Disc%  Tax%   NetAmt" & vbCrLf)
        '    PrintInvc.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)

        '    For Each drDtl As DataRow In dsScan.Tables("ItemScanDetails").Rows
        '        PrintInvc.Append(drDtl("ArticleCode") & Space(16 - drDtl("ArticleCode").ToString.Length) & _
        '                      drDtl("Discription") & Space(40 - drDtl("Discription").ToString.Length) & _
        '                      drDtl("Quantity") & Space(10 - drDtl("Quantity").ToString.Length) & _
        '                      Format(drDtl("SellingPrice"), "0.0") & Space(10 - drDtl("SellingPrice").ToString.Length) & _
        '                      drDtl("Discount") & Space(10 - drDtl("Discount").ToString.Length) & _
        '                      drDtl("ExclTaxAmt") & Space(10 - drDtl("ExclTaxAmt").ToString.Length) & Format(drDtl("NetAmount"), "0.0") & vbCrLf)
        '    Next
        '    PrintInvc.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)

        '    PrintInvc.Append("Total Qty    : " & lblOrderQty.Text & vbCrLf)
        '    PrintInvc.Append("Gross Amt  : " & CtrlCashSummary1.lbltxt1 & vbCrLf)
        '    PrintInvc.Append("Disc  Amt   : " & CtrlCashSummary1.lbltxt2 & vbCrLf)
        '    PrintInvc.Append("Incl. Amt   : " & Format(CDbl(dsScan.Tables("ItemScanDetails").Compute("Sum(IncTaxAmt)", ""))), "0.00" & vbCrLf)
        '    PrintInvc.Append("Excl. Amt   : " & Format(CDbl(dsScan.Tables("ItemScanDetails").Compute("Sum(ExclTaxAmt)", ""))), "0.00" & vbCrLf)
        '    PrintInvc.Append("Net   Amt   : " & CtrlCashSummary1.lbltxt4 & vbCrLf & vbCrLf)

        '    PrintInvc.Append("Payment Received :" & vbCrLf)
        '    PrintInvc.Append("Minimum Advance To Pay: (INR)  " & CtrlCashSummary1.lbltxt5 & vbCrLf & vbCrLf)

        '    PrintInvc.Append("Advance Amount Paid :" & vbCrLf)
        '    'PrintInvc.Append("Tender      :" & vbCrLf)

        '    If dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
        '        Dim RowIndexPayment As Integer = 0
        '        For Each drPayment As DataRow In dsPayment.Tables("MSTRecieptType").Rows
        '            PrintInvc.Append("Tender      :" & drPayment("RecieptType").ToString() & vbCrLf)
        '            If RowIndexPayment = 0 Then
        '                PrintInvc.Append("Tender Info :" & drPayment("Amount") & "  Date : " & Format(vCurrentDate, vDateFormat) & vbCrLf)
        '            End If
        '            If RowIndexPayment > 0 Then
        '                PrintInvc.Append("Tender Info :" & drPayment("Amount") & vbCrLf)
        '            End If
        '            RowIndexPayment += 1
        '        Next

        '    End If

        '    PrintInvc.Append(vbCrLf & "Total Paid Amount:" & vbCrLf)
        '    PrintInvc.Append("Tender      :" & vbCrLf)
        '    If dsMain.Tables("SalesInvoice").Rows.Count > 0 Then
        '        PrintInvc.Append("Tender Info :" & dsMain.Tables("SalesInvoice").Compute("Sum(AmountTendered)", "") & "  Date : " & dsMain.Tables("SalesInvoice").Rows(0).Item("CREATEDON") & vbCrLf)
        '        PrintInvc.Append("Tender Info :" & vbCrLf)
        '    Else
        '        PrintInvc.Append("Tender Info :" & CtrlCashSummary1.lbltxt5 & vbCrLf)
        '    End If

        '    PrintInvc.Append("Balance Amount Due: " & CtrlCashSummary1.lbltxt6 & vbCrLf & vbCrLf)

        '    If vIsPrintingTaxInfoAllowed = True Then
        '        PrintInvc.Append("Print Tax Details..............." & vbCrLf & vbCrLf)
        '    End If

        '    PrintInvc.Append("<Terms & Condition>" & vbCrLf & vbCrLf)

        '    PrintInvc.Append("Authorized Sign:...............            Customer Sign:................" & vbCrLf)

        '    If vIsPromotionalMessageAllowed = True Then
        '        PrintInvc.Append(vbCrLf & "Promotional Message is Welcome" & vbCrLf)
        '    End If
        '    If vFooterNote = True Then
        '        PrintInvc.Append(vbCrLf & "Footer Information is Welcome" & vbCrLf)
        '    End If

        '    If clsDefaultConfiguration.IsPrintPreviewAllowed = True Then
        '        fnPrint(PrintInvc.ToString, "")          'Print Preview
        '    End If
        '    fnPrint(PrintInvc.ToString, "PRN")   'Direct Print

        '    'PrintInvc.Append("")                 'Set Debug Point
        Try
            Dim dsOtherCharges As New DataSet
            dsOtherCharges.Clear()
            Dim dt As DataTable
            If dsMain.Tables.Contains("SalesOrderOtherCharges") Then
                dt = dsMain.Tables("SalesOrderOtherCharges").Copy()
            End If
            If Not dt Is Nothing AndAlso dt.Rows.Count = 0 Then
                dt = TempOtherChargesTable.Copy()
            ElseIf dt Is Nothing Then
                dt = TempOtherChargesTable.Copy()
            End If
            dsOtherCharges.Tables.Add(dt)
            dt.TableName = "NewOtherCharges"

            If dtZInvc IsNot Nothing Then
                Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Payment, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, vSalesNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dtZInvc, CtrlSalesInfo.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, "", dsOtherCharges, "", dtSalesOrderTaxDetails, ShowFullName:=clsDefaultConfiguration.PrintItemFullName _
                                                                        , dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, SalesOrderDeliveryDate:=vSalesOrderExpectedDeliveryDate)
            Else
                Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Payment, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, vSalesNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, "", dsOtherCharges, "", dtSalesOrderTaxDetails, ShowFullName:=clsDefaultConfiguration.PrintItemFullName _
                                                                        , dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, SalesOrderDeliveryDate:=vSalesOrderExpectedDeliveryDate)
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Function

    Private Function PrintSalesOrdersDelivery(ByVal drSite As DataRow, ByVal drHAdds As DataRow, Optional ByVal drDAdds As DataRow = Nothing) As Boolean
        'Dim PrintDelInvc As New System.Text.StringBuilder

        'Try
        '    If dsScan Is Nothing Then
        '        Exit Function
        '    End If

        '    PrintDelInvc.Length = 0
        '    PrintDelInvc.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)
        '    PrintDelInvc.Append("                          SALES OUTBOUND DELIVERY                       " & vbCrLf)
        '    PrintDelInvc.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)

        '    PrintDelInvc.Append("Company Name   : " & "CreativeIT India Ltd" & "     Company Code : " & "CRITI02" & vbCrLf)

        '    If vIsPrintOfficialAddressAllowed = False Then
        '        If Not (drSite Is Nothing) Then
        '            PrintDelInvc.Append("Store Name         : " & drSite.Item("SiteOfficialName") & "                Store Code   : " & vSiteCode & vbCrLf)
        '            PrintDelInvc.Append("Store Address       : " & drSite.Item("SiteAddressLn1") & vbCrLf & "                            " & _
        '                         drSite.Item("SiteAddressLn2") & " " & drSite.Item("SiteAddressLn3") & _
        '                         drSite.Item("SitePinCode") & vbCrLf)
        '        End If
        '    Else
        '        PrintDelInvc.Append(vbCrLf & "Print Official Address " & vbCrLf)
        '    End If

        '    If vHeaderNote = True Then
        '        PrintDelInvc.Append(vbCrLf & "Header Information is Welcome" & vbCrLf)
        '    End If

        '    PrintDelInvc.Append(vbCrLf & "----------------------------------------------------------------------------------------------------" & vbCrLf)
        '    PrintDelInvc.Append("Sales Invoice No            : " & vSalesInvcNo & "  Reference Sales Order : " & CtrlSalesInfo.CtrlTxtOrderNo.Text & "   Date : " & Format(vCurrentDate, vDateFormat) & vbCrLf)
        '    PrintDelInvc.Append("Expected Delivery Date  : " & Format(CtrlSalesInfo.CtrlDtExpDelDate.Value, vDateFormat) & vbCrLf)
        '    PrintDelInvc.Append("Cashier Name                : " & vUserName & vbCrLf & vbCrLf)

        '    PrintDelInvc.Append("Customer Name    : " & CtrlCustDtls1.lblCustNameValue.Text & "          Customer Code : " & vCustomerNo & vbCrLf)

        '    PrintDelInvc.Append("Home Address       : " & drHAdds.Item("Address").ToString & vbCrLf)
        '    PrintDelInvc.Append("                           : " & drHAdds.Item("City") & ", " & drHAdds.Item("State") & ", " & drHAdds.Item("Country") & ", " & drHAdds.Item("PinCode") & vbCrLf & vbCrLf)

        '    If drDelvAdds Is Nothing Then
        '        PrintDelInvc.Append("Delivery Address : " & CtrlCustDtls1.lblAddressValue.Text)
        '        PrintDelInvc.Append("Tel. No.  	   : " & CtrlCustDtls1.lblTelNoValue.Text & vbCrLf & vbCrLf)
        '    Else
        '        PrintDelInvc.Append("Delivery Address : " & drDAdds.Item("Address").ToString & vbCrLf)
        '        PrintDelInvc.Append("                          : " & drDAdds.Item("City") & ", " & drDAdds.Item("State") & ", " & drDAdds.Item("Country") & ", " & drDAdds.Item("PinCode").ToString.Trim & vbCrLf)
        '        PrintDelInvc.Append("Tel. No.  	             : " & drDAdds.Item("OfficeNo") & vbCrLf & vbCrLf)
        '    End If

        '    PrintDelInvc.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)
        '    PrintDelInvc.Append("Item Code       Item Desc                               Qty       Price      Disc%  Tax%   NetAmt" & vbCrLf)
        '    PrintDelInvc.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)

        '    Dim vNetAmount As Double = 0.0
        '    Dim vDiscount As Double = 0.0
        '    Dim vExclTaxAmt As Double = 0.0
        '    For Each drDtl As DataRow In dsScan.Tables("ItemScanDetails").Select("PickUpQty > 0")

        '        vDiscount = drDtl("PickUpQty") * (drDtl("LineDiscount") / drDtl("Quantity"))
        '        vExclTaxAmt = drDtl("PickUpQty") * (drDtl("ExclTaxAmt") / drDtl("Quantity"))
        '        vNetAmount = (drDtl("PickUpQty") * drDtl("SellingPrice")) + vExclTaxAmt - vDiscount

        '        PrintDelInvc.Append(drDtl("ArticleCode") & Space(16 - drDtl("ArticleCode").ToString.Length) & _
        '                      drDtl("Discription") & Space(40 - drDtl("Discription").ToString.Length) & _
        '                      drDtl("PickUpQty") & Space(10 - drDtl("PickUpQty").ToString.Length) & _
        '                      Format(drDtl("SellingPrice"), "0.0") & Space(10 - drDtl("SellingPrice").ToString.Length) & _
        '                      drDtl("Discount") & Space(10 - drDtl("Discount").ToString.Length) & _
        '                      drDtl("ExclTaxAmt") & Space(10 - drDtl("ExclTaxAmt").ToString.Length) & Format(vNetAmount, "0.0") & vbCrLf)
        '    Next
        '    PrintDelInvc.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)

        '    PrintDelInvc.Append("Total Qty : " & lblOrderQty.Text & vbCrLf)
        '    PrintDelInvc.Append("PickUpQty : " & lblPickupQty.Text & vbCrLf)
        '    PrintDelInvc.Append("Gross Amt : " & CtrlCashSummary1.lbltxt1 & vbCrLf)
        '    PrintDelInvc.Append("Disc  Amt : " & CtrlCashSummary1.lbltxt2 & vbCrLf)
        '    PrintDelInvc.Append("Incl. Amt : " & dsScan.Tables("ItemScanDetails").Compute("SUM(IncTaxAmt)", "") & vbCrLf)
        '    PrintDelInvc.Append("Excl. Amt : " & dsScan.Tables("ItemScanDetails").Compute("SUM(ExclTaxAmt)", "") & vbCrLf)
        '    PrintDelInvc.Append("Net   Amt : " & CtrlCashSummary1.lbltxt4 & vbCrLf & vbCrLf)

        '    PrintDelInvc.Append("Payment Received :" & vbCrLf)
        '    PrintDelInvc.Append("Minimum Advance To Pay: (INR)  " & CtrlCashSummary1.lbltxt5 & vbCrLf & vbCrLf)

        '    PrintDelInvc.Append("Advance Amount Paid :" & vbCrLf)
        '    PrintDelInvc.Append("Tender      :" & vbCrLf)

        '    If Not (dsPayment.Tables("MSTRecieptType") Is Nothing) AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
        '        Dim RowIndexPayment As Integer = 0
        '        For Each drPayment As DataRow In dsPayment.Tables("MSTRecieptType").Rows
        '            If RowIndexPayment = 0 Then
        '                PrintDelInvc.Append("Tender Info :" & drPayment("Amount") & "  Date : " & Format(vCurrentDate, vDateFormat) & vbCrLf)
        '            End If
        '            If RowIndexPayment > 0 Then
        '                PrintDelInvc.Append("Tender Info :" & drPayment("Amount") & vbCrLf)
        '            End If
        '            RowIndexPayment += 1
        '        Next

        '    End If

        '    PrintDelInvc.Append(vbCrLf & "Total Paid Amount:" & vbCrLf)
        '    PrintDelInvc.Append("Tender      :" & vbCrLf)
        '    If dsMain.Tables("SalesInvoice").Rows.Count > 0 Then
        '        PrintDelInvc.Append("Tender Info :" & dsMain.Tables("SalesInvoice").Compute("Sum(AmountTendered)", "") & "  Date : " & dsMain.Tables("SalesInvoice").Rows(0).Item("CREATEDON") & vbCrLf)
        '        PrintDelInvc.Append("Tender Info :" & vbCrLf)
        '    Else
        '        PrintDelInvc.Append("Tender Info :" & CtrlCashSummary1.lbltxt5 & vbCrLf)
        '    End If

        '    PrintDelInvc.Append("Balance Amount Due: " & CtrlCashSummary1.lbltxt6 & vbCrLf & vbCrLf)

        '    If vIsPrintingTaxInfoAllowed = True Then
        '        PrintDelInvc.Append("Print Tax Details..............." & vbCrLf & vbCrLf)
        '    End If

        '    PrintDelInvc.Append("<Terms & Condition>" & vbCrLf & vbCrLf)

        '    PrintDelInvc.Append("Authorized Sign:..............." & vbTab & "Customer Sign:................")

        '    If vIsPromotionalMessageAllowed = True Then
        '        PrintDelInvc.Append(vbCrLf & "Promotional Message is Welcome" & vbCrLf)
        '    End If
        '    If vFooterNote = True Then
        '        PrintDelInvc.Append(vbCrLf & "Footer Information is Welcome" & vbCrLf)
        '    End If

        '    If vIsPrintPreviewAllowed = True Then
        '        fnPrint(PrintDelInvc.ToString, "")          'Print Preview
        '    End If
        '    fnPrint(PrintDelInvc.ToString, "PRN")   'Direct Print

        '    'PrintDelInvc.Append("")                 'Set Debug Point

        Try
            Dim strRemark As String = CtrlSalesInfo.CtrlTxtRemarks.Text
            Dim strInvoiceTo As String = CtrlSalesInfo.CtrlTxtInvoice.Text
            SalesPersonName = IIf(CtrlSalesPerson.CtrlSalesPersons.SelectedIndex < 0, "", CtrlSalesPerson.CtrlSalesPersons.Text)
            Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.DeliveryNote, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, vSalesNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo.CtrlTxtCustOrdRef.Text,
                                                                    vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, strReturnReason:=strRemark, ShowFullName:=clsDefaultConfiguration.PrintItemFullName _
                                                                 , dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, SalesOrderDeliveryDate:=vSalesOrderExpectedDeliveryDate, strSalesPerson:=SalesPersonName, strInvoiceTo:=strInvoiceTo)



        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Function

#End Region
#Region "Common Button Action"


    Private IsGiftVoucher As Boolean

    ''' <summary>
    ''' Accept Payment for current Sales Order
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub BtnAcceptPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles BtnSOAcceptPayment.Click
        Try
            grdSOItems.FinishEditing()

            If boolIsReturn = False Then
                If Not (dsScan.Tables(0).Rows.Count > 0) Then
                    'ShowMessage("Please Scan Article first", "Sales Order Information")
                    ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                    Exit Sub
                    'ElseIf CDbl(lblminadvancepaid.Text.Trim) <= 0 Then
                    '    BtnSaveSalesOrder_Click(sender, e)
                    '    Exit Sub
                ElseIf Not CDbl(CtrlCashSummary1.lbltxt6) > Decimal.Zero AndAlso Not CDbl(CtrlCashSummary1.lbltxt6) < Decimal.Zero Then
                    ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If

                Dim objPayment As New frmNAcceptPayment()
                objPayment.TotalBillAmount = CDbl(CtrlCashSummary1.lbltxt6)
                objPayment.ParentRelation = "SalesOrder"

                If CDbl(CtrlCashSummary1.lbltxt6) < 0 Then
                    objPayment.MinimumBillAmount = CDbl(CtrlCashSummary1.lbltxt6)
                ElseIf CDbl(CtrlCashSummary1.lbltxt4) = CDbl(lblminadvancepaid.Text) Then
                    objPayment.MinimumBillAmount = CDbl(CtrlCashSummary1.lbltxt6)
                Else
                    objPayment.MinimumBillAmount = CDbl(lblminadvancepaid.Text)
                End If
                ' CtrlCustDtls1.lblCustTypeValue(lblCustTypeValue)
                If CtrlCustDtls1.lblCustTypeValue.Text <> String.Empty Then
                    objPayment.CLPCustomerCardNumber = CtrlCustDtls1.lblCustNoValue.Text
                End If
                objPayment.AcceptEditBillDataSet = dsPayment
                objPayment.PaymentType = clsAcceptPayment.PaymentType.Advance

                If Val(lblPickupQty.Text) = 0 Then
                    objPayment.AvoidCreditSalesTender = True
                End If
                objPayment.CreditSaleLimitSOPickUpAmt = Val(lblminadvancepaid.Text)
                'Dim obj As New frmSpecialPrompt("What you want to pay")
                'obj.ShowTextBox = True
                'obj.ShowDialog()
                'If Not obj.GetResult Is Nothing Then
                '    objPayment.CustomerWantPay = obj.GetResult
                'End If

                '  New form for accepting payment in sales order.
                Dim preMinAmout As String = "0"
                If CDbl(CDbl(CtrlCashSummary1.lbltxt6.Trim)) < 0 AndAlso Not (CDbl(CDbl(CtrlCashSummary1.lbltxt6.Trim)) = CDbl(CDbl(lblReceivedAmt.Text.Trim))) Then
                Else
                    Dim Obj As New frmNHowMuchtoPay
                    Obj.CtrlTxtMinAmt.Text = lblminadvancepaid.Text
                    preMinAmout = lblminadvancepaid.Text
                    Obj.CtrlTxtPickAmt.Text = PickAmtToPay() 'Math.Round(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "")), 2)
                    Obj.CtrlTxtPickAmt.Text = MyRound(CDbl(Obj.CtrlTxtPickAmt.Text), clsDefaultConfiguration.BillRoundOffAt)
                    Obj.TotalBalAmt = CDbl(CtrlCashSummary1.lbltxt6)
                    Obj.ctrlTxtHowMuchPay.Text = CDbl(lblminadvancepaid.Text)
                    If Obj.TotalBalAmt > 0 Then
                        Obj.ShowDialog(Me)  ' this is add on 24.feb.2010 because this screen appear alone if user toggle with ALT key
                    End If


                    If Obj.DialogResult = Windows.Forms.DialogResult.Cancel Then
                        lblminadvancepaid.Text = Obj.CtrlTxtMinAmt.Text
                        If Obj.blnAllowtoGoPaymentScreen = False Then
                            Exit Sub
                        End If
                        If Obj.ctrlTxtHowMuchPay.Text = Decimal.Zero Then
                            BtnSaveSalesOrder_Click(sender, e, True)
                            Exit Sub
                        End If
                    Else
                        lblminadvancepaid.Text = preMinAmout
                        Exit Sub
                    End If
                    ' end New form for accepting payment in sales order.
                    If Not Obj.ctrlTxtHowMuchPay Is Nothing Then
                        objPayment.CustomerWantPay = Obj.ctrlTxtHowMuchPay.Text
                        objPayment.ctrlPayCash.txtCash.Value = Obj.ctrlTxtHowMuchPay.Text
                    End If

                End If


                objPayment.TotalPick = PickAmtToPay()
                objPayment.IsChangeTender = True
                objPayment.RoundAt = clsDefaultConfiguration.BillRoundOffAt
                objPayment.ShowDialog(Me)

                _dsPayment = New DataSet
                _dsPayment = objPayment.ReciptTotalAmount()

                If objPayment.IsCancelAcceptPayment = False AndAlso _dsPayment.Tables.Count > 0 Then

                    Dim dv As New DataView(_dsPayment.Tables(0), "RecieptTypeCode='CreditVouc(I)'", "", DataViewRowState.CurrentRows)
                    If dv.Count > 0 Then
                        IssuingCV = True
                    End If
                    CalculateSalesOrderSummory(dsScan)
                Else
                    lblminadvancepaid.Text = preMinAmout
                    Exit Sub
                End If
                objPayment.Close()
                Me.Select()



                If objPayment.Action = "Save" Then
                    IsGiftVoucher = False

                    'Added by Rohit for CR5938

                    _dDueDate = objPayment.dDueDate
                    _strRemarks = objPayment.strRemarks

                    BtnSaveSalesOrder_Click(sender, e, True)
                End If
                If objPayment.Action = "Gift" Then
                    IsGiftVoucher = True
                    GiftReceiptMessage = objPayment.GiftReceiptMessage

                    'Added by Rohit for CR5938

                    _dDueDate = objPayment.dDueDate
                    _strRemarks = objPayment.strRemarks

                    BtnSaveSalesOrder_Click(sender, e)
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub BtnPayCash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            grdSOItems.FinishEditing()

            If boolIsReturn = False Then
                If Not (dsScan.Tables(0).Rows.Count > 0) Then
                    'ShowMessage("Please Scan Article first", "Sales Order Information")
                    ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                    Exit Sub
                ElseIf Not CDbl(CtrlCashSummary1.lbltxt6) > Decimal.Zero Then
                    ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If

                'Dim objCash As New frmSpecialPrompt("Enter Amount ")
                'objCash.lblMessage.Text = " Bill Amount is " & CDbl(CtrlCashSummary1.lbltxt4)
                'objCash.ShowTextBox = True
                'objCash.lblMessage.Visible = True
                'objCash.ShowDialog()
                'Dim Value As Double = objCash.GetResult
                'objCash.Dispose()
                'If Value > 0 Then
                '    If Value > CDbl(CtrlCashSummary1.lbltxt4) Then
                '        Dim ReturnAmt As Double = Value - CDbl(CtrlCashSummary1.lbltxt4)
                '        Dim strshowmsg As String = ""
                '        strshowmsg = "Bill Amt to Pay " & CDbl(CtrlCashSummary1.lbltxt4) & vbLf
                '        strshowmsg = strshowmsg & "Customer Paid " & Value & vbLf
                '        strshowmsg = strshowmsg & "Return to Customer " & ReturnAmt & " Amount" & vbLf
                '        ShowMessage(strshowmsg, "Information")
                '    End If
                '    If Value < CDbl(CtrlCashSummary1.lbltxt4) Then
                '        ShowMessage(getValueByKey("CM032"), "CM032")
                '        'ShowMessage("Amount is not Settle ", "Information")
                '        Exit Sub
                '    End If

                '    Dim objPayment As New frmNAcceptPayment()
                '    objPayment.ParentRelation = "SalesOrder"
                '    objPayment.TotalBillAmount = Math.Round(CDbl(CtrlCashSummary1.lbltxt1), 0)
                '    objPayment.MinimumBillAmount = Math.Round(CDbl(CtrlCashSummary1.lbltxt5), 0)

                '    objPayment.PaymentType = clsAcceptPayment.PaymentType.Advance
                '    objPayment.Show()

                '    objPayment.TotalBillAmount = CtrlCashSummary1.lbltxt4
                '    objPayment.Enabled = False
                '    objPayment.cboRecieptType.SelectedValue = "Cash"
                '    objPayment.TotalBillAmount = CtrlCashSummary1.lbltxt4
                '    objPayment.InsertReceiptCashDetails()

                '    _dsPayment = New DataSet
                '    _dsPayment = objPayment.ReciptTotalAmount()
                '    If objPayment.IsCancelAcceptPayment = False Then
                '        CalculateSalesOrderSummory(dsScan)
                '    End If

                '    If dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables(0).Rows.Count > 0 Then
                '        objPayment.Close()
                '        BtnSaveSalesOrder_Click(sender, e)
                '    End If

                'End If
                Dim Obj As New frmNHowMuchtoPay
                Dim preMinAmout As String = "0"
                Obj.CtrlTxtMinAmt.Text = lblminadvancepaid.Text
                preMinAmout = lblminadvancepaid.Text
                Obj.CtrlTxtPickAmt.Text = PickAmtToPay() 'Math.Round(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "")), 2)
                Obj.CtrlTxtPickAmt.Text = MyRound(Obj.CtrlTxtPickAmt.Text, clsDefaultConfiguration.BillRoundOffAt)
                Obj.TotalBalAmt = CDbl(CtrlCashSummary1.lbltxt6)
                Obj.ctrlTxtHowMuchPay.Text = CDbl(lblminadvancepaid.Text)
                Obj.ShowDialog()

                If Obj.DialogResult = Windows.Forms.DialogResult.Cancel Then
                    lblminadvancepaid.Text = Obj.CtrlTxtMinAmt.Text
                    If Obj.blnAllowtoGoPaymentScreen = False Then
                        Exit Sub
                    End If
                    If Obj.ctrlTxtHowMuchPay.Text = Decimal.Zero Then
                        BtnSaveSalesOrder_Click(sender, e, True)
                        Exit Sub
                    End If
                Else
                    lblminadvancepaid.Text = preMinAmout
                    Exit Sub
                End If
                Dim objPaymentByCash As New frmNAcceptPaymentByCash("SO")
                objPaymentByCash.TotalBillAmount = CDbl(CtrlCashSummary1.lbltxt6)
                objPaymentByCash.TotalMinimumAmount = CDbl(lblminadvancepaid.Text)
                'Dim obj As New frmSpecialPrompt("What you want to pay")
                'obj.ShowTextBox = True
                'obj.ShowDialog()
                'If Not obj.GetResult Is Nothing Then
                '    objPaymentByCash.CustomerWantPay = obj.GetResult
                'End If
                If Not Obj.ctrlTxtHowMuchPay Is Nothing Then
                    objPaymentByCash.CustomerWantPay = Obj.ctrlTxtHowMuchPay.Text
                End If
                objPaymentByCash.ShowDialog()

                If Not (objPaymentByCash.IsCancelAcceptPayment) Then
                    If Not objPaymentByCash.ReciptTotalAmount Is Nothing And objPaymentByCash.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                        _dsPayment = objPaymentByCash.ReciptTotalAmount
                        'Dim ds As New DataSet()
                        'ds.Tables.Add(dt)

                        objPaymentByCash.Close()
                        'If Not ds Is Nothing Then
                        If objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeSave Then
                            lblReceivedAmt.Text = CDbl(dsPayment.Tables(0).Compute("Sum(Amount)", ""))
                            BtnSaveSalesOrder_Click(sender, e)
                        ElseIf objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeGift Then
                            IsGiftVoucher = True
                            GiftReceiptMessage = objPaymentByCash.GiftReceiptMessage
                            lblReceivedAmt.Text = CDbl(dsPayment.Tables(0).Compute("Sum(Amount)", ""))
                            BtnSaveSalesOrder_Click(sender, e)
                        End If

                        'End If
                    Else
                        lblminadvancepaid.Text = preMinAmout
                        ShowMessage(getValueByKey("SO070"), "SO070 - " & getValueByKey("CLAE05"))
                    End If
                Else
                    lblminadvancepaid.Text = preMinAmout
                End If
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CM033"), "CM033 - " & getValueByKey("CLAE04"))
            LogException(ex)
            'ShowMessage("Error in Updating cash payment data ", "Information")
        End Try


    End Sub
    Private Sub BtnPayCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            grdSOItems.FinishEditing()

            If boolIsReturn = False Then
                If Not (dsScan.Tables(0).Rows.Count > 0) Then
                    'ShowMessage("Please Scan Article first", "Sales Order Information")
                    ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                    Exit Sub
                ElseIf Not CDbl(CtrlCashSummary1.lbltxt6) > Decimal.Zero Then
                    ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
                Dim Obj As New frmNHowMuchtoPay
                Dim preMinAmout As String = "0"
                Obj.CtrlTxtMinAmt.Text = lblminadvancepaid.Text
                preMinAmout = lblminadvancepaid.Text
                Obj.CtrlTxtPickAmt.Text = PickAmtToPay() 'Math.Round(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "")), 2)
                Obj.CtrlTxtPickAmt.Text = MyRound(Obj.CtrlTxtPickAmt.Text, clsDefaultConfiguration.BillRoundOffAt)
                Obj.TotalBalAmt = CDbl(CtrlCashSummary1.lbltxt6)
                Obj.ctrlTxtHowMuchPay.Text = CDbl(lblminadvancepaid.Text)
                Obj.ShowDialog()

                If Obj.DialogResult = Windows.Forms.DialogResult.Cancel Then
                    lblminadvancepaid.Text = Obj.CtrlTxtMinAmt.Text
                    If Obj.blnAllowtoGoPaymentScreen = False Then
                        Exit Sub
                    End If
                    If Obj.ctrlTxtHowMuchPay.Text = Decimal.Zero Then
                        BtnSaveSalesOrder_Click(sender, e, True)
                        Exit Sub
                    End If
                Else
                    lblminadvancepaid.Text = preMinAmout
                    Exit Sub
                End If
                Dim objPayment As New frmNAcceptPaymentByCard("SO")
                objPayment.TotalBillAmount = CDbl(CtrlCashSummary1.lbltxt6)
                objPayment.TotalMinAmount = CDbl(lblminadvancepaid.Text)
                'objPayment.cboCurrency.SelectedIndex = 1
                objPayment.ShowDialog()
                Dim selectedTenderName As String = objPayment.SelectedTenderName
                Dim strSelectedTenderCode As String = objPayment.CardTenderCode
                objPayment.Close()
                If Not (objPayment.IsCancelAcceptPayment) Then
                    If Not objPayment.ReciptTotalAmount Is Nothing And objPayment.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                        _dsPayment = objPayment.ReciptTotalAmount
                        'Dim ds As New DataSet()
                        'ds.Tables.Add(dt)
                        objPayment.Close()
                        'If Not ds Is Nothing Then
                        If objPayment.Action = My.Resources.AcceptPaymentActionTypeSave Then
                            lblReceivedAmt.Text = CDbl(dsPayment.Tables(0).Compute("Sum(Amount)", ""))
                            BtnSaveSalesOrder_Click(sender, e)
                        End If
                        If objPayment.Action = My.Resources.AcceptPaymentActionTypeGift Then
                            lblReceivedAmt.Text = CDbl(dsPayment.Tables(0).Compute("Sum(Amount)", ""))
                            IsGiftVoucher = True
                            GiftReceiptMessage = objPayment.GiftReceiptMessage
                            BtnSaveSalesOrder_Click(sender, e)
                        End If
                    Else
                        lblminadvancepaid.Text = preMinAmout
                        ShowMessage(getValueByKey("SO070"), "SO070 - " & getValueByKey("CLAE04"))
                    End If
                Else
                    lblminadvancepaid.Text = preMinAmout
                End If
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CM038"), "CM038 - " & getValueByKey("CLAE04"))
            LogException(ex)
        End Try

    End Sub
    Private Sub BtnPayCheque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            grdSOItems.FinishEditing()

            If boolIsReturn = False Then
                If Not (dsScan.Tables(0).Rows.Count > 0) Then
                    'ShowMessage("Please Scan Article first", "Sales Order Information")
                    ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                    Exit Sub
                ElseIf Not CDbl(CtrlCashSummary1.lbltxt6) > Decimal.Zero Then
                    ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
                Dim Obj As New frmNHowMuchtoPay
                Dim preMinAmout As String = "0"
                Obj.CtrlTxtMinAmt.Text = lblminadvancepaid.Text
                preMinAmout = lblminadvancepaid.Text
                Obj.CtrlTxtPickAmt.Text = PickAmtToPay() 'Math.Round(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "")), 2)
                Obj.CtrlTxtPickAmt.Text = MyRound(Obj.CtrlTxtPickAmt.Text, clsDefaultConfiguration.BillRoundOffAt)
                Obj.TotalBalAmt = CDbl(CtrlCashSummary1.lbltxt6)
                Obj.ctrlTxtHowMuchPay.Text = CDbl(lblminadvancepaid.Text)
                Obj.ShowDialog()

                If Obj.DialogResult = Windows.Forms.DialogResult.Cancel Then
                    lblminadvancepaid.Text = Obj.CtrlTxtMinAmt.Text
                    If Obj.blnAllowtoGoPaymentScreen = False Then
                        Exit Sub
                    End If
                    If Obj.ctrlTxtHowMuchPay.Text = Decimal.Zero Then
                        BtnSaveSalesOrder_Click(sender, e, True)
                        Exit Sub
                    End If
                Else
                    lblminadvancepaid.Text = preMinAmout
                    Exit Sub
                End If
                Dim objCheck As New frmNCheckPayment("SO")
                objCheck.BillAmount = CDbl(CtrlCashSummary1.lbltxt6)
                objCheck.TotalMinAmount = CDbl(lblminadvancepaid.Text)
                objCheck.ShowDialog()

                'If objCheck.CheckAmount > 0 Then
                '    objCheck.Close()
                '    Dim objPayment As New frmNAcceptPayment()
                '    objPayment.Show()
                '    objPayment.TotalBillAmount = CtrlCashSummary1.lbltxt4
                '    objPayment.Enabled = False
                '    objPayment.cboRecieptType.SelectedValue = "Cheque"
                '    objPayment.TotalBillAmount = CtrlCashSummary1.lbltxt4
                '    'objPayment.cboCurrency.SelectedIndex = 1
                '    objPayment.InsertCheque(objCheck.CheckAmount, objCheck.CheckNo, objCheck.CheckDate, objCheck.MicrNo, objCheck.BankName)
                '    _dsPayment = New DataSet
                '    _dsPayment = objPayment.ReciptTotalAmount()
                '    objPayment.Close()
                '    If dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables(0).Rows.Count > 0 Then
                '        CalculateSalesOrderSummory(dsScan)
                '        BtnSaveSalesOrder_Click(sender, e)
                '    End If
                'End If
                If objCheck.IsCancelAcceptPayment = False Then
                    If Not objCheck.ReciptTotalAmount Is Nothing And objCheck.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                        _dsPayment = New DataSet
                        _dsPayment = objCheck.ReciptTotalAmount
                        'Dim ds As New DataSet()
                        'ds.Tables.Add(dt)
                        objCheck.Close()
                        'If Not ds Is Nothing Then
                        If dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables(0).Rows.Count > 0 Then
                            If objCheck.Action = My.Resources.AcceptPaymentActionTypeSave Then
                                CalculateSalesOrderSummory(dsScan)
                                lblReceivedAmt.Text = CDbl(dsPayment.Tables(0).Compute("Sum(Amount)", ""))
                                BtnSaveSalesOrder_Click(sender, e)
                            ElseIf objCheck.Action = My.Resources.AcceptPaymentActionTypeGift Then
                                CalculateSalesOrderSummory(dsScan)
                                IsGiftVoucher = True
                                GiftReceiptMessage = objCheck.GiftReceiptMessage
                                lblReceivedAmt.Text = CDbl(dsPayment.Tables(0).Compute("Sum(Amount)", ""))
                                BtnSaveSalesOrder_Click(sender, e)
                            End If

                        End If
                    Else
                        lblminadvancepaid.Text = preMinAmout
                        ShowMessage(getValueByKey("SO070"), "SO070 - " & getValueByKey("CLAE05"))
                    End If
                Else
                    lblminadvancepaid.Text = preMinAmout
                End If
            End If

        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Add Other Charges and Tax in Sales Order
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnAddOtherCharges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles BtnSOOtherCharges.Click
        Try
            If dsScan.Tables(0).Rows.Count > 0 Then
                If IsApplyPromotion = True Then
                    'RemoveApplyPromotion(_dsScan)
                End If
            Else
                ShowMessage(getValueByKey("SO027"), "SO027 - " & getValueByKey("CLAE04"))
                'ShowMessage("Please Select Sales Order first", "Sales Order Information")
                Exit Sub
            End If

            Dim objOpenAddOtherCharges As New frmNAddOthrChrgForSO
            Dim dt As DataTable = dsMain.Tables("SalesOrderOtherCharges").Copy()
            If dtOtherCharges.Rows.Count <= 0 Then
                dtOtherCharges = dt
            End If
            'If Not dsMain.Tables("SalesOrderOtherCharges") Is Nothing AndAlso dsMain.Tables("SalesOrderOtherCharges").Rows.Count > 0 Then
            objOpenAddOtherCharges.dtOtherCharge = dtOtherCharges.Copy()
            'End If
            objOpenAddOtherCharges.SalesOrderNo = vSalesNo
            objOpenAddOtherCharges.ShowDialog()
            If objOpenAddOtherCharges.CancelOthercharges = True Then
                Exit Sub
            End If
            dtOtherCharges = objOpenAddOtherCharges.dtOtherCharge
            'dsMain.Tables.Remove("SalesOrderOtherCharges")
            'dsMain.Tables.Add(dtOtherCharges)
            CalculateSalesOrderSummory(dsScan)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Applying the Promotion in Current Sales Order
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Manual Promotion may be(%,fixed price off,Fixed price sale) </remarks>
    Private Sub cmdDefaultPromo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDefaultPromo.Click
        Try
            If IsQuantityChange = False Then
                ShowMessage(getValueByKey("SO043"), "SO043 - " & getValueByKey("CLAE04"))
                'ShowMessage("Please Change Order Quantity first", "Sales Order Information")
                Exit Sub
            End If

            If Not (dsScan.Tables(0).Rows.Count > 0) Then
                ShowMessage(getValueByKey("SO027"), "SO027 - " & getValueByKey("CLAE04"))
                'ShowMessage("Please Select Sales Order first", "Sales Order Information")
                Exit Sub
            End If

            dsScanProm.Tables(0).Clear()
            Dim dvProm As DataView
            dvProm = New DataView(dsScan.Tables(0), "IsStatus<>'Deleted'", "", DataViewRowState.CurrentRows)

            If dvProm.ToTable.Rows.Count > 0 Then
                dsScanProm.Tables(0).Merge(dvProm.ToTable)
                dsScanProm.AcceptChanges()
                dvProm.Dispose()
            Else
                'ShowMessage("Promotion is not applied properly", "Upchanged Sales Order Items")
            End If

            Dim obj As New clsApplyPromotion
            obj.MainTable = "ItemScanDetails"
            obj.ExclusiveTaxFieldName = "ExclTaxAmt"
            obj.TotalDiscountField = "Discount"
            obj.GrossAmtField = "GrossAmt"
            isPromotionApplied = True
            'If clsDefaultConfiguration.IsPromotionManually = True Then

            '    'If MsgBox(getValueByKey("SO021"), MsgBoxStyle.YesNo, "SO021") = MsgBoxResult.Yes Then
            '    If UCase(sender.id) = UCase("cmdApplySelectedPromo") Then
            '        Dim dtList As DataTable
            '        dtList = obj.GetListofActivePromotions(vSiteCode)

            '        If Not dtList Is Nothing Then
            '            Dim objView As New frmNCommonSearch
            '            objView.SetData = dtList
            '            objView.ShowDialog()

            '            If Not objView.search Is Nothing Then
            '                Dim offerno As String = objView.search(0)

            '                If obj.CheckValidations(offerno) = True Then
            '                    Dim dtValidation As DataTable = obj.GetAllQuestions(offerno)
            '                    Dim StrQues As String = ""

            '                    For Each dr As DataRow In dtValidation.Rows
            '                        StrQues = StrQues & dr("QuestionName").ToString() & ","
            '                    Next

            '                    If StrQues.Contains("Autho") = True AndAlso StrQues.Contains("Voucher") = True Then
            '                        dsScanProm.Tables(0).Columns("Discount").ColumnName = "TotalDiscount"
            '                        dsScanProm.Tables(0).Columns("ExclTaxAmt").ColumnName = "EXCLUSIVETAX"
            '                        CheckInterTransactionAuth("ORD", dsScanProm.Tables(0))
            '                        dsScanProm.Tables(0).Columns("TotalDiscount").ColumnName = "Discount"
            '                        dsScanProm.Tables(0).Columns("EXCLUSIVETAX").ColumnName = "ExclTaxAmt"
            '                        IsApplyPromotion = True
            '                    ElseIf StrQues.Contains("Autho") = True Then
            '                        If CheckInterTransactionAuth("DAUTH", _dsScan.Tables(0)) = True Then
            '                            obj.ApplySelectedPromotion(offerno, dsScanProm, vSiteCode)
            '                            IsApplyPromotion = True
            '                        End If
            '                    End If
            '                Else
            '                    obj.ApplySelectedPromotion(offerno, dsScanProm, vSiteCode)
            '                End If

            '            End If
            '        End If
            '    Else
            '        ShowMessage(getValueByKey("SO022"), "SO022 - " & getValueByKey("CLAE04"))
            '        'ShowMessage("Default Schemes is applied Now", "Message")
            '        obj.CalculatedDs(dsScanProm, vSiteCode)
            '        IsApplyPromotion = True
            '    End If
            'Else
            ShowMessage(getValueByKey("SO022"), "SO022 - " & getValueByKey("CLAE04"))
            '    'ShowMessage("Default Schemes is applied Now", "Message")
            obj.CalculatedDs(dsScanProm, vSiteCode)
            IsApplyPromotion = True
            'End If

            ReCalculateSalesOrder()
            CalculateSalesOrderSummory(dsScanProm)
            RefreshLoadSOData()

        Catch ex As Exception
            ShowMessage(getValueByKey("SO023"), "SO023 - " & getValueByKey("CLAE04"))
            LogException(ex)
            'ShowMessage("Promotion is not applied properly", "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Calculate Sales Order Summary and Show in Screen
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReCalculateSalesOrder()
        Try
            For Each drDisc As DataRow In _dsScan.Tables(0).Select("IsStatus <>'Deleted'")
                drProm = dsScanProm.Tables(0).Select("EAN = '" & drDisc("EAN") & "' And IsStatus <>'Deleted'")
                If drProm.Length > 0 Then

                    drDisc("TotalDiscPercentage") = Math.Round(drProm(0).Item("TotalDiscPercentage"), 3)
                    drDisc("Discount") = Math.Round(drProm(0).Item("Discount"), 2)
                    drDisc("PromotionId") = IIf(drProm(0).Item("FirstLevel") = "", 0, drProm(0).Item("FirstLevel")) & "," & IIf(drProm(0).Item("TopLevel") = "", 0, drProm(0).Item("TopLevel"))

                    If drDisc("PromotionId") = "0,0" Then
                        drDisc("LineDiscount") = Math.Round((drProm(0).Item("GrossAmt") * drProm(0).Item("TotalDiscPercentage")) / 100, 3)
                    Else
                        drDisc("LineDiscount") = Math.Round(drProm(0).Item("LineDiscount"), 3)
                    End If
                    If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                        Dim totalamt As Double = drProm(0).Item("GrossAmt") - drDisc("Discount")
                        Dim objcom As New clsSaleOrderCommon
                        objcom.CreateDataSetForTaxCalculation(drDisc("ARTICLECODE"), totalamt, drDisc, dsMain, CtrlSalesInfo.CtrlTxtOrderNo.Text, drDisc("EAN"))
                    End If
                    drDisc("NetAmount") = Math.Round(drProm(0).Item("GrossAmt") - drDisc("LineDiscount"), 2)

                    TotalSalesQty = drDisc("PickUpQty") + drDisc("DeliveredQty")
                    NetArticleRate = drDisc("NetAmount") / drDisc("Quantity")
                    drDisc("MinPayAmt") = Math.Round(((drDisc("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate), 3)

                    drDisc("FirstLevel") = drProm(0).Item("FirstLevel")
                    drDisc("TopLevel") = drProm(0).Item("TopLevel")
                End If
            Next
            _dsScan.AcceptChanges()

        Catch ex As Exception
            ShowMessage(getValueByKey("SO044"), "SO044 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

#End Region
    Private Sub BtnSOPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'ShowMessage(getValueByKey("SO025"), "SO025")
        'If Not drDelvAdds Is Nothing Then
        '    PrintSalesOrders(drSiteInfo, drHomeAdds, dsMain.Tables("SalesOrderOtherCharges"))
        'Else

        'Rakesh-21.10.2013-8252: Display message as Please select order first!
        If (dsScan.Tables("ItemScanDetails") Is Nothing OrElse dsScan.Tables("ItemScanDetails").Rows.Count < 1) Then
            ShowMessage(getValueByKey("frmnquotationupdate.Warning"), getValueByKey("CLAE04"))
        Else
            PrintSalesOrders(drSiteInfo, drHomeAdds, drDelvAdds)
        End If

        'ResetSalesOrder()
        'CtrlSalesInfo.CtrlTxtOrderNo.Text = String.Empty
        'AutoLogout(FrmTranCode, Me, lblLoggedIn)
        'End If

        'ShowMessage("Print Sales Order service currently not available", "Print Sales Order Informaion")
    End Sub
    Private Sub BtnSOStockCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'ShowMessage(getValueByKey("SO024"), "SO024 - " & getValueByKey("CLAE05"))
        'ShowMessage("Stock Check service currently not available", "Stock Check Informaion")
    End Sub
    Private Sub BtnSOCalculater_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'ShowMessage("Calculator service currently not available", "Calculator Informaion")
    End Sub
    Private Sub BtnSOClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles BtnSOClose.Click
        If dsScan.Tables(0).Rows.Count > 0 Then

            If MsgBox(getValueByKey("SO045"), MsgBoxStyle.YesNo, "SO045 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub
    Private Sub BtnSOCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles BtnSOCancel.Click
        BtnSOSave.Visible = True
        BtnSOPrint.Visible = True
        rbnGrpCMPromotion.Visible = False
        BtnSOAcceptPayment.Visible = True
        BtnSOOtherCharges.Visible = True

        'BtnSOReturn.Visible = True
        BtnSOCancel.Visible = False
        LbReturnReason.Visible = False
        txtReturnReason.Visible = False

        'LbItemScan.Visible = True
        CtrlSalesPerson.CtrlTxtBox.Visible = True
        BtnSearchItem.Visible = True

        'TabSalesOrder.TabPages.Add(tabSales)
        'TabSalesOrder.TabPages.Remove(tabReturn)

        'TabSalesOrder.TabPages.Remove(tabPayment)
        'TabSalesOrder.TabPages.Add(tabPayment)

        IsAllowedSalesReturn = False

        vDocType = vDocTypeCreation
    End Sub
    Private Sub BtnSOReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles BtnSOReturn.Click
        If CheckInterTransactionAuth("SOReturn", dsScan.Tables(0), 0, 0, 0, 0) = True Then

            BtnSOSave.Visible = True
            BtnSOPrint.Visible = False
            rbnGrpCMPromotion.Visible = False
            BtnSOAcceptPayment.Visible = False
            BtnSOOtherCharges.Visible = False

            'BtnSOReturn.Visible = False
            BtnSOCancel.Visible = True
            LbReturnReason.Visible = True
            txtReturnReason.Visible = True
            CtrlSalesInfo.CtrlDtExpDelDate.ReadOnly = True
            'LbItemScan.Visible = False
            'CtrlSalesPerson.CtrlTxtBox.Visible = False
            BtnSearchItem.Visible = False

            'TabSalesOrder.TabPages.Remove(tabSales)
            'TabSalesOrder.TabPages.Add(tabReturn)

            'TabSalesOrder.TabPages.Remove(tabPayment)
            'TabSalesOrder.TabPages.Add(tabPayment)

            'TabSalesOrder.SelectedIndex = 2
            If CtrlBtnReturn.Tag <> "Return" Then
                IsAllowedSalesReturn = True
            Else
                IsAllowedSalesReturn = False
            End If
            GridDeliverdSetting()

            vDocType = vDocTypeReturn
        Else

            ShowMessage(getValueByKey("SO046"), "SO046 - " & getValueByKey("CLAE04"))

            'ShowMessage("You can not return Sales Article beacuse You are not Authorisation. ", "Sales Order Information")
        End If
    End Sub
    Private Sub grdSOItemRetuns_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdSOItemRetuns.AfterEdit

        If grdSOItemRetuns.Cols(grdSOItemRetuns.Col).Name = "PickUpQty" Then
            Try
                Dim vPickupQty As Double = IIf(grdSOItemRetuns.Item(grdSOItemRetuns.Row, "PickupQty") Is DBNull.Value, -1, grdSOItemRetuns.Item(grdSOItemRetuns.Row, "PickupQty"))
                If Not (vPickupQty >= 0) Then
                    ShowMessage(getValueByKey("SO008"), "SO008 - " & getValueByKey("CLAE04"))
                    'ShowMessage("PickUp Quantity cannot less than 1.", "PickUp Quantity Information")
                    grdSOItemRetuns.Item(grdSOItemRetuns.Row, "PickupQty") = 0
                End If

                Dim dvDeliveredQty As New DataView(dsScanReturn.Tables("ItemScanDetails"), "EAN='" & grdSOItemRetuns.Item(grdSOItemRetuns.Row, "EAN") & "'", "", DataViewRowState.CurrentRows)
                If dvDeliveredQty.Count > 0 Then
                    dvDeliveredQty.AllowEdit = True

                    For Each drPickupQty As DataRowView In dvDeliveredQty
                        If vPickupQty <= CDbl(drPickupQty("DeliveredQty")) Then
                            drPickupQty("PickupQty") = grdSOItemRetuns.Item(grdSOItemRetuns.Row, "PickupQty")
                            drPickupQty("IsStatus") = "Return"
                        Else
                            grdSOItemRetuns.Item(grdSOItemRetuns.Row, "PickupQty") = 0
                            'ShowMessage("Return Quantity (" & vPickupQty & ") cannot greater than Delivered Quantity (" & CDbl(drPickupQty("DeliveredQty")) & ").", "Return Article Information")
                            ShowMessage(String.Format(getValueByKey("SO083"), vPickupQty, CDbl(drPickupQty("DeliveredQty"))), "SO083 - " & getValueByKey("CLAE04"))
                        End If
                    Next

                    dsScanReturn.AcceptChanges()
                End If

            Catch ex As Exception
                ShowMessage(ex.Message, getValueByKey("CLAE05"))
                LogException(ex)
            End Try
        End If

    End Sub
    Private Sub BtnSOCloseManualSO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles BtnSOCloseManualSO.Click
        'If clsDefaultConfiguration.IsBatchManagementReq Then
        '    MessageBox.Show("Please close Sales order from Outbound Screen")
        '    Exit Sub
        'Else

        objSO.IsMannualClose = True
        objSO.SelectedCurrencyIndex = clsAdmin.CurrencyCode
        objSO.CurrencyCode = clsAdmin.CurrencyCode
        isLeaved = True
        Dim crdsale As Double = 0.0
        Dim crdsaleadjustamount As Double = 0.0
        Dim PaymentToBeSettled As Double = 0.0
        Dim PenaltyAmount As Double = 0.0
        Dim pendingCreditSale As Double = 0.0
        Dim pickupamount As Double = 0.0
        Dim cashpaid As Double = 0
        If Not (dsScan.Tables("ItemScanDetails") Is Nothing) AndAlso dsScan.Tables("ItemScanDetails").Rows.Count > 0 Then

            If (String.IsNullOrEmpty(vSalesNo) Or CtrlSalesInfo.CtrlTxtOrderNo.Text.Trim <> vSalesNo Or grdSOItems.Rows.Count <= 1) Then
                ShowMessage(getValueByKey("frmnquotationupdate.Warning"), getValueByKey("CLAE04"))
                Exit Sub
            End If

            If MsgBox(getValueByKey("SO078"), MsgBoxStyle.YesNo, "SO078 - " & getValueByKey("CLAE04")) = MsgBoxResult.No Then
                Exit Sub
            End If

            Dim articlePrice = Decimal.Zero, pendingTotalNetAmount = Decimal.Zero, advanceAmountPaid = Decimal.Zero, CVIssueAmount As Decimal = Decimal.Zero
            Try
                For Each drDtls As DataRow In dsMain.Tables("SalesOrderDtl").Rows

                    articlePrice = drDtls("NetAmount") / drDtls("Quantity")
                    pendingTotalNetAmount += articlePrice * (drDtls("Quantity") - drDtls("DeliveredQty"))
                    pickupamount += articlePrice * drDtls("DeliveredQty")
                Next

                advanceAmountPaid = pendingTotalNetAmount - dsMain.Tables("SalesOrderHDR").Rows(0)("BalanceAmount")

                Dim advancecreitsale = dsMain.Tables("SalesOrderHDR").Rows(0)("AdvanceAmt") - 100

                cashpaid = dsMain.Tables("SalesOrderHDR").Rows(0)("AdvanceAmt")


                If (clsDefaultConfiguration.PenaltyPercentageInClose = 0) Then
                    ' Comment by ketan issue when close the SO return amt not correct
                    'CVIssueAmount = advanceAmountPaid + advancecreitsale
                    CVIssueAmount = advanceAmountPaid

                ElseIf (clsDefaultConfiguration.PenaltyPercentageInClose > 0) Then

                    If (advanceAmountPaid = 0) Then
                        CVIssueAmount = ((clsDefaultConfiguration.PenaltyPercentageInClose * pendingTotalNetAmount) / 100) * -1

                    ElseIf (advanceAmountPaid > 0) Then
                        CVIssueAmount = advanceAmountPaid - ((clsDefaultConfiguration.PenaltyPercentageInClose * pendingTotalNetAmount) / 100)
                    End If
                End If

                Dim salesordernumber As String = ""
                If dsMain.Tables("SalesInvoice").Rows.Count > 0 AndAlso Not dsMain.Tables("SalesInvoice") Is Nothing Then
                    If dsMain.Tables("SalesInvoice").Select("TenderHeadCode='Credit Sales'").Length > 0 Then
                        Dim dt As DataTable = dsMain.Tables("SalesInvoice").Select("TenderHeadCode='Credit Sales'").CopyToDataTable
                        If dt.Rows.Count > 0 AndAlso Not dt Is Nothing Then
                            salesordernumber = dt.Rows(0)("DocumentNumber")
                            For Each x In dt.Rows
                                crdsale += x("AmountTendered")

                            Next

                        End If
                    End If
                End If
                If crdsale > 0 Then
                    cashpaid = cashpaid - crdsale
                End If
                Dim dtCreditSaleData As New DataTable
                Dim objclsReturn As New clsCashMemoReturn
                dtCreditSaleData = objclsReturn.getCreditSaleBillData("'" + salesordernumber + "'")
                If dtCreditSaleData.Rows.Count > 0 AndAlso Not dtCreditSaleData Is Nothing Then
                    For Each y In dtCreditSaleData.Rows
                        crdsaleadjustamount += y("CreditSaleAdjustment")
                    Next
                End If


                PenaltyAmount = ((clsDefaultConfiguration.PenaltyPercentageInCancel * pendingTotalNetAmount) / 100)

                If crdsale > 0 Then
                    PaymentToBeSettled = pickupamount - cashpaid - crdsaleadjustamount + PenaltyAmount
                Else
                    PaymentToBeSettled = CVIssueAmount
                End If

                pendingCreditSale = crdsale - crdsaleadjustamount

            Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

            If crdsale <> 0 Then
                If MsgBox(String.Format(getValueByKey("SO092"),
                                 MyRound(pendingTotalNetAmount, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired),
                                MyRound(cashpaid, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired),
                                      crdsaleadjustamount,
                                pendingCreditSale,
                                 clsDefaultConfiguration.PenaltyPercentageInClose,
                     MyRound(PenaltyAmount, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired),
                                 MyRound(PaymentToBeSettled, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)),
                             MsgBoxStyle.YesNo, "SO092 - " & getValueByKey("SO087")) = MsgBoxResult.No Then
                    Exit Sub
                End If
            Else
                If MsgBox(String.Format(getValueByKey("SO088"),
                                        MyRound(pendingTotalNetAmount, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired),
                                        MyRound(cashpaid, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired),
                                        clsDefaultConfiguration.PenaltyPercentageInClose,
                                        MyRound(CVIssueAmount * -1, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)),
                                    MsgBoxStyle.YesNo, "SO078 - " & getValueByKey("SO087")) = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If

            Try
                If crdsale > 0 Then
                    CVIssueAmount = MyRound(PaymentToBeSettled, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)
                Else
                    CVIssueAmount = MyRound(CVIssueAmount * -1, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)
                End If
                If CVIssueAmount <> 0 Then
                    Dim objPayment As New frmNAcceptPayment()
                    getclpsettings()
                    objPayment.TotalBillAmount = CVIssueAmount
                    objPayment.AcceptEditBillDataSet = dsPayment
                    objPayment.PaymentType = clsAcceptPayment.PaymentType.Accept
                    objPayment.ShowDialog()

                    _dsPayment = New DataSet
                    _dsPayment = objPayment.ReciptTotalAmount()
                    objPayment.Close()

                    If (objPayment.IsCancelAcceptPayment) Then
                        Return
                    End If
                End If

                dsMain.Tables("SalesOrderHDR").RejectChanges()
                dsMain.Tables("SalesOrderHDRAudit").RejectChanges()
                dsMain.Tables("SalesOrderDTL").RejectChanges()
                dsMain.Tables("SalesOrderDTLAudit").RejectChanges()
                dsMain.Tables("SalesOrderTaxDtls").RejectChanges()
                dsMain.Tables("SalesOrderOtherCharges").RejectChanges()
                dsMain.Tables("SalesInvoice").RejectChanges()
                dsMain.Tables("OrderHdr").RejectChanges()
                dsMain.Tables("OrderDtl").RejectChanges()

                dsMain.Tables("SalesOrderHDR").Rows(0).Item("PenaltyAmount") = IIf(CVIssueAmount > 0, CVIssueAmount, 0)
                dsMain.Tables("SalesOrderHDR").Rows(0).Item("SOStatus") = "Closed"
                dsMain.Tables("SalesOrderHDR").Rows(0).Item("UPDATEDON") = vCurrentDate
                dsMain.Tables("SalesOrderHDR").Rows(0).Item("UPDATEDBY") = clsAdmin.UserCode
                dsMain.Tables("SalesOrderHDR").Rows(0).Item("UPDATEDAT") = clsAdmin.SiteCode

                If Not (PrepareInvcDataforSave(dsMain) = True) Then
                    Exit Sub
                End If

                If objSO.PrepareSaveData(vSalesInvcNo, clsAdmin.DayOpenDate, clsAdmin.CLPProgram, CtrlCustDtls1.lblCustNo.Text, dsMain, False, IsNextInvoiceNo, vSiteCode, CtrlSalesInfo.CtrlTxtOrderNo.Text, clsDefaultConfiguration.StockStorageLocation, clsAdmin.CVProgram, "SalesOrder", vfinancialYear, clsAdmin.UserCode, clsAdmin.CurrentDate, IsOutboundCreated) = True Then

                    If Not dsPayment.Tables("MSTRecieptType") Is Nothing AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                        Dim totalPay As Decimal
                        For Each dr As DataRow In dsPayment.Tables("MSTRecieptType").Select("RecieptTypeCode='CreditVouc(I)'", "", DataViewRowState.CurrentRows)
                            totalPay = IIf(dr("Amount") > 0, dr("Amount"), dr("Amount") * -1)

                            clsVoucher.PrintGiftVoucherAndCreditNote("SalesOrder", clsAdmin.SiteCode, "CreditNote", String.Empty, totalPay, String.Empty, clsAdmin.UserName, CtrlSalesInfo.CtrlTxtOrderNo.Text, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                        Next
                    End If

                    ShowMessage(getValueByKey("SO039"), "SO039 - " & getValueByKey("CLAE04"))

                    ResetSalesOrder()
                    isLeaved = False
                    CtrlSalesInfo.CtrlTxtOrderNo.Text = String.Empty
                    objSO.IsMannualClose = False
                Else
                    ShowMessage(getValueByKey("SO040"), "SO040 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Sales Order does not Updated", "Sales Order")
                End If

            Catch ex As Exception
                ShowMessage(ex.Message, getValueByKey("CLAE05"))
                LogException(ex)
            End Try

        End If
        'End If

    End Sub
    Private Sub CtrlBtnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlBtnReturn.Click
        If (vSalesNo = "" Or vSalesNo = String.Empty) Then
            ShowMessage(getValueByKey("frmnquotationupdate.Warning"), getValueByKey("CLAE04"))
            Exit Sub
        End If

        If OnlineConnect = False Then
            ShowMessage(getValueByKey("SOO84"), "SOO84 - " & getValueByKey("CLAE04"))
            Exit Sub
        End If
        If CtrlBtnReturn.Tag = "Return" Or CtrlBtnReturn.Tag = "" Then
            TabSalesOrder.TabPages("TabPageItemDetailsReturn").TabVisible = True
            TabSalesOrder.TabPages("TabPageItemDetailsReturn").Select()
            TabSalesOrder.TabPages("TabPageItemDetails").TabVisible = False
            CtrlBtnReturn.Text = "Cancel Return"
            CtrlBtnReturn.Tag = "Cancel Return"
            TabSalesOrder.SelectedTab = TabSalesOrder.TabPages("TabPageItemDetailsReturn")
            vDocType = vDocTypeReturn
            boolIsReturn = True
        Else
            TabSalesOrder.TabPages("TabPageItemDetailsReturn").TabVisible = False
            TabSalesOrder.TabPages("TabPageItemDetails").TabVisible = True
            TabSalesOrder.TabPages("TabPageItemDetails").Select()
            TabSalesOrder.SelectedTab = TabSalesOrder.TabPages("TabPageItemDetails")
            CtrlBtnReturn.Text = "Return"
            CtrlBtnReturn.Tag = "Return"
            vDocType = vDocTypeCreation
            boolIsReturn = False
        End If
    End Sub
    Private Sub GridItemSetting()
        Try

            For colno = 1 To grdSOItems.Cols.Count - 1
                If grdSOItems.Cols(colno).Name.ToUpper() <> "DISCRIPTION".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "EAN".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "ArticleCode".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "SellingPrice".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "Quantity".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "TOTALDISCPERCENTAGE".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "IsCLP".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "TotalDiscount".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "PickUpQty".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "DEL".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "ExpDelDate".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "ReservedQty".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "TotalTaxAmt".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "DeliverySiteCode".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "DeliveredQty".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "EditBarcode".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "NetAmount".ToUpper() Then
                    HideColumns(grdSOItems, False, grdSOItems.Cols(colno).Name)
                End If
            Next
            grdSOItems.Cols("Del").Caption = ""
            grdSOItems.Cols("Del").Width = 20
            grdSOItems.Cols("Del").ComboList = "..."

            If (clsDefaultConfiguration.BarcodeDisplayAllowed) Then
                grdSOItems.Cols("EAN").Caption = getValueByKey("frmnsalesorderupdate.grdsoinvoice.ean")
                grdSOItems.Cols("EAN").Width = 90
                grdSOItems.Cols("EAN").AllowEditing = False
                grdSOItems.Cols("EAN").Visible = True
            Else
                grdSOItems.Cols("EAN").Visible = False
            End If

            grdSOItems.Cols("ArticleCode").Caption = getValueByKey("frmnsalesorderupdate.grdsoitems.articlecode")
            grdSOItems.Cols("ArticleCode").Width = 90
            grdSOItems.Cols("ArticleCode").AllowEditing = False
            grdSOItems.Cols("Discription").Caption = getValueByKey("frmnsalesorderupdate.grdsoitems.discription")
            grdSOItems.Cols("Discription").Width = 150
            grdSOItems.Cols("Discription").AllowEditing = False
            grdSOItems.Cols("SellingPrice").Caption = getValueByKey("frmnsalesorderupdate.grdsoitems.sellingprice")
            grdSOItems.Cols("SellingPrice").Width = 60
            grdSOItems.Cols("SellingPrice").AllowEditing = False
            grdSOItems.Cols("Quantity").Caption = getValueByKey("frmnsalesorderupdate.grdsoitems.quantity")
            grdSOItems.Cols("Quantity").Width = 45
            grdSOItems.Cols("Quantity").Format = "0.000"
            'grdSOItems.Cols("Quantity").EditMask = "999999999"
            grdSOItems.Cols("PickUpQty").Caption = getValueByKey("frmnsalesorderupdate.grdsoitems.pickupqty")
            grdSOItems.Cols("PickUpQty").Width = 45
            'grdSOItems.Cols("PickUpQty").EditMask = "999999999"
            grdSOItems.Cols("PickUpQty").Format = "0.000"
            If clsDefaultConfiguration.IsBatchManagementReq Then
                grdSOItems.Cols("PickUpQty").AllowEditing = False
            End If
            grdSOItems.Cols("DeliveredQty").Caption = getValueByKey("frmnsalesorderupdate.grdsoitems.deliveredqty")
            grdSOItems.Cols("DeliveredQty").Width = 45
            grdSOItems.Cols("DeliveredQty").Format = "0.000"
            grdSOItems.Cols("DeliveredQty").AllowEditing = False
            grdSOItems.Cols("TOTALDISCPERCENTAGE").Caption = getValueByKey("frmnsalesorderupdate.grdsoitems.totaldiscpercentage")
            grdSOItems.Cols("TOTALDISCPERCENTAGE").Width = 45
            grdSOItems.Cols("TOTALDISCPERCENTAGE").Format = "0.00"
            grdSOItems.Cols("TOTALDISCPERCENTAGE").AllowEditing = False
            grdSOItems.Cols("NetAmount").Caption = getValueByKey("frmnsalesorderupdate.grdsoitems.netamount")
            grdSOItems.Cols("NetAmount").Width = 70
            grdSOItems.Cols("NetAmount").AllowEditing = False
            grdSOItems.Cols("ExpDelDate").Caption = getValueByKey("frmnsalesorderupdate.grdsoitems.expdeldate")
            grdSOItems.Cols("ExpDelDate").Width = 120
            grdSOItems.Cols("ExpDelDate").Format = "g"

            grdSOItems.Cols("Stock").Caption = getValueByKey("frmnsalesorderupdate.grdsoitems.stock")
            grdSOItems.Cols("Stock").Width = 45
            grdSOItems.Cols("Stock").AllowEditing = False
            grdSOItems.Cols("IsCLP").Caption = getValueByKey("frmnsalesorderupdate.grdsoitems.isclp")
            grdSOItems.Cols("IsCLP").Width = 45
            grdSOItems.Cols("IsCLP").DataType = Type.GetType("System.Boolean")
            grdSOItems.Cols("ReservedQty").Caption = getValueByKey("frmnsalesorderupdate.grdsoitems.reservedqty")
            grdSOItems.Cols("ReservedQty").Width = 45
            grdSOItems.Cols("ReservedQty").Format = "0"
            grdSOItems.Cols("ReservedQty").DataType = Type.GetType("System.Boolean")
            grdSOItems.Cols("ReservedQty").AllowEditing = False

            grdSOItems.Cols("IsStatus").Visible = False
            grdSOItems.Cols("TotalTaxAmt").Caption = getValueByKey("frmnsalesorderupdate.grdsoitems.excltaxamt")
            grdSOItems.Cols("TotalTaxAmt").Format = "0.00"
            grdSOItems.Cols("TotalTaxAmt").Width = 45
            grdSOItems.Cols("TotalTaxAmt").AllowEditing = False
            'If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            '    For i = 0 To grdSOItems.Cols.Count - 1
            '        grdSOItems.Cols(i).Caption = grdSOItems.Cols(i).Caption.ToUpper
            '    Next
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub GridDeliverdSetting()
        Try
            'grdSOItemRetuns.Cols("Del").Caption = ""
            'grdSOItemRetuns.Cols("Del").Width = 20
            grdSOItemRetuns.Cols("Del").Visible = False
            For colno = 1 To grdSOItems.Cols.Count - 1
                If grdSOItemRetuns.Cols(colno).Name.ToUpper() <> "DISCRIPTION".ToUpper() _
                    AndAlso grdSOItemRetuns.Cols(colno).Name.ToUpper() <> "ArticleCode".ToUpper() _
                    AndAlso grdSOItemRetuns.Cols(colno).Name.ToUpper() <> "SellingPrice".ToUpper() _
                    AndAlso grdSOItemRetuns.Cols(colno).Name.ToUpper() <> "Quantity".ToUpper() _
                    AndAlso grdSOItemRetuns.Cols(colno).Name.ToUpper() <> "TotalDiscPercentage".ToUpper() _
                    AndAlso grdSOItemRetuns.Cols(colno).Name.ToUpper() <> "PickUpQty".ToUpper() _
                    AndAlso grdSOItemRetuns.Cols(colno).Name.ToUpper() <> "IsCLP".ToUpper() _
                    AndAlso grdSOItemRetuns.Cols(colno).Name.ToUpper() <> "TotalDiscount".ToUpper() _
                    AndAlso grdSOItemRetuns.Cols(colno).Name.ToUpper() <> "DEL".ToUpper() _
                    AndAlso grdSOItemRetuns.Cols(colno).Name.ToUpper() <> "ExpDelDate".ToUpper() _
                    AndAlso grdSOItemRetuns.Cols(colno).Name.ToUpper() <> "ReservedQty".ToUpper() _
                    AndAlso grdSOItemRetuns.Cols(colno).Name.ToUpper() <> "TotalTaxAmt".ToUpper() _
                    AndAlso grdSOItemRetuns.Cols(colno).Name.ToUpper() <> "DeliveredQty".ToUpper() _
                    AndAlso grdSOItemRetuns.Cols(colno).Name.ToUpper() <> "EditBarcode".ToUpper() _
                    AndAlso grdSOItemRetuns.Cols(colno).Name.ToUpper() <> "NetAmount".ToUpper() Then
                    HideColumns(grdSOItemRetuns, False, grdSOItemRetuns.Cols(colno).Name)
                End If
            Next
            grdSOItemRetuns.Cols("ArticleCode").Caption = getValueByKey("frmnsalesorderupdate.grdsoitemretuns.articlecode")
            grdSOItemRetuns.Cols("ArticleCode").Width = 90
            grdSOItemRetuns.Cols("ArticleCode").AllowEditing = False
            grdSOItemRetuns.Cols("EAN").Caption = getValueByKey("frmnsalesorderupdate.grdsoitemretuns.ean")
            grdSOItemRetuns.Cols("EAN").Width = 90
            grdSOItemRetuns.Cols("EAN").Visible = False
            grdSOItemRetuns.Cols("Discription").Caption = getValueByKey("frmnsalesorderupdate.grdsoitemretuns.discription")
            grdSOItemRetuns.Cols("Discription").Width = 150
            grdSOItemRetuns.Cols("SellingPrice").Caption = getValueByKey("frmnsalesorderupdate.grdsoitemretuns.sellingprice")
            grdSOItemRetuns.Cols("SellingPrice").Width = 60
            grdSOItemRetuns.Cols("Quantity").Caption = getValueByKey("frmnsalesorderupdate.grdsoitemretuns.quantity")
            grdSOItemRetuns.Cols("Quantity").Width = 45
            grdSOItemRetuns.Cols("Quantity").DataType = Type.GetType("System.Decimal")
            grdSOItemRetuns.Cols("Quantity").Format = "0.000"
            'grdSOItemRetuns.Cols("Quantity").EditMask = "999999999"

            grdSOItemRetuns.Cols("PickUpQty").Caption = getValueByKey("frmnsalesorderupdate.grdsoitemretuns.pickupqty")
            grdSOItemRetuns.Cols("PickUpQty").Width = 45
            grdSOItemRetuns.Cols("PickUpQty").DataType = Type.GetType("System.Decimal")
            grdSOItemRetuns.Cols("PickUpQty").Format = "0.000"
            '----- Commented By Mahesh for allowing decimal 3 digits -------
            '-----  grdSOItemRetuns.Cols("PickUpQty").EditMask = "999999999"

            grdSOItemRetuns.Cols("PickUpQty").AllowEditing = Not clsDefaultConfiguration.IsBatchManagementReq
            grdSOItemRetuns.Cols("DeliveredQty").Caption = getValueByKey("frmnsalesorderupdate.grdsoitemretuns.deliveredqty")
            grdSOItemRetuns.Cols("DeliveredQty").Width = 45
            grdSOItemRetuns.Cols("DeliveredQty").Format = "0.000"
            grdSOItemRetuns.Cols("Quantity").DataType = Type.GetType("System.Decimal")
            grdSOItemRetuns.Cols("PickupQty").DataType = Type.GetType("System.Decimal")
            grdSOItemRetuns.Cols("DeliveredQty").DataType = Type.GetType("System.Decimal")

            If clsDefaultConfiguration.WeightScaleEnabled Then
                grdSOItemRetuns.Cols("Quantity").Format = "0.000"
                grdSOItemRetuns.Cols("PickupQty").Format = "0.000"
                grdSOItemRetuns.Cols("DeliveredQty").Format = "0.000"
            Else
                grdSOItemRetuns.Cols("Quantity").Format = "0.00"
                grdSOItemRetuns.Cols("PickupQty").Format = "0.00"
                grdSOItemRetuns.Cols("DeliveredQty").Format = "0.00"

            End If
            grdSOItemRetuns.Cols("ReturnReasonCode").Caption = getValueByKey("frmnsalesorderupdate.grdsoitemretuns.returnreasoncode")
            grdSOItemRetuns.Cols("ReturnReasonCode").Width = 45
            grdSOItemRetuns.Cols("Discount").Caption = getValueByKey("frmnsalesorderupdate.grdsoitemretuns.discount")
            grdSOItemRetuns.Cols("Discount").Width = 45
            grdSOItemRetuns.Cols("NetAmount").Caption = getValueByKey("frmnsalesorderupdate.grdsoitemretuns.netamount")
            grdSOItemRetuns.Cols("NetAmount").Width = 70
            grdSOItemRetuns.Cols("ExpDelDate").Caption = getValueByKey("frmnsalesorderupdate.grdsoitemretuns.expdeldate")
            grdSOItemRetuns.Cols("ExpDelDate").Width = 120
            grdSOItemRetuns.Cols("ExpDelDate").Format = "g"
            grdSOItemRetuns.Cols("Stock").Caption = getValueByKey("frmnsalesorderupdate.grdsoitemretuns.stock")
            grdSOItemRetuns.Cols("Stock").Width = 45
            grdSOItemRetuns.Cols("IsCLP").Caption = getValueByKey("frmnsalesorderupdate.grdsoitemretuns.isclp")
            grdSOItemRetuns.Cols("IsCLP").Width = 45
            grdSOItemRetuns.Cols("ReservedQty").Caption = getValueByKey("frmnsalesorderupdate.grdsoitemretuns.reservedqty")
            grdSOItemRetuns.Cols("ReservedQty").Width = 45
            grdSOItemRetuns.Cols("ReservedQty").Format = "0.000"
            grdSOItems.Cols("ReservedQty").DataType = Type.GetType("System.Boolean")
            grdSOItems.Cols("IsCLP").DataType = Type.GetType("System.Boolean")
            grdSOItemRetuns.Cols("EAN").AllowEditing = False
            grdSOItemRetuns.Cols("Discription").AllowEditing = False
            grdSOItemRetuns.Cols("SellingPrice").AllowEditing = False
            grdSOItemRetuns.Cols("DeliveredQty").AllowEditing = False
            grdSOItemRetuns.Cols("Discount").AllowEditing = False
            grdSOItemRetuns.Cols("NetAmount").AllowEditing = False
            grdSOItemRetuns.Cols("Stock").AllowEditing = False
            grdSOItemRetuns.Cols("ExpDelDate").AllowEditing = False
            grdSOItemRetuns.Cols("Quantity").AllowEditing = False
            grdSOItemRetuns.AutoSizeCols()
            'If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            '    For i = 0 To grdSOItemRetuns.Cols.Count - 1
            '        grdSOItemRetuns.Cols(i).Caption = grdSOItemRetuns.Cols(i).Caption.ToUpper
            '    Next
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub GridInvoiceSetting()
        Try

            grdSOInvoice.Cols("SalesNO").Caption = getValueByKey("frmnsalesorderupdate.grdsoinvoice.salesno")
            grdSOInvoice.Cols("SalesNO").Width = 90
            grdSOInvoice.Cols("InvoiceNO").Caption = getValueByKey("frmnsalesorderupdate.grdsoinvoice.invoiceno")
            grdSOInvoice.Cols("InvoiceNO").Width = 90
            grdSOInvoice.Cols("DocumentType").Caption = getValueByKey("frmnsalesorderupdate.grdsoinvoice.documenttype")
            grdSOInvoice.Cols("DocumentType").Width = 60
            grdSOInvoice.Cols("TerminalID").Caption = getValueByKey("frmnsalesorderupdate.grdsoinvoice.terminalid")
            grdSOInvoice.Cols("TerminalID").Width = 45
            grdSOInvoice.Cols("UserName").Caption = getValueByKey("frmnsalesorderupdate.grdsoinvoice.username")
            grdSOInvoice.Cols("UserName").Width = 90
            grdSOInvoice.Cols("InvoiceDate").Caption = getValueByKey("frmnsalesorderupdate.grdsoinvoice.invoicedate")
            grdSOInvoice.Cols("InvoiceDate").Width = 70
            grdSOInvoice.Cols("TenderType").Caption = getValueByKey("frmnsalesorderupdate.grdsoinvoice.tendertype")
            grdSOInvoice.Cols("TenderType").Width = 45
            grdSOInvoice.Cols("InvoiceAmt").Caption = getValueByKey("frmnsalesorderupdate.grdsoinvoice.invoiceamt")
            grdSOInvoice.Cols("InvoiceAmt").Width = 45
            grdSOInvoice.AutoSizeCols()
            For Each col As C1.Win.C1FlexGrid.Column In grdSOInvoice.Cols
                col.AllowEditing = False
            Next
            'If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            '    For i = 0 To grdSOInvoice.Cols.Count - 1
            '        grdSOInvoice.Cols(i).Caption = grdSOInvoice.Cols(i).Caption.ToUpper
            '    Next
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdSONew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSONew.Click
        Try
            If dsMain.Tables(0).Rows.Count > 0 Then
                If MsgBox(getValueByKey("SO049"), MsgBoxStyle.YesNo, "SO049 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                    IsFormClosing = True
                    Dim frm As New frmNSalesOrderCreation
                    MDISpectrum.ShowChildForm(frm, True)
                End If
            Else
                Dim frm As New frmNSalesOrderCreation
                MDISpectrum.ShowChildForm(frm, True)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub CmdSOEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSOEdit.Click
        Try
            If dsMain.Tables(0).Rows.Count > 0 Then
                If MsgBox(getValueByKey("SO049"), MsgBoxStyle.YesNo, "SO049 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                    IsFormClosing = True
                    Dim frm As New frmNSalesOrderCancel
                    MDISpectrum.ShowChildForm(frm, True)
                End If
            Else
                Dim frm As New frmNSalesOrderCancel
                MDISpectrum.ShowChildForm(frm, True)
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub grdSOItemRetuns_AfterEdit1(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) 'Handles grdSOItemRetuns.AfterEdit
        Try
            If grdSOItemRetuns.Cols(e.Col).Name.ToUpper() = "PickUpQty".ToUpper() Then
                If Not grdSOItemRetuns.Rows(e.Row)("FreezeSR") Is DBNull.Value AndAlso grdSOItemRetuns.Rows(e.Row)("FreezeSR") = True Then
                    grdSOItemRetuns.Rows(e.Row)("PickUpQty") = 0
                    ShowMessage(getValueByKey("SO079"), "SO079 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
                If grdSOItemRetuns.Rows(e.Row)("PickUpQty") > grdSOItemRetuns.Rows(e.Row)("DeliveredQty") Then
                    ShowMessage(getValueByKey("SO072"), "SO072 - " & getValueByKey("CLAE05"))
                    grdSOItemRetuns.Rows(e.Row)("PickUpQty") = 0
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub CtrlBtnStockCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlBtnStockCheck.Click
        Try
            Dim objfrmStockCheck As New frmNStockCheck
            objfrmStockCheck.ShowDialog()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdClearSelectedPromo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearSelectedPromo.Click
        Try
            For Each dr As C1.Win.C1FlexGrid.Row In grdSOItems.Rows.Selected
                If IsApplyPromotion = True Then
                    For Each drdata As DataRow In dsScan.Tables(0).Select("EAN='" & dr("EAN").ToString() & "' AND ArticleCode='" & dr("ArticleCode").ToString() & "'", "", DataViewRowState.CurrentRows)
                        drdata("Discount") = 0
                        drdata("MinPayAmt") = 0
                        drdata("PromotionId") = 0
                        drdata("LineDiscount") = 0
                        drdata("TotalDiscPercentage") = 0
                        drdata("FirstLevel") = String.Empty
                        drdata("TopLevel") = String.Empty
                        Dim obj As New clsSaleOrderCommon
                        obj.RecalculateLine(drdata, CtrlSalesInfo.CtrlTxtOrderNo.Text, dsMain)
                    Next
                End If
            Next
            'ReCalculateSalesOrder()
            CalculateSalesOrderSummory(dsScan)
            RefreshLoadSOData()
            GridItemSetting()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cmdClrAllPromo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClrAllPromo.Click
        Try
            isPromotionApplied = False
            If dsScan.Tables(0).Rows.Count > 0 Then
                If IsApplyPromotion = True Then
                    RemoveApplyPromotion(_dsScan)
                End If
                'ReCalculateSalesOrder()
                CalculateSalesOrderSummory(dsScan)
                RefreshLoadSOData()
                GridItemSetting()
            End If
        Catch ex As Exception

        End Try

    End Sub
    Protected Overrides Function ProcessKeyPreview(ByRef m As System.Windows.Forms.Message) As Boolean
        Const WM_KEYDOWN As Integer = &H100
        If m.Msg = WM_KEYDOWN Then
            Select Case m.WParam.ToInt32
                Case Keys.F
                    If My.Computer.Keyboard.CtrlKeyDown Then
                        'Debug.Print("Ctrl + F")
                        BtnSearchItem_Click(CtrlSalesPerson1.CtrlTxtBox, New KeyEventArgs(Keys.Enter))
                    End If
                Case Keys.M
                    If My.Computer.Keyboard.CtrlKeyDown Then
                        'Debug.Print("Ctrl + M")
                        CtrlCustDtls1.CtrlLabel3_Click(Nothing, New KeyEventArgs(Keys.Enter))
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
                Case Keys.F2
                    ChangeQty()
            End Select
        End If
        Return MyBase.ProcessKeyPreview(m)
    End Function
    Private Function PickAmtToPay() As Double
        '        Dim TotalPickNDelAmt As Double = 0.0
        If Not dsScan.Tables("ItemScanDetails").Compute("SUM(totalpickupamt)", "IsStatus<> 'Deleted' AND PickUpQty>0") Is DBNull.Value Then
            'PickAmtToPay = IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(totalpickupamt)", "IsStatus<> 'Deleted' AND PickUpQty>0") Is DBNull.Value, 0, CDbl(dsScan.Tables("ItemScanDetails").Compute("SUM(totalpickupamt)", "IsStatus<> 'Deleted'")))
            'PickAmtToPay = IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(totalpickupamt)", "IsStatus<> 'Deleted' AND PickUpQty>0") Is DBNull.Value, 0, CDbl(dsScan.Tables("ItemScanDetails").Compute("SUM(totalpickupamt)", "IsStatus<> 'Deleted'")))
            Dim dr() = dsScan.Tables("ItemScanDetails").Select("IsStatus<> 'Deleted' AND PickUpQty>0")
            For Each drPickupQty In dr
                Dim pickupQty = drPickupQty("PickupQty")
                NetArticleRate = Math.Round(drPickupQty("NetAmount"), 3) / drPickupQty("Quantity")
                PickAmtToPay = PickAmtToPay + (pickupQty * NetArticleRate)
            Next
        Else
            PickAmtToPay = 0
        End If

        'If TotalPickNDelAmt > (vAdvanceAmount) Then  ' if total pick + del amount is greater then advance paid amount then must pay as pickup amt else no need to pay
        '    PickAmtToPay = MyRound((TotalPickNDelAmt - vAdvanceAmount), clsDefaultConfiguration.BillRoundOffAt)
        'Else
        '    PickAmtToPay = strZero
        'End If
    End Function
    Private Sub ChangeQty()
        Try
            If grdSOItems.Rows.Count >= 1 Then
                grdSOItems.Focus()
                grdSOItems.Select(1, 4)
            End If

        Catch ex As Exception

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
                    'grdSOItems.Rows(1)("SellingPrice") = IIf(frm.GetResult Is Nothing, 1, frm.GetResult)
                    'Change by Ashish on Dec 3, 2010
                    'Commented the above line to change the price of the selected row and not the first row 
                    grdSOItems.Rows(grdSOItems.RowSel)("SellingPrice") = IIf(frm.GetResult Is Nothing, 1, frm.GetResult)
                    Dim index As Int32 = grdSOItems.Cols("SellingPrice").Index
                    grdSOItems.Select(grdSOItems.RowSel, grdSOItems.Cols("Quantity").Index)
                    'end of change

                    grdSOItems_AfterEdit(grdSOItems, New C1.Win.C1FlexGrid.RowColEventArgs(1, index))
                End If
            End If
        End If
    End Sub


    Private Sub grdSOItems_ValidateEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.ValidateEditEventArgs) Handles grdSOItems.ValidateEdit
        If grdSOItems.Cols(e.Col).Name.ToUpper = "QUANTITY" Then
            If grdSOItems.Editor.Text.Length > 9 Then
                'CM059() " Qty cannot be greater then 999999999
                If Val(grdSOItems.Editor.Text) > 999999999 Then
                    MsgBox(getValueByKey("CM059"), MsgBoxStyle.Critical, "CM059" & " | " & getValueByKey("CLAE05"))
                    e.Cancel = True
                End If
            End If
        End If

    End Sub

    Private Function GetTaxableAmountForCst(ByVal strMatcode As String, ByVal EAN As String, ByVal Quantity As Double, ByVal TaxableAmount As Double) As Double
        Dim dtTaxCalc As DataTable
        dtTaxCalc = New DataTable
        dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", Quantity, EAN, clsDefaultConfiguration.CSTTaxCode, False)
        If dtTaxCalc.Rows.Count > 0 Then
            dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount
            objCM.getCalculatedDataSet(dtTaxCalc)
            Return dtTaxCalc.Compute("SUM(TAXAMOUNT)", "")
        Else
            Return 0
        End If

    End Function

    Protected Sub grdScanItem_StartEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
        _iArticleQtyBeforeChange = grdSOItems.Rows(e.Row)("Quantity")
    End Sub

    Private Sub CtrReturnBarcode_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles CtrReturnBarcode.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim articleCode As String = objItemSch.GetArticleCodeFromBarcode(clsAdmin.SiteCode, CtrReturnBarcode.Text.Trim)
                If articleCode = String.Empty OrElse articleCode = "" Then
                    MessageBox.Show(getValueByKey("BatchBarcode005"))
                Else
                    Dim clsso As New clsSalesOrder()
                    Dim dtbarcodes = clsso.GetOutboundData(clsAdmin.SiteCode, CtrlSalesInfo.CtrlTxtOrderNo.Text.Trim)
                    If dtbarcodes IsNot Nothing AndAlso dtbarcodes.Rows.Count > 0 Then
                        If dtbarcodes.AsEnumerable().Where(Function(w) w("BarCode").ToString() = CtrReturnBarcode.Text.Trim).Count() > 0 Then
                            If ReturnBatchbarcode.Any(Function(w) w.BatchBarcode = CtrReturnBarcode.Text.Trim) Then
                                If dtbarcodes.AsEnumerable().Where(Function(w) w("BarCode").ToString() = CtrReturnBarcode.Text.Trim).FirstOrDefault()("DeliveredQty") < (ReturnBatchbarcode.Where(Function(w) w.BatchBarcode = CtrReturnBarcode.Text.Trim).FirstOrDefault().Qty - 1) * -1 Then
                                    MessageBox.Show(getValueByKey("batchbarcode.Validation003"))
                                    Exit Sub
                                End If
                            Else
                                If dtbarcodes.AsEnumerable().Where(Function(w) w("BarCode").ToString() = CtrReturnBarcode.Text.Trim).FirstOrDefault()("DeliveredQty") < 1 Then
                                    MessageBox.Show(getValueByKey("BatchBarcode003"))
                                    Exit Sub
                                End If
                            End If

                        Else
                            MessageBox.Show(getValueByKey("BatchBarcode004"))
                            Exit Sub
                        End If

                    End If

                    Dim dt = objItemSch.GetEANData(clsAdmin.SiteCode, articleCode, clsAdmin.LangCode, "", dtItemScanData, False, True, CtrReturnBarcode.Text.Trim)
                    If dt IsNot Nothing Then
                        Dim selectedrow = dsScanReturn.Tables(0).AsEnumerable().Where(Function(w) w("EAN") = dt(0)("EAN")).FirstOrDefault()
                        If selectedrow IsNot Nothing Then
                            Dim PrevQty = selectedrow("PickUpQty")
                            selectedrow("PickUpQty") = Val(selectedrow("PickUpQty")) + 1
                            If Not selectedrow("FreezeSR") Is DBNull.Value AndAlso selectedrow("FreezeSR") = True Then
                                ShowMessage(getValueByKey("SO079"), "SO079 - " & getValueByKey("CLAE04"))
                                selectedrow("PickUpQty") = PrevQty
                                Exit Sub
                            ElseIf selectedrow("PickUpQty") > selectedrow("DeliveredQty") Then
                                ShowMessage(getValueByKey("SO072"), "SO072 - " & getValueByKey("CLAE05"))
                                selectedrow("PickUpQty") = PrevQty
                                Exit Sub
                            Else
                                If ReturnBatchbarcode.Any(Function(w) w.BatchBarcode = CtrReturnBarcode.Text.Trim) Then
                                    ReturnBatchbarcode.Find(Function(w) w.BatchBarcode = CtrReturnBarcode.Text.Trim).Qty -= 1
                                Else
                                    ReturnBatchbarcode.Add(New SpectrumCommon.BtachbarcodeInfo() With {.ArticleCode = selectedrow("ArticleCode"), .BatchBarcode = CtrReturnBarcode.Text.Trim, .EAN = selectedrow("EAN"), .LineNO = selectedrow("BillLineNO"), .Qty = -1, .Status = True, .ArticleName = selectedrow("DISCRIPTION")})
                                End If
                            End If
                        Else
                            MessageBox.Show(getValueByKey("BatchBarcode004"))
                        End If
                    Else
                        MessageBox.Show(getValueByKey("BatchBarcode005"))
                    End If


                End If

            End If
        Catch ex As Exception

        Finally
            CtrReturnBarcode.Text = ""
        End Try
    End Sub

    Public Function AddButtonControlInGrid(ByVal rowIndex As Integer, ByVal Grid As String) As Boolean
        Try
            If Grid = "Sales" Then
                Dim getColumnType As String = String.Empty

                'Create styles with data types, formats, etc
                Dim cellStyle As C1.Win.C1FlexGrid.CellStyle

                cellStyle = grdSOItems.Styles.Add("CellImageType")
                cellStyle.DataType = Type.GetType("System.String")
                cellStyle.TextAlign = TextAlignEnum.LeftCenter
                cellStyle.WordWrap = True

                'Assign styles to editable cells
                Dim assignCellStyles As CellRange
                grdSOItems.Rows(rowIndex).HeightDisplay = 30

                Dim ButtonX As Integer = grdSOItems.Cols("EditBarcode").WidthDisplay

                'Create some new controls
                Dim btnBrowse As New CtrlBtn()
                btnBrowse.Tag = grdSOItems.Rows(rowIndex)("EAN").ToString()
                btnBrowse.MaximumSize = New System.Drawing.Size(ButtonX, 30)
                'btnBrowse.SetRowIndex = rowIndex
                btnBrowse.Text = "Barcode"
                btnBrowse.Name = "btnBarcode" + grdSOItems.Rows(rowIndex)("EAN").ToString()
                'Insert hosted control into grid
                btnBrowse.TabStop = False
                grdSOItems.Controls.Add(btnBrowse)

                'host them in the C1FlexGrid
                controlList.Add(New HostedControl(grdSOItems, btnBrowse, rowIndex, grdSOItems.Cols("EditBarcode").Index, ButtonX, ButtonX))

                'ImagePathRowIndex = rowIndex
                assignCellStyles = grdSOItems.GetCellRange(rowIndex, 3)
                assignCellStyles.Style = grdSOItems.Styles("CellImageType")

                AddHandler btnBrowse.Click, AddressOf BrowseImagePath
            Else

                Dim getColumnType As String = String.Empty

                'Create styles with data types, formats, etc
                Dim cellStyle As C1.Win.C1FlexGrid.CellStyle

                cellStyle = grdSOItemRetuns.Styles.Add("CellImageType")
                cellStyle.DataType = Type.GetType("System.String")
                cellStyle.TextAlign = TextAlignEnum.LeftCenter
                cellStyle.WordWrap = True

                'Assign styles to editable cells
                Dim assignCellStyles As CellRange
                grdSOItemRetuns.Rows(rowIndex).HeightDisplay = 30

                Dim ButtonX As Integer = grdSOItemRetuns.Cols("EditBarcode").WidthDisplay

                'Create some new controls
                Dim btnBrowse As New CtrlBtn()
                btnBrowse.Tag = grdSOItemRetuns.Rows(rowIndex)("EAN").ToString()
                btnBrowse.MaximumSize = New System.Drawing.Size(ButtonX, 30)
                'btnBrowse.SetRowIndex = rowIndex
                btnBrowse.Text = "Barcode"

                'Insert hosted control into grid
                grdSOItemRetuns.Controls.Add(btnBrowse)

                'host them in the C1FlexGrid
                controlList.Add(New HostedControl(grdSOItemRetuns, btnBrowse, rowIndex, grdSOItemRetuns.Cols("EditBarcode").Index, ButtonX, ButtonX))

                'ImagePathRowIndex = rowIndex
                assignCellStyles = grdSOItemRetuns.GetCellRange(rowIndex, 3)
                assignCellStyles.Style = grdSOItemRetuns.Styles("CellImageType")

                AddHandler btnBrowse.Click, AddressOf ReturnEditBatchBarcode
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub grdSOItems_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles grdSOItems.Paint
        For Each hosted As HostedControl In controlList
            hosted.UpdatePosition()
        Next
    End Sub

    Private Sub BrowseImagePath(ByVal sender As Object, ByVal e As EventArgs)
        Try

            Dim blist = SpectrumCommon.ExtensionModule.DeepClone(Batchbarcode.Where(Function(w) w.EAN = DirectCast(sender, Button).Tag).ToList())
            If blist.Count > 0 Then
                Dim batchbarcodes As New BatchBarcodeList(blist, SpectrumCommon.TransactionType.SalesOrderSales)
                Dim result = batchbarcodes.ShowDialog()
                If result = Windows.Forms.DialogResult.OK Then
                    Batchbarcode.RemoveAll(Function(w) w.EAN = DirectCast(sender, Button).Tag)
                    Batchbarcode.AddRange(blist.Where(Function(w) w.Status = True))
                    RecalculatePickUp(blist)
                End If
            Else
                MessageBox.Show(getValueByKey("batchbarcode.Validation002"))
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReturnEditBatchBarcode(ByVal sender As Object, ByVal e As EventArgs)
        Dim blist = SpectrumCommon.ExtensionModule.DeepClone(ReturnBatchbarcode.Where(Function(w) w.EAN = DirectCast(sender, Button).Tag).ToList())
        If blist.Count > 0 Then
            For Each batch As SpectrumCommon.BtachbarcodeInfo In blist
                batch.Qty = batch.Qty * -1
            Next

            Dim batchbarcodes As New BatchBarcodeList(blist, SpectrumCommon.TransactionType.SalesOrderReturn) With {.OrderNo = CtrlSalesInfo.CtrlTxtOrderNo.Text.Trim}
            Dim result = batchbarcodes.ShowDialog()
            If result = Windows.Forms.DialogResult.OK Then
                Dim selectedrow = dsScanReturn.Tables(0).AsEnumerable().Where(Function(w) w("EAN") = DirectCast(sender, Button).Tag).FirstOrDefault()
                selectedrow("PickUpQty") = blist.Where(Function(w) w.Status = True).Sum(Function(w) w.Qty)
                For Each batch As SpectrumCommon.BtachbarcodeInfo In blist
                    batch.Qty = batch.Qty * -1
                Next
                ReturnBatchbarcode.RemoveAll(Function(w) w.EAN = DirectCast(sender, Button).Tag)
                ReturnBatchbarcode.AddRange(blist.Where(Function(w) w.Status = True))
            End If
        Else
            MessageBox.Show(getValueByKey("batchbarcode.Validation002"))
        End If


    End Sub

    Private Sub RecalculatePickUp(ByVal blist As List(Of SpectrumCommon.BtachbarcodeInfo))

        For Each barcode As SpectrumCommon.BtachbarcodeInfo In blist
            For Each dr As DataRow In dsScan.Tables("ItemScanDetails").Select("EAN='" & barcode.EAN & "'")
                dr("PickUpQty") = Batchbarcode.Where(Function(w) w.EAN = barcode.EAN).Sum(Function(w) w.Qty)
                If (dr("PickUpQty") > 0) AndAlso ((dr("DeliveredQty") + dr("PickUpQty")) > dr("Quantity")) Then
                    dr("Quantity") = dr("Quantity") + ((dr("DeliveredQty") + dr("PickUpQty")) - dr("Quantity"))
                End If
            Next
        Next
        ReCalculateSalesOrder()
        CalculateSalesOrderSummory(Nothing)
    End Sub
    Private Sub grdSOItemRetuns_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles grdSOItemRetuns.Paint
        For Each hosted As HostedControl In controlList
            hosted.UpdatePosition()
        Next
    End Sub

    Private Sub rbnAddCombo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal flg As Boolean = False) 'Handles BtnSOSave.Click
        Try
            '.  It will be enabled only when user selects customer.
            'If CtrlCustDtls1.lblCustNameValue.Text = String.Empty Then
            '    ShowMessage(getValueByKey("SO048"), "SO048 - " & getValueByKey("CLAE04"))
            '    CtrlCustSearch1.CtrlTxtCustNo.Select()
            '    Exit Sub
            'End If
            Dim combosrNo As Integer = vRowIndex
            Dim objBulkOrderCombo As New frmBulkOrderCombo
            objBulkOrderCombo.DtSoBulkComboHdr = DtSoBulkComboHdr
            objBulkOrderCombo.DtSoBulkComboDtl = DtSoBulkComboDtl

            objBulkOrderCombo.BulkComboMstId = combosrNo
            objBulkOrderCombo.ComboSrNo = combosrNo
            objBulkOrderCombo.btnPrint.Enabled = False
            If objBulkOrderCombo.ShowDialog() = Windows.Forms.DialogResult.OK Then
                DtSoBulkComboHdr = objBulkOrderCombo.DtSoBulkComboHdr
                DtSoBulkComboDtl = objBulkOrderCombo.DtSoBulkComboDtl

                If DtSoBulkComboHdr.Rows.Count > 0 Then
                    CtrlSalesPerson.CtrlTxtBox.Text = DtSoBulkComboHdr.Rows(DtSoBulkComboHdr.Rows.Count - 1)("PackagingBoxCode")
                    IsNewComboAdd = True
                    txtSearchItem_Leave(CtrlSalesPerson.CtrlTxtBox, New KeyEventArgs(Keys.Enter))
                    IsNewComboAdd = False
                End If
                If vRowIndex = combosrNo Then
                    '---Record was not added to scanGrid need to delete from combo table as well 
                    DeleteBulkCombo(combosrNo)
                End If

            End If
        Catch ex As Exception
            LogException(ex)
            IsNewComboAdd = False
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub grdSOItems_DoubleClick(sender As Object, e As EventArgs) Handles grdSOItems.DoubleClick
        Try
            'If grdSOItems.Col <> 13 Then
            If grdSOItems.Cols(grdSOItems.Col).Name <> "ExpDelDate" Then
                If DtSoBulkComboHdr.Rows.Count > 0 Then
                    Dim ComboSrNo = grdSOItems.Item(grdSOItems.Row, "RowIndex")
                    Dim BulkComboMstId As Int64 = 0

                    Dim dr() = DtSoBulkComboHdr.Select("ComboSrNo=" & ComboSrNo)
                    If dr.Count > 0 Then
                        BulkComboMstId = dr(0)("BulkComboMstId")
                        Dim objBulkOrderCombo As New frmBulkOrderCombo
                        objBulkOrderCombo.DtSoBulkComboHdr = DtSoBulkComboHdr.Copy()
                        objBulkOrderCombo.DtSoBulkComboDtl = DtSoBulkComboDtl.Copy()
                        objBulkOrderCombo.BulkComboMstId = BulkComboMstId
                        objBulkOrderCombo.ComboSrNo = ComboSrNo
                        objBulkOrderCombo.CboPakagingBox.Enabled = False
                        If (grdSOItems.Item(grdSOItems.Row, "DeliveredQty") > 0) Then
                            objBulkOrderCombo.btnAddBulkCombo.Enabled = False
                        End If
                        If objBulkOrderCombo.ShowDialog() = Windows.Forms.DialogResult.OK Then
                            DtSoBulkComboHdr = objBulkOrderCombo.DtSoBulkComboHdr
                            DtSoBulkComboDtl = objBulkOrderCombo.DtSoBulkComboDtl
                            IsStrGenerateApplicable = objBulkOrderCombo.IsStrGenerateApplicable
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            LogException(ex)
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function DeleteBulkCombo(combosrNo As Integer) As Boolean
        DeleteBulkCombo = False
        Try
            Dim BulkComboMstId As Int64 = 0
            If DtSoBulkComboHdr.Rows.Count > 0 Then
                Dim drHdr() = DtSoBulkComboHdr.Select("ComboSrNo=" & combosrNo)
                If drHdr.Count > 0 Then
                    BulkComboMstId = drHdr(0)("BulkComboMstId")
                    For Each row As DataRow In drHdr
                        DtSoBulkComboHdr.Rows.Remove(row)
                    Next
                End If
                Dim drDtl() = DtSoBulkComboDtl.Select("BulkComboMstId=" & BulkComboMstId)
                If drDtl.Count > 0 Then
                    For Each row As DataRow In drDtl
                        DtSoBulkComboDtl.Rows.Remove(row)
                    Next
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub cmdGenerateSTR_Click(sender As Object, e As EventArgs) Handles cmdGenerateSTR.Click
        Try
            If dsScan.Tables(0).Rows.Count = 0 Then
                ShowMessage(getValueByKey("SO027"), "SO027 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            '            '---   SO Edit -       when user click Generate STR 
            '-        Case 1 -Generate STR is not applicable if user put some pick up quantity.
            '-	 case 2- user edit SO then asked for save & Generate STR  YES/No and in case of Yes SO saved and it generate STR .
            '-	 case 3– user has not edit anything then directly generate STR without saving data .

            Dim dr() = dsScan.Tables("ItemScanDetails").Select("PickUpQty > 0")
            If dr.Length > 0 Then
                MessageBox.Show("You cannot generate STR as few items in this sales order have been received by customer.", getValueByKey("CLAE04"))
            Else
                If IsStrGenerateApplicable Then
                    If MessageBox.Show("This sales order is not saved. Click OK to save sales order and generate the STR.", getValueByKey("CLAE04"), MessageBoxButtons.OKCancel) = Windows.Forms.DialogResult.OK Then
                        IsSTRGenerate = True
                        BtnAcceptPayment_Click(Nothing, Nothing)
                        If IsSOSaved Then
                            ShowMessage("STR Generated Successfully", getValueByKey("CLAE04"))
                            IsSOSaved = False
                            IsStrGenerateApplicable = False
                        End If

                    End If
                Else
                    Dim objsales As New clsSalesOrder
                    objsales.GenerateStrData(clsAdmin.SiteCode, vSalesNo)
                    ShowMessage("STR Generated Successfully", getValueByKey("CLAE04"))
                End If
            End If
            IsSTRGenerate = False
        Catch ex As Exception
            LogException(ex)
            MsgBox(ex.Message)

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
                    drItemExists = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "' And BatchBarcode = '" & drItemsRow.Item("BatchBarcode") & "'")
                Else
                    drItemExists = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "' And BatchBarcode IS NULL")
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

                            fnAnalyzeItem = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "' And BatchBarcode = '" & drItemsRow.Item("BatchBarcode") & "'" & IIf(packazingBoxRowNos = String.Empty, "", "AND rowIndex Not In(" & packazingBoxRowNos & ")"))
                        Else
                            fnAnalyzeItem = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "' And BatchBarcode IS NULL " & IIf(packazingBoxRowNos = String.Empty, "", "AND rowIndex Not In(" & packazingBoxRowNos & ")"))
                        End If
                    ElseIf comboCount = 1 Then
                        '---- Get The row for Qty ++
                        If IsDBNull(drItemsRow.Item("BatchBarcode")) = False Then
                            fnAnalyzeItem = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "' And BatchBarcode = '" & drItemsRow.Item("BatchBarcode") & "'")
                        Else
                            fnAnalyzeItem = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "' And BatchBarcode IS NULL")
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
    End Sub
    Private Function Themechange()
        CtrlRbn1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black
        CtrlCashSummary1.AlignChangeForCashSummary = "Sales Order Old"
        CtrlSalesPerson.AlignChange = "Sales Order Old"
        CtrlRbn1.DbtnPay.LargeImage = Global.Spectrum.My.Resources.Resources.payment_Normal
        CtrlRbn1.DbtnPayCash.LargeImage = Global.Spectrum.My.Resources.Resources.Cash_Normal
        CtrlRbn1.DbtnPayCard.LargeImage = Global.Spectrum.My.Resources.Resources.Card_Normal
        CtrlRbn1.DbtnpayCheque.LargeImage = Global.Spectrum.My.Resources.PayByCheque


        Me.rbgrpSO.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        Me.rbgrpSO.ForeColorInner = Color.FromArgb(37, 37, 37)
       

        Me.rbngAddCombo.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        'Me.rbnGrpAddCombo.Image = Global.Spectrum.My.Resources.defaultPromo_Normal
        rbnAddCombo.LargeImage = Global.Spectrum.My.Resources.AddCombonew
        Me.rbngAddCombo.ForeColorInner = Color.FromArgb(37, 37, 37)
        Me.rbnGrpCMPromotion.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        Me.rbnGrpCMPromotion.Image = Global.Spectrum.My.Resources.defaultPromo_Normal
        Me.rbnGrpCMPromotion.ForeColorInner = Color.FromArgb(37, 37, 37)
        CtrlRbn1.DgrpPayments.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        Me.rbgrpSaveNprint.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        '  Me.rbnGrpPayments.Image = Global.Spectrum.My.Resources.payment_Normal
        Me.rbgrpSaveNprint.ForeColorInner = Color.FromArgb(37, 37, 37)
       
        cmdSONew.LargeImage = Global.Spectrum.My.Resources.NewSO1


        CmdSOEdit.LargeImage = Global.Spectrum.My.Resources.EditSO1

        CmdSOClose.LargeImage = Global.Spectrum.My.Resources.CancelSO1
        cmdSave.LargeImage = Global.Spectrum.My.Resources.SaveSO1
        cmdPrint.LargeImage = Global.Spectrum.My.Resources.PrintSO1

        cmdDefaultPromo.Text = cmdDefaultPromo.Text.ToUpper
        cmdApplySelectedPromo.Text = cmdApplySelectedPromo.Text.ToUpper
        cmdClearSelectedPromo.Text = cmdClearSelectedPromo.Text.ToUpper
        CtrlRbn1.DbtnPay.Text = CtrlRbn1.DbtnPay.Text.ToUpper
        CtrlRbn1.DbtnPayCash.Text = CtrlRbn1.DbtnPayCash.Text.ToUpper
        CtrlRbn1.DbtnPayCard.Text = CtrlRbn1.DbtnPayCard.Text.ToUpper
        CtrlRbn1.DbtnpayCheque.Text = CtrlRbn1.DbtnpayCheque.Text.ToUpper
        'cst.Text = rbnCST.Text.ToUpper
        ' rbBtnRoundOff.Text = rbBtnRoundOff.Text.ToUpper

        Me.rbgrpSO.Text = Me.rbgrpSO.Text.ToUpper

        rbnTabSO.Text = rbnTabSO.Text.ToUpper
        rbnTabSO.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)

        '  Me.rbnGrpCST.Text = Me.rbnGrpCST.Text.ToUpper

        rbnAddCombo.Text = rbnAddCombo.Text.ToUpper
        Me.rbngAddCombo.Text = Me.rbngAddCombo.Text.ToUpper
        Me.rbnGrpCMPromotion.Text = Me.rbnGrpCMPromotion.Text.ToUpper
        CtrlRbn1.DgrpPayments.Text = CtrlRbn1.DgrpPayments.Text.ToUpper

        Me.rbgrpSaveNprint.Text = Me.rbgrpSaveNprint.Text.ToUpper

        cmdSONew.Text = cmdSONew.Text.ToUpper
        CmdSOEdit.Text = CmdSOEdit.Text.ToUpper
        CmdSOClose.Text = CmdSOClose.Text.ToUpper
        cmdSave.Text = cmdSave.Text.ToUpper
        cmdPrint.Text = cmdPrint.Text.ToUpper

        rbgrpSO.ForeColorInner = Color.FromArgb(54, 54, 54)
        CtrlRbn1.DgrpPayments.ForeColorInner = Color.FromArgb(54, 54, 54)
        '  RibbonGroup2.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbgrpSaveNprint.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbnGrpCMPromotion.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbngAddCombo.ForeColorInner = Color.FromArgb(54, 54, 54)


        rbgrpSO.ForeColorOuter = Color.FromArgb(0, 107, 163)
        CtrlRbn1.DgrpPayments.ForeColorOuter = Color.FromArgb(0, 107, 163)
        ' RibbonGroup2.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbgrpSaveNprint.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbnGrpCMPromotion.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbngAddCombo.ForeColorOuter = Color.FromArgb(0, 107, 163)
        '  DgrpPayments.ForeColorOuter = Color.FromArgb(0, 107, 163)


        grdSOItems.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        grdSOItems.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212)
        grdSOItems.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        grdSOItems.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        grdSOItems.Rows.MinSize = 30
        grdSOItems.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        grdSOItems.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSOItems.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSOItems.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSOItems.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSOItems.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdSOItems.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        grdSOItems.Styles.Highlight.ForeColor = Color.Black
        grdSOItems.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSOItems.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSOItems.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdSOItems.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdSOItems.CellButtonImage = Global.Spectrum.My.Resources.Delete
        grdSOInvoice.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        grdSOInvoice.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212)
        grdSOInvoice.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        grdSOInvoice.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        grdSOInvoice.Rows.MinSize = 30
        grdSOInvoice.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        grdSOInvoice.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSOInvoice.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSOInvoice.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSOInvoice.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSOInvoice.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdSOInvoice.Styles.Highlight.ForeColor = Color.Black
        grdSOInvoice.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        grdSOInvoice.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSOInvoice.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSOInvoice.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdSOInvoice.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdSOInvoice.CellButtonImage = Global.Spectrum.My.Resources.Delete

        grdSOItemRetuns.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        grdSOItemRetuns.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212)
        grdSOItemRetuns.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        grdSOItemRetuns.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        grdSOItemRetuns.Rows.MinSize = 30
        grdSOItemRetuns.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        grdSOItemRetuns.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSOItemRetuns.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSOItemRetuns.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSOItemRetuns.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSOItemRetuns.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdSOItemRetuns.Styles.Highlight.ForeColor = Color.Black
        grdSOItemRetuns.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        grdSOItemRetuns.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSOItemRetuns.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSOItemRetuns.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdSOItemRetuns.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdSOItemRetuns.CellButtonImage = Global.Spectrum.My.Resources.Delete


        cmdGenerateSTR.Image = My.Resources.GenerateSTRnew
        cmdGenerateSTR.ImageAlign = ContentAlignment.MiddleCenter
        cmdGenerateSTR.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmdGenerateSTR.TextAlign = ContentAlignment.MiddleCenter
        cmdGenerateSTR.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdGenerateSTR.BackColor = Color.Transparent
        cmdGenerateSTR.BackColor = Color.White
        cmdGenerateSTR.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdGenerateSTR.FlatStyle = FlatStyle.Flat

        CtrlBtnReturn.Image = My.Resources.ReturnsNew
        CtrlBtnReturn.ImageAlign = ContentAlignment.MiddleCenter
        CtrlBtnReturn.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlBtnReturn.TextAlign = ContentAlignment.MiddleCenter
        CtrlBtnReturn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnReturn.BackColor = Color.Transparent
        CtrlBtnReturn.BackColor = Color.White
        CtrlBtnReturn.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CtrlBtnReturn.FlatStyle = FlatStyle.Flat

        CtrlBtnStockCheck.VisualStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnStockCheck.Image = My.Resources.StockCheck
        CtrlBtnStockCheck.ImageAlign = ContentAlignment.MiddleCenter
        CtrlBtnStockCheck.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlBtnStockCheck.TextAlign = ContentAlignment.MiddleCenter
        CtrlBtnStockCheck.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnStockCheck.BackColor = Color.Transparent
        CtrlBtnStockCheck.BackColor = Color.White
        CtrlBtnStockCheck.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CtrlBtnStockCheck.FlatStyle = FlatStyle.Flat

        CtrlBtnAddExtraCost.VisualStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnAddExtraCost.Image = My.Resources.AdditionalCost
        CtrlBtnAddExtraCost.ImageAlign = ContentAlignment.MiddleCenter
        CtrlBtnAddExtraCost.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlBtnAddExtraCost.TextAlign = ContentAlignment.MiddleCenter
        CtrlBtnAddExtraCost.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnAddExtraCost.BackColor = Color.Transparent
        CtrlBtnAddExtraCost.BackColor = Color.White
        CtrlBtnAddExtraCost.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CtrlBtnAddExtraCost.FlatStyle = FlatStyle.Flat

        CtrlProductImage.Size = New Size(224, 128)
        CtrlProductImage.Location = New Point(1131, 281)
        CtrlCashSummary1.Location = New Point(1131, 410)
        CtrlCashSummary1.Size = New Size(224, 190)
        CtrlCashSummary1.MinimumSize = New Size(222, 0)
        TabSalesOrder.Size = New Size(1127, 354)
        C1Sizer2.Size = New Size(1130, 72)



        TabPageInvoiceDetails.TabBackColorSelected = Color.FromArgb(212, 212, 212)
        TabPageItemDetails.TabBackColorSelected = Color.FromArgb(212, 212, 212)
        'TabPageInvoiceDetails.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        'TabPageItemDetails.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        TabPageInvoiceDetails.Text = TabPageInvoiceDetails.Text.ToUpper
        TabPageItemDetails.Text = TabPageItemDetails.Text.ToUpper

        Me.C1Sizer3.Controls.Remove(Me.CtrlSalesPerson)
        Me.Controls.Add(CtrlSalesPerson)
        Me.Controls.SetChildIndex(Me.CtrlSalesPerson, 0)
        CtrlSalesPerson.Size = New Size(685, 22)
        CtrlSalesPerson.Location = New Point(430, 282)
        C1Sizer3.Hide()
        Me.TabPageItemDetails.Controls.Add(Me.grdSOItems)
        Me.grdSOItems.Size = New System.Drawing.Size(1118, 327)
        Me.grdSOItems.Location = New System.Drawing.Point(3, 3)

        'For i = 0 To grdSOItemRetuns.Cols.Count - 1
        '    grdSOItemRetuns.Cols(i).Caption = grdSOItemRetuns.Cols(i).Caption.ToUpper
        'Next
        'For i = 0 To grdSOInvoice.Cols.Count - 1
        '    grdSOInvoice.Cols(i).Caption = grdSOInvoice.Cols(i).Caption.ToUpper
        'Next
        'For i = 0 To grdSOItems.Cols.Count - 1
        '    grdSOItems.Cols(i).Caption = grdSOItems.Cols(i).Caption.ToUpper
        'Next

    End Function
End Class

