Imports System.Data.SqlClient
Imports SpectrumCommon
Imports NPOI.HSSF.UserModel
Imports NPOI.HPSF
Imports NPOI.POIFS.FileSystem
Imports NPOI.SS.UserModel
Imports System.IO

Public Class KOTReport
    Inherits ExcelReportBase
    Implements IReports
    Private DayCloseReportPath As String = "D:\DayCloseReports"
    'Public Function GenerateDayCloseReport(ByVal request As SpectrumCommon.DayCloseReportModel) As Boolean Implements IReports.GenerateDayCloseReport
    '    Dim xlApp As New Excel.Application
    '    Dim xlWorkbook As Excel.Workbook = xlApp.Workbooks.Add
    '    Try
    '        Dim path As String = GetDirectoryPath(request)
    '        Dim ds As DataSet = GetKOTReporDataSet(request)
    '        If ds.Tables IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso Not String.IsNullOrEmpty(path) Then
    '            Dim xlWorkSheet As Excel.Worksheet = xlWorkbook.Sheets(1)
    '            xlWorkSheet.Name = "KOT Report"
    '            xlWorkSheet.Range("A1", "B1").Merge()
    '            Dim siteName As String = GetSiteName(request.SiteCode)
    '            xlWorkSheet.Cells(1, 1) = "Name : KOT report"
    '            xlWorkSheet.Range("A2", "C2").Merge()
    '            xlWorkSheet.Cells(2, 1) = "Store name : " & siteName
    '            xlWorkSheet.Range("A3", "C3").Merge()
    '            xlWorkSheet.Cells(3, 1) = "Genrated by  + Date + time : " & request.CreatedBy & ", " & request.CreatedOn.ToString()
    '            xlWorkSheet.Range("A4", "C4").Merge()
    '            xlWorkSheet.Cells(4, 1) = "KOT Report for Period : " & request.FromDate.ToShortDateString() & " to " & request.ToDate.ToShortDateString()
    '            Dim col As Integer = 2
    '            For Each item As DataColumn In ds.Tables(0).Columns
    '                xlWorkSheet.Cells(6, col).Value = item.Caption
    '                col += 1
    '            Next
    '            col = 2
    '            For i As Integer = 1 To 6 Step 1
    '                xlWorkSheet.Rows(i).font.bold = True
    '            Next
    '            Dim row As Integer = 7
    '            For Each dr As DataRow In ds.Tables(2).Rows
    '                Dim nodeName As String = dr("NodeName")
    '                xlWorkSheet.Cells(row, 1) = nodeName
    '                For Each item As DataRow In ds.Tables(1).Select("NodeName='" & nodeName & "'")
    '                    xlWorkSheet.Cells(row, 2) = item(0)
    '                    xlWorkSheet.Cells(row, 3) = item(2)
    '                    xlWorkSheet.Cells(row, 4) = item(3)
    '                    xlWorkSheet.Cells(row, 5) = item(4)
    '                    xlWorkSheet.Cells(row, 6) = item(5)
    '                    xlWorkSheet.Cells(row, 7) = item(6)
    '                    row += 1
    '                Next
    '                xlWorkSheet.Cells(row, 3) = "Sub Total :"
    '                xlWorkSheet.Cells(row, 4) = dr("SALESQUANTITY")
    '                xlWorkSheet.Cells(row, 5) = dr("Total_Amount")
    '                xlWorkSheet.Cells(row, 6) = dr("Total_Discount")
    '                xlWorkSheet.Cells(row, 7) = dr("Total_Percentage")
    '                xlWorkSheet.Rows(row).font.bold = True
    '                row += 2
    '            Next
    '            For Each item As DataRow In ds.Tables(0).Rows
    '                xlWorkSheet.Cells(row, 1) = "Combo"
    '                For Each cell In item.ItemArray
    '                    xlWorkSheet.Cells(row, col) = cell
    '                    col += 1
    '                Next
    '                col = 2
    '                row += 1
    '                Dim comboCode As String = item("ARTICLECODE")
    '                For Each dr As DataRow In ds.Tables(4).Select("ComboArticleCode='" & comboCode & "'")
    '                    xlWorkSheet.Cells(row, 3) = dr("ArticleName")
    '                    xlWorkSheet.Cells(row, 4) = dr("SALESQUANTITY")
    '                    row += 1
    '                Next
    '            Next
    '            row += 1
    '            For Each dr As DataRow In ds.Tables(3).Rows
    '                xlWorkSheet.Cells(row, 3) = "Grand Total :"
    '                xlWorkSheet.Cells(row, 4) = dr("Total_Quantity")
    '                xlWorkSheet.Cells(row, 5) = dr("Total_Amount")
    '                xlWorkSheet.Cells(row, 6) = dr("Total_Discount")
    '                xlWorkSheet.Cells(row, 7) = dr("Total_Percentage")
    '                xlWorkSheet.Rows(row).font.bold = True
    '                row += 1
    '            Next
    '            xlWorkSheet.Cells.Columns.AutoFit()
    '            xlWorkSheet.SaveAs(path)
    '            xlWorkbook.Close()
    '            xlApp.Quit()
    '            releaseObject(xlApp)
    '            releaseObject(xlWorkbook)
    '            releaseObject(xlWorkSheet)
    '            System.Diagnostics.Process.Start(path)
    '        End If
    '    Catch ex As Exception
    '        LogException(ex)
    '        xlWorkbook.Close()
    '        xlApp.Quit()
    '    End Try
    'End Function

    Public Function GenerateDayCloseReport(ByVal request As SpectrumCommon.DayCloseReportModel, reportPath As String) As Boolean Implements IReports.GenerateDayCloseReport
        Try
            If Not String.IsNullOrEmpty(reportPath) Then
                DayCloseReportPath = reportPath
            End If
            Dim path As String = GetDirectoryPath(request)
            Dim ds As DataSet = GetKOTReporDataSet(request)
            If ds.Tables IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso Not String.IsNullOrEmpty(path) Then
                Dim hssfworkbook As HSSFWorkbook = InitializeWorkbook()

                Dim sheet1 As ISheet = hssfworkbook.CreateSheet("KOT Report")
                Dim siteName As String = GetSiteName(request.SiteCode)
                Dim lastRow As IRow = sheet1.CreateRow(0)
                lastRow.CreateCell(0).SetCellValue("Name : KOT report")
                SetCellStyle(lastRow, GetBoldFont(hssfworkbook))
                lastRow = sheet1.CreateRow(1)
                lastRow.CreateCell(0).SetCellValue("Store name : " & siteName)
                SetCellStyle(lastRow, GetBoldFont(hssfworkbook))
                lastRow = sheet1.CreateRow(2)
                lastRow.CreateCell(0).SetCellValue("Genrated by  + Date + time : " & request.CreatedBy & ", " & request.CreatedOn.ToString())
                SetCellStyle(lastRow, GetBoldFont(hssfworkbook))
                lastRow = sheet1.CreateRow(3)
                lastRow.CreateCell(0).SetCellValue("KOT Report for Period : " & request.FromDate.ToShortDateString() & " to " & request.ToDate.ToShortDateString())
                SetCellStyle(lastRow, GetBoldFont(hssfworkbook))
                Dim col As Integer = 1
                lastRow = sheet1.CreateRow(5)
                'lastRow.
                For Each item As DataColumn In ds.Tables(0).Columns
                    lastRow.CreateCell(col).SetCellValue(item.Caption)
                    col += 1
                Next
                SetCellStyle(lastRow, GetBoldFont(hssfworkbook))
                col = 1
                Dim row As Integer = 6
                lastRow = sheet1.CreateRow(row)
                For Each dr As DataRow In ds.Tables(2).Rows
                    Dim nodeName As String = dr("NodeName")
                    lastRow.CreateCell(0).SetCellValue(nodeName)
                    For Each item As DataRow In ds.Tables(1).Select("NodeName='" & nodeName & "'")
                        lastRow.CreateCell(1).SetCellValue(item(0).ToString())
                        lastRow.CreateCell(2).SetCellValue(item(2).ToString())
                        lastRow.CreateCell(3).SetCellValue(item(3).ToString())
                        lastRow.CreateCell(4).SetCellValue(item(4).ToString())
                        lastRow.CreateCell(5).SetCellValue(item(5).ToString())
                        lastRow.CreateCell(6).SetCellValue(item(6).ToString())
                        row += 1
                        lastRow = sheet1.CreateRow(row)
                    Next
                    lastRow.CreateCell(2).SetCellValue("Sub Total :")
                    lastRow.CreateCell(3).SetCellValue(dr("SALESQUANTITY").ToString())
                    lastRow.CreateCell(4).SetCellValue(dr("Total_Amount").ToString())
                    lastRow.CreateCell(5).SetCellValue(dr("Total_Discount").ToString())
                    lastRow.CreateCell(6).SetCellValue(dr("Total_Percentage").ToString())
                    SetCellStyle(lastRow, GetBoldFont(hssfworkbook))
                    row += 2
                    lastRow = sheet1.CreateRow(row)
                Next
                For Each item As DataRow In ds.Tables(0).Rows
                    lastRow.CreateCell(0).SetCellValue("Combo")
                    For Each cell In item.ItemArray
                        lastRow.CreateCell(col).SetCellValue(cell.ToString())
                        col += 1
                    Next
                    col = 1
                    row += 1
                    lastRow = sheet1.CreateRow(row)
                    Dim comboCode As String = item("ARTICLECODE")
                    For Each dr As DataRow In ds.Tables(4).Select("ComboArticleCode='" & comboCode & "'")
                        lastRow.CreateCell(2).SetCellValue(dr("ArticleName").ToString())
                        lastRow.CreateCell(3).SetCellValue(dr("SALESQUANTITY").ToString())
                        row += 1
                        lastRow = sheet1.CreateRow(row)
                    Next
                Next
                row += 1
                lastRow = sheet1.CreateRow(row)
                For Each dr As DataRow In ds.Tables(3).Rows
                    lastRow.CreateCell(2).SetCellValue("Grand Total :")
                    lastRow.CreateCell(3).SetCellValue(dr("Total_Quantity").ToString())
                    lastRow.CreateCell(4).SetCellValue(dr("Total_Amount").ToString())
                    lastRow.CreateCell(5).SetCellValue(dr("Total_Discount").ToString())
                    lastRow.CreateCell(6).SetCellValue(dr("Total_Percentage").ToString())
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

    Private Function GetKOTReporDataSet(ByVal request As SpectrumCommon.DayCloseReportModel) As DataSet
        Try
            Dim query As String = "exec kotreport '" & request.FromDate.ToString("yyyy-MM-dd") & "' ,'" & request.ToDate.ToString("yyyy-MM-dd") & "' ,'" & request.SiteCode & "'"
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
            path = DayCloseReportPath & "\KOTReport_" & siteName & "_" & request.FromDate.ToString("dd-MM-yyyy") & "_To_" & request.ToDate.ToString("dd-MM-yyyy") & ".xls"
            Return path
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

End Class
