Imports C1.Win.C1FlexGrid

Public Class HostedControl
    Public _flex As C1FlexGrid
    Public _ctl As Control
    Public _row As Row
    Public _col As Column
    Public _locationX As Integer
    Public _buttonWidth As Integer

    Public Sub New(ByVal flex As C1FlexGrid, ByVal hosted As Control, ByVal row As Integer, ByVal col As Integer, ByVal locationX As Integer, ByVal buttonWidth As Integer)
        'save info
        _flex = flex
        _ctl = hosted
        _row = flex.Rows(row)
        _col = flex.Cols(col)
        _locationX = locationX
        _buttonWidth = buttonWidth

        'insert hosted control into grid
        _flex.Controls.Add(_ctl)
    End Sub

    Public Function UpdatePosition() As Boolean
        'get cell rect
        Dim rc As Rectangle = _flex.GetCellRect(_row.Index, _col.Index, False)

        If _row.Index <= 0 Then
            _ctl.Visible = False
            Return True
        End If

        'hide control if out of range
        If (rc.Width <= 0 OrElse rc.Height <= 0) Then
            _ctl.Visible = False
            Return True
        Else
            rc.Location = New System.Drawing.Point(rc.Location.X + _locationX + 4 - _buttonWidth, rc.Location.Y)
        End If

        'move the control and show it

        _ctl.Bounds = rc
        _ctl.Visible = True

        'done
        Return True
    End Function

End Class