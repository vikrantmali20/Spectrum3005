Imports System.IO
Public Class frmNDbLocalConnectionBuild
    Dim pass, passD As String
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        If txtServer.Text.Trim = String.Empty Then
            ShowMessage(getValueByKey("DBCB001"), getValueByKey("CLAE04"))
            'ShowMessage("Please Enter Server Name", "Information")
            Exit Sub
        End If
        If txtUser.Text.Trim = String.Empty Then
            ShowMessage(getValueByKey("DBCB002"), getValueByKey("CLAE04"))
            'ShowMessage("Please Enter User Id", "Information")
            Exit Sub
        End If
        If txtDataSource.Text.Trim = String.Empty Then
            ShowMessage(getValueByKey("DBCB003"), getValueByKey("CLAE04"))
            'ShowMessage("Please Enter Data Source Name", "Information")
            Exit Sub
        End If
        'My.Settings.Server = txtServer.Text.Trim
        'My.Settings.UserId = txtUser.Text.Trim
        'My.Settings.DataSource = txtDataSource.Text.Trim
        'My.Settings.TerminalID = txtTerminal.Text
        pass = txtPass.Text.Trim
        ' pass = Encrypt(pass)

        If File.Exists(Application.StartupPath & "\Connection") Then
            ReadORChangeFile(True)
        Else
            CreateConnectionFile()
        End If
        'My.Settings.LocalConnectionString = "Server= " & txtServer.Text & ";DataBase=" & txtDataSource.Text & ";Uid=" & txtUser.Text & ";pwd=" & txtPass.Text
        CreateSpectrumParamFile("LocalConnectionString", "Server= " & txtServer.Text & ";DataBase=" & txtDataSource.Text & ";Uid=" & txtUser.Text & ";pwd=" & txtPass.Text & ";Connection Timeout=1200;")
        'My.Settings.Password = pass
        'SetLocalConnection()
        'Dim frmTerminal As New frmNTerminal()
        'frmTerminal.ShowDialog()
        Me.Close()
    End Sub
    Private Sub frmDbConnectionBuild_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            If File.Exists(Application.StartupPath & "\Connection") Then
                ReadORChangeFile(False)
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
        SetCulture(Me, Me.Name)
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub
    Private Sub cmdTestCon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTestCon.Click
        Dim con As SqlClient.SqlConnection
        Try

            con = New SqlClient.SqlConnection("Server= " & txtServer.Text & ";DataBase=" & txtDataSource.Text & ";Uid=" & txtUser.Text & ";pwd=" & txtPass.Text)
            con.Open()
            ShowMessage(getValueByKey("DBCB004"), "DBCB004")
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub
    Private Sub CreateConnectionFile()
        Try
            Dim dtTable As New DataTable("LocalConnection")
            dtTable.Columns.Add("SERVER", Type.GetType("System.String"))
            dtTable.Columns.Add("UID", Type.GetType("System.String"))
            dtTable.Columns.Add("PWD", Type.GetType("System.String"))
            dtTable.Columns.Add("DATABASE", Type.GetType("System.String"))
            Dim dr As DataRow = dtTable.NewRow
            dr("Server") = txtServer.Text.Trim
            dr("Uid") = txtUser.Text.Trim
            dr("PWD") = pass
            dr("DATABASE") = txtDataSource.Text.Trim
            dtTable.Rows.Add(dr)
            dtTable.WriteXml("Connection", XmlWriteMode.WriteSchema)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ReadORChangeFile(ByVal Change As Boolean)
        Try
            Dim dtData As New DataTable
            dtData.ReadXml("Connection")
            If Change = True Then
                If dtData.Rows.Count > 0 Then
                    dtData.Rows(0)("Server") = txtServer.Text.Trim
                    dtData.Rows(0)("Uid") = txtUser.Text.Trim
                    dtData.Rows(0)("PWD") = pass
                    dtData.Rows(0)("DATABASE") = txtDataSource.Text.Trim
                    dtData.AcceptChanges()
                    File.Delete(Application.StartupPath & "\Connection")
                    dtData.WriteXml("Connection", XmlWriteMode.WriteSchema)
                End If
            Else
                If dtData.Rows.Count > 0 Then
                    txtServer.Text = dtData.Rows(0)("Server")
                    txtUser.Text = dtData.Rows(0)("Uid")
                    passD = dtData.Rows(0)("PWD")
                    'passD = Decrypt(passD)
                    txtPass.Text = passD
                    txtDataSource.Text = dtData.Rows(0)("DATABASE")
                End If
            End If
        Catch ex As Exception

        End Try


    End Sub
    Private Function Themechange()
        Me.Size = New Size(406, 215)
        Me.BackColor = Color.FromArgb(134, 134, 134)
        CtrlLabel1.ForeColor = Color.Black
        CtrlLabel1.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel1.AutoSize = False
        CtrlLabel1.Size = New Size(178, 21)
        CtrlLabel1.SendToBack()
        CtrlLabel1.TextAlign = ContentAlignment.MiddleLeft
        CtrlLabel1.Text = CtrlLabel1.Text.ToUpper
        CtrlLabel1.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        CtrlLabel2.ForeColor = Color.Black
        CtrlLabel2.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel2.AutoSize = False
        CtrlLabel2.Size = New Size(178, 21)
        CtrlLabel2.SendToBack()
        CtrlLabel2.TextAlign = ContentAlignment.MiddleLeft
        CtrlLabel2.Text = CtrlLabel1.Text.ToUpper
        CtrlLabel2.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        CtrlLabel3.ForeColor = Color.Black
        CtrlLabel3.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel3.AutoSize = False
        CtrlLabel3.Size = New Size(178, 21)
        CtrlLabel3.SendToBack()
        CtrlLabel3.TextAlign = ContentAlignment.MiddleLeft
        CtrlLabel3.Text = CtrlLabel1.Text.ToUpper
        CtrlLabel3.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        CtrlLabel4.ForeColor = Color.Black
        CtrlLabel4.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel4.AutoSize = False
        CtrlLabel4.Size = New Size(178, 21)
        CtrlLabel4.SendToBack()
        CtrlLabel4.TextAlign = ContentAlignment.MiddleLeft
        CtrlLabel4.Text = CtrlLabel1.Text.ToUpper
        CtrlLabel4.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        cmdTestCon.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdTestCon.BackColor = Color.Transparent
        cmdTestCon.BackColor = Color.FromArgb(0, 107, 163)
        cmdTestCon.ForeColor = Color.FromArgb(255, 255, 255)
        cmdTestCon.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdTestCon.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdTestCon.FlatStyle = FlatStyle.Flat
        cmdTestCon.FlatAppearance.BorderSize = 0
        cmdTestCon.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        cmdTestCon.Size = New Size(85, 28)
        cmdTestCon.Location = New Point(160, 134)

        cmdSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdSave.BackColor = Color.Transparent
        cmdSave.BackColor = Color.FromArgb(0, 107, 163)
        cmdSave.ForeColor = Color.FromArgb(255, 255, 255)
        cmdSave.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdSave.FlatStyle = FlatStyle.Flat
        cmdSave.FlatAppearance.BorderSize = 0
        cmdSave.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        cmdSave.Size = New Size(85, 28)

    End Function
End Class
