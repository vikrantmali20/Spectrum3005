Public Class CtrlHeader
    Dim HdrTxt As String = "HdrText"
    Public Property HdrText() As String
        Get
            Return C1Label1.Text
        End Get
        Set(ByVal value As String)
            C1Label1.Text = value
        End Set
    End Property

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub CtrlHeader_Load(sender As Object, e As EventArgs) Handles Me.Load
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then

            C1Label1.BackColor = Color.FromArgb(49, 49, 49)
            C1Label1.ForeColor = Color.FromArgb(255, 255, 255)
            C1Label1.TextAlign = ContentAlignment.MiddleLeft
            C1Label1.Font = New Font("Neo Sans", 9.5, FontStyle.Bold)
        End If
    End Sub
End Class
