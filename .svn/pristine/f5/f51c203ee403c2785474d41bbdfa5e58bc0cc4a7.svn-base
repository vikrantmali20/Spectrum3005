Imports SpectrumBL
Public Class ctrlCheque
    Dim _typeOfPayment As String
    Dim dtGVDet As DataTable
    Dim dvGV As DataView
    Public ReadOnly Property DateValid() As Boolean
        Get
            Try
                Dim PreviousDay, Futureday As DateTime
                PreviousDay = DateAdd(DateInterval.Month, (clsDefaultConfiguration.CheckExpiryMonth * -1), Now)
                Futureday = DateAdd(DateInterval.Month, clsDefaultConfiguration.CheckExpiryMonth, Now)
                If Not dtpExpiryDate.Value Is DBNull.Value Then
                    If lblChequeNo.Tag = "Cheque No" Then
                        If dtpExpiryDate.Value > Futureday Or dtpExpiryDate.Value < PreviousDay Then
                            'ShowMessage("Cheque is not Acceptable", "Information")
                            MsgBox(getValueByKey("CLCHQ01"), getValueByKey("CLAE04"))
                            dtpExpiryDate.Value = Now
                            Return False
                        End If
                    Else
                        If DateDiff(DateInterval.Day, Now, dtpExpiryDate.Value) < 0 Then
                            'ShowMessage("Voucher is already expired", "Information")
                            MsgBox(getValueByKey("CLCHQ02"), getValueByKey("CLAE04"))
                            dtpExpiryDate.Value = Now
                            Return False
                        End If
                    End If
                End If
                Return True
            Catch ex As Exception
            End Try
        End Get
    End Property
    Public Property PaymentType() As String
        Get
            Return _typeOfPayment
        End Get
        Set(ByVal value As String)
            _typeOfPayment = value
            If _typeOfPayment = "GiftVoucher" Then
                'lblChequeNo.Text = "Gift Voucher No"
                'lblExpiryDate.Text = "Expiry Date"
                lblChequeNo.Text = getValueByKey("frmnacceptpayment.ctrlpaycheque.lblchequeno_gftvchr")
                lblExpiryDate.Text = getValueByKey("frmnacceptpayment.ctrlpaycheque.lblexpirydate_gftvchr")
                ctrlGiftVouc.Visible = True
                cmbVoucherProgram.Visible = True
                LoadGiftVoucherProgram()
            ElseIf _typeOfPayment = "CreditVouc" Then
                'lblChequeNo.Text = "Credit Voucher No"
                'lblExpiryDate.Text = "Credit Date"
                lblChequeNo.Text = getValueByKey("frmnacceptpayment.ctrlpaycheque.lblchequeno_crdvchr")
                lblExpiryDate.Text = getValueByKey("frmnacceptpayment.ctrlpaycheque.lblexpirydate_crdvchr")
                ctrlGiftVouc.Visible = False
                cmbVoucherProgram.Visible = False
            ElseIf _typeOfPayment = "Cheque" Then
                'lblChequeNo.Text = "Cheque No"
                'lblExpiryDate.Text = "Date"
                lblChequeNo.Text = getValueByKey("frmnacceptpayment.ctrlpaycheque.lblchequeno")
                lblExpiryDate.Text = getValueByKey("frmnacceptpayment.ctrlpaycheque.lblexpirydate")
                lblChequeNo.Tag = "Cheque No"
                ctrlGiftVouc.Visible = False
                cmbVoucherProgram.Visible = False
            End If
        End Set
    End Property
    Private Sub txtChequeNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtChequeNo.KeyDown

        Try
            If _typeOfPayment <> "GiftVoucher" Then
                If (e.KeyValue >= 48 And e.KeyValue < 58) Or (e.KeyValue >= 65 And e.KeyValue <= 90) Or (e.KeyValue >= 96 And e.KeyValue < 106) Or e.KeyValue = 8 Or e.KeyValue = 32 Or e.KeyValue = 46 Or e.KeyValue = 191 Or e.KeyValue = 220 Or e.KeyValue = 189 Then
                    e.SuppressKeyPress = False
                Else
                    e.SuppressKeyPress = True
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub LoadGiftVoucherProgram()
        Try
            'Rakesh-24.09.2013:Issue-7932=>Check GV program exist or not

            Dim obj As New clsAdvanceSale()
            dtGVDet = obj.GetVoucherProg(clsAdmin.SiteCode, NegativeTenderType.GiftVoucherI.ToString())
            dvGV = New DataView(dtGVDet, "", "VOUCHERCODE", DataViewRowState.CurrentRows)
            If Not dtGVDet Is Nothing Then
                cmbVoucherProgram.DataSource = dvGV
                cmbVoucherProgram.DisplayMember = "VOUCHERDESC"
                cmbVoucherProgram.ValueMember = "VOUCHERCODE"
                pC1ComboSetDisplayMember(cmbVoucherProgram)

                If (dtGVDet.Rows.Count > 0) Then
                    cmbVoucherProgram.SelectedIndex = 0
                    'Else
                    '    ShowMessage(getValueByKey("CM063"), getValueByKey("CLAE04"))
                End If

            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub

    Private Sub ctrlCheque_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dtpExpiryDate.Value = Now
    End Sub
End Class
