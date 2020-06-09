Imports SpectrumBL
Public Class frmSelectBarcode

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.dgBarcodes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.Text = "Batch Barcode"
        Me.MaximizeBox = False
        Me.StartPosition = FormStartPosition.CenterParent
        Me.ShowIcon = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Private _ArticleCode As String
    Public Property ArticleCode As String
        Get
            Return _ArticleCode
        End Get
        Set(value As String)
            _ArticleCode = value
        End Set
    End Property

    Private _IsBarcodeNotAvailable As Boolean
    Public Property IsBarcodeNotAvailable As Boolean
        Get
            Return _IsBarcodeNotAvailable
        End Get
        Set(value As Boolean)
            _IsBarcodeNotAvailable = value
        End Set
    End Property

    Private _IsBarcodeAvlbleQtyZero As Boolean
    Public Property IsBarcodeAvlbleQtyZero As Boolean
        Get
            Return _IsBarcodeAvlbleQtyZero
        End Get
        Set(value As Boolean)
            _IsBarcodeAvlbleQtyZero = value
        End Set
    End Property

    Private _clsCashMemo As clsCashMemo
    Public Property ClsCashMemo As clsCashMemo
        Get
            Return _clsCashMemo
        End Get
        Set(value As clsCashMemo)
            _clsCashMemo = value
        End Set
    End Property

    Private _BarCodeTable As DataTable
    Public Property BarCodeTable As DataTable
        Get
            Return _BarCodeTable
        End Get
        Set(value As DataTable)
            _BarCodeTable = value
        End Set
    End Property

    Private _SelectedRow As DataGridViewRow
    Public Property SelectedRow As DataGridViewRow
        Get
            Return _SelectedRow
        End Get
        Set(value As DataGridViewRow)
            _SelectedRow = value
        End Set
    End Property

    Private Sub btnOk_Click(sender As System.Object, e As System.EventArgs) Handles btnOk.Click
        'Try
        '    SelectedRow = dgBarcodes.CurrentRow
        '    Me.Close()
        'Catch ex As Exception
        '    LogException(ex)
        'End Try
        BatchBarcodeExpiryDate()
    End Sub

    Private Sub dgBarcodes_DoubleClick(sender As System.Object, e As System.EventArgs) Handles dgBarcodes.DoubleClick
        BatchBarcodeExpiryDate()
    End Sub
    Public Function BatchBarcodeExpiryDate()
        Try
            'SelectedRow = dgBarcodes.CurrentRow
            ' Me.Close()

            ''added by vipul for expiry date
            If dgBarcodes.Rows.Count > 0 Then

                Dim STR = dgBarcodes.SelectedRows(0).Cells("ExpiryDate").Value
                If Now.Date > STR And Now.Date <> STR Then
                    ShowMessage("Selected Barcode is expired", "Information")
                    Exit Function
                Else
                    SelectedRow = dgBarcodes.CurrentRow
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        SelectedRow = Nothing
        Me.Close()
    End Sub

    Private Sub frmSelectBarcode_Load(sender As System.Object, e As System.EventArgs) Handles Me.Load
        Try
            ClsCashMemo = New clsCashMemo()
            dgBarcodes.AutoGenerateColumns = False
            BarCodeTable = ClsCashMemo.GetBardCodesForArticle(clsAdmin.SiteCode, ArticleCode)
            If BarCodeTable Is Nothing OrElse BarCodeTable.Rows.Count = 0 Then
                IsBarcodeNotAvailable = True
                Me.Close()
            End If
            'vipul for not showing zero qty article in grid 04-09-2018
            If BarCodeTable Is Nothing OrElse BarCodeTable.Rows.Count > 0 Then
                Dim AvailableQty As Double = 0
                AvailableQty = BarCodeTable.Compute("SUM(QtyAllocated)", "")
                If AvailableQty <= 0 Then
                    IsBarcodeAvlbleQtyZero = True
                    Me.Close()
                End If
            End If

            Dim dataView As DataView = BarCodeTable.DefaultView
            dataView.RowFilter = "QtyAllocated > 0"
            dgBarcodes.DataSource = dataView
            dgBarcodes.DataSource = BarCodeTable
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                ThemeChange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub frmSelectBarcode_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        dgBarcodes.Focus()

    End Sub
    Public Function ThemeChange()
        'added by vipul for gridview 
        dgBarcodes.BackColor = Color.FromArgb(212, 212, 212)
        Me.BackColor = Color.FromArgb(134, 134, 134)
        ' btnOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnOk.BackColor = Color.Transparent
        btnOk.BackColor = Color.FromArgb(0, 107, 163)
        btnOk.ForeColor = Color.FromArgb(255, 255, 255)
        btnOk.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        ' btnOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnOk.FlatStyle = FlatStyle.Flat
        btnOk.FlatAppearance.BorderSize = 0
        btnOk.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        'BtnSave.Size = New Size(85, 30)
        With btnCancel
            ' .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
            .BackColor = Color.Transparent
            .BackColor = Color.FromArgb(0, 107, 163)
            .ForeColor = Color.FromArgb(255, 255, 255)
            .Font = New Font("Neo Sans", 9, FontStyle.Bold)
            '.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
            .FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        End With
        With dgBarcodes
            dgBarcodes.BackgroundColor = Color.White
            dgBarcodes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(177, 227, 253)
            dgBarcodes.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            dgBarcodes.DefaultCellStyle.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            dgBarcodes.DefaultCellStyle.SelectionBackColor = Color.LightBlue
            dgBarcodes.DefaultCellStyle.SelectionForeColor = Color.Black
            ' dgBarcodes.RowsDefaultCellStyle.Font = New Font("Neo Sans", 10, FontStyle.Bold)



            'dgBarcodes.RowsDefaultCellStyle.BackColor = Color.FromArgb(177, 227, 253)
            'With grdAddressMapping
            '    .VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
            '    .Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
            '    .Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
            '    .Rows.MinSize = 25
            '    .Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
            '    .Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            '    .Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            '    .Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            '    .Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            '    .Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
            'End With
        End With
    End Function

    Private Sub frmSelectBarcode_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnOk_Click(sender, e)
        End If
    End Sub
End Class