Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Management
Public Class clsTargetVsActualSales
    Public Function GetTargetVsActualSalesReportData(ByVal SiteCode As String, ByVal Month As String, ByVal Year As String) As DataSet
        Dim dsTargetVsActualSalese As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("Usp_TargetVsActualSalesReport", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@SiteCode", SiteCode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@Month", Month)
            sqlComm.Parameters.AddWithValue("@Year", Year)
            sqlComm.CommandType = CommandType.StoredProcedure
            dsTargetVsActualSalese = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsTargetVsActualSalese)
            Return dsTargetVsActualSalese
        Catch ex As Exception
            LogException(ex)
            Return dsTargetVsActualSalese
        End Try
    End Function
End Class
Public Class clsJKDayOfReportPayout
    Public Function GetJKDayOfReportPayout(ByVal SiteCode As String, ByVal fromDate As Date, ByVal Todate As Date, ByVal PromotionsId As String, ByVal payoutvalue As Integer, ByVal Reason As String) As DataSet
        Dim dsJKDayOfReport As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("usp_jkofferreportpayout", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@sitecode", SiteCode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@FROM_DATE", fromDate)
            sqlComm.Parameters.AddWithValue("@TO_DATE", Todate)
            sqlComm.Parameters.AddWithValue("@OFFER_NO", PromotionsId)
            sqlComm.Parameters.AddWithValue("@VALUE", payoutvalue)
            sqlComm.Parameters.AddWithValue("@REASON", Reason)
            sqlComm.CommandType = CommandType.StoredProcedure
            dsJKDayOfReport = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsJKDayOfReport)
            Return dsJKDayOfReport
        Catch ex As Exception
            LogException(ex)
            Return dsJKDayOfReport
        End Try
    End Function
End Class
Public Class clsJKProductMixReport
    Public Function JKProductMixReport(ByVal SiteCode As String, ByVal fromDate As Date, ByVal Todate As Date) As DataSet
        Dim dsJKProductMixReport As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("USP_JKProductMixReport", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@SiteCode", SiteCode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@FromDate", fromDate)
            sqlComm.Parameters.AddWithValue("@ToDate", Todate)
            sqlComm.CommandType = CommandType.StoredProcedure
            dsJKProductMixReport = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsJKProductMixReport)
            Return dsJKProductMixReport
        Catch ex As Exception
            LogException(ex)
            Return dsJKProductMixReport
        End Try
    End Function
    'added by khusrao adil on 29-11-2017 for jk sprint 32
    Public Function JKProductMixReportTillWise(ByVal SiteCode As String, ByVal fromDate As Date, ByVal Todate As Date, ByVal Terminals As String) As DataSet
        Dim dsJKProductMixReport As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("USP_JKProductMixReportTillWise", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@SiteCode", SiteCode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@FromDate", fromDate)
            sqlComm.Parameters.AddWithValue("@ToDate", Todate)
            sqlComm.Parameters.AddWithValue("@Terminalid", Terminals)
            sqlComm.CommandType = CommandType.StoredProcedure
            dsJKProductMixReport = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsJKProductMixReport)
            Return dsJKProductMixReport
        Catch ex As Exception
            LogException(ex)
            Return dsJKProductMixReport
        End Try
    End Function
End Class
Public Class clsHcCustomerDetailsReport
    Public Function HcCustomerDetailsReport(ByVal fromDate As Date, ByVal Todate As Date, ByVal Classify As String) As DataSet
        Dim dsJKProductMixReport As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("USP_CustomerDetails ", SpectrumCon)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@Classify", Classify)
            sqlComm.Parameters.AddWithValue("@FromDate", fromDate)
            sqlComm.Parameters.AddWithValue("@ToDate", Todate)
            sqlComm.CommandType = CommandType.StoredProcedure
            dsJKProductMixReport = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsJKProductMixReport)
            Return dsJKProductMixReport
        Catch ex As Exception
            LogException(ex)
            Return dsJKProductMixReport
        End Try
    End Function
End Class
Public Class clsSalesReconciliation
    Public Function SalesReconciliationReport(ByVal SiteCode As String, ByVal fromDate As Date, ByVal Todate As Date) As DataSet
        Dim dsSalesReconciliation As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("SP_RPTJKSALESRECCONCILATIONREPORTNEW", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@SITECODE", SiteCode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@FROMDATE", fromDate)
            sqlComm.Parameters.AddWithValue("@TODATE", Todate)
            sqlComm.CommandType = CommandType.StoredProcedure
            dsSalesReconciliation = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsSalesReconciliation)
            Return dsSalesReconciliation
        Catch ex As Exception
            LogException(ex)
            Return dsSalesReconciliation
        End Try
    End Function
End Class
