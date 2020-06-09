Imports System.Data.SqlClient
Imports System.Data
Imports System.Resources
Module DBConnection

    'Public resourceMgrbl As ResourceManager = ResourceManager.CreateFileBasedResourceManager("MyResource", App.StartupPath & "\MyResource", Nothing)
    Public Property ConString() As String
        Get
            Return DataBaseConnection.ConString
        End Get
        Set(ByVal value As String)
            DataBaseConnection.ConString = value
        End Set
    End Property
    'Public ConString As String = DataBaseConnection.ConString
    'Public SpectrumCon As SqlConnection
    Public Function SpectrumCon() As SqlConnection
        If DataBaseConnection.Spectrum Is Nothing Then
            Return DataBaseConnection.SpectrumCon()
        End If
        If DataBaseConnection.Spectrum.State <> ConnectionState.Open Then
            Try
                DataBaseConnection.Spectrum.Open()
                DataBaseConnection.Spectrum.Close()
                Return DataBaseConnection.Spectrum

            Catch ex As Exception
                Return DataBaseConnection.SpectrumCon()
            End Try


        End If
        Return DataBaseConnection.Spectrum
    End Function

    Public Function OpenConnection() As String
        Try
            If SpectrumCon.State = ConnectionState.Closed Then
                SpectrumCon.Open()
            End If
            Return ""
        Catch ex As Exception
            Dim clscmn As New clsCommon
            MsgBox(clscmn.getValueByKey("BL098"), MsgBoxStyle.Critical, "BL098 - " & clscmn.getValueByKey("CLAE04"))

            'MsgBox(getValueByKey("BL098"), MsgBoxStyle.Critical, getValueByKey("CLAE04"))

            LogException(ex)
            ' Return "Error In Application. Check Database Connection"
            Return clscmn.getValueByKey("CLDBC02")
        Finally
        End Try
    End Function
    Public Function CloseConnection() As String
        Try
            If SpectrumCon.State = ConnectionState.Open Then
                SpectrumCon.Close()
            End If
            Return ""
        Catch ex As Exception
            LogException(ex)
            Return ex.Message.ToString
        End Try
    End Function



End Module
