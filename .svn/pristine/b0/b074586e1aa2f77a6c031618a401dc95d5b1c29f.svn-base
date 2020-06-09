Imports SpectrumCommon
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class DayCloseStatistics
    Inherits ReportBase
    Implements IDayCloseReport

    Private _DtStatistics As DataTable
    Public Property DtStatistics() As DataTable
        Get
            Return _DtStatistics
        End Get
        Set(ByVal value As DataTable)
            _DtStatistics = value
        End Set
    End Property


    Public Sub CreateReport(ByRef request As SpectrumCommon.DayCloseReportModel, ByRef doc As iTextSharp.text.Document) Implements IDayCloseReport.CreateReport
        Try
            'doc.Add(New Phrase("Statistics", GetHeaderFont()))
            doc.Add(New Paragraph("Statistics", GetContentFontBold()) With {.Alignment = 1})
            HideTableColumn = True
            'Dim dt As DataTable = GetStatisticsDataSet(request)
            PrintDataTable(DtStatistics, doc)
        Catch ex As Exception
            LogException(ex)            
        End Try
    End Sub

    Private Function GetStatisticsDataSet(ByVal request As SpectrumCommon.DayCloseReportModel) As DataTable
        Try
            Dim query As String = "select count(billno) as  " & _
"[Total Bills],Convert(Numeric(18,2), Sum(netamt)) as [Total Amount] ,sum(TotalItems) AS [Total Items] from " & _
"cashmemohdr where BillIntermediateStatus<> 'Deleted' And " & _
"BILLDATE= '" & request.ToDate.ToString("yyyy-MM-dd") & "' And SiteCode = '" & request.SiteCode & "' "
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
End Class
