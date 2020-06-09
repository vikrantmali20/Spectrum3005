Imports System.Text
Imports System.Security.Cryptography
Imports SpectrumCommon
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Management
Public Class ShiftCloseReport
    Enum ShiftCloseEnum
        ReportHeaderData = 0
        NetSaleDataShop
        NetCashData
        ShiftWiseCashierData
        SummaryData
        CorrectedCashMemoData
        DeletedCashMemoData
        SaleReturnData
        SalesOrderData
        ClosingData
        OpeningData
        ItemWiseSalesData
        WriteOffData  '' added by nikhil
        NEFT                 'vipin
        RTGS
    End Enum
    Public Function GetShiftCloseReportData(ByVal SiteCode As String, ByVal DayCloseDate As Date, ByVal TerminalId As String, ByVal ShiftId As Integer) As DataSet
        Dim dsDayClose As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("UDP_GetPCShiftCloseReportData", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@V_SiteCode", SiteCode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@V_DayCloseDate", DayCloseDate.Date)
            sqlComm.Parameters.AddWithValue("@V_TerminalId", TerminalId)
            sqlComm.Parameters.AddWithValue("@V_ShiftId", ShiftId)
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

    Public Function GetShiftCloseInConsistanciesReportData(ByVal SiteCode As String, ByVal DayCloseDate As Date) As DataTable
        Dim dsDayClose As DataTable = Nothing
        Try
            Dim sqlComm As New SqlCommand("UDP_ShiftClosingFloatingInputReport", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@V_SiteCode", SiteCode)
            sqlComm.Parameters.AddWithValue("@V_DayCloseDate", DayCloseDate)
            sqlComm.CommandType = CommandType.StoredProcedure
            dsDayClose = New DataTable
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsDayClose)
            Return dsDayClose
        Catch ex As Exception
            LogException(ex)
            Return dsDayClose
        End Try
    End Function

End Class
