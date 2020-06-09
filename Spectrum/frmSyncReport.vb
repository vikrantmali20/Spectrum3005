Imports System.Data
Imports System.Data.SqlClient
Imports SpectrumBL
Public Class frmSyncReport
    Dim dtSyncData As DataTable
    Dim obj As New clsCommon
    Private Sub frmSyncReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            dtSyncData = New DataTable
            dtSyncData = obj.GetSyncDetail()
            dgSyncMain.DataSource = dtSyncData
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception

        End Try
        SetCulture(Me, Me.Name)
    End Sub

    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(212, 212, 212)
        dgSyncMain.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        dgSyncMain.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgSyncMain.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        dgSyncMain.Rows.MinSize = 25
        dgSyncMain.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        dgSyncMain.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgSyncMain.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgSyncMain.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgSyncMain.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgSyncMain.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgSyncMain.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
    End Function

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub
End Class