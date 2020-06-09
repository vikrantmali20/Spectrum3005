Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Management
Public Class clsFoodCost
    Public Function GetFoodCostReport(ByVal SiteCode As String, ByVal fromDate As Date, ByVal Todate As Date) As DataSet
        Dim dsFoodCostReport As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("UDP_FoodCostReport", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@SITECODE", SiteCode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@FromDate", fromDate)
            sqlComm.Parameters.AddWithValue("@ToDate", Todate)

            sqlComm.CommandType = CommandType.StoredProcedure
            dsFoodCostReport = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsFoodCostReport)
            Return dsFoodCostReport
        Catch ex As Exception
            LogException(ex)
            Return dsFoodCostReport
        End Try
    End Function
    'merged by khusrao adil on 15-09-2017 for jk sprint 29 merging
    Public Function GetPersonWiseSalesReport(ByVal SiteCode As String, ByVal fromDate As Date, ByVal Todate As Date) As DataSet
        Dim dsFoodCostReport As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("sp_SalesPersonWiseSalesReport", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@SITECODE", SiteCode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@FROMDATE", fromDate)
            sqlComm.Parameters.AddWithValue("@TODATE", Todate)

            sqlComm.CommandType = CommandType.StoredProcedure
            dsFoodCostReport = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsFoodCostReport)
            Return dsFoodCostReport
        Catch ex As Exception
            LogException(ex)
            Return dsFoodCostReport
        End Try
    End Function

    Public Function GetBillSummaryReport(ByVal SiteCode As String, ByVal fromDate As Date, ByVal Todate As Date) As DataSet
        Dim dsBillSummaryReport As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("UDP_BillSummaryReport", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@SITECODE", SiteCode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@FROMDATE", fromDate)
            sqlComm.Parameters.AddWithValue("@TODATE", Todate)

            sqlComm.CommandType = CommandType.StoredProcedure
            dsBillSummaryReport = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsBillSummaryReport)
            Return dsBillSummaryReport
        Catch ex As Exception
            LogException(ex)
            Return dsBillSummaryReport
        End Try
    End Function

    Public Function GetDeliveryPartnerWiseSalesReport(ByVal SiteCode As String, ByVal fromDate As Date, ByVal Todate As Date, ByVal selectedPartner As String) As DataSet
        Dim dsBillSummaryReport As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("Usp_DeliveryPartnerWiseSalesReport", SpectrumCon)  ''GetBillwiseGSTReport
            sqlComm.Parameters.AddWithValue("@SiteCode", SiteCode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@FromDate", fromDate)
            sqlComm.Parameters.AddWithValue("@ToDate", Todate)
            sqlComm.Parameters.AddWithValue("@partner", selectedPartner)

            sqlComm.CommandType = CommandType.StoredProcedure
            dsBillSummaryReport = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsBillSummaryReport)
            Return dsBillSummaryReport
        Catch ex As Exception
            LogException(ex)
            Return dsBillSummaryReport
        End Try
    End Function

    Public Function GetBillwiseGSTReport(ByVal SiteCode As String, ByVal fromDate As Date, ByVal Todate As Date) As DataSet
        Dim dsBillSummaryReport As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("UDP_BillGrossAmountTaxBase", SpectrumCon)  ''GetBillwiseGSTReport
            sqlComm.Parameters.AddWithValue("@SITECODE", SiteCode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@FROMDATE", fromDate)
            sqlComm.Parameters.AddWithValue("@TODATE", Todate)


            sqlComm.CommandType = CommandType.StoredProcedure
            dsBillSummaryReport = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsBillSummaryReport)
            Return dsBillSummaryReport
        Catch ex As Exception
            LogException(ex)
            Return dsBillSummaryReport
        End Try
    End Function

    Public Function GetBillwiseTenderReport(ByVal SiteCode As String, ByVal fromDate As Date, ByVal Todate As Date) As DataSet
        Dim dsBillWiseTenderReport As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("TenderWiseBillReport", SpectrumCon)  ''GetBillwiseGSTReport
            sqlComm.Parameters.AddWithValue("@SiteCode", SiteCode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@FromDate", fromDate)
            sqlComm.Parameters.AddWithValue("@ToDate", Todate)


            sqlComm.CommandType = CommandType.StoredProcedure
            dsBillWiseTenderReport = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsBillWiseTenderReport)
            Return dsBillWiseTenderReport
        Catch ex As Exception
            LogException(ex)
            Return dsBillWiseTenderReport
        End Try
    End Function
    Public Function GetKOTReport(ByVal SiteCode As String, ByVal fromDate As Date, ByVal Todate As Date) As DataSet
        Dim dsBillSummaryReport As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("Spectrumkotreport", SpectrumCon)  ''GetBillwiseGSTReport
            sqlComm.Parameters.AddWithValue("@sitecd", SiteCode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@FROMDATE", fromDate)
            sqlComm.Parameters.AddWithValue("@TODATE", Todate)


            sqlComm.CommandType = CommandType.StoredProcedure
            dsBillSummaryReport = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsBillSummaryReport)
            Return dsBillSummaryReport
        Catch ex As Exception
            LogException(ex)
            Return dsBillSummaryReport
        End Try
    End Function
    'vipul 06-09-2018 tenderwisecommisionreport
    Public Function GetTenderWiseCommisionReportData(ByVal SiteCode As String, ByVal fromDate As Date, ByVal Todate As Date, ByVal TenderType As String) As DataSet
        Dim dsBillWiseTenderReport As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("TenderWiseCommisionReport", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@SiteCode", SiteCode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@FromDate", fromDate)
            sqlComm.Parameters.AddWithValue("@ToDate", Todate)
            sqlComm.Parameters.AddWithValue("@TenderType", TenderType)


            sqlComm.CommandType = CommandType.StoredProcedure
            dsBillWiseTenderReport = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsBillWiseTenderReport)
            Return dsBillWiseTenderReport
        Catch ex As Exception
            LogException(ex)
            Return dsBillWiseTenderReport
        End Try
    End Function



End Class
