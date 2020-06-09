Imports System.Data
Imports System.Resources
Imports SpectrumBL
Imports System.Globalization
Imports System.Data.SqlClient
Imports SpectrumBL.POSDBDataSet
Imports System.Net
Imports System.IO
Imports System.SpectrumPrint

''' <summary>
''' This class is used for Denomination Calculation
''' </summary>
''' <CreatedBy>Rama Ranjan Jena</CreatedBy>
''' <UpdatedBy></UpdatedBy>
''' <UpdatedOn></UpdatedOn>
''' <remarks></remarks>
Public Class frmNTillOpen
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
    Dim LastDayCloseData As New DataTable
#End Region
    Private Sub frmNTillOpen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'cmdFinsh.Visible = False
            'If clsDefaultConfiguration.IsPettyCashApplicable AndAlso clsDefaultConfiguration.IsPettyCashOnSameTerminal AndAlso clsAdmin.TerminalID = clsDefaultConfiguration.PettyCashTerminalId Then
            '    'If clsAdmin.TerminalID = clsDefaultConfiguration.PettyCashTerminalId Then
            '    dgMainGrid.Enabled = False
            '    'End If
            'End If
            ConsistancyDone = False
            If DTMain Is Nothing Then
                ObjclsCommon = New clsCommon()
                DTMain = ObjclsCommon.GetDenomination(clsAdmin.CurrencyCode)
            End If

            If EditMode Then
                Dim PreviousShiftData As New DataTable
                PreviousShiftData = ObjclsCommon.GetPreviusTerminalData(clsAdmin.CurrencyCode, clsAdmin.DayOpenDate.Date)
                DTMain = PreviousShiftData
                LastDayCloseData = PreviousShiftData.Copy
            End If


            'If EditMode Then
            If clsDefaultConfiguration.CheckTillCloseAmount = True Then
                PopulateGrid(DTPreviousDetails, "Open", True)
            End If

            'End If
            If DTPreviousDetails IsNot Nothing AndAlso DTPreviousDetails.Rows.Count > 0 Then
                'cmdNext.Text = "&Next"
                cmdNext.Text = getValueByKey("frmntillopen.cmdnext")
                cmdNext.Tag = "Next"
                'Me.Text = "Enter Till Amount:"
                Me.Text = getValueByKey("frmntillopen1")
            Else
                cmdNext.Visible = False
                'Me.Text = "Add Extra Amount:"
                Me.Text = getValueByKey("frmntillopen")
            End If
            ' added by vipul 27.07.2018 
            If EditMode = False AndAlso clsDefaultConfiguration.AutoPopulateFloatingDetails = True AndAlso clsDefaultConfiguration.CheckTillOpenAmount = True Then 'Auto Populate Floating Details 
                Dim PreviusDayTillFloatingData As New DataTable
                PreviusDayTillFloatingData = ObjclsCommon.GetPreviusDayTillFloatingData(clsAdmin.TerminalID, clsAdmin.SiteCode)
                DTMain = PreviusDayTillFloatingData
                dgMainGrid.DataSource = DTMain
            Else
                dgMainGrid.DataSource = DTMain

            End If
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
        Dim obj As New clsTill
        DTFloatingDetail1 = obj.GetAllDataForTillOpen(clsAdmin.CurrencyCode, clsCommon._PrevShiftId, clsAdmin.DayOpenDate, clsAdmin.TerminalID, "ExtraOpen")
        If obj.CheckClosed(clsAdmin.TerminalID, clsAdmin.TerminalID, "", True) = False Then
            Me.Text = getValueByKey("frmntillopen1")
            cmdNext.Visible = False
            Exit Sub
        End If
        If DTPreviousDetails IsNot Nothing AndAlso DTPreviousDetails.Rows.Count > 0 Then
            cmdNext.Text = getValueByKey("frmntillopen.cmdnext")
            cmdNext.Tag = "Next"
            Me.Text = getValueByKey("frmntillopen1")
        Else
            cmdNext.Visible = False
            Me.Text = getValueByKey("frmntillopen")
        End If
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
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
    End Sub

    Private Sub dgMainGrid_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgMainGrid.AfterEdit
        Try
            dgMainGrid.Rows(e.Row)("Amount") = dgMainGrid.Rows(e.Row)("DENOMINATIONAMT") * dgMainGrid.Rows(e.Row)("Qty")
            CalculateTotal(DTMain)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdFinsh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFinsh.Click
        Try

            Dim KeyFlotingDtl(7) As DataColumn
            KeyFlotingDtl(0) = DTFloatingDetail1.Columns("SiteCode")
            KeyFlotingDtl(1) = DTFloatingDetail1.Columns("TerminalID")
            KeyFlotingDtl(2) = DTFloatingDetail1.Columns("Action")
            KeyFlotingDtl(3) = DTFloatingDetail1.Columns("FlotDatetime")
            KeyFlotingDtl(4) = DTFloatingDetail1.Columns("CurrencyCode")
            KeyFlotingDtl(5) = DTFloatingDetail1.Columns("DenominationAmt")
            KeyFlotingDtl(6) = DTFloatingDetail1.Columns("STATUS")
            KeyFlotingDtl(7) = DTFloatingDetail1.Columns("ShiftId")
            DTFloatingDetail1.PrimaryKey = KeyFlotingDtl

            Dim objTill As New clsTill()
            Dim objLoginData As New clsLogin
            Dim OpeningTerminalBalance, CurrentTerminalBalance As Double
            ServerDate = ObjclsCommon.GetCurrentDate()
            If cmdNext.Tag = "Previous" Or cmdNext.Visible = False Then
                DTExtraAmount = DTMain.Copy
            End If

            If UCase(clsDefaultConfiguration.CheckTillOpenAmount) = "TRUE" Then
                If EditMode = True Then
                    If Not DTExtraAmount Is Nothing Then
                        OpeningTerminalBalance = LastDayCloseData.Compute("sum(Amount)", "")
                    End If
                    If Not LastDayCloseData Is Nothing Then
                        CurrentTerminalBalance = DTExtraAmount.Compute("sum(Amount)", "")
                    End If
                    If OpeningTerminalBalance <> CurrentTerminalBalance Then
                        ShowMessage(getValueByKey("frmntillopen.AmountInconsistent"), getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                    If objTill.RemoveData(clsAdmin.TerminalID, clsAdmin.SiteCode, clsAdmin.DayOpenDate) = False Then
                        ShowMessage(getValueByKey("TO01"), "TO01 - " & getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                Else
                    If UCase(clsDefaultConfiguration.CheckTillOpenAmount) = "TRUE" Then
                        If Not DTExtraAmount Is Nothing Then
                            Dim totalAmount As Double = DTExtraAmount.Compute("SUM(AMOUNT)", "")
                            If objTill.CheckIfValidTillOpenAmount(clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.CurrencyCode, totalAmount) = False Then
                                ShowMessage(getValueByKey("frmntillopen.AmountInconsistent"), getValueByKey("CLAE04"))
                                Exit Sub
                            End If
                        Else
                            If objTill.CheckIfValidTillOpenAmount(clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.CurrencyCode, 0) = False Then
                                ShowMessage(getValueByKey("frmntillopen.AmountInconsistent"), getValueByKey("CLAE04"))
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If

            If Not DTExtraAmount Is Nothing Then
                Call AddRows(DTExtraAmount, "ExtraOpen")
                DTExtraAmount.Rows.Clear()
            End If

            If Not DTTillOpen Is Nothing Then                '
                Call AddRows(DTTillOpen, "Open")
                DTTillOpen.Rows.Clear()
            End If
            If DTFloatingDetail1.Rows.Count > 0 Then
                DsFinalData.Tables.Add(DTFloatingDetail1)
            Else
                Call AddRows("ExtraOpen")
                DsFinalData.Tables.Add(DTFloatingDetail1)
            End If
            '----- Mahesh :- Code to check Floating Open Amount is zero then Insert a entry with zero denomation 
            'If (objTill.GetTillOpenAmount(clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.CurrencyCode) = 0) AndAlso DTFloatingDetail1.Rows.Count = 0 Then
            '    Dim tbool = objTill.AddExtraOpen(clsAdmin.TerminalID, clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.UserCode)
            'End If

            If objTill.SaveRecords(DsFinalData, NewEntry, clsAdmin.TerminalID, clsAdmin.SiteCode, clsAdmin.DayOpenDate) = False Then
                Me.Close()
                Exit Sub
            End If
            'If ConsistancyDone = True Then
            'If objTill.UpdateIncosDetail(ConsistancyDone, clsAdmin.TerminalID, clsAdmin.SiteCode, ServerDate, clsAdmin.DayOpenDate, "TillOpen", IncosAmt, clsAdmin.UserCode) = False Then
            '    Exit Sub
            'End If
            'End If
            If EditMode = False Then
                OpenTerminal()
                If clsDefaultConfiguration.SpectrumAsMettler Then
                    OpenSpectrumMettlerTillOpen()
                End If
                DisableTransactionMainMenu(True)
                clsAdmin.Financialyear = objLoginData.GetFinancialYear(clsAdmin.DayOpenDate, clsAdmin.SiteCode)
            End If
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
            If EditMode Then
                ShowMessage(String.Format("{0} Updated Successfully", clsAdmin.TerminalID), getValueByKey("CLAE04"))
            End If
            Me.Close()

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
            Me.Close()
        End Try
    End Sub

    Private Sub cmdNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        Try
            If sender.Tag = "Next" Then
                If ConsistancyDone = False And clsDefaultConfiguration.CheckTillCloseAmount = True Then
                    Dim PreviousAmt, InCosAmount As Double
                    PreviousAmt = DTPreviousDetails.Compute("Sum(TotalAmount)", "")
                    InCosAmount = PreviousAmt - CDbl(lblTotalAmount.Text)
                    If InCosAmount <> 0 Then
                        If MsgBox(getValueByKey("TO06") & " " & InCosAmount, MsgBoxStyle.OkCancel, "TO06 - " & getValueByKey("CLAE04")) = MsgBoxResult.Ok Then
                            IncosAmt = InCosAmount
                            DTTillOpen = DTMain.Copy
                            ConsistancyDone = True
                            NewEntry = True
                        Else
                            ClearGrid()
                            Exit Sub
                        End If
                    Else
                        DTTillOpen = DTMain.Copy
                        ConsistancyDone = True
                    End If
                    ObjclsCommon = New clsCommon()
                    DTMain = ObjclsCommon.GetDenomination(clsAdmin.CurrencyCode)
                    dgMainGrid.DataSource = DTMain
                    FormatGrid()
                    CalculateTotal(DTMain)
                    'cmdNext.Text = "&Previous"
                    cmdNext.Text = getValueByKey("frmntillopen.cmdnextprevious")
                    cmdNext.Tag = "Previous"
                    'Me.Text = "Add Extra Amount:"
                    Me.Text = getValueByKey("frmntillopen")
                Else
                    DTMain = DTExtraAmount
                    'cmdNext.Text = "&Previous"
                    cmdNext.Text = getValueByKey("frmntillopen.cmdnextprevious")
                    cmdNext.Tag = "Previous"
                    'Me.Text = "Add Extra Amount:"
                    Me.Text = getValueByKey("frmntillopen")
                    dgMainGrid.DataSource = DTMain
                    CalculateTotal(DTMain)
                End If
            ElseIf sender.tag = "Previous" Then
                DTExtraAmount = DTMain
                DTMain = DTTillOpen
                'cmdNext.Text = "&Next"
                cmdNext.Text = getValueByKey("frmntillopen.cmdnext")
                cmdNext.Tag = "Next"
                'Me.Text = "Enter Till Amount:"
                Me.Text = getValueByKey("frmntillopen1")
                dgMainGrid.DataSource = DTMain
                CalculateTotal(DTMain)
            End If
            FormatGrid()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReset.Click
        ClearGrid()
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
            'Dim xEntry As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName)
            'Dim ipAddr As Net.IPAddress() = xEntry.AddressList
            'Dim IpAddress As String = ipAddr(0).ToString()
            Dim IpAddress = objlog.getIPAddress()
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
            ShowMessage(getValueByKey("TO02"), "TO02 - " & getValueByKey("CLAE04"))
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
        Dim objTill As New clsTill()
        If objTill.CheckDayOpening(clsAdmin.SiteCode) = False Then
            ShowMessage(getValueByKey("LG008"), "TO03 - " & getValueByKey("CLAE04"))
            Me.Dispose()
            Me.Close()
            Exit Sub
        End If
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
        If objTill.CheckOpening(clsAdmin.TerminalID, clsAdmin.SiteCode, "Open','ExtraOpen") Then
            ShowMessage(getValueByKey("TO04"), "TO04 - " & getValueByKey("CLAE04"), EventType, True, getValueByKey("SMBTN001"))
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

    Private Sub AddRows(ByVal DTTable As DataTable, ByVal ActionType As String)
        Try
            Dim IsNewRow As Boolean = False
            Dim GenDataRow As DataRow
            Dim findKey(7) As Object
            'DTFloatingDetail1.Clear()
            Dim i As Integer = 0
            For i = 0 To DTTable.Rows.Count - 1
                If CDbl(DTTable.Rows(i).Item("Qty").ToString()) > 0 Then

                    findKey(0) = clsAdmin.SiteCode
                    findKey(1) = clsAdmin.TerminalID
                    findKey(2) = ActionType
                    findKey(3) = clsAdmin.DayOpenDate
                    findKey(4) = clsAdmin.CurrencyCode
                    findKey(5) = DTTable.Rows(i).Item("DenominationAmt")
                    findKey(6) = 1
                    findKey(7) = clsCommon._PrevShiftId

                    GenDataRow = DTFloatingDetail1.Rows.Find(findKey)
                    If GenDataRow Is Nothing Then
                        GenDataRow = DTFloatingDetail1.NewRow()
                        IsNewRow = True
                    End If
                    GenDataRow.Item("SiteCode") = clsAdmin.SiteCode
                    GenDataRow.Item("TerminalId") = clsAdmin.TerminalID
                    GenDataRow.Item("Action") = ActionType
                    GenDataRow.Item("FlotDateTime") = clsAdmin.DayOpenDate
                    GenDataRow.Item("CREATEDON") = ServerDate
                    GenDataRow.Item("CurrencyCode") = clsAdmin.CurrencyCode
                    GenDataRow.Item("DenominationAmt") = DTTable.Rows(i).Item("DenominationAmt")
                    GenDataRow.Item("Qty") = DTTable.Rows(i).Item("Qty")
                    GenDataRow.Item("TotalAmount") = DTTable.Rows(i).Item("AMOUNT")
                    GenDataRow.Item("CreatedAt") = clsAdmin.SiteCode
                    GenDataRow.Item("CreatedBY") = clsAdmin.UserCode
                    GenDataRow.Item("Status") = 1
                    GenDataRow.Item("UPDATEDON") = ServerDate
                    GenDataRow.Item("UPDATEDAT") = clsAdmin.SiteCode
                    GenDataRow.Item("UPDATEDBY") = clsAdmin.UserCode
                    GenDataRow.Item("Shiftid") = clsCommon._PrevShiftId
                    If IsNewRow = True Then
                        DTFloatingDetail1.Rows.Add(GenDataRow)
                        IsNewRow = False
                    End If
                End If
            Next
            'DsFinalData.Tables.Add(DTFloatingDetail1)
            'TAFloatingDetail.Update(DTFloatingDetail1)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub AddRows(ByVal ActionType As String)
        Try
            'DTFloatingDetail1.Clear()
            Dim i As Integer = 0
            GenDataRow = DTFloatingDetail1.NewRow()
            GenDataRow.Item("SiteCode") = clsAdmin.SiteCode
            GenDataRow.Item("TerminalId") = clsAdmin.TerminalID
            GenDataRow.Item("Action") = ActionType
            GenDataRow.Item("FlotDateTime") = clsAdmin.DayOpenDate
            GenDataRow.Item("CREATEDON") = ServerDate
            GenDataRow.Item("CurrencyCode") = clsAdmin.CurrencyCode
            GenDataRow.Item("DenominationAmt") = 0
            GenDataRow.Item("Qty") = 0
            GenDataRow.Item("TotalAmount") = 0
            GenDataRow.Item("CreatedAt") = clsAdmin.SiteCode
            GenDataRow.Item("CreatedBY") = clsAdmin.UserCode
            GenDataRow.Item("Status") = 1
            GenDataRow.Item("UPDATEDON") = ServerDate
            GenDataRow.Item("UPDATEDAT") = clsAdmin.SiteCode
            GenDataRow.Item("UPDATEDBY") = clsAdmin.UserCode
            GenDataRow.Item("Shiftid") = clsCommon._PrevShiftId
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
            Dim objTill As New clsTill()
            If objTill.OpenCloseTerminal(clsAdmin.TerminalID, clsAdmin.SiteCode, True, clsAdmin.UserCode) = True Then
                clsDefaultConfiguration.TillOpenDone = True
                MessageBox.Show(getValueByKey("TO05"), "TO05 - " & getValueByKey("CLAE04"))
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub PopulateGrid(ByRef TempDTMain As DataTable, ByVal ActionType As String, ByVal PreviousDetails As Boolean)
        Try
            Dim objTill As New clsTill
            If PreviousDetails = False Then
                Dim i As Integer
                For i = 0 To TempDTMain.Rows.Count - 1
                    DTFloatingDetail.Rows.Clear()

                    '    DTFloatingDetail = TAFloatingDetail.GetExistingFloatingDetail(ActionType, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.DayOpenDate)

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
        dgMainGrid.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
    End Function
#End Region

    Private Sub dgMainGrid_StartEdit(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgMainGrid.StartEdit
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub
    'code added by vipul for opening 
    Private Sub OpenSpectrumMettlerTillOpen()
        Try
            Dim ObjclsCommomSM As New clsCommon
            Dim objTill As New clsTill()
            Dim SpectrumPaymentMettlerTill As String = clsDefaultConfiguration.SpectrumMettlerPaymentTill
            Dim SpectrumMettlerTill As String = Nothing
            Dim SpectrumTill As DataTable = Nothing

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
            If Not String.IsNullOrEmpty(SpectrumMettlerTill) Then
                If objTill.OpenSpectrumMettlerTillOnTillOpen(SpectrumMettlerTill, clsAdmin.SiteCode, clsAdmin.UserCode, Nothing) = True Then

                End If
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

End Class
