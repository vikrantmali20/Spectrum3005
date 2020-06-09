Imports System.Data
Imports System.Data.SqlClient
Imports SpectrumBL
Public Class clsBirthListGobal
    Private _TranscationType As BirthListTransaction
    Public Enum BirthListTransaction
        BirthListCreate
        BirthListUpdate
        BirthListSales
    End Enum
    Public Enum BirthListStatus
        Open = 1
        Close
        Cancel
    End Enum
    ''' <summary>
    ''' Customer Type 
    ''' <UsedBy>clsBirthList.vb,frmBirthListCreate</UsedBy>
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CustomerTypeName
        SO = 1
        CLP = 2
    End Enum




    Private Property BirthListTransactionType() As BirthListTransaction
        Get
            Return _TranscationType
        End Get
        Set(ByVal value As BirthListTransaction)
            _TranscationType = value
        End Set
    End Property



    ''' <summary>
    '''  Add columns to datatable 
    ''' </summary>
    ''' <param name="dtQueryData"></param>
    ''' <usedby>frmBirthListSales.vb,frmBirthListUpdate.vb</usedby>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function CreateBirthListItemTable(ByVal dtQueryData As DataTable) As DataTable
        Dim dtGridItem As New DataTable
        Dim dcBalanceQty As New DataColumn
        Dim dcNetAmount As New DataColumn
        Dim dcPurchaseQty As New DataColumn
        Dim dcPickUpQty As New DataColumn
        Dim dcTotalNetAmt As New DataColumn
        'Dim dcUNITOFMEASURE As New DataColumn("UNITOFMEASURE")


        dcPurchaseQty.ColumnName = "PurchasedQty"
        dcPurchaseQty.DefaultValue = 0

        dcPurchaseQty.DataType = System.Type.GetType("System.Int32")

        dcBalanceQty.DataType = System.Type.GetType("System.Int32")
        dcPickUpQty.DataType = System.Type.GetType("System.Int32")
        dcPickUpQty.ColumnName = "PickUpQty"
        dcPickUpQty.DefaultValue = 0
        dcNetAmount.ColumnName = "NetAmount"
        dcTotalNetAmt.ColumnName = "TotalNetAmount"

        dcBalanceQty.ColumnName = "BalanceItemQty"
        dcBalanceQty.DataType = System.Type.GetType("System.Int32")
        dcNetAmount.DataType = System.Type.GetType("System.Decimal")
        dtGridItem.Columns.Add(dcBalanceQty)
        dtGridItem.Columns.Add(dcNetAmount)

        dtGridItem.Columns.Add(dcPurchaseQty)
        dtGridItem.Columns.Add(dcPickUpQty)


        Try
            dcTotalNetAmt.DataType = System.Type.GetType("System.Decimal")

            dtGridItem.Columns.Add(dcTotalNetAmt)
            Dim dcEXCLUSIVETAX As New DataColumn
            dcEXCLUSIVETAX.ColumnName = "EXCLUSIVETAX"
            Dim dcTOTALTAXAMOUNT As New DataColumn
            dcTOTALTAXAMOUNT.DataType = System.Type.GetType("System.Decimal")
            dcTOTALTAXAMOUNT.ColumnName = "TaxAmt"
            dcTOTALTAXAMOUNT.DefaultValue = Decimal.Zero
            Dim dcIsPurchasedQtyUpdate As New DataColumn
            dcIsPurchasedQtyUpdate.ColumnName = "IsPurchaseQtyUpdate"
            dcIsPurchasedQtyUpdate.DataType = System.Type.GetType("System.Boolean")
            dcIsPurchasedQtyUpdate.DefaultValue = True
            dtGridItem.Columns.Add(dcIsPurchasedQtyUpdate)
            dtGridItem.Columns.Add(dcEXCLUSIVETAX)
            dtGridItem.Columns.Add(dcTOTALTAXAMOUNT)
            'dtGridItem.Columns.Add(dcUNITOFMEASURE)
        Catch ex As Exception
            LogException(ex)
        End Try


        dtGridItem.Merge(dtQueryData)
        dtGridItem.Columns("TotalNetAmount").Expression = "SellingPrice * RequstedQty"
        Return dtGridItem
    End Function
    ''' <summary>
    ''' Calculate Total Balance Quantity
    ''' </summary>
    ''' <param name="dtBirthListItem"></param>
    ''' <param name="IsCalculateNetAmount"></param>
    ''' <param name="strErrorMsg"></param>
    ''' <usedby>frmBirthListSales.vb,frmBirthListUpdate.vb</usedby>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function CalculateTotalBalanceQty(ByVal dtBirthListItem As DataTable, Optional ByVal IsCalculateNetAmount As Boolean = False, Optional ByRef strErrorMsg As String = "") As DataTable
        Try
            Dim iPurchasedQty As Integer
            Dim iReservedQty As Integer
            Dim irequstedqty As Integer
            Dim ibookedqty As Integer = 0
            Dim ideliveredqty As Integer = 0
            For Each drRow As DataRow In dtBirthListItem.Rows
                drRow.BeginEdit()
                'Changes for CR 5679
                'fixed an existing bug related to null values in purchase qty, delivered qty
                If IsDBNull(drRow("PickUpQty")) Then
                    drRow("PickUpQty") = 0
                End If
                If IsDBNull(drRow("PurchasedQty")) Then
                    drRow("PurchasedQty") = 0
                End If
                If IsDBNull(drRow("deliveredqty")) Then
                    drRow("deliveredqty") = 0
                End If
                'end of change
                iPurchasedQty = drRow("PurchasedQty")
                irequstedqty = drRow("requstedqty")
                If Not (drRow("bookedqty") Is DBNull.Value) Then
                    If Not (drRow("ReservedQty") Is DBNull.Value) Then
                        ibookedqty = drRow("bookedqty")
                    Else
                        drRow("ReservedQty") = 0
                        ibookedqty = drRow("bookedqty")
                    End If
                End If
                If Not (drRow("deliveredqty") Is DBNull.Value) Then
                    ideliveredqty = drRow("deliveredqty")
                End If
                If Not (drRow("ReturnQty") Is DBNull.Value) Then
                    drRow("BalanceItemQty") = irequstedqty - (iPurchasedQty + ibookedqty)
                Else
                    drRow("BalanceItemQty") = irequstedqty - (iPurchasedQty + ibookedqty)

                End If

                If (IsCalculateNetAmount) Then  ' BirthListUpdate form
                    If Not drRow("SellingPrice") Is DBNull.Value And Not drRow("PurchasedQty") Is DBNull.Value Then
                        drRow("NetAmount") = drRow("SellingPrice") * drRow("PurchasedQty")
                    Else
                        drRow("NetAmount") = DBNull.Value
                    End If
                End If
                drRow.EndEdit()
            Next
            Return dtBirthListItem
            'Dim iPurchasedQty As Integer
            'Dim irequstedqty As Integer
            'Dim ibookedqty As Integer = 0
            'Dim ideliveredqty As Integer = 0
            'For Each drRow As DataRow In dtBirthListItem.Rows
            '    drRow.BeginEdit()
            '    iPurchasedQty = drRow("PurchasedQty")
            '    irequstedqty = drRow("requstedqty")
            '    If Not (drRow("bookedqty") Is DBNull.Value) Then
            '        If Not (drRow("ReservedQty") Is DBNull.Value) Then
            '            ibookedqty = drRow("bookedqty") + drRow("ReservedQty")
            '        Else
            '            drRow("ReservedQty") = 0
            '            ibookedqty = drRow("bookedqty") + drRow("ReservedQty")
            '        End If
            '    End If
            '    If Not (drRow("deliveredqty") Is DBNull.Value) Then
            '        ideliveredqty = drRow("deliveredqty")
            '    End If
            '    If Not (drRow("ReturnQty") Is DBNull.Value) Then
            '        drRow("BalanceItemQty") = irequstedqty - (iPurchasedQty + ibookedqty)
            '    Else
            '        drRow("BalanceItemQty") = irequstedqty - (iPurchasedQty + ibookedqty)

            '    End If

            '    If (IsCalculateNetAmount) Then  ' BirthListUpdate form
            '        If Not drRow("SellingPrice") Is DBNull.Value And Not drRow("BookedQty") Is DBNull.Value Then
            '            drRow("NetAmount") = drRow("SellingPrice") * drRow("BookedQty")
            '        Else
            '            drRow("NetAmount") = DBNull.Value
            '        End If
            '    End If
            '    drRow.EndEdit()
            'Next
            'Return dtBirthListItem
            strErrorMsg = String.Empty
        Catch ex As Exception
            LogException(ex)
            strErrorMsg = "Problem in calculation of balanceqty"
            Return dtBirthListItem

        Finally

        End Try


    End Function
    ''' <summary>
    '''  Adding new item into BirthList Requested item list 
    ''' </summary>
    ''' <UsedBY>frmBirthListSales.vb</UsedBY>
    ''' <param name="strSiteCode"></param>
    ''' <param name="strUserName"></param>
    ''' <param name="strBirthListID"></param>
    ''' <param name="dtItemInfo"></param>
    ''' <param name="drNewItemInfo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function AddNewItemRow(ByVal strSiteCode As String, ByVal strUserName As String, ByVal strBirthListID As String, ByRef dtItemInfo As DataTable, ByVal drNewItemInfo As DataRow, Optional ByRef strErrorMsg As String = "", Optional ByVal isArticlZeroTaxAllowed As Boolean = True, Optional ByVal dtMainTax As DataTable = Nothing, Optional ByVal PrevFinYear As String = "2011") As Boolean
        Try
            Dim clp As New CLP_Logic
            Dim objclsComman As New clsCommon
            'Dim dcKey(1) As DataColumn
            'dcKey(0) = dtItemInfo.Columns("EAN")
            'dtItemInfo.PrimaryKey = dcKey
            Dim dtNew As New DataTable()
            Dim obj As New clsBirthListSalesSave
            dtNew = dtItemInfo.Clone()
            If Not (IsItemAdded(dtMainTax, strSiteCode, dtItemInfo, drNewItemInfo.Item("EAN"), isArticlZeroTaxAllowed)) Then
                Dim NewRow As DataRow = dtNew.NewRow()
                'NewRow.Item("FinYear") = PrevFinYear
                NewRow.Item("SiteCode") = strSiteCode
                NewRow.Item("BirthListID") = strBirthListID
                NewRow.Item("ArticleCode") = drNewItemInfo.Item("ArticleCode")
                NewRow.Item("EAN") = drNewItemInfo.Item("EAN")
                NewRow.Item("UNITOFMEASURE") = drNewItemInfo.Item("UOM")
                NewRow.Item("DISCRIPTION") = drNewItemInfo.Item("DISCRIPTION")
                NewRow.Item("SellingPRICE") = drNewItemInfo.Item("SellingPRICE")
                NewRow.Item("IsPurchaseQtyUpdate") = False

                If clp.getclphierarchy(drNewItemInfo.Item("ArticleCode"), strSiteCode) Then
                    NewRow.Item("CLPRequire") = True
                Else
                    NewRow.Item("CLPRequire") = False
                End If
                '    drSelectedItemDetails.BeginEdit()
                '    drSelectedItemDetails("CLPRequire") = True
                '    drSelectedItemDetails.EndEdit()
                '    grdScanItem.Update()
                'NewRow.Item("CLPRequire") = False
                If NewRow.Item("IsPriceChangedHere") Is DBNull.Value Then
                    NewRow.Item("IsPriceChangedHere") = False
                End If


                If BirthListTransactionType = BirthListTransaction.BirthListSales Then
                    NewRow.Item("RequstedQty") = 1
                    NewRow.Item("BookedQty") = 0
                    NewRow.Item("PurchasedQty") = 1
                    NewRow.Item("BalanceItemQty") = 0
                    NewRow.Item("FreezeSB") = drNewItemInfo.Item("FreezeSB")
                    NewRow.Item("FreezeOB") = drNewItemInfo.Item("FreezeOB")
                Else
                    NewRow.Item("RequstedQty") = 1
                    NewRow.Item("BookedQty") = 0
                End If
                NewRow.Item("DeliveredQty") = 0
                NewRow.Item("ReservedQty") = 0
                NewRow.Item("ConVToVoucher") = 0

                Dim decTotalAmount = (NewRow.Item("SellingPrice") * NewRow.Item("PurchasedQty"))
                If (CreateDataSetForTaxCalculation(dtMainTax, strSiteCode, NewRow("ArticleCode"), decTotalAmount, NewRow, NewRow("EAN").ToString()) Is Nothing) Then
                    If Not (isArticlZeroTaxAllowed) Then
                        strErrorMsg = "Tax Not Found"
                        Return False
                    End If
                End If
                If Not NewRow.Item("EXCLUSIVETAX") Is Nothing And Not NewRow.Item("EXCLUSIVETAX") Is DBNull.Value Then
                    NewRow.Item("NetAmount") = decTotalAmount + NewRow.Item("EXCLUSIVETAX")
                Else
                    NewRow.Item("EXCLUSIVETAX") = Decimal.Zero
                    NewRow.Item("NetAmount") = decTotalAmount + NewRow.Item("EXCLUSIVETAX")
                End If

                If (BirthListTransactionType = BirthListTransaction.BirthListSales) Then
                    NewRow.Item("Createdon") = objclsComman.GetCurrentDate()
                    NewRow.Item("CreatedBY") = strUserName
                    NewRow.Item("Createdat") = strSiteCode
                    NewRow.Item("UPDATEDat") = strSiteCode
                    NewRow.Item("UPDATEDON") = objclsComman.GetCurrentDate()
                    NewRow.Item("UPDATEDBY") = strUserName
                    NewRow.Item("Status") = True
                End If
                NewRow.Item("AvailableQty") = objclsComman.GetStocks(strSiteCode, drNewItemInfo.Item("EAN"), drNewItemInfo.Item("ArticleCode"), True)
                NewRow.Item("SrNo") = obj.GetSrNo(strBirthListID, NewRow.Item("ArticleCode"), NewRow.Item("Ean")) + 1
                dtNew.Rows.Add(NewRow)
                dtNew.Merge(dtItemInfo)
                dtItemInfo = dtNew
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function
    ''' <summary>
    ''' Assign Tax to the Row
    ''' </summary>
    ''' <param name="strMatcode">Article Code</param>
    ''' <param name="TaxableAmount">Amount on which Tax calulated </param>
    ''' <param name="dr"> Data Row</param>
    ''' <param name="EAN">Ean Code</param>
    ''' <returns>Tax amount</returns>
    ''' <remarks></remarks>
    Public Function CreateDataSetForTaxCalculation(ByVal dtMainTax As DataTable, ByVal strSiteCode As String, ByVal strMatcode As String, ByVal TaxableAmount As Double, ByRef dr As DataRow, Optional ByVal EAN As String = "") As Object
        Try
            Dim objClsComman As New clsCommon
            If dtMainTax Is Nothing Then
                dtMainTax = objClsComman.getTax(strSiteCode, "", "", 0, "")
                dtMainTax.TableName = "BirthListTax"
            End If

            Dim StrTaxCode As Double = 0
            Dim dtTaxCalc As DataTable
            dtTaxCalc = New DataTable
            Dim ExlusiveFound As Boolean = False
            Dim dvTax As New DataView(dtMainTax, "ArticleCode='" & strMatcode & "'", "StepNo", DataViewRowState.CurrentRows)
            If dvTax.Count > 0 Then
                dtTaxCalc = dvTax.ToTable()
                dvTax.AllowDelete = True
                Dim i As Integer
                For i = dvTax.Count - 1 To 0 Step -1
                    dvTax.Delete(i)
                Next
            Else
                dtTaxCalc = objClsComman.getTax(strSiteCode, strMatcode, "BLS", dr("PurchasedQty"), EAN)
            End If
            Dim dbIncTotalTax As Double = 0
            Dim dbExclTotalTax As Double = 0
            If dtTaxCalc.Rows.Count = 0 Then
                Return Nothing
            End If
            If dtTaxCalc.Rows.Count <> 0 Then
                dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount
                objClsComman.getCalculatedDataSet(dtTaxCalc)
                Dim iRowTax As Int16
                With dtTaxCalc
                    For iRowTax = 0 To dtTaxCalc.Rows.Count - 1
                        If CDbl(dtTaxCalc.Rows(iRowTax)("VALUE").ToString) <> 0 Then
                            If dtTaxCalc.Rows(iRowTax)("INCLUSIVE") = "0" Then
                                dbExclTotalTax = dbExclTotalTax + CDbl(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT"))

                            Else
                                dbIncTotalTax = dbIncTotalTax + CDbl(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT"))

                            End If
                        End If
                    Next
                End With
                dr("EXCLUSIVETAX") = dbExclTotalTax
                StrTaxCode = dtTaxCalc.Compute("SUM(TAXAMOUNT)", "")
                dr("TaxAmt") = Math.Round(StrTaxCode, 2)

                dtMainTax.Merge(dtTaxCalc, False, MissingSchemaAction.Ignore)
                dtMainTax.AcceptChanges()
                Return StrTaxCode
            End If
        Catch ex As Exception


            LogException(ex)
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' TODO:Check Is Already item added into grid or not
    ''' </summary>
    ''' <returns>Boolean</returns>
    '''  <usedby>frmBirthList.vb</usedby>
    ''' <remarks></remarks>
    Public Function IsItemAdded(ByVal dtMainTax As DataTable, ByVal strSiteCode As String, ByRef dtItemInfo As DataTable, ByVal strEANNumber As String, ByVal isArticlZeroTaxAllowed As Boolean) As Boolean
        Try

            For Each dr As DataRow In dtItemInfo.Select("EAN='" & strEANNumber & "'", "ArticleCode", DataViewRowState.CurrentRows)

                Dim iRequstedQty As Integer = dr("RequstedQty")

                'If (dr("RequstedQty") - (dr.Item("BookedQty") + dr.Item("ReservedQty") + dr.Item("PurchasedQty"))) = 0 Then
                If (dr("RequstedQty") - (dr.Item("BookedQty") + dr.Item("PurchasedQty"))) = 0 Then
                    If Not iRequstedQty + 1 > MaxQuantity Then
                        dr("RequstedQty") = iRequstedQty + 1

                        dr.Item("IsPurchaseQtyUpdate") = False
                        'Return True
                    Else
                        MsgBox("Quantity is invalid ")
                        Return False
                    End If


                End If
                Dim iPurchaseQty As Integer = dr("PurchasedQty")
                dr.Item("PurchasedQty") = iPurchaseQty + 1
                iPurchaseQty = dr("PurchasedQty")
                If Not (BirthListTransactionType = BirthListTransaction.BirthListSales) Then
                    'In "BirthListSales" this functinality is not required.
                    dr.Item("BookedQty") = 0
                ElseIf (iRequstedQty < iPurchaseQty) Then
                    dr("RequstedQty") = iRequstedQty + 1
                    dr.Item("IsPurchaseQtyUpdate") = False
                End If

                If (dr("RequstedQty") - (dr.Item("BookedQty") + dr.Item("PurchasedQty"))) < 0 Then

                    dr("RequstedQty") = iRequstedQty + 1

                End If

                'dr.Item("PurchasedQty") = dr("RequstedQty") - (dr.Item("BookedQty") + dr.Item("ReservedQty"))
                'dr.Item("BookedQty") = dr.Item("BookedQty") + 1

                Dim decTotalAmount = (dr.Item("SellingPrice") * dr.Item("PurchasedQty"))

                If (CreateDataSetForTaxCalculation(dtMainTax, strSiteCode, dr("ArticleCode"), decTotalAmount, dr, dr("EAN").ToString()) Is Nothing) Then
                    If Not (isArticlZeroTaxAllowed) Then
                        Return False
                    End If
                End If

                If Not dr.Item("EXCLUSIVETAX") Is Nothing And Not dr.Item("EXCLUSIVETAX") Is DBNull.Value Then
                    dr.Item("NetAmount") = decTotalAmount + dr.Item("EXCLUSIVETAX")
                Else
                    dr.Item("EXCLUSIVETAX") = Decimal.Zero
                    dr.Item("NetAmount") = decTotalAmount + dr.Item("EXCLUSIVETAX")
                End If


                Return True
            Next
        Catch ex As Exception
            LogException(ex)
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
    Public Function FindArticleCode(ByRef drowArticleDetail As DataRow, ByVal strArticleEANCode As String, ByVal strLocalSiteCode As String, ByVal EANType As String) As Boolean
        Dim sdrArticleDetail As New SqlDataAdapter
        Dim dtArticleDetails As New DataTable
        Try
            Dim objiteamSearch As New clsIteamSearch
            dtArticleDetails = objiteamSearch.GetEANData(strLocalSiteCode)
            Dim rowColl() As DataRow = dtArticleDetails.Select("EAN='" & strArticleEANCode & "'", "", DataViewRowState.CurrentRows)
            If (rowColl.Length > 0) Then
                For Each row1 As DataRow In rowColl
                    drowArticleDetail = row1
                    Return True
                Next
            Else
                Dim rowColl2() As DataRow = dtArticleDetails.Select("DefaultEAN <> 1", "", DataViewRowState.CurrentRows)
                'Dim rowColl2() As DataRow = dtArticleDetails.Select("EanType='" & EANType & "'", "", DataViewRowState.CurrentRows)
                If (rowColl.Length > 0) Then
                    For Each row1 As DataRow In rowColl2
                        drowArticleDetail = row1
                        Return True
                    Next
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
        End Try
    End Function

    Public Sub New()

    End Sub
    ''' <summary>
    ''' Add new row to grid use this construdtor ,
    ''' </summary>
    ''' <param name="BirthListTransactionType">i.e BirthListUpdate,BirthListSales </param>
    ''' <remarks></remarks>
    Public Sub New(ByVal BirthListTransactionType)
        _TranscationType = BirthListTransactionType
    End Sub

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
            strErrorMsg = "RetrieveQuery Failed."
            LogException(ex)
            Return dtQueryData
        Finally
            CloseConnection()
            strErrorMsg = String.Empty
        End Try
    End Function


    ''' <summary>
    '''  Retieve information from database 
    ''' </summary>
    ''' <param name="strQuery"></param>
    ''' <param name="strErrorMsg"></param>
    ''' <returns>DataSet</returns>
    ''' <usedby>frmBirthList.vb</usedby>
    ''' 
    ''' <remarks></remarks>
    Public Function RetrieveDataset(ByVal strQuery As String, ByRef strErrorMsg As String) As DataSet
        Dim dsQueryData As New DataSet
        Dim cmdRetrieveQuery As New SqlCommand
        Dim adt As New SqlDataAdapter
        Try
            OpenConnection()
            cmdRetrieveQuery.CommandText = strQuery
            cmdRetrieveQuery.Connection = SpectrumCon
            adt.SelectCommand = cmdRetrieveQuery
            adt.Fill(dsQueryData)
            Return dsQueryData
        Catch ex As Exception
            strErrorMsg = "RetrieveQuery Failed."
            LogException(ex)
            Return dsQueryData
        Finally
            CloseConnection()
            strErrorMsg = String.Empty
        End Try
    End Function
    ''' <summary>
    '''  Inserting 
    ''' </summary>
    ''' <param name="strQuery"></param>
    ''' <param name="tra"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertQuery(ByVal strQuery As String, ByVal tra As SqlTransaction) As Boolean
        Try
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter()
            Dim cmdInsertQuery As New SqlCommand(strQuery)
            cmdInsertQuery.Transaction = tra
            cmdInsertQuery.Connection = SpectrumCon
            da.InsertCommand = cmdInsertQuery
            cmdInsertQuery.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Function BirthListCLPCalculation(ByVal TerminalId As String, ByVal Online As Boolean, ByVal _dtBirthListItemDetail As DataTable, ByVal strSiteCode As String, Optional ByVal strCLPProgramID As String = "", Optional ByVal strCustomerID As String = "", Optional ByRef IsTransaction As Boolean = True, Optional ByVal posDbBirthListRequested As POSDBDataSet.BirthListRequestedItemsDataTable = Nothing, Optional ByVal posAdaptorBirthListReq As POSDBDataSetTableAdapters.BirthListRequestedItemsTableAdapter = Nothing, Optional ByVal dtCurrent As DateTime = Nothing, Optional ByVal strFinacialYear As String = "") As Boolean
        Try
            Dim objclscomman As New clsCommon
            Dim strErroMsg As String = String.Empty



            Dim strQueryCLP As String = String.Format("select *  from CLPCustomers where CardNo= {0} and ClpProgramId= '{1}' ", strCustomerID, strCLPProgramID)

            Dim dsCLP As DataSet = RetrieveDataset("Select * from   CLPTransaction where 1=0  select * from CLPTransactionsDetails where 1=0 " & strQueryCLP, strErroMsg)

            dsCLP.Tables(0).TableName = "CLPTransaction"
            dsCLP.Tables(1).TableName = "CLPTransactionsDetails"
            dsCLP.Tables(2).TableName = "CLPCustomers"
            Dim objClsBirthListSave As New clsBirthListSalesSave
            objClsBirthListSave.FinacialYear = strFinacialYear
            Dim salesInvoice As String = objClsBirthListSave.GenNewSaleInVoiceNumber(TerminalId, Online)
            dsCLP.Tables("CLPTransactionsDetails").Columns("BillNo").DefaultValue = salesInvoice
            dsCLP.Tables("CLPTransaction").Columns("BillNo").DefaultValue = salesInvoice
            If strErroMsg = String.Empty Then
                Dim drCLPTransaction As DataRow
                Dim decCLPPoints As Decimal
                If _dtBirthListItemDetail.Rows.Count > 0 Then
                    'decCLPPoints = _dtBirthListItemDetail.Compute("CLPPoints", " ")
                    'If decCLPPoints > Decimal.Zero Then
                    Dim drBirthListItemDetail As DataRow = _dtBirthListItemDetail.Rows(0)
                    drCLPTransaction = dsCLP.Tables("CLPTransaction").NewRow()
                    drCLPTransaction("SiteCode") = strSiteCode
                    'drOrderHdr("DocumentNumber") = OrderDocumentNumber()
                    'drCLPTransaction("FinYear") = objClsBirthListGlobal.FinYear
                    'drCLPTransaction("BillNo") = drBirthListI temDetail("articlecode")
                    drCLPTransaction("BillDate") = dtCurrent.Date
                    drCLPTransaction("AccumLationPoints") = drBirthListItemDetail("CLPPoints")
                    drCLPTransaction("RedemptionPoints") = 1
                    drCLPTransaction("BalAccumlationPoints") = drBirthListItemDetail("CLPPoints")
                    drCLPTransaction("ClpProgramId") = strCLPProgramID
                    'drCLPTransaction("ClpCustomerId") = CustomerID
                    drCLPTransaction("IsRedemptionProcess") = False
                    dsCLP.Tables("CLPTransaction").Rows.Add(drCLPTransaction)
                    Dim iNext As Integer = 1
                    Dim drCLPTransactionDetails As DataRow
                    For Each drBirthListItemDetailOld As DataRow In _dtBirthListItemDetail.Rows
                        If (drBirthListItemDetailOld("BookedQty") > 0) Then
                            drCLPTransactionDetails = dsCLP.Tables("CLPTransactionsDetails").NewRow()
                            drCLPTransactionDetails("SiteCode") = strSiteCode
                            'drOrderHdr("FinYear") = objClsBirthListGlobal.FinYear

                            drCLPTransactionDetails("BillLineNo") = iNext
                            drCLPTransactionDetails("ArticleCode") = drBirthListItemDetailOld("articlecode")
                            drCLPTransactionDetails("SellingPrice") = drBirthListItemDetailOld("SellingPrice")
                            drCLPTransactionDetails("Quantity") = drBirthListItemDetailOld("BookedQty")
                            drCLPTransactionDetails("CLPPoints") = drBirthListItemDetailOld("CLPPoints")
                            drCLPTransactionDetails("CLPDiscount") = drBirthListItemDetailOld("CLPDiscount")
                            drCLPTransactionDetails("EAN") = drBirthListItemDetailOld("EAN")
                            drCLPTransactionDetails("STATUS") = True
                            dsCLP.Tables("CLPTransactionsDetails").Rows.Add(drCLPTransactionDetails)
                            iNext += 1
                        End If
                    Next
                End If



                For Each dr As DataRow In dsCLP.Tables("CLPCustomers").Rows
                    dr.BeginEdit()
                    dr("SiteCode") = strSiteCode
                    dr("CardNo") = strCustomerID
                    dr("ClpProgramId") = strCLPProgramID



                    Dim objTotalBal As Object = _dtBirthListItemDetail.Compute("sum(CLPPoints)", " ")
                    Dim decTotalBal As Decimal
                    If Not objTotalBal Is Nothing Then
                        If Not objTotalBal Is DBNull.Value Then
                            decTotalBal = CDbl(objTotalBal)
                        End If
                    End If
                    Dim objPOINTSACCUMLATED As Object = dsCLP.Tables("CLPCustomers").Rows(0)("POINTSACCUMLATED")
                    Dim decPOINTSACCUMLATED As Decimal
                    If Not objPOINTSACCUMLATED Is Nothing Then
                        If Not objPOINTSACCUMLATED Is DBNull.Value Then
                            decPOINTSACCUMLATED = CDbl(objPOINTSACCUMLATED)
                        End If
                    End If

                    dr("POINTSACCUMLATED") = Decimal.Add(decTotalBal, decPOINTSACCUMLATED)
                    dr.EndEdit()
                Next


                Try
                    OpenConnection()
                    Dim sqlTran As SqlTransaction = Nothing
                    sqlTran = SpectrumCon.BeginTransaction()
                    If (objclscomman.UpdateDocumentNo("BirthListSales", SpectrumCon, sqlTran) AndAlso objclscomman.UpdateDocumentNo("SalesInvoice", SpectrumCon, sqlTran)) Then
                        If (objclscomman.SaveData(dsCLP, SpectrumCon, sqlTran)) Then
                            'If (objclscomman.fnSaveBLEdit(dsCLP, SpectrumCon, sqlTran)) Then
                            If Not posDbBirthListRequested Is Nothing Then
                                If Not posAdaptorBirthListReq Is Nothing Then
                                    posAdaptorBirthListReq.Update(posDbBirthListRequested)
                                End If
                            End If
                            sqlTran.Commit()
                            IsTransaction = True
                        Else
                            sqlTran.Rollback()
                            IsTransaction = False
                        End If
                    Else
                        sqlTran.Rollback()
                        IsTransaction = False
                    End If

                Catch ex As Exception
                    IsTransaction = False
                Finally
                    CloseConnection()
                End Try

            Else
                Return False ' Query Failed
                IsTransaction = False
            End If

        Catch ex As Exception
            IsTransaction = False
            LogException(ex)
        Finally
            CloseConnection()
        End Try
    End Function




    ''' <summary>
    ''' Checking seleted item stock 
    ''' </summary>
    ''' <param name="drItemInfo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function IsStockAvailable(ByVal _BLNegativeInventoryApplicable As Boolean, ByVal drItemInfo As DataRow, Optional ByVal strColumnName As String = "") As Boolean
        'BalanceQty
        Try
            If Not (_BLNegativeInventoryApplicable) Then

                If strColumnName = String.Empty Then
                    If (drItemInfo("AvailableQty") > Decimal.Zero) Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    If (drItemInfo(strColumnName) > Decimal.Zero) Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Else
                Return True
            End If

        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Check Selected Item prize is greater than the zero .
    ''' </summary>
    ''' <param name="drItemInfo">Selected ArticleRow</param>
    ''' <param name="strColumnName">Name of Rate Column of an article.</param>
    ''' <param name="strErrorMsg">Erro msg if rate found it will return empty otherwise return a string </param>
    ''' <returns>On success retrun true otherwise return false</returns>
    ''' <remarks>BirthListCreate,BirthListSales,BirthListUpdate</remarks>
    Public Function IsArticleRateAvailabel(ByVal drItemInfo As DataRow, Optional ByVal strColumnName As String = "", Optional ByVal strErrorMsg As String = "")
        Try
            If Not drItemInfo Is Nothing Then

                Dim obj As Object = drItemInfo(strColumnName.ToUpper())
                If Not obj Is Nothing AndAlso Not obj Is DBNull.Value Then
                    Dim decf As Decimal = CDbl(obj)

                    If (decf > Decimal.Zero) Then
                        Return True
                    Else
                        strErrorMsg = "Rate Not Found "
                        Return False
                    End If

                End If
            Else
                strErrorMsg = "Rate Not Found "
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function


    ''' <summary>
    '''  Saving Credit Voucher details 
    ''' </summary>
    ''' <param name="CLPProgram">CLP Program for current site </param>
    ''' <param name="DocumentType">DocumentType (Birthlist ,SalesOrde etc.</param>
    ''' <param name="siteCode">Current Site Code </param>
    ''' <param name="UserID"> Logged User Code </param>
    ''' <param name="decAmount">TotalAmount applicable for voucher  </param>
    ''' <returns>On success : true otherwise Flase </returns>
    ''' <UsedIn >frmUserAutherisation,frmNBirthListUpdate  </UsedIn>
    ''' <CreatedBy>Rahul</CreatedBy>
    ''' <remarks>CONNECTION IS NOT CLOSED FOR TRANSACTION STATUS </remarks>
    Public Function SaveCreditVoucher(ByRef sqlTransaction As SqlClient.SqlTransaction, ByVal strBirthList As String, ByVal dateServer As Date, ByVal dateExpiry As Date, ByVal CLPProgram As String, ByVal DocumentType As String, ByVal siteCode As String, ByVal UserID As String, ByVal decAmount As Decimal, ByRef genVoucherNO As String, Optional ByVal daysForExpiry As Integer = 0, Optional ByVal Online As Boolean = True, Optional ByVal strTerminalId As String = "", Optional ByVal strFinYear As String = "", Optional ByVal strCurrencyCode As String = "", Optional ByVal dtDayOpen As Date = Nothing) As Boolean
        Try
            Dim objclsComman As New clsCommon
            OpenConnection()
            sqlTransaction = SpectrumCon.BeginTransaction()
            Dim strInvoiceNumber As String = ""
            If (objclsComman.SaveCreditVoucher(CLPProgram, DocumentType, True, strBirthList, siteCode, dateServer, UserID, dateExpiry, sqlTransaction, SpectrumCon, decAmount, "", genVoucherNO, daysForExpiry)) Then
                If Save_SalesInvoice_BirthListCloseCV(Online, siteCode, strFinYear, strBirthList, strInvoiceNumber, strTerminalId, UserID, DateAndTime.Now, UserID, decAmount, strCurrencyCode, sqlTransaction, dtDayOpen) Then
                    Return True
                Else
                    sqlTransaction.Rollback()
                    Return False
                End If
            Else
                sqlTransaction.Rollback()
                Return False
            End If
        Catch ex As Exception
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function
    ''' <summary>
    '''  TODO: Saving birthlist close cv  information in sales invoice .
    ''' </summary>
    ''' <UsedBy>frmNBirthListUpdate.cs</UsedBy>
    ''' <CreatedBy>Rahul</CreatedBy>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save_SalesInvoice_BirthListCloseCV(ByVal Online As Boolean, ByVal strSiteCode As String, ByVal strFinYear As String, ByVal strDocumentnumber As String, ByVal strInvoiceNumber As String, ByVal strTerminalId As String, ByVal strUserName As String, ByVal dtCreatedAt As Date, ByVal strCreatedBy As String, ByVal decTotalAmount As Decimal, ByVal strCurrencyCode As String, ByVal sqlTran As SqlTransaction, ByVal dtDayOpen As Date) As Boolean
        Try
            Dim objClsBirthListGlobal As New clsBirthListGobal
            Dim strQuery As String = ""
            Dim cmdVoucher As SqlCommand
            Dim objClsComman As New clsCommon
            Dim objclsBirthList As New clsBirthListSalesSave




            Dim strTenderHeadCode As String = String.Empty
            Dim dsTender As New SqlCommand("Select TenderHeadCode from mstTender where TenderType = 'CreditVouc(I)' and Positive_Negative = '-'", SpectrumCon)
            dsTender.Transaction = sqlTran
            strTenderHeadCode = dsTender.ExecuteScalar


            strInvoiceNumber = objclsBirthList.GenNewSaleInVoiceNumber(strTerminalId, Online)
            strQuery = "INSERT INTO [SalesInvoice] ([SiteCode],[FinYear],[DocumentNumber],[SaleInvNumber],[SaleInvLineNumber]"
            strQuery = strQuery & " ,[DocumentType],[TerminalID],[TenderTypeCode],[AmountTendered],[ExchangeRate]"
            strQuery = strQuery & " ,[CurrencyCode],[SOInvDate],[SOInvTime],[UserName],[ManagersKeytoUpdate]"
            strQuery = strQuery & ",[ChangeLine],[RefNo_1],[RefNo_2],[RefNo_3],[RefNo_4],[RefDate],[CREATEDAT]"
            strQuery = strQuery & " ,[CREATEDBY],[CREATEDON],[UPDATEDAT],[UPDATEDBY],[UPDATEDON],[STATUS],[TenderHeadCode])"
            strQuery = strQuery & " VALUES "
            strQuery = strQuery & "('" & strSiteCode & " ','" & strFinYear & "', '" & strDocumentnumber & "' ,'" & strInvoiceNumber & "' "
            strQuery = strQuery & ",'1','BLS' , '" & strTerminalId & "' "
            strQuery = strQuery & " ,'CreditVouc(I)','" & clsCommon.ConvertToEnglish(decTotalAmount * -1) & "','0'"
            strQuery = strQuery & ",'" & strCurrencyCode & "','" & Format(dtDayOpen, "yyyyMMdd") & "' ,'" & dtCreatedAt.ToString("yyyyMMdd hh:mm:ss tt") & "'"
            strQuery = strQuery & ",'" & strUserName & "',3,1"
            strQuery = strQuery & ",NULL,NULL,NULL"
            strQuery = strQuery & ",NULL,'" & dtCreatedAt.ToString("yyyyMMdd hh:mm:ss tt") & "','" & strSiteCode & "'"
            strQuery = strQuery & ",'" & strUserName & "' ,'" & dtCreatedAt.ToString("yyyyMMdd hh:mm:ss tt") & "','" & strSiteCode & "'"
            strQuery = strQuery & ",'" & strUserName & "' ,'" & dtCreatedAt.ToString("yyyyMMdd hh:mm:ss tt") & "',1"
            strQuery = strQuery & ", '" & strTenderHeadCode & "')"
            cmdVoucher = New SqlCommand(strQuery, SpectrumCon)
            cmdVoucher.Transaction = sqlTran
            If cmdVoucher.ExecuteNonQuery() > 0 Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            LogException(ex)
            Return False

        End Try

    End Function
    Public Function CalculateExpiryDate(ByVal datebase As Date, ByVal daysForExpiry As Integer) As Date
        Try
            Dim dateExpiry As Date = datebase.AddDays(daysForExpiry)
            Return dateExpiry
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GenerateGiftVoucher(ByRef dtGv As DataTable, ByRef sqlTransaction As SqlClient.SqlTransaction, ByVal docno As String, ByVal SiteCode As String, ByVal userId As String, ByVal GenTime As DateTime) As Boolean
        Try


            Dim objclsComman As New clsCommon()
            OpenConnection()
            sqlTransaction = SpectrumCon.BeginTransaction()
            Dim MaxNo As Int64 = objclsComman.getDocumentNo("GV", SiteCode)
            Dim dtTempGv As DataTable = dtGv.Clone()
            Dim Filter As String = "ISPREPRINTED = True"
            Dim dv As New DataView(dtGv, Filter, "VOUCHERCODE", DataViewRowState.CurrentRows)
            If dv.Count > 0 Then
                dtTempGv = dv.ToTable.Copy()
            End If
            Filter = "ISPREPRINTED is null"
            dv.RowFilter = Filter
            If dv.Count > 0 Then
                dv.AllowEdit = True
                For Each dr As DataRowView In dv
                    Dim i As Int16 = dr("Quantity")
                    For i = 1 To CInt(dr("Quantity"))

                        'Changed by Rohit to generate Document No. for proper sorting
                        Try
                            Dim strFinyear = objclsComman.GetFinancialYear(GenTime, SiteCode)
                            dr("VOURCHERSERIALNBR") = objclsComman.GenDocNo("G" & SiteCode.Substring(SiteCode.Length - 3, 3) & strFinyear.Substring(strFinyear.Length - 2, 2), 13, MaxNo)
                        Catch ex As Exception
                            dr("VOURCHERSERIALNBR") = "G" & SiteCode.Substring(SiteCode.Length - 3, 3) & MaxNo
                        End Try
                        'End Change by Rohit

                        'changed by ram dt:24.05.2009 action : add
                        dr("Createdby") = userId
                        dr("CreatedAt") = SiteCode
                        dr("Createdon") = GenTime
                        dr("Updatedby") = userId
                        dr("UpdatedAt") = SiteCode
                        dr("Updatedon") = GenTime
                        dr("Status") = True
                        'changed by ram dt:24.05.2009 action : add
                        dtTempGv.ImportRow(dr.Row)
                        MaxNo = MaxNo + 1
                    Next
                Next
            End If
            objclsComman.DeleteColumnFromDataTable(dtTempGv, "Quantity")
            objclsComman.DeleteColumnFromDataTable(dtTempGv, "ISPREPRINTED")
            objclsComman.DeleteColumnFromDataTable(dtTempGv, "ISSUEDDOCNUMBER")
            objclsComman.DeleteColumnFromDataTable(dtTempGv, "VOUCHERDESC")
            objclsComman.DeleteColumnFromDataTable(dtTempGv, "NetAmount")
            objclsComman.DeleteColumnFromDataTable(dtTempGv, "SEL")
            objclsComman.DeleteColumnFromDataTable(dtTempGv, "ExpiryInDays")
            objclsComman.AddColumnToDataTable(dtTempGv, "ISSUEDDOCNUMBER", "System.String", docno)
            'changed by ram dt:24.05.2009 action: comment
            'AddColumnToDataTable(dtTempGv, "Createdby", "System.String", userId)
            'AddColumnToDataTable(dtTempGv, "CreatedAt", "System.String", SiteCode)
            'AddColumnToDataTable(dtTempGv, "Createdon", "System.DateTime", GenTime)
            'AddColumnToDataTable(dtTempGv, "Updatedby", "System.String", userId)
            'AddColumnToDataTable(dtTempGv, "UpdatedAt", "System.String", SiteCode)
            'AddColumnToDataTable(dtTempGv, "Updatedon", "System.DateTime", GenTime)
            'changed by ram dt:24.05.2009 action: comment

            objclsComman.AddMode(dtTempGv)
            If objclsComman.SaveData(dtTempGv, SpectrumCon, sqlTransaction) Then
                If objclsComman.UpdateDocumentNo("GV", SpectrumCon, sqlTransaction, MaxNo) = True Then
                    dtGv = Nothing
                    dtGv = dv.ToTable()
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            LogException(ex)
            Return False
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function GetEventName(ByVal strEventID As String, ByVal strSiteCode As String) As String
        Try
            Try
                Dim QrySalesPerson As String = "select * from dbo.BirthListEvents where EventId='" & strEventID & "' and  Sitecode='" & strSiteCode & "' "

                Dim dt As New DataTable
                Dim da As New SqlDataAdapter(QrySalesPerson, ConString)
                da.Fill(dt)
                If Not dt Is Nothing Then
                    Return dt.Rows(0)("EventName").ToString()
                End If
                Return String.Empty
            Catch ex As Exception
                Return String.Empty
            End Try
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try

    End Function


End Class

