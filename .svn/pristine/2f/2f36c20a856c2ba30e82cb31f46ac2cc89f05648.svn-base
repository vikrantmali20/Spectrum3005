Imports SpectrumBL
Imports SpectrumPrint

Public Class frmViewSalesOrderDetails

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Dim objCM As New clsSalesOrder
    Public pSearchCust As String = ""
    Dim dsMain As New DataSet
    Dim dtDocInfo As New DataTable
    Dim dtDocDetail As New DataTable
    Dim objSPrint As New clsReprintBill
    Dim _DocumentNo As String

    Public Property DocumentNo() As String
        Get
            Return _DocumentNo
        End Get
        Set(ByVal value As String)
            _DocumentNo = value
        End Set
    End Property

    Private _customerNo As String
    Public Property CustomerNo() As String
        Get
            Return _customerNo
        End Get
        Set(ByVal value As String)
            _customerNo = value
        End Set
    End Property

    Private _customerName As String
    Public Property CustomerName() As String
        Get
            Return _customerName
        End Get
        Set(ByVal value As String)
            _customerName = value
        End Set
    End Property

    Private _balancePoint As String
    Public Property BalancePoint() As String
        Get
            Return _balancePoint
        End Get
        Set(ByVal value As String)
            _balancePoint = value
        End Set
    End Property

    Dim ObjclsCommon As New clsCommon

    Private Sub frmViewSalesOrderDetails_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                'If UCase(pSearchCust) = "SEARCH" AndAlso Me.Tag = String.Empty Then
                '    Dim ChildForm As New Spectrum.frmViewSalesOrderDetails
                '    ChildForm.pSearchCust = "SEARCH"
                '    ChildForm.Tag = String.Empty
                '    ChildForm.ShowDialog()
                '    Me.Dispose()
                'End If
                SelectedCustmCode = String.Empty
                If UCase(pSearchCust) = "SEARCH" AndAlso Me.Tag = String.Empty Then
                    Call SearchCustomer()
                End If
                If Not String.IsNullOrEmpty(SelectedCustmCode) Then
                    LoadDocuments()
                    DocumentNo = String.Empty
                    If grdDocumentInfo.RowCount > 0 Then
                        DocumentNo = IIf(grdDocumentInfo.Item(grdDocumentInfo.Row, EnumDocs.SalesOrderNo) Is System.DBNull.Value, "", grdDocumentInfo.Item(grdDocumentInfo.Row, EnumDocs.SalesOrderNo))
                    End If
                    Call GetCashMemoDetails(DocumentNo, clsAdmin.SiteCode)
                Else
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub frmViewSalesOrderDetails_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        SelectedCustmCode = String.Empty
        If UCase(pSearchCust) = "SEARCH" AndAlso Me.Tag = String.Empty Then
            Call SearchCustomer()
        Else
            SelectedCustmCode = CustomerNo
            lblCustomerNoValue.Text = CustomerNo
            lblCustomerNameValue.Text = CustomerName
            lblBalancePointsValue.Text = BalancePoint
        End If
        If Not String.IsNullOrEmpty(SelectedCustmCode) Then
            Call LoadDocuments()
            Call getBinding()
            Call SetCulture(Me, Me.Name)
            grdDocumentInfo.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow
            grdDocumentInfo.HighLightRowStyle.ForeColor = Color.WhiteSmoke
            grdDocumentInfo.HighLightRowStyle.BackColor = Color.Navy

            If grdDocumentInfo.RowCount > 0 Then
                DocumentNo = IIf(grdDocumentInfo.Item(grdDocumentInfo.Row, EnumDocs.SalesOrderNo) Is System.DBNull.Value, "", grdDocumentInfo.Item(grdDocumentInfo.Row, EnumDocs.SalesOrderNo))
                Call GetCashMemoDetails(DocumentNo, clsAdmin.SiteCode)
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub SearchCustomer() 'Handles BtnSearchCustomer.Click
        Try
            Dim objCust As New frmSearchCustomer
            objCust.IsViewOrderHistory = False
            objCust.ShowDialog()

            Dim dt As DataTable
            Dim objCustm As New clsCLPCustomer
            Dim dtCust As DataTable = objCust.dtCustmInfo()

            If objCust.dtCustmInfo Is Nothing Then
                If pSearchCust = "SEARCH" Then
                    Exit Sub
                End If
            ElseIf objCust.dtCustmInfo.Rows.Count > 0 Then
                SelectedCustmCode = objCust.dtCustmInfo.Rows(0)("CustomerNo")
                lblBalancePointsValue.Text = objCust.dtCustmInfo.Rows(0)("BALANCEPOINT")
                lblCustomerNameValue.Text = objCust.dtCustmInfo.Rows(0)("CUSTOMERNAME")
                lblCustomerNoValue.Text = objCust.dtCustmInfo.Rows(0)("CustomerNo")
            Else

            End If
            'If Not dtCust Is Nothing AndAlso dtCust.Rows.Count > 0 Then
            '    SetCustomerInformationInForm(dtCust)

            'ElseIf objCust.vCustomerNo <> String.Empty Then
            '    dt = objCustm.GetCustomerInformation("SO", clsAdmin.SiteCode, clsAdmin.CLPProgram, objCust.vCustomerNo)
            '    SetCustomerInformationInForm(dt)
            'End If

            objCust.Dispose()
            '---------------------- search-----------------
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub LoadDocuments()
        Try
            DocumentType = "SalesOrder"
            dtDocInfo = objCM.GetAllDocumentInfoCustmWise(clsAdmin.SiteCode, clsAdmin.Financialyear, DocumentType, SelectedCustmCode)
            '  If Not (dtDocInfo Is Nothing) And dtDocInfo.Rows.Count > 0 Then
            grdDocumentInfo.DataSource = dtDocInfo
            Call grdDocumentInfoColumnSettings()

            For colIndex = 0 To grdDocumentInfo.Splits(0).DisplayColumns.Count - 1
                grdDocumentInfo.Splits(0).DisplayColumns(colIndex).AutoSize()
            Next
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub
    Enum EnumDocs
        DocDate = 0
        SalesOrderNo = 1
        TotalItemst = 2
        TotalAmount = 3
        AmountPaid = 4
        AmountDue = 4
    End Enum

    Private Sub grdDocumentInfoColumnSettings()
        Try
            '      If grdCreditSales.Cols.Count > 0 Then
            'Dim displayColumns As String = "BillDate,CashMemoNo,DeliveryPersonID,TotalItems,NetAmt"
            'Dim columnsList = displayColumns.ToUpper().Split(",")

            'For colIndex = 0 To grdDocumentInfo.Columns.Count - 1 Step 1
            '    If columnsList.Contains(grdDocumentInfo.Columns(colIndex).Caption.ToUpper()) Then
            '        grdDocumentInfo.Columns(colIndex).vis = True
            '    Else
            '        grdDocumentInfo.Columns(colIndex).Visible = False
            '    End If
            'Next
            grdDocumentInfo.Columns("BillDate").Caption = getValueByKey("frmViewSalesOrderDetails.grdDocumentInfo.BillDate")
            grdDocumentInfo.Columns("SalesOrderNo").Caption = getValueByKey("frmViewSalesOrderDetails.grdDocumentInfo.SalesOrderNo")
            grdDocumentInfo.Columns("TotalItems").Caption = getValueByKey("frmViewSalesOrderDetails.grdDocumentInfo.TotalItems")
            grdDocumentInfo.Columns("TotalAmount").Caption = getValueByKey("frmViewSalesOrderDetails.grdDocumentInfo.TotalAmount")
            grdDocumentInfo.Columns("AmountPaid").Caption = getValueByKey("frmViewSalesOrderDetails.grdDocumentInfo.AmountPaid")
            grdDocumentInfo.Columns("AmountReceived").Caption = getValueByKey("frmViewSalesOrderDetails.grdDocumentInfo.AmountReceived")
            grdDocumentInfo.Columns("AmountDue").Caption = getValueByKey("frmViewSalesOrderDetails.grdDocumentInfo.AmountDue")

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub CashMemoDetailsGridColumnSettings()
        Try
            '      If grdCreditSales.Cols.Count > 0 Then
            Dim displayColumns As String = "DISCRIPTION,Quantity,SellingPrice,NetAmt,TotalDiscPercentage"
            Dim columnsList = displayColumns.ToUpper().Split(",")

            For colIndex = 0 To grdCashMemoDetails.Cols.Count - 1 Step 1
                If columnsList.Contains(grdCashMemoDetails.Cols(colIndex).Name.ToUpper()) Then
                    grdCashMemoDetails.Cols(colIndex).Visible = True
                Else
                    grdCashMemoDetails.Cols(colIndex).Visible = False
                End If
            Next
            grdCashMemoDetails.Cols("DISCRIPTION").Caption = getValueByKey("frmViewSalesOrderDetails.grdCashMemoDetails.DISCRIPTION")
            grdCashMemoDetails.Cols("Quantity").Caption = getValueByKey("frmViewSalesOrderDetails.grdCashMemoDetails.Quantity")
            grdCashMemoDetails.Cols("SellingPrice").Caption = getValueByKey("frmViewSalesOrderDetails.grdCashMemoDetails.SellingPrice")
            grdCashMemoDetails.Cols("NetAmt").Caption = getValueByKey("frmViewSalesOrderDetails.grdCashMemoDetails.NetAmt")
            grdCashMemoDetails.Cols("TotalDiscPercentage").Caption = getValueByKey("frmViewSalesOrderDetails.grdCashMemoDetails.TotalDiscPercentage")

            grdCashMemoDetails.AutoSizeCols()
            grdCashMemoDetails.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Binding the Controls with the DataSet
    ''' </summary>
    ''' <param name="StrArticle">ArticleCode</param>
    ''' <remarks></remarks>
    Private Sub getBinding()
        Try
            dsMain = objCM.GetStruc("0", "0")
            ' grdDocumentsDetails.DataSource = dsMain.Tables("CASHMEMODTL")
            CtrlCashSummary1.CtrllblDiscAmt.DataBindings.Add("Text", dsMain.Tables("SALESORDERHDR"), "TOTALDISCOUNT")
            CtrlCashSummary1.CtrllblGrossAmt.DataBindings.Add("Text", dsMain.Tables("SALESORDERHDR"), "GROSSAMT")
            CtrlCashSummary1.CtrllblNetAmt.DataBindings.Add("Text", dsMain.Tables("SALESORDERHDR"), "NETAMT")
            'CashSummary.CtrlLabel5.DataBindings.Add("Text", dsMain.Tables("SALESORDERHDR"), "AmountPaid")

        Catch ex As Exception
            ShowMessage(getValueByKey("CM005"), "CM005 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Transaction Not properly Binded", "Error")
        End Try
    End Sub

    Private Sub grdDocumentInfo_DoubleClick(sender As System.Object, e As System.EventArgs) Handles grdDocumentInfo.DoubleClick
        If grdDocumentInfo.Row = -1 Then Exit Sub
        If grdDocumentInfo.RowCount = 0 Then
            ShowMessage("Please select the record first", getValueByKey("CLAE04"))
            Exit Sub
        End If
        If grdDocumentInfo.Row >= 0 Then
            DocumentNo = IIf(grdDocumentInfo.Item(grdDocumentInfo.Row, EnumDocs.SalesOrderNo) Is System.DBNull.Value, "", grdDocumentInfo.Item(grdDocumentInfo.Row, EnumDocs.SalesOrderNo))
        End If
        Call GetCashMemoDetails(DocumentNo, clsAdmin.SiteCode)
    End Sub

    Private Sub grdDocumentInfo_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdDocumentInfo.KeyUp
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
            If grdDocumentInfo.Row = -1 Then Exit Sub
            If grdDocumentInfo.RowCount = 0 Then
                'ShowMessage("Please select the record first", getValueByKey("CLAE04"))
                Exit Sub
            End If
            If grdDocumentInfo.RowCount > 0 Then
                DocumentNo = IIf(grdDocumentInfo.Item(grdDocumentInfo.Row, EnumDocs.SalesOrderNo) Is System.DBNull.Value, "", grdDocumentInfo.Item(grdDocumentInfo.Row, EnumDocs.SalesOrderNo))
            End If
        End If
        Call GetCashMemoDetails(DocumentNo, clsAdmin.SiteCode)
    End Sub


    ''' <summary>
    ''' Get previous Cash Memo Details
    ''' </summary>
    ''' <param name="strCashMemo">Cash Memo No</param>
    ''' <param name="strSiteCode">Site Code</param>
    ''' <remarks></remarks>
    Private Sub GetCashMemoDetails(ByVal strCashMemo As String, ByVal strSiteCode As String)
        Try

            'dtDocDetail = objCM.GetAllItemsBySalesOrder(clsAdmin.SiteCode, strCashMemo)
            ''  If Not (dtDocInfo Is Nothing) And dtDocInfo.Rows.Count > 0 Then
            'grdCashMemoDetails.DataSource = dtDocDetail
            'Call CashMemoDetailsGridColumnSettings()

            ''For colIndex = 0 To grdDocumentInfo.Splits(0).DisplayColumns.Count - 1
            ''    grdDocumentInfo.Splits(0).DisplayColumns(colIndex).AutoSize()
            ''Next
            'Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog

            Dim dsTemp As DataSet
            dsTemp = objCM.GetStruc(strCashMemo, strSiteCode)
            dsMain.Clear()
            If Not dsTemp Is Nothing AndAlso dsTemp.Tables.Count > 0 Then
                dsMain.Tables("SALESORDERHDR").Merge(dsTemp.Tables(0), False, MissingSchemaAction.Ignore)
                dsMain.Tables("SALESORDERDTL").Merge(dsTemp.Tables(1), False, MissingSchemaAction.Ignore)
                dsMain.Tables("SALESORDERRECEIPT").Merge(dsTemp.Tables(2), False, MissingSchemaAction.Ignore)
                dsMain.Tables("SOBULKCOMBODTL").Merge(dsTemp.Tables(3), False, MissingSchemaAction.Ignore)

                If dsMain.Tables("SALESORDERHDR").Rows.Count > 0 Then
                    '----- Fill Details from CashMemo Header -----
                    'Dim strSumNetAmount As String = dsMain.Tables("CASHMEMODtl").Compute("SUM(NETAMOUNT)", "").ToString()
                    Dim strSumNetAmount As String = dsMain.Tables("SALESORDERHDR").Rows(0)("NETAMT").ToString()
                    CtrlCashSummary1.CtrllblNetAmt.Text = FormatNumber(CDbl(IIf(strSumNetAmount <> "", strSumNetAmount, 0)), 2)

                    'Dim strSumTotalDiscount As String = dsMain.Tables("CASHMEMODtl").Compute("SUM(TOTALDISCOUNT)", "").ToString()
                    Dim strSumTotalDiscount As String = dsMain.Tables("SALESORDERHDR").Rows(0)("TOTALDISCOUNT").ToString()
                    CtrlCashSummary1.CtrllblDiscAmt.Text = FormatNumber(CDbl(IIf(strSumTotalDiscount <> "", strSumTotalDiscount, 0)), 2)

                    'Dim strSumGrossAmt As String = dsMain.Tables("CASHMEMODtl").Compute("SUM(GROSSAMT)", "").ToString()
                    Dim strSumGrossAmt As String = dsMain.Tables("SALESORDERHDR").Rows(0)("GrossAmt").ToString()
                    CtrlCashSummary1.CtrllblGrossAmt.Text = FormatNumber(CDbl(IIf(strSumGrossAmt <> "", strSumGrossAmt, 0)), 2)

                    'Dim strSumTax As String = dsMain.Tables("CASHMEMOHDR").Rows(0)("TaxAmount").ToString()
                    Dim strSumTax As String = dsMain.Tables("SALESORDERDTL").Compute("SUM(TotalTaxAmount)", "").ToString()
                    CtrlCashSummary1.CtrllblTaxAmt.Text = FormatNumber(CDbl(IIf(strSumTax <> "", strSumTax, 0)), 2)

                    Dim strSumAmtPaid As String = dsMain.Tables("SALESORDERHDR").Rows(0)("AmountPaid").ToString()
                    CtrlCashSummary1.CtrlLabelTxt6.Text = strSumAmtPaid

                    Dim strSumAmtDue As String = dsMain.Tables("SALESORDERHDR").Rows(0)("AmountDue").ToString()
                    CtrlCashSummary1.CtrlLabelTxt7.Text = strSumAmtDue

                    Dim strSumItems As String = dsMain.Tables("SALESORDERHDR").Rows(0)("TotalItems").ToString()
                    lblTotalItemsValue.Text = FormatNumber(CDbl(IIf(strSumItems <> "", strSumItems, 0)), 2)

                    CtrlCashSummary1.CtrllblNetAmt.TextAlign = ContentAlignment.MiddleRight
                    CtrlCashSummary1.CtrllblTaxAmt.TextAlign = ContentAlignment.MiddleRight

                    '----- Fill Details from CashMemo Details -----
                    '--- First Update the combo article desc 
                    If dsMain.Tables("SOBULKCOMBODTL").Rows.Count > 0 Then
                        For index = 0 To dsMain.Tables("SALESORDERDTL").Rows.Count - 1

                            Dim RowFilter As String = "  SaleOrderNumber='" & dsMain.Tables("SALESORDERDTL").Rows(index)("SaleOrderNumber").ToString & _
                                                      "' AND BillLineNo ='" & dsMain.Tables("SALESORDERDTL").Rows(index)("BillLineNo") & _
                                                      "' AND siteCode='" & dsMain.Tables("SALESORDERDTL").Rows(index)("sitecode") & "'"

                            Dim dvresult As New DataView(dsMain.Tables("SOBULKCOMBODTL"), RowFilter, "", DataViewRowState.CurrentRows)

                            If dvresult.ToTable.Rows.Count > 0 Then
                                Dim articleDescriptionDictionary As New Dictionary(Of String, Integer)
                                articleDescriptionDictionary.Add(ObjclsCommon.GetArticleDescription(dsMain.Tables("SALESORDERDTL").Rows(index)("ARTICLECODE")), 0)

                                GetArticleDecriptionDictionary(articleDescriptionDictionary, dvresult.ToTable)
                                dsMain.Tables("SALESORDERDTL").Rows(index)("DISCRIPTION") = GetMultilinedString(articleDescriptionDictionary)
                            End If
                        Next
                    End If

                    
                    grdCashMemoDetails.DataSource = dsMain.Tables("SALESORDERDTL")
                    Call CashMemoDetailsGridColumnSettings()
                    '----- Fill Details from CashMemo Reciept -----
                    Dim strPaymentType As String = String.Empty
                    Dim dtPaymentTtpeUnique As New DataTable
                    dtPaymentTtpeUnique = dsMain.Tables("SALESORDERRECEIPT").DefaultView.ToTable(True, "TenderHeadCode")
                    'For index = 0 To dsMain.Tables("SALESORDERRECEIPT").Rows.Count - 1

                    '    strPaymentType &= dsMain.Tables("SALESORDERRECEIPT").Rows(index)("TenderHeadCode").ToString() & " " & vbCrLf
                    'Next
                    For index = 0 To dtPaymentTtpeUnique.Rows.Count - 1
                        strPaymentType &= dtPaymentTtpeUnique.Rows(index)("TenderHeadCode").ToString() & " " & vbCrLf
                    Next
                    lblPaymentsModeValue.Text = strPaymentType
                Else
                    '----- Empty details 
                    CtrlCashSummary1.CtrllblNetAmt.Text = String.Empty
                    CtrlCashSummary1.CtrllblDiscAmt.Text = String.Empty
                    CtrlCashSummary1.CtrllblGrossAmt.Text = String.Empty
                    CtrlCashSummary1.CtrllblTaxAmt.Text = String.Empty
                    CtrlCashSummary1.CtrlLabelTxt6.Text = String.Empty
                    CtrlCashSummary1.CtrlLabelTxt7.Text = String.Empty
                    lblTotalItemsValue.Text = String.Empty
                    '----- Fill Details from CashMemo Details -----
                    '    grdCashMemoDetails.DataSource = dsMain.Tables("SALESORDERDTL")
                    'Call CashMemoDetailsGridColumnSettings()
                    '----- Fill Details from CashMemo Reciept -----
                    lblPaymentsModeValue.Text = String.Empty
                End If
            End If
            grdCashMemoDetails.AutoSizeRows()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Private Sub GetArticleDecriptionDictionary(ByRef articleDescriptionDictionary As Dictionary(Of String, Integer), ByRef dataTable As DataTable)
        Try
            For Each Row In dataTable.Rows
                If articleDescriptionDictionary.ContainsKey(Row("DISCRIPTION")) Then
                    articleDescriptionDictionary(Row("DISCRIPTION")) = articleDescriptionDictionary(Row("DISCRIPTION")) + Row("Quantity")
                Else
                    articleDescriptionDictionary.Add(Row("DISCRIPTION"), Row("Quantity"))
                End If
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
     
End Class