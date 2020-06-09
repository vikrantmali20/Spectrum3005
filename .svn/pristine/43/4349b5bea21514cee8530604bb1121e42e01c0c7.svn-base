Imports C1.Win.C1FlexGrid
Public Class frmDirectReport
    Dim _moduleid, _ReportId, _reportName, _LangCode As String
    Dim dtConfig, _dtReportDtl, dtReportShowDtl As DataTable
    Dim objcomm As New SpectrumBL.clsReports
    Dim DateReq As Boolean = False
    Dim _FromDate, _Todate As DateTime
    Dim groupTotalColList As New Collection
    Dim ColaspedLavel As Int32 = 0
    Public WriteOnly Property fromDate() As DateTime
        Set(ByVal value As DateTime)
            _FromDate = value
        End Set
    End Property
    Public WriteOnly Property ToDate() As DateTime
        Set(ByVal value As DateTime)
            _Todate = value
        End Set
    End Property
    Public Sub New(ByVal Moduleid As String, ByVal ReportId As String, ByVal reportName As String, ByVal dtReport As DataTable, ByVal LangCode As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _moduleid = Moduleid
        _ReportId = ReportId
        _dtReportDtl = dtReport
        _reportName = reportName
        _LangCode = LangCode
        ' Add any initialization after the InitializeComponent() call.

        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub
    Private Sub cmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExport.Click
        Try
            Dim filedlg As New SaveFileDialog()
            'filedlg.InitialDirectory = My.Computer.FileSystem.CurrentDirectory + "\ExcelSheet"
            filedlg.DefaultExt = "xlsx"
            filedlg.Filter = "Excel File|*.xlsx"
            Dim result As DialogResult = filedlg.ShowDialog
            If result = Windows.Forms.DialogResult.OK And filedlg.FileName <> "" Then
                'dgFinalReport.SaveExcel(filedlg.FileName, "Sheet1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells)
                dgFinalReport.SaveExcel(filedlg.FileName, "Sheet1", FileFlags.IncludeFixedCells + FileFlags.OpenXml)
                MsgBox("File has been saved to path " + filedlg.FileName)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmDirectReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrSql1, strSql As String
        Dim strOrderBy As String = vbNullString
        Dim strCondition As String = vbNullString
        Dim sHeading As String
        Dim IntCols As Integer
        Dim SaveQuery As String
        Dim selectedColCount As Integer = 0
        Dim strFinalCondition As String = ""
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            strSql = ""
            IntCols = 0
            sHeading = ""
            dtReportShowDtl = New DataTable
            dtReportShowDtl = objcomm.FillDirectReportDetail(_ReportId, _LangCode)
            dgFinalReport.AllowSorting = AllowSortingEnum.SingleColumn
            Dim i As Int16 = 1
            groupTotalColList.Clear()
            For Each dr As DataRow In dtReportShowDtl.Select("SEL=1", "", DataViewRowState.CurrentRows)
                sHeading = sHeading & dr("ColumnNames").ToString() & "|"  'dgSelection.Item(i, 1) & "|"
                strSql = strSql & dr("ColumnNames").ToString() & ","
                If dr("Total").ToString() = "True" Then
                    groupTotalColList.Add(IntCols)
                End If
                IntCols += 1
            Next
            Dim ViewName As String = _dtReportDtl.Rows(0)("ViewName").ToString()
            If strOrderBy <> vbNullString Then strOrderBy = Mid(strOrderBy, 1, Len(strOrderBy) - 1)
            strSql = Mid(strSql, 1, Len(strSql) - 1)
            StrSql1 = "Select " & strSql & " from " & ViewName & " where 1=1 "
            SaveQuery = StrSql1
            DateReq = IIf(_dtReportDtl.Rows(0)("DateApplicable") Is DBNull.Value, False, _dtReportDtl.Rows(0)("DateApplicable"))
            ColaspedLavel = IIf(_dtReportDtl.Rows(0)("ColapsedTreeLevel").ToString().ToUpper() = "", 0, _dtReportDtl.Rows(0)("ColapsedTreeLevel"))
            If DateReq = True Then
                StrSql1 = StrSql1 & "AND CAST(CONVERT(VARCHAR(10),INVDATETIME,101) AS DATETIME) >='" & Format(_FromDate, "dd-MMM-yyyy") & "' AND CAST(CONVERT(VARCHAR(10),INVDATETIME,101) AS DATETIME) <= '" & Format(_Todate, "dd-MMM-yyyy") & "'"
            ElseIf ViewName.Substring(0, 2).ToUpper() = "FN" And DateReq = True Then
                StrSql1 = "Select " & strSql & " from " & ViewName & " ('" & Format(_FromDate, "dd-MMM-yyyy") & "','" & Format(_Todate, "dd-MMM-yyyy") & "') Where 1=1 "
            ElseIf ViewName.Substring(0, 2).ToUpper() = "FN" And DateReq = False Then
                StrSql1 = "Select " & strSql & " from " & ViewName & "() Where 1=1 "
            End If
            strFinalCondition = ""
            If fp_FillGridList(StrSql1, "", "") = True Then
                _chkGrand.Checked = True
                If dgFinalReport.Rows.Count < 50000 Then
                    Dim colno As Integer = 0
                    dgFinalReport.AllowMerging = AllowMergingEnum.RestrictCols
                    dgFinalReport.Cols(0).AllowMerging = True
                    For i = 1 To dgFinalReport.Cols.Count - 1
                        If dgFinalReport.DataSource IsNot Nothing And dgFinalReport.Cols(i).DataType.Name = "String" Then
                            dgFinalReport.Cols(i).AllowMerging = True
                        End If
                    Next
                End If
                'tbDetails.SelectedTab = tbDetails.TabPages(2)
            End If
            'UpdateTotals()
            Me.Text = _reportName
        Catch ex As Exception
        End Try

    End Sub
    Private Sub UpdateTotals()
        Dim i, j, k As Integer
        dgFinalReport.Subtotal(AggregateEnum.Clear)
        If _chkGrand.Checked Then
            For Each k In groupTotalColList
                dgFinalReport.Subtotal(AggregateEnum.Sum, 0, -1, k, "Grand Total")
                For Each dr As DataRow In dtReportShowDtl.Select("ColumnNames='" & dtReportShowDtl.Rows(k)("ColumnNames").ToString() & "' AND AVGON=1", "", DataViewRowState.CurrentRows)
                    dgFinalReport.Subtotal(AggregateEnum.Average, 0, -1, k, "Grand Total")
                    dgFinalReport.Cols(k).Format = "0.00"
                Next
            Next
            dgFinalReport.Tree.Column = 0
        End If
        j = 1
        For Each dr As DataRow In dtReportShowDtl.Select("GroupOn=1", "SEQ", DataViewRowState.CurrentRows)
            For Each k In groupTotalColList
                dgFinalReport.Subtotal(AggregateEnum.Sum, j, i, k, "Total for {0}")
                For Each drs As DataRow In dtReportShowDtl.Select("ColumnNames='" & dtReportShowDtl.Rows(k)("ColumnNames").ToString() & "' AND AVGON=1", "", DataViewRowState.CurrentRows)
                    dgFinalReport.Subtotal(AggregateEnum.Average, j, i, k, "Total for {0}")
                    dgFinalReport.Cols(k).Format = "0.00"
                Next
            Next
            dgFinalReport.Tree.Column = j
            i = i + 1
            j = j + 1
        Next
        If ColaspedLavel > 0 Then
            dgFinalReport.Tree.Show(ColaspedLavel)
        End If

        'For Each i In chkGroupingList.CheckedIndices
        '    For Each k In groupTotalColList
        '        dgFinalReport.Subtotal(AggregateEnum.Sum, j, i, k, "Total for {0}")
        '    Next
        '    dgFinalReport.Tree.Column = i
        'Next

        dgFinalReport.Redraw = True
        dgFinalReport.AutoSizeCols()

    End Sub
    Private Function fp_FillGridList(ByVal sQuery As String, ByVal sGroupCode As String, ByVal sSort As String) As Boolean
        fp_FillGridList = True
        Dim dt As New DataTable
        dt = objcomm.FillDataTable(sQuery)
        If dt.Rows.Count > 0 Then
            dgFinalReport.DataSource = dt
        Else
            dgFinalReport.DataSource = dt
            MsgBox("No Matched Record Found", MsgBoxStyle.Information)
            fp_FillGridList = False
            Me.Close()
        End If

        'attach scaling info to each numeric column
        Dim r1 As Integer = dgFinalReport.Rows.Fixed
        Dim r2 As Integer = dgFinalReport.Rows.Count - 1
        Dim str As String = Nothing
        Dim COLNO As Int16
        Dim barCols(groupTotalColList.Count - 1) As String '= New String() {"QTY", "Price", "linetotal"}

        For COLNO = 0 To groupTotalColList.Count - 1
            'barCols(COLNO) = dgSelection.Item(Int16.Parse(groupTotalColList(COLNO + 1)) + 1, 1)
            barCols(COLNO) = dgFinalReport.Cols(Int16.Parse(groupTotalColList(COLNO + 1))).Name
        Next
        If dgFinalReport.Rows.Count > 1 Then
            For Each str In barCols
                Dim col As Column = dgFinalReport.Cols(str)
                Dim max As Double = dgFinalReport.Aggregate(AggregateEnum.Max, r1, col.Index, r2, col.Index)
                col.UserData = max
            Next
            'Dim iCol As Integer
            'For iCol = 0 To dgFinalReport.Cols.Count - 1
            '    dgFinalReport.Cols(iCol).Width = (dgFinalReport.Width / dgFinalReport.Cols.Count) - 3
            'Next
        End If

        'Column names assign for diff lang
        For Each Row As DataRow In dtReportShowDtl.Rows
            dgFinalReport.Cols(Row("ColumnNames").ToString()).Caption = Row("Description").ToString()
        Next


        dgFinalReport.Rows.Move(0, 3)
        dgFinalReport.Rows(0)(0) = "Generated On: " & Now.Date & " At " & Now.ToString("hh:MM:ss")
        dgFinalReport.Rows(1)(0) = _reportName
        dgFinalReport.Rows(2)(0) = "For: " & _FromDate & " - " & _Todate



        'turn on ownerdraw
        dgFinalReport.DrawMode = DrawModeEnum.OwnerDraw
        'set up styles
        dgFinalReport.Cols(0).DataType = GetType(System.DateTime)
        dgFinalReport.Styles.Fixed.BackColor = Color.Aqua
        dgFinalReport.Styles.Fixed.ForeColor = Color.Black
        Dim s As CellStyle = dgFinalReport.Styles(CellStyleEnum.Subtotal0)
        s.BackColor = Color.LightBlue
        s.ForeColor = Color.Red
        s.Font = New Font(dgFinalReport.Font, FontStyle.Bold)

        s = dgFinalReport.Styles(CellStyleEnum.Subtotal1)
        s.BackColor = Color.LightCyan
        s.ForeColor = Color.Red
        s = dgFinalReport.Styles(CellStyleEnum.Subtotal2)
        s.BackColor = Color.LightGreen
        s.ForeColor = Color.Red
        s = dgFinalReport.Styles(CellStyleEnum.Subtotal3)
        s.BackColor = Color.LightGray
        s.ForeColor = Color.Red
        s = dgFinalReport.Styles(CellStyleEnum.Subtotal4)
        s.BackColor = Color.LightSalmon
        s.ForeColor = Color.Red
        s = dgFinalReport.Styles(CellStyleEnum.Subtotal5)
        s.BackColor = Color.LightSteelBlue
        s.ForeColor = Color.Red
        s = dgFinalReport.Styles(CellStyleEnum.GrandTotal)
        s.BackColor = Color.Lime
        s.ForeColor = Color.Red

        'more(setup)
        dgFinalReport.SubtotalPosition = SubtotalPositionEnum.BelowData
        dgFinalReport.AllowDragging = AllowDraggingEnum.None
        dgFinalReport.AllowEditing = False
        'Dim c As Integer
        'If dgFinalReport.Rows.Count > 1 Then
        '    For c = 0 To dgFinalReport.Cols.Count - 1
        '        If dgFinalReport.DataSource IsNot Nothing And dgFinalReport.Cols(c).DataType.Name = "Decimal" Then
        '            dgFinalReport.Cols(c).Format = "#,###.00"
        '        End If
        '    Next
        'End If
        UpdateTotals()
    End Function

    Private Sub _chkGrand_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _chkGrand.CheckStateChanged
        UpdateTotals()
    End Sub
    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        _chkGrand.ForeColor = Color.FromArgb(255, 255, 255)
        _chkGrand.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        cmdExport.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdExport.BackColor = Color.Transparent
        cmdExport.BackColor = Color.FromArgb(0, 107, 163)
        cmdExport.ForeColor = Color.FromArgb(255, 255, 255)
        cmdExport.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdExport.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdExport.FlatStyle = FlatStyle.Flat
        cmdExport.FlatAppearance.BorderSize = 0
        cmdExport.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        dgFinalReport.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        dgFinalReport.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgFinalReport.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        dgFinalReport.Rows.MinSize = 25
        dgFinalReport.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        dgFinalReport.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgFinalReport.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgFinalReport.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgFinalReport.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgFinalReport.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
    End Function
End Class
