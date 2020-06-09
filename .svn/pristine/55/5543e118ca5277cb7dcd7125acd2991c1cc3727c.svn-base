Imports System.Text
Imports System.Security.Cryptography
Imports SpectrumCommon
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Management
Public Class DsrReport
    Public Function GetDsrReportData(ByVal SiteCode As String, ByVal DayCloseDate As Date) As DataSet
        Dim dsDSR As DataSet = Nothing
        Try
            Dim dtSite As New DataTable
            dtSite.Columns.Add("SiteCode", GetType(System.String))
            dtSite.Rows.Add(SiteCode)

            Dim dtDate As New DataTable
            dtDate.Columns.Add("Date", GetType(System.String))
            dtDate.Rows.Add(DayCloseDate)

            Dim dt1 As New DataTable
            Dim dt2 As New DataTable

            Dim sqlComm As New SqlCommand("sp_daily_sales_report", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@fromDate", DayCloseDate.Date)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@sitecode", SiteCode)
            sqlComm.CommandType = CommandType.StoredProcedure
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dt1)


            Dim sqlComm1 As New SqlCommand("sp_daily_sales_sub_report", SpectrumCon)
            sqlComm1.Parameters.AddWithValue("@fromDate", DayCloseDate.Date)
            sqlComm1.CommandTimeout = 0
            sqlComm1.Parameters.AddWithValue("@sitecode", SiteCode)
            sqlComm1.CommandType = CommandType.StoredProcedure
            Dim da1 As New SqlDataAdapter(sqlComm1)
            da1.Fill(dt2)

            dsDSR = New DataSet
            dsDSR.Tables.Add(dt1)
            dsDSR.Tables.Add(dt2)
            dsDSR.Tables.Add(dtSite)
            dsDSR.Tables.Add(dtDate)


            Return dsDSR
        Catch ex As Exception
            LogException(ex)
            Return dsDSR
        End Try
    End Function
End Class
