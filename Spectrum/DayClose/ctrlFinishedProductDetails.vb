Imports SpectrumCommon
Imports SpectrumBL
Imports System.Collections
Imports System.ComponentModel
Imports System.Linq
Public Class ctrlFinishedProductDetails
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private _FinishedProductList As BindingList(Of FinishedProductDetails)
    Public Property FinishedProductList As BindingList(Of FinishedProductDetails)
        Get
            If _FinishedProductList Is Nothing Then
                _FinishedProductList = New BindingList(Of FinishedProductDetails)
            End If
            Return _FinishedProductList
        End Get
        Set(ByVal value As BindingList(Of FinishedProductDetails))
            _FinishedProductList = value
        End Set
    End Property

    Private _GridSource As BindingList(Of FinishedProductDetails)
    Public Property GridSource As BindingList(Of FinishedProductDetails)
        Get
            Return _GridSource
        End Get
        Set(ByVal value As BindingList(Of FinishedProductDetails))
            _GridSource = value
        End Set
    End Property

    Private _Instance As IDayCloseScreens(Of FinishedProductDetails)
    Public ReadOnly Property Instance As IDayCloseScreens(Of FinishedProductDetails)
        Get
            If _Instance Is Nothing Then
                _Instance = New DayCloseFinishedProductDetails()
            End If
            Return _Instance
        End Get
    End Property

    Private Sub ctrlFinishedProductDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            dgFinishedProductDetails.Columns("ArticleName").DefaultCellStyle.BackColor = Drawing.Color.LightGray
            dgFinishedProductDetails.AutoGenerateColumns = False          
            CtrlPagination1.PageChanged = AddressOf PageChangedHandler
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub GetDayCloseData(ByRef request As DayCloseDataRequestModel(Of FinishedProductDetails))
        Try
            If FinishedProductList Is Nothing OrElse FinishedProductList.Count = 0 Then
                FinishedProductList = Instance.GetDayCloseData(request, clsDefaultConfiguration.AutoPopulateQtyForAllDayCloseScreens)
                CtrlPagination1.CalculateTotalPages(FinishedProductList.Count)
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
            _isValid = Not (GridSource.Any(Function(subProduct) (subProduct.EnteredQty Is Nothing)))
            If _isValid Then
                Dim qty As Decimal          
                _isValid = Not (GridSource.Any(Function(subProduct) (Decimal.TryParse(subProduct.EnteredQty, qty) = False OrElse subProduct.EnteredQty < 0)))
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

    Public Function IsTotalDataValid() As Boolean
        Try
            Dim _isValid As Boolean
            _isValid = Not (FinishedProductList.Any(Function(subProduct) (subProduct.EnteredQty Is Nothing)))
            If _isValid Then
                Dim qty As Decimal
                _isValid = Not (FinishedProductList.Any(Function(subProduct) (Decimal.TryParse(subProduct.EnteredQty, qty) = False OrElse subProduct.EnteredQty < 0)))
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
                dgFinishedProductDetails.DataSource = GridSource
                GridSource = FinishedProductList.Where(Function(emp, index) index >= pageNumberData.StartNumber - 1 And index <= pageNumberData.EndNumber - 1).ToBindingList()
                dgFinishedProductDetails.DataSource = GridSource
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
            Dim dayCloseSaveDataReq As New DayCloseDataRequestModel(Of FinishedProductDetails)
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

    Private Sub dgFinishedProductDetails_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgFinishedProductDetails.CellBeginEdit
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub

    Private Sub DataErrorEvent(ByVal sender As System.Object, ByVal e As DataGridViewDataErrorEventArgs) Handles dgFinishedProductDetails.DataError

    End Sub

    Public Sub EnableReadOnlyMode()
        Try
            dgFinishedProductDetails.ReadOnly = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function Themechange()
        Me.BackColor = Color.FromArgb(134, 134, 134)
        TableLayoutPanel1.BackColor = Color.FromArgb(134, 134, 134)
        dgFinishedProductDetails.BackgroundColor = Color.White
        dgFinishedProductDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(177, 227, 253)
        dgFinishedProductDetails.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgFinishedProductDetails.DefaultCellStyle.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgFinishedProductDetails.DefaultCellStyle.SelectionBackColor = Color.LightBlue
        CtrlPagination1.BackColor = Color.FromArgb(134, 134, 134)

    End Function
End Class
