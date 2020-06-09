Imports System.Data.SqlClient
Public Class clsDataSync
    Inherits clsCommon

    Public Function GetSyncSettings() As DataTable
        Try
            Dim strQuery As String = "SELECT * FROM SYNCTERMINALSET"
            Dim dt As DataTable = GetFilledTable(strQuery)
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function SaveSettings(ByVal dt As DataTable) As Boolean
        Try
            dt.TableName = "SYNCTERMINALSET"
            Dim tran As SqlTransaction
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If SaveData(dt, SpectrumCon, tran) = True Then
                If savenewSettings(SpectrumCon, tran) = True Then
                    tran.Commit()
                    CloseConnection()
                    Return True
                Else
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
            Else
                tran.Rollback()
                CloseConnection()
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function savenewSettings(ByRef sqlcon As SqlConnection, ByRef tran As SqlTransaction) As Boolean
        Try
            Dim cmd As New SqlCommand("Exec BUILDSYNCSETTING ", sqlcon, tran)
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
