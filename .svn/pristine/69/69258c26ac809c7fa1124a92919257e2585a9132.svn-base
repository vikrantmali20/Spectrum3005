Imports System.Resources
Imports System.Globalization
Imports SpectrumBL
Imports C1.Win.C1FlexGrid
''' <summary>
'''  Saving printing settings
''' </summary>
''' <remarks></remarks>
''' 
Public Class frmNPrintingSettings
    Dim iSrNo As Integer
    Dim iperGridRow As Integer = iSrNo + 1
    Dim iSequenceNumber As Integer = 1
    Private _receiptTextMaxLength As Integer
    Dim dtPosPrintingDetails As New POSDBDataSet.PrintingDetailDataTable
    Dim dvTop As DataView
    Dim dvBottom As DataView
    Dim dvWelcome, dvPromo, dvTax As DataView
    Private Function LoadItemNoReceipt() As Boolean
        cboItemNoReceipt.AddItem("ItemCode")
        cboItemNoReceipt.AddItem("ItemCode and BarCode")
        Return True
    End Function
    Private Function Load_cboDocumentType() As Boolean
        Dim objclsPrintingsettings As New clsPrintingSettings
        Dim dtDocumentType As DataTable = objclsPrintingsettings.GetDocumentType()
        If Not dtDocumentType Is Nothing Then
            cboDocumentType.DataSource = dtDocumentType
            cboDocumentType.DisplayMember = dtDocumentType.Columns("DocumentTypeDesc").ColumnName
            cboDocumentType.ValueMember = dtDocumentType.Columns("DocumentType").ColumnName
            pC1ComboSetDisplayMember(cboDocumentType)
            cboDocumentType.SelectedIndex = 0
        End If
        Return True
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (dtPosPrintingDetails.Rows.Count < 0) Then
            GridData()
        Else
            UpdateData()
        End If
    End Sub
    Private Sub UpdateData()
        Try
            Dim adpPosPrintingDetails As New POSDBDataSetTableAdapters.PrintingDetailTableAdapter

            adpPosPrintingDetails.Update(dtPosPrintingDetails)
            MessageBox.Show(getValueByKey("PRNS001"), "PRNS001 - " & getValueByKey("CLAE04"))
            'MessageBox.Show("Setting are saved ")
        Catch ex As Exception
            MessageBox.Show(getValueByKey("PRNS002"), "PRNS002 - " & getValueByKey("CLAE04"))
            LogException(ex)
            'MessageBox.Show("Setting not  saved ")
        End Try

    End Sub
    Private Sub GridData()
        Try
            Dim dtGirdTop As DataTable = gridTop.DataSource
            AddRows(gridTop)
            AddRows(gridBottom, False)
            Dim adpPosPrintingDetails As New POSDBDataSetTableAdapters.PrintingDetailTableAdapter
            adpPosPrintingDetails.Update(dtPosPrintingDetails)

            gridBottom.Clear(ClearFlags.Content, 1, 1, 14, 5)
            gridTop.Clear(ClearFlags.Content, 1, 1, 14, 5)
            'MessageBox.Show("Setting are saved ")
            MessageBox.Show(getValueByKey("PRNS001"), "PRNS001 - " & getValueByKey("CLAE04"))
        Catch ex As Exception
            MessageBox.Show(getValueByKey("PRNS002"), "PRNS002 - " & getValueByKey("CLAE04"))
            LogException(ex)
            'MessageBox.Show("Setting not  saved ")
        End Try
    End Sub
    Private Sub AddRows(ByVal gird As C1FlexGrid, Optional ByVal IsTop As Boolean = True)
        Try
            Dim objclsComman As New clsCommon

            For Each drrow As C1.Win.C1FlexGrid.Row In gird.Rows
                If (drrow.Index > 0) Then
                    If Not drrow("ReceiptText") Is Nothing And Not drrow("ReceiptText") = String.Empty Then
                        Dim drNew As DataRow = dtPosPrintingDetails.NewRow()
                        drNew("SrNo") = iperGridRow
                        drNew("ReceiptText") = drrow("ReceiptText")
                        drNew("SequenceNo") = iSequenceNumber
                        drNew("Align") = drrow("Align")
                        drNew("Width") = drrow("Width")
                        drNew("Height") = drrow("Height")
                        If (drrow("Bold") Is DBNull.Value) Then
                            drNew("Bold") = False
                        Else
                            drNew("Bold") = True
                        End If
                        If (IsTop) Then
                            drNew("TopBottom") = True
                        Else
                            drNew("TopBottom") = False
                        End If
                        drNew("SiteCode") = clsAdmin.SiteCode
                        drNew("CREATEDAT") = clsAdmin.SiteCode
                        drNew("CREATEDBY") = clsAdmin.UserName
                        drNew("CREATEDON") = objclsComman.GetCurrentDate()
                        drNew("UPDATEDAT") = clsAdmin.SiteCode
                        drNew("UPDATEDBY") = clsAdmin.UserName
                        drNew("UPDATEDON") = objclsComman.GetCurrentDate()
                        drNew("Status") = True
                        dtPosPrintingDetails.Rows.Add(drNew)
                        iSequenceNumber += 1
                        iperGridRow += 1
                    End If
                End If

            Next
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        'Dim result As DialogResult = MessageBox.Show("If you close current changes will lost .Do you want to continue ... ?", "PrintPage setup", MessageBoxButtons.YesNo)


        Dim result As DialogResult = MessageBox.Show(getValueByKey("PRNS003"), "PRNS003 - " & getValueByKey("CLAE04"), MessageBoxButtons.YesNo)

        If (result = Windows.Forms.DialogResult.Yes) Then
            Me.Close()
        ElseIf (result = Windows.Forms.DialogResult.No) Then

        End If
    End Sub
    Private Sub frmPrintingSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            Load_cboDocumentType()
            GridDataSource()
            CtrlTab1.TabPages(0).Show()
        Catch ex As Exception

            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
        SetCulture(Me, Me.Name)
    End Sub
    Private Sub GridDataSource()
        Try

            Dim objclsPrintingSettings As New clsPrintingSettings
            Dim adpPOSPrintingDeatils As New POSDBDataSetTableAdapters.PrintingDetailTableAdapter
            Dim DocType As String = cboDocumentType.SelectedValue
            adpPOSPrintingDeatils.Fill(dtPosPrintingDetails, DocType)
            dvTop = New DataView(dtPosPrintingDetails, "TopBottom='Top'", "", DataViewRowState.CurrentRows)
            gridTop.DataSource = dvTop
            dvBottom = New DataView(dtPosPrintingDetails, "TopBottom='Bottom'", "", DataViewRowState.CurrentRows)
            gridBottom.DataSource = dvBottom
            gridTop.DataSource = dvTop
            gridBottom.DataSource = dvBottom
            dvWelcome = New DataView(dtPosPrintingDetails, "TopBottom='WelCome'", "", DataViewRowState.CurrentRows)
            gridWelcome.DataSource = dvWelcome
            dvPromo = New DataView(dtPosPrintingDetails, "TopBottom='Promo'", "", DataViewRowState.CurrentRows)
            gridPromo.DataSource = dvPromo
            dvTax = New DataView(dtPosPrintingDetails, "TopBottom='Tax'", "", DataViewRowState.CurrentRows)
            gridTax.DataSource = dvTax
            iSrNo = objclsPrintingSettings.GetSRNumber()
            setGrid()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim objclsComman As New clsCommon
            Dim inewAddrowSrNo As Integer
            Dim isequenceno As Integer
            If Not (dtPosPrintingDetails Is Nothing) Then
                If (dtPosPrintingDetails.Rows.Count > 0) Then
                    Dim dvTop1 As DataView
                    Dim dtMax As New DataTable
                    dvTop1 = New DataView(dtPosPrintingDetails, "", "", DataViewRowState.CurrentRows)
                    dtMax = dvTop1.ToTable()

                    If Not dtMax Is Nothing Then
                        If (dtMax.Rows.Count > 0) Then
                            inewAddrowSrNo = dtMax.Compute("max(srno)", " ")
                            inewAddrowSrNo += 1
                            isequenceno = dtMax.Compute("max(sequenceno)", " ")
                            isequenceno += 1
                        Else
                            inewAddrowSrNo += 1
                            isequenceno += 1
                        End If
                    End If
                Else
                    inewAddrowSrNo += 1
                    isequenceno += 1
                End If

                Dim drPosPrintingDetails As POSDBDataSet.PrintingDetailRow
                drPosPrintingDetails = dtPosPrintingDetails.NewRow()
                drPosPrintingDetails("SrNo") = inewAddrowSrNo
                If (tpTop.CanFocus) Then
                    drPosPrintingDetails("TopBottom") = "Top"
                ElseIf (tpBottom.CanFocus) Then
                    drPosPrintingDetails("TopBottom") = "Bottom"
                ElseIf (tpWelcome.CanFocus) Then
                    drPosPrintingDetails("TopBottom") = "Welcome"
                ElseIf (tpPromo.CanFocus) Then
                    drPosPrintingDetails("TopBottom") = "Promo"
                ElseIf (tpPrinting.CanFocus) Then
                    drPosPrintingDetails("TopBottom") = "Tax"
                End If
                drPosPrintingDetails("ReceiptText") = ""
                drPosPrintingDetails("DocumentType") = cboDocumentType.SelectedValue
                drPosPrintingDetails("SequenceNo") = isequenceno
                drPosPrintingDetails("CREATEDAT") = clsAdmin.SiteCode
                drPosPrintingDetails("CREATEDBY") = clsAdmin.UserName
                drPosPrintingDetails("CREATEDON") = objclsComman.GetCurrentDate()
                drPosPrintingDetails("UPDATEDAT") = clsAdmin.SiteCode
                drPosPrintingDetails("UPDATEDBY") = clsAdmin.UserName
                drPosPrintingDetails("UPDATEDON") = objclsComman.GetCurrentDate()
                drPosPrintingDetails("Status") = True
                dtPosPrintingDetails.Rows.Add(drPosPrintingDetails)
                gridWelcome.DataSource = dvWelcome
                gridPromo.DataSource = dvPromo
                gridTax.DataSource = dvTax
                gridTop.DataSource = dvTop
                gridBottom.DataSource = dvBottom
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub setGrid()
        Try
            gridTop.Cols("ReceiptText").Width = 500
            gridTop.Cols("Align").Width = 70
            gridTop.Cols("Width").Width = 40
            gridTop.Cols("Height").Width = 40
            gridTop.Cols("Bold").Width = 40
            gridBottom.Cols("ReceiptText").Width = 500
            gridBottom.Cols("Align").Width = 70
            gridBottom.Cols("Width").Width = 40
            gridBottom.Cols("Height").Width = 40
            gridBottom.Cols("Bold").Width = 40
            gridTax.Cols("ReceiptText").Width = 500
            gridTax.Cols("Align").Width = 70
            gridTax.Cols("Width").Width = 40
            gridTax.Cols("Height").Width = 40
            gridTax.Cols("Bold").Width = 40
            gridWelcome.Cols("ReceiptText").Width = 500
            gridWelcome.Cols("Align").Width = 70
            gridWelcome.Cols("Width").Width = 40
            gridWelcome.Cols("Height").Width = 40
            gridWelcome.Cols("Bold").Width = 40
            gridPromo.Cols("ReceiptText").Width = 500
            gridPromo.Cols("Align").Width = 70
            gridPromo.Cols("Width").Width = 40
            gridPromo.Cols("Height").Width = 40
            gridPromo.Cols("Bold").Width = 40
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cboDocumentType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDocumentType.SelectedValueChanged
        GridDataSource()
    End Sub
    Private Sub gridTop_CellButtonClick(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles gridTop.CellButtonClick, gridBottom.CellButtonClick, gridPromo.CellButtonClick, gridWelcome.CellButtonClick, gridTax.CellButtonClick
        Try

            If Not (dtPosPrintingDetails Is Nothing) Then
                If (dtPosPrintingDetails.Rows.Count > 0) Then
                    If (tpTop.CanFocus) Then
                        gridTop.Redraw = False
                        Dim dvTop1 = New DataView(dtPosPrintingDetails, "TopBottom='Top'", "", DataViewRowState.CurrentRows)
                        dvTop1.Delete(e.Row - 1)
                        gridTop.Redraw = True
                        gridTop.Refresh()
                    ElseIf (tpBottom.CanFocus) Then
                        gridBottom.Redraw = False
                        Dim dvTop1 = New DataView(dtPosPrintingDetails, "TopBottom='Bottom'", "", DataViewRowState.CurrentRows)
                        dvTop1.Delete(e.Row - 1)
                        gridBottom.Redraw = True

                        gridTop.Refresh()
                    ElseIf (tpWelcome.CanFocus) Then
                        gridBottom.Redraw = False
                        Dim dvTop1 = New DataView(dtPosPrintingDetails, "TopBottom='Welcome'", "", DataViewRowState.CurrentRows)
                        dvTop1.Delete(e.Row - 1)
                        gridBottom.Redraw = True

                        gridTop.Refresh()
                    ElseIf (tpPromo.CanFocus) Then
                        gridBottom.Redraw = False
                        Dim dvTop1 = New DataView(dtPosPrintingDetails, "TopBottom='Promo'", "", DataViewRowState.CurrentRows)
                        dvTop1.Delete(e.Row - 1)
                        gridBottom.Redraw = True

                        gridTop.Refresh()
                    ElseIf (tpPrinting.CanFocus) Then
                        gridBottom.Redraw = False
                        Dim dvTop1 = New DataView(dtPosPrintingDetails, "TopBottom='Tax'", "", DataViewRowState.CurrentRows)
                        dvTop1.Delete(e.Row - 1)
                        gridBottom.Redraw = True

                        gridTop.Refresh()
                    End If

                Else
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Private Sub frmNPrintingSettings_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "print-setup.htm")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function Themechange()
        'Me.Size = New Size(847, 410)
        Me.BackColor = Color.FromArgb(134, 134, 134)
        'lblDocumentType.ForeColor = Color.Black
        lblDocumentType.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        lblDocumentType.BorderStyle = BorderStyle.None

        gridTop.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        gridTop.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        gridTop.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        gridTop.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        gridTop.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridTop.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridTop.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridTop.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)

        gridBottom.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        gridBottom.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        gridBottom.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        gridBottom.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        gridBottom.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridBottom.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridBottom.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridBottom.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)


        gridWelcome.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        gridWelcome.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        gridWelcome.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        gridWelcome.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        gridWelcome.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridWelcome.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridWelcome.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridWelcome.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)

        gridPromo.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        gridPromo.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        gridPromo.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        gridPromo.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        gridPromo.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridPromo.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridPromo.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridPromo.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)

        gridTax.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        gridTax.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        gridTax.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        gridTax.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        gridTax.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridTax.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridTax.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        gridTax.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)



        btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnSave.BackColor = Color.Transparent
        btnSave.BackColor = Color.FromArgb(0, 107, 163)
        btnSave.ForeColor = Color.FromArgb(255, 255, 255)
        btnSave.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnSave.Size = New Size(85, 30)

        btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnCancel.BackColor = Color.Transparent
        btnCancel.BackColor = Color.FromArgb(0, 107, 163)
        btnCancel.ForeColor = Color.FromArgb(255, 255, 255)
        btnCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnCancel.Size = New Size(85, 30)

        btnAdd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnAdd.BackColor = Color.Transparent
        btnAdd.BackColor = Color.FromArgb(0, 107, 163)
        btnAdd.ForeColor = Color.FromArgb(255, 255, 255)
        btnAdd.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnAdd.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnAdd.FlatStyle = FlatStyle.Flat
        btnAdd.FlatAppearance.BorderSize = 0
        btnAdd.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnAdd.Size = New Size(85, 30)

        tpBottom.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        tpBottom.TabForeColorSelected = Color.FromArgb(255, 255, 255)
        tpGeneral.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        tpGeneral.TabForeColorSelected = Color.FromArgb(255, 255, 255)

        tpPrinting.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        tpPrinting.TabForeColorSelected = Color.FromArgb(255, 255, 255)

        tpPrinting.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        tpPrinting.TabForeColorSelected = Color.FromArgb(255, 255, 255)

        tpPromo.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        tpPromo.TabForeColorSelected = Color.FromArgb(255, 255, 255)

        tpTop.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        tpTop.TabForeColorSelected = Color.FromArgb(255, 255, 255)

        tpWelcome.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        tpWelcome.TabForeColorSelected = Color.FromArgb(255, 255, 255)
    End Function
End Class
