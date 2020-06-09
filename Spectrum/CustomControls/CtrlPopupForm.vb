Public Class CtrlPopupForm
    Inherits C1.Win.C1Ribbon.C1RibbonForm
    Dim WithEvents btnCloseForm As New Button
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)

        'Add your custom paint code here
    End Sub

    Private Sub CtrlPopupForm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If UCase(Me.Name) <> "FRMQSRPAYMENT" Then
            Me.StartPosition = FormStartPosition.CenterScreen
        End If
    End Sub


    Private Sub CtrlPopupForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If UCase(Me.Name) <> "FRMQSRPAYMENT" Then
            Me.StartPosition = FormStartPosition.CenterScreen
        End If
        ' condition is added so that cancel button of payment screen works
        If UCase(Me.Name) <> "FRMNACCEPTPAYMENT" AndAlso UCase(Me.Name) <> "FRMQSRPAYMENT" AndAlso UCase(Me.Name) <> "FRMVIEWORDERDETAILS" Then
            Me.CancelButton = btnCloseForm
        End If

    End Sub

    Private Sub btnCloseForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCloseForm.Click
        '--- Added not affect payment screen by pressing escape
        If UCase(Me.Name) = "FRMNACCEPTPAYMENT" Then
            Exit Sub
        End If
        '----
        If UCase(Me.Name) <> "FRMNLOGIN" Then
            Me.Close()
        End If
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
