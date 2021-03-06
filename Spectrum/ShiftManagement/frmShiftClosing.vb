﻿Imports System.Data
Imports System.Resources
Imports SpectrumBL
Imports System.Globalization
Imports System.Data.SqlClient
Imports SpectrumBL.POSDBDataSet
Imports SpectrumPrint
Imports SpectrumCommon
Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.ComponentModel
Public Class frmShiftClosing
    Dim DTMain, DTShiftClose, DTExtraAmount, DtCurrency, dtTill, dtNextDayAmount As DataTable
    Dim dtTender As New DataTable
    Dim dsTill As DataSet
    Dim ObjclsCommon As clsCommon
    Dim DvData As DataView
    Dim Filter As String = ""
    Dim IncosAmt As Double = 0
    Dim InCosCash, InCosCV, InCosGV, InCosCheck As Decimal
    Dim ServerDate As DateTime
    Dim EditMode, ConsistancyDone As Boolean
    Dim DTFloatingDetail1 As New FloatingDetailDataTable
    'Dim TAFloatingDetail As New POSDBDataSetTableAdapters.FloatingDetailTableAdapter
    Dim _dtCash, _dtPayment As DataTable
    Dim _float, _GVAmount, _CVAmount, _Check, _CreditCard, _TotalTillCollection, _VendorAmount As Decimal
    Dim nextShiftId As Integer
    Dim prevShiftId As Integer
    Dim strDetail As String
    Public WriteOnly Property TotalCash() As DataTable
        Set(ByVal value As DataTable)
            _dtCash = value
        End Set
    End Property
    Public WriteOnly Property TotalPayments() As DataTable
        Set(ByVal value As DataTable)
            _dtPayment = value
        End Set
    End Property
    Public WriteOnly Property TotalGiftAmount() As Decimal
        Set(ByVal value As Decimal)
            _GVAmount = value
        End Set
    End Property
    Public WriteOnly Property TotalCreditVoucAmount() As Decimal
        Set(ByVal value As Decimal)
            _CVAmount = value
        End Set
    End Property
    Public WriteOnly Property TotalCheckAmount() As Decimal
        Set(ByVal value As Decimal)
            _Check = value
        End Set
    End Property
    Public WriteOnly Property TotalTillAmount() As Decimal
        Set(ByVal value As Decimal)
            _TotalTillCollection = value
        End Set
    End Property
    Public WriteOnly Property TotalCreditCardAmount() As Decimal
        Set(ByVal value As Decimal)
            _CreditCard = value
        End Set
    End Property
    Public WriteOnly Property TotalVendorAmount() As Decimal
        Set(ByVal value As Decimal)
            _VendorAmount = value
        End Set
    End Property
    Public WriteOnly Property TotalFloat() As Decimal
        Set(ByVal value As Decimal)
            _float = value
        End Set
    End Property
    Private _DisplayCash As Boolean = False
    Public Property DisplayCash() As Boolean
        Get
            Return _DisplayCash
        End Get
        Set(ByVal value As Boolean)
            _DisplayCash = value
        End Set
    End Property
    Private Sub frmShiftClosing_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try

            ObjclsCommon = New clsCommon()
            If DTMain Is Nothing Then
                DTMain = ObjclsCommon.GetDenomination(clsAdmin.CurrencyCode, clsAdmin.CurrencyCode)
            End If
            DvData = New DataView(DTMain, Filter, "DENOMINATIONAMT", DataViewRowState.CurrentRows)
            dgMainGrid.DataSource = DvData
            DtCurrency = ObjclsCommon.GetCurrencyDenomination
            DtCurrency.TableName = "Currency"
            If Not DtCurrency Is Nothing Then
                cbCurrency.DataSource = DtCurrency
                cbCurrency.DisplayMember = "CURRENCYDESCRIPTION"
                cbCurrency.ValueMember = "CURRENCYCODE"
                cbCurrency.SelectedValue = clsAdmin.CurrencyCode
                pC1ComboSetDisplayMember(cbCurrency)
                cbCurrency.CaptionVisible = False
            End If
            Dim dt As New DataTable
            dt = clsCommon.GetNextShiftID(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID)
            nextShiftId = clsCommon._NextShiftId
            prevShiftId = clsCommon._PrevShiftId
            strDetail = "SHIFT CLOSE DENOMINATION DETAILS"
            ActivityLogForShift(Nothing, strDetail, prevShiftId)
            'dtCash = ObjclsCommon.GetTerminalCashDetail(clsAdmin.TerminalID, clsAdmin.SiteCode)
            'If Not dtCash Is Nothing Then
            '    BuildCashDetails(dtCash)
            'End If
            'dtOther = ObjclsCommon.GetTerminalOtherDetail(clsAdmin.TerminalID, clsAdmin.SiteCode)
            'If Not dtOther Is Nothing Then
            '    Dim dtTemp As DataTable = dtOther.Clone()
            '    Dim dr As DataRow = dtTemp.NewRow
            '    For Each col As DataColumn In dtOther.Columns
            '        dr(col.ColumnName) = dtOther.Compute("Sum(" & col.ColumnName & ")", "").ToString()
            '    Next
            '    dtOther.Clear()
            '    dtTemp.Rows.Add(dr)
            '    dgOtherPayments.DataSource = dtTemp
            'End If
            'Dim objTill As New clsTill()
            'TillOpenValue = objTill.GetTillOpenDetail(clsAdmin.SiteCode, clsAdmin.TerminalID)
            FormatGrid()
            CalculateTotal(DTMain)
            cmdFinsh.Text = "&Cancel"
            cmdNext.Tag = "Next"
            cmdReset.Tag = "Reset"
            cmdFinsh.Tag = "Cancel"
            cmdPrint.Visible = False
            If clsDefaultConfiguration.DisableAmountPaidToVendorsInTillClose Then 'Jayesh 05/06/2019
                lblOperation.Visible = False
                txtOperations.Visible = False
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
        SetCulture(Me, Me.Name)
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
        End If
        'If clsDefaultConfiguration.IsPettyCashApplicable AndAlso clsDefaultConfiguration.IsPettyCashOnSameTerminal AndAlso clsAdmin.TerminalID = clsDefaultConfiguration.PettyCashTerminalId Then
        '    'If clsDefaultConfiguration.IsPettyCashOnSameTerminal AndAlso clsAdmin.TerminalID = clsDefaultConfiguration.PettyCashTerminalId Then
        '    cmdFinsh.Text = getValueByKey("frmntillclosing.cmdactualfinsh")
        '    cmdFinsh.Tag = "Finish"
        '    cmdNext.Visible = False
        '    cancelBtn.Visible = True
        '    'Else
        '    '    cmdFinsh.Text = getValueByKey("frmntillclosing.cmdfinshcancel")
        '    '    cmdFinsh.Tag = "Cancel"
        '    'End If
        'Else
        '    cmdFinsh.Text = getValueByKey("frmntillclosing.cmdfinshcancel")
        '    cmdFinsh.Tag = "Cancel"
        'End If
        lblHeader.Text = String.Format(lblHeader.Text, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.DayOpenDate.ToShortDateString())
    End Sub
    Private Sub txtOperations_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOperations.Leave
        Try
            If txtOperations.Text <> String.Empty Then
                TotalVendorAmount = txtOperations.Text
            End If
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

    Private Sub dgMainGrid_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgMainGrid.AfterEdit
        Try
            dgMainGrid.Rows(e.Row)("Amount") = dgMainGrid.Rows(e.Row)("DENOMINATIONAMT") * dgMainGrid.Rows(e.Row)("Qty")
            dgMainGrid.Rows(e.Row)("BaseAmount") = dgMainGrid.Rows(e.Row)("DENOMINATIONAMT") * dgMainGrid.Rows(e.Row)("Qty") * dgMainGrid.Rows(e.Row)("EXCHANGERATE")

            Dim dataTable = dgMainGrid.DataSource
            Dim dtData As New DataTable

            If (TypeOf dataTable Is DataTable) Then
                dtData = DirectCast(dataTable, DataTable)
            ElseIf (TypeOf dataTable Is DataView) Then
                dtData = DirectCast(dataTable, DataView).ToTable()
            End If

            CalculateTotal(dtData)
            cmdPrint.Visible = True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub cbCurrency_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbCurrency.SelectedValueChanged
        Try
            Dim dt As DataTable
            Dim dv As New DataView(DTMain, "CURRENCYCODE='" & cbCurrency.SelectedValue & "'", "", DataViewRowState.CurrentRows)
            If dv.Count <= 0 Then
                ObjclsCommon = New clsCommon()
                dt = ObjclsCommon.GetDenomination(cbCurrency.SelectedValue, clsAdmin.CurrencyCode)
                If Not dt Is Nothing Then
                    DTMain.Merge(dt, False, MissingSchemaAction.Ignore)
                End If
            End If
            DvData.RowFilter = "CURRENCYCODE='" & cbCurrency.SelectedValue & "'"
            FormatGrid()
            CalculateTotal(DTMain)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdPrint_Click(sender As System.Object, e As System.EventArgs) Handles cmdPrint.Click
        Dim objSite As New clsCommon
        Dim sitename As String = objSite.GetSiteName(clsAdmin.SiteCode)
        Dim shiftname As String = objSite.GetShiftNameForPrint(prevShiftId, clsAdmin.SiteCode)
        Dim objprint As New clsPrintDenomination(clsDefaultConfiguration.TillClosePrintPreivewReq)
        objprint.PrintShiftDenomination(clsAdmin.TerminalID, clsAdmin.DayOpenDate, clsAdmin.SiteCode, DTMain, strDetail, shiftname, lblTotalAmount.Text, gPrinterName, dtPrinterInfo, clsAdmin.UserName, clsDefaultConfiguration.ClientName, sitename)
        'Me.Close()
    End Sub

    Private Sub cancelBtn_Click(sender As System.Object, e As System.EventArgs) Handles cancelBtn.Click
        Me.Close()
    End Sub
    Private _IsNextExited As Boolean = False
    Private Sub cmdNext_Click(sender As System.Object, e As System.EventArgs) Handles cmdNext.Click
        Try
            If sender.tag = "Next" Then
                strDetail = "NEXT SHIFT OPENING DENOMINATION DETAILS"
                If ConsistancyDone = False Then
                    'Dim InCosAmount As Double
                    'Dim Finalcollection, FinalInput As Decimal
                    'Finalcollection = _TotalTillCollection - _VendorAmount
                    'FinalInput = calculateTotalValue()
                    'InCosAmount = Finalcollection - FinalInput
                    Dim strIncos As String = Check()
                    If strIncos <> String.Empty Then
                        ActivityLogForShift(Nothing, String.Format("ConsistancyDetails {0} ", strIncos), prevShiftId)
                        If Not DisplayCash Then
                            If InCosCheck + InCosCV + InCosGV = 0 Then
                                GoTo CalcCons
                            End If
                        End If
                        If CheckAuthorisation(clsAdmin.UserCode, "SHW_TILL_DECRIP") = True Then
                            If MsgBox(getValueByKey("TLC005") & strIncos, MsgBoxStyle.OkCancel, getValueByKey("CLAE04")) = MsgBoxResult.Ok Then
                                'If CheckInterTransactionAuth("TakeIncosTill") = False Then     Commented By Gaurav Danani
                                '    Exit Sub
                                'End If
                                IncosAmt = IncosAmt + InCosCheck + InCosCV + InCosGV
                                ConsistancyDone = True
                            Else
                                'ClearGrid()
                                _IsNextExited = True
                                dtTender.Reset()
                                Exit Sub

                            End If
                        Else
CalcCons:                   IncosAmt = IncosAmt + InCosCheck + InCosCV + InCosGV
                            ConsistancyDone = True
                        End If
                    End If
                    DTShiftClose = DTMain.Copy
                    ObjclsCommon = New clsCommon()
                    ActivityLogForShift(DTShiftClose, "DENOMINATION :- ", prevShiftId)
                    '----Commented If condition By Mahesh as per Rama Instructions- 06052014
                    'If clsDefaultConfiguration.IsPettyCashApplicable AndAlso clsDefaultConfiguration.IsPettyCashOnSameTerminal AndAlso clsAdmin.TerminalID = clsDefaultConfiguration.PettyCashTerminalId Then                        
                    'Else
                    DTMain = ObjclsCommon.GetDenomination(clsAdmin.CurrencyCode, clsAdmin.CurrencyCode)
                    dgMainGrid.DataSource = DTMain
                    FormatGrid()
                    cbCurrency.SelectedValue = clsAdmin.CurrencyCode
                    CalculateTotal(DTMain)
                    cmdNext.Text = "Previous"
                    cmdNext.Text = getValueByKey("frmntillclosing.cmdnextprevious")
                    cmdNext.Tag = "Previous"

                    'cmdFinsh.Text = "&" & "Finish"
                    cmdFinsh.Text = getValueByKey("frmntillclosing.cmdactualfinsh")
                    cmdFinsh.Tag = "Finish"

                    cbCurrency.Enabled = False
                    sizTop.Enabled = False
                    Me.Text = getValueByKey("frmnshiftclosing")
                    cmdNext.Visible = False
                    cancelBtn.Visible = True
                    txtOperations.Visible = False
                    lblOperation.Visible = False
                    cmdPrint.Visible = False
                    'End If
                Else
                    DTMain = DTExtraAmount
                    cmdNext.Text = getValueByKey("frmntillclosing.cmdnextprevious")
                    cmdNext.Tag = "Previous"
                    cmdFinsh.Text = getValueByKey("frmntillclosing.cmdactualfinsh")
                    'cmdFinsh.Text = "&" & "Finish"
                    cmdFinsh.Tag = "Finish"
                    sizTop.Enabled = False
                    Me.Text = getValueByKey("frmntillclosing2")
                    cbCurrency.SelectedValue = clsAdmin.CurrencyCode
                    cbCurrency.Enabled = False
                    dgMainGrid.DataSource = DTMain
                    CalculateTotal(DTMain)
                    cmdNext.Visible = False
                    txtOperations.Visible = False
                    lblOperation.Visible = False
                End If
            ElseIf sender.tag = "Previous" Then
                DTExtraAmount = DTMain
                DTMain = DTShiftClose
                cmdNext.Text = getValueByKey("frmntillclosing.cmdnext")
                cmdNext.Tag = "Next"
                cmdFinsh.Text = getValueByKey("frmntillclosing.cmdfinshcancel")
                cmdFinsh.Tag = "Cancel"
                sizTop.Enabled = True
                Me.Text = getValueByKey("frmntillclosing1")
                dgMainGrid.DataSource = DTMain
                CalculateTotal(DTMain)
            End If
            FormatGrid()
        Catch ex As Exception

            'ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdReset_Click(sender As System.Object, e As System.EventArgs) Handles cmdReset.Click
        ClearGrid()
        cmdPrint.Visible = False
        ActivityLogForShift(Nothing, "Reset Function" & strDetail, prevShiftId)
    End Sub
    Dim bw As BackgroundWorker
    Public Delegate Sub PictureVisibilityDelegate(ByVal visibility As Integer)
    Dim ChangePictureVisibility As PictureVisibilityDelegate
    'Dim isClosed As Boolean = False
    Dim waitPopupMsg As frmSpecialPrompt
    Private Sub bw_DoWork(sender As Object, e As DoWorkEventArgs)
        'Me.Invoke(ChangePictureVisibility, 0)
        ActivityLogForShift(Nothing, "Start database back up" & strDetail, prevShiftId)
        If DataBaseAutoBackup() = True Then
            ActivityLogForShift(Nothing, "Db back up done" & strDetail, prevShiftId)
            'Me.Invoke(ChangePictureVisibility, 1)
            'ShowMessage("DB Backup successfull on location  " + clsDefaultConfiguration.DatabaseBackupPath, "Information")
        Else
            ActivityLogForShift(Nothing, "Db back up fail" & strDetail, prevShiftId)
            'Me.Invoke(ChangePictureVisibility, 1)
            'ShowMessage("DB Backup failed try again..", "Information")
        End If
    End Sub
    'Private Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)

    '    'Me.Invoke(ChangePictureVisibility, False)
    'End Sub
    Public Sub ChangeVisibility(ByVal steps As Integer)
        If steps = 0 Then
            waitPopupMsg = DayCloseSyncProgress("Please wait while system is backing up the data...", getValueByKey("CLAE04"))
            Application.DoEvents()
        ElseIf steps = 1 Then
            waitPopupMsg.Close()
        End If
    End Sub
    Private Sub cmdFinsh_Click(sender As System.Object, e As System.EventArgs) Handles cmdFinsh.Click
        Try
            If sender.Tag = "Cancel" OrElse DirectCast(sender, Spectrum.CtrlBtn).Tag = "Cancel" Then
                Me.Close()
            Else
                ''-- PettyCashApplicable condition is commented By Mahesh as per Rama Suggestion date 20-05-2014
                'If clsDefaultConfiguration.IsPettyCashApplicable AndAlso clsDefaultConfiguration.IsPettyCashOnSameTerminal AndAlso clsAdmin.TerminalID = clsDefaultConfiguration.PettyCashTerminalId Then
                '    'If clsAdmin.TerminalID = clsDefaultConfiguration.PettyCashTerminalId Then
                '    cmdNext_Click(cmdNext, New EventArgs())
                '    If _IsNextExited Then
                '        _IsNextExited = False
                '        Exit Sub
                '    End If
                '    DTMain = ObjclsCommon.GetDenomination(clsAdmin.CurrencyCode, clsAdmin.CurrencyCode)
                '    'End If
                'End If
                If clsDefaultConfiguration.AutoDBBackupOnShiftClose Then
                    bw = New BackgroundWorker
                    bw.WorkerSupportsCancellation = True
                    AddHandler bw.DoWork, AddressOf bw_DoWork
                    ChangePictureVisibility = AddressOf ChangeVisibility
                    If Not bw.IsBusy = True Then
                        bw.RunWorkerAsync()
                    End If
                End If
                Dim objTill As New clsShift()
                If objTill.CheckOpening(clsAdmin.TerminalID, clsAdmin.SiteCode, "Close") Then
                    'ShowMessage("Shift Closed already done for this terminal", "Information")
                    ShowMessage(getValueByKey("SOC06"), getValueByKey("CLAE04"))
                    Me.Close()
                    Exit Sub
                End If
                ActivityLogForShift(DTMain, strDetail, prevShiftId)
                If DTMain IsNot Nothing Then
                    Dim totalNextDayFloatAmt As Double = DTMain.Compute("SUM(AMount)", "")
                    If totalNextDayFloatAmt > clsDefaultConfiguration.MaxNextDayFloatAmount Then
                        ShowMessage("Max limit for next day float is " & clsDefaultConfiguration.MaxNextDayFloatAmount.ToString() & "", getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                End If
                Dim dt As New DataTable
                dt = clsCommon.GetPreviousShiftDetails(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID)
                ActivityLogForShift(dt, "GetPreviousShiftDetails() ", prevShiftId)
                'Dim objTill As New clsShift()
                objTill.DeleteNextDayFloatIfExist(clsAdmin.SiteCode, clsAdmin.TerminalID, dt.Rows(0)("ShiftId").ToString())
                If cmdNext.Tag = "Previous" Or cmdNext.Visible = False Then
                    If CheckNextDayValue() = True Then
                        DTExtraAmount = DTMain.Copy
                    Else
                        ShowMessage(getValueByKey("TLC008"), getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                End If
                'If EditMode = True Then
                '    Dim objTill As New clsTill()
                '    If objTill.RemoveData(clsAdmin.TerminalID, clsAdmin.SiteCode, Now) = False Then
                '        ShowMessage("Error in removing Previous Details", "Information")
                '        Exit Sub
                '    End If
                'End If
                If Not DTExtraAmount Is Nothing Then
                    dtNextDayAmount = objTill.GetNextDayFloatingDetailStruc()
                    Call AddRows(DTExtraAmount, "Open", dt.Rows(0)("ShiftId").ToString())
                    Call AddRows(DTExtraAmount, "Open", dt.Rows(0)("ShiftId").ToString(), dtNextDayAmount)
                End If
                If Not DTShiftClose Is Nothing Then
                    Call AddRows(DTShiftClose, "Close", dt.Rows(0)("ShiftId").ToString())
                End If
                BuildTillCloseDtl(dt.Rows(0)("ShiftId").ToString())
                If Not dtNextDayAmount Is Nothing AndAlso dtNextDayAmount.Rows.Count > 0 Then
                    dtNextDayAmount.TableName = "NextDayFloatingDetail"
                    dsTill.Tables.Add(dtNextDayAmount)
                End If
                Dim dsTemp As DataSet = dsTill.Copy()
                If objTill.SaveShiftClose(dsTemp, clsAdmin.SiteCode, dt.Rows(0)("ShiftId").ToString(), clsAdmin.TerminalID, clsAdmin.DayOpenDate, clsAdmin.UserCode, ConsistancyDone, "TillClose", IncosAmt, ServerDate, dtTender) = True Then
                    clsDefaultConfiguration.TillOpenDone = False
                    '--------------Display labels
                    Dim data As DataTable = clsCommon.GetShiftName(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID)
                    If data.Rows.Count > 0 Then
                        clsAdmin.ShiftName = data.Rows(0)("ShiftName").ToString()
                        clsAdmin.ShiftStatus = data.Rows(0)("OpenCloseStatus").ToString()
                        clsAdmin.DisplayShift = True
                    End If

                    ShowMessage(String.Format("{0} Closed Successfully", clsAdmin.ShiftName), getValueByKey("CLAE04"))
                    GenerateShiftCloseReport(dt.Rows(0)("ShiftId").ToString())
                    '-------Code Added By Mahesh (Sameer code from frmTillFinancialReport ) to show petty Cash Expenses /Reciept
                    Dim PettyCashExp As String = String.Empty
                    Dim PettyCashRec As String = String.Empty
                    If clsDefaultConfiguration.IsPettyCashApplicable AndAlso clsDefaultConfiguration.IsPettyCashOnSameTerminal AndAlso clsAdmin.TerminalID = clsDefaultConfiguration.PettyCashTerminalId Then
                        Dim request As New SpectrumCommon.GetVoucherBalanceRequest()
                        request.DayOpenDate = clsAdmin.DayOpenDate
                        request.FinYear = clsAdmin.Financialyear
                        request.SiteCode = clsAdmin.SiteCode
                        Dim voucher As IPettyCashVoucher = New PettyCashVoucher()
                        PettyCashRec = voucher.GetTotalPettyCashReceipt(request).ToString()
                        PettyCashExp = voucher.GetTotalPettyCashExpense(request).ToString()
                    End If

                    'Dim objprint As New clsPrintFinancialReport(clsDefaultConfiguration.TillClosePrintPreivewReq, clsDefaultConfiguration.UserAmountPrint)
                    'objprint.PrintFinancialReport(clsAdmin.TerminalID, clsAdmin.DayOpenDate, clsAdmin.SiteCode, _dtPayment, _dtCash, _VendorAmount, gPrinterName, lblTotalAmount.Text, dtPrinterInfo, PettyCashExp, PettyCashRec, clsAdmin.UserName)

                    'After Till Closed, All transactiom menu will be disabled
                    DisableTransactionMainMenu(False)
                    MDISpectrum.DayOpenMenuItem.Enabled = True
                    MDISpectrum.DayCloseToolStripMenuItem.Enabled = True
                    MDISpectrum.ShiftOpen.Enabled = True

                    'Dim objDayCLose As New clsDayClose
                    'If objDayCLose.CheckIfValidDayClose(clsAdmin.SiteCode, clsAdmin.Financialyear, clsAdmin.DayOpenDate) Then

                    '    If (MessageBox.Show(String.Format(getValueByKey("LG022"), clsAdmin.DayOpenDate.ToString("dd/MM/yyyy")), "LG022 - " & getValueByKey("CLAE04"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Cancel) Then
                    '        Me.Close()
                    '        Exit Sub
                    '    End If

                    '    Dim objDefault As New clsDefaultConfiguration("DC")
                    '    objDefault.GetDefaultSettings()
                    '    If clsDefaultConfiguration.DayCloseOtherScreens = False Then
                    '        If objDayCLose.CheckIfAllTerminalAreClosed(clsAdmin.SiteCode) = False Then
                    '            ShowMessage(getValueByKey("mdispectrum.dayclosevalidationmsg"), getValueByKey("CLAE04"))
                    '            Exit Sub
                    '        End If
                    '    End If

                    '    Dim childForm As New Spectrum.frmDayCloseMain
                    '    Try
                    '        childForm.Show()
                    '        childForm.GetNextScreen()
                    '    Catch ex As Exception
                    '        childForm.Close()
                    '    End Try
                    'End If
                Else
                    ShowMessage(getValueByKey("SOC02"), getValueByKey("CLAE04"))
                    Exit Sub
                End If
                'If ConsistancyDone = True Then
                DTExtraAmount.Rows.Clear()
                DTShiftClose.Rows.Clear()
                dtNextDayAmount.Rows.Clear()
                'End If
                ' If EditMode = False Then
                'CloseTerminal()
                'End If
                Me.Close()
                '''''''''''''''''''  mayur till open
                If clsDefaultConfiguration.SpectrumLiteAllowed Then
                    For Each form In My.Application.OpenForms
                        If (form.name = frmDayCloseMain.Name) Then
                            Exit Sub
                        End If
                    Next

                    Dim objDayCLose As New clsDayClose
                    If objDayCLose.CheckIfValidDayClose(clsAdmin.SiteCode, clsAdmin.Financialyear, clsAdmin.DayOpenDate) Then

                        'Dim dtDayOpenStatus As DataTable = objLogin.GetDayCloseStatus(clsAdmin.SiteCode, clsAdmin.Financialyear)

                        'If dtDayOpenStatus IsNot Nothing AndAlso dtDayOpenStatus.Rows.Count > 0 Then
                        '    AnyTerminalOpen = dtDayOpenStatus.Rows(0)("AnyTerminalOpen")

                        '    If (AnyTerminalOpen > 0) Then
                        '        ShowMessage(True, getValueByKey("LG021"), "LG021 - " & getValueByKey("CLAE04"))
                        '        Exit Sub
                        '    End If
                        'End If

                        If (MessageBox.Show(String.Format(getValueByKey("LG022"), clsAdmin.DayOpenDate.ToString("dd/MM/yyyy")), "LG022 - " & getValueByKey("CLAE04"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Cancel) Then
                            Exit Sub
                        End If

                        Dim objDefault As New clsDefaultConfiguration("DC")
                        objDefault.GetDefaultSettings()
                        If clsDefaultConfiguration.DayCloseOtherScreens = False Then
                            If objDayCLose.CheckIfAllTerminalAreClosed(clsAdmin.SiteCode) = False Then
                                ShowMessage(getValueByKey("mdispectrum.dayclosevalidationmsg"), getValueByKey("CLAE04"))
                                Exit Sub
                            End If
                        End If
                        Dim childForm As New Spectrum.frmDayCloseMain
                        Try
                            'childForm.MdiParent = Me
                            childForm.Show()
                            childForm.GetNextScreen()

                        Catch ex As Exception
                            childForm.Close()
                        End Try
                    Else
                        ShowMessage(String.Format(getValueByKey("mdispectrum.daycloseperformedmsg"), clsAdmin.DayOpenDate.ToShortDateString()), getValueByKey("CLAE04"))
                    End If
                    ''''''''''''''''''' mayur till close
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("PRIMARY KEY") Then
                ShowMessage(getValueByKey("SOC03"), getValueByKey("CLAE05"))
            Else
                ShowMessage(ex.Message, getValueByKey("CLAE05"))
            End If
            'ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
            Me.Close()
        End Try
    End Sub

    Private Function GenerateShiftCloseReport(ByVal shiftid As String) As Boolean
        Try
            ObjclsCommon = New clsCommon
            Dim shiftName As String
            shiftName = ObjclsCommon.GetShiftNameForPrint(shiftid, clsAdmin.SiteCode)
            Dim PrintFormat As String = ObjclsCommon.GetTillClosePrintFormat(clsAdmin.TerminalID)
            Dim dsDsr As New DataSet()
            Dim clsObj As New ShiftCloseReport
            dsDsr = clsObj.GetShiftCloseReportData(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID, shiftid)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = ""

            If PrintFormat = "0" Then
                appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\shiftclose.rdl")
            Else
                appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\shiftclosecounter.rdl")
            End If


            reportViewer2.LocalReport.ReportPath = appPath

            Dim dateNameParam As New ReportParameter("V_DayCloseDate", clsAdmin.DayOpenDate)
            Dim SiteCodeParam As New ReportParameter("V_SiteCode", clsAdmin.SiteCode)
            Dim TerminalParm As New ReportParameter("V_TerminalId", clsAdmin.TerminalID)
            Dim ShiftParm As New ReportParameter("V_ShiftId", shiftid)
            Dim ShiftOpenDateParm As New ReportParameter("P_ShiftOpenDateTime", clsAdmin.DayOpenDate)
            Dim ShiftCloseDateParm As New ReportParameter("P_ShiftCloseDateTime", clsAdmin.DayOpenDate)

            reportViewer2.LocalReport.SetParameters(New ReportParameter() {dateNameParam, SiteCodeParam, TerminalParm, ShiftParm, ShiftOpenDateParm, ShiftCloseDateParm})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()

            If PrintFormat = "0" Then
                Dim DataSource As New ReportDataSource("ReportHeaderData", dsDsr.Tables(clsObj.ShiftCloseEnum.ReportHeaderData))
                Dim DataSource1 As New ReportDataSource("NetSaleData", dsDsr.Tables(clsObj.ShiftCloseEnum.NetSaleDataShop))
                Dim DataSource2 As New ReportDataSource("NetCashData", dsDsr.Tables(clsObj.ShiftCloseEnum.NetCashData))
                Dim DataSource3 As New ReportDataSource("ShiftWiseCashierData", dsDsr.Tables(clsObj.ShiftCloseEnum.ShiftWiseCashierData))
                Dim DataSource4 As New ReportDataSource("SummaryData", dsDsr.Tables(clsObj.ShiftCloseEnum.SummaryData))
                Dim DataSource5 As New ReportDataSource("SalesOrderData", dsDsr.Tables(clsObj.ShiftCloseEnum.SalesOrderData))
                Dim DataSource6 As New ReportDataSource("CorrectedCashMemoData", dsDsr.Tables(clsObj.ShiftCloseEnum.CorrectedCashMemoData))
                Dim DataSource7 As New ReportDataSource("DeletedCashMemoData", dsDsr.Tables(clsObj.ShiftCloseEnum.DeletedCashMemoData))
                Dim DataSource8 As New ReportDataSource("SaleReturnData", dsDsr.Tables(clsObj.ShiftCloseEnum.SaleReturnData))
                Dim DataSource9 As New ReportDataSource("OpeningData", dsDsr.Tables(clsObj.ShiftCloseEnum.OpeningData))
                Dim DataSource10 As New ReportDataSource("ClosingData", dsDsr.Tables(clsObj.ShiftCloseEnum.ClosingData))

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
                reportViewer2.LocalReport.DataSources.Add(DataSource10)



            Else
                Dim DataSource As New ReportDataSource("ReportHeaderData", dsDsr.Tables(clsObj.ShiftCloseEnum.ReportHeaderData))
                Dim DataSource1 As New ReportDataSource("NetSaleData", dsDsr.Tables(clsObj.ShiftCloseEnum.NetSaleDataShop))
                Dim DataSource2 As New ReportDataSource("NetCashData", dsDsr.Tables(clsObj.ShiftCloseEnum.NetCashData))
                Dim DataSource3 As New ReportDataSource("ShiftWiseCashierData", dsDsr.Tables(clsObj.ShiftCloseEnum.ShiftWiseCashierData))
                Dim DataSource4 As New ReportDataSource("SummaryData", dsDsr.Tables(clsObj.ShiftCloseEnum.SummaryData))


                Dim DataSource5 As New ReportDataSource("CorrectedCashMemoData", dsDsr.Tables(clsObj.ShiftCloseEnum.CorrectedCashMemoData))
                Dim DataSource6 As New ReportDataSource("DeletedCashMemoData", dsDsr.Tables(clsObj.ShiftCloseEnum.DeletedCashMemoData))
                Dim DataSource7 As New ReportDataSource("SaleReturnData", dsDsr.Tables(clsObj.ShiftCloseEnum.SaleReturnData))

                Dim DataSource8 As New ReportDataSource("OpeningData", dsDsr.Tables(clsObj.ShiftCloseEnum.OpeningData))
                Dim DataSource9 As New ReportDataSource("ClosingData", dsDsr.Tables(clsObj.ShiftCloseEnum.ClosingData))

                Dim DataSource10 As New ReportDataSource("ItemWiseSalesData", dsDsr.Tables(clsObj.ShiftCloseEnum.ItemWiseSalesData))
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
                reportViewer2.LocalReport.DataSources.Add(DataSource10)

            End If

            Dim SiteName As String = ""

            SiteName = dsDsr.Tables(clsObj.ShiftCloseEnum.ReportHeaderData).Rows(0)("Site")

            reportViewer2.Refresh()
            Dim path As String
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            path = clsDefaultConfiguration.DayCloseReportPath & "\" & SiteName & "_ShiftCloseReport_" & shiftName & "_" & clsAdmin.DayOpenDate.ToString("dd-MM-yyyy") & ".pdf"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using

            Process.Start(path)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Function calculateTotalValue() As Decimal
        Try
            Dim TotalValue As Decimal = 0
            If txtCheckAmount.Text <> String.Empty Then
                TotalValue = TotalValue + CDbl(txtCheckAmount.Text)
            End If
            If txtCVAmount.Text <> String.Empty Then
                TotalValue = TotalValue + CDbl(txtCVAmount.Text)
            End If
            If txtGVAmount.Text <> String.Empty Then
                TotalValue = TotalValue + CDbl(txtGVAmount.Text)
            End If
            Dim TotalCash As Decimal = 0
            Dim dv As New DataView(DTMain, "", "", DataViewRowState.CurrentRows)
            Dim dtUnique As DataTable = dv.ToTable(True, "CURRENCYCODE")
            For Each dr As DataRow In dtUnique.Rows
                dv.RowFilter = "CURRENCYCODE='" & dr(0).ToString() & "' And Amount > 0"
                For Each drow As DataRowView In dv
                    TotalCash = TotalCash + drow("Amount")
                Next
            Next
            TotalValue = TotalValue + TotalCash
            Return TotalValue
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
            Return 0
        End Try
    End Function
    Private Function CheckNextDayValue() As Boolean
        Try
            Dim dvCheck As New DataView(DTShiftClose, "", "", DataViewRowState.CurrentRows)
            For Each dr As DataRow In DTMain.Select("isnull(AMount,0)>0", "DenominationAmt", DataViewRowState.CurrentRows)
                dvCheck.RowFilter = "DenominationAmt=" & ConvertToEnglish(dr("DenominationAmt")) & " AND Qty >= " & dr("Qty")
                If dvCheck.Count = 0 Then
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub AddRows(ByVal DTTable As DataTable, ByVal ActionType As String, ByVal ShiftId As Integer, Optional ByRef dtNextDayAmount As DataTable = Nothing)
        Try
            Dim GenDataRow As DataRow
            Dim i As Integer = 0
            ServerDate = ObjclsCommon.GetCurrentDate()
            For i = 0 To DTTable.Rows.Count - 1
                If CDbl(DTTable.Rows(i).Item("Qty").ToString()) > 0 Then
                    If dtNextDayAmount Is Nothing Then
                        GenDataRow = DTFloatingDetail1.NewRow()
                    Else
                        GenDataRow = dtNextDayAmount.NewRow()
                    End If
                    GenDataRow.Item("SiteCode") = clsAdmin.SiteCode
                    GenDataRow.Item("TerminalId") = clsAdmin.TerminalID
                    GenDataRow.Item("Action") = ActionType
                    GenDataRow.Item("FlotDateTime") = clsAdmin.DayOpenDate
                    GenDataRow.Item("CREATEDON") = ServerDate
                    GenDataRow.Item("UPDATEDON") = ServerDate
                    GenDataRow.Item("CurrencyCode") = DTTable.Rows(i).Item("CurrencyCode")
                    GenDataRow.Item("DenominationAmt") = DTTable.Rows(i).Item("DenominationAmt")
                    GenDataRow.Item("Qty") = DTTable.Rows(i).Item("Qty")
                    GenDataRow.Item("TotalAmount") = DTTable.Rows(i).Item("AMOUNT")
                    GenDataRow.Item("CreatedAt") = clsAdmin.SiteCode
                    GenDataRow.Item("CreatedBY") = clsAdmin.UserCode
                    GenDataRow.Item("UpdatedAt") = clsAdmin.SiteCode
                    GenDataRow.Item("UpdatedBY") = clsAdmin.UserCode
                    GenDataRow.Item("ShiftId") = ShiftId
                    If ActionType = "Open" Then
                        GenDataRow.Item("Status") = 0
                        If dtNextDayAmount Is Nothing Then
                            GenDataRow.Item("FlotDateTime") = "9999-12-1"    'DateAdd(DateInterval.Day, 1, clsAdmin.DayOpenDate) 
                        Else
                            GenDataRow.Item("FlotDateTime") = clsAdmin.DayOpenDate
                        End If
                    Else
                        GenDataRow.Item("Status") = 1
                    End If
                    If dtNextDayAmount Is Nothing Then
                        DTFloatingDetail1.Rows.Add(GenDataRow)
                    Else
                        dtNextDayAmount.Rows.Add(GenDataRow)
                    End If
                End If
            Next
            'TAFloatingDetail.Update(DTFloatingDetail1)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    'Private Sub CloseTerminal()
    '    Try
    '        Dim objTill As New clsTill()
    '        If objTill.OpenCloseTerminal(clsAdmin.TerminalID, clsAdmin.SiteCode, False) = True Then
    '            ShowMessage("Terminal is Closed now", "Information")
    '        End If
    '    Catch ex As Exception
    '        ShowMessage(ex.Message, "Error")
    '        LogException(ex)
    '    End Try
    'End Sub
    Private Sub txtCVAmount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCVAmount.KeyDown, txtCheckAmount.KeyDown, txtGVAmount.KeyDown
        If (e.KeyValue >= 48 And e.KeyValue < 58) Or (e.KeyValue >= 96 And e.KeyValue < 106) Or e.KeyValue = 8 Or e.KeyValue = 32 Or e.KeyValue = 46 Or e.KeyValue = 190 Or e.KeyValue = 110 Or e.KeyValue = 188 Then
            e.SuppressKeyPress = False
        Else
            e.SuppressKeyPress = True
        End If
    End Sub
    Private Sub FormatGrid()
        Try
            dgMainGrid.Cols("DENOMINATION").AllowEditing = False
            dgMainGrid.Cols("AMOUNT").AllowEditing = False
            dgMainGrid.Cols("DENOMINATIONAMT").Visible = False
            dgMainGrid.Cols("CURRENCYSYMBOL").Visible = False
            dgMainGrid.Cols("DENOMINATION").Caption = getValueByKey("frmntillclosing.dgmaingrid.denomination")
            dgMainGrid.Cols("AMOUNT").Caption = getValueByKey("frmntillclosing.dgmaingrid.amount")

            dgMainGrid.Cols("AMOUNT").Format = "0.00"


            dgMainGrid.Cols("QTY").Caption = getValueByKey("frmntillclosing.dgmaingrid.qty")
            dgMainGrid.Cols("QTY").Format = "0"
            dgMainGrid.Cols("QTY").WidthDisplay = 100
            dgMainGrid.Cols("CURRENCYCODE").Visible = False
            dgMainGrid.Cols("BASEAMOUNT").Visible = False
            dgMainGrid.Cols("EXCHANGERATE").Visible = False
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub CalculateTotal(ByVal DTTable As DataTable)
        Try
            If Not DTTable Is Nothing AndAlso DTTable.Rows.Count > 0 Then
                lblTotalAmount.Text = DTTable.Compute("SUM(AMOUNT)", "isnull(CURRENCYCODE,'')='" & IIf(cbCurrency.SelectedValue Is Nothing, "", cbCurrency.SelectedValue) & "'")
                lblTotalAmount.Text = FormatNumber(CDbl(lblTotalAmount.Text), 2)
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub ClearGrid()
        Try
            For Each row As C1.Win.C1FlexGrid.Row In dgMainGrid.Rows
                If row.Index > 0 Then
                    row("Qty") = 0
                    row("Amount") = 0
                End If
                lblTotalAmount.Text = ""
            Next
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub BuildTillCloseDtl(ByVal shiftid As Integer)
        Try
            dsTill = New DataSet
            Dim objTill As New clsShift
            If _VendorAmount <> 0 Or txtCheckAmount.Text <> String.Empty Or txtCVAmount.Text <> String.Empty Or txtGVAmount.Text <> String.Empty Then
                dtTill = objTill.TillCloseStru(clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.DayOpenDate, ServerDate, shiftid)
                Dim dr As DataRow = dtTill.NewRow
                dr("VendorAmt") = _VendorAmount
                dr("GVAmount") = IIf(txtGVAmount.Text = String.Empty, 0, txtGVAmount.Text)
                dr("CVAmount") = IIf(txtCVAmount.Text = String.Empty, 0, txtCVAmount.Text)
                dr("CheckAmt") = IIf(txtCheckAmount.Text = String.Empty, 0, txtCheckAmount.Text)
                dr("CreatedBy") = clsAdmin.UserCode
                dr("UpdatedBy") = clsAdmin.UserCode
                dtTill.Rows.Add(dr)
            End If
            If Not dtTill Is Nothing AndAlso dtTill.Rows.Count > 0 Then
                dsTill.Tables.Add(dtTill)
            End If

            If (DTShiftClose IsNot Nothing) Then
                Dim UserEnteredCash As Double = IIf(DTShiftClose.Compute("SUM(BASEAMOUNT)", "") Is DBNull.Value, 0, DTShiftClose.Compute("SUM(BASEAMOUNT)", ""))
                Dim DC As DataColumn = New DataColumn("UserAmount", Type.GetType("System.Decimal"))
                _dtPayment.Columns.Add(DC)
                '_dtPayment.Columns.Add(SI)
                'Dim SI As DataColumn = New DataColumn("ShiftId", Type.GetType("System.Int32"))
                '_dtCash.Columns.Add(SI)
                _dtCash.TableName = "TillCloseCashDtl"
                _dtPayment.TableName = "TillCloseFinanceDtl"
                DTShiftClose.TableName = "FloatingDetail"
                DTShiftClose.Merge(DTExtraAmount, False, MissingSchemaAction.Ignore)
                dsTill.Tables.Add(_dtCash)
                dsTill.Tables.Add(_dtPayment)
                dsTill.Tables.Add(DTFloatingDetail1)
                'dsTill.Tables.Add(DTExtraAmount)
                Dim dv As New DataView(_dtPayment, "", "", DataViewRowState.CurrentRows)
                If txtCheckAmount.Text <> String.Empty Then
                    dv.RowFilter = "TENDERTYPE='Cheque'"
                    If dv.Count > 0 Then
                        dv(0)("UserAmount") = txtCheckAmount.Text
                    End If
                End If
                If txtCVAmount.Text <> String.Empty Then
                    dv.RowFilter = "TENDERTYPE='CreditVouc(R)'"
                    If dv.Count > 0 Then
                        dv(0)("UserAmount") = txtCVAmount.Text
                    End If
                End If
                If txtGVAmount.Text <> String.Empty Then
                    dv.RowFilter = "TENDERTYPE='GiftVoucher(R)'"
                    If dv.Count > 0 Then
                        dv(0)("UserAmount") = txtGVAmount.Text
                    End If
                End If
                dv.RowFilter = "TENDERTYPE='Cash'"
                If dv.Count > 0 Then
                    dv(0)("UserAmount") = UserEnteredCash
                End If
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Function Check() As String
        Dim str As String = ""
        Try
            IncosAmt = 0
            'Dim dtTender As New DataTable
            dtTender.Columns.Add("TenderCode", GetType(System.String))
            dtTender.Columns.Add("Amount", GetType(System.Int32))

            txtCheckAmount.Text = IIf(txtCheckAmount.Text = String.Empty, 0, txtCheckAmount.Text)
            txtCVAmount.Text = IIf(txtCVAmount.Text = String.Empty, 0, txtCVAmount.Text)
            txtGVAmount.Text = IIf(txtGVAmount.Text = String.Empty, 0, txtGVAmount.Text)

            If _GVAmount <> txtGVAmount.Text Then
                InCosGV = _GVAmount - txtGVAmount.Text
                InCosGV = FormatNumber(InCosGV, 2)
                str = "GiftVoucher : " & InCosGV.ToString & vbCrLf
            End If
            '-----------------------------------------------------------------------------------------------
            Dim R As DataRow = dtTender.NewRow
            R("TenderCode") = "GiftVoucher"
            R("Amount") = InCosGV
            dtTender.Rows.Add(R)

            If _CVAmount <> txtCVAmount.Text Then
                InCosCV = _CVAmount - txtCVAmount.Text
                InCosCV = FormatNumber(InCosCV, 2)
                str = str & "CreditVoucher : " & InCosCV.ToString & vbCrLf
            End If
            '-----------------------------------------------------------------------------------------------
            Dim R1 As DataRow = dtTender.NewRow
            R1("TenderCode") = "CreditVoucher"
            R1("Amount") = InCosCV
            dtTender.Rows.Add(R1)

            If _Check <> txtCheckAmount.Text Then
                InCosCheck = _Check - txtCheckAmount.Text
                InCosCheck = FormatNumber(InCosCheck, 2)
                str = str & "Cheque : " & InCosCheck.ToString & vbCrLf
            End If
            '-----------------------------------------------------------------------------------------------
            Dim R2 As DataRow = dtTender.NewRow
            R2("TenderCode") = "Cheque"
            R2("Amount") = InCosCheck
            dtTender.Rows.Add(R2)

            Dim sysAmount, UserAmount As Object
            Dim SysAmt, UserAmt, ExchangeRate As Double
            For Each dr As DataRow In DtCurrency.Rows
                SysAmt = 0 : UserAmt = 0
                ExchangeRate = 1
                sysAmount = _dtCash.Compute("Sum(Amount)", "CurrencyCode='" & dr("CurrencyCode").ToString() & "'")
                If clsDefaultConfiguration.IsPettyCashApplicable AndAlso clsDefaultConfiguration.IsPettyCashOnSameTerminal AndAlso clsAdmin.TerminalID = clsDefaultConfiguration.PettyCashTerminalId Then
                    'If clsAdmin.TerminalID = clsDefaultConfiguration.PettyCashTerminalId Then
                    Dim objPettyCash As IPettyCashVoucher = New PettyCashVoucher()
                    Dim reqest As New GetVoucherBalanceRequest
                    reqest.SiteCode = clsAdmin.SiteCode
                    reqest.FinYear = clsAdmin.Financialyear
                    reqest.DayOpenDate = clsAdmin.DayOpenDate
                    '  Dim pettyCashexpense As Decimal = objPettyCash.GetTotalPettyCashExpense(reqest)
                    ' Dim pettyCashReceipt As Decimal = objPettyCash.GetTotalPettyCashReceipt(reqest)
                    sysAmount = sysAmount '+ pettyCashReceipt - pettyCashexpense
                    'End If
                End If
                UserAmount = DTMain.Compute("Sum(Amount)", "CurrencyCode='" & dr("CurrencyCode").ToString() & "'")
                For Each drex As DataRow In _dtCash.Select("CurrencyCode='" & dr("CurrencyCode").ToString() & "'")
                    ExchangeRate = IIf(drex("Rate") Is DBNull.Value, 0, drex("Rate"))
                    Exit For
                Next
                If ExchangeRate = 0 Then
                    For Each drex As DataRow In DTMain.Select("CurrencyCode='" & dr("CurrencyCode").ToString() & "'")
                        ExchangeRate = IIf(drex("EXCHANGERATE") Is DBNull.Value, 0, drex("EXCHANGERATE"))
                        Exit For
                    Next
                End If
                If Not sysAmount Is DBNull.Value Then
                    SysAmt = CDbl(sysAmount)
                End If
                If Not UserAmount Is DBNull.Value Then
                    UserAmt = CDbl(UserAmount)
                End If
                If dr("CurrencyCode").ToString() = clsAdmin.CurrencyCode Then
                    SysAmt = SysAmt - _VendorAmount
                    'SysAmt = SysAmt + _float
                End If
                SysAmt = FormatNumber(SysAmt, 2)
                UserAmt = FormatNumber(UserAmt, 2)
                If SysAmt <> UserAmt Then
                    InCosCash = SysAmt - UserAmt
                    InCosCash = FormatNumber(InCosCash, 2)
                    Dim InCosCashFinal As Decimal
                    If DisplayCash Then
                        InCosCashFinal = InCosCash
                        str = str & dr("CURRENCYDESCRIPTION").ToString() & " : " & InCosCashFinal
                    Else
                        InCosCashFinal = InCosCash - (SysAmt - UserAmt)
                    End If
                    IncosAmt = IncosAmt + (InCosCash * ExchangeRate)
                    '-----------------------------------------------------------------------------------------------
                    Dim R3 As DataRow = dtTender.NewRow
                    R3("TenderCode") = "Cash"
                    R3("Amount") = InCosCash
                    dtTender.Rows.Add(R3)
                End If
            Next
            Return str
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub New()
        If CheckAuthorisation(clsAdmin.UserCode, "TillOpenNClose") = False Then
            ShowMessage(getValueByKey("TLC001"), getValueByKey("CLAE04"))
            'ShowMessage("You dont have sufficient rights for this operation", "Information")
            Me.Dispose()
            Me.Close()
            Exit Sub
        End If
        Dim objTill As New clsTill()
        If objTill.CheckOpening(clsAdmin.TerminalID, clsAdmin.SiteCode, "Open") = False Then
            ShowMessage(getValueByKey("SOC04"), getValueByKey("CLAE04"))
            Me.Dispose()
            Me.Close()
            Exit Sub
        End If
        If objTill.CheckOpening(clsAdmin.TerminalID, clsAdmin.SiteCode, "Close") Then
            ShowMessage(getValueByKey("SOC03"), getValueByKey("CLAE04"))
            Me.Dispose()
            Me.Close()
            Exit Sub
        End If
        Me.Tag = "TillClose"
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub
    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        gbDenomination.ForeColor = Color.White
        gbDenomination.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmdNext.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdNext.BackColor = Color.Transparent
        cmdNext.BackColor = Color.FromArgb(0, 107, 163)
        cmdNext.ForeColor = Color.FromArgb(255, 255, 255)
        cmdNext.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdNext.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdNext.FlatStyle = FlatStyle.Flat
        cmdNext.FlatAppearance.BorderSize = 0
        cmdNext.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdPrint.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdPrint.BackColor = Color.Transparent
        cmdPrint.BackColor = Color.FromArgb(0, 107, 163)
        cmdPrint.ForeColor = Color.FromArgb(255, 255, 255)
        cmdPrint.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdPrint.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdPrint.FlatStyle = FlatStyle.Flat
        cmdPrint.FlatAppearance.BorderSize = 0
        cmdPrint.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        cancelBtn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cancelBtn.BackColor = Color.Transparent
        cancelBtn.BackColor = Color.FromArgb(0, 107, 163)
        cancelBtn.ForeColor = Color.FromArgb(255, 255, 255)
        cancelBtn.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cancelBtn.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cancelBtn.FlatStyle = FlatStyle.Flat
        cancelBtn.FlatAppearance.BorderSize = 0
        cancelBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)



        cmdReset.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdReset.BackColor = Color.Transparent
        cmdReset.BackColor = Color.FromArgb(0, 107, 163)
        cmdReset.ForeColor = Color.FromArgb(255, 255, 255)
        cmdReset.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdReset.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdReset.FlatStyle = FlatStyle.Flat
        cmdReset.FlatAppearance.BorderSize = 0
        cmdReset.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)


        cmdFinsh.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdFinsh.BackColor = Color.Transparent
        cmdFinsh.BackColor = Color.FromArgb(0, 107, 163)
        cmdFinsh.ForeColor = Color.FromArgb(255, 255, 255)
        cmdFinsh.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdFinsh.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdFinsh.FlatStyle = FlatStyle.Flat
        cmdFinsh.FlatAppearance.BorderSize = 0
        cmdFinsh.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        dgMainGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        dgMainGrid.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgMainGrid.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        dgMainGrid.Rows.MinSize = 25
        dgMainGrid.Styles.Normal.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        dgMainGrid.Styles.Fixed.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgMainGrid.Styles.Highlight.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgMainGrid.Styles.Highlight.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgMainGrid.Styles.Focus.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgMainGrid.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)

        lblHeader.ForeColor = Color.White
        lblHeader.BackColor = Color.Transparent
        lblGVreceived.AutoSize = False
        lblGVreceived.BackColor = Color.FromArgb(212, 212, 212)
        lblGVreceived.Size = New Size(260, 23)
        lblGVreceived.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        lblCVReceived.AutoSize = False
        lblCVReceived.BackColor = Color.FromArgb(212, 212, 212)
        lblCVReceived.Size = New Size(260, 23)
        lblCVReceived.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        lblCheckRecevied.AutoSize = False
        lblCheckRecevied.BackColor = Color.FromArgb(212, 212, 212)
        lblCheckRecevied.Size = New Size(260, 23)
        lblCheckRecevied.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        CtrlLabel3.AutoSize = False
        CtrlLabel3.BackColor = Color.FromArgb(212, 212, 212)
        '  CtrlLabel3.Size = New Size(260, 23)
        CtrlLabel3.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        lblOperation.AutoSize = False
        lblOperation.BackColor = Color.FromArgb(212, 212, 212)
        lblOperation.Size = New Size(270, 23)
        lblOperation.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        lblAmount.AutoSize = False
        lblAmount.BackColor = Color.FromArgb(212, 212, 212)
        lblAmount.Size = New Size(270, 23)
        lblAmount.Font = New Font("Neo Sans", 9, FontStyle.Bold)


        lblTotalAmount.BackColor = Color.FromArgb(255, 255, 255)

    End Function
End Class