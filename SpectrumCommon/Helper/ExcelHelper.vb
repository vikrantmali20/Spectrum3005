Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.HSSF.UserModel
Imports NPOI.HPSF

Public Class ExcelHelper
    Private Sub New()

    End Sub

    Private Shared _Instance As ExcelHelper
    Public Shared ReadOnly Property Instance As ExcelHelper
        Get
            If _Instance Is Nothing Then
                _Instance = New ExcelHelper()
            End If
            Return _Instance
        End Get
    End Property

    Public Function GetBoldFont(ByRef hssfworkbook As HSSFWorkbook) As ICellStyle
        Try
            Dim font1 As IFont = hssfworkbook.CreateFont()
            font1.Boldweight = DirectCast(FontBoldWeight.BOLD, Short)
            Dim style1 As ICellStyle = hssfworkbook.CreateCellStyle()
            style1.SetFont(font1)
            Return style1
        Catch ex As Exception          
            Return Nothing
        End Try
    End Function

    Public Sub SetCellStyle(ByRef row As IRow, ByRef style As ICellStyle)
        Try
            For Each cell In row.Cells
                cell.CellStyle = style
            Next
        Catch ex As Exception           
        End Try
    End Sub


    Public Function InitializeWorkbook(Optional path As String = "") As HSSFWorkbook
        Try
            Dim hssfworkbook As HSSFWorkbook
            If path = String.Empty Then
                hssfworkbook = New HSSFWorkbook()
            Else
                Using file As FileStream = New FileStream(path, FileMode.Open, FileAccess.Read)
                    hssfworkbook = New HSSFWorkbook(file)
                End Using
            End If


            'create a entry of DocumentSummaryInformation
            Dim dsi As DocumentSummaryInformation = PropertySetFactory.CreateDocumentSummaryInformation()
            dsi.Company = "NPOI Team"
            HSSFWorkbook.DocumentSummaryInformation = dsi

            'create a entry of SummaryInformation
            Dim si As SummaryInformation = PropertySetFactory.CreateSummaryInformation()
            si.Subject = "NPOI SDK Example"
            HSSFWorkbook.SummaryInformation = si
            Return HSSFWorkbook
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub WriteToFile(ByVal path As String, ByRef hssfworkbook As HSSFWorkbook)
        Try
            'Write the stream data of workbook to the root directory
            Dim file As FileStream = New FileStream(path, FileMode.Create)
            hssfworkbook.Write(file)
            file.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class
