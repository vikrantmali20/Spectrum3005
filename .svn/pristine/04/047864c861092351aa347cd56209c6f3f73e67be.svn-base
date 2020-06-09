Imports SpectrumCommon
Imports SpectrumBL
Imports System.Collections
Imports System.ComponentModel
Imports System.Linq
Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.Net.Mail
Imports System.Net
Imports System.Text
Imports Spectrum.SpectrumUpdate
Imports System.Net.Sockets
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates

Public Class CtrlDayCloseBankDetailsPC
    Public DayClosed As System.Action
    Private directoryPath As String = Application.StartupPath
    Dim path As String = ""
    Dim JKDayOfReporthPath As String = ""
    Dim JKProductMixReportPath As String = ""
    Public Delegate Sub Temp1()
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
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
    Private _OherTendetDtl As List(Of OtherTenderDetail)
    Public Property OtherTenderDtl As List(Of OtherTenderDetail) 'vipin on 27-04-2017
        Get
            Return _OherTendetDtl
        End Get
        Set(ByVal value As List(Of OtherTenderDetail))
            _OherTendetDtl = value
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

    Private _DayCloseResponse As DayCloseBankDetailsResponse
    Public Property DayCloseResponse As DayCloseBankDetailsResponse
        Get
            Return _DayCloseResponse
        End Get
        Set(ByVal value As DayCloseBankDetailsResponse)
            _DayCloseResponse = value
        End Set
    End Property


    Private Sub CtrlDayCloseBankDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            dgCashDetails.AutoGenerateColumns = False
            dgCashDetails.Columns("CurrencyCode").DefaultCellStyle.BackColor = Drawing.Color.LightGray
            dgCashDetails.Columns("DenominationAmt").DefaultCellStyle.BackColor = Drawing.Color.LightGray
            dgCashDetails.Columns("Quantity").DefaultCellStyle.BackColor = Drawing.Color.LightGray
            dgCashDetails.Columns("TotalAmt").DefaultCellStyle.BackColor = Drawing.Color.LightGray
            dgChequeDetails.AutoGenerateColumns = False
            ' dgChequeDetails.Columns("ChequeNumber").DefaultCellStyle.BackColor = Drawing.Color.LightGray
            ' dgChequeDetails.Columns("Amount").DefaultCellStyle.BackColor = Drawing.Color.LightGray
            If CheckAuthorisation(clsAdmin.UserCode, "Reports") = False Then
                btnDayCloseSalesReport.Visible = False
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub GetDayCloseBankDetailsData(ByRef request As DayCloseBankDetailsRequest)
        Try
            If CashDetailsList Is Nothing OrElse CashDetailsList.Count = 0 Then

                Dim response As DayCloseBankDetailsResponse = Instance.GetDayCloseBankDetails(request, clsDefaultConfiguration.AutoPopulateQtyForAllDayCloseScreens, clsDefaultConfiguration.ClientForMail)
                DayCloseResponse = response
                CashDetailsList = response.CashDenominationList.ToBindingList()
                ChequeDetailsList = response.ChequeDetailsList
                If (clsDefaultConfiguration.ClientForMail = "PC") Then
                    OtherTenderDtl = response.OtherTenderDetail    'vipin on27-04-2017
                    dgOtherTenderDetail.DataSource = OtherTenderDtl  'vipin on27-04-2017
                    LblImprestCashAmt.Text = response.ImprestAmount
                End If
                dgCashDetails.DataSource = CashDetailsList
                dgChequeDetails.DataSource = ChequeDetailsList

                lblActualTotal.Text = response.ActualTotalCashAmt
                lblTotalChequeAmt.Text = response.TotalChequeAmt
                lblAmtGoingToBank.Text = response.AmountGoingToBank
                lblTotalQuantity.Text = 0
                For Each item In CashDetailsList
                    item.QuantityChanged = AddressOf CalculateTotalUserEnteredQty
                Next

                Me.dgChequeDetails.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Me.dgChequeDetails.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Me.dgChequeDetails.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Me.dgChequeDetails.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Me.dgChequeDetails.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                '  Me.dgOtherTenderDetail.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Me.dgOtherTenderDetail.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Me.dgOtherTenderDetail.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Me.dgOtherTenderDetail.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Me.dgOtherTenderDetail.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Function CheckIfAllTerminalAreCLosed() As Boolean
        Try
            Return Instance.CheckIfAllTerminalAreClosed(clsAdmin.SiteCode)
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Sub CalculateTotalUserEnteredQty()
        Try

            lblTotalQuantity.Text = CashDetailsList.Sum(Function(item) IIf(item.EnteredQuantity Is Nothing, 0, item.EnteredQuantity))
            'lblAmtGoingToBank.Text = DayCloseResponse.TotalChequeAmt + CashDetailsList.Sum(Function(item) item.TotalAmt)
            '--- Changed By Mahesh as Disscussed with vinit only cash details denomiation will shown to match Cheque Details 
            lblAmtGoingToBank.Text = CashDetailsList.Sum(Function(item) item.TotalAmt)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function IsTotalDataValid() As Boolean
        Try
            Dim _isValid As Boolean

            If (CashDetailsList IsNot Nothing) Then
                _isValid = Not (CashDetailsList.Any(Function(subProduct) IIf(subProduct.EnteredQuantity Is Nothing, 0, subProduct.EnteredQuantity) < 0))
                If _isValid = False Then
                    ShowMessage(getValueByKey("dayclosevalidDataMsg"), getValueByKey("CLAE04"))
                End If
            Else
                _isValid = True
            End If

            Return _isValid
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    'auto-update on day close ..day close will happen after update automaticallyJK requested on 14-oct-2015 
    'change location of AutoUpdate() method From  CtrlDayCloseBankDetails to MDISpectrum Changes made by Ketan

    'Private Shared Function CreateExecuteServiceObject(languageCode As String, webMethod As String, hardwareKey As String, siteCode As String, terminalId As String) As executeService
    '    Try
    '        Dim exs As New executeService()

    '        Dim req As New wsRequest()
    '        req.languageCode = languageCode
    '        req.webMethod = webMethod

    '        Dim soap As New soapWsHeader()
    '        soap.userName = ""
    '        soap.password = ""
    '        req.soapWsHeader = soap

    '        Dim row As New dynaRow()

    '        Dim col As New dynaColumn()
    '        col.columnName = "hardwareKey"
    '        col.columnType = "STRING"
    '        col.columnValue = hardwareKey

    '        Dim col2 As New dynaColumn()
    '        col2.columnName = "siteCode"
    '        col2.columnType = "STRING"
    '        col2.columnValue = siteCode

    '        Dim col3 As New dynaColumn()
    '        col3.columnName = "terminalId"
    '        col3.columnType = "STRING"
    '        col3.columnValue = terminalId



    '        Dim cols As New List(Of dynaColumn)()
    '        cols.Add(col)
    '        cols.Add(col2)
    '        cols.Add(col3)


    '        row.dynaColumn = cols.ToArray()
    '        req.dynaColumns = row

    '        exs.arg0 = req


    '        Return exs
    '    Catch ex As Exception

    '        Return Nothing
    '    End Try
    'End Function
    'Private Sub DeleteDirectory(path As String)
    '    If Directory.Exists(path) Then
    '        'Delete all files from the Directory
    '        For Each filepath As String In Directory.GetFiles(path)
    '            File.Delete(filepath)
    '        Next
    '        'Delete all child Directories
    '        For Each dir As String In Directory.GetDirectories(path)
    '            DeleteDirectory(dir)
    '        Next
    '        'Delete a Directory
    '        Directory.Delete(path)
    '    End If
    'End Sub
    'Public Sub AutoUpdates()
    '    Try


    '        Dim proxy As New FOAutomaticVersionUpgradeServiceImplClient
    '        Dim exs As New executeService
    '        Dim exser As New executeService



    '        Dim licence As New ClsLicense
    '        Dim HDDKey = licence.GetEncryptedHDDKey()
    '        Dim exsA As executeService = CreateExecuteServiceObject("EN", "isUpdateAvailable", HDDKey, clsAdmin.SiteCode, clsAdmin.TerminalID)

    '        Dim resp As executeServiceResponse = proxy.executeService(exsA)
    '        Dim resp1 As wsResponse = resp.[return]
    '        If resp1.responseCode = "200" Then
    '            Dim message As New StringBuilder
    '            message.Append("Update for a new Spectrum version is available.")
    '            message.Append("System will now install the new update and restart Spectrum.")
    '            message.Append("Spectrum will restart and the login screen will appear once the update installation is done.")
    '            message.Append(" Continue with Day Close operation once the update installation is completed.")

    '            ShowBigMessagewithOK(message.ToString(), "DBCB004 - " & getValueByKey("CLAE04"))
    '            Application.DoEvents()
    '            Dim objCls As New clsCommon
    '            directoryPath = directoryPath & "\Update"
    '            If Directory.Exists(directoryPath) Then
    '                'Directory.Delete(directoryPath)
    '                DeleteDirectory(directoryPath)
    '            End If
    '            Directory.CreateDirectory(directoryPath)




    '            Dim req As New wsRequest
    '            req.languageCode = "EN-US"
    '            req.webMethod = "fetchExeFile"

    '            Dim soap As New soapWsHeader
    '            soap.userName = ""
    '            soap.password = ""
    '            req.soapWsHeader = soap

    '            exser.arg0 = req

    '            Dim exresp As executeServiceResponse = proxy.executeService(exser)

    '            Dim wsResonse As wsResponse = exresp.return

    '            Dim t As dynaTable = wsResonse.dynaTables.FirstOrDefault()
    '            Dim q As dynaRow = t.dynaRows.FirstOrDefault()
    '            Dim w As dynaColumn = q.dynaColumn.FirstOrDefault()
    '            Dim updatezip As String = w.columnValue

    '            Dim bytIn() As Byte = System.Convert.FromBase64String(updatezip)
    '            Dim wFile As FileStream = New FileStream(directoryPath & "\setup.zip", FileMode.Create)
    '            wFile.Write(bytIn, 0, bytIn.Length)
    '            wFile.Close()


    '            Using zip1 As Ionic.Zip.ZipFile = Ionic.Zip.ZipFile.Read(directoryPath & "\setup.zip")
    '                zip1.ExtractAll(directoryPath,
    '                        Ionic.Zip.ExtractExistingFileAction.OverwriteSilently)
    '            End Using

    '            Dim dataSource As String = ReadSpectrumParamFile("Server")
    '            Dim dbname As String = ReadSpectrumParamFile("DataSource")
    '            Dim username As String = ReadSpectrumParamFile("UserId")
    '            Dim password As String = ReadSpectrumParamFile("Password")
    '            Dim clientname As String = objCls.GetSiteName("CCE")

    '            'HDDKey = "c4:43:8f:43:e4:2f"
    '            Dim pHelp As New ProcessStartInfo
    '            pHelp.FileName = "SpectrumUpdate.exe"
    '            pHelp.Arguments = "" & dataSource & " " & dbname & " " & username & " " & password & " " & clientname.Replace(" ", "") & " " & HDDKey & " " & clsAdmin.SiteCode & " " & clsAdmin.TerminalID & ""
    '            pHelp.WorkingDirectory = directoryPath
    '            'pHelp.WorkingDirectory = "C:\Users\sagar.borole\Desktop\ConsoleApplication1"
    '            Process.Start(pHelp)
    '            Application.Exit()
    '            'Else
    '            '    ShowMessage("New Version not available", "DBCB004 - " & getValueByKey("CLAE04"))

    '        End If
    '    Catch ex As Exception
    '        'MessageBox.Show("Error")
    '        LogException(ex)
    '        If (ex.ToString().Contains("no endpoint")) Then
    '            ShowMessage("Invalid CCE Connection", "DBCB004 - " & getValueByKey("CLAE04"))
    '        End If
    '        isClosed = True
    '    End Try
    'End Sub
    Dim bw As BackgroundWorker
    Public Delegate Sub PictureVisibilityDelegate(ByVal visibility As Integer)
    Dim ChangePictureVisibility As PictureVisibilityDelegate
    'Dim isClosed As Boolean = False
    Dim waitPopupMsg As frmSpecialPrompt
    Private Sub btnDayClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDayClose.Click
        bw = New BackgroundWorker
        bw.WorkerSupportsCancellation = True
        btnDayClose.Enabled = False
        If Not IsTotalDataValid() Then
            btnDayClose.Cursor = Cursors.Default
            btnDayClose.Enabled = True
            Exit Sub
        End If

        If CheckIfAllTerminalAreCLosed() = False Then
            ShowMessage(getValueByKey("daycloseterminalclosemsg"), getValueByKey("CLAE04"))
            btnDayClose.Cursor = Cursors.Default
            btnDayClose.Enabled = True
            Exit Sub
        End If

        If clsDefaultConfiguration.BankTotalCheck Then
            Dim receivedAmount As Decimal = Decimal.Zero
            If (CashDetailsList IsNot Nothing) Then
                receivedAmount = CashDetailsList.Sum(Function(item) item.TotalAmt)
            End If

            If CashDetailsList IsNot Nothing AndAlso receivedAmount <> DayCloseResponse.ActualTotalCashAmt Then
                ShowMessage((getValueByKey("dayclosebankdtlamtcheck")), getValueByKey("CLAE04"))
                btnDayClose.Cursor = Cursors.Default
                btnDayClose.Enabled = True
                Exit Sub
            End If
        End If
        If clsDefaultConfiguration.AutoDBBackupOnShiftClose Then
            DataBaseAutoBackup()
        End If
        AddHandler bw.DoWork, AddressOf bw_DoWork
        AddHandler bw.RunWorkerCompleted, AddressOf bw_RunWorkerCompleted
        ChangePictureVisibility = AddressOf ChangeVisibility
        If Not bw.IsBusy = True Then
            bw.RunWorkerAsync()
        End If
        btnDayClose.Cursor = Cursors.Default
        'btnDayClose.Enabled = True

        '        Try
        '            If Not IsTotalDataValid() Then
        '                Exit Sub
        '            End If

        '            If CheckIfAllTerminalAreCLosed() = False Then
        '                ShowMessage(getValueByKey("daycloseterminalclosemsg"), getValueByKey("CLAE04"))
        '                Exit Sub
        '            End If

        '            If clsDefaultConfiguration.BankTotalCheck Then
        '                Dim receivedAmount As Decimal = Decimal.Zero
        '                If (CashDetailsList IsNot Nothing) Then
        '                    receivedAmount = CashDetailsList.Sum(Function(item) item.TotalAmt)
        '                End If

        '                If CashDetailsList IsNot Nothing AndAlso receivedAmount <> DayCloseResponse.ActualTotalCashAmt Then
        '                    ShowMessage((getValueByKey("dayclosebankdtlamtcheck")), getValueByKey("CLAE04"))
        '                    Exit Sub
        '                End If
        '            End If
        '            ''Synch on Day Close Start-Added by mayur

        '            If clsDefaultConfiguration.SyncOnDayClose Then
        '                Try
        '                    If clsDefaultConfiguration.AutoUpdateonDayClose Then
        '                        AutoUpdates()
        '                    End If


        '                    Dim objs As New clsCommon
        '                    Dim dtInfo As New DataTable
        '                    dtInfo = objs.GetRemoteIpPort()
        '                    If dtInfo IsNot Nothing Then
        '                        Try
        '                            Using client = New WebClient()
        '                                Using stream = client.OpenRead("http://www.google.com")
        '                                End Using
        '                            End Using
        '                        Catch ex As Exception
        '                            LogException(ex)
        '                            ShowMessage("Weak or No Internet Connection!", getValueByKey("CLAE04"))
        '                            Exit Sub
        '                        End Try
        '                        If dtInfo.Rows.Count > 0 Then
        '                            Dim IpAddr As String
        '                            Dim port As String
        '                            IpAddr = dtInfo.Rows(0)(1).ToString()
        '                            port = dtInfo.Rows(1)(1).ToString()

        '                            Dim client As New WebClient()
        '                            client.Headers("Content-type") = "application/json"
        '                            ' invoke the REST method
        '                            Dim data As Byte()
        '                            Try
        '                                data = client.DownloadData("http://" & IpAddr & ":" & port & "/posSeam/rest/internetlog/check")
        '                            Catch ex As Exception
        '                                ShowMessage("Server Down!", getValueByKey("CLAE04"))
        '                                LogException(ex)
        '                                Exit Sub
        '                            End Try

        '                            Dim vOut As String = System.Text.Encoding.UTF8.GetString(data)

        '                            If vOut = "SUCCESS" Then
        '                                Dim cmdCounter As Integer = 0
        '                                waitPopupMsg = ShowLicense("Day Close and Synchronization in Progress. Please wait..", getValueByKey("CLAE04"))
        '                                Application.DoEvents()

        'Check:
        '                                Dim isLocked As Boolean = False

        '                                isLocked = objs.IsDayCloseSnycProceed(clsAdmin.SiteCode)

        '                                If Not isLocked Then

        '                                    Dim Proc As New System.Diagnostics.Process
        '                                    Proc.StartInfo = New ProcessStartInfo(Application.StartupPath & "\rundayclose.bat", "syncdayclose")
        '                                    Proc.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
        '                                    Proc.Start()

        '                                    System.Threading.Thread.Sleep(1 * 60 * 1000)
        '                                    Dim LastSyncTym As DateTime = DateTime.Now
        '                                    Dim result As String = objs.GetDayCloseSyncStatusByDate("ALL", clsAdmin.SiteCode, clsAdmin.DayOpenDate, LastSyncTym)
        '                                    If result <> "1" Then
        '                                        waitPopupMsg.Close()
        '                                        ShowMessage("Problem With Synchronization!", getValueByKey("CLAE04"))
        '                                        btnDayClose.Enabled = True
        '                                        Exit Sub
        '                                    ElseIf result = "1" Then
        'CheckIndependent:
        '                                        Dim isIndependentLocked As Boolean = False
        '                                        isIndependentLocked = objs.IsDayCloseSnycProceed(clsAdmin.SiteCode)
        '                                        If Not isIndependentLocked Then


        '                                            Dim ProcI As New System.Diagnostics.Process
        '                                            ProcI.StartInfo = New ProcessStartInfo(Application.StartupPath & "\rundayclose.bat", "INDEPENDENT")
        '                                            ProcI.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
        '                                            ProcI.Start()
        '                                        Else
        '                                            isIndependentLocked = isIndependentLocked + 1
        '                                            If isIndependentLocked = 10 Then
        '                                                waitPopupMsg.Close()
        '                                                ShowMessage("Problem With Synchronization!", getValueByKey("CLAE04"))
        '                                                btnDayClose.Enabled = True
        '                                                Exit Sub
        '                                            End If
        '                                            System.Threading.Thread.Sleep(1 * 60 * 1000)
        '                                            GoTo CheckIndependent
        '                                        End If
        '                                        System.Threading.Thread.Sleep(1 * 60 * 1000)
        '                                        Dim result1 As String = objs.GetDayCloseSyncStatusByDate("INDEPENDENT", clsAdmin.SiteCode, clsAdmin.DayOpenDate, LastSyncTym)
        '                                        If result1 <> "1" Then
        '                                            waitPopupMsg.Close()
        '                                            ShowMessage("Problem With Synchronization!", getValueByKey("CLAE04"))
        '                                            btnDayClose.Enabled = True
        '                                            Exit Sub
        '                                        End If
        '                                    End If
        '                                Else
        '                                    cmdCounter = cmdCounter + 1
        '                                    If cmdCounter = 10 Then
        '                                        waitPopupMsg.Close()
        '                                        ShowMessage("Problem With Synchronization!", getValueByKey("CLAE04"))
        '                                        btnDayClose.Enabled = True
        '                                        Exit Sub
        '                                    End If
        '                                    System.Threading.Thread.Sleep(1 * 60 * 1000)
        '                                    GoTo Check
        '                                End If

        '                            Else
        '                                btnDayClose.Enabled = True
        '                                waitPopupMsg.Close()
        '                                ShowMessage("Weak or No Internet Connection!", getValueByKey("CLAE04"))
        '                                Exit Sub
        '                            End If

        '                        End If
        '                    Else
        '                        btnDayClose.Enabled = True
        '                        waitPopupMsg.Close()
        '                        ShowMessage("Weak or No Internet Connection!", getValueByKey("CLAE04"))
        '                        Exit Sub
        '                    End If
        '                Catch ex As Exception
        '                    btnDayClose.Enabled = True
        '                    waitPopupMsg.Close()
        '                    LogException(ex)
        '                    Return
        '                End Try
        '            End If

        '            ''Synch on Day Close End
        '            Dim request As New BankDetailsSaveDataRequest

        '            If (CashDetailsList IsNot Nothing) Then
        '                request.CashDenominationList = CashDetailsList.ToList()
        '            End If
        '            request.ChequeDetailsList = ChequeDetailsList
        '            request.DayCloseDate = clsAdmin.DayOpenDate
        '            request.FinYear = clsAdmin.Financialyear
        '            request.SiteCode = clsAdmin.SiteCode
        '            request.CreatedAt = clsAdmin.SiteCode
        '            request.CreatedBy = clsAdmin.UserCode
        '            request.UpdatedAt = clsAdmin.SiteCode
        '            request.UpdatedBy = clsAdmin.UserCode
        '            If clsDefaultConfiguration.ClientForMail = "Spectrum" Then
        '                request.IsPDFGenerate = True
        '            Else
        '                request.IsPDFGenerate = False
        '            End If
        '            request.IsPettyCashApplicable = clsDefaultConfiguration.IsPettyCashApplicable
        '            request.AddTotalSalesToPettyCash = clsDefaultConfiguration.AddSalesToPettyCash
        '            request.IsPettyCashOnSalesTerminal = clsDefaultConfiguration.IsPettyCashOnSameTerminal
        '            If Instance.SaveBankDetailsData(request, clsDefaultConfiguration.DayCloseReportPath) Then
        '                btnDayClose.Enabled = False
        '                btnBankReport.Enabled = True
        '                btnDayCloseSalesReport.Enabled = True
        '                dgCashDetails.ReadOnly = True
        '                If clsDefaultConfiguration.CashMemoResetonDayClose Then
        '                    Call Instance.ResetGLNoRangeObjects("CM", clsAdmin.SiteCode)
        '                End If
        '                Call Instance.SaveDayCloseArticleStockBalance(clsAdmin.DayOpenDate, request.CreatedAt, request.CreatedBy)
        '                If DayClosed IsNot Nothing Then
        '                    DayClosed()
        '                End If
        '                'If clsDefaultConfiguration.JKDayCloseReport Then
        '                '    Call GenerateDSRReport()
        '                '    Call GenerateDayCloseReport()
        '                'End If

        '                Dim Idlist As String = clsDefaultConfiguration.DayCloseEmailNotifiaction
        '                Dim obj As New clsDayClose
        '                Dim id As String = obj.GetSiteMailId(clsAdmin.SiteCode)
        '                If Not Idlist = "" Then
        '                    id = id & "," & Idlist
        '                End If

        '                If clsDefaultConfiguration.ClientForMail = "JK" Then
        '                    Call GenerateDSRReport(id)
        '                    Dim objReport As New DayCloseReportController
        '                    objReport.GenerateDayCloseReport(New DayCloseReportModel With {.SiteCode = request.SiteCode, .ToDate = request.DayCloseDate}, clsDefaultConfiguration.DayCloseReportPath)
        '                ElseIf clsDefaultConfiguration.ClientForMail = "PC" Then
        '                    Call GenerateDayCloseReport(id)
        '                Else
        '                    Dim objReport As New DayCloseReportController
        '                    objReport.GenerateDayCloseReport(New DayCloseReportModel With {.SiteCode = request.SiteCode, .ToDate = request.DayCloseDate}, clsDefaultConfiguration.DayCloseReportPath)
        '                End If


        '                If clsDefaultConfiguration.IsMailSend Then
        '                    If clsDefaultConfiguration.ClientForMail = "Spectrum" Then
        '                        path = DayCloseReportController.PathForEmail
        '                        Call SendMail(path, id)
        '                    End If
        '                End If
        '                waitPopupMsg.Close()
        '                ShowMessage(String.Format(getValueByKey("dayclosesuccessmsg"), clsAdmin.DayOpenDate.ToShortDateString()), getValueByKey("CLAE04"))

        '                DisableTransactionMainMenu(False)
        '                MDISpectrum.DayOpenMenuItem.Enabled = True

        '                RunBatchFile()
        '            Else
        '                waitPopupMsg.Close()
        '                ShowMessage((getValueByKey("daycloseerrormsg")), getValueByKey("CLAE05"))
        '                btnDayClose.Enabled = True
        '            End If

        '            'Me.ParentForm.Close()
        '        Catch ex As Exception
        '            LogException(ex)
        '        End Try
    End Sub
    Public Sub ChangeVisibility(ByVal steps As Integer)
        If steps = 0 Then
            waitPopupMsg = DayCloseSyncProgress("Day Close and Synchronization in Progress. Please wait..", getValueByKey("CLAE04"))
            btnDayClose.Enabled = False
            Application.DoEvents()
        ElseIf steps = 1 Then
            waitPopupMsg.Close()
            ShowMessage("Problem With Synchronization!", getValueByKey("CLAE04"))
            btnDayClose.Enabled = True
        ElseIf steps = 2 Then
            waitPopupMsg.Close()
            btnDayClose.Enabled = True
        ElseIf steps = 3 Then
            btnDayClose.Enabled = False
            btnBankReport.Enabled = True
            btnDayCloseSalesReport.Enabled = True
            dgCashDetails.ReadOnly = True
        ElseIf steps = 4 Then
            waitPopupMsg.Close()
            writeDaycloseLog("4 .Due to connectivity issues system is not able to perform Day Close activity..")
            ShowMessage("Due to connectivity issues system is not able to perform Day Close activity.", getValueByKey("CLAE04"))
            btnDayClose.Enabled = True
        ElseIf steps = 5 Then
            waitPopupMsg.Close()
            writeDaycloseLog("5.Day Close failed due to Synchronization issues.")
            ShowMessage(" Day Close failed due to Synchronization issues.", getValueByKey("CLAE04"))
            btnDayClose.Enabled = True
        ElseIf steps = 6 Then
            waitPopupMsg.Close()
            writeDaycloseLog("6. Day Close failed due to network issues.")
            ShowMessage(" Day Close failed due to network issues.", getValueByKey("CLAE04"))
            btnDayClose.Enabled = True
        ElseIf steps = 7 Then
            waitPopupMsg.Close()
            writeDaycloseLog("7. Day Close failed due to invalid parameters requested by system.")
            ShowMessage("Day Close failed due to invalid parameters requested by system.", getValueByKey("CLAE04"))
            btnDayClose.Enabled = True
        ElseIf steps = 8 Then
            waitPopupMsg.Close()
            writeDaycloseLog("8.Internal server error. Day Close failed.")
            ShowMessage("Internal server error. Day Close failed.", getValueByKey("CLAE04"))
            btnDayClose.Enabled = True
        ElseIf steps = 9 Then
            waitPopupMsg.Close()
            ShowMessage("Invalid Response!", getValueByKey("CLAE04"))
            writeDaycloseLog("9. Invalid Response!")
            btnDayClose.Enabled = True
        ElseIf steps = 10 Then
            waitPopupMsg.Close()
            btnDayClose.Enabled = False
        ElseIf steps = 11 Then
            waitPopupMsg.Close()
            writeDaycloseLog("")
            ShowMessage("", getValueByKey("CLAE04"))
            btnDayClose.Enabled = True
        ElseIf steps = 12 Then
            waitPopupMsg = DayCloseSyncProgress("Please wait while system is backing up the data...", getValueByKey("CLAE04"))
            Application.DoEvents()
        ElseIf steps = 13 Then
            waitPopupMsg.Close()
        End If

    End Sub
    Private Sub writeDaycloseLog(ByVal mes As String)
        Dim ax As New ApplicationException(mes)
        LogException(ax)
    End Sub

    ' Incoming data from the client.
    Public Shared Socketdata As String = Nothing
    Private Sub bw_DoWork(sender As Object, e As DoWorkEventArgs)
        Try
            If clsDefaultConfiguration.AutoDBBackupOnShiftClose Then
                writeDaycloseLog("Start db back up")
                'Me.Invoke(ChangePictureVisibility, 12)
                If DataBaseAutoBackup() = True Then
                    writeDaycloseLog("Db back up done")
                    ' Me.Invoke(ChangePictureVisibility, 13)
                    'ShowMessage("DB Backup successfull on location  " + clsDefaultConfiguration.DatabaseBackupPath, "Information")
                Else
                    writeDaycloseLog("Db Back up fail")
                    ' Me.Invoke(ChangePictureVisibility, 13)
                    'ShowMessage("DB Backup failed try again...", "Information")
                End If
            End If
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
                    ElseIf row("FldLabel").ToString() = "SYNCH_SERVER_LOCAL_PORT" Then
                        syncport = row("FldValue").ToString()
                    End If
                Next
            End If
            If clsDefaultConfiguration.SyncOnDayClose Then
                Try
                    'Dim dtInfo As New DataTable
                    'dtInfo = objs.GetRemoteIpPort()
                    If dtInfo IsNot Nothing Then
                        writeDaycloseLog("Checking google.com as internet connected")
                        Try
                            Using client = New WebClient()
                                Using stream = client.OpenRead("http://www.google.com")
                                End Using
                            End Using
                        Catch ex As Exception
                            LogException(ex)
                            ShowMessage("Weak or No Internet Connection!", getValueByKey("CLAE04"))
                            Exit Sub
                        End Try
                        writeDaycloseLog("Checking google.com as internet connected sucess")
                        If dtInfo.Rows.Count > 0 Then
                            'For Each row In dtInfo.Rows
                            '    If row("FldLabel").ToString() = "WebService.Remote.IP" Then
                            '        IpAddr = row("FldValue").ToString()
                            '    ElseIf row("FldLabel").ToString() = "WebService.Remote.PORT" Then
                            '        port = row("FldValue").ToString()
                            '    ElseIf row("FldLabel").ToString() = "SYNCH_SERVER_LOCAL_PORT" Then
                            '        syncport = row("FldValue").ToString()
                            '    End If
                            'Next

                            writeDaycloseLog("Checking CCE server as connected ")
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
                                writeDaycloseLog("Checking CCE server as connected Sucess")
                                If Not Me.IsHandleCreated Then
                                    Me.CreateControl()
                                End If
                                Me.Invoke(ChangePictureVisibility, 0)
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
                                            Shell(Application.StartupPath & "\run_dayCloseSynch.bat", AppWinStyle.Hide)
                                            writeDaycloseLog("started process run_dayCloseSynch.bat ")
                                            '// Poll the socket for reception with a 30 Mins(1800000000) timeout.
                                            If (listener.Poll(1800000000, SelectMode.SelectRead)) Then
                                                bytes = New Byte(1024) {}
                                                handler = listener.Accept()
                                                writeDaycloseLog(" Get Reply from run_dayCloseSynch.bat ")
                                            Else
                                                Me.Invoke(ChangePictureVisibility, 5)
                                                objs.UpdateDayCloseStaus(clsAdmin.SiteCode, clsAdmin.DayOpenDate, False)
                                                writeDaycloseLog(" NO Reply from run_dayCloseSynch.bat ")
                                                Exit Sub
                                            End If
                                            Socketdata = Nothing

                                            bytes = New Byte(1024) {}
                                            Dim bytesRec As Integer = handler.Receive(bytes)
                                            Socketdata += Encoding.UTF8.GetString(bytes, 0, bytesRec)
                                            If Socketdata <> "" Then
                                                If Not Socketdata.Contains("DAY_CLOSE_SYNCH_SUCCESS") Then
                                                    If Socketdata.Contains("DAY_CLOSE_SYNCH_FAILURE") Then
                                                        Me.Invoke(ChangePictureVisibility, 5)
                                                    ElseIf Socketdata.Contains("NETWORK_FAILURE") Then
                                                        Me.Invoke(ChangePictureVisibility, 6)
                                                    ElseIf Socketdata.Contains("INVALID_REQUEST_PARAM") Then
                                                        Me.Invoke(ChangePictureVisibility, 7)
                                                    ElseIf Socketdata.Contains("INTERNAL_SERVER_ERROR") Then
                                                        Me.Invoke(ChangePictureVisibility, 8)
                                                    Else
                                                        Me.Invoke(ChangePictureVisibility, 9)
                                                    End If
                                                    handler.Shutdown(SocketShutdown.Both)
                                                    handler.Close()
                                                    objs.UpdateDayCloseStaus(clsAdmin.SiteCode, clsAdmin.DayOpenDate, False)
                                                    Exit Sub
                                                Else
                                                    writeDaycloseLog(" GEt Reply  from run_dayCloseSynch.bat Reply is DAY_CLOSE_SYNCH_SUCCESS ")
                                                End If

                                            Else
                                                objs.UpdateDayCloseStaus(clsAdmin.SiteCode, clsAdmin.DayOpenDate, False)
                                                Me.Invoke(ChangePictureVisibility, 2)
                                                Exit Sub
                                            End If

                                        Catch ex As Exception
                                            writeDaycloseLog(ex.ToString())
                                            objs.UpdateDayCloseStaus(clsAdmin.SiteCode, clsAdmin.DayOpenDate, False)
                                            Me.Invoke(ChangePictureVisibility, 4)
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
                                    objs.UpdateDayCloseStaus(clsAdmin.SiteCode, clsAdmin.DayOpenDate, False)
                                    Me.Invoke(ChangePictureVisibility, 2)
                                    Exit Sub
                                End Try
                                '----- FO as Server by Mahesh - END 
                            Else
                                btnDayClose.Enabled = True
                                writeDaycloseLog("Checking CCE server as connected Failed ")
                                waitPopupMsg.Close()
                                ShowMessage("Weak or No Internet Connection!", getValueByKey("CLAE04"))
                                Exit Sub
                            End If

                        End If
                    Else
                        btnDayClose.Enabled = True
                        writeDaycloseLog("CCE Details are not  configured properly ")
                        ShowMessage("Weak or No Internet Connection!", getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                Catch ex As Exception
                    btnDayClose.Enabled = True
                    LogException(ex)
                    Return
                End Try
            End If

            ''Synch on Day Close End
            writeDaycloseLog("started Day Close ")
            Dim request As New BankDetailsSaveDataRequest

            If (CashDetailsList IsNot Nothing) Then
                request.CashDenominationList = CashDetailsList.ToList()
            End If
            request.ChequeDetailsList = ChequeDetailsList
            request.DayCloseDate = clsAdmin.DayOpenDate
            request.FinYear = clsAdmin.Financialyear
            request.SiteCode = clsAdmin.SiteCode
            request.CreatedAt = clsAdmin.SiteCode
            request.CreatedBy = clsAdmin.UserCode
            request.UpdatedAt = clsAdmin.SiteCode
            request.UpdatedBy = clsAdmin.UserCode
            request.IsPDFGenerate = False
            'day close report generated twice sabbro issue
            'If clsDefaultConfiguration.ClientForMail = "Spectrum" Then
            '    request.IsPDFGenerate = True
            'Else
            '    request.IsPDFGenerate = False
            'End If
            request.IsPettyCashApplicable = clsDefaultConfiguration.IsPettyCashApplicable
            request.AddTotalSalesToPettyCash = clsDefaultConfiguration.AddSalesToPettyCash
            request.IsPettyCashOnSalesTerminal = clsDefaultConfiguration.IsPettyCashOnSameTerminal
            If Instance.SaveBankDetailsData(request, clsDefaultConfiguration.DayCloseReportPath, clsDefaultConfiguration.UpdateStockOnDayCloseWastage) Then
                '--- Update day close status at cce if SyncOnDayClose true
                If clsDefaultConfiguration.SyncOnDayClose Then
                    Dim client As New WebClient()
                    client.Headers("Content-type") = "application/json"
                    '-- invoke the REST method
                    Dim data As Byte()
                    Try
                        data = client.DownloadData("http://" & IpAddr & ":" & port & "/posSeam/rest/dayclose/update?siteCode=" & clsAdmin.SiteCode & "&openDate=" & clsAdmin.DayOpenDate.ToString("yyyy-MM-dd"))
                    Catch ex As Exception
                        LogException(ex)
                        objs.UpdateDayCloseStaus(clsAdmin.SiteCode, clsAdmin.DayOpenDate, False)
                        Me.Invoke(ChangePictureVisibility, 5)
                        Exit Sub
                    End Try

                    Dim vOut As String = System.Text.Encoding.UTF8.GetString(data)

                    If vOut = "SUCCESS" Then
                        writeDaycloseLog("Update Day close status sucessfully at CCE ")
                    Else
                        objs.UpdateDayCloseStaus(clsAdmin.SiteCode, clsAdmin.DayOpenDate, False)
                        Me.Invoke(ChangePictureVisibility, 5)
                        Exit Sub
                    End If
                End If
                writeDaycloseLog("Day Close Done Successfully")
                If Not Me.IsHandleCreated Then
                    Me.CreateControl()
                    System.Threading.Thread.Sleep(1000)
                End If
                Me.Invoke(ChangePictureVisibility, 3)
                If clsDefaultConfiguration.CashMemoResetonDayClose Then
                    Call Instance.ResetGLNoRangeObjects("CM", clsAdmin.SiteCode)
                    Call Instance.ResetGLNoRangeObjects("External cash memo", clsAdmin.SiteCode) ' reset cash memo current no for done system.
                End If
                writeDaycloseLog("Step1 :- Call Function SaveDayCloseArticleStockBalance ")
                Call Instance.SaveDayCloseArticleStockBalance(clsAdmin.DayOpenDate, request.CreatedAt, request.CreatedBy)
                writeDaycloseLog("Step2 :- After Calling Function SaveDayCloseArticleStockBalance ")
                If DayClosed IsNot Nothing Then
                    DayClosed()
                End If

                Dim Idlist As String = clsDefaultConfiguration.DayCloseEmailNotifiaction
                Dim obj As New clsDayClose
                Dim id As String = obj.GetSiteMailId(clsAdmin.SiteCode)
                Dim id1 As String = id
                If Not Idlist = "" Then
                    id = id & "," & Idlist
                End If
                Dim IdlistSb As String = clsDefaultConfiguration.StockAnalysisReportEmails
                If Not Idlist = "" Then
                    id1 = id1 & "," & IdlistSb
                End If
                If clsDefaultConfiguration.SbarroStockApplicable Then
                    GenerateStockAnalysisReport(id1)
                End If
                If clsDefaultConfiguration.ClientForMail = "JK" Then
                    'Dim objJKDayOfReport As clsJKDayOfReport
                    'objJKDayOfReport.GetJKDayOfReport(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.DayOpenDate, "ALL")
                    Call GenerateJKDayOfReportPayout(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.DayOpenDate, "ALL")
                    Call GenerateJKProductMixReport(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.DayOpenDate, clsAdmin.UserName)
                    Call GenerateDSRReport(id)
                    Dim objReport As New DayCloseReportController
                    objReport.GenerateDayCloseReport(New DayCloseReportModel With {.SiteCode = request.SiteCode, .ToDate = request.DayCloseDate}, clsDefaultConfiguration.DayCloseReportPath)
                ElseIf clsDefaultConfiguration.ClientForMail = "PC" Then
                    writeDaycloseLog("Step3 :- Call Function GenerateDayCloseReport (" + id + ")")
                    Call GenerateDayCloseReport(id)
                    writeDaycloseLog("Step4 :- After Calling Function GenerateDayCloseReport ")
                Else
                    Dim objReport As New DayCloseReportController
                    objReport.GenerateDayCloseReport(New DayCloseReportModel With {.SiteCode = request.SiteCode, .ToDate = request.DayCloseDate}, clsDefaultConfiguration.DayCloseReportPath)
                End If


                If clsDefaultConfiguration.IsMailSend Then

                    If clsDefaultConfiguration.ClientForMail = "Spectrum" Then
                        path = DayCloseReportController.PathForEmail
                        Call SendMail(path, id)
                    End If
                End If
                writeDaycloseLog("Step5 :- Before Check flag SendDayCloseSMS ")
                If clsDefaultConfiguration.SendDayCloseSMS Then
                    writeDaycloseLog("Step6 :-   flag SendDayCloseSMS is true")
                    Dim ObjClscomm As New SpectrumBL.clsCommon
                    writeDaycloseLog("Step7 :-   Call Function GetSmsPCData ")
                    Dim msgstr = ObjClscomm.GetSmsPCData(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsDefaultConfiguration.DayCloseSMSFormat)
                    writeDaycloseLog("Step8 :-   After Call Function GetSmsPCData ")
                    writeDaycloseLog("Step9 :-  Check msgstr Is Empty or Not")
                    If Not msgstr = String.Empty Then
                        writeDaycloseLog("Step10 :-   msgstr Is Not Empty Sting is = " + msgstr)
                        '    Dim urlString = clsDefaultConfiguration.SMSUrl
                        '    Dim urlContent = clsDefaultConfiguration.SMSUrlParameters
                        '    Dim url = urlString & urlContent
                        '    SendSMSForPC(url, clsDefaultConfiguration.DayCloseRecipients, msgstr)
                        writeDaycloseLog("Step10 :- check dtInfo .count")
                        If dtInfo.Rows.Count > 0 Then
                            writeDaycloseLog("Step11 :-  dtInfo .count is > 0 ")
                            Dim client As New WebClient()
                            'client.Headers("Content-type") = "application/json"
                            '-- invoke the REST method
                            Dim data As Byte()
                            Try
                                Using stream = client.OpenRead("http://www.google.com")
                                End Using
                                writeDaycloseLog("message web servive calling")
                                data = client.DownloadData("http://" & IpAddr & ":" & port & "/posSeam/rest/sendProcess/sendSMS?number=" & clsDefaultConfiguration.DayCloseRecipients & "&message=" & msgstr)
                                writeDaycloseLog("message web servive call successful")
                            Catch ex As Exception
                                LogException(ex)
                                writeDaycloseLog("message web servive call unsuccessful or no internet connection")
                            End Try
                        End If
                    Else
                        ShowMessage("Due to technical issues, SMS could not be sent. Please contact system administrator.", getValueByKey("CLAE04"))
                    End If

                End If
                writeDaycloseLog("Step12 :- END Of :- If clsDefaultConfiguration.SendDayCloseSMS = true ")
                If clsDefaultConfiguration.ClientForMail = "PC" Then
                    '----On day close if this procedure return the result we need to send a mail to our support team .
                    Dim ObjCls As New SpectrumBL.clsCommon
                    Dim dtDifference = ObjCls.GetPCAmountGoingToBankDifferenceData(clsAdmin.SiteCode, clsAdmin.DayOpenDate)
                    Dim emailid As String = clsDefaultConfiguration.DAYCLOSEEMAILAMOUNTGOINGTOBANK
                    If dtDifference IsNot Nothing Then
                        If dtDifference.Rows.Count > 0 And emailid = "" Then
                            Call SendMailForBankDifference(emailid)
                        End If
                    End If
                End If
                If clsDefaultConfiguration.SyncOnDayClose Then
                    If Not Me.IsHandleCreated Then
                        Me.CreateControl()
                    End If
                    Me.Invoke(ChangePictureVisibility, 10)
                End If

                ShowMessage(String.Format(getValueByKey("dayclosesuccessmsg"), clsAdmin.DayOpenDate.ToShortDateString()), getValueByKey("CLAE04"))
                'Me.Invoke(ChangePictureVisibility, 9)
                DisableTransactionMainMenu(False)
                MDISpectrum.DayOpenMenuItem.Enabled = True

                RunBatchFile()
            Else
                If clsDefaultConfiguration.SyncOnDayClose Then
                    If Not Me.IsHandleCreated Then
                        Me.CreateControl()
                    End If
                    Me.Invoke(ChangePictureVisibility, 2)
                    'btnDayClose.Enabled = True
                End If
                ShowMessage((getValueByKey("daycloseerrormsg")), getValueByKey("CLAE05"))

            End If
            ' added by Khusrao Adil
            ' Jk requirement
            If clsDefaultConfiguration.ClientForMail = "JK" Then
                Try
                    Process.Start(path)
                    Process.Start(JKDayOfReporthPath)
                    Process.Start(JKProductMixReportPath)
                Catch ex As Exception
                    LogException(ex)
                End Try
                Application.Exit()
            End If
            writeDaycloseLog("Step13 :- END Function ")
            'Me.ParentForm.Close()
        Catch ex As Exception
            LogException(ex)
        Finally
            Try
                bw.CancelAsync()
                If bw.CancellationPending = True Then
                    e.Cancel = True
                End If
                System.Threading.Thread.Sleep(20000)
            Catch ex As Exception
                LogException(ex)
            End Try
        End Try

    End Sub

    Private Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)

        'Me.Invoke(ChangePictureVisibility, False)
    End Sub
    Private Shared Function customCertValidation(ByVal sender As Object, _
                                              ByVal cert As X509Certificate, _
                                              ByVal chain As X509Chain, _
                                              ByVal errors As SslPolicyErrors) As Boolean
        Return True
    End Function
    Public Sub DoStuff()
        'CALL THIS BEFORE ANY HTTPS CALLS THAT WILL FAIL WITH CERT ERROR
        ServicePointManager.ServerCertificateValidationCallback = _
            New System.Net.Security.RemoteCertificateValidationCallback(AddressOf customCertValidation)
        '
        '
        '
    End Sub
    Private Function SendMail(ByVal Path As String, ByVal Idlist As String)
        For index = 0 To 4
            Try
                DoStuff()
                Dim readerPath As New StreamReader(Path)
                Dim readerJKDayOf As StreamReader
                Dim readerJKProductMix As StreamReader
                If clsDefaultConfiguration.ClientForMail = "JK" Then
                    readerJKDayOf = New StreamReader(JKDayOfReporthPath)
                    readerJKProductMix = New StreamReader(JKProductMixReportPath)
                End If
                Using reasder As New StreamReader(Path)
                    Dim NetworkCred As New NetworkCredential()
                    Dim dt As New DataTable
                    Dim host, port As String
                    Dim objClsDayClose As New clsDayClose
                    dt = objClsDayClose.GetUsernamePassword()
                    For Each row In dt.Rows
                        If row("FldLabel").ToString() = "SMTP.Password" Then
                            NetworkCred.Password = row("FldValue").ToString()
                        ElseIf row("FldLabel").ToString() = "SMTP.UserName" Then
                            NetworkCred.UserName = row("FldValue").ToString()
                        ElseIf row("FldLabel").ToString() = "SMTP.HOST" Then
                            host = row("FldValue").ToString()
                        ElseIf row("FldLabel").ToString() = "SMTP.IP" Then
                            port = row("FldValue").ToString()

                        End If
                    Next

                    Dim mm As New MailMessage(NetworkCred.UserName, Idlist)
                    Dim objclscomn As New clsCommon
                    Dim sitename As String
                    sitename = objclscomn.GetSiteName(clsAdmin.SiteCode)
                    mm.Subject = "Day Close Report of " & clsAdmin.DayOpenDate.ToString("dd-MMM-yyyy") & " for " & sitename & ":"
                    Dim msg As New StringBuilder
                    Dim str As String
                    str = "Dear user,"
                    msg.Append(str + vbCrLf)
                    Dim str1 As String
                    str1 = "Please find the attached day close report of " & clsAdmin.DayOpenDate.ToString("dd-MMM-yyyy") & " for store " & sitename & "."

                    msg.Append(str1 + vbCrLf)
                    Dim str2 As String
                    str2 = "Regards,"
                    msg.Append(str2 + vbCrLf)
                    mm.Body = msg.ToString()
                    'mm.Attachments.Add(New Attachment(New MemoryStream(bytes), "D:\DayCloseReports\BankReport_Vertak Nagar_02-03-2015.pdf"))
                    mm.Attachments.Add(New Attachment(readerPath.BaseStream, Path))
                    If clsDefaultConfiguration.ClientForMail = "JK" Then
                        If JKDayOfReporthPath <> "" Then
                            mm.Attachments.Add(New Attachment(readerJKDayOf.BaseStream, JKDayOfReporthPath))
                        End If
                        If JKProductMixReportPath <> "" Then
                            mm.Attachments.Add(New Attachment(readerJKProductMix.BaseStream, JKProductMixReportPath))
                        End If
                    End If
                    mm.IsBodyHtml = False
                    Dim smtp As New SmtpClient()

                    smtp.Host = host
                    'smtp.Host = "smtp.gmail.com"
                    smtp.EnableSsl = True
                    smtp.UseDefaultCredentials = True
                    smtp.Credentials = NetworkCred
                    smtp.Port = port
                    ' smtp.Port = 587
                    smtp.Send(mm)
                    Exit For
                End Using
            Catch ex As Exception
                LogException(ex)
            End Try
        Next

    End Function
    Private Function SendMailForBankDifference(ByVal Idlist As String)
        Try


            ' Using reader As New StreamReader(Path)
            Dim NetworkCred As New NetworkCredential()
            Dim dt As New DataTable
            Dim host, port As String
            Dim objClsDayClose As New clsDayClose
            dt = objClsDayClose.GetUsernamePassword()
            For Each row In dt.Rows
                If row("FldLabel").ToString() = "SMTP.Password" Then
                    NetworkCred.Password = row("FldValue").ToString()
                ElseIf row("FldLabel").ToString() = "SMTP.UserName" Then
                    NetworkCred.UserName = row("FldValue").ToString()
                ElseIf row("FldLabel").ToString() = "SMTP.HOST" Then
                    host = row("FldValue").ToString()
                ElseIf row("FldLabel").ToString() = "SMTP.IP" Then
                    port = row("FldValue").ToString()

                End If
            Next

            Dim mm As New MailMessage(NetworkCred.UserName, Idlist)
            Dim objclscomn As New clsCommon
            Dim sitename As String
            sitename = objclscomn.GetSiteName(clsAdmin.SiteCode)
            mm.Subject = "Amount Bank Difference"
            Dim msg As New StringBuilder
            Dim str As String
            str = "Dear user,There is Difference in AmountGoingToBank at  " & clsAdmin.DayOpenDate.ToString("dd-MMM-yyyy") & " for " & sitename
            msg.Append(str + vbCrLf)

            mm.Body = msg.ToString()
            'mm.Attachments.Add(New Attachment(New MemoryStream(bytes), "D:\DayCloseReports\BankReport_Vertak Nagar_02-03-2015.pdf"))
            'mm.Attachments.Add(New Attachment(reader.BaseStream, path))
            mm.IsBodyHtml = False
            Dim smtp As New SmtpClient()

            smtp.Host = host
            'smtp.Host = "smtp.gmail.com"
            smtp.EnableSsl = True
            smtp.UseDefaultCredentials = True
            smtp.Credentials = NetworkCred
            smtp.Port = port
            ' smtp.Port = 587
            smtp.Send(mm)
            ' End Using
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Function GenerateDSRReport(ByVal id As String) As Boolean
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
            reportViewer2.Refresh()
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Excel")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            path = clsDefaultConfiguration.DayCloseReportPath & "\DSR_" & DateTime.Now.ToString("dd-MM-yyyy-hhmmss") & ".xls"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            If clsDefaultConfiguration.IsMailSend Then
                Call SendMail(path, id)
            End If
            'Process.Start(path)
            'Process.Start(JKDayOfReporthPath)
            'Process.Start(JKProductMixReportPath)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'GenerateJKDayOfReport
    Private Function GenerateJKDayOfReportPayout(ByVal SiteCode As String, ByVal FromDate As Date, ByVal ToDate As Date, ByVal PromotionId As String)
        Try

            Dim dsJkDayOfReport As DataSet
            Dim clsObj As New clsJKDayOfReportPayout
            Dim objcls As New clsCommon
            Dim payout = objcls.GetPayoutValue(clsAdmin.SiteCode)
            Dim reason As String = "ALL"
            dsJkDayOfReport = clsObj.GetJKDayOfReportPayout(SiteCode, FromDate, ToDate, PromotionId, payout, reason)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\JKDayOfReportPayout.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            Dim FromDateParam As New ReportParameter("FROM_DATE", FromDate)
            Dim ToDateParam As New ReportParameter("TO_DATE", ToDate)
            Dim SiteCodeParam As New ReportParameter("sitecode", SiteCode)
            Dim PromotionsIdParam As New ReportParameter("OFFER_NO", PromotionId)
            Dim payoutvalueParam As New ReportParameter("VALUE", payout)
            Dim reasonParam As New ReportParameter("REASON", reason)

            reportViewer2.LocalReport.SetParameters(New ReportParameter() {SiteCodeParam, FromDateParam, ToDateParam, PromotionsIdParam, payoutvalueParam, reasonParam})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource As New ReportDataSource("DS_Offer", dsJkDayOfReport.Tables(0))
            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.Refresh()
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Excel")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            JKDayOfReporthPath = clsDefaultConfiguration.DayCloseReportPath & "\JKDayOfReportPayout_" & DateTime.Now.ToString("dd-MM-yyyy-hhmmss") & ".xls"
            Using fs As FileStream = File.Create(JKDayOfReporthPath)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'GenerateJKProductMixReport
    Private Function GenerateJKProductMixReport(ByVal SiteCode As String, ByVal FromDate As Date, ByVal ToDate As Date, ByVal LoginUser As String)
        Try
            Dim dsProductMixReport As DataSet
            Dim clsObj As New clsJKProductMixReport
            dsProductMixReport = clsObj.JKProductMixReport(SiteCode, FromDate, ToDate)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\JKProductMixReport.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            Dim SiteCodeParam As New ReportParameter("SiteCode", SiteCode)
            Dim FromDateParam As New ReportParameter("FromDate", FromDate)
            'Dim ToDateParam As New ReportParameter("ToDate", request.ToDate)_
            Dim ToDateParam As New ReportParameter("ToDate", ToDate)
            Dim LoginUserNameParam As New ReportParameter("LoginUser", LoginUser)

            reportViewer2.LocalReport.SetParameters(New ReportParameter() {SiteCodeParam, FromDateParam, ToDateParam, LoginUserNameParam})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource1 As New ReportDataSource("dsJKProductMixReportHeader", dsProductMixReport.Tables(0))
            Dim DataSource2 As New ReportDataSource("dsJKProductMixReportData", dsProductMixReport.Tables(1))
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.LocalReport.DataSources.Add(DataSource2)
            reportViewer2.Refresh()
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Excel")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            JKProductMixReportPath = clsDefaultConfiguration.DayCloseReportPath & "\JKProductMixReport_" & DateTime.Now.ToString("dd-MM-yyyy-hhmmss") & ".xls"
            Using fs As FileStream = File.Create(JKProductMixReportPath)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Function GenerateStockAnalysisReport(ByVal id As String) As Boolean
        Try

            Dim dsSTA As New DataSet()
            Dim clsObjs As New DayCloseNewReport
            dsSTA = clsObjs.GetStockAnalysisReportData(clsAdmin.SiteCode, clsAdmin.DayOpenDate)
            Dim reportViewer1 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\StockAnalysis.rdl")
            reportViewer1.LocalReport.ReportPath = appPath

            Dim dateNameParam As New ReportParameter("dayclosedate", clsAdmin.DayOpenDate)
            Dim SiteCodeParam As New ReportParameter("sitecode", clsAdmin.SiteCode)

            reportViewer1.LocalReport.SetParameters(New ReportParameter() {dateNameParam, SiteCodeParam})
            reportViewer1.ProcessingMode = ProcessingMode.Local
            reportViewer1.LocalReport.DataSources.Clear()
            Dim DataSource As New ReportDataSource("DsStockAnalysisReportHeader", dsSTA.Tables(0))
            Dim DataSource1 As New ReportDataSource("dsStockAnalysis", dsSTA.Tables(1))

            reportViewer1.LocalReport.DataSources.Add(DataSource)
            reportViewer1.LocalReport.DataSources.Add(DataSource1)
            reportViewer1.Refresh()

            Dim mybytes As [Byte]() = reportViewer1.LocalReport.Render("pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            path = clsDefaultConfiguration.DayCloseReportPath & "\STA_" & DateTime.Now.ToString("dd-MM-yyyy-hhmmss") & ".pdf"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            If clsDefaultConfiguration.IsMailSend Then
                Call SendMail(path, id)
            End If
            Process.Start(path)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Function GenerateDayCloseReport(ByVal id As String) As Boolean
        Try
            writeDaycloseLog("Step 3A :- In function GenerateDayCloseReport")
            Dim dsDsr As New DataSet()
            Dim clsObj As New DayCloseNewReport
            dsDsr = clsObj.GetNewDayCloseReportData(clsAdmin.SiteCode, clsAdmin.DayOpenDate)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\dayclose.rdl")
            reportViewer2.LocalReport.ReportPath = appPath


            Dim SiteCodeParam As New ReportParameter("V_SiteCode", clsAdmin.SiteCode)
            Dim dateNameParam As New ReportParameter("V_DayCloseDate", clsAdmin.DayOpenDate)
            Dim SaleTypeParm As New ReportParameter("V_SaleType", clsAdmin.SiteCode)

            reportViewer2.LocalReport.SetParameters(New ReportParameter() {SiteCodeParam, dateNameParam, SaleTypeParm})
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

            reportViewer2.LocalReport.DataSources.Add(DataSource19)
            reportViewer2.LocalReport.DataSources.Add(DataSource20)
            reportViewer2.LocalReport.DataSources.Add(DataSource21)
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            path = clsDefaultConfiguration.DayCloseReportPath & "\" & dsDsr.Tables(clsObj.DayCloseDatasetNames.ReportHeaderData).Rows(0)("Site") & "_DayCloseReport_" & clsAdmin.DayOpenDate.ToString("dd-MM-yyyy") & ".pdf"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            If clsDefaultConfiguration.IsMailSend Then
                writeDaycloseLog("Step 3B :- Call function SendMail")
                Call SendMail(path, id)
                writeDaycloseLog("Step 3C :- Mail send Successfully")
            End If
            Process.Start(path)
            writeDaycloseLog("Step 3D :- Day Close Report pdf file Display Successfully")
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Sub dgCashDetails_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgCashDetails.CellBeginEdit
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub

    Private Sub DataErrorEvent(ByVal sender As System.Object, ByVal e As DataGridViewDataErrorEventArgs) Handles dgCashDetails.DataError

    End Sub

    Private Sub btnBankReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBankReport.Click
        Try
            GenerateRpeort(POSReports.BankReport)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnDayCloseSalesReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDayCloseSalesReport.Click
        Try
            GenerateRpeort(POSReports.KOTReport)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub GenerateRpeort(ByVal reportName As POSReports)
        Try
            Dim objReport As IReports = ReportFactory.Instance.GetReportInstance(reportName.ToString())
            Dim request As New DayCloseReportModel
            request.ToDate = clsAdmin.DayOpenDate
            request.FromDate = clsAdmin.DayOpenDate
            request.SiteCode = clsAdmin.SiteCode
            request.CreatedBy = clsAdmin.UserName
            request.CreatedOn = DateTime.Now
            objReport.GenerateDayCloseReport(request, clsDefaultConfiguration.DayCloseReportPath)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub RunBatchFile()
        If System.IO.File.Exists(clsDefaultConfiguration.AutoRunPathAfterDayClose) Then
            Process.Start(clsDefaultConfiguration.AutoRunPathAfterDayClose)
        End If
    End Sub
    Private Sub CtrlDayCloseBankDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "day-close.htm")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function Themechange()
        Me.BackColor = Color.FromArgb(134, 134, 134)
        TableLayoutPanel1.BackColor = Color.FromArgb(134, 134, 134)
        TableLayoutPanel2.BackColor = Color.FromArgb(134, 134, 134)
        Panel4.BackColor = Color.FromArgb(134, 134, 134)
        Label5.BackColor = Color.FromArgb(212, 212, 212)
        Label1.BackColor = Color.FromArgb(212, 212, 212)
        Label4.BackColor = Color.FromArgb(212, 212, 212)
        Label2.BackColor = Color.FromArgb(212, 212, 212)

        GroupBox1.BackColor = Color.White
        lblActualTotal.BackColor = Color.White
        lblAmtGoingToBank.BackColor = Color.White
        lblTotalChequeAmt.BackColor = Color.White
        lblTotalQuantity.BackColor = Color.White
        dgCashDetails.BackgroundColor = Color.White
        dgCashDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(177, 227, 253)
        dgCashDetails.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgCashDetails.DefaultCellStyle.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgCashDetails.DefaultCellStyle.SelectionBackColor = Color.LightBlue

        dgChequeDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(177, 227, 253)
        dgChequeDetails.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgChequeDetails.BackgroundColor = Color.White
        dgChequeDetails.DefaultCellStyle.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgChequeDetails.DefaultCellStyle.SelectionBackColor = Color.LightBlue



        btnBankReport.BackColor = Color.FromArgb(0, 107, 163)
        btnBankReport.ForeColor = Color.FromArgb(255, 255, 255)
        ' btnBankReport.Size = New Size(71, 30)
        btnBankReport.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnBankReport.FlatStyle = FlatStyle.Flat
        btnBankReport.FlatAppearance.BorderSize = 0
        btnBankReport.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)



        btnDayClose.BackColor = Color.FromArgb(0, 107, 163)
        btnDayClose.ForeColor = Color.FromArgb(255, 255, 255)
        ' btnDayClose.Size = New Size(71, 30)
        btnDayClose.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnDayClose.FlatStyle = FlatStyle.Flat
        btnDayClose.FlatAppearance.BorderSize = 0
        btnDayClose.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)



        btnDayCloseSalesReport.BackColor = Color.FromArgb(0, 107, 163)
        btnDayCloseSalesReport.ForeColor = Color.FromArgb(255, 255, 255)
        btnDayCloseSalesReport.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnDayCloseSalesReport.FlatStyle = FlatStyle.Flat
        btnDayCloseSalesReport.FlatAppearance.BorderSize = 0
        btnDayCloseSalesReport.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)


    End Function

    Private Sub dgChequeDetails_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs)
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    'Private Sub Label3_Click(sender As Object, e As EventArgs) Handles LblImprestCashAmt.Click

    'End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) Handles Panel4.Paint

    End Sub
End Class