Imports SpectrumCommon
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class DiscountDetails
    Inherits ReportBase
    Implements IDayCloseReport

    Private _DtDiscount As DataTable
    Public Property DtDiscount() As DataTable
        Get
            Return _DtDiscount
        End Get
        Set(ByVal value As DataTable)
            _DtDiscount = value
        End Set
    End Property


    Public Sub CreateReport(ByRef request As SpectrumCommon.DayCloseReportModel, ByRef doc As iTextSharp.text.Document) Implements IDayCloseReport.CreateReport
        Try
            doc.Add(New Paragraph("Discount Details", GetContentFontBold()) With {.Alignment = 1})
            If DtDiscount.Rows.Count > 0 Then
                tblSumValue = DtDiscount.Compute("SUM([Promotion Value])", "")
            End If
            PrintDataTable(DtDiscount, doc)
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Function GetDiscountDataSet(ByVal request As DayCloseReportModel) As DataTable
        Try
            '            Dim query As String = "SELECT A.PROMOTIONID As 'Promotion',  " & _
            '"isnull(B.OFFERNAME,M.PromotionName) AS 'Promotion Name', Convert(Numeric(18,2), sum(A.PromotionValue))" & _
            '"as 'Promotion Value' FROM SALESDISCDTL A Left Outer JOIN PROMOTIONS B ON " & _
            '"A.PROMOTIONID = B.OFFERNO Left Outer JOIN ManualPROMOTION M ON " & _
            '"A.PROMOTIONID = M.PROMOTIONID left outer join cashmemohdr hdr " & _
            '"on A.Sitecode=hdr.SiteCode and A.FinYear=Hdr.FinYear AND " & _
            '"A.BillNo=hdr.BillNo WHERE convert(datetime,CONVERT(VARCHAR(10), A.BILLDATE ,101)) ='" & request.ToDate.ToString("yyyy-MM-dd") & "' AND  " & _
            '"Hdr.billintermediatestatus <>'Deleted' AND A.PromotionID <> '' AND Hdr.SiteCode = '" & request.SiteCode & "' group by  " & _
            ' "           A.SITECODE, A.PROMOTIONID, B.OFFERNAME, M.PromotionName"

            '----- Changes Done By Mahesh for data same as BO
            Dim query As String = "SELECT A.SITECODE,A.PROMOTIONID, " _
                                + " isnull(B.OFFERNAME,M.PromotionName) AS DESCRIPTION,sum(hdr.totalDiscount)" _
                                + " as PROMOTIONVALUE FROM SALESDISCDTL A Left Outer JOIN PROMOTIONS B ON " _
                                + " A.PROMOTIONID = B.OFFERNO Left Outer JOIN ManualPROMOTION M ON " _
                                + " A.PROMOTIONID = M.PROMOTIONID left outer join cashmemohdr hdr " _
                                + " on A.Sitecode=hdr.SiteCode and A.FinYear=Hdr.FinYear AND " _
                                + " A.BillNo=hdr.BillNo WHERE convert(datetime,CONVERT(VARCHAR(10), A.BILLDATE ,101)) ='" & request.ToDate.ToString("yyyy-MM-dd") & "' AND " _
                                + " A.SITECODE='" & request.SiteCode & "' AND " _
                                + " Hdr.billintermediatestatus <>'Deleted'  " _
                                + " group by A.SITECODE,A.PROMOTIONID, B.OFFERNAME,M.PromotionName"

            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
End Class
