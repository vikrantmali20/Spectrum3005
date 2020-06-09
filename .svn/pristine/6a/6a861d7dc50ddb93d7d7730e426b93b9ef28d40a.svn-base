Imports SpectrumBL
Imports com.criti.spectrum.encrypt
Imports System.ComponentModel

Public Class frmNHowMuchtoPay
    Public blnAllowtoGoPaymentScreen As Boolean = False
    Public TotalBalAmt As Double = Decimal.Zero

    Private _PickupAmountVisiable As Boolean = True
    '<DefaultValueAttribute(True)>
    Public Property PickupAmountVisiable() As Boolean
        Get
            Return _PickupAmountVisiable
        End Get
        Set(ByVal value As Boolean)
            _PickupAmountVisiable = value
            CtrlTxtPickAmt.Visible = value
            CtrlLblPickAmt.Visible = value
            CtrlBtnPayLessMinAmt.Visible = value
        End Set
    End Property
    Dim _IsNew As Boolean = True
    Public Property IsNew() As Boolean
        Get
            Return _IsNew
        End Get
        Set(ByVal value As Boolean)
            _IsNew = value
        End Set
    End Property
    Private _NetAmount As Double
    Public Property NetAmount() As Double
        Get
            Return _NetAmount
        End Get
        Set(ByVal value As Double)
            _NetAmount = value
        End Set
    End Property
    Private _CreditSales As Double
    Public Property CreditSales() As Double
        Get
            Return _CreditSales
        End Get
        Set(ByVal value As Double)
            _CreditSales = value
        End Set
    End Property
    Private _AmtPaid As Double
    Public Property AmtPaid() As Double
        Get
            Return _AmtPaid
        End Get
        Set(ByVal value As Double)
            _AmtPaid = value

        End Set
    End Property
    Dim _IsEditSalesOrder As Boolean = False
    Public Property IsEditSalesOrder() As Boolean
        Get
            Return _IsEditSalesOrder
        End Get
        Set(ByVal value As Boolean)
            _IsEditSalesOrder = value
        End Set
    End Property

    Private Sub CtrlBtnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlBtnOk.Click
        IsNew = False
        If Not Double.TryParse(ctrlTxtHowMuchPay.Text, 0) Then
            ShowMessage(getValueByKey("HMP008"), "HMP008 - " & getValueByKey("CLAE04"))
            Exit Sub
        End If
        If CDbl(CtrlTxtMinAmt.Text) > CDbl(ctrlTxtHowMuchPay.Text) Then
            ShowMessage(getValueByKey("HMP001"), "HMP001 - " & getValueByKey("CLAE04"))
        ElseIf CDbl(ctrlTxtHowMuchPay.Text) > TotalBalAmt Then
            ShowMessage(getValueByKey("HMP002"), "HMP002 - " & getValueByKey("CLAE04"))
        ElseIf CDbl(CtrlTxtMinAmt.Text) > CDbl(CtrlTxtPickAmt.Text) AndAlso CDbl(CtrlTxtPickAmt.Text) > CDbl(ctrlTxtHowMuchPay.Text) Then
            ShowMessage(getValueByKey("HMP003"), "HMP003 - " & getValueByKey("CLAE04"))
        ElseIf IsEditSalesOrder AndAlso CDbl(ctrlTxtHowMuchPay.Text) > CDbl(NetAmount) - (CDbl(AmtPaid) + CDbl(CreditSales)) Then
            ShowMessage(getValueByKey("HMP013"), "HMP013 - " & getValueByKey("CLAE04"))
        Else
            blnAllowtoGoPaymentScreen = True
            CtrlTxtMinAmt.Text = ctrlTxtHowMuchPay.Text
            Me.Close()
        End If
    End Sub

    Private Sub CtrlBtnPayLessMinAmt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlBtnPayLessMinAmt.Click
        IsNew = False
        If Not Double.TryParse(ctrlTxtHowMuchPay.Text, 0) Then
            ShowMessage(getValueByKey("HMP008"), "HMP008 - " & getValueByKey("CLAE04"))
            Exit Sub
        End If
        If CDbl(CtrlTxtPickAmt.Text) > CDbl(ctrlTxtHowMuchPay.Text) Then
            MsgBox(getValueByKey("HMP001"), MsgBoxStyle.Critical, "HMP001 - " & getValueByKey("CLAE04"))
            Exit Sub
        ElseIf CDbl(ctrlTxtHowMuchPay.Text) > TotalBalAmt Then
            MsgBox(getValueByKey("HMP002"), MsgBoxStyle.Critical, "HMP002 - " & getValueByKey("CLAE04"))
        ElseIf IsEditSalesOrder AndAlso CDbl(ctrlTxtHowMuchPay.Text) > CDbl(NetAmount) - (CDbl(AmtPaid) + CDbl(CreditSales)) Then
            ShowMessage(getValueByKey("HMP013"), "HMP013 - " & getValueByKey("CLAE04"))
        Else
            If clsDefaultConfiguration.IsLessMinAdvAmountAllowed = True Then
                'MinAdvSales  ->Transaction code to allow to pay less then min adv amt
                If CheckAuthorisation(clsAdmin.UserCode, "MinAdvSales") = False Then
                    If MsgBox(getValueByKey("HMP005") & vbLf & getValueByKey("HMP006"), MsgBoxStyle.YesNo, "HMP005 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                        SizUserAuth.Visible = True
                    End If
                Else
                    blnAllowtoGoPaymentScreen = True
                    CtrlTxtMinAmt.Text = ctrlTxtHowMuchPay.Text
                    Me.Close()
                End If
            Else
                MsgBox(getValueByKey("HMP007"), MsgBoxStyle.Critical, "HMP007 - " & getValueByKey("CLAE04"))
            End If
        End If
    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click

        Try
            Dim StrMsg As String = ""
            Dim objLog As New clsLogin()
            Dim dbPassword As String = ""
            If objLog.CheckAuthorizeUser(txtUserId.Text.Trim(), txtPassWord.Text.Trim(), StrMsg, dbPassword, clsAdmin.SiteCode) = True Then
                If dbPassword.Length < 4 Then
                    StrMsg = getValueByKey("UA006")
                    ShowMessage(StrMsg, "UA006 - " & getValueByKey("CLAE04"))
                    Exit Sub
                Else
                    Dim deCrpt As New clsEncrypter
                    If deCrpt.authenticatePassword(txtPassWord.Text.Trim(), dbPassword) Then
                        If CheckAuthorisation(txtUserId.Text.Trim, "MinAdvSales") Then
                            blnAllowtoGoPaymentScreen = True
                            CtrlTxtMinAmt.Text = ctrlTxtHowMuchPay.Text
                            Me.Close()
                        Else
                            blnAllowtoGoPaymentScreen = False
                            ShowMessage(getValueByKey("UA005"), "UA005 - " & getValueByKey("CLAE04"))
                            Exit Sub
                            'ShowMessage("User have no authorisation ", "Information")
                        End If
                    Else
                        blnAllowtoGoPaymentScreen = False
                        StrMsg = getValueByKey("UA006")
                        ShowMessage(StrMsg, "UA006 - " & getValueByKey("CLAE04"))
                    End If
                End If
            Else
                ShowMessage(StrMsg, getValueByKey("CLAE04"))
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub frmNHowMuchtoPay_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CtrlBtnPayLessMinAmt.Visible = clsDefaultConfiguration.IsLessMinAdvAmountAllowed
        If PickupAmountVisiable = False Then
            CtrlBtnPayLessMinAmt.Visible = False
        End If

        SetCulture(Me, Me.Name)

        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
        End If
    End Sub

    Private Sub frmNHowMuchtoPay_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        lblMinAmount.ForeColor = Color.FromArgb(255, 255, 255)
        lblMinAmount.BorderStyle = BorderStyle.None
        lblMinAmount.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblPassWord.ForeColor = Color.FromArgb(255, 255, 255)
        lblPassWord.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblPassWord.BorderStyle = BorderStyle.None

        lblRemarks.ForeColor = Color.FromArgb(255, 255, 255)
        lblRemarks.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblRemarks.BorderStyle = BorderStyle.None

        lblUserid.ForeColor = Color.FromArgb(255, 255, 255)
        lblUserid.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblUserid.BorderStyle = BorderStyle.None


        CtrlLblPickAmt.ForeColor = Color.FromArgb(255, 255, 255)
        CtrlLblPickAmt.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CtrlLblPickAmt.BorderStyle = BorderStyle.None
        CtrlLblPickAmt.Location = New Point(100, 61)
        ctrlLbl_howmuchtoPay.ForeColor = Color.FromArgb(255, 255, 255)
        ctrlLbl_howmuchtoPay.Location = New Point(25, 93)
        ctrlLbl_howmuchtoPay.Size = New Size(205, 15)
        ctrlLbl_howmuchtoPay.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        ctrlLbl_howmuchtoPay.BorderStyle = BorderStyle.None

        cmdOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdOk.Font = New Font("Neo Sans", 7.5, FontStyle.Bold)
        cmdOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdOk.BackColor = Color.FromArgb(0, 107, 163)
        cmdOk.FlatStyle = FlatStyle.Popup
        cmdOk.FlatAppearance.BorderSize = 0
        cmdOk.BackColor = Color.FromArgb(0, 107, 163)
        cmdOk.ForeColor = Color.FromArgb(255, 255, 255)
        cmdOk.TextAlign = ContentAlignment.MiddleCenter


        cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdCancel.BackColor = Color.Transparent
        cmdCancel.BackColor = Color.FromArgb(0, 107, 163)
        cmdCancel.ForeColor = Color.FromArgb(255, 255, 255)
        cmdCancel.Font = New Font("Neo Sans", 7.5, FontStyle.Bold)
        cmdCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdCancel.FlatStyle = FlatStyle.Flat
        cmdCancel.FlatAppearance.BorderSize = 0
        cmdCancel.TextAlign = ContentAlignment.MiddleCenter


        CtrlBtnOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnOk.BackColor = Color.Transparent
        CtrlBtnOk.BackColor = Color.FromArgb(0, 107, 163)
        CtrlBtnOk.ForeColor = Color.FromArgb(255, 255, 255)
        CtrlBtnOk.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        CtrlBtnOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CtrlBtnOk.FlatStyle = FlatStyle.Flat
        CtrlBtnOk.FlatAppearance.BorderSize = 0
        CtrlBtnOk.Size = New Size(75, 26)
        CtrlBtnOk.TextAlign = ContentAlignment.MiddleCenter


        CtrlBtnPayLessMinAmt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnPayLessMinAmt.BackColor = Color.Transparent
        CtrlBtnPayLessMinAmt.BackColor = Color.FromArgb(0, 107, 163)
        CtrlBtnPayLessMinAmt.ForeColor = Color.FromArgb(255, 255, 255)
        CtrlBtnPayLessMinAmt.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        CtrlBtnPayLessMinAmt.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CtrlBtnPayLessMinAmt.FlatStyle = FlatStyle.Flat
        CtrlBtnPayLessMinAmt.FlatAppearance.BorderSize = 0
        CtrlBtnPayLessMinAmt.Size = New Size(187, 26)
        CtrlBtnPayLessMinAmt.TextAlign = ContentAlignment.MiddleCenter


    End Function
End Class
