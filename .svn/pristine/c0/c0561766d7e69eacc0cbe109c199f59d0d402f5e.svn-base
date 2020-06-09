Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.Data.SqlClient

Public Class ReportBase
    Inherits clsCommon
    Public Sub New()
    End Sub
    Public Function GetImprestCasheportData(ByVal sitecode As String, ByVal fromDate As Date, ByVal TodateDate As Date, ByVal TerminalId As String) As DataSet

        Try
            Dim dsDayClose As DataSet = Nothing
            Dim frmDate = fromDate.ToString("yyyy-MM-dd")
            Dim Todate = TodateDate.ToString("yyyy-MM-dd")
            Dim sqlComm As New SqlCommand("UDP_StandAloneImprestCashAmtReportData", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@V_SiteCode", sitecode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@FromDate", frmDate)
            sqlComm.Parameters.AddWithValue("@ToDate", Todate)
            sqlComm.Parameters.AddWithValue("@V_TerminalId", TerminalId)
            sqlComm.CommandType = CommandType.StoredProcedure
            dsDayClose = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsDayClose)
            Return dsDayClose
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function OrderTypeWiseSalesReport(ByVal sitecode As String, ByVal fromDate As Date, ByVal TodateDate As Date) As DataSet
        Try
            Dim dsDayClose As DataSet = Nothing
            Dim frmDate = fromDate.ToString("yyyy-MM-dd")
            Dim Todate = TodateDate.ToString("yyyy-MM-dd")
            Dim sqlComm As New SqlCommand("Udp_GetOrderTypeWiseSalesReport", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@SiteCode", sitecode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@FromDate", frmDate)
            sqlComm.Parameters.AddWithValue("@ToDate", Todate)
            sqlComm.CommandType = CommandType.StoredProcedure
            dsDayClose = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsDayClose)
            Return dsDayClose
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetCreditSaleReportData(ByVal sitecode As String) As DataSet 'vipin
        Try
            Dim dsDayClose As DataSet = Nothing
            Dim sqlComm As New SqlCommand("UDP_CreditSalesReportHeader", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@V_SiteCode", sitecode)
            sqlComm.CommandTimeout = 0
            sqlComm.CommandType = CommandType.StoredProcedure
            dsDayClose = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsDayClose)
            Return dsDayClose
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetAllValidReports(ByVal siteCode As String)
        Try
            Dim query As String = "select * from MstReportConfig where STATUS = 1 and Sitecode = '" & siteCode & "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetDayCloseReportPath(ByVal siteCode As String) As DataTable
        Try
            Dim query As String = "Select fldvalue From DefaultConfig Where FldLabel='DayCloseReportPath' and SiteCode='" & siteCode & "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Protected Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            'GC.Collect()
        End Try
    End Sub

    Protected Function GetCurrentVerticalPosition(ByRef writer As PdfWriter) As Single
        Try
            If writer IsNot Nothing Then
                Return writer.GetVerticalPosition(False)
            End If
            Return 0
        Catch ex As Exception
            LogException(ex)
            Return 0
        End Try
    End Function

    'Protected Sub DrawStrokedLine(ByRef cb As PdfContentByte, ByRef writer As PdfWriter, ByRef doc As Document)
    '    Try
    '        If cb IsNot Nothing Then
    '            cb.MoveTo(5, GetCurrentVerticalPosition(writer))
    '            cb.LineTo(doc.PageSize.Width - 5, GetCurrentVerticalPosition(writer))
    '        End If
    '    Catch ex As Exception
    '        LogException(ex)
    '    End Try
    'End Sub

    Protected Sub DrawStrokedLine(ByRef doc As Document)
        Try
            doc.Add(New Phrase("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------", GetContentFontNormal()))
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Protected Function GetContentFontBold() As iTextSharp.text.Font
        Try
            Return FontFactory.GetFont("dax-black", 10, iTextSharp.text.Font.BOLD)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Protected Function GetContentFontNormal() As iTextSharp.text.Font
        Try
            Return FontFactory.GetFont("dax-black", 10)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Protected Function GetHeaderFont() As iTextSharp.text.Font
        Try
            Return FontFactory.GetFont("dax-black", 12, iTextSharp.text.Font.BOLD)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Private _HideTableColumn As Boolean = False
    Public Property HideTableColumn() As Boolean
        Get
            Return _HideTableColumn
        End Get
        Set(ByVal value As Boolean)
            _HideTableColumn = value
        End Set
    End Property

    Private _tblSumFooter As String = "Total"
    Public Property tblSumFooter() As String
        Get
            Return _tblSumFooter
        End Get
        Set(ByVal value As String)
            _tblSumFooter = value
        End Set
    End Property

    Private _tblSumValue As Double
    Public Property tblSumValue() As Double
        Get
            Return _tblSumValue
        End Get
        Set(ByVal value As Double)
            _tblSumValue = value
        End Set
    End Property
    Private _tblRunningGrandTotal As Double = 0
    Public Property tblRunningGrandTotal() As Double
        Get
            Return _tblRunningGrandTotal
        End Get
        Set(ByVal value As Double)
            _tblRunningGrandTotal = value
        End Set
    End Property

    Private _tblRunningGrandTotalfooter As String = "Grand Total:"
    Public Property tblRunningGrandTotalfooter() As String
        Get
            Return _tblRunningGrandTotalfooter
        End Get
        Set(ByVal value As String)
            _tblRunningGrandTotalfooter = value
        End Set
    End Property

    Private _isDrawStroke As Boolean = True
    Public Property isDrawStroke() As Boolean
        Get
            Return _isDrawStroke
        End Get
        Set(ByVal value As Boolean)
            _isDrawStroke = value
        End Set
    End Property



    Protected Sub PrintDataTable(ByRef dt As DataTable, ByRef doc As Document)
        Try
            Dim totalSpace As Int16 = 0
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim pdfTable As PdfPTable = New PdfPTable(dt.Columns.Count)
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT
                pdfTable.WidthPercentage = GetTableWidthPercent(dt.Columns.Count)
                pdfTable.DefaultCell.BorderWidth = 0
                pdfTable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT
                If Not HideTableColumn Then
                    For Each column In dt.Columns
                        pdfTable.AddCell(New Phrase(column.ColumnName.ToString(), GetContentFontBold()))
                    Next
                End If

                For Each row As DataRow In dt.Rows
                    totalSpace = totalSpace + 1
                    For Each cell In row.ItemArray
                        pdfTable.AddCell(New Phrase(cell.ToString(), GetContentFontNormal()))
                    Next
                Next
                '------ Code Added By Mahesh for Showing Sum Value
                If Math.Abs(Val(tblSumValue)) > 0 Then
                    Dim addSpacecolCount = 0
                    If (dt.Columns.Count = 2) Then : addSpacecolCount = 0
                    ElseIf (tblSumFooter.ToString().Length + tblSumValue.ToString.Length) > (dt.Columns(dt.Columns.Count - 1).ColumnName.ToString.Length + dt.Columns(dt.Columns.Count - 2).ColumnName.ToString.Length) Then
                        addSpacecolCount = dt.Columns.Count - 3
                    Else
                        addSpacecolCount = dt.Columns.Count - 2
                    End If

                    For index = 0 To addSpacecolCount - 1
                        pdfTable.AddCell(New Phrase(Space(dt.Columns(index).ColumnName.ToString().Length), GetContentFontNormal()))
                    Next
                    pdfTable.AddCell(New Phrase(tblSumFooter.ToString(), GetContentFontBold()))
                    'pdfTable.AddCell(New Phrase(tblSumValue, GetContentFontBold()))
                    pdfTable.AddCell(New Phrase(FormatNumber(CDbl(clsCommon.CheckIfBlank(tblSumValue)), 2), GetContentFontBold()))
                End If
                If Val(tblRunningGrandTotal) > 0 Then
                    Dim addSpacecolCount = 0
                    If (dt.Columns.Count = 2) Then : addSpacecolCount = 0
                    ElseIf (tblSumFooter.ToString().Length + tblSumValue.ToString.Length) > (dt.Columns(dt.Columns.Count - 1).ColumnName.ToString.Length + dt.Columns(dt.Columns.Count - 2).ColumnName.ToString.Length) Then
                        addSpacecolCount = dt.Columns.Count - 3
                    Else
                        addSpacecolCount = dt.Columns.Count - 2
                    End If

                    For index = 0 To addSpacecolCount - 1
                        pdfTable.AddCell(New Phrase(Space(dt.Columns(index).ColumnName.ToString().Length), GetContentFontNormal()))
                    Next
                    pdfTable.AddCell(New Phrase(tblRunningGrandTotalfooter.ToString(), GetContentFontBold()))
                    pdfTable.AddCell(New Phrase(FormatNumber(CDbl(clsCommon.CheckIfBlank(tblRunningGrandTotal)), 2), GetContentFontBold()))
                End If

                pdfTable.HeaderRows = 1
                pdfTable.DefaultCell.BorderWidth = 0
                doc.Add(pdfTable)

            Else
                doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Phrase("NA", GetContentFontNormal()))
                doc.Add(New Phrase(Environment.NewLine))
            End If
            If isDrawStroke Then DrawStrokedLine(doc)
            doc.Add(New Phrase(Environment.NewLine))
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Function GetTableWidthPercent(ByVal columnCount As Integer) As Integer
        Try
            Dim percent As Integer = 0
            If columnCount = 1 Then
                percent = 25
            ElseIf columnCount = 2 Then
                percent = 50
            ElseIf columnCount = 3 Then
                percent = 75
            ElseIf columnCount >= 4 Then
                percent = 100
            End If
            Return percent
        Catch ex As Exception
            LogException(ex)
            Return 0
        End Try
    End Function

    Public Function GetAdsrProcedureName(ByVal siteCode As String, ByRef adsrReportFileName As String) As String
        Try
            Dim query As String = String.Format("SELECT * FROM FnGetADSRProcName('{0}')", siteCode)
            Dim dtADSR As DataTable = GetFilledTable(query)

            If (Not dtADSR Is Nothing AndAlso dtADSR.Rows.Count > 0) Then
                adsrReportFileName = dtADSR.Rows(0)(1).ToString()
                Return dtADSR.Rows(0)(0).ToString()
            End If

            Return String.Empty

        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function
    Public Function GetOpenDateAndStatus(ByVal sitecode As String, Optional ClientName As String = "") As Date
        Try
            Dim query As String = " select MAX(OpenDate) from DayOpenNClose where SiteCode = '" & sitecode & "' "
            ' change by khusrao adil
            ' For JK day close report
            If Not (ClientName = "JK") Then
                query = query + " and DayCloseStatus = '1'"
            End If
            Dim dtADSR As DataTable = GetFilledTable(query)
            If (Not dtADSR Is Nothing AndAlso dtADSR.Rows.Count > 0) Then
                Dim opendate As Date = dtADSR.Rows(0)(0)
                Return opendate
            Else
                Return Nothing
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSitePromotions(ByVal Sitecode As String, Optional PromotionNames As String = "") As DataTable
        Try
            Dim query As String = ""
            If PromotionNames = "" Then
                query = "select P.OfferNo as PromotionNo,P.OfferName As PromotionName from Promotions P inner join PromotionSiteMap PSM on p.OfferNo=PSM.OfferNo where P.STATUS=1 and  P.OfferActive=1 and PSM.STATUS=1 and SiteCode='" + Sitecode + "'"
            Else
                query = "select P.OfferNo as PromotionNo from Promotions P where P.OfferName in (" + PromotionNames + ")"
            End If
            Dim dt As New DataTable
            dt = GetFilledTable(query)
            Return dt
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function GetCustomerClassify(ByVal siteCode As String) As DataTable
        Try
            Dim query As String = ""
            query = "SELECT * From  ''TableName'' STATUS=1 and SiteCode='" + siteCode + "'"
            Dim dt As New DataTable
            dt = GetFilledTable(query)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetOpenDate(ByVal sitecode As String, ByVal opendate As Date, Optional ClientName As String = "") As Date
        Try

            Dim query As String = " select OpenDate  from DayOpenNClose where OpenDate = @DayOpenDate  and SiteCode = '" & sitecode & "' "
            ' change by khusrao adil
            ' For JK day close report
            If Not (ClientName = "JK") Then
                query = query + " and DayCloseStatus = '1'"
            End If
            Dim cmd As New SqlCommand(query, SpectrumCon())
            cmd.Parameters.Add("@DayOpenDate", SqlDbType.Date)
            cmd.Parameters("@DayOpenDate").Value = opendate

            Dim dtADSR As DataTable = GetFilledTableByCommand(cmd)

            If (Not dtADSR Is Nothing AndAlso dtADSR.Rows.Count > 0) Then
                Dim openeddate As Date = dtADSR.Rows(0)(0)
                Return openeddate
            Else
                Return Nothing
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function getpartner(ByVal Sitecode As String, Optional DelieveryPartnerName As String = "") As DataTable
        Try
            Dim dt As New DataTable
            Dim query As String
            If DelieveryPartnerName = "" Then
                query = "select DelieveryPartnerId ,DelieveryPartnerName from MstDelieveryPartner where Status=1"
            Else
                query = "select DelieveryPartnerId ,DelieveryPartnerName from MstDelieveryPartner where DelieveryPartnerName in (" + DelieveryPartnerName + ")"
            End If
            Dim cmd As New SqlCommand(query, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function getHierarchyWiseReportDate(ByVal sitecode As String, ByVal frmDate As Date, ByVal toDate As Date, ByVal ArticleCode As String, ByVal reportype As String, ByVal boolReport As Boolean) As DataSet
        Try
            Dim dsArticle As DataSet = Nothing
            Dim fromDate = frmDate.ToString("yyyy-MM-dd")
            Dim TDate = toDate.ToString("yyyy-MM-dd")
            Dim sqlcomm As New SqlCommand("UDP_GetHierarchyWiseNetSalesReportData", SpectrumCon)
            sqlcomm.Parameters.AddWithValue("@V_SiteCode", sitecode)
            sqlcomm.Parameters.AddWithValue("@V_FromDate", fromDate)
            sqlcomm.Parameters.AddWithValue("@V_Todate", TDate)
            sqlcomm.Parameters.AddWithValue("@V_ArticleCode", ArticleCode)
            sqlcomm.Parameters.AddWithValue("@V_ReportType", reportype)
            sqlcomm.Parameters.AddWithValue("@V_ReportID", boolReport)
            sqlcomm.CommandType = CommandType.StoredProcedure
            dsArticle = New DataSet
            Dim da As New SqlDataAdapter(sqlcomm)
            da.Fill(dsArticle)
            Return dsArticle

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function getHierarchyWiseDetailsReportData(ByVal sitecode As String, ByVal frmDate As Date, ByVal toDate As Date, ByVal ArticleCode As String, ByVal reportype As String, ByVal boolReport As Boolean) As DataSet
        Try
            Dim dsArticle As DataSet = Nothing
            Dim fromDate = frmDate.ToString("yyyy-MM-dd")
            Dim TDate = toDate.ToString("yyyy-MM-dd")
            Dim sqlcomm As New SqlCommand("UDP_GetHierarchyWiseNetSalesDetailsReportData", SpectrumCon)
            sqlcomm.Parameters.AddWithValue("@V_SiteCode", sitecode)
            sqlcomm.Parameters.AddWithValue("@V_FromDate", fromDate)
            sqlcomm.Parameters.AddWithValue("@V_Todate", TDate)
            sqlcomm.Parameters.AddWithValue("@V_ArticleCode", ArticleCode)
            sqlcomm.Parameters.AddWithValue("@V_ReportType", reportype)
            sqlcomm.Parameters.AddWithValue("@V_ReportID", boolReport)
            sqlcomm.CommandType = CommandType.StoredProcedure
            dsArticle = New DataSet
            Dim da As New SqlDataAdapter(sqlcomm)
            da.Fill(dsArticle)
            Return dsArticle

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'vipul 06-09-2018 tenderwisecommisionreport
    Public Function GetTender(ByVal Sitecode As String, Optional TenderName As String = "") As DataTable
        Try
            Dim dt As New DataTable
            Dim query As String
            If TenderName = "" Then
                query = "select TENDERTYPE,TENDERTYPE  from msttendertype " & _
                         " WHERE TenderType IN ( SELECT TENDERTYPE FROM MSTTENDER WHERE SITECODE='" & Sitecode & "' AND STATUS=1 )" & _
                         " AND STATUS=1"
            Else
                query = "select TENDERTYPE,DESCRIPTION  from msttendertype " & _
                     "WHERE TenderType IN (" + TenderName + " ) And STATUS = 1"
                ' ' "WHERE TenderType IN ( SELECT TENDERTYPE FROM MSTTENDER WHERE SITECODE='" & Sitecode & "' AND STATUS=1 )" & _
                '" AND STATUS=1 where TENDERTYPE in (" + TenderName + ")"
            End If
            Dim cmd As New SqlCommand(query, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

End Class
