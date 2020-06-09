
Public Class frmBrandWiseSale
    Dim dvPayment, dvIssue As DataView
    Public TillOpenValue, VendorAmount As Decimal
    Private _dtCash As DataTable
    Private _dtOther As DataTable
    Private _dtMain As DataTable

    Public Property dtCash() As DataTable
        Get
            Return _dtCash
        End Get
        Set(ByVal value As DataTable)
            _dtCash = value
        End Set
    End Property
    Public Property dtOther() As DataTable
        Get
            Return _dtOther
        End Get
        Set(ByVal value As DataTable)
            _dtOther = value
        End Set
    End Property
    Public Property dtMain() As DataTable
        Get
            Return _dtMain
        End Get
        Set(ByVal value As DataTable)
            _dtMain = value
        End Set
    End Property

    Private Sub frmBrandWiseSale_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If dtMain.Rows.Count > 0 Then
            dgBrandsPayments.DataSource = dtMain
            BuildDetails(dtMain)

        End If

        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
        End If
    End Sub

    Private Sub BuildDetails(ByVal dtMain As DataTable)
        Try

            dgBrandsPayments.Cols("Brand").Caption = "Brand"
            dgBrandsPayments.Cols("Brand").Width = 410
            dgBrandsPayments.Cols("TenderType").Caption = "Tender Type"
            dgBrandsPayments.Cols("TenderType").Width = 100
            dgBrandsPayments.Cols("Amount").Format = "0.00"
            dgBrandsPayments.Cols("Amount").Caption = "Amount"
            dgBrandsPayments.Cols("Amount").Width = 120

            Dim RowNumbrColorChange As Integer = 0
            For Each dgRow In dgBrandsPayments.Rows
                If dgRow("Brand") = "Sub Total" Then
                    dgBrandsPayments.Rows(RowNumbrColorChange).StyleNew.BackColor = Color.FromArgb(226, 226, 255)
                    dgBrandsPayments.Rows(RowNumbrColorChange).StyleNew.ForeColor = Color.Blue
                    dgBrandsPayments.Rows(RowNumbrColorChange).StyleNew.Font = New Font("Neo Sans", 9, FontStyle.Bold)
                End If
                If dgRow("Brand") = "Grand Total" Then
                    dgBrandsPayments.Rows(RowNumbrColorChange).StyleNew.BackColor = Color.FromArgb(182, 182, 252)
                    dgBrandsPayments.Rows(RowNumbrColorChange).StyleNew.ForeColor = Color.Red
                    dgBrandsPayments.Rows(RowNumbrColorChange).StyleNew.Font = New Font("Neo Sans", 9, FontStyle.Bold)
                End If
                RowNumbrColorChange = RowNumbrColorChange + 1
            Next
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        dgBrandsPayments.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        dgBrandsPayments.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgBrandsPayments.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        dgBrandsPayments.Rows.MinSize = 25
        dgBrandsPayments.Styles.Normal.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        dgBrandsPayments.Styles.Fixed.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgBrandsPayments.Styles.Highlight.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgBrandsPayments.Styles.Highlight.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgBrandsPayments.Styles.Focus.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgBrandsPayments.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
    End Function
End Class