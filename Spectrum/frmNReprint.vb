﻿Imports SpectrumBL
Imports SpectrumPrint
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports Spire.Pdf
Imports System.IO
Imports C1.Win.C1BarCode
Imports System.Text
Imports System.Drawing.Printing
Imports System.Drawing.Imaging

Public Class frmNReprint
    Dim dtPInvc As New DataTable
    Dim dvtInvc As New DataTable
    Dim dtSaleItems As DataTable
    Dim dtPayment As DataTable
    Dim dtCharge As DataTable
    Dim IsArticleWiseKot As Boolean = False
    Dim IsCounterCopy As Boolean = False
    Dim IsFinalReceipt As Boolean = False
    Dim dtCustInfo As New DataTable
    Dim dt As New DataTable
    Dim objPrint As New clsReprintBill
    'Dim objCustm As New clsCLPCustomer
    Dim objCharge As New clsSalesOrder
    Dim IsLoadedCombo As Boolean = False

    'Added by Rohit for Issue No. 0006119 Reprint BL Error
    Dim strSalesInvNo As String
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

    Private Sub CtrltxtDocNumber_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtDocNumber.PreviewKeyDown
        If e.KeyCode = Keys.Enter Then
            If String.IsNullOrEmpty(txtDocNumber.Text.Trim) = False Then
                SearchInvoiceInfo(txtDocNumber.Text.Trim)
                RefreshInvoiceGrid()
            Else
                ShowMessage(getValueByKey("RP01"), "RP01 - " & getValueByKey("CLAE04"))
            End If
        End If
    End Sub
    Private Sub BtnSearchInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchInvoice.Click
        Dim objSearch As New frmNReprintSearch
        If objSearch.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim SelectDocNo As String = objSearch.DocumentNo

            txtDocNumber.Text = SelectDocNo

            'Added by Rohit for Issue No. 0006119 Reprint BL Error
            strSalesInvNo = objSearch.SaleInvNo
            'Change end

            SearchInvoiceInfo(SelectDocNo)

            'Added by Rohit for Issue No. 0006119 Reprint BL Error
            If strSalesInvNo <> String.Empty Then
                CboInvoiceNo.SelectedValue = strSalesInvNo
            End If
            'Change end

            RefreshInvoiceGrid()
        End If
    End Sub
    Private Sub CboInvoiceNo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboInvoiceNo.SelectedValueChanged
        If IsLoadedCombo = True Then
            RefreshInvoiceGrid()
        End If
    End Sub

    Private Function SearchInvoiceInfo(ByVal DocumentNumber As String) As Boolean
        Try
            IsLoadedCombo = False
            dtPInvc.Rows.Clear()

            dtPInvc = objPrint.GetAllInvoiceInfo(clsAdmin.SiteCode, clsAdmin.Financialyear, DocumentNumber, PrintTransType)

            If dtPInvc IsNot Nothing AndAlso dtPInvc.Rows.Count > 0 Then

                'Changed by Sameer for Issue ID 6962
                If Not dtPInvc.Rows(0).Item("CustomerNo").ToString() = "" Then
                    dtCustInfo = objCustm.GetCustomerInformation(dtPInvc.Rows(0).Item("CustomerType"), clsAdmin.SiteCode, clsAdmin.CLPProgram, dtPInvc.Rows(0).Item("CustomerNo"))
                    If dtCustInfo IsNot Nothing AndAlso dtCustInfo.Rows.Count > 0 Then
                        SetCustomerInfo(dtCustInfo.Rows(0))
                        C1Sizer1.Visible = True
                    End If
                Else
                    ClearCustomerInfo()
                    C1Sizer1.Visible = False
                End If
                Dim dvPInvc As New DataView(dtPInvc, String.Empty, String.Empty, DataViewRowState.CurrentRows)
                dvtInvc = dvPInvc.ToTable(True, "SaleInvNumber")

                CboInvoiceNo.DataSource = dvtInvc
                CboInvoiceNo.ValueMember = dvtInvc.Columns("SaleInvNumber").ColumnName
                CboInvoiceNo.DisplayMember = dvtInvc.Columns("SaleInvNumber").ColumnName
                CboInvoiceNo.SelectedIndex = -1
                CboInvoiceNo.SelectedIndex = 0
                IsLoadedCombo = True

            Else
                ClearCustomerInfo()
                dvtInvc.Rows.Clear()
                CboInvoiceNo.DataSource = dvtInvc

                IsLoadedCombo = True
                grdInvoiceInfo.DataSource = Nothing

                SetGridColumnCaption()
                ShowMessage(getValueByKey("RP02"), "RP02 - " & getValueByKey("CLAE05"))
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function
    Private Sub RefreshInvoiceGrid()
        Try
            'Dim dvGridInfo As New DataView(dtPInvc, "SaleInvNumber='" & CboInvoiceNo.SelectedValue & "'", String.Empty, DataViewRowState.CurrentRows)
            'Dim dtGridInfo As New DataTable
            'dtGridInfo = dvGridInfo.ToTable(False, "TenderTypeCode", "AmountTendered", "SOInvDate")
            'grdInvoiceInfo.DataSource = dtGridInfo
            If clsDefaultConfiguration.IsNewSalesOrder = True Then
                Dim dvGridInfo As New DataView(dtPInvc)
                Dim dtGridInfo As New DataTable
                dtGridInfo = dvGridInfo.ToTable(False, "TenderTypeCode", "AmountTendered", "SOInvDate")
                grdInvoiceInfo.DataSource = dtGridInfo
            Else
                Dim dvGridInfo As New DataView(dtPInvc, "SaleInvNumber='" & CboInvoiceNo.SelectedValue & "'", String.Empty, DataViewRowState.CurrentRows)
                Dim dtGridInfo As New DataTable
                dtGridInfo = dvGridInfo.ToTable(False, "TenderTypeCode", "AmountTendered", "SOInvDate")
                grdInvoiceInfo.DataSource = dtGridInfo
            End If
            SetGridColumnCaption()
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                For i = 0 To grdInvoiceInfo.Cols.Count - 1
                    grdInvoiceInfo.Cols(i).Caption = grdInvoiceInfo.Cols(i).Caption.ToUpper
                Next
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub
    Private Sub SetGridColumnCaption()
        Try
            grdInvoiceInfo.Cols("TenderTypeCode").Caption = getValueByKey("frmnreprint.grdinvoiceinfo.tendertypecode")
            grdInvoiceInfo.Cols("TenderTypeCode").Width = 220

            grdInvoiceInfo.Cols("AmountTendered").Caption = getValueByKey("frmnreprint.grdinvoiceinfo.amounttendered")
            grdInvoiceInfo.Cols("AmountTendered").Width = 200

            grdInvoiceInfo.Cols("SOInvDate").Caption = getValueByKey("frmnreprint.grdinvoiceinfo.soinvdate")
            grdInvoiceInfo.Cols("SOInvDate").Width = 120
        Catch ex As Exception
        End Try
    End Sub
    Private Sub SetCustomerInfo(ByVal drCustmInfo As DataRow)
        Try
            lblCustTypeValue.Text = IIf(drCustmInfo("CustomerType") Is DBNull.Value, String.Empty, drCustmInfo("CustomerType"))
            lblCustNoValue.Text = IIf(drCustmInfo("CustomerNo") Is DBNull.Value, String.Empty, drCustmInfo("CustomerNo"))
            lblCustNameValue.Text = IIf(drCustmInfo("CustomerName") Is DBNull.Value, String.Empty, drCustmInfo("CustomerName"))
            lblAddressValue.Text = IIf(drCustmInfo("Address") Is DBNull.Value, String.Empty, drCustmInfo("Address"))
            lblEmailIdValue.Text = IIf(drCustmInfo("EmailID") Is DBNull.Value, String.Empty, drCustmInfo("EmailID"))
            lblTelNoValue.Text = IIf(drCustmInfo("ResPhone") Is DBNull.Value, String.Empty, drCustmInfo("ResPhone"))

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub
    Private Sub ClearCustomerInfo()
        Try
            lblCustTypeValue.Text = String.Empty
            lblCustNoValue.Text = String.Empty
            lblCustNameValue.Text = String.Empty
            lblAddressValue.Text = String.Empty
            lblEmailIdValue.Text = String.Empty
            lblTelNoValue.Text = String.Empty

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub
    Dim DtSoBulkComboHdr As New DataTable
    Dim DtSoBulkComboDtl As New DataTable
    Dim _dsMain As New DataSet
    Private Sub BtnReprintInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReprintInvoice.Click
        Try
            If clsDefaultConfiguration.PrintFormatNo = "3" OrElse clsDefaultConfiguration.PrintFormatNo = "4" OrElse clsDefaultConfiguration.PrintFormatNo = "5" OrElse clsDefaultConfiguration.PrintFormatNo = "6" OrElse clsDefaultConfiguration.PrintFormatNo = "7" OrElse clsDefaultConfiguration.PrintFormatNo = "8" OrElse clsDefaultConfiguration.PrintFormatNo = "9" OrElse clsDefaultConfiguration.PrintFormatNo = "11" Then
                If clsDefaultConfiguration.IsFinalReceipt.Contains(clsAdmin.TerminalID) Then
                    IsFinalReceipt = True
                Else
                    IsFinalReceipt = False
                End If
            End If
            If String.IsNullOrEmpty(txtReprintReason.Text.Trim) = False Then
                If PrintTransType = "SalesOrder" Then
                    If clsDefaultConfiguration.IsNewSalesOrder = True Then
                        ''added by ketan 
                        SOprint(txtDocNumber.Text.Trim)
                    Else
                        dtSaleItems = New DataTable
                        Dim SalesPersonName As String = String.Empty
                        dtSaleItems = objPrint.GetSaleItems(clsAdmin.SiteCode, clsAdmin.Financialyear, txtDocNumber.Text.Trim, PrintTransType, CboInvoiceNo.SelectedValue)

                        dtPayment = New DataTable
                        dtPayment = objPrint.GetPaymentDetails(clsAdmin.SiteCode, clsAdmin.Financialyear, txtDocNumber.Text.Trim, CboInvoiceNo.SelectedValue)

                        Dim Dstemp = objSO.GetSOBulkComboTableStruct(clsAdmin.SiteCode, IIf(txtDocNumber.Text.Trim = String.Empty, 0, txtDocNumber.Text.Trim))
                        DtSoBulkComboHdr = Dstemp.Tables("SoBulkComboHdr")
                        DtSoBulkComboDtl = Dstemp.Tables("SoBulkComboDtl")
                        If Not IsDBNull(dtSaleItems.Rows(0)("SalesPersonFullName")) Then SalesPersonName = dtSaleItems.Rows(0)("SalesPersonFullName")
                        Dim vSalesOrderDeliveryDate As DateTime = DateTime.Now
                        vSalesOrderDeliveryDate = dtSaleItems.Rows(0)("ActualDeliveryDate")
                        Dim vSalesOrderRemark As String = ""
                        vSalesOrderRemark = (IIf(dtSaleItems.Rows(0)("Remarks") Is DBNull.Value, "", dtSaleItems.Rows(0)("Remarks")))
                        Dim vInvoiceTo As String = ""
                        vInvoiceTo = (IIf(dtSaleItems.Rows(0)("InvoiceTo") Is DBNull.Value, "", dtSaleItems.Rows(0)("InvoiceTo")))
                        Dim SoCreatedOnDate As DateTime
                        Dim strSoStatus As String
                        If dtSaleItems.Rows.Count > 0 Then
                            SoCreatedOnDate = dtSaleItems.Rows(0)("SOCREATEDON")
                            strSoStatus = dtSaleItems.Rows(0)("SoStatus")
                        End If
                        Dim paymentDate As DateTime
                        If dtPayment.Rows.Count > 0 Then
                            paymentDate = dtPayment.Rows(0)("CREATEDON")
                        End If
                        Dim dsOtherCharges As New DataSet
                        dsOtherCharges.Clear()
                        Dim dt = objCharge.GetDtOtherCharge(clsAdmin.SiteCode, IIf(txtDocNumber.Text.Trim = String.Empty, 0, txtDocNumber.Text.Trim))
                        dsOtherCharges.Tables.Add(dt.Copy())
                        dsOtherCharges.Tables(0).TableName = "NewOtherCharges"
                        Dim vSalesOrderExpectedDeliveryDate As DateTime

                        '=================================================================================================================================

                        Dim salesordernumber As String = txtDocNumber.Text.Trim

                        If clsDefaultConfiguration.PrintFormatNo = "8" Then
                            'code added by irfan for cakeology on 4/10/2017
                            Dim objt As New clsCashMemoPrint(salesordernumber, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving)
                            objt.PrintFormatNo = clsDefaultConfiguration.PrintFormatNo
                            objt.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "SO", "", "", "", "", "", "", "", clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6)   'code added by irfan on 9/9/2017 visiblity of hsn and tax
                            '------------------------------------------------------------------------------------------------

                        Else

                            Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(clsDefaultConfiguration.SOPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired,
                                                    SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Payment, clsAdmin.SiteCode,
                                                    clsAdmin.CurrencyCode, clsAdmin.UserName, txtDocNumber.Text.Trim, dtCustInfo, dtSaleItems, dtPayment, String.Empty,
                                                    CboInvoiceNo.SelectedValue, String.Empty, Nothing, Nothing, dtPrinterInfo, strSoStatus,
                                                    dsOtherCharges, vSalesOrderRemark, ShowFullName:=clsDefaultConfiguration.PrintItemFullName,
                                                    dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, SalesOrderDeliveryDate:=vSalesOrderDeliveryDate, strSalesPerson:=SalesPersonName, SalesPaymentDate:=paymentDate, SalesOrderCreationDate:=SoCreatedOnDate, strInvoiceTo:=vInvoiceTo)

                        End If
                    End If

                    objPrint.UpdateReprintStatus(clsAdmin.SiteCode, clsAdmin.Financialyear, txtDocNumber.Text.Trim, PrintTransType, txtReprintReason.Text.Trim)

                ElseIf PrintTransType = "BirthList" Then
                    dtSaleItems = New DataTable
                    dtSaleItems = objPrint.GetSaleItems(clsAdmin.SiteCode, clsAdmin.Financialyear, txtDocNumber.Text.Trim, PrintTransType, CboInvoiceNo.SelectedValue)

                    dtPayment = New DataTable
                    dtPayment = objPrint.GetPaymentDetails(clsAdmin.SiteCode, clsAdmin.Financialyear, txtDocNumber.Text.Trim, CboInvoiceNo.SelectedValue)

                    Dim printingDll As New SpectrumPrint.clsBirthListNew(clsDefaultConfiguration.BLPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, PrintBirthList.PrintTransactionSet.SaleBirthListItem, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, txtDocNumber.Text.Trim, dtCustInfo, dtSaleItems, CboInvoiceNo.SelectedValue, txtDocNumber.Text.Trim, Nothing, Nothing, Nothing, True, "", "", "", 0, clsDefaultConfiguration.BillRoundOffAt, dtPrinterInfo, "", Nothing, clsAdmin.TerminalID, ShowFullName:=clsDefaultConfiguration.PrintItemFullName)
                    'objPrint.UpdateReprintStatus(clsAdmin.SiteCode, clsAdmin.Financialyear, txtDocNumber.Text.Trim, PrintTransType, txtReprintReason.Text.Trim)


                ElseIf PrintTransType = "CashMemo" Or PrintTransType = "ReturnCashMemo" Then
                    Dim clsDefault As New clsDefaultConfiguration("CMS")
                    clsDefault.GetDefaultSettings()

                    Dim objCMPrint As New clsCashMemoPrint(txtDocNumber.Text.Trim, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving) '0000413
                    objCMPrint.DisplayArticleFullName = clsDefaultConfiguration.PrintItemFullName

                    objCMPrint.AllowDecimalQty = clsDefaultConfiguration.AllowDecimalQty
                    objCMPrint.WeightScaleEnabled = clsDefaultConfiguration.WeightScaleEnabled

                    objCMPrint.TokenNoRequiredInKOT = clsDefaultConfiguration.TokenNoRequiredInKOT
                    objCMPrint.CashMemoResetonDayClose = clsDefaultConfiguration.CashMemoResetonDayClose
                    objCMPrint.AllowBillingOnlyAfterSelectionOfSalesType = clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType
                    objCMPrint.PrintFormatNo = clsDefaultConfiguration.PrintFormatNo
                    objCMPrint.IsRoundRequired = clsDefaultConfiguration.RoundOffRequired
                    objCMPrint.DisplayBrandWiseSale = clsDefaultConfiguration.DisplayBrandWiseSale
                    Dim ErrorMsg As String = ""
                    clsCashMemo.dsCashMemoPrinting = Nothing
                    '--- Changes by mahesh before reprint updated reprint count (same as Cash Memo)
                    objPrint.UpdateReprintStatus(clsAdmin.SiteCode, clsAdmin.Financialyear, txtDocNumber.Text.Trim, PrintTransType, txtReprintReason.Text.Trim)

                    'Rakesh:09-July-2013-->Start: Template based cashmemo bill printing
                    If (clsDefaultConfiguration.TemplatePrintingAllowed) Then
                        objCMPrint.PrintTemplateCashMemoBillDetails(txtDocNumber.Text.Trim, clsAdmin.SiteCode, clsAdmin.CurrencyCode, String.Empty, Nothing, IsBillReprint:=True, ReprintReason:=txtReprintReason.Text.Trim)
                    Else
                        ' objCMPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "Duplicate", "", "", ErrorMsg) '0000413
                        objCMPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "Duplicate", "", "", ErrorMsg, "", IsFinalReceipt:=IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsCounterCopyKot:=True, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6, JKPrintFormatEnable:=clsDefaultConfiguration.JKPrintFormatEnable) '0000413    code added by irfan on 18/9/2017 visiblity of hsn and tax 
                    End If
                    'Rakesh:09-July-2013-->End: Template based cashmemo bill printing

                    If ErrorMsg <> String.Empty Then
                        ShowMessage(ErrorMsg, getValueByKey("CLAE05"))
                    End If



                End If

                    ClearAll()

                    AutoLogout()

                    '  ShowMessage(getValueByKey("RP03"), "RP03 - " & getValueByKey("CLAE04"))
                Else
                    ShowMessage(getValueByKey("RP04"), "RP04 - " & getValueByKey("CLAE04"))
                End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    ''added by nikhil
    Public Function GenerateFinancialReportPrint(ByVal dsfinancial As DataSet, ByVal siteCode As String, ByVal dayCloseDate As Date, ByVal TerminalId As String, ByVal shiftId As Integer) As Boolean
        Try
            'Dim objComn As New clsCommon
            ' Dim objprint As New clsPrintDenomination(clsDefaultConfiguration.TillClosePrintPreivewReq)
            If dsfinancial.Tables.Count > 0 Then

                Dim reportViewer2 As New LocalReport
                Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\FinancialReport.rdl")
                reportViewer2.ReportPath = appPath
                'reportViewer2.ProcessingMode = ProcessingMode.Local
                reportViewer2.DataSources.Clear()

                reportViewer2.SetParameters(New ReportParameter("V_SiteCode", siteCode))
                reportViewer2.SetParameters(New ReportParameter("V_DayCloseDate", dayCloseDate.ToShortDateString))
                reportViewer2.SetParameters(New ReportParameter("V_TerminalId", TerminalId))
                reportViewer2.SetParameters(New ReportParameter("V_ShiftId", shiftId))
                reportViewer2.SetParameters(New ReportParameter("P_ShiftOpenDateTime", dayCloseDate))
                reportViewer2.SetParameters(New ReportParameter("P_ShiftCloseDateTime", dayCloseDate))

                Dim DataSource As New ReportDataSource("FinancialBalance", dsfinancial.Tables(0))
                Dim DataSource1 As New ReportDataSource("NetCashData", dsfinancial.Tables(1))
                Dim DataSource2 As New ReportDataSource("NetSaleData", dsfinancial.Tables(2))
                Dim DataSource3 As New ReportDataSource("CloseSummaryData", dsfinancial.Tables(3))
                Dim DataSource4 As New ReportDataSource("WriteOFF", dsfinancial.Tables(4))
                Dim DataSource5 As New ReportDataSource("NetSaleCreditSale", dsfinancial.Tables(5))
                reportViewer2.DataSources.Add(DataSource)
                reportViewer2.DataSources.Add(DataSource1)
                reportViewer2.DataSources.Add(DataSource2)
                reportViewer2.DataSources.Add(DataSource3)
                reportViewer2.DataSources.Add(DataSource4)
                reportViewer2.DataSources.Add(DataSource5)
                reportViewer2.Refresh()
                Dim objCMPrintData As New clsCashMemoPrint(txtDocNumber.Text.Trim, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving) '0000413
                Dim mybytes As [Byte]() = reportViewer2.Render("Pdf")
                'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
                path = clsDefaultConfiguration.DayCloseReportPath & "\FinancialReport_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                Using fs As FileStream = File.Create(path)
                    fs.Write(mybytes, 0, mybytes.Length)
                End Using

                If clsDefaultConfiguration.TillClosePrintPreivewReq = True Then
                    Process.Start(path)
                Else

                    ' Export(reportViewer2)
                    ExportFin(reportViewer2)
                    Print()

                    ' Code For Print SO
                    'PrinterName = SetPrinterName(dtPrinterInfo, "SalesOrder", "SOInvc")
                    'Dim pdfdocument As New PdfDocument()
                    'pdfdocument.LoadFromFile(path)
                    'pdfdocument.PrinterName = PrinterName
                    'pdfdocument.PrintDocument.PrinterSettings.Copies = 1
                    'pdfdocument.PrintDocument.Print()
                    'pdfdocument.Dispose()
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Public Sub ExportFin(ByVal report As LocalReport)
        Try
            Dim deviceInfo As String = "<DeviceInfo>" & _
               "<OutputFormat>EMF</OutputFormat>" & _
               "<PageWidth>3.5in</PageWidth>" & _
               "<PageHeight>11in</PageHeight>" & _
               "<MarginTop>0.01in</MarginTop>" & _
               "<MarginLeft>0.10in</MarginLeft>" & _
               "<MarginRight>0in</MarginRight>" & _
               "<MarginBottom>0in</MarginBottom>" & _
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

    ''added by nikhil
    Public Function GenerateCardSummaryReportPrint(ByVal dsSummry As DataSet, ByVal siteCode As String, ByVal dayCloseDate As Date, ByVal TerminalId As String, ByVal shiftId As Integer) As Boolean
        Try

            If dsSummry.Tables.Count > 0 Then
                dsSummry.Tables(1).Rows(1)(0) = "Spectrum Card Summary"
                Dim reportViewer2 As New LocalReport
                Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\CardSummaryDetails.rdl")
                reportViewer2.ReportPath = appPath
                'reportViewer2.ProcessingMode = ProcessingMode.Local
                reportViewer2.DataSources.Clear()

                reportViewer2.SetParameters(New ReportParameter("V_SiteCode", siteCode))
                reportViewer2.SetParameters(New ReportParameter("V_DayCloseDate", dayCloseDate.ToShortDateString))
                reportViewer2.SetParameters(New ReportParameter("V_TerminalId", TerminalId))
                reportViewer2.SetParameters(New ReportParameter("V_ShiftId", shiftId))
                reportViewer2.SetParameters(New ReportParameter("P_ShiftOpenDateTime", dayCloseDate))
                reportViewer2.SetParameters(New ReportParameter("P_ShiftCloseDateTime", dayCloseDate))

                Dim DataSource1 As New ReportDataSource("dsHeader", dsSummry.Tables(0))
                Dim DataSource As New ReportDataSource("dsCardSummary", dsSummry.Tables(1))

                reportViewer2.DataSources.Add(DataSource)
                reportViewer2.DataSources.Add(DataSource1)

                reportViewer2.Refresh()
                Dim mybytes As [Byte]() = reportViewer2.Render("Pdf")
                'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
                path = clsDefaultConfiguration.DayCloseReportPath & "\CardSummaryDetails_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                Using fs As FileStream = File.Create(path)
                    fs.Write(mybytes, 0, mybytes.Length)
                End Using

                If clsDefaultConfiguration.TillClosePrintPreivewReq = True Then
                    Process.Start(path)
                Else

                    Export(reportViewer2)
                    Print()

                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    '' added by nikhil
    Private m_streams As IList(Of Stream)
    Private Function CreateStream(ByVal name As String, ByVal fileNameExtension As String, ByVal encoding As Encoding, ByVal mimeType As String, ByVal willSeek As Boolean) As Stream
        Try
            Dim stream As Stream = New MemoryStream()
            m_streams.Add(stream)
            Return stream
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Public Sub Export(ByVal report As LocalReport)
        Try
            Dim deviceInfo As String = "<DeviceInfo>" & _
                "<OutputFormat>EMF</OutputFormat>" & _
               "<PageWidth>3.79in</PageWidth>" & _
               "<PageHeight>11in</PageHeight>" & _
               "<MarginTop>0.01in</MarginTop>" & _
               "<MarginLeft>0.25in</MarginLeft>" & _
               "<MarginRight>0in</MarginRight>" & _
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
    Private m_currentPageIndex As Integer
    Public Sub Print()
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
                printDoc.PrinterSettings.PrinterName = SetPrinterName(dtPrinterInfo, "TillCloseFinReport", "")
                printDoc.Print()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

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

    ''ended by nikhil
    Protected Overrides Function ProcessKeyPreview(ByRef m As System.Windows.Forms.Message) As Boolean
        Const WM_KEYDOWN As Integer = &H100
        If m.Msg = WM_KEYDOWN Then
            Select Case m.WParam.ToInt32
                Case Keys.D
                    If My.Computer.Keyboard.CtrlKeyDown Then
                        'Debug.Print("Ctrl + F")
                        BtnSearchInvoice_Click(BtnSearchInvoice, New KeyEventArgs(Keys.Enter))
                    End If
            End Select
        End If
        Return MyBase.ProcessKeyPreview(m)
    End Function

    Private Sub frmNReprint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetCulture(Me, Me.Name)
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
        End If
        Dim objdefaultSO As New clsDefaultConfiguration("SalesOrder")
        objdefaultSO.GetDefaultSettings()

        Dim objdefaultCM As New clsDefaultConfiguration("CMS")
        objdefaultCM.GetDefaultSettings()

        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Private Sub ClearAll()
        ClearCustomerInfo()
        grdInvoiceInfo.DataSource = Nothing
        txtDocNumber.Text = String.Empty
        txtReprintReason.Text = String.Empty
        CboInvoiceNo.DataSource = Nothing
        For i = 1 To grdInvoiceInfo.Rows.Count - 1
            grdInvoiceInfo.Rows.Remove(grdInvoiceInfo.Rows.Count - 1)
        Next
    End Sub

    Private Sub frmNReprint_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "reprint.htm")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        grdInvoiceInfo.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        grdInvoiceInfo.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        grdInvoiceInfo.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        grdInvoiceInfo.Rows.MinSize = 25
        grdInvoiceInfo.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        grdInvoiceInfo.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdInvoiceInfo.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdInvoiceInfo.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdInvoiceInfo.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdInvoiceInfo.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        BtnReprintInvoice.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnReprintInvoice.BackColor = Color.Transparent
        BtnReprintInvoice.BackColor = Color.FromArgb(0, 107, 163)
        BtnReprintInvoice.ForeColor = Color.FromArgb(255, 255, 255)
        BtnReprintInvoice.Size = New Size(124, 32)
        BtnReprintInvoice.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        BtnReprintInvoice.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnReprintInvoice.FlatStyle = FlatStyle.Flat
        BtnReprintInvoice.FlatAppearance.BorderSize = 0
        BtnReprintInvoice.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        BtnReprintInvoice.TextAlign = ContentAlignment.MiddleCenter
        BtnSearchInvoice.Image = Nothing
        BtnSearchInvoice.VisualStyle = C1.Win.C1Input.VisualStyle.System
        BtnSearchInvoice.BackgroundImage = My.Resources.SearchItems1
        BtnSearchInvoice.FlatStyle = FlatStyle.Flat
        BtnSearchInvoice.BackgroundImageLayout = ImageLayout.Stretch
        BtnSearchInvoice.Size = New Size(40, 21)
        CtrlLabel3.Size = New Size(110, 21)
        CtrlLabel3.Location = New Point(15, 14)
        CtrlLabel3.BackColor = Color.FromArgb(212, 212, 212)

        CtrlLabel4.Size = New Size(110, 21)
        CtrlLabel4.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel4.Location = New Point(15, 49)
        CtrlLabel2.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel2.Size = New Size(110, 49)
        For i = 0 To grdInvoiceInfo.Cols.Count - 1
            grdInvoiceInfo.Cols(i).Caption = grdInvoiceInfo.Cols(i).Caption.ToUpper
        Next
    End Function
    Public Function GenerateCreditSaleSettledReportPrint(ByVal siteCode As String, ByVal Totamt As String, ByVal CollectedAmt As String, ByVal tender As String, ByVal Remarks As String, ByVal AmtInRs As String, ByVal TerminalId As String, ByVal InvoiceNumber As String, ByVal cashier As String, ByVal dtHeader As DataTable, Optional billNo As String = "", Optional ViaWriteoff As Boolean = False) As Boolean
        Try 'vipin Credit Sale PC merge
            'Dim objComn As New clsCommon
            ' Dim objprint As New clsPrintDenomination(clsDefaultConfiguration.TillClosePrintPreivewReq)
            If dtHeader.Rows.Count > 0 Then

                Dim reportViewer2 As New LocalReport
                '  Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\CreditSaleSettledReport.rdl")
                Dim appPath As String
                If ViaWriteoff = False Then
                    appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\CreditSaleSettledReport.rdl")
                Else
                    appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\CreditSaleSettledWriteOffReport.rdl")
                End If
                reportViewer2.ReportPath = appPath
                Dim time = DateTime.Now.ToString("HH:mm")
                Dim Cureentdate = DateTime.Now.ToString("dd MMM yyyy")
                'reportViewer2.ProcessingMode = ProcessingMode.Local
                reportViewer2.DataSources.Clear()
                reportViewer2.SetParameters(New ReportParameter("V_SiteCode", siteCode))
                reportViewer2.SetParameters(New ReportParameter("V_TotAmt", Totamt))
                reportViewer2.SetParameters(New ReportParameter("V_CollectAmt", CollectedAmt))
                reportViewer2.SetParameters(New ReportParameter("V_Date", Cureentdate))
                reportViewer2.SetParameters(New ReportParameter("V_Tender", tender))
                If InvoiceNumber <> "" Then
                    reportViewer2.SetParameters(New ReportParameter("V_DocumentNo", InvoiceNumber))
                Else
                    reportViewer2.SetParameters(New ReportParameter("V_DocumentNo", billNo))
                End If

                reportViewer2.SetParameters(New ReportParameter("V_TillNo", TerminalId))
                reportViewer2.SetParameters(New ReportParameter("V_Time", time))
                reportViewer2.SetParameters(New ReportParameter("V_Cashier", cashier))
                reportViewer2.SetParameters(New ReportParameter("V_AmtInRs", AmtInRs))
                reportViewer2.SetParameters(New ReportParameter("V_Remarks", Remarks))
                reportViewer2.SetParameters(New ReportParameter("V_BillNo", billNo))

                Dim DataSource As New ReportDataSource("dsHeader", dtHeader)
                reportViewer2.DataSources.Add(DataSource)
                reportViewer2.Refresh()
                Dim objCMPrintData As New clsCashMemoPrint(txtDocNumber.Text.Trim, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving) '0000413
                Dim mybytes As [Byte]() = reportViewer2.Render("Pdf")
                'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
                If ViaWriteoff Then
                    If tender = "Discount" Then
                        path = clsDefaultConfiguration.DayCloseReportPath & "\CreditSaleDiscount_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                    Else
                        path = clsDefaultConfiguration.DayCloseReportPath & "\CreditSaleWriteOff_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                    End If
                Else
                    path = clsDefaultConfiguration.DayCloseReportPath & "\CreditSaleSettled_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                End If
                Using fs As FileStream = File.Create(path)
                    fs.Write(mybytes, 0, mybytes.Length)
                End Using

                ' If clsDefaultConfiguration.PrintPreivewReq = True Then
                Process.Start(path)
                'Else
                ' ExportForCreditSale(reportViewer2)  ''Export
                ' Print()
                ' End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
#Region "SO PRINT"
    Dim drSOPrintHeader As DataRow
    Dim dtHeaderDetails As DataTable
    Dim DocumentNo As String
    Dim DtSoBulkRemarks As New DataTable
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
    Dim dtOrderAddresses As DataTable
    Dim dsPackagingDelivery As DataSet
    Dim dtCustmInfo As New DataTable
    Dim _pickupHistory As New DataSet
    Dim dtPackagingPrintBox As New DataTable
    Dim objcomm As New clsCommon
    Dim BarCodestring As String
    Dim path As String = ""
    Dim dsInv As New DataSet
    Dim ProgramID As String
    Dim dsSOInvoice As New DataSet
    Dim objCustm As New clsCLPCustomer

    Dim _dsPackagingVar As New DataSet 'vipin PC SO Merge 03-05-2018
    Dim dtArticleWisePaymentDetails As DataTable
    Dim dtReturnOrderComboDtl As DataTable
    Dim DtCustDtlForSOPrint As DataTable ' vipin
    Dim DtComboGridData As DataTable 'vipin
    Public Sub SOprint(DocumentNumber As String)
        If clsDefaultConfiguration.IsNewSalesOrder = True Then
            Dim clsPCCommon As New clsSalesOrderPC
            Dim ObjclsCommon As New clsCommon
            Dim ClientName As String = clsDefaultConfiguration.ClientName
            Dim TerminalID As String = clsAdmin.TerminalID
            Dim UserName As String = clsAdmin.UserName
            Dim dtSiteInfo As DataTable = objcomm.GetSiteInfo(clsAdmin.SiteCode)
            DocumentNo = DocumentNumber
            dsSOInvoice = objPCSO.GetSOTableStruct(clsAdmin.SiteCode, DocumentNo)
            Dim CUSTNo As String
            If dsSOInvoice.Tables("SalesOrderHDR").Rows.Count > 0 Then
                CUSTNo = dsSOInvoice.Tables("SalesOrderHDR").Rows(0)("CustomerNo")
            End If
            _dsPackagingVar = objPCSO.SetSalesOrderPackVariationInSO(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
            dtCustmInfo = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, CUSTNo, CustFormat:=clsDefaultConfiguration.DetailedCustomerCreationformat, IsNewSalesOrder:=clsDefaultConfiguration.IsNewSalesOrder)
            dsPackagingDelivery = objPCSO.SetSalesOrderDeliveryInSO(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
            dtPackagingPrintBox = ObjclsCommon.GetPackagingBox(clsAdmin.SiteCode, 2)
            dsInv = objPCSO.SetInvoiceInSOCancel(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
            ProgramID = objPCSO.GetCLPProgramID(clsAdmin.SiteCode)
            dtOrderAddresses = objcomm.GetSOAddresses(CUSTNo, ProgramID, True)
            dtStrPrint = objPCSO.GetSalesOrderSTRPrint(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
            _pickupHistory = objPCSO.GetSalesOrderPickupHistory(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))

            dtReturnOrderComboDtl = objPCSO.SOPrintReturnOrderComboDetails() '' added by ketan SO return Chnages 
            dtHeaderDetails = objPCSO.SOPrintHeader(dtSiteInfo, dtCustmInfo, DocumentNo, dsSOInvoice, ClientName, TerminalID, UserName)
            dtOrderDetails = objPCSO.SOPrintOrderDetails(dsSOInvoice, dsPackagingDelivery, clsDefaultConfiguration.PackageFiedlsAllowed, dtPackagingPrintBox)
            dtOrderComboDetails = objPCSO.SOPrintOrderComboDetails(dsSOInvoice, True)
            dtPaymentDetails1 = objPCSO.SOPrintPaymentDetails1(dsSOInvoice, DocumentNo, clsAdmin.SiteCode, dsInv)
            dtPaymentDetails = objPCSO.SOPrintPaymentDetails(dsInv, clsAdmin.SiteCode, DocumentNo)
            dtDeliveryDetails = objPCSO.SOPrintDeliveryDetails(dsSOInvoice, clsDefaultConfiguration.PackageFiedlsAllowed, dtPackagingPrintBox, dtOrderAddresses)
            dtStrDetails = objPCSO.SOStrDetails(dtStrPrint)
            dtAddress = objPCSO.GetSOPrintAddress(dsSOInvoice, dtOrderAddresses)
            dtRemark = objPCSO.SOPrintRemarks(dsSOInvoice)
            dtPickupHistory = objPCSO.SOPrintPickupHistory(dsSOInvoice, _pickupHistory, dtPackagingPrintBox, clsDefaultConfiguration.PackageFiedlsAllowed)
            dtArticleWisePaymentDetails = objPCSO.soprintarticlepaymentwisedetails(dsSOInvoice, dsPackagingDelivery, _dsPackagingVar) ' by ketan
            DtCustDtlForSOPrint = objcomm.GetCustDetailForSoPrint(dtCustmInfo.Rows(0)("CustomerNo").ToString()) 'vipin
            BarCodestring = ImageToBase64(DocumentNo)
            'GenerateSoDeliveryPrint()
            GenerateSOPrint()
            ' GenerateOrderPreparationPrint()
            GenerateOrderPreparationAsPerDeliveryDetails(dtDeliveryDetails)
        Else
            Dim clsPCCommon As New clsSalesOrderPC
            Dim ObjclsCommon As New clsCommon
            Dim ClientName As String = clsDefaultConfiguration.ClientName
            Dim TerminalID As String = clsAdmin.TerminalID
            Dim UserName As String = clsAdmin.UserName
            Dim dtSiteInfo As DataTable = objcomm.GetSiteInfo(clsAdmin.SiteCode)
            DocumentNo = DocumentNumber
            dsSOInvoice = objPCSO.GetSOTableStruct(clsAdmin.SiteCode, DocumentNo)
            Dim CUSTNo As String
            If dsSOInvoice.Tables("SalesOrderHDR").Rows.Count > 0 Then
                CUSTNo = dsSOInvoice.Tables("SalesOrderHDR").Rows(0)("CustomerNo")
            End If
            dtCustmInfo = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, CUSTNo, CustFormat:=clsDefaultConfiguration.DetailedCustomerCreationformat, IsNewSalesOrder:=clsDefaultConfiguration.IsNewSalesOrder)
            dsPackagingDelivery = objPCSO.SetSalesOrderDeliveryInSO(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
            dtPackagingPrintBox = ObjclsCommon.GetPackagingBox(clsAdmin.SiteCode, 2)
            dsInv = objPCSO.SetInvoiceInSOCancel(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
            ProgramID = objPCSO.GetCLPProgramID(clsAdmin.SiteCode)
            dtOrderAddresses = objcomm.GetSOAddresses(CUSTNo, ProgramID, True)
            dtStrPrint = objPCSO.GetSalesOrderSTRPrint(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
            _pickupHistory = objPCSO.GetSalesOrderPickupHistory(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))

            dtHeaderDetails = objPCSO.SOPrintHeader(dtSiteInfo, dtCustmInfo, DocumentNo, dsSOInvoice, ClientName, TerminalID, UserName)
            dtOrderDetails = objPCSO.SOPrintOrderDetails(dsSOInvoice, dsPackagingDelivery, clsDefaultConfiguration.PackageFiedlsAllowed, dtPackagingPrintBox)
            dtOrderComboDetails = objPCSO.SOPrintOrderComboDetails(dsSOInvoice)
            dtPaymentDetails1 = objPCSO.SOPrintPaymentDetails1(dsSOInvoice, DocumentNo, clsAdmin.SiteCode, dsInv)
            dtPaymentDetails = objPCSO.SOPrintPaymentDetails(dsInv, clsAdmin.SiteCode, DocumentNo)
            dtDeliveryDetails = objPCSO.SOPrintDeliveryDetails(dsSOInvoice, clsDefaultConfiguration.PackageFiedlsAllowed, dtPackagingPrintBox, dtOrderAddresses)
            dtStrDetails = objPCSO.SOStrDetails(dtStrPrint)
            dtAddress = objPCSO.GetSOPrintAddress(dsSOInvoice, dtOrderAddresses)
            dtRemark = objPCSO.SOPrintRemarks(dsSOInvoice)
            dtPickupHistory = objPCSO.SOPrintPickupHistory(dsSOInvoice, _pickupHistory, dtPackagingPrintBox, clsDefaultConfiguration.PackageFiedlsAllowed)

            BarCodestring = ImageToBase64(DocumentNo)
            'GenerateSoDeliveryPrint()
            GenerateSOPrint()
            GenerateOrderPreparationPrint()
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

                    '  dvDeliveryDateNEW.RowFilter = "DeliveryTime='" & dr("DeliveryTime").ToString & "'"
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
    Dim clsSOPC As New clsSalesOrderPC
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
            'path = clsDefaultConfiguration.DayCloseReportPath & "\SOPreparationPrint_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm") & ".pdf"
            Dim Newpath As String = ""
            Dim SOPath As String = clsDefaultConfiguration.DayCloseReportPath 'NEWdtDeliveryDetails, clsSOPC
            If consolidated = True Then
                Newpath = clsSOPC.CreatePathForPrint(dtHeaderDetails, NEWdtDeliveryDetails, SOPath, "Consolidated-Order_prep", consolidated, True)
            Else
                Newpath = clsSOPC.CreatePathForPrint(dtHeaderDetails, NEWdtDeliveryDetails, SOPath, "Order_prep" & PrintID & "", consolidated, True)
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
    Private Function GenerateSOPrint() As Boolean
        Try
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
            ' path = clsDefaultConfiguration.DayCloseReportPath & "\SOInvoice_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm") & ".pdf"
            Dim Newpath As String = ""
            Dim SOPath As String = clsDefaultConfiguration.DayCloseReportPath 'NEWdtDeliveryDetails, clsSOPC
            Newpath = clsSOPC.CreatePathForPrint(dtHeaderDetails, NEWdtDeliveryDetails, SOPath, "SOInvoice", False, False)
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
#End Region

End Class