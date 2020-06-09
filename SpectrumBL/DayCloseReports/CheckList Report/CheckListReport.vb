﻿Imports System.Data.SqlClient
Imports SpectrumCommon
Imports NPOI.HSSF.UserModel
Imports NPOI.HPSF
Imports NPOI.POIFS.FileSystem
Imports NPOI.SS.UserModel
Imports System.IO
Public Class CheckListReport
    Inherits ExcelReportBase
    Implements IReports
    Private DayCloseReportPath As String = "D:\DayCloseReports"
    Public Function GenerateDayCloseReport(request As DayCloseReportModel, reportPath As String) As Boolean Implements IReports.GenerateDayCloseReport
        Try
            If Not String.IsNullOrEmpty(reportPath) Then
                DayCloseReportPath = reportPath
            End If
            Dim path As String = GetDirectoryPath(request)
            Dim ds As DataSet = GetCheckListDataSet(request)
            If ds.Tables IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso Not String.IsNullOrEmpty(path) Then
                Dim hssfworkbook As HSSFWorkbook = InitializeWorkbook()
                Dim sheet1 As ISheet = hssfworkbook.CreateSheet("CheckListReport")
                Dim siteName As String = GetSiteName(request.SiteCode)
                Dim lastRow As IRow = sheet1.CreateRow(0)
                lastRow.CreateCell(0).SetCellValue("Name : Check List Report")
                SetCellStyle(lastRow, GetBoldFont(hssfworkbook))
                lastRow = sheet1.CreateRow(1)
                lastRow.CreateCell(0).SetCellValue("Store name : " & siteName)
                SetCellStyle(lastRow, GetBoldFont(hssfworkbook))
                lastRow = sheet1.CreateRow(2)
                lastRow.CreateCell(0).SetCellValue("Genrated by  + Date + time : " & request.CreatedBy & ", " & request.CreatedOn.ToString())
                SetCellStyle(lastRow, GetBoldFont(hssfworkbook))
                lastRow = sheet1.CreateRow(3)
                lastRow.CreateCell(0).SetCellValue("Check List Report for period : " & request.FromDate.ToShortDateString())
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
                For Each item As DataRow In ds.Tables(0).Rows
                    For Each cell In item.ItemArray()
                        lastRow.CreateCell(col).SetCellValue(cell.ToString())
                        col += 1
                    Next
                    col = 0
                    row += 1
                    lastRow = sheet1.CreateRow(row)
                Next
                row += 1
                lastRow = sheet1.CreateRow(row)
                'For Each dr As DataRow In ds.Tables(1).Rows
                '    lastRow.CreateCell(4).SetCellValue("Total :")
                '    lastRow.CreateCell(5).SetCellValue(dr(0).ToString())
                '    lastRow.CreateCell(6).SetCellValue(dr(1).ToString())
                '    SetCellStyle(lastRow, GetBoldFont(hssfworkbook))
                '    row += 1
                '    lastRow = sheet1.CreateRow(row)
                'Next
                sheet1.DefaultColumnWidth = 20
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

    Private Function GetCheckListDataSet(ByVal request As DayCloseReportModel) As DataSet
        Try
            Dim query As String = "select st.SITECODE as [Site Code],SITESHORTNAME as [Site Name],hdr.TerminalID as [Terminal Id], CheckListOpenTime as [Check List Open Time] ,CheckListCloseTime as [Check List Close Time],hdr.CheckListId as [Check List Id],dtl.SrNo as [Sr No.] ,dtl.Question ,dtl.Answer, Convert(datetime,Convert(varchar(12),hdr.CheckListOpenTime ,100)) as [Check List Open Date] from CheckListHdr hdr  inner join CheckListDtl dtl  on hdr.DocumentNumber=dtl.DocumentNumber and hdr.CheckListId=dtl.CheckListId and hdr.SiteCode=dtl.SiteCode  inner join MSTSite st on hdr.SiteCode=st.SiteCode and dtl.SiteCode=st.SiteCode where Convert(datetime,Convert(varchar(12),hdr.CheckListOpenTime ,100))='" & request.ToDate.ToString("yyyy-MM-dd") & "' and st.SITECODE='" & request.SiteCode & "'"
            Dim dataAdapter As SqlDataAdapter
            Dim reportSet As New DataSet
            dataAdapter = New SqlDataAdapter(query, SpectrumCon)
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
            path = DayCloseReportPath & "\CheckListReport_" & siteName & "_" & request.FromDate.ToString("dd-MM-yyyy") & "_To_" & request.ToDate.ToString("dd-MM-yyyy") & ".xls"
            Return path
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
End Class
