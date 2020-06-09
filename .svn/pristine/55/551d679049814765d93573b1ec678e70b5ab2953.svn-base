Imports SpectrumBL

Public Class ctrlCreditCard
    Private _clsSiteBank As clsSiteBank
    Public Property ClsSiteBank As clsSiteBank
        Get
            Return _clsSiteBank
        End Get
        Set(value As clsSiteBank)
            _clsSiteBank = value
        End Set
    End Property

    Private Sub txtCreditCardNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCreditCardNo.KeyDown
        Try
            If (e.KeyValue >= 48 And e.KeyValue < 58) Or (e.KeyValue >= 96 And e.KeyValue < 106) Or e.KeyValue = 8 Or e.KeyValue = 32 Or e.KeyValue = 46 Then
                e.SuppressKeyPress = False
            Else
                e.SuppressKeyPress = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtAuthCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If (e.KeyValue >= 48 And e.KeyValue < 58) Or (e.KeyValue >= 96 And e.KeyValue < 106) Or e.KeyValue = 8 Or e.KeyValue = 32 Or e.KeyValue = 46 Then
                e.SuppressKeyPress = False
            Else
                e.SuppressKeyPress = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtSlipNO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If (e.KeyValue >= 48 And e.KeyValue < 58) Or (e.KeyValue >= 96 And e.KeyValue < 106) Or e.KeyValue = 8 Or e.KeyValue = 32 Or e.KeyValue = 46 Then
                e.SuppressKeyPress = False
            Else
                e.SuppressKeyPress = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public ReadOnly Property DateValid() As Boolean
        Get
            Try
                Dim PreviousDay, Futureday As DateTime
                PreviousDay = DateAdd(DateInterval.Month, -6, Now)
                Futureday = DateAdd(DateInterval.Month, 6, Now)
                If Not dtpExpiryDate.Value Is DBNull.Value Then
                    If DateDiff(DateInterval.Day, Now, dtpExpiryDate.Value) < 0 Then
                        'ShowMessage("Voucher is already expired", "Information")
                        ShowMessage(getValueByKey("CLCRDC01"), "CLCRDC01" - getValueByKey("CLAE04"))
                        dtpExpiryDate.Value = Now
                        Return False
                    End If
                End If
                Return True
            Catch ex As Exception
            End Try
        End Get
    End Property

    Private Sub ctrlCreditCard_Load() Handles Me.Load
        Try
            'ClsSiteBank = New clsSiteBank()
            'cmbBankName.ValueMember = "BankAccNo"
            'cmbBankName.DisplayMember = "BankName"
            'Dim dt As DataTable = ClsSiteBank.GetBankNames(clsAdmin.SiteCode, True)
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    cmbBankName.DataSource = dt
            '    pC1ComboSetDisplayMember(cmbBankName)
            '    cmbBankName.SelectedIndex = 0
            'End If
            '--- Commented by Mahesh use cheqe code ::Instuct by Rakesh Sir issue : 9922
            Dim clsCommon As New SpectrumBL.clsCommon()
            Dim bankDetails = clsCommon.GetBankDetails(clsAdmin.SiteCode)

            If bankDetails IsNot Nothing AndAlso bankDetails.Rows.Count > 0 Then
                cmbBankName.DataSource = bankDetails
                cmbBankName.DisplayMember = "BankName"
                cmbBankName.ValueMember = "BankAccNo"
                cmbBankName.Splits(0).DisplayColumns(0).Visible = False
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function Themechange()
        lblBankName.BackColor = Color.Transparent
        lblBankName.AutoSize = False
        lblBankName.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblBankName.ForeColor = Color.White
        lblCreditCardNo.BackColor = Color.Transparent
        lblCreditCardNo.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblCreditCardNo.ForeColor = Color.White
        lblExpiryDate.BackColor = Color.Transparent
        lblExpiryDate.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblExpiryDate.ForeColor = Color.White
    End Function
End Class
