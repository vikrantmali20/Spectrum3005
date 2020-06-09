Imports SpectrumBL
Public Class CtrlGiftVoucher
    Dim dtGVDet As DataTable
    Dim dvGV As DataView
    Dim _ExpiryDate As DateTime
    Public ReadOnly Property ExpiryDate() As DateTime
        Get
            Return _ExpiryDate
        End Get
    End Property
    Private Sub CtrlGiftVoucher_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim obj As New clsAdvanceSale()
            dtGVDet = obj.GetVoucherProg(clsAdmin.SiteCode, NegativeTenderType.GiftVoucherI.ToString())
            If Not dtGVDet Is Nothing And dtGVDet.Rows.Count > 0 Then
                dvGV = New DataView(dtGVDet, "ISPREPRINTED=False", "VOUCHERCODE", DataViewRowState.CurrentRows)
                If Not dtGVDet Is Nothing Then
                    cmbVoucherProgram.DataSource = dvGV
                    cmbVoucherProgram.DisplayMember = "VOUCHERDESC"
                    cmbVoucherProgram.ValueMember = "VOUCHERCODE"
                    pC1ComboSetDisplayMember(cmbVoucherProgram)
                    If cmbVoucherProgram.ListCount > 0 Then
                        cmbVoucherProgram.SelectedIndex = 0
                    End If
                End If
            End If
        Catch ex As Exception
            'ShowMessage(ex.Message, "Error")
            LogException(ex)
        End Try
    End Sub

    Private Sub cmbVoucherProgram_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVoucherProgram.SelectedValueChanged
        Try
            If Not dtGVDet Is Nothing And dtGVDet.Rows.Count > 0 Then
                Dim dv As New DataView(dtGVDet, "VOUCHERCODE='" & cmbVoucherProgram.SelectedValue & "'", "", DataViewRowState.CurrentRows)
                If dv.Count > 0 Then
                    Dim i As Int32 = 0
                    i = IIf(dv(0)("ExpiryAfterDays") Is DBNull.Value, 0, dv(0)("ExpiryAfterDays"))
                    _ExpiryDate = DateAdd(DateInterval.Day, i, clsAdmin.CurrentDate)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class
