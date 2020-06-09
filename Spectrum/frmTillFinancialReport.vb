﻿Imports System.Data
Imports System.Resources
Imports SpectrumBL
Imports System.Globalization
Imports System.Data.SqlClient
Imports SpectrumPrint
Imports SpectrumCommon

Public Class frmTillFinancialReport
    Dim dtCash, dtOther As DataTable
    Dim dvPayment, dvIssue As DataView
    Dim TillOpenValue, VendorAmount As Decimal
    Dim ObjclsCommon As clsCommon
    Private Sub frmTillFinancialReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            VendorAmount = 0
            ObjclsCommon = New clsCommon
            Dim objTill As New clsTill()
            ''-- PettyCashApplicable condition is commented By Mahesh as per Rama Suggestion date 20-05-2014
            'If clsDefaultConfiguration.IsPettyCashApplicable AndAlso clsDefaultConfiguration.IsPettyCashOnSameTerminal AndAlso clsAdmin.TerminalID = clsDefaultConfiguration.PettyCashTerminalId Then
            '    Dim objPettyCash As IPettyCashVoucher = New PettyCashVoucher()
            '    Dim reqest As New GetVoucherBalanceRequest
            '    reqest.SiteCode = clsAdmin.SiteCode
            '    reqest.FinYear = clsAdmin.Financialyear
            '    reqest.DayOpenDate = clsAdmin.DayOpenDate
            '    TillOpenValue = objPettyCash.GetPettyCashOpeningBalance(reqest)
            'Else
            TillOpenValue = objTill.GetTillOpenDetail(clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.DayOpenDate)
            ' End If
            dtCash = ObjclsCommon.GetTerminalCashDetail(clsAdmin.TerminalID, clsAdmin.SiteCode, clsAdmin.DayOpenDate)
            If Not dtCash Is Nothing Then
                BuildCashDetails(dtCash, TillOpenValue)
                lblTotalCash.Text = FormatNumber(IIf(dtCash.Compute("Sum(Total)", "") Is DBNull.Value, 0, dtCash.Compute("Sum(Total)", "")), 2)
            End If
            dtOther = ObjclsCommon.GetTerminalOtherDetail(clsAdmin.TerminalID, clsAdmin.SiteCode, clsAdmin.DayOpenDate)
            Dim dtGift As DataTable = ObjclsCommon.GetTerminalGiftVoucherIssue(clsAdmin.TerminalID, clsAdmin.SiteCode, clsAdmin.DayOpenDate)
            If Not dtGift Is Nothing AndAlso dtGift.Rows.Count > 0 AndAlso Not IsDBNull(dtGift.Rows(0)("AMOUNTTENDERED")) Then
                dtOther.Merge(dtGift, False, MissingSchemaAction.Ignore)
            End If
            'Changed By Sameer for Issue ID 6768 4-5-2013
            If clsDefaultConfiguration.IsPettyCashApplicable AndAlso clsDefaultConfiguration.IsPettyCashOnSameTerminal AndAlso clsAdmin.TerminalID = clsDefaultConfiguration.PettyCashTerminalId Then
                Dim request As New SpectrumCommon.GetVoucherBalanceRequest()
                request.DayOpenDate = clsAdmin.DayOpenDate
                request.FinYear = clsAdmin.Financialyear
                request.SiteCode = clsAdmin.SiteCode
                Dim voucher As IPettyCashVoucher = New PettyCashVoucher()
                lblPettyCashRecAmt.Text = voucher.GetTotalPettyCashReceipt(request).ToString()
                lblPettyCashExpAmt.Text = voucher.GetTotalPettyCashExpense(request).ToString()
                'code commented by vipul for issue id 2282
                'If (dtCash IsNot Nothing AndAlso dtCash.Rows.Count > 0) Then
                '    dtCash.Rows(0)("Amount") = Val(dtCash.Rows(0)("Amount")) + Val(lblPettyCashRecAmt.Text) - Val(lblPettyCashExpAmt.Text)
                '    dtCash.Rows(0)("Total") = Val(dtCash.Rows(0)("Total")) + Val(lblPettyCashRecAmt.Text) - Val(lblPettyCashExpAmt.Text)
                'End If

                '-------------------------
                'code added for issue id 2282 by vipul
                Dim PettyCashReceiptAmt, PettyCashExpenseAmt As Double
                PettyCashReceiptAmt = Convert.ToDouble(lblPettyCashRecAmt.Text.ToString)
                PettyCashExpenseAmt = Convert.ToDouble(lblPettyCashExpAmt.Text.ToString)




                If (dtCash IsNot Nothing AndAlso dtCash.Rows.Count > 0 Or (PettyCashReceiptAmt > 0)) Then
                    If dtCash IsNot Nothing AndAlso dtCash.Rows.Count > 0 Then
                        dtCash.Rows(0)("Amount") = Val(dtCash.Rows(0)("Amount")) + Val(lblPettyCashRecAmt.Text) - Val(lblPettyCashExpAmt.Text)
                        dtCash.Rows(0)("Total") = Val(dtCash.Rows(0)("Total")) + Val(lblPettyCashRecAmt.Text) - Val(lblPettyCashExpAmt.Text)
                    Else
                        'code added for issue id 2282 by vipul
                        If dtCash.Rows.Count = 0 AndAlso (PettyCashReceiptAmt <> PettyCashExpenseAmt) Then
                            dtCash.Rows.Add("INR", "Rupees", "0.0", "1.00", "0.0")
                            dtCash.Rows(0)("Amount") = Val(dtCash.Rows(0)("Amount")) + Val(lblPettyCashRecAmt.Text) - Val(lblPettyCashExpAmt.Text)
                            dtCash.Rows(0)("Total") = Val(dtCash.Rows(0)("Total")) + Val(lblPettyCashRecAmt.Text) - Val(lblPettyCashExpAmt.Text)

                        End If


                    End If

                End If
                '------------

            End If
            Dim Creditsale As ICreditSale = New ClsCreditSale()

            '----Commented By Mahesh  Getting All Data for day and then do Sum and assign sum 
            'Dim creditsaleamt = Creditsale.GetTotalCreditSalebyDate(clsAdmin.DayOpenDate)
            Dim dtcreditsaleAdjustments = Creditsale.GetTotalCreditSaleAdjustmentbyDate(clsAdmin.DayOpenDate,clsAdmin.TerminalID)
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
                BuildDetails(dtOther)
            End If
            lblFinalCollection.Text = lblTotalCollection.Text
            'Me.Visible = False
            If CheckAuthorisation(clsAdmin.UserCode, "SHW_TILL_SMRY") = False Then
                CmdNext_Click(CmdNext, New EventArgs())
            End If
            If CheckAuthorisation(clsAdmin.UserCode, "SC_CashDetails") = False Then
                cmdDispCash.Enabled = True
                EnableDisableCashDetails(False)
                ' lblFinalCollectio
            Else
                cmdDispCash.Enabled = False
            End If
            If clsDefaultConfiguration.DisplayBrandWiseSale Then
                cmdBrandWiseSale.Visible = True
            Else
                cmdBrandWiseSale.Visible = False
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
        Try
            If clsAdmin.IsCashDrawer = True Then
                If CheckAuthorisation(clsAdmin.UserCode, "OPENDRAWER") = True Then
                    Dim cA4Print As New SpectrumPrint.clsA4Print
                    cA4Print.CashDrawerWithoutDriver = clsAdmin.CashDrawerWithoutDriver
                    cA4Print.OperateDevice("CashDrawer", CDflag:=clsDefaultConfiguration.CDBuildCode)
                End If
            End If
        Catch ex As Exception
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
            '  lblPettyCashExp.Visible = Value
            '  lblPettyCashExpAmt.Visible = Value
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
            drFloat("AMOUNTTENDERED") = TillOpenValue
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

    Private Sub CmdNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdNext.Click
        Try
            Dim objTills As New clsTill()
            Dim objTill As New frmNTillClosing
            objTill.DisplayCash = Not cmdDispCash.Enabled
            If objTills.CheckOpening(clsAdmin.TerminalID, clsAdmin.SiteCode, "Close") Then
                'ShowMessage("Till Closed already done for this terminal", "Information")
                ShowMessage(getValueByKey("TLC002"), getValueByKey("CLAE04"))
                If CheckAuthorisation(clsAdmin.UserCode, "SHW_TILL_SMRY") = False Then
                    Me.Close()
                End If
                Exit Sub
            End If
            removeFloatEntry()
            Dim objImprest As New frmImprestCash
            If clsDefaultConfiguration.TillOperationRequired = True Then
                If clsDefaultConfiguration.EnableImprestCash = True And clsDefaultConfiguration.ImprestCashTill = clsAdmin.TerminalID Then
                    objImprest.ShowDialog()
                End If
                If objImprest.Cancel = True Then
                    Me.Close()
                    Exit Sub
                Else
                    If objTill.Tag <> String.Empty Then
                        objTill.TotalCash = dtCash
                        objTill.TotalTillAmount = lblTotalCollection.Text
                        objTill.TotalGiftAmount = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='GiftVoucher(R)'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='GiftVoucher(R)'"))
                        objTill.TotalCreditVoucAmount = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='CreditVouc(R)'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='CreditVouc(R)'"))
                        objTill.TotalCheckAmount = IIf(dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Cheque'") Is DBNull.Value, 0, dtOther.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE='Cheque'"))
                        objTill.TotalPayments = dtOther
                        objTill.TotalFloat = TillOpenValue
                        If txtOperations.Text <> String.Empty Then
                            objTill.TotalVendorAmount = txtOperations.Text
                        End If
                        objTill.MdiParent = MDISpectrum
                        objTill.Show()
                        Me.Close()
                    End If
                End If
            Else
                ShowMessage(getValueByKey("TFR01"), "TFR01 - " & getValueByKey("CLAE04"))
            End If


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
                dv(0)("AMOUNTTENDERED") = dv(0)("AMOUNTTENDERED") + TillOpenValue
            End If
            dv.Table.AcceptChanges()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub CmdPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdPrint.Click
        Dim DisplayCashTransaction As Boolean = False
        removeFloatEntry()
        If Not cmdDispCash.Enabled Then
            DisplayCashTransaction = True
        End If
        Dim objprint As New clsPrintFinancialReport(clsDefaultConfiguration.TillClosePrintPreivewReq, clsDefaultConfiguration.UserAmountPrint)
        objprint.PrintFinancialReport(clsAdmin.TerminalID, clsAdmin.DayOpenDate, clsAdmin.SiteCode, dtOther, dtCash, VendorAmount, gPrinterName, "0.00", dtPrinterInfo, lblPettyCashExpAmt.Text, lblPettyCashRecAmt.Text, clsAdmin.UserName, DisplayCashTransaction)
        Me.Close()
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
            ShowMessage(getValueByKey("TFR01"), "TFR01 - " & getValueByKey("CLAE04"))
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
        CmdNext.Location = New Point(272, 398)
        CmdNext.Size = New Size(128, 30)
        CmdNext.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        cmdDispCash.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'cmdDispCash.BackColor = Color.Transparent
        cmdDispCash.BackColor = Color.FromArgb(0, 107, 163)
        cmdDispCash.ForeColor = Color.FromArgb(255, 255, 255)
        cmdDispCash.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdDispCash.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdDispCash.FlatStyle = FlatStyle.Flat
        cmdDispCash.FlatAppearance.BorderSize = 0
        'cmdDispCash.Location = New Point(270, 395)
        cmdDispCash.Size = New Size(128, 30)
        cmdDispCash.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

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
            Dim objTills As New frmNTillClosing
            objTills.DisplayCash = True
        End If
    End Sub
    Private Sub cmdBrandWiseSale_Click(sender As Object, e As EventArgs) Handles cmdBrandWiseSale.Click
        Dim objfrmBrandWiseSale As New frmBrandWiseSale
        Dim dt = ObjclsCommon.GetBrandWiseTenderDetailForTillClose(clsAdmin.TerminalID, clsAdmin.SiteCode, clsAdmin.DayOpenDate)
        If dt.Rows.Count > 0 Then
            objfrmBrandWiseSale.dtMain = dt
            objfrmBrandWiseSale.dtOther = dt
            objfrmBrandWiseSale.TillOpenValue = TillOpenValue
            objfrmBrandWiseSale.ShowDialog()
        End If
    End Sub
End Class