Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports SpectrumBL

Public Class clsReprintBill
    'CMS	0	CMSale
    'CMR	0	CMReturn
    'BLS	0	BirthList
    'SO201	0	Sales Order
    Dim daInvoice As New SqlDataAdapter
    Dim dsInvoice As New DataSet
    Dim dtInvoice As DataTable
    Dim SqlQuery As New StringBuilder


    Public Function GetAllDocumentInfo(ByVal pSiteCode As String, ByVal pFinYear As String, ByVal pDocumentType As String) As DataTable
        Try
            SqlQuery.Length = 0
            If pDocumentType = "SalesOrder" Then
                SqlQuery.Append("Select SaleOrderNumber as DocumentNo, TerminalID,CustomerNo, CustomerType, NetAmt, BalanceAmount,createdon " & vbCrLf)
                SqlQuery.Append("from SalesOrderHdr Where SiteCode='" & pSiteCode & "' And FinYear='" & pFinYear & "' and SOStatus not in('Cancel','Return') and AdvanceAmt <> 0 Order By UpdatedOn Desc" & vbCrLf)

            ElseIf pDocumentType = "BirthList" Then
                SqlQuery.Append("Select BirthListId as DocumentNo, CustomerId as CustomerNo, CustomerType, PaidAmt as NetAmt, SaleInvNumber  " & vbCrLf)
                SqlQuery.Append("from BirthListSalesHdr Where SiteCode='" & pSiteCode & "' And FinYear='" & pFinYear & "' Order By UpdatedOn Desc" & vbCrLf)

            ElseIf pDocumentType = "CashMemo" Then
                SqlQuery.Append("Select ch.BillNo as DocumentNo, ch.TerminalID, ch.BillDate, IsNull(ch.CLPNo,0) as CustomerNo, " & vbCrLf)
                SqlQuery.Append("ch.NetAmt, ch.PaymentAmt, IsNull(ch.CLPPoints,0) as CLPPoints , ch.TotalDiscount,ch.createdon " & vbCrLf)
                SqlQuery.Append("from CashMemoHdr ch inner join (select distinct BillNo, BType from CashMemoDtl) cd on ch.BillNo=cd.BillNo " & vbCrLf)
                SqlQuery.Append("Where ch.SiteCode='" & pSiteCode & "' And ch.FinYear='" & pFinYear & "' And cd.BType='S' Order By ch.UpdatedOn Desc" & vbCrLf)

            ElseIf pDocumentType = "ReturnCashMemo" Then
                SqlQuery.Append("Select ch.BillNo as DocumentNo, ch.TerminalID, ch.BillDate, IsNull(ch.CLPNo,0) as CustomerNo, " & vbCrLf)
                SqlQuery.Append("ch.NetAmt, ch.PaymentAmt, IsNull(ch.CLPPoints,0) as CLPPoints , ch.TotalDiscount, 'CLP' as CustomerType " & vbCrLf)
                SqlQuery.Append("from CashMemoHdr ch inner join (select distinct BillNo, BType from CashMemoDtl) cd on ch.BillNo=cd.BillNo " & vbCrLf)
                SqlQuery.Append("Where ch.SiteCode='" & pSiteCode & "' And ch.FinYear='" & pFinYear & "' And cd.BType='R' Order By ch.UpdatedOn Desc" & vbCrLf)
            End If

            daInvoice = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtInvoice = New DataTable
            daInvoice.Fill(dtInvoice)

            Return dtInvoice

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetAllInvoiceInfo(ByVal pSiteCode As String, ByVal pFinYear As String, ByVal pDocNo As String, ByVal pDocType As String) As DataTable
        Try
            SqlQuery.Length = 0
            If pDocType = "SalesOrder" Then
                'SqlQuery.Append("Select si.DocumentNumber, si.SaleInvNumber, si.TerminalID, si.TenderTypeCode, si.AmountTendered, si.ExchangeRate, " & vbCrLf)
                'SqlQuery.Append("si.CurrencyCode, Convert(Varchar(10),si.SOInvDate,105) as SOInvDate, si.UserName, so.CustomerNo, so.CustomerType " & vbCrLf)
                'SqlQuery.Append("from SalesInvoice si Inner join SalesOrderHdr so on si.DocumentNumber =so.SaleOrderNumber " & vbCrLf)
                'SqlQuery.Append("Where si.status=1 and si.SiteCode='" & pSiteCode & "' And si.FinYear='" & pFinYear & "' " & vbCrLf)
                'SqlQuery.Append("And si.DocumentNumber='" & pDocNo & "' " & vbCrLf)
                '' Change By ketan PC Writeoff
                SqlQuery.Append("SELECT * from ( SELECT  A.DocumentNumber as DocumentNumber, A.SaleInvNumber as SaleInvNumber, " & vbCrLf)
                SqlQuery.Append("dbo.FnGetDesc('Terminal',A.TerminalID,A.SiteCode) as TerminalID, A.TenderTypeCode as TenderTypeCode,CASE WHEN  A.TenderTypeCode = 'Credit' THEN " & vbCrLf)
                SqlQuery.Append("A.AmountTendered -Isnull (B.PaidAmount,0)ELSE A.AmountTendered END AS AmountTendered, " & vbCrLf)
                SqlQuery.Append(" A.ExchangeRate, A.CurrencyCode,Convert(Varchar(10),A.SOInvDate,105) as SOInvDate, A.UserName ,so.CustomerNo, so.CustomerType FROM	SALESINVOICE A" & vbCrLf)
                SqlQuery.Append("LEFT JOIN  (SELECT CR.RefBillInvNumber ,CR.RefBillNo ,SUM (CR.AmountTendered ) AS PaidAmount,MAX(CR.CREATEDON ) " & vbCrLf)
                SqlQuery.Append("as CREATEDON ,CR.SiteCode FROM CreditReceipt CR WHERE  CR.TYPECODE ='SO' GROUP BY SiteCode,CR.RefBillNo,CR.RefBillInvNumber ) B " & vbCrLf)
                SqlQuery.Append("ON A.DocumentNumber =B.RefBillNo AND A.SITECODE =B.SITECODE AND B.RefBillInvNumber=A.SaleInvNumber LEFT JOIN CreditSaleWriteOff WOFF ON  A.SiteCode=WOFF.SITECODE AND A.FINYEAR=WOFF.FINYEAR AND   " & vbCrLf)
                SqlQuery.Append(" A.DocumentNumber = WOFF.RefBillNo AND WOFF.RefBillInvNumber=A.SaleInvNumber AND A.TenderTypeCode = 'Credit'  " & vbCrLf)
                SqlQuery.Append("Inner join SalesOrderHdr so on A.DocumentNumber =so.SaleOrderNumber Where A.status =1 AND " & vbCrLf)
                SqlQuery.Append(" WOFF.AmountTendered IS null and A.SiteCode='" & pSiteCode & "' And A.DocumentNumber='" & pDocNo & "'  And A.FinYear='" & pFinYear & "' ) AS A WHERE A.AmountTendered<> 0" & vbCrLf)

                SqlQuery.Append(" UNION ALL SELECT	CR.RefBillNo  AS DocumentNumber, CR.BILLNO AS SaleInvNumber ,dbo.FnGetDesc" & vbCrLf)
                SqlQuery.Append("('Terminal',CR.TerminalID,CR.SiteCode)AS TerminalID ,CR.TenderTypeCode  , CR.AmountTendered,CR.ExchangeRate, CR.CurrencyCode, " & vbCrLf)
                SqlQuery.Append("Convert(Varchar(10),CR.CREATEDON,105)as SOInvDate,AU.UserName,so.CustomerNo, so.CustomerType  " & vbCrLf)
                SqlQuery.Append("FROM CreditReceipt CR LEFT JOIN AuthUsers AU On AU.USERID = CR.CREATEDBY  Inner join SalesOrderHdr so on CR.RefBillNo =so.SaleOrderNumber  " & vbCrLf)
                SqlQuery.Append("WHERE CR.status =1 and CR.SiteCode='" & pSiteCode & "' And CR.RefBillNo='" & pDocNo & "' And CR.FinYear='" & pFinYear & "'  " & vbCrLf)

                SqlQuery.Append("UNION ALL SELECT SI.DocumentNumber , WOFF.WriteOffNumber AS SaleInvNumber , dbo.FnGetDesc('Terminal',WOFF.TerminalID,WOFF.SiteCode)" & vbCrLf)
                SqlQuery.Append("as TerminalID,'Write-Off' as TenderTypeCode, WOFF.AmountTendered ,WOFF.ExchangeRate,WOFF.CurrencyCode," & vbCrLf)
                SqlQuery.Append("Convert(Varchar(10),WOFF.CREATEDON ,105)as SOInvDate,AU.UserName,so.CustomerNo, so.CustomerType FROM SALESINVOICE SI INNER JOIN " & vbCrLf)
                SqlQuery.Append("CreditSaleWriteOff WOFF ON  SI.SiteCode=WOFF.SITECODE AND SI.FINYEAR=WOFF.FINYEAR AND  SI.DocumentNumber = WOFF.RefBillNo" & vbCrLf)
                SqlQuery.Append("AND SI.TenderTypeCode = 'Credit'AND WOFF.RefBillInvNumber=SI.SaleInvNumber LEFT JOIN AUTHUSERs AU ON AU.UserId =WOFF.UpdatedBy " & vbCrLf)
                SqlQuery.Append("Inner join SalesOrderHdr so on SI.DocumentNumber =so.SaleOrderNumber " & vbCrLf)
                SqlQuery.Append("WHERE WOFF.STATUS=1 AND SI.SiteCode='" & pSiteCode & "' And SI.DocumentNumber='" & pDocNo & "' And si.FinYear='" & pFinYear & "'" & vbCrLf)



            ElseIf pDocType = "BirthList" Then
                SqlQuery.Append("Select si.DocumentNumber, si.SaleInvNumber, si.TerminalID, si.TenderTypeCode, si.AmountTendered, si.ExchangeRate, " & vbCrLf)
                SqlQuery.Append("si.CurrencyCode, Convert(Varchar(10),si.SOInvDate,105) as SOInvDate, si.UserName, bh.CustomerID as CustomerNo, bh.CustomerType " & vbCrLf)
                SqlQuery.Append("from SalesInvoice si Inner join BirthListSalesHdr bh on si.DocumentNumber =bh.BirthListId And si.SaleInvNumber=bh.SaleInvNumber " & vbCrLf)
                SqlQuery.Append("Where si.status=1 and si.SiteCode='" & pSiteCode & "' And si.FinYear='" & pFinYear & "' " & vbCrLf)
                SqlQuery.Append("And si.DocumentNumber='" & pDocNo & "' " & vbCrLf)

            ElseIf pDocType = "CashMemo" Or pDocType = "ReturnCashMemo" Then
                'SqlQuery.Append("Select ch.BillNo as DocumentNumber, ch.BillNo as SaleInvNumber, ch.TerminalID, " & vbCrLf)
                'SqlQuery.Append("cr.TenderTypeCode, ISNULL(cr.AmountTendered,0) as AmountTendered, cr.CurrencyCode, IsNull(ch.CLPNo,0) as CustomerNo, " & vbCrLf)
                'SqlQuery.Append("Convert(Varchar(10),CmRcptDate,105) as SOInvDate, cr.CREATEDBY as UserName, 'CLP' as CustomerType " & vbCrLf)
                'SqlQuery.Append("FROM CashMemoReceipt cr right outer join CashMemoHdr ch on cr.BillNo=ch.BillNo  " & vbCrLf)
                'SqlQuery.Append("Where cr.status=1 and ch.SiteCode='" & pSiteCode & "' And ch.FinYear='" & pFinYear & "' " & vbCrLf)
                'SqlQuery.Append("And ch.BillNo='" & pDocNo & "' " & vbCrLf)
                ''Changes By ketan PC WriteOff
                SqlQuery.Append("SELECT * from ( Select ch.BillNo as DocumentNumber, ch.BillNo as SaleInvNumber,dbo.FnGetDesc('Terminal',ch.TerminalID ,CH.SiteCode) " & vbCrLf)
                SqlQuery.Append("AS TerminalID, cr.TenderTypeCode,CASE WHEN  CR.TenderTypeCode = 'Credit' THEN  Isnull (CR.AmountTendered ,0)-Isnull (B.PaidAmount,0) " & vbCrLf)
                SqlQuery.Append(" ELSE CR.AmountTendered END AS AmountTendered,cr.CurrencyCode, IsNull(ch.CLPNo,0) as CustomerNo, Convert(Varchar(10),CmRcptDate,105) " & vbCrLf)
                SqlQuery.Append("as SOInvDate, cr.CREATEDBY as UserName, 'CLP' as CustomerType FROM CashMemoReceipt cr right outer join CashMemoHdr ch on cr.BillNo=ch.BillNo   " & vbCrLf)
                SqlQuery.Append("LEFT JOIN  (SELECT CR.RefBillInvNumber  ,SUM (CR.AmountTendered ) AS PaidAmount,MAX(CR.CREATEDON ) as CREATEDON ,CR.SiteCode  " & vbCrLf)
                SqlQuery.Append("FROM CreditReceipt CR WHERE  CR.TYPECODE ='CM' GROUP BY SiteCode,CR.RefBillInvNumber ) B ON CR.BillNo =B.RefBillInvNumber AND  " & vbCrLf)
                SqlQuery.Append("CR.SITECODE =B.SITECODE  LEFT JOIN CreditSaleWriteOff WOFF ON  CR.SiteCode=WOFF.SITECODE AND CR.FINYEAR=WOFF.FINYEAR  " & vbCrLf)
                SqlQuery.Append("AND  CR.BillNo = WOFF.RefBillNo  AND CR.TenderTypeCode = 'Credit' Where WOFF.AmountTendered IS null AND cr.status=1  " & vbCrLf)
                SqlQuery.Append("and ch.SiteCode='" & pSiteCode & "'  And ch.FinYear='" & pFinYear & "' And ch.BillNo='" & pDocNo & "' ) AS A WHERE A.AmountTendered<> 0" & vbCrLf)

                SqlQuery.Append(" UNION ALL SELECT	CR.RefBillNo AS DocumentNumber , CR.BILLNO AS SaleInvNumber ,dbo.FnGetDesc('Terminal',CR.TerminalID,CR.SiteCode)AS TerminalID " & vbCrLf)
                SqlQuery.Append(",CR.TenderTypeCode  , CR.AmountTendered,CR.CurrencyCode,IsNull(CMH.CLPNo,0) as CustomerNo,Convert(Varchar(10),CR.CREATEDON ,105)  " & vbCrLf)
                SqlQuery.Append(" as SOInvDate,cr.CREATEDBY as UserName,'CLP' as CustomerType FROM CreditReceipt CR INNER JOIN CASHMEMOHDR CMH ON CMH.billNo=CR.RefBillNo" & vbCrLf)
                SqlQuery.Append(" WHERE CR.status =1 and CR.SiteCode='" & pSiteCode & "' And CR.RefBillNo='" & pDocNo & "' " & vbCrLf)

                SqlQuery.Append("UNION ALL SELECT	Woff.RefBillNo AS DocumentNumber , WOFF.WriteOffNumber AS SaleInvNumber ,dbo.FnGetDesc " & vbCrLf)
                SqlQuery.Append("('Terminal',WOFF.TerminalID,WOFF.SiteCode)AS TerminalID ,'Write-Off' AS TenderTypeCode  , WOFF.AmountTendered,WOFF.CurrencyCode,IsNull(CMH.CLPNo,0)  " & vbCrLf)
                SqlQuery.Append(" as CustomerNo,Convert(Varchar(10),WOFF.CREATEDON ,105) as SOInvDate,cr.CREATEDBY as UserName," & vbCrLf)
                SqlQuery.Append("  'CLP' as CustomerType FROM CreditReceipt CR INNER JOIN CreditSaleWriteOff WOFF ON WOFF.RefBillNo=CR.RefBillNo AND WOFF.SITECODE =CR.SITECODE" & vbCrLf)
                SqlQuery.Append(" AND WOFF.FINYEAR=CR.FINYEAR INNER JOIN CASHMEMOHDR CMH ON CMH.billNo=CR.RefBillNo AND CMH.SITECODE=CR.SITECODE AND CMH.FINYEAR=CR.FINYEAR" & vbCrLf)
                SqlQuery.Append("WHERE CR.status =1 and CR.SiteCode='" & pSiteCode & "'  And CR.RefBillNo='" & pDocNo & "'  " & vbCrLf)

            End If

            daInvoice = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtInvoice = New DataTable
            daInvoice.Fill(dtInvoice)

            Return dtInvoice

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    Public Function UpdateReprintStatus(ByVal pSiteCode As String, ByVal pFinYear As String, ByVal pDocNo As String, ByVal pDocType As String, ByVal pReprintReason As String) As Boolean
        SqlQuery.Length = 0

        Try
            OpenConnection()

            If pDocType = "SalesOrder" Then
                SqlQuery.Append("Update SalesOrderHdr Set NoofReprints= IsNull(NoofReprints,0) + 1, ReprintReason='" & pReprintReason.Replace("'", "") & "', " & vbCrLf)
                SqlQuery.Append("ReprintDate='" & Format(Now, "yyyyMMdd") & "', ReprintTime='" & Now.ToString("hh:mm:ss") & "' " & vbCrLf)
                SqlQuery.Append("Where SiteCode='" & pSiteCode & "' And FinYear='" & pFinYear & "' " & vbCrLf)
                SqlQuery.Append("And SaleOrderNumber='" & pDocNo & "' " & vbCrLf)

            ElseIf pDocType = "BirthList" Then
                'SqlQuery.Append("Update CashMemoHdr Set NoofReprints= IsNull(NoofReprints,0) + 1, ReprintReason='" & pReprintReason & "', " & vbCrLf)
                'SqlQuery.Append("ReprintDate='" & Now & "', ReprintTime='" & Now.ToString("hh:mm:ss") & "' " & vbCrLf)
                'SqlQuery.Append("Where SiteCode='" & pSiteCode & "' And FinYear='" & pFinYear & "' " & vbCrLf)
                'SqlQuery.Append("And BillNo='" & pDocNo & "' " & vbCrLf)

            ElseIf pDocType = "CashMemo" Or pDocType = "ReturnCashMemo" Then
                SqlQuery.Append("Update CashMemoHdr Set NoofReprints= IsNull(NoofReprints,0) + 1, ReprintReason='" & pReprintReason.Replace("'", "") & "', " & vbCrLf)
                SqlQuery.Append("ReprintDate='" & Format(Now, "yyyyMMdd") & "', ReprintTime='" & Now.ToString("hh:mm:ss") & "' " & vbCrLf)
                SqlQuery.Append("Where SiteCode='" & pSiteCode & "' And FinYear='" & pFinYear & "' " & vbCrLf)
                SqlQuery.Append("And BillNo='" & pDocNo & "' " & vbCrLf)
            End If

            Dim CmdReprint = New SqlClient.SqlCommand(SqlQuery.ToString, SpectrumCon)
            CmdReprint.ExecuteNonQuery()

            CmdReprint.Dispose()
            CloseConnection()

            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function GetSaleItems(ByVal pSiteCode As String, ByVal pFinYear As String, ByVal pDocNo As String, ByVal pDocType As String, Optional ByVal strSalesInvNo As String = Nothing) As DataTable
        Try
            SqlQuery.Length = 0

            If pDocType = "SalesOrder" Then
                SqlQuery.Append("Select sd.SiteCode, sd.FinYear, sd.SaleOrderNumber, sd.EAN, sd.ArticleCode, am.ArticleName as Discription, sd.PromisedDeliveryDate, " & vbCrLf)
                SqlQuery.Append("sh.ActualDeliveryDate, sd.SellingPrice, sd.NetSellingPrice, sd.NetSellingPriceRoundedOff,sh.SOStatus , " & vbCrLf)
                SqlQuery.Append("sd.Quantity, sd.GrossAmount as GrossAmt, sd.NetAmount, sd.CostAmount,isnull(sd.DeliveredQty,0) as DeliveredQty , isnull(0,0) as PickUpQty, isnull(sd.ReservedQty,0) as ReservedQty , sd.ArticleStatus, " & vbCrLf)
                SqlQuery.Append("sd.OfferNo As PromotionId, sd.IsCLPApplicable as IsCLP, sd.CLPPoints, sd.CLPDiscount, sd.DiscountAmount as Discount, sd.LineDiscount, " & vbCrLf)
                SqlQuery.Append("isnull(sd.DiscountPercentage,0) as totaldiscpercentage, sd.ExclTaxAmt, isnull(sd.TotalTaxAmount,0) as  TotalTaxAmt, sd.UnitofMeasure, sd.TransactionCode, " & vbCrLf)
                SqlQuery.Append("sd.AuthUserId, sd.AuthUserRemarks, sd.ReturnSaleOrderNo, sd.ReturnSaleOrderDt, sd.ReturnQty, " & vbCrLf)
                SqlQuery.Append("sd.SalesReturnReasonCode, sd.SalesStaffID,SP.SalesPersonFullName, sd.Section, sd.Shelf, sd.ScaleItem, sd.ReturnNoSale, sd.SerialNo, '0' as isStatus, " & vbCrLf)
                SqlQuery.Append("sd.SerialNbrNotValid, sd.IFBNo, sd.AmendedNo, sd.CREATEDAT, sd.CREATEDBY, sd.CREATEDON, sd.UPDATEDAT, " & vbCrLf)
                SqlQuery.Append("sd.UPDATEDBY, sd.UPDATEDON, sd.STATUS, sd.FIRSTLEVELDISC, sd.TOPLEVELDISC, sd.FIRSTLEVEL, sd.TOPLEVEL ,am.ArticleName as ArticleDiscription ,sh.CustomerNo	,sh.CustomerType ,sh.Remarks ,BillLineNo, sh.CREATEDON AS SOCREATEDON ,sh.InvoiceCustName as InvoiceTo ,Sh.CustomerOrderRef as CustomerOrderRef  " & vbCrLf)
                SqlQuery.Append("from SalesOrderDtl sd Inner join SalesOrderHdr sh on sd.SaleOrderNumber =sh.SaleOrderNumber Inner Join MstArticle am on sd.ArticleCode=am.ArticleCode " & vbCrLf)
                SqlQuery.Append(" Left Outer Join MstSalesPerson AS SP on SP.Empcode = sd.SalesStaffID And SP.SiteCode = sd.Sitecode " & vbCrLf)
                SqlQuery.Append("Where sd.SiteCode='" & pSiteCode & "' --And sd.FinYear='" & pFinYear & "' " & vbCrLf)
                SqlQuery.Append("And sd.SaleOrderNumber='" & pDocNo & "' " & vbCrLf)

            ElseIf pDocType = "BirthList" Then
                SqlQuery.Append("Select bd.SiteCode, bd.BirthListId, bd.FinYear, bd.CustomerId, bd.SaleInvNumber, bd.EAN, " & vbCrLf)
                SqlQuery.Append("bd.ArticleCode, am.ArticleName as Discription, bd.BookedQty as PurchasedQty, bd.DeliveredQty as PickUpQty, " & vbCrLf)
                SqlQuery.Append("bd.OpenAmountQty, bd.Rate as SellingPrice, bd.CustomerType, bd.CostAmt, bd.ExclusiveTax, " & vbCrLf)
                SqlQuery.Append("bd.TaxAmount as TaxAmt, bd.DiscAmt, bd.IsCLP, bd.FreeTexts, bd.NetAmt as NetAmount, " & vbCrLf)
                SqlQuery.Append("bd.CLPPoints, bd.CLPDiscount, bd.DeliveryDate, bd.TotalDiscountAmt, bd.CREATEDAT, " & vbCrLf)
                SqlQuery.Append("bd.CREATEDBY, bd.CREATEDON, bd.UPDATEDBY, bd.UPDATEDAT, bd.UPDATEDON, bd.STATUS " & vbCrLf)

                SqlQuery.Append("from BirthListSalesDtl bd Inner Join MstArticle am on bd.ArticleCode = am.ArticleCode " & vbCrLf)
                SqlQuery.Append("Where bd.SiteCode='" & pSiteCode & "' And bd.FinYear='" & pFinYear & "' " & vbCrLf)
                SqlQuery.Append("And bd.BirthListId='" & pDocNo & "' And bd.SaleInvNumber = '" & strSalesInvNo & "' " & vbCrLf)
            End If

            daInvoice = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtInvoice = New DataTable
            daInvoice.Fill(dtInvoice)

            Return dtInvoice
        Catch ex As Exception

            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetPaymentDetails(ByVal pSiteCode As String, ByVal pFinYear As String, ByVal pDocNo As String, ByVal pInvcNo As String) As DataTable
        Try
            SqlQuery.Length = 0
            SqlQuery.Append("Select SiteCode, FinYear, DocumentNumber, SaleInvNumber, SaleInvLineNumber, " & vbCrLf)
            SqlQuery.Append("DocumentType, TerminalID, TenderTypeCode as ReceiptTypeCode, AmountTendered as Amount, " & vbCrLf)
            SqlQuery.Append("ExchangeRate, CurrencyCode, SOInvDate, SOInvTime, UserName, ManagersKeytoUpdate, " & vbCrLf)
            SqlQuery.Append("ChangeLine, (CASE WHEN REFNO_1 LIKE '%,%' THEN Replace(REFNO_1,',','.') ELSE REFNO_1 END) AS REFNO_1, RefNo_2, RefNo_3, RefNo_4, RefDate, CREATEDAT, CREATEDBY, " & vbCrLf)
            SqlQuery.Append("CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON, STATUS, TenderHeadCode as RecieptType" & vbCrLf)
            SqlQuery.Append("from SalesInvoice Where status=1 and SiteCode='" & pSiteCode & "' And FinYear='" & pFinYear & "' " & vbCrLf)
            SqlQuery.Append("And DocumentNumber='" & pDocNo & "' And SaleInvNumber='" & pInvcNo & "'" & vbCrLf)

            daInvoice = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtInvoice = New DataTable
            daInvoice.Fill(dtInvoice)

            Return dtInvoice
        Catch ex As Exception

            LogException(ex)
            Return Nothing
        End Try
    End Function
End Class
