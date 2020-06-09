Imports System.Text
Imports System.Security.Cryptography
Imports SpectrumCommon
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Management
Public Class DayCloseNewReport
    Enum DayCloseDatasetNames
        ReportHeaderData = 0
        NetSaleDataShop
        NetSaleDataOther
        ' TotalNetSalesShop
        ' TotalNetSalesOther
        NetCashData
        ShiftWiseCashierData
        SummaryData
        CorrectedCashMemoData
        DeletedCashMemoData
        SaleReturnData
        StatisticsData
        PettyCashExpenseData
        PettyCashReceiptData
        OpeningData
        ClosingData
        NextDayOpening
        CloseBankData
        ItemWiseSalesData
        WriteOffData  '' nikhil
        NEFT                     'vipin
        RTGS
    End Enum
    Public Function GetNewDayCloseReportData(ByVal SiteCode As String, ByVal DayCloseDate As Date) As DataSet
        Dim dsDayClose As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("UDP_GetPCDayCloseReportData", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@V_SiteCode", SiteCode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@V_DayCloseDate", DayCloseDate.Date)
            sqlComm.CommandType = CommandType.StoredProcedure
            dsDayClose = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsDayClose)
            Return dsDayClose
        Catch ex As Exception
            LogException(ex)
            Return dsDayClose
        End Try
    End Function
    Public Function GetStockAnalysisReportData(ByVal SiteCode As String, ByVal DayCloseDate As Date) As DataSet
        Dim dsDayClose As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("USP_GETSTOCKANALYSISREPORTDATA", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@sitecode", SiteCode)
            sqlComm.Parameters.AddWithValue("@dayclosedate", DayCloseDate)

            sqlComm.CommandTimeout = 0

            sqlComm.CommandType = CommandType.StoredProcedure
            dsDayClose = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsDayClose)
            Return dsDayClose
        Catch ex As Exception
            LogException(ex)
            Return dsDayClose
        End Try
    End Function
End Class
