Public Class CtrlTextBox
    Inherits C1.Win.C1Input.C1TextBox
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)

        'Add your custom paint code here
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    'Private Sub CtrlTextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.UpDownEventArgs) Handles Me.KeyDown
    '    'Me.SelectNextControl(Me, True, True, True, False)
    '    If e. = 13 Then
    '        SendKeys.Send("{tab}")
    '        e.SuppressKeyPress = True
    '    End If
    'End Sub

    Private Sub CtrlTextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim ctl As Control = DirectCast(sender, Control)
            If ctl.Tag = "NO" Then
            Else
                Me.Parent.SelectNextControl(ctl, True, True, True, True)
            End If
        End If
    End Sub
   

End Class
