Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Runtime.InteropServices

Public Class AutoCompleteTextBox
    Private _isAdded As Boolean
    Private _values As [String]()
    Private _formerValue As [String] = [String].Empty

    'Public Sub New()
    '    InitializeComponent()
    '    ResetListBox()
    'End Sub

    'Private Sub InitializeComponent()
    '    _listBox = New ListBox()
    '    Me.KeyDown += New System.Windows.Forms.KeyEventHandler(Me.this_KeyDown)
    '    Me.KeyUp += New System.Windows.Forms.KeyEventHandler(Me.this_KeyUp)
    '      _listBox.Visible = False
    'End Sub

    Private Sub ShowListBox()
        'Comment By ketan 
        'If Not _isAdded Then
        '    Parent.Controls.Add(_listBox)
        '    _listBox.Left = Me.Left
        '    _listBox.Top = Me.Top + Me.Height
        '    _isAdded = True
        'End If
        ' add by ketan same logic in AndroidSearchTextBox
        If Not _isAdded Then
            '  Parent.Controls.Add(_listBox)
            '''' Need to popUp on Form Level
            Dim Formparent As Control
            Dim Preparent As Control
            Preparent = Me
            While Preparent IsNot Nothing
                Formparent = Preparent
                Preparent = Formparent.Parent
            End While
            'Me.Controls.Add(_listBox)
            Formparent.Controls.Add(_listBox)
            'parent.PointToScreen(Me.Location)
            '_listBox.Left = Me.Left
            '_listBox.Top = Me.Top + Me.Height
            Dim MePos = Me.FindForm().PointToClient(Me.Parent.PointToScreen(Me.Location))
            _listBox.Location = New Point(MePos.X, MePos.Y + Me.Height)
            _isAdded = True
        End If
        _listBox.Visible = True
        _listBox.BringToFront()
    End Sub

    Private Sub ResetListBox()
        _listBox.Visible = False
        Me.Select()
        Me.SelectionStart = Me.Text.Length
    End Sub

    Private Sub this_KeyUp(sender As Object, e As KeyEventArgs)
        If _values IsNot Nothing Then
            UpdateListBox()
        End If
    End Sub

    Private Sub this_KeyDown(sender As Object, e As KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.Enter
                If True Then
                    If _listBox.Visible Then
                        InsertWord(DirectCast(_listBox.SelectedItem, [String]))
                        ResetListBox()
                        _formerValue = Me.Text
                    End If
                    Exit Select
                End If
            Case Keys.Down
                If True Then
                    If (_listBox.Visible) AndAlso (_listBox.SelectedIndex < _listBox.Items.Count - 1) Then
                        _listBox.SelectedIndex += 1
                    End If
                    e.Handled = True
                    Exit Select
                End If
            Case Keys.Up
                If True Then
                    If (_listBox.Visible) AndAlso (_listBox.SelectedIndex > 0) Then
                        _listBox.SelectedIndex -= 1
                    End If
                    e.Handled = True
                    Exit Select
                End If
            Case Keys.Escape
                ResetListBox()
        End Select
    End Sub

    Protected Overrides Function IsInputKey(keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.Enter
                Return True
            Case Keys.Tab
                ResetListBox()
            Case Else
                Return MyBase.IsInputKey(keyData)
        End Select
    End Function

    Private Sub UpdateListBox()
        If Me.Text <> _formerValue Then
            _formerValue = Me.Text
            Dim word As [String] = GetWord()

            If word.Length > 0 Then
                Dim matches As [String]() = Array.FindAll(_values, Function(x) (x.ToString.ToUpper.StartsWith(word.ToString.ToUpper)))    'AndAlso Not SelectedValues.Contains(x)
                If matches.Length > 0 Then
                    ShowListBox()
                    _listBox.Items.Clear()
                    Array.ForEach(matches, Function(x) _listBox.Items.Add(x))
                    _listBox.SelectedIndex = 0
                    _listBox.Height = 0
                    _listBox.Width = 0
                    Me.Focus()
                    Dim lstheight, lstMaxWidth As Long
                    Using graphics As Graphics = _listBox.CreateGraphics()
                        For i As Integer = 0 To _listBox.Items.Count - 1
                            '' Calculate Last Max Width and max height Here 
                            lstheight += _listBox.GetItemHeight(i)
                            ' it item width is larger than the current one
                            ' set it to the new max item width
                            ' GetItemRectangle does not work for me
                            ' we add a little extra space by using '_'
                            Dim itemWidth As Integer = CInt(graphics.MeasureString(DirectCast(_listBox.Items(i), [String]) + "_", _listBox.Font).Width)
                            '_listBox.Width = If((_listBox.Width < itemWidth), itemWidth, _listBox.Width)
                            lstMaxWidth = If((lstMaxWidth < itemWidth), itemWidth, lstMaxWidth)
                        Next
                    End Using
                    ''Change By ketan For Performance issue 
                    '' pass max height and Width to list box 
                    _listBox.Height = lstheight + 10
                    _listBox.Width = lstMaxWidth + 10
                Else
                    ResetListBox()
                End If
            Else
                ResetListBox()
            End If
        End If
    End Sub

    Private Function GetWord() As [String]
        Dim text As [String] = Me.Text
        Dim pos As Integer = Me.SelectionStart

        Dim posStart As Integer = text.LastIndexOf(" "c, If((pos < 1), 0, pos - 1))
        posStart = If((posStart = -1), 0, posStart + 1)
        Dim posEnd As Integer = text.IndexOf(" "c, pos)
        posEnd = If((posEnd = -1), text.Length, posEnd)

        Dim length As Integer = If(((posEnd - posStart) < 0), 0, posEnd - posStart)

        Return text.Substring(posStart, length)
    End Function

    Private Sub InsertWord(newTag As [String])
        If Not String.IsNullOrEmpty(newTag) Then
            Me.Text = newTag
        End If
        '' Code Commented By Ketan Vaidya For Performance Purpose   
        'If Not String.IsNullOrEmpty(newTag) AndAlso Me.MaxLength >= (Me.Text.ToString.Length + newTag.ToString.Length) Then
        '    Dim text As [String] = Me.Text
        '    Dim pos As Integer = Me.SelectionStart

        '    Dim posStart As Integer = text.LastIndexOf(" "c, If((pos < 1), 0, pos - 1))
        '    posStart = If((posStart = -1), 0, posStart + 1)
        '    Dim posEnd As Integer = text.IndexOf(" "c, pos)

        '    Dim firstPart As [String] = text.Substring(0, posStart) + newTag
        '    Dim updatedText As [String] = firstPart & (If((posEnd = -1), "", text.Substring(posEnd, text.Length - posEnd)))


        '    Me.Text = updatedText
        '    Me.SelectionStart = firstPart.Length
        'End If
       
    End Sub

    Public Property Values() As [String]()
        Get
            Return _values
        End Get
        Set(value As [String]())
            _values = value
        End Set
    End Property

    Public ReadOnly Property SelectedValues() As List(Of [String])
        Get
            Dim result As [String]() = Text.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
            Return New List(Of [String])(result)
        End Get
    End Property

    Private Sub _listBox_MouseClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles _listBox.MouseClick
        Try
            If _listBox.Visible Then
                InsertWord(DirectCast(_listBox.SelectedItem, [String]))
                ResetListBox()
                _formerValue = Me.Text
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class
