Imports SpectrumBL

Public Class ctrlChequeDetails
    Private _clsSiteBank As clsSiteBank
    Public Property ClsSiteBank As clsSiteBank
        Get
            Return _clsSiteBank
        End Get
        Set(value As clsSiteBank)
            _clsSiteBank = value
        End Set
    End Property
    Private Sub ctrlChequeDetails_Load() Handles Me.Load
        Try
            Dim clsCommon As New SpectrumBL.clsCommon()
            Dim bankDetails = clsCommon.GetBankDetails(clsAdmin.SiteCode)

            cmbBankName.DataSource = bankDetails
            cmbBankName.DisplayMember = "BankName"
            cmbBankName.ValueMember = "BankAccNo"
            cmbBankName.Splits(0).DisplayColumns(0).Visible = False
        Catch ex As Exception
            LogException(ex)
        End Try
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
        End If
    End Sub
    Private Function Themechange()
        Me.BackColor = Color.FromArgb(134, 134, 134)
        chequeLayout.BackColor = Color.FromArgb(134, 134, 134)
        lblBankName.BackColor = Color.Transparent
        lblBankName.AutoSize = False
        lblBankName.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblBankName.ForeColor = Color.White

        lblChequeDate.BackColor = Color.Transparent
        lblChequeDate.AutoSize = False
        lblChequeDate.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblChequeDate.ForeColor = Color.White

        lblChequeNo.BackColor = Color.Transparent
        lblChequeNo.AutoSize = False
        lblChequeNo.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblChequeNo.ForeColor = Color.White

        lblMicrNumber.BackColor = Color.Transparent
        lblMicrNumber.AutoSize = False
        lblMicrNumber.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblMicrNumber.ForeColor = Color.White
    End Function
End Class
