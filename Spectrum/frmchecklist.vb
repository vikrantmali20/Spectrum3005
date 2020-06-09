Imports SpectrumBL
Imports C1.Win.C1FlexGrid
Public Class frmchecklist
    Dim obj As New clsCommon

    Dim dtMain, dtHdr, dtDtl As New DataTable
    Public _ChecklistOpenDate As DateTime

    Dim ALT_F4 As Boolean = False

    Public Property ChecklistOpenDate As DateTime
        Get
            Return _ChecklistOpenDate
        End Get
        Set(ByVal value As DateTime)
            _ChecklistOpenDate = value
        End Set
    End Property
    Public _CheckListId As String
    Public Property CheckListId As String
        Get
            Return _CheckListId
        End Get
        Set(ByVal value As String)
            _CheckListId = value
        End Set
    End Property

    Private Sub frmchecklist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            dtMain = obj.GetCheckListDetails()
            gridChecklist.DataSource = dtMain
            HideGridColumns(gridChecklist)
            makeGridStructure()
            _ChecklistOpenDate = DateTime.Now
            Me.Text = getValueByKey("CLIST00")

            gridChecklist.Styles.Normal.WordWrap = True
            gridChecklist.AutoSizeRows()

            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Public Function HideGridColumns(ByRef gridControl As CtrlGrid) As Boolean
        Try
            gridControl.AllowSorting = False
            gridControl.ShowSort = False

            'gridControl.Cols("DocumentType").Visible = False
            'gridControl.Cols("FldType").Visible = False
            'gridControl.Cols("FldLabel").Visible = False
            gridControl.Cols("CheckListId").Caption = "CheckListId"
            gridControl.Cols("CheckListId").AllowEditing = False
            gridControl.Cols("CheckListId").WidthDisplay = 50 'CInt(gridControl.Width - (gridControl.Width * 0.35))
            gridControl.Cols("CheckListId").StyleDisplay.BackColor = Color.AliceBlue


            gridControl.Cols("SRNo").Caption = "Sr No."
            gridControl.Cols("SRNo").AllowEditing = False
            gridControl.Cols("SRNo").WidthDisplay = 50 'CInt(gridControl.Width - (gridControl.Width * 0.35))
            gridControl.Cols("SRNo").StyleDisplay.BackColor = Color.AliceBlue


            gridControl.Cols("Questions").Caption = "Question"
            gridControl.Cols("Questions").AllowEditing = False
            gridControl.Cols("Questions").WidthDisplay = 450 'CInt(gridControl.Width - (gridControl.Width * 0.35))
            gridControl.Cols("Questions").StyleDisplay.BackColor = Color.AliceBlue


            gridControl.Cols("TypeofValue").Caption = "Answer"
            gridControl.Cols("TypeofValue").WidthDisplay = 150
            gridControl.Cols("TypeofValue").StyleDisplay.WordWrap = True

            gridControl.Cols("CheckListId").Visible = False
            Dim getColumnType As String = String.Empty

            'Create styles with data types, formats, etc
            Dim cellStyle As C1.Win.C1FlexGrid.CellStyle

            cellStyle = gridControl.Styles.Add("CellIntType")
            cellStyle.DataType = Type.GetType("System.Int32")


            cellStyle = gridControl.Styles.Add("CellStringType")
            cellStyle.DataType = Type.GetType("System.String")
            cellStyle.WordWrap = True

            'Assign styles to editable cells
            Dim assignCellStyles As CellRange

            For rowIndex = 1 To gridControl.Rows.Count - 1
                getColumnType = gridControl.Item(rowIndex, "TypeofValue").ToString.ToLower
                gridControl.Rows(rowIndex).HeightDisplay = 30

                If getColumnType.ToUpper = "Boolean".ToUpper Then
                    gridControl.Rows.Item(rowIndex).ComboList = "YES|NA"
                    gridControl.Rows.Item(rowIndex).Item(3) = ""

                ElseIf getColumnType.ToUpper = "Integer".ToUpper Then

                    assignCellStyles = gridControl.GetCellRange(rowIndex, 3)
                    assignCellStyles.Style = gridControl.Styles("CellIntType")
                    gridControl.Rows(rowIndex).Item(3) = ""

                ElseIf getColumnType.ToUpper = "String".ToUpper Then
                    assignCellStyles = gridControl.GetCellRange(rowIndex, 3)
                    assignCellStyles.Style = gridControl.Styles("CellStringType")
                    gridControl.Rows(rowIndex).Item(3) = ""

                Else
                    assignCellStyles = gridControl.GetCellRange(rowIndex, 3)
                    assignCellStyles.Style = gridControl.Styles("CellStringType")
                    gridControl.Rows(rowIndex).Item(3) = ""
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.Escape Then
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
    Private Sub btnSaveCheckList_Click(sender As Object, e As EventArgs) Handles btnSaveCheckList.Click
        Try
            If IsFormValid(gridChecklist) Then

                Dim DocumentNo As String = clsAdmin.SiteCode.ToString.Substring(clsAdmin.SiteCode.ToString.Length - 3) & clsAdmin.TerminalID & _ChecklistOpenDate.Year.ToString() & _ChecklistOpenDate.ToString("MM") & _ChecklistOpenDate.ToString("dd")
                Dim status As Boolean = obj.SaveChecklistData(_CheckListId, clsAdmin.SiteCode, clsAdmin.UserName, clsAdmin.TerminalID, _ChecklistOpenDate, DocumentNo, dtDtl)
                If status Then

                    ShowMessage(getValueByKey("CLIST01"), getValueByKey("CLAE04"))
                    ALT_F4 = False
                    Me.Close()
                End If

            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Public Function IsFormValid(ByRef gridControl As CtrlGrid) As Boolean
        Try

            Dim dtlRow As DataRow

            For rowIndex = 1 To gridControl.Rows.Count - 1
                _CheckListId = gridControl.Rows(rowIndex).Item(0).ToString().Trim
                If gridControl.Rows(rowIndex).Item(3).ToString().Trim = "" Then
                    ShowMessage(getValueByKey("CLIST02"), getValueByKey("CLAE04"))
                    Return False
                    Exit Function

                End If
            Next

            For rowIndex = 1 To gridControl.Rows.Count - 1
                dtlRow = dtDtl.NewRow()
                dtlRow(0) = gridControl.Rows(rowIndex).Item(0).ToString().Trim
                dtlRow(1) = gridControl.Rows(rowIndex).Item(1).ToString().Trim
                dtlRow(2) = gridControl.Rows(rowIndex).Item(2).ToString().Trim
                dtlRow(3) = gridControl.Rows(rowIndex).Item(3).ToString().Trim
                dtDtl.Rows.Add(dtlRow)
            Next


            dtDtl = dtDtl.DefaultView.ToTable(True, "CheckListId", "SrNo", "Question", "Answer")
            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function
    Public Sub makeGridStructure()


        Dim CheckListId As DataColumn = New DataColumn("CheckListId")
        CheckListId.DataType = System.Type.GetType("System.String")

        Dim SrNo As DataColumn = New DataColumn("SrNo")
        SrNo.DataType = System.Type.GetType("System.String")


        Dim Question As DataColumn = New DataColumn("Question")
        Question.DataType = System.Type.GetType("System.String")

        Dim Answer As DataColumn = New DataColumn("Answer")
        Answer.DataType = System.Type.GetType("System.String")

        dtDtl.Columns.Add(CheckListId)
        dtDtl.Columns.Add(SrNo)
        dtDtl.Columns.Add(Question)
        dtDtl.Columns.Add(Answer)


    End Sub


    Private Sub frmchecklist_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If Keys.Alt AndAlso e.KeyCode = Keys.F4 Then
            ALT_F4 = True
        End If
    End Sub


    Private Sub frmchecklist_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If ALT_F4 Then
            e.Cancel = True
        End If
    End Sub

    Private Function Themechange()
        'Me.Size = New Size(847, 410)
        Me.BackColor = Color.FromArgb(134, 134, 134)
        'lblDocumentType.ForeColor = Color.Black
        'lblDocumentType.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        'lblDocumentType.BorderStyle = BorderStyle.None

        gridChecklist.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        gridChecklist.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        gridChecklist.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        gridChecklist.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        gridChecklist.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridChecklist.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridChecklist.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridChecklist.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        gridChecklist.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)


        btnSaveCheckList.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnSaveCheckList.BackColor = Color.Transparent
        btnSaveCheckList.BackColor = Color.FromArgb(0, 107, 163)
        btnSaveCheckList.ForeColor = Color.FromArgb(255, 255, 255)
        btnSaveCheckList.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnSaveCheckList.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnSaveCheckList.FlatStyle = FlatStyle.Flat
        btnSaveCheckList.FlatAppearance.BorderSize = 0
        btnSaveCheckList.TextAlign = ContentAlignment.MiddleCenter
        btnSaveCheckList.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnSaveCheckList.Size = New Size(70, 30)

    End Function
    
End Class