Imports SpectrumBL
Imports SpectrumCommon
Public Class LicenseVersionProductInfo

    Dim License As New LicenseModel()
    Public Sub New(ByVal LicenseModel As LicenseModel)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        License = LicenseModel

    End Sub
    Private Sub LicenseVersionProductInfo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim LicenseHelper As New ClsLicense()
        Dim HDDKey = LicenseHelper.GetEncryptedHDDKey()
        lblActivationDate.Text = License.ActivationDate.ToShortDateString()
        lblExpireDate.Text = LicenseHelper.GetDatetoExpire(clsAdmin.SiteCode, HDDKey, clsAdmin.TerminalID)

    End Sub
End Class