Imports SpectrumBL

Public Class BatchBarcodeList
    Private BList As List(Of SpectrumCommon.BtachbarcodeInfo)
    Private _OrderNo As String
    Public Property OrderNo() As String
        Get
            Return _OrderNo
        End Get
        Set(ByVal value As String)
            _OrderNo = value
        End Set
    End Property
    Private TransType As SpectrumCommon.TransactionType

    Public Property BarcodeList() As List(Of SpectrumCommon.BtachbarcodeInfo)
        Get
            Return BList
        End Get
        Set(ByVal value As List(Of SpectrumCommon.BtachbarcodeInfo))
            BList = value
        End Set
    End Property
    Public Sub New(ByVal BarcodeList As List(Of SpectrumCommon.BtachbarcodeInfo), ByVal Transaction As SpectrumCommon.TransactionType)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        dgBatchbarcode.AutoGenerateColumns = False
        BList = BarcodeList
        RefreshBinding()
        btnOK.DialogResult = Windows.Forms.DialogResult.OK
        btnCancel.DialogResult = Windows.Forms.DialogResult.Cancel
        TransType = Transaction
        Me.StartPosition = FormStartPosition.CenterParent
        Me.ControlBox = False

        Me.MaximizeBox = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub
    Private Sub btnOK_Click(sender As System.Object, e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub
    Private Sub RefreshBinding()
        If BList.Count > 0 Then
            dgBatchbarcode.DataSource = BList.Where(Function(w) w.Status = True).ToList()

            dgBatchbarcode.AutoResizeColumns()
        End If
    End Sub
    Private Sub dgBatchbarcode_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgBatchbarcode.CellClick
        If e.ColumnIndex = 0 AndAlso e.RowIndex <> -1 Then
            BList.Where(Function(w) w.BatchBarcode = dgBatchbarcode.SelectedRows(0).Cells("Barcode").Value).FirstOrDefault().Status = False
            BList.Where(Function(w) w.BatchBarcode = dgBatchbarcode.SelectedRows(0).Cells("Barcode").Value).FirstOrDefault().Qty = 0
            RefreshBinding()
        End If
    End Sub
    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub dgBatchbarcode_CellValidating(sender As Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgBatchbarcode.CellValidating

        If TransType = SpectrumCommon.TransactionType.SalesOrderReturn Then
            If e.ColumnIndex = dgBatchbarcode.Columns("PickupQty").Index Then
                Dim comm As New SpectrumBL.clsCommon()
                If e.FormattedValue = "" OrElse Not IsNumeric(e.FormattedValue) Then
                    e.Cancel = True
                    ShowMessage(True, getValueByKey("batchbarcode.Validation001"), getValueByKey("CLAE04"))
                    dgBatchbarcode.CancelEdit()
                    Exit Sub
                End If
                Dim clsso As New clsSalesOrder()
                Dim dtbarcodes = clsso.GetOutboundData(clsAdmin.SiteCode, OrderNo)
                If dtbarcodes IsNot Nothing AndAlso dtbarcodes.Rows.Count > 0 Then
                    If dtbarcodes.AsEnumerable().Where(Function(w) w("BarCode").ToString() = BList(e.RowIndex).BatchBarcode.ToString()).Count() > 0 Then
                        If BList.Any(Function(w) w.BatchBarcode = BList(e.RowIndex).BatchBarcode.ToString()) Then
                            If dtbarcodes.AsEnumerable().Where(Function(w) w("BarCode").ToString() = BList(e.RowIndex).BatchBarcode.ToString()).FirstOrDefault()("DeliveredQty") < CInt(e.FormattedValue) Then
                                ShowMessage(True, getValueByKey("batchbarcode.Validation003"), getValueByKey("CLAE04"))
                                e.Cancel = True
                                dgBatchbarcode.CancelEdit()
                            End If

                        End If

                    Else
                        ShowMessage(True, getValueByKey("BatchBarcode004"), getValueByKey("CLAE04"))
                        e.Cancel = True
                        dgBatchbarcode.CancelEdit()
                    End If

                End If

            End If

        ElseIf TransType = SpectrumCommon.TransactionType.OutBoundCreation Then
            If e.ColumnIndex = dgBatchbarcode.Columns("PickupQty").Index Then
                Dim comm As New SpectrumBL.clsCommon()
                If e.FormattedValue = "" OrElse Not IsNumeric(e.FormattedValue) Then
                    e.Cancel = True
                    ShowMessage(True, getValueByKey("batchbarcode.Validation001"), getValueByKey("CLAE04"))
                    dgBatchbarcode.CancelEdit()
                    Exit Sub
                End If

                If (e.FormattedValue > BList(e.RowIndex).OrderQty) Then
                    e.Cancel = True
                    ShowMessage(getValueByKey("SO009"), "SO009 - " & getValueByKey("CLAE04"))
                    dgBatchbarcode.CancelEdit()
                    Exit Sub
                End If

                If (comm.GetStocks(clsAdmin.SiteCode, BList(e.RowIndex).EAN, BList(e.RowIndex).ArticleCode, True, clsDefaultConfiguration.IsBatchManagementReq, BList(e.RowIndex).BatchBarcode) < e.FormattedValue) Then
                    e.Cancel = True
                    ShowMessage(True, getValueByKey("BatchBarcode003"), getValueByKey("CLAE04"))
                    dgBatchbarcode.CancelEdit()
                    Exit Sub
                End If
            End If
        Else
            If e.ColumnIndex = dgBatchbarcode.Columns("PickupQty").Index Then
                Dim comm As New SpectrumBL.clsCommon()
                If e.FormattedValue = "" OrElse Not IsNumeric(e.FormattedValue) Then
                    e.Cancel = True
                    ShowMessage(True, getValueByKey("batchbarcode.Validation001"), getValueByKey("CLAE04"))
                    dgBatchbarcode.CancelEdit()
                    Exit Sub
                End If

                If (comm.GetStocks(clsAdmin.SiteCode, BList(e.RowIndex).EAN, BList(e.RowIndex).ArticleCode, True, clsDefaultConfiguration.IsBatchManagementReq, BList(e.RowIndex).BatchBarcode) < e.FormattedValue) Then
                    e.Cancel = True
                    ShowMessage(True, getValueByKey("BatchBarcode003"), getValueByKey("CLAE04"))
                    dgBatchbarcode.CancelEdit()
                    Exit Sub
                End If
            End If
        End If


    End Sub

    Private Sub BatchBarcodeList_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        dgBatchbarcode.Columns("PickupQty").HeaderText = getValueByKey("frmnsalesorderupdate.grdsoitems.pickupqty")
    End Sub
End Class