Public Class CtrlPayment
    ''df
    Public Function ThemeChange()
        ' For i As Integer = 1 To Me.C1SizerCustomerInfo.Grid.Rows.Count

        '  Me.Dock = DockStyle.Fill
        'Next
        'form
        Me.Size = New System.Drawing.Size(212, 517)
        Me.MinimumSize = New System.Drawing.Size(260, 240)
        Me.BackColor = Color.FromArgb(49, 49, 49)
        'sizer
       
        'Customer
        Me.CtrlHeader1.Dock = DockStyle.None
        Me.CtrlHeader1.Font = New Font("Neo Sans", 14, FontStyle.Bold)
        
        CtrlListPayment.HeadingStyle.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CtrlListPayment.HeadingStyle.BackColor = Color.FromArgb(212, 212, 212)
        CtrlListPayment.ColumnCaptionHeight = 20
        CtrlListPayment.Styles(0).Font = New Font("Neo Sans", 9, FontStyle.Bold)

        CtrlListPayment.MaximumSize = New Size(0, 460)
        CtrlListPayment.Size = New Size(250, 460)
    End Function

    Private Sub CtrlPayment_Load(sender As Object, e As EventArgs) Handles Me.Load
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            ThemeChange()
        End If
    End Sub
End Class
