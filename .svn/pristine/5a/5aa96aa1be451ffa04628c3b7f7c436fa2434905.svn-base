Imports System.Data.SqlClient

Public Class clsPrintingSettings

    Public Function GetSRNumber() As Integer
        Dim dbSrNo As Integer
        Dim drSrNo As SqlDataReader
        Try
            OpenConnection()
            Dim cmdCurrentDate As New SqlCommand("select max(isnull(Srno,0)) from PrintingDetail", SpectrumCon)
            drSrNo = cmdCurrentDate.ExecuteReader()
            If (drSrNo.Read()) Then
                If Not (drSrNo.IsDBNull(0)) Then
                    dbSrNo = drSrNo.GetDecimal(0)
                End If
            End If
            Return dbSrNo
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            drSrNo.Close()
            CloseConnection()
        End Try
    End Function

    Public Function GetDocumentType() As DataTable
        Dim dtDocumentType As New DataTable
        Try
            OpenConnection()
            Dim cmdCurrentDate As New SqlCommand("SELECT  PrinterDocument as DOCUMENTTYPE , DocumentDesc as DOCUMENTTYPEDESC FROM MstPrinterDoc WHERE STATUS=1", SpectrumCon)
            Dim adp As New SqlDataAdapter(cmdCurrentDate)
            adp.Fill(dtDocumentType)
            Return dtDocumentType
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

End Class
