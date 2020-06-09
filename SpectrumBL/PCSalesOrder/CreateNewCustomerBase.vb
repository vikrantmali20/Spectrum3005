Imports SpectrumCommon
Imports System.Data.SqlClient
Imports System.ComponentModel
Public Class CreateNewCustomerBase
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

    Public Function GetScalerValue(ByVal strQuery As String, Optional ByVal trans As SqlTransaction = Nothing) As Object
        Try
            OpenConnection()

            'Dim da As New SqlDataAdapter(strQuery, SpectrumCon())
            'da.Fill(dt)
            'Return dt
            Dim result As Object
            Dim cmd As SqlCommand = New SqlCommand(strQuery, SpectrumCon())
            If trans IsNot Nothing Then
                cmd.Transaction = trans
            End If
            result = cmd.ExecuteScalar()

            Return result
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Protected Function InsertOrUpdateRecord(ByVal Query As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        OpenConnection()
        Dim cmd As New SqlCommand
        cmd.CommandText = Query
        cmd.Connection = SpectrumCon()
        If trans IsNot Nothing Then
            cmd.Transaction = trans
        End If
        cmd.ExecuteNonQuery()
        Return True
    End Function
End Class
