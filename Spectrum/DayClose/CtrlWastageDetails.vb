Imports SpectrumCommon
Imports SpectrumBL
Imports System.Collections
Imports System.ComponentModel
Imports System.Linq
Public Class CtrlWastageDetails
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private _WastageDetailsList As BindingList(Of WastageDetails)
    Public Property WastageDetailsList As BindingList(Of WastageDetails)
        Get
            If _WastageDetailsList Is Nothing Then
                _WastageDetailsList = New BindingList(Of WastageDetails)
            End If
            Return _WastageDetailsList
        End Get
        Set(ByVal value As BindingList(Of WastageDetails))
            _WastageDetailsList = value
        End Set
    End Property

    Private _GridSource As BindingList(Of WastageDetails)
    Public Property GridSource As BindingList(Of WastageDetails)
        Get
            Return _GridSource
        End Get
        Set(ByVal value As BindingList(Of WastageDetails))
            _GridSource = value
        End Set
    End Property

    Private _Instance As IStockTake(Of WastageDetails)
    Public ReadOnly Property Instance As IStockTake(Of WastageDetails)
        Get
            If _Instance Is Nothing Then
                _Instance = New DayCloseWastageDetails()
            End If
            Return _Instance
        End Get
    End Property

    Private Sub CtrlWastageDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.CheckForIllegalCrossThreadCalls = False
            dgWastageDetails.Columns("ArticleName").DefaultCellStyle.BackColor = Drawing.Color.LightGray

            dgWastageDetails.AutoGenerateColumns = False
            CtrlPagination1.PageChanged = AddressOf PageChangedHandler
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub CtrlWastageDetails_Closing()
        If clsDefaultConfiguration.RenderGrievance = False Then
            Dim result As Boolean = False
            Dim dayCloseSaveDataReq As New DayCloseDataRequestModel(Of WastageDetails)
            dayCloseSaveDataReq.DayCloseDate = clsAdmin.DayOpenDate
            dayCloseSaveDataReq.SiteCode = clsAdmin.SiteCode
            dayCloseSaveDataReq.UserId = clsAdmin.UserCode
            dayCloseSaveDataReq.DayCloseData = WastageDetailsList
            result = Instance.SaveDayCloseData(dayCloseSaveDataReq)
        End If
       
    End Sub


    Public Sub GetDayCloseData(ByRef request As DayCloseDataRequestModel(Of WastageDetails))
        Try
            If WastageDetailsList Is Nothing OrElse WastageDetailsList.Count = 0 Then
                WastageDetailsList = Instance.GetDayCloseData(request, clsDefaultConfiguration.AutoPopulateQtyForAllDayCloseScreens)
                CtrlPagination1.CalculateTotalPages(WastageDetailsList.Count)
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
            Dim _NotNothingWastageQty As Boolean = False
            _isValid = Not (WastageDetailsList.Any(Function(subProduct) (subProduct.EnteredQty Is Nothing)))

            'code added for jk sprint 28
            If _isValid = False Then

                If Not dgWastageDetails Is Nothing AndAlso dgWastageDetails.RowCount > 0 Then
                    For i = 0 To dgWastageDetails.Rows.Count - 1

                        If Not (dgWastageDetails.Rows(i).Cells(3).Value Is Nothing) Then
                            _NotNothingWastageQty = True

                        End If

                    Next
                End If

            End If
            If _isValid Then
                Dim qty As Decimal
                _isValid = Not (WastageDetailsList.Any(Function(subProduct) (Decimal.TryParse(subProduct.EnteredQty, qty) = False OrElse subProduct.EnteredQty < 0)))
            End If

            If _isValid = False AndAlso _NotNothingWastageQty = True Then
                _isValid = True

            End If

            If _isValid = False Then
                '  ShowMessage(getValueByKey("dayclosevalidDataMsg"), getValueByKey("CLAE04"))
                ShowMessage(True, "Please enter atleast 1 value", getValueByKey("CLAE04"))
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
                GridSource = WastageDetailsList.Where(Function(emp, index) index >= pageNumberData.StartNumber - 1 And index <= pageNumberData.EndNumber - 1).ToBindingList()
                dgWastageDetails.DataSource = GridSource
                For i As Integer = 0 To GridSource.Count - 1 Step 1
                    Dim cmbUOMCell As DataGridViewComboBoxCell = dgWastageDetails.Rows(i).Cells("WastageUOMCode")
                    cmbUOMCell.DataSource = GridSource(i).UOMData
                    cmbUOMCell.DisplayMember = "UOMName"
                    cmbUOMCell.ValueMember = "UOMCode"

                    Dim reasonUOMCell As DataGridViewComboBoxCell = dgWastageDetails.Rows(i).Cells("ReasonCode")
                    reasonUOMCell.DataSource = GridSource(i).ReasonData
                    reasonUOMCell.DisplayMember = "ReasonName"
                    reasonUOMCell.ValueMember = "ReasonCode"
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
            Dim dayCloseSaveDataReq As New DayCloseDataRequestModel(Of WastageDetails)
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

    Private Sub dgWastageDetails_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgWastageDetails.CellBeginEdit
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub

    Private Sub DataErrorEvent(ByVal sender As System.Object, ByVal e As DataGridViewDataErrorEventArgs) Handles dgWastageDetails.DataError

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Not GridSource.Any(Function(item) item.IsSelected = True) Then
                ShowMessage(getValueByKey("dayclosedeleteItemmsg"), getValueByKey("CLAE04"))
                Exit Sub
            End If
            Dim dayCloseSaveDataReq As New DayCloseDataRequestModel(Of WastageDetails)
            dayCloseSaveDataReq.DayCloseDate = clsAdmin.DayOpenDate
            dayCloseSaveDataReq.SiteCode = clsAdmin.SiteCode
            dayCloseSaveDataReq.UserId = clsAdmin.UserCode
            dayCloseSaveDataReq.DayCloseData = New List(Of WastageDetails)
            Dim insertIndex = WastageDetailsList.IndexOf(GridSource(0))
            For i As Int32 = GridSource.Count - 1 To 0 Step -1
                Dim Count As Int32 = i
                If GridSource(i).IsSelected Then
                    dayCloseSaveDataReq.DayCloseData.Add(GridSource(i))
                    WastageDetailsList.Remove(GridSource(i))
                End If
            Next
            Instance.DeleteStockTakeData(dayCloseSaveDataReq)
            CtrlPagination1.RecalulateTotalPages(WastageDetailsList.Count)
            If insertIndex > WastageDetailsList.Count - 1 Then
                insertIndex = insertIndex - DayCloseConstants.RecordsPerPage
            End If
            GridSource = WastageDetailsList.Where(Function(emp, index) index >= insertIndex And index < insertIndex + DayCloseConstants.RecordsPerPage).ToBindingList()
            RefreshComboSource()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnAddItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddItem.Click
        Try
            Dim obj As New frmDayCloseItemSearch
            obj.ShowDialog()
            If obj.ItemRows IsNot Nothing AndAlso obj.ItemRows.Count > 0 Then
                Dim newItemList As New BindingList(Of WastageDetails)
                Dim duplicateItemList As New List(Of WastageDetails)
                For Each row In obj.ItemRows
                    Dim newItem As New WastageDetails
                    newItem.ArticleCode = row("ArticleCode")
                    newItem.ArticleName = row("DISCRIPTION")
                    If Not WastageDetailsList.Any(Function(item) item.ArticleCode = newItem.ArticleCode) Then
                        newItemList.Add(newItem)
                    Else
                        duplicateItemList.Add(newItem)
                    End If
                Next
                DisplayDuplicateItemMessage(duplicateItemList)
                If newItemList.Count = 0 Then
                    Exit Sub
                End If
                Dim dayCloseSaveDataReq As New DayCloseDataRequestModel(Of WastageDetails)
                dayCloseSaveDataReq.DayCloseDate = clsAdmin.DayOpenDate
                dayCloseSaveDataReq.SiteCode = clsAdmin.SiteCode
                dayCloseSaveDataReq.UserId = clsAdmin.UserCode
                dayCloseSaveDataReq.DayCloseData = newItemList
                Instance.GetNewItemMasterData(dayCloseSaveDataReq, clsDefaultConfiguration.AutoPopulateQtyForAllDayCloseScreens)
                Dim insertIndex As Integer = 0
                If GridSource IsNot Nothing AndAlso GridSource.Count > 0 Then
                    insertIndex = WastageDetailsList.IndexOf(GridSource(0))
                End If
                For i As Int32 = newItemList.Count - 1 To 0 Step -1
                    Dim Count As Int32 = i
                    'If Not WastageDetailsList.Any(Function(item) item.ArticleCode = newItemList(Count).ArticleCode) Then
                    WastageDetailsList.Insert(insertIndex, newItemList(i))
                    'End If
                Next
                CtrlPagination1.RecalulateTotalPages(WastageDetailsList.Count)
                GridSource = WastageDetailsList.Where(Function(emp, index) index >= insertIndex And index < insertIndex + DayCloseConstants.RecordsPerPage).ToBindingList()
                RefreshComboSource()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub RefreshComboSource()
        Try
            dgWastageDetails.DataSource = GridSource
            For i As Integer = 0 To GridSource.Count - 1 Step 1
                Dim cmbUOMCell As DataGridViewComboBoxCell = dgWastageDetails.Rows(i).Cells("WastageUOMCode")
                cmbUOMCell.DataSource = GridSource(i).UOMData
                cmbUOMCell.DisplayMember = "UOMName"
                cmbUOMCell.ValueMember = "UOMCode"

                Dim reasonUOMCell As DataGridViewComboBoxCell = dgWastageDetails.Rows(i).Cells("ReasonCode")
                reasonUOMCell.DataSource = GridSource(i).ReasonData
                reasonUOMCell.DisplayMember = "ReasonName"
                reasonUOMCell.ValueMember = "ReasonCode"
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub DisplayDuplicateItemMessage(ByRef duplicateItemList As List(Of WastageDetails))
        Try
            If duplicateItemList IsNot Nothing Then
                Dim message As String = String.Empty
                For Each item In duplicateItemList
                    message += item.ArticleName & ", "
                Next
                If message.Length > 0 Then
                    message = message.Remove(message.Count - 2, 2)
                    message += " already Exist."
                    ShowMessage(message, getValueByKey("CLAE04"))
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub EnableReadOnlyMode()
        Try
            dgWastageDetails.ReadOnly = True
            btnAddItem.Enabled = False
            btnDelete.Enabled = False
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function Themechange()
        Me.BackColor = Color.FromArgb(134, 134, 134)
        TableLayoutPanel1.BackColor = Color.FromArgb(134, 134, 134)
        Panel1.BackColor = Color.FromArgb(134, 134, 134)
        CtrlPagination1.BackColor = Color.FromArgb(134, 134, 134)
        dgWastageDetails.BackgroundColor = Color.White
        dgWastageDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(177, 227, 253)
        dgWastageDetails.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgWastageDetails.DefaultCellStyle.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgWastageDetails.DefaultCellStyle.SelectionBackColor = Color.LightBlue
        btnAddItem.Location = New Point(84, 2)
        btnAddItem.BackColor = Color.FromArgb(0, 107, 163)
        btnAddItem.ForeColor = Color.FromArgb(255, 255, 255)
        btnAddItem.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnAddItem.FlatStyle = FlatStyle.Flat
        btnAddItem.FlatAppearance.BorderSize = 0
        btnAddItem.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnAddItem.Size = New Size(75, 26)

        btnDelete.Location = New Point(3, 2)
        btnDelete.BackColor = Color.FromArgb(0, 107, 163)
        btnDelete.ForeColor = Color.FromArgb(255, 255, 255)
        btnDelete.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnDelete.FlatStyle = FlatStyle.Flat
        btnDelete.FlatAppearance.BorderSize = 0
        btnDelete.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnDelete.Size = New Size(75, 26)
    End Function
End Class
