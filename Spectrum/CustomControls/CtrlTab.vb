Public Class CtrlTab
    Inherits C1.Win.C1Command.C1DockingTab
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)

        'Add your custom paint code here
    End Sub

    Public Sub pInit()
        Me.TabStyle = C1.Win.C1Command.TabStyleEnum.Sloping
        Me.TabsSpacing = 6
    End Sub

End Class
