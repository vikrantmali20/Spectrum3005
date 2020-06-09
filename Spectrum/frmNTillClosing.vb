﻿Imports System.Data
Imports System.Resources
Imports SpectrumBL
Imports System.Globalization
Imports System.Data.SqlClient
Imports SpectrumBL.POSDBDataSet
Imports SpectrumPrint
Imports SpectrumCommon
Imports Spectrum.SpectrumUpdate
Imports System.Text
Imports System.IO
Public Class frmNTillClosing
    Dim DTMain, DTTillClose, DTExtraAmount, DtCurrency, dtTill, dtNextDayAmount As DataTable
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
    Private directoryPath As String = Application.StartupPath
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
    Private Sub cbCurrency_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbCurrency.KeyPress
        Try
            e.Handled = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub frmNTillClosing_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            'cmdFinsh.Text = "&Cancel"              
            cmdNext.Tag = "Next"
            cmdReset.Tag = "Reset"
            cmdFinsh.Tag = "Cancel"
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
    Private Sub cmdReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReset.Click
        ClearGrid()
    End Sub
    Private _IsNextExited As Boolean = False
    Private Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        Try
            If sender.tag = "Next" Then
                If ConsistancyDone = False Then
                    'Dim InCosAmount As Double
                    'Dim Finalcollection, FinalInput As Decimal
                    'Finalcollection = _TotalTillCollection - _VendorAmount
                    'FinalInput = calculateTotalValue()
                    'InCosAmount = Finalcollection - FinalInput
                    Dim strIncos As String = Check()
                    If strIncos <> String.Empty Then
                        If CheckAuthorisation(clsAdmin.UserCode, "SHW_TILL_DECRIP") = True Then
                            If Not DisplayCash Then
                                If InCosCheck + InCosCV + InCosGV = 0 Then
                                    GoTo CalcCons
                                End If
                            End If
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
                    DTTillClose = DTMain.Copy
                    ObjclsCommon = New clsCommon()
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
                    Me.Text = getValueByKey("frmntillclosing2")
                    cmdNext.Visible = False
                    cancelBtn.Visible = True
                    txtOperations.Visible = False
                    lblOperation.Visible = False
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
                DTMain = DTTillClose
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
            Dim dvCheck As New DataView(DTTillClose, "", "", DataViewRowState.CurrentRows)
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
    Private Sub cancelBtn_Click() Handles cancelBtn.Click
        Me.Close()
    End Sub

    Private Sub cmdFinsh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFinsh.Click
        Try

            Dim ObjclsCommomSM As New clsCommon
            Dim SpectrumPaymentMettlerTill As String = clsDefaultConfiguration.SpectrumMettlerPaymentTill
            Dim SpectrumMettlerTill As String = Nothing
            Dim SpectrumTill As DataTable = Nothing
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

                Try
                    If clsDefaultConfiguration.SpectrumAsMettler = True Then
                        SpectrumTill = ObjclsCommomSM.GetTerminals(clsAdmin.SiteCode, False)
                        Dim TillId As String = ""
                        For Each dr As DataRow In SpectrumTill.Rows
                            TillId = dr("Terminalid").ToString
                            If Not clsDefaultConfiguration.SpectrumMettlerPaymentTill.Contains(TillId) Then
                                SpectrumMettlerTill = IIf(SpectrumMettlerTill Is Nothing, "", SpectrumMettlerTill + ",") + "'" + dr("Terminalid").ToString + "'"
                            End If
                        Next
                    End If
                Catch exx As Exception
                    LogException(exx)
                End Try


                If DTMain IsNot Nothing Then
                    Dim totalNextDayFloatAmt As Double = DTMain.Compute("SUM(AMount)", "")
                    If totalNextDayFloatAmt > clsDefaultConfiguration.MaxNextDayFloatAmount Then
                        ShowMessage("Max limit for next day float is " & clsDefaultConfiguration.MaxNextDayFloatAmount.ToString() & "", getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                End If
                Dim objTill As New clsTill()
                objTill.DeleteNextDayFloatIfExist(clsAdmin.SiteCode, clsAdmin.TerminalID)
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
                    Call AddRows(DTExtraAmount, "Open")
                    Call AddRows(DTExtraAmount, "Open", dtNextDayAmount)
                End If
                If Not DTTillClose Is Nothing Then
                    Call AddRows(DTTillClose, "Close")
                End If
                BuildTillCloseDtl()
                If Not dtNextDayAmount Is Nothing AndAlso dtNextDayAmount.Rows.Count > 0 Then
                    dtNextDayAmount.TableName = "NextDayFloatingDetail"
                    dsTill.Tables.Add(dtNextDayAmount)
                End If
                Dim dsTemp As DataSet = dsTill.Copy()
                If objTill.SaveTillClose(dsTemp, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.DayOpenDate, clsAdmin.UserCode, ConsistancyDone, "TillClose", IncosAmt, ServerDate, dtTender, clsCommon._PrevShiftId, SpectrumAsMettler:=clsDefaultConfiguration.SpectrumAsMettler, SpectrumMettlerTill:=SpectrumMettlerTill) = True Then
                    clsDefaultConfiguration.TillOpenDone = False
                    ShowMessage(getValueByKey("TLC004"), getValueByKey("CLAE04"))

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
                    'added by vipul for till close print  on right basis
                    If CheckAuthorisation(clsAdmin.UserCode, "SC_CashDetails") = True Then
                        Dim objprint As New clsPrintFinancialReport(clsDefaultConfiguration.TillClosePrintPreivewReq, clsDefaultConfiguration.UserAmountPrint)
                        objprint.PrintFinancialReport(clsAdmin.TerminalID, clsAdmin.DayOpenDate, clsAdmin.SiteCode, _dtPayment, _dtCash, _VendorAmount, gPrinterName, lblTotalAmount.Text, dtPrinterInfo, PettyCashExp, PettyCashRec, clsAdmin.UserName, TransactionAllowed:=True)
                    End If
                    'Dim objprint As New clsPrintFinancialReport(clsDefaultConfiguration.TillClosePrintPreivewReq, clsDefaultConfiguration.UserAmountPrint)
                    'objprint.PrintFinancialReport(clsAdmin.TerminalID, clsAdmin.DayOpenDate, clsAdmin.SiteCode, _dtPayment, _dtCash, _VendorAmount, gPrinterName, lblTotalAmount.Text, dtPrinterInfo, PettyCashExp, PettyCashRec, clsAdmin.UserName, TransactionAllowed:=True)

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
                    ShowMessage(getValueByKey("TLC006"), getValueByKey("CLAE04"))
                    Exit Sub
                End If
                'If ConsistancyDone = True Then
                DTExtraAmount.Rows.Clear()
                DTTillClose.Rows.Clear()
                dtNextDayAmount.Rows.Clear()
                'End If
                ' If EditMode = False Then
                'CloseTerminal()
                'End If
                Me.Close()
                '''''''''''''''''''  mayur till open
                If clsDefaultConfiguration.SpectrumLiteAllowed Then
                    DayCloseContinue()
                ElseIf clsDefaultConfiguration.DayCloseProceedOnTillClose Then
                    Dim objDayClose As New clsDayClose
                    If objDayClose.CheckIfAllTerminalAreClosed(clsAdmin.SiteCode) AndAlso CheckAuthorisation(clsAdmin.UserCode, "DAY_CLS_FO") Then
                        DayCloseContinue()
                    End If
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("PRIMARY KEY") Then
                ShowMessage(getValueByKey("TLC002"), getValueByKey("CLAE05"))
            Else
                ShowMessage(ex.Message, getValueByKey("CLAE05"))
            End If
            'ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
            Me.Close()
        End Try
    End Sub

    '' added by nikhil For Hari Om On 10/04/2017 for till close without
    Public Sub AllTillCloseForHariOM()
        Try
            Dim objTill As New clsTill
            If Not DTExtraAmount Is Nothing Then
                dtNextDayAmount = objTill.GetNextDayFloatingDetailStruc()
                Call AddRows(DTExtraAmount, "Open")
                Call AddRows(DTExtraAmount, "Open", dtNextDayAmount)
            End If
            If Not DTTillClose Is Nothing Then
                Call AddRows(DTTillClose, "Close")
            End If
            BuildTillCloseDtl()
            If Not dtNextDayAmount Is Nothing AndAlso dtNextDayAmount.Rows.Count > 0 Then
                dtNextDayAmount.TableName = "NextDayFloatingDetail"
                dsTill.Tables.Add(dtNextDayAmount)
            End If

            Dim dsTemp As DataSet = dsTill.Copy()
            Dim eventType As Int32
            ShowMessage("All Terminal will be Closed", " WARNING MESSAGE", eventType, "Cancel", "Ok")
            If eventType = 1 Then

                If objTill.SaveTillClose(dsTemp, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.DayOpenDate, clsAdmin.UserCode, ConsistancyDone, "TillClose", IncosAmt, ServerDate, dtTender, clsCommon._PrevShiftId, clsDefaultConfiguration.IsHariOM) = True Then
                    clsDefaultConfiguration.TillOpenDone = False
                    ShowMessage(getValueByKey("TLC004"), getValueByKey("CLAE04"))
                End If

                Dim objCom As New clsCommon
                objCom.UpdateDayCloseStaus(clsAdmin.SiteCode, clsAdmin.DayOpenDate, True)
                ShowMessage(String.Format(getValueByKey("dayclosesuccessmsg"), clsAdmin.DayOpenDate.ToShortDateString()), getValueByKey("CLAE04"))
                'Me.Invoke(ChangePictureVisibility, 9)
                'DisableTransactionMainMenu(False)
                ' DayOpenMenuItem.Enabled = True
                'After Till Closed, All transactiom menu will be disabled0
                DisableTransactionMainMenu(False)
                MDISpectrum.DayOpenMenuItem.Enabled = True
                MDISpectrum.DayCloseToolStripMenuItem.Enabled = False
            ElseIf eventType = 2 Then
                Exit Sub
            End If


        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Dim isClosed As Boolean = False
    Public Function DayCloseContinue() As Boolean
        For Each form In My.Application.OpenForms
            If (form.name = frmDayCloseMain.Name) Then
                Exit Function
            End If
        Next
        Dim objDayClose As New clsDayClose
        ''added By ketan JK Changes
        'If clsDefaultConfiguration.ClientForMail = "JK" Then
        '    If Not CheckAuthorisation(clsAdmin.UserCode, "DAY_OPEN_MODIFY") Then
        '        If objDayClose.CheckBillCount(clsAdmin.SiteCode, clsAdmin.Financialyear, clsAdmin.DayOpenDate) Then
        '            'MessageBox.Show("Day close not allowed since no bills are punched for the day.", "WARNING MESSAGE")
        '            ShowMessage("Day close not allowed since no bills are punched for the day.", "WARNING MESSAGE")
        '            Exit Function
        '        End If
        '    End If
        'End If
        If objDayClose.CheckIfValidDayClose(clsAdmin.SiteCode, clsAdmin.Financialyear, clsAdmin.DayOpenDate) Then


            'Dim dtDayOpenStatus As DataTable = objLogin.GetDayCloseStatus(clsAdmin.SiteCode, clsAdmin.Financialyear)

            'If dtDayOpenStatus IsNot Nothing AndAlso dtDayOpenStatus.Rows.Count > 0 Then
            '    AnyTerminalOpen = dtDayOpenStatus.Rows(0)("AnyTerminalOpen")

            '    If (AnyTerminalOpen > 0) Then
            '        ShowMessage(True, getValueByKey("LG021"), "LG021 - " & getValueByKey("CLAE04"))
            '        Exit Sub
            '    End If
            'End If

            If (MessageBox.Show(String.Format(getValueByKey("LG022"), clsAdmin.DayOpenDate.ToString("dd/MM/yyyy")), "LG022 - " & getValueByKey("CLAE04"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Cancel) Then
                Exit Function
            End If
            If objDayClose.CheckIfValidDayClose(clsAdmin.SiteCode, clsAdmin.Financialyear, clsAdmin.DayOpenDate) Then
                Dim objDefault As New clsDefaultConfiguration("DC")
                objDefault.GetDefaultSettings()
                If clsDefaultConfiguration.DayCloseOtherScreens = False Then
                    If objDayClose.CheckIfAllTerminalAreClosed(clsAdmin.SiteCode) = False Then
                        ShowMessage(getValueByKey("mdispectrum.dayclosevalidationmsg"), getValueByKey("CLAE04"))
                        Exit Function
                    End If
                End If
                'auto-update on day close ..day close will happen after update automatically JK requested on 14-oct-2015

                If clsDefaultConfiguration.AutoUpdateonDayClose Then
                    AutoUpdates()
                    If isClosed Then
                        isClosed = False
                        Exit Function
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
        Else
            ShowMessage(String.Format(getValueByKey("mdispectrum.daycloseperformedmsg"), clsAdmin.DayOpenDate.ToShortDateString()), getValueByKey("CLAE04"))
        End If

    End Function
    Private Sub AddRows(ByVal DTTable As DataTable, ByVal ActionType As String, Optional ByRef dtNextDayAmount As DataTable = Nothing)
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
                    GenDataRow.Item("Shiftid") = clsCommon._PrevShiftId
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
    Private Sub BuildTillCloseDtl()
        Try
            dsTill = New DataSet
            Dim objTill As New clsTill
            If _VendorAmount <> 0 Or txtCheckAmount.Text <> String.Empty Or txtCVAmount.Text <> String.Empty Or txtGVAmount.Text <> String.Empty Then
                dtTill = objTill.TillCloseStru(clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.DayOpenDate, ServerDate, clsCommon._PrevShiftId)
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

            If (DTTillClose IsNot Nothing) Then
                Dim UserEnteredCash As Double = IIf(DTTillClose.Compute("SUM(BASEAMOUNT)", "") Is DBNull.Value, 0, DTTillClose.Compute("SUM(BASEAMOUNT)", ""))
                Dim DC As DataColumn = New DataColumn("UserAmount", Type.GetType("System.Decimal"))
                _dtPayment.Columns.Add(DC)
                _dtCash.TableName = "TillCloseCashDtl"
                _dtPayment.TableName = "TillCloseFinanceDtl"
                DTTillClose.TableName = "FloatingDetail"
                DTTillClose.Merge(DTExtraAmount, False, MissingSchemaAction.Ignore)
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
            dtTender.Columns.Add("TenderCode", GetType(System.String))
            dtTender.Columns.Add("Amount", GetType(System.Int32))

            txtCheckAmount.Text = IIf(txtCheckAmount.Text = String.Empty, 0, txtCheckAmount.Text)
            txtCVAmount.Text = IIf(txtCVAmount.Text = String.Empty, 0, txtCVAmount.Text)
            txtGVAmount.Text = IIf(txtGVAmount.Text = String.Empty, 0, txtGVAmount.Text)

            If _GVAmount <> txtGVAmount.Text Then
                InCosGV = txtGVAmount.Text - _GVAmount
                InCosGV = FormatNumber(InCosGV, 2)
                str = "GiftVoucher: " & InCosGV.ToString & vbCrLf
            End If
            '-----------------------------------------------------------------------------------------------
            Dim R As DataRow = dtTender.NewRow
            R("TenderCode") = "GiftVoucher"
            R("Amount") = InCosGV
            dtTender.Rows.Add(R)

            If _CVAmount <> txtCVAmount.Text Then
                InCosCV = txtCVAmount.Text - _CVAmount
                InCosCV = FormatNumber(InCosCV, 2)
                str = str & "CreditVoucher: " & InCosCV.ToString & vbCrLf
            End If
            '-----------------------------------------------------------------------------------------------
            Dim R1 As DataRow = dtTender.NewRow
            R1("TenderCode") = "CreditVoucher"
            R1("Amount") = InCosCV
            dtTender.Rows.Add(R1)

            If _Check <> txtCheckAmount.Text Then
                InCosCheck = txtCheckAmount.Text - _Check
                InCosCheck = FormatNumber(InCosCheck, 2)
                str = str & "Cheque: " & InCosCheck.ToString & vbCrLf
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
                    InCosCash = UserAmt - SysAmt
                    InCosCash = FormatNumber(InCosCash, 2)
                    Dim InCosCashFinal As Decimal
                    If DisplayCash Then
                        InCosCashFinal = InCosCash
                        str = str & dr("CURRENCYDESCRIPTION").ToString() & " : " & InCosCashFinal
                    Else
                        InCosCashFinal = InCosCash - (SysAmt - UserAmt)
                        str = str & dr("CURRENCYDESCRIPTION").ToString() & " : " & InCosCash
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
        If clsDefaultConfiguration.IsHariOM Then  ''' added by nikhil 

        Else
            If objTill.CheckOpening(clsAdmin.TerminalID, clsAdmin.SiteCode, "Open") = False Then
                ShowMessage(getValueByKey("TLC007"), getValueByKey("CLAE04"))
                Me.Dispose()
                Me.Close()
                Exit Sub
            End If
        End If

        If objTill.CheckOpening(clsAdmin.TerminalID, clsAdmin.SiteCode, "Close") Then
            ShowMessage(getValueByKey("TLC002"), getValueByKey("CLAE04"))
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

    Private Sub cancelBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cancelBtn.Click

    End Sub
    Public Sub AutoUpdates()
        Try
            Dim proxy As New FOAutomaticVersionUpgradeServiceImplClient
            Dim exs As New executeService
            Dim exser As New executeService



            Dim licence As New ClsLicense
            Dim HDDKey = licence.GetEncryptedHDDKey()
            Dim exsA As executeService = CreateExecuteServiceObject("EN", "isUpdateAvailable", HDDKey, clsAdmin.SiteCode, clsAdmin.TerminalID)

            Dim resp As executeServiceResponse = proxy.executeService(exsA)
            Dim resp1 As wsResponse = resp.[return]
            If resp1.responseCode = "200" Then
                Dim message As New StringBuilder
                'message.Append("Update for a new Spectrum version is available.")
                'message.Append("System will now install the new update and restart Spectrum.")
                'message.Append("Spectrum will restart and the login screen will appear once the update installation is done.")
                'message.Append(" Continue with Day Close operation once the update installation is completed.")
                message.Append("Spectrum is being updated to latest version.")
                message.Append("Spectrum application will now close while this operation is being performed.")
                message.Append("Please continuie with day close operation once Spectrum re-starts. ")
                message.Append("Kindly be patient during this process..")

                ShowBigMessagewithOK(message.ToString(), "DBCB004 - " & getValueByKey("CLAE04"))
                Application.DoEvents()
                Dim objCls As New clsCommon
                directoryPath = directoryPath & "\Update"
                If Directory.Exists(directoryPath) Then
                    'Directory.Delete(directoryPath)
                    DeleteDirectory(directoryPath)
                End If
                Directory.CreateDirectory(directoryPath)




                Dim req As New wsRequest
                req.languageCode = "EN-US"
                req.webMethod = "fetchExeFile"

                Dim soap As New soapWsHeader
                soap.userName = ""
                soap.password = ""
                req.soapWsHeader = soap

                exser.arg0 = req

                Dim exresp As executeServiceResponse = proxy.executeService(exser)

                Dim wsResonse As wsResponse = exresp.return

                Dim t As dynaTable = wsResonse.dynaTables.FirstOrDefault()
                Dim q As dynaRow = t.dynaRows.FirstOrDefault()
                Dim w As dynaColumn = q.dynaColumn.FirstOrDefault()
                Dim updatezip As String = w.columnValue

                Dim bytIn() As Byte = System.Convert.FromBase64String(updatezip)
                Dim wFile As FileStream = New FileStream(directoryPath & "\setup.zip", FileMode.Create)
                wFile.Write(bytIn, 0, bytIn.Length)
                wFile.Close()


                Using zip1 As Ionic.Zip.ZipFile = Ionic.Zip.ZipFile.Read(directoryPath & "\setup.zip")
                    zip1.ExtractAll(directoryPath,
                            Ionic.Zip.ExtractExistingFileAction.OverwriteSilently)
                End Using

                Dim dataSource As String = ReadSpectrumParamFile("Server")
                Dim dbname As String = ReadSpectrumParamFile("DataSource")
                Dim username As String = ReadSpectrumParamFile("UserId")
                Dim password As String = ReadSpectrumParamFile("Password")
                Dim clientname As String = objCls.GetSiteName("CCE")
                Dim AutoUpdateDayClose As String = "Yes"
                'HDDKey = "c4:43:8f:43:e4:2f"
                Dim pHelp As New ProcessStartInfo
                pHelp.FileName = "SpectrumUpdate.exe"
                pHelp.Arguments = "" & dataSource & " " & dbname & " " & username & " " & password & " " & clientname.Replace(" ", "") & " " & HDDKey & " " & clsAdmin.SiteCode & " " & clsAdmin.TerminalID & " " & AutoUpdateDayClose & ""
                pHelp.WorkingDirectory = directoryPath
                'pHelp.WorkingDirectory = "C:\Users\sagar.borole\Desktop\ConsoleApplication1"
                Process.Start(pHelp)
                Application.Exit()
                'Else
                '    ShowMessage("New Version not available", "DBCB004 - " & getValueByKey("CLAE04"))

            End If
        Catch ex As Exception
            'MessageBox.Show("Error")
            LogException(ex)
            If (ex.ToString().Contains("no endpoint")) Then
                ShowMessage("Invalid CCE Connection", "DBCB004 - " & getValueByKey("CLAE04"))
            End If
            isClosed = True
        End Try
    End Sub
    Private Shared Function CreateExecuteServiceObject(languageCode As String, webMethod As String, hardwareKey As String, siteCode As String, terminalId As String) As executeService
        Try
            Dim exs As New executeService()

            Dim req As New wsRequest()
            req.languageCode = languageCode
            req.webMethod = webMethod

            Dim soap As New soapWsHeader()
            soap.userName = ""
            soap.password = ""
            req.soapWsHeader = soap

            Dim row As New dynaRow()

            Dim col As New dynaColumn()
            col.columnName = "hardwareKey"
            col.columnType = "STRING"
            col.columnValue = hardwareKey

            Dim col2 As New dynaColumn()
            col2.columnName = "siteCode"
            col2.columnType = "STRING"
            col2.columnValue = siteCode

            Dim col3 As New dynaColumn()
            col3.columnName = "terminalId"
            col3.columnType = "STRING"
            col3.columnValue = terminalId



            Dim cols As New List(Of dynaColumn)()
            cols.Add(col)
            cols.Add(col2)
            cols.Add(col3)


            row.dynaColumn = cols.ToArray()
            req.dynaColumns = row

            exs.arg0 = req


            Return exs
        Catch ex As Exception

            Return Nothing
        End Try
    End Function
    Private Sub DeleteDirectory(path As String)
        If Directory.Exists(path) Then
            'Delete all files from the Directory
            For Each filepath As String In Directory.GetFiles(path)
                File.Delete(filepath)
            Next
            'Delete all child Directories
            For Each dir As String In Directory.GetDirectories(path)
                DeleteDirectory(dir)
            Next
            'Delete a Directory
            Directory.Delete(path)
        End If
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

    Private Sub dgMainGrid_StartEdit(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgMainGrid.StartEdit
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub
End Class
