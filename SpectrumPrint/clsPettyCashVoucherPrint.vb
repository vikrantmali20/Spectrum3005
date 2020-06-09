Imports System.Text
Imports SpectrumBL
Imports SpectrumCommon

Public Class clsPettyCashVoucherPrint

    Public Sub New()
        BLInstance = New PettyCashVoucher()
    End Sub
    Private _BLInstance As IPettyCashVoucher
    Private Property BLInstance As IPettyCashVoucher
        Get
            Return _BLInstance
        End Get
        Set(ByVal value As IPettyCashVoucher)
            _BLInstance = value
        End Set
    End Property

    Private Shared _strPrintPettyCashPageSetup As String
    Public Shared Property PrintPettyCashPageSetup() As String
        Get
            Return _strPrintPettyCashPageSetup
        End Get
        Set(ByVal value As String)
            _strPrintPettyCashPageSetup = value
        End Set
    End Property
    Private LineLength As Integer = 98
    Public Function PrintVoucher(ByVal request As PrintVoucherRequest, Optional ByVal PettyCashPrinterInfo As DataTable = Nothing, Optional ByVal currencyCode As String = Nothing, Optional ByVal currencyDesc As String = Nothing, Optional ByVal PrintFormatNo As Integer = Nothing) As Boolean
        Try
            Dim status As Boolean = False
            Dim str As String = "PettyCash"
            PrintPettyCashPageSetup = (GetPrintFormat(PettyCashPrinterInfo.Rows(0)(0), str)).Rows(0)(0)
            If Not PrintPettyCashPageSetup = "L40" Then
                LineLength = 98
                'Call PrintVoucherA4(request)
                status = PrintVoucherA4(request, currencyCode, currencyDesc)
            Else
                LineLength = 40
                'Call PrintVoucherL40(request)
                status = PrintVoucherL40(request, currencyCode, currencyDesc, PrintFormatNo)
            End If
            Return status
        Catch ex As Exception
            MsgBox(getValueByKey("CLPBL01"), , "CLPBL01 - " & getValueByKey("CLAE05"))
            Return False
        End Try

    End Function

    Public Function PrintVoucherA4(ByVal request As PrintVoucherRequest, Optional ByVal currencyCode As String = Nothing, Optional ByVal currencyDesc As String = Nothing) As Boolean
        Try
            Dim strPrint As New StringBuilder
            Dim strTitle As New StringBuilder
            Dim strGridHeader As New StringBuilder
            Dim strGridItem As New StringBuilder
            Dim strFooter As New StringBuilder
            Dim dtSiteInfo As DataTable = BLInstance.GetSiteInfo(request.VoucherHeader.SiteCode)
            If dtSiteInfo IsNot Nothing Then
                Dim siteName As String = dtSiteInfo.Rows(0)(0)
                Dim title As String = String.Empty
                If request.VoucherHeader.VoucherTypeCode = PettyCashVoucherType.Expense.ToString() Then
                    title = "Payment (Petty Cash) Voucher"
                ElseIf request.VoucherHeader.VoucherTypeCode = PettyCashVoucherType.Receipt.ToString() Then
                    title = "Reciept (Petty Cash) Voucher"
                End If

                strTitle.Append(title.PadLeft(LineLength / 2 + (title.Length / 2)))
                strTitle.Append(vbCrLf)
                strTitle.Append(siteName.PadLeft(LineLength / 2 + (siteName.Length / 2)))
                strTitle.Append(vbCrLf)
                strTitle.Append(SplitString(dtSiteInfo.Rows(0)(2), LineLength))
                strTitle.Append(vbCrLf)
                Dim voucherDate As String = "Dated : " & request.VoucherHeader.ExpenseDate.ToShortDateString()
                Dim vchrNumber As String = "Voucher No. : " & request.VoucherHeader.VoucherID
                strTitle.Append(voucherDate & vchrNumber.PadLeft(LineLength - voucherDate.Length))
                strTitle.Append(vbCrLf & vbCrLf)
            End If
            strGridHeader.Append(strLineA4)
            strGridHeader.Append(vbCrLf)
            Dim gridHeader As String = "Particulars" & "Amount".PadLeft(LineLength - "Particulars".Length)
            strGridHeader.Append(gridHeader)
            strGridHeader.Append(vbCrLf)
            strGridHeader.Append(strLineA4)
            Dim vchrAccountType As String =
                "Account : " & request.VoucherAccountTypes.Where(Function(x) x.VoucherAccountID = request.VoucherHeader.VoucherAccountID).Select(Function(y) y.AccountType).FirstOrDefault()
            strGridItem.Append(vchrAccountType & FormatNumber(CDbl(request.VoucherHeader.TotalAmt), 2).PadLeft(LineLength - vchrAccountType.Length))
            strGridItem.Append(vbCrLf & vbCrLf & vbCrLf)
            strGridItem.Append("Through : Cash")
            strGridItem.Append(vbCrLf & vbCrLf)
            strGridItem.Append("On Acoount Of : ")
            strGridItem.Append(vbCrLf)
            Dim narration As New StringBuilder
            For Each item In request.VoucherHeader.VoucherDetails
                narration.Append(SplitString(item.Narration, LineLength).Append(vbCrLf))
            Next
            strGridItem.Append(narration)
            'strGridItem.Append(vbCrLf)
            strGridItem.Append(strLineA4 & vbCrLf)
            strGridItem.Append("Amount in words : " & vbCrLf)
            Dim amountInWord As String = AmtInWord(request.VoucherHeader.TotalAmt, currencyCode, currencyDesc)
            strGridItem.Append(amountInWord & FormatNumber(CDbl(request.VoucherHeader.TotalAmt), 2).PadLeft(LineLength - amountInWord.Length) & vbCrLf & vbCrLf & vbCrLf & vbCrLf)
            Dim signature As String = "Receiver's Signature : "
            strFooter.Append(signature & "Authorised Signatory".PadLeft(LineLength - signature.Length))
            Dim objclsA4Print As New clsA4Print
            objclsA4Print.DocumentType = clsA4Print.DocumentTypeList.PettyCashVoucher
            objclsA4Print.A4Title = strTitle.ToString()
            objclsA4Print.A4CashierDetails = String.Empty
            objclsA4Print.A4CustomerDetails = String.Empty
            objclsA4Print.A4Remark = String.Empty
            objclsA4Print.A4ItemHeader = strGridHeader.ToString()
            objclsA4Print.A4ItemDetails = strGridItem.ToString()
            objclsA4Print.A4Footer = strFooter.ToString()
            PrinterName = SetPrinterName(request.dtPrinterInfo, "PettyCash", "PCV")
            If request.IsPrviewRequiredForVchr Then
                objclsA4Print.fnPrint("PRV")
            Else
                objclsA4Print.fnPrint("PRN")
            End If
            Return True
        Catch ex As Exception
            MsgBox(getValueByKey("CLPBL01"), , "CLPBL01 - " & getValueByKey("CLAE05"))
            Return False
        End Try
    End Function

    Public Function PrintVoucherL40(ByVal request As PrintVoucherRequest, Optional ByVal currencyCode As String = Nothing, Optional ByVal currencyDesc As String = Nothing, Optional ByVal PrintFormatNo As Integer = Nothing) As Boolean
        Try
            Dim strPrint As New StringBuilder
            Dim strTitle As New StringBuilder
            Dim strGridHeader As New StringBuilder
            Dim strGridItem As New StringBuilder
            Dim strFooter As New StringBuilder
            Dim dtSiteInfo As DataTable = BLInstance.GetSiteInfo(request.VoucherHeader.SiteCode)
            If dtSiteInfo IsNot Nothing Then
                Dim siteName As String = dtSiteInfo.Rows(0)(0)
                Dim title As String = String.Empty
                If request.VoucherHeader.VoucherTypeCode = PettyCashVoucherType.Expense.ToString() Then
                    title = "Payment (Petty Cash) Voucher"
                ElseIf request.VoucherHeader.VoucherTypeCode = PettyCashVoucherType.Receipt.ToString() Then
                    title = "Reciept (Petty Cash) Voucher"
                End If

                strTitle.Append(title.PadLeft(LineLength / 2 + (title.Length / 2)))
                strTitle.Append(vbCrLf)
                strTitle.Append(siteName.PadLeft(LineLength / 2 + (siteName.Length / 2)))
                strTitle.Append(vbCrLf)
                strTitle.Append(SplitString(dtSiteInfo.Rows(0)(2), LineLength))
                strTitle.Append(vbCrLf)
                Dim voucherDate As String = "Dated : " & request.VoucherHeader.ExpenseDate.ToShortDateString()
                Dim vchrNumber As String = "Voucher No. : " & request.VoucherHeader.VoucherID
                If voucherDate.Length + vchrNumber.Length >= LineLength Then
                    strTitle.Append(voucherDate & vbCrLf)
                    strTitle.Append(vchrNumber)
                Else
                    strTitle.Append(voucherDate & vchrNumber.PadLeft(LineLength - voucherDate.Length))
                End If


                strTitle.Append(vbCrLf & vbCrLf)
            End If
            strGridHeader.Append(strLineL40)
            strGridHeader.Append(vbCrLf)
            Dim gridHeader As String = "Particulars" & "Amount".PadLeft(LineLength - "Particulars".Length)
            strGridHeader.Append(gridHeader)
            strGridHeader.Append(vbCrLf)
            strGridHeader.Append(strLineL40)
            Dim vchrAccountType As String =
                "Account : " & request.VoucherAccountTypes.Where(Function(x) x.VoucherAccountID = request.VoucherHeader.VoucherAccountID).Select(Function(y) y.AccountType).FirstOrDefault()
            strGridItem.Append(vchrAccountType & FormatNumber(CDbl(request.VoucherHeader.TotalAmt), 2).PadLeft(LineLength - vchrAccountType.Length))

            strGridItem.Append(vbCrLf & vbCrLf & vbCrLf)
            strGridItem.Append("Through : Cash")
            strGridItem.Append(vbCrLf & vbCrLf)
            strGridItem.Append("On Acoount Of : ")
            strGridItem.Append(vbCrLf)
            Dim narration As New StringBuilder
            For Each item In request.VoucherHeader.VoucherDetails
                narration.Append(SplitString(item.Narration, LineLength).Append(vbCrLf))
            Next
            strGridItem.Append(narration)
            'strGridItem.Append(vbCrLf)
            strGridItem.Append(strLineL40 & vbCrLf)
            strGridItem.Append("Amount in words : " & vbCrLf)
            Dim amountInWord As String = AmtInWord(request.VoucherHeader.TotalAmt, currencyCode, currencyDesc)
            If amountInWord.Length > LineLength Then
                Dim amtSplit As String = SplitString(amountInWord, LineLength).ToString
                strGridItem.Append(amtSplit & FormatNumber(CDbl(request.VoucherHeader.TotalAmt), 2).PadLeft(LineLength) & vbCrLf & vbCrLf & vbCrLf & vbCrLf)
            Else
                strGridItem.Append(amountInWord & FormatNumber(CDbl(request.VoucherHeader.TotalAmt), 2).PadLeft(LineLength - amountInWord.Length) & vbCrLf & vbCrLf & vbCrLf & vbCrLf)
            End If
            Dim signature As String = "Receiver's Signature : "
            strFooter.Append(signature & "Authorised Signatory".PadLeft(LineLength - signature.Length))
            Dim objclsA4Print As New clsA4Print
            objclsA4Print.DocumentType = clsA4Print.DocumentTypeList.PettyCashVoucher
            'objclsA4Print.A4Title = strTitle.ToString()
            'objclsA4Print.A4CashierDetails = String.Empty
            'objclsA4Print.A4CustomerDetails = String.Empty
            'objclsA4Print.A4Remark = String.Empty
            'objclsA4Print.A4ItemHeader = strGridHeader.ToString()
            'objclsA4Print.A4ItemDetails = strGridItem.ToString()
            'objclsA4Print.A4Footer = strFooter.ToString()


            strPrint.Append(strTitle.ToString() & vbCrLf)
            strPrint.Append(String.Empty) 'A4CashierDetails
            strPrint.Append(String.Empty) 'A4CustomerDetails
            strPrint.Append(String.Empty) 'A4Remark
            strPrint.Append(strGridHeader.ToString() & vbCrLf)
            strPrint.Append(strGridItem.ToString() & strFooter.ToString() & vbCrLf)

            If modCommanFunction.IsPrintingWithDefaultFontReq = True Then
                strPrint.Insert(0, "<FontPCBold>")
                strPrint.Append("</FontPCBold>")
            End If

            PrinterName = SetPrinterName(request.dtPrinterInfo, "PettyCash", "PCV")
            If request.IsPrviewRequiredForVchr Then
                fnPrint(strPrint.ToString(), "PRV")
            Else
                fnPrint(strPrint.ToString(), "PRN")
            End If
            Return True
        Catch ex As Exception
            MsgBox(getValueByKey("CLPBL01"), , "CLPBL01 - " & getValueByKey("CLAE05"))
            Return False
        End Try
    End Function


End Class
