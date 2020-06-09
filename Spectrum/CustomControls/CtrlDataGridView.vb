Public Class CtrlDataGridView
    Inherits System.Windows.Forms.DataGridView

    Private Sub TabPressed(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Tab Then
                If Me.CurrentCell IsNot Nothing Then
                    If Me.CurrentCell.RowIndex >= 0 AndAlso Me.CurrentCell.ColumnIndex >= 0 Then
                        Me.CurrentCell = GetNextCell(Me.CurrentCell)
                    End If
                End If
                e.Handled = True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function GetNextCell(ByVal currentCell As DataGridViewCell) As DataGridViewCell
        Try            
            Dim i As Integer = 0
            Dim nextCell As DataGridViewCell = currentCell
            Do
                Dim nextCellIndex As Integer = (nextCell.ColumnIndex + 1) Mod Me.ColumnCount
                Dim nextRowIndex As Integer = IIf(nextCellIndex = 0, (nextCell.RowIndex + 1) Mod Me.RowCount, nextCell.RowIndex)
                nextCell = Me.Rows(nextRowIndex).Cells(nextCellIndex)
                i = i + 1
            Loop While (i < (Me.RowCount * Me.ColumnCount) And nextCell.ReadOnly)
            Return nextCell
        Catch ex As Exception
            LogException(ex)
            Return currentCell
        End Try
    End Function
End Class
