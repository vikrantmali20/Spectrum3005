Imports System.IO
Imports SpectrumBL
Imports System.Resources
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient

Public Class frmNQuotationUpdate
    Dim objQuotation As New clsQuotation
    Dim objCustm As New clsCLPCustomer()
    Dim clsVoucher As New SpectrumPrint.clsPrintVoucher

    Dim dtSalesOrderTaxDetails As DataTable
#Region "Declare Varables"
    Private IsCSTApplicable As Boolean = False
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
    Dim _iArticleQtyBeforeChange As Double = 0
    Dim vIsPrintingTaxInfoAllowed As Boolean = False
    Dim vIsPrintPreviewAllowed As Boolean = False
    Dim vIsPromotionalMessageAllowed As Boolean = False
    Dim vIsPrintOfficialAddressAllowed As Boolean = False
    Dim vIsReturnWithoutOldSOAllowed As Boolean = False
    Dim vIsNegativeInventoryAllowed As Boolean = False
    Dim IsIGSTApplicableForOutsideCustomer As Boolean = False

    Dim tabSales, tabPayment, tabReturn As TabPage
    Dim isPromotionApplied As Boolean = False
    Dim objClpCustm As New clsCLPCustomer
    Dim objComn As New clsCommon
    Dim objCM As New clsCashMemo
    Dim objItemSch As New clsIteamSearch

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
    Dim _strCustNo As String

    Dim vSalesNo As String = ""
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
    Dim QtyBeforEdit As Double

    Dim IsAllowedSalesReturn As Boolean = False
    Dim IsQuantityChange As Boolean = False
    Dim IsEditItem As Boolean = False
    Dim IsNewArticle As Boolean = False
    Dim IsMRPOpen As Boolean = False
    Dim IsNewRow As Boolean = False
    Dim IsApplyPromotion As Boolean = False
    Dim IsNextInvoiceNo As Boolean = False
    Dim TempOtherChargesTable As New DataTable
    Dim IsConvertToSalesOrder As Boolean = False

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
    Dim IsFormClosing As Boolean = False

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

    Private _dDueDate As Date
    Private _strRemarks As String
    Private boolIsReturn As Boolean = False

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

#End Region
#Region "Load Sales Order Application"

    Private Sub frmNQuotationUpdate_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If grdSOItems.Rows.Count > 1 AndAlso Not IsFormClosing Then
            If MsgBox(getValueByKey("SO047"), MsgBoxStyle.YesNo, "SO047 - " & getValueByKey("CLAE04")) = MsgBoxResult.No Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmNQuotationUpdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "532-edit-quotation.htm")
            End If
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

            AddHandler CtrlSalesPerson.CtrlCmdSearch.Click, AddressOf BtnSearchItem_Click
            AddHandler CtrlSalesPerson.CtrlTxtBox.KeyDown, AddressOf txtSearchItem_Leave
            AddHandler CtrlBtnAddExtraCost.Click, AddressOf BtnAddOtherCharges_Click
            AddHandler cmdApplySelectedPromo.Click, AddressOf cmdDefaultPromo_Click
            AddHandler cmdSave.Click, AddressOf BtnSaveSalesOrder_Click
            AddHandler CtrlSalesInfo.CtrlBtn1.Click, AddressOf BtnSearchSalesOrder_Click
            AddHandler CmdSOClose.Click, AddressOf BtnSOCancel_Click
            ' AddHandler CmdSOClose.Click, AddressOf BtnSOCloseManualSO_Click
            AddHandler cmdPrint.Click, AddressOf BtnSOPrint_Click
            AddHandler CtrlBtnReturn.Click, AddressOf BtnSOReturn_Click
            AddHandler CtrlBtnStockCheck.Click, AddressOf BtnSOStockCheck_Click
            AddHandler CtrlRbn1.DbtnPay.Click, AddressOf Btnpay_Click
            AddHandler CtrlRbn1.DbtnPayCard.Click, AddressOf BtnPayCard_Click
            AddHandler CtrlRbn1.DbtnPayCash.Click, AddressOf BtnPayCash_Click
            AddHandler CtrlRbn1.DbtnpayCheque.Click, AddressOf BtnPayCheque_Click
            AddHandler grdSOItems.StartEdit, AddressOf grdScanItem_StartEdit
            AddHandler CtrlSalesInfo.CtrlTxtOrderNo.KeyDown, AddressOf FillSalesOrder
            'AddHandler CtrlSalesPerson.CtrlTxtBox.KeyDown, AddressOf txtSearchItem_KeyDown
            AddHandler CtrlRbn1.DbtnF2.Click, AddressOf ChangeQty
            CtrlBtnReturn.Visible = False
            Dim objdefault As New clsDefaultConfiguration("SalesOrder")
            objdefault.GetDefaultSettings()
            'objdefault = New clsDefaultConfiguration("Quotation")
            'objdefault.GetDefaultSettings()
            dsMain = objQuotation.GetSOTableStruct(vSiteCode, 0)
            objSO.GetSODefaultConfig(vSiteCode)
            dtPrintingDetails = objSO.GetPrintingDetail
            _drSiteInfo = objComn.GetSiteInfo(vSiteCode).Rows(0)

            vDocTypeCreation = objSO.SOCreation
            vDocTypeReturn = objSO.SOReturn
            vDocType = vDocTypeCreation
            vIsPrintPreviewAllowed = clsDefaultConfiguration.IsPreviewReqForQuotation

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
            rbgrpSO.Text = getValueByKey("frmnquotationcreation.rbgrpso")
            'cmdSONew.Text = getValueByKey("frmnquotationcreation.rbbtnsonew")
            'CmdSOEdit.Text = getValueByKey("frmnquotationcreation.rbbtnsoedit")
            'CmdSOClose.Text = getValueByKey("frmnquotationcreation.rbbtnsocancel")
            cmdSONew.LargeImage = Global.Spectrum.My.Resources.Resources.Create_Quotation
            CmdSOEdit.LargeImage = Global.Spectrum.My.Resources.Resources.Edit_Quotation
            CmdSOClose.LargeImage = Global.Spectrum.My.Resources.Resources.Cancel_Quotation

            rbgrpSaveNprint.Visible = False
            rbnTabSO.Text = "Quotation"
            TabPageInvoiceDetails.TabVisible = False

            'Rakesh-10.10.2013-7583->Hide unwanted button from quotation screen
            CtrlBtnReturn.Visible = False

            CtrlCashSummary1.CtrlLabel5.Visible = False
            CtrlCashSummary1.CtrlLabeltxt5.Visible = False
            CtrlCashSummary1.CtrlLabel6.Visible = False
            CtrlCashSummary1.CtrlLabelTxt6.Visible = False
            CtrlCashSummary1.CtrlLabel7.Visible = False
            CtrlCashSummary1.CtrlLabelTxt7.Visible = False

            Call SetTabSequence()

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
            ctrTablIndex.Add(Me.TabPageInvoiceDetails, 1)
            ctrTablIndex.Add(Me.C1Sizer3, 0)
            ctrTablIndex.Add(Me.CtrlSalesPerson, 0)
            ctrTablIndex.Add(Me.CtrlSalesPerson.CtrlSalesPersons, 0)
            ctrTablIndex.Add(Me.CtrlSalesPerson.CtrlTxtBox, 1)
            ctrTablIndex.Add(Me.CtrlSalesPerson.CtrlCmdSearch, 2)

            ctrTablIndex.Add(Me.grdSOItems, 1)

            ctrTablIndex.Add(Me.C1Sizer2, 8)
            ctrTablIndex.Add(Me.CtrlBtnAddExtraCost, 0)
            ctrTablIndex.Add(Me.CtrlBtnStockCheck, 1)

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



    Private Sub frmNQuotationUpdate_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
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
        If CtrlSalesInfo.CtrlDtExpDelDate.Value Is DBNull.Value Then
            ShowMessage(getValueByKey("SO032"), "SO032 - " & getValueByKey("CLAE04"))
            'ShowMessage("Delivery Date cannot be Blank.", "Delivery Date Information")
            CtrlSalesInfo.CtrlDtExpDelDate.Value = consDeliveryDate
        Else
            If CtrlSalesInfo.CtrlDtExpDelDate.Value < vCurrentDate Then
                ShowMessage(getValueByKey("SO010"), "SO010 - " & getValueByKey("CLAE04"))
                'ShowMessage("Delivery Date cannot be backdated.", "Delivery Date Information")
                CtrlSalesInfo.CtrlDtExpDelDate.Value = consDeliveryDate
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
            If (vSalesNo = "" Or vSalesNo = String.Empty) Then
                MessageBox.Show(getValueByKey("frmnquotationupdate.Warning"), getValueByKey("CLAE04"))
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
                    ' ean = SearchAvailableBarcodes(drSearch.Item("ArticleCode").ToString())
                    If String.IsNullOrEmpty(ean) Then
                        ' Dim EventType As Int32
                        ' ShowMessage(getValueByKey("frmnsalesorder.scaningreqmsg"), getValueByKey("CLAE04"), EventType, True, getValueByKey("mod009"))
                        'If EventType = 1 Then
                        _IsScanningWoBarcodeSelected = True
                        ean = drSearch("EAN").ToString()
                        'Else
                        '  Exit Sub
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
                If CtrlSalesPerson.CtrlTxtBox.Text.Length >= 1 Then
                    If (vSalesNo = "" Or vSalesNo = String.Empty) Then
                        MessageBox.Show(getValueByKey("frmnquotationupdate.Warning"), getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                    'dtItemSch = objItemSch.GetEANData(clsAdmin.SiteCode, CtrlSalesPerson.CtrlTxtBox.Text, clsAdmin.LangCode, "", dtItemScanData)                  
                    If clsDefaultConfiguration.IsBatchManagementReq Then
                        Dim articleCode As String = objItemSch.GetArticleCodeFromBarcode(clsAdmin.SiteCode, CtrlSalesPerson.CtrlTxtBox.Text.Trim)
                        If String.IsNullOrEmpty(articleCode) Then
                            articleCode = objItemSch.GetArticleCodeFromEAN(CtrlSalesPerson.CtrlTxtBox.Text.Trim)
                            If String.IsNullOrEmpty(articleCode) Then
                                articleCode = CtrlSalesPerson.CtrlTxtBox.Text.Trim
                            End If
                            Dim barCode As String
                            If _IsScanningWoBarcodeSelected = False Then
                                '    barCode = SearchAvailableBarcodes(articleCode)
                                'End If
                                'If String.IsNullOrEmpty(barCode) AndAlso _IsScanningWoBarcodeSelected = False Then
                                '    Dim EventType As Int32
                                '    ShowMessage(getValueByKey("frmnsalesorder.scaningreqmsg"), getValueByKey("CLAE04"), EventType, True, getValueByKey("mod009"))
                                '    If EventType = 1 Then
                                '        dtItemSch = objItemSch.GetEANData(clsAdmin.SiteCode, articleCode, clsAdmin.LangCode, "", dtItemScanData)
                                '        For Each item In dtItemSch.Rows
                                '            item("BatchBarcode") = DBNull.Value
                                '        Next
                                '    Else
                                '        Exit Sub
                                '    End If
                                'Else
                                '    dtItemSch = objItemSch.GetEANData(clsAdmin.SiteCode, articleCode, clsAdmin.LangCode, "", dtItemScanData, False, True, barCode)
                                '    For Each item In dtItemSch.Rows
                                '        item("BatchBarcode") = barCode
                                '    Next

                                dtItemSch = objItemSch.GetEANData(clsAdmin.SiteCode, articleCode, clsAdmin.LangCode, "", dtItemScanData)
                                For Each item In dtItemSch.Rows
                                    item("BatchBarcode") = DBNull.Value
                                Next
                            Else
                                dtItemSch = objItemSch.GetEANData(clsAdmin.SiteCode, articleCode, clsAdmin.LangCode, "", dtItemScanData)
                                For Each item In dtItemSch.Rows
                                    item("BatchBarcode") = DBNull.Value
                                Next
                            End If

                        Else
                            dtItemSch = objItemSch.GetEANData(clsAdmin.SiteCode, articleCode, clsAdmin.LangCode, "", dtItemScanData, False, True, CtrlSalesPerson.CtrlTxtBox.Text.Trim)
                            For Each item In dtItemSch.Rows
                                item("BatchBarcode") = DBNull.Value
                            Next
                        End If
                    Else
                        dtItemSch = objItemSch.GetEANData(clsAdmin.SiteCode, CtrlSalesPerson.CtrlTxtBox.Text.Trim, clsAdmin.LangCode, "", dtItemScanData)
                        For Each item In dtItemSch.Rows
                            item("BatchBarcode") = DBNull.Value
                        Next
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
        If Not (vEANList.IndexOf(grdSOItems.Item(grdSOItems.Row, "EAN")) = -1) Then
            IsNewArticle = False
        Else
            IsNewArticle = True
        End If

        If (Val(QtyBeforEdit) <> Val(grdSOItems.Item(grdSOItems.Row, "Quantity"))) Then IsConvertToSalesOrder = True
        If IsDBNull(grdSOItems.Item(grdSOItems.Row, "BatchBarcode")) = False Then
            dvCurrentQty = New DataView(dsScan.Tables(0), "EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode = '" & grdSOItems.Item(grdSOItems.Row, "BatchBarcode") & "'", "", DataViewRowState.CurrentRows)
        Else
            dvCurrentQty = New DataView(dsScan.Tables(0), "EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode IS NULL", "", DataViewRowState.CurrentRows)
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

                If Not (vOrderQty > 0) Then

                    ShowMessage(getValueByKey("SO005"), "SO005 - " & getValueByKey("CLAE04"))
                    If CurrentQty <= 0 Then CurrentQty = _iArticleQtyBeforeChange
                    'ShowMessage("Order Quantity cannot less than 1.", "Order Quantity Information")
                    grdSOItems.Item(grdSOItems.Row, "Quantity") = CurrentQty

                ElseIf Not (vOrderQty >= vDeliveredQty + vPickUpQty) Then

                    ShowMessage(getValueByKey("SO033"), "SO033 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Order Quantity cannot less than PickUp + Delivered Quantity.", "Order Quantity Information")
                    grdSOItems.Item(grdSOItems.Row, "Quantity") = vDeliveredQty + vPickUpQty
                End If


                If IsApplyPromotion = True Then
                    ' If MsgBox(getValueByKey("SO034"), MsgBoxStyle.YesNo, "SO034") = MsgBoxResult.Yes Then
                    'RemoveApplyPromotion(_dsScan)
                    'drAddItemExists = _dsScan.Tables("ItemScanDetails").Select("EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' and IsStatus <> 'Deleted' ")
                    If IsDBNull(grdSOItems.Item(grdSOItems.Row, "BatchBarcode")) = False Then
                        drAddItemExists = _dsScan.Tables("ItemScanDetails").Select("EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode = '" & grdSOItems.Item(grdSOItems.Row, "BatchBarcode") & "' and IsStatus <> 'Deleted' ")
                    Else
                        drAddItemExists = _dsScan.Tables("ItemScanDetails").Select("EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' and IsStatus <> 'Deleted' And BatchBarcode IS NULL")
                    End If

                    If drAddItemExists.Length > 0 Then
                        IsEditItem = True
                        drAddItemExists(0)("Quantity") = grdSOItems.Item(grdSOItems.Row, "Quantity")
                        drAddItemExists(0)("GrossAmt") = grdSOItems.Item(grdSOItems.Row, "Quantity") * grdSOItems.Item(grdSOItems.Row, "SellingPrice")
                        'SetScanItemInSO(drAddItemExists(0))
                        Dim obj As New clsSaleOrderCommon
                        obj.IsCSTApplicable = IsCSTApplicable
                        obj.RecalculateLine(drAddItemExists(0), CtrlSalesInfo.CtrlTxtOrderNo.Text, dsMain, False)
                        TotalSalesQty = drAddItemExists(0)("PickupQty") + drAddItemExists(0)("DeliveredQty")
                        Dim ArticleRate As Double = Math.Round(drAddItemExists(0)("NetAmount") / drAddItemExists(0)("Quantity"), 3)
                        drAddItemExists(0)("MinPayAmt") = ((drAddItemExists(0)("Quantity") - TotalSalesQty) * ArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * ArticleRate)
                        drAddItemExists(0)("IsStatus") = "Updated"
                        RefreshLoadSOData()
                        CalculateSalesOrderSummory(dsScan)
                        GridItemSetting()
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
                    dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode = '" & grdSOItems.Item(grdSOItems.Row, "BatchBarcode") & "'", "", DataViewRowState.CurrentRows)
                Else
                    dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode IS NULL", "", DataViewRowState.CurrentRows)
                End If

                If dvPickupQty.Count > 0 Then
                    dvPickupQty.AllowEdit = True

                    For Each drPickupQty As DataRowView In dvPickupQty
                        If CInt(grdSOItems.Item(grdSOItems.Row, "PickupQty")) + CInt(grdSOItems.Item(grdSOItems.Row, "DeliveredQty")) <= grdSOItems.Item(grdSOItems.Row, "Quantity") Then
                            drPickupQty("PickupQty") = grdSOItems.Item(grdSOItems.Row, "PickupQty")
                            drPickupQty("IsStatus") = "Updated"

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
                If Not (Format(grdSOItems.Item(grdSOItems.Row, grdSOItems.Col), vDateFormat) >= Format(vCurrentDate, vDateFormat)) Then
                    ShowMessage(getValueByKey("SO010"), "SO010 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Delivery Date cannot be backdated.", "Delivery Date")
                    grdSOItems.Item(grdSOItems.Row, "ExpDelDate") = CtrlSalesInfo.CtrlDtExpDelDate.Value
                Else
                    Dim dvPickupQty As DataView
                    If IsDBNull(grdSOItems.Item(grdSOItems.Row, "BatchBarcode")) = False Then
                        dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode = '" & grdSOItems.Item(grdSOItems.Row, "BatchBarcode") & "'", "", DataViewRowState.CurrentRows)
                    Else
                        dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdSOItems.Item(grdSOItems.Row, "EAN") & "' And BatchBarcode IS NULL", "", DataViewRowState.CurrentRows)
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

    End Sub

    Private Sub grdSOItems_BeforeEdit(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdSOItems.BeforeEdit
        Try
            If (grdSOItems.Rows.Count > 1) Then
                If (grdSOItems.Cols(grdSOItems.Col).Name = "Quantity") Then
                    QtyBeforEdit = grdSOItems.Item(grdSOItems.Row, "Quantity")
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub


    ''' <summary>
    ''' Delete Selected Article from Item Details DataGrid
    ''' </summary>
    ''' <param name="sender">Select Row</param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdScanItem_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdSOItems.CellButtonClick
        Try
            'commented by rama for bug no 0000563
            'If grdSOItems.Item(grdSOItems.Row, "ExpDelDate") >= Format(vCurrentDate, vDateFormat) Then
            If MsgBox(getValueByKey("SO011"), MsgBoxStyle.YesNo, "SO011 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                If Not (grdSOItems.Item(grdSOItems.Row, "DeliveredQty") > 0) Then
                    If IsDBNull(grdSOItems.Item(grdSOItems.Row, "BatchBarcode")) = False Then
                        DeleteScanItemInSO(grdSOItems.Item(grdSOItems.Row, "EAN"), grdSOItems.Item(grdSOItems.Row, "BatchBarcode"))
                    Else
                        DeleteScanItemInSO(grdSOItems.Item(grdSOItems.Row, "EAN"))
                    End If
                    IsConvertToSalesOrder = True
                Else
                    ShowMessage(getValueByKey("SO035"), "SO035 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Items cannot deleted.", "Delivery Date Information")
                End If
            End If

            'Else
            'ShowMessage(getValueByKey("SO010"), "SO010")
            'ShowMessage("Delivery Date of Item cannot be backdated.", "Delivery Date Information")
            'End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Sub

    ''' <summary>
    ''' Delete Selected Article from DataGrid
    ''' </summary>
    ''' <param name="sender">Select Row</param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdSOItems_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSOItems.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                If grdSOItems.Item(grdSOItems.Row, "ExpDelDate") >= Format(vCurrentDate, vDateFormat) Then
                    If MsgBox(getValueByKey("SO011"), MsgBoxStyle.YesNo, "SO011 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                        If Not (grdSOItems.Item(grdSOItems.Row, "DeliveredQty") > 0) Then
                            If IsDBNull(grdSOItems.Item(grdSOItems.Row, "BatchBarcode")) = False Then
                                DeleteScanItemInSO(grdSOItems.Item(grdSOItems.Row, "EAN"), grdSOItems.Item(grdSOItems.Row, "BatchBarcode"))
                            Else
                                DeleteScanItemInSO(grdSOItems.Item(grdSOItems.Row, "EAN"))
                            End If
                            IsConvertToSalesOrder = True
                        Else
                            ShowMessage(getValueByKey("SO035"), "SO035 - " & getValueByKey("CLAE04"))
                            'ShowMessage("Items cannot deleted.", "Delivery Date Information")
                        End If
                    End If
                Else
                    ShowMessage(getValueByKey("SO010"), "SO010 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Delivery Date of Item cannot be backdated.", "Delivery Date Information")
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
    Private Function DeleteScanItemInSO(ByVal vEAN As String, Optional batchBarcode As String = "") As Boolean
        Try

            If String.IsNullOrEmpty(batchBarcode) = False Then
                For Each drRow As DataRow In dsMain.Tables("QuotationDtl").Select("EAN='" & vEAN & "' And BatchBarcode = '" & batchBarcode & "'", "", DataViewRowState.CurrentRows)
                    drRow.Delete()
                Next
            Else
                For Each drRow As DataRow In dsMain.Tables("QuotationDtl").Select("EAN='" & vEAN & "' And BatchBarcode IS NULL", "", DataViewRowState.CurrentRows)
                    drRow.Delete()
                Next
            End If

            Dim dvEdit As DataView
            If String.IsNullOrEmpty(batchBarcode) = False Then
                dvEdit = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & vEAN & "' And BatchBarcode = '" & batchBarcode & "'", "", DataViewRowState.CurrentRows)
            Else
                dvEdit = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & vEAN & "' And BatchBarcode IS NULL", "", DataViewRowState.CurrentRows)
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

                'drAddItemExists = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "'")
                If IsDBNull(drItemsRow.Item("BatchBarcode")) = False Then
                    drAddItemExists = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "' And BatchBarcode = '" & drItemsRow.Item("BatchBarcode") & "'")
                Else
                    drAddItemExists = dsScan.Tables("ItemScanDetails").Select("EAN='" & drItemsRow.Item("EAN") & "' And BatchBarcode IS NULL")
                End If

                If IsNewArticle = True Then
                    If drAddItemExists.Length = 0 Then
                        drAddItem = _dsScan.Tables("ItemScanDetails").NewRow
                        drAddItem("Quantity") = 1
                        drAddItem("PickUpQty") = 0
                        drAddItem("DeliveredQty") = 0
                        drAddItem("IsStatus") = "Updated"
                    Else
                        drAddItem = drAddItemExists(0)
                        drAddItem("IsStatus") = "Updated"
                        'If IsEditItem = False Then
                        drAddItem("Quantity") = drAddItem("Quantity") + 1
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
                        drAddItem("Quantity") = drAddItem("Quantity") + 1
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

                'Code added by irfan on 28/12/2017 for IGST=========================================================================
                _strCustNo = CtrlCustDtls1.lblCustNoValue.Text.Trim()

                IsIGSTApplicableForOutsideCustomer = objComn.checkIGSTAplicableForOutSideStateCustomer(clsAdmin.SiteCode, _strCustNo)
                Dim IGSTtaxCode As String = objComn.ReturnIGSTTaxID(dtTaxCalc)

                If Not dtTaxCalc Is Nothing AndAlso dtTaxCalc.Rows.Count > 0 Then
                    If IsIGSTApplicableForOutsideCustomer = True Then
                        If IGSTtaxCode <> "" Then
                           
                            Dim dv As New DataView(dtTaxCalc, "TAXCODE='" & IGSTtaxCode & "'", "", DataViewRowState.CurrentRows)
                            dtTaxCalc = dv.ToTable
                           
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

                '=====================================================================================================================


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
                drAddItem("ReservedQty") = 0
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
                    End If
                    IsConvertToSalesOrder = True
                Else
                    ShowMessage(getValueByKey("SO004"), "SO004 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Article Tax Details does not Found. ", "Tax Details")
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
            vSalesNo = SearchQuotation()

            If Not (vSalesNo = "" Or vSalesNo = String.Empty) Then
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
                BtnSOReturn.Enabled = True
                BtnSOStockCheck.Enabled = True
                BtnSOCalculater.Enabled = True

            End If
            GridInvoiceSetting()
            GridItemSetting()
            GridDeliverdSetting()
            IssuingCV = False
            TabSalesOrder.SelectedTab = TabSalesOrder.TabPages("TabPageItemDetails")
            'CtrlSalesPerson.CtrlTxtBox.TabIndex = 8
            fnGridColAutoSize(grdSOItems)
            CtrlSalesPerson.CtrlTxtBox.Focus()
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
                BtnSOReturn.Enabled = True
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

                        BtnSOReturn.Enabled = False
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

                        BtnSOReturn.Enabled = True
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
            If Not (vSalesNo > "0" Or vSalesNo = String.Empty) Then
                Exit Function
            Else
                Dim findKey(2) As Object
                Dim drSearchHdr As DataRow
                Dim dvAddsInfo As New DataView
                _dsMain = objQuotation.GetSOTableStruct(vSiteCode, IIf(vSalesNo = String.Empty, 0, vSalesNo), "Cancel")

                If Not (dsMain Is Nothing) AndAlso dsMain.Tables("QuotationHdr").Rows.Count > 0 Then
                    If dsMain.Tables("QuotationHdr").Rows(0)("SOStatus").ToString.ToUpper() = "CLOSED" Then
                        ShowMessage(getValueByKey("SO082"), "SO082 - " & getValueByKey("CLAE04"))
                        'this is allowed populate for historical data.
                        rbgrpSaveNprint.Enabled = False
                        C1Sizer2.Enabled = False
                        CtrlSalesPerson.Enabled = False
                        CtrlRbn1.DgrpPayments.Enabled = False
                        grdSOItems.Enabled = False
                    ElseIf dsMain.Tables("QuotationHdr").Rows(0)("SOStatus").ToString.ToUpper() = "CANCEL" Then
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

                If Not (dsMain Is Nothing) AndAlso dsMain.Tables("QuotationHdr").Rows.Count > 0 Then
                    CtrlSalesInfo.CtrlTxtOrderNo.Text = vSalesNo
                    BtnSearchItem.Enabled = True
                    vfinancialYear = dsMain.Tables("QuotationHdr").Rows(0)("FinYear")
                    findKey(0) = vSiteCode
                    findKey(1) = vfinancialYear
                    findKey(2) = vSalesNo
                    drSearchHdr = dsMain.Tables("QuotationHdr").Rows.Find(findKey)
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

                    dsScanTemp = objQuotation.SetQuotationInQOCancel(vSiteCode, IIf(vSalesNo = String.Empty, 0, vSalesNo))
                    _dsScan.Tables("ItemScanDetails").Clear()
                    vEANList = ""

                    For Each drScanTemp As DataRow In dsScanTemp.Tables("ItemScanDetails").Rows
                        Dim drScan As DataRow = _dsScan.Tables("ItemScanDetails").NewRow
                        drScan("EAN") = drScanTemp("EAN")
                        drScan("Discription") = drScanTemp("Discription")
                        drScan("SellingPrice") = FormatNumber(drScanTemp("SellingPrice"), 2)
                        drScan("Quantity") = drScanTemp("Quantity")
                        drScan("BatchBarcode") = drScanTemp("BatchBarcode")
                        drScan("PickupQty") = 0
                        drScan("DeliveredQty") = IIf(drScanTemp("DeliveredQty") Is DBNull.Value, 0, drScanTemp("DeliveredQty"))
                        drScan("Discount") = Math.Round(IIf(IsDBNull(drScanTemp("Discount")), 0, drScanTemp("Discount")), 2)
                        drScan("NetAmount") = FormatNumber(drScanTemp("NetAmount"), 2)
                        drScan("ExpDelDate") = drScanTemp("ExpDelDate")
                        drScan("BillLineNo") = drScanTemp("BillLineNo")
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
                        drScan("ReservedQty") = False ' IIf(drScanTemp("ReservedQty") > 0, True, False)
                        drScan("Reserved") = False ' IIf(drScanTemp("ReservedQty") > 0, True, False)

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
                        drScan("IsStatus") = IIf(drScanTemp("Status") = False, "Deleted", "Inserted")
                        drScan("SalesStaffID") = drScanTemp("SalesStaffID")
                        vEANList = vEANList & "'" & drScanTemp("EAN") & "', "

                        _dsScan.Tables("ItemScanDetails").Rows.Add(drScan)

                    Next
                    _dsScan.AcceptChanges()

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
                    Next

                    'dsScanReturn.Merge(dsScan)
                    RefreshLoadSOData()
                    dtOtherCharges = dsMain.Tables("QuotationOtherCharges").Copy()
                    CalculateSalesOrderSummory(_dsScan)
                    GridDeliverdSetting()
                    dsInvoice = objSO.SetInvoiceInSOCancel(vSiteCode, IIf(vSalesNo = String.Empty, 0, vSalesNo))
                    RefreshLoadInvcData()


                    CtrlCustDtls1.lblCustNoValue.Text = vCustomerNo
                    dtCustmInfo = objCustm.GetCustomerInformation(CtrlCustDtls1.lblCustTypeValue.Text, vSiteCode, clsAdmin.CLPProgram, vCustomerNo)

                    CtrlCustDtls1.pDisplayDtls(dtCustmInfo)
                    CtrlCustDtls1.cboAddrType.SelectedValue = vAddressType

                    If clsDefaultConfiguration.IsCstTaxRequired AndAlso dsMain.Tables("Quotationhdr").Rows.Count > 0 AndAlso CBool(If(dsMain.Tables("Quotationhdr").Rows(0)("IsCSTApplied") Is DBNull.Value, False, dsMain.Tables("Quotationhdr").Rows(0)("IsCSTApplied"))) = True Then
                        MessageBox.Show(getValueByKey("CST002"), getValueByKey("CLAE04"))
                        IsCSTApplicable = True
                        clsDefaultConfiguration.CSTTaxCode = dsMain.Tables("Quotationhdr").Rows(0)("CSTTaxCode")
                    Else
                        IsCSTApplicable = False
                        clsDefaultConfiguration.CSTTaxCode = ""
                    End If

                    'CtrlCustDtls1.rbCLPMember.Enabled = False
                    'CtrlCustDtls1.rbOtherCust.Enabled = False

                    CtrlCustDtls1.pDisplayDtls(dtCustmInfo)
                    CtrlCustDtls1.cboAddrType.SelectedValue = vAddressType

                    IsNextInvoiceNo = False
                    IsApplyPromotion = True
                End If
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

            Dim dtSource As DataTable = dsScan.Tables("ItemScanDetails") 'dvDisplayItem.ToTable(False, "DEL", "ArticleCode", "Discription", "SellingPrice", "Quantity", "PickUpQty", "DeliveredQty", "Discount", "NetAmount", "ExpDelDate", "Stock", "IsCLP", "ReservedQty", "EAN", "IsStatus")

            If Not dtSource.Columns.Contains("Blankclm") Then
                AddBlankColumn(dtSource)
            End If

            grdSOItems.DataSource = dtSource

            For Each drGridRow As C1.Win.C1FlexGrid.Row In grdSOItems.Rows
                If Not (drGridRow.Index = 0) Then

                    If Not (vEANList.IndexOf(drGridRow.Item("EAN")) = -1) Then
                        If drGridRow.Item("IsStatus") = "Deleted" Then
                            grdSOItems.Rows(drGridRow.Index).Style = grdSOItems.Styles("RowBackColorItemDelete")
                            grdSOItems.Rows(drGridRow.Index).AllowEditing = False
                        End If
                        If drGridRow.Item("Quantity") = drGridRow.Item("DeliveredQty") Then
                            grdSOItems.Rows(drGridRow.Index).Style = grdSOItems.Styles("RowBackColorfullQty")
                            grdSOItems.Rows(drGridRow.Index).AllowEditing = False
                        End If
                    End If

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
                    If CInt(lblOrderQty.Text) = CInt(lblPickupQty.Text) + CInt(lbldeliveredqty.Text) Then
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



                CtrlCashSummary1.lbltxt5 = FormatNumber(vAdvanceAmount, 2)

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
        IsConvertToSalesOrder = False
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
            If dsScan.Tables(0).Rows.Count = 0 Then
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

            If CtrlCustDtls1.cboAddrType.SelectedIndex > 0 Then
                If Not (CtrlCustDtls1.cboAddrType.SelectedValue = 99) Then
                    drDelvAdds = CtrlCustDtls1.dtCustmInfo.Select("AddressType='" & CtrlCustDtls1.cboAddrType.SelectedValue & "' ")(0)

                Else
                    drDelvAdds = Nothing
                End If
            Else
                drDelvAdds = CtrlCustDtls1.dtCustmInfo.Rows(0)
            End If


            If IsAllowedSalesReturn = False Then

                If Not (dsScan.Tables(0).Rows.Count > 0) Then
                    ShowMessage(getValueByKey("SO027"), "SO027 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Please Select Sales Order first", "Sales Order Information")
                    Exit Sub
                End If

                'Comment by Ashish on 25 Nov 2010
                'Adding a check for variable "flg" to enter or skip the "If CDbl(CtrlCashSummary1.lbltxt6) < CDbl(CtrlCashSummary1.lbltxt5) Then" condition
                'This is for allowing saving of SO when override amount = 0
                'If flg = False Then
                '    If Not (CDbl(lblReceivedAmt.Text.Trim) >= CDbl(lblminadvancepaid.Text.Trim)) Then
                '        If CDbl(CtrlCashSummary1.lbltxt6.Trim) > 0 Then
                '            'ShowMessage(getValueByKey("SO017") & CDbl(CDbl(lblminadvancepaid.Text.Trim)), "SO017 - " & getValueByKey("CLAE04"))
                '            'If Not (CDbl(lblReceivedAmt.Text.Trim) >= CDbl(lblminadvancepaid.Text.Trim)) Then
                '            '    If CDbl(CtrlCashSummary1.lbltxt6.Trim) > 0 Then
                '            '        ShowMessage(getValueByKey("SO017") & CDbl(lblminadvancepaid.Text.Trim), getValueByKey("CLAE04"))
                '            BtnAcceptPayment_Click(sender, e)
                '            Exit Sub
                '            'ShowMessage("Need to pay min advance amount.", "Min Advance Amount")
                '        End If
                '    End If
                'End If
                'End of change


                'Comment by Ashish on 25 Nov 2010
                'Adding a check for variable "flg" to enter or skip the "If CDbl(CtrlCashSummary1.lbltxt6) < CDbl(CtrlCashSummary1.lbltxt5) Then" condition
                'This is for allowing saving of SO when override amount = 0
                'If flg = False Then
                '    If CDbl(CtrlCashSummary1.lbltxt6.Trim) < 0 AndAlso Not (CDbl(CtrlCashSummary1.lbltxt6.Trim) = CDbl(lblReceivedAmt.Text.Trim)) Then
                '        ShowMessage(getValueByKey("SO037"), "SO037 - " & getValueByKey("CLAE04"))
                '        BtnAcceptPayment_Click(sender, e)
                '        Exit Sub
                '        'ShowMessage("Need to Return Balance Amount.", "Return Balance Amount")
                '    End If
                'End If
                'End of change

                'Comment by Ashish on 25 Nov 2010
                'Adding a check for variable "flg" to enter or skip the "If CDbl(CtrlCashSummary1.lbltxt6) < CDbl(CtrlCashSummary1.lbltxt5) Then" condition
                'This is for allowing saving of SO when override amount = 0
                'If flg = False Then
                '    If Not (CDbl(CtrlCashSummary1.lbltxt6.Trim) >= CDbl(lblReceivedAmt.Text.Trim)) Then
                '        ShowMessage(getValueByKey("SO038"), "SO038 - " & getValueByKey("CLAE04"))
                '        BtnAcceptPayment_Click(sender, e)
                '        Exit Sub
                '        'ShowMessage("Need to pay Balance Amount.", "Return Balance Amount")
                '    End If
                'End If
                'End of change

                'If OnlineConnect = True Then
                '    'Start=================Apply Customer Loyalty Program======================
                '    If CtrlCustDtls1.lblCustTypeValue.Text = "CLP" AndAlso CInt(lblOrderQty.Text) = CInt(lblPickupQty.Text) + CInt(lbldeliveredqty.Text) Then
                '        vCardType = CtrlCustDtls1.dtCustmInfo.Rows(0)("CARDTYPE").ToString()
                '        CalCulateCLP(vCardType, dsScan.Tables("ItemScanDetails"), "(PickUpQty>0 Or DeliveredQty>0) and IsCLP='True' and IsStatus<>'Deleted'")
                '        Dim Point As Object
                '        Point = dsScan.Tables("ItemScanDetails").Compute("Sum(CLPPoints)", "(PickUpQty>0 Or DeliveredQty>0) and IsCLP='True' and IsStatus<>'Deleted'")
                '        If Not Point Is DBNull.Value Then
                '            TotalPoints = CDbl(Point)
                '        End If
                '    End If

                '    Try
                '        rbnLStatus.SmallImage = Spectrum.My.Resources.connected1.ToBitmap
                '        rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus2")
                '    Catch ex As Exception

                '    End Try
                'Else
                '    Try
                '        rbnLStatus.SmallImage = Spectrum.My.Resources.disconnected1.ToBitmap
                '        rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus1")
                '    Catch ex As Exception

                '    End Try
                'End If
                grdSOItems.FinishEditing()
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

                'If Not (PrepareInvcDataforSave(dsMain) = True) Then
                '    Exit Sub
                'End If
                If Not (PrepareOtherTaxDataforSave(dsMain) = True) Then
                    Exit Sub
                End If

                'If CInt(lblPickupQty.Text) > 0 Then
                '    IsOutboundCreated = True
                '    If Not (PrepareOrderHdrDataforSave(dsMain) = True) Then
                '        Exit Sub
                '    End If
                '    If Not (PrepareOrderDtlDataforSave(dsMain) = True) Then
                '        Exit Sub
                '    End If
                'End If



                'Start=================Change History Information==========================
                Dim dtUnchange As New DataTable
                dtUnchange.Merge(dsMain.Tables("QuotationDtl"))

                Dim dvHdrAudit As New DataView(dsMain.Tables("QuotationHdr"), "", "", DataViewRowState.ModifiedOriginal)
                Dim dvDtlUnchanged As New DataView(dtUnchange, "", "ArticleCode", DataViewRowState.OriginalRows)
                Dim dvDtlchanged As New DataView(dtUnchange, "", "ArticleCode", DataViewRowState.ModifiedCurrent)

                'If dvHdrAudit.ToTable.Rows.Count > 0 Then

                '    dvDtlchanged.AllowEdit = True
                '    For Each drChanged As DataRowView In dvDtlchanged
                '        Dim drIsChages As DataRow = dvDtlUnchanged.ToTable.Select("EAN='" & drChanged("EAN") & "'")(0)

                '        If drChanged("Quantity") = drIsChages("Quantity") AndAlso drChanged("ArticleStatus") <> "Deleted" Then
                '            drChanged.Delete()
                '        ElseIf drChanged("Status") = True Then

                '        End If
                '    Next

                '    If dvDtlchanged.Count > 0 Then
                '        dsMain.Tables("SalesOrderHdrAudit").Merge(dvHdrAudit.ToTable(), False, MissingSchemaAction.Ignore)
                '        'dsMain.Tables("SalesOrderDtlAudit").Merge(dvDtlchanged.ToTable(), False, MissingSchemaAction.Ignore)
                '        For Each drAudit As DataRowView In dvDtlchanged
                '            Dim drNew As DataRow = dsMain.Tables("SalesOrderDtlAudit").NewRow
                '            For Each col As DataColumn In dsMain.Tables("SalesOrderDtlAudit").Columns
                '                If dvDtlchanged.ToTable.Columns.Contains(col.ColumnName) = True Then
                '                    drNew(col.ColumnName) = drAudit(col.ColumnName)
                '                End If
                '            Next
                '            drNew("AmendedNo") = vAmendedNo
                '            dsMain.Tables("SalesOrderDtlAudit").Rows.Add(drNew)
                '        Next
                '        'For Each drDtl As DataRow In dsMain.Tables("SalesOrderDtlAudit").Select("", "", DataViewRowState.Added)
                '        '    drDtl("AmendedNo") = vAmendedNo
                '        'Next
                '    End If

                'End If
                'End===================Change History Information==========================
                If IssuingCV = True And clsAdmin.CVProgram = String.Empty Then
                    ShowMessage(getValueByKey("SO069"), "SO069 - " & getValueByKey("CLAE05"))
                    Exit Sub
                End If

                dtSalesOrderTaxDetails = dsMain.Tables("SalesOrderTaxDtls").Copy()
                If objQuotation.PrepareSaveData(vSalesInvcNo, clsAdmin.DayOpenDate, clsAdmin.CLPProgram, CtrlCustDtls1.lblCustNo.Text, dsMain, False, IsNextInvoiceNo, vSiteCode, CtrlSalesInfo.CtrlTxtOrderNo.Text, clsDefaultConfiguration.StockStorageLocation, clsAdmin.CVProgram, "SalesOrder", vfinancialYear, clsAdmin.UserCode, clsAdmin.CurrentDate, IsOutboundCreated, CVoucherNo, CVVoucherDay, isPromotionApplied, DtDeletedData) = True Then
                    'If OnlineConnect = True Then
                    '    'Apply Customer loyalty Point
                    '    If CtrlCustDtls1.lblCustTypeValue.Text = "CLP" AndAlso CInt(lblOrderQty.Text) = CInt(lblPickupQty.Text) + CInt(lbldeliveredqty.Text) Then
                    '        'CalCulateCLP(vCardType, dsScan.Tables("ItemScanDetails"), "PickUpQty>0 Or DeliveredQty>0 ")

                    '        If dsMainCLP.Tables.Count = 0 Then
                    '            dsMainCLP.Tables.Add(dsMain.Tables("CLPTransaction").Clone())
                    '            dsMainCLP.Tables.Add(dsMain.Tables("CLPTransactionsDetails").Clone)
                    '        End If

                    '        dsMainCLP.Clear()

                    '        If TotalPoints > 0 AndAlso PrepareClpHdrDataforSave(dsMainCLP) = True AndAlso PrepareClpDtlDataforSave(dsMainCLP) = True Then
                    '            If objSO.PrepareSaveClpData(dsMainCLP, vClpProgramId, CtrlCustDtls1.lblCustNoValue.Text, TotalPoints, vSiteCode, CtrlSalesInfo.CtrlTxtOrderNo.Text) = False Then
                    '                ShowMessage(getValueByKey("SO018"), "SO018 - " & getValueByKey("CLAE04"))
                    '                'ShowMessage("CLP Data is not Saved....", "Information")
                    '            End If
                    '        End If

                    '        dsMainCLP.Clear()
                    '    End If

                    '    Try
                    '        rbnLStatus.SmallImage = Spectrum.My.Resources.connected1.ToBitmap
                    '        rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus2")
                    '    Catch ex As Exception

                    '    End Try
                    'Else
                    '    Try
                    '        rbnLStatus.SmallImage = Spectrum.My.Resources.disconnected1.ToBitmap
                    '        rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus1")
                    '    Catch ex As Exception

                    '    End Try
                    'End If


                    'Print Sales Order Information.-----------------------------------
                    PrintSalesOrders(drSiteInfo, drHomeAdds, drDelvAdds)
                    Dim totalPay As Double

                    'Print Sales Invoice Information----------------------------------
                    'If Not (dsPayment.Tables("MSTRecieptType") Is Nothing) AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then

                    '    PrintSalesOrdersInvoice(drSiteInfo, drHomeAdds, drDelvAdds)

                    '    For Each dr As DataRow In dsPayment.Tables("MSTRecieptType").Select("RecieptTypeCode='CreditVouc(I)'", "", DataViewRowState.CurrentRows)
                    '        totalPay = IIf(dr("Amount") > 0, dr("Amount"), dr("Amount") * -1)

                    '        'PrintCreditVoucher(drSiteInfo, totalPay)
                    '        clsVoucher.PrintGiftVoucherAndCreditNote("SalesOrder", clsAdmin.SiteCode, "CreditNote", String.Empty, totalPay, String.Empty, clsAdmin.UserName, CtrlSalesInfo.CtrlTxtOrderNo.Text, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                    '    Next
                    '    For Each dr As DataRow In dsPayment.Tables("MSTRecieptType").Select("RecieptTypeCode='GiftVoucher(I)'", "", DataViewRowState.CurrentRows)
                    '        totalPay = IIf(dr("Amount") > 0, dr("Amount"), dr("Amount") * -1)

                    '        'PrintCreditVoucher(drSiteInfo, totalPay)
                    '        clsVoucher.PrintGiftVoucherAndCreditNote("SalesOrder", clsAdmin.SiteCode, "GiftVoucher", String.Empty, totalPay, String.Empty, clsAdmin.UserName, CtrlSalesInfo.CtrlTxtOrderNo.Text, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                    '    Next
                    '    'If CInt(CDbl(CtrlCashSummary1.lbltxt6)) < 0 Then
                    '    '    PrintCreditVoucher(drSiteInfo)
                    '    'Else
                    '    '    PrintSalesOrdersInvoice(drSiteInfo, drHomeAdds, drDelvAdds)
                    '    'End If
                    'End If

                    'Print Sales Delivery Information---------------------------------
                    'If CDbl(lblPickupQty.Text) > 0 Then
                    '    PrintSalesOrdersDelivery(drSiteInfo, drHomeAdds, drDelvAdds)
                    'End If

                    'If CDbl(lblPickupQty.Text) > 0 AndAlso dsPayment.Tables("MSTRecieptType") Is Nothing Then
                    '    Dim dtInvoice As New DataTable
                    '    dtInvoice = objComn.GetPaymentInfo

                    '    PrintSalesOrdersInvoice(drSiteInfo, drHomeAdds, drDelvAdds, dtInvoice)
                    'End If

                    'If IsGiftVoucher Then
                    '    PrintGiftReceipt()
                    'End If

                    ShowMessage(getValueByKey("QO079"), "QO079 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Sales Order Updated", "Sales Order")
                    ResetSalesOrder()
                    CtrlSalesInfo.CtrlTxtOrderNo.Text = ""
                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                Else
                    ShowMessage(getValueByKey("QO080"), "QO080 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Sales Order does not Updated", "Sales Order")
                End If

            Else
                ' function for sales order return **********************************
                Dim tempPurchaseQty As Decimal = 0
                If Not (dsScan.Tables(0).Rows.Count > 0) Then
                    ShowMessage(getValueByKey("SO027"), "SO027 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Please Select Sales Order first", "Sales Order Information")
                    Exit Sub
                ElseIf txtReturnReason.Text.Trim = "" Then
                    ShowMessage(getValueByKey("SO041"), "SO041 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Please Enter Reason for Return Sales Order Articles", "Sales Order Information")
                    txtReturnReason.Select()
                    Exit Sub
                End If

                dsMain.Tables("QuotationHdr").RejectChanges()

                dsMain.Tables("QuotationDtl").RejectChanges()


                Dim drNewReturnHdr As DataRow
                Dim drOldReturnHdr As DataRow
                Dim findKeyh(2) As Object
                Dim IsNewReturn As Boolean = False

                If dsScanReturn.Tables(0).Select("PickUpQty>0").Count > 0 Then

                    'Changed by Rohit to generate Document No. for proper sorting
                    Dim NewSalesNo As String = String.Empty
                    Try
                        NewSalesNo = GenDocNo("SO" & clsAdmin.TerminalID & vfinancialYear.ToString.Substring(vfinancialYear.ToString.Length - 2, 2), 15, objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode))
                    Catch ex As Exception
                        NewSalesNo = "SO" & clsAdmin.TerminalID & objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode)
                    End Try
                    'End Change by Rohit

                    For Each drReturnSales As DataRow In dsScanReturn.Tables(0).Select("PickUpQty>0")
                        Dim drNewReturnDtl As DataRow
                        Dim drOldReturnDtl As DataRow
                        Dim findKeyd(3) As Object

                        'Start- Update Old Sales Order for Return Sales Order Article
                        findKeyd(0) = vSiteCode
                        findKeyd(1) = vfinancialYear
                        findKeyd(2) = CtrlSalesInfo.CtrlTxtOrderNo.Text
                        findKeyd(3) = drReturnSales("EAN").ToString
                        drOldReturnDtl = dsMain.Tables("QuotationDtl").Rows.Find(findKeyd)

                        drOldReturnDtl("DeliveredQty") = drOldReturnDtl("DeliveredQty") - drReturnSales("PickUpQty")
                        drOldReturnDtl("ReturnSaleOrderNo") = NewSalesNo
                        drOldReturnDtl("ReturnSaleOrderDt") = vCurrentDate
                        drOldReturnDtl("ReturnQty") = IIf(drOldReturnDtl("ReturnQty") Is DBNull.Value, 0, drOldReturnDtl("ReturnQty")) + drReturnSales("PickUpQty")
                        drOldReturnDtl("SalesReturnReasonCode") = IIf(drReturnSales("ReturnReasonCode") Is DBNull.Value, txtReturnReason.Text.Trim, drReturnSales("ReturnReasonCode"))
                        drOldReturnDtl("UpdatedAt") = vSiteCode
                        drOldReturnDtl("UpdatedBy") = vUserName
                        drOldReturnDtl("UpdatedOn") = vCurrentDate


                        'Start- Add New Sales Order for Return Sales Order Article
                        findKeyd(0) = vSiteCode
                        findKeyd(1) = vfinancialYear
                        findKeyd(2) = NewSalesNo
                        findKeyd(3) = drReturnSales("EAN").ToString
                        drNewReturnDtl = dsMain.Tables("QuotationDtl").Rows.Find(findKeyd)

                        If drNewReturnDtl Is Nothing Then
                            drNewReturnDtl = dsMain.Tables("QuotationDtl").NewRow()
                            IsNewReturn = True
                        End If

                        drNewReturnDtl("SiteCode") = drOldReturnDtl("SiteCode")
                        drNewReturnDtl("SaleOrderNumber") = NewSalesNo
                        drNewReturnDtl("EAN") = drOldReturnDtl("EAN")

                        For NewRowIndex = 3 To drOldReturnDtl.ItemArray.Count - 1
                            drNewReturnDtl(NewRowIndex) = drOldReturnDtl(NewRowIndex)
                        Next

                        drNewReturnDtl("NetSellingPrice") = FormatNumber(drOldReturnDtl("NetAmount") / drOldReturnDtl("Quantity"), 2)
                        tempPurchaseQty = drReturnSales("Quantity")
                        drNewReturnDtl("Quantity") = drReturnSales("PickUpQty") * -1
                        drNewReturnDtl("GrossAmount") = FormatNumber(drNewReturnDtl("SellingPrice") * drNewReturnDtl("Quantity"), 2)
                        drNewReturnDtl("NetAmount") = FormatNumber(drNewReturnDtl("NetSellingPrice") * drNewReturnDtl("Quantity"), 2)
                        drNewReturnDtl("DeliveredQty") = 0
                        drNewReturnDtl("ReservedQty") = 0
                        drNewReturnDtl("OfferNo") = ""
                        drNewReturnDtl("IsCLPApplicable") = 0
                        drNewReturnDtl("CLPPoints") = 0

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
                        drNewReturnDtl("CreatedBy") = vUserName
                        drNewReturnDtl("CreatedOn") = vCurrentDate
                        drNewReturnDtl("UpdatedAt") = vSiteCode
                        drNewReturnDtl("UpdatedBy") = vUserName
                        drNewReturnDtl("UpdatedOn") = vCurrentDate
                        drNewReturnDtl("FinYear") = vfinancialYear

                        If IsNewReturn = True Then
                            dsMain.Tables("QuotationDtl").Rows.Add(drNewReturnDtl)
                            IsNewReturn = False
                        End If

                        'End- Add New Sales Order for Return Sales Order Article
                    Next
                    findKeyh(0) = vSiteCode
                    findKeyh(1) = vfinancialYear
                    findKeyh(2) = NewSalesNo
                    drNewReturnHdr = dsMain.Tables("QuotationHdr").Rows.Find(findKeyh)

                    If drNewReturnHdr Is Nothing Then
                        drNewReturnHdr = dsMain.Tables("QuotationHdr").NewRow()

                        drOldReturnHdr = dsMain.Tables("QuotationHdr").Rows(0)

                        drNewReturnHdr("SiteCode") = drOldReturnHdr("SiteCode")
                        drNewReturnHdr("SaleOrderNumber") = NewSalesNo
                        drNewReturnHdr("TerminalID") = vTerminalID
                        drNewReturnHdr("TransactionCode") = vDocType

                        For NewRowIndex = 5 To drOldReturnHdr.ItemArray.Count - 1
                            drNewReturnHdr(NewRowIndex) = drOldReturnHdr(NewRowIndex)
                        Next

                        drNewReturnHdr("NetAmt") = dsMain.Tables("QuotationDtl").Compute("Sum(NetAmount)", "ArticleStatus='Return'")
                        drNewReturnHdr("CostAmt") = dsMain.Tables("QuotationDtl").Compute("Sum(CostAmount)", "ArticleStatus='Return'")
                        drNewReturnHdr("GrossAmt") = dsMain.Tables("QuotationDtl").Compute("Sum(GrossAmount)", "ArticleStatus='Return'")
                        drNewReturnHdr("DiscountPercentage") = 0
                        drNewReturnHdr("DiscountAmt") = dsMain.Tables("QuotationDtl").Compute("Sum(LineDiscount)", "ArticleStatus='Return'")
                        drNewReturnHdr("TotalDiscount") = dsMain.Tables("QuotationDtl").Compute("Sum(DiscountAmount)", "ArticleStatus='Return'")
                        drNewReturnHdr("CurrencyCode") = clsAdmin.CurrencyCode
                        drNewReturnHdr("TaxAmount") = dsMain.Tables("QuotationDtl").Compute("Sum(TotalTaxAmount)", "ArticleStatus='Return'")
                        drNewReturnHdr("SOStatus") = "Return"
                        drNewReturnHdr("CreatedAt") = vSiteCode
                        drNewReturnHdr("CreatedBy") = vUserName
                        drNewReturnHdr("CreatedOn") = vCurrentDate
                        drNewReturnHdr("UpdatedAt") = vSiteCode
                        drNewReturnHdr("UpdatedBy") = vUserName
                        drNewReturnHdr("UpdatedOn") = vCurrentDate
                        drNewReturnHdr("FinYear") = vfinancialYear
                        dsMain.Tables("QuotationHdr").Rows.Add(drNewReturnHdr)
                    End If
                End If
                Dim dstemp As DataSet = dsMain.Copy()
                If objSO.PrepareSaveData(vSalesInvcNo, clsAdmin.DayOpenDate, clsAdmin.CLPProgram, CtrlCustDtls1.lblCustNo.Text, dsMain, True, IsNextInvoiceNo, vSiteCode, CtrlSalesInfo.CtrlTxtOrderNo.Text, clsDefaultConfiguration.StockStorageLocation, clsAdmin.CVProgram, "SalesOrder", vfinancialYear, clsAdmin.UserCode, clsAdmin.CurrentDate, False, CVoucherNo, CVVoucherDay) = True Then
                    Dim dttemp As DataTable = dsScanReturn.Tables(0).Copy()
                    dsScanReturn.Tables(0).Clear()

                    For Each dr As DataRow In dstemp.Tables("QuotationDtl").Select("ArticleStatus='Return'", "", DataViewRowState.CurrentRows)
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
                    CtrlSalesInfo.CtrlTxtOrderNo.Text = ""

                    BtnSOSave.Visible = True
                    BtnSOPrint.Visible = True
                    rbnGrpCMPromotion.Visible = True
                    BtnSOAcceptPayment.Visible = True
                    BtnSOOtherCharges.Visible = True
                    BtnSOReturn.Visible = True
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
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Sub


    Private Function PrintGiftReceipt() As Boolean
        Try
            If Not dsPayment Is Nothing AndAlso dsPayment.Tables.Contains("MSTRecieptType") Then

                Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.GiftVoucherDocumentPrint, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, vSalesInvcNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo.CtrlTxtCustOrdRef.Text, vSalesInvcNo, GiftReceiptMessage, Nothing, Nothing, dtPrinterInfo, isQuotationPrint:=True, ShowFullName:=clsDefaultConfiguration.PrintItemFullName)

                'Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, vSalesInvcNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo1.CtrlTxtCustOrdRef.Text)

            Else

                Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.GiftVoucherDocumentPrint, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, vSalesInvcNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo.CtrlTxtCustOrdRef.Text, vSalesInvcNo, GiftReceiptMessage, Nothing, Nothing, dtPrinterInfo, isQuotationPrint:=True, ShowFullName:=clsDefaultConfiguration.PrintItemFullName)

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

            drSOHdr = _dsMain.Tables("QuotationHdr").Rows.Find(findKey)
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

                If CInt(lblOrderQty.Text) = CInt(lblPickupQty.Text) + CInt(lbldeliveredqty.Text) Then
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
                drSOHdr("UPDATEDBY") = vUserName
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

        Try
            'For Each drScan As DataRow In dsScan.Tables("ItemScanDetails").Select("IsStatus In ('Deleted','Updated') ")
            For Each drScan As DataRow In dsScan.Tables("ItemScanDetails").Rows
                Try
                    Dim billLineNo As Integer
                    If IsDBNull(drScan("BillLineNo")) = False Then
                        billLineNo = drScan("BillLineNo")
                    Else
                        billLineNo = dsMain.Tables("QuotationDtl").Rows.Count + 1
                    End If
                    findKey(0) = vSiteCode
                    findKey(1) = vfinancialYear
                    findKey(2) = CtrlSalesInfo.CtrlTxtOrderNo.Text
                    findKey(3) = drScan("EAN").ToString
                    findKey(4) = billLineNo
                    drSODtl = dsMain.Tables("QuotationDtl").Rows.Find(findKey)
                    If drSODtl Is Nothing Then
                        drSODtl = dsMain.Tables("QuotationDtl").NewRow
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
                    If CInt(drScan("DeliveredQty")) + CInt(drScan("PickUpQty")) = CInt(drScan("Quantity")) Then
                        drSODtl("ArticleStatus") = "Closed"
                    End If
                    drSODtl("BillLineNo") = billLineNo
                    drSODtl("SaleOrderNumber") = CtrlSalesInfo.CtrlTxtOrderNo.Text
                    drSODtl("SiteCode") = vSiteCode
                    drSODtl("FinYear") = vfinancialYear
                    drSODtl("BatchBarcode") = drScan("BatchBarcode")
                    drSODtl("EAN") = drScan("EAN")
                    drSODtl("ArticleCode") = drScan("ArticleCode")
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
                            drSODtl("Delivered_Qty") = IIf(drScan("PickUpQty") = 0, drScan("DeliveredQty"), drScan("DeliveredQty") + drScan("PickUpQty"))
                        Else
                            drSODtl("Delivered_Qty") = 0
                        End If
                    End If

                    'If drScan("Reserved") Is DBNull.Value Then
                    '    If Not drScan("ReservedQty") Is DBNull.Value AndAlso drScan("ReservedQty") = True Then
                    '        drSODtl("ReservedQty") = drScan("Quantity")
                    '        drSODtl("Reserved_Qty") = drScan("Quantity")
                    '    End If
                    'ElseIf drScan("Reserved") = drScan("ReservedQty") Then
                    '    If Not drScan("ReservedQty") Is DBNull.Value AndAlso drScan("ReservedQty") = True Then
                    '        drSODtl("ReservedQty") = IIf(drSODtl("ReservedQty") Is DBNull.Value, 0, drSODtl("ReservedQty")) + drScan("PickUpQty") * -1
                    '        drSODtl("Reserved_Qty") = drScan("PickUpQty") * -1
                    '    End If
                    'Else
                    '    If Not drScan("ReservedQty") Is DBNull.Value AndAlso drScan("ReservedQty") = True Then
                    '        drSODtl("ReservedQty") = IIf(drSODtl("ReservedQty") Is DBNull.Value, 0, drSODtl("ReservedQty")) + CDbl(drScan("Quantity")) - CDbl(drScan("PickUpQty") + drScan("DeliveredQty"))
                    '        drSODtl("Reserved_Qty") = CDbl(drScan("Quantity")) - CDbl(drScan("PickUpQty") + drScan("DeliveredQty"))
                    '    End If
                    '    If Not drScan("ReservedQty") Is DBNull.Value AndAlso drScan("ReservedQty") = False Then
                    '        drSODtl("ReservedQty") = IIf(drSODtl("ReservedQty") Is DBNull.Value, 0, drSODtl("ReservedQty")) + (drScan("Quantity") - drScan("DeliveredQty")) * -1
                    '        drSODtl("Reserved_Qty") = (drScan("Quantity") - drScan("DeliveredQty")) * -1
                    '    End If
                    'End If
                    drSODtl("ReservedQty") = 0.0
                    drSODtl("Reserved_Qty") = 0.0

                    'If drScan("ReservedQty") > 0 AndAlso drScan("Quantity") > drScan("PickUpQty") + drScan("DeliveredQty") Then
                    '    drSODtl("ReservedQty") = CDbl(drScan("Quantity")) - CDbl(drScan("PickUpQty") + drScan("DeliveredQty"))
                    'Else
                    '    drSODtl("ReservedQty") = 0
                    'End If

                    drSODtl("CostAmount") = DBNull.Value
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
                    drSODtl("UPDATEDBY") = vUserName
                    drSODtl("UPDATEDON") = vCurrentDate

                    If IsAddNewRow = True Then
                        drSODtl("CREATEDAT") = vSiteCode
                        drSODtl("CREATEDBY") = vUserName
                        drSODtl("CREATEDON") = vCurrentDate

                        dsMain.Tables("QuotationDtl").Rows.Add(drSODtl)
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
            SetCostPrice(clsDefaultConfiguration.isMAPbasedCost, dsMain.Tables("QuotationDtl"), clsAdmin.SiteCode, "CostAmount", clsDefaultConfiguration.IsBatchManagementReq)
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
                    drInvc("CREATEDBY") = vUserName
                    drInvc("CREATEDON") = vCurrentDate
                    drInvc("UPDATEDAT") = vSiteCode
                    drInvc("UPDATEDBY") = vUserName
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
            dtOtherCharges.TableName = "QuotationOtherCharges"
            If dsMain.Tables.Contains("Table1") Then
                dsMain.Tables("Table1").TableName = "QuotationOtherCharges"
            End If
            If dsMain.Tables.Contains("QuotationOtherCharges") Then
                dsMain.Tables.Remove("QuotationOtherCharges")
                dsMain.Tables.Add(dtOtherCharges)
                TempOtherChargesTable = dtOtherCharges.Copy()
                For Each dr As DataRow In dsMain.Tables("QuotationOtherCharges").Rows
                    If dr.RowState <> DataRowState.Deleted Then
                        dr("CREATEDAT") = vSiteCode
                        dr("CREATEDBY") = vUserName
                        dr("CREATEDON") = vCurrentDate
                        dr("STATUS") = True
                        dr("UpdatedAT") = vSiteCode
                        dr("UpdatedBY") = vUserName
                        dr("UpdatedON") = vCurrentDate
                        dr("STATUS") = True
                    End If
                Next
            End If
            If Not dsMain.Tables("QuotationOtherCharges") Is Nothing AndAlso dsMain.Tables("QuotationOtherCharges").Rows.Count <= 0 Then
                dsMain.Tables.Remove("QuotationOtherCharges")
            End If
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
            drOrderHdr("CREATEDBY") = vUserName
            drOrderHdr("CREATEDON") = vCurrentDate
            drOrderHdr("UPDATEDAT") = vSiteCode
            drOrderHdr("UPDATEDBY") = vUserName
            drOrderHdr("UPDATEDON") = vCurrentDate
            drOrderHdr("STATUS") = True

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

                drOrderDtl("PONo") = DBNull.Value
                drOrderDtl("BirthListId") = DBNull.Value
                drOrderDtl("SaleOrderNumber") = CtrlSalesInfo.CtrlTxtOrderNo.Text
                drOrderDtl("AllocationRule") = DBNull.Value
                drOrderDtl("MRP") = drScan("SellingPrice")
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
                drOrderDtl("CREATEDBY") = vUserName
                drOrderDtl("CREATEDON") = vCurrentDate
                drOrderDtl("UPDATEDAT") = vSiteCode
                drOrderDtl("UPDATEDBY") = vUserName
                drOrderDtl("UPDATEDON") = vCurrentDate
                drOrderDtl("STATUS") = True

                If IsNewRow = True Then
                    dsMain.Tables("OrderDtl").Rows.Add(drOrderDtl)
                    IsNewRow = False
                End If
                rowIndex = rowIndex + 1
                'End If
            Next

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
            drClpHdr("CREATEDBY") = vUserName
            drClpHdr("CREATEDON") = vCurrentDate
            drClpHdr("UPDATEDAT") = vSiteCode
            drClpHdr("UPDATEDBY") = vUserName
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
                drClpDtl("CREATEDBY") = vUserName
                drClpDtl("CREATEDON") = vCurrentDate
                drClpDtl("UPDATEDAT") = vSiteCode
                drClpDtl("UPDATEDBY") = vUserName
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

            Dim dsOtherCharges As New DataSet
            dsOtherCharges.Clear()
            Dim dt As DataTable
            If dsMain.Tables.Contains("QuotationOtherCharges") Then
                dt = dsMain.Tables("QuotationOtherCharges").Copy()
            End If
            If Not dt Is Nothing AndAlso dt.Rows.Count = 0 AndAlso Not TempOtherChargesTable Is Nothing Then
                dt = TempOtherChargesTable.Copy()
            ElseIf dt Is Nothing Then
                dt = TempOtherChargesTable.Copy()
            End If
            dsOtherCharges.Tables.Add(dt)
            dt.TableName = "NewOtherCharges"

            'Dim dtOld As DataTable = dsMain.Tables("QuotationOtherCharges").Copy()
            'dsOtherCharges.Tables.Add(dtOld)
            Dim strSOStatus As String = ""
            If Not dsPayment Is Nothing AndAlso dsPayment.Tables.Contains("MSTRecieptType") Then

                If CInt(lblOrderQty.Text) = CInt(lblPickupQty.Text) + CInt(lbldeliveredqty.Text) Then
                    strSOStatus = "Closed"
                Else
                    strSOStatus = "Open"

                End If
                Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, vSalesNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, strSOStatus, dsOtherCharges, "", Nothing, True, ShowFullName:=clsDefaultConfiguration.PrintItemFullName)
            Else
                ' this is add to get status in print of SO document when call in edit
                'bug 251
                If lblPickupQty.Text <> String.Empty And lbldeliveredqty.Text <> String.Empty Then
                    If CInt(lblOrderQty.Text) = CInt(lblPickupQty.Text) + CInt(lbldeliveredqty.Text) Then
                        strSOStatus = "Closed"
                    Else
                        strSOStatus = "Open"

                    End If
                End If

                ' this is add to get status in print of SO document when call in edit

                Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, vSalesNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), Nothing, CtrlSalesInfo.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, strSOStatus, dsOtherCharges, "", Nothing, True, ShowFullName:=clsDefaultConfiguration.PrintItemFullName)

            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    Private Function PrintSalesOrdersReturn(ByVal drSite As DataRow, ByVal drHAdds As DataRow) As Boolean
        Try
            Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.SOReturnStatus, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, vSalesNo, CtrlCustDtls1.dtCustmInfo, dsScanReturn.Tables("ItemScanDetails"), Nothing, CtrlSalesInfo.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, "Open", Nothing, txtReturnReason.Text, isQuotationPrint:=True, ShowFullName:=clsDefaultConfiguration.PrintItemFullName)

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
            If dsMain.Tables.Contains("QuotationOtherCharges") Then
                dt = dsMain.Tables("QuotationOtherCharges").Copy()
            End If
            If Not dt Is Nothing AndAlso dt.Rows.Count = 0 Then
                dt = TempOtherChargesTable.Copy()
            ElseIf dt Is Nothing Then
                dt = TempOtherChargesTable.Copy()
            End If
            dsOtherCharges.Tables.Add(dt)
            dt.TableName = "NewOtherCharges"

            If dtZInvc IsNot Nothing Then
                Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Payment, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, vSalesNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dtZInvc, CtrlSalesInfo.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, "", dsOtherCharges, "", dtSalesOrderTaxDetails, isQuotationPrint:=True, ShowFullName:=clsDefaultConfiguration.PrintItemFullName)
            Else
                Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Payment, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, vSalesNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, "", dsOtherCharges, "", dtSalesOrderTaxDetails, isQuotationPrint:=True, ShowFullName:=clsDefaultConfiguration.PrintItemFullName)
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

            Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.DeliveryNote, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, vSalesNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, isQuotationPrint:=True, ShowFullName:=clsDefaultConfiguration.PrintItemFullName)



        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Function

#End Region
#Region "Common Button Action"


    Private IsGiftVoucher As Boolean

    Private Sub Btnpay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If dsMain Is Nothing OrElse dsMain.Tables("QuotationHdr") Is Nothing OrElse Not dsMain.Tables("QuotationHdr").Rows.Count > 0 Then
                ShowMessage(getValueByKey("frmnquotationupdate.Warning"), getValueByKey("CLAE04"))
                Exit Sub
            End If
            Dim salesorderno As String = dsMain.Tables("QuotationHdr").Rows(0)("SaleOrderNumber")
            If Not (PrepareOtherTaxDataforSave(dsMain) = True) Then
                Exit Sub
            End If
            Dim dtOtherChargesInfo As New DataTable
            If (dsMain.Tables.Contains("QuotationOtherCharges")) Then
                dtOtherChargesInfo = dsMain.Tables("QuotationOtherCharges").Copy
            End If

            If IsConvertToSalesOrder Then
                If MessageBox.Show("This Quotation is not saved. Click OK to Save  Quotation and Convert to Sales Order.", getValueByKey("CLAE04"), MessageBoxButtons.OKCancel) = Windows.Forms.DialogResult.OK Then
                    If dsMain.Tables("QuotationHdr").Rows.Count > 0 Then
                        If Not (dsScan.Tables(0).Rows.Count > 0) Then
                            ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                            Exit Sub
                        End If
                        BtnSaveSalesOrder_Click(sender, e)
                    End If
                Else
                    Exit Sub
                End If
            End If
            IsFormClosing = True
            Dim frmSalesOrder As New frmNSalesOrderCreation(True, salesorderno, "", CtrlCustDtls1.lblCustNoValue.Text, dtOtherChargesInfo)
            MDISpectrum.ShowChildForm(frmSalesOrder, True)
            frmSalesOrder.CtrlSalesPersons.CtrlSalesPersons.SelectedIndex = Me.CtrlSalesPerson.CtrlSalesPersons.SelectedIndex
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
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
            If boolIsReturn = False Then
                If Not (dsScan.Tables(0).Rows.Count > 0) Then
                    'ShowMessage("Please Scan Article first", "Sales Order Information")
                    ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                    Exit Sub
                    'Rakesh-20.Dec.2013-As per Rama's suggestion
                    'ElseIf Not CDbl(CtrlCashSummary1.lbltxt6) > Decimal.Zero AndAlso Not CDbl(CtrlCashSummary1.lbltxt6) < Decimal.Zero Then
                    '    ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                    '    Exit Sub
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

                'Dim obj As New frmSpecialPrompt("What you want to pay")
                'obj.ShowTextBox = True
                'obj.ShowDialog()
                'If Not obj.GetResult Is Nothing Then
                '    objPayment.CustomerWantPay = obj.GetResult
                'End If

                '  New form for accepting payment in sales order.
                If CDbl(CDbl(CtrlCashSummary1.lbltxt6.Trim)) < 0 AndAlso Not (CDbl(CDbl(CtrlCashSummary1.lbltxt6.Trim)) = CDbl(CDbl(lblReceivedAmt.Text.Trim))) Then
                Else
                    Dim Obj As New frmNHowMuchtoPay

                    Obj.CtrlTxtMinAmt.Text = lblminadvancepaid.Text
                    Obj.CtrlTxtPickAmt.Text = PickAmtToPay() 'Math.Round(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "")), 2)
                    Obj.CtrlTxtPickAmt.Text = MyRound(CDbl(Obj.CtrlTxtPickAmt.Text), clsDefaultConfiguration.BillRoundOffAt)
                    Obj.TotalBalAmt = CDbl(CtrlCashSummary1.lbltxt6)
                    Obj.ctrlTxtHowMuchPay.Text = CDbl(lblminadvancepaid.Text)
                    If Obj.TotalBalAmt > 0 Then
                        Obj.ShowDialog(Me)  ' this is add on 24.feb.2010 because this screen appear alone if user toggle with ALT key
                    End If


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
                    If Not Obj.ctrlTxtHowMuchPay Is Nothing Then
                        objPayment.CustomerWantPay = Obj.ctrlTxtHowMuchPay.Text
                        objPayment.ctrlPayCash.txtCash.Value = Obj.ctrlTxtHowMuchPay.Text
                    End If

                End If



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
            If boolIsReturn = False Then

                If dsMain Is Nothing OrElse dsMain.Tables("QuotationHdr") Is Nothing OrElse Not dsMain.Tables("QuotationHdr").Rows.Count > 0 Then
                    ShowMessage(getValueByKey("frmnquotationupdate.Warning"), getValueByKey("CLAE04"))
                    Exit Sub
                End If

                If Not (dsScan.Tables(0).Rows.Count > 0) Then
                    'ShowMessage("Please Scan Article first", "Sales Order Information")
                    ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                    Exit Sub
                    'ElseIf Not CDbl(CtrlCashSummary1.lbltxt6) > Decimal.Zero Then
                    '    ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                    '    Exit Sub
                End If
                '    Dim Obj As New frmNHowMuchtoPay

                '    Obj.CtrlTxtMinAmt.Text = lblminadvancepaid.Text
                '    Obj.CtrlTxtPickAmt.Text = PickAmtToPay() 'Math.Round(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "")), 2)
                '    Obj.CtrlTxtPickAmt.Text = MyRound(Obj.CtrlTxtPickAmt.Text, clsDefaultConfiguration.BillRoundOffAt)
                '    Obj.TotalBalAmt = CDbl(CtrlCashSummary1.lbltxt6)
                '    Obj.ctrlTxtHowMuchPay.Text = CDbl(lblminadvancepaid.Text)
                '    Obj.ShowDialog()


                '    Dim objPaymentByCash As New frmNAcceptPaymentByCash("SO")
                '    objPaymentByCash.TotalBillAmount = CDbl(CtrlCashSummary1.lbltxt6)
                '    objPaymentByCash.TotalMinimumAmount = CDbl(lblminadvancepaid.Text)               
                '    If Not Obj.ctrlTxtHowMuchPay Is Nothing Then
                '        objPaymentByCash.CustomerWantPay = Obj.ctrlTxtHowMuchPay.Text
                '    End If
                '    objPaymentByCash.ShowDialog()

                '    If Not (objPaymentByCash.IsCancelAcceptPayment) Then
                '        If Not objPaymentByCash.ReciptTotalAmount Is Nothing And objPaymentByCash.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                '            _dsPayment = objPaymentByCash.ReciptTotalAmount
                '            'Dim ds As New DataSet()
                '            'ds.Tables.Add(dt)

                '            objPaymentByCash.Close()
                '            'If Not ds Is Nothing Then
                '            If objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeSave Then
                '                lblReceivedAmt.Text = CDbl(dsPayment.Tables(0).Compute("Sum(Amount)", ""))
                '                BtnSaveSalesOrder_Click(sender, e)
                '            ElseIf objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeGift Then
                '                IsGiftVoucher = True
                '                GiftReceiptMessage = objPaymentByCash.GiftReceiptMessage
                '                lblReceivedAmt.Text = CDbl(dsPayment.Tables(0).Compute("Sum(Amount)", ""))
                '                BtnSaveSalesOrder_Click(sender, e)
                '            End If

                '            'End If
                '        Else
                '            ShowMessage(getValueByKey("SO070"), "SO070 - " & getValueByKey("CLAE05"))
                '        End If
                '    End If
                IsGiftVoucher = False
                BtnSaveSalesOrder_Click(sender, e)
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM033"), "CM033 - " & getValueByKey("CLAE04"))
            LogException(ex)
            'ShowMessage("Error in Updating cash payment data ", "Information")
        End Try


    End Sub
    Private Sub BtnPayCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If boolIsReturn = False Then
                If Not (dsScan.Tables(0).Rows.Count > 0) Then
                    'ShowMessage("Please Scan Article first", "Sales Order Information")
                    ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                    Exit Sub
                ElseIf Not CDbl(CtrlCashSummary1.lbltxt6) > Decimal.Zero Then
                    ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
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
                        ShowMessage(getValueByKey("SO070"), "SO070 - " & getValueByKey("CLAE04"))
                    End If
                End If
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CM038"), "CM038 - " & getValueByKey("CLAE04"))
            LogException(ex)
        End Try

    End Sub
    Private Sub BtnPayCheque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If boolIsReturn = False Then
                If Not (dsScan.Tables(0).Rows.Count > 0) Then
                    'ShowMessage("Please Scan Article first", "Sales Order Information")
                    ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                    Exit Sub
                ElseIf Not CDbl(CtrlCashSummary1.lbltxt6) > Decimal.Zero Then
                    ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
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
                        ShowMessage(getValueByKey("SO070"), "SO070 - " & getValueByKey("CLAE05"))
                    End If
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
                ShowMessage(getValueByKey("frmnquotationupdate.Warning"), "frmnquotationupdate.Warning - " & getValueByKey("CLAE04"))
                'ShowMessage("Please Select Sales Order first", "Sales Order Information")
                Exit Sub
            End If

            Dim objOpenAddOtherCharges As New frmNAddOthrChrgForSO
            Dim dt As DataTable = dsMain.Tables("QuotationOtherCharges").Copy()
            Dim ChargesRowCounts As Integer = dsMain.Tables("QuotationOtherCharges").Rows.Count
            If dtOtherCharges.Rows.Count <= 0 Then
                dtOtherCharges = dt
            End If
            'If Not dsMain.Tables("SalesOrderOtherCharges") Is Nothing AndAlso dsMain.Tables("SalesOrderOtherCharges").Rows.Count > 0 Then
            objOpenAddOtherCharges.dtOtherCharge = dtOtherCharges
            'End If
            objOpenAddOtherCharges.SalesOrderNo = vSalesNo
            objOpenAddOtherCharges.ShowDialog()
            If objOpenAddOtherCharges.CancelOthercharges = True Then
                Exit Sub
            End If
            If (objOpenAddOtherCharges.dtOtherCharge.Rows.Count() > ChargesRowCounts Or objOpenAddOtherCharges.dtOtherCharge.Rows.Count() < ChargesRowCounts) Then
                IsConvertToSalesOrder = True
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

        PrintSalesOrders(drSiteInfo, drHomeAdds, drDelvAdds)
        'ResetSalesOrder()
        'CtrlSalesInfo.CtrlTxtOrderNo.Text = ""
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
        'BtnSOSave.Visible = True
        'BtnSOPrint.Visible = True
        'rbnGrpCMPromotion.Visible = True
        'BtnSOAcceptPayment.Visible = True
        'BtnSOOtherCharges.Visible = True

        'BtnSOReturn.Visible = True
        'BtnSOCancel.Visible = False
        'LbReturnReason.Visible = False
        'txtReturnReason.Visible = False

        ''LbItemScan.Visible = True
        'CtrlSalesPerson.CtrlTxtBox.Visible = True
        'BtnSearchItem.Visible = True

        'TabSalesOrder.TabPages.Add(tabSales)
        'TabSalesOrder.TabPages.Remove(tabReturn)

        'TabSalesOrder.TabPages.Remove(tabPayment)
        'TabSalesOrder.TabPages.Add(tabPayment)

        'IsAllowedSalesReturn = False

        'vDocType = vDocTypeCreation

        Try
            If dsMain.Tables(0).Rows.Count > 0 Then
                If MsgBox(getValueByKey("SO049"), MsgBoxStyle.YesNo, "SO049 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                    IsFormClosing = True
                    Dim frm As New frmNQuotationCancel
                    MDISpectrum.ShowChildForm(frm, True)
                End If
            Else
                Dim frm As New frmNQuotationCancel
                MDISpectrum.ShowChildForm(frm, True)
            End If

        Catch ex As Exception

        End Try

    End Sub
    Private Sub BtnSOReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles BtnSOReturn.Click
        If CheckInterTransactionAuth("SOReturn", dsScan.Tables(0), 0, 0, 0, 0) = True Then

            BtnSOSave.Visible = True
            BtnSOPrint.Visible = False
            rbnGrpCMPromotion.Visible = False
            BtnSOAcceptPayment.Visible = False
            BtnSOOtherCharges.Visible = False

            BtnSOReturn.Visible = False
            BtnSOCancel.Visible = True
            LbReturnReason.Visible = True
            txtReturnReason.Visible = True

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
    Private Sub grdSOItemRetuns_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)

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
        If Not (dsScan.Tables("ItemScanDetails") Is Nothing) AndAlso dsScan.Tables("ItemScanDetails").Rows.Count > 0 Then
            Dim i As Integer = 1
            Dim j As Integer = 0
            For Each dr As DataRow In dsScan.Tables("ItemScanDetails").Select(" IsStatus In " & vFilterValue & "", "", DataViewRowState.CurrentRows)
                dr("PickupQty") = dr("Quantity") - IIf(dr("DeliveredQty") Is DBNull.Value, 0, dr("DeliveredQty"))
                dr("IsStatus") = "Updated"

                TotalSalesQty = dr("PickupQty") + dr("DeliveredQty")
                NetArticleRate = Math.Round(dr("NetAmount") / dr("Quantity"), 3)
                dr("MinPayAmt") = Math.Round(((dr("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate), 3)
                dr("TotalPickUpAmt") = (TotalSalesQty * NetArticleRate)
                'j = grdSOItems.Cols("PickupQty").Index
                'grdSOItems_AfterEdit(grdSOItems, New C1.Win.C1FlexGrid.RowColEventArgs(i, j))
                'i = i + 1
            Next
            CalculateSalesOrderSummory(dsScan)
            BtnSaveSalesOrder_Click(Me, New System.EventArgs)
            Exit Sub

            Dim vTotalDeliveredAmount As Double = 0.0
            Dim vReturnAmount As Double

            If MsgBox(getValueByKey("SO078"), MsgBoxStyle.YesNo, "SO078 - " & getValueByKey("CLAE04")) = MsgBoxResult.No Then
                Exit Sub
            End If

            Try

                Dim TotalAmt As Object = dsScan.Tables("ItemScanDetails").Compute("SUM(NetAmount)", "DeliveredQty>0 and IsStatus In " & vFilterValue & "")
                vTotalDeliveredAmount = IIf(TotalAmt Is DBNull.Value, 0, TotalAmt)
                vReturnAmount = vTotalDeliveredAmount - CDbl(CtrlCashSummary1.lbltxt5)

                If Not (vReturnAmount < 0) Then
                    'ShowMessage("Need to pay remaining amount is " & Math.Round(vReturnAmount, 2) & " because Delivered Amount is " & vTotalDeliveredAmount, "Payment Information")
                    ShowMessage(String.Format(getValueByKey("SO081"), Math.Round(vReturnAmount, 2), vTotalDeliveredAmount), "SO081 - " & getValueByKey("CLAE04"))
                    Dim objPayment As New frmNAcceptPayment()
                    objPayment.TotalBillAmount = Math.Round(vReturnAmount, 0)

                    objPayment.AcceptEditBillDataSet = dsPayment
                    objPayment.PaymentType = clsAcceptPayment.PaymentType.Accept
                    objPayment.ShowDialog()

                    _dsPayment = New DataSet
                    _dsPayment = objPayment.ReciptTotalAmount()
                    objPayment.Close()
                End If



                'If dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                '    vReceivedAmt = Format(CDbl(dsPayment.Tables("MSTRecieptType").Compute("Sum(Amount)", "")), "0.00")
                'Else
                '    Exit Sub
                'End If
                '--
                'dsMain.Tables("SalesOrderHDR").RejectChanges()
                'dsMain.Tables("SalesOrderHDRAudit").RejectChanges()
                'dsMain.Tables("SalesOrderDTL").RejectChanges()
                'dsMain.Tables("SalesOrderDTLAudit").RejectChanges()
                'dsMain.Tables("SalesInvoice").RejectChanges()
                'dsMain.Tables("SalesOrderTaxDtls").RejectChanges()
                'dsMain.Tables("SalesOrderOtherCharges").RejectChanges()
                'dsMain.Tables("OrderHdr").RejectChanges()
                'dsMain.Tables("OrderDtl").RejectChanges()

                If Not (PrepareHdrDataforSave(dsMain) = True) Then
                    Exit Sub
                End If
                If Not (PrepareDtlDataforSave(dsMain, True) = True) Then
                    Exit Sub
                End If

                If Not (PrepareInvcDataforSave(dsMain) = True) Then
                    Exit Sub
                End If
                If Not (PrepareOtherTaxDataforSave(dsMain) = True) Then
                    Exit Sub
                End If

                If CInt(lblPickupQty.Text) > 0 Then
                    IsOutboundCreated = True
                    dsMain.Tables("OrderHdr").Rows.Clear()
                    dsMain.Tables("OrderDtl").Rows.Clear()
                    If Not (PrepareOrderHdrDataforSave(dsMain) = True) Then
                        Exit Sub
                    End If
                    If Not (PrepareOrderDtlDataforSave(dsMain) = True) Then
                        Exit Sub
                    End If
                End If

                If CDbl(vTotalDeliveredAmount) = CDbl(CDbl(CtrlCashSummary1.lbltxt5) + vReturnAmount) Then
                    Dim findKey(3) As Object
                    Dim drSODtl As DataRow

                    'Start=================Apply Customer Loyalty Program======================
                    If CtrlCustDtls1.lblCustTypeValue.Text = "CLP" AndAlso CInt(lblOrderQty.Text) >= CInt(lblPickupQty.Text) + CInt(lbldeliveredqty.Text) Then
                        CalCulateCLP(vCardType, dsScan.Tables("ItemScanDetails"), "(PickUpQty>0 Or DeliveredQty>0) and IsCLP='True'")
                        Dim objPoints As Object = dsScan.Tables("ItemScanDetails").Compute("Sum(CLPPoints)", "(PickUpQty>0 Or DeliveredQty>0) and IsCLP='True'")
                        '---Changed by rama as it's through error
                        If objPoints Is DBNull.Value Then
                            TotalPoints = 0
                        Else
                            TotalPoints = CDbl(objPoints)
                        End If
                        'TotalPoints = dsScan.Tables("ItemScanDetails").Compute("Sum(CLPPoints)", "(PickUpQty>0 Or DeliveredQty>0) and IsCLP='True'")
                        '---
                        dsMain.Tables("QuotationHdr").Rows(0).Item("CLPPoints") = TotalPoints
                        dsMain.Tables("QuotationHdr").Rows(0).Item("CLPDiscount") = 0

                        For Each drClpData As DataRow In dsScan.Tables(0).Rows
                            findKey(0) = vSiteCode
                            findKey(1) = vfinancialYear
                            findKey(2) = CtrlSalesInfo.CtrlTxtOrderNo.Text
                            findKey(3) = drClpData("EAN")
                            drSODtl = dsMain.Tables("QuotationDtl").Rows.Find(findKey)

                            If Not (drSODtl Is Nothing) Then
                                drSODtl("IsCLPApplicable") = drClpData("IsCLP")
                                drSODtl("CLPPoints") = CDbl(drClpData("CLPPoints"))
                                drSODtl("CLPDiscount") = CDbl(drClpData("CLPDiscount"))
                            End If
                        Next

                    Else
                        dsMain.Tables("QuotationHdr").Rows(0).Item("SOStatus") = "Closed"
                        dsMain.Tables("QuotationHdr").Rows(0).Item("UPDATEDON") = vCurrentDate
                        dsMain.Tables("QuotationHdr").Rows(0).Item("UPDATEDBY") = clsAdmin.UserCode
                        dsMain.Tables("QuotationHdr").Rows(0).Item("UPDATEDAT") = clsAdmin.SiteCode
                    End If
                    'added by rama for unreleased Reserved qty
                    For Each drScan As DataRow In dsScan.Tables("ItemScanDetails").Rows
                        findKey(0) = vSiteCode
                        findKey(1) = vfinancialYear
                        findKey(2) = CtrlSalesInfo.CtrlTxtOrderNo.Text
                        findKey(3) = drScan("EAN").ToString
                        drSODtl = dsMain.Tables("QuotationDtl").Rows.Find(findKey)
                        If Not (drSODtl Is Nothing) Then
                            drSODtl("Reserved_Qty") = IIf(drSODtl("ReservedQty") Is DBNull.Value, 0, drSODtl("ReservedQty")) * -1
                            drSODtl("ReservedQty") = 0
                        End If
                    Next

                    If Not (PrepareInvcDataforSave(dsMain) = True) Then
                        Exit Sub
                    End If

                    If objSO.PrepareSaveData(vSalesInvcNo, clsAdmin.DayOpenDate, clsAdmin.CLPProgram, CtrlCustDtls1.lblCustNo.Text, dsMain, False, IsNextInvoiceNo, vSiteCode, CtrlSalesInfo.CtrlTxtOrderNo.Text, clsDefaultConfiguration.StockStorageLocation, clsAdmin.CVProgram, "SalesOrder", vfinancialYear, clsAdmin.UserCode, clsAdmin.CurrentDate, IsOutboundCreated) = True Then
                        ' PrintCreditVoucher(drSiteInfo)
                        If Not dsPayment.Tables("MSTRecieptType") Is Nothing AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                            Dim totalPay As Decimal
                            For Each dr As DataRow In dsPayment.Tables("MSTRecieptType").Select("RecieptTypeCode='CreditVouc(I)'", "", DataViewRowState.CurrentRows)
                                totalPay = IIf(dr("Amount") > 0, dr("Amount"), dr("Amount") * -1)

                                'PrintCreditVoucher(drSiteInfo, totalPay)
                                clsVoucher.PrintGiftVoucherAndCreditNote("SalesOrder", clsAdmin.SiteCode, "CreditNote", String.Empty, totalPay, String.Empty, clsAdmin.UserName, CtrlSalesInfo.CtrlTxtOrderNo.Text, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                            Next
                        End If

                        ShowMessage(getValueByKey("SO039"), "SO039 - " & getValueByKey("CLAE04"))

                        If CtrlCustDtls1.lblCustTypeValue.Text = "CLP" AndAlso CInt(lblOrderQty.Text) >= CInt(lblPickupQty.Text) + CInt(lbldeliveredqty.Text) Then

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
                        End If

                        ResetSalesOrder()
                        CtrlSalesInfo.CtrlTxtOrderNo.Text = ""
                    Else
                        ShowMessage(getValueByKey("SO040"), "SO040 - " & getValueByKey("CLAE04"))
                        'ShowMessage("Sales Order does not Updated", "Sales Order")
                    End If

                Else
                    ShowMessage(getValueByKey("SO040"), "SO040 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Sales Order does not Updated", "Sales Order")
                    Exit Sub
                End If

            Catch ex As Exception
                ShowMessage(ex.Message, getValueByKey("CLAE05"))
                LogException(ex)
            End Try

        End If
    End Sub
    Private Sub CtrlBtnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlBtnReturn.Click
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
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "DEL".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "ReservedQty".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "TotalTaxAmt".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "NetAmount".ToUpper() Then
                    'AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "PickUpQty".ToUpper() _
                    'AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "DeliveredQty".ToUpper() _
                    'AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "ExpDelDate".ToUpper() _
                    HideColumns(grdSOItems, False, grdSOItems.Cols(colno).Name)
                End If
            Next
            grdSOItems.Cols("Del").Caption = ""
            grdSOItems.Cols("Del").Width = 20
            grdSOItems.Cols("Del").ComboList = "..."

            If (clsDefaultConfiguration.BarcodeDisplayAllowed) Then
                grdSOItems.Cols("EAN").Caption = getValueByKey("frmnquotationupdate.grdsoitems.ean")
                grdSOItems.Cols("EAN").Width = 90
                grdSOItems.Cols("EAN").AllowEditing = False
                grdSOItems.Cols("EAN").Visible = True
            Else
                grdSOItems.Cols("EAN").Visible = False
            End If

            grdSOItems.Cols("ArticleCode").Caption = getValueByKey("frmnquotationupdate.grdsoitems.articlecode")
            grdSOItems.Cols("ArticleCode").Width = 90
            grdSOItems.Cols("ArticleCode").AllowEditing = False
            grdSOItems.Cols("Discription").Caption = getValueByKey("frmNQuotationUpdate.grdsoitems.discription")
            grdSOItems.Cols("Discription").Width = 150
            grdSOItems.Cols("Discription").AllowEditing = False
            grdSOItems.Cols("SellingPrice").Caption = getValueByKey("frmNQuotationUpdate.grdsoitems.sellingprice")
            grdSOItems.Cols("SellingPrice").Width = 60
            grdSOItems.Cols("SellingPrice").AllowEditing = False
            grdSOItems.Cols("Quantity").Caption = getValueByKey("frmNQuotationUpdate.grdsoitems.quantity")
            grdSOItems.Cols("Quantity").Width = 45
            grdSOItems.Cols("Quantity").Format = "0"
            'grdSOItems.Cols("Quantity").EditMask = "999999999"
            grdSOItems.Cols("PickUpQty").Caption = getValueByKey("frmNQuotationUpdate.grdsoitems.pickupqty")
            grdSOItems.Cols("PickUpQty").Width = 45
            'grdSOItems.Cols("PickUpQty").EditMask = "999999999"
            grdSOItems.Cols("PickUpQty").Format = "0"
            grdSOItems.Cols("DeliveredQty").Caption = getValueByKey("frmNQuotationUpdate.grdsoitems.deliveredqty")
            grdSOItems.Cols("DeliveredQty").Width = 45
            grdSOItems.Cols("DeliveredQty").Format = "0"
            grdSOItems.Cols("DeliveredQty").AllowEditing = False
            grdSOItems.Cols("TOTALDISCPERCENTAGE").Caption = getValueByKey("frmNQuotationUpdate.grdsoitems.totaldiscpercentage")
            grdSOItems.Cols("TOTALDISCPERCENTAGE").Width = 45
            grdSOItems.Cols("TOTALDISCPERCENTAGE").Format = "0.00"
            grdSOItems.Cols("TOTALDISCPERCENTAGE").AllowEditing = False
            grdSOItems.Cols("NetAmount").Caption = getValueByKey("frmNQuotationUpdate.grdsoitems.netamount")
            grdSOItems.Cols("NetAmount").Width = 70
            grdSOItems.Cols("NetAmount").AllowEditing = False
            grdSOItems.Cols("ExpDelDate").Caption = getValueByKey("frmNQuotationUpdate.grdsoitems.expdeldate")
            grdSOItems.Cols("ExpDelDate").Width = 95
            grdSOItems.Cols("Stock").Caption = getValueByKey("frmNQuotationUpdate.grdsoitems.stock")
            grdSOItems.Cols("Stock").Width = 45
            grdSOItems.Cols("Stock").AllowEditing = False
            grdSOItems.Cols("IsCLP").Caption = getValueByKey("frmNQuotationUpdate.grdsoitems.isclp")
            grdSOItems.Cols("IsCLP").Width = 45
            grdSOItems.Cols("IsCLP").DataType = Type.GetType("System.Boolean")
            grdSOItems.Cols("ReservedQty").Caption = getValueByKey("frmNQuotationUpdate.grdsoitems.reservedqty")
            grdSOItems.Cols("ReservedQty").Width = 45
            grdSOItems.Cols("ReservedQty").Format = "0"
            grdSOItems.Cols("ReservedQty").DataType = Type.GetType("System.Boolean")
            grdSOItems.Cols("ReservedQty").Visible = False
            grdSOItems.Cols("IsStatus").Visible = False
            grdSOItems.Cols("TotalTaxAmt").Caption = getValueByKey("frmNQuotationUpdate.grdsoitems.excltaxamt")
            grdSOItems.Cols("TotalTaxAmt").Format = "0.00"
            grdSOItems.Cols("TotalTaxAmt").Width = 45
            grdSOItems.Cols("TotalTaxAmt").AllowEditing = False
            'If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            '    For i = 0 To grdSOItems.Cols.Count - 1
            '        grdSOItems.Cols(i).Caption = grdSOItems.Cols(i).Caption.ToUpper
            '    Next
            'End If
            'grdSOItems.AutoSizeCols()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub GridDeliverdSetting()
        Try
            'grdSOItemRetuns.Cols("Del").Caption = ""
            'grdSOItemRetuns.Cols("Del").Width = 20
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
                    AndAlso grdSOItemRetuns.Cols(colno).Name.ToUpper() <> "NetAmount".ToUpper() Then
                    HideColumns(grdSOItemRetuns, False, grdSOItemRetuns.Cols(colno).Name)
                End If
            Next
            grdSOItemRetuns.Cols("ArticleCode").Caption = getValueByKey("frmNQuotationUpdate.grdsoitemretuns.articlecode")
            grdSOItemRetuns.Cols("ArticleCode").Width = 90
            grdSOItemRetuns.Cols("ArticleCode").AllowEditing = False
            grdSOItemRetuns.Cols("EAN").Caption = getValueByKey("frmNQuotationUpdate.grdsoitemretuns.ean")
            grdSOItemRetuns.Cols("EAN").Width = 90
            grdSOItemRetuns.Cols("EAN").Visible = False
            grdSOItemRetuns.Cols("Discription").Caption = getValueByKey("frmNQuotationUpdate.grdsoitemretuns.discription")
            grdSOItemRetuns.Cols("Discription").Width = 150
            grdSOItemRetuns.Cols("SellingPrice").Caption = getValueByKey("frmNQuotationUpdate.grdsoitemretuns.sellingprice")
            grdSOItemRetuns.Cols("SellingPrice").Width = 60
            grdSOItemRetuns.Cols("Quantity").Caption = getValueByKey("frmNQuotationUpdate.grdsoitemretuns.quantity")
            grdSOItemRetuns.Cols("Quantity").Width = 45
            grdSOItemRetuns.Cols("Quantity").Format = "0"
            grdSOItemRetuns.Cols("Quantity").EditMask = "999999999"
            grdSOItemRetuns.Cols("PickUpQty").Caption = getValueByKey("frmNQuotationUpdate.grdsoitemretuns.pickupqty")
            grdSOItemRetuns.Cols("PickUpQty").Width = 45
            grdSOItemRetuns.Cols("PickUpQty").Format = "0"
            grdSOItemRetuns.Cols("PickUpQty").EditMask = "999999999"
            grdSOItemRetuns.Cols("DeliveredQty").Caption = getValueByKey("frmNQuotationUpdate.grdsoitemretuns.deliveredqty")
            grdSOItemRetuns.Cols("DeliveredQty").Width = 45
            grdSOItemRetuns.Cols("DeliveredQty").Format = "0"
            grdSOItemRetuns.Cols("ReturnReasonCode").Caption = getValueByKey("frmNQuotationUpdate.grdsoitemretuns.returnreasoncode")
            grdSOItemRetuns.Cols("ReturnReasonCode").Width = 45
            grdSOItemRetuns.Cols("Discount").Caption = getValueByKey("frmNQuotationUpdate.grdsoitemretuns.discount")
            grdSOItemRetuns.Cols("Discount").Width = 45
            grdSOItemRetuns.Cols("NetAmount").Caption = getValueByKey("frmNQuotationUpdate.grdsoitemretuns.netamount")
            grdSOItemRetuns.Cols("NetAmount").Width = 70
            grdSOItemRetuns.Cols("ExpDelDate").Caption = getValueByKey("frmNQuotationUpdate.grdsoitemretuns.expdeldate")
            grdSOItemRetuns.Cols("ExpDelDate").Width = 95
            grdSOItemRetuns.Cols("Stock").Caption = getValueByKey("frmNQuotationUpdate.grdsoitemretuns.stock")
            grdSOItemRetuns.Cols("Stock").Width = 45
            grdSOItemRetuns.Cols("IsCLP").Caption = getValueByKey("frmNQuotationUpdate.grdsoitemretuns.isclp")
            grdSOItemRetuns.Cols("IsCLP").Width = 45
            grdSOItemRetuns.Cols("ReservedQty").Caption = getValueByKey("frmNQuotationUpdate.grdsoitemretuns.reservedqty")
            grdSOItemRetuns.Cols("ReservedQty").Width = 45
            grdSOItemRetuns.Cols("ReservedQty").Format = "0"
            grdSOItems.Cols("ReservedQty").DataType = Type.GetType("System.Boolean")
            grdSOItems.Cols("IsCLP").DataType = Type.GetType("System.Boolean")
            grdSOItemRetuns.Cols("EAN").AllowEditing = False
            grdSOItemRetuns.Cols("Discription").AllowEditing = False
            grdSOItemRetuns.Cols("SellingPrice").AllowEditing = False
            grdSOItemRetuns.Cols("DeliveredQty").AllowEditing = False
            grdSOItemRetuns.Cols("Discount").AllowEditing = False
            grdSOItemRetuns.Cols("NetAmount").AllowEditing = False
            grdSOItemRetuns.Cols("Stock").AllowEditing = False
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

            grdSOInvoice.Cols("SalesNO").Caption = getValueByKey("frmNQuotationUpdate.grdsoinvoice.salesno")
            grdSOInvoice.Cols("SalesNO").Width = 90
            grdSOInvoice.Cols("InvoiceNO").Caption = getValueByKey("frmNQuotationUpdate.grdsoinvoice.invoiceno")
            grdSOInvoice.Cols("InvoiceNO").Width = 90
            grdSOInvoice.Cols("DocumentType").Caption = getValueByKey("frmNQuotationUpdate.grdsoinvoice.documenttype")
            grdSOInvoice.Cols("DocumentType").Width = 60
            grdSOInvoice.Cols("TerminalID").Caption = getValueByKey("frmNQuotationUpdate.grdsoinvoice.terminalid")
            grdSOInvoice.Cols("TerminalID").Width = 45
            grdSOInvoice.Cols("UserName").Caption = getValueByKey("frmNQuotationUpdate.grdsoinvoice.username")
            grdSOInvoice.Cols("UserName").Width = 90
            grdSOInvoice.Cols("InvoiceDate").Caption = getValueByKey("frmNQuotationUpdate.grdsoinvoice.invoicedate")
            grdSOInvoice.Cols("InvoiceDate").Width = 70
            grdSOInvoice.Cols("TenderType").Caption = getValueByKey("frmNQuotationUpdate.grdsoinvoice.tendertype")
            grdSOInvoice.Cols("TenderType").Width = 45
            grdSOInvoice.Cols("InvoiceAmt").Caption = getValueByKey("frmNQuotationUpdate.grdsoinvoice.invoiceamt")
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
                    Dim frm As New frmNQuotationCreation
                    MDISpectrum.ShowChildForm(frm, True)
                End If
            Else
                Dim frm As New frmNQuotationCreation
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
                    Dim frm As New frmNQuotationUpdate
                    MDISpectrum.ShowChildForm(frm, True)
                End If
            Else
                Dim frm As New frmNQuotationUpdate
                MDISpectrum.ShowChildForm(frm, True)
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub grdSOItemRetuns_AfterEdit1(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdSOItemRetuns.AfterEdit
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
        Dim TotalPickNDelAmt As Double = 0.0
        If Not dsScan.Tables("ItemScanDetails").Compute("SUM(totalpickupamt)", "IsStatus<> 'Deleted'") Is DBNull.Value Then
            TotalPickNDelAmt = IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(totalpickupamt)", "IsStatus<> 'Deleted'") Is DBNull.Value, 0, CDbl(dsScan.Tables("ItemScanDetails").Compute("SUM(totalpickupamt)", "IsStatus<> 'Deleted'")))
        Else
            TotalPickNDelAmt = 0
        End If

        If TotalPickNDelAmt > (vAdvanceAmount) Then  ' if total pick + del amount is greater then advance paid amount then must pay as pickup amt else no need to pay
            PickAmtToPay = MyRound((TotalPickNDelAmt - vAdvanceAmount), clsDefaultConfiguration.BillRoundOffAt)
        Else
            PickAmtToPay = strZero
        End If
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
                frm.IsNumeric = True
                frm.AcceptButton = frm.cmdOk
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

    Protected Sub grdScanItem_StartEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
        _iArticleQtyBeforeChange = grdSOItems.Rows(e.Row)("Quantity")
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
    Private Function Themechange()
        rbnTabSO.Text = rbnTabSO.Text.ToUpper
        rbnTabSO.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        CtrlRbn1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black
        CtrlSalesPerson.AlignChange = "Sales Order Old"
        CtrlCashSummary1.AlignChangeForCashSummary = "Sales Order Old"

        CtrlRbn1.DbtnPay.LargeImage = Global.Spectrum.My.Resources.Resources.ConvertToSalesOrder
        CtrlRbn1.DbtnPayCash.LargeImage = Global.Spectrum.My.Resources.Resources.SaveSO1
        CtrlRbn1.DbtnPayCard.LargeImage = Global.Spectrum.My.Resources.Resources.Card_Normal
        CtrlRbn1.DbtnpayCheque.LargeImage = Global.Spectrum.My.Resources.PayByCheque
        Me.rbgrpSO.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        ' Me.rbgrpSO.Image = Global.Spectrum.My.Resources.defaultPromo_Normal
        Me.rbgrpSO.ForeColorInner = Color.FromArgb(37, 37, 37)
        Me.rbnGrpCMPromotion.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        Me.rbnGrpCMPromotion.Image = Global.Spectrum.My.Resources.defaultPromo_Normal
        Me.rbnGrpCMPromotion.ForeColorInner = Color.FromArgb(37, 37, 37)
        CtrlRbn1.DgrpPayments.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        Me.rbgrpSaveNprint.Font = New Font("Droid Sans Bold", 8, FontStyle.Bold)
        '  Me.rbnGrpPayments.Image = Global.Spectrum.My.Resources.payment_Normal
        Me.rbgrpSaveNprint.ForeColorInner = Color.FromArgb(37, 37, 37)
        cmdSONew.LargeImage = Global.Spectrum.My.Resources.NewSO1


        CmdSOEdit.LargeImage = Global.Spectrum.My.Resources.EditSO1

        CmdSOClose.LargeImage = Global.Spectrum.My.Resources.CancelSO1
        cmdSave.LargeImage = Global.Spectrum.My.Resources.SaveSO1
        cmdPrint.LargeImage = Global.Spectrum.My.Resources.PrintSO1

        rbnGrpCMPromotion.Text = rbnGrpCMPromotion.Text.ToUpper
        rbgrpSO.Text = rbgrpSO.Text.ToUpper
        rbgrpSO.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbnGrpCMPromotion.ForeColorOuter = Color.FromArgb(0, 107, 163)
        cmdSONew.Text = cmdSONew.Text.ToUpper
        CmdSOEdit.Text = CmdSOEdit.Text.ToUpper
        CmdSOClose.Text = CmdSOClose.Text.ToUpper
        BtnSOSave.Text = BtnSOSave.Text.ToUpper
        cmdClearSelectedPromo.Text = cmdClearSelectedPromo.Text.ToUpper
        cmdClrAllPromo.Text = cmdClrAllPromo.Text.ToUpper
        cmdApplySelectedPromo.Text = cmdApplySelectedPromo.Text.ToUpper
        cmdDefaultPromo.Text = cmdDefaultPromo.Text.ToUpper
        '.Text = rbnbtnCST.Text.ToUpper
        CtrlRbn1.DbtnPayCash.Text = CtrlRbn1.DbtnPayCash.Text.ToUpper


        grdSOItems.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        grdSOItems.Styles.Highlight.BackColor = Color.FromArgb(177, 227, 253)
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
        grdSOInvoice.Styles.Highlight.BackColor = Color.FromArgb(177, 227, 253)
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

        '  CtrlSalesInfo.Size = New Size(260, 150)
        ' TabSalesOrder.Size = New Size(1122, 354)
        ' C1Sizer2.Size = New Size(1125, 72)
        CtrlBtnReturn.VisualStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnReturn.Image = My.Resources.ReturnsNew
        CtrlBtnReturn.ImageAlign = ContentAlignment.MiddleCenter
        CtrlBtnReturn.Font = New Font("Neo Sans", 10, FontStyle.Bold)

        CtrlBtnStockCheck.VisualStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnStockCheck.Image = My.Resources.StockCheck
        CtrlBtnStockCheck.ImageAlign = ContentAlignment.MiddleCenter
        CtrlBtnStockCheck.Font = New Font("Neo Sans", 10, FontStyle.Bold)

        CtrlBtnAddExtraCost.VisualStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnAddExtraCost.Image = My.Resources.AdditionalCost
        CtrlBtnAddExtraCost.ImageAlign = ContentAlignment.MiddleCenter
        CtrlBtnAddExtraCost.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        'CtrlProductImage.Size = New Size(225, 135)
        ' CtrlCashSummary1.Location = New Point(1128, 415)
        ' CtrlProductImage.Location = New Point(1128, 281)
        ' CtrlCashSummary1.Size = New Size(225, 190)
        TabPageInvoiceDetails.TabBackColorSelected = Color.FromArgb(212, 212, 212)
        TabPageItemDetails.TabBackColorSelected = Color.FromArgb(212, 212, 212)
        TabPageInvoiceDetails.Text = TabPageInvoiceDetails.Text.ToUpper
        TabPageItemDetails.Text = TabPageItemDetails.Text.ToUpper
        CtrlRbn1.DbtnPay.Text = CtrlRbn1.DbtnPay.Text.ToUpper
        ' Me.C1Sizer3.Controls.Remove(Me.CtrlSalesPerson)
        'Me.Controls.Add(CtrlSalesPerson)
        '  Me.Controls.SetChildIndex(Me.CtrlSalesPerson, 0)
        ' CtrlSalesPerson.Size = New Size(675, 22)
        'CtrlSalesPerson.Location = New Point(429, 282)
        ' C1Sizer3.Hide()
        ' Me.TabPageItemDetails.Controls.Add(Me.grdSOItems)
        ' Me.grdSOItems.Size = New System.Drawing.Size(1118, 327)
        ' Me.grdSOItems.Location = New System.Drawing.Point(3, 3)

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