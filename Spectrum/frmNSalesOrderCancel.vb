﻿Imports System.IO
Imports SpectrumBL
Imports System.Resources
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports SpectrumPrint

Public Class frmNSalesOrderCancel
    Dim CVoucherNo As String
    Dim CVVoucherDay As Int32 = clsAdmin.CreditValidDays
    Dim objCustm As New clsCLPCustomer()
    Dim vSiteCode As String = clsAdmin.SiteCode
    Dim vfinancialYear As String = clsAdmin.Financialyear
    Dim vTerminalID As String = clsAdmin.TerminalID
    Dim vCurrencyDescription As String = clsAdmin.CurrencyDescription
    Dim vCurrencyCode As String = clsAdmin.CurrencyCode
    Dim vUserName As String = clsAdmin.UserName
    Dim vDateFormat As String = clsAdmin.SqlDBDateFormat
    Dim vCurrentDate As Date
    Dim dtSalesOrderTaxDetails As DataTable
    Dim vPrinterSelection As Boolean = False
    Dim vPrintPaperType As String = String.Empty
    Dim vPrintLayoutselection As Boolean = False
    Dim vHeaderNote As Boolean = False
    Dim vFooterNote As Boolean = False
    Dim vResetTransNumbers As Boolean = False

    Dim IsSalesOrderCancel As Boolean = False
    Dim IsNextInvcNo As Boolean = False
    Dim IsFormClosing As Boolean = False

    Dim objCharge As New clsSalesOrder
    Dim objComn As New clsCommon
    Dim objCM As New clsCashMemo

    Dim dsInvoice As New DataSet
    Dim dsScanTemp As New DataSet

    Dim vSalesNo As String = ""
    Dim vSalesPerson As String = ""
    Dim vEANList As String = ""
    Dim vDocType As String
    Dim vSOStatus As String = ""
    Dim vReceivedAmt As Double = 0.0
    Dim GridWidth As Integer = 0
    Dim GridHeight As Integer = 0

    '************************Temperory variable
    Dim lblOrderQty As New Label
    Dim lblPickupQty As New Label
    '************************Temperory variable
    Dim SalesInvoiceNbr As String = ""

    Dim _dsMain As New DataSet
    Public Property dsMain() As DataSet
        Get
            Return _dsMain
        End Get
        Set(ByVal value As DataSet)
            _dsMain = value
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

    Dim _dtCustmInfo As New DataTable
    Public Property dtCustmInfo() As DataTable
        Get
            Return _dtCustmInfo
        End Get
        Set(ByVal value As DataTable)
            _dtCustmInfo = value
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

    Private Sub frmNSalesOrderCancel_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If grdSOItems.Rows.Count > 1 AndAlso Not IsFormClosing Then
            If MsgBox(getValueByKey("SO047"), MsgBoxStyle.YesNo, "SO047 - " & getValueByKey("CLAE04")) = MsgBoxResult.No Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmNSalesOrderCancel_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.N And Keys.ControlKey Then
            cmdSONew_Click(sender, New System.EventArgs)
        ElseIf e.KeyCode = Keys.E And Keys.ControlKey Then
            CmdSOEdit_Click(sender, New System.EventArgs)
        ElseIf e.KeyCode = Keys.X And Keys.ControlKey Then
            BtnSOCancel_Click(sender, New System.EventArgs)
        ElseIf e.KeyCode = Keys.S And Keys.ControlKey Then
            BtnSaveSalesOrder_Click(sender, New System.EventArgs)
        ElseIf e.KeyCode = Keys.M And Keys.ControlKey Then
            CtrlCustDtls1.CtrlLabel3_Click(Nothing, New KeyEventArgs(Keys.Enter))
        ElseIf e.KeyCode = Keys.R And Keys.ControlKey AndAlso cmdPrint.Enabled Then
            Call BtnSOPrint_Click(cmdPrint, New System.EventArgs)
        ElseIf e.KeyCode = Keys.F5 Then
            BtnPayCash_Click(sender, New System.EventArgs)
        End If
    End Sub


    ''' <summary>
    ''' Get the Site default Settings And Set Default Config Object
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSalesOrderCancel_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'AddHandler TabPageItemDetails_Resize(), AddressOf TabPageItemDetails_Resize
            'AddHandler TabPageInvoiceDetails.Resize
            AddHandler CtrlSalesInfo.CtrlBtn1.Click, AddressOf BtnSearchSalesOrder_Click
            'AddHandler CtrlSalesInfo.CtrlTxtOrderNo.Leave, AddressOf txtSalesNo_Leave
            AddHandler CtrlSalesInfo.CtrlTxtOrderNo.PreviewKeyDown, AddressOf txtSalesNo_PreviewKeyDown
            AddHandler cmdPrint.Click, AddressOf BtnSOPrint_Click
            'AddHandler CtrlRbn1.DbtnPay.Click, AddressOf BtnAcceptPayment_Click
            'AddHandler CtrlRbn1.DbtnPayCard.Click, AddressOf BtnPayCard_Click
            'AddHandler CtrlRbn1.DbtnPayCash.Click, AddressOf BtnPayCash_Click
            'AddHandler CtrlRbn1.DbtnpayCheque.Click, AddressOf BtnPayCheque_Click
            CtrlRbn1.DgrpPayments.Visible = False
            CtrlBtnAddExtraCost.Visible = False

            dsMain = objSO.GetSOTableStruct(clsAdmin.SiteCode, 0)
            objSO.GetSODefaultConfig(clsAdmin.SiteCode)
            Dim objdefault As New clsDefaultConfiguration("SalesOrder")
            objdefault.GetDefaultSettings()
            vCurrentDate = objComn.GetCurrentDate
            vDocType = objSO.SOCreation

            _dsScan = objSO.GetCollectionOfItems
            _dsScan.Clear()
            RefreshLoadSOData()

            dsInvoice = objSO.SetInvoiceInSOCancel(vSiteCode, IIf(vSalesNo = String.Empty, 0, vSalesNo))
            RefreshLoadInvcData()
            TabSalesOrder.pInit()
            rbbtnSave.Enabled = False
            GridItemSetting()
            GridInvoiceSetting()
            PSetDefaultCurrencyOfCashMemoSummary(CtrlCashSummary)
            SwipePosition(CtrlCashSummary)
            TabSalesOrder.SelectedTab = TabSalesOrder.TabPages("TabPageItemDetails")

            CtrlSalesPerson.CtrlTxtBox.Visible = False
            CtrlSalesPerson.CtrlCmdSearch.Visible = False
            CtrlSalesPerson.CtrlSalesPersons.Enabled = False

            CtrlSalesInfo.CtrldtOrderDt.Enabled = False
            CtrlSalesInfo.CtrlDtExpDelDate.Enabled = False
            CtrlSalesInfo.CtrlTxtCustOrdRef.Enabled = False
            CtrlSalesInfo.CtrlTxtRemarks.Enabled = False
            CtrlSalesInfo.CtrlTxtInvoice.Enabled = False

            CtrlCustDtls1.cboAddrType.Enabled = False

            CtrlSalesInfo.CtrlTxtOrderNo.Select()

            Call SetTabSequence()
           
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
        SetCulture(Me, Me.Name, CtrlRbn1)
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
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

            ctrTablIndex.Add(Me.C1Sizer2, 1)
            ctrTablIndex.Add(Me.CtrlBtnStockCheck, 2)
            ctrTablIndex.Add(Me.CtrlBtnAddExtraCost, 3)

            SetFormTabIndex(ctrTablIndex:=ctrTablIndex)
            Me.grdSOItems.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.None
            Me.TabSalesOrder.TabStop = False
            Me.C1Sizer3.TabStop = False
            C1Sizer2.TabStop = False
            Me.TabPageInvoiceDetails.TabStop = False
            Me.TabPageItemDetails.TabStop = False

            '---- Set Tab Index END 
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Sub



    ''' <summary>
    ''' Resize DataGrid for Display Items Details
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TabPageItemDetails_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPageItemDetails.Resize
        Try
            GridWidth = 0
            GridHeight = 0

            grdSOItems.Width = TabSalesOrder.TabPages(0).Width - 3
            grdSOItems.Height = TabSalesOrder.TabPages(0).Height - 3
            GridWidth = (TabSalesOrder.TabPages(0).Width * 1) / 100
            GridHeight = (TabSalesOrder.TabPages(0).Height * 1) / 100

            grdSOItems.Cols(1).WidthDisplay = GridWidth * 12.86
            grdSOItems.Cols(2).WidthDisplay = GridWidth * 20.16
            grdSOItems.Cols(3).WidthDisplay = GridWidth * 8.12
            grdSOItems.Cols(4).WidthDisplay = GridWidth * 9.47
            grdSOItems.Cols(5).WidthDisplay = GridWidth * 10.83
            grdSOItems.Cols(6).WidthDisplay = GridWidth * 7.44
            grdSOItems.Cols(7).WidthDisplay = GridWidth * 10.15
            grdSOItems.Cols(8).WidthDisplay = GridWidth * 13.53
            grdSOItems.Cols(9).WidthDisplay = GridWidth * 7.44
            grdSOItems.Refresh()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Resize DataGrid for Display Invoice Details
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TabPageInvoiceDetails_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPageInvoiceDetails.Resize
        Try
            GridWidth = 0
            GridHeight = 0 
            grdSOInvoice.Width = TabSalesOrder.TabPages(1).Width - 3
            grdSOInvoice.Height = TabSalesOrder.TabPages(1).Height - 3
            GridWidth = (TabSalesOrder.TabPages(1).Width * 1) / 100
            GridHeight = (TabSalesOrder.TabPages(1).Height * 1) / 100 
            grdSOInvoice.Cols(1).WidthDisplay = GridWidth * 9.47
            grdSOInvoice.Cols(2).WidthDisplay = GridWidth * 12.18
            grdSOInvoice.Cols(3).WidthDisplay = GridWidth * 14.88
            grdSOInvoice.Cols(4).WidthDisplay = GridWidth * 11.5
            grdSOInvoice.Cols(5).WidthDisplay = GridWidth * 12.86
            grdSOInvoice.Cols(6).WidthDisplay = GridWidth * 11.5
            grdSOInvoice.Cols(7).WidthDisplay = GridWidth * 14.75
            grdSOInvoice.Cols(8).WidthDisplay = GridWidth * 12.86
            grdSOInvoice.Refresh()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

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

            vSalesNo = SearchSalesOrderCustomer(clsAdmin.SiteCode, True)
            SetSalesOrderInForm(vSalesNo)

            rbbtnSave.Enabled = True
            'BtnSOPrint.Enabled = True
            'CtrlBtnPayBill.Enabled = True
            CtrlRbn1.DgrpPayments.Enabled = True
            BtnSOCancel.Enabled = True

            CtrlSalesInfo.CtrlBtn1.Enabled = True
            BtnSOCancel.Enabled = True
            IsNextInvcNo = False
            GridItemSetting()
            fnGridColAutoSize(grdSOItems)
            GridInvoiceSetting()
            fnGridColAutoSize(grdSOInvoice)
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
    Private Sub txtSalesNo_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) 'Handles txtSalesNo.PreviewKeyDown
        Try
            If (e.KeyCode = Keys.Enter) Then
                Try
                    If Not (CtrlSalesInfo.CtrlTxtOrderNo.Text.Trim = String.Empty) AndAlso CtrlSalesInfo.CtrlTxtOrderNo.Text.Trim.Length > 2 Then

                        SetSalesOrderInForm(CtrlSalesInfo.CtrlTxtOrderNo.Text.Trim)

                        If dsMain.Tables.Count > 0 Then
                            If vSOStatus = "Closed" Or vSOStatus = "Return" Or vSOStatus = "Cancel" Then
                                rbbtnSave.Enabled = False
                                'BtnSOPrint.Enabled = False
                                'CtrlBtnPayBill.Enabled = False
                                CtrlRbn1.DgrpPayments.Enabled = False
                                BtnSOCancel.Enabled = False
                            Else
                                rbbtnSave.Enabled = True
                                'BtnSOPrint.Enabled = True
                                'CtrlBtnPayBill.Enabled = True
                                CtrlRbn1.DgrpPayments.Enabled = True
                                BtnSOCancel.Enabled = True
                            End If
                            'Open   Closed  Return  Cancel
                        End If
                        IsNextInvcNo = False
                    Else
                        ShowMessage(getValueByKey("SO073"), "SO073 - " & getValueByKey("CLAE04"))
                    End If
                Catch ex As Exception
                    ShowMessage(ex.Message, getValueByKey("CLAE05"))
                End Try
            End If
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' Load Old Sales Order in window
    ''' </summary>
    ''' <param name="vSalesNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSalesOrderInForm(ByVal vSalesNo As String) As Boolean
        Try
            If vSalesNo = String.Empty Then
                Exit Function
            Else
                Dim findKey(2) As Object
                Dim drSearchHdr As DataRow
                Dim vAddressType As String = ""
                Dim vCustomerNo As String = ""

                _dsMain = objSO.GetSOTableStruct(vSiteCode, IIf(vSalesNo = String.Empty, 0, vSalesNo), "Cancel")

                If Not (dsMain Is Nothing) AndAlso dsMain.Tables("SalesOrderHDR").Rows.Count > 0 Then
                    CtrlSalesInfo.CtrlTxtOrderNo.Text = vSalesNo
                    CtrlSalesInfo.CtrlBtn1.Enabled = True
                    vfinancialYear = dsMain.Tables("SalesOrderHDR").Rows(0)("FinYear")
                    findKey(0) = vSiteCode
                    findKey(1) = vfinancialYear
                    findKey(2) = vSalesNo
                    drSearchHdr = dsMain.Tables("SalesOrderHDR").Rows.Find(findKey)
                    If Not (drSearchHdr Is Nothing) Then

                        vSOStatus = drSearchHdr("SOStatus").ToString
                        CtrlSalesInfo.CtrldtOrderDt.Value = drSearchHdr("CREATEDON").ToString
                        CtrlSalesInfo.CtrlDtExpDelDate.Value = drSearchHdr("PromisedDeliveryDate").ToString

                        CtrlSalesInfo.CtrlTxtRemarks.Text = IIf(drSearchHdr("Remarks") Is DBNull.Value, "", drSearchHdr("Remarks"))
                        CtrlSalesInfo.CtrlTxtCustOrdRef.Text = IIf(drSearchHdr("CustomerOrderRef") Is DBNull.Value, "", drSearchHdr("CustomerOrderRef"))

                        vSalesPerson = IIf(drSearchHdr("SalesExecutiveCode") Is DBNull.Value, "", drSearchHdr("SalesExecutiveCode"))
                        vAddressType = IIf(drSearchHdr("DeliveryAtCustAddressType") Is DBNull.Value, "0", drSearchHdr("DeliveryAtCustAddressType"))
                        vCustomerNo = IIf(drSearchHdr("CustomerNo") Is DBNull.Value, "", drSearchHdr("CustomerNo"))
                        CtrlCustDtls1.lblCustTypeValue.Text = IIf(drSearchHdr("CustomerType") Is DBNull.Value, "", drSearchHdr("CustomerType"))
                        vSalesPerson = IIf(drSearchHdr("SalesExecutiveCode") Is DBNull.Value, "", drSearchHdr("SalesExecutiveCode"))
                        If vSalesPerson <> "" Then
                            CtrlSalesPerson.CtrlSalesPersons.SelectedValue = vSalesPerson
                        End If
                        CtrlCustDtls1.lblCustNoValue.Text = vCustomerNo
                        dtCustmInfo = objCustm.GetCustomerInformation(CtrlCustDtls1.lblCustTypeValue.Text, vSiteCode, clsAdmin.CLPProgram, vCustomerNo)
                        CtrlCustDtls1.pDisplayDtls(dtCustmInfo)
                        CtrlCustDtls1.cboAddrType.SelectedValue = vAddressType

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
                        drScan("ArticleCode") = drScanTemp("ArticleCode")
                        drScan("Discription") = drScanTemp("Discription")
                        drScan("SellingPrice") = FormatNumber(drScanTemp("SellingPrice"), 2)
                        drScan("Quantity") = drScanTemp("Quantity")
                        drScan("PickupQty") = 0
                        drScan("DeliveredQty") = IIf(drScanTemp("DeliveredQty") Is DBNull.Value, 0, drScanTemp("DeliveredQty"))
                        drScan("Discount") = drScanTemp("Discount")
                        drScan("NetAmount") = FormatNumber(drScanTemp("NetAmount"), 2)
                        drScan("ExpDelDate") = drScanTemp("ExpDelDate")
                        drScan("Stock") = drScanTemp("Stock")

                        drScan("ArticleCode") = drScanTemp("ArticleCode")
                        drScan("GrossAmt") = drScanTemp("GrossAmt")
                        drScan("MinPayAmt") = 0
                        drScan("ExclTaxAmt") = drScanTemp("ExclTaxAmt")
                        drScan("IncTaxAmt") = 0
                        drScan("PromotionId") = 0
                        drScan("LineDiscount") = drScanTemp("LineDiscount")
                        drScan("TotalDiscPercentage") = 0
                        drScan("FirstLevel") = 0
                        drScan("TopLevel") = 0
                        drScan("IsStatus") = IIf(drScanTemp("Status") = False, "Deleted", "Inserted")

                        vEANList = vEANList & "'" & drScanTemp("EAN") & "', "

                        _dsScan.Tables("ItemScanDetails").Rows.Add(drScan)
                    Next
                    _dsScan.AcceptChanges()

                    RefreshLoadSOData()

                    CalculateSalesOrderSummory(_dsScan)

                    dsInvoice = objSO.SetInvoiceInSOCancel(vSiteCode, IIf(vSalesNo = String.Empty, 0, vSalesNo))
                    RefreshLoadInvcData()

                    CtrlCustDtls1.lblCustNoValue.Text = vCustomerNo
                    dtCustmInfo = objCustm.GetCustomerInformation(CtrlCustDtls1.lblCustTypeValue.Text, vSiteCode, clsAdmin.CLPProgram, vCustomerNo)

                    'CtrlCustDtls1.rbCLPMember.Enabled = False
                    'CtrlCustDtls1.rbOtherCust.Enabled = False

                    CtrlCustDtls1.pDisplayDtls(dtCustmInfo)
                    CtrlCustDtls1.cboAddrType.SelectedValue = vAddressType

                    '_dtCustmInfo = objSO.GetSOCustmerInfo(vCustomerNo)

                    'Dim dv As New DataView(dtCustmInfo, "AddressType='" & vAddressType & "'", "", DataViewRowState.CurrentRows)
                    'Dim dtCInfo As DataTable = dv.ToTable

                    'SetCustomerInfoInSO(dtCInfo)

                    If drSearchHdr("SOStatus").ToString = "Cancel" Then
                        CtrlRbn1.DgrpPayments.Enabled = False
                        RibbonGroup3.Enabled = False
                        MsgBox(getValueByKey("SO058"), , "SO058 - " & getValueByKey("CLAE04"))
                    Else
                        CtrlRbn1.DgrpPayments.Enabled = True
                        RibbonGroup3.Enabled = True
                    End If

                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Function


#End Region

    ''' <summary>
    ''' Set Languadge in Sales Order Window
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        If clsDefaultConfiguration.TillOperationRequired = True And clsDefaultConfiguration.TillOpenDone = False Then
            ShowMessage(getValueByKey("CM057"), "CM057 - " & getValueByKey("CLAE04"))
            Me.Dispose()
            Me.Close()
            Exit Sub
        End If
        ' This call is required by the Windows Form Designer.
        If CheckAuthorisation(clsAdmin.UserCode, "SOCancel") = False Then
            ShowMessage(getValueByKey("SO053"), "SO053 - " & getValueByKey("CLAE04"))
            Me.Dispose()
            Me.Close()
            Exit Sub
        End If

        If OnlineConnect = False Then
            ShowMessage(getValueByKey("SOO85"), "SOO85 - " & getValueByKey("CLAE04"))
            Me.Dispose()
            Me.Close()
            Exit Sub
        End If
        InitializeComponent()
        Me.ClientSize = New System.Drawing.Size(gmdiclientwidth, gmdiclientheight)
        CtrlRbn1.pInitRbn()

        CtrlRbn1.DbtnF12.LargeImage = Global.Spectrum.My.Resources.Resources.price_change
        CtrlRbn1.DbtnF2.LargeImage = Global.Spectrum.My.Resources.Resources.change_qty

        'LbSalesNo.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbSalesNo", gCI)
        'LbSalesDate.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbSalesDate", gCI)
        'LbExpDelDate.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbExpDelDate", gCI)
        'LbRemarks.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbRemarks", gCI)
        'LbCustomerRef.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbCustomerRef", gCI)
        'LbSalesPerson.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbSalesPerson", gCI)

        'GroupBoxSOCustomerInfo.Text = gResourceMgr.GetString("frmSalesOrderCancel_GroupBoxSOCustomerInfo", gCI)
        'LbCustomerNo.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbCustomerNo", gCI)
        'LbCName.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbCName", gCI)
        ''LbCAddressType.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbCAddressType", gCI)
        'LbCAddress.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbCAddress", gCI)
        'LbCTelephone.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbCTelephone", gCI)

        ''LbItemScan.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbItemScan", gCI)
        ''LbTotal.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbTotal", gCI)

        'TabPageItemDetails.Text = gResourceMgr.GetString("frmSalesOrderCancel_TabPageItemDetails", gCI)
        'TabPageInvoiceDetails.Text = gResourceMgr.GetString("frmSalesOrderCancel_TabPageInvoiceDetails", gCI)

        'GroupBoxSOSummary.Text = gResourceMgr.GetString("frmSalesOrderCancel_GroupBoxSOSummary", gCI)
        'LbGrossAmt.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbGrossAmt", gCI)
        'LbDiscAmt.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbDiscAmt", gCI)
        'LbOtherCharges.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbOtherCharges", gCI)
        'LbNetAmount.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbNetAmount", gCI)
        'LbBalanceAmt.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbBalanceAmt", gCI)
        'LbTotalPaid.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbTotalPaid", gCI)
        'LbDeliveredAmt.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbDeliveredAmt", gCI)
        'LbRefundAmt.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbRefundAmt", gCI)
        'LbBalanceRefundAmt.Text = gResourceMgr.GetString("frmSalesOrderCancel_LbBalanceRefundAmt", gCI)

        'BtnSOCancel.Text = gResourceMgr.GetString("frmSalesOrderCancel_BtnSOCancel", gCI)
        'BtnSOSave.Text = gResourceMgr.GetString("frmSalesOrderCancel_BtnSOSave", gCI)
        'BtnSOPrint.Text = gResourceMgr.GetString("frmSalesOrderCancel_BtnSOPrint", gCI)
        'CtrlBtnPayBill.Text = gResourceMgr.GetString("frmSalesOrderCancel_BtnSOAcceptPayment", gCI)
    End Sub


#Region "Refresh Data Load "

    ''' <summary>
    ''' Refresh Article Scan Data in DataGrid
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RefreshLoadSOData() As Boolean
        Try
            '_dvDisplayItem = New DataView(_dsScan.Tables("ItemScanDetails"))
            Dim dtSource As DataTable = _dsScan.Tables("ItemScanDetails") ' _dvDisplayItem.ToTable(False, "EAN", "Discription", "SellingPrice", "Quantity", "DeliveredQty", "Discount", "NetAmount", "ExpDelDate", "Stock", "ArticleCode", "GrossAmt")

            If Not dtSource.Columns.Contains("Blankclm") Then
                AddBlankColumn(dtSource)
            End If

            grdSOItems.DataSource = dtSource

            grdSOItems.Cols("ArticleCode").Visible = True
            grdSOItems.Cols("GrossAmt").Visible = False
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Function

    ''' <summary>
    ''' Refresh Invoice Details Data in DataGrid
    ''' </summary>
    ''' <returns></returns>
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
            LogException(ex)
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

                lblOrderQty.Text = CDbl(dsScan.Tables("ItemScanDetails").Compute("SUM(Quantity)", "IsStatus ='Inserted'"))

                lblPickupQty.Text = CDbl(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(DeliveredQty)", "IsStatus ='Inserted'") Is DBNull.Value, 0, _
                                           dsScan.Tables("ItemScanDetails").Compute("SUM(DeliveredQty)", "IsStatus ='Inserted'")))


                CtrlCashSummary.lbltxt2 = FormatNumber(CDbl(dsScan.Tables("ItemScanDetails").Compute("SUM(LineDiscount)", "IsStatus ='Inserted'").ToString()), 2)

                If Not (dsMain.Tables("SalesOrderOtherCharges") Is Nothing) AndAlso dsMain.Tables("SalesOrderOtherCharges").Rows.Count > 0 Then
                    Dim vChargeAmount As String = IIf((dsMain.Tables("SalesOrderOtherCharges").Compute("Sum(ChargeAmount)", "") Is DBNull.Value), 0, dsMain.Tables("SalesOrderOtherCharges").Compute("Sum(ChargeAmount)", ""))
                    Dim vTaxAmount As String = IIf((dsMain.Tables("SalesOrderOtherCharges").Compute("Sum(TaxAmt)", "") Is DBNull.Value), 0, dsMain.Tables("SalesOrderOtherCharges").Compute("Sum(TaxAmt)", ""))
                    CtrlCashSummary.lbltxt3 = FormatNumber(CDbl(vChargeAmount) + CDbl(vTaxAmount), 2)
                Else
                    CtrlCashSummary.lbltxt3 = 0.0
                End If

                If Not (CDbl(CtrlCashSummary.lbltxt3) = 0.0) Then
                    '---Dated -27-08-2015 :  Chaitanya as we need to shown gross amount here
                    'CtrlCashSummary.lbltxt1 = FormatNumber(CDbl(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(NetAmount)", "IsStatus ='Inserted'") Is DBNull.Value, 0, _
                    '                             dsScan.Tables("ItemScanDetails").Compute("SUM(NetAmount)", "IsStatus ='Inserted'") + CDbl(CtrlCashSummary.lbltxt3))), 2)

                    CtrlCashSummary.lbltxt1 = FormatNumber(CDbl(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(GrossAmt)", "IsStatus ='Inserted'") Is DBNull.Value, 0, _
                                                  dsScan.Tables("ItemScanDetails").Compute("SUM(GrossAmt)", "IsStatus ='Inserted'") + CDbl(CtrlCashSummary.lbltxt3))), 2)

                Else
                    '---Dated -27-08-2015 :  Chaitanya as we need to shown gross amount here
                    'CtrlCashSummary.lbltxt1 = FormatNumber(CDbl(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(NetAmount)", "IsStatus ='Inserted'") Is DBNull.Value, 0, _
                    '                           dsScan.Tables("ItemScanDetails").Compute("SUM(NetAmount)", "IsStatus ='Inserted'"))), 2)
                    CtrlCashSummary.lbltxt1 = FormatNumber(CDbl(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(GrossAmt)", "IsStatus ='Inserted'") Is DBNull.Value, 0, _
                                                dsScan.Tables("ItemScanDetails").Compute("SUM(GrossAmt)", "IsStatus ='Inserted'"))), 2)
                End If

                'CtrlCashSummary.lbltxt4 = FormatNumber(CDbl(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(NetAmount)", "IsStatus ='Inserted'") Is DBNull.Value, 0, _
                '                                  dsScan.Tables("ItemScanDetails").Compute("SUM(NetAmount)", "IsStatus ='Inserted'") + CDbl(CtrlCashSummary.lbltxt3) - CDbl(CtrlCashSummary.lbltxt2))), 2)
                '---Dated -27-08-2015 : MAhesh As Table's Net Amount already have discounted value so here no need to subtract discount again
                CtrlCashSummary.lbltxt4 = FormatNumber(CDbl(IIf(dsScan.Tables("ItemScanDetails").Compute("SUM(NetAmount)", "IsStatus ='Inserted'") Is DBNull.Value, 0, _
                                                                  dsScan.Tables("ItemScanDetails").Compute("SUM(NetAmount)", "IsStatus ='Inserted'") + CDbl(CtrlCashSummary.lbltxt3))), 2)

                CtrlCashSummary.lbltxt4 = MyRound(CDbl(CtrlCashSummary.lbltxt4), clsDefaultConfiguration.BillRoundOffAt)
                CtrlCashSummary.lbltxt4 = CDbl(CtrlCashSummary.lbltxt4)

                Dim AmtPaid As Double = 0
                Dim crdsaleadjustamount As Double = 0
                Dim salesordernumber As String = ""
                Dim NetCrdSale As Double = 0
                Dim crdsale As Double = 0.0


                If dsMain.Tables("SalesInvoice").Rows.Count > 0 AndAlso Not dsMain.Tables("SalesInvoice") Is Nothing Then
                    AmtPaid = CDbl(dsMain.Tables("SalesInvoice").Compute("Sum(AmountTendered)", ""))
                    If dsMain.Tables("SalesInvoice").Select("TenderHeadCode='Credit Sales'").Length > 0 Then
                        Dim dt As DataTable = dsMain.Tables("SalesInvoice").Select("TenderHeadCode='Credit Sales'").CopyToDataTable
                        If dt.Rows.Count > 0 AndAlso Not dt Is Nothing Then
                            For Each x In dt.Rows
                                crdsale = x("AmountTendered")
                                salesordernumber = x("DocumentNumber")
                            Next
                        End If
                    End If
                    If crdsale > 0 Then
                        AmtPaid = AmtPaid - crdsale
                    End If
                End If
                Dim dtCreditSaleData As New DataTable
                Dim objclsReturn As New clsCashMemoReturn
                dtCreditSaleData = objclsReturn.getCreditSaleBillData("'" + salesordernumber + "'")
                If dtCreditSaleData.Rows.Count > 0 AndAlso Not dtCreditSaleData Is Nothing Then
                    For Each y In dtCreditSaleData.Rows
                        crdsaleadjustamount = y("CreditSaleAdjustment")
                    Next
                End If
                If crdsaleadjustamount > 0 Or crdsale > 0 Then
                    NetCrdSale = crdsale - crdsaleadjustamount
                    AmtPaid = AmtPaid + crdsaleadjustamount
                End If
              
                ' SwipePosition(CtrlCashSummary)

                CtrlCashSummary.lbltxt10 = NetCrdSale
                CtrlCashSummary.lbltxt5 = FormatNumber(AmtPaid, 2)
                CtrlCashSummary.lbltxt6 = FormatNumber(CDbl(CtrlCashSummary.lbltxt4) - CDbl(CtrlCashSummary.lbltxt5), 2)

                Dim dvDeliveredAmt As New DataView(dsScan.Tables("ItemScanDetails"), "DeliveredQty >0 and IsStatus ='Inserted'", "", DataViewRowState.OriginalRows)
                If dvDeliveredAmt.Count > 0 Then
                    Dim vDeliveredAmt As Double = 0.0
                    For Each drView As DataRowView In dvDeliveredAmt
                        vDeliveredAmt = vDeliveredAmt + CDbl(drView.Item("SellingPrice")) * CDbl(drView.Item("DeliveredQty"))
                    Next
                    CtrlCashSummary.lbltxt7 = FormatNumber(vDeliveredAmt, 2)
                End If

                CtrlCashSummary.lbltxt8 = FormatNumber(CDbl(CtrlCashSummary.lbltxt5) - CDbl(CtrlCashSummary.lbltxt7), 2)
                CtrlCashSummary.lbltxt9 = FormatNumber(CDbl(CtrlCashSummary.lbltxt8), 2)

                CtrlCashSummary.lbltxt5 = MyRound(CDbl(CtrlCashSummary.lbltxt5), clsDefaultConfiguration.BillRoundOffAt)
                CtrlCashSummary.lbltxt6 = MyRound(CDbl(CtrlCashSummary.lbltxt6), clsDefaultConfiguration.BillRoundOffAt)

            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CMR08"), "CMR08 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Function

    ''' <summary>
    ''' Show the image of the Current Selected Article
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdSOItems_RowColChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSOItems.RowColChange
        Try
            If (grdSOItems.Row >= 1) Then

                grdSOItems.Cols("ArticleCode").Visible = True
                Dim vArticleCode As String = grdSOItems.Item(grdSOItems.Row, "ArticleCode")
                grdSOItems.Cols("ArticleCode").Visible = True

                'Dim strUrl As String = objComn.GetArticleImage(vArticleCode, My.Settings.ArticleImageFolder)
                Dim strUrl As String = objComn.GetArticleImage(vArticleCode, ReadSpectrumParamFile("ArticleImageFolder"))
                'PictureBoxImages.ImageLocation = strUrl
                CtrlProductImage.ShowArticleImage(vArticleCode)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Clears All Resource for new sales order
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ResetSalesOrder() As Boolean
        Try
            If dsScan.Tables("ItemScanDetails").Rows.Count > 0 Then
                dsScan.Clear()
            End If
            RefreshLoadSOData()

            If dsInvoice.Tables.Count > 0 AndAlso dsInvoice.Tables("InvoiceDetails").Rows.Count > 0 Then
                dsInvoice.Clear()
            End If
            RefreshLoadInvcData()

            If Not (dsMain Is Nothing) Then
                dsMain.Clear()
            End If
            If Not (dsPayment Is Nothing) Then
                dsPayment.Clear()
            End If
            If Not (dtCustmInfo Is Nothing) Then
                dtCustmInfo.Clear()
            End If

            CtrlSalesInfo.CtrlTxtOrderNo.Focus()
            CtrlSalesInfo.CtrlTxtOrderNo.Text = ""
            CtrlSalesInfo.CtrldtOrderDt.Value = DBNull.Value
            CtrlSalesInfo.CtrlDtExpDelDate.Value = DBNull.Value
            CtrlSalesInfo.CtrlTxtRemarks.Text = ""
            CtrlSalesInfo.CtrlTxtCustOrdRef.Text = ""

            CtrlProductImage.ClearImage()
            CtrlSalesPerson.CtrlSalesPersons.SelectedIndex = -1

            CtrlCustDtls1.pClear()

            lblOrderQty.Text = 0
            lblPickupQty.Text = 0
            CtrlCashSummary.lbltxt1 = strZero  ' Grossamt
            CtrlCashSummary.lbltxt2 = strZero  ' Discount amt
            CtrlCashSummary.lbltxt4 = strZero  ' Other Charges
            CtrlCashSummary.lbltxt3 = strZero  ' Net amt

            CtrlCashSummary.lbltxt5 = strZero  'total Paid
            CtrlCashSummary.lbltxt6 = strZero  'Balance to Pay
            CtrlCashSummary.lbltxt7 = strZero  ' Delivered Amt
            CtrlCashSummary.lbltxt8 = strZero  'Refund Amt
            CtrlCashSummary.lbltxt9 = strZero   'Balance Refund Amt

            rbbtnSave.Enabled = False
            'CtrlBtnPayBill.Enabled = False
            CtrlRbn1.DgrpPayments.Enabled = False
            'BtnSOPrint.Enabled = False
            BtnSOCancel.Enabled = False

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function

#End Region

#Region "Save/Update Sales Order "

    ''' <summary>
    ''' Preapring the Sales Order data for save
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 

    Private Sub BtnSaveSalesOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbbtnSave.Click
        Try
            If Not (dsScan.Tables(0).Rows.Count > 0) Then
                ShowMessage(getValueByKey("SO027"), "SO027 - " & getValueByKey("CLAE04"))
                'ShowMessage("Please Select Sales Order first", "Sales Order Information")
                Exit Sub
            End If
            Dim TotalDeliveredQty As Double = 0.0
            Dim TotalDelQty As Object = dsScan.Tables("ItemScanDetails").Compute("Sum(DeliveredQty)", "DeliveredQty>0")
            TotalDeliveredQty = IIf(TotalDelQty Is DBNull.Value, 0.0, TotalDelQty)
            If TotalDeliveredQty > 0 Then
                ShowMessage(getValueByKey("SO029") + " " + CtrlSalesInfo.CtrlTxtOrderNo.Text, "SO029 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If

            If Not (dsPayment.Tables("MSTRecieptType") Is Nothing) AndAlso Not (dsPayment.Tables("MSTRecieptType").Rows.Count > 0) Then
                ShowMessage(String.Format(getValueByKey("SO031"), clsAdmin.CurrencySymbol, CtrlCashSummary.lbltxt8), "SO031 - " & getValueByKey("CLAE04"))
                Exit Sub
            ElseIf CInt(CDbl(CtrlCashSummary.lbltxt9)) > 0 Then
                ShowMessage(String.Format(getValueByKey("SO031"), clsAdmin.CurrencySymbol, CtrlCashSummary.lbltxt8), "SO031 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If


            'If IsSalesOrderCancel = True Then
            If dsMain.Tables("SalesOrderHDR").Rows.Count > 0 AndAlso dsMain.Tables("SalesOrderDTL").Rows.Count > 0 Then

                _drSiteInfo = objComn.GetSiteInfo(vSiteCode).Rows(0)

                For Each drHdr As DataRow In dsMain.Tables("SalesOrderHDR").Rows
                    drHdr("SOStatus") = "Cancel"
                    drHdr("UPDATEDON") = vCurrentDate
                    drHdr("UPDATEDBY") = clsAdmin.UserCode
                    drHdr("UPDATEDAT") = clsAdmin.SiteCode
                Next
                For Each drDtl As DataRow In dsMain.Tables("SalesOrderDTL").Rows
                    drDtl("ArticleStatus") = "Cancel"
                Next

                If Not (PrepareInvcDataforSave(dsMain) = True) Then
                    Exit Sub
                End If
                Dim SalesPersonName As String = String.Empty
                SalesPersonName = IIf(CtrlSalesPerson.CtrlSalesPersons.SelectedIndex < 0, "", CtrlSalesPerson.CtrlSalesPersons.Text)
                Dim SalesOrderNumber = dsMain.Tables("SalesOrderhDR").Rows(0)("SaleOrderNumber")
                Dim dsOtherCharges As New DataSet
                dsOtherCharges.Clear()
                Dim dt = objCharge.GetDtOtherCharge(clsAdmin.SiteCode, IIf(SalesOrderNumber = String.Empty, 0, SalesOrderNumber))
                dsOtherCharges.Tables.Add(dt.Copy())
                dsOtherCharges.Tables(0).TableName = "NewOtherCharges"

                If objSO.PrepareSaveData(SalesInvoiceNbr, clsAdmin.DayOpenDate, clsAdmin.CLPProgram, "", dsMain, False, IsNextInvcNo, clsAdmin.SiteCode, CtrlSalesInfo.CtrlTxtOrderNo.Text, clsDefaultConfiguration.StockStorageLocation, clsAdmin.CVProgram, "SalesOrder", vfinancialYear, clsAdmin.UserCode, clsAdmin.CurrentDate, False, CVoucherNo, CVVoucherDay) = True Then
                    'Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(clsDefaultConfiguration.PrintPreivewReq, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserCode, "", CtrlCustDtls1.dtCustmInfo, dsScan.Tables("ItemScanDetails"), Nothing, "")
                    'PrintCreditVoucher(drSiteInfo)

                    Dim clsVoucher As New SpectrumPrint.clsPrintVoucher
                    Dim ReturnAmt As Double = 0.0



                    clsVoucher.PrintGiftVoucherAndCreditNote("SalesOrder", clsAdmin.SiteCode, "CreditNote", String.Empty, ReturnAmt, String.Empty, clsAdmin.UserName, CtrlSalesInfo.CtrlTxtOrderNo.Text, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)

                    Dim strSOStatus As String = ""
                    If Not dsMain Is Nothing AndAlso dsMain.Tables.Contains("SalesOrderHDR") AndAlso Not dsMain.Tables("SalesOrderHDR").Columns("SOStatus") Is DBNull.Value Then
                        strSOStatus = "Cancel"
                    End If
                    Dim objclsSalesOrder As New SpectrumPrint.clsPrintSalesOrder(clsDefaultConfiguration.PrintPreivewReq, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, clsAdmin.CurrencyDescription, clsAdmin.UserName, vSalesNo, dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), "", "", "", clsDefaultConfiguration.BillRoundOffAt, "", dtPrinterInfo, strSOStatus, dsOtherCharges, ShowFullName:=clsDefaultConfiguration.PrintItemFullName, strSalesPerson:=SalesPersonName)

                    If Not (dsPayment.Tables("MSTRecieptType") Is Nothing) AndAlso Not (dsPayment.Tables("MSTRecieptType").Rows.Count > 0) Then
                        ReturnAmt = CDbl(dsPayment.Tables("MSTRecieptType").Compute("Sum(Amount)", "RecieptType In ('CreditVouc(I)','Cash(R)')"))
                        If ReturnAmt < 0 Then
                            ReturnAmt = ReturnAmt * -1
                        End If
                    End If



                    ShowMessage(String.Format(getValueByKey("SO074"), CtrlSalesInfo.CtrlTxtOrderNo.Text), "SO074 - " & getValueByKey("CLAE04"))

                    ResetSalesOrder()
                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                End If

            Else
                ShowMessage(String.Format(getValueByKey("SO075"), CtrlSalesInfo.CtrlTxtOrderNo.Text), "SO075 - " & getValueByKey("CLAE04"))
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE04"))
            LogException(ex)
        End Try
    End Sub

    Private Function PrintCreditVoucher(ByVal drSite As DataRow) As Boolean
        Try
            Dim PrintSo As New System.Text.StringBuilder
            If dsScan Is Nothing Then
                Exit Function
            End If

            PrintSo.Length = 0
            'PrintSo.Append("                          CREDIT VOUCHER AGAINST CUSTOMER RETURN                                " & vbCrLf & vbCrLf)
            PrintSo.Append("                          " & getValueByKey("SOCVP001") & "                                " & vbCrLf & vbCrLf)
            'PrintSo.Append("Credit Note / Voucher No : " & CVoucherNo & "     Date    : " & Format(vCurrentDate, vDateFormat) & vbCrLf)
            PrintSo.Append(getValueByKey("SOCVP002") & CVoucherNo & "     " & getValueByKey("SOCVP007") & Format(vCurrentDate, vDateFormat) & vbCrLf)
            PrintSo.Append("----------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)

            If vHeaderNote = True Then
                'PrintSo.Append(vbCrLf & "Header Information is Welcome" & vbCrLf)
                PrintSo.Append(vbCrLf & getValueByKey("SOCVP003") & vbCrLf)
            End If


            If Not (drSite Is Nothing) Then
                'PrintSo.Append("Store Name         : " & drSite.Item("SiteOfficialName") & "                Store Code   : " & vSiteCode & vbCrLf)
                'PrintSo.Append("Store Address       : " & drSite.Item("SiteAddressLn1") & vbCrLf & "                            " & _
                '             drSite.Item("SiteAddressLn2") & " " & drSite.Item("SiteAddressLn3") & _
                '             drSite.Item("SitePinCode") & vbCrLf)
                PrintSo.Append(getValueByKey("SOCVP018") & drSite.Item("SiteOfficialName") & "                " & getValueByKey("SOCVP004") & vSiteCode & vbCrLf)
                PrintSo.Append(getValueByKey("SOCVP005") & drSite.Item("SiteAddressLn1") & vbCrLf & "                            " & _
                             drSite.Item("SiteAddressLn2") & " " & drSite.Item("SiteAddressLn3") & _
                             drSite.Item("SitePinCode") & vbCrLf)
            End If
            'PrintSo.Append("Store Tax Name1 :       Date    : " & Format(vCurrentDate, vDateFormat) & vbCrLf)
            'PrintSo.Append("Store Tax NameN :       Date    : " & Format(vCurrentDate, vDateFormat) & vbCrLf)
            PrintSo.Append(getValueByKey("SOCVP006") & "       " & getValueByKey("SOCVP007") & Format(vCurrentDate, vDateFormat) & vbCrLf)
            PrintSo.Append(getValueByKey("SOCVP008") & "       " & getValueByKey("SOCVP007") & Format(vCurrentDate, vDateFormat) & vbCrLf)

            If dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                'PrintSo.Append("Text Credit Note :" & vCurrencyCode & " " & CDbl(dsPayment.Tables("MSTRecieptType").Compute("Sum(Amount)", "")) & vbCrLf)
                PrintSo.Append(getValueByKey("SOCVP009") & vCurrencyCode & " " & CDbl(dsPayment.Tables("MSTRecieptType").Compute("Sum(Amount)", "")) & vbCrLf)
            End If
            'PrintSo.Append("         Applicable at :" & drSite.Item("SiteOfficialName") & vbCrLf)
            'PrintSo.Append("Issued against S.O. No. :" & CtrlSalesInfo.CtrlTxtOrderNo.Text.Trim & "   Date: " & Format(vCurrentDate, vDateFormat) & vbCrLf)

            PrintSo.Append(getValueByKey("SOCVP010") & drSite.Item("SiteOfficialName") & vbCrLf)
            PrintSo.Append(getValueByKey("SOCVP011") & CtrlSalesInfo.CtrlTxtOrderNo.Text.Trim & "   " & getValueByKey("SOCVP007") & Format(vCurrentDate, vDateFormat) & vbCrLf)

            'PrintSo.Append("Valid for " & CVVoucherDay & " days " & vbCrLf)
            'PrintSo.Append("From issue date if stamped and signed." & vbCrLf)
            'PrintSo.Append("Specific and unique numbering (same as the Voucher number)" & vbCrLf & vbCrLf)
            PrintSo.Append(getValueByKey("SOCVP012") & CVVoucherDay & getValueByKey("SOCVP019") & vbCrLf)
            PrintSo.Append(getValueByKey("SOCVP013") & vbCrLf)
            PrintSo.Append(getValueByKey("SOCVP014") & vbCrLf & vbCrLf)

            'PrintSo.Append("                                          _______________" & vbCrLf & vbCrLf)
            'PrintSo.Append("                                          Signed" & vbCrLf)
            PrintSo.Append("                                          _______________" & vbCrLf & vbCrLf)
            PrintSo.Append("                                          " & getValueByKey("SOCVP015") & vbCrLf)


            'PrintSo.Append("<Terms & Condition If Any>" & vbCrLf & vbCrLf)
            PrintSo.Append(getValueByKey("SOCVP016") & vbCrLf & vbCrLf)

            If vFooterNote = True Then
                'PrintSo.Append(vbCrLf & "Footer Information is Welcome" & vbCrLf)
                PrintSo.Append(vbCrLf & getValueByKey("SOCVP017") & vbCrLf)
            End If

            'fnPrint(PrintSo.ToString, "")      'Print Preview
            fnPrint(PrintSo.ToString, "PRN")   'Direct Print

            'PrintSo.Append("")                 'Set Debug Point

        Catch ex As Exception
            ShowMessage(getValueByKey("SO030"), "SO030 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Credit Voucher Against Customer Return does not print", "Print Sales Order Information")
        End Try
    End Function

    ''' <summary>
    ''' Preapring the Sales Order Invoice data for save
    ''' </summary>
    ''' <param name="dsMain"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrepareInvcDataforSave(ByRef dsMain As DataSet) As Boolean
        Try
            If Not (dsPayment Is Nothing) AndAlso dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                Dim vSalesInvcNo As String = ""

                If OnlineConnect = True Then
                    'Changed by Rohit to generate Document No. for proper sorting
                    Try
                        'vSalesInvcNo = "CM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2)
                        ''GST changes by ketan add sitecode in billno  
                        vSalesInvcNo = "C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 3, 3) & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2)
                        vSalesInvcNo = GenDocNo(vSalesInvcNo, 15, objComn.getDocumentNo("CM", clsAdmin.SiteCode))
                    Catch ex As Exception
                        vSalesInvcNo = "C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 3, 3) & objComn.getDocumentNo("CM", clsAdmin.SiteCode)
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

                SalesInvoiceNbr = vSalesInvcNo
                Dim drInvc As DataRow
                Dim findKey(4) As Object
                Dim RowNo As Integer = 1
                Dim IsNewInvcRow As Boolean = False

                For Each drPayment As DataRow In dsPayment.Tables("MSTRecieptType").Rows
                    findKey(0) = vSiteCode
                    findKey(1) = vfinancialYear
                    findKey(2) = CtrlSalesInfo.CtrlTxtOrderNo.Text.Trim
                    findKey(3) = vSalesInvcNo
                    findKey(4) = RowNo

                    drInvc = dsMain.Tables("SalesInvoice").Rows.Find(findKey)
                    If drInvc Is Nothing Then
                        drInvc = dsMain.Tables("SalesInvoice").NewRow()
                        IsNewInvcRow = True
                        IsNextInvcNo = True
                    End If

                    drInvc("SiteCode") = clsAdmin.SiteCode
                    drInvc("FinYear") = vfinancialYear
                    drInvc("DocumentNumber") = CtrlSalesInfo.CtrlTxtOrderNo.Text.Trim
                    drInvc("SaleInvNumber") = vSalesInvcNo
                    drInvc("DocumentType") = vDocType

                    drInvc("SaleInvLineNumber") = drPayment("SrNo")
                    drInvc("TerminalID") = clsAdmin.TerminalID
                    drInvc("TenderTypeCode") = drPayment("RecieptTypeCode")
                    drInvc("TenderHeadCode") = drPayment("RecieptType")
                    'If drInvc("TenderTypeCode") = "CreditVouc(I)" Or drInvc("TenderTypeCode") = "Cash(R)" Then
                    '    drInvc("AmountTendered") = CDbl(drPayment("Amount"))
                    '    drInvc("RefNo_2") = CVoucherNo 'drPayment("Number")
                    'Else
                    drInvc("AmountTendered") = drPayment("Amount")
                    drInvc("RefNo_1") = clsCommon.ConvertToEnglish(drPayment("AMOUNTINCURRENCY")) 'drPayment("Number")
                    'End If

                    drInvc("ExchangeRate") = drPayment("ExchangeRate")
                    drInvc("CurrencyCode") = drPayment("CurrencyCode")
                    drInvc("SOInvDate") = clsAdmin.DayOpenDate.Date
                    drInvc("SOInvTime") = Format(vCurrentDate, "hh:mm:ss tt")
                    drInvc("UserName") = clsAdmin.UserName
                    drInvc("ManagersKeytoUpdate") = DBNull.Value
                    drInvc("ChangeLine") = DBNull.Value

                    drInvc("RefNo_2") = drPayment("Number")
                    drInvc("RefNo_3") = DBNull.Value
                    drInvc("RefNo_4") = DBNull.Value
                    drInvc("RefDate") = drPayment("Date")
                    drInvc("CREATEDAT") = clsAdmin.SiteCode
                    drInvc("CREATEDBY") = clsAdmin.UserCode
                    drInvc("CREATEDON") = vCurrentDate
                    drInvc("UPDATEDAT") = clsAdmin.SiteCode
                    drInvc("UPDATEDBY") = clsAdmin.UserCode
                    drInvc("UPDATEDON") = vCurrentDate
                    drInvc("STATUS") = True

                    If IsNewInvcRow = True Then
                        dsMain.Tables("SalesInvoice").Rows.Add(drInvc)
                        RowNo = RowNo + 1
                    End If

                Next

            End If
            Return True

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function
    Private Sub BtnPayCash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub
    Private Sub BtnPayCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub
    Private Sub BtnPayCheque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub


    ''' <summary>
    ''' Accept Payment for current Sales Order
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnAcceptPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Not (dsScan.Tables(0).Rows.Count > 0) Then
                ShowMessage(getValueByKey("SO027"), "SO027 - " & getValueByKey("CLAE04"))
                'ShowMessage("Please Select Sales Order first", "Sales Order Information")
                Exit Sub
            End If
            Dim TotalDeliveredQty As Double = 0.0
            Dim TotalDelQty As Object = dsScan.Tables("ItemScanDetails").Compute("Sum(DeliveredQty)", "DeliveredQty>0")
            TotalDeliveredQty = IIf(TotalDelQty Is DBNull.Value, 0.0, TotalDelQty)
            If TotalDeliveredQty > 0 Then
                ShowMessage(getValueByKey("SO029") & CtrlSalesInfo.CtrlTxtOrderNo.Text.Trim, "SO029 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            vReceivedAmt = 0
            Dim objPayment As New frmNAcceptPayment()
            objPayment.ParentRelation = "SalesOrder"
            objPayment.TotalBillAmount = CDbl(CDbl(CtrlCashSummary.lbltxt9) * -1)
            objPayment.MinimumBillAmount = CDbl(CDbl(CtrlCashSummary.lbltxt9) * -1)
            objPayment.PaymentType = clsAcceptPayment.PaymentType.Advance
            objPayment.ShowDialog(Me)

            _dsPayment = New DataSet
            _dsPayment = objPayment.ReciptTotalAmount()
            If Not (dsPayment Is Nothing) AndAlso dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables(0).Rows.Count > 0 Then
                vReceivedAmt = CDbl(dsPayment.Tables(0).Compute("Sum(Amount)", ""))

                CtrlCashSummary.lbltxt9 = FormatNumber(CDbl(CtrlCashSummary.lbltxt8) - IIf(vReceivedAmt < 0, (-1 * vReceivedAmt), vReceivedAmt), 2)
            End If

            objPayment.Close()
            If objPayment.Action = "Save" Then
                BtnSaveSalesOrder_Click(sender, e)
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

#End Region

    Private Sub BtnSOCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSOCancel.Click
        If Not (dsScan.Tables(0).Rows.Count > 0) Then
            ShowMessage(getValueByKey("SO027"), "SO027 - " & getValueByKey("CLAE04"))
            'ShowMessage("Please Select Sales Order first", "Sales Order Information")
            Exit Sub
        End If
        If (String.IsNullOrEmpty(vSalesNo) Or CtrlSalesInfo.CtrlTxtOrderNo.Text.Trim <> vSalesNo Or grdSOItems.Rows.Count <= 1) Then
            ShowMessage(getValueByKey("frmnquotationupdate.Warning"), getValueByKey("CLAE04"))
            Exit Sub
        End If

        Dim TotalDelQty As Object
        Dim TotalDeliveredQty As Double = 0.0
        Dim crdsaleadjustamount As Double = 0.0
        Dim crdsale As Double = 0.0
        TotalDelQty = dsScan.Tables("ItemScanDetails").Compute("Sum(DeliveredQty)", "DeliveredQty>0")
        TotalDeliveredQty = IIf(TotalDelQty Is DBNull.Value, 0.0, TotalDelQty)

        If MsgBox(getValueByKey("SO028"), MsgBoxStyle.YesNo, "SO028 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
            If TotalDeliveredQty > 0 Then
                ShowMessage(getValueByKey("SO029") & " " & CtrlSalesInfo.CtrlTxtOrderNo.Text.Trim, "SO029 - " & getValueByKey("CLAE04"))
                AutoLogout(FrmTranCode, Me, lblLoggedIn)
                Exit Sub
            Else
                'ShowMessage("Sales Order No. " & CtrlSalesInfo.CtrlTxtOrderNo.Text.Trim & " is ready to be canceled", "Cancel Sales Order")
                'BtnAcceptPayment_Click(Me, New System.EventArgs)

                Dim articlePrice = Decimal.Zero, pendingTotalNetAmount = Decimal.Zero, advanceAmountPaid = Decimal.Zero, CVIssueAmount As Decimal = Decimal.Zero
                Dim penaltyInPercentage As Decimal = Decimal.Zero
                Try
                    For Each drDtls As DataRow In dsMain.Tables("SalesOrderDtl").Rows

                        articlePrice = drDtls("NetAmount") / drDtls("Quantity")
                        pendingTotalNetAmount += articlePrice * (drDtls("Quantity") - drDtls("DeliveredQty"))
                    Next

                    
                    '----- Commented By Mahesh Here we go for Extra Advance Amt i.e. Advance Amt rec - Picked up Amt 
                    '---- Therefore for so cancel  pickup amt  is Zero so directly Assigned this Amt as Advance Amt
                    'If (dsMain.Tables("SalesOrderHDR").Rows(0)("BalanceAmount") = 0) Then
                    '    advanceAmountPaid = 0
                    'Else
                    '    advanceAmountPaid = pendingTotalNetAmount - dsMain.Tables("SalesOrderHDR").Rows(0)("BalanceAmount")
                    'End If
                    advanceAmountPaid = dsMain.Tables("SalesOrderHDR").Rows(0)("AdvanceAmt")

                    Dim salesordernumber As String = ""
                    If dsMain.Tables("SalesInvoice").Rows.Count > 0 AndAlso Not dsMain.Tables("SalesInvoice") Is Nothing Then
                        If dsMain.Tables("SalesInvoice").Select("TenderHeadCode='Credit Sales'").Length > 0 Then
                            Dim dt As DataTable = dsMain.Tables("SalesInvoice").Select("TenderHeadCode='Credit Sales'").CopyToDataTable
                            If dt.Rows.Count > 0 AndAlso Not dt Is Nothing Then
                                For Each x In dt.Rows
                                    crdsale += x("AmountTendered")
                                    salesordernumber = x("DocumentNumber")
                                Next
                            End If
                        End If
                    End If
                    If crdsale > 0 Then
                        advanceAmountPaid = advanceAmountPaid - crdsale
                    End If
                    Dim dtCreditSaleData As New DataTable
                    Dim objclsReturn As New clsCashMemoReturn
                    dtCreditSaleData = objclsReturn.getCreditSaleBillData("'" + salesordernumber + "'")
                    If dtCreditSaleData.Rows.Count > 0 AndAlso Not dtCreditSaleData Is Nothing Then
                        For Each y In dtCreditSaleData.Rows
                            crdsaleadjustamount += y("CreditSaleAdjustment")
                        Next
                    End If

            If (clsDefaultConfiguration.PenaltyPercentageInCancel = 0) Then
                CVIssueAmount = advanceAmountPaid

            ElseIf (clsDefaultConfiguration.PenaltyPercentageInCancel > 0) Then

                If (DirectCast(dsMain.Tables("SalesOrderHdr").Rows(0)("CreatedOn"), Date).AddDays(clsDefaultConfiguration.NoofdaysAfterPenaltyApplicable).Date > clsAdmin.CurrentDate.Date) Then
                    penaltyInPercentage = 0
                    CVIssueAmount = advanceAmountPaid
                Else
                    penaltyInPercentage = clsDefaultConfiguration.PenaltyPercentageInCancel
                    If (advanceAmountPaid = 0) Then
                        CVIssueAmount = ((clsDefaultConfiguration.PenaltyPercentageInCancel * pendingTotalNetAmount) / 100) * -1

                    ElseIf (advanceAmountPaid > 0) Then
                        CVIssueAmount = advanceAmountPaid - ((clsDefaultConfiguration.PenaltyPercentageInCancel * pendingTotalNetAmount) / 100)
                    End If
                End If
            End If
                Catch ex As Exception
                ShowMessage(ex.Message, getValueByKey("CLAE05"))
                LogException(ex)
            End Try
                Dim PaymentToBeSettled As Double = 0.0
                Dim PenaltyAmount As Double = 0.0
                PenaltyAmount = ((clsDefaultConfiguration.PenaltyPercentageInCancel * pendingTotalNetAmount) / 100)

                If crdsale > 0 Then
                    PaymentToBeSettled = -(advanceAmountPaid + crdsaleadjustamount) + PenaltyAmount
                Else
                    PaymentToBeSettled = CVIssueAmount
                End If



                If (advanceAmountPaid <> 0 Or CVIssueAmount <> 0) Then
                    If (crdsaleadjustamount = 0) Then
                        If MsgBox(String.Format(getValueByKey("SO091"),
                                      MyRound(advanceAmountPaid, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired),
                                      penaltyInPercentage,
                                      MyRound(CVIssueAmount * -1, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)),
                                 MsgBoxStyle.YesNo, "SO091 - " & getValueByKey("SO089")) = MsgBoxResult.No Then
                            Exit Sub
                        End If
                    Else
                        If MsgBox(String.Format(getValueByKey("SO090"),
                                                                    MyRound(advanceAmountPaid, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired),
                                                                    crdsaleadjustamount,
                                                                    penaltyInPercentage,
                                                                    MyRound(PenaltyAmount, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired),
                                                                     MyRound(PaymentToBeSettled, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)),
                                                               MsgBoxStyle.YesNo, "SO090 - " & getValueByKey("SO089")) = MsgBoxResult.No Then
                            Exit Sub
                        End If
                    End If
                End If

                Try
                    If crdsaleadjustamount > 0 Then
                        CVIssueAmount = MyRound(PaymentToBeSettled, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)
                    Else
                        CVIssueAmount = MyRound(CVIssueAmount * -1, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)
                    End If

                    If CVIssueAmount <> 0 Then
                        'ShowMessage("Need to pay remaining amount is " & Math.Round(vReturnAmount, 2) & " because Delivered Amount is " & vTotalDeliveredAmount, "Payment Information")
                        'ShowMessage(String.Format(getValueByKey("SO081"), Math.Round(vReturnAmount, 2), vTotalDeliveredAmount), "SO081 - " & getValueByKey("CLAE04"))

                        Dim objPayment As New frmNAcceptPayment()
                        objPayment.TotalBillAmount = CVIssueAmount
                        objPayment.CreditSettlement = True
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
                    dsMain.Tables("SalesOrderHDR").Rows(0).Item("SOStatus") = "Cancel"
                    dsMain.Tables("SalesOrderHDR").Rows(0).Item("UPDATEDON") = vCurrentDate
                    dsMain.Tables("SalesOrderHDR").Rows(0).Item("UPDATEDBY") = clsAdmin.UserCode
                    dsMain.Tables("SalesOrderHDR").Rows(0).Item("UPDATEDAT") = clsAdmin.SiteCode

                    If Not (PrepareInvcDataforSave(dsMain) = True) Then
                        Exit Sub
                    End If

                    If objSO.PrepareSaveData(CtrlSalesInfo.CtrlTxtOrderNo.Text.Trim, clsAdmin.DayOpenDate, clsAdmin.CLPProgram, CtrlCustDtls1.lblCustNo.Text, dsMain, False, 0, vSiteCode, CtrlSalesInfo.CtrlTxtOrderNo.Text, clsDefaultConfiguration.StockStorageLocation, clsAdmin.CVProgram, "SalesOrder", vfinancialYear, clsAdmin.UserCode, clsAdmin.CurrentDate, IsOutboundCreated) = True Then

                        If Not dsPayment.Tables("MSTRecieptType") Is Nothing AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                            Dim totalPay As Decimal
                            For Each dr As DataRow In dsPayment.Tables("MSTRecieptType").Select("RecieptTypeCode='CreditVouc(I)'", "", DataViewRowState.CurrentRows)
                                totalPay = IIf(dr("Amount") > 0, dr("Amount"), dr("Amount") * -1)

                                Dim clsVoucher As New SpectrumPrint.clsPrintVoucher
                                clsVoucher.PrintGiftVoucherAndCreditNote("SalesOrder", clsAdmin.SiteCode, "CreditNote", String.Empty, totalPay, String.Empty, clsAdmin.UserName, CtrlSalesInfo.CtrlTxtOrderNo.Text, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                            Next
                        End If

                        ShowMessage(getValueByKey("SO039"), "SO039 - " & getValueByKey("CLAE04"))

                        IsSalesOrderCancel = True
                        Dim ChildForm As New frmNSalesOrderCreation
                        Try
                            If ChildForm.Name <> String.Empty Then
                                IsFormClosing = True
                                MDISpectrum.ShowChildForm(ChildForm, True)
                                Exit Sub
                            End If
                        Catch ex As Exception
                            ChildForm.Close()
                        End Try
                        'ResetSalesOrder()
                        CtrlSalesInfo.CtrlTxtOrderNo.Text = ""
                    Else
                        ShowMessage(getValueByKey("SO040"), "SO040 - " & getValueByKey("CLAE04"))
                        'ShowMessage("Sales Order does not Updated", "Sales Order")
                    End If

                Catch ex As Exception
                    ShowMessage(ex.Message, getValueByKey("CLAE05"))
                    LogException(ex)
                End Try

                End If


                rbbtnSave.Enabled = True
                'CtrlBtnPayBill.Enabled = True
                CtrlRbn1.DgrpPayments.Enabled = True


        End If

    End Sub

    'Private Sub BtnSOPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSOPrint.Click
    '    ShowMessage("Print Sales Order service currently not available", "Print Sales Order Informaion")
    'End Sub

    Private Sub BtnSOClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles BtnSOClose.Click
        If dsScan.Tables(0).Rows.Count > 0 Then
            If MsgBox(getValueByKey("SO026"), MsgBoxStyle.YesNo, "SO026 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                Me.Close()
            End If
        Else
            Me.Close()
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
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "ExpDelDate".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "Stock".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "ExclTaxAmt".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "DeliveredQty".ToUpper() _
                    AndAlso grdSOItems.Cols(colno).Name.ToUpper() <> "NetAmount".ToUpper() Then
                    HideColumns(grdSOItems, False, grdSOItems.Cols(colno).Name)
                End If
            Next

            'grdSoItems.Cols("Del").Caption = ""
            'grdSoItems.Cols("Del").Width = 20
            If (clsDefaultConfiguration.BarcodeDisplayAllowed) Then
                grdSOItems.Cols("EAN").Caption = getValueByKey("frmnsalesordercancel.grdsoitems.ean")
                grdSOItems.Cols("EAN").Width = 90
                grdSOItems.Cols("EAN").AllowEditing = False
                grdSOItems.Cols("EAN").Visible = True
            Else
                grdSOItems.Cols("EAN").Visible = False
            End If

            grdSOItems.Cols("ArticleCode").Caption = getValueByKey("frmnsalesordercancel.grdsoitems.articlecode")
            grdSOItems.Cols("ArticleCode").Width = 90
            grdSOItems.Cols("Discription").Caption = getValueByKey("frmnsalesordercancel.grdsoitems.discription")
            grdSOItems.Cols("Discription").Width = 150
            grdSOItems.Cols("SellingPrice").Caption = getValueByKey("frmnsalesordercancel.grdsoitems.sellingprice")
            grdSOItems.Cols("SellingPrice").Width = 60
            grdSOItems.Cols("Quantity").Caption = getValueByKey("frmnsalesordercancel.grdsoitems.quantity")
            grdSOItems.Cols("Quantity").Width = 45
            grdSOItems.Cols("Quantity").Format = "0.000"
            'grdSOItems.Cols("PickUpQty").Caption = "Pick Up Qty"
            'grdSOItems.Cols("PickUpQty").Width = 45
            grdSOItems.Cols("DeliveredQty").Caption = getValueByKey("frmnsalesordercancel.grdsoitems.deliveredqty")
            grdSOItems.Cols("DeliveredQty").Width = 45
            grdSOItems.Cols("DeliveredQty").Format = "0.000"
            grdSOItems.Cols("TOTALDISCPERCENTAGE").Caption = getValueByKey("frmnsalesordercancel.grdsoitems.totaldiscpercentage")
            grdSOItems.Cols("TOTALDISCPERCENTAGE").Width = 45
            grdSOItems.Cols("NetAmount").Caption = getValueByKey("frmnsalesordercancel.grdsoitems.netamount")
            grdSOItems.Cols("NetAmount").Width = 70
            grdSOItems.Cols("ExpDelDate").Caption = getValueByKey("frmnsalesordercancel.grdsoitems.expdeldate")
            grdSOItems.Cols("ExpDelDate").Width = 95
            grdSOItems.Cols("Stock").Caption = getValueByKey("frmnsalesordercancel.grdsoitems.stock")
            grdSOItems.Cols("Stock").Width = 45
            grdSOItems.Cols("Stock").Visible = False
            grdSOItems.Cols("ExclTaxAmt").Caption = getValueByKey("frmnsalesordercancel.grdsoitems.excltaxamt")
            grdSOItems.Cols("ExclTaxAmt").Width = 45
            grdSOItems.Cols("ExclTaxAmt").AllowEditing = False

            grdSOItems.AutoSizeCols()
            For Each col As C1.Win.C1FlexGrid.Column In grdSOItems.Cols
                col.AllowEditing = False
            Next
            grdSOItems.pResizeCol()
            'If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            '    For i = 0 To grdSOItems.Cols.Count - 1
            '        grdSOItems.Cols(i).Caption = grdSOItems.Cols(i).Caption.ToUpper
            '    Next
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub GridInvoiceSetting()
        Try

            'grdSOInvoice.Cols("Del").Caption = ""
            'grdSOInvoice.Cols("Del").Width = 20
            grdSOInvoice.Cols("SalesNO").Caption = getValueByKey("frmnsalesordercancel.grdsoinvoice.salesno")
            grdSOInvoice.Cols("SalesNO").Width = 90
            grdSOInvoice.Cols("InvoiceNO").Caption = getValueByKey("frmnsalesordercancel.grdsoinvoice.invoiceno")
            grdSOInvoice.Cols("InvoiceNO").Width = 90
            grdSOInvoice.Cols("DocumentType").Caption = getValueByKey("frmnsalesordercancel.grdsoinvoice.documenttype")
            grdSOInvoice.Cols("DocumentType").Width = 60
            grdSOInvoice.Cols("TerminalID").Caption = getValueByKey("frmnsalesordercancel.grdsoinvoice.terminalid")
            grdSOInvoice.Cols("TerminalID").Width = 45
            grdSOInvoice.Cols("UserName").Caption = getValueByKey("frmnsalesordercancel.grdsoinvoice.username")
            grdSOInvoice.Cols("UserName").Width = 90
            grdSOInvoice.Cols("InvoiceDate").Caption = getValueByKey("frmnsalesordercancel.grdsoinvoice.invoicedate")
            grdSOInvoice.Cols("InvoiceDate").Width = 70
            grdSOInvoice.Cols("TenderType").Caption = getValueByKey("frmnsalesordercancel.grdsoinvoice.tendertype")
            grdSOInvoice.Cols("TenderType").Width = 45
            grdSOInvoice.Cols("InvoiceAmt").Caption = getValueByKey("frmnsalesordercancel.grdsoinvoice.invoiceamount")
            grdSOInvoice.Cols("InvoiceAmt").Width = 70
            For Each col As C1.Win.C1FlexGrid.Column In grdSOInvoice.Cols
                col.AllowEditing = False
            Next
            grdSOInvoice.pResizeCol()
            'If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            '    For i = 0 To grdSOInvoice.Cols.Count - 1
            '        grdSOInvoice.Cols(i).Caption = grdSOInvoice.Cols(i).Caption.ToUpper
            '    Next
            'End If
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
    Protected Overrides Function ProcessKeyPreview(ByRef m As System.Windows.Forms.Message) As Boolean
        Const WM_KEYDOWN As Integer = &H100
        If m.Msg = WM_KEYDOWN Then
            Select Case m.WParam.ToInt32
                Case Keys.M
                    If My.Computer.Keyboard.CtrlKeyDown Then
                        'Debug.Print("Ctrl + M")
                        'CtrlCustDtls1.CtrlLabel3_Click(Nothing, New KeyEventArgs(Keys.Enter))
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
            End Select
        End If
        Return MyBase.ProcessKeyPreview(m)
    End Function

    Private Sub cmdSONew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSONew.Click
        Try
            If dsScan.Tables(0).Rows.Count > 0 Then
                If MsgBox(getValueByKey("SO077"), MsgBoxStyle.YesNo, "SO077 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
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
            If dsScan.Tables(0).Rows.Count > 0 Then
                If MsgBox(getValueByKey("SO077"), MsgBoxStyle.YesNo, "SO077 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then

                    IsFormClosing = True
                    Dim frm As New frmNSalesOrderUpdate
                    MDISpectrum.ShowChildForm(frm, True)
                End If
            Else
                Dim frm As New frmNSalesOrderUpdate
                MDISpectrum.ShowChildForm(frm, True)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub BtnSOPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'ShowMessage(getValueByKey("SO025"), "SO025")
        'If Not drDelvAdds Is Nothing Then
        '    PrintSalesOrders(drSiteInfo, drHomeAdds, dsMain.Tables("SalesOrderOtherCharges"))
        'Else

        'Rakesh-21.10.2013-8252: Display message as Please select order first!
        If (dsScan.Tables("ItemScanDetails") Is Nothing OrElse dsScan.Tables("ItemScanDetails").Rows.Count < 1) Then
            ShowMessage(getValueByKey("frmnquotationupdate.Warning"), getValueByKey("CLAE04"))
        Else
            Dim strSOStatus As String = ""
            If Not dsMain Is Nothing AndAlso dsMain.Tables.Contains("SalesOrderHDR") AndAlso Not dsMain.Tables("SalesOrderHDR").Columns("SOStatus") Is DBNull.Value Then
                strSOStatus = "Cancel"
            End If
            Dim SalesOrderNumber = dsMain.Tables("SalesOrderhDR").Rows(0)("SaleOrderNumber")
            Dim dsOtherCharges As New DataSet
            dsOtherCharges.Clear()
            Dim dt = objCharge.GetDtOtherCharge(clsAdmin.SiteCode, IIf(SalesOrderNumber = String.Empty, 0, SalesOrderNumber))
            dsOtherCharges.Tables.Add(dt.Copy())
            dsOtherCharges.Tables(0).TableName = "NewOtherCharges"
            Dim SalesPersonName = String.Empty
            SalesPersonName = IIf(CtrlSalesPerson.CtrlSalesPersons.SelectedIndex < 0, "", CtrlSalesPerson.CtrlSalesPersons.Text)
            Dim vSalesOrderExpectedDeliveryDate As DateTime
            vSalesOrderExpectedDeliveryDate = dsMain.Tables("SalesOrderHdr").Rows(0)("ActualDeliveryDate")


            Dim IsArticleWiseKot As Boolean = False
            Dim IsCounterCopy As Boolean = False

            Dim IsFinalReceipt As Boolean = False
            If clsDefaultConfiguration.IsFinalReceipt.Contains(clsAdmin.TerminalID) Then
                IsFinalReceipt = True
            Else
                IsFinalReceipt = False
            End If
            If clsDefaultConfiguration.PrintFormatNo = "8" Then
                'code added by irfan for cakeology on 4/10/2017
                Dim objt As New clsCashMemoPrint(SalesOrderNumber, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving)
                objt.PrintFormatNo = clsDefaultConfiguration.PrintFormatNo
                objt.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "SO", "", "", "", "", "", "", "", clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6)   'code added by irfan on 9/9/2017 visiblity of hsn and tax
                '------------------------------------------------------------------------------------------------
            Else
                Dim objclsSalesOrder As New SpectrumPrint.clsPrintSalesOrder(clsDefaultConfiguration.SOPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, vSalesNo, dtCustmInfo, dsScan.Tables("ItemScanDetails"), dsPayment.Tables("MSTRecieptType"), "", "", "", clsDefaultConfiguration.BillRoundOffAt, "", dtPrinterInfo, strSOStatus, dsOtherCharges, ShowFullName:=clsDefaultConfiguration.PrintItemFullName, SalesOrderDeliveryDate:=vSalesOrderExpectedDeliveryDate, strSalesPerson:=SalesPersonName)
            End If
        End If

        'ResetSalesOrder()
        'CtrlSalesInfo.CtrlTxtOrderNo.Text = String.Empty
        'AutoLogout(FrmTranCode, Me, lblLoggedIn)
        'End If

        'ShowMessage("Print Sales Order service currently not available", "Print Sales Order Informaion")
    End Sub

    Public Sub SwipePosition(ByRef ctrlcashsummary As Object)
        Dim lbl8loaction As Point = ctrlcashsummary.lbl8location
        Dim txt8location As Point = ctrlcashsummary.lbltxt8location
        ctrlcashsummary.lbl10location = New System.Drawing.Point(lbl8loaction.X, lbl8loaction.Y) '' credit sales label position
        ctrlcashsummary.lbltxt10location = New System.Drawing.Point(txt8location.X, txt8location.Y) '' credit sale text here 
    End Sub

    Private Function Themechange()
        rbnTabSO.Text = rbnTabSO.Text.ToUpper
        rbnTabSO.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)

        CtrlRbn1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black
        CtrlCashSummary.AlignChangeForCashSummary = "Sales Order Old Cancel"
        CtrlSalesPerson.AlignChange = "Sales Order Old"
        CtrlRbn1.DbtnPay.LargeImage = Global.Spectrum.My.Resources.Resources.payment_Normal
        CtrlRbn1.DbtnPayCash.LargeImage = Global.Spectrum.My.Resources.Resources.Cash_Normal
        CtrlRbn1.DbtnPayCard.LargeImage = Global.Spectrum.My.Resources.Resources.Card_Normal
        CtrlRbn1.DbtnpayCheque.LargeImage = Global.Spectrum.My.Resources.PayByCheque


        Me.RibbonGroup1.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        Me.RibbonGroup1.ForeColorInner = Color.FromArgb(37, 37, 37)

        CtrlRbn1.DgrpPayments.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        Me.RibbonGroup3.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        '  Me.rbnGrpPayments.Image = Global.Spectrum.My.Resources.payment_Normal
        Me.RibbonGroup3.ForeColorInner = Color.FromArgb(37, 37, 37)

        cmdSONew.LargeImage = Global.Spectrum.My.Resources.NewSO1
        CmdSOEdit.LargeImage = Global.Spectrum.My.Resources.EditSO1

        BtnSOCancel.LargeImage = Global.Spectrum.My.Resources.CancelSO1
        rbbtnSave.LargeImage = Global.Spectrum.My.Resources.SaveSO1
        cmdPrint.LargeImage = Global.Spectrum.My.Resources.PrintSO1


        CtrlRbn1.DbtnPayCash.Text = CtrlRbn1.DbtnPayCash.Text.ToUpper
        CtrlRbn1.DbtnPayCard.Text = CtrlRbn1.DbtnPayCard.Text.ToUpper
        CtrlRbn1.DbtnpayCheque.Text = CtrlRbn1.DbtnpayCheque.Text.ToUpper
        'cst.Text = rbnCST.Text.ToUpper
        ' rbBtnRoundOff.Text = rbBtnRoundOff.Text.ToUpper

        Me.RibbonGroup1.Text = Me.RibbonGroup1.Text.ToUpper

        rbnTabSO.Text = rbnTabSO.Text.ToUpper
        rbnTabSO.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)

        '  Me.rbnGrpCST.Text = Me.rbnGrpCST.Text.ToUpper

        
        CtrlRbn1.DgrpPayments.Text = CtrlRbn1.DgrpPayments.Text.ToUpper

        Me.RibbonGroup3.Text = Me.RibbonGroup3.Text.ToUpper

        RibbonGroup3.ForeColorInner = Color.FromArgb(54, 54, 54)
        CtrlRbn1.DgrpPayments.ForeColorInner = Color.FromArgb(54, 54, 54)
        RibbonGroup1.ForeColorInner = Color.FromArgb(54, 54, 54)

        RibbonGroup1.ForeColorOuter = Color.FromArgb(0, 107, 163)
        CtrlRbn1.DgrpPayments.ForeColorOuter = Color.FromArgb(0, 107, 163)
        RibbonGroup3.ForeColorOuter = Color.FromArgb(0, 107, 163)
      
        cmdSONew.Text = cmdSONew.Text.ToUpper
        CmdSOEdit.Text = CmdSOEdit.Text.ToUpper
        rbbtnSave.Text = rbbtnSave.Text.ToUpper
        BtnSOCancel.Text = BtnSOCancel.Text.ToUpper
        cmdPrint.Text = cmdPrint.Text.ToUpper

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

        CtrlBtnStockCheck.Image = My.Resources.StockCheck
        CtrlBtnStockCheck.ImageAlign = ContentAlignment.MiddleCenter
        CtrlBtnStockCheck.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlBtnStockCheck.TextAlign = ContentAlignment.MiddleCenter
        CtrlBtnStockCheck.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnStockCheck.BackColor = Color.Transparent
        CtrlBtnStockCheck.BackColor = Color.White
        CtrlBtnStockCheck.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CtrlBtnStockCheck.FlatStyle = FlatStyle.Flat


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
        CtrlCashSummary.Location = New Point(1131, 410)
        CtrlCashSummary.Size = New Size(224, 190)
        CtrlCashSummary.MinimumSize = New Size(222, 0)
        TabSalesOrder.Size = New Size(1127, 354)
        C1Sizer2.Size = New Size(1130, 72)

        TabPageInvoiceDetails.TabBackColorSelected = Color.FromArgb(212, 212, 212)
        TabPageItemDetails.TabBackColorSelected = Color.FromArgb(212, 212, 212)
        'TabPageInvoiceDetails.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        'TabPageItemDetails.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        TabPageInvoiceDetails.Text = TabPageInvoiceDetails.Text.ToUpper
        TabPageItemDetails.Text = TabPageItemDetails.Text.ToUpper

        CtrlCustDtls1.Size = New Size(785, 122)
        Me.C1Sizer3.Controls.Remove(Me.CtrlSalesPerson)
        Me.Controls.Add(CtrlSalesPerson)
        Me.Controls.SetChildIndex(Me.CtrlSalesPerson, 0)
        CtrlSalesPerson.Size = New Size(675, 22)
        CtrlSalesPerson.Location = New Point(421, 282)
        C1Sizer3.Hide()
        Me.TabPageItemDetails.Controls.Add(Me.grdSOItems)
        Me.grdSOItems.Size = New System.Drawing.Size(1118, 327)
        Me.grdSOItems.Location = New System.Drawing.Point(3, 3)
        'For i = 0 To grdSOInvoice.Cols.Count - 1
        '    grdSOInvoice.Cols(i).Caption = grdSOInvoice.Cols(i).Caption.ToUpper
        'Next
        'For i = 0 To grdSOItems.Cols.Count - 1
        '    grdSOItems.Cols(i).Caption = grdSOItems.Cols(i).Caption.ToUpper
        'Next
    End Function
End Class