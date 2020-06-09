Imports SpectrumBL
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Text
Imports System.Math

Public Class clsBirthList
    Inherits clsCommon
    Dim dtGridItem As New DataTable()
    Private dsDisplay As New DataSet
    Private dsBirthListReq As New DataSet
    Private _BirthListID As String
    Private _eventDate As DateTime
    Private _customerID As String
    Private _eventID As String
    Dim dtStock As New DataTable()
    Dim dtBirthListReq As New DataTable(DataTableName)
    Private objclsAcceptPayment As New clsAcceptPayment
    Dim dtBirthList As New DataTable
    Dim _dtOriginal As New DataTable

    ' ''' <summary>
    ' '''  SiteCode
    ' ''' </summary>
    ' ''' <value>String </value>
    ' '''  <UsedBY>frmBirthListSales.vb,frmBirthListUpdation.vb</UsedBY>
    ' ''' <returns></returns>
    ' ''' <remarks>Read,Write</remarks>
    ' ''' 
    Private _SiteCode As String
    Public Property SiteCode() As String
        Get
            Return _SiteCode
        End Get
        Set(ByVal value As String)
            _SiteCode = value
        End Set
    End Property
    Private _UserCode As String
    Public Property UsrCode() As String
        Get
            Return _userCode
        End Get
        Set(ByVal value As String)
            _UserCode = value
        End Set
    End Property



    'Private ReadOnly Property ArticleImageFolderPath() As String
    '    Get
    '        Return "C:\Documents and Settings\rahul.katkar.CREATIVEIT\Desktop\ArticleImages\"
    '    End Get
    'End Property
    ''' <summary>
    '''  TODO: BirthList requested item table  
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    '''  <usedby>frmBirthList.vb</usedby>
    ''' <remarks>ReadOnly</remarks>
    Private ReadOnly Property DataTableName() As String
        Get
            Return "BirthListRequestedItems"
        End Get
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

    ''' <summary>
    '''  TODO: Event i.e birthday or anything else
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    '''  <usedby>frmBirthList.vb</usedby>
    ''' <remarks>Readonly </remarks>
    Private ReadOnly Property EventID() As String
        Get
            Return _eventID
        End Get
    End Property

    ''' <summary>
    '''  TODO: Event date 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    '''  <usedby>frmBirthList.vb</usedby>
    ''' <remarks>Read And Write</remarks>
    Private Property EventDate() As DateTime
        Get
            Return _eventDate
        End Get
        Set(ByVal value As DateTime)
            _eventDate = value
        End Set
    End Property

    ''' <summary>
    '''  TODO: CustomerID who is creating bith list 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    '''  <usedby>frmBirthList.vb</usedby>
    ''' <remarks>Readonly   </remarks>
    Private ReadOnly Property CustomerId() As String
        Get
            Return _customerID
        End Get
    End Property

    ''' <summary>
    '''  TODO: Checking table structure for grid is created or not 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    '''  <usedby>frmBirthList.vb</usedby>
    ''' <remarks>Readonly  </remarks>
    Private ReadOnly Property IsTableGridItemDetail_Created() As Boolean
        Get
            If (dtGridItem.Columns.Count > 0) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
    Private _icustomerType As String
    Public Property CustomerType() As String
        Get
            Return _icustomerType
        End Get
        Set(ByVal value As String)
            _icustomerType = value
        End Set
    End Property

    Public Property DataTableOriginal() As DataTable
        Get
            Return _dtOriginal
        End Get
        Set(ByVal value As DataTable)
            _dtOriginal = value
        End Set
    End Property

    ''' <summary>
    '''  TODO: Id for birthlist 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    '''  <usedby>frmBirthList.vb</usedby>
    ''' <remarks>Readonly   </remarks>
    Public Property BirthListID() As String
        Get
            Return _BirthListID
        End Get
        Private Set(ByVal value As String)
            _BirthListID = value
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
    Public Function LoadSalesPerson() As DataTable

        Dim datasetRecieptType As New DataSet
        Try
            OpenConnection()
            'Dim sqlSelectCommand As New SqlCommand("select tendertype,Description from MstTenderType", SpectrumCon)
            Dim sqlSelectCommand As New SqlCommand("select empcode,salesPersonName from MstSalesPerson  ", SpectrumCon)
            Dim sqlAdapter As New SqlDataAdapter()
            sqlAdapter.SelectCommand = sqlSelectCommand
            sqlAdapter.Fill(datasetRecieptType, "tblSalesPerson")
            Return datasetRecieptType.Tables("tblSalesPerson")
        Catch ex As Exception
            LogException(ex)
        Finally
            CloseConnection()
        End Try

    End Function



    ''' <summary>
    '''  TODO:Create table structure for grid datasource
    ''' </summary>
    ''' 
    ''' <returns>DataTable</returns>
    '''  <usedby>frmBirthList.vb</usedby>
    ''' <remarks>Readonly   </remarks>
    Private Function CreateDataTable() As DataTable
        Dim keys(1) As DataColumn

        dtGridItem.TableName = "GridItemDetail"
        'dtGridItem.Columns.Add(New DataColumn("BirthListID"))
        Dim dcEAN As DataColumn = New DataColumn("EAN")
        dcEAN.AllowDBNull = False
        dtGridItem.Columns.Add(dcEAN)

        dtGridItem.Columns.Add(New DataColumn("ArticleCode"))

        dtGridItem.Columns.Add(New DataColumn("ArticleDescription"))

        Dim dcRequestedQty As DataColumn = New DataColumn("RequstedQty", System.Type.GetType("System.Int32"))
        dcRequestedQty.AllowDBNull = False
        dcRequestedQty.DefaultValue = 1
        dtGridItem.Columns.Add(dcRequestedQty)

        Dim dcBookedQty As DataColumn = New DataColumn("BookedQty", System.Type.GetType("System.Int32"))
        dcBookedQty.AllowDBNull = False
        dcBookedQty.DefaultValue = 0
        dtGridItem.Columns.Add(dcBookedQty)

        Dim dcDeliveredQty As DataColumn = New DataColumn("DeliveredQty", System.Type.GetType("System.Int32"))
        dcDeliveredQty.AllowDBNull = False
        dcDeliveredQty.DefaultValue = 0
        dtGridItem.Columns.Add(dcDeliveredQty)


        Dim dcReservedQty As DataColumn = New DataColumn("ReservedQty", System.Type.GetType("System.Int32"))
        dcReservedQty.AllowDBNull = False
        dcReservedQty.DefaultValue = 0
        dtGridItem.Columns.Add(dcReservedQty)

        Dim dcCLPQty As DataColumn = New DataColumn("IsCLP", System.Type.GetType("System.Boolean"))
        dcCLPQty.AllowDBNull = False
        dcCLPQty.DefaultValue = True
        dtGridItem.Columns.Add(dcCLPQty)

        Dim dcFreeText As DataColumn = New DataColumn("FreeTexts")
        dcFreeText.AllowDBNull = False
        dcFreeText.DefaultValue = ""
        dtGridItem.Columns.Add(dcFreeText)

        Dim dcUOM As DataColumn = New DataColumn("UOM")
        dcUOM.AllowDBNull = False
        dcUOM.DefaultValue = ""
        dtGridItem.Columns.Add(dcUOM)

        Dim dcOriginalReservedQty As DataColumn = New DataColumn("OriginalReservedQty", System.Type.GetType("System.Int32"))
        dcOriginalReservedQty.AllowDBNull = False
        dcOriginalReservedQty.DefaultValue = 0
        dtGridItem.Columns.Add(dcOriginalReservedQty)

        dtGridItem.Columns.Add(New DataColumn("Rate", System.Type.GetType("System.Decimal")))
        Dim dcAmount As DataColumn = New DataColumn("Amount", System.Type.GetType("System.Decimal"))
        dcAmount.AllowDBNull = False
        dtGridItem.Columns.Add(dcAmount)
        dtGridItem.Columns.Add(New DataColumn("AvailableQty"))

        Dim dcBLDiscountAmt As DataColumn = New DataColumn("BLDiscountAmt", System.Type.GetType("System.Decimal"))
        dcBLDiscountAmt.AllowDBNull = False
        dcBLDiscountAmt.DefaultValue = Decimal.Zero
        dtGridItem.Columns.Add(dcBLDiscountAmt)

        'Change by Ashish for CR 5679
        Dim dcOriginalSellingPrice As DataColumn = New DataColumn("OriginalSellingPrice", System.Type.GetType("System.Decimal"))
        dcOriginalSellingPrice.AllowDBNull = True
        dtGridItem.Columns.Add(dcOriginalSellingPrice)

        Dim dcIsPriceChanged As DataColumn = New DataColumn("IsPriceChanged", System.Type.GetType("System.Boolean"))
        dcIsPriceChanged.AllowDBNull = True
        dcIsPriceChanged.DefaultValue = False

        dtGridItem.Columns.Add(New DataColumn("AuthUserId"))
        dtGridItem.Columns.Add(New DataColumn("AuthUserRemarks"))

        dtGridItem.Columns.Add(dcIsPriceChanged)
        'End of change

        keys(0) = dcEAN
        dtGridItem.PrimaryKey = keys
        'dtGridItem.Columns.Add(New DataColumn("DeliveryQty"))
        'dtGridItem.Columns.Add(New DataColumn("Discount"))
        'dtGridItem.Columns.Add(New DataColumn("Tax"))
        'dtGridItem.Columns.Add(New DataColumn("NetAmount"))
        'dtGridItem.Columns.Add(New DataColumn("DeliveryDate"))
        'dtGridItem.Columns.Add(New DataColumn("Status"))
        'dtGridItem.Columns.Add(New DataColumn("Remark"))
        dsBirthListReq.Tables.Add(dtBirthList)
        dsBirthListReq.Tables.Add(dtBirthListReq)
        Return dtGridItem
    End Function
    ''' <summary>
    '''  TODO:Create table structure for Stock
    ''' </summary>
    ''' <returns></returns>
    '''  <usedby>frmBirthList.vb</usedby>
    ''' <remarks>    </remarks>

    Private Function Datatable_Stock() As DataTable
        Try
            dtStock.Columns.Add(New DataColumn("Amount"))
            dtStock.Columns.Add(New DataColumn("AvailableQty"))
            dtStock.Columns.Add(New DataColumn("Rate"))
            Return dtStock
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' TODO: Event names
    ''' </summary>
    ''' <returns>DataTable</returns>
    '''  <usedby>frmBirthList.vb</usedby>
    ''' <remarks></remarks>
    Public Function LoadEventName() As DataTable
        Dim sqlCmdEvent As New SqlCommand()
        Try
            OpenConnection()
            sqlCmdEvent.CommandText = "select * from BirthListEvents"
            sqlCmdEvent.Connection = SpectrumCon
            Dim sqladptor As New SqlDataAdapter
            Dim dsEvent As New DataSet
            sqladptor.SelectCommand = sqlCmdEvent
            sqladptor.Fill(dsEvent, "tblEventName")
            Return dsEvent.Tables("tblEventName")
        Catch ex As Exception
            LogException(ex)
        Finally
            CloseConnection()
        End Try
    End Function

    ''' <summary>
    ''' TODO: Adding Items in grid datatable
    ''' </summary>
    ''' <returns>DataTable</returns>
    '''  <usedby>frmBirthList.vb</usedby>
    ''' <remarks></remarks>
    Public Function AddItemIn_BirthListRegTable(ByVal drGridItem As DataRow, ByVal strSiteCode As String, Optional ByVal ItemFullName As Boolean = False) As DataTable
        Try
            Dim objclscomman As New clsCommon
            If Not (IsTableGridItemDetail_Created) Then
                CreateDataTable()
            End If
            Dim dtNew As New DataTable()

            If Not (IsItemAdded(drGridItem.Item("EAN"))) Then
                dtNew = dtGridItem.Clone()
                Dim NewRow As DataRow = dtNew.NewRow()
                'NewRow.Item("BirthListID") = BirthListID
                NewRow.Item("ArticleCode") = drGridItem.Item("ArticleCode")
                NewRow.Item("EAN") = drGridItem.Item("EAN")

                If ItemFullName = True Then
                    Dim objClsCommon As New clsCommon
                    NewRow.Item("ArticleDescription") = objClsCommon.GetArticleFullName(drGridItem.Item("ArticleCode"))
                Else
                    NewRow.Item("ArticleDescription") = drGridItem.Item("DISCRIPTION")
                End If
                
                NewRow.Item("Rate") = drGridItem.Item("SellingPRICE")
                'Change by Ashish for CR 5679
                NewRow.Item("OriginalSellingPrice") = drGridItem.Item("SellingPRICE")
                'End of change
                'NewRow.Item("IsCLP") = drGridItem.Item("CLPRequire")
                NewRow.Item("RequstedQty") = 1
                NewRow.Item("BookedQty") = 0
                NewRow.Item("ReservedQty") = 0
                NewRow.Item("DeliveredQty") = 0
                NewRow.Item("OriginalReservedQty") = 0
                NewRow.Item("Amount") = drGridItem.Item("SellingPRICE") * NewRow.Item("RequstedQty")
                NewRow.Item("UOM") = drGridItem.Item("UOM")
                NewRow.Item("AvailableQty") = objclscomman.GetStocks(strSiteCode, drGridItem.Item("EAN"), drGridItem.Item("ArticleCode"), True)
                NewRow.Item("AuthUserId") = ""
                NewRow.Item("AuthUserRemarks") = ""
                dtNew.Rows.Add(NewRow)
                dtNew.Merge(dtGridItem)
                dtGridItem = dtNew
            Else
            End If
            Return dtGridItem
        Catch ex As Exception
            LogException(ex)
        End Try
        Return dtGridItem
    End Function
    ''' <summary>
    ''' TODO:Check Is Already item added into grid or not
    ''' </summary>
    ''' <returns>Boolean</returns>
    '''  <usedby>frmBirthList.vb</usedby>
    ''' <remarks></remarks>
    Public Function IsItemAdded(ByVal strEANNumber As String) As Boolean
        Dim drArticleCode As DataRow = dtGridItem.Rows.Find(strEANNumber)
        If Not drArticleCode Is Nothing Then
            Dim rt As Integer = drArticleCode.Item("RequstedQty")
            If rt + 1 > MaxQuantity Then
                MsgBox(getValueByKey("CM059"), MsgBoxStyle.Critical, "CM059" & " | " & getValueByKey("CLAE05"))
                Return True
            Else
                drArticleCode.Item("RequstedQty") = rt + 1
                drArticleCode.Item("Amount") = drArticleCode.Item("RequstedQty") * drArticleCode.Item("Rate")
                Return True
            End If

        Else
            Return False
        End If

    End Function
    ''' <summary>
    ''' TODO:Save Total information related to new birth list into Database
    ''' </summary>
    ''' <returns>Boolean</returns>
    '''  <usedby>frmBirthList.vb</usedby>
    ''' <remarks></remarks>
    Public Function SaveBirthList(ByVal OnlineConnect As Boolean, ByVal strSiteCode As String, ByVal dtCustomerInfo As DataTable, ByVal strUserName As String, Optional ByVal IsUpdateBirthList As Boolean = False, Optional ByVal strRemark As String = "", Optional ByVal strTerminalId As String = "") As Boolean
        Dim tran As SqlTransaction = Nothing
        Try

            Dim objClscomman As New clsCommon
            If dtBirthListReq.Rows.Count > 0 Then
                dtBirthListReq.Clear()
                If Not IsUpdateBirthList Then
                    Generate_BirthListNumber(strTerminalId, OnlineConnect)
                End If
                CopyRow(dtBirthListReq)
            Else
                If Not IsUpdateBirthList Then
                    Generate_BirthListNumber(strTerminalId, OnlineConnect)
                End If
                DataTable_BirthListRequestedItem(strSiteCode, strUserName)
            End If

            'Added by Rohit for adding updatedad, updatedon and updated by columns to BLRequestedItems table

            If Not dtBirthListReq.Columns.Contains("UPDATEDAT") Then
                Dim dcUPDATEDAT As New DataColumn
                dcUPDATEDAT.ColumnName = "UPDATEDAT"
                dcUPDATEDAT.DefaultValue = strSiteCode
                dtBirthListReq.Columns.Add(dcUPDATEDAT)
            End If

            If Not dtBirthListReq.Columns.Contains("UPDATEDBY") Then
                Dim dcUPDATEDBy As New DataColumn
                dcUPDATEDBy.ColumnName = "UPDATEDBY"
                dcUPDATEDBy.DefaultValue = strUserName
                dtBirthListReq.Columns.Add(dcUPDATEDBy)
            End If


            If Not dtBirthListReq.Columns.Contains("UPDATEDON") Then
                Dim dcUPDATEDON As New DataColumn
                dcUPDATEDON.ColumnName = "UPDATEDON"
                dcUPDATEDON.DefaultValue = objClscomman.GetCurrentDate()
                dtBirthListReq.Columns.Add(dcUPDATEDON)
            End If

            'Addition Ends

            If Not IsUpdateBirthList Then
                If SetDataRowState_Added() Then
                    If Not dsBirthListReq Is Nothing AndAlso dsBirthListReq.Tables.Contains("BirthList") Then
                        dsBirthListReq.Tables("BirthList").Clear()
                    End If


                    SaveBirthListID(strSiteCode, dtCustomerInfo, , strRemark)
                    OpenConnection()

                    tran = SpectrumCon.BeginTransaction()
                    If (objClscomman.UpdateDocumentNo("BirthList", SpectrumCon, tran)) Then
                        If (SaveData(dsBirthListReq, SpectrumCon, tran)) Then
                            Return UpdateReservedQty(dtGridItem, SpectrumCon, tran)
                        Else
                            tran.Rollback()
                            Return False
                        End If
                    Else
                        tran.Rollback()
                        Return False
                    End If
                End If
            Else
                SaveBirthListID(strSiteCode, dtCustomerInfo, , strRemark)
                OpenConnection()
                tran = SpectrumCon.BeginTransaction()
                If (SaveData(dsBirthListReq, SpectrumCon, tran)) Then
                    Return UpdateReservedQty(dtGridItem, SpectrumCon, tran)
                Else
                    tran.Rollback()
                    Return False
                End If
            End If

        Catch ex As Exception
            tran.Rollback()
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try

    End Function


    Private Function UpdateReservedQty(ByVal dtBirthListItem As DataTable, ByRef spectrumCon As SqlConnection, ByRef sqlTran As SqlTransaction) As Boolean
        Try
            Dim objReservedQty As Object = dtBirthListReq.Compute("sum(ReservedQty)", " ")
            Dim decReservedQty As Decimal
            If Not objReservedQty Is Nothing And Not objReservedQty Is DBNull.Value Then
                decReservedQty = CDbl(objReservedQty)
            End If
            If (decReservedQty > Decimal.Zero) Then
                Dim objclsBirthListSave As New clsBirthListSalesSave
                objclsBirthListSave.FinacialYear = FinacialYear
                objclsBirthListSave.SiteCode = SiteCode
                objclsBirthListSave.StockStorageLocation = StockStorageLocation
                If (objclsBirthListSave.UpdateStock(dtBirthListItem, spectrumCon, sqlTran, True)) Then
                    sqlTran.Commit()
                    dtBirthList.AcceptChanges()
                    dtGridItem.AcceptChanges()
                    Return True
                Else
                    sqlTran.Rollback()
                    Return False
                End If
            Else
                sqlTran.Commit()
                dtBirthList.AcceptChanges()
                dtGridItem.AcceptChanges()
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function


    Private Function SaveChanges(ByVal strSiteCode As String, ByVal strUserName As String, ByVal tran As SqlTransaction) As Boolean
        Try


        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' TODO:SaveBirthList hdr information
    ''' </summary>
    ''' <returns>Boolean</returns>
    '''  <usedby>frmBirthList.vb</usedby>
    ''' <remarks></remarks>
    Private Function SaveBirthListID(ByVal strSiteCode As String, ByVal dtCustomerInfo As DataTable, Optional ByVal IsUpdate As Boolean = False, Optional ByVal strRemark As String = "") As Boolean

        Try
            OpenConnection()
            Dim sqlCmdInsert As New SqlCommand("select * from BirthList where 1=0  ", SpectrumCon)
            Dim sqlAdaptor As New SqlDataAdapter(sqlCmdInsert)
            sqlAdaptor.Fill(dtBirthList)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
        Try
            'adding row to datatable 
            If dtBirthList.Rows.Count > 0 Then
                Dim lastBirthListID As String = ""
                If (dtBirthList.Rows.Count > 1) Then
                    dtBirthList.Clear()
                Else
                    AddRow_BirthList(strSiteCode, dtCustomerInfo, FinacialYear, True, strRemark)
                End If
            Else
                AddRow_BirthList(strSiteCode, dtCustomerInfo, FinacialYear, False, strRemark)
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


    Private Sub AddRow_BirthList(ByVal strSiteCode As String, ByVal dtCustomerInfo As DataTable, ByVal strFinacialYear As String, Optional ByVal IsUpdate As Boolean = False, Optional ByVal strRemark As String = "")
        Try
            Dim objclsComman As New clsCommon
            EventDate = CDate(dtCustomerInfo.Rows(0)("EventDate"))
            Dim strEventID As String = dtCustomerInfo.Rows(0)("EventID")
            Dim strCustomerId As String = dtCustomerInfo.Rows(0)("CustomerId")
            Dim dtDeliveryDate As DateTime = CDate(dtCustomerInfo.Rows(0)("DeliveryDate"))

            Dim blngvApplicable As Boolean = dtCustomerInfo.Rows(0)("GVApplicable")
            Dim strAddressType As String = 1
            Dim strSalesPersonCode As String = dtCustomerInfo.Rows(0)("SalesExecutiveCode")
            Dim drNewBirthList As DataRow = AddRows_BirthList(IsUpdate)

            Dim objclsBirthlistGlobal As New clsBirthListGobal
            If IsUpdate Then
                drNewBirthList.BeginEdit()
            End If
            drNewBirthList("BirthListID") = BirthListID
            drNewBirthList("FinYear") = FinacialYear
            drNewBirthList("SiteCode") = strSiteCode
            drNewBirthList("EventDate") = EventDate
            drNewBirthList("EventID") = strEventID
            drNewBirthList("CustomerID") = strCustomerId
            drNewBirthList("CustomerType") = CustomerType
            drNewBirthList("DeliveryAddressType") = strAddressType
            drNewBirthList("DeliveryDate") = dtDeliveryDate
            drNewBirthList("OpenAmount") = Decimal.Zero
            drNewBirthList("GVApplicable") = blngvApplicable
            drNewBirthList("BirthListStatus") = "Open"
            drNewBirthList("Createdat") = strSiteCode
            drNewBirthList("CreatedOn") = objclsComman.GetCurrentDate()
            drNewBirthList("Updatedat") = strSiteCode
            drNewBirthList("Updatedon") = objclsComman.GetCurrentDate()
            drNewBirthList("Updatedby") = DBNull.Value
            drNewBirthList("Createdby") = UsrCode
            drNewBirthList("SalesExecutiveCode") = strSalesPersonCode
            drNewBirthList("Remarks") = strRemark
            drNewBirthList("Status") = True
            If IsUpdate = False Then
                dtBirthList.Rows.Add(drNewBirthList)
            Else
                drNewBirthList.EndEdit()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    '''   Update changes into already saved BirthList. 
    '''  This function decides whether to Update or add row  for birthlist Table.
    ''' </summary>
    ''' <param name="IsUpdate">True:update,Default :false for add new Row</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddRows_BirthList(ByVal IsUpdate As Boolean) As DataRow
        Try
            If (IsUpdate) Then

                'Dim drNewBirthList As DataRow = dtBirthList.NewRow()
                'dtBirthList.Rows.Add(drNewBirthList)
                'For Each drow As DataRow In dtBirthList.Rows
                '    'If (drow.RowState = DataRowState.Deleted) Then
                '    Console.WriteLine(drow.RowState.ToString())


                '    'End If
                'Next
                Return dtBirthList.Rows(0)
            Else
                Dim drNewBirthList As DataRow = dtBirthList.NewRow()
                Return drNewBirthList
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    '''  Change DataTable RowState to SetAdded
    ''' </summary>
    ''' <returns>Boolean</returns>
    '''  <usedby>frmBirthList.vb</usedby>
    ''' <remarks></remarks>
    Private Function SetDataRowState_Added() As Boolean
        Try
            For Each dr As DataRow In dsBirthListReq.Tables(DataTableName).Rows
                If Not dr.RowState = DataRowState.Deleted Then
                    dr.AcceptChanges()
                    dr.SetAdded()
                End If

            Next
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
        End Try
    End Function



    ''' <summary>
    ''' Table structure for "BirthRequestedItems" db table  
    ''' </summary>
    ''' <param name="strSiteCode"></param>
    ''' <returns>DataSet</returns>
    '''  <usedby>frmBirthList.vb</usedby>
    ''' <remarks></remarks>
    Private Function DataTable_BirthListRequestedItem(ByVal strSiteCode As String, Optional ByVal strUserName As String = "") As DataSet
        Try
            Dim dcBirthListID As New DataColumn
            dcBirthListID.ColumnName = "BirthListID"
            dcBirthListID.DataType = System.Type.GetType("System.String")



            Dim dcEAN As DataColumn = New DataColumn("EAN")
            dcEAN.AllowDBNull = False


            Dim dcArticleCode As New DataColumn
            dcArticleCode.ColumnName = dtGridItem.Columns("ArticleCode").ColumnName
            dcArticleCode.DataType = System.Type.GetType("System.String")

            Dim RequstedQty As New DataColumn
            RequstedQty.ColumnName = dtGridItem.Columns("RequstedQty").ColumnName
            RequstedQty.DataType = dtGridItem.Columns("RequstedQty").DataType

            Dim dcBookedQty As New DataColumn
            dcBookedQty.ColumnName = dtGridItem.Columns("BookedQty").ColumnName
            dcBookedQty.DataType = dtGridItem.Columns("BookedQty").DataType

            Dim dcDeliveredQty As New DataColumn
            dcDeliveredQty.ColumnName = dtGridItem.Columns("DeliveredQty").ColumnName
            dcDeliveredQty.DataType = dtGridItem.Columns("DeliveredQty").DataType

            Dim dcStatus As New DataColumn
            dcStatus.ColumnName = "Status"
            dcStatus.DataType = System.Type.GetType("System.Boolean")
            dcStatus.DefaultValue = True


            Dim dcReservedQty As New DataColumn
            dcReservedQty.ColumnName = dtGridItem.Columns("ReservedQty").ColumnName
            dcReservedQty.DataType = dtGridItem.Columns("ReservedQty").DataType

            Dim dcCLPQty As DataColumn = New DataColumn("IsCLP", System.Type.GetType("System.Boolean"))
            dcCLPQty.AllowDBNull = False
            dcCLPQty.DefaultValue = True


            Dim dcFreeText As DataColumn = New DataColumn("FreeTexts")
            dcFreeText.AllowDBNull = False
            dcFreeText.DefaultValue = ""


            Dim objclsBirthlistGlobal As New clsBirthListGobal
            Dim dcFinYear As DataColumn = New DataColumn("FinYear")
            dcFinYear.AllowDBNull = False
            dcFinYear.DefaultValue = FinacialYear



            Dim dcSiteCode As New DataColumn
            dcSiteCode.ColumnName = "SiteCode"
            dcSiteCode.DefaultValue = strSiteCode

            Dim dcCREATEDAT As New DataColumn
            dcCREATEDAT.ColumnName = "CREATEDAT"
            dcCREATEDAT.DefaultValue = strSiteCode

            Dim dcCREATEDBy As New DataColumn
            dcCREATEDBy.ColumnName = "CREATEDBY"
            dcCREATEDBy.DefaultValue = strUserName

            Dim objclsComman As New clsCommon
            Dim dcCREATEDON As New DataColumn
            dcCREATEDON.ColumnName = "CREATEDON"
            dcCREATEDON.DefaultValue = objclsComman.GetCurrentDate()


            'Added by Rohit for adding updatedad, updatedon and updated by columns to BLRequestedItems table
            Dim dcUPDATEDAT As New DataColumn
            dcUPDATEDAT.ColumnName = "UPDATEDAT"
            dcUPDATEDAT.DefaultValue = strSiteCode

            Dim dcUPDATEDBy As New DataColumn
            dcUPDATEDBy.ColumnName = "UPDATEDBY"
            dcUPDATEDBy.DefaultValue = strUserName

            Dim dcUPDATEDON As New DataColumn
            dcUPDATEDON.ColumnName = "UPDATEDON"
            dcUPDATEDON.DefaultValue = objclsComman.GetCurrentDate()
            'Add Ends

            'Dim dcBLDiscountAmt As DataColumn = New DataColumn("BLDiscountAmt", System.Type.GetType("System.Decimal"))
            'dcBLDiscountAmt.AllowDBNull = False
            'dcBLDiscountAmt.DefaultValue = Decimal.Zero



            Dim dcCLPPoints As DataColumn = New DataColumn("CLPPoints", System.Type.GetType("System.Decimal"))
            dcCLPPoints.AllowDBNull = False
            dcCLPPoints.DefaultValue = Decimal.Zero
            dtBirthListReq.Columns.Add(dcCLPPoints)

            'Change by Ashish for CR 5679
            'Adding code for capturing SellingPrice, Original Price and IsPriceChanged flag
            Dim dcSellingPrice As DataColumn = New DataColumn("SellingPrice", System.Type.GetType("System.Decimal"))
            dcSellingPrice.AllowDBNull = True
            dcSellingPrice.DefaultValue = Decimal.Zero
            dtBirthListReq.Columns.Add(dcSellingPrice)

            Dim dcOriginalSellingPrice As DataColumn = New DataColumn("OriginalSellingPrice", System.Type.GetType("System.Decimal"))
            dcOriginalSellingPrice.AllowDBNull = True
            dcOriginalSellingPrice.DefaultValue = Decimal.Zero
            dtBirthListReq.Columns.Add(dcOriginalSellingPrice)

            Dim dcIsPriceChanged As DataColumn = New DataColumn("IsPriceChanged", System.Type.GetType("System.Boolean"))
            dcIsPriceChanged.AllowDBNull = False
            dcIsPriceChanged.DefaultValue = False
            dtBirthListReq.Columns.Add(dcIsPriceChanged)

            Dim dcSrNo As DataColumn = New DataColumn("SrNo", System.Type.GetType("System.Int32"))
            dcSrNo.AllowDBNull = False
            dcSrNo.DefaultValue = 1
            dtBirthListReq.Columns.Add(dcSrNo)
            'End of change

            'Dim dcCLPDiscount As DataColumn = New DataColumn("CLPDiscount", System.Type.GetType("System.Decimal"))
            'dcCLPDiscount.AllowDBNull = False
            'dcCLPDiscount.DefaultValue = Decimal.Zero
            'dtBirthListReq.Columns.Add(dcCLPDiscount)


            'Dim dcCLPDiscount As DataColumn = New DataColumn("CLPDiscount", System.Type.GetType("System.Decimal"))
            'dcCLPDiscount.AllowDBNull = False
            'dcCLPDiscount.DefaultValue = Decimal.Zero


            'dtBirthListReq.Columns.Add(dcCLPDiscount)
            'dtBirthListReq.Columns.Add(dcBLDiscountAmt)

            dtBirthListReq.Columns.Add(dcBirthListID)
            dtBirthListReq.Columns.Add(dcFinYear)
            dtBirthListReq.Columns.Add(dcEAN)
            dtBirthListReq.Columns.Add(dcArticleCode)
            dtBirthListReq.Columns.Add(RequstedQty)
            dtBirthListReq.Columns.Add(dcBookedQty)
            dtBirthListReq.Columns.Add(dcDeliveredQty)
            dtBirthListReq.Columns.Add(dcStatus)
            dtBirthListReq.Columns.Add(dcCLPQty)
            dtBirthListReq.Columns.Add(dcFreeText)
            dtBirthListReq.Columns.Add(dcCREATEDAT)
            dtBirthListReq.Columns.Add(dcCREATEDON)
            dtBirthListReq.Columns.Add(dcCREATEDBy)
            dtBirthListReq.Columns.Add(dcUPDATEDAT)
            dtBirthListReq.Columns.Add(dcUPDATEDON)
            dtBirthListReq.Columns.Add(dcUPDATEDBy)

            dtBirthListReq.Columns.Add(dcSiteCode)
            dtBirthListReq.Columns.Add(dcReservedQty)
            dtBirthList.TableName = "BirthList"

            CopyRow(dtBirthListReq)

            Return dsBirthListReq
        Catch ex As Exception
            LogException(ex)
            Return dsBirthListReq
        End Try
    End Function

    ''' <summary>
    ''' Copy row
    ''' </summary>
    ''' <param name="dtDestination"></param>
    ''' <returns>Boolean</returns>
    ''' <usedby>frmBirthList.vb</usedby>
    ''' <remarks></remarks>
    Private Function CopyRow(ByVal dtDestination As DataTable) As Boolean
        Try


            If DataTableOriginal.Rows.Count > 0 Then

                'DataTableOriginal.Merge(dtGridItem, True)
                For Each drow As DataRow In dtGridItem.Rows
                    'If (drow.RowState = DataRowState.Deleted) Then
                    Console.WriteLine(drow.RowState.ToString())

                    dtDestination.ImportRow(drow)
                    Console.WriteLine(drow.RowState.ToString())

                    'End If
                Next
                For Each dr As DataRow In dtDestination.Rows
                    Console.WriteLine(dr.RowState.ToString())
                Next
            Else
                For Each drow As DataRow In dtGridItem.Rows
                    'If (drow.RowState = DataRowState.Deleted) Then
                    dtDestination.Columns("BirthListID").DefaultValue = BirthListID
                    dtDestination.Columns("SellingPrice").DefaultValue = drow("Rate")
                    Dim Clp As Boolean = IIf(drow("IsCLP") Is DBNull.Value, False, drow("IsCLP"))
                    dtDestination.ImportRow(drow)
                    'End If
                    dtDestination.Rows(dtDestination.Rows.Count - 1)("IsCLP") = Clp
                Next



                'Change by Ashish for CR 5679
                For Each drow As DataRow In dtDestination.Rows
                    If Not String.IsNullOrEmpty(drow("IsPriceChanged")) Then
                        If CBool(drow("IsPriceChanged")) Then
                            drow("OriginalSellingPrice") = drow("SellingPrice")
                        End If
                    End If

                Next
                'End of change
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    ''' <summary>
    ''' Create new birthlist number 
    ''' </summary>   
    ''' <usedby>frmBirthList.vb</usedby>
    ''' <returns>String (birthlistid)</returns>
    ''' <remarks></remarks>

    Public Function Generate_BirthListNumber(ByVal strTerminalId As String, ByVal OnlineConnect As Boolean) As String
        Dim objclsComman As New clsCommon

        If OnlineConnect Then
            'Changed by Rohit to generate Document No. for proper sorting
            Try
                BirthListID = "BL" + strTerminalId & FinacialYear.Substring(FinacialYear.Length - 2, 2)
                BirthListID = GenDocNo(BirthListID, 15, objclsComman.getDocumentNo("BirthList", SiteCode))
            Catch ex As Exception
                BirthListID = "BL" & strTerminalId & objclsComman.getDocumentNo("BirthList", SiteCode)
            End Try
            'End Change by Rohit
        Else
            'Changed by Rohit to generate Document No. for proper sorting
            Try
                BirthListID = "OBL" + strTerminalId & FinacialYear.Substring(FinacialYear.Length - 2, 2)
                BirthListID = GenDocNo(BirthListID, 15, objclsComman.getDocumentNo("BirthList", SiteCode))
            Catch ex As Exception
                BirthListID = "OBL" & strTerminalId & objclsComman.getDocumentNo("BirthList", SiteCode)
            End Try
            'End Change by Rohit
        End If

        Return BirthListID
    End Function
    ''' <summary> 
    ''' After successfully edit row ,update datasource
    ''' </summary>
    ''' <param name="colName"></param>
    ''' <param name="rowIndex"></param>
    ''' <param name="strChangedValue"></param>
    ''' <returns>DataTable</returns>
    ''' <usedby>frmBirthList.vb</usedby>
    ''' <remarks></remarks>
    Public Function UpdateDataInGrid(ByVal colName As String, ByVal rowIndex As Integer, ByVal strChangedValue As String) As DataTable
        Try
            dtGridItem.Rows(rowIndex).BeginEdit()
            dtGridItem.Rows(rowIndex)(colName) = strChangedValue
            Dim currentAmount As Decimal
            If ((dtGridItem.Columns(colName).ColumnName = "RequstedQty") Or (dtGridItem.Columns(colName).ColumnName = "Rate")) Then
                currentAmount = dtGridItem.Rows(rowIndex)("Rate")
                'dtGridItem.Rows(rowIndex)("Amount") = currentAmount * strChangedValue
                dtGridItem.Rows(rowIndex)("Amount") = currentAmount * (dtGridItem.Rows(rowIndex)("RequstedQty"))
                'Change by Ashish for CR 5679
                If dtGridItem.Rows(rowIndex)("Rate") <> dtGridItem.Rows(rowIndex)("OriginalSellingPrice") Then
                    dtGridItem.Rows(rowIndex)("IsPriceChanged") = True
                Else
                    dtGridItem.Rows(rowIndex)("IsPriceChanged") = False
                End If
                'End of change
            End If
            dtGridItem.Rows(rowIndex).EndEdit()
            'dtGridItem.AcceptChanges()
            'dsDisplay.AcceptChanges()
            Return dtGridItem
        Catch ex As Exception
            LogException(ex)
            Return dtGridItem
        End Try
    End Function
    ''' <summary>
    ''' Find article code of selected grid row 
    ''' </summary>
    ''' <param name="drowArticleDetail"></param>
    ''' <param name="strArticleEANCode"></param>
    ''' <returns>Boolean</returns>
    ''' <usedby>frmBirthList.vb</usedby>
    ''' <remarks></remarks>
    Public Function FindArticleCode(ByRef dtArticleDetail As DataTable, ByVal strArticleEANCode As String, ByVal strLocalSite As String, ByVal EANType As String, ByVal strLanguageCOde As String) As Boolean
        Dim sdrArticleDetail As New SqlDataAdapter
        Dim dtArticleDetails As New DataTable
        Try
            Dim objiteamSearch As New clsIteamSearch
            dtArticleDetails = objiteamSearch.GetEANData(strLocalSite, strArticleEANCode, strLanguageCOde)
            'If Not dtArticleDetails Is Nothing AndAlso dtArticleDetails.Rows.Count > 0 Then
            '    dtArticleDetail = dtArticleDetails
            '    Return True
            'Else
            '    Return False
            'End If

            If dtArticleDetails.Rows.Count > 1 Then



                Dim dvEan As New DataView(dtArticleDetails, "EAN='" & strArticleEANCode & "'", "", DataViewRowState.CurrentRows)
                If dvEan.Count > 0 Then
                    dvEan.RowFilter = "EAN <>'" & strArticleEANCode & "'"
                    If dvEan.Count > 0 Then
                        dvEan.AllowDelete = True
                        For Each dr As DataRowView In dvEan
                            dr.Delete()
                        Next
                        dtArticleDetails.AcceptChanges()
                        dtArticleDetail = dtArticleDetails
                        If Not dtArticleDetail Is Nothing AndAlso dtArticleDetail.Rows.Count > 0 Then
                            Return False
                        Else
                            Dim clp As New CLP_Logic

                            If Not clp.getclphierarchy(dtArticleDetail.Rows(0)("ArticleCode"), strLocalSite) Then
                                dtArticleDetail.Rows(0)("CLPRequire") = False
                            End If
                            Return True
                        End If
                    Else
                        dtArticleDetail = dtArticleDetails
                        Return True

                    End If
                Else

                    Dim dv As New DataView(dtArticleDetails, "DefaultEAN <> 1", "", DataViewRowState.CurrentRows)
                    'Dim dv As New DataView(dtArticleDetails, "EanType <> '" & EANType & "'", "", DataViewRowState.CurrentRows)
                    If dv.Count > 0 Then
                        dv.AllowDelete = True
                        For Each dr As DataRowView In dv
                            dr.Delete()
                        Next
                        dtArticleDetails.AcceptChanges()
                        dtArticleDetail = dtArticleDetails
                        If Not dtArticleDetail Is Nothing AndAlso dtArticleDetail.Rows.Count < 0 Then
                            Return False
                        Else
                            Return True
                        End If
                    Else
                        dtArticleDetails.AcceptChanges()
                        dtArticleDetail = dtArticleDetails
                        If Not dtArticleDetail Is Nothing AndAlso dtArticleDetail.Rows.Count < 0 Then
                            Return False
                        Else
                            Dim clp As New CLP_Logic

                            If clp.getclphierarchy(dtArticleDetail.Rows(0)("ArticleCode"), strLocalSite) Then
                                dtArticleDetail.Rows(0)("CLPRequire") = True
                                dtArticleDetail.AcceptChanges()
                            End If
                            Return True
                        End If
                    End If
                End If
            End If
            'Dim rowColl1() As DataRow = dtArticleDetails.Select("EanType='" & EANType & "'", "", DataViewRowState.CurrentRows)
            'If (rowColl1.Length > 0) Then
            '    For Each row1 As DataRow In rowColl1
            '        drowArticleDetail = row1
            '        Return True
            '    Next
            'End If


            If dtArticleDetails.Rows.Count > 0 Then
                dtArticleDetail = dtArticleDetails
                Return True
            Else
                Return False
            End If



        Catch ex As Exception
            LogException(ex)
            Return False
        Finally

        End Try
    End Function
    ''' <summary>
    ''' Calculate total ordered items
    ''' </summary>
    ''' <param name="strTotalItem"></param>
    ''' <param name="iOrderItem"></param>
    ''' <param name="decAmount"></param>
    ''' <param name="siteCode"></param>
    ''' <param name="strlocalCurrencyIndex"></param>
    '''  <usedby>frmBirthList.vb</usedby>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function CalculateRecieptItem(ByRef strTotalItem As String, ByRef iOrderItem As Integer, ByRef decAmount As Decimal, ByVal siteCode As String, ByVal strlocalCurrencyIndex As String, ByRef iReservedQty As Integer) As Boolean
        Try
            strTotalItem = dtGridItem.Rows.Count
            iOrderItem = IIf(dtGridItem.Compute("Sum(RequstedQty)", " ") Is DBNull.Value, 0, dtGridItem.Compute("Sum(RequstedQty)", " "))
            decAmount = CalculateTotalAmount_Incurrency(IIf(dtGridItem.Compute("Sum(Amount)", " ") Is DBNull.Value, 0, dtGridItem.Compute("Sum(Amount)", " ")), siteCode, strlocalCurrencyIndex)

            iReservedQty = IIf(dtGridItem.Compute("Sum(ReservedQty)", " ") Is DBNull.Value, 0, dtGridItem.Compute("Sum(ReservedQty)", " "))
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Calculate total amount w.r.t currency
    ''' </summary>
    ''' <param name="dec"></param>
    ''' <param name="siteCode"></param>
    ''' <param name="strlocalCurrencyIndex"></param>
    ''' <usedby>frmBirthList.vb</usedby>
    ''' <returns>Decimal(TotalAmount)</returns>
    ''' <remarks></remarks>
    Public Function CalculateTotalAmount_Incurrency(ByVal dec As Decimal, ByVal siteCode As String, ByVal strlocalCurrencyIndex As String) As Decimal
        Try
            Dim descCurrencyRate As Decimal
            objclsAcceptPayment.CalculateTotalBillAmount_InCurrency(dec, strlocalCurrencyIndex, descCurrencyRate, strlocalCurrencyIndex)
            Return dec * descCurrencyRate
        Catch ex As Exception
            LogException(ex)
            Return Decimal.Zero
        End Try
    End Function
    ''' <summary>
    '''  Retieve information from database 
    ''' </summary>
    ''' <param name="strQuery"></param>
    ''' <param name="strErrorMsg"></param>
    ''' <returns>DataTable</returns>
    ''' <usedby>frmBirthList.vb</usedby>
    ''' 
    ''' <remarks></remarks>
    Public Function RetrieveQuery(ByVal strQuery As String, ByRef strErrorMsg As String) As DataTable
        Dim dtQueryData As New DataTable
        Dim cmdRetrieveQuery As New SqlCommand
        Dim adt As New SqlDataAdapter
        Try
            OpenConnection()
            cmdRetrieveQuery.CommandText = strQuery
            cmdRetrieveQuery.Connection = SpectrumCon
            adt.SelectCommand = cmdRetrieveQuery
            adt.Fill(dtQueryData)
            Return dtQueryData
        Catch ex As Exception
            LogException(ex)
            strErrorMsg = "RetrieveQuery Failed."
            Return dtQueryData
        Finally
            CloseConnection()
            strErrorMsg = String.Empty
        End Try
    End Function



    Public Sub New(ByVal strFinacialYear As String)
        FinacialYear = strFinacialYear
    End Sub
End Class
