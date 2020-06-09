Imports System.Data
Imports System.Data.SqlClient
Imports SpectrumCommon
Imports System.Text
Public Class clsSalesOrder

    Dim objComn As New clsCommon
    Dim objCM As New clsCashMemo

    Dim SqlTrans As SqlTransaction
    Dim Sqlda, daScan As New SqlDataAdapter
    Dim Sqlcmdb As SqlCommandBuilder
    Dim Sqlcmd As SqlCommand
    Dim Sqlds, dsScan As New DataSet
    Dim Sqldr As DataRow

    Dim vStmtQry As New System.Text.StringBuilder
    Dim SalesInvcNo As String = ""
    Dim stmtSalesStatus As String = "('Cancel','Closed','Return')"
    Dim _SOCreation As String
    Public Property SOCreation() As String
        Get
            Return _SOCreation
        End Get
        Set(ByVal value As String)
            _SOCreation = value
        End Set
    End Property

    Public Shared _SelectedCurrencyIndex As String
    Public Property SelectedCurrencyIndex() As String
        Get
            SelectedCurrencyIndex = _SelectedCurrencyIndex
        End Get
        Set(value As String)
            _SelectedCurrencyIndex = value
        End Set
    End Property


    Public Shared _iCurrencyCode As String
    Public Property CurrencyCode() As String
        Get
            CurrencyCode = _iCurrencyCode
        End Get
        Set(ByVal value As String)
            _iCurrencyCode = value
        End Set

    End Property



    Dim _SOReturn As String
    Public Property SOReturn() As String
        Get
            Return _SOReturn
        End Get
        Set(ByVal value As String)
            _SOReturn = value
        End Set
    End Property

    Private _SoBulkComboHdr As DataTable
    Public Property DtSoBulkComboHdr() As DataTable
        Get
            Return _SoBulkComboHdr
        End Get
        Set(ByVal value As DataTable)
            _SoBulkComboHdr = value
        End Set
    End Property

    Private _SoBulkComboDtl As DataTable
    Public Property DtSoBulkComboDtl() As DataTable
        Get
            Return _SoBulkComboDtl
        End Get
        Set(ByVal value As DataTable)
            _SoBulkComboDtl = value
        End Set
    End Property

    Private _IsStrGenerate As Boolean
    Public Property IsStrGenerate() As String
        Get
            Return _IsStrGenerate
        End Get
        Set(ByVal value As String)
            _IsStrGenerate = value
        End Set
    End Property

    Private _IsMannualClose As Boolean
    Public Property IsMannualClose() As Boolean
        Get
            Return _IsMannualClose
        End Get
        Set(ByVal value As Boolean)
            _IsMannualClose = value
        End Set
    End Property

    ''' <summary>
    ''' Set Table Structure for Item Scanning
    ''' </summary>
    ''' <returns>DataSet</returns>
    ''' <UsedBy>frmSalesOrderCreation.vb, frmSalesOrderUpdation.vb, frmSalesOrderCancelation.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function GetCollectionOfItems() As DataSet
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select '' as DEL,  " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as EAN, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as ArticleCode, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Discription, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as SellingPrice, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as Quantity, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as PickUpQty, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as DeliveredQty, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as NetAmount, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as Discount, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as TotalDiscPercentage, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as ExclTaxAmt, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as TotalTaxAmt, " & vbCrLf)
            vStmtQry.Append(" getdate() as ExpDelDate, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Stock, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'True') as IsCLP, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'True') as ReservedQty, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'True') as Reserved, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as RowIndex, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as FreezeSB, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as FreezeOB, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as FreezeSR, " & vbCrLf)
            'vStmtQry.Append(" Convert(Varchar(50),'') as ArticleCode, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as UOM, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as GrossAmt, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as MinPayAmt, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as IncTaxAmt, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(40),'') as IsStatus, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as CLPPoints, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as CLPDiscount, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(40),'') as AuthUserId, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(200),'') as AuthUserRemarks, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(250),'') as ReturnReasonCode, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(20),'') as PromotionId, " & vbCrLf) 'PromotionId, OfferId 
            vStmtQry.Append(" Convert(numeric(18,2),0) as LineDiscount, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(40),'') as FirstLevel, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as TopLevel, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as LastNodeCode, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as FirstLevelDISC, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as TopLevelDISC, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(20),'') as SalesStaffID, " & vbCrLf)
            'change to track the pick Amt by ram 
            vStmtQry.Append(" Convert(numeric(18,2),0) as TotalPickUpAmt, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,0),0) as BillLineNo, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as BatchBarcode, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as DeliverySiteCode, " & vbCrLf)
            'change to track the pick Amt
            vStmtQry.Append(" Convert(Varchar(35),'') as EditBarcode, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(85),'') as ArticleDiscription," & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as CostAmount " & vbCrLf)

            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dsScan = New DataSet
            daScan.Fill(dsScan, "ItemScanDetails")

            Return dsScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function



    ''' <summary>
    ''' Get Sales Order Table Structure Information
    ''' </summary>
    ''' <param name="vSiteCode">SiteCode</param>
    ''' <param name="vSalesNo">SalesNo</param>
    ''' <param name="vStatus">Insertion/Updation/Cancelation</param>
    ''' <returns>DataSet</returns>
    ''' <UsedBy>frmSalesOrderCreation.vb, frmSalesOrderUpdation.vb, frmSalesOrderCancelation.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function GetSOTableStruct(ByVal vSiteCode As String, ByVal vSalesNo As String, Optional ByVal vStatus As String = "", Optional ByVal IsQuotation As Boolean = False, Optional ByVal QuotationNumber As String = "") As DataSet
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select * From SalesOrderHDR Where SiteCode='" & vSiteCode & "' " & vbCrLf)
            'If vStatus = "Cancel" Then
            '    vStmtQry.Append(" And SOStatus Not In " & stmtSalesStatus & vbCrLf)
            'End If
            vStmtQry.Append(" And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            vStmtQry.Append(" Select A.*,B.FreezeSB, B.FreezeSR,B.FreezeOB,0.0 as Reserved_Qty,0.0 as Delivered_Qty From SalesOrderDTL A Inner join SalesInfoRecord B on A.SiteCode=B.SiteCode AND A.EAN=B.EAN AND A.ArticleCode=B.ArticleCode AND B.Srno=1 Where isnull(A.ArticleStatus,'')<>'Deleted' AND isnull(A.Status,0)<>0 AND A.SiteCode='" & vSiteCode & "' " & vbCrLf)
            'If vStatus = "Cancel" Then
            '    vStmtQry.Append(" And ArticleStatus Not In " & stmtSalesStatus & vbCrLf)
            'End If
            vStmtQry.Append(" And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            vStmtQry.Append(" Select * From SalesOrderHDRAudit Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SalesOrderDTLAudit Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            vStmtQry.Append(" Select * From SalesInvoice Where status =1 and SiteCode='" & vSiteCode & " ' And DocumentNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SalesOrderTaxDtls Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SalesOrderOtherCharges Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            vStmtQry.Append(" Select * From OrderHdr Where SiteCode='" & vSiteCode & " ' And DocumentNumber='0'; " & vbCrLf)
            vStmtQry.Append(" Select * From OrderDtl Where SiteCode='" & vSiteCode & " ' And DocumentNumber='0'; " & vbCrLf)

            vStmtQry.Append(" Select * from CLPTransaction Where SiteCode='0' And BillNo='0'; " & vbCrLf)
            vStmtQry.Append(" Select * from CLPTransactionsDetails Where SiteCode='0' And BillNo='0' And BillLineNo='0'; " & vbCrLf)

            If IsQuotation AndAlso QuotationNumber <> "" Then
                vStmtQry.Append(" select * from Quotationhdr where SaleOrderNumber='" & QuotationNumber & "'; " & vbCrLf)
                vStmtQry.Append(" select *, b.ArticleName as Discription from Quotationdtl a inner join MSTArticle b on  a.ArticleCode= b.ArticleCode where SaleOrderNumber='" & QuotationNumber & "'; " & vbCrLf)
            End If


            vStmtQry.Append(" Select * From SoBulkComboHdr Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SoBulkComboHdr as BCH Inner Join SoBulkComboDtl as BCD on  BCH.BulkComboMstId = BCD.BulkComboMstId  Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Sqlds.Tables(0).TableName = "SalesOrderHDR"
            Sqlds.Tables(1).TableName = "SalesOrderDTL"
            Sqlds.Tables(2).TableName = "SalesOrderHDRAudit"
            Sqlds.Tables(3).TableName = "SalesOrderDTLAudit"
            Sqlds.Tables(4).TableName = "SalesInvoice"
            Sqlds.Tables(5).TableName = "SalesOrderTaxDtls"
            Sqlds.Tables(6).TableName = "SalesOrderOtherCharges"
            Sqlds.Tables(7).TableName = "OrderHdr"
            Sqlds.Tables(8).TableName = "OrderDtl"
            Sqlds.Tables(9).TableName = "CLPTransaction"
            Sqlds.Tables(10).TableName = "CLPTransactionsDetails"
            If IsQuotation AndAlso QuotationNumber <> "" Then
                Sqlds.Tables(11).TableName = "Quotationhdr"
                Sqlds.Tables(12).TableName = "Quotationdtl"
            End If

            Dim KeySOhdr(2) As DataColumn
            KeySOhdr(0) = Sqlds.Tables("SalesOrderHDR").Columns("SiteCode")
            KeySOhdr(1) = Sqlds.Tables("SalesOrderHDR").Columns("FinYear")
            KeySOhdr(2) = Sqlds.Tables("SalesOrderHDR").Columns("SaleOrderNumber")
            Sqlds.Tables("SalesOrderHDR").PrimaryKey = KeySOhdr

            Dim KeySOdtl(4) As DataColumn
            KeySOdtl(0) = Sqlds.Tables("SalesOrderDTL").Columns("SiteCode")
            KeySOdtl(1) = Sqlds.Tables("SalesOrderDTL").Columns("FinYear")
            KeySOdtl(2) = Sqlds.Tables("SalesOrderDTL").Columns("SaleOrderNumber")
            KeySOdtl(3) = Sqlds.Tables("SalesOrderDTL").Columns("EAN")
            KeySOdtl(4) = Sqlds.Tables("SalesOrderDTL").Columns("BillLineNo")
            Sqlds.Tables("SalesOrderDTL").PrimaryKey = KeySOdtl

            Dim KeySOhdrAudit(3) As DataColumn
            KeySOhdrAudit(0) = Sqlds.Tables("SalesOrderHDRAudit").Columns("SiteCode")
            KeySOhdrAudit(1) = Sqlds.Tables("SalesOrderHDRAudit").Columns("FinYear")
            KeySOhdrAudit(2) = Sqlds.Tables("SalesOrderHDRAudit").Columns("SaleOrderNumber")
            KeySOhdrAudit(3) = Sqlds.Tables("SalesOrderHDRAudit").Columns("AmendedNo")
            Sqlds.Tables("SalesOrderHDRAudit").PrimaryKey = KeySOhdrAudit

            Dim KeySOdtlAudit(4) As DataColumn
            KeySOdtlAudit(0) = Sqlds.Tables("SalesOrderDTLAudit").Columns("SiteCode")
            KeySOdtlAudit(1) = Sqlds.Tables("SalesOrderDTLAudit").Columns("FinYear")
            KeySOdtlAudit(2) = Sqlds.Tables("SalesOrderDTLAudit").Columns("SaleOrderNumber")
            KeySOdtlAudit(3) = Sqlds.Tables("SalesOrderDTLAudit").Columns("EAN")
            KeySOdtlAudit(4) = Sqlds.Tables("SalesOrderDTLAudit").Columns("AmendedNo")
            Sqlds.Tables("SalesOrderDTLAudit").PrimaryKey = KeySOdtlAudit

            Dim KeySOInvc(4) As DataColumn
            KeySOInvc(0) = Sqlds.Tables("SalesInvoice").Columns("SiteCode")
            KeySOInvc(1) = Sqlds.Tables("SalesInvoice").Columns("FinYear")
            KeySOInvc(2) = Sqlds.Tables("SalesInvoice").Columns("DocumentNumber")
            KeySOInvc(3) = Sqlds.Tables("SalesInvoice").Columns("SaleInvNumber")
            KeySOInvc(4) = Sqlds.Tables("SalesInvoice").Columns("SaleInvLineNumber")
            Sqlds.Tables("SalesInvoice").PrimaryKey = KeySOInvc

            Dim KeySOTax(4) As DataColumn
            KeySOTax(0) = Sqlds.Tables("SalesOrderTaxDtls").Columns("SiteCode")
            KeySOTax(1) = Sqlds.Tables("SalesOrderTaxDtls").Columns("FinYear")
            KeySOTax(2) = Sqlds.Tables("SalesOrderTaxDtls").Columns("SaleOrderNumber")
            KeySOTax(3) = Sqlds.Tables("SalesOrderTaxDtls").Columns("EAN")
            KeySOTax(4) = Sqlds.Tables("SalesOrderTaxDtls").Columns("TaxLineNo")
            Sqlds.Tables("SalesOrderTaxDtls").PrimaryKey = KeySOTax

            Dim KeySOCharges(3) As DataColumn
            KeySOCharges(0) = Sqlds.Tables("SalesOrderOtherCharges").Columns("SiteCode")
            KeySOCharges(1) = Sqlds.Tables("SalesOrderOtherCharges").Columns("FinYear")
            KeySOCharges(2) = Sqlds.Tables("SalesOrderOtherCharges").Columns("SaleOrderNumber")
            KeySOCharges(3) = Sqlds.Tables("SalesOrderOtherCharges").Columns("SerailNo")
            Sqlds.Tables("SalesOrderOtherCharges").PrimaryKey = KeySOCharges

            Dim KeyOrderHdr(2) As DataColumn
            KeyOrderHdr(0) = Sqlds.Tables("OrderHdr").Columns("SiteCode")
            KeyOrderHdr(1) = Sqlds.Tables("OrderHdr").Columns("FinYear")
            KeyOrderHdr(2) = Sqlds.Tables("OrderHdr").Columns("DocumentNumber")
            Sqlds.Tables("OrderHdr").PrimaryKey = KeyOrderHdr

            Dim KeyOrderDtl(4) As DataColumn
            KeyOrderDtl(0) = Sqlds.Tables("OrderDtl").Columns("SiteCode")
            KeyOrderDtl(1) = Sqlds.Tables("OrderDtl").Columns("FinYear")
            KeyOrderDtl(2) = Sqlds.Tables("OrderDtl").Columns("DocumentNumber")
            KeyOrderDtl(3) = Sqlds.Tables("OrderDtl").Columns("EAN")
            KeyOrderDtl(4) = Sqlds.Tables("OrderDtl").Columns("LineNumber")
            Sqlds.Tables("OrderDtl").PrimaryKey = KeyOrderDtl

            Dim KeyCLPHdr(1) As DataColumn
            KeyCLPHdr(0) = Sqlds.Tables("CLPTransaction").Columns("SiteCode")
            KeyCLPHdr(1) = Sqlds.Tables("CLPTransaction").Columns("BillNo")
            Sqlds.Tables("CLPTransaction").PrimaryKey = KeyCLPHdr

            Dim KeyCLPDtl(2) As DataColumn
            KeyCLPDtl(0) = Sqlds.Tables("CLPTransactionsDetails").Columns("SiteCode")
            KeyCLPDtl(1) = Sqlds.Tables("CLPTransactionsDetails").Columns("BillNo")
            KeyCLPDtl(2) = Sqlds.Tables("CLPTransactionsDetails").Columns("BillLineNo")
            Sqlds.Tables("CLPTransactionsDetails").PrimaryKey = KeyCLPDtl

            If IsQuotation AndAlso QuotationNumber <> "" Then
                Dim KeyQuhdr(2) As DataColumn
                KeyQuhdr(0) = Sqlds.Tables("Quotationhdr").Columns("SiteCode")
                KeyQuhdr(1) = Sqlds.Tables("Quotationhdr").Columns("FinYear")
                KeyQuhdr(2) = Sqlds.Tables("Quotationhdr").Columns("SaleOrderNumber")
                Sqlds.Tables("Quotationhdr").PrimaryKey = KeyQuhdr

            End If

            Return Sqlds
        Catch SqlEx As SqlException
            LogException(SqlEx)
            If SqlEx.Number = -2 Then
                Throw New ApplicationException("Server is not Responding")
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetAllDocumentInfoCustmWise(ByVal pSiteCode As String, ByVal pFinYear As String, ByVal pDocumentType As String, ByVal SelectedCustomerCode As String) As DataTable
        Try
            Dim daInvoice As New SqlDataAdapter
            Dim dtInvoice As DataTable
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0
            If pDocumentType = "SalesOrder" Then
                 
                SqlQuery.Append("Select ActualDeliveryDate as BillDate, SaleOrderNumber as SalesOrderNo, LineItems as TotalItems, NetAmt as TotalAmount,AdvanceAmt as AmountPaid, " & vbCrLf)
                SqlQuery.Append(" SalesOrderHdr.AdvanceAmt - ISNULL(CreditSaleRecords.CreditAmt, 0) + ISNULL(CrdSaleAdjust.CreditAdjustmentAmt, 0) AS AmountReceived," & vbCrLf)
                SqlQuery.Append(" SalesOrderHdr.NetAmt - (SalesOrderHdr.AdvanceAmt - ISNULL(CreditSaleRecords.CreditAmt, 0) + ISNULL(CrdSaleAdjust.CreditAdjustmentAmt, 0)) AS AmountDue " & vbCrLf)
                SqlQuery.Append("FROM SalesOrderHdr LEFT OUTER JOIN (SELECT DocumentNumber, SUM(AmountTendered) AS CreditAmt FROM SalesInvoice AS SInv" & vbCrLf)
                SqlQuery.Append(" WHERE  SInv.status =1 and (TenderTypeCode = 'Credit') GROUP BY DocumentNumber) AS CreditSaleRecords ON SalesOrderHdr.SaleOrderNumber = CreditSaleRecords.DocumentNumber" & vbCrLf)
                SqlQuery.Append("LEFT OUTER JOIN (SELECT     RefBillNo AS DocumentNumber, SUM(ISNULL(AmountTendered, 0)) AS CreditAdjustmentAmt" & vbCrLf)
                SqlQuery.Append(" FROM CreditReceipt AS Cr GROUP BY RefBillNo) AS CrdSaleAdjust ON SalesOrderHdr.SaleOrderNumber = CrdSaleAdjust.DocumentNumber" & vbCrLf)
                SqlQuery.Append(" Where SiteCode='" & pSiteCode & "' And FinYear='" & pFinYear & "' and SOStatus not in('Cancel','Return') " & vbCrLf)
                SqlQuery.Append(" And CustomerNo='" & SelectedCustomerCode & "'  Order By UpdatedOn Desc" & vbCrLf)

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
    Public Function GetAllComboItemsBySalesOrder(ByVal pSiteCode As String, ByVal pSalesOrderNo As String) As DataTable
        Try
            Dim daInvoice As New SqlDataAdapter
            Dim dtInvoice As DataTable
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0









            SqlQuery.Append(" select  sbdtl.ArticleCode ,sbdtl.ArticleDescription ,sbdtl.Qty ,dtl.ItemQtyBaseUOM ,dtl.SellingPrice , dtl.NetAmount,dtl.DiscountPercentage  ,art.ArticleShortName  from " & vbCrLf)
            SqlQuery.Append(" inner join SoBulkComboDtl sbdtl on sbhdr.BulkComboMstId=sbdtl.BulkComboMstId inner join MSTArticle art on art.ArticleCode=dtl.ArticleCode " & vbCrLf)

            SqlQuery.Append(" where sbhdr.SaleOrderNumber ='" & pSalesOrderNo & "' and dtl.SiteCode='" & pSiteCode & "' " & vbCrLf)
            SqlQuery.Append(" Order By dtl.UpdatedOn Desc" & vbCrLf)




            daInvoice = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtInvoice = New DataTable
            daInvoice.Fill(dtInvoice)

            Return dtInvoice

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetStruc(ByVal CashMemo As String, ByVal SiteCode As String) As DataSet
        Try
            Dim ds As New DataSet
            Dim daCM As SqlDataAdapter
            daCM = New SqlDataAdapter(" EXEC GetSalesOrderBillStru '" & CashMemo & "','" & SiteCode & "'", SpectrumCon)
            daCM.Fill(ds)
            ds.Tables(0).TableName = "SALESORDERHDR"
            ds.Tables(1).TableName = "SALESORDERDTL"
            ds.Tables(2).TableName = "SALESORDERRECEIPT"
            ds.Tables(3).TableName = "SOBULKCOMBODTL"
            Return ds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSOInvoiceTableStruc(ByVal vSiteCode As String, ByVal vSalesNo As String) As DataSet
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select * From SalesInvoice Where status =1 and SiteCode='" & vSiteCode & " ' And DocumentNumber='" & vSalesNo & "'; " & vbCrLf)
            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)
            Sqlds.Tables(0).TableName = "SalesInvoice"
            Dim KeySOInvc(4) As DataColumn
            KeySOInvc(0) = Sqlds.Tables("SalesInvoice").Columns("SiteCode")
            KeySOInvc(1) = Sqlds.Tables("SalesInvoice").Columns("FinYear")
            KeySOInvc(2) = Sqlds.Tables("SalesInvoice").Columns("DocumentNumber")
            KeySOInvc(3) = Sqlds.Tables("SalesInvoice").Columns("SaleInvNumber")
            KeySOInvc(4) = Sqlds.Tables("SalesInvoice").Columns("SaleInvLineNumber")
            Sqlds.Tables("SalesInvoice").PrimaryKey = KeySOInvc
            Return Sqlds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Get Other Charges Table Structure Information
    ''' </summary>
    ''' <param name="sitecode">SiteCode</param>
    ''' <returns>DataTable</returns>
    ''' <UsedBy>frmSalesOrderCreation.vb, frmSalesOrderUpdation.vb, frmSalesOrderCancelation.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function GetDtOtherCharge(ByVal sitecode As String, Optional ByVal SaleOrderNumber As String = "") As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select SiteCode,finyear, SaleOrderNumber, SerailNo, ChargeName, ChargeAmount, TaxName, TaxAmt from SalesOrderOtherCharges Where SiteCode='" & sitecode & "' ")
            If Not String.IsNullOrEmpty(SaleOrderNumber) Then
                vStmtQry.Append(" AND SaleOrderNumber ='" & SaleOrderNumber & "'")
            End If
            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, ConString)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Return Sqlds.Tables(0)

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Prepare Data For Save
    ''' </summary>
    ''' <param name="dsSOMain">Sales Order DataSet</param>
    ''' <returns>True/False</returns>
    ''' <UsedBy>frmSalesOrderCreation.vb, frmSalesOrderUpdation.vb, frmSalesOrderCancelation.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function PrepareSaveData(ByVal currentSalesinvoice As String, ByVal DayOpendate As DateTime, ByVal ClpProgramId As String, ByVal CLPCustomerId As String, ByRef dsSOMain As DataSet, ByVal IsNextSalesNo As Boolean, ByVal IsNextInvoiceNo As Boolean, ByVal vSiteCode As String, ByVal vSalesNo As String, ByVal Storage As String, ByVal CVProgram As String, ByVal DocType As String, ByVal FinYear As String, ByVal UserId As String, ByVal ServerDate As DateTime, ByVal IsOBCreated As Boolean, Optional ByRef Voucherno As String = "", Optional ByRef VoucherDays As Int32 = 0, Optional ByRef isPromoApplied As Boolean = False, Optional ByVal DtDeletedData As DataTable = Nothing, Optional ByVal DeliveryLocInfo As List(Of SODeliveryLocationInfo) = Nothing, Optional ByVal IsbatchMgt As Boolean = False, Optional ByVal BatchBarcodeList As List(Of SpectrumCommon.BtachbarcodeInfo) = Nothing) As Boolean
        Try
            Dim objComm As New clsCommon
            Dim disc As Double
            If isPromoApplied = True Then

                Dim strSql As String = " DELETE FROM SALESDISCDTL WHERE SiteCode = '" & vSiteCode & "' AND FinYear = '" & FinYear & "' AND BillNo = '" & vSalesNo & "'"
                OpenConnection()
                Dim cmd As New SqlCommand(strSql, SpectrumCon)
                cmd.ExecuteNonQuery()

                disc = IIf(dsSOMain.Tables("SalesOrderDTL").Compute("Sum(DiscountAmount)", "") Is DBNull.Value, 0, dsSOMain.Tables("SalesOrderDTL").Compute("Sum(DiscountAmount)", ""))
                If disc > 0 Then
                    Dim dtDtl As DataTable = dsSOMain.Tables("SalesOrderDTL").Copy()

                    Dim dtDisc As DataTable = objComm.CreateDiscSummary(DayOpendate, dtDtl, "", "SO201", vSiteCode, FinYear, vSalesNo, UserId, ServerDate, "FIRSTLEVEL", "TOPLEVEL")
                    If Not dtDisc Is Nothing AndAlso dtDisc.Rows.Count > 0 Then
                        If dsSOMain.Tables.Contains("salesdiscdtl") Then
                            dsSOMain.Tables.Remove("salesdiscdtl")
                            dsSOMain.Tables.Add(dtDisc)
                        Else
                            dsSOMain.Tables.Add(dtDisc)
                        End If
                        'dsSOMain.AcceptChanges()
                    End If
                End If
            End If
            Dim dtTempReserved As DataSet = dsSOMain.Copy()
            If dtTempReserved.Tables("SalesOrderDTL").Columns.Contains("Reserved_Qty") Then
                dtTempReserved.Tables("SalesOrderDTL").Columns.Remove("Reserved_Qty")
            End If
            If dtTempReserved.Tables("SalesOrderDTL").Columns.Contains("Delivered_Qty") Then
                dtTempReserved.Tables("SalesOrderDTL").Columns.Remove("Delivered_Qty")
            End If
            'B.FreezeSB, B.FreezeSR,B.FreezeOB
            If dtTempReserved.Tables("SalesOrderDTL").Columns.Contains("FreezeSB") Then
                dtTempReserved.Tables("SalesOrderDTL").Columns.Remove("FreezeSB")
            End If
            If dtTempReserved.Tables("SalesOrderDTL").Columns.Contains("FreezeSR") Then
                dtTempReserved.Tables("SalesOrderDTL").Columns.Remove("FreezeSR")
            End If
            If dtTempReserved.Tables("SalesOrderDTL").Columns.Contains("FreezeOB") Then
                dtTempReserved.Tables("SalesOrderDTL").Columns.Remove("FreezeOB")
            End If
            OpenConnection()

            '----- SET CREDIT Sale Update 
            Dim dtCreditSaleData As New DataTable
            Dim DvSalesOrderClosed As New DataView(dsSOMain.Tables("SalesOrderHdr"), "SoStatus = 'Closed'", "", DataViewRowState.CurrentRows)
            If DvSalesOrderClosed.ToTable.Rows.Count > 0 Then
                Dim objclsReturn As New clsCashMemoReturn
                dtCreditSaleData = objclsReturn.getCreditSaleBillData("'" & dsSOMain.Tables("SalesOrderHdr")(0)("SaleOrderNumber") & "'")
                dtCreditSaleData.Columns.Add("NetCreditSaleAmount")
                dtCreditSaleData.Columns.Add("AdjustedCredit")
                objCM.SetCloseBillWiseCreditSaleAmt(dtCreditSaleData, dsSOMain)

            End If


            SqlTrans = SpectrumCon.BeginTransaction()
            'If Not SaveDiscount(dsSOMain.Tables("SalesOrderDTL"), vSalesNo, SqlTrans) Then
            '    SqlTrans.Rollback()
            '    CloseConnection()
            '    Return False
            'End If
            If dtTempReserved.Tables.Contains("SalesOrderTaxDtls") Then
                If dtTempReserved.Tables("SalesOrderTaxDtls").Rows.Count > 0 Then
                    For Each row As DataRow In dtTempReserved.Tables("SalesOrderTaxDtls").Rows
                        If row.RowState <> DataRowState.Deleted Then
                            row("CreatedOn") = ServerDate
                            row("Updatedon") = ServerDate
                            row("CreatedBy") = UserId
                            row("Updatedby") = UserId
                            row("createdAt") = vSiteCode
                            row("UpdatedAt") = vSiteCode
                            row("Status") = 1
                            row("DocumentType") = "SalesOrder"
                        End If
                    Next
                End If
            End If

            If objComm.SaveData(dtTempReserved, SpectrumCon, SqlTrans) = True Then
                If DeliveryLocInfo IsNot Nothing AndAlso DeliveryLocInfo.Count > 0 Then
                    Dim query As String
                    For Each item In DeliveryLocInfo
                        If item.IsNew Then
                            query = "insert into SalesOrderDeliveryLocInfo (SalesOrderNumber, SiteCode, DeliverySiteCode, ArticleCode, Quantity, DeliveredQty,ReservedQty,Amount, " & _
                                          "CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON, STATUS) " & _
                                          "values ('" & item.SalesOrderNumber & "','" & item.SiteCode & "','" & item.DeliverySiteCode & "','" & item.ArticleCode & "'," & item.Quantity & ", " & item.DeliveredQuantity & " , " & item.ReservedQuantity & " , " & item.Amount & ", " & _
                                          "'" & item.CreatedAt & "','" & item.CreatedBy & "', getdate(), '" & item.CreatedAt & "','" & item.CreatedBy & "',getdate(),1)"
                            If objComm.InsertOrUpdateRecord(query, SqlTrans) = False Then
                                SqlTrans.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        ElseIf item.IsDirty Then
                            query = "update SalesOrderDeliveryLocInfo set Quantity = " & item.Quantity & " , STATUS = '" & item.Status & "' where SalesOrderNumber = '" & item.SalesOrderNumber & "' and SiteCode = '" & item.SiteCode & "' " & _
                                    "and  DeliverySiteCode = '" & item.DeliverySiteCode & "' and  ArticleCode = '" & item.ArticleCode & "' "
                            If objComm.InsertOrUpdateRecord(query, SqlTrans) = False Then
                                SqlTrans.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        End If
                    Next
                End If

                '---- Update Credit sale data in case of return ....
                Dim terminalid As String = "", BalanceAmount As Double = 0, AdavanceAmount As Double = 0
                Dim Retbillno As String = "", SalesOrderNumber As String = String.Empty

                '---- Fetch Sales Order Current Advance & Balance Amount , later will update these value 
                Dim DvSalesOrder As New DataView(dsSOMain.Tables("SalesOrderHdr"), "SoStatus <> 'Return'", "", DataViewRowState.CurrentRows)
                SalesOrderNumber = DvSalesOrder.ToTable.Rows(0)("SaleOrderNumber")
                    terminalid = DvSalesOrder.ToTable.Rows(0)("terminalid")
                    BalanceAmount = DvSalesOrder.ToTable.Rows(0)("BalanceAmount")
                    AdavanceAmount = DvSalesOrder.ToTable.Rows(0)("AdvanceAmt")

                    '--- Fetch Return Bill No & caculate Credit sale
                Dim DvSalesOrderRet As New DataView(dsSOMain.Tables("SalesOrderHdr"), "SoStatus = 'Return'", "", DataViewRowState.CurrentRows)
                If DvSalesOrderRet.ToTable.Rows.Count > 0 Then
                    Retbillno = DvSalesOrderRet.ToTable.Rows(0)("SaleOrderNumber")
               
                    Dim creditsale As Double = 0, crdsaleadjustamount As Double = 0

                    Dim NetReturnAmount As Double = 0
                    Dim salesorderdtl As New DataView(dsSOMain.Tables("SalesOrderDtl"), "ArticleStatus = 'Return'", "", DataViewRowState.CurrentRows)
                    Dim dt As DataTable = salesorderdtl.ToTable(True, "SaleOrderNumber", "NetAmount")

                    If dt.Rows.Count > 0 AndAlso Not dt Is Nothing Then
                        For Each x In dt.Rows
                            NetReturnAmount += x("NetAmount") * -1
                            'dtBillReturnDetails.Rows.Add(SalesOrderNumber, creditsale, NetReturnAmount, crdsaleadjustamount)
                        Next
                    End If


                    Dim DvSalesInvoice As New DataView(dsSOMain.Tables("SalesInvoice"), "TenderHeadCode = 'Credit Sales'", "", DataViewRowState.CurrentRows)
                    Dim dtInvoiceNoWiseCreditSale As DataTable = DvSalesInvoice.ToTable(True, "SaleInvNumber", "AmountTendered", "DocumentNumber")

                    Dim dtSalesInvoiceCreditDetails As New DataTable
                    dtSalesInvoiceCreditDetails.TableName = "dtSalesInvoiceCreditDetails"
                    dtSalesInvoiceCreditDetails.Columns.Add("SalesInvoiceNumber", GetType(String))
                    dtSalesInvoiceCreditDetails.Columns.Add("CreditSale", GetType(Double))
                    dtSalesInvoiceCreditDetails.Columns.Add("NetReturnAmount", GetType(Double))
                    dtSalesInvoiceCreditDetails.Columns.Add("crdsaleadjustamount", GetType(Double))
                    dtSalesInvoiceCreditDetails.Columns.Add("UpdateValue", GetType(Double))
                    dtSalesInvoiceCreditDetails.Columns.Add("CreditUsed", GetType(Double))
                    dtSalesInvoiceCreditDetails.Columns.Add("BalanceCredit", GetType(Double))
                    Dim FinalValue As Double = 0, CreditUsed As Double = 0, balancecredit As Double = 0
                    Dim SalesOrderInvNumber As String = ""
                    If dtInvoiceNoWiseCreditSale.Rows.Count > 0 Then
                        For Each x In dtInvoiceNoWiseCreditSale.Rows
                            SalesOrderInvNumber = x("SaleInvNumber")
                            creditsale = x("AmountTendered")
                            crdsaleadjustamount = objCM.GetInvoiceWiseCreditAdjusted(SalesOrderInvNumber, SpectrumCon, SqlTrans)
                            Dim maxlimit As Double = 0
                            maxlimit = creditsale - crdsaleadjustamount
                        

                            If maxlimit < NetReturnAmount Then
                                FinalValue = creditsale - maxlimit
                                balancecredit = maxlimit
                            Else
                                FinalValue = creditsale - NetReturnAmount
                                balancecredit = NetReturnAmount
                            End If

                            If NetReturnAmount < creditsale Then
                                CreditUsed = NetReturnAmount
                            Else
                                CreditUsed = creditsale
                            End If

                            dtSalesInvoiceCreditDetails.Rows.Add(SalesOrderInvNumber, creditsale, NetReturnAmount, crdsaleadjustamount, FinalValue, CreditUsed, balancecredit)
                            NetReturnAmount = NetReturnAmount - balancecredit
                        Next

                        Dim NetCrdSale As Double = 0

                        If dtSalesInvoiceCreditDetails.Rows.Count > 0 Then
                            For Each x In dtSalesInvoiceCreditDetails.Rows
                                NetCrdSale = x("creditsale") - x("crdsaleadjustamount")
                                CreditUsed = x("balancecredit")

                                If NetCrdSale > 0 Then

                                    If objCM.UpdateCSThroughSalesInvoiceNo(x("SalesInvoiceNumber"), x("UpdateValue"), SpectrumCon, SqlTrans, terminalid, DayOpendate, Retbillno, x("balancecredit"), vSiteCode) Then
                                        BalanceAmount = BalanceAmount + CreditUsed
                                        AdavanceAmount = AdavanceAmount - CreditUsed

                                        If CreditUsed > 0 Then
                                            If Not objCM.UpdateBalanceAndAdvanceAmt(SalesOrderNumber, BalanceAmount, AdavanceAmount, SpectrumCon, SqlTrans) Then
                                                SqlTrans.Rollback()
                                                CloseConnection()
                                                Return False
                                            End If
                                        End If
                                    Else
                                        SqlTrans.Rollback()
                                        CloseConnection()
                                        Return False
                                    End If
                                End If

                            Next



                        End If

                    End If


                End If

                If dtCreditSaleData.Rows.Count > 0 AndAlso IsMannualClose = True Then

                    If objCM.UpdateCreditSalesOnReturnItems(dtCreditSaleData, _SelectedCurrencyIndex, _iCurrencyCode, SpectrumCon, SqlTrans, terminalid, DayOpendate, dsSOMain.Tables("SalesOrderHdr")(0)("SaleOrderNumber"), vSiteCode) = False Then
                        SqlTrans.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If

                ''updating credit sale value in case of return

                '// ------- FOR BulkCombo Value .....
                '----- Delete Bul Combo Details 
                Dim cmd As New System.Text.StringBuilder()
                If Not DtSoBulkComboHdr Is Nothing AndAlso DtSoBulkComboHdr.Rows.Count > 0 Then
                    '--- First Delete The Record That are not existing in database ----
                    Dim deletedcomboListStr As String = String.Empty
                    For index = 0 To DtSoBulkComboHdr.Rows.Count - 1
                        If Val(DtSoBulkComboHdr.Rows(index)("BulkComboMstId")) >= 100 Then
                            deletedcomboListStr = deletedcomboListStr & Val(DtSoBulkComboHdr.Rows(index)("BulkComboMstId")) & ","
                        End If
                    Next

                    Dim deletedDetailListStr As String = String.Empty
                    For index = 0 To DtSoBulkComboDtl.Rows.Count - 1
                        If Val(DtSoBulkComboDtl.Rows(index)("BulkComboDetId")) >= 500 Then
                            deletedDetailListStr = deletedDetailListStr & Val(DtSoBulkComboDtl.Rows(index)("BulkComboDetId")) & ","
                        End If
                    Next

                    If Not String.IsNullOrEmpty(deletedcomboListStr) Then
                        deletedcomboListStr = deletedcomboListStr.Substring(0, deletedcomboListStr.Length - 1)
                        deletedDetailListStr = deletedDetailListStr.Substring(0, deletedDetailListStr.Length - 1)
                        cmd.Length = 0
                        cmd.Append(" Delete  From SoBulkComboDtl  Where BulkComboMstId NOT IN ( " & deletedcomboListStr & ") ")
                        cmd.Append(" AND BulkComboMstId in ( Select BulkComboMstId  From SoBulkComboHdr Where SaleOrderNumber ='" & vSalesNo & "')")


                        If objComm.InsertOrUpdateRecord(cmd.ToString(), SqlTrans) = False Then
                            SqlTrans.Rollback()
                            CloseConnection()
                            Return False
                        End If

                        cmd.Length = 0
                        cmd.Append(" Delete  From SoBulkComboDtl  Where BulkComboMstId IN ( " & deletedcomboListStr & ") ")
                        cmd.Append(" AND BulkComboDetId not in ( " & deletedDetailListStr & " ) ")
                        If objComm.InsertOrUpdateRecord(cmd.ToString(), SqlTrans) = False Then
                            SqlTrans.Rollback()
                            CloseConnection()
                            Return False
                        End If

                        cmd.Length = 0
                        cmd.Append(" Delete  From SoBulkComboHdr Where BulkComboMstId NOT IN ( " & deletedcomboListStr & ") AND SaleOrderNumber ='" & vSalesNo & "'")

                        If objComm.InsertOrUpdateRecord(cmd.ToString(), SqlTrans) = False Then
                            SqlTrans.Rollback()
                            CloseConnection()
                            Return False
                        End If



                    End If

                    Dim BulkComboMstId As Int64
                    Dim IsNewBulkCombo As Boolean = False
                    For index = 0 To DtSoBulkComboHdr.Rows.Count - 1
                        cmd.Length = 0
                        BulkComboMstId = 0
                        IsNewBulkCombo = False
                        With DtSoBulkComboHdr
                            '---Whether Insert or Update 
                            If Val(.Rows(index)("BulkComboMstId")) > 100 Then
                                BulkComboMstId = .Rows(index)("BulkComboMstId")

                                cmd.Append("  UPDATE   SoBulkComboHdr ")
                                cmd.Append("  SET       ")
                                cmd.Append("  ComboSrNo = '" & .Rows(index)("ComboSrNo") & "',")
                                cmd.Append("  PackagingBoxCode = '" & .Rows(index)("PackagingBoxCode") & "',  ")
                                cmd.Append("  AdditionComments = '" & .Rows(index)("AdditionComments") & "',  ")
                                cmd.Append("  UPDATEDAT = '" & .Rows(index)("UPDATEDAT") & "',  ")
                                cmd.Append("  UPDATEDBY = '" & .Rows(index)("UPDATEDBY") & "',  ")
                                cmd.Append("  UPDATEDON = getdate()  ")
                                cmd.Append("  Where BulkComboMstId =" & .Rows(index)("BulkComboMstId"))
                            Else
                                cmd.Append(" INSERT INTO SoBulkComboHdr ")
                                cmd.Append(" (SaleOrderNumber,sitecode, ComboSrNo,  ")
                                cmd.Append("  PackagingBoxCode,AdditionComments, CREATEDAT,")
                                cmd.Append("  CREATEDBY, CREATEDON,UPDATEDAT,  ")
                                cmd.Append("  UPDATEDBY, UPDATEDON, STATUS) ")
                                cmd.Append(" VALUES (")
                                cmd.Append("'" & .Rows(index)("SaleOrderNumber") & "','" & .Rows(index)("sitecode") & "','" & .Rows(index)("ComboSrNo") & "',")
                                cmd.Append("'" & .Rows(index)("PackagingBoxCode") & "','" & .Rows(index)("AdditionComments") & "','" & .Rows(index)("CREATEDAT") & "',")
                                cmd.Append("'" & .Rows(index)("CREATEDBY") & "',getDate(),'" & .Rows(index)("UPDATEDAT") & "',")
                                cmd.Append("'" & .Rows(index)("UPDATEDBY") & "',getDate(),'" & .Rows(index)("STATUS") & "'")
                                cmd.Append(")")
                            End If
                        End With

                        If objComm.InsertOrUpdateRecord(cmd.ToString(), SqlTrans) = False Then
                            SqlTrans.Rollback()
                            CloseConnection()
                            Return False
                        End If
                        '---- Get BulkComboMstId from table 
                        If Val(BulkComboMstId) = 0 Then
                            BulkComboMstId = objComm.getTableIdentityValueId("SoBulkComboHdr", SpectrumCon, SqlTrans)
                            IsNewBulkCombo = True
                        End If
                        Dim drDtl() = DtSoBulkComboDtl.Select("BulkComboMstId=" & DtSoBulkComboHdr.Rows(index)("BulkComboMstId"))
                        If drDtl.Count > 0 Then
                            For Dtlindex = 0 To drDtl.Count - 1
                                cmd.Length = 0
                                With DtSoBulkComboDtl
                                    If Val(.Rows(Dtlindex)("BulkComboMstId")) > 100 AndAlso Not IsNewBulkCombo AndAlso drDtl(Dtlindex)("BulkComboDetId") > 500 Then
                                        cmd.Append("  UPDATE    SoBulkComboDtl ")
                                        cmd.Append("  SET               ")
                                        cmd.Append("  ArticleCode	=	'" & drDtl(Dtlindex)("ArticleCode") & "'	, ")
                                        cmd.Append("  ArticleDescription	=	'" & drDtl(Dtlindex)("ArticleDescription") & "'	, ")
                                        cmd.Append("  EAN	=	'" & drDtl(Dtlindex)("EAN") & "'	, ")
                                        cmd.Append("  PackagedUOM	=	'" & drDtl(Dtlindex)("PackagedUOM") & "', ")
                                        cmd.Append("  Qty	='" & drDtl(Dtlindex)("Qty") & "', ")
                                        cmd.Append("  Weight	=	'" & drDtl(Dtlindex)("Weight") & "', ")
                                        cmd.Append("  STRQty	=	'" & drDtl(Dtlindex)("STRQty") & "', ")
                                        cmd.Append("  StrExcludeCheck	=	'" & drDtl(Dtlindex)("StrExcludeCheck") & "', ")
                                        cmd.Append("  BaseUOM	=	'" & drDtl(Dtlindex)("BaseUOM") & "'	, ")
                                        cmd.Append("  ItemQtyBaseUOM	= '" & drDtl(Dtlindex)("ItemQtyBaseUOM") & "', ")
                                        cmd.Append("  UPDATEDAT	=	'" & drDtl(Dtlindex)("UPDATEDAT") & "', ")
                                        cmd.Append("  UPDATEDBY	= '" & drDtl(Dtlindex)("UPDATEDBY") & "', ")
                                        cmd.Append("  UPDATEDON	=	GETDATE() ")
                                        cmd.Append("  Where BulkComboDetId =" & drDtl(Dtlindex)("BulkComboDetId"))
                                    Else
                                        cmd.Append(" INSERT INTO SoBulkComboDtl( ")
                                        cmd.Append(" BulkComboMstId, ArticleCode,  ")
                                        cmd.Append(" ArticleDescription,EAN, PackagedUOM, Qty,  ")
                                        cmd.Append(" Weight, STRQty,StrExcludeCheck, BaseUOM, ItemQtyBaseUOM,  ")
                                        cmd.Append(" CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT,  ")
                                        cmd.Append(" UPDATEDBY, UPDATEDON, STATUS) ")
                                        cmd.Append(" Values(")
                                        cmd.Append("'" & BulkComboMstId & "','" & drDtl(Dtlindex)("ArticleCode") & "',")
                                        cmd.Append("'" & drDtl(Dtlindex)("ArticleDescription") & "','" & drDtl(Dtlindex)("EAN") & "','" & drDtl(Dtlindex)("PackagedUOM") & "','" & drDtl(Dtlindex)("Qty") & "',")
                                        cmd.Append("'" & drDtl(Dtlindex)("Weight") & "','" & drDtl(Dtlindex)("STRQty") & "','" & drDtl(Dtlindex)("StrExcludeCheck") & "','" & drDtl(Dtlindex)("BaseUOM") & "',")
                                        cmd.Append("'" & drDtl(Dtlindex)("ItemQtyBaseUOM") & "','" & drDtl(Dtlindex)("CREATEDAT") & "',")
                                        cmd.Append("'" & drDtl(Dtlindex)("CREATEDBY") & "',GetDate(),'" & drDtl(Dtlindex)("UPDATEDAT") & "',")
                                        cmd.Append("'" & drDtl(Dtlindex)("UPDATEDBY") & "',GetDate(),'" & drDtl(Dtlindex)("STATUS") & "'")
                                        cmd.Append(" )")
                                    End If

                                End With
                                If objComm.InsertOrUpdateRecord(cmd.ToString(), SqlTrans) = False Then
                                    SqlTrans.Rollback()
                                    CloseConnection()
                                    Return False
                                End If
                            Next
                        End If
                    Next
                Else
                    cmd.Length = 0
                    cmd.Append(" Delete   From SoBulkComboDtl ")
                    cmd.Append(" Where BulkComboMstId  IN ( Select BulkComboMstId From SoBulkComboHdr Where SaleOrderNumber = '" & vSalesNo & "' and siteCode = '" & vSiteCode & "' )")

                    If objComm.InsertOrUpdateRecord(cmd.ToString(), SqlTrans) = False Then
                        SqlTrans.Rollback()
                        CloseConnection()
                        Return False
                    End If

                    cmd.Length = 0
                    cmd.Append(" Delete From SoBulkComboHdr  ")
                    cmd.Append(" Where SaleOrderNumber = '" & vSalesNo & "' and siteCode = '" & vSiteCode & "' ")

                    If objComm.InsertOrUpdateRecord(cmd.ToString(), SqlTrans) = False Then
                        SqlTrans.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If

                For Each dr As DataRow In dsSOMain.Tables("SalesOrderDTL").Rows
                    If dr.RowState <> DataRowState.Deleted Then
                        Dim reserveQty As Decimal = IIf(dr("Reserved_Qty") Is DBNull.Value, 0, dr("Reserved_Qty"))
                        'If DeliveryLocInfo IsNot Nothing AndAlso DeliveryLocInfo.Count > 0 AndAlso reserveQty > 0 Then
                        '    Dim otherSiteDeliveryQty As Decimal = DeliveryLocInfo.Sum(Function(x) IIf(x.ArticleCode = dr("ArticleCode").ToString(), x.Quantity, 0))
                        '    reserveQty = reserveQty - otherSiteDeliveryQty
                        'End If

                        'If objComm.UpdateStock(dr("SiteCode").ToString, dr("ArticleCode").ToString(), dr("EAN").ToString(), dr("UnitofMeasure").ToString(), IIf(IsDBNull(dr("Delivered_Qty")), 0, dr("Delivered_Qty").ToString()), dr("CreatedAt"), SpectrumCon, SqlTrans, Storage, reserveQty) = False Then
                        '    SqlTrans.Rollback()
                        '    CloseConnection()
                        '    Return False
                        'End If

                        '----- Now Handling Bulk Combo Case (code change By Mahesh )
                        If Not DtSoBulkComboHdr Is Nothing AndAlso DtSoBulkComboHdr.Rows.Count > 0 Then
                            Dim test() = DtSoBulkComboHdr.Select("PackagingBoxCode ='" & dr("ArticleCode").ToString() & "' AND ComboSrNo=" & dr("BillLineNo"))

                            If test.Count > 0 Then

                                Dim DeliveryQty = IIf(IsDBNull(dr("Delivered_Qty")), 0, dr("Delivered_Qty").ToString())
                                Dim DeliveredQty = IIf(IsDBNull(dr("DeliveredQty")), 0, dr("DeliveredQty").ToString())
                                Dim Qty = IIf(IsDBNull(dr("Quantity")), 0, dr("Quantity").ToString())
                                With DtSoBulkComboHdr
                                    '-----Run for PackagedBox
                                    If objComm.UpdateStock(dr("SiteCode").ToString, dr("ArticleCode").ToString(), dr("EAN").ToString(), dr("UnitofMeasure").ToString(), IIf(IsDBNull(dr("Delivered_Qty")), 0, dr("Delivered_Qty").ToString()), dr("CreatedAt"), SpectrumCon, SqlTrans, Storage, reserveQty) = False Then
                                        SqlTrans.Rollback()
                                        CloseConnection()
                                        Return False
                                    End If
                                End With

                                '-----Run for Each Article of Bulk combo
                                Dim comboItemDeliveryQty As Double = 0
                                Dim comboItemReserveQty As Double = 0
                                Dim drDtl() = DtSoBulkComboDtl.Select("BulkComboMstId=" & test(0)("BulkComboMstId"))
                                If drDtl.Count > 0 Then
                                    With DtSoBulkComboDtl
                                        For index = 0 To drDtl.Count - 1
                                            If .Rows(index)("STRExcludeCheck") Then
                                                If (DeliveryQty + DeliveredQty) = Qty Then
                                                    comboItemDeliveryQty = drDtl(index)("ItemQtyBaseUOM").ToString()
                                                End If
                                            Else
                                                comboItemDeliveryQty = DeliveryQty * drDtl(index)("ItemQtyBaseUOM").ToString()
                                                comboItemReserveQty = reserveQty * drDtl(index)("ItemQtyBaseUOM").ToString()
                                            End If

                                            'Dim otherSiteDeliveryQty As Decimal = SoDeliveryInfo.Sum(Function(x) IIf(x.ArticleCode = .Rows(index)("ArticleCode").ToString(), x.Quantity, 0))
                                            If objComm.UpdateStock(dr("SiteCode").ToString, drDtl(index)("ArticleCode").ToString(), drDtl(index)("EAN").ToString(), drDtl(index)("BaseUOM").ToString(), comboItemDeliveryQty, drDtl(index)("CreatedAt"), SpectrumCon, SqlTrans, Storage, comboItemReserveQty) = False Then
                                                SqlTrans.Rollback()
                                                CloseConnection()
                                                Return False
                                            End If
                                        Next index
                                    End With
                                End If
                            Else
                                If objComm.UpdateStock(dr("SiteCode").ToString, dr("ArticleCode").ToString(), dr("EAN").ToString(), dr("UnitofMeasure").ToString(), IIf(IsDBNull(dr("Delivered_Qty")), 0, dr("Delivered_Qty").ToString()), dr("CreatedAt"), SpectrumCon, SqlTrans, Storage, reserveQty) = False Then
                                    SqlTrans.Rollback()
                                    CloseConnection()
                                    Return False
                                End If
                            End If
                        Else
                            If objComm.UpdateStock(dr("SiteCode").ToString, dr("ArticleCode").ToString(), dr("EAN").ToString(), dr("UnitofMeasure").ToString(), IIf(IsDBNull(dr("Delivered_Qty")), 0, dr("Delivered_Qty").ToString()), dr("CreatedAt"), SpectrumCon, SqlTrans, Storage, reserveQty) = False Then
                                SqlTrans.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        End If


                    End If
                Next

                '---
                If IsbatchMgt Then
                    If BatchBarcodeList IsNot Nothing Then

                        For Each dr As SpectrumCommon.BtachbarcodeInfo In BatchBarcodeList
                            'Change by Sameer for Issue 6915 4/4/13
                            If objComn.UpdateBatchDtlQtyAllocated(vSiteCode, dr.BatchBarcode.ToString(), dr.Qty, SqlTrans) = False Then
                                SqlTrans.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        Next
                    End If

                    'For Each dr As DataRow In dsSOMain.Tables("SalesOrderDTL").Rows
                    '    If dr.RowState <> DataRowState.Deleted Then
                    '        If IsDBNull(dr("BatchBarcode")) = False AndAlso String.IsNullOrEmpty(dr("BatchBarcode")) = False Then
                    '            'Change by Sameer for Issue 6915 4/4/13
                    '            If objComn.UpdateBatchDtlQtyAllocated(dr("SiteCode").ToString, dr("BatchBarcode").ToString(), IIf(IsDBNull(dr("Delivered_Qty")), 0, dr("Delivered_Qty").ToString()), SqlTrans) = False Then
                    '                SqlTrans.Rollback()
                    '                CloseConnection()
                    '                Return False
                    '            End If
                    '        End If
                    '    End If
                    'Next
                End If
                '-----deleted data code for reversing reserve Qty---------
                If Not DtDeletedData Is Nothing AndAlso DtDeletedData.Rows.Count > 0 Then
                    For Each dr As DataRow In DtDeletedData.Rows
                        If dr.RowState <> DataRowState.Deleted Then
                            If objComm.UpdateStock(vSiteCode, dr("ArticleCode").ToString(), dr("EAN").ToString(), "", 0, UserId, SpectrumCon, SqlTrans, Storage, IIf(dr("ReservedQty") = False, 0, dr("Quantity") * -1)) = False Then
                                SqlTrans.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        End If
                    Next
                    DtDeletedData = Nothing
                End If
                '---------end here for reserve-----------
                If IsOBCreated = True Then
                    If objComn.UpdateDocumentNo("OutBound", SpectrumCon, SqlTrans) = False Then
                        SqlTrans.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If

                ' Add for insert for CV by ram  in ver 1.0.10.7 . dataviewrowstate.add is add because in edit sales order it is pickup old records.
                'current Sales Invoice Number
                Dim strCurrSaleInvNbr As String = ""
                If dtTempReserved.Tables("SalesInvoice").Rows.Count > 0 Then
                    strCurrSaleInvNbr = dtTempReserved.Tables("SalesInvoice").Compute("MAX(SaleInvNumber)", "")
                End If



                Dim dvCreditVoucher As New DataView(dtTempReserved.Tables("SalesInvoice"), " SaleInvNumber='" & strCurrSaleInvNbr & "' AND TenderTypeCode Like 'CreditVouc%'", "", DataViewRowState.CurrentRows)
                Dim RedimCVExpDay As Integer = 0
                If dvCreditVoucher.Count > 0 Then
                    For Each drView As DataRowView In dvCreditVoucher

                        '' Issue CV against partial redeemation should have expirydate same as orignal CV
                        'If dvCreditVoucher.Count > 1 Then
                        '    Dim dvRedimCV As New DataView(dtTempReserved.Tables("SalesInvoice"), " SALEINVNUMBER='" & strCurrSaleInvNbr & "' AND TenderTypeCode = 'CreditVouc(R)'", "", DataViewRowState.CurrentRows)
                        '    If dvRedimCV.Count > 0 Then
                        '        If Not IsDBNull(dvRedimCV(0).Item("RefDate")) Then
                        '            RedimCVExpDay = DateDiff(DateInterval.Day, dsSOMain.Tables("SalesOrderHdr").Rows(0)("SiteCode"), drView("SOInvDate"), dvRedimCV(0).Item("RefDate"))
                        '        End If
                        '    End If
                        'End If
                        '' Issue CV against partial redeemation should have expirydate same as orignal CV

                        If drView("TenderTypeCode") = "CreditVouc(I)" Then
                            ' this is to get the old expiry date of partial redeem CV
                            'If dvCreditVoucher.Count > 1 Then
                            If Not IsDBNull(drView("RefDate")) Then
                                VoucherDays = DateDiff(DateInterval.Day, ServerDate.Date, CDate(drView("RefDate")).Date)
                                VoucherDays = VoucherDays
                            End If
                            'End If
                            ' this is to get the old expiry date of partial redeem CV

                            If objCM.UpdateCreditVoucher(CVProgram, DocType, True, drView("DocumentNumber").ToString(), dsSOMain.Tables("SalesOrderHdr").Rows(0)("SiteCode"), drView("SOInvDate"), dsSOMain.Tables("SalesOrderHdr").Rows(0)("CreatedBy"), SqlTrans, SpectrumCon, drView("AmountTendered"), Voucherno, VoucherDays) = False Then
                                SqlTrans.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        ElseIf drView("TenderTypeCode") = "CreditVouc(R)" Then
                            If objCM.UpdateCreditVoucher(CVProgram, DocType, False, drView("DocumentNumber").ToString(), dsSOMain.Tables("SalesOrderHdr").Rows(0)("SiteCode"), drView("SOInvDate"), dsSOMain.Tables("SalesOrderHdr").Rows(0)("CreatedBy"), SqlTrans, SpectrumCon, 0, drView("RefNO_2").ToString()) = False Then
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
                        ' Issue GV against partial redeemation should have expirydate same as orignal GV
                        'If dvGiftVoucher.Count > 1 Then
                        '    Dim dvRedimGV As New DataView(dtTempReserved.Tables("SalesInvoice"), " SALEINVNUMBER='" & strCurrSaleInvNbr & "' AND TenderTypeCode = 'GiftVoucher(R)'", "", DataViewRowState.CurrentRows)
                        '    If dvRedimGV.Count > 0 Then
                        '        If Not IsDBNull(dvRedimGV(0).Item("RefDate")) Then
                        '            RedimGVExpDay = DateDiff(DateInterval.Day, Now, dvRedimGV(0).Item("RefDate"))
                        '        End If
                        '    End If
                        'End If
                        '' Issue GV against partial redeemation should have expirydate same as orignal GV

                        If drView("TenderTypeCode") = "GiftVoucher(I)" Then
                            ' this is to get the old expiry date of partial redeem GV
                            'If dvGiftVoucher.Count > 1 Then
                            If Not IsDBNull(drView("RefDate")) Then
                                VoucherDays = DateDiff(DateInterval.Day, ServerDate.Date, CDate(drView("RefDate")).Date)
                                VoucherDays = VoucherDays
                            End If
                            'End If
                            ' this is to get the old expiry date of partial redeem GV

                            If objCM.UpdateCreditVoucher(drView("RefNo_2"), DocType, True, drView("DocumentNumber").ToString(), dsSOMain.Tables("SalesOrderHdr").Rows(0)("SiteCode"), drView("SOInvDate"), dsSOMain.Tables("SalesOrderHdr").Rows(0)("CreatedBy"), SqlTrans, SpectrumCon, drView("AmountTendered"), Voucherno, VoucherDays, "GV") = False Then
                                SqlTrans.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        ElseIf drView("TenderTypeCode") = "GiftVoucher(R)" Then
                            If objCM.UpdateCreditVoucher(CVProgram, DocType, False, drView("DocumentNumber").ToString(), dsSOMain.Tables("SalesOrderHdr").Rows(0)("SiteCode"), drView("SOInvDate"), dsSOMain.Tables("SalesOrderHdr").Rows(0)("CreatedBy"), SqlTrans, SpectrumCon, 0, drView("RefNO_2").ToString(), 0, "GV") = False Then
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
                        If objComm.UpdateClpPoints(False, ClpProgramId, CLPCustomerId, TotalPoints, SpectrumCon, SqlTrans, dsSOMain.Tables("SalesOrderHdr").Rows(0)("SiteCode"), UserId, vSalesNo, ServerDate, False) = False Then
                            SqlTrans.Rollback()
                            CloseConnection()
                            Return False
                        End If
                    Next
                End If
                '--------Code Added By Mahesh to Generate STR 
                If IsStrGenerate Then
                    If objComn.GenerateSaleOrderSTR(vSalesNo, vSiteCode, SpectrumCon, SqlTrans) = False Then
                        SqlTrans.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If

                If IsNextSalesNo = True Then
                    Dim objType = "FO_DOC"
                    If objComn.UpdateDocumentNo("SalesOrder", SpectrumCon, SqlTrans, , objType) = False Then
                        SqlTrans.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If

                If IsNextInvoiceNo = True Then
                    If objComn.UpdateDocumentNo("CM", SpectrumCon, SqlTrans) = False Then
                        SqlTrans.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If




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
    'Public Function PrepareSaveDataCCE(ByVal currentSalesinvoice As String, ByVal DayOpendate As DateTime, ByVal ClpProgramId As String, ByVal CLPCustomerId As String, ByRef dsSOMain As DataSet, ByVal IsNextSalesNo As Boolean, ByVal IsNextInvoiceNo As Boolean, ByVal vSiteCode As String, ByVal vSalesNo As String, ByVal Storage As String, ByVal CVProgram As String, ByVal DocType As String, ByVal FinYear As String, ByVal UserId As String, ByVal ServerDate As DateTime, ByVal IsOBCreated As Boolean, Optional ByRef Voucherno As String = "", Optional ByRef VoucherDays As Int32 = 0, Optional ByRef isPromoApplied As Boolean = False, Optional ByVal DtDeletedData As DataTable = Nothing, Optional ByVal DeliveryLocInfo As List(Of SODeliveryLocationInfo) = Nothing, Optional ByVal IsbatchMgt As Boolean = False, Optional ByVal BatchBarcodeList As List(Of SpectrumCommon.BtachbarcodeInfo) = Nothing) As Boolean
    '    Try
    '        Dim objComm As New clsCommon
    '        Dim disc As Double
    '        If isPromoApplied = True Then

    '            Dim strSql As String = " DELETE FROM SALESDISCDTL WHERE SiteCode = '" & vSiteCode & "' AND FinYear = '" & FinYear & "' AND BillNo = '" & vSalesNo & "'"
    '            OpenConnectionCCE()
    '            Dim cmd As New SqlCommand(strSql, SpectrumConCCE)
    '            cmd.ExecuteNonQuery()

    '            disc = IIf(dsSOMain.Tables("SalesOrderDTL").Compute("Sum(DiscountAmount)", "") Is DBNull.Value, 0, dsSOMain.Tables("SalesOrderDTL").Compute("Sum(DiscountAmount)", ""))
    '            If disc > 0 Then
    '                Dim dtDtl As DataTable = dsSOMain.Tables("SalesOrderDTL").Copy()

    '                Dim dtDisc As DataTable = objComm.CreateDiscSummary(DayOpendate, dtDtl, "", "SO201", vSiteCode, FinYear, vSalesNo, UserId, ServerDate, "FIRSTLEVEL", "TOPLEVEL")
    '                If Not dtDisc Is Nothing AndAlso dtDisc.Rows.Count > 0 Then
    '                    If dsSOMain.Tables.Contains("salesdiscdtl") Then
    '                        dsSOMain.Tables.Remove("salesdiscdtl")
    '                        dsSOMain.Tables.Add(dtDisc)
    '                    Else
    '                        dsSOMain.Tables.Add(dtDisc)
    '                    End If
    '                    'dsSOMain.AcceptChanges()
    '                End If
    '            End If
    '        End If
    '        Dim dtTempReserved As DataSet = dsSOMain.Copy()
    '        If dtTempReserved.Tables("SalesOrderDTL").Columns.Contains("Reserved_Qty") Then
    '            dtTempReserved.Tables("SalesOrderDTL").Columns.Remove("Reserved_Qty")
    '        End If
    '        If dtTempReserved.Tables("SalesOrderDTL").Columns.Contains("Delivered_Qty") Then
    '            dtTempReserved.Tables("SalesOrderDTL").Columns.Remove("Delivered_Qty")
    '        End If
    '        'B.FreezeSB, B.FreezeSR,B.FreezeOB
    '        If dtTempReserved.Tables("SalesOrderDTL").Columns.Contains("FreezeSB") Then
    '            dtTempReserved.Tables("SalesOrderDTL").Columns.Remove("FreezeSB")
    '        End If
    '        If dtTempReserved.Tables("SalesOrderDTL").Columns.Contains("FreezeSR") Then
    '            dtTempReserved.Tables("SalesOrderDTL").Columns.Remove("FreezeSR")
    '        End If
    '        If dtTempReserved.Tables("SalesOrderDTL").Columns.Contains("FreezeOB") Then
    '            dtTempReserved.Tables("SalesOrderDTL").Columns.Remove("FreezeOB")
    '        End If
    '        OpenConnectionCCE()

    '        '----- SET CREDIT Sale Update 
    '        Dim dtCreditSaleData As New DataTable
    '        Dim DvSalesOrderClosed As New DataView(dsSOMain.Tables("SalesOrderHdr"), "SoStatus = 'Closed'", "", DataViewRowState.CurrentRows)
    '        If DvSalesOrderClosed.ToTable.Rows.Count > 0 Then
    '            Dim objclsReturn As New clsCashMemoReturn
    '            dtCreditSaleData = objclsReturn.getCreditSaleBillData("'" & dsSOMain.Tables("SalesOrderHdr")(0)("SaleOrderNumber") & "'")
    '            dtCreditSaleData.Columns.Add("NetCreditSaleAmount")
    '            dtCreditSaleData.Columns.Add("AdjustedCredit")
    '            objCM.SetCloseBillWiseCreditSaleAmt(dtCreditSaleData, dsSOMain)

    '        End If


    '        SqlTrans = SpectrumConCCE.BeginTransaction()
    '        'If Not SaveDiscount(dsSOMain.Tables("SalesOrderDTL"), vSalesNo, SqlTrans) Then
    '        '    SqlTrans.Rollback()
    '        '    CloseConnection()
    '        '    Return False
    '        'End If
    '        If dtTempReserved.Tables.Contains("SalesOrderTaxDtls") Then
    '            If dtTempReserved.Tables("SalesOrderTaxDtls").Rows.Count > 0 Then
    '                For Each row As DataRow In dtTempReserved.Tables("SalesOrderTaxDtls").Rows
    '                    If row.RowState <> DataRowState.Deleted Then
    '                        row("CreatedOn") = ServerDate
    '                        row("Updatedon") = ServerDate
    '                        row("CreatedBy") = UserId
    '                        row("Updatedby") = UserId
    '                        row("createdAt") = vSiteCode
    '                        row("UpdatedAt") = vSiteCode
    '                        row("Status") = 1
    '                        row("DocumentType") = "SalesOrder"
    '                    End If
    '                Next
    '            End If
    '        End If

    '        If objComm.SaveData(dtTempReserved, SpectrumConCCE, SqlTrans) = True Then
    '            SqlTrans.Commit()
    '            CloseConnectionCCE()
    '            Return True
    '        Else
    '            SqlTrans.Rollback()
    '            CloseConnectionCCE()
    '            Return False
    '        End If

    '    Catch ex As Exception
    '        SqlTrans.Rollback()
    '        CloseConnectionCCE()
    '        LogException(ex)
    '        Return False
    '    End Try
    'End Function
    Public Function GenerateStrData(ByVal vSiteCode As String, ByVal vSalesNo As String) As Boolean
        Try
            Dim SqlTranSTR As SqlTransaction = Nothing
            OpenConnection()
            SqlTranSTR = SpectrumCon.BeginTransaction()

            '--------Code Added By Mahesh to Generate STR 

            If objComn.GenerateSaleOrderSTR(vSalesNo, vSiteCode, SpectrumCon, SqlTranSTR) = False Then
                SqlTranSTR.Rollback()
                CloseConnection()
                Return False
            End If
            SqlTranSTR.Commit()
            CloseConnection()
            SqlTranSTR.Dispose()
            Return True
        Catch ex As Exception
            SqlTrans.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function PrepareSaveClpData(ByRef dsCLOMain As DataSet, ByVal CLPProgId As String, ByVal CLPCustmId As String, ByVal CLPPoints As Double, ByVal vSiteCode As String, ByVal vSalesNo As String) As Boolean
        Try
            Dim SqlTranClp As SqlTransaction = Nothing
            OpenConnection()

            SqlTranClp = SpectrumCon.BeginTransaction()
            If SaveClpData(dsCLOMain, SpectrumCon, SqlTranClp, vSiteCode, vSalesNo) = True Then
                If objComn.UpdateClpPoints(True, CLPProgId, CLPCustmId, CLPPoints, SpectrumCon, SqlTranClp) = True Then
                    SqlTranClp.Commit()
                    CloseConnection()
                    Return True
                End If
            End If
            SqlTranClp.Rollback()
            CloseConnection()
            Return False

        Catch ex As Exception
            SqlTrans.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function SaveClpData(ByRef dsSO As DataSet, ByRef Sqlconn As SqlConnection, ByRef SqlTran As SqlTransaction, ByVal vSiteCode As String, ByVal vSalesNo As String) As Boolean
        Try
            For TbCnt = 0 To dsSO.Tables.Count - 1

                Dim tableName As String = dsSO.Tables(TbCnt).TableName
                Dim tableColumns As String = ""

                For ColIndex = 0 To dsSO.Tables(TbCnt).Columns.Count - 1
                    tableColumns = tableColumns & ", " & dsSO.Tables(TbCnt).Columns(ColIndex).ColumnName.ToString()
                Next
                tableColumns = tableColumns.Substring(1)

                vStmtQry.Length = 0
                vStmtQry.Append("SELECT " + tableColumns & " FROM " & tableName)
                vStmtQry.Append(" Where SiteCode = '" & vSiteCode & "' and BillNo  = '" & vSalesNo & "' " & vbCrLf)

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

    ''' <summary>
    ''' Save/Update Sales Order Details
    ''' </summary>
    ''' <param name="dsSO">DataSet</param>
    ''' <param name="Sqlconn">Sql Connection</param>
    ''' <param name="SqlTran">Sql Transaction</param>
    ''' <returns>True/False</returns>
    ''' <UsedBy>frmSalesOrderCreation.vb, frmSalesOrderUpdation.vb, frmSalesOrderCancelation.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function SaveData(ByRef dsSO As DataSet, ByRef Sqlconn As SqlConnection, ByRef SqlTran As SqlTransaction, ByVal vSiteCode As String, ByVal vSalesNo As String) As Boolean
        Try
            For TbCnt = 0 To dsSO.Tables.Count - 1

                Dim tableName As String = dsSO.Tables(TbCnt).TableName
                Dim tableColumns As String = ""

                For ColIndex = 0 To dsSO.Tables(TbCnt).Columns.Count - 1
                    tableColumns = tableColumns & ", " & dsSO.Tables(TbCnt).Columns(ColIndex).ColumnName.ToString()
                Next
                tableColumns = tableColumns.Substring(1)

                vStmtQry.Length = 0
                If tableName = "SalesInvoice" Or tableName = "OrderHdr" Or tableName = "OrderDtl" Then
                    vStmtQry.Append("SELECT " + tableColumns & " FROM " & tableName)
                    vStmtQry.Append(" Where SiteCode = '" & vSiteCode & "' and DocumentNumber= '" & vSalesNo & "' " & vbCrLf)
                Else
                    vStmtQry.Append("SELECT " + tableColumns & " FROM " & tableName)
                    vStmtQry.Append(" Where SiteCode = '" & vSiteCode & "' and SaleOrderNumber  = '" & vSalesNo & "' " & vbCrLf)
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

    ''' <summary>
    ''' Get Sales Order Default Config Information
    ''' </summary>
    ''' <param name="vSiteCode">Site Code</param>
    ''' <returns>Object Values</returns>
    ''' <UsedBy>frmSalesOrderCreation.vb, frmSalesOrderUpdation.vb, frmSalesOrderCancelation.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function GetSODefaultConfig(ByVal vSiteCode As String) As String

        Try
            _SOCreation = "SO201"
            _SOReturn = "SO202"
            'vStmtQry.Length = 0
            ''vStmtQry.Append("Select FldLabel, FldValue from DefaultConfig Where DocumentType='SalesOrder' And SiteCode='" & vSiteCode & "' ; " & vbCrLf)
            'vStmtQry.Append("Select DocumentType, DocumentTypeDesc from MstDocumentTypes Where DocumentTypeDesc like 'Sales %'; " & vbCrLf)

            'Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, ConString)
            'Sqlcmdb = New SqlCommandBuilder(Sqlda)
            'Sqlds = New DataSet
            'Sqlda.Fill(Sqlds)

            ''Sqlds.Tables(0).TableName = "DefaultConfig"
            'Sqlds.Tables(0).TableName = "MstDocumentTypes"

            'For Each drDocType As DataRow In Sqlds.Tables("MstDocumentTypes").Rows
            '    If drDocType("DocumentTypeDesc") = "Sales Order" Then
            '        _SOCreation = drDocType("DocumentType")
            '    End If
            '    If drDocType("DocumentTypeDesc") = "Sales Order Return" Then
            '        _SOReturn = drDocType("DocumentType")
            '    End If
            'Next

            Return ""

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Get Information of Printing format, Layout and Type, etc.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPrintingDetail() As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select * from PrintingDetail Where DocumentType='SO201'")
            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, ConString)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Return Sqlds.Tables(0)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' Get Next Sales Invoice Number
    ''' </summary>
    ''' <param name="vSiteCode">SiteCode</param>
    ''' <param name="DocType">Document Type</param>
    ''' <returns>InvoiceNo</returns>
    ''' <UsedBy>frmSalesOrderCreation.vb, frmSalesOrderUpdation.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function GetNextSOInvcNo(ByVal vSiteCode As String, Optional ByVal DocType As String = "") As String

        Try
            OpenConnection()
            vStmtQry.Length = 0

            vStmtQry.Append("Select IsNull(Max(SaleInvNumber) ,'1200000001')+1 as InvoiceNo From SalesInvoice " & vbCrLf)
            vStmtQry.Append(" Where SiteCode='" & vSiteCode & "' " & vbCrLf)
            If Not (DocType = String.Empty) Then
                vStmtQry.Append(" And DocumentType='" & DocType & "' " & vbCrLf)
            End If

            Sqlcmd = New SqlClient.SqlCommand(vStmtQry.ToString, SpectrumCon)
            SalesInvcNo = Sqlcmd.ExecuteScalar

            Sqlcmd.Dispose()
            CloseConnection()

            Return SalesInvcNo

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Get Customer Information for Order
    ''' </summary>
    ''' <param name="vCustmCode">Customer Code</param>
    ''' <returns>Customer Table</returns>
    ''' <UsedBy>frmSalesOrderCreation.vb, frmSalesOrderUpdation.vb, frmSalesOrderCancel.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function GetSOCustmerInfo(ByVal vCustmCode As String) As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select cd.CustomerType, cd.CustomerNo, ad.AddressType, dbo.FnGetDesc('Address',ad.AddressType,'000') as AddressTypeName, " & vbCrLf)
            vStmtQry.Append(" dbo.FnGetDesc('TITLE',cd.TitleCode,'000') + ' ' +  cd.FirstName + ' ' + cd.LastName as CustomerName, " & vbCrLf)
            vStmtQry.Append(" cd.ResidencePhone, cd.MobilePhone, cd.OfficePhone, cd.EmailId, " & vbCrLf)
            vStmtQry.Append(" ad.AddressLn1 + ', ' + ad.AddressLn2 + ', ' + ad.AddressLn3 + ', ' + ad.AddressLn4 as AddressLn1 , " & vbCrLf)
            vStmtQry.Append(" dbo.FnGetLocnDesc(ad.CityCode) + ', ' + dbo.FnGetLocnDesc(ad.StateCode) + ', ' + " & vbCrLf)
            vStmtQry.Append(" dbo.FnGetLocnDesc(ad.CountryCode) + ', ' + ad.PinCode as AddressLn2 " & vbCrLf)
            vStmtQry.Append(" From CustomerSaleOrder cd, CustomerAddress ad Where cd.CustomerNo=ad.CustomerNo and " & vbCrLf)
            vStmtQry.Append(" ad.AddressType in (1,2) and ad.Status='1' and cd.CustomerNo='" & vCustmCode & "' " & vbCrLf)
            vStmtQry.Append(" Union " & vbCrLf)

            vStmtQry.Append(" Select 2 as CustomerType, cd.AccountNo as CustomerNo, ad.AddressType, dbo.FnGetDesc('Address',ad.AddressType,'000') as AddressTypeName, " & vbCrLf)
            vStmtQry.Append(" cd.TITLE + ' ' +  cd.FirstName + ' ' + cd.SurName as CustomerName, " & vbCrLf)
            vStmtQry.Append(" cd.Res_Phone as ResidencePhone, cd.MobileNo as MobilePhone,'' as OfficePhone,cd.EmailId, " & vbCrLf)
            vStmtQry.Append(" ad.AddressLn1 + ', ' + ad.AddressLn2 + ', ' + ad.AddressLn3 + ', ' + ad.AddressLn4 as AddressLn1, " & vbCrLf)
            vStmtQry.Append(" dbo.FnGetLocnDesc(ad.CityCode) + ', ' + dbo.FnGetLocnDesc(ad.StateCode) + ', ' + " & vbCrLf)
            vStmtQry.Append(" dbo.FnGetLocnDesc(ad.CountryCode) + ', ' + ad.PinCode as AddressLn2 " & vbCrLf)
            vStmtQry.Append(" From CLPCustomers cd, CLPCustomerAddress ad Where cd.CardNo=ad.CardNo and cd.ClpProgramId=ad.ClpProgramId " & vbCrLf)
            vStmtQry.Append(" And ad.AddressType in (1,2) and cd.Status='1' and cd.CardNo='" & vCustmCode & "' " & vbCrLf)

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Return Sqlds.Tables(0)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Get Sales Order Information
    ''' </summary>
    ''' <returns>Sales DataSet</returns>
    ''' <UsedBy>frmSearchSO.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function GetSearchSalesOrder(ByVal siteCode As String, Optional ByVal RequiredStatus As Boolean = False, Optional ByVal IsOtherLocationDelivery As Boolean = False) As DataSet
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select Distinct sHdr.SaleOrderNumber as SalesNo,")
            '--changed by rama for bug 811,1155
            If RequiredStatus = True Then
                vStmtQry.Append(" sHdr.SOStatus AS Status,")
            End If
            '--
            'vStmtQry.Append(" dbo.FnGetDesc('Terminal',sHdr.TerminalID,sHdr.Sitecode) as TerminalID, " & vbCrLf)
            vStmtQry.Append(" Case When sHdr.CustomerType='CLP' Then clpInfo.SurName +' '+ clpInfo.FirstName " & vbCrLf)
            vStmtQry.Append(" When sHdr.CustomerType='SO' Then cInfo.CustomerName End as CustomerName, " & vbCrLf)
            vStmtQry.Append(" sHdr.CustomerNo, sHdr.CreatedOn as SalesDate, sHdr.PromisedDeliveryDate as DeliveryDate,  " & vbCrLf)
            vStmtQry.Append(" sHdr.CreatedBy as CashierName, sp.SalesPersonFullName as SalesPerson,sHdr.NetAmt AS Amount " & vbCrLf)
            vStmtQry.Append("  from SalesOrderHDR sHdr " & vbCrLf)
            vStmtQry.Append(" inner join SalesOrderDtl sdtl on sHdr .SiteCode = sdtl.SiteCode and sHdr.SaleOrderNumber = sdtl.SaleOrderNumber " & vbCrLf)
            vStmtQry.Append(" Left Join SalesInvoice sInvc on sHdr.SaleOrderNumber=sInvc.DocumentNumber and sInvc.status=1 " & vbCrLf)
            vStmtQry.Append(" Left Join CustomerSaleOrder cInfo on sHdr.CustomerNo=cInfo.CustomerNo " & vbCrLf)
            vStmtQry.Append(" Left Join CLPCustomers clpInfo on sHdr.CustomerNo=clpInfo.CardNo " & vbCrLf)
            vStmtQry.Append(" Left Join MstSalesPerson sp on sHdr.SalesExecutiveCode=sp.EmpCode " & vbCrLf)
            vStmtQry.Append(" Where  sHdr.SOStatus Not In " & stmtSalesStatus & vbCrLf)
            If IsOtherLocationDelivery Then
                vStmtQry.Append(" and sdtl.DeliverySiteCode='" & siteCode & "' and sdtl .SiteCode<>'" & siteCode & "' " & vbCrLf)
            Else
                vStmtQry.Append(" and sdtl.SiteCode='" & siteCode & "' " & vbCrLf)
            End If
            vStmtQry.Append(" Order By SalesDate Desc, SalesNo Desc " & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dsScan = New DataSet
            daScan.Fill(dsScan, "SalesOrderSearch")

            Return dsScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    Public Function SearchSalesOrderWithPendingDelivery(ByVal siteCode As String) As DataSet
        Try
            Dim vStmtQry As String = "Select Distinct sHdr.SaleOrderNumber as SalesNo, Case When sHdr.CustomerType='CLP' Then clpInfo.SurName +' '+ clpInfo.FirstName " & _
                                     "When sHdr.CustomerType='SO' Then cInfo.CustomerName End as CustomerName, " & _
                                     "sHdr.CustomerNo, sHdr.CreatedOn as SalesDate, sHdr.PromisedDeliveryDate as DeliveryDate,  " & _
                                     "sHdr.CreatedBy as CashierName, sp.SalesPersonFullName as SalesPerson,sHdr.NetAmt AS Amount " & _
                                     "from SalesOrderHDR sHdr " & _
                                     "Inner join dbo.SalesOrderDeliveryLocInfo sodl on sHdr.SaleOrderNumber = sodl.SalesOrderNumber " & _
                                     "Left Join SalesInvoice sInvc on sHdr.SaleOrderNumber=sInvc.DocumentNumber and sInvc.status =1" & _
                                     "Left Join CustomerSaleOrder cInfo on sHdr.CustomerNo=cInfo.CustomerNo " & _
                                     "Left Join CLPCustomers clpInfo on sHdr.CustomerNo=clpInfo.CardNo " & _
                                     "Left Join MstSalesPerson sp on sHdr.SalesExecutiveCode=sp.EmpCode " & _
                                     "Where  sHdr.SOStatus Not In ('Cancel','Closed','Return') and sodl.DeliverySiteCode = '" & siteCode & "' Order By SalesNo Desc "

            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dsScan = New DataSet
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
    Public Function SetSalesOrderInSOCancel(ByVal vSiteNo As String, ByVal vSalesNo As String) As DataSet

        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select A.ArticleCode ,A.DeliverySiteCode,A.BillLineNo,A.BatchBarcode , dbo.FnGetEANDesc(A.ArticleCode) as Discription, A.SellingPrice, A.Quantity, 0 as PickupQty, " & vbCrLf)
            vStmtQry.Append(" A.DeliveredQty, A.DiscountPercentage,A.DiscountAmount as Discount, A.NetAmount,A.CostAmount, A.ActualDeliveryDate as ExpDelDate, " & vbCrLf)
            vStmtQry.Append(" A.ReservedQty, A.LineDiscount, 50 as Stock, A.EAN , " & vbCrLf)
            vStmtQry.Append(" A.SellingPrice*A.Quantity as GrossAmt, A.ExclTaxAmt as ExclTaxAmt,A.TotalTaxAmount, A.Status, " & vbCrLf)
            vStmtQry.Append(" A.UnitOfMeasure, A.OfferNo, A.SaleOrderNumber,A.SalesStaffID, A.CreatedOn, A.IsCLPApplicable, A.ClpPoints, A.ClpDiscount,isnull(B.FreezeSB,0)as FreezeSB,isnull(B.FreezeSR,0)as FreezeSR,isnull(B.FreezeOB,0)as FreezeOB" & vbCrLf)
            vStmtQry.Append(" from SalesOrderDTL  A Inner join SalesInfoRecord B on A.SiteCode=B.SiteCode AND A.EAN=B.EAN AND A.ArticleCode=B.ArticleCode AND B.Srno=1 Where  A.SiteCode='" & vSiteNo & "' and A.SaleOrderNumber='" & vSalesNo & "' " & vbCrLf)
            '-- added by rama ranjan for bug no 0000566
            vStmtQry.Append(" AND isnull(A.ArticleStatus,'')<>'Deleted' AND isnull(A.Status,0)<>0 " & vbCrLf)
            '--
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dsScan = New DataSet
            daScan.Fill(dsScan, "ItemScanDetails")

            Return dsScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Search a Sales Invoice Information
    ''' </summary>
    ''' <param name="vSiteNo"> Site Code</param>
    ''' <param name="vSalesNo"> Sales Order Number</param>
    ''' <returns>Sales DataSet</returns>
    ''' <UsedBy>frmSalesOrderCancel.vb, frmSalesOrderUpdation.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function SetInvoiceInSOCancel(ByVal vSiteNo As String, ByVal vSalesNo As String) As DataSet
        Try
            vStmtQry.Length = 0

            vStmtQry.Append(" Select DocumentNumber as SalesNo, SaleInvNumber as InvoiceNo, DocumentType, " & vbCrLf)
            vStmtQry.Append(" dbo.FnGetDesc('Terminal',TerminalID,SiteCode) as TerminalID, " & vbCrLf)
            vStmtQry.Append(" TenderTypeCode as TenderType, " & vbCrLf)
            vStmtQry.Append(" AmountTendered as InvoiceAmt, UserName, SOInvTime as InvoiceDate " & vbCrLf)
            vStmtQry.Append(" From SalesInvoice Where status =1 and SiteCode='" & vSiteNo & "' And DocumentNumber='" & vSalesNo & "' " & vbCrLf)

            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dsScan = New DataSet
            daScan.Fill(dsScan, "InvoiceDetails")

            Return dsScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' m
    ''' </summary>
    ''' <param name="vSalesNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateSalesOrderCancel(ByVal vSalesNo As String) As DataRow
        vStmtQry.Length = 0
        vStmtQry.Append(" Select Distinct sHdr.SaleOrderNumber, sHdr.AdvanceAmt, sHdr.BalanceAmount, sHdr.DiscountAmt, " & vbCrLf)
        vStmtQry.Append(" sHdr.NetAmt, Sum(sDtl.NetSellingPrice) as GrossAmount, sHdr.SOStatus " & vbCrLf)
        vStmtQry.Append(" from SalesOrderHDR sHdr " & vbCrLf)
        vStmtQry.Append(" Inner Join SalesOrderDTL sDtl on sHdr.SaleOrderNumber=sDtl.SaleOrderNumber " & vbCrLf)
        vStmtQry.Append(" And sHdr.SOStatus=sDtl.ArticleStatus " & vbCrLf)
        vStmtQry.Append(" Left Join SalesInvoice sInvc on sHdr.SaleOrderNumber=sInvc.SaleOrderNumber and sInvc.status =1 " & vbCrLf)
        vStmtQry.Append(" Where  sHdr.SaleOrderNumber ='" & vSalesNo & "' " & vbCrLf)
        vStmtQry.Append(" Group By sHdr.SaleOrderNumber, sHdr.SOStatus,sHdr.NetAmt, " & vbCrLf)
        vStmtQry.Append(" sHdr.AdvanceAmt, sHdr.BalanceAmount, sHdr.DiscountAmt " & vbCrLf)

        Sqlda = New SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
        Sqlds = New DataSet
        Sqlda.Fill(Sqlds)

        Sqldr = Nothing
        If Sqlds.Tables(0).Rows.Count > 0 Then
            Sqldr = Sqlds.Tables(0).Rows(0)
        End If

        Return Sqldr
    End Function

    ''' <summary>
    ''' n
    ''' </summary>
    ''' <param name="DocType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetReason(ByVal DocType As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim daReturn As New SqlDataAdapter("SELECT REASONCODE,REASONNAME FROM REASONS where Doctype='" & DocType & "'", ConString)
            daReturn.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function SaveDiscount(ByVal DayOpendate As DateTime, ByVal dtSalesDetails As DataTable, ByVal strSalesInvoiceNo As String, ByVal tran As SqlTransaction) As Boolean
        Try
            If Not dtSalesDetails Is Nothing Then
                Dim strSiteCode As String
                Dim strCREATEDBY As String
                Dim strFinYear As String
                Dim strPromotionID As String
                Dim strPromotionValue As Decimal
                Dim dateCreated As Date
                Dim decTotalPromotionvalue As Decimal
                If dtSalesDetails.Rows.Count > 0 Then
                    If Not dtSalesDetails.Rows(0)("SiteCode") Is DBNull.Value Then
                        strSiteCode = dtSalesDetails.Rows(0)("SiteCode")
                    End If
                    If Not dtSalesDetails.Rows(0)("FinYear") Is DBNull.Value Then
                        strFinYear = dtSalesDetails.Rows(0)("SiteCode")
                    End If
                    If Not dtSalesDetails.Rows(0)("CREATEDBY") Is DBNull.Value Then
                        strCREATEDBY = dtSalesDetails.Rows(0)("CREATEDBY")
                    End If
                    If Not dtSalesDetails.Rows(0)("CreatedOn") Is DBNull.Value Then
                        dateCreated = dtSalesDetails.Rows(0)("CreatedOn")
                    End If

                    Dim objclsCommon As New clsCommon

                    Dim disc As Double
                    disc = IIf(dtSalesDetails.Compute("Sum(DiscountAmount)", "") Is DBNull.Value, 0, dtSalesDetails.Compute("Sum(DiscountAmount)", ""))
                    'If disc > 0 Then
                    Dim dtDtl As DataTable = dtSalesDetails.Copy()
                    Dim dtDisc As DataTable = objclsCommon.CreateDiscSummary(DayOpendate, dtDtl, "", "SO", strSiteCode, strFinYear, strCREATEDBY, strCREATEDBY, dateCreated, "FIRSTLEVEL", "TOPLEVEL")
                    If Not dtDisc Is Nothing AndAlso dtDisc.Rows.Count > 0 Then
                        dsScan.Tables.Add(dtDisc)
                        Return True
                    End If
                    'End If

                    'If (objclsBirthListGlobal.InsertQuery("Insert into dbo.SalesDiscDtl(SiteCode,FinYear,BillNo,documentType,PromotionID,PromotionValue,CREATEDBY,CREATEDAT,CREATEDON,UPDATEDBY,UPDATEDAT,UPDATEDON,STATUS ) values ('" + strSiteCode + "','" + strFinYear + "','" + strSalesInvoiceNo + "','SO','" + strPromotionID + "'," & strPromotionValue & ",'" + strCREATEDBY + "','" + strSiteCode + "','" + dateCreated + "',' ','','','1')", tran)) Then
                    '    Return True
                    'Else
                    '    Return False
                    'End If
                End If

            Else
                Return False
            End If



        Catch ex As Exception
            LogException(ex)
            Return False

        End Try
    End Function

    Public Function GetAllOrderInfo(ByVal pSiteCode As String, ByVal pFinYear As String, ByVal pDocNo As String, ByVal pDocType As String) As DataSet
        Try
            vStmtQry.Length = 0
            If pDocType = "SalesOrder" Then
                vStmtQry.Append("Select si.DocumentNumber, si.SaleInvNumber, si.TerminalID, si.TenderTypeCode, si.AmountTendered, si.ExchangeRate, " & vbCrLf)
                vStmtQry.Append("si.CurrencyCode, Convert(Varchar(10),si.SOInvDate,105) as SOInvDate, si.UserName, so.CustomerNo, so.CustomerType " & vbCrLf)
                vStmtQry.Append("from SalesInvoice si Inner join SalesOrderHdr so on si.DocumentNumber =so.SaleOrderNumber " & vbCrLf)
                vStmtQry.Append("Where si.status =1 and si.SiteCode='" & pSiteCode & "' And si.FinYear='" & pFinYear & "' " & vbCrLf)
                vStmtQry.Append("And si.DocumentNumber='" & pDocNo & "' And so.SOStatus='Open' ; " & vbCrLf & vbCrLf)


                vStmtQry.Append("Select sd.ArticleCode, am.ArticleName, sd.Quantity as OrderQty, sd.DeliveredQty, " & vbCrLf)
                vStmtQry.Append("0 as PickupQty, sd.SellingPrice As Rate, sd.NetAmount, 0.0 as PickupAmt, sd.GrossAmount, sd.EAN, sd.UnitofMeasure, sd.LineDiscount, sd.ExclTaxAmt " & vbCrLf)
                vStmtQry.Append("from SalesOrderDtl sd Inner join SalesOrderHdr sh on sd.SaleOrderNumber =sh.SaleOrderNumber " & vbCrLf)
                vStmtQry.Append("Inner Join MstArticle am on sd.ArticleCode=am.ArticleCode " & vbCrLf)
                vStmtQry.Append("Where sd.SiteCode='" & pSiteCode & "' And sd.FinYear='" & pFinYear & "' " & vbCrLf)
                vStmtQry.Append("And sd.SaleOrderNumber='" & pDocNo & "' And sh.SOStatus='Open' ; " & vbCrLf)

            ElseIf pDocType = "BirthList" Then

                'vStmtQry.Append("Select si.DocumentNumber, si.SaleInvNumber, si.TerminalID, si.TenderTypeCode, si.AmountTendered, si.ExchangeRate, " & vbCrLf)
                'vStmtQry.Append("si.CurrencyCode, Convert(Varchar(10),si.SOInvDate,105) as SOInvDate, si.UserName, bl.CustomerId As CustomerNo, bl.CustomerType " & vbCrLf)
                'vStmtQry.Append("from SalesInvoice si Inner join BirthList bl on si.DocumentNumber =bl.BirthListId " & vbCrLf)
                'vStmtQry.Append("Where si.SiteCode='" & pSiteCode & "' And si.FinYear='" & pFinYear & "' " & vbCrLf)
                'vStmtQry.Append("And si.DocumentNumber='" & pDocNo & "' And bl.BirthListStatus<>'Closed'; " & vbCrLf & vbCrLf)

                'vStmtQry.Append("Select bh.ArticleCode, am.ArticleName, bh.BookedQty as OrderQty, bh.DeliveredQty, 0 as PickupQty, " & vbCrLf)
                'vStmtQry.Append("Rate, bh.NetAmt as NetAmount, 0 as PickupAmt, bh.NetAmt as GrossAmount, bh.EAN, bh.SaleInvNumber, " & vbCrLf)
                'vStmtQry.Append("am.SaleUnitofMeasure as UnitofMeasure, IsNull(bh.TotalDiscountAmt,0) as LineDiscount, bh.TaxAmount as ExclTaxAmt, bh.CustomerId as CustomerNo, bh.CustomerType " & vbCrLf)
                'vStmtQry.Append("from BirthListSalesDtl bh Inner Join MstArticle am on bh.ArticleCode=am.ArticleCode " & vbCrLf)
                'vStmtQry.Append("Inner join BirthList bl on bh.BirthListId =bl.BirthListId " & vbCrLf)
                'vStmtQry.Append("Where bh.SiteCode='" & pSiteCode & "' And bh.FinYear='" & pFinYear & "' " & vbCrLf)
                'vStmtQry.Append("And bh.BirthListId='" & pDocNo & "' And bl.BirthListStatus<>'Closed' " & vbCrLf)

                vStmtQry.Append("Select blr.ArticleCode, am.ArticleName, blr.RequstedQty as OrderQty, blr.BookedQty, " & vbCrLf)
                vStmtQry.Append("blr.DeliveredQty, 0 as PickupQty from BirthListRequestedItems blr " & vbCrLf)
                vStmtQry.Append("Inner Join MstArticle am on blr.ArticleCode=am.ArticleCode " & vbCrLf)
                vStmtQry.Append("Where blr.SiteCode='" & pSiteCode & "' And blr.FinYear='" & pFinYear & "' " & vbCrLf)
                vStmtQry.Append("And blr.BirthListId='" & pDocNo & "' And blr.BookedQty>0 ;" & vbCrLf)

                vStmtQry.Append("Select * from BirthList Where SiteCode='" & pSiteCode & "' And FinYear='" & pFinYear & "' " & vbCrLf)
                vStmtQry.Append("And BirthListId='" & pDocNo & "' ;" & vbCrLf)

            End If

            Sqlda = New SqlDataAdapter(vStmtQry.ToString, ConString)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Return Sqlds

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    Public Function GetStructOutboundDelivery(ByVal vSiteCode As String, ByVal vFinYear As String, ByVal vSalesNo As String, ByVal vDocType As String) As DataSet
        Try
            vStmtQry.Length = 0

            vStmtQry.Append("Select * From OrderHdr Where SiteCode='" & vSiteCode & "' And FinYear='" & vSalesNo & "' " & vbCrLf)
            vStmtQry.Append("And DocumentNumber='0'; " & vbCrLf & vbCrLf)
            vStmtQry.Append("Select * From OrderDtl Where SiteCode='" & vSiteCode & "' And FinYear='" & vSalesNo & "' " & vbCrLf)
            vStmtQry.Append("And DocumentNumber='0'; " & vbCrLf & vbCrLf)

            If vDocType = "SalesOrder" Then
                vStmtQry.Append("Select * From SalesOrderHdr Where SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "' " & vbCrLf)
                vStmtQry.Append("And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf & vbCrLf)

                vStmtQry.Append("Select * From SalesOrderDtl Where SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "' " & vbCrLf)
                vStmtQry.Append("And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf & vbCrLf)

            ElseIf vDocType = "BirthList" Then
                vStmtQry.Append("Select * From BirthListRequestedItems Where SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "' " & vbCrLf)
                vStmtQry.Append("And BirthListId='" & vSalesNo & "'; " & vbCrLf & vbCrLf)

                vStmtQry.Append("Select * From BirthListSalesHdr Where SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "' " & vbCrLf)
                vStmtQry.Append("And BirthListId='" & vSalesNo & "'; " & vbCrLf & vbCrLf)

                vStmtQry.Append("Select * From BirthListSalesDtl Where SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "' " & vbCrLf)
                vStmtQry.Append("And BirthListId='" & vSalesNo & "'; " & vbCrLf & vbCrLf)
            End If

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Sqlds.Tables(0).TableName = "OrderHdr"
            Sqlds.Tables(1).TableName = "OrderDtl"

            If vDocType = "SalesOrder" Then
                Sqlds.Tables(2).TableName = "SalesOrderHdr"
                Sqlds.Tables(3).TableName = "SalesOrderDtl"
            ElseIf vDocType = "BirthList" Then
                Sqlds.Tables(2).TableName = "BirthListRequestedItems"
                Sqlds.Tables(3).TableName = "BirthListSalesDtl"
                Sqlds.Tables(4).TableName = "BirthListSalesDtl"
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
                ' If objComn.UpdateDocumentNo("OutboundDelivery", SpectrumCon, SqlTranOD) = False Then
                If objComn.UpdateDocumentNo("OutBound", SpectrumCon, SqlTranOD) = False Then
                    SqlTrans.Rollback()
                    CloseConnection()
                    Return False
                End If

                For Each dr As DataRow In dsODMain.Tables(1).Select("DeliveredQty>0")
                    If objComn.UpdateStock(dr("SiteCode").ToString, dr("ArticleCode").ToString(), dr("EAN").ToString(), dr("UnitofMeasure").ToString(), dr("DeliveredQty").ToString(), dr("CreatedAt"), SpectrumCon, SqlTranOD, vLocation, 0) = False Then
                        SqlTrans.Rollback()
                        CloseConnection()
                        Return False
                    End If
                Next
            Else
                SqlTrans.Rollback()
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
                Else
                    vStmtQry.Append("SELECT " + tableColumns & " FROM " & tableName)
                    vStmtQry.Append(" Where SiteCode = '" & vSiteCode & "' and DocumentNumber  = '" & vOutbNo & "' " & vbCrLf)
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

    Public Function GetOutboundData(ByVal SiteCode As String, ByVal SONumber As String) As DataTable
        Sqlda = New SqlClient.SqlDataAdapter("select BarCode,sum(DeliveredQty)DeliveredQty from OrderDtl where SaleOrderNumber='" & SONumber & "' AND SiteCode='" & SiteCode & "'  group by BarCode", SpectrumCon)
        Sqlcmdb = New SqlCommandBuilder(Sqlda)
        Dim sqldt As New DataTable
        Sqlda.Fill(sqldt)
        Return sqldt
    End Function

    Public Sub GetSOBulkComboTablestructure(ByRef DtSoBulkComboHdr As DataTable, ByRef DtSoBulkComboDtl As DataTable)
        Try
            '------  DtSoBulkComboHdr()
            DtSoBulkComboHdr.Columns.Add("BulkComboMstId", Type.GetType("System.Int64"))
            DtSoBulkComboHdr.Columns.Add("SaleOrderNumber", Type.GetType("System.String"))
            DtSoBulkComboHdr.Columns.Add("sitecode", Type.GetType("System.String"))
            DtSoBulkComboHdr.Columns.Add("ComboSrNo", Type.GetType("System.Int16"))
            DtSoBulkComboHdr.Columns.Add("PackagingBoxCode", Type.GetType("System.String"))
            DtSoBulkComboHdr.Columns.Add("PackagingBox", Type.GetType("System.String"))
            DtSoBulkComboHdr.Columns.Add("OrderQty", Type.GetType("System.Decimal"))
            DtSoBulkComboHdr.Columns.Add("AdditionComments", Type.GetType("System.String"))
            DtSoBulkComboHdr.Columns.Add("CREATEDAT", Type.GetType("System.String"))
            DtSoBulkComboHdr.Columns.Add("CREATEDBY", Type.GetType("System.String"))
            DtSoBulkComboHdr.Columns.Add("CREATEDON", Type.GetType("System.DateTime"))
            DtSoBulkComboHdr.Columns.Add("UPDATEDAT", Type.GetType("System.String"))
            DtSoBulkComboHdr.Columns.Add("UPDATEDBY", Type.GetType("System.String"))
            DtSoBulkComboHdr.Columns.Add("UPDATEDON", Type.GetType("System.DateTime"))
            DtSoBulkComboHdr.Columns.Add("STATUS", Type.GetType("System.Int16"))
            ''-------- DtSoBulkComboDtl
            DtSoBulkComboDtl.Columns.Add("BulkComboDetId", Type.GetType("System.Int64"))
            DtSoBulkComboDtl.Columns.Add("BulkComboMstId", Type.GetType("System.Int64"))
            DtSoBulkComboDtl.Columns.Add("ArticleCode", Type.GetType("System.String"))
            DtSoBulkComboDtl.Columns.Add("ArticleDescription", Type.GetType("System.String"))
            DtSoBulkComboDtl.Columns.Add("EAN", Type.GetType("System.String"))
            DtSoBulkComboDtl.Columns.Add("PackagedUOM", Type.GetType("System.String"))
            DtSoBulkComboDtl.Columns.Add("Qty", Type.GetType("System.Decimal"))
            DtSoBulkComboDtl.Columns.Add("Weight", Type.GetType("System.Decimal"))
            DtSoBulkComboDtl.Columns.Add("STRQty", Type.GetType("System.Decimal"))
            DtSoBulkComboDtl.Columns.Add("STRExcludeCheck", Type.GetType("System.Boolean"))
            DtSoBulkComboDtl.Columns.Add("BaseUOM", Type.GetType("System.String"))
            DtSoBulkComboDtl.Columns.Add("ItemQtyBaseUOM", Type.GetType("System.Decimal"))
            DtSoBulkComboDtl.Columns.Add("CREATEDAT", Type.GetType("System.String"))
            DtSoBulkComboDtl.Columns.Add("CREATEDBY", Type.GetType("System.String"))
            DtSoBulkComboDtl.Columns.Add("CREATEDON", Type.GetType("System.DateTime"))
            DtSoBulkComboDtl.Columns.Add("UPDATEDAT", Type.GetType("System.String"))
            DtSoBulkComboDtl.Columns.Add("UPDATEDBY", Type.GetType("System.String"))
            DtSoBulkComboDtl.Columns.Add("UPDATEDON", Type.GetType("System.DateTime"))
            DtSoBulkComboDtl.Columns.Add("STATUS", Type.GetType("System.Int16"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function GetPatientPrescriptionTableStructure(ByVal DtTable As DataTable) As DataTable
        DtTable.Columns.Add("SrNo", Type.GetType("System.String"))
        DtTable.Columns.Add("ArticleCode", Type.GetType("System.String"))
        DtTable.Columns.Add("ArticleDescription", Type.GetType("System.String"))
        DtTable.Columns.Add("EAN", Type.GetType("System.String"))
        DtTable.Columns.Add("Qty", Type.GetType("System.Decimal"))
        DtTable.Columns.Add("ConsumptionRate", Type.GetType("System.String"))
        DtTable.Columns.Add("Duration", Type.GetType("System.String"))
        DtTable.Columns.Add("Remarks", Type.GetType("System.String"))
        DtTable.Columns.Add("ConsultantsNoteId", Type.GetType("System.String"))
        DtTable.Columns.Add("PatientId", Type.GetType("System.String"))
        DtTable.Columns.Add("SiteCode", Type.GetType("System.String"))
        DtTable.Columns.Add("NoteSrNo", Type.GetType("System.Int16"))
        DtTable.Columns.Add("CREATEDAT", Type.GetType("System.String"))
        DtTable.Columns.Add("CREATEDBY", Type.GetType("System.String"))
        DtTable.Columns.Add("CREATEDON", Type.GetType("System.DateTime"))
        DtTable.Columns.Add("UPDATEDAT", Type.GetType("System.String"))
        DtTable.Columns.Add("UPDATEDBY", Type.GetType("System.String"))
        DtTable.Columns.Add("UPDATEDON", Type.GetType("System.DateTime"))
        DtTable.Columns.Add("STATUS", Type.GetType("System.Int16"))
        Return DtTable
    End Function

    Public Function GetSOBulkComboTableStruct(ByVal vSiteCode As String, ByVal vSalesNo As String, Optional ByVal vStatus As String = "") As DataSet
        Try
            vStmtQry.Length = 0

            'vStmtQry.Append(" Select * From SoBulkComboHdr Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            'vStmtQry.Append(" Select BCD.* From SoBulkComboHdr as BCH Inner Join SoBulkComboDtl as BCD on  BCH.BulkComboMstId = BCD.BulkComboMstId  Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            vStmtQry.Append(" SELECT  SBCH.BulkComboMstId, SBCH.SaleOrderNumber, SBCH.siteCode, SBCH.ComboSrNo,  ")
            vStmtQry.Append(" 		SBCH.PackagingBoxCode, MA.ArticleShortName AS PackagingBox ,SOD.Quantity AS OrderQty,   ")
            vStmtQry.Append("         SBCH.AdditionComments, SBCH.CREATEDAT, SBCH.CREATEDBY, SBCH.CREATEDON, SBCH.UPDATEDAT,   ")
            vStmtQry.Append("         SBCH.UPDATEDBY, SBCH.UPDATEDON, SBCH.STATUS  ")
            vStmtQry.Append(" FROM    SoBulkComboHdr AS SBCH INNER JOIN  ")
            vStmtQry.Append("                       SalesOrderDtl AS SOD ON SBCH.siteCode = SOD.SiteCode   ")
            vStmtQry.Append("                       AND SBCH.SaleOrderNumber = SOD.SaleOrderNumber   ")
            vStmtQry.Append("                       AND SBCH.PackagingBoxCode = SOD.ArticleCode AND   ")
            vStmtQry.Append("                       SBCH.ComboSrNo = SOD.BillLineNo   ")
            vStmtQry.Append("         INNER JOIN MstArticle AS MA ON SBCH.PackagingBoxCode = MA.ArticleCode  ")
            vStmtQry.Append(" WHERE  (SBCH.siteCode = '" & vSiteCode & "') AND (SBCH.SaleOrderNumber = '" & vSalesNo & "')")

            vStmtQry.Append(" Select BCD.* From SoBulkComboHdr as BCH Inner Join SoBulkComboDtl as BCD on  BCH.BulkComboMstId = BCD.BulkComboMstId  Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Sqlds.Tables(0).TableName = "SoBulkComboHdr"
            Sqlds.Tables(1).TableName = "SoBulkComboDtl"

            Return Sqlds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


End Class
