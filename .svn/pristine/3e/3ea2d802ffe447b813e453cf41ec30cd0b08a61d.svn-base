Public Class CtrlSalesInfo

    Enum SOActionValue
        SONEW
        SOEDIT
        SOCANCEL
    End Enum

    Dim _SOAction As SOActionValue
    Public Property SOAction() As SOActionValue
        Get
            Return _SOAction
        End Get
        Set(ByVal value As SOActionValue)
            _SOAction = value
        End Set
    End Property

    Public Sub pCtrlEditable()
        If _SOAction = SOActionValue.SONEW Then
            CtrlTxtOrderNo.Enabled = True
            CtrldtOrderDt.Enabled = True
            CtrlDtExpDelDate.Enabled = True
            CtrlTxtCustOrdRef.Enabled = True
            CtrlTxtRemarks.Enabled = True
            CtrlTxtInvoice.Enabled = True
            CtrlBtn1.Visible = False
        ElseIf _SOAction = SOActionValue.SOEDIT Then
            CtrlTxtOrderNo.Enabled = True
            CtrldtOrderDt.Enabled = False
            CtrlDtExpDelDate.Enabled = False
            CtrlTxtCustOrdRef.Enabled = True
            CtrlTxtRemarks.Enabled = False
            CtrlTxtInvoice.Enabled = False
            CtrlBtn1.Visible = True
        End If
    End Sub

    'Public Sub pCtrlClear()
    '    CtrlTxtOrderNo.Clear()
    '    CtrldtOrderDt.Value = Now.Date
    '    CtrlDtExpDelDate.Value = Now.Date()
    '    CtrlTxtCustOrdRef.Clear()
    '    CtrlTxtRemarks.Clear()
    '    CtrlTxtInvoice.Clear()
    'End Sub


    Private Sub CtrlSalesInfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pCtrlEditable()
        'pCtrlClear()
        'If C1Sizer1.Grid.Columns.Count = 4 And SOAction = SOActionValue.SONEW Then
        '    C1Sizer1.Grid.Columns.Remove(3)
        'End If
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
        End If
    End Sub
    'Public Sub pEditSO()
    '    If _SOAction = SOActionValue.SOEDIT Then
    '        CtrlTxtOrderNo.Enabled = True
    '        CtrldtOrderDt.Enabled = False
    '        CtrlDtExpDelDate.Enabled = True
    '        CtrlTxtCustOrdRef.Enabled = True
    '        CtrlTxtRemarks.Enabled = True
    '        CtrlTxtInvoice.Enabled = True
    '    End If
    'End Sub
    Private Function Themechange()
     
        'Me.CtrlLabel2.BackColor = Color.Silver
        C1Sizer1.Grid.Rows(5).Size = 21
        Me.lblSalesOrderNo.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblSalesOrderNo.Font = New Font("NeoSans", 7, FontStyle.Regular)
        Me.lblSalesOrderNo.TextAlign = ContentAlignment.TopLeft
        Me.lblSalesOrderNo.ForeColor = Color.FromArgb(37, 37, 37)
        Me.lblSalesOrderNo.Text = Me.lblSalesOrderNo.Text.ToUpper
        lblSalesOrderNo.BorderStyle = Windows.Forms.BorderStyle.None

        Me.CtrlTxtOrderNo.BackColor = Color.FromArgb(255, 255, 255)
        Me.CtrlTxtOrderNo.BringToFront()
        Me.CtrlTxtOrderNo.BackColor = Color.FromArgb(255, 255, 255)
        Me.CtrlTxtOrderNo.ForeColor = Color.FromArgb(37, 37, 37)
        Me.CtrlTxtOrderNo.Font = New Font("Neo Sans", 8, FontStyle.Regular)
      

        Me.lblOrderDate.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblOrderDate.Font = New Font("NeoSans", 7, FontStyle.Regular)
        Me.lblOrderDate.TextAlign = ContentAlignment.TopLeft
        Me.lblOrderDate.ForeColor = Color.FromArgb(37, 37, 37)
        Me.lblOrderDate.Text = Me.lblOrderDate.Text.ToUpper
        lblOrderDate.BorderStyle = Windows.Forms.BorderStyle.None

        Me.CtrldtOrderDt.BackColor = Color.FromArgb(255, 255, 255)
        Me.CtrldtOrderDt.BringToFront()
        Me.CtrldtOrderDt.BackColor = Color.FromArgb(255, 255, 255)
        Me.CtrldtOrderDt.ForeColor = Color.FromArgb(37, 37, 37)
        Me.CtrldtOrderDt.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        Me.CtrldtOrderDt.Size = New Size(231, 16)
        Me.lblEpectedDeliveryDt.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblEpectedDeliveryDt.Font = New Font("NeoSans", 7, FontStyle.Regular)
        Me.lblEpectedDeliveryDt.TextAlign = ContentAlignment.TopLeft
        Me.lblEpectedDeliveryDt.ForeColor = Color.FromArgb(37, 37, 37)
        Me.lblEpectedDeliveryDt.Text = Me.lblEpectedDeliveryDt.Text.ToUpper
        lblEpectedDeliveryDt.BorderStyle = Windows.Forms.BorderStyle.None



        Me.CtrlDtExpDelDate.BackColor = Color.FromArgb(255, 255, 255)
        Me.CtrlDtExpDelDate.BringToFront()
        Me.CtrlDtExpDelDate.BackColor = Color.FromArgb(255, 255, 255)
        Me.CtrlDtExpDelDate.ForeColor = Color.FromArgb(37, 37, 37)
        Me.CtrlDtExpDelDate.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        Me.CtrlDtExpDelDate.Size = New Size(231, 18)

        Me.lblOrderRef.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblOrderRef.Font = New Font("NeoSans", 7, FontStyle.Regular)
        Me.lblOrderRef.TextAlign = ContentAlignment.TopLeft
        Me.lblOrderRef.ForeColor = Color.FromArgb(37, 37, 37)
        Me.lblOrderRef.Text = Me.lblOrderRef.Text.ToUpper
        lblOrderRef.BorderStyle = Windows.Forms.BorderStyle.None

        Me.CtrlTxtCustOrdRef.BackColor = Color.FromArgb(255, 255, 255)
        Me.CtrlTxtCustOrdRef.BringToFront()
        Me.CtrlTxtCustOrdRef.BackColor = Color.FromArgb(255, 255, 255)
        Me.CtrlTxtCustOrdRef.ForeColor = Color.FromArgb(37, 37, 37)
        Me.CtrlTxtCustOrdRef.Font = New Font("Neo Sans", 8, FontStyle.Regular)

        Me.lblRemarks.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblRemarks.Font = New Font("NeoSans", 7, FontStyle.Regular)
        Me.lblRemarks.TextAlign = ContentAlignment.TopLeft
        Me.lblRemarks.ForeColor = Color.FromArgb(37, 37, 37)
        Me.lblRemarks.Text = Me.lblRemarks.Text.ToUpper
        lblRemarks.BorderStyle = Windows.Forms.BorderStyle.None

        Me.CtrlTxtRemarks.BackColor = Color.FromArgb(255, 255, 255)
        Me.CtrlTxtRemarks.BringToFront()
        Me.CtrlTxtRemarks.BackColor = Color.FromArgb(255, 255, 255)
        Me.CtrlTxtRemarks.ForeColor = Color.FromArgb(37, 37, 37)
        Me.CtrlTxtRemarks.Font = New Font("Neo Sans", 8, FontStyle.Regular)



        Me.lblInvoiceTo.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblInvoiceTo.Font = New Font("NeoSans", 7, FontStyle.Regular)
        Me.lblInvoiceTo.TextAlign = ContentAlignment.TopLeft
        Me.lblInvoiceTo.ForeColor = Color.FromArgb(37, 37, 37)
        Me.lblInvoiceTo.Text = Me.lblInvoiceTo.Text.ToUpper
        lblInvoiceTo.BorderStyle = Windows.Forms.BorderStyle.None

        Me.CtrlTxtInvoice.BackColor = Color.FromArgb(255, 255, 255)
        Me.CtrlTxtInvoice.BringToFront()
        Me.CtrlTxtInvoice.BackColor = Color.FromArgb(255, 255, 255)
        Me.CtrlTxtInvoice.ForeColor = Color.FromArgb(37, 37, 37)
        Me.CtrlTxtInvoice.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        Me.CtrlTxtInvoice.Size = New Size(231, 20)

        CtrlBtn1.Image = Nothing
        CtrlBtn1.BackgroundImage = My.Resources.SearchItems1
        CtrlBtn1.VisualStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtn1.BackgroundImageLayout = ImageLayout.Stretch
        CtrlBtn1.FlatAppearance.BorderSize = 0
        CtrlBtn1.FlatStyle = FlatStyle.Flat
    End Function
End Class
