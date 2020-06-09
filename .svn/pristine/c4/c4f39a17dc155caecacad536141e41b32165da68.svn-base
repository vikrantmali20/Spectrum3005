''' <summary>
''' This Class is Used for Common View of Data
''' </summary>
''' <CreatedBy>Rama Ranjan Jena</CreatedBy>
''' <UpdatedBy></UpdatedBy>
''' <UpdatedOn></UpdatedOn>
''' <remarks></remarks>
Public Class frmNCommonView
#Region "Global Varibale's & Property's"
    Public search() As String
    Public ColumnName() As String
    ''' <summary>
    ''' Set the data from Outside to Show
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property SetData() As DataTable
        Set(ByVal value As DataTable)
            dtData = value
        End Set
    End Property
    ''' <summary>
    ''' Return the data 
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public ReadOnly Property GetData() As DataTable
        Get
            Return dtData
        End Get
    End Property
    Private _selectRow As DataRow
    Public ReadOnly Property GetResultRow() As DataRow
        Get
            Return _selectRow
        End Get
    End Property
#End Region
#Region "Global Variable For Class"
    Dim dtData As DataTable
#End Region
#Region "Class Events"
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        'added by adil 
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub frmCommonView_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            FillGrid()
            SetCulture(Me, Me.Name)
            cmdOk.DialogResult = Windows.Forms.DialogResult.OK
            cmdCancel.DialogResult = Windows.Forms.DialogResult.Cancel
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
        'SetCulture(Me, Me.Name)
    End Sub
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Try
            Dim i As Integer
            If dgSearch.Row = -1 Then Exit Sub
            If dgSearch.Row >= 0 Then
                Array.Resize(search, dgSearch.Cols.Count)
                Dim strfilter As String
                For i = 0 To dgSearch.Cols.Count - 1
                    search(i) = IIf(dgSearch.Item(dgSearch.Row, i) Is System.DBNull.Value, "", dgSearch.Item(dgSearch.Row, i))
                    strfilter = dgSearch.Cols(0).Name & "='" & dgSearch.Item(dgSearch.Row, 0) & "' AND "
                Next
                If strfilter.Length > 0 Then
                    strfilter = strfilter & " 0=0"
                End If
                Dim dr() As DataRow = dtData.Select(strfilter, "", DataViewRowState.CurrentRows)
                If dr.Length > 0 Then
                    _selectRow = dr(0)
                End If
            End If
            Me.Close()
        Catch ex As Exception
            ShowMessage(getValueByKey("CVW001"), "CVW001 - " & getValueByKey("CLAE04"))
            LogException(ex)
        End Try
    End Sub
    Private Sub dgSearch_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgSearch.DoubleClick
        Try
            Dim i As Integer
            If dgSearch.Row = -1 Then Exit Sub
            If dgSearch.Row >= 0 Then
                Dim strfilter As String
                Array.Resize(search, dgSearch.Cols.Count)
                For i = 0 To dgSearch.Cols.Count - 1
                    search(i) = IIf(dgSearch.Item(dgSearch.Row, i) Is System.DBNull.Value, "", dgSearch.Item(dgSearch.Row, i))
                    strfilter = dgSearch.Cols(0).Name & "='" & dgSearch.Item(dgSearch.Row, 0) & "' AND "
                Next
                If strfilter.Length > 0 Then
                    strfilter = strfilter & " 0=0"
                End If
                Dim dr() As DataRow = dtData.Select(strfilter, "", DataViewRowState.CurrentRows)
                If dr.Length > 0 Then
                    _selectRow = dr(0)
                End If
            End If
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception
            ShowMessage(getValueByKey("CVW001"), "CVW001 - " & getValueByKey("CLAE04"))
            LogException(ex)
        End Try
    End Sub
#End Region
#Region "Attach Data to Grid And Show Only Specific Columns"
    Private Sub FillGrid()
        Try

            If Not dtData Is Nothing Then

                'Array.Resize(ColumnName, dtData.Columns.Count)
                'Dim i As Integer = 0
                'For Each col As DataColumn In dtData.Columns
                '    If col.ColumnName <> "EAN" And col.ColumnName <> "SELLINGPRICE" Then
                '        ColumnName(i) = col.ColumnName
                '    End If
                '    i = i + 1
                'Next

                Dim dv As New DataView(dtData, "", "", DataViewRowState.CurrentRows)
                dgSearch.DataSource = dv
                For Each col As C1.Win.C1FlexGrid.Column In dgSearch.Cols
                    If col.DataType.ToString() = "System.Boolean" Then
                        col.AllowEditing = True
                    Else
                        col.AllowEditing = False
                    End If
                Next
                If Not ColumnName Is Nothing Then
                    For Each Str As String In ColumnName
                        HideColumns(dgSearch, False, Str)
                    Next
                End If
                lblCount.Text = dv.Count
                dgSearch.AutoResize = True
            Else
                ShowMessage(getValueByKey("CVW002"), "CVW002 - " & getValueByKey("CLAE04"))
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
#End Region

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Public Function ThemeChange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)

        'dgSearch
        '
        Me.dgSearch.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        Me.dgSearch.Styles.Highlight.BackColor = Color.FromArgb(153, 255, 255)
        Me.dgSearch.Styles.Highlight.ForeColor = Color.Black
        Me.dgSearch.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        Me.dgSearch.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        Me.dgSearch.Rows.MinSize = 26
        Me.dgSearch.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        Me.dgSearch.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgSearch.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgSearch.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgSearch.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgSearch.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.dgSearch.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        Me.dgSearch.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgSearch.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgSearch.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.dgSearch.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)

        'GroupBox1
        '
        Me.GroupBox1.BackColor = Color.FromArgb(134, 134, 134)
        Me.CtrlLabel1.BackColor = Color.Transparent
        Me.CtrlLabel1.ForeColor = Color.White
        Me.CtrlLabel1.BorderColor = Color.FromArgb(134, 134, 134)
        Me.CtrlLabel1.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        Me.lblCount.BackColor = Color.Transparent
        Me.lblCount.ForeColor = Color.White
        Me.lblCount.BorderColor = Color.FromArgb(134, 134, 134)
        Me.lblCount.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        'cmdOk
        '

        Me.cmdOk.BackColor = Color.FromArgb(0, 107, 163)
        Me.cmdOk.ForeColor = Color.FromArgb(255, 255, 255)
        Me.cmdOk.Location = New System.Drawing.Point(633, 11)
        Me.cmdOk.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'Me.cmdOk.MaximumSize = New Size(124, 42)
        'Me.cmdOk.MinimumSize = New Size(124, 42)
        'Me.cmdOk.Size = New System.Drawing.Size(124, 42)
        Me.cmdOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.cmdOk.FlatStyle = FlatStyle.Flat
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        'cmdCancel
        '
        Me.cmdCancel.BackColor = Color.FromArgb(0, 107, 163)
        Me.cmdCancel.ForeColor = Color.FromArgb(255, 255, 255)
        Me.cmdCancel.Location = New System.Drawing.Point(712, 11)

        Me.cmdCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'Me.cmdCancel.MaximumSize = New Size(124, 42)
        'Me.cmdCancel.MinimumSize = New Size(124, 42)
        'Me.cmdCancel.Size = New System.Drawing.Size(124, 42)
        Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.cmdCancel.FlatStyle = FlatStyle.Flat
        Me.cmdCancel.FlatAppearance.BorderSize = 0
        Me.cmdCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

    End Function
End Class
