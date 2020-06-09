Imports SpectrumCommon
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class SalesByCashier
    Inherits ReportBase
    Implements IDayCloseReport
    Public Sub New()

    End Sub
    Private _DtCashier As DataTable
    Public Property DtCashier() As DataTable
        Get
            Return _DtCashier
        End Get
        Set(ByVal value As DataTable)
            _DtCashier = value
        End Set
    End Property


    Dim DtSumRow As DataTable()
    Public Sub CreateReport(ByRef request As DayCloseReportModel, ByRef doc As Document) Implements IDayCloseReport.CreateReport
        Try
            doc.Add(New Paragraph("Cashier Wise Sale", GetContentFontBold()) With {.Alignment = 1})
            'doc.Add(New Phrase(Environment.NewLine))
            'Dim dt As DataTable = GetSalesByCashierDataSet(request)
            If DtCashier.Rows.Count > 0 Then tblSumValue = DtCashier.Compute("SUM(Amount)", "")
            PrintDataTable(DtCashier, doc)
            'doc.Add(New Phrase(Space(176 - totalLine.Count - 2) & totalLine, GetContentFontBold()))
            'doc.Add(New Phrase(Environment.NewLine))
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function GetSalesByCashierDataSet(ByVal request As DayCloseReportModel) As DataTable
        Try
            'Dim query As String = "select AU.UserName AS Cashier, Convert(Numeric(18,2), SUM (hdr.netamt)) AS Amount from CashMemoHdr As hdr " & _
            '                      "inner join AuthUsers As AU ON hdr.CREATEDBY = AU.UserID " & _
            '                      "where hdr.billdate  ='" & request.ToDate.ToString("yyyy-MM-dd") & "' and hdr.SiteCode = '" & request.SiteCode & "' group by hdr.CREATEDBY  ,AU.UserName "

            'Dim query As String = " select CREATEDBY as [Cashier Name] , Convert(Numeric(18,2),sum(Amount)) Amount from  (select Convert(Numeric(18,2), " & _
            '                    "SUM (hdr.netamt)) AS Amount,CREATEDBY from CashMemoHdr As hdr  where hdr.billdate  ='" & request.ToDate.ToString("yyyy-MM-dd") & "' " & _
            '                    "and hdr.SiteCode ='" & request.SiteCode & "'  group by hdr.CREATEDBY union all " & _
            '                    "select SUM(isnull(Qty,0)*isnull(MRP,0)),CREATEDBY " & _
            '                    "Amount from OrderDtl where dayOpenDate='" & request.ToDate.ToString("yyyy-MM-dd") & "' and SiteCode ='" & request.SiteCode & "' group by CREATEDBY)temp " & _
            '                    "group by CREATEDBY"

            '----- Changes Done By Mahesh for data same as BO
            Dim query As String = " select Cashier,sum(netamount-isnull(returnAmount,0))  as Amount" _
                                   & " from VIEW_SalesReport  " _
                                   & " where convert(datetime,CONVERT(VARCHAR(10), billdate ,101)) ='" & request.ToDate.ToString("yyyy-MM-dd") & "' and sitecode ='" & request.SiteCode & "'" _
                                   & " group by Cashier "
                                   
            Return GetFilledTable(query)

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
End Class
