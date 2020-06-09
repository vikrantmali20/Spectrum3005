Imports System.Data.SqlClient

Public Class DbConnectionBuild
    ''' <summary>
    ''' Set Sqlconnection
    ''' </summary>
    ''' <param name="Strcon">ConnectionString</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Function SetConnection(ByVal Strcon As String) As Boolean
        Try
            'My.Settings.POSDBConnectionString = Strcon
            ConString = Strcon
            SpectrumCon = New SqlConnection(Strcon)
            Return True
        Catch ex As Exception

        End Try
    End Function
End Class
