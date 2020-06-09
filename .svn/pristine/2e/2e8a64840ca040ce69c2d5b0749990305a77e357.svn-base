Imports SpectrumBL
Imports System.Text


Public Class frmPrepStationTemplate

    Dim cls As New clsCommon
    Dim objItem As New clsIteamSearch
    Dim sitename As String
    Dim allnodecode As String
    Dim tbl As New DataTable
    Dim dttblcopy As New DataTable
    Dim dt As DataTable
    Dim nodedt As DataTable
    Dim newdt As DataTable
    Dim dtgrid As New DataTable
    Dim dmdt As DataSet

    Private Sub frmPrepStationTemplate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'Me.Location = New Point(0, 0)
            'Me.Size = Screen.PrimaryScreen.WorkingArea.Size
            'Me.HorizontalScroll.Visible = False
            'Me.VerticalScroll.Visible = False

            Me.AutoScroll = False
            fillsites()
            cboSite.SelectedIndex = 0
            txtHierarchy.ReadOnly = True
            fillTemplate()
            '' cboTemplate.Items.Insert(0, "Create New")

            _loadTempalte()
            dt = objItem.GetArticleTree()
            Panel1.Visible = False
            btnSave.Visible = False
            ' btnCancel.Visible = False
            If Not tbl.Columns.Contains("NodeCode") Then
                tbl.Columns.Add("NodeCode", System.Type.GetType("System.String"))
                tbl.Columns.Add("NodeName", System.Type.GetType("System.String"))
                tbl.Columns.Add("ParentName", System.Type.GetType("System.String"))
                tbl.Columns.Add("Status", System.Type.GetType("System.Boolean"))
            End If
            Me.Size = New System.Drawing.Size((My.Computer.Screen.WorkingArea.Width - 10), (My.Computer.Screen.WorkingArea.Height))

            'Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
            'Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height
            'If screenWidth <= 1024 AndAlso screenHeight = 768 Then
            '    Me.Height = screenHeight
            '    Me.Width = screenWidth
            'End If

            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                ThemeChange()
            End If
            newGridsetting()

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub fillsites()
        Try
            dt = cls.GetSitedetail()
            cboSite.ValueMember = "Id"
            cboSite.DisplayMember = "SiteShortName"
            cboSite.DataSource = dt
            sitename = dt.Rows(0)("SiteShortName")
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub fillTemplate()
        Try
            newdt = cls.GetTemplatedetail()
            cboTemplate.ValueMember = "Id"
            cboTemplate.DisplayMember = "mstPrepStationTemplateID"
            cboTemplate.DataSource = newdt
            cboTemplate.SelectedIndex = 0
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub fillnewTemplate(row)
        Try
            newdt = cls.GetNewTemplatedetail(row)
            cboTemplate.ValueMember = "Id"
            cboTemplate.DisplayMember = "mstPrepStationTemplateID"
            cboTemplate.DataSource = newdt
            cboTemplate.SelectedIndex = 0
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub _loadTempalte()
        Try
            Dim dtgrid As DataTable = cls.showTempate()
            gridsetting()
            Dim k As Int32
            Dim foundrow As DataRow

            gridsetting()
            Dim jK As Integer = 1
            For Each dr In dtgrid.Rows
                CtrlGrid1.Rows.Add()
                CtrlGrid1.Rows(jK)(2) = jK
                CtrlGrid1.Rows(jK)(3) = sitename
                CtrlGrid1.Rows(jK)(4) = dr("mstPrepStationTemplateID")
                CtrlGrid1.Rows(jK)(5) = dr("Status")
                CtrlGrid1.Rows(jK)(6) = dr("createdOn")
                jK = jK + 1
            Next

            Dim xx As Int32
            If CtrlGrid1.Rows.Count > 0 Then
                For xx = CtrlGrid1.Rows.Count - 1 To 0 Step -1
                    If CtrlGrid1.Rows(xx)(3) = "" Then
                        CtrlGrid1.Rows.Remove(xx)
                    End If
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub gridsetting()
        Try
            CtrlGrid1.Cols(1).Width = 20
            CtrlGrid1.Cols(1).ComboList = " "
            CtrlGrid1.Cols(1).ComboList = "..."
            CtrlGrid1.Cols(1).Visible = False
            CtrlGrid1.Cols(1).AllowEditing = True
            CtrlGrid1.Cols(1).Caption = ""
            CtrlGrid1.Cols(2).Visible = True
            CtrlGrid1.Cols(2).Caption = "Prep-StationID"
            CtrlGrid1.Cols(2).Width = 150
            CtrlGrid1.Cols(2).AllowEditing = False
            CtrlGrid1.Cols(3).Visible = True
            CtrlGrid1.Cols(3).Caption = "Site Name"
            CtrlGrid1.Cols(3).Width = 250
            CtrlGrid1.Cols(3).AllowEditing = False
            CtrlGrid1.Cols(4).Visible = True
            CtrlGrid1.Cols(4).Caption = "Template"
            CtrlGrid1.Cols(4).Width = 250
            CtrlGrid1.Cols(4).AllowEditing = False
            CtrlGrid1.Cols(5).Visible = True
            CtrlGrid1.Cols(5).Caption = "Status"
            CtrlGrid1.Cols(5).Width = 250
            CtrlGrid1.Cols(5).AllowEditing = False
            CtrlGrid1.Cols(6).Visible = True
            CtrlGrid1.Cols(6).Caption = "Created On"
            CtrlGrid1.Cols(6).Width = 290
            CtrlGrid1.Cols(6).AllowEditing = False
            CtrlGrid1.Cols(7).Width = 20
            CtrlGrid1.Cols(7).ComboList = " "
            CtrlGrid1.Cols(7).ComboList = "..."
            CtrlGrid1.Cols(7).Visible = True
            CtrlGrid1.Cols(7).AllowEditing = True
            CtrlGrid1.Cols(7).Caption = "Edit"

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub newGridsetting()
        Try
            CtrlArtGrid.Cols(0).Visible = False
            CtrlArtGrid.Cols(1).Visible = True
            CtrlArtGrid.Cols(1).Caption = "ArticleCode"
            CtrlArtGrid.Cols(1).Width = 200
            CtrlArtGrid.Cols(1).AllowEditing = False
            CtrlArtGrid.Cols(1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterTop
            CtrlArtGrid.Cols(1).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterTop
            CtrlArtGrid.Cols(2).Visible = True
            CtrlArtGrid.Cols(2).Caption = "Article Name"
            CtrlArtGrid.Cols(2).Width = 300
            CtrlArtGrid.Cols(2).AllowEditing = False
            CtrlArtGrid.Cols(2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterTop
            CtrlArtGrid.Cols(2).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterTop
            CtrlArtGrid.Cols(3).Visible = True
            CtrlArtGrid.Cols(3).Caption = "Node"
            CtrlArtGrid.Cols(3).Width = 350
            CtrlArtGrid.Cols(3).AllowEditing = False
            CtrlArtGrid.Cols(3).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterTop
            CtrlArtGrid.Cols(3).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterTop
            CtrlArtGrid.Cols(4).Visible = True
            CtrlArtGrid.Cols(4).Caption = "Node Code"
            CtrlArtGrid.Cols(4).Width = 200
            CtrlArtGrid.Cols(4).AllowEditing = False
            CtrlArtGrid.Cols(4).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterTop
            CtrlArtGrid.Cols(4).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterTop
            CtrlArtGrid.Cols(5).Visible = True
            CtrlArtGrid.Cols(5).Caption = "Exclude"
            CtrlArtGrid.Cols(5).Width = 150
            CtrlArtGrid.Cols(5).AllowEditing = True
            CtrlArtGrid.Cols(5).DataType = Type.GetType("System.Boolean")
            CtrlArtGrid.Cols(5).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterTop
            CtrlArtGrid.Cols(5).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterTop
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtHierarchy_Click(sender As Object, e As EventArgs) Handles txtHierarchy.Click
        Try
            Dim ObjHierPopup As New frmHierarchyPopUp
            ObjHierPopup.CheckedHierarachy = True
            ObjHierPopup.ShowDialog()

            allnodecode = ObjHierPopup.SelectedNodeCode

            If Not String.IsNullOrEmpty(allnodecode) Then
                'Dim name() As String = allnodecode.Split(Microsoft.VisualBasic.ChrW(44))
                'Dim i As Integer
                'Dim j As Integer

                'Do While (i < name.Length)
                '    name(i) = name(i).TrimEnd("'")
                '    name(i) = name(i).TrimStart("'")
                '    For j = 0 To dt.Rows.Count - 1
                '        If name(i) = dt.Rows(j)("NodeCode") Then
                '            tbl.Rows.Add(name(i), dt.Rows(j)("NODENAME"), dt.Rows(j)("PARENTNODECODE"), True)
                '            Exit For
                '        End If
                '    Next
                '    i = (i + 1)
                'Loop
                'If tbl.Rows.Count > 0 Then
                '    tbl = tbl.DefaultView.ToTable(True, "NodeCode", "NodeName", "ParentName", "Status")
                'End If
                'If Not dttblcopy.Columns.Contains("NodeCode") Then
                '    dttblcopy = tbl.Clone
                'End If
                'Dim m As Integer

                'Dim name1() As String = allnodecode.Split(Microsoft.VisualBasic.ChrW(44))
                'Dim x As Integer
                'Dim y As Integer
                'Do While (x < name1.Length)
                '    For m = 0 To tbl.Rows.Count - 1
                '        name1(x) = name1(x).TrimEnd("'")
                '        name1(x) = name1(x).TrimStart("'")
                '        If tbl.Rows(m)("NodeCode") = name1(x) Then
                '            dttblcopy.Rows.Add(tbl.Rows(m)("NodeCode"), tbl.Rows(m)("NodeName"), tbl.Rows(m)("ParentName"), True)
                '        End If
                '    Next
                '    x = (x + 1)
                'Loop
                If Not dttblcopy.Columns.Contains("NodeCode") Then
                    dttblcopy = tbl.Clone
                End If

                Dim name() As String = allnodecode.Split(Microsoft.VisualBasic.ChrW(44))
                Dim i As Integer
                Dim j As Integer

                Do While (i < name.Length)
                    name(i) = name(i).TrimEnd("'")
                    name(i) = name(i).TrimStart("'")
                    Dim heirarchyname = name(i).ToString()
                    Dim dtt = dt.Select("NodeCode='" + heirarchyname + "'").CopyToDataTable
                    For j = 0 To dtt.Rows.Count - 1
                        tbl.Rows.Add(name(i), dtt.Rows(j)("NODENAME"), dtt.Rows(j)("PARENTNODECODE"), True)
                        dttblcopy.Rows.Add(name(i), dtt.Rows(j)("NODENAME"), dtt.Rows(j)("PARENTNODECODE"), True)
                        Exit For
                    Next
                    i = (i + 1)
                Loop
                If tbl.Rows.Count > 0 Then
                    tbl = tbl.DefaultView.ToTable(True, "NodeCode", "NodeName", "ParentName", "Status")
                    dttblcopy = dttblcopy.DefaultView.ToTable(True, "NodeCode", "NodeName", "ParentName", "Status")
                End If
                FlowLayoutPanel1.Controls.Clear()
                btnCheck.Visible = True
                btnUncheck.Visible = True
                showGridData()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub showGridData(Optional ByVal value As String = Nothing)
        Try
            'Dim k As Integer
            'Dim x As Integer = 0
            'Dim y As Integer = 0
            ''If dtgrid.Rows.Count > 0 Then
            ''    dtgrid.Rows.Clear()
            ''End If
            'Dim dtpreptemp As DataTable
            'For k = 0 To tbl.Rows.Count - 1
            '    Dim exists As Boolean = dtgrid.AsEnumerable().Where(Function(c) c.Field(Of String)("LastNodeCode").Equals(tbl.Rows(k)("NodeCode"))).Count() > 0
            '    'If Not exists Then
            '    x += 156
            '    Dim btn As New CtrlHierarchyButton
            '    btn.btnDeleteHierarchy.Tag = tbl.Rows(k)("NodeCode")
            '    btn.Name = "DynamicButton"
            '    btn.Size = New Size(156, 26)
            '    btn.Location = New Point(x + y, 10)
            '    AddHandler btn.btnDeleteHierarchy.Click, AddressOf btnDeleteHierarchy_Click
            '    btn.BackColor = Color.SteelBlue
            '    btn.lblHierarchyName.Text = tbl.Rows(k)("NODENAME")
            '    FlowLayoutPanel1.Controls.Add(btn)
            '    y += 3
            '    If Not exists Then
            '        If cls.CheckTemplateArticleExist(tbl.Rows(k)("NodeCode")) Then
            '            dtpreptemp = cls.GetNewpreparedatatemplate(tbl.Rows(k)("NodeCode"))
            '        Else
            '            dtpreptemp = cls.preparedatatemplate(btn.btnDeleteHierarchy.Tag)
            '        End If


            '        CtrlArtGrid.Visible = True
            '        Dim jj As Int32
            '        If CtrlArtGrid.Rows.Count > 0 Then
            '            For jj = CtrlArtGrid.Rows.Count - 1 To 0 Step -1
            '                If CtrlArtGrid.Rows(jj)(1) = "" Then
            '                    CtrlArtGrid.Rows.Remove(jj)
            '                End If
            '            Next
            '        End If

            '        If Not dtgrid.Columns.Contains("ArticleCode") Then
            '            dtgrid = dtpreptemp.Clone
            '        End If
            '        For i As Integer = 0 To dtpreptemp.Rows.Count - 1
            '            dtgrid.ImportRow(dtpreptemp.Rows(i))
            '        Next

            '        Dim jK As Integer = 1
            '        For Each dr In dtgrid.Rows
            '            CtrlArtGrid.Rows.Add(1)
            '            CtrlArtGrid.Rows(jK)(1) = dr("ArticleCode")
            '            CtrlArtGrid.Rows(jK)(2) = dr("ArticleName")
            '            CtrlArtGrid.Rows(jK)(3) = dr("ParentArt")
            '            CtrlArtGrid.Rows(jK)(4) = dr("LastNodeCode")
            '            If Not dmdt Is Nothing Then
            '                Dim Articleexists As Boolean = dmdt.Tables("MstPrepStationTemplateMstArticleMap").AsEnumerable().Where(Function(c) c.Field(Of String)("articleCode").Equals(dr("ArticleCode"))).Count() > 0
            '                If Articleexists Then
            '                    Dim Articlestatus As Boolean
            '                    Dim row As DataRow = dmdt.Tables("MstPrepStationTemplateMstArticleMap").Select("articleCode = '" & dr("ArticleCode") & "'").FirstOrDefault()
            '                    If Not row Is Nothing Then
            '                        Dim a As Boolean = IIf(row.Item("Status") = True, False, True)
            '                        Articlestatus = a
            '                    End If
            '                    CtrlArtGrid.Rows(jK)(5) = Articlestatus
            '                Else
            '                    CtrlArtGrid.Rows(jK)(5) = dr("Status")
            '                End If
            '            Else
            '                CtrlArtGrid.Rows(jK)(5) = dr("Status")
            '            End If
            '            jK = jK + 1
            '        Next

            '        Dim xx As Int32
            '        If CtrlArtGrid.Rows.Count > 0 Then
            '            For xx = CtrlArtGrid.Rows.Count - 1 To 0 Step -1
            '                If CtrlArtGrid.Rows(xx)(1) = "" Then
            '                    CtrlArtGrid.Rows.Remove(xx)
            '                End If
            '            Next
            '        End If

            '    End If
            'Next

            Dim k As Integer
            Dim x As Integer = 0
            Dim y As Integer = 0
            Dim dtt As New DataTable
            Dim commaSeperatedValues As String = ""
            If Not dttblcopy.Columns.Contains("NodeCode") Then
                dttblcopy = tbl.Clone
            End If
            '    Dim exists As Boolean = dtgrid.AsEnumerable().Where(Function(c) c.Field(Of String)("LastNodeCode").Equals(tbl.Rows(k)("NodeCode"))).Count() > 0
            If Not value Is Nothing Then
                dtt = cls.showTemplateArticleExist(value)
                For j = 0 To dtt.Rows.Count - 1
                    Dim Newdtt = dt.Select("NodeCode='" + dtt.Rows(j)("NodeCode") + "'").CopyToDataTable
                    For Each dr As DataRow In Newdtt.Rows
                        tbl.Rows.Add(dr("NodeCode"), dr("NODENAME"), dr("PARENTNODECODE"), True)
                        dttblcopy.Rows.Add(dr("NodeCode"), dr("NODENAME"), dr("PARENTNODECODE"), True)
                    Next
                Next
                If tbl.Rows.Count > 0 Then
                    tbl = tbl.DefaultView.ToTable(True, "NodeCode", "NodeName", "ParentName", "Status")
                    dttblcopy = dttblcopy.DefaultView.ToTable(True, "NodeCode", "NodeName", "ParentName", "Status")
                End If
                Dim SelectedValues = tbl.AsEnumerable().[Select](Function(s) s.Field(Of String)("NodeCode")).ToArray()
                commaSeperatedValues = String.Join(",", SelectedValues)
            Else
                Dim SelectedValues = tbl.AsEnumerable().[Select](Function(s) s.Field(Of String)("NodeCode")).ToArray()
                commaSeperatedValues = String.Join(",", SelectedValues)
            End If
            'If dtgrid.Rows.Count > 0 Then
            '    dtgrid.Rows.Clear()
            'End If
            Dim dtpreptemp As DataTable
            For k = 0 To tbl.Rows.Count - 1

                x += 156
                Dim btn As New CtrlHierarchyButton
                btn.btnDeleteHierarchy.Tag = tbl.Rows(k)("NodeCode")
                btn.Name = "DynamicButton"
                btn.Size = New Size(156, 26)
                btn.Location = New Point(x + y, 10)
                AddHandler btn.btnDeleteHierarchy.Click, AddressOf btnDeleteHierarchy_Click
                btn.BackColor = Color.SteelBlue
                btn.btnDeleteHierarchy.BackColor = Color.White
                btn.lblHierarchyName.Text = tbl.Rows(k)("NODENAME")
                FlowLayoutPanel1.Controls.Add(btn)
                y += 3
            Next

            If Not value Is Nothing Then
                dtpreptemp = cls.GetNewpreparedatatemplate(commaSeperatedValues, value)
            Else
                dtpreptemp = cls.preparedatatemplate(commaSeperatedValues)
            End If

            CtrlArtGrid.Visible = True
            Dim jj As Int32
            If CtrlArtGrid.Rows.Count > 1 Then
                CtrlArtGrid.Clear()
            End If

            If Not dtgrid.Columns.Contains("ArticleCode") Then
                dtgrid = dtpreptemp.Copy
            End If
            dtgrid = dtpreptemp.Copy

            CtrlArtGrid.DataSource = dtgrid
            newGridsetting()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub btnDeleteHierarchy_Click(sender As Object, e As EventArgs)
        Try
            Dim Node = sender.tag
            FlowLayoutPanel1.Controls.Clear()
            Dim xx As Int32
            'If CtrlArtGrid.Rows.Count > 0 Then
            '    For xx = CtrlArtGrid.Rows.Count - 1 To 1 Step -1
            '        If CtrlArtGrid.Rows(xx)(4) = Node Then
            '            CtrlArtGrid.Rows.Remove(xx)
            '        End If
            '    Next
            'End If
            If CtrlArtGrid.Rows.Count > 0 Then
                CtrlArtGrid.Clear()
            End If
            For Each Dr As DataRow In dtgrid.Rows
                If Dr("LastNodeCode") = Node Then
                    Dr.Delete()
                End If
            Next

            Dim ii As Int32
            For ii = dtgrid.Rows.Count - 1 To 0 Step -1
                If dtgrid.Rows(ii).RowState = DataRowState.Deleted Then
                    dtgrid.Rows.RemoveAt(ii)
                End If
            Next

            If Not dttblcopy.Columns.Contains("NodeCode") Then
                dttblcopy = tbl.Clone
            End If

            'If dttblcopy.Rows.Count > 0 Then
            '    dttblcopy = dttblcopy.DefaultView.ToTable(True, "NodeCode", "NodeName", "ParentName", "Status")
            'End If

            Dim m As Integer
            For m = 0 To tbl.Rows.Count - 1
                If tbl.Rows(m)("NodeCode") = Node Then
                    Dim exists As Boolean = dttblcopy.AsEnumerable().Where(Function(c) c.Field(Of String)("NodeCode").Equals(Node)).Count() > 0
                    If exists Then
                        Dim myRow() As Data.DataRow
                        myRow = dttblcopy.Select("NodeCode = '" & Node & "'")
                        myRow(0)("Status") = False
                        'dttblcopy.Rows(m)("Status") = False
                    Else
                        dttblcopy.Rows.Add(tbl.Rows(m)("NodeCode"), tbl.Rows(m)("NodeName"), tbl.Rows(m)("ParentName"), False)
                    End If
                End If
            Next



            'If dttblcopy.Rows.Count > 0 Then
            '    Dim j As Integer
            '    For j = 0 To dttblcopy.Rows.Count - 1
            '        If dttblcopy.Rows(j)("NodeCode") = Node Then
            '            dttblcopy.Rows(j)("status") = False
            '            Exit For
            '        End If
            '    Next
            'End If

            If tbl.Rows.Count > 0 Then
                Dim i As Integer
                For i = 0 To tbl.Rows.Count - 1
                    If tbl.Rows(i)("NodeCode") = Node Then
                        tbl.Rows.RemoveAt(i)
                        Exit For
                    End If
                Next
                showGridData()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        Try
            clear()
            Panel1.Visible = True
            btnSave.Visible = True
            btnCancel.Visible = True
            rbbtnActive.Checked = True
            CtrlArtGrid.Visible = False
            btnCheck.Visible = False
            btnUncheck.Visible = False
            txtName.ReadOnly = False
            txtTemplateName.ReadOnly = False
            If newdt.Rows.Count > 0 Then
                newdt.Rows.Clear()
            End If
            If tbl.Rows.Count > 0 Then
                tbl.Rows.Clear()
            End If
            fillTemplate()
            cboTemplate.Enabled = True
            txtName.Focus()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If Panel1.Visible Then
            If MessageBox.Show("Your will loose unsaved data, Do you wish to continue?", "Information", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If

    End Sub

    Private Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click
        Try
            Dim y As Integer
            If CtrlArtGrid.Rows.Count > 0 Then
                For y = 1 To CtrlArtGrid.Rows.Count - 1
                    CtrlArtGrid.Rows(y)(5) = True
                Next
            End If
            If dtgrid.Rows.Count > 0 Then
                For y = 0 To dtgrid.Rows.Count - 1
                    dtgrid.Rows(y)(4) = True
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnUncheck_Click(sender As Object, e As EventArgs) Handles btnUncheck.Click
        Try
            Dim y As Integer
            If CtrlArtGrid.Rows.Count > 0 Then
                For y = 1 To CtrlArtGrid.Rows.Count - 1
                    CtrlArtGrid.Rows(y)(5) = False
                Next
            End If
            If dtgrid.Rows.Count > 0 Then
                For y = 0 To dtgrid.Rows.Count - 1
                    dtgrid.Rows(y)(4) = False
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub ThemeChange()
        Me.BackColor = Color.FromArgb(134, 134, 134)

        'Label
        'Label5.BackColor = Color.FromArgb(212, 212, 212)
        'Label1.BackColor = Color.FromArgb(212, 212, 212)
        'Label2.BackColor = Color.FromArgb(212, 212, 212)
        'Label3.BackColor = Color.FromArgb(212, 212, 212)
        'Label4.BackColor = Color.FromArgb(212, 212, 212)
        'Label6.BackColor = Color.FromArgb(212, 212, 212)
        'Label8.BackColor = Color.FromArgb(212, 212, 212)
        Label1.ForeColor = Color.White
        Label2.ForeColor = Color.White
        Label3.ForeColor = Color.White
        Label4.ForeColor = Color.White
        Label5.ForeColor = Color.White
        Label6.ForeColor = Color.White
        Label8.ForeColor = Color.White
        rbbtnActive.ForeColor = Color.White
        RbbtnInactive.ForeColor = Color.White

        'create Button
        btnCreate.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnCreate.BackColor = Color.Transparent
        btnCreate.BackColor = Color.FromArgb(0, 107, 163)
        btnCreate.ForeColor = Color.FromArgb(255, 255, 255)
        btnCreate.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnCreate.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnCreate.FlatAppearance.BorderSize = 0
        btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat

        'add Button
        btnAddTemplate.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnAddTemplate.BackColor = Color.Transparent
        btnAddTemplate.BackColor = Color.FromArgb(0, 107, 163)
        btnAddTemplate.ForeColor = Color.FromArgb(255, 255, 255)
        btnAddTemplate.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnAddTemplate.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnAddTemplate.FlatAppearance.BorderSize = 0
        btnAddTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat

        'Save Button
        btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnSave.BackColor = Color.Transparent
        btnSave.BackColor = Color.FromArgb(0, 107, 163)
        btnSave.ForeColor = Color.FromArgb(255, 255, 255)
        btnSave.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat

        'cancel Button
        btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnCancel.BackColor = Color.Transparent
        btnCancel.BackColor = Color.FromArgb(0, 107, 163)
        btnCancel.ForeColor = Color.FromArgb(255, 255, 255)
        btnCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat


        'UnCheckAll Button
        btnUncheck.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnUncheck.BackColor = Color.Transparent
        btnUncheck.BackColor = Color.FromArgb(0, 107, 163)
        btnUncheck.ForeColor = Color.FromArgb(255, 255, 255)
        btnUncheck.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnUncheck.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnUncheck.FlatAppearance.BorderSize = 0
        btnUncheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat

        'CheckAll Button
        btnCheck.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnCheck.BackColor = Color.Transparent
        btnCheck.BackColor = Color.FromArgb(0, 107, 163)
        btnCheck.ForeColor = Color.FromArgb(255, 255, 255)
        btnCheck.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnCheck.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnCheck.FlatAppearance.BorderSize = 0
        btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat

        'ArticleGrid
        CtrlArtGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        CtrlArtGrid.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        CtrlArtGrid.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        CtrlArtGrid.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        CtrlArtGrid.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlArtGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlArtGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlArtGrid.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlArtGrid.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        CtrlArtGrid.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212)
        CtrlArtGrid.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
        CtrlArtGrid.BackColor = Color.White

        'TemplateGrid
        CtrlGrid1.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        CtrlGrid1.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        CtrlGrid1.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        CtrlGrid1.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        CtrlGrid1.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlGrid1.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlGrid1.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlGrid1.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlGrid1.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        CtrlGrid1.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212)
        CtrlGrid1.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
        CtrlGrid1.BackColor = Color.White

    End Sub
    Private Sub clear()
        txtName.Text = ""
        txtTemplateName.Text = ""
        txtTemplateShortDesc.Text = ""
        txtTempateDesc.Text = ""
        FlowLayoutPanel1.Controls.Clear()
        Panel1.Visible = False
        dttblcopy.Rows.Clear()
        If CtrlArtGrid.Rows.Count > 0 Then
            Dim xx As Int32
            If CtrlArtGrid.Rows.Count > 0 Then
                For xx = CtrlArtGrid.Rows.Count - 1 To 1 Step -1
                    If CtrlArtGrid.Rows(xx)(2) <> "" Then
                        CtrlArtGrid.Rows.Remove(xx)
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If String.IsNullOrWhiteSpace(txtName.Text.Trim()) Then
                ShowMessage("Name doesn't allow Blank spaces.", "Information")
                txtName.Clear()
                Exit Sub
            End If
            If String.IsNullOrWhiteSpace(txtTemplateName.Text.Trim()) Then
                ShowMessage("Template Name doesn't allow Blank spaces.", "Information")
                txtTemplateName.Clear()
                Exit Sub
            End If

            If String.IsNullOrWhiteSpace(txtTemplateShortDesc.Text.Trim()) Then
                ShowMessage("Template Short Name doesn't allow Blank spaces.", "Information")
                txtTemplateShortDesc.Clear()
                Exit Sub
            End If
            If String.IsNullOrWhiteSpace(txtTempateDesc.Text.Trim()) Then
                ShowMessage("Template Description doesn't allow Blank spaces.", "Information")
                txtTempateDesc.Clear()
                Exit Sub
            End If
            If txtName.Text = "" Then
                ShowMessage("Name Can't be left Blank.", "Information")
                Exit Sub
            End If

            If txtTemplateName.Text = "" Then
                ShowMessage("Template Name Can't be left Blank.", "Information")
                Exit Sub
            End If
            If txtTemplateShortDesc.Text = "" Then
                ShowMessage("Template Short Name Can't be Left Blank.", "Information")
                Exit Sub
            End If
            If txtTempateDesc.Text = "" Then
                ShowMessage("Template Description Can't be left Blank.", "Information")
                Exit Sub
            End If
            'If CtrlArtGrid.Rows.Count > 1 Then
            Dim name As String = txtName.Text.Trim
            Dim tempname As String = txtTemplateName.Text.Trim
            Dim tempsortname As String = txtTemplateShortDesc.Text.Trim
            Dim tempdesc As String = txtTempateDesc.Text.Trim
            Dim sitecode As String = clsAdmin.SiteCode
            Dim siteName As String = cls.GetSiteName(sitecode)
            Dim usercode As String = clsAdmin.UserName
            Dim tempstatus As Boolean = IIf(rbbtnActive.Checked = True, True, False)

            'If cls.IsTemplateisMappedWithStation(tempname) Then
            '    If tempstatus = False Then
            '        ShowMessage(tempname + " is already Mapped. It Can't be De-activated.", "Information")
            '        rbbtnActive.Checked = True
            '        Exit Sub
            '    End If
            'End If

            dttblcopy.Merge(tbl)
            If dttblcopy.Rows.Count > 0 Then
                dttblcopy = dttblcopy.DefaultView.ToTable(True, "NodeCode", "NodeName", "ParentName", "Status")
            End If

            Dim ii As Int32
            For ii = dtgrid.Rows.Count - 1 To 0 Step -1
                If dtgrid.Rows(ii).RowState = DataRowState.Deleted Then
                    dtgrid.Rows.RemoveAt(ii)
                End If
            Next

            If cls.updateMstArticleNodeMap1(name, tempname, tempsortname, tempdesc, sitecode, usercode, siteName, tempstatus, dttblcopy, dtgrid) Then
                ShowMessage("Data Saved Successfully.", "Information")
                clear()
            End If
            frmPrepStationTemplate_Load(sender, e)
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub CtrlArtGrid_CellChecked(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles CtrlArtGrid.CellChecked
        Try
            Dim i As Integer
            Dim row As String = CtrlArtGrid.Item(CtrlArtGrid.Row, 1)
            Dim boolstatus As Boolean = CtrlArtGrid.Rows(e.Row)(5)
            If dtgrid.Rows.Count > 0 Then
                For i = 0 To dtgrid.Rows.Count - 1
                    If dtgrid.Rows(i)("ArticleCode") = row Then
                        dtgrid.Rows(i)("Status") = boolstatus
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub CtrlGrid1_CellButtonClick(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles CtrlGrid1.CellButtonClick
        Try
            clear()
            Dim row As String = CtrlGrid1.Item(e.Row, 4)
            'If cls.IsPrepStnTempArticleMap Then
            '    Dim dttemplatemaparticle As DataTable = cls.GetArticleMapwithTemplate(row)
            '    Dim h As Integer
            '    For h = 0 To dttemplatemaparticle.Rows.Count - 1
            '        If cls.CheckArticleExistinMappingTemplate(dttemplatemaparticle.Rows(h)("ArticleCode"), clsAdmin.SiteCode, clsAdmin.UserCode) Then
            '            If h = dttemplatemaparticle.Rows.Count - 1 Then
            '                Exit For
            '            End If
            '        End If
            '    Next
            'End If
            dmdt = cls.GetTemplatedata(row)
            Panel1.Visible = True
            btnCancel.Visible = True
            btnSave.Visible = True
            txtName.ReadOnly = True
            btnCheck.Visible = True
            btnUncheck.Visible = True
            txtTemplateName.ReadOnly = True
            If newdt.Rows.Count > 0 Then
                newdt.Rows.Clear()
            End If
            fillnewTemplate(row)
            cboTemplate.Enabled = False

            txtTemplateName.Text = dmdt.Tables("MstPrepStationTemplate").Rows(0)("mstPrepStationTemplateID")
            txtTemplateShortDesc.Text = dmdt.Tables("MstPrepStationTemplate").Rows(0)("shortDesc")
            txtTempateDesc.Text = dmdt.Tables("MstPrepStationTemplate").Rows(0)("description")
            txtName.Text = dmdt.Tables("MstPrepStation").Rows(0)("mstPrepStationID")
            If dmdt.Tables("MstPrepStation").Rows(0)("status") = True Then
                rbbtnActive.Checked = True
                RbbtnInactive.Checked = False
            Else
                RbbtnInactive.Checked = True
                rbbtnActive.Checked = False
            End If
            If tbl.Rows.Count > 0 Then
                tbl.Rows.Clear()
            End If
            If dtgrid.Rows.Count > 0 Then
                dtgrid.Rows.Clear()
            End If
            'nodedt = tbl.Clone
            nodedt = dmdt.Tables("MstPrepStationTemplateMstArticleNodeMap").Copy
            Dim i As Integer
            For i = 0 To nodedt.Rows.Count - 1
                For Each dr As DataRow In dt.Rows
                    If nodedt.Rows(i)("nodeCode") = dr("NodeCode") Then
                        tbl.Rows.Add(nodedt.Rows(i)("nodeCode"), dr("NODENAME"), dr("PARENTNODECODE"), nodedt.Rows(i)("status"))
                    End If
                Next
            Next
            FlowLayoutPanel1.Controls.Clear()
            showGridData(row)
            If Not CtrlArtGrid.Rows.Count > 1 Then
                btnCheck.Visible = False
                btnUncheck.Visible = False
                CtrlArtGrid.Visible = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cboTemplate_TextChanged(sender As Object, e As EventArgs) Handles cboTemplate.TextChanged
        Try
            If cboTemplate.SelectedIndex <> 0 Then
                Dim name As String = cboTemplate.Text
                Dim dttable As DataTable = cls.GetTemplateArticleshortname(name)
                If dttable.Rows.Count > 0 Then
                    txtTemplateName.Text = dttable.Rows(0)("mstPrepStationTemplateID")
                    txtTemplateShortDesc.Text = dttable.Rows(0)("shortDesc")
                    txtTempateDesc.Text = dttable.Rows(0)("description")
                End If
            Else
                txtTemplateName.Text = ""
                txtTemplateShortDesc.Text = ""
                txtTempateDesc.Text = ""

            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnAddTemplate_Click(sender As Object, e As EventArgs) Handles btnAddTemplate.Click
        Try
            Dim addtemp As New frmAddTemplate
            addtemp.ShowDialog()
            clear()
            fillTemplate()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub frmPrepStationTemplate_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                If Panel1.Visible Then
                    If MessageBox.Show("Your will loose unsaved data, Do you wish to continue?", "Information", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
                        Me.Close()
                    End If
                Else
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub




    Private Sub txtTemplateName_Leave(sender As Object, e As System.EventArgs) Handles txtTemplateName.Leave
        Try
            If txtTemplateName.ReadOnly = False Then
                Dim name As String = txtTemplateName.Text
                If cls.IsTemplateisMappedWithStation(name) Then
                    ShowMessage(name & " Name is Already Exist.", "Information")
                    txtTemplateName.Text = ""
                    txtTemplateName.Focus()
                    Exit Sub
                End If
                If name.Length > 15 Then
                    ShowMessage("Template Name Cannot be Greater Than 15 Chararcter.", "Information")
                    txtTemplateName.Text = ""
                    txtTemplateName.Focus()
                    Exit Sub
                End If
                'If name.StartsWith(" ") Then
                '    txtTemplateName.Clear()
                '    txtTemplateName.Focus()
                '    Exit Sub
                'End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub txtName_Leave(sender As Object, e As System.EventArgs) Handles txtName.Leave
        Try
            Dim name As String = txtName.Text
            If name.Length > 34 Then
                ShowMessage("Template Name Cannot be Greater Than 34 Chararcter.", "Information")
                txtName.Text = ""
                txtName.Focus()
                Exit Sub
            End If
            'If name.StartsWith(" ") Then
            '    txtName.Clear()
            '    txtName.Focus()
            '    Exit Sub
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtName_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        Try
            If System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "[^a-zA-Z0-9 \b]") Then
                e.Handled = True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtTempateDesc_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtTempateDesc.KeyPress
        Try
            If System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "[^a-zA-Z0-9 \b]") Then
                e.Handled = True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub txtTemplateName_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtTemplateName.KeyPress
        Try
            If System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "[^a-zA-Z0-9 \b]") Then
                e.Handled = True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtTemplateShortDesc_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtTemplateShortDesc.KeyPress
        Try
            If System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "[^a-zA-Z0-9 \b]") Then
                e.Handled = True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtTemplateShortDesc_Leave(sender As Object, e As System.EventArgs) Handles txtTemplateShortDesc.Leave
        Try
            Dim name As String = txtTemplateShortDesc.Text
            If name.Length > 100 Then
                ShowMessage("Template Short Description Cannot be Greater Than 100 Chararcter.", "Information")
                txtTemplateShortDesc.Text = ""
                txtTemplateShortDesc.Focus()
                Exit Sub
            End If
            'If name.StartsWith(" ") Then
            '    txtTemplateShortDesc.Clear()
            '    txtTemplateShortDesc.Focus()
            '    Exit Sub
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtTempateDesc_Leave(sender As Object, e As System.EventArgs) Handles txtTempateDesc.Leave
        Try
            Dim name As String = txtTempateDesc.Text
            If name.Length > 500 Then
                ShowMessage("Template  Description Cannot be Greater Than 500 Chararcter.", "Information")
                txtTempateDesc.Text = ""
                txtTempateDesc.Focus()
                Exit Sub
            End If
            'If name.StartsWith(" ") Then
            '    txtTempateDesc.Clear()
            '    txtTempateDesc.Focus()
            '    Exit Sub
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
End Class