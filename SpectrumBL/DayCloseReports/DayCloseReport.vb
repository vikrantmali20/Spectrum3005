
Imports System.Reflection ' For Missing.Value and BindingFlags
Imports System.Runtime.InteropServices ' For COMException
Imports SpectrumCommon
Public Class DayCloseReport
    Inherits ReportBase
    Public Sub New()

    End Sub

    'Public Sub GenerateDayCloseReport(ByRef request As DayCloseReportModel)
    '    Dim dt As DataTable = GetDayCloseDataSet(request.SiteCode)
    '    If dt Is Nothing OrElse dt.Rows.Count = 0 Then
    '        Exit Sub
    '    End If
    '    Dim xlApp As Excel.Application
    '    Try
    '        Dim xlWorkBook As Excel.Workbook
    '        Dim xlWorkSheet As Excel.Worksheet
    '        Dim misValue As Object = System.Reflection.Missing.Value

    '        xlApp = New Excel.ApplicationClass
    '        xlWorkBook = xlApp.Workbooks.Add(misValue)
    '        xlWorkSheet = xlWorkBook.Sheets("sheet1")
    '        xlWorkSheet.Name = "Day Close Report"
    '        xlWorkSheet.Range("A1", "C1").Merge()
    '        Dim siteName As String = GetSiteName(request.SiteCode)
    '        xlWorkSheet.Cells(1, 1) = siteName & ", " & request.ToDate.ToShortDateString()
    '        xlWorkSheet.Cells(2, 1) = "Tender Type"
    '        xlWorkSheet.Cells(2, 2) = "Amount"
    '        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
    '            xlWorkSheet.Cells(i + 3, 1) = dt.Rows(i)("DESCRIPTION")
    '            xlWorkSheet.Cells(i + 3, 2) = dt.Rows(i)("AMTTEN")
    '        Next
    '        xlWorkSheet.Columns(1).EntireColumn.AutoFit()
    '        'Dim path As String = System.IO.Path.GetDirectoryName((New System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath)       
    '        Dim path As String = "D:\"
    '        path = System.IO.Path.Combine(path, "DayCloseReports")
    '        If Not System.IO.Directory.Exists(path) Then
    '            System.IO.Directory.CreateDirectory(path)
    '        End If

    '        path = path & "\DayCloseReport_" & siteName & "_" & request.ToDate.ToString("dd-MM-yyyy") & ".xlsx"
    '        xlWorkSheet.SaveAs(path)
    '        xlWorkBook.Close()
    '        xlApp.Quit()
    '        releaseObject(xlApp)
    '        releaseObject(xlWorkBook)
    '        releaseObject(xlWorkSheet)
    '        System.Diagnostics.Process.Start(path)
    '    Catch ex As Exception
    '        LogException(ex)
    '    Finally
    '        'xlApp.Quit()
    '        'CloseConnection()
    '    End Try
    'End Sub

    Private Function GetDayCloseDataSet(ByVal siteCode As String) As DataTable
        Try
            Dim reportQuery As String = "select TENDERTYPE,DESCRIPTION,sum(AMOUNTTENDERED) as AMTTEN from  " & _
      "(SELECT B.TENDERTYPE,C.DESCRIPTION, A.AMOUNTTENDERED,  " & _
      "CONVERT(BIT,CASE WHEN A.TENDERTYPECODE='GiftVoucher(I)' THEN  " & _
     "1 ELSE 0 END) AS ISSUED FROM cashmemohdr D inner join CashMemoReceipt A  " & _
      "ON d.SiteCode=A.Sitecode and d.FinYear=A.finyear and d.BillNo= A.Billno and  " & _
      "d.BillIntermediateStatus <> 'Deleted' INNER JOIN MSTTENDER B  " & _
      "ON A.TENDERHEADCODE= B.TENDERHEADCODE AND A.TENDERTYPECODE=B.TENDERTYPE  " & _
      "AND B.SITECODE='" & siteCode & "' INNER JOIN MSTTENDERTYPE C ON  " & _
      "B.TENDERTYPE=C.TENDERTYPE WHERE convert(datetime,CONVERT(VARCHAR(10), A.cmRcptDate ,101)) =(Select opendate from  " & _
      "DayOpenNClose where daycloseStatus=0 and sitecode='" & siteCode & "') AND  " & _
      "A.SITECODE='" & siteCode & "' " & _
      "and A.TENDERTYPECODE <> 'GiftVoucher(I)' " & _
      "and a.TENDERHEADCODE <> 'CreditCheque' UNION  " & _
      "ALL SELECT A.Tendertypecode,C.DESCRIPTION, A.AMOUNTTENDERED,CONVERT(BIT,CASE  " & _
      "WHEN A.TENDERTYPECODE='CreditVouc(Iss)' THEN 1 ELSE 0 END)  " & _
      "AS ISSUED FROM SALESINVOICE A inner join salesorderhdr b  " & _
      "on a.sitecode=b.sitecode and a.documentnumber=b.saleordernumber and b.sostatus<>'Cancel'  " & _
      "INNER join MSTTENDERTYPE C  " & _
      "on A.Tendertypecode=C.TENDERTYPE " & _
      "WHERE A.status=1 and convert(datetime,CONVERT(VARCHAR(10), A.soinvdate ,101)) = (Select opendate from DayOpenNClose where daycloseStatus=0 " & _
      "and sitecode='" & siteCode & "') AND A.SITECODE='" & siteCode & "'  AND  " & _
      "A.TENDERTYPECODE <>  " & _
     "'GiftVoucher(I)' and a.TENDERHEADCODE <> 'CreditCheque'  " & _
      "Union ALL SELECT A.Tendertypecode,C.DESCRIPTION, A.AMOUNTTENDERED,CONVERT(BIT,CASE  " & _
      "WHEN A.TENDERTYPECODE='CreditVouc(Iss)' THEN 1 ELSE 0 END)  " & _
      "AS ISSUED FROM SALESINVOICE A inner join Birthlist b  " & _
      "on a.sitecode=b.sitecode and a.documentnumber=b.birthlistid and b.BirthListStatus<>'Cancel'  " & _
      "INNER join MSTTENDERTYPE C  " & _
      "on A.Tendertypecode=C.TENDERTYPE  " & _
      "WHERE A.status=1 and convert(datetime,CONVERT(VARCHAR(10), A.soinvdate ,101)) = (Select opendate from DayOpenNClose where daycloseStatus=0  " & _
      "and sitecode='" & siteCode & "') AND A.SITECODE='" & siteCode & "' " & _
      "and A.TENDERTYPECODE <> 'GiftVoucher(I)' and a.TENDERHEADCODE <> 'CreditCheque' ) temp group by TENDERTYPE,DESCRIPTION "
            Return GetFilledTable(reportQuery)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
End Class
