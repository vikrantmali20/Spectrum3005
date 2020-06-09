Imports System.Data.SqlClient
Imports SpectrumCommon
Imports NPOI.HSSF.UserModel
Imports NPOI.HPSF
Imports NPOI.POIFS.FileSystem
Imports NPOI.SS.UserModel
Imports System.IO
Public Class BillWiseReport
    Inherits ExcelReportBase
    Implements IReports

    Private DayCloseReportPath As String = "D:\DayCloseReports"

    Public Function GenerateDayCloseReport(ByVal request As SpectrumCommon.DayCloseReportModel, reportPath As String) As Boolean Implements IReports.GenerateDayCloseReport
        Try
            If Not String.IsNullOrEmpty(reportPath) Then
                DayCloseReportPath = reportPath
            End If
            Dim path As String = GetDirectoryPath(request)
            Dim ds As DataSet = GetBillWiseReporDataSet(request)
            If ds.Tables IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso Not String.IsNullOrEmpty(path) Then
                Dim hssfworkbook As HSSFWorkbook = InitializeWorkbook()
                Dim sheet1 As ISheet = hssfworkbook.CreateSheet("Bill wise detail report")
                Dim siteName As String = GetSiteName(request.SiteCode)
                Dim lastRow As IRow = sheet1.CreateRow(0)
                lastRow.CreateCell(0).SetCellValue("Name : Bill wise detail report")
                SetCellStyle(lastRow, GetBoldFont(hssfworkbook))
                lastRow = sheet1.CreateRow(1)
                lastRow.CreateCell(0).SetCellValue("Store name : " & siteName)
                SetCellStyle(lastRow, GetBoldFont(hssfworkbook))
                lastRow = sheet1.CreateRow(2)
                lastRow.CreateCell(0).SetCellValue("Genrated by  + Date + time : " & request.CreatedBy & ", " & request.CreatedOn.ToString())
                SetCellStyle(lastRow, GetBoldFont(hssfworkbook))
                lastRow = sheet1.CreateRow(3)
                lastRow.CreateCell(0).SetCellValue("Bill wise report for period : " & request.FromDate.ToShortDateString() & " to " & request.ToDate.ToShortDateString())
                SetCellStyle(lastRow, GetBoldFont(hssfworkbook))
                Dim col As Integer = 0
                lastRow = sheet1.CreateRow(5)
                For Each item As DataColumn In ds.Tables(0).Columns
                    lastRow.CreateCell(col).SetCellValue(item.Caption)
                    col += 1
                Next
                SetCellStyle(lastRow, GetBoldFont(hssfworkbook))
                col = 0
                Dim row As Integer = 6
                lastRow = sheet1.CreateRow(row)
                For Each dr As DataRow In ds.Tables(1).Rows
                    Dim billDate As DateTime = dr("BillDate")
                    For Each item As DataRow In ds.Tables(0).Select("BillDate='" & billDate & "'")
                        For Each cell In item.ItemArray()
                            If col = 0 Then
                                lastRow.CreateCell(col).SetCellValue(billDate.ToShortDateString())
                            Else
                                lastRow.CreateCell(col).SetCellValue(cell.ToString())
                            End If
                            col += 1
                        Next
                        col = 0
                        row += 1
                        lastRow = sheet1.CreateRow(row)

                        Dim billno = item("BillNO").ToString()
                        Dim COMBOARTICLENAME = item("ITEM_DISCRIPTION_MODIFIER").ToString()
                        For Each comboItem As DataRow In ds.Tables(3).AsEnumerable().Where(Function(w) w("BillDate") = billDate And w("BillNO") = billno And w("COMBOARTICLENAME").ToString() = COMBOARTICLENAME)
                            lastRow.CreateCell(0).SetCellValue(billDate.ToShortDateString())
                            lastRow.CreateCell(1).SetCellValue(item("SITECODE").ToString())
                            lastRow.CreateCell(2).SetCellValue(item("BILLNO").ToString())
                            lastRow.CreateCell(3).SetCellValue(item("CASHIERNAME").ToString())
                            lastRow.CreateCell(4).SetCellValue(comboItem("ARTICLENAME").ToString())
                            lastRow.CreateCell(5).SetCellValue(item("BILLTIME").ToString())
                            lastRow.CreateCell(6).SetCellValue(comboItem("SALESQUANTITY").ToString())
                            row += 1
                            lastRow = sheet1.CreateRow(row)
                        Next
                    Next
                    lastRow.CreateCell(0).SetCellValue("Sub Total :")
                    lastRow.CreateCell(2).SetCellValue("Count Of Bill : " & dr("COUNT_OF_BILLNO").ToString())
                    lastRow.CreateCell(6).SetCellValue(dr("QUANTITY").ToString())
                    lastRow.CreateCell(7).SetCellValue(dr("RATE").ToString())
                    lastRow.CreateCell(8).SetCellValue(dr("TAX").ToString())
                    lastRow.CreateCell(9).SetCellValue(dr("TOTALDISCOUNT").ToString())
                    lastRow.CreateCell(10).SetCellValue(dr("NETAMOUNT").ToString())
                    SetCellStyle(lastRow, GetBoldFont(hssfworkbook))
                    row += 2
                    lastRow = sheet1.CreateRow(row)
                Next
                row += 1
                lastRow = sheet1.CreateRow(row)
                For Each dr As DataRow In ds.Tables(2).Rows
                    lastRow.CreateCell(0).SetCellValue("Grand Total :")
                    lastRow.CreateCell(2).SetCellValue(dr("TOTAL_COUNT_BILLNO").ToString())
                    lastRow.CreateCell(6).SetCellValue(dr("TOTAL_QUANTITY").ToString())
                    lastRow.CreateCell(7).SetCellValue(dr("TOTAL_RATE").ToString())
                    lastRow.CreateCell(8).SetCellValue(dr("TOTAL_TAX").ToString())
                    lastRow.CreateCell(9).SetCellValue(dr("TOTAL_DISCOUNT").ToString())
                    lastRow.CreateCell(10).SetCellValue(dr("NETAMOUNT").ToString())
                    SetCellStyle(lastRow, GetBoldFont(hssfworkbook))
                    row += 1
                    lastRow = sheet1.CreateRow(row)

                Next
                col = 1
                For Each item As DataColumn In ds.Tables(0).Columns
                    sheet1.AutoSizeColumn(col)
                    col += 1
                Next
                WriteToFile(path, hssfworkbook)
                System.Diagnostics.Process.Start(path)
            End If
        Catch ex As Exception
            LogException(ex)

        End Try
    End Function

    Private Function GetBillWiseReporDataSet(ByVal request As SpectrumCommon.DayCloseReportModel) As DataSet
        Try
            Dim query As String = "exec Bill_dtl_report '" & request.FromDate.ToString("yyyy-MM-dd") & "' ,'" & request.ToDate.ToString("yyyy-MM-dd") & "' ,'" & request.SiteCode & "'"
            Dim dataAdapter As SqlDataAdapter
            Dim reportSet As New DataSet
            'dataAdapter = New SqlDataAdapter(query, SpectrumCon)
            Dim cmd As New SqlCommand(query, SpectrumCon)
            cmd.CommandTimeout = 0
            dataAdapter = New SqlDataAdapter(cmd)
            dataAdapter.Fill(reportSet)
            Return reportSet
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Private Function GetDirectoryPath(ByRef request As DayCloseReportModel) As String
        Try
            Dim path As String = String.Empty
            Dim siteName As String = GetSiteName(request.SiteCode)
            If Not System.IO.Directory.Exists(DayCloseReportPath) Then
                System.IO.Directory.CreateDirectory(DayCloseReportPath)
            End If
            path = DayCloseReportPath & "\BillWiseReport_" & siteName & "_" & request.FromDate.ToString("dd-MM-yyyy") & "_To_" & request.ToDate.ToString("dd-MM-yyyy") & ".xls"
            Return path
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
End Class
