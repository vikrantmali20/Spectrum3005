Imports System.Data.SqlClient
Imports System.Resources
Public Class DataBaseConnection
    Public Shared resourceMgrBL As ResourceManager
    Private Shared _ConString As String
    Public Shared _OnlineConn As String
    Public Shared _OfflineConn As String
    Public Shared _CurrentStatus As Boolean
    Public Shared Spectrum As SqlConnection
    Public Shared MettlerSpectrum As SqlConnection
    Public Shared defaultOnlineConstring As String
    Public Shared Property ConString() As String
        Get
            If _CurrentStatus = False Then
                Try
                    Dim con As New SqlConnection(_OnlineConn)
                    If con.State <> ConnectionState.Open Then
                        con.Open()
                        con.Close()
                        _ConString = _OnlineConn
                        _CurrentStatus = True
                        If Not Spectrum Is Nothing AndAlso Spectrum.State <> ConnectionState.Open Then
                            Spectrum.ConnectionString = _OnlineConn
                        End If
                        Return _OnlineConn
                    End If
                Catch ex As Exception
                    _CurrentStatus = False
                    If Not Spectrum Is Nothing AndAlso Spectrum.State <> ConnectionState.Open Then
                        Spectrum.ConnectionString = _OfflineConn
                    End If

                    _ConString = _OfflineConn
                    Return _OfflineConn
                End Try
            Else
                Try
                    Dim con As New SqlConnection(_OnlineConn)
                    If con.State <> ConnectionState.Open Then
                        con.Open()
                        con.Close()
                        _ConString = _OnlineConn
                        If Not Spectrum Is Nothing AndAlso Spectrum.State <> ConnectionState.Open Then
                            Spectrum.ConnectionString = _OnlineConn
                        End If
                        _CurrentStatus = True
                        Return _OnlineConn
                    End If
                Catch ex As Exception
                    _CurrentStatus = False
                    _ConString = _OfflineConn
                    If Not Spectrum Is Nothing AndAlso Spectrum.State <> ConnectionState.Open Then
                        Spectrum.ConnectionString = _OfflineConn
                    End If

                    Return _OfflineConn
                End Try
            End If
        End Get
        Set(ByVal value As String)
            _ConString = value
        End Set
    End Property
    Public Shared Function SpectrumCon() As SqlConnection
        Try
            Spectrum = New SqlConnection(ConString)
            Return Spectrum
        Catch ex As Exception

        End Try
    End Function
    Public Shared Function MettlerSpectrumCon(ByVal MettConString As String) As SqlConnection
        Try
            MettlerSpectrum = New SqlConnection(MettConString)
            Return MettlerSpectrum
        Catch ex As Exception

        End Try
    End Function
    Public Shared Function ConnectionString() As String
        'ConString = My.Settings.POSDBConnectionString
        ConString = clsLogin.ReadSpectrumParamFile("POSDBConnectionString")
        Return ConString
    End Function
    Public Shared Function MettlerConnectionString() As String
        'ConString = My.Settings.POSDBConnectionString
        Dim MatConString As String = ""
        MatConString = clsLogin.ReadSpectrumParamFile("MettlerDBConnectionString")
        Return MatConString
    End Function
    Public Shared Function blMySettingConn() As String
        blMySettingConn = My.Settings.ConnectionString
    End Function
End Class
