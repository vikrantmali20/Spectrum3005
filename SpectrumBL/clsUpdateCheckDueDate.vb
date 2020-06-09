
Imports System.Data
Imports System.Data.SqlClient

Public Class clsUpdateCheckDueDate
    Private objClsCommon As New clsCommon

    Public Function GetCheckData(ByVal strSiteCode As String, ByVal dtFrom As Date, ByVal dtTo As Date, Optional ByVal bFormLoad As Boolean = False) As DataSet
        Try
            Dim dsCheck As New DataSet
            Dim strQuery As String
            strQuery = "SELECT SiteCode, FinYear, BillDate, BillNo, PayLineNo, Amount, CheckNo, DocumentNo, DocumentType, DueDate, Remarks, CREATEDAT, CREATEDBY," & _
                        "CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON, STATUS FROM CheckDtls WHERE SiteCode = '" & strSiteCode & "' and STATUS = 1 and "

            If Not bFormLoad Then
                strQuery = strQuery & " DATEADD(dd, DATEDIFF(dd,0,DueDate), 0) BETWEEN '" & dtFrom.Date.ToString("yyyyMMdd") & "' and '" & dtTo.Date.ToString("yyyyMMdd") & "'"
            Else
                strQuery = strQuery & " DATEADD(dd, DATEDIFF(dd,0,DueDate), 0)   >=  '" & dtFrom.Date.ToString("yyyyMMdd") & "'"
            End If

            OpenConnection()
            Dim daAdapter As New SqlDataAdapter(strQuery, SpectrumCon)
            daAdapter.Fill(dsCheck)
            dsCheck.Tables(0).TableName = "CheckDtls"
            CloseConnection()
            Return dsCheck
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    Public Function SaveCheckData(ByVal dsData As DataSet) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            If SpectrumCon.State = ConnectionState.Closed Then
                SpectrumCon.Open()
            End If
            tran = SpectrumCon.BeginTransaction
            If objClsCommon.SaveData(dsData, SpectrumCon, tran) Then
                tran.Commit()
                CloseConnection()
                Return True
            Else
                tran.Rollback()
                CloseConnection()
                Return False
            End If
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function GetFromDate(ByVal strSiteCode As String) As Date
        Try
            Dim dtLastCLoseDate As Date
            Dim strQuery As String = "SELECT DATEADD(d,1,MAX(OpenDate)) FROM DayOpenNClose WHERE DayCLoseStatus = 1 and sitecode = '" & strSiteCode & "'"
            OpenConnection()
            Dim scFromDate As New SqlCommand(strQuery, SpectrumCon)
            dtLastCLoseDate = scFromDate.ExecuteScalar()
            CloseConnection()
            Return dtLastCLoseDate
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function


    

End Class