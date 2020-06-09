Imports SpectrumCommon
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class CurrencyDetails
    Inherits ReportBase
    Implements IDayCloseReport

    Private _DtCurrency As DataTable
    Public Property DtCurrency() As DataTable
        Get
            Return _DtCurrency
        End Get
        Set(ByVal value As DataTable)
            _DtCurrency = value
        End Set
    End Property

    Public Sub CreateReport(ByRef request As SpectrumCommon.DayCloseReportModel, ByRef doc As iTextSharp.text.Document) Implements IDayCloseReport.CreateReport
        Try
            doc.Add(New Paragraph("Currency Details", GetContentFontBold()) With {.Alignment = 1})
            PrintDataTable(DtCurrency, doc)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

End Class
