Imports SpectrumCommon
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class TaxDetails
    Inherits ReportBase
    Implements IDayCloseReport

    Private _DtTaxDetails As DataTable
    Public Property DtTaxDetails() As DataTable
        Get
            Return _DtTaxDetails
        End Get
        Set(ByVal value As DataTable)
            _DtTaxDetails = value
        End Set
    End Property


    Public Sub CreateReport(ByRef request As SpectrumCommon.DayCloseReportModel, ByRef doc As iTextSharp.text.Document) Implements IDayCloseReport.CreateReport
        Try
            doc.Add(New Paragraph("Tax Details", GetContentFontBold()) With {.Alignment = 1})
            If DtTaxDetails.Rows.Count > 0 Then
                tblSumValue = DtTaxDetails.Compute("SUM(Amount)", "")
            End If

            PrintDataTable(DtTaxDetails, doc)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


End Class
