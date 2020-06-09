Imports SpectrumCommon
Imports System.ComponentModel
Imports SpectrumBL
Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Collections
Imports System.Linq
Imports System.Net.Mail
Imports Spectrum.SpectrumUpdate
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates


Public Class frmStockTake
    Public Delegate Sub DayClosedDelegate()
    Dim d1 As DayClosedDelegate
    Public DayClosed As System.Action
    Public Delegate Sub DayCloseClosingDelegate()
    Dim screenCloser As DayCloseClosingDelegate
    Dim FormCloseByUser As Boolean = True
    Dim StokeTakeScreenData As String = String.Empty

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.      

        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Private _dayCloseController As StockTakeController
    Public Property DayCloseController As StockTakeController
        Get
            Return _dayCloseController
        End Get
        Set(ByVal value As StockTakeController)
            _dayCloseController = value
        End Set
    End Property
    Private _CashDetailsList As BindingList(Of CashDenominationDtl)
    Public Property CashDetailsList As BindingList(Of CashDenominationDtl)
        Get
            Return _CashDetailsList
        End Get
        Set(ByVal value As BindingList(Of CashDenominationDtl))
            _CashDetailsList = value
        End Set
    End Property
    Private _ChequeDetailsList As List(Of ChequeDetails)
    Public Property ChequeDetailsList As List(Of ChequeDetails)
        Get
            Return _ChequeDetailsList
        End Get
        Set(ByVal value As List(Of ChequeDetails))
            _ChequeDetailsList = value
        End Set
    End Property
    Private _Instance As IDayCloseBankDetails
    Public ReadOnly Property Instance As IDayCloseBankDetails
        Get
            If _Instance Is Nothing Then
                _Instance = New DayCloseBankDetails()
            End If
            Return _Instance
        End Get
    End Property

    Private Sub frmDayCloseMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            DayCloseController = New StockTakeController()
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    'code added for jk sprint 25
    Private Sub writeDaycloseLog(ByVal mes As String)
        Dim ax As New ApplicationException(mes)
        LogException(ax)
    End Sub
    Public Delegate Sub PictureVisibilityDelegate(ByVal visibility As Integer)
    Dim ChangePictureVisibility As PictureVisibilityDelegate


    Public Shared Socketdata As String = Nothing
    Dim waitPopupMsg As frmSpecialPrompt
    Private Sub RunBatchFile()
        If System.IO.File.Exists(clsDefaultConfiguration.AutoRunPathAfterDayClose) Then
            Process.Start(clsDefaultConfiguration.AutoRunPathAfterDayClose)
        End If
    End Sub

    Private Sub frmDayCloseMain_Closing(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.FormClosing
        'log added for stock take by vipul 
        If FormCloseByUser = True Then

            writeDaycloseLog("manually stocktake form close by user")
        Else
            writeDaycloseLog("stocktake form close by application")
        End If
        FormCloseByUser = True
        If Not screenCloser Is Nothing Then
            screenCloser()
        End If
    End Sub
    Public Sub GetNextScreen()
        If DayCloseController.CurrentScreen IsNot Nothing Then
            If SaveDayCloseData(DayCloseController.CurrentScreen) = False Then
                'ShowMessage("Error in Saving Data", "Error")
                Exit Sub
            End If
        End If
        Dim screenInfo As DayCloseScreenConfig = DayCloseController.GetNextScreen()
        If screenInfo IsNot Nothing Then
            ScreenVisiblityController(screenInfo)

            If (screenInfo.ScreenName = "WastageDetails") Then
                btnNext.Text = "&Next"
                btnNext.Enabled = True
                btnBack.Enabled = False
            Else
                btnNext.Text = "&Save"
                btnNext.Enabled = True
                btnBack.Enabled = True
            End If
        End If
       

    End Sub

    Public Sub GetPreviousScreen()
        Dim screenInfo As DayCloseScreenConfig = DayCloseController.GetPreviousScreen()
        If screenInfo IsNot Nothing Then
            ScreenVisiblityController(screenInfo)
        End If
        If (screenInfo.ScreenName = "WastageDetails") Then
            btnNext.Text = "&Next"
            btnNext.Enabled = True
            btnBack.Enabled = False
        Else
            btnNext.Text = "&Save"
            btnNext.Enabled = True
            btnBack.Enabled = True
        End If
    End Sub
    Public Sub SyncOnDSR_Report()
        Try

            Dim IpAddr, port, syncport As String
            Dim objs As New clsCommon
            Dim dtInfo As New DataTable
            dtInfo = objs.GetRemoteIpPort()
            If dtInfo.Rows.Count > 0 Then
                For Each row In dtInfo.Rows
                    If row("FldLabel").ToString() = "WebService.Remote.IP" Then
                        IpAddr = row("FldValue").ToString()
                    ElseIf row("FldLabel").ToString() = "WebService.Remote.PORT" Then
                        port = row("FldValue").ToString()
                        'modified by khusrao adil on 12-10-2017 for jk sprint 30
                    ElseIf row("FldLabel").ToString() = "SYNCH_SERVER_LOCAL_PORT" AndAlso clsDefaultConfiguration.SynchBatFile = "run_dayCloseSynch.bat" Then
                        syncport = row("FldValue").ToString()
                        'added by khusrao adil on 12-10-2017 for jk sprint 30
                    ElseIf row("FldLabel").ToString() = "STOCK_TECH_SYNCH_SERVER_LOCAL_PORT" AndAlso clsDefaultConfiguration.SynchBatFile = "run_stocktech_sync.bat" Then
                        syncport = row("FldValue").ToString()
                    End If
                Next
            End If
            '  If clsDefaultConfiguration.SyncOnDayClose Then
            Try

                If dtInfo IsNot Nothing Then
                    writeDaycloseLog("1 ST Checking google.com as internet connected")
                    Try
                        Using client = New WebClient()
                            Using stream = client.OpenRead("http://www.google.com")
                            End Using
                        End Using
                    Catch ex As Exception
                        LogException(ex)
                        ShowMessage("2 st Weak or No Internet Connection!", getValueByKey("CLAE04"))
                        Exit Sub
                    End Try
                    writeDaycloseLog("3 Checking google.com as internet connected sucess")
                    If dtInfo.Rows.Count > 0 Then


                        writeDaycloseLog("4 Checking CCE server as connected ")
                        Dim client As New WebClient()
                        client.Headers("Content-type") = "application/json"
                        ' invoke the REST method
                        Dim data As Byte()
                        Try
                            data = client.DownloadData("http://" & IpAddr & ":" & port & "/posSeam/rest/internetlog/check")
                        Catch ex As Exception
                            ShowMessage("Server Down!", getValueByKey("CLAE04"))
                            LogException(ex)
                            Exit Sub
                        End Try

                        Dim vOut As String = System.Text.Encoding.UTF8.GetString(data)

                        If vOut = "SUCCESS" Then
                            writeDaycloseLog(" 5 Checking CCE server as connected Sucess")
                            If Not Me.IsHandleCreated Then
                                Me.CreateControl()
                            End If
                            ' Me.Invoke(ChangePictureVisibility, 0)
                            '------    ''' CODE Commented By Mahesh Client Socket Now will Implement Server Socket ...
                            '----- FO as Server by Mahesh
                            Try
                                '--commed by Mahesh as it will be done by service seperatly ..
                                ''  Dim setDayclose As Boolean = objs.UpdateDayCloseStaus(clsAdmin.SiteCode, clsAdmin.DayOpenDate, True)
                                'If setDayclose Then
                                ' Data buffer for incoming data.
                                Dim bytes() As Byte = New [Byte](1024) {}

                                ' Establish the local endpoint for the socket. Dns.GetHostName returns the name of the host running the application.
                                Dim ipHostInfo As IPHostEntry = Dns.Resolve("localhost") 'Dns.GetHostName()
                                Dim ipAddress As IPAddress = ipHostInfo.AddressList(0)
                                Dim localEndPoint As New IPEndPoint(ipAddress, syncport)

                                ' Create a TCP/IP socket.
                                Using listener As New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                                    ' Bind the socket to the local endpoint and listen for incoming connections.
                                    listener.Bind(localEndPoint)
                                    listener.Listen(10)
                                    Dim handler As Socket = Nothing
                                    'dataSource = "DT082"
                                    Try
                                        '---- Run RunDayClose.bat file 
                                        'Dim Proc As New System.Diagnostics.Process
                                        'Proc.StartInfo = New ProcessStartInfo(Application.StartupPath & "\run_dayCloseSynch.bat")
                                        'Proc.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
                                        'Proc.Start()
                                        'added by khusrao adil on 12-10-2017 for jk sprint 30
                                        Dim SynchBatchfile = ""
                                        If clsDefaultConfiguration.SynchBatFile <> "" Then
                                            SynchBatchfile = clsDefaultConfiguration.SynchBatFile
                                        Else
                                            SynchBatchfile = "run_dayCloseSynch.bat"
                                        End If
                                        Shell(Application.StartupPath & "\" & SynchBatchfile, AppWinStyle.Hide)
                                        'Shell(Application.StartupPath & "\run_dayCloseSynch.bat", AppWinStyle.Hide)
                                        'writeDaycloseLog("6 started process run_dayCloseSynch.bat ")
                                        writeDaycloseLog("6 started process " & SynchBatchfile & "")
                                        '// Poll the socket for reception with a 30 Mins(1800000000) timeout.
                                        If (listener.Poll(1800000000, SelectMode.SelectRead)) Then
                                            bytes = New Byte(1024) {}
                                            handler = listener.Accept()
                                            writeDaycloseLog(" 7 Get Reply from " & SynchBatchfile & "")
                                        Else
                                            '  Me.Invoke(ChangePictureVisibility, 5)
                                            '    objs.UpdateDayCloseStaus(clsAdmin.SiteCode, clsAdmin.DayOpenDate, False)
                                            writeDaycloseLog(" 8 NO Reply from " & SynchBatchfile & " ")
                                            Exit Sub
                                        End If
                                        Socketdata = Nothing

                                        bytes = New Byte(1024) {}
                                        Dim bytesRec As Integer = handler.Receive(bytes)
                                        Socketdata += Encoding.UTF8.GetString(bytes, 0, bytesRec)
                                        writeDaycloseLog(Socketdata)
                                        If Socketdata <> "" Then
                                            If Not Socketdata.Contains("DAY_CLOSE_SYNCH_SUCCESS") Then
                                                If Socketdata.Contains("DAY_CLOSE_SYNCH_FAILURE") Then
                                                    ' Me.Invoke(ChangePictureVisibility, 5)
                                                ElseIf Socketdata.Contains("NETWORK_FAILURE") Then
                                                    '   Me.Invoke(ChangePictureVisibility, 6)
                                                ElseIf Socketdata.Contains("INVALID_REQUEST_PARAM") Then
                                                    '  Me.Invoke(ChangePictureVisibility, 7)
                                                ElseIf Socketdata.Contains("INTERNAL_SERVER_ERROR") Then
                                                    '  Me.Invoke(ChangePictureVisibility, 8)
                                                Else
                                                    '   Me.Invoke(ChangePictureVisibility, 9)
                                                End If
                                                handler.Shutdown(SocketShutdown.Both)
                                                handler.Close()

                                                '''''''''''''''''''''''''''''''''''''''
                                                'objs.UpdateDayCloseStaus(clsAdmin.SiteCode, clsAdmin.DayOpenDate, False)
                                                '''''''''''''''''''''''''''''''''''
                                                Exit Sub
                                            Else
                                                writeDaycloseLog(" 9 GEt Reply  from " & SynchBatchfile & " Reply is DAY_CLOSE_SYNCH_SUCCESS ")
                                            End If

                                        Else
                                            '''''''''''''''''''''''''''''''
                                            'objs.UpdateDayCloseStaus(clsAdmin.SiteCode, clsAdmin.DayOpenDate, False)
                                            '''''''''''''''''''''''''''''
                                            ' Me.Invoke(ChangePictureVisibility, 2)
                                            Exit Sub
                                        End If

                                    Catch ex As Exception
                                        writeDaycloseLog(ex.ToString())
                                        ' objs.UpdateDayCloseStaus(clsAdmin.SiteCode, clsAdmin.DayOpenDate, False)
                                        '  Me.Invoke(ChangePictureVisibility, 4)
                                        Exit Sub
                                    Finally
                                        If Not (handler Is Nothing) Then
                                            handler.Shutdown(SocketShutdown.Both)
                                            handler.Close()
                                        End If
                                    End Try
                                End Using
                            Catch ex As Exception
                                writeDaycloseLog(ex.ToString())
                                '       objs.UpdateDayCloseStaus(clsAdmin.SiteCode, clsAdmin.DayOpenDate, False)
                                '   Me.Invoke(ChangePictureVisibility, 2)
                                Exit Sub
                            End Try
                            '----- FO as Server by Mahesh - END 
                        Else
                            '  btnDayClose.Enabled = True
                            writeDaycloseLog(" 10 Checking CCE server as connected Failed ")
                            waitPopupMsg.Close()
                            ShowMessage("Weak or No Internet Connection!", getValueByKey("CLAE04"))
                            Exit Sub
                        End If

                    End If
                Else
                    ' btnDayClose.Enabled = True
                    writeDaycloseLog("11 CCE Details are not  configured properly ")
                    ShowMessage("Weak or No Internet Connection!", getValueByKey("CLAE04"))
                    Exit Sub
                End If
            Catch ex As Exception
                ' btnDayClose.Enabled = True
                LogException(ex)
                Return
            End Try
            '  End If

            ''Synch on Day Close End     

        Catch ex As Exception
            LogException(ex)
        Finally
            Try
                ' bw.CancelAsync()
                'If bw.CancellationPending = True Then
                '    e.Cancel = True
                'End If
                'System.Threading.Thread.Sleep(20000)
            Catch ex As Exception
                LogException(ex)
            End Try
        End Try


    End Sub
    Private Function SaveDayCloseData(ByRef screenInfo As DayCloseScreenConfig) As Boolean
        Try
            Dim result As Boolean = False
            Select Case screenInfo.ScreenName
                Case DayCloseScreens.SubProductDetails.ToString()
                    If SubProductDetails.IsTotalDataValid() Then
                        Dim dayCloseSaveDataReq As New DayCloseDataRequestModel(Of SpectrumCommon.SubProductDetails)
                        dayCloseSaveDataReq.DayCloseDate = clsAdmin.DayOpenDate
                        dayCloseSaveDataReq.SiteCode = clsAdmin.SiteCode
                        dayCloseSaveDataReq.UserId = clsAdmin.UserCode
                        dayCloseSaveDataReq.DayCloseData = SubProductDetails.SubProductList
                        result = SubProductDetails.Instance.SaveDayCloseData(dayCloseSaveDataReq)
                    End If
                Case DayCloseScreens.FinishedProductDetails.ToString()
                    If FinishedProductDetails.IsTotalDataValid() Then
                        Dim dayCloseSaveDataReq As New DayCloseDataRequestModel(Of FinishedProductDetails)
                        dayCloseSaveDataReq.DayCloseDate = clsAdmin.DayOpenDate
                        dayCloseSaveDataReq.SiteCode = clsAdmin.SiteCode
                        dayCloseSaveDataReq.UserId = clsAdmin.UserCode
                        dayCloseSaveDataReq.DayCloseData = FinishedProductDetails.FinishedProductList
                        result = FinishedProductDetails.Instance.SaveDayCloseData(dayCloseSaveDataReq)
                    End If
                Case DayCloseScreens.WastageDetails.ToString()
                    If WastageDetails.IsTotalDataValid() Then
                        Dim dayCloseSaveDataReq As New DayCloseDataRequestModel(Of WastageDetails)
                        dayCloseSaveDataReq.DayCloseDate = clsAdmin.DayOpenDate
                        dayCloseSaveDataReq.SiteCode = clsAdmin.SiteCode
                        dayCloseSaveDataReq.UserId = clsAdmin.UserCode
                        dayCloseSaveDataReq.DayCloseData = WastageDetails.WastageDetailsList
                        result = WastageDetails.Instance.SaveDayCloseData(dayCloseSaveDataReq)
                    End If
                Case DayCloseScreens.StockTakedetails.ToString()
                    If StockTakeDetails.IsTotalDataValid() Then
                        Dim dayCloseSaveDataReq As New DayCloseDataRequestModel(Of StockTakeDetails)
                        dayCloseSaveDataReq.DayCloseDate = clsAdmin.DayOpenDate
                        dayCloseSaveDataReq.SiteCode = clsAdmin.SiteCode
                        dayCloseSaveDataReq.UserId = clsAdmin.UserCode
                        dayCloseSaveDataReq.DayCloseData = StockTakeDetails.StockTakeList
                        result = StockTakeDetails.Instance.SaveDayCloseData(dayCloseSaveDataReq)
                        StokeTakeScreenData = String.Empty
                        StokeTakeScreenData = LogForStokeTake(StockTakeDetails.StockTakeList)

                        If result = True Then
                            writeDaycloseLog(vbNewLine + StokeTakeScreenData + vbNewLine + "save data successfully")
                        Else
                            writeDaycloseLog(vbNewLine + StokeTakeScreenData + vbNewLine + "data not save successfully")
                        End If
                        If clsDefaultConfiguration.AutoSyncOnDSR = True Then

                            writeDaycloseLog(vbNewLine + " performing sync on stock take screen")
                            SyncOnDSR_Report()
                        End If

                        writeDaycloseLog(" generating stock take report ")
                        Call GenerateDSRReport()
                        FormCloseByUser = False
                        Me.Close()
                        FormCloseByUser = True
                    End If
            End Select
            Return result
        Catch ex As Exception
            LogException(ex)
            ShowMessage(getValueByKey("daycloseerrormsg"), getValueByKey("CLAE05"))
            Return False
        End Try
    End Function

    Private Sub ScreenVisiblityController(ByRef screenInfo As DayCloseScreenConfig)
        Try
            Select Case screenInfo.ScreenName
                Case DayCloseScreens.SubProductDetails.ToString()
                    WastageDetails.Visible = False
                    FinishedProductDetails.Visible = False
                    StockTakeDetails.Visible = False
                    DayCloseBankDetails.Visible = False
                    SubProductDetails.Visible = True
                    d1 = System.Delegate.Combine(d1, New DayClosedDelegate(AddressOf SubProductDetails.EnableReadOnlyMode))
                    SubProductDetails.GetDayCloseData(New DayCloseDataRequestModel(Of SpectrumCommon.SubProductDetails) With {.DayCloseDate = clsAdmin.DayOpenDate, .Query = screenInfo.Script, .SiteCode = clsAdmin.SiteCode})
                    Me.Text = "Day Close - SubProductDetails"
                Case DayCloseScreens.FinishedProductDetails.ToString()
                    SubProductDetails.Visible = False
                    WastageDetails.Visible = False
                    StockTakeDetails.Visible = False
                    DayCloseBankDetails.Visible = False
                    FinishedProductDetails.Visible = True
                    d1 = System.Delegate.Combine(d1, New DayClosedDelegate(AddressOf FinishedProductDetails.EnableReadOnlyMode))
                    FinishedProductDetails.GetDayCloseData(New DayCloseDataRequestModel(Of FinishedProductDetails) With {.DayCloseDate = clsAdmin.DayOpenDate, .Query = screenInfo.Script, .SiteCode = clsAdmin.SiteCode})
                    Me.Text = "Day Close - FinishedProductDetails"
                Case DayCloseScreens.WastageDetails.ToString()
                    SubProductDetails.Visible = False
                    FinishedProductDetails.Visible = False
                    StockTakeDetails.Visible = False
                    DayCloseBankDetails.Visible = False
                    WastageDetails.Visible = True
                    d1 = System.Delegate.Combine(d1, New DayClosedDelegate(AddressOf WastageDetails.EnableReadOnlyMode))
                    WastageDetails.GetDayCloseData(New DayCloseDataRequestModel(Of WastageDetails) With {.DayCloseDate = clsAdmin.DayOpenDate, .Query = screenInfo.Script, .SiteCode = clsAdmin.SiteCode})
                    screenCloser = System.Delegate.Combine(screenCloser, New DayCloseClosingDelegate(AddressOf WastageDetails.CtrlWastageDetails_Closing))
                    Me.Text = "Day Close - WastageDetails"
                Case DayCloseScreens.StockTakedetails.ToString()
                    SubProductDetails.Visible = False
                    FinishedProductDetails.Visible = False
                    WastageDetails.Visible = False
                    DayCloseBankDetails.Visible = False
                    StockTakeDetails.Visible = True

                    If clsDefaultConfiguration.HideControlsFromStockTake Then
                        StockTakeDetails.btnRawMaterialClosing.Visible = False
                    Else
                        StockTakeDetails.btnRawMaterialClosing.Visible = True
                    End If

                    ' StockTakeDetails.btnRawMaterialClosing.Visible = True
                    d1 = System.Delegate.Combine(d1, New DayClosedDelegate(AddressOf StockTakeDetails.EnableReadOnlyMode))
                    StockTakeDetails.GetStockGroupDetails()
                    StockTakeDetails.GetDayCloseData(New DayCloseDataRequestModel(Of StockTakeDetails) With {.DayCloseDate = clsAdmin.DayOpenDate, .Query = screenInfo.Script, .SiteCode = clsAdmin.SiteCode})
                    screenCloser = System.Delegate.Combine(screenCloser, New DayCloseClosingDelegate(AddressOf StockTakeDetails.CtrlStockTakeDetails_Closing))
                    Me.Text = "Day Close - StockTakeDetails"
                    writeDaycloseLog("currently user on stocktake screen")
                Case DayCloseScreens.BankDetails.ToString()
                    If DayCloseBankDetails.CheckIfAllTerminalAreCLosed() Then
                        SubProductDetails.Visible = False
                        FinishedProductDetails.Visible = False
                        WastageDetails.Visible = False
                        StockTakeDetails.Visible = False
                        DayCloseBankDetails.Visible = True
                        DayCloseBankDetails.DayClosed = AddressOf EnableReadOnlyMode
                        DayCloseBankDetails.GetDayCloseBankDetailsData(New DayCloseBankDetailsRequest With {.DayCloseDate = clsAdmin.DayOpenDate, .FinYear = clsAdmin.Financialyear, .SiteCode = clsAdmin.SiteCode, .IsShiftManagement = clsDefaultConfiguration.ShiftManagement})
                        Me.Text = "Day Close - BankDetails"
                    Else
                        Dim screenInfo1 As DayCloseScreenConfig = DayCloseController.GetPreviousScreen()
                        If clsDefaultConfiguration.ShiftManagement Then
                            ShowMessage(getValueByKey("daycloseshiftclosemsg"), getValueByKey("CLAE04"))
                        Else
                            ShowMessage(getValueByKey("daycloseterminalclosemsg"), getValueByKey("CLAE04"))
                        End If
                    End If
            End Select
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try
            GetNextScreen()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            GetPreviousScreen()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub EnableReadOnlyMode()
        Try
            If d1 IsNot Nothing Then
                d1()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function GenerateDSRReport() As Boolean
        Try

            Dim dsDsr As New DataSet()
            Dim clsObj As New DsrReport
            dsDsr = clsObj.GetDsrReportData(clsAdmin.SiteCode, clsAdmin.DayOpenDate)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\SalesReportJumboKing.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            Dim dateNameParam As New ReportParameter("Date", clsAdmin.DayOpenDate)
            Dim SiteCodeParam As New ReportParameter("Sitecode", clsAdmin.SiteCode)

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
            Dim path = clsDefaultConfiguration.DayCloseReportPath & "\DSR_" & DateTime.Now.ToString("dd-MM-yyyy-hhmmss") & ".xls"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            Process.Start(path)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Function Themechange()
        DayCloseHeader1.BackColor = Color.FromArgb(134, 134, 134)
        Me.BackColor = Color.FromArgb(134, 134, 134)
        btnBack.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnBack.BackColor = Color.Transparent
        btnBack.BackColor = Color.FromArgb(0, 107, 163)
        btnBack.ForeColor = Color.FromArgb(255, 255, 255)
        ' btnBack.Size = New Size(71, 30)
        btnBack.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnBack.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnBack.FlatStyle = FlatStyle.Flat
        btnBack.FlatAppearance.BorderSize = 0
        btnBack.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        btnNext.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnNext.BackColor = Color.Transparent
        btnNext.BackColor = Color.FromArgb(0, 107, 163)
        btnNext.ForeColor = Color.FromArgb(255, 255, 255)
        '     btnBack.Size = New Size(71, 30)<
        btnNext.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnNext.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnNext.FlatStyle = FlatStyle.Flat
        btnNext.FlatAppearance.BorderSize = 0
        btnNext.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
    End Function

End Class