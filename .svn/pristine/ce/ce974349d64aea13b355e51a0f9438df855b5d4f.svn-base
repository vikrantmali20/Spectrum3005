Imports System.Data
Imports System.Resources
Imports SpectrumBL
Imports System.Globalization
Imports System.Data.SqlClient
Imports SpectrumPrint
Imports SpectrumCommon
Public Class frmShiftFinancialReport
    Dim dtCash, dtOther, dtDiscount, dtmain As DataTable
    Dim dvPayment, dvIssue As DataView
    Dim ShiftOpenValue, VendorAmount As Decimal
    Dim ObjclsCommon As clsCommon
    Dim currentshiftid As Integer
    Dim createdon As DateTime
    Dim TotalDiscount As String
    Dim TotalRoundoff As String
    Dim dsFinancial As New DataSet
    Dim dtTemp As DataTable
    Dim dt As New DataTable
    Dim objShifts As New clsShift()

    Private Sub frmShiftFinancialReport_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            VendorAmount = 0
            ObjclsCommon = New clsCommon
            Dim objShift As New clsShift()
            ''-- PettyCashApplicable condition is commented By Mahesh as per Rama Suggestion date 20-05-2014
            'If clsDefaultConfiguration.IsPettyCashApplicable AndAlso clsDefaultConfiguration.IsPettyCashOnSameTerminal AndAlso clsAdmin.TerminalID = clsDefaultConfiguration.PettyCashTerminalId Then
            '    Dim objPettyCash As IPettyCashVoucher = New PettyCashVoucher()
            '    Dim reqest As New GetVoucherBalanceRequest
            '    reqest.SiteCode = clsAdmin.SiteCode
            '    reqest.FinYear = clsAdmin.Financialyear
            '    reqest.DayOpenDate = clsAdmin.DayOpenDate
            '    TillOpenValue = objPettyCash.GetPettyCashOpeningBalance(reqest)
            'Else
            Dim dt As New DataTable
            dt = clsCommon.GetPreviousShiftDetails(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID)
            If dt.Rows.Count > 0 Then
                If String.IsNullOrEmpty(dt.Rows(0)("ShiftId").ToString()) Then
                    ShowMessage("Shift not Open", "Information")
                    Exit Sub
                End If
                currentshiftid = dt.Rows(0)("ShiftId").ToString()
                createdon = dt.Rows(0)("CREATEDON")
            End If
            ShiftOpenValue = objShift.GetShiftOpenDetail(clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.DayOpenDate, currentshiftid)
            ' End If
            '----------------
            dtCash = ObjclsCommon.GetShiftCashDetail(clsAdmin.TerminalID, clsAdmin.SiteCode, clsAdmin.DayOpenDate, createdon)
            If Not dtCash Is Nothing Then
                BuildCashDetails(dtCash, ShiftOpenValue)
                lblTotalCash.Text = FormatNumber(IIf(dtCash.Compute("Sum(Total)", "") Is DBNull.Value, 0, dtCash.Compute("Sum(Total)", "")), 2)
            End If
            dtOther = ObjclsCommon.GetShiftOtherDetail(clsAdmin.TerminalID, clsAdmin.SiteCode, clsAdmin.DayOpenDate, createdon)
            Dim dtGift As DataTable = ObjclsCommon.GetShiftGiftVoucherIssue(clsAdmin.TerminalID, clsAdmin.SiteCode, clsAdmin.DayOpenDate, createdon)
            If Not dtGift Is Nothing AndAlso dtGift.Rows.Count > 0 AndAlso Not IsDBNull(dtGift.Rows(0)("AMOUNTTENDERED")) Then
                dtOther.Merge(dtGift, False, MissingSchemaAction.Ignore)
            End If

            TotalDiscount = ObjclsCommon.GetShiftWiseDisc(clsAdmin.SiteCode, clsAdmin.DayOpenDate, createdon, clsAdmin.TerminalID)
            TotalRoundoff = ObjclsCommon.GetShiftWiseRoundOff(clsAdmin.SiteCode, clsAdmin.DayOpenDate, createdon, clsAdmin.TerminalID)

            'dtDiscount = ObjclsCommon.GetShiftWiseDiscount(clsAdmin.SiteCode, clsAdmin.DayOpenDate, createdon, clsAdmin.TerminalID)
            'If dtDiscount.Rows.Count > 0 Then
            '    TotalDiscount = FormatNumber(IIf(dtDiscount.Compute("Sum([Promotion Value])", "") Is DBNull.Value, 0, dtDiscount.Compute("Sum([Promotion Value])", "")), 0)
            'End If
            'Changed By Sameer for Issue ID 6768 4-5-2013
            If clsDefaultConfiguration.IsPettyCashApplicable AndAlso clsDefaultConfiguration.IsPettyCashOnSameTerminal AndAlso clsAdmin.TerminalID = clsDefaultConfiguration.PettyCashTerminalId Then
                Dim request As New SpectrumCommon.GetVoucherBalanceRequest()
                request.DayOpenDate = clsAdmin.DayOpenDate
                request.FinYear = clsAdmin.Financialyear
                request.SiteCode = clsAdmin.SiteCode
                request.CreatedOn = createdon
                Dim voucher As IPettyCashVoucher = New PettyCashVoucher()
                lblPettyCashRecAmt.Text = voucher.GetTotalPettyCashReceiptShiftWise(request).ToString()
                lblPettyCashExpAmt.Text = voucher.GetTotalPettyCashExpenseShiftWise(request).ToString()

                If (dtCash IsNot Nothing AndAlso dtCash.Rows.Count > 0) Then
                    dtCash.Rows(0)("Amount") = Val(dtCash.Rows(0)("Amount")) + Val(lblPettyCashRecAmt.Text) - Val(lblPettyCashExpAmt.Text)
                    dtCash.Rows(0)("Total") = Val(dtCash.Rows(0)("Total")) + Val(lblPettyCashRecAmt.Text) - Val(lblPettyCashExpAmt.Text)
                End If
            End If
            Dim Creditsale As ICreditSale = New ClsCreditSale()

            '----Commented By Mahesh  Getting All Data for day and then do Sum and assign sum 
            'Dim creditsaleamt = Creditsale.GetTotalCreditSalebyDate(clsAdmin.DayOpenDate)
            Dim dtcreditsaleAdjustments = Creditsale.GetTotalShiftCreditSaleAdjustmentbyDate(clsAdmin.DayOpenDate, clsAdmin.TerminalID, createdon)
            Dim creditsaleamt = dtcreditsaleAdjustments.Compute("SUM(AMOUNTTENDERED)", "").ToString()
            If dtcreditsaleAdjustments IsNot Nothing Then
                If dtcreditsaleAdjustments.Rows.Count > 0 Then
                    dtOther.Merge(dtcreditsaleAdjustments, False, MissingSchemaAction.Ignore)
                End If
            End If

            'If creditsaleamt > 0 Then
            lblCreditSale.Visible = True
            lblCreditsaleAmt.Visible = True
            lblCreditsaleAmt.Text = creditsaleamt.ToString()
            'End If
            If Not dtOther Is Nothing Then
                dtmain = dtOther.Copy
            End If
            If Not dtOther Is Nothing Then
                BuildDetails(dtOther)
            End If
            lblFinalCollection.Text = lblTotalCollection.Text
            'Me.Visible = False
            If CheckAuthorisation(clsAdmin.UserCode, "SHW_TILL_SMRY") = False Then
                CmdNext_Click(CmdNext, New EventArgs())
            End If
            Dim objshiftclose As New frmShiftClosing
            If CheckAuthorisation(clsAdmin.UserCode, "SC_CashDetails") = False Then
                cmdDispCash.Enabled = True
                EnableDisableCashDetails(False)
                ' lblFinalCollectio
            Else
                cmdDispCash.Enabled = False
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
        SetCulture(Me, Me.Name)
        lblHeader.Text = String.Format(lblHeader.Text, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.DayOpenDate.ToShortDateString())
    End Sub
    Sub EnableDisableCashDetails(ByVal Value As Boolean)
        For i = 1 To dgCash.Rows.Count - 1
            dgCash.Rows(i).Visible = Value
        Next
        For i = 1 To dgPayments.Rows.Count - 1
            If dgPayments.Rows(i)("TenderType") = "Cash" Then
                dgPayments.Rows(i).Visible = Value
            End If
        Next
        lblTotalCash.Visible = Value
        lbltotalCashCollection.Visible = Value
        lblTotalCashCollectionAmt.Visible = Value
        CtrlLabel4.Visible = Value
        lblTotalCollection.Visible = Value
        lblCreditSale.Visible = Value
        lblCreditsaleAmt.Visible = Value
        If clsDefaultConfiguration.IsPettyCashApplicable AndAlso clsDefaultConfiguration.IsPettyCashOnSameTerminal AndAlso clsAdmin.TerminalID = clsDefaultConfiguration.PettyCashTerminalId Then
            lblPettyCashExp.Visible = Value
            lblPettyCashExpAmt.Visible = Value
            lblPettyCashRec.Visible = Value
            lblPettyCashRecAmt.Visible = Value
        End If

    End Sub
    Private Sub BuildDetails(ByVal dtMain As DataTable)
        Try
            Dim dtTemp As DataTable = dtMain.Clone()
            Dim dv As New DataView(dtMain, "", "", DataViewRowState.CurrentRows)
            Dim dtUnique As DataTable = dv.ToTable(True, "TenderHeadCode", "ISSUED")
            For Each dr As DataRow In dtUnique.Rows
                dv.RowFilter = "TenderHeadCode='" & dr("TenderHeadCode").ToString() & "'  AND ISSUED=" & dr("ISSUED").ToString()
                Dim drNew As DataRow = dtTemp.NewRow()
                drNew("AMOUNTTENDERED") = 0
                For Each drdata As DataRowView In dv
                    drNew("TENDERTYPE") = drdata(0).ToString()
                    drNew("TenderHeadCode") = drdata(1).ToString()
                    drNew("DESCRIPTION") = drdata("DESCRIPTION").ToString()
                    drNew("AMOUNTTENDERED") = drNew("AMOUNTTENDERED") + drdata("AMOUNTTENDERED")
                    drNew("ISSUED") = drdata("ISSUED").ToString()
                Next
                dtTemp.Rows.Add(drNew)
            Next
            Dim drFloat As DataRow = dtTemp.NewRow()
            drFloat("TENDERTYPE") = "Cash"
            drFloat("DESCRIPTION") = "Float"
            drFloat("AMOUNTTENDERED") = ShiftOpenValue
            drFloat("ISSUED") = 0
            dtTemp.Rows.Add(drFloat)
            'If Val(lblPettyCashRecAmt.Text) > 0 Then
            '    Dim drPettyCashReciept As DataRow = dtTemp.NewRow()
            '    drFloat("TENDERTYPE") = "Petty Cash Reciept"
            '    drFloat("DESCRIPTION") = "Petty Cash Reciept"
            '    drFloat("AMOUNTTENDERED") = lblPettyCashRecAmt.Text
            '    drFloat("ISSUED") = 0
            '    dtTemp.Rows.Add(drPettyCashReciept)
            'End If

            dvPayment = New DataView(dtTemp, "ISSUED=0", "TENDERTYPE", DataViewRowState.CurrentRows)
            dgPayments.DataSource = dvPayment
            dvIssue = New DataView(dtTemp, "ISSUED=1", "TENDERTYPE", DataViewRowState.CurrentRows)
            'Changed By Sameer for Issue ID 6768 4-5-2013
            dgIssued.DataSource = dvIssue
            If lblPettyCashExpAmt.Visible AndAlso lblPettyCashRecAmt.Visible Then
                'Changed By Mahesh added Petty Cash Reciept in Grid so no need to add again
                lblTotalCollection.Text = FormatNumber(dtTemp.Compute("Sum(AMOUNTTENDERED)", "ISSUED=0") + (CDec(lblPettyCashRecAmt.Text) - CDec(lblPettyCashExpAmt.Text)), 2)
            Else
                lblTotalCollection.Text = FormatNumber(dtTemp.Compute("Sum(AMOUNTTENDERED)", "ISSUED=0"), 2)
            End If
            '-----Added By Mahesh for Getting Cash Total Collection ...Start
            lblTotalCashCollectionAmt.Text = FormatNumber(dtTemp.Compute("Sum(AMOUNTTENDERED)", "ISSUED=0 AND TENDERTYPE ='Cash' ") + (CDec(lblPettyCashRecAmt.Text) - CDec(lblPettyCashExpAmt.Text)), 2)
            If Val(lblTotalCash.Text) Then
                lblTotalCash.Text = FormatNumber(CDec(lblTotalCash.Text) + (CDec(lblPettyCashRecAmt.Text) - CDec(lblPettyCashExpAmt.Text)), 2)
            End If
            If Val(lblTotalCashCollectionAmt.Text) > 0 Then
                lbltotalCashCollection.Visible = True
                lblTotalCashCollectionAmt.Visible = True
            Else
                lbltotalCashCollection.Visible = False
                lblTotalCashCollectionAmt.Visible = False
            End If
            '-----Added By Mahesh for Getting Cash Total Collection ...End


            'If lblCreditSale.Visible AndAlso lblCreditsaleAmt.Visible Then
            '    lblTotalCollection.Text = CDec(lblTotalCollection.Text) + CDec(lblCreditsaleAmt.Text)
            'End If


            dgPayments.Tree.Column = 0
            dgPayments.SubtotalPosition = C1.Win.C1FlexGrid.SubtotalPositionEnum.BelowData
            'dgPayments.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 1, 0, 3, "Sub Totals")
            If CheckAuthorisation(clsAdmin.UserCode, "SC_CashDetails") = True Then
                dgPayments.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 1, 0, 3, "Sub Totals")
                dgPayments.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 3, "Grand Totals")
            End If
            dgPayments.Cols("ISSUED").Visible = False
            dgPayments.Cols("DESCRIPTION").Caption = getValueByKey("frmtillfinancialreport.dgpayments.description")
            dgPayments.Cols("DESCRIPTION").Width = 120
            dgPayments.Cols("AMOUNTTENDERED").Caption = getValueByKey("frmtillfinancialreport.dgpayments.amounttendered")
            dgPayments.Cols("AMOUNTTENDERED").Format = "0.00"
            dgPayments.Cols("TenderHeadCode").Visible = False
            dgPayments.Cols("TENDERTYPE").Caption = ""
            dgPayments.Cols("TENDERTYPE").Width = 100
            dgPayments.ExtendLastCol = True

            dgIssued.Cols("ISSUED").Visible = False
            dgIssued.Cols("DESCRIPTION").Caption = getValueByKey("frmtillfinancialreport.dgissued.description")
            dgIssued.Cols("AMOUNTTENDERED").Caption = getValueByKey("frmtillfinancialreport.dgissued.amounttendered")
            dgIssued.Cols("AMOUNTTENDERED").Format = "0.00"
            dgIssued.Cols("TenderHeadCode").Visible = False
            dgIssued.Cols("TENDERTYPE").Caption = ""

            dgIssued.ExtendLastCol = True

            dtOther.Clear()
            dtOther.Merge(dtTemp, False, MissingSchemaAction.Ignore)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub BuildCashDetails(ByRef dt As DataTable, ByVal Float As Decimal)
        Try
            Dim dv As New DataView(dt, "", "", DataViewRowState.CurrentRows)
            Dim dtCurr As DataTable = dv.ToTable("CASH", True, "CurrencyCode", "Currency", "Rate")
            Dim dtTempCash As DataTable = dt.Clone()
            dtTempCash.TableName = "Cash"
            If Not dtCurr Is Nothing AndAlso dtCurr.Rows.Count > 0 Then
                For Each dr As DataRow In dtCurr.Rows
                    Dim drdata As DataRow = dtTempCash.NewRow()
                    drdata("CurrencyCode") = dr(0).ToString()
                    drdata("Currency") = dr(1).ToString()
                    drdata("Amount") = dt.Compute("Sum(Amount)", "CurrencyCode='" & dr(0).ToString() & "' AND Rate=" & ConvertToEnglish(dr("Rate")))
                    drdata("Rate") = dr(2).ToString()
                    drdata("Total") = dt.Compute("Sum(Total)", "CurrencyCode='" & dr(0).ToString() & "'AND Rate=" & ConvertToEnglish(dr("Rate")))
                    dtTempCash.Rows.Add(drdata)
                Next
            End If
            dtCash.Rows.Clear()
            If Float <> 0 Then
                Dim dvfloat As New DataView(dtTempCash, "CurrencyCode='" & clsAdmin.CurrencyCode & "'", "", DataViewRowState.CurrentRows)
                If dvfloat.Count > 0 Then
                    dvfloat(0)("Amount") = CDbl(dvfloat(0)("Amount")) + Float
                    dvfloat(0)("Total") = CDbl(dvfloat(0)("Total")) + Float
                Else
                    Dim dr As DataRow = dtTempCash.NewRow()
                    dr("CurrencyCode") = clsAdmin.CurrencyCode
                    dr("Currency") = clsAdmin.CurrencyDescription
                    dr("Amount") = Float
                    dr("Rate") = 1
                    dr("Total") = Float
                    dtTempCash.Rows.Add(dr)
                End If
            End If
            dtCash.Merge(dtTempCash, False, MissingSchemaAction.Ignore)
            dgCash.DataSource = dtCash
            dgCash.Cols("Currency").Caption = getValueByKey("frmtillfinancialreport.dgcash.currency")
            dgCash.Cols("Amount").Caption = getValueByKey("frmtillfinancialreport.dgcash.amount")
            dgCash.Cols("Rate").Caption = getValueByKey("frmtillfinancialreport.dgcash.rate")
            dgCash.Cols("Total").Caption = getValueByKey("frmtillfinancialreport.dgcash.total")
            dgCash.Cols("Amount").Format = "0.00"
            dgCash.Cols("Total").Format = "0.00"
            dgCash.Cols("CurrencyCode").Visible = False
            dgCash.ExtendLastCol = True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub txtOperations_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOperations.KeyDown
        If ((e.KeyValue >= 48 And e.KeyValue < 58) Or (e.KeyValue >= 96 And e.KeyValue < 106) Or e.KeyValue = 8 Or e.KeyValue = 32 Or e.KeyValue = 46 Or e.KeyValue = 110 Or e.KeyValue = 190) Or e.KeyValue = 188 And Not (txtOperations.Text = String.Empty And (e.KeyValue = 110 Or e.KeyValue = 190)) And Not (txtOperations.Text.Contains(".") And (e.KeyValue = 110 Or e.KeyValue = 190)) Then
            e.SuppressKeyPress = False
        Else
            e.SuppressKeyPress = True
        End If
    End Sub

    ' Dim objShifts As New clsShift()
    Private Sub CmdPrint_Click(sender As System.Object, e As System.EventArgs) Handles CmdPrint.Click
        'removeFloatEntry()
        Dim objSite As New clsCommon
        Dim DisplayCashTransaction As Boolean = False
        Dim sitename As String = objSite.GetSiteName(clsAdmin.SiteCode)
        Dim shiftname As String = objSite.GetShiftNameForPrint(currentshiftid, clsAdmin.SiteCode)
        ShiftOpenValue = FormatNumber(ShiftOpenValue, 0)
        Dim objprint As New clsPrintDenomination(clsDefaultConfiguration.TillClosePrintPreivewReq)
        Dim objDefault As New clsDefaultConfiguration("DC")
        objDefault.GetDefaultSettings()
        If Not cmdDispCash.Enabled Then
            DisplayCashTransaction = True
        End If
        If clsDefaultConfiguration.ClientForMail = "PC" Then
            Dim DsShiftFinReportData = objShifts.GetShiftFinReportData(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID, currentshiftid)
            DsShiftFinReportData.Tables(0).TableName = "NetCash"
            DsShiftFinReportData.Tables(1).TableName = "Summary"
            DsShiftFinReportData.Tables(2).TableName = "Discount"
            DsShiftFinReportData.Tables(3).TableName = "NetSale"
            'objprint.PrintPCShiftFinancialReport(clsAdmin.TerminalID, clsAdmin.DayOpenDate, clsAdmin.SiteCode, "FINANCIAL BALANCE", shiftname, ShiftOpenValue, dtmain, dtCash, VendorAmount, gPrinterName, "0.00", dtPrinterInfo, lblPettyCashExpAmt.Text, lblPettyCashRecAmt.Text, clsAdmin.UserName, clsDefaultConfiguration.ClientName, sitename, TotalDiscount, TotalRoundoff)
            objprint.PrintPCShiftFinancialReport(DsShiftFinReportData, clsAdmin.TerminalID, clsAdmin.DayOpenDate, clsAdmin.SiteCode, "FINANCIAL BALANCE", shiftname, gPrinterName, dtPrinterInfo, , clsDefaultConfiguration.ClientName, sitename, DisplayCashTransaction)
        Else
            objprint.PrintFinancialReport(clsAdmin.TerminalID, clsAdmin.DayOpenDate, clsAdmin.SiteCode, "FINANCIAL BALANCE", shiftname, ShiftOpenValue, dtmain, dtCash, VendorAmount, gPrinterName, "0.00", dtPrinterInfo, lblPettyCashExpAmt.Text, lblPettyCashRecAmt.Text, clsAdmin.UserName, clsDefaultConfiguration.ClientName, sitename, TotalDiscount, TotalRoundoff, DisplayCashTransaction)
        End If

        ' Me.Close()
    End Sub

    Private Sub CmdNext_Click(sender As System.Object, e As System.EventArgs) Handles CmdNext.Click
        Try
            Dim objSummary As New frmCardSummary
            If objShifts.CheckOpening(clsAdmin.TerminalID, clsAdmin.SiteCode, "Close") Then
                'ShowMessage("Till Closed already done for this terminal", "Information")
                ShowMessage(getValueByKey("SOC06"), getValueByKey("CLAE04"))
                If CheckAuthorisation(clsAdmin.UserCode, "SHW_TILL_SMRY") = False Then
                    Me.Close()
                End If
                Exit Sub
            End If
            ''added by nikhil on 28/04/2017
            If clsDefaultConfiguration.ClientForMail = "PC" Then
                Dim dtDatatableNew As New DataTable
                If clsDefaultConfiguration.EDCSummaryinput Then

                    dtDatatableNew.Clear()
                    dtDatatableNew.Columns.Add("TENDERTYPE", GetType(String))
                    dtDatatableNew.Columns.Add("AMOUNTTENDERED", GetType(Integer))


                    If dtTemp.Select("TENDERTYPE='CreditCard' ").Length > 0 Then
                        For i = 0 To dtTemp.Select("TENDERTYPE='CreditCard' ").Length - 1
                            Dim drValue = dtTemp.Select("TENDERTYPE='CreditCard' ")
                            Dim drRow As DataRow
                            drRow = dtDatatableNew.NewRow

                            drRow("TENDERTYPE") = drValue(i)("TENDERTYPE")
                            drRow("AMOUNTTENDERED") = drValue(i)("AMOUNTTENDERED")
                            dtDatatableNew.Rows.Add(drRow)
                            '   i = i + 1
                        Next
                    End If

                    Dim dtAll As New DataTable
                    If dtDatatableNew.Select("TENDERTYPE='CreditCard' ").Length > 0 Then


                        dtAll.Clear()
                        dtAll.Columns.Add("TENDERTYPE", GetType(String))
                        dtAll.Columns.Add("AMOUNTTENDERED", GetType(Integer))
                        Dim drRow1 As DataRow
                        drRow1 = dtAll.NewRow
                        Dim drValue1 = dtDatatableNew.Select("TENDERTYPE='CreditCard' ")
                        drRow1("TENDERTYPE") = drValue1(0)("TENDERTYPE")
                        drRow1("AMOUNTTENDERED") = dtDatatableNew.Compute("Sum(AMOUNTTENDERED)", "").ToString

                        dtAll.Rows.Add(drRow1)
                        ' dtAll = dtDatatableNew.Compute("SUM(AMOUNTTENDERED)", " group by TENDERTYPE")
                        If dtTemp.Select("TENDERTYPE='CreditCard' ").Length > 0 Then
                            Dim Amount = dtTemp.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='CreditCard'").ToString
                            objSummary.SummaryAmt = Amount

                            objSummary.DtTender = dtAll
                            objSummary.ShiftId = dt.Rows(0)("ShiftId").ToString()
                            objSummary.ShowDialog()
                        Else

                        End If
                    End If
                End If
                Dim objImprest As New frmImprestCash
                If objSummary.frmCancel = True Then   '' added by nikhil to cancel the current form
                    objSummary.frmCancel = False
                    Exit Sub
                ElseIf clsDefaultConfiguration.EnableImprestCash = True Then    '' added by ketan
                    If clsDefaultConfiguration.ImprestCashTill = clsAdmin.TerminalID Then

                        objImprest.ShowDialog()
                    End If
                    If objImprest.Cancel = True Then
                        Me.Close()
                        Exit Sub


                        'End If
                    Else
                        removeFloatEntry()


                        If (clsDefaultConfiguration.ClientForMail = "PC") Then     'vipin 04-05-2017
                            If clsDefaultConfiguration.TillOperationRequired = True Then
                                Dim objShift As New frmShiftClosingPC
                                If objShift.Tag <> String.Empty Then
                                    objShift.TotalCash = dtCash
                                    objShift.TotalTillAmount = lblTotalCollection.Text
                                    objShift.TotalGiftAmount = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='GiftVoucher(R)'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='GiftVoucher(R)'"))
                                    objShift.TotalCreditVoucAmount = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='CreditVouc(R)'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='CreditVouc(R)'"))
                                    objShift.TotalCheckAmount = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Cheque'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Cheque'"))

                                    objShift.NEFT = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Neft'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Neft'"))
                                    objShift.RTGS = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Rtgs'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Rtgs'"))

                                    objShift.TotalPayments = dtOther
                                    objShift.TotalFloat = ShiftOpenValue
                                    If txtOperations.Text <> String.Empty Then
                                        objShift.TotalVendorAmount = txtOperations.Text
                                    End If
                                    objShift.MdiParent = MDISpectrum
                                    objShift.Show()
                                    Me.Close()
                                End If
                            Else
                                ShowMessage(getValueByKey("SOC05"), "SOC05 - " & getValueByKey("CLAE04"))
                            End If
                        Else
                            If clsDefaultConfiguration.TillClosePrintPreivewReq = True Then
                                Dim objShift As New frmShiftClosingPC
                                If objShift.Tag <> String.Empty Then
                                    objShift.TotalCash = dtCash
                                    objShift.TotalTillAmount = lblTotalCollection.Text
                                    objShift.TotalGiftAmount = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='GiftVoucher(R)'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='GiftVoucher(R)'"))
                                    objShift.TotalCreditVoucAmount = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='CreditVouc(R)'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='CreditVouc(R)'"))
                                    objShift.TotalCheckAmount = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Cheque'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Cheque'"))
                                    objShift.TotalPayments = dtOther
                                    objShift.TotalFloat = ShiftOpenValue
                                    If txtOperations.Text <> String.Empty Then
                                        objShift.TotalVendorAmount = txtOperations.Text
                                    End If
                                    objShift.MdiParent = MDISpectrum
                                    objShift.Show()
                                    Me.Close()
                                End If
                            Else
                                ShowMessage(getValueByKey("SOC05"), "SOC05 - " & getValueByKey("CLAE04"))
                            End If
                        End If
                    End If
                Else
                    removeFloatEntry()


                    If (clsDefaultConfiguration.ClientForMail = "PC") Then     'vipin 04-05-2017
                        If clsDefaultConfiguration.TillOperationRequired = True Then
                            Dim objShift As New frmShiftClosingPC
                            If objShift.Tag <> String.Empty Then
                                objShift.TotalCash = dtCash
                                objShift.TotalTillAmount = lblTotalCollection.Text
                                objShift.TotalGiftAmount = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='GiftVoucher(R)'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='GiftVoucher(R)'"))
                                objShift.TotalCreditVoucAmount = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='CreditVouc(R)'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='CreditVouc(R)'"))
                                objShift.TotalCheckAmount = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Cheque'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Cheque'"))

                                objShift.NEFT = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Neft'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Neft'"))
                                objShift.RTGS = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Rtgs'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Rtgs'"))

                                objShift.TotalPayments = dtOther
                                objShift.TotalFloat = ShiftOpenValue
                                If txtOperations.Text <> String.Empty Then
                                    objShift.TotalVendorAmount = txtOperations.Text
                                End If
                                objShift.MdiParent = MDISpectrum
                                objShift.Show()
                                Me.Close()
                            End If
                        Else
                            ShowMessage(getValueByKey("SOC05"), "SOC05 - " & getValueByKey("CLAE04"))
                        End If
                    Else
                        If clsDefaultConfiguration.TillClosePrintPreivewReq = True Then
                            Dim objShift As New frmShiftClosingPC
                            If objShift.Tag <> String.Empty Then
                                objShift.TotalCash = dtCash
                                objShift.TotalTillAmount = lblTotalCollection.Text
                                objShift.TotalGiftAmount = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='GiftVoucher(R)'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='GiftVoucher(R)'"))
                                objShift.TotalCreditVoucAmount = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='CreditVouc(R)'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='CreditVouc(R)'"))
                                objShift.TotalCheckAmount = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Cheque'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Cheque'"))
                                objShift.TotalPayments = dtOther
                                objShift.TotalFloat = ShiftOpenValue
                                If txtOperations.Text <> String.Empty Then
                                    objShift.TotalVendorAmount = txtOperations.Text
                                End If
                                objShift.MdiParent = MDISpectrum
                                objShift.Show()
                                Me.Close()
                            End If
                        Else
                            ShowMessage(getValueByKey("SOC05"), "SOC05 - " & getValueByKey("CLAE04"))
                        End If
                    End If
                End If
            Else
                removeFloatEntry()
                If clsDefaultConfiguration.TillOperationRequired = True Then
                    Dim objShift As New frmShiftClosing
                    objShift.DisplayCash = Not cmdDispCash.Enabled
                    If objShift.Tag <> String.Empty Then
                        objShift.TotalCash = dtCash
                        objShift.TotalTillAmount = lblTotalCollection.Text
                        objShift.TotalGiftAmount = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='GiftVoucher(R)'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='GiftVoucher(R)'"))
                        objShift.TotalCreditVoucAmount = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='CreditVouc(R)'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='CreditVouc(R)'"))
                        objShift.TotalCheckAmount = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Cheque'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Cheque'"))
                        objShift.TotalPayments = dtOther
                        objShift.TotalFloat = ShiftOpenValue
                        If txtOperations.Text <> String.Empty Then
                            objShift.TotalVendorAmount = txtOperations.Text
                        End If
                        objShift.MdiParent = MDISpectrum
                        objShift.Show()
                        Me.Close()
                    End If
                Else
                    ShowMessage(getValueByKey("SOC05"), "SOC05 - " & getValueByKey("CLAE04"))
                End If
            End If
            'Try
            '    Dim objShift As New frmShiftClosing
            '    objShift.DisplayCash = Not cmdDispCash.Enabled
            '    If objShifts.CheckOpening(clsAdmin.TerminalID, clsAdmin.SiteCode, "Close") Then
            '        'ShowMessage("Till Closed already done for this terminal", "Information")
            '        ShowMessage(getValueByKey("SOC06"), getValueByKey("CLAE04"))
            '        If CheckAuthorisation(clsAdmin.UserCode, "SHW_TILL_SMRY") = False Then
            '            Me.Close()
            '        End If
            '        Exit Sub
            '    End If
            '    removeFloatEntry()
            '    If clsDefaultConfiguration.TillOperationRequired = True Then
            '        '  Dim objShift As New frmShiftClosing
            '        If objShift.Tag <> String.Empty Then
            '            objShift.TotalCash = dtCash
            '            objShift.TotalTillAmount = lblTotalCollection.Text
            '            objShift.TotalGiftAmount = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='GiftVoucher(R)'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='GiftVoucher(R)'"))
            '            objShift.TotalCreditVoucAmount = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='CreditVouc(R)'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='CreditVouc(R)'"))
            '            objShift.TotalCheckAmount = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Cheque'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Cheque'"))
            '            objShift.TotalPayments = dtOther
            '            objShift.TotalFloat = ShiftOpenValue
            '            If txtOperations.Text <> String.Empty Then
            '                objShift.TotalVendorAmount = txtOperations.Text
            '            End If
            '            objShift.MdiParent = MDISpectrum
            '            objShift.Show()
            '            Me.Close()
            '        End If
            '    Else
            '        ShowMessage(getValueByKey("SOC05"), "SOC05 - " & getValueByKey("CLAE04"))
            '    End If


        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub txtOperations_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOperations.Leave
        Try
            If txtOperations.Text <> String.Empty Then
                VendorAmount = txtOperations.Text
                lblFinalCollection.Text = lblTotalCollection.Text - txtOperations.Text
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub removeFloatEntry()
        Try
            Dim dv As New DataView(dtOther, "DESCRIPTION='Float'", "", DataViewRowState.CurrentRows)
            If dv.Count > 0 Then
                dv.Delete(0)
            End If
            dv.RowFilter = "TENDERTYPE='CASH'"
            If dv.Count > 0 Then
                dv(0)("AMOUNTTENDERED") = dv(0)("AMOUNTTENDERED") + ShiftOpenValue
            End If
            dv.Table.AcceptChanges()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub txtOperations_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOperations.TextChanged
        Try
            If txtOperations.Text <> String.Empty Then
                VendorAmount = txtOperations.Text
                lblFinalCollection.Text = lblTotalCollection.Text - txtOperations.Text
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Public Sub New()
        Dim objDefault As New clsDefaultConfiguration("TillOpenNClose")
        objDefault.GetDefaultSettings()
        If clsDefaultConfiguration.TillOperationRequired = False Then
            ShowMessage(getValueByKey("SOC05"), "TFR01 - " & getValueByKey("CLAE04"))
            Me.Dispose()
            Exit Sub
        End If

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        'Changed By Sameer for Issue ID 6768 4-5-2013
        If clsDefaultConfiguration.IsPettyCashApplicable AndAlso clsDefaultConfiguration.IsPettyCashOnSameTerminal AndAlso clsAdmin.TerminalID = clsDefaultConfiguration.PettyCashTerminalId Then
            lblPettyCashExp.Visible = True
            lblPettyCashExpAmt.Visible = True
            lblPettyCashRec.Visible = True
            lblPettyCashRecAmt.Visible = True

        End If
        ' Add any initialization after the InitializeComponent() call.
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Private Sub frmShiftFinancialReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "Shift-Close.htm")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        lblHeader.ForeColor = Color.White
        lblHeader.BorderStyle = BorderStyle.None
        lblHeader.BackColor = Color.Transparent
        CtrlLabel2.BorderStyle = BorderStyle.None
        CtrlLabel2.ForeColor = Color.White
        CtrlLabel2.BackColor = Color.Transparent
        lblPettyCashExp.BorderStyle = BorderStyle.None
        lblPettyCashExp.ForeColor = Color.White
        lblPettyCashExp.BackColor = Color.Transparent
        lblPettyCashExpAmt.BorderStyle = BorderStyle.None
        lblPettyCashExpAmt.ForeColor = Color.White
        lblPettyCashExpAmt.BackColor = Color.Transparent
        lblPettyCashRec.BorderStyle = BorderStyle.None
        lblPettyCashRec.ForeColor = Color.White
        lblPettyCashRec.BackColor = Color.Transparent
        lblPettyCashRecAmt.BorderStyle = BorderStyle.None
        lblPettyCashRecAmt.ForeColor = Color.White
        lblPettyCashRecAmt.BackColor = Color.Transparent
        lblTotalCash.BorderStyle = BorderStyle.None
        lblTotalCash.ForeColor = Color.White
        lblTotalCash.BackColor = Color.Transparent
        lbltotalCashCollection.BorderStyle = BorderStyle.None
        lbltotalCashCollection.ForeColor = Color.White
        lbltotalCashCollection.BackColor = Color.Transparent
        lblTotalCashCollectionAmt.BorderStyle = BorderStyle.None
        lblTotalCashCollectionAmt.ForeColor = Color.White
        lblTotalCashCollectionAmt.BackColor = Color.Transparent
        lblTotalCollection.BorderStyle = BorderStyle.None
        lblTotalCollection.ForeColor = Color.White
        lblTotalCollection.BackColor = Color.Transparent
        lblFinalCollection.BorderStyle = BorderStyle.None
        lblFinalCollection.ForeColor = Color.White
        lblFinalCollection.BackColor = Color.Transparent
        CtrlLabel6.BorderStyle = BorderStyle.None
        CtrlLabel6.ForeColor = Color.White
        CtrlLabel6.BackColor = Color.Transparent
        lblOperation.BorderStyle = BorderStyle.None
        lblOperation.ForeColor = Color.White
        lblOperation.BackColor = Color.Transparent
        CtrlLabel1.BorderStyle = BorderStyle.None
        CtrlLabel1.ForeColor = Color.White
        CtrlLabel1.BackColor = Color.Transparent
        CtrlLabel7.BorderStyle = BorderStyle.None
        CtrlLabel7.ForeColor = Color.White
        CtrlLabel7.BackColor = Color.Transparent
        CtrlLabel3.BorderStyle = BorderStyle.None
        CtrlLabel3.ForeColor = Color.White
        CtrlLabel3.BackColor = Color.Transparent
        CtrlLabel4.BorderStyle = BorderStyle.None
        CtrlLabel4.ForeColor = Color.White
        CtrlLabel4.BackColor = Color.Transparent
        lblCreditSale.BorderStyle = BorderStyle.None
        lblCreditSale.ForeColor = Color.White
        lblCreditSale.BackColor = Color.Transparent
        lblCreditsaleAmt.BorderStyle = BorderStyle.None
        lblCreditsaleAmt.ForeColor = Color.White
        lblCreditsaleAmt.BackColor = Color.Transparent
        CmdPrint.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CmdPrint.BackColor = Color.Transparent
        CmdPrint.BackColor = Color.FromArgb(0, 107, 163)
        CmdPrint.ForeColor = Color.FromArgb(255, 255, 255)
        CmdPrint.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CmdPrint.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CmdPrint.FlatStyle = FlatStyle.Flat
        CmdPrint.FlatAppearance.BorderSize = 0
        CmdPrint.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        CmdPrint.Size = New Size(128, 30)
        CmdNext.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CmdNext.BackColor = Color.Transparent
        CmdNext.BackColor = Color.FromArgb(0, 107, 163)
        CmdNext.ForeColor = Color.FromArgb(255, 255, 255)
        CmdNext.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CmdNext.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CmdNext.FlatStyle = FlatStyle.Flat
        CmdNext.FlatAppearance.BorderSize = 0
        CmdNext.Location = New Point(270, 395)
        CmdNext.Size = New Size(128, 30)
        CmdNext.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        dgPayments.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        dgPayments.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgPayments.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        dgPayments.Rows.MinSize = 25
        dgPayments.Styles.Normal.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        dgPayments.Styles.Fixed.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgPayments.Styles.Highlight.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgPayments.Styles.Highlight.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgPayments.Styles.Focus.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgPayments.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgCash.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        dgCash.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgCash.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        dgCash.Rows.MinSize = 25
        dgCash.Styles.Normal.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        dgCash.Styles.Fixed.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgCash.Styles.Highlight.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgCash.Styles.Highlight.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgCash.Styles.Focus.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgCash.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgIssued.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        dgIssued.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgIssued.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        dgIssued.Rows.MinSize = 25
        dgIssued.Styles.Normal.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        dgIssued.Styles.Fixed.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgIssued.Styles.Highlight.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgIssued.Styles.Highlight.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgIssued.Styles.Focus.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgIssued.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)

    End Function

    Private Sub cmdDispCash_Click(sender As Object, e As System.EventArgs) Handles cmdDispCash.Click
        If CheckInterTransactionAuth("SC_CashDetails") = True Then
            EnableDisableCashDetails(True)
            cmdDispCash.Enabled = False
            dgPayments.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 1, 0, 3, "Sub Totals")
            dgPayments.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 3, "Grand Totals")
            Dim objshiftclose As New frmShiftClosing
            objshiftclose.DisplayCash = True
        End If

    End Sub
End Class