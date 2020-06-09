Imports SpectrumCommon
Imports SpectrumBL
Imports System.Collections
Imports System.ComponentModel
Imports System.Linq
Imports NPOI.HSSF.UserModel
Imports NPOI.HPSF
Imports System.IO
Imports NPOI.SS.UserModel

Public Class CtrlStockTakeDetails
    Private Const ExportDirectoryPath As String = "C:\"
    Private Const ExportFileName As String = "StockTakeDetails.xls"
    Dim stocktakedata As String = String.Empty
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private _StockTakeList As BindingList(Of StockTakeDetails)
    Public Property StockTakeList As BindingList(Of StockTakeDetails)
        Get
            If _StockTakeList Is Nothing Then
                _StockTakeList = New BindingList(Of StockTakeDetails)
            End If
            Return _StockTakeList
        End Get
        Set(ByVal value As BindingList(Of StockTakeDetails))
            _StockTakeList = value
        End Set
    End Property

    Private _GridSource As BindingList(Of StockTakeDetails)
    Public Property GridSource As BindingList(Of StockTakeDetails)
        Get
            Return _GridSource
        End Get
        Set(ByVal value As BindingList(Of StockTakeDetails))
            _GridSource = value
        End Set
    End Property

    Private _StockGroups As BindingList(Of ArticleGroupDetails)
    Public Property StockGroups As BindingList(Of ArticleGroupDetails)
        Get
            Return _StockGroups
        End Get
        Set(ByVal value As BindingList(Of ArticleGroupDetails))
            _StockGroups = value
        End Set
    End Property

    Private _Instance As IStockTake(Of StockTakeDetails)
    Public ReadOnly Property Instance As IStockTake(Of StockTakeDetails)
        Get
            If _Instance Is Nothing Then
                '_Instance = New DayCloseStockTakeDetails()
                _Instance = New DayCloseStockTakeDetails(FlagHideControlsFromStockTake:=clsDefaultConfiguration.HideControlsFromStockTake)
            End If
            Return _Instance
        End Get
    End Property

    Private Sub CtrlStockTakeDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.CheckForIllegalCrossThreadCalls = False
            dgStockTakeDetails.Columns("ArticleName").DefaultCellStyle.BackColor = Drawing.Color.LightGray
            dgStockTakeDetails.Columns("ArticleCode").DefaultCellStyle.BackColor = Drawing.Color.LightGray
            dgStockTakeDetails.AutoGenerateColumns = False
            CtrlPagination1.PageChanged = AddressOf PageChangedHandler
            AddHandler dgStockTakeDetails.CellEndEdit, AddressOf dgStockTakeDetails_CellEndEdit
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            If clsDefaultConfiguration.HideControlsFromStockTake Then
                btnDelete.Visible = False
                btnExport.Visible = False
                btnImport.Visible = False
                btnAddItem.Visible = False
                btnRawMaterialClosing.Visible = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub CtrlStockTakeDetails_Closing()
        Try
            If clsDefaultConfiguration.RenderGrievance = False Then
                Dim objclsCommon As New clsCommon
                Dim Status As Boolean = objclsCommon.GetDayOpenOrClose(clsAdmin.DayOpenDate, clsAdmin.SiteCode)
                Dim result As Boolean = False
                If Status = False Then
                    Dim dayCloseSaveDataReq As New DayCloseDataRequestModel(Of StockTakeDetails)
                    dayCloseSaveDataReq.DayCloseDate = clsAdmin.DayOpenDate
                    dayCloseSaveDataReq.SiteCode = clsAdmin.SiteCode
                    dayCloseSaveDataReq.UserId = clsAdmin.UserCode
                    dayCloseSaveDataReq.DayCloseData = StockTakeList
                    'code commneted by vipul for issue id 2496
                    Instance.ClearStockTakeData(dayCloseSaveDataReq)
                    result = Instance.SaveDayCloseData(dayCloseSaveDataReq)
                Else
                    result = True
                End If
                If result = True Then
                    stocktakedata = String.Empty
                    stocktakedata = LogForStokeTake(StockTakeList)
                    writestokeTakeLog(vbNewLine + stocktakedata + vbNewLine + "data save successfully on closing of stock take form ")

                End If
            End If
       
        Catch ex As Exception
            LogException(ex)
            ShowMessage(getValueByKey("daycloseerrormsg"), getValueByKey("CLAE05"))
        End Try
    End Sub

    Public Sub GetDayCloseData(ByRef request As DayCloseDataRequestModel(Of StockTakeDetails))
        Try
            If StockTakeList Is Nothing OrElse StockTakeList.Count = 0 Then
                StockTakeList = Instance.CheckIfDataExist(request)
                'Added by Khusrao Adil
                ' for jk
                'while first attempt of current day close

                If StockTakeList.Count = 0 Then
                    StockTakeList = Instance.GetDayCloseData(request, clsDefaultConfiguration.AutoPopulateQtyForAllDayCloseScreens)
                End If

                CtrlPagination1.CalculateTotalPages(StockTakeList.Count)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub GetStockGroupDetails()
        Try
            If StockGroups Is Nothing OrElse StockGroups.Count = 0 Then
                StockGroups = Instance.GetStockGroups(ArticleGroupType.DS)
                cmbGroup.DataSource = StockGroups
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
            _isValid = StockTakeList.Count > 0
            If _isValid Then
                _isValid = Not (StockTakeList.Any(Function(subProduct) (subProduct.EnteredQty Is Nothing)))
            End If
            If _isValid Then
                Dim qty As Decimal
                _isValid = Not (StockTakeList.Any(Function(subProduct) (Decimal.TryParse(subProduct.EnteredQty, qty) = False OrElse subProduct.EnteredQty < 0)))
            End If

            If _isValid = False AndAlso StockTakeList.Count > 0 Then
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
                GridSource = StockTakeList.Where(Function(emp, index) index >= pageNumberData.StartNumber - 1 And index <= pageNumberData.EndNumber - 1).ToBindingList()
                RefreshComboSource()
                result = True
            End If
            Return result
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            If GridSource Is Nothing OrElse GridSource.Count = 0 Then
                Return True
            End If
            Dim result As Boolean = False
            Dim dayCloseSaveDataReq As New DayCloseDataRequestModel(Of StockTakeDetails)
            dayCloseSaveDataReq.DayCloseDate = clsAdmin.DayOpenDate
            dayCloseSaveDataReq.SiteCode = clsAdmin.SiteCode
            dayCloseSaveDataReq.UserId = clsAdmin.UserCode
            dayCloseSaveDataReq.DayCloseData = GridSource
            'code commneted by vipul for issue id 2496
            ' Instance.ClearStockTakeData(dayCloseSaveDataReq)
            result = Instance.SaveDayCloseData(dayCloseSaveDataReq)
            Return result
        Catch ex As Exception
            LogException(ex)
            ShowMessage(getValueByKey("daycloseerrormsg"), getValueByKey("CLAE05"))
            Return False
        End Try
    End Function

    Private Sub dgStockTakeDetails_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgStockTakeDetails.CellBeginEdit
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub

    Private Sub DataErrorEvent(ByVal sender As System.Object, ByVal e As DataGridViewDataErrorEventArgs) Handles dgStockTakeDetails.DataError

    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Try
            Dim res = MessageBox.Show(getValueByKey("DayCloseValidationMsg02"), getValueByKey("CLAE04"), MessageBoxButtons.OKCancel)
            If res = DialogResult.Cancel Then
                If GridSource.Count > 0 Then
                    cmbGroup.SelectedIndex = StockGroups.IndexOf(StockGroups.Where(Function(w) w.GroupId = GridSource.FirstOrDefault().GroupCode).FirstOrDefault())
                End If
                Exit Sub
            End If
            If cmbGroup.SelectedValue IsNot Nothing Then
                Dim query As String = cmbGroup.SelectedValue
                If clsDefaultConfiguration.HideControlsFromStockTake Then

                    StockTakeList = Instance.CheckIfDataExist(New DayCloseDataRequestModel(Of StockTakeDetails) With {.DayCloseDate = clsAdmin.DayOpenDate, .Query = query, .SiteCode = clsAdmin.SiteCode})

                    Dim stockTakeArticle As BindingList(Of StockTakeDetails)
                    stockTakeArticle = New BindingList(Of StockTakeDetails)

                    stockTakeArticle = Instance.GetDayCloseData(New DayCloseDataRequestModel(Of StockTakeDetails) With {.DayCloseDate = clsAdmin.DayOpenDate, .Query = query, .SiteCode = clsAdmin.SiteCode}, clsDefaultConfiguration.AutoPopulateQtyForAllDayCloseScreens)
                    For Each item As StockTakeDetails In stockTakeArticle


                        Dim IsPresent As Boolean = False
                        For Each mainList As StockTakeDetails In StockTakeList
                            Dim tempArticleCode As String = ""
                            tempArticleCode = mainList.ArticleCode
                            If item.ArticleCode = tempArticleCode Then
                                IsPresent = True
                            End If
                        Next
                        If IsPresent = False Then
                            StockTakeList.Add(item)
                        End If
                    Next

                    Try
                        '   StockTakeList.OrderBy(Function(a) a.ArticleName).ToList()
                        ' StockTakeList.ToList.Sort(Function(x, y) x.ArticleName.CompareTo(y.ArticleName))
                        Dim tempStockTakeDetails As New BindingList(Of StockTakeDetails)
                        Dim tempstr As New ArrayList
                        For Each templist As StockTakeDetails In StockTakeList
                            tempstr.Add(templist.ArticleName)
                        Next
                        tempstr.Sort()
                        For Each Str As String In tempstr
                            For Each mainList As StockTakeDetails In StockTakeList
                                If Str = mainList.ArticleName Then
                                    tempStockTakeDetails.Add(mainList)
                                    Exit For
                                End If
                            Next
                        Next

                        StockTakeList = tempStockTakeDetails

                    Catch ex As Exception
                        LogException(ex)
                    End Try


                Else
                    StockTakeList = Instance.GetDayCloseData(New DayCloseDataRequestModel(Of StockTakeDetails) With {.DayCloseDate = clsAdmin.DayOpenDate, .Query = query, .SiteCode = clsAdmin.SiteCode}, clsDefaultConfiguration.AutoPopulateQtyForAllDayCloseScreens)
                End If
                'StockTakeList = Instance.GetDayCloseData(New DayCloseDataRequestModel(Of StockTakeDetails) With {.DayCloseDate = clsAdmin.DayOpenDate, .Query = query, .SiteCode = clsAdmin.SiteCode}, clsDefaultConfiguration.AutoPopulateQtyForAllDayCloseScreens)
                Dim insertIndex As Integer = 0
                If GridSource IsNot Nothing AndAlso GridSource.Count > 0 Then
                    insertIndex = StockTakeList.IndexOf(GridSource(0))
                End If

                'For i As Int32 = tempList.Count - 1 To 0 Step -1
                '    Dim Count As Int32 = i
                '    If Not StockTakeList.Any(Function(item) item.ArticleCode = tempList(Count).ArticleCode) Then
                '        StockTakeList.Insert(insertIndex, tempList(i))
                '    End If
                'Next
                CtrlPagination1.RecalulateTotalPages(StockTakeList.Count)
                GridSource = StockTakeList.Where(Function(emp, index) index >= insertIndex And index < insertIndex + DayCloseConstants.RecordsPerPage).ToBindingList()
                RefreshComboSource()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnAddItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddItem.Click
        Try
            Dim obj As New frmDayCloseItemSearch
            obj.ShowDialog()
            If obj.ItemRows IsNot Nothing AndAlso obj.ItemRows.Count > 0 Then
                Dim newItemList As New BindingList(Of StockTakeDetails)
                Dim duplicateItemList As New List(Of StockTakeDetails)
                For Each row In obj.ItemRows
                    Dim newItem As New StockTakeDetails
                    newItem.ArticleCode = row("ArticleCode")
                    newItem.ArticleName = row("DISCRIPTION")
                    newItem.GroupCode = DirectCast(cmbGroup.SelectedItem, ArticleGroupDetails).GroupId
                    'added by Khusrao Adil
                    ' set default quantity as zero
                    If clsDefaultConfiguration.AutoPopulateQtyForAllDayCloseScreens Then
                        newItem.EnteredQty = 0
                    End If
                    If Not StockTakeList.Any(Function(item) item.ArticleCode = newItem.ArticleCode) Then
                        newItemList.Add(newItem)
                    Else
                        duplicateItemList.Add(newItem)
                    End If
                Next
                DisplayDuplicateItemMessage(duplicateItemList)
                If newItemList.Count = 0 Then
                    Exit Sub
                End If
                AddNewItem(newItemList)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub AddNewItem(ByVal newItemList As BindingList(Of StockTakeDetails))
        Try
            If newItemList IsNot Nothing AndAlso newItemList.Count > 0 Then
                Dim dayCloseSaveDataReq As New DayCloseDataRequestModel(Of StockTakeDetails)
                dayCloseSaveDataReq.DayCloseDate = clsAdmin.DayOpenDate
                dayCloseSaveDataReq.SiteCode = clsAdmin.SiteCode
                dayCloseSaveDataReq.UserId = clsAdmin.UserCode
                dayCloseSaveDataReq.DayCloseData = newItemList
                Instance.GetNewItemMasterData(dayCloseSaveDataReq, clsDefaultConfiguration.AutoPopulateQtyForAllDayCloseScreens)
                Dim insertIndex As Integer = 0
                If GridSource IsNot Nothing AndAlso GridSource.Count > 0 Then
                    insertIndex = StockTakeList.IndexOf(GridSource(0))
                End If
                For i As Int32 = newItemList.Count - 1 To 0 Step -1
                    Dim Count As Int32 = i
                    If Not StockTakeList.Any(Function(item) item.ArticleCode = newItemList(Count).ArticleCode) Then
                        StockTakeList.Insert(insertIndex, newItemList(i))
                    End If
                Next
                CtrlPagination1.RecalulateTotalPages(StockTakeList.Count)
                GridSource = StockTakeList.Where(Function(emp, index) index >= insertIndex And index < insertIndex + DayCloseConstants.RecordsPerPage).ToBindingList()
                RefreshComboSource()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Not GridSource.Any(Function(item) item.IsSelected = True) Then
                ShowMessage(getValueByKey("dayclosedeleteItemmsg"), getValueByKey("CLAE04"))
                Exit Sub
            End If
            Dim dayCloseSaveDataReq As New DayCloseDataRequestModel(Of StockTakeDetails)
            dayCloseSaveDataReq.DayCloseDate = clsAdmin.DayOpenDate
            dayCloseSaveDataReq.SiteCode = clsAdmin.SiteCode
            dayCloseSaveDataReq.UserId = clsAdmin.UserCode
            dayCloseSaveDataReq.DayCloseData = New List(Of StockTakeDetails)
            Dim insertIndex = StockTakeList.IndexOf(GridSource(0))
            For i As Int32 = GridSource.Count - 1 To 0 Step -1
                Dim Count As Int32 = i
                If GridSource(i).IsSelected Then
                    dayCloseSaveDataReq.DayCloseData.Add(GridSource(i))
                    StockTakeList.Remove(GridSource(i))
                End If
            Next
            Instance.DeleteStockTakeData(dayCloseSaveDataReq)
            CtrlPagination1.RecalulateTotalPages(StockTakeList.Count)
            If insertIndex > StockTakeList.Count - 1 Then
                insertIndex = insertIndex - DayCloseConstants.RecordsPerPage
            End If
            GridSource = StockTakeList.Where(Function(emp, index) index >= insertIndex And index < insertIndex + DayCloseConstants.RecordsPerPage).ToBindingList()
            RefreshComboSource()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function ColorChange(ByVal i As Integer) As Color
        Dim Color As Color
        Select Case i
            Case 1
                Color = System.Drawing.Color.Pink
            Case 2
                Color = System.Drawing.Color.Yellow
            Case 3
                Color = System.Drawing.Color.LightBlue
            Case 4
                Color = System.Drawing.Color.YellowGreen
            Case 5
                Color = System.Drawing.Color.GreenYellow
            Case 6
                Color = System.Drawing.Color.LightCyan
            Case 7
                Color = System.Drawing.Color.LightGreen
            Case 8
                Color = System.Drawing.Color.Cyan
            Case 9
                Color = System.Drawing.Color.Thistle
            Case 10
                Color = System.Drawing.Color.Snow
            Case Else
                Color = System.Drawing.Color.Transparent
        End Select
        Return Color
    End Function
    Private Sub RefreshComboSource()
        Try
            dgStockTakeDetails.DataSource = GridSource
            For i As Integer = 0 To GridSource.Count - 1 Step 1
                Dim cmbUOMCell As DataGridViewComboBoxCell = dgStockTakeDetails.Rows(i).Cells("StockTakeUOMCode")
                cmbUOMCell.DataSource = GridSource(i).UOMData
                cmbUOMCell.DisplayMember = "UOMName"
                cmbUOMCell.ValueMember = "UOMCode"
            Next
            If GridSource.Count > 0 Then
                cmbGroup.SelectedIndex = StockGroups.IndexOf(StockGroups.Where(Function(w) w.GroupId = GridSource.FirstOrDefault().GroupCode).FirstOrDefault())
            End If
            'added By Khusrao Adil
            'for JK sprint 14 for article's color wise group
            If clsDefaultConfiguration.ClientForMail = "JK" Then
                If dgStockTakeDetails.RowCount > 0 Then
                    Dim MaxSubGroupId As Integer = dgStockTakeDetails.Rows(dgStockTakeDetails.RowCount - 1).Cells("SubGroupId").Value
                    dgStockTakeDetails.Refresh()
                    Dim ColorId As Integer = 1
                    For index = 1 To MaxSubGroupId
                        For i = dgStockTakeDetails.RowCount - 1 To 0 Step -1
                            Dim _random As New Random
                            Dim aColorId As Integer
                            If ColorId > 10 Then
                                aColorId = ColorId - 10
                            Else
                                aColorId = ColorId
                            End If
                            If dgStockTakeDetails.Rows(i).Cells("SubGroupId").Value = ColorId Then
                                Dim Color As Color = ColorChange(aColorId)
                                Me.dgStockTakeDetails.Rows(i).DefaultCellStyle.BackColor = Color
                            End If
                        Next
                        'If MaxSubGroupId = 1 Then
                        '    Exit Sub
                        'End If
                        ColorId = ColorId + 1
                        MaxSubGroupId = MaxSubGroupId - 1
                        'If ColorId > 10 Then
                        '    ColorId = 1
                        'End If
                    Next
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub DisplayDuplicateItemMessage(ByRef duplicateItemList As List(Of StockTakeDetails))
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
            dgStockTakeDetails.ReadOnly = True
            btnAddItem.Enabled = False
            btnDelete.Enabled = False
            btnSelect.Enabled = False
            btnExport.Enabled = False
            btnImport.Enabled = False
            btnRawMaterialClosing.Enabled = False
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            If StockTakeList Is Nothing OrElse StockTakeList.Count = 0 Then
                ShowMessage("No data to Export", getValueByKey("CLAE04"))
                Exit Sub
            End If
            Dim hssfworkbook As HSSFWorkbook = ExcelHelper.Instance.InitializeWorkbook()
            Dim sheet1 As ISheet = hssfworkbook.CreateSheet("StockTakeDetails")
            Dim lastRow As IRow = sheet1.CreateRow(0)
            lastRow.CreateCell(0).SetCellValue("ItemCode")
            lastRow.CreateCell(1).SetCellValue("ItemName")
            lastRow.CreateCell(2).SetCellValue("UOM")
            lastRow.CreateCell(3).SetCellValue("Quantity")
            ExcelHelper.Instance.SetCellStyle(lastRow, ExcelHelper.Instance.GetBoldFont(hssfworkbook))        
            Dim row As Integer = 1
            Dim col As Integer = 0
            lastRow = sheet1.CreateRow(row)
            For Each item As StockTakeDetails In StockTakeList
                lastRow.CreateCell(0).SetCellValue(item.ArticleCode)
                lastRow.CreateCell(1).SetCellValue(item.ArticleName)
                lastRow.CreateCell(2).SetCellValue(item.StockTakeUOMCode)
                If item.EnteredQty Is Nothing Then
                    lastRow.CreateCell(3).SetCellValue(String.Empty)
                Else
                    lastRow.CreateCell(3).SetCellValue(item.EnteredQty.Value)
                End If
                row += 1
                lastRow = sheet1.CreateRow(row)
            Next
            For i As Integer = 0 To 3 Step 1
                sheet1.AutoSizeColumn(i)
            Next    
            'If Not System.IO.Directory.Exists(ExportDirectoryPath) Then
            '    System.IO.Directory.CreateDirectory(ExportDirectoryPath)
            'End If
            'Dim path As String = ExportDirectoryPath & "\" & ExportFileName      
            Dim path As String = GetFilePathToExport()
            If path Is Nothing OrElse String.IsNullOrEmpty(path) Then
                Exit Sub
            End If
            ExcelHelper.Instance.WriteToFile(path, hssfworkbook)
            ShowMessage("Excel successfully saved at " & path, getValueByKey("CLAE04"))
            System.Diagnostics.Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Try
            Dim newItemList As New BindingList(Of StockTakeDetails)
            Dim result As Boolean = True
            Dim objClsCommon As New clsCommon
            Dim filePath As String = GetFilePath()
            If filePath Is Nothing OrElse String.IsNullOrEmpty(filePath) Then
                Exit Sub
            End If
            If Not System.IO.File.Exists(filePath) Then
                ShowMessage("Invalid Path", getValueByKey("CLAE04"))
            End If
            Dim hssfworkbook As HSSFWorkbook = ExcelHelper.Instance.InitializeWorkbook(filePath)
            Dim sheet As ISheet = hssfworkbook.GetSheetAt(0)
            Dim rows As System.Collections.IEnumerator = sheet.GetRowEnumerator()
            rows.MoveNext()
            Dim dt As DataTable = New DataTable()
            dt.Columns.Add("ItemCode")
            dt.Columns.Add("ItemName")
            dt.Columns.Add("UOM")
            dt.Columns.Add("Quantity")
            While rows.MoveNext()
                Dim row As IRow = DirectCast(rows.Current, HSSFRow)
                Dim dr As DataRow = dt.NewRow()
                For i As Integer = 0 To 3 Step 1
                    Dim cell As ICell = row.GetCell(i)
                    If cell Is Nothing Then
                        dr(i) = Nothing
                    Else
                        dr(i) = cell.ToString()
                    End If
                Next
                dt.Rows.Add(dr)
            End While
            For Each row In dt.Rows
                Dim ItemCode = row("ItemCode")
                If IsDBNull(ItemCode) OrElse String.IsNullOrEmpty(ItemCode) Then
                    Continue For
                End If
                Dim stockDetails = StockTakeList.Where(Function(stk) stk.ArticleCode = ItemCode).FirstOrDefault()
                If stockDetails IsNot Nothing Then
                    Dim qty As Decimal
                    If Not IsDBNull(row("Quantity")) AndAlso Decimal.TryParse(row("Quantity"), qty) Then
                        stockDetails.EnteredQty = qty
                    Else
                        stockDetails.EnteredQty = Nothing
                    End If
                Else
                    If objClsCommon.CheckIfValidArticleCode(row("ItemCode")) Then
                        Dim newItem As New StockTakeDetails
                        newItem.ArticleCode = row("ItemCode")
                        newItem.ArticleName = objClsCommon.GetArticleDescription(row("ItemCode"))
                        newItem.GroupCode = String.Empty
                        Dim qty As Decimal
                        If Not IsDBNull(row("Quantity")) AndAlso Decimal.TryParse(row("Quantity"), qty) Then
                            newItem.EnteredQty = qty
                        Else
                            newItem.EnteredQty = Nothing
                        End If
                        newItemList.Add(newItem)
                    End If
                End If
            Next
            If newItemList.Count > 0 Then
                AddNewItem(newItemList)
            End If
            If result Then
                ShowMessage(getValueByKey("excelimportsuccessmsg"), getValueByKey("CLAE04"))
            Else
                ShowMessage(getValueByKey("excelimportfailuremsg"), getValueByKey("CLAE05"))
            End If
        Catch ex As Exception
            ShowMessage("Import failed. Please close the file (if open) before import.", getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Function GetFilePath() As String
        Try
            Dim fd As OpenFileDialog = New OpenFileDialog()
            Dim strFileName As String = String.Empty

            fd.Title = "Open File Dialog"
            fd.InitialDirectory = ExportDirectoryPath
            fd.Filter = "Excel Worksheets|*.xls|All files (*.*)|*.*"
            fd.FilterIndex = 1
            fd.RestoreDirectory = True

            If fd.ShowDialog() = DialogResult.OK Then
                strFileName = fd.FileName
            End If
            Return strFileName
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function

    Private Function GetFilePathToExport() As String
        Try

            Dim fd As SaveFileDialog = New SaveFileDialog()
            Dim strFileName As String = String.Empty

            fd.Title = "Save A Stock Take File"
            fd.InitialDirectory = ExportDirectoryPath
            fd.AddExtension = True
            fd.FileName = ExportFileName
            fd.Filter = "Excel Worksheets|*.xls"
            fd.FilterIndex = 1
            fd.RestoreDirectory = True

            If fd.ShowDialog() = DialogResult.OK Then
                strFileName = fd.FileName
            End If
            Return strFileName
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function

    Private Function Themechange()
        dgStockTakeDetails.BackgroundColor = Color.White
        dgStockTakeDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(177, 227, 253)
        dgStockTakeDetails.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgStockTakeDetails.DefaultCellStyle.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgStockTakeDetails.DefaultCellStyle.SelectionBackColor = Color.LightBlue
        CtrlPagination1.BackColor = Color.FromArgb(134, 134, 134)
        Panel1.BackColor = Color.FromArgb(134, 134, 134)
        Me.BackColor = Color.FromArgb(134, 134, 134)
        TableLayoutPanel1.BackColor = Color.FromArgb(134, 134, 134)
        Label1.BackColor = Color.FromArgb(212, 212, 212)


        btnAddItem.BackColor = Color.FromArgb(0, 107, 163)
        btnAddItem.ForeColor = Color.FromArgb(255, 255, 255)
        btnAddItem.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnAddItem.FlatStyle = FlatStyle.Flat
        btnAddItem.FlatAppearance.BorderSize = 0
        btnAddItem.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnAddItem.Size = New Size(75, 26)
        btnAddItem.Location = New Point(316, 0)


        btnExport.BackColor = Color.FromArgb(0, 107, 163)
        btnExport.ForeColor = Color.FromArgb(255, 255, 255)
        btnExport.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnExport.FlatStyle = FlatStyle.Flat
        btnExport.FlatAppearance.BorderSize = 0
        btnExport.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnExport.Size = New Size(75, 26)
        btnExport.Location = New Point(479, 0)

        btnDelete.BackColor = Color.FromArgb(0, 107, 163)
        btnDelete.ForeColor = Color.FromArgb(255, 255, 255)
        btnDelete.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnDelete.FlatStyle = FlatStyle.Flat
        btnDelete.FlatAppearance.BorderSize = 0
        btnDelete.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnDelete.Size = New Size(75, 26)
        btnDelete.Location = New Point(397, 0)

        btnImport.BackColor = Color.FromArgb(0, 107, 163)
        btnImport.ForeColor = Color.FromArgb(255, 255, 255)
        btnImport.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnImport.FlatStyle = FlatStyle.Flat
        btnImport.FlatAppearance.BorderSize = 0
        btnImport.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnImport.Size = New Size(75, 26)
        btnImport.Location = New Point(560, 0)

        btnRawMaterialClosing.BackColor = Color.FromArgb(0, 107, 163)
        btnRawMaterialClosing.ForeColor = Color.FromArgb(255, 255, 255)
        btnRawMaterialClosing.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnRawMaterialClosing.FlatStyle = FlatStyle.Flat
        btnRawMaterialClosing.FlatAppearance.BorderSize = 0
        btnRawMaterialClosing.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnRawMaterialClosing.MinimumSize = New Size(200, 26)
        btnRawMaterialClosing.Size = New Size(200, 26)
        btnRawMaterialClosing.Location = New Point(640, 0)

        btnSelect.BackColor = Color.FromArgb(0, 107, 163)
        btnSelect.ForeColor = Color.FromArgb(255, 255, 255)
        btnSelect.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnSelect.FlatStyle = FlatStyle.Flat
        btnSelect.FlatAppearance.BorderSize = 0
        btnSelect.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnSelect.Size = New Size(75, 26)
        btnSelect.Location = New Point(235, 0)

    End Function
    Private Sub writestokeTakeLog(ByVal mes As String)
        Dim ax As New ApplicationException(mes)
        LogException(ax)
    End Sub

    Private Sub btnRawMaterialClosing_Click(sender As System.Object, e As System.EventArgs) Handles btnRawMaterialClosing.Click
        Try
            Dim obj As New frmOtherStockTakeDetails
            obj.ShowDialog()
            If obj.DialogResult = DialogResult.OK Then
                Dim RawMaterialDetailsList As BindingList(Of RawMaterialDetails)
                RawMaterialDetailsList = obj.RawMaterialDetailsList
                For Each rawMaterial As RawMaterialDetails In RawMaterialDetailsList
                    'Dim result = StockTakeList.Select(
                    'StockTakeList.Exists(Function(rawMaterial) item.ArticleCode = myData.uin)
                    Dim result = StockTakeList.Any(Function(item) item.ArticleCode = rawMaterial.ArticleCode)
                    'parts.Exists(x => x.PartId == 1444)
                    ' If result = False Then
                    Dim stockTake As New StockTakeDetails
                    stockTake.ArticleBaseUOM = rawMaterial.ArticleBaseUOM
                    stockTake.ArticleCode = rawMaterial.ArticleCode
                    stockTake.ArticleName = rawMaterial.ArticleName
                    stockTake.SubGroupId = rawMaterial.SubGroupId
                    stockTake.EnteredQty = rawMaterial.EnteredQty
                    stockTake.GroupCode = rawMaterial.GroupCode
                    stockTake.MAP = rawMaterial.MAP
                    stockTake.CurrentStock = rawMaterial.CurrentStock
                    stockTake.UOMData = rawMaterial.UOMData
                    stockTake.StockTakeUOMCode = rawMaterial.StockTakeUOMCode
                    stockTake.Multiplier = rawMaterial.Multiplier
                    StockTakeList.Add(stockTake)
                    '  End If
                Next
                Dim stocktakedata = LogForStokeTake(StockTakeList)
                writestokeTakeLog(vbNewLine + stocktakedata + vbNewLine + " Filled raw materail data in stocktake list")
                obj.Close()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Private Sub dgStockTakeDetails_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
        Try
            'If clsDefaultConfiguration.HideControlsFromStockTake Then
            '    Dim i As Integer
            '    Dim selectedIndex As Integer = dgStockTakeDetails.SelectedCells.Item(i).RowIndex.ToString()
            '    Dim ArticleType As String = ""
            '    ArticleType = dgStockTakeDetails.Rows(selectedIndex).Cells(3).Value
            '    Dim bb As Decimal
            '    bb = dgStockTakeDetails.Rows(selectedIndex).Cells(4).Value
            '    If ArticleType.ToUpper.ToString.Equals("NOS") Then
            '        If bb = Int(bb) Then
            '        Else
            '            MessageBox.Show("Please enter quantity in integer")
            '        End If
            '    End If
            '    If ArticleType.ToUpper.ToString.Equals("KGS") Then

            '        If bb < 10 Then
            '        Else
            '            MessageBox.Show("Please enter quantity valid quantity")
            '        End If
            '    End If
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
End Class
