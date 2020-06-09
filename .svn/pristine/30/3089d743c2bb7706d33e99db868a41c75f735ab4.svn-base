Imports System.IO
Public Class frmLowStockNotificationPopup
    Public id As Integer
    Private _dtLowStock As DataTable
    Public Property dtLowStock() As DataTable
        Get
            Return _dtLowStock
        End Get
        Set(ByVal value As DataTable)
            _dtLowStock = value
        End Set
    End Property
    Private _PopupType As String
    Public Property PopupType() As String
        Get
            Return _PopupType
        End Get
        Set(ByVal value As String)
            _PopupType = value
        End Set
    End Property

    Private Sub frmLowStockNotificationPopup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            gridSetting()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub gridSetting()

        lblHeading.Text = "Low Stock Notification"
        Me.Text = "Stocks below minimum quantity notifications"
        grdPopUp.DataSource = dtLowStock
        grdPopUp.Cols("ArticleCode").Caption = "Article Code"
        grdPopUp.Cols("ArticleCode").Width = 140
        grdPopUp.Cols("ArticleName").Caption = "Article Name"
        grdPopUp.Cols("ArticleName").Width = 200
        grdPopUp.Cols("BaseUOM").Caption = "Base UOM"
        grdPopUp.Cols("BaseUOM").Width = 80
        grdPopUp.Cols("Minimumstock").Caption = "Minimum stock"
        grdPopUp.Cols("Minimumstock").Width = 140
        grdPopUp.Cols("Availablestock").Caption = "Available stock "
        grdPopUp.Cols("Availablestock").Width = 100
   
    End Sub
    Public Function Themechange() As String
        Me.BackColor = Color.FromArgb(76, 76, 76)
        Me.grdPopUp.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        Me.grdPopUp.Styles.Highlight.BackColor = Color.FromArgb(153, 255, 255)
        Me.grdPopUp.Styles.Highlight.ForeColor = Color.Black
        Me.grdPopUp.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdPopUp.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        Me.grdPopUp.Rows.MinSize = 26
        Me.grdPopUp.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        Me.grdPopUp.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPopUp.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPopUp.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPopUp.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPopUp.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdPopUp.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        Me.grdPopUp.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPopUp.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPopUp.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdPopUp.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)

        Me.lblHeading.BackColor = Color.Transparent
        Me.lblHeading.BorderStyle = BorderStyle.None
        Me.lblHeading.ForeColor = Color.White
        Me.lblHeading.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblHeading.Text = Me.lblHeading.Text.ToUpper()
    End Function

    Private Sub frmLowStockNotificationPopup_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            dtLowStock = Nothing
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End If
    End Sub

    Private Sub cmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExport.Click
        Try
            Dim filedlg As New SaveFileDialog()
            filedlg.DefaultExt = "xlsx"
            filedlg.Filter = "Excel File|*.xlsx"

            filedlg.FileName = "Low Stock Notifications" & DateTime.Now.ToString("dd-MM-yyyy_HH_mm_ss") & ".xlsx"
            Dim result As DialogResult = filedlg.ShowDialog

            '  Dim path As String = clsDefaultConfiguration.DayCloseReportPath & "\" & "Low Stock Notification" & DateTime.Now.ToString("dd-MM-yyyy_HH_mm") & ".xlsx"

            If result = Windows.Forms.DialogResult.OK And filedlg.FileName <> "" Then
               

            Dim vs As C1.Win.C1FlexGrid.VisualStyle
            Dim ps As New System.Drawing.Printing.PrinterSettings

          
            vs = grdPopUp.VisualStyle
            grdPopUp.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue


            For Each pa As System.Drawing.Printing.PaperSize In ps.PaperSizes
                If pa.PaperName = "A4″ Then" Then
                    ps.DefaultPageSettings.PaperSize = pa
                    Exit For
                End If
            Next

            Dim tmp As C1.Win.C1FlexGrid.DrawModeEnum = grdPopUp.DrawMode
            grdPopUp.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.Normal

            grdPopUp.SaveExcel(filedlg.FileName, "Sheet1", CType(C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells Or C1.Win.C1FlexGrid.FileFlags.VisibleOnly Or C1.Win.C1FlexGrid.FileFlags.SaveMergedRanges, C1.Win.C1FlexGrid.FileFlags), ps)

            grdPopUp.DrawMode = tmp
            grdPopUp.VisualStyle = vs
            vs = Nothing
            ps = Nothing

            id = System.Diagnostics.Process.Start(filedlg.FileName).Id
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

End Class