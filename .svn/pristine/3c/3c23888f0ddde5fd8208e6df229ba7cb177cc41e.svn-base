Imports System.Text
Imports SpectrumBL
Imports System.Data.DataSet

Public Class clsPrintFinancialReport
    Dim _UserAmount As Boolean
    Dim _PrintPrieview As Boolean
    'Public Sub PrintFinancialReport(ByVal Terminal As String, ByVal DayOpendate As DateTime, ByVal SiteCode As String, ByVal dtMain As DataTable, ByVal dtCash As DataTable, ByVal VendorAmount As Double, ByVal printername As String, ByVal amountForTomorrow As String, Optional ByVal dtPrinterInfo As DataTable = Nothing, _
    '                                Optional ByVal PettyCashExp As String = "", Optional ByVal PettyCashRec As String = "")
    '    Try
    '        Dim StrFinancial As New StringBuilder()
    '        Dim StrReceived, StrIssued, StrNetSales, StrCashDetial As String
    '        Dim StrLine As String = "========================================"
    '        Dim strCurrency, strAmount, strRate, strTotal, StrCashLine, TotalCash, NetSales, Issued As String
    '        Dim Tender, Value, TenderLine As String
    '        Dim dtOtherIncome As DataTable
    '        Dim Obj As New SpectrumBL.clsCommon
    '        dtOtherIncome = Obj.GetTerminalOtherIncome(Terminal, SiteCode, DayOpendate)
    '        TotalCash = FormatNumber(IIf(dtCash.Compute("SUM(TOTAL)", "") Is DBNull.Value, 0, dtCash.Compute("SUM(TOTAL)", "")), 2)
    '        NetSales = FormatNumber(IIf(dtMain.Compute("Sum(AMOUNTTENDERED)", "Issued=0") Is DBNull.Value, 0, dtMain.Compute("Sum(AMOUNTTENDERED)", "Issued=0")), 2)
    '        Issued = FormatNumber(IIf(dtMain.Compute("Sum(AMOUNTTENDERED)", "Issued=1") Is DBNull.Value, 0, dtMain.Compute("Sum(AMOUNTTENDERED)", "Issued=1")), 2)
    '        NetSales = NetSales + (IIf(Issued <> String.Empty, CDbl(Issued), 0) + VendorAmount)
    '        StrFinancial.Length = 0
    '        'StrFinancial.Append("Store:- " & SiteCode & vbCrLf)
    '        'StrFinancial.Append("Terminal:- " & Terminal & vbCrLf)
    '        'StrFinancial.Append("Date :- " & DayOpendate.ToString(clsCommon.GetSystemDateFormat()) & Space(3) & "Time:- " & Now.ToString("hh:mm:ss") & vbCrLf)

    '        StrFinancial.Append(getValueByKey("CLSPFR001") & " " & SiteCode & vbCrLf)
    '        StrFinancial.Append(getValueByKey("CLSPFR002") & " " & Terminal & vbCrLf)
    '        StrFinancial.Append(getValueByKey("CLSPFR003") & " " & DayOpendate.ToString(clsCommon.GetSystemDateFormat()) & Space(3) & getValueByKey("CLSPFR004") & " " & Now.ToString("hh:mm:ss") & vbCrLf)
    '        StrFinancial.Append(StrLine & vbCrLf)
    '        ' StrFinancial.Append(Space(10) & "FINANCIAL BALANCE" & Space(10) & vbCrLf)
    '        StrFinancial.Append(Space(10) & getValueByKey("CLSPFR005") & Space(10) & vbCrLf)
    '        StrFinancial.Append(StrLine & vbCrLf)
    '        'StrFinancial.Append("Amount collected" & vbCrLf)
    '        StrFinancial.Append(getValueByKey("CLSPFR006") & vbCrLf)
    '        For Each dr As DataRow In dtMain.Select("Issued=0")
    '            Tender = dr("Description").ToString()
    '            If _UserAmount = True AndAlso dr.Table.Columns.Contains("USERAMOUNT") Then
    '                Value = FormatNumber(CDbl(IIf(dr("USERAMOUNT") Is DBNull.Value, CDbl(dr("AMOUNTTENDERED")), dr("USERAMOUNT"))), 2)
    '            Else
    '                Value = FormatNumber(CDbl(dr("AMOUNTTENDERED")), 2)
    '            End If
    '            TenderLine = Tender & Space(39 - ((Value.Length) + (Tender.Length))) & Value & vbCrLf
    '            StrReceived = StrReceived & TenderLine
    '        Next
    '        StrFinancial.Append(StrReceived)
    '        If VendorAmount > 0 Then
    '            'StrFinancial.Append("Other Outcome" & Space(39 - (("Other Outcome".Length) + (VendorAmount.ToString.Length))) & VendorAmount & vbCrLf)
    '            StrFinancial.Append(getValueByKey("CLSPFR007") & Space(39 - ((getValueByKey("CLSPFR007").Length) + (VendorAmount.ToString.Length))) & VendorAmount & vbCrLf)
    '        End If
    '        'StrFinancial.Append(vbCrLf & "Amount Issued" & vbCrLf)
    '        For Each dr As DataRow In dtMain.Select("Issued=1")
    '            Tender = dr("Description").ToString()
    '            Value = FormatNumber(CDbl(dr("AMOUNTTENDERED")), 2)
    '            TenderLine = Tender & Space(39 - ((Value.Length) + (Tender.Length))) & Value & vbCrLf
    '            StrIssued = StrIssued & TenderLine '& dr("Description").ToString() & ":-  " & dr("AMOUNTTENDERED").ToString() & vbCrLf
    '        Next
    '        StrFinancial.Append(StrIssued & vbCrLf)

    '        If Not dtOtherIncome Is Nothing AndAlso dtOtherIncome.Rows.Count > 0 Then
    '            Dim OtherIncome As Decimal = IIf(dtOtherIncome.Compute("SUM(AMOUNTTENDERED)", "") Is DBNull.Value, 0, dtOtherIncome.Compute("SUM(AMOUNTTENDERED)", ""))
    '            OtherIncome = FormatNumber(OtherIncome, 2)
    '            'StrFinancial.Append("//Other Income " & Space(39 - ("//Other Income ".Length + OtherIncome.ToString.Length)) & OtherIncome & vbCrLf & vbCrLf)
    '            StrFinancial.Append(getValueByKey("CLSPFR008") & " " & Space(39 - (Convert.ToString(getValueByKey("CLSPFR008") & " ").Length + OtherIncome.ToString.Length)) & OtherIncome & vbCrLf & vbCrLf)
    '            For Each dr As DataRow In dtOtherIncome.Rows
    '                StrFinancial.Append(dr("Description").ToString() & " " & Space(39 - (Convert.ToString(dr("Description").ToString() & " ").Length + OtherIncome.ToString.Length)) & dr("AmountTendered").ToString() & vbCrLf & vbCrLf)
    '            Next
    '        End If
    '        Dim TerminalCollection As String = NetSales
    '        NetSales = FormatNumber(CDbl(NetSales), 2)
    '        'StrFinancial.Append("Net Sales" & Space(39 - (("Net Sales".Length) + (NetSales.ToString.Length))) & NetSales & vbCrLf)
    '        '-----this line uncommented By Mahesh because NetSales
    '        StrFinancial.Append(getValueByKey("CLSPFR009") & Space(39 - ((getValueByKey("CLSPFR009").Length) + (NetSales.ToString.Length))) & NetSales & vbCrLf)
    '        '------Code Added By Mahesh for Petty Cash Exp and Petty Cash Reciept And Terminal Collection ....
    '        If Val(PettyCashExp) > 0 Then
    '            StrFinancial.Append(vbCrLf)
    '            StrFinancial.Append(getValueByKey("mdispectrum.pettycashlblPettyCashExp") & Space(39 - ((getValueByKey("mdispectrum.pettycashlblPettyCashExp").Length) + (PettyCashExp.ToString.Length))) & PettyCashExp & vbCrLf)
    '        End If

    '        If Val(PettyCashRec) > 0 Then
    '            'StrFinancial.Append(vbCrLf)
    '            StrFinancial.Append(getValueByKey("mdispectrum.pettycashlblPettyCashRec") & Space(39 - ((getValueByKey("mdispectrum.pettycashlblPettyCashRec").Length) + (PettyCashRec.ToString.Length))) & PettyCashRec & vbCrLf)
    '        End If

    '        If Val(PettyCashExp) > 0 Then
    '            TerminalCollection = CDbl(TerminalCollection) - CDbl(PettyCashExp)
    '        End If
    '        If Val(PettyCashRec) > 0 Then
    '            TerminalCollection = CDbl(TerminalCollection) + CDbl(PettyCashRec)
    '        End If
    '        If Val(TerminalCollection) > 0 Then
    '            StrFinancial.Append(vbCrLf)
    '            TerminalCollection = FormatNumber(CDbl(TerminalCollection), 2)
    '            StrFinancial.Append(getValueByKey("frmtillfinancialreport.flowlayoutpanel1.ctrllabel4") & Space(39 - ((getValueByKey("frmtillfinancialreport.ctrllabel4").Length) + (TerminalCollection.ToString.Length))) & TerminalCollection & vbCrLf)
    '        End If


    '        StrFinancial.Append(vbCrLf & vbCrLf & vbCrLf)
    '        'StrFinancial.Append("Currency wise calculation:" & vbCrLf)
    '        StrFinancial.Append(getValueByKey("CLSPFR010") & vbCrLf)
    '        StrFinancial.Append(StrLine & vbCrLf)
    '        'StrFinancial.Append("Currency" & Space(5) & "Amount" & Space(5) & "Rate" & Space(5) & "Total" & vbCrLf)
    '        StrFinancial.Append(getValueByKey("CLSPFR011") & Space(5) & getValueByKey("CLSPFR012") & Space(5) & getValueByKey("CLSPFR013") & Space(5) & getValueByKey("CLSPFR015") & vbCrLf)
    '        StrFinancial.Append(StrLine & vbCrLf)
    '        For Each dr As DataRow In dtCash.Rows
    '            strCurrency = dr("CurrencyCode").ToString()
    '            strAmount = FormatNumber(CDbl(dr("Amount")), 2)
    '            strRate = FormatNumber(CDbl(dr("Rate")), 2)
    '            strTotal = FormatNumber(CDbl(dr("Total")), 2)
    '            StrCashLine = strCurrency.PadRight(10) & strAmount.PadLeft(9) & strRate.PadLeft(8) & strTotal.PadLeft(12)
    '            StrCashDetial = StrCashDetial & StrCashLine & vbCrLf
    '        Next
    '        StrFinancial.Append(StrCashDetial & vbCrLf)
    '        StrFinancial.Append(StrLine & vbCrLf)
    '        'StrFinancial.Append("Total cash collected:" & TotalCash & vbCrLf)

    '        If Val(PettyCashRec) > 0 Then
    '            TotalCash = FormatNumber(CDbl(CDbl(TotalCash) + CDbl(PettyCashRec)), 2)
    '        End If
    '        StrFinancial.Append(getValueByKey("CLSPFR014") & TotalCash & vbCrLf)

    '        'Added by Gaurav Danani 
    '        StrFinancial.Append(vbCrLf)
    '        StrFinancial.Append("Float Amount for Tomorrow:" & amountForTomorrow & vbCrLf)
    '        'Add End

    '        'SpectrumPrint.PrinterName = printername
    '        'Need these data in MstPrintDoc 

    '        printername = SetPrinterName(dtPrinterInfo, "TillCloseFinReport", "")

    '        If _PrintPrieview = True Then
    '            fnPrint(StrFinancial.ToString(), "PRV")
    '        Else
    '            fnPrint(StrFinancial.ToString(), "PRN")
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Public Sub PrintFinancialReport(ByVal Terminal As String, ByVal DayOpendate As DateTime, ByVal SiteCode As String, ByVal dtMain As DataTable, ByVal dtCash As DataTable, ByVal VendorAmount As Double, ByVal printername As String, ByVal amountForTomorrow As String, Optional ByVal dtPrinterInfo As DataTable = Nothing, _
                                   Optional ByVal PettyCashExp As String = "", Optional ByVal PettyCashRec As String = "", Optional ByVal CreatedBy As String = "", Optional ByVal TransactionAllowed As Boolean = False)
        Try
            Dim StrFinancial As New StringBuilder()
            Dim StrReceived, StrIssued, StrNetSales, StrCashDetial, strTotalCash As String
            Dim StrLine As String = "========================================"
            Dim strCurrency, strAmount, strRate, strTotal, StrCashLine, TotalCash, NetSales, Issued As String
            Dim Tender, Value, TenderLine As String
            Dim dtOtherIncome As DataTable
            Dim Obj As New SpectrumBL.clsCommon
            dtOtherIncome = Obj.GetTerminalOtherIncome(Terminal, SiteCode, DayOpendate)
            Dim dtTenderType = Obj.GetTenderInfo(SiteCode)
            TotalCash = FormatNumber(IIf(dtCash.Compute("SUM(TOTAL)", "") Is DBNull.Value, 0, dtCash.Compute("SUM(TOTAL)", "")), 2)
            Issued = FormatNumber(IIf(dtMain.Compute("Sum(AMOUNTTENDERED)", "Issued=1") Is DBNull.Value, 0, dtMain.Compute("Sum(AMOUNTTENDERED)", "Issued=1")), 2)
            StrFinancial.Length = 0
            'StrFinancial.Append("Store:- " & SiteCode & vbCrLf)
            'StrFinancial.Append("Terminal:- " & Terminal & vbCrLf)
            'StrFinancial.Append("Date :- " & DayOpendate.ToString(clsCommon.GetSystemDateFormat()) & Space(3) & "Time:- " & Now.ToString("hh:mm:ss") & vbCrLf)



            StrFinancial.Append(getValueByKey("CLSPFR001") & " " & SiteCode & vbCrLf)
            StrFinancial.Append(getValueByKey("CLSPFR002") & " " & Terminal & vbCrLf)
            StrFinancial.Append(getValueByKey("CLSPFR003") & " " & DayOpendate.ToString(clsCommon.GetSystemDateFormat()) & Space(3) & getValueByKey("CLSPFR004") & " " & Now.ToString("hh:mm:ss") & vbCrLf)
            StrFinancial.Append(StrLine & vbCrLf)

            StrFinancial.Append(Space(10) & getValueByKey("CLSPFR005") & Space(10) & vbCrLf)
            StrFinancial.Append(StrLine & vbCrLf)

            StrFinancial.Append(getValueByKey("CLSPFR006") & vbCrLf)
            'code added for financial report change
            Dim TillOpenValue As Decimal
            Dim objTill As New clsTill()
            TillOpenValue = objTill.GetTillOpenDetail(SiteCode, Terminal, DayOpendate)

            Dim dv As New DataView(dtMain, "", "", DataViewRowState.CurrentRows)
            Dim dtUnique As DataTable = dv.ToTable(True, "TENDERHEADCODE", "ISSUED")

            For Each dr As DataRow In dtUnique.Rows
                Dim istender() = dtTenderType.Select("TENDERHEADCODE ='" & dr("TENDERHEADCODE").ToString() & "'")
                If istender.Count > 0 Then
                    Dim dtrecords() = dtMain.Select(" ISSUED=0 AND TENDERHEADCODE ='" & dr("TENDERHEADCODE").ToString() & "'")
                    Value = String.Empty
                    For index = 0 To dtrecords.Count - 1
                        Tender = dtrecords(0)("Description").ToString()
                        If Val(Value) > 0 Then
                            Value = FormatNumber(CDbl(Value) + Val(dtrecords(index)("AMOUNTTENDERED")), 2)
                        Else
                            Value = FormatNumber(Val(dtrecords(index)("AMOUNTTENDERED")), 2)
                        End If
                    Next
                    If Val(Value) > 0 Then
                        If (Val(NetSales) > 0) Then
                            NetSales = FormatNumber(CDbl(NetSales) + CDbl(Value), 2)
                        Else
                            NetSales = FormatNumber(CDbl(Value), 2)
                        End If
                        If Tender = "Cash" Then
                            If TransactionAllowed Then
                                ' TenderLine = Tender & Space(39 - ((Value.Length) + (Tender.Length))) & Value & vbCrLf
                                Dim a = FormatNumber(CDbl(Value) - CDbl(TillOpenValue), 2)
                                TenderLine = Tender & Space(39 - ((a.Length) + (Tender.Length))) & a & vbCrLf
                                StrReceived = StrReceived & TenderLine
                            End If
                        Else
                            TenderLine = Tender & Space(39 - ((Value.Length) + (Tender.Length))) & Value & vbCrLf
                            StrReceived = StrReceived & TenderLine
                        End If

                        If istender(0)("TENDERTYPE") = "Cash" AndAlso Val(Value) > 0 Then
                            strTotalCash = FormatNumber(CDbl(Value), 0)
                        End If
                    End If
                End If
            Next

            StrFinancial.Append(StrReceived)
            If VendorAmount > 0 Then
                'StrFinancial.Append("Other Outcome" & Space(39 - (("Other Outcome".Length) + (VendorAmount.ToString.Length))) & VendorAmount & vbCrLf)
                StrFinancial.Append(getValueByKey("CLSPFR007") & Space(39 - ((getValueByKey("CLSPFR007").Length) + (VendorAmount.ToString.Length))) & VendorAmount & vbCrLf)
            End If
            'StrFinancial.Append(vbCrLf & "Amount Issued" & vbCrLf)
            For Each dr As DataRow In dtMain.Select("Issued=1")
                Tender = dr("Description").ToString()
                Value = FormatNumber(CDbl(dr("AMOUNTTENDERED")), 2)
                TenderLine = Tender & Space(39 - ((Value.Length) + (Tender.Length))) & Value & vbCrLf
                StrIssued = StrIssued & TenderLine '& dr("Description").ToString() & ":-  " & dr("AMOUNTTENDERED").ToString() & vbCrLf
            Next
            StrFinancial.Append(StrIssued & vbCrLf)

            If Not dtOtherIncome Is Nothing AndAlso dtOtherIncome.Rows.Count > 0 Then
                Dim OtherIncome As Decimal = IIf(dtOtherIncome.Compute("SUM(AMOUNTTENDERED)", "") Is DBNull.Value, 0, dtOtherIncome.Compute("SUM(AMOUNTTENDERED)", ""))
                OtherIncome = FormatNumber(OtherIncome, 2)
                'StrFinancial.Append("//Other Income " & Space(39 - ("//Other Income ".Length + OtherIncome.ToString.Length)) & OtherIncome & vbCrLf & vbCrLf)
                StrFinancial.Append(getValueByKey("CLSPFR008") & " " & Space(39 - (Convert.ToString(getValueByKey("CLSPFR008") & " ").Length + OtherIncome.ToString.Length)) & OtherIncome & vbCrLf & vbCrLf)
                For Each dr As DataRow In dtOtherIncome.Rows
                    StrFinancial.Append(dr("Description").ToString() & " " & Space(39 - (Convert.ToString(dr("Description").ToString() & " ").Length + OtherIncome.ToString.Length)) & dr("AmountTendered").ToString() & vbCrLf & vbCrLf)
                Next
            End If
            Dim TerminalCollection As String = NetSales

            '----- Code Added By Mahesh float Amt is not part of Net Sale  START
            'Dim TillOpenValue As Decimal
            'Dim objTill As New clsTill()
            'TillOpenValue = objTill.GetTillOpenDetail(SiteCode, Terminal, DayOpendate)
            'code added for financial report change
            Dim float = FormatNumber(CDbl(TillOpenValue), 2)
            StrFinancial.Append("Float" & Space(39 - ((float.ToString.Length) + ("Float".Length))) & float & vbCrLf)

            If Val(NetSales) > 0 Then
                'code added and added for financial report change
                ' NetSales = CDbl(NetSales) - TillOpenValue
               

            End If

            '----- Code Added By Mahesh float Amt is not part of Net Sale  END
            NetSales = FormatNumber(CDbl(NetSales), 2)
            If TransactionAllowed Then
                'code commented and added for financial report change
                '  StrFinancial.Append("Net Sales" & Space(39 - (("Net Sales".Length) + (NetSales.ToString.Length))) & NetSales & vbCrLf)
                Dim drow1 = dtMain.Select("TENDERTYPE= 'CASH'")
                If drow1.Length > 0 Then
                    NetSales = FormatNumber((CDbl(NetSales) - CDbl(TillOpenValue)), 2)
                    StrFinancial.Append("Net Sales" & Space(39 - (("Net Sales".Length) + (NetSales.ToString.Length))) & NetSales & vbCrLf)

                Else
                    NetSales = FormatNumber(CDbl(NetSales), 2)
                    StrFinancial.Append("Net Sales" & Space(39 - (("Net Sales".Length) + (NetSales.ToString.Length))) & NetSales & vbCrLf)
                End If
            End If
            '------Code Added By Mahesh for Petty Cash Exp and Petty Cash Reciept And Terminal Collection ....

            If Val(PettyCashExp) > 0 Then
                StrFinancial.Append(vbCrLf)
                StrFinancial.Append(getValueByKey("mdispectrum.pettycashlblPettyCashExp") & Space(39 - ((getValueByKey("mdispectrum.pettycashlblPettyCashExp").Length) + (PettyCashExp.ToString.Length))) & PettyCashExp & vbCrLf)
            End If
            If TransactionAllowed Then
                If Val(PettyCashRec) > 0 Then
                    'StrFinancial.Append(vbCrLf)
                    StrFinancial.Append(getValueByKey("mdispectrum.pettycashlblPettyCashRec") & Space(39 - ((getValueByKey("mdispectrum.pettycashlblPettyCashRec").Length) + (PettyCashRec.ToString.Length))) & PettyCashRec & vbCrLf)
                End If
                '-----this line uncommented By Mahesh because NetSales
                '  StrFinancial.Append(getValueByKey("CLSPFR009") & Space(39 - ((getValueByKey("CLSPFR009").Length) + (NetSales.ToString.Length))) & NetSales & vbCrLf)


                If Val(PettyCashExp) > 0 Then
                    TerminalCollection = FormatNumber(CDbl(TerminalCollection) - CDbl(PettyCashExp), 2)
                    If (Val(strTotalCash) > 0) Then
                        strTotalCash = FormatNumber(CDbl(strTotalCash) - CDbl(PettyCashExp), 2)
                    Else
                        strTotalCash = FormatNumber(-CDbl(PettyCashExp), 2)
                    End If
                End If

                If Val(PettyCashRec) > 0 Then
                    TerminalCollection = FormatNumber(CDbl(TerminalCollection) + CDbl(PettyCashRec), 2)
                    If (Val(strTotalCash) > 0) Then
                        strTotalCash = FormatNumber(CDbl(strTotalCash) + CDbl(PettyCashRec), 2)
                    Else
                        strTotalCash = FormatNumber(CDbl(PettyCashRec), 2)
                    End If
                End If
                If Val(strTotalCash) > 0 Then
                    StrFinancial.Append(vbCrLf)
                    strTotalCash = FormatNumber(CDbl(strTotalCash), 2)
                    '    StrFinancial.Append(getValueByKey("frmtillfinancialreport.flowlayoutpanel1.lbltotalcashcollection") & Space(39 - ((getValueByKey("frmtillfinancialreport.flowlayoutpanel1.lbltotalcashcollection").Length) + (strTotalCash.ToString.Length))) & strTotalCash & vbCrLf)
                End If
                If Val(TerminalCollection) > 0 Then
                    StrFinancial.Append(vbCrLf)
                    'code commented and added for financial report change
                    TerminalCollection = FormatNumber(CDbl(TerminalCollection), 2)
                    If dtMain.Rows.Count > 0 Then
                        Dim drrow = dtMain.Select("TENDERTYPE= 'CASH'")
                        If drrow.Length > 0 Then
                            TerminalCollection = FormatNumber(CDbl(TerminalCollection), 2)
                            StrFinancial.Append(getValueByKey("frmtillfinancialreport.flowlayoutpanel1.ctrllabel4") & Space(39 - ((getValueByKey("frmtillfinancialreport.ctrllabel4").Length) + (TerminalCollection.ToString.Length))) & TerminalCollection & vbCrLf)
                        Else
                            TerminalCollection = FormatNumber(CDbl(TerminalCollection) + CDbl(TillOpenValue), 2)
                            StrFinancial.Append(getValueByKey("frmtillfinancialreport.flowlayoutpanel1.ctrllabel4") & Space(39 - ((getValueByKey("frmtillfinancialreport.ctrllabel4").Length) + (TerminalCollection.ToString.Length))) & TerminalCollection & vbCrLf)

                        End If
                    End If

                    'StrFinancial.Append(getValueByKey("frmtillfinancialreport.flowlayoutpanel1.ctrllabel4") & Space(39 - ((getValueByKey("frmtillfinancialreport.ctrllabel4").Length) + (TerminalCollection.ToString.Length))) & TerminalCollection & vbCrLf)
                End If

                StrFinancial.Append(vbCrLf & vbCrLf & vbCrLf)
                'StrFinancial.Append("Currency wise calculation:" & vbCrLf)
                StrFinancial.Append(getValueByKey("CLSPFR010") & vbCrLf)
                StrFinancial.Append(StrLine & vbCrLf)
                'StrFinancial.Append("Currency" & Space(5) & "Amount" & Space(5) & "Rate" & Space(5) & "Total" & vbCrLf)
                StrFinancial.Append(getValueByKey("CLSPFR011") & Space(5) & getValueByKey("CLSPFR012") & Space(5) & getValueByKey("CLSPFR013") & Space(5) & getValueByKey("CLSPFR015") & vbCrLf)
            End If
            StrFinancial.Append(StrLine & vbCrLf)
            If TransactionAllowed Then
                For Each dr As DataRow In dtCash.Rows
                    strCurrency = dr("CurrencyCode").ToString()
                    strAmount = FormatNumber(CDbl(dr("Amount")), 2)
                    strRate = FormatNumber(CDbl(dr("Rate")), 2)
                    strTotal = FormatNumber(CDbl(dr("Total")), 2)
                    StrCashLine = strCurrency.PadRight(10) & strAmount.PadLeft(9) & strRate.PadLeft(8) & strTotal.PadLeft(12)
                    StrCashDetial = StrCashDetial & StrCashLine & vbCrLf
                Next

                StrFinancial.Append(StrCashDetial)
                ' StrFinancial.Append(StrLine & vbCrLf)
                'StrFinancial.Append("Total cash collected:" & TotalCash & vbCrLf)
                StrFinancial.Append(StrLine & vbCrLf)
                'code added for financial report change
                '  StrFinancial.Append(getValueByKey("CLSPFR014") & strTotalCash & vbCrLf)
                Dim drow = dtMain.Select("TENDERTYPE= 'CASH'")
                If drow.Length > 0 Then
                    StrFinancial.Append(getValueByKey("CLSPFR014") & strTotalCash & vbCrLf)
                Else
                    StrFinancial.Append(getValueByKey("CLSPFR014") & float & vbCrLf)
                End If
                StrFinancial.Append(StrLine & vbCrLf)
                StrFinancial.Append(vbCrLf)
                StrFinancial.Append("Float Amount for Tomorrow:" & amountForTomorrow & vbCrLf)
                StrFinancial.Append("Created By:" & CreatedBy & vbCrLf)
                StrFinancial.Append(vbCrLf)
                StrFinancial.Append(vbCrLf)
            End If
            StrFinancial.Append(vbCrLf)
            StrFinancial.Append(vbCrLf)
            StrFinancial.Append(vbCrLf)
            'Added by Gaurav Danani 
            ' StrFinancial.Append(vbCrLf)
            '   StrFinancial.Append("Float Amount for Tomorrow:" & amountForTomorrow & vbCrLf)
            'Add End
            'SpectrumPrint.PrinterName = printername
            'Need these data in MstPrintDoc 
            '  StrFinancial.Append(StrLine & vbCrLf)
            printername = SetPrinterName(dtPrinterInfo, "TillCloseFinReport", "")

            If _PrintPrieview = True Then
                fnPrint(StrFinancial.ToString(), "PRV")
            Else
                fnPrint(StrFinancial.ToString(), "PRN")
            End If

        Catch ex As Exception

        End Try
    End Sub


    Public Sub New(ByVal PrintPreview As Boolean, ByVal UserAmount As Boolean)
        _PrintPrieview = PrintPreview
        _UserAmount = UserAmount
    End Sub
End Class
