Imports SpectrumCommon
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class CorrectedCashMemo
    Inherits ReportBase
    Implements IDayCloseReport

    Private _DtCorrectedCashMemo As DataTable
    Public Property DtCorrectedCashMemo() As DataTable
        Get
            Return _DtCorrectedCashMemo
        End Get
        Set(ByVal value As DataTable)
            _DtCorrectedCashMemo = value
        End Set
    End Property


    Public Sub CreateReport(ByRef request As SpectrumCommon.DayCloseReportModel, ByRef doc As iTextSharp.text.Document) Implements IDayCloseReport.CreateReport
        Try
            doc.Add(New Paragraph("List of Corrected Cash Memos", GetContentFontBold()) With {.Alignment = 1})
            PrintDataTable(DtCorrectedCashMemo, doc)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

End Class
