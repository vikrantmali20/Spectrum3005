Public Class Information
    Private _ArticleType As String
    Public Property ArticleType As String
        Get
            Return _ArticleType
        End Get
        Set(ByVal value As String)
            _ArticleType = value
        End Set
    End Property
    Private Sub Information_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.MinimizeBox = False
        Me.MaximizeBox = False
        Me.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
        If ArticleType.ToString.Equals("NOS") Then
            lblText.Text = "Please enter valid number."
        ElseIf ArticleType.ToString.Equals("KGS") Then
            lblText.Text = "Please enter valid quantity."
        End If

    End Sub
End Class