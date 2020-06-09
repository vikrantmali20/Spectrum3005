Imports SpectrumCommon
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class BankReport
    Inherits ReportBase
    Implements IReports

    Private DayCloseReportPath As String = "D:\DayCloseReports"
    Private _ReportSegments As New Dictionary(Of IDayCloseReport, Boolean)
    Public Sub New()
        _ReportSegments.Add(New BankSummaryDetails, True)       
    End Sub
    Public Function GenerateDayCloseReport(ByVal request As SpectrumCommon.DayCloseReportModel, reportPath As String) As Boolean Implements IReports.GenerateDayCloseReport
        Try
            If Not String.IsNullOrEmpty(reportPath) Then
                DayCloseReportPath = reportPath
            End If
            Dim result As Boolean
            Dim doc As Document = PdfHelper.Instance.GetPdfDocument()
            Dim path As String = GetDirectoryPath(request)
            If Not String.IsNullOrEmpty(path) Then
                Dim writer As PdfWriter = PdfHelper.Instance.CreatePdfWriter(doc, path)
                doc.Open()
                Dim cb As PdfContentByte = writer.DirectContent
                cb.SetLineDash(3, 1, 0)
                doc.Add(New Paragraph("Bank Report", GetHeaderFont()) With {.Alignment = 1})
                doc.Add(New Phrase(Environment.NewLine))
                DrawStrokedLine(doc)
                doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Phrase("SiteCode : " & request.SiteCode & "", GetContentFontBold()))
                doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Phrase("Store Name : " & GetSiteName(request.SiteCode) & "", GetContentFontBold()))
                doc.Add(New Phrase(Environment.NewLine))
                DrawStrokedLine(doc)
                doc.Add(New Phrase(Environment.NewLine))
                For Each item In _ReportSegments
                    If item.Value Then
                        item.Key.CreateReport(request, doc)
                    End If
                Next
                doc.Close()
                writer.Dispose()
                Process.Start(path)
                result = True
            End If
            Return result
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Function GetDirectoryPath(ByRef request As DayCloseReportModel) As String
        Try
            Dim path As String = String.Empty
            Dim siteName As String = GetSiteName(request.SiteCode)
            If Not System.IO.Directory.Exists(DayCloseReportPath) Then
                System.IO.Directory.CreateDirectory(DayCloseReportPath)
            End If
            path = DayCloseReportPath & "\BankReport_" & siteName & "_" & request.ToDate.ToString("dd-MM-yyyy") & ".pdf"
            Return path
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
End Class
