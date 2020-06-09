Public Class CtrlCustDtls

    Public Sub CtrlLabel3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblMoreInfo.Click
        Try
            If Not _dtCustmInfo Is Nothing AndAlso _dtCustmInfo.Rows.Count > 0 Then
                Dim objfrmCustomerDeatils As New frmCustomerDetails(_dtCustmInfo)
                objfrmCustomerDeatils.ShowDialog()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private _DisplayCustType As Boolean
    Public Property DisplayCustType() As Boolean
        Get
            Return _DisplayCustType
        End Get
        Set(ByVal value As Boolean)
            _DisplayCustType = value
            If _DisplayCustType = False Then
                If C1Sizer1.Grid.Rows.Count = 6 Then
                    cboAddrType.Location = lblCustNoValue.Location
                    cboAddrType.Width = lblCustNoValue.Width
                    lblCustType.Visible = False
                    lblCustTypeValue.Visible = False
                    lblCustNo.Visible = False
                    lblCustNoValue.Visible = False
                    C1Sizer1.Grid.Rows(1).Size = 0
                    C1Sizer1.Grid.AutoGenerate()
                End If
            Else
                lblCustType.Visible = True
                lblCustTypeValue.Visible = True
                lblCustNo.Visible = True
                lblCustNoValue.Visible = True
                cboAddrType.Left = lblCustTypeValue.Right
                C1Sizer1.Grid.Rows(1).Size = 25
                C1Sizer1.Grid.AutoGenerate()
            End If
        End Set
    End Property
    Public _dtCustmInfo As DataTable
    Public Property dtCustmInfo() As DataTable
        Get
            Return _dtCustmInfo
        End Get
        Set(ByVal value As DataTable)
            _dtCustmInfo = value
        End Set
    End Property


    Public Sub pDisplayDtls(ByVal dtCustInfoTab As DataTable)
        dtCustmInfo = dtCustInfoTab
        If _dtCustmInfo IsNot Nothing Then
            If _dtCustmInfo.Rows.Count > 0 Then
                pClear()
                lblCustNameValue.DataBindings.Add("Text", _dtCustmInfo, "CUSTOMERNAME")
                lblCustNoValue.DataBindings.Add("Text", _dtCustmInfo, "CUSTOMERNO")
                lblAddressValue.DataBindings.Add("Text", _dtCustmInfo, "ADDRESS")
                lblTelNoValue.DataBindings.Add("Text", _dtCustmInfo, "ResPhone")
                lblEmailIdValue.DataBindings.Add("Text", _dtCustmInfo, "EMAILID")

                'cboAddrType.DataBindings.Add("SelectedText", _dtCustmInfo, "ADDRESSTYPENAME")
                'cboAddrType.DataBindings.Add("SelectedValue", _dtCustmInfo, "ADDRESSTYPE")
                lblCustTypeValue.DataBindings.Add("Text", _dtCustmInfo, "CUSTOMERTYPE")
                cboAddrType.DataSource = _dtCustmInfo
                cboAddrType.ValueMember = "ADDRESSTYPE"
                cboAddrType.DisplayMember = "ADDRESSTYPENAME"
                pC1ComboSetDisplayMember(cboAddrType)

                cboAddrType.Text = cboAddrType.WillChangeToText

                'If cboAddrType.SelectedIndex = -1 Then
                '    cboAddrType.SelectedIndex = 0
                'End If

                'If _dtCustmInfo.Rows.Count = 1 Then
                '    cboAddrType.SelectedIndex = 0
                'End If

            End If
        Else
            pClear()
        End If

    End Sub

    Public Sub pClear()
        lblCustNameValue.DataBindings.Clear()
        lblCustNoValue.DataBindings.Clear()
        lblAddressValue.DataBindings.Clear()
        lblTelNoValue.DataBindings.Clear()
        lblEmailIdValue.DataBindings.Clear()
        lblCustTypeValue.DataBindings.Clear()
        cboAddrType.DataBindings.Clear()
        'cboAddrType.ClearFields()
        cboAddrType.Text = ""
        lblAddressValue.Text = ""
        lblCustNameValue.Text = ""
        lblCustNoValue.Text = ""
        lblTelNoValue.Text = ""
        lblEmailIdValue.Text = ""
        lblCustTypeValue.Text = ""

    End Sub

    Private Sub CtrlCustDtls_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cboAddrType.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList

        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
        End If

    End Sub
    Private Function Themechange()
        Me.lblCustType.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblCustType.Font = New Font("NeoSans", 7, FontStyle.Regular)
        Me.lblCustType.TextAlign = ContentAlignment.MiddleLeft
        Me.lblCustType.ForeColor = Color.FromArgb(37, 37, 37)
        Me.lblCustType.Text = Me.lblCustType.Text.ToUpper
        lblCustType.BorderStyle = Windows.Forms.BorderStyle.None
        lblCustType.MaximumSize = New Size(0, 20)
        Me.lblCustTypeValue.Font = New Font("Neo Sans", 9, FontStyle.Regular)

        Me.lblCustNo.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblCustNo.Font = New Font("NeoSans", 7, FontStyle.Regular)
        Me.lblCustNo.TextAlign = ContentAlignment.MiddleLeft
        Me.lblCustNo.ForeColor = Color.FromArgb(37, 37, 37)
        Me.lblCustNo.Text = Me.lblCustNo.Text.ToUpper
        lblCustNo.MaximumSize = New Size(0, 20)
        lblCustNo.BorderStyle = Windows.Forms.BorderStyle.None

        Me.lblCustNoValue.Font = New Font("Neo Sans", 9, FontStyle.Regular)



        Me.lblCustName.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblCustName.Font = New Font("NeoSans", 7, FontStyle.Regular)
        Me.lblCustName.TextAlign = ContentAlignment.MiddleLeft
        Me.lblCustName.ForeColor = Color.FromArgb(37, 37, 37)
        Me.lblCustName.Text = Me.lblCustName.Text.ToUpper
        lblCustName.MaximumSize = New Size(0, 27)
        lblCustName.BorderStyle = Windows.Forms.BorderStyle.None


        '  Me.lblCustNameValue.ForeColor = Color.FromArgb(37, 37, 37)
        Me.lblCustNameValue.Font = New Font("Neo Sans", 9, FontStyle.Regular)


        Me.lblAddress.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblAddress.Font = New Font("NeoSans", 7, FontStyle.Regular)
        Me.lblAddress.TextAlign = ContentAlignment.MiddleLeft
        Me.lblAddress.ForeColor = Color.FromArgb(37, 37, 37)
        Me.lblAddress.Text = Me.lblAddress.Text.ToUpper
        lblAddress.MaximumSize = New Size(0, 26)

        lblAddress.BorderStyle = Windows.Forms.BorderStyle.None


        Me.lblAddressValue.Font = New Font("Neo Sans", 9, FontStyle.Regular)


        Me.lblEmailId.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblEmailId.Font = New Font("NeoSans", 7, FontStyle.Regular)
        Me.lblEmailId.TextAlign = ContentAlignment.MiddleLeft
        Me.lblEmailId.ForeColor = Color.FromArgb(37, 37, 37)
        Me.lblEmailId.Text = Me.lblEmailId.Text.ToUpper
        lblEmailId.BorderStyle = Windows.Forms.BorderStyle.None
        lblEmailId.MaximumSize = New Size(0, 27)


        Me.lblEmailIdValue.Font = New Font("Neo Sans", 9, FontStyle.Regular)

        Me.lblTelNo.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblTelNo.Font = New Font("NeoSans", 7, FontStyle.Regular)
        Me.lblTelNo.TextAlign = ContentAlignment.MiddleLeft
        Me.lblTelNo.ForeColor = Color.FromArgb(37, 37, 37)
        Me.lblTelNo.Text = Me.lblTelNo.Text.ToUpper
        lblTelNo.BorderStyle = Windows.Forms.BorderStyle.None
        lblTelNo.MaximumSize = New Size(0, 26)
        Me.lblTelNoValue.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lblMoreInfo.TextAlign = ContentAlignment.MiddleLeft
        lblMoreInfo.Text = lblMoreInfo.Text.ToUpper
        '   lblMoreInfo.MaximumSize = New Size(0, 26)
    End Function
End Class
