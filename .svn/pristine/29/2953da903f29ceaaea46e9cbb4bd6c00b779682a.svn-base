Imports SpectrumBL
Imports SpectrumCommon
Imports Spectrum.LicenseServices

Public Class OnlineActivator

    Private Sub btnNext_Click(sender As System.Object, e As System.EventArgs) Handles btnNext.Click
        Try
            Dim LicenseActivator As New ClsLicense()
            Dim licepop As frmSpecialPrompt
            Dim connectingPopupMsg As frmSpecialPrompt

            connectingPopupMsg = ShowLicense("We are Connecting Spectrums Central License Manager Server" & Environment.NewLine & "This may take few moments. Please do not exit or close the program", "Online Activation")
            'licepop.Close()

            Dim HDDKey = LicenseActivator.GetEncryptedHDDKey()

            'OLD START''
            'Dim newLicense As New Spectrum.LicenseServices.LicenseModel With
            '    {
            '    .HardwareKey = HDDKey,
            '    .ActivationState = "Active",
            '    .ApplicationType = "FO",
            '    .ActivationMode = "Online",
            '    .MasterKey = txtMasterKey.Text.Trim(),
            '    .IsActive = True,
            '    .CreatedBy = clsAdmin.UserCode,
            '    .CreatedOn = clsAdmin.CurrentDate(),
            '    .UpdatedBy = clsAdmin.UserCode,
            '.UpdatedOn = clsAdmin.CurrentDate()
            '}
            'OLD END''

            ''NEW START''
            Dim newLicense As New Spectrum.LicenseServices.LicenseModel With
               {
               .HardwareKey = HDDKey,
               .ActivationState = "Active",
               .ApplicationType = "FO",
               .ActivationMode = "Online",
               .MasterKey = txtMasterKey.Text.Trim(),
               .SiteCode = clsAdmin.SiteCode,
               .TerminalID = clsAdmin.TerminalID,
               .IsActive = True,
               .CreatedBy = clsAdmin.UserCode,
               .CreatedOn = clsAdmin.CurrentDate(),
               .UpdatedBy = clsAdmin.UserCode,
            .UpdatedOn = clsAdmin.CurrentDate()
           }

            Dim Assembly As String = My.Application.Info.Version.ToString()
            Dim newVersion As New Spectrum.LicenseServices.AutoUpdateVersionInfoModel With
               {
               .HardwareKey = HDDKey,
               .CurrentVersion = Assembly,
               .UpgradedVersion = Assembly,
               .CreatedBy = clsAdmin.UserCode,
               .CreatedOn = clsAdmin.CurrentDate(),
               .UpdatedBy = clsAdmin.UserCode,
            .UpdatedOn = clsAdmin.CurrentDate(),
               .IsActive = True
           }
            ''NEW END''

            Dim client As New LicenseServiceClient()
            '    Dim onlineNewLicense = client.AllocateLicense(newLicense)                           'OLD
            Dim onlineNewLicense = client.AllocateLicense(newLicense, newVersion)                'NEW

            Dim clientLicenseDetails = client.ClientLicenseDetailsByMasterKey(txtMasterKey.Text.Trim())

            If (clientLicenseDetails.POSLicense < clientLicenseDetails.POSLicenseActivated) Then
                connectingPopupMsg.Close()

                ShowFailedLicenseMessage("Could not activate the License." & Environment.NewLine & "Please contact your system Administrator or Spectrum help desk", "Online Activation", False)

                client.DeleteByHardwareKeyAsync(onlineNewLicense.HardwareKey)

                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
                client.Close()
                Exit Sub
            End If

            If (onlineNewLicense IsNot Nothing) Then

                connectingPopupMsg.Close()
                licepop = ShowLicense("License Key Received. Applaying the same to the machine." & Environment.NewLine & "Please do not exit or close the program", "Online Activation")
                '---- Get Email and Mobile No from DefaultConfig -Code Addded By Mahesh----
                Dim LicenseHelper As New ClsLicense()
                Dim supportEmailID = LicenseHelper.GetSupportEmailID(clsAdmin.SiteCode)
                Dim supportPhoneNO = LicenseHelper.GetSupportPhoneNo(clsAdmin.SiteCode)
                Dim newLicenseFO As New SpectrumCommon.LicenseModel With
                    {
                     .LicenseKey = onlineNewLicense.LicenseKey,
                     .SiteCode = clsAdmin.SiteCode,
                     .TerminalID = clsAdmin.TerminalID,
                     .Status = True,
                     .CreatedBy = clsAdmin.UserCode,
                     .UpdatedBy = clsAdmin.UserCode
                 }

                If Not LicenseActivator.OnlineLicense(newLicenseFO) Then
                    licepop.Close()
                    ShowFailedLicenseMessage("Failed to install License on this Machine." & Environment.NewLine & "For further information please contact Spectrum Support at: Email " & supportEmailID & Environment.NewLine & "Phone " & supportPhoneNO, "License Activation", False)

                    client.DeleteByHardwareKeyAsync(onlineNewLicense.HardwareKey)
                    Me.DialogResult = Windows.Forms.DialogResult.Cancel
                    client.Close()
                    Exit Sub
                End If

                licepop.Close()
                

                '// ShowMessage("Spectrum License has been successfully registered for this computer." & Environment.NewLine & "For further information please contact Spectrum Support at: ", "License Activated", False)
                ShowBigMessage("Spectrum License has been successfully registered for this computer." & Environment.NewLine & "For further information please contact Spectrum Support at: " _
                       & Environment.NewLine & "Email " & supportEmailID _
                       & Environment.NewLine & "Phone " & supportPhoneNO _
                       , "License Activated", False)

                Me.DialogResult = Windows.Forms.DialogResult.OK
                client.Close()
                Me.Close()
            Else
                Dim LicenseHelper As New ClsLicense()
                Dim supportEmailID = LicenseHelper.GetSupportEmailID(clsAdmin.SiteCode)
                connectingPopupMsg.Close()
                ShowFailedLicenseMessage("Failed to install License on this Machine." & Environment.NewLine & "For further information please contact Spectrum Support at: " & "Email " & supportEmailID, "License Activation", False)

                client.DeleteByHardwareKeyAsync(onlineNewLicense.HardwareKey)
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                client.Close()
                Me.Close()
            End If

        Catch ex As Exception
            LogException(ex)
            If (ex.ToString().Contains("no endpoint")) Then
                ShowFailedLicenseMessage("Invalid License Connection", "License Activation", False)
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
                Application.Exit()
                End
            End If
            Dim LicenseHelper As New ClsLicense()
            Dim supportEmailID = LicenseHelper.GetSupportEmailID(clsAdmin.SiteCode)
            ShowFailedLicenseMessage("Failed to install License on this Machine." & Environment.NewLine & "For further information please contact Spectrum Support at: " & "Email " & supportEmailID, "License Activation", False)

            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Try
    End Sub

    Private Sub OnlineActivator_Load(sender As Object, e As EventArgs) Handles Me.Load
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
        Label1.ForeColor = Color.White
    End Sub
End Class