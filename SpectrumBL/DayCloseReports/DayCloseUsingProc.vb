Imports SpectrumCommon
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class DayCloseUsingProc
    Inherits ReportBase

    Public Sub New()

    End Sub

    Enum DayClosetableNames
        DtCashier = 0
        DtTender
        DtDiscount
        DtTax
        DtCard
        DtCurrency
        DtStatistice
        DtCorrectedCashMemo
        DtDeletedCashMemo
        DtInconsistency
        DtFloatingOpen
        DtFloatingClose
        DtFloatingNext
        DtBank
        DtDayClose
    End Enum



    Public Function PrintDayCloseReport(ByRef request As DayCloseReportModel, ByRef doc As Document) As Boolean
        Try
            PrintDayCloseReport = False
            Dim dsDayCloseReport = GetDsDayCloseReport(request)
            If dsDayCloseReport Is Nothing Then Exit Function
            '---------  Day Close -----------------------------------
            Dim objTotalSales As New TotalSales
            objTotalSales.DtDayClose = dsDayCloseReport.Tables(DayClosetableNames.DtDayClose)
            objTotalSales.CreateReport(request, doc)
            '---------   Cashier -----------------------------------
            Dim objSalesByCashier As New SalesByCashier
            objSalesByCashier.DtCashier = dsDayCloseReport.Tables(DayClosetableNames.DtCashier)
            objSalesByCashier.CreateReport(request, doc)
            '---------   Tender -----------------------------------
            Dim objIncomeDetails As New IncomeDetails
            objIncomeDetails.DtTenderDetails = dsDayCloseReport.Tables(DayClosetableNames.DtTender)
            objIncomeDetails.CreateReport(request, doc)
            '---------   Discount -----------------------------------
            Dim objDiscountDetails As New DiscountDetails
            objDiscountDetails.DtDiscount = dsDayCloseReport.Tables(DayClosetableNames.DtDiscount)
            objDiscountDetails.CreateReport(request, doc)
            '---------   Tax -----------------------------------------------------
            Dim objTaxDetails As New TaxDetails
            objTaxDetails.DtTaxDetails = dsDayCloseReport.Tables(DayClosetableNames.DtTax)
            objTaxDetails.CreateReport(request, doc)

            '---------  Credit/Debit Card  Details--------------------------------
            Dim objCardDetails As New CardDetails
            objCardDetails.DtCardDetails = dsDayCloseReport.Tables(DayClosetableNames.DtCard)
            objCardDetails.CreateReport(request, doc)

            '---------   Currency ------------------------------------------------
            Dim objCurrencyDetails As New CurrencyDetails
            objCurrencyDetails.DtCurrency = dsDayCloseReport.Tables(DayClosetableNames.DtCurrency)
            objCurrencyDetails.CreateReport(request, doc)
            '--------Statistics   ------------------------------------------------
            Dim objDayCloseStatistics As New DayCloseStatistics
            objDayCloseStatistics.DtStatistics = dsDayCloseReport.Tables(DayClosetableNames.DtStatistice)
            objDayCloseStatistics.CreateReport(request, doc)
            '---------   Corrected Cash Memo -------------------------------------
            Dim objCorrectedCashMemo As New CorrectedCashMemo
            objCorrectedCashMemo.DtCorrectedCashMemo = dsDayCloseReport.Tables(DayClosetableNames.DtCorrectedCashMemo)
            objCorrectedCashMemo.CreateReport(request, doc)
            '---------   Deleted Cash Memo ---------------------------------------
            Dim objDeletedCashMemo As New DeletedCashMemo
            objDeletedCashMemo.DtDeletedCashMemo = dsDayCloseReport.Tables(DayClosetableNames.DtDeletedCashMemo)
            objDeletedCashMemo.CreateReport(request, doc)
            '---------   In Consistency ------------------------------------------
            Dim objInconsistencyDetails As New InconsistencyDetails
            objInconsistencyDetails.DtInconsistancyDetails = dsDayCloseReport.Tables(DayClosetableNames.DtInconsistency)
            objInconsistencyDetails.CreateReport(request, doc)
            '---------   Floating Open /Floating Close /Floating Next / Bank -------------------------------
            Dim objFloatingDetails As New FloatingDetails
            objFloatingDetails.DtOpenFloatDtls = dsDayCloseReport.Tables(DayClosetableNames.DtFloatingOpen)
            objFloatingDetails.DtcloseFloatDtls = dsDayCloseReport.Tables(DayClosetableNames.DtFloatingClose)
            objFloatingDetails.DtNextDayFloatDtls = dsDayCloseReport.Tables(DayClosetableNames.DtFloatingNext)
            objFloatingDetails.DtBankDetails = dsDayCloseReport.Tables(DayClosetableNames.DtBank)
            objFloatingDetails.CreateReport(request, doc)

            'For index = 0 To dsDayCloseReport.Tables.Count - 1
            '    Dim datatable = dsDayCloseReport.Tables(index)
            '    Select Case dsDayCloseReport.Tables(index).TableName
            '        Case tableNames.DtCashier.ToString
            '            Dim objSalesByCashier As New SalesByCashier
            '            objSalesByCashier.DtCashier = dsDayCloseReport.Tables(index)
            '            objSalesByCashier.CreateReport(request, doc)
            '        Case (tableNames.DtTender.ToString)
            '            Dim objIncomeDetails As New IncomeDetails
            '            objIncomeDetails.DtTenderDetails = dsDayCloseReport.Tables(index)
            '            objIncomeDetails.CreateReport(request, doc)
            '        Case tableNames.DtTax.ToString

            '        Case tableNames.DtCard.ToString

            '        Case tableNames.DtCurrency.ToString

            '        Case tableNames.DtStatistice.ToString

            '        Case tableNames.DtCorrectedCashMemo.ToString

            '        Case tableNames.DtDeletedCashMemo.ToString

            '        Case tableNames.DtInconsistency.ToString

            '        Case tableNames.DtFloatingOpen.ToString

            '        Case tableNames.DtFloatingClose.ToString

            '        Case tableNames.DtFloatingNext.ToString

            '        Case tableNames.DtBank.ToString

            '        Case tableNames.DtDayClose.ToString
            '            Dim objTotalSales As New TotalSales
            '            objTotalSales.DtDayClose = dsDayCloseReport.Tables(index)
            '            objTotalSales.CreateReport(request, doc)
            '        Case Else

            '    End Select
            'Next
            PrintDayCloseReport = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Function GetDsDayCloseReport(request As DayCloseReportModel) As DataSet
        Try
            Dim dsDataset = GetDayCloseReportData(request.SiteCode, request.ToDate)
            'delete pdf in case of  any failure occured in database level
            'added by adil- blank report generete and mail sent issue-2623
            If dsDataset.Tables.Count = 0 Then
                Return Nothing
            End If
            '---- Assign tables to table Name 
            dsDataset.Tables(DayClosetableNames.DtCashier).TableName = "DtCashier"
            dsDataset.Tables(DayClosetableNames.DtTender).TableName = "DtTender"
            dsDataset.Tables(DayClosetableNames.DtTax).TableName = "DtTax"
            dsDataset.Tables(DayClosetableNames.DtCard).TableName = "DtCard"
            dsDataset.Tables(DayClosetableNames.DtCurrency).TableName = "DtCurrency"
            dsDataset.Tables(DayClosetableNames.DtStatistice).TableName = "DtStatistice"
            dsDataset.Tables(DayClosetableNames.DtCorrectedCashMemo).TableName = "DtCorrectedCashMemo"
            dsDataset.Tables(DayClosetableNames.DtDeletedCashMemo).TableName = "DtDeletedCashMemo"
            dsDataset.Tables(DayClosetableNames.DtInconsistency).TableName = "DtInconsistency"
            dsDataset.Tables(DayClosetableNames.DtFloatingOpen).TableName = "DtFloatingOpen"
            dsDataset.Tables(DayClosetableNames.DtFloatingClose).TableName = "DtFloatingClose"
            dsDataset.Tables(DayClosetableNames.DtFloatingNext).TableName = "DtFloatingNext"
            dsDataset.Tables(DayClosetableNames.DtBank).TableName = "DtBank"
            dsDataset.Tables(DayClosetableNames.DtDayClose).TableName = "DtDayClose"

            Return dsDataset
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

End Class
