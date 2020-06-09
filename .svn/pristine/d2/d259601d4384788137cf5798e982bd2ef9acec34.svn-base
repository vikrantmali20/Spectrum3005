Imports SpectrumCommon
Imports SpectrumBL
Public Class frmStockInfo
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
        ' Add any initialization after the InitializeComponent() call. { new TEmp { Name = "" }, new TEmp { Name = "" } };
    End Sub

    Public _SelectedValue As String
    Private _objSite As New clsSiteInfo
    Private _GridSource As List(Of SODeliveryLocationInfo)
    Public Property GridSource As List(Of SODeliveryLocationInfo)
        Get
            If _GridSource Is Nothing Then
                _GridSource = New List(Of SODeliveryLocationInfo)
            End If
            Return _GridSource
        End Get
        Set(ByVal value As List(Of SODeliveryLocationInfo))
            _GridSource = value
        End Set
    End Property

    Private _ArticleCodeToSearch As String
    Public Property ArticleCodeToSearch As String
        Get

            Return _ArticleCodeToSearch
        End Get
        Set(ByVal value As String)
            _ArticleCodeToSearch = value
        End Set
    End Property

    Private _RequiredQuantity As Decimal
    Public Property RequiredQuantity As Decimal
        Get
            Return _RequiredQuantity
        End Get
        Set(ByVal value As Decimal)
            _RequiredQuantity = value
        End Set
    End Property

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Try
            'Dim totalEnteredQty As Decimal = GridSource.Sum(Function(item) IIf(item.Quantity Is Nothing, 0D, item.Quantity))
            'If totalEnteredQty <= RequiredQuantity Then
            '    Me.Close()
            'Else
            '    ShowMessage("Please enter required quantity", getValueByKey(""))
            'End If
            _SelectedValue = cmbSite.SelectedValue
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            'GridSource = New List(Of SODeliveryLocationInfo)()
            _SelectedValue = Nothing
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub frmStockInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Dim response = New List(Of SODeliveryLocationInfo) From {New SODeliveryLocationInfo With {.DeliverySiteName = "Korum", .ArticleName = ArticleCodeToSearch, .AvailableQuantity = 10, .DeliverySiteCode = "0000225", .ArticleCode = ArticleCodeToSearch}, _
            '                                             New SODeliveryLocationInfo With {.DeliverySiteName = "Vashi", .ArticleName = ArticleCodeToSearch, .AvailableQuantity = 5, .DeliverySiteCode = "0000226", .ArticleCode = ArticleCodeToSearch}, _
            '                                             New SODeliveryLocationInfo With {.DeliverySiteName = "Bhivandi", .ArticleName = ArticleCodeToSearch, .AvailableQuantity = 7, .DeliverySiteCode = "0000227", .ArticleCode = ArticleCodeToSearch}}
            'If GridSource.Count > 0 Then
            '    response.ForEach(AddressOf AssignEnteredQty)
            'End If
            'GridSource = response
            'lblExpectedQtyValue.Text = RequiredQuantity
            'dgStockInfo.AutoGenerateColumns = False
            'dgStockInfo.DataSource = GridSource
            cmbSite.DisplayMember = "SiteShortName"
            cmbSite.ValueMember = "SiteCode"
            cmbSite.DataSource = _objSite.GetAllSitesForDelivery()
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                ThemeChange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub AssignEnteredQty(ByVal articleStockInfo As SODeliveryLocationInfo)
        Try
            articleStockInfo.Quantity = (GridSource.Where(Function(x) x.DeliverySiteCode = articleStockInfo.DeliverySiteCode).Select(Function(y) y.Quantity).FirstOrDefault())
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub DataErrorEvent(ByVal sender As System.Object, ByVal e As DataGridViewDataErrorEventArgs) Handles dgStockInfo.DataError

    End Sub

    Public Function ThemeChange()
        Me.BackColor = Color.FromArgb(134, 134, 134)
        Label1.BackColor = Color.FromArgb(212, 212, 212)
        lblExpectedQty.BackColor = Color.FromArgb(212, 212, 212)
        lblExpectedQtyValue.BackColor = Color.FromArgb(212, 212, 212)
        btnOk.BackColor = Color.Transparent
        btnOk.BackColor = Color.FromArgb(0, 107, 163)
        btnOk.ForeColor = Color.FromArgb(255, 255, 255)
        btnOk.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnOk.FlatStyle = FlatStyle.Flat
        btnOk.FlatAppearance.BorderSize = 0
        btnOk.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnOk.Size = New Size(66, 27)

        btnCancel.BackColor = Color.Transparent
        btnCancel.BackColor = Color.FromArgb(0, 107, 163)
        btnCancel.ForeColor = Color.FromArgb(255, 255, 255)
        btnCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnCancel.Size = New Size(66, 27)


    End Function

End Class