Public Class frmSalesOrderSearch
#Region "Global Variable's & Property's"
    Public search() As String 
    Public _IsSOAssignedByOtherSiteSelected As Boolean
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
    Dim xFrmWidth As Integer = 0
#End Region
#Region "Global Variable's for Class"
    Dim dtData As DataTable
#End Region
#Region "Class Events"
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub
    Private Sub frmCommonSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try           
            rbnSOCreatedAtThisSite.Checked = True
            'FillGrid()
            'dgSearch.Focus()
            FillGrid()
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                ThemeChange()
            End If
            '  Me.Width = xFrmWidth - 879
            'code added by roshan for issue id 2891
            If dgSearch.RowCount = 0 Then
                dgSearch.Splits(0).DisplayColumns(0).Width = 80
                Me.cmdCancel.Location = New System.Drawing.Point(600, 12)
                Me.cmdOk.Location = New System.Drawing.Point(500, 12)

                Me.Width = xFrmWidth - 800

            Else
                Me.Width = xFrmWidth - 879
            End If
            dgSearch.Width = xFrmWidth
            dgSearch.ExtendRightColumn = True
            dgSearch.Focus()
        Catch ex As Exception
            LogException(ex)
        End Try
        SetCulture(Me, Me.Name)
    End Sub
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Try
            Dim i As Integer
            If dgSearch.Row = -1 Then Exit Sub            
            If dgSearch.Row >= 0 Then
                Array.Resize(search, dgSearch.Columns.Count)
                For i = 0 To dgSearch.Columns.Count - 1
                    search(i) = IIf(dgSearch.Item(dgSearch.Row, i) Is System.DBNull.Value, "", dgSearch.Item(dgSearch.Row, i))
                Next
            End If
            If rbtSOAssignedToThisSite.Checked Then
                _IsSOAssignedByOtherSiteSelected = True
            End If
            Me.Close()
        Catch ex As Exception
            ShowMessage(getValueByKey("CS001"), "CS001 - " & getValueByKey("CLAE05"))
            'ShowMessage("Result is not properly intialized", "Information")
        End Try
    End Sub
    Private Sub dgSearch_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgSearch.DoubleClick
        Try
            Dim i As Integer
            If dgSearch.Row = -1 Then Exit Sub            
            If dgSearch.Row >= 0 Then
                Array.Resize(search, dgSearch.Columns.Count)
                For i = 0 To dgSearch.Columns.Count - 1
                    search(i) = IIf(dgSearch.Item(dgSearch.Row, i) Is System.DBNull.Value, "", dgSearch.Item(dgSearch.Row, i))
                Next
            End If
            If rbtSOAssignedToThisSite.Checked Then
                _IsSOAssignedByOtherSiteSelected = True
            End If
            Me.Close()
        Catch ex As Exception
            ShowMessage(getValueByKey("CS001"), "CS001 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
#End Region
#Region "Private Method's & Functions"
    ''' <summary>
    ''' Attach the Data to Grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillGrid()
        Try
            If Not dtData Is Nothing Then
                Dim dv As New DataView(dtData, "", "", DataViewRowState.CurrentRows)
                dgSearch.DataSource = dv
                Dim count As Int32 = dv.Count
                lblCount.Text = count

                If (dgSearch.Splits(0).DisplayColumns.Count > 0) Then
                    dgSearch.Splits(0).DisplayColumns(0).AutoSize()
                End If
                For index = 0 To Me.dgSearch.Splits(0).DisplayColumns.Count - 1
                    If dgSearch.Splits(0).DisplayColumns(index).DataColumn.DataType = GetType(DateTime) Then
                        dgSearch.Splits(0).DisplayColumns(index).DataColumn.EnableDateTimeEditor = True
                        dgSearch.Splits(0).DisplayColumns(index).DataColumn.NumberFormat = "g"
                    End If
                    xFrmWidth = xFrmWidth + dgSearch.Splits(0).DisplayColumns(index).Width
                Next
            Else
                ShowMessage(getValueByKey("CS002"), "CS002 - " & getValueByKey("CLAE05"))
                'ShowMessage("No Data found", "Information")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
#End Region

    Private Sub dgSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgSearch.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                cmdOK_Click(cmdOk, New EventArgs)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.Text = getValueByKey("Crs018")
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Private Sub rbtSOAssignedToThisSite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtSOAssignedToThisSite.CheckedChanged
        Try
            Dim dsSearch As DataSet
            'dsSearch = objSO.SearchSalesOrderWithPendingDelivery(clsAdmin.SiteCode)
            dsSearch = objSO.GetSearchSalesOrder(clsAdmin.SiteCode, False, True)
            SetData = dsSearch.Tables(0)
            FillGrid()
            dgSearch.Focus()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub rbnSOCreatedAtThisSite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbnSOCreatedAtThisSite.CheckedChanged
        Try
            Dim dsSearch As DataSet
            dsSearch = objSO.GetSearchSalesOrder(clsAdmin.SiteCode)
            SetData = dsSearch.Tables(0)
            FillGrid()
            dgSearch.Focus()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function ThemeChange()
        Me.BackColor = Color.FromArgb(134, 134, 134)
        cmdOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdOk.BackColor = Color.Transparent
        cmdOk.BackColor = Color.FromArgb(0, 107, 163)
        cmdOk.ForeColor = Color.FromArgb(255, 255, 255)
        cmdOk.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmdOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdOk.FlatStyle = FlatStyle.Flat
        cmdOk.FlatAppearance.BorderSize = 0
        cmdOk.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdCancel.BackColor = Color.Transparent
        cmdCancel.BackColor = Color.FromArgb(0, 107, 163)
        cmdCancel.ForeColor = Color.FromArgb(255, 255, 255)
        cmdCancel.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmdCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdCancel.FlatStyle = FlatStyle.Flat
        cmdCancel.FlatAppearance.BorderSize = 0
        cmdCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        dgSearch.Font = New Font("Neo Sans", 8, FontStyle.Regular)

        'dgSearch.Splits(0).HeadingStyle.BackColor = Color.FromArgb(177, 227, 253)
        'dgSearch.Splits(0).HighLightRowStyle.BackColor = Color.LightBlue
        'dgSearch.Splits(0).HighLightRowStyle.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        'dgSearch.Splits(0).HighLightRowStyle.BackColor2 = Color.LightBlue
        Me.dgSearch.Splits(0).ColumnCaptionHeight = 40
        dgSearch.Styles(0).BackColor = Color.FromArgb(255, 255, 255)
        dgSearch.Styles(0).Font = New Font("Neo Sans", 9, FontStyle.Regular)
        dgSearch.Styles(1).Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgSearch.Styles(1).BackColor = Color.FromArgb(177, 227, 253)
        dgSearch.Splits(0).Style.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        dgSearch.Styles(5).Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgSearch.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Custom
        dgSearch.HighLightRowStyle.BackColor = Color.LightBlue
        dgSearch.HighLightRowStyle.ForeColor = Color.WhiteSmoke
        CtrlLabel1.ForeColor = Color.FromArgb(255, 255, 255)
        CtrlLabel1.BorderStyle = BorderStyle.None
        CtrlLabel1.BackColor = Color.Transparent
        CtrlLabel1.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        lblCount.ForeColor = Color.FromArgb(255, 255, 255)
        lblCount.BorderStyle = BorderStyle.None
        lblCount.BackColor = Color.Transparent
        lblCount.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgSearch.RowHeight = 20
        rbnSOCreatedAtThisSite.ForeColor = Color.FromArgb(255, 255, 255)
        rbnSOCreatedAtThisSite.BackColor = Color.Transparent
        rbtSOAssignedToThisSite.ForeColor = Color.FromArgb(255, 255, 255)
        rbtSOAssignedToThisSite.BackColor = Color.Transparent
        Me.dgSearch.Style.Borders.BorderType = C1.Win.C1TrueDBGrid.BorderTypeEnum.None
    End Function
End Class