Imports SpectrumBL

Public Class frmArticlesRemark
    Private _ResultRemark As String
    Dim objCls As New clsCommon
    Dim statusbox As New DataTable
    Public Property ResultRemark() As String
        Get
            Return _ResultRemark
        End Get
        Set(ByVal value As String)
            _ResultRemark = value
        End Set
    End Property
    Private Property _isKOTReason As Boolean
    Public Property IsKOTReason() As Boolean
        Get
            Return _isKOTReason
        End Get
        Set(ByVal value As Boolean)
            _isKOTReason = value
        End Set
    End Property
    Private _KOTReasonDetails As String
    Public Property KOTReasonDetails() As String
        Get
            Return _KOTReasonDetails
        End Get
        Set(ByVal value As String)
            _KOTReasonDetails = value
        End Set
    End Property
    Private Property _isTicket As Boolean = False
    Public Property isTicket() As Boolean
        Get
            Return _isTicket
        End Get
        Set(ByVal value As Boolean)
            _isTicket = value
        End Set
    End Property
    Private _ticketStatus As String
    Public Property TicketStatus() As String
        Get
            Return _ticketStatus
        End Get
        Set(ByVal value As String)
            _ticketStatus = value
        End Set
    End Property
    Public _RaisedBy As String
    Public Property RaisedBy() As String
        Get
            Return _RaisedBy
        End Get
        Set(ByVal value As String)
            _RaisedBy = value
        End Set
    End Property
    Public _AuthUserRemarks As String
    Public Property AuthUserRemarks() As String
        Get
            Return _AuthUserRemarks
        End Get
        Set(ByVal value As String)
            _AuthUserRemarks = value
        End Set
    End Property
    Private Sub frmArticlesRemark_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            If isTicket Then
                Me.Text = "Status"
                CtrllblRemark.Visible = False
                CtrlTxtRemark.Visible = False
                statusbox = objCls.GetStatus()
                statusbox.Rows.Clear()
                IsChangeStatus()
                cbStatus.SelectedText = TicketStatus
                IsKOTReason = False
            Else
                cbStatus.Visible = False
                lblStatus.Visible = False
                If IsKOTReason = True Then
                    CtrllblRemark.Text = "Reason"
                    Me.Text = " Add Reason"
                    If clsDefaultConfiguration.EvasPizzaChanges = True Then
                        CtrlTxtRemark.Text = KOTReasonDetails
                    End If
                Else
                    CtrlTxtRemark.Text = ResultRemark
                End If
                CtrlTxtRemark.Focus()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdOk_Click(sender As Object, e As EventArgs) Handles cmdOk.Click
        If IsKOTReason = True Then
            If CtrlTxtRemark.Text <> "" Then
                KOTReasonDetails = CtrlTxtRemark.Text
            Else
                ShowMessage("Please enter reason..", getValueByKey("CLAE04"))
                CtrlTxtRemark.Focus()
                Exit Sub
            End If
        Else

            If isTicket = True Then
                ResultRemark = CtrlTxtRemark.Text.Replace("'", "")
            Else
                If CtrlTxtRemark.Text <> "" Then
                    KOTReasonDetails = CtrlTxtRemark.Text

                Else
                    ShowMessage("Please enter reason..", getValueByKey("CLAE04"))
                    CtrlTxtRemark.Focus()
                    Exit Sub
                End If
                ResultRemark = CtrlTxtRemark.Text.Replace("'", "")
                _AuthUserRemarks = ResultRemark
            End If
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        IsKOTReason = False
        Me.Close()
    End Sub
    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        cmdOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdOk.Location = New Point(244, 1)
        cmdOk.BackColor = Color.Transparent
        cmdOk.BackColor = Color.FromArgb(0, 107, 163)
        cmdOk.ForeColor = Color.FromArgb(255, 255, 255)
        cmdOk.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdOk.FlatStyle = FlatStyle.Flat
        cmdOk.FlatAppearance.BorderSize = 0
        cmdOk.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdOk.Size = New Size(56, 28)
        CtrllblRemark.ForeColor = Color.White
        CtrllblRemark.BackColor = Color.Transparent
        CtrllblRemark.BorderStyle = BorderStyle.None
        CtrllblRemark.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlTxtRemark.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdCancel.Location = New Point(307, 1)
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
    'Private Function IsChangeStatus()
    '    Dim statusMsg As String = TicketStatus
    '    Select Case statusMsg
    '        Case "New"
    '            statusbox.Rows.Add("New", "New")
    '            'statusbox.Rows.Add("Open", "Open")
    '            statusbox.Rows.Add("Resolved", "Resolved")
    '        Case "Open"
    '            statusbox.Rows.Add("Open", "Open")
    '            'statusbox.Rows.Add("Resolved", "Resolved")
    '        Case "Resolved"
    '            statusbox.Rows.Add("Resolved", "Resolved")
    '            statusbox.Rows.Add("Reopened", "Reopened")
    '        Case "Re-opened"
    '            statusbox.Rows.Add("Reopened", "Reopened")
    '            'statusbox.Rows.Add("Resolved", "Resolved")


    '    End Select

    '    PopulateComboBox(statusBox, cbStatus)
    '    pC1ComboSetDisplayMember(cbStatus)
    'End Function

    Private Function IsChangeStatus()
        Dim statusBox = objCls.GetStatus()
        If clsDefaultConfiguration.JkTicketingSystem Then
            statusbox.Rows.RemoveAt(3)
        End If

        Dim statusMsg As String = TicketStatus
        Select Case statusMsg
            Case "New"
                statusBox.Rows.RemoveAt(2)
                If RaisedBy = "Fo" Then
                    statusBox.Rows.RemoveAt(1)
                ElseIf RaisedBy = "Bo" Then
                    statusBox.Rows.Clear()
                    statusBox.Rows.Add("New", "New")
                    statusBox.Rows.Add("Resolved", "Resolved")
                End If
            Case "Open"
                statusBox.Rows.Clear()
                statusBox.Rows.Add("Open", "Open")
            Case "Resolved"
                'statusBox.Rows.RemoveAt(0)
                'If RaisedBy = "Fo" Then
                '    statusBox.Rows.RemoveAt(0)
                'ElseIf RaisedBy = "Bo" Then
                '    statusBox.Rows.RemoveAt(1)
                'End If
                statusBox.Rows.Clear()
                If RaisedBy = "Fo" Then
                    statusBox.Rows.Add("Resolved", "Resolved")
                    statusBox.Rows.Add("Re-opened", "Re-opened")
                ElseIf RaisedBy = "Bo" Then
                    statusBox.Rows.Add("Resolved", "Resolved")
                End If
            Case "Re-opened"
                statusBox.Rows.Clear()
                If RaisedBy = "Fo" Then
                    statusBox.Rows.Add("Re-opened", "Re-opened")
                ElseIf RaisedBy = "Bo" Then
                    statusBox.Rows.Add("Resolved", "Resolved")
                    statusBox.Rows.Add("Re-opened", "Re-opened")
                End If
            Case "Closed"
                statusBox.Rows.RemoveAt(1)
                statusBox.Rows.RemoveAt(1)
        End Select

        PopulateComboBox(statusBox, cbStatus)
        pC1ComboSetDisplayMember(cbStatus)
    End Function
End Class