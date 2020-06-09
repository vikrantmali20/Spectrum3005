Imports SpectrumPrint
Imports SpectrumBL
Imports SpectrumBL.clsAcceptPayment
Public Class frmNCheckPayment
    Dim _CheckDate As DateTime
    Dim _CheckNo, _MicrNo, _BankName As String
    Dim _Amount, _billAmt As Double
    Dim _Actiontype As String
    Private _IsCancelAcceptPayment As Boolean = True
    Private objBLLAcceptPayment As New clsAcceptPayment
    Public Property IsCancelAcceptPayment() As Boolean
        Get
            Return _IsCancelAcceptPayment
        End Get
        Set(ByVal value As Boolean)
            _IsCancelAcceptPayment = value
        End Set
    End Property

    Dim _DocumentType As String
    Public Property DocumentType() As String
        Get
            Return _DocumentType
        End Get
        Set(ByVal value As String)
            _DocumentType = value
        End Set
    End Property

    Private _decTotalMinAmount As Decimal
    Public Property TotalMinAmount() As Decimal
        Get
            Return _decTotalMinAmount
        End Get
        Set(ByVal value As Decimal)
            _decTotalMinAmount = value
        End Set
    End Property

    Public ReadOnly Property Action() As String
        Get
            Return _Actiontype
        End Get
    End Property
    Public ReadOnly Property CheckDate() As DateTime
        Get
            Return _CheckDate
        End Get
    End Property
    Public ReadOnly Property CheckNo() As String
        Get
            Return _CheckNo
        End Get
    End Property
    Public ReadOnly Property MicrNo() As String
        Get
            Return _MicrNo
        End Get
    End Property
    Public ReadOnly Property BankName() As String
        Get
            Return _BankName
        End Get
    End Property
    Public ReadOnly Property CheckAmount() As Double
        Get
            Return _Amount
        End Get
    End Property
    Dim _CollectAmount As Double
    Public Property CollectAmount() As Double
        Get
            Return _CollectAmount
        End Get
        Set(ByVal value As Double)
            _CollectAmount = value
        End Set
    End Property
    ''' <summary>
    ''' Reciept summary 
    ''' </summary>
    ''' <value></value>
    ''' <returns>DataSet</returns>
    ''' <remarks>ReadOnly</remarks>
    Protected dsRecieptType As New DataSet()
    Public ReadOnly Property ReciptTotalAmount() As DataSet
        Get
            Return dsRecieptType
        End Get
    End Property
    Public WriteOnly Property BillAmount() As Double
        Set(ByVal value As Double)
            _billAmt = value
        End Set

    End Property
    Private _strGiftReceiptMessage As String
    Public Property GiftReceiptMessage() As String
        Get
            Return _strGiftReceiptMessage
        End Get
        Set(ByVal value As String)
            _strGiftReceiptMessage = value
        End Set
    End Property
    Private Sub btnGift_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGift.Click
        If ValidateAll() = True Then
            Try
                Dim cA4Print As New clsA4Print
                cA4Print.OperateDevice("CashDrawer", CDflag:=clsDefaultConfiguration.CDBuildCode)

            Catch ex As Exception

            End Try
            _IsCancelAcceptPayment = False
            _Actiontype = My.Resources.AcceptPaymentActionTypeGift.ToString()
            AssignValue()
            If DocumentType = "SO" Then
                If (PaymentTransactionByShortCutForms(CDbl(txtCollectAmount.Text), SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cheque.ToString(), SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cheque.ToString(), dsRecieptType)) Then
                    GiftReceiptMessage = GetGiftMessage()
                    If GiftReceiptMessage = String.Empty Then
                        IsCancelAcceptPayment = True
                        Exit Sub
                    End If
                    Me.Close()
                Else
                    ShowMessage(getValueByKey("CHKP01"), "CHKP01 - " & getValueByKey("CLAE04"))
                End If
            Else
                If (PaymentTransactionByShortCutForms(_billAmt, SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cheque.ToString(), SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cheque.ToString(), dsRecieptType)) Then
                    GiftReceiptMessage = GetGiftMessage()
                    If GiftReceiptMessage = String.Empty Then
                        IsCancelAcceptPayment = True
                        Exit Sub
                    End If
                    Me.Close()
                Else
                    ShowMessage(getValueByKey("CHKP01"), "CHKP01 - " & getValueByKey("CLAE04"))
                End If
            End If
            CollectAmount = txtCollectAmount.Text
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If ValidateAll() = True Then
            Try
                If clsAdmin.IsCashDrawer Then '---Code added by Mahesh for checking Cash Drawer is available or not
                    Dim cA4Print As New clsA4Print
                    cA4Print.OperateDevice("CashDrawer", CDflag:=clsDefaultConfiguration.CDBuildCode)
                End If
            Catch ex As Exception

            End Try
            _IsCancelAcceptPayment = False
            _Actiontype = My.Resources.AcceptPaymentActionTypeSave.ToString()
            AssignValue()

            If DocumentType = "SO" Then
                If (PaymentTransactionByShortCutForms(CDbl(txtCollectAmount.Text), SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cheque.ToString(), SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cheque.ToString(), dsRecieptType, , , , _CheckNo)) Then
                    Me.Close()
                Else
                    ShowMessage(getValueByKey("CHKP01"), "CHKP01 - " & getValueByKey("CLAE04"))
                End If
            Else
                If (PaymentTransactionByShortCutForms(_billAmt, SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cheque.ToString(), SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cheque.ToString(), dsRecieptType, , , , _CheckNo)) Then
                    Me.Close()
                Else
                    ShowMessage(getValueByKey("CHKP01"), "CHKP01 - " & getValueByKey("CLAE04"))
                End If
            End If
            Dim chkDetails As DataTable = objBLLAcceptPayment.GetCheckDetailsTableStruture()
            Dim dataRow As DataRow
            If dsRecieptType.Tables.Contains("CheckDtls") Then
                dataRow = dsRecieptType.Tables("CheckDtls").NewRow()
            Else
                dataRow = chkDetails.NewRow()
            End If
            dataRow("PayLineNo") = "1"
            dataRow("CheckNo") = txtChequeNo.Text
            dataRow("Amount") = txtCollectAmount.Text
            dataRow("DueDate") = dtpChequeDate.Value
            dataRow("BankName") = cmbBankName.Text
            dataRow("CustomerName") = txtMicrNumber.Text
            dataRow("TelephoneNumber") = String.Empty 'CtrlChequeDetails.txtTelephoneNo.Text
            dataRow("STATUS") = 1

            CollectAmount = txtCollectAmount.Text


            ' dsRecieptType.Rows(dtRecieptType.Rows.Count - 1)("Number") = ctrlChequeDetails.txtChequeNo.Text
            chkDetails.TableName = "CheckDtls"
            If dsRecieptType.Tables.Contains("CheckDtls") Then
                dsRecieptType.Tables("CheckDtls").Rows.Add(dataRow)
            Else
                chkDetails.Rows.Add(dataRow)
                dsRecieptType.Tables.Add(chkDetails)
            End If
        End If
    End Sub
    Private Function ValidateAll() As Boolean
        Try
            'Rakeshg : 29-10-2009 3:10 PM  
            'Add extra validation for Cheque Infomation mandatory or not mandatory.

            If Not (CheckInteger(txtCollectAmount.Text)) Then
                ShowMessage(getValueByKey("ACP002"), "ACP002 - " & getValueByKey("CLAE04"))
                Return False
            End If

            If txtCollectAmount.Text = String.Empty Then
                ShowMessage(getValueByKey("CHKP02"), "CHKP02 - " & getValueByKey("CLAE04"))
                txtCollectAmount.Focus()
                Return False
            End If

            If DocumentType = "SO" Then
                If CDbl(txtCollectAmount.Text) <= CDbl(_billAmt) Then
                    If CDbl(txtCollectAmount.Text) < CDbl(TotalMinAmount) Then
                        ShowMessage(getValueByKey("CHKP07"), "CHKP07 - " & getValueByKey("CLAE04"))
                        txtCollectAmount.Focus()
                        Return False
                    End If
                Else
                    ShowMessage(getValueByKey("CHKP07"), "CHKP07 - " & getValueByKey("CLAE04"))
                    txtCollectAmount.Focus()
                    Return False
                End If
            End If

            If _DocumentType <> "SO" AndAlso CDbl(txtCollectAmount.Text) <> _billAmt Then
                ShowMessage(getValueByKey("CHKP07"), "CHKP07 - " & getValueByKey("CLAE04"))
                txtCollectAmount.Focus()
                Return False
            End If

            If clsDefaultConfiguration.ChequeInfomation Then
                Dim ChequeExpiryDays As Int32 = clsDefaultConfiguration.CheckExpiryMonth
                Dim prviousday As DateTime = DateAdd(DateInterval.Month, ChequeExpiryDays * -1, Now)
                Dim futureday As DateTime = DateAdd(DateInterval.Month, ChequeExpiryDays, Now)

                If String.IsNullOrEmpty(cmbBankName.SelectedValue) Then
                    ShowMessage(getValueByKey("CHKP09"), "CHKP09 - " & getValueByKey("CLAE04"))
                    cmbBankName.Focus()
                    Return False

                ElseIf String.IsNullOrEmpty(txtChequeNo.Text) Then
                    ShowMessage(getValueByKey("CHKP06"), "CHKP06 - " & getValueByKey("CLAE04"))
                    txtChequeNo.Focus()
                    Return False

                ElseIf String.IsNullOrEmpty(txtMicrNumber.Text) Then
                    ShowMessage(getValueByKey("CHKP10"), "CHKP10 - " & getValueByKey("CLAE04"))
                    txtMicrNumber.Focus()
                    Return False

                ElseIf dtpChequeDate.Value IsNot DBNull.Value AndAlso (dtpChequeDate.Value > futureday Or dtpChequeDate.Value < prviousday) Then
                    ShowMessage(getValueByKey("CHKP05"), "CHKP05 - " & getValueByKey("CLAE04"))
                    dtpChequeDate.Focus()
                    Return False

                End If
            End If
              
            Return True
        Catch ex As Exception

        End Try
    End Function
     
    Private Sub AssignValue()
        Try
            _Amount = txtCollectAmount.Text
            _BankName = cmbBankName.SelectedValue
            _CheckDate = dtpChequeDate.Value
            _CheckNo = txtChequeNo.Text
            _MicrNo = txtMicrNumber.Text
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub frmNCheckPayment_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F10 Then
                btnSave_Click(btnSave, New System.EventArgs)
            ElseIf e.KeyCode = Keys.F11 Then
                btnGift_Click(btnGift, New System.EventArgs)
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub frmCheckPayment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not _billAmt > Decimal.Zero Then
                'ShowMessage(getValueByKey("ACP019"), "ACP019 - " & getValueByKey("CLAE04"))
                ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                Me.Close()
            End If
            txtBillAmount.Text = _billAmt
            dtpChequeDate.Value = Now.Today

            Try
                Dim clsCommon As New SpectrumBL.clsCommon()
                Dim bankDetails = clsCommon.GetBankDetails(clsAdmin.SiteCode)

                cmbBankName.DataSource = bankDetails
                cmbBankName.DisplayMember = "BankName"
                cmbBankName.ValueMember = "BankAccNo"
                cmbBankName.Splits(0).DisplayColumns(0).Visible = False
            Catch ex As Exception
                LogException(ex)
            End Try

            If DocumentType = "SO" Then
                If clsDefaultConfiguration.IsNewSalesOrder Then
                    txtCollectAmount.Enabled = False 'vipin 29.11.2017
                End If
                lblMinAmount.Visible = True
                txtMinAmount.Visible = True
                txtMinAmount.Text = CurrencyFormat(TotalMinAmount)
                txtCollectAmount.Text = CurrencyFormat(TotalMinAmount)
            Else
                lblMinAmount.Visible = False
                txtMinAmount.Visible = False
            End If
            ' txtCollectAmount.Text = _billAmt
            If clsDefaultConfiguration.IsNewSalesOrder Then  'vipin 22 - 6 - 2017 
                If txtCollectAmount.Text.ToString.Trim = "" Or txtCollectAmount.Text.ToString.Trim = "0" Then
                    txtCollectAmount.Text = _billAmt
                End If
                Me.txtMicrNumber.MaxLength = 9     'vipin
                Me.txtMicrNumber.DataType = GetType(String)
                Me.txtChequeNo.MaxLength = 6     'vipin
                Me.txtChequeNo.DataType = GetType(String)
            Else
                txtCollectAmount.Text = _billAmt
            End If


            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
        SetCulture(Me, Me.Name)
    End Sub

    Public Sub New(Optional ByVal vDocumentType As String = "")

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _DocumentType = vDocumentType
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Public Function Themechange() As String


        Me.BackgroundColor = Color.FromArgb(167, 167, 167)
        Me.chequeLayout.Dock = DockStyle.None
        Me.chequeLayout.RowStyles(0).SizeType = SizeType.Absolute
        Me.chequeLayout.RowStyles(0).Height = 35
        Me.chequeLayout.RowStyles(1).SizeType = SizeType.Absolute
        Me.chequeLayout.RowStyles(1).Height = 28
        Me.chequeLayout.RowStyles(2).SizeType = SizeType.Absolute
        Me.chequeLayout.RowStyles(2).Height = 28
        Me.chequeLayout.RowStyles(3).SizeType = SizeType.Absolute
        Me.chequeLayout.RowStyles(3).Height = 28

        Me.chequeLayout.RowStyles(4).SizeType = SizeType.Absolute
        Me.chequeLayout.RowStyles(4).Height = 28

        Me.chequeLayout.RowStyles(5).SizeType = SizeType.Absolute
        Me.chequeLayout.RowStyles(5).Height = 35
        Me.chequeLayout.ColumnStyles(0).Width = 110
        Me.chequeLayout.Dock = DockStyle.Fill
        Me.chequeLayout.BackColor = Color.FromArgb(167, 167, 167)

        actionPanel.Dock = DockStyle.None

        actionPanel.MaximumSize = New Size(515, 120)
        actionPanel.MinimumSize = New Size(515, 120)
        actionPanel.Size = New Size(515, 120)
        actionPanel.Location = New Point(0, 170)
        actionPanel.RowStyles(0).SizeType = SizeType.Absolute
        actionPanel.RowStyles(0).Height = 190
        actionPanel.BackColor = Color.White
        'actionPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single


        'txtBillAmount
        '
        txtBillAmount.BackColor = Color.White

        txtBillAmount.MaximumSize = New Size(84, 35)
        txtBillAmount.Size = New Size(84, 35)
        Me.txtBillAmount.ForeColor = Color.Black

        'lblTotalAmount
        '
        Me.lblTotalAmount.Dock = DockStyle.None
        Me.lblTotalAmount.ForeColor = Color.White
        Me.lblTotalAmount.BackColor = Color.FromArgb(0, 107, 163)
        Me.lblTotalAmount.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblTotalAmount.MaximumSize = New Size(125, 37)
        Me.lblTotalAmount.MinimumSize = New Size(130, 33)
        Me.lblTotalAmount.Size = New System.Drawing.Size(125, 37)
        lblTotalAmount.BorderStyle = BorderStyle.None
        'Me.lblTotalAmount.Text = "Bill Amount    "

        'lblCollectAmount
        '
        Me.lblCollectAmount.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblTotalAmount.Dock = DockStyle.None
        'Me.lblCollectAmount.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblCollectAmount.Location = New System.Drawing.Point(4, 39)
        Me.lblCollectAmount.MaximumSize = New Size(125, 25)
        Me.lblCollectAmount.MinimumSize = New Size(129, 24)
        Me.lblCollectAmount.Size = New System.Drawing.Size(125, 25)
        lblCollectAmount.BorderStyle = BorderStyle.None
        lblCollectAmount.Margin = New Padding(4, 2, 4, 4)
        'lblBankName
        '
        lblBankName.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblBankName.Dock = DockStyle.None
        'Me.lblBankName.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblBankName.Location = New System.Drawing.Point(4, 80)
        Me.lblBankName.MaximumSize = New Size(125, 25)
        Me.lblBankName.MinimumSize = New Size(129, 24)
        Me.lblBankName.Size = New System.Drawing.Size(125, 25)
        Me.lblBankName.ForeColor = Color.Black
        lblBankName.BorderStyle = BorderStyle.None
        lblBankName.Margin = New Padding(4, 2, 4, 4)

        cmbBankName.Size = New Size(258, 24)
        'lblChequeNo
        '
        lblChequeNo.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblChequeNo.Dock = DockStyle.None
        'Me.lblChequeNo.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblChequeNo.Location = New System.Drawing.Point(4, 80)
        Me.lblChequeNo.MaximumSize = New Size(125, 25)
        Me.lblChequeNo.MinimumSize = New Size(129, 24)
        Me.lblChequeNo.Size = New System.Drawing.Size(125, 25)
        Me.lblChequeNo.ForeColor = Color.Black
        lblChequeNo.BorderStyle = BorderStyle.None
        lblChequeNo.Margin = New Padding(4, 2, 4, 4)
        'lblChequeDate
        '
        lblChequeDate.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblChequeDate.Dock = DockStyle.None
        'Me.lblChequeDate.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblChequeDate.Location = New System.Drawing.Point(4, 80)
        Me.lblChequeDate.MaximumSize = New Size(125, 25)
        Me.lblChequeDate.MinimumSize = New Size(129, 24)
        Me.lblChequeDate.Size = New System.Drawing.Size(125, 25)
        Me.lblChequeDate.ForeColor = Color.Black
        lblChequeDate.BorderStyle = BorderStyle.None
        lblChequeDate.Margin = New Padding(4, 2, 4, 4)
        'lblMicrNumber
        '
        lblMicrNumber.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblMicrNumber.Dock = DockStyle.None
        'Me.lblChequeDate.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblMicrNumber.Location = New System.Drawing.Point(4, 80)
        Me.lblMicrNumber.MaximumSize = New Size(125, 27)
        Me.lblMicrNumber.MinimumSize = New Size(129, 30)
        Me.lblMicrNumber.Size = New System.Drawing.Size(125, 27)
        Me.lblMicrNumber.ForeColor = Color.Black
        lblMicrNumber.BorderStyle = BorderStyle.None
        lblMicrNumber.Margin = New Padding(4, 2, 4, 4)
        'lblBillAmount.BackColor = Color.White
        'lblBillAmount.ForeColor = Color.Black
        'lblBillAmount.MaximumSize = New Size(130, 48)
        'chequeLayout
        '
        '

        'btnSave
        '
        Me.btnSave.Dock = DockStyle.None
        Me.btnSave.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnSave.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnSave.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.btnSave.Location = New System.Drawing.Point(68, 3)
        Me.btnSave.Size = New System.Drawing.Size(179, 51)
        Me.btnSave.BringToFront()
        Me.btnSave.Image = Nothing
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnSave.FlatStyle = FlatStyle.Flat
        Me.btnSave.FlatAppearance.BorderSize = 0
        'Me.btnSave.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        'btnGift
        '
        Me.btnGift.Dock = DockStyle.None
        Me.btnGift.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnGift.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnGift.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.btnGift.Location = New System.Drawing.Point(253, 3)
        Me.btnGift.Size = New System.Drawing.Size(176, 51)
        Me.btnGift.BringToFront()
        Me.btnGift.Image = Nothing
        Me.btnGift.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnGift.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnGift.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnGift.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnGift.FlatStyle = FlatStyle.Flat
        Me.btnGift.FlatAppearance.BorderSize = 0
        'Me.btnGift.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        Return ""
    End Function
End Class