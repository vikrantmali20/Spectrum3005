Imports SpectrumCommon
Imports SpectrumBL
Public Class OfflineLicenseActivator

    Private Sub btnNext_Click(sender As System.Object, e As System.EventArgs) Handles btnNext.Click
        Try
            Dim licepop = ShowLicense("Applaying and Validating the License Key to the Machine." & Environment.NewLine & "Please do not exit or close the program", "Offline Activation")
            Dim licactivator As New ClsLicense()
            Dim NewLicense As New LicenseModel With {.LicenseKey = txtLicenseKey.Text.Trim(), .SiteCode = clsAdmin.SiteCode, .TerminalID = clsAdmin.TerminalID, .Status = True}
            NewLicense.CreatedBy = clsAdmin.UserCode
            NewLicense.UpdatedBy = clsAdmin.UserCode

            If Not licactivator.OnlineLicense(NewLicense) Then
                licepop.Close()
                ShowMessage("License Key you have entered is not correct." & Environment.NewLine & "Please contact Spectrum Support at: ", "License Activated", False)
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
                Exit Sub
            End If
            licepop.Close()
            '---- Get Email and Mobile No from DefaultConfig -Code Addded By Mahesh----
            Dim LicenseHelper As New ClsLicense()
            Dim supportEmailID = LicenseHelper.GetSupportEmailID(clsAdmin.SiteCode)
            Dim supportPhoneNO = LicenseHelper.GetSupportPhoneNo(clsAdmin.SiteCode)

            '// ShowMessage("Spectrum License has been successfully registered for this computer." & Environment.NewLine & "For further information please contact Spectrum Support at: ", "License Activated", False)
            ShowBigMessage("Spectrum License has been successfully registered for this computer." & Environment.NewLine & "For further information please contact Spectrum Support at: " _
                   & Environment.NewLine & "Email " & supportEmailID _
                   & Environment.NewLine & "Phone " & supportPhoneNO _
                   , "License Activated", False)

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception
            ShowMessage("License Key you have entered is not correct." & Environment.NewLine & "Please contact Spectrum Support at: ", "License Activated", False)
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Try
    End Sub

End Class