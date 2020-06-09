Public Class frmFullScreenMessageBox

    Private Sub frmFullScreenMessageBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
        End If
    End Sub
    Private Function Themechange()
        '
        Me.BackColor = Color.FromArgb(76, 76, 76)
        BtnOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.BtnOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        BtnOk.BackColor = Color.Transparent
        BtnOk.BackColor = Color.FromArgb(0, 107, 163)
        BtnOk.ForeColor = Color.FromArgb(255, 255, 255)
        BtnOk.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        BtnOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnOk.FlatStyle = FlatStyle.Flat
        BtnOk.FlatAppearance.BorderSize = 0
        BtnOk.Size = New Size(62, 40)
        BtnOk.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        MessageLabel.ForeColor = Color.White

    End Function

    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles BtnOk.Click
        Me.Close()
    End Sub
End Class