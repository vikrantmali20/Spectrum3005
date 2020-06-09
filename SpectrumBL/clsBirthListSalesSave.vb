Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing
Imports System.Text


''' <summary>
'''  Class is used to save salesInformation aginst BirthList and also used to update/Edit changes to BirthList
'''  <UsedBY>frmBirthListSales.vb,frmBirthListUpdation.vb</UsedBY>
''' </summary>
''' <remarks></remarks>
Public Class clsBirthListSalesSave
    Public dsBirthListSales As New DataSet
    Private objClsSalesOrder As New clsSalesOrder
    Private _SiteCode As String
    Private _TerminalID As String
    Private _UserName As String
    Private _BirthLisID As String
    Private _CustomerID As String
    Private _PaidAmount As Decimal
    Private _dtPaymentHistory As DataTable
    Private _dtBirthListItemDetail As DataTable
    Private _drSelectedCutomerinfo As DataRow
    Private objclsComman As New clsCommon
    Private _dtVoucherSales As DataTable
    Private _isPrintHeader As Boolean = False
    Private _isPrintFooter As Boolean = False
    Private _OpenAmount As Decimal
    Private _RelativesPoint As Boolean = False
    Private _ParentCardNo As String
    Private _ParentCLPProg As String

    'Changed by Rohit for Defect 6013
    'Private BirthListOutBoundCode As String = "OB"
    Private BirthListOutBoundCode As String = "BLOBD"


    Private BirthListOutBoundName As String = "OutBound"
    Private _IsCLPCalculate As Boolean = False
    Public IsMAP As Boolean = False

    Private _strRemarks As String
    Private _prevFinalcialYear As String

    Public Property ParentCLPProg As String
        Get
            Return _ParentCLPProg
        End Get
        Set(ByVal value As String)
            _ParentCLPProg = value
        End Set
    End Property


    Public Property ParentCardNo As String
        Get
            Return _ParentCardNo
        End Get
        Set(ByVal value As String)
            _ParentCardNo = value
        End Set

    End Property

    Public Property RelativesPoint As Boolean
        Get
            Return _RelativesPoint

        End Get
        Set(ByVal value As Boolean)
            _RelativesPoint = value
        End Set
    End Property



    Public Property PreviousFinancialYear As String
        Get
            Return _prevFinalcialYear
        End Get
        Set(ByVal value As String)
            _prevFinalcialYear = value
        End Set
    End Property

    Public Property strRemarks As String
        Get
            Return _strRemarks
        End Get
        Set(ByVal value As String)
            _strRemarks = value
        End Set
    End Property

    Private _dDueDate As Date
    Public Property dDueDate As Date
        Get
            Return _dDueDate
        End Get
        Set(ByVal value As Date)
            _dDueDate = value
        End Set
    End Property

    Dim _supplier As String
    Public Property supplier() As String
        Get
            Return _supplier
        End Get
        Set(ByVal value As String)
            _supplier = value
        End Set
    End Property

    Public Property IsCLPCalculate() As Boolean
        Get
            Return _IsCLPCalculate
        End Get
        Set(ByVal value As Boolean)
            _IsCLPCalculate = value
        End Set
    End Property

    Private dtMainTax As DataTable
    Public Property DataTableTaxDetails() As DataTable
        Get
            Return dtMainTax
        End Get
        Set(ByVal value As DataTable)
            dtMainTax = value
        End Set
    End Property


    ''' <summary>
    ''' Used when call constructor from frmBirthListUpdation
    ''' </summary>
    ''' <remarks></remarks>
    Private _IsUpdateBirthList As Boolean = False
    Dim _SelectedBirthListInfo As DataRow
    Dim _dtGV As DataTable
    ''' <summary>
    '''  SiteCode
    ''' </summary>
    ''' <value>String </value>
    '''  <UsedBY>frmBirthListSales.vb,frmBirthListUpdation.vb</UsedBY>
    ''' <returns></returns>
    ''' <remarks>Read,Write</remarks>
    Public Property SiteCode() As String
        Get
            Return _SiteCode
        End Get
        Set(ByVal value As String)
            _SiteCode = value
        End Set
    End Property

    ''' <summary>
    ''' CLP programid applicable for selected customer or currenct site
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CLPProgrmaID() As String
        Get
            Dim _CLPProgramID As String = ""
            Try
                _CLPProgramID = _drSelectedCutomerinfo("CLPPROGRAMID").ToString()
            Catch ex As Exception

            End Try

            Return _CLPProgramID
        End Get

    End Property

    Private _IsCLPCustomer As Boolean = False

    ''' <summary>
    '''  CLP point accumulate to selected customer or not 
    '''  CLP points given to only those customers having CLP.
    ''' </summary>
    ''' <value>Default is False.</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsCLPCustomer() As Boolean
        Get
            Return _IsCLPCustomer
        End Get
        Set(ByVal value As Boolean)
            _IsCLPCustomer = value
        End Set
    End Property


    ''' <summary>
    '''  Check Whether customer is CLP or SO 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CustomerType() As String
        Get
            If (IsCLPCustomer = True) Then
                Return "CLP"
            Else
                Return "SO"
            End If
        End Get

    End Property
    ''' <summary>
    ''' TerminalID
    ''' </summary>
    ''' <value>String</value>
    '''  <UsedBY>frmBirthListSales.vb,frmBirthListUpdation.vb</UsedBY>
    ''' <returns></returns>
    ''' <remarks>Read,Write</remarks>
    Public Property TerminalID() As String
        Get
            Return _TerminalID
        End Get
        Set(ByVal value As String)
            _TerminalID = value
        End Set
    End Property
    ''' <summary>
    ''' Must set whenever call this class functions from  frmBirthListUpdation.vb is "true "
    ''' </summary>
    ''' <UsedBY>**frmBirthListUpdation.vb</UsedBY>
    ''' <value>true</value>
    ''' <returns></returns>
    ''' <remarks>Only set at frmBirthListUpdation.vb form otherwise not need to set  </remarks>
    Public Property IsUpdateBirthList() As Boolean
        Get
            Return _IsUpdateBirthList
        End Get
        Set(ByVal value As Boolean)
            _IsUpdateBirthList = value
        End Set
    End Property
    ''' <summary>
    ''' User Name
    ''' </summary>
    ''' <value>String</value>
    ''' <UsedBY>frmBirthListSales.vb,frmBirthListUpdation.vb</UsedBY>
    ''' <returns></returns>  
    ''' <remarks>Read,Write</remarks>
    Public Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property
    ''' <summary>
    '''BirthLisID()
    ''' </summary>
    ''' <value>String</value>
    ''' <UsedBY>frmBirthListSales.vb,frmBirthListUpdation.vb</UsedBY>
    ''' <returns></returns>
    ''' <remarks>Read,Write</remarks>
    Public Property BirthLisID() As String
        Get
            Return _BirthLisID
        End Get
        Set(ByVal value As String)
            _BirthLisID = value
        End Set
    End Property




    ''' <summary>
    '''CustomerID
    ''' </summary>
    ''' <value>String</value>
    ''' <UsedBY>frmBirthListSales.vb,frmBirthListUpdation.vb</UsedBY>
    ''' <returns></returns>
    ''' <remarks>Read,Write</remarks>
    Public Property CustomerID() As String
        Get
            Return _CustomerID
        End Get
        Set(ByVal value As String)
            _CustomerID = value
        End Set
    End Property
    ''' <summary>
    ''' Total Paid amount 
    ''' </summary>
    ''' <value>String</value>
    ''' <UsedBY>frmBirthListSales.vb,frmBirthListUpdation.vb</UsedBY>
    ''' <returns></returns>
    ''' <remarks>Read,Write</remarks>
    Public Property PaidAmount() As Decimal
        Get
            Return _PaidAmount
        End Get
        Set(ByVal value As Decimal)
            _PaidAmount = value
        End Set
    End Property


    Public Property OpenAmount() As Decimal
        Get
            Return _OpenAmount
        End Get
        Set(ByVal value As Decimal)
            _OpenAmount = value
        End Set
    End Property
    ''' <summary>
    '''  Payment History data
    ''' </summary>
    ''' <value>DataTable</value>
    ''' <returns>DataTable</returns>
    ''' <UsedBY>frmBirthListSales.vb</UsedBY>
    ''' <remarks>Read,Write</remarks>
    Public Property DataTablePaymentHistory() As DataTable
        Get
            Return _dtPaymentHistory
        End Get
        Set(ByVal value As DataTable)
            _dtPaymentHistory = value
        End Set
    End Property
    Private _dtCheckDtls As DataTable
    ''' <summary>
    '''  Credit Cheque Data
    ''' </summary>
    ''' <value>DataTable</value>
    ''' <returns>DataTable</returns>
    ''' <UsedBY>frmBirthListSales.vb</UsedBY>
    ''' <remarks>Read,Write</remarks>
    Public Property DataTableCheckDtls() As DataTable
        Get
            Return _dtCheckDtls
        End Get
        Set(ByVal value As DataTable)
            _dtCheckDtls = value
        End Set
    End Property

    ''' <summary>
    '''  Store details of gift voucher 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DataTableGV() As DataTable
        Get
            Return _dtGV
        End Get
        Set(ByVal value As DataTable)
            _dtGV = value
        End Set
    End Property

    Public Property DataTableVoucherSales() As DataTable
        Get
            Return _dtVoucherSales
        End Get
        Set(ByVal value As DataTable)
            _dtVoucherSales = value
        End Set
    End Property

    ''' <summary>
    '''  Purchased items information 
    ''' </summary>
    ''' <value>DataTable</value>
    ''' <returns>DataTable</returns>
    ''' <UsedBY>frmBirthListSales.vb,frmBirthListUpdation.vb</UsedBY>
    ''' <remarks>Read,Write</remarks>
    Public Property DataTableBirthListItemDetail() As DataTable
        Get
            Return _dtBirthListItemDetail
        End Get
        Set(ByVal value As DataTable)
            _dtBirthListItemDetail = value
        End Set
    End Property
    ''' <summary>
    '''  BirthList items puchasing customer information
    ''' </summary>
    ''' <value>DataRow</value>
    ''' <UsedBY>frmBirthListSales.vb,frmBirthListUpdation.vb</UsedBY>
    ''' <returns>DataRow</returns>
    ''' <remarks>Read,Write</remarks>
    Public Property SelectedCustomerInfo() As DataRow
        Get
            Return _drSelectedCutomerinfo
        End Get
        Set(ByVal value As DataRow)
            _drSelectedCutomerinfo = value
        End Set
    End Property

    ''' <summary>
    ''' BirthList Information
    ''' </summary>
    ''' <value>DataRow</value>
    ''' <returns>DataRow</returns>
    '''  <UsedBY>frmBirthListSales.vb,frmBirthListUpdation.vb</UsedBY>
    ''' <remarks>Read,Write</remarks>
    Public Property SelectedBirthListInfo() As DataRow
        Get
            Return _SelectedBirthListInfo
        End Get
        Set(ByVal value As DataRow)
            _SelectedBirthListInfo = value
        End Set
    End Property

    Private _SaleInVoiceNumber As String = String.Empty

    ''' <summary>
    '''  Generate new SaleInVoiceNumber
    '''     </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
    '''  <usedby>frmBirthListSales.vb</usedby>
    ''' <remarks></remarks>
    Public Property GenNewSaleInVoiceNumber(ByVal TerminalId As String, ByVal Online As Boolean) As String
        Get
            If (_SaleInVoiceNumber = String.Empty) Then
                _SaleInVoiceNumber = RandomNumber_SalesInVoiceNumber(TerminalId, Online)
                Return RandomNumber_SalesInVoiceNumber(TerminalId, Online)
            Else
                Return _SaleInVoiceNumber
            End If
        End Get
        Set(ByVal value As String)
            _SaleInVoiceNumber = value
        End Set
    End Property

    ''' <summary>
    ''' Retrun Generated Sales Invoice Number 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private ReadOnly Property SaleInVoiceNumber() As String
        Get
            Return _SaleInVoiceNumber
        End Get

    End Property

    Private _strOrderDocumentNumber As String = String.Empty
    ''' <summary>
    '''  Generate new OrderDocumentNumber
    '''     </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
    ''' <UsedBY>frmBirthListSales.vb,frmBirthListUpdation.vb</UsedBY>
    ''' <remarks></remarks>
    Public Property OrderDocumentNumber() As String
        Get
            If (_strOrderDocumentNumber = String.Empty) Then
                _strOrderDocumentNumber = RandomNumber_OrderNumber()

                Return _strOrderDocumentNumber
            Else
                Return _strOrderDocumentNumber
            End If
        End Get
        Set(ByVal value As String)

        End Set
    End Property
    ''' <summary>
    ''' Print setting properties . Do you want to add Header at printing
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Property IsPrintHeader() As Boolean
        Get
            Return _isPrintHeader
        End Get
        Set(ByVal value As Boolean)
            _isPrintHeader = value
        End Set
    End Property
    ''' <summary>
    ''' Print setting properties . Do you want to add footer at printing
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Property IsPrintFooter() As Boolean
        Get
            Return _isPrintFooter
        End Get
        Set(ByVal value As Boolean)
            _isPrintFooter = value
        End Set
    End Property

    ''' <summary>
    ''' Used at the time of stock updation
    ''' </summary>
    ''' <remarks></remarks>
    Private _StockStorageLocation As String
    Public Property StockStorageLocation() As String
        Get
            Return _StockStorageLocation
        End Get
        Set(ByVal value As String)
            _StockStorageLocation = value
        End Set
    End Property

    Private _FinacialYear As String
    Public Property FinacialYear() As String
        Get
            Return _FinacialYear
        End Get
        Set(ByVal value As String)
            _FinacialYear = value
        End Set
    End Property

    Private _DateDayOpen As Date
    Public Property DateDayOpen() As Date
        Get
            Return _DateDayOpen
        End Get
        Set(ByVal value As Date)
            _DateDayOpen = value
        End Set
    End Property

    Public _CreationDate As Date
    Public Property CreationDate() As Date
        Get
            Return _CreationDate
        End Get
        Set(ByVal value As Date)
            _CreationDate = value
        End Set
    End Property

    ''' <summary>
    ''' Get  Table structure 
    ''' </summary>
    ''' <returns>Boolean</returns>
    '''  <usedby>frmBirthListSales.vb</usedby>
    ''' <remarks></remarks>
    Private Function GetDataTableStructure() As Boolean
        Try
            Dim strQuery As New System.Text.StringBuilder
            dsBirthListSales.Clear()
            strQuery.Length = 0
            strQuery.Append("select SiteCode,DocumentNumber,FinYear,SaleInvNumber,SaleInvLineNumber,DocumentType,TerminalID,TenderTypeCode,AmountTendered,")

            strQuery.Append("ExchangeRate,CurrencyCode,SOInvDate,SOInvTime,UserName,ManagersKeytoUpdate,")

            strQuery.Append("ChangeLine,(CASE WHEN REFNO_1 LIKE '%,%' THEN Replace(REFNO_1,',','.') ELSE REFNO_1 END) as REFNO_1,RefNo_2,RefNo_3,RefNo_4,RefDate,CREATEDAT,CREATEDBY,")

            strQuery.Append("CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS,TenderheadCode from salesinvoice where 1=0")


            strQuery.Append(" select SiteCode,BirthListId,CustomerId,TaxAmount,SaleInvNumber,PaidAmt,DelItemAmt,FinYear,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDBY,UPDATEDAT,UPDATEDON,STATUS,CustomerType,CLPPoints,CLPDiscount,TotalDiscountAmt,SalesExecutiveCode from BirthListSalesHdr where 1=0")
            strQuery.Append(" select SiteCode,BirthListId,CustomerId,SaleInvNumber,ArticleCode,EAN,BookedQty,DeliveredQty,")

            strQuery.Append("Rate,DiscAmt,TaxAmount,NetAmt,DeliveryDate,ExclusiveTax,CostAmt,IsCLP,FreeTexts,CREATEDAT,FinYear,CREATEDBY,CREATEDON,UPDATEDBY,UPDATEDAT,UPDATEDON,STATUS,CustomerType,CLPPoints,CLPDiscount,TotalDiscountAmt,OpenAmountQty, SrNo from BirthListSalesDtl where 1=0")

            strQuery.Append(" select * from OrderHdr where 1=0 ")
            strQuery.Append(" select * from OrderDtl where 1=0")
            strQuery.Append(" select * from BirthListRequestedItems where 1=0")
            strQuery.Append(" select * from BirthListReturnDtl where 1=0")
            strQuery.Append(" select * from CLPTransaction where 1=0")
            strQuery.Append(" select * from CLPTransactionsDetails where 1=0")

            Dim strQueryCLP As String = String.Format("select *  from CLPCustomers where CardNo= '{0}' and ClpProgramId= '{1}' ", CustomerID, CLPProgrmaID)
            strQuery.Append(strQueryCLP)
            strQuery.Append(" select * from SalesOrderTaxDtls where 1=0")
            strQuery.Append(" select * from VoucherDtls where 1=0 ")
            OpenConnection()
            Dim sqlCmd As New SqlCommand
            sqlCmd.CommandText = strQuery.ToString()
            sqlCmd.Connection = SpectrumCon()
            Dim sqlAdptor As New SqlDataAdapter
            sqlAdptor.SelectCommand = sqlCmd
            sqlAdptor.Fill(dsBirthListSales)
            dsBirthListSales.Tables(0).TableName = "SalesInvoice"
            dsBirthListSales.Tables(1).TableName = "BirthListSalesHdr"
            dsBirthListSales.Tables(2).TableName = "BirthListSalesDtl"
            dsBirthListSales.Tables(3).TableName = "OrderHdr"
            dsBirthListSales.Tables(4).TableName = "OrderDtl"
            dsBirthListSales.Tables(5).TableName = "BirthListRequestedItems"
            dsBirthListSales.Tables(6).TableName = "BirthListReturnDtl"
            dsBirthListSales.Tables(7).TableName = "CLPTransactionOLD"
            dsBirthListSales.Tables(8).TableName = "CLPTransactionsDetailsOLD"
            dsBirthListSales.Tables(9).TableName = "CLPCustomersOLD"
            dsBirthListSales.Tables(10).TableName = "SalesOrderTaxDtls"
            dsBirthListSales.Tables(11).TableName = "VoucherDtls"
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function




    ''' <summary>
    ''' Saving data into BirthListSalesHdr
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <usedby>frmBirthListSales.vb</usedby>
    ''' <remarks></remarks>
    Private Function Save_BirthListSalesHdr(ByVal online As Boolean) As Boolean
        Try
            If Not (dsBirthListSales.Tables("BirthListSalesHdr") Is Nothing) Then
                dsBirthListSales.Tables("BirthListSalesHdr").Clear()
                Dim drBirthListSalesHdr As DataRow

                Dim decCLPPoints As Object = _dtBirthListItemDetail.Compute("sum(CLPPoints)", " ")
                Dim decCLPDiscount As Object = _dtBirthListItemDetail.Compute("sum(CLPDiscount)", " ")
                Dim decTotalDiscountAmt As Object = decCLPDiscount
                If Not (_dtPaymentHistory Is Nothing) Or Not _dtVoucherSales Is Nothing Then
                    Dim objClsBirthListGlobal As New clsBirthListGobal
                    If (PaidAmount > Decimal.Zero) Then
                        drBirthListSalesHdr = dsBirthListSales.Tables("BirthListSalesHdr").NewRow()
                        drBirthListSalesHdr("SiteCode") = SiteCode
                        drBirthListSalesHdr("BirthListId") = BirthLisID
                        Try
                            If Not FinacialYear Is DBNull.Value Or Not FinacialYear = Nothing Then
                                drBirthListSalesHdr("FinYear") = FinacialYear
                            Else
                                Dim strFinYear As String = "select FinYear from Birthlist where BirthlistId = '" & BirthLisID & "' AND SiteCode = '" & SiteCode & "'"
                                Dim dcFinYear As New SqlCommand(strFinYear, SpectrumCon)
                                FinacialYear = dcFinYear.ExecuteScalar
                                If Not FinacialYear Is DBNull.Value Or Not FinacialYear = Nothing Then
                                    drBirthListSalesHdr("FinYear") = FinacialYear
                                Else
                                    drBirthListSalesHdr("FinYear") = objclsComman.GetFinancialYear(DateDayOpen, SiteCode)
                                End If
                            End If
                        Catch ex As Exception
                            LogException(ex)
                        End Try
                        drBirthListSalesHdr("CustomerId") = CustomerID
                        drBirthListSalesHdr("CustomerType") = CustomerType
                        drBirthListSalesHdr("SaleInvNumber") = GenNewSaleInVoiceNumber(TerminalID, online)

                        If Not decCLPPoints Is DBNull.Value Then
                            drBirthListSalesHdr("CLPPoints") = CDbl(decCLPPoints)
                        Else
                            drBirthListSalesHdr("CLPPoints") = Decimal.Zero
                        End If
                        If Not decCLPDiscount Is DBNull.Value Then
                            drBirthListSalesHdr("CLPDiscount") = CDbl(decCLPDiscount)
                        Else
                            drBirthListSalesHdr("CLPDiscount") = Decimal.Zero
                        End If
                        If Not decTotalDiscountAmt Is DBNull.Value Then
                            drBirthListSalesHdr("TotalDiscountAmt") = CDbl(decTotalDiscountAmt)
                        Else
                            drBirthListSalesHdr("TotalDiscountAmt") = Decimal.Zero
                        End If

                        ' Birthlist Sales Executive code 
                        ' Changes by Rahul Katkar 
                        ' Only added Validate conditions
                        If Not DataTableBirthListItemDetail Is Nothing Then
                            If DataTableBirthListItemDetail.Rows.Count > 0 Then
                                drBirthListSalesHdr("SalesExecutiveCode") = DataTableBirthListItemDetail.Rows(0)("SalesExecutiveCode")
                            Else
                                drBirthListSalesHdr("SalesExecutiveCode") = DBNull.Value
                            End If
                        Else
                            drBirthListSalesHdr("SalesExecutiveCode") = DBNull.Value
                        End If

                        'End Changes 


                        drBirthListSalesHdr("PaidAmt") = PaidAmount
                        drBirthListSalesHdr("TaxAmount") = _dtBirthListItemDetail.Compute("sum(TaxAmt)", " ")
                        drBirthListSalesHdr("DelItemAmt") = PaidAmount
                        drBirthListSalesHdr("CREATEDAT") = SiteCode
                        drBirthListSalesHdr("CREATEDBY") = UserName
                        drBirthListSalesHdr("CREATEDON") = CreationDate
                        drBirthListSalesHdr("UPDATEDAT") = SiteCode
                        drBirthListSalesHdr("UPDATEDBY") = UserName
                        drBirthListSalesHdr("UPDATEDON") = CreationDate
                        drBirthListSalesHdr("STATUS") = True
                        dsBirthListSales.Tables("BirthListSalesHdr").Rows.Add(drBirthListSalesHdr)
                    End If



                Else
                    'Changed by Rohit on 24 March 2011

                    Return True



                End If
                Return True
            Else
                dsBirthListSales.Tables("OrderDtl").Columns("RefDocumentNo").DefaultValue = DBNull.Value
                Return True
            End If

        Catch ex As Exception
            LogException(ex)
            Return False
        End Try


    End Function

    Private Function Save_BirthListReturnDtl(ByVal Online As Boolean) As Boolean
        Dim drBirthListReturnDtl As DataRow
        Dim i As Integer = 1
        Try

            For Each drBirthListItemDetail As DataRow In _dtBirthListItemDetail.Rows
                If Not (drBirthListItemDetail.IsNull("CurrentReturnQty")) Then
                    If (drBirthListItemDetail("CurrentReturnQty") > 0) Then
                        drBirthListReturnDtl = dsBirthListSales.Tables("BirthListReturnDtl").NewRow()
                        drBirthListReturnDtl("SiteCode") = drBirthListItemDetail("SiteCode")
                        drBirthListReturnDtl("BirthListId") = drBirthListItemDetail("BirthListId")
                        'drBirthListReturnDtl("CustomerId") = CustomerID
                        drBirthListReturnDtl("ArticleCode") = drBirthListItemDetail("ArticleCode")
                        drBirthListReturnDtl("EAN") = drBirthListItemDetail("EAN")
                        drBirthListReturnDtl("ReturnInvoiceNumber") = RandomNumber_SalesInVoiceNumber(TerminalID, Online)
                        drBirthListReturnDtl("ReturnDate") = CreationDate
                        drBirthListReturnDtl("ReturnQty") = drBirthListItemDetail("CurrentReturnQty")
                        drBirthListReturnDtl("ReturnReason") = drBirthListItemDetail("CurrentReturnReason")
                        drBirthListReturnDtl("CREATEDAT") = SiteCode
                        drBirthListReturnDtl("CREATEDBY") = UserName
                        drBirthListReturnDtl("CREATEDON") = CreationDate
                        drBirthListReturnDtl("UPDATEDAT") = SiteCode
                        drBirthListReturnDtl("UPDATEDBY") = UserName
                        drBirthListReturnDtl("UPDATEDON") = CreationDate
                        drBirthListReturnDtl("STATUS") = True
                        'Change for CR 5679
                        'Adding the below lines for handling multiple return of same ean with diff price
                        If IsDBNull(drBirthListItemDetail("SrNo")) Then
                            drBirthListReturnDtl("SrNo") = i
                            i = i + 1
                        Else
                            drBirthListReturnDtl("SrNo") = drBirthListItemDetail("SrNo")
                        End If
                        'End of change
                        dsBirthListSales.Tables("BirthListReturnDtl").Rows.Add(drBirthListReturnDtl)
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Function Save_SalesOrderTaxDtls(Optional ByRef saleInvoiceno As String = "0") As Boolean
        Dim drSalesOrderTaxDtls As DataRow
        Dim objClsBirthListGlobal As New clsBirthListGobal
        Dim iTaxLineNo As Integer = 1
        Try

            CreatingLineNO(dtMainTax)
            If Not dtMainTax Is Nothing Then

                For Each drMainTax As DataRow In dtMainTax.Rows
                    If Not (drMainTax.IsNull("TAXAMOUNT")) Then
                        If (drMainTax("TAXAMOUNT") > Decimal.Zero) Then
                            drSalesOrderTaxDtls = dsBirthListSales.Tables("SalesOrderTaxDtls").NewRow()
                            drSalesOrderTaxDtls("SiteCode") = SiteCode
                            drSalesOrderTaxDtls("DocumentType") = "BLS"
                            'drSalesOrderTaxDtls("SaleOrderNumber") = BirthLisID
                            drSalesOrderTaxDtls("SaleOrderNumber") = saleInvoiceno
                            Try
                                If Not FinacialYear Is DBNull.Value Or Not FinacialYear = Nothing Then
                                    drSalesOrderTaxDtls("FinYear") = FinacialYear
                                Else
                                    Dim strFinYear As String = "select FinYear from Birthlist where BirthlistId = '" & BirthLisID & "' AND SiteCode = '" & SiteCode & "'"
                                    Dim dcFinYear As New SqlCommand(strFinYear, SpectrumCon)
                                    FinacialYear = dcFinYear.ExecuteScalar
                                    If Not FinacialYear Is DBNull.Value Or Not FinacialYear = Nothing Then
                                        drSalesOrderTaxDtls("FinYear") = FinacialYear
                                    Else
                                        drSalesOrderTaxDtls("FinYear") = objclsComman.GetFinancialYear(DateDayOpen, SiteCode)
                                    End If
                                End If
                            Catch ex As Exception
                                LogException(ex)
                            End Try
                            drSalesOrderTaxDtls("EAN") = drMainTax("EAN")
                            drSalesOrderTaxDtls("TaxLineNo") = drMainTax("BillLineNo")
                            drSalesOrderTaxDtls("TaxLabel") = drMainTax("TaxCode")
                            drSalesOrderTaxDtls("TaxValue") = drMainTax("TAXAMOUNT")
                            drSalesOrderTaxDtls("CREATEDAT") = SiteCode
                            drSalesOrderTaxDtls("CREATEDBY") = UserName
                            drSalesOrderTaxDtls("CREATEDON") = CreationDate
                            drSalesOrderTaxDtls("UPDATEDAT") = SiteCode
                            drSalesOrderTaxDtls("UPDATEDBY") = UserName
                            drSalesOrderTaxDtls("UPDATEDON") = CreationDate
                            drSalesOrderTaxDtls("STATUS") = True
                            dsBirthListSales.Tables("SalesOrderTaxDtls").Rows.Add(drSalesOrderTaxDtls)
                        End If
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Sub CreatingLineNO(ByRef dt As DataTable)
        Try
            Dim i As Integer = 1

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    dr("BillLineNo") = i
                    i = i + 1
                Next
            End If


        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub



    ''' <summary>
    ''' Saving data into datatable table  "BirthListSalesDtl"
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <usedby>frmBirthListSales.vb</usedby>
    ''' <remarks></remarks>
    Private Function Save_BirthListSalesDtl() As Boolean

        Try
            Dim IsSuccess As Boolean = True

            If Not _dtBirthListItemDetail.Columns("CLPRequire") Is Nothing Then
                _dtBirthListItemDetail.Columns("CLPRequire").ColumnName = "IsCLP"
            End If

            If Not (dsBirthListSales.Tables("BirthListSalesHdr") Is Nothing) Then
                If Not _dtPaymentHistory Is Nothing Then
                    dsBirthListSales.Tables("BirthListSalesDtl").Clear()
                    For Each drBirthListItemDetail As DataRow In _dtBirthListItemDetail.Rows
                        If (IsUpdateBirthList) Then
                            If (IIf(drBirthListItemDetail("CurrentPurchasedAmount") Is DBNull.Value, 0, drBirthListItemDetail("CurrentPurchasedAmount")) > Decimal.Zero) Then
                                IsSuccess = AddRow_BirthListSalesDTl(drBirthListItemDetail, True)
                            End If
                        Else
                            If Not (drBirthListItemDetail.IsNull("NetAmount")) Then
                                If (IIf(drBirthListItemDetail("NetAmount") Is DBNull.Value, 0, drBirthListItemDetail("NetAmount")) > Decimal.Zero) Then
                                    IsSuccess = AddRow_BirthListSalesDTl(drBirthListItemDetail, False)
                                End If
                            End If
                        End If
                    Next

                    ' to update the cost price 
                    SetCostPrice(IsMAP, dsBirthListSales.Tables("BirthListSalesDtl"), SiteCode, "CostAmt")
                    ' to update the cost price 
                    Return IsSuccess
                End If
            End If
            Return IsSuccess
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try

    End Function

    Private Function AddRow_BirthListSalesDTl(ByVal drBirthListItemDetail As DataRow, ByVal IsUpdate As Boolean) As Boolean
        Try
            Dim objClsBirthListGlobal As New clsBirthListGobal
            Dim drBirthListSalesDtl As DataRow
            drBirthListSalesDtl = dsBirthListSales.Tables("BirthListSalesDtl").NewRow()
            drBirthListSalesDtl("SiteCode") = drBirthListItemDetail("SiteCode")
            drBirthListSalesDtl("BirthListId") = drBirthListItemDetail("BirthListId")
            Try
                If Not FinacialYear Is DBNull.Value Or Not FinacialYear = Nothing Then
                    drBirthListSalesDtl("FinYear") = FinacialYear
                Else
                    Dim strFinYear As String = "select FinYear from Birthlist where BirthlistId = '" & BirthLisID & "' AND SiteCode = '" & SiteCode & "'"
                    Dim dcFinYear As New SqlCommand(strFinYear, SpectrumCon)
                    FinacialYear = dcFinYear.ExecuteScalar
                    If Not FinacialYear Is DBNull.Value Or Not FinacialYear = Nothing Then
                        drBirthListSalesDtl("FinYear") = FinacialYear
                    Else
                        drBirthListSalesDtl("FinYear") = objclsComman.GetFinancialYear(DateDayOpen, SiteCode)
                    End If
                End If
            Catch ex As Exception
                LogException(ex)
            End Try
            drBirthListSalesDtl("CustomerId") = CustomerID
            drBirthListSalesDtl("CustomerType") = CustomerType
            drBirthListSalesDtl("ArticleCode") = drBirthListItemDetail("ArticleCode")
            drBirthListSalesDtl("EAN") = drBirthListItemDetail("EAN")
            If (drBirthListItemDetail.IsNull("BookedQty")) Then
                drBirthListSalesDtl("BookedQty") = 0
                Dim objPurchasedQty As Object = drBirthListItemDetail("PurchasedQty")
                drBirthListSalesDtl("BookedQty") = CInt(objPurchasedQty)
            Else
                Dim objPurchasedQty As Object = drBirthListItemDetail("PurchasedQty")
                Dim objBookedQty As Object = drBirthListItemDetail("BookedQty")
                If objBookedQty Is DBNull.Value Then
                    objBookedQty = 0
                End If
                drBirthListSalesDtl("BookedQty") = CInt(objPurchasedQty)
            End If

            drBirthListSalesDtl("CLPPoints") = drBirthListItemDetail("CLPPoints")

            drBirthListSalesDtl("CLPDiscount") = drBirthListItemDetail("CLPDiscount")

            drBirthListSalesDtl("TotalDiscountAmt") = drBirthListItemDetail("CLPDiscount")

            If (IsUpdate) Then
                drBirthListSalesDtl("OpenAmountQty") = drBirthListItemDetail("OpenAmountQty")
            Else
                drBirthListSalesDtl("OpenAmountQty") = 0
            End If


            drBirthListSalesDtl("DeliveredQty") = drBirthListItemDetail("Pickupqty")
            drBirthListSalesDtl("TaxAmount") = drBirthListItemDetail("TaxAmt")
            drBirthListSalesDtl("Rate") = drBirthListItemDetail("SellingPrice")
            drBirthListSalesDtl("DiscAmt") = Decimal.Zero
            'drBirthListSalesDtl("CostAmt") = drBirthListItemDetail("SellingPrice")
            drBirthListSalesDtl("ExclusiveTax") = drBirthListItemDetail("ExclusiveTax")
            drBirthListSalesDtl("IsCLP") = drBirthListItemDetail("IsCLP")
            drBirthListSalesDtl("FreeTexts") = drBirthListItemDetail("FreeTexts")
            If (IsUpdate) Then
                drBirthListSalesDtl("NetAmt") = drBirthListItemDetail("NetAmount")
            Else
                drBirthListSalesDtl("NetAmt") = drBirthListItemDetail("NetAmount")
            End If

            drBirthListSalesDtl("DeliveryDate") = CreationDate
            'drBirthListSalesDtl("SaleInvNumber") = SaleInVoiceNumber
            drBirthListSalesDtl("CREATEDAT") = drBirthListItemDetail("SiteCode")
            drBirthListSalesDtl("CREATEDBY") = UserName
            drBirthListSalesDtl("CREATEDON") = CreationDate
            drBirthListSalesDtl("UPDATEDAT") = drBirthListItemDetail("SiteCode")
            drBirthListSalesDtl("UPDATEDBY") = UserName
            drBirthListSalesDtl("UPDATEDON") = CreationDate
            drBirthListSalesDtl("STATUS") = True
            'Added by Rohit for Issue No. 00006136
            'drBirthListSalesDtl("SrNo") = drBirthListItemDetail("SrNo")
            Try
                drBirthListSalesDtl("SrNo") = dsBirthListSales.Tables("BirthListSalesDtl").Select("articlecode = '" & drBirthListSalesDtl("ArticleCode") & "' and EAN = '" & drBirthListSalesDtl("EAN") & "'", "SrNo Desc", DataViewRowState.CurrentRows)(0)("SrNo") + 1
            Catch ex As Exception
                drBirthListSalesDtl("SrNo") = 1
            End Try

            'GetBLSalesSrNo(BirthLisID, drBirthListSalesDtl("ArticleCode"), drBirthListSalesDtl("EAN")) + 1

            dsBirthListSales.Tables("BirthListSalesDtl").Rows.Add(drBirthListSalesDtl)
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function


    Private Function Save_VoucherSales() As Boolean
        Try
            Dim drBirthListSalesDtl_VoucherSales As DataRow
            If Not (DataTableVoucherSales Is Nothing) Then
                Dim objClsBirthListGlobal As New clsBirthListGobal
                For Each drVoucherSalesDetail As DataRow In _dtVoucherSales.Rows
                    drBirthListSalesDtl_VoucherSales = dsBirthListSales.Tables("BirthListSalesDtl").NewRow()
                    drBirthListSalesDtl_VoucherSales("SiteCode") = drVoucherSalesDetail("SiteCode")
                    Try
                        If Not FinacialYear Is DBNull.Value Or Not FinacialYear = Nothing Then
                            drBirthListSalesDtl_VoucherSales("FinYear") = FinacialYear
                        Else
                            Dim strFinYear As String = "select FinYear from Birthlist where BirthlistId = '" & BirthLisID & "' AND SiteCode = '" & SiteCode & "'"
                            Dim dcFinYear As New SqlCommand(strFinYear, SpectrumCon)
                            FinacialYear = dcFinYear.ExecuteScalar
                            If Not FinacialYear Is DBNull.Value Or Not FinacialYear = Nothing Then
                                drBirthListSalesDtl_VoucherSales("FinYear") = FinacialYear
                            Else
                                drBirthListSalesDtl_VoucherSales("FinYear") = objclsComman.GetFinancialYear(DateDayOpen, SiteCode)
                            End If
                        End If
                    Catch ex As Exception
                        LogException(ex)
                    End Try
                    drBirthListSalesDtl_VoucherSales("BirthListId") = BirthLisID
                    drBirthListSalesDtl_VoucherSales("CustomerId") = CustomerID
                    drBirthListSalesDtl_VoucherSales("CustomerType") = CustomerType
                    drBirthListSalesDtl_VoucherSales("ArticleCode") = drVoucherSalesDetail("ArticleCode")
                    drBirthListSalesDtl_VoucherSales("EAN") = drVoucherSalesDetail("EAN")
                    drBirthListSalesDtl_VoucherSales("BookedQty") = drVoucherSalesDetail("RequstedQty")
                    drBirthListSalesDtl_VoucherSales("DeliveredQty") = 0
                    drBirthListSalesDtl_VoucherSales("Rate") = drVoucherSalesDetail("NetAmount")
                    drBirthListSalesDtl_VoucherSales("DiscAmt") = Decimal.Zero
                    drBirthListSalesDtl_VoucherSales("NetAmt") = drVoucherSalesDetail("NetAmount")
                    drBirthListSalesDtl_VoucherSales("DeliveryDate") = CreationDate
                    drBirthListSalesDtl_VoucherSales("CostAmt") = drVoucherSalesDetail("NetAmount")
                    drBirthListSalesDtl_VoucherSales("TaxAmount") = Decimal.Zero
                    drBirthListSalesDtl_VoucherSales("ExclusiveTax") = Decimal.Zero
                    drBirthListSalesDtl_VoucherSales("IsCLP") = True
                    drBirthListSalesDtl_VoucherSales("CREATEDAT") = drVoucherSalesDetail("SiteCode")
                    drBirthListSalesDtl_VoucherSales("CREATEDBY") = UserName
                    drBirthListSalesDtl_VoucherSales("CREATEDON") = CreationDate
                    drBirthListSalesDtl_VoucherSales("UPDATEDAT") = drVoucherSalesDetail("SiteCode")
                    drBirthListSalesDtl_VoucherSales("UPDATEDBY") = UserName
                    drBirthListSalesDtl_VoucherSales("UPDATEDON") = CreationDate
                    drBirthListSalesDtl_VoucherSales("STATUS") = True

                    dsBirthListSales.Tables("BirthListSalesDtl").Rows.Add(drBirthListSalesDtl_VoucherSales)
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    ''' <summary>
    ''' Saving data into datatable table  "SalesInvoice"
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <usedby>frmBirthListSales.vb</usedby>
    ''' <remarks></remarks>
    Private Function Save_SalesInvoice() As Boolean
        Dim drInvc As DataRow
        Try

            If Not _dtPaymentHistory Is Nothing Then

                Dim objClsBirthListGlobal As New clsBirthListGobal
                For Each drPayment As DataRow In _dtPaymentHistory.Rows

                    If Not drPayment.RowState = DataRowState.Deleted Then
                        drInvc = dsBirthListSales.Tables("SalesInvoice").NewRow()
                        drInvc("SiteCode") = SiteCode
                        drInvc("DocumentNumber") = BirthLisID
                        Try
                            If Not FinacialYear Is DBNull.Value Or Not FinacialYear = Nothing Then
                                drInvc("FinYear") = FinacialYear
                            Else
                                Dim strFinYear As String = "select FinYear from Birthlist where BirthlistId = '" & BirthLisID & "' AND SiteCode = '" & SiteCode & "'"
                                Dim dcFinYear As New SqlCommand(strFinYear, SpectrumCon)
                                FinacialYear = dcFinYear.ExecuteScalar
                                If Not FinacialYear Is DBNull.Value Or Not FinacialYear = Nothing Then
                                    drInvc("FinYear") = FinacialYear
                                Else
                                    drInvc("FinYear") = objclsComman.GetFinancialYear(DateDayOpen, SiteCode)
                                End If
                            End If
                        Catch ex As Exception
                            LogException(ex)
                        End Try
                        'drInvc("SaleInvNumber") = SaleInVoiceNumber

                        drInvc("SaleInvLineNumber") = drPayment("SrNo")
                        drInvc("TerminalID") = TerminalID
                        drInvc("TenderTypeCode") = drPayment("RecieptTypeCode")
                        drInvc("TenderheadCode") = drPayment("RecieptType")
                        drInvc("AmountTendered") = drPayment("Amount")
                        drInvc("ExchangeRate") = drPayment("ExchangeRate")
                        drInvc("CurrencyCode") = drPayment("CurrencyCode")
                        drInvc("SOInvDate") = DateDayOpen.Date
                        drInvc("SOInvTime") = DateDayOpen
                        drInvc("DocumentType") = "BLS" ' by ram on 28.09.2009 BirthListOutBoundCode
                        drInvc("UserName") = UserName
                        drInvc("ManagersKeytoUpdate") = 3
                        drInvc("ChangeLine") = 3
                        drInvc("RefNo_1") = clsCommon.ConvertToEnglish(drPayment("AmountInCurrency"))
                        drInvc("RefNo_2") = drPayment("Number")
                        drInvc("RefNo_3") = DBNull.Value
                        drInvc("RefNo_4") = DBNull.Value
                        drInvc("RefDate") = CreationDate
                        drInvc("CREATEDAT") = SiteCode
                        drInvc("CREATEDBY") = UserName
                        drInvc("CREATEDON") = CreationDate
                        drInvc("UPDATEDAT") = SiteCode
                        drInvc("UPDATEDBY") = UserName
                        drInvc("UPDATEDON") = CreationDate
                        drInvc("STATUS") = True
                        dsBirthListSales.Tables("SalesInvoice").Rows.Add(drInvc)
                    End If
                Next
            End If

            'Added by Rohit for CR - 5938

            'If dsMain.Tables.Contains("CheckDtls") Then
            '    dsMain.Tables.Remove("CheckDtls")
            'End If
            If Not DataTableCheckDtls Is Nothing Then
                Dim tempDtCheckDtls As New DataTable
                tempDtCheckDtls = DataTableCheckDtls.Copy
                tempDtCheckDtls.TableName = "CheckDtls"
                tempDtCheckDtls.AcceptChanges()
                If Not dsBirthListSales.Tables.Contains("CheckDtls") Then
                    dsBirthListSales.Tables.Add(tempDtCheckDtls)
                Else
                    dsBirthListSales.Tables("CheckDtls").Merge(tempDtCheckDtls)
                End If
                objclsComman.PrepareCreditCheckData(dsBirthListSales, SiteCode, UserName, FinacialYear, "BLS", SaleInVoiceNumber, BirthLisID, CreationDate, dDueDate, strRemarks, "SO", DateDayOpen)
                objclsComman.AddMode(dsBirthListSales.Tables("CheckDtls"))
            End If





            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try

    End Function

    ''' <summary>
    ''' Saving data into datatable table  "OrderHdr"
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <UsedBY>frmBirthListSales.vb,frmBirthListUpdation.vb</UsedBY>
    ''' <remarks></remarks>
    Private Function Save_OrderHDR() As Boolean
        Try
            If Not (dsBirthListSales.Tables("OrderHdr") Is Nothing) Then
                Dim drOrderHdr As DataRow
                Dim objClsBirthListGlobal As New clsBirthListGobal

                For Each drBirthListItemDetail As DataRow In _dtBirthListItemDetail.Rows
                    If (drBirthListItemDetail("PickUpQty") > 0) Then
                        drOrderHdr = dsBirthListSales.Tables("OrderHdr").NewRow()
                        drOrderHdr("SiteCode") = SiteCode
                        'drOrderHdr("DocumentNumber") = OrderDocumentNumber()
                        Try
                            If Not FinacialYear Is DBNull.Value Or Not FinacialYear = Nothing Then
                                drOrderHdr("FinYear") = FinacialYear
                            Else
                                Dim strFinYear As String = "select FinYear from Birthlist where BirthlistId = '" & BirthLisID & "' AND SiteCode = '" & SiteCode & "'"
                                Dim dcFinYear As New SqlCommand(strFinYear, SpectrumCon)
                                FinacialYear = dcFinYear.ExecuteScalar
                                If Not FinacialYear Is DBNull.Value Or Not FinacialYear = Nothing Then
                                    drOrderHdr("FinYear") = FinacialYear
                                Else
                                    drOrderHdr("FinYear") = objclsComman.GetFinancialYear(DateDayOpen, SiteCode)
                                End If
                            End If
                        Catch ex As Exception
                            LogException(ex)
                        End Try

                        drOrderHdr("DocumentType") = BirthListOutBoundCode
                        drOrderHdr("SupplierCode") = supplier
                        drOrderHdr("DocNetValue") = PaidAmount
                        drOrderHdr("DocDate") = CreationDate
                        drOrderHdr("PurchaseGroupCode") = DBNull.Value
                        drOrderHdr("DeliverySiteCode") = SiteCode
                        Try
                            If Not (IsUpdateBirthList) Then
                                drOrderHdr("ExpectedDeliveryDt") = dsBirthListSales.Tables("BirthListSalesDtl").Rows(0)("DeliveryDate")
                            End If
                            If Not SelectedBirthListInfo Is Nothing Then
                                drOrderHdr("ExpectedDeliveryDt") = SelectedBirthListInfo("DeliveryDate")
                            End If
                        Catch ex As Exception
                            LogException(ex)
                        End Try
                        drOrderHdr("PaymentAfterDelDays") = DBNull.Value
                        drOrderHdr("AdditionalTermsNConditions") = DBNull.Value
                        drOrderHdr("AdditionalPaymentTerms") = DBNull.Value
                        drOrderHdr("IsClosed") = True
                        drOrderHdr("IsFreezed") = True
                        drOrderHdr("ReturnReasonCode") = "RE104"
                        drOrderHdr("Remarks") = DBNull.Value
                        drOrderHdr("ApprovedDate") = CreationDate
                        drOrderHdr("ApprovedLevel") = DBNull.Value
                        drOrderHdr("AmmendmentNo") = DBNull.Value
                        drOrderHdr("ClosedDate") = CreationDate
                        drOrderHdr("Transporter") = DBNull.Value
                        drOrderHdr("DocApprovalID") = DBNull.Value
                        drOrderHdr("ParentOrderNo") = DBNull.Value
                        drOrderHdr("CREATEDAT") = SiteCode
                        drOrderHdr("CREATEDBY") = UserName
                        drOrderHdr("CREATEDON") = CreationDate
                        drOrderHdr("UPDATEDAT") = SiteCode
                        drOrderHdr("UPDATEDBY") = UserName
                        drOrderHdr("UPDATEDON") = CreationDate
                        drOrderHdr("STATUS") = True
                        drOrderHdr("SupplierCode") = " "

                        dsBirthListSales.Tables("OrderHdr").Rows.Add(drOrderHdr)
                        Return True
                        Exit For
                    End If
                Next
                Return True
            Else
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function


    ''' <summary>
    ''' Saving data into datatable table  "OrderDtl"
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <UsedBY>frmBirthListSales.vb,frmBirthListUpdation.vb</UsedBY>
    ''' <remarks></remarks>
    Private Function Save_OrderDTL() As Boolean
        Try


            If Not (dsBirthListSales.Tables("OrderDtl") Is Nothing) Then
                Dim drOrderHdr As DataRow
                Dim iNext As Integer = 1
                Dim objClsBirthListGlobal As New clsBirthListGobal
                Dim ObjClsComm As New clsCommon
                Dim dtOldSalesItemDtls As New DataTable
                dtOldSalesItemDtls = ObjClsComm.GetFilledTable("select saleInvNumber,ArticleCode,Ean,CreatedOn,BookedQty from  BirthListSalesDtl where birthlistId ='" & BirthLisID & "'")

                For Each drBirthListItemDetail As DataRow In _dtBirthListItemDetail.Rows
                    If (drBirthListItemDetail("PickUpQty") > 0) Then
                        drOrderHdr = dsBirthListSales.Tables("OrderDtl").NewRow()
                        drOrderHdr("SiteCode") = SiteCode
                        Try
                            If Not FinacialYear Is DBNull.Value Or Not FinacialYear = Nothing Then
                                drOrderHdr("FinYear") = FinacialYear
                            Else
                                Dim strFinYear As String = "select FinYear from Birthlist where BirthlistId = '" & BirthLisID & "' AND SiteCode = '" & SiteCode & "'"
                                Dim dcFinYear As New SqlCommand(strFinYear, SpectrumCon)
                                FinacialYear = dcFinYear.ExecuteScalar
                                If Not FinacialYear Is DBNull.Value Or Not FinacialYear = Nothing Then
                                    drOrderHdr("FinYear") = FinacialYear
                                Else
                                    drOrderHdr("FinYear") = objclsComman.GetFinancialYear(DateDayOpen, SiteCode)
                                End If
                            End If
                        Catch ex As Exception
                            LogException(ex)
                        End Try
                        drOrderHdr("ArticleCode") = drBirthListItemDetail("articlecode")
                        drOrderHdr("EAN") = drBirthListItemDetail("EAN")
                        drOrderHdr("LineNumber") = iNext
                        drOrderHdr("Qty") = drBirthListItemDetail("PickUpQty")
                        drOrderHdr("UnitofMeasure") = 1
                        drOrderHdr("OpenQty") = 0
                        drOrderHdr("DeliveredQty") = drBirthListItemDetail("PickUpQty")
                        drOrderHdr("DeliveryCompleted") = True
                        drOrderHdr("PurchaseGroupCode") = DBNull.Value
                        drOrderHdr("RefDocument") = "BirthList"
                        If drBirthListItemDetail("PickUpQty") > 0 And drBirthListItemDetail("PurChasedQty") = 0 Then
                            Dim drOldSales As DataRow() = dtOldSalesItemDtls.Select("ArticleCode = '" & drOrderHdr.Item("ArticleCode") & "' AND Ean = '" & drOrderHdr.Item("EAN") & "'")
                            If drOldSales.Count > 0 Then
                                Dim TotalDelTillYesterday As Integer = 0
                                For Each drDeliveredQty In drOldSales
                                    TotalDelTillYesterday = TotalDelTillYesterday + drDeliveredQty.Item("BookedQty")
                                    If drBirthListItemDetail.Item("DeliveredQty") <= TotalDelTillYesterday Then
                                        drOrderHdr.Item("RefDocumentNo") = drDeliveredQty("saleInvNumber")
                                        Exit For
                                    End If
                                Next
                            End If
                        End If
                        drOrderHdr("PONo") = DBNull.Value
                        drOrderHdr("BirthListId") = BirthLisID
                        drOrderHdr("SaleOrderNumber") = DBNull.Value
                        drOrderHdr("AllocationRule") = DBNull.Value
                        drOrderHdr("DayOpenDate") = DateDayOpen
                        'If Not (IsUpdateBirthList) Then
                        '    drOrderHdr("MRP") = drBirthListItemDetail("Price")
                        'End If
                        drOrderHdr("MRP") = drBirthListItemDetail("SellingPrice")
                        drOrderHdr("CostPrice") = drBirthListItemDetail("NetAmount")
                        drOrderHdr("NetCostPrice") = drBirthListItemDetail("NetAmount")
                        drOrderHdr("ExciseDutyAmt") = DBNull.Value
                        drOrderHdr("ExciseDutyRate") = DBNull.Value
                        drOrderHdr("PurchaseTaxAmt") = drBirthListItemDetail("TaxAmt")
                        drOrderHdr("PurchaseTaxRate") = DBNull.Value
                        drOrderHdr("AdditionalChargeAmt") = DBNull.Value
                        drOrderHdr("DiscountAmount") = drBirthListItemDetail("NetAmount")
                        drOrderHdr("AdditionalChargeRate") = DBNull.Value
                        drOrderHdr("DocValue") = DBNull.Value
                        drOrderHdr("InboundQty") = DBNull.Value
                        drOrderHdr("CREATEDAT") = SiteCode
                        drOrderHdr("CREATEDBY") = UserName
                        drOrderHdr("CREATEDON") = CreationDate
                        drOrderHdr("UPDATEDAT") = SiteCode
                        drOrderHdr("UPDATEDBY") = UserName
                        drOrderHdr("UPDATEDON") = CreationDate
                        drOrderHdr("STATUS") = True
                        dsBirthListSales.Tables("OrderDTL").Rows.Add(drOrderHdr)
                        iNext += 1
                    End If
                Next
                Return True
            Else
                Return True
            End If

        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function

    ''' <summary>
    '''   Update information into database table  "BirthListRequestedItems"  when ReturnQty Changed
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <usedby>*****</usedby>
    ''' <remarks></remarks>
    Public Function UpdateBirthListRequestedItem_ADP(Optional ByVal dtbirthlistrequestediems As DataTable = Nothing) As Boolean
        Try
            Dim iRowIndex As Integer = 0
            dsBirthListSales.Tables("BirthListRequestedItems").Clear()
            Dim dtNewBirthList As DataTable = _dtBirthListItemDetail


            Dim objClsBirthListGlobal As New clsBirthListGobal
            dsBirthListSales.Tables("BirthListRequestedItems").Columns("BirthListID").DefaultValue = BirthLisID
            'dsBirthListSales.Tables("BirthListRequestedItems").Columns("FinYear").DefaultValue = FinacialYear
            If Not PreviousFinancialYear Is DBNull.Value And Not PreviousFinancialYear Is Nothing Then
                dsBirthListSales.Tables("BirthListRequestedItems").Columns("FinYear").DefaultValue = PreviousFinancialYear
            Else
                Try
                    If Not FinacialYear Is DBNull.Value Or Not FinacialYear = Nothing Then
                        dsBirthListSales.Tables("BirthListRequestedItems").Columns("FinYear").DefaultValue = FinacialYear
                    Else
                        Dim strFinYear As String = "select FinYear from Birthlist where BirthlistId = '" & BirthLisID & "' AND SiteCode = '" & SiteCode & "'"
                        Dim dcFinYear As New SqlCommand(strFinYear, SpectrumCon)
                        FinacialYear = dcFinYear.ExecuteScalar
                        If Not FinacialYear Is DBNull.Value Or Not FinacialYear = Nothing Then
                            dsBirthListSales.Tables("BirthListRequestedItems").Columns("FinYear").DefaultValue = FinacialYear
                        Else
                            dsBirthListSales.Tables("BirthListRequestedItems").Columns("FinYear").DefaultValue = objclsComman.GetFinancialYear(DateDayOpen, SiteCode)
                        End If
                    End If
                Catch ex As Exception
                    LogException(ex)
                End Try
            End If


            For Each dr As DataRow In dtNewBirthList.Rows
                If Not (dr.RowState = DataRowState.Deleted) Then
                    If IsDBNull(dr("PickUpQty")) Then
                        dr("PickUpQty") = 0
                    End If
                    If IsDBNull(dr("PurchasedQty")) Then
                        dr("PurchasedQty") = 0
                    End If

                    If Not (dr("Bookedqty") Is DBNull.Value) Then
                        dr("BookedQty") = dr("BookedQty") + dr("PurchasedQty")
                    Else
                        dr("BookedQty") = 0
                        dr("BookedQty") = dr("BookedQty") + dr("PurchasedQty")
                    End If
                    'If Not dr("DeliveredQty") Is DBNull.Value Then
                    '    dr("DeliveredQty") = dr("DeliveredQty") + dr("PickUpQty")
                    'Else
                    '    dr("DeliveredQty") = 0
                    '    dr("DeliveredQty") = dr("DeliveredQty") + dr("PickUpQty")
                    'End If                    
                    If Not dr("ReservedQty") Is DBNull.Value Then
                        dr("ReservedQty") = dr("ReservedQty") - dr("PickUpQty")
                        If dr("ReservedQty") < 0 Then
                            dr("ReservedQty") = 0
                        End If
                    Else
                        dr("ReservedQty") = 0
                    End If

                    Dim dt As New DataTable
                    dt = GetBLReqRecord(BirthLisID, dr("ArticleCode"), dr("EAN"), dr("SellingPrice"), FinacialYear)
                    If dt.Rows.Count > 0 Then
                        Dim srno As String = dt.Rows(0)("SrNo")
                        If srno = "1" Then
                            'Changes by Ashish for CR 5679
                            If CBool(dr("IsPriceChangedHere")) Then
                                'If price has been changed then we must add new row in BLRequested table
                                'and update the quantities of the existing row
                                Dim dr1 As DataRow = dsBirthListSales.Tables("BirthListRequestedItems").NewRow()
                                dr1("SiteCode") = dr("SiteCode")
                                dr1("ArticleCode") = dr("ArticleCode")
                                dr1("EAN") = dr("EAN")
                                'dr1("SrNo") = dr("SrNo") + 1
                                'dr1("SrNo") = GetSrNo(BirthLisID, dr("ArticleCode"), dr("EAN")) + 1
                                Try
                                    dr1("SrNo") = dtNewBirthList.Select("articlecode = '" & dr1("ArticleCode") & "' and EAN = '" & dr1("EAN") & "'", "SrNo Desc", DataViewRowState.CurrentRows)(0)("SrNo") + 1
                                Catch ex As Exception
                                    dr1("SrNo") = 1
                                End Try

                                dr1("RequstedQty") = dr("BookedQty")
                                dr("RequstedQty") = dr("RequstedQty") - dr1("RequstedQty")
                                dr1("BookedQty") = dr("BookedQty")
                                dr("BookedQty") = 0
                                'dr1("DeliveredQty") = dr("DeliveredQty")
                                dr1("ReservedQty") = 0
                                'dr1("ConvToVoucher") = dr("ConvToVoucher")
                                dr1("IsCLP") = dr("IsCLP")
                                'dr1("FreeTexts") = dr("FreeTexts")
                                dr1("ReturnQty") = 0
                                'dr1("ReturnReason") = dr("ReturnReason")
                                dr1("AuthUserId") = dr("AuthUserId")
                                'dr1("AuthUserRemarks") = dr("AuthUserRemarks")
                                dr1("CLPPoints") = 0
                                dr1("CLPDiscount") = 0
                                'dr1("BLDiscountAmt") = dr("BLDiscountAmt")
                                'dr1("TotalDiscountAmt") = dr("TotalDiscountAmt")
                                dr1("CREATEDAT") = SiteCode
                                dr1("CREATEDBY") = UserName
                                dr1("CREATEDON") = DateTime.Now
                                dr1("UPDATEDBY") = UserName
                                dr1("UPDATEDAT") = SiteCode
                                dr1("UPDATEDON") = DateTime.Now
                                dr1("STATUS") = dr("STATUS")
                                dr1("SellingPrice") = dr("SellingPrice")
                                dr1("OriginalSellingPrice") = dr("OriginalSellingPrice")
                                dr1("IsPriceChanged") = True
                                dsBirthListSales.Tables("BirthListRequestedItems").Rows.Add(dr1)
                                'dr("UPDATEDON") = DateTime.Now
                                dr("SellingPrice") = dr("ActualSellingPrice")
                                dr("IsPriceChanged") = True
                            End If
                            'End of change
                        Else

                            If (dr("PurchasedQty") > 0) Or dr("PickUpQty") > 0 Then
                                If CBool(dr("IsPriceChangedHere")) And Not dr.RowState = DataRowState.Added Then


                                    'Add the row to the table
                                    Dim dr3 As DataRow = dt.Rows(0)
                                    Dim dr2 As DataRow = dsBirthListSales.Tables("BirthListRequestedItems").NewRow()
                                    dr2("SiteCode") = dr3("SiteCode")
                                    dr2("FinYear") = dr3("FinYear")
                                    dr2("BirthListID") = dr3("BirthListID")
                                    dr2("ArticleCode") = dr3("ArticleCode")
                                    dr2("EAN") = dr3("EAN")
                                    dr2("SrNo") = dr3("SrNo")
                                    'dr2("ConvToVoucher") = dr3("ConvToVoucher")
                                    dr2("IsCLP") = dr3("IsCLP")
                                    dr2("FreeTexts") = dr3("FreeTexts")
                                    dr2("ReturnQty") = dr3("ReturnQty")
                                    dr2("ReturnReason") = dr3("ReturnReason")
                                    dr2("AuthUserId") = dr3("AuthUserId")
                                    dr2("AuthUserRemarks") = dr3("AuthUserRemarks")
                                    dr2("CLPPoints") = dr3("CLPPoints")
                                    dr2("CLPDiscount") = dr3("CLPDiscount")
                                    dr2("BLDiscountAmt") = dr3("BLDiscountAmt")
                                    dr2("TotalDiscountAmt") = dr3("TotalDiscountAmt")
                                    dr2("CREATEDAT") = dr3("CREATEDAT")
                                    dr2("CREATEDBY") = dr3("CREATEDBY")
                                    dr2("CREATEDON") = dr3("CREATEDON")
                                    dr2("UPDATEDBY") = dr3("UPDATEDBY")
                                    dr2("UPDATEDAT") = dr3("UPDATEDAT")
                                    dr2("UPDATEDON") = dr3("UPDATEDON")
                                    dr2("STATUS") = dr3("STATUS")
                                    dr2("SellingPrice") = dr3("SellingPrice")
                                    dr2("OriginalSellingPrice") = dr3("OriginalSellingPrice")
                                    dr2("IsPriceChanged") = dr3("IsPriceChanged")
                                    dr2("RequstedQty") = dr3("RequstedQty")
                                    dr2("BookedQty") = dr3("BookedQty")
                                    dr2("DeliveredQty") = dr3("DeliveredQty")
                                    dr2("ReservedQty") = dr3("ReservedQty")
                                    dsBirthListSales.Tables("BirthListRequestedItems").Rows.Add(dr2)
                                    dsBirthListSales.Tables("BirthListRequestedItems").Rows(dsBirthListSales.Tables("BirthListRequestedItems").Rows.Count - 1).AcceptChanges()
                                    dsBirthListSales.Tables("BirthListRequestedItems").Rows(dsBirthListSales.Tables("BirthListRequestedItems").Rows.Count - 1).SetModified()
                                    dr2 = dsBirthListSales.Tables("BirthListRequestedItems").Rows(dsBirthListSales.Tables("BirthListRequestedItems").Rows.Count - 1)

                                    dr2("RequstedQty") = dr3("RequstedQty") + dr("PurchasedQty")
                                    dr2("BookedQty") = dr3("BookedQty") + dr("PurchasedQty")
                                    dr2("DeliveredQty") = dr3("DeliveredQty") + dr("PickUpQty")
                                    If Not dr("ReservedQty") Is DBNull.Value Then
                                        dr2("ReservedQty") = dr3("ReservedQty") - dr("PickUpQty")
                                        If dr2("ReservedQty") < 0 Then
                                            dr2("ReservedQty") = 0
                                        End If
                                    Else
                                        dr2("ReservedQty") = 0
                                    End If
                                    dr("RequstedQty") = dr("RequstedQty") - dr("BookedQty")
                                    dr("BookedQty") = 0
                                    dr("DeliveredQty") = dr("DeliveredQty") - dr("PickUpQty")
                                    dr("SellingPrice") = dr("ActualSellingPrice")
                                End If
                            End If

                            'dsBirthListSales.Tables("BirthListRequestedItems").Rows(dsBirthListSales.Tables("BirthListRequestedItems").Rows.Count - 1).SetModified()
                        End If
                    Else


                        If (dr("PurchasedQty") > 0) Or dr("PickUpQty") > 0 Then
                            If CBool(dr("IsPriceChangedHere")) And Not dr.RowState = DataRowState.Added Then
                                'If price has been changed then we must add new row in BLRequested table
                                'and update the quantities of the existing row

                                If Not dr("IsPriceChanged") And dr("RequstedQty") = dr("PurchasedQty") Then
                                    dr("IsPriceChanged") = True
                                    dr("UPDATEDON") = DateTime.Now
                                Else
                                    Dim dr1 As DataRow = dsBirthListSales.Tables("BirthListRequestedItems").NewRow()
                                    dr1("SiteCode") = dr("SiteCode")
                                    dr1("ArticleCode") = dr("ArticleCode")
                                    dr1("EAN") = dr("EAN")
                                    'dr1("SrNo") = dr("SrNo") + 1
                                    'dr1("SrNo") = GetSrNo(BirthLisID, dr("ArticleCode"), dr("EAN")) + 1
                                    Try
                                        dr1("SrNo") = dtNewBirthList.Select("articlecode = '" & dr("ArticleCode") & "' and EAN = '" & dr("EAN") & "'", "SrNo Desc", DataViewRowState.CurrentRows)(0)("SrNo") + 1
                                    Catch ex As Exception
                                        dr1("SrNo") = 1
                                    End Try


                                    'Commented line on Dec 30 for handling 0 order qty scenario
                                    'dr1("RequstedQty") = dr("BookedQty")
                                    'dr("RequstedQty") = dr("RequstedQty") - dr1("RequstedQty")
                                    'dr1("BookedQty") = dr("BookedQty")
                                    'dr("BookedQty") = 0

                                    dr1("RequstedQty") = dr("PurchasedQty")
                                    dr("RequstedQty") = dr("RequstedQty") - dr1("RequstedQty")
                                    dr1("BookedQty") = dr("PurchasedQty")
                                    dr("BookedQty") = dr("BookedQty") - dr("PurchasedQty")

                                    dr1("DeliveredQty") = dr("PickUpQty")
                                    dr("DeliveredQty") = dr("DeliveredQty") - dr("PickUpQty")
                                    dr1("ReservedQty") = 0
                                    dr1("ConvToVoucher") = 0
                                    dr1("IsCLP") = dr("IsCLP")
                                    'dr1("FreeTexts") = dr("FreeTexts")
                                    dr1("ReturnQty") = 0
                                    'dr1("ReturnReason") = dr("ReturnReason")
                                    dr1("AuthUserId") = dr("AuthUserId")
                                    'dr1("AuthUserRemarks") = dr("AuthUserRemarks")
                                    dr1("CLPPoints") = 0
                                    dr1("CLPDiscount") = 0
                                    'dr1("BLDiscountAmt") = dr("BLDiscountAmt")
                                    'dr1("TotalDiscountAmt") = dr("TotalDiscountAmt")
                                    dr1("CREATEDAT") = SiteCode
                                    dr1("CREATEDBY") = UserName
                                    dr1("CREATEDON") = DateTime.Now
                                    dr1("UPDATEDBY") = UserName
                                    dr1("UPDATEDAT") = SiteCode
                                    dr1("UPDATEDON") = DateTime.Now
                                    dr1("STATUS") = dr("STATUS")
                                    dr1("SellingPrice") = dr("SellingPrice")
                                    dr1("OriginalSellingPrice") = dr("OriginalSellingPrice")
                                    dr1("IsPriceChanged") = True
                                    dsBirthListSales.Tables("BirthListRequestedItems").Rows.Add(dr1)
                                    'dr("UPDATEDON") = DateTime.Now
                                    dr("SellingPrice") = dr("ActualSellingPrice")
                                    dr("IsPriceChanged") = True
                                End If

                            Else
                                If dr.RowState = DataRowState.Added Then
                                    dr("IsPriceChanged") = dr("IsPriceChangedHere")
                                    dr("OriginalSellingPrice") = dr("SellingPrice")
                                    dr("CREATEDAT") = SiteCode
                                    dr("CREATEDBY") = UserName
                                    dr("CREATEDON") = DateTime.Now
                                    dr("UPDATEDBY") = UserName
                                    dr("UPDATEDAT") = SiteCode
                                    dr("UPDATEDON") = DateTime.Now
                                    ' dr("Srno") = dtNewBirthList.Select("articlecode = '" & dr("ArticleCode") & "' and EAN = '" & dr("EAN") & "'", "SrNo Desc", DataViewRowState.CurrentRows)(0)("SrNo") + 1
                                End If
                            End If
                        Else

                        End If
                    End If


                    Dim int As Integer = dr("DeliveredQty")
                    Dim openqty As Integer = dr("ConvToVoucher")
                    dsBirthListSales.Tables("BirthListRequestedItems").ImportRow(dr)
                    'Added for issue of not updating seconde line item delivered qty
                    dsBirthListSales.Tables("BirthListRequestedItems").Rows(dsBirthListSales.Tables("BirthListRequestedItems").Rows.Count - 1)("DeliveredQty") = int
                    dsBirthListSales.Tables("BirthListRequestedItems").Rows(dsBirthListSales.Tables("BirthListRequestedItems").Rows.Count - 1)("ConvToVoucher") = openqty
                    iRowIndex += 1
                End If
            Next

            If Not dtbirthlistrequestediems Is Nothing Then
                Try
                    Dim dtTempDeleted As DataTable = dtbirthlistrequestediems.GetChanges(System.Data.DataRowState.Deleted)
                    If Not dtTempDeleted Is Nothing Then
                        dsBirthListSales.Tables("BirthListRequestedItems").Merge(dtbirthlistrequestediems.GetChanges(System.Data.DataRowState.Deleted), False, MissingSchemaAction.Ignore)
                    End If

                    'Rakesh-04.09.2013:Issue-7797-->Reserved qty not updated in edit birthlist
                    For Each drItem In dtbirthlistrequestediems.Rows
                        Dim drRequestedItems = dsBirthListSales.Tables("BirthListRequestedItems").Select("IsPriceChanged=False And ArticleCode='" & drItem("ArticleCode") & "' and EAN='" & drItem("EAN") & "'").FirstOrDefault()

                        drRequestedItems("ReservedQty") = drItem("ReservedQty")
                    Next

                Catch ex As Exception
                    LogException(ex)
                End Try
            End If

            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function
    ''' <summary>
    '''  Update when no change in to ReturnQty
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function UpdateBirthListRequestedItem_ADPForSale(Optional ByVal dtbirthlistrequestediems As DataTable = Nothing) As Boolean
        Try
            Dim iRowIndex As Integer = 0
            dsBirthListSales.Tables("BirthListRequestedItems").Clear()
            Dim dtNewBirthList As DataTable = _dtBirthListItemDetail
            Dim objClsBirthListGlobal As New clsBirthListGobal
            dsBirthListSales.Tables("BirthListRequestedItems").Columns("BirthListID").DefaultValue = BirthLisID
            'dsBirthListSales.Tables("BirthListRequestedItems").Columns("FinYear").DefaultValue = FinacialYear
            If Not PreviousFinancialYear Is DBNull.Value And Not PreviousFinancialYear Is Nothing Then
                dsBirthListSales.Tables("BirthListRequestedItems").Columns("FinYear").DefaultValue = PreviousFinancialYear
            Else
                Try
                    If Not FinacialYear Is DBNull.Value Or Not FinacialYear = Nothing Then
                        dsBirthListSales.Tables("BirthListRequestedItems").Columns("FinYear").DefaultValue = FinacialYear
                    Else
                        Dim strFinYear As String = "select FinYear from Birthlist where BirthlistId = '" & BirthLisID & "' AND SiteCode = '" & SiteCode & "'"
                        Dim dcFinYear As New SqlCommand(strFinYear, SpectrumCon)
                        FinacialYear = dcFinYear.ExecuteScalar
                        If Not FinacialYear Is DBNull.Value Or Not FinacialYear = Nothing Then
                            dsBirthListSales.Tables("BirthListRequestedItems").Columns("FinYear").DefaultValue = FinacialYear
                        Else
                            dsBirthListSales.Tables("BirthListRequestedItems").Columns("FinYear").DefaultValue = objclsComman.GetFinancialYear(DateDayOpen, SiteCode)
                        End If
                    End If
                Catch ex As Exception
                    LogException(ex)
                End Try
            End If

            'dsBirthListSales.Tables("BirthListRequestedItems").Columns("CLPDiscount").DefaultValue = Decimal.Zero
            'dsBirthListSales.Tables("BirthListRequestedItems").Columns("BLDiscountAmt").DefaultValue = Decimal.Zero
            'dsBirthListSales.Tables("BirthListRequestedItems").Columns("TotalDiscountAmt").DefaultValue = Decimal.Zero
            For Each dr As DataRow In dtNewBirthList.Rows
                If Not (dr.RowState = DataRowState.Deleted) Then
                    If IsDBNull(dr("PickUpQty")) Then
                        dr("PickUpQty") = 0
                    End If
                    If IsDBNull(dr("PurchasedQty")) Then
                        dr("PurchasedQty") = 0
                    End If
                    If IsDBNull(dr("DeliveredQty")) Then
                        dr("DeliveredQty") = 0
                    End If
                    If Not (dr("Bookedqty") Is DBNull.Value) Then

                        dr("BookedQty") = dr("BookedQty") + dr("PurchasedQty")
                    Else
                        dr("BookedQty") = 0
                        dr("BookedQty") = dr("BookedQty") + dr("PurchasedQty")
                    End If
                    If Not dr("DeliveredQty") Is DBNull.Value Then
                        dr("DeliveredQty") = dr("DeliveredQty") + dr("PickUpQty")
                    Else
                        dr("DeliveredQty") = 0
                        dr("DeliveredQty") = dr("DeliveredQty") + dr("PickUpQty")
                    End If
                    If Not dr("ReservedQty") Is DBNull.Value Then
                        dr("ReservedQty") = dr("ReservedQty") - dr("PickUpQty")
                        If dr("ReservedQty") < 0 Then
                            dr("ReservedQty") = 0
                        End If
                    Else
                        dr("ReservedQty") = 0
                    End If

                    Dim dt As New DataTable
                    dt = GetBLReqRecord(BirthLisID, dr("ArticleCode"), dr("EAN"), dr("SellingPrice"), FinacialYear)
                    If dt.Rows.Count > 0 Then
                        Dim srno As String = dt.Rows(0)("SrNo")
                        If srno = "1" Then
                            'Changes by Ashish for CR 5679
                            If CBool(dr("IsPriceChangedHere")) Then
                                'If price has been changed then we must add new row in BLRequested table
                                'and update the quantities of the existing row
                                Dim dr1 As DataRow = dsBirthListSales.Tables("BirthListRequestedItems").NewRow()
                                dr1("SiteCode") = dr("SiteCode")
                                dr1("ArticleCode") = dr("ArticleCode")
                                dr1("EAN") = dr("EAN")
                                'dr1("SrNo") = dr("SrNo") + 1
                                'dr1("SrNo") = GetSrNo(BirthLisID, dr("ArticleCode"), dr("EAN")) + 1
                                Try
                                    dr1("SrNo") = dtNewBirthList.Select("articlecode = '" & dr1("ArticleCode") & "' and EAN = '" & dr1("EAN") & "'", "SrNo Desc", DataViewRowState.CurrentRows)(0)("SrNo") + 1
                                Catch ex As Exception
                                    dr1("SrNo") = 1
                                End Try

                                dr1("RequstedQty") = dr("BookedQty")
                                dr("RequstedQty") = dr("RequstedQty") - dr1("RequstedQty")
                                dr1("BookedQty") = dr("BookedQty")
                                dr("BookedQty") = 0
                                'dr1("DeliveredQty") = dr("DeliveredQty")
                                dr1("ReservedQty") = dr("ReservedQty")
                                dr1("ConvToVoucher") = dr("ConvToVoucher")
                                dr1("IsCLP") = dr("IsCLP")
                                'dr1("FreeTexts") = dr("FreeTexts")
                                dr1("ReturnQty") = 0
                                'dr1("ReturnReason") = dr("ReturnReason")
                                dr1("AuthUserId") = dr("AuthUserId")
                                'dr1("AuthUserRemarks") = dr("AuthUserRemarks")
                                dr1("CLPPoints") = 0
                                dr1("CLPDiscount") = 0
                                'dr1("BLDiscountAmt") = dr("BLDiscountAmt")
                                'dr1("TotalDiscountAmt") = dr("TotalDiscountAmt")
                                dr1("CREATEDAT") = SiteCode
                                dr1("CREATEDBY") = UserName
                                dr1("CREATEDON") = DateTime.Now
                                dr1("UPDATEDBY") = UserName
                                dr1("UPDATEDAT") = SiteCode
                                dr1("UPDATEDON") = DateTime.Now
                                dr1("STATUS") = dr("STATUS")
                                dr1("SellingPrice") = dr("SellingPrice")
                                dr1("OriginalSellingPrice") = dr("OriginalSellingPrice")
                                dr1("IsPriceChanged") = True
                                dsBirthListSales.Tables("BirthListRequestedItems").Rows.Add(dr1)
                                'dr("UPDATEDON") = DateTime.Now
                                dr("SellingPrice") = dr("ActualSellingPrice")
                                dr("IsPriceChanged") = True
                            Else
                                If Not dr("ActualSellingPrice") Is DBNull.Value Then
                                    dr("SellingPrice") = dr("ActualSellingPrice")
                                ElseIf Not dr("OriginalSellingPrice") Is DBNull.Value Then
                                    dr("SellingPrice") = dr("OriginalSellingPrice")
                                Else
                                    dr("SellingPrice") = dt.Select("IsPriceChanged = False")(0)("SellingPrice")
                                    dr("IsPriceChanged") = False
                                    dr("OriginalSellingPrice") = dt.Select("IsPriceChanged = False")(0)("OriginalSellingPrice")
                                End If

                            End If
                            'End of change
                        Else

                            If (dr("PurchasedQty") > 0) Or dr("PickUpQty") > 0 Then
                                If CBool(dr("IsPriceChangedHere")) And Not dr.RowState = DataRowState.Added Then
                                    'Add the row to the table
                                    Dim dr3 As DataRow = dt.Rows(0)
                                    Dim dr2 As DataRow = dsBirthListSales.Tables("BirthListRequestedItems").NewRow()
                                    dr2("SiteCode") = dr3("SiteCode")
                                    dr2("FinYear") = dr3("FinYear")
                                    dr2("BirthListID") = dr3("BirthListID")
                                    dr2("ArticleCode") = dr3("ArticleCode")
                                    dr2("EAN") = dr3("EAN")
                                    dr2("SrNo") = dr3("SrNo")
                                    dr2("ConvToVoucher") = dr3("ConvToVoucher")
                                    dr2("IsCLP") = dr3("IsCLP")
                                    dr2("FreeTexts") = dr3("FreeTexts")
                                    dr2("ReturnQty") = dr3("ReturnQty")
                                    dr2("ReturnReason") = dr3("ReturnReason")
                                    dr2("AuthUserId") = dr3("AuthUserId")
                                    dr2("AuthUserRemarks") = dr3("AuthUserRemarks")
                                    dr2("CLPPoints") = dr3("CLPPoints")
                                    dr2("CLPDiscount") = dr3("CLPDiscount")
                                    dr2("BLDiscountAmt") = dr3("BLDiscountAmt")
                                    dr2("TotalDiscountAmt") = dr3("TotalDiscountAmt")
                                    dr2("CREATEDAT") = dr3("CREATEDAT")
                                    dr2("CREATEDBY") = dr3("CREATEDBY")
                                    dr2("CREATEDON") = dr3("CREATEDON")
                                    dr2("UPDATEDBY") = dr3("UPDATEDBY")
                                    dr2("UPDATEDAT") = dr3("UPDATEDAT")
                                    dr2("UPDATEDON") = dr3("UPDATEDON")
                                    dr2("STATUS") = dr3("STATUS")
                                    dr2("SellingPrice") = dr3("SellingPrice")
                                    dr2("OriginalSellingPrice") = dr3("OriginalSellingPrice")
                                    dr2("IsPriceChanged") = dr3("IsPriceChanged")
                                    dr2("RequstedQty") = dr3("RequstedQty")
                                    dr2("BookedQty") = dr3("BookedQty")
                                    dr2("DeliveredQty") = dr3("DeliveredQty")
                                    dr2("ReservedQty") = dr3("ReservedQty")
                                    dsBirthListSales.Tables("BirthListRequestedItems").Rows.Add(dr2)
                                    dsBirthListSales.Tables("BirthListRequestedItems").Rows(dsBirthListSales.Tables("BirthListRequestedItems").Rows.Count - 1).AcceptChanges()
                                    dsBirthListSales.Tables("BirthListRequestedItems").Rows(dsBirthListSales.Tables("BirthListRequestedItems").Rows.Count - 1).SetModified()
                                    dr2 = dsBirthListSales.Tables("BirthListRequestedItems").Rows(dsBirthListSales.Tables("BirthListRequestedItems").Rows.Count - 1)

                                    dr2("RequstedQty") = dr3("RequstedQty") + dr("BookedQty")
                                    dr2("BookedQty") = dr3("BookedQty") + dr("BookedQty")
                                    dr2("DeliveredQty") = dr3("DeliveredQty") + dr("PickUpQty")
                                    If Not dr("ReservedQty") Is DBNull.Value Then
                                        dr2("ReservedQty") = dr3("ReservedQty") - dr("PickUpQty")
                                        If dr2("ReservedQty") < 0 Then
                                            dr2("ReservedQty") = 0
                                        End If
                                    Else
                                        dr2("ReservedQty") = 0
                                    End If
                                    dr("RequstedQty") = dr("RequstedQty") - dr("BookedQty")
                                    dr("BookedQty") = 0
                                    dr("DeliveredQty") = dr("DeliveredQty") - dr("PickUpQty")
                                    dr("SellingPrice") = dr("ActualSellingPrice")
                                End If
                            Else
                                'dr("SellingPrice") = dr("ActualSellingPrice")

                                If Not dr("ActualSellingPrice") Is DBNull.Value Then
                                    dr("SellingPrice") = dr("ActualSellingPrice")
                                ElseIf Not dr("OriginalSellingPrice") Is DBNull.Value Then
                                    dr("SellingPrice") = dr("OriginalSellingPrice")
                                Else
                                    dr("SellingPrice") = dt.Select("IsPriceChanged = False")(0)("SellingPrice")
                                    dr("IsPriceChanged") = False
                                    dr("OriginalSellingPrice") = dt.Select("IsPriceChanged = False")(0)("OriginalSellingPrice")
                                End If
                            End If
                            'dsBirthListSales.Tables("BirthListRequestedItems").Rows(dsBirthListSales.Tables("BirthListRequestedItems").Rows.Count - 1).SetModified()
                        End If
                    Else
                        'Changes by Ashish for CR 5679
                        If CBool(dr("IsPriceChangedHere")) And Not dr.RowState = DataRowState.Added Then
                            'If price has been changed then we must add new row in BLRequested table
                            'and update the quantities of the existing row

                            Dim _dtCopyTable As DataTable = dtNewBirthList.Select("ArticleCode = '" & dr("ArticleCode") & "'").CopyToDataTable()
                            'If _dtCopyTable.Rows.Count = 1 And Not dr("IsPriceChanged") Then
                            '    dr("IsPriceChanged") = True
                            '    dr("UPDATEDON") = DateTime.Now
                            'Else
                            If Not dr("IsPriceChanged") And dr("RequstedQty") = dr("PurchasedQty") Then
                                dr("IsPriceChanged") = True
                                dr("UPDATEDON") = DateTime.Now
                            Else
                                Dim dr1 As DataRow = dsBirthListSales.Tables("BirthListRequestedItems").NewRow()
                                dr1("SiteCode") = dr("SiteCode")
                                dr1("ArticleCode") = dr("ArticleCode")
                                dr1("EAN") = dr("EAN")
                                'dr1("SrNo") = dr("SrNo") + 1
                                'dr1("SrNo") = GetSrNo(BirthLisID, dr("ArticleCode"), dr("EAN")) + 1
                                Try
                                    dr1("SrNo") = dtNewBirthList.Select("articlecode = '" & dr1("ArticleCode") & "' and EAN = '" & dr1("EAN") & "'", "SrNo Desc", DataViewRowState.CurrentRows)(0)("SrNo") + 1
                                Catch ex As Exception
                                    dr1("SrNo") = 1
                                End Try

                                'dr1("RequstedQty") = dr("BookedQty")
                                dr1("RequstedQty") = dr("PurchasedQty")
                                dr("RequstedQty") = dr("RequstedQty") - dr1("RequstedQty")
                                'dr1("BookedQty") = dr("BookedQty")
                                dr1("BookedQty") = dr("PurchasedQty")
                                dr("BookedQty") = dr("BookedQty") - dr("PurchasedQty")
                                'dr1("DeliveredQty") = dr("DeliveredQty")
                                dr1("DeliveredQty") = dr("PickUpQty")
                                dr("DeliveredQty") = dr("DeliveredQty") - dr("PickUpQty")
                                dr1("ReservedQty") = 0
                                dr1("ConvToVoucher") = 0
                                dr1("IsCLP") = dr("IsCLP")
                                'dr1("FreeTexts") = dr("FreeTexts")
                                dr1("ReturnQty") = 0
                                'dr1("ReturnReason") = dr("ReturnReason")
                                dr1("AuthUserId") = dr("AuthUserId")
                                'dr1("AuthUserRemarks") = dr("AuthUserRemarks")
                                dr1("CLPPoints") = 0
                                dr1("CLPDiscount") = 0
                                'dr1("BLDiscountAmt") = dr("BLDiscountAmt")
                                'dr1("TotalDiscountAmt") = dr("TotalDiscountAmt")
                                dr1("CREATEDAT") = SiteCode
                                dr1("CREATEDBY") = UserName
                                dr1("CREATEDON") = DateTime.Now
                                dr1("UPDATEDBY") = UserName
                                dr1("UPDATEDAT") = SiteCode
                                dr1("UPDATEDON") = DateTime.Now
                                dr1("STATUS") = dr("STATUS")
                                dr1("SellingPrice") = dr("SellingPrice")
                                dr1("OriginalSellingPrice") = dr("OriginalSellingPrice")
                                dr1("IsPriceChanged") = True
                                dsBirthListSales.Tables("BirthListRequestedItems").Rows.Add(dr1)
                                'dr("UPDATEDON") = DateTime.Now
                                dr("SellingPrice") = dr("ActualSellingPrice")
                                dr("IsPriceChanged") = True
                            End If
                        Else
                            If dr.RowState = DataRowState.Added Then
                                dr("IsPriceChanged") = dr("IsPriceChangedHere")
                                dr("OriginalSellingPrice") = dr("SellingPrice")
                                dr("CREATEDAT") = SiteCode
                                dr("CREATEDBY") = UserName
                                dr("CREATEDON") = DateTime.Now
                                dr("UPDATEDBY") = UserName
                                dr("UPDATEDAT") = SiteCode
                                dr("UPDATEDON") = DateTime.Now
                            End If
                        End If
                        'End of change
                    End If
                    Dim int As Integer = dr("DeliveredQty")

                    dsBirthListSales.Tables("BirthListRequestedItems").ImportRow(dr)
                    'Added for issue of not updating seconde line item delivered qty
                    dsBirthListSales.Tables("BirthListRequestedItems").Rows(dsBirthListSales.Tables("BirthListRequestedItems").Rows.Count - 1)("DeliveredQty") = int

                    iRowIndex += 1
                End If
            Next
            If Not dtbirthlistrequestediems Is Nothing Then
                Try
                    Dim dtTempDeleted As DataTable = dtbirthlistrequestediems.GetChanges(System.Data.DataRowState.Deleted)
                    If Not dtTempDeleted Is Nothing Then
                        dsBirthListSales.Tables("BirthListRequestedItems").Merge(dtbirthlistrequestediems.GetChanges(System.Data.DataRowState.Deleted), False, MissingSchemaAction.Ignore)
                    End If
                Catch ex As Exception
                    LogException(ex)
                End Try

            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function
    ''' <summary>
    ''' Saving Gift Voucher Deatails
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function Save_GV() As Boolean
        Try
            If Not DataTableVoucherSales Is Nothing Then
                Dim dtVoucher As DataTable = DataTableVoucherSales
                For Each dr As DataRow In dtVoucher.Rows
                    If Not (dr.RowState = DataRowState.Deleted) Then
                        dsBirthListSales.Tables("BirthListRequestedItems").Columns("CREATEDAT").DefaultValue = SiteCode
                        dsBirthListSales.Tables("BirthListRequestedItems").Columns("CREATEDBY").DefaultValue = UserName
                        dsBirthListSales.Tables("BirthListRequestedItems").Columns("CREATEDON").DefaultValue = CreationDate
                        dsBirthListSales.Tables("BirthListRequestedItems").Columns("UPDATEDAT").DefaultValue = SiteCode
                        dsBirthListSales.Tables("BirthListRequestedItems").Columns("UPDATEDBY").DefaultValue = UserName
                        dsBirthListSales.Tables("BirthListRequestedItems").Columns("UPDATEDON").DefaultValue = CreationDate
                        dsBirthListSales.Tables("BirthListRequestedItems").Columns("STATUS").DefaultValue = 1
                        dsBirthListSales.Tables("BirthListRequestedItems").ImportRow(dr)
                        _dtBirthListItemDetail.ImportRow(dr)
                    End If
                Next
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function



    Private dsCLP As New DataSet
    Private Function Save_CLPTransaction() As Boolean
        Try
            dsCLP.Clear()
            Dim dtCLPTransaction As DataTable = dsBirthListSales.Tables("CLPTransactionOLD").Clone()
            dtCLPTransaction.TableName = "CLPTransaction"
            dsCLP.Tables.Add(dtCLPTransaction)

            dsCLP.Tables("CLPTransaction").Columns("BillNo").DefaultValue = SaleInVoiceNumber

            If Not SelectedBirthListInfo Is Nothing Then
                Dim drCLPTransaction As DataRow
                Dim objCLPPoints As Object
                Dim decCLPPoints As Decimal
                If _dtBirthListItemDetail.Rows.Count > 0 Then

                    objCLPPoints = _dtBirthListItemDetail.Compute("sum(CLPPoints)", "purchasedqty>0 and IsCLP=true")
                    If Not objCLPPoints Is Nothing And Not objCLPPoints Is DBNull.Value Then
                        decCLPPoints = CDbl(objCLPPoints)
                    End If
                    If decCLPPoints > Decimal.Zero Then
                        Dim drBirthListItemDetail As DataRow = _dtBirthListItemDetail.Rows(0)
                        drCLPTransaction = dsCLP.Tables("CLPTransaction").NewRow()
                        drCLPTransaction("SiteCode") = SiteCode
                        'drOrderHdr("DocumentNumber") = OrderDocumentNumber()
                        'drCLPTransaction("FinYear") = objClsBirthListGlobal.FinYear
                        'drCLPTransaction("BillNo") = drBirthListItemDetail("articlecode")
                        drCLPTransaction("BillDate") = _dateToday.Date
                        drCLPTransaction("AccumLationPoints") = decCLPPoints
                        If dsBirthListSales.Tables("Salesinvoice").Rows.Count > 0 Then
                            Dim redpoint As Decimal = 0.0
                            For Each dr1 As DataRow In dsBirthListSales.Tables("Salesinvoice").Select("TenderTypeCode='CLPPoint'")
                                redpoint += CDec(dr1("AmountTendered"))
                            Next
                        Else
                            drCLPTransaction("RedemptionPoints") = Decimal.Zero
                        End If


                        drCLPTransaction("BalAccumlationPoints") = decCLPPoints
                        drCLPTransaction("Finyear") = System.DateTime.Now.Year


                        If RelativesPoint Then
                            drCLPTransaction("ClpCustomerId") = _ParentCardNo
                            drCLPTransaction("ClpProgramId") = _ParentCLPProg
                        Else
                            drCLPTransaction("ClpCustomerId") = CustomerID
                            drCLPTransaction("ClpProgramId") = CLPProgrmaID
                        End If


                        drCLPTransaction("IsRedemptionProcess") = False
                        drCLPTransaction("CREATEDAT") = SiteCode
                        drCLPTransaction("CREATEDBY") = UserName
                        drCLPTransaction("CREATEDON") = _dateToday
                        drCLPTransaction("UPDATEDAT") = SiteCode
                        drCLPTransaction("UPDATEDBY") = UserName
                        drCLPTransaction("UPDATEDON") = _dateToday
                        drCLPTransaction("STATUS") = 0
                        dsCLP.Tables("CLPTransaction").Rows.Add(drCLPTransaction)
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function


    Private Function Save_CLPTransactionsDetails() As Boolean
        Try
            Dim iNext As Integer = 1
            Dim dtCLPTransactionsDetails As DataTable = dsBirthListSales.Tables("CLPTransactionsDetailsOLD").Clone()
            dtCLPTransactionsDetails.TableName = "CLPTransactionsDetails"
            dsCLP.Tables.Add(dtCLPTransactionsDetails)
            dsCLP.Tables("CLPTransactionsDetails").Columns("BillNo").DefaultValue = SaleInVoiceNumber
            Dim objClsBirthListGlobal As New clsBirthListGobal

            Dim drCLPTransactionDetails As DataRow
            For Each drBirthListItemDetail As DataRow In _dtBirthListItemDetail.Rows
                If (drBirthListItemDetail("PurchasedQty") > 0) Then
                    drCLPTransactionDetails = dsCLP.Tables("CLPTransactionsDetails").NewRow()
                    drCLPTransactionDetails("SiteCode") = SiteCode

                    'drOrderHdr("FinYear") = objClsBirthListGlobal.FinYear
                    'drCLPTransactionDetails("BillDate") =CreationDate
                    drCLPTransactionDetails("BillLineNo") = iNext
                    drCLPTransactionDetails("ArticleCode") = drBirthListItemDetail("articlecode")
                    drCLPTransactionDetails("SellingPrice") = drBirthListItemDetail("SellingPrice")

                    drCLPTransactionDetails("Quantity") = drBirthListItemDetail("PurchasedQty")
                    drCLPTransactionDetails("CLPPoints") = drBirthListItemDetail("CLPPoints")
                    drCLPTransactionDetails("CLPDiscount") = drBirthListItemDetail("CLPDiscount")

                    drCLPTransactionDetails("EAN") = drBirthListItemDetail("EAN")


                    drCLPTransactionDetails("CREATEDAT") = SiteCode
                    drCLPTransactionDetails("CREATEDBY") = UserName
                    drCLPTransactionDetails("CREATEDON") = _dateToday
                    drCLPTransactionDetails("UPDATEDAT") = SiteCode
                    drCLPTransactionDetails("UPDATEDBY") = UserName
                    drCLPTransactionDetails("UPDATEDON") = _dateToday
                    drCLPTransactionDetails("STATUS") = True
                    dsCLP.Tables("CLPTransactionsDetails").Rows.Add(drCLPTransactionDetails)
                    iNext += 1
                End If
            Next
            Return True

        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Function Save_CLPCustomer() As Boolean
        Try
            Dim drCLPTransactionDetails As DataRow
            Dim dtCLPTransactionsDetails As DataTable = dsBirthListSales.Tables("CLPCustomersOLD").Clone()
            dtCLPTransactionsDetails.TableName = "CLPCustomers"

            dsCLP.Tables.Add(dtCLPTransactionsDetails)
            drCLPTransactionDetails = dsCLP.Tables("CLPCustomers").NewRow()

            For Each dr1 As DataRow In dsBirthListSales.Tables("CLPCustomersOLD").Rows

                dr1.BeginEdit()
                Dim objTotalBal As Decimal = _dtBirthListItemDetail.Compute("sum(CLPPoints)", "purchasedqty>0 and IsCLP=true ")
                Dim objPOINTSACCUMLATED As Object = dr1("POINTSACCUMLATED")
                Dim decPOINTSACCUMLATED As Decimal
                If Not objPOINTSACCUMLATED Is Nothing And Not objPOINTSACCUMLATED Is DBNull.Value Then
                    decPOINTSACCUMLATED = CDbl(objPOINTSACCUMLATED)
                End If
                Dim objPointsRedeemed As Object = dr1("PointsRedeemed")
                If dsBirthListSales.Tables("salesinvoice").Select("TenderTypecode='CLPPoint'").Count > 0 Then
                    For Each drred As DataRow In dsBirthListSales.Tables("salesinvoice").Select("TenderTypecode='CLPPoint'")
                        objPointsRedeemed += CDec(drred("AmountTendered"))
                        dr1("PointsRedeemed") = objPointsRedeemed
                    Next
                End If


                Dim decTotalBalancePoint As Decimal
                If Not objPointsRedeemed Is Nothing And Not objPointsRedeemed Is DBNull.Value Then
                    decTotalBalancePoint = Decimal.Subtract(decPOINTSACCUMLATED, CDbl(objPointsRedeemed))
                    decTotalBalancePoint = Decimal.Add(decTotalBalancePoint, objTotalBal)
                Else
                    decTotalBalancePoint = Decimal.Add(objTotalBal, decPOINTSACCUMLATED)
                End If

                dr1("POINTSACCUMLATED") = Decimal.Add(objTotalBal, decPOINTSACCUMLATED)
                dr1("TotalBalancePoint") = decTotalBalancePoint
                dr1.EndEdit()

            Next

            dsCLP.Tables("CLPCustomers").Merge(dsBirthListSales.Tables("CLPCustomersOLD"))

            dsCLP.Tables("CLPCustomers").AcceptChanges()



            For Each dr As DataRow In dsCLP.Tables("CLPCustomers").Rows
                dr.BeginEdit()
                If dsBirthListSales.Tables("CLPCustomersOLD").Rows.Count > 0 Then
                    dr("SiteCode") = dsBirthListSales.Tables("CLPCustomersOLD").Rows(0)("SiteCode")
                Else
                    dr("SiteCode") = SiteCode
                End If


                dr("CardNo") = CustomerID
                dr("ClpProgramId") = CLPProgrmaID
                'Dim objTotalBal As Decimal = _dtBirthListItemDetail.Compute("sum(CLPPoints)", "purchasedqty>0 and IsCLP=true ")
                'Dim objPOINTSACCUMLATED As Object = dr("POINTSACCUMLATED")
                'Dim decPOINTSACCUMLATED As Decimal
                'If Not objPOINTSACCUMLATED Is Nothing And Not objPOINTSACCUMLATED Is DBNull.Value Then
                '    decPOINTSACCUMLATED = CDbl(objPOINTSACCUMLATED)
                'End If
                'Dim objPointsRedeemed As Object = dr("PointsRedeemed")
                'If dsBirthListSales.Tables("salesinvoice").Select("TenderTypecode='CLPPoint'").Count > 0 Then
                '    For Each drred As DataRow In dsBirthListSales.Tables("salesinvoice").Select("TenderTypecode='CLPPoint'")
                '        objPointsRedeemed += drred("AmountTendered")
                '        dr("PointsRedeemed") = objPointsRedeemed
                '    Next
                'End If


                'Dim decTotalBalancePoint As Decimal
                'If Not objPointsRedeemed Is Nothing And Not objPointsRedeemed Is DBNull.Value Then
                '    decTotalBalancePoint = Decimal.Subtract(decPOINTSACCUMLATED, CDbl(objPointsRedeemed))
                '    decTotalBalancePoint = Decimal.Add(decTotalBalancePoint, objTotalBal)
                'Else
                '    decTotalBalancePoint = Decimal.Add(objTotalBal, decPOINTSACCUMLATED)
                'End If

                'dr("POINTSACCUMLATED") = Decimal.Add(objTotalBal, decPOINTSACCUMLATED)
                'dr("TotalBalancePoint") = decTotalBalancePoint
                dr.EndEdit()
            Next



        Catch ex As Exception
            LogException(ex)

        End Try
    End Function


    Private _OnlineConnect As Boolean
    Public Property OnlineConnect() As Boolean
        Get
            Return _OnlineConnect
        End Get
        Set(ByVal value As Boolean)
            _OnlineConnect = value
        End Set
    End Property


    ''' <summary>
    ''' Saving all information for birthlist
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <usedby>frmBirthListSales.vb</usedby>
    ''' <remarks></remarks>
    Public Function Save(ByVal Online As Boolean, ByVal RoundedAmount As Double, Optional ByRef strPickUpQty As String = "", Optional ByRef IsTransactionSuccess As Boolean = True, Optional ByVal posDbBirthListRequested As POSDBDataSet.BirthListRequestedItemsDataTable = Nothing, Optional ByVal posDtBirthList As POSDBDataSet.BirthListDataTable = Nothing, Optional ByVal posAdaptorBirthListReq As POSDBDataSetTableAdapters.BirthListRequestedItemsTableAdapter = Nothing, Optional ByRef posAdaptorBL As POSDBDataSetTableAdapters.BirthListTableAdapter = Nothing, Optional ByRef IsCLPTransactionSuccess As Boolean = False, Optional ByVal posdtUpdateOpenAmountQtyInSales As POSDBDataSet.OpenQtyBirthListSalesDtlDataTable = Nothing, Optional ByVal posadptUpdateOpenAmountQtyInSales As POSDBDataSetTableAdapters.OpenQtyBirthListSalesDtlTableAdapter = Nothing, Optional ByVal redpoints As Decimal = 0.0) As Boolean

        Dim strPrint As String = String.Empty
        OnlineConnect = Online
        Try
            If (dsBirthListSales.Tables.Count <= 0) Then
                If Not (GetDataTableStructure()) Then
                    IsTransactionSuccess = False
                    IsCLPTransactionSuccess = False
                    Return False
                End If
            Else
                dsBirthListSales.Clear()
            End If
            Dim strInvoiceNumber As String
            strInvoiceNumber = GenNewSaleInVoiceNumber(TerminalID, Online)
            Dim strOrderedDocNumber As String

            strOrderedDocNumber = OrderDocumentNumber()
            If Not strOrderedDocNumber = String.Empty Then
                If Not strInvoiceNumber = String.Empty Then
                    dsBirthListSales.Tables("SalesInvoice").Columns("SaleInvNumber").DefaultValue = strInvoiceNumber
                    dsBirthListSales.Tables("BirthListSalesHdr").Columns("SaleInvNumber").DefaultValue = strInvoiceNumber
                    dsBirthListSales.Tables("BirthListSalesDtl").Columns("SaleInvNumber").DefaultValue = strInvoiceNumber
                    dsBirthListSales.Tables("OrderHdr").Columns("DocumentNumber").DefaultValue = strOrderedDocNumber
                    dsBirthListSales.Tables("OrderDtl").Columns("DocumentNumber").DefaultValue = strOrderedDocNumber
                    dsBirthListSales.Tables("OrderDtl").Columns("RefDocumentNo").DefaultValue = strInvoiceNumber
                    dsBirthListSales.Tables("BirthListReturnDtl").Columns("ReturnInvoiceNumber").DefaultValue = strInvoiceNumber
                    If Not _dtBirthListItemDetail Is Nothing AndAlso _dtBirthListItemDetail.Rows.Count > 0 Then
                        strPickUpQty = _dtBirthListItemDetail.Compute("sum(PickUpQty)", " ")
                    End If
                    If (Save_SalesInvoice()) Then
                        If (Save_BirthListSalesHdr(Online)) Then
                            If (Save_BirthListSalesDtl()) Then
                                If (Save_OrderHDR()) Then
                                    If (Save_OrderDTL()) Then
                                    Else
                                        IsTransactionSuccess = False
                                        IsCLPTransactionSuccess = False
                                        Return False
                                    End If
                                Else
                                    IsTransactionSuccess = False
                                    IsCLPTransactionSuccess = False
                                    Return False
                                End If
                            Else
                                IsTransactionSuccess = False
                                IsCLPTransactionSuccess = False
                                Return False
                            End If
                            'Changed by Rohit on 24 March 2011
                            'Else
                            '    strInvoiceNumber = String.Empty
                            '    dsBirthListSales.Tables("OrderDtl").Columns("RefDocumentNo").DefaultValue = DBNull.Value

                            '    ' add even only pick is done in editmode
                            '    If (Save_OrderHDR()) Then
                            '        If (Save_OrderDTL()) Then
                            '        End If
                            '    End If
                        Else
                            IsTransactionSuccess = False
                            IsCLPTransactionSuccess = False
                            Return False
                        End If
                    Else
                        IsTransactionSuccess = False
                        IsCLPTransactionSuccess = False
                        Return False
                    End If


                    If IsUpdateBirthList Then
                        If Not Save_BirthListReturnDtl(Online) Then
                            IsTransactionSuccess = False
                            IsCLPTransactionSuccess = False
                            Return False
                        End If
                    End If

                    If Not (IsUpdateBirthList) Then
                        If Not posDbBirthListRequested Is Nothing Then
                            If Not UpdateBirthListRequestedItem_ADPForSale(posDbBirthListRequested) Then
                                IsTransactionSuccess = False
                                IsCLPTransactionSuccess = False
                                Return False
                            End If
                        Else
                            If Not UpdateBirthListRequestedItem_ADPForSale() Then
                                IsTransactionSuccess = False
                                IsCLPTransactionSuccess = False
                                Return False
                            End If
                        End If

                        'Save_GV()
                        Save_VoucherSales()
                    Else

                        If Not posDbBirthListRequested Is Nothing Then
                            If Not UpdateBirthListRequestedItem_ADP(posDbBirthListRequested) Then
                                IsTransactionSuccess = False
                                IsCLPTransactionSuccess = False
                                Return False
                            End If
                        Else
                            If Not UpdateBirthListRequestedItem_ADP() Then
                                IsTransactionSuccess = False
                                IsCLPTransactionSuccess = False
                                Return False
                            End If
                        End If
                    End If



                    If Not Save_SalesOrderTaxDtls(strInvoiceNumber) Then
                        IsTransactionSuccess = False
                        IsCLPTransactionSuccess = False
                        Return False
                    End If
                    Dim Tran2 As SqlTransaction = Nothing

                    CloseConnection()
                    OpenConnection()
                    Tran2 = SpectrumCon.BeginTransaction()


                    If IIf(dsBirthListSales.Tables("BirthListSalesDtl").Compute("Sum(CLPPoints)", "") Is DBNull.Value, 0, dsBirthListSales.Tables("BirthListSalesDtl").Compute("Sum(CLPPoints)", "")) > 0 Then
                        Dim clscomm As New clsCommon

                        If RelativesPoint Then
                            clscomm.UpdateClpPoints(True, ParentCLPProg, ParentCardNo, dsBirthListSales.Tables("BirthListSalesDtl").Compute("Sum(CLPPoints)", ""), SiteCode, UserName, SaleInVoiceNumber, CreationDate, True, Tran2, dsBirthListSales.Tables("salesinvoice").Rows(0)("Finyear"))
                        Else
                            clscomm.UpdateClpPoints(True, CLPProgrmaID, CustomerID, dsBirthListSales.Tables("BirthListSalesDtl").Compute("Sum(CLPPoints)", ""), SiteCode, UserName, SaleInVoiceNumber, CreationDate, True, Tran2, dsBirthListSales.Tables("salesinvoice").Rows(0)("Finyear"))
                        End If



                    End If



                    If Not _dtPaymentHistory Is Nothing AndAlso _dtPaymentHistory.Rows.Count > 0 Then
                        If Not (SavePaymentData(Tran2, dsBirthListSales.Tables("VoucherDtls"), , redpoints)) Then
                            IsTransactionSuccess = False
                            IsCLPTransactionSuccess = False
                            Return False
                        Else
                            PaymentGV = dsBirthListSales.Tables("VoucherDtls").Copy()
                            dsBirthListSales.Tables("VoucherDtls").Clear()
                        End If
                    End If


                    'If (objclsComman.UpdateDocumentNo("SalesInvoice", SpectrumCon, Tran2)) Then
                    If (objclsComman.UpdateDocumentNo("CM", SpectrumCon, Tran2)) Then
                        If Not dsBirthListSales.Tables("OrderHdr") Is Nothing Then
                            If dsBirthListSales.Tables("OrderHdr").Rows.Count > 0 Then
                                If (objclsComman.UpdateDocumentNo(BirthListOutBoundName, SpectrumCon, Tran2)) Then
                                Else
                                    Tran2.Rollback()
                                    IsTransactionSuccess = False
                                    Return False
                                End If
                            End If
                        End If

                        If (objclsComman.SaveData(dsBirthListSales, SpectrumCon, Tran2)) Then

                            If dsBirthListSales.Tables("Salesinvoice").Rows.Count > 0 AndAlso RoundedAmount <> 0 Then
                                If objclsComman.SaveRoundedDetails(SpectrumCon, Tran2, SiteCode, BirthLisID, strInvoiceNumber, DateDayOpen, FinacialYear, RoundedAmount, "BLS", UserName) = False Then
                                    Tran2.Rollback()
                                    IsTransactionSuccess = False
                                    Return False
                                End If

                            End If
                            'If (objclsComman.fnSaveBLEdit(dsBirthListSales, SpectrumCon, Tran2)) Then
                            If Not _dtGV Is Nothing Then
                                'If (objclsComman.GenerateGiftVoucher(DataTableGV, Tran2, SaleInVoiceNumber, SiteCode)) Then
                                If (UpdateStock(_dtBirthListItemDetail, SpectrumCon, Tran2)) Then
                                    If Not posAdaptorBL Is Nothing Then
                                        posAdaptorBL.Update(posDtBirthList)
                                        'If Not posAdaptorBirthListReq Is Nothing Then
                                        '    posAdaptorBirthListReq.Update(posDbBirthListRequested)
                                        'End If
                                        If Not posdtUpdateOpenAmountQtyInSales Is Nothing Then
                                            If Not posadptUpdateOpenAmountQtyInSales Is Nothing Then
                                                posadptUpdateOpenAmountQtyInSales.Update(posdtUpdateOpenAmountQtyInSales)
                                            End If
                                        End If
                                    End If
                                    Tran2.Commit()
                                    IsTransactionSuccess = True
                                    'If (PrintGo(strPrint, strPickUpQty)) Then
                                    '    IsTransactionSuccess = True
                                    'End If
                                Else
                                    Tran2.Rollback()
                                    IsTransactionSuccess = False
                                    Return False
                                End If
                            Else
                                Dim dtView As DataView = New DataView(_dtBirthListItemDetail, "PurchasedQty >0 or ReservedQty >=0 ", "", DataViewRowState.CurrentRows)
                                Dim dtUpdateStock As DataTable = dtView.ToTable()

                                If (UpdateStockBL(dtUpdateStock, Tran2)) Then
                                    If Not posAdaptorBL Is Nothing Then
                                        posAdaptorBL.Update(posDtBirthList)
                                        'Changed by rohit for Issue No. 6139
                                        'If Not posAdaptorBirthListReq Is Nothing Then
                                        '    posAdaptorBirthListReq.Update(posDbBirthListRequested)
                                        'End If

                                        If Not posdtUpdateOpenAmountQtyInSales Is Nothing Then
                                            If Not posadptUpdateOpenAmountQtyInSales Is Nothing Then
                                                posadptUpdateOpenAmountQtyInSales.Update(posdtUpdateOpenAmountQtyInSales)
                                            End If
                                        End If
                                    End If
                                    Tran2.Commit()
                                    IsTransactionSuccess = True

                                    'If (PrintGo(Online, strPrint, strPickUpQty)) Then
                                    '    IsTransactionSuccess = True
                                    'End If

                                    If (IsCLPCustomer) And IsCLPCalculate = True Then
                                        If (Save_StartCLPTransaction()) Then
                                            IsCLPTransactionSuccess = True
                                        Else
                                            IsCLPTransactionSuccess = False
                                        End If


                                    End If
                                Else
                                    Tran2.Rollback()
                                    IsTransactionSuccess = False
                                    Return False
                                End If
                            End If
                        Else
                            Tran2.Rollback()
                            IsTransactionSuccess = False
                            IsCLPTransactionSuccess = False
                            Return False
                        End If
                    Else
                        Tran2.Rollback()
                        IsTransactionSuccess = False
                        IsCLPTransactionSuccess = False
                        Return False
                    End If
                Else
                    IsTransactionSuccess = False
                    IsCLPTransactionSuccess = False
                    Return False
                End If
            Else
                IsTransactionSuccess = False
                IsCLPTransactionSuccess = False
                Return False
            End If


        Catch ex As Exception
            IsTransactionSuccess = False
            IsCLPTransactionSuccess = False
            Return False
            LogException(ex)
        Finally
            CloseConnection()
        End Try

        Try
            Dim objclscomman As New clsCommon()
            OpenConnection()
            Dim tran1 As SqlTransaction = SpectrumCon.BeginTransaction()
            If (objclscomman.AssignVoucherToBill(SpectrumCon, tran1, SiteCode, BirthLisID, FinacialYear, "BLS", SaleInVoiceNumber)) Then
                tran1.Commit()
            Else
                tran1.Rollback()
                IsTransactionSuccess = False
                IsCLPTransactionSuccess = False
                Return False
            End If

        Catch ex1 As Exception
            LogException(ex1)
            IsTransactionSuccess = False
            IsCLPTransactionSuccess = False
            Return False
        Finally
            CloseConnection()
        End Try

        Return True
    End Function

    Private Function UpdateStockBL(ByVal dtUpdateStock As DataTable, ByVal Tran2 As SqlTransaction) As Boolean
        Try
            If Not dtUpdateStock Is Nothing Then
                If dtUpdateStock.Rows.Count > 0 Then
                    If (UpdateStock(dtUpdateStock, SpectrumCon, Tran2, IsUpdateBirthList)) Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return True
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            Return False

        End Try
    End Function
    Private _dateToday As DateTime
    Private Property DateToday() As DateTime
        Get
            Return _dateToday

        End Get
        Set(ByVal value As DateTime)
            _dateToday = value
        End Set
    End Property

    ''' <summary>
    ''' Start to save  CLP deatails 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function Save_StartCLPTransaction() As Boolean
        Dim Tran2 As SqlTransaction = Nothing
        Try
            _dateToday = CreationDate
            If (IsCLPCustomer = True) Then
                If (Save_CLPTransaction()) Then
                    Save_CLPTransactionsDetails()
                    Save_CLPCustomer()
                    OpenConnection()
                    Tran2 = SpectrumCon.BeginTransaction()
                    If (objclsComman.SaveData(dsCLP, SpectrumCon, Tran2)) Then
                        'If (objclsComman.fnSaveBLEdit(dsCLP, SpectrumCon, Tran2)) Then
                        Tran2.Commit()
                        Return True
                    Else
                        Tran2.Rollback()
                        Return False
                    End If
                Else
                    Return True
                End If

            Else
                Return False
            End If

        Catch ex As Exception
            Tran2.Rollback()
            LogException(ex)
            Return False
        Finally

            CloseConnection()
        End Try
    End Function

#Region "PrintFunctions "

    ''' <summary>
    ''' Printing OBD 
    ''' </summary>
    ''' <param name="strPrint"></param>
    ''' <param name="strPickUpQty"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintGo(ByVal Online As Boolean, ByRef strPrint As String, ByRef strPickUpQty As String) As Boolean
        Try
            If (IsUpdateBirthList) Then
                Dim IsPickUpQty1 As Boolean = False
                strPrint = PrintBirthListSales(IsPickUpQty1)
                strPickUpQty = PrintBirthListPickedQty(Online)
            Else
                Dim IsPickUpQty As Boolean = False
                strPrint = PrintBirthListSales(IsPickUpQty)
                If (IsPickUpQty) Then
                    strPickUpQty = PrintBirthListPickedQty(Online)
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    '''  Print for BirthList Sold items
    ''' </summary>
    ''' <param name="IsPickUpQty"></param>
    ''' <UsedBY>frmBirthListSales.vb</UsedBY>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Private Function PrintBirthListSales(Optional ByRef IsPickUpQty As Boolean = False) As String
        Dim printMsg As New System.Text.StringBuilder
        Try

            Dim strhdrLine, strhdrTitle, strhdrEndLing As String
            Dim sbCompdetails As New System.Text.StringBuilder

            'Dim _dtSiteInfo As New DataTable
            Dim strSiteName As String = ""
            Dim strSiteAddress As String = ""
            '_dtSiteInfo = objclsComman.GetSiteInfo(SiteCode)


            strhdrLine = "----------------------------------------------------------------------------------------------------" & vbCrLf

            strhdrTitle = "                         SALE Aginst BirthList   " & vbCrLf & "Order No:" & OrderDocumentNumber
            strhdrEndLing = "----------------------------------------------------------------------------------------------------" & vbCrLf

            Dim strHeader As New StringBuilder
            Dim strFooter As New StringBuilder
            PrintBirthListHeaderAndFooter(strHeader, strFooter)

            'sbCompdetails.Append("Company Name  : " & "CreativeIT India Ltd" & vbTab & vbTab & vbTab & "Company Code : " & "CRITI02")
            'If _dtSiteInfo.Rows.Count > 0 Then
            '    strSiteName = "Store Name    : " & _dtSiteInfo.Rows(0).Item("SiteOfficialName") & vbTab & vbTab & vbTab & vbTab & "  " & vbTab & " Store Code   : " & SiteCode
            '    strSiteAddress = "Store Address : " & _dtSiteInfo.Rows(0).Item("SiteAddressLn1") & vbCrLf & vbTab & vbTab & _
            '                 _dtSiteInfo.Rows(0).Item("SiteAddressLn2") & " " & _dtSiteInfo.Rows(0).Item("SiteAddressLn3") & _
            '                 _dtSiteInfo.Rows(0).Item("SitePinCode")
            'End If


            Dim strLineSiteEnd As String
            Dim strOrderNo As String
            Dim strDate As String
            Dim strCashierNm As String
            Dim strCustomerName As String
            Dim strCustomerAddress As New System.Text.StringBuilder
            strLineSiteEnd = "----------------------------------------------------------------------------------------------------"
            strOrderNo = "BirthList ID        : " & BirthLisID & vbTab & vbTab & vbTab & " " & vbTab & " Date    : " & Format(Now.Date, "dd-MM-yyyy")
            strDate = "Expected Delivery Date : " & SelectedBirthListInfo("DeliveryDate")
            strCashierNm = "Cashier Name           : " & UserName
            strCustomerName = "Customer Name    : " & SelectedCustomerInfo("CustomerName") & vbTab & vbTab & "  Customer Code : " & CustomerID
            strCustomerAddress.Append("Home Address     : " & SelectedCustomerInfo("Address"))
            'strCustomerAddress.Append(vbTab & vbTab & " : " & SelectedCustomerInfo("AddressLn1") & vbCrLf)

            'Dim dv As New DataView(dtCustmInfo, "AddressType='" & cboAddressType.SelectedValue & "'", "", DataViewRowState.CurrentRows)
            'Dim dtCustmDeliveryInfo As DataTable = dv.ToTable
            'sw.WriteLine("Delivery Address : " & dtCustmDeliveryInfo.Rows(0).Item("AddressLn1").ToString)
            'sw.WriteLine(vbTab & vbTab & " : " & dtCustmDeliveryInfo.Rows(0).Item("AddressLn2").ToString & vbCrLf)
            Dim strCustomerphonNo As String
            strCustomerphonNo = "Tel. No.  	: " & vbTab & SelectedCustomerInfo("OfficeNO")
            Dim strCustomerLinEnd As String
            Dim strHdrItem As String
            Dim strHdrItemInfo As New System.Text.StringBuilder
            strCustomerLinEnd = "----------------------------------------------------------------------------------------------------"
            strHdrItem = "Item Code                  Item Desc                     Qty    Rate       NetAmt"
            Dim strArticleCode As String
            Dim strArticleDesc As String
            Dim iPurchasedQty As Integer
            Dim iPrice As String
            Dim iNetAmount As String
            For Each drDtl As DataRow In _dtBirthListItemDetail.Rows
                If Not (drDtl.RowState = DataRowState.Deleted) Then
                    If (drDtl("purchasedqty") > 0) Then
                        strArticleCode = drDtl("EAN")
                        strArticleDesc = drDtl("Discription") & Space(30 - drDtl("Discription").ToString.Length)
                        iPurchasedQty = drDtl("purchasedqty") & Space(7 - 4)
                        iPrice = drDtl("SellingPrice")
                        If Not (IsUpdateBirthList) Then
                            iNetAmount = FormatNumber(drDtl("NetAmount").ToString(), 2).ToString
                        Else
                            iNetAmount = FormatNumber(drDtl("CurrentPurchasedAmount").ToString(), 2).ToString
                        End If

                        strHdrItemInfo.Append(strArticleCode & Space(7) & strArticleDesc + iPurchasedQty.ToString() & Space(10) & iPrice & Space(10) & iNetAmount + vbCrLf)
                        If (drDtl("PickUpQty") > 0) Then
                            IsPickUpQty = True
                        End If
                    End If
                End If
            Next

            If Not _dtVoucherSales Is Nothing Then
                For Each drGV As DataRow In _dtVoucherSales.Rows
                    strArticleCode = drGV("EAN")
                    strArticleDesc = drGV("Discription") & Space(30 - drGV("Discription").ToString.Length)
                    iPurchasedQty = drGV("BookedQty") & Space(7 - 4)

                    iNetAmount = FormatNumber(drGV("NetAmount").ToString(), 2).ToString
                    iPrice = drGV("SellingPrice")
                    strHdrItemInfo.Append(strArticleCode & Space(7) & strArticleDesc + iPurchasedQty.ToString() & Space(10) & iPrice & Space(10) & iNetAmount + vbCrLf)

                Next
            End If
            If (strHdrItemInfo.ToString() = String.Empty) Then
                Return String.Empty
            End If
            Dim strItemDetailLineEnd As String
            strItemDetailLineEnd = "----------------------------------------------------------------------------------------------------" & vbCrLf

            Dim iTotalBirthListItem, decNetAmt As Decimal

            Dim strTotalQty, strNetAmt, strTermsCond, strLineEnd As String
            If Not _dtBirthListItemDetail Is Nothing Then
                If (_dtBirthListItemDetail.Rows.Count > 0) Then

                    iTotalBirthListItem = _dtBirthListItemDetail.Compute("Sum(purchasedqty)", "")
                    strTotalQty = "Total Qty : " & _dtBirthListItemDetail.Compute("Sum(purchasedqty)", " ")
                    Try
                        If Not (IsUpdateBirthList) Then
                            decNetAmt = _dtBirthListItemDetail.Compute("Sum(NetAmount)", "")
                            strNetAmt = "Net   Amt : " & FormatNumber(_dtBirthListItemDetail.Compute("Sum(NetAmount)", ""), 2)
                        Else
                            decNetAmt = _dtBirthListItemDetail.Compute("Sum(NetAmount)", "")
                            strNetAmt = "Net   Amt : " & FormatNumber(_dtBirthListItemDetail.Compute("Sum(CurrentPurchasedAmount)", ""), 2)
                        End If
                    Catch ex As Exception

                    End Try
                End If
            End If

            If Not _dtVoucherSales Is Nothing Then
                If _dtVoucherSales.Rows.Count > 0 Then
                    iTotalBirthListItem = Decimal.Add(iTotalBirthListItem, _dtVoucherSales.Compute("Sum(BookedQty)", ""))
                    strTotalQty = "Total Qty : " & iTotalBirthListItem
                    decNetAmt = decNetAmt + _dtVoucherSales.Compute("Sum(NetAmount)", " ")
                    strNetAmt = "Net   Amt : " & decNetAmt

                End If
            End If
            strTermsCond = "<Terms & Condition>" & vbCrLf & vbCrLf
            strLineEnd = "Authorized Sign:............................." & vbTab & "Customer Sign:..............................."
            printMsg.Append(strhdrLine + vbCrLf)
            printMsg.Append(strhdrTitle + vbCrLf)

            printMsg.Append(strhdrEndLing + vbCrLf)
            If IsPrintHeader Then
                printMsg.Append(strHeader.Append(vbCrLf))
            End If

            printMsg.Append(sbCompdetails.Append(vbCrLf))
            printMsg.Append(strSiteName + vbCrLf)
            printMsg.Append(strSiteAddress + vbCrLf)
            printMsg.Append(strLineSiteEnd + vbCrLf)
            printMsg.Append(strOrderNo + vbCrLf)
            printMsg.Append(strDate + vbCrLf)
            printMsg.Append(strCashierNm + vbCrLf)
            printMsg.Append(strCustomerName + vbCrLf)
            printMsg.Append(strCustomerAddress.Append(vbCrLf))
            printMsg.Append(strCustomerphonNo + vbCrLf)
            printMsg.Append(strCustomerLinEnd + vbCrLf)
            printMsg.Append(strHdrItem + vbCrLf)
            printMsg.Append(strItemDetailLineEnd + vbCrLf)
            printMsg.Append(strHdrItemInfo.Append(vbCrLf))
            printMsg.Append(strItemDetailLineEnd + vbCrLf)
            printMsg.Append(strTotalQty + vbCrLf)
            printMsg.Append(strNetAmt + vbCrLf)
            printMsg.Append(strTermsCond + vbCrLf)
            printMsg.Append(strLineEnd + vbCrLf)
            If IsPrintFooter Then
                printMsg.Append(strFooter.Append(vbCrLf))
            End If


        Catch ex As Exception

        End Try
        Return printMsg.ToString()
    End Function

    ''' <summary>
    ''' Print For delivery Items
    ''' </summary>
    ''' <returns>String</returns>
    ''' <UsedBY>frmBirthListSales.vb,frmBirthListUpdation.vb</UsedBY>
    ''' <remarks>Directly make a call on frmBirthListUpdation.vb</remarks>
    ''' 


    Public Function PrintBirthListPickedQty(ByVal Online As Boolean, Optional ByVal IsReprint As Boolean = False, Optional ByVal IsSalesPrint As Boolean = False) As String
        Dim printMsg As New System.Text.StringBuilder
        Try
            Dim strhdrLine, strhdrTitle, strhdrEndLing As String
            Dim sbCompdetails As New System.Text.StringBuilder
            Dim _dtSiteInfo As New DataTable
            Dim strSiteName As String = ""
            Dim strSiteAddress As String = ""
            _dtSiteInfo = objclsComman.GetSiteInfo(SiteCode)
            strhdrLine = "----------------------------------------------------------------------------------------------------" & vbCrLf
            If (IsReprint) Then
                strhdrTitle = "                          BirthList   "
            Else
                strhdrTitle = "                          BirthList Item  Delivery  " & vbCrLf & "Invoice No:" & GenNewSaleInVoiceNumber(TerminalID, Online)

            End If
            strhdrEndLing = "----------------------------------------------------------------------------------------------------"
            'sbCompdetails.Append("Company Name  : " & "CreativeIT India Ltd" & vbTab & vbTab & vbTab & "Company Code : " & "CRITI02")
            'If _dtSiteInfo.Rows.Count > 0 Then
            '    strSiteName = "Store Name    : " & _dtSiteInfo.Rows(0).Item("SiteOfficialName") & vbTab & vbTab & vbTab & vbTab & "  Store Code   : " & SiteCode
            '    strSiteAddress = "Store Address : " & _dtSiteInfo.Rows(0).Item("SiteAddressLn1") & vbCrLf & vbTab & vbTab & _
            '                 _dtSiteInfo.Rows(0).Item("SiteAddressLn2") & " " & _dtSiteInfo.Rows(0).Item("SiteAddressLn3") & _
            '                 _dtSiteInfo.Rows(0).Item("SitePinCode")
            'End If


            Dim strHeader As New StringBuilder
            Dim strFooter As New StringBuilder
            PrintBirthListHeaderAndFooter(strHeader, strFooter)

            Dim strLineSiteEnd As String
            Dim strOrderNo As String
            Dim strDate As String
            Dim strCashierNm As String
            Dim strCustomerName As String
            Dim strCustomerAddress As New System.Text.StringBuilder
            strLineSiteEnd = "----------------------------------------------------------------------------------------------------"
            strOrderNo = "BirthList ID         : " & BirthLisID & vbTab & vbTab & vbTab & " " & vbTab & "  Date : " & Format(Now.Date, "dd-MM-yyyy")
            'strDate = "Expected Delivery Date : " & Format(objclsComman.GetCurrentDate(), "dd-MM-yyyy")
            strCashierNm = "Cashier Name           : " & UserName
            If Not (IsUpdateBirthList) Then
                strCustomerName = "Customer Name    : " & SelectedCustomerInfo("CustomerName") & vbTab & vbTab & "  Customer Code : " & CustomerID
            Else
                strCustomerName = "Customer Name    : " & SelectedBirthListInfo("CustomerName") & vbTab & vbTab & "  Customer Code : " & CustomerID

            End If
            Dim strCustomerphonNo As String
            strCustomerAddress.Append("Home Address     : " & SelectedCustomerInfo("Address") & "")
            strCustomerphonNo = "Tel. No.  	: " & vbTab & SelectedCustomerInfo("OfficeNo")

            'strCustomerAddress.Append("Home Address     : " & SelectedCustomerInfo("AddressLn1") & "," & SelectedCustomerInfo("AddressLn2") & "," & SelectedCustomerInfo("AddressLn3") & "," & SelectedCustomerInfo("AddressLn4"))
            'strCustomerphonNo = "Tel. No.  	: " & vbTab & SelectedCustomerInfo("OfficePhone")

            'strCustomerAddress.Append(vbTab & vbTab & " : " & SelectedCustomerInfo("AddressLn1") & vbCrLf)
            'Dim dv As New DataView(dtCustmInfo, "AddressType='" & cboAddressType.SelectedValue & "'", "", DataViewRowState.CurrentRows)
            'Dim dtCustmDeliveryInfo As DataTable = dv.ToTable
            'sw.WriteLine("Delivery Address : " & dtCustmDeliveryInfo.Rows(0).Item("AddressLn1").ToString)
            'sw.WriteLine(vbTab & vbTab & " : " & dtCustmDeliveryInfo.Rows(0).Item("AddressLn2").ToString & vbCrLf)


            Dim strCustomerLinEnd As String
            Dim strHdrItem As String
            Dim strHdrItemInfo As New System.Text.StringBuilder
            strCustomerLinEnd = "----------------------------------------------------------------------------------------------------"
            strHdrItem = "Item Code                Item Desc                        Qty         Rate            NetAmt"
            Dim strArticleCode As String
            Dim strArticleDesc As String
            Dim iPurchasedQty As Integer
            Dim iPrice As String
            Dim iNetAmount As String
            Dim iCalNetAmount As Decimal
            Dim iCalTotolNetAmt As Decimal = Decimal.Zero

            If IsReprint = True Then
                For Each drDtl As DataRow In _dtBirthListItemDetail.Rows
                    If Not drDtl.RowState = DataRowState.Deleted Then
                        strArticleCode = drDtl("EAN")
                        strArticleDesc = drDtl("Discription") & Space(30 - drDtl("Discription").ToString.Length)

                        If (IsSalesPrint) Then
                            iPurchasedQty = drDtl("BookedQty") & Space(7 - 4)
                        Else
                            iPurchasedQty = drDtl("RequstedQty") & Space(7 - 4)
                        End If
                        If Not drDtl("SellingPrice") Is DBNull.Value Then
                            iPrice = drDtl("SellingPrice")
                        Else
                            If (IsSalesPrint) Then
                                iPrice = drDtl("NetAmt")
                            End If
                        End If
                        If (IsSalesPrint) Then
                            iCalNetAmount = drDtl("BookedQty") * iPrice
                        Else
                            iCalNetAmount = drDtl("RequstedQty") * iPrice
                        End If
                        iNetAmount = String.Format(iCalNetAmount, "O.OO")
                        iCalTotolNetAmt = iCalTotolNetAmt + iNetAmount
                        strHdrItemInfo.Append(strArticleCode & Space(7) & strArticleDesc & Space(10) & iPurchasedQty.ToString() & Space(10) & iPrice & Space(10) & iNetAmount + vbCrLf)
                    End If
                Next
            ElseIf Not IsUpdateBirthList Then
                For Each drDtl As DataRow In _dtBirthListItemDetail.Rows
                    If Not drDtl.RowState = DataRowState.Deleted Then
                        If (drDtl("PickUpQty") > 0) Then
                            strArticleCode = drDtl("EAN")
                            strArticleDesc = drDtl("Discription") & Space(30 - drDtl("Discription").ToString.Length)
                            iPurchasedQty = drDtl("PickUpQty") & Space(7 - 4)
                            iPrice = drDtl("SellingPrice")
                            iCalNetAmount = drDtl("PickUpQty") * drDtl("SellingPrice")
                            iNetAmount = String.Format(iCalNetAmount, "O.OO")
                            iCalTotolNetAmt = iCalTotolNetAmt + iNetAmount
                            strHdrItemInfo.Append(strArticleCode & Space(7) & strArticleDesc + iPurchasedQty.ToString() & Space(10) & iPrice & Space(10) & iNetAmount + vbCrLf)
                        End If
                    End If
                Next
            ElseIf IsUpdateBirthList = True Then
                For Each drDtl As DataRow In _dtBirthListItemDetail.Rows
                    If Not drDtl.RowState = DataRowState.Deleted Then
                        If (drDtl("PickUpQty") > 0) Then
                            strArticleCode = drDtl("EAN")
                            strArticleDesc = drDtl("Discription") & Space(30 - drDtl("Discription").ToString.Length)
                            iPurchasedQty = drDtl("PickUpQty") & Space(7 - 4)
                            iPrice = drDtl("SellingPrice")
                            iCalNetAmount = drDtl("PickUpQty") * drDtl("SellingPrice")
                            iNetAmount = String.Format(iCalNetAmount, "O.OO")
                            iCalTotolNetAmt = iCalTotolNetAmt + iNetAmount
                            strHdrItemInfo.Append(strArticleCode & Space(7) & strArticleDesc + iPurchasedQty.ToString() & Space(10) & iPrice & Space(10) & iNetAmount + vbCrLf)
                        End If
                        If (drDtl("CurrentReturnQty") > 0) Then
                            strArticleCode = drDtl("EAN")
                            strArticleDesc = drDtl("Discription") & Space(30 - drDtl("Discription").ToString.Length)
                            iPurchasedQty = drDtl("CurrentReturnQty") & Space(7 - 4)
                            iPrice = drDtl("SellingPrice")
                            iCalNetAmount = drDtl("CurrentReturnQty") * drDtl("SellingPrice")
                            iNetAmount = String.Format(iCalNetAmount, "O.OO")
                            iCalTotolNetAmt = iCalTotolNetAmt + iNetAmount
                            strHdrItemInfo.Append(strArticleCode & Space(7) & strArticleDesc + iPurchasedQty.ToString() & Space(10) & iPrice & Space(10) & iNetAmount + vbCrLf)

                        End If
                    End If
                Next
            End If


            If (strHdrItemInfo.Length = 0) Then
                Return String.Empty
            End If
            Dim strItemDetailLineEnd As String
            strItemDetailLineEnd = "----------------------------------------------------------------------------------------------------" & vbCrLf
            Dim strTotalQty, strNetAmt, strTermsCond, strLineEnd As String
            If IsReprint = True Then
                If (IsSalesPrint) Then
                    strTotalQty = "Total Qty : " & _dtBirthListItemDetail.Compute("Sum(BookedQty)", " ")
                    strNetAmt = "Net   Amt : " & FormatNumber(_dtBirthListItemDetail.Compute("Sum(NetAmt)", " "), 2)
                Else
                    strTotalQty = "Total Qty : " & _dtBirthListItemDetail.Compute("Sum(RequstedQty)", " ")
                    strNetAmt = "Net   Amt : " & FormatNumber(_dtBirthListItemDetail.Compute("Sum(NetAmount)", " "), 2)
                End If

            Else
                strTotalQty = "Total Qty : " & _dtBirthListItemDetail.Compute("Sum(PickUpQty)", " ")
                'strNetAmt = "Net   Amt : " & String.Format(iCalTotolNetAmt, "0.00")
                strNetAmt = "Net   Amt : " & FormatNumber(iCalTotolNetAmt, 2)

            End If

            strTermsCond = "<Terms & Condition>" & vbCrLf & vbCrLf
            strLineEnd = "Authorized Sign:............................." & vbTab & "Customer Sign:..............................."



            printMsg.Append(strhdrLine + vbCrLf)

            printMsg.Append(strhdrTitle + vbCrLf)
            printMsg.Append(strhdrEndLing + vbCrLf)
            If IsPrintHeader Then
                printMsg.Append(strHeader.AppendLine(vbCrLf))
            End If
            printMsg.Append(sbCompdetails.Append(vbCrLf))
            printMsg.Append(strSiteName + vbCrLf)
            printMsg.Append(strSiteAddress + vbCrLf)
            printMsg.Append(strLineSiteEnd + vbCrLf)
            printMsg.Append(strOrderNo + vbCrLf)
            'printMsg.Append(strDate + vbCrLf)
            printMsg.Append(strCashierNm + vbCrLf)
            printMsg.Append(strCustomerName + vbCrLf)
            printMsg.Append(strCustomerAddress.Append(vbCrLf))
            printMsg.Append(strCustomerphonNo + vbCrLf)
            printMsg.Append(strCustomerLinEnd + vbCrLf)
            printMsg.Append(strHdrItem + vbCrLf)
            printMsg.Append(strItemDetailLineEnd + vbCrLf)
            printMsg.Append(strHdrItemInfo.Append(vbCrLf))
            printMsg.Append(strItemDetailLineEnd + vbCrLf)
            printMsg.Append(strTotalQty + vbCrLf)
            printMsg.Append(strNetAmt + vbCrLf)
            printMsg.Append(strTermsCond + vbCrLf)
            printMsg.Append(strLineEnd + vbCrLf)
            If IsPrintFooter Then
                printMsg.Append(strFooter.AppendLine(vbCrLf))
            End If
        Catch ex As Exception

        End Try
        Return printMsg.ToString()
    End Function



    ''' <summary>
    '''  Print setting for birthlist for all birthlist printing 
    ''' </summary>
    ''' <UsedBy>frmBirthListCreate.vb,frmBirthListSale.vb,frmBirthListUpdate.vb</UsedBy>
    ''' <param name="StrSubHeader"></param>
    ''' <param name="StrFooter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function PrintBirthListHeaderAndFooter(ByRef StrSubHeader As StringBuilder, ByRef StrFooter As StringBuilder) As Boolean
        Dim dtPrint As DataTable
        Dim objClsComman As New clsCommon
        dtPrint = objClsComman.GetPrintingDetails("BLS")
        Dim StrPrintHeaderLine As String

        Dim StrPrintFooterLine As String

        If Not dtPrint Is Nothing Then
            Dim filter As String = ""
            Dim dv As New DataView(dtPrint, filter, "Sequenceno", DataViewRowState.CurrentRows)
            If IsPrintHeader = True Then
                filter = "TOPBOTTOM=1"
                dv.RowFilter = filter
                Dim fon As New Font("Verdana", 3)
                For Each drview As DataRowView In dv
                    StrPrintHeaderLine = drview("ReceiptText").ToString()

                    If Not drview("Align") Is Nothing AndAlso drview("Align").ToString() <> String.Empty Then
                        'StrPrintHeaderLine
                    End If
                    StrSubHeader = StrSubHeader.AppendLine(StrPrintHeaderLine & vbNewLine)
                Next
            End If
            If IsPrintFooter = True Then
                filter = "TOPBOTTOM=0"
                dv.RowFilter = filter
                For Each drview As DataRowView In dv
                    StrPrintFooterLine = drview("ReceiptText").ToString()
                    If Not drview("Align") Is Nothing AndAlso drview("Align").ToString() <> String.Empty Then
                        'StrPrintHeaderLine
                    End If
                    StrFooter = StrFooter.AppendLine(StrPrintFooterLine & vbNewLine)
                Next
            End If
        End If
    End Function
    ''' <summary>
    ''' Update stock for sold items
    ''' </summary>
    ''' <param name="dtPurchasedItems"></param>
    ''' <param name="con"></param>
    ''' <param name="tran"></param>
    ''' ''' <param name="tran"></param>
    ''' <returns>IsBirthListCreate : true for onluBirthListCreate and BirthListUpdate </returns>
    ''' <remarks></remarks>

#End Region

    Public Function UpdateStock(ByVal dtPurchasedItems As DataTable, ByRef con As SqlConnection, ByRef tran As SqlTransaction, Optional ByVal IsBirthListCreate As Boolean = False) As Boolean
        Try
            Dim objclsComman As New clsCommon
            Dim IsTransactionSuccess As Boolean = True
            Dim strArticleCode As String
            Dim strEANCode As String
            Dim iPurchaseQty As Integer
            Dim strUOM As String
            Dim iReservedQty As Integer
            If IsBirthListCreate = False Or IsUpdateBirthList = True Then
                Dim iTotalPickUpQty As Object = dtPurchasedItems.Compute("sum(PickUpQty)", " ")
                If Not iTotalPickUpQty Is Nothing Then
                    If (iTotalPickUpQty > 0) Then
                        For Each drPurchaseItem As DataRow In dtPurchasedItems.Rows
                            iPurchaseQty = drPurchaseItem("PickUpQty")
                            iReservedQty = drPurchaseItem("ReservedQty")

                            If (iPurchaseQty > 0) Then
                                strArticleCode = drPurchaseItem("ArticleCode")
                                strEANCode = drPurchaseItem("EAN")
                                If Not drPurchaseItem("UNITOFMEASURE") Is DBNull.Value Then
                                    strUOM = drPurchaseItem("UNITOFMEASURE")
                                End If

                                If iReservedQty > 0 Then
                                    iReservedQty = iPurchaseQty * -1
                                    If Not objclsComman.UpdateStock(SiteCode, strArticleCode, strEANCode, strUOM, iPurchaseQty, UserName, con, tran, StockStorageLocation, iReservedQty) Then
                                        IsTransactionSuccess = False
                                        Exit For
                                    End If
                                Else
                                    If Not objclsComman.UpdateStock(SiteCode, strArticleCode, strEANCode, strUOM, iPurchaseQty, UserName, con, tran, StockStorageLocation) Then
                                        IsTransactionSuccess = False
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                    End If
                End If

                If IsUpdateBirthList Then
                    Dim objTotalReturnQty As Object = dtPurchasedItems.Compute("sum(CurrentReturnQty)", " ")

                    If Not objTotalReturnQty Is Nothing And Not objTotalReturnQty Is DBNull.Value Then
                        If (objTotalReturnQty > 0) Then
                            Return CheckReturnQtyUpdate(dtPurchasedItems, con, tran)
                        End If
                    End If
                End If


            End If
            If IsBirthListCreate = True Then
                Dim objTotalReservedQty As Object = dtPurchasedItems.Compute("sum(ReservedQty)", " ")
                Dim objTotalLastReserved As Object = dtPurchasedItems.Compute("sum(OriginalReservedQty)", " ")
                If Not objTotalReservedQty Is Nothing Then
                    If Not objTotalLastReserved Is Nothing Then
                        If (IIf(objTotalReservedQty Is DBNull.Value, 0, objTotalReservedQty) > 0 Or IIf(objTotalLastReserved Is DBNull.Value, 0, objTotalLastReserved) > 0) Then
                            Return CheckReserveQtyUpdate(dtPurchasedItems, con, tran)
                        End If
                    End If
                End If
            End If

            Return IsTransactionSuccess
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function


    Private Function CheckReserveQtyUpdate(ByVal dtPurchasedItems As DataTable, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Boolean
        Try
            Dim decReservedQty As Decimal
            Dim objLastReservedQTy As Decimal
            Dim FinalReservedQty As Decimal
            Dim strArticleCode As String
            Dim strEANCode As String
            Dim strUOM As String
            Dim iPurchaseQty As Integer = 0
            Dim iTotalPickUpQty As Object = Nothing
            If dtPurchasedItems.Columns.Contains("PickUpQty") Then
                iTotalPickUpQty = dtPurchasedItems.Compute("sum(PickUpQty)", " ")
            End If
            For Each drPurchaseItem As DataRow In dtPurchasedItems.Rows
                Dim strUOP As String = ReturnUOMName(dtPurchasedItems)
                decReservedQty = IIf(drPurchaseItem("ReservedQty") Is DBNull.Value, 0, drPurchaseItem("ReservedQty"))
                objLastReservedQTy = IIf(drPurchaseItem("OriginalReservedQty") Is DBNull.Value, 0, drPurchaseItem("OriginalReservedQty"))
                If (decReservedQty >= 0 Or objLastReservedQTy >= 0) Then
                    strArticleCode = drPurchaseItem("ArticleCode")
                    strEANCode = drPurchaseItem("EAN")
                    If (IsUpdateBirthList) Then
                        strUOM = drPurchaseItem("UNITOFMEASURE")
                    Else
                        strUOM = drPurchaseItem("UOM")
                    End If
                    If Not iTotalPickUpQty Is Nothing Then
                        iPurchaseQty = drPurchaseItem("PickUpQty")
                    End If
                    If decReservedQty <> objLastReservedQTy Then
                        FinalReservedQty = Decimal.Subtract(decReservedQty, objLastReservedQTy)
                        If Not objclsComman.UpdateStock(SiteCode, strArticleCode, strEANCode, strUOM, iPurchaseQty, UserName, con, tran, StockStorageLocation, FinalReservedQty) Then
                            Return False
                            Exit For
                        End If

                    End If


                End If
            Next
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Function CheckReturnQtyUpdate(ByVal dtPurchasedItems As DataTable, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Boolean
        Try
            Dim decReturnQty As Decimal
            Dim objLastReservedQTy As Decimal
            Dim FinalReservedQty As Decimal
            Dim strArticleCode As String
            Dim strEANCode As String
            Dim strUOM As String
            Dim iReturnQty As Integer = 0
            Dim iTotalReturnQty As Object = Nothing
            If dtPurchasedItems.Columns.Contains("CurrentReturnQty") Then
                iTotalReturnQty = dtPurchasedItems.Compute("sum(CurrentReturnQty)", " ")
            End If
            For Each drPurchaseItem As DataRow In dtPurchasedItems.Rows
                Dim strUOP As String = ReturnUOMName(dtPurchasedItems)
                decReturnQty = drPurchaseItem("CurrentReturnQty")
                'objLastReservedQTy = drPurchaseItem("CurrentReturnQty")
                If (decReturnQty >= 0) Then
                    strArticleCode = drPurchaseItem("ArticleCode")
                    strEANCode = drPurchaseItem("EAN")
                    If (IsUpdateBirthList) Then
                        strUOM = drPurchaseItem("UNITOFMEASURE")
                    Else
                        strUOM = drPurchaseItem("UOM")
                    End If
                    If Not iTotalReturnQty Is Nothing Then
                        iReturnQty = drPurchaseItem("CurrentReturnQty")
                        iReturnQty = iReturnQty * -1
                    End If
                    If decReturnQty <> objLastReservedQTy Then
                        FinalReservedQty = decReturnQty
                        If Not objclsComman.UpdateStock(SiteCode, strArticleCode, strEANCode, strUOM, iReturnQty, UserName, con, tran, StockStorageLocation, FinalReservedQty) Then
                            Return False
                            Exit For
                        End If
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function


    Private Function ReturnUOMName(ByVal dt As DataTable) As String
        Try
            Dim strUOMName As String = ""
            If dt.Columns.Contains("PickUpQty") Then
                Return strUOMName = "UNITOFMEASURE"
            Else
                Return strUOMName = "UOM"
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Used for generate id's
    ''' </summary>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Private Function RandomNumber_OrderNumber() As String
        Dim strSalesInvoiceNumber As String = objclsComman.getDocumentNo(BirthListOutBoundName, SiteCode)

        If OnlineConnect = True Then
            'Changed by Rohit to generate Document No. for proper sorting
            Try
                strSalesInvoiceNumber = objclsComman.GenDocNo("OB" & TerminalID & FinacialYear.Substring(FinacialYear.Length - 2, 2), 15, strSalesInvoiceNumber)
            Catch ex As Exception
                strSalesInvoiceNumber = "OB" & TerminalID & strSalesInvoiceNumber
            End Try
            'End Change by Rohit
        Else
            'Changed by Rohit to generate Document No. for proper sorting
            Try
                strSalesInvoiceNumber = objclsComman.GenDocNo("OOB" & TerminalID & FinacialYear.Substring(FinacialYear.Length - 2, 2), 15, strSalesInvoiceNumber)
            Catch ex As Exception
                strSalesInvoiceNumber = "OOB" & TerminalID & strSalesInvoiceNumber
            End Try
            'End Change by Rohit
        End If


        Return strSalesInvoiceNumber
    End Function
    Private Function RandomNumber_SalesInVoiceNumber(ByVal TerminalId As String, ByVal IsOnlineConnect As Boolean) As String
        'Dim strSalesInvoiceNumber As String = objclsComman.getDocumentNo("SalesInvoice")
        'chngd by ram to get invoice number in sequence of cashmemo number
        Dim strSalesInvoiceNumber As String = objclsComman.getDocumentNo("CM", SiteCode)
        If OnlineConnect = True Then
            'Changed by Rohit to generate Document No. for proper sorting
            Try
                strSalesInvoiceNumber = objclsComman.GenDocNo("CM" & TerminalId & FinacialYear.Substring(FinacialYear.Length - 2, 2), 15, strSalesInvoiceNumber)
            Catch ex As Exception
                strSalesInvoiceNumber = "CM" & TerminalId & strSalesInvoiceNumber
            End Try
            'End Change by Rohit
        Else
            'Changed by Rohit to generate Document No. for proper sorting
            Try
                strSalesInvoiceNumber = objclsComman.GenDocNo("OCM" & TerminalId & FinacialYear.Substring(FinacialYear.Length - 2, 2), 15, strSalesInvoiceNumber)
            Catch ex As Exception
                strSalesInvoiceNumber = "OCM" & TerminalId & strSalesInvoiceNumber
            End Try
            'End Change by Rohit
        End If
        Return strSalesInvoiceNumber
    End Function

    Private _CVProgramId As String
    Public Property CVProgramID() As String
        Get
            Return _CVProgramId
        End Get
        Set(ByVal value As String)
            _CVProgramId = value
        End Set
    End Property

    Private _CVVaildDays As Integer = 30
    Public Property CreditVoucherVaildDays() As Integer
        Get
            Return _CVVaildDays
        End Get
        Set(ByVal value As Integer)
            _CVVaildDays = value
        End Set
    End Property
    ''' <summary>
    ''' Only returns new issued giftvoucher information 
    ''' </summary>
    ''' <remarks></remarks>
    Private _PaymentGV As DataTable
    Public Property PaymentGV() As DataTable
        Get
            Return _PaymentGV
        End Get
        Private Set(ByVal value As DataTable)
            _PaymentGV = value
        End Set
    End Property


    Private Function SavePaymentData(ByRef tran As SqlTransaction, Optional ByRef dtGV As DataTable = Nothing, Optional ByVal GVProgramId As String = "", Optional ByVal redepoints As Decimal = 0.0) As Boolean
        Try
            Dim objclsbirthlistGloaable As New clsBirthListGobal
            Dim DocType As String = "BLS"
            If Not dtGV.Columns.Contains("ISPREPRINTED") Then
                Dim dcColumn As New DataColumn("ISPREPRINTED")
                Dim dcQuantity As New DataColumn("Quantity")
                dtGV.Columns.Add(dcColumn)
                dtGV.Columns.Add(dcQuantity)
            End If

            Dim dvCreditVoucher As New DataView(_dtPaymentHistory, "RecieptTypeCode Like 'CreditVouc%'", "", DataViewRowState.CurrentRows)


            Dim iCVNumber As Integer = 0

            If dvCreditVoucher.Count > 0 Then
                Dim strVoucherNo As String = ""
                For Each drView As DataRowView In dvCreditVoucher
                    If drView("RecieptTypeCode") = "CreditVouc(I)" Then


                        Dim intOrginalDays As Int16 = CreditVoucherVaildDays
                        'this for adding or set partial GV/CV expirydate
                        If Not IsDBNull(drView("Date")) Then
                            CreditVoucherVaildDays = DateDiff(DateInterval.Day, CreationDate.Date, CDate(drView("Date")).Date)

                        End If
                        'this for adding or set partial GV/CV expirydate

                        If UpdateCreditVoucher(CVProgramID, DocType, True, BirthLisID, SiteCode, CreationDate, UserName, tran, SpectrumCon, drView("Amount"), strVoucherNo, CreditVoucherVaildDays, "CV", iCVNumber, drView("IssuedForCLP")) = False Then
                            tran.Rollback()
                            CloseConnection()
                        End If
                        CreditVoucherVaildDays = intOrginalDays
                        drView("Number") = strVoucherNo
                        'drView("Date") = CreationDate.AddDays(CreditVoucherVaildDays)
                    ElseIf drView("RecieptTypeCode") = "CreditVouc(R)" Then
                        If UpdateCreditVoucher(CVProgramID, DocType, False, BirthLisID, SiteCode, CreationDate, UserName, tran, SpectrumCon, 0, drView("Number").ToString()) = False Then
                            tran.Rollback()
                            CloseConnection()
                        End If
                    End If
                Next
            End If
            dvCreditVoucher.RowFilter = "RecieptTypeCode Like 'GiftVouc%'"
            If dvCreditVoucher.Count > 0 Then
                For Each GVRow As DataRowView In dvCreditVoucher
                    If GVRow("RecieptTypeCode") = "GiftVoucher(I)" Then
                        Dim drGV As DataRow = dtGV.NewRow()
                        drGV("SiteCode") = SiteCode
                        drGV("VoucherCode") = GVRow("Number").ToString()
                        drGV("ValueOfVoucher") = CDbl(IIf(GVRow("Amount") < 0, GVRow("Amount") * -1, GVRow("Amount").ToString()))
                        drGV("IsActive") = 1
                        drGV("IsIssued") = 1
                        drGV("IssuedAtSite") = SiteCode
                        drGV("IssuedOnDate") = CreationDate
                        drGV("IssuedInDocType") = DocType
                        drGV("ISPREPRINTED") = "False"
                        drGV("Quantity") = 1
                        'drGV("NetAmount") = CDbl(IIf(GVRow("Amount") < 0, GVRow("Amount") * -1, GVRow("Amount").ToString()))
                        If IsDBNull(GVRow("Date")) Then
                            drGV("ExpiryDate") = objclsbirthlistGloaable.CalculateExpiryDate(GVRow("Date"), CreditVoucherVaildDays)
                        Else
                            drGV("ExpiryDate") = GVRow("Date")
                        End If

                        ' Issue GV against partial redeemation should have expirydate same as orignal GV
                        'If dvCreditVoucher.Count > 1 Then
                        '    Dim dvRedimGV As New DataView(_dtPaymentHistory, "RecieptTypeCode = 'GiftVoucher(R)'", "", DataViewRowState.CurrentRows)
                        '    If dvRedimGV.Count > 0 Then
                        '        If Not IsDBNull(dvRedimGV(0).Item("Date")) Then
                        '            drGV("ExpiryDate") = dvRedimGV(0).Item("Date")
                        '        End If
                        '    End If
                        'End If
                        ' Issue GV against partial redeemation should have expirydate same as orignal GV

                        dtGV.Rows.Add(drGV)

                    ElseIf GVRow("RecieptTypeCode") = "GiftVoucher(R)" Then
                        If UpdateCreditVoucher(GVProgramId, DocType, False, BirthLisID, SiteCode, CreationDate, UserName, tran, SpectrumCon, 0, GVRow("Number").ToString()) = False Then
                            tran.Rollback()
                            CloseConnection()
                        End If
                    End If
                Next
            End If
            Dim CLPRedemptionPoints As Decimal
            Dim objclscomman As New clsCommon
            dvCreditVoucher.RowFilter = "RecieptTypeCode Like 'CLP%'"
            If dvCreditVoucher.Count > 0 Then
                For Each CLpRow As DataRowView In dvCreditVoucher
                    Dim TotalPoints As Decimal = CLpRow("Amount")

                    If CLP_Data.CLPConfigdata.Tables("CLPHeader").Rows(0)("RedemptionType") = "Rdt3" Then

                        If CLP_Data.RedPoint = 0 Then
                            CLPRedemptionPoints = dvCreditVoucher.ToTable().Compute("Sum(Amount)", "")
                        Else
                            CLPRedemptionPoints = CLP_Data.RedPoint
                        End If
                        If objclscomman.UpdateClpPoints(False, CLPProgrmaID, CustomerID, CLPRedemptionPoints, SpectrumCon, tran, SiteCode, UserName, SaleInVoiceNumber, CreationDate) = False Then
                            tran.Rollback()
                            CloseConnection()
                            Return False
                        End If
                    Else


                        If redepoints <= 0 Then
                            CLPRedemptionPoints = TotalPoints
                        Else
                            CLPRedemptionPoints = redepoints
                        End If


                        If objclscomman.UpdateClpPoints(False, CLPProgrmaID, CustomerID, CLPRedemptionPoints, SpectrumCon, tran, SiteCode, UserName, SaleInVoiceNumber, CreationDate) = False Then
                            tran.Rollback()
                            CloseConnection()
                            Return False
                        End If
                    End If

                Next
            End If
            'For Each drCLP As DataRow In ds.Tables("CashMemoDtl").Select("Btype='S' And  ArticleCode='" & CLPArticle & "'", "", DataViewRowState.CurrentRows)
            '    Dim TotalPoints As Integer = drCLP("Quantity")
            '    If UpdateClpPoints(True, ClpProgramId, CLPCustomerId, TotalPoints, SpectrumCon, tran) = False Then
            '        tran.Rollback()
            '        CloseConnection()
            '        Return False
            '    End If
            'Next

            If Not dtGV Is Nothing AndAlso dtGV.Rows.Count > 0 Then
                If objclscomman.GenerateGiftVoucher(dtGV, tran, BirthLisID, SiteCode, UserName, CreationDate, DateDayOpen) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
            End If

            Dim dvGVUpdate As DataView
            For Each drGv As DataRow In dtGV.Rows
                'dvGVUpdate = New DataView(_dtPaymentHistory, "RecieptType=GiftVoucher(I) and Amount='" & drGv("ValueOfVoucher") & "'", "", DataViewRowState.CurrentRows)
                'For Each dvRpw As DataRowView In dvGVUpdate
                '    dvRpw("Number") = drGv("VOURCHERSERIALNBR") 
                'Next

                Dim dtTemp() As DataRow = _dtPaymentHistory.Select("Number='GV Non-PrePrint' and Amount='" & drGv("ValueOfVoucher") * -1 & "'", "", DataViewRowState.CurrentRows)
                For Each dr In dtTemp
                    dr.BeginEdit()
                    dr("Number") = drGv("VOURCHERSERIALNBR")
                    dr.EndEdit()

                Next

            Next



            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function UpdateCreditVoucher(ByVal CVProgram As String, ByVal DocType As String, ByVal NewVoucher As Boolean, ByVal CMNo As String, ByVal SiteCode As String, _
            ByVal ServerDateTime As DateTime, ByVal UserId As String, ByRef tran As SqlTransaction, ByRef con As SqlConnection, _
            Optional ByVal Amount As Double = 0, Optional ByRef VoucherNo As String = "0", Optional ByRef VoucherDays As Int32 = 0, Optional ByVal CVorGV As String = "", Optional ByRef iCVNumber As Integer = 0, Optional ByVal IssuedAgainstCLP As Boolean = False) As Boolean
        Try
            Dim cmdVoucher As SqlCommand
            Dim strQuery As String
            If Amount < 0 Then
                Amount = Amount * -1
            End If

            Dim objclscomman As New clsCommon
            If NewVoucher Then
                Dim CVNo As String
                If iCVNumber = 0 Then
                    CVNo = objclscomman.getDocumentNo(IIf(CVorGV = "", "CV", CVorGV), SiteCode)
                    CVNo = CInt(CVNo) + 1
                    iCVNumber = CVNo
                Else
                    CVNo = iCVNumber + 1
                    iCVNumber = CVNo
                End If

                'Changed by Rohit to generate Document No. for proper sorting
                Dim genVoucherNo As String = String.Empty
                Dim strcvorgv As String = "C"
                Try

                    If CVorGV <> "" Then
                        strcvorgv = CVorGV.Substring(0, 1)
                    Else
                        strcvorgv = CVorGV
                    End If
                    genVoucherNo = objclscomman.GenDocNo(IIf(CVorGV = "", "C", strcvorgv) & SiteCode.Substring(SiteCode.Length - 3, 3) & FinacialYear.Substring(FinacialYear.Length - 2, 2), 13, CVNo)
                Catch ex As Exception
                    genVoucherNo = IIf(CVorGV = "", "C", strcvorgv) + SiteCode.Substring(SiteCode.Length - 3, 3) + CVNo
                End Try
                'End Change by Rohit

                strQuery = "INSERT INTO VOUCHERDTLS(SITECODE,VOUCHERCODE,VOURCHERSERIALNBR,VALUEOFVOUCHER,ISACTIVE," & _
                "ISISSUED,ISSUEDATSITE,ISSUEDONDATE,ISSUEDINDOCTYPE,ISSUEDDOCNUMBER,EXPIRYDATE,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS,IssuedForCLP)" & _
                "VALUES('" & SiteCode & "','" & CVProgram & "','" & genVoucherNo & "'," & clsCommon.ConvertToEnglish(Amount) & ",1,1,'" & SiteCode & "',"

                If IssuedAgainstCLP Then
                    strQuery += "Convert(DateTime,'" & DateDayOpen.ToString("yyyyMMdd") & "') ,'" & DocType & "','" & CMNo & "',getdate() + " & VoucherDays & " ,'" & SiteCode & "','" & UserId & "', getdate(),'" & SiteCode & "','" & UserId & "',getdate(),1,1)"
                Else
                    strQuery += "Convert(DateTime,'" & DateDayOpen.ToString("yyyyMMdd") & "') ,'" & DocType & "','" & CMNo & "',getdate() + " & VoucherDays & " ,'" & SiteCode & "','" & UserId & "', getdate(),'" & SiteCode & "','" & UserId & "',getdate(),1,0)"
                End If


                cmdVoucher = New SqlCommand(strQuery, con)
                cmdVoucher.Transaction = tran
                If cmdVoucher.ExecuteNonQuery() > 0 Then
                    If objclscomman.UpdateDocumentNo(IIf(CVorGV = "", "CV", CVorGV), con, tran, CVNo) Then
                        VoucherNo = genVoucherNo
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                strQuery = "UPDATE VOUCHERDTLS SET RedeemedDocNumber='" & CMNo & "',RedeemedAtSite='" & SiteCode & "',RedeemedInDoctype='" & DocType & "',RedeemedOnDate= Convert(DateTime,'" & DateDayOpen.ToString("yyyyMMdd") & "') " & _
                " ,IsRedeemed=1, UPDATEDAT='" & SiteCode & "',UPDATEDBY='" & UserId & "',UPDATEDON= getdate()   WHERE SITECODE='" & SiteCode & "'And VOURCHERSERIALNBR='" & VoucherNo & "'"
                cmdVoucher = New SqlCommand(strQuery, con)
                cmdVoucher.Transaction = tran
                If cmdVoucher.ExecuteNonQuery() > 0 Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception

            'MsgBox(ex.Message, getValueByKey("CLAE05"))

            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Sub New()
        Try
            CreationDate = objclsComman.GetCurrentDate()
        Catch ex As Exception

        End Try

    End Sub

    'Function added by Ashish for CR 5679
    Public Function GetSrNo(ByVal blid As String, ByVal articlecode As String, ByVal eancode As String) As Int32
        Dim index As Int32 = 0
        Try
            Dim strQuery As String = "Select max(SrNo) from BirthListRequestedItems where birthlistid = '" & blid & "' and articlecode='" & articlecode & "' and ean ='" & eancode & "'"
            Dim _dt As New DataTable
            Dim objclsBirthListSales As New clsBirthListSales
            Dim errMsg As String = String.Empty
            _dt = objclsBirthListSales.GetBLPriceConfig(strQuery, errMsg)
            If _dt.Rows.Count > 0 Then
                'If loop added by Rohit for Issue no.0003273 on internal Manits
                If Not _dt.Rows(0)(0) Is DBNull.Value Then
                    index = CInt(_dt.Rows(0)(0))
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
        Return index
    End Function


    'Function added by Rohit for CR 5679
    Public Function GetBLSalesSrNo(ByVal blid As String, ByVal articlecode As String, ByVal eancode As String) As Int32
        Dim index As Int32 = 0
        Try
            Dim strQuery As String = "Select max(SrNo) from BirthListSalesDtl where birthlistid = '" & blid & "' and articlecode='" & articlecode & "' and ean ='" & eancode & "'"
            Dim _dt As New DataTable
            Dim objclsBirthListSales As New clsBirthListSales
            Dim errMsg As String = String.Empty
            _dt = objclsBirthListSales.GetBLPriceConfig(strQuery, errMsg)
            If _dt.Rows.Count > 0 Then
                'If loop added by Rohit for Issue no.0003273 on internal Manits
                If Not _dt.Rows(0)(0) Is DBNull.Value Then
                    index = CInt(_dt.Rows(0)(0))
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
        Return index
    End Function

    Public Function GetBLReqRecord(ByVal blid As String, ByVal articlecode As String, ByVal eancode As String, ByVal sp As Decimal, ByVal finyear As String) As DataTable
        Dim _dt As New DataTable
        Try
            Dim strQuery As String = "Select * from BirthListRequestedItems where birthlistid = '" & blid & "' and finyear = '" & finyear & "' and articlecode='" & articlecode & "' and ean ='" & eancode & "' and sellingprice=" & clsCommon.ConvertToEnglish(sp) & " and RequstedQty > 0"

            Dim objclsBirthListSales As New clsBirthListSales
            Dim errMsg As String = String.Empty
            _dt = objclsBirthListSales.GetBLPriceConfig(strQuery, errMsg)
        Catch ex As Exception
            LogException(ex)
        End Try
        Return _dt
    End Function

End Class
