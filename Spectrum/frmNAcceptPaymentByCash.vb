Imports SpectrumPrint

Public Class frmNAcceptPaymentByCash
    Private _customerpay As Double
    Public WriteOnly Property CustomerWantPay()
        Set(ByVal value)
            _customerpay = value
        End Set
    End Property
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
    Private Property _altWasPressed As Boolean = False
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
    Private _decTotalCustomerPadiAmount As Decimal
    Public Property TotalCustomerPadiAmount() As Decimal
        Get
            Return _decTotalCustomerPadiAmount
        End Get
        Set(ByVal value As Decimal)
            _decTotalCustomerPadiAmount = value
        End Set
    End Property

    Private _decTotalMinimumAmount As Decimal
    Public Property TotalMinimumAmount() As Decimal
        Get
            Return _decTotalMinimumAmount
        End Get
        Set(ByVal value As Decimal)
            _decTotalMinimumAmount = value
        End Set
    End Property


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

    Private _strGiftReceiptMessage As String
    Public Property GiftReceiptMessage() As String
        Get
            Return _strGiftReceiptMessage
        End Get
        Set(ByVal value As String)
            _strGiftReceiptMessage = value
        End Set
    End Property


    ' ''' <summary>
    ' ''' Reciept summary 
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns>DataSet</returns>
    ' ''' <remarks>ReadOnly</remarks>
    Protected dsRecieptType As New DataSet()
    Public ReadOnly Property ReciptTotalAmount() As DataSet
        Get
            Return dsRecieptType
        End Get
    End Property

    Public _IsCashierPromoSelected As Boolean
    Private Sub frmNAcceptPaymentByCash_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F10 Then
                btnSave_Click(btnSave, New System.EventArgs)
            ElseIf e.KeyCode = Keys.F11 Then
                btnGift_Click(btnGift, New System.EventArgs)
            ElseIf e.KeyCode = Keys.Enter Then
                btnSave_Click(btnSave, New System.EventArgs)
            End If
        Catch ex As Exception
        End Try
    End Sub




    Private Sub frmNAcceptPaymentByCash_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not TotalBillAmount > Decimal.Zero Then
                'ShowMessage(getValueByKey("ACP019"), "ACP019 - " & getValueByKey("CLAE04"))
                ShowMessage(getValueByKey("BL073"), "BL073 - " & getValueByKey("CLAE04"))
                Me.Close()
            End If

            lblBillAmount.Text = _decTotalBillAmount  'CurrencyFormat(TotalBillAmount) commented by rama ranjan on 05-oct-2009

            If DocumentType = "SO" Then
                LbMinAmount.Visible = True
                txtMinAmount.Visible = True
                txtMinAmount.Text = TotalMinimumAmount

            Else
                LbMinAmount.Visible = False
                txtMinAmount.Visible = False
            End If
            If _customerpay > 0 Then
                txtCalCollectAmount.Text = _customerpay
            Else
                txtCalCollectAmount.Text = TotalBillAmount
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
        SetCulture(Me, Me.Name)
        Me.btnSave.Text = "Print (F10) "
        Me.btnGift.Text = "Gift Print (F11)"
        Me.btnCancle.Text = "Cancel"
        Me.ControlBox = ShowIcon.FalseString
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If ValidateEntries() Then
                'Try
                '    If clsAdmin.IsCashDrawer Then '---Code added by Mahesh for checking Cash Drawer is available or not
                '        Dim cA4Print As New clsA4Print
                '        cA4Print.OperateDevice("CashDrawer", CDflag:=clsDefaultConfiguration.CDBuildCode)
                '    End If
                'Catch ex As Exception

                'End Try
                TotalCustomerPadiAmount = TotalCollectAmount
                If IsAmountReturnTocustomer() Then
                    IsCancelAcceptPayment = False
                    _Actiontype = My.Resources.AcceptPaymentActionTypeSave.ToString()
                    If DocumentType = "SO" Then
                        If _IsCashierPromoSelected = True AndAlso (String.IsNullOrEmpty(txtRemark.Text) Or HasAlphaNumericChar(txtRemark.Text) = False) Then
                            ShowMessage(getValueByKey("frmnacceptpaymentbycash.txtremarkvalid"), getValueByKey("CLAE04"))
                            IsCancelAcceptPayment = True
                        ElseIf (PaymentTransactionByShortCutForms(TotalCollectAmount, SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cash.ToString(), SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cash, dsRecieptType)) Then
                            Me.Close()
                        Else
                            ShowMessage(getValueByKey("ACPBCS01"), "ACPBCS01 - " & getValueByKey("CLAE05"))
                        End If
                    Else
                        If _IsCashierPromoSelected = True AndAlso (String.IsNullOrEmpty(txtRemark.Text) Or HasAlphaNumericChar(txtRemark.Text) = False) Then
                            ShowMessage(getValueByKey("frmnacceptpaymentbycash.txtremarkvalid"), getValueByKey("CLAE04"))
                            IsCancelAcceptPayment = True
                        ElseIf (PaymentTransactionByShortCutForms(TotalBillAmount, SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cash.ToString(), SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cash, dsRecieptType)) Then
                            Me.Close()
                        Else
                            ShowMessage(getValueByKey("ACPBCS01"), "ACPBCS01 - " & getValueByKey("CLAE05"))
                        End If
                    End If


                Else
                    ShowMessage(getValueByKey("ACPBCS02"), "ACPBCS02 - " & getValueByKey("CLAE05"))
                End If


            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnGift_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGift.Click
        Try
            If ValidateEntries() Then
                If IsAmountReturnTocustomer() Then
                    Try
                        If clsAdmin.IsCashDrawer Then '---Code added by Mahesh for checking Cash Drawer is available or not
                            Dim cA4Print As New clsA4Print
                            cA4Print.OperateDevice("CashDrawer", CDflag:=clsDefaultConfiguration.CDBuildCode)
                        End If


                    Catch ex As Exception

                    End Try
                    IsCancelAcceptPayment = False
                    _Actiontype = My.Resources.AcceptPaymentActionTypeGift.ToString()
                    If DocumentType = "SO" Then
                        If _IsCashierPromoSelected = True AndAlso (String.IsNullOrEmpty(txtRemark.Text) Or HasAlphaNumericChar(txtRemark.Text) = False) Then
                            ShowMessage(getValueByKey("frmnacceptpaymentbycash.txtremarkvalid"), getValueByKey("CLAE04"))
                            IsCancelAcceptPayment = True
                        ElseIf (PaymentTransactionByShortCutForms(TotalCollectAmount, SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cash.ToString(), SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cash, dsRecieptType)) Then
                            GiftReceiptMessage = GetGiftMessage()
                            If GiftReceiptMessage = String.Empty Then
                                IsCancelAcceptPayment = True
                                Exit Sub
                            End If
                            Me.Close()
                        Else
                            ShowMessage(getValueByKey("ACPBCS01"), "ACPBCS01 - " & getValueByKey("CLAE05"))
                        End If
                    Else
                        If _IsCashierPromoSelected = True AndAlso (String.IsNullOrEmpty(txtRemark.Text) Or HasAlphaNumericChar(txtRemark.Text) = False) Then
                            ShowMessage(getValueByKey("frmnacceptpaymentbycash.txtremarkvalid"), getValueByKey("CLAE04"))
                            IsCancelAcceptPayment = True
                        ElseIf (PaymentTransactionByShortCutForms(TotalBillAmount, SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cash.ToString(), SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cash, dsRecieptType)) Then
                            GiftReceiptMessage = GetGiftMessage()
                            If GiftReceiptMessage = String.Empty Then
                                IsCancelAcceptPayment = True
                                dsRecieptType = New DataSet()
                                Exit Sub
                            End If
                            Me.Close()
                        Else
                            ShowMessage(getValueByKey("ACPBCS01"), "ACPBCS01 - " & getValueByKey("CLAE05"))
                        End If
                    End If

                Else
                    ShowMessage(getValueByKey("ACPBCS02"), "ACPBCS02 - " & getValueByKey("CLAE05"))
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function IsAmountReturnTocustomer() As Boolean
        Try
            If _customerpay > 0 Then
                If TotalCollectAmount > _customerpay Then
                    Dim ReturnAmt As Decimal
                    ReturnAmt = TotalCollectAmount - _customerpay
                    Dim strshowmsg As String = ""
                    ' Commented by Rama ranjan on 05-oct-2009
                    'strshowmsg = strshowmsg & "Customer Paid         " & CurrencyFormat(TotalCollectAmount) & vbLf
                    'strshowmsg = strshowmsg & "Return to Customer    " & CurrencyFormat(ReturnAmt) & "  Amount" & vbLf

                    'Commented by Rohit on 02nd Dec
                    'strshowmsg = getValueByKey("ACPBCS03") & " " & TotalBillAmount & vbLf
                    'strshowmsg = strshowmsg & getValueByKey("ACPBCS04") & " " & TotalCollectAmount & vbLf
                    'strshowmsg = strshowmsg & getValueByKey("ACPBCS05") & " " & ReturnAmt & " " & getValueByKey("ACPBCS06") & vbLf
                    'ShowMessage(strshowmsg, getValueByKey("CLAE04"), True)
                    '''getValueByKey("ACPBCS07")
                    ShowMessage(String.Format(getValueByKey("ACPBCS07"), TotalBillAmount, TotalCollectAmount, ReturnAmt, clsAdmin.CurrencyCode), "ACPBCS07 - " & getValueByKey("CLAE04"))

                    txtCalCollectAmount.Text = CDbl(txtCalCollectAmount.Text) - ReturnAmt
                    Return True
                End If
            End If

            If (TotalCollectAmount > TotalBillAmount) Then
                Dim ReturnAmt As Decimal
                ReturnAmt = TotalCollectAmount - CDbl(TotalBillAmount)
                Dim strshowmsg As String = ""

                ' Commented by Rama ranjan on 05-oct-2009
                'strshowmsg = strshowmsg & "Customer Paid         " & CurrencyFormat(TotalCollectAmount) & vbLf
                'strshowmsg = strshowmsg & "Return to Customer    " & CurrencyFormat(ReturnAmt) & "  Amount" & vbLf

                'Commented by Rohit on 02nd Dec
                'strshowmsg = getValueByKey("ACPBCS03") & " " & TotalBillAmount & vbLf
                'strshowmsg = strshowmsg & getValueByKey("ACPBCS04") & " " & TotalCollectAmount & vbLf
                'strshowmsg = strshowmsg & getValueByKey("ACPBCS05") & " " & ReturnAmt & " " & getValueByKey("ACPBCS06") & vbLf
                'ShowMessage(strshowmsg, getValueByKey("CLAE04"), True)

                ShowMessage(True, String.Format(getValueByKey("ACPBCS07"), TotalBillAmount, TotalCollectAmount, ReturnAmt, clsAdmin.CurrencyCode), "ACPBCS07 - " & getValueByKey("CLAE04"))
                txtCalCollectAmount.Text = CDbl(txtCalCollectAmount.Text) - ReturnAmt

                Return True
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try


    End Function


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
                If TotalCollectAmount < TotalBillAmount AndAlso TotalCollectAmount < TotalMinimumAmount Then
                    ShowMessage(getValueByKey("CM032"), "CM032 - " & getValueByKey("CLAE04"))
                    Return False
                End If
            Else
                If TotalCollectAmount < TotalBillAmount Then
                    ShowMessage(getValueByKey("CM032"), "CM032 - " & getValueByKey("CLAE04"))
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Public Sub New(Optional ByVal vDocumentType As String = "")

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _DocumentType = vDocumentType
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Private Sub txtRemark_Click(sender As Object, e As System.EventArgs) Handles txtRemark.Click
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub

    Private Sub txtRemark_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtRemark.KeyUp
        Try
            If e.Alt AndAlso (e.KeyCode = Keys.D0 Or e.KeyCode = Keys.D1 Or e.KeyCode = Keys.D2 _
                       Or e.KeyCode = Keys.D3 Or e.KeyCode = Keys.D4 Or e.KeyCode = Keys.D5 _
                       Or e.KeyCode = Keys.D6 Or e.KeyCode = Keys.D7 Or e.KeyCode = Keys.D8 _
                       Or e.KeyCode = Keys.D9 Or e.KeyCode = Keys.NumPad0 _
                       Or e.KeyCode = Keys.NumPad1 Or e.KeyCode = Keys.NumPad2 Or e.KeyCode = Keys.NumPad3 _
                       Or e.KeyCode = Keys.NumPad4 Or e.KeyCode = Keys.NumPad5 Or e.KeyCode = Keys.NumPad6 _
                       Or e.KeyCode = Keys.NumPad7 Or e.KeyCode = Keys.NumPad8 Or e.KeyCode = Keys.NumPad9) Then

                _altWasPressed = True
                'Else

                '    Dim ax As New ApplicationException(e.Alt.ToString & "/" & e.KeyCode.ToString)
                '    LogException(ax)
                '    If test = True Then
                '        MsgBox("testit")
                '    End If

                '    If e.Alt.ToString = "False" AndAlso e.KeyCode.ToString = "Menu" Then
                '        test = True
                '    End If


            End If
        Catch ex As Exception

        End Try
    End Sub
    Dim test As Boolean = False
    Private Sub txtRemark_TextChanged(sender As Object, e As System.EventArgs) Handles txtRemark.TextChanged
        Try
            Dim textEnd As String = txtRemark.Text.Substring(txtRemark.SelectionStart - 1, 1)
            If textEnd = "☺" Or textEnd = "☻" Or textEnd = "♥" Or textEnd = "♦" _
                                        Or textEnd = "♣" Or textEnd = "♠" Or textEnd = "•" Or textEnd = "◘" Or textEnd = "○" Or textEnd = "§" Or textEnd = "╚" Or textEnd = "▲" Or textEnd = "ä" Or textEnd = "╤" Or textEnd = "♀" Then

                _altWasPressed = True
            End If

            If (_altWasPressed) Then
                ' remove the added character
                Dim textBox = CType(sender, RichTextBox)
                Dim caretPos = txtRemark.SelectionStart
                If caretPos = 0 Then Exit Sub
                Dim text = txtRemark.Text
                Dim textStart = text.Substring(0, caretPos - 1)
                If (caretPos <= text.Length) Then
                    textEnd = text.Substring(caretPos, text.Length - caretPos)
                    txtRemark.Text = textStart + textEnd
                    txtRemark.SelectionStart = caretPos - 1
                    ' Dim ax As New ApplicationException(text & "/" & textStart & "/" & caretPos & "/" & textEnd & "/" & Me.Text)
                    'LogException(ax)
                    _altWasPressed = False
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' theme change function
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Themechange() As String
        Me.BackgroundColor = Color.FromArgb(167, 167, 167)
        Me.Panel1.Dock = DockStyle.None
        Me.Panel1.BackColor = Color.White
        ' Panel1.Location = New Point(0, 132)
        Panel1.Location = New Point(0, 165)
        Panel1.Size = New Size(620, 73)
        'Label1
        '
        Me.Label1.BackColor = Color.FromArgb(0, 107, 163)
        Label1.AutoSize = False
        Label1.Size = New Size(157, 27)
        Label1.ForeColor = Color.White
        ' Label1.BorderStyle = BorderStyle.FixedSingle

        lblBillAmount.BackColor = Color.White
        lblBillAmount.ForeColor = Color.Black

        lblBillAmount.MinimumSize = New Size(0, 27)
        lblBillAmount.Location = New Point(168, 12)

        Me.LbMinAmount.BackColor = Color.FromArgb(0, 107, 163)
        LbMinAmount.AutoSize = False
        LbMinAmount.Size = New Size(157, 27)
        LbMinAmount.ForeColor = Color.White
        LbMinAmount.Location = New Point(12, 43)
        ' Label1.BorderStyle = BorderStyle.FixedSingle

        txtMinAmount.BackColor = Color.White
        txtMinAmount.ForeColor = Color.Black
        '  txtMinAmount.AutoSize = False
        txtMinAmount.Location = New Point(168, 43)
        txtMinAmount.MinimumSize = New Size(0, 27)




        'LbMinAmount

        Me.LbMinAmount.BackColor = Color.Transparent

        'Label2
        '
        Me.Label2.BackColor = Color.FromArgb(212, 212, 212)
        ' Label2.BorderStyle = BorderStyle.FixedSingle
        Label2.AutoSize = False
        Label2.Size = New Size(157, 50)
        Label2.Location = New Point(12, 105)
        txtRemark.Location = New Point(168, 105)

        'Label3
        '
        Me.Label3.BackColor = Color.FromArgb(212, 212, 212)
        'Label3.BorderStyle = BorderStyle.FixedSingle
        Label3.AutoSize = False
        Label3.Size = New Size(157, 26)
        Label3.Location = New Point(13, 75)

        txtCalCollectAmount.Location = New Point(170, 75)
        'btnSave
        '
        Me.btnSave.Dock = DockStyle.None
        Me.btnSave.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnSave.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnSave.Font = New Font("Neo Sans", 11, FontStyle.Bold)
        'Me.btnSave.Location = New System.Drawing.Point(68, 3)
        'Me.btnSave.Size = New System.Drawing.Size(179, 51)

        Me.btnSave.Location = New System.Drawing.Point(30, 3)
        Me.btnSave.Size = New System.Drawing.Size(130, 54)
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
        Me.btnGift.Location = New System.Drawing.Point(170, 3)
        Me.btnGift.Size = New System.Drawing.Size(130, 54)
        Me.btnGift.BringToFront()
        Me.btnGift.Image = Nothing
        Me.btnGift.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnGift.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnGift.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnGift.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnGift.FlatStyle = FlatStyle.Flat
        Me.btnGift.FlatAppearance.BorderSize = 0
        'Me.btnGift.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)



        'btncancle
        Me.btnCancle.Dock = DockStyle.None
        Me.btnCancle.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnCancle.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnCancle.Font = New Font("Neo Sans", 11, FontStyle.Bold)
        Me.btnCancle.Location = New System.Drawing.Point(310, 3)
        Me.btnCancle.Size = New System.Drawing.Size(130, 54)
        Me.btnCancle.BringToFront()
        Me.btnCancle.Image = Nothing
        Me.btnCancle.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCancle.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnCancle.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnCancle.FlatStyle = FlatStyle.Flat
        Me.btnCancle.FlatAppearance.BorderSize = 0
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

    Private Sub btnCancle_Click(sender As Object, e As EventArgs) Handles btnCancle.Click
        Me.Close()
        Exit Sub
    End Sub
End Class
