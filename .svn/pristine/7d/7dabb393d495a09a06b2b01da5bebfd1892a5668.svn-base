Imports Spectrum
Imports SpectrumBL
Imports SpectrumCommon
Imports C1.Win.C1FlexGrid
Public Class frmGrievance
    Dim objCls As New clsCommon
    Dim objGrievance As New clsGrievance
    Dim _ReturnResultSet As New DataTable
    Dim IsFilterVisible As Boolean = False
    Dim RaisedBy As String = ""
    Dim IsDeleteAuth As Boolean = False


    Private Sub frmGrievance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            RdBtnAll.Enabled = False
            RbBtnCMF.Enabled = False
            RdbtnBO.Enabled = False
            RdBtnFo.Enabled = False

            RdBtnAll.Visible = False
            RdbtnBO.Visible = False
            RdBtnFo.Visible = False
            RbBtnCMF.Visible = False


            ' GridSettings()
            Dim deptBox = objCls.GetDepartment(clsAdmin.UserCode)
            Dim SiteBox = objCls.GetSite(clsAdmin.UserCode, clsAdmin.SiteCode)
            PopulateComboBox(deptBox, cbDepartment)
            PopulateComboBox(SiteBox, CbRaisedSite)
            pC1ComboSetDisplayMember(cbDepartment)
            pC1ComboSetDisplayMember(CbRaisedSite)
            'Dim griTypeBox = objCls.GetGrievanceType()
            'PopulateComboBox(griTypeBox, cbGrievanceType)
            'pC1ComboSetDisplayMember(cbGrievanceType)

            Dim statusBox = objCls.GetStatus()
            If clsDefaultConfiguration.JkTicketingSystem Then
                statusBox.Rows.RemoveAt(3)
            End If
            PopulateComboBox(statusBox, cbStatus)
            pC1ComboSetDisplayMember(cbStatus)

            If clsDefaultConfiguration.JkTicketingSystem Then
                lblGrievanceType.Visible = False
                cbGrievanceType.Visible = False
                lblTicketTitle.Visible = False
                txtGrievanceTitle.Visible = False
                tblpanel.RowStyles(2).SizeType = SizeType.Percent
                tblpanel.RowStyles(2).Height = 0
                'tblpanel.RowStyles(3).SizeType = SizeType.Percent
                'tblpanel.RowStyles(3).Height = 0
            Else
                tblpanel.RowStyles(2).SizeType = SizeType.Percent
                tblpanel.RowStyles(2).Height = 0
                tblpanel.RowStyles(3).SizeType = SizeType.Percent
                tblpanel.RowStyles(3).Height = 16.66
            End If

            '       RdBtnAll.Checked = True
            cbAll.CheckState = CheckState.Checked

            If CheckAuthorisationForTran(clsAdmin.SiteCode, "TicketDelete") = False Then
                IsDeleteAuth = False
            Else
                IsDeleteAuth = True
            End If
            Call cmdSearch_Click(Nothing, Nothing)
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        Try

            'Dim griedate As Date
            'If Not IsDBNull(ToDate.Value) Then
            '    griedate = ToDate.Value
            'End If
            Dim TicketToDate As Date
            If Not IsDBNull(ToDate.Value) Then
                TicketToDate = ToDate.Value
            End If

            Dim TicketFromDate As Date
            If Not IsDBNull(CtrlFromDate.Value) Then
                TicketFromDate = CtrlFromDate.Value
            End If


            Dim TicketUpdatedOn As Date
            If Not IsDBNull(CtrlUpdatedOn.Value) Then
                TicketUpdatedOn = CtrlUpdatedOn.Value
            End If

            Dim grievanceid As String = txtGrievanceId.Text
            Dim title As String = txtGrievanceTitle.Text
            Dim typeid As Integer = cbGrievanceType.SelectedValue
            Dim deptid As Integer = cbDepartment.SelectedValue
            Dim status As String = cbStatus.SelectedValue
            Dim createdBy As String = CtrlTxtCreatedBy.Text
            Dim UpdatedBy As String = CtrlTxtUpdatedBy.Text
            Dim RaisedBySite As String = CbRaisedSite.SelectedValue


            'jk sprint 25

            Dim jkAll, jkRaisedbyCMF, jkRaisedbyFranchisee, jkRaisedbyHo As Boolean


            If cbAll.Checked = True Then
                jkAll = True
            End If

            If cbRaisedbyFranchisee.Checked = True Then
                jkRaisedbyFranchisee = True
            End If
            If cbRaisedbyHo.Checked = True Then
                jkRaisedbyHo = True
            End If
            If cbRaisedbyCMF.Checked = True Then
                jkRaisedbyCMF = True
            End If


            _ReturnResultSet = objCls.GetGrievanceDetails(clsAdmin.UserCode, clsAdmin.SiteCode, grievanceid, title, typeid, deptid, status, TicketFromDate, TicketToDate, TicketUpdatedOn, createdby:=createdBy, updatedby:=UpdatedBy, RaisedBy:=RaisedBy, RaisedBySite:=RaisedBySite, jkAll:=jkAll, jkRaisedbyCMF:=jkRaisedbyCMF, jkRaisedbyFranchisee:=jkRaisedbyFranchisee, jkRaisedbyHo:=jkRaisedbyHo)
            ShowDetail()
            IsRead()
            dgMainGrid.Select(0, 2)
            'txtGrievanceId.Focus()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        Try
            Dim dtFeedTickets As DataTable
            dtFeedTickets = objCls.loadTickets(clsAdmin.SiteCode)
            Dim objFeedback As New frmFeedbackForm(dtFeedTickets)
            If Not dtFeedTickets Is Nothing AndAlso dtFeedTickets.Rows.Count > 0 Then
                Dim FeedBackResult = objFeedback.ShowDialog()
                If FeedBackResult <> Windows.Forms.DialogResult.OK Then
                    Exit Sub
                End If
            End If

            Dim objNew As New frmNGrievance
            objNew.Size = Me.Size
            objNew.StartPosition = FormStartPosition.CenterParent
            If objNew.ShowDialog() = Windows.Forms.DialogResult.OK Then
                cmdSearch_Click(Nothing, Nothing)
            End If
            'code addded for JK sprint 24
            frmGrievance_Load(Nothing, Nothing)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Private Function clearFilter()
        txtGrievanceId.Text = ""
        txtGrievanceTitle.Text = ""
        cbGrievanceType.SelectedValue = ""
        cbDepartment.SelectedValue = ""
        cbStatus.SelectedValue = ""
        CtrlTxtCreatedBy.Text = ""
        CtrlTxtUpdatedBy.Text = ""
        CtrlFromDate.SelectedText = ""
        ToDate.SelectedText = ""
        CtrlUpdatedOn.SelectedText = ""
        CbRaisedSite.SelectedValue = ""
        cmdSearch_Click(Nothing, Nothing)
    End Function

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Try


            Dim objG As New frmNGrievance
            Dim selectedRows() As DataRow = _ReturnResultSet.Select("Select=True", "", DataViewRowState.CurrentRows)
            If (selectedRows.Count = 0) Then
                ShowMessage(getValueByKey("GRV001"), "GRV001 - " & getValueByKey("GRV010"))
                '  ShowMessage("Please select at least one Grievance", " " & getValueByKey("CLAE04"))
                Exit Sub
            End If

            Dim eventType As Int32
            ' Dim result = MessageBox.Show(getValueByKey("GRV013"), "", MessageBoxButtons.OKCancel, MessageBoxIcon.None)
            ShowMessage(getValueByKey("GRV012"), "CM014 - " & getValueByKey("CLAE04"), eventType, "Cancel", "OK")
            If eventType = 1 Then
                For Each dr As DataRow In selectedRows
                    objG.GrievanceId = dr("GrievanceID")
                    objGrievance.DeleteGrivanceDetail(clsAdmin.SiteCode, objG.GrievanceId)
                    dr.Delete()
                Next
                ShowMessage(getValueByKey("GRV002"), "GRV002 - " & getValueByKey("GRV010"))
            Else
                Exit Sub
            End If

            'ShowMessage("Grievance Deleted Successfully ", " " & getValueByKey("CLAE04"))
            'txtGrievanceId.Focus()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub GridSettings()
        Try

            If clsDefaultConfiguration.JkTicketingSystem Then
                dgMainGrid.Cols("GrievanceTitle").Visible = False
                dgMainGrid.Cols("GrievanceType").Visible = False
            Else
                dgMainGrid.Cols("GrievanceTitle").Visible = True
                dgMainGrid.Cols("GrievanceType").Visible = True

                dgMainGrid.Cols("GrievanceTitle").Width = 100
                dgMainGrid.Cols("GrievanceTitle").AllowEditing = False
                dgMainGrid.Cols("GrievanceTitle").Caption = getValueByKey("frmGrievance.dgMainGrid.grievancetitle")

                dgMainGrid.Cols("GrievanceType").Width = 100
                dgMainGrid.Cols("GrievanceType").AllowEditing = False
                dgMainGrid.Cols("GrievanceType").Caption = getValueByKey("frmGrievance.dgMainGrid.grievancetype")
            End If

            If IsDeleteAuth Then
                dgMainGrid.Cols("Select").Width = 30
                dgMainGrid.Cols("Select").Caption = ""
                dgMainGrid.Cols("Select").Visible = True
                cmdDelete.Visible = True
                chkselectall.Visible = True
                cmdNew.Location = New System.Drawing.Point(dgMainGrid.Right - cmdDelete.Width - cmdNew.Width - 6, dgMainGrid.Bottom + 6)
                ' cmdDelete.Location = New System.Drawing.Point(dgMainGrid.Bottom + 10 - dgMainGrid.Right - cmdDelete.Width - 0)
                'cmdNew.Size = New Size(66, 22)
            Else
                dgMainGrid.Cols("Select").Width = 30
                dgMainGrid.Cols("Select").Caption = ""
                dgMainGrid.Cols("Select").Visible = False
                cmdDelete.Visible = False
                chkselectall.Visible = False
                cmdNew.Location = New System.Drawing.Point(dgMainGrid.Right - cmdDelete.Width, dgMainGrid.Bottom + 10)
                'cmdDelete.Location = New System.Drawing.Point(dgMainGrid.Bottom + 10 - dgMainGrid.Right - cmdDelete.Width - 0)
                cmdNew.Size = New Size(66, 27)
            End If


            dgMainGrid.Cols("GrievanceID").Width = 120
            dgMainGrid.Cols("GrievanceID").AllowEditing = False
            dgMainGrid.Cols("GrievanceID").Caption = getValueByKey("frmGrievance.dgMainGrid.grievanceid")


            dgMainGrid.Cols("RaisedFrom Site/Dept").Width = 150
            dgMainGrid.Cols("RaisedFrom Site/Dept").AllowEditing = False
            dgMainGrid.Cols("RaisedFrom Site/Dept").Name = "RaisedFrom Site/Dept"

            dgMainGrid.Cols("RaisedToDept/Site").Width = 150
            dgMainGrid.Cols("RaisedToDept/Site").AllowEditing = False
            dgMainGrid.Cols("RaisedToDept/Site").Name = "RaisedToDept/Site"


            dgMainGrid.Cols("Status").Width = 100
            dgMainGrid.Cols("Status").AllowEditing = False


            'dgMainGrid.Cols("CreatedAt").Width = 100
            'dgMainGrid.Cols("CreatedAt").AllowEditing = False
            'dgMainGrid.Cols("CreatedBy").Caption = getValueByKey("FRMGRIEVANCE.DGMAINGRID.CreatedBy")

            dgMainGrid.Cols("CreatedBy").Width = 100
            dgMainGrid.Cols("CreatedBy").AllowEditing = False
            dgMainGrid.Cols("CreatedBy").Caption = getValueByKey("FRMGRIEVANCE.DGMAINGRID.CreatedBy")

            dgMainGrid.Cols("CreatedOn").Width = 100
            dgMainGrid.Cols("CreatedOn").AllowEditing = False
            dgMainGrid.Cols("CreatedOn").Caption = getValueByKey("FRMGRIEVANCE.DGMAINGRID.CreatedOn")
            dgMainGrid.Cols("CreatedOn").Format = "MM/dd/yyyy HH:mm:ss"

            dgMainGrid.Cols("UpdatedBy").Width = 100
            dgMainGrid.Cols("UpdatedBy").AllowEditing = False
            dgMainGrid.Cols("UpdatedBy").Caption = getValueByKey("FRMGRIEVANCE.DGMAINGRID.UpdatedBy")

            dgMainGrid.Cols("UpdatedOn").Width = 100
            dgMainGrid.Cols("UpdatedOn").AllowEditing = False
            dgMainGrid.Cols("UpdatedOn").Caption = getValueByKey("FRMGRIEVANCE.DGMAINGRID.UpdatedOn")
            dgMainGrid.Cols("UpdatedOn").Format = "MM/dd/yyyy HH:mm:ss"
            'dgMainGrid.ExtendLastCol = True
            dgMainGrid.Cols("IsRead").Width = 100
            dgMainGrid.Cols("IsRead").AllowEditing = False
            dgMainGrid.Cols("IsRead").Visible = False
            dgMainGrid.Size = New Point(cmdDelete.Right - tblpanel.Left, dgMainGrid.Size.Height)

            'dgMainGrid.PerformLayout()
            'dgMainGrid.Refresh()

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub ShowDetail()
        Try
            dgMainGrid.DataSource = _ReturnResultSet
            GridSettings()
            SetCulture(Me, Me.Name)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub New()
        InitializeComponent()
        dgMainGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
    End Sub

    Private Sub dgMainGrid_DoubleClick(sender As Object, e As EventArgs) Handles dgMainGrid.DoubleClick
        Try
            Using objGr As New frmNGrievance
                objGr.Size = Me.Size
                objGr.StartPosition = FormStartPosition.CenterParent
                objGr.IsEdit = True
                If RdbtnBO.Checked Then
                    objGr._RaisedBy = "Bo"
                ElseIf RdBtnFo.Checked Then
                    objGr._RaisedBy = "Fo"
                End If
                objGr.GrievanceId = dgMainGrid.Rows(dgMainGrid.RowSel)("GrievanceID").ToString()
                objGr.UpdatedBy = dgMainGrid.Rows(dgMainGrid.RowSel)("UpdatedBy").ToString()
                objGr.IsRead = dgMainGrid.Rows(dgMainGrid.RowSel)("IsRead").ToString()
                If objGr.ShowDialog() = Windows.Forms.DialogResult.OK Then

                End If
                cmdSearch_Click(sender, e)
            End Using
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cbDepartment_SelectedValueChanged(sender As Object, e As EventArgs) Handles cbDepartment.SelectedValueChanged
        Dim deptid As String = ""
        If Not cbDepartment.SelectedValue Is Nothing Then
            deptid = cbDepartment.SelectedValue.ToString()
        End If
        Dim deptBox = objCls.GetSelectedDepartment(deptid)
        PopulateComboBox(deptBox, cbGrievanceType)
        pC1ComboSetDisplayMember(cbGrievanceType)
    End Sub

    Private Sub CmdShowFilter_Click(sender As Object, e As EventArgs) Handles CmdShowFilter.Click
        If IsFilterVisible = False Then
            tblpanel.Visible = True
            dgMainGrid.Top = tblpanel.Bottom + 5
            CmdShowFilter.Text = "- Filter"
            dgMainGrid.Size = New Point(dgMainGrid.Size.Width, cmdNew.Top - tblpanel.Bottom - 10)
            chkselectall.Location = New Point(dgMainGrid.Left + 10, dgMainGrid.Top + 5)
        Else
            tblpanel.Visible = False
            dgMainGrid.Top = CmdShowFilter.Bottom + 5
            CmdShowFilter.Text = "+ Filter"
            dgMainGrid.Size = New Point(dgMainGrid.Size.Width, cmdNew.Top - CmdShowFilter.Bottom - 10)
            chkselectall.Location = New Point(dgMainGrid.Left + 10, dgMainGrid.Top + 5)
        End If
        IsFilterVisible = Not IsFilterVisible
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles chkselectall.CheckedChanged
        If chkselectall.Checked Then
            For index = 1 To dgMainGrid.Rows.Count - 1
                dgMainGrid.Rows(index)("SELECT") = 1
            Next
        Else
            For index = 1 To dgMainGrid.Rows.Count - 1
                dgMainGrid.Rows(index)("SELECT") = 0
            Next
        End If

    End Sub

    Private Sub CtrlBtnClear_Click(sender As Object, e As EventArgs) Handles CtrlBtnClear.Click
        clearFilter()
        cbRaisedbyCMF.CheckState = CheckState.Unchecked

        cbRaisedbyFranchisee.CheckState = CheckState.Unchecked
        cbRaisedbyHo.CheckState = CheckState.Unchecked
        cbAll.CheckState = CheckState.Checked
    End Sub

    Private Sub RdbtnBO_CheckedChanged(sender As Object, e As EventArgs) Handles RdbtnBO.CheckedChanged
        RaisedBy = "BO"
        cmdSearch_Click(Nothing, Nothing)
    End Sub

    Private Sub RdBtnFo_CheckedChanged(sender As Object, e As EventArgs) Handles RdBtnFo.CheckedChanged
        RaisedBy = "FO"
        cmdSearch_Click(Nothing, Nothing)
    End Sub
    Private Sub RdBtnAll_CheckedChanged(sender As Object, e As System.EventArgs) Handles RdBtnAll.CheckedChanged
        RaisedBy = ""
        cmdSearch_Click(Nothing, Nothing)
    End Sub
    Private Sub RbBtnCMF_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RbBtnCMF.CheckedChanged
        RaisedBy = "CMF"
        cmdSearch_Click(Nothing, Nothing)
    End Sub

    Private Function Themechange()

        Me.BackgroundColor = Color.FromArgb(134, 134, 134)

        ' tblpanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
        dgMainGrid.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgMainGrid.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        dgMainGrid.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212)
        dgMainGrid.Rows.MinSize = 25
        dgMainGrid.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        dgMainGrid.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgMainGrid.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
        cmdNew.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdNew.BackColor = Color.Transparent
        cmdNew.BackColor = Color.FromArgb(0, 107, 163)
        cmdNew.ForeColor = Color.FromArgb(255, 255, 255)
        cmdNew.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdNew.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdNew.FlatStyle = FlatStyle.Flat
        cmdNew.FlatAppearance.BorderSize = 0
        cmdNew.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        cmdNew.Size = New Size(66, 30)

        cmdDelete.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdDelete.BackColor = Color.Transparent
        cmdDelete.BackColor = Color.FromArgb(0, 107, 163)
        cmdDelete.ForeColor = Color.FromArgb(255, 255, 255)
        cmdDelete.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdDelete.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdDelete.FlatStyle = FlatStyle.Flat
        cmdDelete.FlatAppearance.BorderSize = 0
        cmdDelete.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        cmdSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdSearch.BackColor = Color.Transparent
        cmdSearch.BackColor = Color.FromArgb(0, 107, 163)
        cmdSearch.ForeColor = Color.FromArgb(255, 255, 255)
        cmdSearch.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdSearch.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdSearch.FlatStyle = FlatStyle.Flat
        cmdSearch.FlatAppearance.BorderSize = 0
        cmdSearch.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        cmdSearch.Size = New Size(66, 27)

        CmdShowFilter.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CmdShowFilter.BackColor = Color.Transparent
        CmdShowFilter.BackColor = Color.FromArgb(0, 107, 163)
        CmdShowFilter.ForeColor = Color.FromArgb(255, 255, 255)
        CmdShowFilter.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CmdShowFilter.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CmdShowFilter.FlatStyle = FlatStyle.Flat
        CmdShowFilter.FlatAppearance.BorderSize = 0
        CmdShowFilter.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        CmdShowFilter.Size = New Size(85, 27)

        CtrlBtnClear.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnClear.BackColor = Color.Transparent
        CtrlBtnClear.BackColor = Color.FromArgb(0, 107, 163)
        CtrlBtnClear.ForeColor = Color.FromArgb(255, 255, 255)
        CtrlBtnClear.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CtrlBtnClear.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CtrlBtnClear.FlatStyle = FlatStyle.Flat
        CtrlBtnClear.FlatAppearance.BorderSize = 0
        CtrlBtnClear.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        Me.tblpanel.ColumnStyles(3).Width = 135
        Me.Panel1.MinimumSize = New Size(135, 30)
        Me.CtrlBtnClear.Size = New Size(57, 27)
        Me.CtrlBtnClear.Location = New System.Drawing.Point(0, 0)
        Me.cmdSearch.Size = New Size(64, 27)
        Me.cmdSearch.Location = New System.Drawing.Point(62, 0)
        RdBtnFo.ForeColor = Color.White
        RdbtnBO.ForeColor = Color.White

        tblpanel.Padding = New Padding(0, 4, 0, 0)
        tblpanel.Margin = New Padding(0, 0, 0, 0)

        CtrlLabel1.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel1.AutoSize = False
        CtrlLabel1.Size = New Size(150, 20)
        CtrlLabel1.BorderStyle = BorderStyle.None
        CtrlLabel1.Margin = New Padding(3, 0, 0, 0)
        CtrlFromDate.Margin = New Padding(0, 0, 0, 0)
        CtrlLabel2.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel2.AutoSize = False
        CtrlLabel2.Size = New Size(150, 20)
        CtrlLabel2.BorderStyle = BorderStyle.None
        CtrlLabel2.Margin = New Padding(3, 0, 0, 0)
        CbRaisedSite.Dock = DockStyle.Fill

        CtrlFromDate.MinimumSize = New Size(150, 20)
        CtrlFromDate.MaximumSize = New Size(150, 20)
        CtrlFromDate.Size = New Size(150, 20)
        CtrlLabel4.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel4.AutoSize = False
        CtrlLabel4.Size = New Size(150, 20)
        CtrlLabel4.Margin = New Padding(3, 0, 0, 0)
        CtrlUpdatedOn.Margin = New Padding(0, 0, 0, 0)
        CtrlUpdatedOn.MinimumSize = New Size(150, 20)
        CtrlUpdatedOn.MaximumSize = New Size(150, 20)
        CtrlUpdatedOn.Size = New Size(150, 20)

        CtrlLabel3.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel3.AutoSize = False
        CtrlLabel3.Size = New Size(150, 20)
        CtrlLabel3.Margin = New Padding(3, 0, 0, 0)
        CtrlTxtCreatedBy.Margin = New Padding(0, 0, 0, 0)
        CtrlTxtCreatedBy.MinimumSize = New Size(150, 20)
        CtrlTxtCreatedBy.MaximumSize = New Size(150, 20)
        CtrlTxtCreatedBy.Size = New Size(150, 20)

        CtrlLabel5.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel5.AutoSize = False
        CtrlLabel5.Size = New Size(150, 20)
        CtrlLabel5.Margin = New Padding(3, 0, 0, 0)
        CtrlTxtUpdatedBy.Margin = New Padding(0, 0, 0, 0)
        CtrlTxtUpdatedBy.MinimumSize = New Size(150, 20)
        CtrlTxtUpdatedBy.MaximumSize = New Size(150, 20)
        CtrlTxtUpdatedBy.Size = New Size(150, 20)

        lblDate.BackColor = Color.FromArgb(212, 212, 212)
        lblDate.AutoSize = False
        lblDate.Size = New Size(150, 20)
        lblDate.Margin = New Padding(3, 0, 0, 0)

        ToDate.Margin = New Padding(0, 0, 0, 0)
        ToDate.MinimumSize = New Size(150, 20)
        ToDate.MaximumSize = New Size(150, 20)
        ToDate.Size = New Size(150, 20)

        lblDepartment.BackColor = Color.FromArgb(212, 212, 212)
        lblDepartment.AutoSize = False
        lblDepartment.Size = New Size(150, 20) '150, 30
        lblDepartment.Margin = New Padding(3, 0, 0, 0)

        cbDepartment.Margin = New Padding(0, 0, 0, 0)
        cbDepartment.MinimumSize = New Size(150, 20)
        cbDepartment.MaximumSize = New Size(150, 20)
        cbDepartment.Size = New Size(150, 20)

        lblGrievanceId.BackColor = Color.FromArgb(212, 212, 212)
        lblGrievanceId.AutoSize = False
        lblGrievanceId.Size = New Size(150, 20)
        lblGrievanceId.Margin = New Padding(3, 0, 0, 0)
        txtGrievanceId.Margin = New Padding(0, 0, 0, 0)
        txtGrievanceId.MinimumSize = New Size(150, 20)
        txtGrievanceId.MaximumSize = New Size(150, 20)
        txtGrievanceId.Size = New Size(150, 20)


        lblGrievanceType.BackColor = Color.FromArgb(212, 212, 212)
        lblGrievanceType.AutoSize = False
        lblGrievanceType.Size = New Size(150, 20)
        lblGrievanceType.Margin = New Padding(3, 0, 0, 0)
        cbGrievanceType.Margin = New Padding(0, 0, 0, 0)
        cbGrievanceType.MinimumSize = New Size(150, 20)
        cbGrievanceType.MaximumSize = New Size(150, 20)
        cbGrievanceType.Size = New Size(150, 20)

        lblTicketTitle.BackColor = Color.FromArgb(212, 212, 212)
        lblTicketTitle.AutoSize = False
        lblTicketTitle.Size = New Size(150, 20)
        lblTicketTitle.Margin = New Padding(3, 0, 0, 0)
        txtGrievanceTitle.Margin = New Padding(0, 0, 0, 0)
        txtGrievanceTitle.MinimumSize = New Size(150, 20)
        txtGrievanceTitle.MaximumSize = New Size(150, 20)
        txtGrievanceTitle.Size = New Size(150, 20)

        lblStatus.BackColor = Color.FromArgb(212, 212, 212)
        lblStatus.AutoSize = False
        lblStatus.Size = New Size(150, 20)
        lblStatus.Margin = New Padding(3, 0, 0, 0)
        cbStatus.Margin = New Padding(0, 0, 0, 0)
        cbStatus.MinimumSize = New Size(150, 20)
        cbStatus.MaximumSize = New Size(150, 20)
        cbStatus.Size = New Size(150, 20)

        RdBtnAll.ForeColor = Color.White
        'If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
        '    For i = 0 To dgMainGrid.Cols.Count - 1
        '        dgMainGrid.Cols(i).Caption = dgMainGrid.Cols(i).Caption.ToUpper
        '    Next
        'End If

        tblpanel.ColumnStyles(0).SizeType = SizeType.Absolute
        tblpanel.ColumnStyles(0).Width = 100

        tblpanel.ColumnStyles(1).SizeType = SizeType.Absolute
        tblpanel.ColumnStyles(1).Width = 180

        tblpanel.ColumnStyles(2).SizeType = SizeType.Absolute
        tblpanel.ColumnStyles(2).Width = 100

        tblpanel.ColumnStyles(3).SizeType = SizeType.Absolute
        tblpanel.ColumnStyles(3).Width = 150

        tblpanel.RowStyles(0).SizeType = SizeType.Absolute
        tblpanel.RowStyles(0).Height = 25

        tblpanel.RowStyles(1).SizeType = SizeType.Absolute
        tblpanel.RowStyles(1).Height = 25

        If lblGrievanceType.Visible = False AndAlso cbGrievanceType.Visible = False Then
            tblpanel.RowStyles(2).SizeType = SizeType.Absolute
            tblpanel.RowStyles(2).Height = 0

            tblpanel.RowStyles(3).SizeType = SizeType.Absolute
            tblpanel.RowStyles(3).Height = 0
            tblpanel.Size = New Size(605, 150)
        Else
            tblpanel.RowStyles(3).SizeType = SizeType.Absolute
            tblpanel.RowStyles(3).Height = 25

            tblpanel.RowStyles(4).SizeType = SizeType.Absolute
            tblpanel.RowStyles(4).Height = 25

            tblpanel.Size = New Size(605, 216)
        End If

        tblpanel.RowStyles(4).SizeType = SizeType.Absolute
        tblpanel.RowStyles(4).Height = 25

        tblpanel.RowStyles(5).SizeType = SizeType.Absolute
        tblpanel.RowStyles(5).Height = 25

        tblpanel.RowStyles(6).SizeType = SizeType.Absolute
        tblpanel.RowStyles(6).Height = 25



    End Function

    Private Function IsRead()
        For i = 1 To dgMainGrid.Rows.Count - 1
            If dgMainGrid.Rows(i)("IsRead").ToString() = True Then
                If dgMainGrid.Rows(i)("UpdatedBy").ToString().ToUpper <> clsAdmin.UserCode.ToUpper Then
                    ' Me.dgMainGrid.Rows(i).StyleNew.BackColor = Color.Gray
                    Me.dgMainGrid.Rows(i).StyleNew.Font = New Font("Neo Sans", 9.25, FontStyle.Bold)
                End If
            End If
        Next
    End Function
    Private Sub cbAll_CheckedChanged(sender As Object, e As EventArgs) Handles cbAll.CheckedChanged
        If cbAll.Checked = True Then
            'cbRaisedbyCMF.Enabled = False
            'cbRaisedbyFranchisee.Enabled = False
            'cbRaisedbyHo.Enabled = False
            cbRaisedbyCMF.CheckState = CheckState.Unchecked

            cbRaisedbyFranchisee.CheckState = CheckState.Unchecked
            cbRaisedbyHo.CheckState = CheckState.Unchecked
            cbAll.CheckState = CheckState.Checked


        Else


            cbRaisedbyCMF.Enabled = True
            cbRaisedbyFranchisee.Enabled = True
            cbRaisedbyHo.Enabled = True


        End If
    End Sub

    Private Sub cbAll_CheckStateChanged(sender As Object, e As EventArgs) Handles cbAll.CheckStateChanged

        ' RaisedBy = ""
        cmdSearch_Click(Nothing, Nothing)


    End Sub

    Private Sub cbRaisedbyFranchisee_CheckStateChanged(sender As Object, e As EventArgs) Handles cbRaisedbyFranchisee.CheckStateChanged
        cbAll.CheckState = CheckState.Unchecked
        ' RaisedBy = "FO"
        cmdSearch_Click(Nothing, Nothing)


    End Sub

    Private Sub cbRaisedbyHo_CheckStateChanged(sender As Object, e As EventArgs) Handles cbRaisedbyHo.CheckStateChanged
        cbAll.CheckState = CheckState.Unchecked
        ' RaisedBy = "BO"
        cmdSearch_Click(Nothing, Nothing)


    End Sub

    Private Sub cbRaisedbyCMF_CheckStateChanged(sender As Object, e As EventArgs) Handles cbRaisedbyCMF.CheckStateChanged
        cbAll.CheckState = CheckState.Unchecked
        ' RaisedBy = "CMF"
        cmdSearch_Click(Nothing, Nothing)


    End Sub
End Class