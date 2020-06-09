Imports C1.Win.C1FlexGrid
Public Class frmMulipleSellingPrice

    Private _selectedPrice As C1.Win.C1FlexGrid.Row
    Public Property SelectedPrice As C1.Win.C1FlexGrid.Row
        Get
            Return _selectedPrice
        End Get
        Set(ByVal value As C1.Win.C1FlexGrid.Row)
            _selectedPrice = value
        End Set
    End Property

    Private _multipleMrpItems As DataTable
    Public Property MultipleMrpItems As DataTable
        Get
            Return _multipleMrpItems
        End Get
        Set(ByVal value As DataTable)
            _multipleMrpItems = value
        End Set
    End Property
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub frmMulipleSellingPrice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            articleGrid.DataSource = MultipleMrpItems
            GridSettings()
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridSettings()
        If articleGrid.Cols.Count > 0 Then
            articleGrid.Cols("ArticleCode").Width = 150
            articleGrid.Cols("ArticleShortName").Width = 200
            articleGrid.Cols("SellingPrice").Width = 75
            articleGrid.Cols("ArticleCode").AllowEditing = False
            articleGrid.Cols("ArticleShortName").AllowEditing = False
            articleGrid.Cols("SellingPrice").AllowEditing = False
        End If
    End Sub

    Private Sub okBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles okBtn.Click
        SelectedPrice = articleGrid.Rows(articleGrid.Row)
        Me.Close()
    End Sub

    Private Sub cancelBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cancelBtn.Click
        SelectedPrice = Nothing
        Me.Close()
    End Sub

    Private Sub articleGrid_DoubleCLick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles articleGrid.DoubleClick
        SelectedPrice = articleGrid.Rows(articleGrid.Row)
        Me.Close()
    End Sub
End Class