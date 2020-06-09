Imports SpectrumBL
Imports SpectrumPrint

Public Class frmViewOrderDetails

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Dim objCM As New clsCashMemo
    Public pSearchCust As String = ""
    Dim dsMain As New DataSet
    Dim dtDocInfo As New DataTable
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

    Private Sub frmViewOrderDetails_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                'If UCase(pSearchCust) = "SEARCH" AndAlso Me.Tag = String.Empty Then
                '    Dim ChildForm As New Spectrum.frmViewOrderDetails
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
                        DocumentNo = IIf(grdDocumentInfo.Item(grdDocumentInfo.Row, EnumDocs.DocNo) Is System.DBNull.Value, "", grdDocumentInfo.Item(grdDocumentInfo.Row, EnumDocs.DocNo))
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

    Private Sub frmViewOrderDetails_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
        End If
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
            'grdDocumentInfo.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow
            'grdDocumentInfo.HighLightRowStyle.ForeColor = Color.WhiteSmoke
            'grdDocumentInfo.HighLightRowStyle.BackColor = Color.Navy

            If grdDocumentInfo.RowCount > 0 Then
                DocumentNo = IIf(grdDocumentInfo.Item(grdDocumentInfo.Row, EnumDocs.DocNo) Is System.DBNull.Value, "", grdDocumentInfo.Item(grdDocumentInfo.Row, EnumDocs.DocNo))
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
            DocumentType = "CashMemo"
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
        DocNo = 1
        DeliveryPerson = 2
        TotalItems = 3
        Amount = 4
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
            grdDocumentInfo.Columns("BillDate").Caption = getValueByKey("frmViewOrderDetails.grdDocumentInfo.BillDate")
            grdDocumentInfo.Columns("CashMemoNo").Caption = getValueByKey("frmViewOrderDetails.grdDocumentInfo.CashMemoNo")
            grdDocumentInfo.Columns("DeliveryPersonID").Caption = getValueByKey("frmViewOrderDetails.grdDocumentInfo.DeliveryPersonID")
            grdDocumentInfo.Columns("TotalItems").Caption = getValueByKey("frmViewOrderDetails.grdDocumentInfo.TotalItems")
            grdDocumentInfo.Columns("NetAmt").Caption = getValueByKey("frmViewOrderDetails.grdDocumentInfo.NetAmt")

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub CashMemoDetailsGridColumnSettings()
        Try
            '      If grdCreditSales.Cols.Count > 0 Then
            Dim displayColumns As String = "DISCRIPTION,Quantity,SellingPrice,TotalDiscPercentage"
            Dim columnsList = displayColumns.ToUpper().Split(",")

            For colIndex = 0 To grdCashMemoDetails.Cols.Count - 1 Step 1
                If columnsList.Contains(grdCashMemoDetails.Cols(colIndex).Name.ToUpper()) Then
                    grdCashMemoDetails.Cols(colIndex).Visible = True
                Else
                    grdCashMemoDetails.Cols(colIndex).Visible = False
                End If
            Next
            grdCashMemoDetails.Cols("DISCRIPTION").Caption = getValueByKey("frmViewOrderDetails.grdCashMemoDetails.DISCRIPTION")
            grdCashMemoDetails.Cols("Quantity").Caption = getValueByKey("frmViewOrderDetails.grdCashMemoDetails.Quantity")
            grdCashMemoDetails.Cols("SellingPrice").Caption = getValueByKey("frmViewOrderDetails.grdCashMemoDetails.SellingPrice")
            grdCashMemoDetails.Cols("TotalDiscPercentage").Caption = getValueByKey("frmViewOrderDetails.grdCashMemoDetails.TotalDiscPercentage")
            
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
            '            grdDocumentsDetails.DataSource = dsMain.Tables("CASHMEMODTL")
            CashSummary.CtrllblDiscAmt.DataBindings.Add("Text", dsMain.Tables("CASHMEMOHDR"), "TOTALDISCOUNT")
            CashSummary.CtrllblGrossAmt.DataBindings.Add("Text", dsMain.Tables("CASHMEMOHDR"), "GROSSAMT")
            CashSummary.CtrllblNetAmt.DataBindings.Add("Text", dsMain.Tables("CASHMEMOHDR"), "NETAMT")

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
            DocumentNo = IIf(grdDocumentInfo.Item(grdDocumentInfo.Row, EnumDocs.DocNo) Is System.DBNull.Value, "", grdDocumentInfo.Item(grdDocumentInfo.Row, EnumDocs.DocNo))
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
                DocumentNo = IIf(grdDocumentInfo.Item(grdDocumentInfo.Row, EnumDocs.DocNo) Is System.DBNull.Value, "", grdDocumentInfo.Item(grdDocumentInfo.Row, EnumDocs.DocNo))
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
            Dim dsTemp As DataSet
            dsTemp = objCM.GetStruc(strCashMemo, strSiteCode)
            dsMain.Clear()
            If Not dsTemp Is Nothing AndAlso dsTemp.Tables.Count > 0 Then
                dsMain.Tables("CASHMEMOHDR").Merge(dsTemp.Tables(0), False, MissingSchemaAction.Ignore)
                dsMain.Tables("CASHMEMODTL").Merge(dsTemp.Tables(1), False, MissingSchemaAction.Ignore)
                dsMain.Tables("CASHMEMORECEIPT").Merge(dsTemp.Tables(2), False, MissingSchemaAction.Ignore)

                If dsMain.Tables("CASHMEMOHDR").Rows.Count > 0 Then
                    '----- Fill Details from CashMemo Header -----
                    'Dim strSumNetAmount As String = dsMain.Tables("CASHMEMODtl").Compute("SUM(NETAMOUNT)", "").ToString()
                    Dim strSumNetAmount As String = dsMain.Tables("CASHMEMOHDR").Rows(0)("NETAMT").ToString()
                    CashSummary.CtrllblNetAmt.Text = FormatNumber(CDbl(IIf(strSumNetAmount <> "", strSumNetAmount, 0)), 2)

                    'Dim strSumTotalDiscount As String = dsMain.Tables("CASHMEMODtl").Compute("SUM(TOTALDISCOUNT)", "").ToString()
                    Dim strSumTotalDiscount As String = dsMain.Tables("CASHMEMOHDR").Rows(0)("TOTALDISCOUNT").ToString()
                    CashSummary.CtrllblDiscAmt.Text = FormatNumber(CDbl(IIf(strSumTotalDiscount <> "", strSumTotalDiscount, 0)), 2)

                    'Dim strSumGrossAmt As String = dsMain.Tables("CASHMEMODtl").Compute("SUM(GROSSAMT)", "").ToString()
                    Dim strSumGrossAmt As String = dsMain.Tables("CASHMEMOHDR").Rows(0)("GrossAmt").ToString()
                    CashSummary.CtrllblGrossAmt.Text = FormatNumber(CDbl(IIf(strSumGrossAmt <> "", strSumGrossAmt, 0)), 2)

                    'Dim strSumTax As String = dsMain.Tables("CASHMEMOHDR").Rows(0)("TaxAmount").ToString()
                    Dim strSumTax As String = dsMain.Tables("CASHMEMODtl").Compute("SUM(TotalTaxAmount)", "").ToString()
                    CashSummary.CtrllblTaxAmt.Text = FormatNumber(CDbl(IIf(strSumTax <> "", strSumTax, 0)), 2)

                    Dim strSumItems As String = dsMain.Tables("CASHMEMOHDR").Rows(0)("TotalItems").ToString()
                    lblTotalItemsValue.Text = FormatNumber(CDbl(IIf(strSumItems <> "", strSumItems, 0)), 2)

                    CashSummary.CtrllblNetAmt.TextAlign = ContentAlignment.MiddleRight
                    CashSummary.CtrllblTaxAmt.TextAlign = ContentAlignment.MiddleRight

                    '----- Fill Details from CashMemo Details -----
                    grdCashMemoDetails.DataSource = dsMain.Tables("CASHMEMODTL")
                    Call CashMemoDetailsGridColumnSettings()
                    '----- Fill Details from CashMemo Reciept -----
                    Dim strPaymentType As String = String.Empty
                    For index = 0 To dsMain.Tables("CASHMEMORECEIPT").Rows.Count - 1
                        strPaymentType &= dsMain.Tables("CASHMEMORECEIPT").Rows(index)("TenderHeadCode").ToString() & " " & vbCrLf
                    Next
                    lblPaymentsModeValue.Text = strPaymentType
                Else
                    '----- Empty details 
                    CashSummary.CtrllblNetAmt.Text = String.Empty
                    CashSummary.CtrllblDiscAmt.Text = String.Empty
                    CashSummary.CtrllblGrossAmt.Text = String.Empty
                    CashSummary.CtrllblTaxAmt.Text = String.Empty
                    lblTotalItemsValue.Text = String.Empty
                    '----- Fill Details from CashMemo Details -----
                    grdCashMemoDetails.DataSource = dsMain.Tables("CASHMEMODTL")
                    Call CashMemoDetailsGridColumnSettings()
                    '----- Fill Details from CashMemo Reciept -----
                    lblPaymentsModeValue.Text = String.Empty
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Function Themechange() As String


        Me.BackgroundColor = Color.FromArgb(76, 76, 76)

        Me.lblCustomerName.ForeColor = Color.White
        Me.lblCustomerNo.ForeColor = Color.White
        Me.lblOrderDetails.ForeColor = Color.White
        Me.lblTotalItems.ForeColor = Color.White
        Me.lblPaymentsMode.ForeColor = Color.White
        Me.lblBalancePoints.ForeColor = Color.White


        Me.lblCustomerNameValue.ForeColor = Color.White
        Me.lblCustomerNoValue.ForeColor = Color.White
        Me.lblBalancePointsValue.ForeColor = Color.White
        Me.lblTotalItemsValue.ForeColor = Color.White
        Me.lblBalancePointsValue.ForeColor = Color.White
        Me.lblPaymentsModeValue.ForeColor = Color.White


        grdDocumentInfo.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Custom
        grdDocumentInfo.Styles(0).BackColor = Color.FromArgb(255, 255, 255)
        grdDocumentInfo.Styles(0).Font = New Font("Neo Sans", 10, FontStyle.Regular)
        grdDocumentInfo.Styles(1).Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdDocumentInfo.Styles(1).BackColor = Color.FromArgb(177, 227, 253)
        grdDocumentInfo.Splits(0).Style.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        grdDocumentInfo.Styles(5).Font = New Font("Neo Sans", 9, FontStyle.Bold)
        grdDocumentInfo.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Custom
        grdDocumentInfo.HighLightRowStyle.BackColor = Color.FromArgb(177, 227, 253)
        grdDocumentInfo.HighLightRowStyle.ForeColor = Color.WhiteSmoke

        'grdCashMemoDetails
        '
        'Me.grdCashMemoDetails.MaximumSize = New Size(1364, 600)
        ' Me.grdCashMemoDetails.Size = New System.Drawing.Size(1364, 600)
        Me.grdCashMemoDetails.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        Me.grdCashMemoDetails.Styles.Highlight.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdCashMemoDetails.Styles.Highlight.ForeColor = Color.Black
        Me.grdCashMemoDetails.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdCashMemoDetails.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        Me.grdCashMemoDetails.Styles.Normal.ForeColor = Color.Black
        Me.grdCashMemoDetails.Rows.MinSize = 26
        Me.grdCashMemoDetails.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        Me.grdCashMemoDetails.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdCashMemoDetails.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdCashMemoDetails.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdCashMemoDetails.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdCashMemoDetails.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdCashMemoDetails.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        Me.grdCashMemoDetails.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdCashMemoDetails.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdCashMemoDetails.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdCashMemoDetails.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)

        CashSummary.CtrlLabel1.ForeColor = Color.White
        CashSummary.CtrlLabel2.ForeColor = Color.White
        CashSummary.CtrlLabel3.ForeColor = Color.White
        CashSummary.CtrlLabel4.ForeColor = Color.White
        Return ""
    End Function


End Class