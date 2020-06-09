Imports SpectrumBL
Imports C1.Win.C1FlexGrid
Public Class frmReportProducer
    Dim dtConfig, _dtReportDtl, dtRptTotalDtl As DataTable
    Dim objcomm As New clsReports
    Dim _moduleid, _ReportId, _reportName, _langCode As String
    Dim dsReport As DataSet
    Dim viewName As String
    Dim DateReq As Boolean
    Dim groupTotalColList As New Collection
    Dim unique As Int32 = 1
    Dim ColaspedLavel As Int32 = 0
    Private Sub frmReportProducer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            dtpFromDate.Value = Now
            dtpToDate.Value = Now
            Dim ShowFilter As Boolean = False
            dsReport = New DataSet
            ClearTabs()
            dtConfig = objcomm.FillReportSettings(_moduleid, _ReportId, _langCode)
            If Not dtConfig Is Nothing AndAlso dtConfig.Rows.Count > 0 Then
                Dim iPage As Integer = 1
                Dim iCount As Integer
                Dim strKey As String
                'Dim dsTemp As New DataSet
                Dim newGrdSales As C1.Win.C1FlexGrid.C1FlexGrid
                For iCount = 1 To dtConfig.Rows.Count
                    'gbSales.Visible = True

                    If iPage = 1 Then
                        grdSales.Visible = True
                        grdSales.Tag = dtConfig.Rows(iCount - 1).Item("LinkedFieldName").ToString
                        SSSalesAnalysis.TabPages(iPage - 1).Text = dtConfig.Rows(iCount - 1).Item("ConfigName").ToString
                        SSSalesAnalysis.TabPages(iPage - 1).Name = "tb" & iPage
                        FillGrid(grdSales, dtConfig.Rows(iCount - 1).Item("TableName").ToString, dtConfig.Rows(iCount - 1).Item("CodeField").ToString, dtConfig.Rows(iCount - 1).Item("DescriptionField").ToString, dtConfig.Rows(iCount - 1).Item("filtertext").ToString)
                        ShowFilter = True
                    Else
                        newGrdSales = New C1.Win.C1FlexGrid.C1FlexGrid
                        newGrdSales.Name = "grdSales" & iCount - 1
                        SSSalesAnalysis.TabPages.Add(dtConfig.Rows(iCount - 1).Item("ConfigName").ToString)
                        SSSalesAnalysis.TabPages(iCount - 1).Controls.Add(newGrdSales)
                        newGrdSales.Size = grdSales.Size 'New System.Drawing.Size(696, 392)
                        newGrdSales.Location = New System.Drawing.Point(6, 6)
                        newGrdSales.Cols(0).Width = 0
                        newGrdSales.ForeColor = Color.Navy
                        newGrdSales.Tag = dtConfig.Rows(iCount - 1).Item("LinkedFieldName").ToString

                        'AddHandler newGrdSales.AfterEdit, AddressOf GridAfterEdit

                        SSSalesAnalysis.TabPages(iPage - 1).Text = dtConfig.Rows(iCount - 1).Item("ConfigName").ToString
                        SSSalesAnalysis.TabPages(iPage - 1).Name = "tb" & iPage
                        FillGrid(newGrdSales, dtConfig.Rows(iCount - 1).Item("TableName").ToString, dtConfig.Rows(iCount - 1).Item("CodeField").ToString, dtConfig.Rows(iCount - 1).Item("DescriptionField").ToString, dtConfig.Rows(iCount - 1).Item("filtertext").ToString)
                        ShowFilter = True
                    End If
                    strKey = dtConfig.Rows(iCount - 1).Item("TableName").ToString + dtConfig.Rows(iCount - 1).Item("CodeField").ToString
                    'lvwFilterList.Items.Add(dsTemp.Tables(0).Rows(iCount - 1).Item("ReportName").ToString, strKey)
                    'lvwFilterList.Items(iCount - 1).Checked = True
                    iPage += 1
                Next
            Else

            End If
            If Not _dtReportDtl Is Nothing AndAlso _dtReportDtl.Rows.Count > 0 Then
                viewName = _dtReportDtl.Rows(0)("ViewName").ToString()
                DateReq = IIf(_dtReportDtl.Rows(0)("DateApplicable").ToString().ToUpper() = "True".ToUpper(), True, False)
                ColaspedLavel = IIf(_dtReportDtl.Rows(0)("ColapsedTreeLevel").ToString().ToUpper() = "", 0, _dtReportDtl.Rows(0)("ColapsedTreeLevel"))
            End If
            If DateReq = False Then
                pnlDateFilter.Enabled = False
            End If
            tbDetails.SelectedIndex = 0
            'Me.Text = _reportName & "  :Select Filter Criteria"
            Me.Text = _reportName & "  " & getValueByKey("RP006")
            If ShowFilter = False And DateReq = False Then
                cmdNextConfig_Click(cmdNextConfig, New System.EventArgs())
                cmdPrevious.Visible = False
            ElseIf ShowFilter = False And DateReq = True Then
                SSSalesAnalysis.Visible = False
                CmdNextFilter.Visible = False
                cmdPreviousFilter.Visible = False
            End If
            SetCulture(Me, Me.Name)
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ClearTabs()
        Try
            Dim i As Int16
            For i = 1 To SSSalesAnalysis.TabPages.Count - 1
                SSSalesAnalysis.TabPages.RemoveAt(1)
            Next i
            If SSSalesAnalysis.Visible = False Then SSSalesAnalysis.Visible = True
        Catch ex As Exception
        End Try
    End Sub
    Public Sub New(ByVal Moduleid As String, ByVal ReportId As String, ByVal reportName As String, ByVal dtReport As DataTable, ByVal LangCode As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _moduleid = Moduleid
        _ReportId = ReportId
        _dtReportDtl = dtReport
        _reportName = reportName
        _langCode = LangCode
        ' Add any initialization after the InitializeComponent() call.

        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub
    Private Sub FillGrid(ByRef Grd As C1.Win.C1FlexGrid.C1FlexGrid, ByVal TableName As String, ByVal FieldName As String, ByVal fieldname2 As String, ByVal filtertext As String)
        Try
            Dim strTempSql = " Select DISTINCT CONVERT(BIT,0) as CHK ,  " & FieldName & "," & fieldname2 & " from " & TableName
            'If filtertext <> "" Then strTempSql = strTempSql & " where 1=1 " & filtertext & ""
            If String.IsNullOrEmpty(filtertext) Then
                strTempSql = strTempSql & " where 1=1 " & filtertext & ""
            End If
            Dim dt As DataTable
            dt = objcomm.FillDataTable(strTempSql)
            dt.TableName = TableName
            If dsReport.Tables.Contains(TableName) = True Then
                'dsReport.Tables(TableName).Clear()
                TableName = TableName & unique
                unique = unique + 1
                dt.TableName = TableName
            End If
            dsReport.Tables.Add(dt)
            If dt.Rows.Count > 0 Then
                Grd.DataSource = Nothing
                Grd.Rows.Count = 2
                'Grd.DataSource = dt
                Grd.DataSource = dsReport.Tables(TableName)
            Else
                Grd.DataSource = Nothing
                Grd.Rows.Count = 2
            End If
            For Each col As C1.Win.C1FlexGrid.Column In Grd.Cols
                If col.Index <> 1 And col.Index <> 0 Then
                    col.AllowEditing = False
                End If
            Next
            'Grd.Cols(1).AllowEditing = True
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Grd.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
                Grd.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
                Grd.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
                Grd.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
                Grd.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
                Grd.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
                Grd.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
                Grd.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
                Grd.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub cmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExport.Click
        Try
            Dim filedlg As New SaveFileDialog()
            'filedlg.InitialDirectory = My.Computer.FileSystem.CurrentDirectory + "\ExcelSheet"
            filedlg.DefaultExt = "xlsx"
            filedlg.Filter = "Excel File|*.xlsx"
            Dim result As DialogResult = filedlg.ShowDialog

            If result = Windows.Forms.DialogResult.OK And filedlg.FileName <> "" Then
                'dgFinalReport.SaveExcel(filedlg.FileName & "4", "Sheet1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells)
                'dgFinalReport.SaveExcel(filedlg.FileName & "5", "Sheet1", FileFlags.IncludeFixedCells + FileFlags.OpenXml)
                'dgFinalReport.SaveGrid(filedlg.FileName, FileFormatEnum.Excel, FileFlags.OpenXml)
                'dgFinalReport.SaveExcel(filedlg.FileName & "2")
                'dgFinalReport.SaveExcel(filedlg.FileName & "3", FileFlags.AsDisplayed)
                '//System.Diagnostics.Process.Start(filedlg.FileName)


                Dim vs As C1.Win.C1FlexGrid.VisualStyle
                Dim ps As New System.Drawing.Printing.PrinterSettings

                vs = dgFinalReport.VisualStyle
                dgFinalReport.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom

                For Each pa As System.Drawing.Printing.PaperSize In ps.PaperSizes
                    If pa.PaperName = "A4″ Then" Then
                        ps.DefaultPageSettings.PaperSize = pa
                        Exit For
                    End If
                Next

                Dim tmp As C1.Win.C1FlexGrid.DrawModeEnum = dgFinalReport.DrawMode
                dgFinalReport.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.Normal

                dgFinalReport.SaveExcel(filedlg.FileName, "Sheet1", CType(C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells Or C1.Win.C1FlexGrid.FileFlags.VisibleOnly Or C1.Win.C1FlexGrid.FileFlags.SaveMergedRanges, C1.Win.C1FlexGrid.FileFlags), ps)


                dgFinalReport.DrawMode = tmp
                dgFinalReport.VisualStyle = vs
                vs = Nothing
                ps = Nothing


                'MsgBox("File has been saved to path " + filedlg.FileName)
                MsgBox(getValueByKey("RP001") & " " + filedlg.FileName, , "RPT001 - " & getValueByKey("CLAE04"))
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub FillItems()
        Dim strTempSql As String
        Dim dt As New DataTable
        Try

            strTempSql = "SELECT CONVERT(BIT,1) AS 'REQ ?',UPPER(COLUMNNAMES) AS ITEMS ,CONVERT(BIT,0) AS 'ORDER BY ?',CONVERT(BIT,0) AS 'GROUP TOTAL FOR ?' FROM DIRECTREPORTDTL  WHERE REPORTCODE='" & _ReportId & "' and sel = 1"
            dt = objcomm.FillDataTable(strTempSql)
            If viewName.Substring(0, 2) = "SP" Then
                If dt.Rows.Count > 0 Then
                    Dim dttemp As DataTable = CreateTempTableForSp()
                    strTempSql = ""
                    For Each Drow As DataRow In dt.Rows
                        strTempSql = strTempSql & "1,"
                    Next
                    strTempSql = strTempSql.Substring(0, strTempSql.Length - 1)
                    strTempSql = "EXEC " & viewName & " " & strTempSql & ""
                    dt = New DataTable
                    dt = objcomm.FillDataTable(strTempSql)
                    For Each Dcol As DataColumn In dt.Columns
                        dttemp.Rows.Add()
                        dttemp.Rows(dt.Rows.Count - 1)(1) = Dcol.ColumnName
                        'strTempSql = strTempSql & "1,"
                    Next
                    dgSelection.DataSource = dttemp
                End If
            Else
                If dt.Rows.Count > 0 Then
                    dtRptTotalDtl = objcomm.FillDirectReportDetail(_ReportId, _langCode)
                    If dtRptTotalDtl.Rows.Count > 0 Then
                        For Each dr As DataRow In dtRptTotalDtl.Select("Total=1", "", DataViewRowState.CurrentRows)
                            For Each drTotal As DataRow In dt.Select("ITEMS='" & dr("ColumnNames").ToString() & "'", "", DataViewRowState.CurrentRows)
                                drTotal(3) = 1
                            Next
                        Next
                        For Each dr As DataRow In dtRptTotalDtl.Select("GroupOn=1", "", DataViewRowState.CurrentRows)
                            For Each drTotal As DataRow In dt.Select("ITEMS='" & dr("ColumnNames").ToString() & "'", "", DataViewRowState.CurrentRows)
                                drTotal(2) = 1
                            Next
                        Next
                    End If
                    dgSelection.DataSource = dt
                Else
                    dgSelection.Rows.Count = 2
                End If
            End If
            For i As Integer = 0 To dgSelection.Cols.Count - 1
                dgSelection.Cols(i).Caption = getValueByKey("RPT01" & i.ToString)
            Next
            'Me.Text = _reportName & "  Select Columns,Grouping,Sorting "
            Me.Text = _reportName & "  " & getValueByKey("RPT009") & " "
        Catch ex As Exception
        End Try
    End Sub
    Private Function CreateTempTableForSp() As DataTable
        Try
            'Caption added by Rohit 

            Dim dt As New DataTable
            Dim col As DataColumn

            col = New DataColumn
            col.DataType = System.Type.GetType("System.Boolean")
            col.ColumnName = "REQ ?"
            col.Caption = getValueByKey("RPT010")
            col.DefaultValue = True
            dt.Columns.Add(col)

            col = New DataColumn
            col.DataType = System.Type.GetType("System.String")
            col.ColumnName = "ITEMS"
            col.Caption = getValueByKey("RPT011")
            dt.Columns.Add(col)

            col = New DataColumn
            col.DataType = System.Type.GetType("System.Boolean")
            col.ColumnName = "ORDER BY ?"
            col.Caption = getValueByKey("RPT012")
            col.DefaultValue = False
            dt.Columns.Add(col)

            col = New DataColumn
            col.DataType = System.Type.GetType("System.Boolean")
            col.ColumnName = "GROUP TOTAL FOR ?"
            col.Caption = getValueByKey("RPT013")
            col.DefaultValue = False
            dt.Columns.Add(col)

            Return dt
        Catch ex As Exception

        End Try
    End Function
    Private Sub cmdNextConfig_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNextConfig.Click
        Try
            FillItems()
            tbDetails.SelectedIndex = 1
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ExecuteQueryForSP(ByVal strCondition As String, ByVal strOrderBy As String, ByVal StrSql1 As String)

        Try
            Dim Parameters As String = ""
            Dim sQry As String = ""
            Dim sOrderBy As String = ""
            Dim sTotal As String
            If DateReq = False Then Parameters = "'" & Format(dtpFromDate.Value, "dd-MMM-yyyy") & "','" & Format(dtpToDate.Value, "dd-MMM-yyyy") & "'"

            If DateReq = True Then
                Parameters = "'" & Format(dtpFromDate.Value, "dd-MMM-yyyy") & "','" & Format(dtpToDate.Value, "dd-MMM-yyyy") & strCondition & ""
            End If
            Dim strTempSql As String
            Dim dset As New DataSet
            Dim ds, ds1 As New DataSet
            If sQry.Trim <> "" Then sQry = Mid(sQry, 1, Len(sQry) - 1)
            If sQry.Trim <> "" Then sQry = sQry.Substring(1)
            If sQry.Trim <> "" Then If Mid(sQry, Len(sQry) - 2, Len(sQry) - 1) = "'''" Then sQry = Mid(sQry, 1, Len(sQry) - 1)

            sOrderBy = strOrderBy

            StrSql1 = "exec " & viewName & " " & Parameters & ""

            'Dim da As New SqlDataAdapter(StrSql1, conRepos)
            'da.Fill(ds)
            'ds1 = ds.Clone
            Dim dt As New DataTable
            dt = objcomm.FillDataTable(StrSql1)
            If fp_FillGridList(StrSql1, "", "") = True Then
                dgFinalReport.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictCols
                ' dgList.AllowMerging = AllowMergingEnum.Free
                Dim i%
                For Each Dcol As C1.Win.C1FlexGrid.Column In dgFinalReport.Cols
                    Dcol.AllowMerging = True
                    'dgList.Cols(i).AllowMerging = True
                Next
                tbDetails.SelectedTab = tbDetails.TabPages(3)
            End If

        Catch ex As Exception

        End Try

    End Sub
    Private Function fp_FillGridList(ByVal sQuery As String, ByVal sGroupCode As String, ByVal sSort As String) As Boolean
        fp_FillGridList = True
        Dim dt As New DataTable
        dt = objcomm.FillDataTable(sQuery)
        If dt.Rows.Count > 0 Then
            dgFinalReport.DataSource = dt
        Else
            dgFinalReport.DataSource = dt
            'MsgBox("No Matched Record Found", MsgBoxStyle.Information)
            ' MsgBox(getValueByKey("RPT002"), MsgBoxStyle.Information, "RPT002 - " & getValueByKey("CLAE04"))
            fp_FillGridList = False
            Exit Function
        End If

        'Column names assign for diff lang
        For Each Row As DataRow In dtRptTotalDtl.Rows
            dgFinalReport.Cols(Row("ColumnNames").ToString()).Caption = Row("Description").ToString()
        Next


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


        dgFinalReport.Rows.Move(0, 3)
        'dgFinalReport.Rows(0)(0) = curCompany.PlantCode.ToString.Trim + "- " + curCompany.PlantDesc.ToString.Trim
        dgFinalReport.Rows(1)(0) = _reportName
        'dgFinalReport.Rows(2)(0) = "For: " + dtpFromDate.Text + " - " + dtpToDate.Text
        dgFinalReport.Rows(2)(0) = getValueByKey("RPT011") & ": " + dtpFromDate.Text + " - " + dtpToDate.Text



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

        'more setup
        dgFinalReport.SubtotalPosition = SubtotalPositionEnum.BelowData
        dgFinalReport.AllowDragging = AllowDraggingEnum.None
        dgFinalReport.AllowEditing = False
        Dim c As Integer
        If dgFinalReport.Rows.Count > 1 Then
            For c = 0 To dgFinalReport.Cols.Count - 1
                If dgFinalReport.DataSource IsNot Nothing And dgFinalReport.Cols(c).DataType.Name = "Decimal" Then
                    dgFinalReport.Cols(c).Format = "#,###.00"
                End If
            Next
        End If
        Me.Text = _reportName
    End Function
    Private Sub cmdPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrevious.Click
        tbDetails.SelectedIndex = 0
        If DateReq = True Then
            pnlDateFilter.Enabled = True
        End If
    End Sub
    Private Sub UpdateTotals()
        Dim i, j, k As Integer
        dgFinalReport.Subtotal(AggregateEnum.Clear)
        If _chkGrand.Checked Then
            For Each k In groupTotalColList
                'dgFinalReport.Subtotal(AggregateEnum.Sum, 0, -1, k, "Grand Total")
                dgFinalReport.Subtotal(AggregateEnum.Sum, 0, -1, k, getValueByKey("RPT007"))
                For Each dr As DataRow In dtRptTotalDtl.Select("ColumnNames='" & dtRptTotalDtl.Rows(k)("ColumnNames").ToString() & "' AND AVGON=1", "", DataViewRowState.CurrentRows)
                    'dgFinalReport.Subtotal(AggregateEnum.Average, 0, -1, k, "Grand Total")
                    dgFinalReport.Subtotal(AggregateEnum.Average, 0, -1, k, getValueByKey("RPT007"))
                    dgFinalReport.Cols(k).Format = "0.00"
                Next

            Next

            dgFinalReport.Tree.Column = 0
        End If
        j = 1
        For Each i In chkGroupingList.CheckedIndices
            For Each k In groupTotalColList
                'dgFinalReport.Subtotal(AggregateEnum.Sum, j, i, k, "Total for {0}")
                dgFinalReport.Subtotal(AggregateEnum.Sum, j, i, k, getValueByKey("RPT008"))
                For Each dr As DataRow In dtRptTotalDtl.Select("ColumnNames='" & dtRptTotalDtl.Rows(k)("ColumnNames").ToString() & "' AND AVGON=1", "", DataViewRowState.CurrentRows)
                    'dgFinalReport.Subtotal(AggregateEnum.Average, j, i, k, "Total for {0}")
                    dgFinalReport.Subtotal(AggregateEnum.Average, j, i, k, getValueByKey("RPT008"))
                    dgFinalReport.Cols(k).Format = "0.00"
                Next

            Next
            dgFinalReport.Tree.Column = j
            j = j + 1
        Next
        If ColaspedLavel > 0 Then
            dgFinalReport.Tree.Show(ColaspedLavel)
        End If
        dgFinalReport.Redraw = True
        dgFinalReport.AutoSizeCols()

    End Sub
    Private Sub _chk_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _chkAbove.CheckedChanged, _chkGrand.CheckedChanged
        If _chkAbove.Checked = True Then
            dgFinalReport.SubtotalPosition = SubtotalPositionEnum.AboveData
        Else
            dgFinalReport.SubtotalPosition = SubtotalPositionEnum.BelowData
        End If
        UpdateTotals()
    End Sub
    Private Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext.Click

        Dim StrSql1, strSql As String
        Dim strOrderBy As String = vbNullString
        Dim strCondition As String = vbNullString
        Dim sHeading As String
        Dim IntCols As Integer
        Dim SaveQuery As String
        Dim selectedColCount As Integer = 0
        Dim strFinalCondition As String = ""
        Try
            strSql = ""
            IntCols = 0
            sHeading = ""
            chkGroupingList.Items.Clear()
            chkGroupingList.BackColor = Me.BackColor
            dgFinalReport.AllowSorting = AllowSortingEnum.SingleColumn
            Dim i As Int16
            groupTotalColList.Clear()
            For i = 1 To dgSelection.Rows.Count - 1
                If CDbl(dgSelection.Item(i, 0)) = True Then
                    sHeading = sHeading & dgSelection.Item(i, 1) & "|"
                    strSql = strSql & dgSelection.Item(i, 1) & ","
                    If dtRptTotalDtl.Rows.Count > 0 Then
                        For Each dr As DataRow In dtRptTotalDtl.Select("GroupOn=1 AND ColumnNames='" & dgSelection.Item(i, 1).ToString().ToUpper() & "'", "", DataViewRowState.CurrentRows)
                            If dr("GroupOn") = True Then
                                chkGroupingList.Items.Add(dgSelection.Item(i, 1), True)
                            Else
                                chkGroupingList.Items.Add(dgSelection.Item(i, 1), False)
                            End If
                        Next
                    End If

                    If CDbl(dgSelection.Item(i, 3)) = True Then
                        groupTotalColList.Add(IntCols)
                    End If
                    If CDbl(dgSelection.Item(i, 2)) = True Then
                        strOrderBy = strOrderBy & dgSelection.Item(i, 1) & ","
                    End If
                    IntCols += 1
                End If
            Next i
            If Trim(strSql) = "" Then
                'MsgBox(" No fields Selected ", vbInformation)
                MsgBox(getValueByKey("RPT005"), vbInformation, "RPT005 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If



            If strOrderBy <> vbNullString Then strOrderBy = Mid(strOrderBy, 1, Len(strOrderBy) - 1)
            strSql = Mid(strSql, 1, Len(strSql) - 1)
            StrSql1 = "Select " & strSql & " from " & viewName & " where 1=1 "
            SaveQuery = StrSql1
            If DateReq = True And viewName.Substring(0, 2).ToUpper() <> "FN" Then
                StrSql1 = StrSql1 & "AND CAST(CONVERT(VARCHAR(10),INVDATETIME,101) AS DATETIME) >='" & Format(dtpFromDate.Value, "dd-MMM-yyyy") & "' AND CAST(CONVERT(VARCHAR(10),INVDATETIME,101) AS DATETIME) <= '" & Format(dtpToDate.Value, "dd-MMM-yyyy") & "'"
            ElseIf viewName.Substring(0, 2).ToUpper() = "FN" And DateReq = True Then
                ''-- clsAdmin.LangCode Added By Mahesh Nagar :-
                StrSql1 = "Select " & strSql & " from " & viewName & " ('" & Format(dtpFromDate.Value, "dd-MMM-yyyy") & "','" & Format(dtpToDate.Value, "dd-MMM-yyyy") & "','" & clsAdmin.LangCode & "') Where 1=1 "
            ElseIf viewName.Substring(0, 2).ToUpper() = "FN" And DateReq = False Then
                StrSql1 = "Select " & strSql & " from " & viewName & "() Where 1=1 "
            End If
            strFinalCondition = ""
            For i = 1 To SSSalesAnalysis.TabPages.Count
                'If findCondField(CType(SSSalesAnalysis.TabPages(i - 1).Controls(0), C1.Win.C1FlexGrid.C1FlexGrid).Tag) = True Then
                strCondition = getlist(i - 1)
                'End If
                If Trim(strCondition) <> "" Then

                    StrSql1 = StrSql1 & strCondition
                    If viewName.Substring(0, 2) = "SP" Then
                        strFinalCondition = strFinalCondition & "," & strCondition
                    End If
                Else
                    If viewName.Substring(0, 2) = "SP" And i <> 1 Then
                        strFinalCondition = strFinalCondition & "," & "'" & " " & "'"
                    End If
                End If
            Next
            If viewName.Substring(0, 2) = "SP" Then
                strCondition = strFinalCondition.Remove(0, 2)
            End If

            If strOrderBy <> vbNullString Then StrSql1 = StrSql1 & " ORDER BY " & strOrderBy

            If viewName.Substring(0, 2) = "SP" Then
                ExecuteQueryForSP(strCondition, strOrderBy, StrSql1)
                Exit Sub
            End If

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
                tbDetails.SelectedTab = tbDetails.TabPages(2)
            End If
            'UpdateTotals()
            tbDetails.SelectedIndex = 2
        Catch ex As Exception
            'MsgBox("Set Configuration is wrong.Please check it")
            MsgBox(getValueByKey("RPT003"), , "RPT003 - " & getValueByKey("CLAE05"))
        End Try

    End Sub
    Private Function findCondField(ByVal strField As String) As Boolean
        Try
            Dim i As Integer
            For i = 1 To dgSelection.Rows.Count - 1
                If dgSelection.Item(i, 1).ToString.ToUpper() = strField.ToUpper() Then
                    findCondField = True
                    Exit Function
                End If
            Next i
            findCondField = False
        Catch ex As Exception

        End Try
    End Function
    Private Sub chkGroupingList_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles chkGroupingList.ItemCheck
        Try
            Dim i, j, k As Integer
            dgFinalReport.Subtotal(AggregateEnum.Clear)
            If _chkAbove.Checked = True Then
                dgFinalReport.SubtotalPosition = SubtotalPositionEnum.AboveData
            Else
                dgFinalReport.SubtotalPosition = SubtotalPositionEnum.BelowData
            End If
            If _chkGrand.Checked Then
                For Each k In groupTotalColList
                    'dgFinalReport.Subtotal(AggregateEnum.Sum, 0, -1, k, "Grand Total")
                    dgFinalReport.Subtotal(AggregateEnum.Sum, 0, -1, k, getValueByKey("RPT007"))
                Next
                dgFinalReport.Tree.Column = 2
            End If
            j = 1
            For Each i In chkGroupingList.CheckedIndices
                If e.Index <> i Then
                    For Each k In groupTotalColList
                        'dgFinalReport.Subtotal(AggregateEnum.Sum, j, i, k, "Total for {0}")
                        dgFinalReport.Subtotal(AggregateEnum.Sum, j, i, k, getValueByKey("RPT008"))
                    Next
                    dgFinalReport.Tree.Column = i
                    j += 1
                End If
            Next
            If e.NewValue = CheckState.Checked Then
                For Each k In groupTotalColList
                    'dgFinalReport.Subtotal(AggregateEnum.Sum, j, e.Index, k, "Total for {0}")
                    dgFinalReport.Subtotal(AggregateEnum.Sum, j, e.Index, k, getValueByKey("RPT008"))
                Next
                dgFinalReport.Tree.Column = e.Index
            End If

        Catch ex As Exception

        End Try

    End Sub
    Private Function getlist(ByVal intGrdarr As Integer) As String
        Try
            Dim intSel As Integer
            Dim StrStr As String = vbNullString
            Dim grd As New C1.Win.C1FlexGrid.C1FlexGrid
            'If ViewName.Substring(0, 2) = "SP" Then
            '    StrStr = "''"
            'End If
            If intGrdarr > 0 Then
                grd = CType(SSSalesAnalysis.TabPages(intGrdarr).Controls(0), C1.Win.C1FlexGrid.C1FlexGrid)

                For intSel = 1 To grd.Rows.Count - 1
                    If CDbl(grd.Item(intSel, 1)) = True Then
                        If ViewName.Substring(0, 2) <> "SP" Then
                            StrStr = StrStr & "'" & grd.Item(intSel, 2) & "'" & ","
                        Else
                            StrStr = StrStr & "''" & Trim(grd.Item(intSel, 2)) & "''" & ","
                        End If
                    End If
                Next intSel
            Else
                For intSel = 1 To grdSales.Rows.Count - 1
                    If CDbl(grdSales.Item(intSel, 0)) = True Then
                        If ViewName.Substring(0, 2) <> "SP" Then
                            StrStr = StrStr & "'" & Trim(grdSales.Item(intSel, 1)) & "'" & ","
                        Else
                            StrStr = StrStr & "''" & Trim(grdSales.Item(intSel, 1)) & "''" & ","
                        End If
                    End If
                Next intSel
            End If
            If Trim(StrStr) = "" Then
                getlist = vbNullString
                Exit Function
            End If
            'If ViewName.Substring(0, 2) = "SP" Then
            '    StrStr = StrStr & " ''"
            '    'StrStr = Mid(StrStr, 1, Len(StrStr) - 1)
            'Else
            '    StrStr = Mid(StrStr, 1, Len(StrStr) - 1)
            'End If
            StrStr = Mid(StrStr, 1, Len(StrStr) - 1)
            If ViewName.Substring(0, 2) = "SP" Then
                StrStr = "'" & StrStr
                StrStr = StrStr & "'"
            End If
            If ViewName.Substring(0, 2) <> "SP" Then
                'StrStr = "and " & IIf(intGrdarr > 0, grd.Tag, grdSales.Tag) & " in (" & StrStr & ")"
                StrStr = getValueByKey("RPT014") & " " & IIf(intGrdarr > 0, grd.Tag, grdSales.Tag) & " in (" & StrStr & ")"
            End If
            getlist = StrStr
        Catch ex As Exception

        End Try
    End Function
    Private Sub cmdPreviousFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPreviousFilter.Click
        Try
            If SSSalesAnalysis.SelectedIndex = 0 Then Exit Sub
            SSSalesAnalysis.SelectedIndex = SSSalesAnalysis.SelectedIndex - 1
        Catch ex As Exception
        End Try
    End Sub
    Private Sub CmdNextFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdNextFilter.Click
        Try
            If SSSalesAnalysis.SelectedIndex = (SSSalesAnalysis.TabCount - 1) Then Exit Sub
            SSSalesAnalysis.SelectedIndex = SSSalesAnalysis.SelectedIndex + 1
        Catch ex As Exception
        End Try
    End Sub
    Private Function Themechange()
        'Me.Size = New Size(430, 238)
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)

        Label1.ForeColor = Color.White
        Label1.Font = New Font("Neo Sans", 9, FontStyle.Bold)


        C1Label1.ForeColor = Color.White
        C1Label1.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        C1Label1.BackColor = Color.Transparent
        C1Label1.BorderStyle = BorderStyle.None

        C1Label2.ForeColor = Color.White
        C1Label2.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        C1Label2.BackColor = Color.Transparent
        C1Label2.BorderStyle = BorderStyle.None

        'rbBirthList.ForeColor = Color.White
        'rbBirthList.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        ''lblBillDate.ForeColor = Color.Black
        ''lblBillDate.AutoSize = False
        ''lblBillDate.Size = New Size(200, 18)
        ''lblBillDate.BorderStyle = BorderStyle.None
        ''lblBillDate.SendToBack()
        ''lblBillDate.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        ''lblBillDate.TextAlign = ContentAlignment.MiddleLeft
        ''lblBillDate.BackColor = Color.FromArgb(212, 212, 212)

        'lblCashmemo.ForeColor = Color.Black
        ''lblCashmemo.AutoSize = False
        'lblCashmemo.BackColor = Color.FromArgb(212, 212, 212)
        'lblCashmemo.BorderStyle = BorderStyle.None

        grdSales.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        grdSales.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        grdSales.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        grdSales.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        grdSales.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSales.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSales.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSales.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdSales.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)

        dgFinalReport.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        dgFinalReport.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgFinalReport.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        dgFinalReport.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        dgFinalReport.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgFinalReport.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgFinalReport.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgFinalReport.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgFinalReport.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)

        dgSelection.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        dgSelection.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgSelection.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        dgSelection.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        dgSelection.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgSelection.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgSelection.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgSelection.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgSelection.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)

        CmdNextFilter.Location = New Point(120, 48)
        CmdNextFilter.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CmdNextFilter.BackColor = Color.Transparent
        CmdNextFilter.BackColor = Color.FromArgb(0, 107, 163)
        CmdNextFilter.ForeColor = Color.FromArgb(255, 255, 255)
        CmdNextFilter.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        CmdNextFilter.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CmdNextFilter.FlatStyle = FlatStyle.Flat
        CmdNextFilter.FlatAppearance.BorderSize = 0
        CmdNextFilter.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        CmdNextFilter.Size = New Size(100, 29)
        CmdNextFilter.TextAlign = ContentAlignment.MiddleLeft

        cmdPreviousFilter.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdPreviousFilter.BackColor = Color.Transparent
        cmdPreviousFilter.BackColor = Color.FromArgb(0, 107, 163)
        cmdPreviousFilter.ForeColor = Color.FromArgb(255, 255, 255)
        cmdPreviousFilter.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        cmdPreviousFilter.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdPreviousFilter.FlatStyle = FlatStyle.Flat
        cmdPreviousFilter.FlatAppearance.BorderSize = 0
        cmdPreviousFilter.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdPreviousFilter.Size = New Size(110, 29)
        cmdPreviousFilter.TextAlign = ContentAlignment.MiddleLeft

        'cmd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'cmdPreviousFilter.BackColor = Color.Transparent
        'cmdPreviousFilter.BackColor = Color.FromArgb(0, 107, 163)
        'cmdPreviousFilter.ForeColor = Color.FromArgb(255, 255, 255)
        'cmdPreviousFilter.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        'cmdPreviousFilter.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        'cmdPreviousFilter.FlatStyle = FlatStyle.Flat
        'cmdPreviousFilter.FlatAppearance.BorderSize = 0
        'cmdPreviousFilter.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        'cmdPreviousFilter.Size = New Size(110, 29)
        'cmdPreviousFilter.TextAlign = ContentAlignment.MiddleLeft

        cmdNextConfig.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdNextConfig.BackColor = Color.Transparent
        cmdNextConfig.BackColor = Color.FromArgb(0, 107, 163)
        cmdNextConfig.ForeColor = Color.FromArgb(255, 255, 255)
        cmdNextConfig.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdNextConfig.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdNextConfig.FlatStyle = FlatStyle.Flat
        cmdNextConfig.FlatAppearance.BorderSize = 0
        cmdNextConfig.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdNextConfig.Size = New Size(63, 29)


        cmdNext.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdNext.BackColor = Color.Transparent
        cmdNext.BackColor = Color.FromArgb(0, 107, 163)
        cmdNext.ForeColor = Color.FromArgb(255, 255, 255)
        cmdNext.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdNext.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdNext.FlatStyle = FlatStyle.Flat
        cmdNext.FlatAppearance.BorderSize = 0
        cmdNext.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        '  cmdNext.Size = New Size(63, 29)


        cmdExport.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdExport.BackColor = Color.Transparent
        cmdExport.BackColor = Color.FromArgb(0, 107, 163)
        cmdExport.ForeColor = Color.FromArgb(255, 255, 255)
        cmdExport.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdExport.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdExport.FlatStyle = FlatStyle.Flat
        cmdExport.FlatAppearance.BorderSize = 0
        cmdExport.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        _chkGrand.ForeColor = Color.White


        cmdPrevious.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdPrevious.BackColor = Color.Transparent
        cmdPrevious.BackColor = Color.FromArgb(0, 107, 163)
        cmdPrevious.ForeColor = Color.FromArgb(255, 255, 255)
        cmdPrevious.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        cmdPrevious.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdPrevious.FlatStyle = FlatStyle.Flat
        cmdPrevious.FlatAppearance.BorderSize = 0
        cmdPrevious.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdPrevious.TextAlign = ContentAlignment.MiddleLeft

    End Function

End Class
