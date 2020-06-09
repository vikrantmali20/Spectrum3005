Imports SpectrumBL
Imports SpectrumPrint
Imports System.Data.SqlClient
Imports C1.Win.C1FlexGrid

Public Class frmNOutboundDelivery
    Protected controlList As New ArrayList
    Dim BarcodeList As List(Of SpectrumCommon.BtachbarcodeInfo)
    Dim CVoucherNo As String
    Dim CVVoucherDay As Int32 = clsAdmin.CreditValidDays
    Private dsOrder As New DataSet
    Private dsMain As New DataSet
    Private drMain As DataRow
    Private dtCustInfo As New DataTable
    Private dtOBItemInfo As New DataTable
    Private dtOtherCharges As New DataTable

    Private clsPrint As New clsReprintBill
    Private clsOB As New clsOutboundDelivery
    Private clsSO As New clsSalesOrder
    Private objCustm As New clsCLPCustomer
    Private clsCommon As New clsCommon
    Private PickupAmount As Decimal = 0.0
    Private PickupAmountSum As Decimal = 0.0
    Private DocumentType As String = String.Empty
    Private ODNextNumber As String = String.Empty
    Private IsComboLoaded As Boolean = False

    Private vSiteCode As String = String.Empty
    Private vFinYear As String = String.Empty
    Private vCurrentDate As DateTime = Nothing
    Private vUserName As String = String.Empty
    Private BLOutBoundCode As String = "OB"
    Private vSupplierCode As String = String.Empty

    Private drBLHdr As DataRow
    Private drBLDtl As DataRow
    Private objComn As New clsCommon
    Dim vSalesInvcNo As String = ""
    Dim IsNewRow As Boolean = False
    Private _dDueDate As Date
    Private _strRemarks As String
    Private IsGiftVoucher As Boolean
    Private objPayment As New clsAcceptPayment
    'Protected dsRecieptType As New DataSet()
    'Public Property AcceptEditBillDataSet() As DataSet
    '    Get
    '        Return dsRecieptType
    '    End Get
    '    Set(ByVal value As DataSet)
    '        dsRecieptType = value
    '    End Set
    'End Property
    Private _strGiftReceiptMessage As String
    Public Property GiftReceiptMessage() As String
        Get
            Return _strGiftReceiptMessage
        End Get
        Set(ByVal value As String)
            _strGiftReceiptMessage = value
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

    Dim DtSoBulkComboHdr As New DataTable
    Dim DtSoBulkComboDtl As New DataTable
    Dim vOtherCharges As Double = 0

    Private Sub frmNOutboundDelivery_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim dtDocType As New DataTable
            dtDocType.Columns.Add("DocValue", Type.GetType("System.String"))
            dtDocType.Rows.Add("Sales Order")
            dtDocType.Rows.Add("Birth List")

            clsSO.GetSODefaultConfig(vSiteCode)
            'Dim objdefault As New clsDefaultConfiguration("SalesOrder")
            'objdefault.GetDefaultSettings()
            DocumentType = clsSO.SOCreation

            cboDocType.DataSource = dtDocType
            cboDocType.DisplayMember = "DocValue"
            cboDocType.ValueMember = "DocValue"
            cboDocType.ColumnWidth = 150

            txtOrderNo.Text = String.Empty  '"SOT1120285"

            CtrlCustDtls1.lblCustType.Visible = True
            CtrlCustDtls1.cboAddrType.Visible = False
            CtrlCustDtls1.lblCustTypeValue.Visible = True
            CtrlCustDtls1.lblCustTypeValue.Width = CtrlCustDtls1.lblCustNameValue.Width
            ClearCustomerInfo()
            'dsPaymentMain = clsSO.GetSOInvoiceTableStruc(clsAdmin.SiteCode, 0)
            vSiteCode = clsAdmin.SiteCode
            vUserName = clsAdmin.UserName
            vFinYear = clsAdmin.Financialyear
            vCurrentDate = objComn.GetCurrentDate
            'vSupplierCode = clsDefaultConfiguration.SupplierCode
            cboDocType.Focus()
            CtrlTab1.SelectedIndex = 0
            SetCulture(Me, Me.Name)
            BarcodeList = New List(Of SpectrumCommon.BtachbarcodeInfo)
            btnPay.Enabled = False
            grdOrderInfo.AutoSizeCols()
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog

            btnPay.Visible = False
            Call SetTabSequence()

            '---- Load Sales Order Flags (:-Code Added By Mahesh Nagar)
            Dim objdefaultSO As New clsDefaultConfiguration("SalesOrder")
            objdefaultSO.GetDefaultSettings()
            '---- Load Cash Memo flags (:- Code Added By Mahesh Nagar )
            Dim objDefault As New clsDefaultConfiguration("CMS")
            objDefault.GetDefaultSettings()
            'code added by Mahesh for add bulk combo 
            'Call objSO.GetSOBulkComboTablestructure(DtSoBulkComboHdr, DtSoBulkComboDtl)
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    Private Sub BtnCancelOD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancelOD.Click
        Me.Close()
    End Sub
    Private Sub txtDocNumber_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtOrderNo.PreviewKeyDown
        If e.KeyCode = Keys.Enter Then

            If cboDocType.SelectedValue = String.Empty Then
                ShowMessage(getValueByKey("OBD006"), "OBD006 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtOrderNo.Text.Trim) = False Then
                IsComboLoaded = False
                ResetLabelFields()
                If cboDocType.SelectedValue = "Sales Order" Then
                    SearchOrderInfo(txtOrderNo.Text.Trim)
                ElseIf cboDocType.SelectedValue = "Birth List" Then
                    SearchBirthListOrderInfo(txtOrderNo.Text.Trim)
                End If

            Else
                ShowMessage(getValueByKey("OBD007"), "OBD007 - " & getValueByKey("CLAE04"))
            End If
        End If
    End Sub

    Private _IsSOAssignedByOtherSiteSelected As Boolean
    Private Sub BtnSearchOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchOrder.Click
        If cboDocType.SelectedValue = String.Empty Then
            ShowMessage(getValueByKey("OBD006"), "OBD006 - " & getValueByKey("CLAE04"))
            Exit Sub
        End If
        If cboDocType.SelectedValue IsNot String.Empty Then
            Dim _IsDeliveryFromOtherSite As Boolean
            IsComboLoaded = False

            If cboDocType.SelectedValue = "Sales Order" Then
                Dim frmSOSearch As New frmSalesOrderSearch
                frmSOSearch.ShowDialog()
                Dim vSalesNo As String = String.Empty
                If Not frmSOSearch.search Is Nothing AndAlso frmSOSearch.search.Length > 0 Then
                    vSalesNo = frmSOSearch.search(0)
                    Me._IsSOAssignedByOtherSiteSelected = frmSOSearch._IsSOAssignedByOtherSiteSelected
                    _IsDeliveryFromOtherSite = frmSOSearch._IsSOAssignedByOtherSiteSelected
                End If
                'Dim vSalesNo As String = SearchSalesOrderCustomer()
                If String.IsNullOrEmpty(vSalesNo) = False Then
                    ResetLabelFields()
                    txtOrderNo.Text = vSalesNo
                    SearchOrderInfo(txtOrderNo.Text.Trim)
                    Dim Dstemp = objSO.GetSOBulkComboTableStruct(clsAdmin.SiteCode, IIf(vSalesNo = String.Empty, 0, vSalesNo))
                    DtSoBulkComboHdr = Dstemp.Tables("SoBulkComboHdr")
                    DtSoBulkComboDtl = Dstemp.Tables("SoBulkComboDtl")
                    lblAmountToPay.Text = clsOB.GetAmountToPay(clsAdmin.SiteCode, clsAdmin.Financialyear, vSalesNo, _IsDeliveryFromOtherSite)
                End If

            ElseIf cboDocType.SelectedValue = "Birth List" Then
                Dim objFrmBirthListSearch As New frmNBirthListSearch(True)

                objFrmBirthListSearch.ShowDialog()
                Dim drBirthList As DataRow = objFrmBirthListSearch.SearchBirthListInformation

                If drBirthList IsNot Nothing Then
                    ResetLabelFields()
                    Dim strfinyear As String = String.Empty
                    txtOrderNo.Text = drBirthList("BirthListId")
                    strfinyear = drBirthList("FinYear")
                    SearchBirthListOrderInfo(txtOrderNo.Text.Trim, strfinyear)
                End If
            End If
            BarcodeList = New List(Of SpectrumCommon.BtachbarcodeInfo)
        Else
            ShowMessage(getValueByKey("OBD008"), "OBD008 - " & getValueByKey("CLAE04"))
        End If
    End Sub
    Private Function SearchOrderInfo(ByVal DocumentNumber As String) As Boolean
        Try
            'If _IsSOAssignedByOtherSiteSelected Then
            dsOrder = clsOB.GetAllOrderInfo(vSiteCode, vFinYear, DocumentNumber, PrintTransType.Replace(Space(1), Nothing), _IsSOAssignedByOtherSiteSelected)
            'Else            
            'End If
            If dsOrder.Tables("SalesInvoice").Rows.Count > 0 Then
                'If CInt(dsOrder.Tables("OrderDtls").Rows(0).Item("CustomerNo").ToString) > 0 Then
                If Not String.IsNullOrEmpty(dsOrder.Tables("SalesInvoice").Rows(0).Item("CustomerNo").ToString) Then
                    dtCustInfo = objCustm.GetCustomerInformation(dsOrder.Tables("SalesInvoice").Rows(0).Item("CustomerType"), vSiteCode, clsAdmin.CLPProgram, dsOrder.Tables("SalesInvoice").Rows(0).Item("CustomerNo"))
                    If dtCustInfo IsNot Nothing AndAlso dtCustInfo.Rows.Count > 0 Then
                        SetCustomerInfo(dtCustInfo.Rows(0))
                    End If
                Else
                    ClearCustomerInfo()
                End If

                If (dsOrder.Tables("SalesInvoice").Compute("Sum(AmountTendered)", String.Empty) Is DBNull.Value) Then
                    lblReceivedAmount.Text = strZero
                Else
                    lblReceivedAmount.Text = FormatNumber(dsOrder.Tables("SalesInvoice").Compute("Sum(AmountTendered)", String.Empty), 2)
                End If

                '---Code Add By Mahesh for adding Other charges 
                Dim dtOtherCharges = dsOrder.Tables("OtherCharges")
                If Not (dtOtherCharges Is Nothing) AndAlso dtOtherCharges.Rows.Count > 0 Then
                    Dim vChargeAmountOld As String = IIf((dtOtherCharges.Compute("Sum(ChargeAmount)", "") Is DBNull.Value), 0, dtOtherCharges.Compute("Sum(ChargeAmount)", ""))
                    Dim vTaxAmountOld As String = IIf((dtOtherCharges.Compute("Sum(TaxAmt)", "") Is DBNull.Value), 0, dtOtherCharges.Compute("Sum(TaxAmt)", ""))

                    vOtherCharges = CDbl(CDbl(vChargeAmountOld) + CDbl(vTaxAmountOld))
                End If

                If (dsOrder.Tables("OrderDtls").Compute("Sum(NetAmount)", String.Empty) Is DBNull.Value) Then
                    lblNetAmount.Text = strZero
                Else
                    ' lblNetAmount.Text = FormatNumber(dsOrder.Tables("OrderDtls").Compute("Sum(NetAmount)", String.Empty), 2)
                    lblNetAmount.Text = FormatNumber(dsOrder.Tables("OrderDtls").Compute("Sum(NetAmount)", String.Empty) + vOtherCharges, 2)
                End If

                Dim DeliveredAmt As Decimal = 0.0
                For Each dr As DataRow In dsOrder.Tables("OrderDtls").Select("DeliveredQty>0")
                    '----- Code  changed By Mahesh ...Rate must be netAmount/OrderQty ...
                    'DeliveredAmt += (dr("Rate") * dr("DeliveredQty")) - dr("LineDiscount")
                    DeliveredAmt += ((dr("NetAmount") / dr("OrderQty")) * dr("DeliveredQty")) '- dr("LineDiscount")
                Next
                lblDeliveredAmt.Text = FormatNumber(DeliveredAmt, 2)

                RefreshInvoiceGrid(dsOrder.Tables("OrderDtls"))
            Else
                ResetLabelFields()
                ClearCustomerInfo()
                RefreshInvoiceGrid(dsOrder.Tables("OrderDtls"))

                ShowMessage(getValueByKey("OBD009"), "OBD009 - " & getValueByKey("CLAE04"))
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try

    End Function
    Private Function SearchBirthListOrderInfo(ByVal DocumentNumber As String, Optional ByVal strfinyear As String = "") As Boolean

        If strfinyear = "" Then
            strfinyear = vFinYear
        End If
        dsOrder = clsOB.GetAllOrderInfo(vSiteCode, strfinyear, DocumentNumber, PrintTransType.Replace(Space(1), Nothing))

        If dsOrder.Tables("BirthList").Rows.Count > 0 Then
            If dsOrder.Tables("BirthList").Rows(0).Item("CustomerId").ToString() <> String.Empty Then
                dtCustInfo = objCustm.GetCustomerInformation(dsOrder.Tables("BirthList").Rows(0).Item("CustomerType"), vSiteCode, clsAdmin.CLPProgram, dsOrder.Tables("BirthList").Rows(0).Item("CustomerId"))
                If dtCustInfo IsNot Nothing AndAlso dtCustInfo.Rows.Count > 0 Then
                    SetCustomerInfo(dtCustInfo.Rows(0))
                End If
            Else
                ClearCustomerInfo()
            End If

            If (dsOrder.Tables("SalesInvoice").Rows.Count > 0) Then
                lblReceivedAmount.Text = FormatNumber(dsOrder.Tables("SalesInvoice").Compute("Sum(AmountTendered)", String.Empty), 2)
            Else
                lblReceivedAmount.Text = strZero
            End If

            If (dsOrder.Tables("OrderDtls").Rows.Count > 0) Then
                lblNetAmount.Text = FormatNumber(dsOrder.Tables("OrderDtls").Compute("Sum(NetAmount)", String.Empty), 2)
            Else
                lblNetAmount.Text = strZero
            End If

            Dim DeliveredAmt As Decimal = 0.0
            For Each dr As DataRow In dsOrder.Tables("OrderDtls").Select("DeliveredQty>0")
                DeliveredAmt += (dr("Rate") * dr("DeliveredQty"))
            Next
            lblDeliveredAmt.Text = FormatNumber(DeliveredAmt, 2)
        Else
            ClearCustomerInfo()
        End If

        RefreshInvoiceGrid(dsOrder.Tables("OrderDtls"))
        IsComboLoaded = True

    End Function
    Private Sub RefreshInvoiceGrid(ByVal dtGrid As DataTable)
        Try
            grdOrderInfo.DataSource = dtGrid
            SetGridColumnCaption()
            fnGridColAutoSize(grdOrderInfo)
            For index = 1 To grdOrderInfo.Rows.Count - 1
                AddButtonControlInGrid(index)
            Next
            grdOrderInfo.AutoSizeCols()
            BarcodeList = New List(Of SpectrumCommon.BtachbarcodeInfo)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub
    Private Sub SetGridColumnCaption()
        Try
            'If cboDocType.SelectedValue = "Sales Order" AndAlso _IsSOAssignedByOtherSiteSelected Then
            'grdOrderInfo.Cols("AmountToCollect").Visible = True
            'grdOrderInfo.Cols("NetAmount").Visible = False
            'grdOrderInfo.Cols("Rate").Visible = False
            'LbNetAmount.Visible = False
            'lblNetAmount.Visible = False
            'LbReceivedAmount.Visible = False
            'lblReceivedAmount.Visible = False
            'LbPickupAmt.Visible = False
            'lblPickupAmt.Visible = False
            'Else
            'grdOrderInfo.Cols("AmountToCollect").Visible = False
            grdOrderInfo.Cols("NetAmount").Visible = True
            grdOrderInfo.Cols("Rate").Visible = True
            LbNetAmount.Visible = True
            lblNetAmount.Visible = True
            LbReceivedAmount.Visible = True
            lblReceivedAmount.Visible = True
            LbPickupAmt.Visible = True
            'lblPickupAmt.Visible = True
            'End If

            If (clsDefaultConfiguration.BarcodeDisplayAllowed) Then
                grdOrderInfo.Cols("EAN").Caption = getValueByKey("frmnoutbounddelivery.grdorderinfo.ean")
                grdOrderInfo.Cols("EAN").Width = 90
                grdOrderInfo.Cols("EAN").AllowEditing = False
                grdOrderInfo.Cols("EAN").Visible = True
            Else
                grdOrderInfo.Cols("EAN").Visible = False
            End If

            grdOrderInfo.Cols("ArticleCode").Caption = getValueByKey("frmnitemsearch.dgitemsearch.item code")
            grdOrderInfo.Cols("ArticleCode").Width = 90

            grdOrderInfo.Cols("ArticleName").Caption = getValueByKey("frmnitemsearch.dgitemsearch.description")
            grdOrderInfo.Cols("ArticleName").Width = 175

            grdOrderInfo.Cols("OrderQty").Caption = getValueByKey("frmnitemsearch.dgitemsearch.order qty")
            grdOrderInfo.Cols("OrderQty").Width = 65
            ' grdOrderInfo.Cols("OrderQty").Format = "0.0"

            grdOrderInfo.Cols("DeliveredQty").Caption = getValueByKey("frmnoutbounddelivery.grdorderinfo.deliveredqty")
            grdOrderInfo.Cols("DeliveredQty").Width = 60
            '  grdOrderInfo.Cols("DeliveredQty").Format = "0.0"

            grdOrderInfo.Cols("PickupQty").Caption = getValueByKey("frmnoutbounddelivery.grdorderinfo.pickupqty")
            grdOrderInfo.Cols("PickupQty").Width = 70
            '  grdOrderInfo.Cols("PickupQty").Format = "0.0"
            grdOrderInfo.Cols("PickupQty").AllowEditing = Not clsDefaultConfiguration.IsBatchManagementReq

            grdOrderInfo.Cols("BookedQty").Caption = getValueByKey("frmnoutbounddelivery.grdorderinfo.bookedqty")
            grdOrderInfo.Cols("BookedQty").Width = 70
            'grdOrderInfo.Cols("BookedQty").Format = "0.0"

            grdOrderInfo.Cols("Rate").Caption = getValueByKey("frmnoutbounddelivery.grdorderinfo.rate")
            grdOrderInfo.Cols("Rate").Width = 60
            grdOrderInfo.Cols("Rate").Format = "0.00"

            grdOrderInfo.Cols("NetAmount").Caption = getValueByKey("frmnoutbounddelivery.grdorderinfo.netamount")
            grdOrderInfo.Cols("NetAmount").Width = 80
            grdOrderInfo.Cols("NetAmount").Format = "0.00"


            If (clsDefaultConfiguration.BarcodeDisplayAllowed) Then
                grdOrderInfo.Cols("EAN").Caption = getValueByKey("frmnoutbounddelivery.grdorderinfo.ean")
                grdOrderInfo.Cols("EAN").Width = 90
                grdOrderInfo.Cols("EAN").AllowEditing = False
                grdOrderInfo.Cols("EAN").Visible = True
            Else
                grdOrderInfo.Cols("EAN").Visible = False
            End If

            grdOrderInfo.Cols("ArticleCode").Caption = getValueByKey("frmnitemsearch.dgitemsearch.item code")
            grdOrderInfo.Cols("ArticleCode").Width = 90

            grdOrderInfo.Cols("ArticleName").Caption = getValueByKey("frmnitemsearch.dgitemsearch.description")
            grdOrderInfo.Cols("ArticleName").Width = 175

            grdOrderInfo.Cols("OrderQty").Caption = getValueByKey("frmnitemsearch.dgitemsearch.order qty")
            grdOrderInfo.Cols("OrderQty").Width = 65

            grdOrderInfo.Cols("DeliveredQty").Caption = getValueByKey("frmnoutbounddelivery.grdorderinfo.deliveredqty")
            grdOrderInfo.Cols("DeliveredQty").Width = 60


            grdOrderInfo.Cols("PickupQty").Caption = getValueByKey("frmnoutbounddelivery.grdorderinfo.pickupqty")
            grdOrderInfo.Cols("PickupQty").Width = 70
            grdOrderInfo.Cols("PickupQty").AllowEditing = Not clsDefaultConfiguration.IsBatchManagementReq

            grdOrderInfo.Cols("BookedQty").Caption = getValueByKey("frmnoutbounddelivery.grdorderinfo.bookedqty")
            grdOrderInfo.Cols("BookedQty").Width = 70


            If clsDefaultConfiguration.AllowDecimalQty Then
                grdOrderInfo.Cols("OrderQty").DataType = Type.GetType("System.Decimal")
                grdOrderInfo.Cols("PickupQty").DataType = Type.GetType("System.Decimal")
                grdOrderInfo.Cols("DeliveredQty").DataType = Type.GetType("System.Decimal")
                grdOrderInfo.Cols("BookedQty").DataType = Type.GetType("System.Decimal")

                If clsDefaultConfiguration.WeightScaleEnabled Then
                    grdOrderInfo.Cols("OrderQty").Format = "0.000"
                    grdOrderInfo.Cols("PickupQty").Format = "0.000"
                    grdOrderInfo.Cols("DeliveredQty").Format = "0.000"
                    grdOrderInfo.Cols("BookedQty").Format = "0.000"
                Else
                    grdOrderInfo.Cols("OrderQty").Format = "0.00"
                    grdOrderInfo.Cols("PickupQty").Format = "0.00"
                    grdOrderInfo.Cols("DeliveredQty").Format = "0.00"
                    grdOrderInfo.Cols("BookedQty").Format = "0.00"
                End If
            Else

                grdOrderInfo.Cols("OrderQty").DataType = Type.GetType("System.Int32")
                grdOrderInfo.Cols("PickupQty").DataType = Type.GetType("System.Int32")
                grdOrderInfo.Cols("DeliveredQty").DataType = Type.GetType("System.Int32")
                grdOrderInfo.Cols("BookedQty").DataType = Type.GetType("System.Int32")
                grdOrderInfo.Cols("OrderQty").Format = "0"
                grdOrderInfo.Cols("PickupQty").Format = "0"
                grdOrderInfo.Cols("DeliveredQty").Format = "0"
                grdOrderInfo.Cols("BookedQty").Format = "0"
            End If


            lblBatchBarcode.Visible = clsDefaultConfiguration.IsBatchManagementReq
            txtBatchBarcode.Visible = clsDefaultConfiguration.IsBatchManagementReq
            grdOrderInfo.Cols("EditBarcode").Visible = clsDefaultConfiguration.IsBatchManagementReq

        Catch ex As Exception
        End Try
    End Sub
    Private Sub SetCustomerInfo(ByVal drCustmInfo As DataRow)
        Try
            CtrlCustDtls1.lblCustTypeValue.Text = IIf(drCustmInfo("CustomerType") Is DBNull.Value, String.Empty, drCustmInfo("CustomerType"))
            If CtrlCustDtls1.lblCustTypeValue.Text = "SO" Then
                CtrlCustDtls1.lblCustTypeValue.Text = "SO Customer"
            ElseIf CtrlCustDtls1.lblCustTypeValue.Text = "CLP" Then
                CtrlCustDtls1.lblCustTypeValue.Text = "CLP Customer"
            End If
            CtrlCustDtls1.lblCustNoValue.Text = IIf(drCustmInfo("CustomerNo") Is DBNull.Value, String.Empty, drCustmInfo("CustomerNo"))
            CtrlCustDtls1.lblCustNameValue.Text = IIf(drCustmInfo("CustomerName") Is DBNull.Value, String.Empty, drCustmInfo("CustomerName"))
            CtrlCustDtls1.lblAddressValue.Text = IIf(drCustmInfo("Address") Is DBNull.Value, String.Empty, drCustmInfo("Address"))
            CtrlCustDtls1.lblEmailIdValue.Text = IIf(drCustmInfo("EmailID") Is DBNull.Value, String.Empty, drCustmInfo("EmailID"))
            CtrlCustDtls1.lblTelNoValue.Text = IIf(drCustmInfo("ResPhone") Is DBNull.Value, String.Empty, drCustmInfo("ResPhone"))

            ' get more info
            CtrlCustDtls1.dtCustmInfo = drCustmInfo.Table
            ' get more info

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub
    Private Sub ClearCustomerInfo()
        Try
            CtrlCustDtls1.lblCustTypeValue.Text = String.Empty
            CtrlCustDtls1.lblCustNoValue.Text = String.Empty
            CtrlCustDtls1.lblCustNameValue.Text = String.Empty
            CtrlCustDtls1.lblAddressValue.Text = String.Empty
            CtrlCustDtls1.lblEmailIdValue.Text = String.Empty
            CtrlCustDtls1.lblTelNoValue.Text = String.Empty

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub
    Private Sub cboDocType_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDocType.SelectedValueChanged
        PrintTransType = cboDocType.SelectedValue

        If cboDocType.SelectedValue = "Sales Order" Then
            DocumentType = clsSO.SOCreation
            'grdOrderInfo.Cols("Rate").Visible = True
            'grdOrderInfo.Cols("NetAmount").Visible = True
            grdOrderInfo.Cols("BookedQty").Visible = False

            'LbDeliveredAmt.Visible = True
            'lblDeliveredAmt.Visible = True
            'LbPickupAmt.Visible = True
            'lblPickupAmt.Visible = True
            'LbReceivedAmount.Visible = True
            'lblReceivedAmount.Visible = True
            'LbNetAmount.Visible = True
            'lblNetAmount.Visible = True
            btnPay.Enabled = True

            Dim objdefault As New clsDefaultConfiguration("SalesOrder")
            objdefault.GetDefaultSettings()

        Else
            DocumentType = "BLS"
            'grdOrderInfo.Cols("Rate").Visible = True
            'grdOrderInfo.Cols("NetAmount").Visible = True
            grdOrderInfo.Cols("BookedQty").Visible = True

            'LbDeliveredAmt.Visible = True
            'lblDeliveredAmt.Visible = True
            'LbPickupAmt.Visible = True
            'lblPickupAmt.Visible = True
            'LbReceivedAmount.Visible = True
            'lblReceivedAmount.Visible = True
            'LbNetAmount.Visible = True
            'lblNetAmount.Visible = True

            btnPay.Enabled = False

            Dim objdefault As New clsDefaultConfiguration("BLS")
            objdefault.GetDefaultSettings()
        End If

        txtOrderNo.Text = String.Empty
        If dsOrder.Tables.Count > 0 Then
            dsOrder.Tables("OrderDtls").Rows.Clear()
            dsOrder.Tables("SalesInvoice").Rows.Clear()
            RefreshInvoiceGrid(dsOrder.Tables("OrderDtls"))
            ClearCustomerInfo()
            ResetLabelFields()
        End If
    End Sub
    Private Sub grdOrderInfo_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdOrderInfo.AfterEdit
        Try
            Dim ArticleCode As String = grdOrderInfo.Item(grdOrderInfo.Row, "ArticleCode").ToString

            Dim OrderQty As Decimal = CDbl(grdOrderInfo.Item(grdOrderInfo.Row, "OrderQty").ToString)
            Dim DelQty As Decimal
            If (grdOrderInfo.Item(grdOrderInfo.Row, "DeliveredQty")) Is DBNull.Value Then
                DelQty = Decimal.Zero
            Else
                DelQty = CDbl(grdOrderInfo.Item(grdOrderInfo.Row, "DeliveredQty").ToString)
            End If

            Dim PickupQty As Decimal = CDbl(grdOrderInfo.Item(grdOrderInfo.Row, "PickupQty").ToString)
            Dim Rate As Decimal = CDbl(grdOrderInfo.Item(grdOrderInfo.Row, "Rate").ToString)

            '---- Set Complete PickUp Variable
            Dim IsCompletePickUpDone As Boolean = False
            Dim Pq, dq, oq As Double
            For rowIndex = 1 To grdOrderInfo.Rows.Count - 1
                Pq += CDbl(grdOrderInfo.Item(rowIndex, "PickupQty"))
                If Not IsDBNull(grdOrderInfo.Item(grdOrderInfo.Row, "DeliveredQty")) Then
                    dq += CDbl(grdOrderInfo.Item(rowIndex, "DeliveredQty"))
                End If
                oq += CDbl(grdOrderInfo.Item(rowIndex, "OrderQty"))
            Next
            If (oq = dq + Pq) Then IsCompletePickUpDone = True

            ''---- Code Added By Mahesh START  for Rate must be net amount / Order Qty
            Dim ArticleRate As Decimal = Math.Round(CDbl(grdOrderInfo.Item(grdOrderInfo.Row, "NetAmount").ToString) / CDbl(grdOrderInfo.Item(grdOrderInfo.Row, "OrderQty").ToString), 3)
            Rate = ArticleRate
            ''---- Code Added By Mahesh END
            If cboDocType.SelectedValue = "Sales Order" Then
                Dim Disc As Decimal = CDbl(dsOrder.Tables("OrderDtls").Rows(grdOrderInfo.Row - 1)("LineDiscount").ToString)
                Dim orderqtys As Decimal = CDbl(dsOrder.Tables("OrderDtls").Rows(grdOrderInfo.Row - 1)("Orderqty").ToString)

                If OrderQty < DelQty + PickupQty Then
                    ShowMessage(getValueByKey("OBD001"), getValueByKey("CLAE05"))
                    grdOrderInfo.Item(grdOrderInfo.Row, "PickupQty") = 0
                    grdOrderInfo.Cols("PickupAmt").Visible = True
                    grdOrderInfo.Item(grdOrderInfo.Row, "PickupAmt") = 0
                    grdOrderInfo.Cols("PickupAmt").Visible = False
                    lblPickupAmt.Text = FormatNumber(dsOrder.Tables("OrderDtls").Compute("Sum(PickupAmt)", String.Empty), 2)
                    Exit Sub
                End If

                'Rakesh-01.10.2013:7835-->Check article stock balance quantity
                If (Not clsDefaultConfiguration.NegativeInventoryAllowed) Then
                    Dim objCommon As New clsCommon
                    Dim articleCode1 = grdOrderInfo.Item(grdOrderInfo.Row, "ArticleCode")
                    Dim articleEAN = grdOrderInfo.Item(grdOrderInfo.Row, "EAN")
                    Dim iPickUpQty = grdOrderInfo.Item(grdOrderInfo.Row, "PickupQty")

                    Dim StockQty As Double = objCommon.GetStocks(clsAdmin.SiteCode, articleEAN, articleCode1, True)

                    If (StockQty < iPickUpQty) Then
                        ShowMessage(String.Format(getValueByKey("SB015"), StockQty), "SB015 - " & getValueByKey("CLAE04"))
                        grdOrderInfo.Item(grdOrderInfo.Row, "PickupQty") = 0
                    End If
                End If

                grdOrderInfo.Cols("PickupAmt").Visible = True
                '-- 07032015 Mahesh - removing LineDiscount as Net Amount already have effect of Discount so no need to subtract discount again
                grdOrderInfo.Item(grdOrderInfo.Row, "PickupAmt") = (Rate * PickupQty) '- ((Disc / orderqtys) * PickupQty)
                grdOrderInfo.Cols("PickupAmt").Visible = False

                lblPickupAmt.Text = FormatNumber(dsOrder.Tables("OrderDtls").Compute("Sum(PickupAmt)", String.Empty), 2)

                Dim PickupAmount1 As Decimal = CDbl(dsOrder.Tables("OrderDtls").Compute("Sum(PickupAmt)", String.Empty))
                Dim deliveredAmt As Decimal
                For Each dr In dsOrder.Tables("OrderDtls").Select("DeliveredQty>0")
                    '----- Code  changed By Mahesh ...Rate must be netAmount/OrderQty ...
                    'DeliveredAmt += (dr("Rate") * dr("DeliveredQty")) - dr("LineDiscount")

                    '-- 07032015 Mahesh - removing LineDiscount as Net Amount already have effect of Discount so no need to subtract discount again
                    'deliveredAmt += Rate * dr("DeliveredQty")
                    deliveredAmt += ((dr("NetAmount") / dr("OrderQty")) * dr("DeliveredQty"))
                Next

                Dim netAmount = CDbl(dsOrder.Tables("OrderDtls").Compute("Sum(NetAmount)", String.Empty))
                Dim amountToCollect As Decimal = PickupAmount1 + deliveredAmt + ((netAmount - PickupAmount1 - deliveredAmt) * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100))
                Dim totalPickupAmount As Decimal = amountToCollect - CDbl(lblReceivedAmount.Text.Trim)

                If (totalPickupAmount > 0) Then
                    ' lblAmountToPay.Text = FormatNumber(totalPickupAmount, 2)
                    'lblAmountToPay.Text = FormatNumber(totalPickupAmount, 2) + IIf(IsCompletePickUpDone, vOtherCharges, 0)
                    lblAmountToPay.Text = MyRound(FormatNumber(totalPickupAmount, 2) + IIf(IsCompletePickUpDone, vOtherCharges, 0), clsDefaultConfiguration.BillRoundOffAt)
                Else
                    lblAmountToPay.Text = strZero + IIf(IsCompletePickUpDone, vOtherCharges, 0)
                End If

            Else
                Dim BookedQty As Decimal = CDbl(grdOrderInfo.Item(grdOrderInfo.Row, "BookedQty").ToString)

                'If Not (BookedQty >= DelQty + PickupQty) Then
                '    ShowMessage(getValueByKey("OBD002"), "OBD002 - " & getValueByKey("CLAE04"))
                '    grdOrderInfo.Item(grdOrderInfo.Row, "PickupQty") = 0
                'End If
                If OrderQty < DelQty + PickupQty Then
                    ShowMessage(getValueByKey("OBD001"), getValueByKey("CLAE05"))
                    grdOrderInfo.Item(grdOrderInfo.Row, "PickupQty") = 0
                End If

                'Rakesh-01.10.2013:7835-->Check article stock balance quantity
                If (Not clsDefaultConfiguration.NegativeInventoryAllowed) Then
                    Dim objCommon As New clsCommon
                    Dim articleCode1 = grdOrderInfo.Item(grdOrderInfo.Row, "ArticleCode")
                    Dim articleEAN = grdOrderInfo.Item(grdOrderInfo.Row, "EAN")
                    Dim iPickUpQty = grdOrderInfo.Item(grdOrderInfo.Row, "PickupQty")

                    Dim StockQty As Double = objCommon.GetStocks(clsAdmin.SiteCode, articleEAN, articleCode1, True)

                    If (StockQty < iPickUpQty) Then
                        ShowMessage(String.Format(getValueByKey("SB015"), StockQty), "SB015 - " & getValueByKey("CLAE04"))
                        grdOrderInfo.Item(grdOrderInfo.Row, "PickupQty") = 0
                    End If
                End If

                If (BookedQty < DelQty + PickupQty) Then
                    grdOrderInfo.Item(grdOrderInfo.Row, "PurchageQty") = (DelQty + PickupQty) - BookedQty
                    grdOrderInfo.Item(grdOrderInfo.Row, "PurchageAmount") = ((DelQty + PickupQty) - BookedQty) * Rate
                Else
                    grdOrderInfo.Item(grdOrderInfo.Row, "PurchageQty") = 0
                    grdOrderInfo.Item(grdOrderInfo.Row, "PurchageAmount") = 0
                End If

                Dim totalPickupAmount As Double = FormatNumber(dsOrder.Tables("OrderDtls").Compute("Sum(PurchageAmount)", String.Empty), 2)
                If (totalPickupAmount > 0) Then
                    'lblAmountToPay.Text = totalPickupAmount
                    lblAmountToPay.Text = totalPickupAmount + IIf(IsCompletePickUpDone, vOtherCharges, 0)
                Else
                    lblAmountToPay.Text = strZero
                End If

                Dim PickupAmount As Double
                For rowIndex = 1 To grdOrderInfo.Rows.Count - 1
                    PickupAmount += CDbl(grdOrderInfo.Item(rowIndex, "PickupQty").ToString) * CDbl(grdOrderInfo.Item(rowIndex, "Rate").ToString)
                Next
                lblPickupAmt.Text = FormatNumber(PickupAmount, 2)

            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub BtnOKOD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOKOD.Click
        Try
            Dim pickupQty As Decimal = Decimal.Zero

            If (dsOrder.Tables.Count > 0) Then
                pickupQty = CDbl(dsOrder.Tables("OrderDtls").Compute("Sum(PickupQty)", String.Empty))

                If pickupQty > 0 Then
                    If (Convert.ToDecimal(lblAmountToPay.Text) > 0) Then
                        btnPay_Click(sender, e)
                    Else
                        If cboDocType.SelectedValue = "Sales Order" Then
                            CreateOutboundForSalesOrder()
                        Else
                            CreateOutboundForBirthList()
                        End If

                        btnPay.Enabled = False
                        AutoLogout()
                    End If
                Else
                    ShowMessage(getValueByKey("SO008"), "SO008 - " & getValueByKey("CLAE04"))
                End If
            Else
                ShowMessage(getValueByKey("OBD006"), "OBD006 - " & getValueByKey("CLAE04"))
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub SetTabSequence()
        Try
            '---- Set Tab Index START
            SetFormTabStop(Me, tabStopValue:=False)

            Dim ctrTablIndex As New Dictionary(Of Object, Int16)

            ctrTablIndex.Add(Me.cboDocType, 0)
            ctrTablIndex.Add(Me.txtOrderNo, 1)
            ctrTablIndex.Add(Me.BtnSearchOrder, 2)

            ctrTablIndex.Add(Me.txtBatchBarcode, 3)
            ctrTablIndex.Add(Me.grdOrderInfo, 4)

            ctrTablIndex.Add(Me.btnPay, 5)
            ctrTablIndex.Add(Me.BtnOKOD, 6)
            ctrTablIndex.Add(Me.BtnCancelOD, 7)

            SetFormTabIndex(ctrTablIndex:=ctrTablIndex)
            '---- Set Tab Index END 
            
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub



    Private Function CreateOutboundForSalesOrder() As Boolean
        Try
            Dim PickupAmount As Decimal = CDbl(dsOrder.Tables("OrderDtls").Compute("Sum(PickupAmt)", String.Empty))
        Dim SalesPersonName As String = String.Empty
        'If False OrElse CDbl(lblDeliveredAmt.Text.Trim) + PickupAmount <= CDbl(lblReceivedAmount.Text.Trim) Then
        Dim createdAt As String
        dsMain = New DataSet
        If _IsSOAssignedByOtherSiteSelected = False Then
            dsMain = clsOB.GetStructOutboundDelivery(vSiteCode, vFinYear, txtOrderNo.Text.Trim, PrintTransType.Replace(Space(1), Nothing))
        Else
            createdAt = dsOrder.Tables("OrderDtls").Rows(0)("SiteCode")
            dsMain = clsOB.GetStructOutboundDelivery(vSiteCode, vFinYear, txtOrderNo.Text.Trim, PrintTransType.Replace(Space(1), Nothing), createdAt)
        End If


        If dsMain.Tables("SalesOrderHdr").Rows.Count > 0 Then
            drMain = dsMain.Tables("SalesOrderHdr").Rows(0)
        End If

        'Changed by Rohit to generate Document No. for proper sorting
        Try
            ODNextNumber = GenDocNo("OB" & clsAdmin.TerminalID & vFinYear.Substring(vFinYear.Length - 2, 2), 15, clsCommon.getDocumentNo("OutBound", clsAdmin.SiteCode))
        Catch ex As Exception
            ODNextNumber = "OB" & clsAdmin.TerminalID & clsCommon.getDocumentNo("OutBound", clsAdmin.SiteCode)
        End Try
        'End Change by Rohit

        Dim deliveredAmt As Decimal
        For Each dr In dsMain.Tables("SalesOrderDtl").Select("DeliveredQty>0")
            '----- Code  changed By Mahesh ...Rate must be netAmount/OrderQty ...
            'deliveredAmt += (dr("SellingPrice") * dr("DeliveredQty")) - dr("LineDiscount")
            deliveredAmt += ((dr("NetAmount") / dr("Quantity")) * dr("DeliveredQty")) - dr("LineDiscount")
        Next

        Dim orderQty As Decimal = CDbl(dsOrder.Tables("OrderDtls").Compute("Sum(OrderQty)", String.Empty))
        Dim pickupQty As Decimal = CDbl(dsOrder.Tables("OrderDtls").Compute("Sum(PickupQty)", String.Empty))
        Dim deliveredQty As Decimal = CDbl(dsOrder.Tables("OrderDtls").Compute("Sum(DeliveredQty)", String.Empty))

        If orderQty = pickupQty + deliveredQty AndAlso dsMain.Tables("SalesOrderHdr").Rows.Count > 0 Then
            dsMain.Tables("SalesOrderHdr").Rows(0)("SOstatus") = "Closed"
        End If

        For Each drChange As DataRow In dsOrder.Tables("OrderDtls").Select("PickupQty>0")
            Dim filterCondn As String = String.Empty
            Dim dv As DataView
            If _IsSOAssignedByOtherSiteSelected = False Then
                filterCondn = "SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "' "
                    'filterCondn = filterCondn & "And SaleOrderNumber='" & txtOrderNo.Text.Trim & "' And EAN='" & drChange("EAN") & "'"
                    filterCondn = filterCondn & "And SaleOrderNumber='" & txtOrderNo.Text.Trim & "' And EAN='" & drChange("EAN") & "' And BillLineNo ='" & drChange("rowindex") & "'"
                dv = New DataView(dsMain.Tables("SalesOrderDtl"), filterCondn, String.Empty, DataViewRowState.CurrentRows)
            Else
                filterCondn = "SiteCode='" & createdAt & "' And FinYear='" & vFinYear & "' "
                    'filterCondn = filterCondn & "And SaleOrderNumber='" & txtOrderNo.Text.Trim & "' And EAN='" & drChange("EAN") & "'"
                    filterCondn = filterCondn & "And SaleOrderNumber='" & txtOrderNo.Text.Trim & "' And EAN='" & drChange("EAN") & "' And BillLineNo ='" & drChange("rowindex") & "'"
                dv = New DataView(dsMain.Tables("SalesOrderDtl"), filterCondn, String.Empty, DataViewRowState.CurrentRows)
            End If

            dv.AllowEdit = True
            For Each dr As DataRowView In dv
                dr("DeliveredQty") = IIf(IsDBNull(dr("DeliveredQty")), 0, dr("DeliveredQty")) + IIf(IsDBNull(drChange("PickupQty")), 0, drChange("PickupQty"))
                dr("ReservedQty") = IIf(IsDBNull(dr("ReservedQty")), 0, dr("ReservedQty")) - IIf(IsDBNull(drChange("PickupQty")), 0, drChange("PickupQty"))
                dsMain.Tables("SalesOrderHdr").Rows(0)("UPDATEDON") = DateTime.Now
                dsMain.Tables("SalesOrderHdr").Rows(0)("UPDATEDBY") = clsAdmin.UserCode
                dr("UPDATEDON") = DateTime.Now
                dr("UPDATEDBY") = clsAdmin.UserCode
            Next
            'dsMain.Tables("SalesOrderDtl").AcceptChanges()
        Next

        PrepareSOOrderHdrDataforSave(dsMain)
        PrepareSOOrderDtlDataforSave(dsMain)

        Dim netAmount = CDbl(dsMain.Tables("SalesOrderDtl").Compute("Sum(NetAmount)", String.Empty))
        Dim amountToCollect As Decimal = PickupAmount + deliveredAmt + ((netAmount - PickupAmount - deliveredAmt) * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100))
        Dim remainingAmount As Decimal = amountToCollect - CDbl(lblReceivedAmount.Text.Trim)
            '---- Set Complete PickUp Variable
            Dim IsCompletePickUpDone As Boolean = False
            Dim Pq, dq, oq As Double
            For rowIndex = 1 To grdOrderInfo.Rows.Count - 1
                Pq += CDbl(grdOrderInfo.Item(rowIndex, "PickupQty"))
                If Not IsDBNull(grdOrderInfo.Item(grdOrderInfo.Row, "DeliveredQty")) Then
                    dq += CDbl(grdOrderInfo.Item(rowIndex, "DeliveredQty"))
                End If
                oq += CDbl(grdOrderInfo.Item(rowIndex, "OrderQty"))
            Next
            If (oq = dq + Pq) Then IsCompletePickUpDone = True

        Dim Obj As New frmNHowMuchtoPay
        Obj.CtrlTxtMinAmt.Text = CDbl(lblAmountToPay.Text)
        Obj.CtrlTxtPickAmt.Text = MyRound(CDbl(lblAmountToPay.Text), clsDefaultConfiguration.BillRoundOffAt)
        Obj.ctrlTxtHowMuchPay.Text = CDbl(lblAmountToPay.Text)
            'Obj.TotalBalAmt = CDbl(netAmount) - CDbl(lblReceivedAmount.Text.Trim)
            Obj.TotalBalAmt = MyRound(CDbl(netAmount) - CDbl(lblReceivedAmount.Text.Trim) + IIf(IsCompletePickUpDone, vOtherCharges, 0), clsDefaultConfiguration.BillRoundOffAt)
        Obj.ShowDialog(Me)



        If Not Obj Is Nothing Then
            remainingAmount = Obj.CtrlTxtMinAmt.Text
            If Obj.blnAllowtoGoPaymentScreen = False Then
                Exit Function
            End If
        End If

        If remainingAmount > 0 Then

            Dim objPayment As New frmNAcceptPayment()
            objPayment.ParentRelation = "SalesOrder"
            objPayment.TotalBillAmount = CDbl(remainingAmount)
            'objPayment.MinimumBillAmount = CDbl(CtrlCashSummary1.lbltxt5)
            objPayment.MinimumBillAmount = CDbl(remainingAmount)
            objPayment.CustomerWantPay = CDbl(remainingAmount)
            objPayment.ctrlPayCash.txtCash.Value = CDbl(remainingAmount)
            objPayment.PaymentType = clsAcceptPayment.PaymentType.Accept
            objPayment.RoundAt = clsDefaultConfiguration.BillRoundOffAt

            objPayment.CLPCustomerCardNumber = CtrlCustDtls1.lblCustNoValue.Text
            objPayment.CLPCustomerName = CtrlCustDtls1.lblCustNameValue.Text

            objPayment.ShowDialog(Me)

            'Issue ID 8003 by sameer
            If objPayment.IsCancelAcceptPayment Then
                Return False
            End If

            _dsPayment = New DataSet
            _dsPayment = objPayment.ReciptTotalAmount()

            If _dsPayment.Tables.Count > 0 AndAlso _dsPayment.Tables(0).Rows.Count > 0 Then
                objPayment.Close()
            End If
            dsPayment.Tables("MSTRecieptType").Merge(_dsPayment.Tables("MSTRecieptType"))

            If Not (dsPayment Is Nothing) AndAlso dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables(0).Rows.Count > 0 Then
                dsMain.Tables("SalesOrderHdr").Rows(0)("AdvanceAmt") = CType(IIf(dsMain.Tables("SalesOrderHdr").Rows(0)("AdvanceAmt") Is DBNull.Value, 0, dsMain.Tables("SalesOrderHdr").Rows(0)("AdvanceAmt")), Double) + CType(dsPayment.Tables(0).Compute("Sum(Amount)", ""), Double)
            Else
                dsMain.Tables("SalesOrderHdr").Rows(0)("AdvanceAmt") = dsMain.Tables("SalesOrderHdr").Rows(0)("AdvanceAmt")
            End If

            dsMain.Tables("SalesOrderHdr").Rows(0)("BalanceAmount") = Math.Round(CDbl(dsMain.Tables("SalesOrderDtl").Compute("Sum(NetAmount)", String.Empty)) - CDbl(dsMain.Tables("SalesOrderHdr").Rows(0)("AdvanceAmt")), 2)


            PrepareInvcDataforSave(dsMain, IIf(String.IsNullOrEmpty(createdAt) = False, createdAt, clsAdmin.SiteCode))
        End If

        Dim drOBInfo As DataRow
            dtOBItemInfo = clsOB.GetOBItemsInfo()
        dtOBItemInfo.Rows.Clear()

        Dim Dstemp = objSO.GetSOBulkComboTableStruct(vSiteCode, IIf(txtOrderNo.Text.Trim = String.Empty, 0, txtOrderNo.Text.Trim))
        DtSoBulkComboHdr = Dstemp.Tables("SoBulkComboHdr")
        DtSoBulkComboDtl = Dstemp.Tables("SoBulkComboDtl")

        For Each drItemInfo As DataRow In dsOrder.Tables("OrderDtls").Select("PickupQty>0")
                drOBInfo = dtOBItemInfo.NewRow()
                drOBInfo("ArticleCode") = drItemInfo("ArticleCode")
                drOBInfo("Discription") = drItemInfo("ArticleName")
                drOBInfo("Quantity") = drItemInfo("OrderQty")
                drOBInfo("SellingPrice") = drItemInfo("Rate")
                drOBInfo("Discount") = IIf(drItemInfo("LineDiscount") Is DBNull.Value, 0, drItemInfo("LineDiscount"))
                drOBInfo("ExclTaxAmt") = IIf(drItemInfo("ExclTaxAmt") Is DBNull.Value, 0, drItemInfo("ExclTaxAmt"))
                drOBInfo("NetAmount") = drItemInfo("NetAmount") ' CDbl((drItemInfo("NetAmount") / drItemInfo("OrderQty")) * drItemInfo("PickupQty"))
                drOBInfo("PickUpQty") = drItemInfo("PickUpQty")
                drOBInfo("DeliveredQty") = drItemInfo("DeliveredQty")
                drOBInfo("ReservedQty") = 0
                drOBInfo("IsCLP") = True
                drOBInfo("IsStatus") = "Inserted"
                drOBInfo("TotalTaxAmt") = IIf(drItemInfo("TotalTaxAmount") Is DBNull.Value, 0, drItemInfo("TotalTaxAmount"))
                drOBInfo("ArticleDiscription") = drItemInfo("ArticleName")
                drOBInfo("rowIndex") = drItemInfo("rowIndex")
                drOBInfo("BillLineNo") = drItemInfo("rowIndex")
                drOBInfo("totaldiscpercentage") = drItemInfo("DiscountPercentage")
            If Not IsDBNull(drItemInfo("SalesPersonFullName")) Then SalesPersonName = drItemInfo("SalesPersonFullName")
            dtOBItemInfo.Rows.Add(drOBInfo)
        Next
        'dsMain.

        If clsOB.PrepareSaveOutboundData(dsMain, vSiteCode, vFinYear, txtOrderNo.Text.Trim, ODNextNumber, "SYSTEM") = True Then

            Dim vSalesOrderExpectedDeliveryDate As DateTime
                vSalesOrderExpectedDeliveryDate = dsMain.Tables("SalesOrderHdr").Rows(0)("ActualDeliveryDate")

                Dim invoiceTo = dsMain.Tables("SalesOrderHdr").Rows(0)("InvoiceCustName")
                Dim Remarks = dsMain.Tables("SalesOrderHdr").Rows(0)("Remarks")
                Dim objPrintDelivery As New SpectrumPrint.clsPrintSalesOrder(clsDefaultConfiguration.SOPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.DeliveryNote, vSiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, txtOrderNo.Text.Trim, dtCustInfo, dtOBItemInfo, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, dtPrinterInfo, ShowFullName:=clsDefaultConfiguration.PrintItemFullName, dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, SalesOrderDeliveryDate:=vSalesOrderExpectedDeliveryDate, strSalesPerson:=SalesPersonName, strInvoiceTo:=invoiceTo)
            dtOBItemInfo.Rows.Clear()

            For Each drItemInfo As DataRow In dsOrder.Tables("OrderDtls").Rows
                drOBInfo = dtOBItemInfo.NewRow()
                drOBInfo("ArticleCode") = drItemInfo("ArticleCode")
                drOBInfo("Discription") = drItemInfo("ArticleName")
                drOBInfo("Quantity") = drItemInfo("OrderQty")
                drOBInfo("SellingPrice") = drItemInfo("Rate")
                drOBInfo("Discount") = IIf(drItemInfo("LineDiscount") Is DBNull.Value, 0, drItemInfo("LineDiscount"))
                drOBInfo("ExclTaxAmt") = IIf(drItemInfo("ExclTaxAmt") Is DBNull.Value, 0, drItemInfo("ExclTaxAmt"))
                drOBInfo("NetAmount") = drItemInfo("NetAmount")
                drOBInfo("GrossAmt") = drItemInfo("GrossAmount")
                drOBInfo("PickUpQty") = drItemInfo("PickUpQty")
                drOBInfo("DeliveredQty") = drItemInfo("DeliveredQty")
                drOBInfo("ArticleDiscription") = drItemInfo("ArticleName")
                If Not IsDBNull(drItemInfo("SalesPersonFullName")) Then SalesPersonName = drItemInfo("SalesPersonFullName")
                drOBInfo("rowIndex") = drItemInfo("rowIndex")
                drOBInfo("ReservedQty") = 0
                drOBInfo("IsCLP") = True
                    drOBInfo("TotalTaxAmt") = IIf(drItemInfo("TotalTaxAmount") Is DBNull.Value, 0, drItemInfo("TotalTaxAmount"))
                    drOBInfo("IsStatus") = "Inserted"
                    drOBInfo("BillLineNo") = drItemInfo("rowIndex")
                    drOBInfo("totaldiscpercentage") = drItemInfo("DiscountPercentage")
                dtOBItemInfo.Rows.Add(drOBInfo)
            Next
            Dim dt = clsOB.GetOtherlocationSOItems(clsAdmin.SiteCode, clsAdmin.Financialyear, txtOrderNo.Text)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each drItemInfo As DataRow In dt.Rows
                    drOBInfo = dtOBItemInfo.NewRow()
                    drOBInfo("ArticleCode") = drItemInfo("ArticleCode")
                    drOBInfo("Discription") = drItemInfo("ArticleName")
                    drOBInfo("Quantity") = drItemInfo("OrderQty")
                    drOBInfo("SellingPrice") = drItemInfo("Rate")
                    drOBInfo("Discount") = IIf(drItemInfo("LineDiscount") Is DBNull.Value, 0, drItemInfo("LineDiscount"))
                    drOBInfo("ExclTaxAmt") = IIf(drItemInfo("ExclTaxAmt") Is DBNull.Value, 0, drItemInfo("ExclTaxAmt"))
                    drOBInfo("NetAmount") = drItemInfo("NetAmount")
                    drOBInfo("GrossAmt") = drItemInfo("GrossAmount")
                    drOBInfo("PickUpQty") = drItemInfo("PickUpQty")
                    drOBInfo("DeliveredQty") = drItemInfo("DeliveredQty")
                    drOBInfo("ReservedQty") = 0
                    drOBInfo("IsCLP") = True
                        drOBInfo("TotalTaxAmt") = IIf(drItemInfo("TotalTaxAmount") Is DBNull.Value, 0, drItemInfo("TotalTaxAmount"))
                        drOBInfo("IsStatus") = "Inserted"
                        drOBInfo("BillLineNo") = drItemInfo("rowIndex")
                        drOBInfo("totaldiscpercentage") = drItemInfo("DiscountPercentage")
                    dtOBItemInfo.Rows.Add(drOBInfo)
                Next
            End If
            Dim dtInvoice As New DataTable
            dtInvoice = clsCommon.GetPaymentInfo

            Dim dtOBItemInfo1 As New DataTable()
            dtOBItemInfo.DefaultView.RowFilter = "PickUpQty>0"

            Dim SoCreatedOnDate As DateTime
            If dsMain.Tables("SalesOrderHdr").Rows.Count > 0 Then
                SoCreatedOnDate = dsMain.Tables("SalesOrderHdr").Rows(0)("CREATEDON")
            End If

            Dim paymentDate As DateTime
            If dsMain.Tables("SalesInvoice").Rows.Count > 0 Then
                paymentDate = dsMain.Tables("SalesInvoice").Rows(0)("CREATEDON")
            End If

            Dim dsOtherCharges As New DataSet
            Dim d As DataTable
            If dsMain.Tables.Contains("SalesOrderOtherCharges") Then
                d = dsMain.Tables("SalesOrderOtherCharges").Copy()
            End If
            dsOtherCharges.Tables.Add(d)
                d.TableName = "NewOtherCharges"
                Dim strSoStatus As String
                If IsCompletePickUpDone = True Then
                    strSoStatus = "Close"
                Else
                    strSoStatus = "Open"
                End If

                Dim objPrintInvoice As New SpectrumPrint.clsPrintSalesOrder(clsDefaultConfiguration.SOPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Payment, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, txtOrderNo.Text.Trim, CtrlCustDtls1.dtCustmInfo, dtOBItemInfo, dsPayment.Tables("MSTRecieptType"), String.Empty, vSalesInvcNo, "", clsDefaultConfiguration.BillRoundOffAt, Nothing, dtPrinterInfo, strSoStatus, ShowFullName:=clsDefaultConfiguration.PrintItemFullName, dsOtherCost:=dsOtherCharges, dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, strSalesPerson:=SalesPersonName, SalesOrderCreationDate:=SoCreatedOnDate, SalesOrderDeliveryDate:=vSalesOrderExpectedDeliveryDate, SalesPaymentDate:=paymentDate, strInvoiceTo:=invoiceTo)

            ShowMessage(getValueByKey("OBD003"), "OBD003 - " & getValueByKey("CLAE04"))

            If (dsPayment.Tables("MSTRecieptType") IsNot Nothing) Then
                dsPayment.Tables("MSTRecieptType").Rows.Clear()
            End If
        Else
            ShowMessage(getValueByKey("OBD004"), "OBD004 - " & getValueByKey("CLAE04"))
            CreateOutboundForSalesOrder = False
        End If

        dsOrder.Tables("OrderDtls").Rows.Clear()
        dsOrder.Tables("SalesInvoice").Rows.Clear()
        RefreshInvoiceGrid(dsOrder.Tables("OrderDtls"))
        ClearCustomerInfo()
        ResetLabelFields()
        CreateOutboundForSalesOrder = True
        'Else
        'ShowMessage(getValueByKey("OBD005"), "OBD005 - " & getValueByKey("CLAE04"))
        'CreateOutboundForSalesOrder = False
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Function CreateOutboundForBirthList() As Boolean
        dsMain = New DataSet
        vFinYear = dsOrder.Tables("SalesInvoice").Rows(0).Item("FinYear")
        dsMain = clsOB.GetStructOutboundDelivery(vSiteCode, vFinYear, txtOrderNo.Text.Trim, PrintTransType.Replace(Space(1), Nothing))

        'Changed by Rohit to generate Document No. for proper sorting
        Try
            ODNextNumber = GenDocNo("OB" & clsAdmin.TerminalID & vFinYear.Substring(vFinYear.Length - 2, 2), 15, clsCommon.getDocumentNo("OutBound", clsAdmin.SiteCode))
        Catch ex As Exception
            ODNextNumber = "OB" & clsAdmin.TerminalID & clsCommon.getDocumentNo("OutBound", clsAdmin.SiteCode)
        End Try
        'End Change by Rohit

        Try
            PrepareBLOrderHdrDataforSave(dsMain)
            PrepareBLOrderDtlDataforSave(dsMain)

            If (dsPayment IsNot Nothing AndAlso dsPayment.Tables.Count > 0) Then
                PrepareInvcDataforSave(dsMain, clsAdmin.SiteCode)
            End If

            Dim drOBInfo As DataRow
            Dim drOBInfoTp As DataRow
            dtOBItemInfo = clsOB.GetOBItemsInfo
            dtOBItemInfo.Rows.Clear()

            For Each drItemInfo As DataRow In dsOrder.Tables("OrderDtls").Select("PickupQty>0")

                drOBInfoTp = dsMain.Tables("BirthListSalesDtl").Select("ArticleCode='" & drItemInfo("ArticleCode") & "' and EAN='" & drItemInfo("EAN") & "'")(0)

                drOBInfo = dtOBItemInfo.NewRow()
                drOBInfo("ArticleCode") = drItemInfo("ArticleCode")
                drOBInfo("Discription") = drItemInfo("ArticleName")
                drOBInfo("Quantity") = drItemInfo("PickupQty")
                drOBInfo("SellingPrice") = drOBInfoTp("Rate")
                drOBInfo("Discount") = IIf(drOBInfoTp("TotalDiscountAmt") Is DBNull.Value, 0, drOBInfoTp("TotalDiscountAmt"))
                drOBInfo("ExclTaxAmt") = IIf(drOBInfoTp("TaxAmount") Is DBNull.Value, 0, drOBInfoTp("TaxAmount"))
                drOBInfo("NetAmount") = CDbl(drOBInfoTp("Rate") * drItemInfo("PickupQty"))
                drOBInfo("PickUpQty") = drItemInfo("PickUpQty")
                drOBInfo("ReservedQty") = 0
                drOBInfo("IsCLP") = True
                drOBInfo("totaldiscpercentage") = IIf(drOBInfoTp("TotalDiscountAmt") Is DBNull.Value, 0, drOBInfoTp("TotalDiscountAmt"))
                drOBInfo("TotalTaxAmt") = IIf(drOBInfoTp("TaxAmount") Is DBNull.Value, 0, drOBInfoTp("TaxAmount"))

                dtOBItemInfo.Rows.Add(drOBInfo)
            Next

            If clsOB.PrepareSaveOutboundData(dsMain, vSiteCode, vFinYear, txtOrderNo.Text.Trim, ODNextNumber, "SYSTEM") = True Then

                Dim objPrintDelivery As New SpectrumPrint.clsPrintSalesOrderNew(clsDefaultConfiguration.BLPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrderNew.PrintSOTransactionSet.DeliveryNote, vSiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, txtOrderNo.Text.Trim, dtCustInfo, dtOBItemInfo, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, dtPrinterInfo)


                Dim dtInvoice As New DataTable
                dtInvoice = clsCommon.GetPaymentInfo

                Dim objPrintInvoice As New SpectrumPrint.clsPrintSalesOrderNew(clsDefaultConfiguration.BLPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, SpectrumPrint.clsPrintSalesOrderNew.PrintSOTransactionSet.Payment, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, txtOrderNo.Text.Trim, CtrlCustDtls1.dtCustmInfo, dtOBItemInfo, dtInvoice, String.Empty, Nothing, Nothing, Nothing, Nothing, Nothing, dtPrinterInfo)

                ShowMessage(getValueByKey("OBD003"), "OBD003 - " & getValueByKey("CLAE04"))
            Else
                ShowMessage(getValueByKey("OBD004"), "OBD004 - " & getValueByKey("CLAE04"))
                CreateOutboundForBirthList = False
            End If

            For tableIndex = 0 To dsOrder.Tables.Count - 1
                dsOrder.Tables(tableIndex).Rows.Clear()
            Next
 
            RefreshInvoiceGrid(dsOrder.Tables("OrderDtls"))
            ClearCustomerInfo()
            CreateOutboundForBirthList = True
            ResetLabelFields()

        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Private Function PrepareSOOrderHdrDataforSave(ByRef dsMain As DataSet) As Boolean
        Dim drOrderHdr As DataRow
        dsMain.Tables("OrderHdr").Rows.Clear()
        Try
            drOrderHdr = dsMain.Tables("OrderHdr").NewRow
            drOrderHdr("SiteCode") = vSiteCode
            drOrderHdr("FinYear") = vFinYear
            drOrderHdr("DocumentNumber") = ODNextNumber
            drOrderHdr("DocumentType") = DocumentType
            drOrderHdr("DocDate") = objComn.GetCurrentDate
            drOrderHdr("SupplierCode") = vSupplierCode
            drOrderHdr("PurchaseGroupCode") = DBNull.Value
            drOrderHdr("DeliverySiteCode") = vSiteCode
            drOrderHdr("ExpectedDeliveryDt") = drMain("ActualDeliveryDate")
            drOrderHdr("SalesOrderRef") = txtOrderNo.Text.Trim

            drOrderHdr("PaymentAfterDelDays") = DBNull.Value
            drOrderHdr("AdditionalTermsNConditions") = DBNull.Value
            drOrderHdr("AdditionalPaymentTerms") = DBNull.Value

            Dim vDocNetValue As Decimal = IIf(dsOrder.Tables("OrderDtls").Compute("Sum(NetAmount)", "PickupQty>0") Is DBNull.Value, 0, dsOrder.Tables("OrderDtls").Compute("Sum(NetAmount)", "PickupQty>0"))
            Dim vDocGrossValue As Decimal = IIf(dsOrder.Tables("OrderDtls").Compute("Sum(GrossAmount)", "PickupQty>0") Is DBNull.Value, 0, dsOrder.Tables("OrderDtls").Compute("Sum(GrossAmount)", "PickupQty>0"))

            drOrderHdr("DocNetValue") = vDocNetValue
            drOrderHdr("DocGrossValue") = vDocGrossValue
            drOrderHdr("IsClosed") = True
            drOrderHdr("IsFreezed") = True

            drOrderHdr("ReturnReasonCode") = " "
            drOrderHdr("Remarks") = DBNull.Value
            drOrderHdr("ApprovedDate") = DBNull.Value
            drOrderHdr("ApprovedLevel") = DBNull.Value
            drOrderHdr("ApprovalLevel") = DBNull.Value
            drOrderHdr("AmmendmentNo") = DBNull.Value

            drOrderHdr("ClosedDate") = DBNull.Value
            drOrderHdr("Transporter") = DBNull.Value
            drOrderHdr("DocApprovalID") = DBNull.Value
            drOrderHdr("ParentOrderNo") = DBNull.Value

            drOrderHdr("CREATEDAT") = vSiteCode
            drOrderHdr("CREATEDBY") = clsAdmin.UserCode
            drOrderHdr("CREATEDON") = objComn.GetCurrentDate
            drOrderHdr("UPDATEDAT") = vSiteCode
            drOrderHdr("UPDATEDBY") = clsAdmin.UserCode
            drOrderHdr("UPDATEDON") = objComn.GetCurrentDate
            drOrderHdr("STATUS") = True

            dsMain.Tables("OrderHdr").Rows.Add(drOrderHdr)

            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function
    Private Function PrepareSOOrderDtlDataforSave(ByRef dsMain As DataSet) As Boolean
        Dim drOrderDtl As DataRow
        Dim rowIndex As Integer = 1
        Try
            If clsDefaultConfiguration.IsBatchManagementReq Then

                For Each Barcode As SpectrumCommon.BtachbarcodeInfo In BarcodeList

                    Dim drScan = dsOrder.Tables("OrderDtls").Select("PickupQty>0 AND EAN='" & Barcode.EAN & "'").FirstOrDefault()
                    drOrderDtl = dsMain.Tables("OrderDtl").NewRow
                    drOrderDtl("SiteCode") = vSiteCode
                    drOrderDtl("FinYear") = vFinYear
                    drOrderDtl("DocumentNumber") = ODNextNumber
                    drOrderDtl("ArticleCode") = drScan("ArticleCode")
                    drOrderDtl("EAN") = drScan("EAN")

                    drOrderDtl("LineNumber") = rowIndex
                    drOrderDtl("Qty") = drScan("OrderQty")
                    drOrderDtl("UnitofMeasure") = drScan("UnitofMeasure")
                    drOrderDtl("OpenQty") = DBNull.Value
                    drOrderDtl("BarCode") = Barcode.BatchBarcode
                    drOrderDtl("DeliveredQty") = Barcode.Qty
                    drOrderDtl("DeliveryCompleted") = DBNull.Value
                    drOrderDtl("PurchaseGroupCode") = DBNull.Value
                    drOrderDtl("RefDocument") = DBNull.Value
                    drOrderDtl("RefDocumentNo") = DBNull.Value

                    drOrderDtl("PONo") = DBNull.Value
                    drOrderDtl("BirthListId") = DBNull.Value
                    drOrderDtl("SaleOrderNumber") = txtOrderNo.Text
                    drOrderDtl("AllocationRule") = DBNull.Value
                    'drOrderDtl("MRP") = drScan("Rate")
                    drOrderDtl("MRP") = Math.Round(drScan("NetAmount") / drScan("OrderQty"), clsDefaultConfiguration.DecimalPlaces)
                    drOrderDtl("CostPrice") = drScan("Rate")
                    drOrderDtl("NetCostPrice") = drScan("Rate")

                    drOrderDtl("ExciseDutyAmt") = DBNull.Value
                    drOrderDtl("ExciseDutyRate") = DBNull.Value
                    drOrderDtl("PurchaseTaxAmt") = DBNull.Value
                    drOrderDtl("PurchaseTaxRate") = DBNull.Value
                    drOrderDtl("AdditionalChargeAmt") = DBNull.Value
                    drOrderDtl("DiscountAmount") = drScan("LineDiscount")
                    drOrderDtl("AdditionalChargeRate") = drScan("ExclTaxAmt")
                    drOrderDtl("DocValue") = DBNull.Value
                    drOrderDtl("InboundQty") = DBNull.Value

                    drOrderDtl("DayOpenDate") = clsAdmin.DayOpenDate

                    drOrderDtl("CREATEDAT") = vSiteCode
                    drOrderDtl("CREATEDBY") = clsAdmin.UserCode
                    drOrderDtl("CREATEDON") = objComn.GetCurrentDate
                    drOrderDtl("UPDATEDAT") = vSiteCode
                    drOrderDtl("UPDATEDBY") = clsAdmin.UserName
                    drOrderDtl("UPDATEDON") = objComn.GetCurrentDate
                    drOrderDtl("STATUS") = True

                    dsMain.Tables("OrderDtl").Rows.Add(drOrderDtl)
                    rowIndex = rowIndex + 1
                Next
            Else
                For Each drScan As DataRow In dsOrder.Tables("OrderDtls").Select("PickupQty>0")
                    drOrderDtl = dsMain.Tables("OrderDtl").NewRow
                    drOrderDtl("SiteCode") = vSiteCode
                    drOrderDtl("FinYear") = vFinYear
                    drOrderDtl("DocumentNumber") = ODNextNumber
                    drOrderDtl("ArticleCode") = drScan("ArticleCode")
                    drOrderDtl("EAN") = drScan("EAN")

                    drOrderDtl("LineNumber") = rowIndex
                    drOrderDtl("Qty") = drScan("OrderQty")
                    drOrderDtl("UnitofMeasure") = drScan("UnitofMeasure")
                    drOrderDtl("OpenQty") = DBNull.Value
                    drOrderDtl("BarCode") = drScan("BatchBarcode")
                    drOrderDtl("DeliveredQty") = drScan("PickUpQty")
                    drOrderDtl("DeliveryCompleted") = DBNull.Value
                    drOrderDtl("PurchaseGroupCode") = DBNull.Value
                    drOrderDtl("RefDocument") = DBNull.Value
                    drOrderDtl("RefDocumentNo") = DBNull.Value

                    drOrderDtl("PONo") = DBNull.Value
                    drOrderDtl("BirthListId") = DBNull.Value
                    drOrderDtl("SaleOrderNumber") = txtOrderNo.Text
                    drOrderDtl("AllocationRule") = DBNull.Value
                    'drOrderDtl("MRP") = drScan("Rate")
                    drOrderDtl("MRP") = Math.Round(drScan("NetAmount") / drScan("OrderQty"), clsDefaultConfiguration.DecimalPlaces)
                    drOrderDtl("CostPrice") = drScan("Rate")
                    drOrderDtl("NetCostPrice") = drScan("Rate")

                    drOrderDtl("ExciseDutyAmt") = DBNull.Value
                    drOrderDtl("ExciseDutyRate") = DBNull.Value
                    drOrderDtl("PurchaseTaxAmt") = DBNull.Value
                    drOrderDtl("PurchaseTaxRate") = DBNull.Value
                    drOrderDtl("AdditionalChargeAmt") = DBNull.Value
                    drOrderDtl("DiscountAmount") = drScan("LineDiscount")
                    drOrderDtl("AdditionalChargeRate") = drScan("ExclTaxAmt")
                    drOrderDtl("DocValue") = DBNull.Value
                    drOrderDtl("InboundQty") = DBNull.Value

                    drOrderDtl("DayOpenDate") = clsAdmin.DayOpenDate

                    drOrderDtl("CREATEDAT") = vSiteCode
                    drOrderDtl("CREATEDBY") = clsAdmin.UserCode
                    drOrderDtl("CREATEDON") = objComn.GetCurrentDate
                    drOrderDtl("UPDATEDAT") = vSiteCode
                    drOrderDtl("UPDATEDBY") = clsAdmin.UserName
                    drOrderDtl("UPDATEDON") = objComn.GetCurrentDate
                    drOrderDtl("STATUS") = True

                    dsMain.Tables("OrderDtl").Rows.Add(drOrderDtl)
                    rowIndex = rowIndex + 1
                Next
            End If


            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function
    Private Function PrepareBLOrderHdrDataforSave(ByRef dsMain As DataSet) As Boolean
        Dim DocNetValue As Decimal = 0.0
        Dim deliveryDate As DateTime = Nothing
        Dim drBLHdrTp As DataRow
        Dim drBLReqTp As DataRow

        Dim RequstedQty As Decimal = CDbl(dsMain.Tables("BirthListRequestedItems").Compute("Sum(RequstedQty)", String.Empty))
        Dim DeliveredQty As Decimal = CDbl(dsMain.Tables("BirthListRequestedItems").Compute("Sum(DeliveredQty)", String.Empty))
        Dim PickupQty As Decimal = CDbl(dsOrder.Tables("OrderDtls").Compute("Sum(PickupQty)", String.Empty))

        If RequstedQty = DeliveredQty + PickupQty Then
            dsMain.Tables("BirthList").Rows(0).Item("BirthListStatus") = "Closed"
        End If

        For Each drItem As DataRow In dsOrder.Tables("OrderDtls").Select("PickupQty>0")
            drBLHdrTp = dsMain.Tables("BirthListSalesDtl").Select("ArticleCode='" & drItem("ArticleCode") & "' and EAN='" & drItem("EAN") & "'")(0)
            'drBLHdrTp("DeliveredQty") = drBLHdrTp("DeliveredQty") + drItem("PickupQty")

            'Rakesh-26.08.2013:Issue-7764-->Price change of a article
            drBLReqTp = dsMain.Tables("BirthListRequestedItems").Select("ArticleCode='" & drItem("ArticleCode") & "' and EAN='" & drItem("EAN") & "' And SellingPrice = '" & drItem("Rate") & "'")(0)
            drBLReqTp("DeliveredQty") = drBLReqTp("DeliveredQty") + drItem("PickupQty")

            If (CDbl(drBLReqTp("DeliveredQty")) > CDbl(drBLReqTp("BookedQty"))) Then
                drBLReqTp("BookedQty") = drBLReqTp("DeliveredQty")
            End If

            If CDbl(drBLReqTp("DeliveredQty")) < CDbl(drBLReqTp("ReservedQty")) Then
                drBLReqTp("ReservedQty") = drBLReqTp("ReservedQty") - drItem("PickupQty")
            End If


            DocNetValue += drBLHdrTp("Rate") * drItem("PickupQty")
            deliveryDate = drBLHdrTp("DeliveryDate")
        Next

        drBLHdr = dsMain.Tables("OrderHdr").NewRow

        drBLHdr("SiteCode") = vSiteCode
        drBLHdr("DocumentNumber") = ODNextNumber
        drBLHdr("FinYear") = vFinYear
        drBLHdr("DocumentType") = BLOutBoundCode
        drBLHdr("SupplierCode") = vSupplierCode
        drBLHdr("DocNetValue") = DocNetValue
        drBLHdr("DocDate") = vCurrentDate
        drBLHdr("PurchaseGroupCode") = DBNull.Value
        drBLHdr("DeliverySiteCode") = vSiteCode
        drBLHdr("ExpectedDeliveryDt") = deliveryDate
        drBLHdr("PaymentAfterDelDays") = DBNull.Value
        drBLHdr("AdditionalTermsNConditions") = DBNull.Value
        drBLHdr("AdditionalPaymentTerms") = DBNull.Value
        drBLHdr("IsClosed") = True
        drBLHdr("IsFreezed") = True
        drBLHdr("ReturnReasonCode") = "RE104"
        drBLHdr("Remarks") = DBNull.Value
        drBLHdr("ApprovedDate") = vCurrentDate
        drBLHdr("ApprovedLevel") = DBNull.Value
        drBLHdr("AmmendmentNo") = DBNull.Value
        drBLHdr("ClosedDate") = vCurrentDate
        drBLHdr("Transporter") = DBNull.Value
        drBLHdr("DocApprovalID") = DBNull.Value
        drBLHdr("ParentOrderNo") = DBNull.Value

        'drBLHdr("Month") = vCurrentDate.ToString("MM")
        drBLHdr("Month") = GetEnglishMonthNames(vCurrentDate.Month)
        drBLHdr("Day") = vCurrentDate.ToString("dd")
        drBLHdr("Quarter") = vCurrentDate.ToString("MM")

        drBLHdr("CREATEDAT") = vSiteCode
        drBLHdr("CREATEDBY") = clsAdmin.UserCode 'vUserName
        drBLHdr("CREATEDON") = vCurrentDate
        drBLHdr("UPDATEDAT") = vSiteCode
        drBLHdr("UPDATEDBY") = clsAdmin.UserCode 'vUserName
        drBLHdr("UPDATEDON") = vCurrentDate
        drBLHdr("STATUS") = 0

        dsMain.Tables("OrderHdr").Rows.Add(drBLHdr)
    End Function
    Private Function PrepareBLOrderDtlDataforSave(ByRef dsMain As DataSet) As Boolean
        Dim drBLDtlTp As DataRow
        Dim iNext As Integer = 1

        If clsDefaultConfiguration.IsBatchManagementReq Then
            For Each barcode As SpectrumCommon.BtachbarcodeInfo In BarcodeList

                Dim drItem = dsOrder.Tables("OrderDtls").Select("PickupQty>0 AND EAN='" & barcode.EAN & "'").FirstOrDefault()
                drBLDtlTp = dsMain.Tables("BirthListSalesDtl").Select("ArticleCode='" & drItem("ArticleCode") & "' and EAN='" & drItem("EAN") & "'")(0)

                drBLDtl = dsMain.Tables("OrderDtl").NewRow()
                drBLDtl("SiteCode") = vSiteCode
                drBLDtl("DocumentNumber") = ODNextNumber
                drBLDtl("FinYear") = vFinYear
                drBLDtl("ArticleCode") = drItem("ArticleCode")
                drBLDtl("EAN") = drItem("EAN")
                drBLDtl("LineNumber") = iNext
                drBLDtl("Qty") = barcode.Qty
                drBLDtl("UnitofMeasure") = 1
                drBLDtl("OpenQty") = 0
                drBLDtl("DeliveredQty") = barcode.Qty
                drBLDtl("DeliveryCompleted") = True
                drBLDtl("PurchaseGroupCode") = DBNull.Value
                drBLDtl("RefDocument") = "BirthList"
                drBLDtl("RefDocumentNo") = drBLDtlTp("SaleInvNumber")
                drBLDtl("PONo") = DBNull.Value
                drBLDtl("BirthListId") = drBLDtlTp("BirthListId")
                drBLDtl("SaleOrderNumber") = DBNull.Value
                drBLDtl("AllocationRule") = DBNull.Value
                drBLDtl("MRP") = drBLDtlTp("Rate")
                drBLDtl("CostPrice") = drBLDtlTp("CostAmt")
                drBLDtl("NetCostPrice") = drBLDtlTp("NetAmt")
                drBLDtl("ExciseDutyAmt") = DBNull.Value
                drBLDtl("ExciseDutyRate") = DBNull.Value
                drBLDtl("PurchaseTaxAmt") = drBLDtlTp("TaxAmount")
                drBLDtl("PurchaseTaxRate") = DBNull.Value
                drBLDtl("AdditionalChargeAmt") = DBNull.Value
                drBLDtl("DiscountAmount") = drBLDtlTp("TotalDiscountAmt")
                drBLDtl("AdditionalChargeRate") = DBNull.Value
                drBLDtl("DocValue") = DBNull.Value
                drBLDtl("InboundQty") = DBNull.Value
                drBLDtl("CREATEDAT") = vSiteCode
                drBLDtl("CREATEDBY") = clsAdmin.UserCode 'vUserName
                drBLDtl("CREATEDON") = vCurrentDate
                drBLDtl("UPDATEDAT") = vSiteCode
                drBLDtl("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                drBLDtl("UPDATEDON") = vCurrentDate
                drBLDtl("STATUS") = 0
                dsMain.Tables("OrderDTL").Rows.Add(drBLDtl)

                iNext += 1
            Next
        Else
            For Each drItem As DataRow In dsOrder.Tables("OrderDtls").Select("PickupQty>0")

                drBLDtlTp = dsMain.Tables("BirthListSalesDtl").Select("ArticleCode='" & drItem("ArticleCode") & "' and EAN='" & drItem("EAN") & "'")(0)

                drBLDtl = dsMain.Tables("OrderDtl").NewRow()
                drBLDtl("SiteCode") = vSiteCode
                drBLDtl("DocumentNumber") = ODNextNumber
                drBLDtl("FinYear") = vFinYear
                drBLDtl("ArticleCode") = drItem("ArticleCode")
                drBLDtl("EAN") = drItem("EAN")
                drBLDtl("LineNumber") = iNext
                drBLDtl("Qty") = drItem("PickUpQty")
                drBLDtl("UnitofMeasure") = 1
                drBLDtl("OpenQty") = 0
                drBLDtl("DeliveredQty") = drItem("PickUpQty")
                drBLDtl("DeliveryCompleted") = True
                drBLDtl("PurchaseGroupCode") = DBNull.Value
                drBLDtl("RefDocument") = "BirthList"
                drBLDtl("RefDocumentNo") = drBLDtlTp("SaleInvNumber")
                drBLDtl("PONo") = DBNull.Value
                drBLDtl("BirthListId") = drBLDtlTp("BirthListId")
                drBLDtl("SaleOrderNumber") = DBNull.Value
                drBLDtl("AllocationRule") = DBNull.Value
                drBLDtl("MRP") = drBLDtlTp("Rate")
                drBLDtl("CostPrice") = drBLDtlTp("CostAmt")
                drBLDtl("NetCostPrice") = drBLDtlTp("NetAmt")
                drBLDtl("ExciseDutyAmt") = DBNull.Value
                drBLDtl("ExciseDutyRate") = DBNull.Value
                drBLDtl("PurchaseTaxAmt") = drBLDtlTp("TaxAmount")
                drBLDtl("PurchaseTaxRate") = DBNull.Value
                drBLDtl("AdditionalChargeAmt") = DBNull.Value
                drBLDtl("DiscountAmount") = drBLDtlTp("TotalDiscountAmt")
                drBLDtl("AdditionalChargeRate") = DBNull.Value
                drBLDtl("DocValue") = DBNull.Value
                drBLDtl("InboundQty") = DBNull.Value
                drBLDtl("CREATEDAT") = vSiteCode
                drBLDtl("CREATEDBY") = clsAdmin.UserCode 'vUserName
                drBLDtl("CREATEDON") = vCurrentDate
                drBLDtl("UPDATEDAT") = vSiteCode
                drBLDtl("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                drBLDtl("UPDATEDON") = vCurrentDate
                drBLDtl("STATUS") = 0
                dsMain.Tables("OrderDTL").Rows.Add(drBLDtl)

                iNext += 1
            Next
        End If

    End Function

    Private Sub btnPay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnPay.Click
        Try
            If Not String.IsNullOrEmpty(txtOrderNo.Text.Trim) AndAlso Convert.ToDecimal(lblAmountToPay.Text) > 0 Then
                _dsPayment = New DataSet

                If cboDocType.SelectedValue = "Sales Order" Then
                    CreateOutboundForSalesOrder()
                    AutoLogout()
                    btnPay.Enabled = False
                Else
                    Dim objPayment As New frmNAcceptPayment()
                    objPayment.ParentRelation = "BrithList"
                    objPayment.TotalBillAmount = FormatNumber(CDbl(lblAmountToPay.Text), 2)
                    objPayment.PaymentType = clsAcceptPayment.PaymentType.EditBill
                    objPayment.AcceptEditBillDataSet = _dsPayment

                    objPayment.CLPCustomerCardNumber = CtrlCustDtls1.lblCustNoValue.Text
                    objPayment.CLPCustomerName = CtrlCustDtls1.lblCustNameValue.Text

                    objPayment.ShowDialog()

                    _dsPayment = objPayment.ReciptTotalAmount()

                    If objPayment.Action = "Save" Then
                        CreateOutboundForBirthList()
                        AutoLogout()
                        btnPay.Enabled = False
                    End If
                End If
            Else
                If Convert.ToDecimal(lblAmountToPay.Text) <= 0 Then
                    ShowMessage(getValueByKey("frmnoutbounddelivery.validateamt"), getValueByKey("CLAE04"))
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function PrepareInvcDataforSave(ByRef dsMain As DataSet, ByVal invoiceSitecode As String) As Boolean
        Try
            dsMain.Tables("SalesInvoice").Rows.Clear()

            If Not (dsPayment Is Nothing) AndAlso dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then

                'vSalesInvcNo = "CM" & clsAdmin.TerminalID & objComn.getDocumentNo("CM")
                If OnlineConnect = True Then
                    'Changed by Rohit to generate Document No. for proper sorting
                    'Try
                    '    vSalesInvcNo = GenDocNo("CM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, objComn.getDocumentNo("CM", clsAdmin.SiteCode))
                    'Catch ex As Exception
                    '    vSalesInvcNo = "CM" & clsAdmin.TerminalID & objComn.getDocumentNo("CM", clsAdmin.SiteCode)
                    'End Try
                    ''GST Changes By ketan
                    Try
                        ''GST changes by ketan add sitecode in billno   
                        'vSalesInvcNo = GenDocNo("CM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, objComn.getDocumentNo("CM", clsAdmin.SiteCode))
                        vSalesInvcNo = GenDocNo("C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 3, 3) & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, objComn.getDocumentNo("CM", clsAdmin.SiteCode))
                    Catch ex As Exception
                        ''GST changes by ketan add sitecode in billno   
                        'vSalesInvcNo = "CM" & clsAdmin.TerminalID & objComn.getDocumentNo("CM", clsAdmin.SiteCode)
                        vSalesInvcNo = "C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 3, 3) & objComn.getDocumentNo("CM", clsAdmin.SiteCode)
                    End Try
                    'End Change by Rohit
                Else
                    'Changed by Rohit to generate Document No. for proper sorting
                    Try
                        vSalesInvcNo = GenDocNo("OCM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, objComn.getDocumentNo("CM", clsAdmin.SiteCode))
                    Catch ex As Exception
                        vSalesInvcNo = "OCM" & clsAdmin.TerminalID & objComn.getDocumentNo("CM", clsAdmin.SiteCode)
                    End Try
                    'End Change by Rohit
                End If

                Dim drInvc As DataRow

                For Each drPayment As DataRow In dsPayment.Tables("MSTRecieptType").Rows
                    drInvc = dsMain.Tables("SalesInvoice").NewRow()

                    drInvc("SiteCode") = invoiceSitecode
                    drInvc("FinYear") = clsAdmin.Financialyear
                    drInvc("TerminalID") = clsAdmin.TerminalID
                    drInvc("DocumentNumber") = txtOrderNo.Text
                    drInvc("SaleInvNumber") = vSalesInvcNo
                    drInvc("SaleInvLineNumber") = drPayment("SrNo")

                    drInvc("DocumentType") = DocumentType
                    drInvc("TenderHeadCode") = drPayment("RecieptType")
                    drInvc("TenderTypeCode") = drPayment("RecieptTypeCode")
                    drInvc("AmountTendered") = drPayment("Amount")
                    drInvc("PaymentReceivedAt") = vSiteCode

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

                    drInvc("CREATEDAT") = clsAdmin.SiteCode
                    drInvc("CREATEDBY") = clsAdmin.UserCode 'vUserName
                    drInvc("CREATEDON") = objComn.GetCurrentDate
                    drInvc("UPDATEDAT") = vSiteCode
                    drInvc("UPDATEDBY") = clsAdmin.UserCode 'vUserName
                    drInvc("UPDATEDON") = objComn.GetCurrentDate
                    drInvc("STATUS") = True

                    dsMain.Tables("SalesInvoice").Rows.Add(drInvc)
                Next

            End If

            Return True
        Catch ex As Exception
            IsNewRow = False
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    Private Sub txtBatchBarcode_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtBatchBarcode.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim objItemSch As New clsIteamSearch
                Dim articleCode As String = objItemSch.GetArticleCodeFromBarcode(clsAdmin.SiteCode, txtBatchBarcode.Text.Trim)
                If articleCode = String.Empty OrElse articleCode = "" Then
                    MessageBox.Show(getValueByKey("BatchBarcode004"))
                Else
                    Dim dt = objItemSch.GetEANData(clsAdmin.SiteCode, articleCode, clsAdmin.LangCode, "", dtItemScanData, False, True, txtBatchBarcode.Text.Trim)
                    If dt IsNot Nothing Then

                        Dim objCM As New clsCashMemo()
                        Dim BarCodeTable As DataTable = objCM.GetBardCodesForArticle(clsAdmin.SiteCode, articleCode)

                        If Not BarCodeTable Is Nothing AndAlso BarCodeTable.Rows.Count > 0 Then
                            If String.IsNullOrEmpty(txtBatchBarcode.Text.Trim) = False Then
                                Dim barcode = BarCodeTable.Select("BatchBarcode='" & txtBatchBarcode.Text.Trim & "'", "", DataViewRowState.CurrentRows).FirstOrDefault()
                                If Not barcode Is Nothing Then
                                    If Not barcode("QtyAllocated") > 0 Then
                                        MessageBox.Show(getValueByKey("BatchBarcode003"), getValueByKey("CLAE04"))
                                        grdOrderInfo.Item(grdOrderInfo.Row, "PickupQty") = 0
                                        Exit Sub
                                        ' grdOrderInfo.Item(grdOrderInfo.Row, "BatchBarcode") = DBNull.Value
                                    End If
                                Else
                                    MessageBox.Show(getValueByKey("BatchBarcode004"), getValueByKey("CLAE04"))
                                    grdOrderInfo.Item(grdOrderInfo.Row, "PickupQty") = 0
                                    Exit Sub
                                    ' grdOrderInfo.Item(grdOrderInfo.Row, "BatchBarcode") = DBNull.Value
                                End If
                            Else
                                grdOrderInfo.Item(grdOrderInfo.Row, "PickupQty") = 0
                                'grdOrderInfo.Item(grdOrderInfo.Row, "BatchBarcode") = DBNull.Value
                                Exit Sub
                            End If
                        Else
                            MessageBox.Show(getValueByKey("BatchBarcode005"), getValueByKey("CLAE04"))
                            grdOrderInfo.Item(grdOrderInfo.Row, "PickupQty") = 0
                            Exit Sub
                            ' grdOrderInfo.Item(grdOrderInfo.Row, "BatchBarcode") = DBNull.Value
                        End If

                        Dim selectedrow = dsOrder.Tables("OrderDtls").AsEnumerable().Where(Function(w) w("EAN") = dt(0)("EAN")).FirstOrDefault()

                        If selectedrow IsNot Nothing Then
                            Dim PrevQty = selectedrow("PickUpQty")
                            selectedrow("PickUpQty") = CInt(selectedrow("PickUpQty")) + 1

                            Dim comm As New SpectrumBL.clsCommon()
                            If (comm.GetStocks(clsAdmin.SiteCode, selectedrow("EAN"), selectedrow("ArticleCode"), True, clsDefaultConfiguration.IsBatchManagementReq, txtBatchBarcode.Text.Trim) < selectedrow("PickUpQty")) Then
                                ShowMessage(True, getValueByKey("SO009"), "Warning")
                                selectedrow("PickUpQty") = PrevQty
                                Exit Sub
                            End If
                            If (IIf(selectedrow("PickUpQty").ToString() = "", 0, selectedrow("PickUpQty")) + IIf(selectedrow("DeliveredQty").ToString() = "", 0, selectedrow("DeliveredQty"))) > selectedrow("OrderQty") Then
                                ShowMessage(getValueByKey("frmnoutbounddelivery.GreaterOrderQty"), "frmnoutbounddelivery.GreaterOrderQty - " & getValueByKey("CLAE05"))
                                selectedrow("PickUpQty") = PrevQty
                                Exit Sub
                            Else
                                If BarcodeList.Any(Function(w) w.BatchBarcode = txtBatchBarcode.Text.Trim) Then
                                    BarcodeList.Find(Function(w) w.BatchBarcode = txtBatchBarcode.Text.Trim).Qty += 1
                                Else
                                    BarcodeList.Add(New SpectrumCommon.BtachbarcodeInfo() With {.ArticleCode = selectedrow("ArticleCode"), .BatchBarcode = txtBatchBarcode.Text.Trim, .EAN = selectedrow("EAN"), .LineNO = 0, .Qty = 1, .Status = True, .ArticleName = selectedrow("ArticleName"), .OrderQty = (selectedrow("OrderQty") - IIf(selectedrow("DeliveredQty").ToString() = "", 0, selectedrow("DeliveredQty")))})
                                End If

                                selectedrow("PickupAmt") = ((selectedrow("Rate") + IIf(selectedrow("ExclTaxAmt").ToString() = "", 0, selectedrow("ExclTaxAmt")) / selectedrow("Orderqty")) * selectedrow("PickupQty")) - ((selectedrow("LineDiscount") / selectedrow("Orderqty")) * selectedrow("PickupQty"))
                                RecalculatePickUpAmount()
                            End If

                        Else
                            MessageBox.Show(getValueByKey("BatchBarcode004"))
                            Exit Sub
                        End If
                    Else
                        MessageBox.Show(getValueByKey("BatchBarcode005"))
                        Exit Sub
                    End If


                End If

            End If
        Catch ex As Exception

        Finally
            txtBatchBarcode.Text = ""
        End Try
    End Sub

    Public Function AddButtonControlInGrid(ByVal rowIndex As Integer) As Boolean
        Try
            Dim getColumnType As String = String.Empty

            'Create styles with data types, formats, etc
            Dim cellStyle As C1.Win.C1FlexGrid.CellStyle

            cellStyle = grdOrderInfo.Styles.Add("CellImageType")
            cellStyle.DataType = Type.GetType("System.String")
            cellStyle.TextAlign = TextAlignEnum.LeftCenter
            cellStyle.WordWrap = True

            'Assign styles to editable cells
            Dim assignCellStyles As CellRange
            grdOrderInfo.Rows(rowIndex).HeightDisplay = 30

            Dim ButtonX As Integer = grdOrderInfo.Cols("EditBarcode").WidthDisplay

            'Create some new controls
            Dim btnBrowse As New CtrlBtn()
            btnBrowse.Tag = dsOrder.Tables("OrderDtls").Rows(rowIndex - 1)("EAN").ToString()

            btnBrowse.MaximumSize = New System.Drawing.Size(ButtonX, 30)
            'btnBrowse.SetRowIndex = rowIndex
            btnBrowse.Text = "Barcode"

            'Insert hosted control into grid
            grdOrderInfo.Controls.Add(btnBrowse)

            'host them in the C1FlexGrid
            controlList.Add(New HostedControl(grdOrderInfo, btnBrowse, rowIndex, grdOrderInfo.Cols("EditBarcode").Index, ButtonX, ButtonX))

            'ImagePathRowIndex = rowIndex
            assignCellStyles = grdOrderInfo.GetCellRange(rowIndex, 3)
            assignCellStyles.Style = grdOrderInfo.Styles("CellImageType")

            AddHandler btnBrowse.Click, AddressOf BrowseImagePath

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

  
    Private Sub grdOrderInfo_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles grdOrderInfo.Paint
        For Each hosted As HostedControl In controlList
            hosted.UpdatePosition()
        Next
    End Sub
    Private Sub BrowseImagePath(ByVal sender As Object, ByVal e As EventArgs)
        Try

            Dim blist = SpectrumCommon.ExtensionModule.DeepClone(Me.BarcodeList.Where(Function(w) w.EAN = DirectCast(sender, Button).Tag).ToList())
            If blist.Count > 0 Then
                Dim batchbarcodes As New BatchBarcodeList(blist, SpectrumCommon.TransactionType.OutBoundCreation)
                Dim result = batchbarcodes.ShowDialog()
                If result = Windows.Forms.DialogResult.OK Then
                    If RecalculatePickUp(blist) Then
                        Me.BarcodeList.RemoveAll(Function(w) w.EAN = DirectCast(sender, Button).Tag)
                        Me.BarcodeList.AddRange(blist.Where(Function(w) w.Status = True))
                    End If
                End If
            Else
                MessageBox.Show(getValueByKey("batchbarcode.Validation002"))
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Function RecalculatePickUp(ByVal blist As List(Of SpectrumCommon.BtachbarcodeInfo)) As Boolean

        For Each barcode As SpectrumCommon.BtachbarcodeInfo In blist
            For Each dr As DataRow In dsOrder.Tables("OrderDtls").Select("EAN='" & barcode.EAN & "'")

                If Not blist.Where(Function(w) w.EAN = barcode.EAN).Sum(Function(w) w.Qty) > (dr("Orderqty") + IIf(dr("DeliveredQty").ToString() = "", 0, dr("DeliveredQty"))) Then
                    dr("PickUpQty") = blist.Where(Function(w) w.EAN = barcode.EAN).Sum(Function(w) w.Qty)
                    dr("PickupAmt") = (dr("Rate") * dr("PickupQty")) - ((dr("LineDiscount") / dr("Orderqty")) * dr("PickupQty"))
                Else
                    MessageBox.Show(getValueByKey("SO033"))
                    Return False
                End If
            Next
            RecalculatePickUpAmount()
        Next
        Return True
    End Function

    Private Sub RecalculatePickUpAmount()
        Dim PickupAmount1 As Decimal = CDbl(dsOrder.Tables("OrderDtls").Compute("Sum(PickupAmt)", String.Empty))

        lblPickupAmt.Text = FormatNumber(PickupAmount1, 2)

        Dim deliveredAmt As Decimal
        For Each dr In dsOrder.Tables("OrderDtls").Select("DeliveredQty>0")
            deliveredAmt += (dr("Rate") * dr("DeliveredQty")) - dr("LineDiscount")
        Next

        Dim netAmount = CDbl(dsOrder.Tables("OrderDtls").Compute("Sum(NetAmount)", String.Empty))
        Dim amountToCollect As Decimal = PickupAmount1 + deliveredAmt + ((netAmount - PickupAmount1 - deliveredAmt) * (clsDefaultConfiguration.IsSaleAdvanceAllowed / 100))
        Dim totalPickupAmount As Decimal = amountToCollect - CDbl(lblReceivedAmount.Text.Trim)

        If (totalPickupAmount > 0) Then
            lblAmountToPay.Text = FormatNumber(totalPickupAmount, 2)
            btnPay.Enabled = True
        Else
            lblAmountToPay.Text = strZero
            btnPay.Enabled = False
        End If
    End Sub
    Private Sub ResetLabelFields()
        lblDeliveredAmt.Text = strZero
        lblPickupAmt.Text = strZero
        lblReceivedAmount.Text = strZero
        lblNetAmount.Text = strZero
        lblAmountToPay.Text = strZero
    End Sub

    Private Sub frmNOutboundDelivery_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "546-outbound-for-sales-order.htm")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        grdOrderInfo.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        grdOrderInfo.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        grdOrderInfo.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        grdOrderInfo.Rows.MinSize = 25
        grdOrderInfo.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        grdOrderInfo.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdOrderInfo.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdOrderInfo.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdOrderInfo.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdOrderInfo.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        BtnOKOD.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnOKOD.BackColor = Color.Transparent
        BtnOKOD.BackColor = Color.FromArgb(0, 107, 163)
        BtnOKOD.ForeColor = Color.FromArgb(255, 255, 255)
        BtnOKOD.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        BtnOKOD.TextAlign = ContentAlignment.MiddleCenter
        BtnOKOD.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnOKOD.FlatStyle = FlatStyle.Flat
        BtnOKOD.FlatAppearance.BorderSize = 0
        BtnOKOD.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        BtnOKOD.Size = New Size(67, 27)
        BtnCancelOD.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnCancelOD.BackColor = Color.Transparent
        BtnCancelOD.BackColor = Color.FromArgb(0, 107, 163)
        BtnCancelOD.ForeColor = Color.FromArgb(255, 255, 255)
        BtnCancelOD.TextAlign = ContentAlignment.MiddleCenter
        BtnCancelOD.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        BtnCancelOD.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnCancelOD.FlatStyle = FlatStyle.Flat
        BtnCancelOD.FlatAppearance.BorderSize = 0
        BtnCancelOD.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        BtnCancelOD.Size = New Size(67, 27)
        '  BtnCancelOD.Location = New Point(440, 534)
        BtnSearchOrder.Image = Nothing
        BtnSearchOrder.VisualStyle = C1.Win.C1Input.VisualStyle.System
        BtnSearchOrder.BackgroundImage = My.Resources.SearchItems1
        BtnSearchOrder.FlatStyle = FlatStyle.Flat
        BtnSearchOrder.BackgroundImageLayout = ImageLayout.Stretch
        BtnSearchOrder.MinimumSize = New Size(30, 21)
        BtnSearchOrder.MaximumSize = New Size(30, 21)
        BtnSearchOrder.FlatAppearance.BorderSize = 0
        lblDocType.AutoSize = False
        lblDocType.BackColor = Color.FromArgb(212, 212, 212)
        lblDocType.Size = New Size(103, 21)
        lblDocNo.AutoSize = False
        lblDocNo.Size = New Size(103, 21)
        lblDocNo.BackColor = Color.FromArgb(212, 212, 212)
        lblBatchBarcode.AutoSize = False
        lblBatchBarcode.Size = New Size(103, 21)
        lblBatchBarcode.Location = New Point(5, 132)
        lblBatchBarcode.BackColor = Color.FromArgb(212, 212, 212)
        lblDeliveredAmt.BackColor = Color.Transparent
        lblDeliveredAmt.BorderStyle = BorderStyle.None
        lblDeliveredAmt.ForeColor = Color.White
        LbDeliveredAmt.BackColor = Color.Transparent
        LbDeliveredAmt.BorderStyle = BorderStyle.None
        LbDeliveredAmt.ForeColor = Color.White
        lblPickupAmt.BackColor = Color.Transparent
        lblPickupAmt.BorderStyle = BorderStyle.None
        lblPickupAmt.ForeColor = Color.White
        LbPickupAmt.BackColor = Color.Transparent
        LbPickupAmt.BorderStyle = BorderStyle.None
        LbPickupAmt.ForeColor = Color.White
        lblReceivedAmount.BackColor = Color.Transparent
        lblReceivedAmount.BorderStyle = BorderStyle.None
        lblReceivedAmount.ForeColor = Color.White
        lblNetAmount.BackColor = Color.Transparent
        lblNetAmount.BorderStyle = BorderStyle.None
        lblNetAmount.ForeColor = Color.White
        LbNetAmount.BackColor = (Color.Transparent)
        LbNetAmount.BorderStyle = BorderStyle.None
        LbNetAmount.ForeColor = Color.White

        lblAmountToPay.BackColor = Color.Transparent
        lblAmountToPay.BorderStyle = BorderStyle.None
        lblAmountToPay.ForeColor = Color.White
        CtrlLabel1.BackColor = Color.Transparent
        CtrlLabel1.BorderStyle = BorderStyle.None
        CtrlLabel1.ForeColor = Color.White
        LbReceivedAmount.BackColor = Color.Transparent
        LbReceivedAmount.BorderStyle = BorderStyle.None
        LbReceivedAmount.ForeColor = Color.White
    End Function
End Class