Imports SpectrumBL
Imports System.IO
Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient
Imports SpectrumPrint
Imports SpectrumCommon
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Globalization

Public Class frmNewCustomer
    Dim objCustm As New clsSOCustomer
    Dim objCLPCustm As New clsCLPCustomer()
    Dim objComn As New clsCommon
    Dim dsCombo As DataSet = New DataSet
    Dim dtCustmData As DataTable
    Dim dtAddressDetails As New DataTable
    Dim newRow As DataRow
    Dim dtCommunication As New DataTable
    Dim rowCommunication As DataRow
    Dim dtAddress As DataTable
    Dim rowAddress As DataRow
    Dim rowindex = 1
    Dim rowNumber = 1
    Dim switchCount As Integer = 1
    Dim EditAddress As Boolean = False
    Dim EditCommun As Boolean = False
    Dim EditCommunContactValue As String = ""
    Dim mblAltrnumber As String = ""
    Dim dsClPCustDetails As DataSet
    Dim str As String = String.Empty
    Dim vCurrentDate As Date
    Dim message As Boolean = True
    Dim dtAddressCompare As DataTable
    Dim dtContactCompare As DataTable
    Dim dtAddressDelete As DataTable
    Dim dtContactdelete As DataTable
    Dim EditCustomer As Boolean = False
    Dim countResidence As Integer = 1
    Dim sortMbl As Integer = 1
    Dim sortOther As Integer = 100
    Dim dsCLPprog As DataSet
    Dim uniquemblno As String = ""

    Private _CustomerNo As String = ""
    Public Property CustomerNo() As String
        Get
            Return _CustomerNo
        End Get
        Set(ByVal value As String)
            _CustomerNo = value
        End Set
    End Property
    Private _ClpCardNo As String = ""
    Public Property ClpCardNo() As String
        Get
            Return _ClpCardNo
        End Get
        Set(ByVal value As String)
            _ClpCardNo = value
        End Set
    End Property

    Private ReadOnly Property ButtonSize As System.Drawing.Size
        Get
            Return New Size(35, 28)
        End Get
    End Property
    Private _CustomerMaster As GetCustomerMasterResponse
    Public Property CustomerMaster As GetCustomerMasterResponse
        Get
            Return _CustomerMaster
        End Get
        Set(value As GetCustomerMasterResponse)
            _CustomerMaster = value
        End Set
    End Property
    Private _CustomerBL As ICreateNewCustomer
    Public Property CustomerBL As ICreateNewCustomer
        Get
            If _CustomerBL Is Nothing Then
                _CustomerBL = New CreateNewCustomer()
            End If
            Return _CustomerBL
        End Get
        Set(value As ICreateNewCustomer)
            _CustomerBL = value
        End Set
    End Property

    Private _CustomerRequest As SaveCustomerRequest
    Public Property CustomerRequest As SaveCustomerRequest
        Get
            Return _CustomerRequest
        End Get
        Set(value As SaveCustomerRequest)
            _CustomerRequest = value
        End Set
    End Property
    Dim _CustmType As String
    Public Property CustmType() As String
        Get
            Return _CustmType
        End Get
        Set(ByVal value As String)
            _CustmType = value
        End Set
    End Property

    Dim _AddressType As String
    Public Property AddressType() As String
        Get
            Return _AddressType
        End Get
        Set(ByVal value As String)
            _AddressType = value
        End Set
    End Property
    Private _IsCustSearch As Boolean
    Public Property IsCustSearch() As Boolean
        Get
            Return _IsCustSearch
        End Get
        Set(ByVal value As Boolean)
            _IsCustSearch = value
        End Set
    End Property
    Dim _dtCustmInfo As DataTable
    Public Property dtCustmInfo() As DataTable
        Get
            Return _dtCustmInfo
        End Get
        Set(ByVal value As DataTable)
            _dtCustmInfo = value
        End Set
    End Property
    Private _searchValue As String
    Public Property SearchedValue() As String
        Get
            Return _searchValue
        End Get
        Set(ByVal value As String)
            _searchValue = value
        End Set
    End Property

    Private _ViewCust As Boolean = False
    Public Property ViewCust() As Boolean
        Get
            Return _ViewCust
        End Get
        Set(ByVal value As Boolean)
            _ViewCust = value
        End Set
    End Property
    Private _IsCalledFromHomeDelivery As Boolean = False 'vipul
    Public Property IsCalledFromHomeDelivery() As Boolean
        Get
            Return _IsCalledFromHomeDelivery
        End Get
        Set(ByVal value As Boolean)
            _IsCalledFromHomeDelivery = value
        End Set
    End Property

    Private Sub frmNewCustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetCulture(Me, Me.Name)
            tbAddress.pInit()
            tbAddress.SelectedTab = tbAddress.TabPages("tpPersonalDetails")
            CustomerMaster = CustomerBL.GetCustomerMaster()
            InitializeComboBox()
            BtnPrevious.Visible = False
            txtLPM.Enabled = False
            txtTierType.Enabled = False
            txtBalancePoint.Enabled = False
            txtSwipeCard.Enabled = False
            If Not IsCalledFromHomeDelivery = True Then
                chkPrimaryAddress.Checked = True
            End If
            vCurrentDate = objComn.GetCurrentDate
            _CustmType = "CLP"

            If clsDefaultConfiguration.IsCustAddWild = True Then
                txtCustomerAddress.Visible = False
                txtAddress2.Visible = False
                txtAddress3.Visible = False
                txtAddress4.Visible = False
                txtCity.Visible = False
                Call setAddressAutoComplete_AndroidSearch()
            Else
                AndroidSearchtxtCustomerAddress.Visible = False
                AndroidSearchtxtAddress2.Visible = False
                AndroidSearchtxtAddress3.Visible = False
                AndroidSearchtxtAddress4.Visible = False
                AndroidSearchtxtCity.Visible = False
                Call setAddressAutoComplete()
            End If
            CLP_Data.Sitecode = clsAdmin.SiteCode
            dsCLPprog = CLP_Data.getclpdata()
            If dsCLPprog IsNot Nothing AndAlso dsCLPprog.Tables("CLPHeader") IsNot Nothing AndAlso dsCLPprog.Tables("CLPHeader").Rows.Count > 0 Then
                Dim tierslist = CLP_Data.GetCLPTiers(dsCLPprog.Tables("CLPHeader")(0)("CLPPROGRAMID"))
                If tierslist.Count > 0 Then
                    txtTierType.Text = tierslist(0)
                End If
            End If


            dtAddressDetails = objCustm.GetDataForAddressTab()
            dtCommunication = objCustm.GetDataForCommunicationTab()
            dtAddress = objCustm.GetDetailsForAddress()
            dtAddressDetails.Rows.Clear()
            dtCommunication.Rows.Clear()
            dtAddress.Rows.Clear()
            dtAddressDelete = dtAddress.Copy()
            dtContactdelete = dtCommunication.Copy()
            With grdAddressMapping
                .VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
                .ScrollBars = ScrollBars.Both
                .AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns
                .SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
            End With
            RefreshAddressGrid()

            With grdMapping
                .VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
                .ScrollBars = ScrollBars.Both
                .AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns
                .SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
            End With
            RefreshCommunicationGrid()

            AddHandler dtpDOB.Leave, AddressOf dtpDOB_Leave
            AddHandler dtpDOB.Calendar.DateValueSelected, AddressOf dtpDOB_Leave
            AddHandler dtpDOB.TextChanged, AddressOf dtpDOB_Leave

            If IsCustSearch Then
                Call btnSearch_Click(btnSearch, New System.EventArgs)
            End If
            If SearchedValue <> Nothing Then
                If IsNumeric(SearchedValue.ToString()) Then
                    If SearchedValue.Length > 10 Then
                        txtTelPhone.Text = SearchedValue.Substring(0, 10)
                    Else
                        txtTelPhone.Text = SearchedValue
                    End If
                Else
                    If SearchedValue.Length > 60 Then
                        txtFirstName.Text = SearchedValue.Substring(0, 60)
                    Else
                        txtFirstName.Text = SearchedValue
                    End If
                End If
            End If

            If (Not String.IsNullOrEmpty(CustomerNo)) Then
                dsClPCustDetails = objCustm.GetCLPCustDetails(clsAdmin.SiteCode, CustomerNo)
                SetCustomerInformationInForm(dsClPCustDetails)
                'btnAdd.Text = "Save"
                'btnClear.Text = "Cancel"
                message = False
                EditCustomer = True
            End If

            txtTelPhone.Select()
            txtTelPhone.Select(txtTelPhone.Text.Length, 0)
            Me.tbAddress.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Blue

            If ViewCust Then
                For Each Control In tbAddress.Controls
                    For Each Ctrl In Control.Controls
                        Ctrl.Enabled = False
                    Next
                Next
                BtnSave.Enabled = False
                BtnNext.Enabled = False
                BtnPrevious.Enabled = False
            End If


            If clsDefaultConfiguration.ThemeSelect <> "Default" Then
                Select Case clsDefaultConfiguration.ThemeSelect
                    Case "Theme 1"
                        Call Themechange()
                    Case 2

                    Case Else

                End Select
            End If
            If IsCalledFromHomeDelivery = True Then
                tbAddress.SelectedIndex = 1
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function Themechange()
        Me.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black
        grdAddressMapping.BackColor = Color.FromArgb(212, 212, 212)
        tpAdressDetails.TabForeColor = Color.Black
        tpCommunicationDetails.TabForeColor = Color.Black
        tpLoyalityAddress.TabForeColor = Color.Black
        tpPersonalDetails.TabForeColor = Color.Black

        tpAdressDetails.TabForeColorSelected = Color.FromArgb(255, 255, 255)
        tpLoyalityAddress.TabForeColorSelected = Color.FromArgb(255, 255, 255)
        tpPersonalDetails.TabForeColorSelected = Color.FromArgb(255, 255, 255)
        tpCommunicationDetails.TabForeColorSelected = Color.FromArgb(255, 255, 255)


        tpAdressDetails.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        tpCommunicationDetails.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        tpLoyalityAddress.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        tpPersonalDetails.TabBackColorSelected = Color.FromArgb(0, 107, 163)

        '---- Tab Pages ...  
        tbAddress.TabPages(0).TabBackColor = Color.FromArgb(212, 212, 212)
        tbAddress.TabPages(0).BackColor = Color.FromArgb(134, 134, 134)

        tbAddress.TabPages(1).TabBackColor = Color.FromArgb(212, 212, 212)
        tbAddress.TabPages(1).BackColor = Color.FromArgb(134, 134, 134)

        tbAddress.TabPages(2).TabBackColor = Color.FromArgb(212, 212, 212)
        tbAddress.TabPages(2).BackColor = Color.FromArgb(134, 134, 134)

        tbAddress.TabPages(3).TabBackColor = Color.FromArgb(212, 212, 212)
        tbAddress.TabPages(3).BackColor = Color.FromArgb(134, 134, 134)

        '   tbAddress.SelectedTab.TabBackColor = Color.FromArgb(0, 107, 163)


        Me.BackColor = Color.FromArgb(134, 134, 134)

        BtnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnSave.BackColor = Color.Transparent
        BtnSave.BackColor = Color.FromArgb(0, 107, 163)
        BtnSave.ForeColor = Color.FromArgb(255, 255, 255)
        BtnSave.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        BtnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnSave.FlatStyle = FlatStyle.Flat
        BtnSave.FlatAppearance.BorderSize = 0
        BtnSave.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        'BtnSave.Size = New Size(85, 30)
        With BtnNext
            .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
            .BackColor = Color.Transparent
            .BackColor = Color.FromArgb(0, 107, 163)
            .ForeColor = Color.FromArgb(255, 255, 255)
            .Font = New Font("Neo Sans", 9, FontStyle.Bold)
            .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
            .FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        End With
        With BtnCancel
            .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
            .BackColor = Color.Transparent
            .BackColor = Color.FromArgb(0, 107, 163)
            .ForeColor = Color.FromArgb(255, 255, 255)
            .Font = New Font("Neo Sans", 9, FontStyle.Bold)
            .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
            .FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        End With

        With btnClear
            .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
            .BackColor = Color.Transparent
            .BackColor = Color.FromArgb(0, 107, 163)
            .ForeColor = Color.FromArgb(255, 255, 255)
            .Font = New Font("Neo Sans", 9, FontStyle.Bold)
            .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
            .FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        End With
        With BtnPrevious
            .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
            .BackColor = Color.Transparent
            .BackColor = Color.FromArgb(0, 107, 163)
            .ForeColor = Color.FromArgb(255, 255, 255)
            .Font = New Font("Neo Sans", 9, FontStyle.Bold)
            .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
            .FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        End With
        With btnResidenceNo
            .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
            .BackColor = Color.Transparent
            .BackColor = Color.FromArgb(0, 107, 163)
            .ForeColor = Color.FromArgb(255, 255, 255)
            .Font = New Font("Neo Sans", 9, FontStyle.Bold)
            .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
            .FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        End With
        With btnFaxNo
            .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
            .BackColor = Color.Transparent
            .BackColor = Color.FromArgb(0, 107, 163)
            .ForeColor = Color.FromArgb(255, 255, 255)
            .Font = New Font("Neo Sans", 9, FontStyle.Bold)
            .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
            .FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        End With

        With btnEmailAdd
            .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
            .BackColor = Color.Transparent
            .BackColor = Color.FromArgb(0, 107, 163)
            .ForeColor = Color.FromArgb(255, 255, 255)
            .Font = New Font("Neo Sans", 9, FontStyle.Bold)
            .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
            .FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        End With

        With btnOfficeNo
            .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
            .BackColor = Color.Transparent
            .BackColor = Color.FromArgb(0, 107, 163)
            .ForeColor = Color.FromArgb(255, 255, 255)
            .Font = New Font("Neo Sans", 9, FontStyle.Bold)
            .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
            .FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        End With

        With btnMblNoAdd
            .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
            .BackColor = Color.Transparent
            .BackColor = Color.FromArgb(0, 107, 163)
            .ForeColor = Color.FromArgb(255, 255, 255)
            .Font = New Font("Neo Sans", 9, FontStyle.Bold)
            .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
            .FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        End With


        btnAdd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnAdd.BackColor = Color.Transparent
        btnAdd.BackColor = Color.FromArgb(0, 107, 163)
        btnAdd.ForeColor = Color.FromArgb(255, 255, 255)
        btnAdd.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnAdd.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnAdd.FlatStyle = FlatStyle.Flat
        btnAdd.FlatAppearance.BorderSize = 0
        btnAdd.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        'btnAdd.Size = New Size(85, 30)

        With grdAddressMapping
            .VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
            .Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
            .Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
            .Rows.MinSize = 25
            .Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
            .Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            .Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            .Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            .Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            .Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        End With

        With grdMapping
            .VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
            .Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
            .Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
            .Rows.MinSize = 25
            .Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
            .Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            .Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            .Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            .Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            .Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        End With

        Dim lblFont As New Font("Neo Sans", 10, FontStyle.Regular)
        '--- Labels 
        With lblMobileNo
            .Font = lblFont
            .Dock = DockStyle.Top
            .Size = New Size(txtTelPhone.Left - .Left, txtTelPhone.Height)
            .Margin = New Padding(3, 3, 0, 0)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With
        With lblGender
            .Font = lblFont
            .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Size = New Size(cboGender.Left - .Left, cboGender.Height)
            .Margin = New Padding(3, 3, 0, 0)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With

        With lblFirstName
            .Font = lblFont
            .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Margin = New Padding(3, 3, 0, 0)
            .Size = New Size(txtFirstName.Left - .Left, txtFirstName.Height)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With
        With lblMiddleName
            .Font = lblFont
            .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Margin = New Padding(3, 3, 0, 0)
            .Size = New Size(txtMiddleName.Left - .Left, txtMiddleName.Height)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With

        With lblLastName
            .Font = lblFont
            .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Margin = New Padding(3, 3, 0, 0)
            .Size = New Size(txtLastName.Left - .Left, txtLastName.Height)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With


        With lblDOB
            .Font = lblFont
            .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Size = New Size(dtpDOB.Left - .Left, dtpDOB.Height)
            .Margin = New Padding(3, 3, 0, 0)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With


        With lblCompanyName
            .Font = lblFont
            .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Margin = New Padding(3, 3, 0, 0)
            .Size = New Size(txtCompanyName.Left - .Left, txtCompanyName.Height)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With
        With lblPrimaryAdd
            .Font = lblFont
            '   .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            ' .Size = New Size(txtCompanyName.Left - .Left, txtCompanyName.Height)
            .Margin = New Padding(3, 3, 0, 0)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With
        With lblDepartment
            .Font = lblFont
            ' .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Margin = New Padding(3, 3, 0, 0)
            .Size = New Size(txtDepartment.Left - .Left, txtDepartment.Height)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With

        With lblAddressType
            .Font = lblFont
            '.Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Margin = New Padding(3, 3, 0, 0)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With

        With lblAddress1
            .Font = lblFont
            ' .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Margin = New Padding(3, 3, 0, 0)
            .Size = New Size(txtCustomerAddress.Left - .Left, txtCustomerAddress.Height)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With

        With lblAddress2
            .Font = lblFont
            ' .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Margin = New Padding(3, 3, 0, 0)
            .Size = New Size(txtAddress2.Left - .Left, txtAddress2.Height)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With

        With lblAddress3
            .Font = lblFont
            ' .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Margin = New Padding(3, 3, 0, 0)
            .Size = New Size(txtAddress3.Left - .Left, txtAddress3.Height)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With

        With lblAddress4
            .Font = lblFont
            '.Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Margin = New Padding(3, 3, 0, 0)
            .Size = New Size(txtAddress4.Left - .Left, txtAddress4.Height)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With

        With lblCountry
            .Font = lblFont
            '     .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Margin = New Padding(3, 3, 0, 0)
            .Size = New Size(cboCountry.Left - .Left, cboCountry.Height)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With
        With lblState
            .Font = lblFont
            '.Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Margin = New Padding(3, 3, 0, 0)
            .Size = New Size(cboState.Left - .Left, cboState.Height)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With

        With lblCity
            .Font = lblFont
            ' .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Margin = New Padding(3, 3, 0, 0)
            .Size = New Size(txtCity.Left - .Left, txtCity.Height)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With

        With lblZip
            .Font = lblFont
            '  .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Margin = New Padding(3, 3, 0, 0)
            .Size = New Size(txtZipCode.Left - .Left, txtZipCode.Height)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With

        With lblResidenceNo
            .Font = lblFont
            '  .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Margin = New Padding(3, 3, 0, 0)
            '   .Size = New Size(txtResidenceNo.Left - .Left, txtResidenceNo.Height)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With

        With lblOfficeNo
            .Font = lblFont
            ' .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Margin = New Padding(3, 3, 0, 0)
            ' .Size = New Size(txtOfficeNo.Left - .Left, txtOfficeNo.Height)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With


        With lblFaxNo
            .Font = lblFont
            ' .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Margin = New Padding(3, 3, 0, 0)
            .Size = New Size(txtFaxNumber1.Left - .Left, txtFaxNumber1.Height)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With

        With lblEmailAddress
            .Font = lblFont
            '  .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Margin = New Padding(3, 3, 0, 0)
            .Size = New Size(txtEmailId.Left - .Left, txtEmailId.Height)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With

        With lblLoyalityNumber
            .Font = lblFont
            '   .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Margin = New Padding(3, 3, 0, 0)
            .Size = New Size(txtLPM.Left - .Left, txtLPM.Height)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With

        With CtrlLbltiers
            .Font = lblFont
            '    .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Margin = New Padding(3, 3, 0, 0)
            .Size = New Size(txtTierType.Left - .Left, txtTierType.Height)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With

        With CtrLblStatus
            .Font = lblFont
            ' .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Size = New Size(CtrlLblStatusValue.Left - .Left, CtrlLblStatusValue.Height)
            .Margin = New Padding(3, 3, 0, 0)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With

        With CtrlLblStatusValue
            .Font = lblFont
            ' .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Margin = New Padding(3, 3, 0, 0)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With

        With lblBalancePoint
            .Font = lblFont
            '  .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Size = New Size(txtBalancePoint.Left - .Left, txtBalancePoint.Height)
            .Margin = New Padding(3, 3, 0, 0)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With

        With lblSwipeCard
            .Font = lblFont
            ' .Dock = DockStyle.Top
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Size = New Size(txtSwipeCard.Left - .Left, txtSwipeCard.Height)
            .Margin = New Padding(3, 3, 0, 0)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With
        tbAddress.VisualStyle = C1.Win.C1Command.VisualStyle.Classic
        tbAddress.TabAreaBackColor = Color.FromArgb(79, 79, 79)
        C1Sizer1.BackColor = Color.FromArgb(79, 79, 79)
        txtTelPhone.Margin = New Padding(3, 3, 0, 3)
        txtFirstName.Margin = New Padding(3, 3, 0, 3)
        txtLastName.Margin = New Padding(3, 3, 0, 3)
        txtMiddleName.Margin = New Padding(3, 3, 0, 3)
        cboAddressType.Margin = New Padding(3, 3, 0, 3)
        cboGender.Margin = New Padding(3, 3, 0, 3)
        txtCompanyName.Margin = New Padding(3, 3, 0, 3)
        dtpDOB.Margin = New Padding(3, 3, 0, 3)
        lblResidenceNo.Margin = New Padding(3, 3, 0, 3)
        lblOfficeNo.Margin = New Padding(3, 3, 0, 3)
        lblFaxNo.Margin = New Padding(3, 3, 0, 3)
        lblEmailAddress.Margin = New Padding(3, 3, 0, 3)
    End Function

    'Private Sub btnMblNoAdd_Click(sender As Object, e As EventArgs)
    '    AddButton()
    '    mblcount += 1
    '    mblTotalCount += 1
    'End Sub

    'Private Sub AddMobileNumber_Click(sender As Object, e As EventArgs)
    '    AddButton()
    '    mblcount += 1
    '    mblTotalCount += 1
    'End Sub
    ''' <summary>
    ''' Delete alternate mobile number and arrage order of alternate number naming
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DeleteMobileNo_Click(sender As Object, e As EventArgs)
        'pn.Controls.RemoveAt(1)
        Try
            Dim Tblpnl = DirectCast(DirectCast(sender, System.Windows.Forms.Button).Parent, System.Windows.Forms.TableLayoutPanel).Tag
            Dim tablePanel = (From pnl As TableLayoutPanel In mblPanel.Controls Where pnl.Tag = Tblpnl Select pnl.Controls(0)).FirstOrDefault()

            '  articlePanel.Controls.Remove(tablePanel)


            For Each ctrl As Control In mblPanel.Controls
                If TypeOf ctrl Is TableLayoutPanel Then

                    If ctrl.Tag = Tblpnl Then
                        mblPanel.Controls.Remove(ctrl)

                        '-------------Delete From datatable
                        For Each ctrl1 As Control In DirectCast(ctrl, System.Windows.Forms.TableLayoutPanel).Controls
                            If TypeOf ctrl1 Is TextBox Then
                                Dim selectedRows() As DataRow = dtCommunication.Select("ContactDetails='" & ctrl1.Text & "'", "", DataViewRowState.CurrentRows)
                                For Each dr As DataRow In selectedRows
                                    dr.Delete()
                                Next
                                Dim count = 1
                                For index = 0 To dtCommunication.Rows.Count - 1
                                    dtCommunication.Rows(index)("SrNo") = count
                                    count += 1
                                Next
                                rowNumber = count
                            End If
                        Next
                    End If
                End If



            Next

            '----FOr label number
            Dim lblcounter As Integer = 1
            For Each ctrl1 As Control In mblPanel.Controls
                For Each ctrl As Control In DirectCast(ctrl1, System.Windows.Forms.TableLayoutPanel).Controls
                    If TypeOf ctrl Is Label Then
                        ctrl.Text = "Alternate Mobile Number" & ":" & lblcounter & " "
                        lblcounter += 1
                    End If
                Next
            Next




            mblcount -= 1
            mblTotalCount -= 1



            lblcounter = 1
            For index = 1 To dtCommunication.Rows.Count - 1
                If dtCommunication.Rows(index)("SortOrder") < 100 Then
                    dtCommunication.Rows(index)("ContactType") = "Alternate Mobile Number" & ":" & lblcounter & " "
                    lblcounter += 1
                End If
            Next

            'Dim count = 1
            'For index = 0 To dtCommunication.Rows.Count - 1
            '    dtCommunication.Rows(index)("SrNo") = count


            '    If TypeOf ctrl1 Is Label Then
            '        ctrl1.Text = "       Alternate Mobile Number" & " " & count
            '        dtCommunication.Rows(index)("ContactType") = ctrl1.Text.Trim()
            '    End If



            '    count += 1
            'Next
            'rowNumber = count

            'tablePanel.Dispose()

        Catch ex As Exception
            LogException(ex)
        End Try



    End Sub

    Private Sub RefreshAddressGrid()
        Try
            grdAddressMapping.DataSource = dtAddressDetails
            grdAddressMapping.Rows.MinSize = 28
            grdAddressMapping.Cols("Delete").Caption = ""
            grdAddressMapping.Cols("Delete").Width = 20
            grdAddressMapping.Cols("Delete").ComboList = "..."

            grdAddressMapping.Cols("Address Type").Width = 150
            grdAddressMapping.Cols("Address Type").AllowEditing = False
            grdAddressMapping.Cols("Address Type").Caption = getValueByKey("frmNewCustomer.grdAddressMapping.AddressType") '"Address Type" '

            grdAddressMapping.Cols("Address").Caption = getValueByKey("frmNewCustomer.grdAddressMapping.Address") '"Address" '
            grdAddressMapping.Cols("Address").AllowEditing = False
            grdAddressMapping.Cols("Address").Width = 500

            grdAddressMapping.Cols("PrimaryAddress").Width = 150
            grdAddressMapping.Cols("PrimaryAddress").AllowEditing = False
            grdAddressMapping.Cols("PrimaryAddress").Caption = getValueByKey("frmNewCustomer.grdAddressMapping.PrimaryAddress") '"Primary Address?" '

            grdAddressMapping.AutoSizeCols()
            grdAddressMapping.Rows(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub RefreshCommunicationGrid()
        Try

            grdMapping.DataSource = dtCommunication
            grdMapping.Rows.MinSize = 28
            grdMapping.Cols("Delete").Caption = ""
            grdMapping.Cols("Delete").Width = 20
            grdMapping.Cols("Delete").ComboList = "..."

            grdMapping.Cols("ContactType").Width = 300
            grdMapping.Cols("ContactType").AllowEditing = False
            grdMapping.Cols("ContactType").Caption = getValueByKey("frmNewCustomer.grdMapping.ContactType") '"Contact Type" ''

            grdMapping.Cols("ContactDetails").Caption = getValueByKey("frmNewCustomer.grdMapping.ContactDetails") '"Contact Details" '
            grdMapping.Cols("ContactDetails").AllowEditing = False
            grdMapping.Cols("ContactDetails").Width = 150

            'grdMapping.AutoSizeCols()
            grdMapping.Rows(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            ' grdMapping.Rows(1).AllowEditing = False

            If grdMapping.Rows.Count > 1 Then

                If mblcount > 0 Then
                    For Index = 1 To mblcount

                        ' If grdMapping.Rows(Index)("ContactType") = "Altr Mobile Number" Or grdMapping.Rows(Index)("ContactType") = "Mobile Number" Then
                        grdMapping.Rows(Index).AllowEditing = False
                        '   End If

                    Next
                End If


            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Switching of tab 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tbAddress_Click(sender As Object, e As EventArgs) Handles tbAddress.Click
        Try

            If tbAddress.SelectedIndex <> 0 Then
                If ValidatedSOCustomer() = False Then
                    tbAddress.SelectedIndex = 0
                    ' ShowMessage("Please enter all mandatory fields to continue.", getValueByKey("CLAE04"))
                    ShowMessage(getValueByKey("NC0001"), getValueByKey("CLAE04"))

                    Exit Sub
                End If
            End If
            If txtTelPhone.Text.Trim().Length < 10 Then
                tbAddress.SelectedIndex = 0
                ShowMessage("Please enter Valid Mobile Number", getValueByKey("CLAE04"))
                Exit Sub
            End If
            If CheckUniqueMobileNumber(txtTelPhone.Text.Trim(), CustomerNo) Then
                'ShowMessage("Mobile Is not unique", getValueByKey("CLAE05"))
                tbAddress.SelectedIndex = 0
                ShowMessage(getValueByKey("NC0007"), getValueByKey("CLAE04"))
                Exit Sub
            End If

            If tbAddress.SelectedIndex = 0 Then
                BtnPrevious.Visible = False
                BtnNext.Visible = True
                '----If alternate mbl no field blank then delete it
                DeleteAlternateMblNo()

            ElseIf tbAddress.SelectedIndex = 2 Then
                DeleteAlternateMblNo()
                AddDataInCommunicationGrid()
                BtnNext.Visible = True
                BtnPrevious.Visible = True
            ElseIf tbAddress.SelectedIndex = 3 Then
                BtnNext.Visible = False
                BtnPrevious.Visible = True
                For Index = 0 To dtCommunication.Rows.Count - 1
                    Dim hdr = dtCommunication.Select("ContactType='Email Address'")
                    If hdr.Length > 0 Then
                        CtrlLblStatusValue.Text = getValueByKey([Enum].GetName(GetType(SpectrumCommon.RegistrationType), SpectrumCommon.RegistrationType.Registered))
                    Else
                        CtrlLblStatusValue.Text = getValueByKey([Enum].GetName(GetType(SpectrumCommon.RegistrationType), SpectrumCommon.RegistrationType.Enrolled))
                    End If
                Next
            Else
                BtnNext.Visible = True
                BtnPrevious.Visible = True

            End If

        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Function ValidatedSOCustomer() As Boolean
        Try

            If txtFirstName.Text.Trim() = "" Then
                'ShowMessage(getValueByKey("SOC008"), "SOC008 - " & getValueByKey("CLAE04"))
                txtFirstName.Focus()
                Exit Function
            ElseIf txtLastName.Text.Trim() = "" Then
                ' ShowMessage(getValueByKey("SOC009"), "SOC009 - " & getValueByKey("CLAE04"))
                txtLastName.Focus()
                Exit Function
            ElseIf String.IsNullOrEmpty(txtTelPhone.Text.Trim()) Then
                ' ShowMessage("Mobile Number is Mandatory", getValueByKey("CLAE04"))
                txtTelPhone.Focus()
                Exit Function
            End If
            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
            Return False
        End Try
    End Function

    Private Sub InitializeComboBox()
        cboGender.DataSource = New List(Of String) From {"Male", "Female"}
        cboGender.SelectedIndex = -1
        cboAddressType.DisplayMember = "Value"
        cboAddressType.ValueMember = "Status"
        cboAddressType.DataSource = objCustm.GetAddressType()
        cboAddressType.SelectedIndex = 0
        cboCountry.DisplayMember = "AreaName"
        cboCountry.ValueMember = "AreaCode"
        cboCountry.DataSource = CustomerMaster.AreaInfoList.Where(Function(x) x.AreaCode = "IND").ToList()
        cboCountry.SelectedIndex = 0
    End Sub

    Private Sub cboCountry_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboCountry.SelectedValueChanged
        Try
            If Not String.IsNullOrEmpty(cboCountry.SelectedValue) Then
                cboState.DisplayMember = "AreaName"
                cboState.ValueMember = "AreaCode"
                cboState.DataSource = CustomerMaster.AreaInfoList.Where(Function(x) x.AreaType = AreaType.State AndAlso x.ParentAreaCode = cboCountry.SelectedValue).ToList()
                cboState.SelectedValue = "MH"
            Else
                cboState.DataSource = Nothing
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cboState_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboState.SelectedValueChanged
        Try
            Dim dtCity = objCustm.GetCityList(cboState.SelectedValue)
            If dtCity.Rows.Count > 0 Then
                If clsDefaultConfiguration.IsCustAddWild = True Then
                    'Dim listSource As List(Of [String]) = (From row In dtCity Select Convert.ToString(row("Description"))).Distinct.ToList()
                    'AndroidSearchtxtCity.lstNames = listSource
                    Call SetWildSearchTextBox(dtCity, AndroidSearchtxtCity, key:="KeyData", Value:="Value", searchData:="Description")
                Else
                    Dim Addressarray1 As String() = (From row In dtCity
                                Select Convert.ToString(row("Description"))).Distinct().ToArray()
                    txtCity.Values = Addressarray1
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cboAddressType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboAddressType.SelectedValueChanged
        If cboAddressType.SelectedValue = 2 Then
            lblDepartment.Visible = True
            txtDepartment.Visible = True
        Else
            lblDepartment.Visible = False
            txtDepartment.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' Email Validations
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtEmailId_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtEmailId.Validating
        If txtEmailId.Text <> "" Then
            If emailaddresscheck(txtEmailId.Text) Then
                txtEmailId.BackColor = Color.White
            Else
                e.Cancel = True
                txtEmailId.ErrorInfo.ErrorMessage = "Please provide correct email ID"
                txtEmailId.ErrorInfo.ErrorMessageCaption = "Error"
                txtEmailId.BackColor = Color.Red
            End If
        Else
            txtEmailId.BackColor = Color.White
        End If
    End Sub

    Private Sub txtTelPhone_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtTelPhone.TextChanged

        If Not String.IsNullOrEmpty(txtTelPhone.Text) Then
            CtrlLblStatusValue.Text = getValueByKey([Enum].GetName(GetType(SpectrumCommon.RegistrationType), SpectrumCommon.RegistrationType.Enrolled))
        End If
    End Sub

    Public Sub setAddressAutoComplete()
        Try
            Dim objClp As New clsCLPCustomer
            objClp.CustomerSearchParameters = clsDefaultConfiguration.CustSearchParameter
            dtCustmData = objClp.GetCustomerAddressInformation(CustmType, "")

            Dim Addressarray1 As String() = (From row In dtCustmData
                                  Select Convert.ToString(row("ADDRESSLN1"))).Distinct().ToArray()

            txtCustomerAddress.Values = Addressarray1

            Dim Addressarray2 As String() = (From row In dtCustmData
                                  Select Convert.ToString(row("ADDRESSLN2"))).Distinct().ToArray()

            txtAddress2.Values = Addressarray2

            Dim Addressarray3 As String() = (From row In dtCustmData
                                  Select Convert.ToString(row("ADDRESSLN3"))).Distinct().ToArray()

            txtAddress3.Values = Addressarray3

            Dim Addressarray4 As String() = (From row In dtCustmData
                                  Select Convert.ToString(row("ADDRESSLN4"))).Distinct().ToArray()

            txtAddress4.Values = Addressarray4

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Public Sub setAddressAutoComplete_AndroidSearch()
        Try
            Dim objClp As New clsCLPCustomer
            objClp.CustomerSearchParameters = clsDefaultConfiguration.CustSearchParameter
            dtCustmData = objClp.GetCustomerAddressInformation(CustmType, "")
            Dim dtCustmData1 As DataTable
            Dim dtCustmData2 As DataTable
            Dim dtCustmData3 As DataTable
            dtCustmData1 = dtCustmData.Copy
            dtCustmData2 = dtCustmData.Copy
            dtCustmData3 = dtCustmData.Copy
            'Dim listSource1 As List(Of [String]) = (From row In dtCustmData Select Convert.ToString(row("ADDRESSLN1"))).Distinct.ToList()
            'AndroidSearchtxtCustomerAddress.lstNames = listSource1
            Call SetWildSearchTextBox(dtCustmData, AndroidSearchtxtCustomerAddress, key:="KeyData", Value:="Value", searchData:="ADDRESSLN1")

            'Dim listSource2 As List(Of [String]) = (From row In dtCustmData Select Convert.ToString(row("ADDRESSLN2"))).Distinct.ToList()
            'AndroidSearchtxtAddress2.lstNames = listSource2
            Call SetWildSearchTextBox(dtCustmData1, AndroidSearchtxtAddress2, key:="KeyData", Value:="Value", searchData:="ADDRESSLN2")

            'Dim listSource3 As List(Of [String]) = (From row In dtCustmData Select Convert.ToString(row("ADDRESSLN3"))).Distinct.ToList()
            'AndroidSearchtxtAddress3.lstNames = listSource3
            Call SetWildSearchTextBox(dtCustmData2, AndroidSearchtxtAddress3, key:="KeyData", Value:="Value", searchData:="ADDRESSLN3")

            'Dim listSource4 As List(Of [String]) = (From row In dtCustmData Select Convert.ToString(row("ADDRESSLN4"))).Distinct.ToList()
            'AndroidSearchtxtAddress4.lstNames = listSource4
            Call SetWildSearchTextBox(dtCustmData3, AndroidSearchtxtAddress4, key:="KeyData", Value:="Value", searchData:="ADDRESSLN4")

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub txtAddress2_Leave(sender As System.Object, e As System.EventArgs) Handles txtAddress2.Leave
        Try
            If Not String.IsNullOrEmpty(txtAddress2.Text.ToString) Then
                Dim query = (From row In dtCustmData.AsEnumerable()
                                       Where row("ADDRESSLN2").ToString().ToUpper().Equals(txtAddress2.Text.ToString().ToUpper())
                                       Select row
                                       ).FirstOrDefault()
                If Not query Is Nothing Then
                    If Not String.IsNullOrEmpty(query("ADDRESSLN3").ToString()) Then
                        txtAddress3.Text = query("ADDRESSLN3")
                    End If
                    If Not String.IsNullOrEmpty(query("ADDRESSLN4").ToString()) Then
                        txtAddress4.Text = query("ADDRESSLN4")
                    End If
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub txtAddress3_Leave(sender As System.Object, e As System.EventArgs) Handles txtAddress3.Leave
        Try
            If Not String.IsNullOrEmpty(txtAddress3.Text.ToString) Then
                Dim query = (From row In dtCustmData.AsEnumerable()
                                       Where row("ADDRESSLN3").ToString().ToUpper().Equals(txtAddress2.Text.ToString().ToUpper())
                                       Select row
                                       ).FirstOrDefault()

                If Not query Is Nothing AndAlso Not String.IsNullOrEmpty(query("ADDRESSLN4").ToString()) Then
                    txtAddress4.Text = query("ADDRESSLN4")
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Dim btnFont As New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Dim btnLoc As New System.Drawing.Point(5, 3)
    Dim btnPadding As New Padding(0, 0, 0, 0)
    Dim lblFont As New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Dim lb As Label
    Dim lb1 As Label
    Dim pn As TableLayoutPanel
    Dim bt As Button
    Dim bt1 As Button
    Dim txt As TextBox
    Dim AddMobileNumber As New Button
    Dim DeleteMobileNo As New Button
    Dim mblcount As Integer = 1
    Dim mblTotalCount As Integer = 0
    ''' <summary>
    ''' Add Alternate mobile number
    ''' </summary>
    ''' <param name="mblAltrnumber"></param>
    ''' <remarks></remarks>
    Private Sub AddButton(Optional mblAltrnumber As String = "")
        Try
            lb = New Label()
            '  lb.SuspendLayout()
            lb.MaximumSize = New Size(0, 0)
            'lb.Size = New Size(ButtonSize.Width + 20, 30)

            ' lb.AutoSize = True
            lb.Margin = New Padding(3, 2, 0, 0)
            'lb.AutoSize = True
            Dim tip As ToolTip = New ToolTip()
            lb.Name = "Alternate" & mblcount
            'lb.Font = lblFont
            'If mblcount <= 9 Then
            '    lb.Text = "Alternate Mobile Number" & "  " & mblcount & " "
            'Else
            '    lb.Text = "Alternate Mobile Number " & "" & mblcount & " "
            'End If
            lb.Text = "Alternate Mobile Number" & ":" & mblcount & " "
            lb.Anchor = AnchorStyles.Left
            lb.TextAlign = ContentAlignment.TopLeft
            lb.Dock = DockStyle.Left
            'lb.Anchor = AnchorStyles.Right
            lb.Size = New System.Drawing.Size(230, 20)
            lb.Font = New Font("Calibri", 13, FontStyle.Regular)
            txt = New TextBox()
            txt.AutoSize = True
            txt.Margin = New Padding(3, 0, 0, 0)
            txt.MaximumSize = New System.Drawing.Size(0, 0)
            txt.MaxLength = 10
            txt.MinimumSize = New System.Drawing.Size(0, 0)
            txt.Name = "txtAlternate" & mblcount
            txt.Size = New System.Drawing.Size(153, 12)
            txt.Font = New Font("Verdana", 10, FontStyle.Regular)
            txt.Tag = Nothing
            txt.Dock = DockStyle.Left
            txt.Text = mblAltrnumber
            txt.MaxLength = 10
            AddHandler txt.KeyPress, AddressOf txt_Keypress


            bt = New Button
            '  bt.SuspendLayout()
            bt.Font = btnFont
            bt.MaximumSize = New Size(0, 0)
            bt.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            bt.Size = New Size(40, 24)
            bt.Location = btnLoc
            bt.Tag = mblcount
            bt.Name = "Delete" & mblcount
            bt.Anchor = AnchorStyles.Top
            ' bt.AutoSize = True
            bt.TabIndex = 1
            bt.Tag = "-"
            bt.Text = "-"
            bt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            bt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
            bt.UseVisualStyleBackColor = True
            bt.UseVisualStyleBackColor = C1.Win.C1Input.VisualStyle.Office2010Blue
            bt.Dock = DockStyle.Left
            bt.Margin = btnPadding
            AddHandler bt.Click, AddressOf DeleteMobileNo_Click

            'bt1 = New Button
            ''  bt.SuspendLayout()
            'bt1.Font = btnFont
            'bt1.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            'bt1.Size = New Size(ButtonSize.Width, ButtonSize.Height)
            'bt1.Location = btnLoc
            'bt1.Name = "add" & mblcount
            'bt1.Anchor = AnchorStyles.Top
            'bt1.AutoSize = True
            'bt1.TabIndex = 1
            'bt1.Tag = "+"
            'bt1.Text = "+"
            'bt1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            'bt1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
            'bt1.UseVisualStyleBackColor = True
            'bt1.Size = New Size(35, 28)
            'bt1.UseVisualStyleBackColor = C1.Win.C1Input.VisualStyle.Office2010Blue
            'bt1.Dock = DockStyle.Fill
            'bt1.Margin = btnPadding
            'AddHandler bt1.Click, AddressOf AddMobileNumber_Click


          

            pn = New TableLayoutPanel()
            pn.SuspendLayout()
            pn.MaximumSize = New Size(0, 0)
            pn.Margin = New Padding(0)
            pn.Padding = New Padding(0)
            pn.AutoSize = True
            pn.Dock = DockStyle.Fill
            pn.Size = New System.Drawing.Size(400, 28)

            pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.02829!))
            pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.0!))
            pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
            pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.19998!))

            pn.RowCount = 1
            pn.ColumnCount = 4
            pn.Controls.Add(lb, 0, 0)
            pn.Controls.Add(txt, 1, 0)
            pn.Controls.Add(bt, 3, 0)

            '------ Apply Theme here 
            If clsDefaultConfiguration.ThemeSelect <> "Default" Then
                Select Case clsDefaultConfiguration.ThemeSelect
                    Case "Theme 1"
                        Call AddtoTheme(lb, txt)
                    Case 2

                    Case Else

                End Select
            End If
           
            pn.Tag = mblcount

            mblPanel.Controls.Add(pn)
            mblPanel.AutoScroll = True
            mblPanel.HorizontalScroll.Enabled = False
            mblPanel.HorizontalScroll.Visible = False
            Dim vertScrollWidth = SystemInformation.VerticalScrollBarWidth
            mblPanel.Padding = New Padding(0, 0, vertScrollWidth, 0)
            pn.ResumeLayout()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub AddtoTheme(ByRef lb As Label, ByRef txt As TextBox)
        With lb
            .Font = lblFont
            .Dock = DockStyle.Top
            .Size = New Size(txt.Left - .Left, txt.Height)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With

        With bt
            .ForeColor = Color.FromArgb(255, 255, 255)
            .Font = New Font("Neo Sans", 9, FontStyle.Bold)
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
            .FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        End With

    End Sub

    Private Sub grdAddressMapping_CellButtonClick(sender As Object, e As RowColEventArgs) Handles grdAddressMapping.CellButtonClick
        Try
            If MsgBox(getValueByKey("NC0004"), MsgBoxStyle.YesNo, "SO011 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                Dim selectedRows() As DataRow = dtAddressDetails.Select("SrNo='" & e.Row & "'", "", DataViewRowState.CurrentRows)
                For Each dr As DataRow In selectedRows
                    dr.Delete()
                Next
                Dim selectedRows1() As DataRow = dtAddress.Select("rowindex='" & e.Row & "'", "", DataViewRowState.CurrentRows)
                For Each dr As DataRow In selectedRows1
                    dr.Delete()
                Next
                Dim count = 1
                For index = 0 To dtAddressDetails.Rows.Count - 1
                    dtAddressDetails.Rows(index)("SrNo") = count
                    dtAddress.Rows(index)("rowindex") = count
                    count += 1
                Next
                rowindex = count

                If dtAddressDetails.Rows.Count = 1 Then
                    For index = 0 To dtAddressDetails.Rows.Count - 1
                        If dtAddressDetails.Rows(index)("PrimaryAddress") = "No" Then
                            dtAddressDetails.Rows(index)("PrimaryAddress") = "Yes"
                            dtAddress.Rows(index)("PrimaryAddress") = "Yes"
                        End If
                    Next
                End If

            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub grdMapping_CellButtonClick(sender As Object, e As RowColEventArgs) Handles grdMapping.CellButtonClick
        Try
            If MsgBox(getValueByKey("NC0005"), MsgBoxStyle.YesNo, "SO011 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                Dim selectedRows() As DataRow = dtCommunication.Select("SrNo='" & e.Row & "'", "", DataViewRowState.CurrentRows)
                For Each dr As DataRow In selectedRows
                    dr.Delete()
                Next
                Dim count = 1
                For index = 0 To dtCommunication.Rows.Count - 1
                    dtCommunication.Rows(index)("SrNo") = count
                    count += 1
                Next
                rowNumber = count
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Dim currentAddress As Integer
    Private Sub grdAddressMapping_DoubleClick(sender As Object, e As EventArgs) Handles grdAddressMapping.DoubleClick
        Try
            Dim currentrow = grdAddressMapping.RowSel
            currentAddress = currentrow
            Dim selectedRows() As DataRow = dtAddress.Select("rowindex='" & currentrow & "'", "", DataViewRowState.CurrentRows)
            For Each dr As DataRow In selectedRows
                If dr("AddressType") = "2" Then
                    lblDepartment.Visible = True
                    txtDepartment.Visible = True
                Else
                    lblDepartment.Visible = False
                    txtDepartment.Visible = False
                End If
                If clsDefaultConfiguration.IsCustAddWild = True Then
                    AndroidSearchtxtCustomerAddress.Text = dr("FlatNo")
                    AndroidSearchtxtAddress2.Text = dr("BuildingName")
                    AndroidSearchtxtAddress3.Text = dr("Area")
                    AndroidSearchtxtAddress4.Text = dr("Landmark")
                    AndroidSearchtxtCity.Text = dr("City")
                Else
                    txtCustomerAddress.Text = dr("FlatNo")
                    txtAddress2.Text = dr("BuildingName")
                    txtAddress3.Text = dr("Area")
                    txtAddress4.Text = dr("Landmark")
                    txtCity.Text = dr("City")
                End If

                txtZipCode.Text = dr("ZipCode")
                cboState.SelectedValue = dr("State")
                cboCountry.SelectedValue = dr("Country")
                txtDepartment.Text = dr("Department")
                chkPrimaryAddress.Checked = IIf(dr("PrimaryAddress") = "Yes", True, False)
                cboAddressType.SelectedValue = IIf(dr("AddressType") = 2, 2, 1)
            Next
            EditAddress = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Dim currentComm As Integer
    Private Sub grdMapping_DoubleClick(sender As Object, e As EventArgs) Handles grdMapping.DoubleClick
        Try
            ClearCommunicationValues()
            Dim contactype = grdMapping.Rows(grdMapping.RowSel)("ContactType").ToString()
            Dim currentrow = grdMapping.RowSel
            currentComm = currentrow
            Dim selectedRows() As DataRow = dtCommunication.Select("SrNo='" & currentrow & "' And ContactType='" & contactype & "'", "", DataViewRowState.CurrentRows)

            For Each dr As DataRow In selectedRows
                Dim str As String = dr("ContactDetails")
                Dim result As String() = str.Split("-")
                If contactype = "Residence Number" Then

                    txtResindence1.Text = result(0)
                    txtResidenceNo.Text = result(1)

                ElseIf contactype = "Office Number" Then

                    txtOfficeNo1.Text = result(0)
                    txtOfficeNo.Text = result(1)

                ElseIf contactype = "Fax Number" Then

                    txtFaxNumber1.Text = result(0)
                    txtFaxNumber.Text = result(1)

                ElseIf contactype = "Email Address" Then
                    txtEmailId.Text = str
                End If
            Next

            EditCommunContactValue = contactype
            EditCommun = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' For Edit Mode (Fill all Datatable information in dataset for particular customer)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetCustomerInformationInForm(ByVal ds As DataSet) As Boolean
        Try
            If ds IsNot Nothing Then
                '------Personal Details
                txtTelPhone.Text = ds.Tables("CLPCustomer").Rows(0)("Mobileno")
                txtFirstName.Text = ds.Tables("CLPCustomer").Rows(0)("FirstName").ToString()
                txtMiddleName.Text = ds.Tables("CLPCustomer").Rows(0)("MiddleName").ToString()
                txtLastName.Text = ds.Tables("CLPCustomer").Rows(0)("SURNAME").ToString()
                txtCompanyName.Text = ds.Tables("CLPCustomer").Rows(0)("CompanyName").ToString()
                dtpDOB.Value = ds.Tables("CLPCustomer").Rows(0)("BirthDate")
                cboGender.SelectedItem = ds.Tables("CLPCustomer").Rows(0)("Gender").ToString()
                uniquemblno = txtTelPhone.Text
                ClpCardNo = ds.Tables("CLPCustomer").Rows(0)("ClpProgramId").ToString()
                'CustomerNo = ds.Tables("CLPCustomer").Rows(0)("ClpProgramId").ToString()
                '-----Address Details
                If ds.Tables("CLPCustomerAddress").Rows.Count > 0 Then
                    For index = 0 To ds.Tables("CLPCustomerAddress").Rows.Count - 1

                        rowAddress = dtAddress.NewRow()
                        rowAddress("rowindex") = rowindex
                        rowAddress("FlatNo") = ds.Tables("CLPCustomerAddress").Rows(index)("AddressLn1")
                        rowAddress("BuildingName") = ds.Tables("CLPCustomerAddress").Rows(index)("AddressLn2")
                        rowAddress("Area") = ds.Tables("CLPCustomerAddress").Rows(index)("AddressLn3")
                        rowAddress("Landmark") = ds.Tables("CLPCustomerAddress").Rows(index)("AddressLn4")
                        rowAddress("City") = ds.Tables("CLPCustomerAddress").Rows(index)("CityCode")
                        rowAddress("ZipCode") = ds.Tables("CLPCustomerAddress").Rows(index)("PinCode")
                        rowAddress("State") = ds.Tables("CLPCustomerAddress").Rows(index)("StateCode")
                        rowAddress("Country") = ds.Tables("CLPCustomerAddress").Rows(index)("CountryCode")
                        rowAddress("Department") = ds.Tables("CLPCustomerAddress").Rows(index)("Department")
                        rowAddress("PrimaryAddress") = IIf(ds.Tables("CLPCustomerAddress").Rows(index)("DefaultAddress") = True, "Yes", "No") 'IIf(chkPrimaryAddress.Checked, "Yes", "No")
                        rowAddress("AddressType") = ds.Tables("CLPCustomerAddress").Rows(index)("AddressType")
                        rowAddress("SrNo") = ds.Tables("CLPCustomerAddress").Rows(index)("SrNo")
                        dtAddress.Rows.Add(rowAddress)

                        newRow = dtAddressDetails.NewRow()
                        newRow("SrNo") = rowindex
                        newRow("Address Type") = IIf(dtAddress.Rows(index)("AddressType") = 1, "Residential", "Office")
                        If dtAddress.Rows.Count > 0 Then
                            str = String.Empty
                            For coloumn = 1 To dtAddress.Columns.Count - 4
                                If Not dtAddress.Rows(index)(coloumn) Is DBNull.Value Then
                                    If Not dtAddress.Rows(index)(coloumn) = "" Then
                                        str += dtAddress.Rows(index)(coloumn) & ","
                                    End If
                                End If
                            Next
                        End If
                        newRow("Address") = str.Substring(0, str.Length - 1)
                        newRow("PrimaryAddress") = IIf(ds.Tables("CLPCustomerAddress").Rows(index)("DefaultAddress") = True, "Yes", "No")
                        dtAddressDetails.Rows.Add(newRow)
                        rowindex += 1
                    Next
                    RefreshAddressGrid()
                End If

                '-----For Communication details
                If ds.Tables("CLPCustomerContact").Rows.Count > 0 Then

                    For index = 0 To ds.Tables("CLPCustomerContact").Rows.Count - 1
                        Dim b As String = ds.Tables("CLPCustomerContact").Rows(index)("ContactType")
                        If b.Contains("Alternate") Then
                            AddButton(ds.Tables("CLPCustomerContact").Rows(index)("ContactValue"))
                            mblcount += 1
                            mblTotalCount += 1
                        End If

                        rowCommunication = dtCommunication.NewRow()
                        rowCommunication("SrNo") = rowNumber
                        rowCommunication("ContactType") = ds.Tables("CLPCustomerContact").Rows(index)("ContactType")
                        rowCommunication("ContactDetails") = ds.Tables("CLPCustomerContact").Rows(index)("ContactValue")
                        rowCommunication("SrNumber") = ds.Tables("CLPCustomerContact").Rows(index)("SrNo")
                        If b.Contains("Mobile") Then
                            rowCommunication("SortOrder") = sortMbl
                            sortMbl += 1
                        Else
                            rowCommunication("SortOrder") = sortOther
                            sortOther += 1
                        End If
                        dtCommunication.Rows.Add(rowCommunication)
                        rowNumber += 1
                    Next
                    Dim dataView As New DataView(dtCommunication)
                    dataView.Sort = "SortOrder ASC"
                    dtCommunication = dataView.ToTable()
                    '------------
                    Dim count = 1
                    For index = 0 To dtCommunication.Rows.Count - 1
                        dtCommunication.Rows(index)("SrNo") = count
                        count += 1
                    Next
                    rowNumber = count
                    RefreshCommunicationGrid()
                End If


                '-----For Loyality Details
                txtLPM.Text = ds.Tables("CLPCustomer").Rows(0)("CardNo").ToString()
                txtTierType.Text = ds.Tables("CLPCustomer").Rows(0)("CardType").ToString()
                CtrLblStatus.Text = ds.Tables("CLPCustomer").Rows(0)("resgistrationstatus").ToString()
                txtSwipeCard.Text = ds.Tables("CLPCustomer").Rows(0)("CardNo").ToString()
                txtBalancePoint.Text = ds.Tables("CLPCustomer").Rows(0)("TotalBalancePoint").ToString()

                dtAddressCompare = dtAddress.Copy()
                dtContactCompare = dtCommunication.Copy()
                uniquemblno = txtTelPhone.Text
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

#Region "Generic Validation"
    Function CapitalValidation(ByVal str As String) As String
        Try
            Dim data As String = str
            Dim word = data.Split(" ")
            Dim temp = word(word.Length - 1)
            'Dim text = File.ReadAllText("D:\word.txt")
            'text = text.ToUpper()
            'Dim result As String() = text.Split(",")

            '-------
            'If Not temp.Contains(".") Then

            If Not temp = String.Empty Then
                Dim dtAuto = objComn.GetAutocaptiliseWords()
                Dim drHdr() = dtAuto.Select("Word='" & temp & "'")

                If drHdr.Count > 0 Then
                    word(word.Length - 1) = temp
                    str = String.Join(" ", word)
                Else
                    temp = temp.Substring(0, 1).ToUpper() + temp.Substring(1, temp.Length - 1).ToLower()
                    word(word.Length - 1) = temp
                    str = String.Join(" ", word)
                End If

            End If
            'Else
            '    temp = temp.Substring(0, 1).ToUpper() + temp.Substring(1, temp.Length - 1).ToUpper()
            '    word(word.Length - 1) = temp
            '    str = String.Join(" ", word)
            'End If
            Return str
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'Private Sub txtFirstName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFirstName.KeyDown
    '    If e.KeyCode = Keys.Space Then
    '        txtFirstName.Text = CapitalValidation(txtFirstName.Text)
    '        txtFirstName.SelectionStart = txtFirstName.Text.Length
    '    End If
    '    'If e.KeyCode = 190 Then
    '    '    str = txtFirstName.Text
    '    '    Dim data As String = str
    '    '    Dim word = data.Split(" ")
    '    '    Dim temp = word(word.Length - 1)

    '    '    temp = temp.Substring(0, 1).ToUpper() + temp.Substring(1, temp.Length - 1).ToUpper()
    '    '    word(word.Length - 1) = temp
    '    '    str = String.Join(" ", word)
    '    '    txtFirstName.Text = str
    '    '    txtFirstName.SelectionStart = txtFirstName.Text.Length
    '    'End If
    'End Sub

    'Private Sub txtMiddleName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMiddleName.KeyDown
    '    If e.KeyCode = Keys.Space Then
    '        txtMiddleName.Text = CapitalValidation(txtMiddleName.Text)
    '        txtMiddleName.SelectionStart = txtMiddleName.Text.Length
    '    End If
    'End Sub

    'Private Sub txtLastName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtLastName.KeyDown
    '    If e.KeyCode = Keys.Space Then
    '        txtLastName.Text = CapitalValidation(txtLastName.Text)
    '        txtLastName.SelectionStart = txtLastName.Text.Length
    '    End If
    'End Sub

    'Private Sub txtCompanyName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCompanyName.KeyDown
    '    If e.KeyCode = Keys.Space Then
    '        txtCompanyName.Text = CapitalValidation(txtCompanyName.Text)
    '        txtCompanyName.SelectionStart = txtCompanyName.Text.Length
    '    End If
    'End Sub

    'Private Sub txtDepartment_KeyDown(sender As Object, e As KeyEventArgs) Handles txtDepartment.KeyDown
    '    If e.KeyCode = Keys.Space Then
    '        txtDepartment.Text = CapitalValidation(txtDepartment.Text)
    '        txtDepartment.SelectionStart = txtDepartment.Text.Length
    '    End If
    'End Sub

    'Private Sub txtCustomerAddress_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCustomerAddress.KeyDown
    '    If e.KeyCode = Keys.Space Then
    '        txtCustomerAddress.Text = CapitalValidation(txtCustomerAddress.Text)
    '        txtCustomerAddress.SelectionStart = txtCustomerAddress.Text.Length
    '    End If
    'End Sub

    'Private Sub txtAddress2_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAddress2.KeyDown
    '    If e.KeyCode = Keys.Space Then
    '        txtAddress2.Text = CapitalValidation(txtAddress2.Text)
    '        txtAddress2.SelectionStart = txtAddress2.Text.Length
    '    End If
    'End Sub

    'Private Sub txtAddress3_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAddress3.KeyDown
    '    If e.KeyCode = Keys.Space Then
    '        txtAddress3.Text = CapitalValidation(txtAddress3.Text)
    '        txtAddress3.SelectionStart = txtAddress3.Text.Length
    '    End If
    'End Sub

    'Private Sub txtAddress4_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAddress4.KeyDown
    '    If e.KeyCode = Keys.Space Then
    '        txtAddress4.Text = CapitalValidation(txtAddress4.Text)
    '        txtAddress4.SelectionStart = txtAddress4.Text.Length
    '    End If
    'End Sub

    'Private Sub txtCity_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCity.KeyDown
    '    If e.KeyCode = Keys.Space Then
    '        txtCity.Text = CapitalValidation(txtCity.Text)
    '        txtCity.SelectionStart = txtCity.Text.Length
    '    End If
    'End Sub
#End Region

    ''' <summary>
    ''' Validations for duplicate numbers in communication tab
    ''' </summary>
    ''' <param name="text1"></param>
    ''' <param name="text2"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CommunicationTabValidate(ByVal text1 As String, ByVal text2 As String, ByVal type As String) As Boolean
        Try
            If dtCommunication.Rows.Count > 0 Then
                Dim compare = text1 & "-" & text2
                For index = 0 To dtCommunication.Rows.Count - 1
                    If dtCommunication.Rows(index)("ContactType") = type Then
                        If dtCommunication.Rows(index)("ContactDetails") = compare Then
                            Return False
                        End If
                    End If
                Next
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Sub dtpDOB_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim dd As String = vCurrentDate.ToString("dd/MM/yyyy")
            dd = dd.Replace("-", "/")
            If Not dtpDOB.Value Is DBNull.Value Then
                Dim dt1 As DateTime = DateTime.ParseExact(dd, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                Dim dt2 As DateTime = DateTime.ParseExact(dtpDOB.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                Dim cmp = dt1.CompareTo(dt2)
                If cmp = -1 Then
                    'ShowMessage("Should not accept future dates", "" & getValueByKey("CLAE04"))
                    ShowMessage(getValueByKey("NC0002"), "" & getValueByKey("CLAE04"))
                    dtpDOB.Value = vCurrentDate
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Adding mobile and alternate number info in datacommunication grid 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function AddDataInCommunicationGrid() As Boolean
        Try
            Dim alternateLabel As String = String.Empty
            Dim alternatetxt As String = String.Empty

            If True Then
                Dim selecteMbl() As DataRow = dtCommunication.Select("ContactType='Mobile Number'")
                If selecteMbl.Length = 0 Then
                    rowCommunication = dtCommunication.NewRow()
                    rowCommunication("SrNo") = rowNumber
                    rowCommunication("ContactType") = "Mobile Number"
                    rowCommunication("ContactDetails") = txtTelPhone.Text
                    rowCommunication("SrNumber") = 0
                    rowCommunication("SortOrder") = sortMbl
                    dtCommunication.Rows.Add(rowCommunication)
                    rowNumber += 1
                    sortMbl += 1
                Else
                    dtCommunication.Rows(0)(2) = txtTelPhone.Text
                End If

                For Each ctrl1 As Control In mblPanel.Controls

                    For Each ctrl As Control In DirectCast(ctrl1, System.Windows.Forms.TableLayoutPanel).Controls
                        If TypeOf ctrl Is TextBox Then

                            alternatetxt = ctrl.Text

                        ElseIf TypeOf ctrl Is Label Then

                            alternateLabel = ctrl.Text.Trim()

                        End If
                    Next

                    Dim selectAlterMbl() As DataRow = dtCommunication.Select("ContactType='" + alternateLabel + "'")
                    If selectAlterMbl.Length = 0 Then
                        rowCommunication = dtCommunication.NewRow()
                        rowCommunication("SrNo") = rowNumber
                        rowCommunication("ContactType") = alternateLabel
                        rowCommunication("ContactDetails") = alternatetxt
                        rowCommunication("SrNumber") = 0
                        rowCommunication("SortOrder") = sortMbl
                        dtCommunication.Rows.Add(rowCommunication)
                        rowNumber += 1
                        sortMbl += 1
                    Else
                        dtCommunication.Rows(selectAlterMbl(0)("SrNo") - 1)("ContactDetails") = alternatetxt
                    End If

                Next

            End If


            Dim dataView As New DataView(dtCommunication)
            dataView.Sort = "SortOrder ASC"
            dtCommunication = dataView.ToTable()
            Dim count = 1
            For index = 0 To dtCommunication.Rows.Count - 1
                dtCommunication.Rows(index)("SrNo") = count
                count += 1
            Next
            rowNumber = count
            RefreshCommunicationGrid()

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Sub txtTelPhone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtResindence1.KeyPress, txtResidenceNo.KeyPress, txtFaxNumber1.KeyPress, txtFaxNumber.KeyPress, txtOfficeNo1.KeyPress, txtOfficeNo.KeyPress, txtZipCode.KeyPress, txtBalancePoint.KeyPress, txtTelPhone.KeyPress
        'Dim regex = New Regex("[^0-9]\b")
        'If (regex.IsMatch(e.KeyChar.ToString())) Then
        '    e.Handled = True
        'End If
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txt_Keypress(sender As Object, e As KeyPressEventArgs)
        'Dim regex = New Regex("[^0-9]\b")
        'If (regex.IsMatch(e.KeyChar.ToString())) Then
        '    e.Handled = True
        'End If
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Delete Alternate Mobile Function
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteAlternateMblNo() As Boolean
        Try

            Dim tempCtrl As New List(Of TableLayoutPanel)
            For Each ctrl1 As Control In mblPanel.Controls
                For Each ctrl As Control In DirectCast(ctrl1, System.Windows.Forms.TableLayoutPanel).Controls
                    If TypeOf ctrl Is TextBox Then
                        If String.IsNullOrEmpty(ctrl.Text) Then
                            tempCtrl.Add(ctrl1)
                            Exit For
                        End If
                    End If
                Next
            Next
            For Each cntrl In tempCtrl
                mblPanel.Controls.Remove(cntrl)
            Next
            If mblPanel.Controls.Count = 0 Then
                mblcount = 1
                mblTotalCount = 0
            End If
            Dim lblcount As Integer = 1
            For Each ctrl1 As Control In mblPanel.Controls
                For Each ctrl As Control In DirectCast(ctrl1, System.Windows.Forms.TableLayoutPanel).Controls
                    If TypeOf ctrl Is Label Then
                        ctrl.Text = "Alternate Mobile Number" & ":" & lblcount & " "
                        lblcount += 1
                        mblcount = lblcount
                        mblTotalCount = lblcount - 1
                    End If
                Next
            Next

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Sub btnMblNoAdd_Click(sender As Object, e As EventArgs) Handles btnMblNoAdd.Click
        AddButton()
        mblcount += 1
        mblTotalCount += 1
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            Dim objCust As New frmSearchCustomer
            Dim result As DialogResult = objCust.ShowDialog()
            If result = Windows.Forms.DialogResult.Cancel Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
                Exit Sub
            End If
            CustomerNo = objCust.vCustomerNo
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub BtnPrevious_Click(sender As Object, e As EventArgs) Handles BtnPrevious.Click
        Try
            tbAddress.SelectedIndex -= 1
            If tbAddress.SelectedIndex = 0 Then
                BtnPrevious.Visible = False
                '----If alternate mbl no field blank then delete it
                DeleteAlternateMblNo()
            ElseIf tbAddress.SelectedIndex = 2 Then
                AddDataInCommunicationGrid()
                BtnNext.Visible = True
                BtnPrevious.Visible = True
            ElseIf tbAddress.SelectedIndex = 3 Then
                BtnNext.Visible = False
            Else
                BtnNext.Visible = True
                BtnPrevious.Visible = True
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub BtnNext_Click(sender As Object, e As EventArgs) Handles BtnNext.Click
        Try
            If ValidatedSOCustomer() = False Then
                'ShowMessage("Please enter all mandatory fields to continue.", getValueByKey("CLAE04"))
                ShowMessage(getValueByKey("NC0001"), getValueByKey("CLAE04"))
                tbAddress.SelectedIndex = 0
                Exit Sub
            End If
            If CheckUniqueMobileNumber(txtTelPhone.Text.Trim(), CustomerNo) Then
                'ShowMessage("Mobile Is not unique", getValueByKey("CLAE05"))
                tbAddress.SelectedIndex = 0
                ShowMessage(getValueByKey("NC0007"), getValueByKey("CLAE04"))
                Exit Sub
            End If

            tbAddress.SelectedIndex += 1
            If tbAddress.SelectedIndex = 0 Then
                BtnPrevious.Visible = False
                '----If alternate mbl no field blank then delete it
                DeleteAlternateMblNo()
            ElseIf tbAddress.SelectedIndex = 2 Then
                AddDataInCommunicationGrid()
                BtnNext.Visible = True
                BtnPrevious.Visible = True
            ElseIf tbAddress.SelectedIndex = 3 Then
                BtnNext.Visible = False
                For Index = 0 To dtCommunication.Rows.Count - 1
                    Dim hdr = dtCommunication.Select("ContactType='Email Address'")
                    If hdr.Length > 0 Then
                        CtrlLblStatusValue.Text = getValueByKey([Enum].GetName(GetType(SpectrumCommon.RegistrationType), SpectrumCommon.RegistrationType.Registered))
                    Else
                        CtrlLblStatusValue.Text = getValueByKey([Enum].GetName(GetType(SpectrumCommon.RegistrationType), SpectrumCommon.RegistrationType.Enrolled))
                    End If
                Next
            Else
                BtnNext.Visible = True
                BtnPrevious.Visible = True
                '----If alternate mbl no field blank then delete it
                DeleteAlternateMblNo()
            End If
            'If tbAddress.SelectedIndex <> 2 Then
            '    btnClear_Click("", Nothing)
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        If ViewCust Then
            Me.Close()
            Exit Sub
        End If
        If MsgBox(" If you cancel the transaction, you will lose unsaved data. Do you want to continue?", MsgBoxStyle.YesNo, "Information") = MsgBoxResult.Yes Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        Else
            Me.DialogResult = Windows.Forms.DialogResult.None
        End If
    End Sub

    ''' <summary>
    ''' Adding and updating data in Address Grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If chkPrimaryAddress.Checked = True Then
                For index = 0 To dtAddressDetails.Rows.Count - 1
                    If dtAddressDetails.Rows(index)("PrimaryAddress") = "Yes" Then
                        dtAddressDetails.Rows(index)("PrimaryAddress") = "No"
                        dtAddress.Rows(index)("PrimaryAddress") = "No"
                    End If
                Next
            End If
            If clsDefaultConfiguration.IsCustAddWild = True Then  'vipul
                If AndroidSearchtxtAddress3.Text.Trim().Length = 0 AndAlso
                   AndroidSearchtxtAddress2.Text.Trim().Length = 0 AndAlso
                   AndroidSearchtxtAddress4.Text.Trim().Length = 0 AndAlso
                      AndroidSearchtxtCustomerAddress.Text.Trim().Length = 0 Then
                    ShowMessage("Please enter the address", getValueByKey("CLAE04"))
                    Exit Sub
                End If
            Else
                If txtAddress3.Text.Trim().Length = 0 AndAlso
              txtAddress2.Text.Trim().Length = 0 AndAlso
              txtAddress4.Text.Trim().Length = 0 AndAlso
              txtCustomerAddress.Text.Trim().Length = 0 Then
                    ShowMessage("Please enter the address", getValueByKey("CLAE04"))
                    Exit Sub
                End If
            End If

            If EditAddress = True Then

                If clsDefaultConfiguration.IsCustAddWild = True Then
                    dtAddress.Rows(currentAddress - 1)("FlatNo") = AndroidSearchtxtCustomerAddress.Text
                    dtAddress.Rows(currentAddress - 1)("BuildingName") = AndroidSearchtxtAddress2.Text
                    dtAddress.Rows(currentAddress - 1)("Area") = AndroidSearchtxtAddress3.Text
                    dtAddress.Rows(currentAddress - 1)("Landmark") = AndroidSearchtxtAddress4.Text
                    dtAddress.Rows(currentAddress - 1)("City") = AndroidSearchtxtCity.Text
                Else
                    dtAddress.Rows(currentAddress - 1)("FlatNo") = txtCustomerAddress.Text
                    dtAddress.Rows(currentAddress - 1)("BuildingName") = txtAddress2.Text
                    dtAddress.Rows(currentAddress - 1)("Area") = txtAddress3.Text
                    dtAddress.Rows(currentAddress - 1)("Landmark") = txtAddress4.Text
                    dtAddress.Rows(currentAddress - 1)("City") = txtCity.Text
                End If
                dtAddress.Rows(currentAddress - 1)("ZipCode") = txtZipCode.Text
                dtAddress.Rows(currentAddress - 1)("State") = cboState.SelectedValue
                dtAddress.Rows(currentAddress - 1)("Country") = cboCountry.SelectedValue
                dtAddress.Rows(currentAddress - 1)("Department") = txtDepartment.Text.Trim()
                dtAddress.Rows(currentAddress - 1)("PrimaryAddress") = IIf(chkPrimaryAddress.Checked, "Yes", "No")
                dtAddress.Rows(currentAddress - 1)("AddressType") = cboAddressType.SelectedValue

                dtAddressDetails.Rows(currentAddress - 1)("Address Type") = cboAddressType.Text
                If dtAddress.Rows.Count > 0 Then
                    str = String.Empty
                    ' For row = 0 To dtAddress.Rows.Count - 1
                    For coloumn = 1 To dtAddress.Columns.Count - 4
                        If Not dtAddress.Rows(currentAddress - 1)(coloumn) = "" Then
                            str += dtAddress.Rows(currentAddress - 1)(coloumn) & ","
                        End If
                    Next
                    ' Next
                End If
                dtAddressDetails.Rows(currentAddress - 1)("Address") = str.Substring(0, str.Length - 1)
                dtAddressDetails.Rows(currentAddress - 1)("PrimaryAddress") = IIf(chkPrimaryAddress.Checked, "Yes", "No")
                RefreshAddressGrid()
            Else

                rowAddress = dtAddress.NewRow()
                rowAddress("rowindex") = rowindex
                If clsDefaultConfiguration.IsCustAddWild = True Then
                    rowAddress("FlatNo") = AndroidSearchtxtCustomerAddress.Text
                    rowAddress("BuildingName") = AndroidSearchtxtAddress2.Text
                    rowAddress("Area") = AndroidSearchtxtAddress3.Text
                    rowAddress("Landmark") = AndroidSearchtxtAddress4.Text
                    rowAddress("City") = AndroidSearchtxtCity.Text
                Else
                    rowAddress("FlatNo") = txtCustomerAddress.Text
                    rowAddress("BuildingName") = txtAddress2.Text
                    rowAddress("Area") = txtAddress3.Text
                    rowAddress("Landmark") = txtAddress4.Text
                    rowAddress("City") = txtCity.Text
                End If
                rowAddress("ZipCode") = txtZipCode.Text
                rowAddress("State") = cboState.SelectedValue
                rowAddress("Country") = cboCountry.SelectedValue
                rowAddress("Department") = txtDepartment.Text.Trim()
                rowAddress("PrimaryAddress") = IIf(chkPrimaryAddress.Checked, "Yes", "No")
                rowAddress("AddressType") = cboAddressType.SelectedValue
                rowAddress("SrNo") = 0
                dtAddress.Rows.Add(rowAddress)

                newRow = dtAddressDetails.NewRow()
                newRow("SrNo") = rowindex
                newRow("Address Type") = cboAddressType.Text
                If dtAddress.Rows.Count > 0 Then
                    str = String.Empty
                    For coloumn = 1 To dtAddress.Columns.Count - 4
                        If Not dtAddress.Rows(dtAddress.Rows.Count - 1)(coloumn) = "" Then
                            str += dtAddress.Rows(dtAddress.Rows.Count - 1)(coloumn) & ","
                        End If
                    Next
                End If
                If str = String.Empty Then
                    str = ""
                Else
                    str = str.Substring(0, str.Length - 1)
                End If

                newRow("Address") = str
                newRow("PrimaryAddress") = IIf(chkPrimaryAddress.Checked, "Yes", "No")
                dtAddressDetails.Rows.Add(newRow)

                RefreshAddressGrid()


                rowindex += 1

            End If
            btnClear_Click("", Nothing)
            EditAddress = False
            cboAddressType.Focus()
            cboAddressType.Select()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        chkPrimaryAddress.Checked = False
        cboAddressType.SelectedIndex = 0
        txtDepartment.Text = ""
        If clsDefaultConfiguration.IsCustAddWild = True Then
            AndroidSearchtxtCustomerAddress.Text = ""
            AndroidSearchtxtAddress2.Text = ""
            AndroidSearchtxtAddress3.Text = ""
            AndroidSearchtxtAddress4.Text = ""
            AndroidSearchtxtCity.Text = ""
        Else
            txtCustomerAddress.Text = ""
            txtAddress2.Text = ""
            txtAddress3.Text = ""
            txtAddress4.Text = ""
            txtCity.Text = ""
        End If
        txtZipCode.Text = ""
        lblDepartment.Visible = False
        txtDepartment.Visible = False
        EditAddress = False
    End Sub

    ''' <summary>
    ''' Adding and updating data in Communication Grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 

#Region "Communication Add And Update"

    Private Sub btnResidenceNo_Click(sender As Object, e As EventArgs) Handles btnResidenceNo.Click
        Try
            If Not txtResindence1.Text.Trim() = "" AndAlso Not txtResidenceNo.Text.Trim() = "" Then

                If CommunicationTabValidate(txtResindence1.Text, txtResidenceNo.Text, "Residence Number") = False Then
                    'ShowMessage("The Residence Number entered is already added.", getValueByKey("CLAE04"))
                    ShowMessage(String.Format(getValueByKey("NC0006"), "Residence Number"), "NC0006 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If

                If EditCommun And EditCommunContactValue = "Residence Number" Then
                    dtCommunication.Rows(currentComm - 1)("ContactType") = "Residence Number"
                    dtCommunication.Rows(currentComm - 1)("ContactDetails") = txtResindence1.Text & "-" & txtResidenceNo.Text

                Else
                    rowCommunication = dtCommunication.NewRow()
                    rowCommunication("SrNo") = rowNumber
                    rowCommunication("ContactType") = "Residence Number" '& countResidence &
                    rowCommunication("ContactDetails") = txtResindence1.Text & "-" & txtResidenceNo.Text
                    rowCommunication("SrNumber") = 0
                    rowCommunication("SortOrder") = sortOther
                    dtCommunication.Rows.Add(rowCommunication)
                    rowNumber += 1
                    sortOther += 1
                End If

                RefreshCommunicationGrid()
                txtResindence1.Text = ""
                txtResidenceNo.Text = ""
                EditCommun = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnOfficeNo_Click(sender As Object, e As EventArgs) Handles btnOfficeNo.Click
        Try
            If Not txtOfficeNo1.Text.Trim() = "" AndAlso Not txtOfficeNo.Text.Trim() = "" Then

                If CommunicationTabValidate(txtOfficeNo1.Text, txtOfficeNo.Text, "Office Number") = False Then
                    'ShowMessage("The Office Number entered is already added.", getValueByKey("CLAE04"))
                    ShowMessage(String.Format(getValueByKey("NC0006"), "Office Number"), "NC0006 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If

                If EditCommun And EditCommunContactValue = "Office Number" Then
                    dtCommunication.Rows(currentComm - 1)("ContactType") = "Office Number"
                    dtCommunication.Rows(currentComm - 1)("ContactDetails") = txtOfficeNo1.Text & "-" & txtOfficeNo.Text

                Else
                    rowCommunication = dtCommunication.NewRow()
                    rowCommunication("SrNo") = rowNumber
                    rowCommunication("ContactType") = "Office Number"
                    rowCommunication("ContactDetails") = txtOfficeNo1.Text & "-" & txtOfficeNo.Text
                    rowCommunication("SrNumber") = 0
                    rowCommunication("SortOrder") = sortOther
                    dtCommunication.Rows.Add(rowCommunication)
                    rowNumber += 1
                    sortOther += 1
                End If
                RefreshCommunicationGrid()
                txtOfficeNo1.Text = ""
                txtOfficeNo.Text = ""
                EditCommun = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnFaxNo_Click(sender As Object, e As EventArgs) Handles btnFaxNo.Click
        Try
            If Not txtFaxNumber1.Text.Trim() = "" AndAlso Not txtFaxNumber.Text.Trim() = "" Then

                If CommunicationTabValidate(txtFaxNumber1.Text, txtFaxNumber.Text, "Fax Number") = False Then
                    ' ShowMessage("The Fax Number entered is already added.", getValueByKey("CLAE04"))
                    ShowMessage(String.Format(getValueByKey("NC0006"), "Fax Number"), "NC0006 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If

                If EditCommun And EditCommunContactValue = "Fax Number" Then
                    dtCommunication.Rows(currentComm - 1)("ContactType") = "Fax Number"
                    dtCommunication.Rows(currentComm - 1)("ContactDetails") = txtFaxNumber1.Text & "-" & txtFaxNumber.Text

                Else
                    rowCommunication = dtCommunication.NewRow()
                    rowCommunication("SrNo") = rowNumber
                    rowCommunication("ContactType") = "Fax Number"
                    rowCommunication("ContactDetails") = txtFaxNumber1.Text & "-" & txtFaxNumber.Text
                    rowCommunication("SrNumber") = 0
                    rowCommunication("SortOrder") = sortOther
                    dtCommunication.Rows.Add(rowCommunication)
                    rowNumber += 1
                    sortOther += 1
                End If
                RefreshCommunicationGrid()
                txtFaxNumber1.Text = ""
                txtFaxNumber.Text = ""
                EditCommun = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnEmailAdd_Click(sender As Object, e As EventArgs) Handles btnEmailAdd.Click
        Try
            If Not txtEmailId.Text.Trim() = "" Then
                If dtCommunication.Rows.Count > 0 Then
                    Dim compare = txtEmailId.Text
                    For index = 0 To dtCommunication.Rows.Count - 1
                        If dtCommunication.Rows(index)("ContactType") = "Email Address" Then
                            If dtCommunication.Rows(index)("ContactDetails") = compare Then
                                ' ShowMessage("The Email Address entered is already added.", getValueByKey("CLAE04"))
                                ShowMessage(String.Format(getValueByKey("NC0006"), "Email Address"), "NC0006 - " & getValueByKey("CLAE04"))
                                Exit Sub
                            End If
                        End If
                    Next
                End If

                If EditCommun And EditCommunContactValue = "Email Address" Then
                    dtCommunication.Rows(currentComm - 1)("ContactType") = "Email Address"
                    dtCommunication.Rows(currentComm - 1)("ContactDetails") = txtEmailId.Text

                Else
                    rowCommunication = dtCommunication.NewRow()
                    rowCommunication("SrNo") = rowNumber
                    rowCommunication("ContactType") = "Email Address"
                    rowCommunication("ContactDetails") = txtEmailId.Text
                    rowCommunication("SrNumber") = 0
                    rowCommunication("SortOrder") = sortOther
                    dtCommunication.Rows.Add(rowCommunication)
                    rowNumber += 1
                    sortOther += 1
                End If

                RefreshCommunicationGrid()
                txtEmailId.Text = ""
                EditCommun = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

#End Region


    ''' <summary>
    ''' save logic for customer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try

            If ValidatedSOCustomer() = False Then
                ' ShowMessage("Please enter all mandatory fields to continue.", getValueByKey("CLAE04"))
                ShowMessage(getValueByKey("NC0001"), getValueByKey("CLAE04"))
                Exit Sub
            End If
            If txtTelPhone.Text.Trim().Length < 10 Then
                ShowMessage("Please enter Valid Mobile Number", getValueByKey("CLAE04"))
                Exit Sub
            End If
            '------------ALternate mobile number validations
            For Each ctrl1 As Control In mblPanel.Controls
                For Each ctrl As Control In DirectCast(ctrl1, System.Windows.Forms.TableLayoutPanel).Controls
                    If TypeOf ctrl Is TextBox Then
                        If ctrl.Text.Trim().Length < 10 Then
                            ShowMessage("Please enter Valid Alternate Mobile Number", getValueByKey("CLAE04"))
                            Exit Sub
                        End If
                    End If
                Next
            Next
            If CheckUniqueMobileNumber(txtTelPhone.Text.Trim(), CustomerNo) Then
                'ShowMessage("Mobile Is not unique", getValueByKey("CLAE05"))
                tbAddress.SelectedIndex = 0
                ShowMessage(getValueByKey("NC0007"), getValueByKey("CLAE04"))
                Exit Sub
            End If

            DeleteAlternateMblNo()
            AddDataInCommunicationGrid()
            '---Check Whether at least 1 primary 
            If dtAddressDetails.Rows.Count > 0 Then
                Dim result As DataRow() = dtAddressDetails.Select("PrimaryAddress ='Yes'")
                If result.Length = 0 Then
                    ShowMessage("Customer should have at least one primary address", getValueByKey("CLAE04"))
                    Exit Sub
                End If
            End If
            If EditCustomer Then
                For Each dr As DataRow In dtAddressCompare.Rows
                    If dr("SrNo") > 0 Then
                        Dim result As DataRow() = dtAddress.Select("SrNo=" + dr("SrNo").ToString())
                        If result.Count = 0 Then
                            Dim rowAddressdelete = dtAddressDelete.NewRow()
                            rowAddressdelete("rowindex") = dr("rowindex")
                            rowAddressdelete("FlatNo") = dr("FlatNo")
                            rowAddressdelete("BuildingName") = dr("BuildingName")
                            rowAddressdelete("Area") = dr("Area")
                            rowAddressdelete("Landmark") = dr("Landmark")
                            rowAddressdelete("City") = dr("City")
                            rowAddressdelete("ZipCode") = dr("ZipCode")
                            rowAddressdelete("State") = dr("State")
                            rowAddressdelete("Country") = dr("Country")
                            rowAddressdelete("Department") = dr("Department")
                            rowAddressdelete("PrimaryAddress") = dr("PrimaryAddress")
                            rowAddressdelete("AddressType") = dr("AddressType")
                            rowAddressdelete("SrNo") = dr("SrNo")
                            dtAddressDelete.Rows.Add(rowAddressdelete)
                        End If

                    End If
                Next
                For Each dr As DataRow In dtContactCompare.Rows

                    Dim result As DataRow() = dtCommunication.Select("SrNumber=" + dr("SrNumber").ToString())
                    If result.Count = 0 Then
                        Dim rowAddressdelete = dtContactdelete.NewRow()
                        rowAddressdelete("SrNo") = dr("SrNo")
                        rowAddressdelete("ContactType") = dr("ContactType")
                        rowAddressdelete("ContactDetails") = dr("ContactDetails")
                        rowAddressdelete("SrNumber") = dr("SrNumber")
                        dtContactdelete.Rows.Add(rowAddressdelete)
                    End If


                Next
            End If

            Dim request As New SaveCustomerRequest With {.CreatedAt = clsAdmin.SiteCode, .CreatedBy = clsAdmin.UserCode}
            request.CLPCustomer = New CLPCustomerDTO() With {.MobileNo = txtTelPhone.Text, .SiteCode = clsAdmin.SiteCode, .CreatedAt = clsAdmin.SiteCode, .CreatedBy = clsAdmin.UserCode, .EmailId = txtEmailId.Text, .FirstName = txtFirstName.Text.Trim(), .Gender = If(cboGender.SelectedItem Is Nothing, "", cboGender.SelectedItem.ToString()), .LastName = txtLastName.Text.Trim(), .MiddleName = txtMiddleName.Text.Trim(), .RegistrationStatus = CtrlLblStatusValue.Text, .CardType = txtTierType.Text, .CompanyName = txtCompanyName.Text.Trim(), .CardNumber = CustomerNo, .ClpProgId = ClpCardNo}

            If dtAddress.Rows.Count > 0 Then
                'For index = 0 To dtAddress.Rows.Count - 1
                '    request.CLPCustomer.AddressList.Add(New CLPCustomerAddressDTO With {.AddressType = dtAddress.Rows(index)("AddressType"), .AddLine1 = dtAddress.Rows(index)("FlatNo"), .AddLine2 = dtAddress.Rows(index)("BuildingName"), .AddLine3 = dtAddress.Rows(index)("Area"), .AddLine4 = dtAddress.Rows(index)("Landmark"), .City = dtAddress.Rows(index)("City"), .State = If(dtAddress.Rows(index)("State") Is Nothing, "", dtAddress.Rows(index)("State")), .Country = If(dtAddress.Rows(index)("Country") Is Nothing, "", dtAddress.Rows(index)("Country")), .PinCode = dtAddress.Rows(index)("ZipCode"), .Department = dtAddress.Rows(index)("Department"), .DefaultAddress = IIf(dtAddress.Rows(index)("PrimaryAddress") = "Yes", 1, 0), .SrNo = dtAddress.Rows(index)("SrNo")})
                'Next
                For index = 0 To dtAddress.Rows.Count - 1
                    request.CLPCustomer.AddressList.Add(New CLPCustomerAddressDTO With
                                                        {
                                                            .AddressType = dtAddress.Rows(index)("AddressType"),
                                                            .AddLine1 = IIf(IsDBNull(dtAddress.Rows(index)("FlatNo")), String.Empty, dtAddress.Rows(index)("FlatNo")).ToString(),
                                                            .AddLine2 = IIf(IsDBNull(dtAddress.Rows(index)("BuildingName")), String.Empty, dtAddress.Rows(index)("BuildingName")).ToString(),
                                                            .AddLine3 = IIf(IsDBNull(dtAddress.Rows(index)("Area")), String.Empty, dtAddress.Rows(index)("Area")).ToString(),
                                                            .AddLine4 = IIf(IsDBNull(dtAddress.Rows(index)("Landmark")), String.Empty, dtAddress.Rows(index)("Landmark")).ToString(),
                                                            .City = IIf(IsDBNull(dtAddress.Rows(index)("City")), String.Empty, dtAddress.Rows(index)("City")).ToString(),
                                                            .State = If(dtAddress.Rows(index)("State") Is Nothing, "", dtAddress.Rows(index)("State")),
                                                            .Country = If(dtAddress.Rows(index)("Country") Is Nothing, "", dtAddress.Rows(index)("Country")),
                                                            .PinCode = dtAddress.Rows(index)("ZipCode").ToString,
                                                            .Department = dtAddress.Rows(index)("Department").ToString,
                                                            .DefaultAddress = IIf(dtAddress.Rows(index)("PrimaryAddress") = "Yes", 1, 0),
                                                            .SrNo = dtAddress.Rows(index)("SrNo")
                                                        })
                Next
            Else
                If clsDefaultConfiguration.IsCustAddWild = True Then
                    request.CLPCustomer.AddressList.Add(New CLPCustomerAddressDTO With {.AddressType = cboAddressType.SelectedValue, .AddLine1 = AndroidSearchtxtCustomerAddress.Text, .AddLine2 = AndroidSearchtxtAddress2.Text, .AddLine3 = AndroidSearchtxtAddress3.Text, .AddLine4 = AndroidSearchtxtAddress4.Text, .City = AndroidSearchtxtCity.Text, .State = cboState.SelectedValue, .Country = cboCountry.SelectedValue, .PinCode = txtZipCode.Text, .DefaultAddress = 1})
                Else
                    request.CLPCustomer.AddressList.Add(New CLPCustomerAddressDTO With {.AddressType = cboAddressType.SelectedValue, .AddLine1 = txtCustomerAddress.Text, .AddLine2 = txtAddress2.Text, .AddLine3 = txtAddress3.Text, .AddLine4 = txtAddress4.Text, .City = txtCity.Text, .State = cboState.SelectedValue, .Country = cboCountry.SelectedValue, .PinCode = txtZipCode.Text, .DefaultAddress = 1})
                End If
            End If
            If dtCommunication.Rows.Count > 0 Then
                For index = 0 To dtCommunication.Rows.Count - 1
                    request.CLPCustomer.ContactList.Add(New ContactDTO With {.ContactType = dtCommunication.Rows(index)("ContactType"), .ContactValue = dtCommunication.Rows(index)("ContactDetails"), .SrNo = dtCommunication.Rows(index)("SrNumber")})
                Next
            End If

            Date.TryParse(dtpDOB.Value.ToString(), request.CLPCustomer.BirthDate)
            request.CLPCustomer.BirthDate = IIf(IsDBNull(dtpDOB.Value), DateTime.MinValue, dtpDOB.Value)
            Decimal.TryParse(txtBalancePoint.Text, request.CLPCustomer.BalancePoints)

            CustomerBL.SaveCustomerInfo(request, dtDeleteAddress:=dtAddressDelete, dtDeleteContact:=dtContactdelete)

            _dtCustmInfo = objCLPCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, request.CLPCustomer.CardNumber, CustFormat:=clsDefaultConfiguration.DetailedCustomerCreationformat, IsNewSalesOrder:=clsDefaultConfiguration.IsNewSalesOrder)
            If message Then
                ShowMessage(getValueByKey("CLPCustomerRegistrationMsg"), getValueByKey("CLAE04"))
            Else
                ShowMessage("Customer Details Updated Successfully", getValueByKey("CLAE04"))
            End If

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()

        Catch ex As Exception
            'ShowMessage("Unable to load screen", getValueByKey("CLAE05"))
            ShowMessage(getValueByKey("NC0003"), getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub frmNewCustomer_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        TableLayoutPanel1.Width = Me.Width - 50
        TableLayoutPanel1.Height = Me.Height - 50
    End Sub

    Public Function CheckUniqueMobileNumber(ByVal mobilenumber As String, Optional ByVal cardno As String = "") As Boolean
        If uniquemblno <> txtTelPhone.Text.Trim Then
            cardno = ""
        End If
        If objCustm.CheckMobileNoUnique(mobilenumber, clsAdmin.SiteCode, cardno) = False Then
            Return True
        End If
    End Function
    Public Function ClearCommunicationValues() As Boolean
        txtResidenceNo.Text = ""
        txtResindence1.Text = ""

        txtOfficeNo.Text = ""
        txtOfficeNo1.Text = ""

        txtFaxNumber.Text = ""
        txtFaxNumber1.Text = ""

        txtEmailId.Text = ""

    End Function

    Private Sub txtFirstName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFirstName.KeyPress, txtMiddleName.KeyPress, txtLastName.KeyPress, txtCompanyName.KeyPress, txtCustomerAddress.KeyPress, txtAddress2.KeyPress, txtAddress3.KeyPress, txtAddress4.KeyPress, txtCity.KeyPress, txtDepartment.KeyPress, txtEmailId.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtFirstName_Leave(sender As Object, e As EventArgs) Handles txtFirstName.Leave, txtMiddleName.Leave, txtLastName.Leave, txtCompanyName.Leave, _
                                                                     txtCustomerAddress.Leave, txtAddress2.Leave, txtAddress3.Leave, txtAddress4.Leave, txtCity.Leave, _
                                                                     txtDepartment.Leave, AndroidSearchtxtCustomerAddress.Leave, AndroidSearchtxtAddress2.Leave,
                                                                     AndroidSearchtxtAddress3.Leave, AndroidSearchtxtAddress4.Leave, AndroidSearchtxtCity.Leave
        Try
            'sender.Text = CapitalValidation(sender.Text)
            sender.Text = objComn.CapitalValidationStatement(sender.Text)
            sender.SelectionStart = sender.Text.Length
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtFirstName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFirstName.KeyDown, txtMiddleName.KeyDown, txtLastName.KeyDown, txtCompanyName.KeyDown, _
                                                                     txtCustomerAddress.KeyDown, txtAddress2.KeyDown, txtAddress3.KeyDown, txtAddress4.KeyDown, txtCity.KeyDown, _
                                                                     txtDepartment.KeyDown, AndroidSearchtxtCustomerAddress.KeyDown, AndroidSearchtxtAddress2.KeyDown,
                                                                     AndroidSearchtxtAddress3.KeyDown, AndroidSearchtxtAddress4.KeyDown, AndroidSearchtxtCity.KeyDown
        Try
            'If e.KeyCode = Keys.Space Then
            '    sender.Text = CapitalValidation(sender.Text)
            '    sender.SelectionStart = sender.Text.Length
            'End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

 

End Class