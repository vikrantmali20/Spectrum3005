
Imports SpectrumBL
Imports System.Text.RegularExpressions
Public Class frmTrackCustomers


    Dim objCM As New clsCashMemo
    Dim Result As New Integer
    Dim _currenttableNo As String
    Public Property currenttableNo As String
        Get
            Return _currenttableNo
        End Get
        Set(ByVal value As String)
            _currenttableNo = value
        End Set
    End Property

    Dim _currentBillNo As String
    Public Property currentBillNo As String
        Get
            Return _currentBillNo
        End Get
        Set(ByVal value As String)
            _currentBillNo = value
        End Set
    End Property

    Dim _articlecode As String
    Public Property articlecode As String
        Get
            Return _articlecode
        End Get
        Set(ByVal value As String)
            _articlecode = value
        End Set
    End Property

    Dim _eanno As String
    Public Property eanno As String
        Get
            Return _eanno
        End Get
        Set(ByVal value As String)
            _eanno = value
        End Set
    End Property

    Public _IsKOTRemark As Boolean = False
    Public Property IsKOTRemark() As Boolean
        Get
            Return _IsKOTRemark
        End Get
        Set(ByVal value As Boolean)
            _IsKOTRemark = value
        End Set
    End Property


    Private _ResultRemark As String
    Public Property ResultRemark() As String
        Get
            Return _ResultRemark
        End Get
        Set(ByVal value As String)
            _ResultRemark = value
        End Set
    End Property
    Private _TotalPaxONTable As String = ""
    Public Property TotalPaxONTable() As String
        Get
            Return _TotalPaxONTable
        End Get
        Set(ByVal value As String)
            _TotalPaxONTable = value
        End Set
    End Property


    Private Sub cmdOk_Click(sender As Object, e As EventArgs) Handles cmdOk.Click
        Try '##
            If IsKOTRemark Then
                ResultRemark = txtRemark.Text.Replace("'", "")
                'If objCM.UpdateKOTRemark(txtSearchCustomer.Text.Replace("'", ""), currentBillNo, articlecode, eanno) Then
                ShowMessage(getValueByKey("DIN011"), "DIN011 - " & getValueByKey("DIN012"))
                '    'ShowMessage("Saved", "")
                'End If
            Else
                TotalPaxONTable = txtNoOfCustomer.Text
                If Not String.IsNullOrEmpty(txtNoOfCustomer.Text) Then '## Vipin
                    If objCM.UpdateTrackCustomers(currentBillNo, currenttableNo, clsAdmin.SiteCode, txtNoOfCustomer.Text) Then
                        ShowMessage(getValueByKey("DIN027"), "DIN027 - " & getValueByKey("DIN013"))
                        'ShowMessage("Saved", "")
                    End If
                End If
            End If
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            LogException(ex)
            ShowMessage("Error", "" & getValueByKey("DIN014"))
        End Try

    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmTrackCustomers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            If IsKOTRemark Then
                PnNoOfCustomer.Visible = False
                Me.Text = "KOT Remarks"
                Me.lblRemark.Text = "Enter Remark"
                Me.txtRemark.MaxLength = 100
                Me.txtRemark.Multiline = True
                txtRemark.Text = ResultRemark
                txtRemark.Focus()
            Else
                pnRemark.Visible = False
                pnButtons.Width = PnNoOfCustomer.Width
                Me.Size = New System.Drawing.Size(360, 135)
                Result = objCM.GetTrackCustomers(currentBillNo, currenttableNo, clsAdmin.SiteCode)
                If (Result = 0) Then
                    txtNoOfCustomer.Text = ""
                    txtNoOfCustomer.Text = TotalPaxONTable
                Else
                    txtNoOfCustomer.Text = Result
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub txtSearchCustomer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNoOfCustomer.KeyPress
        Try
            If IsKOTRemark = False Then
                If Asc(e.KeyChar) <> 8 Then
                    If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                        e.Handled = True
                    End If
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtSearchCustomer_TextChanged(sender As Object, e As EventArgs) Handles txtNoOfCustomer.TextChanged
        Try
            If IsKOTRemark = False Then
                Dim reg As New Regex("^[1-9][0-9]*$")
                If reg.IsMatch(txtNoOfCustomer.Text) Then
                Else
                    txtNoOfCustomer.Text = ""
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub frmTrackCustomers_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmdOk_Click(sender, e)
        End If
    End Sub

    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        lblRemark.ForeColor = Color.White
        lblNoOfCustomer.ForeColor = Color.White
        txtRemark.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        txtNoOfCustomer.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        cmdOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdOk.BackColor = Color.Transparent
        cmdOk.BackColor = Color.FromArgb(0, 107, 163)
        cmdOk.ForeColor = Color.FromArgb(255, 255, 255)
        cmdOk.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdOk.FlatStyle = FlatStyle.Flat
        cmdOk.FlatAppearance.BorderSize = 0
        cmdOk.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdOk.Size = New Size(56, 28)

        cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdCancel.BackColor = Color.Transparent
        cmdCancel.BackColor = Color.FromArgb(0, 107, 163)
        cmdCancel.ForeColor = Color.FromArgb(255, 255, 255)
        cmdCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdCancel.FlatStyle = FlatStyle.Flat
        cmdCancel.FlatAppearance.BorderSize = 0
        cmdCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdCancel.Size = New Size(63, 28)
    End Function
End Class


