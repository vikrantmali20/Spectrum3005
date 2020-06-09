Imports System.Data.SqlClient

Public Class clsBirthListSave
    Private Shared dsBirthListSales As New DataSet
    Private Shared objClsSalesOrder As New clsSalesOrder
    Private Shared objClsComman As New clsCommon
    Shared _SiteCode As String
    Shared _TerminalID As String
    Shared _UserName As String
    Shared _BirthLisID As String
    Shared _CustomerID As String
    Shared _PaidAmount As Decimal
    Shared _dtPaymentHistory As DataTable
    Shared _dtBirthListItemDetail As DataTable

    Public Shared Property SiteCode() As String
        Get
            Return _SiteCode
        End Get
        Set(ByVal value As String)
            _SiteCode = value
        End Set
    End Property
    Public Shared Property TerminalID() As String
        Get
            Return _TerminalID
        End Get
        Set(ByVal value As String)
            _TerminalID = value
        End Set
    End Property
    Public Shared Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property
    Public Shared Property BirthLisID() As String
        Get
            Return _BirthLisID
        End Get
        Set(ByVal value As String)
            _BirthLisID = value
        End Set
    End Property
    Public Shared Property CustomerID() As String
        Get
            Return _CustomerID
        End Get
        Set(ByVal value As String)
            _CustomerID = value
        End Set
    End Property
    Public Shared Property PaidAmount() As Decimal
        Get
            Return _PaidAmount
        End Get
        Set(ByVal value As Decimal)
            _PaidAmount = value
        End Set
    End Property
    Public Shared Property DataTablePaymentHistory() As DataTable
        Get
            Return _dtPaymentHistory
        End Get
        Set(ByVal value As DataTable)
            _dtPaymentHistory = value
        End Set
    End Property
    Public Shared Property DataTableBirthListItemDetail() As DataTable
        Get
            Return _dtBirthListItemDetail
        End Get
        Set(ByVal value As DataTable)
            _dtBirthListItemDetail = value
        End Set
    End Property

    Private Shared Function GetDataTableStructure() As Boolean
        Try
            Dim strQuery As New System.Text.StringBuilder
            strQuery.Length = 0
            strQuery.Append("select SiteCode,DocumentNumber,SaleInvNumber,SaleInvLineNumber,DocumentType,TerminalID,TenderTypeCode,AmountTendered,")

            strQuery.Append("ExchangeRate,CurrencyCode,SOInvDate,SOInvTime,UserName,ManagersKeytoUpdate,")

            strQuery.Append("ChangeLine,(CASE WHEN REFNO_1 LIKE '%,%' THEN Replace(REFNO_1,',','.') ELSE REFNO_1 END) AS REFNO_1,RefNo_2,RefNo_3,RefNo_4,RefDate,CREATEDAT,CREATEDBY,")

            strQuery.Append("CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS from salesinvoice where 1=0")

            strQuery.Append(" select SiteCode,BirthListId,CustomerId,ArticleCode,EAN,BookedQty,DeliveredQty,")

            strQuery.Append("Rate,DiscAmt,NetAmt,DeliveryDate from BirthListSalesDtl where 1=0")

            strQuery.Append(" select SiteCode,BirthListId,CustomerId,SaleInvNumber,PaidAmt,DelItemAmt from BirthListSalesHdr where 1=0")
            OpenConnection()
            Dim sqlCmd As New SqlCommand
            sqlCmd.CommandText = strQuery.ToString()
            sqlCmd.Connection = SpectrumCon
            Dim sqlAdptor As New SqlDataAdapter
            sqlAdptor.SelectCommand = sqlCmd
            sqlAdptor.Fill(dsBirthListSales)
            dsBirthListSales.Tables(0).TableName = "SalesInvoice"
            dsBirthListSales.Tables(1).TableName = "BirthListSalesDtl"
            dsBirthListSales.Tables(2).TableName = "BirthListSalesHdr"

        Catch ex As Exception
        Finally

            CloseConnection()
        End Try
    End Function
    Private Shared Function Save_BirthListSalesHdr() As Boolean
        Try
            If Not (dsBirthListSales.Tables("BirthListSalesHdr") Is Nothing) Then
                Dim drBirthListSalesHdr As DataRow
                drBirthListSalesHdr = dsBirthListSales.Tables("BirthListSalesHdr").NewRow()
                'SiteCode, BirthListId, CustomerId, SaleInvNumber, PaidAmt, DelItemAmt
                drBirthListSalesHdr("SiteCode") = SiteCode
                drBirthListSalesHdr("BirthListId") = BirthLisID
                drBirthListSalesHdr("CustomerId") = CustomerID
                drBirthListSalesHdr("SaleInvNumber") = objClsSalesOrder.GetNextSOInvcNo(SiteCode)
                drBirthListSalesHdr("PaidAmt") = PaidAmount
                drBirthListSalesHdr("DelItemAmt") = PaidAmount
                dsBirthListSales.Tables("BirthListSalesHdr").Rows.Add(drBirthListSalesHdr)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
        

    End Function
    Private Shared Function Save_BirthListSalesDtl() As Boolean
        Dim drBirthListSalesDtl As DataRow
        Try
            If Not _dtPaymentHistory Is Nothing Then
                For Each drBirthListItemDetail As DataRow In _dtBirthListItemDetail.Rows
                    drBirthListSalesDtl = dsBirthListSales.Tables("BirthListSalesDtl").NewRow()
                    drBirthListSalesDtl("SiteCode") = drBirthListItemDetail("SiteCode")
                    drBirthListSalesDtl("BirthListId") = drBirthListItemDetail("BirthListId")
                    drBirthListSalesDtl("CustomerId") = CustomerID
                    drBirthListSalesDtl("ArticleCode") = drBirthListItemDetail("ArticleCode")
                    drBirthListSalesDtl("EAN") = drBirthListItemDetail("EAN")
                    drBirthListSalesDtl("BookedQty") = drBirthListItemDetail("PurchasedQty")
                    drBirthListSalesDtl("DeliveredQty") = drBirthListItemDetail("Pickupqty")
                    drBirthListSalesDtl("Rate") = drBirthListItemDetail("Price")
                    drBirthListSalesDtl("DiscAmt") = drBirthListItemDetail("NetAmount")
                    drBirthListSalesDtl("NetAmt") = drBirthListItemDetail("NetAmount")
                    drBirthListSalesDtl("DeliveryDate") = objClsComman.GetCurrentDate()
                    dsBirthListSales.Tables("BirthListSalesDtl").Rows.Add(drBirthListSalesDtl)
                Next
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
        Finally
            CloseConnection()
        End Try

    End Function

    Private Shared Function Save_SalesInvoice() As Boolean
        Dim drInvc As DataRow
        Try
            If Not _dtPaymentHistory Is Nothing Then
                For Each drPayment As DataRow In _dtPaymentHistory.Rows
                    drInvc = dsBirthListSales.Tables("SalesInvoice").NewRow()
                    drInvc("SiteCode") = SiteCode
                    drInvc("DocumentNumber") = BirthLisID
                    drInvc("SaleInvNumber") = objClsSalesOrder.GetNextSOInvcNo(SiteCode)
                    drInvc("SaleInvLineNumber") = drPayment("SrNo")
                    drInvc("TerminalID") = TerminalID
                    drInvc("TenderTypeCode") = drPayment("RecieptTypeCode")
                    drInvc("TenderheadCode") = drPayment("RecieptType")
                    drInvc("AmountTendered") = drPayment("Amount")
                    drInvc("ExchangeRate") = drPayment("ExchangeRate")
                    drInvc("CurrencyCode") = drPayment("CurrencyCode")
                    drInvc("SOInvDate") = objClsComman.GetCurrentDate().Date
                    drInvc("SOInvTime") = Format(Now, "hh:mm:ss tt")
                    drInvc("UserName") = UserName
                    drInvc("ManagersKeytoUpdate") = 3
                    drInvc("ChangeLine") = 3
                    drInvc("RefNo_1") = drPayment("Number")
                    drInvc("RefNo_2") = 3
                    drInvc("RefNo_3") = 3
                    drInvc("RefNo_4") = 3
                    drInvc("RefDate") = drPayment("DATE") 'objClsComman.GetCurrentDate()
                    drInvc("CREATEDAT") = SiteCode
                    drInvc("CREATEDBY") = UserName
                    drInvc("CREATEDON") = objClsComman.GetCurrentDate()
                    drInvc("UPDATEDAT") = SiteCode
                    drInvc("UPDATEDBY") = UserName
                    drInvc("UPDATEDON") = objClsComman.GetCurrentDate()
                    drInvc("STATUS") = True
                    dsBirthListSales.Tables("SalesInvoice").Rows.Add(drInvc)
                Next
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
        Finally
            CloseConnection()
        End Try

    End Function
    Public Shared Function UpdateBirthListRequestedItem() As Boolean
        Dim Tran As SqlTransaction = Nothing
        Try
            OpenConnection()
            Dim sqlQuery As New SqlCommand()
            sqlQuery.Connection = SpectrumCon
            Tran = SpectrumCon.BeginTransaction()

            Dim strArticleCode As String
            Dim strEAN As String
            Dim iBookedQty As Integer
            Dim iDeliveredQty As Integer
            For Each drBirthListItemDetail As DataRow In _dtBirthListItemDetail.Rows
                strArticleCode = drBirthListItemDetail("ArticleCode")
                strEAN = drBirthListItemDetail("EAN")
                iBookedQty = drBirthListItemDetail("BookedQty")
                iDeliveredQty = drBirthListItemDetail("DeliveredQty")
                sqlQuery.CommandText = "update BirthListRequestedItems set BookedQty=" & iBookedQty & ",DeliveredQty=" & iDeliveredQty & "  where SiteCode='" & SiteCode & "'and BirthListId='" & BirthLisID & "' and EAN='" & strEAN & "'and ArticleCode='" & strArticleCode & "'"
                sqlQuery.Transaction = Tran
                sqlQuery.ExecuteNonQuery()
            Next
            Tran.Commit()
            Return True
        Catch ex As Exception
            Tran.Rollback()
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function
    Public Shared Function Save() As Boolean
        Dim Tran As SqlTransaction = Nothing
        Try
            GetDataTableStructure()
            Save_SalesInvoice()
            Save_BirthListSalesHdr()
            Save_BirthListSalesDtl()
            UpdateBirthListRequestedItem()
            OpenConnection()
            Tran = SpectrumCon.BeginTransaction()
            objClsComman.SaveData(dsBirthListSales, SpectrumCon, Tran)
            Tran.Commit()
            Return True
        Catch ex As Exception
            Tran.Rollback()
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

End Class
