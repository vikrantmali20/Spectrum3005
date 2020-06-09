Imports System.Windows
Imports System.Resources
Imports System.Globalization
Imports SpectrumBL
Imports System.Text
Imports System.Data.SqlClient
Imports SpectrumPrint

Public Class frmNBirthListUpdate
    Inherits CtrlRbnBaseForm
    Dim vSiteCode As String = clsAdmin.SiteCode
    Private _dsBirthListInfo As DataSet
    Private objclsBirthListUpdate As New clsBirthListUpdate
    Private _dtBirthListItemMergeTable As New DataTable
    Private objclsBirthListGlobal As New clsBirthListGobal
    Private _decOpenAmount As Decimal
    Private objclscomman As New clsCommon
    Private _drBirthListCustomerInformation As DataRow
    Dim dtPaymentReceipt As DataTable
    Dim _TotalItemPurchased As Integer
    Dim _dtVoucherInfo As DataTable
    Dim _IsReprintView As Boolean = False
    Dim dtSoldItemPrice As DataTable
    Dim isFormClosed As Boolean
    Private _dDueDate As Date
    Private _strRemarks As String
    Private dtCheckDtls As New DataTable
    Private decOldReservedQty As Decimal
    Dim RoundedAmt As Double = 0.0

#Region "Not Used "


    'Private Property BirthListInfo() As DataSet
    '    Get
    '        Return _dsBirthListInfo
    '    End Get
    '    Set(ByVal value As DataSet)
    '        _dsBirthListInfo = value
    '    End Set
    'End Property
    'Private ReadOnly Property BirthList_CustomerInfoOLD() As DataTable
    '    Get
    '        Return _dsBirthListInfo.Tables("BirthListCustomerInfo")
    '    End Get
    'End Property
    'Private ReadOnly Property BirthList_CustomerItemInfoOLD() As DataTable
    '    Get
    '        Return _dsBirthListInfo.Tables("BirthListCustomerItemInfo")
    '    End Get
    'End Property
#End Region

    ''' <summary>
    ''' If you want to Reprint of BirthListItem's ,Set this property as 'true'
    ''' </summary>
    ''' <value></value>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Property IsReprintView() As Boolean
        Get
            Return _IsReprintView
        End Get
        Set(ByVal value As Boolean)
            _IsReprintView = value
        End Set
    End Property

    ''' <summary>
    '''  Store BirthList Customer Inforamtion
    ''' </summary>
    ''' <value>DataRow</value>
    ''' <returns>DataRow</returns>
    ''' <remarks>Internal use only</remarks>
    Private Property BirthList_CustomerInformation() As DataRow
        Get
            Return _drBirthListCustomerInformation
        End Get
        Set(ByVal value As DataRow)
            _drBirthListCustomerInformation = value
        End Set
    End Property

    ''' <summary>
    '''  DataSource table to grid 
    ''' </summary>
    ''' <value>DataTable</value>
    ''' <returns>DataTable</returns>
    ''' <remarks>Internal use only</remarks>
    Private Property BirthListItemMergeTable() As DataTable
        Get
            Return _dtBirthListItemMergeTable
        End Get
        Set(ByVal value As DataTable)
            _dtBirthListItemMergeTable = value
        End Set
    End Property

    ''' <summary>
    '''  While giftvoucher printing ,set this property as true  
    ''' </summary>
    ''' <remarks></remarks>
    Private _strGiftReceiptMessage As String
    Public Property GiftReceiptMessage() As String
        Get
            Return _strGiftReceiptMessage
        End Get
        Set(ByVal value As String)
            _strGiftReceiptMessage = value
        End Set
    End Property

    ''' <summary>
    ''' Validate BirthListID
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>ReadOnly</returns>
    ''' <remarks></remarks>
    Private ReadOnly Property BirthListID() As String
        Get
            Try
                If Not POSDBDataSet.BirthList Is Nothing Then
                    If POSDBDataSet.BirthList.Rows.Count > 0 Then
                        If (POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListIdColumn) = String.Empty) Then
                            txtBirthListid.Focus()
                            ShowMessage(getValueByKey("BL034"), "BL034 - " & getValueByKey("CLAE04"))
                            Return String.Empty
                        Else
                            Return POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListIdColumn)
                        End If
                    Else
                        txtBirthListid.Focus()
                        ShowMessage(getValueByKey("BL034"), "BL034 - " & getValueByKey("CLAE04"))
                        'MessageBox.Show(" Please  Select the  BirthList ")
                        Return String.Empty
                    End If
                Else
                    txtBirthListid.Focus()
                    ShowMessage(getValueByKey("BL034"), "BL034 - " & getValueByKey("CLAE04"))
                    'MessageBox.Show(" Please  Select the   BirthList ")
                    Return String.Empty
                End If
            Catch ex As Exception
                LogException(ex)
                Return String.Empty
            End Try
        End Get
    End Property

    ''' <summary>
    '''   birthlist customer id 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private ReadOnly Property BirthListOwnerId() As String
        Get
            Try
                If Not POSDBDataSet.BirthList Is Nothing Then
                    If POSDBDataSet.BirthList.Rows.Count > 0 Then
                        If (POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.CustomerIdColumn) = String.Empty) Then
                            ShowMessage(getValueByKey("BL034"), "BL034 - " & getValueByKey("CLAE04"))
                            Return String.Empty
                        Else
                            Return POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.CustomerIdColumn)
                        End If
                    Else
                        txtBirthListid.Focus()
                        ShowMessage(getValueByKey("BL034"), "BL034 - " & getValueByKey("CLAE04"))
                        'MessageBox.Show(" Please  Select the  BirthList ")
                        Return String.Empty
                    End If
                Else
                    txtBirthListid.Focus()
                    ShowMessage(getValueByKey("BL034"), "BL034 - " & getValueByKey("CLAE04"))
                    'MessageBox.Show(" Please  Select the   BirthList ")
                    Return String.Empty
                End If
            Catch ex As Exception
                LogException(ex)
                Return String.Empty
            End Try
        End Get
    End Property

    ''' <summary>
    '''  BirthList id is entered for searching 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private ReadOnly Property BirthListIDSearch() As String
        Get
            Try

                If (txtBirthListid.Text = String.Empty) Then
                    txtBirthListid.Focus()
                    If Not POSDBDataSet.BirthList Is Nothing AndAlso POSDBDataSet.BirthList.Rows.Count > 0 Then
                        Return " "

                    Else
                        ShowMessage(getValueByKey("BL034"), "BL034 - " & getValueByKey("CLAE04"))
                    End If

                    'MessageBox.Show("Please  Select the   BirthList ")

                Else
                    Return txtBirthListid.Text
                End If
            Catch ex As Exception
                LogException(ex)
                Return String.Empty
            End Try
        End Get
    End Property

    ''' <summary>
    '''  Used at time of OpenAmount calculation
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property OpenAmount() As Decimal
        Get
            Return _decOpenAmount
        End Get
        Set(ByVal value As Decimal)
            _decOpenAmount = value
            CtrlCashSummary1.lbltxt5 = CurrencyFormat(_decOpenAmount)
        End Set
    End Property

    ''' <summary>
    '''  Check Delivery Date 
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>ReadOnly</returns>
    ''' <remarks>Delivery date can not be less than current date</remarks>

    Private ReadOnly Property IsDeliveryDateBackDated() As Boolean
        Get
            If (clsDefaultConfiguration.BLIsCheckDeliveryDate = True) Then

                Try
                    Dim Dateselected As Date = FormatDateTime(c1dateEditDeliverydate.Text)
                    Dim DateCurrent As Date = FormatDateTime(objclscomman.GetCurrentDate(), DateFormat.ShortDate)
                    If (Dateselected >= DateCurrent) Then
                        Return True
                    Else
                        c1dateEditDeliverydate.Focus()
                        ShowMessage(getValueByKey("BL003"), "BL003 - " & getValueByKey("CLAE04"))
                        'MessageBox.Show("Delivery Date cannot be backdated", "DeliveryDate")
                        Return False
                    End If
                Catch ex As Exception
                    c1dateEditDeliverydate.Focus()
                    ShowMessage(getValueByKey("BL004"), "BL004 - " & getValueByKey("CLAE04"))
                    'MessageBox.Show("Please select delivery date")
                    Return False
                End Try
            Else
                Return True
            End If
        End Get
    End Property

    ''' <summary>
    ''' Check for CustomerType i.e CLP,or SO customer
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Property IsCLPCustomer() As Boolean
        Get
            Try
                Dim strCustomerType As String = POSDBDataSet.BirthList.Rows(0)("CustomerType")
                If strCustomerType = "2" Or strCustomerType = "CLP" Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                LogException(ex)
                Return False
            End Try

        End Get
        Set(ByVal value As Boolean)

        End Set
    End Property

    Dim _NewItemAdd As DataRow
    ''' <summary>
    ''' New item adding to db 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property NewItemAdd() As DataRow
        Get
            Return _NewItemAdd
        End Get
        Set(ByVal value As DataRow)
            _NewItemAdd = value
        End Set
    End Property
    ''' <summary>
    ''' Birthlist Event 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _strEventName As String
    Dim _strEventDate As String
    Private Property EventName() As String
        Get
            Return _strEventName
        End Get
        Set(ByVal value As String)
            _strEventName = value
        End Set
    End Property
    ''' <summary>
    ''' Selected Event date for Birthlist
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property EventDate() As String
        Get
            Return _strEventDate
        End Get
        Set(ByVal value As String)
            _strEventDate = value
        End Set
    End Property

    ''' <summary>
    '''  BirthList search by form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 

#Region "Checking is item purchased and Is amount is paid "


    Public ReadOnly Property IsItemPurchased() As Boolean
        Get
            Try
                If Not BirthListItemMergeTable Is Nothing Then
                    If BirthListItemMergeTable.Rows.Count > 0 Then
                        Dim objcountTotalPurchedItem As Object = BirthListItemMergeTable.Compute("count(EAN)", "PurchasedQty > 0")
                        If Not (objcountTotalPurchedItem Is DBNull.Value) Then
                            _TotalItemPurchased = objcountTotalPurchedItem
                            If _TotalItemPurchased > 0 Then
                                Return True
                            Else
                                Return False
                            End If
                        Else
                            Return False
                        End If
                    Else 'Else of Row count 
                        Return False
                    End If
                Else 'Table null
                    Return False
                End If

            Catch ex As Exception
                Return False
            End Try

        End Get

    End Property
    Public ReadOnly Property IsPaymentDone() As Boolean
        Get
            If (IsItemPurchased) Then
                Try
                    Dim decTotalPaymet As Decimal
                    Dim NeedToPayAmount As Decimal
                    Dim descPayedAmount As Decimal
                    decTotalPaymet = IIf(BirthListItemMergeTable.Compute("sum(CurrentPurchasedAmount)", " ") Is DBNull.Value, 0, BirthListItemMergeTable.Compute("sum(CurrentPurchasedAmount)", " "))
                    decTotalPaymet = MyRound(decTotalPaymet, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)
                    If Not dtPaymentReceipt Is Nothing Then
                        If dtPaymentReceipt.Rows.Count > 0 Then
                            descPayedAmount = IIf(dtPaymentReceipt.Compute("sum(Amount)", " ") Is DBNull.Value, 0, dtPaymentReceipt.Compute("sum(Amount)", " "))
                            If (descPayedAmount >= decTotalPaymet) Then
                                Return True
                            Else
                                If (NewOpenAmount > Decimal.Zero) Then
                                    If (NewOpenAmount >= decTotalPaymet) Then
                                        InsertPaymentTransaction_Manually(decTotalPaymet, "OpenAmount", "OpenAmount")
                                        Return True
                                    ElseIf (NewOpenAmount < decTotalPaymet) Then
                                        NeedToPayAmount = decTotalPaymet - NewOpenAmount
                                        InsertPaymentTransaction_Manually(NewOpenAmount, "OpenAmount", "OpenAmount")
                                        Dim descPayedAmount2 As Decimal = IIf(dtPaymentReceipt.Compute("sum(Amount)", " ") Is DBNull.Value, 0, dtPaymentReceipt.Compute("sum(Amount)", " "))
                                        If (descPayedAmount2 = decTotalPaymet) Then
                                            Return True
                                        Else
                                            If (NeedToPayAmount > Decimal.Zero) Then

                                                Dim strMsg As String = String.Format(getValueByKey("BL053"), clsAdmin.CurrencySymbol, NeedToPayAmount)
                                                ShowMessage(strMsg, "BL053 - " & getValueByKey("CLAE04"))
                                                Return False
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            If (NewOpenAmount > Decimal.Zero) Then
                                If (NewOpenAmount >= decTotalPaymet) Then
                                    InsertPaymentTransaction_Manually(decTotalPaymet, "OpenAmount", "OpenAmount")
                                    Return True
                                ElseIf (NewOpenAmount < decTotalPaymet) Then
                                    NeedToPayAmount = decTotalPaymet - NewOpenAmount
                                    InsertPaymentTransaction_Manually(NewOpenAmount, "OpenAmount", "OpenAmount")
                                    Dim descPayedAmount2 As Decimal = IIf(dtPaymentReceipt.Compute("sum(Amount)", " ") Is DBNull.Value, 0, dtPaymentReceipt.Compute("sum(Amount)", " "))
                                    If (descPayedAmount2 = decTotalPaymet) Then
                                        Return True
                                    Else
                                        If (NeedToPayAmount > Decimal.Zero) Then

                                            Dim strMsg As String = String.Format(getValueByKey("BL053"), clsAdmin.CurrencySymbol, NeedToPayAmount)
                                            ShowMessage(strMsg, "BL053 - " & getValueByKey("CLAE04"))
                                            Return False
                                        End If

                                    End If

                                End If

                            Else
                                ShowMessage(getValueByKey("BL038"), "BL038 - " & getValueByKey("CLAE04"))
                                'MessageBox.Show("You need to  do  payment against purchased items ", "Update BirthList")
                                Return False
                            End If

                        End If

                    Else
                        If (NewOpenAmount > Decimal.Zero) Then
                            If (NewOpenAmount >= decTotalPaymet) Then
                                InsertPaymentTransaction_Manually(decTotalPaymet, "OpenAmount", "OpenAmount")
                                Return True
                            ElseIf (NewOpenAmount < decTotalPaymet) Then
                                NeedToPayAmount = decTotalPaymet - NewOpenAmount
                                InsertPaymentTransaction_Manually(NewOpenAmount, "OpenAmount", "OpenAmount")
                                Dim descPayedAmount2 As Decimal = IIf(dtPaymentReceipt.Compute("sum(Amount)", " ") Is DBNull.Value, 0, dtPaymentReceipt.Compute("sum(Amount)", " "))
                                If (descPayedAmount2 = decTotalPaymet) Then
                                    Return True
                                Else
                                    If (NeedToPayAmount > Decimal.Zero) Then

                                        Dim strMsg As String = String.Format(getValueByKey("BL053"), clsAdmin.CurrencySymbol, NeedToPayAmount)
                                        ShowMessage(strMsg, "BL053 - " & getValueByKey("CLAE04"))
                                        Return False
                                    End If

                                End If

                            End If

                        Else

                            ShowMessage(getValueByKey("BL038"), "BL038 - " & getValueByKey("CLAE04"))
                            'MessageBox.Show("You need to  do  payment against purchased items ", "Update BirthList")
                            Return False
                        End If


                    End If

                Catch ex As Exception
                    Return False
                End Try
            Else
                Return True
            End If
        End Get
    End Property

    Public ReadOnly Property AllowToSave() As Boolean
        Get
            Try
                If (IsPaymentDone()) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Get
    End Property
#End Region

    Private IsItemSearch As Boolean = True
    ''' <summary>
    ''' For  Searching birthlist  
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>On Button Click</remarks>
    Private Sub btnSearchBirthListID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchBirthListID.Click, rbnbtnSearchBL.Click
        Try
            '' start here-- Added by rama for OpenAmount to be deducted from TotalPaymentReceived -date 07-Oct-2010
            OpenAmountForBL = 0
            ''End
            Dim objFrmBirthListSearch As New frmNBirthListSearch(True)
            Dim drBirthList As DataRow
            objFrmBirthListSearch.ShowDialog()
            drBirthList = objFrmBirthListSearch.SearchBirthListInformation
            BirthList_CustomerInformation = drBirthList
            If Not (drBirthList Is Nothing) Then
                If Not (drBirthList.Item("BirthListStatus") Is Nothing) Then
                    lblBirthListStatusCal.Text = String.Empty
                    Dim strBirthListID As String = drBirthList("BirthListID")
                    RemoveHandler txtBirthListid.PreviewKeyDown, AddressOf txtBirthListid_PreviewKeyDown
                    txtBirthListid.Text = strBirthListID
                    SearchBirthListOnTextChange()
                    'CalculateTotal()
                    IsItemSearch = True
                    CtrlSalesPerson1.CtrlTxtBox.Enabled = True
                    CtrlSalesPerson1.CtrlTxtBox.Enabled = True
                    c1dateEditDeliverydate.Enabled = True
                    If (Not POSDBDataSet.BirthList Is Nothing) And POSDBDataSet.BirthList.Rows.Count > 0 Then
                        cboEvent.SelectedValue = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.EventIdColumn)
                        EventName = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.EventIdColumn)
                    End If
                    AddHandler txtBirthListid.PreviewKeyDown, AddressOf txtBirthListid_PreviewKeyDown
                Else
                    Dim strStatus As String = String.Format(getValueByKey("Bl102"), drBirthList.Item("BirthListId"), drBirthList.Item("BirthListStatus"))
                    'MessageBox.Show(strStatus, "BirthList")
                    IsItemSearch = False
                    CtrlSalesPerson1.CtrlTxtBox.Enabled = False
                    RemoveHandler txtBirthListid.PreviewKeyDown, AddressOf txtBirthListid_PreviewKeyDown
                    Dim strBirthListID As String = drBirthList("BirthListID")
                    txtBirthListid.Text = strBirthListID
                    BirthListInReadonlyMode()
                    SearchBirthListOnTextChange()
                    OpenAmount = Decimal.Zero
                    CtrlSalesPerson1.CtrlTxtBox.Enabled = False
                    c1dateEditDeliverydate.Enabled = False
                    CalculateTotal()

                    'Modified by Rohit for translation of Document Status values
                    If drBirthList.Item("BirthListStatus").ToString() = "Open" Then
                        lblBirthListStatusCal.Text = getValueByKey("frmnbirthlistupdate.lblbirthliststatuscal1")
                    ElseIf drBirthList.Item("BirthListStatus").ToString() = "Close-DeliveryP" Then
                        lblBirthListStatusCal.Text = getValueByKey("frmnbirthlistupdate.lblbirthliststatuscal3")
                    ElseIf drBirthList.Item("BirthListStatus").ToString() = "Closed" Then
                        lblBirthListStatusCal.Text = getValueByKey("frmnbirthlistupdate.lblbirthliststatuscal2")
                    End If
                    'lblBirthListStatusCal.Text = drBirthList.Item("BirthListStatus").ToString()
                    lblBirthListStatusCal.Tag = drBirthList.Item("BirthListStatus").ToString()
                    'Modify End

                    If (Not POSDBDataSet.BirthList Is Nothing) And POSDBDataSet.BirthList.Rows.Count > 0 Then
                        cboEvent.SelectedValue = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.EventIdColumn)
                    End If
                    AddHandler txtBirthListid.PreviewKeyDown, AddressOf txtBirthListid_PreviewKeyDown
                End If
            End If
            If Not dtPaymentReceipt Is Nothing Then
                dtPaymentReceipt.Clear()
            End If
            If (IsReprintView) Then
                ReprintView()
            End If

            'Added by Rohit for Issue No. 0006126
            grdScanItem.Cols("ReservedQty").Visible = True
            grdScanItem.Cols("ReservedQty").AllowEditing = True
        Catch ex As Exception
            ShowMessage(getValueByKey("BL036"), "BL036 - " & getValueByKey("CLAE04"))
            LogException(ex)
            'MessageBox.Show("Problem for searching BirthListID")
        End Try
    End Sub

    ''' <summary>
    ''' BirthList search by textbox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 

    Private Sub txtBirthListid_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'If txtBirthListid.Text <> String.Empty Then
            '    SearchBirthListOnTextChange()
            '    If Not POSDBDataSet.BirthList Is Nothing Then
            '        If POSDBDataSet.BirthList.Rows.Count > 0 Then
            '            cboEvent.SelectedValue = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.EventIdColumn)
            '        End If
            '    End If
            'End If


        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Search birthlist on text change 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SearchBirthListOnTextChange()
        Try
            Dim strCustType As String = ""

            If (BirthListIDSearch <> String.Empty) Then
                BirthListTableAdapter1.FillBy(POSDBDataSet.BirthList, vSiteCode, BirthListIDSearch)
                If POSDBDataSet.BirthList.Rows.Count > 0 Then
                    lblBirthListStatusCal.Text = String.Empty
                    cboEvent.SelectedValue = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.EventIdColumn)
                    If (POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListStatusColumn) = "Open" Or POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListStatusColumn) = "True") Then
                        BirthList_CustomerInformation = POSDBDataSet.BirthList.Rows(0)
                        c1dateEditEventDate.Value = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.EventDateColumn)
                        c1dateEditDeliverydate.Value = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.DeliveryDateColumn)

                        'Added : Rakesh 01Aug2012
                        Dim strEventID As String = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.EventIdColumn).ToString
                        EventName = objclsBirthListGlobal.GetEventName(strEventID, clsAdmin.SiteCode)

                        EventDate = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.EventDateColumn).ToString
                        CtrlTextBox1.Text = IIf(IsDBNull(POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.RemarksColumn)), "", POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.RemarksColumn))

                        strCustType = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.CustomerTypeColumn)
                        BirthListRequestedItemsTableAdapter1.FillBy(POSDBDataSet.BirthListRequestedItems, vSiteCode, txtBirthListid.Text)

                        'Modified by Rohit for translation of Document Status values
                        'lblBirthListStatusCal.Text = "Open"
                        lblBirthListStatusCal.Text = getValueByKey("frmnbirthlistupdate.lblbirthliststatuscal1")
                        lblBirthListStatusCal.Tag = "Open"
                        'Modify End


                        lblBirthListStatusCal1.ForeColor = Color.ForestGreen
                        If strCustType <> "" Then
                            SearchBirthListInformation()
                        Else
                            ClearBirthListInformation()
                        End If
                        BirthListInNormalMode()
                    Else
                        'check rohit
                        Dim strStatus As String
                        ' Dim strStatus As String = String.Format(getValueByKey("BL102"), POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListIdColumn), POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListStatusColumn))
                        'MessageBox.Show(strStatus, "BirthList") 
                        strStatus = String.Format("Birth-List", POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListIdColumn), POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListStatusColumn))
                        c1dateEditEventDate.Value = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.EventDateColumn)
                        c1dateEditDeliverydate.Value = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.DeliveryDateColumn)



                        strCustType = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.CustomerTypeColumn)
                        BirthListRequestedItemsTableAdapter1.FillBy(POSDBDataSet.BirthListRequestedItems, vSiteCode, BirthListIDSearch)
                        If strCustType <> "" Then
                            SearchBirthListInformation()
                        Else
                            ClearBirthListInformation()
                        End If
                        BirthListInReadonlyMode()

                        'Modified by Rohit for translation of Document Status values
                        'lblBirthListStatusCal.Text = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListStatusColumn).ToString()
                        If POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListStatusColumn).ToString() = "Open" Then
                            lblBirthListStatusCal.Text = getValueByKey("frmnbirthlistupdate.lblbirthliststatuscal1")
                        ElseIf POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListStatusColumn).ToString() = "Close-DeliveryP" Then
                            lblBirthListStatusCal.Text = getValueByKey("frmnbirthlistupdate.lblbirthliststatuscal3")
                        ElseIf POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListStatusColumn).ToString() = "Closed" Then
                            lblBirthListStatusCal.Text = getValueByKey("frmnbirthlistupdate.lblbirthliststatuscal2")
                        End If
                        lblBirthListStatusCal.Tag = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListStatusColumn).ToString()
                        'Modify End

                        lblBirthListStatusCal1.ForeColor = Color.Red
                        rbnbtnSearchBL.Enabled = True
                        CtrlTextBox1.Text = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.RemarksColumn)
                    End If
                Else
                    'lblBirthListStatusCal.Text = "BirthList Not Found ."

                    'Modified by Rohit for translation of Document Status values
                    lblBirthListStatusCal.Text = getValueByKey("BL104")
                    lblBirthListStatusCal.Tag = "BirthList Not Found ."
                    'Modify End

                    lblBirthListStatusCal1.ForeColor = Color.Red
                    BirthListInNormalMode()
                    rbnbtnSearchBL.Enabled = True
                    ClearBirthListInformation()
                End If

            End If
            If Not dtPaymentReceipt Is Nothing Then
                dtPaymentReceipt.Clear()
            End If
            'fnGridColAutoSize(grdScanItem)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Clear BirthList information 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearBirthListInformation()
        Try

            If Not BirthListItemMergeTable Is Nothing Then
                BirthListItemMergeTable.Clear()
            End If
            If Not _dtVoucherInfo Is Nothing Then
                _dtVoucherInfo.Clear()
            End If
            'SearchBirthListInformation()
            If Not dtPaymentReceipt Is Nothing Then
                dtPaymentReceipt.Clear()
            End If
            If Not dtBirthListPaymentHistory Is Nothing Then
                dtBirthListPaymentHistory.Clear()
            End If
            If Not POSDBDataSet.BirthList Is Nothing Then
                POSDBDataSet.BirthList.Clear()
            End If
            If Not dtCheckDtls Is Nothing Then
                dtCheckDtls.Clear()
            End If

            CtrlProductImage1.BackgroundImage = Nothing

            ClearCustomerDtls()
            OpenAmount = Decimal.Zero
            NewOpenAmount = Decimal.Zero
            CtrlCashSummary1.lbltxt4 = Decimal.Zero

            CalculateTotal()
            txtBirthListid.Text = ""

            'CtrlTextBox1.Clear()
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub


    ''' <summary>
    '''  Save birthlist chages to db
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Dim dtCustomerINformation As DataTable
    Private Sub btnSavePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveAndPrint.Click
        Try
            'ValidateEditColumnEntry(grdScanItem.Cols(grdScanItem.Col).Name, grdScanItem.Row)
            If SaveAndPrint(True) Then
                AutoLogout(FrmTranCode, Me, lblLoggedIn)
            End If

        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Save and print selected birthlist 
    ''' </summary>
    ''' <returns>On success , true</returns>
    ''' <remarks></remarks>
    Private Function SaveAndPrint(Optional ByVal CloseFlag As Boolean = False) As Boolean
        Try
            Dim objclsBirthListSalesSave As New clsBirthListSalesSave
            If CloseFlag = False Then
                txtBirthListid.Text = BirthListID
                txtBirthListid.Enabled = False
            End If
            If Not (ValidateUserInput()) Then
                Return False
                Exit Function
            End If
            If Not IsReprintView Then
                If (AllowToSave) Then
                    Try
                        Dim sb As New StringBuilder
                        sb.Append(CtrlCashSummary1.lbltxt5) 'open amount'
                        sb.Replace(clsAdmin.CurrencySymbol.ToString(), "")
                        Dim decFinalOpenAmounValue As String = CDbl(sb.ToString())
                        Dim dateEventdate As Date = c1dateEditEventDate.Value
                        Dim dateDeliverydate As Date = c1dateEditDeliverydate.Value
                        POSDBDataSet.BirthList.Rows(0).BeginEdit()
                        POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.EventIdColumn) = cboEvent.SelectedValue
                        POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.EventDateColumn) = dateEventdate
                        POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.DeliveryDateColumn) = dateDeliverydate
                        POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.OpenAmountColumn) = decFinalOpenAmounValue
                        POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.RemarksColumn) = CtrlTextBox1.Text
                        POSDBDataSet.BirthList.Rows(0).EndEdit()
                        Dim iIndex As Integer = 0
                        Dim iReservedQty As Integer
                        For Each dr As DataRow In BirthListItemMergeTable.Rows
                            If Not (dr.RowState = DataRowState.Deleted) Then
                                Dim strExpression As String = POSDBDataSet.BirthListRequestedItems.EANColumn.ToString() + " = " + dr("EAN")
                                Dim dr_BirthListRequestedItemsRow As POSDBDataSet.BirthListRequestedItemsRow = POSDBDataSet.BirthListRequestedItems.FindBySiteCodeBirthListIdEANSrNo(clsAdmin.SiteCode, dr("BirthListId"), dr("EAN"), dr("SrNo"))
                                If Not dr Is Nothing And Not dr_BirthListRequestedItemsRow Is Nothing Then
                                    dr_BirthListRequestedItemsRow.BeginEdit()
                                    'POSDBDataSet.BirthListRequestedItems.Rows(iIndex)(POSDBDataSet.BirthListRequestedItems.SiteCodeColumn) = dr("SiteCode")
                                    'POSDBDataSet.BirthListRequestedItems.Rows(iIndex)(POSDBDataSet.BirthListRequestedItems.BirthListIdColumn) = dr("BirthListId")
                                    dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.ArticleCodeColumn) = dr("ArticleCode")
                                    'POSDBDataSet.BirthListRequestedItems.Rows(iIndex)(POSDBDataSet.BirthListRequestedItems.EANColumn) = dr("EAN")
                                    dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.RequstedQtyColumn) = dr("RequstedQty")
                                    If IsDBNull(dr("PickUpQty")) Then
                                        dr("PickUpQty") = 0
                                    End If
                                    If IsDBNull(dr("DeliveredQty")) Then
                                        dr("DeliveredQty") = 0
                                    End If
                                    If IsDBNull(dr("OpenAmountQty")) Then
                                        dr("OpenAmountQty") = 0
                                    End If
                                    If IsDBNull(dr("CurrentReturnQty")) Then
                                        dr("CurrentReturnQty") = 0
                                    End If
                                    If IsDBNull(dr("ReservedQty")) Then
                                        dr("ReservedQty") = 0
                                    End If

                                    If (dr("CurrentReturnQty") > 0) Then
                                        If dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.ReturnQtyColumn) Is DBNull.Value Then
                                            dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.ReturnQtyColumn) = 0
                                            dr("ReturnQty") = 0
                                        End If
                                        dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.ReturnQtyColumn) = dr("CurrentReturnQty") + dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.ReturnQtyColumn)
                                        dr("ReturnQty") = dr("ReturnQty") + dr("CurrentReturnQty")
                                        dr("ReturnReason") = dr("CurrentReturnReason")
                                        dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.ReturnReasonColumn) = dr("CurrentReturnReason")
                                    End If
                                    dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.BookedQtyColumn) = dr("bookedqty") + dr("PurchasedQty")
                                    dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.DeliveredQtyColumn) = dr("DeliveredQty") + dr("PickUpQty") - dr("CurrentReturnQty")
                                    dr("DeliveredQty") = dr("DeliveredQty") + dr("PickUpQty") - dr("CurrentReturnQty")
                                    dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.ConvToVoucherColumn) = dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.ConvToVoucherColumn) + dr("OpenAmountQty")
                                    dr("ConvToVoucher") = dr("ConvToVoucher") + dr("OpenAmountQty")
                                    dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.AuthUserIdColumn) = DBNull.Value
                                    dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.AuthUserRemarksColumn) = DBNull.Value
                                    dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.UPDATEDATColumn) = clsAdmin.SiteCode
                                    dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.UPDATEDBYColumn) = clsAdmin.UserName
                                    dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.UPDATEDONColumn) = objclscomman.GetCurrentDate()
                                    dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.FreeTextsColumn) = dr("FreeTexts")
                                    dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.IsCLPColumn) = dr("IsCLP")
                                    If Not dr("ReservedQty") Is DBNull.Value Or dr("ReservedQty") Is Nothing Then
                                        iReservedQty = dr("ReservedQty") - dr("PickUpQty")
                                        If iReservedQty < 0 Then
                                            iReservedQty = 0
                                        End If
                                    End If

                                    dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.ReservedQtyColumn) = iReservedQty
                                    dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.SellingPriceColumn) = dr("SellingPrice")
                                    dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.OriginalSellingPriceColumn) = dr("OriginalSellingPrice")
                                    dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.IsPriceChangedColumn) = dr("IsPriceChanged")
                                    'dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.SrNoColumn) = objclsBirthListSalesSave.GetSrNo(BirthListID, dr("ArticleCode"), dr("Ean")) + 1
                                    dr_BirthListRequestedItemsRow(POSDBDataSet.BirthListRequestedItems.SrNoColumn) = dr("SrNo")
                                    dr_BirthListRequestedItemsRow.EndEdit()
                                Else
                                    Dim dr_BirthListRequestedItemsRow_ADD As POSDBDataSet.BirthListRequestedItemsRow = POSDBDataSet.BirthListRequestedItems.NewRow()
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.EANColumn) = dr("EAN")
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.FinYearColumn) = dr("FinYear")
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.SiteCodeColumn) = dr("SiteCode")
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.BirthListIdColumn) = dr("BirthListId")
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.ArticleCodeColumn) = dr("ArticleCode")
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.RequstedQtyColumn) = dr("RequstedQty")
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.BookedQtyColumn) = dr("PurchasedQty")
                                    If IsDBNull(dr("PickUpQty")) Then
                                        dr("PickUpQty") = 0
                                    End If
                                    If IsDBNull(dr("DeliveredQty")) Then
                                        dr("DeliveredQty") = 0
                                    End If
                                    If IsDBNull(dr("OpenAmountQty")) Then
                                        dr("OpenAmountQty") = 0
                                    End If
                                    If IsDBNull(dr("CurrentReturnQty")) Then
                                        dr("CurrentReturnQty") = 0
                                    End If
                                    If IsDBNull(dr("ReservedQty")) Then
                                        dr("ReservedQty") = 0
                                    End If
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.DeliveredQtyColumn) = dr("DeliveredQty") + dr("PickUpQty")
                                    dr("DeliveredQty") = dr("DeliveredQty") + dr("PickUpQty")
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.ConvToVoucherColumn) = dr("OpenAmountQty")
                                    dr("ConvToVoucher") = dr("OpenAmountQty")
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.CLPPointsColumn) = Decimal.Zero

                                    If (dr("CurrentReturnQty") > 0) Then
                                        dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.ReturnQtyColumn) = dr("CurrentReturnQty")
                                        dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.ReturnReasonColumn) = dr("CurrentReturnReason")
                                    End If
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.AuthUserIdColumn) = DBNull.Value
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.AuthUserRemarksColumn) = DBNull.Value
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.UPDATEDATColumn) = clsAdmin.SiteCode
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.UPDATEDBYColumn) = clsAdmin.UserName
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.UPDATEDONColumn) = objclscomman.GetCurrentDate()
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.CREATEDATColumn) = dr("CREATEDAT")
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.CREATEDBYColumn) = dr("CREATEDBY")
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.CREATEDONColumn) = dr("CREATEDON")
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.STATUSColumn) = True
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.FreeTextsColumn) = dr("FreeTexts")
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.IsCLPColumn) = dr("IsCLP")
                                    If Not dr("ReservedQty") Is DBNull.Value Or dr("ReservedQty") Is Nothing Then
                                        iReservedQty = dr("ReservedQty") - dr("PickUpQty")
                                        If iReservedQty < 0 Then
                                            iReservedQty = 0
                                        End If
                                    End If

                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.ReservedQtyColumn) = iReservedQty
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.SellingPriceColumn) = dr("SellingPrice")
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.OriginalSellingPriceColumn) = dr("SellingPrice")
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.IsPriceChangedColumn) = dr("IsPriceChangedHere")
                                    dr_BirthListRequestedItemsRow_ADD(POSDBDataSet.BirthListRequestedItems.SrNoColumn) = dr("SrNo") 'objclsBirthListSalesSave.GetSrNo(BirthListID, dr("ArticleCode"), dr("Ean")) + 1
                                    POSDBDataSet.BirthListRequestedItems.Rows.Add(dr_BirthListRequestedItemsRow_ADD)
                                End If
                            End If
                        Next


                        objclsBirthListSalesSave.FinacialYear = clsAdmin.Financialyear
                        objclsBirthListSalesSave.SiteCode = clsAdmin.SiteCode
                        objclsBirthListSalesSave.TerminalID = clsAdmin.TerminalID
                        objclsBirthListSalesSave.UserName = clsAdmin.UserName
                        objclsBirthListSalesSave.BirthLisID = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListIdColumn)
                        objclsBirthListSalesSave.IsUpdateBirthList = True
                        objclsBirthListSalesSave.IsCLPCustomer = IsCLPCustomer
                        objclsBirthListSalesSave.IsCLPCalculate = False
                        objclsBirthListSalesSave.StockStorageLocation = clsDefaultConfiguration.StockStorageLocation
                        'objclsBirthListSalesSave.supplier = clsDefaultConfiguration.SupplierCode
                        objclsBirthListSalesSave.DataTableTaxDetails = dtMainTax
                        objclsBirthListSalesSave.DateDayOpen = clsAdmin.DayOpenDate
                        If Not BirthListItemMergeTable Is Nothing Then
                            If (BirthListItemMergeTable.Rows.Count > 0) Then
                                Dim iPurchaseQty As Integer = BirthListItemMergeTable.Compute("sum(PurchasedQty)", "")
                                If iPurchaseQty > 0 Then
                                    BirthListItemMergeTable.Rows(0)("SalesExecutiveCode") = CtrlSalesPerson1.CtrlSalesPersons.SelectedValue
                                End If
                            End If
                        End If
                        Dim iOpenAmount As Integer
                        Try
                            iOpenAmount = BirthListItemMergeTable.Compute("sum(OpenAmountQty)", "")
                        Catch ex As Exception

                        End Try
                        If iOpenAmount > 0 Then
                            UpdateInformationForSalesDetails()
                        End If

                        Dim objClpCustomer As New clsCLPCustomer
                        Dim strCustomerType As String
                        If (IsCLPCustomer) Then
                            strCustomerType = "CLP"
                            'Added because flag was acting blocker in clp calculation
                            IsCLPCalculate = True
                        Else
                            strCustomerType = "SO"
                        End If

                        objclsBirthListSalesSave.CustomerID = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.CustomerIdColumn)
                        Dim posAdpCustomerInfo As New POSDBDataSetTableAdapters.CustomerInformationTableAdapter
                        'Dim dtCustomerINformation As DataTable = posAdpCustomerInfo.GetDataByCustomerIdSiteCode(objclsBirthListSalesSave.CustomerID, clsAdmin.SiteCode)

                        dtCustomerINformation = objClpCustomer.GetCustomerInformation(strCustomerType, clsAdmin.SiteCode, clsAdmin.CLPProgram, objclsBirthListSalesSave.CustomerID)
                        If Not dtCustomerINformation Is Nothing And dtCustomerINformation.Rows.Count > 0 Then
                            objclsBirthListSalesSave.SelectedCustomerInfo = dtCustomerINformation.Rows(0)
                            If BirthListItemMergeTable.Rows.Count > 0 Then
                                If BirthListItemMergeTable.Compute("sum(PurchasedQty)", " ") > 0 And IsCLPCustomer = True And IsCLPCalculate = True Then
                                    Dim strCardType As String = dtCustomerINformation.Rows(0)("CardType")
                                    Dim strFilterCriteria As String = "PurchasedQty > 0 and CLPRequire=true "
                                    AddCLPColumns()
                                    'CalCulateCLP(strCardType, BirthListItemMergeTable, strFilterCriteria)
                                    BirthListItemMergeTable.Columns.Add("FirstLevel")
                                    BirthListItemMergeTable.Columns("ISCLp").ColumnName = "CLPRequire"
                                    CalCulateCLPSlabwise(dtCustomerINformation(0)("CARDTYPE"), BirthListItemMergeTable, strFilterCriteria, dtCustomerINformation(0)("CUSTOMERNO"), dtPaymentReceipt)
                                    BirthListItemMergeTable.Columns("CLPRequire").ColumnName = "ISCLp"
                                Else
                                    AddCLPColumns()
                                End If
                            Else
                                AddCLPColumns()
                            End If
                        Else
                            'ShowMessage("Customer Information is not availabel.", "Customer Selection")
                            ShowMessage(getValueByKey("BL109"), "Customer Selection")
                            Return False
                        End If




                        If BirthListItemMergeTable.Rows.Count > 0 Then
                            objclsBirthListSalesSave.PaidAmount = MyRound(CDbl(IIf(BirthListItemMergeTable.Compute("sum(CurrentPurchasedAmount)", " ") Is DBNull.Value, 0, BirthListItemMergeTable.Compute("sum(CurrentPurchasedAmount)", " "))), clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)
                        End If
                        objclsBirthListSalesSave.IsPrintHeader = False
                        objclsBirthListSalesSave.IsPrintFooter = False
                        objclsBirthListSalesSave.DataTablePaymentHistory = dtPaymentReceipt
                        objclsBirthListSalesSave.DataTableCheckDtls = dtCheckDtls
                        objclsBirthListSalesSave.DataTableBirthListItemDetail = BirthListItemMergeTable
                        objclsBirthListSalesSave.SelectedBirthListInfo = BirthList_CustomerInformation
                        objclsBirthListSalesSave.CVProgramID = clsAdmin.CVProgram
                        objclsBirthListSalesSave.CreditVoucherVaildDays = clsAdmin.CreditValidDays

                        Dim strPickUpQty As String = String.Empty
                        Dim IsTransactionSuccess As Boolean

                        Dim sqlTran As SqlClient.SqlTransaction = Nothing
                        Dim IsCLPTransactionSuccess As Boolean = False
                        objclsBirthListSalesSave.dDueDate = _dDueDate
                        objclsBirthListSalesSave.strRemarks = _strRemarks
                        objclsBirthListSalesSave.PreviousFinancialYear = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.FinYearColumn)

                        Dim str As String = objclsBirthListSalesSave.Save(OnlineConnect, RoundedAmt, strPickUpQty, IsTransactionSuccess, POSDBDataSet.BirthListRequestedItems, POSDBDataSet.BirthList, BirthListRequestedItemsTableAdapter1, BirthListTableAdapter1, IsCLPTransactionSuccess, dtOpenQtyBirthListSalesDtl, AdpOpenQtyBirthListSalesDtl)

                        If (IsTransactionSuccess) Then
                            For Each dr As DataRow In BirthListItemMergeTable.Rows
                                If String.IsNullOrEmpty(dr("PurchasedQty")) Then
                                    dr("SellingPrice") = dr("NetAmount")
                                Else
                                    If dr("PurchasedQty") > 0 Then
                                        dr("SellingPrice") = dr("NetAmount") / dr("PurchasedQty")
                                    Else
                                        'dr("SellingPrice") = dr("NetAmount")
                                    End If
                                End If
                            Next
                            PrintSalesDetails(objclsBirthListSalesSave.GenNewSaleInVoiceNumber(clsAdmin.TerminalID, OnlineConnect), objclsBirthListSalesSave.OrderDocumentNumber, objclsBirthListSalesSave.PaymentGV)
                            'ClearBirthListInformation()
                            ShowMessage(getValueByKey("BL037"), "BL037 - " & getValueByKey("CLAE04"))
                            If CloseFlag = True Then
                                ClearBirthListInformation()
                            Else
                                SearchBirthListOnTextChange()
                            End If
                            'If Not (strPickUpQty = String.Empty) Then
                            '    MessageBox.Show(getValueByKey("BL037"), "BL037")
                            '    'MessageBox.Show("BirthList succesfully saved. ", "Update BirthList")
                            '    'PrintSalesDetails(objclsBirthListSalesSave.GenNewSaleInVoiceNumber, objclsBirthListSalesSave.OrderDocumentNumber)
                            '    Dim resultPrint As DialogResult = MessageBox.Show(getValueByKey("BL051"), "Print:'" & objclsBirthListSalesSave.GenNewSaleInVoiceNumber & "'", MessageBoxButtons.YesNo)
                            '    If resultPrint = Windows.Forms.DialogResult.Yes Then
                            '        fnPrint(strPickUpQty, "PRN")
                            '        If Not (str = String.Empty) Then
                            '            fnPrint(str, "PRN")
                            '        End If
                            '    End If
                            '    Me.Close()
                            'Else
                            '    MessageBox.Show(getValueByKey("BL037"), "BL037")
                            '    'MessageBox.Show("BirthList succesfully saved. ", "Update BirthList")

                            '    If Not (str = String.Empty) Then
                            '        Dim resultPrint As DialogResult = MessageBox.Show(getValueByKey("BL051"), " BL051" & objclsBirthListSalesSave.GenNewSaleInVoiceNumber & "'", MessageBoxButtons.YesNo)
                            '        If resultPrint = Windows.Forms.DialogResult.Yes Then
                            '            fnPrint(str, "PRN")
                            '        End If
                            '    End If
                            'End If
                            If (IsCLPTransactionSuccess) Then
                                'MessageBox.Show("CLP Transaction success ", "Update BirthList")
                            End If
                            If POSDBDataSet.BirthList.Rows.Count > 0 Then
                                cboEvent.SelectedValue = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.EventIdColumn)
                            End If

                            Return True
                        Else
                            ShowMessage(getValueByKey("BL016"), "BL016 - " & getValueByKey("CLAE04"))
                            Return False
                            'MessageBox.Show("BirthList  Not saved. ", "Update BirthList")
                        End If
                        cboEvent.SelectedValue = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.EventIdColumn)

                    Catch ex As Exception
                        ClearBirthListInformation()
                        ShowMessage(ex.Message, getValueByKey("CLAE05"))
                        Return False
                    End Try
                Else

                    Return False

                End If
            Else
                'Dim objclsBirthListSalesSave As New clsBirthListSalesSave
                objclsBirthListSalesSave.FinacialYear = clsAdmin.Financialyear
                objclsBirthListSalesSave.SiteCode = clsAdmin.SiteCode
                objclsBirthListSalesSave.TerminalID = clsAdmin.TerminalID
                objclsBirthListSalesSave.UserName = clsAdmin.UserName
                objclsBirthListSalesSave.BirthLisID = BirthListIDSearch
                objclsBirthListSalesSave.CustomerID = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.CustomerIdColumn)
                Dim posAdpCustomerInfo As New POSDBDataSetTableAdapters.CustomerInformationTableAdapter
                Dim objClpCustomer As New clsCLPCustomer
                Dim strCustomerType As String
                If (IsCLPCustomer) Then
                    strCustomerType = "CLP"
                Else
                    strCustomerType = "SO"
                End If
                Dim dtCustomerINformation As DataTable = objClpCustomer.GetCustomerInformation(strCustomerType, clsAdmin.SiteCode, clsAdmin.CLPProgram, objclsBirthListSalesSave.CustomerID)
                objclsBirthListSalesSave.SelectedCustomerInfo = dtCustomerINformation.Rows(0)
                objclsBirthListSalesSave.DataTableBirthListItemDetail = BirthListItemMergeTable
                objclsBirthListSalesSave.SelectedBirthListInfo = BirthList_CustomerInformation
                Dim strPickUpQty As String = String.Empty
                Dim str As String = objclsBirthListSalesSave.PrintBirthListPickedQty(True)
                If Not (str = String.Empty) Then
                    Dim resultPrint As DialogResult = MessageBox.Show(getValueByKey("BL051"), "BL051 - " & getValueByKey("CLAE04") & objclsBirthListSalesSave.GenNewSaleInVoiceNumber(clsAdmin.TerminalID, OnlineConnect) & "'", MessageBoxButtons.YesNo)
                    If resultPrint = Windows.Forms.DialogResult.Yes Then
                        fnPrint(str, "PRN")
                    End If
                End If
            End If

        Catch ex As Exception
            ClearBirthListInformation()
            ShowMessage(getValueByKey("BL016"), "BL016 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'MessageBox.Show("Problem in saving birthlist details")
        End Try
    End Function

    ''' <summary>
    '''  Used for calculation of reserved qty
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function CopyLastReservedQty() As Boolean
        Try
            Dim objReservedQty As Object = BirthListItemMergeTable.Compute("sum(ReservedQty)", " ")
            If Not objReservedQty Is Nothing And Not objReservedQty <= 0 Then
                For Each dr As DataRow In BirthListItemMergeTable.Rows
                    dr("OriginalReservedQty") = dr("ReservedQty")
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    ''' <summary>
    '''  Validated user provided inouts.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function ValidateUserInput() As Boolean
        Try
            If Not BirthListID <> String.Empty Then
                Return False
                Exit Function
            End If
            If clsDefaultConfiguration.BLIsCheckDeliveryDate Then
                If Not (IsDeliveryDateBackDated) Then
                    Return False
                    Exit Function
                End If
            End If

            If clsDefaultConfiguration.BLIsSalesPersonApplicable = True Then
                Try
                    If (CtrlSalesPerson1.CtrlSalesPersons.SelectedIndex <= -1) Then
                        ShowMessage(getValueByKey("BL002"), "BL002 - " & getValueByKey("CLAE04"))
                        Return False
                        Exit Function
                    End If
                Catch ex As Exception
                    ShowMessage(getValueByKey("BL002"), "BL002 - " & getValueByKey("CLAE04"))
                    Return False
                End Try
            End If

            If (cboEvent.SelectedIndex <= -1) Then
                cboEvent.Focus()
                ShowMessage(getValueByKey("BL048"), "BL048 - " & getValueByKey("CLAE04"))
                'MessageBox.Show("Please select Event Name")
                Return False
                Exit Function
            End If

            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    ''' <summary>
    '''  Printing Birthlist invoice,Status Document.
    ''' </summary>
    ''' <param name="strInvoiceNumber">Invoice Number</param>
    ''' <param name="strOrderNumber">Ordered Number </param>
    ''' <param name="dtTempGv">Information of GiftVoucher's against selected Birth List    </param>
    ''' <returns>On Success ,true</returns>
    ''' <remarks></remarks>
    Private Function PrintSalesDetails(ByVal strInvoiceNumber As String, ByVal strOrderNumber As String, ByVal dtTempGv As DataTable) As Boolean
        Try
            If Not BirthListItemMergeTable Is Nothing Then
                If BirthListItemMergeTable.Rows.Count > 0 Then
                    Dim iPurchsedQty As Integer = BirthListItemMergeTable.Compute("sum(purchasedQty)", "")
                    Dim iPickupQty As Integer = BirthListItemMergeTable.Compute("sum(PickupQty)", "")

                    If (iPurchsedQty > 0 Or iPickupQty > 0) Then
                        Dim dtCustomerDetails As New DataTable
                        If Not BirthList_CustomerInformation Is Nothing Then
                            dtCustomerDetails = BirthList_CustomerInformation.Table.Clone()
                            dtCustomerDetails.ImportRow(BirthList_CustomerInformation)
                            If (dtCustomerDetails.Columns.Contains("CustomerId")) Then
                                dtCustomerDetails.Columns("CustomerId").ColumnName = "CustomerNO"
                            End If
                        End If

                        Dim clsVoucher As Object
                            clsVoucher = New clsPrintVoucherNew
                        

                        If Not dtTempGv Is Nothing AndAlso dtTempGv.Rows.Count > 0 Then
                            Dim dv As New DataView(dtTempGv, "", "VOURCHERSERIALNBR", DataViewRowState.CurrentRows)
                            If dv.Count > 0 Then

                                For Each dr As DataRowView In dv
                                    'objPrint.PrintGiftVoucher(dr("VOURCHERSERIALNBR").ToString(), dr("ValueOfVoucher").ToString(), CDate(IIf(dr("ExpiryDate") Is DBNull.Value, Now, dr("ExpiryDate"))), DateDiff(DateInterval.Day, dr("ExpiryDate"), dr("issuedondate")))
                                    clsVoucher.PrintGiftVoucherAndCreditNote("BLS", clsAdmin.SiteCode, "GiftVoucher", dr("VOURCHERSERIALNBR"), dr("ValueOfVoucher"), CDate(IIf(dr("ExpiryDate") Is DBNull.Value, Now, dr("ExpiryDate"))), clsAdmin.UserName, BirthListID, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                                Next
                            End If
                        End If
                        If Not dtPaymentReceipt Is Nothing AndAlso dtPaymentReceipt.Rows.Count > 0 Then
                            For Each dr As DataRow In dtPaymentReceipt.Select("RecieptTypeCode='CreditVouc(I)'", "", DataViewRowState.CurrentRows)
                                Dim TotalPay As Decimal = IIf(dr("Amount") > 0, dr("Amount"), dr("Amount") * -1)
                                'objPrint.PrintVoucher("CMS", TotalPay, clsDefaultConfiguration.VoucherText, clsAdmin.SiteCode, Errormsg, clsAdmin.CurrencyCode)
                                clsVoucher.PrintGiftVoucherAndCreditNote("BLS", clsAdmin.SiteCode, "CreditNote", dr("Number").ToString(), TotalPay, String.Empty, clsAdmin.UserName, BirthListID, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                            Next
                        End If





                        'Dim objclsPrinting As New PrintBirthList(PrintBirthList.PrintTransactionSet.EditBirthListItem, dtCustomerINformation, BirthListItemMergeTable, Nothing, BirthList_CustomerInformation, dtPaymentReceipt, strInvoiceNumber, strOrderNumber, False)
                        'Dim strErrorMsg As String = ""
                        'If (objclsPrinting.Print(strErrorMsg)) Then
                        'Else
                        '    'MessageBox.Show(strErrorMsg)
                        'End If

                        'Added : Rakesh 08Aug2012
                        Dim EventNameValue As String = objclsBirthListGlobal.GetEventName(EventName, clsAdmin.SiteCode)

                        Dim objclsPrintingdll As New SpectrumPrint.clsBirthListNew(clsDefaultConfiguration.BLPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, clsBirthListNew.PrintBLTransactionSet.EditBirthListItem, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, BirthListID, dtCustomerINformation, BirthListItemMergeTable, strInvoiceNumber, strOrderNumber, Nothing, BirthList_CustomerInformation, dtPaymentReceipt, False, EventDate, EventNameValue, "", 0, clsDefaultConfiguration.BillRoundOffAt, dtPrinterInfo, "", dtMainTax, clsAdmin.TerminalID, ShowFullName:=clsDefaultConfiguration.PrintItemFullName)
                        
                        'Dim printingDll As New SpectrumPrint.clsBirthList(SpectrumPrint.clsBirthList.PrintBLTransactionSet.BirthListStatus, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserCode, BirthListID, dtCustomerINformation, BirthListItemMergeTable)

                        'If Not dtPaymentReceipt Is Nothing AndAlso dtPaymentReceipt.Rows.Count > 0 Then
                        '    Dim TotalPay As Decimal
                        '    Dim dateCV As Date
                        '    For Each dr As DataRow In dtPaymentReceipt.Select("RecieptTypeCode='CreditVouc(I)'", "", DataViewRowState.CurrentRows)
                        '        TotalPay = IIf(dr("Amount") > 0, dr("Amount"), dr("Amount") * -1)
                        '        dateCV = dr("Date")
                        '        objclsPrinting.PrintVoucher(dateCV, "CMS", BirthListID, clsAdmin.UserCode, CDbl(TotalPay))

                        '    Next
                        'End If
                        If isGIftVoucherDocumentPrint Then
                            Dim objclsPrintingdll2 As New SpectrumPrint.clsBirthListNew(clsDefaultConfiguration.BLPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, clsBirthListNew.PrintBLTransactionSet.GiftVoucher, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, BirthListID, dtCustomerINformation, BirthListItemMergeTable, strInvoiceNumber, strOrderNumber, Nothing, BirthList_CustomerInformation, dtPaymentReceipt, False, EventDate, EventName, GiftReceiptMessage, 0, clsDefaultConfiguration.BillRoundOffAt, dtPrinterInfo, "", Nothing, clsAdmin.TerminalID, ShowFullName:=clsDefaultConfiguration.PrintItemFullName)
                            

                            isGIftVoucherDocumentPrint = False
                        End If

                        'Dim objPrintDll As New SpectrumPrint.clsBirthList(SpectrumPrint.clsBirthList.PrintBLTransactionSet.BirthListStatus, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserCode, BirthListID, dtCustomerDetails, BirthListItemMergeTable, _dtVoucherInfo)

                        Return True
                    Else
                        Return True
                    End If
                End If
            End If

        Catch ex As Exception
            LogException(ex)
            ShowMessage(getValueByKey("BL015"), "BL015 - " & getValueByKey("CLAE04"))
            'MessageBox.Show("Printing Problem")
        End Try
    End Function
    ''' <summary>
    ''' Adding CLP Colums(CLPPoints,CLPDiscount) into orginal DataTable (BirthListItemMergeTable)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddCLPColumns() As Boolean
        Try

            If Not (BirthListItemMergeTable.Columns.Contains("CLPPoints")) Then
                Dim clnCLPPoints As New DataColumn
                clnCLPPoints.ColumnName = "CLPPoints"
                clnCLPPoints.DataType = System.Type.GetType("System.Decimal")
                clnCLPPoints.DefaultValue = Decimal.Zero

                BirthListItemMergeTable.Columns.Add(clnCLPPoints)
                Return True
            ElseIf Not (BirthListItemMergeTable.Columns.Contains("CLPDiscount")) Then
                Dim clnCLPDiscount As New DataColumn
                clnCLPDiscount.ColumnName = "CLPDiscount"
                clnCLPDiscount.DataType = System.Type.GetType("System.Decimal")
                BirthListItemMergeTable.Columns.Add(clnCLPDiscount)
                Return True
            Else
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Function ClearBirthListPaymentHistory() As Boolean

        Try
            For Each dr As DataRow In BirthListItemMergeTable.Rows
                If (dr("PurchasedQty") > 0 Or dr("PickUpQty") > 0 Or dr("CurrentReturnQty") > 0) Then
                    dr.BeginEdit()
                    dr("PurchasedQty") = 0
                    dr("PickUpQty") = 0
                    dr("CurrentReturnQty") = 0
                    dr("CurrentReturnReason") = String.Empty
                    dr("CurrentPurchasedAmount") = Decimal.Zero
                    dr.EndEdit()
                End If

            Next
            txtBirthListid.Text = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListIdColumn)
            SearchBirthListInformation()
            CalculateTotal()
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    ''' <summary>
    ''' Display customer information
    ''' </summary>
    ''' <remarks></remarks> 
    ''' 

    Private Sub pPopulateCustomerDtls()
        Try
            If Not POSDBDataSet.BirthList Is Nothing Then
                If (POSDBDataSet.BirthList.Rows.Count > 0) Then
                    Dim objclsClpCustomer As New clsCLPCustomer
                    Dim dtSOCustomer As DataTable = objclsClpCustomer.GetCustomerInformation(POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.CustomerTypeColumn), clsAdmin.SiteCode, clsAdmin.CLPProgram, POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.CustomerIdColumn))
                    If Not dtSOCustomer Is Nothing Then
                        CtrlCustDtls1.pDisplayDtls(dtSOCustomer)
                    Else
                        ClearCustomerDtls()
                    End If
                Else
                    ClearCustomerDtls()
                End If
            Else
                ClearCustomerDtls()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    ''' <summary>
    ''' Clear the Birthlist customer information 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearCustomerDtls()
        Try
            CtrlCustDtls1.dtCustmInfo.Clear()
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Display customer address 
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    'Private Sub pPopulateAddress()
    '    Try
    '        AssignDataSourceAddress()
    '        If Not POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.CustomerTypeColumn) = "CLP" Then
    '            If (POSDBDataSet.BirthList.Rows.Count > 0 And Not POSDBDataSet.BirthList Is Nothing) Then
    '                CustomerAddressTableAdapter1.FillBy(POSDBDataSet.CustomerAddress, vSiteCode, POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.CustomerIdColumn))
    '                cboAddressType.SelectedValue = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.DeliveryAddressTypeColumn)
    '            Else
    '                cboAddressType.SelectedValue = ""
    '            End If
    '        Else
    '            If (POSDBDataSet.BirthList.Rows.Count > 0 And Not POSDBDataSet.BirthList Is Nothing) Then

    '                Dim adpCLPCustomerAddressDataTable As New POSDBDataSetTableAdapters.CLPCustomerAddressTableAdapter
    '                adpCLPCustomerAddressDataTable.FillBy(POSDBDataSet.CLPCustomerAddress, POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.CustomerIdColumn))
    '                'POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.CustomerTypeColumn)

    '                cboAddressType.SelectedValue = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.DeliveryAddressTypeColumn)
    '            Else
    '                cboAddressType.SelectedValue = ""
    '            End If
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    ''' <summary>
    '''  Display Birthlist items  in to  grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    Private Sub pPopulateItemDtls()
        Try
            grdScanItem.DataSource = POSDBDataSet.BirthListArticleDtls
            'grdScanItem.AutoSizeCols(grdScanItem.TopRow, grdScanItem.LeftCol, grdScanItem.BottomRow, grdScanItem.RightCol, 10, C1.Win.C1FlexGrid.AutoSizeFlags.IgnoreHidden)
            'grdScanItem.Cols("DISCRIPTION").Width = grdScanItem.Width * 0.4
            'grdScanItem.Cols("DISCRIPTION").WidthDisplay = grdScanItem.Width * 0.4
            grdScanItem.AutoSizeCols()

        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    'Private Sub cboAddressType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboAddressType.SelectedIndexChanged
    '    Try
    '        AssignDataSourceAddress()
    '        If POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.CustomerTypeColumn) = "CLP" Then
    '            If POSDBDataSet.CLPCustomers.Rows.Count > 0 Then
    '                CustomerAddressBindingSource.MoveFirst()
    '                For intRow = 0 To POSDBDataSet.CustomerAddress.Rows.Count - 1
    '                    If POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.DeliveryAddressTypeColumn) = cboAddressType.SelectedValue Then
    '                        Exit For
    '                        'lblCalAddress.Text = POSDBDataSet.CustomerAddress.Rows(intRow)(POSDBDataSet.CustomerAddress.AddressLn1Column) & POSDBDataSet.CustomerAddress.Rows(intRow)(POSDBDataSet.CustomerAddress.AddressLn2Column) & POSDBDataSet.CustomerAddress.Rows(intRow)(POSDBDataSet.CustomerAddress.AddressLn3Column) & POSDBDataSet.CustomerAddress.Rows(intRow)(POSDBDataSet.CustomerAddress.AddressLn4Column)
    '                    End If
    '                    CustomerAddressBindingSource.MoveNext()
    '                Next
    '            Else

    '            End If
    '        Else
    '            If POSDBDataSet.CustomerAddress.Rows.Count > 0 Then
    '                CustomerAddressBindingSource.MoveFirst()
    '                For intRow = 0 To POSDBDataSet.CustomerAddress.Rows.Count - 1
    '                    If POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.DeliveryAddressTypeColumn) = cboAddressType.SelectedValue Then
    '                        Exit For
    '                        'lblCalAddress.Text = POSDBDataSet.CustomerAddress.Rows(intRow)(POSDBDataSet.CustomerAddress.AddressLn1Column) & POSDBDataSet.CustomerAddress.Rows(intRow)(POSDBDataSet.CustomerAddress.AddressLn2Column) & POSDBDataSet.CustomerAddress.Rows(intRow)(POSDBDataSet.CustomerAddress.AddressLn3Column) & POSDBDataSet.CustomerAddress.Rows(intRow)(POSDBDataSet.CustomerAddress.AddressLn4Column)
    '                    End If
    '                    CustomerAddressBindingSource.MoveNext()
    '                Next
    '            Else

    '            End If
    '        End If


    '    Catch ex As Exception

    '    End Try
    'End Sub

    ''' <summary>
    ''' Change DataSource for Combo:AddressType and CustomerAddressBindingSource on CustomerType
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 

    Private Function AssignDataSourceAddress() As Boolean

        'Try
        '    If POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.CustomerTypeColumn) = "CLP" Then
        '        Me.CustomerAddressBindingSource.DataMember = "CLPCustomerAddress"
        '        cboAddressType.DataSource = POSDBDataSet.CLPCustomerAddress
        '        cboAddressType.DisplayMember = POSDBDataSet.CLPCustomerAddress.AddTypeDescColumn.ColumnName
        '        cboAddressType.ValueMember = POSDBDataSet.CLPCustomerAddress.AddressTypeColumn.ColumnName

        '    Else
        '        Me.CustomerAddressBindingSource.DataMember = "CustomerAddress"
        '        cboAddressType.DataSource = POSDBDataSet.CustomerAddress
        '        cboAddressType.DisplayMember = POSDBDataSet.CustomerAddress.AddTypeDescColumn.ColumnName
        '        cboAddressType.ValueMember = POSDBDataSet.CustomerAddress.AddressTypeColumn.ColumnName

        '    End If

        'Catch ex As Exception

        'End Try

    End Function

    ''' <summary>
    '''  Search BirthListInformation
    ''' </summary>
    ''' <remarks></remarks>
    Dim dtMainTax As DataTable
    Private Sub SearchBirthListInformation()
        Try
            If Not (BirthListIDSearch = String.Empty) Then
                Dim birthItem As New POSDBDataSetTableAdapters.BirthListArticleDtlsTableAdapter

                'birthItem.ClearBeforeFill = True
                'POSDBDataSet.AcceptChanges()
                Dim dtbirthItem As New POSDBDataSet.BirthListArticleDtlsDataTable

                birthItem.FillByBirthListId(dtbirthItem, clsAdmin.LangCode, clsAdmin.SiteCode, BirthListIDSearch)
                BirthListItemMergeTable = dtbirthItem

                GetBirthListItemDetails()
                BirthListItemMergeTable_AddColumns()
                'Change for CR 5679
                'Calling function to set the appropriate selling price based on DefaultConfig and price change during creation
                SetArticlePrices()
                'End of change
                BirthListItemMergeTable = BirthListItemMergeTable.Select("RequstedQty > 0").CopyToDataTable()
                grdScanItem.DataSource = BirthListItemMergeTable

                If (clsDefaultConfiguration.BarcodeDisplayAllowed) Then
                    grdScanItem.Cols("EAN").Caption = getValueByKey("frmnbirthlistupdate.grdscanitem.ean")
                    grdScanItem.Cols("EAN").Width = 90
                    grdScanItem.Cols("EAN").AllowEditing = False
                    grdScanItem.Cols("EAN").Visible = True
                Else
                    grdScanItem.Cols("EAN").Visible = False
                End If

                grdScanItem.AutoSizeCols()

                DisplayBirthListPaymentHistory()
                DisplayGVInforamtion()
                pPopulateCustomerDtls()

                Dim decTotalValue As Decimal = Decimal.Zero
                If POSDBDataSet.BirthList.Rows.Count > 0 Then
                    OpenAmount = Decimal.Zero
                    NewOpenAmount = Decimal.Zero
                    Dim objOpenAmount As Object = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.OpenAmountColumn)
                    If Not objOpenAmount Is DBNull.Value Then
                        OpenAmount = CDbl(objOpenAmount)
                        NewOpenAmount = _decOpenAmount
                    End If

                    If BirthListItemMergeTable.Rows.Count > 0 Then
                        decTotalValue = BirthListItemMergeTable.Compute("sum(NetAmount)", " ")
                    End If

                    ' this is comment to get the actual total amount paid by all birthlist buyer including the tax amount)
                    'CtrlCashSummary1.lbltxt4 = CurrencyFormat(decTotalValue)
                    ' end of about comment

                    CtrlCashSummary1.lbltxt4 = CurrencyFormat(lblCalTotalAmountPaymentHistory.Text)

                    Dim strEventID As String = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.EventIdColumn)
                    cboEvent.SelectedValue = 1
                Else
                    OpenAmount = Decimal.Zero
                    NewOpenAmount = Decimal.Zero
                    CtrlCashSummary1.lbltxt4 = Decimal.Zero
                End If
                Dim objClsComman As New clsCommon
                dtMainTax = objClsComman.getTax(clsAdmin.SiteCode, "", "", 0, "")
                dtMainTax.TableName = "BirthListTax"
                'Dim iDeliveryItemCount As Integer = BirthListItemMergeTable.Compute("sum(DeliveredQty)", "")
                'If (iDeliveryItemCount > 0) Then
                '    btnReturns.Enabled = False
                'Else
                '    btnReturns.Enabled = True
                'End If
                CalculateTotal()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Adding Birthlist return Columns  to Table ( BirthListItemMergeTable)
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    Private Sub BirthListItemMergeTable_AddColumns()
        Try
            Dim dcConvToVoucher As DataColumn = BirthListItemMergeTable.Columns("ConvToVoucher")
            dcConvToVoucher.DefaultValue = 0
            Dim dcOpenAmountQty As DataColumn = New DataColumn("OpenAmountQty", System.Type.GetType("System.Double"))
            dcOpenAmountQty.DefaultValue = 0

            Dim dcOriginalReservedQty As DataColumn = New DataColumn("OriginalReservedQty", System.Type.GetType("System.Double"))
            dcOriginalReservedQty.DefaultValue = 0


            Dim dcOpenAmountQtyAmount As DataColumn = New DataColumn("OpenAmountQtyAmount", System.Type.GetType("System.Decimal"))
            dcOpenAmountQtyAmount.DefaultValue = 0
            Dim dcIsOpenAmountQtyAmountPaid As DataColumn = New DataColumn("IsOpenAmountQtyAmountPaid", System.Type.GetType("System.Boolean"))
            dcIsOpenAmountQtyAmountPaid.DefaultValue = False
            Dim dcCurrentReturnQty As DataColumn = New DataColumn("CurrentReturnQty", System.Type.GetType("System.Double"))
            'dcConvToVoucher.DataType = System.Type.GetType("System.UInt32")
            dcCurrentReturnQty.DefaultValue = 0

            Dim dcSalesExecutiveCode As DataColumn = New DataColumn("SalesExecutiveCode")
            'dcConvToVoucher.DataType = System.Type.GetType("System.UInt32") 

            Dim dcCurrentReturnReason As DataColumn = New DataColumn("CurrentReturnReason")

            'Change for CR 5679
            Dim dcIsPriceChangedHere As DataColumn = New DataColumn("IsPriceChangedHere", System.Type.GetType("System.Boolean"))
            dcIsPriceChangedHere.DefaultValue = False
            BirthListItemMergeTable.Columns.Add(dcIsPriceChangedHere)

            Dim dcActualSellingPrice As DataColumn = New DataColumn("ActualSellingPrice", System.Type.GetType("System.Decimal"))
            dcActualSellingPrice.DefaultValue = 0.0
            BirthListItemMergeTable.Columns.Add(dcActualSellingPrice)

            For Each dr As DataRow In BirthListItemMergeTable.Rows
                dr("ActualSellingPrice") = dr("SellingPrice")
            Next
            'End of change

            ' add to freeze
            'Dim dcFreezeOB As DataColumn = New DataColumn("FreezeOB", System.Type.GetType("System.Boolean"))
            'dcFreezeOB.DefaultValue = False
            'Dim dcFreezeSB As DataColumn = New DataColumn("FreezeSB", System.Type.GetType("System.Boolean"))
            'dcFreezeSB.DefaultValue = False
            'end freeze 

            BirthListItemMergeTable.Columns.Add(dcOriginalReservedQty)
            BirthListItemMergeTable.Columns.Add(dcCurrentReturnQty)
            BirthListItemMergeTable.Columns.Add(dcSalesExecutiveCode)
            BirthListItemMergeTable.Columns.Add(dcCurrentReturnReason)
            BirthListItemMergeTable.Columns.Add(dcOpenAmountQty)
            BirthListItemMergeTable.Columns.Add(dcOpenAmountQtyAmount)
            BirthListItemMergeTable.Columns.Add(dcIsOpenAmountQtyAmountPaid)
            'BirthListItemMergeTable.Columns.Add(dcFreezeSB)
            'BirthListItemMergeTable.Columns.Add(dcFreezeOB)
            CopyLastReservedQty() ' Reserved  Quantity copied to temp 
            Dim posdtBirthListSoldItemPrice As New POSDBDataSet.BirthListSoldItemPriceDataTable

            Dim posADPBirthListSoldItemPriceDataTable As New POSDBDataSetTableAdapters.BirthListSoldItemPriceTableAdapter

            If Not dtSoldItemPrice Is Nothing Then
                dtSoldItemPrice.Clear()
            End If
            posADPBirthListSoldItemPriceDataTable.Fill(posdtBirthListSoldItemPrice, clsAdmin.SiteCode, BirthListIDSearch, clsAdmin.Financialyear)

            dtSoldItemPrice = posdtBirthListSoldItemPrice



        Catch ex As Exception

            ShowMessage(getValueByKey("BL054"), "BL054 - " & getValueByKey("CLAE04"))
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Display giftvoucher information for selected Birthlist .
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 

    Private Function DisplayGVInforamtion() As Boolean
        Try
            Dim adpVoucherInfo As New POSDBDataSetTableAdapters.BirthListVoucherInfoTableAdapter
            Dim strMsg As String = String.Empty
            Dim obBirthListGlobal As New clsBirthListGobal
            Dim dtEAN As DataTable = obBirthListGlobal.RetrieveQuery("SELECT Top 1 B.EAN,B.ARTICLECODE FROM MSTVOUCHER A LEFT OUTER JOIN MSTEAN B ON A.ARTICLECODE=B.ARTICLECODE WHERE A.VourcherType='GiftVoucher(I)'", strMsg)
            Dim EAN As String = String.Empty
            Dim ArticleCode As String = String.Empty
            If Not dtEAN Is Nothing Then
                EAN = dtEAN.Rows(0)("EAN")
                ArticleCode = dtEAN.Rows(0)("ARTICLECODE")
            End If
            _dtVoucherInfo = adpVoucherInfo.GetData(ArticleCode, EAN, BirthListIDSearch, clsAdmin.SiteCode)
            gridGV.DataSource = _dtVoucherInfo
            Dim decGVCalTotalAmout As Decimal = Decimal.Zero
            If Not _dtVoucherInfo Is Nothing Then
                If _dtVoucherInfo.Rows.Count > 0 Then
                    decGVCalTotalAmout = _dtVoucherInfo.Compute("sum(NetAmt)", " ")
                    lblGVCalTotalAmout.Text = CurrencyFormat(decGVCalTotalAmout)
                End If
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Form load event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 

    Private NewOpenAmount As Decimal

    Private Sub frmBirthListUpdation_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            AddHandler CtrlRbn1.DbtnF12.Click, AddressOf PriceChange

            If Not IsReprintView = True Then

                grdScanItem.Cols("CurrentPurchasedAmount").Format = GridAmountColumnCustomeFormat()
                grdScanItem.Cols("SellingPrice").Format = GridAmountColumnCustomeFormat()
                grdScanItem.Cols("NetAmount").Format = GridAmountColumnCustomeFormat()
                grdScanItem.DataSource = BirthListItemMergeTable
                AddHandler txtBirthListid.PreviewKeyDown, AddressOf txtBirthListid_PreviewKeyDown
            Else
                ReprintView()
                AddHandler txtBirthListid.PreviewKeyDown, AddressOf txtBirthListid_PreviewKeyDown
            End If

            Dim objclsBirthlistdefaultsetting As New clsDefaultConfiguration("BLS")
            objclsBirthlistdefaultsetting.GetDefaultSettings()
        Catch ex As Exception
            ShowMessage(getValueByKey("BL055"), "BL055 - " & getValueByKey("CLAE04"))
            LogException(ex)
            'MessageBox.Show("Loading problem")
        End Try
    End Sub

    ''' <summary>
    ''' Reprint of Selected BirthListItems
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function ReprintView() As Boolean

        Try
            'lblItem.Visible = False
            CtrlSalesPerson1.CtrlTxtBox.Visible = False
            CtrlSalesPerson1.CtrlCmdSearch.Visible = False
            'btnAcceptPayment.Enabled = False
            btnClose.Enabled = False
            gridGV.AllowEditing = False
            gridGV.AllowDelete = False
            gridPaymentHistory.AllowEditing = False
            gridPaymentHistory.AllowDelete = False
            grdScanItem.AllowDelete = False
            grdScanItem.AllowEditing = False
            btnReturns.Enabled = False
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    ''' <summary>
    '''  Calculate TotalBalance Quantity and NetAmount 
    ''' </summary>
    ''' <returns>On Success, True</returns>
    ''' <remarks></remarks>
    ''' 

    Private Function GetBirthListItemDetails() As Boolean
        Try
            Dim dtNewItemTable As DataTable
            Dim strErrorMsg As String = ""
            dtNewItemTable = BirthListItemMergeTable
            Dim dtBirthListArticleDtls As New POSDBDataSet.BirthListArticleDtlsDataTable
            Dim posAdaptorBirthListArticleDtls As New POSDBDataSetTableAdapters.BirthListArticleDtlsTableAdapter
            posAdaptorBirthListArticleDtls.FillByBirthListId(dtBirthListArticleDtls, clsAdmin.LangCode, clsAdmin.SiteCode, BirthListIDSearch)

            dtNewItemTable = objclsBirthListGlobal.CreateBirthListItemTable(dtBirthListArticleDtls)

            dtNewItemTable = objclsBirthListGlobal.CalculateTotalBalanceQty(dtNewItemTable, True, strErrorMsg)
            'true:Calculate Total Amount only pass from this form
            If Not (strErrorMsg = String.Empty) Then
                ShowMessage(strErrorMsg, getValueByKey("CLAE05"))
                Exit Function
            End If
            BirthListItemMergeTable = dtNewItemTable
            Return True
        Catch ex As Exception
            ShowMessage(getValueByKey("BL056"), "BL056 - " & getValueByKey("CLAE04"))
            LogException(ex)
            'MessageBox.Show("Problem for CustomeStructured table with query table.")
            Return False
        End Try
    End Function

    Public Sub New()

        If CheckAuthorisation(clsAdmin.UserCode, "BirthListUpdate") = False Then
            ShowMessage(getValueByKey("BL087"), "BL087 - " & getValueByKey("CLAE04"))
            Me.Dispose()
            Me.Close()
            Exit Sub
        End If

        If clsDefaultConfiguration.TillOperationRequired = True And clsDefaultConfiguration.TillOpenDone = False Then
            ShowMessage(getValueByKey("BL088"), "BL088 - " & getValueByKey("CLAE04"))
            Me.Dispose()
            Me.Close()
            Exit Sub
        End If

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.ClientSize = New System.Drawing.Size(gmdiclientwidth, gmdiclientheight)

        CtrlRbn1.DbtnF12.LargeImage = Global.Spectrum.My.Resources.Resources.price_change
        CtrlRbn1.DbtnF2.LargeImage = Global.Spectrum.My.Resources.Resources.change_qty

        Try
            'lblCustomer.Text = gResourceMgr.GetString("frmBirthListUpdate_lbl_Customer", gCI)
            'lblCustomerName.Text = gResourceMgr.GetString("frmBirthListUpdate_lbl_CustomerName", gCI)


            'lblAddressType.Text = gResourceMgr.GetString("frmBirthListUpdate_lbl_AddressType", gCI)
            'lblCustomerAddress.Text = gResourceMgr.GetString("frmBirthListUpdate_lbl_CustomerAddress", gCI)


            ' ''lblPinCode.Text = gResourceMgr.GetString("frmBirthListUpdate_lbl_PinCode", gCI)

            ' ''lblResidencePhone.Text = gResourceMgr.GetString("frmBirthListUpdate_lbl_CustomerName", gCI)

            'grpBoxCustomerInfo.Text = gResourceMgr.GetString("frmBirthListUpdate_grpBox_CustomerInfo", gCI)

            'lblBirthListID.Text = gResourceMgr.GetString("frmBirthListUpdate_lbl_BirthListID", gCI)


            'lblDeliveryDate.Text = gResourceMgr.GetString("frmBirthListUpdate_lbl_DeliveryDate", gCI)
            'lblAddress.Text = gResourceMgr.GetString("frmBirthListUpdate_lbl_Address", gCI)
            'lblBirthDate.Text = gResourceMgr.GetString("frmBirthListUpdate_lbl_BirthDate", gCI)

            'lblTotalValue.Text = gResourceMgr.GetString("frmBirthListUpdate_lbl_TotalValue", gCI)
            'lblOpenAmount.Text = gResourceMgr.GetString("frmBirthListUpdate_lbl_OpenAmount", gCI)
            'grpBoxBirthListInfo.Text = gResourceMgr.GetString("frmBirthListUpdate_grpBox_BirthListInfo", gCI)

            'grpBoxSummary.Text = gResourceMgr.GetString("frmBirthListUpdate_grpBox_Summary", gCI)

            'btnClose.Text = gResourceMgr.GetString("frmBirthListUpdate_btn_Close", gCI)
            'btnSaveAndPrint.Text = gResourceMgr.GetString("frmBirthListUpdate_btn_SaveAndPrint", gCI)
            'btnAcceptPayment.Text = gResourceMgr.GetString("frmBirthListUpdate_btn_AcceptPayment", gCI)
            'tbPageGV.Text = gResourceMgr.GetString("frmBirthListUpdate_tbPage_GV", gCI)
            'tbPageOpen.Text = gResourceMgr.GetString("frmBirthListUpdate_tbPage_Open", gCI)
            'tbPagePaymentHistory.Text = gResourceMgr.GetString("frmBirthListUpdate_tbPage_PaymentHistory", gCI)

            'grdScanItem.Cols("EAN").Caption = gResourceMgr.GetString("frmBirthListUpdate_grid_Column_EAN", gCI)

            'grdScanItem.Cols("DISCRIPTION").Caption = gResourceMgr.GetString("frmBirthListUpdate_grid_Column_DISCRIPTION", gCI)
            'grdScanItem.Cols("SellingPrice").Caption = gResourceMgr.GetString("frmBirthListUpdate_grid_Column_SellingPrice", gCI)


            'grdScanItem.Cols("RequstedQty").Caption = gResourceMgr.GetString("frmBirthListUpdate_grid_Column_RequstedQty", gCI)
            'grdScanItem.Cols("BookedQty").Caption = gResourceMgr.GetString("frmBirthListUpdate_grid_Column_BookedQty", gCI)

            'grdScanItem.Cols("DeliveredQty").Caption = gResourceMgr.GetString("frmBirthListUpdate_grid_Column_DeliveredQty", gCI)
            'grdScanItem.Cols("BalanceItemQty").Caption = gResourceMgr.GetString("frmBirthListUpdate_grid_Column_BalanceItemQty", gCI)

            'grdScanItem.Cols("ConvToVoucher").Caption = gResourceMgr.GetString("frmBirthListUpdate_grid_Column_ConvToVoucher", gCI)
            'grdScanItem.Cols("PickUpQty").Caption = gResourceMgr.GetString("frmBirthListUpdate_grid_Column_PickUpQty", gCI)

            'grdScanItem.Cols("NetAmount").Caption = gResourceMgr.GetString("frmBirthListUpdate_grid_Column_NetAmount", gCI)

        Catch ex As Exception
            ShowMessage(getValueByKey("BL058"), "BL058 - " & getValueByKey("CLAE04"))
            LogException(ex)
            'MessageBox.Show("Problem to load language resource file.", "BirthListUpdate")
        End Try
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    ''' <summary>
    ''' Grid Event for AfterEdit 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 

    Private Sub grdScanItem_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdScanItem.AfterEdit
        Try
            Dim grid As C1.Win.C1FlexGrid.C1FlexGrid = sender
            Dim strEditColumnName As String = grid.Cols(e.Col).Name
            ValidateEditColumnEntry(strEditColumnName, e.Row)
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Validate user inputs for Editable columns into grid 
    ''' </summary>
    ''' <param name="strColumnName">Column Name for validation</param>
    ''' <param name="iRowIndex">Row index </param>
    ''' <remarks></remarks>

    Private Sub ValidateEditColumnEntry(ByVal strColumnName As String, ByVal iRowIndex As Integer)
        Try
            Dim ibookedqtyBeforeChange As Integer
            Dim iChangedOrderedQty As Integer
            If Not (IsDBNull_ColumnValue(grdScanItem.Rows(iRowIndex), "BookedQty")) Then
                ibookedqtyBeforeChange = ReturnColumnValue(iRowIndex, "BookedQty")
            End If
            Select Case strColumnName
                Case "RequstedQty"
                    If (IsDBNull_ColumnValue(grdScanItem.Rows(iRowIndex), "RequstedQty")) Then
                        grdScanItem.Rows(iRowIndex)("RequstedQty") = 1
                    End If

                    iChangedOrderedQty = ReturnColumnValue(iRowIndex, "RequstedQty")
                    If (iChangedOrderedQty < ibookedqtyBeforeChange) Then
                        ShowMessage(getValueByKey("BL057"), "BL057 - " & getValueByKey("CLAE04"))
                        'MessageBox.Show("You can't enter Ordered Quantity less than Sold Quantity")
                        grdScanItem.Rows(iRowIndex)("RequstedQty") = ibookedqtyBeforeChange
                    ElseIf (iChangedOrderedQty = 0) Then
                        ShowMessage(getValueByKey("BL059"), "BL059 - " & getValueByKey("CLAE04"))
                        grdScanItem.Rows(iRowIndex)("RequstedQty") = 1
                    End If

                    Dim iBookedQty As Integer = ReturnColumnValue(iRowIndex, "BookedQty")
                    Dim iOderedQty As Integer = ReturnColumnValue(iRowIndex, "RequstedQty")
                    Dim iBalanceQty As Integer = iOderedQty - iBookedQty
                    Dim iPurchedQty As Integer = ReturnColumnValue(iRowIndex, "PurchasedQty")
                    Dim iSellingPrice As Decimal = ReturnColumnValue(iRowIndex, "SellingPrice")
                    Dim iReservedQty As Integer = ReturnColumnValue(iRowIndex, "ReservedQty")

                    'Rakesh-26.08.2013:Issue-7763-->Order qty cann't less than Reserved qty
                    If (iOderedQty < iReservedQty) Then
                        grdScanItem.Rows(iRowIndex)("ReservedQty") = iOderedQty
                    End If

                    If (iPurchedQty > iBalanceQty) Then
                        grdScanItem.Rows(iRowIndex)("PurchasedQty") = 0
                        BirthListItemMergeTable.Rows(iRowIndex - 1)("PurchasedQty") = 0
                        grdScanItem.Rows(iRowIndex)("CurrentPurchasedAmount") = Decimal.Zero
                        Validate_PurchasedQty(Decimal.Zero)
                    End If
                    objclsBirthListGlobal.CalculateTotalBalanceQty(BirthListItemMergeTable, True)
                    Validate_PickUpQty(iRowIndex, ibookedqtyBeforeChange)

                Case "PickUpQty"
                    Dim iPurchasedQty As Integer
                    If Not (IsDBNull_ColumnValue(grdScanItem.Rows(iRowIndex), "PurchasedQty")) Then
                        iPurchasedQty = ReturnColumnValue(iRowIndex, "PurchasedQty")
                    End If
                    If (ibookedqtyBeforeChange Or iPurchasedQty > 0) Then

                        If Not grdScanItem.Rows(iRowIndex)("FreezeOB") Is Nothing AndAlso Not grdScanItem.Rows(iRowIndex)("FreezeOB") Is DBNull.Value Then
                            If grdScanItem.Rows(iRowIndex)("FreezeOB") = True Then
                                ' Note :FreezeOB stands for delivery of an item is not valid  as per rama jena
                                ShowMessage(getValueByKey("BL104"), "BL104 - " & getValueByKey("BL104"))
                                grdScanItem.Item(iRowIndex, "PickUpQty") = 0
                                Exit Sub
                            Else

                                'Rakesh-01.10.2013:7835-->Check article stock balance quantity
                                If (Not clsDefaultConfiguration.NegativeInventoryAllowed) Then
                                    Dim objCommon As New clsCommon
                                    Dim articleCode = grdScanItem.Item(iRowIndex, "ArticleCode")
                                    Dim articleEAN = grdScanItem.Item(iRowIndex, "EAN")
                                    Dim iPickUpQty = grdScanItem.Item(iRowIndex, "PickUpQty")

                                    Dim StockQty As Double = objCommon.GetStocks(clsAdmin.SiteCode, articleEAN, articleCode, True)

                                    If (StockQty < iPickUpQty) Then
                                        ShowMessage(String.Format(getValueByKey("SB015"), StockQty), "SB015 - " & getValueByKey("CLAE04"))
                                        grdScanItem.Item(iRowIndex, "PickUpQty") = 0
                                        Exit Sub
                                    End If
                                End If

                                Validate_PickUpQty(iRowIndex, ibookedqtyBeforeChange)
                            End If
                        End If

                    Else
                        'MessageBox.Show(getValueByKey("BL059"), getValueByKey("CLAE04"))
                        'MessageBox.Show("There is no sold item available.so you can't pickup this item")
                        ShowMessage(getValueByKey("BL105"), "BL105 - " & getValueByKey("CLAE04"))
                        grdScanItem.Rows(iRowIndex)("PickUpQty") = 0
                    End If
                Case "OpenAmountQty"
                    Dim iPurchedQty As Integer = ReturnColumnValue(iRowIndex, "PurchasedQty")
                    If Not iPurchedQty > 0 Then

                        Dim iPurchasedQty As Integer
                        If Not (IsDBNull_ColumnValue(grdScanItem.Rows(iRowIndex), "PurchasedQty")) Then
                            iPurchasedQty = ReturnColumnValue(iRowIndex, "PurchasedQty")
                        End If
                        If (ibookedqtyBeforeChange > 0) Then
                            Validate_OpenAmountQty(iRowIndex, ibookedqtyBeforeChange)
                        Else
                            ShowMessage(getValueByKey("BL060"), "BL060 - " & getValueByKey("CLAE04"))
                            'MessageBox.Show("There is no sold item and current purchased item available.so you can't convert this item amount into Open Amount.")
                            grdScanItem.Rows(iRowIndex)("OpenAmountQty") = 0
                            'Validate_OpenAmountQty(iRowIndex, ibookedqtyBeforeChange)
                        End If
                    Else
                        ShowMessage(getValueByKey("BL061"), "BL061 - " & getValueByKey("CLAE04"))
                        'MessageBox.Show(" You can't   Purchase item and Convert Sold item amount into OpenAmount for same Item ")
                        grdScanItem.Rows(iRowIndex)("OpenAmountQty") = 0
                        Validate_OpenAmountQty(iRowIndex, ibookedqtyBeforeChange)
                    End If
                Case "PurchasedQty"
                    Try

                        If Not (grdScanItem.Item(iRowIndex, "FreezeSB") Is DBNull.Value) Then
                            If grdScanItem.Rows(iRowIndex)("FreezeSB") = True Then
                                ' Note :FreezeSB stands for sale of an item is not valid . as per rama jena
                                ShowMessage(getValueByKey("BL100"), "BL100 - " & getValueByKey("CLAE04"))
                                grdScanItem.Rows(iRowIndex)("PurchasedQty") = 0
                                Validate_PurchasedQty(Decimal.Zero)
                                Exit Sub
                            End If
                        Else
                            'MessageBox.Show("Entered PurchaseQty is not valid ", "PurchasedQty")
                            ShowMessage(getValueByKey("BL100"), "BL100 - " & getValueByKey("CLAE04"))
                            grdScanItem.Rows(iRowIndex)("PurchasedQty") = 0
                            Validate_PurchasedQty(Decimal.Zero)
                            Exit Sub
                        End If

                        Dim iOpenAmountQty As Integer = ReturnColumnValue(iRowIndex, "OpenAmountQty")
                        If Not iOpenAmountQty > 0 Then
                            If Not (IsDBNull_ColumnValue(grdScanItem.Rows(iRowIndex), "PurchasedQty")) Then
                                Dim iBookedQty As Integer = ReturnColumnValue(iRowIndex, "BookedQty")

                                Dim iOderedQty As Integer = ReturnColumnValue(iRowIndex, "RequstedQty")
                                Dim iBalanceQty As Integer = iOderedQty - (iBookedQty)
                                Dim iPurchedQty As Integer = ReturnColumnValue(iRowIndex, "PurchasedQty")
                                Dim iSellingPrice As Decimal = ReturnColumnValue(iRowIndex, "SellingPrice")
                                If (iPurchedQty > iBalanceQty) Then
                                    grdScanItem.Rows(iRowIndex)("PurchasedQty") = 0
                                    BirthListItemMergeTable.Rows(iRowIndex - 1)("PurchasedQty") = 0
                                    grdScanItem.Rows(iRowIndex)("CurrentPurchasedAmount") = Decimal.Zero
                                    Validate_PurchasedQty(Decimal.Zero)
                                    ShowMessage(getValueByKey("BL062"), "BL062 - " & getValueByKey("CLAE04"))
                                    'MessageBox.Show("You can't purchase item's more than Ordered Quantity balance.")
                                Else

                                    grdScanItem.Rows(iRowIndex)("CurrentPurchasedAmount") = FormatNumber((iPurchedQty * iSellingPrice), 2)
                                    BirthListItemMergeTable.Rows(iRowIndex - 1)("PurchasedQty") = iPurchedQty
                                    Dim NewRow As DataRow = BirthListItemMergeTable.Rows(iRowIndex - 1)
                                    Dim decTotalAmount = (NewRow.Item("SellingPrice") * NewRow.Item("PurchasedQty"))

                                    If objclsBirthListGlobal.CreateDataSetForTaxCalculation(dtMainTax, clsAdmin.SiteCode, NewRow("ArticleCode"), decTotalAmount, NewRow, NewRow("EAN").ToString()) Is Nothing Then
                                        If Not clsDefaultConfiguration.ArticleTaxAllowed Then
                                            ShowMessage(getValueByKey("CM019"), "CM019 - " & getValueByKey("CLAE05"))
                                            BirthListItemMergeTable.Rows(iRowIndex - 1)("PurchasedQty") = 0
                                            decTotalAmount = grdScanItem.Rows(iRowIndex)("CurrentPurchasedAmount") = FormatNumber((0 * iSellingPrice), 2)
                                        End If
                                    End If
                                    If NewRow.Item("EXCLUSIVETAX") Is Nothing Or NewRow.Item("EXCLUSIVETAX") Is DBNull.Value Then
                                        NewRow.Item("EXCLUSIVETAX") = 0
                                    End If
                                    NewRow.Item("CurrentPurchasedAmount") = decTotalAmount + NewRow.Item("EXCLUSIVETAX")
                                    If (Validate_PurchasedQty(decTotalAmount + NewRow.Item("EXCLUSIVETAX"))) Then
                                        BirthListItemMergeTable.Rows(iRowIndex - 1)("IsOpenAmountQtyAmountPaid") = True
                                    End If
                                End If
                                Dim decPickupQty As Decimal = ReturnColumnValue(iRowIndex, "PickUpQty")
                                Validate_PickUpQty(iRowIndex, ibookedqtyBeforeChange)
                                objclsBirthListGlobal.CalculateTotalBalanceQty(BirthListItemMergeTable, True)
                            End If
                        Else
                            grdScanItem.Rows(iRowIndex)("PurchasedQty") = 0
                            BirthListItemMergeTable.Rows(iRowIndex - 1)("PurchasedQty") = 0
                            grdScanItem.Rows(iRowIndex)("CurrentPurchasedAmount") = Decimal.Zero
                            Validate_PurchasedQty(Decimal.Zero)
                            ShowMessage(getValueByKey("BL061"), "BL061 - " & getValueByKey("CLAE04"))
                            'MessageBox.Show(" You can't   Purchase item and Convert Sold item amount into OpenAmount  for same Item")
                        End If

                    Catch ex As Exception
                        ShowMessage(getValueByKey("BL063"), "BL063 - " & getValueByKey("CLAE05"))
                        'MessageBox.Show("Wrong data for CurrentPurchasedAmount calculation ")
                    End Try
                Case "CurrentReturnQty"
                    If (ibookedqtyBeforeChange > 0) Then
                        Validate_ReturnQty(iRowIndex, ibookedqtyBeforeChange)
                    Else
                        ShowMessage(getValueByKey("BL064"), "BL064 - " & getValueByKey("CLAE04"))
                        'MessageBox.Show("There is no sold item available.so you can't  Return of this item")
                        grdScanItem.Rows(iRowIndex)("CurrentReturnQty") = 0
                    End If



                Case "ReservedQty"
                    Dim strChangedValue As String = grdScanItem.Cols("ReservedQty")(iRowIndex)
                    Dim dOldReservedQty As Decimal = decOldReservedQty
                    If Not ValidateReservedQty(strChangedValue, 0, iRowIndex) Then
                        grdScanItem.Cols("ReservedQty")(iRowIndex) = IIf(dOldReservedQty = Nothing, 0, dOldReservedQty)
                    End If

                    'Case "IsCLP"
                    '    If CheckInterTransactionAuth("CLP_Req_Change", grdScanItem.DataSource) = True Then
                    '        Dim prevValue As String
                    '        prevValue = BirthListItemMergeTable.Rows(iRowIndex - 1)("IsCLP")
                    '        'grdScanItem.StartEditing()

                    '        If prevValue.ToString().ToUpper() = "TRUE" Then
                    '            BirthListItemMergeTable.Rows(iRowIndex - 1).BeginEdit()
                    '            BirthListItemMergeTable.Rows(iRowIndex - 1)("IsCLP") = False
                    '            BirthListItemMergeTable.Rows(iRowIndex - 1).EndEdit()

                    '        Else
                    '            BirthListItemMergeTable.Rows(iRowIndex - 1).BeginEdit()
                    '            BirthListItemMergeTable.Rows(iRowIndex - 1)("IsCLP") = True
                    '            BirthListItemMergeTable.Rows(iRowIndex - 1).EndEdit()
                    '            Dim DataRowState As DataRowState = BirthListItemMergeTable.Rows(iRowIndex - 1).RowState
                    '            grdScanItem.Update()

                    '        End If
                    '    Else
                    '        grdScanItem.FinishEditing(True)
                    '        'e.Cancel = True
                    '        'dgMainGrid.Rows(e.Row)("CLPRequire") = prevValue
                    '        ShowMessage("Not Authorised!!", "Validation")
                    '    End If

                Case ""
                    If CheckInterTransactionAuth("CLP_Req_Change", grdScanItem.DataSource) = True Then



                        'If prevValue.ToString().ToUpper() = "TRUE" Then
                        '    dgMainGrid.Rows(e.Row)("CLPRequire") = False
                        'Else
                        '    dgMainGrid.Rows(e.Row)("CLPRequire") = True
                        'End If
                    Else
                        grdScanItem.FinishEditing(True)
                        ' e.Cancel = True
                        'dgMainGrid.Rows(e.Row)("CLPRequire") = prevValue
                        ShowMessage("Not Authorised!!", "Validation")

                    End If



            End Select
            CalculateTotal()
        Catch ex As Exception
            ShowMessage(getValueByKey("BL065"), "BL065 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'MessageBox.Show("Entry can't be validated.")
        End Try
    End Sub
    ''' <summary>
    ''' Validate input against Reserved Qty column.
    ''' </summary>
    ''' <param name="strChangedValue"></param>
    ''' <param name="icolumnIndex"></param>
    ''' <param name="irowIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateReservedQty(ByVal strChangedValue As String, ByVal icolumnIndex As Integer, ByVal irowIndex As Integer) As Boolean
        Try
            Dim iOrderedQty As Integer = grdScanItem.Cols("RequstedQty")(irowIndex)
            Dim iBookedQty As Integer = grdScanItem.Cols("BookedQty")(irowIndex)
            Dim AllowedQty As Integer = iOrderedQty - iBookedQty
            If (strChangedValue > AllowedQty) Then
                ShowMessage(getValueByKey("BL013"), "BL013 - " & getValueByKey("CLAE05")) ' You can't reserved items more than ordred items 
                Return False
            Else
                grdScanItem.Cols("ReservedQty")(irowIndex) = strChangedValue
                CalculateTotal()
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Validate purchased quantity 
    ''' </summary>
    ''' <param name="decTotalPice"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 

    Private Function Validate_PurchasedQty(ByVal decTotalPice As Decimal) As Boolean
        Try
            CalculateTotalOpenAmount_PurchasedQty(decTotalPice)


        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Validate "ConvToVoucher" after editing it's row value
    ''' </summary>
    '''  <param name="iRowIndex">Currenct Editing Row index</param>
    ''' <param name="ibookedqtyBeforeChange">BookedQty</param>
    ''' <remarks></remarks>

    Private Sub Validate_OpenAmountQty(ByVal iRowIndex As Integer, ByVal ibookedqtyBeforeChange As Integer)
        Try
            If Not (IsDBNull_ColumnValue(grdScanItem.Rows(iRowIndex), "PickUpQty") And (IsDBNull_ColumnValue(grdScanItem.Rows(iRowIndex), "ConvToVoucher")) And IsDBNull_ColumnValue(grdScanItem.Rows(iRowIndex), "DeliveredQty")) Then
                Dim iDeliveredQty As Integer = ReturnColumnValue(iRowIndex, "DeliveredQty")
                Dim iGVQty As Integer = ReturnColumnValue(iRowIndex, "ConvToVoucher")
                IsDBNull_ColumnValue(grdScanItem.Rows(iRowIndex), "OpenAmountQty")

                Dim iOpenAmountQty As Integer = ReturnColumnValue(iRowIndex, "OpenAmountQty")
                Dim iPickUpQty As Integer = ReturnColumnValue(iRowIndex, "PickUpQty")
                Dim iCurrentPurchase As Integer = ReturnColumnValue(iRowIndex, "PurchasedQty")
                Dim currentPurchaseTotal As Integer = BirthListItemMergeTable.Compute("sum(PurchasedQty)", "")
                Dim iQtyAvailabelToGVQty As Integer = (ibookedqtyBeforeChange + iCurrentPurchase) - (iDeliveredQty + iPickUpQty + iGVQty)
                If (iOpenAmountQty > iQtyAvailabelToGVQty) Then

                    Dim strErrorMsg As String = String.Format(getValueByKey("BL066"), iQtyAvailabelToGVQty)
                    ShowMessage(strErrorMsg, "BL066 - " & getValueByKey("CLAE05"))
                    grdScanItem.Rows(iRowIndex)("OpenAmountQty") = 0
                    BirthListItemMergeTable.Rows(iRowIndex - 1)("OpenAmountQtyAmount") = Decimal.Zero

                    Dim decOpenAmountQtyAMount As Decimal = Decimal.Zero
                    CaluculateTotalOpenAmountQTy(decOpenAmountQtyAMount)

                    If (currentPurchaseTotal > 0) Then
                        CalculateTotalOpenAmount_PurchasedQty(Decimal.Zero)
                    End If
                Else

                    Dim strArticleCode As String = BirthListItemMergeTable.Rows(iRowIndex - 1)("ArticleCode")
                    Dim strEAN As String = BirthListItemMergeTable.Rows(iRowIndex - 1)("EAN")
                    Dim iSellingPrice As Decimal = ReturnLastSoldPriceForItem(strArticleCode, strEAN)

                    If Not iSellingPrice = Decimal.Zero Then
                        Dim decOpenAmountQtyAMount As Decimal = iSellingPrice * iOpenAmountQty
                        BirthListItemMergeTable.Rows(iRowIndex - 1)("OpenAmountQtyAmount") = decOpenAmountQtyAMount
                        CaluculateTotalOpenAmountQTy(decOpenAmountQtyAMount)
                        If (currentPurchaseTotal > 0) Then
                            CalculateTotalOpenAmount_PurchasedQty(Decimal.Zero)
                        End If
                    Else
                        'ShowMessage("Sold price for selected  item  not found.", "Convert Open Amount")
                        ShowMessage(getValueByKey("BL110"), "Convert Open Amount")

                        grdScanItem.Rows(iRowIndex)("OpenAmountQty") = 0
                        BirthListItemMergeTable.Rows(iRowIndex - 1)("OpenAmountQtyAmount") = Decimal.Zero
                        Dim decOpenAmountQtyAMount As Decimal = Decimal.Zero
                        CaluculateTotalOpenAmountQTy(decOpenAmountQtyAMount)

                        If (currentPurchaseTotal > 0) Then
                            CalculateTotalOpenAmount_PurchasedQty(Decimal.Zero)
                        End If

                    End If



                End If
            Else
                ShowMessage(getValueByKey("BL066"), "BL066 - " & getValueByKey("CLAE05"))
                'MessageBox.Show("Null Values Not Valid.", "PickUpQty")
            End If
        Catch ex As Exception
            grdScanItem.Rows(iRowIndex)("OpenAmountQty") = 0
            BirthListItemMergeTable.Rows(iRowIndex - 1)("OpenAmountQtyAmount") = Decimal.Zero
            ShowMessage(getValueByKey("BL067"), "BL067 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'MessageBox.Show("Problem for validate G.V.Qty", "GVQty")
        End Try
    End Sub

    ''' <summary>
    '''  Return item last purchased price 
    ''' </summary>
    ''' <param name="strArticleCode"></param>
    ''' <param name="strEAN"></param>
    ''' <param name="strErrorMsg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 

    Private Function ReturnLastSoldPriceForItem(ByVal strArticleCode As String, ByVal strEAN As String, Optional ByRef strErrorMsg As String = "") As Decimal
        Try
            Dim decSoldPrice As Decimal
            Dim drRows As DataRow() = dtSoldItemPrice.Select("ArticleCode='" & strArticleCode & "' and EAN= '" & strEAN & "'")
            If (drRows.Length > 0) Then
                For Each dr As DataRow In drRows
                    decSoldPrice = dr("SoldPrice")
                    Return decSoldPrice
                Next
            Else
                strErrorMsg = getValueByKey("BL068")
                Return Nothing
            End If


        Catch ex As Exception

            strErrorMsg = getValueByKey("BL069")
            LogException(ex)
        End Try
    End Function

    ''' <summary>
    '''  Convert sold amount into openamount 
    ''' </summary>
    ''' <remarks></remarks>

    Private Function CaluculateTotalOpenAmountQTy(ByVal decAmount As Decimal)
        Try

            Dim caldecAmount As Decimal = BirthListItemMergeTable.Compute("sum(OpenAmountQtyAMount)", " ")

            NewOpenAmount = Decimal.Add(_decOpenAmount, caldecAmount)
            CtrlCashSummary1.lbltxt5 = CurrencyFormat(NewOpenAmount)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    ''' <summary>
    ''' Calculate total amount for purchased items into BirthList
    ''' </summary>
    ''' <param name="decAmount"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CalculateTotalOpenAmount_PurchasedQty(ByVal decAmount As Decimal) As Boolean
        Try
            Dim caldecAmount As Decimal = BirthListItemMergeTable.Compute("sum(CurrentPurchasedAmount)", " ")
            Dim OpenAmountForPurchasedItem As Decimal
            OpenAmountForPurchasedItem = Decimal.Subtract(NewOpenAmount, caldecAmount)


            If OpenAmountForPurchasedItem < Decimal.Zero Then
                CtrlCashSummary1.lbltxt5 = CurrencyFormat(Decimal.Zero)
            Else
                CtrlCashSummary1.lbltxt5 = CurrencyFormat(OpenAmountForPurchasedItem)
            End If


            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Validate "PickUpQty" after edit 
    ''' </summary>
    ''' <param name="iRowIndex">Currenct Editing Row index</param>
    ''' <param name="ibookedqtyBeforeChange">BookedQty</param>
    ''' <remarks></remarks>
    ''' 

    Private Sub Validate_PickUpQty(ByVal iRowIndex As Integer, ByVal ibookedqtyBeforeChange As Integer)
        Try
            If Not (IsDBNull_ColumnValue(grdScanItem.Rows(iRowIndex), "PickUpQty") And IsDBNull_ColumnValue(grdScanItem.Rows(iRowIndex), "DeliveredQty")) Then
                Dim iDeliveredQty As Integer = ReturnColumnValue(iRowIndex, "DeliveredQty")
                Dim iGVQty As Integer = ReturnColumnValue(iRowIndex, "ConvToVoucher")
                Dim iOpenAmountQty As Integer = ReturnColumnValue(iRowIndex, "OpenAmountQty")
                Dim iTotalQtyOldPurchase As Integer = ibookedqtyBeforeChange - (iDeliveredQty + iGVQty + iOpenAmountQty)
                Dim iCurrentPurchaseItems As Integer = ReturnColumnValue(iRowIndex, "PurchasedQty")
                Dim iQtyAvailabelToPickUp As Integer = iTotalQtyOldPurchase + iCurrentPurchaseItems

                Dim iPickUpQty As Integer = ReturnColumnValue(iRowIndex, "PickUpQty")
                If (iPickUpQty > iQtyAvailabelToPickUp) Then
                    Dim strErrorMsg As String = String.Format(getValueByKey("BL039"), iQtyAvailabelToPickUp)
                    ShowMessage(strErrorMsg, "BL039 - " & getValueByKey("CLAE04"))
                    grdScanItem.Rows(iRowIndex)("PickUpQty") = 0
                End If
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("BL070"), "BL070 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'MessageBox.Show("Problem for validate convert OpenAmount Qty", "ConvToVoucher")
        End Try
    End Sub

    ''' <summary>
    ''' Validation for input provied against Return Quantity columns
    ''' </summary>
    ''' <param name="iRowIndex"></param>
    ''' <param name="ibookedqtyBeforeChange"></param>
    ''' <remarks></remarks>

    Private Sub Validate_ReturnQty(ByVal iRowIndex As Integer, ByVal ibookedqtyBeforeChange As Integer)
        Try
            If Not (IsDBNull_ColumnValue(grdScanItem.Rows(iRowIndex), "PickUpQty") And IsDBNull_ColumnValue(grdScanItem.Rows(iRowIndex), "DeliveredQty")) Then
                Dim iDeliveredQty As Integer = ReturnColumnValue(iRowIndex, "DeliveredQty")
                Dim iReturnQty As Integer = ReturnColumnValue(iRowIndex, "CurrentReturnQty")

                If (iDeliveredQty < iReturnQty) Then

                    'Dim strErrorMsg As String = String.Format("You can't enter ReturnQty Quantity more than {0}", iDeliveredQty)
                    Dim strErrorMsg As String = String.Format(getValueByKey("BL071"), iDeliveredQty)
                    ShowMessage(strErrorMsg, "BL071 - " & getValueByKey("CLAE04"))
                    grdScanItem.Rows(iRowIndex)("CurrentReturnQty") = 0
                Else
                    ''grdScanItem.Rows(iRowIndex)("DeliveredQty") = iDeliveredQty - iReturnQty
                End If
            Else
                grdScanItem.Rows(iRowIndex)("CurrentReturnQty") = 0
            End If
        Catch ex As Exception
            grdScanItem.Rows(iRowIndex)("CurrentReturnQty") = 0
            LogException(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Check column value .
    ''' </summary>
    ''' <param name="c1GridRow"></param>
    ''' <param name="strColumnName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsDBNull_ColumnValue(ByVal c1GridRow As C1.Win.C1FlexGrid.Row, ByVal strColumnName As String) As Boolean
        If (c1GridRow(strColumnName) Is DBNull.Value) Then
            c1GridRow(strColumnName) = 0
            Return True
        Else
            Return False
        End If
    End Function

    Private Function ReturnColumnValue(ByVal iRowIndex As Integer, ByVal strColumnValue As String) As Double
        Try

            Dim objValue As Object = grdScanItem.Rows(iRowIndex)(strColumnValue)
            If Not objValue Is Nothing Then
                If Not objValue Is DBNull.Value And (VarType(objValue) = VariantType.Integer Or VarType(objValue) = VariantType.Decimal Or VarType(objValue) = VariantType.Double) Then
                    Return CDbl(objValue)
                Else
                    Return 0
                End If
            Else
                Return 0
            End If

        Catch ex As Exception
            LogException(ex)
            Return 0
        End Try

    End Function

    ''' <summary>
    ''' Click delete button on grid 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub grdscanitem_CellButtonClick_1(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdScanItem.CellButtonClick
        Try
            Dim objclsComman As New clsCommon
            Dim grid As C1.Win.C1FlexGrid.C1FlexGrid = sender
            Dim iRowIndex As Integer = e.Row
            Dim strErroMsg As String = ""
            If Not (grid.Rows(e.Row)("DeliveredQty") = 0) Or Not (grid.Rows(e.Row)("BookedQty") = 0) Then
                ShowMessage(getValueByKey("BL040"), "BL040 - " & getValueByKey("CLAE04"))

            Else
                If (DeleteItem_AmountAddToOpenAMount(grid, iRowIndex, strErroMsg)) Then
                    Dim drdeltem As DataRow = BirthListItemMergeTable.Rows(iRowIndex - 1)

                    If Not (drdeltem.RowState = DataRowState.Deleted Or drdeltem.RowState = DataRowState.Added) Then
                        Dim drItemRowInPosDb As POSDBDataSet.BirthListRequestedItemsRow = POSDBDataSet.BirthListRequestedItems.FindBySiteCodeBirthListIdEANSrNo(clsAdmin.SiteCode, drdeltem("BirthListId"), drdeltem("EAN"), drdeltem("SrNo"))

                        If Not drItemRowInPosDb Is Nothing Then
                            drItemRowInPosDb.Delete()
                        End If
                        DeleteTaxEntries(BirthListItemMergeTable.Rows((iRowIndex - 1))("EAN"))
                        BirthListItemMergeTable.Rows.RemoveAt(iRowIndex - 1)
                    ElseIf (drdeltem.RowState = DataRowState.Added) Then
                        DeleteTaxEntries(BirthListItemMergeTable.Rows((iRowIndex - 1))("EAN"))
                        'Dim IsDelete As Boolean = BirthListItemMergeTable.Rows(iRowIndex - 1)("IsAmountPaidFromOpenAmountColumn")
                        'If Not IsDelete Then
                        BirthListItemMergeTable.Rows.RemoveAt(iRowIndex - 1)
                        'Else
                        '    ReverseCalculationForOpenAmount(iRowIndex - 1)
                        '    BirthListItemMergeTable.Rows.RemoveAt(iRowIndex - 1)
                        'End If

                    End If
                    If grdScanItem.Rows.Count > 1 Then
                        Dim strArticle As String = grdScanItem.Rows(grdScanItem.RowSel)("ArticleCode").ToString()
                        CtrlProductImage1.ShowArticleImage(strArticle)
                    End If
                    CalculateTotal()
                Else
                    ShowMessage(getValueByKey("BL040"), "BL040 - " & getValueByKey("CLAE04"))
                    'MessageBox.Show("You can't delete selected item.")

                End If

            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("BL041"), "BL041 - " & getValueByKey("CLAE04"))
            LogException(ex)
            'MessageBox.Show("Problem for selected image display")
        End Try
    End Sub

    Private Function DeleteTaxEntries(ByVal strEAN As String) As Boolean
        Try
            If Not dtMainTax Is Nothing Then
                For Each dr As DataRow In dtMainTax.Select("EAN='" + strEAN + "'", "")
                    dr.Delete()
                    dr.AcceptChanges()
                Next
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    ''' <summary>
    ''' Adding Deleted item amount into open Amount .ONly If selected items is sold.
    ''' </summary>
    ''' <param name="senderGrid"></param>
    ''' <param name="iRowGridIndex"></param>
    ''' <param name="strErrorMsg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteItem_AmountAddToOpenAMount(ByVal senderGrid As C1.Win.C1FlexGrid.C1FlexGrid, ByVal iRowGridIndex As Integer, Optional ByRef strErrorMsg As String = "") As Boolean
        Try
            Dim iOpenAmountQty As Integer = senderGrid.Rows(iRowGridIndex)("ConvToVoucher")
            Dim iBookedQty As Integer = senderGrid.Rows(iRowGridIndex)("BookedQty")
            If iBookedQty > 0 Then
                Dim iTotalOpenAmountQty As Integer = iBookedQty - iOpenAmountQty
                Dim strArticleCode As String = senderGrid.Rows(iRowGridIndex)("ArticleCode")
                Dim strEAN As String = senderGrid.Rows(iRowGridIndex)("EAN")

                Dim decSoldPrice As Decimal
                Dim drRows As DataRow() = dtSoldItemPrice.Select("ArticleCode='" & strArticleCode & "' and EAN= '" & strEAN & "'")
                If (drRows.Length > 0) Then
                    For Each dr As DataRow In drRows
                        decSoldPrice = dr("SoldPrice")
                        Exit For
                    Next
                    If (decSoldPrice > Decimal.Zero) Then
                        Dim decTotalAmount As Decimal = decSoldPrice * iTotalOpenAmountQty
                        CalculateTotalOpenAmount(decTotalAmount)
                        Return True
                    Else
                        strErrorMsg = "Sold amount is not valid "
                        Return False
                    End If

                Else
                    strErrorMsg = getValueByKey("BL068")
                    Return False
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    ''' <summary>
    ''' Reverse the added sold item price from openamount.
    ''' </summary>
    ''' <param name="irowIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ReverseCalculationForOpenAmount(ByVal irowIndex As Integer) As Boolean
        Try
            Dim iRequestedQty As Integer = BirthListItemMergeTable.Rows(irowIndex)("RequstedQty")
            Dim isellingPrice As Decimal = BirthListItemMergeTable.Rows(irowIndex)("SellingPrice")
            Dim TotalAmount As Decimal = iRequestedQty * isellingPrice
            CheckArticleAmountAgainst_OpenAmount(-TotalAmount)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    ''' <summary>
    '''  Calculate OpenAmount after sold items deleted from grid
    ''' </summary>
    ''' <param name="decAmount"></param>
    ''' <remarks></remarks>
    ''' 

    Private Sub CalculateTotalOpenAmount(ByVal decAmount As Decimal)
        OpenAmount = Decimal.Add(OpenAmount, decAmount)
    End Sub

    ''' <summary>
    ''' Grid row column change event for display image
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    Private Shared isFirst As Boolean = False

    Private Sub grdScanItem_RowColChange(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdScanItem.RowColChange
        Try
            Dim objclsComman As New clsCommon
            Dim grid As C1.Win.C1FlexGrid.C1FlexGrid = sender
            Dim strArticleCode As String
            If (isFirst) Then
                If (grid.Row >= 0) Then
                    Dim iTrt As Integer = grid.Row
                    strArticleCode = BirthListItemMergeTable.Rows(iTrt - 1)("ArticleCode")
                    'Dim strUrl As String = objclsComman.GetArticleImage(strArticleCode, My.Settings.ArticleImageFolder)
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
    ''' Apply Discount and close
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click, btnApplyDiscountAndClose1.Click
        Try

            If SaveAndPrint(False) Then

                If BirthListID <> String.Empty Then
                    Dim dateCurrent As Date = objclscomman.GetCurrentDate()
                    Dim sqlTran As SqlTransaction = Nothing
                    'ValidateEditColumnEntry(grdScanItem.Cols(grdScanItem.Col).Name, grdScanItem.Row)
                    Try
                        Dim decTotalAmount As Decimal
                        Try
                            Dim strDecTotalAmount As String = dtBirthListPaymentHistory.Compute("sum(PaidAmt)", " ").ToString
                            decTotalAmount = CDbl(IIf(strDecTotalAmount <> String.Empty, strDecTotalAmount, 0))
                            '' start here-- Added by rama for OpenAmount to be deducted from TotalPaymentReceived -date 07-Oct-2010
                            If OpenAmountForBL <> 0 Then
                                decTotalAmount = decTotalAmount - CDbl(OpenAmountForBL)
                            End If
                            ''End
                        Catch ex As Exception

                        End Try
                        Dim genVoucherNO As String = ""
                        Dim decTotalDiscountAmount As Decimal = Decimal.Zero
                        Dim decOpenAmount As Decimal = OpenAmount
                        'decTotalAmount = Decimal.Add(decTotalAmount, decOpenAmount)
                        'CheckInterTransactionAuth(TransactionTypes.BirthListUpdate.ToString(), Nothing, decTotalAmount, decOpenAmount, decTotalDiscountAmount)
                        Dim IsFormcacel As Boolean = False
                        Dim strIssueType As String = String.Empty
                        Dim decDiscountPercentage As Decimal = 0


                        CheckInterTransactionAuth("BL_CLOSE", Nothing, decTotalAmount, decOpenAmount, decTotalDiscountAmount, "", IsFormcacel, strIssueType, "", decDiscountPercentage)

                        If Not IsFormcacel Then
                            strIssueType = "CreditVoucher"
                            Select Case (strIssueType)
                                Case "CreditVoucher"
                                    If Not decTotalDiscountAmount <= Decimal.Zero Then
                                        'Dim dateCurrent1 As Date = Date.Today

                                        If Not (SaveCreditVoucher(dateCurrent, decTotalDiscountAmount, sqlTran, genVoucherNO)) Then
                                            ShowMessage(getValueByKey("BL082"), "BL082 - " & getValueByKey("CLAE04"))
                                            Exit Sub
                                        End If
                                    End If
                                Case "GiftVoucher"
                                    Dim dateCurrent1 As Date = Date.Today
                                    If Not (CloseBirthListGiftVoucher(decTotalDiscountAmount, sqlTran, dateCurrent)) Then
                                        Exit Sub
                                    End If
                            End Select
                            Dim decCLPPoints As Decimal
                            Dim decCLPDiscount As Decimal


                            If (OnItemLevel_Discount(decDiscountPercentage)) Then

                            End If
                            'If IsCLPCustomer = True And clsDefaultConfiguration.BLIsCLPApplicable = True Then

                            '    Dim relPointsdataset As New DataSet
                            '    relPointsdataset = getrelativesCLPData(BirthListID, sqlTran)


                            '    If relPointsdataset.Tables(0).Rows.Count > 0 Then
                            '        Dim dt As New DataTable
                            '        dt = POSDBDataSet.BirthListPaymentHistory
                            '        dt.Clear()
                            '        CalCulateCLPSlabwise(dtCustomerINformation(0)("CardType"), relPointsdataset.Tables(0), "", dtCustomerINformation(0)("CUSTOMERNO"))


                            '        updaterelativedata(sqlTran, relPointsdataset.Tables(0))

                            '    End If


                            '    If Not sqlTran Is Nothing Then
                            '        sqlTran.Commit()
                            '    End If
                            '    If (CLPCaluclation(decCLPPoints, decCLPDiscount, dateCurrent)) Then
                            '        'CLPCaluclation() 
                            '        If IsDeliveryPending() Then
                            '            If Not (BirthListClose("Close-DeliveryPending", decTotalDiscountAmount, decCLPDiscount, decCLPPoints)) Then
                            '                ShowMessage(getValueByKey("BL083"), "BL083 - " & getValueByKey("CLAE04"))
                            '                Exit Sub
                            '            End If
                            '        Else
                            '            If Not (BirthListClose("Closed", decTotalDiscountAmount, decCLPDiscount, decCLPPoints)) Then
                            '                ShowMessage(getValueByKey("BL083"), "BL083 - " & getValueByKey("CLAE04"))
                            '            End If
                            '        End If
                            '        SearchBirthListOnTextChange()
                            '    Else
                            '        If Not sqlTran Is Nothing Then
                            '            sqlTran.Commit()
                            '        End If
                            '        If IsDeliveryPending() Then
                            '            If Not (BirthListClose("Close-DeliveryPending", decTotalDiscountAmount, decCLPDiscount, decCLPPoints)) Then
                            '                ShowMessage(getValueByKey("BL083"), "BL083 - " & getValueByKey("CLAE04"))
                            '                Exit Sub
                            '            End If
                            '        Else
                            '            If Not (BirthListClose("Closed", decTotalDiscountAmount, decCLPDiscount, decCLPPoints)) Then
                            '                ShowMessage(getValueByKey("BL083"), "BL083 - " & getValueByKey("CLAE04"))
                            '                Exit Sub
                            '            End If
                            '        End If
                            '        SearchBirthListOnTextChange()
                            '    End If
                            'Else

                            If Not sqlTran Is Nothing Then
                                sqlTran.Commit()
                            End If
                            If IsDeliveryPending() Then
                                If Not (BirthListClose("Close-DeliveryPending", decTotalDiscountAmount, decCLPDiscount, decCLPPoints)) Then
                                    ShowMessage(getValueByKey("BL083"), "BL083 - " & getValueByKey("CLAE04"))
                                    Exit Sub
                                End If
                            Else
                                If Not (BirthListClose("Closed", decTotalDiscountAmount, decCLPDiscount, decCLPPoints)) Then
                                    ShowMessage(getValueByKey("BL083"), "BL083 - " & getValueByKey("CLAE04"))
                                    Exit Sub
                                End If
                            End If
                            'SearchBirthListOnTextChange()
                            '''''
                            'End If
                            If decTotalDiscountAmount > Decimal.Zero Then
                                Select Case (strIssueType)
                                    Case "CreditVoucher"
                                        PrintCreditVoucher(decTotalDiscountAmount, genVoucherNO)
                                        isFormClosed = True

                                        'Rohit Today
                                        'Dim printingDll As New SpectrumPrint.clsBirthList(SpectrumPrint.clsBirthList.PrintBLTransactionSet.BirthListStatus, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserCode, BirthListID, dtCustomerINformation, BirthListItemMergeTable, "", "", _dtVoucherInfo, POSDBDataSet.BirthList.Rows(0), dtPaymentReceipt, False, EventDate, EventName, "", decTotalDiscountAmount, blPrinterInfo:=dtPrinterInfo)

                                        If PrintBirthListStatus(decTotalDiscountAmount, False) Then
                                            Dim strMsg As String = String.Format(getValueByKey("BL084"), "" & genVoucherNO, clsAdmin.CurrencySymbol, CurrencyFormat(decTotalDiscountAmount))
                                            ShowMessage(strMsg, "BL084 - " & getValueByKey("CLAE04"))

                                            ClearBirthListInformation()
                                        End If

                                    Case "GiftVoucher"
                                        PrintGiftVouchers()
                                End Select
                            Else
                                ClearBirthListInformation()

                            End If
                            'MessageBox.Show(strMsg)
                        End If

                    Catch ex As Exception
                        If Not sqlTran Is Nothing Then
                            sqlTran.Rollback()
                        End If
                        ShowMessage(getValueByKey("BL105"), "BL105 - " & getValueByKey("CLAE05"))
                    Finally

                    End Try
                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                End If
            End If
            txtBirthListid.Enabled = True
            CtrlProductImage1.BackgroundImage = Nothing
        Catch ex As Exception
            txtBirthListid.Enabled = True
            CtrlProductImage1.BackgroundImage = Nothing
            LogException(ex)
            ShowMessage(getValueByKey("BL105"), "BL105 - " & getValueByKey("CLAE05"))
        End Try


    End Sub

    Dim dtGVdetails As DataTable

    Private Function IsDeliveryPending() As Boolean
        Try
            Dim decPickUpQty As Decimal = BirthListItemMergeTable.Compute("sum(DeliveredQty)", "")
            Dim decPurchasedQty As Decimal = BirthListItemMergeTable.Compute("sum(BookedQty)", "")
            If decPickUpQty = decPurchasedQty Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Function CloseBirthListGiftVoucher(ByVal decTotalDenomination As Decimal, ByRef sqlTran As SqlTransaction, ByVal currentDate As Date) As Boolean
        Try
            Dim objfrmAdvanceSale As New frmNAdvanceSale("BirthListUpdate")
            objfrmAdvanceSale.TotalDenomination = decTotalDenomination
            objfrmAdvanceSale.TransactionID = TransactionTypes.BirthListUpdate
            objfrmAdvanceSale.ShowDialog()
            dtGVdetails = objfrmAdvanceSale.GVDetail
            'Dim objclsPrinting As New clsPrinting (
            Dim objclsBirthListGLobal As New clsBirthListGobal
            If objfrmAdvanceSale.IsFormCanceled = True Then
                Return False
            Else
                dtGVdetails = objfrmAdvanceSale.GVDetail
                If Not dtGVdetails Is Nothing Then
                    For Each dr As DataRow In dtGVdetails.Rows
                        dr("ISSUEDATSITE") = clsAdmin.SiteCode
                        dr("ISSUEDONDATE") = Now.Date
                        dr("ISSUEDINDOCTYPE") = "BLs"
                        If Not dr("ExpiryInDays") Is DBNull.Value Then
                            dr("ExpiryDate") = objclsBirthListGLobal.CalculateExpiryDate(currentDate, dr("ExpiryInDays"))
                        End If
                    Next
                End If
                If Not dtGVdetails Is Nothing Then
                    If (objclsBirthListGLobal.GenerateGiftVoucher(dtGVdetails, sqlTran, BirthListIDSearch, clsAdmin.SiteCode, clsAdmin.UserName, currentDate)) Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return True
                End If
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private IsCLPCalculate As Boolean
    Private Function CLPCaluclation(Optional ByRef decCLPPoints As Decimal = Decimal.Zero, Optional ByRef decCLPDiscount As Decimal = Decimal.Zero, Optional ByVal dtCurrent As DateTime = Nothing) As Boolean
        Try
            Dim posBirthListCLPtransaction As New POSDBDataSetTableAdapters.BirthListCLPtransactionTableAdapter
            Dim posDTBirthListCLPtransaction As POSDBDataSet.BirthListCLPtransactionDataTable
            posDTBirthListCLPtransaction = posBirthListCLPtransaction.GetDatabyBirthListID(BirthListIDSearch)

            Dim posBirthListCLPInformation As New POSDBDataSetTableAdapters.CLPCustomersTableAdapter
            Dim posDTBirthListCLPInformation As New POSDBDataSet.CLPCustomersDataTable

            posDTBirthListCLPInformation = posBirthListCLPInformation.GetDataByCustId(clsAdmin.SiteCode, POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.CustomerIdColumn))


            Dim dtCLP As DataTable = posDTBirthListCLPtransaction
            dtCLP.Columns("NetAmt").ColumnName = "NetAmount"
            CalCulateCLP(posDTBirthListCLPInformation.Rows(0)(posDTBirthListCLPInformation.CardTypeColumn).ToString(), dtCLP, " ")
            dtCLP.Columns("NetAmount").ColumnName = "NetAmt"

            If Not dtCLP Is Nothing Then
                If (dtCLP.Rows.Count > 0) Then
                    decCLPPoints = dtCLP.Compute("sum(CLPPoints)", " ")
                    decCLPDiscount = dtCLP.Compute("sum(CLPDiscount)", " ")
                End If

            End If
            Dim objClsBirthListGlobal As New clsBirthListGobal
            Dim IsCLPTransaction As Boolean = True
            If (OnItemLevel_CLPUpdate(dtCLP)) Then
                objClsBirthListGlobal.BirthListCLPCalculation(clsAdmin.TerminalID, OnlineConnect, dtCLP, posDTBirthListCLPInformation.Rows(0)(posDTBirthListCLPInformation.SiteCodeColumn).ToString(), posDTBirthListCLPInformation.Rows(0)(posDTBirthListCLPInformation.ClpProgramIdColumn).ToString(), posDTBirthListCLPInformation.Rows(0)(posDTBirthListCLPInformation.CardNoColumn).ToString(), IsCLPTransaction, POSDBDataSet.BirthListRequestedItems, BirthListRequestedItemsTableAdapter1, dtCurrent, clsAdmin.Financialyear)
            Else
                objClsBirthListGlobal.BirthListCLPCalculation(clsAdmin.TerminalID, OnlineConnect, dtCLP, posDTBirthListCLPInformation.Rows(0)(posDTBirthListCLPInformation.SiteCodeColumn).ToString(), posDTBirthListCLPInformation.Rows(0)(posDTBirthListCLPInformation.ClpProgramIdColumn).ToString(), posDTBirthListCLPInformation.Rows(0)(posDTBirthListCLPInformation.CardNoColumn).ToString(), IsCLPTransaction, Nothing, Nothing, dtCurrent, clsAdmin.Financialyear)
            End If

            If IsCLPTransaction Then
                'MessageBox.Show("CLP transaction success")
                Return True
            Else
                'MessageBox.Show("CLP transaction Failed")
                Return False
            End If


        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Function OnItemLevel_CLPUpdate(ByVal dtCLPItem As DataTable) As Boolean
        Try
            If Not dtCLPItem Is Nothing Then
                Dim strArticleCode As String
                Dim strEAN As String
                Dim strBirthListId As String
                Dim strSiteCode As String
                Dim drCollectionArticle As POSDBDataSet.BirthListRequestedItemsRow
                For Each dr As DataRow In dtCLPItem.Rows
                    strArticleCode = dr("ArticleCode")
                    strEAN = dr("EAN")
                    strBirthListId = dr("BirthListId")
                    strSiteCode = dr("SiteCode")
                    drCollectionArticle = POSDBDataSet.BirthListRequestedItems.FindBySiteCodeBirthListIdEANSrNo(strSiteCode, strBirthListId, strEAN, dr("SrNo"))
                    If Not drCollectionArticle Is Nothing Then
                        drCollectionArticle.BeginEdit()
                        drCollectionArticle("CLPPoints") = dr("CLPPoints")
                        drCollectionArticle("CLPDiscount") = dr("CLPDiscount")
                        drCollectionArticle.EndEdit()
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
    Private Function OnItemLevel_Discount(ByVal decDiscountPercentage As Decimal) As Boolean
        Try
            Dim dtBirthListDiscountOnItemLevel As POSDBDataSet.BirthListDiscountOnItemLevelDataTable
            Dim adpBirthListDiscount As New POSDBDataSetTableAdapters.BirthListDiscountOnItemLevelTableAdapter
            dtBirthListDiscountOnItemLevel = adpBirthListDiscount.GetBySiteCodeBlIdCustomerID(clsAdmin.SiteCode, clsAdmin.Financialyear, BirthListIDSearch)
            If Not dtBirthListDiscountOnItemLevel Is Nothing AndAlso dtBirthListDiscountOnItemLevel.Rows.Count > 0 Then

                Dim strArticleCode As String
                Dim strEAN As String
                Dim strBirthListId As String
                Dim strSiteCode As String
                Dim decItemNetAmount As Decimal = 0
                Dim decDiscountOnItem As Decimal = 0
                Dim drCollectionArticle As POSDBDataSet.BirthListRequestedItemsRow
                For Each dr As POSDBDataSet.BirthListDiscountOnItemLevelRow In dtBirthListDiscountOnItemLevel.Rows
                    strEAN = dr("EAN")
                    strArticleCode = dr("ArticleCode")
                    strBirthListId = dr("BirthListId")
                    strSiteCode = dr("SiteCode")
                    decItemNetAmount = dr("netamt")
                    decItemNetAmount = (decItemNetAmount * decDiscountPercentage) / 100
                    'drCollectionArticle = POSDBDataSet.BirthListRequestedItems.Select("Ean='" + strEAN + "' and ArticleCode='" + strArticleCode + "'")
                    'If Not drCollectionArticle Is Nothing AndAlso drCollectionArticle.Length > 0 Then
                    '    drCollectionArticle(0).BeginEdit()
                    '    drCollectionArticle(0)("BLDiscountAmt") = decItemNetAmount
                    '    drCollectionArticle(0).EndEdit()
                    'End If

                    'For Each drCollectionArticle In POSDBDataSet.BirthListRequestedItems.Select("Ean='" + strEAN + "' and ArticleCode='" + strArticleCode + "'")
                    '    drCollectionArticle.BeginEdit()
                    '    drCollectionArticle("BLDiscountAmt") = decItemNetAmount
                    '    drCollectionArticle.EndEdit()
                    'Next

                    For Each drCollectionArticle In POSDBDataSet.BirthListRequestedItems.Rows
                        If (drCollectionArticle("ArticleCode") = strArticleCode) AndAlso drCollectionArticle("EAN") = strEAN Then
                            drCollectionArticle.BeginEdit()
                            drCollectionArticle(POSDBDataSet.BirthListRequestedItems.BLDiscountAmtColumn) = decItemNetAmount
                            drCollectionArticle.EndEdit()
                        End If
                    Next

                Next
            End If

            'Dim adpBirthListRequested As New POSDBDataSetTableAdapters.BirthListRequestedItemsTableAdapter

            'For Each dr As POSDBDataSet.BirthListRequestedItemsRow In POSDBDataSet.BirthListRequestedItems.Rows
            '    Console.WriteLine(dr.RowState.ToString())
            'Next


            'Console.WriteLine("")

        Catch ex As Exception

        End Try
    End Function

    Private Sub BtnItemSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Not (BirthListID = String.Empty) Then
                Dim objfrmItemSearch1 As New frmNItemSearch
                objfrmItemSearch1.ShowDialog()
                If Not objfrmItemSearch1.SearchResult Is Nothing Then
                    NewItemAdd = objfrmItemSearch1.ItemRow
                    Dim objclsBirthListGlobal As New clsBirthListGobal
                    If Not NewItemAdd Is Nothing Then
                        If NewItemAdd("FreezeSB") = True Then
                            ShowMessage(getValueByKey("BL100"), "BL100 - " & getValueByKey("CLAE04"))
                            CtrlSalesPerson1.CtrlTxtBox.Focus()
                            Exit Sub
                        End If
                        If (objclsBirthListGlobal.IsArticleRateAvailabel(NewItemAdd, "SellingPrice", "")) Then

                            'Rakesh:06.11.2013-->7895 : Avoid stock check validation when order place from SO & BL
                            'If objclsBirthListGlobal.IsStockAvailable(clsDefaultConfiguration.NegativeInventoryAllowed, NewItemAdd, "AvailableQty") Then
                            Grid_AddNewItem(NewItemAdd, False)
                            CalculateTotal()
                            'Else
                            '    ShowMessage(getValueByKey("BL046"), "BL046 - " & getValueByKey("CLAE04"))
                            '    'MessageBox.Show("Item is not availabel in stock")
                            'End If
                        Else
                            ShowMessage(getValueByKey("BL081"), "BL081 - " & getValueByKey("CLAE04"))
                            'Rate Not Found 
                        End If
                    End If

                End If
            Else
                ShowMessage(getValueByKey("BL034"), "BL034")
                'MessageBox.Show("Select BirthList and then try to add items.")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Check whether OpenAmount is availabel or not 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 

    Private Function IsOpenAmountAvailabel() As Boolean
        Try
            If (_decOpenAmount > Decimal.Zero) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function

    Private Function CheckArticleAmountAgainst_OpenAmount(ByVal decArticleAmount As Decimal) As Boolean

        Try
            'Dim decPaidAmount As Decimal

            'Dim decTemp As Decimal
            'If (decArticleAmount > _decOpenAmount) Then
            '    decTemp = Decimal.Subtract(decArticleAmount, _decOpenAmount)
            '    OpenAmount = Decimal.Zero
            '    Return True
            'ElseIf (decArticleAmount < _decOpenAmount) Then
            '    decTemp = Decimal.Subtract(_decOpenAmount, decArticleAmount)

            '    OpenAmount = decTemp
            '    Return True
            'End If


            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Function InsertPaymentTransaction_Manually(ByVal decAmount As Decimal, ByVal strReceipttype As String, ByVal strReceiptTypeCode As String) As Boolean
        Try
            Dim objClsAcceptPayment As New clsAcceptPayment
            If dtPaymentReceipt Is Nothing Then
                Dim ds As DataSet = objClsAcceptPayment.GetDataset()
                dtPaymentReceipt = ds.Tables("MSTRecieptType")
            End If
            Dim decPayedAmount As Decimal = IIf(dtPaymentReceipt.Compute("sum(Amount)", "RecieptType = 'OpenAmount'") Is DBNull.Value, 0, dtPaymentReceipt.Compute("sum(Amount)", "RecieptType = 'OpenAmount'"))
            'Dim rowCount As Integer
            Try
                If decPayedAmount <> 0 Then
                    For Each drRow As DataRow In dtPaymentReceipt.Rows
                        If drRow("RecieptType") = "OpenAmount" Then
                            drRow.Delete()
                        End If
                        dtPaymentReceipt.AcceptChanges()
                    Next
                End If
            Catch ex As Exception

            End Try

            Dim currentDate As Date = objclscomman.GetCurrentDate()
            'rowCount = dtPaymentReceipt.Rows.Count
            'Dim srno As Integer = dtPaymentReceipt.Compute("max(SRNO)", "")

            'dtPaymentReceipt.Rows(rowCount)("SRNO") = srno
            Dim drNewOpenAmountPayment As DataRow = dtPaymentReceipt.NewRow()
            drNewOpenAmountPayment("RecieptType") = "OpenAmount"
            drNewOpenAmountPayment("RecieptTypeCode") = "OpenAmount"
            drNewOpenAmountPayment("Amount") = decAmount
            drNewOpenAmountPayment("Date") = currentDate

            Dim decExchangeRate As Decimal
            Dim decTotalAmountInCurrency As Decimal
            decTotalAmountInCurrency = objClsAcceptPayment.CalculateTotalBillAmount_InCurrency(decAmount, clsAdmin.CurrencyCode, decExchangeRate, clsAdmin.CurrencyCode)
            drNewOpenAmountPayment("ExchangeRate") = decExchangeRate
            drNewOpenAmountPayment("CurrencyCode") = clsAdmin.CurrencyCode
            drNewOpenAmountPayment("AmountInCurrency") = decTotalAmountInCurrency
            dtPaymentReceipt.Rows.Add(drNewOpenAmountPayment)

            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    '''  New Article adding to grid
    ''' </summary>
    ''' <param name="NewItemAdd"></param>
    ''' <remarks></remarks>
    ''' 

    Private Sub Grid_AddNewItem(ByVal NewItemAdd As DataRow, Optional ByVal isAmountPaidFromOpenAmount As Boolean = False)
        Try
            Dim obj As New clsBirthListSalesSave
            Dim dtNewItemAdd As New DataTable
            dtNewItemAdd = BirthListItemMergeTable.Clone()
            If Not (IsItemAdded(NewItemAdd.Item("EAN"), NewItemAdd.Item("SellingPrice"))) Then
                Dim NewRow As DataRow = dtNewItemAdd.NewRow()
                NewRow.Item("BirthListID") = txtBirthListid.Text
                'NewRow.Item("FinYear") = clsAdmin.Financialyear
                NewRow.Item("FinYear") = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.FinYearColumn)
                NewRow.Item("SiteCode") = clsAdmin.SiteCode
                NewRow.Item("ArticleCode") = NewItemAdd.Item("ArticleCode")
                NewRow.Item("EAN") = NewItemAdd.Item("EAN")
                NewRow.Item("DISCRIPTION") = NewItemAdd.Item("DISCRIPTION")
                NewRow.Item("SellingPRICE") = NewItemAdd.Item("SellingPRICE")
                NewRow.Item("RequstedQty") = 1
                NewRow.Item("BookedQty") = 0
                NewRow.Item("DeliveredQty") = 0
                NewRow.Item("ProductGroup") = NewItemAdd.Item("NODENAME")
                NewRow.Item("UNITOFMEASURE") = NewItemAdd.Item("UOM")
                NewRow.Item("NetAmount") = NewItemAdd.Item("SellingPRICE") * NewRow.Item("RequstedQty")
                NewRow.Item("Status") = True
                NewRow.Item("CreatedBy") = clsAdmin.UserName
                NewRow.Item("CreatedON") = objclscomman.GetCurrentDate()
                NewRow.Item("CreatedAT") = clsAdmin.SiteCode
                NewRow.Item("UpdatedAt") = clsAdmin.SiteCode
                NewRow.Item("UpdatedBy") = NewItemAdd.Item("UOM")
                NewRow.Item("Updatedon") = objclscomman.GetCurrentDate()
                NewRow.Item("FreezeSB") = NewItemAdd.Item("FreezeSB")
                NewRow.Item("FreezeOB") = NewItemAdd.Item("FreezeOB")
                NewRow.Item("BalanceItemQty") = NewRow.Item("RequstedQty") - NewRow.Item("BookedQty")
                NewRow.Item("IsAmountPaidFromOpenAmount") = isAmountPaidFromOpenAmount
                'NewRow.Item("IsCLP") = NewItemAdd.Item("CLPRequire")
                'NewRow.Item("SrNo") = obj.GetSrNo(txtBirthListid.Text, NewItemAdd.Item("ArticleCode"), NewItemAdd.Item("Ean")) + 1
                Try
                    NewRow.Item("SrNo") = BirthListItemMergeTable.Select("articlecode = '" & NewItemAdd("ArticleCode") & "' and EAN = '" & NewItemAdd("EAN") & "'", "SrNo Desc", DataViewRowState.CurrentRows)(0)("SrNo") + 1
                Catch ex As Exception
                    NewRow.Item("SrNo") = obj.GetSrNo(txtBirthListid.Text, NewItemAdd.Item("ArticleCode"), NewItemAdd.Item("Ean")) + 1
                End Try

                NewRow.Item("AvailableQty") = objclscomman.GetStocks(clsAdmin.SiteCode, NewItemAdd.Item("EAN"), NewItemAdd.Item("ArticleCode"), True)
                NewRow.Item("IsPriceChangedHere") = False
                NewRow.Item("IsPriceChanged") = False
                NewRow.Item("ConvToVoucher") = 0
                NewRow.Item("ReservedQty") = 0
                NewRow.Item("PurchasedQty") = 0
                NewRow.Item("PickUpQty") = 0

                'If Not cboEvent.SelectedValue = -1 Then
                '    NewRow.Item("EventID") = cboEvent.SelectedValue
                'End If
                'NewRow.Item("CurrentPurchasedAmount") = NewRow.Item("RequstedQty") * NewRow.Item("SellingPRICE")
                'NewRow.Item("AvailableQty") = 20
                dtNewItemAdd.Rows.Add(NewRow)

                dtNewItemAdd.Merge(BirthListItemMergeTable)

                BirthListItemMergeTable = dtNewItemAdd

            End If
            Dim strErrorMsg As String = ""
            BirthListItemMergeTable = objclsBirthListGlobal.CalculateTotalBalanceQty(BirthListItemMergeTable, True, strErrorMsg)
            grdScanItem.DataSource = BirthListItemMergeTable
            grdScanItem.AutoSizeCols()

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Calculate Total Items which are purchsed.
    ''' </summary>
    ''' <remarks></remarks>

    Public Sub CalculateTotal()
        Try
            If Not (BirthListItemMergeTable.Columns("PurchasedQty") Is DBNull.Value) Then
                Dim iTotalPurchasedQty As Integer
                'Calculate Total purchased items quantity
                Dim objsumTotalPurchasedQty As Object
                objsumTotalPurchasedQty = BirthListItemMergeTable.Compute("sum(PurchasedQty)", "")
                If Not (objsumTotalPurchasedQty Is DBNull.Value) Then
                    iTotalPurchasedQty = CInt(objsumTotalPurchasedQty)
                End If
                'Calculate Total Items
                Dim iTotalItems As Integer
                Dim objcountTotalPurchedItem As Object = BirthListItemMergeTable.Compute("count(EAN)", "PurchasedQty > 0")
                If Not (objcountTotalPurchedItem Is DBNull.Value) Then
                    iTotalItems = objcountTotalPurchedItem
                End If
                Dim decTotalAmount As Decimal
                Dim decTotalBlValue As Decimal
                If BirthListItemMergeTable.Rows.Count > 0 Then
                    decTotalAmount = IIf(BirthListItemMergeTable.Compute("sum(CurrentPurchasedAmount)", " ") Is DBNull.Value, 0, BirthListItemMergeTable.Compute("sum(CurrentPurchasedAmount)", " "))
                    RoundedAmt = decTotalAmount
                    decTotalAmount = MyRound(decTotalAmount, clsDefaultConfiguration.BillRoundOffAt)
                    RoundedAmt = decTotalAmount - RoundedAmt
                    RoundedAmt = FormatNumber(RoundedAmt, 2)
                    decTotalBlValue = IIf(BirthListItemMergeTable.Compute("sum(TotalNetAmount)", " ") Is DBNull.Value, 0, BirthListItemMergeTable.Compute("sum(TotalNetAmount)", " "))
                    decTotalBlValue = MyRound(decTotalBlValue, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)
                End If

                Dim decTotalTax As Decimal
                If BirthListItemMergeTable.Rows.Count > 0 Then
                    decTotalTax = IIf(BirthListItemMergeTable.Compute("sum(TaxAmt)", " ") Is DBNull.Value, 0, BirthListItemMergeTable.Compute("sum(TaxAmt)", " "))
                    decTotalTax = MyRound(decTotalTax, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)
                End If



                CashMemoSummaryDisplay(decTotalAmount, iTotalItems, iTotalPurchasedQty, decTotalBlValue)
            Else
                CashMemoSummaryDisplay(0.0, 0, 0, 0.0)
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("BL043"), "BL043 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'MessageBox.Show("Problem in total calculation.")
        End Try
    End Sub

    Private Function CashMemoSummaryDisplay(Optional ByVal decTotalAmount As Decimal = Decimal.Zero, Optional ByVal iTotalItems As Object = Nothing, Optional ByVal iTotalPurchasedQty As Object = Nothing, Optional ByVal decTotalBlValue As Decimal = 0.0) As Boolean
        CtrlCashSummary1.lbltxt1 = CurrencyFormat(decTotalAmount)
        CtrlCashSummary1.lbltxt2 = iTotalItems
        CtrlCashSummary1.lbltxt3 = iTotalPurchasedQty
        CtrlCashSummary1.lbltxt6 = decTotalBlValue

    End Function

    ''' <summary>
    ''' TODO:Check Is Already item added into grid or not
    ''' </summary>
    ''' <returns>Boolean</returns>
    '''  <usedby>frmBirthList.vb</usedby>
    ''' <remarks></remarks>
    ''' 

    Public Function IsItemAdded(ByVal strEANNumber As String, Optional ByVal decPrice As Decimal = Decimal.Zero) As Boolean
        Try
            'Dim d1 As DataColumn = BirthListItemMergeTable.Columns("EAN")
            'Dim d2 As DataColumn = BirthListItemMergeTable.Columns("SellingPrice")
            'Dim ckey(1) As DataColumn
            'ckey(0) = d1
            'ckey(1) = d2
            'BirthListItemMergeTable.PrimaryKey = ckey


            Dim dvData As DataView
            dvData = New DataView(BirthListItemMergeTable, "EAN='" & strEANNumber & "' and SellingPrice='" & decPrice & "'", "", DataViewRowState.CurrentRows)

            If (dvData.Count > 0) Then
                For Each drArticleCode As DataRowView In dvData
                    Dim rt As Integer = drArticleCode.Item("RequstedQty")
                    If Not rt + 1 > clsDefaultConfiguration.MaxQuantity Then
                        drArticleCode("RequstedQty") = rt + 1
                        Return True
                    Else
                        ShowMessage(getValueByKey("CM059"), "CM059 - " & getValueByKey("CLAE05"))
                        Return True
                    End If

                Next
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            ShowMessage(getValueByKey("BL072"), "BL072 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'MessageBox.Show("Problem to add new item")
        End Try
    End Function

    Private Function CheckOpenAmountAvailabel(ByVal strEANNumber) As Boolean
        Try
            If (OpenAmount > Decimal.Zero) Then
                Dim drArticleCode As DataRow = BirthListItemMergeTable.Rows.Find(strEANNumber)
                If Not drArticleCode Is Nothing Then
                    Dim decArticleQty As Decimal = drArticleCode.Item("RequstedQty")
                    Dim decArticleAmount As Decimal = drArticleCode.Item("SellingPrice") * decArticleQty
                    If (decArticleAmount > OpenAmount) Then
                        drArticleCode.Item("CurrentPurchasedAmount") = decArticleAmount - OpenAmount
                        OpenAmount = decArticleAmount - OpenAmount
                    ElseIf (OpenAmount > decArticleAmount) Then
                        drArticleCode.Item("CurrentPurchasedAmount") = Decimal.Zero
                        OpenAmount = OpenAmount - decArticleAmount
                    End If
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    Private isGIftVoucherDocumentPrint As Boolean
    Private dsPaymentReceipt As DataSet
    Private Sub btnAcceptPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'ValidateEditColumnEntry(grdScanItem.Cols(grdScanItem.Col).Name, grdScanItem.Row)
            Dim decOtherCharges As Decimal = 0.0
            If Not (BirthListID = String.Empty) Then
                If Not (CtrlCashSummary1.lbl5 = String.Empty) Then
                    Dim decAmount As Decimal = BirthListItemMergeTable.Compute("sum(CurrentPurchasedAmount)", " ")
                    If (decAmount > Decimal.Zero) Then
                        Dim objAcceptPayment As New frmNAcceptPayment
                        If Not decAmount = Decimal.Zero Then
                            Dim decTotalPaymet As Decimal
                            Dim NeedToPayAmount As Decimal
                            Dim descPayedAmount As Decimal
                            If Not dtPaymentReceipt Is Nothing Then
                                If dtPaymentReceipt.Rows.Count > 0 Then
                                    descPayedAmount = dtPaymentReceipt.Compute("sum(Amount)", " ")
                                End If
                            End If
                            If (IsCLPCustomer) Then
                                objAcceptPayment.CLPCustomerCardNumber = CtrlCustDtls1.dtCustmInfo.Rows(0)("CustomerNo")
                            End If
                            decTotalPaymet = BirthListItemMergeTable.Compute("sum(CurrentPurchasedAmount)", " ")
                            NeedToPayAmount = decTotalPaymet - NewOpenAmount
                            If (NeedToPayAmount > Decimal.Zero) Then
                                Dim objclsBirthListSales As New clsBirthListSales
                                If Not dsPaymentReceipt Is Nothing Then
                                    objAcceptPayment.PaymentType = clsAcceptPayment.PaymentType.EditBill
                                    objAcceptPayment.AcceptEditBillDataSet = dsPaymentReceipt
                                End If
                                NeedToPayAmount = MyRound(NeedToPayAmount, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)
                                objAcceptPayment.ParentRelation = "BrithList"
                                objAcceptPayment.TotalBillAmount = NeedToPayAmount
                                If CtrlCustDtls1.lblCustNoValue.Text <> String.Empty Then
                                    objAcceptPayment.CLPCustomerCardNumber = CtrlCustDtls1.lblCustNoValue.Text
                                End If
                                objAcceptPayment.ShowDialog(Me)
                                objAcceptPayment.Dispose()
                                objAcceptPayment.Close()

                                If Not (objAcceptPayment.IsCancelAcceptPayment) Then
                                    'Added by Rohit for CR5938

                                    _dDueDate = objAcceptPayment.dDueDate
                                    _strRemarks = objAcceptPayment.strRemarks

                                    dsPaymentReceipt = objAcceptPayment.ReciptTotalAmount
                                    dtPaymentReceipt = dsPaymentReceipt.Tables("MSTRecieptType")
                                    dtCheckDtls = dsPaymentReceipt.Tables("CheckDtls")

                                End If
                            Else
                                ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                                'MessageBox.Show("Amount is settled.")
                            End If
                            If (objAcceptPayment.Action = "Save") Then
                                'Added by Rohit for CR5938

                                _dDueDate = objAcceptPayment.dDueDate
                                _strRemarks = objAcceptPayment.strRemarks
                                GiftReceiptMessage = objAcceptPayment.GiftReceiptMessage
                                isGIftVoucherDocumentPrint = False
                            ElseIf objAcceptPayment.Action = "Gift" Then
                                'Added by Rohit for CR5938

                                _dDueDate = objAcceptPayment.dDueDate
                                _strRemarks = objAcceptPayment.strRemarks
                                isGIftVoucherDocumentPrint = True
                                GiftReceiptMessage = objAcceptPayment.GiftReceiptMessage
                            End If
                            btnSavePrint_Click(sender, e)
                        Else
                            ShowMessage(getValueByKey("BL025"), "BL025 - " & getValueByKey("CLAE04"))
                            'MessageBox.Show("First you have to purchase the item")
                        End If
                    Else
                        ShowMessage(getValueByKey("BL025"), "BL025 - " & getValueByKey("CLAE04"))
                        'MessageBox.Show("First you have to purchase the item")
                    End If
                Else
                    ShowMessage(getValueByKey("BL025"), "BL025 - " & getValueByKey("CLAE04"))
                    'MessageBox.Show("First you have to purchase the item")
                End If


            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function IsAmountIsDeductedFromOpenAmout() As Boolean
        Try
            Dim decAmount As Decimal = BirthListItemMergeTable.Compute("sum(CurrentPurchasedAmount)", " ")
            If (decAmount > Decimal.Zero) Then
                If Not decAmount = Decimal.Zero Then
                    Dim decTotalPaymet As Decimal
                    Dim NeedToPayAmount As Decimal
                    Dim descPayedAmount As Decimal
                    If Not dtPaymentReceipt Is Nothing Then
                        If dtPaymentReceipt.Rows.Count > 0 Then
                            descPayedAmount = dtPaymentReceipt.Compute("sum(Amount)", " ")
                        End If
                    End If
                    decTotalPaymet = BirthListItemMergeTable.Compute("sum(CurrentPurchasedAmount)", " ")
                    NeedToPayAmount = decTotalPaymet - NewOpenAmount
                    If NeedToPayAmount > Decimal.Zero Then
                        Return False
                    Else
                        Return True
                    End If
                End If
            End If

        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Sub RowState()
        For Each dr As DataRow In BirthListItemMergeTable.Rows
            Console.WriteLine(dr.RowState.ToString())
        Next
        For Each dr1 As POSDBDataSet.BirthListRequestedItemsRow In POSDBDataSet.BirthListRequestedItems.Rows
            Console.WriteLine(dr1.RowState.ToString())
        Next
    End Sub
    Dim OpenAmountForBL As Double
    Dim dtBirthListPaymentHistory As DataTable

    Private Function DisplayBirthListPaymentHistory() As Boolean
        Try
            'Dim dtBirthListPayment As New DataTable

            'Dim adpPaymentHistory As New POSDBDataSetTableAdapters.BirthListPaymentHistoryTableAdapter
            'dtBirthListPayment = adpPaymentHistory.GetBirthListPaymentHistory_ByBirthlistID(BirthListIDSearch)
            'gridPaymentHistory.DataSource = dtBirthListPayment


            Dim strQuery As New StringBuilder

            'Commented By Rohit

            'strQuery.Append("Select A.* from ")
            'strQuery.Append("(SELECT BirthListSalesHdr.BirthListId, BirthListSalesHdr.CustomerId, BirthListSalesHdr.SiteCode, BirthListSalesHdr.SaleInvNumber, ")
            'strQuery.Append(" BirthListSalesHdr.PaidAmt, BirthListSalesHdr.STATUS, CustomerSaleOrder.CustomerNo, CustomerSaleOrder.CustomerType, ")
            'strQuery.Append("CustomerSaleOrder.LastName + ' ' + CustomerSaleOrder.FirstName as CustomerName , CustomerSaleOrder.MiddleName, BirthListSalesHdr.CREATEDON, CustomerSaleOrder.FirstName, ")
            'strQuery.Append(" CustomerSaleOrder.LastName, CustomerSaleOrder.Gender, CustomerSaleOrder.STATUS AS CustomerStatus FROM BirthListSalesHdr INNER JOIN")
            'strQuery.Append(" CustomerSaleOrder ON BirthListSalesHdr.CustomerId = CustomerSaleOrder.CustomerNo WHERE       (BirthListSalesHdr.BirthListId ='" + BirthListIDSearch + "')")
            'strQuery.Append(" UNION SELECT     BirthListSalesHdr_1.BirthListId, BirthListSalesHdr_1.CustomerId, BirthListSalesHdr_1.SiteCode, BirthListSalesHdr_1.SaleInvNumber, ")
            'strQuery.Append(" BirthListSalesHdr_1.PaidAmt, BirthListSalesHdr_1.STATUS, CLPCustomers.CardNo, CLPCustomers.AccountNo,CLPCustomers.SurName + ' ' + CLPCustomers.FirstName, CLPCustomers.ClpProgramId, ")
            'strQuery.Append("  BirthListSalesHdr_1.CREATEDON, CLPCustomers.SurName, CLPCustomers.NameOnCard, CLPCustomers.CardType, ")
            'strQuery.Append(" CLPCustomers.STATUS AS CustomerStatus FROM  BirthListSalesHdr AS BirthListSalesHdr_1 INNER JOIN CLPCustomers ")
            'strQuery.Append(" ON BirthListSalesHdr_1.SiteCode = CLPCustomers.SiteCode AND BirthListSalesHdr_1.CustomerId = CLPCustomers.CardNo")
            'strQuery.Append(" WHERE (BirthListSalesHdr_1.BirthListId = '" + BirthListIDSearch + "') and CLPCustomers.Status=1) A")
            'strQuery.Append(" INNER JOIN BirthListSalesDtl B ON A.BirthListId = B.BirthListId and B.ArticleCode <> 'GVBaseArticle' and A.SaleInvNumber = B.SaleInvNumber")

            'Commented By Rakesh : 12-July-2012
            'strQuery.Append("select A.BirthListId,A.CustomerId,A.SiteCode,A.SaleInvNumber,sum(A.NetAmt) as PaidAmt, ")
            'strQuery.Append("A.STATUS,A.CREATEDON,B.CustomerNo , B.CustomerType,B.LastName + ' ' + B.FirstName as CustomerName, ")
            'strQuery.Append("B.MiddleName,B.FirstName,B.LastName,B.Gender, B.STATUS AS CustomerStatus ")
            'strQuery.Append("from BirthListSalesDtl A INNER JOIN  CustomerSaleOrder B ON A.CustomerId = B.CustomerNo  ")
            'strQuery.Append("where A.birthlistid = '" & BirthListIDSearch & "' and A.ArticleCode <> 'GVBaseArticle' ")
            'strQuery.Append("group by A.BirthListId,A.CustomerId,A.SiteCode,A.SaleInvNumber,A.STATUS, ")
            'strQuery.Append("A.CREATEDON,B.CustomerNo , B.CustomerType, B.CustomerType, ")
            'strQuery.Append("B.MiddleName,B.FirstName,B.LastName,B.Gender, B.STATUS UNION ")
            'strQuery.Append("select A.BirthListId,A.CustomerId,A.SiteCode,A.SaleInvNumber,sum(A.NetAmt) as PaidAmt, ")
            'strQuery.Append("A.STATUS,A.CREATEDON,B.CardNo , B.AccountNo, B.SurName + ' ' + B.FirstName as CustomerName, ")
            'strQuery.Append("B.ClpProgramId,B.SurName,B.NameOnCard,B.CardType, B.STATUS AS CustomerStatus ")
            'strQuery.Append("from BirthListSalesDtl A INNER JOIN  CLPCustomers B ON A.SiteCode = B.SiteCode ")
            'strQuery.Append("AND A.CustomerId = B.CardNo where A.birthlistid = '" & BirthListIDSearch & "' and ")
            'strQuery.Append("A.ArticleCode <> 'GVBaseArticle' group by A.BirthListId,A.CustomerId, ")
            'strQuery.Append("A.SiteCode,A.SaleInvNumber,A.STATUS,A.CREATEDON,B.CardNo, ")
            'strQuery.Append("B.AccountNo, B.ClpProgramId,B.SurName, B.FirstName,B.NameOnCard,B.CardType, B.STATUS ")

            strQuery.Append("Select A.BirthListId,A.CustomerId,A.SiteCode,A.SaleInvNumber,sum(A.NetAmt) as PaidAmt, " & vbCrLf)
            strQuery.Append("A.STATUS,A.CREATEDON,B.CustomerNo , B.CustomerType,B.LastName + ' ' + B.FirstName as CustomerName, " & vbCrLf)
            strQuery.Append("B.Gender, B.STATUS AS CustomerStatus " & vbCrLf)
            strQuery.Append("from BirthListSalesDtl A " & vbCrLf)
            strQuery.Append("INNER JOIN  CustomerSaleOrder B ON A.CustomerId = B.CustomerNo And A.CustomerType = B.CustomerType " & vbCrLf)
            strQuery.Append("Where A.birthlistid = '" & BirthListIDSearch & "' and A.ArticleCode <> 'GVBaseArticle' " & vbCrLf)
            strQuery.Append("Group by A.BirthListId,A.CustomerId,A.SiteCode,A.SaleInvNumber,A.STATUS, A.CREATEDON,B.CustomerNo, " & vbCrLf)
            strQuery.Append("B.CustomerType, B.CustomerType, B.MiddleName, B.FirstName, B.LastName, B.Gender, B.STATUS " & vbCrLf)
            strQuery.Append("UNION " & vbCrLf)
            strQuery.Append("Select A.BirthListId,A.CustomerId,A.SiteCode,A.SaleInvNumber,sum(A.NetAmt) as PaidAmt, A.STATUS, " & vbCrLf)
            strQuery.Append("A.CREATEDON, B.AccountNo As CustomerNo, 'CLP' As CustomerType,B.SurName + ' ' + B.FirstName as CustomerName, B.Gender, B.STATUS AS CustomerStatus " & vbCrLf)
            strQuery.Append("from BirthListSalesDtl A " & vbCrLf)
            strQuery.Append("INNER JOIN  CLPCustomers B ON A.SiteCode = B.SiteCode AND A.CustomerId = B.CardNo And A.CustomerType = 'CLP' " & vbCrLf)
            strQuery.Append("Where A.birthlistid = '" & BirthListIDSearch & "' and A.ArticleCode <> 'GVBaseArticle' " & vbCrLf)
            strQuery.Append("Group by A.BirthListId,A.CustomerId, A.SiteCode,A.SaleInvNumber,A.STATUS, " & vbCrLf)
            strQuery.Append("A.CREATEDON, B.AccountNo, B.SurName, B.FirstName, B.Gender, B.STATUS " & vbCrLf)


            Dim objclsComman As New clsBirthListGobal
            Dim strErrorMsg As String = String.Empty
            dtBirthListPaymentHistory = objclsComman.RetrieveQuery(strQuery.ToString(), strErrorMsg)
            '' start here-- Added by rama for OpenAmount to be deducted from TotalPaymentReceived -date 11-Aug-2011
            '' calculating the open amount by which items purchased 
            Dim dtOpenAmount As DataTable = objclsComman.RetrieveQuery("select sum(isnull(B.convToVoucher,0)*(isnull(B.sellingPrice,0))) -isnull(A.openAmount,0) Amount " & _
                                            " from BirthList A inner join " & _
                                            " birthlistrequesteditems b On A.SiteCode=b.SiteCode and A.FinYear=B.FinYear And A.BirthListId=B.BirthListID " & _
                                            " Where A.birthlistid='" & BirthListIDSearch & "' And A.Sitecode='" & clsAdmin.SiteCode & "'" & _
                                            " Group by A.openAmount ", strErrorMsg)
            ''end
            If strErrorMsg = String.Empty Then
                gridPaymentHistory.DataSource = dtBirthListPaymentHistory
                If dtBirthListPaymentHistory.Rows.Count > 0 Then
                    lblCalTotalAmountPaymentHistory.Text = CurrencyFormat(IIf(dtBirthListPaymentHistory.Compute("sum(PaidAmt)", " ") Is DBNull.Value, 0, dtBirthListPaymentHistory.Compute("sum(PaidAmt)", " ")))
                    '' start here-- Added by rama for OpenAmount to be deducted from TotalPaymentReceived -date 07-Oct-2010s
                    If Not dtOpenAmount Is Nothing AndAlso dtOpenAmount.Rows.Count > 0 Then
                        OpenAmountForBL = IIf(dtOpenAmount.Rows(0)(0) Is DBNull.Value, 0, dtOpenAmount.Rows(0)(0))
                        'lblCalTotalAmountPaymentHistory.Text = IIf(lblCalTotalAmountPaymentHistory.Text <> String.Empty, CDbl(lblCalTotalAmountPaymentHistory.Text), 0) - OpenAmountForBL
                        lblCalTotalAmountPaymentHistory.Text = IIf(lblCalTotalAmountPaymentHistory.Text <> String.Empty, CDbl(lblCalTotalAmountPaymentHistory.Text), 0)
                    End If
                    ''End
                    btnCancel.Enabled = False
                Else
                    lblCalTotalAmountPaymentHistory.Text = FormatNumber("0", 2)
                    btnCancel.Enabled = True
                End If
            End If

        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    Private Sub btnReturns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturns.Click
        Try
            If (ValidateUserInput()) Then
                IsCLPCalculate = False
                ReturnItems()
                IsCLPCalculate = True
            End If
        Catch ex As Exception
            IsCLPCalculate = True
            LogException(ex)
        End Try
    End Sub

    Private dtOriginalgrdScanItem As DataTable

    Private Function ReturnItems() As Boolean
        Try
            If (RetunDeliveryQtyCheck()) Then
                For Each cln As C1.Win.C1FlexGrid.Column In grdScanItem.Cols
                    cln.Visible = False
                Next
                grdScanItem.Cols("CurrentReturnReason").Visible = True
                grdScanItem.Cols("CurrentReturnQty").Visible = True
                grdScanItem.Cols("DISCRIPTION").Visible = True
                grdScanItem.Cols("EAN").Visible = True
                grdScanItem.Cols("SellingPrice").Visible = True
                grdScanItem.Cols("DeliveredQty").Visible = True
                txtBirthListid.Enabled = False
                btnReturnCancel.Visible = True
                BtnReturnSave.Visible = True
                BirthListReturnMode()
                grdScanItem.AllowEditing = True
                btnSearchBirthListID.Enabled = False
                grdScanItem.Enabled = True
            Else
                ShowMessage(getValueByKey("BL074"), "BL074 - " & getValueByKey("CLAE04"))
                'MessageBox.Show("There is no Delivery Item for Current BirthList")
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function

    Private Function RetunDeliveryQtyCheck() As Boolean
        Try
            Dim dtDeliverdItems As DataTable = grdScanItem.DataSource
            dtOriginalgrdScanItem = dtDeliverdItems
            Dim dtSourceToGridReturn As DataTable = dtDeliverdItems.Clone()
            For Each dr As DataRow In dtDeliverdItems.Select("DeliveredQty > 0", "", DataViewRowState.OriginalRows)
                dtSourceToGridReturn.ImportRow(dr)
            Next
            If (dtSourceToGridReturn.Rows.Count > 0) Then
                grdScanItem.DataSource = dtSourceToGridReturn
                grdScanItem.AutoSizeCols()
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Function ReturnCheckChange() As Boolean
        Try
            Dim IsReturnItem As Boolean = False
            Dim dtReturnData As DataTable = grdScanItem.DataSource
            For Each drChangedReturn As DataRow In dtReturnData.Rows
                If Not drChangedReturn Is DBNull.Value Then
                    If Not drChangedReturn("CurrentReturnQty") Is DBNull.Value AndAlso (drChangedReturn("CurrentReturnQty") > 0) Then
                        If Not drChangedReturn("CurrentReturnReason") Is Nothing AndAlso Not drChangedReturn("CurrentReturnReason") Is DBNull.Value AndAlso Not drChangedReturn("CurrentReturnReason") = String.Empty Then
                            Dim drView As DataView
                            drView = New DataView(dtOriginalgrdScanItem, "EAN='" & drChangedReturn("EAN") & "' and SellingPrice='" & drChangedReturn("SellingPrice") & "'", "", DataViewRowState.CurrentRows)
                            For Each drReturnQty As DataRowView In drView
                                drReturnQty.BeginEdit()
                                drReturnQty("CurrentReturnQty") = drChangedReturn("CurrentReturnQty")
                                drReturnQty("CurrentReturnReason") = drChangedReturn("CurrentReturnReason")
                                drReturnQty.EndEdit()
                                IsReturnItem = True
                            Next
                        Else
                            ShowMessage(getValueByKey("BL085"), "BL085 - " & getValueByKey("CLAE04"))
                            Return False
                        End If
                    End If
                End If
            Next
            If Not IsReturnItem Then
                Dim result As DialogResult = MessageBox.Show(getValueByKey("BL075"), "BL075 - " & getValueByKey("CLAE04"), MessageBoxButtons.YesNo)
                'Dim result As DialogResult = MessageBox.Show("Nothing is change. Do you want close Return ItemView? ", "ReturnItem", MessageBoxButtons.YesNo)
                If (result = Windows.Forms.DialogResult.Yes) Then
                    ReturnAfterSave()
                End If
            End If
            Return IsReturnItem
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Function ReturnAfterSave() As Boolean
        Try
            For Each cln As C1.Win.C1FlexGrid.Column In grdScanItem.Cols
                If Not (cln.Visible) Then
                    cln.Visible = True
                End If
            Next

            BirthListInNormalMode()




            grdScanItem.Cols("CurrentReturnReason").Visible = False
            grdScanItem.Cols("CurrentReturnQty").Visible = False
            'tbPageGV.Show()
            'tbPagePaymentHistory.Show()
            Dim dtReturnData As DataTable = grdScanItem.DataSource
            For Each drChangedReturn As DataRow In dtReturnData.Rows
                Dim drView As DataView
                drView = New DataView(dtOriginalgrdScanItem, "CurrentReturnQty >0", "", DataViewRowState.CurrentRows)
                For Each drReturnQty As DataRowView In drView
                    drReturnQty.BeginEdit()

                    drReturnQty("CurrentReturnQty") = 0
                    drReturnQty("CurrentReturnReason") = ""
                    drReturnQty("PurchasedQty") = 0
                    drReturnQty("PickupQty") = 0
                    drReturnQty.EndEdit()
                Next
            Next
            grdScanItem.DataSource = dtOriginalgrdScanItem
            SearchBirthListInformation()
            If grdScanItem.Cols.Contains("FreezeSB") Then
                grdScanItem.Cols("FreezeSB").Visible = False
            End If
            If grdScanItem.Cols.Contains("FreezeOB") Then
                grdScanItem.Cols("FreezeOB").Visible = False
            End If
            If grdScanItem.Cols.Contains("FreezeSR") Then
                grdScanItem.Cols("FreezeSR").Visible = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Function ReturnSave() As Boolean
        Try

        Catch ex As Exception

        End Try
    End Function

    Private Sub BirthListInReadonlyMode()
        Try
            If Not POSDBDataSet.BirthList Is Nothing AndAlso POSDBDataSet.BirthList.Rows.Count > 0 Then
                If (POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListStatusColumn) = "Cancel" Or POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListStatusColumn) = "Closed" Or POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListStatusColumn) = "Close-DeliveryP") Then
                    Dim strmsg As String = ""
                    strmsg = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListStatusColumn)
                    btnReturnCancel.Visible = False
                    BtnReturnSave.Visible = False
                    BirthListReturnMode()
                Else

                    BirthListInNormalMode()
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub BirthListReturnMode()
        Try
            CtrlSalesPerson1.Enabled = False
            grdScanItem.AllowEditing = False
            CtrlSalesPerson1.CtrlTxtBox.Enabled = False
            btnCancel.Enabled = False
            btnClose.Enabled = False
            btnReturns.Enabled = False
            'CtrlRbn1.Enabled = False
            CtrlRbn1.DgrpPayments.Enabled = False
            btnSaveAndPrint.Enabled = False
            btnPrint.Enabled = True
            'btnApplyDiscountAndClose.Enabled = False
            btnApplyDiscountAndClose1.Enabled = False
            CtrlSalesPerson1.CtrlCmdSearch.Enabled = False
            rbnbtnSearchBL.Enabled = False
            c1dateEditEventDate.Enabled = False
            c1dateEditDeliverydate.Enabled = False
            btnPurchaseQty.Enabled = False
            btnPickUpQty.Enabled = False
            btnCovertToOpenAmount.Enabled = False
            btnShowAllclmGrid.Enabled = False
            CtrlRbn1.DbtnF12.Enabled = False
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub BirthListInNormalMode()
        Try

            CtrlSalesPerson1.Enabled = True
            grdScanItem.AllowEditing = True
            grdScanItem.Enabled = True
            CtrlSalesPerson1.CtrlTxtBox.Enabled = True
            CtrlSalesPerson1.CtrlCmdSearch.Enabled = True
            btnClose.Enabled = True
            btnReturns.Enabled = True
            CtrlRbn1.Enabled = True
            btnSaveAndPrint.Enabled = True
            btnSearchBirthListID.Enabled = True
            btnReturnCancel.Visible = False
            BtnReturnSave.Visible = False
            txtBirthListid.Enabled = True
            c1dateEditEventDate.Enabled = True
            c1dateEditDeliverydate.Enabled = True
            CtrlRbn1.DgrpPayments.Enabled = True
            btnApplyDiscountAndClose1.Enabled = True
            rbnbtnSearchBL.Enabled = True
            btnPrint.Enabled = True
            c1dateEditEventDate.Enabled = True
            c1dateEditDeliverydate.Enabled = True
            btnPurchaseQty.Enabled = True
            btnPickUpQty.Enabled = True
            btnCovertToOpenAmount.Enabled = True
            btnShowAllclmGrid.Enabled = True
            CtrlRbn1.DbtnF12.Enabled = True
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BtnReturnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReturnSave.Click
        Try
            If (IsDeliveryDateBackDated) Then
                If (ReturnCheckChange()) Then
                    btnSavePrint_Click(sender, e)
                    ReturnAfterSave()


                End If
            End If
            IsCLPCalculate = True
        Catch ex As Exception
            IsCLPCalculate = True
            LogException(ex)
        End Try
    End Sub

    Private Sub BtnReturnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturnCancel.Click
        Try
            Dim result As DialogResult = MessageBox.Show(getValueByKey("BL076"), "BL076 - " & getValueByKey("CLAE04"), MessageBoxButtons.YesNo)
            'Dim result As DialogResult = MessageBox.Show("Do you want close Return ItemView? ", "ReturnItem", MessageBoxButtons.YesNo)
            If (result = Windows.Forms.DialogResult.Yes) Then
                ReturnAfterSave()
            End If
        Catch ex As Exception
            LogException(ex)
        Finally
            IsCLPCalculate = True
        End Try

    End Sub

    Private Sub frmNBirthListUpdate_EnabledChanged(sender As Object, e As System.EventArgs) Handles Me.EnabledChanged

    End Sub

    Private Sub frmBirthListUpdation_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try


            ' Dim dResult As DialogResult = MessageBox.Show(getValueByKey("BL047"), getValueByKey("CLAE04"), MessageBoxButtons.YesNo)
            If Not (isFormClosed) Then
                If Not BirthList_CustomerInformation Is Nothing Then
                    Dim dResult As DialogResult = MessageBox.Show(getValueByKey("BL047"), "BL047 - " & getValueByKey("CLAE04"), MessageBoxButtons.YesNo)
                    If (dResult = Windows.Forms.DialogResult.Yes) Then
                        e.Cancel = False
                        isFormClosed = True
                    ElseIf (dResult = Windows.Forms.DialogResult.No) Then
                        e.Cancel = True
                        isFormClosed = False
                    End If
                Else
                    e.Cancel = False
                    isFormClosed = True
                End If
            End If


        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnFormClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            If BirthListID <> String.Empty Then
                Dim dtBirthListArticleDtls As New POSDBDataSet.BirthListCancelDataTable
                Dim AdpBirthListCancel As New POSDBDataSetTableAdapters.BirthListCancelTableAdapter
                dtBirthListArticleDtls = AdpBirthListCancel.GetData(clsAdmin.SiteCode, BirthListID)
                If (dtBirthListArticleDtls.Rows.Count > 0) Then
                    ShowMessage(getValueByKey("BL077"), "BL077 - " & getValueByKey("CLAE04"))
                    'MessageBox.Show("You can't cancel Current birthlist")
                Else
                    Dim dResult As DialogResult = MessageBox.Show(getValueByKey("BL079"), "BL079 - " & getValueByKey("CLAE04"), MessageBoxButtons.YesNo)

                    'Dim dResult As DialogResult = MessageBox.Show("Do you want to Cancel BirthList ?", "BirthListSales", MessageBoxButtons.YesNo)
                    If (dResult = Windows.Forms.DialogResult.Yes) Then
                        AdpBirthListCancel.UpdateStatus(clsAdmin.SiteCode, BirthListIDSearch)
                        SearchBirthListOnTextChange()
                        ShowMessage(getValueByKey("BL078"), "BL078 - " & getValueByKey("CLAE04"))
                        'MessageBox.Show("BirthList canceled sucessfully.") 
                        ClearBirthListInformation()
                        AutoLogout(FrmTranCode, Me, lblLoggedIn)
                    ElseIf (dResult = Windows.Forms.DialogResult.No) Then
                    End If
                End If
            Else
                btnSearchBirthListID_Click(sender, e)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function BirthListClose(ByVal strStatus As String, ByVal decBLDisount As Decimal, ByVal decCLPDiscount As Decimal, ByVal decCLPPoints As Decimal) As Boolean
        Try
            Dim objclsBLGlobal As New clsCommon

            Dim dateCreated As Date = objclsBLGlobal.GetCurrentDate()
            Dim dtBirthListArticleDtls As New POSDBDataSet.BirthListCancelDataTable
            Dim AdpBirthListCancel As New POSDBDataSetTableAdapters.BirthListCancelTableAdapter
            dtBirthListArticleDtls = AdpBirthListCancel.GetData(clsAdmin.SiteCode, BirthListIDSearch)
            'AdpBirthListCancel.UpdateStatusClose(decBLDisount, decCLPDiscount, decCLPPoints, Decimal.Add(decBLDisount, decCLPDiscount), strStatus, clsAdmin.SiteCode, BirthListIDSearch)
            Dim objclsBirthListUpdate As New clsBirthListUpdate
            If Not (objclsBirthListUpdate.SaveBirthListClose(clsAdmin.SiteCode, clsAdmin.UserName, clsAdmin.Financialyear, BirthListIDSearch, "BL1", Decimal.Add(decBLDisount, decCLPDiscount), strStatus, decBLDisount, decCLPDiscount, decCLPPoints, dateCreated, dtBirthListArticleDtls, AdpBirthListCancel, POSDBDataSet.BirthListRequestedItems, BirthListRequestedItemsTableAdapter1, clsAdmin.DayOpenDate.ToString(clsAdmin.SqlDBDateFormat))) Then
                ShowMessage(getValueByKey("BL086"), "BL086 - " & getValueByKey("CLAE04"))
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Sub txtItemSearch_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs)
        Try
            If (e.KeyCode = Keys.Enter) Then
                If (IsItemSearch) Then
                    If Not (BirthListIDSearch = String.Empty) Then
                        If (CtrlSalesPerson1.CtrlTxtBox.Text.Length > 0) Then
                            ManualEnterEANNumber()
                            CtrlSalesPerson1.CtrlTxtBox.Clear()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Dim objclsBirthList As New SpectrumBL.clsBirthList(clsAdmin.Financialyear)

    ''' <summary>
    '''  Entered EAN number for article by keybord
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 

    Private Function ManualEnterEANNumber() As Boolean
        Try
            Dim drSelectedItemDetails As DataRow
            Dim dtItem As DataTable
            If (objclsBirthList.FindArticleCode(dtItem, CtrlSalesPerson1.CtrlTxtBox.Text, clsAdmin.SiteCode, EanType, clsAdmin.LangCode)) Then
                drSelectedItemDetails = SearchBirthListItem(dtItem)
                If Not drSelectedItemDetails Is Nothing Then
                    If drSelectedItemDetails("FreezeSR") = True Then
                        ShowMessage(getValueByKey("BL100"), "BL100 - " & getValueByKey("CLAE04"))
                        Return False
                    End If
                    If (objclsBirthListGlobal.IsArticleRateAvailabel(drSelectedItemDetails, "SellingPrice", "")) Then
                        If objclsBirthListGlobal.IsStockAvailable(clsDefaultConfiguration.NegativeInventoryAllowed, NewItemAdd, "AvailableQty") Then
                            If Not drSelectedItemDetails Is Nothing Then
                                Grid_AddNewItem(drSelectedItemDetails)
                                CalculateTotal()
                                CtrlProductImage1.ShowArticleImage(drSelectedItemDetails("ArticleCode"))
                                Return True
                            Else
                                ShowMessage(getValueByKey("BL014"), "BL014 - " & getValueByKey("CLAE04"))
                                'MessageBox.Show("Item is not found or not for sale")
                                Return False
                            End If
                        Else
                            ShowMessage(getValueByKey("BL014"), "BL014 - " & getValueByKey("CLAE04"))
                            'MessageBox.Show("Item is not found or not for sale")
                            Return False
                        End If

                    Else
                        ShowMessage(getValueByKey("BL081"), "BL081 - " & getValueByKey("CLAE04"))
                        'Article Rate no
                        Return False
                    End If
                Else
                    'MessageBox.Show(getValueByKey("BL014"), "BL014")
                    'MessageBox.Show("Item is not found or not for sale")
                    Return False
                End If

            Else
                ShowMessage(getValueByKey("BL014"), "BL014 - " & getValueByKey("CLAE04"))
                'MessageBox.Show("Item is not found or not for sale")
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Sub txtBirthListid_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs)
        Try
            If (e.KeyCode = Keys.Enter) Then
                If BirthListIDSearch <> String.Empty Then
                    SearchBirthListOnTextChange()
                    If Not POSDBDataSet.BirthList Is Nothing Then
                        If POSDBDataSet.BirthList.Rows.Count > 0 Then
                            cboEvent.SelectedValue = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.EventIdColumn)
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Function UpdateInformationForSalesDetails() As Boolean
        Try
            Dim drsSalesInforamtion As DataRow() = BirthListItemMergeTable.Select("OpenAmountQty > 0")
            Dim strArticleCode As String
            Dim strEAN As String
            Dim iOpenAmountQTY As Integer
            For Each drRows As DataRow In drsSalesInforamtion
                If (drRows("OpenAmountQty") > 0) Then
                    strArticleCode = drRows("ArticleCode")
                    strEAN = drRows("EAN")
                    iOpenAmountQTY = drRows("OpenAmountQty")
                    OpenAmountSalesUpdationCal(iOpenAmountQTY, strArticleCode, strEAN)
                End If
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Dim AdpOpenQtyBirthListSalesDtl As New POSDBDataSetTableAdapters.OpenQtyBirthListSalesDtlTableAdapter

    Dim dtOpenQtyBirthListSalesDtl As New POSDBDataSet.OpenQtyBirthListSalesDtlDataTable

    Private Function OpenAmountSalesUpdationCal(ByVal iOpenQty As Integer, ByVal strArticleCode As String, ByVal strEAN As String) As Boolean
        Try
            'For index As Integer = 1 To iOpenQty
            'Next
            'AdpOpenQtyBirthListSalesDtl = POSDBDataSetTableAdapters.OpenQtyBirthListSalesDtlTableAdapter


            Dim dtOpenQtyBirthListSalesDtlNew As POSDBDataSet.OpenQtyBirthListSalesDtlDataTable = AdpOpenQtyBirthListSalesDtl.GetData(clsAdmin.SiteCode, BirthListIDSearch, strEAN, strArticleCode)

            dtOpenQtyBirthListSalesDtl.Merge(dtOpenQtyBirthListSalesDtlNew)
            Dim strFilterCriteria As String = String.Format(" EAN={1} and ArticleCode={2} and BookedQty={0}  ", iOpenQty, strEAN, strArticleCode)
            Dim IsINsert As Boolean = False
            Dim drView As DataView
            drView = New DataView(dtOpenQtyBirthListSalesDtl, strFilterCriteria, "", DataViewRowState.CurrentRows)
            If drView.Count > 0 Then
                Dim iBookedQty As Integer
                Dim iDeliveryQty As Integer
                Dim iOpenAmountQty As Integer
                Dim TotalAvailabelToInsert As Integer
                Dim dtView As DataTable = drView.ToTable()
                Dim strBirthListID As String = dtView.Rows(0)("BirthListID")
                Dim strEAN1 As String = dtView.Rows(0)("EAN")
                Dim strArticleCode1 As String = dtView.Rows(0)("ArticleCode")
                Dim strSalesInvoiceNo As String = dtView.Rows(0)("SaleInvNumber")
                Dim dateUpdatesale As Date = objclscomman.GetCurrentDate()
                Dim drRow As DataRow() = dtOpenQtyBirthListSalesDtl.Select("SaleInvNumber='" & strSalesInvoiceNo & "' and EAN='" & strEAN1 & "' and ArticleCode ='" & strArticleCode1 & "'")
                For Each drt As DataRow In drRow
                    iBookedQty = drt("BookedQty")
                    iDeliveryQty = drt("DeliveredQty")
                    If Not drt("OpenAmountQty") Is DBNull.Value Then
                        iOpenAmountQty = drt("OpenAmountQty")
                    Else
                        iOpenAmountQty = 0
                    End If

                    TotalAvailabelToInsert = iBookedQty - (iDeliveryQty + iOpenAmountQty)
                    If TotalAvailabelToInsert = iOpenQty Then
                        drt.BeginEdit()
                        drt("OpenAmountQty") = iOpenQty
                        drt.EndEdit()
                        IsINsert = True

                        Exit For
                    End If

                Next
            End If
            If Not IsINsert Then
                Dim strFilterCriteriaNext As String = String.Format(" EAN={0} and ArticleCode={1} ", strEAN, strArticleCode)
                Dim drViewNext As DataView
                drViewNext = New DataView(dtOpenQtyBirthListSalesDtl, strFilterCriteriaNext, "", DataViewRowState.CurrentRows)
                Dim cOpenAmountQty As Integer = iOpenQty
                If drViewNext.Count > 0 Then
                    Dim dtView As DataTable = drViewNext.ToTable()
                    For Each drdtView As DataRow In dtView.Rows
                        Dim strBirthListID As String = drdtView("BirthListID")
                        Dim strEAN1 As String = drdtView("EAN")
                        Dim strArticleCode1 As String = drdtView("ArticleCode")
                        Dim strSalesInvoiceNo As String = drdtView("SaleInvNumber")
                        Dim dateUpdatesale As Date = objclscomman.GetCurrentDate()
                        Dim drRow As DataRow() = dtOpenQtyBirthListSalesDtl.Select("SaleInvNumber='" & strSalesInvoiceNo & "' and EAN='" & strEAN1 & "' and ArticleCode ='" & strArticleCode1 & "'")
                        Dim iBookedQty As Integer
                        Dim iDeliveryQty As Integer
                        Dim iOpenAmountQty As Integer
                        Dim TotalAvailabelToInsert As Integer
                        For Each drt As DataRow In drRow
                            iBookedQty = drt("BookedQty")
                            iDeliveryQty = drt("DeliveredQty")
                            If Not drt("OpenAmountQty") Is DBNull.Value Then
                                iOpenAmountQty = drt("OpenAmountQty")
                            Else
                                iOpenAmountQty = 0
                            End If

                            TotalAvailabelToInsert = iBookedQty - (iDeliveryQty + iOpenAmountQty)

                            If TotalAvailabelToInsert > 0 Then
                                If TotalAvailabelToInsert >= iOpenQty Then
                                    drt.BeginEdit()
                                    drt("OpenAmountQty") = iOpenQty
                                    drt.EndEdit()
                                ElseIf TotalAvailabelToInsert < iOpenQty Then
                                    Do Until TotalAvailabelToInsert = cOpenAmountQty
                                        cOpenAmountQty = cOpenAmountQty - 1
                                    Loop
                                    drt.BeginEdit()
                                    drt("OpenAmountQty") = cOpenAmountQty
                                    drt.EndEdit()
                                End If
                                iOpenQty = iOpenQty - cOpenAmountQty
                            End If

                        Next
                    Next
                End If

            End If

            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function

    Private ilastOpenAmountQty As Integer

    Private Sub grdScanItem_BeforeEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdScanItem.BeforeEdit
        Try
            Dim iRowIndex As Integer = e.Row
            Dim iOpenAmountQty As Integer = ReturnColumnValue(iRowIndex, "OpenAmountQty")
            decOldReservedQty = ReturnColumnValue(iRowIndex, "ReservedQty")
            'Change for CR 5679
            'To not allow changes to OrderQty if the price of it has already been changed
            Dim iCol As Integer = e.Col
            If e.Col = 5 Then
                If BirthListItemMergeTable.Rows.Count > 0 And e.Row <> 0 Then
                    If Not IsDBNull(BirthListItemMergeTable.Rows(e.Row - 1)("IsPriceChanged")) Then
                        If Not String.IsNullOrEmpty(BirthListItemMergeTable.Rows(e.Row - 1)("IsPriceChanged")) Then
                            If BirthListItemMergeTable.Rows(e.Row - 1)("IsPriceChanged") Then
                                e.Cancel = True
                            Else
                                e.Cancel = False
                            End If
                        Else
                            e.Cancel = False
                        End If
                    Else
                        e.Cancel = False
                    End If
                End If

            End If

            If Not grdScanItem.DataSource Is Nothing Then



                If clsDefaultConfiguration.CLP_Applicable_Edit Then

                    If Not grdScanItem.Cols("IsCLP") Is Nothing Then



                        grdScanItem.Cols("IsCLP").AllowEditing = True
                    End If

                Else
                    If Not grdScanItem.Cols("IsCLP") Is Nothing Then

                        grdScanItem.Cols("IsCLP").AllowEditing = False
                    End If

                End If

            End If
            'end of change
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function PrintCreditVoucher(ByVal objAmountApplicable As Object, Optional ByVal strVoucherNo As String = "") As Boolean
        Try
            Dim decTotalVoucherAmout As Decimal = MyRound(objAmountApplicable, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)

            Dim objclsPrintBirthList As New clsPrintVoucherNew

            Dim currentDate As Date = objclscomman.GetCurrentDate()


            '*********Old Print Format ****************************
            'If (objclsPrintBirthList.PrintVoucher(currentDate, "BLS", BirthListIDSearch, clsAdmin.UserName, decTotalVoucherAmout, BirthList_CustomerInformation.Table, strVoucherNo)) Then
            '    Return True
            'Else
            '    Return False
            'End If
             
            Dim expiryDate As Date = objclsBirthListGlobal.CalculateExpiryDate(currentDate, clsAdmin.CreditValidDays)

            objclsPrintBirthList.PrintGiftVoucherAndCreditNote("BLS", clsAdmin.SiteCode, "CreditNote", strVoucherNo, decTotalVoucherAmout.ToString(), expiryDate, clsAdmin.UserName, BirthListID, clsAdmin.CurrencyCode, currentDate, BarCodeType)
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Function PrintGiftVouchers() As Boolean
        Try
            Dim objclsBirthListPrinting As Object

            objclsBirthListPrinting = New clsPrintVoucherNew


            If Not dtGVdetails Is Nothing Then
                For Each dr As DataRow In dtGVdetails.Rows
                    If Not dr("ExpiryDate") Is DBNull.Value Then
                        objclsBirthListPrinting.PrintGiftVoucherAndCreditNote("BL", clsAdmin.SiteCode, "GiftVoucher", dr("VOURCHERSERIALNBR"), dr("NETAMOUNT"), dr("ExpiryDate"), clsAdmin.UserName, BirthListID, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                    Else
                        objclsBirthListPrinting.PrintGiftVoucherAndCreditNote("BL", clsAdmin.SiteCode, "GiftVoucher", dr("VOURCHERSERIALNBR"), dr("NETAMOUNT"), dr("ExpiryDate"), clsAdmin.UserName, BirthListID, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Function SaveCreditVoucher(ByVal dateCurrent As Date, ByVal objAmountApplicable As Object, ByRef sqlTran As SqlClient.SqlTransaction, ByRef genVoucherNO As String) As Boolean
        Try
            If Not (objAmountApplicable Is DBNull.Value) Then
                Dim decTotalVoucherAmout As Decimal = CDbl(objAmountApplicable)
                Dim objclsBirthListGobal As New clsBirthListGobal
                 
                'Dim daysForExpiry As Integer = CInt(clsDefaultConfiguration.BLCreditVoucherExpiryInDays)

                Dim dateExpiry As Date = objclsBirthListGobal.CalculateExpiryDate(dateCurrent, clsAdmin.CreditValidDays)
                If (objclsBirthListGobal.SaveCreditVoucher(sqlTran, BirthListIDSearch, dateCurrent, dateExpiry, clsAdmin.CVProgram, "BLS", clsAdmin.SiteCode, clsAdmin.UserCode, decTotalVoucherAmout, genVoucherNO, clsAdmin.CreditValidDays, OnlineConnect, clsAdmin.TerminalID, clsAdmin.Financialyear, clsAdmin.CurrencyCode, clsAdmin.DayOpenDate)) Then

                    Return True
                Else
                    If Not sqlTran.Connection Is Nothing Then
                        sqlTran.Rollback()
                        Return False
                    End If

                End If
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Function AddEvent() As Boolean
        Try
            AddHandler CtrlSalesPerson1.CtrlCmdSearch.Click, AddressOf BtnItemSearch_Click  'Search item on button click 
            AddHandler CtrlSalesPerson1.CtrlTxtBox.PreviewKeyDown, AddressOf txtItemSearch_PreviewKeyDown ' Search Item on pressing event in items search text box 
            AddHandler CtrlRbn1.DbtnPayCard.Click, AddressOf rbnbtnPayCard_Click
            AddHandler CtrlRbn1.DbtnPayCash.Click, AddressOf rbnbtnPayCash_Click
            AddHandler CtrlRbn1.DbtnPay.Click, AddressOf btnAcceptPayment_Click
            AddHandler CtrlRbn1.DbtnpayCheque.Click, AddressOf rbnbtnPayCheque_Click
            CtrlRbn1.pInitRbn()

            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Sub frmBirthListUpdateNew_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            AddHandler CtrlRbn1.DbtnF12.Click, AddressOf PriceChange
            'CtrlRbn1.pInitRbn()
            If Not IsReprintView = True Then

                grdScanItem.Cols("CurrentPurchasedAmount").Format = GridAmountColumnCustomeFormat()
                grdScanItem.Cols("SellingPrice").Format = GridAmountColumnCustomeFormat()
                grdScanItem.Cols("NetAmount").Format = GridAmountColumnCustomeFormat()
                grdScanItem.DataSource = BirthListItemMergeTable
                AddHandler txtBirthListid.PreviewKeyDown, AddressOf txtBirthListid_PreviewKeyDown
            Else
                ReprintView()
                AddHandler txtBirthListid.PreviewKeyDown, AddressOf txtBirthListid_PreviewKeyDown
            End If



            pC1ComboSetDisplayMember(cboEvent)
            AddEvent()
            CtrlTab1.SelectedTab = CtrlTab1.TabPages("C1DockingTabPage1")
            CtrlProductImage1.ClearImage()
            PSetDefaultCurrencyOfCashMemoSummary(CtrlCashSummary1)
            PrintSetProperty()

            Dim objclsBirthlistdefaultsetting As New clsDefaultConfiguration("BLS")
            objclsBirthlistdefaultsetting.GetDefaultSettings()
            cboEvent.ValueMember = "EventID"
            cboEvent.DisplayMember = "EventName"
            cboEvent.DataSource = objclsBirthList.LoadEventName()
            pC1ComboSetDisplayMember(cboEvent)
            BirthListCommanLoad()
            Call SetTabSequence()
            Call EnableDiableTenderIcons()
        Catch ex As Exception
            ShowMessage(getValueByKey("BL055"), "BL055 - " & getValueByKey("CLAE04"))
            LogException(ex)
            'MessageBox.Show("Loading problem")
        End Try
        SetCulture(Me, Me.Name, CtrlRbn1)

    End Sub

    Private Sub SetTabSequence()
        Try
            '---- Set Tab Index START
            Call SetFormTabStop(Me, tabStopValue:=False)
            Dim ctrTablIndex As New Dictionary(Of Object, Int16)

            ctrTablIndex.Add(C1Sizer1, 0)
            ctrTablIndex.Add(txtBirthListid, 0)
            ctrTablIndex.Add(btnSearchBirthListID, 1)
            ctrTablIndex.Add(cboEvent, 2)
            ctrTablIndex.Add(c1dateEditEventDate, 3)
            ctrTablIndex.Add(c1dateEditDeliverydate, 4)
            ctrTablIndex.Add(CtrlTextBox1, 5)

            ' ctrTablIndex.Add(Me.tabSalesOrder, 1)
            'ctrTablIndex.Add(Me.TabPageOrderedItems, 1)
            'ctrTablIndex.Add(Me.c1SizerGrid, 1)
            ctrTablIndex.Add(Me.CtrlSalesPerson1, 1)
            ctrTablIndex.Add(Me.CtrlSalesPerson1.CtrlSalesPersons, 0)
            ctrTablIndex.Add(Me.CtrlSalesPerson1.CtrlTxtBox, 1)
            ctrTablIndex.Add(Me.CtrlSalesPerson1.CtrlCmdSearch, 2)

            ctrTablIndex.Add(Me.grdScanItem, 2)

            ctrTablIndex.Add(Me.C1Sizer2, 3)
            ctrTablIndex.Add(Me.btnPurchaseQty, 0)
            ctrTablIndex.Add(Me.btnPickUpQty, 1)
            ctrTablIndex.Add(Me.btnCovertToOpenAmount, 2)
            ctrTablIndex.Add(Me.btnApplyDiscountAndClose1, 3)
            ctrTablIndex.Add(Me.btnReturns, 4)
            ctrTablIndex.Add(Me.BtnReturnSave, 5)
            ctrTablIndex.Add(Me.btnReturnCancel, 6)

            Call SetFormTabIndex(ctrTablIndex:=ctrTablIndex)
            Me.grdScanItem.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.None
            c1SizerGrid.TabStop = False
            C1Sizer1.TabStop = False
            Me.C1Sizer2.TabStop = False
            '---- Set Tab Index END 

        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub


    Private Sub btnPurchaseQty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPurchaseQty.Click
        Try

            grdScanItem.Cols("BalanceItemQty").Visible = True
            grdScanItem.Cols("CurrentPurchasedAmount").Visible = True
            grdScanItem.Cols("TaxAmt").Visible = True
            grdScanItem.Cols("PurchasedQty").Visible = True
            grdScanItem.Cols("PickUpQty").Visible = False
            grdScanItem.Cols("OpenAmountQty").Visible = False
            grdScanItem.Cols("ConvToVoucher").Visible = False
            grdScanItem.Cols("ReservedQty").Visible = True
            If grdScanItem.Cols.Contains("CLPDiscount") Then
                grdScanItem.Cols("CLPPoints").Visible = False
            End If
            If grdScanItem.Cols.Contains("FreezSB") Then
                grdScanItem.Cols("FreezSB").Visible = False
            End If
            If grdScanItem.Cols.Contains("FreezOB") Then
                grdScanItem.Cols("FreezOB").Visible = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub btnPickUpQty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPickUpQty.Click
        Try
            grdScanItem.Cols("BalanceItemQty").Visible = True
            grdScanItem.Cols("CurrentPurchasedAmount").Visible = True
            grdScanItem.Cols("OpenAmountQty").Visible = False
            grdScanItem.Cols("TaxAmt").Visible = True
            grdScanItem.Cols("PurchasedQty").Visible = True
            grdScanItem.Cols("PickUpQty").Visible = True
            grdScanItem.Cols("ConvToVoucher").Visible = False
            If grdScanItem.Cols.Contains("FreezSB") Then
                grdScanItem.Cols("FreezSB").Visible = False
            End If
            If grdScanItem.Cols.Contains("FreezOB") Then
                grdScanItem.Cols("FreezOB").Visible = False
            End If
            grdScanItem.Cols("ReservedQty").Visible = True
            If grdScanItem.Cols.Contains("CLPDiscount") Then
                grdScanItem.Cols("CLPDiscount").Visible = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnCovertToOpenAmount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCovertToOpenAmount.Click
        Try

            grdScanItem.Cols("OpenAmountQty").Visible = True
            grdScanItem.Cols("ConvToVoucher").Visible = True
            grdScanItem.Cols("CurrentPurchasedAmount").Visible = False
            grdScanItem.Cols("TaxAmt").Visible = False
            grdScanItem.Cols("PurchasedQty").Visible = False
            grdScanItem.Cols("PickUpQty").Visible = False
            grdScanItem.Cols("ReservedQty").Visible = True

            grdScanItem.Cols("BalanceItemQty").Visible = False
            If grdScanItem.Cols.Contains("CLPDiscount") Then
                grdScanItem.Cols("CLPDiscount").Visible = False
            End If
            If grdScanItem.Cols.Contains("FreezSB") Then
                grdScanItem.Cols("FreezSB").Visible = False
            End If
            If grdScanItem.Cols.Contains("FreezOB") Then
                grdScanItem.Cols("FreezOB").Visible = False
            End If

        Catch ex As Exception
            LogException(ex)
        End Try


    End Sub

    Private Sub rbnbtnPayCash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim decNeedToPayAmount As Decimal
            'ValidateEditColumnEntry(grdScanItem.Cols(grdScanItem.Col).Name, grdScanItem.Row)
            If Not IsAmountSettle(decNeedToPayAmount) Then
                If ValidateUserInput() Then
                    If (decNeedToPayAmount > Decimal.Zero) Then
                        Dim objPaymentByCash As New frmNAcceptPaymentByCash
                        objPaymentByCash.TotalBillAmount = decNeedToPayAmount
                        objPaymentByCash.ShowDialog()
                        If Not (objPaymentByCash.IsCancelAcceptPayment) Then
                            If Not objPaymentByCash.ReciptTotalAmount Is Nothing And objPaymentByCash.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                                dtPaymentReceipt = objPaymentByCash.ReciptTotalAmount.Tables("MSTRecieptType")
                                If objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeSave Then
                                    isGIftVoucherDocumentPrint = False
                                    GiftReceiptMessage = objPaymentByCash.GiftReceiptMessage
                                    btnSavePrint_Click(sender, e)
                                ElseIf objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeGift Then
                                    isGIftVoucherDocumentPrint = True
                                    GiftReceiptMessage = objPaymentByCash.GiftReceiptMessage
                                    btnSavePrint_Click(sender, e)
                                End If

                            Else
                                ShowMessage(getValueByKey("BL095"), "BL095 - " & getValueByKey("CLAE05"))
                            End If
                        End If
                    Else
                        ShowMessage(getValueByKey("BL025"), "BL025 - " & getValueByKey("CLAE04"))
                        'MessageBox.Show("First you have to purchase the item")
                    End If
                End If
            Else
                ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                'MessageBox.Show("Amount is settled.")

            End If


        Catch ex As Exception
            ShowMessage(getValueByKey("CM033"), "CM033 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in Updating cash payment data ", "Information")
        End Try
    End Sub

    ''' <summary>
    ''' Calculate Total Purchased amount 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function CalculateTotalPurchaseAmt() As Decimal
        Try

            If Not BirthListItemMergeTable Is Nothing Then
                If BirthListItemMergeTable.Rows.Count > 0 Then
                    Dim objTotalAount As Object = BirthListItemMergeTable.Compute("sum(CurrentPurchasedAmount)", "")
                    If Not objTotalAount Is DBNull.Value Then
                        Return CDbl(objTotalAount)
                    Else
                        Return Decimal.Zero
                    End If

                Else
                    Return Decimal.Zero
                End If
            Else
                Return Decimal.Zero
            End If


        Catch ex As Exception
            LogException(ex)
            Return Decimal.Zero
        End Try

    End Function

    Private Sub rbnbtnPayCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim decNeedToPayAmount As Decimal
            'ValidateEditColumnEntry(grdScanItem.Cols(grdScanItem.Col).Name, grdScanItem.Row)
            If Not (IsAmountSettle(decNeedToPayAmount)) Then
                If ValidateUserInput() Then
                    If (decNeedToPayAmount > Decimal.Zero) Then
                        If decNeedToPayAmount > Decimal.Zero Then

                            Dim objPayment As New frmNAcceptPaymentByCard()
                            objPayment.TotalBillAmount = decNeedToPayAmount
                            'objPayment.cboCurrency.SelectedIndex = 1
                            objPayment.ShowDialog()
                            Dim selectedTenderName As String = objPayment.SelectedTenderName
                            Dim strSelectedTenderCode As String = objPayment.CardTenderCode
                            objPayment.Close()
                            If Not (objPayment.IsCancelAcceptPayment) Then
                                If Not objPayment.ReciptTotalAmount Is Nothing And objPayment.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                                    dtPaymentReceipt = objPayment.ReciptTotalAmount.Tables("MSTRecieptType")
                                    If objPayment.Action = My.Resources.AcceptPaymentActionTypeSave Then
                                        isGIftVoucherDocumentPrint = False
                                        btnSavePrint_Click(sender, e)
                                    ElseIf objPayment.Action = My.Resources.AcceptPaymentActionTypeGift Then
                                        isGIftVoucherDocumentPrint = True
                                        GiftReceiptMessage = objPayment.GiftReceiptMessage
                                        btnSavePrint_Click(sender, e)
                                    End If
                                Else
                                    ShowMessage(getValueByKey("BL095"), "BL095 - " & getValueByKey("CLAE05"))
                                End If
                            End If

                            'Dim objPayment As New frmNAcceptPayment()
                            'objPayment.TotalBillAmount = decNeedToPayAmount
                            'objPayment.PaymentBy = "Card"
                            'objPayment.SwipeCard()
                            ''objPayment.cboCurrency.SelectedIndex = 1
                            'objPayment.ShowDialog()
                            'Dim ds As DataSet = objPayment.ReciptTotalAmount()
                            'objPayment.Close()
                            'If Not ds Is Nothing AndAlso Not objPayment.IsCancelAcceptPayment Then
                            '    InsertPaymentTransaction_Manually(decNeedToPayAmount, AcceptPaymentTenderType.PositiveTenderType.CreditCard, AcceptPaymentTenderType.PositiveTenderType.CreditCard)
                            '    If Not (objPayment.IsCancelAcceptPayment) Then
                            '        btnSavePrint_Click(sender, e)
                            '    End If
                            'End If
                        Else
                            ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                            'MessageBox.Show("Amount is settled.")
                        End If
                    Else
                        ShowMessage(getValueByKey("BL025"), "BL025 - " & getValueByKey("CLAE04"))
                        'MessageBox.Show("First you have to purchase the item")
                    End If

                End If
            Else
                ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                'MessageBox.Show("Amount is settled.")
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CM034"), "CM034 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in Updating card payment data ", "Information")
        End Try
    End Sub

    Public Function IsAmountSettle(Optional ByRef decRemainingAmountToPaymed As Decimal = 0.0) As Boolean
        Try
            Dim decTotalPurchasedAmount As Decimal = BirthListItemMergeTable.Compute("sum(CurrentPurchasedAmount)", "")
            If Not dtPaymentReceipt Is Nothing AndAlso dtPaymentReceipt.Rows.Count > 0 Then
                Dim decPayedAmount As Decimal = IIf(dtPaymentReceipt.Compute("sum(Amount)", "RecieptType <> 'OpenAmount' ") Is DBNull.Value, 0, dtPaymentReceipt.Compute("sum(Amount)", "RecieptType <> 'OpenAmount' "))
                Dim NeedToPayAmount As Decimal
                NeedToPayAmount = Decimal.Subtract(decTotalPurchasedAmount, NewOpenAmount)
                If decPayedAmount >= NeedToPayAmount Then
                    Return True
                Else
                    decRemainingAmountToPaymed = Decimal.Subtract(NeedToPayAmount, decPayedAmount)
                    decRemainingAmountToPaymed = MyRound(decRemainingAmountToPaymed, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)
                    Return False
                End If
            Else
                If decTotalPurchasedAmount > Decimal.Zero Then
                    decRemainingAmountToPaymed = Decimal.Subtract(decTotalPurchasedAmount, NewOpenAmount)
                    decRemainingAmountToPaymed = MyRound(decRemainingAmountToPaymed, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)
                    If decRemainingAmountToPaymed < Decimal.Zero Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    decRemainingAmountToPaymed = decTotalPurchasedAmount
                    decRemainingAmountToPaymed = MyRound(decRemainingAmountToPaymed, clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)
                    Return False
                End If



            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Sub rbnbtnPayCheque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim decNeedToPayAmount As Decimal
            'ValidateEditColumnEntry(grdScanItem.Cols(grdScanItem.Col).Name, grdScanItem.Row)
            If Not IsAmountSettle(decNeedToPayAmount) Then
                If ValidateUserInput() Then
                    If decNeedToPayAmount > 0 Then
                        Dim objCheck As New frmNCheckPayment
                        objCheck.BillAmount = decNeedToPayAmount
                        objCheck.ShowDialog()
                        'If objCheck.CheckAmount > 0 Then
                        '    objCheck.Close()
                        '    Dim objPayment As New frmNAcceptPayment()
                        '    objPayment.Show()
                        '    objPayment.TotalBillAmount = decNeedToPayAmount
                        '    objPayment.Enabled = False
                        '    objPayment.cboRecieptType.SelectedValue = "Cheque"
                        '    objPayment.TotalBillAmount = decNeedToPayAmount
                        '    'objPayment.cboCurrency.SelectedIndex = 1
                        '    objPayment.InsertCheque(objCheck.CheckAmount, objCheck.CheckNo, objCheck.CheckDate, objCheck.MicrNo, objCheck.BankName)
                        '    Dim ds As DataSet = objPayment.ReciptTotalAmount()
                        '    objPayment.Close()
                        '    If Not ds Is Nothing Then
                        '        If Not (objPayment.IsCancelAcceptPayment) Then
                        '            InsertPaymentTransaction_Manually(decNeedToPayAmount, "Cheque", "Cheque")
                        '            btnSavePrint_Click(sender, e)
                        '        End If
                        '    End If
                        'End If
                        If objCheck.IsCancelAcceptPayment = False Then
                            If Not objCheck.ReciptTotalAmount Is Nothing And objCheck.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                                Dim ds As DataSet = objCheck.ReciptTotalAmount()
                                objCheck.Close()
                                If Not ds Is Nothing Then
                                    If Not (objCheck.IsCancelAcceptPayment) Then
                                        InsertPaymentTransaction_Manually(decNeedToPayAmount, "Cheque", "Cheque")
                                        If objCheck.Action = My.Resources.AcceptPaymentActionTypeSave Then
                                            isGIftVoucherDocumentPrint = False
                                            GiftReceiptMessage = objCheck.GiftReceiptMessage
                                            btnSavePrint_Click(sender, e)
                                        ElseIf objCheck.Action = My.Resources.AcceptPaymentActionTypeGift Then
                                            isGIftVoucherDocumentPrint = True
                                            GiftReceiptMessage = objCheck.GiftReceiptMessage
                                            btnSavePrint_Click(sender, e)
                                        End If
                                    End If
                                End If
                            Else
                                ShowMessage(getValueByKey("BL095"), "BL095 - " & getValueByKey("CLAE05"))
                            End If
                        End If
                    Else
                        ShowMessage(getValueByKey("BL025"), "BL025 - " & getValueByKey("CLAE04"))
                        'MessageBox.Show("First you have to purchase the item")
                    End If
                End If
            Else
                ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                'MessageBox.Show("Amount is settled.")

            End If




        Catch ex As Exception
            ShowMessage(getValueByKey("CM034"), "CM034 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in Updating card payment data ", "Information")
        End Try
    End Sub

    Private isAllClmShow As Boolean = True

    Private Sub btnShowAllclmGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowAllclmGrid.Click
        Try
            If Not isAllClmShow Then
                ColumnShow()
                isAllClmShow = True
            Else
                ColumnShow()
                isAllClmShow = False
            End If

        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub ColumnShow()
        Try
            grdScanItem.Cols("OpenAmountQty").Visible = isAllClmShow
            grdScanItem.Cols("ConvToVoucher").Visible = isAllClmShow
            grdScanItem.Cols("CurrentPurchasedAmount").Visible = isAllClmShow
            grdScanItem.Cols("TaxAmt").Visible = isAllClmShow
            grdScanItem.Cols("PurchasedQty").Visible = isAllClmShow
            grdScanItem.Cols("PickUpQty").Visible = isAllClmShow
            grdScanItem.Cols("ReservedQty").Visible = True
            grdScanItem.Cols("ReservedQty").AllowEditing = True
            grdScanItem.Cols("BalanceItemQty").Visible = True
            If grdScanItem.Cols.Contains("FreezSB") Then
                grdScanItem.Cols("FreezSB").Visible = False
            End If
            If grdScanItem.Cols.Contains("FreezOB") Then
                grdScanItem.Cols("FreezOB").Visible = False
            End If
            If isAllClmShow Then
                'btnShowAllclmGrid.Text = "&Hide Columns"
                btnShowAllclmGrid.Text = getValueByKey("frmnbirthlistupdate.btnshowallclmgrid1")
            Else
                'btnShowAllclmGrid.Text = "Show &All Columns"
                btnShowAllclmGrid.Text = getValueByKey("frmnbirthlistupdate.btnshowallclmgrid")
            End If
            If grdScanItem.Cols.Contains("CLPDiscount") Then
                grdScanItem.Cols("CLPDiscount").Visible = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub rbnbtnstockCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbnbtnstockCheck.Click
        Try
            Dim objfrmStockCheck As New frmNStockCheck
            objfrmStockCheck.ShowDialog()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub rbnbtnNewBL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbnbtnNewBL.Click
        Me.Close()
        If (isFormClosed) Then
            Dim objfrmBirthListCreate As New frmNBirthListCreate
            MDISpectrum.ShowChildForm(objfrmBirthListCreate, True)
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

    Private Sub cboEvent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboEvent.TextChanged

    End Sub
    Private Sub grdScanItem_MouseEnterCell(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdScanItem.MouseEnterCell
        Try
            If CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).MouseRow = 0 Then
                Dim strColName As String = CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).Cols(CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).MouseCol).Name
                Dim strTooltip As String = ""
                Select Case strColName.ToLower
                    Case "ArticleCode".ToLower
                        'strTooltip = "Item Code"
                        strTooltip = getValueByKey("TT001")
                    Case "Discription".ToLower
                        'strTooltip = "Item Description"
                        strTooltip = getValueByKey("TT002")
                    Case "SellingPrice".ToLower
                        'strTooltip = "Item Price"
                        strTooltip = getValueByKey("TT003")
                    Case "RequstedQty".ToLower
                        'strTooltip = "Ordered Quantity"
                        strTooltip = getValueByKey("TT004")
                    Case "BookedQty".ToLower
                        'strTooltip = "Sold Quantity"
                        strTooltip = getValueByKey("TT005")
                    Case "DeliveredQty".ToLower
                        'strTooltip = "Delivered Quantity"
                        strTooltip = getValueByKey("TT006")
                    Case "ReservedQty".ToLower
                        'strTooltip = "Reserved Quantity"
                        strTooltip = getValueByKey("TT007")
                    Case "BalanceItemQty".ToLower
                        'strTooltip = "Balance Quantity"
                        strTooltip = getValueByKey("TT008")
                    Case "ConvToVoucher".ToLower
                        'strTooltip = "Open Amount Quantity"
                        strTooltip = getValueByKey("TT009")
                    Case "OpenAmountQty".ToLower
                        'strTooltip = "Convert To Open Amount Quantity"
                        strTooltip = getValueByKey("TT010")
                    Case "PurchasedQty".ToLower
                        'strTooltip = "Purchased Quantity"
                        strTooltip = getValueByKey("TT011")
                    Case "PickUpQty".ToLower
                        'strTooltip = "Pickup Quantity"
                        strTooltip = getValueByKey("TT012")
                    Case "TaxAmt".ToLower
                        'strTooltip = "Tax Amount"
                        strTooltip = getValueByKey("TT013")
                    Case "CurrentPurchasedAmount".ToLower
                        'strTooltip = "Current Purchased Amount"
                        strTooltip = getValueByKey("TT014")
                    Case "NetAmount".ToLower
                        'strTooltip = "Net Amount"
                        strTooltip = getValueByKey("TT015")
                    Case "isclp".ToLower
                        'strTooltip = "CLP"
                        strTooltip = getValueByKey("TT016")
                    Case "freetexts".ToLower
                        'strTooltip = "Free Text"
                        strTooltip = getValueByKey("TT017")
                    Case "availableqty".ToLower
                        'strTooltip = "Stock"
                        strTooltip = getValueByKey("TT018")
                End Select
                GridTooltip(C1SuperTooltip1, grdScanItem, strTooltip)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gridGV_MouseEnterCell(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles gridGV.MouseEnterCell
        Try
            Dim strColName As String = ""
            Dim strTooltip As String = ""
            If CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).MouseRow = 0 Then
                strColName = CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).Cols(CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).MouseCol).Name

                Select Case strColName.ToLower
                    Case "SaleInvNumber".ToLower
                        'strTooltip = "Invoice Number"
                        strTooltip = getValueByKey("TT019")
                    Case "CustomerName".ToLower
                        'strTooltip = "Customer Name     "
                        strTooltip = getValueByKey("TT020") & "     "
                    Case "NetAmt".ToLower
                        'strTooltip = "Amount"
                        strTooltip = getValueByKey("TT021")
                    Case "CREATEDON".ToLower
                        'strTooltip = "Date"
                        strTooltip = getValueByKey("TT022")
                End Select
                GridTooltip(C1SuperTooltip1, gridGV, strTooltip)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gridPaymentHistory_MouseEnterCell(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles gridPaymentHistory.MouseEnterCell
        Try
            Dim strColName As String = ""
            Dim strTooltip As String = ""
            If CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).MouseRow = 0 Then
                strColName = CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).Cols(CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).MouseCol).Name
                Select Case strColName.ToLower
                    Case "SaleInvNumber".ToLower
                        'strTooltip = "Invoice Number"
                        strTooltip = getValueByKey("TT019")
                    Case "CustomerName".ToLower
                        'strTooltip = "Customer Name     "
                        strTooltip = getValueByKey("TT020") & "     "
                    Case "PaidAmt".ToLower
                        'strTooltip = "Amount"
                        strTooltip = getValueByKey("TT021")
                    Case "CREATEDON".ToLower
                        'strTooltip = "Date"
                        strTooltip = getValueByKey("TT022")
                End Select
                GridTooltip(C1SuperTooltip1, gridPaymentHistory, strTooltip)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub rbtnSaleBirthList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnSaleBirthList.Click
        Try
            Me.Close()
            If (isFormClosed) Then
                Dim objfrmNBirthListSales As New frmNBirthListSales
                MDISpectrum.ShowChildForm(objfrmNBirthListSales, True)
            End If
        Catch ex As Exception

        End Try

    End Sub


    Private Sub frmNBirthListUpdate_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If BirthListOwnerId <> String.Empty Then
                If e.KeyCode = Keys.F2 Then
                    If grdScanItem.Cols("CurrentReturnQty").Visible = False Then
                        Dim outIRowIndex As Integer = 0
                        Dim outIColumnIndex As Integer = 0
                        grdScanItem.Cols("CurrentPurchasedAmount").Visible = True
                        grdScanItem.Cols("TaxAmt").Visible = True
                        grdScanItem.Cols("PurchasedQty").Visible = True
                        grdScanItem.Cols("PickUpQty").Visible = False
                        grdScanItem.Cols("OpenAmountQty").Visible = False
                        grdScanItem.Cols("ConvToVoucher").Visible = False
                        grdScanItem.Cols("ReservedQty").Visible = True
                        If grdScanItem.Cols.Contains("CLPDiscount") Then
                            grdScanItem.Cols("CLPPoints").Visible = False
                        End If

                        'F2_ChangeSalesQuantity(grdScanItem, "PurchasedQty", "Enter Order Qunatity", outIRowIndex, outIColumnIndex)
                        F2_ChangeSalesQuantity(grdScanItem, "PurchasedQty", getValueByKey("BL106"), outIRowIndex, outIColumnIndex)
                        ValidateEditColumnEntry("PurchasedQty", outIRowIndex)

                    End If
                ElseIf e.KeyCode = Keys.F12 Then
                    If Not BirthListOwnerId = String.Empty Then
                        If clsDefaultConfiguration.PriceChageAllowed Then
                            If Not (POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListStatusColumn) = "Cancel" Or POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListStatusColumn) = "Closed" Or POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BirthListStatusColumn) = "Close-DeliveryP") Then
                                If CheckInterTransactionAuth("PriceChange") Then
                                    If grdScanItem.Cols("CurrentReturnQty").Visible = False Then
                                        Dim outIRowIndex As Integer = 0
                                        Dim outIColumnIndex As Integer = 0
                                        Dim val As Decimal = 0.0
                                        Dim originalVal As Decimal = 0.0


                                        If (BirthListItemMergeTable.Rows(grdScanItem.RowSel - 1)("RequstedQty") <> BirthListItemMergeTable.Rows(grdScanItem.RowSel - 1)("BookedQty")) Then
                                            F2_ChangeSalesQuantity(grdScanItem, "SellingPrice", getValueByKey("SP002"), outIRowIndex, outIColumnIndex, BirthListItemMergeTable)
                                            If CheckExistingPrice(BirthListItemMergeTable.Rows(grdScanItem.RowSel - 1)("SellingPrice"), BirthListItemMergeTable.Rows(grdScanItem.RowSel - 1)("EAN"), BirthListItemMergeTable.Rows(grdScanItem.RowSel - 1)("ArticleCode"), grdScanItem.RowSel - 1) Then
                                                'F2_ChangeSalesQuantity(grdScanItem, "SellingPrice", getValueByKey("SP002"), outIRowIndex, outIColumnIndex)
                                                outIColumnIndex = grdScanItem.Cols("PurchasedQty").Index
                                                btnPurchaseQty_Click(btnPurchaseQty, New System.EventArgs)
                                                If (outIRowIndex <> 0) Then
                                                    val = BirthListItemMergeTable.Rows(outIRowIndex - 1)("SellingPrice")
                                                    If (BirthListItemMergeTable.Rows(outIRowIndex - 1).IsNull("ActualSellingPrice")) Then
                                                        BirthListItemMergeTable.Rows(outIRowIndex - 1)("ActualSellingPrice") = 0
                                                    End If
                                                    originalVal = BirthListItemMergeTable.Rows(outIRowIndex - 1)("ActualSellingPrice")
                                                    If (val <> originalVal) Then
                                                        BirthListItemMergeTable.Rows(outIRowIndex - 1)("IsPriceChangedHere") = True
                                                        BirthListItemMergeTable.Rows(outIRowIndex - 1)("IsPriceChanged") = True
                                                    Else
                                                        BirthListItemMergeTable.Rows(outIRowIndex - 1)("IsPriceChangedHere") = False
                                                    End If
                                                    'Changed by Rohit for Issue No. 6123
                                                    If Not BirthListItemMergeTable.Rows(grdScanItem.RowSel - 1)("PurchasedQty") Is DBNull.Value AndAlso BirthListItemMergeTable.Rows(grdScanItem.RowSel - 1)("PurchasedQty") = 0 Then
                                                        'BirthListItemMergeTable.Rows(grdScanItem.RowSel - 1)("PurchasedQty") = BirthListItemMergeTable.Rows(grdScanItem.RowSel - 1)("BalanceItemQty")
                                                    End If
                                                    'End Change by Rohit

                                                    ValidateEditColumnEntry("PurchasedQty", outIRowIndex)
                                                End If
                                            Else
                                                'MessageBox.Show(getValueByKey("BL108"))
                                                ShowMessage(getValueByKey("BL108"), "BL108 - " & getValueByKey("CLAE04"))
                                            End If
                                        Else
                                            'MessageBox.Show(getValueByKey("BL108"))
                                            ShowMessage(getValueByKey("BL108"), "BL108 - " & getValueByKey("CLAE04"))
                                        End If
                                    End If

                                End If
                            End If
                        Else
                            If (e.KeyCode = Keys.Enter) Then
                                'price change is not allowed '
                                ShowMessage(getValueByKey("BL108"), "BL108 - " & getValueByKey("CLAE04"))
                            End If

                        End If
                    End If
                End If


            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If (PrintBirthListStatus()) Then
            ClearBirthListInformation()
            'AutoLogout(FrmTranCode, Me, lblLoggedIn)
        End If

        txtBirthListid.Enabled = True
    End Sub


    Public Function PrintBirthListStatus(Optional ByVal decTotalDiscountAmount As Decimal = 0, Optional ByVal isSaveBirthListBeforePrint As Boolean = True) As Boolean
        Try
            'Modified by Rohit for translation of Document Status values
            'If lblBirthListStatusCal.Text = "Open" Then
            If lblBirthListStatusCal.Tag = "Open" Then
                'ValidateEditColumnEntry(grdScanItem.Cols(grdScanItem.Col).Name, grdScanItem.Row)
                If isSaveBirthListBeforePrint Then
                    If Not SaveAndPrint(False) Then
                        Return False
                    End If
                End If
            End If
            'Modify End


            Dim strErroMsg As String = ""
            Dim strBirthListStatus As String
            'exec [dbo].[GETBLDATATOPRNT] @SITECODE = '0000588',@BIRTHLISTID = 'BLT4212065'
            'Dim dtBirthListStatus As DataTable = objclsBirthListGlobal.RetrieveQuery("select BI.isclp,  BI.BirthListID ,BH.CustomerType, BD.CustomerID,BI.ArticleCode , BI.EAN , MstArticle.ArticleName as Description, 	BI.RequstedQty,BI.Reservedqty,BD.BookedQty,BD.DeliveredQty,BI.BLDiscountAmt ,	BI.TotalDiscountAmt,case when BH.CustomerType='CLP' then  BD.Rate else SR.sellingprice end as Rate, case when BH.CustomerType='CLP' then  BD.NetAmt else SR.sellingprice * BI.RequstedQty end as NetAmt  ,  SurName +' '+FirstName as CustomerName, BD.CREATEDAT,BD.CREATEDBY,BD.CREATEDON from 	BirthListRequestedItems BI 	left outer  join   	BirthListSalesDtl BD   	on BI.SiteCode=BD.SiteCode	and BI.BirthListId=BD.BirthListId  	and BI.ArticleCode=BD.ArticleCode and BI.EAN=BD.EAN inner join 	MstArticle on BI.ArticleCode =MstArticle.ArticleCode inner join SalesInfoRecord  SR on BI.SiteCode=SR.SiteCode and BI.EAN=SR.EAN and BI.ArticleCode =SR.ArticleCode AND SR.SRNO=1 left outer join  	BirthListSalesHdr BH on  BI.BirthListID=BH.Birthlistid and  BI.siteCode=BH.sitecode 	and BD.CustomerId=BH.CustomerID and BD.SaleInvNumber =BH.SaleInvNumber left outer join  CLPCustomers C on BH.customerid=C.AccountNo where  	(BH.CustomerType='CLP' or BH.CustomerType is null)  and   BI.birthlistid= '" + BirthListID + "'   union select   BI.isclp, BI.BirthListID ,BH.CustomerType, BD.CustomerID,BI.ArticleCode , BI.EAN , MstArticle.ArticleName as Description, BI.RequstedQty,BI.Reservedqty,BD.BookedQty,BD.DeliveredQty,BI.BLDiscountAmt ,BI.TotalDiscountAmt,case when BH.CustomerType='CLP' then  BD.Rate else SR.sellingprice end as Rate, case when BH.CustomerType='CLP' then  BD.NetAmt else SR.sellingprice * BI.RequstedQty  end as NetAmt ,  C.LastName +' '+ FirstName as CustomerName , BD.CREATEDAT,BD.CREATEDBY,BD.CREATEDON  from 	BirthListRequestedItems BI 	left outer  join   	BirthListSalesDtl BD   	on BI.SiteCode=BD.SiteCode	and BI.BirthListId=BD.BirthListId  	and BI.ArticleCode=BD.ArticleCode and BI.EAN=BD.EAN inner join 	MstArticle on BI.ArticleCode =MstArticle.ArticleCode inner join SalesInfoRecord  SR on BI.SiteCode=SR.SiteCode and BI.EAN=SR.EAN and BI.ArticleCode =SR.ArticleCode AND SR.SRNO=1 left outer join  	BirthListSalesHdr BH on  BI.BirthListID=BH.Birthlistid and  BI.siteCode=BH.sitecode 	and BD.CustomerId=BH.CustomerID and BD.SaleInvNumber =BH.SaleInvNumber  left outer join  CustomerSaleOrder C on BH.CustomerID=C.CustomerNo where  (BH.CustomerType='SO' or BH.CustomerType is null)  and  BI.birthlistid= '" + BirthListID + "'  ", strErroMsg)


            Dim dtBirthListStatus As DataTable = objclsBirthListGlobal.RetrieveQuery("exec [GETBLDATATOPRNT] @SITECODE = '" & clsAdmin.SiteCode & "',@BIRTHLISTID =  '" + BirthListID + "' ,@LanguageCode='" + clsAdmin.LangCode + "',@finYear = '" + POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.FinYearColumn) + "'", strErroMsg)

            If decTotalDiscountAmount > Decimal.Zero Then
                strBirthListStatus = "Closed"
            Else
                strBirthListStatus = "Open"
            End If


            If Not dtBirthListStatus Is Nothing AndAlso dtBirthListStatus.Rows.Count > 0 Then
                'Modified by Rohit for translation of Document Status values
                'If lblBirthListStatusCal.Text <> "Open" Then
                If lblBirthListStatusCal.Tag <> "Open" Then
                    strBirthListStatus = lblBirthListStatusCal.Tag.ToString()
                    'Modify End

                    Dim objClpCustomer As New clsCLPCustomer
                    Dim strCustomerType As String
                    If (IsCLPCustomer) Then
                        strCustomerType = "CLP"
                    Else
                        strCustomerType = "SO"
                    End If

                    dtCustomerINformation = objClpCustomer.GetCustomerInformation(strCustomerType, clsAdmin.SiteCode, clsAdmin.CLPProgram, POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.CustomerIdColumn))

                    If dtBirthListStatus.Columns.Contains("BLDiscountAmt") Then

                        Dim objTotalDiscountamount As Object
                        If Not POSDBDataSet.BirthList Is Nothing AndAlso POSDBDataSet.BirthList.Columns.Contains(POSDBDataSet.BirthList.BLDiscountAmtColumn.ColumnName) Then
                            objTotalDiscountamount = POSDBDataSet.BirthList.Rows(0)(POSDBDataSet.BirthList.BLDiscountAmtColumn)
                        End If

                        If Not objTotalDiscountamount Is Nothing AndAlso Not objTotalDiscountamount Is DBNull.Value Then
                            decTotalDiscountAmount = CDbl(objTotalDiscountamount)
                        End If

                    End If

                End If

                Dim EventDateTime As DateTime = Convert.ToDateTime(EventDate)

                Dim dvBLOpen As New DataView(dtBirthListStatus, "Status='Open'", String.Empty, DataViewRowState.CurrentRows)
                If (dvBLOpen.ToTable().Rows.Count = 0) Then
                    ShowMessage(getValueByKey("BL111"), "BL111 - " & getValueByKey("CLAE04"))
                    Return False
                End If


                Dim EventNameValue As String = String.Empty

                If (EventName = "BirthDay") Then
                    EventNameValue = objclsBirthListGlobal.GetEventName(EventName, clsAdmin.SiteCode)
                Else
                    EventNameValue = EventName
                End If

                Dim printingDll As New SpectrumPrint.clsBirthListNew(clsDefaultConfiguration.BLPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, clsBirthListNew.PrintBLTransactionSet.CreateBirthList, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, BirthListID, dtCustomerINformation, dtBirthListStatus, "", "", _dtVoucherInfo, POSDBDataSet.BirthList.Rows(0), dtPaymentReceipt, False, EventDateTime.ToShortDateString(), EventNameValue, "", decTotalDiscountAmount, clsDefaultConfiguration.BillRoundOffAt, dtPrinterInfo, strBirthListStatus, Nothing, clsAdmin.TerminalID, ShowFullName:=clsDefaultConfiguration.PrintItemFullName)
                

            Else
                'ShowMessage("Printing Information not found ", "CustomerSelection")
                ShowMessage(getValueByKey("BL107"), "BL107 - " & getValueByKey("CLAE05"))
            End If


            CtrlProductImage1.BackgroundImage = Nothing
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function


    Private Sub grdScanItem_ValidateEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.ValidateEditEventArgs) Handles grdScanItem.ValidateEdit
        Try
            Dim strModifiedColumnName As String = grdScanItem.Cols(e.Col).Name.ToUpper()
            Select Case strModifiedColumnName
                Case "ReservedQty".ToUpper()
                    If (ValidateQuantity(grdScanItem, e.Col, "ReservedQty")) Then
                        'Changed by Rohit for Issue No.0006126
                        e.Cancel = False
                    End If
                Case "RequstedQty".ToUpper()
                    If (ValidateQuantity(grdScanItem, e.Col, "RequstedQty")) Then
                        e.Cancel = True
                    End If
                Case "CurrentReturnQty".ToUpper()
                    If (ValidateQuantity(grdScanItem, e.Col, "CurrentReturnQty")) Then
                        e.Cancel = True
                    End If
                Case "OpenAmountQty".ToUpper()
                    If (ValidateQuantity(grdScanItem, e.Col, "OpenAmountQty")) Then
                        e.Cancel = True
                    End If
                Case "PurchasedQty".ToUpper()
                    If (ValidateQuantity(grdScanItem, e.Col, "PurchasedQty")) Then
                        e.Cancel = True
                    End If
                Case "PickUpQty".ToUpper()
                    If (ValidateQuantity(grdScanItem, e.Col, "PickUpQty")) Then
                        e.Cancel = True
                    End If
                Case "ISCLP"
                    If CheckInterTransactionAuth("CLP_Req_Change", grdScanItem.DataSource) = False Then
                        e.Cancel = True
                    End If

            End Select

        Catch ex As Exception
            LogException(ex)
            ShowMessage(getValueByKey("CM059"), "CM059 - " & getValueByKey("CLAE05"))

        End Try
    End Sub

    Private Sub SetArticlePrices()
        Dim dtPriceConfig As New DataTable
        Dim obj As New clsBirthListUpdate
        Dim qry As String = String.Empty
        Dim strErrorMsg As String = String.Empty
        qry = String.Format("Select FldValue from DefaultConfig where FldLabel='ShowOriginalBLprice' and SiteCode='{0}'", clsAdmin.SiteCode)
        dtPriceConfig = obj.GetBLPriceConfig(qry, strErrorMsg)

        For Each dr As DataRow In BirthListItemMergeTable.Rows

            If Not IsDBNull(dr("IsPriceChanged")) Then
                If CBool(dr("IsPriceChanged")) Then
                    'dr("SellingPrice") = dr("OriginalSellingPrice")
                Else
                    If (dtPriceConfig.Rows.Count > 0) Then
                        If Not String.IsNullOrEmpty(dtPriceConfig.Rows(0)("FldValue")) Then
                            If CBool(dtPriceConfig.Rows(0)("FldValue")) Then
                                If Not IsDBNull(dr("OriginalSellingPrice")) Then 'this check is needed for old BL records
                                    dr("SellingPrice") = dr("OriginalSellingPrice")
                                End If
                            Else
                                'keep the movex price
                                dr("SellingPrice") = dr("MovexPrice")
                                dr("ActualSellingPrice") = dr("MovexPrice")
                            End If
                        Else
                            'keep the movex price
                            dr("SellingPrice") = dr("MovexPrice")
                            dr("ActualSellingPrice") = dr("MovexPrice")
                        End If
                    Else
                        'keep the movex price
                        dr("SellingPrice") = dr("MovexPrice")
                        dr("ActualSellingPrice") = dr("MovexPrice")
                    End If
                End If
                'dr("IsPriceChanged") = False
            End If
        Next
    End Sub

    Private Function CheckExistingPrice(ByVal val As Decimal, ByVal ean As String, ByVal articlecode As String, ByVal srno As Integer) As Boolean
        Dim returnflg As Boolean = True

        Dim i As Integer = 0


        For i = 0 To BirthListItemMergeTable.Rows.Count - 1
            If (i <> srno) Then
                Dim dr As DataRow = BirthListItemMergeTable.Rows(i)
                If (dr("Ean") = ean) And (dr("ArticleCode") = articlecode) Then
                    If (dr("SellingPrice") = val) Then
                        BirthListItemMergeTable.Rows(srno)("SellingPrice") = BirthListItemMergeTable.Rows(srno)("ActualSellingPrice")
                        returnflg = False
                        Exit For
                    End If
                End If
            End If
        Next

        'For Each dr As C1.Win.C1FlexGrid.Row In grdScanItem.Rows
        '    If dr.Index <> srno Then
        '        If (dr("Ean") = ean) And (dr("ArticleCode") = articlecode) Then
        '            If (dr("SellingPrice") = val) Then
        '                dr("RequestedQty") += BirthListItemMergeTable.Rows(srno)("BalanceItemQty")
        '                DeleteTaxEntries(BirthListItemMergeTable.Rows((srno))("EAN"))
        '                BirthListItemMergeTable.Rows.RemoveAt(srno)
        '            End If
        '        End If
        '    End If
        'Next
        Return returnflg

    End Function


    Private Sub PriceChange()
        If Not BirthListOwnerId = String.Empty Then
            If clsDefaultConfiguration.PriceChageAllowed Then
                If CheckInterTransactionAuth("PriceChange") Then
                    If grdScanItem.Cols("CurrentReturnQty").Visible = False Then
                        Dim outIRowIndex As Integer = 0
                        Dim outIColumnIndex As Integer = 0
                        Dim val As Decimal = 0.0
                        Dim originalVal As Decimal = 0.0

                        If (BirthListItemMergeTable.Rows(grdScanItem.RowSel - 1)("BalanceItemQty") <> 0) Then
                            F2_ChangeSalesQuantity(grdScanItem, "SellingPrice", getValueByKey("SP002"), outIRowIndex, outIColumnIndex, BirthListItemMergeTable)
                            If CheckExistingPrice(BirthListItemMergeTable.Rows(grdScanItem.RowSel - 1)("SellingPrice"), BirthListItemMergeTable.Rows(grdScanItem.RowSel - 1)("EAN"), BirthListItemMergeTable.Rows(grdScanItem.RowSel - 1)("ArticleCode"), grdScanItem.RowSel - 1) Then
                                'F2_ChangeSalesQuantity(grdScanItem, "SellingPrice", getValueByKey("SP002"), outIRowIndex, outIColumnIndex)
                                outIColumnIndex = grdScanItem.Cols("PurchasedQty").Index
                                btnPurchaseQty_Click(btnPurchaseQty, New System.EventArgs)
                                If (outIRowIndex <> 0) Then
                                    val = BirthListItemMergeTable.Rows(outIRowIndex - 1)("SellingPrice")
                                    If (BirthListItemMergeTable.Rows(outIRowIndex - 1).IsNull("ActualSellingPrice")) Then
                                        BirthListItemMergeTable.Rows(outIRowIndex - 1)("ActualSellingPrice") = 0
                                    End If
                                    originalVal = BirthListItemMergeTable.Rows(outIRowIndex - 1)("ActualSellingPrice")
                                    If (val <> originalVal) Then
                                        BirthListItemMergeTable.Rows(outIRowIndex - 1)("IsPriceChangedHere") = True
                                        BirthListItemMergeTable.Rows(outIRowIndex - 1)("IsPriceChanged") = True
                                    Else
                                        BirthListItemMergeTable.Rows(outIRowIndex - 1)("IsPriceChangedHere") = False
                                    End If
                                    'Changed by Rohit for Issue No. 6123
                                    If Not BirthListItemMergeTable.Rows(grdScanItem.RowSel - 1)("PurchasedQty") Is DBNull.Value AndAlso BirthListItemMergeTable.Rows(grdScanItem.RowSel - 1)("PurchasedQty") = 0 Then
                                        'BirthListItemMergeTable.Rows(grdScanItem.RowSel - 1)("PurchasedQty") = BirthListItemMergeTable.Rows(grdScanItem.RowSel - 1)("BalanceItemQty")
                                    End If
                                    'End Change

                                    ValidateEditColumnEntry("PurchasedQty", outIRowIndex)
                                End If
                            Else
                                ShowMessage(getValueByKey("BL108"), "BL108 - " & getValueByKey("CLAE04"))
                                'MessageBox.Show("This price already exists...")
                            End If
                        Else
                            'MessageBox.Show(getValueByKey("BL108"))
                            ShowMessage(getValueByKey("BL108"), "BL108 - " & getValueByKey("CLAE04"))
                        End If
                    End If
                End If
            Else
                'price change is not allowed '
                ShowMessage(getValueByKey("BL108"), "BL108 - " & getValueByKey("CLAE04"))
            End If
        End If
    End Sub
    Private Sub EnableDiableTenderIcons()
        '--- Added by Mahesh for disable credit sale if tender not assign
        Dim DtTender As DataTable = GetTenderInfo(clsAdmin.SiteCode)
        '--- Credit sale 
        'Dim dr() = DtTender.Select("TenderType='" & "Credit" & "'")
        'If dr IsNot Nothing AndAlso dr.Count > 0 Then
        '    IsTenderCredit = True
        'End If
        '----Cash
        Dim dt() = DtTender.Select("TenderType='" & "Cash" & "'")
        If Not (dt IsNot Nothing AndAlso dt.Count > 0) Then
            CtrlRbn1.DbtnPayCash.Enabled = False
        End If
        '----Cheque
        Dim dq() = DtTender.Select("TenderType='" & "Cheque" & "'")
        If Not (dq IsNot Nothing AndAlso dq.Count > 0) Then
            CtrlRbn1.DbtnpayCheque.Enabled = False
        End If
        '----CreditCard
        Dim dw() = DtTender.Select("TenderType='" & "CreditCard" & "'")
        If Not (dw IsNot Nothing AndAlso dw.Count > 0) Then
            CtrlRbn1.DbtnPayCard.Enabled = False
        End If
    End Sub
End Class



