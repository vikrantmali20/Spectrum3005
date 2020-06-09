Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Runtime.InteropServices

Public Class AndroidSearchTextBox

#Region "Variable Declaration"
    Private _isAdded As Boolean
    Private _values As [String]()
    Private _lstNames As New List(Of String)
    Private _DtSearchData As DataTable ' 3 column key , Value ,searchData
    Public _SearchValue As [String] = [String].Empty
    Private _SearchBasedOnDB As String
    Private _SearchQueryOnDB As String
    Private _IsMovingControl As Boolean
    Private _AllowUpdateListBox As Boolean = True
    Private _IsItemSelected As Boolean
    Private _IsListBind As Boolean = True
    Private _isMouseOverList As Boolean
    Private _isCallFromPosTab As Boolean ' added by khusrao adil on 27-03-2018 for Spectrum Lite
    Public _IsSetLocation As Boolean
    Public _ListBoxXCoordinate As Integer
    Public _ListBoxYCoordinate As Integer
    Public _ArticleCodeDesclength As Integer
    Public _isCalledFromlablePrint As Boolean
#End Region

#Region "Property Declaration"
    Public Property SearchBasedOnDB() As String
        Get
            Return _SearchBasedOnDB
        End Get
        Set(ByVal value As String)
            _SearchBasedOnDB = value
        End Set
    End Property
    Public Property isCalledFromlablePrint() As Boolean
        Get
            Return _isCalledFromlablePrint
        End Get
        Set(ByVal value As Boolean)
            _isCalledFromlablePrint = value
        End Set
    End Property
    Public Property ArticleCodeDesclength() As Integer
        Get
            Return _ArticleCodeDesclength
        End Get
        Set(ByVal value As Integer)
            _ArticleCodeDesclength = value
        End Set
    End Property
    Public Property SearchQueryOnDB() As String
        Get
            Return _SearchQueryOnDB
        End Get
        Set(ByVal value As String)
            _SearchQueryOnDB = value
        End Set
    End Property

    Public Property IsMovingControl() As Boolean
        Get
            Return _IsMovingControl
        End Get
        Set(ByVal value As Boolean)
            _IsMovingControl = value
        End Set
    End Property

    Public Property AllowUpdateListBox() As Boolean
        Get
            Return _AllowUpdateListBox
        End Get
        Set(ByVal value As Boolean)
            _AllowUpdateListBox = value
        End Set
    End Property


    Public Property IsItemSelected() As Boolean
        Get
            Return _IsItemSelected
        End Get
        Set(value As Boolean)
            _IsItemSelected = value
        End Set
    End Property

    Public Property IsListBind() As Boolean
        Get
            Return _IsListBind
        End Get
        Set(ByVal value As Boolean)
            _IsListBind = value
        End Set
    End Property

    Public Property lstNames() As List(Of [String])
        Get
            Return _lstNames
        End Get
        Set(value As List(Of [String]))
            _lstNames = value
        End Set
    End Property

    Public Property DtSearchData() As DataTable
        Get
            Return _DtSearchData
        End Get
        Set(value As DataTable)
            _DtSearchData = value
        End Set
    End Property

    Public Property IsMouseOverList As Boolean
        Get
            Return _isMouseOverList
        End Get
        Set(value As Boolean)
            _isMouseOverList = value
        End Set
    End Property
    Public Property IsCallFromPosTab As Boolean
        Get
            Return _isCallFromPosTab
        End Get
        Set(value As Boolean)
            _isCallFromPosTab = value
        End Set
    End Property


    Public Property IsSetLocation As Boolean
        Get
            Return _IsSetLocation
        End Get
        Set(value As Boolean)
            _IsSetLocation = value
        End Set
    End Property

    Public Property ListBoxXCoordinate() As Integer
        Get
            Return _ListBoxXCoordinate
        End Get
        Set(ByVal value As Integer)
            _ListBoxXCoordinate = value
        End Set
    End Property
    Public Property ListBoxYCoordinate() As Integer
        Get
            Return _ListBoxYCoordinate
        End Get
        Set(ByVal value As Integer)
            _ListBoxYCoordinate = value
        End Set
    End Property
#End Region
    Private Sub ShowListBox()
        If Not _isAdded Or IsMovingControl Then
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
            If IsCallFromPosTab = True Then
                _listBox.Location = New Point(MePos.X + 85, MePos.Y + Me.Height + 50)
            Else
                _listBox.Location = New Point(MePos.X + 3, MePos.Y + Me.Height + 3)
            End If

            If IsSetLocation = True Then
                _listBox.Location = New Point(ListBoxXCoordinate, ListBoxYCoordinate)
            End If
            _isAdded = True
        End If
        _listBox.Visible = True
        '_listBox.MaximumSize = New Point(Me.Width + 50, 250)
        If isCalledFromlablePrint = True Then
            If ArticleCodeDesclength > 0 Then
                _listBox.MaximumSize = New Point(Me.Width + ArticleCodeDesclength + 100, 250)
            Else
                _listBox.MaximumSize = New Point(Me.Width + 50, 250)
            End If
        Else
            _listBox.MaximumSize = New Point(Me.Width + 50, 250)
        End If
        _listBox.BringToFront()

    End Sub

    Public Sub ResetListBox()
        _listBox.Visible = False
        Me.Select()
        Me.SelectionStart = Me.Text.Length
    End Sub

    Public Function IsListBoxVisible() As Boolean
        Return _listBox.Visible AndAlso (_listBox.Items.Count > 0)
    End Function

    Private Sub this_KeyUp(sender As Object, e As KeyEventArgs)
        If _DtSearchData IsNot Nothing Then
            If IsListBind = True Then
                UpdateListBox()
            Else
                _listBox.Visible = False
            End If
        End If
    End Sub

    Private Sub this_KeyDown(sender As Object, e As KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.Enter
                If True Then
                    If _listBox.Visible Then
                        'InsertWord(DirectCast(_listBox.SelectedItem, [String]))
                        If Me.Text <> "" Then
                            InsertWord(DirectCast(_listBox.SelectedItem("searchData"), [String]))
                            ResetListBox()
                            _SearchValue = Me.Text
                        End If
                    ElseIf Not IsListBind Then
                        IsItemSelected = True
                    Else
                        IsItemSelected = True
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
            Case Else
                _IsItemSelected = False
        End Select
    End Sub

    Protected Overrides Function IsInputKey(keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.Enter
                Return True
            Case Keys.Tab
                'Me.Clear()
                ResetListBox()
            Case Else
                Return MyBase.IsInputKey(keyData)
        End Select
    End Function

    Private Sub UpdateListBox()
        If Me.Text <> _SearchValue Then
            Dim matchesList As New List(Of String)
            Dim IsSomethingMatch As Boolean = False
            Dim IsAllWordsMatchInString As Boolean = False
            _SearchValue = Me.Text
            ''added by ketan crash issue 
            If _SearchValue.Contains("'") = True Then
                _SearchValue = _SearchValue.Replace("'", "''")
            End If
            'modified by khusrao adil on 25-06-2018 for mantis id-3461
            If System.Text.RegularExpressions.Regex.IsMatch(_SearchValue, "^[a-zA-Z0-9 -/]*$") Then
            Else
                _SearchValue = String.Empty
            End If
            '_listBox.Items.Clear()
            If _SearchValue.Length > 0 Then
                'Dim stringSeparators As String() = New String() {" "}
                'lstNames.ForEach(Function(str)
                '                     'For Each CheckValue In _formerValue.ToUpper().Split(stringSeparators, StringSplitOptions.None)
                '                     '    If str.ToUpper().Split(stringSeparators, StringSplitOptions.None).Any(Function(i) i.StartsWith(CheckValue)) Then
                '                     '        IsAllWordsMatchInString = True
                '                     '    Else
                '                     '        IsAllWordsMatchInString = False
                '                     '        Exit For
                '                     '    End If
                '                     'Next
                '                     If str.ToUpper().Contains(_SearchValue.ToUpper()) Then
                '                         _listBox.Items.Add(str)
                '                         IsSomethingMatch = True
                '                     End If
                '                 End Function)

                Dim view As New DataView(DtSearchData, "searchData like('%" & _SearchValue & "%')", "", DataViewRowState.CurrentRows)
                ' Sort by State and ZipCode column in descending order
                If IsNumeric(_SearchValue) Then
                    view.Sort = "key ASC"
                Else
                    view.Sort = "Value ASC"
                End If

                'For Each dr As DataRow In view.ToTable.Rows
                '    _listBox.Items.Add(dr("searchData"))
                '    IsSomethingMatch = True
                'Next
                _listBox.DataSource = view.ToTable
                _listBox.DisplayMember = "searchData"
                _listBox.ValueMember = "key"
                IsSomethingMatch = True
                If IsSomethingMatch Then
                    ShowListBox()
                End If
                Me.SuspendLayout()
                If _listBox.Items.Count > 0 Then
                    Dim lstheight, lstMaxWidth As Long
                    Using graphics As Graphics = _listBox.CreateGraphics()
                        For i As Integer = 0 To _listBox.Items.Count - 1
                            lstheight += _listBox.GetItemHeight(i)
                            ' it item width is larger than the current one
                            ' set it to the new max item width
                            ' GetItemRectangle does not work for me
                            ' we add a little extra space by using '_'
                            ' Dim itemWidth As Integer = CInt(graphics.MeasureString(DirectCast(_listBox.Items(i), [String]) + "_", _listBox.Font).Width)
                            Dim itemWidth = CInt(graphics.MeasureString(DirectCast(_listBox.Items(i)("searchData"), [String]) + "_", _listBox.Font).Width)
                            lstMaxWidth = If((lstMaxWidth < itemWidth), itemWidth, lstMaxWidth)
                        Next
                    End Using
                    _listBox.SelectedIndex = 0
                    _listBox.Height = lstheight + 15
                    _listBox.Width = lstMaxWidth + 15
                    Me.Focus()
                Else
                    ResetListBox()
                End If
                Me.ResumeLayout()
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

            IsItemSelected = True
            Me.Text = newTag

        End If

    End Sub

    Private Sub _listBox_MouseClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles _listBox.MouseClick
        Try
            If _listBox.Visible Then
                'InsertWord(DirectCast(_listBox.SelectedItem, [String]))
                InsertWord(DirectCast(_listBox.SelectedItem("searchData"), [String]))
                ResetListBox()
                _SearchValue = Me.Text
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub AndroidSearchTextBox_Leave(sender As Object, e As EventArgs)
    '    Call ResetListBox()
    'End Sub
    Private Sub _listBox_MouseHover(sender As Object, e As EventArgs) Handles _listBox.MouseHover
        IsMouseOverList = True
    End Sub

    Private Sub _listBox_MouseLeave(sender As Object, e As EventArgs) Handles _listBox.MouseLeave
        IsMouseOverList = False
    End Sub

    Private Sub AndroidSearchTextBox_Click(sender As Object, e As EventArgs) Handles Me.Click
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub

End Class

#Region "Commented Code"
'<Serializable()>
'Public Class lstData

'    Private _ArticleCode As String
'    Public Property ArticleCode() As String
'        Get
'            Return _ArticleCode
'        End Get
'        Set(ByVal value As String)
'            _ArticleCode = value
'        End Set
'    End Property

'    Private _ArticleName As String
'    Public Property ArticleName() As String
'        Get
'            Return _ArticleName
'        End Get
'        Set(ByVal value As String)
'            _ArticleName = value
'        End Set
'    End Property

'    Private _ArticleDesc As String
'    Public Property ArticleDesc() As String
'        Get
'            Return _ArticleDesc
'        End Get
'        Set(ByVal value As String)
'            _ArticleDesc = value
'        End Set
'    End Property



'End Class
#End Region
