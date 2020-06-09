Imports Microsoft.Office.Interop
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class IncomeDetails
    Inherits ReportBase
    Implements IDayCloseReport

    Dim ADVANCE_CREATED_STAUS = "Open"
    Dim CREDIT_VOUCHER_RECEIVED = "CreditVouc(R)"
    Dim GIFT_VOUCHER_RECEIVED = "GiftVoucher(R)"
    Dim CREDIT_VOUCHER_ISSUED = "CreditVouc(I)"
    Dim GIFT_VOUCHER_ISSUED = "GiftVoucher(I)"
    Dim TENDER_TYPE_CASH = "%CASH%"
    Dim TENDER_TYPE_CHEQUE = "%CHEQUE%"
    Dim issuedInDocType = "CMS"
    Dim clpTenderTypeCode = "CLPPOINT"
    Dim serviceArticleType = "Service"
    Dim PLUS_SIGN = "+ " '// space is mandatory
    Dim MINUS_SIGN = "- "
    Dim SLASH_SIGN = "/ "
    Dim FLOATING_DETAIL_DAY_CLOSE_ACTION_FOR_NEXT_DAY_CARRY_AMT = "Open"
    Dim billIntermediateStatus = "Deleted"

    Private _DtTenderDetails As DataTable
    Public Property DtTenderDetails() As DataTable
        Get
            Return _DtTenderDetails
        End Get
        Set(ByVal value As DataTable)
            _DtTenderDetails = value
        End Set
    End Property


    Public Sub CreateReport(ByRef request As SpectrumCommon.DayCloseReportModel, ByRef doc As Document) Implements IDayCloseReport.CreateReport
        Try
            'doc.Add(New Phrase("Income Details", GetHeaderFont()))
            doc.Add(New Paragraph("Tender Wise Net Sales Details", GetContentFontBold()) With {.Alignment = 1})
            'doc.Add(New Phrase(Environment.NewLine))
            'Dim dt As DataTable = GetDayCloseDataSet(request)
            If DtTenderDetails.Rows.Count > 0 Then tblSumValue = DtTenderDetails.Compute("SUM(AMOUNT)", "TenderNature='+'") - DtTenderDetails.Compute("SUM(AMOUNT)", "TenderNature='-'")
            DtTenderDetails.Columns.Remove("TenderNature")
            PrintDataTable(DtTenderDetails, doc)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    'Private Function GetDayCloseDataSet(ByRef request As SpectrumCommon.DayCloseReportModel) As DataTable
    '    Try
    '        Dim reportQuery As String = "select DESCRIPTION As Tender, Convert(Numeric(18,2), sum(AMOUNTTENDERED)) as Amount from  " & _
    '                                      "(SELECT B.TENDERTYPE,C.DESCRIPTION, A.AMOUNTTENDERED,  " & _
    '                                      "CONVERT(BIT,CASE WHEN A.TENDERTYPECODE='GiftVoucher(I)' THEN  " & _
    '                                     "1 ELSE 0 END) AS ISSUED FROM cashmemohdr D inner join CashMemoReceipt A  " & _
    '                                      "ON d.SiteCode=A.Sitecode and d.FinYear=A.finyear and d.BillNo= A.Billno and  " & _
    '                                      "d.BillIntermediateStatus <> 'Deleted' INNER JOIN MSTTENDER B  " & _
    '                                      "ON A.TENDERHEADCODE= B.TENDERHEADCODE AND A.TENDERTYPECODE=B.TENDERTYPE  " & _
    '                                      "AND B.SITECODE='" & request.SiteCode & "' INNER JOIN MSTTENDERTYPE C ON  " & _
    '                                      "B.TENDERTYPE=C.TENDERTYPE WHERE convert(datetime,CONVERT(VARCHAR(10), A.cmRcptDate ,101)) = '" & request.ToDate.ToString("yyyy-MM-dd") & "' " & _
    '                                      " AND  " & _
    '                                      "A.SITECODE='" & request.SiteCode & "' " & _
    '                                      "and A.TENDERTYPECODE <> 'GiftVoucher(I)' " & _
    '                                      "and a.TENDERHEADCODE <> 'CreditCheque' UNION  " & _
    '                                      "ALL SELECT A.Tendertypecode,C.DESCRIPTION, A.AMOUNTTENDERED,CONVERT(BIT,CASE  " & _
    '                                      "WHEN A.TENDERTYPECODE='CreditVouc(Iss)' THEN 1 ELSE 0 END)  " & _
    '                                      "AS ISSUED FROM SALESINVOICE A inner join salesorderhdr b  " & _
    '                                      "on a.sitecode=b.sitecode and a.documentnumber=b.saleordernumber and b.sostatus<>'Cancel'  " & _
    '                                      "INNER join MSTTENDERTYPE C  " & _
    '                                      "on A.Tendertypecode=C.TENDERTYPE " & _
    '                                      "WHERE convert(datetime,CONVERT(VARCHAR(10), A.soinvdate ,101)) = '" & request.ToDate.ToString("yyyy-MM-dd") & "' " & _
    '                                      " AND A.SITECODE='" & request.SiteCode & "'  AND  " & _
    '                                      "A.TENDERTYPECODE <>  " & _
    '                                     "'GiftVoucher(I)' and a.TENDERHEADCODE <> 'CreditCheque'  " & _
    '                                      "Union ALL SELECT A.Tendertypecode,C.DESCRIPTION, A.AMOUNTTENDERED,CONVERT(BIT,CASE  " & _
    '                                      "WHEN A.TENDERTYPECODE='CreditVouc(Iss)' THEN 1 ELSE 0 END)  " & _
    '                                      "AS ISSUED FROM SALESINVOICE A inner join Birthlist b  " & _
    '                                      "on a.sitecode=b.sitecode and a.documentnumber=b.birthlistid and b.BirthListStatus<>'Cancel'  " & _
    '                                      "INNER join MSTTENDERTYPE C  " & _
    '                                      "on A.Tendertypecode=C.TENDERTYPE  " & _
    '                                      "WHERE convert(datetime,CONVERT(VARCHAR(10), A.soinvdate ,101)) = '" & request.ToDate.ToString("yyyy-MM-dd") & "' " & _
    '                                      " AND A.SITECODE='" & request.SiteCode & "' " & _
    '                                      "and A.TENDERTYPECODE <> 'GiftVoucher(I)' and a.TENDERHEADCODE <> 'CreditCheque' ) temp group by TENDERTYPE,DESCRIPTION "

    '        Return GetFilledTable(reportQuery)
    '    Catch ex As Exception
    '        LogException(ex)
    '        Return Nothing
    '    End Try
    'End Function

    Public Function GetDayCloseDataSet(ByRef request As SpectrumCommon.DayCloseReportModel) As DataTable
        Dim resultListTender As DataTable
        Dim resultListTotalTender As DataTable
        Dim dayCloseIncomeDetailsDTOlist As New DataTable
        dayCloseIncomeDetailsDTOlist.Columns.Add("Tender", System.Type.GetType("System.String"))
        dayCloseIncomeDetailsDTOlist.Columns.Add("Amount", System.Type.GetType("System.Decimal"))
        dayCloseIncomeDetailsDTOlist.Columns.Add("TenderNature", System.Type.GetType("System.String"))

        Dim resultListTenderString = "SELECT 'GiftVoucher' AS TENDERTYPE,'Gift Voucher' AS " & _
                              "  DESCRIPTION,SUM(VALUEOFVOUCHER) AS AMOUNTTENDERED,CONVERT(BIT,1) AS ISSUED " & _
                               " FROM VOUCHERDTLS A INNER JOIN MSTVOUCHER B ON A.VOUCHERCODE=B.VOUCHERCODE " & _
                               " AND B.VOURCHERTYPE='" & GIFT_VOUCHER_ISSUED & "' INNER JOIN CASHMEMOHDR C ON " & _
                               " A.ISSUEDDOCNUMBER=C.BILLNO AND A.SITECODE=C.SITECODE AND " & _
                               " A.ISSUEDINDOCTYPE='" & issuedInDocType & "'" & " And C.BillIntermediateStatus <> 'Deleted' " & _
                               " WHERE A.Isactive=1 and convert(datetime,CONVERT(VARCHAR(10), A.IssuedOnDate ,101)) = (Select opendate from DayOpenNClose " & _
                               " Where daycloseStatus=0 and sitecode='" & request.SiteCode & "')"

        '.setParameter("siteCode", Sitecode).setParameter("voucherType", & _GIFT_VOUCHER_ISSUED).setParameter("issuedInDocType", & _issuedInDocType).setParameter("daycloseStatus",daycloseStatus=0) & _.getResultList();
        resultListTender = GetFilledTable(resultListTenderString)

        Dim resultListTotalTenderString = " select TENDERTYPE,DESCRIPTION,sum(AMOUNTTENDERED) as AMTTEN from " _
                                                & " (SELECT B.TENDERTYPE,C.DESCRIPTION, A.AMOUNTTENDERED, " _
                                                & " CONVERT(BIT,CASE WHEN A.TENDERTYPECODE= '" & CREDIT_VOUCHER_ISSUED & "'THEN " _
                                                & "1 ELSE 0 END) AS ISSUED FROM cashmemohdr D inner join CashMemoReceipt A " _
                                                & " ON d.SiteCode=A.Sitecode and d.FinYear=A.finyear and d.BillNo= A.Billno and " _
                                                & " d.BillIntermediateStatus <> '" & billIntermediateStatus & "' INNER JOIN MSTTENDER B " _
                                                & " ON A.TENDERHEADCODE= B.TENDERHEADCODE AND A.TENDERTYPECODE=B.TENDERTYPE " _
                                                & " AND B.SITECODE='" & request.SiteCode & "' INNER JOIN MSTTENDERTYPE C ON " _
                                                & " B.TENDERTYPE=C.TENDERTYPE WHERE convert(datetime,CONVERT(VARCHAR(10), A.cmRcptDate ,101)) ='" & request.ToDate.ToString("yyyy-MM-dd") & "'  AND " _
                                                & " A.SITECODE='" & request.SiteCode & "' AND  A.TENDERTYPECODE <> '" & clpTenderTypeCode & "'" _
                                                & " and A.TENDERTYPECODE <> '" & GIFT_VOUCHER_ISSUED & "'" _
                                                & " and a.TENDERHEADCODE <> 'CreditCheque' UNION " _
                                                & " ALL SELECT A.Tendertypecode,C.DESCRIPTION, A.AMOUNTTENDERED,CONVERT(BIT,CASE " _
                                                & " WHEN A.TENDERTYPECODE='" & CREDIT_VOUCHER_ISSUED & "' THEN 1 ELSE 0 END) " _
                                                & " AS ISSUED FROM SALESINVOICE A inner join salesorderhdr b " _
                                                & " on a.sitecode=b.sitecode and a.documentnumber=b.saleordernumber and b.sostatus<>'Cancel' " _
                                                & " INNER join MSTTENDERTYPE C " _
                                                & " on A.Tendertypecode=C.TENDERTYPE" _
                                                & " WHERE A.status=1 and convert(datetime,CONVERT(VARCHAR(10), A.soinvdate ,101)) = '" & request.ToDate.ToString("yyyy-MM-dd") & "' AND A.SITECODE='" & request.SiteCode & "'  AND " _
                                                & " A.TENDERTYPECODE <> '" & clpTenderTypeCode & "' and A.TENDERTYPECODE <> " _
                                                & " '" & GIFT_VOUCHER_ISSUED & "' and a.TENDERHEADCODE <> 'CreditCheque' " _
                                                & " Union ALL SELECT A.Tendertypecode,C.DESCRIPTION, A.AMOUNTTENDERED,CONVERT(BIT,CASE " _
                                                & " WHEN A.TENDERTYPECODE='" & CREDIT_VOUCHER_ISSUED & "' THEN 1 ELSE 0 END) " _
                                                & " AS ISSUED FROM SALESINVOICE A inner join Birthlist b " _
                                                & " on a.sitecode=b.sitecode and a.documentnumber=b.birthlistid and b.BirthListStatus<>'Cancel' " _
                                                & " INNER join MSTTENDERTYPE C " _
                                                & " on A.Tendertypecode=C.TENDERTYPE" _
                                                & " WHERE A.status=1 and convert(datetime,CONVERT(VARCHAR(10), A.soinvdate ,101)) ='" & request.ToDate.ToString("yyyy-MM-dd") & "' AND A.SITECODE='" & request.SiteCode & "'  AND " _
                                                & " A.TENDERTYPECODE <> '" & clpTenderTypeCode & "' and A.TENDERTYPECODE <> '" & GIFT_VOUCHER_ISSUED & "' and a.TENDERHEADCODE <> 'CreditCheque' ) temp group by TENDERTYPE,DESCRIPTION"

        '.setParameter("billIntermediateStatus", "Deleted")'.setParameter("tenderTypeGiftVoucherIssued",GIFT_VOUCHER_ISSUED).setParameter(  "tenderTypeCreditVoucherIssued", CREDIT_VOUCHER_ISSUED).setParameter("daycloseStatus",daycloseStatus=0).setParameter("siteCode",'siteCode).setParameter("clpTenderTypeCode",' clpTenderTypeCode).getResultList();

        resultListTotalTender = GetFilledTable(resultListTotalTenderString)
        Dim dr As DataRow
        '   If (resultListTender.Rows.Count = 0 AndAlso resultListTotalTender.Rows.Count > 0) Then
        For Each result In resultListTender.Rows
            If IIf(IsDBNull(result(2)), 0, result(2)) > 0 Then
                dr = dayCloseIncomeDetailsDTOlist.NewRow()
                dr(0) = MINUS_SIGN & result(1)
                dr(1) = IIf(IsDBNull(result(2)), 0, result(2))
                dr(2) = MINUS_SIGN.ToString.Trim()
                dayCloseIncomeDetailsDTOlist.Rows.Add(dr)
            End If

        Next
        '  End If


        '  if (resultListTotalTender != null && !resultListTotalTender.isEmpty()) {
        For Each result In resultListTotalTender.Rows
            If Math.Abs(IIf(IsDBNull(result(2)), 0, result(2))) > 0 Then
                dr = dayCloseIncomeDetailsDTOlist.NewRow()
                Dim tenderType = result(0)
                Dim description = result(1)

                If (tenderType = CREDIT_VOUCHER_ISSUED) Then
                    description = MINUS_SIGN + description
                    dr(2) = MINUS_SIGN.ToString.Trim()
                Else
                    description = PLUS_SIGN + description
                    dr(2) = PLUS_SIGN.ToString.Trim()
                End If
                dr(0) = description


                If (description = "+ Cash") Then
                    Dim OtherOutcome = getOtherOutcome(request)
                    dr(1) = result(2) - OtherOutcome
                ElseIf (tenderType.ToString.ToUpper() = CREDIT_VOUCHER_ISSUED.ToUpper()) Then
                    dr(1) = result(2) * (-1)
                Else
                    dr(1) = result(2)
                End If
                dayCloseIncomeDetailsDTOlist.Rows.Add(dr)
            End If
        Next
        ' End if
        ' CREDITCHECK
        Dim CreditCheckAmt = getCreditChecks(request)
        dr = dayCloseIncomeDetailsDTOlist.NewRow()
        dr(0) = PLUS_SIGN & "Credit Cheque"
        dr(1) = CreditCheckAmt
        dr(2) = PLUS_SIGN.ToString.Trim()
        dayCloseIncomeDetailsDTOlist.Rows.Add(dr)


        ' ADVANCEUSED
        Dim AdvanceUsed = getAdvanceUsed(request)
        dr = dayCloseIncomeDetailsDTOlist.NewRow()
        dr(0) = PLUS_SIGN & "Advanced Used"
        dr(1) = AdvanceUsed
        dr(2) = PLUS_SIGN.ToString.Trim()
        dayCloseIncomeDetailsDTOlist.Rows.Add(dr)
        ' OTHER OUT COME -----
        Dim OtherOutComeM = getOtherOutcome(request)
        dr = dayCloseIncomeDetailsDTOlist.NewRow()
        dr(0) = PLUS_SIGN & "Other Outcome"
        dr(1) = OtherOutComeM
        dr(2) = PLUS_SIGN.ToString.Trim()
        dayCloseIncomeDetailsDTOlist.Rows.Add(dr)
        ' ADVANCECREATED()
        Dim AdvanceCreated = getAdvanceCreated(request)
        dr = dayCloseIncomeDetailsDTOlist.NewRow()
        dr(0) = MINUS_SIGN & "Advance Created"
        dr(1) = AdvanceCreated
        dr(2) = MINUS_SIGN.ToString.Trim()
        dayCloseIncomeDetailsDTOlist.Rows.Add(dr)
        ' OtherIncome()
        Dim dtOtherIncome = getOtherIncome(request)
        For Each result In dtOtherIncome.Rows
            dr = dayCloseIncomeDetailsDTOlist.NewRow()
            dr(0) = result(0)
            dr(1) = result(1)
            dr(2) = result(2)
            dayCloseIncomeDetailsDTOlist.Rows.Add(dr)
        Next
        Return dayCloseIncomeDetailsDTOlist

    End Function

    'CREDITCHECK
    Private Function getCreditChecks(ByRef request As SpectrumCommon.DayCloseReportModel) As Double
        Try
            Dim totalCreditCheckAmtString = " select sum(amount) from checkdtls where billdate ='" & request.ToDate.ToString("yyyy-MM-dd") & "'" _
                     & " and sitecode ='" & request.SiteCode & "'"

            Dim dt = GetFilledTable(totalCreditCheckAmtString)

            If dt.Rows.Count > 0 Then getCreditChecks = Val(IIf(IsDBNull(dt.Rows(0)(0)), 0, dt.Rows(0)(0)))
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    ' ADVANCEUSED
    Private Function getAdvanceUsed(ByRef request As SpectrumCommon.DayCloseReportModel) As Double
        Try
            Dim resultAdvanceUsedString = " Select sum(NetAmount) as NetAmount From " _
                    & " (select sum(netAmount-isnull(returnamount,0))as netamount from view_salesreport " _
                    & " where convert(datetime,CONVERT(VARCHAR(10), billdate ,101)) = '" & request.ToDate.ToString("yyyy-MM-dd") & "'" _
                    & " and billno like 'SO%' " _
                    & " union all " _
                    & " select sum(netAmount-isnull(returnamount,0)) " _
                    & " as netamount from view_salesreport " _
                    & " where convert(datetime,CONVERT(VARCHAR(10), billdate ,101)) ='" & request.ToDate.ToString("yyyy-MM-dd") & "'" _
                    & " and billno like 'BL%' " + " )a "

            Dim dt = GetFilledTable(resultAdvanceUsedString)
            If dt.Rows.Count > 0 Then getAdvanceUsed = Val(IIf(IsDBNull(dt.Rows(0)(0)), 0, dt.Rows(0)(0)))
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    '== --- OTHER OUT COME -----
    Private Function getOtherOutcome(ByRef request As SpectrumCommon.DayCloseReportModel) As Double

        Try

            Dim otherOutcomeString = "SELECT sum(vendoramt) from TillCloseDtl where " _
             & "sitecode='" & request.SiteCode & "' and convert(datetime,CONVERT(VARCHAR(10), tilldate ,101)) ='" & request.ToDate.ToString("yyyy-MM-dd") & "' " _
             & " and sitecode='" & request.SiteCode & "'"

        Dim dt = GetFilledTable(otherOutcomeString)
            If dt.Rows.Count > 0 Then getOtherOutcome = Val(IIf(IsDBNull(dt.Rows(0)(0)), 0, dt.Rows(0)(0)))
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    '' ADVANCECREATED() ---
    Private Function getAdvanceCreated(ByRef request As SpectrumCommon.DayCloseReportModel) As Double
        Try

      
            Dim resultAdvanceCreatedString = " select sum(amount) from " _
                        & " ( SELECT  sum(A.AMOUNTTENDERED) as amount " _
                        & " FROM SALESINVOICE A inner join salesorderhdr b " _
                        & " on a.sitecode=b.sitecode and a.documentnumber=b.saleordernumber and b.sostatus<>'Cancel' " _
                        & " INNER join MSTTENDERTYPE C  " _
                        & " on A.Tendertypecode=C.TENDERTYPE " _
                        & " WHERE A.status=1 and convert(datetime,CONVERT(VARCHAR(10), A.soinvdate ,101)) = '" & request.ToDate.ToString("yyyy-MM-dd") & "'  AND A.SITECODE='" & request.SiteCode & "' AND " _
                        & " A.TENDERTYPECODE <> 'CLPPoint' and A.TENDERTYPECODE <> 'GiftVoucher(I)' and  " _
                        & " a.TENDERHEADCODE <> 'CreditCheque'  " _
                        & " Union ALL  " _
                        & " SELECT sum(A.AMOUNTTENDERED) as amount " _
                        & " FROM SALESINVOICE A inner join Birthlist b " _
                        & " on a.sitecode=b.sitecode and a.documentnumber=b.birthlistid and b.BirthListStatus<>'Cancel' " _
                        & " Inner join MSTTENDERTYPE C  " _
                        & " on A.Tendertypecode=C.TENDERTYPE " _
                        & " WHERE A.status=1 and convert(datetime,CONVERT(VARCHAR(10), A.soinvdate ,101)) ='" & request.ToDate.ToString("yyyy-MM-dd") & "' AND A.SITECODE='" & request.SiteCode & "'  AND " _
                        & " A.TENDERTYPECODE <> 'CLPPoint' and A.TENDERTYPECODE <> 'GiftVoucher(I)' and a.TENDERHEADCODE <> 'CreditCheque' " _
                        & " )A "

        Dim dt = GetFilledTable(resultAdvanceCreatedString)
            If dt.Rows.Count > 0 Then getAdvanceCreated = Val(IIf(IsDBNull(dt.Rows(0)(0)), 0, dt.Rows(0)(0)))
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    '==OtherIncome()

    Private Function getOtherIncome(ByRef request As SpectrumCommon.DayCloseReportModel) As DataTable

        Dim serviceArticleType = "Service"
        Try
            Dim resultListString = " Select articlename,sum(NetAmt) as NetAmt from (select sum(netamount) as NetAmt ," _
                        + " B.articlename from cashmemodtl A inner join mstArticle B on " _
                        + " a.Articlecode=b.articlecode inner join cashmemohdr c on a.siteCode=c.siteCode and " _
                        + " a.finYear=c.finYear and a.billNo=c.billNo where c.BillIntermediateStatus<>'DELETED' and " _
                        + " b.ArticalTypeCode='" & serviceArticleType & "' AND " _
                        + " convert(datetime,CONVERT(VARCHAR(10), c.BillDate ,101)) = '" & request.ToDate.ToString("yyyy-MM-dd") & "' Group by b.articlename " _
                        + " Union All select sum(isnull(A.ChargeAmount,0)+isnull(A.taxAmt,0)) as NetAmt,A.Chargename AS articlename " _
                        + " from salesorderOtherCharges A " _
                        + " where convert(datetime,CONVERT(VARCHAR(10), dayopendate ,101)) = '" & request.ToDate.ToString("yyyy-MM-dd") & "' Group By A.Chargename) " _
                        + " temp Group By articlename"

            '.setParameter("serviceArticleType", serviceArticleType).setParameter("daycloseStatus", False).setParameter("siteCode", dayOpenNcloseEntity.getId().getSiteCode())

            Dim dt = GetFilledTable(resultListString)
            Dim totalValueOfIncome As Double
            For Each result In dt.Rows
                Dim setTenderDescription = SLASH_SIGN + result(0).ToString()
                Dim value As Double = Val(IIf(IsDBNull(result(1)), 0, result(1)))
                totalValueOfIncome = totalValueOfIncome + value

            Next

            Dim dayCloseIncomeDetailsDTOlist As New DataTable
            dayCloseIncomeDetailsDTOlist.Columns.Add("Tender", System.Type.GetType("System.String"))
            dayCloseIncomeDetailsDTOlist.Columns.Add("Amount", System.Type.GetType("System.Decimal"))
            dayCloseIncomeDetailsDTOlist.Columns.Add("TenderNature", System.Type.GetType("System.String"))

            Dim dr = dayCloseIncomeDetailsDTOlist.NewRow()
            dr(0) = SLASH_SIGN & "OTHER_INCOME"
            dr(1) = totalValueOfIncome
            dr(2) = "/"
            dayCloseIncomeDetailsDTOlist.Rows.Add(dr)

            Dim resultList1 As New DataTable
            Dim resultList1String = "SELECT 'Other Income' AS TENDERTYPE, 'Ser-' + B.ArticleName AS DESCRIPTION,SUM(NetAmount)AS AMOUNTTENDERED,CONVERT(BIT,0) AS ISSUED   FROM cashmemodtl A INNER JOIN MSTArticle B ON " _
                                    + " A.ArticleCode=B.ArticleCode AND B.ArticalTypeCode='Service'  inner join Cashmemohdr C on A.sitecode=c.sitecode and A.finyear=c.finyear and a.billno=c.billno   WHERE " _
                                    + " C.BillIntermediateStatus <> 'Deleted' AND (Convert(DateTime, Convert(VARCHAR(10), A.BillDate, 101)) ='" & request.ToDate.ToString("yyyy-MM-dd") & "') " _
                                    + " AND A.SiteCode = '" & request.SiteCode & "' group " _
                                    + " by B.ArticleName union all  SELECT 'Other Income' AS TENDERTYPE,ChargeName AS DESCRIPTION,SUM(ChargeAmount + isnull(TaxAmt,0))AS AMOUNTTENDERED,CONVERT(BIT,0) AS ISSUED   FROM " _
                                    + " SalesOrderOtherCharges  WHERE(Convert(DateTime, Convert(VARCHAR(10), dayopendate, 101)) ='" & request.ToDate.ToString("yyyy-MM-dd") & "')" _
                                    + "  AND SiteCode = '" & request.SiteCode & "' group by ChargeName "

            dt = GetFilledTable(resultList1String)

            Dim setTenderType = ""
            Dim amt As Double = 0
            For Each result1 In dt.Rows
                'Dim setTenderType = result1(1) Dim setTenderDescription = result1(1)
                setTenderType = result1(1)
                amt = Val(IIf(IsDBNull(result1(2)), 0, result1(2)))
            Next
            Dim dr2 = dayCloseIncomeDetailsDTOlist.NewRow()
            dr2(0) = setTenderType
            dr2(1) = amt
            dr2(2) = MINUS_SIGN.ToString.Trim()
            dayCloseIncomeDetailsDTOlist.Rows.Add(dr2)

            Return dayCloseIncomeDetailsDTOlist
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    'Private Function GetDayCloseDataSet(ByRef request As SpectrumCommon.DayCloseReportModel) As DataTable
    '    Try
    '        Dim reportQuery As String = "select DESCRIPTION As Tender, Convert(Numeric(18,2), sum(AMOUNTTENDERED)) as Amount from  " & _
    '  "(SELECT B.TENDERTYPE,C.DESCRIPTION, A.AMOUNTTENDERED,  " & _
    '  "CONVERT(BIT,CASE WHEN A.TENDERTYPECODE='GiftVoucher(I)' THEN  " & _
    ' "1 ELSE 0 END) AS ISSUED FROM cashmemohdr D inner join CashMemoReceipt A  " & _
    '  "ON d.SiteCode=A.Sitecode and d.FinYear=A.finyear and d.BillNo= A.Billno and  " & _
    '  "d.BillIntermediateStatus <> 'Deleted' INNER JOIN MSTTENDER B  " & _
    '  "ON A.TENDERHEADCODE= B.TENDERHEADCODE AND A.TENDERTYPECODE=B.TENDERTYPE  " & _
    '  "AND B.SITECODE='" & request.SiteCode & "' INNER JOIN MSTTENDERTYPE C ON  " & _
    '  "B.TENDERTYPE=C.TENDERTYPE WHERE convert(datetime,CONVERT(VARCHAR(10), A.cmRcptDate ,101)) = '" & request.ToDate.ToString("yyyy-MM-dd") & "' " & _
    '  " AND  " & _
    '  "A.SITECODE='" & request.SiteCode & "' " & _
    '  "and A.TENDERTYPECODE <> 'GiftVoucher(I)' " & _
    '  "and a.TENDERHEADCODE <> 'CreditCheque' UNION  " & _
    '  "ALL SELECT A.Tendertypecode,C.DESCRIPTION, A.AMOUNTTENDERED,CONVERT(BIT,CASE  " & _
    '  "WHEN A.TENDERTYPECODE='CreditVouc(Iss)' THEN 1 ELSE 0 END)  " & _
    '  "AS ISSUED FROM SALESINVOICE A inner join salesorderhdr b  " & _
    '  "on a.sitecode=b.sitecode and a.documentnumber=b.saleordernumber and b.sostatus<>'Cancel'  " & _
    '  "INNER join MSTTENDERTYPE C  " & _
    '  "on A.Tendertypecode=C.TENDERTYPE " & _
    '  "WHERE convert(datetime,CONVERT(VARCHAR(10), A.soinvdate ,101)) = '" & request.ToDate.ToString("yyyy-MM-dd") & "' " & _
    '  " AND A.SITECODE='" & request.SiteCode & "'  AND  " & _
    '  "A.TENDERTYPECODE <>  " & _
    ' "'GiftVoucher(I)' and a.TENDERHEADCODE <> 'CreditCheque'  " & _
    '  "Union ALL SELECT A.Tendertypecode,C.DESCRIPTION, A.AMOUNTTENDERED,CONVERT(BIT,CASE  " & _
    '  "WHEN A.TENDERTYPECODE='CreditVouc(Iss)' THEN 1 ELSE 0 END)  " & _
    '  "AS ISSUED FROM SALESINVOICE A inner join Birthlist b  " & _
    '  "on a.sitecode=b.sitecode and a.documentnumber=b.birthlistid and b.BirthListStatus<>'Cancel'  " & _
    '  "INNER join MSTTENDERTYPE C  " & _
    '  "on A.Tendertypecode=C.TENDERTYPE  " & _
    '  "WHERE convert(datetime,CONVERT(VARCHAR(10), A.soinvdate ,101)) = '" & request.ToDate.ToString("yyyy-MM-dd") & "' " & _
    '  " AND A.SITECODE='" & request.SiteCode & "' " & _
    '  "and A.TENDERTYPECODE <> 'GiftVoucher(I)' and a.TENDERHEADCODE <> 'CreditCheque' ) temp group by TENDERTYPE,DESCRIPTION "

    '        Return GetFilledTable(reportQuery)
    '    Catch ex As Exception
    '        LogException(ex)
    '        Return Nothing
    '    End Try
    'End Function


End Class
