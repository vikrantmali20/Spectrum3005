Imports SpectrumPrint
Imports SpectrumBL
Imports SpectrumCommon
Imports Microsoft.Reporting.WinForms
Imports Spire.Pdf
Imports System.IO
Imports C1.Win.C1BarCode

Public Class FrmRedeem
    Dim viewPromo As New Spectrum.CtrlPromo()
    Public _DtPromoList As DataTable
    Public Property DtPromoList() As DataTable
        Get
            Return _DtPromoList
        End Get
        Set(ByVal value As DataTable)
            _DtPromoList = value
        End Set
    End Property

    Public _user_point As String
    Public Property user_point() As String
        Get
            Return _user_point
        End Get
        Set(ByVal value As String)
            _user_point = value
        End Set
    End Property

    Public _is_cashback As String
    Public Property is_cashback() As String
        Get
            Return _is_cashback
        End Get
        Set(ByVal value As String)
            _is_cashback = value
        End Set
    End Property

    Public _promo_ArticleCode As String
    Public Property promo_ArticleCode() As String
        Get
            Return _promo_ArticleCode
        End Get
        Set(ByVal value As String)
            _promo_ArticleCode = value
        End Set
    End Property

    Public _UserSelectedOfferNo As String
    Public Property UserSelectedOfferNo As String
        Get
            Return _UserSelectedOfferNo
        End Get
        Set(ByVal value As String)
            _UserSelectedOfferNo = value
        End Set
    End Property

    Public _promo_NodeCode As String
    Public Property promo_NodeCode As String
        Get
            Return _promo_NodeCode
        End Get
        Set(ByVal value As String)
            _promo_NodeCode = value
        End Set
    End Property
    Public _promo_Value As String
    Public Property promo_Value As String
        Get
            Return _promo_Value
        End Get
        Set(ByVal value As String)
            _promo_Value = value
        End Set
    End Property


    Public _category As String
    Public Property Category As String
        Get
            Return _category
        End Get
        Set(ByVal value As String)
            _category = value
        End Set
    End Property

    Public _HashtagRewardId As String
    Public Property HashtagRewardId As String
        Get
            Return _HashtagRewardId
        End Get
        Set(ByVal value As String)
            _HashtagRewardId = value
        End Set
    End Property


    Dim ObjclsCommon As New clsCommon
    '' <summary>
    ''' Dispatch Click :Getting Data of Home Delivery and Displaying HomeDelivery Form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Public Function Themechange() As String

        Me.BackColor = Color.FromArgb(76, 76, 76)

        LblCustomerName.ForeColor = Color.White
        LblCustomerName.AutoSize = True
        LblCustomerName.BorderStyle = BorderStyle.None
        LblCustomerName.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        LblCustomerName.TextAlign = ContentAlignment.MiddleLeft
        LblCustomerName.BackColor = Color.FromArgb(76, 76, 76)


        LblMobileNo.ForeColor = Color.White
        LblMobileNo.AutoSize = True
        LblMobileNo.BorderStyle = BorderStyle.None
        LblMobileNo.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        LblMobileNo.TextAlign = ContentAlignment.MiddleLeft
        LblMobileNo.BackColor = Color.FromArgb(76, 76, 76)


        LblBalancePoint.ForeColor = Color.White
        LblBalancePoint.AutoSize = True
        LblBalancePoint.BorderStyle = BorderStyle.None
        LblBalancePoint.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        LblBalancePoint.TextAlign = ContentAlignment.MiddleLeft
        LblBalancePoint.BackColor = Color.FromArgb(76, 76, 76)


        'LblMessage
        LblMessage.ForeColor = Color.White
        LblMessage.AutoSize = True
        LblMessage.BorderStyle = BorderStyle.None
        LblMessage.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        LblMessage.TextAlign = ContentAlignment.MiddleLeft
        LblMessage.BackColor = Color.FromArgb(76, 76, 76)

        'Label1
        Label1.ForeColor = Color.White
        Label1.AutoSize = True
        Label1.BorderStyle = BorderStyle.None
        Label1.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Label1.TextAlign = ContentAlignment.MiddleLeft
        Label1.BackColor = Color.FromArgb(76, 76, 76)

        lblTotalBalancePoint.ForeColor = Color.White
        lblTotalBalancePoint.AutoSize = True
        lblTotalBalancePoint.BorderStyle = BorderStyle.None
        lblTotalBalancePoint.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblTotalBalancePoint.TextAlign = ContentAlignment.MiddleLeft
        lblTotalBalancePoint.BackColor = Color.FromArgb(76, 76, 76)


        Label3.ForeColor = Color.White
        Label3.AutoSize = True
        Label3.BorderStyle = BorderStyle.None
        Label3.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Label3.TextAlign = ContentAlignment.MiddleLeft
        Label3.BackColor = Color.FromArgb(76, 76, 76)

        Label4.ForeColor = Color.White
        Label4.AutoSize = True
        Label4.BorderStyle = BorderStyle.None
        Label4.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Label4.TextAlign = ContentAlignment.MiddleLeft
        Label4.BackColor = Color.FromArgb(76, 76, 76)




        Me.btnCancle.BackColor = Color.FromArgb(228, 37, 44)
        Me.btnCancle.ForeColor = Color.White
        Me.btnCancle.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.btnCancle.BringToFront()
        Me.btnCancle.Image = Nothing
        Me.btnCancle.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCancle.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnCancle.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnCancle.FlatStyle = FlatStyle.Flat
        Me.btnCancle.FlatAppearance.BorderSize = 1




        Me.btnCashBack.BackColor = Color.FromArgb(0, 107, 163)
        ' Me.btnCashBack.BackColor = Color.FromArgb(0, 166, 80)
        Me.btnCashBack.ForeColor = Color.White
        Me.btnCashBack.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.btnCashBack.BringToFront()
        Me.btnCashBack.Image = Nothing
        Me.btnCashBack.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCashBack.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCashBack.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnCashBack.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnCashBack.FlatStyle = FlatStyle.Flat
        Me.btnCashBack.FlatAppearance.BorderSize = 1

        Return ""
    End Function



    Private Sub FrmRedeem_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If is_cashback.ToString.ToUpper.Equals("TRUE") Then
                If clsDefaultConfiguration.EnableHashTagLoyaltyPoint Then
                    lblTotalBalancePoint.Visible = True
                    txtPoint.Visible = True
                    btnCashBack.Visible = True
                    ' Label4.Visible = True
                    'LblBalancePoint.Visible = True
                Else
                    lblTotalBalancePoint.Visible = False
                    txtPoint.Visible = False
                    btnCashBack.Visible = False
                    'Label4.Visible = False
                    'LblBalancePoint.Visible = False
                End If
            Else
                lblTotalBalancePoint.Visible = False
                txtPoint.Visible = False
                btnCashBack.Visible = False
                ' Label4.Visible = False
                ' LblBalancePoint.Visible = False
            End If
            ShowPromotionOnGrid()
            lblTotalBalancePoint.Text = "    Total Balance Point :" + user_point.ToString
            LblBalancePoint.Text = user_point

            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub


    Public Sub ApplyPromo(sender As Object, e As EventArgs)

        Try

            Dim parent_control As CtrlPromo = DirectCast(sender, Control).Parent
            UserSelectedOfferNo = parent_control.id
            promo_ArticleCode = parent_control.pos_item_id
            promo_NodeCode = parent_control.pos_category_id
            promo_Value = parent_control.value
            Category = parent_control.category
            user_point = "0"
            HashtagRewardId = parent_control._HashtagRewardId
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnCancle_Click(sender As Object, e As EventArgs) Handles btnCancle.Click

        Try
            UserSelectedOfferNo = ""
            promo_ArticleCode = ""
            promo_NodeCode = ""
            user_point = "0"
            promo_Value = ""
            Category = ""
            HashtagRewardId = ""
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub btnCashBack_Click(sender As Object, e As EventArgs) Handles btnCashBack.Click

        Try
            If String.IsNullOrEmpty(txtpoint.Text) Then
                ShowMessage("Value Should be greater than 0", "Information")
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtpoint.Text) Or Convert.ToDouble(txtpoint.Text) = 0 Then
                ShowMessage("Value Should be greater than 0", "Information")
                Exit Sub
            End If

            If Convert.ToDouble(txtPoint.Text) > Convert.ToDouble(user_point) Then
                ShowMessage("please enter valid points", "Information")
                Exit Sub
            End If
            user_point = txtPoint.Text
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try


    End Sub

    Private Sub FrmRedeem_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    Private Sub ShowPromotionOnGrid()
        Try

            If Not DtPromoList Is Nothing Then
                If DtPromoList.Rows.Count > 0 Then
                    For Each dr In DtPromoList.Rows
                        viewPromo = New CtrlPromo
                        viewPromo.id = dr("promotional_code").ToString
                        viewPromo.name = dr("name").ToString
                        viewPromo.points = dr("points").ToString
                        viewPromo.value = dr("value").ToString
                        viewPromo.category = dr("category").ToString
                        viewPromo.pos_item_id = dr("pos_item_id").ToString
                        viewPromo.is_item_off = dr("is_item_off").ToString
                        viewPromo.pos_category_id = dr("pos_category_id").ToString
                        viewPromo.is_category_off = dr("is_category_off").ToString
                        'viewPromo.lblvalue.Text = "Points Value:" + dr("value").ToString + vbCrLf + "Reward Name:" + dr("PromoName").ToString
                        viewPromo.lblvalue.Text = "Promotion Id : " + dr("promotional_code").ToString + vbCrLf + "Type : " + dr("PromoName").ToString + " " + " Points : " + dr("points").ToString
                        viewPromo.lblname.Text = dr("name").ToString
                        Try
                            Dim len As Integer = viewPromo.lblname.Text.Length
                            If len > 31 Then
                                Dim oldstring = viewPromo.lblname.Text
                                Dim l1 = oldstring.Substring(0, 31)
                                Dim l2 = oldstring.Substring(31, len - 31)
                                viewPromo.lblname.Text = l1 + vbCrLf + l2
                            End If
                        Catch ex As Exception
                            LogException(ex)
                        End Try
                        viewPromo.HashtagRewardId = dr("HashtagRewardId").ToString
                        viewPromo.ctrlbtnApply.BackColor = Color.FromArgb(0, 166, 80)
                        viewPromo.ctrlbtnApply.ForeColor = Color.White
                        viewPromo.ctrlbtnApply.Font = New Font("Neo Sans", 10, FontStyle.Bold)
                        viewPromo.ctrlbtnApply.BringToFront()
                        viewPromo.ctrlbtnApply.Image = Nothing
                        viewPromo.ctrlbtnApply.ImageAlign = System.Drawing.ContentAlignment.TopCenter
                        viewPromo.ctrlbtnApply.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                        ' viewPromo.ctrlbtnApply.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                        'viewPromo.ctrlbtnApply.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
                        viewPromo.ctrlbtnApply.FlatStyle = FlatStyle.Flat
                        viewPromo.ctrlbtnApply.FlatAppearance.BorderSize = 1



                        viewPromo.lblname.ForeColor = Color.White
                        viewPromo.lblname.AutoSize = True
                        viewPromo.lblname.BorderStyle = BorderStyle.None
                        viewPromo.lblname.Font = New Font("Neo Sans", 9, FontStyle.Bold)
                        viewPromo.lblname.BackColor = Color.FromArgb(76, 76, 76)

                        viewPromo.lblvalue.ForeColor = Color.White
                        viewPromo.lblvalue.AutoSize = True
                        viewPromo.lblvalue.BorderStyle = BorderStyle.None
                        viewPromo.lblvalue.Font = New Font("Neo Sans", 9, FontStyle.Bold)
                        viewPromo.lblvalue.BackColor = Color.FromArgb(76, 76, 76)

                        viewPromo.lblname.AutoSize = True
                        viewPromo.lblvalue.AutoSize = True
                        AddHandler viewPromo.ctrlbtnApply.Click, AddressOf ApplyPromo
                        ' viewPromo.Width = ctrlflp.Width
                        Me.ctrlflp.Controls.Add(viewPromo)
                        ctrlflp.AutoScroll = True

                    Next
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtpoint_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtpoint.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub
End Class