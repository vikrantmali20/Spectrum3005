Imports System.Globalization
Imports SpectrumBL
Imports Spectrum
Imports System.Data.SqlClient

Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Class MyApplication

        'Startup: Raised when the application starts, before the startup form is created.
        'Shutdown: Raised after all application forms are closed.  This event is not raised if the application is terminating abnormally.
        'UnhandledException: Raised if the application encounters an unhandled exception.
        'StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
        'NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.

        ''' <summary>
        ''' Update the network connectivity status on the main form each time the connection status changes.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub MyApplication_NetworkAvailabilityChanged(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.Devices.NetworkAvailableEventArgs) Handles Me.NetworkAvailabilityChanged
            'SetConnectionStatus(e.IsNetworkAvailable)

        End Sub

        Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown
            'update the user-logout entry 
            Dim sqlcon As New SqlConnection
            sqlcon.ConnectionString = DataBaseConnection.ConString
            Try
                If sqlcon.State = ConnectionState.Closed Then
                    sqlcon.Open()
                End If

                Dim cmd As New SqlCommand("Update UserLoginHistory set LogoutDatetime = getdate() where LogoutDatetime = NULL AND USERID = '" & clsAdmin.UserCode & "'", sqlcon)
                If cmd.ExecuteNonQuery() > 0 Then
                End If
                sqlcon.Close()
            Catch

            End Try
        End Sub

        'Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown
        '    If MsgBox("Do you want to Close  Spectrum Front Office Application", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '    Else

        '    End If
        'End Sub


        ''' <summary>
        ''' Include logic here that should be performed before any forms are loaded
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            ' When the application starts, set the connection status on the status strip
            'gActiveLangId = "en-US"
            'gCI = New CultureInfo(gActiveLangId.ToString)
            ''Try
            ''    gResourceMgr = System.Resources.ResourceManager.CreateFileBasedResourceManager("resource", cResourcePath & "\MyResource", Nothing)
            ''Catch ex As Exception
            ''    MsgBox(ex.Message & vbLf & " check the Resource file")
            ''End Try
            Try
                Dim obj As Object = DirectCast(My.Application.Log, Microsoft.VisualBasic.Logging.Log).DefaultFileLogWriter
                obj.customlocation = cResourcePath

                SetConnectionStatus(My.Computer.Network.IsAvailable)
                clsAdmin.CultureInfo = "en-US"
                clsAdmin.LangCode = "EN"
                SetCurrentculture(clsAdmin.CultureInfo)
            Catch ex As Exception
                LogException(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Update the status strip to denote the current network connection status
        ''' </summary>
        ''' <param name="connected"></param>
        ''' <remarks></remarks>
        Public Sub SetConnectionStatus(ByVal connected As Boolean)

            Try
                Dim objFrm As CtrlRbnBaseForm
                For FrmCnt As Integer = 1 To OpenForms.Count
                    objFrm = OpenForms(FrmCnt)
                    Dim frmName As String = ""
                    frmName = UCase(objFrm.Name)
                    If frmName = "FRMCASHMEMO" Or frmName = "FRMNSALESORDERCREATION" Or _
                       frmName = "FRMNSALESORDERCANCEL" Or frmName = "FRMNSALESORDERUPDATE" Or _
                       frmName = "FRMNBIRTHLISTSALES" Or frmName = "FRMNBIRTHLISTUPDATE" Or _
                       frmName = "FRMNBIRTHLISTCREATE" Or frmName = "FRMNBIRTHLISTUPDATE" Then
                        objFrm.IsNetWorkConnected = connected

                    End If
                Next
            Catch ex As Exception

            End Try

        End Sub

        Private Sub MyApplication_StartupNextInstance(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            '  MsgBox(" Application is Already Running ")
            My.Application.MainForm.Show()
        End Sub
        Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            Try
                e.ExitApplication = True
                My.Application.Log.WriteEntry(e.Exception.Message, TraceEventType.Error)
                My.Application.Log.WriteEntry(e.Exception.TargetSite.ToString(), TraceEventType.Error)

                LogException(e.Exception)
            Catch ex As Exception
                LogException(ex)
            End Try
        End Sub
    End Class

End Namespace

