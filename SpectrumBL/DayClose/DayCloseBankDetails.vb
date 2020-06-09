Imports SpectrumCommon
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Linq

Imports System.Reflection ' For Missing.Value and BindingFlags
Imports System.Runtime.InteropServices ' For COMException
Public Class DayCloseBankDetails
    Inherits clsDayCloseBase
    Implements IDayCloseBankDetails

    Public Function GetDayCloseBankDetails(ByVal request As SpectrumCommon.DayCloseBankDetailsRequest, Optional ByVal AllowQtyZero As Boolean = False, Optional ByVal ClientForMail As String = "") As SpectrumCommon.DayCloseBankDetailsResponse Implements IDayCloseBankDetails.GetDayCloseBankDetails
        Try
            Dim response As New DayCloseBankDetailsResponse
            Dim cashDtlQuery As String
            'If request.IsShiftManagement Then
            '    cashDtlQuery = "select CurrencyCode ,DenominationAmt ,sum (Qty) as Qty  from FloatingDetail Right Outer Join MstTerminalID AS MTI on FloatingDetail.TerminalID = MTI.TerminalID  AND  FloatingDetail.SiteCode = MTI.SiteCode AND FloatingDetail.ShiftId = (Select ISNULL(Max(ShiftId),0) From ShiftOpenClose Where TerminalID =FloatingDetail.TerminalID And Opendate= '" & request.DayCloseDate.ToString("yyyy-MM-dd") & "'and  SiteCode = '" & request.SiteCode & "' ) where FloatingDetail.SiteCode = '" & request.SiteCode & "' and Action = '" & DayCloseAction.Close.ToString() & "' and flotDatetime ='" & request.DayCloseDate.ToString("yyyy-MM-dd") & "' group by DenominationAmt,CurrencyCode "
            'Else
            cashDtlQuery = "select CurrencyCode ,DenominationAmt ,sum (Qty) as Qty  from FloatingDetail where SiteCode = '" & request.SiteCode & "' and Action = '" & DayCloseAction.Close.ToString() & "' and flotDatetime ='" & request.DayCloseDate.ToString("yyyy-MM-dd") & "' group by DenominationAmt,CurrencyCode "
            'End If
            'cashDtlQuery = "select CurrencyCode ,DenominationAmt ,sum (Qty) as Qty  from FloatingDetail where SiteCode = '" & request.SiteCode & "' and Action = '" & DayCloseAction.Close.ToString() & "' and flotDatetime ='" & request.DayCloseDate.ToString("yyyy-MM-dd") & "' group by DenominationAmt,CurrencyCode "
            Using dataReader As SqlDataReader = GetReader(cashDtlQuery)
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        Dim cashDenominationDtl As New CashDenominationDtl
                        cashDenominationDtl.CurrencyCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        cashDenominationDtl.DenominationAmt = IIf(IsDBNull(dataReader.GetDecimal(1)), 0, dataReader.GetDecimal(1))
                        cashDenominationDtl.Quantity = IIf(IsDBNull(dataReader.GetDecimal(2)), 0, dataReader.GetDecimal(2))
                        'added by Khusrao Adil
                        'set default quantity as zero
                        If AllowQtyZero Then
                            cashDenominationDtl.EnteredQuantity = 0
                        End If
                        response.CashDenominationList.Add(cashDenominationDtl)
                    Loop
                End If
            End Using

            If response.CashDenominationList.Count > 0 Then
                cashDtlQuery = "SELECT 	B.CURRENCYCODE,A.DENOMINATIONAMT As DenominationAmt"
                cashDtlQuery = cashDtlQuery & "  FROM DENOMINATIONDETAIL A INNER JOIN MSTCURRENCY B ON A.CURRENCYCODE=B.CURRENCYCODE where b.CurrencyCode='" & response.CashDenominationList(0).CurrencyCode & "' And A.Status = 1" & _
                            "ORDER BY A.DENOMINATIONAMT"
                Using dataReader As SqlDataReader = GetReader(cashDtlQuery)
                    If dataReader.HasRows Then
                        Do While dataReader.Read()
                            Dim cashDenominationDtl As New CashDenominationDtl
                            cashDenominationDtl.CurrencyCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                            cashDenominationDtl.DenominationAmt = IIf(IsDBNull(dataReader.GetDecimal(1)), 0, dataReader.GetDecimal(1))
                            'added by Khusrao Adil
                            'set default quantity as zero
                            If AllowQtyZero Then
                                cashDenominationDtl.EnteredQuantity = 0
                            End If
                            'cashDenominationDtl.Quantity = IIf(IsDBNull(dataReader.GetDecimal(2)), 0, dataReader.GetDecimal(2))
                            Dim isExist = response.CashDenominationList.Where(Function(item) item.DenominationAmt = cashDenominationDtl.DenominationAmt).FirstOrDefault()
                            If isExist Is Nothing Then
                                response.CashDenominationList.Add(cashDenominationDtl)
                            End If
                        Loop
                    End If
                End Using
            End If
            response.CashDenominationList = response.CashDenominationList.OrderBy(Function(denomination) denomination.DenominationAmt).ToList()
            Dim nextDayFloatList As List(Of CashDenominationDtl)
            '---- Changes done by Mahesh for getting qty for all till adding Group BY 
            Dim nextDayFloatQuery As String
            'If request.IsShiftManagement Then
            '    nextDayFloatQuery = "select CurrencyCode ,DenominationAmt ,sum(isnull(Qty,0)) as Qty  from FloatingDetail where SiteCode = '" & request.SiteCode & "' and Action = '" & DayCloseAction.Open.ToString() & "' and flotDatetime ='9999-12-01 00:00:00.000'  " & _
            '                                  " AND TerminalID in(SELECT  distinct FloatingDetail.TerminalID FROM   FloatingDetail Right Outer Join MstTerminalID AS MTI on FloatingDetail.TerminalID = MTI.TerminalID  AND  FloatingDetail.SiteCode = MTI.SiteCode " & _
            '                                  "AND FloatingDetail.ShiftId = (Select ISNULL(Max(ShiftId),0) From ShiftOpenClose Where TerminalID =FloatingDetail.TerminalID And Opendate= '" & request.DayCloseDate.ToString("yyyy-MM-dd") & "' and SiteCode = '" & request.SiteCode & "')" & _
            '                                  " Where  Action = 'ExtraOpen' AND flotDatetime >='" & request.DayCloseDate.ToString("yyyy-MM-dd") & "')" & _
            '                                  " Group By CurrencyCode ,DenominationAmt "
            'Else
            nextDayFloatQuery = "select CurrencyCode ,DenominationAmt ,sum(isnull(Qty,0)) as Qty  from NextDayFloatingDetail where SiteCode = '" & request.SiteCode & "' and Action = '" & DayCloseAction.Open.ToString() & "' and flotDatetime ='" & request.DayCloseDate.ToString("yyyy-MM-dd") & "'  " & _
                                          " AND TerminalID in(SELECT  distinct TerminalID FROM   FloatingDetail " & _
                                                    " Where  Action = 'ExtraOpen' AND flotDatetime >='" & request.DayCloseDate.ToString("yyyy-MM-dd") & "')" & _
                                                    " Group By CurrencyCode ,DenominationAmt "
            'End If

            Using dataReader As SqlDataReader = GetReader(nextDayFloatQuery)
                If dataReader.HasRows Then
                    nextDayFloatList = New List(Of CashDenominationDtl)
                    Do While dataReader.Read()
                        Dim cashDenominationDtl As New CashDenominationDtl
                        cashDenominationDtl.CurrencyCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        cashDenominationDtl.DenominationAmt = IIf(IsDBNull(dataReader.GetDecimal(1)), 0, dataReader.GetDecimal(1))
                        cashDenominationDtl.Quantity = IIf(IsDBNull(dataReader.GetDecimal(2)), 0, dataReader.GetDecimal(2))
                        nextDayFloatList.Add(cashDenominationDtl)
                    Loop
                End If
            End Using

            If nextDayFloatList IsNot Nothing AndAlso nextDayFloatList.Count > 0 Then
                For Each cashDenomination In response.CashDenominationList
                    Dim cashDenomAmt = cashDenomination.DenominationAmt
                    Dim nextDayFloat = nextDayFloatList.Where(Function(item) item.DenominationAmt = cashDenomAmt).FirstOrDefault()
                    If nextDayFloat IsNot Nothing Then
                        cashDenomination.Quantity = cashDenomination.Quantity - nextDayFloat.Quantity
                    End If
                Next
            End If

            response.ActualTotalCashAmt = response.CashDenominationList.Sum(Function(item) item.Quantity * item.DenominationAmt)

            If (ClientForMail = "PC") Then
                Dim chequeDetailsQuery As String = String.Empty

                chequeDetailsQuery = "select  A.CardNo , cast( round(A.AmountTendered,0) as int) as AmountTendered, ISNULL(CD.BankName,'') BankName FROM CashMemoReceipt A " & vbCrLf
                chequeDetailsQuery += "INNER JOIN CashMemoHdr B ON A.BillNo = B.BillNo " & vbCrLf
                chequeDetailsQuery += "LEFT JOIN CheckDtls CD on CD.SiteCode =b.SiteCode and cd.BillNo = b.BillNo" & vbCrLf
                chequeDetailsQuery += "WHERE A.status=1 and A.SiteCode = '" & request.SiteCode & "' And A.FinYear = '" & request.FinYear & "' and A.TenderTypeCode = 'cheque' " & vbCrLf
                chequeDetailsQuery += "AND A.CardNo IS NOT NULL and b.BillDate = '" & request.DayCloseDate.ToString("yyyy-MM-dd") & "' " & vbCrLf
                '------change by Mahesh  behalf of Issue addition 0012924 , removing cheque amt from it and show all cheque Adding SO Cheque Details & credit adjustmen Too 
                chequeDetailsQuery += " Union ALL "
                chequeDetailsQuery += " SELECT  A.RefNo_2 , cast( round(A.AmountTendered,0) as int) as AmountTendered ,ISNULL(CD.BankName,'') BankName "
                chequeDetailsQuery += " FROM SalesInvoice A  "
                chequeDetailsQuery += " INNER JOIN SalesOrderHdr B ON A.DocumentNumber = B.SaleOrderNumber "
                chequeDetailsQuery += " LEFT JOIN CheckDtls CD on CD.SiteCode =a.SiteCode and cd.BillNo = a.SaleInvNumber "
                chequeDetailsQuery += " WHERE A.status=1 and A.SiteCode = '" & request.SiteCode & "' And A.FinYear = '" & request.FinYear & "' and A.TenderTypeCode = 'cheque' " & vbCrLf
                chequeDetailsQuery += " AND  A.RefNo_2  IS NOT NULL and SOInvDate = '" & request.DayCloseDate.ToString("yyyy-MM-dd") & "' " & vbCrLf
                chequeDetailsQuery += " Union ALL "
                chequeDetailsQuery += " Select  A.CardNo , cast( round(A.AmountTendered,0) as int) as AmountTendered , ISNULL(CD.BankName,'') as BankName "
                chequeDetailsQuery += " from CreditReceipt AS A "
                chequeDetailsQuery += " LEFT JOIN CheckDtls CD on CD.SiteCode =a.SiteCode and cd.BillNo = a.RefBillNo "
                chequeDetailsQuery += " WHERE A.status=1 and A.SiteCode = '" & request.SiteCode & "' And A.FinYear = '" & request.FinYear & "' and A.TenderTypeCode = 'cheque' " & vbCrLf
                chequeDetailsQuery += " AND A.CardNo IS NOT NULL and CmRcptDateTime = '" & request.DayCloseDate.ToString("yyyy-MM-dd") & "' " & vbCrLf


                Using dataReader As SqlDataReader = GetReader(chequeDetailsQuery)
                    If dataReader.HasRows Then
                        Dim CurrentRow1 As Integer = 0
                        Do While dataReader.Read()
                            Dim chequeDetails As New ChequeDetails
                            chequeDetails.SrNo1 = CurrentRow1 + 1
                            CurrentRow1 = CurrentRow1 + 1
                            'chequeDetails.SrNo = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                            chequeDetails.ChequeNumber = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                            chequeDetails.Amount = IIf(IsDBNull(dataReader.GetSqlInt32(1)), 0, dataReader.GetInt32(1))
                            chequeDetails.BankName = IIf(IsDBNull(dataReader.GetString(2)), 0, dataReader.GetString(2))
                            response.ChequeDetailsList.Add(chequeDetails)
                        Loop
                    End If
                End Using



                Dim OtherTenderQuery As String = String.Empty
                OtherTenderQuery = " exec UDP_DayCloseWiseNEFTRTGSDetail '" & request.SiteCode & "', '" & request.DayCloseDate.ToString("yyyy-MM-dd") & "'"


                'OtherTenderQuery = "  select  case when tendertypecode in ('neft','rtgs') then upper(tendertypecode) else tendertypecode end as tendertypecode ,'-' as 'ReferenceNo', cast(cast(AmountTendered as int) as varchar(50))  as TotalAmount  from CashMemoReceipt " & vbCrLf
                'OtherTenderQuery += " where SiteCode='" & request.SiteCode & "' and CmRcptDate ='" & request.DayCloseDate.ToString("yyyy-MM-dd") & "'  and TenderTypeCode not in ('Cheque','Cash')  " & vbCrLf

                'OtherTenderQuery += "   union all select case when tendertypecode in ('neft','rtgs') then upper(tendertypecode) else tendertypecode end as tendertypecode,'-'  as ReferenceNo,cast(cast(AmountTendered as int) as varchar(50))  as TotalAmount from SalesInvoice" & vbCrLf
                'OtherTenderQuery += " where SiteCode='" & request.SiteCode & "' and soinvdate ='" & request.DayCloseDate.ToString("yyyy-MM-dd") & "'  and TenderTypeCode not in ('Cheque','Cash')  " & vbCrLf



                Using dataReader As SqlDataReader = GetReader(OtherTenderQuery)
                    If dataReader.HasRows Then
                        Dim CurrentRow As Integer = 0
                        Do While dataReader.Read()
                            Dim OtheTenderDtl As New OtherTenderDetail
                            OtheTenderDtl.SrNo = CurrentRow + 1
                            CurrentRow = CurrentRow + 1
                            OtheTenderDtl.Tender = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0)).ToString().Trim()
                            OtheTenderDtl.ReferenceNo = IIf(IsDBNull(dataReader.GetString(1)), 0, dataReader.GetString(1))
                            OtheTenderDtl.Amt = IIf(IsDBNull(dataReader.GetString(2)), 0, dataReader.GetString(2))
                            response.OtherTenderDetail.Add(OtheTenderDtl)
                        Loop
                    End If
                End Using


                response.TotalChequeAmt = response.ChequeDetailsList.Sum(Function(item) item.Amount)
            Else
                Dim chequeDetailsQuery As String = String.Empty
                '  select CardNo , AmountTendered  from dbo.CashMemoReceipt where SiteCode = '" & request.SiteCode & "' And FinYear = '" & request.FinYear & "' and TenderTypeCode = 'cheque' and CREATEDON >= '" & request.DayCloseDate.ToString("yyyy-MM-dd") & "' and CardNo Is Not NULL"
                chequeDetailsQuery = "SELECT A.CardNo , A.AmountTendered FROM CashMemoReceipt A " & vbCrLf
                chequeDetailsQuery = "select row_number() over ( order by a.cardno)  as 'SrNo', A.CardNo , A.AmountTendered, '-' BankName FROM CashMemoReceipt A " & vbCrLf
                chequeDetailsQuery += "INNER JOIN CashMemoHdr B ON A.BillNo = B.BillNo " & vbCrLf
                chequeDetailsQuery += "WHERE A.status=1 and A.SiteCode = '" & request.SiteCode & "' And A.FinYear = '" & request.FinYear & "' and A.TenderTypeCode = 'cheque' " & vbCrLf
                chequeDetailsQuery += "AND A.CardNo IS NOT NULL and BillDate = '" & request.DayCloseDate.ToString("yyyy-MM-dd") & "' " & vbCrLf
                '------change by Mahesh  behalf of Issue addition 0012924 , removing cheque amt from it and show all cheque Adding SO Cheque Details & credit adjustmen Too 
                chequeDetailsQuery += " Union ALL "
                chequeDetailsQuery += " SELECT row_number() over ( order by a.documentnumber) as 'SrNo' , A.RefNo_2 , A.AmountTendered ,'-' BankName "
                chequeDetailsQuery += " FROM SalesInvoice A  "
                chequeDetailsQuery += " INNER JOIN SalesOrderHdr B ON A.DocumentNumber = B.SaleOrderNumber "
                chequeDetailsQuery += " WHERE A.status=1 and A.SiteCode = '" & request.SiteCode & "' And A.FinYear = '" & request.FinYear & "' and A.TenderTypeCode = 'cheque' " & vbCrLf
                chequeDetailsQuery += " AND  A.RefNo_2  IS NOT NULL and SOInvDate = '" & request.DayCloseDate.ToString("yyyy-MM-dd") & "' " & vbCrLf
                chequeDetailsQuery += " Union ALL "
                chequeDetailsQuery += " Select row_number() over ( order by a.billno) as 'SrNo', A.CardNo , A.AmountTendered , '-' as BankName "
                chequeDetailsQuery += " from CreditReceipt AS A "
                chequeDetailsQuery += " WHERE A.status=1 and A.SiteCode = '" & request.SiteCode & "' And A.FinYear = '" & request.FinYear & "' and A.TenderTypeCode = 'cheque' " & vbCrLf
                chequeDetailsQuery += " AND A.CardNo IS NOT NULL and CmRcptDateTime = '" & request.DayCloseDate.ToString("yyyy-MM-dd") & "' " & vbCrLf



                Using dataReader As SqlDataReader = GetReader(chequeDetailsQuery)
                    If dataReader.HasRows Then
                        Dim CurrentRow1 As Integer = 0
                        Do While dataReader.Read()
                            Dim chequeDetails As New ChequeDetails
                            chequeDetails.ChequeNumber = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                            chequeDetails.Amount = IIf(IsDBNull(dataReader.GetDecimal(1)), 0, dataReader.GetDecimal(1))
                            response.ChequeDetailsList.Add(chequeDetails)
                        Loop
                    End If
                End Using

                response.TotalChequeAmt = response.ChequeDetailsList.Sum(Function(item) item.Amount)
            End If


            Return response
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function SaveBankDetailsData(ByVal request As SpectrumCommon.BankDetailsSaveDataRequest, ByVal reportPath As String, Optional isUpdateStockOnDayCloseWastage As Boolean = False) As Boolean Implements IDayCloseBankDetails.SaveBankDetailsData
        Try
            Dim objPettyCash As IPettyCashVoucher = New PettyCashVoucher()
            Dim result As Boolean
            Dim query As String = "select ArticleCode ,BUOMQuantity" & _
                                    " from DayCloseStockTakeDetails where SiteCode = '" & request.SiteCode & "' and DayCloseDate = '" & request.DayCloseDate.ToString("yyyy-MM-dd") & "' and status=1 "
            Dim stockTakeList As New List(Of StockTakeDetails)
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        Dim stockTakeDetails As New StockTakeDetails
                        stockTakeDetails.ArticleCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        stockTakeDetails.BUOMQty = IIf(IsDBNull(dataReader.GetDecimal(1)), 0, dataReader.GetDecimal(1))
                        stockTakeList.Add(stockTakeDetails)
                    Loop
                End If
            End Using
            '----------------------------------------------------------------------------------------------------------------------
            'added By Khusrao Adil
            ' for Chileeza and sbarro
            If isUpdateStockOnDayCloseWastage Then
                Dim query1 As String = "select ArticleCode ,WastageBUOMQty" & _
                                    " from DayCloseWastageDetails where SiteCode = '" & request.SiteCode & "' and DayCloseDate = '" & request.DayCloseDate.ToString("yyyy-MM-dd") & "' and status=1 "
                Dim wastageList As New List(Of WastageDetails)
                Using dataReader As SqlDataReader = GetReader(query1)
                    If dataReader.HasRows Then
                        Do While dataReader.Read()
                            Dim WastageDetails As New WastageDetails
                            WastageDetails.ArticleCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                            WastageDetails.WastageBUOMQty = IIf(IsDBNull(dataReader.GetDecimal(1)), 0, dataReader.GetDecimal(1))
                            wastageList.Add(WastageDetails)
                        Loop
                    End If
                End Using
                For Each item In wastageList
                    Dim articleWastageBalanceQuery As String = "Update ArticleStockBalances Set PhysicalQty =ISNULL(PhysicalQty,0) + " & (item.WastageBUOMQty * -1) & _
                                                    ", TotalPhysicalSaleableQty =ISNULL(TotalPhysicalSaleableQty,0) +  " & (item.WastageBUOMQty * -1) & _
                                                    ", TotalSaleableQty = ISNULL(TotalSaleableQty,0) + " & (item.WastageBUOMQty * -1) & _
                                                    ", UPDATEDAT = '" & request.UpdatedAt & "', UPDATEDBY = '" & request.UpdatedBy & _
                                                    "', UPDATEDON = GETDATE() " & _
                                                   "where SiteCode = '" & request.SiteCode & "' and ArticleCode = '" & item.ArticleCode & "' "
                    InsertOrUpdateRecord(articleWastageBalanceQuery)
                    '--------------------------------------------------------------------------------------
                    '' added By ketan Vaidya Updated Qty of KIT Article 
                    Dim SqlQry As String
                    Dim tran As SqlTransaction = Nothing
                    SqlQry = "SELECT A.ARTICLECODE,B.ARTICLECODE,B.EAN,B.QUANTITY,A.ARTICALTYPECODE,B.SaleUnitofMeasure,B.SellingPrice FROM MSTARTICLE A LEFT JOIN MSTARTICLEKIT B ON A.ARTICLECODE=B.KITARTICLECODE AND B.STATUS=1  WHERE A.ARTICLECODE='" & item.ArticleCode & "'"
                    Dim da As New SqlDataAdapter(SqlQry.ToString, SpectrumCon)
                    da.SelectCommand.Transaction = tran
                    Dim dt As New DataTable
                    da.Fill(dt)
                    If dt IsNot Nothing Then
                        If dt.Rows.Count > 0 Then
                            If dt.Rows(0)("ArticalTypeCode").ToString.ToUpper = "KIT" Then
                                For Each dr As DataRow In dt.Rows
                                    Dim KQty As Double = IIf(dr("QUANTITY") Is DBNull.Value, 0, dr("QUANTITY")) * item.WastageBUOMQty * -1
                                    Dim KEan As String = dr("EAN").ToString()
                                    Dim strQty As String
                                    Dim vReservedQtykit As Decimal = 0
                                    strQty = KQty
                                    vReservedQtykit = Val(KQty)

                                    Dim KitarticleWastageBalanceQuery As String = "Update ArticleStockBalances Set PhysicalQty =ISNULL(PhysicalQty,0) + " & strQty & _
                                                                     ", TotalPhysicalSaleableQty =ISNULL(TotalPhysicalSaleableQty,0) +  " & strQty & _
                                                                     ", TotalSaleableQty = ISNULL(TotalSaleableQty,0) + " & strQty & _
                                                                     ", UPDATEDAT = '" & request.UpdatedAt & "', UPDATEDBY = '" & request.UpdatedBy & _
                                                                     "', UPDATEDON = GETDATE() " & _
                                                                    "where SiteCode = '" & request.SiteCode & "' and ArticleCode = '" & dr("ARTICLECODE1").ToString() & "' "
                                    InsertOrUpdateRecord(KitarticleWastageBalanceQuery)

                                    '----------------------------------------------------------------------------------
                                Next

                            End If
                        End If
                    End If
                Next
            End If

            '---------------------------------------------------------------------------------------------------------------------
            For Each item In stockTakeList
                Dim articleStockBalanceQuery As String = "Update ArticleStockBalances Set PhysicalQty = " & item.BUOMQty & ", TotalPhysicalSaleableQty = " & item.BUOMQty & " , TotalSaleableQty = " & item.BUOMQty & ", " & _
                                           "UPDATEDAT = '" & request.UpdatedAt & "', UPDATEDBY = '" & request.UpdatedBy & "', UPDATEDON = '" & GetServerDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "' " & _
                                           "where SiteCode = '" & request.SiteCode & "' and ArticleCode = '" & item.ArticleCode & "' "
                InsertOrUpdateRecord(articleStockBalanceQuery)
            Next

            Dim updateDayCloseStatusQuery As String = "Update DayOpenNClose Set DayCloseStatus = 1, closedAt = 'FO', " & _
                                                      "UPDATEDAT='" & request.UpdatedAt & "', UPDATEDBY = '" & request.UpdatedBy & "', UPDATEDON = '" & GetServerDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "' " & _
                                                      "Where SiteCode = '" & request.SiteCode & "' and FinYear = '" & request.FinYear & "' and OpenDate = '" & request.DayCloseDate.ToString("yyyy-MM-dd") & "' "
            InsertOrUpdateRecord(updateDayCloseStatusQuery)
            'ADDED BY KHUSRAO ADIL ON 21-02-2018 FOR BO SERVICE UPDATE ADDED 2 NEW PARAMETERS -CreatedOn,UpdatedOn
            Dim strDayCloseDetail As String = "select * from DayOpenNClose Where SiteCode = '" & request.SiteCode & "' and FinYear = '" & request.FinYear & "' and OpenDate = '" & request.DayCloseDate.ToString("yyyy-MM-dd") & "'"
            Dim dtDayCloseDetail = GetFilledTable(strDayCloseDetail)
            If dtDayCloseDetail.Rows.Count > 0 Then
                request.CreatedOn = dtDayCloseDetail.Rows(0)("CreatedOn")
                request.UpdatedOn = dtDayCloseDetail.Rows(0)("UpdatedOn")
                request.CreatedBy = dtDayCloseDetail.Rows(0)("CreatedBy")
                request.UpdatedBy = dtDayCloseDetail.Rows(0)("UpdatedBy")
            Else
                request.CreatedOn = GetServerDate().ToString("yyyy-MM-dd HH:mm:ss.fff")
                request.UpdatedOn = GetServerDate().ToString("yyyy-MM-dd HH:mm:ss.fff")
            End If
            '-------------------------------------------------------------------------------------------------------------------------
            For Each Item In request.CashDenominationList
                If Item.EnteredQuantity IsNot Nothing AndAlso Item.EnteredQuantity <> 0 Then
                    Dim dayCloseBankdtlsInsert As String = "Insert into DayCloseBankDetails (SiteCode , CloseDate , CurrencyCode , DenominationAmt ,Qty, " & _
                                                  "TotalAmount ,CREATEDAT , CREATEDBY , CREATEDON ,UPDATEDAT , UPDATEDBY , UPDATEDON,STATUS)  VAlues " & _
                                                  "('" & request.SiteCode & "','" & request.DayCloseDate.ToString("yyyy-MM-dd") & "','" & Item.CurrencyCode & "'," & Item.DenominationAmt & "," & Item.EnteredQuantity & "," & Item.TotalAmt & ", " & _
                                                   "'" & request.CreatedAt & "','" & request.CreatedBy & "','" & GetServerDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "','" & request.UpdatedAt & "','" & request.UpdatedBy & "','" & GetServerDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "',1) "
                    InsertOrUpdateRecord(dayCloseBankdtlsInsert)
                End If
            Next
            Dim totalCashAmount As Decimal = request.CashDenominationList.Sum(Function(item) item.TotalAmt)
            Dim dayCloseSummaryCashInsert As String = "Insert Into DayCloseBankSummary (SiteCode , CloseDate,TenderType ,TotalAmount ,CREATEDAT , " & _
                                                    "CREATEDBY , CREATEDON ,UPDATEDAT , UPDATEDBY , UPDATEDON,STATUS) values " & _
                                                    "('" & request.SiteCode & "','" & request.DayCloseDate.ToString("yyyy-MM-dd") & "','" & TenderHeadCodes.Cash.ToString() & "'," & totalCashAmount & ",'" & request.CreatedAt & "', " & _
                                                    "'" & request.CreatedBy & "','" & GetServerDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "','" & request.UpdatedAt & "','" & request.UpdatedBy & "','" & GetServerDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "',1) "
            InsertOrUpdateRecord(dayCloseSummaryCashInsert)

            Dim totalChequeAmount As Decimal = request.ChequeDetailsList.Sum(Function(item) item.Amount)
            Dim dayCloseSummaryChequeInsert As String = "Insert Into DayCloseBankSummary (SiteCode , CloseDate,TenderType ,TotalAmount ,CREATEDAT , " & _
                                                    "CREATEDBY , CREATEDON ,UPDATEDAT , UPDATEDBY , UPDATEDON,STATUS) values " & _
                                                    "('" & request.SiteCode & "','" & request.DayCloseDate.ToString("yyyy-MM-dd") & "','" & TenderHeadCodes.Cheque.ToString() & "'," & totalChequeAmount & ",'" & request.CreatedAt & "', " & _
                                                    "'" & request.CreatedBy & "','" & GetServerDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "','" & request.UpdatedAt & "','" & request.UpdatedBy & "','" & GetServerDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "',1) "
            InsertOrUpdateRecord(dayCloseSummaryChequeInsert)

            If request.IsPettyCashApplicable Then
                Dim expensesmryRequest As New GetVoucherBalanceRequest
                expensesmryRequest.DayOpenDate = request.DayCloseDate
                expensesmryRequest.FinYear = request.FinYear
                expensesmryRequest.SiteCode = request.SiteCode
                Dim totalSales As Decimal = 0
                Dim totalExpense As Decimal = objPettyCash.GetTotalPettyCashExpense(expensesmryRequest)
                Dim totalReceipt As Decimal = objPettyCash.GetTotalPettyCashReceipt(expensesmryRequest)
                Dim openingBalance As Decimal = objPettyCash.GetPettyCashOpeningBalance(expensesmryRequest)
                If request.IsPettyCashOnSalesTerminal Then
                    totalSales = totalCashAmount - (openingBalance + totalReceipt - totalExpense)
                Else
                    If request.AddTotalSalesToPettyCash Then
                        totalSales = totalCashAmount
                    End If
                End If
                'Dim closingBalance As Decimal = openingBalance + totalReceipt + totalSales - totalExpense
                Dim closingBalance As Decimal = totalCashAmount
                Dim expenseSummaryUpdate As String = "update ExpenseSummary Set TotalExpense = " & totalExpense & ",TotalIncome = " & totalReceipt & ", " & _
                                                     "TotalSales = 0 , ClosingBalance = " & closingBalance & "" & _
                                                     " Where Sitecode = '" & request.SiteCode & "' and FinYear = '" & request.FinYear & "' and Date = '" & request.DayCloseDate.ToString("yyyy-MM-dd") & "'"
                InsertOrUpdateRecord(expenseSummaryUpdate)
            End If
            If request.IsPDFGenerate = True Then
                Dim objReport As New DayCloseReportController
                objReport.GenerateDayCloseReport(New DayCloseReportModel With {.SiteCode = request.SiteCode, .ToDate = request.DayCloseDate}, reportPath)
            End If

            result = True
            Return result
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function CheckIfAllTerminalAreClosed(ByVal siteCode As String) As Boolean Implements IDayCloseBankDetails.CheckIfAllTerminalAreClosed
        Try
            Dim result As Boolean
            Dim query As String = "Select * from dbo.MstTerminalID where SiteCode = '" & siteCode & "' and OpenCloseStatus = 'Open' AND Status = 1"
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows = False Then
                    result = True
                End If
            End Using
            Return result
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Function ResetGLNoRangeObjects(obj As String, SiteCode As String) As Boolean Implements IDayCloseBankDetails.ResetGLNoRangeObjects
        Try
            Dim DocNo As String = ""
            Dim dt As New DataTable
            Dim daDoc As New SqlDataAdapter("SELECT OBJECTID,OBJECTTYPEID FROM GLNORANGEOBJECTSM WHERE OBJECTNAME='" & obj & "'", ConString)
            daDoc.Fill(dt)
            If dt.Rows.Count > 0 Then
                Dim objTypeId, objId As String
                objTypeId = dt.Rows(0)("OBJECTTYPEID")
                objId = dt.Rows(0)("OBJECTID")
                Dim UpdateQuery = " UPDATE GLNORANGEOBJECTS  SET CurrentNo = 1 WHERE OBJECTTYPEID='" & objTypeId & "' AND OBJECTID='" & objId & "' AND SiteCode='" & SiteCode & "'"
                InsertOrUpdateRecord(UpdateQuery)
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return ""
        End Try
    End Function
    Function SaveDayCloseArticleStockBalance(ByVal DayCloseDate As Date, SiteCode As String, UserCode As String) As Boolean Implements IDayCloseBankDetails.SaveDayCloseArticleStockBalance
        Try
            Dim result As Boolean = False
            Dim insertQuery As New SqlCommand()
            insertQuery.CommandText = " Insert Into ArticleStockDayCloseBalances ( " & _
                        " 			DayCloseDate,SiteCode, ArticleCode, EAN, UOM, PhysicalQty, ReservedQty, DamagedQty, " & _
                        " 		    OnOrderQty, InTrasnsitQty, NonSaleableQty, TotalPhysicalSaleableQty, TotalPhysicalNonSaleableQty,  " & _
                        "             TotalVirtualNonSaleableQty, TotalSaleableQty, TotalARSQty, CREATEDAT, CREATEDBY, CREATEDON,  " & _
                        "             UPDATEDAT, UPDATEDBY, UPDATEDON, STATUS, StockStatus, NextAvailableDate) " & _
                        " SELECT   @DayCloseDate, SiteCode, ArticleCode, EAN, UOM, PhysicalQty, ReservedQty, DamagedQty,  " & _
                        " 		    OnOrderQty, InTrasnsitQty, NonSaleableQty, TotalPhysicalSaleableQty, TotalPhysicalNonSaleableQty,  " & _
                        "             TotalVirtualNonSaleableQty, TotalSaleableQty, TotalARSQty, @CREATEDAT, @CREATEDBY, getDate(),  " & _
                        "             @CREATEDAT, @CREATEDBY,  getDate(), STATUS, StockStatus, NextAvailableDate " & _
                        " FROM         ArticleStockBalances "

            insertQuery.Parameters.AddWithValue("@DayCloseDate", DayCloseDate)
            insertQuery.Parameters.AddWithValue("@CREATEDAT", SiteCode)
            insertQuery.Parameters.AddWithValue("@CREATEDBY", UserCode)
            InsertOrUpdateRecord(insertQuery)
            result = True

            Return result
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    'Private Sub GenerateDayCloseReport(ByRef dt As DataTable)
    '    If dt Is Nothing OrElse dt.Rows.Count = 0 Then
    '        Exit Sub
    '    End If
    '    Dim xlApp As Excel.Application
    '    Try
    '        Dim xlWorkBook As Excel.Workbook
    '        Dim xlWorkSheet As Excel.Worksheet
    '        Dim misValue As Object = System.Reflection.Missing.Value

    '        xlApp = New Excel.ApplicationClass
    '        xlWorkBook = xlApp.Workbooks.Add(misValue)
    '        xlWorkBook.Password = "admin"
    '        xlWorkSheet = xlWorkBook.Sheets("sheet1")
    '        xlWorkSheet.Name = "Day Close Report"
    '        xlWorkSheet.Cells(1, 1) = "Tender Type"
    '        xlWorkSheet.Cells(1, 2) = "Amount"
    '        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
    '            xlWorkSheet.Cells(i + 2, 1) = dt.Rows(i)("DESCRIPTION")
    '            xlWorkSheet.Cells(i + 2, 2) = dt.Rows(i)("AMTTEN")
    '        Next
    '        xlWorkSheet.SaveAs("D:\DayCloseReport.xlsx")
    '        System.Diagnostics.Process.Start("D:\DayCloseReport.xlsx")
    '        releaseObject(xlApp)
    '        releaseObject(xlWorkBook)
    '        releaseObject(xlWorkSheet)
    '    Catch ex As Exception
    '        LogException(ex)
    '    Finally
    '        'xlApp.Quit()
    '        CloseConnection()
    '    End Try
    'End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            'GC.Collect()
        End Try
    End Sub
End Class
