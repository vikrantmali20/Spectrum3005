Imports SpectrumPrint

Public Class frmNAcceptPaymentByCard
    Private _IsCancelAcceptPayment As Boolean = True
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

    ''' <summary>
    '''  TODO:Taking selected option i.e either gift or save 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _Actiontype As String
    Public ReadOnly Property Action() As String
        Get
            Return _Actiontype
        End Get
    End Property

    ''' <summary>
    '''  Total amount to be tendered .
    ''' </summary>
    ''' <remarks></remarks>
    Private _decTotalBillAmount As Decimal
    Public Property TotalBillAmount() As Decimal
        Get
            Return _decTotalBillAmount
        End Get
        Set(ByVal value As Decimal)
            _decTotalBillAmount = value
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


    '------------------------------------------------------------------------
    'added on 12 may - ashma - for Innoviti
    Private _Billno As String
    Public Property Billno() As String
        Get
            Return _Billno
        End Get
        Set(ByVal value As String)
            _Billno = value
        End Set
    End Property
    'added by sagar for innvatii
    Private _resonseInnoviti As Dictionary(Of String, String)
    Public Property resonseInnoviti() As Dictionary(Of String, String)
        Get
            Return _resonseInnoviti
        End Get
        Set(ByVal value As Dictionary(Of String, String))
            _resonseInnoviti = value
        End Set
    End Property
    ''added by khusrao adil on 20-02-2018  for innviti with card functionality point _ natural client
    ' ''---------------------------------
    Private _dtInnoviti As DataTable
    Public Property dtInnoviti() As DataTable
        Get
            Return _dtInnoviti
        End Get
        Set(ByVal value As DataTable)
            _dtInnoviti = value
        End Set
    End Property
    '------------------------------------------------------------------------

    ''' <summary>
    ''' Total Amount received by Cashier 
    ''' </summary>
    ''' <remarks></remarks>
    Private _decTotalCollectAmount As Decimal
    Public ReadOnly Property TotalCollectAmount() As Decimal
        Get
            Try
                If CheckInteger(txtCalCollectAmount.Text) Then
                    _decTotalCollectAmount = CDbl(txtCalCollectAmount.Text)
                    Return _decTotalCollectAmount
                Else
                    _decTotalCollectAmount = Decimal.Zero
                End If
            Catch ex As Exception
                LogException(ex)
            End Try
        End Get
    End Property
    ''' <summary>
    ''' Return Amount to customer 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TotalAmountReturnToCustomer() As Decimal
        Get
            Return Decimal.Subtract(_decTotalCollectAmount, _decTotalBillAmount)
        End Get
    End Property


    Public ReadOnly Property SelectedTenderName() As String
        Get
            Try
                Dim dataRow As DataRowView '= cboSelectCardType.SelectedItem
                If Not dataRow Is Nothing Then
                    Return dataRow.Item("TenderHeadName").ToString()
                Else
                    Return "Nothing"
                End If
            Catch ex As Exception
                LogException(ex)
            End Try
        End Get
    End Property

    Private _CardTenderName As String
    Public Property CardTenderName() As String
        Get
            Return _CardTenderName
        End Get
        Set(ByVal value As String)
            _CardTenderName = value
        End Set
    End Property

    Private _CardTenderCode As String
    Public Property CardTenderCode() As String
        Get
            Return _CardTenderCode
        End Get
        Set(ByVal value As String)
            _CardTenderCode = value
        End Set
    End Property

    Private _CreditCardNumber As String
    Public Property CreditCardNumber() As String
        Get
            Return _CreditCardNumber
        End Get
        Set(ByVal value As String)
            _CreditCardNumber = value
        End Set
    End Property

    Private _CardExpiryDate As DateTime?
    Public Property CardExpiryDate() As DateTime?
        Get
            Return _CardExpiryDate
        End Get
        Set(ByVal value As DateTime?)
            _CardExpiryDate = value
        End Set
    End Property

    Private _BankAccNumber As String
    Public Property BankAccNumber() As String
        Get
            Return _BankAccNumber
        End Get
        Set(ByVal value As String)
            _BankAccNumber = value
        End Set
    End Property



    ''' <summary>
    '''  Validate user entered inputs
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateEntries() As Boolean
        Try
            If Not (CheckInteger(txtCalCollectAmount.Text)) Then
                ShowMessage(getValueByKey("ACP002"), "ACP002 - " & getValueByKey("CLAE04"))
                Return False
            End If

            If DocumentType = "SO" Then
                If TotalCollectAmount <= TotalBillAmount Then
                    If TotalCollectAmount < TotalMinAmount Then
                        'ShowMessage(IIf(getValueByKey("ACP021") = "", getValueByKey("ACPCD01"), getValueByKey("ACP021")), "ACP021 - " & getValueByKey("CLAE04"))
                        ShowMessage(getValueByKey("CHKP07"), "CHKP07 - " & getValueByKey("CLAE04"))
                        Return False
                    End If
                Else
                    ShowMessage(getValueByKey("ACP020"), "ACP020 - " & getValueByKey("CLAE04"))
                    Return False
                End If
            Else
                If Not Decimal.Equals(TotalCollectAmount, TotalBillAmount) Then
                    'MessageBox.Show(getValueByKey("ACP20"), "ACP20")
                    ShowMessage(getValueByKey("ACPCD01"), "ACPCD01 - " & getValueByKey("CLAE04"))
                    Return False
                End If
            End If
            '------------------------------------------------------------------------------------------------
            'added on 15 may - ashma - for Innoviti (for hiding Credit Card No)
            If Not clsDefaultConfiguration.PayFromInnoviti Then
                '-------------------------------------------------------------------------------------
                If clsDefaultConfiguration.CreditCardInfo = False Then
                    If (String.IsNullOrEmpty(txtCardNo.Text.Trim())) Then
                        txtCardNo.Focus()
                        ShowMessage(getValueByKey("ACP006"), "ACP006 - " & getValueByKey("CLAE04"))
                        Return False
                    ElseIf Not (txtCardNo.Text.Length >= 4 AndAlso txtCardNo.Text.Length <= 16) Then
                        txtCardNo.Focus()
                        ShowMessage(getValueByKey("ACP021"), "ACP021 - " & getValueByKey("CLAE04"))
                        Return False
                    Else
                        Return True
                    End If
                Else
                    If cmbBankName.SelectedIndex = -1 Then
                        cmbBankName.Focus()
                        ShowMessage(getValueByKey("CHKP09"), "CHKP09 - " & getValueByKey("CLAE04"))
                        Return False
                    End If

                    If (String.IsNullOrEmpty(txtCardNo.Text)) Then
                        txtCardNo.Focus()
                        ShowMessage(getValueByKey("ACP006"), "ACP006 - " & getValueByKey("CLAE04"))
                        Return False
                        'ElseIf (String.IsNullOrEmpty(ctrlPayCredit.txtSlipNO.Text)) Then
                        '    ctrlPayCredit.txtSlipNO.Focus()
                        '    ShowMessage(getValueByKey("ACP024"), "ACP024 - " & getValueByKey("CLAE04"))
                        'ElseIf (String.IsNullOrEmpty(ctrlPayCredit.txtAuthCode.Text)) Then
                        '    ctrlPayCredit.txtAuthCode.Focus()
                        'ShowMessage(getValueByKey("ACP024"), "ACP024 - " & getValueByKey("CLAE04"))
                    ElseIf Not (txtCardNo.Text.Length >= 4 AndAlso txtCardNo.Text.Length <= 16) Then
                        txtCardNo.Focus()
                        ShowMessage(getValueByKey("ACP021"), "ACP021 - " & getValueByKey("CLAE04"))
                        Return False
                        'ElseIf String.IsNullOrEmpty(dtpExpiryDate.Text) OrElse dtpExpiryDate.Value < clsAdmin.CurrentDate.Date Then
                        '    dtpExpiryDate.Focus()
                        '    ShowMessage(getValueByKey("ACP034"), "ACP034 - " & getValueByKey("CLAE04"))
                        '    Return False
                    Else
                        Return True
                    End If
                End If
                'If Not cboSelectCardType.SelectedIndex > -1 Then
                '    ShowMessage(getValueByKey("ACP017"), "ACP017 - " & getValueByKey("CLAE04"))
                '    Return False
                'End If
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

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


    Public Sub New(Optional ByVal vDocumentType As String = "")

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _DocumentType = vDocumentType
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Private Sub frmNAcceptPaymentByCard_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F10 Then
                btnSave_Click(btnSave, New System.EventArgs)
            ElseIf e.KeyCode = Keys.F11 Then
                btnGift_Click(btnGift, New System.EventArgs)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub frmNAcceptPaymentByCard_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not TotalBillAmount > Decimal.Zero Then

                'ShowMessage(getValueByKey("ACP019"), "ACP019 - " & getValueByKey("CLAE04"))
                ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                Me.Close()
            End If
            'lblBillAmount.Text = CurrencyFormat(TotalBillAmount)
            lblBillAmount.Text = TotalBillAmount
            If DocumentType = "SO" Then
                lblMinAmount.Visible = True
                txtMinAmount.Visible = True
                'txtMinAmount.Text = CurrencyFormat(TotalMinAmount)
                txtMinAmount.Text = TotalMinAmount
                txtCalCollectAmount.Text = TotalMinAmount
            Else
                lblMinAmount.Visible = False
                txtMinAmount.Visible = False
            End If

            Dim objBirthListGlobal As New SpectrumBL.clsBirthListGobal
            Dim strErrorMsg As String = ""
            Dim dtPayment As DataTable = objBirthListGlobal.RetrieveQuery("select * from dbo.MstTender where tendertype='CreditCard'and Positive_Negative='+' and status=1 and sitecode='" & clsAdmin.SiteCode & "' ", strErrorMsg)

            If (dtPayment IsNot Nothing AndAlso dtPayment.Rows.Count > 0) Then
                CardTenderName = dtPayment.Rows(0)("TenderHeadName").ToString()
                CardTenderCode = dtPayment.Rows(0)("TenderHeadCode").ToString()
            End If
            '----- Added By Mahesh Fill Bank Name Combo 
            Dim clsCommon As New SpectrumBL.clsCommon()
            Dim bankDetails = clsCommon.GetBankDetails(clsAdmin.SiteCode)

            If bankDetails IsNot Nothing AndAlso bankDetails.Rows.Count > 0 Then
                cmbBankName.DataSource = bankDetails
                cmbBankName.DisplayMember = "BankName"
                cmbBankName.ValueMember = "BankAccNo"
                cmbBankName.Splits(0).DisplayColumns(0).Visible = False
            End If

            'added by khusrao adil on 26-07-2018
            dtInnoviti = clsCommon.GetInnovitiStruc()
            dtInnoviti.Clear()
            '----------------------------------------------------------------------------------------------
            'added on 15 may - ashma - for Innoviti (for hiding Credit Card No)
            If clsDefaultConfiguration.PayFromInnoviti = True And clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) Then
                lblBankName.Visible = False
                cmbBankName.Visible = False
                lblCardNo.Visible = False
                txtCardNo.Visible = False
                lblExpiryDate.Visible = False
                dtpExpiryDate.Visible = False
                txtCalCollectAmount.Enabled = False
            End If
            '---------------------------------------------------------------------------------------------

            If clsDefaultConfiguration.IsNewSalesOrder Then 'vipin 22 - 6 - 2017
                '  txtCalCollectAmount.Text = TotalBillAmount  
                If txtCalCollectAmount.Text.ToString.Trim = "" Or txtCalCollectAmount.Text.ToString.Trim = "0" Then
                    txtCalCollectAmount.Text = TotalBillAmount
                End If
            Else
                txtCalCollectAmount.Text = TotalBillAmount
            End If

            txtCardNo.Select()
            txtCardNo.Focus()

            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            Me.btnSave.Text = "Print (F10) "
            Me.btnGift.Text = "Gift Print (F11)"
            Me.btnCancle.Text = "Cancel"
            Me.ControlBox = ShowIcon.FalseString
        Catch ex As Exception
            LogException(ex)
        End Try
        SetCulture(Me, Me.Name)
    End Sub
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
        Try
            If Not IsDBNull(dtpExpiryDate.Value) Then
                CardExpiryDate = dtpExpiryDate.Value
            End If
            CreditCardNumber = txtCardNo.Text
            BankAccNumber = cmbBankName.SelectedValue
            If ValidateEntries() Then
                IsCancelAcceptPayment = False
                _Actiontype = My.Resources.AcceptPaymentActionTypeGift.ToString()
                Try
                    If clsAdmin.IsCashDrawer Then '---Code added by Mahesh for checking Cash Drawer is available or not
                        Dim cA4Print As New clsA4Print
                        cA4Print.OperateDevice("CashDrawer", CDflag:=clsDefaultConfiguration.CDBuildCode)
                    End If
                Catch ex As Exception

                End Try
                If DocumentType = "SO" Then
                    If (PaymentTransactionByShortCutForms(TotalCollectAmount, CardTenderCode, SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.CreditCard, dsRecieptType, CardExpiryDate, CreditCardNumber, BankAccNumber)) Then
                        GiftReceiptMessage = GetGiftMessage()
                        If GiftReceiptMessage = String.Empty Then
                            IsCancelAcceptPayment = True
                            Exit Sub
                        End If

                        Me.Close()

                    Else
                        ShowMessage(getValueByKey("ACPCD02"), "ACPCD02 - " & getValueByKey("CLAE05"))
                    End If
                Else
                    If (PaymentTransactionByShortCutForms(TotalBillAmount, CardTenderCode, SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.CreditCard, dsRecieptType, CardExpiryDate, CreditCardNumber, BankAccNumber)) Then
                        GiftReceiptMessage = GetGiftMessage()
                        If GiftReceiptMessage = String.Empty Then
                            IsCancelAcceptPayment = True
                            Exit Sub
                        End If
                        Me.Close()
                    Else
                        ShowMessage(getValueByKey("ACPCD02"), "ACPCD02 - " & getValueByKey("CLAE05"))
                    End If
                End If


            End If

        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Not IsDBNull(dtpExpiryDate.Value) Then
                CardExpiryDate = dtpExpiryDate.Value
            End If
            CreditCardNumber = txtCardNo.Text
            BankAccNumber = cmbBankName.SelectedValue

            '----------------------------------------------------------------------------------------------
            Try
                'added on 12 may - ashma - for Innoviti (If condition)
                If clsDefaultConfiguration.PayFromInnoviti = True And clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) Then
                    If DocumentType.ToUpper = "SO" Then

                    Else
                        Billno = modCommonFunc.GetCashBillno(clsAdmin.TerminalID, clsAdmin.DayOpenDate, clsAdmin.Financialyear)
                    End If

                    Dim responseFromInnov As New Dictionary(Of String, String)
                    responseFromInnov = modCommanFunction.PaymentToCallEDC(Billno, txtCalCollectAmount.Text.Trim)
                    If responseFromInnov Is Nothing Then
                        Dim ex As New Exception(modCommanFunction.InnovitiResponseError.ToString())
                        LogException(ex)
                        ShowMessage("Error in Card payment", "Information")
                        Exit Sub
                    Else
                        resonseInnoviti = New Dictionary(Of String, String)
                        resonseInnoviti = responseFromInnov
                        CreditCardNumber = resonseInnoviti("CardNumber")
                        'added by khusrao adil on 26-07-2018
                        Dim InnvoitiLineNo = 1
                        Dim dr As DataRow = dtInnoviti.NewRow
                        dr("LineNo") = InnvoitiLineNo
                        dr("TenderType") = CardTenderCode
                        dr("BillNo") = Billno
                        dr("AmountTendered") = txtCalCollectAmount.Text.Trim
                        dr("CardNo") = resonseInnoviti("CardNumber")
                        dr("RetrievalReferenceNumber") = resonseInnoviti("RetrievalReferenceNumber")
                        dr("InnvoitiApplicable") = True
                        dtInnoviti.Rows.Add(dr)
                    End If
                End If
            Catch ex As Exception
                LogException(ex)
            End Try
            '-------------------------------------------------------------------------------------------------


            If ValidateEntries() Then
                Try
                    If clsAdmin.IsCashDrawer Then '---Code added by Mahesh for checking Cash Drawer is available or not
                        Dim cA4Print As New clsA4Print
                        cA4Print.OperateDevice("CashDrawer", CDflag:=clsDefaultConfiguration.CDBuildCode)
                    End If


                Catch ex As Exception

                End Try

                IsCancelAcceptPayment = False
                _Actiontype = My.Resources.AcceptPaymentActionTypeSave.ToString()
                If DocumentType = "SO" Then
                    If (PaymentTransactionByShortCutForms(TotalCollectAmount, CardTenderCode, SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.CreditCard, dsRecieptType, CardExpiryDate, CreditCardNumber, BankAccNumber)) Then

                        Me.Close()
                    Else
                        ShowMessage(getValueByKey("ACPCD02"), "ACPCD02 - " & getValueByKey("CLAE05"))
                    End If
                Else
                    If (PaymentTransactionByShortCutForms(TotalBillAmount, CardTenderCode, SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.CreditCard, dsRecieptType, CardExpiryDate, CreditCardNumber, BankAccNumber)) Then

                        Me.Close()
                    Else
                        ShowMessage(getValueByKey("ACPCD02"), "ACPCD02 - " & getValueByKey("CLAE05"))
                    End If
                End If

            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtCardNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtCardNo.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnSave_Click(btnSave, New System.EventArgs)
        End If
    End Sub

    Public Function Themechange() As String

        Me.BackgroundColor = Color.FromArgb(167, 167, 167)
        Me.CardLayout.Dock = DockStyle.None
        CardLayout.Margin = New Padding(0)
        Me.CardLayout.RowStyles(0).SizeType = SizeType.Absolute
        Me.CardLayout.RowStyles(0).Height = 50
        Me.CardLayout.RowStyles(1).SizeType = SizeType.Absolute
        Me.CardLayout.RowStyles(1).Height = 28
        Me.CardLayout.RowStyles(2).SizeType = SizeType.Absolute
        Me.CardLayout.RowStyles(2).Height = 28
        Me.CardLayout.RowStyles(3).SizeType = SizeType.Absolute
        Me.CardLayout.RowStyles(3).Height = 28
        Me.CardLayout.RowStyles(4).SizeType = SizeType.Absolute
        Me.CardLayout.RowStyles(4).Height = 30
        Me.CardLayout.RowStyles(5).SizeType = SizeType.Absolute
        Me.CardLayout.RowStyles(5).Height = 85
        Me.CardLayout.ColumnStyles(0).Width = 110
        Me.CardLayout.Dock = DockStyle.Fill

        'actionPanel
        '
        actionPanel.Dock = DockStyle.None
        actionPanel.Size = New Size(515, 120)
        actionPanel.Location = New Point(0, 170)
        actionPanel.RowStyles(0).SizeType = SizeType.Absolute
        actionPanel.RowStyles(0).Height = 190
        actionPanel.BackColor = Color.White

        'lblBillAmount
        '
        lblBillAmount.BackColor = Color.White
        lblBillAmount.ForeColor = Color.Black
        lblBillAmount.MaximumSize = New Size(130, 48)

        'lblTotalAmount
        Me.lblTotalAmount.Dock = DockStyle.None
        Me.lblTotalAmount.ForeColor = Color.White
        Me.lblTotalAmount.BackColor = Color.FromArgb(0, 107, 163)
        Me.lblTotalAmount.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblTotalAmount.MaximumSize = New Size(137, 48)
        lblTotalAmount.MinimumSize = New System.Drawing.Size(139, 48)
        Me.lblTotalAmount.Size = New System.Drawing.Size(139, 48)
        Me.lblTotalAmount.Text = "Bill Amount    "

        'lblCollectAmount'
        lblCollectAmount.Dock = DockStyle.None
        Me.lblCollectAmount.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblCollectAmount.MaximumSize = New Size(130, 25)
        lblCollectAmount.MinimumSize = New System.Drawing.Size(139, 25)
        Me.lblCollectAmount.Size = New System.Drawing.Size(130, 25)
        Me.lblCollectAmount.Location = New System.Drawing.Point(1, 52)

        txtCalCollectAmount.MinimumSize = New System.Drawing.Size(0, 26)
      
        'lblMinAmount
        '
        lblMinAmount.BackColor = Color.FromArgb(212, 212, 212)
        lblMinAmount.MaximumSize = New Size(157, 27)
        lblMinAmount.MinimumSize = New Size(157, 27)
        lblMinAmount.Size = New Size(157, 27)
        Me.lblMinAmount.ForeColor = Color.White

        'lblBankName
        '
        lblBankName.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblBankName.Dock = DockStyle.None
        Me.lblBankName.Location = New System.Drawing.Point(4, 82)
        Me.lblBankName.MaximumSize = New Size(130, 25)
        Me.lblBankName.MinimumSize = New Size(137, 25)
        Me.lblBankName.Size = New System.Drawing.Size(130, 25)
        Me.lblBankName.ForeColor = Color.Black

        'lblCardNo
        lblCardNo.BackColor = Color.FromArgb(212, 212, 212)

        Me.lblCardNo.Dock = DockStyle.None
        '   Me.lblCardNo.Location = New System.Drawing.Point(10, 110)
        lblCardNo.Margin = New Padding(8, 1, 1, 1)
        Me.lblCardNo.MaximumSize = New Size(130, 25)
        Me.lblCardNo.MinimumSize = New Size(139, 25)
        Me.lblCardNo.Size = New System.Drawing.Size(130, 25)
        Me.lblCardNo.ForeColor = Color.Black

        'lblExpiryDate
        lblExpiryDate.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblExpiryDate.Dock = DockStyle.None
        Me.lblExpiryDate.Location = New System.Drawing.Point(7, 138)
        Me.lblExpiryDate.MaximumSize = New Size(130, 25)
        Me.lblExpiryDate.MinimumSize = New Size(139, 25)
        Me.lblExpiryDate.Size = New System.Drawing.Size(130, 25)
        Me.lblExpiryDate.ForeColor = Color.Black

        'btnSave
        '
        Me.btnSave.Dock = DockStyle.None
        Me.btnSave.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnSave.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnSave.Font = New Font("Neo Sans", 11, FontStyle.Bold)
        'Me.btnSave.Location = New System.Drawing.Point(68, 3)
        'Me.btnSave.Size = New System.Drawing.Size(179, 51)

        Me.btnSave.Location = New System.Drawing.Point(30, 3)
        Me.btnSave.Size = New System.Drawing.Size(140, 51)
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
        Me.btnGift.Font = New Font("Neo Sans", 11, FontStyle.Bold)
        'Me.btnGift.Location = New System.Drawing.Point(253, 3)
        'Me.btnGift.Size = New System.Drawing.Size(176, 51)

        Me.btnGift.Location = New System.Drawing.Point(180, 3)
        Me.btnGift.Size = New System.Drawing.Size(140, 51)
        Me.btnGift.BringToFront()
        Me.btnGift.Image = Nothing
        Me.btnGift.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnGift.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnGift.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnGift.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnGift.FlatStyle = FlatStyle.Flat
        Me.btnGift.FlatAppearance.BorderSize = 0
        'Me.btnGift.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        'btnCancle

        Me.btnCancle.Dock = DockStyle.None
        Me.btnCancle.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnCancle.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnCancle.Font = New Font("Neo Sans", 11, FontStyle.Bold)
        Me.btnCancle.Location = New System.Drawing.Point(330, 3)
        Me.btnCancle.Size = New System.Drawing.Size(140, 51)
        Me.btnCancle.BringToFront()
        Me.btnCancle.Image = Nothing
        Me.btnCancle.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCancle.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnCancle.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnCancle.FlatStyle = FlatStyle.Flat
        Me.btnCancle.FlatAppearance.BorderSize = 0

        txtCalCollectAmount.Margin = New Padding(1, 4, 1, 1)
        cmbBankName.Margin = New Padding(1, 4, 1, 1)
        lblCardNo.Margin = New Padding(1, 4, 1, 1)
        lblExpiryDate.Margin = New Padding(1, 4, 1, 1)
        Return ""
    End Function

    Private Sub txtCalCollectAmount_Click(sender As Object, e As EventArgs) Handles txtCalCollectAmount.Click
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub


    Private Sub txtMinAmount_Click(sender As Object, e As EventArgs) Handles txtMinAmount.Click
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub

    Private Sub dtpExpiryDate_Click(sender As Object, e As EventArgs) Handles dtpExpiryDate.Click
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub
    Private Sub btnCancle_Click(sender As Object, e As EventArgs) Handles btnCancle.Click
        Me.Close()
        Exit Sub
    End Sub
End Class
