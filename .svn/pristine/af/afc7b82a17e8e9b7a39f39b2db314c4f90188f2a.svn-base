Imports System.Text
Imports C1.Win.C1BarCode
Imports SpectrumBL
Public Class clsPrintVoucher

    Public Function PrintGiftVoucherAndCreditNote(ByVal pDocType As String, ByVal pSiteCode As String, ByVal VoucherType As String, ByVal VoucherCode As String, ByVal VoucherValue As String, ByVal ExpiryDate As String, ByVal UserName As String, ByVal BillNo As String, ByVal CurrencyCode As String, ByVal IssueDate As String, Optional ByVal BarCodeType As String = "0") As Boolean
        Try
            Dim sbHeader As New StringBuilder
            Dim sbCondn As New StringBuilder
            Dim PrintVoucher As New StringBuilder
            Dim VoucherName As String = String.Empty
            Dim objcomm As New clsCommon
            Dim dtPrint As New DataTable

            Dim strDate = String.Empty, strCashierName = String.Empty, strVoucherCode = String.Empty, strVoucherName As String = String.Empty
            Dim strHeaderText = String.Empty, strFooterText As String = String.Empty

            If ((Not String.IsNullOrEmpty(VoucherValue)) AndAlso (Not Double.Equals(Convert.ToDouble(VoucherValue), 0.0))) Then

                dtPrint = objcomm.GetPrintingDetails("CNGVP")
                Dim dv As New DataView(dtPrint, String.Empty, "TOPBOTTOM", DataViewRowState.CurrentRows)

                'If String.IsNullOrEmpty(VoucherCode) = True AndAlso String.IsNullOrEmpty(ExpiryDate) = True Then
                Dim dtVoucherInfo As New DataTable

                dtVoucherInfo = objcomm.GetVoucherDetails(BillNo, pSiteCode, pDocType, IIf(VoucherCode <> String.Empty, VoucherCode, ""), IIf(VoucherValue <> 0, VoucherValue, 0))
                If Not dtVoucherInfo Is Nothing And dtVoucherInfo.Rows.Count > 0 Then
                    VoucherCode = dtVoucherInfo.Rows(dtVoucherInfo.Rows.Count - 1)("VOURCHERSERIALNBR")
                    VoucherValue = dtVoucherInfo.Rows(dtVoucherInfo.Rows.Count - 1)("VALUEOFVOUCHER")
                    ExpiryDate = dtVoucherInfo.Rows(dtVoucherInfo.Rows.Count - 1)("EXPIRYDATE")
                    VoucherName = dtVoucherInfo.Rows(dtVoucherInfo.Rows.Count - 1)("VOUCHERDESC")
                End If

                'End If
                Dim strSiteHeader As New StringBuilder
                'Added :Rakesh-27/07/2012: Site Information
                strSiteHeader.Append(L40OrA4GetSiteDetails(pSiteCode, "L40"))

                dv.RowFilter = "TOPBOTTOM='Top'"    ' Header

                Dim strHeaderLine = "", strFooterLine As String = String.Empty

                For Each drview As DataRowView In dv
                    strHeaderLine = drview("ReceiptText").ToString()
                    strHeaderText += SplitString(strHeaderLine, 39).ToString().Trim() + vbCrLf
                Next
                If (Not String.IsNullOrEmpty(strHeaderText.Trim())) Then
                    sbHeader.Append(strHeaderText)
                End If

                dv.RowFilter = "TOPBOTTOM='Bottom'"    ' Conditions
                For Each drview As DataRowView In dv
                    strFooterLine = drview("ReceiptText").ToString()
                    strFooterText += SplitString(strFooterLine, 39).ToString().Trim() + vbCrLf
                Next
                If (Not String.IsNullOrEmpty(strFooterText.Trim())) Then
                    sbCondn.Append(strFooterText)
                End If

                PrintVoucher.Length = 0
                PrintVoucher.Append(strSiteHeader)
                PrintVoucher.Append(strLineL40 + vbCrLf)

                If (Not String.IsNullOrEmpty(sbHeader.ToString())) Then
                    PrintVoucher.Append(sbHeader.ToString)
                    PrintVoucher.Append(strLineL40 + vbCrLf)
                End If

                If VoucherType.ToLower = "CreditNote".ToLower Then
                    strVoucherCode = getValueByKey("CLSPV001") + VoucherCode
                    PrintVoucher.Append(SplitString(strVoucherCode, 39).ToString() + vbCrLf)    'CREDIT VOUCHER NO.

                    strVoucherName = getValueByKey("CLSPV002") + VoucherName
                    PrintVoucher.Append(SplitString(strVoucherName, 39).ToString() + vbCrLf)     'CREDIT VOUCHER PROGRAM

                ElseIf VoucherType.ToLower = "GiftVoucher".ToLower Then
                    strVoucherCode = getValueByKey("CLSPV003") + VoucherCode
                    PrintVoucher.Append(SplitString(strVoucherCode, 39).ToString() + vbCrLf)    'GIFT VOUCHER NO.

                    strVoucherName = getValueByKey("CLSPV004") + VoucherName
                    PrintVoucher.Append(SplitString(strVoucherName, 39).ToString() + vbCrLf)    'GIFT VOUCHER PROGRAM
                End If

                PrintVoucher.Append(strLineL40 + vbCrLf)
                strDate = String.Format(getValueByKey("CLSPS4009"), IssueDate)
                PrintVoucher.Append(SplitString(strDate, 39).ToString() + vbCrLf)                             'IssueDate

                strCashierName = String.Format(getValueByKey("MDCMFN008") + UserName)
                PrintVoucher.Append(SplitString(strCashierName).ToString() + vbCrLf)                          'Cashier Name
                PrintVoucher.Append(strLineL40 + vbCrLf)

                PrintVoucher.Append(getValueByKey("CLSPV007") & " " & VoucherValue & "  " & CurrencyCode & vbCrLf)      'VALUE

                'Set Barcode Top Position in Voucher Prints
                Dim totalLine As Integer = PrintVoucher.ToString().Split(vbCrLf).Length
                BarcodeMarginTop = Math.Round(totalLine * 12, 0)
                BarcodeMarginTop = BarcodeMarginTop + 10

                PrintVoucher.Append(vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf)
                PrintVoucher.Append(strLineL40 + vbCrLf)

                Dim IssuedAgainstBill As String = getValueByKey("CLSPV008") & " " & BillNo     'Issued against CASH MEMO No.
                PrintVoucher.Append(SplitString(IssuedAgainstBill, 39).ToString() + vbCrLf)
                PrintVoucher.Append(strLineL40 + vbCrLf)

                Dim ExpiryDateNew As String = String.Empty

                If (ExpiryDate.Contains(Space(1))) Then
                    ExpiryDateNew = ExpiryDate.Split(Space(1))(0)
                Else
                    ExpiryDateNew = ExpiryDate
                End If
                PrintVoucher.Append(getValueByKey("CLSPV009") & " " & ExpiryDateNew + vbCrLf)      'VALID TILL
                PrintVoucher.Append(strLineL40 + vbCrLf)

                PrintVoucher.Append(sbCondn.ToString + vbCrLf)

                Dim s As C1BarCode = GetBarcode(VoucherCode)
                VarBarcode = s

                IsPrintVoucher = True

                If Not dtPrinterInfo1 Is Nothing Then
                    PrinterName = SetPrinterName(dtPrinterInfo1, "Voucher", "")
                End If

                'If _PrintPreview = True Then
                '    fnPrint(PrintVoucher.ToString(), "PRV")
                'Else
                'fnPrint(PrintVoucher.ToString(), "PRV")
                fnPrint(PrintVoucher.ToString(), "PRN")
                'End If
            End If

            IsPrintVoucher = False
            Return True

        Catch ex As Exception
            IsPrintVoucher = False
            Return False
        End Try
    End Function
End Class
