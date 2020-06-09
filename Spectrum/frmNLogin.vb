﻿Imports System.Data.SqlClient
Imports SpectrumBL
Imports System.Globalization
Imports SpectrumPrint
Imports Spectrum.SpectrumUpdate
Imports System.IO
Imports System.IO.Compression
Imports Ionic.Zip
Imports SpectrumCommon
Imports System.Collections
Imports System.ComponentModel
Imports System.Linq
Imports Microsoft.Reporting.WinForms
Imports System.Net.Mail
Imports System.Net
Imports System.Text
Imports Spectrum.BO
Imports Spectrum.BL
Public Class frmNLogin
    Dim dtLang As DataTable
    Dim imgpath As String

    Public bSwitchUser As Boolean = False
    Private _IsNetWorkConnected As Boolean
    Private _SiteCodeForDefaultConfig As String
    Private directoryPath As String = Application.StartupPath
    Public Property IsNetWorkConnected() As Boolean
        Get
            Return _IsNetWorkConnected
        End Get
        Set(ByVal value As Boolean)
            _IsNetWorkConnected = value
            'Dim ob As C1.Win.C1Ribbon.RibbonLabel
            'ob = C1StatusBar1.LeftPaneItems("lblUserId")
            If value = True Then
                MDISpectrum.rbnLStatus.SmallImage = Spectrum.My.Resources.connected1.ToBitmap

                MDISpectrum.rbnLStatus.Text = Spectrum.My.Resources.ConnectedText

            Else
                'rbnLStatus.Text = "Disconnected"
                MDISpectrum.rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus1")
            End If
        End Set
    End Property
    Private _IpAddress As String
    Public Property IpAddress() As String
        Get
            Return _IpAddress
        End Get
        Set(ByVal value As String)
            _IpAddress = value
        End Set
    End Property
    Private _IsHOInstance As Boolean
    Private Property IsHOInstance As Boolean
        Get
            Return _IsHOInstance
        End Get
        Set(ByVal value As Boolean)
            _IsHOInstance = value
        End Set
    End Property

    '' added by nikhil for Logout Screen changes 
    Public _IsLogoutFromBillingScreen As Boolean = False
    Public Property IsLogoutFromBillingScreen As Boolean
        Get
            Return _IsLogoutFromBillingScreen
        End Get
        Set(ByVal value As Boolean)
            _IsLogoutFromBillingScreen = value
        End Set
    End Property

    Public _BillingScreenName As String = ""
    Public Property BillingScreenName() As String
        Get
            Return _BillingScreenName
        End Get
        Set(ByVal value As String)
            _BillingScreenName = value
        End Set
    End Property

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Try
            If Directory.Exists(directoryPath & "\Update") Then
                'Directory.Delete(directoryPath)
                DeleteDirectory(directoryPath & "\Update")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
        If clsDefaultConfiguration.AutoUpdateOnLogin Then
            ctrlBtnAutoUpdate.Enabled = False
            AutoUpdateLogin()
        End If
        If IsDBNull(cboLanguage.SelectedValue) Or cboLanguage.SelectedValue Is Nothing Then
            clsAdmin.CultureInfo = "en-US"
            clsAdmin.LangCode = "EN"
            SetCurrentculture(clsAdmin.CultureInfo)
        Else
            clsAdmin.CultureInfo = cboLanguage.SelectedValue
            clsAdmin.LangCode = cboLanguage.Columns("LanguageCode").Value
            SetCurrentculture(clsAdmin.CultureInfo)
        End If
        ''NEW START''
        Dim objAccess As New clsCommon


        'If objAccess.IsTransactionAccess(clsAdmin.SiteCode) = False Then
        '    ShowMessage("Login failed. Please contact System Administrator.", getValueByKey("CLAE04"))
        '    Return
        'End If
        ''NEW END''
        If LoginAuth() = True Then
            Dim objTill As New clsTill
            Dim TillStatus As Boolean = objTill.CheckOpening(clsAdmin.TerminalID, clsAdmin.SiteCode, "Open")
            clsDefaultConfiguration.TillOpenDone = TillStatus
            '----''-- Added By Mahesh for set whether Till have Cash Drawer Options or Not .....
            clsAdmin.IsCashDrawer = objTill.CheckIsCashDrawer(clsAdmin.TerminalID, clsAdmin.SiteCode)
            MDISpectrum.LogoutToolStripMenuItem.Visible = True
            '------ Code Ended By Mahesh .....

            MDISpectrum.lblLoggedIn.Text = clsAdmin.UserName

            MDISpectrum.lblLoggedIn.Text = clsAdmin.UserName
            'MDISpectrum.lblTodayDate.Text = FormatDateTime(Now, DateFormat.ShortDate)
            MDISpectrum.lblTodayDate.Text = clsAdmin.DayOpenDate.ToShortDateString()
            'lblTerminalId.Text = "Terminal Id"
            MDISpectrum.lblTerminalId.Text = getValueByKey("ctrlrbnbaseform.lblterminalid") & " " & clsAdmin.TerminalID
            'MDISpectrum.lblVersion.Text = "Ver(1.0.0.5)"
            clsDefaultConfiguration.ISAllowSOBooking = clsDefaultConfiguration.SOBookingScreenTills.Contains(clsAdmin.TerminalID)
            MDISpectrum.lblVersion.Text = String.Format("Ver({0})", My.Application.Info.Version.ToString())
            MDISpectrum.CheckAllTransactionRights(IsHOInstance)
            IsNetWorkConnected = OnlineConnect
            'rbnLStatus.Text = IIf(OnlineConnect = True, "Online", "Offline")
            Spectrum.Models.CommonModel.ConnectionString = ReadSpectrumParamFile("ServerConnectionString")
            Spectrum.Models.CommonModel.SiteCode = clsAdmin.SiteCode
            Spectrum.Models.CommonModel.UserID = clsAdmin.UserName
            Spectrum.Models.CommonModel.CurrentDate = DateTime.Now
            '----- Commented BY Mahesh for allow user/Devlopers to Work locally ...
            Spectrum.BO.CommonFunc.SpectrumResources = Spectrum.BO.CommonFunc.GetResourceManager()
            ' Spectrum.Models.Mappers.EntityToModelMappings.ConfigureMappers()
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                'code is added by irfan on 29/9/2017 for background image
                If clsDefaultConfiguration.IsTablet Then
                    MDISpectrum.GrpHome.BackgroundImage = My.Resources.TabBackgroundImage
                    '  MDISpectrum.GrpHome.BackgroundImage = My.Resources.TabBackgroundImage
                    Dim imagepath As String
                    imagepath = clsFiscalprinter.ReadSpectrumParamFile("MDIBGImagePath")
                    If Not Directory.Exists(imagepath) Then
                        If Not (String.IsNullOrEmpty(imagepath)) Then
                            If File.Exists(imagepath) Then
                                MDISpectrum.GrpHome.BackgroundImage = System.Drawing.Bitmap.FromFile(imagepath)
                            Else
                                MDISpectrum.GrpHome.BackgroundImage = My.Resources.LoadingScreen
                            End If

                        Else
                            'MDISpectrum.GrpHome.BackgroundImage = My.Resources.TabBackgroundImage
                            MDISpectrum.GrpHome.BackgroundImage = My.Resources.LoadingScreen
                        End If
                    Else
                        MDISpectrum.GrpHome.BackgroundImage = My.Resources.LoadingScreen
                    End If
                Else
                    ' MDISpectrum.GrpHome.BackgroundImage = My.Resources.LoadingScreen
                    Me.BackgroundImage = Nothing 'System.Drawing.Bitmap.FromFile("C:\MyFolder\mainbg.png")
                    Dim imagepath As String                                 '= "C:\MyFolder\mainbg.png"
                    'imagepath = clsFiscalprinter.ReadSpectrumParamFile("MDIBGImagePath")

                    'MDISpectrum.GrpHome.BackgroundImage = System.Drawing.Bitmap.FromFile(imagepath)
                    imagepath = clsFiscalprinter.ReadSpectrumParamFile("MDIBGImagePath")
                    If Not Directory.Exists(imagepath) Then
                        If Not (String.IsNullOrEmpty(imagepath)) Then
                            If File.Exists(imagepath) Then
                                MDISpectrum.GrpHome.BackgroundImage = System.Drawing.Bitmap.FromFile(imagepath)
                            Else
                                MDISpectrum.GrpHome.BackgroundImage = My.Resources.LoadingScreen
                            End If
                        Else
                            MDISpectrum.GrpHome.BackgroundImage = My.Resources.LoadingScreen
                        End If
                    Else
                        MDISpectrum.GrpHome.BackgroundImage = My.Resources.LoadingScreen
                    End If
                End If
            End If
            Dim objDefault As New clsDefaultConfiguration("CMS")
            Dim objIntegration As New clsDefaultConfiguration("Integration")
            'Integration
            objDefault.GetDefaultSettings()
            objIntegration.GetDefaultSettings()
            If clsDefaultConfiguration.ExtendScreen Then
                If clsDefaultConfiguration.HariOmExtendScreen = True Then
                    '        Dim myScreen As System.Windows.Forms.Screen
                    '        myScreen = Screen.FromControl(Me)

                    '        'Form2 ff = new Form2();
                    '        If Screen.AllScreens.Length > 1 Then
                    '            Dim otherScreen = Screen.AllScreens(1)
                    '            ' ff.StartPosition = FormStartPosition.Manual;
                    '            'objextend.Left = otherScreen.WorkingArea.Left + 200
                    '            'objextend.Top = otherScreen.WorkingArea.Top + 120
                    '            objextendDisplay.Left = otherScreen.WorkingArea.Left + 1
                    '            objextendDisplay.Top = otherScreen.WorkingArea.Top + 1

                    '            objextendDisplay.Size = New System.Drawing.Size((My.Computer.Screen.WorkingArea.Width + otherScreen.WorkingArea.Width - 10), (My.Computer.Screen.WorkingArea.Height + otherScreen.WorkingArea.Height - 50))
                    '            objextendDisplay.Show()
                    '        End If
                    Dim custStr1 = Strings.Mid("       ", 1, 7) + Strings.Mid(clsDefaultConfiguration.ClientName, 1, 8) + Strings.Space(20 - Strings.Len(Strings.Mid(clsDefaultConfiguration.ClientName, 1, 8)) - Strings.Len(Strings.Mid("       ", 1, 7))) + Strings.Mid("", 1, 3)
                    Dim custStr2 = Strings.Mid("", 1, 7) + Strings.Space(20 - Strings.Len(Strings.Mid("", 1, 7)) - Strings.Len(Strings.Mid("", 1, 7))) + Strings.Mid("", 1, 7)
                    clsTwoLineDisplay.ClearDisplay20x2Line(clsDefaultConfiguration.SerialPort)
                    clsTwoLineDisplay.ClientNameDisplay20x2Line(custStr1, custStr2, clsDefaultConfiguration.SerialPort)
                End If
            End If
            If IsLogoutFromBillingScreen = True Then      '' added by nikhil for Natural to show the latest screen while logout  the form
                If BillingScreenName = "frmTouchCashMemo" Then
                    Dim ChildForm = New Spectrum.frmTouchCashMemo
                    If ChildForm.Name <> String.Empty Then
                        BillingScreenName = String.Empty
                        MDISpectrum.ShowChildForm(ChildForm, True)
                    End If

                End If
            Else
                MDISpectrum.GrpHome.Focus()
                MDISpectrum.GrpHome.Select()
            End If
        Else
            ClearTextBoxes()
        End If

            'gCI = New CultureInfo(gActiveLangId.ToString)
            'Try
            '    gResourceMgr = Resources.ResourceManager.CreateFileBasedResourceManager("resource", cResourcePath & "\MyResource", Nothing)
            'Catch ex As Exception
            '    MsgBox(ex.Message & vbLf & " check the Resource file")
            'End Try 
    End Sub


    Public Function LoginAuth() As Boolean
        Try
            Dim objLoginData As New clsLogin
            Dim IpAddress = objLoginData.getIPAddress()
            ''get Local IP Address
            'Dim xEntry As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName)
            'Dim ipAddr As Net.IPAddress() = xEntry.AddressList
            'Dim IpAddress As String = ipAddr(0).ToString()
            DataBaseConnection.resourceMgrBL = Spectrum.resourceMgr

            lblErrorTxtUsername.Text = ""
            Dim strErrorMsg As String = ""
            Dim strPassword As String = ""
            Dim dbpassword As String = ""
            Dim isIdCard As Boolean = False

            If txtIDcard.Text <> String.Empty Or txtusername.Text <> String.Empty Or pwduserpassword.Text <> String.Empty Then
                Dim user As String = ""
                If objLoginData.ValidateID(IdNum:=txtIDcard.Text.Trim, strUserId:=txtusername.Text.Trim, pwd:=pwduserpassword.Text.Trim, ErrorMsg:=strErrorMsg, userid:=user) = True Then
                    txtusername.Text = user
                    isIdCard = True

                ElseIf txtIDcard.Text <> String.Empty Then 'Added by Rohit for CR 6001
                    If strErrorMsg = String.Empty Then
                        ShowMessage(getValueByKey("LG016"), "LG016 - " & getValueByKey("CLAE04"))
                        Return False
                    ElseIf strErrorMsg <> String.Empty Then
                        ShowMessage(strErrorMsg, getValueByKey("CLAE04"))
                        Return False
                    End If
                ElseIf strErrorMsg <> String.Empty Then 'Added by Rohit for CR 6001
                    ShowMessage(strErrorMsg, getValueByKey("CLAE04"))
                    Return False
                End If
            End If
            If (CheckNullTextBox(txtusername.Text)) Then
                ' MsgBox(getValueByKey("LG001"), MsgBoxStyle.Critical, "LG001 - " & getValueByKey("CLAE04"))
                ShowMessage(getValueByKey("LG001"), getValueByKey("CLAE04"))
                'MsgBox("User Name Cannot Be Blank", MsgBoxStyle.Critical, "Login")
                lblError.Text = strErrorMsg
                Return False
            Else
                strErrorMsg = ""
                lblError.Text = ""
            End If

            If (CheckNullTextBox(pwduserpassword.Text) AndAlso Not isIdCard) Then
                'MsgBox(getValueByKey("LG002"), MsgBoxStyle.Critical, "Login:LG002")
                ShowMessage(getValueByKey("LG002"), getValueByKey("CLAE04"))
                'MsgBox("Password Cannot Be Blank", MsgBoxStyle.Critical, "Login")
                lblError.Text = strErrorMsg
                Exit Function
            Else
                strErrorMsg = ""
                lblError.Text = ""
            End If

            If Not isIdCard AndAlso Not CheckNullTextBox(pwduserpassword.Text) AndAlso (CheckPassword(pwduserpassword.Text, strErrorMsg)) = False Then
                ShowMessage(strErrorMsg, getValueByKey("CLAE04"))
                lblError.Text = strErrorMsg
                ClearTextBoxes()
                Return False
            Else
                lblError.Text = ""
                strErrorMsg = ""
            End If
            '--Check Site Active Status
            If Not objLoginData.CheckSiteActiveStatus(clsAdmin.SiteCode) Then
                strErrorMsg = getValueByKey("LG026")
                ShowMessage(strErrorMsg, getValueByKey("CLAE05"))
                Return False
            End If

            If objLoginData.IsUserPasswordExpire(txtusername.Text.Trim, pwduserpassword.Text.Trim) = False Then
                strErrorMsg = getValueByKey("LG010")
                ShowMessage(strErrorMsg, getValueByKey("CLAE05"))
                Return False
            End If

            If (objLoginData.CheckAuthorizeUser(txtusername.Text, pwduserpassword.Text, strErrorMsg, dbpassword, clsAdmin.SiteCode, , , clsAdmin.UserName)) Then
                If dbpassword <> "" Then
                    If dbpassword.Length < 4 Then
                        strErrorMsg = getValueByKey("LG004")
                        'strErrorMsg = "Invalid Password"
                        lblError.Text = strErrorMsg
                        MsgBox(strErrorMsg, MsgBoxStyle.Critical, "LG004 - " & getValueByKey("CLAE04"))
                        Return False
                    Else
                        Try
                            Dim deCrpt As New clsEncrypter
                            If isIdCard Or deCrpt.authenticatePassword(pwduserpassword.Text, dbpassword) Then
                                Dim CVValidDays As Int32
                                Dim CVBaseArticleCode As String = String.Empty

                                LoginStatus = True
                                strErrorMsg = getValueByKey("LG005")
                                clsAdmin.UserCode = txtusername.Text.Trim
                                'If objLoginData.checkUserstatus(clsAdmin.UserCode, strErrorMsg, My.Settings.TerminalID, getValueByKey("CLL001")) = False Then
                                If objLoginData.checkUserstatus(clsAdmin.UserCode, strErrorMsg, ReadSpectrumParamFile("TerminalID"), getValueByKey("CLL001")) = False Then
                                    'ShowMessage(strErrorMsg, "Error")
                                    Return False
                                End If

                                SetConnection()
                                GetDefaultSettings()

                                'clsAdmin.TerminalID = My.Settings.TerminalID
                                clsAdmin.TerminalID = ReadSpectrumParamFile("TerminalID")
                                clsAdmin.PrepStationID = ReadSpectrumParamFile("mstPrepStationID")

                                clsDefaultConfiguration.Comport = ReadSpectrumParamFile("COMPORT")
                                GetPrinterTerminalInfo()

                                clsAdmin.CLPProgram = objLoginData.GetCLPProgram(clsAdmin.SiteCode, clsAdmin.ClpArticle, CLPRedemptionAllowed)
                                clsAdmin.CVProgram = objLoginData.GetCVPProgram(clsAdmin.SiteCode, CVValidDays, CVBaseArticleCode)
                                clsAdmin.CreditValidDays = CVValidDays
                                clsAdmin.CVBaseArticle = CVBaseArticleCode

                                clsDefaultConfiguration.IsBatchManagementReq = objLoginData.CheckIfBatchMgtApplicable(clsAdmin.SiteCode)
                                clsDefaultConfiguration.CSTTaxCode = objLoginData.GetCSTTaxCode(clsAdmin.SiteCode)

                                If objLoginData.CheckForMultipleDayOpen(clsAdmin.SiteCode) = False Then
                                    ShowMessage("Contact System Administrator", getValueByKey("CLAE04"))
                                    Return False
                                End If
                                clsAdmin.Financialyear = objLoginData.GetFinancialYear(clsAdmin.SiteCode)
                                GetUserAuthDetails(clsAdmin.SiteCode, clsAdmin.UserCode)
                                clsDefaultConfiguration.IsOtherCustomerRequired = CheckAuthorisation(clsAdmin.UserCode, "OtherCustomer")

                                'If clsAdmin.Financialyear = "" Then
                                '    LoginStatus = False
                                '    If CheckAuthorisation(clsAdmin.UserCode, "DAY_OPEN") Then
                                '        Dim dayOpenForm As New frmDayOpen
                                '        dayOpenForm.ShowDialog()
                                '        If dayOpenForm.dayOpenDate.Value Is Nothing Or IsDBNull(dayOpenForm.dayOpenDate.Value) Then
                                '            Return False
                                '        Else
                                '            Dim currentFinYear As String = objLoginData.GetFinancialYear(clsAdmin.SiteCode)
                                '            objLoginData.InsertDayOpenDetails(clsAdmin.SiteCode, currentFinYear, DirectCast(dayOpenForm.dayOpenDate.Value, Date).Date, clsAdmin.UserCode, clsDefaultConfiguration.IsPettyCashApplicable)
                                '            clsAdmin.Financialyear = currentFinYear
                                '            clsAdmin.DayOpenDate = dayOpenForm.dayOpenDate.Value
                                '            LoginStatus = True
                                '        End If
                                '    Else
                                '        ShowMessage(getValueByKey("LG008"), "LG008 - " & getValueByKey("CLAE04"))
                                '        Return False
                                '    End If
                                'End If
                                Try
                                    Dim dtDayOpenStatus As DataTable = objLoginData.GetDayCloseStatus(clsAdmin.SiteCode, clsAdmin.Financialyear)
                                    If dtDayOpenStatus IsNot Nothing AndAlso dtDayOpenStatus.Rows.Count > 0 Then
                                        clsAdmin.DayOpenDate = IIf(dtDayOpenStatus.Rows(0)("DayOpenDate") Is DBNull.Value, DateTime.MinValue, dtDayOpenStatus.Rows(0)("DayOpenDate"))
                                    End If
                                Catch ex As Exception
                                    LogException(ex)
                                End Try

                                clsAdmin.CurrentDate = objLoginData.GetCurrentDate()
                                If clsAdmin.CurrentDate.Date <> clsAdmin.DayOpenDate.Date Then
                                    ShowMessage(getValueByKey("LG009"), "LG009 - " & getValueByKey("CLAE04"))
                                End If

                                deCrpt = Nothing
                                lblError.Text = strErrorMsg
                                objLoginData.TrackUserInformation(clsAdmin.SiteCode, clsAdmin.CurrencyCode, EanType) 'Track usersite code,terminalID,
                                objLoginData.GetCurrencyInfo(clsAdmin.CurrencyDescription, clsAdmin.CurrencySymbol, clsAdmin.CurrencyCode)
                                objLoginData.InsertLoginHistory(txtusername.Text, clsAdmin.SiteCode, clsAdmin.TerminalID, True, IpAddress)
                                'clsAdmin.UserName = txtusername.Text
                                clsCashMemoPrint.LoginUserName = clsAdmin.UserName
                                clsAdmin.Articleimagepath = objLoginData.GetArticleImagepath
                                If Not dtAuthUserTran Is Nothing Then dtAuthUserTran.Clear()
                                GetUserAuthDetails(clsAdmin.SiteCode, clsAdmin.UserCode)
                                LogFileCreate()
                                If clsDefaultConfiguration.ShiftManagement Then
                                    LogFileForShiftActivityCreate()
                                End If
                                If clsDefaultConfiguration.IsTablet Then
                                    If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
                                        Dim drp() = Process.GetProcessesByName("osk")
                                        If drp.Length > 0 Then
                                            Dim proc As New Process
                                            For Each pr As Process In Process.GetProcesses()
                                                If pr.ProcessName = "osk" Then
                                                    pr.Kill()
                                                End If

                                            Next
                                        End If
                                    End If
                                End If
                                Me.Close()
                            Else
                                strErrorMsg = getValueByKey("LG004")
                                'strErrorMsg = "Invalid Password"
                                deCrpt = Nothing
                                'MsgBox(strErrorMsg, MsgBoxStyle.Critical, "LG004 - " & getValueByKey("CLAE04"))
                                ShowMessage(strErrorMsg, getValueByKey("CLAE04"))
                                Return False
                            End If
                        Catch ex As Exception
                            MsgBox(ex.Message, MsgBoxStyle.Critical, getValueByKey("CLAE04"))
                        End Try
                    End If
                Else
                    If dbpassword = "" And pwduserpassword.Text <> "" Then
                        strErrorMsg = getValueByKey("LG004")
                        'strErrorMsg = "Invalid Password"
                        lblError.Text = strErrorMsg
                        MsgBox(strErrorMsg, MsgBoxStyle.Critical, "LG004 - " & getValueByKey("CLAE04"))
                        Return False
                    Else
                        Dim CVValidDays As Int32
                        Dim CVBaseArticleCode As String = String.Empty
                        LoginStatus = True
                        strErrorMsg = getValueByKey("LG005")
                        clsAdmin.UserCode = txtusername.Text.Trim
                        'If objLoginData.checkUserstatus(clsAdmin.UserCode, strErrorMsg, My.Settings.TerminalID) = False Then
                        If objLoginData.checkUserstatus(clsAdmin.UserCode, strErrorMsg, ReadSpectrumParamFile("TerminalID")) = False Then
                            ShowMessage(strErrorMsg, "LG005 - " & getValueByKey("CLAE04"))
                            Return False
                        End If

                        GetDefaultSettings()
                        'clsAdmin.TerminalID = My.Settings.TerminalID
                        clsAdmin.TerminalID = ReadSpectrumParamFile("TerminalID")
                        GetPrinterTerminalInfo()

                        clsAdmin.CLPProgram = objLoginData.GetCLPProgram(clsAdmin.SiteCode, clsAdmin.ClpArticle, CLPRedemptionAllowed)
                        clsAdmin.CVProgram = objLoginData.GetCVPProgram(clsAdmin.SiteCode, CVValidDays, CVBaseArticleCode)
                        clsAdmin.CreditValidDays = CVValidDays
                        clsAdmin.CVBaseArticle = CVBaseArticleCode


                        If objLoginData.CheckForMultipleDayOpen(clsAdmin.SiteCode) = False Then
                            ShowMessage("Contact System Administrator", getValueByKey("CLAE04"))
                            Return False
                        End If
                        clsAdmin.Financialyear = objLoginData.GetFinancialYear(clsAdmin.SiteCode)

                        'If clsAdmin.Financialyear = "" Then
                        '    LoginStatus = False
                        '    If CheckAuthorisation(clsAdmin.UserCode, "DAY_OPEN") Then
                        '        Dim dayOpenForm As New frmDayOpen
                        '        dayOpenForm.ShowDialog()
                        '        If dayOpenForm.dayOpenDate.Value Is Nothing Or IsDBNull(dayOpenForm.dayOpenDate.Value) Then
                        '            Return False
                        '        Else
                        '            Dim currentFinYear As String = objLoginData.GetFinancialYear(clsAdmin.SiteCode)
                        '            objLoginData.InsertDayOpenDetails(clsAdmin.SiteCode, currentFinYear, DirectCast(dayOpenForm.dayOpenDate.Value, Date).Date, clsAdmin.UserCode, clsDefaultConfiguration.IsPettyCashApplicable)
                        '            clsAdmin.Financialyear = currentFinYear
                        '            clsAdmin.DayOpenDate = dayOpenForm.dayOpenDate.Value
                        '            LoginStatus = True
                        '        End If
                        '    Else
                        '        ShowMessage(getValueByKey("LG008"), "LG008 - " & getValueByKey("CLAE04"))
                        '        Return False
                        '    End If
                        'End If
                        clsAdmin.CurrentDate = objLoginData.GetCurrentDate()
                        lblError.Text = strErrorMsg
                        objLoginData.TrackUserInformation(clsAdmin.SiteCode, clsAdmin.CurrencyCode, EanType) 'Track usersite code,terminalID,
                        objLoginData.GetCurrencyInfo(clsAdmin.CurrencyDescription, clsAdmin.CurrencySymbol, clsAdmin.CurrencyCode)
                        objLoginData.InsertLoginHistory(txtusername.Text, clsAdmin.SiteCode, clsAdmin.TerminalID, True, IpAddress)
                        clsAdmin.UserName = txtusername.Text
                        clsAdmin.Articleimagepath = objLoginData.GetArticleImagepath
                        If Not dtAuthUserTran Is Nothing Then dtAuthUserTran.Clear()
                        GetUserAuthDetails(clsAdmin.SiteCode, clsAdmin.UserCode)
                        clsDefaultConfiguration.IsOtherCustomerRequired = CheckAuthorisation(clsAdmin.UserCode, "OtherCustomer")

                        LogFileCreate()
                        Me.Close()
                    End If
                End If

            Else
                'objLoginData.CheckUserNameOrPassword(txtusername.Text, pwduserpassword.Text, strErrorMsg)
                lblError.Text = strErrorMsg
                If strErrorMsg <> String.Empty Then
                    ShowMessage(strErrorMsg, getValueByKey("CLAE04"))
                End If
                Return False
            End If

            Return True
        Catch ex As Exception

        End Try

    End Function

    Public Function CheckNullTextBox(ByVal textBoxText As String) As Boolean

        Return String.IsNullOrEmpty(textBoxText)
    End Function

    Public Function CheckPassword(ByVal passwordText As String, ByRef errorMsg As String) As Boolean
        Try
            If (passwordText.Length >= 3) Then
                Return True
            Else
                errorMsg = getValueByKey("LG003")
                'errorMsg = "Minimum Password Length cannot be less then 6 character"
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If LoginStatus = False And bSwitchUser = False Then
            If clsDefaultConfiguration.IsTablet Then
                If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
                    Dim drp() = Process.GetProcessesByName("osk")
                    If drp.Length > 0 Then
                        Dim proc As New Process
                        For Each pr As Process In Process.GetProcesses()
                            If pr.ProcessName = "osk" Then
                                pr.Kill()
                            End If

                        Next
                    End If
                End If
            End If
            Application.Exit()
        End If
    End Sub
    Private Sub frmLogin_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If LoginStatus = False And bSwitchUser = False Then
            Application.Exit()
        End If
    End Sub

    Private Sub frmNLogin_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        txtusername.Focus()
    End Sub


    Private Sub frmLogin_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "login.htm")
            End If
            If e.KeyCode = Keys.Enter Then
                btnLogin_Click(btnLogin, e)
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub GetDefaultSettings()
        Try

            Dim dt As DataTable
            Dim ObjClscomm As New SpectrumBL.clsCommon
            dt = ObjClscomm.GetDefaultSetting(_SiteCodeForDefaultConfig, "0000")
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If dr("FLDLABEL").ToString().ToUpper = "LOCALSITECODE" Then
                        clsAdmin.SiteCode = dr("FLDVALUE").ToString

                    ElseIf dr("FLDLABEL").ToString().ToUpper = "ROUNDOFFREQUIRED" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                        clsDefaultConfiguration.RoundOffRequired = True
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "BILLROUNDOFF" Then
                        clsDefaultConfiguration.BillRoundOffAt = dr("FLDVALUE").ToString
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "ARTICLETAXALLOWED" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                        clsDefaultConfiguration.ArticleTaxAllowed = True
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "NEGATIVEINVENTORYALLOWED" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                        clsDefaultConfiguration.NegativeInventoryAllowed = True
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "CLPPOINTSSALEALLOWED" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                        clsDefaultConfiguration.CLPPointSaleAllowed = True

                    ElseIf dr("FLDLABEL").ToString().ToUpper = "ASKFORMORECUSTOMERINFO" Then
                        clsDefaultConfiguration.AskForMoreCustomerInfo = dr("FldValue")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "PRICECHANGEALLOWED" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                        clsDefaultConfiguration.PriceChageAllowed = True
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "EXCLUSIVETAXAFTERDISCOUNT" Then
                        clsDefaultConfiguration.ExclusiveTaxAfterDisc = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "CHECKEXPIRYMONTH" Then
                        clsDefaultConfiguration.CheckExpiryMonth = dr("FLDVALUE")
                        'ElseIf dr("FLDLABEL").ToString().ToUpper() = "SOSupplier".ToUpper() Then
                        '    clsDefaultConfiguration.SupplierCode = dr("FldValue")
                    ElseIf dr("FLDLABEL").ToString().ToUpper() = "ISMANUALCLPCUSTOMERSEARCH".ToUpper() Then
                        clsDefaultConfiguration.IsManualCLPCustomerSearch = dr("FldValue")
                    ElseIf dr("FLDLABEL").ToString().ToUpper() = "CreditCardRefund".ToUpper() Then
                        clsDefaultConfiguration.IsCreditCardRefundAllowed = dr("FldValue")
                    ElseIf dr("FLDLABEL").ToString().ToUpper() = "IsChequeRefundAllowed".ToUpper() Then
                        clsDefaultConfiguration.IsChequeRefundAllowed = dr("FldValue")
                    ElseIf dr("FLDLABEL").ToString().ToUpper() = "CreditCardInfo".ToUpper() Then
                        clsDefaultConfiguration.CreditCardInfo = dr("FldValue")
                    ElseIf dr("FLDLABEL").ToString().ToUpper() = "TillOpenRequired".ToUpper() Then
                        clsDefaultConfiguration.TillOperationRequired = dr("FldValue")
                    ElseIf dr("FLDLABEL").ToString().ToUpper() = "GVARTICLE".ToUpper() Then
                        clsDefaultConfiguration.GVBaseArticle = dr("FldValue")
                    ElseIf dr("FLDLABEL").ToString().ToUpper() = "CLPARTICLE".ToUpper() Then
                        clsDefaultConfiguration.ClpBaseArticle = dr("FldValue")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "INCLUSIVETAXDISPLAY".ToUpper() Then
                        clsDefaultConfiguration.InclusiveTaxDisplay = dr("FLDVALUE").ToString
                        'ElseIf dr("FLDLABEL").ToString().ToUpper() = "CVARTICLE".ToUpper() Then
                        '    clsDefaultConfiguration.CVBaseArticle = dr("FldValue")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "DECIMALSUPTO".ToUpper() Then
                        clsDefaultConfiguration.DecimalPlaces = dr("FLDVALUE").ToString
                        'Change by Ashish
                        'Commenting the below lines as these field/values have been moved to PrinterTillMap table
                        'ElseIf dr("FLDLABEL").ToString().ToUpper() = "PRINTBLPAGESETUP".ToUpper() Then
                        '    SpectrumPrint.clsBirthList.PrintBLPageSetup = dr("FldValue")
                        ' ElseIf dr("FLDLABEL").ToString().ToUpper() = "PRINTSOPAGESETUP".ToUpper() Then
                        '    SpectrumPrint.clsPrintSalesOrder.PrintSOPageSetup = dr("FldValue")
                        'ChequeinfoReq,ChequeInfomation
                        'End of change

                    ElseIf dr("FLDLABEL").ToString().ToUpper() = "ChequeinfoReq".ToUpper() Then
                        clsDefaultConfiguration.ChequeInfomation = dr("FldValue")
                    ElseIf dr("FLDLABEL").ToString().ToUpper() = "isMAPBased".ToUpper() Then
                        clsDefaultConfiguration.isMAPbasedCost = dr("FldValue")
                    ElseIf dr("FLDLABEL").ToString().ToUpper() = "BarcodeType".ToUpper() Then
                        BarCodeType = dr("FldValue")
                    ElseIf dr("FLDLABEL").ToString().ToUpper() = "FISCALPRINTERNAME".ToUpper() Then
                        CreateSpectrumParamFile("FISCALPRINTERNAME", dr("FldValue"))
                        'ElseIf dr("FLDLABEL").ToString().ToUpper() = "AdsrReportProcedureName".ToUpper() Then
                        '    clsDefaultConfiguration.AdsrReportProcedureName = dr("FldValue")
                        'Added By Rohit for CR-6001
                    ElseIf dr("FLDLABEL").ToString().ToUpper() = "SignOnRequiredForCashMemo".ToUpper() Then
                        clsDefaultConfiguration.SignOnRequired = Convert.ToBoolean(dr("FldValue"))

                        'Rakesh-23.08.2013: Set Top & Bottom margin, default margin is 35
                    ElseIf dr("FLDLABEL").ToString().ToUpper() = "PrintMarginTop".ToUpper() Then
                        clsDefaultConfiguration.PrintMarginTop = Convert.ToInt32(dr("FldValue"))
                    ElseIf dr("FLDLABEL").ToString().ToUpper() = "PrintMarginBottom".ToUpper() Then
                        clsDefaultConfiguration.PrintMarginBottom = Convert.ToInt32(dr("FldValue"))

                        'ElseIf dr("FLDLABEL").ToString().ToUpper() = "BatchManagementMandatory".ToUpper() Then
                        '    clsDefaultConfiguration.IsBatchManagementReq = Convert.ToBoolean(dr("FldValue"))
                        'ElseIf dr("FLDLABEL").ToString().ToUpper() = "IsOtherCustomerRequired".ToUpper() Then
                        '    clsDefaultConfiguration.IsOtherCustomerRequired = Convert.ToBoolean(dr("FldValue"))
                    ElseIf dr("FLDLABEL").ToString().ToUpper() = "IsPrintingWithDefaultFontReq".ToUpper() Then
                        modCommanFunction.IsPrintingWithDefaultFontReq = Convert.ToBoolean(dr("FldValue"))

                    ElseIf dr("FLDLABEL").ToString().ToUpper = "WEBSERVICESTOCKURL" Then
                        clsDefaultConfiguration.WebserviceStockURL = dr("FLDVALUE")
                        'My.WebServices.Spectrum_StockWebservice_ArticleStockBalancesSynchronizerService.Url = clsDefaultConfiguration.WebserviceStockURL

                    ElseIf dr("FLDLABEL").ToString().ToUpper = "CustomerServiceUrl".ToUpper() Then
                        clsDefaultConfiguration.CustomerWebServiceUrl = dr("FLDVALUE")
                        'My.WebServices.Spectrum_StockWebservice_ArticleStockBalancesSynchronizerService.Url = clsDefaultConfiguration.WebserviceStockURL

                    ElseIf dr("FLDLABEL").ToString().ToUpper = "IsCstTaxRequired".ToUpper() Then
                        clsDefaultConfiguration.IsCstTaxRequired = dr("FLDVALUE")
                        'ElseIf dr("FLDLABEL").ToString().ToUpper = "CSTTaxCode".ToUpper() Then
                        '    clsDefaultConfiguration.CSTTaxCode = dr("FLDVALUE")
                        'My.WebServices.ArticleStockBalancesSynchronizerService.Url = clsDefaultConfiguration.WebserviceStockURL
                        'Add End

                        'ElseIf dr("FLDLABEL").ToString().ToUpper() = "MDIBGImagePath".ToUpper() Then
                        'CreateLocalParaFile(dr("FLDLABEL").ToString().ToUpper(), dr("FldValue"))
                        'code is added by irfan on 22/9/2017 for background image in login page and mdispectrum page.
                    ElseIf dr("FLDLABEL").ToString().ToUpper() = "MDIBGImagePath".ToUpper() Then
                        imgpath = dr("FLDVALUE")

                        'Rakesh:09-July-2013-->Start: Template based cashmemo bill printing parameter
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "TEMPLATEPRINTINGALLOWED" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                        clsDefaultConfiguration.TemplatePrintingAllowed = True

                    ElseIf dr("FLDLABEL").ToString().ToUpper = "PRINTLINELENGTH" Then
                        SpectrumCommon.PrintLineLength = dr("FLDVALUE")

                    ElseIf dr("FLDLABEL").ToString().ToUpper = "TEMPLATENAME" Then
                        SpectrumCommon.TemplateName = dr("FLDVALUE")

                    ElseIf dr("FLDLABEL").ToString().ToUpper = "DRAWLINETEXTCODE" Then
                        SpectrumCommon.DrawLineTextCode = dr("FLDVALUE")

                    ElseIf dr("FLDLABEL").ToString().ToUpper = "TEMPLATEXMLNAME" Then
                        SpectrumCommon.TemplateXmlName = dr("FLDVALUE")

                    ElseIf dr("FLDLABEL").ToString().ToUpper = "COMBOITEMPRINTINGALLOWED" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                        SpectrumCommon.ComboItemPrintingAllowed = True

                        'Rakesh:09-July-2013-->End: Template based cashmemo bill printing parameter

                        'Start Weighing Scale Related Paramerters
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "WeightScaleEnabled".ToUpper() Then
                        clsDefaultConfiguration.WeightScaleEnabled = CBool(dr("FLDVALUE"))
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "WeightBarcodePrefix".ToUpper() Then
                        clsDefaultConfiguration.WeightBarcodePrefix = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "WeightBarcodePrefixDigits".ToUpper() Then
                        clsDefaultConfiguration.WeightBarcodePrefixDigits = CInt(dr("FLDVALUE"))
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "WeightBarcodeLength".ToUpper() Then
                        clsDefaultConfiguration.WeightBarcodeLength = CInt(dr("FLDVALUE"))
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "WeightBarcodeWholeNOLength".ToUpper() Then
                        clsDefaultConfiguration.WeightBarcodeWholeNOLength = CInt(dr("FLDVALUE"))
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "WeightBarcodedecimalLength".ToUpper() Then
                        clsDefaultConfiguration.WeightBarcodedecimalLength = CInt(dr("FLDVALUE"))
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "WeightBarcodeRateApplicable".ToUpper() Then
                        clsDefaultConfiguration.WeightBarcodeRateApplicable = CBool(dr("FLDVALUE"))
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "WeightBarcodeRateLength".ToUpper() Then
                        clsDefaultConfiguration.WeightBarcodeRateLength = CInt(dr("FLDVALUE"))
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "WeightBarcodeRateWholeNOLength".ToUpper() Then
                        clsDefaultConfiguration.WeightBarcodeRateWholeNOLength = CInt(dr("FLDVALUE"))
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "WeightBarcodeRateDecimalLength".ToUpper() Then
                        clsDefaultConfiguration.WeightBarcodeRateDecimalLength = CInt(dr("FLDVALUE"))
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "WeightBarcodeSequence".ToUpper() Then
                        clsDefaultConfiguration._WeightBarcodeSequence = dr("FLDVALUE")

                    ElseIf dr("FLDLABEL").ToString().ToUpper = "BarcodeDisplayAllowed".ToUpper() Then
                        clsDefaultConfiguration.BarcodeDisplayAllowed = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "ITEMFULLNAME" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                        clsDefaultConfiguration.PrintItemFullName = True
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "SpectrumLiteAllowed".ToUpper() Then
                        clsDefaultConfiguration.SpectrumLiteAllowed = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper() = "CashMemoResetonDayClose".ToUpper() Then
                        clsDefaultConfiguration.CashMemoResetonDayClose = dr("FLDVALUE").ToString
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "TILLCLOSEPRINTPREVIEWREQUIRED".ToUpper() Then
                        clsDefaultConfiguration.TillClosePrintPreivewReq = dr("FLDVALUE")
                        '------ Added By Mahesh for Notification Changes 
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "ChecklistOnTillOpen".ToUpper() Then
                        clsDefaultConfiguration.ChecklistOnTillOpen = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "NotificationPopUpRequired".ToUpper() Then
                        clsDefaultConfiguration.NotificationPopUpRequired = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "NotificationText".ToUpper() Then
                        clsDefaultConfiguration.NotificationText = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "NotificationTiming".ToUpper() Then
                        clsDefaultConfiguration.NotificationTiming = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "MettlerDBConnectionString".ToUpper() Then
                        clsDefaultConfiguration.MettlerConnString = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "DayCloseReportPath".ToUpper() Then
                        clsDefaultConfiguration.DayCloseReportPath = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "ShiftManagement".ToUpper() Then
                        clsDefaultConfiguration.ShiftManagement = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "ClientName".ToUpper() Then
                        clsDefaultConfiguration.ClientName = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "PCRoundOFF".ToUpper() Then
                        clsDefaultConfiguration.PCRoundOff = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "BillFontSelection".ToUpper() Then
                        modCommanFunction.BillFont = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "CustSearchCharpos".ToUpper() Then
                        clsDefaultConfiguration.CustSearchCharpos = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "CustSearchOn".ToUpper() Then
                        clsDefaultConfiguration.CustSearchParameter = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "DayCloseProceedOnTillClose".ToUpper() Then
                        clsDefaultConfiguration.DayCloseProceedOnTillClose = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "AutoUpdateOnLogin".ToUpper() Then
                        clsDefaultConfiguration.AutoUpdateOnLogin = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "JKTicketingSystem".ToUpper() Then
                        clsDefaultConfiguration.JkTicketingSystem = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "ordPrepTime".ToUpper() Then
                        clsDefaultConfiguration.OrdPrepTime = dr("FLDVALUE")
                        'ElseIf dr("FLDLABEL").ToString().ToUpper = "DetailedCustomerCreationformat".ToUpper() Then
                        '    clsDefaultConfiguration.DetailedCustomerCreationformat = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "IsNewSalesOrder".ToUpper() Then
                        clsDefaultConfiguration.IsNewSalesOrder = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "EnablewildSearch".ToUpper() Then
                        clsDefaultConfiguration.EnablewildSearch = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "UpdateBillTime".ToUpper() Then
                        clsDefaultConfiguration.UpdateBillTime = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "WildSearchInNewCust".ToUpper() Then
                        clsDefaultConfiguration.IsCustAddWild = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "CDBuildCode".ToUpper() Then
                        clsDefaultConfiguration.CDBuildCode = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "CUSTOMER_SEARCH_USING_PHONE_NUMBER".ToUpper() Then
                        clsDefaultConfiguration.CUSTOMERSEARCHUSINGPHONENUMBER = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "ThemeSelect".ToUpper() Then
                        clsDefaultConfiguration.ThemeSelect = dr("FLDVALUE")
                        CommonFunc.Themeselect = clsDefaultConfiguration.ThemeSelect
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "DefaultModuleAfterLogin".ToUpper() Then 'vipin
                        clsDefaultConfiguration.DefaultModuleAfterLogin = dr("FLDVALUE").ToString()
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "SynchBatFile".ToUpper() Then  'added by khusrao on 11-09-2017 for jk sprint 30 requirment
                        clsDefaultConfiguration.SynchBatFile = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "IsSavoy".ToUpper() Then
                        clsDefaultConfiguration.IsSavoy = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "BatchFileProcessName".ToUpper() Then
                        clsDefaultConfiguration.BatchFileProcessName = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "PromotionBasedOn".ToUpper() Then
                        clsDefaultConfiguration.PromotionBasedOn = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "IsMembership".ToUpper Then
                        clsDefaultConfiguration.IsMembership = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "AutoDBBackupOnShiftClose".ToUpper Then
                        clsDefaultConfiguration.AutoDBBackupOnShiftClose = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "DatabaseBackupPath".ToUpper() Then
                        clsDefaultConfiguration.DatabaseBackupPath = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "DineInButtonWithText".ToUpper() Then
                        clsDefaultConfiguration.DineInButtonWithText = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "AllowOnScreenKeyBoard".ToUpper() Then
                        clsDefaultConfiguration.AllowOnScreenKeyBoard = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "Grievance_Remark_AttachmentPath".ToUpper() Then
                        clsDefaultConfiguration.GrievanceRemarkAttachment = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "IsTablet".ToUpper() Then
                        clsDefaultConfiguration.IsTablet = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "IsCustomerMandatoryForCreditSale".ToUpper() Then
                        clsDefaultConfiguration.IsCustomerMandatoryForCreditSale = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "KOTAndBillGeneration".ToUpper() Then
                        clsDefaultConfiguration.KOTAndBillGeneration = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "RecordDaysToDelete".ToUpper() Then
                        clsDefaultConfiguration.RecordDaysToDelete = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "DoneSystemApplicable".ToUpper() Then
                        clsDefaultConfiguration.DoneSystemApplicable = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "ExternalOrdersTillNo".ToUpper() Then
                        clsDefaultConfiguration.ExternalOrdersTillNo = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "CustomerClassSelection".ToUpper() Then
                        clsDefaultConfiguration.CustomerClassSelection = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "TicketingSystemPopupDisplayInterval".ToUpper() Then
                        clsDefaultConfiguration.TicketingSystemPopupInterval = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "ReservationCancelTiming".ToUpper() Then
                        clsDefaultConfiguration.ReservationCancelTiming = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper() = "ISReservationCancelOnTimer".ToUpper() Then
                        clsDefaultConfiguration.ISReservationCancelOnTimer = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper() = "ISPoleDisplay".ToUpper() Then
                        clsDefaultConfiguration.IsPoleDisply = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper() = "IsHostManagement".ToUpper() Then
                        clsDefaultConfiguration.IsHostManagementEnable = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString.ToUpper() = "IsHariOM".ToUpper() Then  '' added by nikhil
                        clsDefaultConfiguration.IsHariOM = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString.ToUpper() = "AutoSyncOnDSR".ToUpper() Then  ''jk sprint 25
                        clsDefaultConfiguration.AutoSyncOnDSR = dr("FLDVALUE")
                        'code added for jk sprint 28
                    ElseIf dr("FLDLABEL").ToString.ToUpper() = "OpenBackOfficeFromFO".ToUpper() Then
                        clsDefaultConfiguration.OpenBackOfficeFromFO = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString.ToUpper() = "EnableAmmyyAdmin".ToUpper() Then
                        clsDefaultConfiguration.EnableAmmyyAdmin = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString.ToUpper() = "ReportTerminalId".ToUpper() Then
                        clsDefaultConfiguration.ReportTerminalId = dr("FLDVALUE").ToString()        'irfan 28/9/2017
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "IsGSTForComboArticle".ToUpper() Then   'vipin 07.05.2017
                        clsDefaultConfiguration.IsGSTForComboArticle = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "EnableCreditSalesPopup".ToUpper() Then   'added by khusrao adil on 13-03-2018 for spectrum new developement 
                        clsDefaultConfiguration.EnableCreditSalesPopup = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "EnableExpiryProduct".ToUpper() Then
                        clsDefaultConfiguration.EnableExpiryProductPopup = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "EnableExpiryProductDays".ToUpper() Then
                        clsDefaultConfiguration.EnableExpiryProductDaysPopup = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "CreditSalesPopupInterval".ToUpper() Then   'added by khusrao adil on 13-03-2018 for spectrum new developement 
                        clsDefaultConfiguration.CreditSalesPopupInterval = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "CreditSalesPopupRecordsBeforeHours".ToUpper() Then   'added by khusrao adil on 15-03-2018 for spectrum new developement 
                        clsDefaultConfiguration.CreditSalesPopupRecordsBeforeHours = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "EnableLowStockNotificatonPopup".ToUpper() Then   'added by khusrao adil on 15-03-2018 for spectrum new developement 
                        clsDefaultConfiguration.EnableLowStockNotificatonPopup = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "LowStockNotificatonInterval".ToUpper() Then   'added by khusrao adil on 15-03-2018 for spectrum new developement 
                        clsDefaultConfiguration.LowStockNotificationInterval = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "EnableSalesOrderPopup".ToUpper() Then   'added by khusrao adil on 13-03-2018 for spectrum new developement 
                        clsDefaultConfiguration.EnableSalesOrderPopup = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "SalesOrderPopupInterval".ToUpper() Then   'added by khusrao adil on 13-03-2018 for spectrum new developement 
                        clsDefaultConfiguration.SalesOrderPopupInterval = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "SalesOrderPopupRecordsBeforeHours".ToUpper() Then   'added by khusrao adil on 15-03-2018 for spectrum new developement 
                        clsDefaultConfiguration.SalesOrderPopupRecordsBeforeHours = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "EnableScanQRCode".ToUpper() Then 'Vipin For Scan QR code Changes
                        clsDefaultConfiguration.EnableScanQRCode = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "QRCodeTrailer".ToUpper() Then 'Vipin For Scan QR code Changes
                        clsDefaultConfiguration.QRCodeTrailer = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "IsNewCreditSale".ToUpper() Then 'Vipin For Credit Sale changes
                        clsDefaultConfiguration.IsNewCreditSale = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "OrderPackagingScreenScrollTimeInterval".ToUpper() Then 'vipin So merge
                        clsDefaultConfiguration.OrderPackagingScreenScrollTimeInterval = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "EnableMailReSend".ToUpper() Then 'vipin Mail Resend 08.08.2018
                        clsDefaultConfiguration.EnableMailReSend = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "FailedMailReSendInterval".ToUpper() Then 'vipin Mail Resend 08.08.2018
                        clsDefaultConfiguration.FailedMailReSendInterval = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "KotWiseKDS".ToUpper() Then 'vipin Mail Resend 08.08.2018
                        clsDefaultConfiguration.KotWiseKds = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "KDSScreenRefershTimeInterval".ToUpper() Then   ' vipin 21.01.2019 
                        clsDefaultConfiguration.KDSScreenTimeInterval = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "AutoKOTGenerateTimeInterval".ToUpper() Then   ' vipin 21.01.2019 
                        clsDefaultConfiguration.AutoKOTGenerateTimeInterval = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "OnlineOrderNotificationInterval".ToUpper() Then   ' vipin 21.01.2019 
                        clsDefaultConfiguration.OnlineOrderNotificationInterval = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "EnableOnlineOrderNotification".ToUpper() Then   ' vipin 21.01.2019 
                        clsDefaultConfiguration.EnableOnlineOrderNotification = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "EnableRejectOrderNotification".ToUpper() Then
                        clsDefaultConfiguration.EnableRejectOrderNotification = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "OnlineOrderRejectionInterval".ToUpper() Then
                        clsDefaultConfiguration.OnlineOrderRejectionInterval = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "HashTagIntegrationId".ToUpper() Then
                        clsDefaultConfiguration.HashTagIntegrationID = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "EnableHashTagIntegration".ToUpper() Then
                        clsDefaultConfiguration.EnableHashTagIntegration = dr("FLDVALUE")
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "HashTagRewardsUrl".ToUpper() Then
                        clsDefaultConfiguration.HashTagRewardsUrl = dr("FLDVALUE")
                    End If
                    'End Weighing Scale Related Paramerters CUSTOMERSEARCHUSINGPHONENUMBER
                Next
            End If
            dt = ObjClscomm.GetDefaultSetting(clsAdmin.SiteCode, "PCM")
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If dr("FLDLABEL").ToString().ToUpper = "PETTYCASHTERMINALID" Then
                        clsDefaultConfiguration.PettyCashTerminalId = dr("FLDVALUE").ToString
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "MANAGEPETTYCASHONSINGLECOUNTER" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                        clsDefaultConfiguration.IsPettyCashOnSameTerminal = True
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "ISPETTYCASHAPPLICABLE" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                        clsDefaultConfiguration.IsPettyCashApplicable = True
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "ADDSALESTOPETTYCASH" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                        clsDefaultConfiguration.AddSalesToPettyCash = True
                    ElseIf dr("FLDLABEL").ToString().ToUpper = "ISPRVIEWREQUIREDFORVCHR" And dr("FLDVALUE").ToString.ToUpper = "TRUE" Then
                        clsDefaultConfiguration.IsPrviewRequiredForVchr = True
                    End If
                Next
            End If
            'update stock at store level
            Dim dtUpdateStock = ObjClscomm.GetDefaultSetting("UpdateStock")
            If Not dtUpdateStock Is Nothing AndAlso dtUpdateStock.Rows.Count > 0 Then
                For Each dr As DataRow In dtUpdateStock.Rows
                    If dr("FLDLABEL").ToString().ToUpper() = "UpdateStock".ToUpper() Then
                        clsDefaultConfiguration.UpdateStockAtStoreLevel = dr("FLDVALUE")
                    End If
                Next
            End If
            dt = ObjClscomm.GetDefaultSetting("SMS")
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If dr("FLDLABEL").ToString().ToUpper() = "sms_url".ToUpper() Then
                        clsDefaultConfiguration.SMSUrl = dr("FLDVALUE")

                    ElseIf dr("FLDLABEL").ToString().ToUpper() = "sms_url_content_transac".ToUpper() Then
                        clsDefaultConfiguration.SMSUrlParameters = dr("FLDVALUE")
                    End If
                Next
            End If

            Dim dtDetailCust = ObjClscomm.GetDefaultSetting("DetailedCustomerCreationformat")
            If Not dtDetailCust Is Nothing AndAlso dtDetailCust.Rows.Count > 0 Then
                For Each dr As DataRow In dtDetailCust.Rows
                    If dr("FLDLABEL").ToString().ToUpper() = "DetailedCustomerCreationformat".ToUpper() Then
                        clsDefaultConfiguration.DetailedCustomerCreationformat = dr("FLDVALUE")
                    End If
                Next
            End If
            Dim dtSalesOrder = ObjClscomm.GetDefaultSetting("SOBookingTills")
            If Not dtSalesOrder Is Nothing AndAlso dtSalesOrder.Rows.Count > 0 Then
                For Each dr As DataRow In dtSalesOrder.Rows
                    If dr("FLDLABEL").ToString().ToUpper = "SOBookingTills".ToUpper() Then
                        clsDefaultConfiguration.SOBookingScreenTills = dr("FLDVALUE").ToString
                    End If
                Next
            End If
            Dim dtReprint = ObjClscomm.GetDefaultSetting("NumberOfDaysApplicableForReprint")
            If Not dtReprint Is Nothing AndAlso dtReprint.Rows.Count > 0 Then
                For Each dr As DataRow In dtReprint.Rows
                    If dr("FLDLABEL").ToString().ToUpper() = "NumberOfDaysApplicableForReprint".ToUpper() Then
                        clsDefaultConfiguration.NumberOfDaysApplicableForReprint = dr("FLDVALUE")
                    End If
                Next
            End If
            Dim dtAttachment = ObjClscomm.GetDefaultSetting("Ticketing.Attachment.Size")
            If Not dtAttachment Is Nothing AndAlso dtAttachment.Rows.Count > 0 Then
                For Each dr As DataRow In dtAttachment.Rows
                    If dr("FLDLABEL").ToString().ToUpper() = "Ticketing.Attachment.Size".ToUpper() Then
                        clsDefaultConfiguration.TicketingAttachment = dr("FLDVALUE")
                    End If
                Next
            End If
            'added by Khusrao adil for grievance 
            Dim dtRenderGrievance = ObjClscomm.GetDefaultSetting("RenderGrievance")
            If Not dtRenderGrievance Is Nothing AndAlso dtRenderGrievance.Rows.Count > 0 Then
                For Each dr As DataRow In dtRenderGrievance.Rows
                    If dr("FLDLABEL").ToString().ToUpper() = "RenderGrievance".ToUpper() Then
                        clsDefaultConfiguration.RenderGrievance = dr("FLDVALUE")
                    End If
                Next
            End If
            'added by vipin on 27.08.2018 For SO SMS CHANGES
            Dim DtSOSms = ObjClscomm.GetDefaultSetting("so_sms_applicable")
            If Not DtSOSms Is Nothing AndAlso DtSOSms.Rows.Count > 0 Then
                For Each dr As DataRow In DtSOSms.Rows
                    If dr("FLDLABEL").ToString().ToUpper() = "so_sms_applicable".ToUpper() Then
                        clsDefaultConfiguration.so_sms_applicable = dr("FLDVALUE")
                    End If
                Next
            End If
            ' for JK client mail flag check
            Dim objDefault As New clsDefaultConfiguration("DC")
            objDefault.GetDefaultSettings()
        Catch ex As Exception
            ShowMessage(getValueByKey("LG006"), "LG006 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in intiallizing Default Configuration ", "Error")
        End Try
    End Sub
    Private Sub GetPrinterTerminalInfo()
        Try
            Dim clsCom As New clsCommon
            dtPrinterInfo = clsCom.GetPrinterMapping(clsAdmin.TerminalID)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmbSite_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSite.SelectedValueChanged
        If Not String.IsNullOrEmpty(cmbSite.SelectedValue) Then
            _SiteCodeForDefaultConfig = cmbSite.SelectedValue
            PopulateLanguageDropdown()
        End If
    End Sub
    Private Sub PopulateLanguageDropdown()
        GetDefaultSettings()
        Dim objLoginData As New clsLogin
        Dim msg, DefaultLang As String
        dtLang = objLoginData.GetLanguages(clsAdmin.SiteCode, msg, DefaultLang)
        If msg <> String.Empty Then
            ShowMessage(msg, getValueByKey("CLAE05"))
            Exit Sub
        End If
        If Not dtLang Is Nothing Then
            dtLang.TableName = "Culture"
            If Not dtLang Is Nothing AndAlso dtLang.Rows.Count > 0 Then
                cboLanguage.DataSource = dtLang
                cboLanguage.DisplayMember = "LANGUAGENAME"
                cboLanguage.ValueMember = "CULTURE"
                If Not DefaultLang Is Nothing Then
                    cboLanguage.SelectedValue = DefaultLang
                End If
                '"en-US"
                pC1ComboSetDisplayMember(cboLanguage)
                cboLanguage.CaptionVisible = False
                cboLanguage.ExtendRightColumn = True
            ElseIf Not dtLang Is Nothing AndAlso dtLang.Rows.Count = 0 Then
                ShowMessage(getValueByKey("LG007"), "LG007 - " & getValueByKey("CLAE04"))
                'ShowMessage("Language data Not found", "Information")
            End If
        End If
    End Sub

    Private Sub frmLogin_Rendered(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            txtusername.Focus()
            txtusername.Select()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub frmLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Me.Text = "Login to Terminal Id->" & My.Settings.TerminalID
            'Me.Text = getValueByKey("frmnlogin1") & My.Settings.TerminalID
            If clsDefaultConfiguration.AutoUpdate Then
                ctrlBtnAutoUpdate.Visible = True
            Else
                ctrlBtnAutoUpdate.Visible = False
            End If


            If UCase(ReadSpectrumParamFile("InstanceName")) = "HO" Then
                IsHOInstance = True
            End If
            Dim objLoginData As New clsLogin
            Me.Text = getValueByKey("frmnlogin1") & ReadSpectrumParamFile("TerminalID")
            If IsHOInstance Then
                cmbSite.Visible = True
                lblSite.Visible = True
                Dim sites As DataTable = objLoginData.GetAllDefaultSite()
                cmbSite.DataSource = sites
                cmbSite.DisplayMember = "SITENAME"
                cmbSite.ValueMember = "SITECODE"
                pC1ComboSetDisplayMember(cmbSite)
                cmbSite.SelectedIndex = 0
            Else
                cmbSite.Visible = False
                lblSite.Visible = False
                _SiteCodeForDefaultConfig = "0000"
                PopulateLanguageDropdown()
            End If
            ctrlBtnDbConnection.Text = "Test Connection"
            txtusername.Select()
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                ThemeChange()
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Else
                'If clsDefaultConfiguration.HostTerminals IsNot Nothing AndAlso clsDefaultConfiguration.HostTerminals <> "" Then

                '    ctrlBtnAutoUpdate.Visible = False
                '    ctrlBtnDbConnection.Visible = False
                '    Me.BackgroundImage = Global.Spectrum.My.Resources.host_loging_bg
                '    '    Me.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black
                '    Me.BackgroundColor = Color.FromArgb(236, 236, 236)
                '    Me.Size = New Size(650, 481)
                '    '   Me.Location = New Point(0, 0)
                '    Me.StartPosition = FormStartPosition.CenterScreen
                '    Me.MaximizeBox = True
                '    Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None

                '    Me.C1Sizer1.Location = New Point(142, 215)
                '    Me.C1Sizer1.BringToFront()
                '    Me.C1Sizer1.BackColor = Color.FromArgb(236, 236, 236)
                '    Me.C1Sizer1.Border.Color = Color.FromArgb(236, 236, 236)
                '    Me.C1Sizer1.BackgroundImage = Global.Spectrum.My.Resources.host_loging_table_bg
                '    ' C1Sizer1.BackColor = Color.Transparent
                '    '
                '    'Me.txtusername.Margin = New Padding(0, 0, 10, 0)
                '    'Me.txtusername.Padding = New Padding(0, 0, 10, 0)
                '    Me.txtusername.BackColor = Color.White
                '    'Me.txtusername.BorderStyle = BorderStyle.None
                '    Me.pwduserpassword.BackColor = Color.White
                '    'Me.pwduserpassword.BorderStyle = BorderStyle.None
                '    Me.txtIDcard.BackColor = Color.White
                '    'Me.txtIDcard.BorderStyle = BorderStyle.None
                '    Me.cboLanguage.VisualStyle = C1.Win.C1List.VisualStyle.System
                '    Me.cboLanguage.BackColor = Color.White

                '    Me.cmbSite.VisualStyle = C1.Win.C1List.VisualStyle.System
                '    Me.cmbSite.BackColor = Color.White

                '    Me.lblMessage.BackColor = Color.FromArgb(236, 236, 236)
                '    Me.lblMessage.BorderColor = Color.FromArgb(236, 236, 236)
                '    Me.lblMessage.Font = New Font("Trebuchet MS", 10, FontStyle.Bold)

                '    Me.lblUserId.BackColor = Color.FromArgb(236, 236, 236)
                '    Me.lblUserId.BorderColor = Color.FromArgb(236, 236, 236)
                '    Me.lblUserId.Font = New Font("Trebuchet MS", 10, FontStyle.Bold)

                '    Me.lblPassword.BackColor = Color.FromArgb(236, 236, 236)
                '    Me.lblPassword.BorderColor = Color.FromArgb(236, 236, 236)
                '    Me.lblPassword.Font = New Font("Trebuchet MS", 10, FontStyle.Bold)

                '    Me.lblID.BackColor = Color.FromArgb(236, 236, 236)
                '    Me.lblID.BorderColor = Color.FromArgb(236, 236, 236)
                '    Me.lblID.Font = New Font("Trebuchet MS", 10, FontStyle.Bold)

                '    Me.lblSite.BackColor = Color.FromArgb(236, 236, 236)
                '    Me.lblSite.BorderColor = Color.FromArgb(236, 236, 236)
                '    Me.lblSite.Font = New Font("Trebuchet MS", 10, FontStyle.Bold)

                '    Me.btnLogin.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                '    Me.btnLogin.FlatStyle = FlatStyle.Flat
                '    Me.btnLogin.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
                '    Me.btnLogin.FlatAppearance.BorderSize = 0
                '    Me.btnLogin.BackColor = Color.FromArgb(0, 113, 188)
                '    Me.btnLogin.ForeColor = Color.White

                '    Me.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                '    Me.btnCancel.FlatStyle = FlatStyle.Flat
                '    Me.btnCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
                '    Me.btnCancel.FlatAppearance.BorderSize = 0
                '    Me.btnCancel.BackColor = Color.FromArgb(0, 113, 188)
                '    Me.btnCancel.ForeColor = Color.White
                'End If
            End If


        Catch ex As Exception
            'ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub cboLanguage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboLanguage.SelectedValueChanged
        Try
            If cboLanguage.SelectedValue IsNot Nothing Then
                gActiveLangId = cboLanguage.SelectedValue.ToString
                If IsDBNull(cboLanguage.SelectedValue) Or cboLanguage.SelectedValue Is Nothing Then
                    clsAdmin.CultureInfo = "en-US"
                    clsAdmin.LangCode = "EN"
                    SetCurrentculture(clsAdmin.CultureInfo)
                Else
                    clsAdmin.CultureInfo = cboLanguage.SelectedValue
                    clsAdmin.LangCode = cboLanguage.Columns("LanguageCode").Value
                    SetCurrentculture(clsAdmin.CultureInfo)
                End If
            End If
            SetCulture(Me)
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                ctrlBtnDbConnection.Text = String.Empty
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ctrlBtnDbConnection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ctrlBtnDbConnection.Click
        'Dim frm As New frmNDbConnectionBuild
        'frm.ShowDialog()
        'txtusername.Focus()        
        Dim dataSource As String = ReadSpectrumParamFile("DataSource")
        Dim server As String = ReadSpectrumParamFile("Server")
        Dim userId As String = ReadSpectrumParamFile("UserId")
        'Dim password As String = Decrypt(ReadLocalParaFile("Password"))
        Dim password As String = ReadSpectrumParamFile("Password")
        TestConnection(server, dataSource, userId, password)
    End Sub
    Private Sub ctrlBtnAutoUpdate_Click(sender As System.Object, e As System.EventArgs) Handles ctrlBtnAutoUpdate.Click

        Try

            ctrlBtnAutoUpdate.Enabled = False
            AutoUpdates()

        Catch ex As Exception
            LogException(ex)
            ctrlBtnAutoUpdate.Enabled = True
        End Try
    End Sub
    Private Sub TestConnection(ByVal server As String, ByVal dataSource As String, ByVal userId As String, ByVal password As String)
        Dim con As SqlClient.SqlConnection
        Try
            con = New SqlClient.SqlConnection("Server= " & server & ";DataBase=" & dataSource & ";Uid=" & userId & ";pwd=" & password & " ; Max Pool Size=10  ")
            con.Open()
            'ShowMessage(getValueByKey("DBCB004"), "DBCB004")
            ShowMessage(getValueByKey("DBCB004"), "DBCB004 - " & getValueByKey("CLAE04"))
        Catch ex As Exception
            'ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub ClearTextBoxes()
        lblError.Text = ""
        lblErrortxtPassward.Text = ""
        lblErrorTxtUsername.Text = ""
        txtusername.Text = ""
        txtIDcard.Text = ""
        pwduserpassword.Text = ""
        txtusername.Focus()
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
            LogException(ex)
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
    Public Sub AutoUpdates()
        Try
            Dim objCls As New clsCommon
            'Dim dtInfo As New DataTable
            'dtInfo = objCls.GetRemoteIpPort()
            'If dtInfo IsNot Nothing Then
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


            ShowMessage("Need to Log out All Tills before proceed", "DBCB004 - " & getValueByKey("CLAE04"))

            Dim proxy As New FOAutomaticVersionUpgradeServiceImplClient
            Dim exs As New executeService
            Dim exser As New executeService



            Dim licence As New ClsLicense
            Dim HDDKey = licence.GetEncryptedHDDKey()
            Dim exsA As executeService = CreateExecuteServiceObject("EN", "isUpdateAvailable", HDDKey, clsAdmin.SiteCode, clsAdmin.TerminalID)

            Dim resp As executeServiceResponse = proxy.executeService(exsA)
            Dim resp1 As wsResponse = resp.[return]
            If resp1.responseCode = "200" Then

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
                Dim AutoUpdateDayClose As String = "No"
                'HDDKey = "c4:43:8f:43:e4:2f"
                Dim pHelp As New ProcessStartInfo
                pHelp.FileName = "SpectrumUpdate.exe"
                pHelp.Arguments = "" & dataSource & " " & dbname & " " & username & " " & password & " " & clientname.Replace(" ", "") & " " & HDDKey & " " & clsAdmin.SiteCode & " " & clsAdmin.TerminalID & " " & AutoUpdateDayClose & ""
                pHelp.WorkingDirectory = directoryPath
                'pHelp.WorkingDirectory = "C:\Users\sagar.borole\Desktop\ConsoleApplication1"
                Process.Start(pHelp)
                Application.Exit()
            Else
                ShowMessage("New Version not available", "DBCB004 - " & getValueByKey("CLAE04"))
                ctrlBtnAutoUpdate.Enabled = True
            End If
        Catch ex As Exception
            'MessageBox.Show("Error")
            LogException(ex)
            ctrlBtnAutoUpdate.Enabled = True
            If (ex.ToString().Contains("no endpoint")) Then
                ShowMessage("Invalid CCE Connection", "DBCB004 - " & getValueByKey("CLAE04"))
            End If



        End Try
    End Sub
    Public Sub AutoUpdateLogin()
        Try

            Dim objCls As New clsCommon
            'Dim dtInfo As New DataTable
            'dtInfo = objCls.GetRemoteIpPort()
            'If dtInfo IsNot Nothing Then
            Try
                Using client = New WebClient()
                    Using stream = client.OpenRead("http://www.google.com")
                    End Using
                End Using
            Catch ex As Exception
                LogException(ex)



                Exit Sub
            End Try


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
                message.Append("Spectrum is being updated to latest version.")
                message.Append("Spectrum application will now close while this operation is being performed.")
                message.Append("Kindly be patient during this process..")
                ShowBigMessagewithOK(message.ToString(), "DBCB004 - " & getValueByKey("CLAE04"))
                Application.DoEvents()
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
                Dim AutoUpdateDayClose As String = "No"
                'HDDKey = "c4:43:8f:43:e4:2f"
                Dim pHelp As New ProcessStartInfo
                pHelp.FileName = "SpectrumUpdate.exe"
                pHelp.Arguments = "" & dataSource & " " & dbname & " " & username & " " & password & " " & clientname.Replace(" ", "") & " " & HDDKey & " " & clsAdmin.SiteCode & " " & clsAdmin.TerminalID & " " & AutoUpdateDayClose & ""
                pHelp.WorkingDirectory = directoryPath
                'pHelp.WorkingDirectory = "C:\Users\sagar.borole\Desktop\ConsoleApplication1"
                Process.Start(pHelp)
                Application.Exit()
            Else

                ctrlBtnAutoUpdate.Enabled = True
            End If
        Catch ex As Exception
            'MessageBox.Show("Error")
            LogException(ex)
            ctrlBtnAutoUpdate.Enabled = True
            If (ex.ToString().Contains("no endpoint")) Then
                ShowMessage("Invalid CCE Connection", "DBCB004 - " & getValueByKey("CLAE04"))
            End If



        End Try
    End Sub

    Dim p1, p2 As New Panel
    Private Function ThemeChange()
        Me.BackgroundImage = Global.Spectrum.My.Resources.LoginScreen1
        Me.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Blue
        'Me.Size = New Size(932, 550)
        Me.Size = New Size(1019, 481)
        Me.Location = New Point(0, 0)
        Me.StartPosition = FormStartPosition.CenterScreen

        C1Sizer1.BackColor = Color.White
        C1Sizer1.Size = New Size(406, 341)
        C1Sizer1.Location = New Point(621, 55)
        'Me.C1Sizer1.GridDefinition = <data name="C1Sizer1.GridDefinition" xml:space="preserve">
        '                                 <value>7.33137829912023:False:True;5.86510263929619:False:True;5.86510263929619:False:True;7.33137829912023:False:True;5.86510263929619:False:True;7.33137829912023:False:True;5.86510263929619:False:False;7.33137829912023:False:False;5.86510263929619:False:False;7.33137829912023:False:False;5.86510263929619:False:False;7.33137829912023:False:False;11.1436950146628:False:False;9.67741935483871:False:False;	20.4787234042553:False:False;31.9148936170213:False:True;31.9148936170213:False:False;15.6914893617021:False:False;</value>
        '                             </data>

        Me.C1Sizer1.GridDefinition = <data name="C1Sizer1.GridDefinition" xml:space="preserve">
                                         <value>7.33137829912023:False:True;5.86510263929619:False:True;5.86510263929619:False:True;7.33137829912023:False:True;5.86510263929619:False:True;7.33137829912023:False:True;5.86510263929619:False:False;7.33137829912023:False:False;5.86510263929619:False:False;7.33137829912023:False:False;5.86510263929619:False:False;7.33137829912023:False:False;11.1436950146628:False:False;9.67741935483871:False:False;	14.6276595744681:False:True;29.2553191489362:False:True;5.31914893617021:False:True;29.2553191489362:False:True;14.3617021276596:False:True;</value>
                                     </data>
        'Me.C1Sizer1.GridDefinition = <data name="C1Sizer1.GridDefinition" xml:space="preserve">
        '                                 <value>7.33137829912023:False:True;4.39882697947214:False:True;7.33137829912023:False:True;7.33137829912023:False:True;7.33137829912023:False:True;7.33137829912023:False:True;7.33137829912023:False:True;7.33137829912023:False:True;7.33137829912023:False:True;7.33137829912023:False:True;7.33137829912023:False:True;7.33137829912023:False:True;7.33137829912023:False:True;7.62463343108504:False:False;	14.6276595744681:False:True;29.2553191489362:False:True;5.31914893617021:False:True;29.2553191489362:False:True;14.3617021276596:False:True;</value>
        '                             </data>
        C1Sizer1.Grid.Columns(0).Size = 55
        C1Sizer1.Grid.Columns(1).Size = 115
        C1Sizer1.Grid.Columns(2).Size = 25
        C1Sizer1.Grid.Columns(3).Size = 110
        C1Sizer1.Grid.Columns(4).Size = 54
        Me.Top = (My.Computer.Screen.WorkingArea.Height \ 2) - (Me.Height \ 2)
        Me.Left = (My.Computer.Screen.WorkingArea.Width \ 2) - (Me.Width \ 2)
        'Dim ll As New Label()
        'll.Location = New Point(549, 1)
        'll.Size = New Size(379, 52)
        ctrlBtnDbConnection.Size = New Size(134, 31)
        ctrlBtnAutoUpdate.Visible = True
        If ctrlBtnAutoUpdate.Visible Then
            ctrlBtnDbConnection.Location = New Point(664, 12)
            ctrlBtnAutoUpdate.Location = New Point(812, 12)
        Else
            ctrlBtnDbConnection.Location = New Point(735, 12)
        End If
        lblMessage.Visible = False
        ctrlBtnDbConnection.Text = ""
        ctrlBtnDbConnection.BackColor = Color.White
        ctrlBtnDbConnection.Image = Global.Spectrum.My.Resources.Resources.TestConnection
        ctrlBtnAutoUpdate.Size = New Size(124, 31)
        ctrlBtnAutoUpdate.Text = ""
        ctrlBtnAutoUpdate.BackColor = Color.White
        ctrlBtnAutoUpdate.Image = Global.Spectrum.My.Resources.Resources.AutoUpdate
        lblUserId.Size = New Size(79, 13)
        lblPassword.Size = New Size(66, 13)
        lblID.Size = New Size(58, 13)
        Dim l1 As New Label
        l1.Text = "Language"
        l1.BackColor = Color.Transparent
        lblSite.Size = New Size(62, 13)
        l1.Size = New Size(34, 13)
        lblUserId.Location = New Point(77, 45)
        lblUserId.Text = "User Name:"
        lblPassword.Location = New Point(77, 90)
        lblID.Location = New Point(77, 135)
        lblSite.Location = New Point(77, 180)
        l1.Location = New Point(77, 225)
        C1Sizer1.Controls.Add(l1)
        txtusername.MaximumSize = New Size(0, 0)
        pwduserpassword.MaximumSize = New Size(0, 0)
        cmbSite.MaximumSize = New Size(0, 22)
        txtusername.MaximumSize = New Size(0, 22)
        pwduserpassword.MaximumSize = New Size(0, 22)
        txtIDcard.MaximumSize = New Size(0, 22)
        cboLanguage.MaximumSize = New Size(0, 22)
        txtusername.Size = New Size(240, 25)
        pwduserpassword.Size = New Size(240, 25)

        txtIDcard.Size = New Size(240, 25)
        cmbSite.Size = New Size(240, 22)
        cboLanguage.Dock = DockStyle.None
        cboLanguage.Location = New Point(77, 245)
        cboLanguage.Size = New Size(240, 22)
        txtusername.Location = New Point(77, 65)
        pwduserpassword.Location = New Point(77, 110)
        txtIDcard.Location = New Point(77, 155)
        cmbSite.Location = New Point(77, 200)

        txtusername.BackColor = Color.White
        pwduserpassword.BackColor = Color.White
        cmbSite.BackColor = Color.White
        txtusername.BackColor = Color.White
        pwduserpassword.BackColor = Color.White
        txtIDcard.BackColor = Color.White
        cboLanguage.BackColor = Color.White
        cboLanguage.EditorBackColor = Color.White
        cboLanguage.Styles(0).BackColor = Color.White
        cboLanguage.Styles(1).BackColor = Color.White
        cboLanguage.Styles(2).BackColor = Color.White
        cboLanguage.Styles(3).BackColor = Color.White
        cboLanguage.Styles(4).BackColor = Color.White
        cboLanguage.BackColor = Color.White
        cboLanguage.Dock = DockStyle.None
        btnLogin.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnLogin.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnLogin.FlatStyle = FlatStyle.Flat
        btnLogin.TextAlign = ContentAlignment.MiddleCenter
        btnLogin.BackColor = Color.FromArgb(0, 107, 163)
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.TextAlign = ContentAlignment.MiddleCenter
        btnCancel.BackColor = Color.FromArgb(0, 107, 163)
        btnLogin.Location = New Point(55, 308)
        btnLogin.Size = New Size(110, 33)
        btnCancel.Location = New Point(185, 308)
        btnCancel.Size = New Size(110, 33)

        lblUserId.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        lblPassword.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        lblID.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        lblSite.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        l1.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        txtusername.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        txtIDcard.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        pwduserpassword.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        cmbSite.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        cboLanguage.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        cboLanguage.EditorFont = New Font("Neo Sans", 8, FontStyle.Bold)
        cboLanguage.ForeColor = Color.FromArgb(0, 107, 163)
        cboLanguage.EditorForeColor = Color.FromArgb(0, 107, 163)
        btnLogin.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        btnCancel.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        btnLogin.ForeColor = Color.White
        btnCancel.ForeColor = Color.White

        lblUserId.Text = lblUserId.Text.ToUpper
        lblID.Text = lblID.Text.ToUpper
        lblPassword.Text = lblPassword.Text.ToUpper
        l1.Text = l1.Text.ToUpper
        lblSite.Text = lblSite.Text.ToUpper
        Dim rect As New System.Drawing.RectangleF(20, 10, 83, 25)
        Dim mypath As System.Drawing.Drawing2D.GraphicsPath
        txtusername.TabIndex = 0
        pwduserpassword.TabIndex = 1
        txtIDcard.TabIndex = 2
        cmbSite.TabIndex = 3
        cboLanguage.TabIndex = 4
        btnLogin.TabIndex = 5
        btnCancel.TabIndex = 6
        ctrlBtnDbConnection.TabIndex = 7
        ctrlBtnAutoUpdate.TabIndex = 8
    End Function

End Class
