Imports C1.Win.C1FlexGrid
Imports SpectrumBL

Public Class CtrlConfigDataGrid
    Protected controlList As New ArrayList
    Protected ImagePathRowIndex As Integer = 0

    Private flexGrid As C1FlexGrid
    Public Property DefaultFlexGrid() As C1FlexGrid
        Get
            Return flexGrid
        End Get
        Set(ByVal value As C1FlexGrid)
            flexGrid = value
        End Set
    End Property

    Private BeforeCellChange As Object

    Public Function RefreshGridData(ByVal dtSettings As DataTable) As Boolean
        Try
            gridConfigSettings.DataSource = dtSettings
            gridConfigSettings.DataMember = dtSettings.TableName

            HideGridColumns(gridConfigSettings)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Function HideGridColumns(ByRef gridControl As CtrlGrid) As Boolean
        Try
            gridControl.AllowSorting = True
            gridControl.ShowSort = True

            gridControl.Cols("DocumentType").Visible = False
            gridControl.Cols("FldType").Visible = False
            gridControl.Cols("FldLabel").Visible = False
            gridControl.Cols(5).Visible = False
            gridControl.Cols(6).Visible = False

            gridControl.Cols("Description").Caption = getValueByKey("DC0001") '"Setting"
            gridControl.Cols("Description").AllowEditing = False
            gridControl.Cols("Description").WidthDisplay = CInt(gridControl.Width - (gridControl.Width * 0.35))
            gridControl.Cols("Description").StyleDisplay.BackColor = Color.AliceBlue

            gridControl.Cols("FldValue").Caption = getValueByKey("DC0002") '"Value"
            gridControl.Cols("FldValue").WidthDisplay = CInt(gridControl.Width - (gridControl.Width * 0.65) - 25)
            gridControl.Cols("FldValue").StyleDisplay.WordWrap = True
            gridControl.Cols("FldValue").AllowSorting = False

            Dim getColumnType As String = String.Empty

            'Create styles with data types, formats, etc
            Dim cellStyle As C1.Win.C1FlexGrid.CellStyle

            cellStyle = gridControl.Styles.Add("CellIntType")
            cellStyle.DataType = Type.GetType("System.Int32")

            cellStyle = gridControl.Styles.Add("CellImageType")
            cellStyle.DataType = Type.GetType("System.String")
            cellStyle.TextAlign = TextAlignEnum.LeftCenter
            cellStyle.WordWrap = True

            cellStyle = gridControl.Styles.Add("CellStringType")
            cellStyle.DataType = Type.GetType("System.String")
            cellStyle.WordWrap = True

            'Assign styles to editable cells
            Dim assignCellStyles As CellRange

            For rowIndex = 1 To gridControl.Rows.Count - 1
                getColumnType = gridControl.Item(rowIndex, "FldType").ToString.ToLower
                gridControl.Rows(rowIndex).HeightDisplay = 30

                If getColumnType.ToUpper = "Boolean".ToUpper Then
                    gridControl.Rows.Item(rowIndex).ComboList = "True|False"
                ElseIf getColumnType.ToUpper = "Select".ToUpper Then
                    gridControl.Rows.Item(rowIndex).ComboList = "JK|PC|Spectrum"
                    'added by khusrao
                    'new flag PromotionBasedOn for sprint 14 promotion
                ElseIf getColumnType.ToUpper = "SelectDate".ToUpper Then
                    gridControl.Rows.Item(rowIndex).ComboList = "ApplicationDate|SystemDate"
                ElseIf getColumnType.ToUpper = "FontSelect".ToUpper Then
                    gridControl.Rows.Item(rowIndex).ComboList = "Courier New|Franklin Gothic Demi|Britannic Bold|Eras Demi ITC|Eras Bold ITC|Impact"

                ElseIf getColumnType.ToUpper = "CustSearch".ToUpper Then
                    gridControl.Rows.Item(rowIndex).ComboList = "All|Mobile No."

                ElseIf getColumnType.ToUpper = "SelTheme".ToUpper Then
                    gridControl.Rows.Item(rowIndex).ComboList = "Default|Theme 1"
                ElseIf getColumnType.ToUpper = "Module".ToUpper Then                   'vipin
                    gridControl.Rows.Item(rowIndex).ComboList = "NA|CM|TCM|Dine In|SO"
                    'code added for jk sprint 28
                ElseIf getColumnType.ToUpper = "Drive".ToUpper Then
                    gridControl.Rows.Item(rowIndex).ComboList = "C:\AmmyyAdmin|D:\AmmyyAdmin|E:\AmmyyAdmin|F:\AmmyyAdmin"
                    'added by khusrao adil on 11-10-2017 for jk sprint 30 requirement 
                ElseIf getColumnType.ToUpper = "SelectBat".ToUpper Then
                    gridControl.Rows.Item(rowIndex).ComboList = "run_dayCloseSynch.bat|run_stocktech_sync.bat"
                ElseIf getColumnType.ToUpper = "Integer".ToUpper Then
                    If (gridControl.Item(rowIndex, "FldLabel").ToString.ToUpper = "CheckExpiryMonth".ToUpper) Then
                        gridControl.Rows.Item(rowIndex).ComboList = "1|2|3|4|5|6|7|8|9|10|11|12"

                    ElseIf (gridControl.Item(rowIndex, "FldLabel").ToString.ToUpper = "LocalSiteCode".ToUpper) Then
                        Dim clsComn As New clsCommon
                        Dim dtDefaultSite As New DataTable
                        dtDefaultSite = clsComn.GetSiteAndSupplierInfo("Site")

                        'Dim cboDefaultSite As New ComboBox
                        'cboDefaultSite.DataSource = dtDefaultSite
                        'cboDefaultSite.DisplayMember = "SiteShortName"
                        'cboDefaultSite.ValueMember = "SiteCode"

                        'gridControl.Rows.Item(rowIndex).Editor = cboDefaultSite

                        Dim drSupplier As DataRow() = dtDefaultSite.Select("SiteCode='" & gridControl.Item(rowIndex, "FldValue") & "'")
                        gridControl.Item(rowIndex, "FldValue") = drSupplier(0)("SiteShortName")

                    ElseIf (gridControl.Item(rowIndex, "FldLabel").ToString.ToUpper = "SOSupplier".ToUpper) Then
                        Dim clsComn As New clsCommon
                        Dim dtDefaultSupplier As New DataTable
                        dtDefaultSupplier = clsComn.GetSiteAndSupplierInfo("Supplier")

                        Dim cboDefaultSupplier As New ComboBox
                        cboDefaultSupplier.DataSource = dtDefaultSupplier
                        cboDefaultSupplier.DisplayMember = "SupplierName"
                        cboDefaultSupplier.ValueMember = "SupplierCode"

                        gridControl.Rows.Item(rowIndex).Editor = cboDefaultSupplier

                        Dim drSupplier As DataRow() = dtDefaultSupplier.Select("SupplierCode='" & gridControl.Item(rowIndex, "FldValue") & "'")
                        gridControl.Item(rowIndex, "FldValue") = drSupplier(0)("SupplierName")

                    Else
                        assignCellStyles = gridControl.GetCellRange(rowIndex, 3)
                        assignCellStyles.Style = gridControl.Styles("CellIntType")
                    End If

                ElseIf getColumnType.ToUpper = "String".ToUpper Then
                    assignCellStyles = gridControl.GetCellRange(rowIndex, 3)
                    assignCellStyles.Style = gridControl.Styles("CellStringType")

                ElseIf getColumnType.ToUpper = "Images".ToUpper Then
                    Dim ButtonX As Integer = gridControl.Cols("FldValue").WidthDisplay
                    Dim ButtonWidth As Integer = 45

                    'Create some new controls
                    Dim btnBrowse As New CtrlBtn()
                    btnBrowse.MaximumSize = New System.Drawing.Size(ButtonWidth, 30)
                    btnBrowse.SetRowIndex = rowIndex
                    btnBrowse.Text = "..."

                    'Insert hosted control into grid
                    gridControl.Controls.Add(btnBrowse)

                    'host them in the C1FlexGrid
                    controlList.Add(New HostedControl(gridControl, btnBrowse, rowIndex, 3, ButtonX, ButtonWidth))

                    'ImagePathRowIndex = rowIndex
                    assignCellStyles = gridControl.GetCellRange(rowIndex, 3)
                    assignCellStyles.Style = gridControl.Styles("CellImageType")

                    AddHandler btnBrowse.Click, AddressOf BrowseImagePath
               
                Else
                    assignCellStyles = gridControl.GetCellRange(rowIndex, 3)
                    assignCellStyles.Style = gridControl.Styles("CellStringType")
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub gridConfigSettings_AfterEdit(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles gridConfigSettings.AfterEdit
        Dim fldValue As Object = Nothing
        Try
            If (gridConfigSettings.Item(e.Row, "FldType").ToString.ToUpper = "String".ToUpper) Then
                fldValue = gridConfigSettings.Item(e.Row, e.Col)

                If (fldValue Is Nothing OrElse String.IsNullOrEmpty(fldValue.ToString())) Then
                    ShowMessage(getValueByKey("DC0014"), "DC0014 - " + getValueByKey("CLAE04"))
                    gridConfigSettings.Item(e.Row, e.Col) = BeforeCellChange
                End If

            ElseIf (gridConfigSettings.Item(e.Row, "FldType").ToString.ToUpper = "Integer".ToUpper) Then
                fldValue = gridConfigSettings.Item(e.Row, e.Col)

                If (gridConfigSettings.Item(e.Row, "Fldlabel").ToString.ToUpper = "CustSearchCharpos".ToUpper) Then
                    If fldValue.ToString() > 10 Then
                        ShowMessage("Value canot be greater than 10", getValueByKey("CLAE05"))
                        Exit Sub
                    End If
                End If

                If (fldValue Is Nothing OrElse String.IsNullOrEmpty(fldValue.ToString())) Then
                    ShowMessage(getValueByKey("DC0015"), "DC0015 - " + getValueByKey("CLAE04"))
                    gridConfigSettings.Item(e.Row, e.Col) = BeforeCellChange
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub gridConfigSettings_BeforeEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles gridConfigSettings.BeforeEdit

        If (gridConfigSettings.Item(e.Row, "FldType").ToString.ToUpper = "Images".ToUpper) Then
            e.Cancel = True

        ElseIf (gridConfigSettings.Item(e.Row, "FldLabel").ToString.ToUpper = "LocalSiteCode".ToUpper) Then
            e.Cancel = True
        End If
    End Sub
 
    Private Sub gridConfigSettings_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles gridConfigSettings.Paint
        For Each hosted As HostedControl In controlList
            hosted.UpdatePosition()
        Next
    End Sub

    Public Function BrowseImagePath(ByVal sender As Object, ByVal e As EventArgs) As String
        Try
            Dim openPath As New OpenFileDialog
            openPath.Title = getValueByKey("DC0011") '"Select Background Image file"
            openPath.Filter = "PNG file (*.png)|*.png|JPEG file (*.jpg)|*.jpg|Bitmap file (*.bmp)|*.bmp|ICO file (*.ico)|*.ico|TIFF file (*.tif)|*.tif|GIF file (*.gif)|*.gif"
            openPath.FilterIndex = 0
            openPath.ShowDialog()
            'If (dgresult = DialogResult.OK) Or (dgresult = DialogResult.Cancel) Then
            flexGrid.Item(DirectCast(sender, Spectrum.CtrlBtn).SetRowIndex, 3) = openPath.FileName
            'End If
            Return openPath.FileName

        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Private Sub gridConfigSettings_StartEdit(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles gridConfigSettings.StartEdit
        Try
            If (gridConfigSettings.Item(e.Row, "FldType").ToString.ToUpper = "String".ToUpper) Then
                BeforeCellChange = gridConfigSettings.Item(e.Row, e.Col)

            ElseIf (gridConfigSettings.Item(e.Row, "FldType").ToString.ToUpper = "Integer".ToUpper) Then
                BeforeCellChange = gridConfigSettings.Item(e.Row, e.Col)

            End If
            If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
                OnTouchKeyBoard()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
 
    Private Sub CtrlConfigDataGrid_Load(sender As Object, e As EventArgs) Handles Me.Load
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
        End If
    End Sub
    Private Function Themechange()
        gridConfigSettings.VisualStyle = VisualStyle.Custom
        gridConfigSettings.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        gridConfigSettings.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        gridConfigSettings.Rows.MinSize = 25
        gridConfigSettings.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        gridConfigSettings.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridConfigSettings.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridConfigSettings.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridConfigSettings.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridConfigSettings.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
    End Function
End Class




