Imports SpectrumCommon
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class InconsistencyDetails
    Inherits ReportBase
    Implements IDayCloseReport

    Private _DtInconsistancyDetails As DataTable
    Public Property DtInconsistancyDetails() As DataTable
        Get
            Return _DtInconsistancyDetails
        End Get
        Set(ByVal value As DataTable)
            _DtInconsistancyDetails = value
        End Set
    End Property

    Public Sub CreateReport(ByRef request As SpectrumCommon.DayCloseReportModel, ByRef doc As iTextSharp.text.Document) Implements IDayCloseReport.CreateReport
        Try
            doc.Add(New Paragraph("Inconsistancy Details", GetContentFontBold()) With {.Alignment = 1})
            If DtInconsistancyDetails.Rows.Count > 0 Then tblSumValue = DtInconsistancyDetails.Compute("SUM(Amount)", "")
            PrintDataTable(DtInconsistancyDetails, doc)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
End Class
