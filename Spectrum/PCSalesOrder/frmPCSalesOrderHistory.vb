Imports SpectrumBL
Imports SpectrumPrint
Imports Microsoft.Reporting.WinForms
Imports Spire.Pdf
Imports System.IO
Imports C1.Win.C1BarCode
Public Class frmPCSalesOrderHistory
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    ''' <summary>
    ''' Global Variables
    ''' </summary>
    ''' <remarks></remarks> 
    Dim DtCustDtlForSOPrint As DataTable ' vipin
    Dim DtComboGridData As DataTable 'vipin
    Dim dsMain As New DataSet
    Dim dtDocInfo As New DataTable
    Dim dtSOHistory As New DataTable
    Dim dtOrderAddresses As DataTable
    Dim dsPackagingDelivery As DataSet
    Dim dsPackagingVar As DataSet
    Dim dsSOInvoice As New DataSet
    Dim _pickupHistory As New DataSet
    Dim _dsPackagingVar As New DataSet
    Dim DtSoBulkRemarks As New DataTable
    Dim drSOPrintHeader As DataRow
    Dim dtHeaderDetails As DataTable
    Dim dtOrderDetails As DataTable
    Dim dtOrderComboDetails As DataTable
    Dim dtPaymentDetails1 As DataTable
    Dim dtPaymentDetails As DataTable
    Dim dtDeliveryDetails As DataTable
    Dim dtRemark As DataTable
    Dim dtAddress As DataTable
    Dim dtStrDetails As DataTable
    Dim dtPickupHistory As DataTable
    Dim dtStrPrint As DataTable
    Dim path As String = ""
    Dim dsInv As New DataSet
    Dim objCustm As New clsCLPCustomer()
    Dim dtCustmInfo As New DataTable
    Dim dtPackagingPrintBox As New DataTable
    Dim objcomm As New clsCommon
    Dim ProgramID As String
    Dim strSumAmtDue As String
    Dim strSumAmtPaid As String
    Dim tooltip As New ToolTip
    Dim BarCodestring As String
    Dim BillLineNo As Integer = 0
    Dim _DocumentNo As String
    Dim dtArticleWisePaymentDetails As DataTable 'vipin GST TAx changes
    Dim dtReturnOrderDetails As New DataTable

    Public Property DocumentNo() As String
        Get
            Return _DocumentNo
        End Get
        Set(ByVal value As String)
            _DocumentNo = value
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

    Private _customerName As String
    Public Property CustomerName() As String
        Get
            Return _customerName
        End Get
        Set(ByVal value As String)
            _customerName = value
        End Set
    End Property

    Private _CompName As String
    Public Property CompName() As String
        Get
            Return _CompName
        End Get
        Set(ByVal value As String)
            _CompName = value
        End Set
    End Property

    Private _DepartmentName As String
    Public Property DepartmentName() As String
        Get
            Return _DepartmentName
        End Get
        Set(ByVal value As String)
            _DepartmentName = value
        End Set
    End Property

    Private _balancePoint As String
    Public Property BalancePoint() As String
        Get
            Return _balancePoint
        End Get
        Set(ByVal value As String)
            _balancePoint = value
        End Set
    End Property

    Private _MobileNo As String
    Public Property MobileNo() As String
        Get
            Return _MobileNo
        End Get
        Set(ByVal value As String)
            _MobileNo = value
        End Set
    End Property
    Public Enum PrintSOTransactionSet
        Status
    End Enum
    Private _PrintTransaction As PrintSOTransactionSet
    Public Property PrintSOTransaction() As PrintSOTransactionSet
        Get
            Return _PrintTransaction
        End Get
        Set(ByVal value As PrintSOTransactionSet)
            _PrintTransaction = value
        End Set
    End Property
#Region "Events"
    Private Sub frmPCSalesOrderHistory_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            SelectedCustmCode = String.Empty
            SelectedCustmCode = CustomerNo
            lblCustomerNameValue.Text = CustomerName
            lblCompanyNameValue.Text = CompName
            lblDepartmentNameValue.Text = DepartmentName
            lblBalancePointsValue.Text = BalancePoint
            lblMobileNoValue.Text = MobileNo
            Dim tooltip As New ToolTip
            tooltip.SetToolTip(lblCustomerNameValue, lblCustomerNameValue.Text)
            tooltip.SetToolTip(lblCompanyNameValue, lblCompanyNameValue.Text)
            tooltip.SetToolTip(lblDepartmentNameValue, lblDepartmentNameValue.Text)
            tooltip.SetToolTip(lblBalancePointsValue, lblBalancePointsValue.Text)
            tooltip.SetToolTip(lblMobileNoValue, lblMobileNoValue.Text)
            GetDataTableForPrinting()
            ProgramID = objPCSO.GetCLPProgramID(clsAdmin.SiteCode)
            dtOrderAddresses = objcomm.GetSOAddresses(CustomerNo, ProgramID, True)
            If Not dtOrderAddresses Is Nothing AndAlso dtOrderAddresses.Rows.Count > 0 Then
                Dim drTo() As DataRow = dtOrderAddresses.Select("AddressValue='To Be Decided'")
                If Not drTo.Length > 0 Then
                    Dim NewRow As DataRow = dtOrderAddresses.NewRow()
                    NewRow(0) = 0
                    NewRow(1) = "To Be Decided"
                    NewRow(2) = "Address"
                    dtOrderAddresses.Rows.InsertAt(NewRow, 0)
                End If
            End If
            If Not String.IsNullOrEmpty(SelectedCustmCode) Then
                Call LoadDocumentInfo()
                Call getBinding()
                Call SetCulture(Me, Me.Name)
                If grdSOInfo.RowCount > 0 Then
                    DocumentNo = IIf(grdSOInfo.Item(grdSOInfo.Row, EnumDocs.SalesOrderNo) Is System.DBNull.Value, "", grdSOInfo.Item(grdSOInfo.Row, EnumDocs.SalesOrderNo))
                    strSumAmtDue = grdSOInfo.Item(grdSOInfo.Row, "AmountDue")
                    strSumAmtPaid = grdSOInfo.Item(grdSOInfo.Row, "AmountPaid")
                    Call GetSalesOrderDetails(DocumentNo, clsAdmin.SiteCode)

                ElseIf grdSOInfo.RowCount = 0 Then
                    lblSONoValue.Text = "-"
                    lblStoreNameValue.Text = "-"
                    lblBookingValue.Text = "-"

                    lblSONoOrderValue.Text = "-"
                    lblStoreNameOrderValue.Text = "-"
                    lblBookingValueOrder.Text = "-"

                    lblSONoValuePickup.Text = "-"
                    lblStoreNameValuePickup.Text = "-"
                    lblBookingValuePickup.Text = "-"

                    lblSoNoValuePayment.Text = "-"
                    lblStoreNameValuePayment.Text = "-"
                    lblBookingValuePayment.Text = "-"
                End If
            Else
                Me.Close()


            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            If clsDefaultConfiguration.ClientForMail = "PC" Then 'vipin
                CtrlCashSummary1.CtrlLabel1.Text = "SO Amt"
                CtrlCashSummary1.CtrlLabel4.Text = "Gross Amt"
                CtrlCashSummary1.CtrlLabel8.Text = "SO Return Amt"
                CtrlCashSummary1.CtrlLabelTxt8.Text = "0.0"
            End If
        Catch Ex As Exception
            LogException(Ex)
        End Try
    End Sub

    ''' <summary>
    '''  After Double Click Displaying Information of Selected Sales Order in all Grids.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdSOInfo_DoubleClick(sender As Object, e As EventArgs) Handles grdSOInfo.DoubleClick
        If grdSOInfo.Row = -1 Then Exit Sub
        If grdSOInfo.RowCount = 0 Then
            ShowMessage("Please select the record first", getValueByKey("CLAE04"))
            Exit Sub
        End If
        If grdSOInfo.Row >= 0 Then
            DocumentNo = IIf(grdSOInfo.Item(grdSOInfo.Row, EnumDocs.SalesOrderNo) Is System.DBNull.Value, "", grdSOInfo.Item(grdSOInfo.Row, EnumDocs.SalesOrderNo))
            strSumAmtDue = grdSOInfo.Item(grdSOInfo.Row, "AmountDue")
            strSumAmtPaid = grdSOInfo.Item(grdSOInfo.Row, "AmountPaid")
        End If
        Call GetSalesOrderDetails(DocumentNo, clsAdmin.SiteCode)
    End Sub

    ''' <summary>
    ''' After Key up Displaying Information of Selected Sales Order in all Grids.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdSOInfo_KeyUp(sender As Object, e As KeyEventArgs) Handles grdSOInfo.KeyUp
        If grdSOInfo.Row = -1 Then Exit Sub
        If grdSOInfo.RowCount = 0 Then
            ShowMessage("Please select the record first", getValueByKey("CLAE04"))
            Exit Sub
        End If
        If grdSOInfo.Row >= 0 Then
            DocumentNo = IIf(grdSOInfo.Item(grdSOInfo.Row, EnumDocs.SalesOrderNo) Is System.DBNull.Value, "", grdSOInfo.Item(grdSOInfo.Row, EnumDocs.SalesOrderNo))
            strSumAmtDue = grdSOInfo.Item(grdSOInfo.Row, "AmountDue")
            strSumAmtPaid = grdSOInfo.Item(grdSOInfo.Row, "AmountPaid")
        End If
        Call GetSalesOrderDetails(DocumentNo, clsAdmin.SiteCode)
    End Sub
    ''' <summary>
    ''' For Copying Text of Grid Cell on Clipboard
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdPaymentInfo_KeyDown(sender As Object, e As KeyEventArgs) Handles grdPaymentInfo.KeyDown
        If e.Control And e.KeyCode = Keys.C Then
            If grdPaymentInfo.Rows.Count > 0 Then

                If grdPaymentInfo.Cols(grdPaymentInfo.Col).Name.ToUpper = "INVOICENO" AndAlso grdPaymentInfo.Rows(grdPaymentInfo.Row)(grdPaymentInfo.Col) <> Nothing Then
                    Clipboard.SetText(grdPaymentInfo.Rows(grdPaymentInfo.Row)(grdPaymentInfo.Col))
                End If
            End If
        End If
    End Sub
    ''' <summary>
    '''  For Copying Text of Grid Cell on Clipboard
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdSOInfo_KeyDown(sender As Object, e As KeyEventArgs) Handles grdSOInfo.KeyDown
        If e.Control And e.KeyCode = Keys.C Then
            If grdSOInfo.Rows.Count > 0 Then
                If grdSOInfo.Rows(grdSOInfo.Row)(grdSOInfo.Col) <> Nothing Then
                    Clipboard.SetText(grdPaymentInfo.Rows(grdSOInfo.Row)(grdSOInfo.Col))
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' For Printing SO History 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            GenerateSOHistoryPrint()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Dim dtSiteInfo As DataTable = objcomm.GetSiteInfo(clsAdmin.SiteCode) '
    ''' <summary>
    '''  For Printing SO Invoice
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSoPrint_Click(sender As Object, e As EventArgs) Handles btnSoPrint.Click
        Dim ObjclsCommon As New clsCommon
        If grdSOInfo.RowCount > 0 Then
            DocumentNo = IIf(grdSOInfo.Item(grdSOInfo.Row, EnumDocs.SalesOrderNo) Is System.DBNull.Value, "", grdSOInfo.Item(grdSOInfo.Row, EnumDocs.SalesOrderNo))
            dsSOInvoice = objPCSO.GetSOTableStruct(clsAdmin.SiteCode, DocumentNo)
            DtSoBulkRemarks = objPCSO.SetSalesOrderRemarks(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
            dsPackagingDelivery = objPCSO.SetSalesOrderDeliveryInSO(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
            dtCustmInfo = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, CustomerNo, CustFormat:=clsDefaultConfiguration.DetailedCustomerCreationformat, IsNewSalesOrder:=clsDefaultConfiguration.IsNewSalesOrder)
            _pickupHistory = objPCSO.GetSalesOrderPickupHistory(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
            dtStrPrint = objPCSO.GetSalesOrderSTRPrint(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
            dtPackagingPrintBox = ObjclsCommon.GetPackagingBox(clsAdmin.SiteCode, 2)
            _dsPackagingVar = objPCSO.SetSalesOrderPackVariationInSO(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
            SOPrintHeader(dtSiteInfo, dtCustmInfo)
            GetSOPrintAddress()
            SOPrintRemarks()
            SOPrintDeliveryDetails()
            SOPrintPaymentDetails()
            SOPrintOrderDetails()
            SOPrintOrderComboDetails()
            SOPrintPaymentDetails1()
            '' added by ketan SO return 
            SOPrintReturnsOrderDetails()
            SOPrintReturnOrderComboDetails()
            SOStrDetails()
            SOPrintPickupHistory()
            soprintarticlepaymentwisedetails()
            DtCustDtlForSOPrint = ObjclsCommon.GetCustDetailForSoPrint(dtCustmInfo.Rows(0)("CustomerNo").ToString())
            DtComboGridData = ObjclsCommon.GetComboDtlForSoPrint(DocumentNo)

            BarCodestring = ImageToBase64(DocumentNo)

            If clsCommon.checkDiscSpce(clsDefaultConfiguration.DayCloseReportPath.Substring(0, 3)) = False Then
                ShowMessage("Insufficient disk space for saving files", "")
            End If
            GenerateSoDeliveryPrint()
            GenerateSOPrint()
            'GenerateOrderPreparationPrint()
            GenerateOrderPreparationAsPerDeliveryDetails(dtDeliveryDetails)
        ElseIf grdSOInfo.RowCount = 0 Then
            ShowMessage("Please select the record first", getValueByKey("CLAE04"))
            Exit Sub
        End If
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
#End Region
#Region "Functions"
    ''' <summary>
    ''' Fetching Data of SO History in DataTable
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataTableForPrinting()
        Try
            Dim ObjclsCommon As New clsCommon
            dtSOHistory = objPCSO.GetSchemaSOHistoryHeader()
            dtSOHistory.Rows.Clear()
            Dim drSOHistoryHeader As DataRow = dtSOHistory.NewRow()
            drSOHistoryHeader("ClientName") = clsDefaultConfiguration.ClientName & "-"
            drSOHistoryHeader("SiteName") = ObjclsCommon.GetSiteName(clsAdmin.SiteCode)
            drSOHistoryHeader("HeaderText") = "Sales Order History"
            drSOHistoryHeader("CustomerName") = CustomerName
            drSOHistoryHeader("CompanyName") = CompName
            drSOHistoryHeader("Department") = DepartmentName
            drSOHistoryHeader("MobileNo") = MobileNo
            drSOHistoryHeader("generatedDate") = DateTime.Now
            dtSOHistory.Rows.Add(drSOHistoryHeader)
        Catch ex As Exception
            LogException(ex)

        End Try
    End Sub

    ''' <summary>
    ''' Load All Information of Sales Order Customer Wise
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDocumentInfo()
        Try
            Dim objPCSO As New clsSalesOrderPC
            DocumentType = "SalesOrder"
            dtDocInfo = objPCSO.GetDocumentInfoCustmWise(clsAdmin.SiteCode, clsAdmin.Financialyear, DocumentType, SelectedCustmCode, IsNewSalesOrder:=clsDefaultConfiguration.IsNewSalesOrder)
            grdSOInfo.DataSource = dtDocInfo
            Call grdSOInfoColumnSetting()
            If Not dtDocInfo Is Nothing And dtDocInfo.Rows.Count > 0 Then
                lblTotalItems.TextAlign = ContentAlignment.MiddleLeft
                lblTotalAmt.TextAlign = ContentAlignment.MiddleRight
                lblAmtPaid.TextAlign = ContentAlignment.MiddleRight
                lblAmtDue.TextAlign = ContentAlignment.MiddleRight
                lblTotalItems.Text = FormatNumber(dtDocInfo.Compute("SUM(TotalItems)", Nothing), 0)
                lblTotalAmt.Text = FormatNumber(dtDocInfo.Compute("SUM(TotalAmount)", Nothing), 0)
                lblAmtPaid.Text = FormatNumber(dtDocInfo.Compute("SUM(AmountPaid)", Nothing), 0)
                lblAmtDue.Text = FormatNumber(dtDocInfo.Compute("SUM(AmountDue)", Nothing), 0)
                lblTotalAmt.Text = CDbl(lblTotalAmt.Text)
                lblAmtPaid.Text = CDbl(lblAmtPaid.Text)
                lblAmtDue.Text = CDbl(lblAmtDue.Text)
                lblTotalAmt.Text = IIf(lblTotalAmt.Text = 0, 0, Val(lblTotalAmt.Text).ToString("##,##,##,###", New System.Globalization.CultureInfo("hi-IN")))
                lblAmtPaid.Text = IIf(lblAmtPaid.Text = 0, 0, Val(lblAmtPaid.Text).ToString("##,##,##,###", New System.Globalization.CultureInfo("hi-IN")))
                lblAmtDue.Text = IIf(lblAmtDue.Text = 0, 0, Val(lblAmtDue.Text).ToString("##,##,##,###", New System.Globalization.CultureInfo("hi-IN")))
                lblTotalItems.Text = Integer.Parse(lblTotalItems.Text)
                tooltip.SetToolTip(lblTotalItems, lblTotalItems.Text)
                tooltip.SetToolTip(lblTotalAmt, lblTotalAmt.Text)
                tooltip.SetToolTip(lblAmtPaid, lblAmtPaid.Text)
                tooltip.SetToolTip(lblAmtDue, lblAmtDue.Text)
            End If

            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Enum EnumDocs
        SalesOrderNo = 4
    End Enum
    Private Sub grdSOInfoColumnSetting()
        Try
            grdSOInfo.Splits(0).DisplayColumns("SrNo").AllowSizing = True
            grdSOInfo.Splits(0).DisplayColumns("SrNo").Width = 45
            grdSOInfo.Columns("SrNo").Caption = "Sr.No."
            grdSOInfo.Splits(0).DisplayColumns("SrNo").Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
            grdSOInfo.Splits(0).DisplayColumns("SrNo").HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
            grdSOInfo.Columns("BookingDate").Caption = "Booking Date"
            grdSOInfo.Splits(0).DisplayColumns("BookingDate").AllowSizing = True
            grdSOInfo.Splits(0).DisplayColumns("BookingDate").Width = 95
            grdSOInfo.Splits(0).DisplayColumns("BookingDate").Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
            grdSOInfo.Splits(0).DisplayColumns("BookingDate").HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
            grdSOInfo.Columns("BookingDate").NumberFormat = "dd/MM/yyyy"
            grdSOInfo.Columns("BookingTime").Caption = "Booking Time"
            grdSOInfo.Columns("BookingTime").NumberFormat = "hh:mm tt"
            grdSOInfo.Splits(0).DisplayColumns("BookingTime").AllowSizing = True
            grdSOInfo.Splits(0).DisplayColumns("BookingTime").Width = 95
            grdSOInfo.Splits(0).DisplayColumns("BookingTime").Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
            grdSOInfo.Splits(0).DisplayColumns("BookingTime").HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
            grdSOInfo.Columns("SiteShortName").Caption = "Store Name"
            grdSOInfo.Splits(0).DisplayColumns("SiteShortName").AllowSizing = True
            grdSOInfo.Splits(0).DisplayColumns("SiteShortName").Width = 130
            grdSOInfo.Splits(0).DisplayColumns("SiteShortName").Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
            grdSOInfo.Splits(0).DisplayColumns("SiteShortName").HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
            grdSOInfo.Columns("SalesOrderNo").Caption = "SO No."
            grdSOInfo.Columns("SalesOrderNo").DataWidth = 100
            grdSOInfo.Columns("SalesPerson").Caption = "Sales Person"
            grdSOInfo.Splits(0).DisplayColumns("SalesPerson").AllowSizing = True
            grdSOInfo.Splits(0).DisplayColumns("SalesPerson").Width = 120
            grdSOInfo.Splits(0).DisplayColumns("SalesPerson").Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
            grdSOInfo.Splits(0).DisplayColumns("SalesPerson").HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
            grdSOInfo.Columns("TotalItems").Caption = "Total Items"
            grdSOInfo.Splits(0).DisplayColumns("TotalItems").Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
            grdSOInfo.Splits(0).DisplayColumns("TotalItems").HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
            grdSOInfo.Splits(0).DisplayColumns("TotalItems").Width = 90
            grdSOInfo.Columns("TotalAmount").Caption = "Total Amt.(Rs.)"
            grdSOInfo.Splits(0).DisplayColumns("TotalAmount").Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Far
            grdSOInfo.Splits(0).DisplayColumns("TotalAmount").Style.Padding.Right = 2
            grdSOInfo.Splits(0).DisplayColumns("TotalAmount").HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
            grdSOInfo.Splits(0).DisplayColumns("TotalAmount").Width = 105
            grdSOInfo.Columns("TotalAmount").NumberFormat = "0"
            grdSOInfo.Columns("AmountPaid").Caption = "Amt. Paid(Rs.)"
            grdSOInfo.Splits(0).DisplayColumns("AmountPaid").Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Far
            grdSOInfo.Splits(0).DisplayColumns("AmountPaid").Style.Padding.Right = 2
            grdSOInfo.Splits(0).DisplayColumns("AmountPaid").HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
            grdSOInfo.Columns("AmountPaid").DataWidth = 35
            grdSOInfo.Columns("AmountPaid").NumberFormat = "0"
            grdSOInfo.Columns("AmountDue").Caption = "Amt. Due(Rs.)"
            grdSOInfo.Splits(0).DisplayColumns("AmountDue").Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Far
            grdSOInfo.Splits(0).DisplayColumns("AmountDue").Style.Padding.Right = 2
            grdSOInfo.Splits(0).DisplayColumns("AmountDue").HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
            grdSOInfo.Columns("AmountDue").DataWidth = 35
            grdSOInfo.Columns("AmountDue").NumberFormat = "0"
            grdSOInfo.AllowFilter = True
            grdSOInfo.FilterBar = True
            grdSOInfo.Splits(0).DisplayColumns(0).FilterButton = True
            'grdSOInfo.CaptionStyle.BackgroundImage = Nothing
            'grdSOInfo.HeadingStyle.BackgroundImage = Nothing
            grdSOInfo.Style.BackgroundImage = Nothing
            'grdSOInfo.HighLightRowStyle.BackColor = Color.LightBlue
            'grdSOInfo.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.NoMarquee
            'grdSOInfo.MultiSelect = C1.Win.C1TrueDBGrid.MultiSelectEnum.Simple
            'grdSOInfo.Style.Borders.BorderType = C1.Win.C1TrueDBGrid.BorderTypeEnum.Inset
            'grdSOInfo.Columns("OrderPreparationSite").Caption = "OP. Site"
            'grdSOInfo.Splits(0).DisplayColumns("OrderPreparationSite").AllowSizing = True
            'grdSOInfo.Splits(0).DisplayColumns("OrderPreparationSite").Width = 80
            'grdSOInfo.Splits(0).DisplayColumns("OrderPreparationSite").Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
            'grdSOInfo.Splits(0).DisplayColumns("OrderPreparationSite").HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub ItemDetailsGridColumnSettings()
        Try
            grdItemDetails.Rows.MinSize = 28
            grdItemDetails.AllowEditing = False
            grdItemDetails.Cols("SrNo").Caption = "Sr.No."
            grdItemDetails.Cols("ArticleType").Caption = "Article Type"
            grdItemDetails.Cols("ArticleCode").Caption = "Article Code"
            grdItemDetails.Cols("Discription").Caption = "Article Description"
            grdItemDetails.Cols("UnitOfMeasure").Caption = "UOM"
            grdItemDetails.Cols("Quantity").Caption = "Order Qty."
            grdItemDetails.Cols("SellingPrice").Caption = "Price"
            grdItemDetails.Cols("SellingPrice").Format = "0"
            grdItemDetails.Cols("DiscountAmount").Caption = "Disc."
            grdItemDetails.Cols("NetAmount").Caption = "Amount"
            grdItemDetails.Cols("NetAmount").Format = "0"

            grdItemDetails.Cols("PckgSize").Caption = "Pckg. Size(KG)"
            grdItemDetails.Cols("PckgQty").Caption = "Pckg. Qty(NOS)"
            grdItemDetails.Cols("PckgType").Caption = "Packaging Type"
            grdItemDetails.Cols("PckgMaterial").Caption = "Packaging Material"
            grdItemDetails.Cols("PickupQty").Caption = "Delivered Qty."
            grdItemDetails.Cols("TotalTaxAmount").Caption = "Tax"
            grdItemDetails.Cols("NetAmount").Caption = "Gross Amt" 'vipin
            'grdItemDetails.Cols("NetAmt").Format = "0.00" '##
            grdItemDetails.AutoSizeCols()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub OrderDetailsGridColumnSettings()
        Try
            grdOrderDetails.Rows.MinSize = 28
            grdOrderDetails.AllowEditing = False
            grdOrderDetails.Cols("SrNo").Caption = "Sr.No."
            grdOrderDetails.Cols("ComboSrNo").Visible = False
            grdOrderDetails.Cols("ArticleType").Caption = "Article Type"
            If clsDefaultConfiguration.PackageFiedlsAllowed Then
                grdOrderDetails.Cols("PckgSize").Caption = "Pckg Size(kg)"
                grdOrderDetails.Cols("PckgSize").Format = "0.000"
                grdOrderDetails.Cols("PckgQty").Caption = "Pckg Qty(Nos)"
                grdOrderDetails.Cols("PckgQty").Format = "0.000"
            End If
            grdOrderDetails.Cols("DeliveryDate").Caption = "Delivery Date"
            grdOrderDetails.Cols("DeliveryDate").Format = "dd/MM/yyyy"
            grdOrderDetails.Cols("DeliveryTime").Caption = "Delivery Time"
            grdOrderDetails.Cols("DeliveryTime").Format = "hh:mm tt"
            For i = 1 To grdOrderDetails.Rows.Count - 1
                Dim row As DataRow() = dtOrderAddresses.Select("AddressKey='" & grdOrderDetails.Rows(i)("DeliveryAddress") & "'")
                If row.Length > 0 Then
                    grdOrderDetails.Rows(i)("DeliveryAddress") = row(0)("AddressValue")
                End If
            Next
            grdOrderDetails.Cols("DeliveryAddress").Caption = "Delivery Address"
            grdOrderDetails.Cols("Discription").Caption = "Order"
            grdOrderDetails.Cols("UnitOfMeasure").Caption = "UOM"
            grdOrderDetails.Cols("Quantity").Caption = "Order Qty."
            grdOrderDetails.Cols("PickupQty").Caption = "Pick Up Qty."
            grdOrderDetails.Cols("LastPickUpDateTime").Caption = "Pick Up Date & Time"
            grdOrderDetails.Cols("LastPickUpDateTime").Format = "dd/MM/yyyy hh:mm tt"
            grdOrderDetails.Cols("PendingQty").Caption = "Pending Qty"
            grdOrderDetails.Cols("PckgMaterial").Caption = "Packaging Material"
            grdOrderDetails.Cols("BillLineNo").Visible = False
            grdOrderDetails.AutoSizeCols()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub PickUpDetailsGridColumnSettings()
        Try
            grdPickUpDetails.Rows.MinSize = 28
            grdPickUpDetails.AllowEditing = False
            grdPickUpDetails.Cols("SrNo").Caption = "Sr.No."
            grdPickUpDetails.Cols("ComboSrNo").Visible = False
            grdPickUpDetails.Cols("ArticleType").Visible = False
            If clsDefaultConfiguration.PackageFiedlsAllowed Then
                grdPickUpDetails.Cols("PckgSize").Caption = "Pckg Size(kg)"
                grdPickUpDetails.Cols("PckgSize").Format = "0.000"
                grdPickUpDetails.Cols("PckgQty").Caption = "Pckg Qty(Nos)"
                grdPickUpDetails.Cols("PckgQty").Format = "0.000"
            End If
            grdPickUpDetails.Cols("DeliveryDate").Caption = "Delivery Date"
            grdPickUpDetails.Cols("DeliveryDate").Format = "dd/MM/yyyy"
            grdPickUpDetails.Cols("DeliveryTime").Caption = "Delivery Time"
            grdPickUpDetails.Cols("DeliveryTime").Format = "hh:mm tt"
            For i = 1 To grdPickUpDetails.Rows.Count - 1
                Dim row As DataRow() = dtOrderAddresses.Select("AddressKey='" & grdPickUpDetails.Rows(i)("DeliveryAddress") & "'")
                If row.Length > 0 Then
                    grdPickUpDetails.Rows(i)("DeliveryAddress") = row(0)("AddressValue")
                End If
            Next
            grdPickUpDetails.Cols("DeliveryAddress").Caption = "Delivery Address"
            grdPickUpDetails.Cols("Discription").Caption = "Order"
            grdPickUpDetails.Cols("UnitOfMeasure").Caption = "UOM"
            grdPickUpDetails.Cols("Quantity").Caption = "Order Qty."
            grdPickUpDetails.Cols("PickupQty").Caption = "Pick Up Qty."
            grdPickUpDetails.Cols("PickupDate").Caption = "Pick Up Date"
            grdPickUpDetails.Cols("PickupDate").Format = "dd/MM/yyyy hh:mm tt"
            grdPickUpDetails.Cols("PendingQty").Caption = "Pending Qty"
            grdPickUpDetails.Cols("PckgMaterial").Caption = "Packaging Material"
            grdPickUpDetails.AutoSizeCols()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub PaymentInfoGridColumnSettings()
        Try
            grdPaymentInfo.Rows.MinSize = 28
            grdPaymentInfo.AllowEditing = False
            grdPaymentInfo.Cols("InvoiceNo").Caption = "Invoice No."
            grdPaymentInfo.Cols("InvoiceDate").Caption = "Payment Date & Time"
            grdPaymentInfo.Cols("InvoiceDate").Format = "dd/MM/yyyy hh:mm tt"
            grdPaymentInfo.Cols("TerminalID").Caption = "Till No."
            grdPaymentInfo.Cols("TenderType").Caption = "Tender Type"
            grdPaymentInfo.Cols("InvoiceAmt").Caption = "Amt. Paid(Rs.)"
            grdPaymentInfo.Cols("InvoiceAmt").Format = "0"
            grdPaymentInfo.Cols("UserName").Caption = "Cashier"
            grdPaymentInfo.AutoSizeCols()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Binding the Controls with the DataSet
    ''' </summary>
    ''' <param name="StrArticle">ArticleCode</param>
    ''' <remarks></remarks>
    Private Sub getBinding()
        Try
            dsMain = objPCSO.GetStruc("0", "0")
            CtrlCashSummary1.CtrllblDiscAmt.DataBindings.Add("Text", dsMain.Tables("SALESORDERHDR"), "TOTALDISCOUNT")
            CtrlCashSummary1.CtrllblGrossAmt.DataBindings.Add("Text", dsMain.Tables("SALESORDERHDR"), "GROSSAMT")
            CtrlCashSummary1.CtrllblNetAmt.DataBindings.Add("Text", dsMain.Tables("SALESORDERHDR"), "NETAMT")
        Catch ex As Exception
            ShowMessage(getValueByKey("CM005"), "CM005 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Transaction Not properly Binded", "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Get previous Sales Order Details
    ''' </summary>
    ''' <param name="strSONo">SO No</param>
    ''' <param name="strSiteCode">Site Code</param>
    ''' <remarks></remarks>
    Private Sub GetSalesOrderDetails(ByVal strSONo As String, ByVal strSiteCode As String)
        Try
            Dim dsTemp As DataSet
            Dim DtWriteOff As DataTable
            Dim dtItemInfo As New DataTable
            Dim dtOrderInfo As New DataTable
            Dim dtPickUpInfo As New DataTable
            Dim dtPaymentInfo As New DataTable
            Dim dtcombodetails As DataTable
            dsTemp = objPCSO.GetStruc(strSONo, strSiteCode)
            Dim objPC As New clsSalesOrderPC
            DtWriteOff = objPC.GetWriteOffAmt(strSONo, strSiteCode)
            Dim DataDoc As DataRow() = dtDocInfo.Select("SalesOrderNo='" + strSONo + "'")
            lblSONoValue.Text = DataDoc(0)("SalesOrderNo")
            lblStoreNameValue.Text = DataDoc(0)("SiteShortName")
            lblBookingValue.Text = Format(DataDoc(0)("BookingDate"), "dd/MM/yyyy hh:mm tt")

            lblSONoOrderValue.Text = DataDoc(0)("SalesOrderNo")
            lblStoreNameOrderValue.Text = DataDoc(0)("SiteShortName")
            lblBookingValueOrder.Text = Format(DataDoc(0)("BookingDate"), "dd/MM/yyyy hh:mm tt")

            lblSONoValuePickup.Text = DataDoc(0)("SalesOrderNo")
            lblStoreNameValuePickup.Text = DataDoc(0)("SiteShortName")
            lblBookingValuePickup.Text = Format(DataDoc(0)("BookingDate"), "dd/MM/yyyy hh:mm tt")

            lblSoNoValuePayment.Text = DataDoc(0)("SalesOrderNo")
            lblStoreNameValuePayment.Text = DataDoc(0)("SiteShortName")
            lblBookingValuePayment.Text = Format(DataDoc(0)("BookingDate"), "dd/MM/yyyy hh:mm tt")

            dsMain.Clear()
            If Not dsTemp Is Nothing AndAlso dsTemp.Tables.Count > 0 Then
                dsMain.Tables("SALESORDERHDR").Merge(dsTemp.Tables(0), False, MissingSchemaAction.Ignore)
                dsMain.Tables("SALESORDERDTL").Merge(dsTemp.Tables(1), False, MissingSchemaAction.Ignore)
                dsMain.Tables("SALESORDERRECEIPT").Merge(dsTemp.Tables(2), False, MissingSchemaAction.Ignore)
                dsMain.Tables("SOBULKCOMBODTL").Merge(dsTemp.Tables(3), False, MissingSchemaAction.Ignore)

                If dsMain.Tables("SALESORDERHDR").Rows.Count > 0 Then
                    '----- Fill Details from Sales Order Header -----
                    Dim strSumNetAmount As String = dsMain.Tables("SALESORDERHDR").Rows(0)("NETAMT").ToString()
                    CtrlCashSummary1.CtrllblNetAmt.Text = FormatNumber(CDbl(IIf(strSumNetAmount <> "", strSumNetAmount, 0)), 2)

                    Dim strSumTotalDiscount As String = dsMain.Tables("SALESORDERHDR").Rows(0)("TOTALDISCOUNT").ToString()
                    CtrlCashSummary1.CtrllblDiscAmt.Text = FormatNumber(CDbl(IIf(strSumTotalDiscount <> "", strSumTotalDiscount, 0)), 2)

                    Dim strSumGrossAmt As String = dsMain.Tables("SALESORDERHDR").Rows(0)("GrossAmt").ToString()
                    CtrlCashSummary1.CtrllblGrossAmt.Text = FormatNumber(CDbl(IIf(strSumGrossAmt <> "", strSumGrossAmt, 0)), 2)

                    dsPackagingVar = objPCSO.SetSalesOrderPackVariationInSO(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
                    dsSOInvoice = objPCSO.GetSOTableStruct(clsAdmin.SiteCode, DocumentNo)
                    Dim TaxAmount As Double = FormatNumber(CDbl(dsPackagingVar.Tables("PackagingMaterial").Compute("Sum(TotalTaxAmount)", "")), 0) + Math.Round(IIf(dsSOInvoice.Tables("SalesOrderTaxDtls").Compute("SUM(TaxValue)", "IsDocumentLevel=1 and status=1") Is DBNull.Value, 0, dsSOInvoice.Tables("SalesOrderTaxDtls").Compute("SUM(taxValue)", "IsDocumentLevel=1 and status=1")), 2)

                    ' Dim strSumTax As String = dsMain.Tables("SALESORDERHDR").Rows(0)("TaxAmount").ToString()
                    CtrlCashSummary1.CtrllblTaxAmt.Text = FormatNumber(TaxAmount, 2)

                    'Dim strSumAmtPaid As String = dsMain.Tables("SALESORDERHDR").Rows(0)("AmountPaid").ToString()
                    CtrlCashSummary1.CtrlLabelTxt6.Text = strSumAmtPaid

                    ' Dim strSumAmtDue As String = dsMain.Tables("SALESORDERHDR").Rows(0)("AmountDue").ToString()
                    CtrlCashSummary1.CtrlLabelTxt7.Text = strSumAmtDue

                    If DtWriteOff IsNot Nothing Then
                        If DtWriteOff.Rows.Count > 0 Then
                            CtrlCashSummary1.CtrlLabeltxt5.Text = DtWriteOff.Rows(0)("WriteOffAmountTender")
                        Else
                            CtrlCashSummary1.CtrlLabeltxt5.Text = 0
                        End If
                    Else
                        CtrlCashSummary1.CtrlLabeltxt5.Text = 0
                    End If


                    CtrlCashSummary1.CtrllblNetAmt.TextAlign = ContentAlignment.MiddleRight
                    CtrlCashSummary1.CtrllblTaxAmt.TextAlign = ContentAlignment.MiddleRight
                    CtrlCashSummary1.CtrllblDiscAmt.TextAlign = ContentAlignment.MiddleRight
                    CtrlCashSummary1.CtrllblNetAmt.TextAlign = ContentAlignment.MiddleRight
                    CtrlCashSummary1.CtrlLabelTxt6.TextAlign = ContentAlignment.MiddleRight
                    CtrlCashSummary1.CtrlLabelTxt7.TextAlign = ContentAlignment.MiddleRight
                    CtrlCashSummary1.CtrlLabeltxt5.TextAlign = ContentAlignment.MiddleRight

                    CtrlCashSummary1.CtrllblNetAmt.Text = FormatNumber(CtrlCashSummary1.CtrllblNetAmt.Text, 0)
                    CtrlCashSummary1.CtrllblTaxAmt.Text = FormatNumber(CtrlCashSummary1.CtrllblTaxAmt.Text, 0)
                    CtrlCashSummary1.CtrllblDiscAmt.Text = FormatNumber(CtrlCashSummary1.CtrllblDiscAmt.Text, 0)
                    CtrlCashSummary1.CtrlLabelTxt6.Text = FormatNumber(CtrlCashSummary1.CtrlLabelTxt6.Text, 0)
                    CtrlCashSummary1.CtrllblGrossAmt.Text = FormatNumber(CtrlCashSummary1.CtrllblGrossAmt.Text, 0)
                    CtrlCashSummary1.CtrlLabelTxt7.Text = FormatNumber(CtrlCashSummary1.CtrlLabelTxt7.Text, 0)
                    CtrlCashSummary1.CtrlLabeltxt5.Text = FormatNumber(CtrlCashSummary1.CtrlLabeltxt5.Text, 0)


                    CtrlCashSummary1.CtrllblNetAmt.Text = CDbl(CtrlCashSummary1.CtrllblNetAmt.Text)
                    CtrlCashSummary1.CtrllblTaxAmt.Text = CDbl(CtrlCashSummary1.CtrllblTaxAmt.Text)
                    CtrlCashSummary1.CtrllblDiscAmt.Text = CDbl(CtrlCashSummary1.CtrllblDiscAmt.Text)
                    CtrlCashSummary1.CtrlLabelTxt6.Text = CDbl(CtrlCashSummary1.CtrlLabelTxt6.Text)
                    CtrlCashSummary1.CtrllblGrossAmt.Text = CDbl(CtrlCashSummary1.CtrllblGrossAmt.Text)
                    CtrlCashSummary1.CtrlLabelTxt7.Text = CDbl(CtrlCashSummary1.CtrlLabelTxt7.Text)
                    CtrlCashSummary1.CtrlLabeltxt5.Text = CDbl(CtrlCashSummary1.CtrlLabeltxt5.Text)


                    CtrlCashSummary1.CtrllblNetAmt.Text = IIf(CtrlCashSummary1.CtrllblNetAmt.Text = 0, 0, Val(CtrlCashSummary1.CtrllblNetAmt.Text).ToString("##,##,##,###", New System.Globalization.CultureInfo("hi-IN")))
                    CtrlCashSummary1.CtrllblTaxAmt.Text = IIf(CtrlCashSummary1.CtrllblTaxAmt.Text = 0, 0, Val(CtrlCashSummary1.CtrllblTaxAmt.Text).ToString("##,##,##,###", New System.Globalization.CultureInfo("hi-IN")))
                    CtrlCashSummary1.CtrllblDiscAmt.Text = IIf(CtrlCashSummary1.CtrllblDiscAmt.Text = 0, 0, Val(CtrlCashSummary1.CtrllblDiscAmt.Text).ToString("##,##,##,###", New System.Globalization.CultureInfo("hi-IN")))
                    CtrlCashSummary1.CtrlLabelTxt6.Text = IIf(CtrlCashSummary1.CtrlLabelTxt6.Text = 0, 0, Val(CtrlCashSummary1.CtrlLabelTxt6.Text).ToString("##,##,##,###", New System.Globalization.CultureInfo("hi-IN")))
                    CtrlCashSummary1.CtrllblGrossAmt.Text = IIf(CtrlCashSummary1.CtrllblGrossAmt.Text = 0, 0, Val(CtrlCashSummary1.CtrllblGrossAmt.Text).ToString("##,##,##,###", New System.Globalization.CultureInfo("hi-IN")))
                    CtrlCashSummary1.CtrlLabelTxt7.Text = IIf(CtrlCashSummary1.CtrlLabelTxt7.Text = 0, 0, Val(CtrlCashSummary1.CtrlLabelTxt7.Text).ToString("##,##,##,###", New System.Globalization.CultureInfo("hi-IN")))
                    CtrlCashSummary1.CtrlLabeltxt5.Text = IIf(CtrlCashSummary1.CtrlLabeltxt5.Text = 0, 0, Val(CtrlCashSummary1.CtrlLabeltxt5.Text).ToString("##,##,##,###", New System.Globalization.CultureInfo("hi-IN")))

                    CtrlCashSummary1.CtrllblGrossAmt.Visible = True
                    CtrlCashSummary1.CtrllblTaxAmt.Visible = True
                    CtrlCashSummary1.CtrllblDiscAmt.Visible = True
                    CtrlCashSummary1.CtrllblNetAmt.Visible = True
                    CtrlCashSummary1.CtrlLabelTxt6.Visible = True
                    CtrlCashSummary1.CtrlLabeltxt5.Visible = True
                    CtrlCashSummary1.CtrlLabelTxt6.Location = New System.Drawing.Point(217, 129)
                    CtrlCashSummary1.CtrlLabelTxt7.Location = New System.Drawing.Point(217, 149)
                    CtrlCashSummary1.CtrlLabelTxt7.Visible = True
                    CtrlCashSummary1.CtrlLabeltxt5.Visible = True
                    CtrlCashSummary1.CtrlLabel1.Visible = True
                    CtrlCashSummary1.CtrlLabel2.Visible = True
                    CtrlCashSummary1.CtrlLabel3.Visible = True
                    CtrlCashSummary1.CtrlLabel4.Visible = True
                    CtrlCashSummary1.CtrlLabel5.Visible = True
                    CtrlCashSummary1.CtrlLabel6.Visible = True
                    CtrlCashSummary1.CtrlLabel6.Location = New System.Drawing.Point(6, 129)
                    CtrlCashSummary1.CtrlLabel7.Visible = True
                    CtrlCashSummary1.CtrlLabel7.Location = New System.Drawing.Point(6, 149)
                    CtrlCashSummary1.CtrlLabel5.Visible = True


                    '' ADDEd By ketan pC SO Return Changes 
                    CtrlCashSummary1.CtrlLabel8.Visible = True
                    CtrlCashSummary1.CtrlLabelTxt8.Visible = True
                    CtrlCashSummary1.CtrlLabel8.Location = New System.Drawing.Point(6, 169)
                    CtrlCashSummary1.CtrlLabelTxt8.Location = New System.Drawing.Point(217, 169)



                    '--------------Getting the Details of Item Info,Order Info ,Pickup and Payment Info and Binding to Grid From Datatable
                    '---------------------------------------------------------------------------------------------------------------------
                    dtItemInfo = objPCSO.GetItemInfoSO(clsAdmin.SiteCode, strSONo)

                    Dim srnoH As Integer = 1
                    Dim srNoNH As Integer = 1
                    For index1 = 0 To dtItemInfo.Rows.Count - 1
                        If dtItemInfo(index1)("IsHeader") Then
                            dtItemInfo(index1)("SrNo") = srnoH
                            Dim result As DataRow() = dtItemInfo.Select("IsHeader=False and Articlecode='" & dtItemInfo(index1)("Articlecode").ToString & "'")
                            If result.Length > 0 Then
                                srNoNH = 1
                                For Each drr As DataRow In result
                                    drr("SrNo") = srnoH & "." & srNoNH
                                    srNoNH += 1
                                Next
                            End If
                            srnoH += 1
                        End If

                    Next
                    dtItemInfo.Columns.Remove("IsHeader")
                    grdItemDetails.DataSource = dtItemInfo
                    Call ItemDetailsGridColumnSettings()
                    dtOrderInfo = objPCSO.GetOrderInfoDelivery(clsAdmin.SiteCode, strSONo, clsDefaultConfiguration.PackageFiedlsAllowed)
                    ' *Start*---------Display Srno As display in SO New or Edit Added by sagar
                    Dim indexS As Integer = 0
                    Dim index As Integer = 1
                    Dim indexP As Integer = 1
                    Dim Ean As String
                    Dim dell As Integer = 1
                    Dim status As Boolean

                    For drindex = 0 To dtOrderInfo.Rows.Count - 1

                        If dtOrderInfo.Rows(drindex)("SrNo").ToString().IndexOf(".") <> -1 Then
                            If Ean = dtOrderInfo.Rows(drindex)("Ean").ToString() Then
                                Dim s As String() = dtOrderInfo.Rows(drindex)("SrNo").ToString().Split(".")
                                If s.Length > 1 And dtOrderInfo.Rows(drindex)("IsHeader").ToString() = "False" Then
                                    If False Then
                                        If s.Length = 2 Then
                                            If Char.IsLetter(s(1).ToString()) = True Then
                                                dtOrderInfo.Rows(drindex)("SrNo") = indexS & "." & Chr(64 + dell).ToString()
                                                dell = dell + 1
                                            Else
                                                dtOrderInfo.Rows(drindex)("SrNo") = indexS & "." & indexP
                                                indexP = indexP + 1
                                            End If

                                        End If
                                        If s.Length = 3 Then
                                            dtOrderInfo.Rows(drindex)("SrNo") = indexS & "." & s(1).ToString() & "." & Chr(64 + dell).ToString()
                                            dell = dell + 1
                                        End If

                                    Else
                                        If s.Length = 2 Then
                                            If Char.IsLetter(s(1).ToString()) = True Then
                                                dtOrderInfo.Rows(drindex)("SrNo") = indexS & "." & s(1).ToString()
                                            Else
                                                dtOrderInfo.Rows(drindex)("SrNo") = indexS & "." & indexP
                                                indexP = indexP + 1
                                            End If

                                        End If
                                        If s.Length = 3 Then
                                            dtOrderInfo.Rows(drindex)("SrNo") = indexS & "." & s(1).ToString() & "." & s(2).ToString()
                                        End If
                                    End If

                                Else

                                    dtOrderInfo.Rows(drindex)("SrNo") = indexS & "." & indexP
                                    indexP = indexP + 1
                                    dell = 1
                                End If

                            End If


                        Else
                            dtOrderInfo.Rows(drindex)("SrNo") = index
                            Ean = dtOrderInfo.Rows(drindex)("Ean").ToString()
                            index = index + 1
                            indexP = 1
                            indexS = indexS + 1
                            dell = 1
                        End If
                    Next
                    '*End*--------------------------------------

                    Dim Globalindex As Integer = 1
                    dtcombodetails = objPCSO.GetComboDetails(clsAdmin.SiteCode, strSONo)

                    Dim dtschema As DataTable
                    dtschema = objPCSO.GetOrderInfoSchema(clsDefaultConfiguration.PackageFiedlsAllowed)

                    dtschema.Rows.Clear()
                    BillLineNo = 0
                    For Each dr As DataRow In dtOrderInfo.Rows

                        Dim drSOHistoryData As DataRow = dtschema.NewRow()


                        drSOHistoryData("SrNo") = dr("SrNo")
                        drSOHistoryData("ComboSrNo") = dr("ComboSrNo")
                        drSOHistoryData("ArticleType") = dr("ArticleType")
                        drSOHistoryData("Discription") = dr("Discription")
                        drSOHistoryData("Quantity") = dr("Quantity")
                        drSOHistoryData("UnitOfMeasure") = dr("UnitOfMeasure")
                        drSOHistoryData("PckgMaterial") = dr("PckgMaterial")
                        If clsDefaultConfiguration.PackageFiedlsAllowed Then
                            drSOHistoryData("PckgSize") = dr("PckgSize")
                            drSOHistoryData("PckgQty") = dr("PckgQty")
                        End If
                        drSOHistoryData("DeliveryDate") = dr("DeliveryDate")
                        drSOHistoryData("DeliveryTime") = dr("DeliveryTime")
                        drSOHistoryData("DeliveryAddress") = dr("DeliveryAddress")
                        drSOHistoryData("PickupQty") = dr("PickupQty")
                        drSOHistoryData("ReturnQty") = dr("ReturnQty") '' added by ketan PC SO Return Chnages 
                        drSOHistoryData("LastPickupDateTime") = dr("LastPickupDateTime")
                        drSOHistoryData("PendingQty") = dr("PendingQty")
                        drSOHistoryData("BillLineNo") = dr("BillLineNo")
                        dtschema.Rows.Add(drSOHistoryData)
                        If dr("ArticleType") = "Combo" Then
                            If drSOHistoryData("BillLineNo") <> BillLineNo Then
                                For Each drr As DataRow In dtcombodetails.Select("ComboSrNo='" & dr("ComboSrNo").ToString() & "'")
                                    If dtcombodetails.Rows.Count > 0 Then
                                        Dim drso As DataRow = dtschema.NewRow()
                                        drso("ComboSrNo") = drr("ComboSrNo")
                                        drso("Discription") = drr("Discription")
                                        drso("Quantity") = drr("Quantity")
                                        drso("UnitOfMeasure") = drr("UnitOfMeasure")
                                        dtschema.Rows.Add(drso)
                                    End If
                                Next
                            End If
                            BillLineNo = dr("BillLineNo")
                        End If
                    Next




                    Dim cntr As Integer = 0
                    Dim comboSrNo As String
                    Dim cntr1 As Integer = 1
                    BillLineNo = 0
                    Dim del As Integer = 1
                    If dtschema.Rows.Count > 0 Then
                        'dtschema = dtschema.Select("", "ComboSrNo").CopyToDataTable()
                        For i = 0 To dtschema.Rows.Count - 1
                            If (dtschema.Rows(i)("ArticleType") Is DBNull.Value) Then
                                Dim dr As DataRow()
                                dr = dtschema.Select("[SrNo] <> 'n/a' AND [ArticleType] is NULL AND ComboSrNo='" & comboSrNo & "'")
                                '-Giving Sr.No to Contents of Combo Chr(64 + del).ToString()
                                '  dtschema.Rows(i)("SrNo") = cntr & "." & dr.Length + 1
                                dtschema.Rows(i)("SrNo") = Chr(96 + del).ToString()
                                del = del + 1
                            Else

                                'If dtschema.Rows(i)("BillLineNo") = BillLineNo Then
                                '    '--Sr.NO for Sub Variations
                                '    dtschema.Rows(i)("SrNo") = cntr & "." & cntr1
                                '    cntr1 = cntr1 + 1
                                'Else
                                '    '---Sr.NO for Header Articles
                                '    cntr1 = 1
                                '    cntr = cntr + 1
                                '    del = 1
                                '    dtschema.Rows(i)("SrNo") = cntr   
                                'End If
                                BillLineNo = dtschema.Rows(i)("BillLineNo")
                                comboSrNo = dtschema.Rows(i)("ComboSrNo")
                            End If
                        Next
                    End If
                    grdOrderDetails.DataSource = dtschema

                    Call OrderDetailsGridColumnSettings()

                    dtPickUpInfo = objPCSO.GetPickupInfo(clsAdmin.SiteCode, strSONo, clsDefaultConfiguration.PackageFiedlsAllowed)
                    grdPickUpDetails.DataSource = dtPickUpInfo
                    Call PickUpDetailsGridColumnSettings()

                    dtPaymentInfo = objPCSO.GetPaymentInfo(clsAdmin.SiteCode, strSONo)
                    grdPaymentInfo.DataSource = dtPaymentInfo
                    '' added by ketan pC SO Return Chnages 
                    Try
                        CtrlCashSummary1.CtrlLabelTxt8.Text = Format(IIf(dtPaymentInfo.Compute("SUM(InvoiceAmt)", "TenderType='Cash(R)'") Is DBNull.Value, 0, dtPaymentInfo.Compute("SUM(InvoiceAmt)", "TenderType='Cash(R)'")), 0)
                    Catch ex As Exception
                        LogException(ex)
                    End Try
                    Call PaymentInfoGridColumnSettings()

                Else
                    '----- Empty details 
                    CtrlCashSummary1.CtrllblNetAmt.Text = String.Empty
                    CtrlCashSummary1.CtrllblDiscAmt.Text = String.Empty
                    CtrlCashSummary1.CtrllblGrossAmt.Text = String.Empty
                    CtrlCashSummary1.CtrllblTaxAmt.Text = String.Empty
                    CtrlCashSummary1.CtrlLabelTxt6.Text = String.Empty
                    CtrlCashSummary1.CtrlLabelTxt7.Text = String.Empty
                    CtrlCashSummary1.CtrlLabeltxt5.Text = String.Empty

                End If
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Generating Print of SO History 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateSOHistoryPrint() As Boolean
        Try

            Dim path As String = ""
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\SOHistory.rdl")
            reportViewer2.LocalReport.ReportPath = appPath
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource As New ReportDataSource("dsSOHistoryHeader", dtSOHistory)
            Dim DataSource1 As New ReportDataSource("dsSOHistoryData", dtDocInfo)

            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.Refresh()
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            Dim obj As New clsCommon
            'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
            path = clsDefaultConfiguration.DayCloseReportPath & "\SO History_" & CustomerName & "_" & DateTime.Now.ToString("ddMMMyyyy_HH.mm") & ".pdf"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            If clsDefaultConfiguration.SOPrintPreviewAllowed = True Then
                Process.Start(path)
            Else
                'Code For Print SO
                PrinterName = SetPrinterName(dtPrinterInfo, "SalesOrder", PrintSOTransaction.ToString)
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

    Private Function SOPrintHeader(ByRef dtSiteInfo As DataTable, ByRef dtCustDetail As DataTable) As Boolean
        Try
            Dim deliverydate As String
            dtHeaderDetails = objPCSO.GetSOPrintHeaderTableStruc()
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
                drSOPrintHeader("SalesOrderNo") = DocumentNo
                drSOPrintHeader("OrderDate") = dsSOInvoice.Tables("SalesOrderHdr").Rows(0)("CreatedOn")
                If dsSOInvoice.Tables("SalesOrderHdr").Rows(0)("IsDelivery").ToString Then
                    drSOPrintHeader("DeliveryRequired") = "Yes"
                Else
                    drSOPrintHeader("DeliveryRequired") = "No"
                End If
                For i = 0 To dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Rows.Count - 1
                    If dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Rows(i)("STATUS") = True Then
                        If Convert.ToDateTime(dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Rows(0)("DeliveryTime")) = Convert.ToDateTime(dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Rows(i)("DeliveryTime")) Then
                            deliverydate = dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Rows(0)("DeliveryTime").ToString()
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
                drSOPrintHeader("BookedBy") = IIf(dsSOInvoice.Tables("SalesOrderHdr").Rows(0)("SalesExecutiveCode") Is DBNull.Value, clsAdmin.UserName, dsSOInvoice.Tables("SalesOrderHdr").Rows(0)("SalesExecutiveCode"))
                drSOPrintHeader("CHSN") = dtCustDetail.Rows(0)("CHSN")
                drSOPrintHeader("LocalSalesTaxNo") = dtSiteInfo.Rows(0)("LocalSalesTaxNo")
                dtHeaderDetails.Rows.Add(drSOPrintHeader)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    Private Function soprintarticlepaymentwisedetails() As Boolean 'by ketan 
        Try
            Dim dtPickUpDiscGrid As New DataTable
            If dsSOInvoice.Tables("Salesorderhdr").Rows.Count > 0 Then
                dtPickUpDiscGrid = objPCSO.GetSalesOrderPickUpDisc(clsAdmin.SiteCode, IIf(dsSOInvoice.Tables("Salesorderhdr").Rows(0)("Saleordernumber") = String.Empty, 0, dsSOInvoice.Tables("Salesorderhdr").Rows(0)("Saleordernumber")))
            End If
            Dim a = dsPackagingDelivery
            dtArticleWisePaymentDetails = objPCSO.GetSOPrintArticleWisePaymentTableStruc()
            dtArticleWisePaymentDetails.Rows.Clear()
            Dim otherCharges As Double
            If dsSOInvoice.Tables("SalesOrderOtherCharges").Rows.Count() > 0 Then
                otherCharges = dsSOInvoice.Tables("SalesOrderOtherCharges").Compute("Sum(ChargeAmount)", "")
            End If
            Dim i As Integer = 1
            If dsSOInvoice.Tables("sopackagingboxdeliverydtl").Rows.Count > 0 Then
                For Each dr As DataRow In dsSOInvoice.Tables("sopackagingboxdeliverydtl").Select("status=true", "billlineno")
                    Dim drresult As DataRow() = dsSOInvoice.Tables("soitempackagingboxdtl").Select("pkglineno='" + dr("pkglineno").ToString + "'")
                    Dim drpickupqty As DataRow() = (dsPackagingDelivery.Tables(0)).Select("deliverylineno='" + dr("deliverylineno").ToString + "'")
                    Dim drdisc As DataRow() = dsSOInvoice.Tables("sopackagingdiscdtl").Select("pkglineno='" + dr("pkglineno").ToString + "' and status=true")
                    Dim drtax As DataRow() = dsSOInvoice.Tables("salesordertaxdtls").Select("packagingindex='" + dr("pkglineno").ToString + "' and status=true")
                    Dim DsPackVar As DataRow() = _dsPackagingVar.Tables(0).Select("pkglineno='" + dr("pkglineno").ToString + "'")
                    Dim DrDiscDtl As DataRow() = dtPickUpDiscGrid.Select("pkglineno='" + dr("pkglineno").ToString + "'")
                    Dim dttx As DataTable
                    If drtax.Length > 0 Then
                        Dim taxtlabel As String = String.Empty
                        For index = 0 To drtax.Length - 1
                            taxtlabel = taxtlabel & ",'" & drtax(index)("taxlabel").ToString & "'"

                        Next
                        taxtlabel = taxtlabel.Substring(1, taxtlabel.Length - 1)
                        dttx = clsCommon.GetArticleGSTTax(taxtlabel)
                    End If

                    drSOPrintHeader = dtArticleWisePaymentDetails.NewRow()
                    drSOPrintHeader("billlineno") = dr("billlineno")
                    Dim drsrno As DataRow() = dtArticleWisePaymentDetails.Select("billlineno='" + dr("billlineno").ToString + "'")
                    If drsrno.Length = 0 Then
                        drSOPrintHeader("srno") = i
                    Else
                        i = i - 1
                        drSOPrintHeader("srno") = i & "." & drsrno.Length
                    End If
                    Dim discription = dsSOInvoice.Tables("salesorderbulkcombohdr").Select("combosrno='" & dr("billlineno") & "'")
                    If discription.Length > 0 Then
                        drSOPrintHeader("articledescription") = discription(0)("packagingboxname")
                    Else
                        drSOPrintHeader("articledescription") = clsCommon.GetArticleFullName(dr("articlecode")).ToString()
                    End If
                    drSOPrintHeader("quantity") = dr("quantity")
                    drSOPrintHeader("price") = drresult(0)("sellingprice")
                    drSOPrintHeader("netamt") = drresult(0)("sellingprice") * dr("quantity")
                    drSOPrintHeader("othercharges") = otherCharges
                    '       If drdisc.Length > 0 Then

                    If CDbl(CtrlCashSummary1.CtrllblDiscAmt.Text) <> 0 AndAlso drresult(0)("discountamount") = 0 Then
                        If DrDiscDtl.Length > 0 Then
                            drresult(0)("discountamount") = DrDiscDtl(0)("DiscountAmount")
                        Else
                            drresult(0)("discountamount") = 0
                        End If
                    End If
                    'If drresult.Length > 0 Then
                    '    'drsoprintheader("discount") = drdisc(0)("discountamount")
                    '    'drsoprintheader("discountper") = drdisc(0)("discountper")
                    '    'drsoprintheader("taxableamount") = (drresult(0)("sellingprice") * dr("quantity")) - (drdisc(0)("discountamount"))
                    '    drSOPrintHeader("discount") = drresult(0)("discountamount")  'vipin
                    '    drSOPrintHeader("discountper") = drresult(0)("discountpercentage")
                    '    drSOPrintHeader("taxableamount") = (drresult(0)("sellingprice") * dr("quantity")) - (drresult(0)("discountamount"))
                    'Else
                    '    drSOPrintHeader("discount") = 0
                    '    drSOPrintHeader("discountper") = 0
                    '    drSOPrintHeader("taxableamount") = (drresult(0)("sellingprice") * dr("quantity"))
                    'End If

                    If drresult.Length > 0 Then
                        drSOPrintHeader("discount") = (drresult(0)("discountamount") / DsPackVar(0)("Quantity")) * dr("quantity") 'vipin 
                        If drresult(0)("discountamount") = 0 Then 'vipin
                            drSOPrintHeader("discountper") = Math.Round(0, 2)
                        Else
                            drSOPrintHeader("discountper") = (((drresult(0)("discountamount") / DsPackVar(0)("Quantity")) * dr("quantity")) * 100) / (drresult(0)("sellingprice") * dr("quantity")) 'Math.Round((drresult(0)("discountamount") / CDbl(CtrlCashSummary1.CtrllblDiscAmt.Text)) * 100, 2) 'vipin
                        End If
                        drSOPrintHeader("taxableamount") = (drresult(0)("sellingprice") * dr("quantity")) - ((drresult(0)("discountamount") / DsPackVar(0)("Quantity")) * dr("quantity"))
                    Else
                        drSOPrintHeader("discount") = 0
                        drSOPrintHeader("discountper") = 0
                        drSOPrintHeader("taxableamount") = (drresult(0)("sellingprice") * dr("quantity"))
                    End If

                    'added by vipin
                    '' change by ketan issue in So print @ Prodution 
                    'drSOPrintHeader("cgstvalue") = DsPackVar(0)("TotalTaxAmount") / 2
                    'drSOPrintHeader("sgstvalue") = DsPackVar(0)("TotalTaxAmount") / 2
                    drSOPrintHeader("cgstvalue") = ((DsPackVar(0)("TotalTaxAmount") / DsPackVar(0)("Quantity")) * dr("quantity")) / 2 'DsPackVar(0)("TotalTaxAmount") / 2
                    drSOPrintHeader("sgstvalue") = ((DsPackVar(0)("TotalTaxAmount") / DsPackVar(0)("Quantity")) * dr("quantity")) / 2 'DsPackVar(0)("TotalTaxAmount") / 2

                    drSOPrintHeader("cgst") = (drSOPrintHeader("cgstvalue") / drSOPrintHeader("taxableamount")) * 100
                    drSOPrintHeader("sgst") = (drSOPrintHeader("sgstvalue") / drSOPrintHeader("taxableamount")) * 100


                    drSOPrintHeader("grossamt") = drSOPrintHeader("taxableamount") + drSOPrintHeader("cgstvalue") + drSOPrintHeader("sgstvalue")
                    ' drSOPrintHeader("hsncode") = objComn.GetHSNbyArticle(dr("articlecode"))
                    If dr("IsCombo") = True Then  'vipin to hide HSN in case of combo
                        drSOPrintHeader("hsncode") = ""
                        drSOPrintHeader("IsCombo") = "Y"
                    Else
                        drSOPrintHeader("hsncode") = objcomm.GetHSNbyArticle(dr("articlecode"))
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
    Private Function SOPrintOrderDetails() As Boolean ' 
        Try
            Dim a = dsPackagingDelivery
            dtOrderDetails = objPCSO.GetSOPrintOrderDetailsTableStruc()
            dtOrderDetails.Rows.Clear()
            Dim i As Integer = 1
            If dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Rows.Count > 0 Then
                For Each dr As DataRow In dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Select("Status=True", "BillLineNo")
                    Dim drResult As DataRow() = dsSOInvoice.Tables("SOitemPackagingBoxDtl").Select("pkgLineNo='" + dr("pkgLineNo").ToString + "'")
                    Dim drpickupQty As DataRow() = (dsPackagingDelivery.Tables(0)).Select("DeliverylineNo='" + dr("DeliverylineNo").ToString + "'")
                    drSOPrintHeader = dtOrderDetails.NewRow()
                    drSOPrintHeader("BillLineNo") = dr("BillLineNo")
                    Dim drSrNo As DataRow() = dtOrderDetails.Select("BillLineNo='" + dr("BillLineNo").ToString + "'")
                    If drSrNo.Length = 0 Then
                        drSOPrintHeader("SrNo") = i
                    Else
                        i = i - 1
                        drSOPrintHeader("SrNo") = i & "." & drSrNo.Length
                    End If
                    Dim discription = dsSOInvoice.Tables("SalesOrderBulkComboHdr").Select("ComboSrNo='" & dr("BillLineNo") & "'")
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
                    'drSOPrintHeader("PickupQuantity") = dr("DeliveredQty")
                    drSOPrintHeader("PickupQuantity") = dr("DeliveredQty") + IIf(dr("ReturnQty") Is System.DBNull.Value, 0, dr("ReturnQty"))
                    drSOPrintHeader("IsCombo") = dr("IsCombo")
                    drSOPrintHeader("header") = 1
                    drSOPrintHeader("PackagingMaterial") = drResult(0)("pckgMaterial")
                    dtOrderDetails.Rows.Add(drSOPrintHeader)
                    i = i + 1
                Next
            End If
        Catch ex As Exception

        End Try

    End Function

    Private Function SOPrintOrderComboDetails() As Boolean ' 
        Try
            Dim ObjclsCommon As New clsCommon
            dtOrderComboDetails = objPCSO.GetSOPrintOrderDetailsTableStruc()
            dtOrderComboDetails.Rows.Clear()
            Dim i As Long = 1
            If dsSOInvoice.Tables("SalesOrderBulkComboDtl").Rows.Count > 0 Then
                Dim DtComboDtlForSoPrint As DataTable = ObjclsCommon.GetComboDtlForSoPrint(dsSOInvoice.Tables("SalesOrderBulkComboDtl").Rows(0)("SaleOrderNumber").ToString())

                'For Each dr As DataRow In dsSOInvoice.Tables("SalesOrderBulkComboDtl").Select("Status=True", "ComboSrNo")
                '    Dim drResult As DataRow() = dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Select("BillLineNo='" + dr("ComboSrNo").ToString + "'")
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
                '    drSOPrintHeader("HSN") = objcomm.GetHSNbyArticle(dr("ArticleCode"))
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

                    'drSOPrintHeader("HSN") = objComn.GetHSNbyArticle(dr("ArticleCode"))    'vipin 27/07/2017 GST
                    dtOrderComboDetails.Rows.Add(drSOPrintHeader)
                    i = i + 1
                Next
            End If
        Catch ex As Exception

        End Try
        dtOrderComboDetails.Merge(dtOrderDetails)
        If dtOrderComboDetails.Rows.Count > 0 Then
            dtOrderComboDetails = dtOrderComboDetails.Select("", "BillLineNo").CopyToDataTable()
        End If
        dtOrderComboDetails.AcceptChanges()
    End Function
    Private Function SOPrintPaymentDetails1() As Boolean ' 
        Try
            'dsInv()
            Dim dsPackagingVar As DataSet = objPCSO.SetSalesOrderPackVariationInSO(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
            Dim TaxAmount As Double = FormatNumber(CDbl(dsPackagingVar.Tables("PackagingMaterial").Compute("Sum(TotalTaxAmount)", "")), 0) + Math.Round(IIf(dsSOInvoice.Tables("SalesOrderTaxDtls").Compute("SUM(TaxValue)", "IsDocumentLevel=1 and status=1") Is DBNull.Value, 0, dsSOInvoice.Tables("SalesOrderTaxDtls").Compute("SUM(taxValue)", "IsDocumentLevel=1 and status=1")), 2)
            Dim st As Double
            Dim crdsaleadjustamount As Double = 0
            Dim dtCreditSaleData As New DataTable
            Dim objclsReturn As New clsCashMemoReturn
            dtCreditSaleData = objclsReturn.getCreditSaleBillData(DocumentNo)
            If Not dtCreditSaleData Is Nothing AndAlso dtCreditSaleData.Rows.Count > 0 Then
                For Each y In dtCreditSaleData.Rows
                    st += y("CreditSaleAdjustment")
                Next
            End If
            Dim otherCharges As Double
            If dsSOInvoice.Tables("SalesOrderOtherCharges").Rows.Count() > 0 Then
                otherCharges = dsSOInvoice.Tables("SalesOrderOtherCharges").Compute("Sum(ChargeAmount)", "")
            End If
            dtPaymentDetails1 = objPCSO.GetSOPrintPaymenytDetails1TableStruc()
            dtPaymentDetails1.Rows.Clear()
            If dsSOInvoice.Tables("SalesOrderHdr").Rows.Count > 0 Then
                For Each dr As DataRow In dsSOInvoice.Tables("SalesOrderHdr").Select("Status=True")
                    drSOPrintHeader = dtPaymentDetails1.NewRow()
                    drSOPrintHeader("GrossAmt") = FormatNumber(dr("GrossAmt"), 0)
                    ' drSOPrintHeader("GrossAmt") = FormatNumber(drSOPrintHeader("GrossAmt"), 0)
                    drSOPrintHeader("Discount") = dr("TotalDiscount")
                    drSOPrintHeader("Tax") = FormatNumber(TaxAmount, 0)
                    drSOPrintHeader("OtherCharges") = otherCharges
                    drSOPrintHeader("NetAmt") = FormatNumber(dr("NetAmt"), 0)
                    'drSOPrintHeader("paidAmt") = dr("AdvanceAmt")

                    'drSOPrintHeader("BalAmt") = dr("NetAmt") - dr("AdvanceAmt")
                    drSOPrintHeader("paidAmt") = dr("AdvanceAmt") - IIf(dsInv.Tables(0).Compute("Sum(InvoiceAmt)", "Tendertype='Credit'") Is DBNull.Value, 0, dsInv.Tables(0).Compute("Sum(InvoiceAmt)", "Tendertype='Credit'")) ' + st
                    drSOPrintHeader("BalAmt") = dr("NetAmt") - dr("AdvanceAmt") + IIf(dsInv.Tables(0).Compute("Sum(InvoiceAmt)", "Tendertype='Credit'") Is DBNull.Value, 0, dsInv.Tables(0).Compute("Sum(InvoiceAmt)", "Tendertype='Credit'")) '- st
                    drSOPrintHeader("ReturnAmt") = IIf(dsInv.Tables(0).Compute("Sum(InvoiceAmt)", "Tendertype='Cash(R)'") Is DBNull.Value, 0, dsInv.Tables(0).Compute("Sum(InvoiceAmt)", "Tendertype='Cash(R)'"))
                    dtPaymentDetails1.Rows.Add(drSOPrintHeader)
                Next
            End If
        Catch ex As Exception

        End Try

    End Function
    Private Function SOPrintPaymentDetails() As Boolean ' 
        Try
            dtPaymentDetails = objPCSO.GetSOPrintPaymenytDetailsTableStruc()
            dtPaymentDetails.Rows.Clear()

            dsInv = objPCSO.SetInvoiceInSOCancel(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
            If dsInv.Tables(0).Rows.Count > 0 Then
                For Each dr As DataRow In dsInv.Tables(0).Rows
                    drSOPrintHeader = dtPaymentDetails.NewRow()
                    drSOPrintHeader("Shift") = clsCommon.GetShiftNameForPrint(dr("Shift").ToString.Trim(), clsAdmin.SiteCode) 'vipin
                    drSOPrintHeader("Till") = dr("TerminalID")
                    '' dr("UserName") Updated by ketan CashierName 
                    drSOPrintHeader("CashierName") = dr("UserName") 'IIf(dsSOInvoice.Tables("SalesOrderHdr").Rows(0)("SalesExecutiveCode") Is DBNull.Value, clsAdmin.UserName, dsSOInvoice.Tables("SalesOrderHdr").Rows(0)("SalesExecutiveCode"))
                    drSOPrintHeader("InvoiceNo") = dr("InvoiceNo")
                    drSOPrintHeader("PymentDateAndTime") = dr("InvoiceDate")
                    drSOPrintHeader("Tender") = dr("TenderType")
                    drSOPrintHeader("Amt") = dr("InvoiceAmt")
                    dtPaymentDetails.Rows.Add(drSOPrintHeader)
                Next
            End If
        Catch ex As Exception

        End Try

    End Function
    Dim dtOrderReturntable As DataTable
    Dim dtReturnOrderComboDtl As DataTable
    Private Function SOPrintReturnsOrderDetails() As Boolean ' 
        Try
            '' single return data 
            Dim a = dsPackagingDelivery
            dtOrderReturntable = objPCSO.GetSOPrintOrderDetailsTableStruc()
            dtOrderReturntable.Rows.Clear()
            Dim i As Integer = 1
            If dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Rows.Count > 0 Then
                For Each dr As DataRow In dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Select("Status=True", "BillLineNo")
                    If IIf(dr("ReturnQty") Is System.DBNull.Value, 0, dr("ReturnQty")) > 0 Then

                        Dim drResult As DataRow() = dsSOInvoice.Tables("SOitemPackagingBoxDtl").Select("pkgLineNo='" + dr("pkgLineNo").ToString + "' ")
                        Dim drpickupQty As DataRow() = (dsPackagingDelivery.Tables(0)).Select("DeliverylineNo='" + dr("DeliverylineNo").ToString + "'")
                        drSOPrintHeader = dtOrderReturntable.NewRow()
                        drSOPrintHeader("BillLineNo") = dr("BillLineNo")
                        Dim drSrNo As DataRow() = dtOrderReturntable.Select("BillLineNo='" + dr("BillLineNo").ToString + "'")
                        If drSrNo.Length = 0 Then
                            drSOPrintHeader("SrNo") = i
                        Else
                            i = i - 1
                            drSOPrintHeader("SrNo") = i & "." & drSrNo.Length
                        End If
                        Dim discription = dsSOInvoice.Tables("SalesOrderBulkComboHdr").Select("ComboSrNo='" & dr("BillLineNo") & "'")
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
                        '' return QTY
                        drSOPrintHeader("PickupQuantity") = IIf(dr("ReturnQty") Is System.DBNull.Value, 0, (-1) * dr("ReturnQty")) ''Return Qty
                        drSOPrintHeader("IsCombo") = dr("IsCombo")
                        drSOPrintHeader("header") = 1
                        drSOPrintHeader("PackagingMaterial") = drResult(0)("pckgMaterial")
                        dtOrderReturntable.Rows.Add(drSOPrintHeader)
                        i = i + 1
                    End If
                Next
            End If

        Catch ex As Exception

        End Try

    End Function
    Dim dtReturnOrderComboDetails As DataTable
    Private Function SOPrintReturnOrderComboDetails() As Boolean ' 
        Try
            '' Combo return data 
            dtReturnOrderComboDtl = objPCSO.GetSOPrintOrderDetailsTableStruc()
            dtReturnOrderComboDtl.Rows.Clear()
            Dim i As Long = 1
            If dsSOInvoice.Tables("SalesOrderBulkComboDtl").Rows.Count > 0 Then
                For Each dr As DataRow In dsSOInvoice.Tables("SalesOrderBulkComboDtl").Select("Status=True", "ComboSrNo")
                    Dim drResult As DataRow() = dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Select("BillLineNo='" + dr("ComboSrNo").ToString + "'")
                    drSOPrintHeader = dtReturnOrderComboDtl.NewRow()
                    drSOPrintHeader("BillLineNo") = dr("ComboSrNo")
                    Dim drSrNo As DataRow() = dtReturnOrderComboDtl.Select("BillLineNo='" + dr("ComboSrNo").ToString + "'")
                    If drSrNo.Length = 0 Then
                        i = 1
                        drSOPrintHeader("SrNo") = Chr(i + 96)
                    Else
                        'i = i - 1
                        drSOPrintHeader("SrNo") = Chr(i + 96)
                    End If
                    drSOPrintHeader("ArticleDescription") = dr("ArticleDescription")
                    drSOPrintHeader("Quantity") = dr("QTy")
                    drSOPrintHeader("UnitofMeasure") = dr("PackagedUOM")
                    drSOPrintHeader("Price") = 0
                    drSOPrintHeader("Total") = 0
                    drSOPrintHeader("PckgSize") = 0
                    drSOPrintHeader("PckgQty") = 0
                    drSOPrintHeader("PckgType") = 0
                    '' return QTY
                    drSOPrintHeader("PickupQuantity") = IIf(dr("ReturnQty") Is System.DBNull.Value, 0, (-1) * dr("ReturnQty")) ''Return Qty
                    drSOPrintHeader("IsCombo") = 1
                    drSOPrintHeader("header") = 0
                    drSOPrintHeader("HSN") = objcomm.GetHSNbyArticle(dr("ArticleCode"))
                    dtReturnOrderComboDtl.Rows.Add(drSOPrintHeader)
                    i = i + 1
                Next
            End If
        Catch ex As Exception

        End Try
        dtReturnOrderComboDtl.Merge(dtOrderReturntable)
        If dtReturnOrderComboDtl.Rows.Count > 0 Then
            dtReturnOrderComboDtl = dtReturnOrderComboDtl.Select("", "BillLineNo").CopyToDataTable()
        End If
        dtReturnOrderComboDtl.AcceptChanges()
    End Function

    Private Function SOPrintDeliveryDetails() As Boolean ' 
        Try

            dtDeliveryDetails = objPCSO.GetSOPrintDeliveryDetailsTableStruc()
            dtDeliveryDetails.Rows.Clear()
            Dim i As Long = 1
            If dsSOInvoice.Tables("SOItemPackagingBoxDtl").Rows.Count > 0 Then

                For Each dr As DataRow In dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Select("Status =True", "BillLineNo")
                    'Dim drResult As DataRow() = dsMain.Tables("SOPackagingBoxDeliveryDtl").Select("pkglineNo='" + dr("pkglineNo").ToString + "'")
                    drSOPrintHeader = dtDeliveryDetails.NewRow()
                    Dim discription = dsSOInvoice.Tables("SalesOrderBulkComboHdr").Select("ComboSrNo='" & dr("BillLineNo") & "'")
                    If discription.Length > 0 Then
                        drSOPrintHeader("Orders") = discription(0)("PackagingBoxName")
                    Else
                        drSOPrintHeader("Orders") = clsCommon.GetArticleFullName(dr("ArticleCode")).ToString
                    End If
                    drSOPrintHeader("Orderqty") = dr("Quantity")
                    Dim drResult As DataRow() = dsSOInvoice.Tables("SOItemPackagingBoxDtl").Select("pkglineNo='" + dr("pkglineNo").ToString + "'")
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
                    Dim drAddress As DataRow() = dtOrderAddresses.Select("AddressKey='" & dr("DeliveryAddress") & "'")
                    If drAddress.Length = 0 Then
                        drSOPrintHeader("DeliveryAddress") = "-"
                    Else
                        drSOPrintHeader("DeliveryAddress") = drAddress(0)("AddressValue")
                    End If
                    drSOPrintHeader("DeliveryDate") = dr("DeliveryDate")
                    drSOPrintHeader("DeliveryTime") = dr("DeliveryTime")
                    drSOPrintHeader("pickupQty") = dr("DeliveredQty")
                    drSOPrintHeader("PendingQty") = dr("Quantity") - dr("DeliveredQty")
                    dtDeliveryDetails.Rows.Add(drSOPrintHeader)
                    i = i + 1
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Private Function SOStrDetails() As Boolean '
        Try
            dtStrDetails = objPCSO.GetStrDetailsTableStruc()
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
                    drSOPrintHeader("STRDeliveryTime") = dr("StrTime").ToString().Substring(0, dr("StrTime").ToString().Length - 1)
                    drSOPrintHeader("CREATEDON") = dr("GeneratedOn")
                    drSOPrintHeader("CREATEDBY") = dr("GeneratedBy")
                    dtStrDetails.Rows.Add(drSOPrintHeader)
                Next
            End If

        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Private Function GetSOPrintAddress() As Boolean ' 
        Try
            dtAddress = objPCSO.GetSOPrintAddressTableStruc()
            dtAddress.Rows.Clear()
            If dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Rows.Count > 0 Then
                For Each dr As DataRow In dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Select("Status=True", "BillLineNo")
                    Dim drAddress As DataRow() = dtOrderAddresses.Select("AddressKey='" & dr("DeliveryAddress") & "'")
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

    Private Function SOPrintRemarks() As Boolean
        Try
            If dsMain.Tables("SalesOrderdtl").Rows.Count > 0 Then
                dtRemark = objPCSO.GetSOPrintRemarksTableStruc()
                dtRemark.Rows.Clear()
                For Each dr As DataRow In dsSOInvoice.Tables("SalesOrderdtl").Select("Status=True", "BillLineNo")
                    drSOPrintHeader = dtRemark.NewRow()
                    Dim discription = dsSOInvoice.Tables("SalesOrderBulkComboHdr").Select("ComboSrNo='" & dr("BillLineNo") & "'")
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

    Private Function SOPrintPickupHistory() As Boolean
        Try
            dtPickupHistory = objPCSO.GetSOPrintPickupHistoryTableStruc()
            dtPickupHistory.Rows.Clear()
            If Not _pickupHistory Is Nothing AndAlso _pickupHistory.Tables("PickupHistory").Rows.Count > 0 Then
                For Each dr As DataRow In _pickupHistory.Tables("PickupHistory").Rows
                    drSOPrintHeader = dtPickupHistory.NewRow()
                    Dim drResult As DataRow() = dsSOInvoice.Tables("SOitemPackagingBoxDtl").Select("pkgLineNo='" + dr("PkgLineNo").ToString + "'")
                    Dim discription = dsSOInvoice.Tables("SalesOrderBulkComboHdr").Select("ComboSrNo='" & dr("BillLineNo") & "'")
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

            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            Dim obj As New clsCommon
            'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
            'path = clsDefaultConfiguration.DayCloseReportPath & "\SODeliveryNote_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm") & ".pdf"
            Dim Newpath As String = ""
            Newpath = CreatePathForPrint(dtHeaderDetails, "SODeliveryNote", False, False)
            path = Newpath
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            If clsDefaultConfiguration.SOPrintPreviewAllowed = True Then
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

            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            Dim obj As New clsCommon
            'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
            Dim Newpath As String = ""
            If consolidated = True Then
                Newpath = CreatePathForPrint(dtHeaderDetails, "Consolidated-Order_prep", consolidated, True)
            Else
                Newpath = CreatePathForPrint(dtHeaderDetails, "Order_prep" & PrintID & "", consolidated, True)
            End If
            path = Newpath
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            If clsDefaultConfiguration.SOPrintPreviewAllowed = True Then
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
    Private Function CreatePathForPrint(ByVal dtHeaderDetails As DataTable, Optional ByVal PrintName As String = "", Optional ByVal consolidated As Boolean = False, Optional ByVal PrintFormOrderPre As Boolean = True) As String
        Try
            Dim PathSaved As String
            Dim SalesOrderNo As String
            Dim CustName As String
            Dim CustomerCompanyName As String
            Dim DeliveryTime As String
            If dtHeaderDetails.Rows.Count > 0 Then
                SalesOrderNo = dtHeaderDetails.Rows(0)("SalesOrderNo")
                CustName = dtHeaderDetails.Rows(0)("CustomerName")
                CustomerCompanyName = dtHeaderDetails.Rows(0)("CustomerCompanyName")
                CustomerCompanyName = CustomerCompanyName.Replace(".", "")
            End If
            If Not NEWdtDeliveryDetails Is Nothing AndAlso NEWdtDeliveryDetails.Rows.Count > 0 Then
                DeliveryTime = Convert.ToDateTime(NEWdtDeliveryDetails(0)("DeliveryTime")).ToString("dd-MM-yyyy_HH-mm")
            End If

            Dim SOPath As String = clsDefaultConfiguration.DayCloseReportPath & "\Sales Order\" & SalesOrderNo & ""
            'SOPath = "D" + SOPath.Substring(1)
            If Not Directory.Exists(SOPath) Then
                Directory.CreateDirectory(SOPath)
            End If

            If consolidated = True Then
                PathSaved = SOPath & "\" & CustName & "-" & CustomerCompanyName & "-" & SalesOrderNo.Substring(9, SalesOrderNo.Length - 9) & "-" & PrintName & "-" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
            Else
                If PrintFormOrderPre = True Then
                    PathSaved = SOPath & "\" & CustName & "-" & CustomerCompanyName & "-" & SalesOrderNo.Substring(9, SalesOrderNo.Length - 9) & "-DD-" & DeliveryTime & "-" & PrintName & "-" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                Else
                    PathSaved = SOPath & "\" & PrintName & "-" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                End If
            End If
            Return PathSaved
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
            ' Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\SalesOrderPrint.rdl")
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

            'DtshiftID = clsCommon.GetShiftName(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID)

            'dtPaymentDetails.Columns.Add("Shift", GetType(String))
            'If Not DtshiftID Is Nothing AndAlso DtshiftID.Rows.Count > 0 Then
            '    For Each dtPaymentDetails12 In dtPaymentDetails.Rows
            '        dtPaymentDetails12("Shift") = DtshiftID.Rows(0)(0).ToString.Trim
            '    Next
            'End If

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
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            Dim obj As New clsCommon
            'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
            'path = clsDefaultConfiguration.DayCloseReportPath & "\SOInvoice_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm") & ".pdf"
            Dim Newpath As String = ""
            Newpath = CreatePathForPrint(dtHeaderDetails, "SOInvoice", False, False)
            path = Newpath
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            If clsDefaultConfiguration.SOPrintPreviewAllowed = True Then
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

    Private Sub frmPCSalesOrderHistory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "525-order-history.htm")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Function Themechange() As String

        CtrlCashSummary1.BackColor = Color.White
        Me.BackColor = Color.FromArgb(134, 134, 134)

        Me.lblCustomerName.ForeColor = Color.White
        Me.lblCustomerName.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        Me.lblCustomerNameValue.ForeColor = Color.White
        Me.lblCustomerNameValue.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        Me.lblCompanyName.ForeColor = Color.White
        Me.lblCompanyName.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        Me.lblCompanyNameValue.ForeColor = Color.White
        Me.lblCompanyNameValue.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        Me.lblDepartmentName.ForeColor = Color.White
        Me.lblDepartmentName.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        Me.lblDepartmentNameValue.ForeColor = Color.White
        Me.lblDepartmentNameValue.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        Me.lblMobileNo.ForeColor = Color.White
        Me.lblMobileNo.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        Me.lblMobileNoValue.ForeColor = Color.White
        Me.lblMobileNoValue.Font = New Font("Neo Sans", 9, FontStyle.Bold)


        Me.lblSONo.BackColor = Color.Transparent
        lblSONo.ForeColor = Color.White
        Me.lblSONo.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblSONo.BorderStyle = BorderStyle.None

        Me.lblSONoValue.BackColor = Color.Transparent
        Me.lblSONoValue.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblSONoValue.BorderStyle = BorderStyle.None
        lblSONoValue.ForeColor = Color.White

        Me.lblStoreName.BackColor = Color.Transparent
        lblStoreName.ForeColor = Color.White
        Me.lblStoreName.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblStoreName.BorderStyle = BorderStyle.None

        lblStoreNameValue.ForeColor = Color.White
        Me.lblStoreNameValue.BackColor = Color.Transparent
        Me.lblStoreNameValue.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblStoreNameValue.BorderStyle = BorderStyle.None

        lblBooking.ForeColor = Color.White
        Me.lblBooking.BackColor = Color.Transparent
        Me.lblBooking.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblBooking.BorderStyle = BorderStyle.None

        lblBookingValue.ForeColor = Color.White
        Me.lblBookingValue.BackColor = Color.Transparent
        Me.lblBookingValue.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblBookingValue.BorderStyle = BorderStyle.None
        '=================================================
        lblSONoOrder.ForeColor = Color.White
        Me.lblSONoOrder.BackColor = Color.Transparent
        Me.lblSONoOrder.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblSONoOrder.BorderStyle = BorderStyle.None

        lblSONoOrderValue.ForeColor = Color.White
        Me.lblSONoOrderValue.BackColor = Color.Transparent
        Me.lblSONoOrderValue.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblSONoOrderValue.BorderStyle = BorderStyle.None

        lblStoreNameOrder.ForeColor = Color.White
        Me.lblStoreNameOrder.BackColor = Color.Transparent
        Me.lblStoreNameOrder.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblStoreNameOrder.BorderStyle = BorderStyle.None

        lblStoreNameOrderValue.ForeColor = Color.White
        Me.lblStoreNameOrderValue.BackColor = Color.Transparent
        Me.lblStoreNameOrderValue.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblStoreNameOrderValue.BorderStyle = BorderStyle.None

        lblBookingOrder.ForeColor = Color.White
        Me.lblBookingOrder.BackColor = Color.Transparent
        Me.lblBookingOrder.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblBookingOrder.BorderStyle = BorderStyle.None

        lblBookingValueOrder.ForeColor = Color.White
        Me.lblBookingValueOrder.BackColor = Color.Transparent
        Me.lblBookingValueOrder.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblBookingValueOrder.BorderStyle = BorderStyle.None
        '======================================================
        lblSONoPickup.ForeColor = Color.White
        Me.lblSONoPickup.BackColor = Color.Transparent
        Me.lblSONoPickup.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblSONoPickup.BorderStyle = BorderStyle.None

        lblSONoValuePickup.ForeColor = Color.White
        Me.lblSONoValuePickup.BackColor = Color.Transparent
        Me.lblSONoValuePickup.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblSONoValuePickup.BorderStyle = BorderStyle.None

        lblStoreNamePickup.ForeColor = Color.White
        Me.lblStoreNamePickup.BackColor = Color.Transparent
        Me.lblStoreNamePickup.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblStoreNamePickup.BorderStyle = BorderStyle.None

        lblStoreNameValuePickup.ForeColor = Color.White
        Me.lblStoreNameValuePickup.BackColor = Color.Transparent
        Me.lblStoreNameValuePickup.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblStoreNameValuePickup.BorderStyle = BorderStyle.None

        lblBookingPickup.ForeColor = Color.White
        Me.lblBookingPickup.BackColor = Color.Transparent
        Me.lblBookingPickup.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblBookingPickup.BorderStyle = BorderStyle.None

        lblBookingValuePickup.ForeColor = Color.White
        Me.lblBookingValuePickup.BackColor = Color.Transparent
        Me.lblBookingValuePickup.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblBookingValuePickup.BorderStyle = BorderStyle.None
        '================================================
        lblSoNoPayment.ForeColor = Color.White
        Me.lblSoNoPayment.BackColor = Color.Transparent
        Me.lblSoNoPayment.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblSoNoPayment.BorderStyle = BorderStyle.None

        lblSoNoValuePayment.ForeColor = Color.White
        Me.lblSoNoValuePayment.BackColor = Color.Transparent
        Me.lblSoNoValuePayment.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblSoNoValuePayment.BorderStyle = BorderStyle.None

        lblStoreNamePayment.ForeColor = Color.White
        Me.lblStoreNamePayment.BackColor = Color.Transparent
        Me.lblStoreNamePayment.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblStoreNamePayment.BorderStyle = BorderStyle.None

        lblStoreNameValuePayment.ForeColor = Color.White
        Me.lblStoreNameValuePayment.BackColor = Color.Transparent
        Me.lblStoreNameValuePayment.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblStoreNameValuePayment.BorderStyle = BorderStyle.None

        lblBookingPayment.ForeColor = Color.White
        Me.lblBookingPayment.BackColor = Color.Transparent
        Me.lblBookingPayment.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblBookingPayment.BorderStyle = BorderStyle.None

        lblBookingValuePayment.ForeColor = Color.White
        Me.lblBookingValuePayment.BackColor = Color.Transparent
        Me.lblBookingValuePayment.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        Me.lblBookingValuePayment.BorderStyle = BorderStyle.None

        Me.lblBalancePoints.BackColor = Color.Transparent
        lblBalancePoints.ForeColor = Color.White
        Me.lblBalancePointsValue.BackColor = Color.Transparent
        lblBalancePointsValue.ForeColor = Color.White
        'grdSOInfo
        '
        grdSOInfo.Styles(0).BackColor = Color.FromArgb(255, 255, 255)
        grdSOInfo.Styles(0).Font = New Font("Neo Sans", 10, FontStyle.Regular)
        grdSOInfo.Styles(1).Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSOInfo.Styles(1).BackColor = Color.FromArgb(177, 227, 253)
        grdSOInfo.Styles(1).ForeColor = Color.Black
        grdSOInfo.Splits(0).Style.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        grdSOInfo.Styles(5).Font = New Font("Neo Sans", 9, FontStyle.Bold)
        grdSOInfo.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Custom
        grdSOInfo.HighLightRowStyle.BackColor = Color.LightBlue
        grdSOInfo.HighLightRowStyle.ForeColor = Color.Black


        Me.btnPrint.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnPrint.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnPrint.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'Me.btnPrint.MaximumSize = New Size(0, 30)
        'Me.btnPrint.MinimumSize = New Size(0, 30)
        Me.btnPrint.Size = New System.Drawing.Size(75, 26)
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnPrint.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnPrint.FlatStyle = FlatStyle.Flat
        Me.btnPrint.FlatAppearance.BorderSize = 0
        Me.btnPrint.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        'btnSoPrint
        '
        Me.btnSoPrint.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnSoPrint.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnSoPrint.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'Me.btnSoPrint.MaximumSize = New Size(0, 30)
        'Me.btnSoPrint.MinimumSize = New Size(0, 30)
        Me.btnSoPrint.Size = New System.Drawing.Size(75, 26)
        Me.btnSoPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSoPrint.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnSoPrint.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnSoPrint.FlatStyle = FlatStyle.Flat
        Me.btnSoPrint.FlatAppearance.BorderSize = 0
        Me.btnSoPrint.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)


        'TabPageItemDetails
        '
        TabPageItemDetails.BackColor = Color.FromArgb(79, 79, 79)
        TabPageItemDetails.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        TabPageItemDetails.TabForeColorSelected = Color.White
        'tabSalesOrder
        '
        TabPageOrderDetails.BackColor = Color.FromArgb(79, 79, 79)
        TabPageOrderDetails.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        TabPageOrderDetails.TabForeColorSelected = Color.White

        'TabPagePickUpDetails
        '
        TabPagePickUpDetails.BackColor = Color.FromArgb(79, 79, 79)
        TabPagePickUpDetails.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        TabPagePickUpDetails.TabForeColorSelected = Color.White
        'TabPagePaymentInfo
        '
        TabPagePaymentInfo.BackColor = Color.FromArgb(79, 79, 79)
        TabPagePaymentInfo.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        TabPagePaymentInfo.TabForeColorSelected = Color.White

        'grdItemDetails
        '
        'Me.grdItemDetails.MaximumSize = New Size(1364, 600)
        'Me.grdItemDetails.Size = New System.Drawing.Size(1364, 600)
        Me.grdItemDetails.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        Me.grdItemDetails.Styles.Highlight.BackColor = Color.FromArgb(153, 255, 255)
        Me.grdItemDetails.Styles.Highlight.ForeColor = Color.Black
        Me.grdItemDetails.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdItemDetails.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        Me.grdItemDetails.Rows.MinSize = 26
        Me.grdItemDetails.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        Me.grdItemDetails.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdItemDetails.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdItemDetails.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdItemDetails.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdItemDetails.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdItemDetails.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        Me.grdItemDetails.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdItemDetails.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdItemDetails.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdItemDetails.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)

        'grdOrderDetails
        '
        Me.grdOrderDetails.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        Me.grdOrderDetails.Styles.Highlight.BackColor = Color.FromArgb(153, 255, 255)
        Me.grdOrderDetails.Styles.Highlight.ForeColor = Color.Black
        Me.grdOrderDetails.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdOrderDetails.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        Me.grdOrderDetails.Rows.MinSize = 26
        Me.grdOrderDetails.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        Me.grdOrderDetails.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdOrderDetails.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdOrderDetails.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdOrderDetails.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdOrderDetails.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdOrderDetails.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        Me.grdOrderDetails.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdOrderDetails.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdOrderDetails.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdOrderDetails.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)

        'grdPickUpDetails
        '
        Me.grdPickUpDetails.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        Me.grdPickUpDetails.Styles.Highlight.BackColor = Color.FromArgb(153, 255, 255)
        Me.grdPickUpDetails.Styles.Highlight.ForeColor = Color.Black
        Me.grdPickUpDetails.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdPickUpDetails.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        Me.grdPickUpDetails.Rows.MinSize = 26
        Me.grdPickUpDetails.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        Me.grdPickUpDetails.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPickUpDetails.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPickUpDetails.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPickUpDetails.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPickUpDetails.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdPickUpDetails.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        Me.grdPickUpDetails.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPickUpDetails.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPickUpDetails.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdPickUpDetails.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)

        'grdPaymentInfo
        '
        Me.grdPaymentInfo.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        Me.grdPaymentInfo.Styles.Highlight.BackColor = Color.FromArgb(153, 255, 255)
        Me.grdPaymentInfo.Styles.Highlight.ForeColor = Color.Black
        Me.grdPaymentInfo.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdPaymentInfo.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        Me.grdPaymentInfo.Rows.MinSize = 26
        Me.grdPaymentInfo.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        Me.grdPaymentInfo.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPaymentInfo.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPaymentInfo.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPaymentInfo.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPaymentInfo.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdPaymentInfo.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        Me.grdPaymentInfo.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPaymentInfo.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPaymentInfo.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdPaymentInfo.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)

        'CtrlCashSummary1
        '

        'Me.CtrlCashSummary1.BackColor=color.
        CtrlCashSummary1.Height = 221
        CtrlCashSummary1.CtrlLabel1.BackColor = Color.FromArgb(212, 212, 212)
        CtrlCashSummary1.CtrlLabel2.BackColor = Color.FromArgb(212, 212, 212)
        CtrlCashSummary1.CtrlLabel3.BackColor = Color.FromArgb(212, 212, 212)
        CtrlCashSummary1.CtrlLabel4.BackColor = Color.FromArgb(212, 212, 212)
        CtrlCashSummary1.CtrlLabel5.BackColor = Color.FromArgb(212, 212, 212)
        CtrlCashSummary1.CtrlLabel6.BackColor = Color.FromArgb(212, 212, 212)
        CtrlCashSummary1.CtrlLabel7.BackColor = Color.FromArgb(212, 212, 212)
        Return ""
    End Function
#End Region

End Class
