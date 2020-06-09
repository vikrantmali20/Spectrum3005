Imports SpectrumBL
Imports System.ComponentModel
Imports NPOI.HSSF.UserModel
Imports SpectrumCommon
Imports NPOI.SS.UserModel
Imports System.Net.Mime.MediaTypeNames
Imports Microsoft.Office.Interop


Public Class frmSeatingAreaWiseArticlePriceMapping
    Dim clscom As New clsCommon
    Dim dt As DataTable

    Private _dtprice As BindingList(Of ArticleSeatingAreaMap)
    Public Property dtprice As BindingList(Of ArticleSeatingAreaMap)
        Get
            If _dtprice Is Nothing Then
                _dtprice = New BindingList(Of ArticleSeatingAreaMap)
            End If
            Return _dtprice
        End Get
        Set(ByVal value As BindingList(Of ArticleSeatingAreaMap))
            _dtprice = value
        End Set
    End Property

    Private Sub frmSeatingAreaWiseArticlePrinceMapping_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            ThemeChange()
        End If
    End Sub

    Public Sub ThemeChange()
        Me.BackColor = Color.FromArgb(134, 134, 134)

        'Label
        'Label5.BackColor = Color.FromArgb(212, 212, 212)

        'Cancel Browse
        btnBrowse.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnBrowse.BackColor = Color.Transparent
        btnBrowse.BackColor = Color.FromArgb(0, 107, 163)
        btnBrowse.ForeColor = Color.FromArgb(255, 255, 255)
        btnBrowse.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnBrowse.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnBrowse.FlatAppearance.BorderSize = 0
        btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat

        'Change button
        btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnCancel.BackColor = Color.Transparent
        btnCancel.BackColor = Color.FromArgb(0, 107, 163)
        btnCancel.ForeColor = Color.FromArgb(255, 255, 255)
        btnCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat

        'Change Export
        btnExport.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnExport.BackColor = Color.Transparent
        btnExport.BackColor = Color.FromArgb(0, 107, 163)
        btnExport.ForeColor = Color.FromArgb(255, 255, 255)
        btnExport.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnExport.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnExport.FlatAppearance.BorderSize = 0
        btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat

        btnUpload.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnUpload.BackColor = Color.Transparent
        btnUpload.BackColor = Color.FromArgb(0, 107, 163)
        btnUpload.ForeColor = Color.FromArgb(255, 255, 255)
        btnUpload.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnUpload.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnUpload.FlatAppearance.BorderSize = 0
        btnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat

    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try
            Try
                Dim _isValidToSaveData As Boolean = True
                Dim st As Excel.Worksheet

                Dim filePath As String = txtPath.Text
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
                Dim dtExcel = New DataTable()
                dtExcel.Columns.Add("SiteCode")
                dtExcel.Columns.Add("ArticleCode")
                dtExcel.Columns.Add("EAN")
                dtExcel.Columns.Add("SeatingAreaId")
                dtExcel.Columns.Add("SeatingAreaName")
                dtExcel.Columns.Add("Price")
                dtExcel.Columns.Add("Status")
                While rows.MoveNext()
                    Dim row As IRow = DirectCast(rows.Current, HSSFRow)
                    Dim dr As DataRow = dtExcel.NewRow()
                    For i As Integer = 0 To 6 Step 1
                        Dim cell As ICell = row.GetCell(i)
                        If cell Is Nothing Then
                            dr(i) = Nothing
                        Else
                            dr(i) = cell.ToString()
                        End If
                    Next
                    dtExcel.Rows.Add(dr)
                End While
                dtExcel.Columns.Add("IsValid")
                Dim dtExcelFailed = dtExcel.Copy
                dtExcelFailed.Clear()
                Dim drExcelFailed As DataRow
                Dim isRowValid As Boolean = True
                Dim isRowValidExcelFailed As Boolean = False
                For Each drRow As DataRow In dtExcel.Rows
                    drExcelFailed = dtExcelFailed.NewRow
                    Dim _articleCode = drRow("ArticleCode").ToString()
                    Dim _siteCode = drRow("SiteCode").ToString()
                    Dim _ean = drRow("EAN").ToString()
                    Dim _ExcelseatingAreaId = drRow("SeatingAreaId").ToString()
                    Dim _ExcelseatingAreaName = drRow("SeatingAreaName").ToString()
                    Dim _price = drRow("Price").ToString().Trim
                    Dim _status = Convert.ToString(drRow("Status"))
                    isRowValidExcelFailed = False
                    If String.IsNullOrEmpty(_articleCode) Then
                        _articleCode = ""
                        drExcelFailed("ArticleCode") = "#InValid"
                        drExcelFailed("SiteCode") = _siteCode
                        drExcelFailed("EAN") = _ean
                        drExcelFailed("SeatingAreaId") = _ExcelseatingAreaId
                        drExcelFailed("SeatingAreaName") = _ExcelseatingAreaName
                        drExcelFailed("Price") = _price
                        drExcelFailed("Status") = _status
                        drExcelFailed("IsValid") = False
                        isRowValid = False
                        isRowValidExcelFailed = True
                    Else
                        drExcelFailed("ArticleCode") = _articleCode
                        drRow("ArticleCode") = _articleCode
                        If isRowValidExcelFailed = False Then
                            isRowValid = True
                        End If
                    End If
                    Dim _dsSaleInfoRecAndSittingArea As DataSet = clscom.GetArtDetails(_articleCode, _ExcelseatingAreaId, clsAdmin.SiteCode)
                    For Each drSaleInfoRec As DataRow In _dsSaleInfoRecAndSittingArea.Tables(0).Rows
                        If String.IsNullOrEmpty(_siteCode) OrElse _siteCode <> drSaleInfoRec("SiteCode").ToString() Then
                            drExcelFailed("SiteCode") = "#InValid"
                            isRowValid = False
                            isRowValidExcelFailed = True
                        Else
                            drExcelFailed("SiteCode") = _siteCode
                            drRow("SiteCode") = _siteCode
                            If isRowValidExcelFailed = False Then
                                isRowValid = True
                            End If
                        End If
                        If _ean = "" Then
                            If isRowValidExcelFailed = False Then
                                isRowValid = True
                            End If
                        Else
                            If _ean <> drSaleInfoRec("EAN").ToString Then
                                drExcelFailed("EAN") = "#InValid"
                                isRowValid = False
                                isRowValidExcelFailed = True
                            Else
                                drExcelFailed("EAN") = _ean
                                drRow("EAN") = _ean
                                If isRowValidExcelFailed = False Then
                                    isRowValid = True
                                End If
                            End If
                        End If
                        If String.IsNullOrEmpty(_price) OrElse _price = "0" Then
                            drExcelFailed("Price") = "#InValid"
                            isRowValid = False
                            isRowValidExcelFailed = True
                        Else
                            drExcelFailed("Price") = _price
                            drRow("Price") = _price
                            If isRowValidExcelFailed = False Then
                                isRowValid = True
                            End If
                        End If

                        If String.IsNullOrEmpty(_status) Then
                            drExcelFailed("Status") = "#InValid"
                            isRowValid = False
                            isRowValidExcelFailed = True
                        Else
                            drExcelFailed("Status") = _status
                            drRow("Status") = _status
                            If isRowValidExcelFailed = False Then
                                isRowValid = True
                            End If
                        End If
                    Next
                    For Each drSittingArea As DataRow In _dsSaleInfoRecAndSittingArea.Tables(1).Rows
                        Dim _IseatingAreaId = drSittingArea("SeatingAreaId").ToString()
                        Dim _seatingAreaName = drSittingArea("SeatingAreaName")

                        If String.IsNullOrEmpty(_IseatingAreaId) OrElse _ExcelseatingAreaId <> _IseatingAreaId Then
                            drExcelFailed("SeatingAreaId") = "#InValid"
                            isRowValid = False
                            isRowValidExcelFailed = True
                        Else
                            drExcelFailed("SeatingAreaId") = _IseatingAreaId
                            drRow("SeatingAreaId") = _IseatingAreaId
                            If isRowValidExcelFailed = False Then
                                isRowValid = True
                            End If
                        End If

                        If String.IsNullOrEmpty(_seatingAreaName) OrElse _ExcelseatingAreaName <> _seatingAreaName Then
                            drExcelFailed("SeatingAreaName") = "#InValid"
                            isRowValid = False
                            isRowValidExcelFailed = True
                        Else
                            drExcelFailed("SeatingAreaName") = _seatingAreaName
                            drRow("SeatingAreaName") = _seatingAreaName
                            If isRowValidExcelFailed = False Then
                                isRowValid = True
                            End If
                        End If
                    Next
                    drRow("IsValid") = isRowValid
                    drExcelFailed("IsValid") = isRowValid
                    dtExcelFailed.Rows.Add(drExcelFailed)
                Next

                Dim dv As New DataView(dtExcelFailed, "IsValid ='False'", "", DataViewRowState.CurrentRows)
                Dim dd = dtExcelFailed.Select("IsValid='False'")
                If dd.Length > 0 Then
                    dtExcelFailed = dd.CopyToDataTable
                Else
                    dtExcelFailed.Clear()
                End If
                If dtExcelFailed.Rows.Count > 0 Then
                    ShowMessage("Excel Failed due to InValid Data. Please Find The Attached Excel.", "Information")
                    If GetExcel(dtExcelFailed) Then
                        If dtExcelFailed.Rows.Count = dtExcel.Rows.Count Then
                            GoTo ClearData
                        Else
                            GoTo SaveValidData
                        End If
                    Else
                        GoTo ShowError
                    End If
                End If
SaveValidData:  If clscom.SaveSittingPriceMapping(dtExcel, clsAdmin.SiteCode, clsAdmin.UserCode) Then
                    ShowMessage(getValueByKey("excelimportsuccessmsg"), getValueByKey("CLAE04"))
ClearData:          txtPath.Text = ""
                    dtExcel.Clear()
                Else
ShowError:          ShowMessage(getValueByKey("excelimportfailuremsg"), getValueByKey("CLAE05"))
                End If
                'Dim j As Integer
                'Dim flag As Boolean = False
                'Dim dtclone As DataTable = dt.Copy
                'dtclone.Columns.Add("IsInvalid", GetType(Boolean))
                'For j = 0 To dt.Rows.Count - 1
                '    Dim _articleCode = dt.Rows(j)("ArticleCode").ToString()
                '    Dim _seatingAreaId = dt.Rows(j)("SeatingAreaId")
                '    If String.IsNullOrEmpty(_articleCode) Then
                '        _articleCode = ""
                '        dt.Rows(j)("ArticleCode") = "#InValid"
                '        flag = True
                '    End If
                '    Dim dtt As DataSet = clscom.GetArtDetails(_articleCode, _seatingAreaId, clsAdmin.SiteCode)
                '    For Each dr As DataRow In dtt.Tables("salesinforecord").Rows
                '        dt.Rows(j)("SiteCode") = IIf(dt.Rows(j)("SiteCode") Is DBNull.Value, "0", dt.Rows(j)("SiteCode"))
                '        If dr("SiteCode") <> dt.Rows(j)("SiteCode") Then
                '            dt.Rows(j)("SiteCode") = "#InValid"
                '            flag = True
                '        End If
                '        If dt.Rows(j)("EAN") Is DBNull.Value Then
                '            dt.Rows(j)("EAN") = dr("EAN")
                '        Else
                '            If dr("EAN") <> dt.Rows(j)("EAN") Then
                '                dt.Rows(j)("EAN") = "#InValid"
                '                flag = True
                '            End If
                '        End If

                '        dt.Rows(j)("Price") = IIf(dt.Rows(j)("Price") Is DBNull.Value, "0", dt.Rows(j)("Price"))
                '        If dt.Rows(j)("Price") = "0" Then
                '            dt.Rows(j)("Price") = "#InValid"
                '            flag = True
                '        End If

                '        dt.Rows(j)("Status") = IIf(dt.Rows(j)("Status") Is DBNull.Value, "1", dt.Rows(j)("Status"))
                '        If dt.Rows(j)("Status") = "1" Then
                '            dt.Rows(j)("Status") = "1"
                '        Else
                '            dt.Rows(j)("Status") = "0"
                '        End If
                '    Next


                '    If dtt.Tables("seatingarea").Rows.Count > 0 Then
                '        For Each drr As DataRow In dtt.Tables("seatingarea").Rows
                '            If drr("SeatingAreaId") <> dt.Rows(j)("SeatingAreaId") Then
                '                dt.Rows(j)("SeatingAreaId") = "#InValid"
                '                flag = True
                '            End If
                '            If drr("SeatingAreaName") <> dt.Rows(j)("SeatingAreaName") Then
                '                dt.Rows(j)("SeatingAreaName") = "#InValid"
                '                flag = True
                '            End If
                '        Next
                '    End If
                '    If flag = True Then
                '        For Each drr As DataRow In dtclone.Select("ArticleCode='" & _articleCode & "'", "", DataViewRowState.CurrentRows)
                '            drr.Delete()
                '        Next

                '    End If
                '    flag = False
                'Next
                'For m = 0 To dtclone.Rows.Count - 1
                '    For Each drr As DataRow In dt.Select("ArticleCode='" & dtclone.Rows(m)("ArticleCode") & "'", "", DataViewRowState.CurrentRows)
                '        drr.Delete()
                '    Next
                'Next

                'If dt.Rows.Count > 0 Then
                '    ShowMessage("Excel Failed due to InValid Data. Please Find The Attached Excel.", "Information")
                '    If GetExcel(dt) Then
                '        txtPath.Text = ""
                '    End If
                'End If

                'Dim sitename As String = clsAdmin.SiteCode
                'Dim username As String = clsAdmin.UserCode
                'If clscom.SaveSittingPriceMapping(dtclone, sitename, username) Then
                '    ShowMessage(getValueByKey("excelimportsuccessmsg"), getValueByKey("CLAE04"))
                '    txtPath.Text = ""
                '    dtclone.Clear()
                'Else
                '    If dtclone.Rows.Count > 1 Then
                '        ShowMessage(getValueByKey("excelimportfailuremsg"), getValueByKey("CLAE05"))
                '    End If
                'End If
            Catch ex As Exception
                ShowMessage("Import failed. Please close the file (if open) before import.", getValueByKey("CLAE05"))
                LogException(ex)
            End Try

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Function GetExcel(ByVal dttable As DataTable) As Boolean
        Try

            Dim hssfworkbook As HSSFWorkbook = ExcelHelper.Instance.InitializeWorkbook()
            Dim sheet1 As ISheet = hssfworkbook.CreateSheet("ArticlePriceMapping")
            Dim lastRow As IRow = sheet1.CreateRow(0)
            lastRow.CreateCell(0).SetCellValue("SiteCode")
            lastRow.CreateCell(1).SetCellValue("ArticleCode")
            lastRow.CreateCell(2).SetCellValue("EAN")
            lastRow.CreateCell(3).SetCellValue("SeatingAreaId")
            lastRow.CreateCell(4).SetCellValue("SeatingAreaName")
            lastRow.CreateCell(5).SetCellValue("Price")
            lastRow.CreateCell(6).SetCellValue("Status")
            ExcelHelper.Instance.SetCellStyle(lastRow, ExcelHelper.Instance.GetBoldFont(hssfworkbook))
            Dim row As Integer = 1
            Dim col As Integer = 0
            lastRow = sheet1.CreateRow(row)
            Dim dt As DataTable = dttable

            Dim x As Integer
            For x = 0 To dt.Rows.Count - 1
                Dim ArticleSeating As New ArticleSeatingAreaMap
                ArticleSeating.SiteCode = dt.Rows(x)("SiteCode")
                ArticleSeating.ArticleCode = dt.Rows(x)("ArticleCode")
                ArticleSeating.EAN = dt.Rows(x)("EAN")
                ArticleSeating.SeatingAreaId = dt.Rows(x)("SeatingAreaId")
                ArticleSeating.SeatingAreaName = dt.Rows(x)("SeatingAreaName")
                ArticleSeating.Price = dt.Rows(x)("Price")
                ArticleSeating.Status = dt.Rows(x)("Status")
                dtprice.Add(ArticleSeating)
            Next
            Dim i As Integer

            For Each item As ArticleSeatingAreaMap In dtprice
                lastRow.CreateCell(0).SetCellValue(item.SiteCode)
                lastRow.CreateCell(1).SetCellValue(item.ArticleCode)
                lastRow.CreateCell(2).SetCellValue(item.EAN)
                lastRow.CreateCell(3).SetCellValue(item.SeatingAreaId)
                lastRow.CreateCell(4).SetCellValue(item.SeatingAreaName)
                lastRow.CreateCell(5).SetCellValue(item.Price)
                lastRow.CreateCell(6).SetCellValue(item.Status)
                row += 1
                lastRow = sheet1.CreateRow(row)
            Next
            For j As Integer = 0 To 6 Step 1
                sheet1.AutoSizeColumn(j)
            Next

            Dim path As String = GetFilePathToExport(True)
            If path Is Nothing OrElse String.IsNullOrEmpty(path) Then
                dtprice.Clear()
                Return True
            End If
            ExcelHelper.Instance.WriteToFile(path, hssfworkbook)
            ' System.Diagnostics.Process.Start(path)
            dtprice.Clear()
            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Const ExportDirectoryPath As String = "C:\"
    Private Const ExportFileName As String = "PriceMapping.xls"
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
    Private Function GetFilePathToExport(Optional ByVal value As Boolean = False) As String
        Try

            Dim fd As SaveFileDialog = New SaveFileDialog()
            Dim strFileName As String = String.Empty
            fd.Title = "Seating Area Wise Price Mapping."
            fd.InitialDirectory = ExportDirectoryPath
            fd.AddExtension = True
            If value = True Then
                fd.FileName = "Failed! " + ExportFileName
            Else
                fd.FileName = ExportFileName
            End If

            '  fd.Filter = "Excel Worksheets|*.xls"
            fd.Filter = "Excel Worksheets|*.xlsx|All files (*.*)|*.*"
            fd.FilterIndex = 1
            fd.RestoreDirectory = True

            If fd.ShowDialog() = DialogResult.OK Then
                strFileName = fd.FileName
            Else
                Exit Function
            End If
            Return strFileName
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try

            Dim hssfworkbook As HSSFWorkbook = ExcelHelper.Instance.InitializeWorkbook()
            Dim sheet1 As ISheet = hssfworkbook.CreateSheet("ArticlePriceMapping")
            Dim lastRow As IRow = sheet1.CreateRow(0)
            lastRow.CreateCell(0).SetCellValue("SiteCode")
            lastRow.CreateCell(1).SetCellValue("ArticleCode")
            lastRow.CreateCell(2).SetCellValue("EAN")
            lastRow.CreateCell(3).SetCellValue("SeatingAreaId")
            lastRow.CreateCell(4).SetCellValue("SeatingAreaName")
            lastRow.CreateCell(5).SetCellValue("Price")
            lastRow.CreateCell(6).SetCellValue("Status")
            ExcelHelper.Instance.SetCellStyle(lastRow, ExcelHelper.Instance.GetBoldFont(hssfworkbook))
            Dim row As Integer = 1
            Dim col As Integer = 0
            lastRow = sheet1.CreateRow(row)
            Dim dt As DataTable = clscom.GetPriceMappingDetail()

            Dim x As Integer
            For x = 0 To dt.Rows.Count - 1
                Dim ArticleSeating As New ArticleSeatingAreaMap
                ArticleSeating.SiteCode = dt.Rows(x)("SiteCode")
                ArticleSeating.ArticleCode = dt.Rows(x)("ArticleCode")
                ArticleSeating.EAN = dt.Rows(x)("EAN")
                ArticleSeating.SeatingAreaId = dt.Rows(x)("SeatingAreaId")
                ArticleSeating.SeatingAreaName = dt.Rows(x)("SeatingAreaName")
                ArticleSeating.Price = dt.Rows(x)("Price")
                ArticleSeating.Status = dt.Rows(x)("Status")
                dtprice.Add(ArticleSeating)
            Next
            Dim i As Integer

            For Each item As ArticleSeatingAreaMap In dtprice
                lastRow.CreateCell(0).SetCellValue(item.SiteCode)
                lastRow.CreateCell(1).SetCellValue(item.ArticleCode)
                lastRow.CreateCell(2).SetCellValue(item.EAN)
                lastRow.CreateCell(3).SetCellValue(item.SeatingAreaId)
                lastRow.CreateCell(4).SetCellValue(item.SeatingAreaName)
                lastRow.CreateCell(5).SetCellValue(item.Price)
                lastRow.CreateCell(6).SetCellValue(item.Status)
                row += 1
                lastRow = sheet1.CreateRow(row)
            Next
            For j As Integer = 0 To 3 Step 1
                sheet1.AutoSizeColumn(j)
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
            dtprice.Clear()
        Catch ex As Exception
            '  blnFileOpen = False
            LogException(ex)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If Not String.IsNullOrEmpty(txtPath.Text) Then
            If MessageBox.Show("You will loose unsaved data, Do you wish to continue?", "Information", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Try
            txtPath.Text = GetFilePath()
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub frmSeatingAreaWiseArticlePrinceMapping_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Dim userid As String = txtPath.Text.Trim
            If Not String.IsNullOrEmpty(userid) Then
                If MessageBox.Show("You will loose unsaved data, Do you wish to continue?", "Information", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
                    Me.Close()
                End If
            Else
                Me.Close()
            End If
        End If
    End Sub
End Class