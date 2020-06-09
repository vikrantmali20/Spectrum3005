Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports C1.C1Pdf
Imports System.Drawing.Printing
Imports C1.Win.C1BarCode
Imports SpectrumBL
Imports SpectrumCommon
Imports SpectrumCommon.ExtensionModule
Public Class kotprint
    Public Sub GenerateKOT(ByVal request As SpectrumCommon.DayCloseReportModel, reportPath As String, ByVal dtPrinterInfo As DataTable, Optional ReportName As String = "")

        Try

            Dim ds As DataSet = GetKOTReporDataSet(request)

            '  Dim pathtxt As String = GetDirectoryTxtPath(request)
            Dim dsBun As DataSet = GetKOTReportBunCountDataSet(request)
            GenerateKOTReportL40(request, ds, dsBun, dtPrinterInfo, ReportName)
            Exit Sub



            'If PrintFormatNo = 1 Then
            '    CashMemoPrintFormat_Naturals(dayopenDate, LangCode, Currency, Sitecode, Type, DuplicatePrinting, DeletedUserid, AuthorisedUser, errorMsg, GiftMsg, BillAmt, PaidAmt)
            'Else
            '    CashMemoPrintFormat(dayopenDate, LangCode, Currency, Sitecode, Type, DuplicatePrinting, DeletedUserid, AuthorisedUser, errorMsg, GiftMsg)
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    Public Function GenerateKOTReportL40(ByVal request As SpectrumCommon.DayCloseReportModel, ByVal ds As DataSet, ByVal dsBun As DataSet, ByVal dsPrint As DataTable, Optional Report_Name As String = "") As Boolean
        Try
            Dim objnew As New SpectrumBL.clsCommon
            Dim ReportName, siteName, GeneratedBy, Period, Header As String
            Dim lineLength As Integer = 40
            Dim lineL40 As String = "---------------------------------------"
            Header = "ITEMS          |   QTY|      AMT|  DISC"
            Dim MainContent As New StringBuilder
            Dim HeaderContent As New StringBuilder

            If ds.Tables IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                'ReportName = "Name : KOT Report"
                If Report_Name = "ProductMixReport" Then
                    ReportName = "Name : Product Mix Report"
                Else
                    ReportName = "Name : KOT Report"
                End If

                siteName = "Store Name : " & objnew.GetSiteName(request.SiteCode)


                GeneratedBy = "Genrated by  + Date + time : " & vbCrLf & request.CreatedBy & ", " & request.CreatedOn.ToString()

                'Period = "KOT Report for Period : " & vbCrLf & request.FromDate.ToShortDateString() & " to " & request.ToDate.ToShortDateString()
                If Report_Name = "ProductMixReport" Then
                    Period = "Product Mix Report for Period : " & vbCrLf & request.FromDate.ToShortDateString() & " to " & request.ToDate.ToShortDateString()
                Else
                    Period = "KOT Report for Period : " & vbCrLf & request.FromDate.ToShortDateString() & " to " & request.ToDate.ToShortDateString()
                End If
                HeaderContent.Append(ReportName & vbCrLf)
                HeaderContent.Append(siteName & vbCrLf)
                HeaderContent.Append(GeneratedBy & vbCrLf)
                HeaderContent.Append(Period & vbCrLf & vbCrLf & vbCrLf)
                HeaderContent.Append(Header & vbCrLf)

                For Each dr As DataRow In ds.Tables(2).Rows
                    Dim nodeName As String = dr("NodeName")
                    MainContent.Append(lineL40 & vbCrLf)
                    MainContent.Append(nodeName & vbCrLf)
                    MainContent.Append(lineL40 & vbCrLf)
                    For Each item As DataRow In ds.Tables(1).Select("NodeName='" & nodeName & "'")
                        MainContent.Append(item(2).ToString() & vbCrLf)
                        Dim Qty = "|" & item(3).ToString().PadLeft(6)
                        Dim Amt = "|" & item(4).ToString().PadLeft(9)
                        Dim Disc = "|" & item(5).ToString().PadLeft(6)

                        Dim strDtl As String = (Qty & Amt & Disc).PadLeft(39)
                        MainContent.Append(strDtl & vbCrLf)
                    Next
                    MainContent.Append(lineL40 & vbCrLf)

                    Dim SubQty = "|" & dr("SALESQUANTITY").ToString().PadLeft(6)
                    Dim SubAmt = "|" & dr("Total_Amount").ToString().PadLeft(9)
                    Dim SubDisc = "|" & dr("Total_Discount").ToString().PadLeft(6)
                    Dim SubFinal = ("Sub Total :").PadRight(15) & SubQty & SubAmt & SubDisc
                    MainContent.Append(SubFinal & vbCrLf)
                Next

                For Each dr As DataRow In ds.Tables(3).Rows
                    MainContent.Append(lineL40 & vbCrLf)
                    Dim GrandQty = "|" & dr("Total_Quantity").ToString().PadLeft(6)
                    Dim GrandAmt = "|" & dr("Total_Amount").ToString().PadLeft(9)
                    Dim GrandDisc = "|" & dr("Total_Discount").ToString().PadLeft(6)
                    Dim GrandFinal = ("Grand Total :").PadRight(15) & GrandQty & GrandAmt & GrandDisc
                    MainContent.Append(GrandFinal & vbCrLf)
                Next

                HeaderContent.Append(MainContent)

                HeaderContent.Append(vbCrLf & "Bun Count   :     " & dsBun.Tables(0).Rows(0)(0).ToString())
                PrinterName = SetPrinterName(dsPrint, "CashMemo", "Billing")
                fnPrint(HeaderContent.ToString(), "PRV")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Function GetKOTReportBunCountDataSet(ByVal request As SpectrumCommon.DayCloseReportModel) As DataSet
        Try
            Dim query As New StringBuilder
            query.Append("select  isnull(sum(NETSALESQTY),0) from  (SELECT CAST(SUM(CONVERT(NUMERIC(18),ISNULL((A.QUANTITY*MAK.QUANTITY), 0) - ")
            query.Append("ISNULL((A.RETURNQTY*MAK.QUANTITY), 0))) AS DECIMAL(10,2)) AS NETSALESQTY,ma.articlecode,ma.lastnodecode ")
            query.Append("FROM VIEW_SALESREPORT AS A ")
            query.Append("INNER JOIN MSTARTICLEKIT  MAK ON MAK.KITARTICLECODE=A.ARTICLECODE ")
            query.Append("INNER JOIN VW_HIREARCYLEVEL  I ON A.LASTNODECODE=I.LEVEL1 ")
            query.Append("INNER JOIN MSTARTICLE MA ON MA.ARTICLECODE=MAK.ARTICLECODE ")
            query.Append("WHERE A.billdate between CONVERT(CHAR(10),'" & request.FromDate.ToString("yyyy-MM-dd") & "',126)   and   CONVERT(CHAR(10),'" & request.ToDate.ToString("yyyy-MM-dd") & "',126) ")
            query.Append("AND A.SITECODE= '" & request.SiteCode & "'  and  MAK.ARTICLECODE IN (SELECT ARTICLECODE FROM MSTARTICLE  WHERE ARTICALTYPECODE='SINGLE' ) ")
            query.Append("group by ma.articlecode ,ma.lastnodecode ) ")
            query.Append("a where lastnodecode='ANCCCE000000005' OR  a.articlecode='310202022'  ")
            ' or a.articlecode='310202022'
            'Dim query As String = "SELECT ISNULL( CAST(SUM(CONVERT(NUMERIC(18),ISNULL((A.QUANTITY*MAK.QUANTITY), 0) - ISNULL((A.RETURNQTY*MAK.QUANTITY), 0))) AS DECIMAL(10,2)),0) AS NETSALESQTY  FROM VIEW_SALESREPORT AS A  INNER JOIN MSTARTICLEKIT  MAK ON MAK.KITARTICLECODE=A.ARTICLECODE  INNER JOIN VW_HIREARCYLEVEL  I ON A.LASTNODECODE=I.LEVEL1  INNER JOIN MSTARTICLE MA ON MA.ARTICLECODE=MAK.ARTICLECODE  WHERE CONVERT(CHAR(10),A.BILLDATE,126) between CONVERT(CHAR(10),'" & request.FromDate.ToString("yyyy-MM-dd") & "',126)   and   CONVERT(CHAR(10),'" & request.ToDate.ToString("yyyy-MM-dd") & "',126)  AND A.SITECODE= '" & request.SiteCode & "'  AND MAK.ARTICLECODE IN (SELECT ARTICLECODE FROM MSTARTICLE  WHERE ARTICALTYPECODE='SINGLE') and   MA.LastNodeCode='ANCCCE000000005'  "
            Dim dataAdapter As SqlDataAdapter
            Dim reportSet As New DataSet
            'dataAdapter = New SqlDataAdapter(query, SpectrumCon)
            Dim cmd As New SqlCommand(query.ToString(), SpectrumCon)
            cmd.CommandTimeout = 0
            dataAdapter = New SqlDataAdapter(cmd)
            dataAdapter.Fill(reportSet)
            Return reportSet
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Private Function GetKOTReporDataSet(ByVal request As SpectrumCommon.DayCloseReportModel) As DataSet
        Try
            Dim query As String = "exec kotreport '" & request.FromDate.ToString("yyyy-MM-dd") & "' ,'" & request.ToDate.ToString("yyyy-MM-dd") & "' ,'" & request.SiteCode & "'"
            Dim dataAdapter As SqlDataAdapter
            Dim reportSet As New DataSet
            'dataAdapter = New SqlDataAdapter(query, SpectrumCon)
            Dim cmd As New SqlCommand(query, SpectrumCon)
            cmd.CommandTimeout = 0
            dataAdapter = New SqlDataAdapter(cmd)
            dataAdapter.Fill(reportSet)
            Return reportSet
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    'Private Function GetDirectoryPath(ByRef request As DayCloseReportModel) As String
    '    Try
    '        Dim path As String = String.Empty
    '        Dim siteName As String = GetSiteName(request.SiteCode)
    '        If Not System.IO.Directory.Exists(DayCloseReportPath) Then
    '            System.IO.Directory.CreateDirectory(DayCloseReportPath)
    '        End If
    '        path = DayCloseReportPath & "\KOTReport_" & siteName & "_" & request.FromDate.ToString("dd-MM-yyyy") & "_To_" & request.ToDate.ToString("dd-MM-yyyy") & ".xls"
    '        Return path
    '    Catch ex As Exception
    '        LogException(ex)
    '        Return Nothing
    '    End Try
    'End Function
    'Private Function GetDirectoryTxtPath(ByRef request As DayCloseReportModel) As String
    '    Try
    '        Dim path As String = String.Empty
    '        Dim siteName As String = GetSiteName(request.SiteCode)
    '        If Not System.IO.Directory.Exists(DayCloseReportPath) Then
    '            System.IO.Directory.CreateDirectory(DayCloseReportPath)
    '        End If
    '        path = DayCloseReportPath & "\KOTReport_" & siteName & "_" & request.FromDate.ToString("dd-MM-yyyy") & "_To_" & request.ToDate.ToString("dd-MM-yyyy") & ".txt"
    '        Return path
    '    Catch ex As Exception
    '        LogException(ex)
    '        Return Nothing
    '    End Try
    'End Function
End Class
