Imports System.Resources
Imports System.Globalization
Imports SpectrumBL
Imports C1.Win.C1FlexGrid

''' <summary>
'''  Saving printing settings
''' </summary>
''' <remarks></remarks>
''' 
Public Class frmPrintingSettings
    Private _receiptTextMaxLength As Integer

    'Private ReadOnly Property IsReceiptTextMaxLength() As Boolean
    '    Get
    '        Try
    '            If Not (txtReceiptTextMaxLength.Text = String.Empty) Then
    '                Dim iReceiptTextMaxLen As Integer = CInt(txtReceiptTextMaxLength.Text)
    '                Return True
    '            Else
    '                MessageBox.Show("Enter Receipt Text Max length.")
    '                Return False
    '            End If
    '        Catch ex As Exception
    '            MessageBox.Show("Only Integer number is allowed")
    '            Return False
    '        End Try
    '    End Get
    'End Property


    Private Function LoadItemNoReceipt() As Boolean
        cboItemNoReceipt.Items.Add("ItemCode")
        cboItemNoReceipt.Items.Add("ItemCode and BarCode")
        Return True
    End Function

    Private Function Load_cboDocumentType() As Boolean
        Dim objclsPrintingsettings As New clsPrintingSettings
        Dim dtDocumentType As DataTable = objclsPrintingsettings.GetDocumentType()
        If Not dtDocumentType Is Nothing Then
            cboDocumentType.DataSource = dtDocumentType
            cboDocumentType.DisplayMember = dtDocumentType.Columns("DocumentTypeDesc").ColumnName
            cboDocumentType.ValueMember = dtDocumentType.Columns("DocumentType").ColumnName
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
        Catch ex As Exception
            MessageBox.Show(getValueByKey("PRNS002"), "PRNS002 - " & getValueByKey("CLAE04"))
        End Try

    End Sub

    Dim iSrNo As Integer
    Dim iperGridRow As Integer = iSrNo + 1


    Private Sub GridData()
        Try
            Dim dtGirdTop As DataTable = gridTop.DataSource
            AddRows(gridTop)
            AddRows(gridBottom, False)
            Dim adpPosPrintingDetails As New POSDBDataSetTableAdapters.PrintingDetailTableAdapter
            adpPosPrintingDetails.Update(dtPosPrintingDetails)
            MessageBox.Show(getValueByKey("PRNS004"), "PRNS004 - " & getValueByKey("CLAE04"))
            gridBottom.Clear(ClearFlags.Content, 1, 1, 14, 5)
            gridTop.Clear(ClearFlags.Content, 1, 1, 14, 5)
            MessageBox.Show(getValueByKey("PRNS001"), "PRNS001 - " & getValueByKey("CLAE04"))
        Catch ex As Exception
            MessageBox.Show(getValueByKey("PRNS002"), "PRNS002 - " & getValueByKey("CLAE05"))
        End Try
    End Sub
    Dim iSequenceNumber As Integer = 1
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
        Dim result As DialogResult = MessageBox.Show(getValueByKey("PRNS003"), "PRNS003 - " & getValueByKey("CLAE04"), MessageBoxButtons.YesNo)
        If (result = Windows.Forms.DialogResult.Yes) Then
            Me.Close()
        ElseIf (result = Windows.Forms.DialogResult.No) Then

        End If
    End Sub
    Dim dtPosPrintingDetails As New POSDBDataSet.PrintingDetailDataTable
    Dim dvTop As DataView
    Dim dvBottom As DataView
    Dim dvWelcome, dvPromo, dvTax As DataView
    Private Sub frmPrintingSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Load_cboDocumentType()
            GridDataSource()

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
            Dim dataRow As DataRowView = cboDocumentType.SelectedItem
            adpPOSPrintingDeatils.Fill(dtPosPrintingDetails, dataRow.Item("DocumentType").ToString())
            dvTop = New DataView(dtPosPrintingDetails, "TopBottom='Top'", "", DataViewRowState.CurrentRows)
            If (dvTop.Count > 0) Then
                gridTop.DataSource = dvTop
            End If
            dvBottom = New DataView(dtPosPrintingDetails, "TopBottom='Bottom'", "", DataViewRowState.CurrentRows)
            If (dvBottom.Count > 0) Then
                gridBottom.DataSource = dvBottom
            End If
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
    Private Sub cboDocumentType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDocumentType.SelectedIndexChanged
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
End Class