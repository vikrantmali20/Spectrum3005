Imports SpectrumCommon
Imports SpectrumBL
Imports System.Collections
Imports System.ComponentModel
Imports System.Linq
Imports NPOI.HSSF.UserModel
Imports NPOI.HPSF
Imports System.IO
Imports NPOI.SS.UserModel
Public Class CtrlRawMaterialClosing
    Public _objClsDayClose As New clsDayClose
    Private _DayCloseScreenList As IList(Of DayCloseScreenConfig)
    Private Property DayCloseScreenList As IList(Of DayCloseScreenConfig)
        Get
            Return _DayCloseScreenList
        End Get
        Set(ByVal value As IList(Of DayCloseScreenConfig))
            _DayCloseScreenList = value
        End Set
    End Property
    Private _RawMaterialDetailsList As BindingList(Of RawMaterialDetails)
    Public Property RawMaterialDetailsList() As BindingList(Of RawMaterialDetails)
        Get
            Return _RawMaterialDetailsList
        End Get
        Set(ByVal value As BindingList(Of RawMaterialDetails))
            _RawMaterialDetailsList = value
        End Set
    End Property


    Private _GridSource As BindingList(Of RawMaterialDetails)
    Public Property GridSource As BindingList(Of RawMaterialDetails)
        Get
            Return _GridSource
        End Get
        Set(ByVal value As BindingList(Of RawMaterialDetails))
            _GridSource = value
        End Set
    End Property
    Private _Instance As IRawMaterial(Of RawMaterialDetails)
    Public ReadOnly Property Instance As IRawMaterial(Of RawMaterialDetails)
        Get
            If _Instance Is Nothing Then
                _Instance = New DayCloseRawMaterialDetails()
            End If
            Return _Instance
        End Get
    End Property

    Private Sub CtrlRawMaterialClosing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.CheckForIllegalCrossThreadCalls = False
            Me.CheckForIllegalCrossThreadCalls = False
            dgRawMaterialClosing.Columns(0).Visible = False
            dgRawMaterialClosing.AutoGenerateColumns = False
            CtrlPagination1.PageChanged = AddressOf PageChangedHandler
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Function IsTotalDataValid() As Boolean
        Try
            Dim _isValid As Boolean
            _isValid = RawMaterialDetailsList.Count > 0
            If _isValid Then
                _isValid = Not (RawMaterialDetailsList.Any(Function(rawMaterial) (rawMaterial.EnteredQty Is Nothing)))
            End If
            If _isValid Then
                Dim qty As Decimal
                _isValid = Not (RawMaterialDetailsList.Any(Function(rawMaterial) (Decimal.TryParse(rawMaterial.EnteredQty, qty) = False OrElse rawMaterial.EnteredQty < 0)))
            End If

            If _isValid = False AndAlso RawMaterialDetailsList.Count > 0 Then
                ShowMessage(getValueByKey("dayclosevalidDataMsg"), getValueByKey("CLAE04"))
            End If
            Return _isValid
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Public Sub GetDayCloseData(ByRef request As DayCloseDataRequestModel(Of RawMaterialDetails))
        Try
            RawMaterialDetailsList = Instance.CheckIfDataExist(request)
            If RawMaterialDetailsList Is Nothing OrElse RawMaterialDetailsList.Count = 0 Then
                'If RawMaterialDetailsList Is Nothing OrElse RawMaterialDetailsList.Count = 0 Then
                'RawMaterialDetailsList = New BindingList(Of RawMaterialDetails)()
                RawMaterialDetailsList = Instance.GetDayCloseData(request, clsDefaultConfiguration.AutoPopulateQtyForAllDayCloseScreens)
                'End If
            End If
            CtrlPagination1.CalculateTotalPages(RawMaterialDetailsList.Count)
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

    Private Function PageChangedHandler(ByVal pageNumberData As PaginationData) As Boolean
        Try
            Dim result As Boolean
            If IsValid() AndAlso SaveData() Then
                GridSource = RawMaterialDetailsList.Where(Function(emp, index) index >= pageNumberData.StartNumber - 1 And index <= pageNumberData.EndNumber - 1).ToBindingList()
                RefreshComboSource()
                result = True
            End If
            Return result
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Sub RefreshComboSource()
        Try
            dgRawMaterialClosing.DataSource = GridSource
            For i As Integer = 0 To GridSource.Count - 1 Step 1
                'Dim cmbUOMCell As DataGridViewComboBoxCell = dgRawMaterialClosing.Rows(i).Cells("StockTakeUOMCode")
                Dim cmbUOMCell As DataGridViewComboBoxCell = dgRawMaterialClosing.Rows(i).Cells(3)
                cmbUOMCell.DataSource = GridSource(i).UOMData
                cmbUOMCell.DisplayMember = "UOMName"
                cmbUOMCell.ValueMember = "UOMCode"
            Next

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function SaveData() As Boolean
        Try
            If GridSource Is Nothing OrElse GridSource.Count = 0 Then
                Return True
            End If
            Dim result As Boolean = False
            Dim dayCloseSaveDataReq As New DayCloseDataRequestModel(Of RawMaterialDetails)

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

    Private Function Themechange()
        dgRawMaterialClosing.BackgroundColor = Color.White
        dgRawMaterialClosing.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(177, 227, 253)
        dgRawMaterialClosing.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgRawMaterialClosing.DefaultCellStyle.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgRawMaterialClosing.DefaultCellStyle.SelectionBackColor = Color.LightBlue
        CtrlPagination1.BackColor = Color.FromArgb(134, 134, 134)
        'Panel1.BackColor = Color.FromArgb(134, 134, 134)
        Me.BackColor = Color.FromArgb(134, 134, 134)
        TableLayoutPanel1.BackColor = Color.FromArgb(134, 134, 134)
        'Label1.BackColor = Color.FromArgb(212, 212, 212)
        Return ""
    End Function
End Class
