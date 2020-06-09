Imports SpectrumCommon
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class BankSummaryDetails
    Inherits ReportBase
    Implements IDayCloseReport

    Public Sub CreateReport(ByRef request As SpectrumCommon.DayCloseReportModel, ByRef doc As iTextSharp.text.Document) Implements IDayCloseReport.CreateReport
        Try
            Dim dt As DataTable        
            doc.Add(New Phrase("Bank Float Details", GetHeaderFont()))
            dt = GetBankDetailsFloatingDetaills(request)
            PrintDataTable(dt, doc)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function GetBankDetailsFloatingDetaills(ByRef request As DayCloseReportModel) As DataTable
        Try
            Dim query As String = "select CurrencyCode As [Currency Code] ,DenominationAmt As [Denomination Amount] ,Qty As [Quantity],TotalAmount As [Total Amount] " & _
               "from DayCloseBankDetails where SiteCode = '" & request.SiteCode & "' and CloseDate = '" & request.ToDate.ToString("yyyy-MM-dd") & "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
End Class
