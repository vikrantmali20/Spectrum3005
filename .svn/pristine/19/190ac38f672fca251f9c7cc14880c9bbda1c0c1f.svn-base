Public Class CtrlRoundedButton
    Inherits System.Windows.Forms.Button

    Public Sub New()

        Me.Width = 14
        Me.Height = 20
        Me.BackColor = Color.White
        Me.FlatAppearance.BorderSize = 0
        Me.FlatStyle = Windows.Forms.FlatStyle.Flat
        ' Me.BackColor = Color.Red
        Me.Refresh()

    End Sub

    Private Sub RoundedButton_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged

        Me.Refresh()

    End Sub

    Private Sub RoundedButton_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint

        Dim gp As New System.Drawing.Drawing2D.GraphicsPath
        gp.AddEllipse(New Rectangle(0, 0, Me.Width, Me.Height))
        Me.Region = New Region(gp)

    End Sub

End Class
