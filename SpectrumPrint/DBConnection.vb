Imports System.Data.SqlClient
Imports System.Data
Module DBConnection
    Public ConString As String
    Public SpectrumCon As SqlConnection
    Public Function OpenConnection() As String
        Try
            If SpectrumCon.State = ConnectionState.Closed Then
                SpectrumCon.Open()
            End If
            Return ""
        Catch ex As Exception
            MsgBox(getValueByKey("BL098"), MsgBoxStyle.Critical, "BL098 - " & getValueByKey("CLAE04"))
            'LogException(ex)
            Return getValueByKey("DC001")
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
            'LogException(ex)
            Return ex.Message.ToString
        End Try
    End Function
    
End Module
