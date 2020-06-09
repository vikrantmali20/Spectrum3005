Public Class ctrlNumberPad
    Private completeNumber As String = String.Empty
    Public Delegate Sub PublishNumber(ByVal number As String)
    Public NumberEntered As PublishNumber
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub NumberPad_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnOK.TextAlign = ContentAlignment.MiddleCenter
        btnOK.TextImageRelation = TextImageRelation.Overlay
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            ThemeChange()
        End If
        For Each control As Control In Me.numberPadTableLayout.Controls
            If (TypeOf control Is CtrlBtn AndAlso control.Tag IsNot Nothing) Then
                RemoveHandler control.Click, AddressOf NumberClicked
                AddHandler control.Click, AddressOf NumberClicked
            End If
        Next
    End Sub

    Private Sub NumberPad_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Leave
        'completeNumber = String.Empty
    End Sub

    Public Sub NumberClicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            If sender IsNot Nothing Then
                Dim number As Integer
                If Int32.TryParse(DirectCast(sender, Control).Tag, number) Then
                    completeNumber += number.ToString()
                    If NumberEntered IsNot Nothing Then
                        NumberEntered(completeNumber)
                    End If
                ElseIf DirectCast(sender, Control).Tag.Equals(".") AndAlso Not completeNumber.Contains(".") Then
                    If (clsDefaultConfiguration.AllowDecimalQty) Then
                        completeNumber += "."
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PublishEnteredNumber(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        ClearNumber()
    End Sub

    Public Sub ClearNumber()
        completeNumber = String.Empty
    End Sub
    Private Function ThemeChange()
        ' Me.C1SizerCustomerInfo.Grid.Clear()
        'Next
        'form

        Me.Size = New System.Drawing.Size(59, 35)
        Me.BackColor = Color.FromArgb(0, 107, 163)
        'sizer
        'Me.numberPadTableLayout.BackColor = Color.FromArgb(0, 0, 64)
        '   Me.numberPadTableLayout.BackColor = Color.FromArgb(0, 107, 163)
        Me.numberPadTableLayout.BackColor = Color.FromArgb(72, 72, 72)
        Me.numberPadTableLayout.RowStyles.Insert(10, New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.0!))
        ' Me.numberPadTableLayout.RowStyles.Clear()
        '1
        'Me.numberPadTableLayout.Controls.Add()
        ' Me.CtrlLabel1.Size = New System.Drawing.Size(43, 21)
        ' Me.CtrlLabel1.Location = New System.Drawing.Point(113, 14)
        Me.CtrlBtn1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.CtrlBtn1.BackColor = Color.FromArgb(0, 107, 163)
        Me.CtrlBtn1.ForeColor = Color.FromArgb(255, 255, 255)
        Me.CtrlBtn1.Font = New Font("NeoSans", 18, FontStyle.Bold)
        Me.CtrlBtn1.FlatAppearance.BorderSize = 1
        Me.CtrlBtn1.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199)
        Me.CtrlBtn1.FlatStyle = FlatStyle.Flat
        Me.CtrlBtn1.TextAlign = ContentAlignment.MiddleCenter
        Dim old1 As Padding = CtrlBtn1.Margin
        CtrlBtn1.Margin = New Padding(0, 0, 1, 0)


        Me.CtrlBtn2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.CtrlBtn2.BackColor = Color.FromArgb(0, 107, 163)
        Me.CtrlBtn2.ForeColor = Color.FromArgb(255, 255, 255)
        Me.CtrlBtn2.Font = New Font("NeoSans", 18, FontStyle.Bold)
        Me.CtrlBtn2.FlatAppearance.BorderSize = 1
        Me.CtrlBtn2.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199)
        Me.CtrlBtn2.TextAlign = ContentAlignment.MiddleCenter
        Me.CtrlBtn2.FlatStyle = FlatStyle.Flat
        Dim old2 As Padding = CtrlBtn1.Margin
        CtrlBtn2.Margin = New Padding(0, 0, 1, 0)

        Me.CtrlBtn3.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.CtrlBtn3.BackColor = Color.FromArgb(0, 107, 163)
        Me.CtrlBtn3.ForeColor = Color.FromArgb(255, 255, 255)
        Me.CtrlBtn3.Font = New Font("NeoSans", 18, FontStyle.Bold)
        Me.CtrlBtn3.FlatAppearance.BorderSize = 1
        Me.CtrlBtn3.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199)
        Me.CtrlBtn3.FlatStyle = FlatStyle.Flat
        Me.CtrlBtn3.TextAlign = ContentAlignment.MiddleCenter
        Dim old3 As Padding = CtrlBtn3.Margin
        CtrlBtn3.Margin = New Padding(0, 0, 1, 0)


        Me.CtrlBtn4.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.CtrlBtn4.BackColor = Color.FromArgb(0, 107, 163)
        Me.CtrlBtn4.ForeColor = Color.FromArgb(255, 255, 255)
        Me.CtrlBtn4.Font = New Font("NeoSans", 18, FontStyle.Bold)
        Me.CtrlBtn4.FlatAppearance.BorderSize = 1
        Me.CtrlBtn4.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199)
        Me.CtrlBtn4.FlatStyle = FlatStyle.Flat
        Me.CtrlBtn4.TextAlign = ContentAlignment.MiddleCenter
        Dim old4 As Padding = CtrlBtn4.Margin
        CtrlBtn4.Margin = New Padding(0, 0, 1, 0)

        Me.CtrlBtn5.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.CtrlBtn5.BackColor = Color.FromArgb(0, 107, 163)
        Me.CtrlBtn5.ForeColor = Color.FromArgb(255, 255, 255)
        Me.CtrlBtn5.Font = New Font("NeoSans", 18, FontStyle.Bold)
        Me.CtrlBtn5.FlatAppearance.BorderSize = 1
        Me.CtrlBtn5.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199)
        Me.CtrlBtn5.FlatStyle = FlatStyle.Flat
        Me.CtrlBtn5.TextAlign = ContentAlignment.MiddleCenter
        Dim old5 As Padding = CtrlBtn5.Margin
        CtrlBtn5.Margin = New Padding(0, 0, 1, 0)

        Me.CtrlBtn6.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.CtrlBtn6.BackColor = Color.FromArgb(0, 107, 163)
        Me.CtrlBtn6.ForeColor = Color.FromArgb(255, 255, 255)
        Me.CtrlBtn6.Font = New Font("NeoSans", 18, FontStyle.Bold)
        Me.CtrlBtn6.FlatAppearance.BorderSize = 1
        Me.CtrlBtn6.TextAlign = ContentAlignment.MiddleCenter
        Me.CtrlBtn6.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199)
        Me.CtrlBtn6.FlatStyle = FlatStyle.Flat
        Dim old6 As Padding = CtrlBtn6.Margin
        CtrlBtn6.Margin = New Padding(0, 0, 1, 0)

        Me.CtrlBtn7.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.CtrlBtn7.BackColor = Color.FromArgb(0, 107, 163)
        Me.CtrlBtn7.ForeColor = Color.FromArgb(255, 255, 255)
        Me.CtrlBtn7.Font = New Font("NeoSans", 18, FontStyle.Bold)
        Me.CtrlBtn7.FlatAppearance.BorderSize = 1
        Me.CtrlBtn7.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199)
        Me.CtrlBtn7.TextAlign = ContentAlignment.MiddleCenter
        Me.CtrlBtn7.FlatStyle = FlatStyle.Flat
        Dim old7 As Padding = CtrlBtn7.Margin
        CtrlBtn7.Margin = New Padding(0, 0, 1, 0)

        Me.CtrlBtn8.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.CtrlBtn8.BackColor = Color.FromArgb(0, 107, 163)
        Me.CtrlBtn8.ForeColor = Color.FromArgb(255, 255, 255)
        Me.CtrlBtn8.Font = New Font("NeoSans", 18, FontStyle.Bold)
        Me.CtrlBtn8.FlatAppearance.BorderSize = 1
        Me.CtrlBtn8.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199)
        Me.CtrlBtn8.FlatStyle = FlatStyle.Flat
        Me.CtrlBtn8.TextAlign = ContentAlignment.MiddleCenter
        Dim old8 As Padding = CtrlBtn8.Margin
        CtrlBtn8.Margin = New Padding(0, 0, 1, 0)

        Me.CtrlBtn9.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.CtrlBtn9.BackColor = Color.FromArgb(0, 107, 163)
        Me.CtrlBtn9.ForeColor = Color.FromArgb(255, 255, 255)
        Me.CtrlBtn9.Font = New Font("NeoSans", 18, FontStyle.Bold)
        Me.CtrlBtn9.FlatAppearance.BorderSize = 1
        Me.CtrlBtn9.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199)
        Me.CtrlBtn9.FlatStyle = FlatStyle.Flat
        Me.CtrlBtn9.TextAlign = ContentAlignment.MiddleCenter
        Dim old9 As Padding = CtrlBtn9.Margin
        CtrlBtn9.Margin = New Padding(0, 0, 1, 0)

        Me.CtrlBtn10.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.CtrlBtn10.BackColor = Color.FromArgb(0, 107, 163)
        Me.CtrlBtn10.ForeColor = Color.FromArgb(255, 255, 255)
        Me.CtrlBtn10.Font = New Font("NeoSans", 18, FontStyle.Bold)
        Me.CtrlBtn10.FlatAppearance.BorderSize = 1
        Me.CtrlBtn10.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199)
        Me.CtrlBtn10.FlatStyle = FlatStyle.Flat
        Dim old10 As Padding = CtrlBtn10.Margin
        Me.CtrlBtn10.TextAlign = ContentAlignment.MiddleCenter
        CtrlBtn10.Margin = New Padding(0, 0, 1, 0)

        Me.CtrlBtnDot.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.CtrlBtnDot.BackColor = Color.FromArgb(0, 107, 163)
        Me.CtrlBtnDot.ForeColor = Color.FromArgb(255, 255, 255)
        Me.CtrlBtnDot.Font = New Font("NeoSans", 18, FontStyle.Bold)
        Me.CtrlBtnDot.FlatAppearance.BorderSize = 1
        Me.CtrlBtnDot.TextAlign = ContentAlignment.MiddleCenter
        Me.CtrlBtnDot.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199)
        Me.CtrlBtnDot.FlatStyle = FlatStyle.Flat
        Dim olddot As Padding = CtrlBtnDot.Margin
        CtrlBtnDot.Margin = New Padding(0, 0, 1, 0)

    End Function
End Class
