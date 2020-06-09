Imports System.Data
Imports System.Data.SqlClient

Public Class clsOutboundDelivery
    Inherits clsPettyCashBase

    Dim objComn As New clsCommon
    Dim objCM As New clsCashMemo

    Dim SqlTrans As SqlTransaction
    Dim Sqlda, daScan As New SqlDataAdapter
    Dim Sqlcmdb As SqlCommandBuilder
    Dim Sqlcmd As SqlCommand
    Dim Sqlds, dsScan As New DataSet
    Dim Sqldr As DataRow

    Dim vStmtQry As New System.Text.StringBuilder

    Public Function GetOtherlocationSOItems(ByVal pSiteCode As String, ByVal pFinYear As String, ByVal pDocNo As String) As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select sd.SiteCode,sd.ArticleCode, am.ArticleName, sd.Quantity as OrderQty, sd.DeliveredQty, " & vbCrLf)
            vStmtQry.Append("0 as PickupQty, sd.SellingPrice As Rate, sd.NetAmount, 0.00 as PickupAmt, sd.GrossAmount, sd.EAN, sd.UnitofMeasure, sd.LineDiscount, sd.ExclTaxAmt, sd.BatchBarcode " & vbCrLf)
            vStmtQry.Append("from SalesOrderDtl sd Inner join SalesOrderHdr sh on sd.SaleOrderNumber =sh.SaleOrderNumber " & vbCrLf)
            vStmtQry.Append("Inner Join MstArticle am on sd.ArticleCode=am.ArticleCode " & vbCrLf)
            vStmtQry.Append("Where sd.DeliverySiteCode<>'" & pSiteCode & "' And sd.FinYear='" & pFinYear & "' " & vbCrLf)
            vStmtQry.Append("And sd.SaleOrderNumber='" & pDocNo & "' " & vbCrLf)
            Sqlda = New SqlDataAdapter(vStmtQry.ToString, ConString)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Dim Sqlds As New DataTable
            Sqlda.Fill(Sqlds)
            Return Sqlds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetOBItemsInfo() As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Varchar(50),'') as ArticleCode, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Discription, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as Quantity, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as SellingPrice, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as Discount, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as ExclTaxAmt, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as NetAmount, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as GrossAmt, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as PickUpQty, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as DeliveredQty, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as ReservedQty, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'True') as IsCLP , " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as totaldiscpercentage, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as TotalTaxAmt, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as IsStatus, " & vbCrLf)
            vStmtQry.Append(" Convert(int,0) as BillLineNo, " & vbCrLf)
            '----- Column Added By Mahesh for Print ----
            vStmtQry.Append(" Convert(Varchar(50),'') as ArticleDiscription, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as SalesPersonFullName, " & vbCrLf)
            vStmtQry.Append(" Convert(int,0) as rowindex " & vbCrLf)

            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtScan As New DataTable
            daScan.Fill(dtScan)

            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    Public Function GetAllOrderInfo(ByVal pSiteCode As String, ByVal pFinYear As String, ByVal pDocNo As String, ByVal pDocType As String, Optional ByVal IsOtherLocationDelivery As Boolean = False) As DataSet
        Try
            vStmtQry.Length = 0
            If pDocType = "SalesOrder" Then
                'Change by Sameer for Issue 6915 4/4/13
                vStmtQry.Append("Select sd.SiteCode,sd.EAN,sd.ArticleCode, am.ArticleName, sd.Quantity as OrderQty, sd.DeliveredQty, " & vbCrLf)
                vStmtQry.Append("0.000 as PickupQty, sd.SellingPrice As Rate, sd.NetAmount, 0.00 as PickupAmt, sd.GrossAmount, sd.EAN, sd.UnitofMeasure, sd.LineDiscount, sd.ExclTaxAmt,sd.TotalTaxAmount,sd.DiscountPercentage , sd.BatchBarcode " & vbCrLf)
                vStmtQry.Append(" ,SP.SalesPersonFullName,am.ArticleName as ArticleDiscription,BillLineNo AS rowindex  ")
                vStmtQry.Append("from SalesOrderDtl sd Inner join SalesOrderHdr sh on sd.SaleOrderNumber =sh.SaleOrderNumber " & vbCrLf)
                vStmtQry.Append("Inner Join MstArticle am on sd.ArticleCode=am.ArticleCode " & vbCrLf)
                vStmtQry.Append(" Left Outer Join MstSalesPerson AS SP on SP.Empcode = sd.SalesStaffID  " & vbCrLf)
                If IsOtherLocationDelivery Then
                    vStmtQry.Append("Where sd.DeliverySiteCode='" & pSiteCode & "' And sd.FinYear='" & pFinYear & "' " & vbCrLf)
                Else
                    vStmtQry.Append("Where (sd.SiteCode='" & pSiteCode & "' OR sd.DeliverySiteCode='" & pSiteCode & "') And sd.FinYear='" & pFinYear & "' " & vbCrLf)
                End If               
                vStmtQry.Append("And sd.SaleOrderNumber='" & pDocNo & "' And sh.SOStatus='Open' ; " & vbCrLf)

                vStmtQry.Append("Select si.DocumentNumber, si.SaleInvNumber, si.TerminalID, si.TenderTypeCode, si.AmountTendered, si.ExchangeRate, " & vbCrLf)
                vStmtQry.Append("si.CurrencyCode, Convert(Varchar(10),si.SOInvDate,105) as SOInvDate, si.UserName, so.CustomerNo, so.CustomerType " & vbCrLf)
                vStmtQry.Append("from SalesInvoice si right join SalesOrderHdr so on si.DocumentNumber =so.SaleOrderNumber " & vbCrLf)
                'vStmtQry.Append("Where si.SiteCode='" & pSiteCode & "' And si.FinYear='" & pFinYear & "' " & vbCrLf)
                'Removed sitecode from query to get payment info done at other site - Gaurav Danani
                vStmtQry.Append("Where ISNULL(si.status ,1)  =1 and so.FinYear='" & pFinYear & "' " & vbCrLf)
                'vStmtQry.Append("Where  so.FinYear='" & pFinYear & "' " & vbCrLf)
                vStmtQry.Append("And so.SaleOrderNumber='" & pDocNo & "' And so.SOStatus='Open' ; " & vbCrLf & vbCrLf)

                vStmtQry.Append("select * " & vbCrLf)
                vStmtQry.Append(" from SalesOrderOtherCharges as OtherCharges " & vbCrLf)
                vStmtQry.Append(" Where OtherCharges.FinYear='" & pFinYear & "' " & vbCrLf)
                vStmtQry.Append(" And OtherCharges.SaleOrderNumber='" & pDocNo & "'" & vbCrLf)
                vStmtQry.Append(" AND OtherCharges.SiteCode='" & pSiteCode & "'")
            ElseIf pDocType = "BirthList" Then
                vStmtQry.Append("Select blr.ArticleCode, am.ArticleName, blr.RequstedQty as OrderQty, blr.BookedQty, blr.SellingPrice As Rate, " & vbCrLf)
                vStmtQry.Append("(blr.SellingPrice*RequstedQty) As NetAmount,  0.00 as PurchageQty, 0.00 as PurchageAmount, " & vbCrLf)
                vStmtQry.Append("blr.DeliveredQty, 0.000 as PickupQty, blr.EAN from BirthListRequestedItems blr " & vbCrLf)
                vStmtQry.Append("Inner Join MstArticle am on blr.ArticleCode=am.ArticleCode " & vbCrLf)
                vStmtQry.Append("Where blr.SiteCode='" & pSiteCode & "' And blr.FinYear='" & pFinYear & "' " & vbCrLf)
                vStmtQry.Append("And blr.BirthListId='" & pDocNo & "' And blr.BookedQty>0 ;" & vbCrLf)

                vStmtQry.Append("SELECT * FROM SalesInvoice WHERE status =1 and DocumentNumber = '" & pDocNo & "';" & vbCrLf)

                vStmtQry.Append("Select * from BirthList Where SiteCode='" & pSiteCode & "' And FinYear='" & pFinYear & "' " & vbCrLf)
                vStmtQry.Append("And BirthListId='" & pDocNo & "' ;" & vbCrLf)
            End If

            Sqlda = New SqlDataAdapter(vStmtQry.ToString, ConString)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Sqlds.Tables(0).TableName = "OrderDtls"
            Sqlds.Tables(1).TableName = "SalesInvoice"

            If pDocType = "BirthList" Then
                Sqlds.Tables(2).TableName = "BirthList"
            ElseIf pDocType = "SalesOrder" Then
                Sqlds.Tables(2).TableName = "OtherCharges"
            End If

            Return Sqlds

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    Public Function GetAllOrderInfoAssigned(ByVal pSiteCode As String, ByVal pFinYear As String, ByVal pDocNo As String, ByVal pDocType As String) As DataSet
        Try
            Dim query As String = "Select si.DocumentNumber, si.SaleInvNumber, si.TerminalID, si.TenderTypeCode, si.AmountTendered, si.ExchangeRate, " & _
                                    "si.CurrencyCode, Convert(Varchar(10),si.SOInvDate,105) as SOInvDate, si.UserName, so.CustomerNo, so.CustomerType  " & _
                                    "from SalesInvoice si Inner join SalesOrderHdr so on si.DocumentNumber =so.SaleOrderNumber inner join " & _
                                    "dbo.SalesOrderDeliveryLocInfo sod on sod.SiteCode  = so.SiteCode  and sod.SalesOrderNumber = so.SaleOrderNumber " & _
                                    "Where si.status =1 and sod.DeliverySiteCode ='" & pSiteCode & "' And si.FinYear='" & pFinYear & "' " & _
                                    "And si.DocumentNumber='" & pDocNo & "' And so.SOStatus='Open'  " & vbCrLf

            query = query & "Select sd.SiteCode, sd.ArticleCode, am.ArticleName, sod.Quantity as OrderQty, sod.DeliveredQty, sod.Quantity As QuantityToDeliver, " & _
                            "(sod.Quantity-sod.DeliveredQty) as PickupQty, sd.SellingPrice As Rate, sod.Amount As AmountToCollect, sd.NetAmount, 0.00 as PickupAmt, sd.GrossAmount, sd.EAN, sd.UnitofMeasure, sd.LineDiscount, sd.ExclTaxAmt " & _
                            "from SalesOrderDtl sd inner join SalesOrderDeliveryLocInfo sod " & _
                            " on sod.SalesOrderNumber = sd.SaleOrderNumber and sod.ArticleCode = sd.ArticleCode " & _
                            "Inner join SalesOrderHdr sh on sd.SaleOrderNumber =sh.SaleOrderNumber " & _
                            "Inner Join MstArticle am on sd.ArticleCode=am.ArticleCode " & _
                            "Where sod.DeliverySiteCode = '" & pSiteCode & "' And sd.FinYear='" & pFinYear & "' " & _
                            "And sd.SaleOrderNumber='" & pDocNo & "' And sh.SOStatus='Open'"

            Sqlda = New SqlDataAdapter(query, ConString)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)
            Return Sqlds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetStructOutboundDelivery(ByVal vSiteCode As String, ByVal vFinYear As String, ByVal vSalesNo As String, ByVal vDocType As String, Optional ByVal createdAt As String = Nothing) As DataSet
        Try
            vStmtQry.Length = 0

            vStmtQry.Append("Select * From OrderHdr Where SiteCode='" & vSiteCode & "' And FinYear='" & vSalesNo & "' " & vbCrLf)
            vStmtQry.Append("And DocumentNumber='0'; " & vbCrLf & vbCrLf)
            vStmtQry.Append("Select * From OrderDtl Where SiteCode='" & vSiteCode & "' And FinYear='" & vSalesNo & "' " & vbCrLf)
            vStmtQry.Append("And DocumentNumber='0'; " & vbCrLf & vbCrLf)

            If vDocType = "SalesOrder" Then
                If createdAt IsNot Nothing Then
                    vStmtQry.Append("Select * From SalesOrderHdr Where SiteCode='" & createdAt & "' And FinYear='" & vFinYear & "' " & vbCrLf)
                Else
                    vStmtQry.Append("Select * From SalesOrderHdr Where SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "' " & vbCrLf)
                End If

                vStmtQry.Append("And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf & vbCrLf)
                If createdAt IsNot Nothing Then
                    vStmtQry.Append("Select * From SalesOrderDtl Where SiteCode='" & createdAt & "' And FinYear='" & vFinYear & "' " & vbCrLf)
                Else
                    vStmtQry.Append("Select * From SalesOrderDtl Where SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "' " & vbCrLf)
                End If
                vStmtQry.Append("And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf & vbCrLf)

                'vStmtQry.Append(" Select * From SalesInvoice Where SiteCode='" & vSiteCode & "' And DocumentNumber='0' " & vbCrLf)
                vStmtQry.Append(" Select * From SalesInvoice Where status =1 and SiteCode='" & vSiteCode & "' And DocumentNumber='0' " & vbCrLf & "; " & vbCrLf & vbCrLf)

                vStmtQry.Append(" Select * From SalesOrderOtherCharges Where SiteCode='" & vSiteCode & "' And SaleOrderNumber= '" & vSalesNo & "'; " & vbCrLf & vbCrLf)

            ElseIf vDocType = "BirthList" Then
                vStmtQry.Append("Select * From BirthList Where SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "' " & vbCrLf)
                vStmtQry.Append("And BirthListId='" & vSalesNo & "'; " & vbCrLf & vbCrLf)

                vStmtQry.Append("Select * From BirthListRequestedItems Where SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "' " & vbCrLf)
                vStmtQry.Append("And BirthListId='" & vSalesNo & "'; " & vbCrLf & vbCrLf)

                vStmtQry.Append("Select * From BirthListSalesHdr Where SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "' " & vbCrLf)
                vStmtQry.Append("And BirthListId='" & vSalesNo & "'; " & vbCrLf & vbCrLf)

                vStmtQry.Append("Select * From BirthListSalesDtl Where SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "' " & vbCrLf)
                vStmtQry.Append("And BirthListId='" & vSalesNo & "'; " & vbCrLf & vbCrLf)

                vStmtQry.Append("Select * From SalesInvoice Where status =1 and SiteCode='" & vSiteCode & "' And DocumentNumber='0' " & vbCrLf)
            End If

            'vStmtQry.Append("select * from SalesOrderDeliveryLocInfo Where " & vbCrLf)
            'vStmtQry.Append(" SalesOrderNumber='" & vSalesNo & "' And DeliverySiteCode='" & vSiteCode & "' ; " & vbCrLf & vbCrLf)

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Sqlds.Tables(0).TableName = "OrderHdr"
            Sqlds.Tables(1).TableName = "OrderDtl"

            If vDocType = "SalesOrder" Then
                Sqlds.Tables(2).TableName = "SalesOrderHdr"
                Sqlds.Tables(3).TableName = "SalesOrderDtl"
                Sqlds.Tables(4).TableName = "SalesInvoice"
                Sqlds.Tables(5).TableName = "SalesOrderOtherCharges"
            ElseIf vDocType = "BirthList" Then

                Sqlds.Tables(2).TableName = "BirthList"
                Sqlds.Tables(3).TableName = "BirthListRequestedItems"
                Sqlds.Tables(4).TableName = "BirthListSalesHdr"
                Sqlds.Tables(5).TableName = "BirthListSalesDtl"
                Sqlds.Tables(6).TableName = "SalesInvoice"
            End If

            Return Sqlds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function PrepareSaveOutboundData(ByRef dsODMain As DataSet, ByVal vSiteCode As String, ByVal FinYear As String, ByVal vSalesNo As String, ByVal vOutbNo As String, ByVal vLocation As String) As Boolean
        Dim SqlTranOD As SqlTransaction = Nothing
        Try
            OpenConnection()
            SqlTranOD = SpectrumCon.BeginTransaction()

            If SaveOutboundData(dsODMain, SpectrumCon, SqlTranOD, vSiteCode, vSalesNo, vOutbNo) = True Then
                If objComn.UpdateDocumentNo("OutBound", SpectrumCon, SqlTranOD) = False Then
                    SqlTranOD.Rollback()
                    CloseConnection()
                    Return False
                End If
                If objComn.UpdateDocumentNo("CM", SpectrumCon, SqlTranOD) = False Then
                    SqlTranOD.Rollback()
                    CloseConnection()
                    Return False
                End If
                For Each dr As DataRow In dsODMain.Tables(1).Select("DeliveredQty>0")
                    If objComn.UpdateStock(dr("SiteCode").ToString, dr("ArticleCode").ToString(), dr("EAN").ToString(), dr("UnitofMeasure").ToString(), dr("DeliveredQty").ToString(), dr("CreatedAt"), SpectrumCon, SqlTranOD, vLocation, 0) = False Then
                        SqlTranOD.Rollback()
                        CloseConnection()
                        Return False
                    End If

                   
                Next
                'Added by Sameer for Issue 6915 4/4/13
                For Each drsalesdtl As DataRow In dsODMain.Tables(1).Select("DeliveredQty>0")
                    If drsalesdtl.RowState <> DataRowState.Deleted Then
                        If IsDBNull(drsalesdtl("Barcode")) = False AndAlso String.IsNullOrEmpty(drsalesdtl("Barcode")) = False Then
                            If objComn.UpdateBatchDtlQtyAllocated(drsalesdtl("SiteCode").ToString, drsalesdtl("Barcode").ToString(), drsalesdtl("DeliveredQty").ToString(), SqlTranOD) = False Then
                                SqlTranOD.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        End If
                    End If
                Next
            Else
                SqlTranOD.Rollback()
                CloseConnection()
                Return False
            End If
            SqlTranOD.Commit()
            CloseConnection()
            Return True

        Catch ex As Exception
            SqlTranOD.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function SaveOutboundData(ByRef dsSO As DataSet, ByRef Sqlconn As SqlConnection, ByRef SqlTran As SqlTransaction, ByVal vSiteCode As String, ByVal vSalesNo As String, ByVal vOutbNo As String) As Boolean
        Try
            For TbCnt = 0 To dsSO.Tables.Count - 1

                Dim tableName As String = dsSO.Tables(TbCnt).TableName
                Dim tableColumns As String = ""

                For ColIndex = 0 To dsSO.Tables(TbCnt).Columns.Count - 1
                    tableColumns = tableColumns & ", " & dsSO.Tables(TbCnt).Columns(ColIndex).ColumnName.ToString()
                Next
                tableColumns = tableColumns.Substring(1)

                vStmtQry.Length = 0
                If tableName = "SalesOrderHdr" Or tableName = "SalesOrderDtl" Then
                    vStmtQry.Append("SELECT " + tableColumns & " FROM " & tableName)
                    vStmtQry.Append(" Where SiteCode = '" & vSiteCode & "' and SaleOrderNumber  = '" & vSalesNo & "' " & vbCrLf)
                ElseIf tableName = "OrderHdr" Or tableName = "OrderDtl" Then
                    vStmtQry.Append("SELECT " + tableColumns & " FROM " & tableName)
                    vStmtQry.Append(" Where SiteCode = '" & vSiteCode & "' and DocumentNumber  = '" & vOutbNo & "' " & vbCrLf)
                ElseIf tableName = "SalesInvoice" Then
                    vStmtQry.Append("SELECT " + tableColumns & " FROM " & tableName)
                    vStmtQry.Append(" Where SiteCode = '" & vSiteCode & "' and SaleInvNumber  = '" & vOutbNo & "' " & vbCrLf)
                Else 'If tableName = "BirthListRequestedItems" Or tableName = "BirthListSalesHdr" Then
                    vStmtQry.Append("SELECT " + tableColumns & " FROM " & tableName)
                    vStmtQry.Append(" Where SiteCode = '" & vSiteCode & "' and BirthListId  = '" & vOutbNo & "' " & vbCrLf)
                End If

                Sqlda = New SqlDataAdapter(vStmtQry.ToString, Sqlconn)
                Sqlda.SelectCommand.Transaction = SqlTran

                Sqlcmdb = New SqlCommandBuilder(Sqlda)
                Sqlda.TableMappings.Add(tableName, tableName)
                Sqlda = Sqlcmdb.DataAdapter
                Sqlda.Update(dsSO, tableName)
            Next
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function PrepareSaveData(ByVal currentSalesinvoice As String, ByVal ClpProgramId As String, ByVal CLPCustomerId As String, ByRef dsSOMain As DataSet, ByVal vSiteCode As String, ByVal vSalesNo As String, ByVal CVProgram As String, ByVal DocType As String, ByVal FinYear As String, ByVal UserId As String, ByVal ServerDate As DateTime, Optional ByRef Voucherno As String = "", Optional ByRef VoucherDays As Int32 = 0) As Boolean
        Try
            Dim objComm As New clsCommon
            Dim disc As Double

            Dim dtTempReserved As DataSet = dsSOMain.Copy()
            OpenConnection()
            SqlTrans = SpectrumCon.BeginTransaction()
            If objComm.SaveData(dtTempReserved, SpectrumCon, SqlTrans) = True Then
                Dim strCurrSaleInvNbr As String = ""
                If dtTempReserved.Tables("SalesInvoice").Rows.Count > 0 Then
                    strCurrSaleInvNbr = currentSalesinvoice 'dtTempReserved.Tables("SalesInvoice").Compute("MAX(SALEINVNUMBER)", "")
                End If
                Dim dvCreditVoucher As New DataView(dtTempReserved.Tables("SalesInvoice"), " SALEINVNUMBER='" & strCurrSaleInvNbr & "' AND TenderTypeCode Like 'CreditVouc%'", "", DataViewRowState.CurrentRows)
                Dim RedimCVExpDay As Integer = 0
                If dvCreditVoucher.Count > 0 Then
                    For Each drView As DataRowView In dvCreditVoucher
                        If drView("TenderTypeCode") = "CreditVouc(I)" Then
                            ' this is to get the old expiry date of partial redeem CV
                            'If dvCreditVoucher.Count > 1 Then
                            If Not IsDBNull(drView("RefDate")) Then
                                VoucherDays = DateDiff(DateInterval.Day, ServerDate.Date, CDate(drView("RefDate")).Date)
                                VoucherDays = VoucherDays
                            End If
                            'End If
                            ' this is to get the old expiry date of partial redeem CV

                            If objCM.UpdateCreditVoucher(CVProgram, DocType, True, drView("DocumentNumber").ToString(), vSiteCode, drView("SOInvDate"), UserId, SqlTrans, SpectrumCon, drView("AmountTendered"), Voucherno, VoucherDays) = False Then
                                SqlTrans.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        ElseIf drView("TenderTypeCode") = "CreditVouc(R)" Then
                            If objCM.UpdateCreditVoucher(CVProgram, DocType, False, drView("DocumentNumber").ToString(), vSiteCode, drView("SOInvDate"), UserId, SqlTrans, SpectrumCon, 0, drView("RefNO_2").ToString()) = False Then
                                SqlTrans.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        End If
                    Next
                End If

                ' Add for insert for GV by ram  in ver 1.0.9.2 
                Dim dvGiftVoucher As New DataView(dtTempReserved.Tables("SalesInvoice"), " SALEINVNUMBER='" & strCurrSaleInvNbr & "' AND TenderTypeCode Like 'GiftVouch%'", "", DataViewRowState.CurrentRows)
                Dim RedimGVExpDay As Integer = 0
                If dvGiftVoucher.Count > 0 Then
                    For Each drView As DataRowView In dvGiftVoucher
                        If drView("TenderTypeCode") = "GiftVoucher(I)" Then
                            ' this is to get the old expiry date of partial redeem GV
                            'If dvGiftVoucher.Count > 1 Then
                            If Not IsDBNull(drView("RefDate")) Then
                                VoucherDays = DateDiff(DateInterval.Day, ServerDate.Date, CDate(drView("RefDate")).Date)
                                VoucherDays = VoucherDays
                            End If
                            'End If
                            ' this is to get the old expiry date of partial redeem GV

                            If objCM.UpdateCreditVoucher(drView("RefNo_2"), DocType, True, drView("DocumentNumber").ToString(), vSiteCode, drView("SOInvDate"), UserId, SqlTrans, SpectrumCon, drView("AmountTendered"), Voucherno, VoucherDays, "GV") = False Then
                                SqlTrans.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        ElseIf drView("TenderTypeCode") = "GiftVoucher(R)" Then
                            If objCM.UpdateCreditVoucher(CVProgram, DocType, False, drView("DocumentNumber").ToString(), vSiteCode, drView("SOInvDate"), UserId, SqlTrans, SpectrumCon, 0, drView("RefNO_2").ToString(), 0, "GV") = False Then
                                SqlTrans.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        End If
                    Next
                End If

                ' Add for insert for GV by ram  in ver 1.0.9.2 
                Dim dvCLP As New DataView(dtTempReserved.Tables("SalesInvoice"), "TenderTypeCode Like 'CLP%'", "", DataViewRowState.CurrentRows)
                If dvCLP.Count > 0 Then
                    For Each CLpRow As DataRowView In dvCreditVoucher
                        Dim TotalPoints As Integer = CLpRow("AmountTendered")
                        'CLPRedemptionPoints = TotalPoints
                        'TotalPoints = TotalPoints * -1
                        If objComm.UpdateClpPoints(False, ClpProgramId, CLPCustomerId, TotalPoints, SpectrumCon, SqlTrans, vSiteCode, UserId, vSalesNo, ServerDate, False) = False Then
                            SqlTrans.Rollback()
                            CloseConnection()
                            Return False
                        End If
                    Next
                End If
                If objComn.UpdateDocumentNo("CM", SpectrumCon, SqlTrans) = False Then
                    SqlTrans.Rollback()
                    CloseConnection()
                    Return False
                End If
                SqlTrans.Commit()
                CloseConnection()

                dsSOMain.Clear()
                SqlTrans.Dispose()
                Return True
            Else
                SqlTrans.Rollback()
                CloseConnection()
                Return False
            End If

        Catch ex As Exception
            SqlTrans.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function GetAmountToPay(ByVal vSiteCode As String, ByVal FinYear As String, ByVal vSalesNo As String, ByVal isDeliveryFromOtherSite As Boolean) As Decimal
        Try
            Dim remainingAmtToPay As Decimal
            Dim query As String = "select SUM(AmountTendered) As AmountTendered from SalesInvoice where status =1 and SiteCode = '" & vSiteCode & _
                                     "' and FinYear = '" & FinYear & "' and DocumentNumber = '" & vSalesNo & "'"
            Dim dtAmtCollected As DataTable = GetFilledTable(query)
            Dim totalPaidAmt As Decimal
            If dtAmtCollected IsNot Nothing AndAlso dtAmtCollected.Rows.Count > 0 Then
                totalPaidAmt = dtAmtCollected.Rows(0)("AmountTendered")
            End If
            If isDeliveryFromOtherSite Then
                query = "select Quantity ,DeliveredQty,Amount   from SalesOrderDeliveryLocInfo where " & _
                        "SalesOrderNumber = '" & vSalesNo & "'  and DeliverySiteCode = '" & vSiteCode & "'"
                Dim dt As DataTable = GetFilledTable(query)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim totalSalesAmt As Decimal = 0
                    For Each item In dt.Rows
                        totalSalesAmt += (item("Amount") / item("Quantity")) * item("DeliveredQty")
                    Next
                    remainingAmtToPay = totalSalesAmt - totalPaidAmt
                End If
            Else
                query = "select NetAmount,Quantity,DeliveredQty  from SalesOrderDtl where SiteCode = '" & vSiteCode & "' " & _
                        "and SaleOrderNumber = '" & vSalesNo & "' and FinYear = '" & FinYear & "'"
                Dim dt As DataTable = GetFilledTable(query)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim totalSalesAmt As Decimal = 0
                    For Each item In dt.Rows
                        totalSalesAmt += (item("NetAmount") / item("Quantity")) * item("DeliveredQty")
                    Next
                    remainingAmtToPay = totalSalesAmt - totalPaidAmt
                End If
            End If
            If remainingAmtToPay < 0 Then
                remainingAmtToPay = 0
            End If
            Return Math.Round(remainingAmtToPay, 2)
        Catch ex As Exception
            LogException(ex)
            Return 0
        End Try
    End Function
End Class
