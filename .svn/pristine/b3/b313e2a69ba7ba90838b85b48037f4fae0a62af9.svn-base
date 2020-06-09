Imports System.Text
Imports System.Data.SqlClient
Imports SpectrumBL

Public Class ViewOrderDetails

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Dim dt As New DataTable
    Dim _SiteCode As String
    Public Property SiteCode() As String
        Get
            Return _SiteCode
        End Get
        Set(ByVal value As String)
            _SiteCode = value
        End Set
    End Property
    Dim _BillNo As String
    Public Property BillNo() As String
        Get
            Return _BillNo
        End Get
        Set(ByVal value As String)
            _BillNo = value
        End Set
    End Property
    Dim objClsCommon As New clsCommon
    Private Sub ViewOrderDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        
        Call BindOrderDetails()
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
        End If
    End Sub
#Region "methods"
    Private Sub GridColumnSettings()
        Try
            grdViewOrderDetails.AllowEditing = True
            grdViewOrderDetails.Cols("DISCRIPTION").Caption = "Article Desc"
            grdViewOrderDetails.Cols("DISCRIPTION").AllowEditing = False
            grdViewOrderDetails.Cols("DISCRIPTION").Width = 300


            grdViewOrderDetails.Cols("QUANTITY").Caption = "Qty"
            grdViewOrderDetails.Cols("QUANTITY").AllowEditing = False
            grdViewOrderDetails.Cols("SELLINGPRICE").Caption = "Price "
            grdViewOrderDetails.Cols("SELLINGPRICE").AllowEditing = False
            grdViewOrderDetails.Cols("TOTALDISCOUNT").Caption = "Disc Amt "
            grdViewOrderDetails.Cols("TOTALDISCOUNT").AllowEditing = False
            grdViewOrderDetails.Cols("TOTALDISCPERCENTAGE").Caption = "Disc %"
            grdViewOrderDetails.Cols("TOTALDISCPERCENTAGE").AllowEditing = False
            
            grdViewOrderDetails.Cols("EXCLUSIVETAX").Caption = "Tax"
            grdViewOrderDetails.Cols("EXCLUSIVETAX").AllowEditing = False
            grdViewOrderDetails.Cols("GROSSAMT").Caption = "Gross"
            grdViewOrderDetails.Cols("GROSSAMT").AllowEditing = False
            'grdViewOrderDetails.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub BindOrderDetails()
        'dt = objClsCommon.FillForm(_BillNo, _SiteCode)
        dt = objClsCommon.FillForm_OnlineOrder(_BillNo, _SiteCode)
        If (dt.Rows.Count > 0) Then
            '-- fill all label values
            'lblArticleCodeValue.Text = (dt(0)(0).ToString())
            'lblArticleDescriptionValue.Text = (dt(0)(4).ToString())
            grdViewOrderDetails.DataSource = dt
            GridColumnSettings()
            lblITotalItemValue.Text = dt.Rows.Count()
            lblTotalQuantityValue.Text = dt.Compute("SUM(QUANTITY)", String.Empty).ToString()
            'lblTotalAmountValue.Text = dt.Compute("SUM(GROSSAMT)", String.Empty)
            lblTotalAmountValue.Text = Math.Round(dt.Compute("SUM(NetAmount)", String.Empty), 2)
        End If
    End Sub
    Public Function Themechange() As String
        Me.BackColor = Color.FromArgb(76, 76, 76)
        'lblTotalQuantity
        Me.lblTotalQuantity.BackColor = Color.Transparent
        Me.lblTotalQuantity.BorderStyle = BorderStyle.None
        Me.lblTotalQuantity.ForeColor = Color.White
        Me.lblTotalQuantity.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.lblTotalQuantity.Text = Me.lblTotalQuantity.Text.ToUpper()

        'lblTotalQuantityValue
        Me.lblTotalQuantityValue.BackColor = Color.Transparent
        Me.lblTotalQuantityValue.BorderStyle = BorderStyle.None
        Me.lblTotalQuantityValue.ForeColor = Color.White
        Me.lblTotalQuantityValue.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.lblTotalQuantityValue.Text = Me.lblTotalQuantityValue.Text.ToUpper()

        'lblITotaltem
        Me.lblITotalItem.BackColor = Color.Transparent
        Me.lblITotalItem.BorderStyle = BorderStyle.None
        Me.lblITotalItem.ForeColor = Color.White
        Me.lblITotalItem.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.lblITotalItem.Text = Me.lblITotalItem.Text.ToUpper()

        'lblITotaltemValue
        Me.lblITotalItemValue.BackColor = Color.Transparent
        Me.lblITotalItemValue.BorderStyle = BorderStyle.None
        Me.lblITotalItemValue.ForeColor = Color.White
        Me.lblITotalItemValue.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.lblITotalItemValue.Text = Me.lblITotalItemValue.Text.ToUpper()

        'lblTotalAmount
        Me.lblTotalAmount.BackColor = Color.Transparent
        Me.lblTotalAmount.BorderStyle = BorderStyle.None
        Me.lblTotalAmount.ForeColor = Color.White
        Me.lblTotalAmount.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.lblTotalAmount.Text = Me.lblTotalAmount.Text.ToUpper()

        'lblTotalAmountValue
        Me.lblTotalAmountValue.BackColor = Color.Transparent
        Me.lblTotalAmountValue.BorderStyle = BorderStyle.None
        Me.lblTotalAmountValue.ForeColor = Color.White
        Me.lblTotalAmountValue.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.lblTotalAmountValue.Text = Me.lblTotalAmountValue.Text.ToUpper()

        Me.grdViewOrderDetails.MaximumSize = New Size(1364, 600)
        Me.grdViewOrderDetails.Size = New System.Drawing.Size(1364, 600)
        Me.grdViewOrderDetails.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        Me.grdViewOrderDetails.Styles.Highlight.BackColor = Color.FromArgb(153, 255, 255)
        Me.grdViewOrderDetails.Styles.Highlight.ForeColor = Color.Black
        Me.grdViewOrderDetails.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdViewOrderDetails.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        Me.grdViewOrderDetails.Rows.MinSize = 26
        Me.grdViewOrderDetails.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        Me.grdViewOrderDetails.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdViewOrderDetails.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdViewOrderDetails.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdViewOrderDetails.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdViewOrderDetails.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdViewOrderDetails.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        Me.grdViewOrderDetails.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdViewOrderDetails.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdViewOrderDetails.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdViewOrderDetails.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
    End Function
#End Region

    Private Sub ViewOrderDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            dt = Nothing
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End If
    End Sub
End Class