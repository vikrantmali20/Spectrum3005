Public Class Extened
 
     
    Private Sub Extened_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            ThemeChange()
        Else
            'If clsDefaultConfiguration.HariOmExtendScreen = True Then
            '    dgMainGrid1.Visible = False
            'End If
        End If

    End Sub
    Sub ThemeChange()
        'If clsDefaultConfiguration.HariOmExtendScreen = False Then
        dgMainGrid1.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgMainGrid1.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        dgMainGrid1.Styles.Fixed.Font = New Font("Neo Sans", 14, FontStyle.Regular)
        dgMainGrid1.Styles.Focus.Font = New Font("Neo Sans", 14, FontStyle.Regular)
        dgMainGrid1.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212)

        dgMainGrid1.Styles.Normal.Font = New Font("Neo Sans", 14, FontStyle.Regular)
        dgMainGrid1.Styles.Highlight.Font = New Font("Neo Sans", 14, FontStyle.Bold) 'Neo Sans
        dgMainGrid1.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 16, FontStyle.Bold)
        dgMainGrid1.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 16, FontStyle.Bold)
        dgMainGrid1.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgMainGrid1.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgMainGrid1.Rows.MinSize = 37

        Me.dgMainGrid1.Cols("ArticleDesc").Width = 120
        Me.dgMainGrid1.Cols("Qty").Width = 99
        Me.dgMainGrid1.Cols("Price").Width = 99
        Me.dgMainGrid1.Cols("DiscAmt").Width = 99
        Me.dgMainGrid1.Cols("DiscPer").Width = 99
        Me.dgMainGrid1.Cols("Tax").Width = 99
        Me.dgMainGrid1.Cols("Gross").Width = 99

        Me.lbl.Font = New Font("Neo Sans", 16, FontStyle.Bold)
        Me.lblTotalItems.Location = New System.Drawing.Point(150, 13)
        Me.lblTotalItems.Font = New Font("Neo Sans", 16, FontStyle.Bold)

        Me.Label3.Font = New Font("Neo Sans", 16, FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(230, 13)

        Me.lblTotalQty.Font = New Font("Neo Sans", 16, FontStyle.Bold)
        Me.lblTotalQty.Location = New System.Drawing.Point(350, 13)

        Me.Label5.Font = New Font("Neo Sans", 16, FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(480, 13)
        Me.lblTotalAmount.Font = New Font("Neo Sans", 16, FontStyle.Bold)
        Me.lblTotalAmount.Location = New System.Drawing.Point(650, 13)
        '  Else
        'Me.Height = 100
        'TableLayoutPanel1.Height = 100
        'Panel1.Height = 100
        'dgMainGrid1.Visible = False
        'TableLayoutPanel1.RowStyles.Item(0).SizeType = SizeType.Percent
        'TableLayoutPanel1.RowStyles.Item(0).Height = 5
        'TableLayoutPanel1.RowStyles.Item(1).SizeType = SizeType.Absolute
        'TableLayoutPanel1.RowStyles.Item(1).Height = 95

        'Me.lbl.Font = New Font("Neo Sans", 16, FontStyle.Bold)
        'Me.lbl.Location = New System.Drawing.Point(10, 25)

        'Me.lblTotalItems.Location = New System.Drawing.Point(150, 25)
        'Me.lblTotalItems.Font = New Font("Neo Sans", 16, FontStyle.Bold)

        'Me.Label3.Font = New Font("Neo Sans", 16, FontStyle.Bold)
        'Me.Label3.Location = New System.Drawing.Point(230, 25)

        'Me.lblTotalQty.Font = New Font("Neo Sans", 16, FontStyle.Bold)
        'Me.lblTotalQty.Location = New System.Drawing.Point(350, 25)

        'Me.Label5.Font = New Font("Neo Sans", 16, FontStyle.Bold)
        'Me.Label5.Location = New System.Drawing.Point(480, 25)

        'Me.lblTotalAmount.Font = New Font("Neo Sans", 16, FontStyle.Bold)
        'Me.lblTotalAmount.Location = New System.Drawing.Point(650, 25)
        '  End If
    End Sub
End Class