Imports SpectrumBL


Public Class ConnectionSelector

    Private Sub btnNext_Click(sender As System.Object, e As System.EventArgs) Handles btnNext.Click
        If rbnYes.Checked Then
            Dim onlineActivator As New OnlineActivator()
            onlineActivator.ShowDialog()
            Me.DialogResult = onlineActivator.DialogResult
            Me.Close()
        Else
            ShowBigMessage("Please communicate the following 25 digit License key to the Spectrum Team." & Environment.NewLine & New ClsLicense().GetEncryptedHDDKey() & Environment.NewLine & "please contact Spectrum Support at:" & Environment.NewLine & "You will receive another 25 Characterkey. You are required to complete the installation using that key.", "Offline Activation", False)
            Dim offlineactivator As New OfflineLicenseActivator()
            offlineactivator.ShowDialog()
            Me.DialogResult = offlineactivator.DialogResult
            Me.Close()
        End If

    End Sub

    Private Sub ConnectionSelector_Load(sender As Object, e As EventArgs) Handles Me.Load
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
        End If
    End Sub
    Private Sub Themechange()
        Me.BackColor = Color.FromArgb(134, 134, 134)
        btnNext.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnNext.BackColor = Color.Transparent
        btnNext.BackColor = Color.FromArgb(0, 107, 163)
        btnNext.ForeColor = Color.FromArgb(255, 255, 255)
        btnNext.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnNext.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnNext.FlatStyle = FlatStyle.Flat
        btnNext.FlatAppearance.BorderSize = 0

        btnBack.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnBack.BackColor = Color.Transparent
        btnBack.BackColor = Color.FromArgb(0, 107, 163)
        btnBack.ForeColor = Color.FromArgb(255, 255, 255)
        btnBack.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnBack.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnBack.FlatStyle = FlatStyle.Flat
        btnBack.FlatAppearance.BorderSize = 0

        btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnCancel.BackColor = Color.Transparent
        btnCancel.BackColor = Color.FromArgb(0, 107, 163)
        btnCancel.ForeColor = Color.FromArgb(255, 255, 255)
        btnCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.FlatAppearance.BorderSize = 0

        rbnNo.ForeColor = Color.White
        rbnYes.ForeColor = Color.White
        Label1.ForeColor = Color.White

    End Sub
End Class