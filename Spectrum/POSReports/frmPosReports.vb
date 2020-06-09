﻿Imports SpectrumCommon
Imports SpectrumBL
Imports System.Globalization
Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports SpectrumPrint
Imports Spire.Pdf
Public Class frmPosReports
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim objReportBase As New ReportBase
        Dim reportsDt As DataTable = objReportBase.GetAllValidReports(clsAdmin.SiteCode)
        If reportsDt Is Nothing OrElse reportsDt.Rows.Count <= 0 Then
            ShowMessage(getValueByKey("reportvalidationmsg"), getValueByKey("CLAE05"))
            Me.Close()
            Exit Sub
        End If
        For Each dr In reportsDt.Rows
            ReportSource.Add(dr("ReportName"))
        Next

        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
    End Sub
    Private ReportSource As New List(Of String)
    Dim path As String = ""
    Public _paraMonth As String
    Public _paraYear As String
    Public _PromotionIds As String
    Public SelectedClass As String = ""
    Dim obNetSale As New frmNHierarchyWiseNetSales
    Private Sub frmPosReports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
        End If
        cmbReports.DataSource = ReportSource
        cmbReports.SelectedIndex = 0
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim obj As New frmReportFilter
            obj.dtToDate.Value = DateTime.Now
            obj.dtFromDate.Value = DateTime.Now
            obj.dtExpiryDate.Value = DateTime.Now
            Dim objDefault As New clsDefaultConfiguration("DC")
            objDefault.GetDefaultSettings()
            Dim Client As String = clsDefaultConfiguration.ClientForMail
            If cmbReports.SelectedItem = POSReports.KOTReport.ToString() OrElse cmbReports.SelectedItem = POSReports.BillWiseReport.ToString() OrElse _
                cmbReports.SelectedItem = POSReports.CashierWiseSalesReport.ToString() OrElse cmbReports.SelectedItem = POSReports.TaxDetailsReport.ToString() _
                OrElse cmbReports.SelectedItem = POSReports.PostingReport.ToString() OrElse cmbReports.SelectedItem = POSReports.ProductMixReport.ToString() _
                OrElse cmbReports.SelectedItem = POSReports.FoodCostReport.ToString() Then
                obj.dtFromDate.Visible = True
                obj.lblFromDate.Visible = True
            ElseIf cmbReports.SelectedItem = POSReports.GiftVoucherReport.ToString() Then
                obj.dtFromDate.Visible = True
                obj.lblFromDate.Visible = True
                obj.lblExpryDate.Visible = True
                obj.dtExpiryDate.Visible = True
            ElseIf cmbReports.SelectedItem = POSReports.TimeScheduledSaleReport.ToString() Then
                obj.dtFromDate.Visible = True
                obj.lblFromDate.Visible = True
                obj.lblTimeSpan.Visible = True
                obj.txtTimeSpan.Visible = True
            ElseIf cmbReports.SelectedItem = POSReports.HcCustomerDetailsReport.ToString() Then
                obj.pnlCustomerClass.Visible = True
                obj.lblSelectClass.Visible = True
                obj.dtFromDate.Visible = True
                obj.lblFromDate.Visible = True
                obj.pnlPromotions.Visible = False
            ElseIf cmbReports.SelectedItem = POSReports.XReadCategorywiseReport.ToString() Then
                obj.dtFromDate.Visible = False
                obj.lblFromDate.Visible = False
                obj.lblTimeSpan.Visible = False
                obj.txtTimeSpan.Visible = False
            ElseIf cmbReports.SelectedItem = POSReports.TallySalesReport.ToString() Then 'vipin
                obj.dtFromDate.Visible = True
                obj.lblFromDate.Visible = True
                obj.dtToDate.Visible = True
                obj.lblToDate.Visible = True
                obj.lblTimeSpan.Visible = False
                obj.txtTimeSpan.Visible = False
            ElseIf cmbReports.SelectedItem = POSReports.HierarchyWiseSalesReport.ToString() Then  '' added by vipin PC SO
                obNetSale.IsNetSale = False
                obNetSale.ShowDialog()
                Me.Cursor = Cursors.Default
                Exit Sub
            ElseIf cmbReports.SelectedItem = POSReports.HierarchyWiseNetSalesDetailsReport.ToString() Then  '' added by vipin PC SO
                obNetSale.IsNetSale = True
                obNetSale.ShowDialog()
                Me.Cursor = Cursors.Default
                Exit Sub
            ElseIf cmbReports.SelectedItem = "X-Read" OrElse cmbReports.SelectedItem = "XReadFlavourwiseReport" OrElse cmbReports.SelectedItem = "XReadCatReport" Then
                obj.dtFromDate.Visible = True
                obj.lblFromDate.Visible = True
            End If
            obj.selectedReport = cmbReports.SelectedItem
            obj.ShowDialog()
            _paraMonth = obj.selectedmonth
            _paraYear = obj.selectedyear
            _PromotionIds = obj.SelectedPromotions
            SelectedClass = obj.SelectedClassify
            If Not IsDBNull(obj.dtToDate.Value) AndAlso obj.dtToDate.Value IsNot Nothing AndAlso Not IsDBNull(obj.dtFromDate.Value) AndAlso obj.dtFromDate.Value IsNot Nothing Then
                Dim adsrReportFileName As String = String.Empty
                Dim objReportBase As New ReportBase
                Dim adsrProcName As String = objReportBase.GetAdsrProcedureName(clsAdmin.SiteCode, adsrReportFileName)

                Dim objReport As IReports = ReportFactory.Instance.GetReportInstance(cmbReports.SelectedItem)
                Dim request As New DayCloseReportModel
                request.ToDate = DirectCast(obj.dtToDate.Value, Date).Date
                request.FromDate = DirectCast(obj.dtFromDate.Value, Date).Date
                request.ExpiryDate = DirectCast(obj.dtExpiryDate.Value, Date).Date
                request.TimeSpan = IIf(obj.txtTimeSpan.Text = "" OrElse obj.txtTimeSpan.Text = String.Empty, 1, obj.txtTimeSpan.Text)
                request.SiteCode = clsAdmin.SiteCode
                request.CreatedBy = clsAdmin.UserName
                request.CreatedOn = DateTime.Now

                request.AdsrReportProcedureName = adsrProcName
                request.AdsrReportFileName = adsrReportFileName

                Dim objClient As New clsCommon
                Dim clientname As String = objClient.GetFLdValue(clsAdmin.SiteCode)

                If cmbReports.SelectedItem = "DayCloseReport" Then
                    If clientname = "JK" Then
                        Call GenerateDSRReport(request)
                    ElseIf clientname = "PC" Then
                        Call GenerateDayCloseReport(request)
                    Else
                        objReport.GenerateDayCloseReport(request, clsDefaultConfiguration.DayCloseReportPath)
                    End If
                ElseIf Client = "JK" And (cmbReports.SelectedItem = "KOTReport" Or cmbReports.SelectedItem = "ProductMixReport") Then
                    Dim objKOT As New kotprint
                    objKOT.GenerateKOT(request, clsDefaultConfiguration.DayCloseReportPath, dtPrinterInfo, cmbReports.SelectedItem)
                ElseIf cmbReports.SelectedItem = "X-Read" Then
                    Call genrateXread(request)
                ElseIf cmbReports.SelectedItem = "XReadCategorywiseReport" Then
                    Call genrateXreadCategoryWise(request)
                ElseIf cmbReports.SelectedItem = "XReadCatReport" Then
                    Call genrateXreadcat(request)
                ElseIf cmbReports.SelectedItem = "XReadFlavourwiseReport" Then
                    Call genrateXreadFlavourWise(request)
                ElseIf cmbReports.SelectedItem = "FoodCostReport" Then
                    Call GenerateFoodCost(request)
                ElseIf cmbReports.SelectedItem = "TargetVsActualSales" Then
                    Call TargetVsAcutalSalesReport(request)
                ElseIf cmbReports.SelectedItem = "JKOftheDayPayoutReport" Then
                    Call JKDayOfReportPayout(request, _PromotionIds)
                ElseIf cmbReports.SelectedItem = "JKProductMixReport" Then
                    Call JKProductMixReport(request, clsAdmin.UserName)
                ElseIf cmbReports.SelectedItem = "HcCustomerDetailsReport" Then
                    Call HcCustomerDetailReport(request, SelectedClass)
                ElseIf cmbReports.SelectedItem = "SalesReconciliationReport" Then
                    Call GenerateJKSalesReconciliation(request)
                ElseIf cmbReports.SelectedItem = "TallySalesReport" Then
                    Call GenerateTallySalesReport(request)
                Else
                    objReport.GenerateDayCloseReport(request, clsDefaultConfiguration.DayCloseReportPath)
                End If
                'If Not objReport.GenerateDayCloseReport(request, clsDefaultConfiguration.DayCloseReportPath) Then
                '    'ShowMessage("Unable to generating report", getValueByKey("CLAE05"))
                'End If
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowMessage("Error in generating report", getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' target v/s actual sales report
    ''' </summary>
    ''' <param name="request"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TargetVsAcutalSalesReport(ByVal request As SpectrumCommon.DayCloseReportModel)
        Try
            Dim path As String = ""
            Dim dsTargetVsActualSales As DataSet
            Dim clsObj As New clsTargetVsActualSales
            dsTargetVsActualSales = clsObj.GetTargetVsActualSalesReportData(request.SiteCode.ToString(), _paraMonth, _paraYear)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\TargetSalesVsActualSalesReport.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            Dim MonthParam As New ReportParameter("Month", _paraMonth)
            Dim YearParam As New ReportParameter("Year", _paraYear)
            Dim SiteCodeParam As New ReportParameter("SiteCode", request.SiteCode)

            reportViewer2.LocalReport.SetParameters(New ReportParameter() {SiteCodeParam, MonthParam, YearParam})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource As New ReportDataSource("dsTargetVsActualSalesHeader", dsTargetVsActualSales.Tables(0))
            Dim DataSource1 As New ReportDataSource("dsTargetVsActualSalesReportData", dsTargetVsActualSales.Tables(1))
            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            path = clsDefaultConfiguration.DayCloseReportPath & "\TargetSalesVsActualSalesReport_" & request.ToDate.ToString("dd-MM-yyyy") & ".pdf"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using

            Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'JKDayOfReport
    Public Function JKDayOfReportPayout(ByVal request As SpectrumCommon.DayCloseReportModel, ByVal PromotionId As String)
        Try
            Dim path As String = ""
            Dim dsJkDayOfReport As DataSet
            Dim clsObj As New clsJKDayOfReportPayout
            Dim objcls As New clsCommon
            Dim payout = objcls.GetPayoutValue(clsAdmin.SiteCode)
            Dim reason As String = "ALL"
            dsJkDayOfReport = clsObj.GetJKDayOfReportPayout(request.SiteCode.ToString(), request.FromDate, request.ToDate, PromotionId, payout, reason)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\JKDayOfReportPayout.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            Dim FromDateParam As New ReportParameter("FROM_DATE", request.FromDate)
            Dim ToDateParam As New ReportParameter("TO_DATE", request.ToDate)
            Dim SiteCodeParam As New ReportParameter("sitecode", request.SiteCode)
            Dim PromotionsIdParam As New ReportParameter("OFFER_NO", PromotionId)
            Dim payoutvalueParam As New ReportParameter("VALUE", payout)
            Dim reasonParam As New ReportParameter("REASON", reason)

            reportViewer2.LocalReport.SetParameters(New ReportParameter() {SiteCodeParam, FromDateParam, ToDateParam, PromotionsIdParam, payoutvalueParam, reasonParam})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource As New ReportDataSource("DS_Offer", dsJkDayOfReport.Tables(0))
            reportViewer2.LocalReport.DataSources.Add(DataSource)
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Excel")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            path = clsDefaultConfiguration.DayCloseReportPath & "\JKDayOfReportPayout_" & System.DateTime.Now.ToString("dd-MM-yyyy") & ".xls"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using

            Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'JKProductMixReport
    Public Function JKProductMixReport(ByVal request As SpectrumCommon.DayCloseReportModel, ByVal LoginUser As String)
        Try
            Dim path As String = ""
            Dim dsProductMixReport As DataSet
            Dim clsObj As New clsJKProductMixReport
            dsProductMixReport = clsObj.JKProductMixReport(request.SiteCode.ToString(), request.FromDate, request.ToDate)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\JKProductMixReport.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            Dim SiteCodeParam As New ReportParameter("SiteCode", request.SiteCode)
            Dim FromDateParam As New ReportParameter("FromDate", request.FromDate)
            'Dim ToDateParam As New ReportParameter("ToDate", request.ToDate)_
            Dim ToDateParam As New ReportParameter("ToDate", request.ToDate)
            Dim LoginUserNameParam As New ReportParameter("LoginUser", LoginUser)

            reportViewer2.LocalReport.SetParameters(New ReportParameter() {SiteCodeParam, FromDateParam, ToDateParam, LoginUserNameParam})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource1 As New ReportDataSource("dsJKProductMixReportHeader", dsProductMixReport.Tables(0))
            Dim DataSource2 As New ReportDataSource("dsJKProductMixReportData", dsProductMixReport.Tables(1))
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.LocalReport.DataSources.Add(DataSource2)
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Excel")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            path = clsDefaultConfiguration.DayCloseReportPath & "\JKProductMixReport_" & System.DateTime.Now.ToString("dd-MM-yyyy") & ".xls"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'GenerateJKSalesReconciliation
    ' added by khusrao adil
    Public Function GenerateJKSalesReconciliation(ByVal request As SpectrumCommon.DayCloseReportModel)
        Try
            Dim DSSalesReconciliation As DataSet
            Dim clsObj As New clsSalesReconciliation
            DSSalesReconciliation = clsObj.SalesReconciliationReport(request.SiteCode.ToString(), request.FromDate, request.ToDate)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\SalesReconciliationReport.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            Dim FromDateParam As New ReportParameter("FROMDATE", request.FromDate)
            Dim ToDateParam As New ReportParameter("TODATE", request.ToDate)
            Dim SiteCodeParam As New ReportParameter("SITECODE", request.SiteCode)

            reportViewer2.LocalReport.SetParameters(New ReportParameter() {FromDateParam, ToDateParam, SiteCodeParam})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()

            Dim DataSource1 As New ReportDataSource("DataSet1", DSSalesReconciliation.Tables(0))
            reportViewer2.LocalReport.DataSources.Add(DataSource1)

            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Excel")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            path = clsDefaultConfiguration.DayCloseReportPath & "\JKSalesReconciliationReport_" & DateTime.Now.ToString("dd-MM-yyyy-hhmmss") & ".xls"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function ImprestCashAmountDetailsReport(ByVal request As SpectrumCommon.DayCloseReportModel)
        Try
            Dim fDate = request.FromDate
            fDate = Convert.ToDateTime(fDate).ToString("dd-MMM-yyyy")
            Dim tDate = request.ToDate
            tDate = Convert.ToDateTime(tDate).ToString("dd-MMM-yyyy")
            Dim currentshiftid As String = ""
            Dim dt As DataTable = clsCommon.GetPreviousShiftDetails(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID)
            If dt.Rows.Count > 0 Then
                currentshiftid = dt.Rows(0)("ShiftId").ToString()
            End If
            Dim dsImprestReport As DataSet
            Dim path As String = ""
            Dim TerminalNo As String = "ALL"
            ' If objPc.cmdTill.SelectedValue <> "" Then
            TerminalNo = "ALL"
            '  Else

            '   End If
            Dim clsObj As New ReportBase
            Dim Time = DateTime.Now.ToString("HH:mm")
            dsImprestReport = clsObj.GetImprestCasheportData(clsAdmin.SiteCode, request.FromDate, request.ToDate, TerminalNo)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\ImprestCashAmountReport.rdl")
            reportViewer2.LocalReport.ReportPath = appPath
            'Dim f1Date = Convert.ToDateTime(frmDate).ToString("dd-MM-yyyy")
            'Dim T1Date = Convert.ToDateTime(toDate).ToString("dd-MM-yyyy")
            Dim f1Date = Convert.ToDateTime(request.FromDate).ToString("yyyy-MM-dd")
            Dim T1Date = Convert.ToDateTime(request.ToDate).ToString("yyyy-MM-dd")
            Dim SiteCodeParam As New ReportParameter("V_SiteCode", clsAdmin.SiteCode)
            Dim FromDateParam As New ReportParameter("FromDate", f1Date)
            Dim ToDateParam As New ReportParameter("ToDate", T1Date)
            Dim terminalId As New ReportParameter("V_TerminalId", TerminalNo)
            Dim paratime As New ReportParameter("GTime", Time)
            reportViewer2.LocalReport.SetParameters(New ReportParameter() {SiteCodeParam, FromDateParam, ToDateParam, terminalId, paratime})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource As New ReportDataSource("dsHeader", dsImprestReport.Tables(0))
            Dim datasource1 As New ReportDataSource("dsImprestCash", dsImprestReport.Tables(1))
            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(datasource1)

            Dim mybytes As [Byte]()

            'If objPc.radioPdf.Checked = True Then
            '    mybytes = reportViewer2.LocalReport.Render("Pdf")
            '    path = clsDefaultConfiguration.DayCloseReportPath & "\" & fDate & " To " & tDate & "_" & TerminalNo & "_" & "_ImprestCashAmountDetailReport" & ".pdf"
            'Else
            mybytes = reportViewer2.LocalReport.Render("Excel")
            '   path = clsDefaultConfiguration.DayCloseReportPath & "\" & fDate & " To " & tDate & "_" & TerminalNo & "_" & "_ImprestCashAmountDetailReport" & ".xls"
            path = clsDefaultConfiguration.DayCloseReportPath & "\" & request.FromDate.ToString("dd-MM-yyyy") & " To " & request.ToDate.ToString("dd-MM-yyyy") & "_" & TerminalNo & "_" & "_ImprestCashAmountDetailReport" & ".xls"

            '    End If
            If System.IO.File.Exists(path) Then
                File.Delete(path)
                ' Dim id As Integer 'Global variable
                ' id = System.Diagnostics.Process.Start("E:\DayCloseReports\path.pdf").Id
                ' System.Diagnostics.Process.GetProcessById(id).Kill()
            Else

            End If

            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using


            Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
   Public Function GenerateTallySalesReport(ByVal request As SpectrumCommon.DayCloseReportModel)
        Try
            Dim DtTally As DataTable
            Dim obj As New clsCommon
            DtTally = obj.GenerateTallyReport(request.SiteCode.ToString(), request.FromDate, request.ToDate)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\TallySalesReport.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            Dim SiteCodeParam As New ReportParameter("SiteCode", request.SiteCode)
            Dim FromDateParam As New ReportParameter("FromDate", request.FromDate)
            Dim ToDateParam As New ReportParameter("Todate", request.ToDate)


            reportViewer2.LocalReport.SetParameters(New ReportParameter() {FromDateParam, ToDateParam, SiteCodeParam})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()

            Dim DataSource1 As New ReportDataSource("DataSet1", DtTally)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)

            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Excel")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            path = clsDefaultConfiguration.DayCloseReportPath & "\TallySalesReport" & request.FromDate.ToString("dd-MM-yyyy") & "_" & request.ToDate.ToString("dd-MM-yyyy") & ".xls"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            Process.Start(path)


        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function genrateXread(ByVal request As SpectrumCommon.DayCloseReportModel)
        Try

            Dim path As String = ""
            Dim dtHeader As DataTable
            Dim dtSummary As DataTable
            Dim dtItemWiseSales As DataTable
            Dim dtTenderWiseSales As DataTable
            Dim dtTimeWiseSales As DataTable
            Dim dtConsumption As DataTable
            Dim dtNoOfBills As DataTable

            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\X-Read.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            reportViewer2.LocalReport.SetParameters(New ReportParameter("V_BillDate", request.ToDate.ToString()))

            Dim xredobj As New clsXRead

            Dim ds As New DataSet
            'ds = xredobj.GetXReadData(clsAdmin.DayOpenDate)
            'dtHeader = xredobj.GetXReadHeaderData(clsAdmin.DayOpenDate) 'Changed by ketan 
            ds = xredobj.GetXReadData(request.FromDate.ToString(), clsAdmin.SiteCode, request.ToDate.ToString())
            dtHeader = xredobj.GetXReadHeaderData(request.FromDate.ToString(), clsAdmin.SiteCode, request.ToDate.ToString())


            If ds.Tables.Count > 0 AndAlso Not ds Is Nothing Then
                dtSummary = ds.Tables(0)
                dtItemWiseSales = ds.Tables(1)
                dtTenderWiseSales = ds.Tables(2)
                dtTimeWiseSales = ds.Tables(3)
                dtConsumption = ds.Tables(4)
                dtNoOfBills = ds.Tables(5)
            End If


            Dim DataSource As New ReportDataSource("dtHeader", dtHeader)
            Dim DataSource1 As New ReportDataSource("dtSummary", dtSummary)
            Dim DataSource2 As New ReportDataSource("dtItemWiseSales", dtItemWiseSales)
            Dim DataSource3 As New ReportDataSource("dtTenderWiseSales", dtTenderWiseSales)
            Dim DataSource4 As New ReportDataSource("dtTimeWiseSales", dtTimeWiseSales)
            Dim DataSource5 As New ReportDataSource("dtExistingXREADDataColumn", dtConsumption)
            Dim DataSource6 As New ReportDataSource("dtNoOfBills", dtNoOfBills)


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
            path = clsDefaultConfiguration.DayCloseReportPath & "\X-Read" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm") & ".pdf"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using

            'Code For Print x-read

            PrinterName = SetPrinterName(dtPrinterInfo, "ALLDocs", "X-Read")
            Dim pdfdocument As New PdfDocument()
            pdfdocument.LoadFromFile(path)
            If Not String.IsNullOrEmpty(PrinterName) Then
                pdfdocument.PrinterName = PrinterName
                pdfdocument.PrintDocument.PrinterSettings.Copies = 1
                pdfdocument.PrintDocument.Print()
            End If
            pdfdocument.Dispose()

        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    'Code is added by irfan on 31/8/2018 for CustomerWiseSalesReport
    Public Function genrateCustomerWiseSalesReport(ByVal request As SpectrumCommon.DayCloseReportModel)
        Try

            Dim path As String = ""

            Dim dtSummary As DataTable
            Dim dtItemWiseSales As DataTable
            Dim dtTenderWiseSales As DataTable
            Dim dtTimeWiseSales As DataTable
            Dim dtConsumption As DataTable

            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\CustomerWiseSaleReport.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            Dim SiteCodeParam As New ReportParameter("SiteCode", request.SiteCode)
            Dim FromDateParam As New ReportParameter("FromDate", request.FromDate)
            Dim ToDateParam As New ReportParameter("ToDate", request.ToDate)
            Dim UserParam As New ReportParameter("User", request.CreatedBy)

            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            reportViewer2.LocalReport.SetParameters(New ReportParameter() {SiteCodeParam, FromDateParam, ToDateParam, UserParam})


            Dim xredobj As New clsXRead
            Dim ds As New DataSet
            'ds = xredobj.GetXReadData(clsAdmin.DayOpenDate)
            'dtHeader = xredobj.GetXReadHeaderData(clsAdmin.DayOpenDate) 'Changed by ketan 
            ds = xredobj.GetCustomerwiseSalesReportData(clsAdmin.SiteCode, request.FromDate.ToString(), request.ToDate.ToString())


            Dim DataSource As New ReportDataSource("dtDetail", ds.Tables(0))
            Dim DataSource1 As New ReportDataSource("dtHeader", ds.Tables(1))


            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)

            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Excel")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            Dim obj As New clsCommon
            'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
            path = clsDefaultConfiguration.DayCloseReportPath & "\CustomerWiseSalesReport" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm") & ".xls"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            Process.Start(path)
            'Code For Print x-read

            'PrinterName = SetPrinterName(dtPrinterInfo, "ALLDocs", "CustomerWiseSalesReport")
            'Dim pdfdocument As New PdfDocument()
            'pdfdocument.LoadFromFile(path)
            'If Not String.IsNullOrEmpty(PrinterName) Then
            '    pdfdocument.PrinterName = PrinterName
            '    pdfdocument.PrintDocument.PrinterSettings.Copies = 1
            '    pdfdocument.PrintDocument.Print()
            'End If
            'pdfdocument.Dispose()

        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    'Added by Jayesh for 3005

    Public Function DisplaySalesandTransactionsReport(ByVal request As SpectrumCommon.DayCloseReportModel)
        Try
            Dim path As String = ""
            Dim dtSummary As DataTable
            Dim dtItemWiseSales As DataTable
            Dim dtTenderWiseSales As DataTable
            Dim dtTimeWiseSales As DataTable
            Dim dtConsumption As DataTable
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\SalesandTransactionsReport.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            Dim SiteCodeParam As New ReportParameter("SiteCode", request.SiteCode)
            Dim FromDateParam As New ReportParameter("FromDate", request.FromDate)
            Dim ToDateParam As New ReportParameter("ToDate", request.ToDate)
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            reportViewer2.LocalReport.SetParameters(New ReportParameter() {SiteCodeParam, FromDateParam, ToDateParam})

            Dim xredobj As New clsXRead
            Dim ds As New DataSet
            ds = xredobj.GetSalesandTransactionsReport(clsAdmin.SiteCode, request.FromDate.ToString(), request.ToDate.ToString())

            Dim DataSource As New ReportDataSource("DataSet1", ds.Tables(0))
            Dim DataSource1 As New ReportDataSource("DataSet2", ds.Tables(1))


            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)

            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Excel")
            Dim obj As New clsCommon
            path = clsDefaultConfiguration.DayCloseReportPath & "\SalesandTransactionsReport" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm") & ".xls"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    'added by khusrao adil on 14-07-2017
    Public Function genrateXreadCategoryWise(ByVal request As SpectrumCommon.DayCloseReportModel)
        Try

            Dim path As String = ""
            Dim dtHeader As DataTable
            Dim dtSummary As DataTable
            Dim dtItemWiseSales As DataTable
            Dim dtTenderWiseSales As DataTable
            Dim dtTimeWiseSales As DataTable
            Dim dtConsumption As DataTable

            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\X-ReadCategorywise.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            reportViewer2.LocalReport.SetParameters(New ReportParameter("V_BillDate", request.ToDate.ToString()))

            Dim xredobj As New clsXRead

            Dim ds As New DataSet
            'ds = xredobj.GetXReadData(clsAdmin.DayOpenDate)
            'dtHeader = xredobj.GetXReadHeaderData(clsAdmin.DayOpenDate) 'Changed by ketan 
            ds = xredobj.GetXReadCategorywiseData(request.ToDate.ToString(), clsAdmin.SiteCode)
            dtHeader = xredobj.GetXReadCategorywiseHeaderData(request.ToDate.ToString(), clsAdmin.SiteCode)


            If ds.Tables.Count > 0 AndAlso Not ds Is Nothing Then
                dtSummary = ds.Tables(0)
                dtItemWiseSales = ds.Tables(1)
                dtTenderWiseSales = ds.Tables(2)
                'dtTimeWiseSales = ds.Tables(3)
                'dtConsumption = ds.Tables(4)
            End If


            Dim DataSource As New ReportDataSource("dtHeader", dtHeader)
            Dim DataSource1 As New ReportDataSource("dtSummary", dtSummary)
            Dim DataSource2 As New ReportDataSource("dtItemWiseSales", dtItemWiseSales)
            Dim DataSource3 As New ReportDataSource("dtTenderWiseSales", dtTenderWiseSales)
            '  Dim DataSource4 As New ReportDataSource("dtTimeWiseSales", dtTimeWiseSales)
            '  Dim DataSource5 As New ReportDataSource("dtExistingXREADDataColumn", dtConsumption)


            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.LocalReport.DataSources.Add(DataSource2)
            reportViewer2.LocalReport.DataSources.Add(DataSource3)
            ' reportViewer2.LocalReport.DataSources.Add(DataSource4)
            ' reportViewer2.LocalReport.DataSources.Add(DataSource5)

            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            Dim obj As New clsCommon
            'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
            path = clsDefaultConfiguration.DayCloseReportPath & "\X-ReadCategorywise" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm") & ".pdf"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using

            'Code For Print x-read

            PrinterName = SetPrinterName(dtPrinterInfo, "ALLDocs", "X-ReadCategorywise")
            Dim pdfdocument As New PdfDocument()
            pdfdocument.LoadFromFile(path)
            If Not String.IsNullOrEmpty(PrinterName) Then
                pdfdocument.PrinterName = PrinterName
                pdfdocument.PrintDocument.PrinterSettings.Copies = 1
                pdfdocument.PrintDocument.Print()
            End If
            pdfdocument.Dispose()

        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    'code added by vipul on 21-05-2018
    Public Function genrateXreadFlavourWise(ByVal request As SpectrumCommon.DayCloseReportModel)
        Try

            Dim path As String = ""
            Dim dtHeader As DataTable
            Dim dtSummary As DataTable
            Dim dtItemWiseSales As DataTable
            Dim dtTenderWiseSales As DataTable
            Dim dtTimeWiseSales As DataTable
            Dim dtConsumption As DataTable

            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\X-ReadFlavourwise.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            reportViewer2.LocalReport.SetParameters(New ReportParameter("V_BillDate", request.ToDate.ToString()))

            Dim xredobj As New clsXRead

            Dim ds As New DataSet

            ds = xredobj.GetXReadFavourwiseData(request.FromDate.ToString(), clsAdmin.SiteCode, request.ToDate.ToString())
            dtHeader = xredobj.GetXReadFlavourwiseHeaderData(request.FromDate.ToString(), clsAdmin.SiteCode, request.ToDate.ToString())


            If ds.Tables.Count > 0 AndAlso Not ds Is Nothing Then
                dtSummary = ds.Tables(0)
                dtItemWiseSales = ds.Tables(1)
                dtTenderWiseSales = ds.Tables(2)
            End If


            Dim DataSource As New ReportDataSource("dtHeader", dtHeader)
            Dim DataSource1 As New ReportDataSource("dtSummary", dtSummary)
            Dim DataSource2 As New ReportDataSource("dtItemWiseSales", dtItemWiseSales)
            Dim DataSource3 As New ReportDataSource("dtTenderWiseSales", dtTenderWiseSales)



            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.LocalReport.DataSources.Add(DataSource2)
            reportViewer2.LocalReport.DataSources.Add(DataSource3)


            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")

            Dim obj As New clsCommon

            path = clsDefaultConfiguration.DayCloseReportPath & "\X-ReadFlavourwise" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm") & ".pdf"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using

            PrinterName = SetPrinterName(dtPrinterInfo, "ALLDocs", "X-ReadCategorywise")
            Dim pdfdocument As New PdfDocument()
            pdfdocument.LoadFromFile(path)
            If Not String.IsNullOrEmpty(PrinterName) Then
                pdfdocument.PrinterName = PrinterName
                pdfdocument.PrintDocument.PrinterSettings.Copies = 1
                pdfdocument.PrintDocument.Print()
            End If
            pdfdocument.Dispose()

        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    'code added by vipul on 25-04-2018
    Public Function genrateXreadcat(ByVal request As SpectrumCommon.DayCloseReportModel)
        Try

            Dim path As String = ""
            Dim dtHeader As DataTable
            Dim dtSummary As DataTable
            Dim dtItemWiseSales As DataTable
            Dim dtTenderWiseSales As DataTable
            Dim dtTimeWiseSales As DataTable
            Dim dtConsumption As DataTable

            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\X-ReadCatReport.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            reportViewer2.LocalReport.SetParameters(New ReportParameter("V_BillDate", request.ToDate.ToString()))

            Dim xredobj As New clsXRead

            Dim ds As New DataSet

            ds = xredobj.GetXReadCatData(request.FromDate.ToString(), clsAdmin.SiteCode, request.ToDate.ToString())
            dtHeader = xredobj.GetXReadCatHeaderData(request.FromDate.ToString(), clsAdmin.SiteCode, request.ToDate.ToString())


            If ds.Tables.Count > 0 AndAlso Not ds Is Nothing Then
                dtSummary = ds.Tables(0)
                dtItemWiseSales = ds.Tables(1)
                dtTenderWiseSales = ds.Tables(2)
            End If


            Dim DataSource As New ReportDataSource("dtHeader", dtHeader)
            Dim DataSource1 As New ReportDataSource("dtSummary", dtSummary)
            Dim DataSource2 As New ReportDataSource("dtItemWiseSales", dtItemWiseSales)
            Dim DataSource3 As New ReportDataSource("dtTenderWiseSales", dtTenderWiseSales)



            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.LocalReport.DataSources.Add(DataSource2)
            reportViewer2.LocalReport.DataSources.Add(DataSource3)


            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")

            Dim obj As New clsCommon

            path = clsDefaultConfiguration.DayCloseReportPath & "\X-ReadCat" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm") & ".pdf"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using

            PrinterName = SetPrinterName(dtPrinterInfo, "ALLDocs", "X-ReadCategorywise")
            Dim pdfdocument As New PdfDocument()
            pdfdocument.LoadFromFile(path)
            If Not String.IsNullOrEmpty(PrinterName) Then
                pdfdocument.PrinterName = PrinterName
                pdfdocument.PrintDocument.PrinterSettings.Copies = 1
                pdfdocument.PrintDocument.Print()
            End If
            pdfdocument.Dispose()
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    'Code is added by irfan on 24-7-2018
    Public Function WriteOffDetailsReport(ByVal request As SpectrumCommon.DayCloseReportModel)
        Try
            Dim clsObj As New clsXRead
            '  Dim fDate = objPc.dtFromDate.Text
            Dim fDate = Convert.ToDateTime(request.FromDate).ToString("dd-MMM-yyyy")
            ' Dim tDate = objPc.dtToDate.Text
            Dim tDate = Convert.ToDateTime(request.ToDate).ToString("dd-MMM-yyyy")
            Dim currentshiftid As String = ""
            Dim dt As DataTable = clsCommon.GetPreviousShiftDetails(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID)
            If dt.Rows.Count > 0 Then
                currentshiftid = dt.Rows(0)("ShiftId").ToString()
                '' createdon = dt.Rows(0)("CREATEDON")
            End If
            Dim path As String = ""
            Dim TerminalNo As String = "ALL"
            Dim dsWriteOffReport As DataSet
            'If objPc.cmdTill.SelectedValue <> "" Then
            '    TerminalNo = objPc.cmdTill.SelectedValue
            '    ' TerminalNo=clsAdmin.TerminalID
            'Else

            'End If

            Dim Time = DateTime.Now.ToString("HH:mm")

            dsWriteOffReport = clsObj.GetWriteOffReportData(clsAdmin.SiteCode, request.FromDate.ToString(), request.ToDate.ToString(), TerminalNo)

            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\WriteOffDetailsReport.rdl")
            reportViewer2.LocalReport.ReportPath = appPath
            Dim f1Date = Convert.ToDateTime(request.FromDate).ToString("dd-MM-yyyy")
            Dim T1Date = Convert.ToDateTime(request.ToDate).ToString("dd-MM-yyyy")
            Dim SiteCodeParam As New ReportParameter("V_SiteCode", request.SiteCode)
            Dim FromDateParam As New ReportParameter("FromDate", f1Date)
            Dim ToDateParam As New ReportParameter("ToDate", T1Date)
            Dim terminalId As New ReportParameter("V_TerminalId", TerminalNo)
            Dim paratime As New ReportParameter("GTime", Time)
            reportViewer2.LocalReport.SetParameters(New ReportParameter() {SiteCodeParam, FromDateParam, ToDateParam, terminalId, paratime})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource As New ReportDataSource("dsHeader", dsWriteOffReport.Tables(0))
            Dim datasource1 As New ReportDataSource("dsWriteOffData", dsWriteOffReport.Tables(1))
            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(datasource1)
            Dim mybytes As [Byte]()
            'If objPc.radioPdf.Checked = True Then
            mybytes = reportViewer2.LocalReport.Render("Pdf")
            path = clsDefaultConfiguration.DayCloseReportPath & "\" & fDate & "_" & tDate & "_" & currentshiftid & "_" & TerminalNo & "_WriteOffDetailsReport" & ".pdf"
            'Else
            'mybytes = reportViewer2.LocalReport.Render("Excel")
            'path = clsDefaultConfiguration.DayCloseReportPath & "\" & fDate & "_" & tDate & "_" & currentshiftid & "_" & TerminalNo & "_WriteOffDetailsReport" & ".xls"
            'End If
            If System.IO.File.Exists(path) Then
                File.Delete(path)

            Else

            End If

            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using

            Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    ''added by Nikhil
    Public Function GenerateFoodCost(ByVal request As SpectrumCommon.DayCloseReportModel)
        Try
            Dim path As String = ""
            Dim dsFoodCost As DataSet
            Dim clsObj As New clsFoodCost
            dsFoodCost = clsObj.GetFoodCostReport(request.SiteCode.ToString(), request.FromDate, request.ToDate)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\JKFoodCostReport.rdl")
            reportViewer2.LocalReport.ReportPath = appPath
            Dim FromDateParam As New ReportParameter("FromDate", request.FromDate)
            Dim ToDateParam As New ReportParameter("ToDate", request.ToDate)
            Dim SiteCodeParam As New ReportParameter("SITECODE", request.SiteCode)
            reportViewer2.LocalReport.SetParameters(New ReportParameter() {FromDateParam, ToDateParam, SiteCodeParam})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource As New ReportDataSource("UDP_FoodCostReport", dsFoodCost.Tables(0))
            reportViewer2.LocalReport.DataSources.Add(DataSource)

            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Excel")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            path = clsDefaultConfiguration.DayCloseReportPath & "\JKFoodCostReport_" & System.DateTime.Now.ToString("dd-MM-yyyy") & ".xls"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Public Function GenerateDSRReport(ByVal request As SpectrumCommon.DayCloseReportModel) As Boolean
        Try

            Dim dsDsr As New DataSet()
            Dim clsObj As New DsrReport
            dsDsr = clsObj.GetDsrReportData(request.SiteCode, request.ToDate)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\SalesReportJumboKing.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            Dim dateNameParam As New ReportParameter("Date", request.ToDate)
            Dim SiteCodeParam As New ReportParameter("Sitecode", request.SiteCode)

            reportViewer2.LocalReport.SetParameters(New ReportParameter() {dateNameParam, SiteCodeParam})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource As New ReportDataSource("DataSet1", dsDsr.Tables(0))
            Dim DataSource1 As New ReportDataSource("DataSet2", dsDsr.Tables(0))
            Dim DataSource2 As New ReportDataSource("DataSet3", dsDsr.Tables(0))
            Dim DataSource3 As New ReportDataSource("DataSet4", dsDsr.Tables(0))
            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.LocalReport.DataSources.Add(DataSource2)
            reportViewer2.LocalReport.DataSources.Add(DataSource3)

            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Excel")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            path = clsDefaultConfiguration.DayCloseReportPath & "\DSR_" & request.ToDate.ToString("dd-MM-yyyy") & ".xls"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using

            Process.Start(path)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function GenerateDayCloseReport(ByVal request As SpectrumCommon.DayCloseReportModel) As Boolean
        Try

            Dim dsDsr As New DataSet()
            Dim clsObj As New DayCloseNewReport
            dsDsr = clsObj.GetNewDayCloseReportData(request.SiteCode, request.ToDate)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\dayclose.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            Dim dateNameParam As New ReportParameter("V_DayCloseDate", request.ToDate)
            Dim SiteCodeParam As New ReportParameter("V_SiteCode", request.SiteCode)
            Dim SaleTypeParm As New ReportParameter("V_SaleType", "SHOP")

            reportViewer2.LocalReport.SetParameters(New ReportParameter() {dateNameParam, SiteCodeParam, SaleTypeParm})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource As New ReportDataSource("ReportHeaderData", dsDsr.Tables(clsObj.DayCloseDatasetNames.ReportHeaderData))
            Dim DataSource1 As New ReportDataSource("NetSaleDataShop", dsDsr.Tables(clsObj.DayCloseDatasetNames.NetSaleDataShop))
            ' Dim DataSource2 As New ReportDataSource("TotalNetSalesShop", dsDsr.Tables(clsObj.DayCloseDatasetNames.TotalNetSalesShop))
            ' Dim DataSource3 As New ReportDataSource("TotalNetSalesOther", dsDsr.Tables(clsObj.DayCloseDatasetNames.TotalNetSalesOther))
            Dim DataSource4 As New ReportDataSource("NetSaleDataOther", dsDsr.Tables(clsObj.DayCloseDatasetNames.NetSaleDataOther))
            Dim DataSource5 As New ReportDataSource("NetCashData", dsDsr.Tables(clsObj.DayCloseDatasetNames.NetCashData))
            Dim DataSource6 As New ReportDataSource("ShiftWiseCashierData", dsDsr.Tables(clsObj.DayCloseDatasetNames.ShiftWiseCashierData))
            Dim DataSource7 As New ReportDataSource("SummaryData", dsDsr.Tables(clsObj.DayCloseDatasetNames.SummaryData))
            Dim DataSource8 As New ReportDataSource("CorrectedCashMemoData", dsDsr.Tables(clsObj.DayCloseDatasetNames.CorrectedCashMemoData))
            Dim DataSource9 As New ReportDataSource("DeletedCashMemoData", dsDsr.Tables(clsObj.DayCloseDatasetNames.DeletedCashMemoData))
            Dim DataSource10 As New ReportDataSource("SaleReturnData", dsDsr.Tables(clsObj.DayCloseDatasetNames.SaleReturnData))
            Dim DataSource11 As New ReportDataSource("StatisticsData", dsDsr.Tables(clsObj.DayCloseDatasetNames.StatisticsData))
            Dim DataSource12 As New ReportDataSource("PettyCashExpenseData", dsDsr.Tables(clsObj.DayCloseDatasetNames.PettyCashExpenseData))
            Dim DataSource13 As New ReportDataSource("PettyCashReceiptData", dsDsr.Tables(clsObj.DayCloseDatasetNames.PettyCashReceiptData))
            Dim DataSource14 As New ReportDataSource("OpeningData", dsDsr.Tables(clsObj.DayCloseDatasetNames.OpeningData))
            Dim DataSource15 As New ReportDataSource("ClosingData", dsDsr.Tables(clsObj.DayCloseDatasetNames.ClosingData))
            Dim DataSource16 As New ReportDataSource("NextDayOpening", dsDsr.Tables(clsObj.DayCloseDatasetNames.NextDayOpening))
            Dim DataSource17 As New ReportDataSource("CloseBankData", dsDsr.Tables(clsObj.DayCloseDatasetNames.CloseBankData))
            Dim DataSource18 As New ReportDataSource("ItemWiseSalesData", dsDsr.Tables(clsObj.DayCloseDatasetNames.ItemWiseSalesData))
            Dim DataSource19 As New ReportDataSource("WriteOffData", dsDsr.Tables(clsObj.DayCloseDatasetNames.WriteOffData)) '' added by nikhil
            Dim DataSource20 As New ReportDataSource("NEFT", dsDsr.Tables(clsObj.DayCloseDatasetNames.NEFT))               'vipin
            Dim DataSource21 As New ReportDataSource("RTGS", dsDsr.Tables(clsObj.DayCloseDatasetNames.RTGS))
            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            ' reportViewer2.LocalReport.DataSources.Add(DataSource2)
            ' reportViewer2.LocalReport.DataSources.Add(DataSource3)
            reportViewer2.LocalReport.DataSources.Add(DataSource4)
            reportViewer2.LocalReport.DataSources.Add(DataSource5)
            reportViewer2.LocalReport.DataSources.Add(DataSource6)
            reportViewer2.LocalReport.DataSources.Add(DataSource7)
            reportViewer2.LocalReport.DataSources.Add(DataSource8)
            reportViewer2.LocalReport.DataSources.Add(DataSource9)
            reportViewer2.LocalReport.DataSources.Add(DataSource10)
            reportViewer2.LocalReport.DataSources.Add(DataSource11)
            reportViewer2.LocalReport.DataSources.Add(DataSource12)
            reportViewer2.LocalReport.DataSources.Add(DataSource13)
            reportViewer2.LocalReport.DataSources.Add(DataSource14)
            reportViewer2.LocalReport.DataSources.Add(DataSource15)
            reportViewer2.LocalReport.DataSources.Add(DataSource16)
            reportViewer2.LocalReport.DataSources.Add(DataSource17)
            reportViewer2.LocalReport.DataSources.Add(DataSource18)
            reportViewer2.LocalReport.DataSources.Add(DataSource19)  'vipin
            reportViewer2.LocalReport.DataSources.Add(DataSource20)
            reportViewer2.LocalReport.DataSources.Add(DataSource21)
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            Dim obj As New clsCommon
            Dim siteName As String = obj.GetSiteName(request.SiteCode)
            'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
            path = clsDefaultConfiguration.DayCloseReportPath & "\" & dsDsr.Tables(clsObj.DayCloseDatasetNames.ReportHeaderData).Rows(0)("Site") & "_DayCloseReport_" & request.ToDate.ToString("dd-MM-yyyy") & ".pdf"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using

            Process.Start(path)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Public Function HcCustomerDetailReport(ByVal request As SpectrumCommon.DayCloseReportModel, ByVal Classify As String)
        Try
            Dim path As String = ""
            Dim dsHcCustomerDetails As DataSet
            Dim clsObj As New clsHcCustomerDetailsReport
            dsHcCustomerDetails = clsObj.HcCustomerDetailsReport(request.FromDate, request.ToDate, Classify)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\CustomerDetailsReport.rdl")
            reportViewer2.LocalReport.ReportPath = appPath
            Dim ClassifyParam As New ReportParameter("Classify", Classify)
            Dim FromDateParam As New ReportParameter("FromDate", request.FromDate)
            Dim ToDateParam As New ReportParameter("ToDate", request.ToDate)

            reportViewer2.LocalReport.SetParameters(New ReportParameter() {ClassifyParam, FromDateParam, ToDateParam})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource1 As New ReportDataSource("dsHcCustomersDetailsReport", dsHcCustomerDetails.Tables(0))
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Excel")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            path = clsDefaultConfiguration.DayCloseReportPath & "\CustomerDetailsReport_" & System.DateTime.Now.ToString("dd-MM-yyyy") & ".xls"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    ''added by nikhil 
    'merged by khusrao adil on 15-09-2017 for jk sprint 29 code merging
    Public Function GeneratePersonWiseSalesReport(ByVal request As SpectrumCommon.DayCloseReportModel)
        Try
            Dim path As String = ""
            Dim dsFoodCost As DataSet
            Dim clsObj As New clsFoodCost
            dsFoodCost = clsObj.GetPersonWiseSalesReport(request.SiteCode.ToString(), request.FromDate, request.ToDate)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\SalesPersonWiseSalesReport.rdl")
            reportViewer2.LocalReport.ReportPath = appPath
            Dim SiteCodeParam As New ReportParameter("SITECODE", request.SiteCode)
            Dim FromDateParam As New ReportParameter("FROMDATE", request.FromDate)
            Dim ToDateParam As New ReportParameter("TODATE", request.ToDate)

            reportViewer2.LocalReport.SetParameters(New ReportParameter() {SiteCodeParam, FromDateParam, ToDateParam})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource As New ReportDataSource("DataSet1", dsFoodCost.Tables(0))
            Dim DataSource1 As New ReportDataSource("DataSet2", dsFoodCost.Tables(1))
            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)

            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Excel")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            path = clsDefaultConfiguration.DayCloseReportPath & "\SalesPersonWiseSalesReport" & System.DateTime.Now.ToString("dd-MM-yyyy") & ".xls"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    ''

    Public Function GenerateDeliveryPartnerWiseSalesReport(ByVal request As SpectrumCommon.DayCloseReportModel, ByVal selectedPartner As String)
        Try

            Dim path As String = ""
            Dim dsFoodCost As DataSet
            Dim clsObj As New clsFoodCost
            dsFoodCost = clsObj.GetDeliveryPartnerWiseSalesReport(request.SiteCode.ToString(), request.FromDate, request.ToDate, selectedPartner)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\DeliveryPartnerWiseSalesReport.rdl")
            reportViewer2.LocalReport.ReportPath = appPath
            Dim SiteCodeParam As New ReportParameter("SiteCode", request.SiteCode)
            Dim FromDateParam As New ReportParameter("FromDate", request.FromDate)
            Dim ToDateParam As New ReportParameter("ToDate", request.ToDate)
            Dim selectedPartnerParam As New ReportParameter("partner", selectedPartner)
            ' Dim ipath As New ReportParameter("Path", Nothing)
            reportViewer2.LocalReport.SetParameters(New ReportParameter() {FromDateParam, ToDateParam, SiteCodeParam, selectedPartnerParam})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource As New ReportDataSource("HomeDeliveryHeader", dsFoodCost.Tables(0))
            Dim DataSource1 As New ReportDataSource("HomeDeliveryReportData", dsFoodCost.Tables(1))
            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)

            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Excel")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            path = clsDefaultConfiguration.DayCloseReportPath & "\DeliveryPartnerWiseSalesReport" & System.DateTime.Now.ToString("dd-MM-yyyy") & ".xls"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    'JKProductMixReportTillWise
    'added by khusrao adil on 29-11-2017 for jk sprint 32
    Public Function JKProductMixReportTillWise(ByVal request As SpectrumCommon.DayCloseReportModel, ByVal Terminals As String)
        Try
            Dim path As String = ""
            Dim dsProductMixReport As DataSet
            Dim clsObj As New clsJKProductMixReport
            dsProductMixReport = clsObj.JKProductMixReportTillWise(request.SiteCode.ToString(), request.FromDate, request.ToDate, Terminals)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\JKProductMixReportTillWise.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            Dim SiteCodeParam As New ReportParameter("SiteCode", request.SiteCode)
            Dim FromDateParam As New ReportParameter("FromDate", request.FromDate)
            Dim ToDateParam As New ReportParameter("ToDate", request.ToDate)
            Dim LoginUserNameParam As New ReportParameter("LoginUser", clsAdmin.UserName)
            Dim TerminalidParam As New ReportParameter("Terminalid", Terminals)

            reportViewer2.LocalReport.SetParameters(New ReportParameter() {SiteCodeParam, FromDateParam, ToDateParam, LoginUserNameParam, TerminalidParam})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource1 As New ReportDataSource("dsJKProductMixReportTillWiseHeader", dsProductMixReport.Tables(0))
            Dim DataSource2 As New ReportDataSource("dsJKProductMixReportTillWiseData", dsProductMixReport.Tables(1))
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.LocalReport.DataSources.Add(DataSource2)
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Excel")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            path = clsDefaultConfiguration.DayCloseReportPath & "\ConversionReport_" & System.DateTime.Now.ToString("dd-MM-yyyy") & ".xls"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function GenerateBillSummaryReport(ByVal request As SpectrumCommon.DayCloseReportModel)
        Try
            Dim path As String = ""
            Dim dsBillSummaryReport As DataSet
            Dim clsObj As New clsFoodCost
            dsBillSummaryReport = clsObj.GetBillSummaryReport(request.SiteCode.ToString(), request.FromDate, request.ToDate)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\BillSummaryReport.rdl")
            reportViewer2.LocalReport.ReportPath = appPath
            Dim FromDateParam As New ReportParameter("FROMDATE", request.FromDate)
            Dim ToDateParam As New ReportParameter("TODATE", request.ToDate)
            Dim SiteCodeParam As New ReportParameter("SITECODE", request.SiteCode)
            '  Dim ImagePath As New ReportParameter("PATH", DBNull)
            reportViewer2.LocalReport.SetParameters(New ReportParameter() {SiteCodeParam, FromDateParam, ToDateParam})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource As New ReportDataSource("DataSet1", dsBillSummaryReport.Tables(0))
            reportViewer2.LocalReport.DataSources.Add(DataSource)

            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Excel")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            path = clsDefaultConfiguration.DayCloseReportPath & "\BillSummaryReport" & System.DateTime.Now.ToString("dd-MM-yyyy") & ".xls"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    '
    Public Function GenerateBillWiseGSTReport(ByVal request As SpectrumCommon.DayCloseReportModel)
        Try
            Dim path As String = ""
            Dim dsBillSummaryReport As DataSet
            Dim clsObj As New clsFoodCost
            dsBillSummaryReport = clsObj.GetBillwiseGSTReport(request.SiteCode.ToString(), request.FromDate, request.ToDate)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\BillWiseGSTReport.rdl")
            reportViewer2.LocalReport.ReportPath = appPath
            Dim FromDateParam As New ReportParameter("FROMDATE", request.FromDate)
            Dim ToDateParam As New ReportParameter("TODATE", request.ToDate)
            Dim SiteCodeParam As New ReportParameter("SITECODE", request.SiteCode)
            '  Dim ImagePath As New ReportParameter("PATH", DBNull)
            reportViewer2.LocalReport.SetParameters(New ReportParameter() {SiteCodeParam, FromDateParam, ToDateParam})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource As New ReportDataSource("DataSet1", dsBillSummaryReport.Tables(0))
            reportViewer2.LocalReport.DataSources.Add(DataSource)

            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Excel")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            path = clsDefaultConfiguration.DayCloseReportPath & "\BillWiseGSTReport" & System.DateTime.Now.ToString("dd-MM-yyyy") & ".xls"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Public Function GenerateBillWiseTenderReport(ByVal request As SpectrumCommon.DayCloseReportModel, ByVal userName As String)
        Try
            Dim path As String = ""
            Dim dsBillWiseTenderReport As DataSet
            Dim clsObj As New clsFoodCost
            dsBillWiseTenderReport = clsObj.GetBillwiseTenderReport(request.SiteCode.ToString(), request.FromDate, request.ToDate)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\BillWiseTenderReport.rdl")
            reportViewer2.LocalReport.ReportPath = appPath
            Dim FromDateParam As New ReportParameter("FromDate", request.FromDate)
            Dim ToDateParam As New ReportParameter("ToDate", request.ToDate)
            Dim SiteCodeParam As New ReportParameter("SiteCode", request.SiteCode)
            Dim UserNameParam As New ReportParameter("UserName", userName)
            '  Dim ImagePath As New ReportParameter("PATH", DBNull)
            reportViewer2.LocalReport.SetParameters(New ReportParameter() {SiteCodeParam, FromDateParam, ToDateParam, UserNameParam})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource As New ReportDataSource("DataSet1", dsBillWiseTenderReport.Tables(0))
            Dim DataSource1 As New ReportDataSource("HeaderData", dsBillWiseTenderReport.Tables(1))


            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)

            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Excel")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            path = clsDefaultConfiguration.DayCloseReportPath & "\BillWiseTenderReport" & System.DateTime.Now.ToString("dd-MM-yyyy") & ".xls"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function


    Public Function GenerateKOTReport(ByVal request As SpectrumCommon.DayCloseReportModel)
        Try
            Dim path As String = ""
            Dim dsBillSummaryReport As DataSet
            Dim clsObj As New clsFoodCost
            dsBillSummaryReport = clsObj.GetKOTReport(request.SiteCode.ToString(), request.FromDate, request.ToDate)

            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\Spectrumkotreport.rdl")
            reportViewer2.LocalReport.ReportPath = appPath
            Dim FromDateParam As New ReportParameter("fromdate", request.FromDate)
            Dim ToDateParam As New ReportParameter("todate", request.ToDate)
            Dim SiteCodeParam As New ReportParameter("sitecd", request.SiteCode)
            '  Dim ImagePath As New ReportParameter("PATH", DBNull)
            reportViewer2.LocalReport.SetParameters(New ReportParameter() {SiteCodeParam, FromDateParam, ToDateParam})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource As New ReportDataSource("DtKotReport", dsBillSummaryReport.Tables(1))
            Dim DataSource1 As New ReportDataSource("DataSet1", dsBillSummaryReport.Tables(5))
            Dim DataSource2 As New ReportDataSource("DataSet2", dsBillSummaryReport.Tables(3))
            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.LocalReport.DataSources.Add(DataSource2)

            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Excel")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            path = clsDefaultConfiguration.DayCloseReportPath & "\Spectrumkotreport" & System.DateTime.Now.ToString("dd-MM-yyyy") & ".xls"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Public Function OrderTypeWiseSalesReport(ByVal request As SpectrumCommon.DayCloseReportModel)
        Try
            'Dim fDate = request.FromDate
            'fDate = Convert.ToDateTime(fDate).ToString("dd-MMM-yyyy")
            'Dim tDate = request.ToDate
            'tDate = Convert.ToDateTime(tDate).ToString("dd-MMM-yyyy")
            'Dim currentshiftid As String = ""
            'Dim dt As DataTable = clsCommon.GetPreviousShiftDetails(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID)
            'If dt.Rows.Count > 0 Then
            '    currentshiftid = dt.Rows(0)("ShiftId").ToString()
            'End If
            Dim ODTypeWiseSaleReport As DataSet
            'Dim path As String = ""
            'Dim TerminalNo As String = "ALL"
            '' If objPc.cmdTill.SelectedValue <> "" Then
            'TerminalNo = "ALL"
            '  Else

            '   End If
            Dim clsObj As New ReportBase
            ODTypeWiseSaleReport = clsObj.OrderTypeWiseSalesReport(clsAdmin.SiteCode, request.FromDate, request.ToDate)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\OrderTypeWiseSales.rdl")
            reportViewer2.LocalReport.ReportPath = appPath
            Dim f1Date = Convert.ToDateTime(request.FromDate).ToString("yyyy-MM-dd")
            Dim T1Date = Convert.ToDateTime(request.ToDate).ToString("yyyy-MM-dd")
            Dim SiteCodeParam As New ReportParameter("SiteCode", clsAdmin.SiteCode)
            Dim FromDateParam As New ReportParameter("FromDate", f1Date)
            Dim ToDateParam As New ReportParameter("ToDate", T1Date)
            reportViewer2.LocalReport.EnableExternalImages = True
            Dim ImgPath As New ReportParameter("Path", "")
            reportViewer2.LocalReport.SetParameters(New ReportParameter() {SiteCodeParam, FromDateParam, ToDateParam, ImgPath})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource As New ReportDataSource("DSHeader", ODTypeWiseSaleReport.Tables(0))
            Dim datasource1 As New ReportDataSource("DSReportData", ODTypeWiseSaleReport.Tables(1))
            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(datasource1)

            Dim mybytes As [Byte]()

            mybytes = reportViewer2.LocalReport.Render("Excel")
            path = clsDefaultConfiguration.DayCloseReportPath & "\" & request.FromDate.ToString("yyyy-MM-dd") & " To " & request.ToDate.ToString("yyyy-MM-dd") & "_" & "_OrderTypeWiseSalesReport" & ".xls"
            If System.IO.File.Exists(path) Then
                File.Delete(path)
            Else

            End If
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using


            Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'vipul 06-09-2018 tenderwisecommisinreport
    Public Function GenerateTenderWiseCommisionReport(ByVal request As SpectrumCommon.DayCloseReportModel, ByVal userName As String, ByVal TenderType As String)
        Try
            Dim path As String = ""
            Dim dsBillWiseTenderReport As DataSet
            Dim clsObj As New clsFoodCost
            dsBillWiseTenderReport = clsObj.GetTenderWiseCommisionReportData(request.SiteCode.ToString(), request.FromDate, request.ToDate, TenderType)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\TenderWiseCommisionReport.rdl")
            reportViewer2.LocalReport.ReportPath = appPath
            Dim FromDateParam As New ReportParameter("FromDate", request.FromDate)
            Dim ToDateParam As New ReportParameter("ToDate", request.ToDate)
            Dim SiteCodeParam As New ReportParameter("SiteCode", request.SiteCode)
            Dim UserNameParam As New ReportParameter("UserName", userName)
            '  Dim ImagePath As New ReportParameter("PATH", DBNull)
            reportViewer2.LocalReport.SetParameters(New ReportParameter() {SiteCodeParam, FromDateParam, ToDateParam, UserNameParam})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource As New ReportDataSource("DataSet1", dsBillWiseTenderReport.Tables(0))
            Dim DataSource1 As New ReportDataSource("HeaderData", dsBillWiseTenderReport.Tables(1))


            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)

            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Excel")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            path = clsDefaultConfiguration.DayCloseReportPath & "\TenderWiseCommisionReport" & System.DateTime.Now.ToString("dd-MM-yyyy") & ".xls"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Function Themechange()
        'Me.Size = New Size(847, 410)
        Me.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black
        Me.BackColor = Color.FromArgb(134, 134, 134)

        Label1.ForeColor = Color.White
        'lblToDate.BackColor = Color.FromArgb(212, 212, 212)
        Label1.AutoSize = False
        Label1.Size = New Size(65, 16)
        Label1.SendToBack()
        Label1.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Label1.TextAlign = ContentAlignment.MiddleLeft

        'Button1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Button1.BackColor = Color.Transparent
        Button1.BackColor = Color.FromArgb(0, 107, 163)
        Button1.ForeColor = Color.FromArgb(255, 255, 255)
        Button1.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        ' Button1.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Button1.FlatStyle = FlatStyle.Flat
        Button1.FlatAppearance.BorderSize = 0
        Button1.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        Button1.Size = New Size(80, 30)

        'Button2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnClose.BackColor = Color.Transparent
        btnClose.BackColor = Color.FromArgb(0, 107, 163)
        btnClose.ForeColor = Color.FromArgb(255, 255, 255)
        btnClose.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'Button2.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.FlatAppearance.BorderSize = 0
        btnClose.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnClose.Size = New Size(80, 30)

    End Function
End Class