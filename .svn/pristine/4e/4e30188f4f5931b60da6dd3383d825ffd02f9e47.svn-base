Imports SpectrumBL
Imports System.IO
Imports System.Text
Imports System.Threading
Public Class frmDataArcive
    Dim FormNormalHeight, SpliterDistance As Integer
    Dim objcls As New clsDataArcive
    'Dim strStatusMessages As New StringBuilder
    Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click
        Try
            If ValidateInput() Then
                SplitContainer1.Panel1.Enabled = False
                'Dim thread As New Thread(AddressOf StartProces)
                'thread.Start()

                'StartProces()
                BackgroundWorker1.WorkerReportsProgress = True
                BackgroundWorker1.RunWorkerAsync()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub StartProces(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        Dim worker As System.ComponentModel.BackgroundWorker
        Try

            worker = sender

            'worker.ReportProgress(0, "Process start .... ")
            worker.ReportProgress(0, getValueByKey("DA001"))
            'StatusMessage("Database copyping start  .... ")
            'worker.ReportProgress(2, "Database copyping start  .... ")
            worker.ReportProgress(2, getValueByKey("DA002"))

            If Not (objcls.CopyDataBase(ReadSpectrumParamFile("DataSource"), txtDBName.Text.Trim, "\\" & ReadSpectrumParamFile("Server") & "\" & txtServerPath.Text.Trim, False)) Then
                'StatusMessage("Database coping error.", True)
                'worker.ReportProgress(0, "Database coping error.")
                worker.ReportProgress(0, getValueByKey("DA003"))

                Exit Sub
            End If
            'StatusMessage("New database created .... ")
            'worker.ReportProgress(20, "New database created ....")
            worker.ReportProgress(20, getValueByKey("DA004"))
            'ProgressBar1.Value = 20
            'worker.ReportProgress(20, "Deleting   transaction tables data in  Archieval database  start  .... ")
            worker.ReportProgress(20, getValueByKey("DA005"))
            'StatusMessage("Deleting   transaction tables data in  Archieval database  start  .... ")
            If Not (objcls.DeleteExtraFromArciveData(dtpFromDate.Value, dtpToDate.Value, clsAdmin.SiteCode)) Then
                'worker.ReportProgress(25, "Deleting   transaction tables data in  Archieval database error.")
                worker.ReportProgress(25, getValueByKey("DA006"))
                'StatusMessage("Deleting   transaction tables data in  Archieval database error.", True)
                Exit Sub
            End If
            'StatusMessage("Deleting transaction tables data  complete in Archieval start  .... ")
            'worker.ReportProgress(25, "Deleting transaction tables data  complete in Archieval start  ....")
            worker.ReportProgress(25, getValueByKey("DA007"))
            'ProgressBar1.Value = 30
            'StatusMessage("Deleting master table data in Archieval database start  .... ")
            'worker.ReportProgress(30, "Deleting master table data in Archieval database start  .... ")
            worker.ReportProgress(30, getValueByKey("DA008"))
            If Not objcls.DeleteMaster(dtpFromDate.Value, dtpToDate.Value) Then
                'worker.ReportProgress(30, "Deleting master table data in Archieval database error. ")
                worker.ReportProgress(30, getValueByKey("DA009"))
                'StatusMessage("Deleting master table data in Archieval database error.", True)
                Exit Sub
            End If
            'worker.ReportProgress(40, "Deleting master table data in Archieval database complete")
            worker.ReportProgress(40, getValueByKey("DA010"))
            'StatusMessage("Deleting master table data in Archieval database complete")

            'StatusMessage("Reindexing start on Archieval database.... ")
            'worker.ReportProgress(50, "Reindexing start on Archieval database.... ")
            worker.ReportProgress(50, getValueByKey("DA011"))
            If Not objcls.ReindexingArciveTable(True) Then
                'StatusMessage("Deleting master table data in Archieval database error.", True)
                'worker.ReportProgress(50, "Deleting master table data in Archieval database error. ")
                worker.ReportProgress(50, getValueByKey("DA012"))
                Exit Sub
            End If
            'ProgressBar1.Value = 60
            'worker.ReportProgress(50, "Reindexing complete on Archieval  database....")
            worker.ReportProgress(50, getValueByKey("DA013"))
            'StatusMessage("Reindexing complete on Archieval  database.... ")
            'StatusMessage("Deleting  transaction tables data on Main database  start.... ")
            'worker.ReportProgress(60, "Deleting  transaction tables data on Main database  start.... ")
            worker.ReportProgress(60, getValueByKey("DA014"))
            If Not objcls.deletefromOriginal(dtpFromDate.Value, dtpToDate.Value, clsAdmin.SiteCode) Then
                'StatusMessage("Deleting master table data in original database error.", True)
                'worker.ReportProgress(60, "Deleting master table data in original database error. ")
                worker.ReportProgress(60, getValueByKey("DA015"))
                Exit Sub
            End If
            'StatusMessage("Deleting   transaction tables data on Main database  Completed.... ")
            'ProgressBar1.Value = 80
            'worker.ReportProgress(70, "Deleting   transaction tables data on Main database  Completed....  ")
            worker.ReportProgress(70, getValueByKey("DA016"))
            objcls.DataArchiveLog(clsAdmin.SiteCode, txtDBName.Text, dtpFromDate.Value, dtpToDate.Value, dtpFromDate.Value, clsAdmin.UserCode)
            'StatusMessage("Reindexing start on main database.... ")
            'worker.ReportProgress(70, "Reindexing start on main database....")
            worker.ReportProgress(70, getValueByKey("DA017"))
            If Not objcls.ReindexingArciveTable(False) Then
                'StatusMessage("Reindexing coping error.", True)
                'worker.ReportProgress(80, "Reindexing coping error.")
                worker.ReportProgress(80, getValueByKey("DA018"))
                Exit Sub
            End If
            'worker.ReportProgress(100, "Reindexing complete on Original database....")
            worker.ReportProgress(100, getValueByKey("DA019"))
            'StatusMessage("Reindexing complete on Original database.... ")
            'ProgressBar1.Value = 100
            If Not (objcls.DataArchiveLog(clsAdmin.SiteCode, txtDBName.Text, dtpFromDate.Value, dtpToDate.Value, dtpFromDate.Value, clsAdmin.UserCode)) Then
                'StatusMessage("Log  database done Successfully.... ")
                'worker.ReportProgress(100, "Log  database done Successfully.... ")
                worker.ReportProgress(100, getValueByKey("DA020"))
            End If

            'worker.ReportProgress(100, "Archieval database done Successfully....  ")
            worker.ReportProgress(100, getValueByKey("DA021"))
            'StatusMessage("Archieval database done Successfully.... ", True)
        Catch ex As Exception
            If Not worker Is Nothing Then
                'worker.ReportProgress(0, "Error occured in Data Archive ")
                worker.ReportProgress(0, getValueByKey("DA022") & " - " & ex.Message)
            End If 
            LogException(ex)
        End Try
    End Sub
    Private Function ValidateInput() As Boolean
        Try
            Dim CurrentDate As Date = clsAdmin.CurrentDate
            'strStatusMessages.Length = 0
            If Not dtpToDate.Value Is DBNull.Value Then
                If dtpToDate.Value >= CurrentDate.Date Then
                    'MessageBox.Show("To date may not be current date ")
                    ShowMessage(getValueByKey("DA023"), "DA023 - " & getValueByKey("CLAE05"))
                    Return False
                End If
            Else
                'MessageBox.Show("To date may not be current date ")
                ShowMessage(getValueByKey("DA023"), "DA023 - " & getValueByKey("CLAE05"))
                Return False
            End If

            If dtpToDate.Value <= dtpFromDate.Value Then
                'MessageBox.Show("To date is geater than from date ")
                ShowMessage(getValueByKey("DA024"), "DA024 - " & getValueByKey("CLAE05"))
                Return False
            End If
            If txtDBName.Text = String.Empty Then
                'MessageBox.Show("Please provide database name for Data Archive. ")
                ShowMessage(getValueByKey("DA025"), "DA025 - " & getValueByKey("CLAE05"))
                Return False
            End If

            If txtServerPath.Text = String.Empty Then
                'MessageBox.Show("Please provide server path  for Data Archive Database . ")
                ShowMessage(getValueByKey("DA026"), "DA026 - " & getValueByKey("CLAE05"))
                Return False
            End If
            Dim strPath As String = txtServerPath.Text.Trim()
            If strPath.StartsWith("\\") Then
                strPath = strPath.Replace(":", "$")
                'If Not Directory.Exists("\\" & My.Settings.Server & "\" & strPath) Then
                If Not Directory.Exists("\\" & ReadSpectrumParamFile("Server") & "\" & strPath) Then
                    'MessageBox.Show("Incorrect server path  for Data Archive Database  ")
                    ShowMessage(getValueByKey("DA027"), "DA027 - " & getValueByKey("CLAE05"))
                    Return False
                End If
            Else
                If Not (Directory.Exists("\\" & ReadSpectrumParamFile("Server") & "\" & strPath)) Then
                    'MessageBox.Show("Incorrect server path  for Data Archive Database  ")
                    ShowMessage(getValueByKey("DA027"), "DA027 - " & getValueByKey("CLAE05"))
                    Return False
                End If
            End If
            'SetformLayout(2)
            'SplitContainer1.Panel1Collapsed = True
            'rtxtStatus.Text = StatusMessage("Validation success .... ")
            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Function StatusMessage(ByVal strMessage As String, Optional ByVal isFailed As Boolean = False) As String
        Try
            'strStatusMessages.Append(strMessage + vbCrLf)
            'rtxtStatus.Text = strStatusMessages.ToString()
            If isFailed Then
                SplitContainer1.Panel1Collapsed = False
                SplitContainer1.Panel2Collapsed = False
            End If
            'ToolStripStatusLabel1.Text = strStatusMessages.ToString()
            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function
    'Private Sub SetformLayout(ByVal Level As Integer)
    '    If Level = 1 Then
    '        Dim i As Integer
    '        i = SplitContainer1.Panel2.Height
    '        i = FormNormalHeight - i
    '        Me.Height = i
    '        SplitContainer1.Panel2Collapsed = True
    '    ElseIf Level = 2 Then
    '        Dim i As Integer
    '        i = SplitContainer1.Panel1.Height
    '        i = FormNormalHeight - i
    '        Me.Height = i
    '        SplitContainer1.Panel1Collapsed = True
    '        'SplitContainer1.SplitterDistance = SpliterDistance
    '    End If
    'End Sub

    Private Sub frmDataArcive_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            'FormNormalHeight = Me.Height
            'SpliterDistance = SplitContainer1.SplitterDistance
            'SetformLayout(1)
            'SplitContainer1.Panel2Collapsed = True
            rtxtStatus.ReadOnly = True
            Dim objDate As Object = objcls.getLastDataArchiveDate(clsAdmin.SiteCode)
            If Not objDate Is Nothing Then
                dtpFromDate.Value = CDate(objDate)
                dtpFromDate.Enabled = False
            End If
            SetCulture(Me, Me.Name)
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        StartProces(sender, e)

    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        rtxtStatus.Text = rtxtStatus.Text & vbCrLf & e.UserState.ToString()
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        'rtxtStatus.Text = rtxtStatus.Text & vbCrLf & "Data Archieval Work has been completed"
        ProgressBar1.Value = 100
        ProgressBar1.Value = 0
        cmdStart.Enabled = True
    End Sub

    Public Sub New()
        Try
            InitializeComponent()
        Catch ex As Exception

        End Try
        ' This call is required by the Windows Form Designer.


        ' Add any initialization after the InitializeComponent() call.
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Private Sub frmDataArcive_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "data-archive.htm")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function Themechange()
        'Me.Size = New Size(847, 410)
        Me.BackColor = Color.FromArgb(134, 134, 134)

        ' CtrlLabel2.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel2.BorderStyle = BorderStyle.None
        CtrlLabel2.AutoSize = False
        CtrlLabel2.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel2.Size = New Size(191, 19)
        CtrlLabel2.SendToBack()
        CtrlLabel2.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CtrlLabel2.TextAlign = ContentAlignment.MiddleLeft


        'CtrlLabel3.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel3.AutoSize = False
        CtrlLabel3.BorderStyle = BorderStyle.None
        CtrlLabel3.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel3.Size = New Size(191, 26)
        CtrlLabel3.SendToBack()
        CtrlLabel3.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CtrlLabel3.TextAlign = ContentAlignment.MiddleLeft


        CtrlLabel4.BorderStyle = BorderStyle.None
        'CtrlLabel4.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel4.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel4.AutoSize = False
        CtrlLabel4.Size = New Size(191, 26)
        CtrlLabel4.SendToBack()
        CtrlLabel4.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CtrlLabel4.TextAlign = ContentAlignment.MiddleLeft


        CtrlLabel1.BorderStyle = BorderStyle.None
        'CtrlLabel1.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel1.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel1.AutoSize = False
        CtrlLabel1.Size = New Size(144, 19)
        CtrlLabel1.SendToBack()
        CtrlLabel1.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CtrlLabel1.TextAlign = ContentAlignment.MiddleLeft

        cmdStart.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdStart.BackColor = Color.Transparent
        cmdStart.BackColor = Color.FromArgb(0, 107, 163)
        cmdStart.ForeColor = Color.FromArgb(255, 255, 255)
        cmdStart.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdStart.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdStart.FlatStyle = FlatStyle.Flat
        cmdStart.FlatAppearance.BorderSize = 0
        cmdStart.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        cmdStart.Size = New Size(80, 30)


    End Function
End Class
