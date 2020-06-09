Imports System.Data.SqlClient
Imports SpectrumBL
Imports SpectrumCommon


Public Class clsXRead

    Public Function GetXReadData(ByVal dttodate As Date, ByVal Sitecode As String, ByVal dtfromdate As Date) As DataSet
        Try
            Dim DsXreadData As New DataSet
            Dim daxread As SqlDataAdapter
            Dim stringComman As String = "GetXReadReportData"
            Dim cmd As New SqlCommand(stringComman.ToString(), SpectrumCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@V_BillDate", dttodate)
            cmd.Parameters.AddWithValue("@V_SiteCode", Sitecode)
            cmd.Parameters.AddWithValue("@V_ToBillDate", dtfromdate)
            daxread = New SqlDataAdapter(cmd)
            daxread.Fill(DsXreadData)
            Return DsXreadData
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function



    Public Function GetXReadHeaderData(ByVal dttodate As Date, ByVal Sitecode As String, ByVal dtfromdate As Date) As DataTable
        Try
            Dim DsXreadHEaderData As New DataTable
            Dim daxread As SqlDataAdapter
            Dim stringComman As String = "GetHeaderXRead"
            Dim cmd As New SqlCommand(stringComman.ToString(), SpectrumCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@V_BillDate", dttodate)
            cmd.Parameters.AddWithValue("@V_SiteCode", Sitecode)
            cmd.Parameters.AddWithValue("@V_ToBillDate", dtfromdate)
            daxread = New SqlDataAdapter(cmd)
            daxread.Fill(DsXreadHEaderData)
            Return DsXreadHEaderData
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    'added by khusrao adil on 14-07-2017
    Public Function GetXReadCategorywiseData(ByVal dttodate As Date, ByVal Sitecode As String) As DataSet
        Try
            Dim DsXreadData As New DataSet
            Dim daxread As SqlDataAdapter
            Dim stringComman As String = "GetXReadCategorywiseReportData"
            Dim cmd As New SqlCommand(stringComman.ToString(), SpectrumCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@V_BillDate", dttodate)
            cmd.Parameters.AddWithValue("@V_SiteCode", Sitecode)
            daxread = New SqlDataAdapter(cmd)
            daxread.Fill(DsXreadData)
            Return DsXreadData
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    'Added by Jayesh 

    Public Function GetSalesandTransactionsReport(ByVal Sitecode As String, ByVal frmdate As Date, ByVal todate As Date) As DataSet
        Try
            Dim DsXreadsalesrData As New DataSet
            Dim daxread As SqlDataAdapter
            Dim stringComman As String = "Usp_SalesandTransactionReport"
            Dim cmd As New SqlCommand(stringComman.ToString(), SpectrumCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@SiteCode", Sitecode)
            cmd.Parameters.AddWithValue("@FromDate", frmdate)
            cmd.Parameters.AddWithValue("@ToDate", todate)
            daxread = New SqlDataAdapter(cmd)
            daxread.Fill(DsXreadsalesrData)
            Return DsXreadsalesrData
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    'added by khusrao adil on 14-07-2017
    Public Function GetXReadCategorywiseHeaderData(ByVal dttodate As Date, ByVal Sitecode As String) As DataTable
        Try
            Dim DsXreadHEaderData As New DataTable
            Dim daxread As SqlDataAdapter
            Dim stringComman As String = "GetHeaderXReadCategorywise"
            Dim cmd As New SqlCommand(stringComman.ToString(), SpectrumCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@V_BillDate", dttodate)
            cmd.Parameters.AddWithValue("@V_SiteCode", Sitecode)
            daxread = New SqlDataAdapter(cmd)
            daxread.Fill(DsXreadHEaderData)
            Return DsXreadHEaderData
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'added by irfan on 31/8/2018
    Public Function GetCustomerwiseSalesReportData(ByVal Sitecode As String, ByVal frmdate As Date, ByVal todate As Date) As DataSet
        Try
            Dim DsXreadHEaderData As New DataSet
            Dim daxread As SqlDataAdapter
            Dim stringComman As String = "Usp_CustomerWiseSales"
            Dim cmd As New SqlCommand(stringComman.ToString(), SpectrumCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@SiteCode", Sitecode)
            cmd.Parameters.AddWithValue("@FromDate", frmdate)
            cmd.Parameters.AddWithValue("@ToDate", todate)
            daxread = New SqlDataAdapter(cmd)
            daxread.Fill(DsXreadHEaderData)
            Return DsXreadHEaderData
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'code added by vipul on 21-05-2018

    Public Function GetXReadFavourwiseData(ByVal dttodate As Date, ByVal Sitecode As String, ByVal dtfromdate As Date) As DataSet
        Try
            Dim DsXreadData As New DataSet
            Dim daxread As SqlDataAdapter
            Dim stringComman As String = "GetXReadFlavourwiseReportData"
            Dim cmd As New SqlCommand(stringComman.ToString(), SpectrumCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@V_BillDate", dttodate)
            cmd.Parameters.AddWithValue("@V_SiteCode", Sitecode)
            cmd.Parameters.AddWithValue("@V_ToBillDate", dtfromdate)
            daxread = New SqlDataAdapter(cmd)
            daxread.Fill(DsXreadData)
            Return DsXreadData
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    Public Function GetXReadFlavourwiseHeaderData(ByVal dttodate As Date, ByVal Sitecode As String, ByVal dtfromdate As Date) As DataTable
        Try
            Dim DsXreadHEaderData As New DataTable
            Dim daxread As SqlDataAdapter
            Dim stringComman As String = "GetHeaderXReadFlavourwise"
            Dim cmd As New SqlCommand(stringComman.ToString(), SpectrumCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@V_BillDate", dttodate)
            cmd.Parameters.AddWithValue("@V_SiteCode", Sitecode)
            cmd.Parameters.AddWithValue("@V_ToBillDate", dtfromdate)
            daxread = New SqlDataAdapter(cmd)
            daxread.Fill(DsXreadHEaderData)
            Return DsXreadHEaderData
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    'code added by vipul for new report x-read cat
    Public Function GetXReadCatData(ByVal dttodate As Date, ByVal Sitecode As String, ByVal dtfromdate As Date) As DataSet
        Try
            Dim DsXreadData As New DataSet
            Dim daxread As SqlDataAdapter
            Dim stringComman As String = "GetXReadCatReportData"
            Dim cmd As New SqlCommand(stringComman.ToString(), SpectrumCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@V_BillDate", dttodate)
            cmd.Parameters.AddWithValue("@V_SiteCode", Sitecode)
            cmd.Parameters.AddWithValue("@V_ToBillDate", dtfromdate)
            daxread = New SqlDataAdapter(cmd)
            daxread.Fill(DsXreadData)
            Return DsXreadData
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    Public Function GetWriteOffReportData(ByVal sitecode As String, ByVal fromDate As Date, ByVal TodateDate As Date, ByVal TerminalId As String) As DataSet

        Try
            Dim dsDayClose As DataSet = Nothing
            Dim frmDate = fromDate.ToString("yyyy-MM-dd")
            Dim Todate = TodateDate.ToString("yyyy-MM-dd")
            Dim sqlComm As New SqlCommand("UDP_StandAloneWriteReportData", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@V_SiteCode", sitecode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@FromDate", frmDate)
            sqlComm.Parameters.AddWithValue("@ToDate", Todate)
            sqlComm.Parameters.AddWithValue("@V_TerminalId", TerminalId)
            sqlComm.CommandType = CommandType.StoredProcedure
            dsDayClose = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsDayClose)
            Return dsDayClose
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetXReadCatHeaderData(ByVal dttodate As Date, ByVal Sitecode As String, ByVal dtfromdate As Date) As DataTable
        Try
            Dim DsXreadHEaderData As New DataTable
            Dim daxread As SqlDataAdapter
            Dim stringComman As String = "GetHeaderXReadCatReport"
            Dim cmd As New SqlCommand(stringComman.ToString(), SpectrumCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@V_BillDate", dttodate)
            cmd.Parameters.AddWithValue("@V_SiteCode", Sitecode)
            cmd.Parameters.AddWithValue("@V_ToBillDate", dtfromdate)
            daxread = New SqlDataAdapter(cmd)
            daxread.Fill(DsXreadHEaderData)
            Return DsXreadHEaderData
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

End Class
