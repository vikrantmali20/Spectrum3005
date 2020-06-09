Imports System.Data.SqlClient

Public Class clsBirthListUpdateSave
    Private dsBirthListSales As New DataSet
    Private objClsSalesOrder As New clsSalesOrder
    Private objClsComman As New clsCommon
    Private _SiteCode As String
    Private _TerminalID As String
    Private _UserName As String
    Private _BirthLisID As String
    Private _CustomerID As String
    Private _PaidAmount As Decimal
    Private _dtBirthListCustomerDetail As DataTable
    Private _dtPaymentHistory As DataTable
    Private _dtBirthListItemDetail As DataTable
    Private _eventDate As Date
    Private _birthDate As Date
  

    Public Property SiteCode() As String
        Get
            Return _SiteCode
        End Get
        Set(ByVal value As String)
            _SiteCode = value
        End Set
    End Property
    Public Property TerminalID() As String
        Get
            Return _TerminalID
        End Get
        Set(ByVal value As String)
            _TerminalID = value
        End Set
    End Property
    Public Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property
    Public Property BirthLisID() As String
        Get
            Return _BirthLisID
        End Get
        Set(ByVal value As String)
            _BirthLisID = value
        End Set
    End Property
    Public Property EventDate() As String
        Get
            Return _eventDate
        End Get
        Set(ByVal value As String)
            _eventDate = value
        End Set
    End Property
    Public Property BirthDate() As String
        Get

        End Get
        Set(ByVal value As String)

        End Set
    End Property
    Public Property CustomerID() As String
        Get
            Return _CustomerID
        End Get
        Set(ByVal value As String)
            _CustomerID = value
        End Set
    End Property
    Public Property PaidAmount() As Decimal
        Get
            Return _PaidAmount
        End Get
        Set(ByVal value As Decimal)
            _PaidAmount = value
        End Set
    End Property
    Public Property DataTablePaymentHistory() As DataTable
        Get
            Return _dtPaymentHistory
        End Get
        Set(ByVal value As DataTable)
            _dtPaymentHistory = value
        End Set
    End Property
    Public Property DataTableBirthListItemDetail() As DataTable
        Get
            Return _dtBirthListItemDetail
        End Get
        Set(ByVal value As DataTable)
            _dtBirthListItemDetail = value
        End Set
    End Property
    Public Property DataTableBirthListCustomerDetail() As DataTable
        Get
            Return _dtBirthListCustomerDetail
        End Get
        Set(ByVal value As DataTable)
            _dtBirthListCustomerDetail = value
        End Set
    End Property

    Private Function GetDataTableStructure() As Boolean
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

            strQuery.Append(" select  SiteCode,BirthListId,EventDate,EventId,CustomerId,DeliveryDate,BirthDate,GVApplicable,BirthListStatus,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDBY,UPDATEDAT,UPDATEDON,STATUS from BirthList where 1=0")

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
            dsBirthListSales.Tables(3).TableName = "BirthList"
        Catch ex As Exception
            LogException(ex)
        Finally
            CloseConnection()
        End Try
    End Function
    Public Function Save() As Boolean
        GetDataTableStructure()
        Save_BirthList()
    End Function
    Public Function Save_BirthList() As Boolean


        Try
          
            'dsBirthListSales.Tables("BirthList").Rows(0).EndEdit()
            'dsBirthListSales.Tables("BirthList").Rows(0).AcceptChanges()
            'dsBirthListSales.Tables("BirthList").AcceptChanges()

            'If (dsBirthListSales.Tables("BirthList").Rows(0).RowState = DataRowState.Unchanged) Then
            '    dsBirthListSales.Tables("BirthList").Rows(0).SetModified()
            'End If
            OpenConnection()
            Dim tran = SpectrumCon.BeginTransaction()
            If (UpdateData(dsBirthListSales.Tables("BirthList"), SpectrumCon, tran)) Then
                tran.Commit()
            Else
                tran.Rollback()
            End If
            'UpdateData(dsBirthListSales.Tables("BirthList"), SpectrumCon, tran)

        Catch ex As Exception
            LogException(ex)
        Finally

            CloseConnection()

        End Try
    End Function
    Public Function UpdateData(ByVal dTable As DataTable, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Boolean
        Try
            Dim dr As DataRow = DataTableBirthListCustomerDetail.Rows(0)
            dTable.ImportRow(dr)
            dTable = SaveDataTable(dTable)
            Dim strSelect As String
            Dim StrTableName As String
            Dim SelectedColumns As String = ""
            Dim updateStr As String = ""
            Dim str As DataRowState = dTable.Rows(0).RowState
            StrTableName = dTable.TableName.ToString()
            Dim j As Integer = 0
            For j = 0 To dTable.Columns.Count - 1
                SelectedColumns = SelectedColumns & "," & dTable.Columns(j).ColumnName.ToString()
                updateStr = updateStr & dTable.Columns(j).ColumnName.ToString() & "=@" & dTable.Columns(j).ColumnName.ToString()
            Next j
            SelectedColumns = SelectedColumns.Substring(1)
            strSelect = "Select " + SelectedColumns & " FROM " & StrTableName & " Where SiteCode='" & dTable.Rows(0)("SiteCode") & "'  and BirthListid='" & dTable.Rows(0)("BirthListID") & "'"

            Dim atable As New DataTable
            Dim dasave As New SqlDataAdapter(strSelect, con)
            dasave.SelectCommand.CommandTimeout = 0
            dasave.SelectCommand.Transaction = tran

            Dim cb As SqlCommandBuilder
            cb = New SqlCommandBuilder(dasave)
            dasave = cb.DataAdapter
            dasave.Fill(atable)
            
            Dim iINdex As Integer = dasave.Update(SaveDataTable(atable))

            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Function SaveDataTable(ByVal dt As DataTable) As DataTable
        Dim dr As DataRow = DataTableBirthListCustomerDetail.Rows(0)
        dt.Rows(0)("BirthDate") = dr("DateOfBirth")
        dt.Rows(0)("EventDate") = dr("EventDate")
        dt.Rows(0)("GVApplicable") = True
        dt.Rows(0)("BirthListStatus") = "Open"
        dt.Rows(0)("CREATEDAT") = "Open"
        dt.Rows(0)("CREATEDBY") = "Open"
        dt.Rows(0)("BirthListStatus") = "dsfd"
        dt.Rows(0)("BirthDate") = "2/02/09"
        dt.Rows(0)("CREATEDON") = "2/02/09"
        dt.Rows(0)("UPDATEDBY") = "Open"
        dt.Rows(0)("UPDATEDAT") = "Open"
        dt.Rows(0)("UPDATEDON") = "2/02/09"
        dt.Rows(0)("STATUS") = 1
        Return dt
    End Function


End Class
