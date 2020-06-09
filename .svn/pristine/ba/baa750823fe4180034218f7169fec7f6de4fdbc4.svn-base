Imports SpectrumBL
Public Class frmSyncSettings
    Dim objSync As New clsDataSync
    Dim dtSync As DataTable
    Dim OlddtSync As DataTable
    Dim strTerminal As String = ""
    Dim strName As String = String.Empty

    Private Sub frmSyncSettings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            dtSync = objSync.GetSyncSettings()
            OlddtSync = objSync.GetSyncSettings()
            dgSync.DataSource = dtSync
            Dim dt As DataTable = objSync.GetTerminals(clsAdmin.SiteCode)
            If dt.Rows.Count > 0 Then
                Dim str As String = ""
                For Each row As DataRow In dt.Rows
                    str = str & row("TerminalId").ToString() & "|"
                Next
                strTerminal = str
            End If
            gridset()
        Catch ex As Exception

        End Try
        SetCulture(Me, Me.Name)
    End Sub
    Private Sub gridset()
        dgSync.Cols("TerminalId").Caption = getValueByKey("frmsyncsettings.dgsync.terminalid")
        dgSync.Cols("Name").Caption = getValueByKey("frmsyncsettings.dgsync.name")
        dgSync.Cols("Status").Caption = getValueByKey("frmsyncsettings.dgsync.status")
        For Each col As C1.Win.C1FlexGrid.Column In dgSync.Cols
            If col.Name.ToUpper() <> "TERMINALID" And col.Name.ToUpper() <> "NAME" And col.Name.ToUpper() <> "STATUS" Then
                col.Visible = False
            End If
        Next
        dgSync.Cols("TerminalID").ComboList = strTerminal
        'dgSync.Cols("Name").ComboList = getValueByKey("frmsyncsettings.dgsync.name.cboval")
        'dgSync.Cols("Name").ComboList = String.Empty
        'If dtSync.Rows.Count > 0 Then
        '    Dim strString As String = getValueByKey("frmsyncsettings.dgsync.name.cashmemo")
        '    Dim dvView As New DataView(dtSync, "(Groups = 'BL' OR NAME= '" & getValueByKey("frmsyncsettings.dgsync.name.birthlist") & "')  AND isnull(Status,0)=1", "", DataViewRowState.CurrentRows)
        '    If dvView.Count = 0 Then
        '        strString = strString & "|" & getValueByKey("frmsyncsettings.dgsync.name.birthlist")
        '    End If
        '    dvView.RowFilter = "(Groups = 'SO' OR NAME='" & getValueByKey("frmsyncsettings.dgsync.name.salesorder") & "') AND isnull(Status,0)=1"
        '    If dvView.Count = 0 Then
        '        strString = strString & "|" & getValueByKey("frmsyncsettings.dgsync.name.salesorder")
        '    End If

        '    dgSync.Cols("Name").ComboList = strString
        'End If
    End Sub
    Private Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Try
            If (dtSync.Rows.Count = 0) Or (dtSync.Rows.Count > 0 AndAlso (dtSync.Rows(dtSync.Rows.Count - 1)("TerminalID") <> String.Empty And dtSync.Rows(dtSync.Rows.Count - 1)("NAME") <> String.Empty)) Then
                Dim dr As DataRow = dtSync.NewRow
                dr("Status") = 1
                dtSync.Rows.Add(dr)
                gridset()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            Dim dtTempSync As DataTable = dtSync

            For Each row As DataRow In dtSync.Rows
                If row("TerminalId") Is DBNull.Value Or row("Name") Is DBNull.Value Then
                    ShowMessage(getValueByKey("SYNSET04"), "SYNSET04 - " & getValueByKey("CLAE04"))
                    Exit Sub
                ElseIf row("TerminalId") = String.Empty Or row("Name") = String.Empty Then
                    ShowMessage(getValueByKey("SYNSET04"), "SYNSET04 - " & getValueByKey("CLAE04"))
                    Exit Sub
                Else
                    Dim dvView As New DataView(dtSync, "(Groups = 'BL' OR NAME= '" & getValueByKey("frmsyncsettings.dgsync.name.birthlist") & "')  AND TerminalId = '" & row("TerminalId") & "' AND isnull(Status,0)=1", "", DataViewRowState.CurrentRows)
                    If dvView.Count > 1 Then
                        ShowMessage(getValueByKey("SYNSET03") & row("TerminalId") & " - " & row("Name"), "SYNSET03 - " & getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                    dvView.RowFilter = "(Groups = 'SO' OR NAME='" & getValueByKey("frmsyncsettings.dgsync.name.salesorder") & "') AND TerminalId = '" & row("TerminalId") & "' AND isnull(Status,0)=1"
                    If dvView.Count > 1 Then
                        ShowMessage(getValueByKey("SYNSET03") & row("TerminalId") & " - " & row("Name"), "SYNSET03 - " & getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                    dvView.RowFilter = "(Groups = 'CM' OR NAME='" & getValueByKey("frmsyncsettings.dgsync.name.cashmemo") & "') AND TerminalId = '" & row("TerminalId") & "' AND isnull(Status,0)=1"
                    If dvView.Count > 1 Then
                        ShowMessage(getValueByKey("SYNSET03") & row("TerminalId") & " - " & row("Name"), "SYNSET03 - " & getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                End If
                If row.RowState = DataRowState.Added Then
                    If row("Name") = getValueByKey("frmsyncsettings.dgsync.name.cashmemo") Then
                        row("Groups") = "CM"
                    ElseIf row("NAME") = getValueByKey("frmsyncsettings.dgsync.name.birthlist") Then
                        row("Groups") = "BL"
                    ElseIf row("Name") = getValueByKey("frmsyncsettings.dgsync.name.salesorder") Then
                        row("Groups") = "SO"
                    End If
                    row("CreatedOn") = clsAdmin.CurrentDate
                    row("CreatedBy") = clsAdmin.UserName
                    row("CreatedAt") = clsAdmin.SiteCode
                    row("UpdatedOn") = clsAdmin.CurrentDate
                    row("UpdatedBy") = clsAdmin.UserName
                    row("UpdatedAt") = clsAdmin.SiteCode
                End If
            Next
            
            If objSync.SaveSettings(dtSync) = True Then
                ShowMessage(getValueByKey("SYNSET01"), "SYNSET01 - " & getValueByKey("CLAE04"))
            Else
                ShowMessage(getValueByKey("SYNSET02"), "SYNSET02 - " & getValueByKey("CLAE05"))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgSync_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgSync.AfterEdit
        If dgSync.Cols(e.Col).Name = "TerminalID" Then
            If Not dgSync.Rows(e.Row)("TerminalId") Is DBNull.Value AndAlso Not dgSync.Rows(e.Row)("TerminalId") = String.Empty Then
                gridsetName(dgSync.Rows(e.Row)("TerminalId"))
            End If
        End If
        
    End Sub

    Private Sub gridsetName(ByVal strTerminalId As String)

        dgSync.Cols("Name").Caption = getValueByKey("frmsyncsettings.dgsync.name")
        dgSync.Cols("Name").ComboList = getValueByKey("frmsyncsettings.dgsync.name.cboval")
        If dtSync.Rows.Count > 0 Then
            'Dim strString As String = getValueByKey("frmsyncsettings.dgsync.name.cashmemo")
            Dim strString As String = String.Empty
            Dim dvView As New DataView(dtSync, "(Groups = 'BL' OR NAME= '" & getValueByKey("frmsyncsettings.dgsync.name.birthlist") & "')  AND TerminalId = '" & strTerminalId & "' AND  isnull(Status,0)=1", "", DataViewRowState.CurrentRows)
            If dvView.Count = 0 Then
                strString = strString & "|" & getValueByKey("frmsyncsettings.dgsync.name.birthlist")
            End If
            dvView.RowFilter = "(Groups = 'SO' OR NAME='" & getValueByKey("frmsyncsettings.dgsync.name.salesorder") & "') AND TerminalId = '" & strTerminalId & "' AND isnull(Status,0)=1"
            If dvView.Count = 0 Then
                strString = strString & "|" & getValueByKey("frmsyncsettings.dgsync.name.salesorder")
            End If
            dvView.RowFilter = "(Groups = 'CM' OR NAME='" & getValueByKey("frmsyncsettings.dgsync.name.cashmemo") & "') AND TerminalId = '" & strTerminalId & "' AND isnull(Status,0)=1"
            If dvView.Count = 0 Then
                strString = strString & "|" & getValueByKey("frmsyncsettings.dgsync.name.cashmemo")
            End If

            dgSync.Cols("Name").ComboList = strString
        End If
    End Sub

    
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
    Private Function Themechange()
        'Me.Size = New Size(847, 410)
        Me.BackColor = Color.FromArgb(134, 134, 134)

        dgSync.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        dgSync.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgSync.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        'dgReprint.Rows.MinSize = 25
        dgSync.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        dgSync.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgSync.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgSync.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        'dgReprint.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgSync.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)

        cmdSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdSave.BackColor = Color.Transparent
        cmdSave.BackColor = Color.FromArgb(0, 107, 163)
        cmdSave.ForeColor = Color.FromArgb(255, 255, 255)
        cmdSave.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdSave.FlatStyle = FlatStyle.Flat
        cmdSave.FlatAppearance.BorderSize = 0
        cmdSave.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdSave.Size = New Size(85, 30)

        cmdAdd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdAdd.BackColor = Color.Transparent
        cmdAdd.BackColor = Color.FromArgb(0, 107, 163)
        cmdAdd.ForeColor = Color.FromArgb(255, 255, 255)
        cmdAdd.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdAdd.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdAdd.FlatStyle = FlatStyle.Flat
        cmdAdd.FlatAppearance.BorderSize = 0
        cmdAdd.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdAdd.Size = New Size(85, 30)

    End Function
End Class
