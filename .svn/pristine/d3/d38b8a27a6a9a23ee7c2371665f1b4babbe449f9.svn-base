Imports SpectrumBL

Public Class frmChangeUserPassword

    Dim clscom As New clsCommon
    Dim clsEnc As New clsEncrypter
    Dim screenWidth As Integer = 0
    Dim screenHeight As Integer = 0

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If String.IsNullOrEmpty(txtUserName.Text) Then
                ShowMessage("User Id Field Can't be Empty.", "Information")
                Exit Sub
            End If
            If String.IsNullOrEmpty(TxtOldPass.Text) Then
                ShowMessage("Old Password Field Can't be Empty.", "Information")
                Exit Sub
            End If
            If String.IsNullOrEmpty(txtNewPass.Text) Then
                ShowMessage("New Password Field Can't be Empty.", "Information")
                Exit Sub
            End If
            If String.IsNullOrEmpty(txtConfirmpass.Text) Then
                ShowMessage("Confirm Password Field Can't be Empty.", "Information")
                Exit Sub
            End If
            'If TxtOldPass.Text.Length < 6 Then
            '    ShowMessage("Old Password Cannot be Less than 6 Character.", "Information")
            '    TxtOldPass.Text = ""
            '    Exit Sub
            'End If
            If txtNewPass.Text.Length < 6 Then
                ShowMessage("New Password Cannot be Less than 6 Character.", "Information")
                txtNewPass.Text = ""
            End If
            If txtConfirmpass.Text.Length < 6 Then
                ShowMessage("Confirm Password Cannot be Less than 6 Character.", "Information")
                txtConfirmpass.Text = ""
                Exit Sub
            End If
            If (txtNewPass.Text.Trim).ToString <> (txtConfirmpass.Text.Trim).ToString Then
                ShowMessage("Confirm Password Field is not matching.", "Information")
                txtNewPass.Text = ""
                txtConfirmpass.Text = ""
                Exit Sub
            End If
            Dim dt As DataTable = clscom.CheckAuthUserDetails(txtUserName.Text)
            If clsEnc.authenticatePassword(TxtOldPass.Text.Trim, dt.Rows(0)("Password")) = False Then
                ShowMessage("Old Password Field is Wrong.", "Information")
                TxtOldPass.Text = ""
                txtNewPass.Text = ""
                txtConfirmpass.Text = ""
                Exit Sub
            End If
            Dim confirmpass As String = clsEnc.getEncryptedPassword(txtConfirmpass.Text)
            If clscom.UpdateAuthUserDetails(txtUserName.Text, confirmpass, clsAdmin.CurrentDate, clsAdmin.SiteCode, clsAdmin.UserCode, clsAdmin.CurrentDate) Then
                ShowMessage("Password Saved Saccessfully.", "Information")
                Clear()
                Exit Sub
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub Clear()
        txtUserName.Text = ""
        txtEmail.Text = ""
        txtCountry.Text = ""
        TxtDesignation.Text = ""
        txtSiteCode.Text = ""
        TxtUserActive.Text = ""
        TxtOldPass.Text = ""
        txtNewPass.Text = ""
        txtConfirmpass.Text = ""
    End Sub

    Private Sub frmChangeUserPassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            screenWidth = Screen.PrimaryScreen.Bounds.Width
            screenHeight = Screen.PrimaryScreen.Bounds.Height
            txtUserName.Focus()
            txtEmail.Enabled = False
            txtCountry.Enabled = False
            TxtDesignation.Enabled = False
            txtSiteCode.Enabled = False
            TxtUserActive.Enabled = False
            Dim dtBind As DataTable = clscom.GetAllAuthUser()
            If Not dtBind Is Nothing AndAlso dtBind.Rows.Count > 0 Then
                Call SetWildSearchTextBox(dtBind, txtUserName, key:="UserID", Value:="UserName", searchData:="SearchBy")
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                ThemeChange()
            End If
            If screenWidth > 1024 Then
                txtUserName.IsSetLocation = True
                txtUserName.ListBoxXCoordinate = 489
                txtUserName.ListBoxYCoordinate = 222
            Else
                txtUserName.IsSetLocation = True
                txtUserName.ListBoxXCoordinate = 318
                txtUserName.ListBoxYCoordinate = 224
            End If

            AddHandler txtUserName.KeyUp, AddressOf txtUserName_KeyUp
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim userid As String = txtUserName.Text.Trim
        If Not String.IsNullOrEmpty(userid) Then
            If MessageBox.Show("You will loose unsaved data, Do you wish to continue?", "Information", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub txtUserName_KeyDown(sender As Object, e As KeyEventArgs) 'Handles txtUserName.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim userid As String = txtUserName.Text.Trim
                Dim userdet As DataTable = clscom.GetAuthUserdetail(userid)
                If userdet.Rows.Count > 0 AndAlso Not userdet Is Nothing Then
                    txtUserName.Text = userdet.Rows(0)("UserID")
                    txtEmail.Text = userdet.Rows(0)("EmailId")
                    txtCountry.Text = userdet.Rows(0)("CountryCode")
                    TxtDesignation.Text = userdet.Rows(0)("Designation")
                    txtSiteCode.Text = userdet.Rows(0)("SiteCode")
                    TxtUserActive.Text = userdet.Rows(0)("Active")
                    txtEmail.Enabled = False
                    txtCountry.Enabled = False
                    TxtDesignation.Enabled = False
                    txtSiteCode.Enabled = False
                    TxtUserActive.Enabled = False
                Else
                    ShowMessage("User does not Exist.", "Information")
                    Clear()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub ThemeChange()
        Me.BackColor = Color.FromArgb(134, 134, 134)

        'Label
        'Label5.BackColor = Color.FromArgb(212, 212, 212)

        'Cancel button
        Button1.VisualStyle = C1.Win.C1Input.VisualStyle.System
        Button1.BackColor = Color.Transparent
        Button1.BackColor = Color.FromArgb(0, 107, 163)
        Button1.ForeColor = Color.FromArgb(255, 255, 255)
        Button1.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Button1.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Button1.FlatAppearance.BorderSize = 0
        Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat

        'Change button
        Button2.VisualStyle = C1.Win.C1Input.VisualStyle.System
        Button2.BackColor = Color.Transparent
        Button2.BackColor = Color.FromArgb(0, 107, 163)
        Button2.ForeColor = Color.FromArgb(255, 255, 255)
        Button2.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Button2.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Button2.FlatAppearance.BorderSize = 0
        Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat

    End Sub

    Private Sub frmChangeUserPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Dim userid As String = txtUserName.Text.Trim
            If Not String.IsNullOrEmpty(userid) Then
                If MessageBox.Show("You will loose unsaved data, Do you wish to continue?", "Information", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
                    Me.Close()
                End If
            Else
                Me.Close()
            End If
        End If
    End Sub

    Private Sub txtUserName_KeyUp(sender As Object, e As KeyEventArgs) ' Handles txtUserName.KeyUp
        If Not String.IsNullOrEmpty(txtUserName.Text.Trim) Then
            If txtUserName.Text.Length > 44 Then
                ShowMessage("User Name Character is too Long.", "Information")
                txtUserName.Text = String.Empty
                Exit Sub
            End If
        Else
            Clear()
        End If
    End Sub

    Private Sub TxtOldPass_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtOldPass.KeyUp
        If Not String.IsNullOrEmpty(txtUserName.Text.Trim) Then
            If TxtOldPass.Text.Length > 149 Then
                ShowMessage("Old Password Character is too Long.", "Information")
                Exit Sub
            End If
        End If
    End Sub

    Private Sub txtNewPass_KeyUp(sender As Object, e As KeyEventArgs) Handles txtNewPass.KeyUp
        If Not String.IsNullOrEmpty(txtUserName.Text.Trim) Then
            If txtNewPass.Text.Length > 149 Then
                ShowMessage("New Password Character is too Long.", "Information")
                Exit Sub
            End If
        End If
    End Sub

    Private Sub txtConfirmpass_KeyUp(sender As Object, e As KeyEventArgs) Handles txtConfirmpass.KeyUp
        If Not String.IsNullOrEmpty(txtUserName.Text.Trim) Then
            If txtConfirmpass.Text.Length > 149 Then
                ShowMessage("Confirm Password Character is too Long.", "Information")
                Exit Sub
            End If
        End If
    End Sub

    Private Sub TxtOldPass_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtOldPass.KeyPress
        If String.IsNullOrEmpty(txtUserName.Text.Trim) Then
            e.Handled = True
            'ShowMessage("Please Enter The UseName First.", "Information")
            'Exit Sub
        End If
    End Sub

    Private Sub txtNewPass_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewPass.KeyPress
        If String.IsNullOrEmpty(txtUserName.Text.Trim) Then
            e.Handled = True
            'ShowMessage("Please Enter The UseName First.", "Information")
            'Exit Sub
        End If
    End Sub

    Private Sub txtConfirmpass_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtConfirmpass.KeyPress
        If String.IsNullOrEmpty(txtUserName.Text.Trim) Then
            e.Handled = True
            'ShowMessage("Please Enter The UseName First.", "Information")
            'Exit Sub
        End If
    End Sub
    Private Sub txtUserName_TextChanged(sender As Object, e As EventArgs) Handles txtUserName.TextChanged
        If Not String.IsNullOrEmpty(txtUserName.Text) AndAlso txtUserName.IsItemSelected Then
            txtUserName.IsItemSelected = False
            Dim eKeyDown = New System.Windows.Forms.KeyEventArgs(Keys.Enter)
            Call txtUserName_Leave(sender, eKeyDown)
        End If
    End Sub
    Private Sub txtUserName_Leave(sender As System.Object, e As System.EventArgs)
        Try

            Cursor.Current = Cursors.WaitCursor

            Dim userid As String = txtUserName.Text.ToString().Split(" ")(0)
            Dim userdet As DataTable = clscom.GetAuthUserdetail(userid)
            If userdet.Rows.Count > 0 AndAlso Not userdet Is Nothing Then
                txtUserName.Text = userdet.Rows(0)("UserID")
                txtEmail.Text = userdet.Rows(0)("EmailId")
                txtCountry.Text = userdet.Rows(0)("CountryCode")
                TxtDesignation.Text = userdet.Rows(0)("Designation")
                txtSiteCode.Text = userdet.Rows(0)("SiteCode")
                TxtUserActive.Text = userdet.Rows(0)("Active")
                txtEmail.Enabled = False
                txtCountry.Enabled = False
                TxtDesignation.Enabled = False
                txtSiteCode.Enabled = False
                TxtUserActive.Enabled = False
            Else
                ShowMessage("User does not Exist.", "Information")
                Clear()
                Exit Sub
            End If
        Catch ex As Exception
            'ShowMessage(getValueByKey("CM020"), "CM020 - " & getValueByKey("CLAE05"))
            LogException(ex)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
End Class