Imports System.IO
Public Class frmProductExpiryDatePopUp
    Private _dtCreditSales As DataTable
    Public id As Integer
    Public Property dtCreditSales() As DataTable
        Get
            Return _dtCreditSales
        End Get
        Set(ByVal value As DataTable)
            _dtCreditSales = value
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


    Private Sub frmProductExpiryDatePopUp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        Try
            Dim rowcount As Integer = dtCreditSales.Rows.Count
            If PopupType = enumProductNotificationTimerPopups.ExpiryProductPopup Then
                ' grdPopUp.AutoSizeRow(i)
                Me.Text = "Near To Product Expiry."
                grdPopUp.Cols(1).Caption = "ArticleName"
                grdPopUp.Cols(1).Width = 550
                grdPopUp.Cols(1).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                grdPopUp.Cols(1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                grdPopUp.Cols(2).Caption = "Quantity"
                grdPopUp.Cols(2).Width = 100
                grdPopUp.Cols(2).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterTop
                grdPopUp.Cols(2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterTop
                grdPopUp.Cols(3).Caption = "ExpiryDate"
                grdPopUp.Cols(3).Width = 15
                grdPopUp.Cols(3).DataType = Type.GetType("System.DateTime")
                grdPopUp.Cols(3).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterTop
                grdPopUp.Cols(3).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterTop
                ' grdPopUp.DataSource = dtCreditSales
                Dim i As Integer = 1
                For Each dr As DataRow In dtCreditSales.Rows
                    grdPopUp.Rows.Add()
                    grdPopUp.Rows(i)(1) = dr("ArticleShortName")
                    grdPopUp.Rows(i)(2) = dr("QtyAllocated")
                    grdPopUp.Rows(i)(3) = dr("ExpiryDate")
                    i = i + 1
                Next
                'grdPopUp.Rows(0).Height = 2 * grdPopUp.Rows.DefaultSize
            End If
            For i = 1 To grdPopUp.Rows.Count - 1
                If grdPopUp.Rows(i)(3) < DateTime.Today.AddDays(0) Then
                    Me.grdPopUp.Rows(i).StyleNew.Font = New Font("Neo Sans", 9.25, FontStyle.Bold)
                    Me.grdPopUp.Rows(i).StyleNew.BackColor = Color.Red
                End If
            Next
        Catch ex As Exception
            LogException(ex)
        End Try 
    End Sub

    Public Function Themechange() As String
        Me.BackColor = Color.FromArgb(76, 76, 76)

        'btnExport.VisualStyle = C1.Win.C1Input.VisualStyle.System
        'btnExport.BackColor = Color.Transparent
        'btnExport.BackColor = Color.FromArgb(0, 107, 163)
        'btnExport.ForeColor = Color.FromArgb(255, 255, 255)
        'btnExport.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'btnExport.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        'btnExport.FlatAppearance.BorderSize = 0
        'btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat

        Me.grdPopUp.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        Me.grdPopUp.Styles.Highlight.BackColor = Color.FromArgb(153, 255, 255)
        Me.grdPopUp.Styles.Highlight.ForeColor = Color.Black
        Me.grdPopUp.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdPopUp.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        Me.grdPopUp.Rows.MinSize = 26
        Me.grdPopUp.AutoResize = True

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

    Private Sub frmProductExpiryDatePopUp_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub btnExport_Click(sender As System.Object, e As System.EventArgs) Handles btnExport.Click
        Try
            Dim filedlg As New SaveFileDialog()
            filedlg.DefaultExt = "xlsx"
            filedlg.Filter = "Excel File|*.xlsx"

            filedlg.FileName = "Product Expiry" & DateTime.Now.ToString("dd-MM-yyyy_HH_mm_ss") & ".xlsx"
            Dim result As DialogResult = filedlg.ShowDialog

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