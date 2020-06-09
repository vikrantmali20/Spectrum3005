Imports SpectrumBL
Public Class DayCloseHeader
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub DayCloseHeader_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim objCommon As New clsCommon
            lblHeader.Text = String.Format(lblHeader.Text, clsAdmin.SiteCode, objCommon.GetSiteName(clsAdmin.SiteCode), clsAdmin.DayOpenDate.ToShortDateString(), clsAdmin.UserName)
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function Themechange()
        Me.BackColor = Color.FromArgb(134, 134, 134)
        lblHeader.ForeColor = Color.White
    End Function
End Class
