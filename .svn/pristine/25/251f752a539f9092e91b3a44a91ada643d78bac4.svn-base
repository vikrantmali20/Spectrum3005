
Imports System.Resources
Imports System.Globalization
Imports SpectrumBL
Imports SpectrumPrint

Public Class frmNBirthListSales

#Region "Varaibles"
    Private drSelectedCustomer As DataRow
    Private objclsBirthListGlobal As New clsBirthListGobal(TransactionTypes.BirthListSales)
    Dim _dtCustmInfo As DataTable
    Private _drSelectedBirthList As DataRow
    Dim _dtBirthListInfo As DataTable
    Dim drSelectedItemDetails As DataRow
    Dim dtGV As DataTable
    Dim dtPOsVoucher As New POSDBDataSet.dtVoucherPaymentDataTable
    Dim dsReceiptSummart As DataSet
    Dim _IsReprint As Boolean
    Dim _CustomerType As String
    Private dtTempBLSnap As DataTable
    Dim clpvw As New DataView
    Private _dDueDate As Date
    Private _strRemarks As String
    Dim RoundedAmt As Double = 0.0

#End Region
    ''' <summary>
    '''  Is  reprint   
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsReprint() As Boolean
        Get
            Return _IsReprint
        End Get
        Set(ByVal value As Boolean)
            _IsReprint = True
        End Set
    End Property

    Private _prevFinalcialYear As String
    Public Property PreviousFinancialYear As String
        Get
            Return _prevFinalcialYear
        End Get
        Set(ByVal value As String)
            _prevFinalcialYear = value
        End Set
    End Property

    ''' <summary>
    ''' Customer information who is purchasing items in birthlist
    ''' </summary>
    ''' <value>DataTable</value>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Property dtCustmInfo() As DataTable
        Get
            Return CtrlCustDtls1.dtCustmInfo
        End Get
        Set(ByVal value As DataTable)
            _dtCustmInfo = CtrlCustDtls1.dtCustmInfo
        End Set
    End Property

    ''' <summary>
    ''' BirthList owner information
    ''' </summary>
    ''' <value>DataRow</value>
    ''' <returns>DataRow</returns>
    ''' <remarks></remarks>
    Public Property SelectedBirthListInfo() As DataRow
        Get
            Return _drSelectedBirthList
        End Get
        Set(ByVal value As DataRow)
            _drSelectedBirthList = value
        End Set
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
                Dim strCustomerType As String = dtCustmInfo.Rows(0)("CustomerType")
                If strCustomerType = "CLP" Or strCustomerType = "2" Then
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

    ''' <summary>
    '''  Customer Type as Silver ,Gold,...
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private ReadOnly Property SalesCustomerType() As String
        Get
            If Not (dtCustmInfo Is Nothing) Then
                If (dtCustmInfo.Rows.Count > 0) Then
                    Return dtCustmInfo.Rows(0)("CardType").ToString()
                Else
                    Return String.Empty
                End If

            Else
                Return String.Empty
            End If

        End Get

    End Property

    ''' <summary>  
    ''' Selected birthlist items information
    ''' </summary>
    ''' <value>DataTable</value>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Private Property BirthListInfo() As DataTable
        Get
            Return _dtBirthListInfo
        End Get
        Set(ByVal value As DataTable)
            _dtBirthListInfo = value
        End Set
    End Property

    ''' <summary>
    ''' Check for integer values 
    ''' </summary>
    ''' <param name="iValue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckInteger(ByVal iValue As String)
        Try
            Dim iCon As Integer = CInt(iValue)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    '''Check BirthListID textbox is empty or not 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BirthListID() As String
        Get
            Try
                If Not (SelectedBirthListInfo Is Nothing) Then
                    Return SelectedBirthListInfo("BirthListID")
                Else
                    ShowMessage(getValueByKey("BL020"), "BL020 - " & getValueByKey("CLAE04"))
                    CtrlBirthListID.Focus()
                    Return String.Empty
                End If

            Catch ex As Exception
                ShowMessage(getValueByKey("BL020"), "BL020 - " & getValueByKey("CLAE04"))
                CtrlBirthListID.Focus()
                LogException(ex)
                Return String.Empty
            End Try
        End Get
    End Property

    Public Property IsCLPApplicable() As Boolean
        Get

        End Get
        Set(ByVal value As Boolean)

        End Set
    End Property


    ''' <summary>
    ''' Check for customer is selected or not . who purchasing the items in birthlist
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private ReadOnly Property IsPurchaseCustomerSelected() As Boolean
        Get
            Try
                If (Not dtCustmInfo Is Nothing AndAlso Not dtCustmInfo.Rows.Count <= 0) Then
                    Return True
                Else
                    ShowMessage(getValueByKey("BL001"), "BL001 - " & getValueByKey("CLAE04"))
                    'MessageBox.Show("Please Select Customer ")
                    CtrlCustSearch1.CtrlTxtCustNo.Select()
                    Return False
                End If
            Catch ex As Exception
                LogException(ex)
                Return False
            End Try
        End Get

    End Property
    ''' <summary>
    ''' Message for giftVocuher documnet
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
    ''' Class Construtor 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

        If CheckAuthorisation(clsAdmin.UserCode, "BirthListSales") = False Then
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

        Try
            CtrlPayment1.CtrlListPayment.Columns("Amount").NumberFormat = GridAmountColumnCustomeFormat()
        Catch ex As Exception

        End Try
        CtrlRbn1.pInitRbn()
        CtrlCustSearch1.rbCLPMember.Checked = True

        CtrlRbn1.DbtnF12.LargeImage = Global.Spectrum.My.Resources.Resources.price_change
        CtrlRbn1.DbtnF2.LargeImage = Global.Spectrum.My.Resources.Resources.change_qty

        'Try


        '    'Dim resourceMgr As ResourceManager = ResourceManager.CreateFileBasedResourceManager("resource", ResourceFilePath, Nothing)
        '    'Dim ci As New CultureInfo(clsAdmin.CulturInfo)
        '    lblBirthDate.Text = gResourceMgr.GetString("frmBirthListSales_lbl_BirthDate", gCI)
        '    lblBirthListID.Text = gResourceMgr.GetString("frmBirthListSales_lbl_BirthlistID", gCI)
        '    lblCustomerName.Text = gResourceMgr.GetString("frmBirthListSales_lbl_CustomerName", gCI)
        '    lblExpectedDeliDate.Text = gResourceMgr.GetString("frmBirthListSales_lbl_ExpectedDate", gCI)
        '    lblAddress.Text = gResourceMgr.GetString("frmBirthListSales_lbl_CustomerAddress", gCI)
        '    grpBoxCustomerInof.Text = gResourceMgr.GetString("frmBirthListSales_GrpBox_CustomerInfo", gCI)
        '    lblName.Text = gResourceMgr.GetString("frmBirthListSales_lbl_Name", gCI)
        '    lblAddressType.Text = gResourceMgr.GetString("frmBirthListSales_lbl_AddressType", gCI)
        '    lblCustomerAddress.Text = gResourceMgr.GetString("frmBirthListSales_lbl_CustomerAddress", gCI)
        '    lblTelephone.Text = gResourceMgr.GetString("frmBirthListSales_lbl_Telephone", gCI)
        '    btnAcceptPayment.Text = gResourceMgr.GetString("frmBirthListSales_btn_AcceptPayment", gCI)
        '    btnSavePrint.Text = gResourceMgr.GetString("frmBirthListSales_btn_SaveAndPrint", gCI)

        '    btnAdvanceSale.Text = gResourceMgr.GetString("frmBirthListSales_btn_AdvanceSale", gCI)
        '    grpBoxCustomerInof.Text = gResourceMgr.GetString("frmBirthListSales_GrpBox_CustomerInfo", gCI)
        '    grpBoxSummary.Text = gResourceMgr.GetString("frmBirthListSales_GrpBox_Summary", gCI)
        '    lblTotalGrossQty.Text = gResourceMgr.GetString("frmBirthListSales_lbl_Summary_TotalGrossAmount", gCI)
        '    lblTotalPickupQty.Text = gResourceMgr.GetString("frmBirthListSales_lbl_Summary_TotalPickupQty", gCI)
        '    lblTotalPurchasedQty.Text = gResourceMgr.GetString("frmBirthListSales_lbl_Summary_TotalPurchasedQty", gCI)
        '    lblOtherCharges.Text = gResourceMgr.GetString("frmBirthListSales_lbl_Summary_OtherCharges", gCI)
        '    lblNetAmount.Text = gResourceMgr.GetString("frmBirthListSales_lbl_Summary_NetAmount", gCI)


        '    'GridColumn Naming 

        '    grdScanItem.Cols("EAN").Caption = gResourceMgr.GetString("frmBirthListSales_Grid_Column_EAN", gCI)


        '    grdScanItem.Cols("Discription").Caption = gResourceMgr.GetString("frmBirthListSales_Grid_Column_Discription", gCI)

        '    grdScanItem.Cols("Price").Caption = gResourceMgr.GetString("frmBirthListSales_Grid_Column_Price", gCI)

        '    grdScanItem.Cols("requstedqty").Caption = gResourceMgr.GetString("frmBirthListSales_Grid_Column_requstedqty", gCI)

        '    grdScanItem.Cols("BalanceItemQty").Caption = gResourceMgr.GetString("frmBirthListSales_Grid_Column_BalanceItemQty", gCI)


        '    grdScanItem.Cols("PurchasedQty").Caption = gResourceMgr.GetString("frmBirthListSales_Grid_Column_PurchasedQty", gCI)

        '    grdScanItem.Cols("NetAmount").Caption = gResourceMgr.GetString("frmBirthListSales_Grid_Column_NetAmount", gCI)

        '    grdScanItem.Cols("PickUpQty").Caption = gResourceMgr.GetString("frmBirthListSales_Grid_Column_PickUpQty", gCI)

        '    grdScanItem.Cols("AvailableQty").Caption = gResourceMgr.GetString("frmBirthListSales_Grid_Column_BALANCEQTY", gCI)

        'Catch ex As Exception
        '    MessageBox.Show("Problem to load language resource file.", "BirthListSales")
        'End Try
        '' Add any initialization after the InitializeComponent() call.
    End Sub



    ''' <summary>
    ''' Search birthlist by BirthListSearch form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearchBirthListID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchBirthListID.Click, rbtnBirthList.Click
        Try
            If IsReprint = True Then
                Dim objFrmBirthListSearch As New frmNBirthListSearch
                objFrmBirthListSearch.ShowDialog()
                SelectedBirthListInfo = objFrmBirthListSearch.SearchBirthListInformation
                If Not dtReprintBirthListItemDetails Is Nothing Then
                    dtReprintBirthListItemDetails.Clear()
                End If
                ReprintView()

            Else
                Dim objFrmBirthListSearch As New frmNBirthListSearch

                objFrmBirthListSearch.ShowDialog()
                Dim selectedRow As DataRow
                selectedRow = objFrmBirthListSearch.SearchBirthListInformation

                If Not (selectedRow Is Nothing) Then
                    PreviousFinancialYear = selectedRow("FinYear")
                    SelectedBirthListInfo = selectedRow
                    If (SelectedBirthListInfo.Item("BirthListStatus") = "True" Or SelectedBirthListInfo.Item("BirthListStatus") = "Open") Then
                        DisplayBirthListInformation()
                        ClearPaymentReceipt()
                        ClearGVDetails()
                        If Not BirthListInfo Is Nothing Then
                            dtTempBLSnap = BirthListInfo.Copy
                        End If
                    Else
                        Dim strStatus As String = String.Format(getValueByKey("BL102"), SelectedBirthListInfo.Item("BirthListId"), SelectedBirthListInfo.Item("BirthListStatus"))
                        ShowMessage(strStatus, "BL102 - " & getValueByKey("CLAE04"))
                        ClearBirthListInformation()
                        ClearGVDetails()
                        ClearPaymentReceipt()
                        If Not BirthListInfo Is Nothing Then
                            dtTempBLSnap = BirthListInfo.Copy
                        End If
                    End If

                End If
                CtrlSalesPerson1.CtrlTxtBox.Select()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try


    End Sub
    ''' <summary>
    ''' Page 
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks>ReprintView Method </remarks>

    Private Function ReprintView() As Boolean
        Try
            Dim objclsBirthListSales As New clsBirthListSales
            Dim strSiteCode As String = SelectedBirthListInfo.Item("SiteCode")
            Dim strBirthListID As String = SelectedBirthListInfo.Item("BirthListID")
            CtrlBirthListID.Text = strBirthListID
            lblBLCustomerNo.Text = SelectedBirthListInfo.Item("CustomerName")
            Dim strCustomerType As String = SelectedBirthListInfo.Item("CustomerType")
            'lblBlAddress.Text = objclsBirthListSales.GetBLCustomerAddress(SelectedBirthListInfo.Item("CustomerID"), SelectedBirthListInfo.Item("SiteCode"), 1, strCustomerType)
            lblBLEventDate.Text = CStr(SelectedBirthListInfo.Item("DeliveryDate"))

            'lblBLBirthDate.Text = CStr(SelectedBirthListInfo.Item("BirthDate"))
            ReprintLoadInvoiceCombo()
            ReprintGridItem()
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function
    ''' <summary>
    '''  Calculation PaymentTransaction 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>ReprintView Method </remarks>
    Private Function ReprintView_CalculatepaymentSummaray() As Boolean
        Try
            If dtReprintBirthListItemDetails.Rows.Count > 0 Then
                Dim decTotalAmount As Decimal = dtReprintBirthListItemDetails.Compute("sum(NetAmt)", " ")
                CashMemoSummaryDisplay(decTotalAmount, dtReprintBirthListItemDetails.Compute("sum(DeliveredQty)", " "), dtReprintBirthListItemDetails.Compute("sum(BookedQty)", " "))
            Else
                ClearPaymentReceipt()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    Private Function CashMemoSummaryDisplay(Optional ByVal decTotalAmount As Decimal = Decimal.Zero, Optional ByVal iDeliveryQty As Integer = 0, Optional ByVal iBookedQty As Integer = 0) As Boolean
        Try
            Dim strMAount As String = CurrencyFormat(decTotalAmount)
            CtrlCashSummary1.lbltxt1 = strMAount
            CtrlCashSummary1.lbltxt2 = CurrencyFormat(decTotalAmount)
            CtrlCashSummary1.lbltxt3 = iBookedQty
            CtrlCashSummary1.lbltxt4 = iDeliveryQty

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function




    Dim dtReprintBirthListItemDetails As POSDBDataSet.ReprintBirthListSalesDtlDataTable
    ''' <summary>
    '''  Settings for  ItemGrid .
    '''  Grid will operate into ReadOnly mode
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>ReprintView Method </remarks>
    Private Function ReprintGridItem() As Boolean
        Try
            dtReprintBirthListItemDetails = New POSDBDataSet.ReprintBirthListSalesDtlDataTable
            Dim adpBirthListItemDetails As New POSDBDataSetTableAdapters.ReprintBirthListSalesDtlTableAdapter
            'If Not cboInvoiceNo.SelectedValue Is Nothing Then
            '    dtReprintBirthListItemDetails = adpBirthListItemDetails.GetData(clsAdmin.SiteCode, txtCustmCode.Text, BirthListID, cboInvoiceNo.SelectedValue.ToString())
            'Else
            '    dtReprintBirthListItemDetails = adpBirthListItemDetails.GetData(clsAdmin.SiteCode, txtCustmCode.Text, BirthListID, "000")
            'End If

            BirthListInfo = dtReprintBirthListItemDetails
            grdScanItem.DataSource = dtReprintBirthListItemDetails
            grdScanItem.AutoSizeCols()

            ReprintView_CalculatepaymentSummaray()
            ReprintPaymentHistory()
            ReprintVoucherInfot()
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function
    ''' <summary>
    ''' Fill information of Payment Transactions against BirthList 
    ''' </summary>
    ''' <remarks>ReprintView Method </remarks>
    Private Sub ReprintPaymentHistory()
        Try
            Dim dtPosPaymentHistory As New POSDBDataSet.ReprintPaymentDataTable
            Dim adpPosPaymentHistory As New POSDBDataSetTableAdapters.ReprintPaymentTableAdapter
            'dtPosPaymentHistory = adpPosPaymentHistory.GetData(clsAdmin.SiteCode, BirthListID, cboInvoiceNo.SelectedValue.ToString())
            dtPaymentReceipt = dtPosPaymentHistory.Copy()
            'If dtPosPaymentHistory.Rows.Count > 0 Then

            '    grdTotalReciept.DataSource = dtPaymentReceipt
            'Else
            '    dtPosPaymentHistory.Clear()
            '    grdTotalReciept.DataSource = dtPaymentReceipt
            'End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Fill information of voucher against BirthList
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReprintVoucherInfot()
        Try
            'Dim dtPosVoucherInfo As New POSDBDataSet.ReprintVoucherInformationDataTable
            'Dim adpPosVoucherInfo As New POSDBDataSetTableAdapters.ReprintVoucherInformationTableAdapter
            'dtPosVoucherInfo = adpPosVoucherInfo.GetData(clsAdmin.SiteCode, BirthListID, cboInvoiceNo.SelectedValue.ToString())
            'If dtPosVoucherInfo.Rows.Count > 0 Then
            '    Dim decVoucherTotal As Decimal = dtPosVoucherInfo.Compute("sum(NetAmt)", " ")
            '    lblCalVoucherTotal.Text = CurrencyFormat(decVoucherTotal)
            'Else
            '    lblCalVoucherTotal.Text = strZero
            'End If
        Catch ex As Exception
            LogException(ex)
            'lblCalVoucherTotal.Text = strZero
        End Try
    End Sub
    ''' <summary>
    '''  Filling invoice numbers against BirthList
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReprintLoadInvoiceCombo()
        Try
            Dim cbodtBirthListItemDetails As New POSDBDataSet.ReprintLoadInvoiceTableDataTable
            Dim adpBirthListItemDetails As New POSDBDataSetTableAdapters.ReprintLoadInvoiceTableTableAdapter
            'cbodtBirthListItemDetails = adpBirthListItemDetails.GetData(clsAdmin.SiteCode, BirthListID, txtCustmCode.Text)

            If Not cbodtBirthListItemDetails Is Nothing Then
                'cboInvoiceNo.SelectedText = ""
                'cboInvoiceNo.DataSource = cbodtBirthListItemDetails
                'cboInvoiceNo.ValueMember = cbodtBirthListItemDetails.Columns("SaleInvNumber").ColumnName
                'cboInvoiceNo.DisplayMember = cbodtBirthListItemDetails.Columns("SaleInvNumber").ColumnName
            Else
                'cboInvoiceNo.Items.Clear()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    ''' <summary>
    '''  Page display settings for ReprintView
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReprintPageSettings()
        Try
            'lblItem.Visible = False
            'txtItemSearch.Visible = False
            'BtnItemSearch.Visible = False
            'grpReprintView.Visible = True
            'grdScanItem.AllowEditing = False
            'grdScanItem.AllowDelete = False
            'btnAcceptPayment.Enabled = False
            'btnAdvanceSale.Enabled = False
            grdScanItem.Cols("RequstedQty").Name = "BookedQty"
            grdScanItem.Cols("RequstedQty").Caption = "PurchasedQty"
            grdScanItem.Cols("NetAmount").Name = "NetAmt"
            grdScanItem.Cols("TaxAmt").Format = GridAmountColumnCustomeFormat()
            grdScanItem.Cols("PickUpQty").Name = "DeliveredQty"
            grdScanItem.Cols("PurchasedQty").Visible = False
            grdScanItem.Cols("BalanceItemQty").Visible = False
            grdScanItem.Cols("SellingPrice").Format = GridAmountColumnCustomeFormat()
            grdScanItem.Cols("NetAmt").Format = GridAmountColumnCustomeFormat()
            'grdTotalReciept.Cols("RecieptType").Name = "Description"
            'grdTotalReciept.Cols("Amount").Format = GridAmountColumnCustomeFormat()
            'grdTotalReciept.Cols("Amount").Name = "AmountTendered"
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    '''  Display selected birthlist information
    ''' </summary>
    ''' <remarks></remarks>
    Dim dtMainTax As DataTable
    Private Sub DisplayBirthListInformation()
        Try
            Dim objclsBirthListSales As New clsBirthListSales
            Dim strSiteCode As String = SelectedBirthListInfo.Item("SiteCode")
            Dim strBirthListID As String = SelectedBirthListInfo.Item("BirthListID")
            Dim dtBirthListItemDetails As New DataTable
            ' RemoveHandler CtrlBirthListID.KeyDown, AddressOf lblBirthListNo_TextChanged           

            Dim strErrorMsg As String = String.Empty
            Dim strQuery As String
            'strQuery = String.Format("select BirthListRequestedItems.sitecode,BirthListRequestedItems.EAN,BirthListRequestedItems.BirthListId,BirthListRequestedItems.articlecode,BirthListRequestedItems.requstedqty,BirthListRequestedItems.bookedqty,BirthListRequestedItems.DeliveredQty ,Price,MstEAN.Discription,BALANCEQTY from BirthListRequestedItems inner join  MstEAN on BirthListRequestedItems.EAN =MstEAN.EAN inner join PricingCondition on BirthListRequestedItems.EAN =PricingCondition.EAN and BirthListRequestedItems.ArticleCode=PricingCondition.ArticleCode and BirthListRequestedItems.SiteCode = PricingCondition.SiteCode inner join ARTICLESTOCKBALANCES on BirthListRequestedItems.SiteCode=ARTICLESTOCKBALANCES.SiteCode and BirthListRequestedItems.EAN =ARTICLESTOCKBALANCES.EAN  where BirthListRequestedItems.BirthListId='{0}'", strBirthListID)

            'strQuery = String.Format("SELECT A.ArticleCode, H.EAN, A.ArticleName AS DISCRIPTION, D.NodeName AS PRODUCTGROUP, B.SellingPrice, E.BalanceQty, dbo.GETCHAROFARTICLE(A.ArticleCode) AS CHARS, CASE WHEN (A.ISMRPOPEN = 'True') THEN 'Yes' ELSE 'No' END AS ISMRPOPEN,  CASE WHEN (A.ISSUEFREEGIFT = 'True') THEN 'Yes' ELSE 'No' END AS ISSUEFREEGIFT, BirthListRequestedItems.SiteCode,  BirthListRequestedItems.BirthListId,BirthListRequestedItems.Createdon,BirthListRequestedItems.Createdby,BirthListRequestedItems.Createdat,BirthListRequestedItems.UPDATEDBY,BirthListRequestedItems.UPDATEdat,BirthListRequestedItems.UPDATEDON,BirthListRequestedItems.Status, BirthListRequestedItems.RequstedQty, BirthListRequestedItems.BookedQty,BirthListRequestedItems.DeliveredQty, BirthListRequestedItems.ConvToVoucher, BirthListRequestedItems.AuthUserId,BirthListRequestedItems.AuthUserRemarks, B.FromDate, B.ToDate, BirthListRequestedItems.EAN AS Expr1,BirthListRequestedItems.ArticleCode AS Expr2 FROM MstEAN AS H INNER JOIN    MstArticle AS A ON H.ArticleCode = A.ArticleCode INNER JOIN  SalesInfoRecord AS B ON H.EAN = B.EAN INNER JOIN  ArticleNodeMap AS C ON A.ArticleCode = C.ArticleCode INNER JOIN   MstArticleNode AS D ON C.LastNodeCode = D.Nodecode INNER JOIN  ArticleStockBalances AS E ON H.ArticleCode = E.ArticleCode AND H.EAN = E.EAN INNER JOIN     BirthListRequestedItems ON H.EAN = BirthListRequestedItems.EAN  WHERE BirthListRequestedItems.BirthListId='{0}'", strBirthListID)

            'strQuery = String.Format("SELECT A.ArticleCode, A.BASEUNITOFMEASURE  AS UNITOFMEASURE, H.EAN,A.ArticleName AS DISCRIPTION, D.NodeName AS PRODUCTGROUP, B.SellingPrice, E.TotalPhysicalSaleableQty as AvailableQty, dbo.GETCHAROFARTICLE(A.ArticleCode) AS CHARS, CASE WHEN (A.ISMRPOPEN = 'True') THEN 'Yes' ELSE 'No' END AS ISMRPOPEN,  CASE WHEN (A.ISSUEFREEGIFT = 'True') THEN 'Yes' ELSE 'No' END AS ISSUEFREEGIFT,BirthListRequestedItems.SiteCode,E.SiteCode,BirthListRequestedItems.SiteCode,BirthListRequestedItems.BLDiscountAmt,BirthListRequestedItems.ReturnQty,BirthListRequestedItems.ReturnReason,BirthListRequestedItems.ReservedQty,BirthListRequestedItems.BirthListId,BirthListRequestedItems.Createdon,BirthListRequestedItems.Createdby,BirthListRequestedItems.Createdat,BirthListRequestedItems.UPDATEDBY,BirthListRequestedItems.UPDATEdat,BirthListRequestedItems.CLPPoints,BirthListRequestedItems.CLPDiscount,BirthListRequestedItems.UPDATEDON,BirthListRequestedItems.Status, BirthListRequestedItems.RequstedQty, BirthListRequestedItems.BookedQty,BirthListRequestedItems.DeliveredQty, BirthListRequestedItems.ConvToVoucher,BirthListRequestedItems.IsCLP,BirthListRequestedItems.FreeTexts, BirthListRequestedItems.AuthUserId,BirthListRequestedItems.AuthUserRemarks, B.FromDate, B.ToDate, BirthListRequestedItems.EAN AS Expr1,BirthListRequestedItems.ArticleCode AS Expr2,isnull(B.FreezeSB,0) as FreezeSB,isnull(B.FreezeOB,0) as FreezeOB FROM MstEAN AS H INNER JOIN    MstArticle AS A ON H.ArticleCode = A.ArticleCode  INNER JOIN  ArticleNodeMap AS C ON A.ArticleCode = C.ArticleCode INNER JOIN   MstArticleNode AS D ON C.LastNodeCode = D.Nodecode INNER JOIN     BirthListRequestedItems ON H.EAN = BirthListRequestedItems.EAN LEFT OUTER JOIN  SalesInfoRecord AS B ON  BirthListRequestedItems.SiteCode=B.SiteCode and  H.EAN = B.EAN  INNER JOIN  ArticleStockBalances AS E ON H.ArticleCode = E.ArticleCode AND H.EAN = E.EAN and BirthListRequestedItems.SiteCode=E.SiteCode   WHERE BirthListRequestedItems.BirthListId='{0}' and BirthListRequestedItems.SiteCode='{1}' and B.Srno='1'", strBirthListID, clsAdmin.SiteCode)

            'Change by Ashish for CR 5679 - BL Price change
            Dim dtPriceConfig As New DataTable
            Dim dtTemp As New DataTable
            Dim qry As String = String.Empty
            qry = ""
            qry = String.Format("Select FldValue from DefaultConfig where FldLabel='ShowOriginalBLprice' and SiteCode='{0}'", clsAdmin.SiteCode)
            dtPriceConfig = objclsBirthListSales.GetBLPriceConfig(qry, strErrorMsg)

            strQuery = String.Format("SELECT A.ArticleCode, A.BASEUNITOFMEASURE  AS UNITOFMEASURE,H.EAN, ISNULL( Z.ARTICLESHORTNAME,A.ARTICLESHORTNAME) AS  DISCRIPTION, D.NodeName AS PRODUCTGROUP,isnull(BirthListRequestedItems.SellingPrice,B.SellingPrice) as SellingPrice, E.TotalPhysicalSaleableQty as AvailableQty, dbo.GETCHAROFARTICLE(A.ArticleCode) AS CHARS, CASE WHEN (A.ISMRPOPEN = 'True') THEN 'Yes' ELSE 'No' END AS ISMRPOPEN,  CASE WHEN (A.ISSUEFREEGIFT = 'True') THEN 'Yes' ELSE 'No' END AS ISSUEFREEGIFT,BirthListRequestedItems.SiteCode,E.SiteCode,BirthListRequestedItems.SiteCode,BirthListRequestedItems.BLDiscountAmt,BirthListRequestedItems.ReturnQty,BirthListRequestedItems.ReturnReason,BirthListRequestedItems.ReservedQty,BirthListRequestedItems.BirthListId,BirthListRequestedItems.Createdon,BirthListRequestedItems.Createdby,BirthListRequestedItems.Createdat,BirthListRequestedItems.UPDATEDBY,BirthListRequestedItems.UPDATEdat,BirthListRequestedItems.CLPPoints,BirthListRequestedItems.CLPDiscount,BirthListRequestedItems.UPDATEDON,BirthListRequestedItems.Status, BirthListRequestedItems.RequstedQty, BirthListRequestedItems.BookedQty,BirthListRequestedItems.DeliveredQty, BirthListRequestedItems.ConvToVoucher,BirthListRequestedItems.IsCLP as CLPREQUIRE,BirthListRequestedItems.FreeTexts, BirthListRequestedItems.AuthUserId,BirthListRequestedItems.AuthUserRemarks, B.FromDate, B.ToDate, BirthListRequestedItems.EAN AS Expr1,BirthListRequestedItems.ArticleCode AS Expr2,isnull(B.FreezeSB,0) as FreezeSB,isnull(B.FreezeOB,0) as FreezeOB,BirthListRequestedItems.IsPriceChanged, BirthListRequestedItems.OriginalSellingPrice, BirthListRequestedItems.SrNo, 'False' as IsPriceChangedHere, B.SellingPrice as MovexPrice,isnull(BirthListRequestedItems.SellingPrice,B.SellingPrice) as ActualSellingPrice  FROM MstEAN AS H INNER JOIN    MstArticle AS A ON H.ArticleCode = A.ArticleCode  INNER JOIN  ArticleNodeMap AS C ON A.ArticleCode = C.ArticleCode INNER JOIN   MstArticleNode AS D ON C.LastNodeCode = D.Nodecode INNER JOIN     BirthListRequestedItems ON H.EAN = BirthListRequestedItems.EAN LEFT OUTER JOIN  SalesInfoRecord AS B ON  BirthListRequestedItems.SiteCode=B.SiteCode and  H.EAN = B.EAN  INNER JOIN  ArticleStockBalances AS E ON H.ArticleCode = E.ArticleCode AND H.EAN = E.EAN and BirthListRequestedItems.SiteCode=E.SiteCode  LEFT OUTER JOIN ARTICLEDESCINDIFFLANG Z with (nolock) ON BirthListRequestedItems.ARTICLECODE = Z.ARTICLECODE  and  Z.LANGUAGECODE = '{2}'   WHERE BirthListRequestedItems.BirthListId='{0}' and BirthListRequestedItems.SiteCode='{1}' and B.Srno='1' and ((BirthListRequestedItems.SrNo=1 and BirthListRequestedItems.RequstedQty-BirthListRequestedItems.BookedQty >0)or BirthListRequestedItems.RequstedQty-BirthListRequestedItems.BookedQty >0) ", strBirthListID, strSiteCode, clsAdmin.LangCode)
            _dtBirthListInfo = objclsBirthListSales.BirthListInfo(strQuery, strErrorMsg)

            strQuery = String.Format("select distinct A.SiteCode, A.FinYear, A.BirthListId, A.EAN,A.articlecode,A.sellingprice from birthlistrequesteditems A inner join (select SiteCode, FinYear, BirthListId, EAN,articlecode,max(updatedon)as updatedon from birthlistrequesteditems group by SiteCode, FinYear, BirthListId, EAN,articlecode )B on A.SiteCode=B.SiteCode And  A.FinYear=B.FinYear and  A.BirthListId=B.BirthListid And A.EAN=B.EAN and a.updatedon=b.updatedon where A.birthlistid = '{0}' and a.sitecode = '{1}' ", strBirthListID, strSiteCode)
            Dim _dtMaxTable = New DataTable
            _dtMaxTable = objclsBirthListSales.GetBLPriceConfig(strQuery, strErrorMsg)

            If Not _dtBirthListInfo Is Nothing Then
                '  grdScanItem.DataSource = _dtBirthListInfo
            Else
                ShowMessage(getValueByKey("BL098"), "BL098 - " & getValueByKey("CLAE05"))
                ClearBirthListInformation()
                Exit Sub
            End If
            lblBLEventDate.Text = SelectedBirthListInfo.Item("EventDate")

            CtrlBirthListID.Text = strBirthListID
            'AddHandler lblBirthListNo1.TextChanged, AddressOf lblBirthListNo_TextChanged
            ' AddHandler CtrlBirthListID.KeyDown, AddressOf lblBirthListNo_TextChanged

            lblBLCustomerNo.Text = SelectedBirthListInfo.Item("Customerid")
            lblBLCustomerName.Text = SelectedBirthListInfo.Item("CustomerName")

            Dim strCustomerType As String = SelectedBirthListInfo.Item("CustomerType").ToString()
            If Not strCustomerType = "CLP" Then
                'lblBlAddress.Text = objclsBirthListSales.GetBLCustomerAddress(SelectedBirthListInfo.Item("CustomerID"), SelectedBirthListInfo.Item("SiteCode"))
            Else

                'lblBlAddress.Text = objclsBirthListSales.GetBLCustomerAddress(SelectedBirthListInfo.Item("CustomerID"), SelectedBirthListInfo.Item("SiteCode"), 1, "CLP")

            End If


            Dim strEventID As String = SelectedBirthListInfo("EventId")
            lblEventName.Text = objclsBirthListGlobal.GetEventName(strEventID, clsAdmin.SiteCode)

            CalculateTotalBalanceQty()
            Dim iRowIndex As Integer = 0
            For Each dr As DataRow In _dtBirthListInfo.Rows
                dr.AcceptChanges()
                dr.SetModified()
                iRowIndex += 1
            Next

            'For Each dr As DataRow In _dtBirthListInfo.Rows
            '    For Each dr1 As DataRow In _dtMaxTable.Rows
            '        If (dr1("ArticleCode") = dr("ArticleCode")) Then
            '            dr("SellingPrice") = dr1("sellingprice")
            '        End If
            '    Next
            'Next

            Dim str As String = String.Empty
            For Each dr As DataRow In _dtBirthListInfo.Rows
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
            'End of change


            If Not _dtBirthListInfo Is Nothing Then

                grdScanItem.DataSource = _dtBirthListInfo

            Else
                ShowMessage(getValueByKey("BL098"), "BL098 - " & getValueByKey("CLAE05"))
                ClearBirthListInformation()
                Exit Sub
            End If

            Dim objClsComman As New clsCommon
            dtMainTax = objClsComman.getTax(strSiteCode, "", "", 0, "")
            dtMainTax.TableName = "BirthListTax"
            'fnGridColAutoSize(grdScanItem)

            If (clsDefaultConfiguration.BarcodeDisplayAllowed) Then
                grdScanItem.Cols("EAN").Caption = getValueByKey("frmnbirthlistsales.grdscanitem.ean")
                grdScanItem.Cols("EAN").Width = 90
                grdScanItem.Cols("EAN").AllowEditing = False
                grdScanItem.Cols("EAN").Visible = True
            Else
                grdScanItem.Cols("EAN").Visible = False
            End If

            grdScanItem.AutoSizeCols()

        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub DefaultValue_DataTableBirthListInfo()
        Try
            _dtBirthListInfo.Columns("SiteCode").DefaultValue = clsAdmin.SiteCode
            _dtBirthListInfo.Columns("BirthListID").DefaultValue = BirthListID
        Catch ex As Exception

            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Clear previous birthlist information 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearBirthListInformation(Optional ByVal IsClearBirthListID As Boolean = True)
        Try
            If Not BirthListInfo Is Nothing Then
                BirthListInfo.Clear()
            End If

            Dim objclsBirthListSales As New clsBirthListSales
            Dim strSiteCode As String = String.Empty
            Dim strBirthListID As String = String.Empty
            Dim dtBirthListItemDetails As New DataTable
            If (IsClearBirthListID) Then
                CtrlBirthListID.Text = strBirthListID
            End If
            lblBLCustomerName.Text = String.Empty
            lblBLCustomerNo.Text = String.Empty
            CtrlCustSearch1.CtrlTxtCustNo.Text = ""
            'lblBlAddress.Text = String.Empty
            lblBLEventDate.Text = String.Empty
            lblBLEventDate.Text = String.Empty
            lblEventName.Text = String.Empty
            Dim strErrorMsg As String = String.Empty
            Dim strQuery As String
            'strQuery = String.Format("select BirthListRequestedItems.sitecode,BirthListRequestedItems.ReturnQty,BirthListRequestedItems.IsCLP,BirthListRequestedItems.FreeTexts,BirthListRequestedItems.EAN,BirthListRequestedItems.BirthListId,BirthListRequestedItems.articlecode,BirthListRequestedItems.requstedqty,BirthListRequestedItems.bookedqty,BirthListRequestedItems.DeliveredQty ,Salesinforecord.SellingPrice,MstEAN.Discription,TotalPhysicalSaleableQty as AvailableQty from BirthListRequestedItems inner join  MstEAN on BirthListRequestedItems.EAN =MstEAN.EAN inner join  Salesinforecord on BirthListRequestedItems.EAN =Salesinforecord.EAN and BirthListRequestedItems.ArticleCode=Salesinforecord.ArticleCode and BirthListRequestedItems.SiteCode = Salesinforecord.SiteCode inner join ARTICLESTOCKBALANCES on BirthListRequestedItems.SiteCode=ARTICLESTOCKBALANCES.SiteCode and BirthListRequestedItems.EAN =ARTICLESTOCKBALANCES.EAN  where BirthListRequestedItems.BirthListId='{0}' and BirthListRequestedItems.sitecode='{1}'", strBirthListID, strSiteCode)
            ''commented by rama as pricingcondition table not in db
            ''strQuery = String.Format("select BirthListRequestedItems.sitecode,BirthListRequestedItems.ReturnQty,BirthListRequestedItems.IsCLP,BirthListRequestedItems.FreeTexts,BirthListRequestedItems.EAN,BirthListRequestedItems.BirthListId,BirthListRequestedItems.articlecode,BirthListRequestedItems.requstedqty,BirthListRequestedItems.bookedqty,BirthListRequestedItems.DeliveredQty ,Price,MstEAN.Discription,TotalPhysicalSaleableQty as AvailableQty from BirthListRequestedItems inner join  MstEAN on BirthListRequestedItems.EAN =MstEAN.EAN inner join PricingCondition on BirthListRequestedItems.EAN =PricingCondition.EAN and BirthListRequestedItems.ArticleCode=PricingCondition.ArticleCode and BirthListRequestedItems.SiteCode = PricingCondition.SiteCode inner join ARTICLESTOCKBALANCES on BirthListRequestedItems.SiteCode=ARTICLESTOCKBALANCES.SiteCode and BirthListRequestedItems.EAN =ARTICLESTOCKBALANCES.EAN  where BirthListRequestedItems.BirthListId='{0}' and BirthListRequestedItems.sitecode='{1}'", strBirthListID, strSiteCode)
            ''EditDisplayTable()
            '_dtBirthListInfo = objclsBirthListSales.BirthListInfo(strQuery, strErrorMsg)

            If Not BirthListInfo Is Nothing Then
                BirthListInfo.Clear()
            End If
            grdScanItem.DataSource = BirthListInfo
            ClearPaymentReceipt()
            ClearGVDetails()
            If Not dtCustmInfo Is Nothing Then
                dtCustmInfo.Clear()

                If Not CtrlCustDtls1.dtCustmInfo Is Nothing Then
                    CtrlCustDtls1.dtCustmInfo.Clear()
                End If

            End If
            SelectedBirthListInfo = Nothing
            CtrlProductImage1.BackgroundImage = Nothing
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Calculcate Total balance qunatity for each item in grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateTotalBalanceQty(Optional ByVal IsSortbyBalanceQty As Boolean = True)
        Try
            BirthListInfo = objclsBirthListGlobal.CalculateTotalBalanceQty(BirthListInfo)
            CheckBalanceQty(IsSortbyBalanceQty)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Called after row of grid  is edited 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdScanItem_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdScanItem.AfterEdit
        Try
            Dim strColumn As String = grdScanItem.Cols(e.Col).Name
            Dim irowIndex As Integer = e.Row
            Dim icolumnIndex As Integer = e.Col

            CheckColumnName(strColumn, irowIndex, icolumnIndex)


            'CalculateTotalBalanceQty()

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Find Which column value is changed
    ''' </summary>
    ''' <param name="strColumn"></param>
    ''' <param name="iRowIndex"></param>
    ''' <param name="iColumnIndex"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Function CheckColumnName(ByVal strColumn As String, ByVal iRowIndex As Integer, ByVal iColumnIndex As Integer) As Boolean
        Try
            Select Case strColumn
                Case "PurchasedQty"
                    If Not (grdScanItem.Item(iRowIndex, "FreezeSB") Is DBNull.Value) Then
                        If grdScanItem.Rows(iRowIndex)("FreezeSB") = True Then
                            ' Note :FreezeSB stands for sale of an item is not valid . as per rama jena
                            ShowMessage(getValueByKey("BL100"), "BL100 - " & getValueByKey("CLAE04"))
                            grdScanItem.Rows(iRowIndex)("PurchasedQty") = 0
                            EditColumn_NetAmount(iRowIndex, 0)
                            Return False
                        End If
                    Else
                        'MessageBox.Show("Entered PurchaseQty is not valid ", "PurchasedQty")
                        ShowMessage(getValueByKey("BL100"), "BL100 - " & getValueByKey("CLAE04"))
                        grdScanItem.Rows(iRowIndex)("PurchasedQty") = 0
                        EditColumn_NetAmount(iRowIndex, 0)
                        Return False
                    End If


                    If Not (grdScanItem.Item(iRowIndex, iColumnIndex) Is DBNull.Value) Then

                        Dim iValue As Integer = grdScanItem.Item(iRowIndex, iColumnIndex)
                        Dim iBalanceValue As Integer = CalculateAvailabel_PurchaseQty(iRowIndex)
                        If iValue = iBalanceValue Then
                            If (IsAllowToChangePurchaseQty(iRowIndex)) Then
                                If grdScanItem.Item(iRowIndex, "RequstedQty") = (grdScanItem.Item(iRowIndex, "BalanceItemQty") + iValue) Then
                                    grdScanItem.Item(iRowIndex, iColumnIndex) = iValue
                                    EditColumn_NetAmount(iRowIndex, iValue)
                                Else
                                    'ShowMessage(getValueByKey("BL050"), "BL050 - " & getValueByKey("CLAE04"))
                                    'grdScanItem.Item(iRowIndex, iColumnIndex) = 0
                                    SyncRequestedAndPurchaseQty(iValue, iRowIndex)
                                    EditColumn_NetAmount(iRowIndex, iValue)
                                End If
                            Else
                                Dim iRquestedQty = grdScanItem.Item(iRowIndex, "RequstedQty")
                                Dim iBookedQty = grdScanItem.Item(iRowIndex, "BookedQty")
                                Dim iReservedQty As Decimal = BirthListInfo.Rows(iRowIndex - 1)("ReservedQty")
                                Dim purchasedQty = iRquestedQty - (iBookedQty + iReservedQty)
                                If Not grdScanItem.DataSource.Rows(iRowIndex - 1)("IsPriceChanged") Is DBNull.Value Then
                                    If grdScanItem.DataSource.Rows(iRowIndex - 1)("IsPriceChanged") = False And Not (CType(grdScanItem.DataSource, DataTable).Rows(iRowIndex - 1).RowState = DataRowState.Added) Then
                                        ShowMessage(getValueByKey("BL050"), "BL050 - " & getValueByKey("CLAE04"))
                                    End If
                                End If
                                grdScanItem.Item(iRowIndex, "PurchasedQty") = purchasedQty
                                'EditColumn_NetAmount(iRowIndex, purchasedQty)
                            End If
                        ElseIf Not iValue > iBalanceValue AndAlso Not iBalanceValue < 0 Then

                            If (IsAllowToChangePurchaseQty(iRowIndex)) Then
                                If grdScanItem.Item(iRowIndex, "RequstedQty") = (grdScanItem.Item(iRowIndex, "BalanceItemQty") + iValue) Then
                                    grdScanItem.Item(iRowIndex, iColumnIndex) = iValue
                                    EditColumn_NetAmount(iRowIndex, iValue)
                                Else
                                    'ShowMessage(getValueByKey("BL050"), "BL050 - " & getValueByKey("CLAE04"))
                                    'grdScanItem.Item(iRowIndex, iColumnIndex) = 0
                                    grdScanItem.Item(iRowIndex, iColumnIndex) = iValue
                                    SyncRequestedAndPurchaseQty(iValue, iRowIndex)
                                    EditColumn_NetAmount(iRowIndex, iValue)
                                End If
                            Else
                                Dim iRquestedQty = grdScanItem.Item(iRowIndex, "RequstedQty")
                                Dim iBookedQty = grdScanItem.Item(iRowIndex, "BookedQty")
                                Dim iReservedQty As Decimal = BirthListInfo.Rows(iRowIndex - 1)("ReservedQty")
                                Dim purchasedQty = iRquestedQty - (iBookedQty + iReservedQty)
                                If Not grdScanItem.DataSource.Rows(iRowIndex - 1)("IsPriceChanged") Is DBNull.Value Then
                                    If grdScanItem.DataSource.Rows(iRowIndex - 1)("IsPriceChanged") = False And Not (CType(grdScanItem.DataSource, DataTable).Rows(iRowIndex - 1).RowState = DataRowState.Added) Then
                                        ShowMessage(getValueByKey("BL050"), "BL050 - " & getValueByKey("CLAE04"))
                                    End If
                                End If
                                grdScanItem.Item(iRowIndex, "PurchasedQty") = purchasedQty
                                EditColumn_NetAmount(iRowIndex, purchasedQty)
                            End If
                        Else
                            If (IsAllowToChangePurchaseQty(iRowIndex)) Then
                                Dim iPurchasedQty = grdScanItem.Item(iRowIndex, "PurchasedQty")
                                '  Dim strErorMsg As String = String.Format(getValueByKey("BL021"), iBalanceValue)
                                ' ShowMessage(strErorMsg, "BL021 - " & getValueByKey("CLAE04"))
                                'grdScanItem.Item(iRowIndex, "PurchasedQty") = iBalanceValue
                                'EditColumn_NetAmount(iRowIndex, iBalanceValue)
                                grdScanItem.Item(iRowIndex, iColumnIndex) = iValue
                                SyncRequestedAndPurchaseQty(iValue, iRowIndex)
                                EditColumn_NetAmount(iRowIndex, iValue)
                            Else
                                Dim iRquestedQty As Decimal = grdScanItem.Item(iRowIndex, "RequstedQty")
                                Dim iBookedQty As Decimal = grdScanItem.Item(iRowIndex, "BookedQty")
                                Dim iReservedQty As Decimal = BirthListInfo.Rows(iRowIndex - 1)("ReservedQty")
                                Dim purchasedQty = iRquestedQty - (iBookedQty + iReservedQty)
                                grdScanItem.Item(iRowIndex, "PurchasedQty") = purchasedQty
                                If Not grdScanItem.DataSource.Rows(iRowIndex - 1)("IsPriceChanged") Is DBNull.Value Then
                                    If grdScanItem.DataSource.Rows(iRowIndex - 1)("IsPriceChanged") = False And Not (CType(grdScanItem.DataSource, DataTable).Rows(iRowIndex - 1).RowState = DataRowState.Added) Then
                                        ShowMessage(getValueByKey("BL050"), "BL050 - " & getValueByKey("CLAE04"))
                                    End If
                                End If
                                EditColumn_NetAmount(iRowIndex, purchasedQty)
                            End If
                        End If
                    Else
                        If (IsAllowToChangePurchaseQty(iRowIndex)) Then
                            grdScanItem.Item(iRowIndex, iColumnIndex) = 0
                            EditColumn_NetAmount(iRowIndex, 0)
                        Else
                            Dim iRquestedQty = grdScanItem.Item(iRowIndex, "RequstedQty")
                            Dim iBookedQty = grdScanItem.Item(iRowIndex, "BookedQty")
                            Dim iReservedQty As Decimal = BirthListInfo.Rows(iRowIndex - 1)("ReservedQty")
                            Dim purchasedQty = iRquestedQty - (iBookedQty + iReservedQty)
                            ShowMessage(getValueByKey("BL050"), "BL050 - " & getValueByKey("CLAE04"))
                            grdScanItem.Item(iRowIndex, "PurchasedQty") = purchasedQty

                            'EditColumn_NetAmount(iRowIndex, purchasedQty)
                        End If
                    End If
                Case "PickUpQty"
                    If Not (grdScanItem.Item(iRowIndex, iColumnIndex) Is DBNull.Value) Then
                        If Not grdScanItem.Rows(iRowIndex)("FreezeOB") Is Nothing AndAlso Not grdScanItem.Rows(iRowIndex)("FreezeOB") Is DBNull.Value Then
                            If grdScanItem.Rows(iRowIndex)("FreezeOB") = True Then
                                ' Note :FreezeOB stands for delivery of an item is not valid  as per rama jena
                                ShowMessage(getValueByKey("BL100"), "BL100 - " & getValueByKey("CLAE04"))
                                grdScanItem.Item(iRowIndex, iColumnIndex) = 0
                                Return False
                            End If
                        End If
                        'If grdScanItem.Rows(iRowIndex)("FreezeOB") = True Then
                        '    ShowMessage("Item is freezed first released it", "Information")
                        '    grdScanItem.Rows(iRowIndex)("PurchasedQty") = 0
                        '    Return False
                        'End If

                        Dim iValue As Integer = grdScanItem.Item(iRowIndex, iColumnIndex)
                        Dim iPurchasedQty As Integer = grdScanItem.Item(iRowIndex, "PurchasedQty")

                        'Rakesh-01.10.2013:7835-->Check article stock balance quantity
                        If (Not clsDefaultConfiguration.NegativeInventoryAllowed) Then
                            Dim objCommon As New clsCommon
                            Dim articleCode = grdScanItem.Item(iRowIndex, "ArticleCode")
                            Dim articleEAN = grdScanItem.Item(iRowIndex, "EAN")
                            Dim iPickUpQty = grdScanItem.Item(iRowIndex, "PickUpQty")

                            Dim StockQty As Double = objCommon.GetStocks(clsAdmin.SiteCode, articleEAN, articleCode, IIf(iPickUpQty > 0, False, True))

                            If (StockQty < iPickUpQty) Then
                                ShowMessage(String.Format(getValueByKey("SB015"), StockQty), "SB015 - " & getValueByKey("CLAE04"))
                                grdScanItem.Item(iRowIndex, iColumnIndex) = 0
                                Exit Function
                            End If
                        End If

                        If Not (iValue > iPurchasedQty) Then
                            EditColumn_PickUpQty(iRowIndex, iValue)
                        Else
                            Dim strErorMsg As String = String.Format(getValueByKey("BL022"), iPurchasedQty)
                            ShowMessage(strErorMsg, "BL022 - " & getValueByKey("CLAE04"))
                            grdScanItem.Item(iRowIndex, iColumnIndex) = 0
                            EditColumn_PickUpQty(iRowIndex, 0)
                        End If
                    Else
                        grdScanItem.Item(iRowIndex, iColumnIndex) = 0
                    End If


                Case "CLPRequire"
                    If CheckInterTransactionAuth("CLP_Req_Change", grdScanItem.DataSource) = True Then
                        Dim prevValue As String
                        prevValue = BirthListInfo.Rows(iRowIndex - 1)("CLPREQUIRE")
                        'grdScanItem.StartEditing()

                        If prevValue.ToString().ToUpper() = "TRUE" Then
                            BirthListInfo.Rows(iRowIndex - 1).BeginEdit()
                            BirthListInfo.Rows(iRowIndex - 1)("CLPREQUIRE") = False
                            BirthListInfo.Rows(iRowIndex - 1).EndEdit()
                            Dim DataRowState As DataRowState = BirthListInfo.Rows(iRowIndex - 1).RowState
                            grdScanItem.Refresh()
                        Else
                            BirthListInfo.Rows(iRowIndex - 1).BeginEdit()
                            BirthListInfo.Rows(iRowIndex - 1)("CLPREQUIRE") = True
                            BirthListInfo.Rows(iRowIndex - 1).EndEdit()
                            Dim DataRowState As DataRowState = BirthListInfo.Rows(iRowIndex - 1).RowState
                            grdScanItem.Update()

                        End If
                    Else
                        grdScanItem.FinishEditing(True)
                        'e.Cancel = True
                        'dgMainGrid.Rows(e.Row)("CLPRequire") = prevValue
                        ShowMessage("Not Authorised!!", "Validation")
                    End If

            End Select
        Catch ex As Exception
            ShowMessage(getValueByKey("BL023"), "BL023 - " & getValueByKey("CLAE04"))
            LogException(ex)
        End Try
    End Function

    Private Function SyncRequestedAndPurchaseQty(ByVal iValue As Decimal, ByVal iRowIndex As Integer) As Boolean
        Try
            If Not dtTempBLSnap Is Nothing AndAlso dtTempBLSnap.Rows.Count > 0 Then
                Dim drTempRow As DataRow
                Dim rcTempBL() As DataRow
                Dim strCondition As String = "Articlecode = '" & grdScanItem.Rows(iRowIndex).Item("ArticleCode") & "' and Ean = '" & grdScanItem.Rows(iRowIndex).Item("EAN") & "' and SellingPrice = " & ConvertToEnglish(CDbl(clsCommon.CheckIfBlank(grdScanItem.Rows(iRowIndex).Item("SellingPrice"))))
                rcTempBL = dtTempBLSnap.Select(strCondition, "ArticleCode", DataViewRowState.CurrentRows)
                If rcTempBL.Length > 0 Then
                    drTempRow = rcTempBL(0)
                    If grdScanItem.Rows(iRowIndex)("RequstedQty") > drTempRow("RequstedQty") AndAlso iValue <= drTempRow("BalanceItemQty") Then
                        BirthListInfo.Rows(iRowIndex - 1)("RequstedQty") = drTempRow("RequstedQty")
                    ElseIf grdScanItem.Rows(iRowIndex)("RequstedQty") >= drTempRow("RequstedQty") AndAlso iValue > drTempRow("BalanceItemQty") Then
                        BirthListInfo.Rows(iRowIndex - 1)("RequstedQty") = drTempRow("RequstedQty") + (iValue - drTempRow("BalanceItemQty"))
                    End If
                End If
            End If
        Catch ex As Exception
            LogException(ex)
            Throw ex
        End Try
    End Function

    Private Function IsAllowToChangePurchaseQty(ByVal iRowIndex As Integer, Optional ByVal IsDeleteItem As Boolean = False) As Boolean
        Try
            Dim dtGridSource As DataTable = grdScanItem.DataSource

            If (dtGridSource.Rows(iRowIndex - 1).RowState = DataRowState.Added) Then
                Return False
                'Else
                '    Dim iRquestedQty As Integer = grdScanItem.Item(iRowIndex, "RequstedQty")
                '    Dim objPurchaseQty As Object = grdScanItem.Item(iRowIndex, "PurchasedQty")
                '    Dim iPurchaseQty As Integer
                '    If Not objPurchaseQty Is Nothing And Not objPurchaseQty Is DBNull.Value Then
                '        iPurchaseQty = CInt(objPurchaseQty)
                '    End If

                '    If Not IsDeleteItem Then
                '        If (dtGridSource.Rows(iRowIndex - 1)("IsPurchaseQtyUpdate") = False) Then
                '            If (iRquestedQty <= iPurchaseQty) Then
                '                'Return False
                '                Return True
                '            Else
                '                Return True
                '            End If
                '        Else
                '            Return True
                '        End If
                '    Else
                '        Return True
                '    End If
            Else
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            If Not IsDeleteItem Then
                Return False
            Else
                Return True
            End If

        End Try
    End Function
    ''' <summary>
    ''' Calculate Availabel items quantity for purchase
    ''' </summary>
    ''' <param name="iRowIndex">Editing row index</param>
    ''' <returns>Total Availabel purchase quantity</returns>
    ''' <remarks></remarks>
    Private Function CalculateAvailabel_PurchaseQty(ByVal iRowIndex As Integer) As Integer
        Try
            If Not (BirthListInfo.Rows(iRowIndex - 1)("RequstedQty") Is DBNull.Value) Then
                Dim iRequestedQty As Integer = BirthListInfo.Rows(iRowIndex - 1)("RequstedQty")
                Dim iReservedQty As Integer
                If Not (BirthListInfo.Rows(iRowIndex - 1)("ReservedQty") Is DBNull.Value) Then
                    iReservedQty = BirthListInfo.Rows(iRowIndex - 1)("ReservedQty")
                End If
                Dim iBookedQty As Integer
                If Not (BirthListInfo.Rows(iRowIndex - 1)("BookedQty") Is DBNull.Value) Then
                    iBookedQty = BirthListInfo.Rows(iRowIndex - 1)("BookedQty")
                End If
                Dim iDeliveredQty As Integer
                If Not (BirthListInfo.Rows(iRowIndex - 1)("DeliveredQty") Is DBNull.Value) Then
                    iDeliveredQty = BirthListInfo.Rows(iRowIndex - 1)("DeliveredQty")
                End If

                Dim iAvailabelToPurchase As Integer = iRequestedQty - (iBookedQty)
                Return iAvailabelToPurchase
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("BL023"), "BL023 - " & getValueByKey("CLAE04"))
            LogException(ex)
        End Try
    End Function

    ''' <summary>
    ''' Check balance qty and sorting list,changing color
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckBalanceQty(Optional ByVal IsSortbyBalanceQty As Boolean = True) As Boolean
        Try
            If (IsSortbyBalanceQty) Then
                Dim rowStyle As C1.Win.C1FlexGrid.CellStyle
                rowStyle = grdScanItem.Styles.Add("RowBackColor")
                rowStyle.BackColor = Color.FromArgb(234, 242, 251)
                Dim dttemp As DataTable = _dtBirthListInfo.Copy()
                _dtBirthListInfo.Clear()
                If dttemp.Rows.Count > 0 Then
                    For Each dr As DataRow In dttemp.Select("", " BalanceItemQty desc", DataViewRowState.CurrentRows)
                        _dtBirthListInfo.ImportRow(dr)
                    Next
                    'Dim dataView As New DataView(_dtBirthListInfo)
                    'dataView.Sort = " BalanceItemQty desc" ' Don't remove the space
                    '_dtBirthListInfo = dataView.ToTable()
                    grdScanItem.DataSource = _dtBirthListInfo
                    'Dim iDex As Integer = 1
                    'For Each rows As DataRow In BirthListInfo.Rows
                    '    If (rows("BalanceItemQty") = 0) Then
                    '        grdScanItem.Rows(iDex).Style = grdScanItem.Styles("RowBackColor")
                    '    Else
                    '        grdScanItem.Rows(iDex).Style = grdScanItem.Styles.Normal
                    '    End If
                    '    iDex += 1
                    'Next
                    'Dim iDex As Integer = 1

                    For index As Integer = 1 To grdScanItem.Rows.Count - 1
                        If (grdScanItem.Item(index, "BalanceItemQty") = 0) Then
                            grdScanItem.Rows(index).Style = grdScanItem.Styles("RowBackColor")
                        Else
                            grdScanItem.Rows(index).Style = grdScanItem.Styles.Normal
                        End If
                    Next
                    Return True
                Else
                    Return False
                End If
            Else
                CalculateTotalAmount()
                CalculatePaymentReciept()
                grdScanItem.DataSource = BirthListInfo
            End If

        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Calculate NetAmount when purchase quantity is changed
    ''' </summary>
    ''' <param name="iRowIndex"></param>
    ''' <param name="iValue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditColumn_NetAmount(ByVal iRowIndex As Integer, ByVal iValue As Integer)
        Try
            Dim dr As DataRow = _dtBirthListInfo.Rows(iRowIndex - 1)

            BirthListInfo.Rows(iRowIndex - 1).BeginEdit()
            Dim decRate As Decimal = BirthListInfo.Rows(iRowIndex - 1)("SellingPrice")
            Dim irequstedqty As Integer = BirthListInfo.Rows(iRowIndex - 1)("Requstedqty")
            Dim ibookedqty As Integer
            If Not (BirthListInfo.Rows(iRowIndex - 1)("Bookedqty") Is DBNull.Value) Then
                ibookedqty = BirthListInfo.Rows(iRowIndex - 1)("bookedqty")
            End If
            Dim ideliveredqty As Integer
            If Not (BirthListInfo.Rows(iRowIndex - 1)("deliveredqty") Is DBNull.Value) Then
                ideliveredqty = BirthListInfo.Rows(iRowIndex - 1)("deliveredqty")
            End If
            Dim decTaxAmount As Decimal

            If Not (iValue <= BirthListInfo.Rows(iRowIndex - 1)("PickUpQty")) Then
                BirthListInfo.Rows(iRowIndex - 1)("PurchasedQty") = iValue
                Dim iPurchaseQty As Integer = BirthListInfo.Rows(iRowIndex - 1)("PurchasedQty")

                If Not dr Is Nothing Then
                    Dim decTotalAmount = (dr.Item("SellingPrice") * dr.Item("PurchasedQty"))
                    If objclsBirthListGlobal.CreateDataSetForTaxCalculation(dtMainTax, clsAdmin.SiteCode, dr("ArticleCode"), decTotalAmount, dr, dr("EAN").ToString()) Is Nothing Then
                        If Not clsDefaultConfiguration.ArticleTaxAllowed Then
                            BirthListInfo.Rows(iRowIndex - 1)("NetAmount") = Decimal.Zero
                            BirthListInfo.Rows(iRowIndex - 1)("PurchasedQty") = 0
                            BirthListInfo.Rows(iRowIndex - 1)("TaxAmt") = Decimal.Zero
                            BirthListInfo.Rows(iRowIndex - 1)("EXCLUSIVETAX") = Decimal.Zero
                            BirthListInfo.Rows(iRowIndex - 1)("BalanceItemQty") = irequstedqty - (ibookedqty)
                            CalculatePaymentReciept()
                            ShowMessage(getValueByKey("CM019"), "CM019 - " & getValueByKey("CLAE04"))
                            Return False
                        End If
                    End If
                End If
                If Not (BirthListInfo.Rows(iRowIndex - 1)("EXCLUSIVETAX") Is DBNull.Value) Then
                    decTaxAmount = BirthListInfo.Rows(iRowIndex - 1)("EXCLUSIVETAX")
                End If
                BirthListInfo.Rows(iRowIndex - 1)("NetAmount") = (iPurchaseQty * decRate) + decTaxAmount
                BirthListInfo.Rows(iRowIndex - 1)("BalanceItemQty") = irequstedqty - (iPurchaseQty + ibookedqty)
                BirthListInfo.Rows(iRowIndex - 1).EndEdit()
                CalculatePaymentReciept()
            Else
                BirthListInfo.Rows(iRowIndex - 1)("PickUpQty") = 0
                BirthListInfo.Rows(iRowIndex - 1)("PurchasedQty") = iValue
                Dim iPurchaseQty As Integer = BirthListInfo.Rows(iRowIndex - 1)("PurchasedQty")
                Dim iReservedQty As Integer = BirthListInfo.Rows(iRowIndex - 1)("ReservedQty")
                If Not dr Is Nothing Then
                    Dim decTotalAmount = (dr.Item("SellingPrice") * dr.Item("PurchasedQty"))


                    If objclsBirthListGlobal.CreateDataSetForTaxCalculation(dtMainTax, clsAdmin.SiteCode, dr("ArticleCode"), decTotalAmount, dr, dr("EAN").ToString()) Is Nothing Then
                        If Not clsDefaultConfiguration.ArticleTaxAllowed Then
                            BirthListInfo.Rows(iRowIndex - 1)("NetAmount") = Decimal.Zero
                            BirthListInfo.Rows(iRowIndex - 1)("PurchasedQty") = 0
                            BirthListInfo.Rows(iRowIndex - 1)("TaxAmt") = Decimal.Zero
                            BirthListInfo.Rows(iRowIndex - 1)("EXCLUSIVETAX") = Decimal.Zero
                            BirthListInfo.Rows(iRowIndex - 1)("BalanceItemQty") = irequstedqty - (ibookedqty)
                            CalculatePaymentReciept()
                            If iValue > 0 Then
                                ShowMessage(getValueByKey("CM019"), "CM019 - " & getValueByKey("CLAE04"))
                            End If

                            Return False
                        End If
                    End If
                End If
                If Not (BirthListInfo.Rows(iRowIndex - 1)("EXCLUSIVETAX") Is DBNull.Value) Then
                    decTaxAmount = BirthListInfo.Rows(iRowIndex - 1)("EXCLUSIVETAX")
                End If
                BirthListInfo.Rows(iRowIndex - 1)("NetAmount") = (iPurchaseQty * decRate) + decTaxAmount
                BirthListInfo.Rows(iRowIndex - 1)("BalanceItemQty") = irequstedqty - (iPurchaseQty + ibookedqty)
                BirthListInfo.Rows(iRowIndex - 1).EndEdit()
                CalculatePaymentReciept()
            End If

            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Check for pickup quantity 
    ''' </summary>
    ''' <param name="iRowIndex2"></param>
    ''' <param name="iValue"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Function EditColumn_PickUpQty(ByVal iRowIndex2 As Integer, ByVal iValue As Integer) As Boolean
        Try
            Dim irowIndex As Integer = iRowIndex2 - 1
            If (irowIndex = -1) Then
                irowIndex = 0
            End If
            If (iValue > 0) Then
                BirthListInfo.Rows(irowIndex).BeginEdit()
                BirthListInfo.Rows(irowIndex)("PickUpQty") = iValue
                BirthListInfo.Rows(irowIndex).EndEdit()
                CalculatePaymentReciept()
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function


    Dim isFormClosed As Boolean
    Private Sub frmBirthListSales_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try
            If Not BirthListInfo Is Nothing AndAlso BirthListInfo.Rows.Count > 0 Then
                Dim dResult1 As DialogResult = MessageBox.Show(getValueByKey("BL047"), "BL047 - " & getValueByKey("CLAE04"), MessageBoxButtons.YesNo)
                If (dResult1 = Windows.Forms.DialogResult.Yes) Then
                    e.Cancel = False
                    isFormClosed = True
                ElseIf (dResult1 = Windows.Forms.DialogResult.No) Then
                    e.Cancel = True
                    isFormClosed = False
                End If
            Else
                e.Cancel = False
                isFormClosed = True
            End If

        Catch ex As Exception

            LogException(ex)
        End Try
    End Sub



    ''' <summary>
    ''' Called when form load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Event</remarks>
    Private Sub frmBirthListSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            AddHandler CtrlRbn1.DbtnF12.Click, AddressOf PriceChange

            If IsReprint = True Then
                ReprintPageSettings()
            Else
                grdScanItem.Cols("SellingPrice").Format = GridAmountColumnCustomeFormat()
                grdScanItem.Cols("NetAmount").Format = GridAmountColumnCustomeFormat()
            End If

            Dim objclsBirthlistdefaultsetting As New clsDefaultConfiguration("BLS")

            objclsBirthlistdefaultsetting.GetDefaultSettings()

            CtrlProductImage1.CtrlProductImages.Image = Nothing
            PSetDefaultCurrencyOfCashMemoSummary(CtrlCashSummary1)
            AddEvent()
            BirthListCommanLoad()
            CtrlBirthListID.Select()
            Call setTabSequence()
            Call EnableDiableTenderIcons()
        Catch ex As Exception
            LogException(ex)
        End Try
        SetCulture(Me, Me.Name, CtrlRbn1)
    End Sub

    Private Sub setTabSequence()
        Try

            '---- Set Tab Index START
            Call SetFormTabStop(Me, tabStopValue:=False)
            Dim ctrTablIndex As New Dictionary(Of Object, Int16)

            ctrTablIndex.Add(C1Sizer1, 0)
            ctrTablIndex.Add(CtrlBirthListID, 0)
            ctrTablIndex.Add(btnSearchBirthListID, 1)

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

            ctrTablIndex.Add(Me.grdScanItem, 3)

            ctrTablIndex.Add(Me.C1Sizer2, 4)
            ctrTablIndex.Add(Me.CtrlBtnStockCheck, 0)
            ctrTablIndex.Add(Me.btnSaleGV, 1)
            ctrTablIndex.Add(Me.btnSaleCLPPoint, 2)

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


    Private Function AddEvent() As Boolean
        Try
            AddHandler CtrlSalesPerson1.CtrlCmdSearch.Click, AddressOf BtnItemSearch_Click  'Search item on button click 
            AddHandler CtrlSalesPerson1.CtrlTxtBox.KeyDown, AddressOf txtItemSearch_KeyDown ' Search Item on pressing event in items search text box 
            'AddHandler CtrlBirthListID.KeyDown, AddressOf lblBirthListNo_TextChanged
            AddHandler CtrlRbn1.DbtnPayCard.Click, AddressOf rbnbtnPayCard_Click
            AddHandler CtrlRbn1.DbtnPayCash.Click, AddressOf rbnbtnPayCash_Click
            AddHandler CtrlRbn1.DbtnPay.Click, AddressOf btnAcceptPayment_Click
            AddHandler CtrlRbn1.DbtnpayCheque.Click, AddressOf rbnbtnPayCheque_Click
            'CtrlRbn1.pInitRbn()

            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Calculate total payment summary
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Function CalculatePaymentReciept() As Boolean
        Try
            If (BirthListInfo.Rows.Count > 0) Then
                CashMemoSummaryDisplay(CalculateTotalAmount(), BirthListInfo.Compute("sum(PickupQty)", " "), BirthListInfo.Compute("sum(purchasedqty)", " "))
                Return True
            Else
                CashMemoSummaryDisplay(CalculateTotalAmount(), 0, 0)
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function CalculateTotalAmount() As Decimal
        Try

            Dim decBirthListPayment As Decimal
            Dim objBlPayment As Object
            If BirthListInfo.Rows.Count > 0 Then
                objBlPayment = BirthListInfo.Compute("sum(NetAmount)", "NetAmount is not null")
            End If
            If Not objBlPayment Is Nothing And Not objBlPayment Is DBNull.Value Then
                decBirthListPayment = CDbl(objBlPayment)
                RoundedAmt = decBirthListPayment
                decBirthListPayment = MyRound(CDbl(decBirthListPayment), clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)
                RoundedAmt = decBirthListPayment - RoundedAmt
                RoundedAmt = FormatNumber(RoundedAmt, 2)
            End If

            Dim decGVPayment As Decimal
            Dim objGVPayment As Object
            If Not dtVoucherSales Is Nothing Then
                If (dtVoucherSales.Rows.Count > 0) Then
                    objGVPayment = dtVoucherSales.Compute("sum(NetAmount)", " ")
                End If
            End If
            If Not objGVPayment Is Nothing And Not objGVPayment Is DBNull.Value Then
                decGVPayment = MyRound(CDbl(objGVPayment), clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)
            End If
            Dim decTotalPayment As Decimal = Decimal.Add(decBirthListPayment, decGVPayment)
            decTotalPayment = MyRound(CDbl(decTotalPayment), clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)
            Return decTotalPayment
        Catch ex As Exception
            LogException(ex)
            Return Decimal.Zero
        End Try
    End Function

    ''' <summary>
    ''' Clear Payment summary
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearPaymentReceipt()
        CashMemoSummaryDisplay()
        If Not dtPaymentReceipt Is Nothing And dtPaymentReceipt.Rows.Count > 0 Then
            dtPaymentReceipt.Clear()
        End If
    End Sub
    Private Sub ClearGVDetails()
        CtrlCashSummary1.lbltxt5 = String.Empty
        If Not dtVoucherSales Is Nothing Then
            If (dtVoucherSales.Rows.Count > 0) Then
                dtVoucherSales.Clear()
            End If
        End If
        If Not dtGV Is Nothing Then
            If (dtGV.Rows.Count > 0) Then
                dtGV.Clear()
            End If
        End If
    End Sub

    Private Shared isFirst As Boolean = False
    ''' <summary>
    ''' Display selected article image 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdScanItem_RowColChange(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdScanItem.RowColChange
        Try
            Dim objclsComman As New clsCommon
            Dim grid As C1.Win.C1FlexGrid.C1FlexGrid = sender
            Dim strArticleCode As String
            If (isFirst) Then
                If (grid.Row >= 0) Then
                    Dim iTrt As Integer = grid.Row
                    strArticleCode = BirthListInfo.Rows(iTrt - 1)("ArticleCode")
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
    ''' Called when grid delete button pressed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdItemDetails_CellButtonClick(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdScanItem.CellButtonClick
        Try
            Dim iSelectedRow As Int32 = e.Row
            If iSelectedRow >= 0 Then
                If Not (IsAllowToChangePurchaseQty(iSelectedRow, True)) Then
                    Dim objArticleDescription As Object = BirthListInfo.Rows(iSelectedRow - 1)("Discription")
                    Dim resultMsg As DialogResult = MessageBox.Show(getValueByKey("SO011"), "SO011 - " & getValueByKey("CLAE04") + objArticleDescription, MessageBoxButtons.YesNo)
                    If resultMsg = Windows.Forms.DialogResult.Yes Then
                        BirthListInfo.Rows(iSelectedRow - 1).Delete()
                        CalculatePaymentReciept()
                    End If
                Else
                    ShowMessage(getValueByKey("BL092"), "BL092 - " & getValueByKey("CLAE04"))
                End If
            End If


        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private dtPaymentReceipt As New DataTable
    Private dtCheckDtls As New DataTable
    ''' <summary>
    '''  Open Accept payment form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Event</remarks>
    Private Sub btnAcceptPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If clsDefaultConfiguration.IsSalesPersonApplicable = True Then
                If CtrlSalesPerson1.CtrlSalesPersons.Text.ToString = String.Empty Then
                    ShowMessage(getValueByKey("BL097"), "BL097 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
            End If

            Dim decOtherCharges As Decimal = 0.0
            If Not CtrlCustDtls1.dtCustmInfo Is Nothing Then
                If (CtrlCustDtls1.dtCustmInfo.Rows.Count > 0) Then

                    If Not (CtrlCashSummary1.lbl5 = String.Empty) Then
                        If Not CalculateTotalAmount() = Decimal.Zero Then
                            Dim objAcceptPayment As New frmNAcceptPayment
                            Dim objclsBirthListSales As New clsBirthListSales
                            Dim decTotalAmount As Decimal = CalculateTotalAmount()
                            objAcceptPayment.ParentRelation = "BrithList"
                            If (IsCLPCustomer) Then
                                objAcceptPayment.CLPCustomerCardNumber = dtCustmInfo.Rows(0)("CustomerNo")
                            End If
                            If CtrlCustDtls1.lblCustNoValue.Text <> String.Empty Then
                                objAcceptPayment.CLPCustomerCardNumber = CtrlCustDtls1.lblCustNoValue.Text
                            End If
                            objAcceptPayment.TotalBillAmount = FormatNumber(decTotalAmount, 2)
                            objAcceptPayment.PaymentType = clsAcceptPayment.PaymentType.EditBill
                            objAcceptPayment.AcceptEditBillDataSet = dsReceiptSummart
                            objAcceptPayment.ShowDialog(Me)
                            objAcceptPayment.Dispose()
                            objAcceptPayment.Close()
                            If Not (objAcceptPayment.IsCancelAcceptPayment) Then
                                'Added by Rohit for CR5938

                                _dDueDate = objAcceptPayment.dDueDate
                                _strRemarks = objAcceptPayment.strRemarks
                                dsReceiptSummart = objAcceptPayment.ReciptTotalAmount
                            End If
                            If Not dsReceiptSummart Is Nothing Then
                                If (dsReceiptSummart.Tables.Count > 0) And Not (objAcceptPayment.IsCancelAcceptPayment) Then
                                    dtPaymentReceipt = dsReceiptSummart.Tables("MSTRecieptType")
                                    dtCheckDtls = dsReceiptSummart.Tables("CheckDtls")
                                    DisplayTotalRecieptAmount()
                                End If
                                If (objAcceptPayment.ReturnTOCustomer > Decimal.Zero) Then
                                    ShowMessage(getValueByKey("BL024"), "BL024 - " & getValueByKey("CLAE04"), MessageBoxButtons.OKCancel)
                                    'MessageBox.Show("Return {0} amount to customer", "Payment", MessageBoxButtons.OKCancel)
                                End If
                            End If
                            'Dim PaymentVW As New DataView(dtPaymentReceipt, "RecieptTypeCode='CLPPoint'", "", DataViewRowState.CurrentRows)

                            'If PaymentVW.Count > 0 Then
                            '    Dim clp As New CLP_Logic()
                            '    Dim errstr As String
                            '    Dim ReedType As String
                            '    clpvw = clp.CLPRedCalc(CtrlCustSearch1.CtrlTxtCustNo.Text, errstr, ReedType, dsReceiptSummart)
                            '    If Convert.ToDecimal(PaymentVW.ToTable().Compute("Sum(Amount)", "").ToString()) < Convert.ToDecimal(clpvw(0)("redemptionamount")) Then

                            '        Dim comm As New clsCommon
                            '        Dim dt1 As New DataTable
                            '        dt1 = comm.Gettender()
                            '        Dim diff As Decimal = (Convert.ToDecimal(clpvw(0)("redemptionamount")) - Convert.ToDecimal(PaymentVW.ToTable().Compute("Sum(Amount)", "").ToString()))
                            '        PaymentVW.Table.Rows(0)("Amount") = Convert.ToDecimal(PaymentVW.Table.Rows(0)("Amount")) + diff 'Need to improvise
                            '        Dim newrow As DataRow = dtPaymentReceipt.NewRow()
                            '        newrow.Item("Srno") = dtPaymentReceipt.Rows.Count + 1
                            '        newrow.Item("Reciept") = dt1.Rows(0)("tenderheadname")
                            '        newrow.Item("RecieptType") = dt1.Rows(0)("tenderheadcode")
                            '        newrow.Item("Amount") = diff * -1
                            '        newrow.Item("Date") = System.DateTime.Now
                            '        newrow.Item("RecieptTypeCode") = dt1.Rows(0)("tendertype")
                            '        newrow.Item("Exchangerate") = "1"
                            '        newrow.Item("CurrencyCode") = "MAD"

                            '        dtPaymentReceipt.Rows.Add(newrow)
                            '    End If

                            'End If

                            If (objAcceptPayment.Action = "Save") Then
                                GiftReceiptMessage = objAcceptPayment.GiftReceiptMessage
                                isGiftVoucherDocumentPrint = False
                            ElseIf (objAcceptPayment.Action = "Gift") Then
                                GiftReceiptMessage = objAcceptPayment.GiftReceiptMessage
                                isGiftVoucherDocumentPrint = True
                            End If
                            btnSavePrint_Click(sender, e)
                            AutoLogout(FrmTranCode, Me, lblLoggedIn)
                        Else
                            ShowMessage(getValueByKey("BL025"), "BL025 - " & getValueByKey("CLAE04"))
                            CtrlBirthListID.Focus()
                        End If
                    Else
                        ShowMessage(getValueByKey("BL025"), "BL025 - " & getValueByKey("CLAE04"))
                        CtrlBirthListID.Focus()
                    End If
                Else
                    ShowMessage(getValueByKey("BL001"), "BL001 - " & getValueByKey("CLAE04"))
                    CtrlCustDtls1.Focus()
                End If
            Else
                ShowMessage(getValueByKey("BL001"), "BL001 - " & getValueByKey("CLAE04"))
                CtrlCustDtls1.Focus()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    '''  Payment breakup display
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DisplayTotalRecieptAmount()
        Try

            If (dsReceiptSummart.Tables.Count > 0) Then
                CtrlPayment1.CtrlListPayment.DataSource = dsReceiptSummart.Tables("MSTRecieptType")
                CtrlPayment1.CtrlListPayment.Columns("RecieptType").Caption = "Payment Mode"
                CtrlPayment1.CtrlListPayment.Columns("Amount").Caption = "Amount"
                CtrlPayment1.CtrlListPayment.DisplayMember = "RecieptType"
                CtrlPayment1.CtrlListPayment.ValueMember = "Amount"

            End If

            For Each r As C1.Win.C1List.Split In CtrlPayment1.CtrlListPayment.Splits
                Dim i As Integer
                For i = 0 To r.DisplayColumns.Count - 1
                    If r.DisplayColumns(i).DataColumn.DataField.ToUpper() <> "RecieptType".ToUpper() And r.DisplayColumns(i).DataColumn.DataField.ToUpper() <> "Amount".ToUpper() Then
                        r.DisplayColumns(i).Visible = False
                    End If
                Next
            Next
            CtrlPayment1.CtrlListPayment.ExtendRightColumn = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    ''' <summary>
    ''' Saving all sales information into database
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSavePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RibbonButton11.Click, RibbonButton10.Click
        Try
            btnSearchBirthListID.Focus()
            If Not ValidateInput() Then
                Exit Sub
            End If

            Dim dtBirthListOriginal As New DataTable
            If Not _dtBirthListInfo Is Nothing Then
                dtBirthListOriginal = _dtBirthListInfo.Copy()
            End If
            If IsReprint = False Then
                If Not (dtPaymentReceipt Is Nothing) Then
                    Dim decTotalBillAmount As Decimal = CalculateTotalAmount()
                    If (CalculateTotalAmount() > 0) Then
                        If (dtPaymentReceipt.Rows.Count > 0) Then
                            Dim objclsBirthListSalesSave As New clsBirthListSalesSave
                            objclsBirthListSalesSave.FinacialYear = clsAdmin.Financialyear
                            Dim decAmount As Decimal = Math.Round(dtPaymentReceipt.Compute("sum(Amount)", " "), 2)
                            Dim decOtherCharges As Decimal = 0.0
                            If (decAmount >= decTotalBillAmount) Then
                                objclsBirthListSalesSave.SiteCode = clsAdmin.SiteCode
                                objclsBirthListSalesSave.TerminalID = clsAdmin.TerminalID
                                objclsBirthListSalesSave.UserName = clsAdmin.UserName
                                objclsBirthListSalesSave.BirthLisID = BirthListID
                                objclsBirthListSalesSave.DataTableGV = dtGV
                                If (dtCustmInfo.Rows.Count > 0) Then
                                    objclsBirthListSalesSave.SelectedCustomerInfo = dtCustmInfo.Rows(0)
                                    objclsBirthListSalesSave.CustomerID = dtCustmInfo.Rows(0)("CustomerNo")
                                End If
                                objclsBirthListSalesSave.TerminalID = clsAdmin.TerminalID
                                objclsBirthListSalesSave.PaidAmount = FormatNumber(decTotalBillAmount, 2)
                                dtPaymentReceipt.AcceptChanges()
                                objclsBirthListSalesSave.DataTablePaymentHistory = dtPaymentReceipt
                                objclsBirthListSalesSave.DataTableCheckDtls = dtCheckDtls
                                objclsBirthListSalesSave.DataTableVoucherSales = dtVoucherSales
                                objclsBirthListSalesSave.SelectedBirthListInfo = SelectedBirthListInfo
                                objclsBirthListSalesSave.IsPrintHeader = False
                                objclsBirthListSalesSave.IsPrintFooter = False
                                objclsBirthListSalesSave.IsCLPCustomer = IsCLPCustomer
                                objclsBirthListSalesSave.IsCLPCalculate = clsDefaultConfiguration.CLPPointSaleAllowed
                                objclsBirthListSalesSave.StockStorageLocation = clsDefaultConfiguration.StockStorageLocation
                                'objclsBirthListSalesSave.supplier = clsDefaultConfiguration.SupplierCode
                                objclsBirthListSalesSave.DataTableTaxDetails = dtMainTax
                                objclsBirthListSalesSave.DateDayOpen = clsAdmin.DayOpenDate
                                objclsBirthListSalesSave.IsMAP = clsDefaultConfiguration.isMAPbasedCost
                                objclsBirthListSalesSave.CVProgramID = clsAdmin.CVProgram
                                objclsBirthListSalesSave.CreditVoucherVaildDays = clsAdmin.CreditValidDays
                                'If clsDefaultConfiguration.BLIsCLPApplicable = True And IsCLPCustomer = True Then
                                'Removed on guidence of rama for CLP calculation of So customer
                                If clsDefaultConfiguration.CLPPointSaleAllowed = True Then


                                    If IsCLPCustomer = True Then
                                        BirthListInfo.Columns.Add("FirstLevel")
                                        CalculateCLPPoints()
                                        'Resetting last redeemed slab


                                    Else


                                        If SelectedBirthListInfo("Customertype").ToString().ToUpper() = "CLP" Then
                                            BirthListInfo.Columns.Add("FirstLevel")
                                            Dim dr1 As DataRow = Getdetails(SelectedBirthListInfo("customerid"))

                                            objclsBirthListSalesSave.ParentCardNo = SelectedBirthListInfo("customerid")
                                            objclsBirthListSalesSave.ParentCLPProg = dr1("ClpProgramId")
                                            objclsBirthListSalesSave.RelativesPoint = True

                                            CalculateCLPPoints(dr1("CardType"))

                                        End If




                                    End If



                                End If
                                If (AddCLPColumns()) Then
                                    If Not BirthListInfo Is Nothing AndAlso BirthListInfo.Rows.Count > 0 Then
                                        BirthListInfo.Rows(0)("SalesExecutiveCode") = CtrlSalesPerson1.CtrlSalesPersons.SelectedValue
                                    End If

                                End If
                                Dim posdtBirthList As New POSDBDataSet.BirthListDataTable
                                Dim adptorposdtBirthList As New POSDBDataSetTableAdapters.BirthListTableAdapter
                                If Not dtVoucherSales Is Nothing Then
                                    If dtVoucherSales.Rows.Count > 0 Then
                                        posdtBirthList = adptorposdtBirthList.GetDataBySiteCodeandBirthListId(clsAdmin.SiteCode, BirthListID)
                                        Dim decTotalVoucherAmount As Decimal = dtVoucherSales.Compute("sum(NetAmount)", "")
                                        Dim objlastOpenAMount As Object = posdtBirthList.Rows(0)(posdtBirthList.OpenAmountColumn)
                                        Dim decTotalLastOpenAmount As Decimal
                                        Dim decTotalOpenAmount As Decimal
                                        If Not (objlastOpenAMount Is DBNull.Value) Then
                                            decTotalLastOpenAmount = CDbl(objlastOpenAMount)
                                            decTotalOpenAmount = Decimal.Add(decTotalVoucherAmount, decTotalLastOpenAmount)
                                        End If
                                        posdtBirthList.Rows(0)(posdtBirthList.OpenAmountColumn) = decTotalOpenAmount
                                    End If
                                End If
                                objclsBirthListSalesSave.DataTableBirthListItemDetail = BirthListInfo

                                Dim datat As DataSet = objclsBirthListSalesSave.dsBirthListSales
                                Dim strPickUpQty As String = String.Empty
                                Dim IsCLPTransactionSuccess As Boolean = False
                                Dim IsSuceessTransaction As Boolean
                                objclsBirthListSalesSave.dDueDate = _dDueDate
                                objclsBirthListSalesSave.strRemarks = _strRemarks
                                objclsBirthListSalesSave.PreviousFinancialYear = PreviousFinancialYear

                                Dim str As String = objclsBirthListSalesSave.Save(OnlineConnect, RoundedAmt, strPickUpQty, IsSuceessTransaction, Nothing, posdtBirthList, Nothing, adptorposdtBirthList, IsCLPTransactionSuccess, , )
                                If (IsSuceessTransaction) Then


                                    For Each dr As DataRow In BirthListInfo.Rows
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

                                    Dim strOrderNumber As String = String.Empty
                                    Dim strSalesInVoiceNumber As String = String.Empty
                                    'Dim resultPrint As DialogResult = MessageBox.Show(getValueByKey("BL051"), "SalesInvoice:BL051" & objclsBirthListSalesSave.OrderDocumentNumber & "'", MessageBoxButtons.YesNo)
                                    'If resultPrint = Windows.Forms.DialogResult.Yes Then
                                    PrintSalesDetails(objclsBirthListSalesSave.GenNewSaleInVoiceNumber(clsAdmin.TerminalID, OnlineConnect), strOrderNumber, objclsBirthListSalesSave.PaymentGV, clsAdmin.TerminalID)

                                    'End If
                                    'strOrderNumber = String.Format(getValueByKey("BL027"), objclsBirthListSalesSave.OrderDocumentNumber)

                                    If (IsCLPCustomer) Then
                                        If (IsCLPTransactionSuccess) Then

                                            'MessageBox.Show(getValueByKey("BL052"), "BL052 - " & getValueByKey("CLAE04"))
                                        End If
                                    End If
                                    NewBirthListForSale()
                                    strSalesInVoiceNumber = String.Format(getValueByKey("BL026"), objclsBirthListSalesSave.GenNewSaleInVoiceNumber(clsAdmin.TerminalID, OnlineConnect))

                                    Dim strMsg As String = String.Format(getValueByKey("BL029"), strOrderNumber, strSalesInVoiceNumber)
                                    ShowMessage(strMsg, "BL029 - " & getValueByKey("CLAE04"))

                                Else
                                    BirthListInfo = dtBirthListOriginal
                                    grdScanItem.DataSource = BirthListInfo
                                    ShowMessage(getValueByKey("BL016"), "BL016 - " & getValueByKey("CLAE04"))
                                    'MessageBox.Show("BirthList not saved.")
                                End If

                            Else
                                Dim strErrorMsg As String = String.Format(getValueByKey("BL030"), decTotalBillAmount)
                                ShowMessage(strErrorMsg, "BL030 - " & getValueByKey("CLAE04"))
                            End If
                        Else
                            ShowMessage(getValueByKey("BL031"), "BL031 - " & getValueByKey("CLAE04"))
                            'MessageBox.Show("Do the payment against purchase items")
                        End If
                    Else
                        grdScanItem.Focus()
                        ShowMessage(getValueByKey("BL025"), "BL025 - " & getValueByKey("CLAE04"))
                        'MessageBox.Show("First you have to purchase the item")
                    End If

                    'ElseIf (voucherPaidAmount > 0) Then
                    '    Dim objclsBirthListSalesSave As New clsBirthListSalesSave
                    '    objclsBirthListSalesSave.SiteCode = clsAdmin.SiteCode
                    '    objclsBirthListSalesSave.TerminalID = clsAdmin.TerminalID
                    '    objclsBirthListSalesSave.UserName = clsAdmin.UserName
                    '    objclsBirthListSalesSave.BirthLisID = BirthListID
                    '    objclsBirthListSalesSave.DataTableGV = dtGV
                    '    objclsBirthListSalesSave.CustomerID = txtCustmCode.Text
                    '    If (dtCustmInfo.Rows.Count > 0) Then
                    '        objclsBirthListSalesSave.SelectedCustomerInfo = dtCustmInfo.Rows(0)
                    '    End If

                    '    objclsBirthListSalesSave.DataTableVoucherSales = dtVoucherSales
                    '    Dim itemPaidAmount As Decimal
                    '    Dim decTotalPaidAmount As Decimal = Decimal.Add(itemPaidAmount, voucherPaidAmount)
                    '    objclsBirthListSalesSave.PaidAmount = Format(decTotalPaidAmount, "0.00")
                    '    objclsBirthListSalesSave.DataTablePaymentHistory = dtPaymentReceipt
                    '    objclsBirthListSalesSave.DataTableBirthListItemDetail = BirthListInfo
                    '    objclsBirthListSalesSave.SelectedBirthListInfo = SelectedBirthListInfo
                    '    Dim strPickUpQty As String = String.Empty
                    '    Dim str As String = objclsBirthListSalesSave.Save(strPickUpQty)
                    '    If Not (str = String.Empty) Then
                    '        fnPrint(str, "PRN")
                    '    End If
                    '    If Not (strPickUpQty = String.Empty) Then
                    '        fnPrint(strPickUpQty, "PRN")
                    '    End If
                    '    ShowMessage("BirthList Saved.")
                    '    ClearPaymentReceipt()
                    '    NewBirthListForSale()

                Else
                    ShowMessage(getValueByKey("BL025"), "BL025 - " & getValueByKey("CLAE04"))
                End If

            Else
                Dim objclsBirthListSalesSave As New clsBirthListSalesSave
                objclsBirthListSalesSave.FinacialYear = clsAdmin.Financialyear
                objclsBirthListSalesSave.SiteCode = clsAdmin.SiteCode
                objclsBirthListSalesSave.TerminalID = clsAdmin.TerminalID
                objclsBirthListSalesSave.UserName = clsAdmin.UserName
                objclsBirthListSalesSave.BirthLisID = BirthListID
                objclsBirthListSalesSave.DataTableGV = dtGV
                objclsBirthListSalesSave.CustomerID = CtrlCustDtls1.dtCustmInfo.Rows(0)("CustomerNo").Text
                If (dtCustmInfo.Rows.Count > 0) Then
                    objclsBirthListSalesSave.SelectedCustomerInfo = dtCustmInfo.Rows(0)
                End If
                objclsBirthListSalesSave.DataTablePaymentHistory = dtPaymentReceipt
                objclsBirthListSalesSave.DataTableBirthListItemDetail = BirthListInfo
                objclsBirthListSalesSave.DataTableVoucherSales = dtVoucherSales
                objclsBirthListSalesSave.SelectedBirthListInfo = SelectedBirthListInfo
                objclsBirthListSalesSave.IsPrintHeader = False
                objclsBirthListSalesSave.IsPrintFooter = False

                Dim strPickUpQty As String = String.Empty
                Dim str As String = objclsBirthListSalesSave.PrintBirthListPickedQty(True, True)
                Dim strOrderNumber As String = String.Empty
                Dim strSalesInVoiceNumber As String = String.Empty
                If Not (str = String.Empty) Then
                    Dim resultPrint As DialogResult = MessageBox.Show(getValueByKey("BL051"), "BL051 - " & getValueByKey("CLAE04") & objclsBirthListSalesSave.GenNewSaleInVoiceNumber(clsAdmin.TerminalID, OnlineConnect) & "'", MessageBoxButtons.YesNo)
                    If resultPrint = Windows.Forms.DialogResult.Yes Then
                        fnPrint(str, "PRN")
                    End If
                End If
                NewBirthListForSale()
            End If

            If CLP_Data.RedPoint <> 0 Then
                CLP_Data._SlabPoints = 0
            End If

        Catch ex As Exception

            ShowMessage(getValueByKey("BL093"), "BL093 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Function PrintBirthListOld(ByVal IsSuceessTransaction As Boolean, ByVal IsCLPTransactionSuccess As Boolean, ByVal dtBirthListOriginal As DataTable, ByVal objclsBirthListSalesSave As clsBirthListSalesSave) As Boolean

        If (IsSuceessTransaction) Then
            Dim strOrderNumber As String = String.Empty
            Dim strSalesInVoiceNumber As String = String.Empty


            Dim resultPrint As DialogResult = MessageBox.Show(getValueByKey("BL051"), "BL051 - " & getValueByKey("CLAE04") & objclsBirthListSalesSave.OrderDocumentNumber & "'", MessageBoxButtons.YesNo)
            If resultPrint = Windows.Forms.DialogResult.Yes Then

                'PrintSalesDetails(objclsBirthListSalesSave.GenNewSaleInVoiceNumber(clsAdmin.TerminalID, OnlineConnect), strOrderNumber, )
            End If
            strSalesInVoiceNumber = String.Format(getValueByKey("BL026"), objclsBirthListSalesSave.GenNewSaleInVoiceNumber(clsAdmin.TerminalID, OnlineConnect))

            'Dim resultPrint2 As DialogResult = ShowMessage(getValueByKey("BL051"), "ItemDeliveryReceipt:BL051 " & objclsBirthListSalesSave.GenNewSaleInVoiceNumber & "'", MessageBoxButtons.YesNo)
            'If resultPrint2 = Windows.Forms.DialogResult.Yes Then
            '    fnPrint(strPickUpQty, "PRN")
            'End If
            strOrderNumber = String.Format(getValueByKey("BL027"), objclsBirthListSalesSave.OrderDocumentNumber)

            Dim strMsg As String = String.Format(getValueByKey("BL029"), strOrderNumber, strSalesInVoiceNumber)
            ShowMessage(strMsg, "BL029 - " & getValueByKey("CLAE04"))
            If (IsCLPCustomer) Then
                If (IsCLPTransactionSuccess) Then
                    ShowMessage(getValueByKey("BL052"), "BL052 - " & getValueByKey("CLAE04"))
                End If
            End If
            NewBirthListForSale()
        Else
            BirthListInfo = dtBirthListOriginal
            ShowMessage(getValueByKey("BL016"), "BL016 - " & getValueByKey("CLAE04"))
            'MessageBox.Show("BirthList not saved.")
        End If
    End Function

    Private isGiftVoucherDocumentPrint As Boolean



    Private Function PrintSalesDetails(ByVal strInvoiceNumber As String, ByVal strOrderNumber As String, ByVal dtTempGv As DataTable, ByVal terminal As String) As Boolean
        Try
            'Dim objclsPrinting As New PrintBirthList(PrintBirthList.PrintTransactionSet.SaleBirthListItem, dtCustmInfo, _dtBirthListInfo, dtVoucherSales, SelectedBirthListInfo, dtPaymentReceipt, strInvoiceNumber, strOrderNumber, False)

            Dim clsVoucher As Object
            'If (clsDefaultConfiguration.PrintVersion = clsDefaultConfiguration.PrintVersionTypes.NewLayout) Then
            clsVoucher = New clsPrintVoucherNew
            'Else
            '    clsVoucher = New clsPrintVoucher
            'End If

            If Not dtTempGv Is Nothing AndAlso dtTempGv.Rows.Count > 0 Then
                Dim dv As New DataView(dtTempGv, "", "VOURCHERSERIALNBR", DataViewRowState.CurrentRows)
                If dv.Count > 0 Then

                    For Each dr As DataRowView In dv
                        'objPrint.PrintGiftVoucher(dr("VOURCHERSERIALNBR").ToString(), dr("ValueOfVoucher").ToString(), CDate(IIf(dr("ExpiryDate") Is DBNull.Value, Now, dr("ExpiryDate"))), DateDiff(DateInterval.Day, dr("ExpiryDate"), dr("issuedondate")))
                        clsVoucher.PrintGiftVoucherAndCreditNote("BLS", clsAdmin.SiteCode, "GiftVoucher", dr("VOURCHERSERIALNBR"), dr("ValueOfVoucher"), CDate(IIf(dr("ExpiryDate") Is DBNull.Value, Now, dr("ExpiryDate"))), clsAdmin.UserName, BirthListID, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                    Next
                End If
            End If
            For Each dr As DataRow In dtPaymentReceipt.Select("RecieptTypeCode='CreditVouc(I)'", "", DataViewRowState.CurrentRows)
                Dim TotalPay As Decimal = IIf(dr("Amount") > 0, dr("Amount"), dr("Amount") * -1)
                'objPrint.PrintVoucher("CMS", TotalPay, clsDefaultConfiguration.VoucherText, clsAdmin.SiteCode, Errormsg, clsAdmin.CurrencyCode)
                clsVoucher.PrintGiftVoucherAndCreditNote("BLS", clsAdmin.SiteCode, "CreditNote", String.Empty, TotalPay, String.Empty, clsAdmin.UserName, BirthListID, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
            Next

            Dim printingDll As New SpectrumPrint.clsBirthListNew(clsDefaultConfiguration.BLPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, clsBirthListNew.PrintBLTransactionSet.SaleBirthListItem, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, BirthListID, CtrlCustDtls1.dtCustmInfo, BirthListInfo, strInvoiceNumber, strOrderNumber, dtVoucherSales, SelectedBirthListInfo, dtPaymentReceipt, False, lblBLEventDate.Text, lblEventName.Text, "", 0, clsDefaultConfiguration.BillRoundOffAt, dtPrinterInfo, "", dtMainTax, clsAdmin.TerminalID, ShowFullName:=clsDefaultConfiguration.PrintItemFullName)
            

            'If Not dtPaymentReceipt Is Nothing AndAlso dtPaymentReceipt.Rows.Count > 0 Then
            '    Dim TotalPay As Decimal
            '    Dim dateCV As Date
            '    For Each dr As DataRow In dtPaymentReceipt.Select("RecieptTypeCode='CreditVouc(I)'", "", DataViewRowState.CurrentRows)
            '        TotalPay = IIf(dr("Amount") > 0, dr("Amount"), dr("Amount") * -1)
            '        dateCV = dr("Date")
            '        objclsPrinting.PrintVoucher(dateCV, "CMS", BirthListID, clsAdmin.UserCode, CDbl(TotalPay))
            '    Next
            'End If
            If isGiftVoucherDocumentPrint Then
                Dim objPrintDll2 As New SpectrumPrint.clsBirthListNew(clsDefaultConfiguration.BLPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired, clsBirthListNew.PrintBLTransactionSet.GiftVoucher, clsAdmin.SiteCode, clsAdmin.CurrencyCode, clsAdmin.UserName, BirthListID, CtrlCustDtls1.dtCustmInfo, BirthListInfo, strInvoiceNumber, strOrderNumber, dtVoucherSales, SelectedBirthListInfo, dtPaymentReceipt, False, lblBLEventDate.Text, lblEventName.Text, GiftReceiptMessage, 0, clsDefaultConfiguration.BillRoundOffAt, dtPrinterInfo, "", Nothing, clsAdmin.TerminalID, ShowFullName:=clsDefaultConfiguration.PrintItemFullName)
                 

                isGiftVoucherDocumentPrint = False
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    ''' <summary>
    ''' Clear All the Current information. 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub NewBirthListForSale()
        Try
            'RemoveHandler txtCustmCode.TextChanged, AddressOf txtCustmCode_PreviewKeyDown
            If Not CtrlCustDtls1.dtCustmInfo Is Nothing Then
                CtrlCustDtls1.dtCustmInfo.Clear()
            End If
            'AddHandler txtCustmCode.TextChanged, AddressOf txtCustmCode_PreviewKeyDown
            ClearBirthListInformation()
            ClearPaymentReceipt()
            ClearGVDetails()
            If (IsReprint) Then
                dtPaymentReceipt.Clear()
            End If
            If Not dtCheckDtls Is Nothing Then
                dtCheckDtls.Clear()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    '''  Manually search birthlist id 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub lblBirthListNo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If (e.KeyCode = Keys.Enter) Then
            Try
                Dim dtBirth As New DataTable

                Dim errorMsg As String = String.Empty
                If (errorMsg = String.Empty) Then
                    dtBirth = objclsBirthList.RetrieveQuery("select BirthList.BirthListId,BirthList.SiteCode,BirthList.Customertype,BirthList.customerid,BirthList.DeliveryDate,BirthList.EventDate,BirthList.BirthDate,CustomerSaleOrder.FirstName,CustomerSaleOrder.LastName,CustomerSaleOrder.CustomerName,sum(BirthListRequestedItems.RequstedQty) as RequstedQty,sum(BirthListRequestedItems.BookedQty ) as BookedQty,sum(BirthListRequestedItems.DeliveredQty)  as DeliveredQty  from BirthList inner join BirthListRequestedItems on BirthList.BirthListId = BirthListRequestedItems.BirthListId and BirthList.SiteCode=BirthListRequestedItems.SiteCode inner join CustomerSaleOrder on BirthList.customerid=CustomerSaleOrder.CustomerNo where BirthList.BirthListId='" & CtrlBirthListID.Text & "' AND BirthList.SiteCode = '" & clsAdmin.SiteCode & "'  group by BirthList.BirthListId,BirthList.SiteCode,BirthList.customerid,BirthList.DeliveryDate, BirthList.EventDate, BirthList.BirthDate, CustomerSaleOrder.FirstName, CustomerSaleOrder.LastName,CustomerSaleOrder.CustomerName ", errorMsg)
                    If (dtBirth.Rows.Count = 0) Then
                        ClearBirthListInformation()
                        ShowMessage(getValueByKey("BL032"), "BL032 - " & getValueByKey("CLAE04"))
                        'MessageBox.Show("BirthListID not Found")
                        CtrlBirthListID.Focus()
                    Else
                        SelectedBirthListInfo = dtBirth.Rows(0)
                        DisplayBirthListInformation()
                    End If
                Else
                    ShowMessage(getValueByKey("DBCON01"), "DBCON01 - " & getValueByKey("CLAE04"))
                    'MessageBox.Show("Connection Closed", "BirthListID Search")
                End If
            Catch ex As Exception
                LogException(ex)
            End Try
        End If

    End Sub

    ''' <summary>
    '''  Manually search birthlist id .
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub lblBirthListNo_TextChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Enter Then


                Dim dtBirth As New DataTable

                Dim errorMsg As String = String.Empty
                If (errorMsg = String.Empty AndAlso Not CtrlBirthListID.Text = String.Empty) Then

                    Dim strQuery As String = String.Format("select BirthList.BirthListId,BirthList.SiteCode,BirthList.CustomerType,BirthList.customerid,BirthList.DeliveryDate,BirthList.EventId,BirthList.EventDate,BirthList.BirthListStatus,BirthList.BirthDate,CustomerSaleOrder.FirstName,CustomerSaleOrder.LastName,CustomerSaleOrder.CustomerName,sum(BirthListRequestedItems.RequstedQty) as RequstedQty,sum(BirthListRequestedItems.BookedQty ) as BookedQty,sum(BirthListRequestedItems.DeliveredQty) as DeliveredQty from BirthList inner join BirthListRequestedItems on BirthList.BirthListId = BirthListRequestedItems.BirthListId and BirthList.SiteCode=BirthListRequestedItems.SiteCode inner join CustomerSaleOrder on BirthList.customerid=CustomerSaleOrder.CustomerNo where BirthList.BirthListId='{0}' group by BirthList.BirthListId,BirthList.SiteCode,BirthList.CustomerType,BirthList.customerid,BirthList.DeliveryDate,BirthList.EventId, BirthList.EventDate,BirthList.BirthListStatus, BirthList.BirthDate, CustomerSaleOrder.FirstName, CustomerSaleOrder.LastName,CustomerSaleOrder.CustomerName union select BirthList.BirthListId,BirthList.SiteCode,BirthList.CustomerType,BirthList.customerid,BirthList.DeliveryDate,BirthList.EventId,BirthList.EventDate,BirthList.BirthListStatus,BirthList.BirthDate,CLPCustomers.MiddleName, CLPCustomers.FirstName + ' ' + CLPCustomers.Surname as CustomerName,CLPCustomers.NameOnCard,sum(BirthListRequestedItems.RequstedQty) as RequstedQty,sum(BirthListRequestedItems.BookedQty ) as BookedQty,sum(BirthListRequestedItems.DeliveredQty) as DeliveredQty from BirthList inner join BirthListRequestedItems on BirthList.BirthListId = BirthListRequestedItems.BirthListId and BirthList.SiteCode=BirthListRequestedItems.SiteCode inner join dbo.CLPCustomers on BirthList.customerid=CLPCustomers.CardNo and CLPCustomers.status =1 where BirthList.BirthListId='{0}' and Birthlist.SiteCode = '{1}' group by BirthList.BirthListId,BirthList.CustomerType,BirthList.SiteCode,BirthList.customerid,BirthList.DeliveryDate, BirthList.EventId,BirthList.EventDate,BirthList.BirthListStatus, BirthList.BirthDate, CLPCustomers.FirstName, CLPCustomers.Surname,CLPCustomers.NameOnCard,CLPCustomers.MiddleName ", CtrlBirthListID.Text, clsAdmin.SiteCode)

                    dtBirth = objclsBirthList.RetrieveQuery(strQuery, errorMsg)
                    If Not BirthListInfo Is Nothing Then
                        dtTempBLSnap = BirthListInfo.Copy
                    End If
                    If (dtBirth.Rows.Count = 0) Then
                        ClearBirthListInformation(False)
                        If Not BirthListInfo Is Nothing Then
                            dtTempBLSnap = BirthListInfo.Copy
                        End If
                    Else
                        SelectedBirthListInfo = dtBirth.Rows(0)
                        If Not (SelectedBirthListInfo Is Nothing) Then
                            If (SelectedBirthListInfo.Item("BirthListStatus") = "Open" Or SelectedBirthListInfo.Item("BirthListStatus") = "True") Then
                                DisplayBirthListInformation()
                                ClearPaymentReceipt()
                                ClearGVDetails()
                                If Not BirthListInfo Is Nothing Then
                                    dtTempBLSnap = BirthListInfo.Copy
                                End If
                            Else
                                Dim strStatus As String = String.Format(getValueByKey("BL102"), SelectedBirthListInfo.Item("BirthListId"), SelectedBirthListInfo.Item("BirthListStatus"))
                                ShowMessage(strStatus, "BL102 - " & getValueByKey("CLAE04"))
                                ClearBirthListInformation()
                                ClearGVDetails()
                                ClearPaymentReceipt()
                            End If
                            CalculateTotalBalanceQty(True)

                        End If
                        If Not BirthListInfo Is Nothing Then
                            dtTempBLSnap = BirthListInfo.Copy
                        End If
                    End If
                Else
                    ClearBirthListInformation()
                    If Not BirthListInfo Is Nothing Then
                        dtTempBLSnap = BirthListInfo.Copy
                    End If
                    'MessageBox.Show(getValueByKey("BL023"), "BL023")
                    'MessageBox.Show("Connection Closed", "BirthListID Search")
                End If

            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub BtnItemSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnPurchaseBirthListItem.Click
        Try

            If Not (BirthListID = String.Empty) Then
                Dim objfrmItemSearch As New frmNItemSearch
                objfrmItemSearch.ShowDialog()
                If Not objfrmItemSearch.SearchResult Is Nothing Then

                    drSelectedItemDetails = objfrmItemSearch.ItemRow
                    Dim objclsBirthListGlobal As New clsBirthListGobal
                    If (objclsBirthListGlobal.IsArticleRateAvailabel(drSelectedItemDetails, "SellingPrice", "")) Then

                        'Rakesh:06.11.2013-->7895 : Avoid stock check validation when order place from SO & BL
                        'If (objclsBirthListGlobal.IsStockAvailable(clsDefaultConfiguration.NegativeInventoryAllowed, drSelectedItemDetails)) Then
                        AddItem()
                        'Else
                        '    ShowMessage(getValueByKey("BL046"), "BL046 - " & getValueByKey("CLAE04"))
                        '    'MessageBox.Show("Item not availabel in stock")
                        '    CtrlSalesPerson1.CtrlTxtBox.Focus()
                        'End If
                    Else
                        ShowMessage(getValueByKey("BL081"), "BL081 - " & getValueByKey("CLAE04"))
                        'Rate not found 
                        CtrlSalesPerson1.CtrlTxtBox.Focus()

                    End If
                    grdScanItem.AutoSizeCols()
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub AddItem()
        Try
            Dim strErrorMsg As String = String.Empty
            If IsItemSaleable(drSelectedItemDetails) Then
                objclsBirthListGlobal.AddNewItemRow(clsAdmin.SiteCode, clsAdmin.UserName, BirthListID, BirthListInfo, drSelectedItemDetails, strErrorMsg, clsDefaultConfiguration.ArticleTaxAllowed, dtMainTax, PreviousFinancialYear)
                If Not strErrorMsg = String.Empty Then
                    ShowMessage(strErrorMsg, getValueByKey("CLAE05"))
                End If

            Else
                ShowMessage(getValueByKey("BL100"), "BL100 - " & getValueByKey("CLAE04"))
            End If

            CalculateTotalBalanceQty(False)
            CtrlSalesPerson1.CtrlTxtBox.Focus()
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Function IsItemSaleable(Optional ByVal drSelectedItem As DataRow = Nothing) As Boolean
        Try
            If Not drSelectedItem Is Nothing Then
                If Not drSelectedItem("FreezeSB") Is DBNull.Value AndAlso drSelectedItem("FreezeSB") = False Then
                    Return True
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

    Private Sub txtItemSearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If (e.KeyCode = Keys.Enter) Then
                If BirthListID <> String.Empty Then
                    If CtrlSalesPerson1.CtrlTxtBox.Text.Trim() <> String.Empty Then
                        ManualEnterEANNumber()
                        CtrlSalesPerson1.CtrlTxtBox.Clear()
                    Else
                        ShowMessage(getValueByKey("BL094"), "BL094 - " & getValueByKey("CLAE04"))
                    End If

                End If

            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub



    Dim dtVoucherSales As DataTable
    Private Sub btnAdvanceSale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaleGV.Click
        Try
            If clsDefaultConfiguration.BLGVSale Then
                If Not BirthListID = String.Empty Then
                    'Dim obj As New frmAdvanceSale
                    'obj.TransactionID = TransactionTypes.BirthListSales
                    'dtVoucherSales = dtPOsVoucher
                    'obj.BirthListID = BirthListID
                    'obj.DataSource = dtVoucherSales
                    'obj.GVDetail = dtGV
                    'obj.ShowDialog()
                    'dtGV = obj.GVDetail
                    'If Not dtGV Is Nothing Then
                    '    For Each dr As DataRow In dtGV.Rows
                    '        dr("ISSUEDATSITE") = clsAdmin.SiteCode
                    '        dr("ISSUEDONDATE") = Now.Date
                    '        dr("ISSUEDINDOCTYPE") = 301
                    '    Next
                    'End If


                    Dim objAdvanceSale As New frmNBirthListAdvanceSale

                    If dtVoucherSales Is Nothing Then
                        Dim objclsBirthListSales As New clsBirthListSales
                        dtVoucherSales = objclsBirthListSales.TableStructure()
                    End If
                    objAdvanceSale.DTableVoucher = dtVoucherSales
                    objAdvanceSale.ShowDialog()
                    dtVoucherSales = objAdvanceSale.DTableVoucher
                    DisplayGV()
                    CalculatePaymentReciept()
                    'CalculateTotalBalanceQty()
                End If
            Else
                ShowMessage(getValueByKey("BL080"), "BL080 - " & getValueByKey("CLAE04"))
            End If
        Catch ex As Exception
            ShowMessage(ex.Message(), getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub DisplayGV()
        Try
            Dim decPrevVoucherTotal As Decimal = Decimal.Zero
            Dim decTotalVoucher As Decimal = Decimal.Zero
            If Not dtVoucherSales Is Nothing Then
                If (dtVoucherSales.Rows.Count > 0) Then
                    If Not (dtVoucherSales.Rows(0)("EAN") Is DBNull.Value) Then
                        For Each drAdvanceSale As DataRow In dtVoucherSales.Rows
                            decTotalVoucher = decPrevVoucherTotal + dtVoucherSales.Compute("sum(NetAmount)", " ")
                            CtrlCashSummary1.lbltxt5 = CurrencyFormat(decTotalVoucher)
                        Next
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
            Dim dtItem As DataTable
            If (objclsBirthList.FindArticleCode(dtItem, CtrlSalesPerson1.CtrlTxtBox.Text, clsAdmin.SiteCode, EanType, clsAdmin.LangCode)) Then
                drSelectedItemDetails = SearchBirthListItem(dtItem)
                If Not drSelectedItemDetails Is Nothing Then
                    If drSelectedItemDetails("FreezeSR") = True Then
                        ShowMessage(getValueByKey("BL100"), "BL100 - " & getValueByKey("CLAE04"))
                        Return False

                    End If

                    If drSelectedItemDetails("FreezeSB") = True Then
                        ShowMessage(getValueByKey("BL100"), "BL100 - " & getValueByKey("CLAE04"))
                        Return False

                    End If
                    If (objclsBirthListGlobal.IsArticleRateAvailabel(drSelectedItemDetails, "SellingPrice", "")) Then
                        Dim objclsBirthListGlobal As New clsBirthListGobal
                        If (objclsBirthListGlobal.IsStockAvailable(clsDefaultConfiguration.NegativeInventoryAllowed, drSelectedItemDetails)) Then

                            GridDataSource()


                        Else
                            'MessageBox.Show("Item is not availabel in stock")
                            ShowMessage(getValueByKey("BL046"), "BL046 - " & getValueByKey("CLAE04"))
                            CtrlSalesPerson1.CtrlTxtBox.Focus()
                        End If
                        Return True
                    Else
                        ShowMessage(getValueByKey("BL081"), "BL081 - " & getValueByKey("CLAE04")) 'Rate Not found 
                        Return False
                    End If
                Else

                    Return False
                End If

            Else
                ShowMessage(getValueByKey("BL014"), "BL014 - " & getValueByKey("CLAE04"))
                'MessageBox.Show("Item is not found or not for sale")
                CtrlSalesPerson1.CtrlTxtBox.Focus()
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function
    ''' <summary>
    '''  DataSource for grid 
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>

    Private Function GridDataSource() As Boolean
        If Not (BirthListID = String.Empty) Then
            If Not drSelectedItemDetails Is Nothing Then
                AddItem()
            End If
        End If

    End Function


    Private Sub txtItemSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If Not (BirthListID = String.Empty) Then
            If CtrlSalesPerson1.CtrlTxtBox.Text.Length > 3 Then
                ManualEnterEANNumber()
            End If


        End If
    End Sub

    Private Sub cboInvoiceNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            ReprintGridItem()
        Catch ex As Exception
            LogException(ex)
        End Try


    End Sub

    Private Sub BtnSOClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnClose.Click

        Me.Close()

    End Sub

    Private Sub txtItemSearch_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs)
        If (e.KeyCode = Keys.Enter) Then
            If Not (BirthListID = String.Empty) Then
                ManualEnterEANNumber()
            End If
        End If
    End Sub

    Private Function CalculateCLPPoints(Optional ByVal cardtype As String = "", Optional ByVal CNumber As String = "") As Boolean
        Try
            If (AddCLPColumns()) Then
                Dim strFilterCriteria As String = "CLPRequire=true and PurchasedQty > 0"
                'If (CalCulateCLP(SalesCustomerType, BirthListInfo, strFilterCriteria)) Then
                'End If
                Dim card As String
                Dim cNO As String

                If cardtype <> "" Then
                    card = cardtype
                Else
                    card = SalesCustomerType
                End If


                If CNumber <> "" Then
                    cNO = CNumber
                Else
                    cNO = dtCustmInfo.Rows(0)("CustomerNo")
                End If

                CalCulateCLPSlabwise(card, BirthListInfo, strFilterCriteria, cNO, dtPaymentReceipt)
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Private Function AddCLPColumns() As Boolean
        Try
            If Not (BirthListInfo.Columns.Contains("SalesExecutiveCode")) Then
                Dim clnSalesExecutiveCode As New DataColumn
                clnSalesExecutiveCode.ColumnName = "SalesExecutiveCode"
                BirthListInfo.Columns.Add(clnSalesExecutiveCode)
            End If


            'If Not (BirthListInfo.Columns.Contains("CLPPoints")) Then
            '    Dim clnCLPPoints As New DataColumn
            '    clnCLPPoints.ColumnName = "CLPPoints"
            '    clnCLPPoints.DataType = System.Type.GetType("System.Decimal")
            '    clnCLPPoints.DefaultValue = Decimal.Zero
            '    BirthListInfo.Columns.Add(clnCLPPoints)

            'End If

            'If Not (BirthListInfo.Columns.Contains("CLPDiscount")) Then
            '    Dim clnCLPDiscount As New DataColumn
            '    clnCLPDiscount.ColumnName = "CLPDiscount"
            '    clnCLPDiscount.DataType = System.Type.GetType("System.Decimal")
            '    BirthListInfo.Columns.Add(clnCLPDiscount)

            'End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Sub rbtnCancelBirhtlist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnCancelBirhtlist.Click
        'Dim frmBirthListUpdate As New frmn
        'frmBirthListUpdate()

    End Sub

    Private Sub rbtnNewBirthList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnNewBirthList.Click
        Try
            Me.Close()
            If (isFormClosed) Then
                Dim objfrmBirthListCreate As New frmNBirthListCreate
                MDISpectrum.ShowChildForm(objfrmBirthListCreate, True)
            End If

        Catch ex As Exception

        End Try

    End Sub

    ''' <summary>
    ''' validate user input.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateInput() As Boolean
        Try
            If (BirthListID <> String.Empty) Then
                If (IsPurchaseCustomerSelected) Then
                    If clsDefaultConfiguration.BLIsSalesPersonApplicable = True Then
                        If (CtrlSalesPerson1.CtrlSalesPersons.SelectedIndex <= -1) Then
                            ShowMessage(getValueByKey("BL002"), "BL002 - " & getValueByKey("CLAE04"))
                            Return False
                        Else
                            Return True
                        End If
                    Else
                        Return True
                    End If
                Else 'Purchase Customer '
                    Return False
                End If
            Else 'BirthList ID'
                Return False
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("BL002"), "BL002 - " & getValueByKey("CLAE05"))
            LogException(ex)
            Return False
        End Try
    End Function

    Private Sub rbnbtnPayCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If ValidateInput() Then
                If Not (IsAmountSettle()) Then
                    Dim decTotalPurchasedAmount As Decimal = CalculateTotalAmount()
                    If (decTotalPurchasedAmount > Decimal.Zero) Then
                        Dim objPayment As New frmNAcceptPaymentByCard()
                        objPayment.TotalBillAmount = decTotalPurchasedAmount
                        objPayment.ShowDialog()
                        Dim selectedTenderName As String = objPayment.SelectedTenderName
                        Dim strSelectedTenderCode As String = objPayment.CardTenderCode
                        objPayment.Close()
                        If Not (objPayment.IsCancelAcceptPayment) Then
                            If Not objPayment.ReciptTotalAmount Is Nothing And objPayment.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then

                                dtPaymentReceipt = objPayment.ReciptTotalAmount.Tables("MSTRecieptType")
                                If objPayment.Action = My.Resources.AcceptPaymentActionTypeSave Then
                                    isGiftVoucherDocumentPrint = False
                                    GiftReceiptMessage = objPayment.GiftReceiptMessage
                                    btnSavePrint_Click(sender, e)
                                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                                ElseIf objPayment.Action = My.Resources.AcceptPaymentActionTypeGift Then
                                    isGiftVoucherDocumentPrint = True
                                    GiftReceiptMessage = objPayment.GiftReceiptMessage
                                    btnSavePrint_Click(sender, e)
                                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                                End If

                            Else
                                ShowMessage(getValueByKey("BL095"), "BL095 - " & getValueByKey("CLAE05"))
                            End If
                        End If
                    Else
                        ShowMessage(getValueByKey("BL025"), "BL025 - " & getValueByKey("CLAE04"))
                        'MessageBox.Show("First you have to purchase the item")
                    End If
                Else
                    ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                    'MessageBox.Show("Amount is settled.")
                End If
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM034"), "CM034 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in Updating card payment data ", "Information")
        End Try
    End Sub

    Private Function IsAmountSettle() As Boolean
        Try
            If Not dtPaymentReceipt Is Nothing And dtPaymentReceipt.Rows.Count > 0 Then
                Dim decTotalAmount As Decimal = CalculateTotalAmount()
                Dim decTotalPaid As Decimal = dtPaymentReceipt.Compute("sum(Amount)", "")
                If (decTotalAmount <= decTotalPaid) Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM034"), "CM034 - " & getValueByKey("CLAE05"))
            LogException(ex)
            Return False
        End Try
    End Function


    Private Sub rbnbtnPayCheque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            If ValidateInput() Then
                If Not (IsAmountSettle()) Then


                    Dim decTotalPurchasedAmount As Decimal = CalculateTotalAmount()
                    If decTotalPurchasedAmount > 0 Then
                        Dim objCheck As New frmNCheckPayment
                        objCheck.BillAmount = decTotalPurchasedAmount
                        objCheck.ShowDialog()
                        'If objCheck.CheckAmount > 0 Then
                        '    objCheck.Close()
                        '    Dim objPayment As New frmNAcceptPayment()
                        '    objPayment.Show()
                        '    objPayment.TotalBillAmount = decTotalPurchasedAmount
                        '    objPayment.Enabled = False
                        '    objPayment.cboRecieptType.SelectedValue = "Cheque"
                        '    objPayment.TotalBillAmount = decTotalPurchasedAmount
                        '    'objPayment.cboCurrency.SelectedIndex = 1
                        '    objPayment.InsertCheque(objCheck.CheckAmount, objCheck.CheckNo, objCheck.CheckDate, objCheck.MicrNo, objCheck.BankName)
                        '    Dim ds As DataSet = objPayment.ReciptTotalAmount()
                        '    objPayment.Close()
                        '    If Not ds Is Nothing Then
                        '        If Not (objPayment.IsCancelAcceptPayment) Then
                        '            'InsertPaymentTransaction_Manually(decTotalPurchasedAmount, "Cheque", "Cheque")
                        '            btnSavePrint_Click(sender, e)
                        '        End If
                        '    End If
                        'End If
                        If objCheck.IsCancelAcceptPayment = False Then
                            If Not objCheck.ReciptTotalAmount Is Nothing And objCheck.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                                dtPaymentReceipt = objCheck.ReciptTotalAmount.Tables("MSTRecieptType")
                                objCheck.Close()
                                If Not dtPaymentReceipt Is Nothing Then
                                    If Not (objCheck.IsCancelAcceptPayment) Then

                                        If objCheck.Action = My.Resources.AcceptPaymentActionTypeSave Then
                                            isGiftVoucherDocumentPrint = False
                                            GiftReceiptMessage = objCheck.GiftReceiptMessage
                                            btnSavePrint_Click(sender, e)
                                            AutoLogout(FrmTranCode, Me, lblLoggedIn)
                                        ElseIf objCheck.Action = My.Resources.AcceptPaymentActionTypeGift Then
                                            isGiftVoucherDocumentPrint = True
                                            GiftReceiptMessage = objCheck.GiftReceiptMessage
                                            btnSavePrint_Click(sender, e)
                                            AutoLogout(FrmTranCode, Me, lblLoggedIn)
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
                Else
                    ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                    'MessageBox.Show("Amount is settled.")
                End If

            End If


        Catch ex As Exception
            ShowMessage(getValueByKey("CM034"), "CM034 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in Updating card payment data ", "Information")
        End Try
    End Sub
    Private Sub rbnbtnPayCash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If ValidateInput() Then
                If Not (IsAmountSettle()) Then


                    Dim decTotalPurchasedAmount As Decimal = CalculateTotalAmount()
                    If (decTotalPurchasedAmount > Decimal.Zero) Then
                        Dim objPaymentByCash As New frmNAcceptPaymentByCash
                        objPaymentByCash.TotalBillAmount = decTotalPurchasedAmount
                        objPaymentByCash.ShowDialog()
                        If Not (objPaymentByCash.IsCancelAcceptPayment) Then
                            If Not objPaymentByCash.ReciptTotalAmount Is Nothing And objPaymentByCash.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                                dtPaymentReceipt = objPaymentByCash.ReciptTotalAmount.Tables("MSTRecieptType")

                                If objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeSave Then
                                    isGiftVoucherDocumentPrint = False
                                    GiftReceiptMessage = objPaymentByCash.GiftReceiptMessage

                                    btnSavePrint_Click(sender, e)
                                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                                ElseIf objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeGift Then
                                    isGiftVoucherDocumentPrint = True
                                    GiftReceiptMessage = objPaymentByCash.GiftReceiptMessage
                                    btnSavePrint_Click(sender, e)
                                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                                End If

                            Else
                                ShowMessage(getValueByKey("BL095"), "BL095 - " & getValueByKey("CLAE05"))
                            End If
                        End If
                    Else
                        ShowMessage(getValueByKey("BL025"), "BL025 - " & getValueByKey("CLAE04"))
                        'MessageBox.Show("First you have to purchase the item")
                    End If
                Else
                    ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                    'MessageBox.Show("Amount is settled.")
                End If
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
            If Not BirthListInfo Is Nothing Then
                If BirthListInfo.Rows.Count > 0 Then
                    Dim objTotalAount As Object = BirthListInfo.Compute("sum(NetAmount)", "")
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

    Private Sub CtrlBtnStockCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlBtnStockCheck.Click
        Try
            Dim objfrmStockCheck As New frmNStockCheck
            objfrmStockCheck.ShowDialog()
        Catch ex As Exception
            LogException(ex)
        End Try
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
                Case Keys.F12
                    'PriceChange()
                    If BirthListID <> String.Empty Then
                        Dim outIRowIndex As Integer = 0
                        Dim outIColumnIndex As Integer = 0
                        If clsDefaultConfiguration.PriceChageAllowed = True Then
                            If grdScanItem.RowSel > 0 Then
                                If CheckInterTransactionAuth("PriceChange") Then
                                    If (BirthListInfo.Rows(grdScanItem.RowSel - 1)("RequstedQty") <> BirthListInfo.Rows(grdScanItem.RowSel - 1)("BookedQty")) Then
                                        F2_ChangeSalesQuantity(grdScanItem, "SellingPrice", getValueByKey("SP002"), outIRowIndex, outIColumnIndex, BirthListInfo)
                                        outIColumnIndex = grdScanItem.Cols("PurchasedQty").Index
                                        'Change by Ashish for CR 5679
                                        'Set the Price changed flag here to true or false based on the new price entered.
                                        Dim val As Decimal = 0.0
                                        Dim originalVal As Decimal = 0.0
                                        If (outIRowIndex <> 0) Then
                                            val = BirthListInfo.Rows(outIRowIndex - 1)("SellingPrice")
                                            If (BirthListInfo.Rows(outIRowIndex - 1).IsNull("ActualSellingPrice")) Then
                                                BirthListInfo.Rows(outIRowIndex - 1)("ActualSellingPrice") = 0
                                            End If
                                            originalVal = BirthListInfo.Rows(outIRowIndex - 1)("ActualSellingPrice")
                                            If (val <> originalVal) Then
                                                BirthListInfo.Rows(outIRowIndex - 1)("IsPriceChangedHere") = True
                                                BirthListInfo.Rows(outIRowIndex - 1)("IsPriceChanged") = True
                                            Else
                                                BirthListInfo.Rows(outIRowIndex - 1)("IsPriceChangedHere") = False
                                            End If
                                            'Changed by Rohit for Issue No. 6123
                                            If Not BirthListInfo.Rows(grdScanItem.RowSel - 1)("PurchasedQty") Is DBNull.Value AndAlso BirthListInfo.Rows(grdScanItem.RowSel - 1)("PurchasedQty") = 0 Then
                                                BirthListInfo.Rows(grdScanItem.RowSel - 1)("PurchasedQty") = BirthListInfo.Rows(grdScanItem.RowSel - 1)("BalanceItemQty")
                                            End If
                                            'End of change by Rohit
                                            'End of change
                                            CheckColumnName("PurchasedQty", outIRowIndex, outIColumnIndex)
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


                Case Keys.F2
                    Try
                        If BirthListID <> String.Empty Then

                            Dim outIRowIndex As Integer = 0
                            Dim outIColumnIndex As Integer = 0
                            F2_ChangeSalesQuantity(grdScanItem, "PurchasedQty", getValueByKey("BL106"), outIRowIndex, outIColumnIndex)
                            CheckColumnName("PurchasedQty", outIRowIndex, outIColumnIndex)

                        End If

                    Catch ex As Exception
                        LogException(ex)
                    End Try
            End Select
        End If
        Return MyBase.ProcessKeyPreview(m)
    End Function

    Private Sub CtrlBirthListID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CtrlBirthListID.KeyDown
        If e.KeyCode = Keys.Enter Then
            lblBirthListNo_TextChanged(sender, e)
            'btnSearchBirthListID_Click(sender, e)
        End If
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
                    Case "ArticleDescription".ToLower
                        'strTooltip = "Item Description"
                        strTooltip = getValueByKey("TT002")
                    Case "SellingPrice".ToLower
                        'strTooltip = "Ordered Quantity"
                        strTooltip = getValueByKey("TT003")
                    Case "RequstedQty".ToLower
                        'strTooltip = "Ordered Quantity"
                        strTooltip = getValueByKey("TT004")
                    Case "Rate".ToLower
                        'strTooltip = "Price"
                        strTooltip = getValueByKey("TT003")
                    Case "Amount".ToLower
                        'strTooltip = "Amount"
                        strTooltip = getValueByKey("TT021")
                    Case "ReservedQty".ToLower
                        'strTooltip = "Reserved Quantity"
                        strTooltip = getValueByKey("TT007")
                    Case "CLPREQUIRE".ToLower
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


    Private Sub rbtnNewBirthList1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnNewBirthList1.Click
        Try

            Me.Close()
            If (isFormClosed) Then
                Dim objfrmBirthListSales As New frmNBirthListUpdate
                MDISpectrum.ShowChildForm(objfrmBirthListSales, True)
            End If


        Catch ex As Exception

        End Try
    End Sub

    'Private Sub frmNBirthListSales_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    '    Try
    '        If e.KeyCode = Keys.F2 Then
    '            Dim outIRowIndex As Integer = 0
    '            Dim outIColumnIndex As Integer = 0
    '            F2_ChangeSalesQuantity(grdScanItem, "PurchasedQty", "Enter Order Qunatity", outIRowIndex, outIColumnIndex)
    '            CheckColumnName("PurchasedQty", outIRowIndex, outIColumnIndex)
    '        End If
    '    Catch ex As Exception
    '        LogException(ex)
    '    End Try 
    'End Sub


    Private Sub grdScanItem_ValidateEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.ValidateEditEventArgs) Handles grdScanItem.ValidateEdit
        Try

            If ValidateQuantity(grdScanItem, e.Col, "PurchasedQty") Then
                MsgBox(getValueByKey("CM059"), MsgBoxStyle.Critical, "CM059" & " | " & getValueByKey("CLAE05"))
                e.Cancel = True
            ElseIf ValidateQuantity(grdScanItem, e.Col, "PickUpQty") Then

                MsgBox(getValueByKey("CM059"), MsgBoxStyle.Critical, "CM059" & " | " & getValueByKey("CLAE05"))
                e.Cancel = True

            End If


        Catch ex As Exception
            LogException(ex)
            ShowMessage(getValueByKey("CM059"), "CM059 - " & getValueByKey("CLAE05"))

        End Try
    End Sub

    Private Sub PriceChange()
        If BirthListID <> String.Empty Then
            Dim outIRowIndex As Integer = 0
            Dim outIColumnIndex As Integer = 0
            If clsDefaultConfiguration.PriceChageAllowed = True Then
                If grdScanItem.RowSel > 0 Then
                    If CheckInterTransactionAuth("PriceChange") Then
                        If (BirthListInfo.Rows(grdScanItem.RowSel - 1)("BookedQty") = 0) Then
                            F2_ChangeSalesQuantity(grdScanItem, "SellingPrice", getValueByKey("SP002"), outIRowIndex, outIColumnIndex, BirthListInfo)
                            outIColumnIndex = grdScanItem.Cols("PurchasedQty").Index
                            Dim val As Decimal = 0.0
                            Dim originalVal As Decimal = 0.0
                            If (outIRowIndex <> 0) Then
                                val = BirthListInfo.Rows(outIRowIndex - 1)("SellingPrice")
                                If (BirthListInfo.Rows(outIRowIndex - 1).IsNull("ActualSellingPrice")) Then
                                    BirthListInfo.Rows(outIRowIndex - 1)("ActualSellingPrice") = 0
                                End If
                                originalVal = BirthListInfo.Rows(outIRowIndex - 1)("ActualSellingPrice")
                                If (val <> originalVal) Then
                                    BirthListInfo.Rows(outIRowIndex - 1)("IsPriceChangedHere") = True
                                    BirthListInfo.Rows(outIRowIndex - 1)("IsPriceChanged") = True
                                Else
                                    BirthListInfo.Rows(outIRowIndex - 1)("IsPriceChangedHere") = False
                                End If
                                'Changed by Rohit for Issue No. 6123
                                If Not BirthListInfo.Rows(grdScanItem.RowSel - 1)("PurchasedQty") Is DBNull.Value AndAlso BirthListInfo.Rows(grdScanItem.RowSel - 1)("PurchasedQty") = 0 Then
                                    BirthListInfo.Rows(grdScanItem.RowSel - 1)("PurchasedQty") = BirthListInfo.Rows(grdScanItem.RowSel - 1)("BalanceItemQty")
                                End If

                                CheckColumnName("PurchasedQty", outIRowIndex, outIColumnIndex)
                            End If
                        Else
                            ShowMessage(getValueByKey("BL108"), "BL108 - " & getValueByKey("CLAE04"))
                            'MessageBox.Show("Cannot change price of sold quantities")
                        End If
                    End If
                End If
            Else
                'price change is not allowed '
                ShowMessage(getValueByKey("BL108"), "BL108 - " & getValueByKey("CLAE04"))
            End If
        End If
    End Sub

    Private Sub grdScanItem_BeforeEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdScanItem.BeforeEdit


        If Not grdScanItem.DataSource Is Nothing Then



            If clsDefaultConfiguration.CLP_Applicable_Edit Then

                If Not grdScanItem.Cols("CLPREQUIRE") Is Nothing Then



                    grdScanItem.Cols("CLPREQUIRE").AllowEditing = True
                End If

            Else
                If Not grdScanItem.Cols("CLPREQUIRE") Is Nothing Then

                    grdScanItem.Cols("CLPREQUIRE").AllowEditing = False
                End If

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
    Private Sub frmNBirthListSales_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "553-birth-list-sales.htm")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
End Class
