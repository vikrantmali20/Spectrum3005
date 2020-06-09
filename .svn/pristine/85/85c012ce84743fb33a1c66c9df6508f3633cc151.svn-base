Imports SpectrumBL
Public Class frmMergeOrder

    Private dtTable As New DataTable
    Private clsCash As New clsCashMemo
    Dim dtOrderDetails As DataTable
    Dim dtFinal As New DataTable
    Dim dv As New DataView
    Dim newRow As DataRow

    Private _MergeId As Int64
    Public Property MergeId() As Int64
        Get
            Return _MergeId
        End Get
        Set(ByVal value As Int64)
            _MergeId = value
        End Set
    End Property

    Private Sub frmMergeOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            SetCulture(Me, Me.Name)
            dtTable = clsCash.GetTableForMerge(clsAdmin.SiteCode, clsAdmin.DayOpenDate)
            Dim dtNewdtTable As New DataTable
            dtNewdtTable = dtTable.Copy
            dtNewdtTable.Columns.Remove("BillNo")
            dtNewdtTable = dtNewdtTable.DefaultView.ToTable(True, "DineInNumber", "DineInName", "ReservationId")
            CboTableNo.DataSource = dtNewdtTable
            CboTableNo.ValueMember = dtNewdtTable.Columns("DineInNumber").ColumnName
            CboTableNo.DisplayMember = dtNewdtTable.Columns("DineInName").ColumnName
            If CboTableNo.SelectedIndex <> -1 Then
                CboTableNo.SelectedIndex = 0
            End If


            Dim dsTemp = clsCash.GetMappingMerge(MergeId, clsAdmin.DayOpenDate)
            If MergeId > 0 Then
                txtName.Text = dsTemp.Tables(0).Rows(0)(0)
            End If
            dtFinal = dsTemp.Tables(1)
            RefreshPrinterGrid()
            btnSaveLoad.Enabled = False
            btnSave.Enabled = False
            With grdComboMapping
                .VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
                .ScrollBars = ScrollBars.Both
                .AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns
                .SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
            End With
            txtName.Focus()
            txtName.Select()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If txtName.Text.Trim() = String.Empty Then
                ShowMessage(getValueByKey("DIN015"), "DIN015-" & getValueByKey("DIN030"))
                'ShowMessage("Please Enter Name", "" & getValueByKey("CLAE04"))
                Exit Sub
            End If
            If CboTableNo.SelectedIndex = -1 Then
                ShowMessage(getValueByKey("DIN016"), "DIN016-" & getValueByKey("DIN030"))
                'ShowMessage("Please Select Table", "" & getValueByKey("DIN030"))
                Exit Sub
            End If
            If CboOrderNo.SelectedIndex = -1 Then
                ShowMessage(getValueByKey("DIN017"), "DIN017-" & getValueByKey("DIN030"))
                'ShowMessage("Please Select Order", "" & getValueByKey("DIN030"))
                Exit Sub
            End If
            newRow = dtFinal.Rows.Find(CboOrderNo.SelectedValue)
            If newRow Is Nothing Then
                newRow = dtFinal.NewRow()
                newRow("TableNumber") = CboTableNo.SelectedValue
                newRow("OrderNumber") = CboOrderNo.SelectedValue
                Dim drReserId() = dtTable.Select("DineInNumber='" & CboTableNo.SelectedValue.ToString & "' AND BillNo='" & CboOrderNo.SelectedValue.ToString & "'")
                If drReserId.Count > 0 Then
                    newRow("Reservationid") = drReserId(0)("ReservationId")
                End If
                dtFinal.Rows.Add(newRow)
            End If
            RefreshPrinterGrid()
            btnSaveLoad.Enabled = True
            btnSave.Enabled = True

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If dtFinal.Rows.Count = 1 Then

                ShowMessage(getValueByKey("DIN037"), getValueByKey("DIN030"))
                ' ShowMessage("Select more than one Table", getValueByKey("DIN030"))
                Exit Sub
            End If
            If clsCash.SaveForMergeOrder(dtFinal, txtName.Text.Replace("'", ""), clsAdmin.UserCode, clsAdmin.SiteCode, MergeId) Then
                ShowMessage(getValueByKey("DIN018"), "DIN018-" & getValueByKey("DIN030"))
                'ShowMessage("Order Saved", "")
                Me.DialogResult = Windows.Forms.DialogResult.OK
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnSaveLoad_Click(sender As Object, e As EventArgs) Handles btnSaveLoad.Click
        Try
            If dtFinal.Rows.Count = 1 Then
                'ShowMessage("Select more than one Table", getValueByKey("DIN030"))
                ShowMessage(getValueByKey("DIN037"), getValueByKey("DIN030"))
                Exit Sub
            End If
            If clsCash.SaveForMergeOrder(dtFinal, txtName.Text.Replace("'", ""), clsAdmin.UserCode, clsAdmin.SiteCode, MergeId) Then
                ShowMessage(getValueByKey("DIN018"), "DIN018-" & getValueByKey("DIN030"))
                'ShowMessage("Order Saved", "")
                Me.DialogResult = Windows.Forms.DialogResult.Yes
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        'Me.Close()
    End Sub

    Private Sub RefreshPrinterGrid()


        grdComboMapping.DataSource = dtFinal
        grdComboMapping.Cols("Delete").Caption = ""
        grdComboMapping.Cols("Delete").Width = 20
        grdComboMapping.Cols("Delete").ComboList = "..."
        grdComboMapping.AutoSizeCols()
        grdComboMapping.Rows(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
    End Sub

    Private Sub grdComboMapping_CellButtonClick(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdComboMapping.CellButtonClick
        'Are you sure you want to delete this Row?
        If MsgBox(getValueByKey("DIN036"), MsgBoxStyle.YesNo, "DIN036 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
            Dim filterDelete As String = String.Empty

            filterDelete = "OrderNumber='" & grdComboMapping.Item(grdComboMapping.Row, "OrderNumber") & "' "

            dv = New DataView(dtFinal, filterDelete, "", DataViewRowState.CurrentRows)

            If dv.Count > 0 Then
                For Each drView As DataRowView In dv
                    drView.Row.Delete()
                    dtFinal.AcceptChanges()

                    If dtFinal.Rows.Count = 0 Then
                        btnSave.Enabled = False
                        btnSaveLoad.Enabled = False
                    Else
                        btnSave.Enabled = True
                        btnSaveLoad.Enabled = True

                    End If
                Next
            End If
            RefreshPrinterGrid()
            'btnSaveLoad.Enabled = True
            'btnSave.Enabled = True
        End If
    End Sub

    Public Sub New(ByVal VMergeId As Integer)

        MergeId = VMergeId
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub CboTableNo_SelectedValueChanged(sender As Object, e As EventArgs) Handles CboTableNo.SelectedValueChanged
        Try
            If Not (CboTableNo.SelectedValue.ToString = "System.Data.DataRowView") Then
                dtOrderDetails = clsCash.GetOrderDetailForMerge(clsAdmin.SiteCode, CboTableNo.SelectedValue, clsAdmin.DayOpenDate)
                If dtOrderDetails.Rows.Count > 0 Then
                    CboOrderNo.DataSource = dtOrderDetails
                    CboOrderNo.ValueMember = dtOrderDetails.Columns("BillNo").ColumnName
                    CboOrderNo.DisplayMember = dtOrderDetails.Columns("BillNo").ColumnName
                    CboOrderNo.SelectedIndex = 0
                Else
                    CboOrderNo.DataSource = Nothing
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        lblName.ForeColor = Color.White
        lblName.BackColor = Color.Transparent
        lblName.BorderStyle = BorderStyle.None
        lblName.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        txtName.MaximumSize = New Size(0, 19)
        txtName.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        CboTableNo.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        CboOrderNo.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        lblOrderNumber.ForeColor = Color.White
        lblOrderNumber.BackColor = Color.Transparent
        lblOrderNumber.BorderStyle = BorderStyle.None
        lblOrderNumber.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        lblTableNumber.ForeColor = Color.White
        lblTableNumber.BackColor = Color.Transparent
        lblTableNumber.BorderStyle = BorderStyle.None
        lblTableNumber.Location = New Point(14, 49)
        lblTableNumber.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        'lblOrderNo.ForeColor = Color.White
        'lblOrderNoValue.ForeColor = Color.White
        grdComboMapping.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        grdComboMapping.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        grdComboMapping.Rows.MinSize = 25
        grdComboMapping.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        grdComboMapping.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdComboMapping.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdComboMapping.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdComboMapping.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdComboMapping.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdComboMapping.CellButtonImage = Global.Spectrum.My.Resources.Delete
        C1Sizer1.Grid.Rows(1).Size = 25

        btnAdd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnAdd.BackColor = Color.Transparent
        btnAdd.BackColor = Color.FromArgb(0, 107, 163)
        btnAdd.ForeColor = Color.FromArgb(255, 255, 255)
        btnAdd.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        btnAdd.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnAdd.FlatStyle = FlatStyle.Flat
        btnAdd.FlatAppearance.BorderSize = 0
        btnAdd.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnAdd.TextAlign = ContentAlignment.MiddleCenter
        btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnSave.BackColor = Color.Transparent
        btnSave.BackColor = Color.FromArgb(0, 107, 163)
        btnSave.ForeColor = Color.FromArgb(255, 255, 255)
        btnSave.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnSave.TextAlign = ContentAlignment.MiddleCenter
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnCancel.BackColor = Color.Transparent
        btnCancel.BackColor = Color.FromArgb(0, 107, 163)
        btnCancel.TextAlign = ContentAlignment.MiddleCenter
        btnCancel.ForeColor = Color.FromArgb(255, 255, 255)
        btnCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnSaveLoad.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnSaveLoad.BackColor = Color.Transparent
        btnSaveLoad.BackColor = Color.FromArgb(0, 107, 163)
        btnSaveLoad.ForeColor = Color.FromArgb(255, 255, 255)
        btnSaveLoad.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        btnSaveLoad.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnSaveLoad.FlatStyle = FlatStyle.Flat
        btnSaveLoad.FlatAppearance.BorderSize = 0
        btnSaveLoad.TextAlign = ContentAlignment.MiddleCenter
        btnSaveLoad.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
    End Function
  
End Class