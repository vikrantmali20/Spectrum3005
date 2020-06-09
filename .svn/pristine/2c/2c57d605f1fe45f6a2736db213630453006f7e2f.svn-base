
Public Class ctrlDate
    Inherits C1.Win.C1Input.C1DateEdit
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)

        'Add your custom paint code here
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.ShowUpDownButtons = True
        Me.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Me.ButtonImages.DropImage = My.Resources.Calendar
        Else
            Me.ButtonImages.DropImage = My.Resources.calendar_month_16
        End If
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ctrlDate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim ctl As Control = DirectCast(sender, Control)
            Me.Parent.SelectNextControl(ctl, True, True, True, True)
        End If
    End Sub
End Class
