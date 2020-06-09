
Imports SpectrumBL
Imports System.Text.RegularExpressions
Public Class frmacceptReject


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
    Private _ZomatoAcceptScreen As Boolean = False
    Public Property ZomatoAcceptScreen() As Boolean
        Get
            Return _ZomatoAcceptScreen
        End Get
        Set(ByVal value As Boolean)
            _ZomatoAcceptScreen = value
        End Set
    End Property
    Private _ZomatoRejectScreen As Boolean = False
    Public Property ZomatoRejectScreen() As Boolean
        Get
            Return _ZomatoRejectScreen
        End Get
        Set(ByVal value As Boolean)
            _ZomatoRejectScreen = value
        End Set
    End Property
    Dim objcomn As New clsCommon

    Private Sub cmdOk_Click(sender As Object, e As EventArgs) Handles cmdOk.Click
        Try '##
            'If IsKOTRemark Then
            'ResultRemark = txtRemark.Text.Replace("'", "")
            ''If objCM.UpdateKOTRemark(txtSearchCustomer.Text.Replace("'", ""), currentBillNo, articlecode, eanno) Then
            'ShowMessage(getValueByKey("DIN011"), "DIN011 - " & getValueByKey("DIN012"))
            ''    'ShowMessage("Saved", "")
            'End If
            Me.DialogResult = DialogResult.OK
            Me.Close()
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

            If ZomatoAcceptScreen Then
                pnDeliveryTime.Visible = True
                PnRejectReason.Visible = False
                PaRejectReamrk.Visible = False
                Me.Text = "Accept Order"
                Me.lblRemark.Text = "Delivery Time in min"
                '  Me.lblRemark.DataType = System.Int16
                Me.TxtDeliveryTime.MaxLength = 100
                Me.TxtDeliveryTime.Multiline = True
                TxtDeliveryTime.Text = ResultRemark
                TxtDeliveryTime.Focus()
                Me.Size = New System.Drawing.Size(238, 150)
            End If
            If ZomatoRejectScreen Then
                pnDeliveryTime.Visible = False
                PnRejectReason.Visible = True
                PaRejectReamrk.Visible = True
                Me.Text = "Reject Order"
                Dim dChannelPartner As DataTable = objcomn.GetAllRejectionReasons()
                PopulateComboBox(dChannelPartner, CboRejectReason)
                pC1ComboSetDisplayMember(CboRejectReason)
                CboRejectReason.SelectedIndex = 0
                Me.Size = New System.Drawing.Size(238, 230)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub txtSearchCustomer_KeyPress(sender As Object, e As KeyPressEventArgs)
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

    Private Sub txtSearchCustomer_TextChanged(sender As Object, e As EventArgs)
        Try

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
        TxtDeliveryTime.Font = New Font("Neo Sans", 9, FontStyle.Regular)
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

    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel1.Paint

    End Sub
End Class


