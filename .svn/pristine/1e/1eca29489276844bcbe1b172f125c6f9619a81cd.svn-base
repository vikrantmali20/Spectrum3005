Imports SpectrumBL
Public Class VersionSelector

    Private Sub btnNext_Click(sender As System.Object, e As System.EventArgs) Handles btnNext.Click
        If rbtnTrailVersion.Checked Then
            Dim LicInstaller As New ClsLicense()
            'added by khusrao adil on 20-07-2018  for Dynamic License Duration setting
            If LicInstaller.CreateTrialLicense(clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.UserCode, clsAdmin.OfflinelicenseActivationDuration) Then
                ShowMessage("Trial Version installed on Machine for " + Convert.ToString(clsAdmin.OfflinelicenseActivationDuration) + " days", "Version Selector")
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                ShowMessage("Fail to install spectrum License. Please contact system Admin", "Version Selector")
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
            End If

        Else
            Dim Connection As New ConnectionSelector()
            Connection.ShowDialog()
            Me.DialogResult = Connection.DialogResult
            Me.Close()
        End If

    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click

    End Sub
End Class