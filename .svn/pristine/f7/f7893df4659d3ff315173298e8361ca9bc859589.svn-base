Imports SpectrumCommon
Imports iTextSharp.text
Imports iTextSharp.text.pdf


Public Class DayCloseReportController
    Inherits ReportBase
    Implements IReports

    'Private Const DayCloseReportPath As String = "D:\DayCloseReports"

    Private Shared _DayCloseReportPath As String = "D:\DayCloseReports"
    Public Shared Property DayCloseReportPath() As String
        Get
            Return _DayCloseReportPath
        End Get
        Set(ByVal value As String)
            _DayCloseReportPath = value
        End Set
    End Property

    Private Shared _PathForEmail As String
    Public Shared Property PathForEmail() As String
        Get
            Return _PathForEmail
        End Get
        Set(ByVal value As String)
            _PathForEmail = value
        End Set
    End Property

    Private _ReportSegments As New Dictionary(Of IDayCloseReport, Boolean)
    Public Sub New()
        _ReportSegments.Add(New TotalSales, True) '--- Day Close Summary
        _ReportSegments.Add(New SalesByCashier, True) '--- Cashier Wise Sale
        _ReportSegments.Add(New IncomeDetails, True)  '--- Tender Wise Net Sales Details
        _ReportSegments.Add(New IncomeDetails, True)  '--- Tax Details
        '--- Credit/Debit Card Details
        '--- Currency Details
        _ReportSegments.Add(New DayCloseStatistics, True) '--- Statistice
        '---- List of Corrected Cash Memo 
        _ReportSegments.Add(New DiscountDetails, True)
        '---- List Of Deleted Cash Memo 
        '---- Inconsistency Details
        '---- Floating Details 
        _ReportSegments.Add(New FloatingDetails, True)
    End Sub

    Public Function GenerateDayCloseReport(ByVal request As SpectrumCommon.DayCloseReportModel, reportPath As String) As Boolean Implements IReports.GenerateDayCloseReport
        Try
            Dim result As Boolean
            If Not String.IsNullOrEmpty(reportPath) Then
                DayCloseReportPath = reportPath
            End If

            Dim doc As Document = PdfHelper.Instance.GetPdfDocument()
            Dim path As String = GetDirectoryPath(request)

            If Not String.IsNullOrEmpty(path) Then
                Dim writer As PdfWriter = PdfHelper.Instance.CreatePdfWriter(doc, path)
                doc.Open()
                Dim cb As PdfContentByte = writer.DirectContent
                cb.SetLineDash(3, 1, 0)
                doc.Add(New Paragraph("Day Close Report", GetHeaderFont()) With {.Alignment = 1})
                doc.Add(New Phrase(Environment.NewLine))
                DrawStrokedLine(doc)
                doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Phrase("Site Code : " & request.SiteCode & "", GetContentFontBold()))
                doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Phrase("Site Name : " & GetSiteName(request.SiteCode) & "", GetContentFontBold()))
                doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Phrase("Closing Date : " & request.ToDate & "", GetContentFontBold()))
                doc.Add(New Phrase(Environment.NewLine))
                DrawStrokedLine(doc)
                doc.Add(New Phrase(Environment.NewLine))
                'For Each item In _ReportSegments
                '    If item.Value Then
                '        item.Key.CreateReport(request, doc)
                '    End If
                'Next
                Dim dayClose As New DayCloseUsingProc()
                result = dayClose.PrintDayCloseReport(request, doc)

                doc.Close()
                writer.Dispose()
                If result Then
                    Process.Start(path)
                    result = True
                Else
                    'delete pdf in case of  any failure occured in database level
                    'added by vipul on 07-03-2018- 
                    'blank report generete and mail sent issue-2623
                    If System.IO.File.Exists(path) = True Then
                        System.IO.File.Delete(path)
                    End If
                    PathForEmail = ""
                    MsgBox(" Unable to Generate Day Close Report ")
                End If
            End If
            Return result
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    'Public Sub GenerateDayCloseReport(ByVal request As DayCloseReportModel)
    '    Try
    '        Dim doc As Document = PdfHelper.Instance.GetPdfDocument()
    '        Dim path As String = GetDirectoryPath(request)
    '        If Not String.IsNullOrEmpty(path) Then
    '            Dim writer As PdfWriter = PdfHelper.Instance.CreatePdfWriter(doc, path)
    '            doc.Open()
    '            Dim cb As PdfContentByte = writer.DirectContent
    '            cb.SetLineDash(3, 1, 0)
    '            doc.Add(New Paragraph("Day Close Report", GetHeaderFont()) With {.Alignment = 1})
    '            doc.Add(New Phrase(Environment.NewLine))
    '            DrawStrokedLine(doc)
    '            doc.Add(New Phrase(Environment.NewLine))
    '            doc.Add(New Phrase("SiteCode : " & request.SiteCode & "", GetContentFontBold()))
    '            doc.Add(New Phrase(Environment.NewLine))
    '            doc.Add(New Phrase("Store Name : " & GetSiteName(request.SiteCode) & "", GetContentFontBold()))
    '            doc.Add(New Phrase(Environment.NewLine))
    '            DrawStrokedLine(doc)
    '            doc.Add(New Phrase(Environment.NewLine))
    '            For Each item In _ReportSegments
    '                If item.Value Then
    '                    item.Key.CreateReport(request, doc)
    '                End If
    '            Next
    '            doc.Close()
    '            writer.Dispose()
    '            Process.Start(path)
    '        End If
    '    Catch ex As Exception
    '        LogException(ex)
    '    End Try
    'End Sub

    Private Function GetDirectoryPath(ByRef request As DayCloseReportModel) As String
        Try
            Dim path As String = String.Empty
            Dim siteName As String = GetSiteName(request.SiteCode)
            If Not System.IO.Directory.Exists(DayCloseReportPath) Then
                System.IO.Directory.CreateDirectory(DayCloseReportPath)
            End If
            path = DayCloseReportPath & "\DayCloseReport_" & siteName & "_" & request.ToDate.ToString("dd-MM-yyyy") & ".pdf"
            PathForEmail = path
            Return path
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

End Class
