Public Class CtrlTextBox
    Inherits C1.Win.C1Input.C1TextBox
    Private Property _altWasPressed As Boolean = False
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)

        'Add your custom paint code here
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private _MoveToNxtCtrl As Object
    Public Property MoveToNxtCtrl() As Object
        Get
            Return _MoveToNxtCtrl
        End Get
        Set(ByVal value As Object)
            _MoveToNxtCtrl = value
        End Set
    End Property
    'Private Sub CtrlTextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.UpDownEventArgs) Handles Me.KeyDown
    '    'Me.SelectNextControl(Me, True, True, True, False)
    '    If e. = 13 Then
    '        SendKeys.Send("{tab}")
    '        e.SuppressKeyPress = True
    '    End If
    'End Sub
    Private Sub CtrlTextBox_Click(sender As Object, e As EventArgs) Handles Me.Click
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub
    Private Sub CtrlTextBox_TextChanged(sender As Object, e As EventArgs) Handles MyBase.TextChanged
        Try
            Dim textEnd As String = Text.Substring(Me.SelectionStart - 1, 1)
            If textEnd = "☺" Or textEnd = "☻" Or textEnd = "♥" Or textEnd = "♦" _
                                        Or textEnd = "♣" Or textEnd = "♠" Or textEnd = "•" Or textEnd = "◘" Or textEnd = "○" Or textEnd = "§" Or textEnd = "╚" Or textEnd = "▲" Or textEnd = "ä" Or textEnd = "╤" Or textEnd = "♀" Then

                _altWasPressed = True
            End If

            If (_altWasPressed) Then
                ' remove the added character
                Dim textBox = CType(sender, TextBox)
                Dim caretPos = Me.SelectionStart
                If caretPos = 0 Then Exit Sub
                Dim text = Me.Text
                Dim textStart = text.Substring(0, caretPos - 1)
                If (caretPos <= text.Length) Then
                    textEnd = text.Substring(caretPos, text.Length - caretPos)
                    Me.Text = textStart + textEnd
                    Me.SelectionStart = caretPos - 1
                    ' Dim ax As New ApplicationException(text & "/" & textStart & "/" & caretPos & "/" & textEnd & "/" & Me.Text)
                    'LogException(ax)
                    _altWasPressed = False
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CtrlTextBox_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        Try
            If e.Alt AndAlso (e.KeyCode = Keys.D0 Or e.KeyCode = Keys.D1 Or e.KeyCode = Keys.D2 _
                       Or e.KeyCode = Keys.D3 Or e.KeyCode = Keys.D4 Or e.KeyCode = Keys.D5 _
                       Or e.KeyCode = Keys.D6 Or e.KeyCode = Keys.D7 Or e.KeyCode = Keys.D8 _
                       Or e.KeyCode = Keys.D9 Or e.KeyCode = Keys.NumPad0 _
                       Or e.KeyCode = Keys.NumPad1 Or e.KeyCode = Keys.NumPad2 Or e.KeyCode = Keys.NumPad3 _
                       Or e.KeyCode = Keys.NumPad4 Or e.KeyCode = Keys.NumPad5 Or e.KeyCode = Keys.NumPad6 _
                       Or e.KeyCode = Keys.NumPad7 Or e.KeyCode = Keys.NumPad8 Or e.KeyCode = Keys.NumPad9) Then

                _altWasPressed = True

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CtrlTextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim ctl As Control = DirectCast(sender, Control)
            If ctl.Tag = "NO" Then
            Else
                Me.Parent.SelectNextControl(ctl, True, True, True, True)
            End If
        End If
        '----------------------added by khusrao adil for healthcare

        'If e.KeyCode = Keys.Enter Then
        '    Dim ctl As Control = DirectCast(sender, Control)
        '    If ctl.Tag = "NO" Then
        '    Else
        '        If Not (_MoveToNxtCtrl Is Nothing) Then
        '            MoveToNxtCtrl.Select()
        '        Else
        '            Me.Parent.SelectNextControl(ctl, True, True, True, True)
        '        End If
        '    End If
        'ElseIf e.KeyCode = Keys.Tab Or e.KeyCode = Keys.Menu Then
        '    Dim ctl As Control = DirectCast(sender, Control)
        '    If Not (_MoveToNxtCtrl Is Nothing) Then
        '        MoveToNxtCtrl.select()
        '    End If

        'End If
    End Sub

End Class
