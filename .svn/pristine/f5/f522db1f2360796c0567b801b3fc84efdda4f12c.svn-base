Imports SpectrumBL

Public Class frmNBirthListCreate
    Inherits CtrlRbnBaseForm
    Dim dtCustmInfoBL As New DataTable
    Private objclsComman As New clsCommon
    Private objclsBirthList As New clsBirthList(clsAdmin.Financialyear)
    Private drSelectedCustomer As DataRow
    Protected Shared IsCreateNewBlID As Boolean = True
    Private drSelectedItemDetails As DataRow
    Private objPrinting As New clsPrintBirthList
    Private dtGridData As DataTable
    Private dtOriginal As DataTable
    Private IsUpdateChanges As Boolean = False
    Private _CustomerInfoTable As DataTable
    Private isFormClosed As Boolean = True

    ''' <summary>
    ''' Checking Customer is selected or not 
    ''' </summary>
    ''' <value></value>
    ''' <returns>Boolean</returns>
    ''' <remarks>ReadOnly</remarks>

    Private ReadOnly Property IsCustomerSelected() As Boolean
        Get
            If Not (CtrlCustDtls1.dtCustmInfo Is Nothing) Then
                If (CtrlCustDtls1.dtCustmInfo.Rows.Count = 0) Then
                    CtrlCustSearch1.CtrlTxtCustNo.Focus()
                    ShowMessage(getValueByKey("BL001"), "BL001 - " & getValueByKey("CLAE04"))
                    Return False
                Else
                    Return True
                End If
            Else
                CtrlCustSearch1.CtrlTxtCustNo.Focus()
                ShowMessage(getValueByKey("BL001"), "BL001 - " & getValueByKey("CLAE04"))
                Return False
            End If
        End Get
    End Property

    ''' <summary>
    ''' Checking Sales person is selected or not 
    ''' </summary>
    ''' <value></value>
    ''' <returns>Boolean</returns>
    ''' <remarks>ReadOnly</remarks>
    Private ReadOnly Property IsSalesPersonSelected() As Boolean
        Get
            If clsDefaultConfiguration.BLIsSalesPersonApplicable = True Then
                Try
                    Dim dataRow As Object = CtrlSalesPerson1.CtrlSalesPersons.SelectedValue
                    If clsDefaultConfiguration.BLIsSalesPersonApplicable Then
                        If (dataRow Is Nothing) Then
                            CtrlSalesPerson1.CtrlSalesPersons.Focus()
                            ShowMessage(getValueByKey("BL002"), "BL002 - " & getValueByKey("CLAE04"))
                            Return False
                        Else
                            Return True
                        End If
                    Else
                        Return True
                    End If
                Catch
                    CtrlSalesPerson1.CtrlSalesPersons.Focus()
                    Return False
                    'MessageBox.Show(getValueByKey("BL002"), "BL002")
                End Try
            Else
                Return True
            End If
        End Get
    End Property

    ''' <summary>
    ''' Selected Sales Person Name 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private ReadOnly Property SalesPersonName() As String
        Get
            Try
                If CtrlSalesPerson1.CtrlSalesPersons.SelectedIndex > -1 Then
                    Return CtrlSalesPerson1.CtrlSalesPersons.SelectedValue.ToString()
                Else
                    Return String.Empty
                End If
            Catch ex As Exception
                LogException(ex)
                Return String.Empty
            End Try
        End Get
    End Property


    Private Property CustomerInfoTable() As DataTable
        Get
            Return _CustomerInfoTable
        End Get
        Set(ByVal value As DataTable)
            If Not value Is Nothing Then
                If value.Rows.Count > 0 Then
                    drSelectedCustomer = value.Rows(0)
                    _CustomerInfoTable = value
                End If

            End If
        End Set
    End Property
    ''' <summary>
    ''' Checking  customer is selected or not  
    ''' </summary>
    ''' <value></value>
    ''' <returns>String</returns>
    ''' <remarks>ReadOnly</remarks>
    Private ReadOnly Property CustomerID() As String
        Get
            Try
                Return drSelectedCustomer.Item("CustomerNo").ToString()
            Catch ex As Exception
                CtrlCustDtls1.Focus()
                ShowMessage(getValueByKey("BL001"), "BL001 - " & getValueByKey("CLAE04"))
                LogException(ex)
                Return String.Empty
            End Try
        End Get
    End Property


    ''' <summary>
    '''   EventID selected by customer 
    ''' </summary>
    ''' <value></value>
    ''' <returns>String</returns>
    ''' <remarks>ReadOnly</remarks>

    Private ReadOnly Property EventID() As String
        Get
            Try
                Return cboEvent.SelectedValue.ToString()
            Catch ex As Exception
                LogException(ex)
            End Try
        End Get
    End Property

    ''' <summary>
    '''   Event Date  selected by customer 
    ''' </summary>
    ''' <value></value>
    ''' <returns>String</returns>
    ''' <remarks>ReadOnly</remarks>
    Private ReadOnly Property EventDate() As String
        Get
            Try
                Return dateCEvent.Text
            Catch ex As Exception
                LogException(ex)
            End Try
        End Get
    End Property

    ''' <summary>
    '''  Validate Delivery date 
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>Boolean</returns>
    ''' <remarks>ReadOnly</remarks>
    Private ReadOnly Property IsDeliveryDateBackDated() As Boolean
        Get
            If (clsDefaultConfiguration.BLIsCheckDeliveryDate = True) Then
                Try
                    If Not dateCDelivery.Text = String.Empty Then
                        Dim dateDelivery As Date = FormatDateTime(dateCDelivery.Text, DateFormat.ShortDate)
                        Dim dateCurrent1 As Date = FormatDateTime(dateCurrent, DateFormat.ShortDate)
                        If (dateDelivery >= dateCurrent1) Then
                            Return False
                        Else
                            dateCDelivery.Focus()
                            ShowMessage(getValueByKey("BL003"), "BL003 - " & getValueByKey("CLAE04"))
                            Return True
                        End If
                    Else
                        dateCDelivery.Focus()
                        ShowMessage(getValueByKey("BL004"), "BL004 - " & getValueByKey("CLAE04"))
                        Return True
                    End If
                Catch ex As Exception
                    dateCDelivery.Focus()
                    ShowMessage(getValueByKey("BL004"), "BL004 - " & getValueByKey("CLAE04"))
                    LogException(ex)
                    Return True
                End Try
            Else
                Return False
            End If
        End Get
    End Property

    ''' <summary>
    '''  Validate EventDate
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>Boolean</returns>
    ''' <remarks>ReadOnly</remarks>

    Private ReadOnly Property IsEventDateValid() As Boolean
        Get
            Try
                If Not dateCEvent.Text = String.Empty Then
                    Dim dateEvent As Date = FormatDateTime(dateCEvent.Text, DateFormat.ShortDate)
                    Dim dateCurrent1 As Date = FormatDateTime(dateCurrent, DateFormat.ShortDate)
                    If (dateEvent >= dateCurrent1) Then
                        Return False
                    Else
                        dateCEvent.Focus()
                        ShowMessage(getValueByKey("BL007"), "BL007 - " & getValueByKey("CLAE04"))
                        Return True
                    End If
                Else
                    ShowMessage(getValueByKey("BL008"), "BL008 - " & getValueByKey("CLAE04"))
                    dateCEvent.Focus()
                    Return True
                End If
            Catch ex As Exception
                dateCEvent.Focus()
                ShowMessage(getValueByKey("BL008"), "BL008 - " & getValueByKey("CLAE04"))
                LogException(ex)
                Return True
            End Try
        End Get
    End Property

    Private Function GetCustomerInfo() As Boolean
        Try
            If Not CtrlCustDtls1.dtCustmInfo Is Nothing Then
                If CtrlCustDtls1.dtCustmInfo.Rows.Count > 0 Then
                    drSelectedCustomer = CtrlCustDtls1.dtCustmInfo.Rows(0)
                Else
                    CtrlCustDtls1.Focus()
                    ShowMessage(getValueByKey("BL001"), "BL001 - " & getValueByKey("CLAE04"))

                    Return False
                    Exit Function
                End If
            End If
            If Not (dtCustmInfoBL.Columns.Count > 0) Then
                Dim dcGVApplicable As New DataColumn
                dtCustmInfoBL.Columns.Add(New DataColumn("CustomerID"))
                dtCustmInfoBL.Columns.Add(New DataColumn("CustomerName"))
                dtCustmInfoBL.Columns.Add(New DataColumn("Address"))
                dtCustmInfoBL.Columns.Add(New DataColumn("EmailID"))
                Dim dcEventdate As New DataColumn("EventDate")
                dcEventdate.DataType = System.Type.GetType("System.DateTime")
                dtCustmInfoBL.Columns.Add(dcEventdate)
                dtCustmInfoBL.Columns.Add(New DataColumn("EventID"))
                dtCustmInfoBL.Columns.Add(New DataColumn("DeliveryDate", System.Type.GetType("System.DateTime")))
                dcGVApplicable.ColumnName = "GVApplicable"
                dcGVApplicable.DataType = System.Type.GetType("System.Boolean")
                dtCustmInfoBL.Columns.Add(dcGVApplicable)
                dtCustmInfoBL.Columns.Add(New DataColumn("PinCode"))
                dtCustmInfoBL.Columns.Add(New DataColumn("TotalAmount"))
                dtCustmInfoBL.Columns.Add(New DataColumn("TotalItem"))
                dtCustmInfoBL.Columns.Add(New DataColumn("TotalQty"))
                dtCustmInfoBL.Columns.Add(New DataColumn("SalesExecutiveCode"))
                'dtCustmInfoBL.Columns.Add(New DataColumn("Remark"))
            End If
            If Not drSelectedCustomer Is Nothing Then
                If Not IsDeliveryDateBackDated Then
                    If Not IsEventDateValid Then
                        dtCustmInfoBL.Clear()
                        Dim newRow As DataRow = dtCustmInfoBL.NewRow()
                        dtCustmInfoBL.Rows.Add(newRow)
                        dtCustmInfoBL.AcceptChanges()
                        dtCustmInfoBL.Rows(0).BeginEdit()
                        dtCustmInfoBL.Rows(0)("CustomerID") = drSelectedCustomer("CustomerNo")
                        dtCustmInfoBL.Rows(0)("CustomerName") = drSelectedCustomer("CustomerName")
                        dtCustmInfoBL.Rows(0)("Address") = CtrlCustDtls1.lblAddressValue.Text
                        dtCustmInfoBL.Rows(0)("EmailID") = CtrlCustDtls1.lblEmailIdValue.Text
                        Dim dateEventDate As Date = dateCEvent.Text
                        Dim strEventDate As String = dateEventDate.ToString(clsAdmin.SqlDBDateFormat)
                        'Dim sdt As Date = CDate(strEventDate)
                        dtCustmInfoBL.Rows(0)("EventDate") = dateEventDate
                        'dtCustmInfo.Rows(0)("BirthDate") = dtpEventDate.Text
                        dtCustmInfoBL.Rows(0)("EventID") = cboEvent.SelectedValue
                        Dim objDeliveryDate As Object = dateCDelivery.Text
                        Dim dateDeliveryDate As Date
                        If Not objDeliveryDate Is Nothing And Not objDeliveryDate Is DBNull.Value And Not objDeliveryDate = String.Empty Then
                            dateDeliveryDate = CDate(objDeliveryDate)
                        Else
                            dateDeliveryDate = objclsComman.GetCurrentDate()
                        End If
                        Dim strDeliveryDate As String = dateDeliveryDate.ToString(clsAdmin.SqlDBDateTimeFormat)
                        dtCustmInfoBL.Rows(0)("DeliveryDate") = dateDeliveryDate
                        dtCustmInfoBL.Rows(0)("GVapplicable") = True
                        dtCustmInfoBL.Rows(0)("PinCode") = drSelectedCustomer.Item("PinCode")
                        dtCustmInfoBL.Rows(0)("TotalAmount") = clsAdmin.CurrencySymbol + " " + CtrlCashSummary1.lbl8
                        dtCustmInfoBL.Rows(0)("TotalItem") = CtrlCashSummary1.lbl5
                        dtCustmInfoBL.Rows(0)("TotalQty") = CtrlCashSummary1.lbl6
                        dtCustmInfoBL.Rows(0)("SalesExecutiveCode") = SalesPersonName

                        dtCustmInfoBL.Rows(0).EndEdit()
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
                Return True
            Else
                CtrlCustDtls1.Focus()
                ShowMessage(getValueByKey("BL001"), "BL001 - " & getValueByKey("CLAE04"))
                Return False
            End If
        Catch ex As Exception
            CtrlCustDtls1.Focus()
            ShowMessage(getValueByKey("BL009"), "BL009 - " & getValueByKey("CLAE04"))
            LogException(ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Customer search by popup form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub BtnCustomerSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCustomerSearch.Click
    '    Dim objfrmSearchCustomer As New frmSearchCustomer
    '    'If objSO.IsCLPApplicable = True Then
    '    'If MsgBox("Do you want to Select CLP Customer? ", MsgBoxStyle.YesNo, "Customer Information") = MsgBoxResult.Yes Then
    '    '    Dim objfrmSearchCLPCustomer As New frmSearchCustomer
    '    '    objfrmSearchCLPCustomer.ShowDialog()
    '    '    RemoveHandler txtCustomerID.TextChanged, AddressOf txtCustomerID_TextChanged
    '    '    CustomerInfoTable = objfrmSearchCustomer.dtCustmInfo
    '    '    Display_CustomerInfo(drSelectedCustomer)

    '    '    CustomerInfoTable = objfrmSearchCustomer.dtCustmInfo
    '    '    CustomerType = clsBirthListGobal.CustomerTypeName.CLP
    '    '    AddHandler txtCustomerID.TextChanged, AddressOf txtCustomerID_TextChanged
    '    'Else
    '    '    objfrmSearchCustomer.BtnCreateNew.Visible = True
    '    '    objfrmSearchCustomer.ShowDialog()

    '    '    CustomerInfoTable = objfrmSearchCustomer.dtCustmInfo
    '    '    Display_CustomerInfo(drSelectedCustomer)
    '    '    CustomerType = clsBirthListGobal.CustomerTypeName.SO
    '    'End If
    '    'Else
    '    RemoveHandler txtCustomerID.TextChanged, AddressOf txtCustomerID_TextChanged
    '    If (rbtnCLPSearch.Checked) Then
    '        objfrmSearchCustomer.ShowCLP = True
    '        objfrmSearchCustomer.ShowSO = False
    '    Else
    '        objfrmSearchCustomer.ShowCLP = False
    '        objfrmSearchCustomer.ShowSO = True
    '    End If
    '    objfrmSearchCustomer.BtnCreateNew.Visible = True

    '    objfrmSearchCustomer.ShowDialog()

    '    CustomerInfoTable = objfrmSearchCustomer.dtCustmInfo
    '    Display_CustomerInfo(drSelectedCustomer)

    '    AddHandler txtCustomerID.TextChanged, AddressOf txtCustomerID_TextChanged
    '    'End If
    '    IsNewCustomer = False
    'End Sub
    ''' <summary>
    '''  Items search by popup form 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnItemSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If (CheckUserInput()) Then
                Dim objfrmItemSearch1 As New frmNItemSearch
                If (IsCustomerSelected) Then
                    If Not (IsDeliveryDateBackDated) Then
                        If Not (IsEventDateValid) Then
                            If (IsCreateNewBlID) Then
                                IsCreateNewBlID = False
                            End If
                            objfrmItemSearch1.ShowDialog()
                            If Not objfrmItemSearch1.SearchResult Is Nothing Then
                                drSelectedItemDetails = objfrmItemSearch1.ItemRow

                                'Rakesh:06.11.2013-->7895 : Avoid stock check validation when order place from SO & BL
                                'Dim objclsBirthListGlobal As New clsBirthListGobal
                                'If (objclsBirthListGlobal.IsStockAvailable(clsDefaultConfiguration.NegativeInventoryAllowed, drSelectedItemDetails)) Then
                                GridDataSource()
                                'Else
                                '    CtrlSalesPerson1.Focus()
                                '    ShowMessage(getValueByKey("BL046"), "BL046 - " & getValueByKey("CLAE04"))
                                'End If

                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    ''' <summary>
    '''  DataSource for grid 
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Function GridDataSource() As Boolean
        Try
            btnCreateNew.Enabled = True
            Dim objclsBirthListGlobal As New clsBirthListGobal
            If (objclsBirthListGlobal.IsArticleRateAvailabel(drSelectedItemDetails, "Sellingprice", "")) Then
                If clsDefaultConfiguration.PrintItemFullName = True Then
                    grdItemDetails.DataSource = objclsBirthList.AddItemIn_BirthListRegTable(drSelectedItemDetails, clsAdmin.SiteCode, True)
                Else
                    grdItemDetails.DataSource = objclsBirthList.AddItemIn_BirthListRegTable(drSelectedItemDetails, clsAdmin.SiteCode, False)
                End If
                Display_GridSetting()
                CalculateTotal()
                Return True
            Else
                ShowMessage(getValueByKey("BL081"), "BL081 - " & getValueByKey("CLAE04")) 'Rate Not found 
            End If

        Catch ex As Exception
            CtrlSalesPerson1.Focus()
            ShowMessage(getValueByKey("BL012"), "BL012 - " & getValueByKey("CLAE04")) 'Problem to assign data source 
            LogException(ex)
            Return False
        End Try

    End Function

    Private Function CheckGridDataSource() As Boolean
        Try
            dtGridData = grdItemDetails.DataSource
            If (Not dtGridData Is Nothing) Then
                If (dtGridData.Rows.Count > 0) Then
                    Return True
                Else
                    CtrlSalesPerson1.Focus()
                    ShowMessage(getValueByKey("BL010"), "BL010 - " & getValueByKey("CLAE04"))
                    Return False
                End If
            Else
                CtrlSalesPerson1.Focus()
                ShowMessage(getValueByKey("BL010"), "BL010 - " & getValueByKey("CLAE04")) ' please add items to grid 
                Return False
            End If
        Catch ex As Exception
            CtrlSalesPerson1.Focus()
            ShowMessage(getValueByKey("BL011"), "BL011 - " & getValueByKey("CLAE04")) ' Check added items into grid 
            LogException(ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Display setting for grid 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Display_GridSetting() As Boolean
        Try
            grdItemDetails.Cols("BirthListId").Visible = False

            If (clsDefaultConfiguration.BarcodeDisplayAllowed) Then
                grdItemDetails.Cols("EAN").Caption = IIf(resourceMgr Is Nothing, "Barcode", getValueByKey("frmNBirthListCreate.grdItemDetails.ean"))
                grdItemDetails.Cols("EAN").Width = 90
                grdItemDetails.Cols("EAN").AllowEditing = False
                grdItemDetails.Cols("EAN").Visible = True
            Else
                grdItemDetails.Cols("EAN").Visible = False
            End If

            grdItemDetails.Cols("ArticleCode").Visible = True
            grdItemDetails.AutoSizeCols()

        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    ''' <summary>
    ''' Saving birth list information into database  
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSaveBirthList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbbtnSaveBL.Click
        Try
            If (CheckUserInput()) Then
                If SaveBirthList() Then
                    CreateNewBirthList()
                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                End If

            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    '''  Called after grid row edited
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdItemDetails_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdItemDetails.AfterEdit
        Try
            Dim strColumn As String = grdItemDetails.Cols(e.Col).Name
            If Not (grdItemDetails.Cols(e.Col)(e.Row) Is DBNull.Value) Then
                Dim strChangedValue As String = grdItemDetails.Cols(e.Col)(e.Row)
                If Not strChangedValue.Length > 9 Then
                    If strColumn = "RequstedQty" Or strColumn = "Rate" Then
                        Dim iReservedQty As String = grdItemDetails.Cols("ReservedQty")(e.Row)
                        If Not strChangedValue = "0" Then
                            grdItemDetails.DataSource = objclsBirthList.UpdateDataInGrid(strColumn, e.Row - 1, strChangedValue)
                            ValidateReservedQty(iReservedQty, e.Col, e.Row)
                        Else
                            grdItemDetails.Cols(e.Col)(e.Row) = 1
                            ValidateReservedQty(iReservedQty, e.Col, e.Row)
                            CalculateTotal()
                        End If

                    ElseIf (strColumn = "ReservedQty") Then
                        grdItemDetails.DataSource = objclsBirthList.UpdateDataInGrid(strColumn, e.Row - 1, strChangedValue)
                        ValidateReservedQty(strChangedValue, e.Col, e.Row)

                        If (Not clsDefaultConfiguration.NegativeInventoryAllowed) Then
                            Dim objCommon As New clsCommon
                            Dim articleCode = grdItemDetails.Item(e.Row, "ArticleCode")
                            Dim articleEAN = grdItemDetails.Item(e.Row, "EAN")
                            Dim Quantity = grdItemDetails.Item(e.Row, "ReservedQty")

                            Dim StockQty As Double = objCommon.GetStocks(clsAdmin.SiteCode, articleEAN, articleCode, True)

                            If (StockQty < CDbl(strChangedValue)) Then
                                ShowMessage(String.Format(getValueByKey("SB016"), StockQty), "SB016 - " & getValueByKey("CLAE04"))
                                grdItemDetails.Item(e.Row, "ReservedQty") = IIf(StockQty > 0, StockQty, 0)
                            End If
                        End If
                    End If
                Else
                    MsgBox(getValueByKey("CM059"), MsgBoxStyle.Critical, "CM059" & " | " & getValueByKey("CLAE05"))
                End If

            ElseIf strColumn = "RequstedQty" Then
                grdItemDetails.Cols(e.Col)(e.Row) = 1
            ElseIf (strColumn = "ReservedQty") Then
                grdItemDetails.Cols(e.Col)(e.Row) = 0
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function ValidateReservedQty(ByVal strChangedValue As String, ByVal icolumnIndex As Integer, ByVal irowIndex As Integer, Optional ByVal isChangeReservedQty As Boolean = True) As Boolean
        Try
            Dim iOrderedQty As Integer = grdItemDetails.Cols("RequstedQty")(irowIndex)
            Dim strColumnName As String = grdItemDetails.Cols(icolumnIndex)(irowIndex)

            If (strChangedValue > iOrderedQty) Then
                ShowMessage(getValueByKey("BL013"), "BL013 - " & getValueByKey("CLAE04")) ' You can't reserved items more than ordred items 
                grdItemDetails.Cols("ReservedQty")(irowIndex) = 0
                CalculateTotal()
            Else
                If isChangeReservedQty Then
                    grdItemDetails.Cols("ReservedQty")(irowIndex) = strChangedValue
                End If

                CalculateTotal()
            End If

            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function


    'Private Sub frmBirthListCreate_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    '    CloseForm()
    'End Sub
    'Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        CloseForm()
    '    Catch ex As Exception

    '    End Try
    'End Sub


    Dim dateCurrent As Date
    ''' <summary>
    '''  Loading Event Name display combobox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmBirthListCreate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            '---- Set Tab Index START
            Call SetFormTabStop(Me, tabStopValue:=False)
            Dim ctrTablIndex As New Dictionary(Of Object, Int16)

            ctrTablIndex.Add(C1Sizer1, 0)
            ctrTablIndex.Add(cboEvent, 0)
            ctrTablIndex.Add(dateCEvent, 1)
            ctrTablIndex.Add(dateCDelivery, 2)
            ctrTablIndex.Add(rtxtRemark, 3)

            ctrTablIndex.Add(Me.CtrlCustSearch1, 1)
            ctrTablIndex.Add(Me.CtrlCustSearch1.rbOtherCust, 0)
            ctrTablIndex.Add(Me.CtrlCustSearch1.rbCLPMember, 1)
            ctrTablIndex.Add(Me.CtrlCustSearch1.CtrlBtn1, 2)
            ctrTablIndex.Add(Me.CtrlCustSearch1.CtrlBtnNew, 3)
            ctrTablIndex.Add(Me.CtrlCustSearch1.CtrlTxtCustNo, 4)
            ctrTablIndex.Add(Me.CtrlCustSearch1.CtrlTxtSwapeCard, 5)

            ' ctrTablIndex.Add(Me.tabSalesOrder, 1)
            'ctrTablIndex.Add(Me.TabPageOrderedItems, 1)
            'ctrTablIndex.Add(Me.c1SizerGrid, 1)
            ctrTablIndex.Add(Me.CtrlSalesPerson1, 2)
            ctrTablIndex.Add(Me.CtrlSalesPerson1.CtrlSalesPersons, 0)
            ctrTablIndex.Add(Me.CtrlSalesPerson1.CtrlTxtBox, 1)
            ctrTablIndex.Add(Me.CtrlSalesPerson1.CtrlCmdSearch, 2)

            ctrTablIndex.Add(Me.grdItemDetails, 3)

            Call SetFormTabIndex(ctrTablIndex:=ctrTablIndex)
            Me.grdItemDetails.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.None
            c1SizerGrid.TabStop = False
            C1Sizer1.TabStop = False
            '---- Set Tab Index END 

            AddHandler CtrlRbn1.DbtnF12.Click, AddressOf PriceChange

            isFormClosed = False
            PSetDefaultCurrencyOfCashMemoSummary(CtrlCashSummary1)
            Dim objclsBirthlistdefaultsetting As New clsDefaultConfiguration("BLS")
            objclsBirthlistdefaultsetting.GetDefaultSettings()

            cboEvent.ValueMember = "EventID"
            cboEvent.DisplayMember = "EventName"
            cboEvent.DataSource = objclsBirthList.LoadEventName()
            IsCreateNewBlID = True

            CtrlProductImage1.CtrlProductImages.Image = Nothing
            pC1ComboSetDisplayMember(cboEvent)
            dateCurrent = objclsComman.GetCurrentDate()
            AddEvent()
            grdItemDetails.Cols("Amount").Format = GridAmountColumnCustomeFormat()
            grdItemDetails.Cols("Rate").Format = GridAmountColumnCustomeFormat()

            btnCreateNew.Enabled = False
            PrintSetProperty()
            SetCulture(Me, Me.Name, CtrlRbn1)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Private Function AddEvent() As Boolean
        Try

            AddHandler CtrlSalesPerson1.CtrlCmdSearch.Click, AddressOf BtnItemSearch_Click  'Search item on button click 
            AddHandler CtrlSalesPerson1.CtrlTxtBox.KeyDown, AddressOf txtItemSearch_KeyDown ' Search Item on pressing event in items search text box 

            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Called when grid delete button pressed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdItemDetails_CellButtonClick(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdItemDetails.CellButtonClick
        Try
            Dim iSelectedRow As Int32 = e.Row
            dtGridData = grdItemDetails.DataSource
            Dim strEAN As String = grdItemDetails.Item(e.Row, "EAN")
            If Not iSelectedRow = -1 Then
                If Not dtGridData Is Nothing Then
                    Dim dvInfo As DataView
                    dvInfo = New DataView(dtGridData, "EAN='" + strEAN + "'", "", DataViewRowState.CurrentRows)
                    For Each drview As DataRowView In dvInfo
                        drview.Delete()
                    Next
                    dtGridData = dvInfo.Table
                    If Not dtOriginal Is Nothing Then
                        dvInfo = New DataView(dtOriginal, "EAN='" + strEAN + "'", "", DataViewRowState.CurrentRows)
                        For Each drview As DataRowView In dvInfo
                            drview.Delete()
                        Next
                    End If
                Else
                    grdItemDetails.Rows.Remove(e.Row)
                End If
                CalculateTotal()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Calculate Total ordered items, amount  
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function CalculateTotal() As Boolean
        Try
            Dim decAmount As Decimal
            Dim iorderQty As Integer
            Dim strTotalItem As String = ""
            Dim iReservedQty As Integer
            objclsBirthList.CalculateRecieptItem(strTotalItem, iorderQty, decAmount, clsAdmin.SiteCode, clsAdmin.CurrencyCode, iReservedQty)

            CashSummaryDisplay(strTotalItem, iorderQty, decAmount, iReservedQty)
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Sub CashSummaryDisplay(Optional ByVal strTotalItem As String = "", Optional ByVal strTotalQuantity As Object = Nothing, Optional ByVal strTotalAmt As Object = Nothing, Optional ByVal iReservedQty As Integer = 0)

        Try
            'CtrlCashSummary1.lbl8 = String.Format("{0} {1}", clsAdmin.CurrencySymbol, strTotalAmt)
            CtrlCashSummary1.lbltxt10 = MyRound(CDbl(strTotalAmt), clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)
            CtrlCashSummary1.lbltxt2 = strTotalQuantity
            CtrlCashSummary1.lbltxt1 = strTotalItem
            CtrlCashSummary1.lbltxt3 = iReservedQty
        Catch ex As Exception
            LogException(ex)

        End Try


    End Sub

    Private _iselectedindex As Integer = 0

    Private Shared isFirst As Boolean = False
    ''' <summary>
    ''' Display selected grid row  article image 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdItemDetails_RowColChange(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdItemDetails.RowColChange
        Try
            Dim grid As C1.Win.C1FlexGrid.C1FlexGrid = sender
            Dim strArticleCode As String
            If (isFirst) Then
                If (grid.Row >= 0) Then
                    Dim iTrt As Integer = grid.Row
                    strArticleCode = grid.Cols("ArticleCode")(iTrt)
                    'Dim strUrl As String = objclsComman.GetArticleImage(strArticleCode, My.Settings.ArticleImageFolder)
                    Dim strUrl As String = objclsComman.GetArticleImage(strArticleCode, ReadSpectrumParamFile("ArticleImageFolder"))
                    'CtrlProductImage1.CtrlProductImages.ImageLocation = strUrl
                    CtrlProductImage1.ShowArticleImage(strArticleCode)
                End If
            End If
            isFirst = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    '''  Entered EAN number for article by keybord
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ManualEnterEANNumber() As Boolean
        Try
            Dim objclsBirthListGlobal As New clsBirthListGobal
            Dim dtItem As DataTable
            If Not CtrlSalesPerson1.CtrlTxtBox.Text = String.Empty Then
                If (objclsBirthList.FindArticleCode(dtItem, CtrlSalesPerson1.CtrlTxtBox.Text, clsAdmin.SiteCode, EanType, clsAdmin.LangCode)) Then
                    drSelectedItemDetails = SearchBirthListItem(dtItem)
                    If Not drSelectedItemDetails Is Nothing Then
                        If (objclsBirthListGlobal.IsStockAvailable(clsDefaultConfiguration.NegativeInventoryAllowed, drSelectedItemDetails)) Then
                            GridDataSource()
                            Return True
                        Else
                            CtrlSalesPerson1.CtrlTxtBox.Focus()
                            ShowMessage(getValueByKey("BL014"), "BL014 - " & getValueByKey("CLAE04"))
                            'MessageBox.Show("Item not available in stock ", "ItemCode")
                            Return False
                        End If
                    Else
                        'MessageBox.Show("Item Code  Not Found. or Item is not purchase", "ItemCode")
                        Return False
                    End If
                Else
                    CtrlSalesPerson1.CtrlTxtBox.Focus()
                    ShowMessage(getValueByKey("BL014"), "BL014 - " & getValueByKey("CLAE04"))
                    'MessageBox.Show("Item Code  Not Found. or Item is not purchase", "ItemCode")
                    Return False
                End If
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Callled when pressing entered on EAN textbox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtItemSearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If (e.KeyCode = Keys.Enter) Then
                If (CheckUserInput()) Then
                    ManualEnterEANNumber()
                    CtrlSalesPerson1.CtrlTxtBox.Clear()
                End If
                fnGridColAutoSize(grdItemDetails)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try



    End Sub


    ''' <summary>
    '''  Check for value edited in grid row
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdItemDetails_BeforeEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdItemDetails.BeforeEdit
        Try
            Dim strChangedValue As String = grdItemDetails.Cols(e.Col)(e.Row)
            Dim strColumn As String = grdItemDetails.Cols(e.Col).Name
            If (strChangedValue = "0") And strColumn = "RequstedQty" Then
                grdItemDetails.Cols(e.Col)(e.Row) = 1
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbbtnPrintBL.Click
        Try
            If (CheckUserInput()) Then
                If (GetCustomerInfo()) Then
                    If (CheckGridDataSource()) Then
                        If (SaveBirthList()) Then

                            Dim EventName As String = cboEvent.SelectedText
                            Dim printingDll As New SpectrumPrint.clsBirthListNew(clsDefaultConfiguration.BLPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, PrintBirthList.PrintTransactionSet.CreateBirthList, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, BirthListID, CtrlCustSearch1.dtCustmInfo, dtGridData, Nothing, "", EventDate, EventName, 0, clsDefaultConfiguration.BillRoundOffAt, dtPrinterInfo, "Open", Nothing, clsAdmin.TerminalID, ShowFullName:=clsDefaultConfiguration.PrintItemFullName)

                            CreateNewBirthList()
                            AutoLogout(FrmTranCode, Me, lblLoggedIn)
                        End If

                        'Dim resultPrint As DialogResult = ShowMessage(getValueByKey("BL051"), "BL051", MessageBoxButtons.YesNo)
                        'If resultPrint = Windows.Forms.DialogResult.Yes Then
                        '    Dim objPrinting As New PrintBirthList(PrintBirthList.PrintTransactionSet.CreateBirthList, dtCustmInfoBL, dtGridData, Nothing, Nothing, Nothing, BirthListID)
                        '    Dim strErrorMsg As String = String.Empty
                        '    If (objPrinting.Print(strErrorMsg)) Then
                        '        'MessageBox.Show("Printing Finish.")
                        '    Else
                        '        ShowMessage(strErrorMsg)
                        '    End If
                        'End If
                        'Rohit Today

                    End If
                End If
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("BL016"), "BL016 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'MessageBox.Show("BirthList not saved")
        End Try
    End Sub

    Private Sub btnCreateNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateNew.Click
        Try

            Dim resultMsg As DialogResult = MessageBox.Show(getValueByKey("BL017"), "BL017 - " & getValueByKey("CLAE04"), MessageBoxButtons.YesNo)
            If (resultMsg = Windows.Forms.DialogResult.Yes) Then
                CreateNewBirthList()
                lblBirthListNo.Visible = False
                lblCalBirthListNo.Text = String.Empty
                lblCalBirthListNo.Visible = False
                CtrlProductImage1.CtrlProductImages.Image = Nothing
                btnCreateNew.Enabled = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Clear current BirthList information 
    ''' </summary>
    ''' <param name="IsClearCustomerID">Clear CustomerID </param>
    ''' <remarks></remarks>
    Private Sub CreateNewBirthList(Optional ByVal IsClearCustomerID As Boolean = True)
        Try
            CtrlCustSearch1.rbCLPMember.Enabled = True
            CtrlCustSearch1.rbOtherCust.Enabled = True
            dateCDelivery.ValueIsDbNull = True
            dateCDelivery.NullText = String.Empty
            dateCEvent.ValueIsDbNull = True
            dateCEvent.NullText = String.Empty
            lblCalBirthListNo.Text = String.Empty
            ClearBirthList_searching()
            If (IsClearCustomerID) Then
                CtrlCustSearch1.CtrlTxtCustNo.Text = String.Empty
                If Not dtOriginal Is Nothing Then dtOriginal.Clear()
                If Not dtGridData Is Nothing Then dtGridData.Clear()
                IsUpdateChanges = False
                CtrlCustSearch1.CtrlTxtCustNo.Enabled = True
                CtrlCustSearch1.CtrlBtn1.Enabled = True
                If Not CtrlCustDtls1.dtCustmInfo Is Nothing Then
                    CtrlCustDtls1.dtCustmInfo.Clear()
                End If
            End If
            Dim dtDataTable As DataTable = grdItemDetails.DataSource
            If Not dtDataTable Is Nothing And IsClearCustomerID = True Then
                dtDataTable.Clear()
            End If
            grdItemDetails.DataSource = dtDataTable
            Display_GridSetting()
            CalculateTotal()
            objclsBirthList.Generate_BirthListNumber(clsAdmin.TerminalID, OnlineConnect)
            rtxtRemark.Text = ""
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Function CheckUserInput() As Boolean
        Try
            If (cboEvent.SelectedIndex <= -1) Then
                cboEvent.Focus()
                ShowMessage(getValueByKey("BL048"), "BL048 - " & getValueByKey("CLAE04"))
                'MessageBox.Show("Please select Event Name")
                Return False
                Exit Function
            End If
            If Not IsEventDateValid Then
                If Not IsDeliveryDateBackDated Then
                    If IsSalesPersonSelected Then
                        If IsCustomerSelected Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Function ClearBirthList_searching()
        Try
            CtrlCustSearch1.CtrlTxtCustNo.Text = String.Empty
            cboEvent.SelectedIndex = -1
            CtrlSalesPerson1.CtrlSalesPersons.SelectedIndex = -1
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private _strBirthListID As String
    Private Property BirthListID() As String
        Get
            Return _strBirthListID
        End Get
        Set(ByVal value As String)
            _strBirthListID = value
        End Set
    End Property

    Private ReadOnly Property CustomerType() As String
        Get
            Try
                Return CtrlCustDtls1.dtCustmInfo.Rows(0)("CUSTOMERTYPE").ToString()
            Catch ex As Exception
                Return String.Empty
            End Try
        End Get
    End Property

    Private Function SaveBirthList() As Boolean
        Try
            btnCreateNew.Enabled = True
            If cboEvent.SelectedIndex = -1 Then
                cboEvent.Focus()
                ShowMessage(getValueByKey("BL048"), "BL048 - " & getValueByKey("CLAE04"))
                'MessageBox.Show("Select Event Name ")
                Exit Function
            End If
            If (GetCustomerInfo()) Then
                If (CheckGridDataSource()) Then
                    If IsUpdateChanges = False Then
                        If CustomerType <> String.Empty Then
                            objclsBirthList.CustomerType = CustomerType
                        Else
                            'MessageBox.Show("Customer Type is not defined")
                            CtrlCustDtls1.Focus()
                            Exit Function
                        End If
                        objclsBirthList.StockStorageLocation = clsDefaultConfiguration.StockStorageLocation
                        objclsBirthList.SiteCode = clsAdmin.SiteCode
                        objclsBirthList.UsrCode = clsAdmin.UserCode

                        If (objclsBirthList.SaveBirthList(OnlineConnect, clsAdmin.SiteCode, dtCustmInfoBL, clsAdmin.UserName, Nothing, rtxtRemark.Text, clsAdmin.TerminalID)) Then
                            ShowMessage(String.Format(getValueByKey("BL018"), objclsBirthList.BirthListID), "BL018 - " & getValueByKey("CLAE04"))
                            lblBirthListNo.Visible = True
                            BirthListID = objclsBirthList.BirthListID
                            lblCalBirthListNo.Visible = True
                            IsUpdateChanges = True
                            lblCalBirthListNo.Text = objclsBirthList.BirthListID
                            CtrlCustSearch1.CtrlBtn1.Enabled = False
                            CtrlCustSearch1.CtrlTxtCustNo.Enabled = False
                            CtrlCustSearch1.rbCLPMember.Enabled = False
                            CtrlCustSearch1.rbOtherCust.Enabled = False
                            CopyLastReservedQty()
                            dtOriginal = dtGridData.Copy()
                            SetDataRowState_Modified()
                            Return True
                        Else
                            lblBirthListNo.Visible = False
                            lblCalBirthListNo.Visible = False
                            IsUpdateChanges = False
                            ShowMessage(getValueByKey("BL016"), "BL016 - " & getValueByKey("CLAE04"))
                            Return False
                        End If
                    ElseIf IsUpdateChanges = True Then
                        'objclsBirthList.DataTableOriginal = dtOriginal
                        If (objclsBirthList.SaveBirthList(OnlineConnect, clsAdmin.SiteCode, dtCustmInfoBL, clsAdmin.UserName, True, rtxtRemark.Text)) Then
                            SetDataRowState_Modified()
                            CopyLastReservedQty()
                            ShowMessage(String.Format(getValueByKey("BL049"), objclsBirthList.BirthListID), "BL049 - " & getValueByKey("CLAE04"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return True
                        Else
                            Return False
                        End If
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
    Private Function CopyLastReservedQty() As Boolean
        Try
            Dim objReservedQty As Object = dtGridData.Compute("sum(ReservedQty)", " ")
            If Not objReservedQty Is Nothing And Not objReservedQty <= 0 Then
                For Each dr As DataRow In dtGridData.Rows
                    dr("OriginalReservedQty") = dr("ReservedQty")
                Next
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Function SetDataRowState_Modified() As Boolean
        Try
            For Each dr As DataRow In dtOriginal.Rows
                If dr.RowState = DataRowState.Unchanged Then
                    dr.SetModified()
                    Console.WriteLine(dr.RowState.ToString())
                ElseIf dr.RowState = DataRowState.Deleted Then
                    Console.WriteLine(dr.RowState.ToString())
                End If
            Next
            dtGridData.AcceptChanges()
            For Each dr As DataRow In dtGridData.Rows
                If dr.RowState = DataRowState.Unchanged Then
                    dr.SetModified()
                    Console.WriteLine(dr.RowState.ToString())
                ElseIf dr.RowState = DataRowState.Deleted Then
                    Console.WriteLine(dr.RowState.ToString())
                End If
            Next
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
        End Try
    End Function
    Public Sub New()
        Me.FrmTranCode = "BirthListCreate"
        If CheckAuthorisation(clsAdmin.UserCode, "BirthListCreate") = False Then
            ShowMessage(getValueByKey("SO053"), "SO053 - " & getValueByKey("CLAE04"))
            Me.Dispose()
            Me.Close()
            Exit Sub
        End If
        If clsDefaultConfiguration.TillOperationRequired = True And clsDefaultConfiguration.TillOpenDone = False Then
            ShowMessage(getValueByKey("SO052"), "SO052 - " & getValueByKey("CLAE04"))
            Me.Dispose()
            Me.Close()
            Exit Sub
        End If
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        CtrlRbn1.pInitRbn()
        Me.ClientSize = New System.Drawing.Size(gmdiclientwidth, gmdiclientheight)
        CtrlCustSearch1.rbCLPMember.Checked = True

        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            CtrlRbn1.DbtnF12.LargeImage = Global.Spectrum.My.Resources.Resources.PriceChange
            CtrlRbn1.DbtnF2.LargeImage = Global.Spectrum.My.Resources.Resources.ChangeQuantity
        Else
            CtrlRbn1.DbtnF12.LargeImage = Global.Spectrum.My.Resources.Resources.price_change
            CtrlRbn1.DbtnF2.LargeImage = Global.Spectrum.My.Resources.Resources.change_qty
        End If
        
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    'Private Sub txtCustomerID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCustomerID.TextChanged
    '    Try
    '        Dim objClpCustomer As New clsCLPCustomer
    '        Dim _dtCustmInfo As DataTable
    '        Dim strSearchCustomer As String
    '        If rbtnCLPSearch.Checked Then
    '            strSearchCustomer = "CLP"
    '        Else
    '            strSearchCustomer = "SO"
    '        End If
    '        If Not (txtCustomerID.Text = String.Empty AndAlso txtCustomerID.Text.Length > 4) Then
    '            _dtCustmInfo = objClpCustomer.GetCustomerInformation(strSearchCustomer, clsAdmin.SiteCode, txtCustomerID.Text)
    '            If Not _dtCustmInfo Is Nothing Then
    '                If (_dtCustmInfo.Rows.Count > 0) Then
    '                    drSelectedCustomer = _dtCustmInfo.Rows(0)
    '                    Display_CustomerInfo(drSelectedCustomer)
    '                Else
    '                    drSelectedCustomer = Nothing

    '                    ClearBirthList_searching()
    '                End If
    '            Else

    '                drSelectedCustomer = Nothing

    '                ClearBirthList_searching()

    '            End If
    '        Else
    '            drSelectedCustomer = Nothing

    '            ClearBirthList_searching()


    '        End If


    '    Catch ex As Exception

    '    End Try
    'End Sub


    Private Sub grdItemDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdItemDetails.Click

    End Sub

    Private Sub frmNBirthListCreate_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            'If Not dtCustmInfoBL Is Nothing Or Not dtGridData Is Nothing Then
            '    If IsCustomerSelected Then
            '        Dim resultMsg As DialogResult = MessageBox.Show(getValueByKey("BL047"), "BirthList-Create", MessageBoxButtons.YesNo)
            '        If (resultMsg = Windows.Forms.DialogResult.Yes) Then
            '            e.Cancel = False
            '        ElseIf (resultMsg = Windows.Forms.DialogResult.No) Then
            '            e.Cancel = True
            '        End If
            '    Else
            '        e.Cancel = False
            '    End If
            'Else
            '    e.Cancel = False
            'End If

            If grdItemDetails.Rows.Count > 1 Then
                If MsgBox(getValueByKey("SO047"), MsgBoxStyle.YesNo, "SO047 - " & getValueByKey("CLAE04")) = MsgBoxResult.No Then
                    e.Cancel = True
                    isFormClosed = False
                Else
                    isFormClosed = True
                End If
            Else
                isFormClosed = True
            End If

        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub rbbtnstockcheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbbtnstockcheck.Click
        Try
            Dim objfrmStockCheck As New frmNStockCheck
            objfrmStockCheck.ShowDialog()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub rbbtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbbtnEdit.Click
        Me.Close()
        If (isFormClosed) Then
            Dim objfrmNBirthListUpdate As New frmNBirthListUpdate
            MDISpectrum.ShowChildForm(objfrmNBirthListUpdate, True)
        End If
    End Sub
    Protected Overrides Function ProcessKeyPreview(ByRef m As System.Windows.Forms.Message) As Boolean
        Const WM_KEYDOWN As Integer = &H100
        If m.Msg = WM_KEYDOWN Then
            Select Case m.WParam.ToInt32
                Case Keys.F
                    If My.Computer.Keyboard.CtrlKeyDown Then
                        'Debug.Print("Ctrl + F")
                        BtnItemSearch_Click(CtrlSalesPerson1.CtrlTxtBox, New KeyEventArgs(Keys.Enter))
                    End If
                Case Keys.M
                    If My.Computer.Keyboard.CtrlKeyDown Then
                        'Debug.Print("Ctrl + M")
                        CtrlCustDtls1.CtrlLabel3_Click(Nothing, New KeyEventArgs(Keys.Enter))
                    End If
                Case Keys.Q
                    If My.Computer.Keyboard.CtrlKeyDown Then
                        'Debug.Print("Ctrl + Q") ' Direct to Cash Memo
                        ' Create a new instance of the child form.
                        Dim ChildForm As New Spectrum.frmCashMemo
                        If ChildForm.Name <> String.Empty Then
                            MDISpectrum.MenuStrip.Hide()
                            MDISpectrum.ShowChildForm(ChildForm, True)
                            MDISpectrum.MenuStrip.Hide()
                        End If

                    End If

            End Select
        End If
        Return MyBase.ProcessKeyPreview(m)
    End Function



    Private Sub dateCEvent_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles dateCEvent.Leave
        If dateCDelivery.Text = "" And dateCEvent.Value IsNot DBNull.Value Then
            dateCDelivery.Value = dateCEvent.Value
            dateCDelivery.Text = dateCEvent.Text
        End If
    End Sub


    Private Sub rbbtnBLSales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbbtnBLSales.Click
        Me.Close()
        If (isFormClosed) Then
            Dim objfrmNBirthListSales As New frmNBirthListSales
            MDISpectrum.ShowChildForm(objfrmNBirthListSales, True)
        End If
    End Sub

    

    Private Sub frmNBirthListCreate_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "new-birth-list.htm")
            End If
            If e.KeyCode = Keys.F2 Then
                Dim iRowIndex As Integer = 0
                Dim iColumnIdex As Integer = 0
                F2_ChangeSalesQuantity(grdItemDetails, "RequstedQty", "Enter Order Qunatity", iRowIndex, iColumnIdex)
                Dim strChangedValue As String = grdItemDetails.Item(iRowIndex, "ReservedQty")
                If Not ValidateReservedQty(strChangedValue, iColumnIdex, iRowIndex, False) Then
                    grdItemDetails.Item(iRowIndex, iColumnIdex) = 0
                End If
            ElseIf e.KeyCode = Keys.F12 Then

                If Not CtrlCustSearch1.CtrlTxtCustNo.Text = String.Empty Then
                    If clsDefaultConfiguration.PriceChageAllowed Then
                        If CheckInterTransactionAuth("PriceChange") Then
                            If (grdItemDetails.Rows.Count > 1) Then
                                Dim outIRowIndex As Integer = 0
                                Dim outIColumnIndex As Integer = 0
                                F2_ChangeSalesQuantity(grdItemDetails, "Rate", getValueByKey("SP002"), outIRowIndex, outIColumnIndex)
                                Dim strChangeVal As String = grdItemDetails.Cols("Rate")(outIRowIndex)
                                grdItemDetails.DataSource = objclsBirthList.UpdateDataInGrid("Rate", outIRowIndex - 1, strChangeVal)
                                CalculateTotal()
                            Else
                                ShowMessage(getValueByKey("SO012"), "SO012 - " & getValueByKey("CLAE04"))
                            End If
                        End If
                    Else
                        'price change is not allowed '
                        ShowMessage(getValueByKey("BL108"), "BL108 - " & getValueByKey("CLAE04"))
                    End If
                End If
            End If



        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    
 
    Private Sub grdItemDetails_ValidateEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.ValidateEditEventArgs) Handles grdItemDetails.ValidateEdit
        Try
            If Not isFormClosed Then
                If (ValidateQuantity(grdItemDetails, e.Col, "ReservedQty")) Then
                    e.Cancel = True
                ElseIf (ValidateQuantity(grdItemDetails, e.Col, "RequstedQty")) Then 
                    e.Cancel = True  
                End If
            End If 
        Catch ex As Exception
            LogException(ex)
            MsgBox(getValueByKey("CM059"), MsgBoxStyle.Critical, "CM059" & " | " & getValueByKey("CLAE05"))

        End Try
      

    End Sub

    Private Sub PriceChange()
        If Not CtrlCustSearch1.CtrlTxtCustNo.Text = String.Empty Then
            If clsDefaultConfiguration.PriceChageAllowed Then
                If CheckInterTransactionAuth("PriceChange") Then
                    Dim outIRowIndex As Integer = 0
                    Dim outIColumnIndex As Integer = 0
                    F2_ChangeSalesQuantity(grdItemDetails, "Rate", getValueByKey("SP002"), outIRowIndex, outIColumnIndex)
                    Dim strChangeVal As String = grdItemDetails.Cols("Rate")(outIRowIndex)
                    grdItemDetails.DataSource = objclsBirthList.UpdateDataInGrid("Rate", outIRowIndex - 1, strChangeVal)
                    CalculateTotal()
                End If
            Else
                'price change is not allowed '
                ShowMessage(getValueByKey("BL108"), "BL108 - " & getValueByKey("CLAE04"))
            End If
        End If
    End Sub

End Class




 