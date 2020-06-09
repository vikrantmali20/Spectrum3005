Imports System.Text
Imports SpectrumBL
Imports System.Data.DataSet
Imports C1.Win.C1BarCode
Public Class clsPrintDenomination
    Dim _PrintPrieview As Boolean


    Public Sub PrintShiftDenomination(ByVal Terminal As String, ByVal DayOpendate As DateTime, ByVal SiteCode As String, ByVal dtMain As DataTable, ByVal strDetail As String, ByVal shiftid As String, ByVal total As String, ByVal printername As String, Optional ByVal dtPrinterInfo As DataTable = Nothing, Optional ByVal CreatedBy As String = "", Optional ByVal ClientName As String = "", Optional ByVal sitename As String = "")
        Try
            Dim strDenomination As New StringBuilder()
            Dim StrLine As String = "========================================"
            Dim sbHdrItem As New StringBuilder()
            Dim Denomination As String = ""
            Dim Qty As String = ""
            Dim Amount As String = ""

            strDenomination.Append(Space(10) & "!!" & ClientName & "!!" & vbCrLf)
            strDenomination.Append(Space(13) & " " & sitename & vbCrLf)
            strDenomination.Append(getValueByKey("CLSPSD002") & " " & Terminal & Space(8) & getValueByKey("CLSPSD003") & shiftid & vbCrLf)
            strDenomination.Append(getValueByKey("CLSPSD004") & " " & DayOpendate.ToString(clsCommon.GetSystemDateFormat()) & Space(8) & getValueByKey("CLSPSD005") & Now.ToString("hh:mm tt") & vbCrLf)
            strDenomination.Append(StrLine & vbCrLf)

            strDenomination.Append(Space(1) & strDetail & Space(10) & vbCrLf)
            strDenomination.Append(StrLine & vbCrLf)

            sbHdrItem.Append(getValueByKey("CLSPSD006") & " " & Space(5) & getValueByKey("CLSPSD007") & " " & Space(7) & getValueByKey("CLSPSD008"))
            strDenomination.Append(sbHdrItem.ToString() & vbCrLf)

            For Each drGrid As DataRow In dtMain.Rows

                ValidateDataRow(drGrid("DenominationAmt"), Denomination)
                ValidateDataRow(drGrid("Qty"), Qty)
                ValidateDataRow(drGrid("Amount"), Amount)
                strDenomination.Append(Denomination.PadLeft(7).ToString() & " " & Qty.PadLeft(13).ToString() & " " & FormatNumber(Amount, 0).PadLeft(13).ToString() & vbCrLf)

                'strDenomination.Append(drGrid("Denomination") & Space(10))
                'strDenomination.Append(drGrid("Qty") & Space(10))
                'strDenomination.Append(drGrid("Amount") & vbCrLf)
            Next

            strDenomination.Append(StrLine & vbCrLf)
            strDenomination.Append(getValueByKey("CLSPSD009") & FormatNumber(total, 0).PadLeft(30) & vbCrLf)
            strDenomination.Append(StrLine & vbCrLf)
            printername = SetPrinterName(dtPrinterInfo, "TillCloseFinReport", "")

            If _PrintPrieview = True Then
                ' If True Then
                fnPrint(strDenomination.ToString(), "PRV")
            Else
                fnPrint(strDenomination.ToString(), "PRN")
            End If

        Catch ex As Exception
            LogException(ex)
        End Try


    End Sub

    Public Sub PrintPCShiftFinancialReport(ByVal dsFinRpt As DataSet, ByVal Terminal As String, ByVal DayOpendate As DateTime, ByVal SiteCode As String, ByVal strDetail As String, ByVal shiftid As String, ByVal printername As String, Optional ByVal dtPrinterInfo As DataTable = Nothing, _
                                Optional ByVal CreatedBy As String = "", Optional ByVal ClientName As String = "", Optional ByVal sitename As String = "", Optional ByVal TransactionAllowed As Boolean = False)
        Try
            Dim StrFinancial As New StringBuilder()
            Dim StrLine As String = "========================================"
            Dim sbHdrItem As New StringBuilder()
            Dim NetAmount As String
            Dim TotalDisc As String
            Dim Tender, Value, TenderLine, TenderNetCash, TenderSummary, TenderDisc, TenderNetSale As String
            Dim StrNetCash, StrSummary, StrDisc, StrNetSale As String
            Dim NetCash As String
            StrFinancial.Append(Space(10) & "!!" & ClientName & "!!" & vbCrLf)
            StrFinancial.Append(Space(13) & " " & sitename & vbCrLf)
            StrFinancial.Append(getValueByKey("CLSPSD002") & " " & Terminal & Space(8) & getValueByKey("CLSPSD003") & shiftid & vbCrLf)
            StrFinancial.Append(getValueByKey("CLSPSD004") & " " & DayOpendate.ToString(clsCommon.GetSystemDateFormat()) & Space(8) & getValueByKey("CLSPSD005") & Now.ToString("hh:mm tt") & vbCrLf)
            StrFinancial.Append(StrLine & vbCrLf)

            StrFinancial.Append(Space(10) & strDetail & Space(10) & vbCrLf)
            StrFinancial.Append(StrLine & vbCrLf)

            Dim Obj As New SpectrumBL.clsCommon
            Dim dtTenderType = Obj.GetTenderInfo(SiteCode)

            '------------------------Net Cash
            For index = 0 To dsFinRpt.Tables("NetCash").Rows.Count - 1
                Value = String.Empty
                Tender = dsFinRpt.Tables("NetCash").Rows(index)("Tender")
                Value = dsFinRpt.Tables("NetCash").Rows(index)("Amount")


                If (Val(NetCash) > 0) Then
                    NetCash = FormatNumber(CDbl(NetCash) + CDbl(Value), 0)
                Else
                    NetCash = FormatNumber(CDbl(Value), 0)
                End If
                If Tender.Length + Value.Length > strLineL40.Length Then
                    TenderNetCash = SplitString(Tender, strLineL40.Length).ToString().Trim & vbCrLf 'Tender & vbCrLf
                    TenderNetCash = TenderNetCash & Value.PadLeft(strLineL40.Length) & vbCrLf
                Else
                    TenderNetCash = Tender & Space(39 - ((Value.Length) + (Tender.Length))) & Value.Replace("-", " ") & vbCrLf & vbCrLf
                End If

                'TenderNetCash = Tender & Space(39 - ((Value.Length) + (Tender.Length))) & Value.Replace("-", " ") & vbCrLf & vbCrLf
                StrNetCash = StrNetCash & TenderNetCash
            Next
            NetCash = IIf(NetCash Is Nothing, 0, NetCash)
            If TransactionAllowed Then
                StrFinancial.Append(StrNetCash)
                StrFinancial.Append(StrLine & vbCrLf)
                StrFinancial.Append("Net Cash In Drawer".PadRight(15) & " " & NetCash.PadLeft(20) & vbCrLf & vbCrLf)
                StrFinancial.Append(StrLine & vbCrLf)
            End If

            '------------------------Summary

            For index = 0 To dsFinRpt.Tables("Summary").Rows.Count - 1
                Value = String.Empty
                Tender = dsFinRpt.Tables("Summary").Rows(index)("Tender")
                Value = IIf(IsDBNull(dsFinRpt.Tables("Summary").Rows(index)("Amount")), 0, dsFinRpt.Tables("Summary").Rows(index)("Amount"))
                TenderSummary = Tender & Space(39 - ((Value.Length) + (Tender.Length))) & Value & vbCrLf & vbCrLf
                StrSummary = StrSummary & TenderSummary
            Next
            StrFinancial.Append(StrSummary)
            StrFinancial.Append(StrLine & vbCrLf)

            '--------------------------Discount
            For index = 0 To dsFinRpt.Tables("Discount").Rows.Count - 1
                Value = String.Empty
                Tender = dsFinRpt.Tables("Discount").Rows(index)("Tender")
                Value = dsFinRpt.Tables("Discount").Rows(index)("Amount")
                If (Val(TotalDisc) > 0) Then
                    TotalDisc = FormatNumber(CDbl(TotalDisc) + CDbl(Value), 0)
                Else
                    TotalDisc = FormatNumber(CDbl(Value), 0)
                End If
                TenderDisc = Tender & Space(39 - ((Value.Length) + (Tender.Length))) & Value & vbCrLf & vbCrLf
                StrDisc = StrDisc & TenderDisc
            Next
            TotalDisc = IIf(TotalDisc Is Nothing, 0, TotalDisc)
            StrFinancial.Append(StrDisc)
            StrFinancial.Append(StrLine & vbCrLf)
            StrFinancial.Append(getValueByKey("CLSPSD013").PadRight(15) & " " & TotalDisc.PadLeft(23) & vbCrLf)
            StrFinancial.Append(StrLine & vbCrLf)

            '------------------Net Sales
            For index = 0 To dsFinRpt.Tables("NetSale").Rows.Count - 1
                Tender = dsFinRpt.Tables("NetSale").Rows(0)("Tender")
                Value = IIf(IsDBNull(dsFinRpt.Tables("NetSale").Rows(0)("Amount")), 0, dsFinRpt.Tables("NetSale").Rows(0)("Amount"))

                TenderNetSale = Tender & Space(39 - ((Value.Length) + (Tender.Length))) & Value & vbCrLf & vbCrLf
                StrNetSale = StrNetSale & TenderNetSale
            Next
            If TransactionAllowed Then
                StrFinancial.Append(StrNetSale)
            End If
            StrFinancial.Append(StrLine & vbCrLf)
            printername = SetPrinterName(dtPrinterInfo, "TillCloseFinReport", "")

            If _PrintPrieview = True Then
                'If True Then
                fnPrint(StrFinancial.ToString(), "PRV")
            Else
                fnPrint(StrFinancial.ToString(), "PRN")
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Public Sub PrintFinancialReport(ByVal Terminal As String, ByVal DayOpendate As DateTime, ByVal SiteCode As String, ByVal strDetail As String, ByVal shiftid As String, ByVal floatamt As String, ByVal dtMain As DataTable, ByVal dtCash As DataTable, ByVal VendorAmount As Double, ByVal printername As String, ByVal amountForTomorrow As String, Optional ByVal dtPrinterInfo As DataTable = Nothing, _
                                            Optional ByVal PettyCashExp As String = "", Optional ByVal PettyCashRec As String = "", Optional ByVal CreatedBy As String = "", Optional ByVal ClientName As String = "", Optional ByVal sitename As String = "", Optional ByVal discount As String = Nothing, Optional ByVal roundoff As String = "", Optional ByVal TransactionAllowed As Boolean = False)
        Try
            Dim StrFinancial As New StringBuilder()
            Dim StrLine As String = "========================================"
            Dim sbHdrItem As New StringBuilder()
            Dim TotalCash, strTotalCheque, strTotalCard, NetSales, strTotalCash, StrReceived As String
            Dim Tender, Value, TenderLine As String
            Dim NetAmount As String
            Dim TotalDisc As String
            StrFinancial.Append(Space(10) & "!!" & ClientName & "!!" & vbCrLf)
            StrFinancial.Append(Space(13) & " " & sitename & vbCrLf)
            StrFinancial.Append(getValueByKey("CLSPSD002") & " " & Terminal & Space(8) & getValueByKey("CLSPSD003") & shiftid & vbCrLf)
            StrFinancial.Append(getValueByKey("CLSPSD004") & " " & DayOpendate.ToString(clsCommon.GetSystemDateFormat()) & Space(8) & getValueByKey("CLSPSD005") & Now.ToString("hh:mm tt") & vbCrLf)
            StrFinancial.Append(StrLine & vbCrLf)

            StrFinancial.Append(Space(10) & strDetail & Space(10) & vbCrLf)
            StrFinancial.Append(StrLine & vbCrLf)

            Dim Obj As New SpectrumBL.clsCommon
            Dim dtTenderType = Obj.GetTenderInfo(SiteCode)
            TotalCash = FormatNumber(IIf(dtCash.Compute("SUM(TOTAL)", "") Is DBNull.Value, 0, dtCash.Compute("SUM(TOTAL)", "")), 0)


            Dim dv As New DataView(dtMain, "", "", DataViewRowState.CurrentRows)
            Dim dtUnique As DataTable = dv.ToTable(True, "TENDERTYPE", "ISSUED")
            For Each dr As DataRow In dtUnique.Rows
                Dim istender() = dtTenderType.Select("TENDERTYPE ='" & dr("TENDERTYPE").ToString() & "'")
                If istender.Count > 0 Then
                    Dim dtrecords() = dtMain.Select(" ISSUED=0 AND TENDERTYPE ='" & dr("TENDERTYPE").ToString() & "'")
                    Value = String.Empty
                    For index = 0 To dtrecords.Count - 1
                        Tender = dtrecords(0)("Description").ToString()
                        If Val(Value) > 0 Then
                            Value = FormatNumber(CDbl(Value) + Val(dtrecords(index)("AMOUNTTENDERED")), 0)
                        Else
                            Value = FormatNumber(Val(dtrecords(index)("AMOUNTTENDERED")), 0)
                        End If
                    Next
                    If Val(Value) > 0 Then
                        If (Val(NetSales) > 0) Then
                            NetSales = FormatNumber(CDbl(NetSales) + CDbl(Value), 0)
                        Else
                            NetSales = FormatNumber(CDbl(Value), 0)
                        End If

                        If Tender = "Cash" Then
                            If TransactionAllowed Then
                                TenderLine = Tender & Space(39 - ((Value.Length) + (Tender.Length))) & Value & vbCrLf & vbCrLf
                            End If
                        Else
                            TenderLine = Tender & Space(39 - ((Value.Length) + (Tender.Length))) & Value & vbCrLf & vbCrLf
                        End If
                        'TenderLine = Tender & Space(39 - ((Value.Length) + (Tender.Length))) & Value & vbCrLf & vbCrLf

                        StrReceived = StrReceived & TenderLine
                        If istender(0)("TENDERTYPE") = "Cash" AndAlso Val(Value) > 0 Then
                            strTotalCash = FormatNumber(CDbl(Value), 0)
                        End If
                        If istender(0)("TENDERTYPE") = "Cheque" AndAlso Val(Value) > 0 Then
                            strTotalCheque = FormatNumber(CDbl(Value), 0)
                        End If
                        If istender(0)("TENDERTYPE") = "CreditCard" AndAlso Val(Value) > 0 Then
                            strTotalCard = FormatNumber(CDbl(Value), 0)
                        End If
                    End If
                End If
            Next
            'strTotalCash = IIf(strTotalCash Is Nothing, 0, strTotalCash)
            'strTotalCheque = IIf(strTotalCheque Is Nothing, 0, strTotalCheque)
            'strTotalCard = IIf(strTotalCard Is Nothing, 0, strTotalCard)
            discount = IIf(discount Is Nothing, 0, discount)
            PettyCashRec = IIf(PettyCashRec = "", 0, PettyCashRec)
            PettyCashExp = IIf(PettyCashExp = "", 0, PettyCashExp)
            floatamt = IIf(floatamt = "", 0, floatamt)
            If Val(NetSales) > 0 Then
                NetAmount = FormatNumber((CDbl(NetSales) + CDbl(floatamt) + CDbl(PettyCashRec)) - CDbl(PettyCashExp), 0)
            End If
            TotalDisc = FormatNumber(CDbl(discount) + CDbl(roundoff), 0)
            If NetAmount = Nothing Then
                NetAmount = FormatNumber((CDbl(floatamt) + CDbl(PettyCashRec)) - CDbl(PettyCashExp), 0)
            End If
            StrFinancial.Append(getValueByKey("CLSPSD018").PadRight(15) & " " & floatamt.PadLeft(23) & vbCrLf & vbCrLf)
            StrFinancial.Append(StrReceived)
            If TransactionAllowed Then
                StrFinancial.Append(getValueByKey("CLSPSD019").PadRight(15) & " " & FormatNumber(PettyCashRec, 0).PadLeft(23) & vbCrLf & vbCrLf)
                StrFinancial.Append(getValueByKey("CLSPSD021").PadRight(15) & " " & FormatNumber(PettyCashExp, 0).PadLeft(23) & vbCrLf & vbCrLf)
                StrFinancial.Append(StrLine & vbCrLf)
                StrFinancial.Append(getValueByKey("CLSPSD020").PadRight(15) & " " & NetAmount.PadLeft(23) & vbCrLf & vbCrLf)
            End If
            StrFinancial.Append(StrLine & vbCrLf)
            StrFinancial.Append(getValueByKey("CLSPSD011").PadRight(15) & " " & FormatNumber(discount, 0).PadLeft(23) & vbCrLf & vbCrLf)
            StrFinancial.Append(getValueByKey("CLSPSD012").PadRight(15) & " " & FormatNumber(roundoff, 0).PadLeft(23) & vbCrLf & vbCrLf)
            StrFinancial.Append(StrLine & vbCrLf)
            StrFinancial.Append(getValueByKey("CLSPSD013").PadRight(15) & " " & TotalDisc.PadLeft(23) & vbCrLf)

            'StrFinancial.Append(getValueByKey("CLSPSD011").PadRight(15) & " " & discount.PadLeft(23) & vbCrLf & vbCrLf)
            'StrFinancial.Append(StrReceived)
            'StrFinancial.Append(StrLine & vbCrLf)
            'StrFinancial.Append(getValueByKey("CLSPSD017").PadRight(15) & " " & NetSales.PadLeft(23) & vbCrLf & vbCrLf)
            'StrFinancial.Append(getValueByKey("CLSPSD018").PadRight(15) & " " & floatamt.PadLeft(21) & vbCrLf & vbCrLf)
            'StrFinancial.Append(getValueByKey("CLSPSD019").PadRight(15) & " " & FormatNumber(PettyCashRec, 0).PadLeft(23) & vbCrLf)
            'StrFinancial.Append(StrLine & vbCrLf)
            'StrFinancial.Append(getValueByKey("CLSPSD020").PadRight(15) & " " & NetAmount.PadLeft(23) & vbCrLf)
            'StrFinancial.Append(StrLine & vbCrLf)

            printername = SetPrinterName(dtPrinterInfo, "TillCloseFinReport", "")

            If _PrintPrieview = True Then
                'If True Then
                fnPrint(StrFinancial.ToString(), "PRV")
            Else
                fnPrint(StrFinancial.ToString(), "PRN")
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub PrintSOBarcode(ByVal sitename As String, ByVal barcode As String, ByVal customername As String, ByVal GeneratedOn As DateTime, ByVal clientname As String, ByVal dtPrinterInfo1 As DataTable)
        Try
            Dim strFinal As New StringBuilder()
            Dim strHeader As String
            strHeader = clientname & "-" & sitename
            ' strFinal.Append(Space(10) & clientname & "-" & sitename & vbCrLf & vbCrLf & vbCrLf)
            strFinal.Append("<B>" & strHeader.PadLeft((((39 - strHeader.Length)) + strHeader.Length) / 2, " ") & "</B>" & vbCrLf & vbCrLf)
            strFinal.Append("Customer:" & customername & vbCrLf & vbCrLf)
            strFinal.Append("Generated On:" & GeneratedOn & vbCrLf & vbCrLf)

            strFinal.Append("<B>" & barcode.PadLeft((((39 - barcode.Length)) + barcode.Length) / 2, " ") & "</B>" & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf)
            strFinal.Append(" This barcode is valid only for 1 hours " & vbCrLf & " from the date of generation")
            'strFinal.Append(Space(10) & barcode & vbCrLf)
            Dim s As C1BarCode = GetBarcode(barcode)
            VarBarcode = s
            modCommanFunction._IsBookingPrint = True
            PrinterName = SetPrinterName(dtPrinterInfo1, "SalesOrderBooking", "")
            If _PrintPrieview = True Then
                fnPrint(strFinal.ToString(), "PRV")
            Else
                fnPrint(strFinal.ToString(), "PRN")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub New(ByVal PrintPreview As Boolean)
        _PrintPrieview = PrintPreview
    End Sub
End Class
