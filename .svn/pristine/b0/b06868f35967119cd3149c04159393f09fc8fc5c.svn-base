Imports SpectrumBL
Public Class frmNDbConnectionBuild
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim pass As String
        If txtServer.Text.Trim = String.Empty Then
            ShowMessage(getValueByKey("DBCB001"), "DBCB001 - " & "DBCB001 - " & getValueByKey("CLAE04"))
            'ShowMessage("Please Enter Server Name", "Information")
            Exit Sub
        End If
        If txtUser.Text.Trim = String.Empty Then
            ShowMessage(getValueByKey("DBCB002"), "DBCB002 - " & "DBCB002 - " & getValueByKey("CLAE04"))
            'ShowMessage("Please Enter User Id", "Information")
            Exit Sub
        End If
        If txtDataSource.Text.Trim = String.Empty Then
            ShowMessage(getValueByKey("DBCB003"), "DBCB003 - " & "DBCB003 - " & getValueByKey("CLAE04"))
            'ShowMessage("Please Enter Data Source Name", "Information")
            Exit Sub
        End If


        'My.Settings.Server = txtServer.Text.Trim
        'My.Settings.UserId = txtUser.Text.Trim
        'My.Settings.DataSource = txtDataSource.Text.Trim
        'My.Settings.TerminalID = txtTerminal.Text
        pass = txtPass.Text.Trim
        '  pass = Encrypt(pass)
        'My.Settings.Password = pass

        'Change
        CreateSpectrumParamFile("Server", txtServer.Text.Trim)
        CreateSpectrumParamFile("UserId", txtUser.Text.Trim)
        CreateSpectrumParamFile("DataSource", txtDataSource.Text.Trim)
        CreateSpectrumParamFile("Password", pass)

        CreateSpectrumParamFile("COMPORT", "COM1")
        'End of change

        SetConnection()

        'If My.Settings.TerminalID = String.Empty Then
        If ReadSpectrumParamFile("TerminalID") = String.Empty Then
            Dim frmTerminal As New frmNTerminal()
            frmTerminal.ShowDialog()
        End If
        Me.Close()
    End Sub
    Private Sub frmDbConnectionBuild_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            'txtDataSource.Text = My.Settings.DataSource
            'txtServer.Text = My.Settings.Server
            'txtUser.Text = My.Settings.UserId
            'txtPass.Text = Decrypt(My.Settings.Password)
            txtDataSource.Text = ReadSpectrumParamFile("DataSource")
            txtServer.Text = ReadSpectrumParamFile("Server")
            txtUser.Text = ReadSpectrumParamFile("UserId")
            txtPass.Text = ReadSpectrumParamFile("Password")
        Catch ex As Exception
            LogException(ex)
        End Try
        SetCulture(Me, Me.Name)
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub
    Private Sub cmdTestCon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTestCon.Click
        Dim con As SqlClient.SqlConnection
        Try
            con = New SqlClient.SqlConnection("Server= " & txtServer.Text & ";DataBase=" & txtDataSource.Text & ";Uid=" & txtUser.Text & ";pwd=" & txtPass.Text & " ; Max Pool Size=10 ")
            con.Open()
            'ShowMessage(getValueByKey("DBCB004"), "DBCB004")
            ShowMessage(getValueByKey("DBCB004"), "DBCB004 - " & getValueByKey("CLAE04"))
        Catch ex As Exception
            'ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
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


