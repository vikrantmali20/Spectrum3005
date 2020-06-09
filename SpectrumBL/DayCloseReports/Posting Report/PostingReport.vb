Imports System.Data.SqlClient
Imports SpectrumCommon
Imports NPOI.HSSF.UserModel
Imports NPOI.HPSF
Imports NPOI.POIFS.FileSystem
Imports NPOI.SS.UserModel
Imports System.IO
Public Class PostingReport
    Inherits ExcelReportBase
    Implements IReports

    Private DayCloseReportPath As String = "D:\DayCloseReports"

    Public Function GenerateDayCloseReport(request As DayCloseReportModel, reportPath As String) As Boolean Implements IReports.GenerateDayCloseReport
        Try
            If Not String.IsNullOrEmpty(reportPath) Then
                DayCloseReportPath = reportPath
            End If
            Dim path As String = GetDirectoryPath(request)
            Dim ds As DataSet = GetPostingReportDataSet(request)
            If ds.Tables IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso Not String.IsNullOrEmpty(path) Then
                Dim hssfworkbook As HSSFWorkbook = InitializeWorkbook()
                Dim sheet1 As ISheet = hssfworkbook.CreateSheet("Posting report")
                Dim siteName As String = GetSiteName(request.SiteCode)
                Dim lastRow As IRow = sheet1.CreateRow(0)
                lastRow.CreateCell(0).SetCellValue("Name : Posting report")
                SetCellStyle(lastRow, GetBoldFont(hssfworkbook))
                lastRow = sheet1.CreateRow(1)
                lastRow.CreateCell(0).SetCellValue("Store name : " & siteName)
                SetCellStyle(lastRow, GetBoldFont(hssfworkbook))
                lastRow = sheet1.CreateRow(2)
                lastRow.CreateCell(0).SetCellValue("Genrated by  + Date + time : " & request.CreatedBy & ", " & request.CreatedOn.ToString())
                SetCellStyle(lastRow, GetBoldFont(hssfworkbook))
                lastRow = sheet1.CreateRow(3)
                lastRow.CreateCell(0).SetCellValue("Posting report for period : " & request.FromDate.ToShortDateString() & " to " & request.ToDate.ToShortDateString())
                SetCellStyle(lastRow, GetBoldFont(hssfworkbook))

                Dim col As Integer = 0
                lastRow = sheet1.CreateRow(6)
                lastRow.CreateCell(0).SetCellValue("Particulars")
                lastRow.CreateCell(1).SetCellValue("Debit")
                lastRow.CreateCell(2).SetCellValue("Particulars")
                lastRow.CreateCell(3).SetCellValue("Credit")
                SetCellStyle(lastRow, GetBoldFont(hssfworkbook))

                lastRow = sheet1.CreateRow(7)
                lastRow.CreateCell(1).SetCellValue("Rs.")
                lastRow.CreateCell(3).SetCellValue("Rs.")
                SetCellStyle(lastRow, GetBoldFont(hssfworkbook))

                lastRow = sheet1.CreateRow(8)
                lastRow.CreateCell(0).SetCellValue("Cash")
                lastRow.CreateCell(2).SetCellValue("Net Sales")
                SetCellStyle(lastRow, GetBoldFont(hssfworkbook))

                Dim row As Integer = 9
                lastRow = sheet1.CreateRow(row)
                lastRow.CreateCell(0).SetCellValue("Cash Sales")
                Dim totalCash As String = IIf(ds.Tables(0).Rows.Count > 0 AndAlso IsDBNull(ds.Tables(0).Rows(0)(1)) = False, ds.Tables(0).Rows(0)(1).ToString(), String.Empty)
                lastRow.CreateCell(1).SetCellValue(totalCash)
                row += 1
                lastRow = sheet1.CreateRow(row)
                lastRow.CreateCell(0).SetCellValue("Sub Total :")
                lastRow.CreateCell(1).SetCellValue(totalCash)
                SetCellStyle(lastRow.Cells(0), GetBoldFont(hssfworkbook))
                SetCellStyle(lastRow.Cells(1), GetBoldFont(hssfworkbook))
                row += 2
                lastRow = sheet1.CreateRow(row)
                lastRow.CreateCell(0).SetCellValue("Credit Card(Credit Sales)")
                SetCellStyle(lastRow.Cells(0), GetBoldFont(hssfworkbook))
                For Each dr As DataRow In ds.Tables(1).Rows
                    lastRow.CreateCell(0).SetCellValue(dr(0).ToString())
                    lastRow.CreateCell(1).SetCellValue(dr(1).ToString())
                    row += 1
                    lastRow = sheet1.CreateRow(row)
                Next
                lastRow.CreateCell(0).SetCellValue("Sub Total :")
                Dim totalCredit As String = IIf(ds.Tables(2).Rows.Count > 0 AndAlso IsDBNull(ds.Tables(2).Rows(0)(0)) = False, ds.Tables(2).Rows(0)(0).ToString(), String.Empty)
                lastRow.CreateCell(1).SetCellValue(totalCredit)
                SetCellStyle(lastRow.Cells(0), GetBoldFont(hssfworkbook))
                SetCellStyle(lastRow.Cells(1), GetBoldFont(hssfworkbook))

                row = 9
                lastRow = GetRow(row, sheet1)
                For Each dr As DataRow In ds.Tables(4).Rows
                    lastRow.CreateCell(2).SetCellValue(dr(0).ToString())
                    lastRow.CreateCell(3).SetCellValue(dr(1).ToString())
                    row += 1
                    lastRow = GetRow(row, sheet1)
                Next

                lastRow.CreateCell(2).SetCellValue("Sub Total :")
                lastRow.CreateCell(3).SetCellValue(ds.Tables(4).Compute("Sum(Amount)", "").ToString())
                SetCellStyle(lastRow.Cells(0), GetBoldFont(hssfworkbook))
                SetCellStyle(lastRow.Cells(1), GetBoldFont(hssfworkbook))
                row += 2
                lastRow = GetRow(row, sheet1)
                lastRow.CreateCell(2).SetCellValue("GV")
                lastRow.CreateCell(3).SetCellValue(ds.Tables(5).Rows(0)(0).ToString())

                row += 2
                lastRow = GetRow(row, sheet1)
                For Each dr As DataRow In ds.Tables(7).Rows
                    lastRow.CreateCell(2).SetCellValue(dr(0).ToString())
                    lastRow.CreateCell(3).SetCellValue(dr(1).ToString())
                    row += 1
                    lastRow = GetRow(row, sheet1)
                Next

                row += 2
                lastRow = GetRow(row, sheet1)
                lastRow.CreateCell(2).SetCellValue("Discount")
                lastRow.CreateCell(3).SetCellValue(ds.Tables(6).Rows(0)(0).ToString())



                lastRow = sheet1.CreateRow(sheet1.LastRowNum + 2)
                lastRow.CreateCell(0).SetCellValue("Total Collection")
                lastRow.CreateCell(2).SetCellValue("Total Collection")
                lastRow.CreateCell(1).SetCellValue(ds.Tables(3).Rows(0)(0).ToString())
                lastRow.CreateCell(3).SetCellValue(ds.Tables(3).Rows(0)(0).ToString())
                SetCellStyle(lastRow, GetBoldFont(hssfworkbook))
                sheet1.DefaultColumnWidth = 25
                WriteToFile(path, hssfworkbook)
                System.Diagnostics.Process.Start(path)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Function GetPostingReportDataSet(ByVal request As DayCloseReportModel) As DataSet
        Try
            Dim query As String = "exec USP_PostingReport '" & request.FromDate.ToString("yyyy-MM-dd") & "' ,'" & request.ToDate.ToString("yyyy-MM-dd") & "' ,'" & request.SiteCode & "'"
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
            path = DayCloseReportPath & "\PostingReport_" & siteName & "_" & request.FromDate.ToString("dd-MM-yyyy") & "_To_" & request.ToDate.ToString("dd-MM-yyyy") & ".xls"
            Return path
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
End Class
