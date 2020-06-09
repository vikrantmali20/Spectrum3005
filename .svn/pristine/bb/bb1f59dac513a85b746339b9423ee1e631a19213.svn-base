
Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.AccessControl
Imports System.Net

Public Class clsLogin
    Inherits clsCommon
#Region "Private Variable"
    Dim iCurrencyCode As String
    Dim objclAcceptpayment As New clsAcceptPayment
    'Change
    Private Shared _appPath As String
    Public Shared Property AppStartUpPath() As String
        Get
            Return _appPath
        End Get
        Set(ByVal value As String)
            _appPath = value
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
    'End of change

#End Region

#Region "Public Function"

    Public Function TrackUserInformation(ByVal SiteCode As String, ByRef CurrencyCode As String, ByRef EanType As String) As Boolean
        iCurrencyCode = objclAcceptpayment.GetLocalCurrency(SiteCode, EanType)
        'GetCurrencyInfo()
        CurrencyCode = iCurrencyCode
    End Function

    Public Function GetLastDayCloseDate(ByVal finYear As String) As DataTable
        Try
            Dim query As String
            query = "select MAX (OPenDate)  As LastClosedDate from DayOpenNClose where DayCloseStatus = 1 and FinYear = '" & finYear & "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''Added by nikhil to get Public IP Address
    Public Function getIPAddress() As String
        Try
            Dim Addr = "nslookup myip.opendns.com. resolver1.opendns.com"
            Dim procStartInfo As System.Diagnostics.ProcessStartInfo = New ProcessStartInfo("cmd", "/c " + Addr)
            procStartInfo.RedirectStandardOutput = True
            procStartInfo.UseShellExecute = False
            procStartInfo.CreateNoWindow = True
            Using process As New Process()
                process.StartInfo = procStartInfo
                process.Start()
                process.WaitForExit()
                Dim IpAddress As String = process.StandardOutput.ReadToEnd()
                IpAddress = IpAddress.Replace("Server:  resolver1.opendns.com" & vbCrLf & "Address:  208.67.222.222" & vbCrLf & "" & vbCrLf & "Name:    myip.opendns.com" & vbCrLf & "Address: ", "").Replace("& vbCrLf & "" & vbCrLf &", "").ToString()
                Return IpAddress
            End Using
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    ''' <summary>
    ''' creaded by khusrao adil
    ''' for system date insertion of opent date when there is no record in table
    ''' </summary>
    ''' <param name="siteCode"></param>
    ''' <param name="finYear"></param>
    ''' <param name="openDate"></param>
    ''' <param name="createdBy"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertDayOpenDetail(ByVal siteCode As String, ByVal finYear As String, ByVal openDate As Date, ByVal createdBy As String) As Boolean
        Try
            Dim query As String
            query = "Insert into  DayOpenNClose (SiteCode, FinYear, OpenDate, DayCloseStatus, CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON, STATUS, ClosedAt) values ('" & siteCode & "','" & finYear & "','" & openDate.ToString("yyyy-MM-dd") & "',0,'" & siteCode & "','" & createdBy & "','" & GetCurrentDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "','" & siteCode & "','" & createdBy & "','" & GetCurrentDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "',1, NULL)"
            OpenConnection()
            Dim cmd As New SqlCommand
            cmd.CommandText = query
            cmd.Connection = SpectrumCon()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function
    Public Function InsertDayOpenDetails(ByVal siteCode As String, ByVal finYear As String, ByVal openDate As Date, ByVal createdBy As String, ByVal isPettyCashApplicable As Boolean) As Boolean
        Try
            Dim query As String
            query = "Insert into  DayOpenNClose (SiteCode, FinYear, OpenDate, DayCloseStatus, CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON, STATUS, ClosedAt) values ('" & siteCode & "','" & finYear & "','" & openDate.ToString("yyyy-MM-dd") & "',0,'" & siteCode & "','" & createdBy & "','" & GetCurrentDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "','" & siteCode & "','" & createdBy & "','" & GetCurrentDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "',1, NULL)"
            OpenConnection()
            Dim cmd As New SqlCommand
            cmd.CommandText = query
            cmd.Connection = SpectrumCon()
            cmd.ExecuteNonQuery()
            If isPettyCashApplicable Then
                Dim openingBalance As Decimal = 0
                query = "select top(1) ClosingBalance from ExpenseSummary where Sitecode='" & siteCode & "' and FinYear = '" & finYear & "' order by Date desc"
                Dim dt As DataTable = GetFilledTable(query)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    openingBalance = dt.Rows(0)(0)
                End If
                query = "Insert Into ExpenseSummary (Sitecode,FinYear,Date,OpeningBal,TotalExpense,TotalIncome, " & _
                        "TotalSales,ClosingBalance,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS) values " & _
                        "('" & siteCode & "','" & finYear & "','" & openDate.ToString("yyyy-MM-dd") & "'," & openingBalance & ",0,0,0,0,'" & siteCode & "','" & createdBy & "',getdate(),'" & siteCode & "','" & createdBy & "',getdate(),1)"
                InsertOrUpdateRecord(query)
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    ''added by nikhil for Hari OM
    Public Function UpdateDayOpenDetailsForHariOm(ByVal SiteCode As String, ByVal openDate As Date) As Boolean
        Try
            Dim query As String
            query = "UPDATE  DAYOPENNCLOSE SET DAYCLOSESTATUS ='0' WHERE OPENDATE='" & openDate.ToString("yyyy-MM-dd") & "' AND STATUS = 1 "
            OpenConnection()
            Dim cmd As New SqlCommand
            cmd.CommandText = query
            cmd.Connection = SpectrumCon()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function ValidateID(ByVal strUserId As String, ByVal pwd As String, ByVal IdNum As String, ByRef ErrorMsg As String, ByRef userid As String) As Boolean
        Try
            'Changed by Rohit for CR 6001
            'Changed by Mahesh forcefully validate only to NumId textbox ....instruction by Rakesh Sir .

            Dim strId As String = String.Empty


            'If strUserId <> String.Empty Then
            '    strId = strUserId
            'ElseIf pwd <> String.Empty Then
            '    strId = pwd
            'Else
            If IdNum <> String.Empty Then
                strId = IdNum
            End If

            If (strUserId.Length > 0) Then

                If (strUserId.IndexOf("%") > -1 AndAlso strUserId.IndexOf("?") > 0) Then
                    strId = strUserId.Substring(1, strUserId.Length - 2)
                End If

                If (strId.IndexOf("^") > 0) Then
                    strId = strId.Substring(1, strId.IndexOf("^") - 1)
                End If
            End If

            If IdNum <> String.Empty Then
                Dim daUser As New SqlDataAdapter("SELECT UserID, Password, Designation,Createdby,PasswordExpiredon FROM AuthUsers where IDNumber='" & strId & "'", ConString)
                Dim dt As New DataTable
                daUser.Fill(dt)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    If Not dt.Rows(0)("PasswordExpiredon") Is DBNull.Value AndAlso dt.Rows(0)("PasswordExpiredon") >= Now Then
                        userid = dt.Rows(0)("UserID").ToString()

                        ErrorMsg = getValueByKey("LG011")
                        Return True
                    Else
                        ErrorMsg = getValueByKey("LG010")
                        Return False
                    End If
                Else
                    'ErrorMsg = getValueByKey("LG012")
                    Return False
                End If
            End If
        Catch ex As Exception
            If ex.Message = "Operator '>=' is not defined for type 'DBNull' and type 'Date'." Then
                ErrorMsg = getValueByKey("LG013")
            End If
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function GetCurrencyInfo(ByRef CurrencyDescription As String, ByRef CurrencySymbol As String, ByVal Currencycode As String) As Boolean
        Dim drLC As SqlDataReader
        Try
            OpenConnection()
            Dim sqlCmdLocalCurrency As New SqlCommand("select currencycode,currencyDescription,CurrencySymbol from MstCurrency where currencycode='" & Currencycode & "'", SpectrumCon)
            drLC = sqlCmdLocalCurrency.ExecuteReader()
            If (drLC.Read()) Then
                If Not (drLC.IsDBNull(0)) Then
                    CurrencyDescription = CStr(drLC.GetString(1))
                    CurrencySymbol = CStr(drLC.GetString(2))
                End If
            End If
            Return True
        Catch ex As Exception
            LogException(ex)

            Return False
        Finally
            drLC.Close()
            CloseConnection()
        End Try

    End Function
    ''' <summary>
    '''  Matching  username ant it's password 
    ''' </summary>
    ''' <param name="userName"></param>
    ''' <param name="password"></param>
    ''' <param name="errorMsg"></param>
    ''' <returns>If match return true otherwise false</returns>
    ''' <remarks></remarks>
    Public Function CheckAuthorizeUser(ByVal userName As String, ByVal password As String, ByRef errorMsg As String, ByRef dbPassword As String, ByVal strSiteCode As String, Optional ByRef isIdNumber As Boolean = False, Optional ByRef strUserid As String = "", Optional ByRef strUserName As String = "") As Boolean
        Dim sqlDataReader As SqlDataReader

        If OpenConnection() <> "" Then
            Exit Function
        End If

        Try
            '' Dim sqlCmd As New SqlCommand("SELECT top 1 A.UserID,A.UserName, A.[Password],A.IDNumber, A.Designation,A.CREATEDBY,A.PasswordExpiredon FROM AuthUsers A inner join AuthUserSiteRoleMap b on a.UserID=b.UserID  where B.SiteCode = '" & strSiteCode & "' AND (A.[UserID]='" & userName & "' OR A.[IDNumber] = '" & userName & "' OR A.[IDNumber] = '" & password & "')", SpectrumCon)
            Dim sqlCmd As New SqlCommand("select top 1 a.UserID,a.UserName,a.Password,a.IDNumber,a.Designation,a.CREATEDBY,a.PasswordExpiredon from  AuthUsers a inner join AuthUserSiteRoleMap b  on a.UserID=b.UserID where b.SiteCode='" & strSiteCode & "' and a.UserID='" & userName & "'", SpectrumCon)
            sqlDataReader = sqlCmd.ExecuteReader()
            If (sqlDataReader.Read()) Then
                If IsDBNull(sqlDataReader.Item("IDNumber")) = False Then
                    If sqlDataReader.Item("IDNumber") = userName Or sqlDataReader.Item("IDNumber") = password Then
                        isIdNumber = True
                        strUserid = sqlDataReader.Item("UserID")
                        strUserName = sqlDataReader.Item("UserName")
                    Else
                        strUserName = sqlDataReader.Item("UserName")
                    End If
                Else
                    strUserName = sqlDataReader.Item("UserName")
                End If

                If IsDBNull(sqlDataReader.Item("PasswordExpiredon")) = False Then
                    'If Format(sqlDataReader.Item("PasswordExpiredon"), "dd/MM/yyyy") >= Format(Now.Date, "dd/MM/yyyy") Then
                    dbPassword = sqlDataReader.Item("Password")
                    errorMsg = getValueByKey("LG011")
                    Return True
                    'Else
                    '    errorMsg = "Password is expired"
                    '    Return False
                    'End If
                Else
                    errorMsg = getValueByKey("LG010")
                    Return False
                End If
            Else
                errorMsg = getValueByKey("LG012")
                Return False
            End If
            sqlDataReader.Close()
        Catch ex As Exception
            If Not sqlDataReader.IsClosed Then
                sqlDataReader.Close()
            End If
            If ex.Message = "Operator '>=' is not defined for type 'DBNull' and type 'Date'." Then
                errorMsg = getValueByKey("LG013")
            End If
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function CheckUserNameOrPassword(ByVal strUserName As String, ByVal strPassword As String, ByRef errorMsg As String) As Boolean
        Dim sqlDataReader As SqlDataReader
        Dim blnIsUserNameFound As Boolean
        Dim blnIsPasswordMatch As Boolean
        Try
            OpenConnection()
            Dim sqlCmdUserName As New SqlCommand("SELECT [UserID] FROM [AuthUsers] where [UserID]='" + strUserName + "'", SpectrumCon)
            sqlDataReader = sqlCmdUserName.ExecuteReader()
            If Not (sqlDataReader.Read()) Then
                errorMsg = getValueByKey("LG012")
                blnIsUserNameFound = False
                If Not sqlDataReader.IsClosed Then
                    sqlDataReader.Close()
                End If
                Dim sqlCmdPassword As New SqlCommand("SELECT [Password] FROM [AuthUsers] where [Password]='" + strPassword + "'", SpectrumCon)
                sqlDataReader = sqlCmdPassword.ExecuteReader()
                If Not (sqlDataReader.Read()) Then
                    errorMsg = getValueByKey("LG004")
                    blnIsPasswordMatch = False
                Else
                    blnIsPasswordMatch = True
                End If
            Else
                blnIsUserNameFound = True
                If Not sqlDataReader.IsClosed Then
                    sqlDataReader.Close()
                End If
                Dim sqlCmdPassword As New SqlCommand("SELECT [Password] FROM [AuthUsers] where [Password]='" + strPassword + "'", SpectrumCon)
                sqlDataReader = sqlCmdPassword.ExecuteReader()
                If Not (sqlDataReader.Read()) Then
                    errorMsg = getValueByKey("LG004")
                    blnIsPasswordMatch = False
                Else
                    blnIsPasswordMatch = True
                End If
            End If
            If Not (blnIsUserNameFound) Then
                If Not (blnIsPasswordMatch) Then
                    errorMsg = getValueByKey("LG014")
                End If
            End If
            'Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            If Not sqlDataReader.IsClosed Then
                sqlDataReader.Close()
            End If
            CloseConnection()
        End Try
    End Function

    Public Function checkUserstatus(ByVal userid As String, ByRef msg As String, ByVal mycurrTerminal As String, Optional ByVal TillMsgCLL001 As String = "") As Boolean
        Try
            Dim daUser As New SqlDataAdapter("SELECT * FROM USERLOGINHISTORY WHERE USERID='" & userid & "' AND CONVERT(VARCHAR(10),LOGINDATETIME,105)=CONVERT(VARCHAR(10), GETDATE(), 105)  AND  LOGOUTDATETIME IS NULL  And TerminalID <> 'CCE' ", ConString)
            Dim dtUser As New DataTable
            daUser.Fill(dtUser)
            If dtUser.Rows.Count > 0 Then
                If dtUser.Rows(0)("TerminalId").ToString() <> mycurrTerminal Then ' if same user is logged on someother terminal then
                    'msg = "User is already login in " & dtUser.Rows(0)("TerminalId").ToString() & " Terminal. Still you want to Login"
                    'Try
                    'getValueByKey("CLL001")
                    msg = String.Format(TillMsgCLL001, dtUser.Rows(0)("TerminalId").ToString())
                    'Catch ex As Exception

                    If MsgBox(msg, MsgBoxStyle.YesNo, getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return True
                End If
            Else
                Return True
            End If
        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' TODO: User login to System and When logout from system.
    ''' </summary>
    ''' <param name="loginID">UserName</param>    
    ''' <param name="loginStatus">If userLogin then pass 'True' otherwise 'False' </param>
    ''' <returns>Insertion successfull or not</returns>
    ''' <remarks></remarks>
    '''
    Public Function InsertLoginHistory(ByVal loginID As String, ByVal Sitecode As String, ByVal TerminalID As String, ByVal loginStatus As Boolean, ByVal IpAddress As String, Optional ByRef UserAgent As String = "") As Boolean
        Try
            OpenConnection()
            Dim cmd As New SqlCommand
            If (loginStatus) Then
                cmd.CommandText = "Insert into UserLoginHistory([UserID],[SiteCode],[TerminalID],[LoginDateTime],IPAddress,LoginAt,UserAgent,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS) values('" & loginID & "','" & Sitecode & "','" & TerminalID & "',getdate(),'" & IpAddress & "','" & "FO" & "','" & UserAgent & "','" & Sitecode & "','" & loginID & "',getdate(),'" & Sitecode & "','" & loginID & "',getdate(),1)"
                cmd.Connection = SpectrumCon()
            Else
                'Logout Time
                cmd.CommandText = "Update UserLoginHistory set LogoutDateTime=getdate() Where UserID='" & loginID & "' and SiteCode='" & Sitecode & "' And TerminalID='" & TerminalID & "' And CONVERT(VARCHAR(10),LOGINDATETIME,105)=CONVERT(VARCHAR(10), GETDATE(), 105) AND LogoutDateTime is null "
                cmd.Connection = SpectrumCon()
            End If
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function
    ''' <summary>
    ''' Set Sqlconnection
    ''' </summary>
    ''' <param name="Strcon">ConnectionString</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Function SetConnection(ByVal Strcon As String) As Boolean
        Try
            'Change for saving App Settings to a file
            'Commenting the line below 
            'My.Settings.POSDBConnectionString = Strcon
            CreateSpectrumParamFile("POSDBConnectionString", Strcon)
            DataBaseConnection.ConString = Strcon
            ConString = Strcon
            'End of change

            'ConString = DataBaseConnection.ConnectionString()
            DataBaseConnection.SpectrumCon()
            My.Settings.Save()
            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function GetLanguages(ByVal SiteCode As String, ByRef Errormsg As String, ByRef DefLang As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim daLang As New SqlDataAdapter("SELECT  A.LANGUAGECODE, A.LANGUAGENAME, A.CULTURE,B.LANGUAGECODE AS DEFAULTLANG FROM MSTLANGUAGE A Inner JOIN SiteLanguageMap C on A.LanguageCode=C.LocalLanguageCode Inner join MSTSITE B On C.SiteCode=B.SiteCode WHERE B.SITECODE='" & SiteCode & "' AND A.STATUS = 1 AND  c.STATUS = 1", ConString)
            daLang.Fill(dt)
            Dim dv As New DataView(dt, "LANGUAGECODE=DEFAULTLANG", "", DataViewRowState.CurrentRows)
            If dv.Count > 0 Then
                DefLang = dv(0)("CULTURE").ToString()
            End If
            Return dt
        Catch ex As Exception
            Errormsg = ex.Message
            LogException(ex)
            Return Nothing
        End Try
    End Function
    '---Checking Site Active Status added by prasad
    Public Function CheckSiteActiveStatus(ByVal sitecode As String) As Boolean
        Try
            Dim dt As New DataTable
            Dim daLang As New SqlDataAdapter("select *from mstsite where SiteCode='" & sitecode & "' AND IsActive=1 and STATUS=1", ConString)
            daLang.Fill(dt)
            Return IIf(Not dt Is Nothing AndAlso dt.Rows.Count > 0, True, False)

        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
#End Region

#Region " PrivateFunction"
    ''' <summary>
    ''' TODO:Check user password ExpireDate
    ''' </summary>
    ''' <param name="userName">UserID</param>
    ''' <param name="password">user password</param>
    ''' <returns>if password expire return true otherwise false</returns>
    ''' <remarks></remarks>
    Public Function IsUserPasswordExpire(ByVal userName As String, ByVal password As String) As Boolean
        Dim dr As SqlDataReader
        Try
            OpenConnection()
            Dim expiryDate As Date
            Dim currentDate As Date
            Dim cmd1 As New SqlCommand("SELECT getDate()", SpectrumCon)
            Dim dr1 As SqlDataReader
            dr1 = cmd1.ExecuteReader()
            If (dr1.Read()) Then
                If Not (dr1.IsDBNull(0)) Then
                    currentDate = dr1.GetDateTime(0)
                End If
            End If
            dr1.Close()
            cmd1.Dispose()
            Dim cmd As New SqlCommand("SELECT [UserID],[PasswordExpiredon] FROM [AuthUsers] where [UserID]='" + userName + "'", SpectrumCon)
            dr = cmd.ExecuteReader()
            If (dr.Read()) Then
                If Not (dr.IsDBNull(1)) Then
                    expiryDate = dr.GetDateTime(1)
                    If (expiryDate > currentDate) Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return True
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            Return True
        Finally
            dr.Close()
            CloseConnection()
        End Try
    End Function

    Public Function pDbConnTest() As Boolean
        Try
            If SpectrumCon.State = ConnectionState.Open Then
                SpectrumCon.Close()
            End If
            SpectrumCon.Open()
            SpectrumCon.Close()
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    'vipin 23.10.2018 connection to CCE server
    'Public Function pDbConnTestCCE() As Boolean
    '    Try
    '        If SpectrumConCCE.State = ConnectionState.Open Then
    '            SpectrumConCCE.Close()
    '        End If
    '        SpectrumConCCE.Open()
    '        SpectrumConCCE.Close()
    '        Return True
    '    Catch ex As Exception
    '        LogException(ex)
    '        Return False
    '    End Try
    'End Function

#End Region

    'Change for saving App Settings to common file
    'Public Shared Sub CreateLocalParaFile(ByVal strLabel As String, ByVal strValue As String)
    '    ' Dim permission As New System.Security.Permissions.FileIOPermission( _
    '    'System.Security.Permissions.FileIOPermissionAccess.Write, _
    '    'AppStartUpPath & "\SpectrumParam")

    '    ' Try
    '    '     permission.Demand()
    '    ' Catch ex As System.Security.SecurityException
    '    '     Dim a As New ApplicationException("Permission to save the file was denied, " & _
    '    '        "and the bitmap was not saved.")
    '    '     ' Let the user know the save won't work. 
    '    '     LogException(a)
    '    '     LogException(ex)
    '    ' Catch ex As System.Exception
    '    '     ' React to other exceptions here.
    '    '     '           MessageBox.Show("Other error.")
    '    '     LogException(ex)
    '    ' End Try
    '    Try
    '        Dim isFound As Boolean = False
    '        'If File.Exists(Application.StartupPath & "\SpectrumParam") Then

    '        If File.Exists(AppStartUpPath & "\SpectrumParam") Then
    '            Dim dtData As New DataSet
    '            dtData.ReadXml(AppStartUpPath & "\SpectrumParam")
    '            For i As Integer = 1 To dtData.Tables(0).Rows.Count
    '                If UCase(dtData.Tables(0).Rows(i - 1)("Label")) = UCase(strLabel) Then
    '                    isFound = True
    '                    dtData.Tables(0).Rows(i - 1)("Value") = Decrypt(strValue)
    '                    dtData.AcceptChanges()
    '                    dtData.WriteXml(AppStartUpPath & "\SpectrumParam", XmlWriteMode.DiffGram)
    '                    dtData.AcceptChanges()
    '                    dtData.WriteXml(AppStartUpPath & "\SpectrumParam", XmlWriteMode.IgnoreSchema)
    '                    Exit For
    '                End If
    '            Next
    '            If isFound = False Then
    '                Dim dr As DataRow = dtData.Tables(0).NewRow
    '                dr("Label") = strLabel
    '                dr("Value") = Decrypt(strValue)
    '                dtData.Tables(0).Rows.Add(dr)
    '                dtData.AcceptChanges()
    '                dtData.WriteXml(AppStartUpPath & "\SpectrumParam", XmlWriteMode.DiffGram)
    '                dtData.AcceptChanges()
    '                dtData.WriteXml(AppStartUpPath & "\SpectrumParam", XmlWriteMode.WriteSchema)
    '            End If

    '        Else
    '            Dim dtTable As New DataTable("SpectrumParam")
    '            dtTable.Columns.Add("Label", Type.GetType("System.String"))
    '            dtTable.Columns.Add("Value", Type.GetType("System.String"))
    '            Dim dr As DataRow = dtTable.NewRow
    '            dr("Label") = strLabel
    '            dr("Value") = Decrypt(strValue)
    '            dtTable.Rows.Add(dr)
    '            dtTable.WriteXml(AppStartUpPath & "\SpectrumParam", XmlWriteMode.WriteSchema)
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'End of change

    Public Shared Sub CreateSpectrumParamFile(ByVal strLabel As String, ByVal strValue As String)
        ' Dim permission As New System.Security.Permissions.FileIOPermission( _
        'System.Security.Permissions.FileIOPermissionAccess.Write, _
        'AppStartUpPath & "\SpectrumParam")

        ' Try
        '     permission.Demand()
        ' Catch ex As System.Security.SecurityException
        '     Dim a As New ApplicationException("Permission to save the file was denied, " & _
        '        "and the bitmap was not saved.")
        '     ' Let the user know the save won't work. 
        '     LogException(a)
        '     LogException(ex)
        ' Catch ex As System.Exception
        '     ' React to other exceptions here.
        '     '           MessageBox.Show("Other error.")
        '     LogException(ex)
        ' End Try
        Try
            Dim isFound As Boolean = False
            'If File.Exists(Application.StartupPath & "\SpectrumParam") Then

            If File.Exists(AppStartUpPath & "\SpectrumParam") Then
                Dim dtData As New DataSet
                dtData.ReadXml(AppStartUpPath & "\SpectrumParam")
                For i As Integer = 1 To dtData.Tables(0).Rows.Count
                    If UCase(dtData.Tables(0).Rows(i - 1)("Label")) = UCase(strLabel) Then
                        isFound = True
                        dtData.Tables(0).Rows(i - 1)("Value") = Decrypt(strValue)
                        dtData.AcceptChanges()
                        dtData.WriteXml(AppStartUpPath & "\SpectrumParam", XmlWriteMode.DiffGram)
                        dtData.AcceptChanges()
                        dtData.WriteXml(AppStartUpPath & "\SpectrumParam", XmlWriteMode.IgnoreSchema)
                        Exit For
                    End If
                Next
                If isFound = False Then
                    Dim dr As DataRow = dtData.Tables(0).NewRow
                    dr("Label") = strLabel
                    dr("Value") = Decrypt(strValue)
                    dtData.Tables(0).Rows.Add(dr)
                    dtData.AcceptChanges()
                    dtData.WriteXml(AppStartUpPath & "\SpectrumParam", XmlWriteMode.DiffGram)
                    dtData.AcceptChanges()
                    dtData.WriteXml(AppStartUpPath & "\SpectrumParam", XmlWriteMode.WriteSchema)
                End If

            Else
                Dim dtTable As New DataTable("SpectrumParam")
                dtTable.Columns.Add("Label", Type.GetType("System.String"))
                dtTable.Columns.Add("Value", Type.GetType("System.String"))
                Dim dr As DataRow = dtTable.NewRow
                dr("Label") = strLabel
                dr("Value") = Decrypt(strValue)
                dtTable.Rows.Add(dr)
                dtTable.WriteXml(AppStartUpPath & "\SpectrumParam", XmlWriteMode.WriteSchema)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

        Try
            Call CreateLocalParaFile(strLabel, strValue)
        Catch ex As Exception

        End Try
    End Sub

    'Change for saving App Settings to common file
    Public Shared Sub CreateLocalParaFile(ByVal strLabel As String, ByVal strValue As String)
        ' Dim permission As New System.Security.Permissions.FileIOPermission( _
        'System.Security.Permissions.FileIOPermissionAccess.Write, _
        'AppStartUpPath & "\SpectrumParam")

        ' Try
        '     permission.Demand()
        ' Catch ex As System.Security.SecurityException
        '     Dim a As New ApplicationException("Permission to save the file was denied, " & _
        '        "and the bitmap was not saved.")
        '     ' Let the user know the save won't work. 
        '     LogException(a)
        '     LogException(ex)
        ' Catch ex As System.Exception
        '     ' React to other exceptions here.
        '     '           MessageBox.Show("Other error.")
        '     LogException(ex)
        ' End Try
        Try
            Dim isFound As Boolean = False
            'If File.Exists(Application.StartupPath & "\SpectrumParam") Then

            If File.Exists(AppStartUpPath & "\LocalPara") Then
                Dim dtData As New DataSet
                dtData.ReadXml(AppStartUpPath & "\LocalPara")
                For i As Integer = 1 To dtData.Tables(0).Rows.Count
                    If UCase(dtData.Tables(0).Rows(i - 1)("Label")) = UCase(strLabel) Then
                        isFound = True
                        dtData.Tables(0).Rows(i - 1)("Value") = Decrypt(strValue)
                        dtData.AcceptChanges()
                        dtData.WriteXml(AppStartUpPath & "\LocalPara", XmlWriteMode.DiffGram)
                        dtData.AcceptChanges()
                        dtData.WriteXml(AppStartUpPath & "\LocalPara", XmlWriteMode.IgnoreSchema)
                        Exit For
                    End If
                Next
                If isFound = False Then
                    Dim dr As DataRow = dtData.Tables(0).NewRow
                    dr("Label") = strLabel
                    dr("Value") = Decrypt(strValue)
                    dtData.Tables(0).Rows.Add(dr)
                    dtData.AcceptChanges()
                    dtData.WriteXml(AppStartUpPath & "\LocalPara", XmlWriteMode.DiffGram)
                    dtData.AcceptChanges()
                    dtData.WriteXml(AppStartUpPath & "\LocalPara", XmlWriteMode.WriteSchema)
                End If

            Else
                Dim dtTable As New DataTable("LocalPara")
                dtTable.Columns.Add("Label", Type.GetType("System.String"))
                dtTable.Columns.Add("Value", Type.GetType("System.String"))
                Dim dr As DataRow = dtTable.NewRow
                dr("Label") = strLabel
                dr("Value") = Decrypt(strValue)
                dtTable.Rows.Add(dr)
                dtTable.WriteXml(AppStartUpPath & "\LocalPara", XmlWriteMode.WriteSchema)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    'End of change




    'Change for saving App Settings to common file
    Public Shared Function ReadSpectrumParamFile(ByVal strLabel As String) As String
        Dim strValue As String = ""
        Try
            If File.Exists(AppStartUpPath & "\SpectrumParam") Then
                Dim dtData As New DataSet
                dtData.ReadXml("SpectrumParam")
                For i As Integer = 0 To dtData.Tables(0).Rows.Count - 1
                    If UCase(dtData.Tables(0).Rows(i)("Label")) = UCase(strLabel) Then
                        strValue = Encrypt(dtData.Tables(0).Rows(i)("Value"))
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            strValue = ""
        End Try
        Return strValue
    End Function

    '----- Method Add By Mahesh Aim- Secure LocalPara File 
    'Public Shared Sub SetFilePermission(filePath As String, domainName As String, userName As String)
    '    Try
    '        Dim fi As New FileInfo(filePath)
    '        Dim fs As FileSecurity = fi.GetAccessControl()
    '        fs.SetAccessRuleProtection(True, False)
    '        Dim rules As AuthorizationRuleCollection = fs.GetAccessRules(True, True, GetType(System.Security.Principal.NTAccount))
    '        For Each rule As FileSystemAccessRule In rules
    '            fs.RemoveAccessRule(rule)
    '        Next
    '        fs.AddAccessRule(New FileSystemAccessRule(Convert.ToString(domainName) & "\" & Convert.ToString(userName), FileSystemRights.FullControl, AccessControlType.Deny))
    '        fs.AddAccessRule(New FileSystemAccessRule("Authenticated Users", FileSystemRights.Delete, AccessControlType.Deny))
    '        fs.AddAccessRule(New FileSystemAccessRule("test",FileSystemRights.Modify ,AccessControlType.Deny)
    '        File.SetAccessControl(filePath, fs)
    '    Catch ex As Exception

    '    End Try
    'End Sub
    ''----- Method Add By Mahesh Aim- Secure LocalPara File 
    'Public Shared Sub RemoveFilePermission(filePath As String, domainName As String, userName As String)
    '    Try
    '        Dim fi As New FileInfo(filePath)
    '        Dim fs As FileSecurity = fi.GetAccessControl()
    '        fs.SetAccessRuleProtection(True, False)
    '        Dim rules As AuthorizationRuleCollection = fs.GetAccessRules(True, True, GetType(System.Security.Principal.NTAccount))
    '        For Each rule As FileSystemAccessRule In rules
    '            fs.RemoveAccessRule(rule)
    '        Next
    '        fs.AddAccessRule(New FileSystemAccessRule(Convert.ToString(domainName) & "\" & Convert.ToString(userName), FileSystemRights.FullControl, AccessControlType.Deny))
    '        fs.AddAccessRule(New FileSystemAccessRule("Authenticated Users", FileSystemRights.Delete, AccessControlType.Deny))
    '        File.SetAccessControl(filePath, fs)
    '    Catch ex As Exception

    '    End Try
    'End Sub





    Public Function GetAllDefaultSite() As DataTable
        Try
            Dim dataAdapter As SqlDataAdapter
            Dim articles As New DataTable
            dataAdapter = New SqlDataAdapter("Exec getAllDefaultSite", SpectrumCon)
            dataAdapter.Fill(articles)
            Return articles
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function CheckForMultipleDayOpen(ByVal siteCode As String) As String
        Try
            Dim result As Boolean = False
            Dim query = "select COUNT (*) from DayOpenNClose where SiteCode = '" & siteCode & "' and DayCloseStatus = 0"
            Dim dt As DataTable = GetFilledTable(query)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If dt.Rows(0)(0) <= 1 Then
                    result = True
                End If
            End If
            Return result
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    'End of change

    Public Function CheckIfBatchMgtApplicable(ByVal SiteCode As String) As Boolean
        Dim query = "select isBatchApplicable  from mstsite where SiteCode='" & SiteCode & "'"
        Dim dt As DataTable = GetFilledTable(query)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            If dt(0)(0).ToString() = String.Empty Then
                Return False
            Else
                Return CBool(dt(0)(0).ToString())
            End If
        Else
            Return False
        End If
    End Function

    Public Function GetCSTTaxCode(ByVal SiteCode As String) As String
        Dim query = "SELECT * FROM MstSite WHERE SiteCode='" & SiteCode & "'"
        Dim dtSite As DataTable = GetFilledTable(query)

        If dtSite IsNot Nothing AndAlso dtSite.Rows.Count > 0 Then
            Return IIf(dtSite.Rows(0)("CentralSalesTaxNo") Is Nothing, String.Empty, dtSite.Rows(0)("CentralSalesTaxNo").ToString())
        End If
        Return String.Empty

    End Function

    Public Function GetDayCloseStatus(ByVal SiteCode As String, ByVal finYear As String) As DataTable
        Dim sqlQuery As New System.Text.StringBuilder

        sqlQuery.Append("SELECT (SELECT MAX(OpenDate) FROM DayOpenNClose WHERE SiteCode='" & SiteCode & "' AND FinYear = '" & finYear & "' AND DayCloseStatus = 1 ) AS LastClosedDate, " & vbCrLf)
        sqlQuery.Append("(SELECT MAX(OpenDate) FROM DayOpenNClose WHERE SiteCode='" & SiteCode & "' AND FinYear = '" & finYear & "' AND DayCloseStatus = 0) AS DayOpenDate, " & vbCrLf)
        sqlQuery.Append("(SELECT COUNT(TerminalID) FROM MstTerminalID WHERE SiteCode='" & SiteCode & "' AND OpenCloseStatus = 'Open') AS AnyTerminalOpen " & vbCrLf)

        Return GetFilledTable(sqlQuery.ToString())

    End Function
    Public Shared Function GetAutoUpdateFLdValue(ByVal SiteCode As String) As Boolean
        Try
            Dim dataTable As New DataTable
            Dim query As String = "SELECT FldValue FROM DefaultConfig WHERE   FldLabel ='AutoUpdate' And SiteCode='" & SiteCode & "'"
            Dim da As New SqlDataAdapter(query, ConString)
            da.Fill(dataTable)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                If dataTable.Rows(0)(0) = "True" Then
                    Return True
                Else
                    Return False
                End If
            End If
            Return False
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
End Class
