Imports SpectrumBL
Imports SpectrumCommon
Public Class TrailVersionProductInfo
    Dim License As New LicenseModel
    Public Sub New(ByVal LicenseModel As LicenseModel)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        License = LicenseModel
    End Sub
    Private Sub btnLicenseKey_Click(sender As System.Object, e As System.EventArgs) Handles btnLicenseKey.Click
        Me.Close()
        Dim ConnectionSelector As New ConnectionSelector()
        ConnectionSelector.ShowDialog()
    End Sub

    Private Sub TrailVersionProductInfo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim LicenseHelper As New ClsLicense()

        Dim HDDKey = LicenseHelper.GetEncryptedHDDKey()
        'lblActivationDateValue.Text = License.ActivationDate
        'Dim Hours As String = License.ActivationDate.ToString("hh")
        'If Hours.StartsWith("0") Then
        '    Hours = Hours.Replace("0", "")
        'End If
        'Dim Minutes As String = (CInt(License.ActivationDate.Minute) - 1).ToString()
        'Dim Seconds As String = (CInt(License.ActivationDate.Second) - 1).ToString()

        'If InStr(lblActivationDateValue.Text, "PM") > 0 Then
        '    lblExpiryDateValue.Text = LicenseHelper.GetDatetoExpire(clsAdmin.SiteCode, HDDKey) & " " & Hours & ":" & Minutes & ":" & Seconds & " PM"
        'Else
        '    lblExpiryDateValue.Text = LicenseHelper.GetDatetoExpire(clsAdmin.SiteCode, HDDKey) & " " & Hours & ":" & Minutes & ":" & Seconds & " AM"
        'End If

        ''If InStr(lblActivationDateValue.Text, "PM") > 0 Then
        ''    lblExpiryDateValue.Text = LicenseHelper.GetDatetoExpire(clsAdmin.SiteCode, HDDKey) & " " & Hours & ":" & Minutes & ":" & Seconds & " PM"
        ''Else
        ''    lblExpiryDateValue.Text = LicenseHelper.GetDatetoExpire(clsAdmin.SiteCode, HDDKey) & " " & Hours & ":" & Minutes & ":" & Seconds & " AM"
        ''End If

        'lblDaysToExpireValue.Text = LicenseHelper.GetDaystoExpire(clsAdmin.SiteCode, HDDKey)
        'lblEmailIdValue.Text = String.Empty
        'lblPhoneValue.Text = String.Empty

        lblActivationDateValue.Text = License.ActivationDate.ToShortDateString()

        lblExpiryDateValue.Text = LicenseHelper.GetDatetoExpire(clsAdmin.SiteCode, HDDKey, clsAdmin.TerminalID)
        lblDaysToExpireValue.Text = LicenseHelper.GetDaystoExpire(clsAdmin.SiteCode, HDDKey, clsAdmin.TerminalID)



        lblEmailIdValue.Text = LicenseHelper.GetSupportEmailID(clsAdmin.SiteCode)
        lblPhoneValue.Text = LicenseHelper.GetSupportPhoneNo(clsAdmin.SiteCode)

        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
        End If

    End Sub
    Private Sub Themechange()
        Me.BackColor = Color.FromArgb(134, 134, 134)
        lblLicenseVersion.ForeColor = Color.White
        lblActivationDate.ForeColor = Color.White
        lblActivationDateValue.ForeColor = Color.White
        lblDaysToExpire.ForeColor = Color.White
        lblDaysToExpireValue.ForeColor = Color.White
        lblEmailId.ForeColor = Color.White
        lblEmailIdValue.ForeColor = Color.White
        lblExpiryDate.ForeColor = Color.White
        lblExpiryDateValue.ForeColor = Color.White
        lblPhone.ForeColor = Color.White
        lblPhoneValue.ForeColor = Color.White
        lblPurchasedLicenseInfo.ForeColor = Color.White
        lblSpectrumSupportInfo.ForeColor = Color.White
        btnLicenseKey.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnLicenseKey.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnLicenseKey.BackColor = Color.Transparent
        btnLicenseKey.BackColor = Color.FromArgb(0, 107, 163)
        btnLicenseKey.ForeColor = Color.FromArgb(255, 255, 255)
        btnLicenseKey.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnLicenseKey.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnLicenseKey.FlatStyle = FlatStyle.Flat
        btnLicenseKey.FlatAppearance.BorderSize = 0
        btnLicenseKey.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
    End Sub

End Class