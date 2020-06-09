Imports System.Data
Imports System.Data.SqlClient
Imports SpectrumCommon
Imports System.Text

Public Class clsQuotation
    Inherits clsCommon
    Dim SqlTrans As SqlTransaction
    Dim stmtSalesStatus As String = "('Cancel','Closed','Return')"
    Public Sub New()

    End Sub

    Public Function GetSOTableStruct(ByVal vSiteCode As String, ByVal vSalesNo As String, Optional ByVal vStatus As String = "") As DataSet
        Try
            Dim vStmtQry As New StringBuilder
            Dim Sqlda As SqlDataAdapter
            Dim Sqlcmdb As SqlCommandBuilder
            Dim Sqlds As DataSet
            vStmtQry.Length = 0
            vStmtQry.Append(" Select * From QuotationHdr Where SiteCode='" & vSiteCode & "' " & vbCrLf)
            'If vStatus = "Cancel" Then
            '    vStmtQry.Append(" And SOStatus Not In " & stmtSalesStatus & vbCrLf)
            'End If
            vStmtQry.Append(" And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            vStmtQry.Append(" Select A.*,B.FreezeSB, B.FreezeSR,B.FreezeOB,0.0 as Reserved_Qty,0.0 as Delivered_Qty From QuotationDtl A Inner join SalesInfoRecord B on A.SiteCode=B.SiteCode AND A.EAN=B.EAN AND A.ArticleCode=B.ArticleCode AND B.Srno=1 Where isnull(A.ArticleStatus,'')<>'Deleted' AND isnull(A.Status,0)<>0 AND A.SiteCode='" & vSiteCode & "' " & vbCrLf)
            'If vStatus = "Cancel" Then
            '    vStmtQry.Append(" And ArticleStatus Not In " & stmtSalesStatus & vbCrLf)
            'End If
            vStmtQry.Append(" And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            vStmtQry.Append(" Select * From SalesOrderTaxDtls Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            vStmtQry.Append(" Select * From QuotationOtherCharges Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Sqlds.Tables(0).TableName = "QuotationHdr"
            Sqlds.Tables(1).TableName = "QuotationDtl"
            Sqlds.Tables(2).TableName = "SalesOrderTaxDtls"
            Sqlds.Tables(3).TableName = "QuotationOtherCharges"

            Dim KeySOhdr(2) As DataColumn
            KeySOhdr(0) = Sqlds.Tables("QuotationHdr").Columns("SiteCode")
            KeySOhdr(1) = Sqlds.Tables("QuotationHdr").Columns("FinYear")
            KeySOhdr(2) = Sqlds.Tables("QuotationHdr").Columns("SaleOrderNumber")
            Sqlds.Tables("QuotationHdr").PrimaryKey = KeySOhdr

            Dim KeySOdtl(4) As DataColumn
            KeySOdtl(0) = Sqlds.Tables("QuotationDtl").Columns("SiteCode")
            KeySOdtl(1) = Sqlds.Tables("QuotationDtl").Columns("FinYear")
            KeySOdtl(2) = Sqlds.Tables("QuotationDtl").Columns("SaleOrderNumber")
            KeySOdtl(3) = Sqlds.Tables("QuotationDtl").Columns("EAN")
            KeySOdtl(4) = Sqlds.Tables("QuotationDtl").Columns("BillLineNo")
            Sqlds.Tables("QuotationDtl").PrimaryKey = KeySOdtl

            Dim KeySOTax(4) As DataColumn
            KeySOTax(0) = Sqlds.Tables("SalesOrderTaxDtls").Columns("SiteCode")
            KeySOTax(1) = Sqlds.Tables("SalesOrderTaxDtls").Columns("FinYear")
            KeySOTax(2) = Sqlds.Tables("SalesOrderTaxDtls").Columns("SaleOrderNumber")
            KeySOTax(3) = Sqlds.Tables("SalesOrderTaxDtls").Columns("EAN")
            KeySOTax(4) = Sqlds.Tables("SalesOrderTaxDtls").Columns("TaxLineNo")
            Sqlds.Tables("SalesOrderTaxDtls").PrimaryKey = KeySOTax

            Dim KeySOCharges(3) As DataColumn
            KeySOCharges(0) = Sqlds.Tables("QuotationOtherCharges").Columns("SiteCode")
            KeySOCharges(1) = Sqlds.Tables("QuotationOtherCharges").Columns("FinYear")
            KeySOCharges(2) = Sqlds.Tables("QuotationOtherCharges").Columns("SaleOrderNumber")
            KeySOCharges(3) = Sqlds.Tables("QuotationOtherCharges").Columns("SerailNo")
            Sqlds.Tables("QuotationOtherCharges").PrimaryKey = KeySOCharges


            Return Sqlds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function PrepareSaveData(ByVal currentSalesinvoice As String, ByVal DayOpendate As DateTime, ByVal ClpProgramId As String, ByVal CLPCustomerId As String, ByRef dsSOMain As DataSet, ByVal IsNextSalesNo As Boolean, ByVal IsNextInvoiceNo As Boolean, ByVal vSiteCode As String, ByVal vSalesNo As String, ByVal Storage As String, ByVal CVProgram As String, ByVal DocType As String, ByVal FinYear As String, ByVal UserId As String, ByVal ServerDate As DateTime, ByVal IsOBCreated As Boolean, Optional ByRef Voucherno As String = "", Optional ByRef VoucherDays As Int32 = 0, Optional ByRef isPromoApplied As Boolean = False, Optional ByVal DtDeletedData As DataTable = Nothing, Optional ByVal DeliveryLocInfo As List(Of SODeliveryLocationInfo) = Nothing) As Boolean
        Try
            Dim objComm As New clsCommon
            Dim disc As Double
            'If isPromoApplied = True Then

            '    Dim strSql As String = " DELETE FROM SALESDISCDTL WHERE SiteCode = '" & vSiteCode & "' AND FinYear = '" & FinYear & "' AND BillNo = '" & vSalesNo & "'"
            '    OpenConnection()
            '    Dim cmd As New SqlCommand(strSql, SpectrumCon)
            '    cmd.ExecuteNonQuery()

            '    disc = IIf(dsSOMain.Tables("SalesOrderDTL").Compute("Sum(DiscountAmount)", "") Is DBNull.Value, 0, dsSOMain.Tables("SalesOrderDTL").Compute("Sum(DiscountAmount)", ""))
            '    If disc > 0 Then
            '        Dim dtDtl As DataTable = dsSOMain.Tables("SalesOrderDTL").Copy()

            '        Dim dtDisc As DataTable = objComm.CreateDiscSummary(DayOpendate, dtDtl, "", "SO201", vSiteCode, FinYear, vSalesNo, UserId, ServerDate, "FIRSTLEVEL", "TOPLEVEL")
            '        If Not dtDisc Is Nothing AndAlso dtDisc.Rows.Count > 0 Then
            '            If dsSOMain.Tables.Contains("salesdiscdtl") Then
            '                dsSOMain.Tables.Remove("salesdiscdtl")
            '                dsSOMain.Tables.Add(dtDisc)
            '            Else
            '                dsSOMain.Tables.Add(dtDisc)
            '            End If
            '            'dsSOMain.AcceptChanges()
            '        End If
            '    End If
            'End If
            Dim dtTempReserved As DataSet = dsSOMain.Copy()
            If dtTempReserved.Tables("QuotationDtl").Columns.Contains("Reserved_Qty") Then
                dtTempReserved.Tables("QuotationDtl").Columns.Remove("Reserved_Qty")
            End If
            If dtTempReserved.Tables("QuotationDtl").Columns.Contains("Delivered_Qty") Then
                dtTempReserved.Tables("QuotationDtl").Columns.Remove("Delivered_Qty")
            End If
            'B.FreezeSB, B.FreezeSR,B.FreezeOB
            If dtTempReserved.Tables("QuotationDtl").Columns.Contains("FreezeSB") Then
                dtTempReserved.Tables("QuotationDtl").Columns.Remove("FreezeSB")
            End If
            If dtTempReserved.Tables("QuotationDtl").Columns.Contains("FreezeSR") Then
                dtTempReserved.Tables("QuotationDtl").Columns.Remove("FreezeSR")
            End If
            If dtTempReserved.Tables("QuotationDtl").Columns.Contains("FreezeOB") Then
                dtTempReserved.Tables("QuotationDtl").Columns.Remove("FreezeOB")
            End If
            OpenConnection()
            SqlTrans = SpectrumCon.BeginTransaction()
            'If Not SaveDiscount(dsSOMain.Tables("SalesOrderDTL"), vSalesNo, SqlTrans) Then
            '    SqlTrans.Rollback()
            '    CloseConnection()
            '    Return False
            'End If
            'If dtTempReserved.Tables.Contains("SalesOrderTaxDtls") Then
            '    If dtTempReserved.Tables("SalesOrderTaxDtls").Rows.Count > 0 Then
            '        For Each row As DataRow In dtTempReserved.Tables("SalesOrderTaxDtls").Rows
            '            If row.RowState <> DataRowState.Deleted Then
            '                row("CreatedOn") = ServerDate
            '                row("Updatedon") = ServerDate
            '                row("CreatedBy") = UserId
            '                row("Updatedby") = UserId
            '                row("createdAt") = vSiteCode
            '                row("UpdatedAt") = vSiteCode
            '                row("Status") = 1
            '                row("DocumentType") = "SalesOrder"
            '            End If
            '        Next
            '    End If
            'End If
            dtTempReserved.Tables.Remove("SalesOrderTaxDtls")
            If objComm.SaveData(dtTempReserved, SpectrumCon, SqlTrans) = True Then
                'If DeliveryLocInfo IsNot Nothing AndAlso DeliveryLocInfo.Count > 0 Then
                '    Dim query As String
                '    For Each item In DeliveryLocInfo
                '        If item.IsNew Then
                '            query = "insert into SalesOrderDeliveryLocInfo (SalesOrderNumber, SiteCode, DeliverySiteCode, ArticleCode, Quantity, DeliveredQty,ReservedQty,Amount, " & _
                '                          "CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON, STATUS) " & _
                '                          "values ('" & item.SalesOrderNumber & "','" & item.SiteCode & "','" & item.DeliverySiteCode & "','" & item.ArticleCode & "'," & item.Quantity & ", " & item.DeliveredQuantity & " , " & item.ReservedQuantity & " , " & item.Amount & ", " & _
                '                          "'" & item.CreatedAt & "','" & item.CreatedBy & "', getdate(), '" & item.CreatedAt & "','" & item.CreatedBy & "',getdate(),1)"
                '            If objComm.InsertOrUpdateRecord(query, SqlTrans) = False Then
                '                SqlTrans.Rollback()
                '                CloseConnection()
                '                Return False
                '            End If
                '        ElseIf item.IsDirty Then
                '            query = "update SalesOrderDeliveryLocInfo set Quantity = " & item.Quantity & " , STATUS = '" & item.Status & "' where SalesOrderNumber = '" & item.SalesOrderNumber & "' and SiteCode = '" & item.SiteCode & "' " & _
                '                    "and  DeliverySiteCode = '" & item.DeliverySiteCode & "' and  ArticleCode = '" & item.ArticleCode & "' "
                '            If objComm.InsertOrUpdateRecord(query, SqlTrans) = False Then
                '                SqlTrans.Rollback()
                '                CloseConnection()
                '                Return False
                '            End If
                '        End If
                '    Next
                'End If
                'For Each dr As DataRow In dsSOMain.Tables("SalesOrderDTL").Rows
                '    If dr.RowState <> DataRowState.Deleted Then
                '        Dim reserveQty As Decimal = IIf(dr("Reserved_Qty") Is DBNull.Value, 0, dr("Reserved_Qty"))
                '        'If DeliveryLocInfo IsNot Nothing AndAlso DeliveryLocInfo.Count > 0 AndAlso reserveQty > 0 Then
                '        '    Dim otherSiteDeliveryQty As Decimal = DeliveryLocInfo.Sum(Function(x) IIf(x.ArticleCode = dr("ArticleCode").ToString(), x.Quantity, 0))
                '        '    reserveQty = reserveQty - otherSiteDeliveryQty
                '        'End If
                '        If objComm.UpdateStock(dr("SiteCode").ToString, dr("ArticleCode").ToString(), dr("EAN").ToString(), dr("UnitofMeasure").ToString(), IIf(IsDBNull(dr("Delivered_Qty")), 0, dr("Delivered_Qty").ToString()), dr("CreatedAt"), SpectrumCon, SqlTrans, Storage, reserveQty) = False Then
                '            SqlTrans.Rollback()
                '            CloseConnection()
                '            Return False
                '        End If
                '    End If

                'Next
                'For Each dr As DataRow In dsSOMain.Tables("SalesOrderDTL").Rows
                '    If IsDBNull(dr("BatchBarcode")) = False AndAlso String.IsNullOrEmpty(dr("BatchBarcode")) = False Then
                '        If objComn.UpdateBatchDtlQtyAllocated(dr("SiteCode").ToString, dr("BatchBarcode").ToString(), dr("QUANTITY").ToString(), SqlTrans) = False Then
                '            SqlTrans.Rollback()
                '            CloseConnection()
                '            Return False
                '        End If
                '    End If
                'Next
                '-----deleted data code for reversing reserve Qty---------
                'If Not DtDeletedData Is Nothing AndAlso DtDeletedData.Rows.Count > 0 Then
                '    For Each dr As DataRow In DtDeletedData.Rows
                '        If dr.RowState <> DataRowState.Deleted Then
                '            If objComm.UpdateStock(vSiteCode, dr("ArticleCode").ToString(), dr("EAN").ToString(), "", 0, UserId, SpectrumCon, SqlTrans, Storage, IIf(dr("ReservedQty") = False, 0, dr("Quantity") * -1)) = False Then
                '                SqlTrans.Rollback()
                '                CloseConnection()
                '                Return False
                '            End If
                '        End If
                '    Next
                '    DtDeletedData = Nothing
                'End If
                '---------end here for reserve-----------
                'If IsOBCreated = True Then
                '    If UpdateDocumentNo("OutBound", SpectrumCon, SqlTrans) = False Then
                '        SqlTrans.Rollback()
                '        CloseConnection()
                '        Return False
                '    End If
                'End If

                ' Add for insert for CV by ram  in ver 1.0.10.7 . dataviewrowstate.add is add because in edit sales order it is pickup old records.
                'current Sales Invoice Number
                'Dim strCurrSaleInvNbr As String = ""
                'If dtTempReserved.Tables("SalesInvoice").Rows.Count > 0 Then
                '    strCurrSaleInvNbr = currentSalesinvoice 'dtTempReserved.Tables("SalesInvoice").Compute("MAX(SALEINVNUMBER)", "")
                'End If



                'Dim dvCreditVoucher As New DataView(dtTempReserved.Tables("SalesInvoice"), " SALEINVNUMBER='" & strCurrSaleInvNbr & "' AND TenderTypeCode Like 'CreditVouc%'", "", DataViewRowState.CurrentRows)
                'Dim RedimCVExpDay As Integer = 0
                'If dvCreditVoucher.Count > 0 Then
                '    For Each drView As DataRowView In dvCreditVoucher

                '        '' Issue CV against partial redeemation should have expirydate same as orignal CV
                '        'If dvCreditVoucher.Count > 1 Then
                '        '    Dim dvRedimCV As New DataView(dtTempReserved.Tables("SalesInvoice"), " SALEINVNUMBER='" & strCurrSaleInvNbr & "' AND TenderTypeCode = 'CreditVouc(R)'", "", DataViewRowState.CurrentRows)
                '        '    If dvRedimCV.Count > 0 Then
                '        '        If Not IsDBNull(dvRedimCV(0).Item("RefDate")) Then
                '        '            RedimCVExpDay = DateDiff(DateInterval.Day, dsSOMain.Tables("SalesOrderHdr").Rows(0)("SiteCode"), drView("SOInvDate"), dvRedimCV(0).Item("RefDate"))
                '        '        End If
                '        '    End If
                '        'End If
                '        '' Issue CV against partial redeemation should have expirydate same as orignal CV

                '        If drView("TenderTypeCode") = "CreditVouc(I)" Then
                '            ' this is to get the old expiry date of partial redeem CV
                '            'If dvCreditVoucher.Count > 1 Then
                '            If Not IsDBNull(drView("RefDate")) Then
                '                VoucherDays = DateDiff(DateInterval.Day, ServerDate.Date, CDate(drView("RefDate")).Date)
                '                VoucherDays = VoucherDays
                '            End If
                '            'End If
                '            ' this is to get the old expiry date of partial redeem CV

                '            If objCM.UpdateCreditVoucher(CVProgram, DocType, True, drView("DocumentNumber").ToString(), dsSOMain.Tables("SalesOrderHdr").Rows(0)("SiteCode"), drView("SOInvDate"), dsSOMain.Tables("SalesOrderHdr").Rows(0)("CreatedBy"), SqlTrans, SpectrumCon, drView("AmountTendered"), Voucherno, VoucherDays) = False Then
                '                SqlTrans.Rollback()
                '                CloseConnection()
                '                Return False
                '            End If
                '        ElseIf drView("TenderTypeCode") = "CreditVouc(R)" Then
                '            If objCM.UpdateCreditVoucher(CVProgram, DocType, False, drView("DocumentNumber").ToString(), dsSOMain.Tables("SalesOrderHdr").Rows(0)("SiteCode"), drView("SOInvDate"), dsSOMain.Tables("SalesOrderHdr").Rows(0)("CreatedBy"), SqlTrans, SpectrumCon, 0, drView("RefNO_2").ToString()) = False Then
                '                SqlTrans.Rollback()
                '                CloseConnection()
                '                Return False
                '            End If
                '        End If
                '    Next
                'End If

                ' Add for insert for GV by ram  in ver 1.0.9.2 
                'Dim dvGiftVoucher As New DataView(dtTempReserved.Tables("SalesInvoice"), " SALEINVNUMBER='" & strCurrSaleInvNbr & "' AND TenderTypeCode Like 'GiftVouch%'", "", DataViewRowState.CurrentRows)
                'Dim RedimGVExpDay As Integer = 0
                'If dvGiftVoucher.Count > 0 Then
                '    For Each drView As DataRowView In dvGiftVoucher
                '        ' Issue GV against partial redeemation should have expirydate same as orignal GV
                '        'If dvGiftVoucher.Count > 1 Then
                '        '    Dim dvRedimGV As New DataView(dtTempReserved.Tables("SalesInvoice"), " SALEINVNUMBER='" & strCurrSaleInvNbr & "' AND TenderTypeCode = 'GiftVoucher(R)'", "", DataViewRowState.CurrentRows)
                '        '    If dvRedimGV.Count > 0 Then
                '        '        If Not IsDBNull(dvRedimGV(0).Item("RefDate")) Then
                '        '            RedimGVExpDay = DateDiff(DateInterval.Day, Now, dvRedimGV(0).Item("RefDate"))
                '        '        End If
                '        '    End If
                '        'End If
                '        '' Issue GV against partial redeemation should have expirydate same as orignal GV

                '        If drView("TenderTypeCode") = "GiftVoucher(I)" Then
                '            ' this is to get the old expiry date of partial redeem GV
                '            'If dvGiftVoucher.Count > 1 Then
                '            If Not IsDBNull(drView("RefDate")) Then
                '                VoucherDays = DateDiff(DateInterval.Day, ServerDate.Date, CDate(drView("RefDate")).Date)
                '                VoucherDays = VoucherDays
                '            End If
                '            'End If
                '            ' this is to get the old expiry date of partial redeem GV

                '            If objCM.UpdateCreditVoucher(drView("RefNo_2"), DocType, True, drView("DocumentNumber").ToString(), dsSOMain.Tables("SalesOrderHdr").Rows(0)("SiteCode"), drView("SOInvDate"), dsSOMain.Tables("SalesOrderHdr").Rows(0)("CreatedBy"), SqlTrans, SpectrumCon, drView("AmountTendered"), Voucherno, VoucherDays, "GV") = False Then
                '                SqlTrans.Rollback()
                '                CloseConnection()
                '                Return False
                '            End If
                '        ElseIf drView("TenderTypeCode") = "GiftVoucher(R)" Then
                '            If objCM.UpdateCreditVoucher(CVProgram, DocType, False, drView("DocumentNumber").ToString(), dsSOMain.Tables("SalesOrderHdr").Rows(0)("SiteCode"), drView("SOInvDate"), dsSOMain.Tables("SalesOrderHdr").Rows(0)("CreatedBy"), SqlTrans, SpectrumCon, 0, drView("RefNO_2").ToString(), 0, "GV") = False Then
                '                SqlTrans.Rollback()
                '                CloseConnection()
                '                Return False
                '            End If
                '        End If
                '    Next
                'End If

                ' Add for insert for GV by ram  in ver 1.0.9.2 
                'Dim dvCLP As New DataView(dtTempReserved.Tables("SalesInvoice"), "TenderTypeCode Like 'CLP%'", "", DataViewRowState.CurrentRows)
                'If dvCLP.Count > 0 Then
                '    For Each CLpRow As DataRowView In dvCreditVoucher
                '        Dim TotalPoints As Integer = CLpRow("AmountTendered")
                '        'CLPRedemptionPoints = TotalPoints
                '        'TotalPoints = TotalPoints * -1
                '        If objComm.UpdateClpPoints(False, ClpProgramId, CLPCustomerId, TotalPoints, SpectrumCon, SqlTrans, dsSOMain.Tables("SalesOrderHdr").Rows(0)("SiteCode"), UserId, vSalesNo, ServerDate, False) = False Then
                '            SqlTrans.Rollback()
                '            CloseConnection()
                '            Return False
                '        End If
                '    Next
                'End If


                If IsNextSalesNo = True Then
                    If UpdateDocumentNo("Quotation", SpectrumCon, SqlTrans) = False Then
                        SqlTrans.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If

                'If IsNextInvoiceNo = True Then
                '    If objComn.UpdateDocumentNo("CM", SpectrumCon, SqlTrans) = False Then
                '        SqlTrans.Rollback()
                '        CloseConnection()
                '        Return False
                '    End If
                'End If



                SqlTrans.Commit()
                CloseConnection()

                dsSOMain.Clear()
                SqlTrans.Dispose()
                isPromoApplied = False
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
    Dim vStmtQry As New System.Text.StringBuilder
    ''' <summary>
    ''' Get quotation Information
    ''' </summary>
    ''' <returns>Sales DataSet</returns>
    ''' <UsedBy>frmSearchSO.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function GetSearchQuotation(Optional ByVal RequiredStatus As Boolean = False) As DataSet
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select Distinct sHdr.SaleOrderNumber as SaleNo,")
            '--changed by rama for bug 811,1155
            If RequiredStatus = True Then
                vStmtQry.Append(" sHdr.SOStatus AS Status,")
            End If
            '--
            'vStmtQry.Append(" dbo.FnGetDesc('Terminal',sHdr.TerminalID,sHdr.Sitecode) as TerminalID, " & vbCrLf)
            vStmtQry.Append(" Case When sHdr.CustomerType='CLP' Then clpInfo.SurName +' '+ clpInfo.FirstName " & vbCrLf)
            vStmtQry.Append(" When sHdr.CustomerType='SO' Then cInfo.CustomerName End as CustomerName, " & vbCrLf)
            vStmtQry.Append(" sHdr.CustomerNo, sHdr.CreatedOn as [Quotation Date], sHdr.PromisedDeliveryDate as DeliveryDate,  " & vbCrLf)
            vStmtQry.Append(" sHdr.CreatedBy as CashierName, sp.SalesPersonFullName as SalesPerson,sHdr.NetAmt AS Amount " & vbCrLf)
            vStmtQry.Append("  from QuotationHdr sHdr " & vbCrLf)
            'vStmtQry.Append(" Left Join SalesInvoice sInvc on sHdr.SaleOrderNumber=sInvc.DocumentNumber " & vbCrLf)
            vStmtQry.Append(" Left Join CustomerSaleOrder cInfo on sHdr.CustomerNo=cInfo.CustomerNo " & vbCrLf)
            vStmtQry.Append(" Left Join CLPCustomers clpInfo on sHdr.CustomerNo=clpInfo.CardNo " & vbCrLf)
            vStmtQry.Append(" Left Join MstSalesPerson sp on sHdr.SalesExecutiveCode=sp.EmpCode " & vbCrLf)
            vStmtQry.Append(" Where sHdr.SOStatus Not In " & stmtSalesStatus & " Order By SaleNo Desc " & vbCrLf)

            Dim daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dsScan = New DataSet
            daScan.Fill(dsScan, "SalesOrderSearch")

            Return dsScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Search a Sales Order Information
    ''' </summary>
    ''' <param name="vSiteNo"> Site Code</param>
    ''' <param name="vSalesNo"> Sales Order Number</param>
    ''' <returns>Sales DataSet</returns>
    ''' <UsedBy>frmSalesOrderCancel.vb, frmSalesOrderUpdation.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function SetQuotationInQOCancel(ByVal vSiteNo As String, ByVal vSalesNo As String) As DataSet

        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select A.ArticleCode ,A.BillLineNo , A.BatchBarcode, dbo.FnGetEANDesc(A.ArticleCode) as Discription, A.SellingPrice, A.Quantity, 0 as PickupQty, " & vbCrLf)
            vStmtQry.Append(" A.DeliveredQty, A.DiscountPercentage,A.DiscountAmount as Discount, A.NetAmount, A.ActualDeliveryDate as ExpDelDate, " & vbCrLf)
            vStmtQry.Append(" A.ReservedQty, A.LineDiscount, 50 as Stock, A.EAN , " & vbCrLf)
            vStmtQry.Append(" A.SellingPrice*A.Quantity as GrossAmt, A.ExclTaxAmt as ExclTaxAmt,A.TotalTaxAmount, A.Status, " & vbCrLf)
            vStmtQry.Append(" A.UnitOfMeasure, A.OfferNo, A.SaleOrderNumber,A.SalesStaffID, A.CreatedOn, A.IsCLPApplicable, A.ClpPoints, A.ClpDiscount,isnull(B.FreezeSB,0)as FreezeSB,isnull(B.FreezeSR,0)as FreezeSR,isnull(B.FreezeOB,0)as FreezeOB" & vbCrLf)
            vStmtQry.Append(" from QuotationDtl  A Inner join SalesInfoRecord B on A.SiteCode=B.SiteCode AND A.EAN=B.EAN AND A.ArticleCode=B.ArticleCode AND B.Srno=1 Where  A.SiteCode='" & vSiteNo & "' and A.SaleOrderNumber='" & vSalesNo & "' " & vbCrLf)
            '-- added by rama ranjan for bug no 0000566
            vStmtQry.Append(" AND isnull(A.ArticleStatus,'')<>'Deleted' AND isnull(A.Status,0)<>0 " & vbCrLf)
            '--
            Dim daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dsScan = New DataSet
            daScan.Fill(dsScan, "ItemScanDetails")

            Return dsScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
End Class
