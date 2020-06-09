Imports System.Text
Imports System.IO
Imports SpectrumBL
Imports System.Resources
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports SpectrumCommon
Imports System.ComponentModel
Imports SpectrumPrint

Public Class frmNSalesOrderCreation
    Inherits CtrlRbnBaseForm
    Dim clsVoucher As New SpectrumPrint.clsPrintVoucher
    'Sales order tax details 
    Dim dtSalesOrderTaxDetails As DataTable
    Dim IsIGSTApplicableForOutsideCustomer As Boolean = False
    Dim _strCustNo As String


#Region "Declare Varables"
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
    Dim IsFinalReceipt As Boolean = False
    Dim vCurrentDate As Date
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
    Dim IsRoundOffMsg As Boolean = False
    Dim IsRoundOfflabel As Boolean = False
    Dim dtUserAuth As DataTable
    Dim dtCashMemoDtls As DataTable
    Dim Obj As New frmNHowMuchtoPay
    Dim objPaymentByCash As New frmNAcceptPaymentByCash("SO")

    Dim GridWidth As Integer = 0
    Dim GridHeight As Integer = 0
    Dim vRowIndex As Integer = 1

    Dim dsMain As New DataSet
    Dim dsMainCLP As New DataSet

    Dim dtItemSch As New DataTable
    Dim dtPrintingDetails As New DataTable
    Dim dtOtherCharges As New DataTable
    Dim dtSalesPerson As New DataTable
    Dim dtAddressType As New DataTable
    Dim dt As New DataTable
    Dim dtTaxCalc As DataTable

    Dim dvCurrentQty As DataView
    Dim dvAddressType As DataView
    Dim dvEditDeleteItems As DataView
    Dim dvDeleteTaxOnItem As DataView

    Dim drHomeAdds As DataRow
    Dim drDelvAdds As DataRow

    Dim drAddItemExists() As DataRow
    Dim drItemSch As DataRow
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

    Dim DtSoBulkComboHdr As New DataTable
    Dim DtSoBulkComboDtl As New DataTable
    Dim IsSTRGenerate As Boolean = False
    Dim IsSOSaved As Boolean = False
    Dim vSalesOrderExpectedDeliveryDate As DateTime = DateTime.Now
    Dim SalesPersonName As String = ""
    Dim strSOStatus As String = ""
    Dim IsNewComboAdd As Boolean = False
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

#End Region

#Region "Load Sales Order Application"

    Private Sub frmSalesOrderCreation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '---- Set Tab Index START
            'added by khusrao adil on 12-10-2017 for product specific change -Wild search functionality
            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType = False AndAlso clsDefaultConfiguration.EnablewildSearch = True Then
                ''Work Wild Seach funcatonality 
                CtrlSalesPersons.AndroidSearchTextBox.IsListBind = True
            Else
                ''Work Old Seach funcatonality 
                CtrlSalesPersons.AndroidSearchTextBox.IsListBind = False
            End If
            SetFormTabStop(Me, tabStopValue:=False)

            Dim ctrTablIndex As New Dictionary(Of Object, Int16)

            ctrTablIndex.Add(CtrlSalesInfo1, 0)
            ctrTablIndex.Add(CtrlSalesInfo1.CtrlDtExpDelDate, 0)
            ctrTablIndex.Add(CtrlSalesInfo1.CtrlTxtCustOrdRef, 1)
            ctrTablIndex.Add(CtrlSalesInfo1.CtrlTxtRemarks, 2)
            ctrTablIndex.Add(CtrlSalesInfo1.CtrlTxtInvoice, 3)

            ' ctrTablIndex.Add(Me.tabSalesOrder, 1)
            'ctrTablIndex.Add(Me.TabPageOrderedItems, 1)
            'ctrTablIndex.Add(Me.c1SizerGrid, 1)
            ctrTablIndex.Add(Me.CtrlSalesPersons, 1)
            ctrTablIndex.Add(Me.CtrlSalesPersons.CtrlSalesPersons, 0)
            ctrTablIndex.Add(Me.CtrlSalesPersons.CtrlTxtBox, 1)
            ctrTablIndex.Add(Me.CtrlSalesPersons.CtrlCmdSearch, 2)

            ctrTablIndex.Add(Me.grdScanItem, 2)

            ctrTablIndex.Add(Me.CtrlCustSearch1, 3)
            ctrTablIndex.Add(Me.CtrlCustSearch1.rbOtherCust, 0)
            ctrTablIndex.Add(Me.CtrlCustSearch1.rbCLPMember, 1)
            ctrTablIndex.Add(Me.CtrlCustSearch1.CtrlBtn1, 2)
            ctrTablIndex.Add(Me.CtrlCustSearch1.CtrlBtnNew, 3)
            ctrTablIndex.Add(Me.CtrlCustSearch1.CtrlTxtCustNo, 4)
            ctrTablIndex.Add(Me.CtrlCustSearch1.CtrlTxtSwapeCard, 5)

            ctrTablIndex.Add(Me.C1Sizer2, 4)
            ctrTablIndex.Add(Me.CtrlBtnStockCheck, 4)
            ctrTablIndex.Add(Me.CtrlbtnSOOtherCharges, 5)
            ctrTablIndex.Add(Me.CtrlBtnSearchCLP, 6)
            ctrTablIndex.Add(Me.BtnSelectDeliveryLoc, 7)

            SetFormTabIndex(ctrTablIndex:=ctrTablIndex)
            Me.grdScanItem.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.None
            'Me.tabSalesOrder.TabStop = False
            'Me.TabPageOrderedItems.TabStop = False
            'Me.TabPageDeliveryLocation.TabStop = False
            'c1SizerGrid.TabStop = False
            C1Sizer2.TabStop = False
            '---- Set Tab Index END 

            rbnGrpCST.Text = getValueByKey("CST003")
            rbnCST.Text = getValueByKey("CST004")
            BtnSelectDeliveryLoc.Image = My.Resources.Select_Delivery_Location
            BtnSelectDeliveryLoc.Text = getValueByKey("frmnsalesordercreation.BtnSelectDeliveryLoc")
            rbnCST.LargeImage = My.Resources.ApplyCSTTax

            AddHandler CtrlSalesPersons.CtrlTxtBox.PreviewKeyDown, AddressOf txtSearchItem_PreviewKeyDown
            AddHandler CtrlSalesPersons.CtrlTxtBox.Leave, AddressOf txtSearchItem_Leave
            AddHandler CtrlSalesPersons.CtrlCmdSearch.Click, AddressOf BtnSearchItem_Click
            AddHandler CtrlSalesInfo1.CtrlDtExpDelDate.Leave, AddressOf dtpExpDeliveryDate_Leave
            AddHandler CtrlSalesInfo1.CtrlDtExpDelDate.Calendar.DateValueSelected, AddressOf dtpExpDeliveryDate_Leave

            'added by khusrao adil on 12-10-2017 for product specific change -Wild search functionality
            'add for Wild Search
            AddHandler CtrlSalesPersons.CtrlTxtBox.TextChanged, AddressOf txtSearchItem_textchange
            '---AndroidSearchTextBox_Leave commeted by aJAY 
            'AddHandler CtrlSalesPersons.AndroidSearchTextBox.Leave, AddressOf AndroidSearchTextBox_Leave
            ' AddHandler CtrlSalesPersons.AndroidSearchTextBox.PreviewKeyDown, AddressOf AndroidSearchTextBox_PreviewKeyDown
            AddHandler CtrlSalesPersons.AndroidSearchTextBox.TextChanged, AddressOf AndroidSearchTextBox_Textchange

            AddHandler CtrlbtnSOOtherCharges.Click, AddressOf BtnAddOtherCharges_Click
            ' this uses othercharge form , 

            AddHandler rbbtnSelectPromo.Click, AddressOf rbbtnDefaultPromo_Click
            AddHandler rbbtnSave.Click, AddressOf BtnSaveSalesOrder_Click
            AddHandler rbBtnAddCombo.Click, AddressOf rbBtnAddCombo_Click

            AddHandler rbbtnPrint.Click, AddressOf BtnSOPrint_Click
            AddHandler CtrlBtnStockCheck.Click, AddressOf BtnStockCheck_Click

            AddHandler CtrlRbn1.DbtnPay.Click, AddressOf BtnAcceptPayment_Click
            AddHandler CtrlRbn1.DbtnPayCard.Click, AddressOf BtnPayCard_Click
            AddHandler CtrlRbn1.DbtnPayCash.Click, AddressOf BtnPayCash_Click
            AddHandler CtrlRbn1.DbtnpayCheque.Click, AddressOf BtnPayCheque_Click
            AddHandler grdScanItem.StartEdit, AddressOf grdScanItem_StartEdit


            CtrlCustDtls1.cboAddrType.DropMode = 1
            CtrlCustSearch1.rbCLPMember.Checked = True
            CtrlCustSearch1.rbOtherCust.Visible = clsDefaultConfiguration.IsOtherCustomerRequired

            AddHandler CtrlCustSearch1.CustomerChanged, AddressOf CustomerSearch_Completed
            Dim objdefault As New clsDefaultConfiguration("SalesOrder")
            objdefault.GetDefaultSettings()
            dsMain = objSO.GetSOTableStruct(vSiteCode, 0, , ISQuotationConversion, QuotationNumber)
            objSO.GetSODefaultConfig(vSiteCode)
            dtPrintingDetails = objSO.GetPrintingDetail
            rbbtnSONew.Tag = "NEW"
            If clsDefaultConfiguration.IsOtherChargesAllowed = False Then
                CtrlbtnSOOtherCharges.Visible = False
            Else
                CtrlbtnSOOtherCharges.Visible = True
            End If
            vIsPrintPreviewAllowed = clsDefaultConfiguration.SOPrintPreviewAllowed

            vDocType = objSO.SOCreation
            vCurrentDate = objComn.GetCurrentDate

            _dsScan = objSO.GetCollectionOfItems
            If Not (dsScan Is Nothing) Then
                _dsScan.Clear()
            End If

            CtrlCustSearch1.CustmType = "CLP"
            RefreshScanData(dsScan)

            CtrlSalesInfo1.CtrldtOrderDt.Value = vCurrentDate
            CtrlSalesInfo1.CtrldtOrderDt.ReadOnly = True

            consDeliveryDate = vCurrentDate.AddDays(clsDefaultConfiguration.ChkDeliveryDate)
            'dtpExpDeliveryDate.DisplayFormat.CustomFormat = vDateFormat
            'dtpExpDeliveryDate.EditFormat.CustomFormat = vDateFormat
            CtrlSalesInfo1.CtrlDtExpDelDate.Value = vCurrentDate.AddDays(clsDefaultConfiguration.ChkDeliveryDate)
            DeliverySiteCode = clsAdmin.SiteCode
            grdScanItem_Resize(sender, New System.EventArgs)

            If Not ISQuotationConversion Then
                BtnSONew_Click(sender, e)
            Else
                GetNewSalesOrderNumber()
            End If

            GridSetting()
            CtrlProductImage.ClearImage()
            CtrlProductImage.ShowArticleImage("xx")

            PSetDefaultCurrencyOfCashMemoSummary(CtrlCashSummary1)
            PrintSetProperty()
            dgDeliveryLocation.AutoGenerateColumns = False
            dgDeliveryLocation.DataSource = SoDeliveryInfo
            objCM.DecimalDigits = clsDefaultConfiguration.DecimalPlaces
            defaultconfig.GetDefaultSettings()

            If defaultconfig.IsCstTaxRequired Then
                rbnGrpCST.Visible = True
            Else
                rbnGrpCST.Visible = False
            End If

            If ISQuotationConversion Then
                SetQuotationArticles()
            End If

            'code added by Mahesh for add bulk combo 
            Call objSO.GetSOBulkComboTablestructure(DtSoBulkComboHdr, DtSoBulkComboDtl)
            Call EnableDiableTenderIcons()
            '---------PcRoundoff
            RibbonGroup2.Visible = clsDefaultConfiguration.PCRoundOff
            'CtrlSalesInfo1.CtrlTxtOrderNo.Enabled = False
            'CtrlSalesInfo1.CtrldtOrderDt.Enabled = False

            'code is added by irfan for wildsearch functionality=====================================
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
            '=========================================================================================
          
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
        SetCulture(Me, Me.Name, CtrlRbn1)
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
        End If

        CtrlSalesPersons.CtrlTxtBox.Select()
        Me.Select()
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
                PriceChange()
                'ElseIf e.KeyCode = Keys.F9 Then
                '    'cmdCash_Click(sender, New System.EventArgs)
                'ElseIf e.KeyCode = Keys.F8 Then
                '    'cmdCard_Click(sender, New System.EventArgs)
            ElseIf e.Alt And e.KeyCode = Keys.S Then
                BtnSaveSalesOrder_Click(Nothing, New System.EventArgs)
            ElseIf e.KeyCode = Keys.F8 And IsTenderCredit Then
                CreditSales(Nothing, New System.EventArgs)
            ElseIf e.KeyCode = Keys.F9 Then
                If clsDefaultConfiguration.IsNewCreditSale Then 'vipin 21.05.2018 
                    Dim objCreditSales As New frmNCreditSalesNew(False)
                    objCreditSales.ShowDialog()
                Else
                    Dim objCreditSales As New frmNCreditSales(False)
                    objCreditSales.ShowDialog()
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub

    Private Sub frmNSalesOrderCreation_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If grdScanItem.Rows.Count > 1 AndAlso Not IsFormClosing Then
                If MsgBox(getValueByKey("SO047"), MsgBoxStyle.YesNo, "SO047 - " & getValueByKey("CLAE04")) = MsgBoxResult.No Then
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub

    ''' <summary>
    ''' Get the Site default Settings And Set Default Config Object
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Dim FirstSearchCustomer As Boolean = True
    Private Sub CustomerSearch_Completed(ByVal Customer As CLPCustomerDTO)
        ' If Customer.AddressList IsNot Nothing AndAlso Customer.AddressList.Count > 0 Then
        ' If clsDefaultConfiguration.IsCstTaxRequired AndAlso objCustm.CheckIfCstApplicable(clsAdmin.SiteCode, Customer.AddressList.FirstOrDefault().State) Then

        'End If

        '    Else
        '        IsCSTApplicable = False
        ' ResetTax(False)
        ' End If


        'CODE ADDED BY IRFAN ON 23/10/2017 FOR IGST CAKEKRAFT
        If FirstSearchCustomer = True Then
            ResetTax(False, True)
            FirstSearchCustomer = False
        Else
            FirstSearchCustomer = False
            ResetTax(False, False)
        End If
    End Sub

    ''' <summary>
    ''' This Method is written for Resetting tax for CST application on Items
    ''' </summary>
    ''' <param name="considerCst"></param>
    ''' <remarks></remarks>
    Private Sub ResetTax(ByVal considerCst As Boolean, Optional IsCustomerSearchFirst As Boolean = False)
        'Tax Reset
        For drindex = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1
            If IsCSTApplicable Then
                dt = objCM.getTax(vSiteCode, String.Empty, "SO201", _dsScan.Tables("ItemScanDetails").Rows(drindex)("Quantity"), _dsScan.Tables("ItemScanDetails").Rows(drindex)("EAN"), clsDefaultConfiguration.CSTTaxCode, considerCst)
            Else
                dt = objCM.getTax(vSiteCode, _dsScan.Tables("ItemScanDetails").Rows(drindex)("ArticleCode"), "SO201", _dsScan.Tables("ItemScanDetails").Rows(drindex)("Quantity"), clsDefaultConfiguration.CSTTaxCode, considerCst)
            End If
            'Existing code given below
            'Dim dt = objCM.getTax(vSiteCode, String.Empty, "SO201", _dsScan.Tables("ItemScanDetails").Rows(drindex)("Quantity"), _dsScan.Tables("ItemScanDetails").Rows(drindex)("EAN"), clsDefaultConfiguration.CSTTaxCode, considerCst)
            'If dt Is Nothing OrElse Not dt.Rows.Count > 0 Then
            '    Exit Sub
            'End If
        Next

        'Code added by irfan on 17-10-2017 for salesorder
        _strCustNo = CtrlCustSearch1.CtrlTxtCustNo.Text.Trim()

        ''Dim state As DataTable = objCM.getSiteStateCode(vSiteCode)
        Dim IsIGSTApplicableForOutsideCustomer = objComn.checkIGSTAplicableForOutSideStateCustomer(clsAdmin.SiteCode, _strCustNo)
        Dim IGSTtaxCode As String = objComn.ReturnIGSTTaxID(dt)

        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
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
                    Dim dv As New DataView(dt, "TAXCODE='" & IGSTtaxCode & "'", "", DataViewRowState.CurrentRows)
                    dt = dv.ToTable
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
                If dt.Rows.Count > 0 Then
                    Dim index As Integer
                    For index = 0 To dt.Rows.Count - 1
                        If dt.Rows(index)("TAXCODE").ToString.Trim = IGSTtaxCode Then
                            dt.Rows.RemoveAt(index)
                            dt.AcceptChanges()
                            Exit For
                        End If
                    Next
                End If
            End If
        End If




        If _dsScan.Tables("ItemScanDetails").Columns.Contains("TaxPer") = True Then
            If IsIGSTApplicableForOutsideCustomer = True Then

                For i = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1

                    _dsScan.Tables("ItemScanDetails").Rows(i)("TaxPer") = IIf(dt.Rows.Count > 0, dt.Compute("sum(Value)", ""), 0)  'dtTaxCalc.Rows(0)("Value")
                Next
            Else
                ' _dsScan.Tables("ItemScanDetails").Columns.Add("TaxPer", System.Type.GetType("System.String"))
                For i = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1
                    _dsScan.Tables("ItemScanDetails").Rows(i)("TaxPer") = IIf(dt.Rows.Count > 0, dt.Compute("sum(Value)", ""), 0)    'dtTaxCalc.Compute("sum(Value)", "")
                Next
            End If
        Else
            _dsScan.Tables("ItemScanDetails").Columns.Add("TaxPer", System.Type.GetType("System.String"))
            If IsIGSTApplicableForOutsideCustomer = True Then

                For i = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1

                    _dsScan.Tables("ItemScanDetails").Rows(i)("TaxPer") = IIf(dt.Rows.Count > 0, dt.Compute("sum(Value)", ""), 0)            'dtTaxCalc.Rows(0)("Value")
                Next
            Else
                '    _dsScan.Tables("ItemScanDetails").Columns.Add("TaxPer", System.Type.GetType("System.String"))
                For i = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1
                    _dsScan.Tables("ItemScanDetails").Rows(i)("TaxPer") = IIf(dt.Rows.Count > 0, dt.Compute("sum(Value)", ""), 0)   ' dtTaxCalc.Compute("sum(Value)", "")
                Next
            End If
        End If


        '=========================================================================================================================
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
                '  Dim dt = objCM.getTax(vSiteCode, String.Empty, "SO201", _dsScan.Tables("ItemScanDetails").Rows(drindex)("Quantity"), _dsScan.Tables("ItemScanDetails").Rows(drindex)("EAN"), clsDefaultConfiguration.CSTTaxCode, considerCst)
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
                    drTax1("SaleOrderNumber") = CtrlSalesInfo1.CtrlTxtOrderNo.Value
                    drTax1("EAN") = _dsScan.Tables("ItemScanDetails").Rows(drindex)("EAN")
                    drTax1("TaxLineNo") = iRowTax + 1
                    drTax1("TaxLabel") = dt.Rows(iRowTax)("TaxCode")
                    drTax1("TaxValue") = Math.Round(dt.Rows(iRowTax)("TaxAmount"), 2)
                    drTax1("CustomerNo") = _strCustNo    'code added by irfan on 23/10/2017 for cakekraft IGST
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
    Private Sub grdScanItem_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdScanItem.Resize

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
        CtrlSalesInfo1.CtrlDtExpDelDate.ValidateText()
        If CtrlSalesInfo1.CtrlDtExpDelDate.Value Is DBNull.Value Then
            ShowMessage(getValueByKey("SO051"), "SO051 - " & getValueByKey("CLAE04"))
            CtrlSalesInfo1.CtrlDtExpDelDate.Value = consDeliveryDate
        Else
            If DateDiff(DateInterval.Day, vCurrentDate, CtrlSalesInfo1.CtrlDtExpDelDate.Value) < 0 Then
                ShowMessage(getValueByKey("SO010"), "SO010 - " & getValueByKey("CLAE04"))
                CtrlSalesInfo1.CtrlDtExpDelDate.Value = consDeliveryDate
            ElseIf (vCurrentDate > Convert.ToDateTime(CtrlSalesInfo1.CtrlDtExpDelDate.Value)) Then
                ShowMessage(getValueByKey("SO010"), "SO010 - " & getValueByKey("CLAE04"))
                CtrlSalesInfo1.CtrlDtExpDelDate.Value = consDeliveryDate

            Else
                vSalesOrderExpectedDeliveryDate = CtrlSalesInfo1.CtrlDtExpDelDate.Value
                For Each drGridRow As C1.Win.C1FlexGrid.Row In grdScanItem.Rows
                    If Not (drGridRow.Index = 0) Then
                        drGridRow.Item("ExpDelDate") = CtrlSalesInfo1.CtrlDtExpDelDate.Value
                    End If
                Next
            End If
        End If
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
        If CheckAuthorisation(clsAdmin.UserCode, "SOCreation") = False Then
            ShowMessage(getValueByKey("SO053"), "SO053 - " & getValueByKey("CLAE04"))
            Me.Dispose()
            Me.Close()
            Exit Sub
        End If

        InitializeComponent()
        Me.ClientSize = New System.Drawing.Size(gmdiclientwidth, gmdiclientheight)
        CtrlRbn1.pInitRbn()
        If Batchbarcode Is Nothing Then
            Batchbarcode = New List(Of SpectrumCommon.BtachbarcodeInfo)
        End If

        CtrlRbn1.DbtnF12.LargeImage = Global.Spectrum.My.Resources.Resources.price_change
        CtrlRbn1.DbtnF2.LargeImage = Global.Spectrum.My.Resources.Resources.change_qty

        CtrlSalesInfo1.CtrlTxtOrderNo.TextDetached = False
        CtrlSalesInfo1.CtrlTxtOrderNo.ReadOnly = True
        CtrlSalesInfo1.CtrldtOrderDt.ReadOnly = True
        If CtrlSalesInfo1.C1Sizer1.Grid.Columns.Count = 4 Then
            CtrlSalesInfo1.C1Sizer1.Grid.Columns.Remove(3)
        End If

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
            If CtrlCustSearch1.CtrlTxtCustNo.Text = "" Then
                ShowMessage("Customer Selection is Mandatory", getValueByKey("CLAE04"))
                Exit Sub
            End If

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
                    RemoveApplyPromotion(dsScan)
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
                CtrlProductImage.ShowArticleImage(vArticleImagesCode)
                GridSetting()

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
    Private Sub txtSearchItem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles txtSearchItem.Leave
        Try
            If CtrlSalesPersons.CtrlTxtBox.Text.ToString.Trim.Length > 0 Then
                If IsApplyPromotion = True Then
                    RemoveApplyPromotion(dsScan)
                End If
                'If clsDefaultConfiguration.IsSalesPersonApplicable = True AndAlso CtrlSalesPersons.CtrlSalesPersons.SelectedValue = Nothing Then
                '    ShowMessage(getValueByKey("SO014"), "SO014 - " & getValueByKey("CLAE04"))
                '    CtrlSalesPersons.CtrlTxtBox.Text = ""
                '    CtrlSalesPersons.CtrlSalesPersons.Select()
                '    Exit Sub
                'End If
                CtrlSalesPersons.CtrlTxtBox.Text = CtrlSalesPersons.CtrlTxtBox.Text.ToString().Split(" ")(0)
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
                ' CtrlSalesPersons.CtrlTxtBox.Text = ""
                'CtrlSalesPersons.CtrlTxtBox.Select()
                CtrlSalesPersons.AndroidSearchTextBox.IsItemSelected = False
                CtrlSalesPersons.AndroidSearchTextBox.Text = ""
                CtrlSalesPersons.CtrlTxtBox.Text = ""
                CtrlSalesPersons.CtrlTxtBox.Select()
                'CtrlSalesPersons.CtrlTxtBox.Select()
                CtrlSalesPersons.AndroidSearchTextBox.Select()
                grdScanItem.Select(grdScanItem.Rows.Count - 1, 1, True)
            End If
            GridSetting()
        Catch ex As Exception
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
                CtrlSalesPersons.CtrlTxtBox.Text = ""
                CtrlSalesPersons.CtrlSalesPersons.Select()
                Exit Sub
            End If

            If IsApplyPromotion = True Then
                RemoveApplyPromotion(dsScan)
            End If

            txtSearchItem_Leave(sender, New System.EventArgs)
        End If
        GridSetting()

        CtrlSalesPersons.CtrlTxtBox.Select()
    End Sub

    Private Sub grdScanItem_BeforeEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdScanItem.BeforeEdit
        Try
            Dim CurrentCell As Integer = e.Col
            Dim CurrentRow As Integer = e.Row
            'If (grdScanItem.Cols(CurrentCell).Name = "DeliverySiteCode" AndAlso IsDBNull(grdScanItem.Item(CurrentRow, "BatchBarcode")) = False) OrElse (grdScanItem.Cols(CurrentCell).Name = "PickUpQty" AndAlso IsDBNull(grdScanItem.Item(CurrentRow, "BatchBarcode"))) Then
            If (grdScanItem.Cols(CurrentCell).Name = "DeliverySiteCode" AndAlso clsDefaultConfiguration.IsBatchManagementReq) OrElse (grdScanItem.Cols(CurrentCell).Name = "PickUpQty" AndAlso clsDefaultConfiguration.IsBatchManagementReq) Then
                e.Cancel = True
            End If
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
            If grdScanItem.Row = -1 Then Exit Sub
            Dim CurrentCell As Integer = e.Col
            Dim CurrentRow As Integer = e.Row         ' grdScanItem.Row '-- e.Row

            Dim ComboSrNo = grdScanItem.Item(grdScanItem.Row, "RowIndex")
            Dim addCondtionRow As String = String.Empty
            If DtSoBulkComboHdr.Rows.Count > 0 Then
                Dim drHdr() = DtSoBulkComboHdr.Select("ComboSrNo=" & ComboSrNo)
                If drHdr.Count > 0 Then
                    addCondtionRow = " AND RowIndex =" & ComboSrNo
                End If
            End If
            If IsDBNull(grdScanItem.Item(CurrentRow, "BatchBarcode")) = False Then
                dvCurrentQty = New DataView(dsScan.Tables(0), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode = '" & grdScanItem.Item(CurrentRow, "BatchBarcode") & "'" & addCondtionRow, "", DataViewRowState.CurrentRows)
            Else
                dvCurrentQty = New DataView(dsScan.Tables(0), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode IS NULL" & addCondtionRow, "", DataViewRowState.CurrentRows)
            End If
            Dim CurrentQty As Double = dvCurrentQty.ToTable.Rows(0).Item("Quantity")

            'If grdScanItem.Cols(CurrentCell).Name = "Quantity" Then  commented to correct get netamount calculate after price change
            If grdScanItem.Cols(CurrentCell).Name = "Quantity" Or grdScanItem.Cols(CurrentCell).Name = "SellingPrice" Then
                Try
                    Dim vOrderQty As Double = IIf(grdScanItem.Item(CurrentRow, "Quantity") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "Quantity"))
                    Dim vPickupQty As Double = IIf(grdScanItem.Item(CurrentRow, "PickupQty") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "PickupQty"))
                    Dim vStock As Double = IIf(grdScanItem.Item(CurrentRow, "Stock") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "Stock"))

                    If Not (vOrderQty > 0) Then
                        ShowMessage(getValueByKey("SO005"), "SO005 - " & getValueByKey("CLAE04"))
                        'ShowMessage("Order Quantity cannot less than 1.", "Order Quantity Information")
                        grdScanItem.Item(CurrentRow, "Quantity") = 1
                    ElseIf Not (vOrderQty >= vPickupQty) Then
                        ShowMessage(getValueByKey("SO006"), "SO006 - " & getValueByKey("CLAE04"))
                        'ShowMessage("Order Quantity cannot less than PickUp Quantity.", "Order Quantity Information")
                        grdScanItem.Rows(e.Row)("Quantity") = _iArticleQtyBeforeChange
                        Exit Sub

                        'Rakesh-01.10.2013:7998-->Commented for Check article stock balance quantity
                        'ElseIf Not (vOrderQty <= vStock) AndAlso clsDefaultConfiguration.NegativeInventoryAllowed = False Then
                        '    ShowMessage(getValueByKey("SO007"), "SO007 - " & getValueByKey("CLAE04"))
                        '    'ShowMessage("Order Quantity cannot greater than Stock Quantity.", "Order Quantity Information")
                        '    grdScanItem.Item(CurrentRow, "Quantity") = CurrentQty
                        '    Exit Sub
                    End If

                    If IsApplyPromotion = True Then
                        RemoveApplyPromotion(_dsScan)
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

                    If IsDBNull(grdScanItem.Item(CurrentRow, "BatchBarcode")) = False Then
                        drAddItemExists = _dsScan.Tables("ItemScanDetails").Select("EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode = '" & grdScanItem.Item(CurrentRow, "BatchBarcode") & "'")
                    Else
                        'drAddItemExists = _dsScan.Tables("ItemScanDetails").Select("EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode IS NULL")
                        drAddItemExists = _dsScan.Tables("ItemScanDetails").Select("EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "'And RowIndex='" & ComboSrNo & "' And BatchBarcode IS NULL")
                    End If

                    If drAddItemExists.Length > 0 Then
                        IsEditItem = True
                        'drAddItemExists(0)("GrossAmt") = grdScanItem.Item(CurrentRow, "Quantity") * grdScanItem.Item(CurrentRow, "SellingPrice")
                        Dim obj As New clsSaleOrderCommon
                        obj.IsCSTApplicable = IsCSTApplicable
                        obj.CustomerNo = _strCustNo                                'code added by irfan for error in tax updation mantis issue
                        obj.RecalculateLine(drAddItemExists(0), CtrlSalesInfo1.CtrlTxtOrderNo.Value, dsMain, , False, _iArticleQtyBeforeChange)
                        TotalSalesQty = drAddItemExists(0)("PickupQty") + drAddItemExists(0)("DeliveredQty")
                        Dim ArticleRate As Double = Math.Round(drAddItemExists(0)("NetAmount") / drAddItemExists(0)("Quantity"), 3)
                        drAddItemExists(0)("MinPayAmt") = ((drAddItemExists(0)("Quantity") - TotalSalesQty) * ArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * ArticleRate)
                        'SetScanItemInSO(drAddItemExists(0))
                        'code commented by irfan on 07/11/2017 for cakeology IGST
                        'For Each dr As DataRow In dsMain.Tables("SalesOrderTaxDtls").Select("SiteCode='" & clsAdmin.SiteCode & "' And Finyear='" & clsAdmin.Financialyear & "' And SaleOrderNumber='" & CtrlSalesInfo1.CtrlTxtOrderNo.Value & "' And EAN='" & drAddItemExists(0)("EAN") & "'")
                        '    dr("TaxValue") = (dr("TaxValue") / _iArticleQtyBeforeChange) * drAddItemExists(0)("Quantity")
                        'Next
                        CalculateSalesOrderSummary(dsScan)
                        RefreshScanData(dsScan)
                        GridSetting()
                    End If
                    If IsDBNull(grdScanItem.Item(CurrentRow, "BatchBarcode")) = False Then
                        grdScanItem.Item(CurrentRow, "PickUpQty") = vOrderQty
                        grdScanItem_AfterEdit(grdScanItem, New C1.Win.C1FlexGrid.RowColEventArgs(CurrentRow, grdScanItem.Cols("PickUpQty").Index))
                    End If
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
                    If IsDBNull(grdScanItem.Item(CurrentRow, "BatchBarcode")) = False Then
                        dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode = '" & grdScanItem.Item(CurrentRow, "BatchBarcode") & "'" & addCondtionRow, "", DataViewRowState.CurrentRows)
                    Else
                        dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode IS NULL" & addCondtionRow, "", DataViewRowState.CurrentRows)
                    End If

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

                        For Each drPickupQty As DataRowView In dvPickupQty

                            If grdScanItem.Item(CurrentRow, "PickupQty") <= grdScanItem.Item(CurrentRow, "Quantity") Then
                                drPickupQty("PickupQty") = grdScanItem.Item(CurrentRow, "PickupQty")

                                TotalSalesQty = drPickupQty("PickupQty") + drPickupQty("DeliveredQty")
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

                    CalculateSalesOrderSummary(dsScan)

                Catch ex As Exception
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
                            grdScanItem.Item(CurrentRow, "ExpDelDate") = CtrlSalesInfo1.CtrlDtExpDelDate.Value
                        ElseIf (vCurrentDate.AddSeconds(-1) > Convert.ToDateTime(grdScanItem.Item(CurrentRow, CurrentCell))) Then
                            ShowMessage(getValueByKey("SO010"), "SO010 - " & getValueByKey("CLAE04"))
                            grdScanItem.Item(CurrentRow, CurrentCell) = CtrlSalesInfo1.CtrlDtExpDelDate.Value
                        Else
                            Dim dvDelivery As DataView
                            If IsDBNull(grdScanItem.Item(CurrentRow, "BatchBarcode")) = False Then
                                dvDelivery = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode = '" & grdScanItem.Item(CurrentRow, "BatchBarcode") & "'" & addCondtionRow, "", DataViewRowState.CurrentRows)
                            Else
                                dvDelivery = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode IS NULL" & addCondtionRow, "", DataViewRowState.CurrentRows)
                            End If
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
                    ShowMessage(ex.Message, getValueByKey("CLAE05"))
                End Try
            End If

            If grdScanItem.Cols(CurrentCell).Name = "ReservedQty" Then
                Try
                    Dim dvPickupQty As New DataView
                    If IsDBNull(grdScanItem.Item(CurrentRow, "BatchBarcode")) = False Then
                        dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode = '" & grdScanItem.Item(CurrentRow, "BatchBarcode") & "'", "", DataViewRowState.CurrentRows)
                    Else
                        dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode IS NULL", "", DataViewRowState.CurrentRows)
                    End If

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
                    ShowMessage(ex.Message, getValueByKey("CLAE05"))
                End Try
            End If

            If grdScanItem.Cols(CurrentCell).Name = "IsCLP" Then
                Try
                    Dim dvPickupQty As DataView
                    If IsDBNull(grdScanItem.Item(CurrentRow, "BatchBarcode")) = False Then
                        dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode = '" & grdScanItem.Item(CurrentRow, "BatchBarcode") & "'", "", DataViewRowState.CurrentRows)
                    Else
                        dvPickupQty = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & grdScanItem.Item(CurrentRow, "EAN") & "' And BatchBarcode IS NULL", "", DataViewRowState.CurrentRows)
                    End If
                    If dvPickupQty.Count > 0 Then
                        dvPickupQty.AllowEdit = True
                        For Each drPickupQty As DataRowView In dvPickupQty
                            drPickupQty("IsCLP") = grdScanItem.Item(CurrentRow, "IsCLP")
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
    Private Sub grdScanItem_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdScanItem.CellButtonClick
        Try
            Dim BulkComboMstId As Int64 = 0
            Dim ComboSrNo = grdScanItem.Item(grdScanItem.Row, "RowIndex")
            'Dim deleteRowNo As Integer = 0
            If DtSoBulkComboHdr.Rows.Count > 0 Then
                Dim drHdr() = DtSoBulkComboHdr.Select("ComboSrNo=" & ComboSrNo)
                'If drHdr.Count > 0 Then
                '    deleteRowNo = ComboSrNo
                'End If
            End If

            If MsgBox(getValueByKey("SO011"), MsgBoxStyle.YesNo, "SO011 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                If IsDBNull(grdScanItem.Item(grdScanItem.Row, "BatchBarcode")) = False Then
                    'DeleteScanItemInSO(grdScanItem.Item(grdScanItem.Row, "EAN"), grdScanItem.Item(grdScanItem.Row, "BatchBarcode"), deleteRowNo)
                    DeleteScanItemInSO(grdScanItem.Item(grdScanItem.Row, "EAN"), grdScanItem.Item(grdScanItem.Row, "BatchBarcode"), ComboSrNo)
                Else
                    'DeleteScanItemInSO(grdScanItem.Item(grdScanItem.Row, "EAN"), , deleteRowNo)
                    DeleteScanItemInSO(grdScanItem.Item(grdScanItem.Row, "EAN"), , ComboSrNo)
                End If
                ''----Delete Bulk Combo Details As Well...
                If DtSoBulkComboHdr.Rows.Count > 0 Then
                    DeleteBulkCombo(ComboSrNo)
                End If
            End If
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
    Private Sub grdScanItem_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdScanItem.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                If MsgBox(getValueByKey("SO011"), MsgBoxStyle.YesNo, "SO011 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                    If IsDBNull(grdScanItem.Item(grdScanItem.Row, "BatchBarcode")) = False Then
                        DeleteScanItemInSO(grdScanItem.Item(grdScanItem.Row, "EAN"), grdScanItem.Item(grdScanItem.Row, "BatchBarcode"))
                    Else
                        DeleteScanItemInSO(grdScanItem.Item(grdScanItem.Row, "EAN"))
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
    Private Sub grdScanItem_RowColChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdScanItem.RowColChange
        Try
            If (grdScanItem.Row >= 1) Then

                'grdScanItem.Cols("ArticleCode").Visible = True
                vArticleImagesCode = grdScanItem.Item(grdScanItem.Row, "ArticleCode")
                'grdScanItem.Cols("ArticleCode").Visible = False

                'strImagesUrl = objComn.GetArticleImage(vArticleImagesCode, My.Settings.ArticleImageFolder)
                'PictureBoxImages.ImageLocation = strImagesUrl
                CtrlProductImage.ShowArticleImage(vArticleImagesCode)
            End If
        Catch ex As Exception
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
            Dim addCondtion As String = String.Empty
            If rowIndex > 0 Then
                addCondtion = " AND RowIndex =" & rowIndex
            End If

            If String.IsNullOrEmpty(batchBarcode) = False Then
                dvEditDeleteItems = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & vEAN & "' And BatchBarcode = '" & batchBarcode & "'" & addCondtion, "", DataViewRowState.CurrentRows)
            Else
                dvEditDeleteItems = New DataView(_dsScan.Tables("ItemScanDetails"), "EAN='" & vEAN & "' And BatchBarcode IS NULL" & addCondtion, "", DataViewRowState.CurrentRows)
            End If
            If dvEditDeleteItems.Count > 0 Then
                dvEditDeleteItems.AllowEdit = True

                For Each drView As DataRowView In dvEditDeleteItems
                    dvDeleteTaxOnItem = New DataView(dsMain.Tables("SalesOrderTaxDtls"), "EAN='" & vEAN & "'", "", DataViewRowState.CurrentRows)
                    If dvDeleteTaxOnItem.Count > 0 Then
                        dvDeleteTaxOnItem.AllowDelete = True
                        For Each dr As DataRowView In dvDeleteTaxOnItem
                            ' dr.Delete()
                            'modified by irfan on 25-10-2017 for cake carft changes
                            For i As Integer = dsMain.Tables("SalesOrderTaxDtls").Rows.Count - 1 To 0 Step -1
                                ' dr.Delete()
                                If dsMain.Tables("SalesOrderTaxDtls").Rows(i)("EAN") = vEAN Then
                                    dsMain.Tables("SalesOrderTaxDtls").Rows.RemoveAt(i)
                                End If
                            Next
                        Next
                    End If
                    drView.Delete()

                Next
                If Me.Batchbarcode.Where(Function(w) w.EAN = vEAN).FirstOrDefault() IsNot Nothing Then
                    Me.Batchbarcode.RemoveAll(Function(w) w.EAN = vEAN)
                End If

                _dsScan.AcceptChanges()
                CalculateSalesOrderSummary(dsScan)
                RefreshScanData(dsScan)
                GridSetting()
            End If

            If grdScanItem.Rows.Count = 1 Then
                CtrlProductImage.ClearImage()
                'PictureBoxImages.Image = Nothing

                lblTotalItem.Text = 0
                lblOrderQty.Text = 0
                lblPickupQty.Text = 0

                CtrlCashSummary1.lbltxt1 = strZero
                lblGrossAmt1.Text = strZero
                CtrlCashSummary1.lbltxt2 = strZero
                CtrlCashSummary1.lbltxt3 = strZero

                CtrlCashSummary1.lbltxt4 = strZero
                CtrlCashSummary1.lbltxt5 = strZero
                CtrlCashSummary1.lbltxt7 = strZero



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
        Dim findkeyTax(4) As Object
        'Dim dtTaxCalc As DataTable
        Dim drAddItem As DataRow

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

                drAddItem("EAN") = drItemsRow.Item("EAN")
                drAddItem("DeliverySiteCode") = DeliverySiteCode
                drAddItem("Discription") = drItemsRow.Item("DISCRIPTION")
                drAddItem("BatchBarcode") = drItemsRow.Item("BatchBarcode")

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

                'Code added by irfan on 07/11/2017 for cakeology IGST==========================================================
                Dim _strCustNo = CtrlCustSearch1.CtrlTxtCustNo.Text.Trim()
              
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

                '=====================================================================================================================

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
                            findkeyTax(2) = CtrlSalesInfo1.CtrlTxtOrderNo.Value
                            findkeyTax(3) = drAddItem("EAN")
                            findkeyTax(4) = vTaxLineNo
                            drTax = dsMain.Tables("SalesOrderTaxDtls").Rows.Find(findkeyTax)

                            If drTax Is Nothing Then
                                drTax = dsMain.Tables("SalesOrderTaxDtls").NewRow

                                drTax("SiteCode") = vSiteCode
                                drTax("FinYear") = clsAdmin.Financialyear
                                drTax("SaleOrderNumber") = CtrlSalesInfo1.CtrlTxtOrderNo.Value
                                drTax("EAN") = drAddItem("EAN")
                                drTax("TaxLineNo") = vTaxLineNo
                                drTax("TaxLabel") = drRowTax("TaxCode")
                                drTax("TaxValue") = Math.Round(drRowTax("TaxAmount"), 2)

                                dsMain.Tables("SalesOrderTaxDtls").Rows.Add(drTax)
                            Else
                                drTax("SiteCode") = vSiteCode
                                drTax("SaleOrderNumber") = CtrlSalesInfo1.CtrlTxtOrderNo.Value
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
                drAddItem("ExpDelDate") = CtrlSalesInfo1.CtrlDtExpDelDate.Value
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
                        'end of change
                        vRowIndex = vRowIndex + 1
                    End If
                Else
                    ShowMessage(getValueByKey("SO004"), "SO004 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Article Tax Details does not Found. ", "Tax Details")
                End If

                _dsScan.AcceptChanges()
            End If
            'code added by irfan on 07/11/2017 for cakeology IGST==============================================================
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


            If _dsScan.Tables("ItemScanDetails").Columns.Contains("NetAmountPickup") = True Then
                _dsScan.Tables("ItemScanDetails").Columns.Remove("NetAmountPickup")
            End If

            If dsMain.Tables("SalesOrderTaxDtls").Rows.Count > 0 Then
                If dsMain.Tables("SalesOrderTaxDtls").Columns.Contains("CustomerNo") = True Then
                    For i = 0 To dsMain.Tables("SalesOrderTaxDtls").Rows.Count - 1
                        dsMain.Tables("SalesOrderTaxDtls").Rows(i)("CustomerNo") = CtrlCustSearch1.CtrlTxtCustNo.Text.Trim()
                    Next
                Else
                    dsMain.Tables("SalesOrderTaxDtls").Columns.Add("CustomerNo", System.Type.GetType("System.String"))
                    For i = 0 To dsMain.Tables("SalesOrderTaxDtls").Rows.Count - 1
                        dsMain.Tables("SalesOrderTaxDtls").Rows(i)("CustomerNo") = CtrlCustSearch1.CtrlTxtCustNo.Text.Trim()
                    Next
                End If
            End If
            '===================================================================================================================

            'grdScanItem.Select(grdScanItem.Rows.Count - 1, 1, True)
            grdScanItem.Select(1, 1, True)
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
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

        Return True
    End Function

#End Region

    Private Sub rbBtnAddCombo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal flg As Boolean = False) 'Handles BtnSOSave.Click
        Try
            '.  It will be enabled only when user selects customer.
            If CtrlCustSearch1.CtrlTxtCustNo.Text = String.Empty Then
                ShowMessage(getValueByKey("SO048"), "SO048 - " & getValueByKey("CLAE04"))
                CtrlCustSearch1.CtrlTxtCustNo.Select()
                Exit Sub
            End If
            Dim combosrNo As Integer = vRowIndex
            Dim objBulkOrderCombo As New frmBulkOrderCombo
            objBulkOrderCombo.DtSoBulkComboHdr = DtSoBulkComboHdr
            objBulkOrderCombo.DtSoBulkComboDtl = DtSoBulkComboDtl

            objBulkOrderCombo.BulkComboMstId = combosrNo
            objBulkOrderCombo.ComboSrNo = combosrNo
            objBulkOrderCombo.btnPrint.Enabled = False
            objBulkOrderCombo.btnPrint.Enabled = False

            If objBulkOrderCombo.ShowDialog() = Windows.Forms.DialogResult.OK Then
                DtSoBulkComboHdr = objBulkOrderCombo.DtSoBulkComboHdr
                DtSoBulkComboDtl = objBulkOrderCombo.DtSoBulkComboDtl

                If DtSoBulkComboHdr.Rows.Count > 0 Then
                    CtrlSalesPersons.CtrlTxtBox.Text = DtSoBulkComboHdr.Rows(DtSoBulkComboHdr.Rows.Count - 1)("PackagingBoxCode")
                    IsNewComboAdd = True
                    txtSearchItem_Leave(CtrlSalesPersons.CtrlTxtBox, e)
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


    Private Sub grdScanItem_DoubleClick(sender As Object, e As EventArgs) Handles grdScanItem.DoubleClick
        Try
            If DtSoBulkComboHdr.Rows.Count > 0 Then
                Dim ComboSrNo = grdScanItem.Item(grdScanItem.Row, "RowIndex")
                Dim BulkComboMstId As Int64 = 0

                Dim dr() = DtSoBulkComboHdr.Select("ComboSrNo=" & ComboSrNo)
                If dr.Count > 0 Then
                    BulkComboMstId = dr(0)("BulkComboMstId")
                    Dim objBulkOrderCombo As New frmBulkOrderCombo
                    objBulkOrderCombo.DtSoBulkComboHdr = DtSoBulkComboHdr.Copy()
                    objBulkOrderCombo.DtSoBulkComboDtl = DtSoBulkComboDtl.Copy()
                    objBulkOrderCombo.BulkComboMstId = BulkComboMstId
                    objBulkOrderCombo.ComboSrNo = ComboSrNo
                    'objBulkOrderCombo.BulkComboMode = enumBulkComboMode.AddDoubleClick
                    objBulkOrderCombo.CboPakagingBox.Enabled = False
                    objBulkOrderCombo.btnPrint.Enabled = False
                    If objBulkOrderCombo.ShowDialog() = Windows.Forms.DialogResult.OK Then
                        DtSoBulkComboHdr = objBulkOrderCombo.DtSoBulkComboHdr
                        DtSoBulkComboDtl = objBulkOrderCombo.DtSoBulkComboDtl
                    End If
                End If
            End If

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
                lblOrderQty.Text = CDbl(dsScan.Tables("ItemScanDetails").Compute("SUM(Quantity)", ""))
                lblPickupQty.Text = CDbl(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(PickUpQty)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("SUM(PickUpQty)", "")))

                CtrlCashSummary1.lbltxt1 = CDbl(dsScan.Tables("ItemScanDetails").Compute("SUM(GrossAmt)", ""))
                lblGrossAmt1.Text = CtrlCashSummary1.lbltxt1
                If IsRoundOfflabel Then
                    CtrlCashSummary1.CtrlLabel2.Text = "Roundoff Amt."
                    CtrlCashSummary1.CtrlLabel2.Tag = "Roundoff Amt."
                    'IsRoundOffMsg = False
                Else
                    CtrlCashSummary1.CtrlLabel2.Text = "Disc Amt."
                    CtrlCashSummary1.CtrlLabel2.Tag = "Disc Amt."
                End If
                CtrlCashSummary1.lbltxt2 = FormatNumber(CDbl(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(LineDiscount)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("SUM(LineDiscount)", ""))), 2)
                CtrlCashSummary1.lbltxt4 = FormatNumber(CDbl(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(NetAmount)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("SUM(NetAmount)", ""))), 2)
                If clsDefaultConfiguration.RoundOffRequired = True Then
                    CtrlCashSummary1.lbltxt4 = MyRound(CDbl(CtrlCashSummary1.lbltxt4), clsDefaultConfiguration.BillRoundOffAt)
                Else
                    CtrlCashSummary1.lbltxt4 = CDbl(CtrlCashSummary1.lbltxt4)
                End If
                If Not (dtOtherCharges Is Nothing) AndAlso dtOtherCharges.Rows.Count > 0 Then
                    Dim vChargeAmount As String = IIf((dtOtherCharges.Compute("Sum(ChargeAmount)", "") Is DBNull.Value), 0, dtOtherCharges.Compute("Sum(ChargeAmount)", ""))
                    Dim vTaxAmount As String = IIf((dtOtherCharges.Compute("Sum(TaxAmt)", "") Is DBNull.Value), 0, dtOtherCharges.Compute("Sum(TaxAmt)", ""))

                    CtrlCashSummary1.lbltxt3 = FormatNumber(CDbl(vChargeAmount) + CDbl(vTaxAmount), 2)
                Else
                    CtrlCashSummary1.lbltxt3 = "0"
                End If

                If Not (CDbl(IIf(CtrlCashSummary1.lbltxt3 <> String.Empty, CtrlCashSummary1.lbltxt3, 0)) = 0) Then
                    CtrlCashSummary1.lbltxt1 = FormatNumber(CDbl(CtrlCashSummary1.lbltxt1) + CDbl(CtrlCashSummary1.lbltxt3), 2)
                    CtrlCashSummary1.lbltxt4 = FormatNumber(CDbl(CtrlCashSummary1.lbltxt4) + CDbl(CtrlCashSummary1.lbltxt3), 2)
                End If
                Dim VminOtherAdjamt As Double
                vMinAdvancePay = Math.Round(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(MinPayAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("SUM(MinPayAmt)", "")), 2)

                If Not (CDbl(IIf(CtrlCashSummary1.lbltxt3 <> String.Empty, CtrlCashSummary1.lbltxt3, 0)) = 0) Then
                    VminOtherAdjamt = CDbl(CtrlCashSummary1.lbltxt3) * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)
                    vMinAdvancePay = vMinAdvancePay + VminOtherAdjamt
                End If

                If (Val(lblOrderQty.Text) = Val(lblPickupQty.Text)) Then
                    CtrlCashSummary1.lbltxt5 = FormatNumber(CDbl(CtrlCashSummary1.lbltxt4), 2)
                Else
                    CtrlCashSummary1.lbltxt5 = FormatNumber(vMinAdvancePay, 2)
                End If

                If Not (dsPayment Is Nothing) AndAlso dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                    CtrlCashSummary1.lbltxt6 = FormatNumber(CDbl(dsPayment.Tables("MSTRecieptType").Compute("Sum(Amount)", "")), 2)
                    If Not (CDbl(CtrlCashSummary1.lbltxt4) > CDbl(CtrlCashSummary1.lbltxt6)) AndAlso CDbl(CtrlCashSummary1.lbltxt5) < CDbl(CtrlCashSummary1.lbltxt6) Then
                        CtrlCashSummary1.lbltxt6 = FormatNumber(CDbl(CtrlCashSummary1.lbltxt4), 2)
                    End If
                    CtrlCashSummary1.lbltxt7 = FormatNumber(CDbl(CtrlCashSummary1.lbltxt4) - CDbl(CtrlCashSummary1.lbltxt6), 2)
                Else
                    CtrlCashSummary1.lbltxt6 = "0"
                    CtrlCashSummary1.lbltxt7 = "0"
                End If
                CtrlCashSummary1.lbltxt4 = FormatNumber(MyRound(CDbl(CtrlCashSummary1.lbltxt4), clsDefaultConfiguration.BillRoundOffAt), 2)
            End If
            CtrlCashSummary1.lbltxt4 = MyRound(CtrlCashSummary1.lbltxt4, clsDefaultConfiguration.BillRoundOffAt)
            CtrlCashSummary1.lbltxt5 = MyRound(CtrlCashSummary1.lbltxt5, clsDefaultConfiguration.BillRoundOffAt)

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

                Dim obj As New clsSaleOrderCommon
                obj.IsCSTApplicable = Me.IsCSTApplicable
                obj.RecalculateLine(drRem, CtrlSalesInfo1.CtrlTxtOrderNo.Value, dsMain)
            Next

            dsScanTemp.AcceptChanges()
            IsApplyPromotion = False
            IsSelectedPromotion = False
            IsDefaultPromotion = False
            rbnCST.Enabled = True
            IsRoundOffMsg = False
            IsRoundOfflabel = False
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
                        dtSource(0)("DISCRIPTION") = objClsCommon.GetArticleFullName(dtSource.Rows(0)("ArticleCode").ToString())
                    End If
                End If
                grdScanItem.DataSource = dtSource
                grdScanItem.Cols("DeliverySiteCode").DataMap = hash

                grdScanItem.Cols("DeliverySiteCode").AllowEditing = False

                'grdScanItem.Cols("ArticleCode").Visible = False
                'grdScanItem.Cols("ExpDelDate").StyleDisplay.Format = DateFormat.ShortDate
                grdScanItem.Cols("ExpDelDate").UserData = CtrlSalesInfo1.CtrlDtExpDelDate.Value
                GridSetting()
            Else
                grdScanItem.DataSource = Nothing
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    ''' <summary>
    ''' Clears All Resource for new sales order
    ''' </summary>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function ResetSalesOrder() As Boolean

        If Not (dsScan Is Nothing) AndAlso dsScan.Tables("ItemScanDetails").Rows.Count > 0 Then
            dsScan.Clear()
        End If

        RefreshScanData(dsScan)
        GridSetting()

        If Not (dsMain Is Nothing) Then
            dsMain.Clear()
        End If

        If Not dtOtherCharges Is Nothing AndAlso dtOtherCharges.Rows.Count > 0 Then
            dtOtherCharges.Clear()
        End If

        If Not (dsPayment Is Nothing) Then
            dsPayment.Clear()
        End If

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

        CtrlSalesInfo1.CtrldtOrderDt.Value = vCurrentDate
        CtrlSalesInfo1.CtrlDtExpDelDate.Value = vCurrentDate.AddDays(clsDefaultConfiguration.ChkDeliveryDate)
        CtrlSalesInfo1.CtrlTxtRemarks.Text = ""

        CtrlSalesInfo1.CtrlTxtCustOrdRef.Text = ""

        CtrlProductImage.ClearImage()
        CtrlSalesPersons.CtrlSalesPersons.SelectedIndex = -1
        'CtrlCustDtls.pClear()

        CtrlSalesInfo1.CtrlTxtInvoice.Text = ""
        lblTotalItem.Text = 0
        lblOrderQty.Text = 0
        lblPickupQty.Text = 0
        'CtrlCashSummary1.lbl5 = "0.00"
        'lblGrossAmt1.Text = strZero

        CtrlCashSummary1.lbltxt1 = strZero
        CtrlCashSummary1.lbltxt2 = strZero
        CtrlCashSummary1.lbltxt3 = strZero
        CtrlCashSummary1.lbltxt4 = strZero
        CtrlCashSummary1.lbltxt5 = strZero
        CtrlCashSummary1.lbltxt6 = strZero
        CtrlCashSummary1.lbltxt7 = strZero

        CtrlCustSearch1.rbOtherCust.Checked = True
        CtrlCustSearch1.rbCLPMember.Checked = True
        CtrlCustSearch1.CustmType = "CLP"
        CtrlCashSummary1.CtrlLabel2.Text = "Disc Amt."
        IsRoundOfflabel = False
        vCardType = ""
        'vClpProgramId = ""
        vSalesInvcNo = String.Empty
        TotalPoints = 0
        IsDefaultPromotion = False
        IsOutboundCreated = False
        SoDeliveryInfo = New BindingList(Of SODeliveryLocationInfo)()
        dgDeliveryLocation.DataSource = SoDeliveryInfo
        DeliverySiteCode = clsAdmin.SiteCode
        Batchbarcode = New List(Of SpectrumCommon.BtachbarcodeInfo)
        If Not (CtrlCustDtls1.dtCustmInfo Is Nothing) Then
            CtrlCustDtls1.dtCustmInfo.Clear()
        End If
        dtItemScanData = Nothing
    End Function

#End Region

#Region "Save/Update Sales Order "
    ''' <summary>
    ''' Preapring the Sales Order data for save
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnSaveSalesOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal flg As Boolean = False) 'Handles BtnSOSave.Click

        Try
            grdScanItem.FinishEditing()

            If Not (dsScan.Tables(0).Rows.Count > 0) Then

                ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If


            If CtrlCustSearch1.CtrlTxtCustNo.Text = String.Empty Then
                ShowMessage(getValueByKey("SO013"), "SO013 - " & getValueByKey("CLAE04"))
                CtrlCustSearch1.CtrlTxtCustNo.Select()

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
                If CDbl(CtrlCashSummary1.lbltxt6) < CDbl(CtrlCashSummary1.lbltxt5) Then
                    BtnAcceptPayment_Click(sender, e)
                    Exit Sub
                End If
                '********************************************************
            End If
            'End of change



            GetNewSalesOrderNumber()
            'If OnlineConnect = True Then
            '    'Changed by Rohit to generate Document No. for proper sorting
            '    Try
            '        CtrlSalesInfo1.CtrlTxtOrderNo.Value = GenDocNo("SO" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode))
            '    Catch ex As Exception
            '        CtrlSalesInfo1.CtrlTxtOrderNo.Value = "SO" & clsAdmin.TerminalID & objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode)
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
            '        CtrlSalesInfo1.CtrlTxtOrderNo.Value = GenDocNo("OSO" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode))
            '    Catch ex As Exception
            '        CtrlSalesInfo1.CtrlTxtOrderNo.Value = "OSO" & clsAdmin.TerminalID & objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode)
            '    End Try

            '    Try
            '        rbnLStatus.SmallImage = Spectrum.My.Resources.disconnected1.ToBitmap
            '        rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus1")
            '    Catch ex As Exception

            '    End Try
            '    'End Change by Rohit
            'End If

            'CtrlSalesInfo1.CtrlTxtOrderNo.Value = "SO" & clsAdmin.TerminalID & objComn.getDocumentNo("SalesOrder")
            If OnlineConnect = True Then
                If CtrlCustSearch1.CustmType = "CLP" AndAlso Val(lblOrderQty.Text) = Val(lblPickupQty.Text) Then
                    vCardType = CtrlCustSearch1.CardType
                    CalCulateCLP(vCardType, dsScan.Tables("ItemScanDetails"), "PickUpQty>0")
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
            End If

            'Code added by irfan on 07/11/2017 for cakeology IGST===============================================================
            Dim _strCustNo = CtrlCustSearch1.CtrlTxtCustNo.Text.Trim()
            Dim IsIGSTApplicableForOutsideCustomer = objComn.checkIGSTAplicableForOutSideStateCustomer(clsAdmin.SiteCode, _strCustNo)
            Dim tempSalesOrderTaxDtl As DataTable = dsMain.Tables("SalesOrderTaxDtls").Copy
            Dim tempSalesOrderDtl As DataTable = dsMain.Tables("SalesOrderDTL").Copy
            Dim dv As New DataView(tempSalesOrderDtl)
            dv.Sort = "EAN Asc"
            For io As Integer = 0 To dv.Table.Rows.Count - 1
                Dim drm As DataRow() = tempSalesOrderTaxDtl.Select("EAN='" & dv.Table.Rows(io)("EAN") & "'")
                'For Each drr As DataRow In drm
                For i As Integer = 0 To drm.Length - 1
                    If tempSalesOrderDtl.Rows(io)("LineDiscount").ToString() <> "" Then
                        If IsIGSTApplicableForOutsideCustomer = False Then
                            drm(i)("TaxValue") = tempSalesOrderDtl.Rows(io)("TotalTaxAmount") / 2
                        Else
                            drm(i)("TaxValue") = tempSalesOrderDtl.Rows(io)("TotalTaxAmount")
                        End If
                    End If
                Next
                'Next
                dsMain.Tables("SalesOrderTaxDtls").Clear()
                dsMain.Tables("SalesOrderTaxDtls").AcceptChanges()
                dsMain.Tables("SalesOrderTaxDtls").Merge(tempSalesOrderTaxDtl)
                dsMain.Tables("SalesOrderTaxDtls").AcceptChanges()
            Next


            If dsMain.Tables("SalesOrderTaxDtls").Columns.Contains("CustomerNo") = True Then
                dsMain.Tables("SalesOrderTaxDtls").Columns.Remove("CustomerNo")
            End If
            '===============================================================================================================
            dsMain.Tables("SalesOrderTaxDtls").AcceptChanges()
            For Each drTax As DataRow In dsMain.Tables("SalesOrderTaxDtls").Rows
                drTax("SaleOrderNumber") = CtrlSalesInfo1.CtrlTxtOrderNo.Value
            Next

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
                RecalculateDeliveryAmt(CDbl(CtrlCashSummary1.lbltxt4), totalPaidAmt)
            End If
            For Each item In SoDeliveryInfo
                If item.Quantity IsNot Nothing Then
                    item.ReservedQuantity = item.Quantity
                End If
            Next

            If DtSoBulkComboHdr.Rows.Count > 0 Then
                For index = 0 To DtSoBulkComboHdr.Rows.Count - 1
                    DtSoBulkComboHdr(index)("SaleOrderNumber") = CtrlSalesInfo1.CtrlTxtOrderNo.Value

                    'Dim BillNo = (From x In dsMain.Tables("SalesOrderDTL")
                    '               Where x("RowIndex") = DtSoBulkComboHdr(index)("ComboSrNo")
                    '               Select x("BillLineNo")).FirstOrDefault()

                    Dim BillNo = dsMain.Tables("SalesOrderDTL").Select("RowIndex=" & DtSoBulkComboHdr(index)("ComboSrNo"))(0)("BillLineNo")
                    If Not BillNo Is Nothing Then
                        DtSoBulkComboHdr(index)("ComboSrNo") = BillNo
                    End If
                Next

                objSO.DtSoBulkComboHdr = DtSoBulkComboHdr
                objSO.DtSoBulkComboDtl = DtSoBulkComboDtl
            End If

            dsMain.Tables("SalesOrderDTL").Columns.Remove("RowIndex")
            '--- for SO Generate
            objSO.IsStrGenerate = IsSTRGenerate

            Dim SOStatus As String
            SOStatus = dsMain.Tables("SalesOrderhDR").Rows(0)("SOstatus")

            If objSO.PrepareSaveData(vSalesInvcNo, clsAdmin.DayOpenDate, clsAdmin.CLPProgram, CtrlCustSearch1.CtrlTxtCustNo.Text, dsMain, True, True, vSiteCode, CtrlSalesInfo1.CtrlTxtOrderNo.Value, clsDefaultConfiguration.StockStorageLocation, clsAdmin.CVProgram, "SalesOrder", clsAdmin.Financialyear, clsAdmin.UserCode, clsAdmin.CurrentDate, IsOutboundCreated, CVoucherNo, CVVoucherDay, IsApplyPromotion, Nothing, SoDeliveryInfo.ToList(), clsDefaultConfiguration.IsBatchManagementReq, Batchbarcode) = True Then
                'Apply Customer loyalty Point
                If OnlineConnect = True Then
                    If CtrlCustSearch1.CustmType = "CLP" AndAlso Val(lblOrderQty.Text) = Val(lblPickupQty.Text) Then
                        If dsMainCLP.Tables.Count = 0 Then
                            dsMainCLP.Tables.Add(dsMain.Tables("CLPTransaction").Clone())
                            dsMainCLP.Tables.Add(dsMain.Tables("CLPTransactionsDetails").Clone)
                        End If

                        dsMainCLP.Clear()
                        TotalPoints = CDbl(dsScan.Tables("ItemScanDetails").Compute("Sum(CLPPoints)", "PickUpQty>0"))

                        If TotalPoints > 0 AndAlso PrepareClpHdrDataforSave(dsMainCLP) = True AndAlso PrepareClpDtlDataforSave(dsMainCLP) = True Then
                            If objSO.PrepareSaveClpData(dsMainCLP, vClpProgramId, CtrlCustSearch1.CtrlTxtCustNo.Text, TotalPoints, vSiteCode, CtrlSalesInfo1.CtrlTxtOrderNo.Value) = False Then

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


                _drSiteInfo = objComn.GetSiteInfo(vSiteCode).Rows(0)

                If CtrlCustDtls1.dtCustmInfo.Rows.Count > 2 Then
                    drHomeAdds = CtrlCustDtls1.dtCustmInfo.Select("AddressType='1'")(0)
                Else
                    drHomeAdds = CtrlCustDtls1.dtCustmInfo.Rows(0)
                End If
                If Not IsDBNull(CtrlCustDtls1.cboAddrType.SelectedValue) AndAlso (CtrlCustDtls1.cboAddrType.SelectedValue.ToString() <> String.Empty) Then
                    ' If Not (CtrlCustDtls1.cboAddrType.SelectedValue Is DBNull.Value) AndAlso Not (CtrlCustDtls1.cboAddrType.SelectedValue = 99) Then
                    drDelvAdds = CtrlCustDtls1.dtCustmInfo.Select("AddressType='" & CtrlCustDtls1.cboAddrType.SelectedValue & "' ")(0)
                Else
                    drDelvAdds = CtrlCustDtls1.dtCustmInfo.Rows(0)
                End If


                'Print Sales Order Information.-----------------------------------
                PrintSalesOrders(drSiteInfo, drHomeAdds, drDelvAdds, SOStatus)
                Dim totalPay As Double
                'Print Sales Invoice Information----------------------------------
                If Not (dsPayment.Tables("MSTRecieptType") Is Nothing) AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                    '---- Commented By Mahesh Not required as we are showing on bill itself disscussed with B.A. Manish
                    ' PrintSalesOrdersInvoice(drSiteInfo, drHomeAdds, drDelvAdds)

                    For Each dr As DataRow In dsPayment.Tables("MSTRecieptType").Select("RecieptTypeCode='CreditVouc(I)'", "", DataViewRowState.CurrentRows)
                        totalPay = IIf(dr("Amount") > 0, dr("Amount"), dr("Amount") * -1)

                        'PrintCreditVoucher(drSiteInfo, totalPay)
                        clsVoucher.PrintGiftVoucherAndCreditNote("SalesOrder", clsAdmin.SiteCode, "CreditNote", String.Empty, totalPay, String.Empty, clsAdmin.UserName, CtrlSalesInfo1.CtrlTxtOrderNo.Value, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                    Next
                    For Each dr As DataRow In dsPayment.Tables("MSTRecieptType").Select("RecieptTypeCode='GiftVoucher(I)'", "", DataViewRowState.CurrentRows)
                        totalPay = IIf(dr("Amount") > 0, dr("Amount"), dr("Amount") * -1)

                        'PrintCreditVoucher(drSiteInfo, totalPay)
                        clsVoucher.PrintGiftVoucherAndCreditNote("SalesOrder", clsAdmin.SiteCode, "GiftVoucher", String.Empty, totalPay, String.Empty, clsAdmin.UserName, CtrlSalesInfo1.CtrlTxtOrderNo.Value, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                    Next
                End If

                '---- Commented By Mahesh Not required as we are showing on bill itself disscussed with B.A. Manish
                'Print Sales Delivery Information---------------------------------
                If CDbl(lblPickupQty.Text) > 0 Then
                    PrintSalesOrdersDelivery(drSiteInfo, drHomeAdds, drDelvAdds)
                End If

                If IsGiftVoucher Then
                    PrintGiftReceipt()
                End If

                ShowMessage(getValueByKey("SO019"), "SO019 - " & getValueByKey("CLAE04"))
                IsSOSaved = True
                'ShowMessage("Sales Order Created", "Sales Order")
                If clsDefaultConfiguration.AskForMoreCustomerInfo Then
                    'Dim missingInfo As String = CustomerMissingInfoFinder.Instance.FindMissingParameter(CtrlCustSearch1.dtCustmInfo)
                    'If Not String.IsNullOrEmpty(missingInfo) Then
                    '    Dim EventType As Int32
                    '    ShowMessage("Please ask " & missingInfo & " from customer", getValueByKey("CLAE04"), EventType, True, getValueByKey("mod009"))
                    '    If EventType = 1 Then
                    '        Dim objCreateNewCustm As New frmNSOCustomer
                    '        objCreateNewCustm.Tag = String.Empty
                    '        objCreateNewCustm.pSearchCust = "SEARCH"
                    '        objCreateNewCustm._CustomerNoToSearch = CtrlCustSearch1.CtrlTxtCustNo.Text
                    '        objCreateNewCustm.ShowDialog()
                    '    End If
                    'End If
                End If


                ResetSalesOrder()
                BtnSONew_Click(sender, e)
                'CtrlSalesInfo1.CtrlTxtOrderNo.Value = CtrlSalesInfo1.CtrlTxtOrderNo.Value + 1
                EnableButton(True)
                AutoLogout(FrmTranCode, Me, lblLoggedIn)
            Else
                ShowMessage(getValueByKey("SO020"), "SO020 - " & getValueByKey("CLAE04"))
                'ShowMessage("Sales Order is not created", "Sales Order")
            End If
        Catch ex As Exception
            LogException(ex)
            ShowMessage(getValueByKey("SO020"), "SO020 - " & getValueByKey("CLAE04"))
        End Try
    End Sub
    Private Function PrintGiftReceipt() As Boolean
        Try
            If Not dsPayment Is Nothing AndAlso dsPayment.Tables.Contains("MSTRecieptType") Then

                Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.GiftVoucherDocumentPrint, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserCode, CtrlSalesInfo1.CtrlTxtOrderNo.Value, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo1.CtrlTxtCustOrdRef.Text, vSalesInvcNo, GiftReceiptMessage, Nothing, Nothing, dtPrinterInfo, ShowFullName:=clsDefaultConfiguration.PrintItemFullName)

                'Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, vSalesInvcNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo1.CtrlTxtCustOrdRef.Text)

            Else

                Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.GiftVoucherDocumentPrint, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserCode, CtrlSalesInfo1.CtrlTxtOrderNo.Value, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), Nothing, CtrlSalesInfo1.CtrlTxtCustOrdRef.Text, vSalesInvcNo, GiftReceiptMessage, Nothing, Nothing, dtPrinterInfo, ShowFullName:=clsDefaultConfiguration.PrintItemFullName)

                'Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, vSalesInvcNo, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), Nothing, CtrlSalesInfo1.CtrlTxtCustOrdRef.Text)


            End If
            IsGiftVoucher = False
        Catch ex As Exception
        Finally

            IsGiftVoucher = False

        End Try

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
    '        PrintSo.Append("Issued against S.O. No. :" & CtrlSalesInfo1.CtrlTxtOrderNo.Value & "   Date: " & Format(vCurrentDate, vDateFormat) & vbCrLf)

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
            findKey(2) = CtrlSalesInfo1.CtrlTxtOrderNo.Value
            drSOHdr = dsMain.Tables("SalesOrderHDR").Rows.Find(findKey)
            If drSOHdr Is Nothing Then
                drSOHdr = dsMain.Tables("SalesOrderHDR").NewRow()
                IsNewRow = True
            End If
            drSOHdr("SaleOrderNumber") = CtrlSalesInfo1.CtrlTxtOrderNo.Value
            drSOHdr("SiteCode") = vSiteCode
            drSOHdr("TerminalID") = vTerminalID
            drSOHdr("FinYear") = clsAdmin.Financialyear
            drSOHdr("TerminalID") = vTerminalID
            drSOHdr("TransactionCode") = vDocType

            drSOHdr("CustomerNo") = CtrlCustSearch1.CtrlTxtCustNo.Text
            drSOHdr("CustomerType") = CtrlCustSearch1.CustmType
            drSOHdr("NetAmt") = CDbl(CtrlCashSummary1.lbltxt4)
            drSOHdr("CostAmt") = CDbl(CtrlCashSummary1.lbltxt4)
            drSOHdr("GrossAmt") = CDbl(CtrlCashSummary1.lbltxt1.Trim)
            drSOHdr("IsCSTApplied") = IsCSTApplicable
            drSOHdr("CSTTaxCode") = clsDefaultConfiguration.CSTTaxCode
            If Not (dsPayment Is Nothing) AndAlso dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables(0).Rows.Count > 0 Then
                drSOHdr("AdvanceAmt") = CType(IIf(drSOHdr("AdvanceAmt") Is DBNull.Value, 0, drSOHdr("AdvanceAmt")), Double) + CType(dsPayment.Tables(0).Compute("Sum(Amount)", ""), Double)
            Else
                drSOHdr("AdvanceAmt") = 0.0
            End If

            drSOHdr("DiscountPercentage") = FormatNumber(dsScan.Tables("ItemScanDetails").Compute("Sum(TotalDiscPercentage)", "") / dsScan.Tables("ItemScanDetails").Rows.Count, 2)
            drSOHdr("DiscountAmt") = CDbl(CtrlCashSummary1.CtrllblTaxAmt.Text.Trim)
            drSOHdr("TotalDiscount") = CDbl(CtrlCashSummary1.CtrllblTaxAmt.Text.Trim)
            'drSOHdr("BalanceAmount") = CDbl(CtrlCashSummary1.lbltxt7.Trim)
            drSOHdr("BalanceAmount") = Math.Round(CDbl(CtrlCashSummary1.lbltxt4.Trim) - CDbl(drSOHdr("AdvanceAmt")), 2)
            drSOHdr("RoundedAmt") = CDbl(CtrlCashSummary1.lbltxt4)

            drSOHdr("LineItems") = dsScan.Tables("ItemScanDetails").Rows.Count
            drSOHdr("CreditNoteNo") = DBNull.Value
            drSOHdr("TransCurrency") = vCurrencyDescription
            drSOHdr("PostingStatus") = "Posted"

            drSOHdr("ExchangeRate") = "0"
            drSOHdr("CurrencyCode") = clsAdmin.CurrencyCode
            drSOHdr("TaxAmount") = IIf(dsScan.Tables("ItemScanDetails").Compute("Sum(TotalTaxAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("Sum(TotalTaxAmt)", ""))

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
            drSOHdr("PromisedDeliveryDate") = CtrlSalesInfo1.CtrlDtExpDelDate.Value
            drSOHdr("ActualDeliveryDate") = CtrlSalesInfo1.CtrlDtExpDelDate.Value

            'vSalesOrderExpectedDeliveryDate = drSOHdr("ActualDeliveryDate")
            vSalesOrderExpectedDeliveryDate = CtrlSalesInfo1.CtrlDtExpDelDate.Value
            'If IIf(IsDBNull(CtrlCustDtls1.cboAddrType.SelectedValue), "1", (CtrlCustDtls1.cboAddrType.SelectedValue)) = 99 Then
            '    drSOHdr("OtherDeliveryAdd") = CtrlCustDtls1.lblAddressValue.Text.Trim & ", " & CtrlCustDtls1.lblEmailIdValue.Value.Trim & ", " & CtrlCustDtls1.lblTelNoValue.Value.Trim
            '    drSOHdr("DeliveryAtCustAddressType") = 0
            'Else
            '    drSOHdr("DeliveryAtCustAddressType") = IIf(IsDBNull(CtrlCustDtls1.cboAddrType.SelectedValue), "1", (CtrlCustDtls1.cboAddrType.SelectedValue))
            'End If
            drSOHdr("OtherDeliveryAdd") = CtrlCustDtls1.lblAddressValue.Text.Trim & ", " & CtrlCustDtls1.lblEmailIdValue.Value.ToString().Trim & ", " & CtrlCustDtls1.lblTelNoValue.Value.ToString().Trim
            drSOHdr("DeliveryAtCustAddressType") = IIf(IsDBNull(CtrlCustDtls1.cboAddrType.SelectedValue), "0", (CtrlCustDtls1.cboAddrType.SelectedValue))

            drSOHdr("CustomerOrderRef") = IIf(CtrlSalesInfo1.CtrlTxtCustOrdRef.Text.Trim = "", DBNull.Value, CtrlSalesInfo1.CtrlTxtCustOrdRef.Text.Trim)
            drSOHdr("Remarks") = IIf(CtrlSalesInfo1.CtrlTxtRemarks.Text.Trim = "", DBNull.Value, CtrlSalesInfo1.CtrlTxtRemarks.Text.Trim)

            drSOHdr("InvoiceCustName") = IIf(CtrlSalesInfo1.CtrlTxtInvoice.Text.Trim = "", CtrlCustDtls1.lblCustNameValue.Text.Trim, CtrlSalesInfo1.CtrlTxtInvoice.Text.Trim)
            drSOHdr("AuthUserId") = vAuthUserId
            drSOHdr("AuthUserRemarks") = vAuthUserRemarks

            If CDbl(CtrlCashSummary1.lbltxt7) = 0 AndAlso Val(lblPickupQty.Text) = Val(lblOrderQty.Text) Then
                drSOHdr("SOStatus") = "Closed"
                drSOHdr("CLPPoints") = CDbl(dsScan.Tables("ItemScanDetails").Compute("Sum(CLPPoints)", ""))
                drSOHdr("CLPDiscount") = CDbl(dsScan.Tables("ItemScanDetails").Compute("Sum(CLPDiscount)", ""))

            Else
                drSOHdr("SOStatus") = "Open"
                drSOHdr("CLPPoints") = 0
                drSOHdr("CLPDiscount") = 0

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

            If IsNewRow = True Then
                dsMain.Tables("SalesOrderHDR").Rows.Add(drSOHdr)
                IsNewRow = False
            End If

            Return True
        Catch ex As Exception
            IsNewRow = False
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
                Dim billLineNo As Integer = dsMain.Tables("SalesOrderDTL").Rows.Count + 1
                findKey(0) = vSiteCode
                findKey(1) = clsAdmin.Financialyear
                findKey(2) = CtrlSalesInfo1.CtrlTxtOrderNo.Value
                findKey(3) = drScan("EAN").ToString
                findKey(4) = billLineNo
                drSODtl = dsMain.Tables("SalesOrderDTL").Rows.Find(findKey)

                If drSODtl Is Nothing Then
                    drSODtl = dsMain.Tables("SalesOrderDTL").NewRow()
                    IsNewRow = True
                End If
                'drSODtl("BatchBarcode") = drScan("BatchBarcode")
                drSODtl("BillLineNo") = billLineNo
                drSODtl("RowIndex") = drScan("RowIndex")
                '---- NEED to put BillNo in scan table to for print 
                drScan("BillLineNo") = billLineNo

                drSODtl("SaleOrderNumber") = CtrlSalesInfo1.CtrlTxtOrderNo.Value
                drSODtl("SiteCode") = vSiteCode
                drSODtl("DeliverySiteCode") = drScan("DeliverySiteCode")
                drSODtl("FinYear") = clsAdmin.Financialyear
                drSODtl("EAN") = drScan("EAN")
                drSODtl("ArticleCode") = drScan("ArticleCode")
                drSODtl("BatchBarcode") = drScan("BatchBarcode")
                drSODtl("PromisedDeliveryDate") = CtrlSalesInfo1.CtrlDtExpDelDate.Value
                drSODtl("ActualDeliveryDate") = drScan("ExpDelDate")
                'drSODtl("SalesStaffID") = IIf(CtrlSalesPersons.CtrlSalesPersons.SelectedValue = String.Empty, "", CtrlSalesPersons.CtrlSalesPersons.SelectedValue)

                drSODtl("SellingPrice") = drScan("SellingPrice")
                drSODtl("Quantity") = drScan("Quantity")
                drSODtl("DeliveredQty") = drScan("PickUpQty")
                drSODtl("Delivered_Qty") = drScan("PickUpQty")
                'If Not drScan("ReservedQty") Is DBNull.Value AndAlso drScan("ReservedQty") = True AndAlso drScan("Quantity") > drScan("PickUpQty") Then
                If Not drScan("ReservedQty") Is DBNull.Value AndAlso drScan("ReservedQty") = True Then
                    Dim otherSiteDeliveryQty As Decimal = SoDeliveryInfo.Sum(Function(x) IIf(x.ArticleCode = drScan("ArticleCode").ToString(), x.Quantity, 0))
                    drSODtl("ReservedQty") = CDbl(drScan("Quantity")) - CDbl(drScan("PickUpQty")) - otherSiteDeliveryQty
                    drSODtl("Reserved_Qty") = CDbl(drScan("Quantity")) - CDbl(drScan("PickUpQty")) - otherSiteDeliveryQty
                Else
                    drSODtl("ReservedQty") = 0
                    drSODtl("Reserved_Qty") = 0
                End If

                drSODtl("CostAmount") = drScan("CostAmount") 'DBNull.Value
                drSODtl("GrossAmount") = Math.Round(drScan("GrossAmt"), 3)
                drSODtl("NetAmount") = Math.Round(drScan("NetAmount"), 3)

                If Val(lblOrderQty.Text) = Val(lblPickupQty.Text) Then
                    drSODtl("ArticleStatus") = "Closed"
                    strSOStatus = "Closed"
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

                drSODtl("LineDiscount") = drScan("LineDiscount")
                drSODtl("DiscountAmount") = CDbl(IIf(drScan("LineDiscount") Is DBNull.Value, 0, drScan("LineDiscount"))) + CDbl(IIf(drSODtl("CLPDiscount") Is DBNull.Value, 0, drSODtl("CLPDiscount")))
                drSODtl("DiscountPercentage") = drScan("TotalDiscPercentage")

                drSODtl("UnitofMeasure") = IIf(drScan("UOM") Is DBNull.Value, 0, drScan("UOM"))
                drSODtl("ExclTaxAmt") = drScan("ExclTaxAmt")
                'drSODtl("TotalTaxAmount") = Math.Round(IIf(drScan("ExclTaxAmt") Is DBNull.Value, 0, drScan("ExclTaxAmt")) + IIf(drScan("IncTaxAmt") Is DBNull.Value, 0, drScan("IncTaxAmt")), 3)
                drSODtl("TotalTaxAmount") = Math.Round(IIf(drScan("TotalTaxAmt") Is DBNull.Value, 0, drScan("TotalTaxAmt")), 3)
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
                item.SalesOrderNumber = CtrlSalesInfo1.CtrlTxtOrderNo.Value
            Next
            Return True
        Catch ex As Exception
            IsNewRow = False
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
                        vSalesInvcNo = GenDocNo("CM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, objComn.getDocumentNo("CM", clsAdmin.SiteCode))
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
                        vSalesInvcNo = GenDocNo("OCM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, objComn.getDocumentNo("CM", clsAdmin.SiteCode))
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
                    findKey(1) = clsAdmin.Financialyear
                    findKey(2) = CtrlSalesInfo1.CtrlTxtOrderNo.Value
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
                    drInvc("DocumentNumber") = CtrlSalesInfo1.CtrlTxtOrderNo.Value
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
                    drInvc("RefNo_4") = DBNull.Value
                    drInvc("RefDate") = drPayment("date")
                    'drInvc("RefDate") = vCurrentDate

                    drInvc("CREATEDAT") = vSiteCode
                    drInvc("CREATEDBY") = clsAdmin.UserCode 'vUserName
                    drInvc("CREATEDON") = objComn.GetCurrentDate
                    drInvc("UPDATEDAT") = vSiteCode
                    drInvc("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                    drInvc("UPDATEDON") = objComn.GetCurrentDate
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

            objComn.PrepareCreditCheckData(dsMain, vSiteCode, vUserName, clsAdmin.Financialyear, vDocType, vSalesInvcNo, CtrlSalesInfo1.CtrlTxtOrderNo.Value, objComn.GetCurrentDate, _dDueDate, _strRemarks, "SO", clsAdmin.DayOpenDate)
            objComn.AddMode(dsMain.Tables("CheckDtls"))

            Return True

        Catch ex As Exception
            IsNewRow = False
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
                        findKey(2) = CtrlSalesInfo1.CtrlTxtOrderNo.Value
                        findKey(3) = RowNo

                        drOtherTax = dsMain.Tables("SalesOrderOtherCharges").Rows.Find(findKey)
                        If drOtherTax Is Nothing Then
                            drOtherTax = dsMain.Tables("SalesOrderOtherCharges").NewRow()
                            IsNewRow = True
                        End If

                        drOtherTax("SiteCode") = vSiteCode
                        drOtherTax("FinYear") = clsAdmin.Financialyear
                        drOtherTax("SaleOrderNumber") = CtrlSalesInfo1.CtrlTxtOrderNo.Value
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
            drOrderHdr("ExpectedDeliveryDt") = CtrlSalesInfo1.CtrlDtExpDelDate.Value

            drOrderHdr("PaymentAfterDelDays") = DBNull.Value
            drOrderHdr("AdditionalTermsNConditions") = DBNull.Value
            drOrderHdr("AdditionalPaymentTerms") = DBNull.Value
            drOrderHdr("DocNetValue") = CDbl(CtrlCashSummary1.lbltxt4)
            drOrderHdr("DocGrossValue") = CDbl(CtrlCashSummary1.lbltxt1.Trim)
            drOrderHdr("IsClosed") = True
            drOrderHdr("IsFreezed") = True

            drOrderHdr("ReturnReasonCode") = " "
            drOrderHdr("Remarks") = CtrlSalesInfo1.CtrlTxtRemarks.Text.Trim
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
                        findKey(4) = rowIndex

                        drOrderDtl = dsMain.Tables("OrderDtl").Rows.Find(findKey)
                        If drOrderDtl Is Nothing Then
                            drOrderDtl = dsMain.Tables("OrderDtl").NewRow
                            IsNewRow = True
                        End If

                        drOrderDtl("SiteCode") = vSiteCode
                        drOrderDtl("FinYear") = clsAdmin.Financialyear
                        drOrderDtl("DocumentNumber") = dsMain.Tables("OrderHdr").Rows(0)("DocumentNumber")
                        drOrderDtl("ArticleCode") = drScan("ArticleCode")
                        drOrderDtl("EAN") = drScan("EAN")
                        drOrderDtl("LineNumber") = rowIndex
                        drOrderDtl("Qty") = drScan("Quantity")
                        drOrderDtl("UnitofMeasure") = drScan("UOM")
                        drOrderDtl("BarCode") = drScan("BatchBarCode")
                        drOrderDtl("OpenQty") = DBNull.Value
                        drOrderDtl("DeliveredQty") = drScan("PickUpQty")
                        drOrderDtl("DeliveryCompleted") = DBNull.Value
                        drOrderDtl("PurchaseGroupCode") = DBNull.Value
                        drOrderDtl("RefDocument") = DBNull.Value
                        drOrderDtl("RefDocumentNo") = DBNull.Value

                        drOrderDtl("PONo") = DBNull.Value
                        drOrderDtl("BirthListId") = DBNull.Value
                        drOrderDtl("SaleOrderNumber") = CtrlSalesInfo1.CtrlTxtOrderNo.Value
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
                        findKey(4) = rowIndex

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
                        drOrderDtl("LineNumber") = rowIndex
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
                        drOrderDtl("SaleOrderNumber") = CtrlSalesInfo1.CtrlTxtOrderNo.Value
                        drOrderDtl("AllocationRule") = DBNull.Value
                        'drOrderDtl("MRP") = drScan("SellingPrice")
                        drOrderDtl("MRP") = Math.Round(itemobj("NetAmount") / itemobj("Quantity"), clsDefaultConfiguration.DecimalPlaces)
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
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function


    Private Function PrepareClpHdrDataforSave(ByRef dsMain As DataSet) As Boolean
        Dim drClpHdr As DataRow
        Try
            drClpHdr = dsMain.Tables("CLPTransaction").NewRow

            drClpHdr("SiteCode") = vSiteCode
            drClpHdr("BillNo") = CtrlSalesInfo1.CtrlTxtOrderNo.Value
            drClpHdr("BillDate") = objComn.GetCurrentDate().Date
            drClpHdr("AccumLationPoints") = TotalPoints
            drClpHdr("RedemptionPoints") = 0
            drClpHdr("BalAccumlationPoints") = TotalPoints
            drClpHdr("ClpProgramId") = vClpProgramId
            drClpHdr("ClpCustomerId") = CtrlCustSearch1.CtrlTxtCustNo.Text.Trim
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
                drClpDtl("BillNo") = CtrlSalesInfo1.CtrlTxtOrderNo.Value
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
            'PrintSalesOrder.Append("Sales Order No              : " & CtrlSalesInfo1.CtrlTxtOrderNo.Value & "                    Date    : " & Format(vCurrentDate.Date, vDateFormat) & vbCrLf)
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
            Dim strRemark As String = CtrlSalesInfo1.CtrlTxtRemarks.Text
            Dim strInvoiceTo As String = CtrlSalesInfo1.CtrlTxtInvoice.Text
            SalesPersonName = IIf(CtrlSalesPersons.CtrlSalesPersons.SelectedIndex < 0, "", CtrlSalesPersons.CtrlSalesPersons.Text)
            Dim dsOtherCharges As New DataSet
            Dim dt As DataTable = dtOtherCharges.Copy()
            dsOtherCharges.Tables.Add(dt)
            dt.TableName = "NewOtherCharges"


            'changes to show credit sale to be adjusted in sales invoice
            Dim crdsale As Double = 0
            Dim crdsaleadjustamount As Double = 0
            Dim salesordernumber As String = CtrlSalesInfo1.CtrlTxtOrderNo.Value


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
            '==============================================

            If Not dsPayment Is Nothing AndAlso dsPayment.Tables.Contains("MSTRecieptType") Then
                If clsDefaultConfiguration.PrintFormatNo = "8" Then
                    'code added by irfan for cakeology on 4/10/2017
                    Dim objt As New clsCashMemoPrint(salesordernumber, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving)
                    objt.PrintFormatNo = clsDefaultConfiguration.PrintFormatNo
                    objt.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "SO", "", "", "", "", "", Obj.TotalBalAmt, objPaymentByCash.TotalMinimumAmount, clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6)   'code added by irfan on 9/9/2017 visiblity of hsn and tax
                    '------------------------------------------------------------------------------------------------

                Else
                    'Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserCode, CtrlSalesInfo1.CtrlTxtOrderNo.Value, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo1.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, "Open", dsOtherCharges, ShowFullName:=clsDefaultConfiguration.PrintItemFullName, dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, SalesOrderDeliveryDate:=vSalesOrderExpectedDeliveryDate, strSalesPerson:=SalesPersonName)
                    Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, CtrlSalesInfo1.CtrlTxtOrderNo.Value, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo1.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, status, dsOtherCharges, ShowFullName:=clsDefaultConfiguration.PrintItemFullName, strReturnReason:=strRemark, dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, SalesOrderDeliveryDate:=vSalesOrderExpectedDeliveryDate, strSalesPerson:=SalesPersonName, CreditSale:=crdsale, strInvoiceTo:=strInvoiceTo)
                End If
            Else
                If clsDefaultConfiguration.PrintFormatNo = "8" Then
                    'code added by irfan for cakeology on 4/10/2017
                    Dim objt As New clsCashMemoPrint(salesordernumber, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving)
                    objt.PrintFormatNo = clsDefaultConfiguration.PrintFormatNo
                    objt.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "SO", "", "", "", "", "", Obj.TotalBalAmt, objPaymentByCash.TotalMinimumAmount, clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6)   'code added by irfan on 9/9/2017 visiblity of hsn and tax
                    '------------------------------------------------------------------------------------------------

                Else
                    'Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserCode, CtrlSalesInfo1.CtrlTxtOrderNo.Value, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), Nothing, CtrlSalesInfo1.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, "Open", dsOtherCharges, ShowFullName:=clsDefaultConfiguration.PrintItemFullName, dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, SalesOrderDeliveryDate:=vSalesOrderExpectedDeliveryDate, strSalesPerson:=SalesPersonName)
                    Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, CtrlSalesInfo1.CtrlTxtOrderNo.Value, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), Nothing, CtrlSalesInfo1.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, status, dsOtherCharges, ShowFullName:=clsDefaultConfiguration.PrintItemFullName, strReturnReason:=strRemark, dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, SalesOrderDeliveryDate:=vSalesOrderExpectedDeliveryDate, strSalesPerson:=SalesPersonName, CreditSale:=crdsale, strInvoiceTo:=strInvoiceTo)
                End If
            End If

        Catch ex As Exception
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
            '    PrintInvoice.Append("                   Sales Invoice No  - " & CtrlSalesInfo1.CtrlTxtOrderNo.Value & vbCrLf & vbCrLf)

            '    PrintInvoice.Append("Cashier Name  : " & vUserName & vbTab & vbTab & vbTab & "Invoice Date : " & Format(vCurrentDate.Date, vDateFormat) & vbCrLf)
            '    PrintInvoice.Append("Sales Order   : " & CtrlSalesInfo1.CtrlTxtOrderNo.Value & "   		Sales Date   : " & Format(vCurrentDate.Date, vDateFormat) & vbCrLf)
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
            Dim strRemark As String = CtrlSalesInfo1.CtrlTxtRemarks.Text

            Dim dsOtherCharges As New DataSet
            Dim dt As DataTable = dtOtherCharges.Copy()
            dt.TableName = "NewOtherCharges"
            dsOtherCharges.Tables.Add(dt)
            'Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Payment, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserCode, CtrlSalesInfo1.CtrlTxtOrderNo.Value, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo1.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, "Open", dsOtherCharges, "", dtSalesOrderTaxDetails, ShowFullName:=clsDefaultConfiguration.PrintItemFullName, dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl)
            Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Payment, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserCode, CtrlSalesInfo1.CtrlTxtOrderNo.Value, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), CtrlSalesInfo1.CtrlTxtCustOrdRef.Text, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, strSOStatus, dsOtherCharges, "", dtSalesOrderTaxDetails, ShowFullName:=clsDefaultConfiguration.PrintItemFullName, dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, SalesOrderDeliveryDate:=vSalesOrderExpectedDeliveryDate)

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Function
    Private Function PrintSalesOrdersInvoiceBackup(ByVal drSite As DataRow, ByVal drHAdds As DataRow, Optional ByVal drDAdds As DataRow = Nothing) As Boolean
        Dim PrintInvoice As New System.Text.StringBuilder

        Try
            If dsScan Is Nothing Then
                Exit Function
            End If

            PrintInvoice.Length = 0
            PrintInvoice.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)
            PrintInvoice.Append("                          SALES INVOICE                                 " & vbCrLf)
            PrintInvoice.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)

            If vHeaderNote = True Then
                PrintInvoice.Append(vbCrLf & "Header Information is Welcome" & vbCrLf)
            End If

            PrintInvoice.Append("Company Name   : " & "CreativeIT India Ltd" & "             Company Code : " & "CRITI02" & vbCrLf)

            If vIsPrintOfficialAddressAllowed = False Then
                If Not (drSite Is Nothing) Then
                    PrintInvoice.Append("Store Name     : " & drSite.Item("SiteOfficialName") & "                Store Code   : " & vSiteCode & vbCrLf)
                    PrintInvoice.Append("Store Address  : " & drSite.Item("SiteAddressLn1") & vbCrLf & "                 " & _
                                 drSite.Item("SiteAddressLn2") & " " & drSite.Item("SiteAddressLn3") & _
                                 drSite.Item("SitePinCode") & vbCrLf)
                End If
            Else
                PrintInvoice.Append(vbCrLf & "Print Official Address " & vbCrLf)
            End If

            PrintInvoice.Append(vbCrLf & "----------------------------------------------------------------------------------------------------" & vbCrLf)
            PrintInvoice.Append("Sales Invoice No        : " & CtrlSalesInfo1.CtrlTxtOrderNo.Value & "  Reference Sales Order : " & CtrlSalesInfo1.CtrlTxtOrderNo.Value & "   Date : " & Format(vCurrentDate.Date, vDateFormat) & vbCrLf)
            PrintInvoice.Append("Expected Delivery Date  : " & Format(CtrlSalesInfo1.CtrlDtExpDelDate.Value, vDateFormat) & vbCrLf)
            PrintInvoice.Append("Cashier Name            : " & vUserName & vbCrLf & vbCrLf)

            PrintInvoice.Append("Customer Name    : " & IIf(CtrlSalesInfo1.CtrlTxtInvoice.Text.Trim = "", CtrlCustDtls1.lblCustNameValue.Text.Trim, CtrlSalesInfo1.CtrlTxtInvoice.Text.Trim) & "          Customer Code : " & CtrlCustSearch1.CtrlTxtCustNo.Text & vbCrLf)

            PrintInvoice.Append("Home Address     : " & drHAdds.Item("Address").ToString & vbCrLf)
            PrintInvoice.Append("                 : " & drHAdds.Item("City") & ", " & drHAdds.Item("State") & ", " & drHAdds.Item("Country") & ", " & drHAdds.Item("PinCode") & vbCrLf & vbCrLf)

            If drDelvAdds Is Nothing Then
                PrintInvoice.Append("Delivery Address : " & CtrlCustDtls1.lblAddressValue.Text & vbCrLf)
                PrintInvoice.Append("Tel. No.  	 : " & CtrlCustDtls1.lblTelNoValue.Text & vbCrLf & vbCrLf)
            Else
                PrintInvoice.Append("Delivery Address : " & drDAdds.Item("Address").ToString & vbCrLf)
                PrintInvoice.Append("                 : " & drDAdds.Item("City") & ", " & drDAdds.Item("State") & ", " & drDAdds.Item("Country") & ", " & drDAdds.Item("PinCode") & vbCrLf)
                PrintInvoice.Append("Tel. No.  	 : " & drDAdds.Item("OfficeNo") & vbCrLf & vbCrLf)
            End If

            PrintInvoice.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)
            PrintInvoice.Append("Item Code       Item Desc                               Qty       Price    Disc%        Tax%  NetAmt " & vbCrLf)
            PrintInvoice.Append("----------------------------------------------------------------------------------------------------" & vbCrLf)

            For Each drDtl As DataRow In dsScan.Tables("ItemScanDetails").Rows
                PrintInvoice.Append(drDtl("ArticleCode") & Space(16 - drDtl("ArticleCode").ToString.Length) & _
                             drDtl("Discription") & Space(40 - drDtl("Discription").ToString.Length) & _
                             drDtl("Quantity") & Space(10 - drDtl("Quantity").ToString.Length) & _
                             Format(drDtl("SellingPrice"), "0.0") & Space(10 - drDtl("SellingPrice").ToString.Length) & _
                             drDtl("Discount") & Space(13 - drDtl("Discount").ToString.Length) & _
                             IIf(drDtl("ExclTaxAmt") Is DBNull.Value, "0", drDtl("ExclTaxAmt")) & Space(5 - drDtl("ExclTaxAmt").ToString.Length) & FormatNumber(drDtl("NetAmount"), 1) & vbCrLf)
            Next

            PrintInvoice.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)

            PrintInvoice.Append("Total Qty   : " & lblOrderQty.Text & vbCrLf)
            PrintInvoice.Append("Gross Amt   : " & CtrlCashSummary1.lbltxt1 & vbCrLf)
            PrintInvoice.Append("Disc  Amt   : " & CtrlCashSummary1.CtrllblTaxAmt.Text & vbCrLf)
            PrintInvoice.Append("Incl. Amt   : " & FormatNumber(IIf(dsScan.Tables("ItemScanDetails").Compute("Sum(IncTaxAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("Sum(IncTaxAmt)", ""))), strZero & vbCrLf)
            PrintInvoice.Append("Excl. Amt   : " & FormatNumber(IIf(dsScan.Tables("ItemScanDetails").Compute("Sum(IncTaxAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("Sum(IncTaxAmt)", ""))), strZero & vbCrLf)
            PrintInvoice.Append("Net   Amt   : " & CtrlCashSummary1.lbltxt4 & vbCrLf & vbCrLf)

            PrintInvoice.Append("Payment Received :" & vbCrLf)
            PrintInvoice.Append("Minimum Advance To Pay: (INR)  " & CtrlCashSummary1.lbltxt5 & vbCrLf & vbCrLf)

            PrintInvoice.Append("Advance Amount Paid :" & vbCrLf)


            If dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                Dim RowIndexPayment As Integer = 0
                For Each drPayment As DataRow In dsPayment.Tables("MSTRecieptType").Rows
                    PrintInvoice.Append("Tender      :" & drPayment("RecieptType").ToString() & vbCrLf)
                    If RowIndexPayment = 0 Then
                        PrintInvoice.Append("Tender Info :" & drPayment("Amount") & "  Date : " & Format(vCurrentDate, vDateFormat) & vbCrLf)
                    End If
                    If RowIndexPayment > 0 Then
                        PrintInvoice.Append("Tender Info :" & drPayment("Amount") & vbCrLf)
                    End If
                    RowIndexPayment += 1
                Next

            End If

            PrintInvoice.Append(vbCrLf & "Total Paid Amount:" & vbCrLf)
            PrintInvoice.Append("Tender      :" & vbCrLf)
            If dsMain.Tables("SalesInvoice").Rows.Count > 0 Then
                PrintInvoice.Append("Tender Info :" & dsMain.Tables("SalesInvoice").Compute("Sum(AmountTendered)", "") & "  Date : " & dsMain.Tables("SalesInvoice").Rows(0).Item("CREATEDON") & vbCrLf)
                PrintInvoice.Append("Tender Info :" & vbCrLf)
            Else
                PrintInvoice.Append("Tender Info :" & CtrlCashSummary1.lbltxt5 & vbCrLf)
            End If

            If vIsPrintingTaxInfoAllowed = True Then
                PrintInvoice.Append("Print Tax Details..............." & vbCrLf & vbCrLf)
            End If

            PrintInvoice.Append("Balance Amount Due: " & CtrlCashSummary1.lbltxt7 & vbCrLf & vbCrLf)

            PrintInvoice.Append("<Terms & Condition>" & vbCrLf & vbCrLf)

            PrintInvoice.Append("Authorized Sign:...............            Customer Sign:................" & vbCrLf)

            If vIsPromotionalMessageAllowed = True Then
                PrintInvoice.Append(vbCrLf & "Promotional Message is Welcome" & vbCrLf)
            End If
            If vFooterNote = True Then
                PrintInvoice.Append(vbCrLf & "Footer Information is Welcome" & vbCrLf)
            End If

            If vIsPrintPreviewAllowed = True Then
                fnPrint(PrintInvoice.ToString, "")          'Print Preview
            End If
            fnPrint(PrintInvoice.ToString, "PRN")       'Direct Print

            'PrintInvoice.Append("Print")                'Set Debug Point

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

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
            '    PrintDeliveryInvc.Append("Sales Invoice No        : " & vSalesInvcNo & "  Reference Sales Order : " & CtrlSalesInfo1.CtrlTxtOrderNo.Value & "   Date : " & Format(vCurrentDate, vDateFormat) & vbCrLf)
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
            Dim strRemark As String = CtrlSalesInfo1.CtrlTxtRemarks.Text
            Dim strInvoiceTo As String = CtrlSalesInfo1.CtrlTxtInvoice.Text
            ' Dim strCustomerOrdRef As String = CtrlSalesInfo1.CtrlTxtCustOrdRef.Text   
            SalesPersonName = IIf(CtrlSalesPersons.CtrlSalesPersons.SelectedIndex < 0, "", CtrlSalesPersons.CtrlSalesPersons.Text)
            Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(vIsPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.DeliveryNote, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, CtrlSalesInfo1.CtrlTxtOrderNo.Value, CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), Nothing, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, ShowFullName:=clsDefaultConfiguration.PrintItemFullName, strReturnReason:=strRemark, dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, SalesOrderDeliveryDate:=vSalesOrderExpectedDeliveryDate, strSalesPerson:=SalesPersonName, strInvoiceTo:=strInvoiceTo)
           
        Catch ex As Exception
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
    Private Sub BtnSONew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbbtnSONew.Click '  BtnSONew.Click
        If rbbtnSONew.Tag = "NEW" Then
            Try
                'EnableButton(True)
                'rbbtnSONew.Tag = "Cancel"
                'ResetSalesOrder()
                'CtrlSalesInfo1.CtrlTxtOrderNo.Value = "SO" & clsAdmin.TerminalID & objComn.getDocumentNo("SalesOrder")
                ''dtpExpDeliveryDate.Value = consDeveliveryDt
                'CtrlSalesPersons.CtrlTxtBox.Select()
                If grdScanItem.Rows.Count > 1 Then
                    If MsgBox(getValueByKey("SO057"), MsgBoxStyle.YesNo, "SO057 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                        EnableButton(False)
                        rbbtnSONew.Tag = "NEW"
                        CtrlSalesInfo1.CtrlTxtOrderNo.Value = 0
                        'dtpExpDeliveryDate.Value = consDeveliveryDt
                        ResetSalesOrder()

                    End If
                Else
                    EnableButton(True)
                    rbbtnSONew.Tag = "NEW"
                    CtrlSalesInfo1.CtrlTxtOrderNo.Value = 0
                    'dtpExpDeliveryDate.Value = consDeveliveryDt
                    ResetSalesOrder()
                End If
                IsCSTApplicable = False
                rbnCST.Enabled = True
                clsDefaultConfiguration.CSTTaxCode = ""

                GetNewSalesOrderNumber()

                DtSoBulkComboHdr.Rows.Clear()
                DtSoBulkComboDtl.Rows.Clear()

                'If OnlineConnect = True Then
                '    Try
                '        CtrlSalesInfo1.CtrlTxtOrderNo.Value = GenDocNo("SO" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode))
                '    Catch ex As Exception
                '        CtrlSalesInfo1.CtrlTxtOrderNo.Value = "SO" & clsAdmin.TerminalID & objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode)
                '    End Try
                'Else
                '    Try
                '        CtrlSalesInfo1.CtrlTxtOrderNo.Value = GenDocNo("OSO" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode))
                '    Catch ex As Exception
                '        CtrlSalesInfo1.CtrlTxtOrderNo.Value = "OSO" & clsAdmin.TerminalID & objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode)
                '    End Try
                'End If

            Catch ex As Exception
                ShowMessage(ex.Message, getValueByKey("CLAE05"))
            End Try

        ElseIf rbbtnSONew.Tag.Trim = "Cancel" Then
            Try
                If Not (dsScan.Tables(0).Rows.Count > 0) Then
                    'ShowMessage(getValueByKey("SO012"), "SO012")
                    'ShowMessage("Please Scan Articles first", "Sales Order Information")

                End If
                If grdScanItem.Rows.Count > 1 Then
                    If MsgBox(getValueByKey("SO057"), MsgBoxStyle.YesNo, "SO057 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                        EnableButton(False)
                        rbbtnSONew.Tag = "NEW"
                        CtrlSalesInfo1.CtrlTxtOrderNo.Value = 0
                        'dtpExpDeliveryDate.Value = consDeveliveryDt
                        ResetSalesOrder()
                    End If
                Else
                    EnableButton(True)
                    rbbtnSONew.Tag = "NEW"
                    CtrlSalesInfo1.CtrlTxtOrderNo.Value = 0
                    'dtpExpDeliveryDate.Value = consDeveliveryDt
                    ResetSalesOrder()
                End If

            Catch ex As Exception
                ShowMessage(ex.Message, getValueByKey("CLAE05"))
                LogException(ex)
            End Try
        End If

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
            grdScanItem.FinishEditing()

            If CtrlCustSearch1.CtrlTxtCustNo.Text = String.Empty Then
                ShowMessage(getValueByKey("SO048"), "SO048 - " & getValueByKey("CLAE04"))
                CtrlCustSearch1.CtrlTxtCustNo.Select()
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
            rbbtnDefaultPromo_Click(rbbtnDefaultPromo, New System.EventArgs)
            Dim Obj As New frmNHowMuchtoPay
            Obj.CtrlTxtMinAmt.Text = CDbl(CtrlCashSummary1.lbltxt5)
            Obj.CtrlTxtPickAmt.Text = Math.Round(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "")), 2)
            Obj.CtrlTxtPickAmt.Text = MyRound(CDbl(Obj.CtrlTxtPickAmt.Text), clsDefaultConfiguration.BillRoundOffAt)
            Obj.ctrlTxtHowMuchPay.Text = CDbl(CtrlCashSummary1.lbltxt5)
            Obj.TotalBalAmt = CDbl(CtrlCashSummary1.lbltxt4)
            Obj.ShowDialog(Me)

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
            Dim objPayment As New frmNAcceptPayment()
            getclpsettings()
            objPayment.ParentRelation = "SalesOrder"
            objPayment.TotalBillAmount = CDbl(CtrlCashSummary1.lbltxt4)
            'objPayment.MinimumBillAmount = CDbl(CtrlCashSummary1.lbltxt5)
            objPayment.MinimumBillAmount = CDbl(Obj.CtrlTxtMinAmt.Text)
            objPayment.TotalPick = CDbl(CtrlCashSummary1.lbltxt5)
            objPayment.IsChangeTender = True
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

            If CtrlCustSearch1.CtrlTxtCustNo.Text <> String.Empty Then
                objPayment.CLPCustomerCardNumber = CtrlCustSearch1.CtrlTxtCustNo.Text
            End If

            objPayment.PaymentType = clsAcceptPayment.PaymentType.Advance
            objPayment.RoundAt = clsDefaultConfiguration.BillRoundOffAt
            objPayment.ShowDialog(Me)

            _dsPayment = New DataSet
            _dsPayment = objPayment.ReciptTotalAmount()

            If dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables(0).Rows.Count > 0 Then
                objPayment.Close()
                CalculateSalesOrderSummary(dsScan)
            End If



            If objPayment.Action = "Save" Then
                ' reset the mininum amount add on 11/11/2009 by ram
                CtrlCashSummary1.lbltxt5 = Obj.CtrlTxtMinAmt.Text
                'MsgBox(objPayment.intReturnCashToCust)
                ' end of reset

                'Added by Rohit for CR5938
                _dDueDate = objPayment.dDueDate
                _strRemarks = objPayment.strRemarks

                BtnSaveSalesOrder_Click(sender, e)
            End If

            If objPayment.Action = "Gift" Then
                ' reset the mininum amount add on 11/11/2009 by ram
                CtrlCashSummary1.lbltxt5 = Obj.CtrlTxtMinAmt.Text
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
            grdScanItem.FinishEditing()

            If CtrlCustSearch1.CtrlTxtCustNo.Text = String.Empty Then
                ShowMessage(getValueByKey("SO048"), "SO048 - " & getValueByKey("CLAE04"))
                CtrlCustSearch1.CtrlTxtCustNo.Select()
                Exit Sub
            End If

            If Not (dsScan.Tables(0).Rows.Count > 0) Then
                'ShowMessage("Please Scan Article first", "Sales Order Information")
                ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                Exit Sub
            ElseIf CDbl(CtrlCashSummary1.lbltxt4) <= Decimal.Zero Then
                ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            'Dim obj As New frmSpecialPrompt("What you want to pay")
            'obj.ShowTextBox = True
            'obj.ShowDialog()
            rbbtnDefaultPromo_Click(rbbtnDefaultPromo, New System.EventArgs)
            Dim Obj As New frmNHowMuchtoPay
            Obj.CtrlTxtMinAmt.Text = CDbl(CtrlCashSummary1.lbltxt5)
            Obj.CtrlTxtPickAmt.Text = MyRound(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "")), clsDefaultConfiguration.BillRoundOffAt)
            Obj.CtrlTxtPickAmt.Text = MyRound(CDbl(Obj.CtrlTxtPickAmt.Text), clsDefaultConfiguration.BillRoundOffAt)
            Obj.ctrlTxtHowMuchPay.Text = CDbl(CtrlCashSummary1.lbltxt5)
            Obj.TotalBalAmt = CDbl(CtrlCashSummary1.lbltxt4)
            Obj.ShowDialog()

            If Not Obj Is Nothing Then
                CtrlCashSummary1.lbltxt5 = Obj.CtrlTxtMinAmt.Text
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
            objPaymentByCash.TotalBillAmount = CDbl(CtrlCashSummary1.lbltxt4)
            objPaymentByCash.TotalMinimumAmount = CDbl(CtrlCashSummary1.lbltxt5)

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
                    CtrlCashSummary1.lbltxt5 = Obj.CtrlTxtMinAmt.Text
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
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CM033"), "CM033 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in Updating cash payment data ", "Information")
        End Try
    End Sub
    Private Sub CreditSales(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            grdScanItem.FinishEditing()

            If CtrlCustSearch1.CtrlTxtCustNo.Text = String.Empty Then
                ShowMessage(getValueByKey("SO048"), "SO048 - " & getValueByKey("CLAE04"))
                CtrlCustSearch1.CtrlTxtCustNo.Select()
                Exit Sub
            End If

            If Not (dsScan.Tables(0).Rows.Count > 0) Then
                'ShowMessage("Please Scan Article first", "Sales Order Information")
                ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                Exit Sub
            ElseIf CDbl(CtrlCashSummary1.lbltxt4) <= Decimal.Zero Then
                ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            'Dim obj As New frmSpecialPrompt("What you want to pay")
            'obj.ShowTextBox = True
            'obj.ShowDialog()
            rbbtnDefaultPromo_Click(rbbtnDefaultPromo, New System.EventArgs)
            Dim Obj As New frmNHowMuchtoPay
            Obj.CtrlTxtMinAmt.Text = CDbl(CtrlCashSummary1.lbltxt5)
            Obj.CtrlTxtPickAmt.Text = MyRound(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "")), clsDefaultConfiguration.BillRoundOffAt)
            Obj.CtrlTxtPickAmt.Text = MyRound(CDbl(Obj.CtrlTxtPickAmt.Text), clsDefaultConfiguration.BillRoundOffAt)
            Obj.ctrlTxtHowMuchPay.Text = CDbl(CtrlCashSummary1.lbltxt5)
            Obj.TotalBalAmt = CDbl(CtrlCashSummary1.lbltxt4)
            Obj.ShowDialog()

            If Not Obj Is Nothing Then
                CtrlCashSummary1.lbltxt5 = Obj.CtrlTxtMinAmt.Text
                If Obj.blnAllowtoGoPaymentScreen = False Then
                    Exit Sub
                End If
            Else
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
            dsPayment("Amount") = CDbl(CtrlCashSummary1.lbltxt5)
            dsPayment("AmountInCurrency") = CDbl(CtrlCashSummary1.lbltxt5)
            dsPayment("Number") = "Rs. " & CDbl(CtrlCashSummary1.lbltxt5)
            dsPayment("Date") = clsAdmin.CurrentDate.Date
            dsPayment("RecieptTypeCode") = "Credit"
            dsPayment("ExchangeRate") = 1
            dsPayment("CurrencyCode") = "INR"
            dsPayment("RefNO_2") = CDbl(CtrlCashSummary1.lbltxt5)
            dsPayment("RefNO_3") = ""
            dsPayment("RefNO_4") = ""
            dsPayment("BankAccNo") = ""
            dsPayment("NOCLP") = False
            dsPayment("IssuedForCLP") = False
            _dsPayment.Tables("MSTRecieptType").Rows.Add(dsPayment)

            ' objPaymentByCash.Close()
            CalculateSalesOrderSummary(dsScan)

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
            grdScanItem.FinishEditing()

            If CtrlCustSearch1.CtrlTxtCustNo.Text = String.Empty Then
                ShowMessage(getValueByKey("SO048"), "SO048 - " & getValueByKey("CLAE04"))
                CtrlCustSearch1.CtrlTxtCustNo.Select()
                Exit Sub
            End If

            If Not (dsScan.Tables(0).Rows.Count > 0) Then
                'ShowMessage("Please Scan Article first", "Sales Order Information")
                ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                Exit Sub
            ElseIf CDbl(CtrlCashSummary1.lbltxt4) <= Decimal.Zero Then
                ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            rbbtnDefaultPromo_Click(rbbtnDefaultPromo, New System.EventArgs)
            Dim Obj As New frmNHowMuchtoPay
            Obj.CtrlTxtMinAmt.Text = CDbl(CtrlCashSummary1.lbltxt5)
            Obj.CtrlTxtPickAmt.Text = MyRound(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "")), clsDefaultConfiguration.BillRoundOffAt)
            Obj.CtrlTxtPickAmt.Text = MyRound(CDbl(Obj.CtrlTxtPickAmt.Text), clsDefaultConfiguration.BillRoundOffAt)
            Obj.ctrlTxtHowMuchPay.Text = CDbl(CtrlCashSummary1.lbltxt5)
            Obj.TotalBalAmt = CDbl(CtrlCashSummary1.lbltxt4)
            Obj.ShowDialog()

            If Not Obj Is Nothing Then
                CtrlCashSummary1.lbltxt5 = Obj.CtrlTxtMinAmt.Text
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
            Dim objPayment As New frmNAcceptPaymentByCard("SO")
            objPayment.TotalBillAmount = CDbl(CtrlCashSummary1.lbltxt4)
            objPayment.TotalMinAmount = CDbl(CtrlCashSummary1.lbltxt5)
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
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CM034"), "CM034 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in Updating card payment data ", "Information")
        End Try
    End Sub
    Private Sub BtnPayCheque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            grdScanItem.FinishEditing()
            If CtrlCustSearch1.CtrlTxtCustNo.Text = String.Empty Then
                ShowMessage(getValueByKey("SO048"), "SO048 - " & getValueByKey("CLAE04"))
                CtrlCustSearch1.CtrlTxtCustNo.Select()
                Exit Sub
            End If
            If Not (dsScan.Tables(0).Rows.Count > 0) Then
                'ShowMessage("Please Scan Article first", "Sales Order Information")
                ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                Exit Sub
            ElseIf CDbl(CtrlCashSummary1.lbltxt4) <= Decimal.Zero Then
                ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            rbbtnDefaultPromo_Click(rbbtnDefaultPromo, New System.EventArgs)
            Dim Obj As New frmNHowMuchtoPay
            Obj.CtrlTxtMinAmt.Text = CDbl(CtrlCashSummary1.lbltxt5)
            Obj.CtrlTxtPickAmt.Text = MyRound(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "") Is DBNull.Value, 0, dsScan.Tables("ItemScanDetails").Compute("SUM(TotalPickUpAmt)", "")), clsDefaultConfiguration.BillRoundOffAt)
            Obj.CtrlTxtPickAmt.Text = MyRound(CDbl(Obj.CtrlTxtPickAmt.Text), clsDefaultConfiguration.BillRoundOffAt)
            Obj.ctrlTxtHowMuchPay.Text = CDbl(CtrlCashSummary1.lbltxt5)
            Obj.TotalBalAmt = CDbl(CtrlCashSummary1.lbltxt4)
            Obj.ShowDialog()

            If Not Obj Is Nothing Then
                CtrlCashSummary1.lbltxt5 = Obj.CtrlTxtMinAmt.Text
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
            Dim objCheck As New frmNCheckPayment("SO")
            objCheck.BillAmount = CDbl(CtrlCashSummary1.lbltxt4)
            objCheck.TotalMinAmount = CDbl(CtrlCashSummary1.lbltxt5)
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
            End If
        Catch ex As Exception
            LogException(ex)
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
            obj.MainTable = "ItemScanDetails"
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
                            Dim p As Object = "Clear"
                            rbbtnClrAllPromo_Click("Clear", Nothing)
                            If StrQues.Contains("Autho") = True AndAlso StrQues.Contains("Voucher") = True Then
                                _dsScan.Tables(0).Columns("Discount").ColumnName = "TotalDiscount"
                                _dsScan.Tables(0).Columns("ExclTaxAmt").ColumnName = "EXCLUSIVETAX"
                                CheckInterTransactionAuth("ORD", _dsScan.Tables(0), 0, 0, 0, offerno)
                                _dsScan.Tables(0).Columns("TotalDiscount").ColumnName = "Discount"
                                _dsScan.Tables(0).Columns("EXCLUSIVETAX").ColumnName = "ExclTaxAmt"
                            ElseIf StrQues.Contains("Autho") = True Then
                                If CheckInterTransactionAuth("DAUTH", _dsScan.Tables(0)) = True Then
                                    obj.ApplySelectedPromotion(offerno, _dsScan, vSiteCode)
                                End If
                            End If
                        Else
                            obj.ApplySelectedPromotion(offerno, _dsScan, vSiteCode)
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
                'code is added by irfan on 24/04/2018 for mantis issue======================
                For Each drRem As DataRow In _dsScan.Tables(0).Rows
                    drRem("Discount") = 0
                    drRem("PromotionId") = 0
                    drRem("LineDiscount") = 0
                    ' drRem("TotalDiscPercentage") = 0
                    drRem("FirstLevel") = String.Empty
                    drRem("TopLevel") = String.Empty
                    drRem("TopLevelDisc") = 0
                Next
                '==========================================================================
                'ShowMessage("Default Schemes is applied Now", "Message")
                obj.CalculatedDs(_dsScan, vSiteCode)
            End If
            'Else
            '    ShowMessage(getValueByKey("SO022"), "SO022 - " & getValueByKey("CLAE04"))
            '    'ShowMessage("Default Schemes is applied Now", "Message")
            '    obj.CalculatedDs(_dsScan, vSiteCode)
            'End If

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
                        objcom.CreateDataSetForTaxCalculation(drDisc("ARTICLECODE"), totalamt, drDisc, dsMain, CtrlSalesInfo1.CtrlTxtOrderNo.Value, drDisc("EAN"), True)
                    End If

                    'drDisc("NetAmount") = drDisc("GrossAmt") - drDisc("LineDiscount") + IIf(drDisc("ExclTaxAmt") Is DBNull.Value, 0, drDisc("ExclTaxAmt")) + IIf(drDisc("IncTaxAmt") Is DBNull.Value, 0, drDisc("IncTaxAmt"))
                    'drDisc("MinPayAmt") = Math.Round((drDisc("NetAmount") / drDisc("Quantity")) * drDisc("PickUpQty"), 3)
                    'code added by irfan for cakeology IGST ON  07/11/2017=======================================================
                    drDisc("EXCLTAXAMT") = (((drDisc("GROSSAMT") - drDisc("DISCOUNT"))) * drDisc("TaxPer")) / 100
                    drDisc("EXCLTAXAMT") = FormatNumber(CDbl(drDisc("EXCLTAXAMT")), 3)
                    drDisc("TOTALTAXAMT") = drDisc("EXCLTAXAMT")
                    drDisc("NETAMOUNT") = (drDisc("GROSSAMT") - drDisc("DISCOUNT")) + drDisc("EXCLTAXAMT")


                    drDisc("NETAMOUNT") = FormatNumber(CDbl(drDisc("NETAMOUNT")), 3)
                    '============================================================================================================

                    TotalSalesQty = drDisc("PickUpQty") + IIf(drDisc("DeliveredQty") IsNot DBNull.Value, drDisc("DeliveredQty"), 0)
                    NetArticleRate = drDisc("NetAmount") / drDisc("Quantity")
                    drDisc("MinPayAmt") = ((drDisc("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate)
                    drDisc("TotalPickUpAmt") = (drDisc("PickupQty") * NetArticleRate)

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
            CLPCardType = CtrlCustSearch1.CardType
            ClearCLP()
            CalCulateCLP(CLPCardType, _dsScan.Tables("ItemScanDetails"), "ISCLP=TRUE")
            'lblBalPoint.Text = dsMain.Tables("CashMemoDtl").Compute("SUM(CLPPoints)", "")
            'CustInfo.ctrlTxtPoints.Text = dsMain.Tables("CashMemoDtl").Compute("SUM(CLPPoints)", "")
            'End If
        Catch ex As Exception

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
                If MsgBox(getValueByKey("SO078"), MsgBoxStyle.YesNo, "SO078 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then

                    IsFormClosing = True
                    Dim frm As New frmNSalesOrderUpdate
                    MDISpectrum.ShowChildForm(frm, True)
                End If
            Else
                Dim frm As New frmNSalesOrderUpdate
                MDISpectrum.ShowChildForm(frm, True)
            End If

        Catch ex As Exception

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
        Dim advanceAMount As Double
        Dim drSOHdr As DataRow
        Dim findKey(2) As Object
        findKey(0) = vSiteCode
        findKey(1) = clsAdmin.Financialyear
        findKey(2) = CtrlSalesInfo1.CtrlTxtOrderNo.Value
        drSOHdr = dsMain.Tables("SalesOrderHDR").Rows.Find(findKey)
        'If Not (dsPayment Is Nothing) AndAlso dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables(0).Rows.Count > 0 Then
        '    If drSOHdr IsNot Nothing Then
        '        advanceAMount = CType(IIf(drSOHdr("AdvanceAmt") Is DBNull.Value, 0, drSOHdr("AdvanceAmt")), Double) + CType(dsPayment.Tables(0).Compute("Sum(Amount)", ""), Double)
        '    Else
        '        advanceAMount = CType(dsPayment.Tables(0).Compute("Sum(Amount)", ""), Double)
        '    End If
        'Else
        '    advanceAMount = 0.0
        'End If
        Dim assignedDeliveryAmount As Decimal = SoDeliveryInfo.Sum(Function(x) x.Amount)
        Dim totalRemainingAmount As Double = CDbl(CtrlCashSummary1.lbltxt4) - CDbl(CtrlCashSummary1.lbltxt5) - assignedDeliveryAmount
        Dim deliveryAmount As Decimal
        If qty * rate < totalRemainingAmount Then
            deliveryAmount = qty * rate
        Else
            deliveryAmount = totalRemainingAmount
        End If
        Return deliveryAmount
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
            If dgDeliveryLocation.Row >= 1 Then
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

            For colno = 1 To grdScanItem.Cols.Count - 1
                If grdScanItem.Cols(colno).Name.ToUpper() <> "DISCRIPTION".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "EAN".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "ArticleCode".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "SellingPrice".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "Quantity".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "TOTALDISCPERCENTAGE".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "PickUpQty".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "IsCLP".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "TotalDiscount".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "DEL".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "ExpDelDate".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "ReservedQty".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "TotalTaxAmt".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "DeliverySiteCode".ToUpper() _
                    AndAlso grdScanItem.Cols(colno).Name.ToUpper() <> "NetAmount".ToUpper() Then
                    HideColumns(grdScanItem, False, grdScanItem.Cols(colno).Name)
                End If
            Next

            grdScanItem.Cols("Del").Caption = ""
            grdScanItem.Cols("Del").Width = 20
            grdScanItem.Cols("Del").ComboList = "..."

            If (clsDefaultConfiguration.BarcodeDisplayAllowed) Then
                grdScanItem.Cols("EAN").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.ean")
                grdScanItem.Cols("EAN").Width = 90
                grdScanItem.Cols("EAN").AllowEditing = False
                grdScanItem.Cols("EAN").Visible = True
            Else
                grdScanItem.Cols("EAN").Visible = False
            End If

            grdScanItem.Cols("ArticleCode").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.articlecode")
            grdScanItem.Cols("ArticleCode").Width = 90
            grdScanItem.Cols("ArticleCode").AllowEditing = False
            grdScanItem.Cols("ArticleCode").Visible = True
            grdScanItem.Cols("Discription").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.discription")
            grdScanItem.Cols("Discription").Width = 150
            grdScanItem.Cols("Discription").AllowEditing = False
            'grdScanItem.Cols("SellingPrice").Caption = "Price" 'getValueByKey("frmnsalesordercreation.grdscanitem.sellingprice")
            grdScanItem.Cols("SellingPrice").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.sellingprice")
            grdScanItem.Cols("SellingPrice").Width = 60
            grdScanItem.Cols("SellingPrice").AllowEditing = False
            grdScanItem.Cols("Quantity").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.quantity")
            grdScanItem.Cols("Quantity").Width = 45
            'grdScanItem.Cols("Quantity").EditMask = "999999999"
            grdScanItem.Cols("Quantity").Format = "0.000"
            grdScanItem.Cols("PickUpQty").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.pickupqty")
            grdScanItem.Cols("PickUpQty").Width = 45
            grdScanItem.Cols("PickUpQty").Format = "0.000"
            If clsDefaultConfiguration.IsBatchManagementReq Then
                grdScanItem.Cols("PickUpQty").AllowEditing = False
            End If

            grdScanItem.Cols("TOTALDISCPERCENTAGE").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.totaldiscpercentage")
            grdScanItem.Cols("TOTALDISCPERCENTAGE").Width = 45
            grdScanItem.Cols("TOTALDISCPERCENTAGE").Format = "0.00"
            grdScanItem.Cols("TOTALDISCPERCENTAGE").AllowEditing = False
            grdScanItem.Cols("NetAmount").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.netamount")
            grdScanItem.Cols("NetAmount").Width = 70
            grdScanItem.Cols("NetAmount").Format = "0.00"
            grdScanItem.Cols("NetAmount").AllowEditing = False
            grdScanItem.Cols("ExpDelDate").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.expdeldate")
            grdScanItem.Cols("ExpDelDate").Width = 140
            grdScanItem.Cols("ExpDelDate").Format = "g"
            grdScanItem.Cols("Stock").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.stock")
            grdScanItem.Cols("Stock").Width = 45
            grdScanItem.Cols("Stock").AllowEditing = False
            grdScanItem.Cols("IsCLP").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.isclp")
            grdScanItem.Cols("IsCLP").Width = 45
            grdScanItem.Cols("ReservedQty").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.reservedqty")
            grdScanItem.Cols("ReservedQty").Width = 45
            grdScanItem.Cols("ReservedQty").Format = "0.000"
            'grdScanItem.Cols("ReservedQty").DataType = Type.GetType("System.Boolean")
            'grdScanItem.Cols("IsCLP").DataType = Type.GetType("System.Boolean")
            'ExclTaxAmt
            grdScanItem.Cols("TotalTaxAmt").Caption = getValueByKey("frmnsalesordercreation.grdscanitem.excltaxamt")
            grdScanItem.Cols("TotalTaxAmt").Width = 45
            grdScanItem.Cols("TotalTaxAmt").AllowEditing = False
            grdScanItem.Cols("TotalTaxAmt").Format = "0.00"
            grdScanItem.AutoSizeCols()
            grdScanItem.Cols("Del").Width = 20
            grdScanItem.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.None
            'If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            '    For i = 0 To grdScanItem.Cols.Count - 1
            '        grdScanItem.Cols(i).Caption = grdScanItem.Cols(i).Caption.ToUpper
            '    Next
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub ClearCLP()
        Try
            For Each dr As DataRow In _dsScan.Tables("ItemScanDetails").Rows
                dr("CLPPoints") = DBNull.Value
                dr("CLPDiscount") = DBNull.Value
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rbbtnClrAllPromo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbbtnClrAllPromo.Click
        If dsScan.Tables(0).Rows.Count > 0 Then
            If IsApplyPromotion = True Then
                RemoveApplyPromotion(_dsScan)
            End If
            dsScan.AcceptChanges()
            For Each drDisc As DataRow In _dsScan.Tables(0).Rows

                ' If Not (IIf(IsDBNull(drDisc("TotalDiscPercentage")), 0, drDisc("TotalDiscPercentage")) = 0) Then

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
                    objcom.CreateDataSetForTaxCalculation(drDisc("ARTICLECODE"), totalamt, drDisc, dsMain, CtrlSalesInfo1.CtrlTxtOrderNo.Value, drDisc("EAN"), True)
                End If

                '   drDisc("NetAmount") = drDisc("GrossAmt") - drDisc("LineDiscount") + IIf(drDisc("ExclTaxAmt") Is DBNull.Value, 0, drDisc("ExclTaxAmt")) + IIf(drDisc("IncTaxAmt") Is DBNull.Value, 0, drDisc("IncTaxAmt"))
                'drDisc("MinPayAmt") = Math.Round((drDisc("NetAmount") / drDisc("Quantity")) * drDisc("PickUpQty"), 3)
                drDisc("NetAmount") = drDisc("GrossAmt") - drDisc("Discount") + IIf(drDisc("ExclTaxAmt") Is DBNull.Value, 0, drDisc("ExclTaxAmt")) + IIf(drDisc("IncTaxAmt") Is DBNull.Value, 0, drDisc("IncTaxAmt"))

                TotalSalesQty = drDisc("PickUpQty") + IIf(drDisc("DeliveredQty") IsNot DBNull.Value, drDisc("DeliveredQty"), 0)
                NetArticleRate = drDisc("NetAmount") / drDisc("Quantity")
                drDisc("MinPayAmt") = ((drDisc("Quantity") - TotalSalesQty) * NetArticleRate * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100)) + (TotalSalesQty * NetArticleRate)
                drDisc("TotalPickUpAmt") = (drDisc("PickupQty") * NetArticleRate)

                '    End If

            Next



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
                        obj.RecalculateLine(drdata, CtrlSalesInfo1.CtrlTxtOrderNo.Value, dsMain)
                    Next
                End If
            Next
            CalculateSalesOrderSummary(dsScan)
            RefreshScanData(dsScan)
            GridSetting()
        Catch ex As Exception

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
            If dsScan.Tables(0).Rows.Count > 0 Then
                If MsgBox(getValueByKey("SO078"), MsgBoxStyle.YesNo, "SO078 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                    Dim frm As New frmNSalesOrderCancel
                    MDISpectrum.ShowChildForm(frm, True)
                End If
            Else
                Dim frm As New frmNSalesOrderCancel
                MDISpectrum.ShowChildForm(frm, True)
            End If

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
                Case Keys.N
                    If My.Computer.Keyboard.CtrlKeyDown Then
                        BtnSONew_Click(Nothing, New System.EventArgs)
                    End If
                Case Keys.E
                    If My.Computer.Keyboard.CtrlKeyDown Then
                        rbbtnSOEdit_Click(Nothing, New System.EventArgs)
                    End If

                Case Keys.X
                    If My.Computer.Keyboard.CtrlKeyDown Then
                        rbbtnSOCancel_Click(Nothing, New System.EventArgs)
                    End If
                Case Keys.S
                    If My.Computer.Keyboard.CtrlKeyDown Then
                        'BtnSaveSalesOrder_Click(Nothing, New System.EventArgs)
                        CtrlCustSearch1.CtrlBtn1_Click(CtrlCustSearch1.CtrlBtn1, New System.EventArgs)
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


    Private Sub BtnSelectDeliveryLoc_Click(sender As System.Object, e As System.EventArgs) Handles BtnSelectDeliveryLoc.Click
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
        Dim EventType As Int32
        ShowMessage(getValueByKey("CST001"), getValueByKey("CLAE04"), EventType, True, getValueByKey("mod009"))
        If EventType = 1 Then
            Dim dtList = objComn.GetAllTaxesAppliedToSite(clsAdmin.SiteCode, "SO201")
            If dtList Is Nothing OrElse dtList.Rows.Count <= 0 Then
                ShowMessage("No Taxes available for this document type", getValueByKey("CLAE04"))
                Exit Sub
            End If
            Dim objView As New frmNCommonSearch
            objView.SetData = dtList
            objView.ShowDialog()
            If Not objView.search Is Nothing Then
                clsDefaultConfiguration.CSTTaxCode = objView.search(1)
            Else
                Exit Sub
            End If
            IsCSTApplicable = True
            ResetTax(True)
            rbnCST.Enabled = False
        End If


        'Dim res = MessageBox.Show(getValueByKey("CST001"), getValueByKey("CLAE04"), MessageBoxButtons.YesNo)
        'If res = Windows.Forms.DialogResult.Yes Then
        '    IsCSTApplicable = True
        '    ResetTax(True)

        'End If
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

            'Code added by irfan on 29/12/2017 for cakeology IGST==========================================================
            For Each dr As DataRow In _dsScan.Tables("ItemScanDetails").Rows
                If IsCSTApplicable Then
                    dtTaxCalc = objCM.getTax(vSiteCode, String.Empty, "SO201", dr("Quantity"), dr("EAN"), clsDefaultConfiguration.CSTTaxCode, True)
                Else
                    dtTaxCalc = objCM.getTax(vSiteCode, dr("ArticleCode"), "SO201", dr("Quantity"), dr("EAN"), clsDefaultConfiguration.CSTTaxCode, False)
                End If

                _strCustNo = dsMain.Tables("QuotationHdr").Rows(0)("CustomerNo")

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

                Dim vTaxLineNo As Integer = 0
                For Each drrow As DataRow In dtTaxCalc.Rows
                    Dim row As DataRow = dsMain.Tables("SalesOrderTaxDtls").NewRow
                    vTaxLineNo += 1
                    row(0) = vSiteCode
                    row(1) = clsAdmin.Financialyear
                    row(2) = CtrlSalesInfo1.CtrlTxtOrderNo.Value
                    row(3) = drrow("EAN")
                    row(4) = vTaxLineNo
                    row(5) = drrow("TaxCode")
                    row(6) = Math.Round(drrow("TaxAmount"), 2)
                    dsMain.Tables("SalesOrderTaxDtls").Rows.Add(row)

                Next

                If _dsScan.Tables("ItemScanDetails").Columns.Contains("TaxPer") = True Then
                    If IsIGSTApplicableForOutsideCustomer = True Then
                        For i = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1
                            _dsScan.Tables("ItemScanDetails").Rows(i)("TaxPer") = IIf(dtTaxCalc.Rows.Count > 0, dtTaxCalc.Compute("sum(Value)", ""), 0)
                        Next
                    Else
                        For i = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1
                            _dsScan.Tables("ItemScanDetails").Rows(i)("TaxPer") = IIf(dtTaxCalc.Rows.Count > 0, dtTaxCalc.Compute("sum(Value)", ""), 0)
                        Next
                    End If
                Else
                    _dsScan.Tables("ItemScanDetails").Columns.Add("TaxPer", System.Type.GetType("System.String"))
                    If IsIGSTApplicableForOutsideCustomer = True Then
                        For i = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1
                            _dsScan.Tables("ItemScanDetails").Rows(i)("TaxPer") = IIf(dtTaxCalc.Rows.Count > 0, dtTaxCalc.Compute("sum(Value)", ""), 0)
                        Next
                    Else
                        For i = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1
                            _dsScan.Tables("ItemScanDetails").Rows(i)("TaxPer") = IIf(dtTaxCalc.Rows.Count > 0, dtTaxCalc.Compute("sum(Value)", ""), 0)
                        Next
                    End If
                End If

            Next

            Dim ind As Integer
            For ind = 0 To _dsScan.Tables("ItemScanDetails").Rows.Count - 1
                _dsScan.Tables("ItemScanDetails").Rows(ind)("RowIndex") = ind + 1
            Next
            '===============================================================================================================

            If Not QuotationOtherCharges Is Nothing Then
                dtOtherCharges = QuotationOtherCharges
            End If

            dsMain.Tables.Remove("Quotationdtl")
            If dsMain.Tables("Quotationhdr").Rows.Count > 0 Then
                dsMain.Tables("Quotationhdr")(0)("SOStatus") = "Closed"

                If dsMain.Tables("quotationhdr")(0)("CustomerType").ToString().ToUpper() = "CLP" Then
                    CtrlCustSearch1.rbCLPMember.Checked = True
                Else
                    CtrlCustSearch1.rbOtherCust.Checked = True
                End If

                CtrlCustSearch1.CtrlTxtCustNo.Text = dsMain.Tables("quotationhdr")(0)("CustomerNo")
                CtrlCustDtls1.lblCustNoValue.Text = dsMain.Tables("quotationhdr")(0)("CustomerNo")
                CtrlCustSearch1.CustmType = dsMain.Tables("quotationhdr")(0)("CustomerType")


                Dim dtCustmInfo = objCustm.GetCustomerInformation(dsMain.Tables("quotationhdr")(0)("CustomerType"), vSiteCode, clsAdmin.CLPProgram, dsMain.Tables("quotationhdr")(0)("CustomerNo"))
                CtrlCustDtls1.pDisplayDtls(dtCustmInfo)
                RefreshScanData(dsScan)
                CalculateSalesOrderSummary(dsScan)
            End If
        End If
    End Sub

    Protected Sub grdScanItem_StartEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
        _iArticleQtyBeforeChange = grdScanItem.Rows(e.Row)("Quantity")
    End Sub

    Private Sub GetNewSalesOrderNumber()
        If OnlineConnect = True Then
            'Changed by Rohit to generate Document No. for proper sorting
            Dim objType = "FO_DOC"
            Try
                CtrlSalesInfo1.CtrlTxtOrderNo.Value = GenDocNo("SO" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode, objType))
            Catch ex As Exception
                CtrlSalesInfo1.CtrlTxtOrderNo.Value = "SO" & clsAdmin.TerminalID & objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode, objType)
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
                CtrlSalesInfo1.CtrlTxtOrderNo.Value = GenDocNo("OSO" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode))
            Catch ex As Exception
                CtrlSalesInfo1.CtrlTxtOrderNo.Value = "OSO" & clsAdmin.TerminalID & objComn.getDocumentNo("SalesOrder", clsAdmin.SiteCode)
            End Try

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
            '----Check if pickup qty is there or not if there then if user put some pick up quantity then Generate  STR is not applicable &  show a message .
            If dsScan.Tables("ItemScanDetails").Rows.Count > 0 Then
                Dim dr() = dsScan.Tables("ItemScanDetails").Select("PickUpQty>0")
                If dr.Length > 0 Then
                    MessageBox.Show("You cannot generate STR as few items in this sales order have been received by customer", getValueByKey("CLAE04"))
                Else
                    If MessageBox.Show("This sales order is not saved. Click OK to save sales order and generate the STR.", getValueByKey("CLAE04"), MessageBoxButtons.OKCancel) = Windows.Forms.DialogResult.OK Then
                        IsSTRGenerate = True
                        BtnAcceptPayment_Click(Nothing, Nothing)
                        If IsSOSaved Then
                            ShowMessage("STR Generated Successfully", getValueByKey("CLAE04"))
                            IsSOSaved = False
                        End If
                    End If
                End If
            End If
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

    Private Sub rbBtnRoundOff_Click(sender As Object, e As EventArgs) Handles rbBtnRoundOff.Click
        Try
            'If Not CtrlCashSummary1.lbltxt4 Mod 5 = 0 Then
            Dim p As Object = "ClearPromWithoutMessage"
            rbbtnClrAllPromo_Click(p, Nothing)
            'End If
            If CtrlCashSummary1.lbltxt4 Mod 5 = 0 Then
                Exit Sub
            End If
            Dim FilterCondition As String = " isnull(Toplevel,'')='' "
            Dim totalGAmount As Double
            Dim percentage, totalDiscValue As Double
            totalDiscValue = CtrlCashSummary1.lbltxt4 Mod 5
            Dim dtUserAuth As DataTable = _dsScan.Tables(0).Copy
            Dim dtCashMemoDtls As DataTable = _dsScan.Tables(0)

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

            For Each drDisc As DataRow In _dsScan.Tables(0).Rows

                If Not (IIf(IsDBNull(drDisc("TotalDiscPercentage")), 0, drDisc("TotalDiscPercentage")) = 0) Then

                    If (Not String.IsNullOrEmpty(drDisc("CLPDiscount").ToString())) Then
                        drDisc("Discount") = CDbl(drDisc("LineDiscount").ToString()) + CDbl(drDisc("CLPDiscount").ToString())
                    End If

                    drDisc("TotalDiscPercentage") = (IIf(drDisc("Discount") Is DBNull.Value, 0, drDisc("Discount")) * 100) / drDisc("GrossAmt")
                    drDisc("PromotionId") = IIf(drDisc("TopLevel") = String.Empty, 0, drDisc("TopLevel"))

                    'If drDisc("PromotionId") = "0,0" Then
                    '    drDisc("LineDiscount") = (drDisc("GrossAmt") * drDisc("TotalDiscPercentage")) / 100
                    'End If
                    Dim totalamt As Decimal = 0
                    If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                        totalamt = drDisc("GrossAmt") - drDisc("Discount")
                        Dim objcom As New clsSaleOrderCommon
                        objcom.IsCSTApplicable = IsCSTApplicable
                        objcom.CreateDataSetForTaxCalculation(drDisc("ARTICLECODE"), totalamt, drDisc, dsMain, CtrlSalesInfo1.CtrlTxtOrderNo.Value, drDisc("EAN"), True)
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
            CtrlSalesPersons.CtrlTxtBox.Select()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    'added by khusrao adil on 12-10-2017 for product specific change -Wild search functionality
    Private Sub AndroidSearchTextBox_Textchange(sender As Object, e As EventArgs)
        If CtrlSalesPersons.AndroidSearchTextBox.IsItemSelected Then
            CtrlSalesPersons.CtrlTxtBox.Text = CtrlSalesPersons.AndroidSearchTextBox.Text
        End If
    End Sub
    'added by khusrao adil on 12-10-2017 for product specific change -Wild search functionality
    Private Sub txtSearchItem_textchange(sender As Object, e As EventArgs)
        If Not String.IsNullOrEmpty(CtrlSalesPersons.CtrlTxtBox.Text) AndAlso CtrlSalesPersons.AndroidSearchTextBox.IsItemSelected Then
            CtrlSalesPersons.AndroidSearchTextBox.IsItemSelected = False
            txtSearchItem_Leave(sender, New System.EventArgs)
        End If
    End Sub
    Private Function Themechange()

        rbnTabSO.Text = rbnTabSO.Text.ToUpper
        rbnTabSO.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)



        CtrlRbn1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black
        CtrlSalesPersons.AlignChange = "Sales Order Old"
        CtrlCashSummary1.AlignChangeForCashSummary = "Sales Order Old"
        rbbtnDefaultPromo.LargeImage = Global.Spectrum.My.Resources.Resources.defaultPromo_Normal
        rbbtnSelectPromo.LargeImage = Global.Spectrum.My.Resources.Resources.SelectPromo_Normal
        rbbtnClrAllPromo.LargeImage = Global.Spectrum.My.Resources.Resources.ClearPromo_Normal
        CtrlRbn1.DbtnPay.LargeImage = Global.Spectrum.My.Resources.Resources.payment_Normal
        CtrlRbn1.DbtnPayCash.LargeImage = Global.Spectrum.My.Resources.Resources.Cash_Normal
        CtrlRbn1.DbtnPayCard.LargeImage = Global.Spectrum.My.Resources.Resources.Card_Normal
        CtrlRbn1.DbtnpayCheque.LargeImage = Global.Spectrum.My.Resources.PayByCheque
        rbnCST.LargeImage = Global.Spectrum.My.Resources.ApplyTax
        rbBtnRoundOff.LargeImage = Global.Spectrum.My.Resources.RoundOff
        Me.rbgrpSO.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        ' Me.rbgrpSO.Image = Global.Spectrum.My.Resources.defaultPromo_Normal
        Me.rbgrpSO.ForeColorInner = Color.FromArgb(37, 37, 37)
        Me.rbnGrpCST.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        ' Me.rbnGrpCST.Image = Global.Spectrum.My.Resources.defaultPromo_Normal
        Me.rbnGrpCST.ForeColorInner = Color.FromArgb(37, 37, 37)
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
        rbbtnSOEdit.LargeImage = Global.Spectrum.My.Resources.EditSO1
        rbbtnSOCancel.LargeImage = Global.Spectrum.My.Resources.CancelSO1
        rbbtnSave.LargeImage = Global.Spectrum.My.Resources.SaveSO1
        rbbtnPrint.LargeImage = Global.Spectrum.My.Resources.PrintSO1
        rbbtnDefaultPromo.Text = "Default Promo(Ctrl+D)"
        rbbtnSelectPromo.Text = "Select Promo(Ctrl+P)"
        rbbtnClrAllPromo.Text = "Clear Promo(Ctrl+C)"
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


        rbgrpSO.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbnGrpCST.ForeColorInner = Color.FromArgb(54, 54, 54)
        RibbonGroup2.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbgrpSaveAndPrint.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbnGrpCMPromotion.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbnGrpAddCombo.ForeColorInner = Color.FromArgb(54, 54, 54)


        rbgrpSO.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbnGrpCST.ForeColorOuter = Color.FromArgb(0, 107, 163)
        RibbonGroup2.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbgrpSaveAndPrint.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbnGrpCMPromotion.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbnGrpAddCombo.ForeColorOuter = Color.FromArgb(0, 107, 163)
        '  DgrpPayments.ForeColorOuter = Color.FromArgb(0, 107, 163)

        grdScanItem.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        grdScanItem.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212)
        grdScanItem.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        grdScanItem.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        grdScanItem.Rows.MinSize = 30
        grdScanItem.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        grdScanItem.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdScanItem.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdScanItem.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdScanItem.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdScanItem.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdScanItem.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        grdScanItem.Styles.Highlight.ForeColor = Color.Black
        grdScanItem.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdScanItem.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdScanItem.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdScanItem.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdScanItem.CellButtonImage = Global.Spectrum.My.Resources.Delete
        dgDeliveryLocation.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        dgDeliveryLocation.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212)
        dgDeliveryLocation.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgDeliveryLocation.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        dgDeliveryLocation.Rows.MinSize = 30
        dgDeliveryLocation.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        dgDeliveryLocation.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgDeliveryLocation.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgDeliveryLocation.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgDeliveryLocation.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgDeliveryLocation.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgDeliveryLocation.Styles.Highlight.ForeColor = Color.Black
        dgDeliveryLocation.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        dgDeliveryLocation.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgDeliveryLocation.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgDeliveryLocation.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgDeliveryLocation.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgDeliveryLocation.CellButtonImage = Global.Spectrum.My.Resources.Delete

        CtrlSalesInfo1.Size = New Size(260, 150)

        BtnSelectDeliveryLoc.Image = My.Resources.HomeDelivery_Normal
        BtnSelectDeliveryLoc.ImageAlign = ContentAlignment.MiddleCenter
        BtnSelectDeliveryLoc.TextAlign = ContentAlignment.MiddleCenter
        BtnSelectDeliveryLoc.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        BtnSelectDeliveryLoc.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnSelectDeliveryLoc.BackColor = Color.Transparent
        BtnSelectDeliveryLoc.BackColor = Color.White
        BtnSelectDeliveryLoc.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnSelectDeliveryLoc.FlatStyle = FlatStyle.Flat


        cmdGenerateSTR.Image = My.Resources.GenerateSTRnew
        cmdGenerateSTR.ImageAlign = ContentAlignment.MiddleCenter
        cmdGenerateSTR.TextAlign = ContentAlignment.MiddleCenter
        cmdGenerateSTR.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        cmdGenerateSTR.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdGenerateSTR.BackColor = Color.Transparent
        cmdGenerateSTR.BackColor = Color.White
        cmdGenerateSTR.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdGenerateSTR.FlatStyle = FlatStyle.Flat


        CtrlBtnSearchCLP.Image = My.Resources.LoyaltyPoints
        CtrlBtnSearchCLP.ImageAlign = ContentAlignment.MiddleCenter
        CtrlBtnSearchCLP.TextAlign = ContentAlignment.MiddleCenter
        CtrlBtnSearchCLP.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        CtrlBtnSearchCLP.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnSearchCLP.BackColor = Color.Transparent
        CtrlBtnSearchCLP.BackColor = Color.White
        CtrlBtnSearchCLP.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CtrlBtnSearchCLP.FlatStyle = FlatStyle.Flat


        CtrlBtnStockCheck.Image = My.Resources.StockCheck
        CtrlBtnStockCheck.ImageAlign = ContentAlignment.MiddleCenter
        CtrlBtnStockCheck.TextAlign = ContentAlignment.MiddleCenter
        CtrlBtnStockCheck.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        CtrlBtnStockCheck.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnStockCheck.BackColor = Color.Transparent
        CtrlBtnStockCheck.BackColor = Color.White
        CtrlBtnStockCheck.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CtrlBtnStockCheck.FlatStyle = FlatStyle.Flat


        CtrlbtnSOOtherCharges.Image = My.Resources.AdditionalCost
        CtrlbtnSOOtherCharges.ImageAlign = ContentAlignment.MiddleCenter
        CtrlbtnSOOtherCharges.TextAlign = ContentAlignment.MiddleCenter
        CtrlbtnSOOtherCharges.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        CtrlbtnSOOtherCharges.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CtrlbtnSOOtherCharges.BackColor = Color.Transparent
        CtrlbtnSOOtherCharges.BackColor = Color.White
        CtrlbtnSOOtherCharges.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CtrlbtnSOOtherCharges.FlatStyle = FlatStyle.Flat

        CtrlProductImage.Size = New Size(224, 122)
        CtrlCashSummary1.Location = New Point(1131, 379)
        CtrlCashSummary1.Size = New Size(224, 190)
        CtrlCashSummary1.MinimumSize = New Size(222, 0)
        TabPageDeliveryLocation.TabBackColorSelected = Color.FromArgb(212, 212, 212)
        TabPageOrderedItems.TabBackColorSelected = Color.FromArgb(212, 212, 212)
        'TabPageDeliveryLocation.Font = New Font("Neo Sans", 7, FontStyle.Bold)
        'TabPageOrderedItems.Font = New Font("Neo Sans", 7, FontStyle.Bold)
        TabPageOrderedItems.Text = TabPageOrderedItems.Text.ToUpper
        TabPageDeliveryLocation.Text = TabPageDeliveryLocation.Text.ToUpper
        Me.c1SizerGrid.Controls.Remove(Me.CtrlSalesPersons)
        Me.Controls.Add(CtrlSalesPersons)
        Me.Controls.SetChildIndex(Me.CtrlSalesPersons, 0)
        CtrlSalesPersons.Size = New Size(635, 22)
        CtrlSalesPersons.Location = New Point(490, 308)
        c1SizerGrid.Controls.Remove(grdScanItem)
        c1SizerGrid.Hide()
        Me.TabPageOrderedItems.Controls.Add(Me.grdScanItem)
        Me.grdScanItem.Size = New System.Drawing.Size(1118, 304)
        Me.grdScanItem.Location = New System.Drawing.Point(4, 2)
        'If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
        '    For i = 0 To dgDeliveryLocation.Cols.Count - 1
        '        dgDeliveryLocation.Cols(i).Caption = dgDeliveryLocation.Cols(i).Caption.ToUpper
        '    Next
        'End If
    End Function
End Class