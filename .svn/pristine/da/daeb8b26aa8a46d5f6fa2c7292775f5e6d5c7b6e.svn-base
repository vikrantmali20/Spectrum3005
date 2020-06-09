Imports SpectrumCommon
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class DeletedCashMemo
    Inherits ReportBase
    Implements IDayCloseReport

    Private _DtDeletedCashMemo As DataTable
    Public Property DtDeletedCashMemo() As DataTable
        Get
            Return _DtDeletedCashMemo
        End Get
        Set(ByVal value As DataTable)
            _DtDeletedCashMemo = value
        End Set
    End Property


    Public Sub CreateReport(ByRef request As SpectrumCommon.DayCloseReportModel, ByRef doc As iTextSharp.text.Document) Implements IDayCloseReport.CreateReport
        Try
            doc.Add(New Paragraph("List of Deleted Cash Memos", GetContentFontBold()) With {.Alignment = 1})
            PrintDataTable(DtDeletedCashMemo, doc)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
End Class
