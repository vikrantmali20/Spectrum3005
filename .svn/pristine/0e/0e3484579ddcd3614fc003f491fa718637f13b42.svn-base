Public Class FrmPromotion
    Private _btnClearPromoClick As Boolean
    Public Property BtnClearPromoClick() As Boolean
        Get
            Return _btnClearPromoClick
        End Get
        Set(ByVal value As Boolean)
            _btnClearPromoClick = value
        End Set
    End Property

    Private _btnDefaultPromoClick As Boolean
    Public Property BtnDefaultPromoClick() As Boolean
        Get
            Return _btnDefaultPromoClick
        End Get
        Set(ByVal value As Boolean)
            _btnDefaultPromoClick = value
        End Set
    End Property

    Private _btnSelectPromoClick As Boolean
    Public Property BtnSelectPromoClick() As Boolean
        Get
            Return _btnSelectPromoClick
        End Get
        Set(ByVal value As Boolean)
            _btnSelectPromoClick = value
        End Set
    End Property


    Public Sub Theme()

        Me.Size = New Size(448, 200)
        '    Me.TableLayoutPanel1.Dock = DockStyle.Fill
        Me.TableLayoutPanel1.Location = New Point(50, 33)
        Me.btnDefaultPromo.Dock = DockStyle.None
        Me.btnDefaultPromo.Dock = DockStyle.None
        Me.btnDefaultPromo.BackColor = Color.White
        Me.btnDefaultPromo.ForeColor = Color.Black
        Me.btnDefaultPromo.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnDefaultPromo.BringToFront()
        Me.btnDefaultPromo.Image = Global.Spectrum.My.Resources.defaultPromo1
        Me.btnDefaultPromo.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnDefaultPromo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDefaultPromo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnDefaultPromo.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        'Me.btnDefaultPromo.FlatStyle = FlatStyle.System
        Me.btnDefaultPromo.FlatAppearance.BorderSize = 0

        Me.btnSelectPromo.Dock = DockStyle.None
        Me.btnSelectPromo.Dock = DockStyle.None
        Me.btnSelectPromo.BackColor = Color.White
        Me.btnSelectPromo.ForeColor = Color.Black
        Me.btnSelectPromo.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnSelectPromo.BringToFront()
        Me.btnSelectPromo.Image = Global.Spectrum.My.Resources.SelectPromo1
        Me.btnSelectPromo.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSelectPromo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSelectPromo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnSelectPromo.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        'Me.btnSelectPromo.FlatStyle = FlatStyle.System
        Me.btnSelectPromo.FlatAppearance.BorderSize = 0

        Me.btnClearPromo.Dock = DockStyle.None
        Me.btnClearPromo.Dock = DockStyle.None
        Me.btnClearPromo.BackColor = Color.White
        Me.btnClearPromo.ForeColor = Color.Black
        Me.btnClearPromo.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnClearPromo.BringToFront()
        Me.btnClearPromo.Image = Global.Spectrum.My.Resources.ClearPromo12
        Me.btnClearPromo.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnClearPromo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnClearPromo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnClearPromo.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        'Me.btnClearPromo.FlatStyle = FlatStyle.System
        Me.btnClearPromo.FlatAppearance.BorderSize = 0

        Me.btnClearPromo.Text = "Clear Promo   (Ctrl+C)"

        CtrlBtn5.TextAlign = ContentAlignment.MiddleCenter
        CtrlBtn5.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        CtrlBtn5.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtn5.TextAlign = ContentAlignment.MiddleCenter
        CtrlBtn5.BackColor = Color.FromArgb(228, 37, 44)
        CtrlBtn5.ForeColor = Color.White
        CtrlBtn5.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CtrlBtn5.FlatStyle = FlatStyle.Flat
        CtrlBtn5.FlatAppearance.BorderSize = 0

        'CtrlBtn5.FlatStyle = FlatStyle.Flat
        'CtrlBtn5.FlatAppearance.BorderSize = 0
        'Dim gp As New Drawing.Drawing2D.GraphicsPath
        'Dim rect As New Rectangle
        'rect.Location = New Point(2, 6)
        'rect.Size = New Size(27, 27)
        'rect.Inflate(-2, -2)
        'gp.AddEllipse(rect)
        ''  gp.AddEllipse(rect(New Point(3, 3), New Size(25, 26)).Inflate(-5, -5))
        'CtrlBtn5.Region = New Region(gp)
        'Me.CtrlBtn5.Text = ""
        'Me.CtrlBtn5.Dock = DockStyle.Right
        'Me.CtrlBtn5.Size = New System.Drawing.Size(32, 32)
        ''Me.CtrlBtn5.Location = New Point(388, 3)
        'Me.CtrlBtn5.TextImageRelation = TextImageRelation.Overlay
        'Me.CtrlBtn5.ImageAlign = ContentAlignment.MiddleCenter
        'Me.CtrlBtn5.Image = Global.Spectrum.My.Resources.Close_Hover
        ''Me.CtrlBtn5.Image = Global.Spectrum.My.Resources.Close_blue
    End Sub

    Private Sub FrmPromotion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Theme()
    End Sub

    Private Sub btnDefaultPromo_Click(sender As Object, e As EventArgs) Handles btnDefaultPromo.Click
        BtnDefaultPromoClick = True
        BtnClearPromoClick = False
        BtnSelectPromoClick = False
        Me.Close()
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnSelectPromo_Click(sender As Object, e As EventArgs) Handles btnSelectPromo.Click
        BtnSelectPromoClick = True
        BtnDefaultPromoClick = False
        BtnClearPromoClick = False
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnClearPromo_Click(sender As Object, e As EventArgs) Handles btnClearPromo.Click
        BtnClearPromoClick = True
        BtnDefaultPromoClick = False
        BtnSelectPromoClick = False
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub CtrlBtn5_Click(sender As Object, e As EventArgs) Handles CtrlBtn5.Click
        Me.Close()
    End Sub
End Class