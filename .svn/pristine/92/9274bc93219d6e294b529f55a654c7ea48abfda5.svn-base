Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Runtime.InteropServices

Public Class AndroidSearch
    Private _isAdded As Boolean
    Private _values As [String]()
    Private _lstNames As New List(Of String)
    Private _formerValue As [String] = [String].Empty

    Private Sub ShowListBox()
        If Not _isAdded Then
            ' Parent.Controls.Add(_ComboBox)
            ' _ComboBox.Left = Me.Left
            ' _ComboBox.Top = Me.Top + Me.Height
            _isAdded = True
        End If
        ' _ComboBox.Visible = True
        '   _ComboBox.MaximumSize = New Point(Me.Width + 10, 250)
        Me.BringToFront()
    End Sub
    Private Sub ResetListBox()
        '    _ComboBox.Visible = False
    End Sub
    Private Sub this_KeyUp(sender As Object, e As KeyEventArgs)
        If _lstNames IsNot Nothing Then
            UpdateListBox()
        End If
    End Sub

    Private Sub this_KeyDown(sender As Object, e As KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.Enter
                If True Then
                    If Me.Visible Then
                        InsertWord(DirectCast(Me.SelectedItem, [String]))
                        ResetListBox()
                        _formerValue = Me.Text
                    End If
                    Exit Select
                End If
                'Case Keys.Down
                '    If True Then
                '        If (Me.Visible) AndAlso (Me.SelectedIndex < Me.Items.Count - 1) Then
                '            Me.SelectedIndex += 1
                '        End If
                '        e.Handled = True
                '        Exit Select
                '    End If
                'Case Keys.Up
                '    If True Then
                '        If (Me.Visible) AndAlso (Me.SelectedIndex > 0) Then
                '            'Me.SelectedIndex -= 1
                '        End If
                '        e.Handled = True
                '        Exit Select
                '    End If
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
            Dim matchesList As New List(Of String)
            Dim IsSomethingMatch As Boolean = False
            Dim IsAllWordsMatchInString As Boolean = False

            _formerValue = Me.Text
            'Me.Items.Clear()
            If _formerValue.Length > 0 Then

                Dim stringSeparators As String() = New String() {" "}

                lstNames.ForEach(Function(str)
                                     For Each CheckValue In _formerValue.ToUpper().Split(stringSeparators, StringSplitOptions.None)
                                         If str.ToUpper().Split(stringSeparators, StringSplitOptions.None).Any(Function(i) i.StartsWith(CheckValue)) Then
                                             IsAllWordsMatchInString = True
                                         Else
                                             IsAllWordsMatchInString = False
                                             Exit For
                                         End If
                                     Next
                                     If IsAllWordsMatchInString Then
                                         If Not IsSomethingMatch Then
                                             ShowListBox()
                                             IsSomethingMatch = True
                                         End If
                                         Me.Items.Add(str)
                                     End If
                                 End Function)

                If Me.Items.Count > 0 Then
                    '_ComboBox.SelectedIndex = 0
                    '_ComboBox.Height = 0
                    '_ComboBox.Width = 0
                    Me.Focus()
                    Using graphics As Graphics = Me.CreateGraphics()
                        For i As Integer = 0 To Me.Items.Count - 1
                            Me.Height += Me.GetItemHeight(i)
                            ' it item width is larger than the current one
                            ' set it to the new max item width
                            ' GetItemRectangle does not work for me
                            ' we add a little extra space by using '_'
                            Dim itemWidth As Integer = CInt(graphics.MeasureString(DirectCast(Me.Items(i), [String]) + "_", Me.Font).Width)
                            Me.Width = If((Me.Width < itemWidth), itemWidth, Me.Width)
                        Next
                    End Using
                Else
                    ResetListBox()
                End If
            Else
                ResetListBox()
            End If
        End If
    End Sub

    Private Sub InsertWord(newTag As [String])
        If Not String.IsNullOrEmpty(newTag) Then
            'Dim text As [String] = Me.Text
            'Dim pos As Integer = Me.SelectionStart

            'Dim posStart As Integer = text.LastIndexOf(" "c, If((pos < 1), 0, pos - 1))
            'posStart = If((posStart = -1), 0, posStart + 1)
            'Dim posEnd As Integer = text.IndexOf(" "c, pos)

            'Dim firstPart As [String] = text.Substring(0, posStart) + newTag
            'Dim updatedText As [String] = firstPart & (If((posEnd = -1), "", text.Substring(posEnd, text.Length - posEnd)))


            Me.Text = newTag

        End If

    End Sub

    Public Property lstNames() As List(Of [String])
        Get
            Return _lstNames
        End Get
        Set(value As List(Of [String]))
            _lstNames = value
        End Set
    End Property

    Private Sub _ComboBox_MouseClick_(sender As Object, e As MouseEventArgs)
        Try
            'If Me.Visible Then
            '    InsertWord(DirectCast(Me.SelectedItem, [String]))
            'ResetListBox()
            '    _formerValue = Me.Text
            'End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub AndroidSearch_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles MyBase.PreviewKeyDown
        '    Me.DropDownStyle = ComboBoxStyle.Simple
    End Sub

    Private Sub AndroidSearch_Leave(sender As Object, e As EventArgs) Handles MyBase.Leave
        Me.DropDownStyle = ComboBoxStyle.DropDown
    End Sub

    Private Sub AndroidSearch_Enter(sender As Object, e As EventArgs) Handles MyBase.Enter
        Me.DropDownStyle = ComboBoxStyle.Simple
    End Sub
End Class
