Imports SpectrumCommon
Imports SpectrumBL
Imports System.Collections
Imports System.ComponentModel
Imports System.Linq
Imports NPOI.HSSF.UserModel
Imports NPOI.HPSF
Imports System.IO
Imports NPOI.SS.UserModel
Public Class frmOtherStockTakeDetails

    Dim objDayClose As New clsDayClose
    Private _RawMaterialDetailsList As BindingList(Of RawMaterialDetails)
    Public Property RawMaterialDetailsList() As BindingList(Of RawMaterialDetails)
        Get
            Return _RawMaterialDetailsList
        End Get
        Set(ByVal value As BindingList(Of RawMaterialDetails))
            _RawMaterialDetailsList = value
        End Set
    End Property
    Private Sub frmOtherStockTakeDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'Me.AutoSize = False
            'Me.AutoSizeMode = Windows.Forms.AutoSizeMode.GrowOnly
            'Me.ShowIcon = True
            'Me.HelpButton = True
            'Me.MinimizeBox = True
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            BindotherStockTake()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub BindotherStockTake()
        Try
            Me.Text = "Day Close - Raw Material Closing"
            Dim screenList As New List(Of DayCloseScreenConfig)
            screenList = objDayClose.GetDayCloseScreenConfig(Modules.Day_Close, DayCloseScreens.RawMaterialDetails)
            Dim screenInfo As DayCloseScreenConfig
            screenInfo = screenList(0)

            CtrlRawMaterialClosing1.GetDayCloseData(New DayCloseDataRequestModel(Of RawMaterialDetails) With {.DayCloseDate = clsAdmin.DayOpenDate, .Query = screenInfo.Script, .SiteCode = clsAdmin.SiteCode})
            'TableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset
            'Dim btn As New Spectrum.CtrlBtn
            'btn.Text = "Close"
            'AddHandler btn.Click, AddressOf btn_Click
            'TableLayoutPanel1.Controls.Add(btn, 0, 0)
            If CtrlRawMaterialClosing1.RawMaterialDetailsList IsNot Nothing Then
                Dim stocktakedata = LogForStokeTake(CtrlRawMaterialClosing1.RawMaterialDetailsList)
                writestokeTakeLog(vbNewLine + stocktakedata + vbNewLine + " Load raw material data on screeen")
            End If
            If CtrlRawMaterialClosing1.RawMaterialDetailsList Is Nothing Then
                Me.Dispose()
                Me.DialogResult = Windows.Forms.DialogResult.Abort
                Me.Close()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Me.CtrlRawMaterialClosing1.IsTotalDataValid Then
                Dim stocktakedata = LogForStokeTake(CtrlRawMaterialClosing1.RawMaterialDetailsList)
                writestokeTakeLog(vbNewLine + stocktakedata + vbNewLine + " get from Raw materail screen to save")
                RawMaterialDetailsList = Me.CtrlRawMaterialClosing1.RawMaterialDetailsList
                Me.DialogResult = Windows.Forms.DialogResult.OK
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If RawMaterialDetailsList IsNot Nothing Then
                If RawMaterialDetailsList.Count > 0 Then
                    RawMaterialDetailsList.Clear()
                End If
            End If
            Me.DialogResult = Windows.Forms.DialogResult.Abort
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub writestokeTakeLog(ByVal mes As String)
        Dim ax As New ApplicationException(mes)
        LogException(ax)
    End Sub
    Private Function Themechange()

        Me.BackColor = Color.FromArgb(134, 134, 134)
        btnBack.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnBack.BackColor = Color.Transparent
        btnBack.BackColor = Color.FromArgb(0, 107, 163)
        btnBack.ForeColor = Color.FromArgb(255, 255, 255)
        ' btnBack.Size = New Size(71, 30)
        btnBack.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnBack.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnBack.FlatStyle = FlatStyle.Flat
        btnBack.FlatAppearance.BorderSize = 0
        btnBack.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnSave.BackColor = Color.Transparent
        btnSave.BackColor = Color.FromArgb(0, 107, 163)
        btnSave.ForeColor = Color.FromArgb(255, 255, 255)

        btnSave.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

    End Function

    Private Sub frmOtherStockTakeDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            RawMaterialDetailsList.Clear()
            Me.DialogResult = Windows.Forms.DialogResult.Abort
            Me.Close()
        End If
    End Sub
End Class
