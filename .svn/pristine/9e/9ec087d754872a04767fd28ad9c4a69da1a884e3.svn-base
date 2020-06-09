Imports SpectrumCommon
Imports System.Data.SqlClient
Imports System.ComponentModel
Public Class clsPettyCashBase
    Protected Function GetReader(ByRef query As String) As SqlDataReader
        Try
            OpenConnection()
            Dim sqlCmd As New SqlCommand(query, SpectrumCon)
            Return sqlCmd.ExecuteReader()
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetFilledTable(ByVal strQuery As String) As DataTable
        Try
            OpenConnection()
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(strQuery, SpectrumCon())
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Protected Function InsertOrUpdateRecord(ByVal Query As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            OpenConnection()
            Dim cmd As New SqlCommand
            cmd.CommandText = Query
            cmd.Connection = SpectrumCon()
            If trans IsNot Nothing Then
                cmd.Transaction = trans
            End If
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
