Imports System.Data
Imports System.Resources
Imports SpectrumBL
Imports System.Globalization
Imports System.Data.SqlClient
Imports SpectrumPrint
Imports SpectrumBL.POSDBDataSet
Imports System.Net
Imports System.IO

Public Class frmShiftOpen
#Region "Global Variable For Class"

    Dim DTPreviousDetails, DTMain, DTTillOpen, DTExtraAmount As DataTable
    Dim DsFinalData As New DataSet
    Dim ObjclsCommon As clsCommon
    Dim IncosAmt As Double
    Dim GenDataRow As DataRow
    Dim ServerDate As DateTime
    Dim DTFloatingDetail, DTFloatingDetail1 As New FloatingDetailDataTable
    Dim DRFloatingDetail As FloatingDetailRow

    Dim TADenominationDetail As New POSDBDataSetTableAdapters.DenominationDetailTableAdapter
    Dim DTDenominationDetail As New DenominationDetailDataTable
    Dim DRDenominationDetail As DenominationDetailRow
    Dim EditMode, ConsistancyDone, NewEntry As Boolean
    Dim nextShiftId As Integer
    Dim prevShiftId As Integer
    Dim sitename As String
#End Region
    Private Sub frmShiftOpen_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            ConsistancyDone = False
            If DTMain Is Nothing Then
                ObjclsCommon = New clsCommon()
                DTMain = ObjclsCommon.GetDenomination(clsAdmin.CurrencyCode)
            End If

            'If clsDefaultConfiguration.CheckTillCloseAmount = True Then
            '    PopulateGrid(DTPreviousDetails, "Open", True)
            'End If
            Dim dt As New DataTable
            dt = clsCommon.GetNextShiftID(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID)
            If EditMode = False Then
                nextShiftId = clsCommon._NextShiftId
                prevShiftId = clsCommon._PrevShiftId
            Else
                nextShiftId = clsCommon._PrevShiftId
                prevShiftId = clsCommon._PrevShiftId
                DTMain = ObjclsCommon.GetPreviusShiftData(clsAdmin.CurrencyCode, clsCommon._PrevShiftId, clsAdmin.DayOpenDate.Date, clsAdmin.TerminalID)
            End If

            ''End If
            If DTPreviousDetails IsNot Nothing AndAlso DTPreviousDetails.Rows.Count > 0 Then
                'cmdNext.Text = "&Next"
                cmdNext.Text = getValueByKey("frmntillopen.cmdnext")
                cmdNext.Tag = "Next"
                'Me.Text = "Enter Till Amount:"
                Me.Text = getValueByKey("frmntillopen1")
            Else
                cmdNext.Visible = False
                'Me.Text = "Enter Open Amount "
                Me.Text = getValueByKey("frmShiftOpen")
            End If
            dgMainGrid.DataSource = DTMain
            FormatGrid()
            CalculateTotal(DTMain)
            cmdFinsh.Tag = "Finish"
            cmdReset.Tag = "Reset"
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
        SetCulture(Me, Me.Name)
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
        End If
        Dim obj As New clsShift
        If obj.CheckClosed(clsAdmin.TerminalID, clsAdmin.SiteCode, "", True) = False Then
            Me.Text = getValueByKey("frmntillopen1")
            cmdNext.Visible = False
            Exit Sub
        End If
        cmdPrint.Visible = False
        'If DTPreviousDetails IsNot Nothing AndAlso DTPreviousDetails.Rows.Count > 0 Then
        '    cmdNext.Text = getValueByKey("frmntillopen.cmdnext")
        '    cmdNext.Tag = "Next"
        '    Me.Text = getValueByKey("frmntillopen1")
        'Else
        '    cmdNext.Visible = False
        '    Me.Text = getValueByKey("frmntillopen")
        'End If
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub
    Private Sub dgMainGrid_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgMainGrid.AfterEdit
        Try
            dgMainGrid.Rows(e.Row)("Amount") = dgMainGrid.Rows(e.Row)("DENOMINATIONAMT") * dgMainGrid.Rows(e.Row)("Qty")
            CalculateTotal(DTMain)
            cmdPrint.Visible = True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdReset_Click(sender As System.Object, e As System.EventArgs) Handles cmdReset.Click
        ClearGrid()
    End Sub

    Private Sub cmdFinsh_Click(sender As System.Object, e As System.EventArgs) Handles cmdFinsh.Click
        Try
            Dim objShift As New clsShift()
            Dim objLoginData As New clsLogin
            Dim PreviousAmountTotalDataTbl As New DataTable
            Dim OpeningShiftBalance, CurrentOpeningShiftBalance As Double
            'dt = clsCommon.GetID(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID)
            ServerDate = ObjclsCommon.GetCurrentDate()
            If cmdNext.Tag = "Previous" Or cmdNext.Visible = False Then
                DTExtraAmount = DTMain.Copy
            End If
            If UCase(clsDefaultConfiguration.CheckTillOpenAmount) = "TRUE" Then
                If EditMode = True Then
                    PreviousAmountTotalDataTbl = ObjclsCommon.GetPreviusShiftData(clsAdmin.CurrencyCode, clsCommon._PrevShiftId.ToString, clsAdmin.DayOpenDate.Date, clsAdmin.TerminalID)
                    If Not PreviousAmountTotalDataTbl Is Nothing Then
                        OpeningShiftBalance = PreviousAmountTotalDataTbl.Compute("Sum(Amount)", "")
                    End If
                    CurrentOpeningShiftBalance = DTExtraAmount.Compute("sum(Amount)", "")
                    If OpeningShiftBalance <> CurrentOpeningShiftBalance Then
                        ShowMessage(getValueByKey("frmShiftOpen.AmountInconsistent"), getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                Else
                    If UCase(clsDefaultConfiguration.CheckTillOpenAmount) = "TRUE" Then
                        If Not DTExtraAmount Is Nothing Then
                            Dim totalAmount As Double = DTExtraAmount.Compute("SUM(AMOUNT)", "")

                            If objShift.CheckIfValidShiftOpenAmount(clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.CurrencyCode, totalAmount, prevShiftId) = False Then
                                ShowMessage(getValueByKey("frmShiftOpen.AmountInconsistent"), getValueByKey("CLAE04"))
                                Exit Sub
                            End If
                        Else
                            If objShift.CheckIfValidShiftOpenAmount(clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.CurrencyCode, 0, prevShiftId) = False Then
                                ShowMessage(getValueByKey("frmShiftOpen.AmountInconsistent"), getValueByKey("CLAE04"))
                                Exit Sub
                            End If
                        End If
                    End If

                End If
            End If
            If EditMode = True Then
                If objShift.RemoveData(clsAdmin.TerminalID, clsAdmin.SiteCode, clsAdmin.DayOpenDate, prevShiftId) = False Then
                    ShowMessage(getValueByKey("TO01"), "TO01 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
            End If

            If Not DTExtraAmount Is Nothing Then
                Call AddRows(DTExtraAmount, "ExtraOpen", nextShiftId)
                DTExtraAmount.Rows.Clear()
            End If

            If Not DTTillOpen Is Nothing Then                '
                Call AddRows(DTTillOpen, "Open", "")
                DTTillOpen.Rows.Clear()
            End If
            If DTFloatingDetail1.Rows.Count > 0 Then
                DsFinalData.Tables.Add(DTFloatingDetail1)
            Else
                Call AddRows("ExtraOpen", nextShiftId)
                DsFinalData.Tables.Add(DTFloatingDetail1)
            End If
            '----- Mahesh :- Code to check Floating Open Amount is zero then Insert a entry with zero denomation 
            'If (objTill.GetTillOpenAmount(clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.CurrencyCode) = 0) AndAlso DTFloatingDetail1.Rows.Count = 0 Then
            '    Dim tbool = objTill.AddExtraOpen(clsAdmin.TerminalID, clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.UserCode)
            'End If
            If objShift.SaveRecords(DsFinalData, NewEntry, clsAdmin.TerminalID, clsAdmin.SiteCode, clsAdmin.DayOpenDate, prevShiftId, nextShiftId, clsAdmin.UserCode, EditMode) = False Then
                Exit Sub
            End If
            'If ConsistancyDone = True Then
            'If objShift.UpdateIncosDetail(ConsistancyDone, clsAdmin.TerminalID, prevShiftId, clsAdmin.SiteCode, ServerDate, clsAdmin.DayOpenDate, "TillOpen", IncosAmt, clsAdmin.UserCode) = False Then
            '    Exit Sub
            'End If
            'End If
            '--------------Display labels
            Dim data As DataTable = clsCommon.GetShiftName(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID)
            If data.Rows.Count > 0 Then
                clsAdmin.ShiftName = data.Rows(0)("ShiftName").ToString()
                clsAdmin.ShiftStatus = data.Rows(0)("OpenCloseStatus").ToString()
                clsAdmin.DisplayShift = True
            End If
            If EditMode = False Then
                OpenTerminal()
                'DisableTransactionMainMenu(True)
                clsAdmin.Financialyear = objLoginData.GetFinancialYear(clsAdmin.DayOpenDate, clsAdmin.SiteCode)
                'ShowMessage("Shift is Opened", getValueByKey("CLAE04"))
            End If
            If EditMode Then
                ShowMessage(String.Format("{0} Updated Successfully", clsAdmin.ShiftName), getValueByKey("CLAE04"))
            Else
                ShowMessage(String.Format("{0} Opened Successfully", clsAdmin.ShiftName), getValueByKey("CLAE04"))
            End If

            clsAdmin.CurrentDate = ObjclsCommon.GetCurrentDate()
            DisableTransactionMainMenu(True)
            If clsDefaultConfiguration.ChecklistOnTillOpen = True And EditMode = False Then
                Dim IsCheckListFilled As Boolean = ObjclsCommon.IsCheckListFilled(clsAdmin.TerminalID, clsAdmin.SiteCode)

                If IsCheckListFilled = False Then
                    Dim IsCheckListExist As Boolean = ObjclsCommon.IsCheckListAvailabl()
                    If IsCheckListExist = True Then
                        Dim chkListObj As New frmchecklist()
                        chkListObj.ShowDialog()
                        chkListObj.Dispose()
                    Else
                        ShowMessage(getValueByKey("CLIST03"), getValueByKey("CLIST03"))
                    End If

                End If

                '     NotificationTimer.start()
            End If
            Me.Close()

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
            Me.Close()
        End Try
    End Sub

    Private Sub cmdNext_Click(sender As System.Object, e As System.EventArgs) Handles cmdNext.Click

    End Sub

    Private Sub cmdPrint_Click(sender As System.Object, e As System.EventArgs) Handles cmdPrint.Click
        Dim objSite As New clsCommon
        Dim sitename As String = objSite.GetSiteName(clsAdmin.SiteCode)
        Dim shiftname As String = objSite.GetShiftNameForPrint(nextShiftId, clsAdmin.SiteCode)
        Dim objprint As New clsPrintDenomination(clsDefaultConfiguration.TillClosePrintPreivewReq)
        objprint.PrintShiftDenomination(clsAdmin.TerminalID, clsAdmin.DayOpenDate, clsAdmin.SiteCode, DTMain, "SHIFT OPEN DENOMINATION DETAILS", shiftname, lblTotalAmount.Text, gPrinterName, dtPrinterInfo, clsAdmin.UserName, clsDefaultConfiguration.ClientName, sitename)
        ' Me.Close()
    End Sub

#Region "Private Function's & Method's"

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
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub CalculateTotal(ByVal DTTable As DataTable)
        Try
            lblTotalAmount.Text = IIf(DTTable.Compute("SUM(AMOUNT)", "") Is DBNull.Value, 0, DTTable.Compute("SUM(AMOUNT)", ""))
            lblTotalAmount.Text = FormatNumber(CDbl(lblTotalAmount.Text), 2)
        Catch ex As Exception
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
                lblTotalAmount.Text = "0"
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub AutoLogOut()
        If Logout() = False Then
            Exit Sub
        End If
        LoginStatus = False
        Dim ChildForm As New frmNLogin
        ChildForm.StartPosition = FormStartPosition.CenterScreen
        ChildForm.txtusername.Focus()
        ChildForm.Focus()
        ChildForm.Show()
        ChildForm.Select()
        ChildForm.txtusername.Select()
    End Sub
  
    Private Function Logout() As Boolean
        Try
            Dim objlog As New clsLogin
            Dim IpAddress = objlog.getIPAddress()

            ''Added by nikhil for Local IP Address
            'Dim xEntry As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName)
            'Dim ipAddr As Net.IPAddress() = xEntry.AddressList
            'Dim IpAddress As String = ipAddr(0).ToString()
            If objlog.InsertLoginHistory(clsAdmin.UserCode, clsAdmin.SiteCode, clsAdmin.TerminalID, False, IpAddress) = True Then
                Return True
            End If
            Return False
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    Public Sub New(Optional ByRef isAutoLogout As Integer = 0)
        Dim EventType As Int32
        Dim objDefault As New clsDefaultConfiguration("TillOpenNClose")
        objDefault.GetDefaultSettings()

        If clsDefaultConfiguration.TillOperationRequired = False Then
            ShowMessage(getValueByKey("SOC05"), "TO02 - " & getValueByKey("CLAE04"))
            Me.Dispose()
            Me.Close()
            Exit Sub
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "TillOpenNClose") = False Then 'Rohit
            ShowMessage(getValueByKey("TO03"), "TO03 - " & getValueByKey("CLAE04"))
            Me.Dispose()
            Me.Close()
            Exit Sub
        End If
        Dim objTill As New clsShift()
        If objTill.CheckDayOpening(clsAdmin.SiteCode) = False Then
            ShowMessage(getValueByKey("LG008"), "TO03 - " & getValueByKey("CLAE04"))
            Me.Dispose()
            Me.Close()
            Exit Sub
        End If
        If clsCommon.GetShiftCloseDetails(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID) = "Close" Then
            If objTill.CheckClosed(clsAdmin.TerminalID, clsAdmin.SiteCode, "Close") = True Then
                Dim dayOpenDate As Date = clsAdmin.DayOpenDate
                If CheckAuthorisation(clsAdmin.UserCode, "DAY_CLS_FO") Then
                    'Dim dayOpenDate As Date = objTill.GetDatesToCloseForTillOpen(clsAdmin.TerminalID, clsAdmin.SiteCode)
                    'ShowMessage(String.Format(getValueByKey("frmntillopen.CloseOpenDayMsg"), dayOpenDate.ToLongDateString()), getValueByKey("CLAE04"), EventType, True, getValueByKey("mod009"))
                    ShowMessage(String.Format(getValueByKey("frmntillopen.CloseOpenDayMsg"), dayOpenDate.ToLongDateString()), getValueByKey("CLAE04"))
                    Me.Dispose()
                    Me.Close()
                    Exit Sub
                    'If EventType = 2 Then
                    '    Me.Dispose()
                    '    Me.Close()
                    '    Exit Sub
                    'ElseIf EventType = 1 Then
                    '    objTill.CLoseDayForTillOpen(clsAdmin.SiteCode, clsAdmin.Financialyear, dayOpenDate, clsAdmin.UserCode)
                    '    isAutoLogout = 1
                    '    Me.Close()
                    '    Me.Dispose()
                    '    Exit Sub
                    'End If
                Else
                    ShowMessage(String.Format(getValueByKey("frmntillopen.CloseOpenDayMsg"), dayOpenDate.ToLongDateString()), getValueByKey("CLAE04"))
                    Me.Dispose()
                    Me.Close()
                    Exit Sub
                End If
            End If
        End If
        If objTill.CheckOpening(clsAdmin.TerminalID, clsAdmin.SiteCode, "Open','ExtraOpen") Then
            ShowMessage(getValueByKey("SOC01"), "TO04 - " & getValueByKey("CLAE04"), EventType, True, getValueByKey("SMBTN001"))
            If EventType = 1 Then
                Me.Dispose()
                Exit Sub
            ElseIf EventType = 2 Then
                EditMode = True
                If EditMode = True Then
                    If CheckAuthorisation(clsAdmin.UserCode, "EditTill") = False Then 'Rohit
                        ShowMessage(getValueByKey("TO03"), "TO03 - " & getValueByKey("CLAE04"))
                        Me.Dispose()
                        Exit Sub
                    End If
                End If
            End If

        End If

        'ElseIf EditMode = True Then
        '    ShowMessage("Till Open not done for the current date", "Information")
        '    Me.Dispose()
        '    Exit Sub
        'End If
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal Value As Boolean)
        'EditMode = Value
        'If EditMode = True Then
        '    If CheckAuthorisation(clsAdmin.UserCode, "EditTill") = False Then 'Rohit
        '        ShowMessage("You dont have sufficient rights for this operation", "Information")
        '        Me.Dispose()
        '        Exit Sub
        '    End If
        'End If
        'Dim objTill As New clsTill()
        'If objTill.CheckOpening("Open", "ExtraOpen") Then
        '    If EditMode = False Then
        '        ShowMessage("Till Open Already Done for Current Date", "Information")
        '        Me.Dispose()
        '        Exit Sub
        '    End If
        'ElseIf EditMode = True Then
        '    ShowMessage("Till Open not done for the current date", "Information")
        '    Me.Dispose()
        '    Exit Sub
        'End If
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="DTTable"></param>
    ''' <param name="ActionType"></param>
    ''' <param name="ShiftId"></param>
    ''' <remarks></remarks>

    Private Sub AddRows(ByVal DTTable As DataTable, ByVal ActionType As String, ByVal ShiftId As Integer)
        Try
            Dim i As Integer = 0
            For i = 0 To DTTable.Rows.Count - 1
                If CDbl(DTTable.Rows(i).Item("Qty").ToString()) > 0 Then
                    GenDataRow = DTFloatingDetail1.NewRow()
                    GenDataRow.Item("SiteCode") = clsAdmin.SiteCode
                    GenDataRow.Item("TerminalId") = clsAdmin.TerminalID
                    GenDataRow.Item("Action") = ActionType
                    GenDataRow.Item("FlotDateTime") = clsAdmin.DayOpenDate
                    GenDataRow.Item("CREATEDON") = DateAdd(DateInterval.Minute, 1, ServerDate)
                    GenDataRow.Item("CurrencyCode") = clsAdmin.CurrencyCode
                    GenDataRow.Item("DenominationAmt") = DTTable.Rows(i).Item("DenominationAmt")
                    GenDataRow.Item("Qty") = DTTable.Rows(i).Item("Qty")
                    GenDataRow.Item("TotalAmount") = DTTable.Rows(i).Item("AMOUNT")
                    GenDataRow.Item("CreatedAt") = clsAdmin.SiteCode
                    GenDataRow.Item("CreatedBY") = clsAdmin.UserCode
                    GenDataRow.Item("Status") = 1
                    GenDataRow.Item("UPDATEDON") = DateAdd(DateInterval.Minute, 1, ServerDate)
                    GenDataRow.Item("UPDATEDAT") = clsAdmin.SiteCode
                    GenDataRow.Item("UPDATEDBY") = clsAdmin.UserCode
                    GenDataRow.Item("ShiftId") = ShiftId
                    DTFloatingDetail1.Rows.Add(GenDataRow)
                End If
            Next
            'DsFinalData.Tables.Add(DTFloatingDetail1)
            'TAFloatingDetail.Update(DTFloatingDetail1)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub AddRows(ByVal ActionType As String, ByVal ShiftId As Integer)
        Try
            'DTFloatingDetail1.Clear()
            clsAdmin.CurrentDate = ObjclsCommon.GetCurrentDate()
            Dim i As Integer = 0
            GenDataRow = DTFloatingDetail1.NewRow()
            GenDataRow.Item("SiteCode") = clsAdmin.SiteCode
            GenDataRow.Item("TerminalId") = clsAdmin.TerminalID
            GenDataRow.Item("Action") = ActionType
            GenDataRow.Item("FlotDateTime") = clsAdmin.DayOpenDate
            GenDataRow.Item("CREATEDON") = DateAdd(DateInterval.Minute, 1, ServerDate)
            GenDataRow.Item("CurrencyCode") = clsAdmin.CurrencyCode
            GenDataRow.Item("DenominationAmt") = 0
            GenDataRow.Item("Qty") = 0
            GenDataRow.Item("TotalAmount") = 0
            GenDataRow.Item("CreatedAt") = clsAdmin.SiteCode
            GenDataRow.Item("CreatedBY") = clsAdmin.UserCode
            GenDataRow.Item("Status") = 1
            GenDataRow.Item("UPDATEDON") = DateAdd(DateInterval.Minute, 1, ServerDate)
            GenDataRow.Item("UPDATEDAT") = clsAdmin.SiteCode
            GenDataRow.Item("UPDATEDBY") = clsAdmin.UserCode
            GenDataRow.Item("ShiftId") = ShiftId
            DTFloatingDetail1.Rows.Add(GenDataRow)
            'DsFinalData.Tables.Add(DTFloatingDetail1)
            'TAFloatingDetail.Update(DTFloatingDetail1)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub OpenTerminal()
        Try
            Dim objTill As New clsShift()
            If objTill.OpenCloseTerminal(clsAdmin.TerminalID, clsAdmin.SiteCode, True, clsAdmin.UserCode) = True Then
                clsDefaultConfiguration.TillOpenDone = True
                'MessageBox.Show(getValueByKey("TO05"), "TO05 - " & getValueByKey("CLAE04"))
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub OpenShift()
    End Sub

    Private Sub PopulateGrid(ByRef TempDTMain As DataTable, ByVal ActionType As String, ByVal PreviousDetails As Boolean)
        Try
            Dim objTill As New clsTill
            If PreviousDetails = False Then
                Dim i As Integer
                For i = 0 To TempDTMain.Rows.Count - 1
                    DTFloatingDetail.Rows.Clear()
                    'DTFloatingDetail = TAFloatingDetail.GetExistingFloatingDetail(ActionType, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.DayOpenDate)

                    DTFloatingDetail = objTill.GetExistingDetail(ActionType, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.DayOpenDate)
                    If DTFloatingDetail.Rows.Count > 0 Then
                        If DTFloatingDetail.Rows(0).Item("DenominationAmt") = TempDTMain.Rows(i).Item("DenominationAmt") Then
                            TempDTMain.Rows(i).Item("Qty") = DTFloatingDetail.Rows(0).Item("Qty")
                            TempDTMain.Rows(i).Item("AMOUNT") = DTFloatingDetail.Rows(0).Item("TotalAmount")
                        End If
                    End If
                Next
            Else

                ' TempDTMain = TAFloatingDetail.GetExistingFloatingDetail(ActionType, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.DayOpenDate)

                TempDTMain = objTill.GetExistingDetail(ActionType, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.DayOpenDate)

            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub frmShiftOpen_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "Shift-Open.htm")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        cmdNext.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdNext.BackColor = Color.Transparent
        cmdNext.BackColor = Color.FromArgb(0, 107, 163)
        cmdNext.ForeColor = Color.FromArgb(255, 255, 255)
        cmdNext.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdNext.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdNext.FlatStyle = FlatStyle.Flat
        cmdNext.FlatAppearance.BorderSize = 0
        cmdNext.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdNext.Size = New Size(94, 28)

        cmdPrint.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdPrint.BackColor = Color.Transparent
        cmdPrint.BackColor = Color.FromArgb(0, 107, 163)
        cmdPrint.ForeColor = Color.FromArgb(255, 255, 255)
        cmdPrint.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdPrint.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdPrint.FlatStyle = FlatStyle.Flat
        cmdPrint.FlatAppearance.BorderSize = 0
        cmdPrint.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdPrint.Size = New Size(94, 28)

        cmdReset.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdReset.BackColor = Color.Transparent
        cmdReset.BackColor = Color.FromArgb(0, 107, 163)
        cmdReset.ForeColor = Color.FromArgb(255, 255, 255)
        cmdReset.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdReset.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdReset.FlatStyle = FlatStyle.Flat
        cmdReset.FlatAppearance.BorderSize = 0
        cmdReset.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdReset.Size = New Size(94, 28)

        cmdFinsh.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdFinsh.BackColor = Color.Transparent
        cmdFinsh.BackColor = Color.FromArgb(0, 107, 163)
        cmdFinsh.ForeColor = Color.FromArgb(255, 255, 255)
        cmdFinsh.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdFinsh.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdFinsh.FlatStyle = FlatStyle.Flat
        cmdFinsh.FlatAppearance.BorderSize = 0
        cmdFinsh.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdFinsh.Size = New Size(94, 28)


        lblAmount.BackColor = Color.Transparent
        lblTotalAmount.BackColor = Color.Transparent
        lblAmount.ForeColor = Color.White
        lblTotalAmount.ForeColor = Color.White
        lblAmount.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblTotalAmount.Font = New Font("Neo Sans", 9, FontStyle.Bold)

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
    End Function
#End Region
End Class