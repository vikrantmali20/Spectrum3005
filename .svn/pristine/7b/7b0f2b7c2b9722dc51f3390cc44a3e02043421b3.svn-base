Imports SpectrumBL
Public Class frmNHomeDelivery
    Dim _deliveryDate As DateTime = Now
    Dim _Name As String
    Dim _address1, _address2, _address3, _address4 As String
    Dim _telNo As String
    Dim _MobileNo As String
    Dim _email As String
    Dim _Remark As String
    Dim _cmDate As DateTime
    Public _IsCleared As Boolean
    Dim dtCustAddress As New DataTable
    Private _deliveryPersonID As String
    Public Property DeliveryPersonID() As String
        Get
            Return _deliveryPersonID
        End Get
        Set(ByVal value As String)
            _deliveryPersonID = value
        End Set
    End Property
    Private _delieverypartnerid As String
    Public Property delieverypartnerid() As String
        Get
            Return _delieverypartnerid
        End Get
        Set(value As String)
            _delieverypartnerid = value
        End Set
    End Property
    Private _updateCustomerAddress As Boolean = False
    Public Property IsUpdateCustomerAddress() As Boolean
        Get
            Return _updateCustomerAddress
        End Get
        Set(ByVal value As Boolean)
            _updateCustomerAddress = value
        End Set
    End Property

    Private _IsupdateDeliveryPersonAllowed As Boolean = False
    Public Property IsupdateDeliveryPersonAllowed() As Boolean
        Get
            Return _IsupdateDeliveryPersonAllowed
        End Get
        Set(ByVal value As Boolean)
            _IsupdateDeliveryPersonAllowed = value
        End Set
    End Property

    Public Property CMdate() As DateTime
        Get
            Return _cmDate
        End Get
        Set(ByVal value As DateTime)
            _cmDate = value
        End Set
    End Property
    Public Property DeliveryDate() As DateTime
        Get
            Return _deliveryDate
        End Get
        Set(ByVal value As DateTime)
            _deliveryDate = value
        End Set
    End Property
    Public Property HdName() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property
    Public Property Address1() As String
        Get
            Return _address1
        End Get
        Set(ByVal value As String)
            _address1 = value
        End Set
    End Property

    Public Property Address2() As String
        Get
            Return _address2
        End Get
        Set(ByVal value As String)
            _address2 = value
        End Set
    End Property

    Public Property Address3() As String
        Get
            Return _address3
        End Get
        Set(ByVal value As String)
            _address3 = value
        End Set
    End Property

    Public Property Address4() As String
        Get
            Return _address4
        End Get
        Set(ByVal value As String)
            _address4 = value
        End Set
    End Property

    Public Property TelNo() As String
        Get
            Return _telNo
        End Get
        Set(ByVal value As String)
            _telNo = value
        End Set
    End Property

    Public Property MobileNo() As String
        Get
            Return _MobileNo
        End Get
        Set(ByVal value As String)
            _MobileNo = value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property
    Public Property Remark() As String
        Get
            Return _Remark
        End Get
        Set(ByVal value As String)
            _Remark = value
        End Set
    End Property

    Private _CustomerNo As String
    Public Property CustomerNo() As String
        Get
            Return _CustomerNo
        End Get
        Set(ByVal value As String)
            _CustomerNo = value
        End Set
    End Property
    Private _FloatAmt As Double
    Public Property FloatAmt() As Double
        Get
            Return _FloatAmt
        End Get
        Set(ByVal value As Double)
            _FloatAmt = value
        End Set
    End Property
    Private _IsDispatch As Boolean
    Public Property IsDispatch As Boolean
        Get
            Return _IsDispatch
        End Get
        Set(ByVal value As Boolean)
            _IsDispatch = value
        End Set
    End Property
    '-- added for updating HD cancel order - ashma 21 dec 2016
    Private _BillIntermediateStatus As String
    Public Property BillIntermediateStatus() As String
        Get
            Return _BillIntermediateStatus
        End Get
        Set(ByVal value As String)
            _BillIntermediateStatus = value
        End Set
    End Property
    Private _DeliveryPartnerOrderID As String
    Public Property DeliveryPartnerOrderID() As String 'vipin
        Get
            Return _DeliveryPartnerOrderID
        End Get
        Set(ByVal value As String)
            _DeliveryPartnerOrderID = value
        End Set
    End Property
    Public _State As String = "" 'vipul
    Public Property State() As String
        Get
            Return _State
        End Get
        Set(ByVal value As String)
            _State = value
        End Set
    End Property
    Public _Country As String = ""
    Public Property Country() As String
        Get
            Return _Country
        End Get
        Set(ByVal value As String)
            _Country = value
        End Set
    End Property
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        txtRemark.Text = String.Empty
        DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Try
            If cmbDeliveryPerson.SelectedIndex = -1 Then
                If clsDefaultConfiguration.EvasPizzaChanges = False Then
                    ShowMessage(getValueByKey("HMP012"), "HMP012 - " & getValueByKey("CLAE04"))
                    cmbDeliveryPerson.Focus()
                    Exit Sub
                End If
            ElseIf DateDiff(DateInterval.Day, clsAdmin.CurrentDate, dtpDeliveryDate.Value) < 0 Then
                ShowMessage(getValueByKey("HMD002"), "HMD002 - " & getValueByKey("CLAE04"))
                Exit Sub
            ElseIf txtEmail.Text <> String.Empty AndAlso validateEmailId(txtEmail.Text) = False Then
                ShowMessage(getValueByKey("HMD003"), "HMD003 - " & getValueByKey("CLAE04"))
                txtEmail.Focus()
                Exit Sub
                '-- Commented By Mahesh 0010294: Natural changes 
                'ElseIf String.IsNullOrEmpty(txtName.Text.Trim()) Then
                '    ShowMessage(getValueByKey("HMP009"), "HMP009 - " & getValueByKey("CLAE04"))
                '    txtName.Focus()
                '    Exit Sub
            ElseIf String.IsNullOrEmpty(txtAddress1.Text.Trim) Then
                ShowMessage(getValueByKey("HMP010"), "HMP010 - " & getValueByKey("CLAE04"))
                txtAddress1.Focus()
                Exit Sub
                'ElseIf String.IsNullOrEmpty(txtRemark.Text.Trim) Then
                '    ShowMessage(getValueByKey("HMP011"), "HMP011 - " & getValueByKey("CLAE04"))
                '    txtRemark.Focus()
                '    Exit Sub
            End If

            '----------Checking If Value Entered is Integer Value
            If Not (CheckInteger(txtFloatAmt.Text)) Then
                ShowMessage(getValueByKey("ACP041"), "ACP041 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            If txtFloatAmt.Text < 0 Then
                'Float Amount cannot be less than 0
                ShowMessage(getValueByKey("ACP038"), "ACP038 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            If cmbDeliveryPerson.SelectedIndex > -1 Then
                DeliveryPersonID = cmbDeliveryPerson.SelectedValue
            End If
            If cmbDeliveryPartner.SelectedIndex > -1 Then
                delieverypartnerid = cmbDeliveryPartner.SelectedValue
            End If
            dtpDate.Value = Now
            DeliveryDate = dtpDeliveryDate.Value
            If String.IsNullOrEmpty(txtName.Text.Trim()) Then
                HdName = cmbDeliveryPerson.SelectedValue
            Else
                HdName = txtName.Text.Trim()
            End If

            Address1 = txtAddress1.Text.Trim()
            Address2 = txtAddress2.Text.Trim()
            Address3 = txtAddress3.Text.Trim()
            Address4 = txtAddress4.Text.Trim()
            TelNo = txtTelNo.Text.Trim()
            MobileNo = txtMobileNo.Text.Trim()
            Email = txtEmail.Text.Trim()
            Remark = txtRemark.Text.Trim()
            FloatAmt = txtFloatAmt.Text.Trim()
            DeliveryPartnerOrderID = TxtPartnerOrderId.Text 'vipin 29-03-2018
            DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub frmHomeDelivery_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                ThemeChange()
            End If
            If clsDefaultConfiguration.EvasPizzaChanges = True Then
                lblDeliveryPerson.Text = "Delivery Person"
            End If
            dtpDate.Value = Now
            txtName.Text = HdName
            txtAddress1.Text = Address1
            txtAddress2.Text = Address2
            txtAddress3.Text = Address3
            txtAddress4.Text = Address4
            txtTelNo.Text = TelNo
            txtMobileNo.Text = MobileNo
            txtEmail.Text = Email
            TxtPartnerOrderId.Text = DeliveryPartnerOrderID 'vipin 
            txtRemark.Text = Remark
            txtFloatAmt.Text = FloatAmt
            'dtpDeliveryDate.Value = Now
            dtpDeliveryDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
            dtpDeliveryDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
            dtpDeliveryDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
            dtpDeliveryDate.DisplayFormat.CustomFormat = "dd-MM-yy HH:mm"
            dtpDeliveryDate.EditFormat.CustomFormat = " dd-MM-yy HH:mm"
            dtpDeliveryDate.Value = DeliveryDate
            dtpDate.Value = CMdate
            LoadDeliveryPersonData()
            LoadCustomerMultipleAddresses() 'vipul
            If String.IsNullOrEmpty(DeliveryPersonID) Then
                cmbDeliveryPerson.SelectedIndex = -1
            Else
                cmbDeliveryPerson.SelectedValue = DeliveryPersonID
            End If
            If cmdOk.Text = "Dispatch" Then
                cmbDeliveryPartner.Enabled = False
            End If
            LoadDeliveryPartners()
            If String.IsNullOrEmpty(delieverypartnerid) Then
                cmbDeliveryPartner.SelectedIndex = -1
            Else
                If delieverypartnerid <> "" Then
                    cmbDeliveryPartner.SelectedValue = delieverypartnerid
                End If
            End If
            '---Code Added By Mahesh for IsupdateDeliveryPersonAllowed only allowed to edit Delivery person 
            If IsupdateDeliveryPersonAllowed Then
                dtpDate.ReadOnly = True
                txtName.ReadOnly = True
                txtAddress1.ReadOnly = True
                txtAddress2.ReadOnly = True
                txtAddress3.ReadOnly = True
                txtAddress4.ReadOnly = True
                txtTelNo.ReadOnly = True
                txtMobileNo.ReadOnly = True
                txtEmail.ReadOnly = True
                txtRemark.ReadOnly = True
                dtpDeliveryDate.ReadOnly = True
                btnClear.Enabled = False
                chkUpdateCustomerAddress.Enabled = False

            End If

            cmbDeliveryPerson.Select()
        Catch ex As Exception
            LogException(ex)
        End Try
        SetCulture(Me, Me.Name)
        If IsDispatch Then
            cmdOk.Text = "Dispatch"
        End If
    End Sub

    Private Sub LoadDeliveryPersonData()
        Try
            Dim dt As DataTable
            Dim objComn As New SpectrumBL.clsCommon
            dt = objComn.GetSalesPerson(clsAdmin.SiteCode)
            If Not dt Is Nothing And dt.Rows.Count > 0 Then
                cmbDeliveryPerson.DataSource = dt
                cmbDeliveryPerson.DisplayMember = dt.Columns("SalesPersonName").ToString()
                cmbDeliveryPerson.ValueMember = dt.Columns("EmpCode").ToString()
                cmbDeliveryPerson.ExtendRightColumn = True

                For Each r As C1.Win.C1List.Split In cmbDeliveryPerson.Splits
                    Dim i As Integer
                    For i = 0 To r.DisplayColumns.Count - 1
                        If r.DisplayColumns(i).Name <> cmbDeliveryPerson.DisplayMember Then
                            r.DisplayColumns(i).Visible = False
                        End If
                    Next
                Next
                cmbDeliveryPerson.SelectedIndex = -1
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub LoadDeliveryPartners()
        Try
            Dim dt As DataTable
            Dim objComn As New SpectrumBL.clsCommon
            dt = objComn.GetDeliveryPartners()
            If Not dt Is Nothing And dt.Rows.Count > 0 Then
                cmbDeliveryPartner.DataSource = dt
                cmbDeliveryPartner.DisplayMember = dt.Columns("DelieveryPartnerName").ToString()
                cmbDeliveryPartner.ValueMember = dt.Columns("DelieveryPartnerId").ToString()
                cmbDeliveryPartner.ExtendRightColumn = True

                For Each r As C1.Win.C1List.Split In cmbDeliveryPartner.Splits
                    Dim i As Integer
                    For i = 0 To r.DisplayColumns.Count - 1
                        If r.DisplayColumns(i).Name <> cmbDeliveryPartner.DisplayMember Then
                            r.DisplayColumns(i).Visible = False
                        End If
                    Next
                Next
                cmbDeliveryPartner.SelectedIndex = -1
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub LoadCustomerMultipleAddresses() 'vipul
        Try
            Dim objComn As New SpectrumBL.clsCommon
            dtCustAddress = objComn.GetCustomerMultipleAddresses(CustomerNo, clsAdmin.CLPProgram, clsDefaultConfiguration.DetailedCustomerCreationformat)
            If Not dtCustAddress Is Nothing And dtCustAddress.Rows.Count > 0 Then
                CmbAddress.DataSource = dtCustAddress
                CmbAddress.DisplayMember = dtCustAddress.Columns("Address").ToString()
                CmbAddress.ValueMember = dtCustAddress.Columns("SrNo").ToString()
                CmbAddress.ExtendRightColumn = True
                For Each r As C1.Win.C1List.Split In CmbAddress.Splits
                    Dim i As Integer
                    For i = 0 To r.DisplayColumns.Count - 1
                        If r.DisplayColumns(i).Name <> CmbAddress.DisplayMember Then
                            r.DisplayColumns(i).Visible = False
                        End If
                    Next
                Next
                CmbAddress.SelectedIndex = -1
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub txtName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyDown
        Try
            If (e.KeyValue > 64 AndAlso e.KeyValue < 91) Or e.KeyValue = 8 Or e.KeyValue = 32 Or e.KeyValue = 46 Then
                e.SuppressKeyPress = False
            Else
                e.SuppressKeyPress = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtTelNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTelNo.KeyDown
        Try
            If (e.KeyValue >= 48 And e.KeyValue < 58) Or (e.KeyValue >= 96 And e.KeyValue < 106) Or e.KeyValue = 8 Or e.KeyValue = 32 Or e.KeyValue = 46 Then
                e.SuppressKeyPress = False
            Else
                e.SuppressKeyPress = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        _IsCleared = True
        DialogResult = Windows.Forms.DialogResult.Retry
        Me.Close()
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.MaximizeBox = False
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Private Sub chkUpdateCustomerAddress_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkUpdateCustomerAddress.CheckedChanged
        If chkUpdateCustomerAddress.Checked Then
            _updateCustomerAddress = True
        Else
            _updateCustomerAddress = False
        End If

    End Sub

    Private Sub cmbDeliveryPerson_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles cmbDeliveryPerson.SelectedValueChanged
        _deliveryPersonID = cmbDeliveryPerson.SelectedValue
    End Sub
    Private Sub cmbDeliveryPartner_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles cmbDeliveryPartner.SelectedValueChanged
        _delieverypartnerid = cmbDeliveryPartner.SelectedValue
    End Sub
    Private Sub txtFloatAmt_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtFloatAmt.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    
    Private Sub txtTelNo_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtTelNo.KeyPress
        'If Asc(e.KeyChar) = 39 Then
        '    e.Handled = True
        'End If
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub txtMobileNo_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtMobileNo.KeyPress
        'If Asc(e.KeyChar) = 39 Then
        '    e.Handled = True
        'End If
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub frmNHomeDelivery_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "526-dine-in-take-away-home-delivery.htm")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function ThemeChange()
        Me.Size = New Size(620, 390)
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        sizDetail.BackColor = Color.FromArgb(134, 134, 134)
        sizDetail.SplitterWidth = 2
        cmbDeliveryPerson.BackColor = Color.FromArgb(212, 212, 212)
        cmbDeliveryPerson.SelectedStyle.BackColor = Color.FromArgb(212, 212, 212)
        cmbDeliveryPerson.Styles(0).BackColor = Color.FromArgb(212, 212, 212)
        cmbDeliveryPerson.Location = New Point(121, 11)
        dtpDeliveryDate.Location = New Point(360, 11)
        ' lblDeliveryPerson.Size = New Size(116, 23)
        lblDeliveryPerson.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        'lblDeliveryPerson.Text = lblDeliveryPerson.Text.ToUpper
        lblDeliveryPerson.BackColor = Color.FromArgb(212, 212, 212)
        lblDeliveryPerson.AutoSize = False
        lblDeliveryPerson.Location = New Point(3, 11)
        lblDeliveryPerson.Size = New Size(116, 23)
        '----
        lblDeliveryPartner.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        'lblDeliveryPerson.Text = lblDeliveryPerson.Text.ToUpper
        lblDeliveryPartner.BackColor = Color.FromArgb(212, 212, 212)
        lblDeliveryPartner.AutoSize = False
        'lblDeliveryPartner.Location = New Point(3, 11)
        lblDeliveryPartner.Size = New Size(116, 23)
        cmbDeliveryPartner.BackColor = Color.FromArgb(212, 212, 212)
        cmbDeliveryPartner.SelectedStyle.BackColor = Color.FromArgb(212, 212, 212)
        cmbDeliveryPartner.Styles(0).BackColor = Color.FromArgb(212, 212, 212)

        CmbAddress.BackColor = Color.FromArgb(212, 212, 212)
        CmbAddress.SelectedStyle.BackColor = Color.FromArgb(212, 212, 212)
        CmbAddress.Styles(0).BackColor = Color.FromArgb(212, 212, 212)

        LblMutipleAddress.Font = New Font("Neo Sans", 8, FontStyle.Regular)

        LblMutipleAddress.BackColor = Color.FromArgb(212, 212, 212)
        LblMutipleAddress.AutoSize = False

        LblMutipleAddress.Size = New Size(116, 23)

        ' cmbDeliveryPartner.Location = New Point(121, 11)
        '----
        lblDeliveryDate.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        lblDeliveryDate.Text = lblDeliveryDate.Text.ToUpper
        lblDeliveryDate.BackColor = Color.FromArgb(212, 212, 212)
        lblDeliveryDate.Location = New Point(269, 11)
        lblDeliveryDate.Size = New Size(88, 22)
        lblDeliveryDate.AutoSize = False
        lblName.BorderStyle = BorderStyle.None
        lblName.BackColor = Color.FromArgb(212, 212, 212)
        lblName.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        ' lblName.Text = lblName.Text.ToUpper
        lblName.AutoSize = False
        lblName.MaximumSize = New Size(0, 20)
        lblAddress.BackColor = Color.FromArgb(212, 212, 212)
        lblAddress.BorderStyle = BorderStyle.None
        lblAddress.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        ' lblAddress.Text = lblAddress.Text.ToUpper
        lblAddress.AutoSize = False
        lblAddress.MaximumSize = New Size(0, 20)
        lblTelNo.BackColor = Color.FromArgb(212, 212, 212)
        lblTelNo.MaximumSize = New Size(0, 20)
        lblTelNo.BorderStyle = BorderStyle.None
        lblTelNo.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        'lblTelNo.Text = lblTelNo.Text.ToUpper
        lblTelNo.AutoSize = False
        lblMobileNo.BackColor = Color.FromArgb(212, 212, 212)
        lblMobileNo.BorderStyle = BorderStyle.None
        lblMobileNo.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        lblMobileNo.MaximumSize = New Size(0, 20)
        'lblMobileNo.Text = lblMobileNo.Text.ToUpper
        lblMobileNo.AutoSize = False

        LblPartnerOrderId.BackColor = Color.FromArgb(212, 212, 212)
        LblPartnerOrderId.BorderStyle = BorderStyle.None
        LblPartnerOrderId.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        LblPartnerOrderId.MaximumSize = New Size(0, 20)
        LblPartnerOrderId.AutoSize = False


        lblEmail.BorderStyle = BorderStyle.None
        lblEmail.BackColor = Color.FromArgb(212, 212, 212)
        lblEmail.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        lblEmail.MaximumSize = New Size(0, 20)
        ' lblEmail.Text = lblEmail.Text.ToUpper
        lblEmail.AutoSize = False
        lblRemark.BackColor = Color.FromArgb(212, 212, 212)
        lblRemark.BorderStyle = BorderStyle.None
        lblRemark.MaximumSize = New Size(0, 29)

        txtRemark.MaximumSize = New Size(353, 29)
        lblRemark.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        'lblRemark.Text = lblRemark.Text.ToUpper
        lblRemark.AutoSize = False
        lblFloatAmt.BackColor = Color.FromArgb(212, 212, 212)
        lblFloatAmt.BorderStyle = BorderStyle.None
        lblFloatAmt.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        ' lblFloatAmt.Text = lblFloatAmt.Text.ToUpper
        lblFloatAmt.MaximumSize = New Size(0, 21)
        lblFloatAmt.AutoSize = False
        ' chkUpdateCustomerAddress.MaximumSize = New Size(0, 20)
        chkUpdateCustomerAddress.BackColor = Color.FromArgb(212, 212, 212)
        chkUpdateCustomerAddress.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        chkUpdateCustomerAddress.AutoSize = False
        cmbDeliveryPerson.BackColor = Color.FromArgb(212, 212, 212)
        cmbDeliveryPerson.EditorBackColor = Color.FromArgb(212, 212, 212)
        ' chkUpdateCustomerAddress.Text = chkUpdateCustomerAddress.Text.ToUpper
        Me.btnClear.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnClear.BackColor = Color.Transparent
        btnClear.BackColor = Color.FromArgb(0, 107, 163)
        btnClear.ForeColor = Color.FromArgb(255, 255, 255)
        btnClear.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        btnClear.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnClear.FlatStyle = FlatStyle.Flat
        btnClear.FlatAppearance.BorderSize = 0
        btnClear.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        Me.cmdOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System


        cmdOk.BackColor = Color.Transparent
        cmdOk.BackColor = Color.FromArgb(0, 107, 163)
        cmdOk.ForeColor = Color.FromArgb(255, 255, 255)
        cmdOk.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        cmdOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdOk.FlatStyle = FlatStyle.Flat
        cmdOk.FlatAppearance.BorderSize = 0
        cmdOk.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        Me.cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdCancel.BackColor = Color.Transparent
        cmdCancel.BackColor = Color.FromArgb(0, 107, 163)
        cmdCancel.ForeColor = Color.FromArgb(255, 255, 255)
        cmdCancel.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        cmdCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdCancel.FlatStyle = FlatStyle.Flat
        cmdCancel.FlatAppearance.BorderSize = 0
        cmdCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        'Me.AutoSize = True
        'Me.Height = 370



    End Function
    Private Sub CmbAddress_SelectedValueChanged(sender As Object, e As EventArgs) Handles CmbAddress.SelectedValueChanged 'vipul
        Try
            If Not dtCustAddress Is Nothing And dtCustAddress.Rows.Count > 0 Then
                Dim SelectedAddressSrNo = CmbAddress.SelectedValue
                If Not SelectedAddressSrNo Is Nothing Then
                    If SelectedAddressSrNo <> 0 Then

                        Dim ObjNewC As New frmNewCustomer
                        ObjNewC.CustomerNo = CustomerNo
                        ObjNewC.IsCalledFromHomeDelivery = True
                        Dim DialogResult = ObjNewC.ShowDialog()
                        If ObjNewC.DialogResult = Windows.Forms.DialogResult.OK Then
                            LoadCustomerMultipleAddresses()
                            Dim dr() As DataRow = dtCustAddress.Select("DefaultAddress=1")
                            If dr.Length > 0 Then
                                txtAddress1.Text = dr(0)("AddressLn1").ToString
                                txtAddress2.Text = dr(0)("AddressLn2").ToString
                                txtAddress3.Text = dr(0)("AddressLn3").ToString
                                txtAddress4.Text = dr(0)("AddressLn4").ToString
                                State = dr(0)("State").ToString
                                Country = dr(0)("Country").ToString
                            End If
                        Else
                            CmbAddress.SelectedIndex = -1

                        End If
                        If ObjNewC.IsDisposed = False Then
                            ObjNewC.Dispose()
                        End If
                    Else

                        Dim dr() As DataRow = dtCustAddress.Select("SrNo= '" & SelectedAddressSrNo & "'")
                        If dr.Length > 0 Then
                            txtAddress1.Text = dr(0)("AddressLn1").ToString
                            txtAddress2.Text = dr(0)("AddressLn2").ToString
                            txtAddress3.Text = dr(0)("AddressLn3").ToString
                            txtAddress4.Text = dr(0)("AddressLn4").ToString
                            State = dr(0)("State").ToString
                            Country = dr(0)("Country").ToString
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
End Class
