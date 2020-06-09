Imports NPOI.HSSF.UserModel
Imports NPOI.HPSF
Imports NPOI.POIFS.FileSystem
Imports NPOI.SS.UserModel
Imports System.IO

Public Class ExcelReportBase
    Inherits clsCommon

    Public Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            'GC.Collect()
        End Try
    End Sub

    Protected Function GetBoldFont(ByRef hssfworkbook As HSSFWorkbook) As ICellStyle
        Try
            Dim font1 As IFont = hssfworkbook.CreateFont()
            font1.Boldweight = DirectCast(FontBoldWeight.BOLD, Short)
            Dim style1 As ICellStyle = hssfworkbook.CreateCellStyle()
            style1.SetFont(font1)
            Return style1
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Protected Sub SetCellStyle(ByRef row As IRow, ByRef style As ICellStyle)
        Try
            For Each cell In row.Cells
                cell.CellStyle = style
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Protected Sub SetCellStyle(ByRef cell As ICell, ByRef style As ICellStyle)
        Try
            cell.CellStyle = style
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Protected Function GetRow(ByRef rowno As Integer, ByRef sheet As ISheet) As IRow
        Dim row As IRow = sheet.GetRow(rowno)
        If row IsNot Nothing Then
            Return row
        Else
            Return sheet.CreateRow(rowno)
        End If
    End Function


    Protected Function InitializeWorkbook() As HSSFWorkbook
        Try
            Dim hssfworkbook As HSSFWorkbook = New HSSFWorkbook()

            'create a entry of DocumentSummaryInformation
            Dim dsi As DocumentSummaryInformation = PropertySetFactory.CreateDocumentSummaryInformation()
            dsi.Company = "NPOI Team"
            hssfworkbook.DocumentSummaryInformation = dsi

            'create a entry of SummaryInformation
            Dim si As SummaryInformation = PropertySetFactory.CreateSummaryInformation()
            si.Subject = "NPOI SDK Example"
            hssfworkbook.SummaryInformation = si
            Return hssfworkbook
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Protected Sub WriteToFile(ByVal path As String, ByRef hssfworkbook As HSSFWorkbook)

        'Write the stream data of workbook to the root directory
        Dim file As FileStream = New FileStream(path, FileMode.Create)       
        hssfworkbook.Write(file)
        file.Close()
    End Sub
End Class
