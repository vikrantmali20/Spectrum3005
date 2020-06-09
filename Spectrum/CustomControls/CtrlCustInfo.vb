Public Class CtrlCustInfo
    Public Function ThemeChange()
        ' For i As Integer = 1 To Me.C1SizerCustomerInfo.Grid.Rows.Count
        ' Me.C1SizerCustomerInfo.Grid.Clear()
        '  Me.Dock = DockStyle.Fill
        'Next
        'form
        '   Me.Size = New System.Drawing.Size(212, 517)
        '    Me.MinimumSize = New System.Drawing.Size(260, 240)
        Me.BackColor = Color.FromArgb(49, 49, 49)
        'sizer
        C1SizerCustomerInfo.SplitterWidth = 1
        C1SizerCustomerInfo.Border.Color = Color.Transparent
        '  C1SizerCustomerInfo.Size = New System.Drawing.Size(212, 517)
        C1SizerCustomerInfo.BackColor = Color.FromArgb(49, 49, 49)
        'Customer
        '  Me.CtrlHeader1.Dock = DockStyle.None
        ' Me.CtrlHeader1.Size = New System.Drawing.Size(120, 21)
        '  Me.CtrlHeader1.Location = New System.Drawing.Point(3, 2)
        Me.CtrlHeader1.Font = New Font("Neo Sans", 14, FontStyle.Bold)
        'Me.CtrlHeader1.
        'more
        '  Me.CtrlLabel4.Size = New System.Drawing.Size(70, 21)
        'Me.CtrlLabel4.Location = New System.Drawing.Point(125, 4)
        Me.CtrlLabel4.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        ' Me.CtrlLabel4.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.CtrlLabel4.VisualStyle = C1.Win.C1Input.VisualStyle.System
        CtrlLabel4.Text = "More Info"
        Me.CtrlLabel4.TextAlign = ContentAlignment.MiddleCenter
        Me.CtrlLabel4.ForeColor = Color.FromArgb(72, 72, 72)
        Me.CtrlLabel4.BackColor = Color.Silver


        'clear
        ' Me.BtnClearCustmInfo.Size = New System.Drawing.Size(70, 21)
        ' Me.BtnClearCustmInfo.Location = New System.Drawing.Point(142, 4)
        Me.BtnClearCustmInfo.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.BtnClearCustmInfo.ForeColor = Color.FromArgb(72, 72, 72)
        Me.BtnClearCustmInfo.Text = "Clear Info"
        Me.BtnClearCustmInfo.TextAlign = ContentAlignment.MiddleCenter

        Me.BtnClearCustmInfo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        '   btnExit.BackColor = Color.Transparent
        BtnClearCustmInfo.BackColor = Color.FromArgb(0, 107, 163)
        BtnClearCustmInfo.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnClearCustmInfo.BackColor = Color.Silver
        BtnClearCustmInfo.FlatStyle = FlatStyle.Flat
        BtnClearCustmInfo.FlatAppearance.BorderSize = 0

        'custinfo label
        '   Me.CtrlHeader1.Size = New System.Drawing.Size(102, 21)
        '   Me.CtrlHeader1.Location = New System.Drawing.Point(5, 4)
        Me.CtrlHeader1.BackColor = Color.Transparent
        Me.CtrlHeader1.ForeColor = Color.White
        '  Me.CtrlHeader1.Text = Me.CtrlHeader1.Text.ToUpper
        'custno
        '  Me.CtrlLabel1.Size = New System.Drawing.Size(99, 22)
        '  Me.CtrlLabel1.Location = New System.Drawing.Point(4, 32)
        Me.CtrlLabel1.BackColor = Color.FromArgb(212, 212, 212)
        Me.CtrlLabel1.TextAlign = ContentAlignment.MiddleLeft
        Me.CtrlLabel1.Font = New Font("NeoSans", 9, FontStyle.Bold)
        Me.CtrlLabel1.ForeColor = Color.FromArgb(37, 37, 37)
        '  Me.CtrlLabel1.Text = Me.CtrlLabel1.Text.ToUpper

        '  Me.CtrlTxtCustomerNo.Size = New System.Drawing.Size(185, 22)
        ' Me.CtrlTxtCustomerNo.Location = New System.Drawing.Point(102, 32)
        Me.CtrlTxtCustomerNo.BackColor = Color.FromArgb(255, 255, 255)
        Me.CtrlTxtCustomerNo.BringToFront()
        'Me.CtrlTxtCustomerNo.Text = Me.CtrlTxtCustomerNo.Text.ToUpper
        'swipe
        ' Me.CtrlLabel2.Size = New System.Drawing.Size(99, 22)
        ' Me.CtrlLabel2.Location = New System.Drawing.Point(3, 57)
        'Me.CtrlLabel2.BackColor = Color.Silver
        Me.CtrlLabel2.BackColor = Color.FromArgb(212, 212, 212)
        Me.CtrlLabel2.Font = New Font("NeoSans", 9, FontStyle.Bold)
        Me.CtrlLabel2.TextAlign = ContentAlignment.MiddleLeft
        Me.CtrlLabel2.ForeColor = Color.FromArgb(37, 37, 37)
        CtrlLabel2.BorderColor = Color.Transparent
        'Me.CtrlLabel2.Text = Me.CtrlLabel2.Text.ToUpper
        'code added for issue id 1511 by vipul
        Me.CtrlTxtSwape.Size = New System.Drawing.Size(210, 21)
        '  Me.CtrlTxtSwape.Size = New System.Drawing.Size(185, 22)
        ' Me.CtrlTxtSwape.Location = New System.Drawing.Point(102, 57)
        Me.CtrlTxtSwape.BackColor = Color.FromArgb(255, 255, 255)
        Me.CtrlTxtSwape.BringToFront()
        '  Me.CtrlTxtSwape.Text = Me.CtrlTxtSwape.Text.ToUpper
        'name
        '  Me.CtrlLabel3.Size = New System.Drawing.Size(99, 22)
        '  Me.CtrlLabel3.Location = New System.Drawing.Point(4, 82)
        'Me.CtrlLabel3.BackColor = Color.Silver
        Me.CtrlLabel3.BackColor = Color.FromArgb(212, 212, 212)
        Me.CtrlLabel3.Font = New Font("NeoSans", 9, FontStyle.Bold)
        Me.CtrlLabel3.TextAlign = ContentAlignment.MiddleLeft
        Me.CtrlLabel3.ForeColor = Color.FromArgb(37, 37, 37)
        '  Me.CtrlLabel3.Text = Me.CtrlLabel3.Text.ToUpper

        ' Me.CtrltxtCustomerName.Size = New System.Drawing.Size(185, 22)
        ' Me.CtrltxtCustomerName.Location = New System.Drawing.Point(102, 82)
        Me.CtrltxtCustomerName.BackColor = Color.FromArgb(255, 255, 255)
        Me.CtrltxtCustomerName.BringToFront()
        ' Me.CtrltxtCustomerName.Text = Me.CtrltxtCustomerName.Text.ToUpper
        'balpoints
        ' Me.CtrlLabel5.Size = New System.Drawing.Size(99, 22)
        ' Me.CtrlLabel5.Location = New System.Drawing.Point(4, 107)
        'Me.CtrlLabel5.BackColor = Color.Silver
        Me.CtrlLabel5.BackColor = Color.FromArgb(212, 212, 212)
        Me.CtrlLabel5.Font = New Font("NeoSans", 9, FontStyle.Bold)
        Me.CtrlLabel5.TextAlign = ContentAlignment.MiddleLeft
        Me.CtrlLabel5.ForeColor = Color.FromArgb(37, 37, 37)
        ' Me.CtrlLabel5.Text = Me.CtrlLabel5.Text.ToUpper

        ' Me.ctrlTxtPoints.Size = New System.Drawing.Size(113, 22)
        ' Me.ctrlTxtPoints.Location = New System.Drawing.Point(102, 107)
        '  ctrlTxtPoints.
        Me.ctrlTxtPoints.BackColor = Color.FromArgb(255, 255, 255)
        Me.ctrlTxtPoints.BringToFront()
        '   Me.ctrlTxtPoints.Text = Me.ctrlTxtPoints.Text.ToUpper


        ' Me.CtrlLabel6.Size = New System.Drawing.Size(99, 22)
        ' Me.CtrlLabel6.Location = New System.Drawing.Point(4, 132)
        Me.CtrlLabel6.BackColor = Color.FromArgb(212, 212, 212)
        Me.CtrlLabel6.Font = New Font("NeoSans", 9, FontStyle.Bold)
        Me.CtrlLabel6.TextAlign = ContentAlignment.MiddleLeft
        Me.CtrlLabel6.ForeColor = Color.FromArgb(37, 37, 37)
        'Me.CtrlLabel6.Text = Me.CtrlLabel6.Text.ToUpper

        ' Me.CtrlLastVisit.Size = New System.Drawing.Size(185, 22)
        ' Me.CtrlLastVisit.Location = New System.Drawing.Point(102, 132)
        Me.CtrlLastVisit.BackColor = Color.FromArgb(255, 255, 255)
        Me.CtrlLastVisit.BringToFront()
        '   Me.CtrlLastVisit.Text = Me.CtrlLastVisit.Text.ToUpper


    End Function

    Private Sub CtrlCustInfo_Load(sender As Object, e As EventArgs) Handles Me.Load
        If clsDefaultConfiguration.EvasPizzaChanges = True Then
            ctrlTxtPoints.Visible = False
            ctrlTxtAddress.Visible = True
            CtrlLabel4.Visible = False
            CtrlLabel5.Text = "Address"
            CtrlLabel6.Text = "   "
        End If
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            ThemeChange()
        End If
    End Sub

    Private Sub CtrlTxtSwape_Click(sender As Object, e As EventArgs) Handles CtrlTxtSwape.Click
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub
End Class
