Imports SpectrumCommon
Imports SpectrumBL
Imports System.Collections
Imports System.ComponentModel
Imports System.Linq

Public Class SubProductDetails
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        'dgSubProductDetails.AutoGenerateColumns = False
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private _SubProductList As BindingList(Of SpectrumCommon.SubProductDetails)
    Public Property SubProductList As BindingList(Of SpectrumCommon.SubProductDetails)
        Get
            If _SubProductList Is Nothing Then
                _SubProductList = New BindingList(Of SpectrumCommon.SubProductDetails)
            End If
            Return _SubProductList
        End Get
        Set(ByVal value As BindingList(Of SpectrumCommon.SubProductDetails))
            _SubProductList = value
        End Set
    End Property

    Private _GridSource As BindingList(Of SpectrumCommon.SubProductDetails)
    Public Property GridSource As BindingList(Of SpectrumCommon.SubProductDetails)
        Get
            Return _GridSource
        End Get
        Set(ByVal value As BindingList(Of SpectrumCommon.SubProductDetails))
            _GridSource = value
        End Set
    End Property

    Private _Instance As IDayCloseScreens(Of SpectrumCommon.SubProductDetails)
    Public ReadOnly Property Instance As IDayCloseScreens(Of SpectrumCommon.SubProductDetails)
        Get
            If _Instance Is Nothing Then
                _Instance = New DayCloseSubProductDetails()
            End If
            Return _Instance
        End Get
    End Property

    Private Sub SubProductDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        Try            
            dgSubProductDetails.Columns("ArticleBaseUOM").DefaultCellStyle.BackColor = Drawing.Color.LightGray
            dgSubProductDetails.Columns("CalculatedQty").DefaultCellStyle.BackColor = Drawing.Color.LightGray
            dgSubProductDetails.Columns("ArticleName").DefaultCellStyle.BackColor = Drawing.Color.LightGray
            dgSubProductDetails.Columns("CalculatedQty").Visible = False
            dgSubProductDetails.AutoGenerateColumns = False          
            CtrlPagination1.PageChanged = AddressOf PageChangedHandler

            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub GetDayCloseData(ByRef request As DayCloseDataRequestModel(Of SpectrumCommon.SubProductDetails))
        Try
            If SubProductList Is Nothing OrElse SubProductList.Count = 0 Then
                SubProductList = Instance.GetDayCloseData(request, clsDefaultConfiguration.AutoPopulateQtyForAllDayCloseScreens)
                'dgSubProductDetails.DataSource = SubProductList  ArticleName
                'For i As Integer = 0 To SubProductList.Count - 1 Step 1
                '    Dim cmbRecipeCell As DataGridViewComboBoxCell = dgSubProductDetails.Rows(i).Cells("RecipeCode")
                '    cmbRecipeCell.DataSource = SubProductList(i).RecipeData
                '    cmbRecipeCell.DisplayMember = "RecipeName"
                '    cmbRecipeCell.ValueMember = "RecipeCode"
                'Next
                CtrlPagination1.CalculateTotalPages(SubProductList.Count)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Function IsValid() As Boolean
        Try
            If GridSource Is Nothing OrElse GridSource.Count = 0 Then
                Return True
            End If
            Dim _isValid As Boolean
            _isValid = Not (GridSource.Any(Function(subProduct) (subProduct.EnteredQty Is Nothing Or subProduct.BatchCount Is Nothing)))
            If _isValid Then
                Dim qty As Decimal
                Dim batchCount As Decimal
                _isValid = Not (GridSource.Any(Function(subProduct) (Decimal.TryParse(subProduct.EnteredQty, qty) = False OrElse Decimal.TryParse(subProduct.BatchCount, batchCount) = False OrElse subProduct.EnteredQty < 0 OrElse subProduct.BatchCount < 0)))
            End If
            If _isValid = False Then
                ShowMessage(getValueByKey("dayclosevalidDataMsg"), getValueByKey("CLAE04"))
            Else

            End If
            Return _isValid
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function IsTotalDataValid() As Boolean
        Try
            Dim _isValid As Boolean
            _isValid = Not (SubProductList.Any(Function(subProduct) (subProduct.EnteredQty Is Nothing Or subProduct.BatchCount Is Nothing)))
            If _isValid Then             
                Dim qty As Decimal
                Dim batchCount As Decimal
                _isValid = Not (SubProductList.Any(Function(subProduct) (Decimal.TryParse(subProduct.EnteredQty, qty) = False OrElse Decimal.TryParse(subProduct.BatchCount, batchCount) = False OrElse subProduct.EnteredQty < 0 OrElse subProduct.BatchCount < 0)))
            End If
            If _isValid = False Then
                ShowMessage(getValueByKey("dayclosevalidDataMsg"), getValueByKey("CLAE04"))
            End If
            Return _isValid
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Function PageChangedHandler(ByVal pageNumberData As PaginationData) As Boolean
        Try
            Dim result As Boolean
            If IsValid() AndAlso SaveData() Then
                GridSource = SubProductList.Where(Function(emp, index) index >= pageNumberData.StartNumber - 1 And index <= pageNumberData.EndNumber - 1).ToBindingList()
                dgSubProductDetails.DataSource = GridSource
                For i As Integer = 0 To GridSource.Count - 1 Step 1
                    Dim cmbRecipeCell As DataGridViewComboBoxCell = dgSubProductDetails.Rows(i).Cells("RecipeCode")
                    cmbRecipeCell.DataSource = GridSource(i).RecipeData
                    cmbRecipeCell.DisplayMember = "RecipeName"
                    cmbRecipeCell.ValueMember = "RecipeCode"
                Next
                result = True
            End If
            Return result
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            If GridSource Is Nothing OrElse GridSource.Count = 0 Then
                Return True
            End If
            Dim result As Boolean = False
            Dim dayCloseSaveDataReq As New DayCloseDataRequestModel(Of SpectrumCommon.SubProductDetails)
            dayCloseSaveDataReq.DayCloseDate = clsAdmin.DayOpenDate
            dayCloseSaveDataReq.SiteCode = clsAdmin.SiteCode
            dayCloseSaveDataReq.UserId = clsAdmin.UserCode
            dayCloseSaveDataReq.DayCloseData = GridSource
            result = Instance.SaveDayCloseData(dayCloseSaveDataReq)
            Return result
        Catch ex As Exception
            LogException(ex)
            ShowMessage(getValueByKey("daycloseerrormsg"), getValueByKey("CLAE05"))
            Return False
        End Try
    End Function

    Private Sub dgSubProductDetails_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgSubProductDetails.CellBeginEdit
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub

    Private Sub DataErrorEvent(ByVal sender As System.Object, ByVal e As DataGridViewDataErrorEventArgs) Handles dgSubProductDetails.DataError

    End Sub

    Public Sub EnableReadOnlyMode()
        Try
            dgSubProductDetails.ReadOnly = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function Themechange()
        Me.BackColor = Color.FromArgb(134, 134, 134)
        TableLayoutPanel1.BackColor = Color.FromArgb(134, 134, 134)
        CtrlPagination1.BackColor = Color.FromArgb(134, 134, 134)
        dgSubProductDetails.BackgroundColor = Color.White
        dgSubProductDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(177, 227, 253)
        dgSubProductDetails.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgSubProductDetails.DefaultCellStyle.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgSubProductDetails.DefaultCellStyle.SelectionBackColor = Color.LightBlue
    End Function
End Class
