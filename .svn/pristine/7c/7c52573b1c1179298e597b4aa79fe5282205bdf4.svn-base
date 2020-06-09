Imports SpectrumCommon
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class CardDetails
    Inherits ReportBase
    Implements IDayCloseReport

    Private _DtCardDetails As DataTable
    Public Property DtCardDetails() As DataTable
        Get
            Return _DtCardDetails
        End Get
        Set(ByVal value As DataTable)
            _DtCardDetails = value
        End Set
    End Property


    Public Sub CreateReport(ByRef request As SpectrumCommon.DayCloseReportModel, ByRef doc As iTextSharp.text.Document) Implements IDayCloseReport.CreateReport
        Try
            doc.Add(New Paragraph("Credit/Debit Card Details", GetContentFontBold()) With {.Alignment = 1})
            If DtCardDetails.Rows.Count > 0 Then
                tblSumValue = DtCardDetails.Compute("SUM(Amount)", "")
            End If
            PrintDataTable(DtCardDetails, doc)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


End Class
