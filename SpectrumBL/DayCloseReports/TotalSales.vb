Imports SpectrumCommon
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class TotalSales
    Inherits ReportBase
    Implements IDayCloseReport

    Public Sub New()

    End Sub
    Private _DtDayClose As DataTable
    Public Property DtDayClose() As DataTable
        Get
            Return _DtDayClose
        End Get
        Set(ByVal value As DataTable)
            _DtDayClose = value
        End Set
    End Property


    Public Sub CreateReport(ByRef request As SpectrumCommon.DayCloseReportModel, ByRef doc As iTextSharp.text.Document) Implements IDayCloseReport.CreateReport
        Try
            'Dim totalSales As Double
            'Dim totalDiscount As Double = GetTotalDiscount(request)
            'Dim netSales As Double = GetNetSales(request)
            'Dim objIncomeDetails As New IncomeDetails()
            'Try
            '    ' Dim dt As DataTable = objIncomeDetails.GetDayCloseDataSet(request)
            '    Dim tblSumValue = dt.Compute("SUM(AMOUNT)", "TenderNature='+'") - dt.Compute("SUM(AMOUNT)", "TenderNature='-'")
            '    If Math.Abs(Val(tblSumValue)) > 0 Then
            '        netSales = tblSumValue
            '    End If
            'Catch ex As Exception
            '    LogException(ex)
            'End Try
            'TotalSales = totalDiscount + netSales
            'For index = 0 To DtDayClose.Rows.Count - 1
            '    doc.Add(New Phrase(DtDayClose.Rows(index)(0).ToString() & FormatNumber(DtDayClose.Rows(index)(1).ToString(), 2) & "", GetContentFontBold()) With {.Capacity = 2})
            '    doc.Add(New Phrase(Environment.NewLine))
            'Next

            If DtDayClose IsNot Nothing AndAlso DtDayClose.Rows.Count > 0 Then
                Dim pdfTable As PdfPTable = New PdfPTable(DtDayClose.Columns.Count)
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT
                pdfTable.WidthPercentage = GetTableWidthPercent(DtDayClose.Columns.Count)
                pdfTable.DefaultCell.BorderWidth = 0
                pdfTable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT
                For Each row As DataRow In DtDayClose.Rows
                    For Each cell In row.ItemArray
                        pdfTable.AddCell(New Phrase(cell.ToString(), GetContentFontBold()))
                    Next
                Next
                pdfTable.HeaderRows = 1
                pdfTable.DefaultCell.BorderWidth = 0
                doc.Add(pdfTable)

            Else
                doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Phrase("NA", GetContentFontNormal()))
                doc.Add(New Phrase(Environment.NewLine))
            End If
            DrawStrokedLine(doc)
            doc.Add(New Phrase(Environment.NewLine))
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function GetTotalDiscount(ByVal request As SpectrumCommon.DayCloseReportModel) As Double
        Try
            Dim query As String = "SELECT A.PROMOTIONID As [Promotion],  " & _
                                  "isnull(B.OFFERNAME,M.PromotionName) AS [Promotion Name],sum(hdr.TotalDiscount)" & _
                                    " as [Promotion Value] FROM SALESDISCDTL A Left Outer JOIN PROMOTIONS B ON " & _
                                    " A.PROMOTIONID = B.OFFERNO Left Outer JOIN ManualPROMOTION M ON " & _
                                    " A.PROMOTIONID = M.PROMOTIONID left outer join cashmemohdr hdr " & _
                                    " on A.Sitecode=hdr.SiteCode and A.FinYear=Hdr.FinYear AND " & _
                                    " A.BillNo=hdr.BillNo WHERE convert(datetime,CONVERT(VARCHAR(10), A.BILLDATE ,101)) ='" & request.ToDate.ToString("yyyy-MM-dd") & "' AND  " & _
                                    " Hdr.billintermediatestatus <>'Deleted' AND A.PromotionID <> '' AND Hdr.SiteCode = '" & request.SiteCode & "' group by  " & _
                                    "       A.PROMOTIONID, B.OFFERNAME, M.PromotionName"

            Dim dt As DataTable = GetFilledTable(query)
            Dim totalDiscount As Double = dt.Compute("Sum([Promotion Value])", "")
            Return totalDiscount
        Catch ex As Exception
            LogException(ex)
            Return 0
        End Try
    End Function

    Private Function GetNetSales(ByVal request As SpectrumCommon.DayCloseReportModel) As Double
        Try
            'Dim query As String = "select AU.UserName AS Cashier, Convert(Numeric(18,2), SUM (hdr.netamt)) AS Amount from CashMemoHdr As hdr " & _
            '                      "inner join AuthUsers As AU ON hdr.CREATEDBY = AU.UserID " & _
            '                      "where hdr.billdate  ='" & request.ToDate.ToString("yyyy-MM-dd") & "' and hdr.SiteCode = '" & request.SiteCode & "' group by hdr.CREATEDBY  ,AU.UserName "
            Dim query As String = "select Convert(Numeric(18,2),sum(Amount))TotalAmount from  " & _
                                   "(select Convert(Numeric(18,2), SUM (hdr.netamt)) AS Amount from CashMemoHdr As hdr " & _
                                   " where hdr.billdate  ='" & request.ToDate.ToString("yyyy-MM-dd") & "' and hdr.SiteCode ='" & request.SiteCode & "'  " & _
                                    " " & _
                                   "union all " & _
                                   "select SUM(isnull(Qty,0)*isnull(MRP,0)) Amount from OrderDtl where dayOpenDate='" & request.ToDate.ToString("yyyy-MM-dd") & "' and SiteCode ='" & request.SiteCode & "')temp "
            Dim dt As DataTable = GetFilledTable(query)
            Dim netSales As Double = dt.Compute("Sum(TotalAmount)", "")
            Return netSales
        Catch ex As Exception
            LogException(ex)
            Return 0
        End Try
    End Function
End Class
