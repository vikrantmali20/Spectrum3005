Imports SpectrumBL

Public Class frmAddTemplate

    Dim cls As New clsCommon
    Dim tbl As New DataTable

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If Panel1.Visible Then
            If MessageBox.Show("Your will loose unsaved data, Do you wish to continue?", "Information", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If
       
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If txtTemplateName.Text = "" Then
                ShowMessage("Template Name Can't be left Blank.", "Information")
                Exit Sub
            End If
            If txtTemplateShortDesc.Text = "" Then
                ShowMessage("Template Short Name Can't be Left Blank.", "Information")
                Exit Sub
            End If
            If txtDescription.Text = "" Then
                ShowMessage("Template Description Can't be left Blank.", "Information")
                Exit Sub
            End If

            Dim templatename As String = txtTemplateName.Text.Trim
            Dim templateshortdesc As String = txtTemplateShortDesc.Text.Trim
            Dim tempdescription As String = txtDescription.Text.Trim
            Dim sitecode As String = clsAdmin.SiteCode
            Dim siteName As String = cls.GetSiteName(sitecode)
            Dim usercode As String = clsAdmin.UserName
            Dim tempstatus As Boolean = IIf(rbbtnActive.Checked = True, True, False)

            If cls.IsTemplateisMappedWithStation(templatename) Then
                If tempstatus = False Then
                    ShowMessage(templatename + " is already Mapped. It Can't be De-activated.", "Information")
                    rbbtnActive.Checked = True
                    Exit Sub
                End If
            End If

            If cls.AddTemplate(templatename, templateshortdesc, tempdescription, sitecode, usercode, siteName, tempstatus) Then
                ShowMessage("Data Saved Successfully.", "Information")
                clear()
            End If
            frmAddTemplate_Load(sender, e)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub clear()
        Try
            txtTemplateName.Text = ""
            txtTemplateShortDesc.Text = ""
            txtDescription.Text = ""
            rbbtnActive.Checked = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub frmAddTemplate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Panel1.Visible = False
            btnSave.Visible = False

            'If Not tbl.Columns.Contains("NodeCode") Then
            '    tbl.Columns.Add("NodeCode", System.Type.GetType("System.String"))
            '    tbl.Columns.Add("NodeName", System.Type.GetType("System.String"))
            '    tbl.Columns.Add("ParentName", System.Type.GetType("System.String"))
            'End If

            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                ThemeChange()
            End If
            showtemplate()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub showtemplate()
        Try
            Try
                Dim dtgrid As DataTable = cls.shownewTempate()
                gridsetting()
                Dim k As Int32
                Dim foundrow As DataRow

                gridsetting()
                Dim jK As Integer = 1
                For Each dr In dtgrid.Rows
                    CtrlGrid1.Rows.Add()
                    CtrlGrid1.Rows(jK)(2) = dr("mstPrepStationTemplateID")
                    CtrlGrid1.Rows(jK)(3) = dr("ShortDesc")
                    CtrlGrid1.Rows(jK)(4) = dr("Status")
                    CtrlGrid1.Rows(jK)(5) = dr("createdOn")
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

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub gridsetting()
        Try
            CtrlGrid1.Cols(1).Width = 20
            CtrlGrid1.Cols(1).ComboList = " "
            CtrlGrid1.Cols(1).ComboList = "..."
            CtrlGrid1.Cols(1).Visible = False
            CtrlGrid1.Cols(1).AllowEditing = True
            CtrlGrid1.Cols(1).Caption = ""
            CtrlGrid1.Cols(2).Visible = True
            CtrlGrid1.Cols(2).Caption = "Template Name"
            CtrlGrid1.Cols(2).Width = 200
            CtrlGrid1.Cols(2).AllowEditing = False
            CtrlGrid1.Cols(3).Visible = True
            CtrlGrid1.Cols(3).Caption = "Short Descriprtion"
            CtrlGrid1.Cols(3).Width = 200
            CtrlGrid1.Cols(3).AllowEditing = False
            CtrlGrid1.Cols(4).Visible = True
            CtrlGrid1.Cols(4).Caption = "Status"
            CtrlGrid1.Cols(4).Width = 150
            CtrlGrid1.Cols(4).AllowEditing = False
            CtrlGrid1.Cols(5).Visible = True
            CtrlGrid1.Cols(5).Caption = "Created On"
            CtrlGrid1.Cols(5).Width = 205
            CtrlGrid1.Cols(5).AllowEditing = False
            CtrlGrid1.Cols(6).Width = 20
            CtrlGrid1.Cols(6).ComboList = "..."
            CtrlGrid1.Cols(6).Visible = True
            CtrlGrid1.Cols(6).AllowEditing = True
            CtrlGrid1.Cols(6).Caption = "Edit"
            'CtrlGrid1.Cols(6).ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter
            'CtrlGrid1.Cols(6).ImageAlignFixed = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter
            'CtrlGrid1.Cols(6).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            'Dim lnk As DataGridViewLinkColumn = New DataGridViewLinkColumn()
            'CtrlGrid1.Cols.Add(Convert.ToInt32(lnk))
            'lnk.HeaderText = "Link Data"
            'lnk.Name = "Edit"
            'lnk.Text = "Edit"
            'lnk.UseColumnTextForLinkValue = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Public Sub ThemeChange()
        'create save
        btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnSave.BackColor = Color.Transparent
        btnSave.BackColor = Color.FromArgb(0, 107, 163)
        btnSave.ForeColor = Color.FromArgb(255, 255, 255)
        btnSave.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat

        'create cancel
        btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnCancel.BackColor = Color.Transparent
        btnCancel.BackColor = Color.FromArgb(0, 107, 163)
        btnCancel.ForeColor = Color.FromArgb(255, 255, 255)
        btnCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat

        'create create
        btnCreate.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnCreate.BackColor = Color.Transparent
        btnCreate.BackColor = Color.FromArgb(0, 107, 163)
        btnCreate.ForeColor = Color.FromArgb(255, 255, 255)
        btnCreate.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnCreate.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnCreate.FlatAppearance.BorderSize = 0
        btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat

        'CtrlGrid1
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

        Me.BackColor = Color.FromArgb(134, 134, 134)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        Try
            Panel1.Visible = True
            btnSave.Visible = True
            txtTemplateName.ReadOnly = False
            clear()
            txtTemplateName.Focus()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub frmAddTemplate_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub CtrlGrid1_CellButtonClick(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles CtrlGrid1.CellButtonClick
        Try
            clear()
            Panel1.Visible = True
            btnSave.Visible = True
            txtTemplateName.ReadOnly = False
            Dim dmdt As DataTable
            Dim row As String = CtrlGrid1.Item(CtrlGrid1.Row, 2)
            dmdt = cls.GetTemplateArticleshortname(row)
            txtTemplateName.Text = dmdt.Rows(0)("mstPrepStationTemplateID")
            txtTemplateShortDesc.Text = dmdt.Rows(0)("shortDesc")
            txtDescription.Text = dmdt.Rows(0)("description")
            txtTemplateName.ReadOnly = True
            If dmdt.Rows(0)("status") = True Then
                rbbtnActive.Checked = True
                rbbtnInactive.Checked = False
            Else
                rbbtnInactive.Checked = True
                rbbtnActive.Checked = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtTemplateName_Leave(sender As Object, e As System.EventArgs) Handles txtTemplateName.Leave
        Try
            If txtTemplateName.ReadOnly = False Then
                Dim name As String = txtTemplateName.Text
                If cls.CheckTemplateNameExist(name) Then
                    ShowMessage(name & " Name is Already Exist.", "Information")
                    txtTemplateName.Text = ""
                    Exit Sub
                End If
                If name.Length > 15 Then
                    ShowMessage("Template Name Cannot be Greater Than 15 Chararcter.", "Information")
                    txtTemplateName.Text = ""
                    txtTemplateName.Focus()
                    Exit Sub
                End If
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
                Exit Sub
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtDescription_Leave(sender As Object, e As System.EventArgs) Handles txtDescription.Leave
        Try
            Dim name As String = txtDescription.Text
            If name.Length > 500 Then
                ShowMessage("Template  Description Cannot be Greater Than 500 Chararcter.", "Information")
                txtDescription.Text = ""
                Exit Sub
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtDescription_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtDescription.KeyPress
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
End Class