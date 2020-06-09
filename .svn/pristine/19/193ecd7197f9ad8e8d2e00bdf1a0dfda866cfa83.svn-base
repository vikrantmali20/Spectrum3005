Imports System.Data.SqlClient
Imports SpectrumBL
Imports System.Globalization
Public Class frmDayOpen
    Dim objLoginData As New clsLogin

    Private _MinDate As DateTime
    Public Property MinDate() As DateTime
        Get
            Return _MinDate
        End Get
        Set(ByVal value As DateTime)
            _MinDate = value
        End Set
    End Property


    Private _MaxDate As DateTime
    Public Property MaxDate() As DateTime
        Get
            Return _MaxDate
        End Get
        Set(ByVal value As DateTime)
            _MaxDate = value
        End Set
    End Property

    '' added by nikhil
    Private _DayUpdate As Boolean
    Public Property DayUpdate() As Boolean
        Get
            Return _DayUpdate

        End Get
        Set(value As Boolean)
            _DayUpdate = value
        End Set
    End Property


    Private Sub CtrlBtn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles okBtn.Click
        Try
            '' added by Nikhil  for Hari OM to get billing details is done or not
            Dim objTill As New clsTill
            If clsDefaultConfiguration.IsHariOM Then

                Dim dtBillDate As New DataTable
                dtBillDate = objTill.GetBillingDetailsForHariOM(clsAdmin.SiteCode)
                Dim BillDate As Date = dtBillDate.Rows(0)("BILLDATE")
                If BillDate = DirectCast(dayOpenDate.Value, Date).Date Then
                    ShowMessage("Day open can not allowed , bill are punched for that day", "Information")
                    Me.Close()
                Else
                    DayUpdate = True
                    Me.Close()

                End If
            Else
                If dayOpenDate.Value Is Nothing Or IsDBNull(dayOpenDate.Value) Then
                    ShowMessage("Enter Valid Date", "Information")
                    Exit Sub
                Else
                    If DirectCast(dayOpenDate.Value, Date).Date > _MinDate AndAlso DirectCast(dayOpenDate.Value, Date).Date <= _MaxDate Then
                        Me.Close()
                    Else
                        ShowMessage("Enter Valid Date", "Information")
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub CtrlBtn2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cancelBtn.Click
        dayOpenDate.Value = Nothing
        Me.Close()
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub
  
    Private Sub frmDayOpen_Load(sender As Object, e As EventArgs) Handles Me.Load
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
        End If
    End Sub
    Private Function Themechange()
        Me.BackColor = Color.FromArgb(134, 134, 134)
        TableLayoutPanel1.BackColor = Color.FromArgb(134, 134, 134)
        Panel1.BackColor = Color.FromArgb(134, 134, 134)
        okBtn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        okBtn.BackColor = Color.Transparent
        okBtn.BackColor = Color.FromArgb(0, 107, 163)
        okBtn.ForeColor = Color.FromArgb(255, 255, 255)
        okBtn.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        okBtn.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        okBtn.FlatStyle = FlatStyle.Flat
        okBtn.FlatAppearance.BorderSize = 0
        okBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        okBtn.Size = New Size(75, 26)
        dayOpenDate.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cancelBtn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cancelBtn.BackColor = Color.Transparent
        cancelBtn.BackColor = Color.FromArgb(0, 107, 163)
        cancelBtn.ForeColor = Color.FromArgb(255, 255, 255)
        cancelBtn.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cancelBtn.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cancelBtn.FlatStyle = FlatStyle.Flat
        cancelBtn.FlatAppearance.BorderSize = 0
        cancelBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cancelBtn.Size = New Size(75, 26)

        Label1.ForeColor = Color.White
        Label2.ForeColor = Color.White
    End Function
End Class