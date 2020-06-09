Imports SpectrumBL
Public Class frmViewMergeOrders
    Dim dtLoad As New DataTable
    Dim clsCash As New clsCashMemo

    Private _MergeId As Int64
    Public Property MergeId() As Int64
        Get
            Return _MergeId
        End Get
        Set(ByVal value As Int64)
            _MergeId = value
        End Set
    End Property


    Private Sub frmViewMergeOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            dtLoad = clsCash.LoadMergeOrders()
            If dtLoad.Rows.Count = 0 Then
                btnLoad.Enabled = False
                btnEdit.Enabled = False
                btnDelete.Enabled = False
                Exit Sub
            End If
            Call GridRefresh()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            Dim mergid As Integer
            Dim selectedRows() As DataRow = dtLoad.Select("Select=True", "", DataViewRowState.CurrentRows)
            If (selectedRows.Count = 0) Then
                ' ShowMessage(getValueByKey("DIN019"), "DIN019-" & getValueByKey("DIN030"))
                'ShowMessage("Please select at least Order", " " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            For Each dr As DataRow In selectedRows
                mergid = dr("MergeId")
            Next
            Dim objMerge As New frmMergeOrder(mergid)
            objMerge.ShowDialog()
            dtLoad = clsCash.LoadMergeOrders()
            Call GridRefresh()
            If objMerge.DialogResult = Windows.Forms.DialogResult.OK Then
            ElseIf objMerge.DialogResult = Windows.Forms.DialogResult.Cancel Then
            Else
                Me.DialogResult = Windows.Forms.DialogResult.Yes
                MergeId = mergid
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            Dim selectedRows() As DataRow = dtLoad.Select("Select=True", "", DataViewRowState.CurrentRows)
            If (selectedRows.Count = 0) Then
                'ShowMessage(getValueByKey("DIN019"), "DIN019-" & getValueByKey("DIN030"))
                'ShowMessage("Please select at least Order", " " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            For Each dr As DataRow In selectedRows
                MergeId = dr("MergeId")
            Next
            Me.DialogResult = Windows.Forms.DialogResult.Yes
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub RefreshPrinterGrid()
     
        dgMainGrid.DataSource = dtLoad
        dgMainGrid.Cols("Select").Caption = ""
        dgMainGrid.Cols("Select").Width = 30
        dgMainGrid.Cols("Name").AllowEditing = False
        dgMainGrid.Cols("Name").Width = 125
        dgMainGrid.Cols("Orders").AllowEditing = False
        dgMainGrid.Cols("MergeId").Visible = False
        dgMainGrid.Rows(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

    End Sub
   
    Private Sub dgMainGrid_AfterEdit(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgMainGrid.AfterEdit
        If dgMainGrid.Cols(e.Col).Name.ToUpper.Equals("SELECT") Then
            Dim row = e.Row
            For index = 1 To dgMainGrid.Rows.Count - 1
                dgMainGrid.Rows(index)("SELECT") = 0
            Next
            dgMainGrid.Rows(row)("SELECT") = 1
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Dim selectedRows() As DataRow = dtLoad.Select("Select=True", "", DataViewRowState.CurrentRows)
            If (selectedRows.Count = 0) Then
                'ShowMessage(getValueByKey("DIN019"), "DIN019-" & getValueByKey("DIN030"))
                'ShowMessage("Please select at least Order", " " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            For Each dr As DataRow In selectedRows
                MergeId = dr("MergeId")
                If clsCash.DeleteMergeOrder(MergeId) = True Then
                    ShowMessage(getValueByKey("DIN035"), "DIN035-" & getValueByKey("DIN030"))
                    'ShowMessage("Delete Order Successfully", " " & getValueByKey("CLAE04"))
                End If
            Next
            dtLoad = clsCash.LoadMergeOrders()
            Call GridRefresh()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub dgMainGrid_AfterSelChange(sender As Object, e As C1.Win.C1FlexGrid.RangeEventArgs) Handles dgMainGrid.AfterSelChange
        dgMainGrid.AutoSizeRows()
    End Sub
    Public Function GridRefresh()
        With dgMainGrid
            .VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
            '.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns
            .SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
            .ScrollBars = ScrollBars.Vertical
            .ExtendLastCol = False
        End With
        ' MergeId = dtLoad.Rows(0)("MergeId")
        '---- Update Split values 
        For index = 0 To dtLoad.Rows.Count - 1
            Dim alternativeVbcrlf As Boolean = False
            If Len(dtLoad.Rows(index)(2).ToString) > 0 Then
                Dim drp() = dtLoad.Rows(index)(2).ToString.Split(",")
                Dim tempStrBuilder As New System.Text.StringBuilder
                For index2 = 0 To drp.Length - 1
                    If alternativeVbcrlf Then
                        tempStrBuilder.Append(drp(index2).ToString & vbCrLf)
                    Else
                        tempStrBuilder.Append(drp(index2).ToString)
                    End If
                    alternativeVbcrlf = Not alternativeVbcrlf

                Next
                dtLoad.Rows(index)(2) = tempStrBuilder.ToString()
            End If
        Next

        RefreshPrinterGrid()
        dgMainGrid.AutoSizeRows()
        dgMainGrid.Rows(1)("SELECT") = 1
        dgMainGrid.Focus()
        dgMainGrid.Select()
    End Function

    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        dgMainGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        dgMainGrid.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgMainGrid.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        dgMainGrid.Rows.MinSize = 25
        dgMainGrid.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        dgMainGrid.Styles.Fixed.Font = New Font("Neo Sans", 11, FontStyle.Bold)
        dgMainGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgMainGrid.CellButtonImage = Global.Spectrum.My.Resources.Delete
        'C1Sizer1.Grid.Rows(1).Size = 25

        btnEdit.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnEdit.BackColor = Color.Transparent
        btnEdit.BackColor = Color.FromArgb(0, 107, 163)
        btnEdit.ForeColor = Color.FromArgb(255, 255, 255)
        btnEdit.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        btnEdit.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnEdit.FlatStyle = FlatStyle.Flat
        btnEdit.FlatAppearance.BorderSize = 0
        btnEdit.TextAlign = ContentAlignment.MiddleCenter
        btnEdit.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnLoad.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnLoad.BackColor = Color.Transparent
        btnLoad.BackColor = Color.FromArgb(0, 107, 163)
        btnLoad.TextAlign = ContentAlignment.MiddleCenter
        btnLoad.ForeColor = Color.FromArgb(255, 255, 255)
        btnLoad.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnLoad.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnLoad.FlatStyle = FlatStyle.Flat
        btnLoad.FlatAppearance.BorderSize = 0
        btnLoad.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnDelete.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnDelete.BackColor = Color.Transparent
        btnDelete.TextAlign = ContentAlignment.MiddleCenter
        btnDelete.BackColor = Color.FromArgb(0, 107, 163)
        btnDelete.ForeColor = Color.FromArgb(255, 255, 255)
        btnDelete.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnDelete.FlatStyle = FlatStyle.Flat
        btnDelete.FlatAppearance.BorderSize = 0
        btnDelete.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

    End Function
End Class