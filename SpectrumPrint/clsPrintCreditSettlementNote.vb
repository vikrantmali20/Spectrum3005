Imports SpectrumBL
Imports SpectrumCommon
Imports System.Text

Public Class clsPrintCreditSettlementNote
    Dim Pettycashvouch As IPettyCashVoucher
    Private LineLength As Integer = 98
    Public Sub New()
        Pettycashvouch = New PettyCashVoucher()
    End Sub

    ''' <summary>
    ''' For Printing Credit Settlement Notes
    ''' </summary>
    ''' <param name="TransactionList"></param>
    ''' <param name="dtprint"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PrintNote(ByVal TransactionList As List(Of SalesInvoice), ByVal dtprint As DataTable, Optional ByVal currencyCode As String = Nothing, Optional ByVal currencyDesc As String = Nothing) As Boolean
        Try
            Dim strPrint As New StringBuilder
            Dim strTitle As New StringBuilder
            Dim strGridHeader As New StringBuilder
            Dim strGridItem As New StringBuilder
            Dim strFooter As New StringBuilder
            Dim dtSiteInfo As DataTable = Pettycashvouch.GetSiteInfo(TransactionList.FirstOrDefault().SiteCode)
            If dtSiteInfo IsNot Nothing Then
                Dim siteName As String = dtSiteInfo.Rows(0)(0)
                Dim title As String = String.Empty
                'title = "Credit Settlement Note"
                title = getValueByKey("CrS001")

                strTitle.Append(title.PadLeft(LineLength / 2 + (title.Length / 2)))
                strTitle.Append(vbCrLf)
                strTitle.Append(siteName.PadLeft(LineLength / 2 + (siteName.Length / 2)))
                strTitle.Append(vbCrLf)
                strTitle.Append(SplitString(dtSiteInfo.Rows(0)(2), LineLength))
                strTitle.Append(vbCrLf)
                Dim voucherDate As String = getValueByKey("Crs002") & TransactionList.FirstOrDefault().RecTime.ToShortDateString()
                Dim vchrNumber As String = getValueByKey("Crs003") & TransactionList.FirstOrDefault().BillNO
                strTitle.Append(voucherDate & vchrNumber.PadLeft(LineLength - voucherDate.Length))
                strTitle.Append(vbCrLf & vbCrLf)
            End If
            strGridHeader.Append(strLineA4)
            strGridHeader.Append(vbCrLf)
            Dim gridHeader As String = getValueByKey("Crs007") & "Amount".PadLeft(LineLength - getValueByKey("Crs007").Length)
            strGridHeader.Append(gridHeader)
            strGridHeader.Append(vbCrLf)
            strGridHeader.Append(strLineA4)
            Dim vchrAccountType As String =
               getValueByKey("Crs004") & TransactionList.FirstOrDefault().InvoiceNumber
            strGridItem.Append(vchrAccountType & vbCrLf)
            strGridItem.Append(getValueByKey("Crs005") & vbCrLf)
            Dim sum As Integer = 0
            For Each inv As SalesInvoice In TransactionList
                strGridItem.Append(inv.TenderHeadCode & MyRound(inv.AmountTendered, 10).ToString().PadLeft(LineLength - inv.TenderHeadCode.Length) & vbCrLf)
                sum = sum + inv.AmountTendered

            Next
            ' strGridItem.Append(vbCrLf)
            strGridItem.Append(strLineA4 & vbCrLf)
            strGridItem.Append(getValueByKey("Crs006") & vbCrLf)
            Dim amountInWord As String = AmtInWord(sum, currencyCode, currencyDesc)
            strGridItem.Append(amountInWord & FormatNumber(CDbl(sum), 2).PadLeft(LineLength - amountInWord.Length) & vbCrLf & vbCrLf & vbCrLf & vbCrLf)
            Dim objclsA4Print As New clsA4Print
            objclsA4Print.DocumentType = clsA4Print.DocumentTypeList.PettyCashVoucher
            objclsA4Print.A4Title = strTitle.ToString()
            objclsA4Print.A4CashierDetails = String.Empty
            objclsA4Print.A4CustomerDetails = String.Empty
            objclsA4Print.A4Remark = String.Empty
            objclsA4Print.A4ItemHeader = strGridHeader.ToString()
            objclsA4Print.A4ItemDetails = strGridItem.ToString()
            objclsA4Print.A4Footer = strFooter.ToString()
            PrinterName = SetPrinterName(dtprint, "CreditSettleMentNote", "CreditSettlement")
            objclsA4Print.fnPrint("PRV")
            Return True
        Catch ex As Exception
            MsgBox(getValueByKey("CLPBL01"), , "CLPBL01 - " & getValueByKey("CLAE05"))
            Return False
        End Try
        Return True
    End Function
End Class