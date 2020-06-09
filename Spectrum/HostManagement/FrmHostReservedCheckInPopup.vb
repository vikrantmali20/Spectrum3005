Public Class FrmHostReservedCheckInPopup
    Private _isReserverd As Boolean

    Public Property IsReserverd() As Boolean
        Get
            Return _isReserverd
        End Get
        Set(ByVal value As Boolean)
            _isReserverd = value
        End Set
    End Property
    Private Sub btnReservedPayment_Click(sender As Object, e As EventArgs) Handles btnReservedPayment.Click
        IsReserverd = True
        Me.DialogResult = Windows.Forms.DialogResult.Yes   'save reserved
        'Me.Close()
    End Sub

    Private Sub btnCheckInPayment_Click(sender As Object, e As EventArgs) Handles btnCheckInPayment.Click
        IsReserverd = False
        Me.DialogResult = Windows.Forms.DialogResult.OK  'save check in
        ' Me.Close()
    End Sub

    Private Sub FrmHostReservedCheckInPopup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DefaultTheme()
    End Sub
    Public Sub DefaultTheme()
        ' btnReservedPayment
        Me.btnReservedPayment.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnReservedPayment.FlatStyle = FlatStyle.Flat
        Me.btnReservedPayment.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.btnReservedPayment.FlatAppearance.BorderSize = 0
        Me.btnReservedPayment.BackColor = Color.FromArgb(0, 113, 188)
        Me.btnReservedPayment.ForeColor = Color.White

        ' btnCheckInPayment
        Me.btnCheckInPayment.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnCheckInPayment.FlatStyle = FlatStyle.Flat
        Me.btnCheckInPayment.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.btnCheckInPayment.FlatAppearance.BorderSize = 0
        Me.btnCheckInPayment.BackColor = Color.FromArgb(0, 113, 188)
        Me.btnCheckInPayment.ForeColor = Color.White
    End Sub
   
End Class