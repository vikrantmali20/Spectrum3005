Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports SpectrumBL

Public Class clsChangeTender
    Dim daTender As New SqlDataAdapter
    Dim cmdbTender As New SqlCommandBuilder
    Dim cmdTender As New SqlCommand
    Dim dsTender As New DataSet
    Dim dtTender As DataTable
    Dim SqlQuery As New StringBuilder
    Dim SqlTrans As SqlTransaction = Nothing
    Dim clsComn As New clsCommon

    Dim _ShiftmanagementForSO As Boolean = False
    Public Property ShiftManagementForSO() As Boolean
        Get
            Return _ShiftmanagementForSO
        End Get
        Set(ByVal value As Boolean)
            _ShiftmanagementForSO = value
        End Set
    End Property

    Public Function GetDocumentInfo(ByVal pSiteCode As String, ByVal pFinYear As String, ByVal pDocumentType As String, ByVal DayOpenDate As Date, ByVal pTerminal As String, Optional ByVal cashiername As String = Nothing, Optional ByVal shiftid As String = Nothing) As DataTable
        Try
            'SqlQuery.Append("" & vbCrLf)
            SqlQuery.Length = 0
            If pDocumentType = "SalesOrder" Then
                SqlQuery.Append("Select Distinct sHdr.SaleOrderNumber as SalesNo, sHdr.CreatedBy as CashierName, " & vbCrLf)
                SqlQuery.Append("Case When sHdr.CustomerType='CLP' Then clpInfo.SurName +' '+ clpInfo.FirstName " & vbCrLf)
                SqlQuery.Append("When sHdr.CustomerType='SO' Then cInfo.CustomerName End as CustomerName, " & vbCrLf)
                SqlQuery.Append("sHdr.CustomerNo,Convert(Varchar(25), sHdr.CreatedOn,105) as SalesDate, " & vbCrLf)
                SqlQuery.Append("Convert(Varchar(25), sHdr.PromisedDeliveryDate,105) as DeliveryDate, " & vbCrLf)
                SqlQuery.Append("sHdr.NetAmt AS TotalAmount, sHdr.AdvanceAmt, sHdr.BalanceAmount " & vbCrLf)
                SqlQuery.Append("from SalesOrderHDR sHdr Left Join CustomerSaleOrder cInfo on sHdr.CustomerNo=cInfo.CustomerNo " & vbCrLf)
                SqlQuery.Append("Left Join CLPCustomers clpInfo on sHdr.CustomerNo=clpInfo.CardNo " & vbCrLf)
                SqlQuery.Append(" Inner Join SalesInvoice on sHdr.SaleOrderNumber = SalesInvoice.DocumentNumber " & vbCrLf)
                '------issue no 0013521 shiftwise change tender
                If ShiftManagementForSO Then
                    SqlQuery.Append(" left outer join ShiftOpenClose as  shift on shift.TerminalId =SalesInvoice.TerminalID  " & vbCrLf)
                End If
                'SqlQuery.Append("Where SalesInvoice.status =1 and sHdr.SiteCode='" & pSiteCode & "' And sHdr.FinYear='" & pFinYear & "' " & vbCrLf)
                SqlQuery.Append("Where SalesInvoice.status =1 and sHdr.SiteCode='" & pSiteCode & "' And sHdr.FinYear='" & pFinYear & "' And SalesInvoice.TerminalID='" & pTerminal & "' " & vbCrLf)
                SqlQuery.Append("And sHdr.SOStatus In ('Closed','Open') " & vbCrLf)
                '  SqlQuery.Append("And sHdr.AdvanceAmt <> '0'" & vbCrLf)
                SqlQuery.Append("And Convert(varchar(10),SalesInvoice.SOInvDate,112)='" & DayOpenDate.ToString("yyyyMMdd") & "'" & vbCrLf) '20091102
                '------issue no 0013521 shiftwise change tender
                If ShiftManagementForSO Then
                    SqlQuery.Append("And shift.ShiftId='" & shiftid & "'" & vbCrLf)
                    SqlQuery.Append("And SalesInvoice.CREATEDON >= shift.CREATEDON " & vbCrLf)
                    SqlQuery.Append("And SalesInvoice.CreatedBy='" & cashiername & "'" & vbCrLf)
                    SqlQuery.Append("And Shift.OpenDate= @DayOpenDate" & vbCrLf)
                    SqlQuery.Append("And Shift.OpenCloseStatus= 'Open'" & vbCrLf)
                End If
                SqlQuery.Append("Order By SalesNo Desc " & vbCrLf)

            ElseIf pDocumentType = "BirthList" Then
                SqlQuery.Append("Select Distinct(bHdr.BirthListId) as BirthListNo, bHdr.CreatedBy as CashierName, " & vbCrLf)
                SqlQuery.Append("Case When bHdr.CustomerType='CLP' Then clpc.SurName +' '+ clpc.FirstName " & vbCrLf)
                SqlQuery.Append("When bHdr.CustomerType='SO' Then SOc.CustomerName End as CutomerName, " & vbCrLf)
                SqlQuery.Append("bHdr.CustomerId as CustomerNo, bHdr.CustomerType, bHdr.PaidAmt as TotalAmount, " & vbCrLf)
                SqlQuery.Append("Convert(varchar(10),bHdr.CreatedOn,105) As SalesDate, " & vbCrLf)
                SqlQuery.Append("Convert(varchar(10),bDtl.DeliveryDate ,105) As DeliveryDate " & vbCrLf)
                SqlQuery.Append("from BirthListSalesHdr bHdr " & vbCrLf)
                SqlQuery.Append("Left Join CustomerSaleOrder SOc on bHdr.CustomerId=SOc.CustomerNo " & vbCrLf)
                SqlQuery.Append("Left Join BirthListSalesDtl bDtl on bHdr.BirthListId=bDtl.BirthListId " & vbCrLf)
                SqlQuery.Append("Left Join CLPCustomers clpc on bHdr.CustomerId=clpc.CardNo " & vbCrLf)
                SqlQuery.Append("Where bHdr.SiteCode='" & pSiteCode & "' And bHdr.FinYear='" & pFinYear & "' " & vbCrLf)
                SqlQuery.Append("And Convert(varchar(10),bHdr.CreatedOn,112)='" & DayOpenDate & "'" & vbCrLf)
                SqlQuery.Append("Order By bHdr.BirthListId Desc " & vbCrLf)
            End If


            Dim dtTender As New DataTable
            Dim cmd As New SqlCommand(SqlQuery.ToString, SpectrumCon())
            cmd.Parameters.Add("@DayOpenDate", SqlDbType.Date)
            cmd.Parameters("@DayOpenDate").Value = DayOpenDate
            daTender = New SqlDataAdapter(cmd)
            daTender.Fill(dtTender)
            Return dtTender
            'daTender = New SqlDataAdapter(SqlQuery.ToString, ConString)
            'dtTender = New DataTable
            'daTender.Fill(dtTender)

            'Return dtTender

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetInvoiceInfo(ByVal pSiteCode As String, ByVal pFinYear As String, ByVal pDocNo As String, ByVal pDocType As String, ByVal pCurrentDate As String) As DataSet
        Try
            'SqlQuery.Append("" & vbCrLf)
            SqlQuery.Length = 0
            If pDocType = "SalesOrder" Then
                SqlQuery.Append("Select si.SaleInvNumber As InvoiceNo, si.DocumentNumber As DocNo, " & vbCrLf)
                SqlQuery.Append("Convert(Varchar(10),si.CreatedOn,105) As CreatedOn,si.UpdatedOn, si.CreatedBy, " & vbCrLf)
                SqlQuery.Append("so.UpdatedBy As LastUpdatedBy, so.NetAmt as TotalPayableAmt, " & vbCrLf)
                SqlQuery.Append("so.AdvanceAmt As TotalAmtReceived, so.BalanceAmount, " & vbCrLf)
                SqlQuery.Append("Sum(si.AmountTendered) As AmtReceivedToday, so.SOStatus As Status, " & vbCrLf)
                SqlQuery.Append("Convert(Varchar(10),so.UpdatedOn ,105) As UpdatedOn, so.CustomerNo " & vbCrLf)
                SqlQuery.Append("from SalesInvoice si " & vbCrLf)
                SqlQuery.Append("Inner join SalesOrderHdr so on si.DocumentNumber =so.SaleOrderNumber " & vbCrLf)
                SqlQuery.Append("And si.SiteCode=so.SiteCode And si.FinYear=so.FinYear" & vbCrLf)
                SqlQuery.Append("Where si.status =1 and si.SiteCode='" & pSiteCode & "' And si.FinYear='" & pFinYear & "' " & vbCrLf)
                SqlQuery.Append("And si.DocumentNumber='" & pDocNo & "' And so.SOStatus <> 'Cancel' " & vbCrLf)
                SqlQuery.Append("And Convert(varchar(10),si.SOInvDate,112)='" & pCurrentDate & "' " & vbCrLf)
                SqlQuery.Append("Group By si.CreatedOn,si.UpdatedOn, si.SaleInvNumber, si.DocumentNumber, si.CreatedBy, " & vbCrLf)
                SqlQuery.Append("so.UpdatedBy, so.NetAmt, so.AdvanceAmt, so.BalanceAmount, so.SOStatus, so.UpdatedOn, so.CustomerNo " & vbCrLf)
                SqlQuery.Append("Order By si.SaleInvNumber Desc; " & vbCrLf & vbCrLf)

            ElseIf pDocType = "BirthList" Then
                SqlQuery.Append("Select si.SaleInvNumber As InvoiceNo, si.DocumentNumber As DocNo, " & vbCrLf)
                SqlQuery.Append("Convert(Varchar(10),si.CreatedOn,105) As CreatedOn, si.CreatedBy, " & vbCrLf)
                SqlQuery.Append("bh.UpdatedBy As LastUpdatedBy, bh.PaidAmt as TotalPayableAmt, " & vbCrLf)
                SqlQuery.Append("bh.PaidAmt as TotalAmtReceived, '0' as BalanceAmount, " & vbCrLf)
                SqlQuery.Append("Sum(si.AmountTendered) As AmtReceivedToday, bl.BirthListStatus As Status, " & vbCrLf)
                SqlQuery.Append("Convert(Varchar(10),bh.UpdatedOn ,105) As UpdatedOn " & vbCrLf)
                SqlQuery.Append("from SalesInvoice si " & vbCrLf)
                SqlQuery.Append("Inner join BirthListSalesHdr bh on si.DocumentNumber =bh.BirthListId " & vbCrLf)
                SqlQuery.Append("And si.SaleInvNumber=bh.SaleInvNumber " & vbCrLf)
                SqlQuery.Append("And si.SiteCode=bh.SiteCode And si.FinYear=bh.FinYear " & vbCrLf)
                SqlQuery.Append("Inner Join BirthList bl on bh.BirthListId=bl.BirthListId " & vbCrLf)
                SqlQuery.Append("And si.SiteCode=bl.SiteCode And si.FinYear=bl.FinYear " & vbCrLf)
                SqlQuery.Append("Where si.status =1 and si.SiteCode='" & pSiteCode & "' And si.FinYear='" & pFinYear & "' " & vbCrLf)
                SqlQuery.Append("And si.DocumentNumber='" & pDocNo & "'  And bl.BirthListStatus <> 'Cancel' " & vbCrLf)
                SqlQuery.Append("And Convert(varchar(10),si.SOInvDate,112)='" & pCurrentDate & "' and si.TenderTypeCode <> 'OpenAmount'" & vbCrLf)
                SqlQuery.Append("Group By si.CreatedOn, si.SaleInvNumber, si.DocumentNumber, si.CreatedBy, bh.UpdatedOn,  " & vbCrLf)
                SqlQuery.Append("bh.UpdatedBy, bh.PaidAmt, bl.BirthListStatus Order By si.SaleInvNumber Desc; " & vbCrLf & vbCrLf)

            End If

            SqlQuery.Append("Select A.*,B.Tenderheadname as Receipt from SalesInvoice A inner join MstTender B on A.tenderheadcode=b.tenderheadcode" & vbCrLf)
            SqlQuery.Append("Where A.status =1 and A.SiteCode='" & pSiteCode & "' And A.FinYear='" & pFinYear & "' " & vbCrLf)
            SqlQuery.Append("And A.DocumentNumber='" & pDocNo & "' " & vbCrLf)
            SqlQuery.Append("And Convert(varchar(10),A.SOInvDate,112)='" & pCurrentDate & "'; " & vbCrLf)

            SqlQuery.Append("Select * from SalesInvoiceAudit " & vbCrLf)
            SqlQuery.Append("Where SiteCode='" & pSiteCode & "' And FinYear='" & pFinYear & "' " & vbCrLf)
            SqlQuery.Append("And DocumentNumber='0' " & vbCrLf)
            SqlQuery.Append("And Convert(varchar(10),CreatedOn,112)='" & pCurrentDate & "'; " & vbCrLf)

            daTender = New SqlDataAdapter(SqlQuery.ToString, ConString)
            cmdbTender = New SqlCommandBuilder(daTender)
            dsTender = New DataSet
            daTender.Fill(dsTender)

            dsTender.Tables(0).TableName = "SalesOrderInfo"
            dsTender.Tables(1).TableName = "SalesInvoice"
            dsTender.Tables(2).TableName = "SalesInvoiceAudit"

            Return dsTender

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    Public Function GetChangeHistoryNo(ByVal vSiteCode As String, ByVal vFinYear As String, ByVal vSalesNo As String, ByVal vInvoiceNo As String) As Integer
        Try
            Dim changeOrderNo As Object

            SqlQuery.Length = 0
            SqlQuery.Append("Select Max(InvoiceChangeNumber)+1 As ChangeOrderNo from SalesInvoiceAudit" & vbCrLf)
            SqlQuery.Append("Where SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "'" & vbCrLf)
            SqlQuery.Append("And DocumentNumber='" & vSalesNo & "' And SaleInvNumber='" & vInvoiceNo & "'" & vbCrLf)

            If SpectrumCon.State = ConnectionState.Closed Then
                SpectrumCon.Open()
            End If

            cmdTender = New SqlCommand(SqlQuery.ToString, SpectrumCon)
            changeOrderNo = cmdTender.ExecuteScalar
            SpectrumCon.Close()

            Return IIf(changeOrderNo Is DBNull.Value, 1, changeOrderNo)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function IsSOPickupDone(ByVal vSiteCode As String, ByVal vFinYear As String, ByVal vSalesNo As String) As Boolean
        Try
            IsSOPickupDone = False
            SqlQuery.Length = 0
            SqlQuery.Append(" select SUM(ISNULL(DeliveredQty,0))  from SalesOrderDtl  AS SOD" & vbCrLf)
            SqlQuery.Append("Where SOD.SiteCode='" & vSiteCode & "' And SOD.FinYear='" & vFinYear & "'" & vbCrLf)
            SqlQuery.Append("And SOD.SaleOrderNumber='" & vSalesNo & "'" & vbCrLf)

            If SpectrumCon.State = ConnectionState.Closed Then
                SpectrumCon.Open()
            End If

            cmdTender = New SqlCommand(SqlQuery.ToString, SpectrumCon)
            If (cmdTender.ExecuteScalar > 0) Then
                IsSOPickupDone = True
            End If

            SpectrumCon.Close()


        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function PrepareSaveInvoiceData(ByRef dsMain As DataSet, ByVal vSiteCode As String, ByVal vFinYear As String, ByVal vSalesNo As String, ByVal vInvoiceNo As String, Optional ByVal strUserId As String = "", Optional ByVal strCVProgram As String = "", Optional ByVal dateDayOpen As Date = Nothing, Optional ByVal strDocumentType As String = "", Optional ByVal iVoucherDays As Long = 0) As Boolean
        Try
            Dim objclsComman As New clsCommon
            Dim objclsBirthListGlobal As New clsBirthListGobal
            Dim strQuery As String = "select * from salesInvoice Where status =1 and SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "' And DocumentNumber = '" & vSalesNo & "' And SaleInvNumber='" & vInvoiceNo & "'  "
            Dim strErrorMsg As String = ""
            Dim dtLastInvoiceData As DataTable = objclsBirthListGlobal.RetrieveQuery(strQuery, strErrorMsg)
            If Not strQuery = String.Empty Then
                OpenConnection()
                SqlTrans = SpectrumCon.BeginTransaction()
                If SaveInvoiceData(dsMain, SpectrumCon, SqlTrans, vSiteCode, vFinYear, vSalesNo, vInvoiceNo) = True Then
                    If CheckForCreditVouc(dtLastInvoiceData, dsMain, SpectrumCon, SqlTrans, vSiteCode, strUserId, strCVProgram, dateDayOpen, strDocumentType, iVoucherDays, vFinYear, vInvoiceNo, vSalesNo) Then

                        If clsComn.UpdateDocumentNo("InvoiceHistory", SpectrumCon, SqlTrans) = True Then

                            SqlTrans.Commit()


                            CloseConnection()
                            OpenConnection()
                            Dim SqlTrans1 As SqlTransaction = SpectrumCon.BeginTransaction()
                            If (objclsComman.AssignVoucherToBill(SpectrumCon, SqlTrans1, vSiteCode, vSalesNo, vFinYear, strDocumentType, vInvoiceNo)) Then
                                SqlTrans1.Commit()
                                CloseConnection()
                            Else
                                SqlTrans1.Rollback()
                                CloseConnection()
                                Return False
                            End If

                            Return True
                        End If
                    End If
                    SqlTrans.Rollback()
                    CloseConnection()
                End If
            End If

            Return False

        Catch ex As Exception
            SqlTrans.Rollback()
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function
    Public Sub UpdateCashier(ByVal userid As String, ByVal sitecode As String, ByVal Terminal As String, ByVal Billno As String)
        Try
            OpenConnection()
            SqlTrans = SpectrumCon.BeginTransaction()
            If clsComn.UpdateCashier(userid, sitecode, Terminal, Billno, SpectrumCon, SqlTrans) = True Then
                SqlTrans.Commit()
            Else
                SqlTrans.Rollback()
            End If
            CloseConnection()
        Catch ex As Exception
            SqlTrans.Rollback()
            CloseConnection()
            LogException(ex)
        End Try
    End Sub
    Public Function SaveInvoiceData(ByRef dsMainInvc As DataSet, ByRef Sqlconn As SqlConnection, ByRef SqlTran As SqlTransaction, ByVal vSiteCode As String, ByVal vFinYear As String, ByVal vSalesNo As String, ByVal vInvoiceNo As String) As Boolean
        Try
            For TbCnt = 0 To dsMainInvc.Tables.Count - 1
                Dim tableName As String = dsMainInvc.Tables(TbCnt).TableName
                If Not (tableName = "SalesOrderInfo") Then
                    If tableName = "SalesInvoice" Then
                        Dim cmdDelInvc As New SqlCommand
                        SqlQuery.Length = 0
                        'SqlQuery.Append("Delete from SalesInvoice " & vbCrLf)
                        'SqlQuery.Append("Where SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "' " & vbCrLf)
                        'SqlQuery.Append("And DocumentNumber = '" & vSalesNo & "' And SaleInvNumber='" & vInvoiceNo & "' " & vbCrLf)
                        SqlQuery.Append("Update SalesInvoice set updatedon=getdate(),status=0" & vbCrLf)
                        SqlQuery.Append("Where status =1 and SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "' " & vbCrLf)
                        SqlQuery.Append("And DocumentNumber = '" & vSalesNo & "' And SaleInvNumber='" & vInvoiceNo & "' " & vbCrLf)
                        cmdDelInvc = New SqlCommand(SqlQuery.ToString, Sqlconn)
                        cmdDelInvc.Transaction = SqlTran
                        cmdDelInvc.CommandTimeout = 0
                        cmdDelInvc.ExecuteNonQuery()
                    ElseIf tableName = "CheckDtls" Then
                        Dim updatechkdtls As New SqlCommand
                        SqlQuery.Append("update checkdtls set status=0, updatedon=getdate() where sitecode='" & Sitecode & "' and billno='" & vInvoiceNo & "' and status =1 " & vbCrLf)
                        updatechkdtls = New SqlCommand(SqlQuery.ToString, Sqlconn)
                        updatechkdtls.Transaction = SqlTran
                        updatechkdtls.CommandTimeout = 0
                        updatechkdtls.ExecuteNonQuery()
                    End If

                    'Rakesh-06.11.2013: Avoid extra columns in selected column list
                    'Dim tableColumns As String = ""
                    'For ColIndex = 0 To dsMainInvc.Tables(TbCnt).Columns.Count - 1
                    '    tableColumns = tableColumns & ", " & dsMainInvc.Tables(TbCnt).Columns(ColIndex).ColumnName.ToString()
                    'Next
                    'tableColumns = tableColumns.Substring(1)

                    SqlQuery.Length = 0
                    SqlQuery.Append("SELECT * FROM " & tableName)
                    SqlQuery.Append(" Where SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "' " & vbCrLf)
                    If Not tableName = "CheckDtls" Then
                        SqlQuery.Append(" And DocumentNumber = '" & vSalesNo & "' And SaleInvNumber='" & vInvoiceNo & "' " & vbCrLf)
                    Else
                        SqlQuery.Append(" And DocumentNo = '" & vSalesNo & "' And BillNo='" & vInvoiceNo & "' " & vbCrLf)
                    End If


                    daTender = New SqlDataAdapter(SqlQuery.ToString, Sqlconn)
                    daTender.SelectCommand.Transaction = SqlTran
                    cmdbTender = New SqlCommandBuilder(daTender)
                    daTender.TableMappings.Add(tableName, tableName)
                    daTender = cmdbTender.DataAdapter
                    daTender.Update(dsMainInvc, tableName)
                    If tableName.ToUpper() = "SALESINVOICE" Then
                        dsMainInvc.Tables(tableName).Clear()
                        daTender.Fill(dsMainInvc, tableName)
                    End If

                End If

            Next

            If Not dsMainInvc.Tables.Contains("Checkdtls") Then
                Dim updatechkdtls As New SqlCommand
                SqlQuery.Append("update checkdtls set status=0, updatedon=getdate() where sitecode='" & Sitecode & "' and billno='" & vInvoiceNo & "' and status =1 " & vbCrLf)
                updatechkdtls = New SqlCommand(SqlQuery.ToString, Sqlconn)
                updatechkdtls.Transaction = SqlTran
                updatechkdtls.CommandTimeout = 0
                updatechkdtls.ExecuteNonQuery()
            End If

            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' TODO: Retrieves last sales invoice records against document.
    ''' For comparing current salesinvoice records with past records.
    ''' </summary>      

    Public Function CheckForCreditVouc(ByVal dtlastinvoice As DataTable, ByVal ds As DataSet, ByRef sqlcon As SqlConnection, ByRef sqltran As SqlTransaction, ByVal strSiteCode As String, Optional ByVal strUserId As String = "", Optional ByVal strCVProgram As String = "", Optional ByVal dateDayOpen As DateTime = Nothing, Optional ByVal strDocumentType As String = "", Optional ByVal iVoucherDays As Long = 0, Optional ByVal strFinYear As String = "", Optional ByVal strSalesInvoiceNumber As String = "", Optional ByVal strDocumentNumber As String = "") As Boolean
        Try

            'Dim strSalesInvoiceNumber As String = ""
            Dim objclsComman As New clsCommon
            Dim strVoucherNo As String = ""
            Dim objclscashmemo As New clsCashMemo
            If Not dtlastinvoice Is Nothing AndAlso dtlastinvoice.Rows.Count > 0 Then
                'Last invoice Checking CV or GV issued 
                Dim drlastRows_CreditVoucI As DataRow() = dtlastinvoice.Select("TenderTypeCode='CreditVouc(I)'  or TenderTypeCode='GiftVoucher(I)'")
                If Not drlastRows_CreditVoucI Is Nothing Then

                    For Each drLast As DataRow In drlastRows_CreditVoucI
                        If Not drLast("RefNo_2") Is DBNull.Value Then
                            strVoucherNo = drLast("RefNo_2")
                        ElseIf Not drLast("RefNo_1") Is DBNull.Value Then
                            strVoucherNo = drLast("RefNo_1")
                        End If

                        strDocumentNumber = drLast("DocumentNumber")
                        objclsComman.DeactivateVoucher(sqlcon, sqltran, strSiteCode, strDocumentNumber, strUserId, strDocumentType, strVoucherNo)
                    Next
                End If
                'Last invoice checking cv or gv redemmed 
                Dim drlastRows_CreditVoucR As DataRow() = dtlastinvoice.Select("TenderTypeCode='CreditVouc(R)'  or TenderTypeCode='GiftVoucher(R)'")
                If Not drlastRows_CreditVoucR Is Nothing Then
                    For Each drLast As DataRow In drlastRows_CreditVoucR
                        If Not drLast("RefNo_2") Is DBNull.Value Then
                            strVoucherNo = drLast("RefNo_2")
                        ElseIf Not drLast("RefNo_1") Is DBNull.Value Then
                            strVoucherNo = drLast("RefNo_1")
                        End If
                        objclsComman.ActiveVoucher(sqlcon, sqltran, strSiteCode, strVoucherNo, strUserId)
                    Next
                End If
            End If
            If Not ds Is Nothing AndAlso ds.Tables.Contains("SalesInvoice") Then
                'Issued voucher in updated Sales Invoice 
                Dim drCurrentRows_CreditVoucI As DataRow() = ds.Tables("SalesInvoice").Select("TenderTypeCode='CreditVouc(I)' or TenderTypeCode='GiftVoucher(I)'")
                If Not drCurrentRows_CreditVoucI Is Nothing Then

                    Dim decVoucherAmount As Decimal = 0
                    Dim strVoucherType As String = ""
                    For Each drCurrent_issue As DataRow In drCurrentRows_CreditVoucI
                        If (drCurrent_issue("RefNo_2") Is Nothing Or drCurrent_issue("RefNo_2") Is DBNull.Value) And (drCurrent_issue("RefNo_1") Is Nothing Or drCurrent_issue("RefNo_1") Is DBNull.Value) Then
                            strVoucherNo = ""
                            strVoucherType = drCurrent_issue("TenderTypeCode")
                            If strVoucherType = "CreditVouc(I)" Then
                                strVoucherType = "CV"
                            Else
                                strVoucherType = "GV"
                            End If
                            decVoucherAmount = drCurrent_issue("AmountTendered")
                            objclscashmemo.UpdateCreditVoucher(strCVProgram, strDocumentType, True, strDocumentNumber, strSiteCode, dateDayOpen, strUserId, sqltran, sqlcon, decVoucherAmount, strVoucherNo, iVoucherDays, strVoucherType)

                        Else
                            If Not drCurrent_issue("RefNo_2") Is DBNull.Value Then
                                strVoucherNo = drCurrent_issue("RefNo_2")
                            ElseIf Not drCurrent_issue("RefNo_1") Is DBNull.Value Then
                                strVoucherNo = drCurrent_issue("RefNo_1")
                            End If


                            objclsComman.ActiveVoucher(sqlcon, sqltran, strSiteCode, strVoucherNo, strUserId)
                            'objclsComman.DeactivateVoucher(sqlcon, sqltran, strSiteCode, strDocumentNumber, strUserId, strDocumentType, strVoucherNo)
                        End If
                    Next
                End If

                'Redemeed Voucher in Updated Sales invoice 
                Dim drCurrentRows_CreditVoucRedemeed As DataRow() = ds.Tables("SalesInvoice").Select("TenderTypeCode='CreditVouc(R)' or TenderTypeCode='GiftVoucher(R)'")
                If Not drCurrentRows_CreditVoucRedemeed Is Nothing Then
                    For Each drCurrent_redemmed As DataRow In drCurrentRows_CreditVoucRedemeed
                        strVoucherNo = drCurrent_redemmed("RefNo_1")
                        objclsComman.DeactivateVoucher(sqlcon, sqltran, strSiteCode, strDocumentNumber, strUserId, strDocumentType, strVoucherNo)
                    Next
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Function IsTotalPickUp(ByVal vSiteCode As String, ByVal vFinYear As String, ByVal vSalesNo As String) As DataTable
        Try
            Dim dt As DataTable
            SqlQuery.Length = 0
            Dim strString As String = "select *  from SalesOrderDtl  AS SOD Where SOD.SiteCode='" & vSiteCode & "' And SOD.FinYear='" & vFinYear & "'And SOD.SaleOrderNumber='" & vSalesNo & "'"
            Dim da As New SqlDataAdapter(strString, ConString)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

End Class
