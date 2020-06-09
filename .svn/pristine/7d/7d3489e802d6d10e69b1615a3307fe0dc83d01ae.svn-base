Imports System.Data.SqlClient
Imports SpectrumBL
Public Class frmNTerminal
    Dim dt As DataTable
    Dim obj As New clsCommon
    Private Sub frmTerminal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            'CtrlLabel1.Text = My.Settings.TerminalID
            CtrlLabel1.Text = ReadSpectrumParamFile("TerminalID")
            Dim Site As String = ""
            If String.IsNullOrEmpty(clsAdmin.SiteCode) Then
                Dim dt As DataTable = obj.GetDefaultSetting("0000", "")
                If Not dt Is Nothing Then
                    For Each dr As DataRow In dt.Select("fldLabel='LocalSiteCode'", "", DataViewRowState.CurrentRows)
                        Site = dr("fldvalue").ToString()
                    Next
                End If
            Else
                Site = clsAdmin.SiteCode
            End If
            dt = obj.GetTerminals(Site, True)
            dt.TableName = "Terminal"
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                cmbTerminal.DataSource = dt
                cmbTerminal.DisplayMember = "TERMINALNAME"
                cmbTerminal.ValueMember = "TERMINALID"
                cmbTerminal.ExtendRightColumn = True
                For Each r As C1.Win.C1List.Split In cmbTerminal.Splits
                    Dim i As Integer
                    For i = 0 To r.DisplayColumns.Count - 1
                        If r.DisplayColumns(i).Name <> cmbTerminal.DisplayMember Then
                            r.DisplayColumns(i).Visible = False
                        End If
                    Next
                Next
            ElseIf Not dt Is Nothing AndAlso dt.Rows.Count = 0 Then
                ShowMessage(getValueByKey("T01") & Site, "T01 - " & getValueByKey("CLAE04"))
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))

            LogException(ex)
        End Try
        SetCulture(Me, Me.Name)
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub
    Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            'If My.Settings.TerminalID <> String.Empty AndAlso My.Settings.TerminalID <> cmbTerminal.SelectedValue Then
            If ReadSpectrumParamFile("TerminalID") <> String.Empty AndAlso ReadSpectrumParamFile("TerminalID") <> cmbTerminal.SelectedValue Then
                ShowMessage(getValueByKey("T02"), "T02 - " & getValueByKey("CLAE04"))
                'Exit Sub
            End If

            If obj.UpdateTerminal(cmbTerminal.SelectedValue, My.Computer.Name, clsAdmin.SiteCode, clsAdmin.UserName, clsAdmin.CurrentDate) Then
                Dim str As String = cmbTerminal.SelectedValue
                'My.Settings.TerminalID = str
                CreateSpectrumParamFile("TerminalID", str)
                clsAdmin.TerminalID = str
                Me.Close()
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub frmNPrintingSettings_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "set-the-terminal.htm")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function Themechange()
        'Me.Size = New Size(847, 410)
        Me.BackColor = Color.FromArgb(134, 134, 134)
        CtrlLabel1.ForeColor = Color.White
        CtrlLabel1.BorderStyle = BorderStyle.None
        CtrlLabel1.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        CtrlLabel1.BackColor = Color.Transparent

        CtrlLabel2.ForeColor = Color.Black
        CtrlLabel2.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel2.AutoSize = False
        CtrlLabel2.Size = New Size(178, 22)
        CtrlLabel2.SendToBack()
        CtrlLabel2.TextAlign = ContentAlignment.MiddleLeft
        CtrlLabel2.BorderStyle = BorderStyle.None
        ' lblSlapperName.Text = lblSlapperName.Text.ToUpper

        cmdSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdSave.BackColor = Color.Transparent
        cmdSave.BackColor = Color.FromArgb(0, 107, 163)
        cmdSave.ForeColor = Color.FromArgb(255, 255, 255)
        cmdSave.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdSave.FlatStyle = FlatStyle.Flat
        cmdSave.FlatAppearance.BorderSize = 0
        cmdSave.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdSave.TextAlign = ContentAlignment.MiddleCenter
        cmdSave.Size = New Size(85, 35)



    End Function
End Class
