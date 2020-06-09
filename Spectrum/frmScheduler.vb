Imports System.Configuration
Public Class frmScheduler
    Inherits CtrlRbnBaseForm
    Private Sub frmScheduler_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            
            'Dim LastRun As String = My.Settings.SchedularLastRunTime
            txtUrl.Text = My.Settings.SchedulerUrl
            txtUrl.ReadOnly = True
            If Not String.IsNullOrEmpty(My.Settings.SchedulerUrl) Then
                txtDuration.Text = My.Settings.SchedularInterval
            End If

            'Dim datetim As DateTime = DateTime.Now()
            If Not String.IsNullOrEmpty(My.Settings.SchedularLastRunTime.Trim()) Then
                lblDispLRT.Text = Convert.ToDateTime(My.Settings.SchedularLastRunTime)

            End If
            If Not String.IsNullOrEmpty(My.Settings.SchedularNextRunTime.Trim()) AndAlso Not String.IsNullOrEmpty(My.Settings.SchedularInterval.Trim()) Then
                lblDispNRT.Text = Convert.ToDateTime(My.Settings.SchedularNextRunTime)
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If (IsFormValid()) Then
                My.Settings.SchedulerUrl = txtUrl.Text
                My.Settings.SchedularInterval = txtDuration.Text
                'My.Settings.SchedularLastRunTime = "#9/24/2014 4:05:04 PM#"
                My.Settings.Save()
                ShowMessage("Setting Saved Successfully", getValueByKey("CLAE04"))
                Me.Close()
            End If

            ' System.Diagnostics.Process.Start(txtUrl.Text)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Try
            Dim fd As OpenFileDialog = New OpenFileDialog()
            Dim strFileName As String

            fd.Title = "Open File Dialog"
            fd.Filter = "Batch bat|*.bat"
            fd.FilterIndex = 2
            fd.RestoreDirectory = True

            If fd.ShowDialog() = DialogResult.OK Then
                txtUrl.ReadOnly = False
                txtUrl.Text = fd.FileName
                txtUrl.ReadOnly = True
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Public Function IsFormValid() As Boolean
        Try
            If String.IsNullOrEmpty(txtUrl.Text.Trim()) Then
                ShowMessage("Path is Mandatory", getValueByKey("CLAE04"))
                txtUrl.Focus()
                Return False
            End If
            If String.IsNullOrEmpty(txtDuration.Text.Trim()) Then
                ShowMessage("Time Interval is Mandatory", getValueByKey("CLAE04"))
                txtDuration.Focus()
                Return False
            ElseIf Val(txtDuration.Text) = 0 Then
                ShowMessage("Please Enter Valid Time Interval", getValueByKey("CLAE04"))
                txtDuration.Focus()
                Return False
            ElseIf Val(txtDuration.Text) > 35000 Then
                ShowMessage(" Time Interval must be less than 35000 mins", getValueByKey("CLAE04"))
                txtDuration.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function

    
    Private Sub txtDuration_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDuration.KeyPress
        If e.KeyChar <> ControlChars.Back Then
            e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ".")
        End If
    End Sub
    Private Function Themechange()
        'Me.Size = New Size(847, 410)
        Me.BackColor = Color.FromArgb(134, 134, 134)

        lblLastRunTime.ForeColor = Color.White
        ' CtrlLabel2.BackColor = Color.FromArgb(212, 212, 212)
        lblLastRunTime.BorderStyle = BorderStyle.None
        lblLastRunTime.AutoSize = False
        lblLastRunTime.BackColor = Color.Transparent
        lblLastRunTime.Size = New Size(100, 16)
        lblLastRunTime.SendToBack()
        lblLastRunTime.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblLastRunTime.TextAlign = ContentAlignment.MiddleLeft
        'lblDispLRT.Text = lblDispLRT.Text


        lblUrl.ForeColor = Color.White
        ' CtrlLabel2.BackColor = Color.FromArgb(212, 212, 212)
        lblUrl.BorderStyle = BorderStyle.None
        lblUrl.AutoSize = False
        lblUrl.BackColor = Color.Transparent
        lblUrl.Size = New Size(100, 16)
        lblUrl.SendToBack()
        lblUrl.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblUrl.TextAlign = ContentAlignment.MiddleLeft
        'lblUrl.Text = lblDispLRT.Text

        lblNextRunTime.ForeColor = Color.White
        'CtrlLabel3.BackColor = Color.FromArgb(212, 212, 212)
        lblNextRunTime.AutoSize = False
        lblNextRunTime.BorderStyle = BorderStyle.None
        lblNextRunTime.BackColor = Color.Transparent
        lblNextRunTime.Size = New Size(100, 16)
        lblNextRunTime.SendToBack()
        lblNextRunTime.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblNextRunTime.TextAlign = ContentAlignment.MiddleLeft

        lblInterval.ForeColor = Color.White
        lblInterval.BorderStyle = BorderStyle.None
        'CtrlLabel4.BackColor = Color.FromArgb(212, 212, 212)
        lblInterval.BackColor = Color.Transparent
        lblInterval.AutoSize = False
        lblInterval.Size = New Size(65, 16)
        lblInterval.SendToBack()
        lblInterval.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblInterval.TextAlign = ContentAlignment.MiddleLeft

        btnBrowse.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnBrowse.BackColor = Color.Transparent
        btnBrowse.BackColor = Color.FromArgb(0, 107, 163)
        btnBrowse.ForeColor = Color.FromArgb(255, 255, 255)
        btnBrowse.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnBrowse.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnBrowse.FlatStyle = FlatStyle.Flat
        btnBrowse.FlatAppearance.BorderSize = 0
        btnBrowse.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnBrowse.TextAlign = ContentAlignment.MiddleCenter
        btnBrowse.Size = New Size(80, 30)

        btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnSave.BackColor = Color.Transparent
        btnSave.BackColor = Color.FromArgb(0, 107, 163)
        btnSave.ForeColor = Color.FromArgb(255, 255, 255)
        btnSave.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.FlatAppearance.BorderSize = 0
        btnBrowse.TextAlign = ContentAlignment.MiddleCenter
        btnSave.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnSave.Size = New Size(80, 30)

    End Function
End Class
