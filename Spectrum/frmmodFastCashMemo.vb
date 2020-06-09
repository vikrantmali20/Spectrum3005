Public Class frmmodFastCashMemo




    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmmodFastCashMemo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


        For Each dm As TableLayoutPanel In CtrlMODMenu1.flpMenuButton.Controls
            For Each kl As CtrlBtn In dm.Controls
                kl.Text = "Rahul "
            Next
        Next
    End Sub
End Class