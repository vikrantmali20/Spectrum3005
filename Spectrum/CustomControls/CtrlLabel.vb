Public Class CtrlLabel
    Inherits C1.Win.C1Input.C1Label
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)

        'Add your custom paint code here
    End Sub
    Private _AttachedTextBoxName As String
    Public Property AttachedTextBoxName() As String
        Get
            Return _AttachedTextBoxName
        End Get
        Set(ByVal value As String)
            _AttachedTextBoxName = value
        End Set
    End Property

    Private Sub CtrlLabel_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Not Me.AttachedTextBoxName = String.Empty Then
            Me.Parent.Controls(Me.AttachedTextBoxName.ToString).Left = Me.Right + 2
        End If
    End Sub
End Class
