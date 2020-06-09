Public Class LicenseWarning

    Public Sub New(ByVal msg As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lblMessage.Text = msg
    End Sub

    Private Sub btnLicenseKey_Click(sender As System.Object, e As System.EventArgs) Handles btnLicenseKey.Click
        Me.Close()
        Dim ConnectionSelector As New ConnectionSelector()
        ConnectionSelector.ShowDialog()
    End Sub
End Class